using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Linx.Portal.Authentication;
using Linx.Portal.Models;
using Linx.Tools;
using Newtonsoft.Json;
using RestSharp;


namespace Linx.Portal.Controllers
{
    public class AccountController : Controller
    {
        private const string SessionIdentifiedUser = "PortalLoginIdentifiedUser";
        private const string SessionIdentifiedSso = "PortalLoginIdentifiedSso";
        // Identifier-first: username → GetPortalLoginOptions → password and optional Microsoft SSO.

        //
        // GET: /Account/
        public ActionResult Login(RouteValueDictionary values)
        {
            if (string.Equals(Request["alterar"], "1", StringComparison.OrdinalIgnoreCase))
                ClearIdentifiedLogin();
            return LoginView();
        }

        [HttpPost]
        public ActionResult Login(LogOnModel model)
        {
            if (model == null)
                model = new LogOnModel();

            try
            {
                if (model.RecoverPassword)
                {
                    if (ModelState.IsValid)
                    {
                        Uri uri = new Uri(string.Format("{0}LinxFrameworkAutorizacao/RecoverUserPassword?userName={1}", Utils.GetServiceUrl(), model.UserName));
                        var result = WebClientHelper.Get(uri);
                        ViewBag.SuccessMessage = "E-mail enviado com sucesso.".Translate();
                    }
                    return LoginView(model);
                }

                if (model.IdentifyOnly || (string.IsNullOrWhiteSpace(model.Password) && !string.IsNullOrWhiteSpace(model.UserName)))
                    return IdentifyUser(model);

                if (string.IsNullOrWhiteSpace(model.UserName) && Session != null)
                    model.UserName = Session[SessionIdentifiedUser] as string;

                if (!model.UserName.IsNullOrEmpty() && !model.Password.IsNullOrEmpty())
                {
                    if (AuthenticateUser(model.UserName, model.Password, model.RememberMe))
                    {
                        ClearIdentifiedLogin();
                        PortalMfaClient.ClearSession(Session);
                        return RedirectToAction("Index", "Home", new RouteValueDictionary { { "formulario", HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["formulario"] }, { "supportMode", HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["supportMode"] }, { "showEnvironments", model.ShowEnvironments } });
                    }
                }
                else if (model.UserName.IsNullOrEmpty())
                {
                    ModelState.AddModelError("", "Informe o usuário.".Translate());
                }
                else
                {
                    ModelState.AddModelError("", "Informe a senha.".Translate());
                }
            }
            catch (Exception oException)
            {
                ModelState.AddModelError("", oException.Message);
            }
            return LoginView(model);
        }

        /// <summary>
        /// OmniPOS-equivalent LoginForceAsync entry: redirect browser to Azure AD authorize (prompt=login).
        /// </summary>
        [HttpGet]
        public async Task<ActionResult> SsoLogin(string userName)
        {
            if (!Utils.IsSsoEnabled())
            {
                PortalSsoAudit.Fail(userName, "OFF", "SSO não está habilitado.");
                ModelState.AddModelError("", "SSO não está habilitado.".Translate());
                return LoginView();
            }

            if (SsoLoginHelper.IsContingencyEnabled(Session) && Utils.IsSsoOfflineFallbackAllowed())
            {
                PortalSsoAudit.Fail(userName, "CONT", "SSO em modo contingência.");
                ModelState.AddModelError("", "SSO em modo contingência. Use usuário e senha local.".Translate());
                return LoginView();
            }

            try
            {
                if (userName.IsNullOrEmpty() && Session != null)
                    userName = Session[SessionIdentifiedUser] as string;
                PortalSsoAudit.Info(userName, "START", "Redirecionando para Azure AD.");
                Uri authorizeUrl = await SsoLoginHelper.BeginForceLoginAsync(Session, userName);
                return Redirect(authorizeUrl.ToString());
            }
            catch (Exception ex)
            {
                bool suggestContingency;
                string message = SsoLoginHelper.MapMsalException(ex, out suggestContingency);
                if (suggestContingency && Utils.IsSsoOfflineFallbackAllowed())
                    SsoLoginHelper.EnableContingency(Session);
                PortalSsoAudit.Fail(userName, "START", message);
                ModelState.AddModelError("", message.Translate());
                return LoginView();
            }
        }

        /// <summary>
        /// Azure AD redirect URI callback: exchange code → UPN → local NomeAutenticacao → Forms cookie.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult> SsoCallback(string code, string state, string error, string error_description)
        {
            string pendingUser = Session != null ? Session[SsoLoginHelper.PendingLocalUserSessionKey] as string : null;
            if (string.IsNullOrWhiteSpace(pendingUser) && Session != null)
                pendingUser = Session[SessionIdentifiedUser] as string;

            if (!Utils.IsSsoEnabled())
            {
                PortalSsoAudit.Fail(pendingUser, "OFF", "SSO não está habilitado no callback.");
                ModelState.AddModelError("", "SSO não está habilitado.".Translate());
                return LoginView();
            }

            if (!error.IsNullOrEmpty())
            {
                bool contingency = string.Equals(error, "temporarily_unavailable", StringComparison.OrdinalIgnoreCase);
                if (contingency && Utils.IsSsoOfflineFallbackAllowed())
                    SsoLoginHelper.EnableContingency(Session);

                string msg = !error_description.IsNullOrEmpty()
                    ? error_description
                    : (string.Equals(error, "access_denied", StringComparison.OrdinalIgnoreCase)
                        ? "O usuário abortou o processo de autenticação."
                        : ("Azure SSO error: " + error));
                PortalSsoAudit.Fail(pendingUser, "AZURE", msg);
                ModelState.AddModelError("", msg.Translate());
                return LoginView();
            }

            if (code.IsNullOrEmpty())
            {
                PortalSsoAudit.Fail(pendingUser, "CODE", "Callback Azure sem code.");
                ModelState.AddModelError("", "Azure não devolveu o código de autorização (callback sem code). Use o navegador em /Account/SsoLogin.".Translate());
                return LoginView();
            }

            try
            {
                AuthenticationResultModel auth = await SsoLoginHelper.CompleteForceLoginAsync(code, state, Session);
                if (auth == null || !auth.IsAuthenticated || auth.User == null || auth.User.Username.IsNullOrEmpty())
                {
                    string tokenMsg = auth != null && !auth.Message.IsNullOrEmpty() ? auth.Message : "Usuário não autenticado.";
                    PortalSsoAudit.Fail(pendingUser, "TOKEN", tokenMsg);
                    ModelState.AddModelError("", tokenMsg.Translate());
                    return LoginView();
                }

                string azureUpn = auth.User.Username;
                PortalSsoAudit.Info(pendingUser ?? azureUpn, "AZURE", "Azure autenticado UPN=" + azureUpn);

                // Prefer the NomeAutenticacao typed on CONTINUAR (session), not the Azure UPN prefix.
                string localLogin = SsoLoginHelper.ResolveLocalLoginAfterSso(Session, azureUpn);
                if (localLogin.IsNullOrEmpty())
                {
                    PortalSsoAudit.Fail(azureUpn, "BIND", "Login local vazio após Azure.");
                    ModelState.AddModelError("", "Usuário não autenticado.".Translate());
                    return LoginView();
                }

                string bindSource = !string.IsNullOrWhiteSpace(pendingUser) ? "session" : "azure-upn";
                PortalSsoAudit.Info(localLogin, "BIND", "local=" + localLogin + " azure=" + azureUpn + " source=" + bindSource);

                // Azure token is identity proof only — Portal session continues as the Linx login.
                string canonicalUser;
                if (!AuthenticateUserSso(localLogin, rememberMe: true, out canonicalUser))
                {
                    // AuthenticatePortalSso already wrote TIPO_EVENTO=F.
                    ModelState.AddModelError("", "Usuário autenticado no Azure, mas sem cadastro local. Ajuste o login na retaguarda.".Translate());
                    return LoginView();
                }

                SsoLoginHelper.ClearContingency(Session);
                SsoLoginHelper.ClearPendingLocalUser(Session);
                ClearIdentifiedLogin();
                PortalMfaClient.ClearSession(Session);

                string formulario = Request["formulario"] ?? (Request.UrlReferrer != null ? HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["formulario"] : null);
                string supportMode = Request["supportMode"] ?? (Request.UrlReferrer != null ? HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["supportMode"] : null);

                return RedirectToAction("Index", "Home", new RouteValueDictionary
                {
                    { "formulario", formulario },
                    { "supportMode", supportMode },
                    { "showEnvironments", false }
                });
            }
            catch (Exception ex)
            {
                bool suggestContingency;
                string message = SsoLoginHelper.MapMsalException(ex, out suggestContingency);
                if (suggestContingency && Utils.IsSsoOfflineFallbackAllowed())
                    SsoLoginHelper.EnableContingency(Session);
                if (!IsLocalSsoAuthFailure(message))
                    PortalSsoAudit.Fail(pendingUser, "EXC", message);
                ModelState.AddModelError("", message.Translate());
                return LoginView();
            }
        }

        [HttpPost]
        public JsonResult SendPasswordResetLink(string userName)
        {
            try
            {
                if (userName.IsNullOrEmpty())
                    return Json(new { success = false, message = "Informe o usuário.".Translate() });

                string callbackUrl = Url.Action("Login", "Account", null, Request.Url.Scheme);

                Uri uri = new Uri(string.Format("{0}LinxFrameworkAutorizacao/SendPasswordResetLink?userName={1}&callbackUrl={2}",
                    Utils.GetServiceUrl(), HttpUtility.UrlEncode(userName), HttpUtility.UrlEncode(callbackUrl)));
                WebClientHelper.Get(uri);

                // Mensagem genérica para não revelar se o usuário existe.
                return Json(new { success = true, message = "Se o usuário estiver cadastrado, você receberá um e-mail com o link para redefinir a senha.".Translate() });
            }
            catch (Exception oException)
            {
                return Json(new { success = false, message = oException.Message });
            }
        }

        [HttpGet]
        public JsonResult ValidateResetToken(string token)
        {
            try
            {
                if (token.IsNullOrEmpty())
                    return Json(new { valid = false }, JsonRequestBehavior.AllowGet);

                Uri uri = new Uri(string.Format("{0}LinxFrameworkAutorizacao/ValidatePasswordResetToken?token={1}",
                    Utils.GetServiceUrl(), HttpUtility.UrlEncode(token)));
                string result = WebClientHelper.Get(uri);

                bool valid = !result.IsNullOrEmpty() && result.ToLower().Contains("true");
                return Json(new { valid = valid }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { valid = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ResetPassword(string token, string newPassword)
        {
            try
            {
                if (token.IsNullOrEmpty() || newPassword.IsNullOrEmpty())
                    return Json(new { success = false, message = "Dados inválidos.".Translate() });

                Uri uri = new Uri(string.Format("{0}LinxFrameworkAutorizacao/ResetPasswordWithToken?token={1}&newPassword={2}",
                    Utils.GetServiceUrl(), HttpUtility.UrlEncode(token), HttpUtility.UrlEncode(newPassword)));
                WebClientHelper.Get(uri);

                return Json(new { success = true, message = "Senha redefinida com sucesso.".Translate() });
            }
            catch (Exception oException)
            {
                return Json(new { success = false, message = oException.Message });
            }
        }

        public ActionResult Authenticate(string usuario = null, string senha = null, string formulario = null, bool listaAmbientes = true)
        {
            string _usuario = null, _senha = null, _formulario = null;
            bool _showEnvironments = true;

            _usuario = usuario.IsNullOrEmpty() ? this.Request.Headers["usuario"] : usuario;
            _senha = senha.IsNullOrEmpty() ? this.Request.Headers["senha"] : senha;
            _formulario = formulario.IsNullOrEmpty() ? this.Request.Headers["formulario"] : formulario;
            _showEnvironments = listaAmbientes.IsNull()  ? _showEnvironments : listaAmbientes;

            if (User.Identity.IsAuthenticated || (!_usuario.IsNullOrEmpty() && !_senha.IsNullOrEmpty() && AuthenticateUser(_usuario, _senha, true)))
                return RedirectToAction("Index", "Home", new RouteValueDictionary { { "formulario", _formulario }, { "showEnvironments", _showEnvironments } });

            return RedirectToAction("Login", "Account", new RouteValueDictionary { { "formulario", _formulario.IsNull() ? "" : _formulario } });
        }

        private ActionResult IdentifyUser(LogOnModel model)
        {
            string user = (model.UserName ?? string.Empty).Trim();
            if (user.IsNullOrEmpty())
            {
                ModelState.AddModelError("", "Informe o usuário.".Translate());
                ClearIdentifiedLogin();
                return LoginView(model);
            }

            PortalLoginOptions options = LookupPortalLoginOptions(user);
            if (options != null && options.IndicaUsuarioServico)
            {
                ModelState.AddModelError("", "ERRAUT022 - Usuário de serviço não pode acessar pelo Portal.".Translate());
                ClearIdentifiedLogin();
                return LoginView(model);
            }
            string canonical = options != null && !string.IsNullOrWhiteSpace(options.NomeAutenticacao)
                ? options.NomeAutenticacao
                : user;
            if (Session != null)
            {
                Session[SessionIdentifiedUser] = canonical;
                Session[SessionIdentifiedSso] = options != null && options.UserUtilizaSso;
                SsoLoginHelper.RememberPendingLocalUser(Session, canonical);
            }
            if (options != null && options.UserUtilizaSso)
                PortalSsoAudit.Info(canonical, "IDENT", "Usuário identificado com SSO habilitado.");
            model.UserName = canonical;
            model.IdentifyOnly = false;
            model.Password = null;
            return LoginView(model);
        }

        private ActionResult LoginView(LogOnModel model = null)
        {
            if (model == null)
                model = new LogOnModel();
            BindIdentifiedLogin(model);
            return View("Login", model);
        }

        private void BindIdentifiedLogin(LogOnModel model)
        {
            string user = model != null ? model.UserName : null;
            if (user.IsNullOrEmpty() && Session != null)
                user = Session[SessionIdentifiedUser] as string;
            if (model != null && model.UserName.IsNullOrEmpty() && !user.IsNullOrEmpty())
                model.UserName = user;

            bool identified = Session != null && Session[SessionIdentifiedUser] != null && !string.IsNullOrWhiteSpace(user);
            bool userSso = identified && Session != null && Session[SessionIdentifiedSso] != null && Convert.ToBoolean(Session[SessionIdentifiedSso]);
            bool ssoEnabled = Utils.IsSsoEnabled();
            bool contingency = SsoLoginHelper.IsContingencyEnabled(Session);

            ViewBag.LoginStep = identified ? "password" : "identify";
            ViewBag.IdentifiedUserName = user;
            ViewBag.ShowSsoButton = identified && ssoEnabled && !contingency && userSso;
            ViewBag.SsoContingency = ssoEnabled && contingency;
        }

        private void ClearIdentifiedLogin()
        {
            if (Session == null)
                return;
            Session.Remove(SessionIdentifiedUser);
            Session.Remove(SessionIdentifiedSso);
            SsoLoginHelper.ClearPendingLocalUser(Session);
        }

        private static PortalLoginOptions LookupPortalLoginOptions(string userName)
        {
            try
            {
                var client = new RestClient(Utils.GetServiceUrl());
                var request = new RestRequest("LinxFrameworkAutorizacao/GetPortalLoginOptions");
                request.AddParameter("userName", userName);
                var result = client.ExecuteAsGet(request, "GET");
                if (result.ErrorException != null || result.StatusCode != System.Net.HttpStatusCode.OK || string.IsNullOrWhiteSpace(result.Content))
                    return new PortalLoginOptions();
                return JsonConvert.DeserializeObject<PortalLoginOptions>(result.Content) ?? new PortalLoginOptions();
            }
            catch
            {
                return new PortalLoginOptions();
            }
        }

        private bool AuthenticateUser(string user, string password, bool rememberMe)
        {
            bool logged = false;

            Linx.Security.Cryptography crypto = new Linx.Security.Cryptography();
            var client = new RestClient(Utils.GetServiceUrl());
            var request = new RestRequest("LinxFrameworkAutorizacao/AuthenticatePortal");
            request.AddParameter("authenticateParameters", crypto.Encrypt(String.Format("{0}||{1}", crypto.Encrypt(user.Trim()), crypto.Encrypt(password.Trim()))));

            string clientIp = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrWhiteSpace(clientIp))
                clientIp = Request.UserHostAddress;
            else if (clientIp.Contains(","))
                clientIp = clientIp.Split(',')[0].Trim();

            if (!string.IsNullOrWhiteSpace(clientIp))
                request.AddHeader("X-Client-IP", clientIp);
            request.AddHeader("X-Auth-Channel", "Portal");

            var result = client.ExecuteAsGet(request, "GET");

            if (result.ErrorException != null)
                throw new Exception(result.ErrorException.Message);
            else if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                // Prefer Service JSON/HTML body so Portal login shows the real EF/SQL error, not only "Internal Server Error".
                string detail = result.Content;
                if (!string.IsNullOrWhiteSpace(detail))
                {
                    detail = System.Text.RegularExpressions.Regex.Replace(detail, @"\s+", " ").Trim();
                    if (detail.Length > 800)
                        detail = detail.Substring(0, 800) + "...";
                    throw new Exception(string.Format("{0}: {1}", result.StatusDescription, detail));
                }
                throw new Exception(result.StatusDescription);
            }

            string content = result.Content != null ? result.Content.Replace("\"", string.Empty) : string.Empty;
            string[] resultLines = crypto.Decrypt(HttpUtility.UrlDecode(content)).Split(new string[] { "||" }, StringSplitOptions.None);

            if (crypto.Decrypt(resultLines[0]) == "0")
            {
                string errorMessage = resultLines.Length > 1 ? crypto.Decrypt(resultLines[1]) : ErrorConstants._UserBadNameOrPassword.Message;

                // Guarantee lockout message on the login screen when Membership IsLockedOut = true.
                if (IsMembershipUserLockedOut(user) || ErrorConstants.IsMembershipLockoutMessage(errorMessage))
                    errorMessage = ErrorConstants.FormatUserLockedOutMessage();

                throw new Exception(errorMessage);
            }
            else if (crypto.Decrypt(resultLines[0]) == "1")
            {
                FormsAuthentication.SetAuthCookie(user, rememberMe);
                logged = true;
            }

            return logged;
        }

        /// <summary>
        /// Passwordless Portal login after Azure AD proof. Cookie uses canonical NomeAutenticacao from Service.
        /// </summary>
        private bool AuthenticateUserSso(string localLogin, bool rememberMe, out string canonicalUser)
        {
            canonicalUser = null;
            Linx.Security.Cryptography crypto = new Linx.Security.Cryptography();
            var client = new RestClient(Utils.GetServiceUrl());
            var request = new RestRequest("LinxFrameworkAutorizacao/AuthenticatePortalSso");
            request.AddParameter("userName", localLogin);

            string clientIp = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrWhiteSpace(clientIp))
                clientIp = Request.UserHostAddress;
            else if (clientIp.Contains(","))
                clientIp = clientIp.Split(',')[0].Trim();

            if (!string.IsNullOrWhiteSpace(clientIp))
                request.AddHeader("X-Client-IP", clientIp);
            request.AddHeader("X-Auth-Channel", "PortalSSO");

            var result = client.ExecuteAsGet(request, "GET");

            if (result.ErrorException != null)
                throw new Exception(result.ErrorException.Message);
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception(result.StatusDescription);

            string content = result.Content != null ? result.Content.Replace("\"", string.Empty) : string.Empty;
            string[] resultLines = crypto.Decrypt(HttpUtility.UrlDecode(content)).Split(new string[] { "||" }, StringSplitOptions.None);

            if (crypto.Decrypt(resultLines[0]) == "0")
            {
                string errorMessage = resultLines.Length > 1 ? crypto.Decrypt(resultLines[1]) : "Usuário autenticado no Azure, mas sem cadastro local. Ajuste o login na retaguarda.";
                if (IsMembershipUserLockedOut(localLogin) || ErrorConstants.IsMembershipLockoutMessage(errorMessage))
                    errorMessage = ErrorConstants.FormatUserLockedOutMessage();
                throw new Exception(errorMessage);
            }

            if (crypto.Decrypt(resultLines[0]) == "1")
            {
                canonicalUser = resultLines.Length > 3 ? crypto.Decrypt(resultLines[3]) : localLogin;
                if (canonicalUser.IsNullOrEmpty())
                    canonicalUser = localLogin;
                FormsAuthentication.SetAuthCookie(canonicalUser, rememberMe);
                return true;
            }

            return false;
        }

        private static bool IsLocalSsoAuthFailure(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return false;
            return message.IndexOf("sem cadastro local", StringComparison.OrdinalIgnoreCase) >= 0
                || message.IndexOf("ERRAUT022", StringComparison.OrdinalIgnoreCase) >= 0
                || ErrorConstants.IsMembershipLockoutMessage(message);
        }

        private bool IsMembershipUserLockedOut(string userName)
        {
            try
            {
                var client = new RestClient(Utils.GetServiceUrl());
                var request = new RestRequest("LinxFrameworkAutorizacao/IsMembershipUserLockedOut");
                request.AddParameter("userName", userName);
                var result = client.ExecuteAsGet(request, "GET");
                if (result.ErrorException != null || result.StatusCode != System.Net.HttpStatusCode.OK || result.Content.IsNullOrEmpty())
                    return false;

                string content = result.Content.Replace("\"", string.Empty).Trim();
                bool locked;
                return bool.TryParse(content, out locked) && locked;
            }
            catch
            {
                return false;
            }
        }


    }
}

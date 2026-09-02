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
using RestSharp;


namespace Linx.Portal.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        public ActionResult Login(RouteValueDictionary values)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LogOnModel model)
        {
            try
            {
                // When SSO is on and not in contingency, reject classic login unless offline fallback is allowed.
                if (Utils.IsSsoEnabled()
                    && !SsoLoginHelper.IsContingencyEnabled(Session)
                    && !Utils.IsSsoOfflineFallbackAllowed()
                    && !model.RecoverPassword)
                {
                    ModelState.AddModelError("", "Use o login com Microsoft (SSO).".Translate());
                    return View();
                }

                if (ModelState.IsValid)
                {
                    if (model.RecoverPassword)
                    {
                        Uri uri = new Uri(string.Format("{0}LinxFrameworkAutorizacao/RecoverUserPassword?userName={1}", Utils.GetServiceUrl(), model.UserName));
                        var result = WebClientHelper.Get(uri);
                        ViewBag.SuccessMessage = "E-mail enviado com sucesso.".Translate();
                    }
                    else if (!model.UserName.IsNullOrEmpty() && !model.Password.IsNullOrEmpty())
                    {
                        if (AuthenticateUser(model.UserName, model.Password, model.RememberMe))
                        {
                            PortalMfaClient.ClearSession(Session);
                            return RedirectToAction("Index", "Home", new RouteValueDictionary { { "formulario", HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["formulario"] }, { "supportMode", HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["supportMode"] }, { "showEnvironments", model.ShowEnvironments } });
                        }
                    }
                }
            }
            catch (Exception oException)
            {
                ModelState.AddModelError("", oException.Message);
            }
            return View();
        }

        /// <summary>
        /// OmniPOS-equivalent LoginForceAsync entry: redirect browser to Azure AD authorize (prompt=login).
        /// </summary>
        [HttpGet]
        public async Task<ActionResult> SsoLogin()
        {
            if (!Utils.IsSsoEnabled())
            {
                ModelState.AddModelError("", "SSO não está habilitado.".Translate());
                return View("Login");
            }

            if (SsoLoginHelper.IsContingencyEnabled(Session) && Utils.IsSsoOfflineFallbackAllowed())
            {
                ModelState.AddModelError("", "SSO em modo contingência. Use usuário e senha local.".Translate());
                return View("Login");
            }

            try
            {
                Uri authorizeUrl = await SsoLoginHelper.BeginForceLoginAsync(Session);
                return Redirect(authorizeUrl.ToString());
            }
            catch (Exception ex)
            {
                bool suggestContingency;
                string message = SsoLoginHelper.MapMsalException(ex, out suggestContingency);
                if (suggestContingency && Utils.IsSsoOfflineFallbackAllowed())
                    SsoLoginHelper.EnableContingency(Session);
                ModelState.AddModelError("", message.Translate());
                return View("Login");
            }
        }

        /// <summary>
        /// Azure AD redirect URI callback: exchange code → UPN → local NomeAutenticacao → Forms cookie.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult> SsoCallback(string code, string state, string error, string error_description)
        {
            if (!Utils.IsSsoEnabled())
            {
                ModelState.AddModelError("", "SSO não está habilitado.".Translate());
                return View("Login");
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
                ModelState.AddModelError("", msg.Translate());
                return View("Login");
            }

            if (code.IsNullOrEmpty())
            {
                ModelState.AddModelError("", "Azure não devolveu o código de autorização (callback sem code). Use o navegador em /Account/SsoLogin.".Translate());
                return View("Login");
            }

            try
            {
                AuthenticationResultModel auth = await SsoLoginHelper.CompleteForceLoginAsync(code, state, Session);
                if (auth == null || !auth.IsAuthenticated || auth.User == null || auth.User.Username.IsNullOrEmpty())
                {
                    ModelState.AddModelError("", (auth != null && !auth.Message.IsNullOrEmpty() ? auth.Message : "Usuário não autenticado.").Translate());
                    return View("Login");
                }

                string localLogin = SsoLoginHelper.ExtractLocalLogin(auth.User.Username);
                if (localLogin.IsNullOrEmpty())
                {
                    ModelState.AddModelError("", "Usuário não autenticado.".Translate());
                    return View("Login");
                }

                // Azure token is not forwarded — only local session after Service validates cadastro.
                string canonicalUser;
                if (!AuthenticateUserSso(localLogin, rememberMe: true, out canonicalUser))
                {
                    ModelState.AddModelError("", "Usuário autenticado no Azure, mas sem cadastro local. Ajuste o login na retaguarda.".Translate());
                    return View("Login");
                }

                SsoLoginHelper.ClearContingency(Session);
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
                ModelState.AddModelError("", message.Translate());
                return View("Login");
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

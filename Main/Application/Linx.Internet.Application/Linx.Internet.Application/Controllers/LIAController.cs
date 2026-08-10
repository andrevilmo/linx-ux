using System;
using Linx.Internet.Application.Common.Filters;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AttributeRouting.Web.Mvc;
using Linx.Internet.Application.Common.Providers;
using RestSharp;
using System.Net;
using System.Configuration;
using System.Text;
using Linx.Internet.Application.Helpers;
using System.Linq;
using StackExchange.Profiling;
using Linx.Internet.Application.Models;
using Newtonsoft.Json;

namespace Linx.Internet.Application.Controllers
{
    public class LIAController : Controller
    {
        private MiniProfiler profiler = MiniProfiler.Current;

        //protected override ITempDataProvider CreateTempDataProvider()
        //{
        //    return new CookieTempDataProvider();
        //}

        [NoCache]
        [GET("Authentication")]
        public ActionResult Authentication(string uidEmpresa, string uidUsuario, string uidAplicacao, string loginUrl, string formulario, string idAmbiente, string uidGrupoEconomico, string nomeEmpresa, string grupoEconomico, string idGpecon, string usuarioAutenticacao, string supportMode, string urlWorkArea)
        {
            LoginInfo loginInfo = new LoginInfo();

            var _shellMode = BaseHelpers.GetShellMode();
            this.ViewData["_loginUrl"] = (this.Session["loginUrl"] == null ? ConfigurationManager.AppSettings.GetValue<string>("Portal", "http://localhost:8172/") : this.Session["loginUrl"].ToString());

            if (_shellMode == "DEV" || _shellMode == "SETUP")
            {
                return RedirectToAction("Loading");
            }

            if (User.Identity.IsAuthenticated)
            {
                string _uidEmpresa = null, _uidUsuario = null, _uidAplicacao = null, _loginUrl = null, _transaction = null, _idAmbiente = null, _uidGrupoEconomico = null, _nomeEmpresa = null, _grupoEconomico = null, _idGpecon = null, _usuarioAutenticacao = null, _supportMode = null;

                // utiliza os valores (passados por querystring ou header)
                _uidEmpresa = string.IsNullOrEmpty(uidEmpresa) == true ? this.Request.Headers["CurrentCompany"] : uidEmpresa;
                _uidUsuario = string.IsNullOrEmpty(uidUsuario) == true ? this.Request.Headers["CurrentUser"] : uidUsuario;
                _uidAplicacao = string.IsNullOrEmpty(uidAplicacao) == true ? this.Request.Headers["Application"] : uidAplicacao;
                _loginUrl = string.IsNullOrEmpty(loginUrl) == true ? this.Request.Headers["loginUrl"] : loginUrl;
                _transaction = string.IsNullOrEmpty(formulario) == true ? this.Request.Headers["formulario"] : formulario;
                _idAmbiente = string.IsNullOrEmpty(idAmbiente) == true ? this.Request.Headers["Environment"] : idAmbiente;
                _uidGrupoEconomico = string.IsNullOrEmpty(uidGrupoEconomico) == true ? this.Request.Headers["EconomicGroup"] : uidGrupoEconomico;
                _nomeEmpresa = string.IsNullOrEmpty(nomeEmpresa) == true ? this.Request.Headers["CompanyName"] : nomeEmpresa;
                _grupoEconomico = string.IsNullOrEmpty(grupoEconomico) == true ? this.Request.Headers["EconomicGroupName"] : grupoEconomico;
                _idGpecon = string.IsNullOrEmpty(idGpecon) == true ? this.Request.Headers["IdLinxGpecon"] : idGpecon;
                _usuarioAutenticacao = string.IsNullOrEmpty(usuarioAutenticacao) ? this.Request.Headers["UsuarioAutenticacao"] : usuarioAutenticacao;
                _supportMode = string.IsNullOrEmpty(supportMode) ? this.Request.Headers["SupporMode"] : supportMode;


                // consiste os parametros
                if (string.IsNullOrEmpty(_uidEmpresa) || string.IsNullOrEmpty(_uidUsuario) || string.IsNullOrEmpty(_uidAplicacao) || string.IsNullOrEmpty(_idAmbiente) || string.IsNullOrEmpty(_uidGrupoEconomico) || string.IsNullOrEmpty(_nomeEmpresa) || string.IsNullOrEmpty(_grupoEconomico) || string.IsNullOrEmpty(_idGpecon))
                {
                    ViewBag.Mensagem = "Parametros inv�lidos!";
                    return View();
                }

                // copia os valores para a sessao
                this.Session["loginUrl"] = _loginUrl;
                this.Session["transaction"] = _transaction;

                if (_shellMode == "DEV" || _shellMode == "SETUP")
                {
                    loginInfo.UidUsuario = Guid.Parse(_uidUsuario);
                    loginInfo.UsuarioAutenticacao = _usuarioAutenticacao;
                    loginInfo.UidGrupoEconomico = Guid.Parse(_uidGrupoEconomico);
                    loginInfo.DescricaoGrupoEconomico = _grupoEconomico;
                    loginInfo.IdLinxGrupoEconomico = Convert.ToInt32(_idGpecon);
                    loginInfo.Ambientes.Add(new AmbienteInfo() { IdTcsAmbiente = Convert.ToInt32(_idAmbiente), UidAplicacao = Guid.Parse(_uidAplicacao), UidEmpresa = Guid.Parse(_uidEmpresa), DescricaoEmpresa = _nomeEmpresa });
                }
                else
                {
                    string retorno;
                    loginInfo = this.AuthenticateUser(_uidEmpresa, _uidUsuario, _uidAplicacao, _idAmbiente, _usuarioAutenticacao, out retorno);

                    if (retorno.Length > 0)
                    {
                        ViewBag.Mensagem = retorno;
                        return View();
                    }
                }

                loginInfo.IdTcsAmbienteDefault = Convert.ToInt32(_idAmbiente);
                loginInfo.IsSupportMode = Convert.ToBoolean(_supportMode);
                loginInfo.UrlWorkArea = urlWorkArea;

                loginInfo.CacheKey = null;

                for (int i = 0; i < loginInfo.Ambientes.Count; i++)
                {
                    loginInfo.CacheKey = loginInfo.CacheKey + (loginInfo.CacheKey == null ? "" : "_") + loginInfo.Ambientes[i].IdTcsAmbiente;
                }

                loginInfo.Info = uidEmpresa + "||" + uidUsuario + "||" + uidAplicacao + "||" + loginUrl + "||" + formulario + "||" + idAmbiente + "||" + uidGrupoEconomico + "||" + nomeEmpresa + "||" + grupoEconomico + "||" + idGpecon + "||" + usuarioAutenticacao + "||" + supportMode;
                this.Session["loginInfo"] = loginInfo;
                this.Session["loginInfo_" + loginInfo.UidUsuario + "_" + loginInfo.IdTcsAmbienteDefault] = loginInfo;

                return RedirectToAction("Loading", new { formulario = _transaction });
            }

            return RedirectToAction("Unauthorized", "LIA");
        }


        [NoCache]
        [PreserveQueryString]
        [GET("UiRedirect")]

        public ActionResult UiRedirect()
        {
            LoginInfo loginInfo = this.Session["loginInfo"] as LoginInfo;

            if (loginInfo != null)
                return RedirectToAction("Loading");
            else if (User.Identity.Name == null || User.Identity.Name == string.Empty)
                return RedirectToAction("Unauthorized", "LIA");
            else
            {
                string retorno = string.Empty;
                var _serviceBus = System.Configuration.ConfigurationManager.AppSettings.GetValue("ServiceBus", "http://localhost:1710");
                var client = new RestClient(_serviceBus);
                var request = new RestRequest("LinxFrameworkUsuarioAutorizacao/GetTcsUsuarioAcessoAmbienteByEntitySearchNoAssociations");
                string jEntitySearch = "TcsUsuarioAcessoAmbiente{NomeAutenticacao#==#S" + User.Identity.Name + ";IndicaAcessoPadrao#==#BTrue}";
                request.AddParameter("jEntitySearch", jEntitySearch);

                var response = client.ExecuteAsGet(request, "GET");
                // erro na requisicao
                if (response.ErrorException != null)
                {
                    retorno = response.ErrorException.Message;
                }
                else if (response.StatusCode == HttpStatusCode.OK)
                {
                    LoggedUser[] users = JsonConvert.DeserializeObject<LoggedUser[]>(response.Content);

                    if (users == null || users.Count() == 0)
                        retorno = "Usu�rio n�o possui acesso padr�o !";
                    else
                    {
                        return RedirectToAction("Authentication", "LIA",
                             new
                             {
                                 uidEmpresa = users[0].UidEmpresa,
                                 uidUsuario = users[0].UidUsuario,
                                 uidAplicacao = users[0].UidAplicacao,
                                 loginUrl = string.Empty,
                                 formulario = string.Empty,
                                 idAmbiente = users[0].IdTcsAmbiente,
                                 uidGrupoEconomico = users[0].UidGrupoEconomico,
                                 nomeEmpresa = users[0].NomeEmpresa,
                                 grupoEconomico = users[0].GrupoEconomico,
                                 idGpecon = users[0].IdLinxGpecon,
                                 usuarioAutenticacao = users[0].NomeAutenticacao,
                                 supportMode = string.Empty,
                                 urlWorkArea = users[0].UrlWorkArea
                             }
                         );
                    }
                }
                else
                {
                    retorno = string.Concat("Retorno inv�lido!<BR>", response.StatusCode, " : ", ExtractError(response.Content));
                }

                if (retorno.Length > 0)
                {
                    ViewBag.Mensagem = retorno;
                    return View("Authentication");
                }
                else
                    return RedirectToAction("Unauthorized", "LIA");
            }
        }

        [NoCache]
        [PreserveQueryString]
        [GET("/")]
        public ActionResult Loading(string uidEmpresa, string uidUsuario, string uidAplicacao, string loginUrl, string formulario, string idAmbiente, string uidGrupoEconomico, string nomeEmpresa, string grupoEconomico, string idGpecon, string usuarioAutenticacao, string supportMode)
        {
            using (profiler.Step("Loading"))
            {
                if (BaseHelpers.GetShellMode() == "DEV" || BaseHelpers.GetShellMode() == "SETUP" || BaseHelpers.GetLoginMode() == "POSUX")
                {
                    this.Session["loginUrl"] = null;
                    this.Session["transaction"] = null;
                    this.Session["loginInfo"] = null;
                }
                else
                {
                    bool hasParameters = !string.IsNullOrEmpty(uidEmpresa) && !string.IsNullOrEmpty(uidUsuario) && !string.IsNullOrEmpty(uidAplicacao) && !string.IsNullOrEmpty(idAmbiente) && !string.IsNullOrEmpty(uidGrupoEconomico) &&
                                         !string.IsNullOrEmpty(nomeEmpresa) && !string.IsNullOrEmpty(grupoEconomico) && !string.IsNullOrEmpty(idGpecon);

                    LoginInfo loginInfo = this.Session["loginInfo"] as LoginInfo;
                    LoginInfo loginInfoUser = this.Session["loginInfo_" + uidUsuario + "_" + idAmbiente] as LoginInfo;

                    if (loginInfo != null)
                    {
                        if (hasParameters && (loginInfo.UidUsuario != Guid.Parse(uidUsuario) || loginInfo.IdTcsAmbienteDefault != int.Parse(idAmbiente)))
                        {
                            loginInfo = loginInfoUser;
                            this.Session["loginInfo"] = loginInfo;
                            if (!(hasParameters && loginInfo == null))
                            {
                                return Redirect("~/");
                            }
                        }
                        else if (hasParameters)
                        { return Redirect("~/"); }
                    }
                    else if (loginInfo == null && loginInfoUser != null)
                    {
                        loginInfo = loginInfoUser;
                        this.Session["loginInfo"] = loginInfo;
                        return Redirect("~/");
                    }

                    if (hasParameters)
                    {
                        return RedirectToAction("Authentication", "LIA",
                            new
                            {
                                uidEmpresa = uidEmpresa,
                                //uidGrupoAcesso = uidGrupoAcesso,
                                uidUsuario = uidUsuario,
                                uidAplicacao = uidAplicacao,
                                loginUrl = loginUrl,
                                formulario = formulario,
                                idAmbiente = idAmbiente,
                                uidGrupoEconomico = uidGrupoEconomico,
                                nomeEmpresa = nomeEmpresa,
                                grupoEconomico = grupoEconomico,
                                idGpecon = idGpecon,
                                usuarioAutenticacao = usuarioAutenticacao,
                                supportMode = supportMode
                            }
                        );
                    }
                    //else if (!User.Identity.IsAuthenticated || this.Session["tokenId"] == null)
                    else if (!User.Identity.IsAuthenticated || loginInfo == null)
                    {
                        return RedirectToAction("Unauthorized", "LIA");
                    }
                    else if (string.IsNullOrEmpty(formulario) == false)
                    {
                        //return Redirect("~/#" + HttpUtility.HtmlDecode(formulario));
                        return Redirect("~/");
                    }
                }
            }


            return View();
        }

        [NoCache]
        [GET("Logoff")]
        public ActionResult Logoff()
        {
            var _shellMode = BaseHelpers.GetShellMode();
            if (_shellMode == "DEV" || _shellMode == "SETUP")
            {
                return RedirectToAction("Loading");
            }

            var _loginUrl = (this.Session["loginUrl"] == null ? ConfigurationManager.AppSettings.GetValue<string>("Portal", "http://localhost:8172/") : this.Session["loginUrl"].ToString());

            // limpa a sessao
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();

            // Logoff
            FormsAuthentication.SignOut();

            // Redireciona Portal
            return this.Redirect(_loginUrl);
        }

        [NoCache]
        [GET("LogoffForPasswordChange")]
        public ActionResult LogoffForPasswordChange()
        {
            var loginInfo = this.Session["loginInfo"] as LoginInfo;
            var loginUrl = this.Session["loginUrl"];
            var transaction = this.Session["transaction"];
            var expiracao = this.Session["Expiracao"];

            FormsAuthentication.SignOut();

            if (loginInfo != null)
            {
                this.Session["loginInfo"] = loginInfo;
                this.Session["loginInfo_" + loginInfo.UidUsuario + "_" + loginInfo.IdTcsAmbienteDefault] = loginInfo;
            }

            if (loginUrl != null)
                this.Session["loginUrl"] = loginUrl;

            if (transaction != null)
                this.Session["transaction"] = transaction;

            this.Session["Expiracao"] = (expiracao ?? "true");
            this.Session["PasswordChangeOnly"] = true;

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [NoCache]
        [GET("ChangeEnvironment")]
        public ActionResult ChangeEnvironment()
        {
            var _loginUrl = (this.Session["loginUrl"] == null ? ConfigurationManager.AppSettings.GetValue<string>("Portal", "http://localhost:8172/") : this.Session["loginUrl"].ToString()) + "?showEnvironments=True";

            // Redireciona Portal
            return this.Redirect(_loginUrl);
        }

        [NoCache]
        [POST("UpdateExpiration")]
        public void UpdateExpiration()
        {
            this.Session["Expiracao"] = false;
            this.Session["PasswordChangeOnly"] = false;
        }

        [NoCache]
        [GET("Unauthorized")]
        public ActionResult Unauthorized()
        {
            //if (!User.Identity.IsAuthenticated || this.Session["tokenId"] == null)
            //{
            //    Response.Write("logado: " + User.Identity.IsAuthenticated.ToString());
            //    Response.Write("<BR>");
            //    Response.Write("sessao: " + (this.Session["tokenId"] == null).ToString());
            //}

            this.ViewData["_loginUrl"] = (this.Session["loginUrl"] == null ? ConfigurationManager.AppSettings.GetValue<string>("Portal", "http://localhost:8172/") : this.Session["loginUrl"].ToString());

            if (!string.IsNullOrEmpty(Request.Url.Query))
            {
                this.ViewData["_loginUrl"] = string.Concat(this.ViewData["_loginUrl"], Request.Url.Query);
            }

            return View();
        }

        [NoCache]
        [GET("/PageOpen")]
        public ActionResult PageOpen()
        {
            return View();
        }

        [NoCache]
        [GET("Reauthenticate")]
        public ActionResult Reauthenticate()
        {
            string[] authenticationInfo = this.Session["ReauthenticationInfo"].ToString().Split(new string[] { "||" }, StringSplitOptions.None);

            if (authenticationInfo != null)
            {
                string formulario = authenticationInfo[0];
                LoginInfo loginInfo = this.Session["loginInfo_" + authenticationInfo[1] + "_" + authenticationInfo[2]] as LoginInfo;

                if (loginInfo != null)
                {
                    this.Session["loginInfo_" + loginInfo.UidUsuario + "_" + loginInfo.IdTcsAmbienteDefault] = null;
                    string[] info = loginInfo.Info.Split(new string[] { "||" }, StringSplitOptions.None);
                    this.Session["loginInfo"] = null;


                    return RedirectToAction("Authentication", "LIA", new
                    {
                        uidEmpresa = info[0],
                        uidUsuario = info[1],
                        uidAplicacao = info[2],
                        loginUrl = info[3],
                        formulario = formulario,
                        idAmbiente = info[5],
                        uidGrupoEconomico = info[6],
                        nomeEmpresa = info[7],
                        grupoEconomico = info[8],
                        idGpecon = info[9],
                        usuarioAutenticacao = info[10],
                        supportMode = info[11]
                    });
                }
            }
            return Redirect("~/");
        }

        [NoCache]
        [POST("UpdateReauthenticationInfo")]
        public void UpdateTransactionInfo(string info)
        {
            this.Session["ReauthenticationInfo"] = info;
        }

        [NoCache]
        [GET("ForgotPassword")]
        public ActionResult ForgotPassword()
        {
            this.ViewData["_loginUrl"] = (this.Session["loginUrl"] == null ? ConfigurationManager.AppSettings.GetValue<string>("Portal", "http://localhost:8172/") : this.Session["loginUrl"].ToString());
            return View();
        }

        [NoCache]
        [POST("SendPasswordResetLink")]
        public ActionResult SendPasswordResetLink(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return Json(new { success = false, message = "Informe o usuário." });

            var _serviceBus = ConfigurationManager.AppSettings.GetValue("ServiceBus", "http://localhost:1710");
            var client = new RestClient(_serviceBus);
            var request = new RestRequest("LinxFrameworkAutorizacao/SendPasswordResetLink");
            request.AddParameter("userName", userName, ParameterType.QueryString);

            var callbackUrl = string.Concat(Request.Url.GetLeftPart(UriPartial.Authority), Url.Content("~/"), "ResetPassword");
            request.AddParameter("callbackUrl", callbackUrl, ParameterType.QueryString);

            var response = client.ExecuteAsGet(request, "GET");

            if (response.ErrorException != null)
                return Json(new { success = false, message = response.ErrorException.Message });

            if (response.StatusCode == HttpStatusCode.OK)
                return Json(new { success = true, message = "Se o usuário estiver cadastrado, você receberá um e-mail com o link para redefinir a senha." });

            return Json(new { success = false, message = ExtractError(response.Content) });
        }

        [NoCache]
        [GET("ResetPassword")]
        public ActionResult ResetPassword(string token)
        {
            this.ViewData["_loginUrl"] = (this.Session["loginUrl"] == null ? ConfigurationManager.AppSettings.GetValue<string>("Portal", "http://localhost:8172/") : this.Session["loginUrl"].ToString());

            bool valid = false;

            if (!string.IsNullOrEmpty(token))
            {
                var _serviceBus = ConfigurationManager.AppSettings.GetValue("ServiceBus", "http://localhost:1710");
                var client = new RestClient(_serviceBus);
                var request = new RestRequest("LinxFrameworkAutorizacao/ValidatePasswordResetToken");
                request.AddParameter("token", token, ParameterType.QueryString);

                var response = client.ExecuteAsGet(request, "GET");
                valid = response.StatusCode == HttpStatusCode.OK && response.Content.Trim().Equals("true", StringComparison.OrdinalIgnoreCase);
            }

            ViewBag.Token = token;
            ViewBag.TokenValid = valid;

            return View();
        }

        [NoCache]
        [POST("ResetPassword")]
        public ActionResult ResetPasswordSubmit(string token, string newPassword)
        {
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(newPassword))
                return Json(new { success = false, message = "Dados inválidos." });

            var _serviceBus = ConfigurationManager.AppSettings.GetValue("ServiceBus", "http://localhost:1710");
            var client = new RestClient(_serviceBus);
            var request = new RestRequest("LinxFrameworkAutorizacao/ResetPasswordWithToken");
            request.AddParameter("token", token, ParameterType.QueryString);
            request.AddParameter("newPassword", newPassword, ParameterType.QueryString);

            var response = client.ExecuteAsGet(request, "GET");

            if (response.ErrorException != null)
                return Json(new { success = false, message = response.ErrorException.Message });

            if (response.StatusCode == HttpStatusCode.OK)
                return Json(new { success = true, message = "Senha redefinida com sucesso." });

            return Json(new { success = false, message = ExtractError(response.Content) });
        }

        private LoginInfo AuthenticateUser(string uidEmpresa, string uidUsuario, string uidAplicacao, string idAmbiente, string usuarioAutenticacao, out string retorno)
        {
            retorno = string.Empty;
            LoginInfo loginInfo = new LoginInfo();

            var _serviceBus = System.Configuration.ConfigurationManager.AppSettings.GetValue("ServiceBus", "http://localhost:1710");

            // busca o token na api de autenticacao
            var client = new RestClient(_serviceBus);

            var request = new RestRequest("LinxFrameworkAutorizacao/authenticateUser");
            request.AddParameter("authenticatedUser", usuarioAutenticacao ?? User.Identity.Name, ParameterType.QueryString);
            request.AddParameter("applicationId", uidAplicacao, ParameterType.QueryString);
            request.AddParameter("companyId", uidEmpresa, ParameterType.QueryString);
            request.AddParameter("accessGroupId", Guid.Empty, ParameterType.QueryString);
            request.AddParameter("environmentId", idAmbiente, ParameterType.QueryString);

            var response = client.ExecuteAsGet(request, "GET");
            // erro na requisicao
            if (response.ErrorException != null)
            {
                retorno = response.ErrorException.Message;
            }
            else if (response.StatusCode == HttpStatusCode.OK)
            {
                loginInfo = JsonConvert.DeserializeObject<LoginInfo>(response.Content);

                if (loginInfo == null)
                    retorno = "Mensagem inv�lida!";
                else
                {
                    this.Session["Expiracao"] = (!loginInfo.AutenticacaoWindows && DateTime.Now >= loginInfo.DataExpiracaoSenha);
                }

                loginInfo.UsuarioAutenticacao = usuarioAutenticacao;
            }
            else
            {
                if (response.Content.Contains("Linx.Framework.BV.LicenseException"))
                {
                    retorno = string.Concat("<b>Falha na Valida��o do Controle de Licen�as.<BR><BR>", ExtractError(response.Content), "</b>");
                }
                else
                    retorno = string.Concat("Retorno inv�lido!<BR>", response.StatusCode, " : ", ExtractError(response.Content));
            }
            return loginInfo;
        }

        private string ExtractError(string content)
        {
            var responseError = content.Replace("\"", string.Empty);
            if (responseError.IndexOf("ExceptionMessage:") > 0)
            {
                var indexI = responseError.IndexOf("ExceptionMessage:") + 17;
                var indexF = responseError.IndexOf("ExceptionType");
                responseError = responseError.Substring(indexI, indexF - indexI - 1);
            }
            return responseError;
        }
    }
}

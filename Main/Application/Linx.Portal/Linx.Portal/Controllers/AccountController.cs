using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
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
                            return RedirectToAction("Index", "Home", new RouteValueDictionary { { "formulario", HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["formulario"] }, { "supportMode", HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["supportMode"] }, { "showEnvironments", model.ShowEnvironments } });
                    }
                }
            }
            catch (Exception oException)
            {
                ModelState.AddModelError("", oException.Message);
            }
            return View();
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
                throw new Exception(result.StatusDescription);

            string content = result.Content != null ? result.Content.Replace("\"", string.Empty) : string.Empty;
            string[] resultLines = crypto.Decrypt(HttpUtility.UrlDecode(content)).Split(new string[] { "||" }, StringSplitOptions.None);

            if (crypto.Decrypt(resultLines[0]) == "0")
            {
                string errorMessage = resultLines.Length > 1 ? crypto.Decrypt(resultLines[1]) : ErrorConstants._UserBadNameOrPassword.Message;

                // Guarantee lockout message on the login screen when Membership IsLockedOut = true.
                if (IsMembershipUserLockedOut(user))
                    errorMessage = String.Format("{0} - {1}", ErrorConstants._UserLockedOut.Code, ErrorConstants._UserLockedOut.Message);

                throw new Exception(errorMessage);
            }
            else if (crypto.Decrypt(resultLines[0]) == "1")
            {
                FormsAuthentication.SetAuthCookie(user, rememberMe);
                logged = true;
            }

            return logged;
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

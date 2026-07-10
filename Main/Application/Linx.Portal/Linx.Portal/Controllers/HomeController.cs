using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Linx.Portal.Models;
using Linx.Tools;
using Newtonsoft.Json;
using RestSharp;
using System.Web.Security;

namespace Linx.Portal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string parameter)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", new RouteValueDictionary { { "formulario", Request["formulario"] }, { "supportMode", Request["supportMode"] } });
            }

            try
            {
                Linx.Security.Cryptography crypto = new Linx.Security.Cryptography();
                string supportMode = Request.QueryString["supportMode"];
                bool isSupportMode = !supportMode.IsNullOrEmpty();
                bool showEnvironments = Convert.ToBoolean(Request.QueryString["showEnvironments"]);

                var client = new RestClient(Utils.GetServiceUrl());
                RestRequest request = new RestRequest("LinxFrameworkUsuarioAutorizacao/PortalUserAccess", Method.POST);
                request.RequestFormat = DataFormat.Json;

                //para teste
                //request.AddBody(new { NomeAutenticacao = User.Identity.Name, AcessoLocal = false, Parametros = isSupportMode ? supportMode : crypto.Encrypt("") });

                //para producao
                request.AddBody(new { NomeAutenticacao = User.Identity.Name, AcessoLocal = Request.IsLocal, Parametros = isSupportMode ? supportMode : crypto.Encrypt("") });

                var result = client.Post(request);

                if (result.ErrorException != null)
                    throw new Exception(result.ErrorException.Message);

                if (result.StatusCode != System.Net.HttpStatusCode.OK)
                    throw new Exception(ErrorMessage(result.Content, result.StatusDescription));

                var users = JsonConvert.DeserializeObject<LoggedUser[]>(result.Content);
                LoggedUser usr = users.FirstOrDefault();

                if (!showEnvironments)
                {
                    LoggedUser access = users.Where(i => i.IndicaAcessoPadrao).FirstOrDefault();
                    if (!access.IsNullOrEmpty())
                        usr = access;
                    else
                        showEnvironments = true;
                }

                ViewBag.Message = "Bem vindo, ".Translate() + (usr.IsNullOrEmpty() ? User.Identity.Name : isSupportMode ? usr.UsuarioSuporte : usr.NomeUsuario);
                string formulario = Request.QueryString["formulario"];
                ViewBag.FormId = formulario.IsNull() ? "" : formulario;
                ViewBag.IsSupportMode = isSupportMode;
                ViewBag.UsuarioAutenticacao = (isSupportMode ? usr.NomeAutenticacao : User.Identity.Name);

                if (parameter == null)
                {
                    TempData["SortColumn"] = "DescricaoAmbiente";
                    TempData["SortDirection"] = "ASC";
                    ViewBag.SortColumn = "DescricaoAmbiente";
                    ViewBag.SortDirection = "ASC";
                }
                else
                {
                    var sortColumn = (string)TempData["SortColumn"];
                    var sortDirection = (string)TempData["SortDirection"];

                    TempData["SortColumn"] = parameter;
                    ViewBag.SortColumn = parameter;

                    var sort = (sortColumn == parameter ? (sortDirection == "ASC" ? "DESC" : "ASC") : "ASC");
                    ViewBag.SortDirection = sort;
                    TempData["SortDirection"] = sort;
                }


                if ((!showEnvironments || users.Length == 1) && !isSupportMode)
                {
                    this.Redirect(usr.Url, usr.UidEmpresa, usr.UidGrupoEconomico, usr.UidUsuario, usr.UidAplicacao, usr.IdTcsAmbiente, formulario, usr.NomeEmpresa, usr.GrupoEconomico, usr.IdLinxGpecon, User.Identity.Name, isSupportMode, usr.UrlWorkArea);
                }
                else
                    ViewData.Add("users", users);

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }

        public ActionResult LogOut(string formulario)
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account", new RouteValueDictionary { { "formulario", formulario.IsNull() ? "" : formulario } });
        }

        public void Redirect(string url, Guid uidEmpresa, Guid uidGrupoEconomico, Guid uidUsuario, Guid uidAplicacao, int idAmbiente, string formulario, string nomeEmpresa, string grupoEconomico, int idLinxGpecon, string usuarioAutenticacao, bool supportMode, string urlWorkArea)
        {
            string loginUrl = Utils.GetPortalUrl();
            url = string.Format("{0}?uidEmpresa={1}&uidUsuario={2}&uidAplicacao={3}&loginUrl={4}&formulario={5}&idAmbiente={6}&uidGrupoEconomico={7}&nomeEmpresa={8}&grupoEconomico={9}&idGpecon={10}&usuarioAutenticacao={11}&supportMode={12}&urlWorkArea={13}", url, uidEmpresa, uidUsuario, uidAplicacao, loginUrl, formulario.IsNull() ? "" : formulario, idAmbiente, uidGrupoEconomico, HttpUtility.UrlEncode(nomeEmpresa), HttpUtility.UrlEncode(grupoEconomico), idLinxGpecon, usuarioAutenticacao, supportMode, urlWorkArea);
            Response.Redirect(url);
        }

        private string ErrorMessage(string content, string statusDescription)
        {
            return Linx.Tools.WebClientHelper.GetResponseErrorMessage(content) ?? statusDescription;
        }

    }
}

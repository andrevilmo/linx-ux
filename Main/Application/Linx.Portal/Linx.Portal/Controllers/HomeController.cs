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
                    return this.Redirect(usr.Url, usr.UidEmpresa, usr.UidGrupoEconomico, usr.UidUsuario, usr.UidAplicacao, usr.IdTcsAmbiente, formulario, usr.NomeEmpresa, usr.GrupoEconomico, usr.IdLinxGpecon, User.Identity.Name, isSupportMode, usr.UrlWorkArea);
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
            Session.Remove(PortalMfaClient.SessionPending);
            Session.Remove(PortalMfaClient.SessionTicket);
            Session.Remove(PortalMfaClient.SessionVerified);
            return RedirectToAction("Login", "Account", new RouteValueDictionary { { "formulario", formulario.IsNull() ? "" : formulario } });
        }

        public ActionResult Redirect(string url, Guid uidEmpresa, Guid uidGrupoEconomico, Guid uidUsuario, Guid uidAplicacao, int idAmbiente, string formulario, string nomeEmpresa, string grupoEconomico, int idLinxGpecon, string usuarioAutenticacao, bool supportMode, string urlWorkArea)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            MfaPendingRedirect pending = new MfaPendingRedirect
            {
                Url = url,
                UidEmpresa = uidEmpresa,
                UidGrupoEconomico = uidGrupoEconomico,
                UidUsuario = uidUsuario,
                UidAplicacao = uidAplicacao,
                IdAmbiente = idAmbiente,
                Formulario = formulario.IsNull() ? "" : formulario,
                NomeEmpresa = nomeEmpresa,
                GrupoEconomico = grupoEconomico,
                IdLinxGpecon = idLinxGpecon,
                UsuarioAutenticacao = usuarioAutenticacao,
                SupportMode = supportMode,
                UrlWorkArea = urlWorkArea
            };

            string verifiedKey = PortalMfaClient.VerifiedKey(uidUsuario, idLinxGpecon);
            string existingTicket = Session[PortalMfaClient.SessionTicket] as string;
            if (Session[PortalMfaClient.SessionVerified] as string == verifiedKey && !string.IsNullOrWhiteSpace(existingTicket))
            {
                pending.Ticket = existingTicket;
                return RedirectToApplication(pending);
            }

            try
            {
                PortalMfaStatus status = PortalMfaClient.GetStatus(uidUsuario, idLinxGpecon);
                if (status == null)
                    throw new Exception("Não foi possível consultar o status MFA.");

                if (!status.RequiresMfa)
                {
                    PortalMfaValidate skip = PortalMfaClient.IssueSkipTicket(uidUsuario, idLinxGpecon, status.SkipReason);
                    if (skip == null || !skip.Success)
                        throw new Exception(skip != null ? skip.Message : "Não foi possível emitir o ticket MFA.");
                    pending.Ticket = skip.Ticket;
                    Session[PortalMfaClient.SessionTicket] = skip.Ticket;
                    Session[PortalMfaClient.SessionVerified] = verifiedKey;
                    return RedirectToApplication(pending);
                }

                Session[PortalMfaClient.SessionPending] = pending;
                return RedirectToAction("Challenge", "Mfa");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return RedirectToAction("Index", "Home", new { showEnvironments = true });
            }
        }

        private ActionResult RedirectToApplication(MfaPendingRedirect pending)
        {
            string loginUrl = Utils.GetPortalUrl();
            return Redirect(PortalMfaClient.BuildApplicationUrl(pending, loginUrl));
        }

        private string ErrorMessage(string content, string statusDescription)
        {
            return Linx.Tools.WebClientHelper.GetResponseErrorMessage(content) ?? statusDescription;
        }

    }
}

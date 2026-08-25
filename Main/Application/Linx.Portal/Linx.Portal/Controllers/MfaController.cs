using System;
using System.Web.Mvc;
using Linx.Portal.Models;

namespace Linx.Portal.Controllers
{
    public class MfaController : Controller
    {
        [HttpGet]
        public ActionResult Challenge()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            MfaPendingRedirect pending = Session[PortalMfaClient.SessionPending] as MfaPendingRedirect;
            if (pending == null)
                return RedirectToAction("Index", "Home");

            try
            {
                PortalMfaStatus status = PortalMfaClient.GetStatus(pending.UidUsuario, pending.IdLinxGpecon);
                if (status == null)
                {
                    ViewBag.Error = "Não foi possível consultar o status MFA.";
                    return View();
                }

                if (!status.RequiresMfa)
                {
                    PortalMfaValidate skip = PortalMfaClient.IssueSkipTicket(pending.UidUsuario, pending.IdLinxGpecon, status.SkipReason);
                    return FinishAndRedirect(pending, skip);
                }

                if (status.MfaLocked)
                {
                    ViewBag.Locked = true;
                    ViewBag.Error = "MFA bloqueado por excesso de tentativas. Tente novamente em alguns minutos.";
                    ViewBag.AccountLabel = status.NomeEmpresa + " + " + status.NomeAutenticacao;
                    ViewBag.Enrolled = status.Enrolled;
                    return View();
                }

                ViewBag.Enrolled = status.Enrolled;
                ViewBag.AccountLabel = string.IsNullOrWhiteSpace(status.NomeEmpresa)
                    ? status.NomeAutenticacao
                    : status.NomeEmpresa + " + " + status.NomeAutenticacao;
                ViewBag.NomeEmpresa = status.NomeEmpresa ?? pending.NomeEmpresa;

                if (!status.Enrolled)
                {
                    PortalMfaEnroll enroll = PortalMfaClient.BeginEnroll(pending.UidUsuario, pending.IdLinxGpecon);
                    if (enroll == null || !enroll.Success)
                    {
                        ViewBag.Error = enroll != null ? enroll.Message : "Não foi possível iniciar o cadastro MFA.";
                        return View();
                    }
                    ViewBag.QrCodePngBase64 = enroll.QrCodePngBase64;
                    if (!string.IsNullOrWhiteSpace(enroll.AccountLabel))
                        ViewBag.AccountLabel = enroll.AccountLabel;
                }

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public ActionResult Verify(string code)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            MfaPendingRedirect pending = Session[PortalMfaClient.SessionPending] as MfaPendingRedirect;
            if (pending == null)
                return RedirectToAction("Index", "Home");

            try
            {
                PortalMfaStatus status = PortalMfaClient.GetStatus(pending.UidUsuario, pending.IdLinxGpecon);
                if (status == null)
                {
                    ViewBag.Error = "Não foi possível consultar o status MFA.";
                    return View("Challenge");
                }

                PortalMfaValidate result;
                if (status.Enrolled)
                    result = PortalMfaClient.ValidateCode(pending.UidUsuario, pending.IdLinxGpecon, code);
                else
                    result = PortalMfaClient.ConfirmEnroll(pending.UidUsuario, pending.IdLinxGpecon, code);

                if (result == null || !result.Success)
                {
                    ViewBag.Error = result != null ? result.Message : "Código MFA inválido.";
                    ViewBag.Locked = result != null && result.MfaLocked;
                    ViewBag.Enrolled = status.Enrolled;
                    ViewBag.AccountLabel = ViewBag.AccountLabel ?? (status.NomeEmpresa + " + " + status.NomeAutenticacao);
                    if (!status.Enrolled && ViewBag.QrCodePngBase64 == null)
                    {
                        try
                        {
                            PortalMfaEnroll enroll = PortalMfaClient.BeginEnroll(pending.UidUsuario, pending.IdLinxGpecon);
                            if (enroll != null && enroll.Success)
                                ViewBag.QrCodePngBase64 = enroll.QrCodePngBase64;
                        }
                        catch
                        {
                        }
                    }
                    return View("Challenge");
                }

                return FinishAndRedirect(pending, result);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Challenge");
            }
        }

        private ActionResult FinishAndRedirect(MfaPendingRedirect pending, PortalMfaValidate result)
        {
            if (result == null || !result.Success || string.IsNullOrWhiteSpace(result.Ticket))
            {
                ViewBag.Error = result != null ? result.Message : "Ticket MFA ausente.";
                return View("Challenge");
            }

            pending.Ticket = result.Ticket;
            Session[PortalMfaClient.SessionPending] = pending;
            Session[PortalMfaClient.SessionTicket] = result.Ticket;
            Session[PortalMfaClient.SessionVerified] = PortalMfaClient.VerifiedKey(pending.UidUsuario, pending.IdLinxGpecon);
            string loginUrl = Utils.GetPortalUrl();
            return Redirect(PortalMfaClient.BuildApplicationUrl(pending, loginUrl));
        }
    }
}

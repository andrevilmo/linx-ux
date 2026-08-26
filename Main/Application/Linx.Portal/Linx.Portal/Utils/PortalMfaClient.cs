using System;
using System.Web;
using Linx.Portal.Models;
using Newtonsoft.Json;
using RestSharp;

namespace Linx.Portal
{
    internal static class PortalMfaClient
    {
        public const string SessionPending = "MfaPendingRedirect";
        public const string SessionVerified = "MfaVerifiedKey";
        public const string SessionTicket = "MfaTicket";
        public const string OriginUx = "UX";

        public static string VerifiedKey(Guid uidUsuario, int idGpecon)
        {
            return string.Format("{0}|{1}", uidUsuario, idGpecon);
        }

        public static string BuildApplicationUrl(MfaPendingRedirect pending, string loginUrl)
        {
            return string.Format(
                "{0}?uidEmpresa={1}&uidUsuario={2}&uidAplicacao={3}&loginUrl={4}&formulario={5}&idAmbiente={6}&uidGrupoEconomico={7}&nomeEmpresa={8}&grupoEconomico={9}&idGpecon={10}&usuarioAutenticacao={11}&supportMode={12}&urlWorkArea={13}&mfaTicket={14}",
                pending.Url,
                pending.UidEmpresa,
                pending.UidUsuario,
                pending.UidAplicacao,
                loginUrl,
                pending.Formulario ?? "",
                pending.IdAmbiente,
                pending.UidGrupoEconomico,
                HttpUtility.UrlEncode(pending.NomeEmpresa),
                HttpUtility.UrlEncode(pending.GrupoEconomico),
                pending.IdLinxGpecon,
                HttpUtility.UrlEncode(pending.UsuarioAutenticacao),
                pending.SupportMode,
                HttpUtility.UrlEncode(pending.UrlWorkArea),
                HttpUtility.UrlEncode(pending.Ticket ?? ""));
        }

        public static PortalMfaStatus GetStatus(Guid uidUsuario, int idGpecon)
        {
            var request = NewRequest("GetMfaStatus");
            request.AddParameter("tableOrigin", OriginUx);
            request.AddParameter("idGpecon", idGpecon);
            request.AddParameter("uidUsuario", uidUsuario);
            return Execute<PortalMfaStatus>(request);
        }

        public static PortalMfaEnroll BeginEnroll(Guid uidUsuario, int idGpecon)
        {
            var request = NewRequest("BeginMfaEnrollment");
            request.AddParameter("tableOrigin", OriginUx);
            request.AddParameter("idGpecon", idGpecon);
            request.AddParameter("uidUsuario", uidUsuario);
            return Execute<PortalMfaEnroll>(request);
        }

        public static PortalMfaValidate ConfirmEnroll(Guid uidUsuario, int idGpecon, string code)
        {
            var request = NewRequest("ConfirmMfaEnrollment");
            request.AddParameter("tableOrigin", OriginUx);
            request.AddParameter("idGpecon", idGpecon);
            request.AddParameter("uidUsuario", uidUsuario);
            request.AddParameter("code", code ?? "");
            return Execute<PortalMfaValidate>(request);
        }

        public static PortalMfaValidate ValidateCode(Guid uidUsuario, int idGpecon, string code)
        {
            var request = NewRequest("ValidateMfaCode");
            request.AddParameter("tableOrigin", OriginUx);
            request.AddParameter("idGpecon", idGpecon);
            request.AddParameter("uidUsuario", uidUsuario);
            request.AddParameter("code", code ?? "");
            request.AddParameter("canal", "Portal");
            return Execute<PortalMfaValidate>(request);
        }

        public static PortalMfaValidate IssueSkipTicket(Guid uidUsuario, int idGpecon, string reason)
        {
            var request = NewRequest("IssueMfaSkipTicket");
            request.AddParameter("tableOrigin", OriginUx);
            request.AddParameter("idGpecon", idGpecon);
            request.AddParameter("uidUsuario", uidUsuario);
            request.AddParameter("reason", reason ?? "SKIP");
            return Execute<PortalMfaValidate>(request);
        }

        public static PortalMfaValidate ValidateTicket(string ticket)
        {
            var request = NewRequest("ValidateMfaTicket");
            request.AddParameter("ticket", ticket ?? "");
            return Execute<PortalMfaValidate>(request);
        }

        private static RestRequest NewRequest(string action)
        {
            return new RestRequest("LinxFrameworkAutorizacao/" + action);
        }

        private static T Execute<T>(RestRequest request) where T : class
        {
            var client = new RestClient(Utils.GetServiceUrl());
            var result = client.ExecuteAsGet(request, "GET");
            if (result.ErrorException != null)
                throw new Exception(result.ErrorException.Message);
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                string detail = result.Content;
                if (!string.IsNullOrWhiteSpace(detail) && detail.Length > 400)
                    detail = detail.Substring(0, 400) + "...";
                throw new Exception(string.Format("{0}: {1}", result.StatusDescription, detail));
            }
            if (string.IsNullOrWhiteSpace(result.Content))
                return null;
            return JsonConvert.DeserializeObject<T>(result.Content);
        }
    }
}

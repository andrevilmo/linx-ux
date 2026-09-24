using System;
using RestSharp;

namespace Linx.Portal.Authentication
{
    /// <summary>
    /// Best-effort trail of the Portal SSO process into Service TCS_LOG_ACESSO_AUTH.
    /// Never throws; login must not fail because audit is down.
    /// </summary>
    internal static class PortalSsoAudit
    {
        public static void Info(string userName, string step, string detail = null)
        {
            Write(userName, step, detail, failed: false);
        }

        public static void Fail(string userName, string step, string detail = null)
        {
            Write(userName, step, detail, failed: true);
        }

        private static void Write(string userName, string step, string detail, bool failed)
        {
            try
            {
                var client = new RestClient(Utils.GetServiceUrl());
                var request = new RestRequest("LinxFrameworkAutorizacao/LogPortalSsoProcess");
                request.AddParameter("userName", userName ?? string.Empty);
                request.AddParameter("step", step ?? string.Empty);
                if (!string.IsNullOrWhiteSpace(detail))
                    request.AddParameter("detail", detail);
                request.AddParameter("failed", failed);
                request.AddHeader("X-Auth-Channel", "PortalSSO");
                client.ExecuteAsGet(request, "GET");
            }
            catch
            {
            }
        }
    }
}

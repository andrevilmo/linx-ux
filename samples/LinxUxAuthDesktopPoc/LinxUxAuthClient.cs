using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Linx.Security;
using Newtonsoft.Json;

namespace Linx.Ux.AuthDesktopPoc
{
    public sealed class LinxUxAuthClient : IDisposable
    {
        public const string OriginUx = "UX";

        private readonly HttpClient _http;
        private readonly Cryptography _crypto = new Cryptography();

        public LinxUxAuthClient(string serviceBaseUrl)
        {
            _http = new HttpClient { BaseAddress = new Uri(serviceBaseUrl.TrimEnd('/') + "/") };
            _http.Timeout = TimeSpan.FromSeconds(90);
            _http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _http.DefaultRequestHeaders.Add("X-Auth-Channel", "DesktopPoc");
        }

        public void Dispose() => _http.Dispose();

        public async Task<string> LoginComSenhaAsync(string usuario, string senha)
        {
            string inner = _crypto.Encrypt(usuario.Trim()) + "||" + _crypto.Encrypt(senha.Trim());
            string envelope = _crypto.Encrypt(inner);
            string url = "LinxFrameworkAutorizacao/AuthenticatePortal?authenticateParameters="
                         + Uri.EscapeDataString(envelope);
            string[] parts = await GetEncryptedPartsAsync(url).ConfigureAwait(false);
            if (DecryptPart(parts, 0) != "1")
                throw new InvalidOperationException(DecryptPart(parts, 1));
            return usuario.Trim();
        }

        public async Task<string> LoginAposSsoAsync(string nomeAutenticacao)
        {
            string url = "LinxFrameworkAutorizacao/AuthenticatePortalSso?userName="
                         + Uri.EscapeDataString(nomeAutenticacao);
            string[] parts = await GetEncryptedPartsAsync(url).ConfigureAwait(false);
            if (DecryptPart(parts, 0) != "1")
                throw new InvalidOperationException(DecryptPart(parts, 1));
            return parts.Length > 3 ? DecryptPart(parts, 3) : nomeAutenticacao;
        }

        public async Task<List<AmbienteAcesso>> ListarAmbientesAsync(string nomeAutenticacao, bool? acessoLocal = null)
        {
            if (acessoLocal.HasValue)
                return await PortalUserAccessAsync(nomeAutenticacao, acessoLocal.Value).ConfigureAwait(false);

            // Portal usa Request.IsLocal. No desktop, tenta produção (false) e depois dev (true).
            List<AmbienteAcesso> lista = await PortalUserAccessAsync(nomeAutenticacao, false).ConfigureAwait(false);
            if (lista.Count > 0)
                return lista;
            return await PortalUserAccessAsync(nomeAutenticacao, true).ConfigureAwait(false);
        }

        private async Task<List<AmbienteAcesso>> PortalUserAccessAsync(string nomeAutenticacao, bool acessoLocal)
        {
            // Igual ao Portal: Parametros = Encrypt("") (Decrypt("") estoura Substring no Service).
            var body = new
            {
                NomeAutenticacao = nomeAutenticacao,
                AcessoLocal = acessoLocal,
                Parametros = _crypto.Encrypt("")
            };
            using (var req = new HttpRequestMessage(HttpMethod.Post, "LinxFrameworkUsuarioAutorizacao/PortalUserAccess"))
            {
                req.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
                using (HttpResponseMessage resp = await _http.SendAsync(req).ConfigureAwait(false))
                {
                    string json = await EnsureOkAsync(resp).ConfigureAwait(false);
                    return JsonConvert.DeserializeObject<List<AmbienteAcesso>>(json) ?? new List<AmbienteAcesso>();
                }
            }
        }

        public Task<MfaStatusDto> GetMfaStatusAsync(Guid uidUsuario, int idGpecon)
        {
            return GetJsonAsync<MfaStatusDto>(
                "LinxFrameworkAutorizacao/GetMfaStatus"
                + "?tableOrigin=" + OriginUx
                + "&idGpecon=" + idGpecon
                + "&uidUsuario=" + uidUsuario);
        }

        public Task<MfaEnrollDto> BeginEnrollAsync(Guid uidUsuario, int idGpecon)
        {
            return GetJsonAsync<MfaEnrollDto>(
                "LinxFrameworkAutorizacao/BeginMfaEnrollment"
                + "?tableOrigin=" + OriginUx
                + "&idGpecon=" + idGpecon
                + "&uidUsuario=" + uidUsuario);
        }

        public Task<MfaTicketDto> ConfirmEnrollAsync(Guid uidUsuario, int idGpecon, string code6)
        {
            return GetJsonAsync<MfaTicketDto>(
                "LinxFrameworkAutorizacao/ConfirmMfaEnrollment"
                + "?tableOrigin=" + OriginUx
                + "&idGpecon=" + idGpecon
                + "&uidUsuario=" + uidUsuario
                + "&code=" + Uri.EscapeDataString(code6 ?? ""));
        }

        public Task<MfaTicketDto> ValidateTotpAsync(Guid uidUsuario, int idGpecon, string code6)
        {
            return GetJsonAsync<MfaTicketDto>(
                "LinxFrameworkAutorizacao/ValidateMfaCode"
                + "?tableOrigin=" + OriginUx
                + "&idGpecon=" + idGpecon
                + "&uidUsuario=" + uidUsuario
                + "&code=" + Uri.EscapeDataString(code6 ?? "")
                + "&canal=" + Uri.EscapeDataString("Desktop"));
        }

        public Task<MfaTicketDto> IssueSkipTicketAsync(Guid uidUsuario, int idGpecon, string reason)
        {
            return GetJsonAsync<MfaTicketDto>(
                "LinxFrameworkAutorizacao/IssueMfaSkipTicket"
                + "?tableOrigin=" + OriginUx
                + "&idGpecon=" + idGpecon
                + "&uidUsuario=" + uidUsuario
                + "&reason=" + Uri.EscapeDataString(reason ?? "SKIP"));
        }

        public Task<MfaTicketDto> ValidateTicketAsync(string ticket)
        {
            return GetJsonAsync<MfaTicketDto>(
                "LinxFrameworkAutorizacao/ValidateMfaTicket?ticket=" + Uri.EscapeDataString(ticket ?? ""));
        }

        public Task<LoginInfoDto> AuthenticateUserAsync(AmbienteAcesso ambiente, string nomeAutenticacao)
        {
            return GetJsonAsync<LoginInfoDto>(
                "LinxFrameworkAutorizacao/AuthenticateUser"
                + "?authenticatedUser=" + Uri.EscapeDataString(nomeAutenticacao)
                + "&applicationId=" + ambiente.UidAplicacao
                + "&companyId=" + ambiente.UidEmpresa
                + "&accessGroupId=" + Guid.Empty
                + "&environmentId=" + ambiente.IdTcsAmbiente);
        }

        public async Task<MfaTicketDto> CompletarMfaAsync(
            AmbienteAcesso ambiente,
            Func<MfaEnrollDto, string> pedirCodigoComQr,
            Func<string> pedirCodigo)
        {
            MfaStatusDto status = await GetMfaStatusAsync(ambiente.UidUsuario, ambiente.IdLinxGpecon)
                .ConfigureAwait(false);
            if (status.MfaLocked)
                throw new InvalidOperationException("MFA bloqueado por excesso de tentativas (15 min).");

            if (!status.RequiresMfa)
                return await IssueSkipTicketAsync(ambiente.UidUsuario, ambiente.IdLinxGpecon, status.SkipReason)
                    .ConfigureAwait(false);

            if (!status.Enrolled)
            {
                MfaEnrollDto enroll = await BeginEnrollAsync(ambiente.UidUsuario, ambiente.IdLinxGpecon)
                    .ConfigureAwait(false);
                if (!enroll.Success)
                    throw new InvalidOperationException(enroll.Message ?? "Falha ao iniciar MFA.");
                string code = pedirCodigoComQr(enroll);
                return await ConfirmEnrollAsync(ambiente.UidUsuario, ambiente.IdLinxGpecon, code)
                    .ConfigureAwait(false);
            }

            return await ValidateTotpAsync(ambiente.UidUsuario, ambiente.IdLinxGpecon, pedirCodigo())
                .ConfigureAwait(false);
        }

        public async Task<LoginInfoDto> AbrirSessaoApplicationAsync(
            AmbienteAcesso ambiente, string nomeAutenticacao, string mfaTicket)
        {
            MfaTicketDto check = await ValidateTicketAsync(mfaTicket).ConfigureAwait(false);
            if (check == null || !check.Success)
                throw new InvalidOperationException(check != null ? check.Message : "Ticket MFA inválido.");
            return await AuthenticateUserAsync(ambiente, nomeAutenticacao).ConfigureAwait(false);
        }

        public static AmbienteAcesso EscolherAmbiente(IList<AmbienteAcesso> lista)
        {
            if (lista == null || lista.Count == 0)
                throw new InvalidOperationException("Usuário sem ambientes.");
            foreach (AmbienteAcesso a in lista)
            {
                if (a.IndicaAcessoPadrao)
                    return a;
            }
            if (lista.Count == 1)
                return lista[0];
            throw new InvalidOperationException(
                "Há vários ambientes sem INDICA_ACESSO_PADRAO. Passe --ambiente <IdTcsAmbiente>.");
        }

        private async Task<string[]> GetEncryptedPartsAsync(string relativeUrl)
        {
            using (HttpResponseMessage resp = await _http.GetAsync(relativeUrl).ConfigureAwait(false))
            {
                string raw = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (!resp.IsSuccessStatusCode)
                    throw new InvalidOperationException((int)resp.StatusCode + " " + Truncate(raw, 400));
                raw = raw.Trim().Trim('"').Replace("\\\"", "");
                string decoded = Uri.UnescapeDataString(raw.Replace("+", " "));
                string plain = _crypto.Decrypt(decoded);
                if (string.IsNullOrEmpty(plain))
                    throw new InvalidOperationException("Resposta de autenticação inválida.");
                return plain.Split(new[] { "||" }, StringSplitOptions.None);
            }
        }

        private string DecryptPart(string[] parts, int index)
        {
            if (parts == null || index >= parts.Length)
                return string.Empty;
            return _crypto.Decrypt(parts[index]);
        }

        private async Task<T> GetJsonAsync<T>(string relativeUrl)
        {
            using (HttpResponseMessage resp = await _http.GetAsync(relativeUrl).ConfigureAwait(false))
            {
                string json = await EnsureOkAsync(resp).ConfigureAwait(false);
                return JsonConvert.DeserializeObject<T>(json);
            }
        }

        private static async Task<string> EnsureOkAsync(HttpResponseMessage resp)
        {
            string body = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (!resp.IsSuccessStatusCode)
                throw new InvalidOperationException((int)resp.StatusCode + " " + Truncate(body, 400));
            return body;
        }

        private static string Truncate(string s, int n)
        {
            if (string.IsNullOrEmpty(s) || s.Length <= n)
                return s;
            return s.Substring(0, n) + "...";
        }
    }
}

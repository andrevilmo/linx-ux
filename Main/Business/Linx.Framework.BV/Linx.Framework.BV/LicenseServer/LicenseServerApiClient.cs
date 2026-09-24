using System;
using System.Net;
using Newtonsoft.Json;
using RestSharp;

namespace Linx.Framework.BV.LicenseServer
{
    /// <summary>
    /// Omni License Server HTTP client: Authentication + billing/Validate + Revoke.
    /// </summary>
    public sealed class LicenseServerApiClient
    {
        private static readonly object TokenSync = new object();
        private static string _cachedToken;
        private static DateTime _tokenExpiryUtc = DateTime.MinValue;

        private readonly LicenseServerSettings _settings;

        public LicenseServerApiClient(LicenseServerSettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException("settings");
            _settings = settings;
        }

        public LicenseValidationResult Validate(LicenseValidationRequest request)
        {
            return Send<LicenseValidationRequest, LicenseValidationResult>("api/v1/Licensing/billing/Validate", request);
        }

        public LicenseInfo Revoke(LicenseValidationRequest request)
        {
            return Send<LicenseValidationRequest, LicenseInfo>("api/v1/Licensing/Revoke", request);
        }

        public string GetToken(bool forceRefresh)
        {
            lock (TokenSync)
            {
                if (!forceRefresh && !string.IsNullOrEmpty(_cachedToken) && DateTime.UtcNow < _tokenExpiryUtc.AddMinutes(-5))
                    return _cachedToken;

                var client = new RestClient(_settings.BaseUrl);
                var restRequest = new RestRequest("api/v1/Authentication", Method.POST);
                AddJsonBody(restRequest, new { email = _settings.Email, password = _settings.Password });

                IRestResponse response;
                try
                {
                    response = client.Execute(restRequest);
                }
                catch (Exception ex)
                {
                    throw new LicenseException("Falha ao conectar ao License Server para autenticação. " + ex.Message);
                }

                if (response.ErrorException != null)
                    throw new LicenseException("Falha ao conectar ao License Server para autenticação. " + response.ErrorException.Message);

                if (response.StatusCode != HttpStatusCode.OK)
                    throw new LicenseException("Autenticação no License Server falhou com HTTP " + (int)response.StatusCode + "." + FormatBody(response.Content));

                AuthenticationResult result;
                try
                {
                    result = JsonConvert.DeserializeObject<AuthenticationResult>(response.Content);
                }
                catch (Exception ex)
                {
                    throw new LicenseException("Resposta de autenticação do License Server em formato inesperado. " + ex.Message);
                }

                if (result == null || result.Data == null || string.IsNullOrWhiteSpace(result.Data.ApiToken))
                    throw new LicenseException("Token não encontrado na resposta de autenticação do License Server.");

                var expiresInHours = result.Data.ExpiresInHours > 0 ? result.Data.ExpiresInHours : 24.0;
                _cachedToken = result.Data.ApiToken;
                _tokenExpiryUtc = DateTime.UtcNow.AddHours(expiresInHours);
                return _cachedToken;
            }
        }

        private TResponse Send<TRequest, TResponse>(string relativeUrl, TRequest payload)
        {
            var response = ExecuteAuthorized(relativeUrl, payload, false);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
                response = ExecuteAuthorized(relativeUrl, payload, true);

            if (response.ErrorException != null)
                throw new LicenseException("Falha ao chamar o License Server. " + response.ErrorException.Message);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new LicenseException("License Server retornou HTTP " + (int)response.StatusCode + "." + FormatBody(response.Content));

            if (string.IsNullOrWhiteSpace(response.Content))
                throw new LicenseException("License Server retornou resposta vazia para " + relativeUrl + ".");

            try
            {
                return JsonConvert.DeserializeObject<TResponse>(response.Content);
            }
            catch (Exception ex)
            {
                throw new LicenseException("Falha ao ler a resposta do License Server. " + ex.Message);
            }
        }

        private IRestResponse ExecuteAuthorized<TRequest>(string relativeUrl, TRequest payload, bool forceRefresh)
        {
            var token = GetToken(forceRefresh);
            var client = new RestClient(_settings.BaseUrl);
            var restRequest = new RestRequest(relativeUrl, Method.POST);
            restRequest.AddHeader("Authorization", "Bearer " + token);
            AddJsonBody(restRequest, payload);
            return client.Execute(restRequest);
        }

        internal static string SerializeBody(object payload)
        {
            return JsonConvert.SerializeObject(payload);
        }

        private static void AddJsonBody(RestRequest restRequest, object payload)
        {
            restRequest.AddParameter("application/json", SerializeBody(payload), ParameterType.RequestBody);
        }

        private static string FormatBody(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                return string.Empty;

            var trimmed = content.Trim();
            if (trimmed.Length > 400)
                trimmed = trimmed.Substring(0, 400);
            return " " + trimmed;
        }
    }
}

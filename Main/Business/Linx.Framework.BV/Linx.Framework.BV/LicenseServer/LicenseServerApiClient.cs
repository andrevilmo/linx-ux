using System;
using System.Net;
using System.Threading.Tasks;
using Linx.LicenseServer.Client;
using Linx.LicenseServer.Client.Configuration;
using Linx.LicenseServer.Client.DependencyInjection;
using Linx.LicenseServer.Client.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using DllRequests = Linx.LicenseServer.Client.Models.Requests;
using DllResponses = Linx.LicenseServer.Client.Models.Responses;

namespace Linx.Framework.BV.LicenseServer
{
    /// <summary>
    /// Sync adapter over <see cref="ILicenseServerClient"/> (Linx.LicenseServer.Client.dll).
    /// Keeps the existing LicenseControl / Bootstrap call sites unchanged.
    /// </summary>
    public sealed class LicenseServerApiClient : IDisposable
    {
        private static readonly object FactorySync = new object();
        private static ServiceProvider _sharedProvider;
        private static string _sharedProviderKey;

        private readonly LicenseServerSettings _settings;
        private readonly ILicenseServerClient _client;
        private readonly bool _ownsProvider;
        private readonly ServiceProvider _ownedProvider;

        static LicenseServerApiClient()
        {
            EnsureSecureTls();
        }

        public LicenseServerApiClient(LicenseServerSettings settings)
            : this(settings, false)
        {
        }

        private LicenseServerApiClient(LicenseServerSettings settings, bool isolatedProvider)
        {
            if (settings == null)
                throw new ArgumentNullException("settings");

            _settings = settings;
            EnsureSecureTls();

            if (isolatedProvider)
            {
                _ownedProvider = BuildProvider(settings);
                _ownsProvider = true;
                _client = _ownedProvider.GetRequiredService<ILicenseServerClient>();
            }
            else
            {
                _client = GetOrCreateSharedClient(settings);
            }
        }

        /// <summary>
        /// .NET 4.6.1 defaults to TLS 1.0. api-hml.linx.com.br requires TLS 1.2+.
        /// </summary>
        internal static void EnsureSecureTls()
        {
            try
            {
                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
            }
            catch (NotSupportedException)
            {
            }

            const SecurityProtocolType tls13 = (SecurityProtocolType)12288;
            try
            {
                ServicePointManager.SecurityProtocol |= tls13;
            }
            catch (NotSupportedException)
            {
            }
        }

        public LicenseValidationResult Validate(LicenseValidationRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            try
            {
                DllResponses.LicenseValidationResult dllResult = Run(
                    () => _client.ValidateAsync(ToDllValidationRequest(request)));
                return FromDll(dllResult);
            }
            catch (LicenseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw MapException(ex, "Falha ao validar a licença no License Server.");
            }
        }

        public LicenseInfo Revoke(LicenseValidationRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            try
            {
                DllResponses.LicenseInfo dllInfo = Run(
                    () => _client.RevokeAsync(ToDllRevocationRequest(request)));
                return FromDll(dllInfo);
            }
            catch (LicenseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw MapException(ex, "Falha ao revogar a licença no License Server.");
            }
        }

        /// <summary>
        /// Desbloqueio em confiança via DLL: POST BillingRuler/v2/desbloqueio/cnpj/{digits}.
        /// CNPJ-only (empty body), matching Linx.LicenseServer.Client.
        /// 200 Succeeded, 400 Denied, other Failed.
        /// </summary>
        public TrustUnblockResult UnblockByTrust(string cnpj, string usuario)
        {
            // usuario is retained for API compatibility with Bootstrap; the DLL does not send it.
            try
            {
                Run(() => _client.UnblockByTrustAsync(cnpj));
                return TrustUnblockResult.Create(TrustUnblockOutcome.Succeeded, null);
            }
            catch (ArgumentException ex)
            {
                return TrustUnblockResult.Create(TrustUnblockOutcome.Failed, ex.Message);
            }
            catch (LicenseServerApiException ex)
            {
                if (ex.StatusCode == HttpStatusCode.BadRequest)
                {
                    string denied = string.IsNullOrWhiteSpace(ex.ResponseBody)
                        ? "Desbloqueio em confiança recusado pelo servidor."
                        : ex.ResponseBody.Trim();
                    if (denied.Length > 400)
                        denied = denied.Substring(0, 400);
                    return TrustUnblockResult.Create(TrustUnblockOutcome.Denied, denied);
                }

                return TrustUnblockResult.Create(
                    TrustUnblockOutcome.Failed,
                    "License Server retornou HTTP " + (int)ex.StatusCode + "." + FormatBody(ex.ResponseBody));
            }
            catch (LicenseServerAuthenticationException ex)
            {
                return TrustUnblockResult.Create(
                    TrustUnblockOutcome.Failed,
                    FormatAuthenticationFailure(ex));
            }
            catch (LicenseException ex)
            {
                return TrustUnblockResult.Create(TrustUnblockOutcome.Failed, LicenseException.StripMarker(ex.Message));
            }
            catch (Exception ex)
            {
                return TrustUnblockResult.Create(
                    TrustUnblockOutcome.Failed,
                    "Falha ao chamar o License Server. " + Unwrap(ex).Message);
            }
        }

        /// <summary>
        /// Forces a fresh client/token provider. Used by live smoke tests.
        /// </summary>
        internal static void ResetTokenCacheForTests()
        {
            lock (FactorySync)
            {
                if (_sharedProvider != null)
                {
                    _sharedProvider.Dispose();
                    _sharedProvider = null;
                    _sharedProviderKey = null;
                }
            }
        }

        /// <summary>
        /// Live-test helper: triggers authentication through the DLL client.
        /// Returns a non-empty marker on success (token itself is owned by the DLL).
        /// </summary>
        public string GetToken(bool forceRefresh)
        {
            if (forceRefresh)
                ResetTokenCacheForTests();

            try
            {
                // Validate with configured key forces Authentication + billing/Validate.
                // For auth-only probing we use an isolated provider so wrong-password tests
                // do not poison the shared cache.
                using (var probe = new LicenseServerApiClient(_settings, true))
                {
                    probe.Validate(new LicenseValidationRequest
                    {
                        IdLicenca = _settings.LicenseId,
                        Cnpj = _settings.Cnpj,
                        Chave = _settings.Key,
                        Usuario = FirstNonEmpty(_settings.User, _settings.Email),
                        Terminal = FirstNonEmpty(_settings.Terminal, _settings.Key),
                        Versao = _settings.Version
                    });
                }

                return "authenticated";
            }
            catch (LicenseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw MapException(ex, "Falha ao conectar ao License Server para autenticação.");
            }
        }

        public void Dispose()
        {
            if (_ownsProvider && _ownedProvider != null)
                _ownedProvider.Dispose();
        }

        private static ILicenseServerClient GetOrCreateSharedClient(LicenseServerSettings settings)
        {
            string key = BuildProviderKey(settings);
            lock (FactorySync)
            {
                if (_sharedProvider == null || !string.Equals(_sharedProviderKey, key, StringComparison.Ordinal))
                {
                    if (_sharedProvider != null)
                        _sharedProvider.Dispose();

                    _sharedProvider = BuildProvider(settings);
                    _sharedProviderKey = key;
                }

                return _sharedProvider.GetRequiredService<ILicenseServerClient>();
            }
        }

        private static ServiceProvider BuildProvider(LicenseServerSettings settings)
        {
            var services = new ServiceCollection();
            services.AddLicenseServerClient(options =>
            {
                options.Email = settings.Email ?? string.Empty;
                options.Password = settings.Password ?? string.Empty;
                options.BaseUrl = LicenseServerSettings.NormalizeBaseUrl(settings.BaseUrl);
                options.Environment = settings.Environment == 1
                    ? LicenseServerEnvironment.Production
                    : LicenseServerEnvironment.Homologation;
            });
            return services.BuildServiceProvider();
        }

        private static string BuildProviderKey(LicenseServerSettings settings)
        {
            return string.Join("|",
                settings.Email ?? string.Empty,
                settings.Password ?? string.Empty,
                LicenseServerSettings.NormalizeBaseUrl(settings.BaseUrl) ?? string.Empty,
                settings.Environment.ToString());
        }

        private static T Run<T>(Func<Task<T>> action)
        {
            return action().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        private static void Run(Func<Task> action)
        {
            action().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        private static DllRequests.LicenseValidationRequest ToDllValidationRequest(LicenseValidationRequest request)
        {
            return new DllRequests.LicenseValidationRequest
            {
                IdLicenca = request.IdLicenca,
                Cnpj = request.Cnpj,
                Chave = request.Chave,
                Usuario = request.Usuario,
                Terminal = request.Terminal,
                Versao = request.Versao
            };
        }

        private static DllRequests.LicenseRevocationRequest ToDllRevocationRequest(LicenseValidationRequest request)
        {
            return new DllRequests.LicenseRevocationRequest
            {
                IdLicenca = request.IdLicenca,
                Cnpj = request.Cnpj,
                Chave = request.Chave,
                Usuario = request.Usuario,
                Terminal = request.Terminal,
                Versao = request.Versao
            };
        }

        private static LicenseValidationResult FromDll(DllResponses.LicenseValidationResult dllResult)
        {
            if (dllResult == null)
                return null;

            return new LicenseValidationResult
            {
                Licenca = FromDll(dllResult.Licenca)
            };
        }

        private static LicenseInfo FromDll(DllResponses.LicenseInfo dllInfo)
        {
            if (dllInfo == null)
                return null;

            return new LicenseInfo
            {
                IdLicencaUso = dllInfo.IdLicencaUso,
                LxStatusChave = dllInfo.LxStatusChave,
                Periodicidade = dllInfo.Periodicidade,
                DiasOffline = dllInfo.DiasOffline,
                Mensagem = dllInfo.Mensagem,
                Cnpj = dllInfo.Cnpj,
                OrigemBloqueio = dllInfo.OrigemBloqueio,
                QuantidadeContratada = dllInfo.QuantidadeContratada,
                QuantidadeEmUso = dllInfo.QuantidadeEmUso,
                LxStatusChaveName = dllInfo.LxStatusChaveName
            };
        }

        private static LicenseException MapException(Exception ex, string fallbackPrefix)
        {
            Exception root = Unwrap(ex);

            var auth = root as LicenseServerAuthenticationException;
            if (auth != null)
                return new LicenseException(FormatAuthenticationFailure(auth));

            var api = root as LicenseServerApiException;
            if (api != null)
                return new LicenseException(
                    "License Server retornou HTTP " + (int)api.StatusCode + "." + FormatBody(api.ResponseBody));

            if (root is LicenseException)
                return (LicenseException)root;

            return new LicenseException(fallbackPrefix + " " + root.Message);
        }

        private static string FormatAuthenticationFailure(LicenseServerAuthenticationException ex)
        {
            if (ex.StatusCode.HasValue)
            {
                return "Autenticação no License Server falhou com HTTP "
                    + (int)ex.StatusCode.Value + "."
                    + FormatBody(ex.ResponseBody);
            }

            string message = string.IsNullOrWhiteSpace(ex.Message)
                ? "Autenticação no License Server falhou."
                : ex.Message.Trim();

            if (message.IndexOf("Autenticação", StringComparison.OrdinalIgnoreCase) < 0
                && message.IndexOf("autentic", StringComparison.OrdinalIgnoreCase) < 0)
            {
                message = "Autenticação no License Server falhou. " + message;
            }

            return message + FormatBody(ex.ResponseBody);
        }

        private static Exception Unwrap(Exception ex)
        {
            while (ex is AggregateException && ex.InnerException != null)
                ex = ex.InnerException;
            return ex;
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

        private static string FirstNonEmpty(string first, string second)
        {
            if (!string.IsNullOrWhiteSpace(first))
                return first.Trim();
            if (!string.IsNullOrWhiteSpace(second))
                return second.Trim();
            return null;
        }
    }
}

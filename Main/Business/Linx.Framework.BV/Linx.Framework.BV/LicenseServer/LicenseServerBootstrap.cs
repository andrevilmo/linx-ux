using System;
using Linx.Tools;

namespace Linx.Framework.BV.LicenseServer
{
    /// <summary>
    /// Omni-style fail-closed gate for UX login (authenticateUser / UpdateToken).
    /// Configuration is read only from Web.config.
    /// </summary>
    public static class LicenseServerBootstrap
    {
        private static readonly object TrustUnblockSync = new object();
        private static string _lastTrustUnblockCnpj;
        private static DateTime _lastTrustUnblockUtc = DateTime.MinValue;
        private static readonly TimeSpan TrustUnblockSpacing = TimeSpan.FromMinutes(15);

        public static bool IsEnabled
        {
            get
            {
                if (LocalServiceBus.Enabled)
                    return false;
                return LicenseServerSettings.Load().Enabled;
            }
        }

        public static void EnsureLicensed(string chave, string usuario, Guid uidEmpresa)
        {
            var settings = LicenseServerSettings.Load();
            if (!settings.Enabled || LocalServiceBus.Enabled)
                return;

            string message;
            if (!settings.IsValid(out message))
                throw new LicenseException(message);

            var request = BuildRequest(settings, chave, usuario);
            var client = new LicenseServerApiClient(settings);
            LicenseValidationResult result;
            try
            {
                result = client.Validate(request);
            }
            catch (LicenseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new LicenseException("Falha ao validar a licença no License Server. " + ex.Message);
            }

            LicenseAccessGuard.EnsureValidationAccess(result);
        }

        /// <summary>
        /// Second call (Omni UnblockByTrustAsync): BillingRuler desbloqueio then re-Validate.
        /// Local credentials are checked by the caller, never sent to License Server.
        /// </summary>
        public static TrustUnblockResult TryUnblockByTrust()
        {
            var settings = LicenseServerSettings.Load();
            if (!settings.Enabled || LocalServiceBus.Enabled)
                return TrustUnblockResult.Create(TrustUnblockOutcome.Denied, "License Server não está habilitado.");

            string configMessage;
            if (!settings.IsValid(out configMessage))
                return TrustUnblockResult.Create(TrustUnblockOutcome.Failed, configMessage);

            string cnpj = settings.Cnpj;
            if (string.IsNullOrWhiteSpace(cnpj))
                return TrustUnblockResult.Create(TrustUnblockOutcome.Failed, "CNPJ da licença não informado.");

            lock (TrustUnblockSync)
            {
                if (string.Equals(_lastTrustUnblockCnpj, cnpj, StringComparison.Ordinal)
                    && DateTime.UtcNow - _lastTrustUnblockUtc < TrustUnblockSpacing)
                {
                    return TrustUnblockResult.Create(
                        TrustUnblockOutcome.Failed,
                        "Desbloqueio em confiança muito frequente; aguarde antes de tentar novamente.");
                }

                _lastTrustUnblockCnpj = cnpj;
                _lastTrustUnblockUtc = DateTime.UtcNow;
            }

            TrustUnblockResult unblock;
            try
            {
                unblock = new LicenseServerApiClient(settings).UnblockByTrust(cnpj);
            }
            catch (ArgumentException ex)
            {
                return TrustUnblockResult.Create(TrustUnblockOutcome.Failed, ex.Message);
            }
            catch (LicenseException ex)
            {
                return TrustUnblockResult.Create(TrustUnblockOutcome.Failed, ex.Message);
            }
            catch (Exception ex)
            {
                return TrustUnblockResult.Create(TrustUnblockOutcome.Failed, "Falha ao desbloquear licença: " + ex.Message);
            }

            if (unblock == null || !unblock.Succeeded)
            {
                if (unblock == null)
                    return TrustUnblockResult.Create(TrustUnblockOutcome.Failed, "Falha ao solicitar desbloqueio em confiança.");
                if (string.IsNullOrWhiteSpace(unblock.Details))
                {
                    unblock.Details = unblock.Outcome == (int)TrustUnblockOutcome.Denied
                        ? "Desbloqueio em confiança recusado pelo servidor."
                        : "Falha ao solicitar desbloqueio em confiança.";
                }
                return unblock;
            }

            try
            {
                var client = new LicenseServerApiClient(settings);
                var result = client.Validate(BuildRequest(settings, settings.Key, settings.User));
                LicenseAccessResult decision = LicenseAccessDecision.EvaluateValidation(result);
                if (!decision.Allowed)
                {
                    return TrustUnblockResult.Create(
                        TrustUnblockOutcome.Failed,
                        FirstNonEmpty(decision.Message, "Licença ainda bloqueada após desbloqueio."));
                }
            }
            catch (LicenseException ex)
            {
                return TrustUnblockResult.Create(TrustUnblockOutcome.Failed, LicenseException.StripMarker(ex.Message));
            }
            catch (Exception ex)
            {
                return TrustUnblockResult.Create(TrustUnblockOutcome.Failed, "Falha ao revalidar a licença: " + ex.Message);
            }

            return TrustUnblockResult.Create(TrustUnblockOutcome.Succeeded, null);
        }

        public static void Release(string chave, string usuario, Guid uidEmpresa)
        {
            var settings = LicenseServerSettings.Load();
            if (!settings.Enabled || LocalServiceBus.Enabled)
                return;

            string message;
            if (!settings.IsValid(out message))
                return;

            try
            {
                new LicenseServerApiClient(settings).Revoke(BuildRequest(settings, chave, usuario));
            }
            catch
            {
                /* never break logout because revoke failed */
            }
        }

        internal static LicenseValidationRequest BuildRequest(LicenseServerSettings settings, string chave, string usuario)
        {
            return new LicenseValidationRequest
            {
                IdLicenca = settings.LicenseId,
                Cnpj = settings.Cnpj,
                Chave = FirstNonEmpty(settings.Key, chave),
                Usuario = FirstNonEmpty(usuario, settings.User),
                Terminal = settings.Terminal,
                Versao = settings.Version
            };
        }

        private static string FirstNonEmpty(params string[] values)
        {
            if (values == null)
                return null;

            for (var i = 0; i < values.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(values[i]))
                    return values[i].Trim();
            }
            return null;
        }
    }
}

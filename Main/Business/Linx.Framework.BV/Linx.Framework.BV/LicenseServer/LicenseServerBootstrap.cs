using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Linx.LicenseServer.Client.Configuration;
using Linx.LicenseServer.Client.Exceptions;
using Linx.LicenseServer.Client.Licensing;
using Linx.Tools;
using DllTrustUnblockOutcome = Linx.LicenseServer.Client.Licensing.TrustUnblockOutcome;
using DllTrustUnblockResult = Linx.LicenseServer.Client.Licensing.TrustUnblockResult;

namespace Linx.Framework.BV.LicenseServer
{
    /// <summary>
    /// UX license gate using official Linx.LicenseServer.Client 1.0.1 (ILicenseManager).
    /// </summary>
    public static class LicenseServerBootstrap
    {
        private static readonly object ManagerSync = new object();
        private static ILicenseManager _manager;
        private static string _managerKey;

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
            EnsureLicensed(chave, usuario, uidEmpresa, Guid.Empty, 0, null);
        }

        public static void EnsureLicensed(
            string chave,
            string usuario,
            Guid uidEmpresa,
            Guid userUid,
            int environmentId,
            string sessionId)
        {
            var settings = LicenseServerSettings.Load();
            if (!settings.Enabled || LocalServiceBus.Enabled)
                return;

            string message;
            if (!settings.IsValid(out message))
                throw new LicenseException(message);

            LicenseBinding binding = BuildBinding(settings, chave, usuario, uidEmpresa, environmentId, sessionId);
            ILicenseManager manager = GetOrCreateManager(settings);

            LicenseStatus status;
            try
            {
                status = Run(() => manager.RegisterLicenseAsync(binding));
            }
            catch (Exception ex)
            {
                throw MapException(ex, "Falha ao registrar a licença no License Server.");
            }

            if (status != null && !status.IsBlocked)
            {
                if (userUid != Guid.Empty)
                    LicenseSessionStore.Remember(userUid, uidEmpresa, environmentId, binding);
                return;
            }

            ThrowBlocked(status);
        }

        public static TrustUnblockResult TryUnblockByTrust(string usuario)
        {
            if (!LicenseTrustUnblock.LS_CheckAllowUnblock(usuario))
            {
                var denied = TrustUnblockResult.Create(
                    TrustUnblockOutcome.Denied,
                    "Usuário não está apto a desbloquear em confiança.");
                LicenseTrustUnblockAudit.Record(usuario, null, TrustUnblockOutcome.Denied, denied.Details);
                return denied;
            }

            var settings = LicenseServerSettings.Load();
            if (!settings.Enabled || LocalServiceBus.Enabled)
            {
                var disabled = TrustUnblockResult.Create(TrustUnblockOutcome.Denied, "License Server não está habilitado.");
                LicenseTrustUnblockAudit.Record(usuario, null, TrustUnblockOutcome.Denied, disabled.Details);
                return disabled;
            }

            string configMessage;
            if (!settings.IsValid(out configMessage))
            {
                var invalid = TrustUnblockResult.Create(TrustUnblockOutcome.Failed, configMessage);
                LicenseTrustUnblockAudit.Record(usuario, settings.Cnpj, TrustUnblockOutcome.Failed, invalid.Details);
                return invalid;
            }

            string cnpj = LicenseTrustUnblock.LS_GetCNP(settings);
            if (string.IsNullOrWhiteSpace(cnpj))
            {
                var missing = TrustUnblockResult.Create(TrustUnblockOutcome.Failed, "CNPJ do GPECON não informado.");
                LicenseTrustUnblockAudit.Record(usuario, null, TrustUnblockOutcome.Failed, missing.Details);
                return missing;
            }

            lock (TrustUnblockSync)
            {
                if (string.Equals(_lastTrustUnblockCnpj, cnpj, StringComparison.Ordinal)
                    && DateTime.UtcNow - _lastTrustUnblockUtc < TrustUnblockSpacing)
                {
                    var throttled = TrustUnblockResult.Create(
                        TrustUnblockOutcome.Failed,
                        "Desbloqueio em confiança muito frequente; aguarde antes de tentar novamente.");
                    LicenseTrustUnblockAudit.Record(usuario, cnpj, TrustUnblockOutcome.Failed, throttled.Details);
                    return throttled;
                }

                _lastTrustUnblockCnpj = cnpj;
                _lastTrustUnblockUtc = DateTime.UtcNow;
            }

            TrustUnblockResult mapped;
            try
            {
                ILicenseManager manager = GetOrCreateManager(settings);
                DllTrustUnblockResult dllResult = Run(() => manager.UnblockByTrustAsync(cnpj));
                mapped = MapTrustUnblock(dllResult);
            }
            catch (ArgumentException ex)
            {
                mapped = TrustUnblockResult.Create(TrustUnblockOutcome.Failed, ex.Message);
            }
            catch (LicenseException ex)
            {
                mapped = TrustUnblockResult.Create(TrustUnblockOutcome.Failed, LicenseException.StripMarker(ex.Message));
            }
            catch (Exception ex)
            {
                mapped = TrustUnblockResult.Create(TrustUnblockOutcome.Failed, "Falha ao desbloquear licença: " + Unwrap(ex).Message);
            }

            LicenseTrustUnblockAudit.Record(usuario, cnpj, (TrustUnblockOutcome)mapped.Outcome, mapped.Details);
            return mapped;
        }

        public static void Release(string chave, string usuario, Guid uidEmpresa)
        {
            Release(chave, usuario, uidEmpresa, Guid.Empty, 0);
        }

        public static void Release(string chave, string usuario, Guid uidEmpresa, Guid userUid, int environmentId)
        {
            var settings = LicenseServerSettings.Load();
            if (!settings.Enabled || LocalServiceBus.Enabled)
                return;

            try
            {
                LicenseBinding binding;
                if (userUid != Guid.Empty
                    && LicenseSessionStore.TryTake(userUid, uidEmpresa, environmentId, out binding)
                    && binding != null)
                {
                    Run(() => GetOrCreateManager(settings).ReleaseLicenseAsync(binding));
                    return;
                }

                binding = BuildBinding(settings, chave, usuario, uidEmpresa, environmentId, null);
                Run(() => GetOrCreateManager(settings).ReleaseLicenseAsync(binding));
            }
            catch
            {
                /* never break logout because revoke failed */
            }
        }

        public static void ShutdownBestEffort()
        {
            lock (ManagerSync)
            {
                if (_manager == null)
                    return;
                try
                {
                    Run(() => _manager.ShutdownAsync());
                }
                catch
                {
                }
                finally
                {
                    DisposeManager_NoLock();
                }
            }
        }

        public static void SendActiveStoresBestEffort(LicenseServerSettings settings)
        {
            if (settings == null)
                settings = LicenseServerSettings.Load();

            var data = new ActiveStoreData
            {
                Cnpj = settings.Cnpj,
                RazaoSocial = FirstNonEmpty(settings.ProductName, settings.ProductId),
                NomeProduto = FirstNonEmpty(settings.ProductName, settings.ProductId),
                TipoProduto = FirstNonEmpty(settings.ProductType, "UX"),
                DataUltimoMovimento = DateTime.UtcNow,
                Versao = settings.Version,
                Origem = "Linx.UX",
                Observacao = "envio-diario"
            };

            try
            {
                var result = Run(() => GetOrCreateManager(settings).SendActiveStoreAsync(data));
                System.Diagnostics.Trace.WriteLine(
                    "[LicenseActiveStores] outcome=" + (result != null ? result.Outcome.ToString() : "null")
                    + " message=" + (result != null ? result.Message : null));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("[LicenseActiveStores] " + ex.Message);
            }
        }

        internal static void ResetManagerForTests()
        {
            lock (ManagerSync)
            {
                DisposeManager_NoLock();
            }
        }

        private static ILicenseManager GetOrCreateManager(LicenseServerSettings settings)
        {
            string key = BuildManagerKey(settings);
            lock (ManagerSync)
            {
                if (_manager != null && string.Equals(_managerKey, key, StringComparison.Ordinal))
                    return _manager;

                DisposeManager_NoLock();

                var options = new LicenseManagerOptions
                {
                    ProductId = settings.ProductId,
                    Email = settings.Email,
                    Password = settings.Password,
                    BaseUrl = LicenseServerSettings.NormalizeBaseUrl(settings.BaseUrl),
                    Environment = settings.Environment == 1
                        ? LicenseServerEnvironment.Production
                        : LicenseServerEnvironment.Homologation,
                    DefaultReleaseMode = LicenseReleaseMode.VariableWithIdleTimeout,
                    IdleTimeout = TimeSpan.FromMinutes(Math.Max(1, settings.IdleTimeoutMinutes))
                };

                _manager = LicenseManagerFactory.Create(options);
                _managerKey = key;
                LicenseActiveStoresScheduler.EnsureStarted();
                return _manager;
            }
        }

        private static void DisposeManager_NoLock()
        {
            var disposable = _manager as IDisposable;
            if (disposable != null)
            {
                try { disposable.Dispose(); }
                catch { }
            }
            _manager = null;
            _managerKey = null;
        }

        private static string BuildManagerKey(LicenseServerSettings settings)
        {
            return string.Join("|",
                settings.ProductId ?? string.Empty,
                settings.Email ?? string.Empty,
                settings.Password ?? string.Empty,
                LicenseServerSettings.NormalizeBaseUrl(settings.BaseUrl) ?? string.Empty,
                settings.Environment.ToString(),
                settings.IdleTimeoutMinutes.ToString());
        }

        internal static LicenseBinding BuildBinding(
            LicenseServerSettings settings,
            string chave,
            string usuario,
            Guid uidEmpresa,
            int environmentId,
            string sessionId)
        {
            string cnpj = LicenseTrustUnblock.LS_GetCNP(settings);
            string environmentTag = MapEnvironmentTag(environmentId, settings.Environment);
            string session = FirstNonEmpty(sessionId, Guid.NewGuid().ToString("N"));

            var binding = new LicenseBinding
            {
                LicenseId = settings.LicenseId,
                Cnpj = cnpj,
                User = FirstNonEmpty(usuario, settings.User),
                Terminal = settings.Terminal,
                Version = settings.Version,
                EnvironmentTag = environmentTag,
                ReleaseMode = LicenseReleaseMode.VariableWithIdleTimeout,
                KeyParts = new[]
                {
                    uidEmpresa == Guid.Empty ? "empresa" : uidEmpresa.ToString("N"),
                    FirstNonEmpty(usuario, settings.User, "usuario"),
                    session,
                    environmentTag
                }
            };

            // Optional fixed key override only when explicitly configured and no session composition desired.
            if (!string.IsNullOrWhiteSpace(settings.Key)
                && string.Equals(settings.Key, chave, StringComparison.OrdinalIgnoreCase)
                && string.IsNullOrWhiteSpace(sessionId)
                && uidEmpresa == Guid.Empty)
            {
                binding.Key = settings.Key;
                binding.KeyParts = null;
            }

            return binding;
        }

        private static string MapEnvironmentTag(int environmentId, int settingsEnvironment)
        {
            // UX logical environment: prefer runtime ambiente id when available; else config Homologation/Production.
            if (environmentId > 0)
                return "Ambiente-" + environmentId;
            return settingsEnvironment == 1 ? "Producao" : "Homologacao";
        }

        private static void ThrowBlocked(LicenseStatus status)
        {
            if (status == null)
            {
                throw new LicenseException(
                    LicenseAccessDecision.GetBlockReasonMessage("NotYetValidated"),
                    false,
                    "USAGE_KEY_MISSING");
            }

            string reason = status.Reason.ToString();
            string serverMessage = null;
            if (status.ServerData != null && !string.IsNullOrWhiteSpace(status.ServerData.Message))
                serverMessage = status.ServerData.Message;

            string message = FirstNonEmpty(serverMessage, LicenseAccessDecision.GetBlockReasonMessage(reason));
            bool canUnblock = status.CanUnblockByTrust;
            string reasonCode = MapReasonCode(status.Reason, canUnblock);
            throw new LicenseException(message, canUnblock, reasonCode);
        }

        private static string MapReasonCode(LicenseBlockReason reason, bool canUnblock)
        {
            switch (reason)
            {
                case LicenseBlockReason.BlockedByBillingRuler:
                    return "CUSTOMER_FINANCIAL_BLOCK";
                case LicenseBlockReason.BlockedByContract:
                    return "CUSTOMER_LICENSE_DISCONTINUED";
                case LicenseBlockReason.UsageLimitReached:
                    return "USAGE_KEY_UNAUTHORIZED";
                case LicenseBlockReason.Revoked:
                case LicenseBlockReason.IdleTimeoutExpired:
                    return "USAGE_KEY_REVOKED";
                case LicenseBlockReason.NotYetValidated:
                    return "USAGE_KEY_MISSING";
                default:
                    return canUnblock ? "CUSTOMER_FINANCIAL_BLOCK" : "USAGE_KEY_INVALID";
            }
        }

        private static TrustUnblockResult MapTrustUnblock(DllTrustUnblockResult dllResult)
        {
            if (dllResult == null)
                return TrustUnblockResult.Create(TrustUnblockOutcome.Failed, "Falha ao solicitar desbloqueio em confiança.");

            if (dllResult.Outcome == DllTrustUnblockOutcome.Succeeded)
                return TrustUnblockResult.Create(TrustUnblockOutcome.Succeeded, null);

            if (dllResult.Outcome == DllTrustUnblockOutcome.Denied)
            {
                return TrustUnblockResult.Create(
                    TrustUnblockOutcome.Denied,
                    FirstNonEmpty(dllResult.Details, "Desbloqueio em confiança recusado pelo servidor."));
            }

            return TrustUnblockResult.Create(
                TrustUnblockOutcome.Failed,
                FirstNonEmpty(dllResult.Details, "Falha ao solicitar desbloqueio em confiança."));
        }

        private static LicenseException MapException(Exception ex, string fallbackPrefix)
        {
            Exception root = Unwrap(ex);

            var auth = root as LicenseServerAuthenticationException;
            if (auth != null)
            {
                if (auth.StatusCode.HasValue)
                {
                    return new LicenseException(
                        "Autenticação no License Server falhou com HTTP "
                        + (int)auth.StatusCode.Value + "."
                        + FormatBody(auth.ResponseBody));
                }
                return new LicenseException("Autenticação no License Server falhou. " + auth.Message);
            }

            var api = root as LicenseServerApiException;
            if (api != null)
            {
                return new LicenseException(
                    "License Server retornou HTTP " + (int)api.StatusCode + "." + FormatBody(api.ResponseBody));
            }

            if (root is LicenseException)
                return (LicenseException)root;

            return new LicenseException(fallbackPrefix + " " + root.Message);
        }

        private static T Run<T>(Func<Task<T>> action)
        {
            return action().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        private static void Run(Func<Task> action)
        {
            action().ConfigureAwait(false).GetAwaiter().GetResult();
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

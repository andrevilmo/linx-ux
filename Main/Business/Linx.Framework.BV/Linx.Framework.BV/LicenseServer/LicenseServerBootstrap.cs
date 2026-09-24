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

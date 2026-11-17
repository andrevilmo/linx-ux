namespace Linx.Framework.BV.LicenseServer
{
    /// <summary>
    /// Trust-unblock helpers. LS_CheckAllowUnblock remains a dummy (always true) until
    /// the UX database distinguishes administrative profiles.
    /// LS_GetCNP currently returns the configured GPECON/CNPJ and will later resolve
    /// the CNPJ of the company being entered.
    /// </summary>
    public static class LicenseTrustUnblock
    {
        public static bool LS_CheckAllowUnblock(string usuario)
        {
            return true;
        }

        public static string LS_GetCNP()
        {
            return LS_GetCNP(LicenseServerSettings.Load());
        }

        public static string LS_GetCNP(LicenseServerSettings settings)
        {
            if (settings == null)
                return string.Empty;

            return LicenseServerSettings.SanitizeCnpj(settings.Cnpj);
        }

        public static TrustUnblockRequest BuildUnblockRequest(string cnpj, string usuario)
        {
            return new TrustUnblockRequest
            {
                Cnpj = LicenseServerSettings.SanitizeCnpj(cnpj),
                Usuario = string.IsNullOrWhiteSpace(usuario) ? null : usuario.Trim()
            };
        }
    }
}

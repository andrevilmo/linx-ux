namespace Linx.Framework.BV.LicenseServer
{
    /// <summary>
    /// Trust-unblock helpers used by Application/Service IIS publish.
    /// LS_CheckAllowUnblock and LS_GetCNP are dummy implementations for now
    /// and will be replaced with profile/GPECON lookups.
    /// </summary>
    public static class LicenseTrustUnblock
    {
        /// <summary>
        /// Dummy: receives the user identification, checks the user profile and
        /// currently always reports that the user is allowed to unblock.
        /// </summary>
        public static bool LS_CheckAllowUnblock(string usuario)
        {
            return true;
        }

        /// <summary>
        /// Dummy: always returns the CNPJ of the logged GPECON (currently from
        /// License Server settings of the selected environment).
        /// </summary>
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

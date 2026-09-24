namespace Linx.Framework.BV.LicenseServer
{
    /// <summary>
    /// Facade used by the existing LicenseControl hook.
    /// Throws LicenseException so AuthenticateUser / LIA already surface the block.
    /// </summary>
    public static class LicenseAccessGuard
    {
        public static void EnsureCustomerAccess(LicenseAccessSnapshot snapshot)
        {
            LicenseAccessResult result = LicenseAccessDecision.EvaluateCustomer(snapshot);
            if (!result.Allowed)
                throw new LicenseException(result.Message);
        }

        public static void EnsureUsageKeyAccess(LicenseUsageSnapshot snapshot)
        {
            LicenseAccessResult result = LicenseAccessDecision.EvaluateUsageKey(snapshot);
            if (!result.Allowed)
                throw new LicenseException(result.Message);
        }
    }
}

using System;
using System.Collections.Concurrent;
using Linx.LicenseServer.Client.Licensing;

namespace Linx.Framework.BV.LicenseServer
{
    /// <summary>
    /// Holds the LicenseBinding registered for an active UX session so logout can release it.
    /// </summary>
    public static class LicenseSessionStore
    {
        private static readonly ConcurrentDictionary<string, LicenseBinding> Bindings =
            new ConcurrentDictionary<string, LicenseBinding>(StringComparer.OrdinalIgnoreCase);

        public static string BuildKey(Guid userUid, Guid companyUid, int environmentId)
        {
            return userUid.ToString("N") + "|" + companyUid.ToString("N") + "|" + environmentId;
        }

        public static void Remember(Guid userUid, Guid companyUid, int environmentId, LicenseBinding binding)
        {
            if (binding == null)
                return;
            Bindings[BuildKey(userUid, companyUid, environmentId)] = binding;
        }

        public static bool TryGet(Guid userUid, Guid companyUid, int environmentId, out LicenseBinding binding)
        {
            return Bindings.TryGetValue(BuildKey(userUid, companyUid, environmentId), out binding);
        }

        public static bool TryTake(Guid userUid, Guid companyUid, int environmentId, out LicenseBinding binding)
        {
            return Bindings.TryRemove(BuildKey(userUid, companyUid, environmentId), out binding);
        }

        public static void Forget(Guid userUid, Guid companyUid, int environmentId)
        {
            LicenseBinding unused;
            Bindings.TryRemove(BuildKey(userUid, companyUid, environmentId), out unused);
        }
    }
}

using Linx.Framework.BV.LicenseServer;
using System;

namespace Linx.Framework.BV
{
    public static class LicenseControl
    {
        public static void Validate(string chave, string usuario, Guid uidEmpresa)
        {
            Validate(chave, usuario, uidEmpresa, Guid.Empty, 0, null);
        }

        public static void Validate(
            string chave,
            string usuario,
            Guid uidEmpresa,
            Guid userUid,
            int environmentId,
            string sessionId)
        {
            if (LicenseServerBootstrap.IsEnabled)
                LicenseServerBootstrap.EnsureLicensed(chave, usuario, uidEmpresa, userUid, environmentId, sessionId);
        }

        public static void Remove(string chave, string usuario, Guid uidEmpresa)
        {
            Remove(chave, usuario, uidEmpresa, Guid.Empty, 0);
        }

        public static void Remove(string chave, string usuario, Guid uidEmpresa, Guid userUid, int environmentId)
        {
            if (LicenseServerBootstrap.IsEnabled)
                LicenseServerBootstrap.Release(chave, usuario, uidEmpresa, userUid, environmentId);
        }

        public static TrustUnblockResult UnblockByTrust(string usuario)
        {
            return LicenseServerBootstrap.TryUnblockByTrust(usuario);
        }
    }
}

using Linx.Framework.BV.LicenseServer;
using System;

namespace Linx.Framework.BV
{
    class LicenseException : Exception
    {
        public LicenseException(string message) : base(message)
        {
        }
    }

    public static class LicenseControl
    {
        public static void Validate(string chave, string usuario, Guid uidEmpresa)
        {
            if (LicenseServerBootstrap.IsEnabled)
                LicenseServerBootstrap.EnsureLicensed(chave, usuario, uidEmpresa);
        }

        public static void Remove(string chave, string usuario, Guid uidEmpresa)
        {
            if (LicenseServerBootstrap.IsEnabled)
                LicenseServerBootstrap.Release(chave, usuario, uidEmpresa);
        }
    }
}

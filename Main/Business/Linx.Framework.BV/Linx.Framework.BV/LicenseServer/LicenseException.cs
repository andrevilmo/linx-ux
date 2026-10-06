using System;

namespace Linx.Framework.BV
{
    public class LicenseException : Exception
    {
        public LicenseException(string message)
            : base(message)
        {
        }
    }
}

using System;

namespace Linx.Framework.BV
{
    public class LicenseException : Exception
    {
        public const string CanUnblockByTrustMarker = "[canUnblockByTrust=true]";

        public bool CanUnblockByTrust { get; private set; }
        public string ReasonCode { get; private set; }

        public LicenseException(string message)
            : this(message, false, null)
        {
        }

        public LicenseException(string message, bool canUnblockByTrust, string reasonCode)
            : base(AppendMarker(message, canUnblockByTrust))
        {
            CanUnblockByTrust = canUnblockByTrust;
            ReasonCode = reasonCode;
        }

        public static bool MessageOffersUnblockByTrust(string text)
        {
            return !string.IsNullOrEmpty(text)
                && text.IndexOf("canUnblockByTrust=true", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public static string StripMarker(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;
            return text.Replace(CanUnblockByTrustMarker, string.Empty).Trim();
        }

        private static string AppendMarker(string message, bool canUnblockByTrust)
        {
            var text = message ?? string.Empty;
            if (!canUnblockByTrust || MessageOffersUnblockByTrust(text))
                return text;
            if (text.Length == 0)
                return CanUnblockByTrustMarker;
            return text.TrimEnd() + " " + CanUnblockByTrustMarker;
        }
    }
}

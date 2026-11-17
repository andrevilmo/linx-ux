using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Linx.Framework.BV.LicenseServer
{
    /// <summary>
    /// Persists who triggered trust-unblock and when (independent of permission rules).
    /// </summary>
    public static class LicenseTrustUnblockAudit
    {
        private static readonly object Sync = new object();
        private static readonly List<string> Recent = new List<string>();
        private const int RecentLimit = 200;

        public static void Record(string usuario, string cnpj, TrustUnblockOutcome outcome, string details)
        {
            string line = DateTime.UtcNow.ToString("o")
                + "\tusuario=" + (usuario ?? string.Empty)
                + "\tcnpj=" + (cnpj ?? string.Empty)
                + "\toutcome=" + outcome
                + "\tdetails=" + Sanitize(details);

            lock (Sync)
            {
                Recent.Add(line);
                while (Recent.Count > RecentLimit)
                    Recent.RemoveAt(0);
            }

            try
            {
                Trace.WriteLine("[LicenseTrustUnblockAudit] " + line);
            }
            catch
            {
            }

            try
            {
                string dir = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                    "Linx",
                    "UX",
                    "LicenseServer");
                Directory.CreateDirectory(dir);
                File.AppendAllText(Path.Combine(dir, "trust-unblock-audit.log"), line + Environment.NewLine, Encoding.UTF8);
            }
            catch
            {
                // Audit must never break unblock.
            }
        }

        public static IList<string> GetRecentSnapshot()
        {
            lock (Sync)
            {
                return new List<string>(Recent);
            }
        }

        private static string Sanitize(string details)
        {
            if (string.IsNullOrEmpty(details))
                return string.Empty;
            return details.Replace('\r', ' ').Replace('\n', ' ').Trim();
        }
    }
}

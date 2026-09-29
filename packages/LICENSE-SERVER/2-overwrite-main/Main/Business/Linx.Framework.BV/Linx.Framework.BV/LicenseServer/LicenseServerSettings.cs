using System;
using System.Configuration;

namespace Linx.Framework.BV.LicenseServer
{
    /// <summary>
    /// All License Server settings come from Web.config appSettings.
    /// </summary>
    public sealed class LicenseServerSettings
    {
        public bool Enabled { get; set; }
        public string BaseUrl { get; set; }
        public string ApiV1Root { get; set; }
        public int Environment { get; set; }
        public string ProductId { get; set; }
        public long LicenseId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Cnpj { get; set; }
        public string Key { get; set; }
        public string User { get; set; }
        public string Terminal { get; set; }
        public string Version { get; set; }

        public static LicenseServerSettings Load()
        {
            var settings = new LicenseServerSettings
            {
                Enabled = ReadBool("LicenseServer.Enabled", false),
                BaseUrl = NormalizeBaseUrl(Read("LicenseServer.BaseUrl", "https://api-hml.linx.com.br/app-licensing/")),
                ApiV1Root = Read("LicenseServer.ApiV1Root", "https://api-hml.linx.com.br/app-licensing/api/v1/"),
                Environment = ReadInt("LicenseServer.Environment", 0),
                ProductId = Read("LicenseServer.ProductId", "LINX-POS"),
                LicenseId = ReadLong("LicenseServer.LicenseId", 4),
                Email = Read("LicenseServer.Email", string.Empty),
                Password = Read("LicenseServer.Password", string.Empty),
                Cnpj = SanitizeCnpj(Read("LicenseServer.Cnpj", string.Empty)),
                Key = Read("LicenseServer.Key", string.Empty),
                User = Read("LicenseServer.User", string.Empty),
                Terminal = Read("LicenseServer.Terminal", System.Environment.MachineName),
                Version = Read("LicenseServer.Version", "1.0")
            };

            if (string.IsNullOrWhiteSpace(settings.Terminal))
                settings.Terminal = System.Environment.MachineName;

            return settings;
        }

        public bool IsValid(out string message)
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                message = "LicenseServer.Email e LicenseServer.Password são obrigatórios no Web.config.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(Cnpj) || string.IsNullOrWhiteSpace(Key) || LicenseId <= 0)
            {
                message = "LicenseServer.Cnpj, LicenseServer.Key e LicenseServer.LicenseId são obrigatórios no Web.config.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(ProductId))
            {
                message = "LicenseServer.ProductId é obrigatório no Web.config.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(BaseUrl))
            {
                message = "LicenseServer.BaseUrl é obrigatório no Web.config.";
                return false;
            }

            message = null;
            return true;
        }

        public static string SanitizeCnpj(string cnpj)
        {
            if (string.IsNullOrEmpty(cnpj))
                return string.Empty;

            var digits = new char[cnpj.Length];
            var count = 0;
            for (var i = 0; i < cnpj.Length; i++)
            {
                if (char.IsDigit(cnpj[i]))
                    digits[count++] = cnpj[i];
            }
            return new string(digits, 0, count);
        }

        public static string NormalizeBaseUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return url;

            url = url.Trim();
            var markers = new[] { "/api/v1/", "/api/v1" };
            foreach (var marker in markers)
            {
                if (url.EndsWith(marker, StringComparison.OrdinalIgnoreCase))
                {
                    url = url.Substring(0, url.Length - marker.Length);
                    break;
                }
            }

            if (!url.EndsWith("/", StringComparison.Ordinal))
                url = url + "/";

            return url;
        }

        private static string Read(string key, string fallback)
        {
            try
            {
                var value = ConfigurationManager.AppSettings[key];
                return string.IsNullOrWhiteSpace(value) ? fallback : value.Trim();
            }
            catch
            {
                return fallback;
            }
        }

        private static bool ReadBool(string key, bool fallback)
        {
            var raw = Read(key, null);
            bool parsed;
            return bool.TryParse(raw, out parsed) ? parsed : fallback;
        }

        private static int ReadInt(string key, int fallback)
        {
            var raw = Read(key, null);
            int parsed;
            return int.TryParse(raw, out parsed) ? parsed : fallback;
        }

        private static long ReadLong(string key, long fallback)
        {
            var raw = Read(key, null);
            long parsed;
            return long.TryParse(raw, out parsed) ? parsed : fallback;
        }
    }
}

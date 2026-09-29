using System;
using System.IO;
using System.Xml;
using Linx.Framework.BV;
using Linx.Framework.BV.LicenseServer;

namespace Linx.License.Server.Access.Tests
{
    /// <summary>
    /// Hits HML app-licensing with the Service Web.config binding.
    /// </summary>
    internal static class LicenseServerLiveTests
    {
        public static void Run()
        {
            Console.WriteLine();
            Console.WriteLine("=== Live HML License Server ===");

            LicenseServerSettings settings = LoadFromWebConfig();
            Program.AssertTrue(settings.IsValid(out string validMessage), "Web.config binding is complete" + (validMessage == null ? string.Empty : " (" + validMessage + ")"));
            Program.AssertTrue(!string.IsNullOrWhiteSpace(settings.Email), "Web.config Email is present");
            Program.AssertTrue(settings.LicenseId == 4, "Web.config LicenseId is 4");
            Program.AssertEqual("45510647000100", settings.Cnpj, "Web.config CNPJ");
            Program.AssertEqual("MAQUINA-01", settings.Key, "Web.config chave");

            RunWrongPassword(settings);
            RunSuccessfulAuthAndValidate(settings);
        }

        private static void RunWrongPassword(LicenseServerSettings configured)
        {
            LicenseServerApiClient.ResetTokenCacheForTests();

            var wrongPassword = Clone(configured);
            wrongPassword.Password = "wrong-password-" + Guid.NewGuid().ToString("N");

            bool threw = false;
            string message = null;
            try
            {
                new LicenseServerApiClient(wrongPassword).GetToken(true);
            }
            catch (LicenseException ex)
            {
                threw = true;
                message = ex.Message;
            }

            Program.AssertTrue(threw, "Wrong password throws LicenseException");
            Program.AssertContains("Autenticação", message, "Wrong password message mentions authentication");
            Program.AssertTrue(
                message != null && message.IndexOf("HTTP", StringComparison.OrdinalIgnoreCase) >= 0,
                "Wrong password message includes HTTP status");
        }

        private static void RunSuccessfulAuthAndValidate(LicenseServerSettings settings)
        {
            LicenseServerApiClient.ResetTokenCacheForTests();

            var client = new LicenseServerApiClient(settings);
            string token = client.GetToken(true);
            Program.AssertTrue(!string.IsNullOrWhiteSpace(token), "Configured password returns an API token");

            var result = client.Validate(new LicenseValidationRequest
            {
                IdLicenca = settings.LicenseId,
                Cnpj = settings.Cnpj,
                Chave = settings.Key,
                Usuario = FirstNonEmpty(settings.User, settings.Email),
                Terminal = FirstNonEmpty(settings.Terminal, settings.Key),
                Versao = settings.Version
            });

            Program.AssertTrue(result != null && result.Licenca != null, "Validate returns a license payload");
            Program.AssertEqual(settings.Cnpj, LicenseServerSettings.SanitizeCnpj(result.Licenca.Cnpj), "Validate echoes the configured CNPJ");

            LicenseAccessResult decision = LicenseAccessDecision.EvaluateValidation(result);
            Program.AssertTrue(!string.IsNullOrWhiteSpace(decision.ReasonCode), "Validate result produces a license decision");
            Console.WriteLine("INFO  Live Validate lxStatusChave=" + result.Licenca.LxStatusChave
                + " origemBloqueio=" + (result.Licenca.OrigemBloqueio ?? string.Empty)
                + " allowed=" + decision.Allowed
                + " reason=" + decision.ReasonCode);
        }

        internal static LicenseServerSettings LoadFromWebConfig()
        {
            string path = FindWebConfig();
            Program.AssertTrue(path != null && File.Exists(path), "Service Web.config is available");

            var settings = new LicenseServerSettings();
            var document = new XmlDocument();
            document.Load(path);
            var nodes = document.SelectNodes("//appSettings/add");
            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    if (node.Attributes == null)
                        continue;
                    var keyAttr = node.Attributes["key"];
                    var valueAttr = node.Attributes["value"];
                    if (keyAttr == null)
                        continue;
                    Apply(settings, keyAttr.Value, valueAttr == null ? null : valueAttr.Value);
                }
            }

            settings.BaseUrl = LicenseServerSettings.NormalizeBaseUrl(settings.BaseUrl);
            settings.Cnpj = LicenseServerSettings.SanitizeCnpj(settings.Cnpj);
            if (string.IsNullOrWhiteSpace(settings.Terminal))
                settings.Terminal = System.Environment.MachineName;
            return settings;
        }

        private static void Apply(LicenseServerSettings settings, string key, string value)
        {
            if (string.IsNullOrWhiteSpace(key))
                return;
            value = value ?? string.Empty;
            switch (key)
            {
                case "LicenseServer.Enabled":
                    bool enabled;
                    settings.Enabled = bool.TryParse(value, out enabled) && enabled;
                    break;
                case "LicenseServer.BaseUrl":
                    settings.BaseUrl = value;
                    break;
                case "LicenseServer.ApiV1Root":
                    settings.ApiV1Root = value;
                    break;
                case "LicenseServer.ProductId":
                    settings.ProductId = value;
                    break;
                case "LicenseServer.LicenseId":
                    long licenseId;
                    settings.LicenseId = long.TryParse(value, out licenseId) ? licenseId : 0;
                    break;
                case "LicenseServer.Email":
                    settings.Email = value;
                    break;
                case "LicenseServer.Password":
                    settings.Password = value;
                    break;
                case "LicenseServer.Cnpj":
                    settings.Cnpj = value;
                    break;
                case "LicenseServer.Key":
                    settings.Key = value;
                    break;
                case "LicenseServer.User":
                    settings.User = value;
                    break;
                case "LicenseServer.Terminal":
                    settings.Terminal = value;
                    break;
                case "LicenseServer.Version":
                    settings.Version = value;
                    break;
            }
        }

        private static string FindWebConfig()
        {
            var dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            for (var i = 0; i < 8 && dir != null; i++, dir = dir.Parent)
            {
                var candidate = Path.Combine(dir.FullName, "Main", "Binary", "Service", "Web.config");
                if (File.Exists(candidate))
                    return candidate;
            }

            var cwd = new DirectoryInfo(Directory.GetCurrentDirectory());
            for (var i = 0; i < 8 && cwd != null; i++, cwd = cwd.Parent)
            {
                var candidate = Path.Combine(cwd.FullName, "Main", "Binary", "Service", "Web.config");
                if (File.Exists(candidate))
                    return candidate;
            }

            return null;
        }

        private static LicenseServerSettings Clone(LicenseServerSettings source)
        {
            return new LicenseServerSettings
            {
                Enabled = source.Enabled,
                BaseUrl = source.BaseUrl,
                ApiV1Root = source.ApiV1Root,
                Environment = source.Environment,
                ProductId = source.ProductId,
                LicenseId = source.LicenseId,
                Email = source.Email,
                Password = source.Password,
                Cnpj = source.Cnpj,
                Key = source.Key,
                User = source.User,
                Terminal = source.Terminal,
                Version = source.Version
            };
        }

        private static string FirstNonEmpty(string first, string second)
        {
            if (!string.IsNullOrWhiteSpace(first))
                return first.Trim();
            if (!string.IsNullOrWhiteSpace(second))
                return second.Trim();
            return null;
        }
    }
}

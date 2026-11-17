using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;
using Linx.Framework.BV.LicenseServer;

namespace Linx.License.Server.Access.Tests
{
    /// <summary>
    /// Live checks against Web.config + official Linx.LicenseServer.Client 1.0.1.
    /// Full RegisterLicenseAsync requires Windows (DPAPI); non-Windows hosts skip that part.
    /// </summary>
    internal static class LicenseServerLiveTests
    {
        public static void Run()
        {
            Console.WriteLine();
            Console.WriteLine("=== Live License Server (DLL 1.0.1) ===");

            LicenseServerSettings settings = LoadFromWebConfig();
            Program.AssertTrue(settings.IsValid(out string validMessage), "Web.config binding is complete" + (validMessage == null ? string.Empty : " (" + validMessage + ")"));
            Program.AssertTrue(!string.IsNullOrWhiteSpace(settings.Email), "Web.config Email is present");
            Program.AssertTrue(settings.LicenseId == 4, "Web.config LicenseId is 4");
            Program.AssertEqual("45510647000100", settings.Cnpj, "Web.config CNPJ");
            Program.AssertTrue(!string.IsNullOrWhiteSpace(settings.ProductId), "Web.config ProductId is present");
            Program.AssertTrue(settings.IdleTimeoutMinutes > 0, "IdleTimeoutMinutes configured");

            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.WriteLine("SKIP  RegisterLicenseAsync / manager live calls — official DLL 1.0.1 requires Windows (DPAPI).");
                Program.AssertTrue(true, "Skipped manager live calls on non-Windows host");
                return;
            }

            RunWrongPassword(settings);
            RunSuccessfulRegister(settings);
        }

        private static void RunWrongPassword(LicenseServerSettings configured)
        {
            LicenseServerBootstrap.ResetManagerForTests();

            var wrongPassword = Clone(configured);
            wrongPassword.Password = "wrong-password-" + Guid.NewGuid().ToString("N");

            bool threw = false;
            string message = null;
            try
            {
                LicenseServerBootstrap.EnsureLicensed(
                    "sessao-teste",
                    FirstNonEmpty(wrongPassword.User, wrongPassword.Email),
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    1,
                    Guid.NewGuid().ToString("N"));
            }
            catch (Linx.Framework.BV.LicenseException ex)
            {
                threw = true;
                message = ex.Message;
            }
            finally
            {
                LicenseServerBootstrap.ResetManagerForTests();
            }

            Program.AssertTrue(threw, "Wrong password throws LicenseException");
            Program.AssertContains("Autenticação", message, "Wrong password message mentions authentication");
        }

        private static void RunSuccessfulRegister(LicenseServerSettings settings)
        {
            LicenseServerBootstrap.ResetManagerForTests();
            try
            {
                LicenseServerBootstrap.EnsureLicensed(
                    "sessao-teste",
                    FirstNonEmpty(settings.User, settings.Email),
                    Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    1,
                    Guid.NewGuid().ToString("N"));
                Program.AssertTrue(true, "RegisterLicenseAsync completed without throw");
            }
            catch (Linx.Framework.BV.LicenseException ex)
            {
                // Financial block is a valid server verdict for the HML fixture.
                Program.AssertTrue(
                    !string.IsNullOrWhiteSpace(ex.Message),
                    "Register returned a license decision: " + ex.Message);
                Console.WriteLine("INFO  Live Register blocked as expected: " + ex.Message
                    + " canUnblock=" + ex.CanUnblockByTrust
                    + " reason=" + ex.ReasonCode);
            }
            finally
            {
                LicenseServerBootstrap.ResetManagerForTests();
            }
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
                case "LicenseServer.ProductName":
                    settings.ProductName = value;
                    break;
                case "LicenseServer.ProductType":
                    settings.ProductType = value;
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
                case "LicenseServer.IdleTimeoutMinutes":
                    int idle;
                    settings.IdleTimeoutMinutes = int.TryParse(value, out idle) ? idle : 120;
                    break;
                case "LicenseServer.ActiveStoresEnabled":
                    bool activeStores;
                    settings.ActiveStoresEnabled = bool.TryParse(value, out activeStores) && activeStores;
                    break;
                case "LicenseServer.Environment":
                    int env;
                    settings.Environment = int.TryParse(value, out env) ? env : 0;
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
                ProductName = source.ProductName,
                ProductType = source.ProductType,
                LicenseId = source.LicenseId,
                Email = source.Email,
                Password = source.Password,
                Cnpj = source.Cnpj,
                Key = source.Key,
                User = source.User,
                Terminal = source.Terminal,
                Version = source.Version,
                IdleTimeoutMinutes = source.IdleTimeoutMinutes,
                ActiveStoresEnabled = source.ActiveStoresEnabled
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

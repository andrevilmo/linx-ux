using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Linx.Portal
{
    /// <summary>
    /// Server-side hCaptcha siteverify (https://docs.hcaptcha.com/).
    /// </summary>
    public static class HCaptchaVerifier
    {
        private const string SiteVerifyUrl = "https://hcaptcha.com/siteverify";

        public static bool TryVerify(string responseToken, string remoteIp, out string errorMessage)
        {
            errorMessage = null;

            if (!Utils.IsHCaptchaEnabled())
                return true;

            string secret = Utils.GetHCaptchaSecretKey();
            if (string.IsNullOrWhiteSpace(secret))
            {
                errorMessage = "Captcha não configurado no servidor.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(responseToken))
            {
                errorMessage = "Confirme o captcha antes de entrar.";
                return false;
            }

            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection
                    {
                        { "secret", secret.Trim() },
                        { "response", responseToken.Trim() }
                    };
                    if (!string.IsNullOrWhiteSpace(remoteIp))
                        values["remoteip"] = remoteIp.Trim();

                    byte[] raw = client.UploadValues(SiteVerifyUrl, values);
                    string json = Encoding.UTF8.GetString(raw);
                    JObject parsed = JObject.Parse(json);
                    bool success = parsed.Value<bool?>("success") == true;
                    if (success)
                        return true;

                    errorMessage = "Captcha inválido ou expirado. Tente novamente.";
                    return false;
                }
            }
            catch (Exception ex)
            {
                errorMessage = "Não foi possível validar o captcha. Tente novamente.";
                System.Diagnostics.Trace.WriteLine("hCaptcha siteverify failed: " + ex.Message);
                return false;
            }
        }
    }
}

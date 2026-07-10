using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;



namespace Linx.Tools
{
    public static class LocalServiceBus
    {
        private static bool _devMode = false;
        public static bool DevMode { get { return _devMode; } }
        private static bool _hasError = false;
        private static Dictionary<int, string> _headers = new Dictionary<int, string>();
        private static Dictionary<string, string> _businessAddresses = new Dictionary<string, string>();
        public static Dictionary<string, string> BusinessAddresses { get { return _businessAddresses; } }
        private static string _authenticationServer = "";
        private static string _user = "";
        private static string _password = "";
        private static string _applicationId = "";
        private static bool _enabled = false;
        private static Guid? _currentUser = null;
        private static Int64? _currentUserId = null;
        private static int _environment = 0;
        private static Guid? _currentCompany = null;
        private static Guid? _economicGroup = null;

        public static bool Enabled { get { return !_hasError && (_enabled || _devMode); } }
        private static string _fingerPrint = "";
        public static int IdLinx { get; set; }
        public static int IdGpecon { get; set; }
        public static bool IsUserMultiGpecon { get; set; }
        public static int ApplicativeId { get; set; }
        public static bool IsUserAdministrator { get; set; }

        public static string FingerPrint
        {
            get
            {
                if (_fingerPrint.IsNullOrEmpty())
                    _fingerPrint = Linx.Tools.FingerPrint.GetFlatValue();

                return _fingerPrint;
            }
        }

        public static Guid ApplicationId
        {
            get
            {
                return new Guid(_applicationId);
            }
        }

        public static string CurrentUserName
        {
            get
            {
                return _user;
            }
        }

        public static Guid? CurrentUser
        {
            get
            {
                return _currentUser;
            }
        }

        public static Int64? CurrentUserId
        {
            get
            {
                return _currentUserId;
            }
        }

        public static int Environment
        {
            get
            {
                return _environment;
            }
        }

        public static Guid? CurrentCompany
        {
            get
            {
                return _currentCompany;
            }
        }

        public static Guid? EconomicGroup
        {
            get
            {
                return _economicGroup;
            }
        }

        public static Guid? AuthorizationToken
        {
            get
            {
                if (_headers != null && _headers.ContainsKey(2))
                    return new Guid(_headers[2]);
                else
                    return null;
            }
        }

        public static Guid? AccessGroup
        {
            get
            {
                if (_headers != null && _headers.ContainsKey(4))
                    return new Guid(_headers[4]);
                else
                    return null;
            }
        }

        private static List<RelatorioInfo> _reportList = null;
        public static List<RelatorioInfo> ReportList
        {
            get
            {
                if (_reportList.IsNull())
                    _reportList = GetReports();

                return _reportList;
            }
        }

        #region Authentication

        private static IList<string> GetListOfOTPs(string base32EncodedSecret)
        {
            DateTime epochStart = new DateTime(1970, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc);
            DateTime refDate = DateTime.UtcNow;

            long counter = (long)Math.Floor((refDate - epochStart).TotalSeconds / 30);
            var otps = new List<string>();

            otps.Add(GetHotp(base32EncodedSecret, counter - 1)); // previous OTP
            otps.Add(GetHotp(base32EncodedSecret, counter)); // current OTP
            otps.Add(GetHotp(base32EncodedSecret, counter + 1)); // next OTP

            return otps;
        }

        private static string GetHotp(string base32EncodedSecret, long counter)
        {
            byte[] message = BitConverter.GetBytes(counter).Reverse().ToArray(); //Intel machine (little endian) 
            byte[] secret = base32EncodedSecret.ToByteArray();

            HMACSHA1 hmac = new HMACSHA1(secret, true);

            byte[] hash = hmac.ComputeHash(message);
            int offset = hash[hash.Length - 1] & 0xf;
            int truncatedHash = ((hash[offset] & 0x7f) << 24) |
            ((hash[offset + 1] & 0xff) << 16) |
            ((hash[offset + 2] & 0xff) << 8) |
            (hash[offset + 3] & 0xff);

            int hotp = truncatedHash % 1000000;
            return hotp.ToString().PadLeft(6, '0');
        }

        public static bool IsUserAuthorized(string user, string password)
        {
            if (_devMode)
                return true;

            return Enabled && !user.IsNullOrEmpty() && !password.IsNullOrEmpty() && user == _user && password == _password;
        }

        public static IList<string> GetSecurityKeys(string deviceId)
        {
            string secret = Base32Url.LxToBase32String(FingerPrint, deviceId);
            return GetListOfOTPs(secret);
        }

        private static string LxFromBase32String(string input)
        {
            var b32 = new Base32Url(true);
            return Encoding.ASCII.GetString(b32.Decode(input));
        }

        public static bool IsAuthorized(string deviceId, string encodedSecret)
        {
            if (_devMode)
                return true;

            //DeviceCode
            encodedSecret = LxFromBase32String(encodedSecret);

            var keys = GetSecurityKeys(deviceId);
            return Enabled && !deviceId.IsNullOrEmpty() && !encodedSecret.IsNullOrEmpty() && keys.Contains(encodedSecret);
        }

        public static AuthenticationResult GetAuthenticationByDevice(string deviceId, string encodedSecret)
        {
            if (IsAuthorized(deviceId, encodedSecret))
            {
                return new AuthenticationResult() { IsOk = true, Headers = _headers ?? new Dictionary<int, string>(), BusinessAddresses = _businessAddresses ?? new Dictionary<string, string>() };
            }
            else
                return new AuthenticationResult();
        }

        public static AuthenticationResult GetAuthenticationByUser(string user, string password)
        {
            if (_devMode)
                return new AuthenticationResult() { IsOk = true, BusinessAddresses = new Dictionary<string, string>(), Headers = new Dictionary<int, string>() };

            if (IsUserAuthorized(user, password))
            {
                return new AuthenticationResult() { IsOk = true, Headers = _headers ?? new Dictionary<int, string>(), BusinessAddresses = _businessAddresses ?? new Dictionary<string, string>() };
            }
            else
                return new AuthenticationResult();
        }

        private static List<RelatorioInfo> GetReports()
        {
            List<RelatorioInfo> reports = new List<RelatorioInfo>();

            if (_enabled)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_authenticationServer);
                    string serviceCall = "LinxReportAccessReportAccess/GetTelerikReportsFullList?cacheHash=null";
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.GetAsync(serviceCall).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        reports = Newtonsoft.Json.JsonConvert.DeserializeObject<List<RelatorioInfo>>(response.Content.ReadAsStringAsync().Result);
                    }
                }
            }

            return reports;
        }

        public static void Start()
        {
            if (_devMode)
            {
                IdLinx = int.Parse((System.Configuration.ConfigurationManager.AppSettings["IdLinx"] ?? "1"));
                IdGpecon = int.Parse((System.Configuration.ConfigurationManager.AppSettings["IdGpecon"] ?? "1"));
                IsUserMultiGpecon = bool.Parse((System.Configuration.ConfigurationManager.AppSettings["IsUserMultiGpecon"] ?? "true"));
                IsUserAdministrator = bool.Parse((System.Configuration.ConfigurationManager.AppSettings["IsUserAdministrator"] ?? "false"));
                ApplicativeId = int.Parse((System.Configuration.ConfigurationManager.AppSettings["IdTcsAplicativo"] ?? "1"));
                _currentUser = Guid.Parse((System.Configuration.ConfigurationManager.AppSettings["UserUid"] ?? "00000000-0000-0000-0000-000000000000"));
                _currentUserId = int.Parse((System.Configuration.ConfigurationManager.AppSettings["UserId"] ?? "0"));
                _environment = int.Parse((System.Configuration.ConfigurationManager.AppSettings["EnvironmentId"] ?? "1"));
                _currentCompany = Guid.Parse((System.Configuration.ConfigurationManager.AppSettings["CurrentCompany"] ?? "00000000-0000-0000-0000-000000000000"));
                _economicGroup = Guid.Parse((System.Configuration.ConfigurationManager.AppSettings["EconomicGroup"] ?? "00000000-0000-0000-0000-000000000000"));
            }
            else if (_enabled)
            {
                try
                {
                    var client = new HttpClient();
                    client.BaseAddress = new Uri(_authenticationServer);
                    string serviceCall = "Linx-Framework-BV-Autorizacao-AutorizacaoDomainService.svc/json/AuthenticateJson?userName=" + _user + "&password=" + _password + "&applicationId=" + _applicationId;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = client.GetAsync(serviceCall).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var headersResult = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<object, System.Collections.DictionaryEntry[]>>(response.Content.ReadAsStringAsync().Result);
                        if (headersResult.Values.Count > 0)
                        {
                            //Add returned headers
                            _headers = new Dictionary<int, string>();
                            foreach (var element in headersResult.Values.First())
                            {
                                _headers.Add(int.Parse(element.Key.ToString()), element.Value.IsNullOrEmpty() ? "" : element.Value.ToString());
                            }

                            //Add application into headers
                            //_headers.Add(8, _applicationId);
                            _applicationId = _headers[8];

                            if (_headers != null && _headers.ContainsKey(9) && !_headers[9].IsNullOrEmpty())
                            {
                                _authenticationServer = _headers[9] + (_headers[9].EndsWith("/") ? "" : "/");
                                client = new HttpClient();
                                client.BaseAddress = new Uri(_authenticationServer);
                                client.DefaultRequestHeaders.Accept.Clear();
                                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            }
                        }

                        if (_headers != null && _headers.ContainsKey(6))
                        {
                            if (_headers != null && _headers.ContainsKey(3))
                                _currentUser = new Guid(_headers[3]);

                            if (_headers != null && _headers.ContainsKey(7))
                                _currentUserId = Convert.ToInt64(_headers[7]);

                            if (_headers != null && _headers.ContainsKey(6))
                                _environment = int.Parse(_headers[6]);

                            if (_headers != null && _headers.ContainsKey(1))
                                _currentCompany = new Guid(_headers[1]);

                            if (_headers != null && _headers.ContainsKey(5))
                                _economicGroup = new Guid(_headers[5]);

                            //Getting business addresses
                            serviceCall = "LinxFrameworkAmbiente/GetAmbienteServicoExcecao";
                            client.DefaultRequestHeaders.Add("Environment", _headers[6]);
                            response = client.GetAsync(serviceCall).Result;
                            if (response.IsSuccessStatusCode)
                            {
                                _businessAddresses = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content.ReadAsStringAsync().Result);

                                //Getting Id Linx
                                serviceCall = "LinxFrameworkEmpresa/GetTcsEmpresaAutenticacaoNoAssociations?$filter=UidEmpresa eq (guid'" + CurrentCompany.ToString() + "')&$select=IdLinx";
                                response = client.GetAsync(serviceCall).Result;
                                if (response.IsSuccessStatusCode)
                                {
                                    var objResult = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<object, object>[]>(response.Content.ReadAsStringAsync().Result).FirstOrDefault();
                                    if (objResult != null && objResult.Values.Count > 0)
                                        IdLinx = int.Parse(objResult.Values.First().ToString());

                                    if (CurrentCompany.Equals(EconomicGroup))
                                    {
                                        IdGpecon = IdLinx;
                                    }
                                    else
                                    {
                                        //Getting Id Gpecon
                                        serviceCall = "LinxFrameworkEmpresa/GetTcsEmpresaAutenticacaoNoAssociations?$filter=UidEmpresa eq (guid'" + EconomicGroup.ToString() + "')&$select=IdLinx";
                                        response = client.GetAsync(serviceCall).Result;
                                        if (response.IsSuccessStatusCode)
                                        {
                                            objResult = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<object, object>[]>(response.Content.ReadAsStringAsync().Result).FirstOrDefault();
                                            if (objResult != null && objResult.Values.Count > 0)
                                                IdGpecon = int.Parse(objResult.Values.First().ToString());
                                        }
                                        else
                                        {
                                            ReportError(response);
                                            return;
                                        }
                                    }

                                    //Getting Id Aplicativo
                                    serviceCall = "LinxFrameworkAplicacao/GetTcsAplicacaoNoAssociations?$filter=UidAplicacao eq (guid'" + _applicationId + "')&$select=IdTcsAplicativo";
                                    response = client.GetAsync(serviceCall).Result;
                                    if (response.IsSuccessStatusCode)
                                    {
                                        objResult = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<object, object>[]>(response.Content.ReadAsStringAsync().Result).FirstOrDefault();
                                        if (objResult != null && objResult.Values.Count > 0)
                                            ApplicativeId = int.Parse(objResult.Values.First().ToString());
                                    }
                                    else
                                    {
                                        ReportError(response);
                                    }
                                }
                                else
                                {
                                    ReportError(response);
                                }

                            }
                            else
                            {
                                ReportError(response);
                            }
                        }

                    }
                    else
                    {
                        ReportError(response);
                    }
                }
                catch (Exception oException)
                {
                    ReportError(oException);
                }
            }
        }

        private static void ReportError(HttpResponseMessage response)
        {
            var responseContent = response.Content.ReadAsStringAsync();
            responseContent.Wait();
            var errorMessage = responseContent.Result;

            LogErrorFile(errorMessage);
        }

        private static void ReportError(Exception oException)
        {
            string errorMessage = string.Empty;
            var level = oException.InnerException;

            while (true)
            {
                errorMessage = errorMessage + (errorMessage.IsNullOrEmpty() ? "" : "\r\n") + level.Message;
                level = level.InnerException;

                if (level.IsNull())
                    break;
            }

            if (errorMessage.IsNullOrEmpty())
                errorMessage = oException.Message;

            LogErrorFile(errorMessage);
        }

        private static void LogErrorFile(string errorMessage)
        {
            _hasError = true;

            var directory = String.Empty;
            try
            {
                directory = HttpRuntime.BinDirectory;
            }
            catch
            {
                directory = AssemblyHelper.GetCurrentAssemblyDirectory<Linx.Tools.AuthenticationResult>();
            }

            if (Directory.Exists(directory))
            {
                File.WriteAllText(Path.Combine(directory, "..\\AuthenticationError.log"), errorMessage);
            }
            else throw new Exception(errorMessage);
        }

        #endregion

        static LocalServiceBus()
        {
            Hashtable config = System.Configuration.ConfigurationManager.GetSection("LocalServiceBusSettings") as Hashtable;
            if (config != null)
            {
                _authenticationServer = config["authenticationServer"] as string;

                //Add bar at the end, if does not exist.
                if (!_authenticationServer.IsNullOrEmpty() && _authenticationServer.Right(1) != "/")
                {
                    _authenticationServer += "/";
                }

                _user = config["user"] as string;
                _password = config["password"] as string;
                _applicationId = config["applicationId"] as string;
                var mode = (config["mode"] as string);
                _devMode = (!mode.IsNullOrEmpty() && mode.ToLower() == "dev");
            }
            else
            {
                _authenticationServer = String.Empty;
                _user = String.Empty;
                _password = String.Empty;
                _applicationId = String.Empty;
                _devMode = true;
            }
            _enabled = !_authenticationServer.IsNullOrEmpty() && !_user.IsNullOrEmpty() && !_password.IsNullOrEmpty() && !_applicationId.IsNullOrEmpty();
        }

        public static DateTime UNIX_EPOCH { get; set; }
    }

    public class AuthenticationResult
    {
        public bool IsOk { get; set; }
        public Dictionary<int, string> Headers { get; set; }
        public Dictionary<string, string> BusinessAddresses { get; set; }
    }

    public static class StringHelper
    {
        private static string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";

        public static string ToBase32String(this byte[] secret)
        {
            var bits = secret.Select(b => Convert.ToString(b, 2).PadLeft(8, '0')).Aggregate((a, b) => a + b);

            return Enumerable.Range(0, bits.Length / 5).Select(i => alphabet.Substring(Convert.ToInt32(bits.Substring(i * 5, 5), 2), 1)).Aggregate((a, b) => a + b);
        }

        public static byte[] ToByteArray(this string secret)
        {
            var bits = secret.ToUpper().ToCharArray().Select(c => Convert.ToString(alphabet.IndexOf(c), 2).PadLeft(5, '0')).Aggregate((a, b) => a + b);

            return Enumerable.Range(0, bits.Length / 8).Select(i => Convert.ToByte(bits.Substring(i * 8, 8), 2)).ToArray();
        }

    }

    [Serializable()]
    public class RelatorioInfo
    {
        [System.ComponentModel.DataAnnotations.Key()]
        public string IdRelatorio { get; set; }
        public string NomeRelatorio { get; set; }
        public string DescricaoRelatorio { get; set; }
        public string CaminhoRelatorio { get; set; }
    }
}


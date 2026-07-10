using Linx.Tools;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Framework.BV
{
    class LicenseException : Exception
    {
        public LicenseException(string message) : base(message)
        {
        }
    }

    class LicencaInfo
    {
        public Int64 IdLicencaUso { get; set; }
        public Byte LxStatusChave { get; set; }
        public int Peridiocidade { get; set; }
        public int DiasOffline { get; set; }
        public string Mensagem { get; set; }

    }

    class CustomerLicenseInfo
    {
        public byte LxStatusLicencaCliente { get; set; }
        public string LxStatusLicencaClienteName { get; set; }
        public int QtdeContratada { get; set; }
        public int QtdeEmUso { get; set; }
        public int DiasOffline { get; set; }
    }

    public static class LicenseControl
    {
        private static readonly Int64 idLicenca = Int64.Parse((ConfigurationManager.AppSettings["LicenseControl.LicenseByUserId"] ?? "8"));
        private static readonly bool licenseControlEnabled = bool.Parse((ConfigurationManager.AppSettings["LicenseControl.Enabled"] ?? "false")) && !LocalServiceBus.Enabled && idLicenca > 0;
        private static readonly string licenseServerUrl = ConfigurationManager.AppSettings["LicenseControl.LicenseServerUrl"] ?? "https://svc-licensing.linxsaas.com.br/";
        private static readonly string prefixoCache = "Lic";
        private static readonly string terminal = Environment.MachineName;

        private static IRestResponse ApiPost(RestRequest request)
        {
            var client = new RestSharp.RestClient(licenseServerUrl);
            var result = client.Post(request);

            if (result.ErrorException != null)
                throw new LicenseException(result.ErrorException.Message);

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
                throw new LicenseException(Linx.Tools.WebClientHelper.GetResponseErrorMessage(result.Content) ?? result.StatusDescription);

            return result;
        }

        private static void GetCustomerLicenseInfo(string cnpj, Guid uidEmpresa)
        {
            //verifica licenca no cache
            string cacheKey = string.Format("{0}_{1}", prefixoCache, cnpj);
            CustomerLicenseInfo customerLicenseInfo = WebCacheHelper.GetWebCache<CustomerLicenseInfo>(cacheKey);

            if (customerLicenseInfo.IsNullOrEmpty())
            {
                var razaoSocial = BusinessUserServiceHelper.GetCompanyName(uidEmpresa);

                RestSharp.RestRequest request = new RestSharp.RestRequest("/LinxLicenseServerLicenciamento/GetCustomerLicense", RestSharp.Method.POST)
                {
                    RequestFormat = DataFormat.Json
                };

                request.AddBody(new { IdLicenca = idLicenca, IdCliente = cnpj, Cnpj = cnpj, RazaoSocial = razaoSocial });
                var response = ApiPost(request);
                customerLicenseInfo = JsonConvert.DeserializeObject<CustomerLicenseInfo>(response.Content);
                WebCacheHelper.AddWebCache(cacheKey, customerLicenseInfo, 168); //Mantém por 7 dias
            }

        }

        public static void Validate(string chave, string usuario, Guid uidEmpresa)
        {
            if (licenseControlEnabled)
            {
                //Verifica chave no cache
                string cacheKey = string.Format("{0}_{1}_{2}", prefixoCache, chave, uidEmpresa);
                LicencaInfo licencaInfo = WebCacheHelper.GetWebCache<LicencaInfo>(cacheKey);

                if (licencaInfo.IsNullOrEmpty())
                {
                    string cnpj = BusinessUserServiceHelper.GetCompanyCnpj(uidEmpresa);

                    GetCustomerLicenseInfo(cnpj, uidEmpresa);

                    RestSharp.RestRequest request = new RestSharp.RestRequest("/LinxLicenseServerLicenciamento/ValidateLicense", RestSharp.Method.POST)
                    {
                        RequestFormat = DataFormat.Json
                    };

                    request.AddBody(new { IdLicenca = idLicenca, IdCliente = cnpj, Chave = chave, Usuario = usuario, Terminal = terminal });
                    var response = ApiPost(request);
                    licencaInfo = JsonConvert.DeserializeObject<LicencaInfo>(response.Content);

                    if (licencaInfo.LxStatusChave != 1)
                    {
                        throw new LicenseException("Erro na validação do Status da Licença.");
                    }

                    WebCacheHelper.AddWebCache(cacheKey, licencaInfo, 12);
                }
            }
        }

        public static void Remove(string chave, string usuario, Guid uidEmpresa)
        {
            if (licenseControlEnabled)
            {
                string cnpj = BusinessUserServiceHelper.GetCompanyCnpj(uidEmpresa);
                string cacheKey = string.Format("{0}_{1}_{2}", prefixoCache, chave, uidEmpresa);
                WebCacheHelper.RemoveWebCache(cacheKey);

                RestSharp.RestRequest request = new RestSharp.RestRequest("/LinxLicenseServerLicenciamento/RemoveLicense", RestSharp.Method.POST)
                {
                    RequestFormat = DataFormat.Json
                };

                request.AddBody(new { IdLicenca = idLicenca, IdCliente = cnpj, Chave = chave, Usuario = usuario, Terminal = terminal });
                var response = ApiPost(request);
            }
        }
    }
}

namespace Linx.Framework.BV.Reports {
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Linx.Tools;
    using Linx.Business.Tools;
    using Linx.Framework.BV.Empresa;
    using System.Net.Http;
    using System.Net.Http.Headers;
    
    
    public partial class EmpresaDataSource {
        
        private string url = "LinxFrameworkEmpresa/";
        
        public Linx.Framework.BV.Empresa.EmpresaDomainService _empresaContext;
        
        private string[] _detailsForLoading;
        
        public string[] DetailsForLoading {
            get {
                return this._detailsForLoading;
            }
            set {
                this._detailsForLoading = value;
            }
        }
        
        private string GetFilterExpression(Telerik.Reporting.Processing.Report report, System.Type entityType, string[] replacements) {

            var filters = LinxReportHelper.GetReportFilters(report);
            //Begin: Telerik filters treatment
            foreach (var filter in filters)
            {
                if (filter.Value.Split('.').GetValue(0).ToString() == "= Parameters")
                {
                    var parameterName = filter.Value.Split('.').GetValue(1).ToString();
                    filter.Value = report.Parameters[parameterName].Value.ToString();
                }
            }
            //End: Telerik filters treatment
            //Begin: Adjust translated query
            var preDefinedTranslatedJqueryExpression = (report.Parameters.ContainsKey("PreDefinedTranslatedJqueryExpression") ? report.Parameters["PreDefinedTranslatedJqueryExpression"].Value.ToString() : "");
            if (!preDefinedTranslatedJqueryExpression.IsNullOrEmpty())
            {
                var translatedJqueryExpression = (report.Parameters.ContainsKey("TranslatedJqueryExpression") ? report.Parameters["TranslatedJqueryExpression"].Value.ToString() : "");
                report.Parameters["TranslatedJqueryExpression"].Value = (translatedJqueryExpression + preDefinedTranslatedJqueryExpression).Replace(")(", " e ");
            }
            //End: Adjust translated query
            var jEntitySearch = LinxReportHelper.ConvertFilterToJExpression(filters, entityType);
            if (report.Parameters.ContainsKey("PreDefinedQueryExpression") && !report.Parameters["PreDefinedQueryExpression"].Value.IsNullOrEmpty())
            {
                jEntitySearch = report.Parameters["PreDefinedQueryExpression"].Value.ToString() + (jEntitySearch ?? "");
            }
            if (!report.Parameters["JqueryExpression"].Value.IsNullOrEmpty())
            {
                jEntitySearch = report.Parameters["JqueryExpression"].Value.ToString() + (jEntitySearch ?? "");
            }
            //Replace parent composition elements
            if (!jEntitySearch.IsNullOrEmpty() && replacements.Length > 0)
            {
                foreach (string value in replacements)
                {
                    jEntitySearch = jEntitySearch.Replace(value + "{", entityType.Name + "{");
                }
            }
            
            return jEntitySearch;
        }
        
        private IEnumerable<TcsEmpresaAutenticacao> GetLocalTcsEmpresaAutenticacao(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsEmpresaAutenticacao> result = default(IEnumerable<TcsEmpresaAutenticacao>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_empresaContext == null) _empresaContext = new Linx.Framework.BV.Empresa.EmpresaDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsEmpresaAutenticacao), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsEmpresaAutenticacao), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._empresaContext.GetTcsEmpresaAutenticacaoByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._empresaContext.GetTcsEmpresaAutenticacaoByEntitySearchNoAssociations(null).ToList();
            if (this.DetailsForLoading != null && this.DetailsForLoading.Length > 0)
            {
               foreach (var entity in result)
               {
                   entity.FillDetails(this._empresaContext, null, null, this.DetailsForLoading);
               }
            }
            return result;
        }
        
        public virtual IEnumerable<TcsEmpresaAutenticacao> GetTcsEmpresaAutenticacao(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsEmpresaAutenticacao> result = default(IEnumerable<TcsEmpresaAutenticacao>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsEmpresaAutenticacao(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsEmpresaAutenticacao?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
                    client.BaseAddress = new Uri(serviceBus + (serviceBus.Right(1) == "/" ? "" : "/") +  url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    if (!userName.Key.IsNullOrEmpty() && !password.Key.IsNullOrEmpty())
                    {
                        if (userName.Value.Value.IsNullOrEmpty() || password.Value.Value.IsNullOrEmpty())
                        {
                            report.Exception = new Exception("Usuário ou senha não informados.".Translate());
                            return result;
                        }
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsEmpresaAutenticacao), new string[] { });
                        serviceAddress = "GetTcsEmpresaAutenticacaoByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", userName.Value.Value, password.Value.Value))));
                    }
                    else
                    {
                        if (System.AppDomain.CurrentDomain.FriendlyName.Contains("Telerik.ReportDesigner"))
                        {
                            report.Exception = new Exception("Este relatório apenas pode ser visualizado pela aplicação Linx UX.".Translate());
                            return result;
                        }
                        else
                        {
                            client.DefaultRequestHeaders.Add("CurrentUser", "Developer");
                            client.DefaultRequestHeaders.Add("Application", "A9B8C7D6-E5F4-F4E6-D6C7-B8A9A9B8C7D6");
                            client.DefaultRequestHeaders.Add("CurrentCompany", "F27FFC4F-EB6E-4484-91ED-A318A4A394B0");
                        }
                    }
                
                    HttpResponseMessage response = client.GetAsync(serviceAddress).Result;
                    if (response.IsSuccessStatusCode)
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsEmpresaAutenticacao>>(response.Content.ReadAsStringAsync().Result);
                    else
                    {
                        var responseContent = response.Content.ReadAsStringAsync();
                        responseContent.Wait();
                        dynamic errorMessage = Newtonsoft.Json.Linq.JObject.Parse(responseContent.Result);
                        report.Exception = new Exception((string)errorMessage.ExceptionMessage);
                    }
                }
            }

            return result;
        }
        
        private IEnumerable<TcsEmpresaGpecon> GetLocalTcsEmpresaGpecon(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsEmpresaGpecon> result = default(IEnumerable<TcsEmpresaGpecon>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_empresaContext == null) _empresaContext = new Linx.Framework.BV.Empresa.EmpresaDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsEmpresaGpecon), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsEmpresaGpecon), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._empresaContext.GetTcsEmpresaGpeconByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._empresaContext.GetTcsEmpresaGpeconByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TcsEmpresaGpecon> GetTcsEmpresaGpecon(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsEmpresaGpecon> result = default(IEnumerable<TcsEmpresaGpecon>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsEmpresaGpecon(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsEmpresaGpecon?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
                    client.BaseAddress = new Uri(serviceBus + (serviceBus.Right(1) == "/" ? "" : "/") +  url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    if (!userName.Key.IsNullOrEmpty() && !password.Key.IsNullOrEmpty())
                    {
                        if (userName.Value.Value.IsNullOrEmpty() || password.Value.Value.IsNullOrEmpty())
                        {
                            report.Exception = new Exception("Usuário ou senha não informados.".Translate());
                            return result;
                        }
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsEmpresaGpecon), new string[] { });
                        serviceAddress = "GetTcsEmpresaGpeconByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", userName.Value.Value, password.Value.Value))));
                    }
                    else
                    {
                        if (System.AppDomain.CurrentDomain.FriendlyName.Contains("Telerik.ReportDesigner"))
                        {
                            report.Exception = new Exception("Este relatório apenas pode ser visualizado pela aplicação Linx UX.".Translate());
                            return result;
                        }
                        else
                        {
                            client.DefaultRequestHeaders.Add("CurrentUser", "Developer");
                            client.DefaultRequestHeaders.Add("Application", "A9B8C7D6-E5F4-F4E6-D6C7-B8A9A9B8C7D6");
                            client.DefaultRequestHeaders.Add("CurrentCompany", "F27FFC4F-EB6E-4484-91ED-A318A4A394B0");
                        }
                    }
                
                    HttpResponseMessage response = client.GetAsync(serviceAddress).Result;
                    if (response.IsSuccessStatusCode)
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsEmpresaGpecon>>(response.Content.ReadAsStringAsync().Result);
                    else
                    {
                        var responseContent = response.Content.ReadAsStringAsync();
                        responseContent.Wait();
                        dynamic errorMessage = Newtonsoft.Json.Linq.JObject.Parse(responseContent.Result);
                        report.Exception = new Exception((string)errorMessage.ExceptionMessage);
                    }
                }
            }

            return result;
        }
        
        private IEnumerable<TcsEmpresaGpeconParentComposition> GetLocalTcsEmpresaGpeconParentComposition(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsEmpresaGpeconParentComposition> result = default(IEnumerable<TcsEmpresaGpeconParentComposition>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_empresaContext == null) _empresaContext = new Linx.Framework.BV.Empresa.EmpresaDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsEmpresaGpeconParentComposition), new string[] {"TcsEmpresaAutenticacao"});
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsEmpresaGpeconParentComposition), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._empresaContext.GetTcsEmpresaGpeconParentCompositionByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._empresaContext.GetTcsEmpresaGpeconParentCompositionByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TcsEmpresaGpeconParentComposition> GetTcsEmpresaGpeconParentComposition(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsEmpresaGpeconParentComposition> result = default(IEnumerable<TcsEmpresaGpeconParentComposition>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsEmpresaGpeconParentComposition(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsEmpresaGpeconParentComposition?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
                    client.BaseAddress = new Uri(serviceBus + (serviceBus.Right(1) == "/" ? "" : "/") +  url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    if (!userName.Key.IsNullOrEmpty() && !password.Key.IsNullOrEmpty())
                    {
                        if (userName.Value.Value.IsNullOrEmpty() || password.Value.Value.IsNullOrEmpty())
                        {
                            report.Exception = new Exception("Usuário ou senha não informados.".Translate());
                            return result;
                        }
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsEmpresaGpeconParentComposition), new string[] {"TcsEmpresaAutenticacao"});
                        serviceAddress = "GetTcsEmpresaGpeconParentCompositionByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", userName.Value.Value, password.Value.Value))));
                    }
                    else
                    {
                        if (System.AppDomain.CurrentDomain.FriendlyName.Contains("Telerik.ReportDesigner"))
                        {
                            report.Exception = new Exception("Este relatório apenas pode ser visualizado pela aplicação Linx UX.".Translate());
                            return result;
                        }
                        else
                        {
                            client.DefaultRequestHeaders.Add("CurrentUser", "Developer");
                            client.DefaultRequestHeaders.Add("Application", "A9B8C7D6-E5F4-F4E6-D6C7-B8A9A9B8C7D6");
                            client.DefaultRequestHeaders.Add("CurrentCompany", "F27FFC4F-EB6E-4484-91ED-A318A4A394B0");
                        }
                    }
                
                    HttpResponseMessage response = client.GetAsync(serviceAddress).Result;
                    if (response.IsSuccessStatusCode)
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsEmpresaGpeconParentComposition>>(response.Content.ReadAsStringAsync().Result);
                    else
                    {
                        var responseContent = response.Content.ReadAsStringAsync();
                        responseContent.Wait();
                        dynamic errorMessage = Newtonsoft.Json.Linq.JObject.Parse(responseContent.Result);
                        report.Exception = new Exception((string)errorMessage.ExceptionMessage);
                    }
                }
            }

            return result;
        }
        
        private IEnumerable<TcsAmbiente> GetLocalTcsAmbiente(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsAmbiente> result = default(IEnumerable<TcsAmbiente>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_empresaContext == null) _empresaContext = new Linx.Framework.BV.Empresa.EmpresaDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsAmbiente), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsAmbiente), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._empresaContext.GetTcsAmbienteByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._empresaContext.GetTcsAmbienteByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TcsAmbiente> GetTcsAmbiente(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsAmbiente> result = default(IEnumerable<TcsAmbiente>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsAmbiente(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsAmbiente?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
                    client.BaseAddress = new Uri(serviceBus + (serviceBus.Right(1) == "/" ? "" : "/") +  url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    if (!userName.Key.IsNullOrEmpty() && !password.Key.IsNullOrEmpty())
                    {
                        if (userName.Value.Value.IsNullOrEmpty() || password.Value.Value.IsNullOrEmpty())
                        {
                            report.Exception = new Exception("Usuário ou senha não informados.".Translate());
                            return result;
                        }
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsAmbiente), new string[] { });
                        serviceAddress = "GetTcsAmbienteByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", userName.Value.Value, password.Value.Value))));
                    }
                    else
                    {
                        if (System.AppDomain.CurrentDomain.FriendlyName.Contains("Telerik.ReportDesigner"))
                        {
                            report.Exception = new Exception("Este relatório apenas pode ser visualizado pela aplicação Linx UX.".Translate());
                            return result;
                        }
                        else
                        {
                            client.DefaultRequestHeaders.Add("CurrentUser", "Developer");
                            client.DefaultRequestHeaders.Add("Application", "A9B8C7D6-E5F4-F4E6-D6C7-B8A9A9B8C7D6");
                            client.DefaultRequestHeaders.Add("CurrentCompany", "F27FFC4F-EB6E-4484-91ED-A318A4A394B0");
                        }
                    }
                
                    HttpResponseMessage response = client.GetAsync(serviceAddress).Result;
                    if (response.IsSuccessStatusCode)
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsAmbiente>>(response.Content.ReadAsStringAsync().Result);
                    else
                    {
                        var responseContent = response.Content.ReadAsStringAsync();
                        responseContent.Wait();
                        dynamic errorMessage = Newtonsoft.Json.Linq.JObject.Parse(responseContent.Result);
                        report.Exception = new Exception((string)errorMessage.ExceptionMessage);
                    }
                }
            }

            return result;
        }
        
        private IEnumerable<TcsAmbienteParentComposition> GetLocalTcsAmbienteParentComposition(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsAmbienteParentComposition> result = default(IEnumerable<TcsAmbienteParentComposition>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_empresaContext == null) _empresaContext = new Linx.Framework.BV.Empresa.EmpresaDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsAmbienteParentComposition), new string[] {"TcsEmpresaAutenticacao"});
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsAmbienteParentComposition), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._empresaContext.GetTcsAmbienteParentCompositionByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._empresaContext.GetTcsAmbienteParentCompositionByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TcsAmbienteParentComposition> GetTcsAmbienteParentComposition(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsAmbienteParentComposition> result = default(IEnumerable<TcsAmbienteParentComposition>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsAmbienteParentComposition(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsAmbienteParentComposition?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
                    client.BaseAddress = new Uri(serviceBus + (serviceBus.Right(1) == "/" ? "" : "/") +  url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    if (!userName.Key.IsNullOrEmpty() && !password.Key.IsNullOrEmpty())
                    {
                        if (userName.Value.Value.IsNullOrEmpty() || password.Value.Value.IsNullOrEmpty())
                        {
                            report.Exception = new Exception("Usuário ou senha não informados.".Translate());
                            return result;
                        }
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsAmbienteParentComposition), new string[] {"TcsEmpresaAutenticacao"});
                        serviceAddress = "GetTcsAmbienteParentCompositionByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", userName.Value.Value, password.Value.Value))));
                    }
                    else
                    {
                        if (System.AppDomain.CurrentDomain.FriendlyName.Contains("Telerik.ReportDesigner"))
                        {
                            report.Exception = new Exception("Este relatório apenas pode ser visualizado pela aplicação Linx UX.".Translate());
                            return result;
                        }
                        else
                        {
                            client.DefaultRequestHeaders.Add("CurrentUser", "Developer");
                            client.DefaultRequestHeaders.Add("Application", "A9B8C7D6-E5F4-F4E6-D6C7-B8A9A9B8C7D6");
                            client.DefaultRequestHeaders.Add("CurrentCompany", "F27FFC4F-EB6E-4484-91ED-A318A4A394B0");
                        }
                    }
                
                    HttpResponseMessage response = client.GetAsync(serviceAddress).Result;
                    if (response.IsSuccessStatusCode)
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsAmbienteParentComposition>>(response.Content.ReadAsStringAsync().Result);
                    else
                    {
                        var responseContent = response.Content.ReadAsStringAsync();
                        responseContent.Wait();
                        dynamic errorMessage = Newtonsoft.Json.Linq.JObject.Parse(responseContent.Result);
                        report.Exception = new Exception((string)errorMessage.ExceptionMessage);
                    }
                }
            }

            return result;
        }
        
        private IEnumerable<TcsEmpresaModulo> GetLocalTcsEmpresaModulo(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsEmpresaModulo> result = default(IEnumerable<TcsEmpresaModulo>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_empresaContext == null) _empresaContext = new Linx.Framework.BV.Empresa.EmpresaDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsEmpresaModulo), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsEmpresaModulo), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._empresaContext.GetTcsEmpresaModuloByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._empresaContext.GetTcsEmpresaModuloByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TcsEmpresaModulo> GetTcsEmpresaModulo(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsEmpresaModulo> result = default(IEnumerable<TcsEmpresaModulo>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsEmpresaModulo(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsEmpresaModulo?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
                    client.BaseAddress = new Uri(serviceBus + (serviceBus.Right(1) == "/" ? "" : "/") +  url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    if (!userName.Key.IsNullOrEmpty() && !password.Key.IsNullOrEmpty())
                    {
                        if (userName.Value.Value.IsNullOrEmpty() || password.Value.Value.IsNullOrEmpty())
                        {
                            report.Exception = new Exception("Usuário ou senha não informados.".Translate());
                            return result;
                        }
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsEmpresaModulo), new string[] { });
                        serviceAddress = "GetTcsEmpresaModuloByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", userName.Value.Value, password.Value.Value))));
                    }
                    else
                    {
                        if (System.AppDomain.CurrentDomain.FriendlyName.Contains("Telerik.ReportDesigner"))
                        {
                            report.Exception = new Exception("Este relatório apenas pode ser visualizado pela aplicação Linx UX.".Translate());
                            return result;
                        }
                        else
                        {
                            client.DefaultRequestHeaders.Add("CurrentUser", "Developer");
                            client.DefaultRequestHeaders.Add("Application", "A9B8C7D6-E5F4-F4E6-D6C7-B8A9A9B8C7D6");
                            client.DefaultRequestHeaders.Add("CurrentCompany", "F27FFC4F-EB6E-4484-91ED-A318A4A394B0");
                        }
                    }
                
                    HttpResponseMessage response = client.GetAsync(serviceAddress).Result;
                    if (response.IsSuccessStatusCode)
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsEmpresaModulo>>(response.Content.ReadAsStringAsync().Result);
                    else
                    {
                        var responseContent = response.Content.ReadAsStringAsync();
                        responseContent.Wait();
                        dynamic errorMessage = Newtonsoft.Json.Linq.JObject.Parse(responseContent.Result);
                        report.Exception = new Exception((string)errorMessage.ExceptionMessage);
                    }
                }
            }

            return result;
        }
        
        private IEnumerable<TcsEmpresaModuloParentComposition> GetLocalTcsEmpresaModuloParentComposition(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsEmpresaModuloParentComposition> result = default(IEnumerable<TcsEmpresaModuloParentComposition>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_empresaContext == null) _empresaContext = new Linx.Framework.BV.Empresa.EmpresaDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsEmpresaModuloParentComposition), new string[] {"TcsEmpresaAutenticacao"});
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsEmpresaModuloParentComposition), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._empresaContext.GetTcsEmpresaModuloParentCompositionByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._empresaContext.GetTcsEmpresaModuloParentCompositionByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TcsEmpresaModuloParentComposition> GetTcsEmpresaModuloParentComposition(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsEmpresaModuloParentComposition> result = default(IEnumerable<TcsEmpresaModuloParentComposition>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsEmpresaModuloParentComposition(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsEmpresaModuloParentComposition?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
                    client.BaseAddress = new Uri(serviceBus + (serviceBus.Right(1) == "/" ? "" : "/") +  url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    if (!userName.Key.IsNullOrEmpty() && !password.Key.IsNullOrEmpty())
                    {
                        if (userName.Value.Value.IsNullOrEmpty() || password.Value.Value.IsNullOrEmpty())
                        {
                            report.Exception = new Exception("Usuário ou senha não informados.".Translate());
                            return result;
                        }
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsEmpresaModuloParentComposition), new string[] {"TcsEmpresaAutenticacao"});
                        serviceAddress = "GetTcsEmpresaModuloParentCompositionByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", userName.Value.Value, password.Value.Value))));
                    }
                    else
                    {
                        if (System.AppDomain.CurrentDomain.FriendlyName.Contains("Telerik.ReportDesigner"))
                        {
                            report.Exception = new Exception("Este relatório apenas pode ser visualizado pela aplicação Linx UX.".Translate());
                            return result;
                        }
                        else
                        {
                            client.DefaultRequestHeaders.Add("CurrentUser", "Developer");
                            client.DefaultRequestHeaders.Add("Application", "A9B8C7D6-E5F4-F4E6-D6C7-B8A9A9B8C7D6");
                            client.DefaultRequestHeaders.Add("CurrentCompany", "F27FFC4F-EB6E-4484-91ED-A318A4A394B0");
                        }
                    }
                
                    HttpResponseMessage response = client.GetAsync(serviceAddress).Result;
                    if (response.IsSuccessStatusCode)
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsEmpresaModuloParentComposition>>(response.Content.ReadAsStringAsync().Result);
                    else
                    {
                        var responseContent = response.Content.ReadAsStringAsync();
                        responseContent.Wait();
                        dynamic errorMessage = Newtonsoft.Json.Linq.JObject.Parse(responseContent.Result);
                        report.Exception = new Exception((string)errorMessage.ExceptionMessage);
                    }
                }
            }

            return result;
        }
        
        private IEnumerable<TcsUsuarioAutenticacao> GetLocalTcsUsuarioAutenticacao(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsUsuarioAutenticacao> result = default(IEnumerable<TcsUsuarioAutenticacao>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_empresaContext == null) _empresaContext = new Linx.Framework.BV.Empresa.EmpresaDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsUsuarioAutenticacao), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsUsuarioAutenticacao), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._empresaContext.GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._empresaContext.GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacao(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsUsuarioAutenticacao> result = default(IEnumerable<TcsUsuarioAutenticacao>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsUsuarioAutenticacao(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsUsuarioAutenticacao?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
                    client.BaseAddress = new Uri(serviceBus + (serviceBus.Right(1) == "/" ? "" : "/") +  url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    if (!userName.Key.IsNullOrEmpty() && !password.Key.IsNullOrEmpty())
                    {
                        if (userName.Value.Value.IsNullOrEmpty() || password.Value.Value.IsNullOrEmpty())
                        {
                            report.Exception = new Exception("Usuário ou senha não informados.".Translate());
                            return result;
                        }
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsUsuarioAutenticacao), new string[] { });
                        serviceAddress = "GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", userName.Value.Value, password.Value.Value))));
                    }
                    else
                    {
                        if (System.AppDomain.CurrentDomain.FriendlyName.Contains("Telerik.ReportDesigner"))
                        {
                            report.Exception = new Exception("Este relatório apenas pode ser visualizado pela aplicação Linx UX.".Translate());
                            return result;
                        }
                        else
                        {
                            client.DefaultRequestHeaders.Add("CurrentUser", "Developer");
                            client.DefaultRequestHeaders.Add("Application", "A9B8C7D6-E5F4-F4E6-D6C7-B8A9A9B8C7D6");
                            client.DefaultRequestHeaders.Add("CurrentCompany", "F27FFC4F-EB6E-4484-91ED-A318A4A394B0");
                        }
                    }
                
                    HttpResponseMessage response = client.GetAsync(serviceAddress).Result;
                    if (response.IsSuccessStatusCode)
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsUsuarioAutenticacao>>(response.Content.ReadAsStringAsync().Result);
                    else
                    {
                        var responseContent = response.Content.ReadAsStringAsync();
                        responseContent.Wait();
                        dynamic errorMessage = Newtonsoft.Json.Linq.JObject.Parse(responseContent.Result);
                        report.Exception = new Exception((string)errorMessage.ExceptionMessage);
                    }
                }
            }

            return result;
        }
        
        private IEnumerable<TcsUsuarioAutenticacaoParentComposition> GetLocalTcsUsuarioAutenticacaoParentComposition(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsUsuarioAutenticacaoParentComposition> result = default(IEnumerable<TcsUsuarioAutenticacaoParentComposition>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_empresaContext == null) _empresaContext = new Linx.Framework.BV.Empresa.EmpresaDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsUsuarioAutenticacaoParentComposition), new string[] {"TcsEmpresaAutenticacao"});
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsUsuarioAutenticacaoParentComposition), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._empresaContext.GetTcsUsuarioAutenticacaoParentCompositionByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._empresaContext.GetTcsUsuarioAutenticacaoParentCompositionByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TcsUsuarioAutenticacaoParentComposition> GetTcsUsuarioAutenticacaoParentComposition(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsUsuarioAutenticacaoParentComposition> result = default(IEnumerable<TcsUsuarioAutenticacaoParentComposition>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsUsuarioAutenticacaoParentComposition(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsUsuarioAutenticacaoParentComposition?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
                    client.BaseAddress = new Uri(serviceBus + (serviceBus.Right(1) == "/" ? "" : "/") +  url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    if (!userName.Key.IsNullOrEmpty() && !password.Key.IsNullOrEmpty())
                    {
                        if (userName.Value.Value.IsNullOrEmpty() || password.Value.Value.IsNullOrEmpty())
                        {
                            report.Exception = new Exception("Usuário ou senha não informados.".Translate());
                            return result;
                        }
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsUsuarioAutenticacaoParentComposition), new string[] {"TcsEmpresaAutenticacao"});
                        serviceAddress = "GetTcsUsuarioAutenticacaoParentCompositionByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", userName.Value.Value, password.Value.Value))));
                    }
                    else
                    {
                        if (System.AppDomain.CurrentDomain.FriendlyName.Contains("Telerik.ReportDesigner"))
                        {
                            report.Exception = new Exception("Este relatório apenas pode ser visualizado pela aplicação Linx UX.".Translate());
                            return result;
                        }
                        else
                        {
                            client.DefaultRequestHeaders.Add("CurrentUser", "Developer");
                            client.DefaultRequestHeaders.Add("Application", "A9B8C7D6-E5F4-F4E6-D6C7-B8A9A9B8C7D6");
                            client.DefaultRequestHeaders.Add("CurrentCompany", "F27FFC4F-EB6E-4484-91ED-A318A4A394B0");
                        }
                    }
                
                    HttpResponseMessage response = client.GetAsync(serviceAddress).Result;
                    if (response.IsSuccessStatusCode)
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsUsuarioAutenticacaoParentComposition>>(response.Content.ReadAsStringAsync().Result);
                    else
                    {
                        var responseContent = response.Content.ReadAsStringAsync();
                        responseContent.Wait();
                        dynamic errorMessage = Newtonsoft.Json.Linq.JObject.Parse(responseContent.Result);
                        report.Exception = new Exception((string)errorMessage.ExceptionMessage);
                    }
                }
            }

            return result;
        }
        
        private IEnumerable<TcsEmpresaGpeconP> GetLocalTcsEmpresaGpeconP(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsEmpresaGpeconP> result = default(IEnumerable<TcsEmpresaGpeconP>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_empresaContext == null) _empresaContext = new Linx.Framework.BV.Empresa.EmpresaDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsEmpresaGpeconP), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsEmpresaGpeconP), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._empresaContext.GetTcsEmpresaGpeconPByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._empresaContext.GetTcsEmpresaGpeconPByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TcsEmpresaGpeconP> GetTcsEmpresaGpeconP(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsEmpresaGpeconP> result = default(IEnumerable<TcsEmpresaGpeconP>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsEmpresaGpeconP(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsEmpresaGpeconP?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
                    client.BaseAddress = new Uri(serviceBus + (serviceBus.Right(1) == "/" ? "" : "/") +  url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    if (!userName.Key.IsNullOrEmpty() && !password.Key.IsNullOrEmpty())
                    {
                        if (userName.Value.Value.IsNullOrEmpty() || password.Value.Value.IsNullOrEmpty())
                        {
                            report.Exception = new Exception("Usuário ou senha não informados.".Translate());
                            return result;
                        }
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsEmpresaGpeconP), new string[] { });
                        serviceAddress = "GetTcsEmpresaGpeconPByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", userName.Value.Value, password.Value.Value))));
                    }
                    else
                    {
                        if (System.AppDomain.CurrentDomain.FriendlyName.Contains("Telerik.ReportDesigner"))
                        {
                            report.Exception = new Exception("Este relatório apenas pode ser visualizado pela aplicação Linx UX.".Translate());
                            return result;
                        }
                        else
                        {
                            client.DefaultRequestHeaders.Add("CurrentUser", "Developer");
                            client.DefaultRequestHeaders.Add("Application", "A9B8C7D6-E5F4-F4E6-D6C7-B8A9A9B8C7D6");
                            client.DefaultRequestHeaders.Add("CurrentCompany", "F27FFC4F-EB6E-4484-91ED-A318A4A394B0");
                        }
                    }
                
                    HttpResponseMessage response = client.GetAsync(serviceAddress).Result;
                    if (response.IsSuccessStatusCode)
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsEmpresaGpeconP>>(response.Content.ReadAsStringAsync().Result);
                    else
                    {
                        var responseContent = response.Content.ReadAsStringAsync();
                        responseContent.Wait();
                        dynamic errorMessage = Newtonsoft.Json.Linq.JObject.Parse(responseContent.Result);
                        report.Exception = new Exception((string)errorMessage.ExceptionMessage);
                    }
                }
            }

            return result;
        }
        
        private IEnumerable<TcsEmpresaAutenticacaoP> GetLocalTcsEmpresaAutenticacaoP(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsEmpresaAutenticacaoP> result = default(IEnumerable<TcsEmpresaAutenticacaoP>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_empresaContext == null) _empresaContext = new Linx.Framework.BV.Empresa.EmpresaDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsEmpresaAutenticacaoP), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsEmpresaAutenticacaoP), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._empresaContext.GetTcsEmpresaAutenticacaoPByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._empresaContext.GetTcsEmpresaAutenticacaoPByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TcsEmpresaAutenticacaoP> GetTcsEmpresaAutenticacaoP(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsEmpresaAutenticacaoP> result = default(IEnumerable<TcsEmpresaAutenticacaoP>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsEmpresaAutenticacaoP(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsEmpresaAutenticacaoP?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
                    client.BaseAddress = new Uri(serviceBus + (serviceBus.Right(1) == "/" ? "" : "/") +  url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    if (!userName.Key.IsNullOrEmpty() && !password.Key.IsNullOrEmpty())
                    {
                        if (userName.Value.Value.IsNullOrEmpty() || password.Value.Value.IsNullOrEmpty())
                        {
                            report.Exception = new Exception("Usuário ou senha não informados.".Translate());
                            return result;
                        }
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsEmpresaAutenticacaoP), new string[] { });
                        serviceAddress = "GetTcsEmpresaAutenticacaoPByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", userName.Value.Value, password.Value.Value))));
                    }
                    else
                    {
                        if (System.AppDomain.CurrentDomain.FriendlyName.Contains("Telerik.ReportDesigner"))
                        {
                            report.Exception = new Exception("Este relatório apenas pode ser visualizado pela aplicação Linx UX.".Translate());
                            return result;
                        }
                        else
                        {
                            client.DefaultRequestHeaders.Add("CurrentUser", "Developer");
                            client.DefaultRequestHeaders.Add("Application", "A9B8C7D6-E5F4-F4E6-D6C7-B8A9A9B8C7D6");
                            client.DefaultRequestHeaders.Add("CurrentCompany", "F27FFC4F-EB6E-4484-91ED-A318A4A394B0");
                        }
                    }
                
                    HttpResponseMessage response = client.GetAsync(serviceAddress).Result;
                    if (response.IsSuccessStatusCode)
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsEmpresaAutenticacaoP>>(response.Content.ReadAsStringAsync().Result);
                    else
                    {
                        var responseContent = response.Content.ReadAsStringAsync();
                        responseContent.Wait();
                        dynamic errorMessage = Newtonsoft.Json.Linq.JObject.Parse(responseContent.Result);
                        report.Exception = new Exception((string)errorMessage.ExceptionMessage);
                    }
                }
            }

            return result;
        }
    }
}

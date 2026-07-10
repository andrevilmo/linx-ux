namespace Linx.Framework.BV.Reports {
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Linx.Tools;
    using Linx.Business.Tools;
    using Linx.Framework.BV.Parametro;
    using System.Net.Http;
    using System.Net.Http.Headers;
    
    
    public partial class ParametroDataSource {
        
        private string url = "LinxFrameworkParametro/";
        
        public Linx.Framework.BV.Parametro.ParametroDomainService _parametroContext;
        
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
        
        private IEnumerable<TcsParametro> GetLocalTcsParametro(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsParametro> result = default(IEnumerable<TcsParametro>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_parametroContext == null) _parametroContext = new Linx.Framework.BV.Parametro.ParametroDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsParametro), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsParametro), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._parametroContext.GetTcsParametroByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._parametroContext.GetTcsParametroByEntitySearchNoAssociations(null).ToList();
            if (this.DetailsForLoading != null && this.DetailsForLoading.Length > 0)
            {
               foreach (var entity in result)
               {
                   entity.FillDetails(this._parametroContext, null, null, this.DetailsForLoading);
               }
            }
            return result;
        }
        
        public virtual IEnumerable<TcsParametro> GetTcsParametro(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsParametro> result = default(IEnumerable<TcsParametro>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsParametro(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsParametro?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsParametro), new string[] { });
                        serviceAddress = "GetTcsParametroByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsParametro>>(response.Content.ReadAsStringAsync().Result);
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
        
        private IEnumerable<TcsParametroValorP> GetLocalTcsParametroValorP(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsParametroValorP> result = default(IEnumerable<TcsParametroValorP>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_parametroContext == null) _parametroContext = new Linx.Framework.BV.Parametro.ParametroDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsParametroValorP), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsParametroValorP), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._parametroContext.GetTcsParametroValorPByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._parametroContext.GetTcsParametroValorPByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TcsParametroValorP> GetTcsParametroValorP(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsParametroValorP> result = default(IEnumerable<TcsParametroValorP>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsParametroValorP(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsParametroValorP?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsParametroValorP), new string[] { });
                        serviceAddress = "GetTcsParametroValorPByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsParametroValorP>>(response.Content.ReadAsStringAsync().Result);
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
        
        private IEnumerable<ParametroValor> GetLocalParametroValor(Telerik.Reporting.Processing.Report report) {
            IEnumerable<ParametroValor> result = default(IEnumerable<ParametroValor>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_parametroContext == null) _parametroContext = new Linx.Framework.BV.Parametro.ParametroDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(ParametroValor), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(ParametroValor), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._parametroContext.GetParametroValorByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._parametroContext.GetParametroValorByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<ParametroValor> GetParametroValor(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<ParametroValor> result = default(IEnumerable<ParametroValor>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalParametroValor(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleParametroValor?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(ParametroValor), new string[] { });
                        serviceAddress = "GetParametroValorByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<ParametroValor>>(response.Content.ReadAsStringAsync().Result);
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
        
        private IEnumerable<TcsParametroValorVariacaoP> GetLocalTcsParametroValorVariacaoP(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsParametroValorVariacaoP> result = default(IEnumerable<TcsParametroValorVariacaoP>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_parametroContext == null) _parametroContext = new Linx.Framework.BV.Parametro.ParametroDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsParametroValorVariacaoP), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsParametroValorVariacaoP), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._parametroContext.GetTcsParametroValorVariacaoPByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._parametroContext.GetTcsParametroValorVariacaoPByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TcsParametroValorVariacaoP> GetTcsParametroValorVariacaoP(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsParametroValorVariacaoP> result = default(IEnumerable<TcsParametroValorVariacaoP>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsParametroValorVariacaoP(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsParametroValorVariacaoP?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsParametroValorVariacaoP), new string[] { });
                        serviceAddress = "GetTcsParametroValorVariacaoPByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsParametroValorVariacaoP>>(response.Content.ReadAsStringAsync().Result);
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
        
        private IEnumerable<TcsParametroTabelaSelecao> GetLocalTcsParametroTabelaSelecao(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsParametroTabelaSelecao> result = default(IEnumerable<TcsParametroTabelaSelecao>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_parametroContext == null) _parametroContext = new Linx.Framework.BV.Parametro.ParametroDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsParametroTabelaSelecao), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsParametroTabelaSelecao), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._parametroContext.GetTcsParametroTabelaSelecaoByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._parametroContext.GetTcsParametroTabelaSelecaoByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TcsParametroTabelaSelecao> GetTcsParametroTabelaSelecao(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsParametroTabelaSelecao> result = default(IEnumerable<TcsParametroTabelaSelecao>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsParametroTabelaSelecao(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsParametroTabelaSelecao?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsParametroTabelaSelecao), new string[] { });
                        serviceAddress = "GetTcsParametroTabelaSelecaoByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsParametroTabelaSelecao>>(response.Content.ReadAsStringAsync().Result);
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
        
        private IEnumerable<TcsParametroValor> GetLocalTcsParametroValor(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsParametroValor> result = default(IEnumerable<TcsParametroValor>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_parametroContext == null) _parametroContext = new Linx.Framework.BV.Parametro.ParametroDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsParametroValor), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsParametroValor), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._parametroContext.GetTcsParametroValorByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._parametroContext.GetTcsParametroValorByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TcsParametroValor> GetTcsParametroValor(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsParametroValor> result = default(IEnumerable<TcsParametroValor>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsParametroValor(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsParametroValor?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsParametroValor), new string[] { });
                        serviceAddress = "GetTcsParametroValorByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsParametroValor>>(response.Content.ReadAsStringAsync().Result);
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
        
        private IEnumerable<ParametroValorVariacao> GetLocalParametroValorVariacao(Telerik.Reporting.Processing.Report report) {
            IEnumerable<ParametroValorVariacao> result = default(IEnumerable<ParametroValorVariacao>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_parametroContext == null) _parametroContext = new Linx.Framework.BV.Parametro.ParametroDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(ParametroValorVariacao), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(ParametroValorVariacao), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._parametroContext.GetParametroValorVariacaoByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._parametroContext.GetParametroValorVariacaoByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<ParametroValorVariacao> GetParametroValorVariacao(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<ParametroValorVariacao> result = default(IEnumerable<ParametroValorVariacao>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalParametroValorVariacao(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleParametroValorVariacao?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(ParametroValorVariacao), new string[] { });
                        serviceAddress = "GetParametroValorVariacaoByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<ParametroValorVariacao>>(response.Content.ReadAsStringAsync().Result);
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
        
        private IEnumerable<TcsUsuarioParametro> GetLocalTcsUsuarioParametro(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsUsuarioParametro> result = default(IEnumerable<TcsUsuarioParametro>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_parametroContext == null) _parametroContext = new Linx.Framework.BV.Parametro.ParametroDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsUsuarioParametro), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsUsuarioParametro), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._parametroContext.GetTcsUsuarioParametroByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._parametroContext.GetTcsUsuarioParametroByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TcsUsuarioParametro> GetTcsUsuarioParametro(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsUsuarioParametro> result = default(IEnumerable<TcsUsuarioParametro>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsUsuarioParametro(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsUsuarioParametro?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsUsuarioParametro), new string[] { });
                        serviceAddress = "GetTcsUsuarioParametroByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsUsuarioParametro>>(response.Content.ReadAsStringAsync().Result);
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
        
        private IEnumerable<TcsParametroValorUsuario> GetLocalTcsParametroValorUsuario(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsParametroValorUsuario> result = default(IEnumerable<TcsParametroValorUsuario>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_parametroContext == null) _parametroContext = new Linx.Framework.BV.Parametro.ParametroDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsParametroValorUsuario), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsParametroValorUsuario), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._parametroContext.GetTcsParametroValorUsuarioByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._parametroContext.GetTcsParametroValorUsuarioByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TcsParametroValorUsuario> GetTcsParametroValorUsuario(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsParametroValorUsuario> result = default(IEnumerable<TcsParametroValorUsuario>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsParametroValorUsuario(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsParametroValorUsuario?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsParametroValorUsuario), new string[] { });
                        serviceAddress = "GetTcsParametroValorUsuarioByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsParametroValorUsuario>>(response.Content.ReadAsStringAsync().Result);
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
        
        private IEnumerable<TbcBandeiraRedeParametro> GetLocalTbcBandeiraRedeParametro(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TbcBandeiraRedeParametro> result = default(IEnumerable<TbcBandeiraRedeParametro>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_parametroContext == null) _parametroContext = new Linx.Framework.BV.Parametro.ParametroDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TbcBandeiraRedeParametro), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TbcBandeiraRedeParametro), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._parametroContext.GetTbcBandeiraRedeParametroByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._parametroContext.GetTbcBandeiraRedeParametroByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TbcBandeiraRedeParametro> GetTbcBandeiraRedeParametro(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TbcBandeiraRedeParametro> result = default(IEnumerable<TbcBandeiraRedeParametro>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTbcBandeiraRedeParametro(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTbcBandeiraRedeParametro?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(TbcBandeiraRedeParametro), new string[] { });
                        serviceAddress = "GetTbcBandeiraRedeParametroByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TbcBandeiraRedeParametro>>(response.Content.ReadAsStringAsync().Result);
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
        
        private IEnumerable<TcsParametroValorRede> GetLocalTcsParametroValorRede(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsParametroValorRede> result = default(IEnumerable<TcsParametroValorRede>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_parametroContext == null) _parametroContext = new Linx.Framework.BV.Parametro.ParametroDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsParametroValorRede), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsParametroValorRede), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._parametroContext.GetTcsParametroValorRedeByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._parametroContext.GetTcsParametroValorRedeByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TcsParametroValorRede> GetTcsParametroValorRede(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsParametroValorRede> result = default(IEnumerable<TcsParametroValorRede>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsParametroValorRede(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsParametroValorRede?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsParametroValorRede), new string[] { });
                        serviceAddress = "GetTcsParametroValorRedeByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsParametroValorRede>>(response.Content.ReadAsStringAsync().Result);
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
        
        private IEnumerable<TbcGrupoEconomicoParametro> GetLocalTbcGrupoEconomicoParametro(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TbcGrupoEconomicoParametro> result = default(IEnumerable<TbcGrupoEconomicoParametro>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_parametroContext == null) _parametroContext = new Linx.Framework.BV.Parametro.ParametroDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TbcGrupoEconomicoParametro), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TbcGrupoEconomicoParametro), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._parametroContext.GetTbcGrupoEconomicoParametroByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._parametroContext.GetTbcGrupoEconomicoParametroByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TbcGrupoEconomicoParametro> GetTbcGrupoEconomicoParametro(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TbcGrupoEconomicoParametro> result = default(IEnumerable<TbcGrupoEconomicoParametro>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTbcGrupoEconomicoParametro(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTbcGrupoEconomicoParametro?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(TbcGrupoEconomicoParametro), new string[] { });
                        serviceAddress = "GetTbcGrupoEconomicoParametroByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TbcGrupoEconomicoParametro>>(response.Content.ReadAsStringAsync().Result);
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
        
        private IEnumerable<TcsParametroValorGpecon> GetLocalTcsParametroValorGpecon(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsParametroValorGpecon> result = default(IEnumerable<TcsParametroValorGpecon>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_parametroContext == null) _parametroContext = new Linx.Framework.BV.Parametro.ParametroDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsParametroValorGpecon), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsParametroValorGpecon), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._parametroContext.GetTcsParametroValorGpeconByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._parametroContext.GetTcsParametroValorGpeconByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TcsParametroValorGpecon> GetTcsParametroValorGpecon(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsParametroValorGpecon> result = default(IEnumerable<TcsParametroValorGpecon>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsParametroValorGpecon(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsParametroValorGpecon?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsParametroValorGpecon), new string[] { });
                        serviceAddress = "GetTcsParametroValorGpeconByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsParametroValorGpecon>>(response.Content.ReadAsStringAsync().Result);
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
        
        private IEnumerable<TbcFilialParametro> GetLocalTbcFilialParametro(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TbcFilialParametro> result = default(IEnumerable<TbcFilialParametro>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_parametroContext == null) _parametroContext = new Linx.Framework.BV.Parametro.ParametroDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TbcFilialParametro), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TbcFilialParametro), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._parametroContext.GetTbcFilialParametroByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._parametroContext.GetTbcFilialParametroByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TbcFilialParametro> GetTbcFilialParametro(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TbcFilialParametro> result = default(IEnumerable<TbcFilialParametro>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTbcFilialParametro(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTbcFilialParametro?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(TbcFilialParametro), new string[] { });
                        serviceAddress = "GetTbcFilialParametroByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TbcFilialParametro>>(response.Content.ReadAsStringAsync().Result);
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
        
        private IEnumerable<TcsParametroValorFilial> GetLocalTcsParametroValorFilial(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsParametroValorFilial> result = default(IEnumerable<TcsParametroValorFilial>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_parametroContext == null) _parametroContext = new Linx.Framework.BV.Parametro.ParametroDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsParametroValorFilial), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsParametroValorFilial), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._parametroContext.GetTcsParametroValorFilialByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._parametroContext.GetTcsParametroValorFilialByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TcsParametroValorFilial> GetTcsParametroValorFilial(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsParametroValorFilial> result = default(IEnumerable<TcsParametroValorFilial>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsParametroValorFilial(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsParametroValorFilial?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsParametroValorFilial), new string[] { });
                        serviceAddress = "GetTcsParametroValorFilialByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsParametroValorFilial>>(response.Content.ReadAsStringAsync().Result);
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
        
        private IEnumerable<TcsParametroValorLjvLoja> GetLocalTcsParametroValorLjvLoja(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsParametroValorLjvLoja> result = default(IEnumerable<TcsParametroValorLjvLoja>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_parametroContext == null) _parametroContext = new Linx.Framework.BV.Parametro.ParametroDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsParametroValorLjvLoja), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsParametroValorLjvLoja), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._parametroContext.GetTcsParametroValorLjvLojaByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._parametroContext.GetTcsParametroValorLjvLojaByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TcsParametroValorLjvLoja> GetTcsParametroValorLjvLoja(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsParametroValorLjvLoja> result = default(IEnumerable<TcsParametroValorLjvLoja>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsParametroValorLjvLoja(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsParametroValorLjvLoja?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsParametroValorLjvLoja), new string[] { });
                        serviceAddress = "GetTcsParametroValorLjvLojaByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsParametroValorLjvLoja>>(response.Content.ReadAsStringAsync().Result);
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
        
        private IEnumerable<TcsParametroValorVariacaoGenericaP> GetLocalTcsParametroValorVariacaoGenericaP(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsParametroValorVariacaoGenericaP> result = default(IEnumerable<TcsParametroValorVariacaoGenericaP>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_parametroContext == null) _parametroContext = new Linx.Framework.BV.Parametro.ParametroDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsParametroValorVariacaoGenericaP), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsParametroValorVariacaoGenericaP), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._parametroContext.GetTcsParametroValorVariacaoGenericaPByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._parametroContext.GetTcsParametroValorVariacaoGenericaPByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TcsParametroValorVariacaoGenericaP> GetTcsParametroValorVariacaoGenericaP(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsParametroValorVariacaoGenericaP> result = default(IEnumerable<TcsParametroValorVariacaoGenericaP>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsParametroValorVariacaoGenericaP(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsParametroValorVariacaoGenericaP?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsParametroValorVariacaoGenericaP), new string[] { });
                        serviceAddress = "GetTcsParametroValorVariacaoGenericaPByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsParametroValorVariacaoGenericaP>>(response.Content.ReadAsStringAsync().Result);
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
        
        private IEnumerable<TcsParametroValorVariacaoGenerica> GetLocalTcsParametroValorVariacaoGenerica(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsParametroValorVariacaoGenerica> result = default(IEnumerable<TcsParametroValorVariacaoGenerica>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_parametroContext == null) _parametroContext = new Linx.Framework.BV.Parametro.ParametroDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsParametroValorVariacaoGenerica), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsParametroValorVariacaoGenerica), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._parametroContext.GetTcsParametroValorVariacaoGenericaByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._parametroContext.GetTcsParametroValorVariacaoGenericaByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TcsParametroValorVariacaoGenerica> GetTcsParametroValorVariacaoGenerica(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsParametroValorVariacaoGenerica> result = default(IEnumerable<TcsParametroValorVariacaoGenerica>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsParametroValorVariacaoGenerica(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsParametroValorVariacaoGenerica?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsParametroValorVariacaoGenerica), new string[] { });
                        serviceAddress = "GetTcsParametroValorVariacaoGenericaByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsParametroValorVariacaoGenerica>>(response.Content.ReadAsStringAsync().Result);
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
        
        private IEnumerable<TcsParametroValorP1> GetLocalTcsParametroValorP1(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsParametroValorP1> result = default(IEnumerable<TcsParametroValorP1>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_parametroContext == null) _parametroContext = new Linx.Framework.BV.Parametro.ParametroDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsParametroValorP1), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsParametroValorP1), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._parametroContext.GetTcsParametroValorP1ByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._parametroContext.GetTcsParametroValorP1ByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TcsParametroValorP1> GetTcsParametroValorP1(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsParametroValorP1> result = default(IEnumerable<TcsParametroValorP1>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsParametroValorP1(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsParametroValorP1?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsParametroValorP1), new string[] { });
                        serviceAddress = "GetTcsParametroValorP1ByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsParametroValorP1>>(response.Content.ReadAsStringAsync().Result);
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
        
        private IEnumerable<ParametroInfo> GetLocalParametroInfo(Telerik.Reporting.Processing.Report report) {
            IEnumerable<ParametroInfo> result = default(IEnumerable<ParametroInfo>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_parametroContext == null) _parametroContext = new Linx.Framework.BV.Parametro.ParametroDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(ParametroInfo), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(ParametroInfo), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._parametroContext.GetParametroInfoByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._parametroContext.GetParametroInfoByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<ParametroInfo> GetParametroInfo(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<ParametroInfo> result = default(IEnumerable<ParametroInfo>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalParametroInfo(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleParametroInfo?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(ParametroInfo), new string[] { });
                        serviceAddress = "GetParametroInfoByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<ParametroInfo>>(response.Content.ReadAsStringAsync().Result);
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
        
        private IEnumerable<TcsParametroValorP2> GetLocalTcsParametroValorP2(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsParametroValorP2> result = default(IEnumerable<TcsParametroValorP2>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_parametroContext == null) _parametroContext = new Linx.Framework.BV.Parametro.ParametroDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsParametroValorP2), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsParametroValorP2), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._parametroContext.GetTcsParametroValorP2ByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._parametroContext.GetTcsParametroValorP2ByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TcsParametroValorP2> GetTcsParametroValorP2(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsParametroValorP2> result = default(IEnumerable<TcsParametroValorP2>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsParametroValorP2(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsParametroValorP2?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsParametroValorP2), new string[] { });
                        serviceAddress = "GetTcsParametroValorP2ByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsParametroValorP2>>(response.Content.ReadAsStringAsync().Result);
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
        
        private IEnumerable<TcsParametroValorLojaP> GetLocalTcsParametroValorLojaP(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsParametroValorLojaP> result = default(IEnumerable<TcsParametroValorLojaP>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_parametroContext == null) _parametroContext = new Linx.Framework.BV.Parametro.ParametroDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsParametroValorLojaP), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsParametroValorLojaP), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._parametroContext.GetTcsParametroValorLojaPByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._parametroContext.GetTcsParametroValorLojaPByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TcsParametroValorLojaP> GetTcsParametroValorLojaP(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsParametroValorLojaP> result = default(IEnumerable<TcsParametroValorLojaP>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsParametroValorLojaP(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsParametroValorLojaP?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsParametroValorLojaP), new string[] { });
                        serviceAddress = "GetTcsParametroValorLojaPByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsParametroValorLojaP>>(response.Content.ReadAsStringAsync().Result);
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
        
        private IEnumerable<LjvLojaParametro> GetLocalLjvLojaParametro(Telerik.Reporting.Processing.Report report) {
            IEnumerable<LjvLojaParametro> result = default(IEnumerable<LjvLojaParametro>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_parametroContext == null) _parametroContext = new Linx.Framework.BV.Parametro.ParametroDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(LjvLojaParametro), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(LjvLojaParametro), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._parametroContext.GetLjvLojaParametroByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._parametroContext.GetLjvLojaParametroByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<LjvLojaParametro> GetLjvLojaParametro(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<LjvLojaParametro> result = default(IEnumerable<LjvLojaParametro>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalLjvLojaParametro(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleLjvLojaParametro?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(LjvLojaParametro), new string[] { });
                        serviceAddress = "GetLjvLojaParametroByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<LjvLojaParametro>>(response.Content.ReadAsStringAsync().Result);
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
        
        private IEnumerable<TcsParametroValorUsuarioP> GetLocalTcsParametroValorUsuarioP(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsParametroValorUsuarioP> result = default(IEnumerable<TcsParametroValorUsuarioP>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_parametroContext == null) _parametroContext = new Linx.Framework.BV.Parametro.ParametroDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsParametroValorUsuarioP), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsParametroValorUsuarioP), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._parametroContext.GetTcsParametroValorUsuarioPByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._parametroContext.GetTcsParametroValorUsuarioPByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TcsParametroValorUsuarioP> GetTcsParametroValorUsuarioP(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsParametroValorUsuarioP> result = default(IEnumerable<TcsParametroValorUsuarioP>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsParametroValorUsuarioP(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsParametroValorUsuarioP?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsParametroValorUsuarioP), new string[] { });
                        serviceAddress = "GetTcsParametroValorUsuarioPByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsParametroValorUsuarioP>>(response.Content.ReadAsStringAsync().Result);
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
        
        private IEnumerable<TcsParametroValorRedeP> GetLocalTcsParametroValorRedeP(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsParametroValorRedeP> result = default(IEnumerable<TcsParametroValorRedeP>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_parametroContext == null) _parametroContext = new Linx.Framework.BV.Parametro.ParametroDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsParametroValorRedeP), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsParametroValorRedeP), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._parametroContext.GetTcsParametroValorRedePByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._parametroContext.GetTcsParametroValorRedePByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TcsParametroValorRedeP> GetTcsParametroValorRedeP(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsParametroValorRedeP> result = default(IEnumerable<TcsParametroValorRedeP>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsParametroValorRedeP(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsParametroValorRedeP?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsParametroValorRedeP), new string[] { });
                        serviceAddress = "GetTcsParametroValorRedePByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsParametroValorRedeP>>(response.Content.ReadAsStringAsync().Result);
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
        
        private IEnumerable<TcsParametroValorGpeconP> GetLocalTcsParametroValorGpeconP(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsParametroValorGpeconP> result = default(IEnumerable<TcsParametroValorGpeconP>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_parametroContext == null) _parametroContext = new Linx.Framework.BV.Parametro.ParametroDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsParametroValorGpeconP), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsParametroValorGpeconP), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._parametroContext.GetTcsParametroValorGpeconPByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._parametroContext.GetTcsParametroValorGpeconPByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TcsParametroValorGpeconP> GetTcsParametroValorGpeconP(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsParametroValorGpeconP> result = default(IEnumerable<TcsParametroValorGpeconP>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsParametroValorGpeconP(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsParametroValorGpeconP?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsParametroValorGpeconP), new string[] { });
                        serviceAddress = "GetTcsParametroValorGpeconPByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsParametroValorGpeconP>>(response.Content.ReadAsStringAsync().Result);
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
        
        private IEnumerable<TcsParametroValorFilialP> GetLocalTcsParametroValorFilialP(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsParametroValorFilialP> result = default(IEnumerable<TcsParametroValorFilialP>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_parametroContext == null) _parametroContext = new Linx.Framework.BV.Parametro.ParametroDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsParametroValorFilialP), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsParametroValorFilialP), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._parametroContext.GetTcsParametroValorFilialPByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._parametroContext.GetTcsParametroValorFilialPByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TcsParametroValorFilialP> GetTcsParametroValorFilialP(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsParametroValorFilialP> result = default(IEnumerable<TcsParametroValorFilialP>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsParametroValorFilialP(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsParametroValorFilialP?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsParametroValorFilialP), new string[] { });
                        serviceAddress = "GetTcsParametroValorFilialPByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsParametroValorFilialP>>(response.Content.ReadAsStringAsync().Result);
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
        
        private IEnumerable<TcsParametroValorVariacaoGenericaP1> GetLocalTcsParametroValorVariacaoGenericaP1(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsParametroValorVariacaoGenericaP1> result = default(IEnumerable<TcsParametroValorVariacaoGenericaP1>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_parametroContext == null) _parametroContext = new Linx.Framework.BV.Parametro.ParametroDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsParametroValorVariacaoGenericaP1), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsParametroValorVariacaoGenericaP1), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._parametroContext.GetTcsParametroValorVariacaoGenericaP1ByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._parametroContext.GetTcsParametroValorVariacaoGenericaP1ByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TcsParametroValorVariacaoGenericaP1> GetTcsParametroValorVariacaoGenericaP1(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsParametroValorVariacaoGenericaP1> result = default(IEnumerable<TcsParametroValorVariacaoGenericaP1>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsParametroValorVariacaoGenericaP1(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsParametroValorVariacaoGenericaP1?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsParametroValorVariacaoGenericaP1), new string[] { });
                        serviceAddress = "GetTcsParametroValorVariacaoGenericaP1ByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsParametroValorVariacaoGenericaP1>>(response.Content.ReadAsStringAsync().Result);
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

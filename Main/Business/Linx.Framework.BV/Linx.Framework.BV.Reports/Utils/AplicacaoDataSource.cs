namespace Linx.Framework.BV.Reports {
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Linx.Tools;
    using Linx.Business.Tools;
    using Linx.Framework.BV.Aplicacao;
    using System.Net.Http;
    using System.Net.Http.Headers;
    
    
    public partial class AplicacaoDataSource {
        
        private string url = "LinxFrameworkAplicacao/";
        
        public Linx.Framework.BV.Aplicacao.AplicacaoDomainService _aplicacaoContext;
        
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
        
        private IEnumerable<TcsAplicacao> GetLocalTcsAplicacao(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsAplicacao> result = default(IEnumerable<TcsAplicacao>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_aplicacaoContext == null) _aplicacaoContext = new Linx.Framework.BV.Aplicacao.AplicacaoDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsAplicacao), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsAplicacao), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._aplicacaoContext.GetTcsAplicacaoByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._aplicacaoContext.GetTcsAplicacaoByEntitySearchNoAssociations(null).ToList();
            if (this.DetailsForLoading != null && this.DetailsForLoading.Length > 0)
            {
               foreach (var entity in result)
               {
                   entity.FillDetails(this._aplicacaoContext, null, null, this.DetailsForLoading);
               }
            }
            return result;
        }
        
        public virtual IEnumerable<TcsAplicacao> GetTcsAplicacao(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsAplicacao> result = default(IEnumerable<TcsAplicacao>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsAplicacao(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsAplicacao?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsAplicacao), new string[] { });
                        serviceAddress = "GetTcsAplicacaoByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsAplicacao>>(response.Content.ReadAsStringAsync().Result);
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
        
        private IEnumerable<TcsAplicacaoVersaoHistorico> GetLocalTcsAplicacaoVersaoHistorico(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsAplicacaoVersaoHistorico> result = default(IEnumerable<TcsAplicacaoVersaoHistorico>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_aplicacaoContext == null) _aplicacaoContext = new Linx.Framework.BV.Aplicacao.AplicacaoDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsAplicacaoVersaoHistorico), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsAplicacaoVersaoHistorico), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._aplicacaoContext.GetTcsAplicacaoVersaoHistoricoByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._aplicacaoContext.GetTcsAplicacaoVersaoHistoricoByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TcsAplicacaoVersaoHistorico> GetTcsAplicacaoVersaoHistorico(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsAplicacaoVersaoHistorico> result = default(IEnumerable<TcsAplicacaoVersaoHistorico>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsAplicacaoVersaoHistorico(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsAplicacaoVersaoHistorico?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsAplicacaoVersaoHistorico), new string[] { });
                        serviceAddress = "GetTcsAplicacaoVersaoHistoricoByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsAplicacaoVersaoHistorico>>(response.Content.ReadAsStringAsync().Result);
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
        
        private IEnumerable<TcsAplicacaoVersaoHistoricoParentComposition> GetLocalTcsAplicacaoVersaoHistoricoParentComposition(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsAplicacaoVersaoHistoricoParentComposition> result = default(IEnumerable<TcsAplicacaoVersaoHistoricoParentComposition>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_aplicacaoContext == null) _aplicacaoContext = new Linx.Framework.BV.Aplicacao.AplicacaoDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsAplicacaoVersaoHistoricoParentComposition), new string[] {"TcsAplicacao"});
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsAplicacaoVersaoHistoricoParentComposition), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._aplicacaoContext.GetTcsAplicacaoVersaoHistoricoParentCompositionByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._aplicacaoContext.GetTcsAplicacaoVersaoHistoricoParentCompositionByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TcsAplicacaoVersaoHistoricoParentComposition> GetTcsAplicacaoVersaoHistoricoParentComposition(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsAplicacaoVersaoHistoricoParentComposition> result = default(IEnumerable<TcsAplicacaoVersaoHistoricoParentComposition>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsAplicacaoVersaoHistoricoParentComposition(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsAplicacaoVersaoHistoricoParentComposition?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsAplicacaoVersaoHistoricoParentComposition), new string[] {"TcsAplicacao"});
                        serviceAddress = "GetTcsAplicacaoVersaoHistoricoParentCompositionByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsAplicacaoVersaoHistoricoParentComposition>>(response.Content.ReadAsStringAsync().Result);
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
            if (_aplicacaoContext == null) _aplicacaoContext = new Linx.Framework.BV.Aplicacao.AplicacaoDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsAmbiente), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsAmbiente), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._aplicacaoContext.GetTcsAmbienteByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._aplicacaoContext.GetTcsAmbienteByEntitySearchNoAssociations(null).ToList();
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
            if (_aplicacaoContext == null) _aplicacaoContext = new Linx.Framework.BV.Aplicacao.AplicacaoDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsAmbienteParentComposition), new string[] {"TcsAplicacao"});
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsAmbienteParentComposition), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._aplicacaoContext.GetTcsAmbienteParentCompositionByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._aplicacaoContext.GetTcsAmbienteParentCompositionByEntitySearchNoAssociations(null).ToList();
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
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsAmbienteParentComposition), new string[] {"TcsAplicacao"});
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
    }
}

namespace Linx.Framework.BV.Reports {
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Linx.Tools;
    using Linx.Business.Tools;
    using Linx.Framework.BV.ObjetoAutorizacao;
    using System.Net.Http;
    using System.Net.Http.Headers;
    
    
    public partial class ObjetoAutorizacaoDataSource {
        
        private string url = "LinxFrameworkObjetoAutorizacao/";
        
        public Linx.Framework.BV.ObjetoAutorizacao.ObjetoAutorizacaoDomainService _objetoAutorizacaoContext;
        
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
        
        private IEnumerable<TcsObjetoAutorizacao> GetLocalTcsObjetoAutorizacao(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsObjetoAutorizacao> result = default(IEnumerable<TcsObjetoAutorizacao>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_objetoAutorizacaoContext == null) _objetoAutorizacaoContext = new Linx.Framework.BV.ObjetoAutorizacao.ObjetoAutorizacaoDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsObjetoAutorizacao), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsObjetoAutorizacao), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._objetoAutorizacaoContext.GetTcsObjetoAutorizacaoByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._objetoAutorizacaoContext.GetTcsObjetoAutorizacaoByEntitySearchNoAssociations(null).ToList();
            if (this.DetailsForLoading != null && this.DetailsForLoading.Length > 0)
            {
               foreach (var entity in result)
               {
                   entity.FillDetails(this._objetoAutorizacaoContext, null, null, this.DetailsForLoading);
               }
            }
            return result;
        }
        
        public virtual IEnumerable<TcsObjetoAutorizacao> GetTcsObjetoAutorizacao(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsObjetoAutorizacao> result = default(IEnumerable<TcsObjetoAutorizacao>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsObjetoAutorizacao(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsObjetoAutorizacao?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsObjetoAutorizacao), new string[] { });
                        serviceAddress = "GetTcsObjetoAutorizacaoByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsObjetoAutorizacao>>(response.Content.ReadAsStringAsync().Result);
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
        
        private IEnumerable<TcsTransacaoAutorizacaoChild> GetLocalTcsTransacaoAutorizacaoChild(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsTransacaoAutorizacaoChild> result = default(IEnumerable<TcsTransacaoAutorizacaoChild>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_objetoAutorizacaoContext == null) _objetoAutorizacaoContext = new Linx.Framework.BV.ObjetoAutorizacao.ObjetoAutorizacaoDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsTransacaoAutorizacaoChild), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsTransacaoAutorizacaoChild), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._objetoAutorizacaoContext.GetTcsTransacaoAutorizacaoChildByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._objetoAutorizacaoContext.GetTcsTransacaoAutorizacaoChildByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TcsTransacaoAutorizacaoChild> GetTcsTransacaoAutorizacaoChild(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsTransacaoAutorizacaoChild> result = default(IEnumerable<TcsTransacaoAutorizacaoChild>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsTransacaoAutorizacaoChild(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsTransacaoAutorizacaoChild?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsTransacaoAutorizacaoChild), new string[] { });
                        serviceAddress = "GetTcsTransacaoAutorizacaoChildByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsTransacaoAutorizacaoChild>>(response.Content.ReadAsStringAsync().Result);
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
        
        private IEnumerable<TcsTransacaoAutorizacaoChildParentComposition> GetLocalTcsTransacaoAutorizacaoChildParentComposition(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsTransacaoAutorizacaoChildParentComposition> result = default(IEnumerable<TcsTransacaoAutorizacaoChildParentComposition>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_objetoAutorizacaoContext == null) _objetoAutorizacaoContext = new Linx.Framework.BV.ObjetoAutorizacao.ObjetoAutorizacaoDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsTransacaoAutorizacaoChildParentComposition), new string[] {"TcsObjetoAutorizacao"});
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsTransacaoAutorizacaoChildParentComposition), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._objetoAutorizacaoContext.GetTcsTransacaoAutorizacaoChildParentCompositionByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._objetoAutorizacaoContext.GetTcsTransacaoAutorizacaoChildParentCompositionByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TcsTransacaoAutorizacaoChildParentComposition> GetTcsTransacaoAutorizacaoChildParentComposition(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsTransacaoAutorizacaoChildParentComposition> result = default(IEnumerable<TcsTransacaoAutorizacaoChildParentComposition>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsTransacaoAutorizacaoChildParentComposition(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsTransacaoAutorizacaoChildParentComposition?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsTransacaoAutorizacaoChildParentComposition), new string[] {"TcsObjetoAutorizacao"});
                        serviceAddress = "GetTcsTransacaoAutorizacaoChildParentCompositionByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsTransacaoAutorizacaoChildParentComposition>>(response.Content.ReadAsStringAsync().Result);
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
        
        private IEnumerable<TcsObjetoConteudoAutorizacao> GetLocalTcsObjetoConteudoAutorizacao(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsObjetoConteudoAutorizacao> result = default(IEnumerable<TcsObjetoConteudoAutorizacao>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_objetoAutorizacaoContext == null) _objetoAutorizacaoContext = new Linx.Framework.BV.ObjetoAutorizacao.ObjetoAutorizacaoDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsObjetoConteudoAutorizacao), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsObjetoConteudoAutorizacao), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._objetoAutorizacaoContext.GetTcsObjetoConteudoAutorizacaoByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._objetoAutorizacaoContext.GetTcsObjetoConteudoAutorizacaoByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TcsObjetoConteudoAutorizacao> GetTcsObjetoConteudoAutorizacao(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsObjetoConteudoAutorizacao> result = default(IEnumerable<TcsObjetoConteudoAutorizacao>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsObjetoConteudoAutorizacao(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsObjetoConteudoAutorizacao?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsObjetoConteudoAutorizacao), new string[] { });
                        serviceAddress = "GetTcsObjetoConteudoAutorizacaoByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsObjetoConteudoAutorizacao>>(response.Content.ReadAsStringAsync().Result);
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
        
        private IEnumerable<TcsObjetoConteudoAutorizacaoParentComposition> GetLocalTcsObjetoConteudoAutorizacaoParentComposition(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsObjetoConteudoAutorizacaoParentComposition> result = default(IEnumerable<TcsObjetoConteudoAutorizacaoParentComposition>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_objetoAutorizacaoContext == null) _objetoAutorizacaoContext = new Linx.Framework.BV.ObjetoAutorizacao.ObjetoAutorizacaoDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsObjetoConteudoAutorizacaoParentComposition), new string[] {"TcsObjetoAutorizacao"});
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsObjetoConteudoAutorizacaoParentComposition), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._objetoAutorizacaoContext.GetTcsObjetoConteudoAutorizacaoParentCompositionByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._objetoAutorizacaoContext.GetTcsObjetoConteudoAutorizacaoParentCompositionByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TcsObjetoConteudoAutorizacaoParentComposition> GetTcsObjetoConteudoAutorizacaoParentComposition(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsObjetoConteudoAutorizacaoParentComposition> result = default(IEnumerable<TcsObjetoConteudoAutorizacaoParentComposition>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsObjetoConteudoAutorizacaoParentComposition(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsObjetoConteudoAutorizacaoParentComposition?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsObjetoConteudoAutorizacaoParentComposition), new string[] {"TcsObjetoAutorizacao"});
                        serviceAddress = "GetTcsObjetoConteudoAutorizacaoParentCompositionByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsObjetoConteudoAutorizacaoParentComposition>>(response.Content.ReadAsStringAsync().Result);
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

namespace Linx.Framework.BV.Reports {
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Linx.Tools;
    using Linx.Business.Tools;
    using Linx.Framework.BV.UsuarioAutorizacao;
    using System.Net.Http;
    using System.Net.Http.Headers;
    
    
    public partial class UsuarioAutorizacaoDataSource {
        
        private string url = "LinxFrameworkUsuarioAutorizacao/";
        
        public Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService _usuarioAutorizacaoContext;
        
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
        
        private IEnumerable<TcsUsuarioAutenticacao> GetLocalTcsUsuarioAutenticacao(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsUsuarioAutenticacao> result = default(IEnumerable<TcsUsuarioAutenticacao>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_usuarioAutorizacaoContext == null) _usuarioAutorizacaoContext = new Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsUsuarioAutenticacao), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsUsuarioAutenticacao), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._usuarioAutorizacaoContext.GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._usuarioAutorizacaoContext.GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations(null).ToList();
            if (this.DetailsForLoading != null && this.DetailsForLoading.Length > 0)
            {
               foreach (var entity in result)
               {
                   entity.FillDetails(this._usuarioAutorizacaoContext, null, null, this.DetailsForLoading);
               }
            }
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
        
        private IEnumerable<TcsUsuarioAcesso> GetLocalTcsUsuarioAcesso(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsUsuarioAcesso> result = default(IEnumerable<TcsUsuarioAcesso>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_usuarioAutorizacaoContext == null) _usuarioAutorizacaoContext = new Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsUsuarioAcesso), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsUsuarioAcesso), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._usuarioAutorizacaoContext.GetTcsUsuarioAcessoByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._usuarioAutorizacaoContext.GetTcsUsuarioAcessoByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TcsUsuarioAcesso> GetTcsUsuarioAcesso(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsUsuarioAcesso> result = default(IEnumerable<TcsUsuarioAcesso>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsUsuarioAcesso(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsUsuarioAcesso?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsUsuarioAcesso), new string[] { });
                        serviceAddress = "GetTcsUsuarioAcessoByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsUsuarioAcesso>>(response.Content.ReadAsStringAsync().Result);
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
        
        private IEnumerable<TcsUsuarioAcessoParentComposition> GetLocalTcsUsuarioAcessoParentComposition(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsUsuarioAcessoParentComposition> result = default(IEnumerable<TcsUsuarioAcessoParentComposition>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_usuarioAutorizacaoContext == null) _usuarioAutorizacaoContext = new Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsUsuarioAcessoParentComposition), new string[] {"TcsUsuarioAutenticacao"});
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsUsuarioAcessoParentComposition), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._usuarioAutorizacaoContext.GetTcsUsuarioAcessoParentCompositionByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._usuarioAutorizacaoContext.GetTcsUsuarioAcessoParentCompositionByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TcsUsuarioAcessoParentComposition> GetTcsUsuarioAcessoParentComposition(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsUsuarioAcessoParentComposition> result = default(IEnumerable<TcsUsuarioAcessoParentComposition>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsUsuarioAcessoParentComposition(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsUsuarioAcessoParentComposition?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsUsuarioAcessoParentComposition), new string[] {"TcsUsuarioAutenticacao"});
                        serviceAddress = "GetTcsUsuarioAcessoParentCompositionByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsUsuarioAcessoParentComposition>>(response.Content.ReadAsStringAsync().Result);
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
        
        private IEnumerable<TcsIdentidadeExterna> GetLocalTcsIdentidadeExterna(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsIdentidadeExterna> result = default(IEnumerable<TcsIdentidadeExterna>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_usuarioAutorizacaoContext == null) _usuarioAutorizacaoContext = new Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsIdentidadeExterna), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsIdentidadeExterna), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._usuarioAutorizacaoContext.GetTcsIdentidadeExternaByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._usuarioAutorizacaoContext.GetTcsIdentidadeExternaByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TcsIdentidadeExterna> GetTcsIdentidadeExterna(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsIdentidadeExterna> result = default(IEnumerable<TcsIdentidadeExterna>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsIdentidadeExterna(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsIdentidadeExterna?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsIdentidadeExterna), new string[] { });
                        serviceAddress = "GetTcsIdentidadeExternaByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsIdentidadeExterna>>(response.Content.ReadAsStringAsync().Result);
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
        
        private IEnumerable<TcsIdentidadeExternaParentComposition> GetLocalTcsIdentidadeExternaParentComposition(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsIdentidadeExternaParentComposition> result = default(IEnumerable<TcsIdentidadeExternaParentComposition>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_usuarioAutorizacaoContext == null) _usuarioAutorizacaoContext = new Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsIdentidadeExternaParentComposition), new string[] {"TcsUsuarioAutenticacao"});
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsIdentidadeExternaParentComposition), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._usuarioAutorizacaoContext.GetTcsIdentidadeExternaParentCompositionByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._usuarioAutorizacaoContext.GetTcsIdentidadeExternaParentCompositionByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TcsIdentidadeExternaParentComposition> GetTcsIdentidadeExternaParentComposition(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsIdentidadeExternaParentComposition> result = default(IEnumerable<TcsIdentidadeExternaParentComposition>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsIdentidadeExternaParentComposition(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsIdentidadeExternaParentComposition?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsIdentidadeExternaParentComposition), new string[] {"TcsUsuarioAutenticacao"});
                        serviceAddress = "GetTcsIdentidadeExternaParentCompositionByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsIdentidadeExternaParentComposition>>(response.Content.ReadAsStringAsync().Result);
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
        
        private IEnumerable<RequisicaoAcesso> GetLocalRequisicaoAcesso(Telerik.Reporting.Processing.Report report) {
            IEnumerable<RequisicaoAcesso> result = default(IEnumerable<RequisicaoAcesso>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_usuarioAutorizacaoContext == null) _usuarioAutorizacaoContext = new Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(RequisicaoAcesso), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(RequisicaoAcesso), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._usuarioAutorizacaoContext.GetRequisicaoAcessoByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._usuarioAutorizacaoContext.GetRequisicaoAcessoByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<RequisicaoAcesso> GetRequisicaoAcesso(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<RequisicaoAcesso> result = default(IEnumerable<RequisicaoAcesso>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalRequisicaoAcesso(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleRequisicaoAcesso?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(RequisicaoAcesso), new string[] { });
                        serviceAddress = "GetRequisicaoAcessoByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<RequisicaoAcesso>>(response.Content.ReadAsStringAsync().Result);
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
        
        private IEnumerable<UsuarioAcesso> GetLocalUsuarioAcesso(Telerik.Reporting.Processing.Report report) {
            IEnumerable<UsuarioAcesso> result = default(IEnumerable<UsuarioAcesso>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_usuarioAutorizacaoContext == null) _usuarioAutorizacaoContext = new Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(UsuarioAcesso), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(UsuarioAcesso), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._usuarioAutorizacaoContext.GetUsuarioAcessoByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._usuarioAutorizacaoContext.GetUsuarioAcessoByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<UsuarioAcesso> GetUsuarioAcesso(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<UsuarioAcesso> result = default(IEnumerable<UsuarioAcesso>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalUsuarioAcesso(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleUsuarioAcesso?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(UsuarioAcesso), new string[] { });
                        serviceAddress = "GetUsuarioAcessoByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<UsuarioAcesso>>(response.Content.ReadAsStringAsync().Result);
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
        
        private IEnumerable<TcsSuporteAcessoLog> GetLocalTcsSuporteAcessoLog(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsSuporteAcessoLog> result = default(IEnumerable<TcsSuporteAcessoLog>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_usuarioAutorizacaoContext == null) _usuarioAutorizacaoContext = new Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsSuporteAcessoLog), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsSuporteAcessoLog), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._usuarioAutorizacaoContext.GetTcsSuporteAcessoLogByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._usuarioAutorizacaoContext.GetTcsSuporteAcessoLogByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TcsSuporteAcessoLog> GetTcsSuporteAcessoLog(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsSuporteAcessoLog> result = default(IEnumerable<TcsSuporteAcessoLog>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsSuporteAcessoLog(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsSuporteAcessoLog?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsSuporteAcessoLog), new string[] { });
                        serviceAddress = "GetTcsSuporteAcessoLogByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsSuporteAcessoLog>>(response.Content.ReadAsStringAsync().Result);
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
        
        private IEnumerable<RequisicaoSuporte> GetLocalRequisicaoSuporte(Telerik.Reporting.Processing.Report report) {
            IEnumerable<RequisicaoSuporte> result = default(IEnumerable<RequisicaoSuporte>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_usuarioAutorizacaoContext == null) _usuarioAutorizacaoContext = new Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(RequisicaoSuporte), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(RequisicaoSuporte), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._usuarioAutorizacaoContext.GetRequisicaoSuporteByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._usuarioAutorizacaoContext.GetRequisicaoSuporteByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<RequisicaoSuporte> GetRequisicaoSuporte(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<RequisicaoSuporte> result = default(IEnumerable<RequisicaoSuporte>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalRequisicaoSuporte(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleRequisicaoSuporte?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(RequisicaoSuporte), new string[] { });
                        serviceAddress = "GetRequisicaoSuporteByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<RequisicaoSuporte>>(response.Content.ReadAsStringAsync().Result);
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
        
        private IEnumerable<TcsUsuarioAcessoAmbiente> GetLocalTcsUsuarioAcessoAmbiente(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsUsuarioAcessoAmbiente> result = default(IEnumerable<TcsUsuarioAcessoAmbiente>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_usuarioAutorizacaoContext == null) _usuarioAutorizacaoContext = new Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsUsuarioAcessoAmbiente), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsUsuarioAcessoAmbiente), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._usuarioAutorizacaoContext.GetTcsUsuarioAcessoAmbienteByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._usuarioAutorizacaoContext.GetTcsUsuarioAcessoAmbienteByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TcsUsuarioAcessoAmbiente> GetTcsUsuarioAcessoAmbiente(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsUsuarioAcessoAmbiente> result = default(IEnumerable<TcsUsuarioAcessoAmbiente>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsUsuarioAcessoAmbiente(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsUsuarioAcessoAmbiente?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsUsuarioAcessoAmbiente), new string[] { });
                        serviceAddress = "GetTcsUsuarioAcessoAmbienteByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsUsuarioAcessoAmbiente>>(response.Content.ReadAsStringAsync().Result);
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
        
        private IEnumerable<TcsUsuarioAutenticacaoAcessoP> GetLocalTcsUsuarioAutenticacaoAcessoP(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsUsuarioAutenticacaoAcessoP> result = default(IEnumerable<TcsUsuarioAutenticacaoAcessoP>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_usuarioAutorizacaoContext == null) _usuarioAutorizacaoContext = new Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsUsuarioAutenticacaoAcessoP), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsUsuarioAutenticacaoAcessoP), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._usuarioAutorizacaoContext.GetTcsUsuarioAutenticacaoAcessoPByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._usuarioAutorizacaoContext.GetTcsUsuarioAutenticacaoAcessoPByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TcsUsuarioAutenticacaoAcessoP> GetTcsUsuarioAutenticacaoAcessoP(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsUsuarioAutenticacaoAcessoP> result = default(IEnumerable<TcsUsuarioAutenticacaoAcessoP>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsUsuarioAutenticacaoAcessoP(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsUsuarioAutenticacaoAcessoP?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsUsuarioAutenticacaoAcessoP), new string[] { });
                        serviceAddress = "GetTcsUsuarioAutenticacaoAcessoPByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsUsuarioAutenticacaoAcessoP>>(response.Content.ReadAsStringAsync().Result);
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
        
        private IEnumerable<TcsUsuarioGpecon> GetLocalTcsUsuarioGpecon(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsUsuarioGpecon> result = default(IEnumerable<TcsUsuarioGpecon>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_usuarioAutorizacaoContext == null) _usuarioAutorizacaoContext = new Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsUsuarioGpecon), new string[] { });
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsUsuarioGpecon), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._usuarioAutorizacaoContext.GetTcsUsuarioGpeconByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._usuarioAutorizacaoContext.GetTcsUsuarioGpeconByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TcsUsuarioGpecon> GetTcsUsuarioGpecon(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsUsuarioGpecon> result = default(IEnumerable<TcsUsuarioGpecon>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsUsuarioGpecon(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsUsuarioGpecon?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsUsuarioGpecon), new string[] { });
                        serviceAddress = "GetTcsUsuarioGpeconByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsUsuarioGpecon>>(response.Content.ReadAsStringAsync().Result);
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
        
        private IEnumerable<TcsUsuarioGpeconParentComposition> GetLocalTcsUsuarioGpeconParentComposition(Telerik.Reporting.Processing.Report report) {
            IEnumerable<TcsUsuarioGpeconParentComposition> result = default(IEnumerable<TcsUsuarioGpeconParentComposition>);
            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);
            if (_usuarioAutorizacaoContext == null) _usuarioAutorizacaoContext = new Linx.Framework.BV.UsuarioAutorizacao.UsuarioAutorizacaoDomainService(headers) { IsSecure = true };
            string entitySearchExpression = String.Empty;
            var jEntitySearch = GetFilterExpression(report, typeof(TcsUsuarioGpeconParentComposition), new string[] {"TcsUsuarioAutenticacao"});
            if (!jEntitySearch.IsNullOrEmpty())
            {
                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(TcsUsuarioGpeconParentComposition), jEntitySearch, false, false, false);
            }
            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())
              result = this._usuarioAutorizacaoContext.GetTcsUsuarioGpeconParentCompositionByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();
            else
              result = this._usuarioAutorizacaoContext.GetTcsUsuarioGpeconParentCompositionByEntitySearchNoAssociations(null).ToList();
            return result;
        }
        
        public virtual IEnumerable<TcsUsuarioGpeconParentComposition> GetTcsUsuarioGpeconParentComposition(object reportItem) {
            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;
            IEnumerable<TcsUsuarioGpeconParentComposition> result = default(IEnumerable<TcsUsuarioGpeconParentComposition>);
            if (report != null && report.Parameters.ContainsKey("CurrentUser") && !report.Parameters["CurrentUser"].Value.IsNullOrEmpty())
            {
                return GetLocalTcsUsuarioGpeconParentComposition(report);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    var userName = report.Parameters.FirstOrDefault(x => x.Key == "Username");
                    var password = report.Parameters.FirstOrDefault(x => x.Key == "Password");
                    
                    string serviceBus = (report != null && report.Parameters.ContainsKey("ServiceBusUrl") && !report.Parameters["ServiceBusUrl"].Value.IsNullOrEmpty() ? report.Parameters["ServiceBusUrl"].Value.ToString() : "http://localhost:1710/");
                    string serviceAddress = "GetSampleTcsUsuarioGpeconParentComposition?details=" + String.Join("-", (this.DetailsForLoading ?? new string[] {}));
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
                        var jEntitySearch = GetFilterExpression(report, typeof(TcsUsuarioGpeconParentComposition), new string[] {"TcsUsuarioAutenticacao"});
                        serviceAddress = "GetTcsUsuarioGpeconParentCompositionByEntitySearchNoAssociations?jEntitySearch=" + System.Web.HttpUtility.UrlEncode(jEntitySearch);
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
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TcsUsuarioGpeconParentComposition>>(response.Content.ReadAsStringAsync().Result);
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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Linx.Tools;
using Linx.Business.Tools;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Composition;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using Linx.Data;
using System.ServiceModel.DomainServices.Server;
using System.Web.Http.OData;
using Linx.DataService;
using Breeze.WebApi2;
using Breeze.ContextProvider;
using BusinessNS = LinxTraining001.BV.TiposCampos;

namespace LinxTraining001.BV.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/LinxTraining001TiposCampos/[ActionName]
    // Entities Catalog Call: http://localhost:1710/LinxTraining001TiposCampos/GetEntities
    // Entity MetaData Call: http://localhost:1710/LinxTraining001TiposCampos/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/LinxTraining001TiposCampos/GetClientDomains
    // Client Service Call: http://localhost:1710/LinxTraining001TiposCampos/GetClientService
    // Client Factory Call: http://localhost:1710/LinxTraining001TiposCampos/GetClientFactory?entityName=[EntityName]
    // Client Factory Custom Events Call: http://localhost:1710/LinxTraining001TiposCampos/GetClientFactoryCustomEvents?entityName=[EntityName]
    // Help Call: http://localhost:1710/HelpController/LinxTraining001TiposCampos
    // Feed OData Call: http://localhost:1710/LinxTraining001TiposCamposOData
    [RoutePrefix("LinxTraining001TiposCampos")]
    [Breeze.WebApi2.BreezeController]
    [ODataBasicAuthenticationFilter]
    public partial class LinxTraining001TiposCamposController : ApiController
    {
        private DataServiceRepository<BusinessNS.TiposCamposDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.TiposCamposDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.TiposCamposDomainService>(typeof(BusinessNS.TiposCamposFilhaView), typeof(BusinessNS.TiposCamposView)); _repository.Context.IsSecure = true; } return _repository; } }
        public LinxTraining001TiposCamposController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(LinxTraining001TiposCamposController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.TiposCamposDomainService).Assembly.FullName,
                ModelAssemblyName = repository.Context.GetModelAssemblyName()
            };
        }
        
        [Route("GetClientDomains"), System.Web.Http.HttpGet()]
        public string[] GetClientDomains()
        {
            var result = repository.Context.GetClientDomains();
            return result;
        }
        
        [Route("GetClientService"), System.Web.Http.HttpGet()]
        public string[] GetClientService()
        {
            var result = repository.Context.GetClientService();
            return result;
        }
        
        [Route("GetClientFactory"), System.Web.Http.HttpGet()]
        public string[] GetClientFactory(string entityName)
        {
            var result = repository.Context.GetClientFactory(entityName);
            return result;
        }
        
        [Route("GetClientFactoryCustomEvents"), System.Web.Http.HttpGet()]
        public string[] GetClientFactoryCustomEvents(string entityName)
        {
            var result = repository.Context.GetClientFactoryCustomEvents(entityName);
            return result;
        }
        
        [Route("GetMetaData"), System.Web.Http.HttpGet()]
        public List<LinxEntityReferenceInfo> GetMetaData(string entityName = "", bool allComposition = false)
        {
            var result = repository.Context.GetMetaDataObject("LinxTraining001.BV.TiposCampos." + entityName, false, true);
            return (allComposition ? result : result.Where(e => e.ClassName == entityName).ToList());
        }
        
        [Route("GetEntities"), System.Web.Http.HttpGet()]
        public object[] GetEntities()
        {
            return new object[] { 
            };
        }
        
        [Route("GetTemplateReport"), System.Web.Http.HttpGet()]
        public string GetTemplateReport(string reportPath)
        {
            var zip = new LinxZip();
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/" + reportPath));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetReportDataSource"), System.Web.Http.HttpGet()]
        public string GetReportDataSource()
        {
            var zip = new LinxZip();
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetDomainsInfo"), System.Web.Http.HttpGet()]
        public string[] GetDomainsInfo(string domainNames)
        {
            return LinxTraining001.BV.Domains.DomainHelper.GetDomainsInfo(domainNames);
        }
        
        #region Get LookUps
        #endregion
        #region Get KPI Ranges
        
        [LinxTraining001TiposCamposControllerAuthorize]
        [Route("GetKpiTesteRanges"), System.Web.Http.HttpGet()]
        public IEnumerable<KpiRangeItem> GetKpiTesteRanges()
        {
            var kpi = new LinxTraining001.BV.KPIs.KpiTeste();
            Linx.Business.Tools.KpiManager.UpdateKpiInfo(kpi);
            return kpi.Ranges.Values.ToArray();
        }
        
        #endregion
        
        #region Get Business Entities
        
        [Route("GetBmEntityProperties"), System.Web.Http.HttpGet()]
        public List<BmMetaDataProperty> GetBmEntityProperties(string entityName, string parentDataPath)
        {
            return repository.Context.GetBmEntityProperties(entityName, parentDataPath);
        }
        
        [LinxTraining001TiposCamposControllerAuthorize]
        [Route("GetTiposCamposView"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TiposCamposView> GetTiposCamposView()
        {
            return repository.Context.GetTiposCamposView();
        }
        
        [LinxTraining001TiposCamposControllerAuthorize]
        [Route("GetTiposCamposViewNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TiposCamposView> GetTiposCamposViewNoAssociations()
        {
            return repository.Context.GetTiposCamposViewNoAssociations();
        }
        
        [LinxTraining001TiposCamposControllerAuthorize]
        [Route("GetTiposCamposViewByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TiposCamposView> GetTiposCamposViewByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTiposCamposViewByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TiposCamposView), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [LinxTraining001TiposCamposControllerAuthorize]
        [Route("GetTiposCamposViewByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TiposCamposView> GetTiposCamposViewByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTiposCamposViewByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TiposCamposView), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTiposCamposViewToExcel"), System.Web.Http.HttpPost()]
        [LinxTraining001TiposCamposControllerAuthorize]
        public string GetTiposCamposViewToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TiposCamposView), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTiposCamposViewByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("LinxTraining001.BV.TiposCampos.TiposCamposView");
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            if (columns.Count > 0)
            {
                foreach (var item in metadata[0].Properties)
                {
                    item.IsBrowsable = columns.ContainsKey(item.Name);
                    if (item.IsBrowsable && !columns[item.Name].IsNullOrEmpty())
                        item.Caption = columns[item.Name];
                    item.Order = item.IsBrowsable ? Array.IndexOf(columns.Keys.ToArray(), item.Name) : -1;
                }
            }
            var excelBytes = ExcelExport.CreateExcelDocumentFile(new ExcelExport.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
            return Convert.ToBase64String(excelBytes);
        }
        [Route("GetTiposCamposViewToReportXml"), System.Web.Http.HttpPost()]
        [LinxTraining001TiposCamposControllerAuthorize]
        public string GetTiposCamposViewToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TiposCamposView), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTiposCamposViewByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("LinxTraining001.BV.TiposCampos.TiposCamposView", true);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            if (columns.Count > 0)
            {
                foreach (var item in metadata[0].Properties)
                {
                    item.IsBrowsable = columns.ContainsKey(item.Name);
                    if (item.IsBrowsable && !columns[item.Name].IsNullOrEmpty())
                        item.Caption = columns[item.Name];
                    item.Order = item.IsBrowsable ? Array.IndexOf(columns.Keys.ToArray(), item.Name) : -1;
                }
            }
            var zip = new LinxZip();
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "LinxTraining001.BV.Reports", DataSourceFullName = "LinxTraining001.BV.Reports.TiposCamposDataSource", DataSourceObject = "GetTiposCamposView", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxTraining001TiposCamposControllerAuthorize]
        [Route("GetSampleTiposCamposView"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TiposCamposView> GetSampleTiposCamposView(string details)
        {
            var result = repository.Context.GetTiposCamposViewByEntitySearchNoAssociations(null).Take(100).ToList();
               if (!details.IsNullOrEmpty())
               {
                   foreach(var entity in result)
                   {
                       entity.FillDetails(repository.Context, null, null, details.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries), 5);
                   }
               }
            return result.AsQueryable();
        }
        
        [LinxTraining001TiposCamposControllerAuthorize]
        [Route("GetTiposCamposFilhaView"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TiposCamposFilhaView> GetTiposCamposFilhaView()
        {
            return repository.Context.GetTiposCamposFilhaView();
        }
        
        [LinxTraining001TiposCamposControllerAuthorize]
        [Route("GetTiposCamposFilhaViewNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TiposCamposFilhaView> GetTiposCamposFilhaViewNoAssociations()
        {
            return repository.Context.GetTiposCamposFilhaViewNoAssociations();
        }
        
        [LinxTraining001TiposCamposControllerAuthorize]
        [Route("GetTiposCamposFilhaViewByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TiposCamposFilhaView> GetTiposCamposFilhaViewByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTiposCamposFilhaViewByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TiposCamposFilhaView), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [LinxTraining001TiposCamposControllerAuthorize]
        [Route("GetTiposCamposFilhaViewByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TiposCamposFilhaView> GetTiposCamposFilhaViewByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTiposCamposFilhaViewByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TiposCamposFilhaView), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTiposCamposFilhaViewToExcel"), System.Web.Http.HttpPost()]
        [LinxTraining001TiposCamposControllerAuthorize]
        public string GetTiposCamposFilhaViewToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TiposCamposFilhaView), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTiposCamposFilhaViewByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("LinxTraining001.BV.TiposCampos.TiposCamposFilhaView");
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            if (columns.Count > 0)
            {
                foreach (var item in metadata[0].Properties)
                {
                    item.IsBrowsable = columns.ContainsKey(item.Name);
                    if (item.IsBrowsable && !columns[item.Name].IsNullOrEmpty())
                        item.Caption = columns[item.Name];
                    item.Order = item.IsBrowsable ? Array.IndexOf(columns.Keys.ToArray(), item.Name) : -1;
                }
            }
            var excelBytes = ExcelExport.CreateExcelDocumentFile(new ExcelExport.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
            return Convert.ToBase64String(excelBytes);
        }
        [Route("GetTiposCamposFilhaViewToReportXml"), System.Web.Http.HttpPost()]
        [LinxTraining001TiposCamposControllerAuthorize]
        public string GetTiposCamposFilhaViewToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TiposCamposFilhaView), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTiposCamposFilhaViewByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("LinxTraining001.BV.TiposCampos.TiposCamposFilhaView", true);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            if (columns.Count > 0)
            {
                foreach (var item in metadata[0].Properties)
                {
                    item.IsBrowsable = columns.ContainsKey(item.Name);
                    if (item.IsBrowsable && !columns[item.Name].IsNullOrEmpty())
                        item.Caption = columns[item.Name];
                    item.Order = item.IsBrowsable ? Array.IndexOf(columns.Keys.ToArray(), item.Name) : -1;
                }
            }
            var zip = new LinxZip();
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "LinxTraining001.BV.Reports", DataSourceFullName = "LinxTraining001.BV.Reports.TiposCamposDataSource", DataSourceObject = "GetTiposCamposFilhaView", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxTraining001TiposCamposControllerAuthorize]
        [Route("GetSampleTiposCamposFilhaView"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TiposCamposFilhaView> GetSampleTiposCamposFilhaView(string details)
        {
            var result = repository.Context.GetTiposCamposFilhaViewByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        #endregion
        
        #region Get Business Entities By Parent Composition
        
        [LinxTraining001TiposCamposControllerAuthorize]
        [Route("GetTiposCamposFilhaViewParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TiposCamposFilhaViewParentComposition> GetTiposCamposFilhaViewParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTiposCamposFilhaViewParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TiposCamposFilhaViewParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTiposCamposFilhaViewParentCompositionToExcel"), System.Web.Http.HttpPost()]
        [LinxTraining001TiposCamposControllerAuthorize]
        public string GetTiposCamposFilhaViewParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            jEntitySearch = jEntitySearch.Replace("TiposCamposFilhaView{", "TiposCamposFilhaViewParentComposition{");
            jEntitySearch = jEntitySearch.Replace("TiposCamposView{", "TiposCamposFilhaViewParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TiposCamposFilhaViewParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTiposCamposFilhaViewParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("LinxTraining001.BV.TiposCampos.TiposCamposFilhaView");
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            if (columns.Count > 0)
            {
                foreach (var item in metadata[0].Properties)
                {
                    item.IsBrowsable = columns.ContainsKey(item.Name);
                    if (item.IsBrowsable && !columns[item.Name].IsNullOrEmpty())
                        item.Caption = columns[item.Name];
                    item.Order = item.IsBrowsable ? Array.IndexOf(columns.Keys.ToArray(), item.Name) : -1;
                }
            }
            var excelBytes = ExcelExport.CreateExcelDocumentFile(new ExcelExport.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
            return Convert.ToBase64String(excelBytes);
        }
        [Route("GetTiposCamposFilhaViewParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        [LinxTraining001TiposCamposControllerAuthorize]
        public string GetTiposCamposFilhaViewParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TiposCamposFilhaViewParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTiposCamposFilhaViewParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("LinxTraining001.BV.TiposCampos.TiposCamposFilhaView", true);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            if (columns.Count > 0)
            {
                foreach (var item in metadata[0].Properties)
                {
                    item.IsBrowsable = columns.ContainsKey(item.Name);
                    if (item.IsBrowsable && !columns[item.Name].IsNullOrEmpty())
                        item.Caption = columns[item.Name];
                    item.Order = item.IsBrowsable ? Array.IndexOf(columns.Keys.ToArray(), item.Name) : -1;
                }
            }
            var zip = new LinxZip();
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "LinxTraining001.BV.Reports", DataSourceFullName = "LinxTraining001.BV.Reports.TiposCamposDataSource", DataSourceObject = "GetTiposCamposFilhaViewParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxTraining001TiposCamposControllerAuthorize]
        [Route("GetSampleTiposCamposFilhaViewParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TiposCamposFilhaViewParentComposition> GetSampleTiposCamposFilhaViewParentComposition(string details)
        {
            var result = repository.Context.GetTiposCamposFilhaViewParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        #endregion
        
        #region Save Changes
        [LinxTraining001TiposCamposControllerAuthorize]
        [Route("SaveChanges"), System.Web.Http.HttpPost()]
        public SaveResult SaveChanges(JObject saveBundle)
        {
            var result = repository.SaveChanges(saveBundle);
            repository.Context.Dispose();
            return result;
        }
        #endregion
    }
    
    [ODataBasicAuthenticationFilter]
    public partial class LinxTraining001TiposCamposFeedController : ODataController
    {
        private BusinessNS.TiposCamposDomainService _context;
        public BusinessNS.TiposCamposDomainService Context { get {  if (_context == null) { _context = new BusinessNS.TiposCamposDomainService(); _context.IsSecure = true; } return _context; }  }
        
        #region Get Action to Business Entities
        
        
        [LinxTraining001TiposCamposControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TiposCamposView> GetTiposCamposView(Int32 key0)
        {
            var entity = this.Context.GetTiposCamposViewByKey(key0);
            if (entity != null)
               return (new BusinessNS.TiposCamposView[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TiposCamposView>);
        }
        
        [LinxTraining001TiposCamposControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TiposCamposView> GetTiposCamposView()
        {
            return this.Context.GetTiposCamposViewByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxTraining001TiposCamposControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TiposCamposFilhaView> GetTiposCamposView__TiposCamposFilhaView(Int32 key0, string navigation)
        {
            var entity = this.Context.GetTiposCamposViewByKey(key0);
            if (entity != null && navigation == "TiposCamposFilhaViewList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TiposCamposFilhaView" });
               return entity.TiposCamposFilhaViewList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TiposCamposFilhaView>);
        }
        
        [LinxTraining001TiposCamposControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TiposCamposFilhaView> GetTiposCamposFilhaView(Int32 key0)
        {
            var entity = this.Context.GetTiposCamposFilhaViewByKey(key0);
            if (entity != null)
               return (new BusinessNS.TiposCamposFilhaView[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TiposCamposFilhaView>);
        }
        
        [LinxTraining001TiposCamposControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TiposCamposFilhaView> GetTiposCamposFilhaView()
        {
            return this.Context.GetTiposCamposFilhaViewByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxTraining001TiposCamposControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TiposCamposFilhaViewParentComposition> GetTiposCamposFilhaViewParentComposition()
        {
            return this.Context.GetTiposCamposFilhaViewParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxTraining001TiposCamposControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TiposCamposView> GetTiposCamposFilhaView__TiposCamposView(Int32 key0, string navigation)
        {
            var entity = this.Context.GetTiposCamposFilhaViewByKey(key0);
            if (entity != null && navigation == "TiposCamposView")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TiposCamposView[] { entity.TiposCamposView }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TiposCamposView>);
        }
        #endregion
        
    }
    
    public partial class LinxTraining001TiposCamposControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return LinxAutorization.CheckAuthorization(actionContext, string.Format("{0}#{1}#{1}/{2}", "LinxTraining001.BV", "LinxTraining001TiposCampos", actionContext.ActionDescriptor.ActionName));
        }
    }
}

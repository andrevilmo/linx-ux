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
using BusinessNS = LinxTraining001.BV.TiposCamposSingle;

namespace LinxTraining001.BV.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/LinxTraining001TiposCamposSingle/[ActionName]
    // Entities Catalog Call: http://localhost:1710/LinxTraining001TiposCamposSingle/GetEntities
    // Entity MetaData Call: http://localhost:1710/LinxTraining001TiposCamposSingle/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/LinxTraining001TiposCamposSingle/GetClientDomains
    // Client Service Call: http://localhost:1710/LinxTraining001TiposCamposSingle/GetClientService
    // Client Factory Call: http://localhost:1710/LinxTraining001TiposCamposSingle/GetClientFactory?entityName=[EntityName]
    // Client Factory Custom Events Call: http://localhost:1710/LinxTraining001TiposCamposSingle/GetClientFactoryCustomEvents?entityName=[EntityName]
    // Help Call: http://localhost:1710/HelpController/LinxTraining001TiposCamposSingle
    // Feed OData Call: http://localhost:1710/LinxTraining001TiposCamposSingleOData
    [RoutePrefix("LinxTraining001TiposCamposSingle")]
    [Breeze.WebApi2.BreezeController]
    [ODataBasicAuthenticationFilter]
    public partial class LinxTraining001TiposCamposSingleController : ApiController
    {
        private DataServiceRepository<BusinessNS.TiposCamposSingleDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.TiposCamposSingleDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.TiposCamposSingleDomainService>(typeof(BusinessNS.TiposCamposView)); _repository.Context.IsSecure = true; } return _repository; } }
        public LinxTraining001TiposCamposSingleController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(LinxTraining001TiposCamposSingleController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.TiposCamposSingleDomainService).Assembly.FullName,
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
            var result = repository.Context.GetMetaDataObject("LinxTraining001.BV.TiposCamposSingle." + entityName, false, true);
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
        #endregion
        
        #region Get Business Entities
        
        [Route("GetBmEntityProperties"), System.Web.Http.HttpGet()]
        public List<BmMetaDataProperty> GetBmEntityProperties(string entityName, string parentDataPath)
        {
            return repository.Context.GetBmEntityProperties(entityName, parentDataPath);
        }
        
        [LinxTraining001TiposCamposSingleControllerAuthorize]
        [Route("GetTiposCamposView"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TiposCamposView> GetTiposCamposView()
        {
            return repository.Context.GetTiposCamposView();
        }
        
        [LinxTraining001TiposCamposSingleControllerAuthorize]
        [Route("GetTiposCamposViewNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TiposCamposView> GetTiposCamposViewNoAssociations()
        {
            return repository.Context.GetTiposCamposViewNoAssociations();
        }
        
        [LinxTraining001TiposCamposSingleControllerAuthorize]
        [Route("GetTiposCamposViewByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TiposCamposView> GetTiposCamposViewByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTiposCamposViewByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TiposCamposView), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [LinxTraining001TiposCamposSingleControllerAuthorize]
        [Route("GetTiposCamposViewByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TiposCamposView> GetTiposCamposViewByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTiposCamposViewByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TiposCamposView), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTiposCamposViewToExcel"), System.Web.Http.HttpPost()]
        [LinxTraining001TiposCamposSingleControllerAuthorize]
        public string GetTiposCamposViewToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TiposCamposView), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTiposCamposViewByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("LinxTraining001.BV.TiposCamposSingle.TiposCamposView");
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
        [LinxTraining001TiposCamposSingleControllerAuthorize]
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
            var metadata = repository.Context.GetMetaDataObject("LinxTraining001.BV.TiposCamposSingle.TiposCamposView", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "LinxTraining001.BV.Reports", DataSourceFullName = "LinxTraining001.BV.Reports.TiposCamposSingleDataSource", DataSourceObject = "GetTiposCamposView", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxTraining001TiposCamposSingleControllerAuthorize]
        [Route("GetSampleTiposCamposView"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TiposCamposView> GetSampleTiposCamposView(string details)
        {
            var result = repository.Context.GetTiposCamposViewByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        #endregion
        
        #region Get Business Entities By Parent Composition
        #endregion
        
        #region Save Changes
        [LinxTraining001TiposCamposSingleControllerAuthorize]
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
    public partial class LinxTraining001TiposCamposSingleFeedController : ODataController
    {
        private BusinessNS.TiposCamposSingleDomainService _context;
        public BusinessNS.TiposCamposSingleDomainService Context { get {  if (_context == null) { _context = new BusinessNS.TiposCamposSingleDomainService(); _context.IsSecure = true; } return _context; }  }
        
        #region Get Action to Business Entities
        
        
        [LinxTraining001TiposCamposSingleControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TiposCamposView> GetTiposCamposView(Int32 key0)
        {
            var entity = this.Context.GetTiposCamposViewByKey(key0);
            if (entity != null)
               return (new BusinessNS.TiposCamposView[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TiposCamposView>);
        }
        
        [LinxTraining001TiposCamposSingleControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TiposCamposView> GetTiposCamposView()
        {
            return this.Context.GetTiposCamposViewByEntitySearchNoAssociations(null).AsQueryable();
        }
        #endregion
        
    }
    
    public partial class LinxTraining001TiposCamposSingleControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return LinxAutorization.CheckAuthorization(actionContext, string.Format("{0}#{1}#{1}/{2}", "LinxTraining001.BV", "LinxTraining001TiposCamposSingle", actionContext.ActionDescriptor.ActionName));
        }
    }
}

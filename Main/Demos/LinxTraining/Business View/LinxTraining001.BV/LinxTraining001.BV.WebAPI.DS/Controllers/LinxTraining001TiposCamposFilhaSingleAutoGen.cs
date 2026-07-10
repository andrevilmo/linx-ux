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
using BusinessNS = LinxTraining001.BV.TiposCamposFilhaSingle;

namespace LinxTraining001.BV.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/LinxTraining001TiposCamposFilhaSingle/[ActionName]
    // Entities Catalog Call: http://localhost:1710/LinxTraining001TiposCamposFilhaSingle/GetEntities
    // Entity MetaData Call: http://localhost:1710/LinxTraining001TiposCamposFilhaSingle/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/LinxTraining001TiposCamposFilhaSingle/GetClientDomains
    // Client Service Call: http://localhost:1710/LinxTraining001TiposCamposFilhaSingle/GetClientService
    // Client Factory Call: http://localhost:1710/LinxTraining001TiposCamposFilhaSingle/GetClientFactory?entityName=[EntityName]
    // Client Factory Custom Events Call: http://localhost:1710/LinxTraining001TiposCamposFilhaSingle/GetClientFactoryCustomEvents?entityName=[EntityName]
    // Help Call: http://localhost:1710/HelpController/LinxTraining001TiposCamposFilhaSingle
    // Feed OData Call: http://localhost:1710/LinxTraining001TiposCamposFilhaSingleOData
    [RoutePrefix("LinxTraining001TiposCamposFilhaSingle")]
    [Breeze.WebApi2.BreezeController]
    [ODataBasicAuthenticationFilter]
    public partial class LinxTraining001TiposCamposFilhaSingleController : ApiController
    {
        private DataServiceRepository<BusinessNS.TiposCamposFilhaSingleDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.TiposCamposFilhaSingleDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.TiposCamposFilhaSingleDomainService>(typeof(BusinessNS.TiposCamposFilhaView)); _repository.Context.IsSecure = true; } return _repository; } }
        public LinxTraining001TiposCamposFilhaSingleController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(LinxTraining001TiposCamposFilhaSingleController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.TiposCamposFilhaSingleDomainService).Assembly.FullName,
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
            var result = repository.Context.GetMetaDataObject("LinxTraining001.BV.TiposCamposFilhaSingle." + entityName, false, true);
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
        
        [LinxTraining001TiposCamposFilhaSingleControllerAuthorize]
        [Route("GetAllLookUpTiposCampos"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTiposCampos> GetAllLookUpTiposCampos()
        {
            return repository.Context.GetAllLookUpTiposCampos();
        }
        
        [LinxTraining001TiposCamposFilhaSingleControllerAuthorize]
        [Route("GetLookUpTiposCamposByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTiposCampos> GetLookUpTiposCamposByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTiposCamposByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        #endregion
        #region Get KPI Ranges
        #endregion
        
        #region Get Business Entities
        
        [Route("GetBmEntityProperties"), System.Web.Http.HttpGet()]
        public List<BmMetaDataProperty> GetBmEntityProperties(string entityName, string parentDataPath)
        {
            return repository.Context.GetBmEntityProperties(entityName, parentDataPath);
        }
        
        [LinxTraining001TiposCamposFilhaSingleControllerAuthorize]
        [Route("GetTiposCamposFilhaView"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TiposCamposFilhaView> GetTiposCamposFilhaView()
        {
            return repository.Context.GetTiposCamposFilhaView();
        }
        
        [LinxTraining001TiposCamposFilhaSingleControllerAuthorize]
        [Route("GetTiposCamposFilhaViewNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TiposCamposFilhaView> GetTiposCamposFilhaViewNoAssociations()
        {
            return repository.Context.GetTiposCamposFilhaViewNoAssociations();
        }
        
        [LinxTraining001TiposCamposFilhaSingleControllerAuthorize]
        [Route("GetTiposCamposFilhaViewByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TiposCamposFilhaView> GetTiposCamposFilhaViewByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTiposCamposFilhaViewByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TiposCamposFilhaView), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [LinxTraining001TiposCamposFilhaSingleControllerAuthorize]
        [Route("GetTiposCamposFilhaViewByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TiposCamposFilhaView> GetTiposCamposFilhaViewByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTiposCamposFilhaViewByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TiposCamposFilhaView), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTiposCamposFilhaViewToExcel"), System.Web.Http.HttpPost()]
        [LinxTraining001TiposCamposFilhaSingleControllerAuthorize]
        public string GetTiposCamposFilhaViewToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TiposCamposFilhaView), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTiposCamposFilhaViewByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("LinxTraining001.BV.TiposCamposFilhaSingle.TiposCamposFilhaView");
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
        [LinxTraining001TiposCamposFilhaSingleControllerAuthorize]
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
            var metadata = repository.Context.GetMetaDataObject("LinxTraining001.BV.TiposCamposFilhaSingle.TiposCamposFilhaView", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "LinxTraining001.BV.Reports", DataSourceFullName = "LinxTraining001.BV.Reports.TiposCamposFilhaSingleDataSource", DataSourceObject = "GetTiposCamposFilhaView", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxTraining001TiposCamposFilhaSingleControllerAuthorize]
        [Route("GetSampleTiposCamposFilhaView"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TiposCamposFilhaView> GetSampleTiposCamposFilhaView(string details)
        {
            var result = repository.Context.GetTiposCamposFilhaViewByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        #endregion
        
        #region Get Business Entities By Parent Composition
        #endregion
        
        #region Save Changes
        [LinxTraining001TiposCamposFilhaSingleControllerAuthorize]
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
    public partial class LinxTraining001TiposCamposFilhaSingleFeedController : ODataController
    {
        private BusinessNS.TiposCamposFilhaSingleDomainService _context;
        public BusinessNS.TiposCamposFilhaSingleDomainService Context { get {  if (_context == null) { _context = new BusinessNS.TiposCamposFilhaSingleDomainService(); _context.IsSecure = true; } return _context; }  }
        
        #region Get Action to Business Entities
        
        
        [LinxTraining001TiposCamposFilhaSingleControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TiposCamposFilhaView> GetTiposCamposFilhaView(Int32 key0)
        {
            var entity = this.Context.GetTiposCamposFilhaViewByKey(key0);
            if (entity != null)
               return (new BusinessNS.TiposCamposFilhaView[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TiposCamposFilhaView>);
        }
        
        [LinxTraining001TiposCamposFilhaSingleControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TiposCamposFilhaView> GetTiposCamposFilhaView()
        {
            return this.Context.GetTiposCamposFilhaViewByEntitySearchNoAssociations(null).AsQueryable();
        }
        #endregion
        
    }
    
    public partial class LinxTraining001TiposCamposFilhaSingleControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return LinxAutorization.CheckAuthorization(actionContext, string.Format("{0}#{1}#{1}/{2}", "LinxTraining001.BV", "LinxTraining001TiposCamposFilhaSingle", actionContext.ActionDescriptor.ActionName));
        }
    }
}

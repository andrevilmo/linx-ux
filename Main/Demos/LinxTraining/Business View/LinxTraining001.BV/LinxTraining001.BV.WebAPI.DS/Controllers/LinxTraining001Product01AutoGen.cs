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
using BusinessNS = LinxTraining001.BV.Product01;

namespace LinxTraining001.BV.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/LinxTraining001Product01/[ActionName]
    // Entities Catalog Call: http://localhost:1710/LinxTraining001Product01/GetEntities
    // Entity MetaData Call: http://localhost:1710/LinxTraining001Product01/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/LinxTraining001Product01/GetClientDomains
    // Client Service Call: http://localhost:1710/LinxTraining001Product01/GetClientService
    // Client Factory Call: http://localhost:1710/LinxTraining001Product01/GetClientFactory?entityName=[EntityName]
    // Client Factory Custom Events Call: http://localhost:1710/LinxTraining001Product01/GetClientFactoryCustomEvents?entityName=[EntityName]
    // Help Call: http://localhost:1710/HelpController/LinxTraining001Product01
    // Feed OData Call: http://localhost:1710/LinxTraining001Product01OData
    [RoutePrefix("LinxTraining001Product01")]
    [Breeze.WebApi2.BreezeController]
    [ODataBasicAuthenticationFilter]
    public partial class LinxTraining001Product01Controller : ApiController
    {
        private DataServiceRepository<BusinessNS.Product01DomainService> _repository = null;
        private DataServiceRepository<BusinessNS.Product01DomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.Product01DomainService>(typeof(BusinessNS.ProductView)); _repository.Context.IsSecure = true; } return _repository; } }
        public LinxTraining001Product01Controller()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(LinxTraining001Product01Controller).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.Product01DomainService).Assembly.FullName,
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
            var result = repository.Context.GetMetaDataObject("LinxTraining001.BV.Product01." + entityName, false, true);
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
        
        [LinxTraining001Product01ControllerAuthorize]
        [Route("GetAllLookUpProductModel"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpProductModel> GetAllLookUpProductModel()
        {
            return repository.Context.GetAllLookUpProductModel();
        }
        
        [LinxTraining001Product01ControllerAuthorize]
        [Route("GetLookUpProductModelByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpProductModel> GetLookUpProductModelByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpProductModelByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [LinxTraining001Product01ControllerAuthorize]
        [Route("GetAllLookUpProductSubcategory"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpProductSubcategory> GetAllLookUpProductSubcategory()
        {
            return repository.Context.GetAllLookUpProductSubcategory();
        }
        
        [LinxTraining001Product01ControllerAuthorize]
        [Route("GetLookUpProductSubcategoryByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpProductSubcategory> GetLookUpProductSubcategoryByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpProductSubcategoryByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [LinxTraining001Product01ControllerAuthorize]
        [Route("GetAllLookUpUnitMeasure"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpUnitMeasure> GetAllLookUpUnitMeasure()
        {
            return repository.Context.GetAllLookUpUnitMeasure();
        }
        
        [LinxTraining001Product01ControllerAuthorize]
        [Route("GetLookUpUnitMeasureByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpUnitMeasure> GetLookUpUnitMeasureByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpUnitMeasureByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [LinxTraining001Product01ControllerAuthorize]
        [Route("GetAllLookUpUnitMeasure1"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpUnitMeasure1> GetAllLookUpUnitMeasure1()
        {
            return repository.Context.GetAllLookUpUnitMeasure1();
        }
        
        [LinxTraining001Product01ControllerAuthorize]
        [Route("GetLookUpUnitMeasure1ByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpUnitMeasure1> GetLookUpUnitMeasure1ByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpUnitMeasure1ByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
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
        
        [LinxTraining001Product01ControllerAuthorize]
        [Route("GetProductView"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.ProductView> GetProductView()
        {
            return repository.Context.GetProductView();
        }
        
        [LinxTraining001Product01ControllerAuthorize]
        [Route("GetProductViewNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.ProductView> GetProductViewNoAssociations()
        {
            return repository.Context.GetProductViewNoAssociations();
        }
        
        [LinxTraining001Product01ControllerAuthorize]
        [Route("GetProductViewByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.ProductView> GetProductViewByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetProductViewByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.ProductView), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [LinxTraining001Product01ControllerAuthorize]
        [Route("GetProductViewByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.ProductView> GetProductViewByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetProductViewByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.ProductView), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetProductViewToExcel"), System.Web.Http.HttpPost()]
        [LinxTraining001Product01ControllerAuthorize]
        public string GetProductViewToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.ProductView), jEntitySearch, false, false, false);
            var entities = repository.Context.GetProductViewByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("LinxTraining001.BV.Product01.ProductView");
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
        [Route("GetProductViewToReportXml"), System.Web.Http.HttpPost()]
        [LinxTraining001Product01ControllerAuthorize]
        public string GetProductViewToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.ProductView), jEntitySearch, false, false, false);
            var entities = repository.Context.GetProductViewByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("LinxTraining001.BV.Product01.ProductView", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "LinxTraining001.BV.Reports", DataSourceFullName = "LinxTraining001.BV.Reports.Product01DataSource", DataSourceObject = "GetProductView", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxTraining001Product01ControllerAuthorize]
        [Route("GetSampleProductView"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.ProductView> GetSampleProductView(string details)
        {
            var result = repository.Context.GetProductViewByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        #endregion
        
        #region Get Business Entities By Parent Composition
        #endregion
        
        #region Save Changes
        [LinxTraining001Product01ControllerAuthorize]
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
    public partial class LinxTraining001Product01FeedController : ODataController
    {
        private BusinessNS.Product01DomainService _context;
        public BusinessNS.Product01DomainService Context { get {  if (_context == null) { _context = new BusinessNS.Product01DomainService(); _context.IsSecure = true; } return _context; }  }
        
        #region Get Action to Business Entities
        
        
        [LinxTraining001Product01ControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.ProductView> GetProductView(Int32 key0)
        {
            var entity = this.Context.GetProductViewByKey(key0);
            if (entity != null)
               return (new BusinessNS.ProductView[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.ProductView>);
        }
        
        [LinxTraining001Product01ControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.ProductView> GetProductView()
        {
            return this.Context.GetProductViewByEntitySearchNoAssociations(null).AsQueryable();
        }
        #endregion
        
    }
    
    public partial class LinxTraining001Product01ControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return LinxAutorization.CheckAuthorization(actionContext, string.Format("{0}#{1}#{1}/{2}", "LinxTraining001.BV", "LinxTraining001Product01", actionContext.ActionDescriptor.ActionName));
        }
    }
}

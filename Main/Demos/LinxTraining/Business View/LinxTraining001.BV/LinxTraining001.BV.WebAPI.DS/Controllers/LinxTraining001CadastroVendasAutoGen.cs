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
using BusinessNS = LinxTraining001.BV.CadastroVendas;

namespace LinxTraining001.BV.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/LinxTraining001CadastroVendas/[ActionName]
    // Entities Catalog Call: http://localhost:1710/LinxTraining001CadastroVendas/GetEntities
    // Entity MetaData Call: http://localhost:1710/LinxTraining001CadastroVendas/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/LinxTraining001CadastroVendas/GetClientDomains
    // Client Service Call: http://localhost:1710/LinxTraining001CadastroVendas/GetClientService
    // Client Factory Call: http://localhost:1710/LinxTraining001CadastroVendas/GetClientFactory?entityName=[EntityName]
    // Client Factory Custom Events Call: http://localhost:1710/LinxTraining001CadastroVendas/GetClientFactoryCustomEvents?entityName=[EntityName]
    // Help Call: http://localhost:1710/HelpController/LinxTraining001CadastroVendas
    // Feed OData Call: http://localhost:1710/LinxTraining001CadastroVendasOData
    [RoutePrefix("LinxTraining001CadastroVendas")]
    [Breeze.WebApi2.BreezeController]
    [ODataBasicAuthenticationFilter]
    public partial class LinxTraining001CadastroVendasController : ApiController
    {
        private DataServiceRepository<BusinessNS.CadastroVendasDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.CadastroVendasDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.CadastroVendasDomainService>(typeof(BusinessNS.TestePIVOTView), typeof(BusinessNS.VendasView)); _repository.Context.IsSecure = true; } return _repository; } }
        public LinxTraining001CadastroVendasController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(LinxTraining001CadastroVendasController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.CadastroVendasDomainService).Assembly.FullName,
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
            var result = repository.Context.GetMetaDataObject("LinxTraining001.BV.CadastroVendas." + entityName, false, true);
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
        
        [LinxTraining001CadastroVendasControllerAuthorize]
        [Route("GetAllLookUpClientes"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpClientes> GetAllLookUpClientes()
        {
            return repository.Context.GetAllLookUpClientes();
        }
        
        [LinxTraining001CadastroVendasControllerAuthorize]
        [Route("GetLookUpClientesByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpClientes> GetLookUpClientesByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpClientesByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
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
        
        [LinxTraining001CadastroVendasControllerAuthorize]
        [Route("GetVendasView"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendasView> GetVendasView()
        {
            return repository.Context.GetVendasView();
        }
        
        [LinxTraining001CadastroVendasControllerAuthorize]
        [Route("GetVendasViewNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendasView> GetVendasViewNoAssociations()
        {
            return repository.Context.GetVendasViewNoAssociations();
        }
        
        [LinxTraining001CadastroVendasControllerAuthorize]
        [Route("GetVendasViewByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendasView> GetVendasViewByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetVendasViewByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendasView), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [LinxTraining001CadastroVendasControllerAuthorize]
        [Route("GetVendasViewByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendasView> GetVendasViewByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetVendasViewByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendasView), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetVendasViewToExcel"), System.Web.Http.HttpPost()]
        [LinxTraining001CadastroVendasControllerAuthorize]
        public string GetVendasViewToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendasView), jEntitySearch, false, false, false);
            var entities = repository.Context.GetVendasViewByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("LinxTraining001.BV.CadastroVendas.VendasView");
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
        [Route("GetVendasViewToReportXml"), System.Web.Http.HttpPost()]
        [LinxTraining001CadastroVendasControllerAuthorize]
        public string GetVendasViewToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendasView), jEntitySearch, false, false, false);
            var entities = repository.Context.GetVendasViewByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("LinxTraining001.BV.CadastroVendas.VendasView", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "LinxTraining001.BV.Reports", DataSourceFullName = "LinxTraining001.BV.Reports.CadastroVendasDataSource", DataSourceObject = "GetVendasView", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxTraining001CadastroVendasControllerAuthorize]
        [Route("GetSampleVendasView"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendasView> GetSampleVendasView(string details)
        {
            var result = repository.Context.GetVendasViewByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [LinxTraining001CadastroVendasControllerAuthorize]
        [Route("GetTestePIVOTView"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TestePIVOTView> GetTestePIVOTView()
        {
            return repository.Context.GetTestePIVOTView();
        }
        
        [LinxTraining001CadastroVendasControllerAuthorize]
        [Route("GetTestePIVOTViewNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TestePIVOTView> GetTestePIVOTViewNoAssociations()
        {
            return repository.Context.GetTestePIVOTViewNoAssociations();
        }
        
        [LinxTraining001CadastroVendasControllerAuthorize]
        [Route("GetTestePIVOTViewByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TestePIVOTView> GetTestePIVOTViewByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTestePIVOTViewByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TestePIVOTView), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [LinxTraining001CadastroVendasControllerAuthorize]
        [Route("GetTestePIVOTViewByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TestePIVOTView> GetTestePIVOTViewByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTestePIVOTViewByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TestePIVOTView), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTestePIVOTViewToExcel"), System.Web.Http.HttpPost()]
        [LinxTraining001CadastroVendasControllerAuthorize]
        public string GetTestePIVOTViewToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TestePIVOTView), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTestePIVOTViewByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("LinxTraining001.BV.CadastroVendas.TestePIVOTView");
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
        [Route("GetTestePIVOTViewToReportXml"), System.Web.Http.HttpPost()]
        [LinxTraining001CadastroVendasControllerAuthorize]
        public string GetTestePIVOTViewToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TestePIVOTView), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTestePIVOTViewByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("LinxTraining001.BV.CadastroVendas.TestePIVOTView", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "LinxTraining001.BV.Reports", DataSourceFullName = "LinxTraining001.BV.Reports.CadastroVendasDataSource", DataSourceObject = "GetTestePIVOTView", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxTraining001CadastroVendasControllerAuthorize]
        [Route("GetSampleTestePIVOTView"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TestePIVOTView> GetSampleTestePIVOTView(string details)
        {
            var result = repository.Context.GetTestePIVOTViewByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        #endregion
        
        #region Get Business Entities By Parent Composition
        #endregion
        
        #region Save Changes
        [LinxTraining001CadastroVendasControllerAuthorize]
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
    public partial class LinxTraining001CadastroVendasFeedController : ODataController
    {
        private BusinessNS.CadastroVendasDomainService _context;
        public BusinessNS.CadastroVendasDomainService Context { get {  if (_context == null) { _context = new BusinessNS.CadastroVendasDomainService(); _context.IsSecure = true; } return _context; }  }
        
        #region Get Action to Business Entities
        
        
        [LinxTraining001CadastroVendasControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendasView> GetVendasView(Int32 key0)
        {
            var entity = this.Context.GetVendasViewByKey(key0);
            if (entity != null)
               return (new BusinessNS.VendasView[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.VendasView>);
        }
        
        [LinxTraining001CadastroVendasControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendasView> GetVendasView()
        {
            return this.Context.GetVendasViewByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxTraining001CadastroVendasControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TestePIVOTView> GetTestePIVOTView(Int32 key0)
        {
            var entity = this.Context.GetTestePIVOTViewByKey(key0);
            if (entity != null)
               return (new BusinessNS.TestePIVOTView[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TestePIVOTView>);
        }
        
        [LinxTraining001CadastroVendasControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TestePIVOTView> GetTestePIVOTView()
        {
            return this.Context.GetTestePIVOTViewByEntitySearchNoAssociations(null).AsQueryable();
        }
        #endregion
        
    }
    
    public partial class LinxTraining001CadastroVendasControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return LinxAutorization.CheckAuthorization(actionContext, string.Format("{0}#{1}#{1}/{2}", "LinxTraining001.BV", "LinxTraining001CadastroVendas", actionContext.ActionDescriptor.ActionName));
        }
    }
}

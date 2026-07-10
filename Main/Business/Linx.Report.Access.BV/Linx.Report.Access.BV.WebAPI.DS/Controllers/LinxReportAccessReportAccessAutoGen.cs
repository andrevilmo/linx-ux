using Linx.Data;
using Linx.LinqExtensions.Dynamic;
using Linx.Tools;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using Linx.DataService;
using System.ServiceModel.DomainServices.Server;
using Linx.Business.Tools;
using System.ComponentModel.Composition;
using System.Web.Http;
using Breeze.ContextProvider;
using Breeze.WebApi2;
using System.Web.Http.OData;
using BusinessNS = Linx.Report.Access.BV.ReportAccess;

namespace Linx.Report.Access.BV.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/LinxReportAccessReportAccess/[ActionName]
    // Security Information Call: http://localhost:1710/LinxReportAccessReportAccess/GetSecurityInfo
    // Entities Catalog Call: http://localhost:1710/LinxReportAccessReportAccess/GetEntities
    // Entity MetaData Call: http://localhost:1710/LinxReportAccessReportAccess/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/LinxReportAccessReportAccess/GetClientDomains?erp=true
    // Client Service Call: http://localhost:1710/LinxReportAccessReportAccess/GetClientService?erp=true
    // Client Factory Call: http://localhost:1710/LinxReportAccessReportAccess/GetClientFactory?entityName=[EntityName]&erp=true
    // Client Factory Custom Events Call: http://localhost:1710/LinxReportAccessReportAccess/GetClientFactoryCustomEvents?entityName=[EntityName]&erp=true
    // Help Call: http://localhost:1710/HelpController/LinxReportAccessReportAccess
    // Feed OData Call: http://localhost:1710/LinxReportAccessReportAccessOData
    [RoutePrefix("LinxReportAccessReportAccess")]
    [Breeze.WebApi2.BreezeController]
    public partial class LinxReportAccessReportAccessController : ApiController
    {
        private const int maxObjectExcelReturned = 2300000;
        private DataServiceRepository<BusinessNS.ReportAccessDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.ReportAccessDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.ReportAccessDomainService>(typeof(BusinessNS.TelerikReportsList)); _repository.Context.IsSecure = true; } return _repository; } }
        public LinxReportAccessReportAccessController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(LinxReportAccessReportAccessController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.ReportAccessDomainService).Assembly.FullName,
                ModelAssemblyName = ""
            };
        }
        
        [Route("GetClientDomains"), System.Web.Http.HttpGet()]
        public string[] GetClientDomains(bool erp = false)
        {
            var result = repository.Context.GetClientDomains(erp);
            return result;
        }
        
        [Route("GetClientService"), System.Web.Http.HttpGet()]
        public string[] GetClientService(bool erp = false)
        {
            var result = repository.Context.GetClientService(erp);
            return result;
        }
        
        [Route("GetClientFactory"), System.Web.Http.HttpGet()]
        public string[] GetClientFactory(string entityName, bool erp = false)
        {
            var result = repository.Context.GetClientFactory(entityName, erp);
            return result;
        }
        
        [Route("GetClientFactoryCustomEvents"), System.Web.Http.HttpGet()]
        public string[] GetClientFactoryCustomEvents(string entityName, bool erp = false)
        {
            var result = repository.Context.GetClientFactoryCustomEvents(entityName, erp);
            return result;
        }
        
        [Route("GetMetaData"), System.Web.Http.HttpGet()]
        public List<LinxEntityReferenceInfo> GetMetaData(string entityName = "", bool allComposition = false)
        {
            var result = repository.Context.GetMetaDataObject("Linx.Report.Access.BV.ReportAccess." + entityName, false, true);
            return (allComposition ? result : result.Where(e => e.ClassName == entityName).ToList());
        }
        
        [Route("GetSecurityInfo"), System.Web.Http.HttpGet()]
        public string[] GetSecurityInfo()
        {
           return new string[] { "Linx.Report.Access.BV", "LinxReportAccessReportAccess", "LinxReportAccessReportAccess/ActionName" };
        }
        
        [Route("GetEntities"), System.Web.Http.HttpGet()]
        public object[] GetEntities()
        {
            throw new Exception("Não há 'LocalServices' para este serviço.");
        }
        
        [Route("GetTemplateReport"), System.Web.Http.HttpGet()]
        public string GetTemplateReport(string reportPath)
        {
            var zip = new LinxZip();
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/" + reportPath));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Report.Access.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Report.Access.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetReportDataSource"), System.Web.Http.HttpGet()]
        public string GetReportDataSource()
        {
            var zip = new LinxZip();
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Report.Access.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Report.Access.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetDomainsInfo"), System.Web.Http.HttpGet()]
        public string[] GetDomainsInfo(string domainNames)
        {
            return Linx.Report.Access.BV.Domains.DomainHelper.GetDomainsInfo(domainNames);
        }
        
        [Route("GetDomainValues"), System.Web.Http.HttpGet()]
        public Dictionary<string, string> GetDomainValues(string domainName)
        {
            return Linx.Report.Access.BV.Domains.DomainHelper.GetDomainValues(domainName);
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
        
        [Route("GetTelerikReportsList"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TelerikReportsList> GetTelerikReportsList()
        {
            return repository.Context.GetTelerikReportsList().AsQueryable();
        }
        
        [Route("GetTelerikReportsListNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TelerikReportsList> GetTelerikReportsListNoAssociations()
        {
            return repository.Context.GetTelerikReportsListNoAssociations().AsQueryable();
        }
        
        [Route("GetTelerikReportsListByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TelerikReportsList> GetTelerikReportsListByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTelerikReportsListByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TelerikReportsList), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        
        [Route("GetTelerikReportsListByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TelerikReportsList> GetTelerikReportsListByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTelerikReportsListByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TelerikReportsList), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        [Route("GetTelerikReportsListToExcel"), System.Web.Http.HttpPost()]
        public string GetTelerikReportsListToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TelerikReportsList), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTelerikReportsListByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).AsQueryable().OrderBy("Hash asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Report.Access.BV.ReportAccess.TelerikReportsList");
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
            if ((entities.Count() * metadata[0].Properties.Count(p=> p.IsBrowsable)) < maxObjectExcelReturned)
            	return Convert.ToBase64String(ExcelExport.CreateExcelDocumentFile(new ExcelExport.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch }));
            else
               return ExcelExportPagination<BusinessNS.TelerikReportsList>.CreateExcelDocumentFileMapPath("TelerikReportsList",new ExcelExportPagination<BusinessNS.TelerikReportsList>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTelerikReportsListToReportXml"), System.Web.Http.HttpPost()]
        public string GetTelerikReportsListToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TelerikReportsList), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTelerikReportsListByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Report.Access.BV.ReportAccess.TelerikReportsList", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Report.Access.BV.Reports", DataSourceFullName = "Linx.Report.Access.BV.Reports.ReportAccessDataSource", DataSourceObject = "GetTelerikReportsList", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Report.Access.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Report.Access.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTelerikReportsList"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TelerikReportsList> GetSampleTelerikReportsList(string details)
        {
            var result = repository.Context.GetTelerikReportsListByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTelerikReportsListEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTelerikReportsListEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TelerikReportsList), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTelerikReportsListByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TelerikReportsList> GetTelerikReportsListByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTelerikReportsListByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        #endregion
        
        #region Get Business Entities By Parent Composition
        #endregion
        
        #region Save Changes
        [Route("SaveChanges"), System.Web.Http.HttpPost()]
        public SaveResult SaveChanges(JObject saveBundle)
        {
            var result = repository.SaveChanges(saveBundle);
            repository.Context.Dispose();
            return result;
        }
        #endregion
    }
    
    public partial class LinxReportAccessReportAccessFeedController : ODataController
    {
        private BusinessNS.ReportAccessDomainService _context;
        public BusinessNS.ReportAccessDomainService Context { get {  if (_context == null) { _context = new BusinessNS.ReportAccessDomainService(); _context.IsSecure = true; } return _context; }  }
        
        #region Get Action to Business Entities
        
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TelerikReportsList> GetTelerikReportsListById([FromODataUri]string key0)
        {
            var entity = this.Context.GetTelerikReportsListByKey(key0);
            if (entity != null)
               return (new BusinessNS.TelerikReportsList[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TelerikReportsList>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TelerikReportsList> GetTelerikReportsListByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTelerikReportsListByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TelerikReportsList), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TelerikReportsList>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TelerikReportsList> GetTelerikReportsList()
        {
            return this.Context.GetTelerikReportsListByEntitySearchNoAssociations(null).AsQueryable();
        }
        #endregion
        
    }
    
    public partial class LinxReportAccessReportAccessControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return true;
        }
    }
}

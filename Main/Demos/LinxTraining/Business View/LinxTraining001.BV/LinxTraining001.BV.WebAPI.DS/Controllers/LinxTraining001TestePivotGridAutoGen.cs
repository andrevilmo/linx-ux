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
using BusinessNS = LinxTraining001.BV.TestePivotGrid;

namespace LinxTraining001.BV.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/LinxTraining001TestePivotGrid/[ActionName]
    // Entities Catalog Call: http://localhost:1710/LinxTraining001TestePivotGrid/GetEntities
    // Entity MetaData Call: http://localhost:1710/LinxTraining001TestePivotGrid/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/LinxTraining001TestePivotGrid/GetClientDomains
    // Client Service Call: http://localhost:1710/LinxTraining001TestePivotGrid/GetClientService
    // Client Factory Call: http://localhost:1710/LinxTraining001TestePivotGrid/GetClientFactory?entityName=[EntityName]
    // Client Factory Custom Events Call: http://localhost:1710/LinxTraining001TestePivotGrid/GetClientFactoryCustomEvents?entityName=[EntityName]
    // Help Call: http://localhost:1710/HelpController/LinxTraining001TestePivotGrid
    // Feed OData Call: http://localhost:1710/LinxTraining001TestePivotGridOData
    [RoutePrefix("LinxTraining001TestePivotGrid")]
    [Breeze.WebApi2.BreezeController]
    [ODataBasicAuthenticationFilter]
    public partial class LinxTraining001TestePivotGridController : ApiController
    {
        private DataServiceRepository<BusinessNS.TestePivotGridDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.TestePivotGridDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.TestePivotGridDomainService>(typeof(BusinessNS.PivoGridOlap), typeof(BusinessNS.PivotGridOlapFilha)); _repository.Context.IsSecure = true; } return _repository; } }
        public LinxTraining001TestePivotGridController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(LinxTraining001TestePivotGridController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.TestePivotGridDomainService).Assembly.FullName,
                ModelAssemblyName = ""
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
            var result = repository.Context.GetMetaDataObject("LinxTraining001.BV.TestePivotGrid." + entityName, false, true);
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
        
        [LinxTraining001TestePivotGridControllerAuthorize]
        [Route("GetAllLookUpPivoGridOlapAno"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpPivoGridOlapAno> GetAllLookUpPivoGridOlapAno()
        {
            return repository.Context.GetAllLookUpPivoGridOlapAno();
        }
        
        [LinxTraining001TestePivotGridControllerAuthorize]
        [Route("GetLookUpPivoGridOlapAnoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpPivoGridOlapAno> GetLookUpPivoGridOlapAnoByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpPivoGridOlapAnoByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [LinxTraining001TestePivotGridControllerAuthorize]
        [Route("GetAllLookUpEntityAdapter1Ano"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpEntityAdapter1Ano> GetAllLookUpEntityAdapter1Ano()
        {
            return repository.Context.GetAllLookUpEntityAdapter1Ano();
        }
        
        [LinxTraining001TestePivotGridControllerAuthorize]
        [Route("GetLookUpEntityAdapter1AnoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpEntityAdapter1Ano> GetLookUpEntityAdapter1AnoByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpEntityAdapter1AnoByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
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
        
        [LinxTraining001TestePivotGridControllerAuthorize]
        [Route("GetPivoGridOlap"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.PivoGridOlap> GetPivoGridOlap()
        {
            return repository.Context.GetPivoGridOlap().AsQueryable();
        }
        
        [LinxTraining001TestePivotGridControllerAuthorize]
        [Route("GetPivoGridOlapNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.PivoGridOlap> GetPivoGridOlapNoAssociations()
        {
            return repository.Context.GetPivoGridOlapNoAssociations().AsQueryable();
        }
        
        [LinxTraining001TestePivotGridControllerAuthorize]
        [Route("GetPivoGridOlapByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.PivoGridOlap> GetPivoGridOlapByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetPivoGridOlapByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.PivoGridOlap), jEntitySearch, false, false, true), jEntitySearch).AsQueryable();
        }
        
        [LinxTraining001TestePivotGridControllerAuthorize]
        [Route("GetPivoGridOlapByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.PivoGridOlap> GetPivoGridOlapByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetPivoGridOlapByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.PivoGridOlap), jEntitySearch, false, false, true), jEntitySearch).AsQueryable();
        }
        [Route("GetPivoGridOlapToExcel"), System.Web.Http.HttpPost()]
        [LinxTraining001TestePivotGridControllerAuthorize]
        public string GetPivoGridOlapToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.PivoGridOlap), jEntitySearch, false, false, true);
            var entities = repository.Context.GetPivoGridOlapByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("LinxTraining001.BV.TestePivotGrid.PivoGridOlap");
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
        [Route("GetPivoGridOlapToReportXml"), System.Web.Http.HttpPost()]
        [LinxTraining001TestePivotGridControllerAuthorize]
        public string GetPivoGridOlapToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.PivoGridOlap), jEntitySearch, false, false, true);
            var entities = repository.Context.GetPivoGridOlapByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("LinxTraining001.BV.TestePivotGrid.PivoGridOlap", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "LinxTraining001.BV.Reports", DataSourceFullName = "LinxTraining001.BV.Reports.TestePivotGridDataSource", DataSourceObject = "GetPivoGridOlap", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxTraining001TestePivotGridControllerAuthorize]
        [Route("GetSamplePivoGridOlap"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.PivoGridOlap> GetSamplePivoGridOlap(string details)
        {
            var result = repository.Context.GetPivoGridOlapByEntitySearchNoAssociations(null).Take(100).ToList();
               if (!details.IsNullOrEmpty())
               {
                   foreach(var entity in result)
                   {
                       entity.FillDetails(repository.Context, null, null, details.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries), 5);
                   }
               }
            return result.AsQueryable();
        }
        
        [LinxTraining001TestePivotGridControllerAuthorize]
        [Route("GetPivotGridOlapFilha"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.PivotGridOlapFilha> GetPivotGridOlapFilha()
        {
            return repository.Context.GetPivotGridOlapFilha().AsQueryable();
        }
        
        [LinxTraining001TestePivotGridControllerAuthorize]
        [Route("GetPivotGridOlapFilhaNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.PivotGridOlapFilha> GetPivotGridOlapFilhaNoAssociations()
        {
            return repository.Context.GetPivotGridOlapFilhaNoAssociations().AsQueryable();
        }
        
        [LinxTraining001TestePivotGridControllerAuthorize]
        [Route("GetPivotGridOlapFilhaByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.PivotGridOlapFilha> GetPivotGridOlapFilhaByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetPivotGridOlapFilhaByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.PivotGridOlapFilha), jEntitySearch, false, false, true), jEntitySearch).AsQueryable();
        }
        
        [LinxTraining001TestePivotGridControllerAuthorize]
        [Route("GetPivotGridOlapFilhaByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.PivotGridOlapFilha> GetPivotGridOlapFilhaByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetPivotGridOlapFilhaByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.PivotGridOlapFilha), jEntitySearch, false, false, true), jEntitySearch).AsQueryable();
        }
        [Route("GetPivotGridOlapFilhaToExcel"), System.Web.Http.HttpPost()]
        [LinxTraining001TestePivotGridControllerAuthorize]
        public string GetPivotGridOlapFilhaToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.PivotGridOlapFilha), jEntitySearch, false, false, true);
            var entities = repository.Context.GetPivotGridOlapFilhaByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("LinxTraining001.BV.TestePivotGrid.PivotGridOlapFilha");
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
        [Route("GetPivotGridOlapFilhaToReportXml"), System.Web.Http.HttpPost()]
        [LinxTraining001TestePivotGridControllerAuthorize]
        public string GetPivotGridOlapFilhaToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.PivotGridOlapFilha), jEntitySearch, false, false, true);
            var entities = repository.Context.GetPivotGridOlapFilhaByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("LinxTraining001.BV.TestePivotGrid.PivotGridOlapFilha", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "LinxTraining001.BV.Reports", DataSourceFullName = "LinxTraining001.BV.Reports.TestePivotGridDataSource", DataSourceObject = "GetPivotGridOlapFilha", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxTraining001TestePivotGridControllerAuthorize]
        [Route("GetSamplePivotGridOlapFilha"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.PivotGridOlapFilha> GetSamplePivotGridOlapFilha(string details)
        {
            var result = repository.Context.GetPivotGridOlapFilhaByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        #endregion
        
        #region Get Business Entities By Parent Composition
        #endregion
        
        #region Save Changes
        [LinxTraining001TestePivotGridControllerAuthorize]
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
    public partial class LinxTraining001TestePivotGridFeedController : ODataController
    {
        private BusinessNS.TestePivotGridDomainService _context;
        public BusinessNS.TestePivotGridDomainService Context { get {  if (_context == null) { _context = new BusinessNS.TestePivotGridDomainService(); _context.IsSecure = true; } return _context; }  }
        
        #region Get Action to Business Entities
        
        
        [LinxTraining001TestePivotGridControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.PivoGridOlap> GetPivoGridOlap(System.Guid key0)
        {
               return default(IQueryable<BusinessNS.PivoGridOlap>);
        }
        
        [LinxTraining001TestePivotGridControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.PivoGridOlap> GetPivoGridOlap()
        {
            return this.Context.GetPivoGridOlapByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxTraining001TestePivotGridControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.PivotGridOlapFilha> GetPivoGridOlap__PivotGridOlapFilha(System.Guid key0, string navigation)
        {
               return default(IQueryable<BusinessNS.PivotGridOlapFilha>);
        }
        
        [LinxTraining001TestePivotGridControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.PivotGridOlapFilha> GetPivotGridOlapFilha(System.Guid key0)
        {
               return default(IQueryable<BusinessNS.PivotGridOlapFilha>);
        }
        
        [LinxTraining001TestePivotGridControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.PivotGridOlapFilha> GetPivotGridOlapFilha()
        {
            return this.Context.GetPivotGridOlapFilhaByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxTraining001TestePivotGridControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.PivoGridOlap> GetPivotGridOlapFilha__PivoGridOlap(System.Guid key0, string navigation)
        {
               return default(IQueryable<BusinessNS.PivoGridOlap>);
        }
        #endregion
        
    }
    
    public partial class LinxTraining001TestePivotGridControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return LinxAutorization.CheckAuthorization(actionContext, string.Format("{0}#{1}#{1}/{2}", "LinxTraining001.BV", "LinxTraining001TestePivotGrid", actionContext.ActionDescriptor.ActionName));
        }
    }
}

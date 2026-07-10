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
using BusinessNS = Linx.Framework.BV.PastaUsuario;

namespace Linx.Framework.BV.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/LinxFrameworkPastaUsuario/[ActionName]
    // Entities Catalog Call: http://localhost:1710/LinxFrameworkPastaUsuario/GetEntities
    // Entity MetaData Call: http://localhost:1710/LinxFrameworkPastaUsuario/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/LinxFrameworkPastaUsuario/GetClientDomains
    // Client Service Call: http://localhost:1710/LinxFrameworkPastaUsuario/GetClientService
    // Client Factory Call: http://localhost:1710/LinxFrameworkPastaUsuario/GetClientFactory?entityName=[EntityName]
    // Client Factory Custom Events Call: http://localhost:1710/LinxFrameworkPastaUsuario/GetClientFactoryCustomEvents?entityName=[EntityName]
    // Help Call: http://localhost:1710/HelpController/LinxFrameworkPastaUsuario
    // Feed OData Call: http://localhost:1710/LinxFrameworkPastaUsuarioOData
    [RoutePrefix("LinxFrameworkPastaUsuario")]
    [Breeze.WebApi2.BreezeController]
    public partial class LinxFrameworkPastaUsuarioController : ApiController
    {
        private DataServiceRepository<BusinessNS.PastaUsuarioDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.PastaUsuarioDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.PastaUsuarioDomainService>(typeof(BusinessNS.TcsDocumentoUsuario), typeof(BusinessNS.TcsPastaUsuario)); _repository.Context.IsSecure = true; } return _repository; } }
        public LinxFrameworkPastaUsuarioController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(LinxFrameworkPastaUsuarioController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.PastaUsuarioDomainService).Assembly.FullName,
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
            var result = repository.Context.GetMetaDataObject("Linx.Framework.BV.PastaUsuario." + entityName, false, true);
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
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetReportDataSource"), System.Web.Http.HttpGet()]
        public string GetReportDataSource()
        {
            var zip = new LinxZip();
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetDomainsInfo"), System.Web.Http.HttpGet()]
        public string[] GetDomainsInfo(string domainNames)
        {
            return Linx.Framework.BV.Domains.DomainHelper.GetDomainsInfo(domainNames);
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
        
        [Route("GetTcsPastaUsuario"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPastaUsuario> GetTcsPastaUsuario()
        {
            return repository.Context.GetTcsPastaUsuario();
        }
        
        [Route("GetTcsPastaUsuarioNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPastaUsuario> GetTcsPastaUsuarioNoAssociations()
        {
            return repository.Context.GetTcsPastaUsuarioNoAssociations();
        }
        
        [Route("GetTcsPastaUsuarioByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPastaUsuario> GetTcsPastaUsuarioByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsPastaUsuarioByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPastaUsuario), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsPastaUsuarioByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPastaUsuario> GetTcsPastaUsuarioByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsPastaUsuarioByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPastaUsuario), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsPastaUsuarioToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsPastaUsuarioToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPastaUsuario), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsPastaUsuarioByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.PastaUsuario.TcsPastaUsuario");
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
        [Route("GetTcsPastaUsuarioToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsPastaUsuarioToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPastaUsuario), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsPastaUsuarioByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.PastaUsuario.TcsPastaUsuario", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.PastaUsuarioDataSource", DataSourceObject = "GetTcsPastaUsuario", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsPastaUsuario"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPastaUsuario> GetSampleTcsPastaUsuario(string details)
        {
            var result = repository.Context.GetTcsPastaUsuarioByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("GetTcsDocumentoUsuario"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsDocumentoUsuario> GetTcsDocumentoUsuario()
        {
            return repository.Context.GetTcsDocumentoUsuario();
        }
        
        [Route("GetTcsDocumentoUsuarioNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsDocumentoUsuario> GetTcsDocumentoUsuarioNoAssociations()
        {
            return repository.Context.GetTcsDocumentoUsuarioNoAssociations();
        }
        
        [Route("GetTcsDocumentoUsuarioByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsDocumentoUsuario> GetTcsDocumentoUsuarioByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsDocumentoUsuarioByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsDocumentoUsuario), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsDocumentoUsuarioByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsDocumentoUsuario> GetTcsDocumentoUsuarioByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsDocumentoUsuarioByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsDocumentoUsuario), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsDocumentoUsuarioToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsDocumentoUsuarioToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsDocumentoUsuario), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsDocumentoUsuarioByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.PastaUsuario.TcsDocumentoUsuario");
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
        [Route("GetTcsDocumentoUsuarioToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsDocumentoUsuarioToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsDocumentoUsuario), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsDocumentoUsuarioByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.PastaUsuario.TcsDocumentoUsuario", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.PastaUsuarioDataSource", DataSourceObject = "GetTcsDocumentoUsuario", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsDocumentoUsuario"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsDocumentoUsuario> GetSampleTcsDocumentoUsuario(string details)
        {
            var result = repository.Context.GetTcsDocumentoUsuarioByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
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
    
    public partial class LinxFrameworkPastaUsuarioFeedController : ODataController
    {
        private BusinessNS.PastaUsuarioDomainService _context;
        public BusinessNS.PastaUsuarioDomainService Context { get {  if (_context == null) { _context = new BusinessNS.PastaUsuarioDomainService(); _context.IsSecure = true; } return _context; }  }
        
        #region Get Action to Business Entities
        
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPastaUsuario> GetTcsPastaUsuario(System.Guid key0)
        {
            var entity = this.Context.GetTcsPastaUsuarioByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsPastaUsuario[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsPastaUsuario>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPastaUsuario> GetTcsPastaUsuario()
        {
            return this.Context.GetTcsPastaUsuarioByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsDocumentoUsuario> GetTcsDocumentoUsuario(System.Guid key0)
        {
               return default(IQueryable<BusinessNS.TcsDocumentoUsuario>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsDocumentoUsuario> GetTcsDocumentoUsuario()
        {
            return this.Context.GetTcsDocumentoUsuarioByEntitySearchNoAssociations(null).AsQueryable();
        }
        #endregion
        
    }
    
    public partial class LinxFrameworkPastaUsuarioControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return true;
        }
    }
}

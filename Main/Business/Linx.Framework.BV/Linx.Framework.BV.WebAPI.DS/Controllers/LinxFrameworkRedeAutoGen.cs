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
using BusinessNS = Linx.Framework.BV.Rede;

namespace Linx.Framework.BV.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/LinxFrameworkRede/[ActionName]
    // Security Information Call: http://localhost:1710/LinxFrameworkRede/GetSecurityInfo
    // Entities Catalog Call: http://localhost:1710/LinxFrameworkRede/GetEntities
    // Entity MetaData Call: http://localhost:1710/LinxFrameworkRede/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/LinxFrameworkRede/GetClientDomains?erp=true
    // Client Service Call: http://localhost:1710/LinxFrameworkRede/GetClientService?erp=true
    // Client Factory Call: http://localhost:1710/LinxFrameworkRede/GetClientFactory?entityName=[EntityName]&erp=true
    // Client Factory Custom Events Call: http://localhost:1710/LinxFrameworkRede/GetClientFactoryCustomEvents?entityName=[EntityName]&erp=true
    // Help Call: http://localhost:1710/HelpController/LinxFrameworkRede
    // Feed OData Call: http://localhost:1710/LinxFrameworkRedeOData
    [RoutePrefix("LinxFrameworkRede")]
    [Breeze.WebApi2.BreezeController]
    public partial class LinxFrameworkRedeController : ApiController
    {
        private const int maxObjectExcelReturned = 2300000;
        private DataServiceRepository<BusinessNS.RedeDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.RedeDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.RedeDomainService>(typeof(BusinessNS.BandeiraRedeCache), typeof(BusinessNS.TbcBandeiraRede)); _repository.Context.IsSecure = true; } return _repository; } }
        public LinxFrameworkRedeController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(LinxFrameworkRedeController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.RedeDomainService).Assembly.FullName,
                ModelAssemblyName = repository.Context.GetModelAssemblyName()
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
            var result = repository.Context.GetMetaDataObject("Linx.Framework.BV.Rede." + entityName, false, true);
            return (allComposition ? result : result.Where(e => e.ClassName == entityName).ToList());
        }
        
        [Route("GetSecurityInfo"), System.Web.Http.HttpGet()]
        public string[] GetSecurityInfo()
        {
           return new string[] { "Linx.Framework.BV", "LinxFrameworkRede", "LinxFrameworkRede/ActionName" };
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
        
        [Route("GetDomainValues"), System.Web.Http.HttpGet()]
        public Dictionary<string, string> GetDomainValues(string domainName)
        {
            return Linx.Framework.BV.Domains.DomainHelper.GetDomainValues(domainName);
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
        
        [Route("GetTbcBandeiraRede"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcBandeiraRede> GetTbcBandeiraRede()
        {
            return repository.Context.GetTbcBandeiraRede();
        }
        
        [Route("GetTbcBandeiraRedeNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcBandeiraRede> GetTbcBandeiraRedeNoAssociations()
        {
            return repository.Context.GetTbcBandeiraRedeNoAssociations();
        }
        
        [Route("GetTbcBandeiraRedeByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcBandeiraRede> GetTbcBandeiraRedeByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTbcBandeiraRedeByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcBandeiraRede), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTbcBandeiraRedeByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcBandeiraRede> GetTbcBandeiraRedeByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTbcBandeiraRedeByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcBandeiraRede), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTbcBandeiraRedeToExcel"), System.Web.Http.HttpPost()]
        public string GetTbcBandeiraRedeToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcBandeiraRede), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTbcBandeiraRedeByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdBandeiraRede asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Rede.TbcBandeiraRede");
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
               return ExcelExportPagination<BusinessNS.TbcBandeiraRede>.CreateExcelDocumentFileMapPath("TbcBandeiraRede",new ExcelExportPagination<BusinessNS.TbcBandeiraRede>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTbcBandeiraRedeToReportXml"), System.Web.Http.HttpPost()]
        public string GetTbcBandeiraRedeToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcBandeiraRede), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTbcBandeiraRedeByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Rede.TbcBandeiraRede", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.RedeDataSource", DataSourceObject = "GetTbcBandeiraRede", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTbcBandeiraRede"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcBandeiraRede> GetSampleTbcBandeiraRede(string details)
        {
            var result = repository.Context.GetTbcBandeiraRedeByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTbcBandeiraRedeEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTbcBandeiraRedeEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcBandeiraRede), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTbcBandeiraRedeByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcBandeiraRede> GetTbcBandeiraRedeByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTbcBandeiraRedeByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetBandeiraRedeCache"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.BandeiraRedeCache> GetBandeiraRedeCache()
        {
            return repository.Context.GetBandeiraRedeCache().AsQueryable();
        }
        
        [Route("GetBandeiraRedeCacheNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.BandeiraRedeCache> GetBandeiraRedeCacheNoAssociations()
        {
            return repository.Context.GetBandeiraRedeCacheNoAssociations().AsQueryable();
        }
        
        [Route("GetBandeiraRedeCacheByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.BandeiraRedeCache> GetBandeiraRedeCacheByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetBandeiraRedeCacheByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.BandeiraRedeCache), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        
        [Route("GetBandeiraRedeCacheByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.BandeiraRedeCache> GetBandeiraRedeCacheByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetBandeiraRedeCacheByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.BandeiraRedeCache), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        [Route("GetBandeiraRedeCacheToExcel"), System.Web.Http.HttpPost()]
        public string GetBandeiraRedeCacheToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.BandeiraRedeCache), jEntitySearch, false, false, false);
            var entities = repository.Context.GetBandeiraRedeCacheByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).AsQueryable().OrderBy("Hash asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Rede.BandeiraRedeCache");
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
               return ExcelExportPagination<BusinessNS.BandeiraRedeCache>.CreateExcelDocumentFileMapPath("BandeiraRedeCache",new ExcelExportPagination<BusinessNS.BandeiraRedeCache>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetBandeiraRedeCacheToReportXml"), System.Web.Http.HttpPost()]
        public string GetBandeiraRedeCacheToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.BandeiraRedeCache), jEntitySearch, false, false, false);
            var entities = repository.Context.GetBandeiraRedeCacheByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Rede.BandeiraRedeCache", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.RedeDataSource", DataSourceObject = "GetBandeiraRedeCache", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleBandeiraRedeCache"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.BandeiraRedeCache> GetSampleBandeiraRedeCache(string details)
        {
            var result = repository.Context.GetBandeiraRedeCacheByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddBandeiraRedeCacheEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddBandeiraRedeCacheEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.BandeiraRedeCache), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetBandeiraRedeCacheByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.BandeiraRedeCache> GetBandeiraRedeCacheByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetBandeiraRedeCacheByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
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
    
    public partial class LinxFrameworkRedeFeedController : ODataController
    {
        private BusinessNS.RedeDomainService _context;
        public BusinessNS.RedeDomainService Context { get {  if (_context == null) { _context = new BusinessNS.RedeDomainService(); _context.IsSecure = true; } return _context; }  }
        
        #region Get Action to Business Entities
        
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TbcBandeiraRede> GetTbcBandeiraRedeById([FromODataUri]Int32 key0)
        {
            var entity = this.Context.GetTbcBandeiraRedeByKey(key0);
            if (entity != null)
               return (new BusinessNS.TbcBandeiraRede[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TbcBandeiraRede>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TbcBandeiraRede> GetTbcBandeiraRedeByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTbcBandeiraRedeByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcBandeiraRede), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TbcBandeiraRede>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TbcBandeiraRede> GetTbcBandeiraRede()
        {
            return this.Context.GetTbcBandeiraRedeByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.BandeiraRedeCache> GetBandeiraRedeCacheById([FromODataUri]string key0)
        {
            var entity = this.Context.GetBandeiraRedeCacheByKey(key0);
            if (entity != null)
               return (new BusinessNS.BandeiraRedeCache[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.BandeiraRedeCache>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.BandeiraRedeCache> GetBandeiraRedeCacheByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetBandeiraRedeCacheByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.BandeiraRedeCache), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.BandeiraRedeCache>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.BandeiraRedeCache> GetBandeiraRedeCache()
        {
            return this.Context.GetBandeiraRedeCacheByEntitySearchNoAssociations(null).AsQueryable();
        }
        #endregion
        
    }
    
    public partial class LinxFrameworkRedeControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return true;
        }
    }
}

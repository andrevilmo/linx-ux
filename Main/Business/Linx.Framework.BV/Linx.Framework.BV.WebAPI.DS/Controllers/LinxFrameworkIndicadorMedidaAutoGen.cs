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
using BusinessNS = Linx.Framework.BV.IndicadorMedida;

namespace Linx.Framework.BV.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/LinxFrameworkIndicadorMedida/[ActionName]
    // Security Information Call: http://localhost:1710/LinxFrameworkIndicadorMedida/GetSecurityInfo
    // Entities Catalog Call: http://localhost:1710/LinxFrameworkIndicadorMedida/GetEntities
    // Entity MetaData Call: http://localhost:1710/LinxFrameworkIndicadorMedida/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/LinxFrameworkIndicadorMedida/GetClientDomains?erp=true
    // Client Service Call: http://localhost:1710/LinxFrameworkIndicadorMedida/GetClientService?erp=true
    // Client Factory Call: http://localhost:1710/LinxFrameworkIndicadorMedida/GetClientFactory?entityName=[EntityName]&erp=true
    // Client Factory Custom Events Call: http://localhost:1710/LinxFrameworkIndicadorMedida/GetClientFactoryCustomEvents?entityName=[EntityName]&erp=true
    // Help Call: http://localhost:1710/HelpController/LinxFrameworkIndicadorMedida
    // Feed OData Call: http://localhost:1710/LinxFrameworkIndicadorMedidaOData
    [RoutePrefix("LinxFrameworkIndicadorMedida")]
    [Breeze.WebApi2.BreezeController]
    public partial class LinxFrameworkIndicadorMedidaController : ApiController
    {
        private const int maxObjectExcelReturned = 2300000;
        private DataServiceRepository<BusinessNS.IndicadorMedidaDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.IndicadorMedidaDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.IndicadorMedidaDomainService>(typeof(BusinessNS.TcsIndicadorIndice), typeof(BusinessNS.TcsIndicadorMedida)); _repository.Context.IsSecure = true; } return _repository; } }
        public LinxFrameworkIndicadorMedidaController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(LinxFrameworkIndicadorMedidaController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.IndicadorMedidaDomainService).Assembly.FullName,
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
            var result = repository.Context.GetMetaDataObject("Linx.Framework.BV.IndicadorMedida." + entityName, false, true);
            return (allComposition ? result : result.Where(e => e.ClassName == entityName).ToList());
        }
        
        [Route("GetSecurityInfo"), System.Web.Http.HttpGet()]
        public string[] GetSecurityInfo()
        {
           return new string[] { "Linx.Framework.BV", "LinxFrameworkIndicadorMedida", "LinxFrameworkIndicadorMedida/ActionName" };
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
        
        [Route("GetTcsIndicadorMedida"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsIndicadorMedida> GetTcsIndicadorMedida()
        {
            return repository.Context.GetTcsIndicadorMedida();
        }
        
        [Route("GetTcsIndicadorMedidaNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsIndicadorMedida> GetTcsIndicadorMedidaNoAssociations()
        {
            return repository.Context.GetTcsIndicadorMedidaNoAssociations();
        }
        
        [Route("GetTcsIndicadorMedidaByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsIndicadorMedida> GetTcsIndicadorMedidaByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsIndicadorMedidaByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsIndicadorMedida), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsIndicadorMedidaByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsIndicadorMedida> GetTcsIndicadorMedidaByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsIndicadorMedidaByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsIndicadorMedida), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsIndicadorMedidaToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsIndicadorMedidaToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsIndicadorMedida), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsIndicadorMedidaByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdIndicadorMedida asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.IndicadorMedida.TcsIndicadorMedida");
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
               return ExcelExportPagination<BusinessNS.TcsIndicadorMedida>.CreateExcelDocumentFileMapPath("TcsIndicadorMedida",new ExcelExportPagination<BusinessNS.TcsIndicadorMedida>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsIndicadorMedidaToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsIndicadorMedidaToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsIndicadorMedida), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsIndicadorMedidaByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.IndicadorMedida.TcsIndicadorMedida", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.IndicadorMedidaDataSource", DataSourceObject = "GetTcsIndicadorMedida", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsIndicadorMedida"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsIndicadorMedida> GetSampleTcsIndicadorMedida(string details)
        {
            var result = repository.Context.GetTcsIndicadorMedidaByEntitySearchNoAssociations(null).Take(100).ToList();
               if (!details.IsNullOrEmpty())
               {
                   foreach(var entity in result)
                   {
                       entity.FillDetails(repository.Context, null, null, details.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries), 5);
                   }
               }
            return result.AsQueryable();
        }
        
        [Route("AddTcsIndicadorMedidaEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsIndicadorMedidaEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsIndicadorMedida), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsIndicadorMedidaByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsIndicadorMedida> GetTcsIndicadorMedidaByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsIndicadorMedidaByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsIndicadorIndice"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsIndicadorIndice> GetTcsIndicadorIndice()
        {
            return repository.Context.GetTcsIndicadorIndice();
        }
        
        [Route("GetTcsIndicadorIndiceNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsIndicadorIndice> GetTcsIndicadorIndiceNoAssociations()
        {
            return repository.Context.GetTcsIndicadorIndiceNoAssociations();
        }
        
        [Route("GetTcsIndicadorIndiceByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsIndicadorIndice> GetTcsIndicadorIndiceByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsIndicadorIndiceByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsIndicadorIndice), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsIndicadorIndiceByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsIndicadorIndice> GetTcsIndicadorIndiceByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsIndicadorIndiceByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsIndicadorIndice), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsIndicadorIndiceToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsIndicadorIndiceToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsIndicadorIndice), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsIndicadorIndiceByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdIndiceMedida asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.IndicadorMedida.TcsIndicadorIndice");
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
               return ExcelExportPagination<BusinessNS.TcsIndicadorIndice>.CreateExcelDocumentFileMapPath("TcsIndicadorIndice",new ExcelExportPagination<BusinessNS.TcsIndicadorIndice>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsIndicadorIndiceToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsIndicadorIndiceToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsIndicadorIndice), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsIndicadorIndiceByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.IndicadorMedida.TcsIndicadorIndice", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.IndicadorMedidaDataSource", DataSourceObject = "GetTcsIndicadorIndice", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsIndicadorIndice"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsIndicadorIndice> GetSampleTcsIndicadorIndice(string details)
        {
            var result = repository.Context.GetTcsIndicadorIndiceByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsIndicadorIndiceEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsIndicadorIndiceEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsIndicadorIndice), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsIndicadorIndiceByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsIndicadorIndice> GetTcsIndicadorIndiceByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsIndicadorIndiceByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        #endregion
        
        #region Get Business Entities By Parent Composition
        
        [Route("GetTcsIndicadorIndiceParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsIndicadorIndiceParentComposition> GetTcsIndicadorIndiceParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsIndicadorIndiceParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsIndicadorIndiceParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsIndicadorIndiceParentCompositionToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsIndicadorIndiceParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("TcsIndicadorIndice{", "TcsIndicadorIndiceParentComposition{");
            jEntitySearch = jEntitySearch.Replace("TcsIndicadorMedida{", "TcsIndicadorIndiceParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsIndicadorIndiceParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsIndicadorIndiceParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdIndiceMedida asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.IndicadorMedida.TcsIndicadorIndice");
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
               return ExcelExportPagination<BusinessNS.TcsIndicadorIndiceParentComposition>.CreateExcelDocumentFileMapPath("TcsIndicadorIndice",new ExcelExportPagination<BusinessNS.TcsIndicadorIndiceParentComposition>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsIndicadorIndiceParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsIndicadorIndiceParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsIndicadorIndiceParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsIndicadorIndiceParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.IndicadorMedida.TcsIndicadorIndice", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.IndicadorMedidaDataSource", DataSourceObject = "GetTcsIndicadorIndiceParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsIndicadorIndiceParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsIndicadorIndiceParentComposition> GetSampleTcsIndicadorIndiceParentComposition(string details)
        {
            var result = repository.Context.GetTcsIndicadorIndiceParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
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
    
    public partial class LinxFrameworkIndicadorMedidaFeedController : ODataController
    {
        private BusinessNS.IndicadorMedidaDomainService _context;
        public BusinessNS.IndicadorMedidaDomainService Context { get {  if (_context == null) { _context = new BusinessNS.IndicadorMedidaDomainService(); _context.IsSecure = true; } return _context; }  }
        
        #region Get Action to Business Entities
        
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsIndicadorMedida> GetTcsIndicadorMedidaById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetTcsIndicadorMedidaByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsIndicadorMedida[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsIndicadorMedida>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsIndicadorMedida> GetTcsIndicadorMedidaByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsIndicadorMedidaByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsIndicadorMedida), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsIndicadorMedida>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsIndicadorMedida> GetTcsIndicadorMedida()
        {
            return this.Context.GetTcsIndicadorMedidaByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsIndicadorIndice> GetTcsIndicadorMedida__TcsIndicadorIndice(Int64 key0, string navigation)
        {
            var entity = this.Context.GetTcsIndicadorMedidaByKey(key0);
            if (entity != null && navigation == "TcsIndicadorIndiceList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsIndicadorIndice" });
               return entity.TcsIndicadorIndiceList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsIndicadorIndice>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsIndicadorIndice> GetTcsIndicadorIndiceById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetTcsIndicadorIndiceByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsIndicadorIndice[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsIndicadorIndice>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsIndicadorIndice> GetTcsIndicadorIndiceByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsIndicadorIndiceByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsIndicadorIndice), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsIndicadorIndice>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsIndicadorIndice> GetTcsIndicadorIndice()
        {
            return this.Context.GetTcsIndicadorIndiceByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsIndicadorIndiceParentComposition> GetTcsIndicadorIndiceParentComposition()
        {
            return this.Context.GetTcsIndicadorIndiceParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsIndicadorIndiceParentComposition> GetTcsIndicadorIndiceParentCompositionByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                jEntitySearch = jEntitySearch.Replace("TcsIndicadorIndice{", "TcsIndicadorIndiceParentComposition{");
                jEntitySearch = jEntitySearch.Replace("TcsIndicadorMedida{", "TcsIndicadorIndiceParentComposition{");
                var entity = this.Context.GetTcsIndicadorIndiceParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsIndicadorIndiceParentComposition), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsIndicadorIndiceParentComposition>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsIndicadorMedida> GetTcsIndicadorIndice__TcsIndicadorMedida(Int64 key0, string navigation)
        {
            var entity = this.Context.GetTcsIndicadorIndiceByKey(key0);
            if (entity != null && navigation == "TcsIndicadorMedida")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsIndicadorMedida[] { entity.TcsIndicadorMedida }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsIndicadorMedida>);
        }
        #endregion
        
    }
    
    public partial class LinxFrameworkIndicadorMedidaControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return true;
        }
    }
}

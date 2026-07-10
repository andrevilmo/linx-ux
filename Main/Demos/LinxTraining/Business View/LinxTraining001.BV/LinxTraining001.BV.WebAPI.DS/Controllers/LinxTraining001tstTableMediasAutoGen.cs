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
using BusinessNS = LinxTraining001.BV.tstTableMedias;

namespace LinxTraining001.BV.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/LinxTraining001tstTableMedias/[ActionName]
    // Entities Catalog Call: http://localhost:1710/LinxTraining001tstTableMedias/GetEntities
    // Entity MetaData Call: http://localhost:1710/LinxTraining001tstTableMedias/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/LinxTraining001tstTableMedias/GetClientDomains
    // Client Service Call: http://localhost:1710/LinxTraining001tstTableMedias/GetClientService
    // Client Factory Call: http://localhost:1710/LinxTraining001tstTableMedias/GetClientFactory?entityName=[EntityName]
    // Client Factory Custom Events Call: http://localhost:1710/LinxTraining001tstTableMedias/GetClientFactoryCustomEvents?entityName=[EntityName]
    // Help Call: http://localhost:1710/HelpController/LinxTraining001tstTableMedias
    // Feed OData Call: http://localhost:1710/LinxTraining001tstTableMediasOData
    [RoutePrefix("LinxTraining001tstTableMedias")]
    [Breeze.WebApi2.BreezeController]
    [ODataBasicAuthenticationFilter]
    public partial class LinxTraining001tstTableMediasController : ApiController
    {
        private DataServiceRepository<BusinessNS.tstTableMediasDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.tstTableMediasDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.tstTableMediasDomainService>(typeof(BusinessNS.ClientesView), typeof(BusinessNS.VendasView)); _repository.Context.IsSecure = true; } return _repository; } }
        public LinxTraining001tstTableMediasController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(LinxTraining001tstTableMediasController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.tstTableMediasDomainService).Assembly.FullName,
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
            var result = repository.Context.GetMetaDataObject("LinxTraining001.BV.tstTableMedias." + entityName, false, true);
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
        
        [LinxTraining001tstTableMediasControllerAuthorize]
        [Route("GetClientesView"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.ClientesView> GetClientesView()
        {
            return repository.Context.GetClientesView();
        }
        
        [LinxTraining001tstTableMediasControllerAuthorize]
        [Route("GetClientesViewNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.ClientesView> GetClientesViewNoAssociations()
        {
            return repository.Context.GetClientesViewNoAssociations();
        }
        
        [LinxTraining001tstTableMediasControllerAuthorize]
        [Route("GetClientesViewByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.ClientesView> GetClientesViewByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetClientesViewByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.ClientesView), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [LinxTraining001tstTableMediasControllerAuthorize]
        [Route("GetClientesViewByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.ClientesView> GetClientesViewByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetClientesViewByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.ClientesView), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetClientesViewToExcel"), System.Web.Http.HttpPost()]
        [LinxTraining001tstTableMediasControllerAuthorize]
        public string GetClientesViewToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.ClientesView), jEntitySearch, false, false, false);
            var entities = repository.Context.GetClientesViewByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("LinxTraining001.BV.tstTableMedias.ClientesView");
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
        [Route("GetClientesViewToReportXml"), System.Web.Http.HttpPost()]
        [LinxTraining001tstTableMediasControllerAuthorize]
        public string GetClientesViewToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.ClientesView), jEntitySearch, false, false, false);
            var entities = repository.Context.GetClientesViewByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("LinxTraining001.BV.tstTableMedias.ClientesView", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "LinxTraining001.BV.Reports", DataSourceFullName = "LinxTraining001.BV.Reports.tstTableMediasDataSource", DataSourceObject = "GetClientesView", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxTraining001tstTableMediasControllerAuthorize]
        [Route("GetSampleClientesView"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.ClientesView> GetSampleClientesView(string details)
        {
            var result = repository.Context.GetClientesViewByEntitySearchNoAssociations(null).Take(100).ToList();
               if (!details.IsNullOrEmpty())
               {
                   foreach(var entity in result)
                   {
                       entity.FillDetails(repository.Context, null, null, details.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries), 5);
                   }
               }
            return result.AsQueryable();
        }
        
        [LinxTraining001tstTableMediasControllerAuthorize]
        [Route("GetVendasView"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendasView> GetVendasView()
        {
            return repository.Context.GetVendasView();
        }
        
        [LinxTraining001tstTableMediasControllerAuthorize]
        [Route("GetVendasViewNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendasView> GetVendasViewNoAssociations()
        {
            return repository.Context.GetVendasViewNoAssociations();
        }
        
        [LinxTraining001tstTableMediasControllerAuthorize]
        [Route("GetVendasViewByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendasView> GetVendasViewByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetVendasViewByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendasView), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [LinxTraining001tstTableMediasControllerAuthorize]
        [Route("GetVendasViewByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendasView> GetVendasViewByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetVendasViewByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendasView), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetVendasViewToExcel"), System.Web.Http.HttpPost()]
        [LinxTraining001tstTableMediasControllerAuthorize]
        public string GetVendasViewToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendasView), jEntitySearch, false, false, false);
            var entities = repository.Context.GetVendasViewByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("LinxTraining001.BV.tstTableMedias.VendasView");
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
        [LinxTraining001tstTableMediasControllerAuthorize]
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
            var metadata = repository.Context.GetMetaDataObject("LinxTraining001.BV.tstTableMedias.VendasView", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "LinxTraining001.BV.Reports", DataSourceFullName = "LinxTraining001.BV.Reports.tstTableMediasDataSource", DataSourceObject = "GetVendasView", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxTraining001tstTableMediasControllerAuthorize]
        [Route("GetSampleVendasView"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendasView> GetSampleVendasView(string details)
        {
            var result = repository.Context.GetVendasViewByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        #endregion
        
        #region Get Business Entities By Parent Composition
        
        [LinxTraining001tstTableMediasControllerAuthorize]
        [Route("GetVendasViewParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendasViewParentComposition> GetVendasViewParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetVendasViewParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendasViewParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetVendasViewParentCompositionToExcel"), System.Web.Http.HttpPost()]
        [LinxTraining001tstTableMediasControllerAuthorize]
        public string GetVendasViewParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            jEntitySearch = jEntitySearch.Replace("VendasView{", "VendasViewParentComposition{");
            jEntitySearch = jEntitySearch.Replace("ClientesView{", "VendasViewParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendasViewParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetVendasViewParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("LinxTraining001.BV.tstTableMedias.VendasView");
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
        [Route("GetVendasViewParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        [LinxTraining001tstTableMediasControllerAuthorize]
        public string GetVendasViewParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendasViewParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetVendasViewParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("LinxTraining001.BV.tstTableMedias.VendasView", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "LinxTraining001.BV.Reports", DataSourceFullName = "LinxTraining001.BV.Reports.tstTableMediasDataSource", DataSourceObject = "GetVendasViewParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxTraining001tstTableMediasControllerAuthorize]
        [Route("GetSampleVendasViewParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendasViewParentComposition> GetSampleVendasViewParentComposition(string details)
        {
            var result = repository.Context.GetVendasViewParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        #endregion
        
        #region Save Changes
        [LinxTraining001tstTableMediasControllerAuthorize]
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
    public partial class LinxTraining001tstTableMediasFeedController : ODataController
    {
        private BusinessNS.tstTableMediasDomainService _context;
        public BusinessNS.tstTableMediasDomainService Context { get {  if (_context == null) { _context = new BusinessNS.tstTableMediasDomainService(); _context.IsSecure = true; } return _context; }  }
        
        #region Get Action to Business Entities
        
        
        [LinxTraining001tstTableMediasControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.ClientesView> GetClientesView(System.Guid key0)
        {
            var entity = this.Context.GetClientesViewByKey(key0);
            if (entity != null)
               return (new BusinessNS.ClientesView[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.ClientesView>);
        }
        
        [LinxTraining001tstTableMediasControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.ClientesView> GetClientesView()
        {
            return this.Context.GetClientesViewByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxTraining001tstTableMediasControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendasView> GetClientesView__VendasView(System.Guid key0, string navigation)
        {
            var entity = this.Context.GetClientesViewByKey(key0);
            if (entity != null && navigation == "VendasViewList")
            {
               entity.FillDetails(_context, null, null, new string[] { "VendasView" });
               return entity.VendasViewList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.VendasView>);
        }
        
        [LinxTraining001tstTableMediasControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendasView> GetVendasView(Int32 key0)
        {
            var entity = this.Context.GetVendasViewByKey(key0);
            if (entity != null)
               return (new BusinessNS.VendasView[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.VendasView>);
        }
        
        [LinxTraining001tstTableMediasControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendasView> GetVendasView()
        {
            return this.Context.GetVendasViewByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxTraining001tstTableMediasControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendasViewParentComposition> GetVendasViewParentComposition()
        {
            return this.Context.GetVendasViewParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxTraining001tstTableMediasControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.ClientesView> GetVendasView__ClientesView(Int32 key0, string navigation)
        {
            var entity = this.Context.GetVendasViewByKey(key0);
            if (entity != null && navigation == "ClientesView")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.ClientesView[] { entity.ClientesView }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.ClientesView>);
        }
        #endregion
        
    }
    
    public partial class LinxTraining001tstTableMediasControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return LinxAutorization.CheckAuthorization(actionContext, string.Format("{0}#{1}#{1}/{2}", "LinxTraining001.BV", "LinxTraining001tstTableMedias", actionContext.ActionDescriptor.ActionName));
        }
    }
}

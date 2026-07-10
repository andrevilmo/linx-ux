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
using BusinessNS = LinxTraining001.BV.DetalhamentoVenda;

namespace LinxTraining001.BV.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/LinxTraining001DetalhamentoVenda/[ActionName]
    // Entities Catalog Call: http://localhost:1710/LinxTraining001DetalhamentoVenda/GetEntities
    // Entity MetaData Call: http://localhost:1710/LinxTraining001DetalhamentoVenda/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/LinxTraining001DetalhamentoVenda/GetClientDomains
    // Client Service Call: http://localhost:1710/LinxTraining001DetalhamentoVenda/GetClientService
    // Client Factory Call: http://localhost:1710/LinxTraining001DetalhamentoVenda/GetClientFactory?entityName=[EntityName]
    // Client Factory Custom Events Call: http://localhost:1710/LinxTraining001DetalhamentoVenda/GetClientFactoryCustomEvents?entityName=[EntityName]
    // Help Call: http://localhost:1710/HelpController/LinxTraining001DetalhamentoVenda
    // Feed OData Call: http://localhost:1710/LinxTraining001DetalhamentoVendaOData
    [RoutePrefix("LinxTraining001DetalhamentoVenda")]
    [Breeze.WebApi2.BreezeController]
    [ODataBasicAuthenticationFilter]
    public partial class LinxTraining001DetalhamentoVendaController : ApiController
    {
        private DataServiceRepository<BusinessNS.DetalhamentoVendaDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.DetalhamentoVendaDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.DetalhamentoVendaDomainService>(typeof(BusinessNS.VendaDetalheView), typeof(BusinessNS.VendasView)); _repository.Context.IsSecure = true; } return _repository; } }
        public LinxTraining001DetalhamentoVendaController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(LinxTraining001DetalhamentoVendaController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.DetalhamentoVendaDomainService).Assembly.FullName,
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
            var result = repository.Context.GetMetaDataObject("LinxTraining001.BV.DetalhamentoVenda." + entityName, false, true);
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
        
        [LinxTraining001DetalhamentoVendaControllerAuthorize]
        [Route("GetAllLookUpClientes"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpClientes> GetAllLookUpClientes()
        {
            return repository.Context.GetAllLookUpClientes();
        }
        
        [LinxTraining001DetalhamentoVendaControllerAuthorize]
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
        
        [LinxTraining001DetalhamentoVendaControllerAuthorize]
        [Route("GetVendasView"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendasView> GetVendasView()
        {
            return repository.Context.GetVendasView();
        }
        
        [LinxTraining001DetalhamentoVendaControllerAuthorize]
        [Route("GetVendasViewNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendasView> GetVendasViewNoAssociations()
        {
            return repository.Context.GetVendasViewNoAssociations();
        }
        
        [LinxTraining001DetalhamentoVendaControllerAuthorize]
        [Route("GetVendasViewByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendasView> GetVendasViewByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetVendasViewByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendasView), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [LinxTraining001DetalhamentoVendaControllerAuthorize]
        [Route("GetVendasViewByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendasView> GetVendasViewByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetVendasViewByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendasView), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetVendasViewToExcel"), System.Web.Http.HttpPost()]
        [LinxTraining001DetalhamentoVendaControllerAuthorize]
        public string GetVendasViewToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendasView), jEntitySearch, false, false, false);
            var entities = repository.Context.GetVendasViewByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("LinxTraining001.BV.DetalhamentoVenda.VendasView");
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
        [LinxTraining001DetalhamentoVendaControllerAuthorize]
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
            var metadata = repository.Context.GetMetaDataObject("LinxTraining001.BV.DetalhamentoVenda.VendasView", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "LinxTraining001.BV.Reports", DataSourceFullName = "LinxTraining001.BV.Reports.DetalhamentoVendaDataSource", DataSourceObject = "GetVendasView", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxTraining001DetalhamentoVendaControllerAuthorize]
        [Route("GetSampleVendasView"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendasView> GetSampleVendasView(string details)
        {
            var result = repository.Context.GetVendasViewByEntitySearchNoAssociations(null).Take(100).ToList();
               if (!details.IsNullOrEmpty())
               {
                   foreach(var entity in result)
                   {
                       entity.FillDetails(repository.Context, null, null, details.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries), 5);
                   }
               }
            return result.AsQueryable();
        }
        
        [LinxTraining001DetalhamentoVendaControllerAuthorize]
        [Route("GetVendaDetalheView"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaDetalheView> GetVendaDetalheView()
        {
            return repository.Context.GetVendaDetalheView();
        }
        
        [LinxTraining001DetalhamentoVendaControllerAuthorize]
        [Route("GetVendaDetalheViewNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaDetalheView> GetVendaDetalheViewNoAssociations()
        {
            return repository.Context.GetVendaDetalheViewNoAssociations();
        }
        
        [LinxTraining001DetalhamentoVendaControllerAuthorize]
        [Route("GetVendaDetalheViewByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaDetalheView> GetVendaDetalheViewByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetVendaDetalheViewByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaDetalheView), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [LinxTraining001DetalhamentoVendaControllerAuthorize]
        [Route("GetVendaDetalheViewByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaDetalheView> GetVendaDetalheViewByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetVendaDetalheViewByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaDetalheView), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetVendaDetalheViewToExcel"), System.Web.Http.HttpPost()]
        [LinxTraining001DetalhamentoVendaControllerAuthorize]
        public string GetVendaDetalheViewToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaDetalheView), jEntitySearch, false, false, false);
            var entities = repository.Context.GetVendaDetalheViewByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("LinxTraining001.BV.DetalhamentoVenda.VendaDetalheView");
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
        [Route("GetVendaDetalheViewToReportXml"), System.Web.Http.HttpPost()]
        [LinxTraining001DetalhamentoVendaControllerAuthorize]
        public string GetVendaDetalheViewToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaDetalheView), jEntitySearch, false, false, false);
            var entities = repository.Context.GetVendaDetalheViewByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("LinxTraining001.BV.DetalhamentoVenda.VendaDetalheView", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "LinxTraining001.BV.Reports", DataSourceFullName = "LinxTraining001.BV.Reports.DetalhamentoVendaDataSource", DataSourceObject = "GetVendaDetalheView", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxTraining001DetalhamentoVendaControllerAuthorize]
        [Route("GetSampleVendaDetalheView"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaDetalheView> GetSampleVendaDetalheView(string details)
        {
            var result = repository.Context.GetVendaDetalheViewByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        #endregion
        
        #region Get Business Entities By Parent Composition
        
        [LinxTraining001DetalhamentoVendaControllerAuthorize]
        [Route("GetVendaDetalheViewParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaDetalheViewParentComposition> GetVendaDetalheViewParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetVendaDetalheViewParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaDetalheViewParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetVendaDetalheViewParentCompositionToExcel"), System.Web.Http.HttpPost()]
        [LinxTraining001DetalhamentoVendaControllerAuthorize]
        public string GetVendaDetalheViewParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            jEntitySearch = jEntitySearch.Replace("VendaDetalheView{", "VendaDetalheViewParentComposition{");
            jEntitySearch = jEntitySearch.Replace("VendasView{", "VendaDetalheViewParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaDetalheViewParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetVendaDetalheViewParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("LinxTraining001.BV.DetalhamentoVenda.VendaDetalheView");
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
        [Route("GetVendaDetalheViewParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        [LinxTraining001DetalhamentoVendaControllerAuthorize]
        public string GetVendaDetalheViewParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaDetalheViewParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetVendaDetalheViewParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("LinxTraining001.BV.DetalhamentoVenda.VendaDetalheView", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "LinxTraining001.BV.Reports", DataSourceFullName = "LinxTraining001.BV.Reports.DetalhamentoVendaDataSource", DataSourceObject = "GetVendaDetalheViewParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxTraining001DetalhamentoVendaControllerAuthorize]
        [Route("GetSampleVendaDetalheViewParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaDetalheViewParentComposition> GetSampleVendaDetalheViewParentComposition(string details)
        {
            var result = repository.Context.GetVendaDetalheViewParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        #endregion
        
        #region Save Changes
        [LinxTraining001DetalhamentoVendaControllerAuthorize]
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
    public partial class LinxTraining001DetalhamentoVendaFeedController : ODataController
    {
        private BusinessNS.DetalhamentoVendaDomainService _context;
        public BusinessNS.DetalhamentoVendaDomainService Context { get {  if (_context == null) { _context = new BusinessNS.DetalhamentoVendaDomainService(); _context.IsSecure = true; } return _context; }  }
        
        #region Get Action to Business Entities
        
        
        [LinxTraining001DetalhamentoVendaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendasView> GetVendasView(Int32 key0)
        {
            var entity = this.Context.GetVendasViewByKey(key0);
            if (entity != null)
               return (new BusinessNS.VendasView[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.VendasView>);
        }
        
        [LinxTraining001DetalhamentoVendaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendasView> GetVendasView()
        {
            return this.Context.GetVendasViewByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxTraining001DetalhamentoVendaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaDetalheView> GetVendasView__VendaDetalheView(Int32 key0, string navigation)
        {
            var entity = this.Context.GetVendasViewByKey(key0);
            if (entity != null && navigation == "VendaDetalheViewList")
            {
               entity.FillDetails(_context, null, null, new string[] { "VendaDetalheView" });
               return entity.VendaDetalheViewList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.VendaDetalheView>);
        }
        
        [LinxTraining001DetalhamentoVendaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaDetalheView> GetVendaDetalheView(Int32 key0)
        {
            var entity = this.Context.GetVendaDetalheViewByKey(key0);
            if (entity != null)
               return (new BusinessNS.VendaDetalheView[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.VendaDetalheView>);
        }
        
        [LinxTraining001DetalhamentoVendaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaDetalheView> GetVendaDetalheView()
        {
            return this.Context.GetVendaDetalheViewByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxTraining001DetalhamentoVendaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaDetalheViewParentComposition> GetVendaDetalheViewParentComposition()
        {
            return this.Context.GetVendaDetalheViewParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxTraining001DetalhamentoVendaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendasView> GetVendaDetalheView__VendasView(Int32 key0, string navigation)
        {
            var entity = this.Context.GetVendaDetalheViewByKey(key0);
            if (entity != null && navigation == "VendasView")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.VendasView[] { entity.VendasView }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.VendasView>);
        }
        #endregion
        
    }
    
    public partial class LinxTraining001DetalhamentoVendaControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return LinxAutorization.CheckAuthorization(actionContext, string.Format("{0}#{1}#{1}/{2}", "LinxTraining001.BV", "LinxTraining001DetalhamentoVenda", actionContext.ActionDescriptor.ActionName));
        }
    }
}

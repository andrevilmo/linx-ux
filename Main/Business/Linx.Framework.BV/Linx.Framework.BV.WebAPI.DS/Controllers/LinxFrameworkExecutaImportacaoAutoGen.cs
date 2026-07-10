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
using BusinessNS = Linx.Framework.BV.ExecutaImportacao;

namespace Linx.Framework.BV.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/LinxFrameworkExecutaImportacao/[ActionName]
    // Entities Catalog Call: http://localhost:1710/LinxFrameworkExecutaImportacao/GetEntities
    // Entity MetaData Call: http://localhost:1710/LinxFrameworkExecutaImportacao/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/LinxFrameworkExecutaImportacao/GetClientDomains
    // Client Service Call: http://localhost:1710/LinxFrameworkExecutaImportacao/GetClientService
    // Client Factory Call: http://localhost:1710/LinxFrameworkExecutaImportacao/GetClientFactory?entityName=[EntityName]
    // Client Factory Custom Events Call: http://localhost:1710/LinxFrameworkExecutaImportacao/GetClientFactoryCustomEvents?entityName=[EntityName]
    // Help Call: http://localhost:1710/HelpController/LinxFrameworkExecutaImportacao
    // Feed OData Call: http://localhost:1710/LinxFrameworkExecutaImportacaoOData
    [RoutePrefix("LinxFrameworkExecutaImportacao")]
    [Breeze.WebApi2.BreezeController]
    public partial class LinxFrameworkExecutaImportacaoController : ApiController
    {
        private DataServiceRepository<BusinessNS.ExecutaImportacaoDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.ExecutaImportacaoDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.ExecutaImportacaoDomainService>(typeof(BusinessNS.TcsArquivo), typeof(BusinessNS.TcsArquivoImportar), typeof(BusinessNS.TcsArquivoLog)); _repository.Context.IsSecure = true; } return _repository; } }
        public LinxFrameworkExecutaImportacaoController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(LinxFrameworkExecutaImportacaoController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.ExecutaImportacaoDomainService).Assembly.FullName,
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
            var result = repository.Context.GetMetaDataObject("Linx.Framework.BV.ExecutaImportacao." + entityName, false, true);
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
        
        [Route("GetTcsArquivoImportar"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsArquivoImportar> GetTcsArquivoImportar()
        {
            return repository.Context.GetTcsArquivoImportar();
        }
        
        [Route("GetTcsArquivoImportarNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsArquivoImportar> GetTcsArquivoImportarNoAssociations()
        {
            return repository.Context.GetTcsArquivoImportarNoAssociations();
        }
        
        [Route("GetTcsArquivoImportarByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsArquivoImportar> GetTcsArquivoImportarByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsArquivoImportarByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsArquivoImportar), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsArquivoImportarByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsArquivoImportar> GetTcsArquivoImportarByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsArquivoImportarByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsArquivoImportar), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsArquivoImportarToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsArquivoImportarToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsArquivoImportar), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsArquivoImportarByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.ExecutaImportacao.TcsArquivoImportar");
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
        [Route("GetTcsArquivoImportarToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsArquivoImportarToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsArquivoImportar), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsArquivoImportarByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.ExecutaImportacao.TcsArquivoImportar", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ExecutaImportacaoDataSource", DataSourceObject = "GetTcsArquivoImportar", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsArquivoImportar"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsArquivoImportar> GetSampleTcsArquivoImportar(string details)
        {
            var result = repository.Context.GetTcsArquivoImportarByEntitySearchNoAssociations(null).Take(100).ToList();
               if (!details.IsNullOrEmpty())
               {
                   foreach(var entity in result)
                   {
                       entity.FillDetails(repository.Context, null, null, details.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries), 5);
                   }
               }
            return result.AsQueryable();
        }
        
        [Route("GetTcsArquivoLog"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsArquivoLog> GetTcsArquivoLog()
        {
            return repository.Context.GetTcsArquivoLog();
        }
        
        [Route("GetTcsArquivoLogNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsArquivoLog> GetTcsArquivoLogNoAssociations()
        {
            return repository.Context.GetTcsArquivoLogNoAssociations();
        }
        
        [Route("GetTcsArquivoLogByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsArquivoLog> GetTcsArquivoLogByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsArquivoLogByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsArquivoLog), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsArquivoLogByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsArquivoLog> GetTcsArquivoLogByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsArquivoLogByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsArquivoLog), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsArquivoLogToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsArquivoLogToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsArquivoLog), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsArquivoLogByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.ExecutaImportacao.TcsArquivoLog");
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
        [Route("GetTcsArquivoLogToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsArquivoLogToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsArquivoLog), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsArquivoLogByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.ExecutaImportacao.TcsArquivoLog", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ExecutaImportacaoDataSource", DataSourceObject = "GetTcsArquivoLog", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsArquivoLog"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsArquivoLog> GetSampleTcsArquivoLog(string details)
        {
            var result = repository.Context.GetTcsArquivoLogByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("GetTcsArquivo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsArquivo> GetTcsArquivo()
        {
            return repository.Context.GetTcsArquivo();
        }
        
        [Route("GetTcsArquivoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsArquivo> GetTcsArquivoNoAssociations()
        {
            return repository.Context.GetTcsArquivoNoAssociations();
        }
        
        [Route("GetTcsArquivoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsArquivo> GetTcsArquivoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsArquivoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsArquivo), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsArquivoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsArquivo> GetTcsArquivoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsArquivoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsArquivo), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsArquivoToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsArquivoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsArquivo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsArquivoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.ExecutaImportacao.TcsArquivo");
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
        [Route("GetTcsArquivoToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsArquivoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsArquivo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsArquivoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.ExecutaImportacao.TcsArquivo", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ExecutaImportacaoDataSource", DataSourceObject = "GetTcsArquivo", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsArquivo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsArquivo> GetSampleTcsArquivo(string details)
        {
            var result = repository.Context.GetTcsArquivoByEntitySearchNoAssociations(null).Take(100).ToList();
               if (!details.IsNullOrEmpty())
               {
                   foreach(var entity in result)
                   {
                       entity.FillDetails(repository.Context, null, null, details.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries), 5);
                   }
               }
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
    
    public partial class LinxFrameworkExecutaImportacaoFeedController : ODataController
    {
        private BusinessNS.ExecutaImportacaoDomainService _context;
        public BusinessNS.ExecutaImportacaoDomainService Context { get {  if (_context == null) { _context = new BusinessNS.ExecutaImportacaoDomainService(); _context.IsSecure = true; } return _context; }  }
        
        #region Get Action to Business Entities
        
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsArquivoImportar> GetTcsArquivoImportar(Int32 key0)
        {
            var entity = this.Context.GetTcsArquivoImportarByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsArquivoImportar[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsArquivoImportar>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsArquivoImportar> GetTcsArquivoImportar()
        {
            return this.Context.GetTcsArquivoImportarByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsArquivo> GetTcsArquivoImportar__TcsArquivo(Int32 key0, string navigation)
        {
            var entity = this.Context.GetTcsArquivoImportarByKey(key0);
            if (entity != null && navigation == "TcsArquivo")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsArquivo[] { entity.TcsArquivo }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsArquivo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsArquivoLog> GetTcsArquivoImportar__TcsArquivoLog(Int32 key0, string navigation)
        {
            var entity = this.Context.GetTcsArquivoImportarByKey(key0);
            if (entity != null && navigation == "TcsArquivoLogList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsArquivoLog" });
               return entity.TcsArquivoLogList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsArquivoLog>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsArquivoLog> GetTcsArquivoLog(Int32 key0)
        {
            var entity = this.Context.GetTcsArquivoLogByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsArquivoLog[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsArquivoLog>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsArquivoLog> GetTcsArquivoLog()
        {
            return this.Context.GetTcsArquivoLogByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsArquivoImportar> GetTcsArquivoLog__TcsArquivoImportar(Int32 key0, string navigation)
        {
            var entity = this.Context.GetTcsArquivoLogByKey(key0);
            if (entity != null && navigation == "TcsArquivoImportar")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsArquivoImportar[] { entity.TcsArquivoImportar }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsArquivoImportar>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsArquivo> GetTcsArquivo(Int32 key0)
        {
            var entity = this.Context.GetTcsArquivoByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsArquivo[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsArquivo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsArquivo> GetTcsArquivo()
        {
            return this.Context.GetTcsArquivoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsArquivoImportar> GetTcsArquivo__TcsArquivoImportar(Int32 key0, string navigation)
        {
            var entity = this.Context.GetTcsArquivoByKey(key0);
            if (entity != null && navigation == "TcsArquivoImportarList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsArquivoImportar" });
               return entity.TcsArquivoImportarList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsArquivoImportar>);
        }
        #endregion
        
    }
    
    public partial class LinxFrameworkExecutaImportacaoControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return true;
        }
    }
}

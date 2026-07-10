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
using BusinessNS = Linx.Framework.BV.TratamentoErros;

namespace Linx.Framework.BV.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/LinxFrameworkTratamentoErros/[ActionName]
    // Security Information Call: http://localhost:1710/LinxFrameworkTratamentoErros/GetSecurityInfo
    // Entities Catalog Call: http://localhost:1710/LinxFrameworkTratamentoErros/GetEntities
    // Entity MetaData Call: http://localhost:1710/LinxFrameworkTratamentoErros/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/LinxFrameworkTratamentoErros/GetClientDomains?erp=true
    // Client Service Call: http://localhost:1710/LinxFrameworkTratamentoErros/GetClientService?erp=true
    // Client Factory Call: http://localhost:1710/LinxFrameworkTratamentoErros/GetClientFactory?entityName=[EntityName]&erp=true
    // Client Factory Custom Events Call: http://localhost:1710/LinxFrameworkTratamentoErros/GetClientFactoryCustomEvents?entityName=[EntityName]&erp=true
    // Help Call: http://localhost:1710/HelpController/LinxFrameworkTratamentoErros
    // Feed OData Call: http://localhost:1710/LinxFrameworkTratamentoErrosOData
    [RoutePrefix("LinxFrameworkTratamentoErros")]
    [Breeze.WebApi2.BreezeController]
    [ODataBasicAuthenticationFilter]
    public partial class LinxFrameworkTratamentoErrosController : ApiController
    {
        private const int maxObjectExcelReturned = 2300000;
        private DataServiceRepository<BusinessNS.TratamentoErrosDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.TratamentoErrosDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.TratamentoErrosDomainService>(typeof(BusinessNS.LogFile), typeof(BusinessNS.TcsLogErros), typeof(BusinessNS.TcsLogErrosDash)); _repository.Context.IsSecure = true; } return _repository; } }
        public LinxFrameworkTratamentoErrosController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(LinxFrameworkTratamentoErrosController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.TratamentoErrosDomainService).Assembly.FullName,
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
            var result = repository.Context.GetMetaDataObject("Linx.Framework.BV.TratamentoErros." + entityName, false, true);
            return (allComposition ? result : result.Where(e => e.ClassName == entityName).ToList());
        }
        
        [Route("GetSecurityInfo"), System.Web.Http.HttpGet()]
        public string[] GetSecurityInfo()
        {
           return new string[] { "Linx.Framework.BV", "LinxFrameworkTratamentoErros", "LinxFrameworkTratamentoErros/ActionName" };
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
        
        [LinxFrameworkTratamentoErrosControllerAuthorize]
        [Route("GetAllLookUpGpecon"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpGpecon> GetAllLookUpGpecon()
        {
            return repository.Context.GetAllLookUpGpecon();
        }
        
        [LinxFrameworkTratamentoErrosControllerAuthorize]
        [Route("GetLookUpGpeconByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpGpecon> GetLookUpGpeconByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpGpeconByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [LinxFrameworkTratamentoErrosControllerAuthorize]
        [Route("GetAllLookUpTcsAmbiente"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsAmbiente> GetAllLookUpTcsAmbiente()
        {
            return repository.Context.GetAllLookUpTcsAmbiente();
        }
        
        [LinxFrameworkTratamentoErrosControllerAuthorize]
        [Route("GetLookUpTcsAmbienteByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsAmbiente> GetLookUpTcsAmbienteByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsAmbienteByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [LinxFrameworkTratamentoErrosControllerAuthorize]
        [Route("GetAllLookUpTcsAplicacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsAplicacao> GetAllLookUpTcsAplicacao()
        {
            return repository.Context.GetAllLookUpTcsAplicacao();
        }
        
        [LinxFrameworkTratamentoErrosControllerAuthorize]
        [Route("GetLookUpTcsAplicacaoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsAplicacao> GetLookUpTcsAplicacaoByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsAplicacaoByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [LinxFrameworkTratamentoErrosControllerAuthorize]
        [Route("GetAllLookUpTcsEmpresaAutenticacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsEmpresaAutenticacao> GetAllLookUpTcsEmpresaAutenticacao()
        {
            return repository.Context.GetAllLookUpTcsEmpresaAutenticacao();
        }
        
        [LinxFrameworkTratamentoErrosControllerAuthorize]
        [Route("GetLookUpTcsEmpresaAutenticacaoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsEmpresaAutenticacao> GetLookUpTcsEmpresaAutenticacaoByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsEmpresaAutenticacaoByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [LinxFrameworkTratamentoErrosControllerAuthorize]
        [Route("GetAllLookUpTcsUsuarioAutenticacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsUsuarioAutenticacao> GetAllLookUpTcsUsuarioAutenticacao()
        {
            return repository.Context.GetAllLookUpTcsUsuarioAutenticacao();
        }
        
        [LinxFrameworkTratamentoErrosControllerAuthorize]
        [Route("GetLookUpTcsUsuarioAutenticacaoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsUsuarioAutenticacao> GetLookUpTcsUsuarioAutenticacaoByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsUsuarioAutenticacaoByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
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
        
        [LinxFrameworkTratamentoErrosControllerAuthorize]
        [Route("GetTcsLogErrosDash"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsLogErrosDash> GetTcsLogErrosDash()
        {
            return repository.Context.GetTcsLogErrosDash().AsQueryable();
        }
        
        [LinxFrameworkTratamentoErrosControllerAuthorize]
        [Route("GetTcsLogErrosDashNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsLogErrosDash> GetTcsLogErrosDashNoAssociations()
        {
            return repository.Context.GetTcsLogErrosDashNoAssociations().AsQueryable();
        }
        
        [LinxFrameworkTratamentoErrosControllerAuthorize]
        [Route("GetTcsLogErrosDashByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsLogErrosDash> GetTcsLogErrosDashByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsLogErrosDashByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsLogErrosDash), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        
        [LinxFrameworkTratamentoErrosControllerAuthorize]
        [Route("GetTcsLogErrosDashByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsLogErrosDash> GetTcsLogErrosDashByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsLogErrosDashByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsLogErrosDash), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        [Route("GetTcsLogErrosDashToExcel"), System.Web.Http.HttpPost()]
        [LinxFrameworkTratamentoErrosControllerAuthorize]
        public string GetTcsLogErrosDashToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsLogErrosDash), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsLogErrosDashByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).AsQueryable().OrderBy("DataErro asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.TratamentoErros.TcsLogErrosDash");
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
               return ExcelExportPagination<BusinessNS.TcsLogErrosDash>.CreateExcelDocumentFileMapPath("TcsLogErrosDash",new ExcelExportPagination<BusinessNS.TcsLogErrosDash>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsLogErrosDashToReportXml"), System.Web.Http.HttpPost()]
        [LinxFrameworkTratamentoErrosControllerAuthorize]
        public string GetTcsLogErrosDashToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsLogErrosDash), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsLogErrosDashByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.TratamentoErros.TcsLogErrosDash", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.TratamentoErrosDataSource", DataSourceObject = "GetTcsLogErrosDash", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxFrameworkTratamentoErrosControllerAuthorize]
        [Route("GetSampleTcsLogErrosDash"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsLogErrosDash> GetSampleTcsLogErrosDash(string details)
        {
            var result = repository.Context.GetTcsLogErrosDashByEntitySearchNoAssociations(null).Take(100).ToList();
               if (!details.IsNullOrEmpty())
               {
                   foreach(var entity in result)
                   {
                       entity.FillDetails(repository.Context, null, null, details.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries), 5);
                   }
               }
            return result.AsQueryable();
        }
        
        [Route("AddTcsLogErrosDashEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsLogErrosDashEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsLogErrosDash), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [LinxFrameworkTratamentoErrosControllerAuthorize]
        [Route("GetTcsLogErrosDashByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsLogErrosDash> GetTcsLogErrosDashByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsLogErrosDashByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [LinxFrameworkTratamentoErrosControllerAuthorize]
        [Route("GetLogFile"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LogFile> GetLogFile()
        {
            return repository.Context.GetLogFile().AsQueryable();
        }
        
        [LinxFrameworkTratamentoErrosControllerAuthorize]
        [Route("GetLogFileNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LogFile> GetLogFileNoAssociations()
        {
            return repository.Context.GetLogFileNoAssociations().AsQueryable();
        }
        
        [LinxFrameworkTratamentoErrosControllerAuthorize]
        [Route("GetLogFileByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LogFile> GetLogFileByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetLogFileByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LogFile), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        
        [LinxFrameworkTratamentoErrosControllerAuthorize]
        [Route("GetLogFileByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LogFile> GetLogFileByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetLogFileByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LogFile), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        [Route("GetLogFileToExcel"), System.Web.Http.HttpPost()]
        [LinxFrameworkTratamentoErrosControllerAuthorize]
        public string GetLogFileToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LogFile), jEntitySearch, false, false, false);
            var entities = repository.Context.GetLogFileByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).AsQueryable().OrderBy("FileName asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.TratamentoErros.LogFile");
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
               return ExcelExportPagination<BusinessNS.LogFile>.CreateExcelDocumentFileMapPath("LogFile",new ExcelExportPagination<BusinessNS.LogFile>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetLogFileToReportXml"), System.Web.Http.HttpPost()]
        [LinxFrameworkTratamentoErrosControllerAuthorize]
        public string GetLogFileToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LogFile), jEntitySearch, false, false, false);
            var entities = repository.Context.GetLogFileByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.TratamentoErros.LogFile", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.TratamentoErrosDataSource", DataSourceObject = "GetLogFile", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxFrameworkTratamentoErrosControllerAuthorize]
        [Route("GetSampleLogFile"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LogFile> GetSampleLogFile(string details)
        {
            var result = repository.Context.GetLogFileByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddLogFileEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddLogFileEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LogFile), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [LinxFrameworkTratamentoErrosControllerAuthorize]
        [Route("GetLogFileByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LogFile> GetLogFileByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetLogFileByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [LinxFrameworkTratamentoErrosControllerAuthorize]
        [Route("GetTcsLogErros"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsLogErros> GetTcsLogErros()
        {
            return repository.Context.GetTcsLogErros();
        }
        
        [LinxFrameworkTratamentoErrosControllerAuthorize]
        [Route("GetTcsLogErrosNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsLogErros> GetTcsLogErrosNoAssociations()
        {
            return repository.Context.GetTcsLogErrosNoAssociations();
        }
        
        [LinxFrameworkTratamentoErrosControllerAuthorize]
        [Route("GetTcsLogErrosByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsLogErros> GetTcsLogErrosByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsLogErrosByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsLogErros), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [LinxFrameworkTratamentoErrosControllerAuthorize]
        [Route("GetTcsLogErrosByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsLogErros> GetTcsLogErrosByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsLogErrosByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsLogErros), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsLogErrosToExcel"), System.Web.Http.HttpPost()]
        [LinxFrameworkTratamentoErrosControllerAuthorize]
        public string GetTcsLogErrosToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsLogErros), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsLogErrosByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("DataErro desc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.TratamentoErros.TcsLogErros");
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
               return ExcelExportPagination<BusinessNS.TcsLogErros>.CreateExcelDocumentFileMapPath("TcsLogErros",new ExcelExportPagination<BusinessNS.TcsLogErros>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsLogErrosToReportXml"), System.Web.Http.HttpPost()]
        [LinxFrameworkTratamentoErrosControllerAuthorize]
        public string GetTcsLogErrosToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsLogErros), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsLogErrosByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.TratamentoErros.TcsLogErros", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.TratamentoErrosDataSource", DataSourceObject = "GetTcsLogErros", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxFrameworkTratamentoErrosControllerAuthorize]
        [Route("GetSampleTcsLogErros"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsLogErros> GetSampleTcsLogErros(string details)
        {
            var result = repository.Context.GetTcsLogErrosByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsLogErrosEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsLogErrosEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsLogErros), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [LinxFrameworkTratamentoErrosControllerAuthorize]
        [Route("GetTcsLogErrosByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsLogErros> GetTcsLogErrosByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsLogErrosByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        #endregion
        
        #region Get Business Entities By Parent Composition
        #endregion
        
        #region Save Changes
        [LinxFrameworkTratamentoErrosControllerAuthorize]
        [Route("SaveChanges"), System.Web.Http.HttpPost()]
        public SaveResult SaveChanges(JObject saveBundle)
        {
            var result = repository.SaveChanges(saveBundle);
            repository.Context.Dispose();
            return result;
        }
        [LinxFrameworkTratamentoErrosControllerAuthorize]
        [Route("SaveTcsLogErrosDash"), System.Web.Http.HttpPost()]
        public List<BusinessNS.TcsLogErrosDash> SaveTcsLogErrosDash(List<BusinessNS.TcsLogErrosDash> dataList)
        {
            if (dataList != null && dataList.Count > 0)
            {
                List<ChangeSetEntry> changeSetEntries = new List<ChangeSetEntry>();
                foreach (var data in dataList.Where(e => e.ChangeState.InList("I", "U", "D")).ToArray())
                {
                   if (data.ChangeState == "D") data.ResetDetails();
                   foreach (var entity in data.GetFlatEntities())
                   {
                       string state = entity.GetPropertyValue("ChangeState") as string;
                       if (state.InList("I", "U", "D"))
                       {
                           var changeOP = (state == "I" ? DomainOperation.Insert : (state == "D" ? DomainOperation.Delete :  DomainOperation.Update));
                           changeSetEntries.Add(new ChangeSetEntry(changeSetEntries.Count, entity, null, changeOP) { HasMemberChanges = (changeOP == DomainOperation.Update) });
                       }
                   }
                   if (data.ChangeState != "D") data.ResetDetails();
                }
                repository.Context.SaveEntities(changeSetEntries, false);
            }
            repository.Context.Dispose();
            //Set return with nochanges
            var result = dataList.Where(e => e.ChangeState.InList("I", "U", "N")).ToList();
            foreach (var data in result.ToArray())
            {
                   if (data.ChangeState == "N") data.ResetDetails();
                   else data.ResetChangeState();
            }
            return result;
        }
        [LinxFrameworkTratamentoErrosControllerAuthorize]
        [Route("SaveTcsLogErrosDashInCache"), System.Web.Http.HttpPost()]
        public void SaveTcsLogErrosDashInCache(SaveInformation<BusinessNS.TcsLogErrosDash> saveInfo)
        {
            saveInfo.Validate();
            QueueTransaction.SaveTransaction(saveInfo, System.Reflection.Assembly.GetExecutingAssembly().FullName, this.ControllerContext, "SaveTcsLogErrosDash");
        }
        public List<BusinessNS.TcsLogErrosDash> SaveTcsLogErrosDash__ForMEF(string jsonString, string viewMapInfo, List<ChangeTracker> changes)
        {
            List<BusinessNS.TcsLogErrosDash> dataList = SerializationManager<List<BusinessNS.TcsLogErrosDash>>.JsonToObject(jsonString);
            if (!viewMapInfo.IsNullOrEmpty() && changes.Count > 0)
            {
                var viewMap = ViewMapHelper.Parse(viewMapInfo);
                if(changes.Any(c => c.ComponentName == viewMap.ParentUIView))
                    dataList = viewMap.ReplaceEntities(dataList, changes.First(c => c.ComponentName == viewMap.ParentUIView).ListReturnedObjects);
            }
            return SaveTcsLogErrosDash(dataList);
        }
        
        
        [LinxFrameworkTratamentoErrosControllerAuthorize]
        [Route("SubmitAllChanges"), System.Web.Http.HttpGet()]
        public Dictionary<string, List<object>> SubmitAllChanges(Guid transactionID)
        {
            var obj = QueueTransaction.GetTransaction(transactionID);
            if (obj.IsNull())
                throw new ArgumentOutOfRangeException(string.Format("Não foi possível localizar o objeto 'QueueTransaction', para ID={0}", transactionID));
            Dictionary<string, List<object>> changes = new Dictionary<string, List<object>>();
            var operations = obj.SubmitTansaction();
            foreach (var _op in operations) changes.Add(_op.ComponentName, _op.ListReturnedObjects);
            return changes;
        }
        [LinxFrameworkTratamentoErrosControllerAuthorize]
        [Route("CancelAllChanges"), System.Web.Http.HttpGet()]
        public void CancelAllChanges(Guid transactionID)
        {
            var obj = QueueTransaction.GetTransaction(transactionID);
            if (!obj.IsNull())
                obj.DeleteCache();
        }
        #endregion
    }
    
    [ODataBasicAuthenticationFilter]
    public partial class LinxFrameworkTratamentoErrosFeedController : ODataController
    {
        private BusinessNS.TratamentoErrosDomainService _context;
        public BusinessNS.TratamentoErrosDomainService Context { get {  if (_context == null) { _context = new BusinessNS.TratamentoErrosDomainService(); _context.IsSecure = true; } return _context; }  }
        
        #region Get Action to Business Entities
        
        
        [LinxFrameworkTratamentoErrosControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.LogFile> GetLogFileById([FromODataUri]System.String key0)
        {
            var entity = this.Context.GetLogFileByKey(key0);
            if (entity != null)
               return (new BusinessNS.LogFile[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.LogFile>);
        }
        
        [LinxFrameworkTratamentoErrosControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.LogFile> GetLogFileByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetLogFileByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LogFile), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.LogFile>);
        }
        
        [LinxFrameworkTratamentoErrosControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.LogFile> GetLogFile()
        {
            return this.Context.GetLogFileByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxFrameworkTratamentoErrosControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsLogErrosDash> GetLogFile__TcsLogErrosDash(System.String key0, string navigation)
        {
            var entity = this.Context.GetLogFileByKey(key0);
            if (entity != null && navigation == "TcsLogErrosDash")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsLogErrosDash[] { entity.TcsLogErrosDash }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsLogErrosDash>);
        }
        
        [LinxFrameworkTratamentoErrosControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsLogErros> GetTcsLogErrosById([FromODataUri]Int32 key0)
        {
            var entity = this.Context.GetTcsLogErrosByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsLogErros[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsLogErros>);
        }
        
        [LinxFrameworkTratamentoErrosControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsLogErros> GetTcsLogErrosByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsLogErrosByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsLogErros), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsLogErros>);
        }
        
        [LinxFrameworkTratamentoErrosControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsLogErros> GetTcsLogErros()
        {
            return this.Context.GetTcsLogErrosByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxFrameworkTratamentoErrosControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsLogErrosDash> GetTcsLogErros__TcsLogErrosDash(Int32 key0, string navigation)
        {
            var entity = this.Context.GetTcsLogErrosByKey(key0);
            if (entity != null && navigation == "TcsLogErrosDash")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsLogErrosDash[] { entity.TcsLogErrosDash }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsLogErrosDash>);
        }
        #endregion
        
    }
    
    public partial class LinxFrameworkTratamentoErrosControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return LinxAutorization.CheckAuthorization(actionContext, string.Format("{0}#{1}#{1}/{2}", "Linx.Framework.BV", "LinxFrameworkTratamentoErros", actionContext.ActionDescriptor.ActionName));
        }
    }
}

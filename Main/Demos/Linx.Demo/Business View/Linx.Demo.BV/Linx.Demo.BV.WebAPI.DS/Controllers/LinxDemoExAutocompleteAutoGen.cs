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
using BusinessNS = Linx.Demo.BV.ExAutocomplete;

namespace Linx.Demo.BV.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/LinxDemoExAutocomplete/[ActionName]
    // Security Information Call: http://localhost:1710/LinxDemoExAutocomplete/GetSecurityInfo
    // Entities Catalog Call: http://localhost:1710/LinxDemoExAutocomplete/GetEntities
    // Entity MetaData Call: http://localhost:1710/LinxDemoExAutocomplete/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/LinxDemoExAutocomplete/GetClientDomains?erp=true
    // Client Service Call: http://localhost:1710/LinxDemoExAutocomplete/GetClientService?erp=true
    // Client Factory Call: http://localhost:1710/LinxDemoExAutocomplete/GetClientFactory?entityName=[EntityName]&erp=true
    // Client Factory Custom Events Call: http://localhost:1710/LinxDemoExAutocomplete/GetClientFactoryCustomEvents?entityName=[EntityName]&erp=true
    // Help Call: http://localhost:1710/HelpController/LinxDemoExAutocomplete
    // Feed OData Call: http://localhost:1710/LinxDemoExAutocompleteOData
    [RoutePrefix("LinxDemoExAutocomplete")]
    [Breeze.WebApi2.BreezeController]
    [ODataBasicAuthenticationFilter]
    public partial class LinxDemoExAutocompleteController : ApiController
    {
        private const int maxObjectExcelReturned = 2300000;
        private DataServiceRepository<BusinessNS.ExAutocompleteDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.ExAutocompleteDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.ExAutocompleteDomainService>(typeof(BusinessNS.Cliente), typeof(BusinessNS.Tbnmcompleto), typeof(BusinessNS.TesteCkbView)); _repository.Context.IsSecure = true; } return _repository; } }
        public LinxDemoExAutocompleteController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(LinxDemoExAutocompleteController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.ExAutocompleteDomainService).Assembly.FullName,
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
            var result = repository.Context.GetMetaDataObject("Linx.Demo.BV.ExAutocomplete." + entityName, false, true);
            return (allComposition ? result : result.Where(e => e.ClassName == entityName).ToList());
        }
        
        [Route("GetSecurityInfo"), System.Web.Http.HttpGet()]
        public string[] GetSecurityInfo()
        {
           return new string[] { "Linx.Demo.BV", "LinxDemoExAutocomplete", "LinxDemoExAutocomplete/ActionName" };
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
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetReportDataSource"), System.Web.Http.HttpGet()]
        public string GetReportDataSource()
        {
            var zip = new LinxZip();
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetDomainsInfo"), System.Web.Http.HttpGet()]
        public string[] GetDomainsInfo(string domainNames)
        {
            return Linx.Demo.BV.Domains.DomainHelper.GetDomainsInfo(domainNames);
        }
        
        [Route("GetDomainValues"), System.Web.Http.HttpGet()]
        public Dictionary<string, string> GetDomainValues(string domainName)
        {
            return Linx.Demo.BV.Domains.DomainHelper.GetDomainValues(domainName);
        }
        
        #region Get LookUps
        
        [LinxDemoExAutocompleteControllerAuthorize]
        [Route("GetAllLookUpTbnmmeio"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTbnmmeio> GetAllLookUpTbnmmeio()
        {
            return repository.Context.GetAllLookUpTbnmmeio();
        }
        
        [LinxDemoExAutocompleteControllerAuthorize]
        [Route("GetLookUpTbnmmeioByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTbnmmeio> GetLookUpTbnmmeioByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTbnmmeioByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [LinxDemoExAutocompleteControllerAuthorize]
        [Route("GetAllLookUpTbnome"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTbnome> GetAllLookUpTbnome()
        {
            return repository.Context.GetAllLookUpTbnome();
        }
        
        [LinxDemoExAutocompleteControllerAuthorize]
        [Route("GetLookUpTbnomeByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTbnome> GetLookUpTbnomeByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTbnomeByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [LinxDemoExAutocompleteControllerAuthorize]
        [Route("GetAllLookUpTbsobrenm"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTbsobrenm> GetAllLookUpTbsobrenm()
        {
            return repository.Context.GetAllLookUpTbsobrenm();
        }
        
        [LinxDemoExAutocompleteControllerAuthorize]
        [Route("GetLookUpTbsobrenmByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTbsobrenm> GetLookUpTbsobrenmByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTbsobrenmByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [LinxDemoExAutocompleteControllerAuthorize]
        [Route("GetAllLookUpEstado"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpEstado> GetAllLookUpEstado()
        {
            return repository.Context.GetAllLookUpEstado();
        }
        
        [LinxDemoExAutocompleteControllerAuthorize]
        [Route("GetLookUpEstadoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpEstado> GetLookUpEstadoByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpEstadoByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [LinxDemoExAutocompleteControllerAuthorize]
        [Route("GetAllLkpTbnmcompleto"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LkpTbnmcompleto> GetAllLkpTbnmcompleto()
        {
            return repository.Context.GetAllLkpTbnmcompleto();
        }
        
        [LinxDemoExAutocompleteControllerAuthorize]
        [Route("GetLkpTbnmcompletoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LkpTbnmcompleto> GetLkpTbnmcompletoByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLkpTbnmcompletoByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [LinxDemoExAutocompleteControllerAuthorize]
        [Route("GetAllLookUpCliente"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpCliente> GetAllLookUpCliente()
        {
            return repository.Context.GetAllLookUpCliente();
        }
        
        [LinxDemoExAutocompleteControllerAuthorize]
        [Route("GetLookUpClienteByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpCliente> GetLookUpClienteByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpClienteByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
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
        
        [LinxDemoExAutocompleteControllerAuthorize]
        [Route("GetTbnmcompleto"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Tbnmcompleto> GetTbnmcompleto()
        {
            return repository.Context.GetTbnmcompleto();
        }
        
        [LinxDemoExAutocompleteControllerAuthorize]
        [Route("GetTbnmcompletoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Tbnmcompleto> GetTbnmcompletoNoAssociations()
        {
            return repository.Context.GetTbnmcompletoNoAssociations();
        }
        
        [LinxDemoExAutocompleteControllerAuthorize]
        [Route("GetTbnmcompletoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Tbnmcompleto> GetTbnmcompletoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTbnmcompletoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Tbnmcompleto), jEntitySearch, false, true, false), jEntitySearch);
        }
        
        [LinxDemoExAutocompleteControllerAuthorize]
        [Route("GetTbnmcompletoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Tbnmcompleto> GetTbnmcompletoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTbnmcompletoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Tbnmcompleto), jEntitySearch, false, true, false), jEntitySearch);
        }
        [Route("GetTbnmcompletoToExcel"), System.Web.Http.HttpPost()]
        [LinxDemoExAutocompleteControllerAuthorize]
        public string GetTbnmcompletoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Tbnmcompleto), jEntitySearch, false, true, false);
            var entities = repository.Context.GetTbnmcompletoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("idNomeCompleto asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.ExAutocomplete.Tbnmcompleto");
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
               return ExcelExportPagination<BusinessNS.Tbnmcompleto>.CreateExcelDocumentFileMapPath("Tbnmcompleto",new ExcelExportPagination<BusinessNS.Tbnmcompleto>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTbnmcompletoToReportXml"), System.Web.Http.HttpPost()]
        [LinxDemoExAutocompleteControllerAuthorize]
        public string GetTbnmcompletoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Tbnmcompleto), jEntitySearch, false, true, false);
            var entities = repository.Context.GetTbnmcompletoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.ExAutocomplete.Tbnmcompleto", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Demo.BV.Reports", DataSourceFullName = "Linx.Demo.BV.Reports.ExAutocompleteDataSource", DataSourceObject = "GetTbnmcompleto", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxDemoExAutocompleteControllerAuthorize]
        [Route("GetSampleTbnmcompleto"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Tbnmcompleto> GetSampleTbnmcompleto(string details)
        {
            var result = repository.Context.GetTbnmcompletoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [LinxDemoExAutocompleteControllerAuthorize]
        [Route("GetTesteCkbView"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TesteCkbView> GetTesteCkbView()
        {
            return repository.Context.GetTesteCkbView();
        }
        
        [LinxDemoExAutocompleteControllerAuthorize]
        [Route("GetTesteCkbViewNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TesteCkbView> GetTesteCkbViewNoAssociations()
        {
            return repository.Context.GetTesteCkbViewNoAssociations();
        }
        
        [LinxDemoExAutocompleteControllerAuthorize]
        [Route("GetTesteCkbViewByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TesteCkbView> GetTesteCkbViewByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTesteCkbViewByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TesteCkbView), jEntitySearch, false, true, false), jEntitySearch);
        }
        
        [LinxDemoExAutocompleteControllerAuthorize]
        [Route("GetTesteCkbViewByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TesteCkbView> GetTesteCkbViewByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTesteCkbViewByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TesteCkbView), jEntitySearch, false, true, false), jEntitySearch);
        }
        [Route("GetTesteCkbViewToExcel"), System.Web.Http.HttpPost()]
        [LinxDemoExAutocompleteControllerAuthorize]
        public string GetTesteCkbViewToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TesteCkbView), jEntitySearch, false, true, false);
            var entities = repository.Context.GetTesteCkbViewByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdQualquer asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.ExAutocomplete.TesteCkbView");
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
               return ExcelExportPagination<BusinessNS.TesteCkbView>.CreateExcelDocumentFileMapPath("TesteCkbView",new ExcelExportPagination<BusinessNS.TesteCkbView>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTesteCkbViewToReportXml"), System.Web.Http.HttpPost()]
        [LinxDemoExAutocompleteControllerAuthorize]
        public string GetTesteCkbViewToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TesteCkbView), jEntitySearch, false, true, false);
            var entities = repository.Context.GetTesteCkbViewByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.ExAutocomplete.TesteCkbView", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Demo.BV.Reports", DataSourceFullName = "Linx.Demo.BV.Reports.ExAutocompleteDataSource", DataSourceObject = "GetTesteCkbView", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxDemoExAutocompleteControllerAuthorize]
        [Route("GetSampleTesteCkbView"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TesteCkbView> GetSampleTesteCkbView(string details)
        {
            var result = repository.Context.GetTesteCkbViewByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [LinxDemoExAutocompleteControllerAuthorize]
        [Route("GetCliente"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Cliente> GetCliente()
        {
            return repository.Context.GetCliente();
        }
        
        [LinxDemoExAutocompleteControllerAuthorize]
        [Route("GetClienteNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Cliente> GetClienteNoAssociations()
        {
            return repository.Context.GetClienteNoAssociations();
        }
        
        [LinxDemoExAutocompleteControllerAuthorize]
        [Route("GetClienteByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Cliente> GetClienteByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetClienteByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Cliente), jEntitySearch, false, true, false), jEntitySearch);
        }
        
        [LinxDemoExAutocompleteControllerAuthorize]
        [Route("GetClienteByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Cliente> GetClienteByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetClienteByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Cliente), jEntitySearch, false, true, false), jEntitySearch);
        }
        [Route("GetClienteToExcel"), System.Web.Http.HttpPost()]
        [LinxDemoExAutocompleteControllerAuthorize]
        public string GetClienteToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Cliente), jEntitySearch, false, true, false);
            var entities = repository.Context.GetClienteByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdCliente asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.ExAutocomplete.Cliente");
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
               return ExcelExportPagination<BusinessNS.Cliente>.CreateExcelDocumentFileMapPath("Cliente",new ExcelExportPagination<BusinessNS.Cliente>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetClienteToReportXml"), System.Web.Http.HttpPost()]
        [LinxDemoExAutocompleteControllerAuthorize]
        public string GetClienteToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Cliente), jEntitySearch, false, true, false);
            var entities = repository.Context.GetClienteByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.ExAutocomplete.Cliente", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Demo.BV.Reports", DataSourceFullName = "Linx.Demo.BV.Reports.ExAutocompleteDataSource", DataSourceObject = "GetCliente", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxDemoExAutocompleteControllerAuthorize]
        [Route("GetSampleCliente"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Cliente> GetSampleCliente(string details)
        {
            var result = repository.Context.GetClienteByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        #endregion
        
        #region Get Business Entities By Parent Composition
        #endregion
        
        #region Save Changes
        [LinxDemoExAutocompleteControllerAuthorize]
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
    public partial class LinxDemoExAutocompleteFeedController : ODataController
    {
        private BusinessNS.ExAutocompleteDomainService _context;
        public BusinessNS.ExAutocompleteDomainService Context { get {  if (_context == null) { _context = new BusinessNS.ExAutocompleteDomainService(); _context.IsSecure = true; } return _context; }  }
        
        #region Get Action to Business Entities
        
        
        [LinxDemoExAutocompleteControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Tbnmcompleto> GetTbnmcompletoById([FromODataUri]int key0)
        {
            var entity = this.Context.GetTbnmcompletoByKey(key0);
            if (entity != null)
               return (new BusinessNS.Tbnmcompleto[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.Tbnmcompleto>);
        }
        
        [LinxDemoExAutocompleteControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Tbnmcompleto> GetTbnmcompletoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTbnmcompletoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Tbnmcompleto), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.Tbnmcompleto>);
        }
        
        [LinxDemoExAutocompleteControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Tbnmcompleto> GetTbnmcompleto()
        {
            return this.Context.GetTbnmcompletoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxDemoExAutocompleteControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TesteCkbView> GetTesteCkbViewById([FromODataUri]int key0)
        {
            var entity = this.Context.GetTesteCkbViewByKey(key0);
            if (entity != null)
               return (new BusinessNS.TesteCkbView[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TesteCkbView>);
        }
        
        [LinxDemoExAutocompleteControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TesteCkbView> GetTesteCkbViewByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTesteCkbViewByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TesteCkbView), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TesteCkbView>);
        }
        
        [LinxDemoExAutocompleteControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TesteCkbView> GetTesteCkbView()
        {
            return this.Context.GetTesteCkbViewByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxDemoExAutocompleteControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Cliente> GetClienteById([FromODataUri]int key0)
        {
            var entity = this.Context.GetClienteByKey(key0);
            if (entity != null)
               return (new BusinessNS.Cliente[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.Cliente>);
        }
        
        [LinxDemoExAutocompleteControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Cliente> GetClienteByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetClienteByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Cliente), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.Cliente>);
        }
        
        [LinxDemoExAutocompleteControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Cliente> GetCliente()
        {
            return this.Context.GetClienteByEntitySearchNoAssociations(null).AsQueryable();
        }
        #endregion
        
    }
    
    public partial class LinxDemoExAutocompleteControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return LinxAutorization.CheckAuthorization(actionContext, string.Format("{0}#{1}#{1}/{2}", "Linx.Demo.BV", "LinxDemoExAutocomplete", actionContext.ActionDescriptor.ActionName));
        }
    }
}

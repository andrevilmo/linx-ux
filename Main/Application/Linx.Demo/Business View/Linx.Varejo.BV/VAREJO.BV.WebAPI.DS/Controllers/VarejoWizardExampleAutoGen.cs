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
using System.ServiceModel.DomainServices.Server;
using Linx.DataService;
using Linx.Business.Tools;
using System.ComponentModel.Composition;
using System.Web.Http;
using Breeze.ContextProvider;
using Breeze.WebApi2;
using System.Web.Http.OData;
using BusinessNS = VAREJO.BV.WizardExample;

namespace VAREJO.BV.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/VarejoWizardExample/[ActionName]
    // Security Information Call: http://localhost:1710/VarejoWizardExample/GetSecurityInfo
    // Entities Catalog Call: http://localhost:1710/VarejoWizardExample/GetEntities
    // Entity MetaData Call: http://localhost:1710/VarejoWizardExample/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/VarejoWizardExample/GetClientDomains?erp=true
    // Client Service Call: http://localhost:1710/VarejoWizardExample/GetClientService?erp=true
    // Client Factory Call: http://localhost:1710/VarejoWizardExample/GetClientFactory?entityName=[EntityName]&erp=true
    // Client Factory Custom Events Call: http://localhost:1710/VarejoWizardExample/GetClientFactoryCustomEvents?entityName=[EntityName]&erp=true
    // Help Call: http://localhost:1710/HelpController/VarejoWizardExample
    // Feed OData Call: http://localhost:1710/VarejoWizardExampleOData
    [RoutePrefix("VarejoWizardExample")]
    [Breeze.WebApi2.BreezeController]
    [ODataBasicAuthenticationFilter]
    public partial class VarejoWizardExampleController : ApiController
    {
        private const int maxObjectExcelReturned = 2300000;
        private DataServiceRepository<BusinessNS.WizardExampleDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.WizardExampleDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.WizardExampleDomainService>(typeof(BusinessNS.Cliente), typeof(BusinessNS.Venda), typeof(BusinessNS.VendaAtacado)); _repository.Context.IsSecure = true; } return _repository; } }
        public VarejoWizardExampleController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(VarejoWizardExampleController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.WizardExampleDomainService).Assembly.FullName,
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
            var result = repository.Context.GetMetaDataObject("VAREJO.BV.WizardExample." + entityName, false, true);
            return (allComposition ? result : result.Where(e => e.ClassName == entityName).ToList());
        }
        
        [Route("GetSecurityInfo"), System.Web.Http.HttpGet()]
        public string[] GetSecurityInfo()
        {
           return new string[] { "VAREJO.BV", "VarejoWizardExample", "VarejoWizardExample/ActionName" };
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
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/VAREJO.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/VAREJO.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetReportDataSource"), System.Web.Http.HttpGet()]
        public string GetReportDataSource()
        {
            var zip = new LinxZip();
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/VAREJO.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/VAREJO.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetDomainsInfo"), System.Web.Http.HttpGet()]
        public string[] GetDomainsInfo(string domainNames)
        {
            return VAREJO.BV.Domains.DomainHelper.GetDomainsInfo(domainNames);
        }
        
        [Route("GetDomainValues"), System.Web.Http.HttpGet()]
        public Dictionary<string, string> GetDomainValues(string domainName)
        {
            return VAREJO.BV.Domains.DomainHelper.GetDomainValues(domainName);
        }
        
        #region Get LookUps
        
        [VarejoWizardExampleControllerAuthorize]
        [Route("GetAllLookUpEstado"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpEstado> GetAllLookUpEstado()
        {
            return repository.Context.GetAllLookUpEstado();
        }
        
        [VarejoWizardExampleControllerAuthorize]
        [Route("GetLookUpEstadoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpEstado> GetLookUpEstadoByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpEstadoByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [VarejoWizardExampleControllerAuthorize]
        [Route("GetAllLookUpLoja"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpLoja> GetAllLookUpLoja()
        {
            return repository.Context.GetAllLookUpLoja();
        }
        
        [VarejoWizardExampleControllerAuthorize]
        [Route("GetLookUpLojaByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpLoja> GetLookUpLojaByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpLojaByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
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
        
        [VarejoWizardExampleControllerAuthorize]
        [Route("GetCliente"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Cliente> GetCliente()
        {
            return repository.Context.GetCliente();
        }
        
        [VarejoWizardExampleControllerAuthorize]
        [Route("GetClienteNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Cliente> GetClienteNoAssociations()
        {
            return repository.Context.GetClienteNoAssociations();
        }
        
        [VarejoWizardExampleControllerAuthorize]
        [Route("GetClienteByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Cliente> GetClienteByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetClienteByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Cliente), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [VarejoWizardExampleControllerAuthorize]
        [Route("GetClienteByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Cliente> GetClienteByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetClienteByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Cliente), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetClienteToExcel"), System.Web.Http.HttpPost()]
        [VarejoWizardExampleControllerAuthorize]
        public string GetClienteToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Cliente), jEntitySearch, false, false, false);
            var entities = repository.Context.GetClienteByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdCliente asc");
            var metadata = repository.Context.GetMetaDataObject("VAREJO.BV.WizardExample.Cliente");
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
        [VarejoWizardExampleControllerAuthorize]
        public string GetClienteToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Cliente), jEntitySearch, false, false, false);
            var entities = repository.Context.GetClienteByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("VAREJO.BV.WizardExample.Cliente", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "VAREJO.BV.Reports", DataSourceFullName = "VAREJO.BV.Reports.WizardExampleDataSource", DataSourceObject = "GetCliente", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/VAREJO.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/VAREJO.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [VarejoWizardExampleControllerAuthorize]
        [Route("GetSampleCliente"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Cliente> GetSampleCliente(string details)
        {
            var result = repository.Context.GetClienteByEntitySearchNoAssociations(null).Take(100).ToList();
               if (!details.IsNullOrEmpty())
               {
                   foreach(var entity in result)
                   {
                       entity.FillDetails(repository.Context, null, null, details.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries), 5);
                   }
               }
            return result.AsQueryable();
        }
        
        [VarejoWizardExampleControllerAuthorize]
        [Route("GetVenda"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Venda> GetVenda()
        {
            return repository.Context.GetVenda();
        }
        
        [VarejoWizardExampleControllerAuthorize]
        [Route("GetVendaNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Venda> GetVendaNoAssociations()
        {
            return repository.Context.GetVendaNoAssociations();
        }
        
        [VarejoWizardExampleControllerAuthorize]
        [Route("GetVendaByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Venda> GetVendaByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetVendaByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Venda), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [VarejoWizardExampleControllerAuthorize]
        [Route("GetVendaByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Venda> GetVendaByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetVendaByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Venda), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetVendaToExcel"), System.Web.Http.HttpPost()]
        [VarejoWizardExampleControllerAuthorize]
        public string GetVendaToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Venda), jEntitySearch, false, false, false);
            var entities = repository.Context.GetVendaByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdVenda asc");
            var metadata = repository.Context.GetMetaDataObject("VAREJO.BV.WizardExample.Venda");
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
               return ExcelExportPagination<BusinessNS.Venda>.CreateExcelDocumentFileMapPath("Venda",new ExcelExportPagination<BusinessNS.Venda>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        [Route("GetVendaToReportXml"), System.Web.Http.HttpPost()]
        [VarejoWizardExampleControllerAuthorize]
        public string GetVendaToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Venda), jEntitySearch, false, false, false);
            var entities = repository.Context.GetVendaByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("VAREJO.BV.WizardExample.Venda", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "VAREJO.BV.Reports", DataSourceFullName = "VAREJO.BV.Reports.WizardExampleDataSource", DataSourceObject = "GetVenda", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/VAREJO.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/VAREJO.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [VarejoWizardExampleControllerAuthorize]
        [Route("GetSampleVenda"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Venda> GetSampleVenda(string details)
        {
            var result = repository.Context.GetVendaByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [VarejoWizardExampleControllerAuthorize]
        [Route("GetVendaAtacado"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaAtacado> GetVendaAtacado()
        {
            return repository.Context.GetVendaAtacado();
        }
        
        [VarejoWizardExampleControllerAuthorize]
        [Route("GetVendaAtacadoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaAtacado> GetVendaAtacadoNoAssociations()
        {
            return repository.Context.GetVendaAtacadoNoAssociations();
        }
        
        [VarejoWizardExampleControllerAuthorize]
        [Route("GetVendaAtacadoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaAtacado> GetVendaAtacadoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetVendaAtacadoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaAtacado), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [VarejoWizardExampleControllerAuthorize]
        [Route("GetVendaAtacadoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaAtacado> GetVendaAtacadoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetVendaAtacadoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaAtacado), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetVendaAtacadoToExcel"), System.Web.Http.HttpPost()]
        [VarejoWizardExampleControllerAuthorize]
        public string GetVendaAtacadoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaAtacado), jEntitySearch, false, false, false);
            var entities = repository.Context.GetVendaAtacadoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdVendaAtacado asc");
            var metadata = repository.Context.GetMetaDataObject("VAREJO.BV.WizardExample.VendaAtacado");
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
               return ExcelExportPagination<BusinessNS.VendaAtacado>.CreateExcelDocumentFileMapPath("VendaAtacado",new ExcelExportPagination<BusinessNS.VendaAtacado>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        [Route("GetVendaAtacadoToReportXml"), System.Web.Http.HttpPost()]
        [VarejoWizardExampleControllerAuthorize]
        public string GetVendaAtacadoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaAtacado), jEntitySearch, false, false, false);
            var entities = repository.Context.GetVendaAtacadoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("VAREJO.BV.WizardExample.VendaAtacado", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "VAREJO.BV.Reports", DataSourceFullName = "VAREJO.BV.Reports.WizardExampleDataSource", DataSourceObject = "GetVendaAtacado", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/VAREJO.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/VAREJO.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [VarejoWizardExampleControllerAuthorize]
        [Route("GetSampleVendaAtacado"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaAtacado> GetSampleVendaAtacado(string details)
        {
            var result = repository.Context.GetVendaAtacadoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        #endregion
        
        #region Get Business Entities By Parent Composition
        
        [VarejoWizardExampleControllerAuthorize]
        [Route("GetVendaParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaParentComposition> GetVendaParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetVendaParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetVendaParentCompositionToExcel"), System.Web.Http.HttpPost()]
        [VarejoWizardExampleControllerAuthorize]
        public string GetVendaParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("Venda{", "VendaParentComposition{");
            jEntitySearch = jEntitySearch.Replace("Cliente{", "VendaParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetVendaParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdVenda asc");
            var metadata = repository.Context.GetMetaDataObject("VAREJO.BV.WizardExample.Venda");
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
               return ExcelExportPagination<BusinessNS.VendaParentComposition>.CreateExcelDocumentFileMapPath("Venda",new ExcelExportPagination<BusinessNS.VendaParentComposition>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        [Route("GetVendaParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        [VarejoWizardExampleControllerAuthorize]
        public string GetVendaParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetVendaParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("VAREJO.BV.WizardExample.Venda", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "VAREJO.BV.Reports", DataSourceFullName = "VAREJO.BV.Reports.WizardExampleDataSource", DataSourceObject = "GetVendaParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/VAREJO.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/VAREJO.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [VarejoWizardExampleControllerAuthorize]
        [Route("GetSampleVendaParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaParentComposition> GetSampleVendaParentComposition(string details)
        {
            var result = repository.Context.GetVendaParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [VarejoWizardExampleControllerAuthorize]
        [Route("GetVendaAtacadoParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaAtacadoParentComposition> GetVendaAtacadoParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetVendaAtacadoParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaAtacadoParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetVendaAtacadoParentCompositionToExcel"), System.Web.Http.HttpPost()]
        [VarejoWizardExampleControllerAuthorize]
        public string GetVendaAtacadoParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("VendaAtacado{", "VendaAtacadoParentComposition{");
            jEntitySearch = jEntitySearch.Replace("Cliente{", "VendaAtacadoParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaAtacadoParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetVendaAtacadoParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdVendaAtacado asc");
            var metadata = repository.Context.GetMetaDataObject("VAREJO.BV.WizardExample.VendaAtacado");
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
               return ExcelExportPagination<BusinessNS.VendaAtacadoParentComposition>.CreateExcelDocumentFileMapPath("VendaAtacado",new ExcelExportPagination<BusinessNS.VendaAtacadoParentComposition>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        [Route("GetVendaAtacadoParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        [VarejoWizardExampleControllerAuthorize]
        public string GetVendaAtacadoParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaAtacadoParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetVendaAtacadoParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("VAREJO.BV.WizardExample.VendaAtacado", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "VAREJO.BV.Reports", DataSourceFullName = "VAREJO.BV.Reports.WizardExampleDataSource", DataSourceObject = "GetVendaAtacadoParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/VAREJO.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/VAREJO.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [VarejoWizardExampleControllerAuthorize]
        [Route("GetSampleVendaAtacadoParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaAtacadoParentComposition> GetSampleVendaAtacadoParentComposition(string details)
        {
            var result = repository.Context.GetVendaAtacadoParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        #endregion
        
        #region Save Changes
        [VarejoWizardExampleControllerAuthorize]
        [Route("SaveChanges"), System.Web.Http.HttpPost()]
        public SaveResult SaveChanges(JObject saveBundle)
        {
            var result = repository.SaveChanges(saveBundle);
            repository.Context.Dispose();
            return result;
        }
        [VarejoWizardExampleControllerAuthorize]
        [Route("SaveCliente"), System.Web.Http.HttpPost()]
        public List<BusinessNS.Cliente> SaveCliente(List<BusinessNS.Cliente> dataList)
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
        [VarejoWizardExampleControllerAuthorize]
        [Route("SaveClienteInCache"), System.Web.Http.HttpPost()]
        public void SaveClienteInCache(SaveInformation<BusinessNS.Cliente> saveInfo)
        {
            saveInfo.Validate();
            QueueTransaction.SaveTransaction(saveInfo, System.Reflection.Assembly.GetExecutingAssembly().FullName, this.ControllerContext, "SaveCliente");
        }
        [VarejoWizardExampleControllerAuthorize]
        public List<BusinessNS.Cliente> SaveCliente__ForMEF(string jsonString, string viewMapInfo, List<ChangeTracker> changes)
        {
            List<BusinessNS.Cliente> dataList = SerializationManager<List<BusinessNS.Cliente>>.JsonToObject(jsonString);
            if (!viewMapInfo.IsNullOrEmpty() && changes.Count > 0)
            {
                var viewMap = ViewMapHelper.Parse(viewMapInfo);
                if(changes.Any(c => c.ComponentName == viewMap.ParentUIView))
                    dataList = viewMap.ReplaceEntities(dataList, changes.First(c => c.ComponentName == viewMap.ParentUIView).ListReturnedObjects);
            }
            return SaveCliente(dataList);
        }
        
        
        [VarejoWizardExampleControllerAuthorize]
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
        [VarejoWizardExampleControllerAuthorize]
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
    public partial class VarejoWizardExampleFeedController : ODataController
    {
        private BusinessNS.WizardExampleDomainService _context;
        public BusinessNS.WizardExampleDomainService Context { get {  if (_context == null) { _context = new BusinessNS.WizardExampleDomainService(); _context.IsSecure = true; } return _context; }  }
        
        #region Get Action to Business Entities
        
        
        [VarejoWizardExampleControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Cliente> GetClienteById([FromODataUri]Int32 key0)
        {
            var entity = this.Context.GetClienteByKey(key0);
            if (entity != null)
               return (new BusinessNS.Cliente[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.Cliente>);
        }
        
        [VarejoWizardExampleControllerAuthorize]
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
        
        [VarejoWizardExampleControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Cliente> GetCliente()
        {
            return this.Context.GetClienteByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [VarejoWizardExampleControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Venda> GetCliente__Venda(Int32 key0, string navigation)
        {
            var entity = this.Context.GetClienteByKey(key0);
            if (entity != null && navigation == "VendaList")
            {
               entity.FillDetails(_context, null, null, new string[] { "Venda" });
               return entity.VendaList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.Venda>);
        }
        
        [VarejoWizardExampleControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaAtacado> GetCliente__VendaAtacado(Int32 key0, string navigation)
        {
            var entity = this.Context.GetClienteByKey(key0);
            if (entity != null && navigation == "VendaAtacadoList")
            {
               entity.FillDetails(_context, null, null, new string[] { "VendaAtacado" });
               return entity.VendaAtacadoList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.VendaAtacado>);
        }
        
        [VarejoWizardExampleControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Venda> GetVendaById([FromODataUri]Int32 key0)
        {
            var entity = this.Context.GetVendaByKey(key0);
            if (entity != null)
               return (new BusinessNS.Venda[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.Venda>);
        }
        
        [VarejoWizardExampleControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Venda> GetVendaByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetVendaByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Venda), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.Venda>);
        }
        
        [VarejoWizardExampleControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Venda> GetVenda()
        {
            return this.Context.GetVendaByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [VarejoWizardExampleControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaParentComposition> GetVendaParentComposition()
        {
            return this.Context.GetVendaParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [VarejoWizardExampleControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaParentComposition> GetVendaParentCompositionByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                jEntitySearch = jEntitySearch.Replace("Venda{", "VendaParentComposition{");
                jEntitySearch = jEntitySearch.Replace("Cliente{", "VendaParentComposition{");
                var entity = this.Context.GetVendaParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaParentComposition), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.VendaParentComposition>);
        }
        
        [VarejoWizardExampleControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Cliente> GetVenda__Cliente(Int32 key0, string navigation)
        {
            var entity = this.Context.GetVendaByKey(key0);
            if (entity != null && navigation == "Cliente")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.Cliente[] { entity.Cliente }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.Cliente>);
        }
        
        [VarejoWizardExampleControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaAtacado> GetVendaAtacadoById([FromODataUri]Int32 key0)
        {
            var entity = this.Context.GetVendaAtacadoByKey(key0);
            if (entity != null)
               return (new BusinessNS.VendaAtacado[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.VendaAtacado>);
        }
        
        [VarejoWizardExampleControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaAtacado> GetVendaAtacadoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetVendaAtacadoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaAtacado), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.VendaAtacado>);
        }
        
        [VarejoWizardExampleControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaAtacado> GetVendaAtacado()
        {
            return this.Context.GetVendaAtacadoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [VarejoWizardExampleControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaAtacadoParentComposition> GetVendaAtacadoParentComposition()
        {
            return this.Context.GetVendaAtacadoParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [VarejoWizardExampleControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaAtacadoParentComposition> GetVendaAtacadoParentCompositionByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                jEntitySearch = jEntitySearch.Replace("VendaAtacado{", "VendaAtacadoParentComposition{");
                jEntitySearch = jEntitySearch.Replace("Cliente{", "VendaAtacadoParentComposition{");
                var entity = this.Context.GetVendaAtacadoParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaAtacadoParentComposition), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.VendaAtacadoParentComposition>);
        }
        
        [VarejoWizardExampleControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Cliente> GetVendaAtacado__Cliente(Int32 key0, string navigation)
        {
            var entity = this.Context.GetVendaAtacadoByKey(key0);
            if (entity != null && navigation == "Cliente")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.Cliente[] { entity.Cliente }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.Cliente>);
        }
        #endregion
        
    }
    
    public partial class VarejoWizardExampleControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return LinxAutorization.CheckAuthorization(actionContext, string.Format("{0}#{1}#{1}/{2}", "VAREJO.BV", "VarejoWizardExample", actionContext.ActionDescriptor.ActionName));
        }
    }
}

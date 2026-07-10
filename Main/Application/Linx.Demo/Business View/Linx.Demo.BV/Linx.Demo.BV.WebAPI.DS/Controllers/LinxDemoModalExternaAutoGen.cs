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
using BusinessNS = Linx.Demo.BV.ModalExterna;

namespace Linx.Demo.BV.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/LinxDemoModalExterna/[ActionName]
    // Security Information Call: http://localhost:1710/LinxDemoModalExterna/GetSecurityInfo
    // Entities Catalog Call: http://localhost:1710/LinxDemoModalExterna/GetEntities
    // Entity MetaData Call: http://localhost:1710/LinxDemoModalExterna/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/LinxDemoModalExterna/GetClientDomains?erp=true
    // Client Service Call: http://localhost:1710/LinxDemoModalExterna/GetClientService?erp=true
    // Client Factory Call: http://localhost:1710/LinxDemoModalExterna/GetClientFactory?entityName=[EntityName]&erp=true
    // Client Factory Custom Events Call: http://localhost:1710/LinxDemoModalExterna/GetClientFactoryCustomEvents?entityName=[EntityName]&erp=true
    // Help Call: http://localhost:1710/HelpController/LinxDemoModalExterna
    // Feed OData Call: http://localhost:1710/LinxDemoModalExternaOData
    [RoutePrefix("LinxDemoModalExterna")]
    [Breeze.WebApi2.BreezeController]
    [ODataBasicAuthenticationFilter]
    public partial class LinxDemoModalExternaController : ApiController
    {
        private const int maxObjectExcelReturned = 2300000;
        private DataServiceRepository<BusinessNS.ModalExternaDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.ModalExternaDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.ModalExternaDomainService>(typeof(BusinessNS.Cliente), typeof(BusinessNS.Estado), typeof(BusinessNS.FormaPagamento), typeof(BusinessNS.Loja), typeof(BusinessNS.Venda), typeof(BusinessNS.VendaItem)); _repository.Context.IsSecure = true; } return _repository; } }
        public LinxDemoModalExternaController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(LinxDemoModalExternaController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.ModalExternaDomainService).Assembly.FullName,
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
            var result = repository.Context.GetMetaDataObject("Linx.Demo.BV.ModalExterna." + entityName, false, true);
            return (allComposition ? result : result.Where(e => e.ClassName == entityName).ToList());
        }
        
        [Route("GetSecurityInfo"), System.Web.Http.HttpGet()]
        public string[] GetSecurityInfo()
        {
           return new string[] { "Linx.Demo.BV", "LinxDemoModalExterna", "LinxDemoModalExterna/ActionName" };
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
        
        [LinxDemoModalExternaControllerAuthorize]
        [Route("GetAllLookUpCliente"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpCliente> GetAllLookUpCliente()
        {
            return repository.Context.GetAllLookUpCliente();
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [Route("GetLookUpClienteByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpCliente> GetLookUpClienteByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpClienteByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [Route("GetAllLookUpVenda"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpVenda> GetAllLookUpVenda()
        {
            return repository.Context.GetAllLookUpVenda();
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [Route("GetLookUpVendaByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpVenda> GetLookUpVendaByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpVendaByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [Route("GetAllLookUpEstado"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpEstado> GetAllLookUpEstado()
        {
            return repository.Context.GetAllLookUpEstado();
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [Route("GetLookUpEstadoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpEstado> GetLookUpEstadoByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpEstadoByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [Route("GetAllLookUpLoja"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpLoja> GetAllLookUpLoja()
        {
            return repository.Context.GetAllLookUpLoja();
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [Route("GetLookUpLojaByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpLoja> GetLookUpLojaByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpLojaByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [Route("GetAllLookUpPais"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpPais> GetAllLookUpPais()
        {
            return repository.Context.GetAllLookUpPais();
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [Route("GetLookUpPaisByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpPais> GetLookUpPaisByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpPaisByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
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
        
        [LinxDemoModalExternaControllerAuthorize]
        [Route("GetCliente"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Cliente> GetCliente()
        {
            return repository.Context.GetCliente();
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [Route("GetClienteNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Cliente> GetClienteNoAssociations()
        {
            return repository.Context.GetClienteNoAssociations();
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [Route("GetClienteByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Cliente> GetClienteByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetClienteByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Cliente), jEntitySearch, false, true, false), jEntitySearch);
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [Route("GetClienteByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Cliente> GetClienteByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetClienteByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Cliente), jEntitySearch, false, true, false), jEntitySearch);
        }
        [Route("GetClienteToExcel"), System.Web.Http.HttpPost()]
        [LinxDemoModalExternaControllerAuthorize]
        public string GetClienteToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Cliente), jEntitySearch, false, true, false);
            var entities = repository.Context.GetClienteByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdCliente asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.ModalExterna.Cliente");
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
        [LinxDemoModalExternaControllerAuthorize]
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.ModalExterna.Cliente", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Demo.BV.Reports", DataSourceFullName = "Linx.Demo.BV.Reports.ModalExternaDataSource", DataSourceObject = "GetCliente", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [Route("GetSampleCliente"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Cliente> GetSampleCliente(string details)
        {
            var result = repository.Context.GetClienteByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [Route("GetVenda"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Venda> GetVenda()
        {
            return repository.Context.GetVenda();
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [Route("GetVendaNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Venda> GetVendaNoAssociations()
        {
            return repository.Context.GetVendaNoAssociations();
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [Route("GetVendaByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Venda> GetVendaByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetVendaByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Venda), jEntitySearch, false, true, false), jEntitySearch);
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [Route("GetVendaByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Venda> GetVendaByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetVendaByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Venda), jEntitySearch, false, true, false), jEntitySearch);
        }
        [Route("GetVendaToExcel"), System.Web.Http.HttpPost()]
        [LinxDemoModalExternaControllerAuthorize]
        public string GetVendaToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Venda), jEntitySearch, false, true, false);
            var entities = repository.Context.GetVendaByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdVenda asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.ModalExterna.Venda");
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
        [LinxDemoModalExternaControllerAuthorize]
        public string GetVendaToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Venda), jEntitySearch, false, true, false);
            var entities = repository.Context.GetVendaByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.ModalExterna.Venda", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Demo.BV.Reports", DataSourceFullName = "Linx.Demo.BV.Reports.ModalExternaDataSource", DataSourceObject = "GetVenda", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [Route("GetSampleVenda"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Venda> GetSampleVenda(string details)
        {
            var result = repository.Context.GetVendaByEntitySearchNoAssociations(null).Take(100).ToList();
               if (!details.IsNullOrEmpty())
               {
                   foreach(var entity in result)
                   {
                       entity.FillDetails(repository.Context, null, null, details.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries), 5);
                   }
               }
            return result.AsQueryable();
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [Route("GetVendaItem"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaItem> GetVendaItem()
        {
            return repository.Context.GetVendaItem();
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [Route("GetVendaItemNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaItem> GetVendaItemNoAssociations()
        {
            return repository.Context.GetVendaItemNoAssociations();
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [Route("GetVendaItemByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaItem> GetVendaItemByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetVendaItemByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaItem), jEntitySearch, false, true, false), jEntitySearch);
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [Route("GetVendaItemByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaItem> GetVendaItemByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetVendaItemByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaItem), jEntitySearch, false, true, false), jEntitySearch);
        }
        [Route("GetVendaItemToExcel"), System.Web.Http.HttpPost()]
        [LinxDemoModalExternaControllerAuthorize]
        public string GetVendaItemToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaItem), jEntitySearch, false, true, false);
            var entities = repository.Context.GetVendaItemByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdVendaItem asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.ModalExterna.VendaItem");
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
               return ExcelExportPagination<BusinessNS.VendaItem>.CreateExcelDocumentFileMapPath("VendaItem",new ExcelExportPagination<BusinessNS.VendaItem>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetVendaItemToReportXml"), System.Web.Http.HttpPost()]
        [LinxDemoModalExternaControllerAuthorize]
        public string GetVendaItemToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaItem), jEntitySearch, false, true, false);
            var entities = repository.Context.GetVendaItemByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.ModalExterna.VendaItem", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Demo.BV.Reports", DataSourceFullName = "Linx.Demo.BV.Reports.ModalExternaDataSource", DataSourceObject = "GetVendaItem", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [Route("GetSampleVendaItem"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaItem> GetSampleVendaItem(string details)
        {
            var result = repository.Context.GetVendaItemByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [Route("GetFormaPagamento"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.FormaPagamento> GetFormaPagamento()
        {
            return repository.Context.GetFormaPagamento();
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [Route("GetFormaPagamentoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.FormaPagamento> GetFormaPagamentoNoAssociations()
        {
            return repository.Context.GetFormaPagamentoNoAssociations();
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [Route("GetFormaPagamentoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.FormaPagamento> GetFormaPagamentoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetFormaPagamentoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.FormaPagamento), jEntitySearch, false, true, false), jEntitySearch);
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [Route("GetFormaPagamentoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.FormaPagamento> GetFormaPagamentoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetFormaPagamentoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.FormaPagamento), jEntitySearch, false, true, false), jEntitySearch);
        }
        [Route("GetFormaPagamentoToExcel"), System.Web.Http.HttpPost()]
        [LinxDemoModalExternaControllerAuthorize]
        public string GetFormaPagamentoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.FormaPagamento), jEntitySearch, false, true, false);
            var entities = repository.Context.GetFormaPagamentoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdFormaPagamento asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.ModalExterna.FormaPagamento");
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
               return ExcelExportPagination<BusinessNS.FormaPagamento>.CreateExcelDocumentFileMapPath("FormaPagamento",new ExcelExportPagination<BusinessNS.FormaPagamento>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetFormaPagamentoToReportXml"), System.Web.Http.HttpPost()]
        [LinxDemoModalExternaControllerAuthorize]
        public string GetFormaPagamentoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.FormaPagamento), jEntitySearch, false, true, false);
            var entities = repository.Context.GetFormaPagamentoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.ModalExterna.FormaPagamento", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Demo.BV.Reports", DataSourceFullName = "Linx.Demo.BV.Reports.ModalExternaDataSource", DataSourceObject = "GetFormaPagamento", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [Route("GetSampleFormaPagamento"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.FormaPagamento> GetSampleFormaPagamento(string details)
        {
            var result = repository.Context.GetFormaPagamentoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [Route("GetLoja"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Loja> GetLoja()
        {
            return repository.Context.GetLoja();
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [Route("GetLojaNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Loja> GetLojaNoAssociations()
        {
            return repository.Context.GetLojaNoAssociations();
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [Route("GetLojaByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Loja> GetLojaByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetLojaByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Loja), jEntitySearch, false, true, false), jEntitySearch);
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [Route("GetLojaByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Loja> GetLojaByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetLojaByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Loja), jEntitySearch, false, true, false), jEntitySearch);
        }
        [Route("GetLojaToExcel"), System.Web.Http.HttpPost()]
        [LinxDemoModalExternaControllerAuthorize]
        public string GetLojaToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Loja), jEntitySearch, false, true, false);
            var entities = repository.Context.GetLojaByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdLoja asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.ModalExterna.Loja");
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
               return ExcelExportPagination<BusinessNS.Loja>.CreateExcelDocumentFileMapPath("Loja",new ExcelExportPagination<BusinessNS.Loja>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetLojaToReportXml"), System.Web.Http.HttpPost()]
        [LinxDemoModalExternaControllerAuthorize]
        public string GetLojaToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Loja), jEntitySearch, false, true, false);
            var entities = repository.Context.GetLojaByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.ModalExterna.Loja", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Demo.BV.Reports", DataSourceFullName = "Linx.Demo.BV.Reports.ModalExternaDataSource", DataSourceObject = "GetLoja", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [Route("GetSampleLoja"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Loja> GetSampleLoja(string details)
        {
            var result = repository.Context.GetLojaByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [Route("GetEstado"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Estado> GetEstado()
        {
            return repository.Context.GetEstado();
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [Route("GetEstadoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Estado> GetEstadoNoAssociations()
        {
            return repository.Context.GetEstadoNoAssociations();
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [Route("GetEstadoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Estado> GetEstadoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetEstadoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Estado), jEntitySearch, false, true, false), jEntitySearch);
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [Route("GetEstadoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Estado> GetEstadoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetEstadoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Estado), jEntitySearch, false, true, false), jEntitySearch);
        }
        [Route("GetEstadoToExcel"), System.Web.Http.HttpPost()]
        [LinxDemoModalExternaControllerAuthorize]
        public string GetEstadoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Estado), jEntitySearch, false, true, false);
            var entities = repository.Context.GetEstadoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdEstado asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.ModalExterna.Estado");
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
               return ExcelExportPagination<BusinessNS.Estado>.CreateExcelDocumentFileMapPath("Estado",new ExcelExportPagination<BusinessNS.Estado>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetEstadoToReportXml"), System.Web.Http.HttpPost()]
        [LinxDemoModalExternaControllerAuthorize]
        public string GetEstadoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Estado), jEntitySearch, false, true, false);
            var entities = repository.Context.GetEstadoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.ModalExterna.Estado", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Demo.BV.Reports", DataSourceFullName = "Linx.Demo.BV.Reports.ModalExternaDataSource", DataSourceObject = "GetEstado", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [Route("GetSampleEstado"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Estado> GetSampleEstado(string details)
        {
            var result = repository.Context.GetEstadoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        #endregion
        
        #region Get Business Entities By Parent Composition
        
        [LinxDemoModalExternaControllerAuthorize]
        [Route("GetVendaItemParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaItemParentComposition> GetVendaItemParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetVendaItemParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaItemParentComposition), jEntitySearch, false, true, false), jEntitySearch);
        }
        [Route("GetVendaItemParentCompositionToExcel"), System.Web.Http.HttpPost()]
        [LinxDemoModalExternaControllerAuthorize]
        public string GetVendaItemParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("VendaItem{", "VendaItemParentComposition{");
            jEntitySearch = jEntitySearch.Replace("Venda{", "VendaItemParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaItemParentComposition), jEntitySearch, false, true, false);
            var entities = repository.Context.GetVendaItemParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdVendaItem asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.ModalExterna.VendaItem");
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
               return ExcelExportPagination<BusinessNS.VendaItemParentComposition>.CreateExcelDocumentFileMapPath("VendaItem",new ExcelExportPagination<BusinessNS.VendaItemParentComposition>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetVendaItemParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        [LinxDemoModalExternaControllerAuthorize]
        public string GetVendaItemParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaItemParentComposition), jEntitySearch, false, true, false);
            var entities = repository.Context.GetVendaItemParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.ModalExterna.VendaItem", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Demo.BV.Reports", DataSourceFullName = "Linx.Demo.BV.Reports.ModalExternaDataSource", DataSourceObject = "GetVendaItemParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [Route("GetSampleVendaItemParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaItemParentComposition> GetSampleVendaItemParentComposition(string details)
        {
            var result = repository.Context.GetVendaItemParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        #endregion
        
        #region Save Changes
        [LinxDemoModalExternaControllerAuthorize]
        [Route("SaveChanges"), System.Web.Http.HttpPost()]
        public SaveResult SaveChanges(JObject saveBundle)
        {
            var result = repository.SaveChanges(saveBundle);
            repository.Context.Dispose();
            return result;
        }
        [LinxDemoModalExternaControllerAuthorize]
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
        [LinxDemoModalExternaControllerAuthorize]
        [Route("SaveClienteInCache"), System.Web.Http.HttpPost()]
        public void SaveClienteInCache(SaveInformation<BusinessNS.Cliente> saveInfo)
        {
            saveInfo.Validate();
            QueueTransaction.SaveTransaction(saveInfo, System.Reflection.Assembly.GetExecutingAssembly().FullName, this.ControllerContext, "SaveCliente");
        }
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
        [LinxDemoModalExternaControllerAuthorize]
        [Route("SaveVenda"), System.Web.Http.HttpPost()]
        public List<BusinessNS.Venda> SaveVenda(List<BusinessNS.Venda> dataList)
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
        [LinxDemoModalExternaControllerAuthorize]
        [Route("SaveVendaInCache"), System.Web.Http.HttpPost()]
        public void SaveVendaInCache(SaveInformation<BusinessNS.Venda> saveInfo)
        {
            saveInfo.Validate();
            QueueTransaction.SaveTransaction(saveInfo, System.Reflection.Assembly.GetExecutingAssembly().FullName, this.ControllerContext, "SaveVenda");
        }
        public List<BusinessNS.Venda> SaveVenda__ForMEF(string jsonString, string viewMapInfo, List<ChangeTracker> changes)
        {
            List<BusinessNS.Venda> dataList = SerializationManager<List<BusinessNS.Venda>>.JsonToObject(jsonString);
            if (!viewMapInfo.IsNullOrEmpty() && changes.Count > 0)
            {
                var viewMap = ViewMapHelper.Parse(viewMapInfo);
                if(changes.Any(c => c.ComponentName == viewMap.ParentUIView))
                    dataList = viewMap.ReplaceEntities(dataList, changes.First(c => c.ComponentName == viewMap.ParentUIView).ListReturnedObjects);
            }
            return SaveVenda(dataList);
        }
        [LinxDemoModalExternaControllerAuthorize]
        [Route("SaveFormaPagamento"), System.Web.Http.HttpPost()]
        public List<BusinessNS.FormaPagamento> SaveFormaPagamento(List<BusinessNS.FormaPagamento> dataList)
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
        [LinxDemoModalExternaControllerAuthorize]
        [Route("SaveFormaPagamentoInCache"), System.Web.Http.HttpPost()]
        public void SaveFormaPagamentoInCache(SaveInformation<BusinessNS.FormaPagamento> saveInfo)
        {
            saveInfo.Validate();
            QueueTransaction.SaveTransaction(saveInfo, System.Reflection.Assembly.GetExecutingAssembly().FullName, this.ControllerContext, "SaveFormaPagamento");
        }
        public List<BusinessNS.FormaPagamento> SaveFormaPagamento__ForMEF(string jsonString, string viewMapInfo, List<ChangeTracker> changes)
        {
            List<BusinessNS.FormaPagamento> dataList = SerializationManager<List<BusinessNS.FormaPagamento>>.JsonToObject(jsonString);
            if (!viewMapInfo.IsNullOrEmpty() && changes.Count > 0)
            {
                var viewMap = ViewMapHelper.Parse(viewMapInfo);
                if(changes.Any(c => c.ComponentName == viewMap.ParentUIView))
                    dataList = viewMap.ReplaceEntities(dataList, changes.First(c => c.ComponentName == viewMap.ParentUIView).ListReturnedObjects);
            }
            return SaveFormaPagamento(dataList);
        }
        [LinxDemoModalExternaControllerAuthorize]
        [Route("SaveLoja"), System.Web.Http.HttpPost()]
        public List<BusinessNS.Loja> SaveLoja(List<BusinessNS.Loja> dataList)
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
        [LinxDemoModalExternaControllerAuthorize]
        [Route("SaveLojaInCache"), System.Web.Http.HttpPost()]
        public void SaveLojaInCache(SaveInformation<BusinessNS.Loja> saveInfo)
        {
            saveInfo.Validate();
            QueueTransaction.SaveTransaction(saveInfo, System.Reflection.Assembly.GetExecutingAssembly().FullName, this.ControllerContext, "SaveLoja");
        }
        public List<BusinessNS.Loja> SaveLoja__ForMEF(string jsonString, string viewMapInfo, List<ChangeTracker> changes)
        {
            List<BusinessNS.Loja> dataList = SerializationManager<List<BusinessNS.Loja>>.JsonToObject(jsonString);
            if (!viewMapInfo.IsNullOrEmpty() && changes.Count > 0)
            {
                var viewMap = ViewMapHelper.Parse(viewMapInfo);
                if(changes.Any(c => c.ComponentName == viewMap.ParentUIView))
                    dataList = viewMap.ReplaceEntities(dataList, changes.First(c => c.ComponentName == viewMap.ParentUIView).ListReturnedObjects);
            }
            return SaveLoja(dataList);
        }
        [LinxDemoModalExternaControllerAuthorize]
        [Route("SaveEstado"), System.Web.Http.HttpPost()]
        public List<BusinessNS.Estado> SaveEstado(List<BusinessNS.Estado> dataList)
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
        [LinxDemoModalExternaControllerAuthorize]
        [Route("SaveEstadoInCache"), System.Web.Http.HttpPost()]
        public void SaveEstadoInCache(SaveInformation<BusinessNS.Estado> saveInfo)
        {
            saveInfo.Validate();
            QueueTransaction.SaveTransaction(saveInfo, System.Reflection.Assembly.GetExecutingAssembly().FullName, this.ControllerContext, "SaveEstado");
        }
        public List<BusinessNS.Estado> SaveEstado__ForMEF(string jsonString, string viewMapInfo, List<ChangeTracker> changes)
        {
            List<BusinessNS.Estado> dataList = SerializationManager<List<BusinessNS.Estado>>.JsonToObject(jsonString);
            if (!viewMapInfo.IsNullOrEmpty() && changes.Count > 0)
            {
                var viewMap = ViewMapHelper.Parse(viewMapInfo);
                if(changes.Any(c => c.ComponentName == viewMap.ParentUIView))
                    dataList = viewMap.ReplaceEntities(dataList, changes.First(c => c.ComponentName == viewMap.ParentUIView).ListReturnedObjects);
            }
            return SaveEstado(dataList);
        }
        
        
        [LinxDemoModalExternaControllerAuthorize]
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
        [LinxDemoModalExternaControllerAuthorize]
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
    public partial class LinxDemoModalExternaFeedController : ODataController
    {
        private BusinessNS.ModalExternaDomainService _context;
        public BusinessNS.ModalExternaDomainService Context { get {  if (_context == null) { _context = new BusinessNS.ModalExternaDomainService(); _context.IsSecure = true; } return _context; }  }
        
        #region Get Action to Business Entities
        
        
        [LinxDemoModalExternaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Cliente> GetClienteById([FromODataUri]int key0)
        {
            var entity = this.Context.GetClienteByKey(key0);
            if (entity != null)
               return (new BusinessNS.Cliente[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.Cliente>);
        }
        
        [LinxDemoModalExternaControllerAuthorize]
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
        
        [LinxDemoModalExternaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Cliente> GetCliente()
        {
            return this.Context.GetClienteByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Venda> GetVendaById([FromODataUri]int key0)
        {
            var entity = this.Context.GetVendaByKey(key0);
            if (entity != null)
               return (new BusinessNS.Venda[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.Venda>);
        }
        
        [LinxDemoModalExternaControllerAuthorize]
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
        
        [LinxDemoModalExternaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Venda> GetVenda()
        {
            return this.Context.GetVendaByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaItem> GetVenda__VendaItem(int key0, string navigation)
        {
            var entity = this.Context.GetVendaByKey(key0);
            if (entity != null && navigation == "VendaItemList")
            {
               entity.FillDetails(_context, null, null, new string[] { "VendaItem" });
               return entity.VendaItemList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.VendaItem>);
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaItem> GetVendaItemById([FromODataUri]int key0)
        {
            var entity = this.Context.GetVendaItemByKey(key0);
            if (entity != null)
               return (new BusinessNS.VendaItem[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.VendaItem>);
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaItem> GetVendaItemByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetVendaItemByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaItem), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.VendaItem>);
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaItem> GetVendaItem()
        {
            return this.Context.GetVendaItemByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaItemParentComposition> GetVendaItemParentComposition()
        {
            return this.Context.GetVendaItemParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaItemParentComposition> GetVendaItemParentCompositionByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                jEntitySearch = jEntitySearch.Replace("VendaItem{", "VendaItemParentComposition{");
                jEntitySearch = jEntitySearch.Replace("Venda{", "VendaItemParentComposition{");
                var entity = this.Context.GetVendaItemParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaItemParentComposition), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.VendaItemParentComposition>);
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Venda> GetVendaItem__Venda(int key0, string navigation)
        {
            var entity = this.Context.GetVendaItemByKey(key0);
            if (entity != null && navigation == "Venda")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.Venda[] { entity.Venda }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.Venda>);
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.FormaPagamento> GetFormaPagamentoById([FromODataUri]int key0)
        {
            var entity = this.Context.GetFormaPagamentoByKey(key0);
            if (entity != null)
               return (new BusinessNS.FormaPagamento[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.FormaPagamento>);
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.FormaPagamento> GetFormaPagamentoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetFormaPagamentoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.FormaPagamento), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.FormaPagamento>);
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.FormaPagamento> GetFormaPagamento()
        {
            return this.Context.GetFormaPagamentoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Loja> GetLojaById([FromODataUri]int key0)
        {
            var entity = this.Context.GetLojaByKey(key0);
            if (entity != null)
               return (new BusinessNS.Loja[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.Loja>);
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Loja> GetLojaByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetLojaByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Loja), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.Loja>);
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Loja> GetLoja()
        {
            return this.Context.GetLojaByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Estado> GetEstadoById([FromODataUri]int key0)
        {
            var entity = this.Context.GetEstadoByKey(key0);
            if (entity != null)
               return (new BusinessNS.Estado[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.Estado>);
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Estado> GetEstadoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetEstadoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Estado), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.Estado>);
        }
        
        [LinxDemoModalExternaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Estado> GetEstado()
        {
            return this.Context.GetEstadoByEntitySearchNoAssociations(null).AsQueryable();
        }
        #endregion
        
    }
    
    public partial class LinxDemoModalExternaControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return LinxAutorization.CheckAuthorization(actionContext, string.Format("{0}#{1}#{1}/{2}", "Linx.Demo.BV", "LinxDemoModalExterna", actionContext.ActionDescriptor.ActionName));
        }
    }
}

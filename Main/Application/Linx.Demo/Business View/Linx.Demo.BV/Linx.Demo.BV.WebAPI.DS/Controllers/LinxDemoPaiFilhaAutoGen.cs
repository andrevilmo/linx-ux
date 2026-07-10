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
using BusinessNS = Linx.Demo.BV.PaiFilha;

namespace Linx.Demo.BV.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/LinxDemoPaiFilha/[ActionName]
    // Security Information Call: http://localhost:1710/LinxDemoPaiFilha/GetSecurityInfo
    // Entities Catalog Call: http://localhost:1710/LinxDemoPaiFilha/GetEntities
    // Entity MetaData Call: http://localhost:1710/LinxDemoPaiFilha/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/LinxDemoPaiFilha/GetClientDomains?erp=true
    // Client Service Call: http://localhost:1710/LinxDemoPaiFilha/GetClientService?erp=true
    // Client Factory Call: http://localhost:1710/LinxDemoPaiFilha/GetClientFactory?entityName=[EntityName]&erp=true
    // Client Factory Custom Events Call: http://localhost:1710/LinxDemoPaiFilha/GetClientFactoryCustomEvents?entityName=[EntityName]&erp=true
    // Help Call: http://localhost:1710/HelpController/LinxDemoPaiFilha
    // Feed OData Call: http://localhost:1710/LinxDemoPaiFilhaOData
    [RoutePrefix("LinxDemoPaiFilha")]
    [Breeze.WebApi2.BreezeController]
    [ODataBasicAuthenticationFilter]
    public partial class LinxDemoPaiFilhaController : ApiController
    {
        private const int maxObjectExcelReturned = 2300000;
        private DataServiceRepository<BusinessNS.PaiFilhaDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.PaiFilhaDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.PaiFilhaDomainService>(typeof(BusinessNS.Cliente), typeof(BusinessNS.Loja), typeof(BusinessNS.Venda), typeof(BusinessNS.VendaAtacado), typeof(BusinessNS.VendaItem), typeof(BusinessNS.Vendedor)); _repository.Context.IsSecure = true; } return _repository; } }
        public LinxDemoPaiFilhaController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(LinxDemoPaiFilhaController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.PaiFilhaDomainService).Assembly.FullName,
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
            var result = repository.Context.GetMetaDataObject("Linx.Demo.BV.PaiFilha." + entityName, false, true);
            return (allComposition ? result : result.Where(e => e.ClassName == entityName).ToList());
        }
        
        [Route("GetSecurityInfo"), System.Web.Http.HttpGet()]
        public string[] GetSecurityInfo()
        {
           return new string[] { "Linx.Demo.BV", "LinxDemoPaiFilha", "LinxDemoPaiFilha/ActionName" };
        }
        
        [Route("GetEntities"), System.Web.Http.HttpGet()]
        public object[] GetEntities()
        {
            return new object[] { 
                               new {
                                   Name = "Cliente", ListName = ""
                                   , Details = new object[] { 
                                   new {
                                       Name = "Venda", ListName = "VendaList"
                                       , Details = new object[] { 
                                       new {
                                           Name = "VendaItem", ListName = "VendaItemList"
                                           , Details = new object[] { 
                                           }
                                       }
                                       }
                                   }
                                   , new {
                                       Name = "VendaAtacado", ListName = "VendaAtacadoList"
                                       , Details = new object[] { 
                                       }
                                   }
                                   }
                               }
                               , new {
                                   Name = "Loja", ListName = ""
                                   , Details = new object[] { 
                                   new {
                                       Name = "Vendedor", ListName = "VendedorList"
                                       , Details = new object[] { 
                                       }
                                   }
                                   }
                               }
            };
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
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("GetAllLookUpEstado"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpEstado> GetAllLookUpEstado()
        {
            return repository.Context.GetAllLookUpEstado();
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("GetLookUpEstadoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpEstado> GetLookUpEstadoByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpEstadoByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("GetAllLookUpLoja"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpLoja> GetAllLookUpLoja()
        {
            return repository.Context.GetAllLookUpLoja();
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
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
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("GetCliente"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Cliente> GetCliente()
        {
            return repository.Context.GetCliente();
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("GetClienteNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Cliente> GetClienteNoAssociations()
        {
            return repository.Context.GetClienteNoAssociations();
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("GetClienteByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Cliente> GetClienteByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetClienteByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Cliente), jEntitySearch, false, true, false), jEntitySearch);
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("GetClienteByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Cliente> GetClienteByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetClienteByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Cliente), jEntitySearch, false, true, false), jEntitySearch);
        }
        [Route("GetClienteToExcel"), System.Web.Http.HttpPost()]
        [LinxDemoPaiFilhaControllerAuthorize]
        public string GetClienteToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Cliente), jEntitySearch, false, true, false);
            var entities = repository.Context.GetClienteByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdCliente asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.PaiFilha.Cliente");
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
        [LinxDemoPaiFilhaControllerAuthorize]
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.PaiFilha.Cliente", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Demo.BV.Reports", DataSourceFullName = "Linx.Demo.BV.Reports.PaiFilhaDataSource", DataSourceObject = "GetCliente", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
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
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("GetVenda"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Venda> GetVenda()
        {
            return repository.Context.GetVenda();
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("GetVendaNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Venda> GetVendaNoAssociations()
        {
            return repository.Context.GetVendaNoAssociations();
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("GetVendaByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Venda> GetVendaByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetVendaByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Venda), jEntitySearch, false, true, false), jEntitySearch);
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("GetVendaByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Venda> GetVendaByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetVendaByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Venda), jEntitySearch, false, true, false), jEntitySearch);
        }
        [Route("GetVendaToExcel"), System.Web.Http.HttpPost()]
        [LinxDemoPaiFilhaControllerAuthorize]
        public string GetVendaToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Venda), jEntitySearch, false, true, false);
            var entities = repository.Context.GetVendaByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdVenda asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.PaiFilha.Venda");
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
        [LinxDemoPaiFilhaControllerAuthorize]
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.PaiFilha.Venda", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Demo.BV.Reports", DataSourceFullName = "Linx.Demo.BV.Reports.PaiFilhaDataSource", DataSourceObject = "GetVenda", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
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
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("GetVendaAtacado"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaAtacado> GetVendaAtacado()
        {
            return repository.Context.GetVendaAtacado();
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("GetVendaAtacadoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaAtacado> GetVendaAtacadoNoAssociations()
        {
            return repository.Context.GetVendaAtacadoNoAssociations();
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("GetVendaAtacadoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaAtacado> GetVendaAtacadoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetVendaAtacadoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaAtacado), jEntitySearch, false, true, false), jEntitySearch);
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("GetVendaAtacadoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaAtacado> GetVendaAtacadoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetVendaAtacadoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaAtacado), jEntitySearch, false, true, false), jEntitySearch);
        }
        [Route("GetVendaAtacadoToExcel"), System.Web.Http.HttpPost()]
        [LinxDemoPaiFilhaControllerAuthorize]
        public string GetVendaAtacadoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaAtacado), jEntitySearch, false, true, false);
            var entities = repository.Context.GetVendaAtacadoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdVendaAtacado asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.PaiFilha.VendaAtacado");
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
        [LinxDemoPaiFilhaControllerAuthorize]
        public string GetVendaAtacadoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaAtacado), jEntitySearch, false, true, false);
            var entities = repository.Context.GetVendaAtacadoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.PaiFilha.VendaAtacado", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Demo.BV.Reports", DataSourceFullName = "Linx.Demo.BV.Reports.PaiFilhaDataSource", DataSourceObject = "GetVendaAtacado", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("GetSampleVendaAtacado"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaAtacado> GetSampleVendaAtacado(string details)
        {
            var result = repository.Context.GetVendaAtacadoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("GetVendaItem"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaItem> GetVendaItem()
        {
            return repository.Context.GetVendaItem();
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("GetVendaItemNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaItem> GetVendaItemNoAssociations()
        {
            return repository.Context.GetVendaItemNoAssociations();
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("GetVendaItemByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaItem> GetVendaItemByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetVendaItemByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaItem), jEntitySearch, false, true, false), jEntitySearch);
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("GetVendaItemByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaItem> GetVendaItemByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetVendaItemByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaItem), jEntitySearch, false, true, false), jEntitySearch);
        }
        [Route("GetVendaItemToExcel"), System.Web.Http.HttpPost()]
        [LinxDemoPaiFilhaControllerAuthorize]
        public string GetVendaItemToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaItem), jEntitySearch, false, true, false);
            var entities = repository.Context.GetVendaItemByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdVendaItem asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.PaiFilha.VendaItem");
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
        [LinxDemoPaiFilhaControllerAuthorize]
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.PaiFilha.VendaItem", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Demo.BV.Reports", DataSourceFullName = "Linx.Demo.BV.Reports.PaiFilhaDataSource", DataSourceObject = "GetVendaItem", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("GetSampleVendaItem"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaItem> GetSampleVendaItem(string details)
        {
            var result = repository.Context.GetVendaItemByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("GetLoja"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Loja> GetLoja()
        {
            return repository.Context.GetLoja();
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("GetLojaNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Loja> GetLojaNoAssociations()
        {
            return repository.Context.GetLojaNoAssociations();
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("GetLojaByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Loja> GetLojaByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetLojaByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Loja), jEntitySearch, false, true, false), jEntitySearch);
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("GetLojaByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Loja> GetLojaByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetLojaByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Loja), jEntitySearch, false, true, false), jEntitySearch);
        }
        [Route("GetLojaToExcel"), System.Web.Http.HttpPost()]
        [LinxDemoPaiFilhaControllerAuthorize]
        public string GetLojaToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Loja), jEntitySearch, false, true, false);
            var entities = repository.Context.GetLojaByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdLoja asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.PaiFilha.Loja");
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
        [LinxDemoPaiFilhaControllerAuthorize]
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.PaiFilha.Loja", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Demo.BV.Reports", DataSourceFullName = "Linx.Demo.BV.Reports.PaiFilhaDataSource", DataSourceObject = "GetLoja", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("GetSampleLoja"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Loja> GetSampleLoja(string details)
        {
            var result = repository.Context.GetLojaByEntitySearchNoAssociations(null).Take(100).ToList();
               if (!details.IsNullOrEmpty())
               {
                   foreach(var entity in result)
                   {
                       entity.FillDetails(repository.Context, null, null, details.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries), 5);
                   }
               }
            return result.AsQueryable();
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("GetVendedor"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Vendedor> GetVendedor()
        {
            return repository.Context.GetVendedor();
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("GetVendedorNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Vendedor> GetVendedorNoAssociations()
        {
            return repository.Context.GetVendedorNoAssociations();
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("GetVendedorByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Vendedor> GetVendedorByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetVendedorByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Vendedor), jEntitySearch, false, true, false), jEntitySearch);
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("GetVendedorByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Vendedor> GetVendedorByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetVendedorByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Vendedor), jEntitySearch, false, true, false), jEntitySearch);
        }
        [Route("GetVendedorToExcel"), System.Web.Http.HttpPost()]
        [LinxDemoPaiFilhaControllerAuthorize]
        public string GetVendedorToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Vendedor), jEntitySearch, false, true, false);
            var entities = repository.Context.GetVendedorByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdVendedor asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.PaiFilha.Vendedor");
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
               return ExcelExportPagination<BusinessNS.Vendedor>.CreateExcelDocumentFileMapPath("Vendedor",new ExcelExportPagination<BusinessNS.Vendedor>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetVendedorToReportXml"), System.Web.Http.HttpPost()]
        [LinxDemoPaiFilhaControllerAuthorize]
        public string GetVendedorToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Vendedor), jEntitySearch, false, true, false);
            var entities = repository.Context.GetVendedorByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.PaiFilha.Vendedor", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Demo.BV.Reports", DataSourceFullName = "Linx.Demo.BV.Reports.PaiFilhaDataSource", DataSourceObject = "GetVendedor", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("GetSampleVendedor"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Vendedor> GetSampleVendedor(string details)
        {
            var result = repository.Context.GetVendedorByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        #endregion
        
        #region Get Business Entities By Parent Composition
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("GetVendaParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaParentComposition> GetVendaParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetVendaParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaParentComposition), jEntitySearch, false, true, false), jEntitySearch);
        }
        [Route("GetVendaParentCompositionToExcel"), System.Web.Http.HttpPost()]
        [LinxDemoPaiFilhaControllerAuthorize]
        public string GetVendaParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("Venda{", "VendaParentComposition{");
            jEntitySearch = jEntitySearch.Replace("Cliente{", "VendaParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaParentComposition), jEntitySearch, false, true, false);
            var entities = repository.Context.GetVendaParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdVenda asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.PaiFilha.Venda");
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
        [LinxDemoPaiFilhaControllerAuthorize]
        public string GetVendaParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaParentComposition), jEntitySearch, false, true, false);
            var entities = repository.Context.GetVendaParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.PaiFilha.Venda", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Demo.BV.Reports", DataSourceFullName = "Linx.Demo.BV.Reports.PaiFilhaDataSource", DataSourceObject = "GetVendaParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("GetSampleVendaParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaParentComposition> GetSampleVendaParentComposition(string details)
        {
            var result = repository.Context.GetVendaParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("GetVendaAtacadoParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaAtacadoParentComposition> GetVendaAtacadoParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetVendaAtacadoParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaAtacadoParentComposition), jEntitySearch, false, true, false), jEntitySearch);
        }
        [Route("GetVendaAtacadoParentCompositionToExcel"), System.Web.Http.HttpPost()]
        [LinxDemoPaiFilhaControllerAuthorize]
        public string GetVendaAtacadoParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("VendaAtacado{", "VendaAtacadoParentComposition{");
            jEntitySearch = jEntitySearch.Replace("Cliente{", "VendaAtacadoParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaAtacadoParentComposition), jEntitySearch, false, true, false);
            var entities = repository.Context.GetVendaAtacadoParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdVendaAtacado asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.PaiFilha.VendaAtacado");
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
        [LinxDemoPaiFilhaControllerAuthorize]
        public string GetVendaAtacadoParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaAtacadoParentComposition), jEntitySearch, false, true, false);
            var entities = repository.Context.GetVendaAtacadoParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.PaiFilha.VendaAtacado", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Demo.BV.Reports", DataSourceFullName = "Linx.Demo.BV.Reports.PaiFilhaDataSource", DataSourceObject = "GetVendaAtacadoParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("GetSampleVendaAtacadoParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaAtacadoParentComposition> GetSampleVendaAtacadoParentComposition(string details)
        {
            var result = repository.Context.GetVendaAtacadoParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("GetVendaItemParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaItemParentComposition> GetVendaItemParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetVendaItemParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaItemParentComposition), jEntitySearch, false, true, false), jEntitySearch);
        }
        [Route("GetVendaItemParentCompositionToExcel"), System.Web.Http.HttpPost()]
        [LinxDemoPaiFilhaControllerAuthorize]
        public string GetVendaItemParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("VendaItem{", "VendaItemParentComposition{");
            jEntitySearch = jEntitySearch.Replace("Venda{", "VendaItemParentComposition{");
            jEntitySearch = jEntitySearch.Replace("Cliente{", "VendaItemParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaItemParentComposition), jEntitySearch, false, true, false);
            var entities = repository.Context.GetVendaItemParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdVendaItem asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.PaiFilha.VendaItem");
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
        [LinxDemoPaiFilhaControllerAuthorize]
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.PaiFilha.VendaItem", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Demo.BV.Reports", DataSourceFullName = "Linx.Demo.BV.Reports.PaiFilhaDataSource", DataSourceObject = "GetVendaItemParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("GetSampleVendaItemParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaItemParentComposition> GetSampleVendaItemParentComposition(string details)
        {
            var result = repository.Context.GetVendaItemParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("GetVendedorParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendedorParentComposition> GetVendedorParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetVendedorParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendedorParentComposition), jEntitySearch, false, true, false), jEntitySearch);
        }
        [Route("GetVendedorParentCompositionToExcel"), System.Web.Http.HttpPost()]
        [LinxDemoPaiFilhaControllerAuthorize]
        public string GetVendedorParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("Vendedor{", "VendedorParentComposition{");
            jEntitySearch = jEntitySearch.Replace("Loja{", "VendedorParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendedorParentComposition), jEntitySearch, false, true, false);
            var entities = repository.Context.GetVendedorParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdVendedor asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.PaiFilha.Vendedor");
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
               return ExcelExportPagination<BusinessNS.VendedorParentComposition>.CreateExcelDocumentFileMapPath("Vendedor",new ExcelExportPagination<BusinessNS.VendedorParentComposition>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetVendedorParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        [LinxDemoPaiFilhaControllerAuthorize]
        public string GetVendedorParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendedorParentComposition), jEntitySearch, false, true, false);
            var entities = repository.Context.GetVendedorParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.PaiFilha.Vendedor", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Demo.BV.Reports", DataSourceFullName = "Linx.Demo.BV.Reports.PaiFilhaDataSource", DataSourceObject = "GetVendedorParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("GetSampleVendedorParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendedorParentComposition> GetSampleVendedorParentComposition(string details)
        {
            var result = repository.Context.GetVendedorParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        #endregion
        
        #region Save Changes
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("SaveChanges"), System.Web.Http.HttpPost()]
        public SaveResult SaveChanges(JObject saveBundle)
        {
            var result = repository.SaveChanges(saveBundle);
            repository.Context.Dispose();
            return result;
        }
        [LinxDemoPaiFilhaControllerAuthorize]
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
        [LinxDemoPaiFilhaControllerAuthorize]
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
        [LinxDemoPaiFilhaControllerAuthorize]
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
        [LinxDemoPaiFilhaControllerAuthorize]
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
        
        
        [LinxDemoPaiFilhaControllerAuthorize]
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
        [LinxDemoPaiFilhaControllerAuthorize]
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
    public partial class LinxDemoPaiFilhaFeedController : ODataController
    {
        private BusinessNS.PaiFilhaDomainService _context;
        public BusinessNS.PaiFilhaDomainService Context { get {  if (_context == null) { _context = new BusinessNS.PaiFilhaDomainService(); _context.IsSecure = true; } return _context; }  }
        
        #region Get Action to Business Entities
        
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Cliente> GetClienteById([FromODataUri]int key0)
        {
            var entity = this.Context.GetClienteByKey(key0);
            if (entity != null)
               return (new BusinessNS.Cliente[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.Cliente>);
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
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
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Cliente> GetCliente()
        {
            return this.Context.GetClienteByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Venda> GetCliente__Venda(int key0, string navigation)
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
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaAtacado> GetCliente__VendaAtacado(int key0, string navigation)
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
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Venda> GetVendaById([FromODataUri]int key0)
        {
            var entity = this.Context.GetVendaByKey(key0);
            if (entity != null)
               return (new BusinessNS.Venda[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.Venda>);
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
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
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Venda> GetVenda()
        {
            return this.Context.GetVendaByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaParentComposition> GetVendaParentComposition()
        {
            return this.Context.GetVendaParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
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
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Cliente> GetVenda__Cliente(int key0, string navigation)
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
        
        [LinxDemoPaiFilhaControllerAuthorize]
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
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaAtacado> GetVendaAtacadoById([FromODataUri]int key0)
        {
            var entity = this.Context.GetVendaAtacadoByKey(key0);
            if (entity != null)
               return (new BusinessNS.VendaAtacado[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.VendaAtacado>);
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
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
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaAtacado> GetVendaAtacado()
        {
            return this.Context.GetVendaAtacadoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaAtacadoParentComposition> GetVendaAtacadoParentComposition()
        {
            return this.Context.GetVendaAtacadoParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
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
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Cliente> GetVendaAtacado__Cliente(int key0, string navigation)
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
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaItem> GetVendaItemById([FromODataUri]int key0)
        {
            var entity = this.Context.GetVendaItemByKey(key0);
            if (entity != null)
               return (new BusinessNS.VendaItem[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.VendaItem>);
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
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
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaItem> GetVendaItem()
        {
            return this.Context.GetVendaItemByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaItemParentComposition> GetVendaItemParentComposition()
        {
            return this.Context.GetVendaItemParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaItemParentComposition> GetVendaItemParentCompositionByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                jEntitySearch = jEntitySearch.Replace("VendaItem{", "VendaItemParentComposition{");
                jEntitySearch = jEntitySearch.Replace("Venda{", "VendaItemParentComposition{");
                jEntitySearch = jEntitySearch.Replace("Cliente{", "VendaItemParentComposition{");
                var entity = this.Context.GetVendaItemParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaItemParentComposition), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.VendaItemParentComposition>);
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
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
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Loja> GetLojaById([FromODataUri]int key0)
        {
            var entity = this.Context.GetLojaByKey(key0);
            if (entity != null)
               return (new BusinessNS.Loja[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.Loja>);
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
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
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Loja> GetLoja()
        {
            return this.Context.GetLojaByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Vendedor> GetLoja__Vendedor(int key0, string navigation)
        {
            var entity = this.Context.GetLojaByKey(key0);
            if (entity != null && navigation == "VendedorList")
            {
               entity.FillDetails(_context, null, null, new string[] { "Vendedor" });
               return entity.VendedorList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.Vendedor>);
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Vendedor> GetVendedorById([FromODataUri]int key0)
        {
            var entity = this.Context.GetVendedorByKey(key0);
            if (entity != null)
               return (new BusinessNS.Vendedor[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.Vendedor>);
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Vendedor> GetVendedorByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetVendedorByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Vendedor), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.Vendedor>);
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Vendedor> GetVendedor()
        {
            return this.Context.GetVendedorByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendedorParentComposition> GetVendedorParentComposition()
        {
            return this.Context.GetVendedorParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendedorParentComposition> GetVendedorParentCompositionByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                jEntitySearch = jEntitySearch.Replace("Vendedor{", "VendedorParentComposition{");
                jEntitySearch = jEntitySearch.Replace("Loja{", "VendedorParentComposition{");
                var entity = this.Context.GetVendedorParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendedorParentComposition), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.VendedorParentComposition>);
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Loja> GetVendedor__Loja(int key0, string navigation)
        {
            var entity = this.Context.GetVendedorByKey(key0);
            if (entity != null && navigation == "Loja")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.Loja[] { entity.Loja }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.Loja>);
        }
        #endregion
        
    }
    
    public partial class LinxDemoPaiFilhaControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return LinxAutorization.CheckAuthorization(actionContext, string.Format("{0}#{1}#{1}/{2}", "Linx.Demo.BV", "LinxDemoPaiFilha", actionContext.ActionDescriptor.ActionName));
        }
    }
}

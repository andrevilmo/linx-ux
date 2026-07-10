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
        private DataServiceRepository<BusinessNS.PaiFilhaDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.PaiFilhaDomainService>(typeof(BusinessNS.Loja), typeof(BusinessNS.Venda), typeof(BusinessNS.VendaItem)); _repository.Context.IsSecure = true; } return _repository; } }
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
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("GetAllLookUpVendedor"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpVendedor> GetAllLookUpVendedor()
        {
            return repository.Context.GetAllLookUpVendedor();
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("GetLookUpVendedorByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpVendedor> GetLookUpVendedorByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpVendedorByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("GetAllLookUpCidade"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpCidade> GetAllLookUpCidade()
        {
            return repository.Context.GetAllLookUpCidade();
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("GetLookUpCidadeByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpCidade> GetLookUpCidadeByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpCidadeByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
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
            return repository.Context.GetLojaByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Loja), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("GetLojaByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Loja> GetLojaByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetLojaByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Loja), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetLojaToExcel"), System.Web.Http.HttpPost()]
        [LinxDemoPaiFilhaControllerAuthorize]
        public string GetLojaToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Loja), jEntitySearch, false, false, false);
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
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Loja), jEntitySearch, false, false, false);
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
            return repository.Context.GetVendaByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Venda), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("GetVendaByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Venda> GetVendaByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetVendaByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Venda), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetVendaToExcel"), System.Web.Http.HttpPost()]
        [LinxDemoPaiFilhaControllerAuthorize]
        public string GetVendaToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Venda), jEntitySearch, false, false, false);
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
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Venda), jEntitySearch, false, false, false);
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
            return repository.Context.GetVendaItemByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaItem), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("GetVendaItemByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaItem> GetVendaItemByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetVendaItemByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaItem), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetVendaItemToExcel"), System.Web.Http.HttpPost()]
        [LinxDemoPaiFilhaControllerAuthorize]
        public string GetVendaItemToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaItem), jEntitySearch, false, false, false);
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
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaItem), jEntitySearch, false, false, false);
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
        #endregion
        
        #region Get Business Entities By Parent Composition
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [Route("GetVendaParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaParentComposition> GetVendaParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetVendaParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaParentComposition), jEntitySearch, false, false, false), jEntitySearch);
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
            jEntitySearch = jEntitySearch.Replace("Loja{", "VendaParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaParentComposition), jEntitySearch, false, false, false);
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
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaParentComposition), jEntitySearch, false, false, false);
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
        [Route("GetVendaItemParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaItemParentComposition> GetVendaItemParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetVendaItemParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaItemParentComposition), jEntitySearch, false, false, false), jEntitySearch);
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
            jEntitySearch = jEntitySearch.Replace("Loja{", "VendaItemParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaItemParentComposition), jEntitySearch, false, false, false);
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
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaItemParentComposition), jEntitySearch, false, false, false);
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
        public IQueryable<BusinessNS.Venda> GetLoja__Venda(int key0, string navigation)
        {
            var entity = this.Context.GetLojaByKey(key0);
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
                jEntitySearch = jEntitySearch.Replace("Loja{", "VendaParentComposition{");
                var entity = this.Context.GetVendaParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaParentComposition), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.VendaParentComposition>);
        }
        
        [LinxDemoPaiFilhaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Loja> GetVenda__Loja(int key0, string navigation)
        {
            var entity = this.Context.GetVendaByKey(key0);
            if (entity != null && navigation == "Loja")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.Loja[] { entity.Loja }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.Loja>);
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
                jEntitySearch = jEntitySearch.Replace("Loja{", "VendaItemParentComposition{");
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

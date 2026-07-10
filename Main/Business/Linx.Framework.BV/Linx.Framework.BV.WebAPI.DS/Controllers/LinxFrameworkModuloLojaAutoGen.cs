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
using BusinessNS = Linx.Framework.BV.ModuloLoja;

namespace Linx.Framework.BV.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/LinxFrameworkModuloLoja/[ActionName]
    // Security Information Call: http://localhost:1710/LinxFrameworkModuloLoja/GetSecurityInfo
    // Entities Catalog Call: http://localhost:1710/LinxFrameworkModuloLoja/GetEntities
    // Entity MetaData Call: http://localhost:1710/LinxFrameworkModuloLoja/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/LinxFrameworkModuloLoja/GetClientDomains?erp=true
    // Client Service Call: http://localhost:1710/LinxFrameworkModuloLoja/GetClientService?erp=true
    // Client Factory Call: http://localhost:1710/LinxFrameworkModuloLoja/GetClientFactory?entityName=[EntityName]&erp=true
    // Client Factory Custom Events Call: http://localhost:1710/LinxFrameworkModuloLoja/GetClientFactoryCustomEvents?entityName=[EntityName]&erp=true
    // Help Call: http://localhost:1710/HelpController/LinxFrameworkModuloLoja
    // Feed OData Call: http://localhost:1710/LinxFrameworkModuloLojaOData
    [RoutePrefix("LinxFrameworkModuloLoja")]
    [Breeze.WebApi2.BreezeController]
    public partial class LinxFrameworkModuloLojaController : ApiController
    {
        private const int maxObjectExcelReturned = 2300000;
        private DataServiceRepository<BusinessNS.ModuloLojaDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.ModuloLojaDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.ModuloLojaDomainService>(typeof(BusinessNS.LjvModulo), typeof(BusinessNS.LjvModuloMenu), typeof(BusinessNS.LjvTransacaoMenu)); _repository.Context.IsSecure = true; } return _repository; } }
        public LinxFrameworkModuloLojaController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(LinxFrameworkModuloLojaController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.ModuloLojaDomainService).Assembly.FullName,
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
            var result = repository.Context.GetMetaDataObject("Linx.Framework.BV.ModuloLoja." + entityName, false, true);
            return (allComposition ? result : result.Where(e => e.ClassName == entityName).ToList());
        }
        
        [Route("GetSecurityInfo"), System.Web.Http.HttpGet()]
        public string[] GetSecurityInfo()
        {
           return new string[] { "Linx.Framework.BV", "LinxFrameworkModuloLoja", "LinxFrameworkModuloLoja/ActionName" };
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
        
        [Route("GetLjvModulo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LjvModulo> GetLjvModulo()
        {
            return repository.Context.GetLjvModulo();
        }
        
        [Route("GetLjvModuloNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LjvModulo> GetLjvModuloNoAssociations()
        {
            return repository.Context.GetLjvModuloNoAssociations();
        }
        
        [Route("GetLjvModuloByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LjvModulo> GetLjvModuloByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetLjvModuloByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LjvModulo), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetLjvModuloByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LjvModulo> GetLjvModuloByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetLjvModuloByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LjvModulo), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetLjvModuloToExcel"), System.Web.Http.HttpPost()]
        public string GetLjvModuloToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LjvModulo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetLjvModuloByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdModulo asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.ModuloLoja.LjvModulo");
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
               return ExcelExportPagination<BusinessNS.LjvModulo>.CreateExcelDocumentFileMapPath("LjvModulo",new ExcelExportPagination<BusinessNS.LjvModulo>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetLjvModuloToReportXml"), System.Web.Http.HttpPost()]
        public string GetLjvModuloToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LjvModulo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetLjvModuloByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.ModuloLoja.LjvModulo", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ModuloLojaDataSource", DataSourceObject = "GetLjvModulo", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleLjvModulo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LjvModulo> GetSampleLjvModulo(string details)
        {
            var result = repository.Context.GetLjvModuloByEntitySearchNoAssociations(null).Take(100).ToList();
               if (!details.IsNullOrEmpty())
               {
                   foreach(var entity in result)
                   {
                       entity.FillDetails(repository.Context, null, null, details.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries), 5);
                   }
               }
            return result.AsQueryable();
        }
        
        [Route("AddLjvModuloEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddLjvModuloEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LjvModulo), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetLjvModuloByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LjvModulo> GetLjvModuloByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetLjvModuloByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetLjvModuloMenu"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LjvModuloMenu> GetLjvModuloMenu()
        {
            return repository.Context.GetLjvModuloMenu();
        }
        
        [Route("GetLjvModuloMenuNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LjvModuloMenu> GetLjvModuloMenuNoAssociations()
        {
            return repository.Context.GetLjvModuloMenuNoAssociations();
        }
        
        [Route("GetLjvModuloMenuByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LjvModuloMenu> GetLjvModuloMenuByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetLjvModuloMenuByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LjvModuloMenu), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetLjvModuloMenuByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LjvModuloMenu> GetLjvModuloMenuByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetLjvModuloMenuByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LjvModuloMenu), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetLjvModuloMenuToExcel"), System.Web.Http.HttpPost()]
        public string GetLjvModuloMenuToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LjvModuloMenu), jEntitySearch, false, false, false);
            var entities = repository.Context.GetLjvModuloMenuByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdModuloMenu asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.ModuloLoja.LjvModuloMenu");
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
               return ExcelExportPagination<BusinessNS.LjvModuloMenu>.CreateExcelDocumentFileMapPath("LjvModuloMenu",new ExcelExportPagination<BusinessNS.LjvModuloMenu>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetLjvModuloMenuToReportXml"), System.Web.Http.HttpPost()]
        public string GetLjvModuloMenuToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LjvModuloMenu), jEntitySearch, false, false, false);
            var entities = repository.Context.GetLjvModuloMenuByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.ModuloLoja.LjvModuloMenu", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ModuloLojaDataSource", DataSourceObject = "GetLjvModuloMenu", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleLjvModuloMenu"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LjvModuloMenu> GetSampleLjvModuloMenu(string details)
        {
            var result = repository.Context.GetLjvModuloMenuByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddLjvModuloMenuEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddLjvModuloMenuEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LjvModuloMenu), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetLjvModuloMenuByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LjvModuloMenu> GetLjvModuloMenuByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetLjvModuloMenuByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetLjvTransacaoMenu"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LjvTransacaoMenu> GetLjvTransacaoMenu()
        {
            return repository.Context.GetLjvTransacaoMenu();
        }
        
        [Route("GetLjvTransacaoMenuNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LjvTransacaoMenu> GetLjvTransacaoMenuNoAssociations()
        {
            return repository.Context.GetLjvTransacaoMenuNoAssociations();
        }
        
        [Route("GetLjvTransacaoMenuByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LjvTransacaoMenu> GetLjvTransacaoMenuByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetLjvTransacaoMenuByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LjvTransacaoMenu), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetLjvTransacaoMenuByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LjvTransacaoMenu> GetLjvTransacaoMenuByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetLjvTransacaoMenuByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LjvTransacaoMenu), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetLjvTransacaoMenuToExcel"), System.Web.Http.HttpPost()]
        public string GetLjvTransacaoMenuToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LjvTransacaoMenu), jEntitySearch, false, false, false);
            var entities = repository.Context.GetLjvTransacaoMenuByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTransacaoMenu asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.ModuloLoja.LjvTransacaoMenu");
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
               return ExcelExportPagination<BusinessNS.LjvTransacaoMenu>.CreateExcelDocumentFileMapPath("LjvTransacaoMenu",new ExcelExportPagination<BusinessNS.LjvTransacaoMenu>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetLjvTransacaoMenuToReportXml"), System.Web.Http.HttpPost()]
        public string GetLjvTransacaoMenuToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LjvTransacaoMenu), jEntitySearch, false, false, false);
            var entities = repository.Context.GetLjvTransacaoMenuByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.ModuloLoja.LjvTransacaoMenu", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ModuloLojaDataSource", DataSourceObject = "GetLjvTransacaoMenu", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleLjvTransacaoMenu"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LjvTransacaoMenu> GetSampleLjvTransacaoMenu(string details)
        {
            var result = repository.Context.GetLjvTransacaoMenuByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddLjvTransacaoMenuEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddLjvTransacaoMenuEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LjvTransacaoMenu), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetLjvTransacaoMenuByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LjvTransacaoMenu> GetLjvTransacaoMenuByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetLjvTransacaoMenuByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        #endregion
        
        #region Get Business Entities By Parent Composition
        
        [Route("GetLjvModuloMenuParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LjvModuloMenuParentComposition> GetLjvModuloMenuParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetLjvModuloMenuParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LjvModuloMenuParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetLjvModuloMenuParentCompositionToExcel"), System.Web.Http.HttpPost()]
        public string GetLjvModuloMenuParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("LjvModuloMenu{", "LjvModuloMenuParentComposition{");
            jEntitySearch = jEntitySearch.Replace("LjvModulo{", "LjvModuloMenuParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LjvModuloMenuParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetLjvModuloMenuParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdModuloMenu asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.ModuloLoja.LjvModuloMenu");
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
               return ExcelExportPagination<BusinessNS.LjvModuloMenuParentComposition>.CreateExcelDocumentFileMapPath("LjvModuloMenu",new ExcelExportPagination<BusinessNS.LjvModuloMenuParentComposition>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetLjvModuloMenuParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        public string GetLjvModuloMenuParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LjvModuloMenuParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetLjvModuloMenuParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.ModuloLoja.LjvModuloMenu", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ModuloLojaDataSource", DataSourceObject = "GetLjvModuloMenuParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleLjvModuloMenuParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LjvModuloMenuParentComposition> GetSampleLjvModuloMenuParentComposition(string details)
        {
            var result = repository.Context.GetLjvModuloMenuParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
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
    
    public partial class LinxFrameworkModuloLojaFeedController : ODataController
    {
        private BusinessNS.ModuloLojaDomainService _context;
        public BusinessNS.ModuloLojaDomainService Context { get {  if (_context == null) { _context = new BusinessNS.ModuloLojaDomainService(); _context.IsSecure = true; } return _context; }  }
        
        #region Get Action to Business Entities
        
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.LjvModulo> GetLjvModuloById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetLjvModuloByKey(key0);
            if (entity != null)
               return (new BusinessNS.LjvModulo[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.LjvModulo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.LjvModulo> GetLjvModuloByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetLjvModuloByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LjvModulo), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.LjvModulo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.LjvModulo> GetLjvModulo()
        {
            return this.Context.GetLjvModuloByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.LjvModuloMenu> GetLjvModulo__LjvModuloMenu(Int64 key0, string navigation)
        {
            var entity = this.Context.GetLjvModuloByKey(key0);
            if (entity != null && navigation == "LjvModuloMenuList")
            {
               entity.FillDetails(_context, null, null, new string[] { "LjvModuloMenu" });
               return entity.LjvModuloMenuList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.LjvModuloMenu>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.LjvModuloMenu> GetLjvModuloMenuById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetLjvModuloMenuByKey(key0);
            if (entity != null)
               return (new BusinessNS.LjvModuloMenu[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.LjvModuloMenu>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.LjvModuloMenu> GetLjvModuloMenuByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetLjvModuloMenuByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LjvModuloMenu), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.LjvModuloMenu>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.LjvModuloMenu> GetLjvModuloMenu()
        {
            return this.Context.GetLjvModuloMenuByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.LjvModuloMenuParentComposition> GetLjvModuloMenuParentComposition()
        {
            return this.Context.GetLjvModuloMenuParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.LjvModuloMenuParentComposition> GetLjvModuloMenuParentCompositionByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                jEntitySearch = jEntitySearch.Replace("LjvModuloMenu{", "LjvModuloMenuParentComposition{");
                jEntitySearch = jEntitySearch.Replace("LjvModulo{", "LjvModuloMenuParentComposition{");
                var entity = this.Context.GetLjvModuloMenuParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LjvModuloMenuParentComposition), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.LjvModuloMenuParentComposition>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.LjvModulo> GetLjvModuloMenu__LjvModulo(Int64 key0, string navigation)
        {
            var entity = this.Context.GetLjvModuloMenuByKey(key0);
            if (entity != null && navigation == "LjvModulo")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.LjvModulo[] { entity.LjvModulo }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.LjvModulo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.LjvTransacaoMenu> GetLjvTransacaoMenuById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetLjvTransacaoMenuByKey(key0);
            if (entity != null)
               return (new BusinessNS.LjvTransacaoMenu[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.LjvTransacaoMenu>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.LjvTransacaoMenu> GetLjvTransacaoMenuByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetLjvTransacaoMenuByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LjvTransacaoMenu), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.LjvTransacaoMenu>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.LjvTransacaoMenu> GetLjvTransacaoMenu()
        {
            return this.Context.GetLjvTransacaoMenuByEntitySearchNoAssociations(null).AsQueryable();
        }
        #endregion
        
    }
    
    public partial class LinxFrameworkModuloLojaControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return true;
        }
    }
}

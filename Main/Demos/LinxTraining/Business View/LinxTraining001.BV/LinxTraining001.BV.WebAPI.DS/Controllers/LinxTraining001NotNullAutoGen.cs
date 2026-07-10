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
using BusinessNS = LinxTraining001.BV.NotNull;

namespace LinxTraining001.BV.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/LinxTraining001NotNull/[ActionName]
    // Entities Catalog Call: http://localhost:1710/LinxTraining001NotNull/GetEntities
    // Entity MetaData Call: http://localhost:1710/LinxTraining001NotNull/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/LinxTraining001NotNull/GetClientDomains
    // Client Service Call: http://localhost:1710/LinxTraining001NotNull/GetClientService
    // Client Factory Call: http://localhost:1710/LinxTraining001NotNull/GetClientFactory?entityName=[EntityName]
    // Client Factory Custom Events Call: http://localhost:1710/LinxTraining001NotNull/GetClientFactoryCustomEvents?entityName=[EntityName]
    // Help Call: http://localhost:1710/HelpController/LinxTraining001NotNull
    // Feed OData Call: http://localhost:1710/LinxTraining001NotNullOData
    [RoutePrefix("LinxTraining001NotNull")]
    [Breeze.WebApi2.BreezeController]
    [ODataBasicAuthenticationFilter]
    public partial class LinxTraining001NotNullController : ApiController
    {
        private DataServiceRepository<BusinessNS.NotNullDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.NotNullDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.NotNullDomainService>(typeof(BusinessNS.FilhaNotNullView), typeof(BusinessNS.PaiNotNullView), typeof(BusinessNS.TiposCamposFilhaView), typeof(BusinessNS.TiposCamposView)); _repository.Context.IsSecure = true; } return _repository; } }
        public LinxTraining001NotNullController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(LinxTraining001NotNullController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.NotNullDomainService).Assembly.FullName,
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
            var result = repository.Context.GetMetaDataObject("LinxTraining001.BV.NotNull." + entityName, false, true);
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
        
        [LinxTraining001NotNullControllerAuthorize]
        [Route("GetTiposCamposView"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TiposCamposView> GetTiposCamposView()
        {
            return repository.Context.GetTiposCamposView();
        }
        
        [LinxTraining001NotNullControllerAuthorize]
        [Route("GetTiposCamposViewNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TiposCamposView> GetTiposCamposViewNoAssociations()
        {
            return repository.Context.GetTiposCamposViewNoAssociations();
        }
        
        [LinxTraining001NotNullControllerAuthorize]
        [Route("GetTiposCamposViewByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TiposCamposView> GetTiposCamposViewByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTiposCamposViewByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TiposCamposView), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [LinxTraining001NotNullControllerAuthorize]
        [Route("GetTiposCamposViewByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TiposCamposView> GetTiposCamposViewByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTiposCamposViewByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TiposCamposView), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTiposCamposViewToExcel"), System.Web.Http.HttpPost()]
        [LinxTraining001NotNullControllerAuthorize]
        public string GetTiposCamposViewToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TiposCamposView), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTiposCamposViewByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("LinxTraining001.BV.NotNull.TiposCamposView");
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
        [Route("GetTiposCamposViewToReportXml"), System.Web.Http.HttpPost()]
        [LinxTraining001NotNullControllerAuthorize]
        public string GetTiposCamposViewToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TiposCamposView), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTiposCamposViewByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("LinxTraining001.BV.NotNull.TiposCamposView", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "LinxTraining001.BV.Reports", DataSourceFullName = "LinxTraining001.BV.Reports.NotNullDataSource", DataSourceObject = "GetTiposCamposView", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxTraining001NotNullControllerAuthorize]
        [Route("GetSampleTiposCamposView"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TiposCamposView> GetSampleTiposCamposView(string details)
        {
            var result = repository.Context.GetTiposCamposViewByEntitySearchNoAssociations(null).Take(100).ToList();
               if (!details.IsNullOrEmpty())
               {
                   foreach(var entity in result)
                   {
                       entity.FillDetails(repository.Context, null, null, details.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries), 5);
                   }
               }
            return result.AsQueryable();
        }
        
        [LinxTraining001NotNullControllerAuthorize]
        [Route("GetTiposCamposFilhaView"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TiposCamposFilhaView> GetTiposCamposFilhaView()
        {
            return repository.Context.GetTiposCamposFilhaView();
        }
        
        [LinxTraining001NotNullControllerAuthorize]
        [Route("GetTiposCamposFilhaViewNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TiposCamposFilhaView> GetTiposCamposFilhaViewNoAssociations()
        {
            return repository.Context.GetTiposCamposFilhaViewNoAssociations();
        }
        
        [LinxTraining001NotNullControllerAuthorize]
        [Route("GetTiposCamposFilhaViewByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TiposCamposFilhaView> GetTiposCamposFilhaViewByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTiposCamposFilhaViewByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TiposCamposFilhaView), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [LinxTraining001NotNullControllerAuthorize]
        [Route("GetTiposCamposFilhaViewByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TiposCamposFilhaView> GetTiposCamposFilhaViewByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTiposCamposFilhaViewByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TiposCamposFilhaView), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTiposCamposFilhaViewToExcel"), System.Web.Http.HttpPost()]
        [LinxTraining001NotNullControllerAuthorize]
        public string GetTiposCamposFilhaViewToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TiposCamposFilhaView), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTiposCamposFilhaViewByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("LinxTraining001.BV.NotNull.TiposCamposFilhaView");
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
        [Route("GetTiposCamposFilhaViewToReportXml"), System.Web.Http.HttpPost()]
        [LinxTraining001NotNullControllerAuthorize]
        public string GetTiposCamposFilhaViewToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TiposCamposFilhaView), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTiposCamposFilhaViewByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("LinxTraining001.BV.NotNull.TiposCamposFilhaView", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "LinxTraining001.BV.Reports", DataSourceFullName = "LinxTraining001.BV.Reports.NotNullDataSource", DataSourceObject = "GetTiposCamposFilhaView", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxTraining001NotNullControllerAuthorize]
        [Route("GetSampleTiposCamposFilhaView"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TiposCamposFilhaView> GetSampleTiposCamposFilhaView(string details)
        {
            var result = repository.Context.GetTiposCamposFilhaViewByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [LinxTraining001NotNullControllerAuthorize]
        [Route("GetPaiNotNullView"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.PaiNotNullView> GetPaiNotNullView()
        {
            return repository.Context.GetPaiNotNullView();
        }
        
        [LinxTraining001NotNullControllerAuthorize]
        [Route("GetPaiNotNullViewNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.PaiNotNullView> GetPaiNotNullViewNoAssociations()
        {
            return repository.Context.GetPaiNotNullViewNoAssociations();
        }
        
        [LinxTraining001NotNullControllerAuthorize]
        [Route("GetPaiNotNullViewByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.PaiNotNullView> GetPaiNotNullViewByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetPaiNotNullViewByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.PaiNotNullView), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [LinxTraining001NotNullControllerAuthorize]
        [Route("GetPaiNotNullViewByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.PaiNotNullView> GetPaiNotNullViewByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetPaiNotNullViewByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.PaiNotNullView), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetPaiNotNullViewToExcel"), System.Web.Http.HttpPost()]
        [LinxTraining001NotNullControllerAuthorize]
        public string GetPaiNotNullViewToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.PaiNotNullView), jEntitySearch, false, false, false);
            var entities = repository.Context.GetPaiNotNullViewByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("LinxTraining001.BV.NotNull.PaiNotNullView");
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
        [Route("GetPaiNotNullViewToReportXml"), System.Web.Http.HttpPost()]
        [LinxTraining001NotNullControllerAuthorize]
        public string GetPaiNotNullViewToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.PaiNotNullView), jEntitySearch, false, false, false);
            var entities = repository.Context.GetPaiNotNullViewByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("LinxTraining001.BV.NotNull.PaiNotNullView", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "LinxTraining001.BV.Reports", DataSourceFullName = "LinxTraining001.BV.Reports.NotNullDataSource", DataSourceObject = "GetPaiNotNullView", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxTraining001NotNullControllerAuthorize]
        [Route("GetSamplePaiNotNullView"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.PaiNotNullView> GetSamplePaiNotNullView(string details)
        {
            var result = repository.Context.GetPaiNotNullViewByEntitySearchNoAssociations(null).Take(100).ToList();
               if (!details.IsNullOrEmpty())
               {
                   foreach(var entity in result)
                   {
                       entity.FillDetails(repository.Context, null, null, details.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries), 5);
                   }
               }
            return result.AsQueryable();
        }
        
        [LinxTraining001NotNullControllerAuthorize]
        [Route("GetFilhaNotNullView"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.FilhaNotNullView> GetFilhaNotNullView()
        {
            return repository.Context.GetFilhaNotNullView();
        }
        
        [LinxTraining001NotNullControllerAuthorize]
        [Route("GetFilhaNotNullViewNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.FilhaNotNullView> GetFilhaNotNullViewNoAssociations()
        {
            return repository.Context.GetFilhaNotNullViewNoAssociations();
        }
        
        [LinxTraining001NotNullControllerAuthorize]
        [Route("GetFilhaNotNullViewByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.FilhaNotNullView> GetFilhaNotNullViewByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetFilhaNotNullViewByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.FilhaNotNullView), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [LinxTraining001NotNullControllerAuthorize]
        [Route("GetFilhaNotNullViewByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.FilhaNotNullView> GetFilhaNotNullViewByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetFilhaNotNullViewByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.FilhaNotNullView), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetFilhaNotNullViewToExcel"), System.Web.Http.HttpPost()]
        [LinxTraining001NotNullControllerAuthorize]
        public string GetFilhaNotNullViewToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.FilhaNotNullView), jEntitySearch, false, false, false);
            var entities = repository.Context.GetFilhaNotNullViewByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("LinxTraining001.BV.NotNull.FilhaNotNullView");
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
        [Route("GetFilhaNotNullViewToReportXml"), System.Web.Http.HttpPost()]
        [LinxTraining001NotNullControllerAuthorize]
        public string GetFilhaNotNullViewToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.FilhaNotNullView), jEntitySearch, false, false, false);
            var entities = repository.Context.GetFilhaNotNullViewByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("LinxTraining001.BV.NotNull.FilhaNotNullView", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "LinxTraining001.BV.Reports", DataSourceFullName = "LinxTraining001.BV.Reports.NotNullDataSource", DataSourceObject = "GetFilhaNotNullView", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxTraining001NotNullControllerAuthorize]
        [Route("GetSampleFilhaNotNullView"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.FilhaNotNullView> GetSampleFilhaNotNullView(string details)
        {
            var result = repository.Context.GetFilhaNotNullViewByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        #endregion
        
        #region Get Business Entities By Parent Composition
        
        [LinxTraining001NotNullControllerAuthorize]
        [Route("GetTiposCamposFilhaViewParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TiposCamposFilhaViewParentComposition> GetTiposCamposFilhaViewParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTiposCamposFilhaViewParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TiposCamposFilhaViewParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTiposCamposFilhaViewParentCompositionToExcel"), System.Web.Http.HttpPost()]
        [LinxTraining001NotNullControllerAuthorize]
        public string GetTiposCamposFilhaViewParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            jEntitySearch = jEntitySearch.Replace("TiposCamposFilhaView{", "TiposCamposFilhaViewParentComposition{");
            jEntitySearch = jEntitySearch.Replace("TiposCamposView{", "TiposCamposFilhaViewParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TiposCamposFilhaViewParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTiposCamposFilhaViewParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("LinxTraining001.BV.NotNull.TiposCamposFilhaView");
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
        [Route("GetTiposCamposFilhaViewParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        [LinxTraining001NotNullControllerAuthorize]
        public string GetTiposCamposFilhaViewParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TiposCamposFilhaViewParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTiposCamposFilhaViewParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("LinxTraining001.BV.NotNull.TiposCamposFilhaView", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "LinxTraining001.BV.Reports", DataSourceFullName = "LinxTraining001.BV.Reports.NotNullDataSource", DataSourceObject = "GetTiposCamposFilhaViewParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxTraining001NotNullControllerAuthorize]
        [Route("GetSampleTiposCamposFilhaViewParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TiposCamposFilhaViewParentComposition> GetSampleTiposCamposFilhaViewParentComposition(string details)
        {
            var result = repository.Context.GetTiposCamposFilhaViewParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [LinxTraining001NotNullControllerAuthorize]
        [Route("GetFilhaNotNullViewParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.FilhaNotNullViewParentComposition> GetFilhaNotNullViewParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetFilhaNotNullViewParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.FilhaNotNullViewParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetFilhaNotNullViewParentCompositionToExcel"), System.Web.Http.HttpPost()]
        [LinxTraining001NotNullControllerAuthorize]
        public string GetFilhaNotNullViewParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            jEntitySearch = jEntitySearch.Replace("FilhaNotNullView{", "FilhaNotNullViewParentComposition{");
            jEntitySearch = jEntitySearch.Replace("PaiNotNullView{", "FilhaNotNullViewParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.FilhaNotNullViewParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetFilhaNotNullViewParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("LinxTraining001.BV.NotNull.FilhaNotNullView");
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
        [Route("GetFilhaNotNullViewParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        [LinxTraining001NotNullControllerAuthorize]
        public string GetFilhaNotNullViewParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.FilhaNotNullViewParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetFilhaNotNullViewParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("LinxTraining001.BV.NotNull.FilhaNotNullView", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "LinxTraining001.BV.Reports", DataSourceFullName = "LinxTraining001.BV.Reports.NotNullDataSource", DataSourceObject = "GetFilhaNotNullViewParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/LinxTraining001.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxTraining001NotNullControllerAuthorize]
        [Route("GetSampleFilhaNotNullViewParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.FilhaNotNullViewParentComposition> GetSampleFilhaNotNullViewParentComposition(string details)
        {
            var result = repository.Context.GetFilhaNotNullViewParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        #endregion
        
        #region Save Changes
        [LinxTraining001NotNullControllerAuthorize]
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
    public partial class LinxTraining001NotNullFeedController : ODataController
    {
        private BusinessNS.NotNullDomainService _context;
        public BusinessNS.NotNullDomainService Context { get {  if (_context == null) { _context = new BusinessNS.NotNullDomainService(); _context.IsSecure = true; } return _context; }  }
        
        #region Get Action to Business Entities
        
        
        [LinxTraining001NotNullControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TiposCamposView> GetTiposCamposView(Int32 key0)
        {
            var entity = this.Context.GetTiposCamposViewByKey(key0);
            if (entity != null)
               return (new BusinessNS.TiposCamposView[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TiposCamposView>);
        }
        
        [LinxTraining001NotNullControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TiposCamposView> GetTiposCamposView()
        {
            return this.Context.GetTiposCamposViewByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxTraining001NotNullControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TiposCamposFilhaView> GetTiposCamposView__TiposCamposFilhaView(Int32 key0, string navigation)
        {
            var entity = this.Context.GetTiposCamposViewByKey(key0);
            if (entity != null && navigation == "TiposCamposFilhaViewList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TiposCamposFilhaView" });
               return entity.TiposCamposFilhaViewList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TiposCamposFilhaView>);
        }
        
        [LinxTraining001NotNullControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TiposCamposFilhaView> GetTiposCamposFilhaView(Int32 key0)
        {
            var entity = this.Context.GetTiposCamposFilhaViewByKey(key0);
            if (entity != null)
               return (new BusinessNS.TiposCamposFilhaView[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TiposCamposFilhaView>);
        }
        
        [LinxTraining001NotNullControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TiposCamposFilhaView> GetTiposCamposFilhaView()
        {
            return this.Context.GetTiposCamposFilhaViewByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxTraining001NotNullControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TiposCamposFilhaViewParentComposition> GetTiposCamposFilhaViewParentComposition()
        {
            return this.Context.GetTiposCamposFilhaViewParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxTraining001NotNullControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TiposCamposView> GetTiposCamposFilhaView__TiposCamposView(Int32 key0, string navigation)
        {
            var entity = this.Context.GetTiposCamposFilhaViewByKey(key0);
            if (entity != null && navigation == "TiposCamposView")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TiposCamposView[] { entity.TiposCamposView }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TiposCamposView>);
        }
        
        [LinxTraining001NotNullControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.PaiNotNullView> GetPaiNotNullView(Int32 key0)
        {
            var entity = this.Context.GetPaiNotNullViewByKey(key0);
            if (entity != null)
               return (new BusinessNS.PaiNotNullView[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.PaiNotNullView>);
        }
        
        [LinxTraining001NotNullControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.PaiNotNullView> GetPaiNotNullView()
        {
            return this.Context.GetPaiNotNullViewByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxTraining001NotNullControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.FilhaNotNullView> GetPaiNotNullView__FilhaNotNullView(Int32 key0, string navigation)
        {
            var entity = this.Context.GetPaiNotNullViewByKey(key0);
            if (entity != null && navigation == "FilhaNotNullViewList")
            {
               entity.FillDetails(_context, null, null, new string[] { "FilhaNotNullView" });
               return entity.FilhaNotNullViewList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.FilhaNotNullView>);
        }
        
        [LinxTraining001NotNullControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.FilhaNotNullView> GetFilhaNotNullView(Int32 key0)
        {
            var entity = this.Context.GetFilhaNotNullViewByKey(key0);
            if (entity != null)
               return (new BusinessNS.FilhaNotNullView[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.FilhaNotNullView>);
        }
        
        [LinxTraining001NotNullControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.FilhaNotNullView> GetFilhaNotNullView()
        {
            return this.Context.GetFilhaNotNullViewByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxTraining001NotNullControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.FilhaNotNullViewParentComposition> GetFilhaNotNullViewParentComposition()
        {
            return this.Context.GetFilhaNotNullViewParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxTraining001NotNullControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.PaiNotNullView> GetFilhaNotNullView__PaiNotNullView(Int32 key0, string navigation)
        {
            var entity = this.Context.GetFilhaNotNullViewByKey(key0);
            if (entity != null && navigation == "PaiNotNullView")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.PaiNotNullView[] { entity.PaiNotNullView }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.PaiNotNullView>);
        }
        #endregion
        
    }
    
    public partial class LinxTraining001NotNullControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return LinxAutorization.CheckAuthorization(actionContext, string.Format("{0}#{1}#{1}/{2}", "LinxTraining001.BV", "LinxTraining001NotNull", actionContext.ActionDescriptor.ActionName));
        }
    }
}

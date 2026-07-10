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
using BusinessNS = Linx.Framework.BV.LayoutArquivo;

namespace Linx.Framework.BV.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/LinxFrameworkLayoutArquivo/[ActionName]
    // Entities Catalog Call: http://localhost:1710/LinxFrameworkLayoutArquivo/GetEntities
    // Entity MetaData Call: http://localhost:1710/LinxFrameworkLayoutArquivo/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/LinxFrameworkLayoutArquivo/GetClientDomains
    // Client Service Call: http://localhost:1710/LinxFrameworkLayoutArquivo/GetClientService
    // Client Factory Call: http://localhost:1710/LinxFrameworkLayoutArquivo/GetClientFactory?entityName=[EntityName]
    // Client Factory Custom Events Call: http://localhost:1710/LinxFrameworkLayoutArquivo/GetClientFactoryCustomEvents?entityName=[EntityName]
    // Help Call: http://localhost:1710/HelpController/LinxFrameworkLayoutArquivo
    // Feed OData Call: http://localhost:1710/LinxFrameworkLayoutArquivoOData
    [RoutePrefix("LinxFrameworkLayoutArquivo")]
    [Breeze.WebApi2.BreezeController]
    public partial class LinxFrameworkLayoutArquivoController : ApiController
    {
        private DataServiceRepository<BusinessNS.LayoutArquivoDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.LayoutArquivoDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.LayoutArquivoDomainService>(typeof(BusinessNS.TcsArquivo), typeof(BusinessNS.TcsArquivoGrupo), typeof(BusinessNS.TcsArquivoGrupoVinculo), typeof(BusinessNS.TcsArquivoItem), typeof(BusinessNS.TcsArquivoItemCampo), typeof(BusinessNS.TcsArquivoLog)); _repository.Context.IsSecure = true; } return _repository; } }
        public LinxFrameworkLayoutArquivoController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(LinxFrameworkLayoutArquivoController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.LayoutArquivoDomainService).Assembly.FullName,
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
            var result = repository.Context.GetMetaDataObject("Linx.Framework.BV.LayoutArquivo." + entityName, false, true);
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
        
        [Route("GetAllLookUpArquivoItemPai"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpArquivoItemPai> GetAllLookUpArquivoItemPai()
        {
            return repository.Context.GetAllLookUpArquivoItemPai();
        }
        
        [Route("GetLookUpArquivoItemPaiByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpArquivoItemPai> GetLookUpArquivoItemPaiByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpArquivoItemPaiByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [Route("GetAllLookUpTcsArquivoGrupo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsArquivoGrupo> GetAllLookUpTcsArquivoGrupo()
        {
            return repository.Context.GetAllLookUpTcsArquivoGrupo();
        }
        
        [Route("GetLookUpTcsArquivoGrupoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsArquivoGrupo> GetLookUpTcsArquivoGrupoByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsArquivoGrupoByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.LayoutArquivo.TcsArquivo");
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.LayoutArquivo.TcsArquivo", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.LayoutArquivoDataSource", DataSourceObject = "GetTcsArquivo", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
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
        
        [Route("GetTcsArquivoItem"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsArquivoItem> GetTcsArquivoItem()
        {
            return repository.Context.GetTcsArquivoItem();
        }
        
        [Route("GetTcsArquivoItemNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsArquivoItem> GetTcsArquivoItemNoAssociations()
        {
            return repository.Context.GetTcsArquivoItemNoAssociations();
        }
        
        [Route("GetTcsArquivoItemByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsArquivoItem> GetTcsArquivoItemByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsArquivoItemByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsArquivoItem), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsArquivoItemByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsArquivoItem> GetTcsArquivoItemByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsArquivoItemByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsArquivoItem), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsArquivoItemToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsArquivoItemToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsArquivoItem), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsArquivoItemByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.LayoutArquivo.TcsArquivoItem");
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
        [Route("GetTcsArquivoItemToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsArquivoItemToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsArquivoItem), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsArquivoItemByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.LayoutArquivo.TcsArquivoItem", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.LayoutArquivoDataSource", DataSourceObject = "GetTcsArquivoItem", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsArquivoItem"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsArquivoItem> GetSampleTcsArquivoItem(string details)
        {
            var result = repository.Context.GetTcsArquivoItemByEntitySearchNoAssociations(null).Take(100).ToList();
               if (!details.IsNullOrEmpty())
               {
                   foreach(var entity in result)
                   {
                       entity.FillDetails(repository.Context, null, null, details.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries), 5);
                   }
               }
            return result.AsQueryable();
        }
        
        [Route("GetTcsArquivoItemCampo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsArquivoItemCampo> GetTcsArquivoItemCampo()
        {
            return repository.Context.GetTcsArquivoItemCampo();
        }
        
        [Route("GetTcsArquivoItemCampoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsArquivoItemCampo> GetTcsArquivoItemCampoNoAssociations()
        {
            return repository.Context.GetTcsArquivoItemCampoNoAssociations();
        }
        
        [Route("GetTcsArquivoItemCampoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsArquivoItemCampo> GetTcsArquivoItemCampoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsArquivoItemCampoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsArquivoItemCampo), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsArquivoItemCampoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsArquivoItemCampo> GetTcsArquivoItemCampoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsArquivoItemCampoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsArquivoItemCampo), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsArquivoItemCampoToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsArquivoItemCampoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsArquivoItemCampo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsArquivoItemCampoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.LayoutArquivo.TcsArquivoItemCampo");
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
        [Route("GetTcsArquivoItemCampoToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsArquivoItemCampoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsArquivoItemCampo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsArquivoItemCampoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.LayoutArquivo.TcsArquivoItemCampo", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.LayoutArquivoDataSource", DataSourceObject = "GetTcsArquivoItemCampo", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsArquivoItemCampo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsArquivoItemCampo> GetSampleTcsArquivoItemCampo(string details)
        {
            var result = repository.Context.GetTcsArquivoItemCampoByEntitySearchNoAssociations(null).Take(100).ToList();
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.LayoutArquivo.TcsArquivoLog");
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.LayoutArquivo.TcsArquivoLog", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.LayoutArquivoDataSource", DataSourceObject = "GetTcsArquivoLog", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
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
        
        [Route("GetTcsArquivoGrupoVinculo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsArquivoGrupoVinculo> GetTcsArquivoGrupoVinculo()
        {
            return repository.Context.GetTcsArquivoGrupoVinculo();
        }
        
        [Route("GetTcsArquivoGrupoVinculoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsArquivoGrupoVinculo> GetTcsArquivoGrupoVinculoNoAssociations()
        {
            return repository.Context.GetTcsArquivoGrupoVinculoNoAssociations();
        }
        
        [Route("GetTcsArquivoGrupoVinculoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsArquivoGrupoVinculo> GetTcsArquivoGrupoVinculoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsArquivoGrupoVinculoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsArquivoGrupoVinculo), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsArquivoGrupoVinculoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsArquivoGrupoVinculo> GetTcsArquivoGrupoVinculoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsArquivoGrupoVinculoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsArquivoGrupoVinculo), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsArquivoGrupoVinculoToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsArquivoGrupoVinculoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsArquivoGrupoVinculo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsArquivoGrupoVinculoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.LayoutArquivo.TcsArquivoGrupoVinculo");
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
        [Route("GetTcsArquivoGrupoVinculoToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsArquivoGrupoVinculoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsArquivoGrupoVinculo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsArquivoGrupoVinculoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.LayoutArquivo.TcsArquivoGrupoVinculo", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.LayoutArquivoDataSource", DataSourceObject = "GetTcsArquivoGrupoVinculo", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsArquivoGrupoVinculo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsArquivoGrupoVinculo> GetSampleTcsArquivoGrupoVinculo(string details)
        {
            var result = repository.Context.GetTcsArquivoGrupoVinculoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("GetTcsArquivoGrupo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsArquivoGrupo> GetTcsArquivoGrupo()
        {
            return repository.Context.GetTcsArquivoGrupo();
        }
        
        [Route("GetTcsArquivoGrupoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsArquivoGrupo> GetTcsArquivoGrupoNoAssociations()
        {
            return repository.Context.GetTcsArquivoGrupoNoAssociations();
        }
        
        [Route("GetTcsArquivoGrupoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsArquivoGrupo> GetTcsArquivoGrupoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsArquivoGrupoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsArquivoGrupo), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsArquivoGrupoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsArquivoGrupo> GetTcsArquivoGrupoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsArquivoGrupoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsArquivoGrupo), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsArquivoGrupoToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsArquivoGrupoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsArquivoGrupo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsArquivoGrupoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.LayoutArquivo.TcsArquivoGrupo");
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
        [Route("GetTcsArquivoGrupoToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsArquivoGrupoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsArquivoGrupo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsArquivoGrupoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.LayoutArquivo.TcsArquivoGrupo", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.LayoutArquivoDataSource", DataSourceObject = "GetTcsArquivoGrupo", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsArquivoGrupo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsArquivoGrupo> GetSampleTcsArquivoGrupo(string details)
        {
            var result = repository.Context.GetTcsArquivoGrupoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        #endregion
        
        #region Get Business Entities By Parent Composition
        
        [Route("GetTcsArquivoItemParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsArquivoItemParentComposition> GetTcsArquivoItemParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsArquivoItemParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsArquivoItemParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsArquivoItemParentCompositionToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsArquivoItemParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            jEntitySearch = jEntitySearch.Replace("TcsArquivoItem{", "TcsArquivoItemParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsArquivoItemParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsArquivoItemParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.LayoutArquivo.TcsArquivoItem");
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
        [Route("GetTcsArquivoItemParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsArquivoItemParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsArquivoItemParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsArquivoItemParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.LayoutArquivo.TcsArquivoItem", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.LayoutArquivoDataSource", DataSourceObject = "GetTcsArquivoItemParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsArquivoItemParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsArquivoItemParentComposition> GetSampleTcsArquivoItemParentComposition(string details)
        {
            var result = repository.Context.GetTcsArquivoItemParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("GetTcsArquivoItemCampoParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsArquivoItemCampoParentComposition> GetTcsArquivoItemCampoParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsArquivoItemCampoParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsArquivoItemCampoParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsArquivoItemCampoParentCompositionToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsArquivoItemCampoParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            jEntitySearch = jEntitySearch.Replace("TcsArquivoItemCampo{", "TcsArquivoItemCampoParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsArquivoItemCampoParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsArquivoItemCampoParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.LayoutArquivo.TcsArquivoItemCampo");
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
        [Route("GetTcsArquivoItemCampoParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsArquivoItemCampoParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsArquivoItemCampoParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsArquivoItemCampoParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.LayoutArquivo.TcsArquivoItemCampo", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.LayoutArquivoDataSource", DataSourceObject = "GetTcsArquivoItemCampoParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsArquivoItemCampoParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsArquivoItemCampoParentComposition> GetSampleTcsArquivoItemCampoParentComposition(string details)
        {
            var result = repository.Context.GetTcsArquivoItemCampoParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("GetTcsArquivoLogParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsArquivoLogParentComposition> GetTcsArquivoLogParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsArquivoLogParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsArquivoLogParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsArquivoLogParentCompositionToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsArquivoLogParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            jEntitySearch = jEntitySearch.Replace("TcsArquivoLog{", "TcsArquivoLogParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsArquivoLogParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsArquivoLogParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.LayoutArquivo.TcsArquivoLog");
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
        [Route("GetTcsArquivoLogParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsArquivoLogParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsArquivoLogParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsArquivoLogParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.LayoutArquivo.TcsArquivoLog", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.LayoutArquivoDataSource", DataSourceObject = "GetTcsArquivoLogParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsArquivoLogParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsArquivoLogParentComposition> GetSampleTcsArquivoLogParentComposition(string details)
        {
            var result = repository.Context.GetTcsArquivoLogParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("GetTcsArquivoGrupoVinculoParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsArquivoGrupoVinculoParentComposition> GetTcsArquivoGrupoVinculoParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsArquivoGrupoVinculoParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsArquivoGrupoVinculoParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsArquivoGrupoVinculoParentCompositionToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsArquivoGrupoVinculoParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            jEntitySearch = jEntitySearch.Replace("TcsArquivoGrupoVinculo{", "TcsArquivoGrupoVinculoParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsArquivoGrupoVinculoParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsArquivoGrupoVinculoParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.LayoutArquivo.TcsArquivoGrupoVinculo");
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
        [Route("GetTcsArquivoGrupoVinculoParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsArquivoGrupoVinculoParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsArquivoGrupoVinculoParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsArquivoGrupoVinculoParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.LayoutArquivo.TcsArquivoGrupoVinculo", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.LayoutArquivoDataSource", DataSourceObject = "GetTcsArquivoGrupoVinculoParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsArquivoGrupoVinculoParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsArquivoGrupoVinculoParentComposition> GetSampleTcsArquivoGrupoVinculoParentComposition(string details)
        {
            var result = repository.Context.GetTcsArquivoGrupoVinculoParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
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
    
    public partial class LinxFrameworkLayoutArquivoFeedController : ODataController
    {
        private BusinessNS.LayoutArquivoDomainService _context;
        public BusinessNS.LayoutArquivoDomainService Context { get {  if (_context == null) { _context = new BusinessNS.LayoutArquivoDomainService(); _context.IsSecure = true; } return _context; }  }
        
        #region Get Action to Business Entities
        
        
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
        public IQueryable<BusinessNS.TcsArquivoItem> GetTcsArquivo__TcsArquivoItem(Int32 key0, string navigation)
        {
            var entity = this.Context.GetTcsArquivoByKey(key0);
            if (entity != null && navigation == "TcsArquivoItemList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsArquivoItem" });
               return entity.TcsArquivoItemList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsArquivoItem>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsArquivoLog> GetTcsArquivo__TcsArquivoLog(Int32 key0, string navigation)
        {
            var entity = this.Context.GetTcsArquivoByKey(key0);
            if (entity != null && navigation == "TcsArquivoLogList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsArquivoLog" });
               return entity.TcsArquivoLogList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsArquivoLog>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsArquivoGrupoVinculo> GetTcsArquivo__TcsArquivoGrupoVinculo(Int32 key0, string navigation)
        {
            var entity = this.Context.GetTcsArquivoByKey(key0);
            if (entity != null && navigation == "TcsArquivoGrupoVinculoList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsArquivoGrupoVinculo" });
               return entity.TcsArquivoGrupoVinculoList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsArquivoGrupoVinculo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsArquivoItem> GetTcsArquivoItem(Int32 key0)
        {
            var entity = this.Context.GetTcsArquivoItemByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsArquivoItem[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsArquivoItem>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsArquivoItem> GetTcsArquivoItem()
        {
            return this.Context.GetTcsArquivoItemByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsArquivoItemParentComposition> GetTcsArquivoItemParentComposition()
        {
            return this.Context.GetTcsArquivoItemParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsArquivo> GetTcsArquivoItem__TcsArquivo(Int32 key0, string navigation)
        {
            var entity = this.Context.GetTcsArquivoItemByKey(key0);
            if (entity != null && navigation == "TcsArquivo")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsArquivo[] { entity.TcsArquivo }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsArquivo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsArquivoItemCampo> GetTcsArquivoItem__TcsArquivoItemCampo(Int32 key0, string navigation)
        {
            var entity = this.Context.GetTcsArquivoItemByKey(key0);
            if (entity != null && navigation == "TcsArquivoItemCampoList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsArquivoItemCampo" });
               return entity.TcsArquivoItemCampoList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsArquivoItemCampo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsArquivoItemCampo> GetTcsArquivoItemCampo(Int32 key0)
        {
            var entity = this.Context.GetTcsArquivoItemCampoByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsArquivoItemCampo[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsArquivoItemCampo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsArquivoItemCampo> GetTcsArquivoItemCampo()
        {
            return this.Context.GetTcsArquivoItemCampoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsArquivoItemCampoParentComposition> GetTcsArquivoItemCampoParentComposition()
        {
            return this.Context.GetTcsArquivoItemCampoParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsArquivoItem> GetTcsArquivoItemCampo__TcsArquivoItem(Int32 key0, string navigation)
        {
            var entity = this.Context.GetTcsArquivoItemCampoByKey(key0);
            if (entity != null && navigation == "TcsArquivoItem")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsArquivoItem[] { entity.TcsArquivoItem }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsArquivoItem>);
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
        public IQueryable<BusinessNS.TcsArquivoLogParentComposition> GetTcsArquivoLogParentComposition()
        {
            return this.Context.GetTcsArquivoLogParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsArquivo> GetTcsArquivoLog__TcsArquivo(Int32 key0, string navigation)
        {
            var entity = this.Context.GetTcsArquivoLogByKey(key0);
            if (entity != null && navigation == "TcsArquivo")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsArquivo[] { entity.TcsArquivo }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsArquivo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsArquivoGrupoVinculo> GetTcsArquivoGrupoVinculo(Int32 key0, Int32 key1)
        {
            var entity = this.Context.GetTcsArquivoGrupoVinculoByKey(key0, key1);
            if (entity != null)
               return (new BusinessNS.TcsArquivoGrupoVinculo[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsArquivoGrupoVinculo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsArquivoGrupoVinculo> GetTcsArquivoGrupoVinculo()
        {
            return this.Context.GetTcsArquivoGrupoVinculoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsArquivoGrupoVinculoParentComposition> GetTcsArquivoGrupoVinculoParentComposition()
        {
            return this.Context.GetTcsArquivoGrupoVinculoParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsArquivo> GetTcsArquivoGrupoVinculo__TcsArquivo(Int32 key0, Int32 key1, string navigation)
        {
            var entity = this.Context.GetTcsArquivoGrupoVinculoByKey(key0, key1);
            if (entity != null && navigation == "TcsArquivo")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsArquivo[] { entity.TcsArquivo }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsArquivo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsArquivoGrupo> GetTcsArquivoGrupo(Int32 key0)
        {
            var entity = this.Context.GetTcsArquivoGrupoByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsArquivoGrupo[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsArquivoGrupo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsArquivoGrupo> GetTcsArquivoGrupo()
        {
            return this.Context.GetTcsArquivoGrupoByEntitySearchNoAssociations(null).AsQueryable();
        }
        #endregion
        
    }
    
    public partial class LinxFrameworkLayoutArquivoControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return true;
        }
    }
}

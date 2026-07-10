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
using BusinessNS = Linx.Framework.BV.Auditoria;

namespace Linx.Framework.BV.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/LinxFrameworkAuditoria/[ActionName]
    // Security Information Call: http://localhost:1710/LinxFrameworkAuditoria/GetSecurityInfo
    // Entities Catalog Call: http://localhost:1710/LinxFrameworkAuditoria/GetEntities
    // Entity MetaData Call: http://localhost:1710/LinxFrameworkAuditoria/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/LinxFrameworkAuditoria/GetClientDomains?erp=true
    // Client Service Call: http://localhost:1710/LinxFrameworkAuditoria/GetClientService?erp=true
    // Client Factory Call: http://localhost:1710/LinxFrameworkAuditoria/GetClientFactory?entityName=[EntityName]&erp=true
    // Client Factory Custom Events Call: http://localhost:1710/LinxFrameworkAuditoria/GetClientFactoryCustomEvents?entityName=[EntityName]&erp=true
    // Help Call: http://localhost:1710/HelpController/LinxFrameworkAuditoria
    // Feed OData Call: http://localhost:1710/LinxFrameworkAuditoriaOData
    [RoutePrefix("LinxFrameworkAuditoria")]
    [Breeze.WebApi2.BreezeController]
    [ODataBasicAuthenticationFilter]
    public partial class LinxFrameworkAuditoriaController : ApiController
    {
        private const int maxObjectExcelReturned = 2300000;
        private DataServiceRepository<BusinessNS.AuditoriaDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.AuditoriaDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.AuditoriaDomainService>(typeof(BusinessNS.AdtAuditoria), typeof(BusinessNS.AdtAuditoriaItem), typeof(BusinessNS.AdtAuditoriaItemDetalhe)); _repository.Context.IsSecure = true; } return _repository; } }
        public LinxFrameworkAuditoriaController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(LinxFrameworkAuditoriaController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.AuditoriaDomainService).Assembly.FullName,
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
            var result = repository.Context.GetMetaDataObject("Linx.Framework.BV.Auditoria." + entityName, false, true);
            return (allComposition ? result : result.Where(e => e.ClassName == entityName).ToList());
        }
        
        [Route("GetSecurityInfo"), System.Web.Http.HttpGet()]
        public string[] GetSecurityInfo()
        {
           return new string[] { "Linx.Framework.BV", "LinxFrameworkAuditoria", "LinxFrameworkAuditoria/ActionName" };
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
        
        [LinxFrameworkAuditoriaControllerAuthorize]
        [Route("GetAllLookUpTcsUsuario"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsUsuario> GetAllLookUpTcsUsuario()
        {
            return repository.Context.GetAllLookUpTcsUsuario();
        }
        
        [LinxFrameworkAuditoriaControllerAuthorize]
        [Route("GetLookUpTcsUsuarioByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsUsuario> GetLookUpTcsUsuarioByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsUsuarioByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
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
        
        [LinxFrameworkAuditoriaControllerAuthorize]
        [Route("GetAdtAuditoria"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AdtAuditoria> GetAdtAuditoria()
        {
            return repository.Context.GetAdtAuditoria();
        }
        
        [LinxFrameworkAuditoriaControllerAuthorize]
        [Route("GetAdtAuditoriaNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AdtAuditoria> GetAdtAuditoriaNoAssociations()
        {
            return repository.Context.GetAdtAuditoriaNoAssociations();
        }
        
        [LinxFrameworkAuditoriaControllerAuthorize]
        [Route("GetAdtAuditoriaByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AdtAuditoria> GetAdtAuditoriaByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetAdtAuditoriaByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AdtAuditoria), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [LinxFrameworkAuditoriaControllerAuthorize]
        [Route("GetAdtAuditoriaByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AdtAuditoria> GetAdtAuditoriaByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetAdtAuditoriaByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AdtAuditoria), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetAdtAuditoriaToExcel"), System.Web.Http.HttpPost()]
        [LinxFrameworkAuditoriaControllerAuthorize]
        public string GetAdtAuditoriaToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AdtAuditoria), jEntitySearch, false, false, false);
            var entities = repository.Context.GetAdtAuditoriaByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdAdtAuditoria asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Auditoria.AdtAuditoria");
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
               return ExcelExportPagination<BusinessNS.AdtAuditoria>.CreateExcelDocumentFileMapPath("AdtAuditoria",new ExcelExportPagination<BusinessNS.AdtAuditoria>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetAdtAuditoriaToReportXml"), System.Web.Http.HttpPost()]
        [LinxFrameworkAuditoriaControllerAuthorize]
        public string GetAdtAuditoriaToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AdtAuditoria), jEntitySearch, false, false, false);
            var entities = repository.Context.GetAdtAuditoriaByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Auditoria.AdtAuditoria", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.AuditoriaDataSource", DataSourceObject = "GetAdtAuditoria", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxFrameworkAuditoriaControllerAuthorize]
        [Route("GetSampleAdtAuditoria"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AdtAuditoria> GetSampleAdtAuditoria(string details)
        {
            var result = repository.Context.GetAdtAuditoriaByEntitySearchNoAssociations(null).Take(100).ToList();
               if (!details.IsNullOrEmpty())
               {
                   foreach(var entity in result)
                   {
                       entity.FillDetails(repository.Context, null, null, details.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries), 5);
                   }
               }
            return result.AsQueryable();
        }
        
        [Route("AddAdtAuditoriaEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddAdtAuditoriaEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AdtAuditoria), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [LinxFrameworkAuditoriaControllerAuthorize]
        [Route("GetAdtAuditoriaByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AdtAuditoria> GetAdtAuditoriaByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetAdtAuditoriaByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [LinxFrameworkAuditoriaControllerAuthorize]
        [Route("GetAdtAuditoriaItem"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AdtAuditoriaItem> GetAdtAuditoriaItem()
        {
            return repository.Context.GetAdtAuditoriaItem();
        }
        
        [LinxFrameworkAuditoriaControllerAuthorize]
        [Route("GetAdtAuditoriaItemNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AdtAuditoriaItem> GetAdtAuditoriaItemNoAssociations()
        {
            return repository.Context.GetAdtAuditoriaItemNoAssociations();
        }
        
        [LinxFrameworkAuditoriaControllerAuthorize]
        [Route("GetAdtAuditoriaItemByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AdtAuditoriaItem> GetAdtAuditoriaItemByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetAdtAuditoriaItemByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AdtAuditoriaItem), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [LinxFrameworkAuditoriaControllerAuthorize]
        [Route("GetAdtAuditoriaItemByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AdtAuditoriaItem> GetAdtAuditoriaItemByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetAdtAuditoriaItemByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AdtAuditoriaItem), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetAdtAuditoriaItemToExcel"), System.Web.Http.HttpPost()]
        [LinxFrameworkAuditoriaControllerAuthorize]
        public string GetAdtAuditoriaItemToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AdtAuditoriaItem), jEntitySearch, false, false, false);
            var entities = repository.Context.GetAdtAuditoriaItemByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdAdtAuditoriaItem asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Auditoria.AdtAuditoriaItem");
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
               return ExcelExportPagination<BusinessNS.AdtAuditoriaItem>.CreateExcelDocumentFileMapPath("AdtAuditoriaItem",new ExcelExportPagination<BusinessNS.AdtAuditoriaItem>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetAdtAuditoriaItemToReportXml"), System.Web.Http.HttpPost()]
        [LinxFrameworkAuditoriaControllerAuthorize]
        public string GetAdtAuditoriaItemToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AdtAuditoriaItem), jEntitySearch, false, false, false);
            var entities = repository.Context.GetAdtAuditoriaItemByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Auditoria.AdtAuditoriaItem", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.AuditoriaDataSource", DataSourceObject = "GetAdtAuditoriaItem", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxFrameworkAuditoriaControllerAuthorize]
        [Route("GetSampleAdtAuditoriaItem"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AdtAuditoriaItem> GetSampleAdtAuditoriaItem(string details)
        {
            var result = repository.Context.GetAdtAuditoriaItemByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddAdtAuditoriaItemEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddAdtAuditoriaItemEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AdtAuditoriaItem), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [LinxFrameworkAuditoriaControllerAuthorize]
        [Route("GetAdtAuditoriaItemByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AdtAuditoriaItem> GetAdtAuditoriaItemByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetAdtAuditoriaItemByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [LinxFrameworkAuditoriaControllerAuthorize]
        [Route("GetAdtAuditoriaItemDetalhe"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AdtAuditoriaItemDetalhe> GetAdtAuditoriaItemDetalhe()
        {
            return repository.Context.GetAdtAuditoriaItemDetalhe();
        }
        
        [LinxFrameworkAuditoriaControllerAuthorize]
        [Route("GetAdtAuditoriaItemDetalheNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AdtAuditoriaItemDetalhe> GetAdtAuditoriaItemDetalheNoAssociations()
        {
            return repository.Context.GetAdtAuditoriaItemDetalheNoAssociations();
        }
        
        [LinxFrameworkAuditoriaControllerAuthorize]
        [Route("GetAdtAuditoriaItemDetalheByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AdtAuditoriaItemDetalhe> GetAdtAuditoriaItemDetalheByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetAdtAuditoriaItemDetalheByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AdtAuditoriaItemDetalhe), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [LinxFrameworkAuditoriaControllerAuthorize]
        [Route("GetAdtAuditoriaItemDetalheByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AdtAuditoriaItemDetalhe> GetAdtAuditoriaItemDetalheByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetAdtAuditoriaItemDetalheByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AdtAuditoriaItemDetalhe), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetAdtAuditoriaItemDetalheToExcel"), System.Web.Http.HttpPost()]
        [LinxFrameworkAuditoriaControllerAuthorize]
        public string GetAdtAuditoriaItemDetalheToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AdtAuditoriaItemDetalhe), jEntitySearch, false, false, false);
            var entities = repository.Context.GetAdtAuditoriaItemDetalheByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdAdtAuditoriaItemDetalhe asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Auditoria.AdtAuditoriaItemDetalhe");
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
               return ExcelExportPagination<BusinessNS.AdtAuditoriaItemDetalhe>.CreateExcelDocumentFileMapPath("AdtAuditoriaItemDetalhe",new ExcelExportPagination<BusinessNS.AdtAuditoriaItemDetalhe>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetAdtAuditoriaItemDetalheToReportXml"), System.Web.Http.HttpPost()]
        [LinxFrameworkAuditoriaControllerAuthorize]
        public string GetAdtAuditoriaItemDetalheToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AdtAuditoriaItemDetalhe), jEntitySearch, false, false, false);
            var entities = repository.Context.GetAdtAuditoriaItemDetalheByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Auditoria.AdtAuditoriaItemDetalhe", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.AuditoriaDataSource", DataSourceObject = "GetAdtAuditoriaItemDetalhe", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxFrameworkAuditoriaControllerAuthorize]
        [Route("GetSampleAdtAuditoriaItemDetalhe"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AdtAuditoriaItemDetalhe> GetSampleAdtAuditoriaItemDetalhe(string details)
        {
            var result = repository.Context.GetAdtAuditoriaItemDetalheByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddAdtAuditoriaItemDetalheEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddAdtAuditoriaItemDetalheEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AdtAuditoriaItemDetalhe), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [LinxFrameworkAuditoriaControllerAuthorize]
        [Route("GetAdtAuditoriaItemDetalheByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AdtAuditoriaItemDetalhe> GetAdtAuditoriaItemDetalheByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetAdtAuditoriaItemDetalheByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        #endregion
        
        #region Get Business Entities By Parent Composition
        
        [LinxFrameworkAuditoriaControllerAuthorize]
        [Route("GetAdtAuditoriaItemParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AdtAuditoriaItemParentComposition> GetAdtAuditoriaItemParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetAdtAuditoriaItemParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AdtAuditoriaItemParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetAdtAuditoriaItemParentCompositionToExcel"), System.Web.Http.HttpPost()]
        [LinxFrameworkAuditoriaControllerAuthorize]
        public string GetAdtAuditoriaItemParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("AdtAuditoriaItem{", "AdtAuditoriaItemParentComposition{");
            jEntitySearch = jEntitySearch.Replace("AdtAuditoria{", "AdtAuditoriaItemParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AdtAuditoriaItemParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetAdtAuditoriaItemParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdAdtAuditoriaItem asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Auditoria.AdtAuditoriaItem");
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
               return ExcelExportPagination<BusinessNS.AdtAuditoriaItemParentComposition>.CreateExcelDocumentFileMapPath("AdtAuditoriaItem",new ExcelExportPagination<BusinessNS.AdtAuditoriaItemParentComposition>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetAdtAuditoriaItemParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        [LinxFrameworkAuditoriaControllerAuthorize]
        public string GetAdtAuditoriaItemParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AdtAuditoriaItemParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetAdtAuditoriaItemParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Auditoria.AdtAuditoriaItem", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.AuditoriaDataSource", DataSourceObject = "GetAdtAuditoriaItemParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxFrameworkAuditoriaControllerAuthorize]
        [Route("GetSampleAdtAuditoriaItemParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AdtAuditoriaItemParentComposition> GetSampleAdtAuditoriaItemParentComposition(string details)
        {
            var result = repository.Context.GetAdtAuditoriaItemParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        #endregion
        
        #region Save Changes
        [LinxFrameworkAuditoriaControllerAuthorize]
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
    public partial class LinxFrameworkAuditoriaFeedController : ODataController
    {
        private BusinessNS.AuditoriaDomainService _context;
        public BusinessNS.AuditoriaDomainService Context { get {  if (_context == null) { _context = new BusinessNS.AuditoriaDomainService(); _context.IsSecure = true; } return _context; }  }
        
        #region Get Action to Business Entities
        
        
        [LinxFrameworkAuditoriaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.AdtAuditoria> GetAdtAuditoriaById([FromODataUri]long key0)
        {
            var entity = this.Context.GetAdtAuditoriaByKey(key0);
            if (entity != null)
               return (new BusinessNS.AdtAuditoria[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.AdtAuditoria>);
        }
        
        [LinxFrameworkAuditoriaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.AdtAuditoria> GetAdtAuditoriaByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetAdtAuditoriaByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AdtAuditoria), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.AdtAuditoria>);
        }
        
        [LinxFrameworkAuditoriaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.AdtAuditoria> GetAdtAuditoria()
        {
            return this.Context.GetAdtAuditoriaByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxFrameworkAuditoriaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.AdtAuditoriaItem> GetAdtAuditoria__AdtAuditoriaItem(long key0, string navigation)
        {
            var entity = this.Context.GetAdtAuditoriaByKey(key0);
            if (entity != null && navigation == "AdtAuditoriaItemList")
            {
               entity.FillDetails(_context, null, null, new string[] { "AdtAuditoriaItem" });
               return entity.AdtAuditoriaItemList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.AdtAuditoriaItem>);
        }
        
        [LinxFrameworkAuditoriaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.AdtAuditoriaItem> GetAdtAuditoriaItemById([FromODataUri]long key0)
        {
            var entity = this.Context.GetAdtAuditoriaItemByKey(key0);
            if (entity != null)
               return (new BusinessNS.AdtAuditoriaItem[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.AdtAuditoriaItem>);
        }
        
        [LinxFrameworkAuditoriaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.AdtAuditoriaItem> GetAdtAuditoriaItemByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetAdtAuditoriaItemByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AdtAuditoriaItem), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.AdtAuditoriaItem>);
        }
        
        [LinxFrameworkAuditoriaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.AdtAuditoriaItem> GetAdtAuditoriaItem()
        {
            return this.Context.GetAdtAuditoriaItemByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxFrameworkAuditoriaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.AdtAuditoriaItemParentComposition> GetAdtAuditoriaItemParentComposition()
        {
            return this.Context.GetAdtAuditoriaItemParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxFrameworkAuditoriaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.AdtAuditoriaItemParentComposition> GetAdtAuditoriaItemParentCompositionByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                jEntitySearch = jEntitySearch.Replace("AdtAuditoriaItem{", "AdtAuditoriaItemParentComposition{");
                jEntitySearch = jEntitySearch.Replace("AdtAuditoria{", "AdtAuditoriaItemParentComposition{");
                var entity = this.Context.GetAdtAuditoriaItemParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AdtAuditoriaItemParentComposition), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.AdtAuditoriaItemParentComposition>);
        }
        
        [LinxFrameworkAuditoriaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.AdtAuditoria> GetAdtAuditoriaItem__AdtAuditoria(long key0, string navigation)
        {
            var entity = this.Context.GetAdtAuditoriaItemByKey(key0);
            if (entity != null && navigation == "AdtAuditoria")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.AdtAuditoria[] { entity.AdtAuditoria }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.AdtAuditoria>);
        }
        
        [LinxFrameworkAuditoriaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.AdtAuditoriaItemDetalhe> GetAdtAuditoriaItemDetalheById([FromODataUri]long key0)
        {
            var entity = this.Context.GetAdtAuditoriaItemDetalheByKey(key0);
            if (entity != null)
               return (new BusinessNS.AdtAuditoriaItemDetalhe[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.AdtAuditoriaItemDetalhe>);
        }
        
        [LinxFrameworkAuditoriaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.AdtAuditoriaItemDetalhe> GetAdtAuditoriaItemDetalheByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetAdtAuditoriaItemDetalheByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AdtAuditoriaItemDetalhe), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.AdtAuditoriaItemDetalhe>);
        }
        
        [LinxFrameworkAuditoriaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.AdtAuditoriaItemDetalhe> GetAdtAuditoriaItemDetalhe()
        {
            return this.Context.GetAdtAuditoriaItemDetalheByEntitySearchNoAssociations(null).AsQueryable();
        }
        #endregion
        
    }
    
    public partial class LinxFrameworkAuditoriaControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return LinxAutorization.CheckAuthorization(actionContext, string.Format("{0}#{1}#{1}/{2}", "Linx.Framework.BV", "LinxFrameworkAuditoria", actionContext.ActionDescriptor.ActionName));
        }
    }
}

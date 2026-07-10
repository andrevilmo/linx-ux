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
using BusinessNS = Linx.Framework.BV.Multimidia;

namespace Linx.Framework.BV.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/LinxFrameworkMultimidia/[ActionName]
    // Security Information Call: http://localhost:1710/LinxFrameworkMultimidia/GetSecurityInfo
    // Entities Catalog Call: http://localhost:1710/LinxFrameworkMultimidia/GetEntities
    // Entity MetaData Call: http://localhost:1710/LinxFrameworkMultimidia/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/LinxFrameworkMultimidia/GetClientDomains?erp=true
    // Client Service Call: http://localhost:1710/LinxFrameworkMultimidia/GetClientService?erp=true
    // Client Factory Call: http://localhost:1710/LinxFrameworkMultimidia/GetClientFactory?entityName=[EntityName]&erp=true
    // Client Factory Custom Events Call: http://localhost:1710/LinxFrameworkMultimidia/GetClientFactoryCustomEvents?entityName=[EntityName]&erp=true
    // Help Call: http://localhost:1710/HelpController/LinxFrameworkMultimidia
    // Feed OData Call: http://localhost:1710/LinxFrameworkMultimidiaOData
    [RoutePrefix("LinxFrameworkMultimidia")]
    [Breeze.WebApi2.BreezeController]
    public partial class LinxFrameworkMultimidiaController : ApiController
    {
        private const int maxObjectExcelReturned = 2300000;
        private DataServiceRepository<BusinessNS.MultimidiaDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.MultimidiaDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.MultimidiaDomainService>(typeof(BusinessNS.DocMultimidia), typeof(BusinessNS.DocMultimidiaCompact), typeof(BusinessNS.DocMultimidiaConfig), typeof(BusinessNS.DocMultimidiaInfo), typeof(BusinessNS.DocMultimidiaTabela), typeof(BusinessNS.DocMultimidiaTabelaChild), typeof(BusinessNS.DocMultimidiaUid), typeof(BusinessNS.DocMultimidiaUpload), typeof(BusinessNS.DocTabelaSync), typeof(BusinessNS.MediaConfigLength), typeof(BusinessNS.MediaElement), typeof(BusinessNS.MultimidiaCompact2BO)); _repository.Context.IsSecure = true; } return _repository; } }
        public LinxFrameworkMultimidiaController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(LinxFrameworkMultimidiaController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.MultimidiaDomainService).Assembly.FullName,
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
            var result = repository.Context.GetMetaDataObject("Linx.Framework.BV.Multimidia." + entityName, false, true);
            return (allComposition ? result : result.Where(e => e.ClassName == entityName).ToList());
        }
        
        [Route("GetSecurityInfo"), System.Web.Http.HttpGet()]
        public string[] GetSecurityInfo()
        {
           return new string[] { "Linx.Framework.BV", "LinxFrameworkMultimidia", "LinxFrameworkMultimidia/ActionName" };
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
        
        [Route("GetAllLookUpDocMultimidia"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpDocMultimidia> GetAllLookUpDocMultimidia()
        {
            return repository.Context.GetAllLookUpDocMultimidia();
        }
        
        [Route("GetLookUpDocMultimidiaByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpDocMultimidia> GetLookUpDocMultimidiaByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpDocMultimidiaByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [Route("GetAllLookUpDocMultimidiaCompact2"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpDocMultimidiaCompact2> GetAllLookUpDocMultimidiaCompact2()
        {
            return repository.Context.GetAllLookUpDocMultimidiaCompact2();
        }
        
        [Route("GetLookUpDocMultimidiaCompact2ByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpDocMultimidiaCompact2> GetLookUpDocMultimidiaCompact2ByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpDocMultimidiaCompact2ByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [Route("GetAllLookUpDocMultimidiaCompact"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpDocMultimidiaCompact> GetAllLookUpDocMultimidiaCompact()
        {
            return repository.Context.GetAllLookUpDocMultimidiaCompact();
        }
        
        [Route("GetLookUpDocMultimidiaCompactByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpDocMultimidiaCompact> GetLookUpDocMultimidiaCompactByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpDocMultimidiaCompactByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [Route("GetAllLookUpDocClassificador"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpDocClassificador> GetAllLookUpDocClassificador()
        {
            return repository.Context.GetAllLookUpDocClassificador();
        }
        
        [Route("GetLookUpDocClassificadorByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpDocClassificador> GetLookUpDocClassificadorByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpDocClassificadorByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [Route("GetAllLookUpDocClassificador1"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpDocClassificador1> GetAllLookUpDocClassificador1()
        {
            return repository.Context.GetAllLookUpDocClassificador1();
        }
        
        [Route("GetLookUpDocClassificador1ByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpDocClassificador1> GetLookUpDocClassificador1ByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpDocClassificador1ByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [Route("GetAllLookUpTcsAplicativo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsAplicativo> GetAllLookUpTcsAplicativo()
        {
            return repository.Context.GetAllLookUpTcsAplicativo();
        }
        
        [Route("GetLookUpTcsAplicativoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsAplicativo> GetLookUpTcsAplicativoByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsAplicativoByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
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
        
        [Route("GetDocMultimidiaTabela"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaTabela> GetDocMultimidiaTabela()
        {
            return repository.Context.GetDocMultimidiaTabela();
        }
        
        [Route("GetDocMultimidiaTabelaNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaTabela> GetDocMultimidiaTabelaNoAssociations()
        {
            return repository.Context.GetDocMultimidiaTabelaNoAssociations();
        }
        
        [Route("GetDocMultimidiaTabelaByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaTabela> GetDocMultimidiaTabelaByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetDocMultimidiaTabelaByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaTabela), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetDocMultimidiaTabelaByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaTabela> GetDocMultimidiaTabelaByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetDocMultimidiaTabelaByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaTabela), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetDocMultimidiaTabelaToExcel"), System.Web.Http.HttpPost()]
        public string GetDocMultimidiaTabelaToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaTabela), jEntitySearch, false, false, false);
            var entities = repository.Context.GetDocMultimidiaTabelaByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdChave asc, UidChave asc, UidDocumento asc, UidTabela asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Multimidia.DocMultimidiaTabela");
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
               return ExcelExportPagination<BusinessNS.DocMultimidiaTabela>.CreateExcelDocumentFileMapPath("DocMultimidiaTabela",new ExcelExportPagination<BusinessNS.DocMultimidiaTabela>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetDocMultimidiaTabelaToReportXml"), System.Web.Http.HttpPost()]
        public string GetDocMultimidiaTabelaToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaTabela), jEntitySearch, false, false, false);
            var entities = repository.Context.GetDocMultimidiaTabelaByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Multimidia.DocMultimidiaTabela", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.MultimidiaDataSource", DataSourceObject = "GetDocMultimidiaTabela", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleDocMultimidiaTabela"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaTabela> GetSampleDocMultimidiaTabela(string details)
        {
            var result = repository.Context.GetDocMultimidiaTabelaByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddDocMultimidiaTabelaEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddDocMultimidiaTabelaEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaTabela), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetDocMultimidiaTabelaByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaTabela> GetDocMultimidiaTabelaByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetDocMultimidiaTabelaByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetDocMultimidiaCompact"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaCompact> GetDocMultimidiaCompact()
        {
            return repository.Context.GetDocMultimidiaCompact();
        }
        
        [Route("GetDocMultimidiaCompactNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaCompact> GetDocMultimidiaCompactNoAssociations()
        {
            return repository.Context.GetDocMultimidiaCompactNoAssociations();
        }
        
        [Route("GetDocMultimidiaCompactByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaCompact> GetDocMultimidiaCompactByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetDocMultimidiaCompactByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaCompact), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetDocMultimidiaCompactByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaCompact> GetDocMultimidiaCompactByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetDocMultimidiaCompactByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaCompact), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetDocMultimidiaCompactToExcel"), System.Web.Http.HttpPost()]
        public string GetDocMultimidiaCompactToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaCompact), jEntitySearch, false, false, false);
            var entities = repository.Context.GetDocMultimidiaCompactByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdChave asc, UidChave asc, UidDocumento asc, UidTabela asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Multimidia.DocMultimidiaCompact");
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
               return ExcelExportPagination<BusinessNS.DocMultimidiaCompact>.CreateExcelDocumentFileMapPath("DocMultimidiaCompact",new ExcelExportPagination<BusinessNS.DocMultimidiaCompact>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetDocMultimidiaCompactToReportXml"), System.Web.Http.HttpPost()]
        public string GetDocMultimidiaCompactToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaCompact), jEntitySearch, false, false, false);
            var entities = repository.Context.GetDocMultimidiaCompactByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Multimidia.DocMultimidiaCompact", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.MultimidiaDataSource", DataSourceObject = "GetDocMultimidiaCompact", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleDocMultimidiaCompact"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaCompact> GetSampleDocMultimidiaCompact(string details)
        {
            var result = repository.Context.GetDocMultimidiaCompactByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddDocMultimidiaCompactEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddDocMultimidiaCompactEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaCompact), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetDocMultimidiaCompactByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaCompact> GetDocMultimidiaCompactByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetDocMultimidiaCompactByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetMultimidiaCompact2BO"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.MultimidiaCompact2BO> GetMultimidiaCompact2BO()
        {
            return repository.Context.GetMultimidiaCompact2BO();
        }
        
        [Route("GetMultimidiaCompact2BONoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.MultimidiaCompact2BO> GetMultimidiaCompact2BONoAssociations()
        {
            return repository.Context.GetMultimidiaCompact2BONoAssociations();
        }
        
        [Route("GetMultimidiaCompact2BOByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.MultimidiaCompact2BO> GetMultimidiaCompact2BOByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetMultimidiaCompact2BOByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.MultimidiaCompact2BO), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetMultimidiaCompact2BOByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.MultimidiaCompact2BO> GetMultimidiaCompact2BOByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetMultimidiaCompact2BOByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.MultimidiaCompact2BO), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetMultimidiaCompact2BOToExcel"), System.Web.Http.HttpPost()]
        public string GetMultimidiaCompact2BOToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.MultimidiaCompact2BO), jEntitySearch, false, false, false);
            var entities = repository.Context.GetMultimidiaCompact2BOByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdChave asc, UidChave asc, UidDocumento asc, UidTabela asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Multimidia.MultimidiaCompact2BO");
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
               return ExcelExportPagination<BusinessNS.MultimidiaCompact2BO>.CreateExcelDocumentFileMapPath("MultimidiaCompact2BO",new ExcelExportPagination<BusinessNS.MultimidiaCompact2BO>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetMultimidiaCompact2BOToReportXml"), System.Web.Http.HttpPost()]
        public string GetMultimidiaCompact2BOToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.MultimidiaCompact2BO), jEntitySearch, false, false, false);
            var entities = repository.Context.GetMultimidiaCompact2BOByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Multimidia.MultimidiaCompact2BO", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.MultimidiaDataSource", DataSourceObject = "GetMultimidiaCompact2BO", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleMultimidiaCompact2BO"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.MultimidiaCompact2BO> GetSampleMultimidiaCompact2BO(string details)
        {
            var result = repository.Context.GetMultimidiaCompact2BOByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddMultimidiaCompact2BOEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddMultimidiaCompact2BOEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.MultimidiaCompact2BO), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetMultimidiaCompact2BOByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.MultimidiaCompact2BO> GetMultimidiaCompact2BOByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetMultimidiaCompact2BOByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetDocMultimidiaUid"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaUid> GetDocMultimidiaUid()
        {
            return repository.Context.GetDocMultimidiaUid();
        }
        
        [Route("GetDocMultimidiaUidNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaUid> GetDocMultimidiaUidNoAssociations()
        {
            return repository.Context.GetDocMultimidiaUidNoAssociations();
        }
        
        [Route("GetDocMultimidiaUidByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaUid> GetDocMultimidiaUidByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetDocMultimidiaUidByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaUid), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetDocMultimidiaUidByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaUid> GetDocMultimidiaUidByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetDocMultimidiaUidByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaUid), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetDocMultimidiaUidToExcel"), System.Web.Http.HttpPost()]
        public string GetDocMultimidiaUidToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaUid), jEntitySearch, false, false, false);
            var entities = repository.Context.GetDocMultimidiaUidByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("UidDocumento asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Multimidia.DocMultimidiaUid");
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
               return ExcelExportPagination<BusinessNS.DocMultimidiaUid>.CreateExcelDocumentFileMapPath("DocMultimidiaUid",new ExcelExportPagination<BusinessNS.DocMultimidiaUid>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetDocMultimidiaUidToReportXml"), System.Web.Http.HttpPost()]
        public string GetDocMultimidiaUidToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaUid), jEntitySearch, false, false, false);
            var entities = repository.Context.GetDocMultimidiaUidByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Multimidia.DocMultimidiaUid", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.MultimidiaDataSource", DataSourceObject = "GetDocMultimidiaUid", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleDocMultimidiaUid"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaUid> GetSampleDocMultimidiaUid(string details)
        {
            var result = repository.Context.GetDocMultimidiaUidByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddDocMultimidiaUidEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddDocMultimidiaUidEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaUid), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetDocMultimidiaUidByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaUid> GetDocMultimidiaUidByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetDocMultimidiaUidByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetDocMultimidiaInfo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaInfo> GetDocMultimidiaInfo()
        {
            return repository.Context.GetDocMultimidiaInfo().AsQueryable();
        }
        
        [Route("GetDocMultimidiaInfoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaInfo> GetDocMultimidiaInfoNoAssociations()
        {
            return repository.Context.GetDocMultimidiaInfoNoAssociations().AsQueryable();
        }
        
        [Route("GetDocMultimidiaInfoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaInfo> GetDocMultimidiaInfoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetDocMultimidiaInfoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaInfo), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        
        [Route("GetDocMultimidiaInfoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaInfo> GetDocMultimidiaInfoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetDocMultimidiaInfoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaInfo), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        [Route("GetDocMultimidiaInfoToExcel"), System.Web.Http.HttpPost()]
        public string GetDocMultimidiaInfoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaInfo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetDocMultimidiaInfoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).AsQueryable().OrderBy("UidDocumento asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Multimidia.DocMultimidiaInfo");
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
               return ExcelExportPagination<BusinessNS.DocMultimidiaInfo>.CreateExcelDocumentFileMapPath("DocMultimidiaInfo",new ExcelExportPagination<BusinessNS.DocMultimidiaInfo>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetDocMultimidiaInfoToReportXml"), System.Web.Http.HttpPost()]
        public string GetDocMultimidiaInfoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaInfo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetDocMultimidiaInfoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Multimidia.DocMultimidiaInfo", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.MultimidiaDataSource", DataSourceObject = "GetDocMultimidiaInfo", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleDocMultimidiaInfo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaInfo> GetSampleDocMultimidiaInfo(string details)
        {
            var result = repository.Context.GetDocMultimidiaInfoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddDocMultimidiaInfoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddDocMultimidiaInfoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaInfo), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetDocMultimidiaInfoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaInfo> GetDocMultimidiaInfoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetDocMultimidiaInfoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetDocMultimidia"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidia> GetDocMultimidia()
        {
            return repository.Context.GetDocMultimidia();
        }
        
        [Route("GetDocMultimidiaNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidia> GetDocMultimidiaNoAssociations()
        {
            return repository.Context.GetDocMultimidiaNoAssociations();
        }
        
        [Route("GetDocMultimidiaByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidia> GetDocMultimidiaByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetDocMultimidiaByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidia), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetDocMultimidiaByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidia> GetDocMultimidiaByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetDocMultimidiaByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidia), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetDocMultimidiaToExcel"), System.Web.Http.HttpPost()]
        public string GetDocMultimidiaToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidia), jEntitySearch, false, false, false);
            var entities = repository.Context.GetDocMultimidiaByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("UidDocumento asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Multimidia.DocMultimidia");
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
               return ExcelExportPagination<BusinessNS.DocMultimidia>.CreateExcelDocumentFileMapPath("DocMultimidia",new ExcelExportPagination<BusinessNS.DocMultimidia>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetDocMultimidiaToReportXml"), System.Web.Http.HttpPost()]
        public string GetDocMultimidiaToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidia), jEntitySearch, false, false, false);
            var entities = repository.Context.GetDocMultimidiaByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Multimidia.DocMultimidia", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.MultimidiaDataSource", DataSourceObject = "GetDocMultimidia", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleDocMultimidia"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidia> GetSampleDocMultimidia(string details)
        {
            var result = repository.Context.GetDocMultimidiaByEntitySearchNoAssociations(null).Take(100).ToList();
               if (!details.IsNullOrEmpty())
               {
                   foreach(var entity in result)
                   {
                       entity.FillDetails(repository.Context, null, null, details.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries), 5);
                   }
               }
            return result.AsQueryable();
        }
        
        [Route("AddDocMultimidiaEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddDocMultimidiaEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidia), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetDocMultimidiaByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidia> GetDocMultimidiaByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetDocMultimidiaByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetDocMultimidiaTabelaChild"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaTabelaChild> GetDocMultimidiaTabelaChild()
        {
            return repository.Context.GetDocMultimidiaTabelaChild();
        }
        
        [Route("GetDocMultimidiaTabelaChildNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaTabelaChild> GetDocMultimidiaTabelaChildNoAssociations()
        {
            return repository.Context.GetDocMultimidiaTabelaChildNoAssociations();
        }
        
        [Route("GetDocMultimidiaTabelaChildByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaTabelaChild> GetDocMultimidiaTabelaChildByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetDocMultimidiaTabelaChildByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaTabelaChild), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetDocMultimidiaTabelaChildByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaTabelaChild> GetDocMultimidiaTabelaChildByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetDocMultimidiaTabelaChildByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaTabelaChild), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetDocMultimidiaTabelaChildToExcel"), System.Web.Http.HttpPost()]
        public string GetDocMultimidiaTabelaChildToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaTabelaChild), jEntitySearch, false, false, false);
            var entities = repository.Context.GetDocMultimidiaTabelaChildByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdChave asc, UidChave asc, UidDocumento asc, UidTabela asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Multimidia.DocMultimidiaTabelaChild");
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
               return ExcelExportPagination<BusinessNS.DocMultimidiaTabelaChild>.CreateExcelDocumentFileMapPath("DocMultimidiaTabelaChild",new ExcelExportPagination<BusinessNS.DocMultimidiaTabelaChild>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetDocMultimidiaTabelaChildToReportXml"), System.Web.Http.HttpPost()]
        public string GetDocMultimidiaTabelaChildToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaTabelaChild), jEntitySearch, false, false, false);
            var entities = repository.Context.GetDocMultimidiaTabelaChildByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Multimidia.DocMultimidiaTabelaChild", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.MultimidiaDataSource", DataSourceObject = "GetDocMultimidiaTabelaChild", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleDocMultimidiaTabelaChild"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaTabelaChild> GetSampleDocMultimidiaTabelaChild(string details)
        {
            var result = repository.Context.GetDocMultimidiaTabelaChildByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddDocMultimidiaTabelaChildEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddDocMultimidiaTabelaChildEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaTabelaChild), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetDocMultimidiaTabelaChildByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaTabelaChild> GetDocMultimidiaTabelaChildByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetDocMultimidiaTabelaChildByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetDocMultimidiaConfig"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaConfig> GetDocMultimidiaConfig()
        {
            return repository.Context.GetDocMultimidiaConfig();
        }
        
        [Route("GetDocMultimidiaConfigNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaConfig> GetDocMultimidiaConfigNoAssociations()
        {
            return repository.Context.GetDocMultimidiaConfigNoAssociations();
        }
        
        [Route("GetDocMultimidiaConfigByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaConfig> GetDocMultimidiaConfigByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetDocMultimidiaConfigByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaConfig), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetDocMultimidiaConfigByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaConfig> GetDocMultimidiaConfigByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetDocMultimidiaConfigByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaConfig), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetDocMultimidiaConfigToExcel"), System.Web.Http.HttpPost()]
        public string GetDocMultimidiaConfigToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaConfig), jEntitySearch, false, false, false);
            var entities = repository.Context.GetDocMultimidiaConfigByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsAplicativo asc, LxUsoMultimidia asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Multimidia.DocMultimidiaConfig");
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
               return ExcelExportPagination<BusinessNS.DocMultimidiaConfig>.CreateExcelDocumentFileMapPath("DocMultimidiaConfig",new ExcelExportPagination<BusinessNS.DocMultimidiaConfig>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetDocMultimidiaConfigToReportXml"), System.Web.Http.HttpPost()]
        public string GetDocMultimidiaConfigToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaConfig), jEntitySearch, false, false, false);
            var entities = repository.Context.GetDocMultimidiaConfigByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Multimidia.DocMultimidiaConfig", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.MultimidiaDataSource", DataSourceObject = "GetDocMultimidiaConfig", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleDocMultimidiaConfig"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaConfig> GetSampleDocMultimidiaConfig(string details)
        {
            var result = repository.Context.GetDocMultimidiaConfigByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddDocMultimidiaConfigEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddDocMultimidiaConfigEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaConfig), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetDocMultimidiaConfigByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaConfig> GetDocMultimidiaConfigByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetDocMultimidiaConfigByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetMediaElement"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.MediaElement> GetMediaElement()
        {
            return repository.Context.GetMediaElement().AsQueryable();
        }
        
        [Route("GetMediaElementNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.MediaElement> GetMediaElementNoAssociations()
        {
            return repository.Context.GetMediaElementNoAssociations().AsQueryable();
        }
        
        [Route("GetMediaElementByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.MediaElement> GetMediaElementByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetMediaElementByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.MediaElement), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        
        [Route("GetMediaElementByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.MediaElement> GetMediaElementByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetMediaElementByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.MediaElement), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        [Route("GetMediaElementToExcel"), System.Web.Http.HttpPost()]
        public string GetMediaElementToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.MediaElement), jEntitySearch, false, false, false);
            var entities = repository.Context.GetMediaElementByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).AsQueryable().OrderBy("Id asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Multimidia.MediaElement");
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
               return ExcelExportPagination<BusinessNS.MediaElement>.CreateExcelDocumentFileMapPath("MediaElement",new ExcelExportPagination<BusinessNS.MediaElement>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetMediaElementToReportXml"), System.Web.Http.HttpPost()]
        public string GetMediaElementToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.MediaElement), jEntitySearch, false, false, false);
            var entities = repository.Context.GetMediaElementByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Multimidia.MediaElement", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.MultimidiaDataSource", DataSourceObject = "GetMediaElement", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleMediaElement"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.MediaElement> GetSampleMediaElement(string details)
        {
            var result = repository.Context.GetMediaElementByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddMediaElementEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddMediaElementEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.MediaElement), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetMediaElementByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.MediaElement> GetMediaElementByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetMediaElementByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetMediaConfigLength"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.MediaConfigLength> GetMediaConfigLength()
        {
            return repository.Context.GetMediaConfigLength().AsQueryable();
        }
        
        [Route("GetMediaConfigLengthNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.MediaConfigLength> GetMediaConfigLengthNoAssociations()
        {
            return repository.Context.GetMediaConfigLengthNoAssociations().AsQueryable();
        }
        
        [Route("GetMediaConfigLengthByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.MediaConfigLength> GetMediaConfigLengthByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetMediaConfigLengthByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.MediaConfigLength), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        
        [Route("GetMediaConfigLengthByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.MediaConfigLength> GetMediaConfigLengthByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetMediaConfigLengthByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.MediaConfigLength), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        [Route("GetMediaConfigLengthToExcel"), System.Web.Http.HttpPost()]
        public string GetMediaConfigLengthToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.MediaConfigLength), jEntitySearch, false, false, false);
            var entities = repository.Context.GetMediaConfigLengthByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).AsQueryable().OrderBy("IdApp asc, IdUse asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Multimidia.MediaConfigLength");
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
               return ExcelExportPagination<BusinessNS.MediaConfigLength>.CreateExcelDocumentFileMapPath("MediaConfigLength",new ExcelExportPagination<BusinessNS.MediaConfigLength>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetMediaConfigLengthToReportXml"), System.Web.Http.HttpPost()]
        public string GetMediaConfigLengthToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.MediaConfigLength), jEntitySearch, false, false, false);
            var entities = repository.Context.GetMediaConfigLengthByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Multimidia.MediaConfigLength", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.MultimidiaDataSource", DataSourceObject = "GetMediaConfigLength", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleMediaConfigLength"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.MediaConfigLength> GetSampleMediaConfigLength(string details)
        {
            var result = repository.Context.GetMediaConfigLengthByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddMediaConfigLengthEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddMediaConfigLengthEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.MediaConfigLength), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetMediaConfigLengthByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.MediaConfigLength> GetMediaConfigLengthByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetMediaConfigLengthByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetDocMultimidiaUpload"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaUpload> GetDocMultimidiaUpload()
        {
            return repository.Context.GetDocMultimidiaUpload().AsQueryable();
        }
        
        [Route("GetDocMultimidiaUploadNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaUpload> GetDocMultimidiaUploadNoAssociations()
        {
            return repository.Context.GetDocMultimidiaUploadNoAssociations().AsQueryable();
        }
        
        [Route("GetDocMultimidiaUploadByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaUpload> GetDocMultimidiaUploadByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetDocMultimidiaUploadByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaUpload), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        
        [Route("GetDocMultimidiaUploadByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaUpload> GetDocMultimidiaUploadByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetDocMultimidiaUploadByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaUpload), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        [Route("GetDocMultimidiaUploadToExcel"), System.Web.Http.HttpPost()]
        public string GetDocMultimidiaUploadToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaUpload), jEntitySearch, false, false, false);
            var entities = repository.Context.GetDocMultimidiaUploadByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).AsQueryable().OrderBy("TipoDocumento asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Multimidia.DocMultimidiaUpload");
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
               return ExcelExportPagination<BusinessNS.DocMultimidiaUpload>.CreateExcelDocumentFileMapPath("DocMultimidiaUpload",new ExcelExportPagination<BusinessNS.DocMultimidiaUpload>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetDocMultimidiaUploadToReportXml"), System.Web.Http.HttpPost()]
        public string GetDocMultimidiaUploadToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaUpload), jEntitySearch, false, false, false);
            var entities = repository.Context.GetDocMultimidiaUploadByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Multimidia.DocMultimidiaUpload", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.MultimidiaDataSource", DataSourceObject = "GetDocMultimidiaUpload", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleDocMultimidiaUpload"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaUpload> GetSampleDocMultimidiaUpload(string details)
        {
            var result = repository.Context.GetDocMultimidiaUploadByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddDocMultimidiaUploadEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddDocMultimidiaUploadEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaUpload), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetDocMultimidiaUploadByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaUpload> GetDocMultimidiaUploadByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetDocMultimidiaUploadByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetDocTabelaSync"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocTabelaSync> GetDocTabelaSync()
        {
            return repository.Context.GetDocTabelaSync().AsQueryable();
        }
        
        [Route("GetDocTabelaSyncNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocTabelaSync> GetDocTabelaSyncNoAssociations()
        {
            return repository.Context.GetDocTabelaSyncNoAssociations().AsQueryable();
        }
        
        [Route("GetDocTabelaSyncByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocTabelaSync> GetDocTabelaSyncByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetDocTabelaSyncByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocTabelaSync), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        
        [Route("GetDocTabelaSyncByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocTabelaSync> GetDocTabelaSyncByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetDocTabelaSyncByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocTabelaSync), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        [Route("GetDocTabelaSyncToExcel"), System.Web.Http.HttpPost()]
        public string GetDocTabelaSyncToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocTabelaSync), jEntitySearch, false, false, false);
            var entities = repository.Context.GetDocTabelaSyncByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).AsQueryable().OrderBy("NomeTabela asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Multimidia.DocTabelaSync");
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
               return ExcelExportPagination<BusinessNS.DocTabelaSync>.CreateExcelDocumentFileMapPath("DocTabelaSync",new ExcelExportPagination<BusinessNS.DocTabelaSync>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetDocTabelaSyncToReportXml"), System.Web.Http.HttpPost()]
        public string GetDocTabelaSyncToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocTabelaSync), jEntitySearch, false, false, false);
            var entities = repository.Context.GetDocTabelaSyncByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Multimidia.DocTabelaSync", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.MultimidiaDataSource", DataSourceObject = "GetDocTabelaSync", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleDocTabelaSync"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocTabelaSync> GetSampleDocTabelaSync(string details)
        {
            var result = repository.Context.GetDocTabelaSyncByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddDocTabelaSyncEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddDocTabelaSyncEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocTabelaSync), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetDocTabelaSyncByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocTabelaSync> GetDocTabelaSyncByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetDocTabelaSyncByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        #endregion
        
        #region Get Business Entities By Parent Composition
        
        [Route("GetDocMultimidiaTabelaChildParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaTabelaChildParentComposition> GetDocMultimidiaTabelaChildParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetDocMultimidiaTabelaChildParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaTabelaChildParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetDocMultimidiaTabelaChildParentCompositionToExcel"), System.Web.Http.HttpPost()]
        public string GetDocMultimidiaTabelaChildParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("DocMultimidiaTabelaChild{", "DocMultimidiaTabelaChildParentComposition{");
            jEntitySearch = jEntitySearch.Replace("DocMultimidia{", "DocMultimidiaTabelaChildParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaTabelaChildParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetDocMultimidiaTabelaChildParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdChave asc, UidChave asc, UidDocumento asc, UidTabela asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Multimidia.DocMultimidiaTabelaChild");
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
               return ExcelExportPagination<BusinessNS.DocMultimidiaTabelaChildParentComposition>.CreateExcelDocumentFileMapPath("DocMultimidiaTabelaChild",new ExcelExportPagination<BusinessNS.DocMultimidiaTabelaChildParentComposition>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetDocMultimidiaTabelaChildParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        public string GetDocMultimidiaTabelaChildParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaTabelaChildParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetDocMultimidiaTabelaChildParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Multimidia.DocMultimidiaTabelaChild", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.MultimidiaDataSource", DataSourceObject = "GetDocMultimidiaTabelaChildParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleDocMultimidiaTabelaChildParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaTabelaChildParentComposition> GetSampleDocMultimidiaTabelaChildParentComposition(string details)
        {
            var result = repository.Context.GetDocMultimidiaTabelaChildParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
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
    
    public partial class LinxFrameworkMultimidiaFeedController : ODataController
    {
        private BusinessNS.MultimidiaDomainService _context;
        public BusinessNS.MultimidiaDomainService Context { get {  if (_context == null) { _context = new BusinessNS.MultimidiaDomainService(); _context.IsSecure = true; } return _context; }  }
        
        #region Get Action to Business Entities
        
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.DocMultimidiaTabela> GetDocMultimidiaTabelaById([FromODataUri]Int64 key0, [FromODataUri]System.Guid key1, [FromODataUri]System.Guid key2, [FromODataUri]System.Guid key3)
        {
            var entity = this.Context.GetDocMultimidiaTabelaByKey(key0, key1, key2, key3);
            if (entity != null)
               return (new BusinessNS.DocMultimidiaTabela[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.DocMultimidiaTabela>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.DocMultimidiaTabela> GetDocMultimidiaTabelaByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetDocMultimidiaTabelaByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaTabela), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.DocMultimidiaTabela>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.DocMultimidiaTabela> GetDocMultimidiaTabela()
        {
            return this.Context.GetDocMultimidiaTabelaByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.DocMultimidiaCompact> GetDocMultimidiaCompactById([FromODataUri]Int64 key0, [FromODataUri]System.Guid key1, [FromODataUri]System.Guid key2, [FromODataUri]System.Guid key3)
        {
            var entity = this.Context.GetDocMultimidiaCompactByKey(key0, key1, key2, key3);
            if (entity != null)
               return (new BusinessNS.DocMultimidiaCompact[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.DocMultimidiaCompact>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.DocMultimidiaCompact> GetDocMultimidiaCompactByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetDocMultimidiaCompactByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaCompact), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.DocMultimidiaCompact>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.DocMultimidiaCompact> GetDocMultimidiaCompact()
        {
            return this.Context.GetDocMultimidiaCompactByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.MultimidiaCompact2BO> GetMultimidiaCompact2BOById([FromODataUri]Int64 key0, [FromODataUri]System.Guid key1, [FromODataUri]System.Guid key2, [FromODataUri]System.Guid key3)
        {
            var entity = this.Context.GetMultimidiaCompact2BOByKey(key0, key1, key2, key3);
            if (entity != null)
               return (new BusinessNS.MultimidiaCompact2BO[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.MultimidiaCompact2BO>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.MultimidiaCompact2BO> GetMultimidiaCompact2BOByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetMultimidiaCompact2BOByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.MultimidiaCompact2BO), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.MultimidiaCompact2BO>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.MultimidiaCompact2BO> GetMultimidiaCompact2BO()
        {
            return this.Context.GetMultimidiaCompact2BOByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.DocMultimidiaUid> GetDocMultimidiaUidById([FromODataUri]System.Guid key0)
        {
            var entity = this.Context.GetDocMultimidiaUidByKey(key0);
            if (entity != null)
               return (new BusinessNS.DocMultimidiaUid[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.DocMultimidiaUid>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.DocMultimidiaUid> GetDocMultimidiaUidByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetDocMultimidiaUidByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaUid), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.DocMultimidiaUid>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.DocMultimidiaUid> GetDocMultimidiaUid()
        {
            return this.Context.GetDocMultimidiaUidByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.DocMultimidiaInfo> GetDocMultimidiaInfoById([FromODataUri]Guid key0)
        {
            var entity = this.Context.GetDocMultimidiaInfoByKey(key0);
            if (entity != null)
               return (new BusinessNS.DocMultimidiaInfo[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.DocMultimidiaInfo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.DocMultimidiaInfo> GetDocMultimidiaInfoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetDocMultimidiaInfoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaInfo), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.DocMultimidiaInfo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.DocMultimidiaInfo> GetDocMultimidiaInfo()
        {
            return this.Context.GetDocMultimidiaInfoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.DocMultimidia> GetDocMultimidiaById([FromODataUri]System.Guid key0)
        {
            var entity = this.Context.GetDocMultimidiaByKey(key0);
            if (entity != null)
               return (new BusinessNS.DocMultimidia[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.DocMultimidia>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.DocMultimidia> GetDocMultimidiaByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetDocMultimidiaByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidia), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.DocMultimidia>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.DocMultimidia> GetDocMultimidia()
        {
            return this.Context.GetDocMultimidiaByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.DocMultimidiaTabelaChild> GetDocMultimidia__DocMultimidiaTabelaChild(System.Guid key0, string navigation)
        {
            var entity = this.Context.GetDocMultimidiaByKey(key0);
            if (entity != null && navigation == "DocMultimidiaTabelaChildList")
            {
               entity.FillDetails(_context, null, null, new string[] { "DocMultimidiaTabelaChild" });
               return entity.DocMultimidiaTabelaChildList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.DocMultimidiaTabelaChild>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.DocMultimidiaTabelaChild> GetDocMultimidiaTabelaChildById([FromODataUri]Int64 key0, [FromODataUri]System.Guid key1, [FromODataUri]System.Guid key2, [FromODataUri]System.Guid key3)
        {
            var entity = this.Context.GetDocMultimidiaTabelaChildByKey(key0, key1, key2, key3);
            if (entity != null)
               return (new BusinessNS.DocMultimidiaTabelaChild[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.DocMultimidiaTabelaChild>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.DocMultimidiaTabelaChild> GetDocMultimidiaTabelaChildByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetDocMultimidiaTabelaChildByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaTabelaChild), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.DocMultimidiaTabelaChild>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.DocMultimidiaTabelaChild> GetDocMultimidiaTabelaChild()
        {
            return this.Context.GetDocMultimidiaTabelaChildByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.DocMultimidiaTabelaChildParentComposition> GetDocMultimidiaTabelaChildParentComposition()
        {
            return this.Context.GetDocMultimidiaTabelaChildParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.DocMultimidiaTabelaChildParentComposition> GetDocMultimidiaTabelaChildParentCompositionByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                jEntitySearch = jEntitySearch.Replace("DocMultimidiaTabelaChild{", "DocMultimidiaTabelaChildParentComposition{");
                jEntitySearch = jEntitySearch.Replace("DocMultimidia{", "DocMultimidiaTabelaChildParentComposition{");
                var entity = this.Context.GetDocMultimidiaTabelaChildParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaTabelaChildParentComposition), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.DocMultimidiaTabelaChildParentComposition>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.DocMultimidia> GetDocMultimidiaTabelaChild__DocMultimidia(Int64 key0, System.Guid key1, System.Guid key2, System.Guid key3, string navigation)
        {
            var entity = this.Context.GetDocMultimidiaTabelaChildByKey(key0, key1, key2, key3);
            if (entity != null && navigation == "DocMultimidia")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.DocMultimidia[] { entity.DocMultimidia }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.DocMultimidia>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.DocMultimidiaConfig> GetDocMultimidiaConfigById([FromODataUri]Int32 key0, [FromODataUri]Byte key1)
        {
            var entity = this.Context.GetDocMultimidiaConfigByKey(key0, key1);
            if (entity != null)
               return (new BusinessNS.DocMultimidiaConfig[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.DocMultimidiaConfig>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.DocMultimidiaConfig> GetDocMultimidiaConfigByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetDocMultimidiaConfigByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaConfig), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.DocMultimidiaConfig>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.DocMultimidiaConfig> GetDocMultimidiaConfig()
        {
            return this.Context.GetDocMultimidiaConfigByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.MediaElement> GetMediaElementById([FromODataUri]Guid key0)
        {
            var entity = this.Context.GetMediaElementByKey(key0);
            if (entity != null)
               return (new BusinessNS.MediaElement[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.MediaElement>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.MediaElement> GetMediaElementByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetMediaElementByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.MediaElement), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.MediaElement>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.MediaElement> GetMediaElement()
        {
            return this.Context.GetMediaElementByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.MediaConfigLength> GetMediaConfigLengthById([FromODataUri]int key0, [FromODataUri]int key1)
        {
            var entity = this.Context.GetMediaConfigLengthByKey(key0, key1);
            if (entity != null)
               return (new BusinessNS.MediaConfigLength[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.MediaConfigLength>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.MediaConfigLength> GetMediaConfigLengthByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetMediaConfigLengthByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.MediaConfigLength), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.MediaConfigLength>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.MediaConfigLength> GetMediaConfigLength()
        {
            return this.Context.GetMediaConfigLengthByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.DocMultimidiaUpload> GetDocMultimidiaUploadById([FromODataUri]byte key0)
        {
            var entity = this.Context.GetDocMultimidiaUploadByKey(key0);
            if (entity != null)
               return (new BusinessNS.DocMultimidiaUpload[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.DocMultimidiaUpload>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.DocMultimidiaUpload> GetDocMultimidiaUploadByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetDocMultimidiaUploadByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaUpload), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.DocMultimidiaUpload>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.DocMultimidiaUpload> GetDocMultimidiaUpload()
        {
            return this.Context.GetDocMultimidiaUploadByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.DocTabelaSync> GetDocTabelaSyncById([FromODataUri]string key0)
        {
            var entity = this.Context.GetDocTabelaSyncByKey(key0);
            if (entity != null)
               return (new BusinessNS.DocTabelaSync[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.DocTabelaSync>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.DocTabelaSync> GetDocTabelaSyncByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetDocTabelaSyncByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocTabelaSync), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.DocTabelaSync>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.DocTabelaSync> GetDocTabelaSync()
        {
            return this.Context.GetDocTabelaSyncByEntitySearchNoAssociations(null).AsQueryable();
        }
        #endregion
        
    }
    
    public partial class LinxFrameworkMultimidiaControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return true;
        }
    }
}

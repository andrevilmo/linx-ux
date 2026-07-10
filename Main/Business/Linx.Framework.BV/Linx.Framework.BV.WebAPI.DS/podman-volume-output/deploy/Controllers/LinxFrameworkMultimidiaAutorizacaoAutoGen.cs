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
using BusinessNS = Linx.Framework.BV.MultimidiaAutorizacao;

namespace Linx.Framework.BV.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/LinxFrameworkMultimidiaAutorizacao/[ActionName]
    // Security Information Call: http://localhost:1710/LinxFrameworkMultimidiaAutorizacao/GetSecurityInfo
    // Entities Catalog Call: http://localhost:1710/LinxFrameworkMultimidiaAutorizacao/GetEntities
    // Entity MetaData Call: http://localhost:1710/LinxFrameworkMultimidiaAutorizacao/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/LinxFrameworkMultimidiaAutorizacao/GetClientDomains?erp=true
    // Client Service Call: http://localhost:1710/LinxFrameworkMultimidiaAutorizacao/GetClientService?erp=true
    // Client Factory Call: http://localhost:1710/LinxFrameworkMultimidiaAutorizacao/GetClientFactory?entityName=[EntityName]&erp=true
    // Client Factory Custom Events Call: http://localhost:1710/LinxFrameworkMultimidiaAutorizacao/GetClientFactoryCustomEvents?entityName=[EntityName]&erp=true
    // Help Call: http://localhost:1710/HelpController/LinxFrameworkMultimidiaAutorizacao
    // Feed OData Call: http://localhost:1710/LinxFrameworkMultimidiaAutorizacaoOData
    [RoutePrefix("LinxFrameworkMultimidiaAutorizacao")]
    [Breeze.WebApi2.BreezeController]
    public partial class LinxFrameworkMultimidiaAutorizacaoController : ApiController
    {
        private const int maxObjectExcelReturned = 2300000;
        private DataServiceRepository<BusinessNS.MultimidiaAutorizacaoDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.MultimidiaAutorizacaoDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.MultimidiaAutorizacaoDomainService>(typeof(BusinessNS.DocMultimidiaAutorizacao), typeof(BusinessNS.DocMultimidiaTabelaAutorizacao), typeof(BusinessNS.DocMultimidiaTabelaAutorizacaoChild)); _repository.Context.IsSecure = true; } return _repository; } }
        public LinxFrameworkMultimidiaAutorizacaoController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(LinxFrameworkMultimidiaAutorizacaoController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.MultimidiaAutorizacaoDomainService).Assembly.FullName,
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
            var result = repository.Context.GetMetaDataObject("Linx.Framework.BV.MultimidiaAutorizacao." + entityName, false, true);
            return (allComposition ? result : result.Where(e => e.ClassName == entityName).ToList());
        }
        
        [Route("GetSecurityInfo"), System.Web.Http.HttpGet()]
        public string[] GetSecurityInfo()
        {
           return new string[] { "Linx.Framework.BV", "LinxFrameworkMultimidiaAutorizacao", "LinxFrameworkMultimidiaAutorizacao/ActionName" };
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
        
        [Route("GetAllLookUpDocMultimidiaAutorizacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpDocMultimidiaAutorizacao> GetAllLookUpDocMultimidiaAutorizacao()
        {
            return repository.Context.GetAllLookUpDocMultimidiaAutorizacao();
        }
        
        [Route("GetLookUpDocMultimidiaAutorizacaoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpDocMultimidiaAutorizacao> GetLookUpDocMultimidiaAutorizacaoByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpDocMultimidiaAutorizacaoByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
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
        
        [Route("GetDocMultimidiaAutorizacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaAutorizacao> GetDocMultimidiaAutorizacao()
        {
            return repository.Context.GetDocMultimidiaAutorizacao();
        }
        
        [Route("GetDocMultimidiaAutorizacaoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaAutorizacao> GetDocMultimidiaAutorizacaoNoAssociations()
        {
            return repository.Context.GetDocMultimidiaAutorizacaoNoAssociations();
        }
        
        [Route("GetDocMultimidiaAutorizacaoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaAutorizacao> GetDocMultimidiaAutorizacaoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetDocMultimidiaAutorizacaoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaAutorizacao), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetDocMultimidiaAutorizacaoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaAutorizacao> GetDocMultimidiaAutorizacaoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetDocMultimidiaAutorizacaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaAutorizacao), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetDocMultimidiaAutorizacaoToExcel"), System.Web.Http.HttpPost()]
        public string GetDocMultimidiaAutorizacaoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaAutorizacao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetDocMultimidiaAutorizacaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("UidDocumento asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.MultimidiaAutorizacao.DocMultimidiaAutorizacao");
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
               return ExcelExportPagination<BusinessNS.DocMultimidiaAutorizacao>.CreateExcelDocumentFileMapPath("DocMultimidiaAutorizacao",new ExcelExportPagination<BusinessNS.DocMultimidiaAutorizacao>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetDocMultimidiaAutorizacaoToReportXml"), System.Web.Http.HttpPost()]
        public string GetDocMultimidiaAutorizacaoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaAutorizacao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetDocMultimidiaAutorizacaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.MultimidiaAutorizacao.DocMultimidiaAutorizacao", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.MultimidiaAutorizacaoDataSource", DataSourceObject = "GetDocMultimidiaAutorizacao", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleDocMultimidiaAutorizacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaAutorizacao> GetSampleDocMultimidiaAutorizacao(string details)
        {
            var result = repository.Context.GetDocMultimidiaAutorizacaoByEntitySearchNoAssociations(null).Take(100).ToList();
               if (!details.IsNullOrEmpty())
               {
                   foreach(var entity in result)
                   {
                       entity.FillDetails(repository.Context, null, null, details.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries), 5);
                   }
               }
            return result.AsQueryable();
        }
        
        [Route("AddDocMultimidiaAutorizacaoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddDocMultimidiaAutorizacaoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaAutorizacao), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetDocMultimidiaAutorizacaoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaAutorizacao> GetDocMultimidiaAutorizacaoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetDocMultimidiaAutorizacaoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetDocMultimidiaTabelaAutorizacaoChild"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaTabelaAutorizacaoChild> GetDocMultimidiaTabelaAutorizacaoChild()
        {
            return repository.Context.GetDocMultimidiaTabelaAutorizacaoChild();
        }
        
        [Route("GetDocMultimidiaTabelaAutorizacaoChildNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaTabelaAutorizacaoChild> GetDocMultimidiaTabelaAutorizacaoChildNoAssociations()
        {
            return repository.Context.GetDocMultimidiaTabelaAutorizacaoChildNoAssociations();
        }
        
        [Route("GetDocMultimidiaTabelaAutorizacaoChildByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaTabelaAutorizacaoChild> GetDocMultimidiaTabelaAutorizacaoChildByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetDocMultimidiaTabelaAutorizacaoChildByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaTabelaAutorizacaoChild), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetDocMultimidiaTabelaAutorizacaoChildByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaTabelaAutorizacaoChild> GetDocMultimidiaTabelaAutorizacaoChildByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetDocMultimidiaTabelaAutorizacaoChildByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaTabelaAutorizacaoChild), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetDocMultimidiaTabelaAutorizacaoChildToExcel"), System.Web.Http.HttpPost()]
        public string GetDocMultimidiaTabelaAutorizacaoChildToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaTabelaAutorizacaoChild), jEntitySearch, false, false, false);
            var entities = repository.Context.GetDocMultimidiaTabelaAutorizacaoChildByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdChave asc, UidChave asc, UidDocumento asc, UidTabela asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.MultimidiaAutorizacao.DocMultimidiaTabelaAutorizacaoChild");
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
               return ExcelExportPagination<BusinessNS.DocMultimidiaTabelaAutorizacaoChild>.CreateExcelDocumentFileMapPath("DocMultimidiaTabelaAutorizacaoChild",new ExcelExportPagination<BusinessNS.DocMultimidiaTabelaAutorizacaoChild>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetDocMultimidiaTabelaAutorizacaoChildToReportXml"), System.Web.Http.HttpPost()]
        public string GetDocMultimidiaTabelaAutorizacaoChildToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaTabelaAutorizacaoChild), jEntitySearch, false, false, false);
            var entities = repository.Context.GetDocMultimidiaTabelaAutorizacaoChildByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.MultimidiaAutorizacao.DocMultimidiaTabelaAutorizacaoChild", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.MultimidiaAutorizacaoDataSource", DataSourceObject = "GetDocMultimidiaTabelaAutorizacaoChild", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleDocMultimidiaTabelaAutorizacaoChild"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaTabelaAutorizacaoChild> GetSampleDocMultimidiaTabelaAutorizacaoChild(string details)
        {
            var result = repository.Context.GetDocMultimidiaTabelaAutorizacaoChildByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddDocMultimidiaTabelaAutorizacaoChildEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddDocMultimidiaTabelaAutorizacaoChildEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaTabelaAutorizacaoChild), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetDocMultimidiaTabelaAutorizacaoChildByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaTabelaAutorizacaoChild> GetDocMultimidiaTabelaAutorizacaoChildByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetDocMultimidiaTabelaAutorizacaoChildByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetDocMultimidiaTabelaAutorizacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaTabelaAutorizacao> GetDocMultimidiaTabelaAutorizacao()
        {
            return repository.Context.GetDocMultimidiaTabelaAutorizacao();
        }
        
        [Route("GetDocMultimidiaTabelaAutorizacaoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaTabelaAutorizacao> GetDocMultimidiaTabelaAutorizacaoNoAssociations()
        {
            return repository.Context.GetDocMultimidiaTabelaAutorizacaoNoAssociations();
        }
        
        [Route("GetDocMultimidiaTabelaAutorizacaoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaTabelaAutorizacao> GetDocMultimidiaTabelaAutorizacaoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetDocMultimidiaTabelaAutorizacaoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaTabelaAutorizacao), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetDocMultimidiaTabelaAutorizacaoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaTabelaAutorizacao> GetDocMultimidiaTabelaAutorizacaoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetDocMultimidiaTabelaAutorizacaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaTabelaAutorizacao), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetDocMultimidiaTabelaAutorizacaoToExcel"), System.Web.Http.HttpPost()]
        public string GetDocMultimidiaTabelaAutorizacaoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaTabelaAutorizacao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetDocMultimidiaTabelaAutorizacaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdChave asc, UidChave asc, UidDocumento asc, UidTabela asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.MultimidiaAutorizacao.DocMultimidiaTabelaAutorizacao");
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
               return ExcelExportPagination<BusinessNS.DocMultimidiaTabelaAutorizacao>.CreateExcelDocumentFileMapPath("DocMultimidiaTabelaAutorizacao",new ExcelExportPagination<BusinessNS.DocMultimidiaTabelaAutorizacao>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetDocMultimidiaTabelaAutorizacaoToReportXml"), System.Web.Http.HttpPost()]
        public string GetDocMultimidiaTabelaAutorizacaoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaTabelaAutorizacao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetDocMultimidiaTabelaAutorizacaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.MultimidiaAutorizacao.DocMultimidiaTabelaAutorizacao", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.MultimidiaAutorizacaoDataSource", DataSourceObject = "GetDocMultimidiaTabelaAutorizacao", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleDocMultimidiaTabelaAutorizacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaTabelaAutorizacao> GetSampleDocMultimidiaTabelaAutorizacao(string details)
        {
            var result = repository.Context.GetDocMultimidiaTabelaAutorizacaoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddDocMultimidiaTabelaAutorizacaoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddDocMultimidiaTabelaAutorizacaoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaTabelaAutorizacao), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetDocMultimidiaTabelaAutorizacaoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaTabelaAutorizacao> GetDocMultimidiaTabelaAutorizacaoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetDocMultimidiaTabelaAutorizacaoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        #endregion
        
        #region Get Business Entities By Parent Composition
        
        [Route("GetDocMultimidiaTabelaAutorizacaoChildParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaTabelaAutorizacaoChildParentComposition> GetDocMultimidiaTabelaAutorizacaoChildParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetDocMultimidiaTabelaAutorizacaoChildParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaTabelaAutorizacaoChildParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetDocMultimidiaTabelaAutorizacaoChildParentCompositionToExcel"), System.Web.Http.HttpPost()]
        public string GetDocMultimidiaTabelaAutorizacaoChildParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("DocMultimidiaTabelaAutorizacaoChild{", "DocMultimidiaTabelaAutorizacaoChildParentComposition{");
            jEntitySearch = jEntitySearch.Replace("DocMultimidiaAutorizacao{", "DocMultimidiaTabelaAutorizacaoChildParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaTabelaAutorizacaoChildParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetDocMultimidiaTabelaAutorizacaoChildParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdChave asc, UidChave asc, UidDocumento asc, UidTabela asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.MultimidiaAutorizacao.DocMultimidiaTabelaAutorizacaoChild");
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
               return ExcelExportPagination<BusinessNS.DocMultimidiaTabelaAutorizacaoChildParentComposition>.CreateExcelDocumentFileMapPath("DocMultimidiaTabelaAutorizacaoChild",new ExcelExportPagination<BusinessNS.DocMultimidiaTabelaAutorizacaoChildParentComposition>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetDocMultimidiaTabelaAutorizacaoChildParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        public string GetDocMultimidiaTabelaAutorizacaoChildParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaTabelaAutorizacaoChildParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetDocMultimidiaTabelaAutorizacaoChildParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.MultimidiaAutorizacao.DocMultimidiaTabelaAutorizacaoChild", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.MultimidiaAutorizacaoDataSource", DataSourceObject = "GetDocMultimidiaTabelaAutorizacaoChildParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleDocMultimidiaTabelaAutorizacaoChildParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.DocMultimidiaTabelaAutorizacaoChildParentComposition> GetSampleDocMultimidiaTabelaAutorizacaoChildParentComposition(string details)
        {
            var result = repository.Context.GetDocMultimidiaTabelaAutorizacaoChildParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
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
    
    public partial class LinxFrameworkMultimidiaAutorizacaoFeedController : ODataController
    {
        private BusinessNS.MultimidiaAutorizacaoDomainService _context;
        public BusinessNS.MultimidiaAutorizacaoDomainService Context { get {  if (_context == null) { _context = new BusinessNS.MultimidiaAutorizacaoDomainService(); _context.IsSecure = true; } return _context; }  }
        
        #region Get Action to Business Entities
        
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.DocMultimidiaAutorizacao> GetDocMultimidiaAutorizacaoById([FromODataUri]System.Guid key0)
        {
            var entity = this.Context.GetDocMultimidiaAutorizacaoByKey(key0);
            if (entity != null)
               return (new BusinessNS.DocMultimidiaAutorizacao[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.DocMultimidiaAutorizacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.DocMultimidiaAutorizacao> GetDocMultimidiaAutorizacaoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetDocMultimidiaAutorizacaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaAutorizacao), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.DocMultimidiaAutorizacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.DocMultimidiaAutorizacao> GetDocMultimidiaAutorizacao()
        {
            return this.Context.GetDocMultimidiaAutorizacaoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.DocMultimidiaTabelaAutorizacaoChild> GetDocMultimidiaAutorizacao__DocMultimidiaTabelaAutorizacaoChild(System.Guid key0, string navigation)
        {
            var entity = this.Context.GetDocMultimidiaAutorizacaoByKey(key0);
            if (entity != null && navigation == "DocMultimidiaTabelaAutorizacaoChildList")
            {
               entity.FillDetails(_context, null, null, new string[] { "DocMultimidiaTabelaAutorizacaoChild" });
               return entity.DocMultimidiaTabelaAutorizacaoChildList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.DocMultimidiaTabelaAutorizacaoChild>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.DocMultimidiaTabelaAutorizacaoChild> GetDocMultimidiaTabelaAutorizacaoChildById([FromODataUri]Int64 key0, [FromODataUri]System.Guid key1, [FromODataUri]System.Guid key2, [FromODataUri]System.Guid key3)
        {
            var entity = this.Context.GetDocMultimidiaTabelaAutorizacaoChildByKey(key0, key1, key2, key3);
            if (entity != null)
               return (new BusinessNS.DocMultimidiaTabelaAutorizacaoChild[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.DocMultimidiaTabelaAutorizacaoChild>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.DocMultimidiaTabelaAutorizacaoChild> GetDocMultimidiaTabelaAutorizacaoChildByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetDocMultimidiaTabelaAutorizacaoChildByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaTabelaAutorizacaoChild), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.DocMultimidiaTabelaAutorizacaoChild>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.DocMultimidiaTabelaAutorizacaoChild> GetDocMultimidiaTabelaAutorizacaoChild()
        {
            return this.Context.GetDocMultimidiaTabelaAutorizacaoChildByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.DocMultimidiaTabelaAutorizacaoChildParentComposition> GetDocMultimidiaTabelaAutorizacaoChildParentComposition()
        {
            return this.Context.GetDocMultimidiaTabelaAutorizacaoChildParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.DocMultimidiaTabelaAutorizacaoChildParentComposition> GetDocMultimidiaTabelaAutorizacaoChildParentCompositionByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                jEntitySearch = jEntitySearch.Replace("DocMultimidiaTabelaAutorizacaoChild{", "DocMultimidiaTabelaAutorizacaoChildParentComposition{");
                jEntitySearch = jEntitySearch.Replace("DocMultimidiaAutorizacao{", "DocMultimidiaTabelaAutorizacaoChildParentComposition{");
                var entity = this.Context.GetDocMultimidiaTabelaAutorizacaoChildParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaTabelaAutorizacaoChildParentComposition), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.DocMultimidiaTabelaAutorizacaoChildParentComposition>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.DocMultimidiaAutorizacao> GetDocMultimidiaTabelaAutorizacaoChild__DocMultimidiaAutorizacao(Int64 key0, System.Guid key1, System.Guid key2, System.Guid key3, string navigation)
        {
            var entity = this.Context.GetDocMultimidiaTabelaAutorizacaoChildByKey(key0, key1, key2, key3);
            if (entity != null && navigation == "DocMultimidiaAutorizacao")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.DocMultimidiaAutorizacao[] { entity.DocMultimidiaAutorizacao }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.DocMultimidiaAutorizacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.DocMultimidiaTabelaAutorizacao> GetDocMultimidiaTabelaAutorizacaoById([FromODataUri]Int64 key0, [FromODataUri]System.Guid key1, [FromODataUri]System.Guid key2, [FromODataUri]System.Guid key3)
        {
            var entity = this.Context.GetDocMultimidiaTabelaAutorizacaoByKey(key0, key1, key2, key3);
            if (entity != null)
               return (new BusinessNS.DocMultimidiaTabelaAutorizacao[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.DocMultimidiaTabelaAutorizacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.DocMultimidiaTabelaAutorizacao> GetDocMultimidiaTabelaAutorizacaoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetDocMultimidiaTabelaAutorizacaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.DocMultimidiaTabelaAutorizacao), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.DocMultimidiaTabelaAutorizacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.DocMultimidiaTabelaAutorizacao> GetDocMultimidiaTabelaAutorizacao()
        {
            return this.Context.GetDocMultimidiaTabelaAutorizacaoByEntitySearchNoAssociations(null).AsQueryable();
        }
        #endregion
        
    }
    
    public partial class LinxFrameworkMultimidiaAutorizacaoControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return true;
        }
    }
}

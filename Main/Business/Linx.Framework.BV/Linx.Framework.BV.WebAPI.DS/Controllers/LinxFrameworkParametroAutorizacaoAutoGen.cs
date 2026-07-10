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
using BusinessNS = Linx.Framework.BV.ParametroAutorizacao;

namespace Linx.Framework.BV.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/LinxFrameworkParametroAutorizacao/[ActionName]
    // Security Information Call: http://localhost:1710/LinxFrameworkParametroAutorizacao/GetSecurityInfo
    // Entities Catalog Call: http://localhost:1710/LinxFrameworkParametroAutorizacao/GetEntities
    // Entity MetaData Call: http://localhost:1710/LinxFrameworkParametroAutorizacao/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/LinxFrameworkParametroAutorizacao/GetClientDomains?erp=true
    // Client Service Call: http://localhost:1710/LinxFrameworkParametroAutorizacao/GetClientService?erp=true
    // Client Factory Call: http://localhost:1710/LinxFrameworkParametroAutorizacao/GetClientFactory?entityName=[EntityName]&erp=true
    // Client Factory Custom Events Call: http://localhost:1710/LinxFrameworkParametroAutorizacao/GetClientFactoryCustomEvents?entityName=[EntityName]&erp=true
    // Help Call: http://localhost:1710/HelpController/LinxFrameworkParametroAutorizacao
    // Feed OData Call: http://localhost:1710/LinxFrameworkParametroAutorizacaoOData
    [RoutePrefix("LinxFrameworkParametroAutorizacao")]
    [Breeze.WebApi2.BreezeController]
    public partial class LinxFrameworkParametroAutorizacaoController : ApiController
    {
        private const int maxObjectExcelReturned = 2300000;
        private DataServiceRepository<BusinessNS.ParametroAutorizacaoDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.ParametroAutorizacaoDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.ParametroAutorizacaoDomainService>(typeof(BusinessNS.TcsParametroAutorizacao), typeof(BusinessNS.TcsParametroAutorizacaoGrupo), typeof(BusinessNS.TcsParametroGrupoAutorizacao), typeof(BusinessNS.TcsParametroTabelaSelecaoAutorizacao)); _repository.Context.IsSecure = true; } return _repository; } }
        public LinxFrameworkParametroAutorizacaoController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(LinxFrameworkParametroAutorizacaoController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.ParametroAutorizacaoDomainService).Assembly.FullName,
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
            var result = repository.Context.GetMetaDataObject("Linx.Framework.BV.ParametroAutorizacao." + entityName, false, true);
            return (allComposition ? result : result.Where(e => e.ClassName == entityName).ToList());
        }
        
        [Route("GetSecurityInfo"), System.Web.Http.HttpGet()]
        public string[] GetSecurityInfo()
        {
           return new string[] { "Linx.Framework.BV", "LinxFrameworkParametroAutorizacao", "LinxFrameworkParametroAutorizacao/ActionName" };
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
        
        [Route("GetAllLookUpTcsParametroGrupoAutorizacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsParametroGrupoAutorizacao> GetAllLookUpTcsParametroGrupoAutorizacao()
        {
            return repository.Context.GetAllLookUpTcsParametroGrupoAutorizacao();
        }
        
        [Route("GetLookUpTcsParametroGrupoAutorizacaoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsParametroGrupoAutorizacao> GetLookUpTcsParametroGrupoAutorizacaoByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsParametroGrupoAutorizacaoByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [Route("GetAllLookUpTcsTabelaAutorizacaoSelecao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsTabelaAutorizacaoSelecao> GetAllLookUpTcsTabelaAutorizacaoSelecao()
        {
            return repository.Context.GetAllLookUpTcsTabelaAutorizacaoSelecao();
        }
        
        [Route("GetLookUpTcsTabelaAutorizacaoSelecaoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsTabelaAutorizacaoSelecao> GetLookUpTcsTabelaAutorizacaoSelecaoByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsTabelaAutorizacaoSelecaoByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
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
        
        [Route("GetTcsParametroAutorizacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroAutorizacao> GetTcsParametroAutorizacao()
        {
            return repository.Context.GetTcsParametroAutorizacao();
        }
        
        [Route("GetTcsParametroAutorizacaoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroAutorizacao> GetTcsParametroAutorizacaoNoAssociations()
        {
            return repository.Context.GetTcsParametroAutorizacaoNoAssociations();
        }
        
        [Route("GetTcsParametroAutorizacaoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroAutorizacao> GetTcsParametroAutorizacaoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroAutorizacaoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroAutorizacao), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsParametroAutorizacaoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroAutorizacao> GetTcsParametroAutorizacaoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroAutorizacaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroAutorizacao), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsParametroAutorizacaoToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsParametroAutorizacaoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroAutorizacao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroAutorizacaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdParametro asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.ParametroAutorizacao.TcsParametroAutorizacao");
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
               return ExcelExportPagination<BusinessNS.TcsParametroAutorizacao>.CreateExcelDocumentFileMapPath("TcsParametroAutorizacao",new ExcelExportPagination<BusinessNS.TcsParametroAutorizacao>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsParametroAutorizacaoToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsParametroAutorizacaoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroAutorizacao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroAutorizacaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.ParametroAutorizacao.TcsParametroAutorizacao", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ParametroAutorizacaoDataSource", DataSourceObject = "GetTcsParametroAutorizacao", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsParametroAutorizacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroAutorizacao> GetSampleTcsParametroAutorizacao(string details)
        {
            var result = repository.Context.GetTcsParametroAutorizacaoByEntitySearchNoAssociations(null).Take(100).ToList();
               if (!details.IsNullOrEmpty())
               {
                   foreach(var entity in result)
                   {
                       entity.FillDetails(repository.Context, null, null, details.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries), 5);
                   }
               }
            return result.AsQueryable();
        }
        
        [Route("AddTcsParametroAutorizacaoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsParametroAutorizacaoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroAutorizacao), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsParametroAutorizacaoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroAutorizacao> GetTcsParametroAutorizacaoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsParametroAutorizacaoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsParametroTabelaSelecaoAutorizacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroTabelaSelecaoAutorizacao> GetTcsParametroTabelaSelecaoAutorizacao()
        {
            return repository.Context.GetTcsParametroTabelaSelecaoAutorizacao();
        }
        
        [Route("GetTcsParametroTabelaSelecaoAutorizacaoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroTabelaSelecaoAutorizacao> GetTcsParametroTabelaSelecaoAutorizacaoNoAssociations()
        {
            return repository.Context.GetTcsParametroTabelaSelecaoAutorizacaoNoAssociations();
        }
        
        [Route("GetTcsParametroTabelaSelecaoAutorizacaoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroTabelaSelecaoAutorizacao> GetTcsParametroTabelaSelecaoAutorizacaoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroTabelaSelecaoAutorizacaoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroTabelaSelecaoAutorizacao), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsParametroTabelaSelecaoAutorizacaoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroTabelaSelecaoAutorizacao> GetTcsParametroTabelaSelecaoAutorizacaoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroTabelaSelecaoAutorizacaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroTabelaSelecaoAutorizacao), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsParametroTabelaSelecaoAutorizacaoToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsParametroTabelaSelecaoAutorizacaoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroTabelaSelecaoAutorizacao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroTabelaSelecaoAutorizacaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTabelaSelecao asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.ParametroAutorizacao.TcsParametroTabelaSelecaoAutorizacao");
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
               return ExcelExportPagination<BusinessNS.TcsParametroTabelaSelecaoAutorizacao>.CreateExcelDocumentFileMapPath("TcsParametroTabelaSelecaoAutorizacao",new ExcelExportPagination<BusinessNS.TcsParametroTabelaSelecaoAutorizacao>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsParametroTabelaSelecaoAutorizacaoToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsParametroTabelaSelecaoAutorizacaoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroTabelaSelecaoAutorizacao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroTabelaSelecaoAutorizacaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.ParametroAutorizacao.TcsParametroTabelaSelecaoAutorizacao", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ParametroAutorizacaoDataSource", DataSourceObject = "GetTcsParametroTabelaSelecaoAutorizacao", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsParametroTabelaSelecaoAutorizacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroTabelaSelecaoAutorizacao> GetSampleTcsParametroTabelaSelecaoAutorizacao(string details)
        {
            var result = repository.Context.GetTcsParametroTabelaSelecaoAutorizacaoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsParametroTabelaSelecaoAutorizacaoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsParametroTabelaSelecaoAutorizacaoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroTabelaSelecaoAutorizacao), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsParametroTabelaSelecaoAutorizacaoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroTabelaSelecaoAutorizacao> GetTcsParametroTabelaSelecaoAutorizacaoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsParametroTabelaSelecaoAutorizacaoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsParametroGrupoAutorizacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroGrupoAutorizacao> GetTcsParametroGrupoAutorizacao()
        {
            return repository.Context.GetTcsParametroGrupoAutorizacao();
        }
        
        [Route("GetTcsParametroGrupoAutorizacaoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroGrupoAutorizacao> GetTcsParametroGrupoAutorizacaoNoAssociations()
        {
            return repository.Context.GetTcsParametroGrupoAutorizacaoNoAssociations();
        }
        
        [Route("GetTcsParametroGrupoAutorizacaoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroGrupoAutorizacao> GetTcsParametroGrupoAutorizacaoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroGrupoAutorizacaoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroGrupoAutorizacao), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsParametroGrupoAutorizacaoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroGrupoAutorizacao> GetTcsParametroGrupoAutorizacaoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroGrupoAutorizacaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroGrupoAutorizacao), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsParametroGrupoAutorizacaoToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsParametroGrupoAutorizacaoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroGrupoAutorizacao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroGrupoAutorizacaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdGrupoParametro asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.ParametroAutorizacao.TcsParametroGrupoAutorizacao");
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
               return ExcelExportPagination<BusinessNS.TcsParametroGrupoAutorizacao>.CreateExcelDocumentFileMapPath("TcsParametroGrupoAutorizacao",new ExcelExportPagination<BusinessNS.TcsParametroGrupoAutorizacao>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsParametroGrupoAutorizacaoToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsParametroGrupoAutorizacaoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroGrupoAutorizacao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroGrupoAutorizacaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.ParametroAutorizacao.TcsParametroGrupoAutorizacao", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ParametroAutorizacaoDataSource", DataSourceObject = "GetTcsParametroGrupoAutorizacao", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsParametroGrupoAutorizacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroGrupoAutorizacao> GetSampleTcsParametroGrupoAutorizacao(string details)
        {
            var result = repository.Context.GetTcsParametroGrupoAutorizacaoByEntitySearchNoAssociations(null).Take(100).ToList();
               if (!details.IsNullOrEmpty())
               {
                   foreach(var entity in result)
                   {
                       entity.FillDetails(repository.Context, null, null, details.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries), 5);
                   }
               }
            return result.AsQueryable();
        }
        
        [Route("AddTcsParametroGrupoAutorizacaoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsParametroGrupoAutorizacaoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroGrupoAutorizacao), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsParametroGrupoAutorizacaoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroGrupoAutorizacao> GetTcsParametroGrupoAutorizacaoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsParametroGrupoAutorizacaoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsParametroAutorizacaoGrupo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroAutorizacaoGrupo> GetTcsParametroAutorizacaoGrupo()
        {
            return repository.Context.GetTcsParametroAutorizacaoGrupo();
        }
        
        [Route("GetTcsParametroAutorizacaoGrupoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroAutorizacaoGrupo> GetTcsParametroAutorizacaoGrupoNoAssociations()
        {
            return repository.Context.GetTcsParametroAutorizacaoGrupoNoAssociations();
        }
        
        [Route("GetTcsParametroAutorizacaoGrupoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroAutorizacaoGrupo> GetTcsParametroAutorizacaoGrupoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroAutorizacaoGrupoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroAutorizacaoGrupo), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsParametroAutorizacaoGrupoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroAutorizacaoGrupo> GetTcsParametroAutorizacaoGrupoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroAutorizacaoGrupoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroAutorizacaoGrupo), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsParametroAutorizacaoGrupoToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsParametroAutorizacaoGrupoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroAutorizacaoGrupo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroAutorizacaoGrupoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdParametro asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.ParametroAutorizacao.TcsParametroAutorizacaoGrupo");
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
               return ExcelExportPagination<BusinessNS.TcsParametroAutorizacaoGrupo>.CreateExcelDocumentFileMapPath("TcsParametroAutorizacaoGrupo",new ExcelExportPagination<BusinessNS.TcsParametroAutorizacaoGrupo>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsParametroAutorizacaoGrupoToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsParametroAutorizacaoGrupoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroAutorizacaoGrupo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroAutorizacaoGrupoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.ParametroAutorizacao.TcsParametroAutorizacaoGrupo", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ParametroAutorizacaoDataSource", DataSourceObject = "GetTcsParametroAutorizacaoGrupo", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsParametroAutorizacaoGrupo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroAutorizacaoGrupo> GetSampleTcsParametroAutorizacaoGrupo(string details)
        {
            var result = repository.Context.GetTcsParametroAutorizacaoGrupoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsParametroAutorizacaoGrupoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsParametroAutorizacaoGrupoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroAutorizacaoGrupo), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsParametroAutorizacaoGrupoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroAutorizacaoGrupo> GetTcsParametroAutorizacaoGrupoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsParametroAutorizacaoGrupoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        #endregion
        
        #region Get Business Entities By Parent Composition
        
        [Route("GetTcsParametroTabelaSelecaoAutorizacaoParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroTabelaSelecaoAutorizacaoParentComposition> GetTcsParametroTabelaSelecaoAutorizacaoParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroTabelaSelecaoAutorizacaoParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroTabelaSelecaoAutorizacaoParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsParametroTabelaSelecaoAutorizacaoParentCompositionToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsParametroTabelaSelecaoAutorizacaoParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("TcsParametroTabelaSelecaoAutorizacao{", "TcsParametroTabelaSelecaoAutorizacaoParentComposition{");
            jEntitySearch = jEntitySearch.Replace("TcsParametroAutorizacao{", "TcsParametroTabelaSelecaoAutorizacaoParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroTabelaSelecaoAutorizacaoParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroTabelaSelecaoAutorizacaoParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTabelaSelecao asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.ParametroAutorizacao.TcsParametroTabelaSelecaoAutorizacao");
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
               return ExcelExportPagination<BusinessNS.TcsParametroTabelaSelecaoAutorizacaoParentComposition>.CreateExcelDocumentFileMapPath("TcsParametroTabelaSelecaoAutorizacao",new ExcelExportPagination<BusinessNS.TcsParametroTabelaSelecaoAutorizacaoParentComposition>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsParametroTabelaSelecaoAutorizacaoParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsParametroTabelaSelecaoAutorizacaoParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroTabelaSelecaoAutorizacaoParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroTabelaSelecaoAutorizacaoParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.ParametroAutorizacao.TcsParametroTabelaSelecaoAutorizacao", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ParametroAutorizacaoDataSource", DataSourceObject = "GetTcsParametroTabelaSelecaoAutorizacaoParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsParametroTabelaSelecaoAutorizacaoParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroTabelaSelecaoAutorizacaoParentComposition> GetSampleTcsParametroTabelaSelecaoAutorizacaoParentComposition(string details)
        {
            var result = repository.Context.GetTcsParametroTabelaSelecaoAutorizacaoParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("GetTcsParametroAutorizacaoGrupoParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroAutorizacaoGrupoParentComposition> GetTcsParametroAutorizacaoGrupoParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroAutorizacaoGrupoParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroAutorizacaoGrupoParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsParametroAutorizacaoGrupoParentCompositionToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsParametroAutorizacaoGrupoParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("TcsParametroAutorizacaoGrupo{", "TcsParametroAutorizacaoGrupoParentComposition{");
            jEntitySearch = jEntitySearch.Replace("TcsParametroGrupoAutorizacao{", "TcsParametroAutorizacaoGrupoParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroAutorizacaoGrupoParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroAutorizacaoGrupoParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdParametro asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.ParametroAutorizacao.TcsParametroAutorizacaoGrupo");
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
               return ExcelExportPagination<BusinessNS.TcsParametroAutorizacaoGrupoParentComposition>.CreateExcelDocumentFileMapPath("TcsParametroAutorizacaoGrupo",new ExcelExportPagination<BusinessNS.TcsParametroAutorizacaoGrupoParentComposition>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsParametroAutorizacaoGrupoParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsParametroAutorizacaoGrupoParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroAutorizacaoGrupoParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroAutorizacaoGrupoParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.ParametroAutorizacao.TcsParametroAutorizacaoGrupo", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ParametroAutorizacaoDataSource", DataSourceObject = "GetTcsParametroAutorizacaoGrupoParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsParametroAutorizacaoGrupoParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroAutorizacaoGrupoParentComposition> GetSampleTcsParametroAutorizacaoGrupoParentComposition(string details)
        {
            var result = repository.Context.GetTcsParametroAutorizacaoGrupoParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
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
    
    public partial class LinxFrameworkParametroAutorizacaoFeedController : ODataController
    {
        private BusinessNS.ParametroAutorizacaoDomainService _context;
        public BusinessNS.ParametroAutorizacaoDomainService Context { get {  if (_context == null) { _context = new BusinessNS.ParametroAutorizacaoDomainService(); _context.IsSecure = true; } return _context; }  }
        
        #region Get Action to Business Entities
        
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroAutorizacao> GetTcsParametroAutorizacaoById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetTcsParametroAutorizacaoByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsParametroAutorizacao[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsParametroAutorizacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroAutorizacao> GetTcsParametroAutorizacaoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsParametroAutorizacaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroAutorizacao), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsParametroAutorizacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroAutorizacao> GetTcsParametroAutorizacao()
        {
            return this.Context.GetTcsParametroAutorizacaoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroTabelaSelecaoAutorizacao> GetTcsParametroAutorizacao__TcsParametroTabelaSelecaoAutorizacao(Int64 key0, string navigation)
        {
            var entity = this.Context.GetTcsParametroAutorizacaoByKey(key0);
            if (entity != null && navigation == "TcsParametroTabelaSelecaoAutorizacaoList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsParametroTabelaSelecaoAutorizacao" });
               return entity.TcsParametroTabelaSelecaoAutorizacaoList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsParametroTabelaSelecaoAutorizacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroTabelaSelecaoAutorizacao> GetTcsParametroTabelaSelecaoAutorizacaoById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetTcsParametroTabelaSelecaoAutorizacaoByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsParametroTabelaSelecaoAutorizacao[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsParametroTabelaSelecaoAutorizacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroTabelaSelecaoAutorizacao> GetTcsParametroTabelaSelecaoAutorizacaoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsParametroTabelaSelecaoAutorizacaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroTabelaSelecaoAutorizacao), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsParametroTabelaSelecaoAutorizacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroTabelaSelecaoAutorizacao> GetTcsParametroTabelaSelecaoAutorizacao()
        {
            return this.Context.GetTcsParametroTabelaSelecaoAutorizacaoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroTabelaSelecaoAutorizacaoParentComposition> GetTcsParametroTabelaSelecaoAutorizacaoParentComposition()
        {
            return this.Context.GetTcsParametroTabelaSelecaoAutorizacaoParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroTabelaSelecaoAutorizacaoParentComposition> GetTcsParametroTabelaSelecaoAutorizacaoParentCompositionByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                jEntitySearch = jEntitySearch.Replace("TcsParametroTabelaSelecaoAutorizacao{", "TcsParametroTabelaSelecaoAutorizacaoParentComposition{");
                jEntitySearch = jEntitySearch.Replace("TcsParametroAutorizacao{", "TcsParametroTabelaSelecaoAutorizacaoParentComposition{");
                var entity = this.Context.GetTcsParametroTabelaSelecaoAutorizacaoParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroTabelaSelecaoAutorizacaoParentComposition), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsParametroTabelaSelecaoAutorizacaoParentComposition>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroAutorizacao> GetTcsParametroTabelaSelecaoAutorizacao__TcsParametroAutorizacao(Int64 key0, string navigation)
        {
            var entity = this.Context.GetTcsParametroTabelaSelecaoAutorizacaoByKey(key0);
            if (entity != null && navigation == "TcsParametroAutorizacao")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsParametroAutorizacao[] { entity.TcsParametroAutorizacao }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsParametroAutorizacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroGrupoAutorizacao> GetTcsParametroGrupoAutorizacaoById([FromODataUri]Int16 key0)
        {
            var entity = this.Context.GetTcsParametroGrupoAutorizacaoByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsParametroGrupoAutorizacao[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsParametroGrupoAutorizacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroGrupoAutorizacao> GetTcsParametroGrupoAutorizacaoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsParametroGrupoAutorizacaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroGrupoAutorizacao), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsParametroGrupoAutorizacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroGrupoAutorizacao> GetTcsParametroGrupoAutorizacao()
        {
            return this.Context.GetTcsParametroGrupoAutorizacaoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroAutorizacaoGrupo> GetTcsParametroGrupoAutorizacao__TcsParametroAutorizacaoGrupo(Int16 key0, string navigation)
        {
            var entity = this.Context.GetTcsParametroGrupoAutorizacaoByKey(key0);
            if (entity != null && navigation == "TcsParametroAutorizacaoGrupoList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsParametroAutorizacaoGrupo" });
               return entity.TcsParametroAutorizacaoGrupoList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsParametroAutorizacaoGrupo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroAutorizacaoGrupo> GetTcsParametroAutorizacaoGrupoById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetTcsParametroAutorizacaoGrupoByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsParametroAutorizacaoGrupo[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsParametroAutorizacaoGrupo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroAutorizacaoGrupo> GetTcsParametroAutorizacaoGrupoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsParametroAutorizacaoGrupoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroAutorizacaoGrupo), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsParametroAutorizacaoGrupo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroAutorizacaoGrupo> GetTcsParametroAutorizacaoGrupo()
        {
            return this.Context.GetTcsParametroAutorizacaoGrupoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroAutorizacaoGrupoParentComposition> GetTcsParametroAutorizacaoGrupoParentComposition()
        {
            return this.Context.GetTcsParametroAutorizacaoGrupoParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroAutorizacaoGrupoParentComposition> GetTcsParametroAutorizacaoGrupoParentCompositionByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                jEntitySearch = jEntitySearch.Replace("TcsParametroAutorizacaoGrupo{", "TcsParametroAutorizacaoGrupoParentComposition{");
                jEntitySearch = jEntitySearch.Replace("TcsParametroGrupoAutorizacao{", "TcsParametroAutorizacaoGrupoParentComposition{");
                var entity = this.Context.GetTcsParametroAutorizacaoGrupoParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroAutorizacaoGrupoParentComposition), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsParametroAutorizacaoGrupoParentComposition>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroGrupoAutorizacao> GetTcsParametroAutorizacaoGrupo__TcsParametroGrupoAutorizacao(Int64 key0, string navigation)
        {
            var entity = this.Context.GetTcsParametroAutorizacaoGrupoByKey(key0);
            if (entity != null && navigation == "TcsParametroGrupoAutorizacao")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsParametroGrupoAutorizacao[] { entity.TcsParametroGrupoAutorizacao }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsParametroGrupoAutorizacao>);
        }
        #endregion
        
    }
    
    public partial class LinxFrameworkParametroAutorizacaoControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return true;
        }
    }
}

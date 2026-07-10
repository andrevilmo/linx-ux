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
using BusinessNS = Linx.Framework.BV.Objeto;

namespace Linx.Framework.BV.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/LinxFrameworkObjeto/[ActionName]
    // Security Information Call: http://localhost:1710/LinxFrameworkObjeto/GetSecurityInfo
    // Entities Catalog Call: http://localhost:1710/LinxFrameworkObjeto/GetEntities
    // Entity MetaData Call: http://localhost:1710/LinxFrameworkObjeto/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/LinxFrameworkObjeto/GetClientDomains?erp=true
    // Client Service Call: http://localhost:1710/LinxFrameworkObjeto/GetClientService?erp=true
    // Client Factory Call: http://localhost:1710/LinxFrameworkObjeto/GetClientFactory?entityName=[EntityName]&erp=true
    // Client Factory Custom Events Call: http://localhost:1710/LinxFrameworkObjeto/GetClientFactoryCustomEvents?entityName=[EntityName]&erp=true
    // Help Call: http://localhost:1710/HelpController/LinxFrameworkObjeto
    // Feed OData Call: http://localhost:1710/LinxFrameworkObjetoOData
    [RoutePrefix("LinxFrameworkObjeto")]
    [Breeze.WebApi2.BreezeController]
    public partial class LinxFrameworkObjetoController : ApiController
    {
        private const int maxObjectExcelReturned = 2300000;
        private DataServiceRepository<BusinessNS.ObjetoDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.ObjetoDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.ObjetoDomainService>(typeof(BusinessNS.ConfiguracaoExportacao), typeof(BusinessNS.LayoutInfo), typeof(BusinessNS.TcsObjeto), typeof(BusinessNS.TcsObjetoConteudoMnt), typeof(BusinessNS.TcsObjetoPermissao), typeof(BusinessNS.TcsTransacao), typeof(BusinessNS.TcsUsuario)); _repository.Context.IsSecure = true; } return _repository; } }
        public LinxFrameworkObjetoController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(LinxFrameworkObjetoController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.ObjetoDomainService).Assembly.FullName,
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
            var result = repository.Context.GetMetaDataObject("Linx.Framework.BV.Objeto." + entityName, false, true);
            return (allComposition ? result : result.Where(e => e.ClassName == entityName).ToList());
        }
        
        [Route("GetSecurityInfo"), System.Web.Http.HttpGet()]
        public string[] GetSecurityInfo()
        {
           return new string[] { "Linx.Framework.BV", "LinxFrameworkObjeto", "LinxFrameworkObjeto/ActionName" };
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
        
        [Route("GetTcsObjeto"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsObjeto> GetTcsObjeto()
        {
            return repository.Context.GetTcsObjeto();
        }
        
        [Route("GetTcsObjetoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsObjeto> GetTcsObjetoNoAssociations()
        {
            return repository.Context.GetTcsObjetoNoAssociations();
        }
        
        [Route("GetTcsObjetoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsObjeto> GetTcsObjetoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsObjetoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsObjeto), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsObjetoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsObjeto> GetTcsObjetoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsObjetoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsObjeto), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsObjetoToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsObjetoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsObjeto), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsObjetoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdObjeto asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Objeto.TcsObjeto");
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
               return ExcelExportPagination<BusinessNS.TcsObjeto>.CreateExcelDocumentFileMapPath("TcsObjeto",new ExcelExportPagination<BusinessNS.TcsObjeto>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsObjetoToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsObjetoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsObjeto), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsObjetoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Objeto.TcsObjeto", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ObjetoDataSource", DataSourceObject = "GetTcsObjeto", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsObjeto"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsObjeto> GetSampleTcsObjeto(string details)
        {
            var result = repository.Context.GetTcsObjetoByEntitySearchNoAssociations(null).Take(100).ToList();
               if (!details.IsNullOrEmpty())
               {
                   foreach(var entity in result)
                   {
                       entity.FillDetails(repository.Context, null, null, details.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries), 5);
                   }
               }
            return result.AsQueryable();
        }
        
        [Route("AddTcsObjetoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsObjetoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsObjeto), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsObjetoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsObjeto> GetTcsObjetoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsObjetoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsObjetoConteudoMnt"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsObjetoConteudoMnt> GetTcsObjetoConteudoMnt()
        {
            return repository.Context.GetTcsObjetoConteudoMnt();
        }
        
        [Route("GetTcsObjetoConteudoMntNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsObjetoConteudoMnt> GetTcsObjetoConteudoMntNoAssociations()
        {
            return repository.Context.GetTcsObjetoConteudoMntNoAssociations();
        }
        
        [Route("GetTcsObjetoConteudoMntByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsObjetoConteudoMnt> GetTcsObjetoConteudoMntByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsObjetoConteudoMntByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsObjetoConteudoMnt), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsObjetoConteudoMntByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsObjetoConteudoMnt> GetTcsObjetoConteudoMntByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsObjetoConteudoMntByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsObjetoConteudoMnt), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsObjetoConteudoMntToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsObjetoConteudoMntToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsObjetoConteudoMnt), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsObjetoConteudoMntByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("ConteudoXml asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Objeto.TcsObjetoConteudoMnt");
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
               return ExcelExportPagination<BusinessNS.TcsObjetoConteudoMnt>.CreateExcelDocumentFileMapPath("TcsObjetoConteudoMnt",new ExcelExportPagination<BusinessNS.TcsObjetoConteudoMnt>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsObjetoConteudoMntToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsObjetoConteudoMntToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsObjetoConteudoMnt), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsObjetoConteudoMntByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Objeto.TcsObjetoConteudoMnt", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ObjetoDataSource", DataSourceObject = "GetTcsObjetoConteudoMnt", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsObjetoConteudoMnt"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsObjetoConteudoMnt> GetSampleTcsObjetoConteudoMnt(string details)
        {
            var result = repository.Context.GetTcsObjetoConteudoMntByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsObjetoConteudoMntEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsObjetoConteudoMntEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsObjetoConteudoMnt), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsObjetoConteudoMntByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsObjetoConteudoMnt> GetTcsObjetoConteudoMntByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsObjetoConteudoMntByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsTransacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacao> GetTcsTransacao()
        {
            return repository.Context.GetTcsTransacao();
        }
        
        [Route("GetTcsTransacaoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacao> GetTcsTransacaoNoAssociations()
        {
            return repository.Context.GetTcsTransacaoNoAssociations();
        }
        
        [Route("GetTcsTransacaoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacao> GetTcsTransacaoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsTransacaoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacao), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsTransacaoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacao> GetTcsTransacaoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsTransacaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacao), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsTransacaoToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsTransacaoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsTransacaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTransacao asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Objeto.TcsTransacao");
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
               return ExcelExportPagination<BusinessNS.TcsTransacao>.CreateExcelDocumentFileMapPath("TcsTransacao",new ExcelExportPagination<BusinessNS.TcsTransacao>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsTransacaoToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsTransacaoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsTransacaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Objeto.TcsTransacao", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ObjetoDataSource", DataSourceObject = "GetTcsTransacao", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsTransacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacao> GetSampleTcsTransacao(string details)
        {
            var result = repository.Context.GetTcsTransacaoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsTransacaoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsTransacaoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacao), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsTransacaoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacao> GetTcsTransacaoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsTransacaoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetConfiguracaoExportacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.ConfiguracaoExportacao> GetConfiguracaoExportacao()
        {
            return repository.Context.GetConfiguracaoExportacao().AsQueryable();
        }
        
        [Route("GetConfiguracaoExportacaoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.ConfiguracaoExportacao> GetConfiguracaoExportacaoNoAssociations()
        {
            return repository.Context.GetConfiguracaoExportacaoNoAssociations().AsQueryable();
        }
        
        [Route("GetConfiguracaoExportacaoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.ConfiguracaoExportacao> GetConfiguracaoExportacaoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetConfiguracaoExportacaoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.ConfiguracaoExportacao), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        
        [Route("GetConfiguracaoExportacaoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.ConfiguracaoExportacao> GetConfiguracaoExportacaoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetConfiguracaoExportacaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.ConfiguracaoExportacao), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        [Route("GetConfiguracaoExportacaoToExcel"), System.Web.Http.HttpPost()]
        public string GetConfiguracaoExportacaoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.ConfiguracaoExportacao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetConfiguracaoExportacaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).AsQueryable().OrderBy("Id asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Objeto.ConfiguracaoExportacao");
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
               return ExcelExportPagination<BusinessNS.ConfiguracaoExportacao>.CreateExcelDocumentFileMapPath("ConfiguracaoExportacao",new ExcelExportPagination<BusinessNS.ConfiguracaoExportacao>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetConfiguracaoExportacaoToReportXml"), System.Web.Http.HttpPost()]
        public string GetConfiguracaoExportacaoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.ConfiguracaoExportacao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetConfiguracaoExportacaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Objeto.ConfiguracaoExportacao", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ObjetoDataSource", DataSourceObject = "GetConfiguracaoExportacao", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleConfiguracaoExportacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.ConfiguracaoExportacao> GetSampleConfiguracaoExportacao(string details)
        {
            var result = repository.Context.GetConfiguracaoExportacaoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddConfiguracaoExportacaoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddConfiguracaoExportacaoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.ConfiguracaoExportacao), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetConfiguracaoExportacaoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.ConfiguracaoExportacao> GetConfiguracaoExportacaoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetConfiguracaoExportacaoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsObjetoPermissao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsObjetoPermissao> GetTcsObjetoPermissao()
        {
            return repository.Context.GetTcsObjetoPermissao();
        }
        
        [Route("GetTcsObjetoPermissaoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsObjetoPermissao> GetTcsObjetoPermissaoNoAssociations()
        {
            return repository.Context.GetTcsObjetoPermissaoNoAssociations();
        }
        
        [Route("GetTcsObjetoPermissaoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsObjetoPermissao> GetTcsObjetoPermissaoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsObjetoPermissaoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsObjetoPermissao), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsObjetoPermissaoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsObjetoPermissao> GetTcsObjetoPermissaoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsObjetoPermissaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsObjetoPermissao), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsObjetoPermissaoToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsObjetoPermissaoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsObjetoPermissao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsObjetoPermissaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsObjetoPermissao asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Objeto.TcsObjetoPermissao");
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
               return ExcelExportPagination<BusinessNS.TcsObjetoPermissao>.CreateExcelDocumentFileMapPath("TcsObjetoPermissao",new ExcelExportPagination<BusinessNS.TcsObjetoPermissao>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsObjetoPermissaoToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsObjetoPermissaoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsObjetoPermissao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsObjetoPermissaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Objeto.TcsObjetoPermissao", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ObjetoDataSource", DataSourceObject = "GetTcsObjetoPermissao", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsObjetoPermissao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsObjetoPermissao> GetSampleTcsObjetoPermissao(string details)
        {
            var result = repository.Context.GetTcsObjetoPermissaoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsObjetoPermissaoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsObjetoPermissaoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsObjetoPermissao), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsObjetoPermissaoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsObjetoPermissao> GetTcsObjetoPermissaoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsObjetoPermissaoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsUsuario"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuario> GetTcsUsuario()
        {
            return repository.Context.GetTcsUsuario();
        }
        
        [Route("GetTcsUsuarioNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuario> GetTcsUsuarioNoAssociations()
        {
            return repository.Context.GetTcsUsuarioNoAssociations();
        }
        
        [Route("GetTcsUsuarioByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuario> GetTcsUsuarioByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsUsuarioByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuario), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsUsuarioByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuario> GetTcsUsuarioByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsUsuarioByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuario), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsUsuarioToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsUsuarioToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuario), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsUsuarioByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdUsuario asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Objeto.TcsUsuario");
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
               return ExcelExportPagination<BusinessNS.TcsUsuario>.CreateExcelDocumentFileMapPath("TcsUsuario",new ExcelExportPagination<BusinessNS.TcsUsuario>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsUsuarioToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsUsuarioToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuario), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsUsuarioByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Objeto.TcsUsuario", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ObjetoDataSource", DataSourceObject = "GetTcsUsuario", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsUsuario"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuario> GetSampleTcsUsuario(string details)
        {
            var result = repository.Context.GetTcsUsuarioByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsUsuarioEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsUsuarioEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuario), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsUsuarioByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuario> GetTcsUsuarioByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsUsuarioByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetLayoutInfo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LayoutInfo> GetLayoutInfo()
        {
            return repository.Context.GetLayoutInfo().AsQueryable();
        }
        
        [Route("GetLayoutInfoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LayoutInfo> GetLayoutInfoNoAssociations()
        {
            return repository.Context.GetLayoutInfoNoAssociations().AsQueryable();
        }
        
        [Route("GetLayoutInfoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LayoutInfo> GetLayoutInfoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetLayoutInfoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LayoutInfo), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        
        [Route("GetLayoutInfoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LayoutInfo> GetLayoutInfoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetLayoutInfoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LayoutInfo), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        [Route("GetLayoutInfoToExcel"), System.Web.Http.HttpPost()]
        public string GetLayoutInfoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LayoutInfo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetLayoutInfoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).AsQueryable().OrderBy("Id asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Objeto.LayoutInfo");
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
               return ExcelExportPagination<BusinessNS.LayoutInfo>.CreateExcelDocumentFileMapPath("LayoutInfo",new ExcelExportPagination<BusinessNS.LayoutInfo>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetLayoutInfoToReportXml"), System.Web.Http.HttpPost()]
        public string GetLayoutInfoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LayoutInfo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetLayoutInfoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Objeto.LayoutInfo", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ObjetoDataSource", DataSourceObject = "GetLayoutInfo", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleLayoutInfo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LayoutInfo> GetSampleLayoutInfo(string details)
        {
            var result = repository.Context.GetLayoutInfoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddLayoutInfoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddLayoutInfoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LayoutInfo), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetLayoutInfoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LayoutInfo> GetLayoutInfoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetLayoutInfoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        #endregion
        
        #region Get Business Entities By Parent Composition
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
    
    public partial class LinxFrameworkObjetoFeedController : ODataController
    {
        private BusinessNS.ObjetoDomainService _context;
        public BusinessNS.ObjetoDomainService Context { get {  if (_context == null) { _context = new BusinessNS.ObjetoDomainService(); _context.IsSecure = true; } return _context; }  }
        
        #region Get Action to Business Entities
        
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsObjeto> GetTcsObjetoById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetTcsObjetoByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsObjeto[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsObjeto>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsObjeto> GetTcsObjetoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsObjetoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsObjeto), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsObjeto>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsObjeto> GetTcsObjeto()
        {
            return this.Context.GetTcsObjetoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacao> GetTcsObjeto__TcsTransacao(Int64 key0, string navigation)
        {
            var entity = this.Context.GetTcsObjetoByKey(key0);
            if (entity != null && navigation == "TcsTransacaoList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsTransacao" });
               return entity.TcsTransacaoList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsTransacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsObjetoConteudoMnt> GetTcsObjetoConteudoMntById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetTcsObjetoConteudoMntByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsObjetoConteudoMnt[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsObjetoConteudoMnt>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsObjetoConteudoMnt> GetTcsObjetoConteudoMntByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsObjetoConteudoMntByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsObjetoConteudoMnt), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsObjetoConteudoMnt>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsObjetoConteudoMnt> GetTcsObjetoConteudoMnt()
        {
            return this.Context.GetTcsObjetoConteudoMntByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacao> GetTcsTransacaoById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetTcsTransacaoByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsTransacao[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsTransacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacao> GetTcsTransacaoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsTransacaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacao), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsTransacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacao> GetTcsTransacao()
        {
            return this.Context.GetTcsTransacaoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsObjeto> GetTcsTransacao__TcsObjeto(Int64 key0, string navigation)
        {
            var entity = this.Context.GetTcsTransacaoByKey(key0);
            if (entity != null && navigation == "TcsObjeto")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsObjeto[] { entity.TcsObjeto }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsObjeto>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.ConfiguracaoExportacao> GetConfiguracaoExportacaoById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetConfiguracaoExportacaoByKey(key0);
            if (entity != null)
               return (new BusinessNS.ConfiguracaoExportacao[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.ConfiguracaoExportacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.ConfiguracaoExportacao> GetConfiguracaoExportacaoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetConfiguracaoExportacaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.ConfiguracaoExportacao), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.ConfiguracaoExportacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.ConfiguracaoExportacao> GetConfiguracaoExportacao()
        {
            return this.Context.GetConfiguracaoExportacaoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsObjetoPermissao> GetTcsObjetoPermissaoById([FromODataUri]Int32 key0)
        {
            var entity = this.Context.GetTcsObjetoPermissaoByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsObjetoPermissao[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsObjetoPermissao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsObjetoPermissao> GetTcsObjetoPermissaoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsObjetoPermissaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsObjetoPermissao), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsObjetoPermissao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsObjetoPermissao> GetTcsObjetoPermissao()
        {
            return this.Context.GetTcsObjetoPermissaoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuario> GetTcsUsuarioById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetTcsUsuarioByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsUsuario[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsUsuario>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuario> GetTcsUsuarioByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsUsuarioByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuario), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsUsuario>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuario> GetTcsUsuario()
        {
            return this.Context.GetTcsUsuarioByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.LayoutInfo> GetLayoutInfoById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetLayoutInfoByKey(key0);
            if (entity != null)
               return (new BusinessNS.LayoutInfo[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.LayoutInfo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.LayoutInfo> GetLayoutInfoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetLayoutInfoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LayoutInfo), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.LayoutInfo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.LayoutInfo> GetLayoutInfo()
        {
            return this.Context.GetLayoutInfoByEntitySearchNoAssociations(null).AsQueryable();
        }
        #endregion
        
    }
    
    public partial class LinxFrameworkObjetoControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return true;
        }
    }
}

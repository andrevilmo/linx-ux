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
using BusinessNS = Linx.Framework.BV.TransacaoAutorizacao;

namespace Linx.Framework.BV.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/LinxFrameworkTransacaoAutorizacao/[ActionName]
    // Security Information Call: http://localhost:1710/LinxFrameworkTransacaoAutorizacao/GetSecurityInfo
    // Entities Catalog Call: http://localhost:1710/LinxFrameworkTransacaoAutorizacao/GetEntities
    // Entity MetaData Call: http://localhost:1710/LinxFrameworkTransacaoAutorizacao/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/LinxFrameworkTransacaoAutorizacao/GetClientDomains?erp=true
    // Client Service Call: http://localhost:1710/LinxFrameworkTransacaoAutorizacao/GetClientService?erp=true
    // Client Factory Call: http://localhost:1710/LinxFrameworkTransacaoAutorizacao/GetClientFactory?entityName=[EntityName]&erp=true
    // Client Factory Custom Events Call: http://localhost:1710/LinxFrameworkTransacaoAutorizacao/GetClientFactoryCustomEvents?entityName=[EntityName]&erp=true
    // Help Call: http://localhost:1710/HelpController/LinxFrameworkTransacaoAutorizacao
    // Feed OData Call: http://localhost:1710/LinxFrameworkTransacaoAutorizacaoOData
    [RoutePrefix("LinxFrameworkTransacaoAutorizacao")]
    [Breeze.WebApi2.BreezeController]
    public partial class LinxFrameworkTransacaoAutorizacaoController : ApiController
    {
        private const int maxObjectExcelReturned = 2300000;
        private DataServiceRepository<BusinessNS.TransacaoAutorizacaoDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.TransacaoAutorizacaoDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.TransacaoAutorizacaoDomainService>(typeof(BusinessNS.TcsTransacaoAutorizacao), typeof(BusinessNS.TcsTransacaoDependenteAutorizacao), typeof(BusinessNS.TcsTransacaoMenuAutorizacao)); _repository.Context.IsSecure = true; } return _repository; } }
        public LinxFrameworkTransacaoAutorizacaoController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(LinxFrameworkTransacaoAutorizacaoController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.TransacaoAutorizacaoDomainService).Assembly.FullName,
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
            var result = repository.Context.GetMetaDataObject("Linx.Framework.BV.TransacaoAutorizacao." + entityName, false, true);
            return (allComposition ? result : result.Where(e => e.ClassName == entityName).ToList());
        }
        
        [Route("GetSecurityInfo"), System.Web.Http.HttpGet()]
        public string[] GetSecurityInfo()
        {
           return new string[] { "Linx.Framework.BV", "LinxFrameworkTransacaoAutorizacao", "LinxFrameworkTransacaoAutorizacao/ActionName" };
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
        
        [Route("GetAllLookUpTcsObjetoAutorizacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsObjetoAutorizacao> GetAllLookUpTcsObjetoAutorizacao()
        {
            return repository.Context.GetAllLookUpTcsObjetoAutorizacao();
        }
        
        [Route("GetLookUpTcsObjetoAutorizacaoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsObjetoAutorizacao> GetLookUpTcsObjetoAutorizacaoByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsObjetoAutorizacaoByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [Route("GetAllLookUpTcsModuloMenuAutorizacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsModuloMenuAutorizacao> GetAllLookUpTcsModuloMenuAutorizacao()
        {
            return repository.Context.GetAllLookUpTcsModuloMenuAutorizacao();
        }
        
        [Route("GetLookUpTcsModuloMenuAutorizacaoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsModuloMenuAutorizacao> GetLookUpTcsModuloMenuAutorizacaoByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsModuloMenuAutorizacaoByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [Route("GetAllLookUpTcsTransacaoDependente"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsTransacaoDependente> GetAllLookUpTcsTransacaoDependente()
        {
            return repository.Context.GetAllLookUpTcsTransacaoDependente();
        }
        
        [Route("GetLookUpTcsTransacaoDependenteByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsTransacaoDependente> GetLookUpTcsTransacaoDependenteByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsTransacaoDependenteByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [Route("GetAllLookUpTcsModuloAutorizacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsModuloAutorizacao> GetAllLookUpTcsModuloAutorizacao()
        {
            return repository.Context.GetAllLookUpTcsModuloAutorizacao();
        }
        
        [Route("GetLookUpTcsModuloAutorizacaoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsModuloAutorizacao> GetLookUpTcsModuloAutorizacaoByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsModuloAutorizacaoByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
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
        
        [Route("GetTcsTransacaoAutorizacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoAutorizacao> GetTcsTransacaoAutorizacao()
        {
            return repository.Context.GetTcsTransacaoAutorizacao();
        }
        
        [Route("GetTcsTransacaoAutorizacaoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoAutorizacao> GetTcsTransacaoAutorizacaoNoAssociations()
        {
            return repository.Context.GetTcsTransacaoAutorizacaoNoAssociations();
        }
        
        [Route("GetTcsTransacaoAutorizacaoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoAutorizacao> GetTcsTransacaoAutorizacaoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsTransacaoAutorizacaoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoAutorizacao), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsTransacaoAutorizacaoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoAutorizacao> GetTcsTransacaoAutorizacaoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsTransacaoAutorizacaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoAutorizacao), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsTransacaoAutorizacaoToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsTransacaoAutorizacaoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoAutorizacao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsTransacaoAutorizacaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTransacao asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.TransacaoAutorizacao.TcsTransacaoAutorizacao");
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
               return ExcelExportPagination<BusinessNS.TcsTransacaoAutorizacao>.CreateExcelDocumentFileMapPath("TcsTransacaoAutorizacao",new ExcelExportPagination<BusinessNS.TcsTransacaoAutorizacao>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsTransacaoAutorizacaoToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsTransacaoAutorizacaoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoAutorizacao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsTransacaoAutorizacaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.TransacaoAutorizacao.TcsTransacaoAutorizacao", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.TransacaoAutorizacaoDataSource", DataSourceObject = "GetTcsTransacaoAutorizacao", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsTransacaoAutorizacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoAutorizacao> GetSampleTcsTransacaoAutorizacao(string details)
        {
            var result = repository.Context.GetTcsTransacaoAutorizacaoByEntitySearchNoAssociations(null).Take(100).ToList();
               if (!details.IsNullOrEmpty())
               {
                   foreach(var entity in result)
                   {
                       entity.FillDetails(repository.Context, null, null, details.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries), 5);
                   }
               }
            return result.AsQueryable();
        }
        
        [Route("AddTcsTransacaoAutorizacaoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsTransacaoAutorizacaoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoAutorizacao), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsTransacaoAutorizacaoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoAutorizacao> GetTcsTransacaoAutorizacaoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsTransacaoAutorizacaoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsTransacaoMenuAutorizacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoMenuAutorizacao> GetTcsTransacaoMenuAutorizacao()
        {
            return repository.Context.GetTcsTransacaoMenuAutorizacao();
        }
        
        [Route("GetTcsTransacaoMenuAutorizacaoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoMenuAutorizacao> GetTcsTransacaoMenuAutorizacaoNoAssociations()
        {
            return repository.Context.GetTcsTransacaoMenuAutorizacaoNoAssociations();
        }
        
        [Route("GetTcsTransacaoMenuAutorizacaoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoMenuAutorizacao> GetTcsTransacaoMenuAutorizacaoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsTransacaoMenuAutorizacaoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoMenuAutorizacao), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsTransacaoMenuAutorizacaoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoMenuAutorizacao> GetTcsTransacaoMenuAutorizacaoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsTransacaoMenuAutorizacaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoMenuAutorizacao), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsTransacaoMenuAutorizacaoToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsTransacaoMenuAutorizacaoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoMenuAutorizacao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsTransacaoMenuAutorizacaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsTransacaoMenuAutorizacao asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.TransacaoAutorizacao.TcsTransacaoMenuAutorizacao");
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
               return ExcelExportPagination<BusinessNS.TcsTransacaoMenuAutorizacao>.CreateExcelDocumentFileMapPath("TcsTransacaoMenuAutorizacao",new ExcelExportPagination<BusinessNS.TcsTransacaoMenuAutorizacao>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsTransacaoMenuAutorizacaoToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsTransacaoMenuAutorizacaoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoMenuAutorizacao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsTransacaoMenuAutorizacaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.TransacaoAutorizacao.TcsTransacaoMenuAutorizacao", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.TransacaoAutorizacaoDataSource", DataSourceObject = "GetTcsTransacaoMenuAutorizacao", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsTransacaoMenuAutorizacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoMenuAutorizacao> GetSampleTcsTransacaoMenuAutorizacao(string details)
        {
            var result = repository.Context.GetTcsTransacaoMenuAutorizacaoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsTransacaoMenuAutorizacaoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsTransacaoMenuAutorizacaoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoMenuAutorizacao), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsTransacaoMenuAutorizacaoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoMenuAutorizacao> GetTcsTransacaoMenuAutorizacaoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsTransacaoMenuAutorizacaoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsTransacaoDependenteAutorizacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoDependenteAutorizacao> GetTcsTransacaoDependenteAutorizacao()
        {
            return repository.Context.GetTcsTransacaoDependenteAutorizacao();
        }
        
        [Route("GetTcsTransacaoDependenteAutorizacaoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoDependenteAutorizacao> GetTcsTransacaoDependenteAutorizacaoNoAssociations()
        {
            return repository.Context.GetTcsTransacaoDependenteAutorizacaoNoAssociations();
        }
        
        [Route("GetTcsTransacaoDependenteAutorizacaoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoDependenteAutorizacao> GetTcsTransacaoDependenteAutorizacaoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsTransacaoDependenteAutorizacaoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoDependenteAutorizacao), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsTransacaoDependenteAutorizacaoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoDependenteAutorizacao> GetTcsTransacaoDependenteAutorizacaoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsTransacaoDependenteAutorizacaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoDependenteAutorizacao), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsTransacaoDependenteAutorizacaoToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsTransacaoDependenteAutorizacaoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoDependenteAutorizacao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsTransacaoDependenteAutorizacaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTransacaoDependente asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.TransacaoAutorizacao.TcsTransacaoDependenteAutorizacao");
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
               return ExcelExportPagination<BusinessNS.TcsTransacaoDependenteAutorizacao>.CreateExcelDocumentFileMapPath("TcsTransacaoDependenteAutorizacao",new ExcelExportPagination<BusinessNS.TcsTransacaoDependenteAutorizacao>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsTransacaoDependenteAutorizacaoToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsTransacaoDependenteAutorizacaoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoDependenteAutorizacao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsTransacaoDependenteAutorizacaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.TransacaoAutorizacao.TcsTransacaoDependenteAutorizacao", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.TransacaoAutorizacaoDataSource", DataSourceObject = "GetTcsTransacaoDependenteAutorizacao", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsTransacaoDependenteAutorizacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoDependenteAutorizacao> GetSampleTcsTransacaoDependenteAutorizacao(string details)
        {
            var result = repository.Context.GetTcsTransacaoDependenteAutorizacaoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsTransacaoDependenteAutorizacaoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsTransacaoDependenteAutorizacaoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoDependenteAutorizacao), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsTransacaoDependenteAutorizacaoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoDependenteAutorizacao> GetTcsTransacaoDependenteAutorizacaoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsTransacaoDependenteAutorizacaoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        #endregion
        
        #region Get Business Entities By Parent Composition
        
        [Route("GetTcsTransacaoMenuAutorizacaoParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoMenuAutorizacaoParentComposition> GetTcsTransacaoMenuAutorizacaoParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsTransacaoMenuAutorizacaoParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoMenuAutorizacaoParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsTransacaoMenuAutorizacaoParentCompositionToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsTransacaoMenuAutorizacaoParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("TcsTransacaoMenuAutorizacao{", "TcsTransacaoMenuAutorizacaoParentComposition{");
            jEntitySearch = jEntitySearch.Replace("TcsTransacaoAutorizacao{", "TcsTransacaoMenuAutorizacaoParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoMenuAutorizacaoParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsTransacaoMenuAutorizacaoParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsTransacaoMenuAutorizacao asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.TransacaoAutorizacao.TcsTransacaoMenuAutorizacao");
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
               return ExcelExportPagination<BusinessNS.TcsTransacaoMenuAutorizacaoParentComposition>.CreateExcelDocumentFileMapPath("TcsTransacaoMenuAutorizacao",new ExcelExportPagination<BusinessNS.TcsTransacaoMenuAutorizacaoParentComposition>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsTransacaoMenuAutorizacaoParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsTransacaoMenuAutorizacaoParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoMenuAutorizacaoParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsTransacaoMenuAutorizacaoParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.TransacaoAutorizacao.TcsTransacaoMenuAutorizacao", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.TransacaoAutorizacaoDataSource", DataSourceObject = "GetTcsTransacaoMenuAutorizacaoParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsTransacaoMenuAutorizacaoParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoMenuAutorizacaoParentComposition> GetSampleTcsTransacaoMenuAutorizacaoParentComposition(string details)
        {
            var result = repository.Context.GetTcsTransacaoMenuAutorizacaoParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("GetTcsTransacaoDependenteAutorizacaoParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoDependenteAutorizacaoParentComposition> GetTcsTransacaoDependenteAutorizacaoParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsTransacaoDependenteAutorizacaoParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoDependenteAutorizacaoParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsTransacaoDependenteAutorizacaoParentCompositionToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsTransacaoDependenteAutorizacaoParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("TcsTransacaoDependenteAutorizacao{", "TcsTransacaoDependenteAutorizacaoParentComposition{");
            jEntitySearch = jEntitySearch.Replace("TcsTransacaoAutorizacao{", "TcsTransacaoDependenteAutorizacaoParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoDependenteAutorizacaoParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsTransacaoDependenteAutorizacaoParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTransacaoDependente asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.TransacaoAutorizacao.TcsTransacaoDependenteAutorizacao");
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
               return ExcelExportPagination<BusinessNS.TcsTransacaoDependenteAutorizacaoParentComposition>.CreateExcelDocumentFileMapPath("TcsTransacaoDependenteAutorizacao",new ExcelExportPagination<BusinessNS.TcsTransacaoDependenteAutorizacaoParentComposition>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsTransacaoDependenteAutorizacaoParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsTransacaoDependenteAutorizacaoParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoDependenteAutorizacaoParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsTransacaoDependenteAutorizacaoParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.TransacaoAutorizacao.TcsTransacaoDependenteAutorizacao", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.TransacaoAutorizacaoDataSource", DataSourceObject = "GetTcsTransacaoDependenteAutorizacaoParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsTransacaoDependenteAutorizacaoParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoDependenteAutorizacaoParentComposition> GetSampleTcsTransacaoDependenteAutorizacaoParentComposition(string details)
        {
            var result = repository.Context.GetTcsTransacaoDependenteAutorizacaoParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
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
    
    public partial class LinxFrameworkTransacaoAutorizacaoFeedController : ODataController
    {
        private BusinessNS.TransacaoAutorizacaoDomainService _context;
        public BusinessNS.TransacaoAutorizacaoDomainService Context { get {  if (_context == null) { _context = new BusinessNS.TransacaoAutorizacaoDomainService(); _context.IsSecure = true; } return _context; }  }
        
        #region Get Action to Business Entities
        
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacaoAutorizacao> GetTcsTransacaoAutorizacaoById([FromODataUri]long key0)
        {
            var entity = this.Context.GetTcsTransacaoAutorizacaoByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsTransacaoAutorizacao[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsTransacaoAutorizacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacaoAutorizacao> GetTcsTransacaoAutorizacaoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsTransacaoAutorizacaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoAutorizacao), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsTransacaoAutorizacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacaoAutorizacao> GetTcsTransacaoAutorizacao()
        {
            return this.Context.GetTcsTransacaoAutorizacaoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacaoMenuAutorizacao> GetTcsTransacaoAutorizacao__TcsTransacaoMenuAutorizacao(long key0, string navigation)
        {
            var entity = this.Context.GetTcsTransacaoAutorizacaoByKey(key0);
            if (entity != null && navigation == "TcsTransacaoMenuAutorizacaoList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsTransacaoMenuAutorizacao" });
               return entity.TcsTransacaoMenuAutorizacaoList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsTransacaoMenuAutorizacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacaoDependenteAutorizacao> GetTcsTransacaoAutorizacao__TcsTransacaoDependenteAutorizacao(long key0, string navigation)
        {
            var entity = this.Context.GetTcsTransacaoAutorizacaoByKey(key0);
            if (entity != null && navigation == "TcsTransacaoDependenteAutorizacaoList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsTransacaoDependenteAutorizacao" });
               return entity.TcsTransacaoDependenteAutorizacaoList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsTransacaoDependenteAutorizacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacaoMenuAutorizacao> GetTcsTransacaoMenuAutorizacaoById([FromODataUri]int key0)
        {
            var entity = this.Context.GetTcsTransacaoMenuAutorizacaoByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsTransacaoMenuAutorizacao[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsTransacaoMenuAutorizacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacaoMenuAutorizacao> GetTcsTransacaoMenuAutorizacaoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsTransacaoMenuAutorizacaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoMenuAutorizacao), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsTransacaoMenuAutorizacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacaoMenuAutorizacao> GetTcsTransacaoMenuAutorizacao()
        {
            return this.Context.GetTcsTransacaoMenuAutorizacaoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacaoMenuAutorizacaoParentComposition> GetTcsTransacaoMenuAutorizacaoParentComposition()
        {
            return this.Context.GetTcsTransacaoMenuAutorizacaoParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacaoMenuAutorizacaoParentComposition> GetTcsTransacaoMenuAutorizacaoParentCompositionByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                jEntitySearch = jEntitySearch.Replace("TcsTransacaoMenuAutorizacao{", "TcsTransacaoMenuAutorizacaoParentComposition{");
                jEntitySearch = jEntitySearch.Replace("TcsTransacaoAutorizacao{", "TcsTransacaoMenuAutorizacaoParentComposition{");
                var entity = this.Context.GetTcsTransacaoMenuAutorizacaoParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoMenuAutorizacaoParentComposition), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsTransacaoMenuAutorizacaoParentComposition>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacaoAutorizacao> GetTcsTransacaoMenuAutorizacao__TcsTransacaoAutorizacao(int key0, string navigation)
        {
            var entity = this.Context.GetTcsTransacaoMenuAutorizacaoByKey(key0);
            if (entity != null && navigation == "TcsTransacaoAutorizacao")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsTransacaoAutorizacao[] { entity.TcsTransacaoAutorizacao }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsTransacaoAutorizacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacaoDependenteAutorizacao> GetTcsTransacaoDependenteAutorizacaoById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetTcsTransacaoDependenteAutorizacaoByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsTransacaoDependenteAutorizacao[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsTransacaoDependenteAutorizacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacaoDependenteAutorizacao> GetTcsTransacaoDependenteAutorizacaoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsTransacaoDependenteAutorizacaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoDependenteAutorizacao), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsTransacaoDependenteAutorizacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacaoDependenteAutorizacao> GetTcsTransacaoDependenteAutorizacao()
        {
            return this.Context.GetTcsTransacaoDependenteAutorizacaoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacaoDependenteAutorizacaoParentComposition> GetTcsTransacaoDependenteAutorizacaoParentComposition()
        {
            return this.Context.GetTcsTransacaoDependenteAutorizacaoParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacaoDependenteAutorizacaoParentComposition> GetTcsTransacaoDependenteAutorizacaoParentCompositionByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                jEntitySearch = jEntitySearch.Replace("TcsTransacaoDependenteAutorizacao{", "TcsTransacaoDependenteAutorizacaoParentComposition{");
                jEntitySearch = jEntitySearch.Replace("TcsTransacaoAutorizacao{", "TcsTransacaoDependenteAutorizacaoParentComposition{");
                var entity = this.Context.GetTcsTransacaoDependenteAutorizacaoParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoDependenteAutorizacaoParentComposition), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsTransacaoDependenteAutorizacaoParentComposition>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacaoAutorizacao> GetTcsTransacaoDependenteAutorizacao__TcsTransacaoAutorizacao(Int64 key0, string navigation)
        {
            var entity = this.Context.GetTcsTransacaoDependenteAutorizacaoByKey(key0);
            if (entity != null && navigation == "TcsTransacaoAutorizacao")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsTransacaoAutorizacao[] { entity.TcsTransacaoAutorizacao }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsTransacaoAutorizacao>);
        }
        #endregion
        
    }
    
    public partial class LinxFrameworkTransacaoAutorizacaoControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return true;
        }
    }
}

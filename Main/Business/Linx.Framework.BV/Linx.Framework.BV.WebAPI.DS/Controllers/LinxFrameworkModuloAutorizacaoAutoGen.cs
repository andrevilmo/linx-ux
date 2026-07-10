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
using BusinessNS = Linx.Framework.BV.ModuloAutorizacao;

namespace Linx.Framework.BV.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/LinxFrameworkModuloAutorizacao/[ActionName]
    // Security Information Call: http://localhost:1710/LinxFrameworkModuloAutorizacao/GetSecurityInfo
    // Entities Catalog Call: http://localhost:1710/LinxFrameworkModuloAutorizacao/GetEntities
    // Entity MetaData Call: http://localhost:1710/LinxFrameworkModuloAutorizacao/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/LinxFrameworkModuloAutorizacao/GetClientDomains?erp=true
    // Client Service Call: http://localhost:1710/LinxFrameworkModuloAutorizacao/GetClientService?erp=true
    // Client Factory Call: http://localhost:1710/LinxFrameworkModuloAutorizacao/GetClientFactory?entityName=[EntityName]&erp=true
    // Client Factory Custom Events Call: http://localhost:1710/LinxFrameworkModuloAutorizacao/GetClientFactoryCustomEvents?entityName=[EntityName]&erp=true
    // Help Call: http://localhost:1710/HelpController/LinxFrameworkModuloAutorizacao
    // Feed OData Call: http://localhost:1710/LinxFrameworkModuloAutorizacaoOData
    [RoutePrefix("LinxFrameworkModuloAutorizacao")]
    [Breeze.WebApi2.BreezeController]
    public partial class LinxFrameworkModuloAutorizacaoController : ApiController
    {
        private const int maxObjectExcelReturned = 2300000;
        private DataServiceRepository<BusinessNS.ModuloAutorizacaoDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.ModuloAutorizacaoDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.ModuloAutorizacaoDomainService>(typeof(BusinessNS.TcsModuloAutorizacao), typeof(BusinessNS.TcsModuloMenuAutorizacao), typeof(BusinessNS.TcsTransacaoMenuAutorizacaoModulo)); _repository.Context.IsSecure = true; } return _repository; } }
        public LinxFrameworkModuloAutorizacaoController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(LinxFrameworkModuloAutorizacaoController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.ModuloAutorizacaoDomainService).Assembly.FullName,
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
            var result = repository.Context.GetMetaDataObject("Linx.Framework.BV.ModuloAutorizacao." + entityName, false, true);
            return (allComposition ? result : result.Where(e => e.ClassName == entityName).ToList());
        }
        
        [Route("GetSecurityInfo"), System.Web.Http.HttpGet()]
        public string[] GetSecurityInfo()
        {
           return new string[] { "Linx.Framework.BV", "LinxFrameworkModuloAutorizacao", "LinxFrameworkModuloAutorizacao/ActionName" };
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
        
        [Route("GetAllLookUpModuloMenuSuperior"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpModuloMenuSuperior> GetAllLookUpModuloMenuSuperior()
        {
            return repository.Context.GetAllLookUpModuloMenuSuperior();
        }
        
        [Route("GetLookUpModuloMenuSuperiorByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpModuloMenuSuperior> GetLookUpModuloMenuSuperiorByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpModuloMenuSuperiorByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [Route("GetAllLookUpTcsTransacaoAutorizacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsTransacaoAutorizacao> GetAllLookUpTcsTransacaoAutorizacao()
        {
            return repository.Context.GetAllLookUpTcsTransacaoAutorizacao();
        }
        
        [Route("GetLookUpTcsTransacaoAutorizacaoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsTransacaoAutorizacao> GetLookUpTcsTransacaoAutorizacaoByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsTransacaoAutorizacaoByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
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
        
        [Route("GetTcsModuloAutorizacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloAutorizacao> GetTcsModuloAutorizacao()
        {
            return repository.Context.GetTcsModuloAutorizacao();
        }
        
        [Route("GetTcsModuloAutorizacaoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloAutorizacao> GetTcsModuloAutorizacaoNoAssociations()
        {
            return repository.Context.GetTcsModuloAutorizacaoNoAssociations();
        }
        
        [Route("GetTcsModuloAutorizacaoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloAutorizacao> GetTcsModuloAutorizacaoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsModuloAutorizacaoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloAutorizacao), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsModuloAutorizacaoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloAutorizacao> GetTcsModuloAutorizacaoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsModuloAutorizacaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloAutorizacao), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsModuloAutorizacaoToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsModuloAutorizacaoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloAutorizacao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsModuloAutorizacaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdModulo asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.ModuloAutorizacao.TcsModuloAutorizacao");
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
               return ExcelExportPagination<BusinessNS.TcsModuloAutorizacao>.CreateExcelDocumentFileMapPath("TcsModuloAutorizacao",new ExcelExportPagination<BusinessNS.TcsModuloAutorizacao>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsModuloAutorizacaoToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsModuloAutorizacaoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloAutorizacao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsModuloAutorizacaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.ModuloAutorizacao.TcsModuloAutorizacao", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ModuloAutorizacaoDataSource", DataSourceObject = "GetTcsModuloAutorizacao", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsModuloAutorizacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloAutorizacao> GetSampleTcsModuloAutorizacao(string details)
        {
            var result = repository.Context.GetTcsModuloAutorizacaoByEntitySearchNoAssociations(null).Take(100).ToList();
               if (!details.IsNullOrEmpty())
               {
                   foreach(var entity in result)
                   {
                       entity.FillDetails(repository.Context, null, null, details.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries), 5);
                   }
               }
            return result.AsQueryable();
        }
        
        [Route("AddTcsModuloAutorizacaoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsModuloAutorizacaoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloAutorizacao), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsModuloAutorizacaoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloAutorizacao> GetTcsModuloAutorizacaoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsModuloAutorizacaoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsModuloMenuAutorizacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloMenuAutorizacao> GetTcsModuloMenuAutorizacao()
        {
            return repository.Context.GetTcsModuloMenuAutorizacao();
        }
        
        [Route("GetTcsModuloMenuAutorizacaoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloMenuAutorizacao> GetTcsModuloMenuAutorizacaoNoAssociations()
        {
            return repository.Context.GetTcsModuloMenuAutorizacaoNoAssociations();
        }
        
        [Route("GetTcsModuloMenuAutorizacaoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloMenuAutorizacao> GetTcsModuloMenuAutorizacaoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsModuloMenuAutorizacaoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloMenuAutorizacao), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsModuloMenuAutorizacaoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloMenuAutorizacao> GetTcsModuloMenuAutorizacaoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsModuloMenuAutorizacaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloMenuAutorizacao), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsModuloMenuAutorizacaoToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsModuloMenuAutorizacaoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloMenuAutorizacao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsModuloMenuAutorizacaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdModuloMenu asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.ModuloAutorizacao.TcsModuloMenuAutorizacao");
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
               return ExcelExportPagination<BusinessNS.TcsModuloMenuAutorizacao>.CreateExcelDocumentFileMapPath("TcsModuloMenuAutorizacao",new ExcelExportPagination<BusinessNS.TcsModuloMenuAutorizacao>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsModuloMenuAutorizacaoToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsModuloMenuAutorizacaoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloMenuAutorizacao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsModuloMenuAutorizacaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.ModuloAutorizacao.TcsModuloMenuAutorizacao", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ModuloAutorizacaoDataSource", DataSourceObject = "GetTcsModuloMenuAutorizacao", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsModuloMenuAutorizacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloMenuAutorizacao> GetSampleTcsModuloMenuAutorizacao(string details)
        {
            var result = repository.Context.GetTcsModuloMenuAutorizacaoByEntitySearchNoAssociations(null).Take(100).ToList();
               if (!details.IsNullOrEmpty())
               {
                   foreach(var entity in result)
                   {
                       entity.FillDetails(repository.Context, null, null, details.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries), 5);
                   }
               }
            return result.AsQueryable();
        }
        
        [Route("AddTcsModuloMenuAutorizacaoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsModuloMenuAutorizacaoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloMenuAutorizacao), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsModuloMenuAutorizacaoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloMenuAutorizacao> GetTcsModuloMenuAutorizacaoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsModuloMenuAutorizacaoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsTransacaoMenuAutorizacaoModulo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoMenuAutorizacaoModulo> GetTcsTransacaoMenuAutorizacaoModulo()
        {
            return repository.Context.GetTcsTransacaoMenuAutorizacaoModulo();
        }
        
        [Route("GetTcsTransacaoMenuAutorizacaoModuloNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoMenuAutorizacaoModulo> GetTcsTransacaoMenuAutorizacaoModuloNoAssociations()
        {
            return repository.Context.GetTcsTransacaoMenuAutorizacaoModuloNoAssociations();
        }
        
        [Route("GetTcsTransacaoMenuAutorizacaoModuloByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoMenuAutorizacaoModulo> GetTcsTransacaoMenuAutorizacaoModuloByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsTransacaoMenuAutorizacaoModuloByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoMenuAutorizacaoModulo), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsTransacaoMenuAutorizacaoModuloByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoMenuAutorizacaoModulo> GetTcsTransacaoMenuAutorizacaoModuloByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsTransacaoMenuAutorizacaoModuloByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoMenuAutorizacaoModulo), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsTransacaoMenuAutorizacaoModuloToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsTransacaoMenuAutorizacaoModuloToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoMenuAutorizacaoModulo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsTransacaoMenuAutorizacaoModuloByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsTransacaoMenuAutorizacao asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.ModuloAutorizacao.TcsTransacaoMenuAutorizacaoModulo");
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
               return ExcelExportPagination<BusinessNS.TcsTransacaoMenuAutorizacaoModulo>.CreateExcelDocumentFileMapPath("TcsTransacaoMenuAutorizacaoModulo",new ExcelExportPagination<BusinessNS.TcsTransacaoMenuAutorizacaoModulo>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsTransacaoMenuAutorizacaoModuloToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsTransacaoMenuAutorizacaoModuloToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoMenuAutorizacaoModulo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsTransacaoMenuAutorizacaoModuloByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.ModuloAutorizacao.TcsTransacaoMenuAutorizacaoModulo", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ModuloAutorizacaoDataSource", DataSourceObject = "GetTcsTransacaoMenuAutorizacaoModulo", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsTransacaoMenuAutorizacaoModulo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoMenuAutorizacaoModulo> GetSampleTcsTransacaoMenuAutorizacaoModulo(string details)
        {
            var result = repository.Context.GetTcsTransacaoMenuAutorizacaoModuloByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsTransacaoMenuAutorizacaoModuloEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsTransacaoMenuAutorizacaoModuloEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoMenuAutorizacaoModulo), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsTransacaoMenuAutorizacaoModuloByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoMenuAutorizacaoModulo> GetTcsTransacaoMenuAutorizacaoModuloByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsTransacaoMenuAutorizacaoModuloByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
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
    
    public partial class LinxFrameworkModuloAutorizacaoFeedController : ODataController
    {
        private BusinessNS.ModuloAutorizacaoDomainService _context;
        public BusinessNS.ModuloAutorizacaoDomainService Context { get {  if (_context == null) { _context = new BusinessNS.ModuloAutorizacaoDomainService(); _context.IsSecure = true; } return _context; }  }
        
        #region Get Action to Business Entities
        
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsModuloAutorizacao> GetTcsModuloAutorizacaoById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetTcsModuloAutorizacaoByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsModuloAutorizacao[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsModuloAutorizacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsModuloAutorizacao> GetTcsModuloAutorizacaoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsModuloAutorizacaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloAutorizacao), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsModuloAutorizacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsModuloAutorizacao> GetTcsModuloAutorizacao()
        {
            return this.Context.GetTcsModuloAutorizacaoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsModuloMenuAutorizacao> GetTcsModuloAutorizacao__TcsModuloMenuAutorizacao(Int64 key0, string navigation)
        {
            var entity = this.Context.GetTcsModuloAutorizacaoByKey(key0);
            if (entity != null && navigation == "TcsModuloMenuAutorizacaoList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsModuloMenuAutorizacao" });
               return entity.TcsModuloMenuAutorizacaoList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsModuloMenuAutorizacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsModuloMenuAutorizacao> GetTcsModuloMenuAutorizacaoById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetTcsModuloMenuAutorizacaoByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsModuloMenuAutorizacao[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsModuloMenuAutorizacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsModuloMenuAutorizacao> GetTcsModuloMenuAutorizacaoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsModuloMenuAutorizacaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloMenuAutorizacao), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsModuloMenuAutorizacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsModuloMenuAutorizacao> GetTcsModuloMenuAutorizacao()
        {
            return this.Context.GetTcsModuloMenuAutorizacaoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsModuloAutorizacao> GetTcsModuloMenuAutorizacao__TcsModuloAutorizacao(Int64 key0, string navigation)
        {
            var entity = this.Context.GetTcsModuloMenuAutorizacaoByKey(key0);
            if (entity != null && navigation == "TcsModuloAutorizacao")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsModuloAutorizacao[] { entity.TcsModuloAutorizacao }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsModuloAutorizacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacaoMenuAutorizacaoModulo> GetTcsModuloMenuAutorizacao__TcsTransacaoMenuAutorizacaoModulo(Int64 key0, string navigation)
        {
            var entity = this.Context.GetTcsModuloMenuAutorizacaoByKey(key0);
            if (entity != null && navigation == "TcsTransacaoMenuAutorizacaoModuloList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsTransacaoMenuAutorizacaoModulo" });
               return entity.TcsTransacaoMenuAutorizacaoModuloList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsTransacaoMenuAutorizacaoModulo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacaoMenuAutorizacaoModulo> GetTcsTransacaoMenuAutorizacaoModuloById([FromODataUri]Int32 key0)
        {
            var entity = this.Context.GetTcsTransacaoMenuAutorizacaoModuloByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsTransacaoMenuAutorizacaoModulo[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsTransacaoMenuAutorizacaoModulo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacaoMenuAutorizacaoModulo> GetTcsTransacaoMenuAutorizacaoModuloByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsTransacaoMenuAutorizacaoModuloByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoMenuAutorizacaoModulo), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsTransacaoMenuAutorizacaoModulo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacaoMenuAutorizacaoModulo> GetTcsTransacaoMenuAutorizacaoModulo()
        {
            return this.Context.GetTcsTransacaoMenuAutorizacaoModuloByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsModuloMenuAutorizacao> GetTcsTransacaoMenuAutorizacaoModulo__TcsModuloMenuAutorizacao(Int32 key0, string navigation)
        {
            var entity = this.Context.GetTcsTransacaoMenuAutorizacaoModuloByKey(key0);
            if (entity != null && navigation == "TcsModuloMenuAutorizacao")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsModuloMenuAutorizacao[] { entity.TcsModuloMenuAutorizacao }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsModuloMenuAutorizacao>);
        }
        #endregion
        
    }
    
    public partial class LinxFrameworkModuloAutorizacaoControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return true;
        }
    }
}

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
using BusinessNS = Linx.Framework.BV.Empresa;

namespace Linx.Framework.BV.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/LinxFrameworkEmpresa/[ActionName]
    // Security Information Call: http://localhost:1710/LinxFrameworkEmpresa/GetSecurityInfo
    // Entities Catalog Call: http://localhost:1710/LinxFrameworkEmpresa/GetEntities
    // Entity MetaData Call: http://localhost:1710/LinxFrameworkEmpresa/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/LinxFrameworkEmpresa/GetClientDomains?erp=true
    // Client Service Call: http://localhost:1710/LinxFrameworkEmpresa/GetClientService?erp=true
    // Client Factory Call: http://localhost:1710/LinxFrameworkEmpresa/GetClientFactory?entityName=[EntityName]&erp=true
    // Client Factory Custom Events Call: http://localhost:1710/LinxFrameworkEmpresa/GetClientFactoryCustomEvents?entityName=[EntityName]&erp=true
    // Help Call: http://localhost:1710/HelpController/LinxFrameworkEmpresa
    // Feed OData Call: http://localhost:1710/LinxFrameworkEmpresaOData
    [RoutePrefix("LinxFrameworkEmpresa")]
    [Breeze.WebApi2.BreezeController]
    public partial class LinxFrameworkEmpresaController : ApiController
    {
        private const int maxObjectExcelReturned = 2300000;
        private DataServiceRepository<BusinessNS.EmpresaDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.EmpresaDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.EmpresaDomainService>(typeof(BusinessNS.TcsAmbiente), typeof(BusinessNS.TcsEmpresaAutenticacao), typeof(BusinessNS.TcsEmpresaAutenticacaoP), typeof(BusinessNS.TcsEmpresaGpecon), typeof(BusinessNS.TcsEmpresaGpeconP), typeof(BusinessNS.TcsEmpresaModulo), typeof(BusinessNS.TcsUsuarioAutenticacao)); _repository.Context.IsSecure = true; } return _repository; } }
        public LinxFrameworkEmpresaController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(LinxFrameworkEmpresaController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.EmpresaDomainService).Assembly.FullName,
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
            var result = repository.Context.GetMetaDataObject("Linx.Framework.BV.Empresa." + entityName, false, true);
            return (allComposition ? result : result.Where(e => e.ClassName == entityName).ToList());
        }
        
        [Route("GetSecurityInfo"), System.Web.Http.HttpGet()]
        public string[] GetSecurityInfo()
        {
           return new string[] { "Linx.Framework.BV", "LinxFrameworkEmpresa", "LinxFrameworkEmpresa/ActionName" };
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
        
        [Route("GetAllLookUpTcsEmpresaAutenticacaoGpecon"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsEmpresaAutenticacaoGpecon> GetAllLookUpTcsEmpresaAutenticacaoGpecon()
        {
            return repository.Context.GetAllLookUpTcsEmpresaAutenticacaoGpecon();
        }
        
        [Route("GetLookUpTcsEmpresaAutenticacaoGpeconByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsEmpresaAutenticacaoGpecon> GetLookUpTcsEmpresaAutenticacaoGpeconByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsEmpresaAutenticacaoGpeconByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [Route("GetAllLookUpTcsAplicacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsAplicacao> GetAllLookUpTcsAplicacao()
        {
            return repository.Context.GetAllLookUpTcsAplicacao();
        }
        
        [Route("GetLookUpTcsAplicacaoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsAplicacao> GetLookUpTcsAplicacaoByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsAplicacaoByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
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
        
        [Route("GetAllLookUpTcsAmbiente"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsAmbiente> GetAllLookUpTcsAmbiente()
        {
            return repository.Context.GetAllLookUpTcsAmbiente();
        }
        
        [Route("GetLookUpTcsAmbienteByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsAmbiente> GetLookUpTcsAmbienteByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsAmbienteByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [Route("GetAllLookUpTcsUsuarioAutenticacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsUsuarioAutenticacao> GetAllLookUpTcsUsuarioAutenticacao()
        {
            return repository.Context.GetAllLookUpTcsUsuarioAutenticacao();
        }
        
        [Route("GetLookUpTcsUsuarioAutenticacaoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsUsuarioAutenticacao> GetLookUpTcsUsuarioAutenticacaoByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsUsuarioAutenticacaoByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [Route("GetAllLookUpTcsEmpresaAutenticacaoP"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsEmpresaAutenticacaoP> GetAllLookUpTcsEmpresaAutenticacaoP()
        {
            return repository.Context.GetAllLookUpTcsEmpresaAutenticacaoP();
        }
        
        [LinxFrameworkAutorizacaoControllerAuthorize]
        [Route("GetLookUpTcsEmpresaAutenticacaoPByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsEmpresaAutenticacaoP> GetLookUpTcsEmpresaAutenticacaoPByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsEmpresaAutenticacaoPByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
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
        
        [Route("GetTcsEmpresaAutenticacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsEmpresaAutenticacao> GetTcsEmpresaAutenticacao()
        {
            return repository.Context.GetTcsEmpresaAutenticacao();
        }
        
        [Route("GetTcsEmpresaAutenticacaoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsEmpresaAutenticacao> GetTcsEmpresaAutenticacaoNoAssociations()
        {
            return repository.Context.GetTcsEmpresaAutenticacaoNoAssociations();
        }
        
        [Route("GetTcsEmpresaAutenticacaoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsEmpresaAutenticacao> GetTcsEmpresaAutenticacaoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsEmpresaAutenticacaoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsEmpresaAutenticacao), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsEmpresaAutenticacaoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsEmpresaAutenticacao> GetTcsEmpresaAutenticacaoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsEmpresaAutenticacaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsEmpresaAutenticacao), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsEmpresaAutenticacaoToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsEmpresaAutenticacaoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsEmpresaAutenticacao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsEmpresaAutenticacaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdLinx asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Empresa.TcsEmpresaAutenticacao");
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
               return ExcelExportPagination<BusinessNS.TcsEmpresaAutenticacao>.CreateExcelDocumentFileMapPath("TcsEmpresaAutenticacao",new ExcelExportPagination<BusinessNS.TcsEmpresaAutenticacao>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsEmpresaAutenticacaoToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsEmpresaAutenticacaoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsEmpresaAutenticacao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsEmpresaAutenticacaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Empresa.TcsEmpresaAutenticacao", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.EmpresaDataSource", DataSourceObject = "GetTcsEmpresaAutenticacao", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsEmpresaAutenticacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsEmpresaAutenticacao> GetSampleTcsEmpresaAutenticacao(string details)
        {
            var result = repository.Context.GetTcsEmpresaAutenticacaoByEntitySearchNoAssociations(null).Take(100).ToList();
               if (!details.IsNullOrEmpty())
               {
                   foreach(var entity in result)
                   {
                       entity.FillDetails(repository.Context, null, null, details.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries), 5);
                   }
               }
            return result.AsQueryable();
        }
        
        [Route("AddTcsEmpresaAutenticacaoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsEmpresaAutenticacaoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsEmpresaAutenticacao), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsEmpresaAutenticacaoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsEmpresaAutenticacao> GetTcsEmpresaAutenticacaoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsEmpresaAutenticacaoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsEmpresaGpecon"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsEmpresaGpecon> GetTcsEmpresaGpecon()
        {
            return repository.Context.GetTcsEmpresaGpecon();
        }
        
        [Route("GetTcsEmpresaGpeconNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsEmpresaGpecon> GetTcsEmpresaGpeconNoAssociations()
        {
            return repository.Context.GetTcsEmpresaGpeconNoAssociations();
        }
        
        [Route("GetTcsEmpresaGpeconByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsEmpresaGpecon> GetTcsEmpresaGpeconByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsEmpresaGpeconByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsEmpresaGpecon), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsEmpresaGpeconByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsEmpresaGpecon> GetTcsEmpresaGpeconByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsEmpresaGpeconByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsEmpresaGpecon), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsEmpresaGpeconToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsEmpresaGpeconToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsEmpresaGpecon), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsEmpresaGpeconByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdGrupoEconomico asc, IdLinx asc, IdLinxGpecon asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Empresa.TcsEmpresaGpecon");
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
               return ExcelExportPagination<BusinessNS.TcsEmpresaGpecon>.CreateExcelDocumentFileMapPath("TcsEmpresaGpecon",new ExcelExportPagination<BusinessNS.TcsEmpresaGpecon>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsEmpresaGpeconToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsEmpresaGpeconToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsEmpresaGpecon), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsEmpresaGpeconByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Empresa.TcsEmpresaGpecon", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.EmpresaDataSource", DataSourceObject = "GetTcsEmpresaGpecon", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsEmpresaGpecon"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsEmpresaGpecon> GetSampleTcsEmpresaGpecon(string details)
        {
            var result = repository.Context.GetTcsEmpresaGpeconByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsEmpresaGpeconEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsEmpresaGpeconEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsEmpresaGpecon), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsEmpresaGpeconByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsEmpresaGpecon> GetTcsEmpresaGpeconByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsEmpresaGpeconByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsAmbiente"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbiente> GetTcsAmbiente()
        {
            return repository.Context.GetTcsAmbiente();
        }
        
        [Route("GetTcsAmbienteNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbiente> GetTcsAmbienteNoAssociations()
        {
            return repository.Context.GetTcsAmbienteNoAssociations();
        }
        
        [Route("GetTcsAmbienteByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbiente> GetTcsAmbienteByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsAmbienteByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbiente), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsAmbienteByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbiente> GetTcsAmbienteByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsAmbienteByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbiente), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsAmbienteToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsAmbienteToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbiente), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsAmbienteByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsAmbiente asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Empresa.TcsAmbiente");
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
               return ExcelExportPagination<BusinessNS.TcsAmbiente>.CreateExcelDocumentFileMapPath("TcsAmbiente",new ExcelExportPagination<BusinessNS.TcsAmbiente>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsAmbienteToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsAmbienteToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbiente), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsAmbienteByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Empresa.TcsAmbiente", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.EmpresaDataSource", DataSourceObject = "GetTcsAmbiente", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsAmbiente"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbiente> GetSampleTcsAmbiente(string details)
        {
            var result = repository.Context.GetTcsAmbienteByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsAmbienteEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsAmbienteEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbiente), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsAmbienteByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbiente> GetTcsAmbienteByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsAmbienteByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsEmpresaModulo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsEmpresaModulo> GetTcsEmpresaModulo()
        {
            return repository.Context.GetTcsEmpresaModulo();
        }
        
        [Route("GetTcsEmpresaModuloNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsEmpresaModulo> GetTcsEmpresaModuloNoAssociations()
        {
            return repository.Context.GetTcsEmpresaModuloNoAssociations();
        }
        
        [Route("GetTcsEmpresaModuloByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsEmpresaModulo> GetTcsEmpresaModuloByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsEmpresaModuloByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsEmpresaModulo), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsEmpresaModuloByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsEmpresaModulo> GetTcsEmpresaModuloByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsEmpresaModuloByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsEmpresaModulo), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsEmpresaModuloToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsEmpresaModuloToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsEmpresaModulo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsEmpresaModuloByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsEmpresaModulo asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Empresa.TcsEmpresaModulo");
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
               return ExcelExportPagination<BusinessNS.TcsEmpresaModulo>.CreateExcelDocumentFileMapPath("TcsEmpresaModulo",new ExcelExportPagination<BusinessNS.TcsEmpresaModulo>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsEmpresaModuloToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsEmpresaModuloToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsEmpresaModulo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsEmpresaModuloByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Empresa.TcsEmpresaModulo", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.EmpresaDataSource", DataSourceObject = "GetTcsEmpresaModulo", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsEmpresaModulo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsEmpresaModulo> GetSampleTcsEmpresaModulo(string details)
        {
            var result = repository.Context.GetTcsEmpresaModuloByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsEmpresaModuloEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsEmpresaModuloEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsEmpresaModulo), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsEmpresaModuloByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsEmpresaModulo> GetTcsEmpresaModuloByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsEmpresaModuloByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsUsuarioAutenticacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacao()
        {
            return repository.Context.GetTcsUsuarioAutenticacao();
        }
        
        [Route("GetTcsUsuarioAutenticacaoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacaoNoAssociations()
        {
            return repository.Context.GetTcsUsuarioAutenticacaoNoAssociations();
        }
        
        [Route("GetTcsUsuarioAutenticacaoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacaoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsUsuarioAutenticacaoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioAutenticacao), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioAutenticacao), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsUsuarioAutenticacaoToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsUsuarioAutenticacaoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioAutenticacao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("NomeAutenticacao asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Empresa.TcsUsuarioAutenticacao");
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
               return ExcelExportPagination<BusinessNS.TcsUsuarioAutenticacao>.CreateExcelDocumentFileMapPath("TcsUsuarioAutenticacao",new ExcelExportPagination<BusinessNS.TcsUsuarioAutenticacao>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsUsuarioAutenticacaoToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsUsuarioAutenticacaoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioAutenticacao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Empresa.TcsUsuarioAutenticacao", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.EmpresaDataSource", DataSourceObject = "GetTcsUsuarioAutenticacao", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsUsuarioAutenticacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioAutenticacao> GetSampleTcsUsuarioAutenticacao(string details)
        {
            var result = repository.Context.GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsUsuarioAutenticacaoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsUsuarioAutenticacaoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioAutenticacao), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsUsuarioAutenticacaoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacaoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsUsuarioAutenticacaoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsEmpresaGpeconP"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsEmpresaGpeconP> GetTcsEmpresaGpeconP()
        {
            return repository.Context.GetTcsEmpresaGpeconP();
        }
        
        [Route("GetTcsEmpresaGpeconPNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsEmpresaGpeconP> GetTcsEmpresaGpeconPNoAssociations()
        {
            return repository.Context.GetTcsEmpresaGpeconPNoAssociations();
        }
        
        [Route("GetTcsEmpresaGpeconPByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsEmpresaGpeconP> GetTcsEmpresaGpeconPByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsEmpresaGpeconPByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsEmpresaGpeconP), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsEmpresaGpeconPByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsEmpresaGpeconP> GetTcsEmpresaGpeconPByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsEmpresaGpeconPByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsEmpresaGpeconP), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsEmpresaGpeconPToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsEmpresaGpeconPToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsEmpresaGpeconP), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsEmpresaGpeconPByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdLinx asc, IdLinxGpecon asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Empresa.TcsEmpresaGpeconP");
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
               return ExcelExportPagination<BusinessNS.TcsEmpresaGpeconP>.CreateExcelDocumentFileMapPath("TcsEmpresaGpeconP",new ExcelExportPagination<BusinessNS.TcsEmpresaGpeconP>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsEmpresaGpeconPToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsEmpresaGpeconPToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsEmpresaGpeconP), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsEmpresaGpeconPByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Empresa.TcsEmpresaGpeconP", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.EmpresaDataSource", DataSourceObject = "GetTcsEmpresaGpeconP", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsEmpresaGpeconP"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsEmpresaGpeconP> GetSampleTcsEmpresaGpeconP(string details)
        {
            var result = repository.Context.GetTcsEmpresaGpeconPByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsEmpresaGpeconPEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsEmpresaGpeconPEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsEmpresaGpeconP), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsEmpresaGpeconPByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsEmpresaGpeconP> GetTcsEmpresaGpeconPByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsEmpresaGpeconPByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsEmpresaAutenticacaoP"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsEmpresaAutenticacaoP> GetTcsEmpresaAutenticacaoP()
        {
            return repository.Context.GetTcsEmpresaAutenticacaoP();
        }
        
        [Route("GetTcsEmpresaAutenticacaoPNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsEmpresaAutenticacaoP> GetTcsEmpresaAutenticacaoPNoAssociations()
        {
            return repository.Context.GetTcsEmpresaAutenticacaoPNoAssociations();
        }
        
        [Route("GetTcsEmpresaAutenticacaoPByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsEmpresaAutenticacaoP> GetTcsEmpresaAutenticacaoPByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsEmpresaAutenticacaoPByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsEmpresaAutenticacaoP), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsEmpresaAutenticacaoPByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsEmpresaAutenticacaoP> GetTcsEmpresaAutenticacaoPByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsEmpresaAutenticacaoPByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsEmpresaAutenticacaoP), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsEmpresaAutenticacaoPToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsEmpresaAutenticacaoPToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsEmpresaAutenticacaoP), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsEmpresaAutenticacaoPByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdLinx asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Empresa.TcsEmpresaAutenticacaoP");
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
               return ExcelExportPagination<BusinessNS.TcsEmpresaAutenticacaoP>.CreateExcelDocumentFileMapPath("TcsEmpresaAutenticacaoP",new ExcelExportPagination<BusinessNS.TcsEmpresaAutenticacaoP>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsEmpresaAutenticacaoPToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsEmpresaAutenticacaoPToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsEmpresaAutenticacaoP), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsEmpresaAutenticacaoPByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Empresa.TcsEmpresaAutenticacaoP", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.EmpresaDataSource", DataSourceObject = "GetTcsEmpresaAutenticacaoP", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsEmpresaAutenticacaoP"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsEmpresaAutenticacaoP> GetSampleTcsEmpresaAutenticacaoP(string details)
        {
            var result = repository.Context.GetTcsEmpresaAutenticacaoPByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsEmpresaAutenticacaoPEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsEmpresaAutenticacaoPEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsEmpresaAutenticacaoP), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsEmpresaAutenticacaoPByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsEmpresaAutenticacaoP> GetTcsEmpresaAutenticacaoPByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsEmpresaAutenticacaoPByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        #endregion
        
        #region Get Business Entities By Parent Composition
        
        [Route("GetTcsEmpresaGpeconParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsEmpresaGpeconParentComposition> GetTcsEmpresaGpeconParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsEmpresaGpeconParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsEmpresaGpeconParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsEmpresaGpeconParentCompositionToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsEmpresaGpeconParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("TcsEmpresaGpecon{", "TcsEmpresaGpeconParentComposition{");
            jEntitySearch = jEntitySearch.Replace("TcsEmpresaAutenticacao{", "TcsEmpresaGpeconParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsEmpresaGpeconParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsEmpresaGpeconParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdGrupoEconomico asc, IdLinx asc, IdLinxGpecon asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Empresa.TcsEmpresaGpecon");
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
               return ExcelExportPagination<BusinessNS.TcsEmpresaGpeconParentComposition>.CreateExcelDocumentFileMapPath("TcsEmpresaGpecon",new ExcelExportPagination<BusinessNS.TcsEmpresaGpeconParentComposition>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsEmpresaGpeconParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsEmpresaGpeconParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsEmpresaGpeconParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsEmpresaGpeconParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Empresa.TcsEmpresaGpecon", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.EmpresaDataSource", DataSourceObject = "GetTcsEmpresaGpeconParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsEmpresaGpeconParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsEmpresaGpeconParentComposition> GetSampleTcsEmpresaGpeconParentComposition(string details)
        {
            var result = repository.Context.GetTcsEmpresaGpeconParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("GetTcsAmbienteParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbienteParentComposition> GetTcsAmbienteParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsAmbienteParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsAmbienteParentCompositionToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsAmbienteParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("TcsAmbiente{", "TcsAmbienteParentComposition{");
            jEntitySearch = jEntitySearch.Replace("TcsEmpresaAutenticacao{", "TcsAmbienteParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsAmbienteParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsAmbiente asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Empresa.TcsAmbiente");
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
               return ExcelExportPagination<BusinessNS.TcsAmbienteParentComposition>.CreateExcelDocumentFileMapPath("TcsAmbiente",new ExcelExportPagination<BusinessNS.TcsAmbienteParentComposition>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsAmbienteParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsAmbienteParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsAmbienteParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Empresa.TcsAmbiente", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.EmpresaDataSource", DataSourceObject = "GetTcsAmbienteParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsAmbienteParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbienteParentComposition> GetSampleTcsAmbienteParentComposition(string details)
        {
            var result = repository.Context.GetTcsAmbienteParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("GetTcsEmpresaModuloParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsEmpresaModuloParentComposition> GetTcsEmpresaModuloParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsEmpresaModuloParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsEmpresaModuloParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsEmpresaModuloParentCompositionToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsEmpresaModuloParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("TcsEmpresaModulo{", "TcsEmpresaModuloParentComposition{");
            jEntitySearch = jEntitySearch.Replace("TcsEmpresaAutenticacao{", "TcsEmpresaModuloParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsEmpresaModuloParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsEmpresaModuloParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsEmpresaModulo asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Empresa.TcsEmpresaModulo");
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
               return ExcelExportPagination<BusinessNS.TcsEmpresaModuloParentComposition>.CreateExcelDocumentFileMapPath("TcsEmpresaModulo",new ExcelExportPagination<BusinessNS.TcsEmpresaModuloParentComposition>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsEmpresaModuloParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsEmpresaModuloParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsEmpresaModuloParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsEmpresaModuloParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Empresa.TcsEmpresaModulo", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.EmpresaDataSource", DataSourceObject = "GetTcsEmpresaModuloParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsEmpresaModuloParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsEmpresaModuloParentComposition> GetSampleTcsEmpresaModuloParentComposition(string details)
        {
            var result = repository.Context.GetTcsEmpresaModuloParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("GetTcsUsuarioAutenticacaoParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioAutenticacaoParentComposition> GetTcsUsuarioAutenticacaoParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsUsuarioAutenticacaoParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioAutenticacaoParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsUsuarioAutenticacaoParentCompositionToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsUsuarioAutenticacaoParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("TcsUsuarioAutenticacao{", "TcsUsuarioAutenticacaoParentComposition{");
            jEntitySearch = jEntitySearch.Replace("TcsEmpresaAutenticacao{", "TcsUsuarioAutenticacaoParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioAutenticacaoParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsUsuarioAutenticacaoParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("NomeAutenticacao asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Empresa.TcsUsuarioAutenticacao");
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
               return ExcelExportPagination<BusinessNS.TcsUsuarioAutenticacaoParentComposition>.CreateExcelDocumentFileMapPath("TcsUsuarioAutenticacao",new ExcelExportPagination<BusinessNS.TcsUsuarioAutenticacaoParentComposition>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsUsuarioAutenticacaoParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsUsuarioAutenticacaoParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioAutenticacaoParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsUsuarioAutenticacaoParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Empresa.TcsUsuarioAutenticacao", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.EmpresaDataSource", DataSourceObject = "GetTcsUsuarioAutenticacaoParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsUsuarioAutenticacaoParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioAutenticacaoParentComposition> GetSampleTcsUsuarioAutenticacaoParentComposition(string details)
        {
            var result = repository.Context.GetTcsUsuarioAutenticacaoParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
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
    
    public partial class LinxFrameworkEmpresaFeedController : ODataController
    {
        private BusinessNS.EmpresaDomainService _context;
        public BusinessNS.EmpresaDomainService Context { get {  if (_context == null) { _context = new BusinessNS.EmpresaDomainService(); _context.IsSecure = true; } return _context; }  }
        
        #region Get Action to Business Entities
        
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsEmpresaAutenticacao> GetTcsEmpresaAutenticacaoById([FromODataUri]int key0)
        {
            var entity = this.Context.GetTcsEmpresaAutenticacaoByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsEmpresaAutenticacao[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsEmpresaAutenticacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsEmpresaAutenticacao> GetTcsEmpresaAutenticacaoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsEmpresaAutenticacaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsEmpresaAutenticacao), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsEmpresaAutenticacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsEmpresaAutenticacao> GetTcsEmpresaAutenticacao()
        {
            return this.Context.GetTcsEmpresaAutenticacaoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsEmpresaGpecon> GetTcsEmpresaAutenticacao__TcsEmpresaGpecon(int key0, string navigation)
        {
            var entity = this.Context.GetTcsEmpresaAutenticacaoByKey(key0);
            if (entity != null && navigation == "TcsEmpresaGpeconList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsEmpresaGpecon" });
               return entity.TcsEmpresaGpeconList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsEmpresaGpecon>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsAmbiente> GetTcsEmpresaAutenticacao__TcsAmbiente(int key0, string navigation)
        {
            var entity = this.Context.GetTcsEmpresaAutenticacaoByKey(key0);
            if (entity != null && navigation == "TcsAmbienteList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsAmbiente" });
               return entity.TcsAmbienteList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsAmbiente>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsEmpresaModulo> GetTcsEmpresaAutenticacao__TcsEmpresaModulo(int key0, string navigation)
        {
            var entity = this.Context.GetTcsEmpresaAutenticacaoByKey(key0);
            if (entity != null && navigation == "TcsEmpresaModuloList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsEmpresaModulo" });
               return entity.TcsEmpresaModuloList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsEmpresaModulo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioAutenticacao> GetTcsEmpresaAutenticacao__TcsUsuarioAutenticacao(int key0, string navigation)
        {
            var entity = this.Context.GetTcsEmpresaAutenticacaoByKey(key0);
            if (entity != null && navigation == "TcsUsuarioAutenticacaoList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsUsuarioAutenticacao" });
               return entity.TcsUsuarioAutenticacaoList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsUsuarioAutenticacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsEmpresaGpecon> GetTcsEmpresaGpeconById([FromODataUri]Int32 key0, [FromODataUri]Int32 key1, [FromODataUri]Int32 key2)
        {
            var entity = this.Context.GetTcsEmpresaGpeconByKey(key0, key1, key2);
            if (entity != null)
               return (new BusinessNS.TcsEmpresaGpecon[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsEmpresaGpecon>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsEmpresaGpecon> GetTcsEmpresaGpeconByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsEmpresaGpeconByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsEmpresaGpecon), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsEmpresaGpecon>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsEmpresaGpecon> GetTcsEmpresaGpecon()
        {
            return this.Context.GetTcsEmpresaGpeconByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsEmpresaGpeconParentComposition> GetTcsEmpresaGpeconParentComposition()
        {
            return this.Context.GetTcsEmpresaGpeconParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsEmpresaGpeconParentComposition> GetTcsEmpresaGpeconParentCompositionByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                jEntitySearch = jEntitySearch.Replace("TcsEmpresaGpecon{", "TcsEmpresaGpeconParentComposition{");
                jEntitySearch = jEntitySearch.Replace("TcsEmpresaAutenticacao{", "TcsEmpresaGpeconParentComposition{");
                var entity = this.Context.GetTcsEmpresaGpeconParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsEmpresaGpeconParentComposition), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsEmpresaGpeconParentComposition>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsEmpresaAutenticacao> GetTcsEmpresaGpecon__TcsEmpresaAutenticacao(Int32 key0, Int32 key1, Int32 key2, string navigation)
        {
            var entity = this.Context.GetTcsEmpresaGpeconByKey(key0, key1, key2);
            if (entity != null && navigation == "TcsEmpresaAutenticacao")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsEmpresaAutenticacao[] { entity.TcsEmpresaAutenticacao }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsEmpresaAutenticacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsAmbiente> GetTcsAmbienteById([FromODataUri]Int32 key0)
        {
            var entity = this.Context.GetTcsAmbienteByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsAmbiente[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsAmbiente>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsAmbiente> GetTcsAmbienteByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsAmbienteByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbiente), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsAmbiente>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsAmbiente> GetTcsAmbiente()
        {
            return this.Context.GetTcsAmbienteByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsAmbienteParentComposition> GetTcsAmbienteParentComposition()
        {
            return this.Context.GetTcsAmbienteParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsAmbienteParentComposition> GetTcsAmbienteParentCompositionByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                jEntitySearch = jEntitySearch.Replace("TcsAmbiente{", "TcsAmbienteParentComposition{");
                jEntitySearch = jEntitySearch.Replace("TcsEmpresaAutenticacao{", "TcsAmbienteParentComposition{");
                var entity = this.Context.GetTcsAmbienteParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteParentComposition), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsAmbienteParentComposition>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsEmpresaAutenticacao> GetTcsAmbiente__TcsEmpresaAutenticacao(Int32 key0, string navigation)
        {
            var entity = this.Context.GetTcsAmbienteByKey(key0);
            if (entity != null && navigation == "TcsEmpresaAutenticacao")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsEmpresaAutenticacao[] { entity.TcsEmpresaAutenticacao }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsEmpresaAutenticacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsEmpresaModulo> GetTcsEmpresaModuloById([FromODataUri]Int32 key0)
        {
            var entity = this.Context.GetTcsEmpresaModuloByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsEmpresaModulo[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsEmpresaModulo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsEmpresaModulo> GetTcsEmpresaModuloByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsEmpresaModuloByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsEmpresaModulo), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsEmpresaModulo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsEmpresaModulo> GetTcsEmpresaModulo()
        {
            return this.Context.GetTcsEmpresaModuloByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsEmpresaModuloParentComposition> GetTcsEmpresaModuloParentComposition()
        {
            return this.Context.GetTcsEmpresaModuloParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsEmpresaModuloParentComposition> GetTcsEmpresaModuloParentCompositionByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                jEntitySearch = jEntitySearch.Replace("TcsEmpresaModulo{", "TcsEmpresaModuloParentComposition{");
                jEntitySearch = jEntitySearch.Replace("TcsEmpresaAutenticacao{", "TcsEmpresaModuloParentComposition{");
                var entity = this.Context.GetTcsEmpresaModuloParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsEmpresaModuloParentComposition), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsEmpresaModuloParentComposition>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsEmpresaAutenticacao> GetTcsEmpresaModulo__TcsEmpresaAutenticacao(Int32 key0, string navigation)
        {
            var entity = this.Context.GetTcsEmpresaModuloByKey(key0);
            if (entity != null && navigation == "TcsEmpresaAutenticacao")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsEmpresaAutenticacao[] { entity.TcsEmpresaAutenticacao }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsEmpresaAutenticacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacaoById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetTcsUsuarioAutenticacaoByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsUsuarioAutenticacao[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsUsuarioAutenticacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacaoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioAutenticacao), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsUsuarioAutenticacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacao()
        {
            return this.Context.GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioAutenticacaoParentComposition> GetTcsUsuarioAutenticacaoParentComposition()
        {
            return this.Context.GetTcsUsuarioAutenticacaoParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioAutenticacaoParentComposition> GetTcsUsuarioAutenticacaoParentCompositionByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                jEntitySearch = jEntitySearch.Replace("TcsUsuarioAutenticacao{", "TcsUsuarioAutenticacaoParentComposition{");
                jEntitySearch = jEntitySearch.Replace("TcsEmpresaAutenticacao{", "TcsUsuarioAutenticacaoParentComposition{");
                var entity = this.Context.GetTcsUsuarioAutenticacaoParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioAutenticacaoParentComposition), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsUsuarioAutenticacaoParentComposition>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsEmpresaAutenticacao> GetTcsUsuarioAutenticacao__TcsEmpresaAutenticacao(Int64 key0, string navigation)
        {
            var entity = this.Context.GetTcsUsuarioAutenticacaoByKey(key0);
            if (entity != null && navigation == "TcsEmpresaAutenticacao")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsEmpresaAutenticacao[] { entity.TcsEmpresaAutenticacao }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsEmpresaAutenticacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsEmpresaGpeconP> GetTcsEmpresaGpeconPById([FromODataUri]Int32 key0, [FromODataUri]Int32 key1)
        {
            var entity = this.Context.GetTcsEmpresaGpeconPByKey(key0, key1);
            if (entity != null)
               return (new BusinessNS.TcsEmpresaGpeconP[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsEmpresaGpeconP>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsEmpresaGpeconP> GetTcsEmpresaGpeconPByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsEmpresaGpeconPByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsEmpresaGpeconP), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsEmpresaGpeconP>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsEmpresaGpeconP> GetTcsEmpresaGpeconP()
        {
            return this.Context.GetTcsEmpresaGpeconPByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsEmpresaAutenticacaoP> GetTcsEmpresaAutenticacaoPById([FromODataUri]Int32 key0)
        {
            var entity = this.Context.GetTcsEmpresaAutenticacaoPByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsEmpresaAutenticacaoP[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsEmpresaAutenticacaoP>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsEmpresaAutenticacaoP> GetTcsEmpresaAutenticacaoPByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsEmpresaAutenticacaoPByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsEmpresaAutenticacaoP), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsEmpresaAutenticacaoP>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsEmpresaAutenticacaoP> GetTcsEmpresaAutenticacaoP()
        {
            return this.Context.GetTcsEmpresaAutenticacaoPByEntitySearchNoAssociations(null).AsQueryable();
        }
        #endregion
        
    }
    
    public partial class LinxFrameworkEmpresaControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return true;
        }
    }
}

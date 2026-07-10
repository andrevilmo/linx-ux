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
using BusinessNS = Linx.Framework.BV.Configuracao;

namespace Linx.Framework.BV.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/LinxFrameworkConfiguracao/[ActionName]
    // Security Information Call: http://localhost:1710/LinxFrameworkConfiguracao/GetSecurityInfo
    // Entities Catalog Call: http://localhost:1710/LinxFrameworkConfiguracao/GetEntities
    // Entity MetaData Call: http://localhost:1710/LinxFrameworkConfiguracao/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/LinxFrameworkConfiguracao/GetClientDomains?erp=true
    // Client Service Call: http://localhost:1710/LinxFrameworkConfiguracao/GetClientService?erp=true
    // Client Factory Call: http://localhost:1710/LinxFrameworkConfiguracao/GetClientFactory?entityName=[EntityName]&erp=true
    // Client Factory Custom Events Call: http://localhost:1710/LinxFrameworkConfiguracao/GetClientFactoryCustomEvents?entityName=[EntityName]&erp=true
    // Help Call: http://localhost:1710/HelpController/LinxFrameworkConfiguracao
    // Feed OData Call: http://localhost:1710/LinxFrameworkConfiguracaoOData
    [RoutePrefix("LinxFrameworkConfiguracao")]
    [Breeze.WebApi2.BreezeController]
    public partial class LinxFrameworkConfiguracaoController : ApiController
    {
        private const int maxObjectExcelReturned = 2300000;
        private DataServiceRepository<BusinessNS.ConfiguracaoDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.ConfiguracaoDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.ConfiguracaoDomainService>(typeof(BusinessNS.ConfiguracaoAcesso), typeof(BusinessNS.TcsUsuarioConfiguracao), typeof(BusinessNS.TcsUsuarioConfiguracaoAcesso)); _repository.Context.IsSecure = true; } return _repository; } }
        public LinxFrameworkConfiguracaoController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(LinxFrameworkConfiguracaoController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.ConfiguracaoDomainService).Assembly.FullName,
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
            var result = repository.Context.GetMetaDataObject("Linx.Framework.BV.Configuracao." + entityName, false, true);
            return (allComposition ? result : result.Where(e => e.ClassName == entityName).ToList());
        }
        
        [Route("GetSecurityInfo"), System.Web.Http.HttpGet()]
        public string[] GetSecurityInfo()
        {
           return new string[] { "Linx.Framework.BV", "LinxFrameworkConfiguracao", "LinxFrameworkConfiguracao/ActionName" };
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
        
        [Route("GetAllLookUpTcsEmpresaAutenticacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsEmpresaAutenticacao> GetAllLookUpTcsEmpresaAutenticacao()
        {
            return repository.Context.GetAllLookUpTcsEmpresaAutenticacao();
        }
        
        [Route("GetLookUpTcsEmpresaAutenticacaoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsEmpresaAutenticacao> GetLookUpTcsEmpresaAutenticacaoByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsEmpresaAutenticacaoByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
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
        #endregion
        #region Get KPI Ranges
        #endregion
        
        #region Get Business Entities
        
        [Route("GetBmEntityProperties"), System.Web.Http.HttpGet()]
        public List<BmMetaDataProperty> GetBmEntityProperties(string entityName, string parentDataPath)
        {
            return repository.Context.GetBmEntityProperties(entityName, parentDataPath);
        }
        
        [Route("GetTcsUsuarioConfiguracao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioConfiguracao> GetTcsUsuarioConfiguracao()
        {
            return repository.Context.GetTcsUsuarioConfiguracao();
        }
        
        [Route("GetTcsUsuarioConfiguracaoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioConfiguracao> GetTcsUsuarioConfiguracaoNoAssociations()
        {
            return repository.Context.GetTcsUsuarioConfiguracaoNoAssociations();
        }
        
        [Route("GetTcsUsuarioConfiguracaoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioConfiguracao> GetTcsUsuarioConfiguracaoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsUsuarioConfiguracaoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioConfiguracao), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsUsuarioConfiguracaoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioConfiguracao> GetTcsUsuarioConfiguracaoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsUsuarioConfiguracaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioConfiguracao), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsUsuarioConfiguracaoToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsUsuarioConfiguracaoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioConfiguracao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsUsuarioConfiguracaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdUsuario asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Configuracao.TcsUsuarioConfiguracao");
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
               return ExcelExportPagination<BusinessNS.TcsUsuarioConfiguracao>.CreateExcelDocumentFileMapPath("TcsUsuarioConfiguracao",new ExcelExportPagination<BusinessNS.TcsUsuarioConfiguracao>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsUsuarioConfiguracaoToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsUsuarioConfiguracaoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioConfiguracao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsUsuarioConfiguracaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Configuracao.TcsUsuarioConfiguracao", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ConfiguracaoDataSource", DataSourceObject = "GetTcsUsuarioConfiguracao", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsUsuarioConfiguracao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioConfiguracao> GetSampleTcsUsuarioConfiguracao(string details)
        {
            var result = repository.Context.GetTcsUsuarioConfiguracaoByEntitySearchNoAssociations(null).Take(100).ToList();
               if (!details.IsNullOrEmpty())
               {
                   foreach(var entity in result)
                   {
                       entity.FillDetails(repository.Context, null, null, details.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries), 5);
                   }
               }
            return result.AsQueryable();
        }
        
        [Route("AddTcsUsuarioConfiguracaoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsUsuarioConfiguracaoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioConfiguracao), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsUsuarioConfiguracaoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioConfiguracao> GetTcsUsuarioConfiguracaoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsUsuarioConfiguracaoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsUsuarioConfiguracaoAcesso"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioConfiguracaoAcesso> GetTcsUsuarioConfiguracaoAcesso()
        {
            return repository.Context.GetTcsUsuarioConfiguracaoAcesso();
        }
        
        [Route("GetTcsUsuarioConfiguracaoAcessoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioConfiguracaoAcesso> GetTcsUsuarioConfiguracaoAcessoNoAssociations()
        {
            return repository.Context.GetTcsUsuarioConfiguracaoAcessoNoAssociations();
        }
        
        [Route("GetTcsUsuarioConfiguracaoAcessoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioConfiguracaoAcesso> GetTcsUsuarioConfiguracaoAcessoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsUsuarioConfiguracaoAcessoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioConfiguracaoAcesso), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsUsuarioConfiguracaoAcessoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioConfiguracaoAcesso> GetTcsUsuarioConfiguracaoAcessoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsUsuarioConfiguracaoAcessoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioConfiguracaoAcesso), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsUsuarioConfiguracaoAcessoToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsUsuarioConfiguracaoAcessoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioConfiguracaoAcesso), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsUsuarioConfiguracaoAcessoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsUsuarioAcesso asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Configuracao.TcsUsuarioConfiguracaoAcesso");
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
               return ExcelExportPagination<BusinessNS.TcsUsuarioConfiguracaoAcesso>.CreateExcelDocumentFileMapPath("TcsUsuarioConfiguracaoAcesso",new ExcelExportPagination<BusinessNS.TcsUsuarioConfiguracaoAcesso>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsUsuarioConfiguracaoAcessoToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsUsuarioConfiguracaoAcessoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioConfiguracaoAcesso), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsUsuarioConfiguracaoAcessoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Configuracao.TcsUsuarioConfiguracaoAcesso", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ConfiguracaoDataSource", DataSourceObject = "GetTcsUsuarioConfiguracaoAcesso", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsUsuarioConfiguracaoAcesso"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioConfiguracaoAcesso> GetSampleTcsUsuarioConfiguracaoAcesso(string details)
        {
            var result = repository.Context.GetTcsUsuarioConfiguracaoAcessoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsUsuarioConfiguracaoAcessoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsUsuarioConfiguracaoAcessoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioConfiguracaoAcesso), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsUsuarioConfiguracaoAcessoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioConfiguracaoAcesso> GetTcsUsuarioConfiguracaoAcessoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsUsuarioConfiguracaoAcessoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetConfiguracaoAcesso"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.ConfiguracaoAcesso> GetConfiguracaoAcesso()
        {
            return repository.Context.GetConfiguracaoAcesso().AsQueryable();
        }
        
        [Route("GetConfiguracaoAcessoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.ConfiguracaoAcesso> GetConfiguracaoAcessoNoAssociations()
        {
            return repository.Context.GetConfiguracaoAcessoNoAssociations().AsQueryable();
        }
        
        [Route("GetConfiguracaoAcessoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.ConfiguracaoAcesso> GetConfiguracaoAcessoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetConfiguracaoAcessoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.ConfiguracaoAcesso), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        
        [Route("GetConfiguracaoAcessoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.ConfiguracaoAcesso> GetConfiguracaoAcessoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetConfiguracaoAcessoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.ConfiguracaoAcesso), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        [Route("GetConfiguracaoAcessoToExcel"), System.Web.Http.HttpPost()]
        public string GetConfiguracaoAcessoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.ConfiguracaoAcesso), jEntitySearch, false, false, false);
            var entities = repository.Context.GetConfiguracaoAcessoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).AsQueryable().OrderBy("IdLinx asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Configuracao.ConfiguracaoAcesso");
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
               return ExcelExportPagination<BusinessNS.ConfiguracaoAcesso>.CreateExcelDocumentFileMapPath("ConfiguracaoAcesso",new ExcelExportPagination<BusinessNS.ConfiguracaoAcesso>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetConfiguracaoAcessoToReportXml"), System.Web.Http.HttpPost()]
        public string GetConfiguracaoAcessoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.ConfiguracaoAcesso), jEntitySearch, false, false, false);
            var entities = repository.Context.GetConfiguracaoAcessoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Configuracao.ConfiguracaoAcesso", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ConfiguracaoDataSource", DataSourceObject = "GetConfiguracaoAcesso", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleConfiguracaoAcesso"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.ConfiguracaoAcesso> GetSampleConfiguracaoAcesso(string details)
        {
            var result = repository.Context.GetConfiguracaoAcessoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddConfiguracaoAcessoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddConfiguracaoAcessoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.ConfiguracaoAcesso), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetConfiguracaoAcessoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.ConfiguracaoAcesso> GetConfiguracaoAcessoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetConfiguracaoAcessoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        #endregion
        
        #region Get Business Entities By Parent Composition
        
        [Route("GetTcsUsuarioConfiguracaoAcessoParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioConfiguracaoAcessoParentComposition> GetTcsUsuarioConfiguracaoAcessoParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsUsuarioConfiguracaoAcessoParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioConfiguracaoAcessoParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsUsuarioConfiguracaoAcessoParentCompositionToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsUsuarioConfiguracaoAcessoParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("TcsUsuarioConfiguracaoAcesso{", "TcsUsuarioConfiguracaoAcessoParentComposition{");
            jEntitySearch = jEntitySearch.Replace("TcsUsuarioConfiguracao{", "TcsUsuarioConfiguracaoAcessoParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioConfiguracaoAcessoParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsUsuarioConfiguracaoAcessoParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsUsuarioAcesso asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Configuracao.TcsUsuarioConfiguracaoAcesso");
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
               return ExcelExportPagination<BusinessNS.TcsUsuarioConfiguracaoAcessoParentComposition>.CreateExcelDocumentFileMapPath("TcsUsuarioConfiguracaoAcesso",new ExcelExportPagination<BusinessNS.TcsUsuarioConfiguracaoAcessoParentComposition>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsUsuarioConfiguracaoAcessoParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsUsuarioConfiguracaoAcessoParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioConfiguracaoAcessoParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsUsuarioConfiguracaoAcessoParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Configuracao.TcsUsuarioConfiguracaoAcesso", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ConfiguracaoDataSource", DataSourceObject = "GetTcsUsuarioConfiguracaoAcessoParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsUsuarioConfiguracaoAcessoParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioConfiguracaoAcessoParentComposition> GetSampleTcsUsuarioConfiguracaoAcessoParentComposition(string details)
        {
            var result = repository.Context.GetTcsUsuarioConfiguracaoAcessoParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
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
    
    public partial class LinxFrameworkConfiguracaoFeedController : ODataController
    {
        private BusinessNS.ConfiguracaoDomainService _context;
        public BusinessNS.ConfiguracaoDomainService Context { get {  if (_context == null) { _context = new BusinessNS.ConfiguracaoDomainService(); _context.IsSecure = true; } return _context; }  }
        
        #region Get Action to Business Entities
        
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioConfiguracao> GetTcsUsuarioConfiguracaoById([FromODataUri]long key0)
        {
            var entity = this.Context.GetTcsUsuarioConfiguracaoByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsUsuarioConfiguracao[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsUsuarioConfiguracao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioConfiguracao> GetTcsUsuarioConfiguracaoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsUsuarioConfiguracaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioConfiguracao), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsUsuarioConfiguracao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioConfiguracao> GetTcsUsuarioConfiguracao()
        {
            return this.Context.GetTcsUsuarioConfiguracaoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioConfiguracaoAcesso> GetTcsUsuarioConfiguracao__TcsUsuarioConfiguracaoAcesso(long key0, string navigation)
        {
            var entity = this.Context.GetTcsUsuarioConfiguracaoByKey(key0);
            if (entity != null && navigation == "TcsUsuarioConfiguracaoAcessoList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsUsuarioConfiguracaoAcesso" });
               return entity.TcsUsuarioConfiguracaoAcessoList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsUsuarioConfiguracaoAcesso>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioConfiguracaoAcesso> GetTcsUsuarioConfiguracaoAcessoById([FromODataUri]int key0)
        {
            var entity = this.Context.GetTcsUsuarioConfiguracaoAcessoByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsUsuarioConfiguracaoAcesso[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsUsuarioConfiguracaoAcesso>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioConfiguracaoAcesso> GetTcsUsuarioConfiguracaoAcessoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsUsuarioConfiguracaoAcessoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioConfiguracaoAcesso), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsUsuarioConfiguracaoAcesso>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioConfiguracaoAcesso> GetTcsUsuarioConfiguracaoAcesso()
        {
            return this.Context.GetTcsUsuarioConfiguracaoAcessoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioConfiguracaoAcessoParentComposition> GetTcsUsuarioConfiguracaoAcessoParentComposition()
        {
            return this.Context.GetTcsUsuarioConfiguracaoAcessoParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioConfiguracaoAcessoParentComposition> GetTcsUsuarioConfiguracaoAcessoParentCompositionByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                jEntitySearch = jEntitySearch.Replace("TcsUsuarioConfiguracaoAcesso{", "TcsUsuarioConfiguracaoAcessoParentComposition{");
                jEntitySearch = jEntitySearch.Replace("TcsUsuarioConfiguracao{", "TcsUsuarioConfiguracaoAcessoParentComposition{");
                var entity = this.Context.GetTcsUsuarioConfiguracaoAcessoParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioConfiguracaoAcessoParentComposition), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsUsuarioConfiguracaoAcessoParentComposition>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioConfiguracao> GetTcsUsuarioConfiguracaoAcesso__TcsUsuarioConfiguracao(int key0, string navigation)
        {
            var entity = this.Context.GetTcsUsuarioConfiguracaoAcessoByKey(key0);
            if (entity != null && navigation == "TcsUsuarioConfiguracao")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsUsuarioConfiguracao[] { entity.TcsUsuarioConfiguracao }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsUsuarioConfiguracao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.ConfiguracaoAcesso> GetConfiguracaoAcessoById([FromODataUri]int key0)
        {
            var entity = this.Context.GetConfiguracaoAcessoByKey(key0);
            if (entity != null)
               return (new BusinessNS.ConfiguracaoAcesso[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.ConfiguracaoAcesso>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.ConfiguracaoAcesso> GetConfiguracaoAcessoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetConfiguracaoAcessoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.ConfiguracaoAcesso), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.ConfiguracaoAcesso>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.ConfiguracaoAcesso> GetConfiguracaoAcesso()
        {
            return this.Context.GetConfiguracaoAcessoByEntitySearchNoAssociations(null).AsQueryable();
        }
        #endregion
        
    }
    
    public partial class LinxFrameworkConfiguracaoControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return true;
        }
    }
}

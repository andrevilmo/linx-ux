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
using BusinessNS = Linx.Framework.BV.Ambiente;

namespace Linx.Framework.BV.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/LinxFrameworkAmbiente/[ActionName]
    // Security Information Call: http://localhost:1710/LinxFrameworkAmbiente/GetSecurityInfo
    // Entities Catalog Call: http://localhost:1710/LinxFrameworkAmbiente/GetEntities
    // Entity MetaData Call: http://localhost:1710/LinxFrameworkAmbiente/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/LinxFrameworkAmbiente/GetClientDomains?erp=true
    // Client Service Call: http://localhost:1710/LinxFrameworkAmbiente/GetClientService?erp=true
    // Client Factory Call: http://localhost:1710/LinxFrameworkAmbiente/GetClientFactory?entityName=[EntityName]&erp=true
    // Client Factory Custom Events Call: http://localhost:1710/LinxFrameworkAmbiente/GetClientFactoryCustomEvents?entityName=[EntityName]&erp=true
    // Help Call: http://localhost:1710/HelpController/LinxFrameworkAmbiente
    // Feed OData Call: http://localhost:1710/LinxFrameworkAmbienteOData
    [RoutePrefix("LinxFrameworkAmbiente")]
    [Breeze.WebApi2.BreezeController]
    public partial class LinxFrameworkAmbienteController : ApiController
    {
        private const int maxObjectExcelReturned = 2300000;
        private DataServiceRepository<BusinessNS.AmbienteDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.AmbienteDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.AmbienteDomainService>(typeof(BusinessNS.AmbienteServicoInfo), typeof(BusinessNS.EnvironmentInfo), typeof(BusinessNS.ServicoExcecaoInfo), typeof(BusinessNS.TcsAmbiente), typeof(BusinessNS.TcsAmbienteConexao), typeof(BusinessNS.TcsAmbienteRelacionado), typeof(BusinessNS.TcsAmbienteServicoExcecao), typeof(BusinessNS.TcsAmbienteUsuarioAcesso), typeof(BusinessNS.TcsServico)); _repository.Context.IsSecure = true; } return _repository; } }
        public LinxFrameworkAmbienteController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(LinxFrameworkAmbienteController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.AmbienteDomainService).Assembly.FullName,
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
            var result = repository.Context.GetMetaDataObject("Linx.Framework.BV.Ambiente." + entityName, false, true);
            return (allComposition ? result : result.Where(e => e.ClassName == entityName).ToList());
        }
        
        [Route("GetSecurityInfo"), System.Web.Http.HttpGet()]
        public string[] GetSecurityInfo()
        {
           return new string[] { "Linx.Framework.BV", "LinxFrameworkAmbiente", "LinxFrameworkAmbiente/ActionName" };
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
        
        [Route("GetAllLookUpTcsBancoServidor"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsBancoServidor> GetAllLookUpTcsBancoServidor()
        {
            return repository.Context.GetAllLookUpTcsBancoServidor();
        }
        
        [Route("GetLookUpTcsBancoServidorByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsBancoServidor> GetLookUpTcsBancoServidorByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsBancoServidorByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [Route("GetAllLookUpTcsServico"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsServico> GetAllLookUpTcsServico()
        {
            return repository.Context.GetAllLookUpTcsServico();
        }
        
        [Route("GetLookUpTcsServicoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsServico> GetLookUpTcsServicoByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsServicoByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [Route("GetAllLookUpTcsAplicativoConexao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsAplicativoConexao> GetAllLookUpTcsAplicativoConexao()
        {
            return repository.Context.GetAllLookUpTcsAplicativoConexao();
        }
        
        [Route("GetLookUpTcsAplicativoConexaoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsAplicativoConexao> GetLookUpTcsAplicativoConexaoByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsAplicativoConexaoByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [Route("GetAllLookUpTcsAmbienteAdministrativo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsAmbienteAdministrativo> GetAllLookUpTcsAmbienteAdministrativo()
        {
            return repository.Context.GetAllLookUpTcsAmbienteAdministrativo();
        }
        
        [Route("GetLookUpTcsAmbienteAdministrativoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsAmbienteAdministrativo> GetLookUpTcsAmbienteAdministrativoByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsAmbienteAdministrativoByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
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
        
        [Route("GetTcsAmbienteUsuarioAcesso"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbienteUsuarioAcesso> GetTcsAmbienteUsuarioAcesso()
        {
            return repository.Context.GetTcsAmbienteUsuarioAcesso();
        }
        
        [Route("GetTcsAmbienteUsuarioAcessoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbienteUsuarioAcesso> GetTcsAmbienteUsuarioAcessoNoAssociations()
        {
            return repository.Context.GetTcsAmbienteUsuarioAcessoNoAssociations();
        }
        
        [Route("GetTcsAmbienteUsuarioAcessoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbienteUsuarioAcesso> GetTcsAmbienteUsuarioAcessoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsAmbienteUsuarioAcessoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteUsuarioAcesso), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsAmbienteUsuarioAcessoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbienteUsuarioAcesso> GetTcsAmbienteUsuarioAcessoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsAmbienteUsuarioAcessoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteUsuarioAcesso), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsAmbienteUsuarioAcessoToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsAmbienteUsuarioAcessoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteUsuarioAcesso), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsAmbienteUsuarioAcessoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsUsuarioAcesso asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Ambiente.TcsAmbienteUsuarioAcesso");
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
               return ExcelExportPagination<BusinessNS.TcsAmbienteUsuarioAcesso>.CreateExcelDocumentFileMapPath("TcsAmbienteUsuarioAcesso",new ExcelExportPagination<BusinessNS.TcsAmbienteUsuarioAcesso>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsAmbienteUsuarioAcessoToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsAmbienteUsuarioAcessoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteUsuarioAcesso), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsAmbienteUsuarioAcessoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Ambiente.TcsAmbienteUsuarioAcesso", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.AmbienteDataSource", DataSourceObject = "GetTcsAmbienteUsuarioAcesso", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsAmbienteUsuarioAcesso"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbienteUsuarioAcesso> GetSampleTcsAmbienteUsuarioAcesso(string details)
        {
            var result = repository.Context.GetTcsAmbienteUsuarioAcessoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsAmbienteUsuarioAcessoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsAmbienteUsuarioAcessoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteUsuarioAcesso), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsAmbienteUsuarioAcessoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbienteUsuarioAcesso> GetTcsAmbienteUsuarioAcessoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsAmbienteUsuarioAcessoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Ambiente.TcsAmbiente");
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Ambiente.TcsAmbiente", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.AmbienteDataSource", DataSourceObject = "GetTcsAmbiente", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsAmbiente"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbiente> GetSampleTcsAmbiente(string details)
        {
            var result = repository.Context.GetTcsAmbienteByEntitySearchNoAssociations(null).Take(100).ToList();
               if (!details.IsNullOrEmpty())
               {
                   foreach(var entity in result)
                   {
                       entity.FillDetails(repository.Context, null, null, details.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries), 5);
                   }
               }
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
        
        [Route("GetTcsAmbienteConexao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbienteConexao> GetTcsAmbienteConexao()
        {
            return repository.Context.GetTcsAmbienteConexao();
        }
        
        [Route("GetTcsAmbienteConexaoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbienteConexao> GetTcsAmbienteConexaoNoAssociations()
        {
            return repository.Context.GetTcsAmbienteConexaoNoAssociations();
        }
        
        [Route("GetTcsAmbienteConexaoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbienteConexao> GetTcsAmbienteConexaoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsAmbienteConexaoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteConexao), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsAmbienteConexaoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbienteConexao> GetTcsAmbienteConexaoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsAmbienteConexaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteConexao), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsAmbienteConexaoToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsAmbienteConexaoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteConexao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsAmbienteConexaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsAmbienteConexao asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Ambiente.TcsAmbienteConexao");
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
               return ExcelExportPagination<BusinessNS.TcsAmbienteConexao>.CreateExcelDocumentFileMapPath("TcsAmbienteConexao",new ExcelExportPagination<BusinessNS.TcsAmbienteConexao>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsAmbienteConexaoToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsAmbienteConexaoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteConexao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsAmbienteConexaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Ambiente.TcsAmbienteConexao", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.AmbienteDataSource", DataSourceObject = "GetTcsAmbienteConexao", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsAmbienteConexao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbienteConexao> GetSampleTcsAmbienteConexao(string details)
        {
            var result = repository.Context.GetTcsAmbienteConexaoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsAmbienteConexaoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsAmbienteConexaoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteConexao), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsAmbienteConexaoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbienteConexao> GetTcsAmbienteConexaoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsAmbienteConexaoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsAmbienteServicoExcecao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbienteServicoExcecao> GetTcsAmbienteServicoExcecao()
        {
            return repository.Context.GetTcsAmbienteServicoExcecao();
        }
        
        [Route("GetTcsAmbienteServicoExcecaoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbienteServicoExcecao> GetTcsAmbienteServicoExcecaoNoAssociations()
        {
            return repository.Context.GetTcsAmbienteServicoExcecaoNoAssociations();
        }
        
        [Route("GetTcsAmbienteServicoExcecaoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbienteServicoExcecao> GetTcsAmbienteServicoExcecaoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsAmbienteServicoExcecaoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteServicoExcecao), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsAmbienteServicoExcecaoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbienteServicoExcecao> GetTcsAmbienteServicoExcecaoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsAmbienteServicoExcecaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteServicoExcecao), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsAmbienteServicoExcecaoToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsAmbienteServicoExcecaoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteServicoExcecao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsAmbienteServicoExcecaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsAmbienteServicoExcecao asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Ambiente.TcsAmbienteServicoExcecao");
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
               return ExcelExportPagination<BusinessNS.TcsAmbienteServicoExcecao>.CreateExcelDocumentFileMapPath("TcsAmbienteServicoExcecao",new ExcelExportPagination<BusinessNS.TcsAmbienteServicoExcecao>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsAmbienteServicoExcecaoToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsAmbienteServicoExcecaoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteServicoExcecao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsAmbienteServicoExcecaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Ambiente.TcsAmbienteServicoExcecao", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.AmbienteDataSource", DataSourceObject = "GetTcsAmbienteServicoExcecao", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsAmbienteServicoExcecao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbienteServicoExcecao> GetSampleTcsAmbienteServicoExcecao(string details)
        {
            var result = repository.Context.GetTcsAmbienteServicoExcecaoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsAmbienteServicoExcecaoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsAmbienteServicoExcecaoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteServicoExcecao), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsAmbienteServicoExcecaoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbienteServicoExcecao> GetTcsAmbienteServicoExcecaoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsAmbienteServicoExcecaoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsServico"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsServico> GetTcsServico()
        {
            return repository.Context.GetTcsServico();
        }
        
        [Route("GetTcsServicoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsServico> GetTcsServicoNoAssociations()
        {
            return repository.Context.GetTcsServicoNoAssociations();
        }
        
        [Route("GetTcsServicoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsServico> GetTcsServicoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsServicoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsServico), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsServicoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsServico> GetTcsServicoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsServicoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsServico), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsServicoToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsServicoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsServico), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsServicoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsServico asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Ambiente.TcsServico");
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
               return ExcelExportPagination<BusinessNS.TcsServico>.CreateExcelDocumentFileMapPath("TcsServico",new ExcelExportPagination<BusinessNS.TcsServico>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsServicoToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsServicoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsServico), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsServicoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Ambiente.TcsServico", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.AmbienteDataSource", DataSourceObject = "GetTcsServico", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsServico"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsServico> GetSampleTcsServico(string details)
        {
            var result = repository.Context.GetTcsServicoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsServicoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsServicoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsServico), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsServicoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsServico> GetTcsServicoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsServicoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsAmbienteRelacionado"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbienteRelacionado> GetTcsAmbienteRelacionado()
        {
            return repository.Context.GetTcsAmbienteRelacionado();
        }
        
        [Route("GetTcsAmbienteRelacionadoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbienteRelacionado> GetTcsAmbienteRelacionadoNoAssociations()
        {
            return repository.Context.GetTcsAmbienteRelacionadoNoAssociations();
        }
        
        [Route("GetTcsAmbienteRelacionadoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbienteRelacionado> GetTcsAmbienteRelacionadoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsAmbienteRelacionadoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteRelacionado), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsAmbienteRelacionadoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbienteRelacionado> GetTcsAmbienteRelacionadoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsAmbienteRelacionadoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteRelacionado), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsAmbienteRelacionadoToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsAmbienteRelacionadoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteRelacionado), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsAmbienteRelacionadoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsUsuarioAcesso asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Ambiente.TcsAmbienteRelacionado");
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
               return ExcelExportPagination<BusinessNS.TcsAmbienteRelacionado>.CreateExcelDocumentFileMapPath("TcsAmbienteRelacionado",new ExcelExportPagination<BusinessNS.TcsAmbienteRelacionado>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsAmbienteRelacionadoToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsAmbienteRelacionadoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteRelacionado), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsAmbienteRelacionadoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Ambiente.TcsAmbienteRelacionado", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.AmbienteDataSource", DataSourceObject = "GetTcsAmbienteRelacionado", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsAmbienteRelacionado"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbienteRelacionado> GetSampleTcsAmbienteRelacionado(string details)
        {
            var result = repository.Context.GetTcsAmbienteRelacionadoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsAmbienteRelacionadoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsAmbienteRelacionadoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteRelacionado), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsAmbienteRelacionadoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbienteRelacionado> GetTcsAmbienteRelacionadoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsAmbienteRelacionadoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetServicoExcecaoInfo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.ServicoExcecaoInfo> GetServicoExcecaoInfo()
        {
            return repository.Context.GetServicoExcecaoInfo().AsQueryable();
        }
        
        [Route("GetServicoExcecaoInfoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.ServicoExcecaoInfo> GetServicoExcecaoInfoNoAssociations()
        {
            return repository.Context.GetServicoExcecaoInfoNoAssociations().AsQueryable();
        }
        
        [Route("GetServicoExcecaoInfoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.ServicoExcecaoInfo> GetServicoExcecaoInfoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetServicoExcecaoInfoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.ServicoExcecaoInfo), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        
        [Route("GetServicoExcecaoInfoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.ServicoExcecaoInfo> GetServicoExcecaoInfoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetServicoExcecaoInfoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.ServicoExcecaoInfo), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        [Route("GetServicoExcecaoInfoToExcel"), System.Web.Http.HttpPost()]
        public string GetServicoExcecaoInfoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.ServicoExcecaoInfo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetServicoExcecaoInfoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).AsQueryable().OrderBy("IdTcsAmbiente asc, Servico asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Ambiente.ServicoExcecaoInfo");
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
               return ExcelExportPagination<BusinessNS.ServicoExcecaoInfo>.CreateExcelDocumentFileMapPath("ServicoExcecaoInfo",new ExcelExportPagination<BusinessNS.ServicoExcecaoInfo>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetServicoExcecaoInfoToReportXml"), System.Web.Http.HttpPost()]
        public string GetServicoExcecaoInfoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.ServicoExcecaoInfo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetServicoExcecaoInfoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Ambiente.ServicoExcecaoInfo", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.AmbienteDataSource", DataSourceObject = "GetServicoExcecaoInfo", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleServicoExcecaoInfo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.ServicoExcecaoInfo> GetSampleServicoExcecaoInfo(string details)
        {
            var result = repository.Context.GetServicoExcecaoInfoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddServicoExcecaoInfoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddServicoExcecaoInfoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.ServicoExcecaoInfo), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetServicoExcecaoInfoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.ServicoExcecaoInfo> GetServicoExcecaoInfoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetServicoExcecaoInfoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetAmbienteServicoInfo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AmbienteServicoInfo> GetAmbienteServicoInfo()
        {
            return repository.Context.GetAmbienteServicoInfo().AsQueryable();
        }
        
        [Route("GetAmbienteServicoInfoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AmbienteServicoInfo> GetAmbienteServicoInfoNoAssociations()
        {
            return repository.Context.GetAmbienteServicoInfoNoAssociations().AsQueryable();
        }
        
        [Route("GetAmbienteServicoInfoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AmbienteServicoInfo> GetAmbienteServicoInfoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetAmbienteServicoInfoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AmbienteServicoInfo), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        
        [Route("GetAmbienteServicoInfoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AmbienteServicoInfo> GetAmbienteServicoInfoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetAmbienteServicoInfoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AmbienteServicoInfo), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        [Route("GetAmbienteServicoInfoToExcel"), System.Web.Http.HttpPost()]
        public string GetAmbienteServicoInfoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AmbienteServicoInfo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetAmbienteServicoInfoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).AsQueryable().OrderBy("Hash asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Ambiente.AmbienteServicoInfo");
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
               return ExcelExportPagination<BusinessNS.AmbienteServicoInfo>.CreateExcelDocumentFileMapPath("AmbienteServicoInfo",new ExcelExportPagination<BusinessNS.AmbienteServicoInfo>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetAmbienteServicoInfoToReportXml"), System.Web.Http.HttpPost()]
        public string GetAmbienteServicoInfoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AmbienteServicoInfo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetAmbienteServicoInfoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Ambiente.AmbienteServicoInfo", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.AmbienteDataSource", DataSourceObject = "GetAmbienteServicoInfo", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleAmbienteServicoInfo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AmbienteServicoInfo> GetSampleAmbienteServicoInfo(string details)
        {
            var result = repository.Context.GetAmbienteServicoInfoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddAmbienteServicoInfoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddAmbienteServicoInfoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AmbienteServicoInfo), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetAmbienteServicoInfoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AmbienteServicoInfo> GetAmbienteServicoInfoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetAmbienteServicoInfoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetEnvironmentInfo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.EnvironmentInfo> GetEnvironmentInfo()
        {
            return repository.Context.GetEnvironmentInfo().AsQueryable();
        }
        
        [Route("GetEnvironmentInfoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.EnvironmentInfo> GetEnvironmentInfoNoAssociations()
        {
            return repository.Context.GetEnvironmentInfoNoAssociations().AsQueryable();
        }
        
        [Route("GetEnvironmentInfoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.EnvironmentInfo> GetEnvironmentInfoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetEnvironmentInfoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.EnvironmentInfo), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        
        [Route("GetEnvironmentInfoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.EnvironmentInfo> GetEnvironmentInfoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetEnvironmentInfoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.EnvironmentInfo), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        [Route("GetEnvironmentInfoToExcel"), System.Web.Http.HttpPost()]
        public string GetEnvironmentInfoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.EnvironmentInfo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetEnvironmentInfoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).AsQueryable().OrderBy("EnvironmentId asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Ambiente.EnvironmentInfo");
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
               return ExcelExportPagination<BusinessNS.EnvironmentInfo>.CreateExcelDocumentFileMapPath("EnvironmentInfo",new ExcelExportPagination<BusinessNS.EnvironmentInfo>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetEnvironmentInfoToReportXml"), System.Web.Http.HttpPost()]
        public string GetEnvironmentInfoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.EnvironmentInfo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetEnvironmentInfoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Ambiente.EnvironmentInfo", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.AmbienteDataSource", DataSourceObject = "GetEnvironmentInfo", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleEnvironmentInfo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.EnvironmentInfo> GetSampleEnvironmentInfo(string details)
        {
            var result = repository.Context.GetEnvironmentInfoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddEnvironmentInfoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddEnvironmentInfoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.EnvironmentInfo), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetEnvironmentInfoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.EnvironmentInfo> GetEnvironmentInfoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetEnvironmentInfoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        #endregion
        
        #region Get Business Entities By Parent Composition
        
        [Route("GetTcsAmbienteUsuarioAcessoParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbienteUsuarioAcessoParentComposition> GetTcsAmbienteUsuarioAcessoParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsAmbienteUsuarioAcessoParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteUsuarioAcessoParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsAmbienteUsuarioAcessoParentCompositionToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsAmbienteUsuarioAcessoParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("TcsAmbienteUsuarioAcesso{", "TcsAmbienteUsuarioAcessoParentComposition{");
            jEntitySearch = jEntitySearch.Replace("TcsAmbiente{", "TcsAmbienteUsuarioAcessoParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteUsuarioAcessoParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsAmbienteUsuarioAcessoParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsUsuarioAcesso asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Ambiente.TcsAmbienteUsuarioAcesso");
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
               return ExcelExportPagination<BusinessNS.TcsAmbienteUsuarioAcessoParentComposition>.CreateExcelDocumentFileMapPath("TcsAmbienteUsuarioAcesso",new ExcelExportPagination<BusinessNS.TcsAmbienteUsuarioAcessoParentComposition>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsAmbienteUsuarioAcessoParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsAmbienteUsuarioAcessoParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteUsuarioAcessoParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsAmbienteUsuarioAcessoParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Ambiente.TcsAmbienteUsuarioAcesso", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.AmbienteDataSource", DataSourceObject = "GetTcsAmbienteUsuarioAcessoParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsAmbienteUsuarioAcessoParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbienteUsuarioAcessoParentComposition> GetSampleTcsAmbienteUsuarioAcessoParentComposition(string details)
        {
            var result = repository.Context.GetTcsAmbienteUsuarioAcessoParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("GetTcsAmbienteConexaoParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbienteConexaoParentComposition> GetTcsAmbienteConexaoParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsAmbienteConexaoParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteConexaoParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsAmbienteConexaoParentCompositionToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsAmbienteConexaoParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("TcsAmbienteConexao{", "TcsAmbienteConexaoParentComposition{");
            jEntitySearch = jEntitySearch.Replace("TcsAmbiente{", "TcsAmbienteConexaoParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteConexaoParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsAmbienteConexaoParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsAmbienteConexao asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Ambiente.TcsAmbienteConexao");
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
               return ExcelExportPagination<BusinessNS.TcsAmbienteConexaoParentComposition>.CreateExcelDocumentFileMapPath("TcsAmbienteConexao",new ExcelExportPagination<BusinessNS.TcsAmbienteConexaoParentComposition>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsAmbienteConexaoParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsAmbienteConexaoParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteConexaoParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsAmbienteConexaoParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Ambiente.TcsAmbienteConexao", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.AmbienteDataSource", DataSourceObject = "GetTcsAmbienteConexaoParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsAmbienteConexaoParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbienteConexaoParentComposition> GetSampleTcsAmbienteConexaoParentComposition(string details)
        {
            var result = repository.Context.GetTcsAmbienteConexaoParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("GetTcsAmbienteServicoExcecaoParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbienteServicoExcecaoParentComposition> GetTcsAmbienteServicoExcecaoParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsAmbienteServicoExcecaoParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteServicoExcecaoParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsAmbienteServicoExcecaoParentCompositionToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsAmbienteServicoExcecaoParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("TcsAmbienteServicoExcecao{", "TcsAmbienteServicoExcecaoParentComposition{");
            jEntitySearch = jEntitySearch.Replace("TcsAmbiente{", "TcsAmbienteServicoExcecaoParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteServicoExcecaoParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsAmbienteServicoExcecaoParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsAmbienteServicoExcecao asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Ambiente.TcsAmbienteServicoExcecao");
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
               return ExcelExportPagination<BusinessNS.TcsAmbienteServicoExcecaoParentComposition>.CreateExcelDocumentFileMapPath("TcsAmbienteServicoExcecao",new ExcelExportPagination<BusinessNS.TcsAmbienteServicoExcecaoParentComposition>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsAmbienteServicoExcecaoParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsAmbienteServicoExcecaoParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteServicoExcecaoParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsAmbienteServicoExcecaoParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Ambiente.TcsAmbienteServicoExcecao", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.AmbienteDataSource", DataSourceObject = "GetTcsAmbienteServicoExcecaoParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsAmbienteServicoExcecaoParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbienteServicoExcecaoParentComposition> GetSampleTcsAmbienteServicoExcecaoParentComposition(string details)
        {
            var result = repository.Context.GetTcsAmbienteServicoExcecaoParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
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
    
    public partial class LinxFrameworkAmbienteFeedController : ODataController
    {
        private BusinessNS.AmbienteDomainService _context;
        public BusinessNS.AmbienteDomainService Context { get {  if (_context == null) { _context = new BusinessNS.AmbienteDomainService(); _context.IsSecure = true; } return _context; }  }
        
        #region Get Action to Business Entities
        
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsAmbienteUsuarioAcesso> GetTcsAmbienteUsuarioAcessoById([FromODataUri]Int32 key0)
        {
            var entity = this.Context.GetTcsAmbienteUsuarioAcessoByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsAmbienteUsuarioAcesso[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsAmbienteUsuarioAcesso>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsAmbienteUsuarioAcesso> GetTcsAmbienteUsuarioAcessoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsAmbienteUsuarioAcessoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteUsuarioAcesso), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsAmbienteUsuarioAcesso>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsAmbienteUsuarioAcesso> GetTcsAmbienteUsuarioAcesso()
        {
            return this.Context.GetTcsAmbienteUsuarioAcessoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsAmbienteUsuarioAcessoParentComposition> GetTcsAmbienteUsuarioAcessoParentComposition()
        {
            return this.Context.GetTcsAmbienteUsuarioAcessoParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsAmbienteUsuarioAcessoParentComposition> GetTcsAmbienteUsuarioAcessoParentCompositionByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                jEntitySearch = jEntitySearch.Replace("TcsAmbienteUsuarioAcesso{", "TcsAmbienteUsuarioAcessoParentComposition{");
                jEntitySearch = jEntitySearch.Replace("TcsAmbiente{", "TcsAmbienteUsuarioAcessoParentComposition{");
                var entity = this.Context.GetTcsAmbienteUsuarioAcessoParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteUsuarioAcessoParentComposition), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsAmbienteUsuarioAcessoParentComposition>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsAmbiente> GetTcsAmbienteUsuarioAcesso__TcsAmbiente(Int32 key0, string navigation)
        {
            var entity = this.Context.GetTcsAmbienteUsuarioAcessoByKey(key0);
            if (entity != null && navigation == "TcsAmbiente")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsAmbiente[] { entity.TcsAmbiente }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsAmbiente>);
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
        public IQueryable<BusinessNS.TcsAmbienteUsuarioAcesso> GetTcsAmbiente__TcsAmbienteUsuarioAcesso(Int32 key0, string navigation)
        {
            var entity = this.Context.GetTcsAmbienteByKey(key0);
            if (entity != null && navigation == "TcsAmbienteUsuarioAcessoList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsAmbienteUsuarioAcesso" });
               return entity.TcsAmbienteUsuarioAcessoList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsAmbienteUsuarioAcesso>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsAmbienteConexao> GetTcsAmbiente__TcsAmbienteConexao(Int32 key0, string navigation)
        {
            var entity = this.Context.GetTcsAmbienteByKey(key0);
            if (entity != null && navigation == "TcsAmbienteConexaoList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsAmbienteConexao" });
               return entity.TcsAmbienteConexaoList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsAmbienteConexao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsAmbienteServicoExcecao> GetTcsAmbiente__TcsAmbienteServicoExcecao(Int32 key0, string navigation)
        {
            var entity = this.Context.GetTcsAmbienteByKey(key0);
            if (entity != null && navigation == "TcsAmbienteServicoExcecaoList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsAmbienteServicoExcecao" });
               return entity.TcsAmbienteServicoExcecaoList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsAmbienteServicoExcecao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsAmbienteConexao> GetTcsAmbienteConexaoById([FromODataUri]Int32 key0)
        {
            var entity = this.Context.GetTcsAmbienteConexaoByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsAmbienteConexao[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsAmbienteConexao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsAmbienteConexao> GetTcsAmbienteConexaoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsAmbienteConexaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteConexao), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsAmbienteConexao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsAmbienteConexao> GetTcsAmbienteConexao()
        {
            return this.Context.GetTcsAmbienteConexaoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsAmbienteConexaoParentComposition> GetTcsAmbienteConexaoParentComposition()
        {
            return this.Context.GetTcsAmbienteConexaoParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsAmbienteConexaoParentComposition> GetTcsAmbienteConexaoParentCompositionByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                jEntitySearch = jEntitySearch.Replace("TcsAmbienteConexao{", "TcsAmbienteConexaoParentComposition{");
                jEntitySearch = jEntitySearch.Replace("TcsAmbiente{", "TcsAmbienteConexaoParentComposition{");
                var entity = this.Context.GetTcsAmbienteConexaoParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteConexaoParentComposition), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsAmbienteConexaoParentComposition>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsAmbiente> GetTcsAmbienteConexao__TcsAmbiente(Int32 key0, string navigation)
        {
            var entity = this.Context.GetTcsAmbienteConexaoByKey(key0);
            if (entity != null && navigation == "TcsAmbiente")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsAmbiente[] { entity.TcsAmbiente }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsAmbiente>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsAmbienteServicoExcecao> GetTcsAmbienteServicoExcecaoById([FromODataUri]Int32 key0)
        {
            var entity = this.Context.GetTcsAmbienteServicoExcecaoByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsAmbienteServicoExcecao[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsAmbienteServicoExcecao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsAmbienteServicoExcecao> GetTcsAmbienteServicoExcecaoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsAmbienteServicoExcecaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteServicoExcecao), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsAmbienteServicoExcecao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsAmbienteServicoExcecao> GetTcsAmbienteServicoExcecao()
        {
            return this.Context.GetTcsAmbienteServicoExcecaoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsAmbienteServicoExcecaoParentComposition> GetTcsAmbienteServicoExcecaoParentComposition()
        {
            return this.Context.GetTcsAmbienteServicoExcecaoParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsAmbienteServicoExcecaoParentComposition> GetTcsAmbienteServicoExcecaoParentCompositionByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                jEntitySearch = jEntitySearch.Replace("TcsAmbienteServicoExcecao{", "TcsAmbienteServicoExcecaoParentComposition{");
                jEntitySearch = jEntitySearch.Replace("TcsAmbiente{", "TcsAmbienteServicoExcecaoParentComposition{");
                var entity = this.Context.GetTcsAmbienteServicoExcecaoParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteServicoExcecaoParentComposition), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsAmbienteServicoExcecaoParentComposition>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsAmbiente> GetTcsAmbienteServicoExcecao__TcsAmbiente(Int32 key0, string navigation)
        {
            var entity = this.Context.GetTcsAmbienteServicoExcecaoByKey(key0);
            if (entity != null && navigation == "TcsAmbiente")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsAmbiente[] { entity.TcsAmbiente }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsAmbiente>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsServico> GetTcsServicoById([FromODataUri]Int32 key0)
        {
            var entity = this.Context.GetTcsServicoByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsServico[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsServico>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsServico> GetTcsServicoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsServicoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsServico), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsServico>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsServico> GetTcsServico()
        {
            return this.Context.GetTcsServicoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsAmbienteRelacionado> GetTcsAmbienteRelacionadoById([FromODataUri]Int32 key0)
        {
            var entity = this.Context.GetTcsAmbienteRelacionadoByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsAmbienteRelacionado[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsAmbienteRelacionado>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsAmbienteRelacionado> GetTcsAmbienteRelacionadoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsAmbienteRelacionadoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteRelacionado), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsAmbienteRelacionado>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsAmbienteRelacionado> GetTcsAmbienteRelacionado()
        {
            return this.Context.GetTcsAmbienteRelacionadoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.ServicoExcecaoInfo> GetServicoExcecaoInfoById([FromODataUri]int key0, [FromODataUri]string key1)
        {
            var entity = this.Context.GetServicoExcecaoInfoByKey(key0, key1);
            if (entity != null)
               return (new BusinessNS.ServicoExcecaoInfo[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.ServicoExcecaoInfo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.ServicoExcecaoInfo> GetServicoExcecaoInfoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetServicoExcecaoInfoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.ServicoExcecaoInfo), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.ServicoExcecaoInfo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.ServicoExcecaoInfo> GetServicoExcecaoInfo()
        {
            return this.Context.GetServicoExcecaoInfoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.AmbienteServicoInfo> GetAmbienteServicoInfoById([FromODataUri]string key0)
        {
            var entity = this.Context.GetAmbienteServicoInfoByKey(key0);
            if (entity != null)
               return (new BusinessNS.AmbienteServicoInfo[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.AmbienteServicoInfo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.AmbienteServicoInfo> GetAmbienteServicoInfoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetAmbienteServicoInfoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AmbienteServicoInfo), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.AmbienteServicoInfo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.AmbienteServicoInfo> GetAmbienteServicoInfo()
        {
            return this.Context.GetAmbienteServicoInfoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.EnvironmentInfo> GetEnvironmentInfoById([FromODataUri]int key0)
        {
            var entity = this.Context.GetEnvironmentInfoByKey(key0);
            if (entity != null)
               return (new BusinessNS.EnvironmentInfo[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.EnvironmentInfo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.EnvironmentInfo> GetEnvironmentInfoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetEnvironmentInfoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.EnvironmentInfo), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.EnvironmentInfo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.EnvironmentInfo> GetEnvironmentInfo()
        {
            return this.Context.GetEnvironmentInfoByEntitySearchNoAssociations(null).AsQueryable();
        }
        #endregion
        
    }
    
    public partial class LinxFrameworkAmbienteControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return true;
        }
    }
}

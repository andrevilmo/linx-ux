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
using BusinessNS = Linx.Framework.Setup.LinxAutoSetup;

namespace Linx.Framework.Setup.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/LinxFrameworkLinxAutoSetup/[ActionName]
    // Security Information Call: http://localhost:1710/LinxFrameworkLinxAutoSetup/GetSecurityInfo
    // Entities Catalog Call: http://localhost:1710/LinxFrameworkLinxAutoSetup/GetEntities
    // Entity MetaData Call: http://localhost:1710/LinxFrameworkLinxAutoSetup/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/LinxFrameworkLinxAutoSetup/GetClientDomains?erp=true
    // Client Service Call: http://localhost:1710/LinxFrameworkLinxAutoSetup/GetClientService?erp=true
    // Client Factory Call: http://localhost:1710/LinxFrameworkLinxAutoSetup/GetClientFactory?entityName=[EntityName]&erp=true
    // Client Factory Custom Events Call: http://localhost:1710/LinxFrameworkLinxAutoSetup/GetClientFactoryCustomEvents?entityName=[EntityName]&erp=true
    // Help Call: http://localhost:1710/HelpController/LinxFrameworkLinxAutoSetup
    // Feed OData Call: http://localhost:1710/LinxFrameworkLinxAutoSetupOData
    [RoutePrefix("LinxFrameworkLinxAutoSetup")]
    [Breeze.WebApi2.BreezeController]
    public partial class LinxFrameworkLinxAutoSetupController : ApiController
    {
        private const int maxObjectExcelReturned = 2300000;
        private DataServiceRepository<BusinessNS.LinxAutoSetupDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.LinxAutoSetupDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.LinxAutoSetupDomainService>(typeof(BusinessNS.AmbienteInfo), typeof(BusinessNS.LjvCanalVenda), typeof(BusinessNS.MultimarcaInfo), typeof(BusinessNS.TbcBandeiraRede), typeof(BusinessNS.TbcFilial), typeof(BusinessNS.TbcGrupoEconomico), typeof(BusinessNS.TcsAmbiente), typeof(BusinessNS.TcsAmbienteConexao), typeof(BusinessNS.TcsAmbienteInfo), typeof(BusinessNS.TcsAmbienteUsuarioAcesso), typeof(BusinessNS.TcsEmpresaAutenticacao), typeof(BusinessNS.TcsEmpresaAutenticacaoModulo), typeof(BusinessNS.TcsEmpresaGpecon), typeof(BusinessNS.TcsModuloGrupo), typeof(BusinessNS.TcsModuloGrupoDetalhe), typeof(BusinessNS.TcsParametroAutorizacao), typeof(BusinessNS.TcsParametroValor), typeof(BusinessNS.TcsPerfil), typeof(BusinessNS.TcsPerfilRegraModulo), typeof(BusinessNS.TcsPerfilUsuario), typeof(BusinessNS.TcsUsuarioAutenticacao), typeof(BusinessNS.TcsUsuarioAutenticacaoAcesso), typeof(BusinessNS.TcsUsuarioPerfil)); _repository.Context.IsSecure = true; } return _repository; } }
        public LinxFrameworkLinxAutoSetupController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(LinxFrameworkLinxAutoSetupController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.LinxAutoSetupDomainService).Assembly.FullName,
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
            var result = repository.Context.GetMetaDataObject("Linx.Framework.Setup.LinxAutoSetup." + entityName, false, true);
            return (allComposition ? result : result.Where(e => e.ClassName == entityName).ToList());
        }
        
        [Route("GetSecurityInfo"), System.Web.Http.HttpGet()]
        public string[] GetSecurityInfo()
        {
           return new string[] { "Linx.Framework.Setup", "LinxFrameworkLinxAutoSetup", "LinxFrameworkLinxAutoSetup/ActionName" };
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
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetReportDataSource"), System.Web.Http.HttpGet()]
        public string GetReportDataSource()
        {
            var zip = new LinxZip();
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetDomainsInfo"), System.Web.Http.HttpGet()]
        public string[] GetDomainsInfo(string domainNames)
        {
            return Linx.Framework.Setup.Domains.DomainHelper.GetDomainsInfo(domainNames);
        }
        
        [Route("GetDomainValues"), System.Web.Http.HttpGet()]
        public Dictionary<string, string> GetDomainValues(string domainName)
        {
            return Linx.Framework.Setup.Domains.DomainHelper.GetDomainValues(domainName);
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Setup.LinxAutoSetup.TcsEmpresaAutenticacao");
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Setup.LinxAutoSetup.TcsEmpresaAutenticacao", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.Setup.Reports", DataSourceFullName = "Linx.Framework.Setup.Reports.LinxAutoSetupDataSource", DataSourceObject = "GetTcsEmpresaAutenticacao", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.dll"));
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
        
        [Route("GetTcsEmpresaAutenticacaoModulo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsEmpresaAutenticacaoModulo> GetTcsEmpresaAutenticacaoModulo()
        {
            return repository.Context.GetTcsEmpresaAutenticacaoModulo();
        }
        
        [Route("GetTcsEmpresaAutenticacaoModuloNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsEmpresaAutenticacaoModulo> GetTcsEmpresaAutenticacaoModuloNoAssociations()
        {
            return repository.Context.GetTcsEmpresaAutenticacaoModuloNoAssociations();
        }
        
        [Route("GetTcsEmpresaAutenticacaoModuloByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsEmpresaAutenticacaoModulo> GetTcsEmpresaAutenticacaoModuloByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsEmpresaAutenticacaoModuloByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsEmpresaAutenticacaoModulo), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsEmpresaAutenticacaoModuloByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsEmpresaAutenticacaoModulo> GetTcsEmpresaAutenticacaoModuloByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsEmpresaAutenticacaoModuloByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsEmpresaAutenticacaoModulo), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsEmpresaAutenticacaoModuloToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsEmpresaAutenticacaoModuloToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsEmpresaAutenticacaoModulo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsEmpresaAutenticacaoModuloByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsEmpresaModulo asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Setup.LinxAutoSetup.TcsEmpresaAutenticacaoModulo");
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
               return ExcelExportPagination<BusinessNS.TcsEmpresaAutenticacaoModulo>.CreateExcelDocumentFileMapPath("TcsEmpresaAutenticacaoModulo",new ExcelExportPagination<BusinessNS.TcsEmpresaAutenticacaoModulo>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsEmpresaAutenticacaoModuloToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsEmpresaAutenticacaoModuloToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsEmpresaAutenticacaoModulo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsEmpresaAutenticacaoModuloByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Setup.LinxAutoSetup.TcsEmpresaAutenticacaoModulo", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.Setup.Reports", DataSourceFullName = "Linx.Framework.Setup.Reports.LinxAutoSetupDataSource", DataSourceObject = "GetTcsEmpresaAutenticacaoModulo", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsEmpresaAutenticacaoModulo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsEmpresaAutenticacaoModulo> GetSampleTcsEmpresaAutenticacaoModulo(string details)
        {
            var result = repository.Context.GetTcsEmpresaAutenticacaoModuloByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
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
            var entities = repository.Context.GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdUsuario asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Setup.LinxAutoSetup.TcsUsuarioAutenticacao");
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Setup.LinxAutoSetup.TcsUsuarioAutenticacao", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.Setup.Reports", DataSourceFullName = "Linx.Framework.Setup.Reports.LinxAutoSetupDataSource", DataSourceObject = "GetTcsUsuarioAutenticacao", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsUsuarioAutenticacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioAutenticacao> GetSampleTcsUsuarioAutenticacao(string details)
        {
            var result = repository.Context.GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations(null).Take(100).ToList();
               if (!details.IsNullOrEmpty())
               {
                   foreach(var entity in result)
                   {
                       entity.FillDetails(repository.Context, null, null, details.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries), 5);
                   }
               }
            return result.AsQueryable();
        }
        
        [Route("GetTcsUsuarioAutenticacaoAcesso"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioAutenticacaoAcesso> GetTcsUsuarioAutenticacaoAcesso()
        {
            return repository.Context.GetTcsUsuarioAutenticacaoAcesso();
        }
        
        [Route("GetTcsUsuarioAutenticacaoAcessoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioAutenticacaoAcesso> GetTcsUsuarioAutenticacaoAcessoNoAssociations()
        {
            return repository.Context.GetTcsUsuarioAutenticacaoAcessoNoAssociations();
        }
        
        [Route("GetTcsUsuarioAutenticacaoAcessoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioAutenticacaoAcesso> GetTcsUsuarioAutenticacaoAcessoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsUsuarioAutenticacaoAcessoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioAutenticacaoAcesso), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsUsuarioAutenticacaoAcessoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioAutenticacaoAcesso> GetTcsUsuarioAutenticacaoAcessoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsUsuarioAutenticacaoAcessoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioAutenticacaoAcesso), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsUsuarioAutenticacaoAcessoToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsUsuarioAutenticacaoAcessoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioAutenticacaoAcesso), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsUsuarioAutenticacaoAcessoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsUsuarioAcesso asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Setup.LinxAutoSetup.TcsUsuarioAutenticacaoAcesso");
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
               return ExcelExportPagination<BusinessNS.TcsUsuarioAutenticacaoAcesso>.CreateExcelDocumentFileMapPath("TcsUsuarioAutenticacaoAcesso",new ExcelExportPagination<BusinessNS.TcsUsuarioAutenticacaoAcesso>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsUsuarioAutenticacaoAcessoToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsUsuarioAutenticacaoAcessoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioAutenticacaoAcesso), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsUsuarioAutenticacaoAcessoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Setup.LinxAutoSetup.TcsUsuarioAutenticacaoAcesso", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.Setup.Reports", DataSourceFullName = "Linx.Framework.Setup.Reports.LinxAutoSetupDataSource", DataSourceObject = "GetTcsUsuarioAutenticacaoAcesso", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsUsuarioAutenticacaoAcesso"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioAutenticacaoAcesso> GetSampleTcsUsuarioAutenticacaoAcesso(string details)
        {
            var result = repository.Context.GetTcsUsuarioAutenticacaoAcessoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("GetTcsUsuarioPerfil"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioPerfil> GetTcsUsuarioPerfil()
        {
            return repository.Context.GetTcsUsuarioPerfil();
        }
        
        [Route("GetTcsUsuarioPerfilNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioPerfil> GetTcsUsuarioPerfilNoAssociations()
        {
            return repository.Context.GetTcsUsuarioPerfilNoAssociations();
        }
        
        [Route("GetTcsUsuarioPerfilByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioPerfil> GetTcsUsuarioPerfilByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsUsuarioPerfilByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioPerfil), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsUsuarioPerfilByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioPerfil> GetTcsUsuarioPerfilByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsUsuarioPerfilByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioPerfil), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsUsuarioPerfilToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsUsuarioPerfilToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioPerfil), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsUsuarioPerfilByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsUsuarioPerfil asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Setup.LinxAutoSetup.TcsUsuarioPerfil");
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
               return ExcelExportPagination<BusinessNS.TcsUsuarioPerfil>.CreateExcelDocumentFileMapPath("TcsUsuarioPerfil",new ExcelExportPagination<BusinessNS.TcsUsuarioPerfil>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsUsuarioPerfilToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsUsuarioPerfilToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioPerfil), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsUsuarioPerfilByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Setup.LinxAutoSetup.TcsUsuarioPerfil", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.Setup.Reports", DataSourceFullName = "Linx.Framework.Setup.Reports.LinxAutoSetupDataSource", DataSourceObject = "GetTcsUsuarioPerfil", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsUsuarioPerfil"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioPerfil> GetSampleTcsUsuarioPerfil(string details)
        {
            var result = repository.Context.GetTcsUsuarioPerfilByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Setup.LinxAutoSetup.TcsAmbiente");
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Setup.LinxAutoSetup.TcsAmbiente", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.Setup.Reports", DataSourceFullName = "Linx.Framework.Setup.Reports.LinxAutoSetupDataSource", DataSourceObject = "GetTcsAmbiente", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.dll"));
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Setup.LinxAutoSetup.TcsAmbienteConexao");
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Setup.LinxAutoSetup.TcsAmbienteConexao", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.Setup.Reports", DataSourceFullName = "Linx.Framework.Setup.Reports.LinxAutoSetupDataSource", DataSourceObject = "GetTcsAmbienteConexao", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsAmbienteConexao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbienteConexao> GetSampleTcsAmbienteConexao(string details)
        {
            var result = repository.Context.GetTcsAmbienteConexaoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Setup.LinxAutoSetup.TcsAmbienteUsuarioAcesso");
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Setup.LinxAutoSetup.TcsAmbienteUsuarioAcesso", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.Setup.Reports", DataSourceFullName = "Linx.Framework.Setup.Reports.LinxAutoSetupDataSource", DataSourceObject = "GetTcsAmbienteUsuarioAcesso", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsAmbienteUsuarioAcesso"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbienteUsuarioAcesso> GetSampleTcsAmbienteUsuarioAcesso(string details)
        {
            var result = repository.Context.GetTcsAmbienteUsuarioAcessoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("GetTcsModuloGrupo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloGrupo> GetTcsModuloGrupo()
        {
            return repository.Context.GetTcsModuloGrupo();
        }
        
        [Route("GetTcsModuloGrupoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloGrupo> GetTcsModuloGrupoNoAssociations()
        {
            return repository.Context.GetTcsModuloGrupoNoAssociations();
        }
        
        [Route("GetTcsModuloGrupoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloGrupo> GetTcsModuloGrupoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsModuloGrupoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloGrupo), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsModuloGrupoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloGrupo> GetTcsModuloGrupoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsModuloGrupoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloGrupo), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsModuloGrupoToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsModuloGrupoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloGrupo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsModuloGrupoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdGrupoModulo asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Setup.LinxAutoSetup.TcsModuloGrupo");
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
               return ExcelExportPagination<BusinessNS.TcsModuloGrupo>.CreateExcelDocumentFileMapPath("TcsModuloGrupo",new ExcelExportPagination<BusinessNS.TcsModuloGrupo>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsModuloGrupoToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsModuloGrupoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloGrupo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsModuloGrupoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Setup.LinxAutoSetup.TcsModuloGrupo", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.Setup.Reports", DataSourceFullName = "Linx.Framework.Setup.Reports.LinxAutoSetupDataSource", DataSourceObject = "GetTcsModuloGrupo", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsModuloGrupo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloGrupo> GetSampleTcsModuloGrupo(string details)
        {
            var result = repository.Context.GetTcsModuloGrupoByEntitySearchNoAssociations(null).Take(100).ToList();
               if (!details.IsNullOrEmpty())
               {
                   foreach(var entity in result)
                   {
                       entity.FillDetails(repository.Context, null, null, details.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries), 5);
                   }
               }
            return result.AsQueryable();
        }
        
        [Route("GetTcsModuloGrupoDetalhe"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloGrupoDetalhe> GetTcsModuloGrupoDetalhe()
        {
            return repository.Context.GetTcsModuloGrupoDetalhe();
        }
        
        [Route("GetTcsModuloGrupoDetalheNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloGrupoDetalhe> GetTcsModuloGrupoDetalheNoAssociations()
        {
            return repository.Context.GetTcsModuloGrupoDetalheNoAssociations();
        }
        
        [Route("GetTcsModuloGrupoDetalheByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloGrupoDetalhe> GetTcsModuloGrupoDetalheByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsModuloGrupoDetalheByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloGrupoDetalhe), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsModuloGrupoDetalheByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloGrupoDetalhe> GetTcsModuloGrupoDetalheByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsModuloGrupoDetalheByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloGrupoDetalhe), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsModuloGrupoDetalheToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsModuloGrupoDetalheToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloGrupoDetalhe), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsModuloGrupoDetalheByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdModuloDoGrupo asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Setup.LinxAutoSetup.TcsModuloGrupoDetalhe");
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
               return ExcelExportPagination<BusinessNS.TcsModuloGrupoDetalhe>.CreateExcelDocumentFileMapPath("TcsModuloGrupoDetalhe",new ExcelExportPagination<BusinessNS.TcsModuloGrupoDetalhe>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsModuloGrupoDetalheToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsModuloGrupoDetalheToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloGrupoDetalhe), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsModuloGrupoDetalheByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Setup.LinxAutoSetup.TcsModuloGrupoDetalhe", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.Setup.Reports", DataSourceFullName = "Linx.Framework.Setup.Reports.LinxAutoSetupDataSource", DataSourceObject = "GetTcsModuloGrupoDetalhe", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsModuloGrupoDetalhe"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloGrupoDetalhe> GetSampleTcsModuloGrupoDetalhe(string details)
        {
            var result = repository.Context.GetTcsModuloGrupoDetalheByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("GetTcsParametroValor"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValor> GetTcsParametroValor()
        {
            return repository.Context.GetTcsParametroValor();
        }
        
        [Route("GetTcsParametroValorNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValor> GetTcsParametroValorNoAssociations()
        {
            return repository.Context.GetTcsParametroValorNoAssociations();
        }
        
        [Route("GetTcsParametroValorByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValor> GetTcsParametroValorByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroValorByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValor), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsParametroValorByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValor> GetTcsParametroValorByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroValorByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValor), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsParametroValorToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsParametroValorToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValor), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroValorByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdParametroValor asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Setup.LinxAutoSetup.TcsParametroValor");
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
               return ExcelExportPagination<BusinessNS.TcsParametroValor>.CreateExcelDocumentFileMapPath("TcsParametroValor",new ExcelExportPagination<BusinessNS.TcsParametroValor>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsParametroValorToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsParametroValorToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValor), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroValorByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Setup.LinxAutoSetup.TcsParametroValor", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.Setup.Reports", DataSourceFullName = "Linx.Framework.Setup.Reports.LinxAutoSetupDataSource", DataSourceObject = "GetTcsParametroValor", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsParametroValor"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValor> GetSampleTcsParametroValor(string details)
        {
            var result = repository.Context.GetTcsParametroValorByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("GetTcsPerfil"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfil> GetTcsPerfil()
        {
            return repository.Context.GetTcsPerfil();
        }
        
        [Route("GetTcsPerfilNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfil> GetTcsPerfilNoAssociations()
        {
            return repository.Context.GetTcsPerfilNoAssociations();
        }
        
        [Route("GetTcsPerfilByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfil> GetTcsPerfilByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsPerfilByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfil), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsPerfilByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfil> GetTcsPerfilByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsPerfilByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfil), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsPerfilToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsPerfilToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfil), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsPerfilByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdPerfil asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Setup.LinxAutoSetup.TcsPerfil");
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
               return ExcelExportPagination<BusinessNS.TcsPerfil>.CreateExcelDocumentFileMapPath("TcsPerfil",new ExcelExportPagination<BusinessNS.TcsPerfil>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsPerfilToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsPerfilToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfil), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsPerfilByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Setup.LinxAutoSetup.TcsPerfil", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.Setup.Reports", DataSourceFullName = "Linx.Framework.Setup.Reports.LinxAutoSetupDataSource", DataSourceObject = "GetTcsPerfil", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsPerfil"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfil> GetSampleTcsPerfil(string details)
        {
            var result = repository.Context.GetTcsPerfilByEntitySearchNoAssociations(null).Take(100).ToList();
               if (!details.IsNullOrEmpty())
               {
                   foreach(var entity in result)
                   {
                       entity.FillDetails(repository.Context, null, null, details.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries), 5);
                   }
               }
            return result.AsQueryable();
        }
        
        [Route("GetTcsPerfilRegraModulo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilRegraModulo> GetTcsPerfilRegraModulo()
        {
            return repository.Context.GetTcsPerfilRegraModulo();
        }
        
        [Route("GetTcsPerfilRegraModuloNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilRegraModulo> GetTcsPerfilRegraModuloNoAssociations()
        {
            return repository.Context.GetTcsPerfilRegraModuloNoAssociations();
        }
        
        [Route("GetTcsPerfilRegraModuloByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilRegraModulo> GetTcsPerfilRegraModuloByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsPerfilRegraModuloByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilRegraModulo), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsPerfilRegraModuloByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilRegraModulo> GetTcsPerfilRegraModuloByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsPerfilRegraModuloByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilRegraModulo), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsPerfilRegraModuloToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsPerfilRegraModuloToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilRegraModulo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsPerfilRegraModuloByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdPerfilRegraModulo asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Setup.LinxAutoSetup.TcsPerfilRegraModulo");
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
               return ExcelExportPagination<BusinessNS.TcsPerfilRegraModulo>.CreateExcelDocumentFileMapPath("TcsPerfilRegraModulo",new ExcelExportPagination<BusinessNS.TcsPerfilRegraModulo>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsPerfilRegraModuloToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsPerfilRegraModuloToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilRegraModulo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsPerfilRegraModuloByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Setup.LinxAutoSetup.TcsPerfilRegraModulo", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.Setup.Reports", DataSourceFullName = "Linx.Framework.Setup.Reports.LinxAutoSetupDataSource", DataSourceObject = "GetTcsPerfilRegraModulo", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsPerfilRegraModulo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilRegraModulo> GetSampleTcsPerfilRegraModulo(string details)
        {
            var result = repository.Context.GetTcsPerfilRegraModuloByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("GetTcsPerfilUsuario"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilUsuario> GetTcsPerfilUsuario()
        {
            return repository.Context.GetTcsPerfilUsuario();
        }
        
        [Route("GetTcsPerfilUsuarioNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilUsuario> GetTcsPerfilUsuarioNoAssociations()
        {
            return repository.Context.GetTcsPerfilUsuarioNoAssociations();
        }
        
        [Route("GetTcsPerfilUsuarioByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilUsuario> GetTcsPerfilUsuarioByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsPerfilUsuarioByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilUsuario), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsPerfilUsuarioByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilUsuario> GetTcsPerfilUsuarioByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsPerfilUsuarioByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilUsuario), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsPerfilUsuarioToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsPerfilUsuarioToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilUsuario), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsPerfilUsuarioByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsUsuarioPerfil asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Setup.LinxAutoSetup.TcsPerfilUsuario");
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
               return ExcelExportPagination<BusinessNS.TcsPerfilUsuario>.CreateExcelDocumentFileMapPath("TcsPerfilUsuario",new ExcelExportPagination<BusinessNS.TcsPerfilUsuario>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsPerfilUsuarioToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsPerfilUsuarioToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilUsuario), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsPerfilUsuarioByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Setup.LinxAutoSetup.TcsPerfilUsuario", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.Setup.Reports", DataSourceFullName = "Linx.Framework.Setup.Reports.LinxAutoSetupDataSource", DataSourceObject = "GetTcsPerfilUsuario", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsPerfilUsuario"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilUsuario> GetSampleTcsPerfilUsuario(string details)
        {
            var result = repository.Context.GetTcsPerfilUsuarioByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("GetAmbienteInfo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AmbienteInfo> GetAmbienteInfo()
        {
            return repository.Context.GetAmbienteInfo().AsQueryable();
        }
        
        [Route("GetAmbienteInfoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AmbienteInfo> GetAmbienteInfoNoAssociations()
        {
            return repository.Context.GetAmbienteInfoNoAssociations().AsQueryable();
        }
        
        [Route("GetAmbienteInfoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AmbienteInfo> GetAmbienteInfoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetAmbienteInfoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AmbienteInfo), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        
        [Route("GetAmbienteInfoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AmbienteInfo> GetAmbienteInfoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetAmbienteInfoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AmbienteInfo), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        [Route("GetAmbienteInfoToExcel"), System.Web.Http.HttpPost()]
        public string GetAmbienteInfoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AmbienteInfo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetAmbienteInfoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).AsQueryable().OrderBy("IdLinx asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Setup.LinxAutoSetup.AmbienteInfo");
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
               return ExcelExportPagination<BusinessNS.AmbienteInfo>.CreateExcelDocumentFileMapPath("AmbienteInfo",new ExcelExportPagination<BusinessNS.AmbienteInfo>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetAmbienteInfoToReportXml"), System.Web.Http.HttpPost()]
        public string GetAmbienteInfoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AmbienteInfo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetAmbienteInfoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Setup.LinxAutoSetup.AmbienteInfo", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.Setup.Reports", DataSourceFullName = "Linx.Framework.Setup.Reports.LinxAutoSetupDataSource", DataSourceObject = "GetAmbienteInfo", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleAmbienteInfo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AmbienteInfo> GetSampleAmbienteInfo(string details)
        {
            var result = repository.Context.GetAmbienteInfoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
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
            var entities = repository.Context.GetTcsEmpresaGpeconByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdLinx asc, IdLinxGpecon asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Setup.LinxAutoSetup.TcsEmpresaGpecon");
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Setup.LinxAutoSetup.TcsEmpresaGpecon", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.Setup.Reports", DataSourceFullName = "Linx.Framework.Setup.Reports.LinxAutoSetupDataSource", DataSourceObject = "GetTcsEmpresaGpecon", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsEmpresaGpecon"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsEmpresaGpecon> GetSampleTcsEmpresaGpecon(string details)
        {
            var result = repository.Context.GetTcsEmpresaGpeconByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("GetTcsAmbienteInfo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbienteInfo> GetTcsAmbienteInfo()
        {
            return repository.Context.GetTcsAmbienteInfo();
        }
        
        [Route("GetTcsAmbienteInfoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbienteInfo> GetTcsAmbienteInfoNoAssociations()
        {
            return repository.Context.GetTcsAmbienteInfoNoAssociations();
        }
        
        [Route("GetTcsAmbienteInfoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbienteInfo> GetTcsAmbienteInfoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsAmbienteInfoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteInfo), jEntitySearch, false, true, false), jEntitySearch);
        }
        
        [Route("GetTcsAmbienteInfoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbienteInfo> GetTcsAmbienteInfoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsAmbienteInfoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteInfo), jEntitySearch, false, true, false), jEntitySearch);
        }
        [Route("GetTcsAmbienteInfoToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsAmbienteInfoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteInfo), jEntitySearch, false, true, false);
            var entities = repository.Context.GetTcsAmbienteInfoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsAmbiente asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Setup.LinxAutoSetup.TcsAmbienteInfo");
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
               return ExcelExportPagination<BusinessNS.TcsAmbienteInfo>.CreateExcelDocumentFileMapPath("TcsAmbienteInfo",new ExcelExportPagination<BusinessNS.TcsAmbienteInfo>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsAmbienteInfoToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsAmbienteInfoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteInfo), jEntitySearch, false, true, false);
            var entities = repository.Context.GetTcsAmbienteInfoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Setup.LinxAutoSetup.TcsAmbienteInfo", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.Setup.Reports", DataSourceFullName = "Linx.Framework.Setup.Reports.LinxAutoSetupDataSource", DataSourceObject = "GetTcsAmbienteInfo", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsAmbienteInfo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbienteInfo> GetSampleTcsAmbienteInfo(string details)
        {
            var result = repository.Context.GetTcsAmbienteInfoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Setup.LinxAutoSetup.TcsParametroAutorizacao");
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Setup.LinxAutoSetup.TcsParametroAutorizacao", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.Setup.Reports", DataSourceFullName = "Linx.Framework.Setup.Reports.LinxAutoSetupDataSource", DataSourceObject = "GetTcsParametroAutorizacao", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsParametroAutorizacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroAutorizacao> GetSampleTcsParametroAutorizacao(string details)
        {
            var result = repository.Context.GetTcsParametroAutorizacaoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("GetMultimarcaInfo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.MultimarcaInfo> GetMultimarcaInfo()
        {
            return repository.Context.GetMultimarcaInfo().AsQueryable();
        }
        
        [Route("GetMultimarcaInfoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.MultimarcaInfo> GetMultimarcaInfoNoAssociations()
        {
            return repository.Context.GetMultimarcaInfoNoAssociations().AsQueryable();
        }
        
        [Route("GetMultimarcaInfoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.MultimarcaInfo> GetMultimarcaInfoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetMultimarcaInfoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.MultimarcaInfo), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        
        [Route("GetMultimarcaInfoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.MultimarcaInfo> GetMultimarcaInfoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetMultimarcaInfoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.MultimarcaInfo), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        [Route("GetMultimarcaInfoToExcel"), System.Web.Http.HttpPost()]
        public string GetMultimarcaInfoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.MultimarcaInfo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetMultimarcaInfoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).AsQueryable().OrderBy("IdLinx asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Setup.LinxAutoSetup.MultimarcaInfo");
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
               return ExcelExportPagination<BusinessNS.MultimarcaInfo>.CreateExcelDocumentFileMapPath("MultimarcaInfo",new ExcelExportPagination<BusinessNS.MultimarcaInfo>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetMultimarcaInfoToReportXml"), System.Web.Http.HttpPost()]
        public string GetMultimarcaInfoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.MultimarcaInfo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetMultimarcaInfoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Setup.LinxAutoSetup.MultimarcaInfo", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.Setup.Reports", DataSourceFullName = "Linx.Framework.Setup.Reports.LinxAutoSetupDataSource", DataSourceObject = "GetMultimarcaInfo", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleMultimarcaInfo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.MultimarcaInfo> GetSampleMultimarcaInfo(string details)
        {
            var result = repository.Context.GetMultimarcaInfoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("GetTbcFilial"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcFilial> GetTbcFilial()
        {
            return repository.Context.GetTbcFilial();
        }
        
        [Route("GetTbcFilialNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcFilial> GetTbcFilialNoAssociations()
        {
            return repository.Context.GetTbcFilialNoAssociations();
        }
        
        [Route("GetTbcFilialByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcFilial> GetTbcFilialByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTbcFilialByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcFilial), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTbcFilialByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcFilial> GetTbcFilialByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTbcFilialByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcFilial), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTbcFilialToExcel"), System.Web.Http.HttpPost()]
        public string GetTbcFilialToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcFilial), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTbcFilialByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdPfj asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Setup.LinxAutoSetup.TbcFilial");
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
               return ExcelExportPagination<BusinessNS.TbcFilial>.CreateExcelDocumentFileMapPath("TbcFilial",new ExcelExportPagination<BusinessNS.TbcFilial>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTbcFilialToReportXml"), System.Web.Http.HttpPost()]
        public string GetTbcFilialToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcFilial), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTbcFilialByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Setup.LinxAutoSetup.TbcFilial", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.Setup.Reports", DataSourceFullName = "Linx.Framework.Setup.Reports.LinxAutoSetupDataSource", DataSourceObject = "GetTbcFilial", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTbcFilial"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcFilial> GetSampleTbcFilial(string details)
        {
            var result = repository.Context.GetTbcFilialByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("GetTbcGrupoEconomico"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcGrupoEconomico> GetTbcGrupoEconomico()
        {
            return repository.Context.GetTbcGrupoEconomico();
        }
        
        [Route("GetTbcGrupoEconomicoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcGrupoEconomico> GetTbcGrupoEconomicoNoAssociations()
        {
            return repository.Context.GetTbcGrupoEconomicoNoAssociations();
        }
        
        [Route("GetTbcGrupoEconomicoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcGrupoEconomico> GetTbcGrupoEconomicoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTbcGrupoEconomicoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcGrupoEconomico), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTbcGrupoEconomicoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcGrupoEconomico> GetTbcGrupoEconomicoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTbcGrupoEconomicoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcGrupoEconomico), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTbcGrupoEconomicoToExcel"), System.Web.Http.HttpPost()]
        public string GetTbcGrupoEconomicoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcGrupoEconomico), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTbcGrupoEconomicoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdGpeconCadastro asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Setup.LinxAutoSetup.TbcGrupoEconomico");
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
               return ExcelExportPagination<BusinessNS.TbcGrupoEconomico>.CreateExcelDocumentFileMapPath("TbcGrupoEconomico",new ExcelExportPagination<BusinessNS.TbcGrupoEconomico>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTbcGrupoEconomicoToReportXml"), System.Web.Http.HttpPost()]
        public string GetTbcGrupoEconomicoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcGrupoEconomico), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTbcGrupoEconomicoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Setup.LinxAutoSetup.TbcGrupoEconomico", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.Setup.Reports", DataSourceFullName = "Linx.Framework.Setup.Reports.LinxAutoSetupDataSource", DataSourceObject = "GetTbcGrupoEconomico", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTbcGrupoEconomico"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcGrupoEconomico> GetSampleTbcGrupoEconomico(string details)
        {
            var result = repository.Context.GetTbcGrupoEconomicoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("GetTbcBandeiraRede"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcBandeiraRede> GetTbcBandeiraRede()
        {
            return repository.Context.GetTbcBandeiraRede();
        }
        
        [Route("GetTbcBandeiraRedeNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcBandeiraRede> GetTbcBandeiraRedeNoAssociations()
        {
            return repository.Context.GetTbcBandeiraRedeNoAssociations();
        }
        
        [Route("GetTbcBandeiraRedeByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcBandeiraRede> GetTbcBandeiraRedeByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTbcBandeiraRedeByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcBandeiraRede), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTbcBandeiraRedeByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcBandeiraRede> GetTbcBandeiraRedeByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTbcBandeiraRedeByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcBandeiraRede), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTbcBandeiraRedeToExcel"), System.Web.Http.HttpPost()]
        public string GetTbcBandeiraRedeToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcBandeiraRede), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTbcBandeiraRedeByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdBandeiraRedeCadastro asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Setup.LinxAutoSetup.TbcBandeiraRede");
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
               return ExcelExportPagination<BusinessNS.TbcBandeiraRede>.CreateExcelDocumentFileMapPath("TbcBandeiraRede",new ExcelExportPagination<BusinessNS.TbcBandeiraRede>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTbcBandeiraRedeToReportXml"), System.Web.Http.HttpPost()]
        public string GetTbcBandeiraRedeToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcBandeiraRede), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTbcBandeiraRedeByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Setup.LinxAutoSetup.TbcBandeiraRede", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.Setup.Reports", DataSourceFullName = "Linx.Framework.Setup.Reports.LinxAutoSetupDataSource", DataSourceObject = "GetTbcBandeiraRede", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTbcBandeiraRede"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcBandeiraRede> GetSampleTbcBandeiraRede(string details)
        {
            var result = repository.Context.GetTbcBandeiraRedeByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("GetLjvCanalVenda"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LjvCanalVenda> GetLjvCanalVenda()
        {
            return repository.Context.GetLjvCanalVenda();
        }
        
        [Route("GetLjvCanalVendaNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LjvCanalVenda> GetLjvCanalVendaNoAssociations()
        {
            return repository.Context.GetLjvCanalVendaNoAssociations();
        }
        
        [Route("GetLjvCanalVendaByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LjvCanalVenda> GetLjvCanalVendaByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetLjvCanalVendaByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LjvCanalVenda), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetLjvCanalVendaByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LjvCanalVenda> GetLjvCanalVendaByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetLjvCanalVendaByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LjvCanalVenda), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetLjvCanalVendaToExcel"), System.Web.Http.HttpPost()]
        public string GetLjvCanalVendaToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LjvCanalVenda), jEntitySearch, false, false, false);
            var entities = repository.Context.GetLjvCanalVendaByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdLjvCanalVenda asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Setup.LinxAutoSetup.LjvCanalVenda");
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
               return ExcelExportPagination<BusinessNS.LjvCanalVenda>.CreateExcelDocumentFileMapPath("LjvCanalVenda",new ExcelExportPagination<BusinessNS.LjvCanalVenda>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetLjvCanalVendaToReportXml"), System.Web.Http.HttpPost()]
        public string GetLjvCanalVendaToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LjvCanalVenda), jEntitySearch, false, false, false);
            var entities = repository.Context.GetLjvCanalVendaByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Setup.LinxAutoSetup.LjvCanalVenda", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.Setup.Reports", DataSourceFullName = "Linx.Framework.Setup.Reports.LinxAutoSetupDataSource", DataSourceObject = "GetLjvCanalVenda", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Setup.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleLjvCanalVenda"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LjvCanalVenda> GetSampleLjvCanalVenda(string details)
        {
            var result = repository.Context.GetLjvCanalVendaByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
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
    
    public partial class LinxFrameworkLinxAutoSetupFeedController : ODataController
    {
        private BusinessNS.LinxAutoSetupDomainService _context;
        public BusinessNS.LinxAutoSetupDomainService Context { get {  if (_context == null) { _context = new BusinessNS.LinxAutoSetupDomainService(); _context.IsSecure = true; } return _context; }  }
        
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
        public IQueryable<BusinessNS.TcsEmpresaAutenticacaoModulo> GetTcsEmpresaAutenticacao__TcsEmpresaAutenticacaoModulo(int key0, string navigation)
        {
            var entity = this.Context.GetTcsEmpresaAutenticacaoByKey(key0);
            if (entity != null && navigation == "TcsEmpresaAutenticacaoModuloList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsEmpresaAutenticacaoModulo" });
               return entity.TcsEmpresaAutenticacaoModuloList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsEmpresaAutenticacaoModulo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsEmpresaAutenticacaoModulo> GetTcsEmpresaAutenticacaoModuloById([FromODataUri]Int32 key0)
        {
            var entity = this.Context.GetTcsEmpresaAutenticacaoModuloByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsEmpresaAutenticacaoModulo[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsEmpresaAutenticacaoModulo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsEmpresaAutenticacaoModulo> GetTcsEmpresaAutenticacaoModuloByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsEmpresaAutenticacaoModuloByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsEmpresaAutenticacaoModulo), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsEmpresaAutenticacaoModulo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsEmpresaAutenticacaoModulo> GetTcsEmpresaAutenticacaoModulo()
        {
            return this.Context.GetTcsEmpresaAutenticacaoModuloByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsEmpresaAutenticacao> GetTcsEmpresaAutenticacaoModulo__TcsEmpresaAutenticacao(Int32 key0, string navigation)
        {
            var entity = this.Context.GetTcsEmpresaAutenticacaoModuloByKey(key0);
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
        public IQueryable<BusinessNS.TcsUsuarioAutenticacaoAcesso> GetTcsUsuarioAutenticacao__TcsUsuarioAutenticacaoAcesso(Int64 key0, string navigation)
        {
            var entity = this.Context.GetTcsUsuarioAutenticacaoByKey(key0);
            if (entity != null && navigation == "TcsUsuarioAutenticacaoAcessoList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsUsuarioAutenticacaoAcesso" });
               return entity.TcsUsuarioAutenticacaoAcessoList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsUsuarioAutenticacaoAcesso>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioAutenticacaoAcesso> GetTcsUsuarioAutenticacaoAcessoById([FromODataUri]Int32 key0)
        {
            var entity = this.Context.GetTcsUsuarioAutenticacaoAcessoByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsUsuarioAutenticacaoAcesso[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsUsuarioAutenticacaoAcesso>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioAutenticacaoAcesso> GetTcsUsuarioAutenticacaoAcessoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsUsuarioAutenticacaoAcessoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioAutenticacaoAcesso), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsUsuarioAutenticacaoAcesso>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioAutenticacaoAcesso> GetTcsUsuarioAutenticacaoAcesso()
        {
            return this.Context.GetTcsUsuarioAutenticacaoAcessoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioAutenticacao> GetTcsUsuarioAutenticacaoAcesso__TcsUsuarioAutenticacao(Int32 key0, string navigation)
        {
            var entity = this.Context.GetTcsUsuarioAutenticacaoAcessoByKey(key0);
            if (entity != null && navigation == "TcsUsuarioAutenticacao")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsUsuarioAutenticacao[] { entity.TcsUsuarioAutenticacao }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsUsuarioAutenticacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioPerfil> GetTcsUsuarioPerfilById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetTcsUsuarioPerfilByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsUsuarioPerfil[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsUsuarioPerfil>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioPerfil> GetTcsUsuarioPerfilByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsUsuarioPerfilByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioPerfil), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsUsuarioPerfil>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioPerfil> GetTcsUsuarioPerfil()
        {
            return this.Context.GetTcsUsuarioPerfilByEntitySearchNoAssociations(null).AsQueryable();
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
        public IQueryable<BusinessNS.TcsModuloGrupo> GetTcsModuloGrupoById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetTcsModuloGrupoByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsModuloGrupo[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsModuloGrupo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsModuloGrupo> GetTcsModuloGrupoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsModuloGrupoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloGrupo), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsModuloGrupo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsModuloGrupo> GetTcsModuloGrupo()
        {
            return this.Context.GetTcsModuloGrupoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsModuloGrupoDetalhe> GetTcsModuloGrupo__TcsModuloGrupoDetalhe(Int64 key0, string navigation)
        {
            var entity = this.Context.GetTcsModuloGrupoByKey(key0);
            if (entity != null && navigation == "TcsModuloGrupoDetalheList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsModuloGrupoDetalhe" });
               return entity.TcsModuloGrupoDetalheList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsModuloGrupoDetalhe>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsModuloGrupoDetalhe> GetTcsModuloGrupoDetalheById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetTcsModuloGrupoDetalheByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsModuloGrupoDetalhe[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsModuloGrupoDetalhe>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsModuloGrupoDetalhe> GetTcsModuloGrupoDetalheByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsModuloGrupoDetalheByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloGrupoDetalhe), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsModuloGrupoDetalhe>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsModuloGrupoDetalhe> GetTcsModuloGrupoDetalhe()
        {
            return this.Context.GetTcsModuloGrupoDetalheByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsModuloGrupo> GetTcsModuloGrupoDetalhe__TcsModuloGrupo(Int64 key0, string navigation)
        {
            var entity = this.Context.GetTcsModuloGrupoDetalheByKey(key0);
            if (entity != null && navigation == "TcsModuloGrupo")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsModuloGrupo[] { entity.TcsModuloGrupo }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsModuloGrupo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValor> GetTcsParametroValorById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetTcsParametroValorByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsParametroValor[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsParametroValor>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValor> GetTcsParametroValorByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsParametroValorByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValor), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsParametroValor>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValor> GetTcsParametroValor()
        {
            return this.Context.GetTcsParametroValorByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfil> GetTcsPerfilById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetTcsPerfilByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsPerfil[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsPerfil>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfil> GetTcsPerfilByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsPerfilByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfil), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsPerfil>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfil> GetTcsPerfil()
        {
            return this.Context.GetTcsPerfilByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilRegraModulo> GetTcsPerfil__TcsPerfilRegraModulo(Int64 key0, string navigation)
        {
            var entity = this.Context.GetTcsPerfilByKey(key0);
            if (entity != null && navigation == "TcsPerfilRegraModuloList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsPerfilRegraModulo" });
               return entity.TcsPerfilRegraModuloList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsPerfilRegraModulo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilUsuario> GetTcsPerfil__TcsPerfilUsuario(Int64 key0, string navigation)
        {
            var entity = this.Context.GetTcsPerfilByKey(key0);
            if (entity != null && navigation == "TcsPerfilUsuarioList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsPerfilUsuario" });
               return entity.TcsPerfilUsuarioList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsPerfilUsuario>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilRegraModulo> GetTcsPerfilRegraModuloById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetTcsPerfilRegraModuloByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsPerfilRegraModulo[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsPerfilRegraModulo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilRegraModulo> GetTcsPerfilRegraModuloByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsPerfilRegraModuloByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilRegraModulo), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsPerfilRegraModulo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilRegraModulo> GetTcsPerfilRegraModulo()
        {
            return this.Context.GetTcsPerfilRegraModuloByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfil> GetTcsPerfilRegraModulo__TcsPerfil(Int64 key0, string navigation)
        {
            var entity = this.Context.GetTcsPerfilRegraModuloByKey(key0);
            if (entity != null && navigation == "TcsPerfil")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsPerfil[] { entity.TcsPerfil }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsPerfil>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilUsuario> GetTcsPerfilUsuarioById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetTcsPerfilUsuarioByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsPerfilUsuario[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsPerfilUsuario>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilUsuario> GetTcsPerfilUsuarioByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsPerfilUsuarioByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilUsuario), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsPerfilUsuario>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilUsuario> GetTcsPerfilUsuario()
        {
            return this.Context.GetTcsPerfilUsuarioByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfil> GetTcsPerfilUsuario__TcsPerfil(Int64 key0, string navigation)
        {
            var entity = this.Context.GetTcsPerfilUsuarioByKey(key0);
            if (entity != null && navigation == "TcsPerfil")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsPerfil[] { entity.TcsPerfil }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsPerfil>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.AmbienteInfo> GetAmbienteInfoById([FromODataUri]int key0)
        {
            var entity = this.Context.GetAmbienteInfoByKey(key0);
            if (entity != null)
               return (new BusinessNS.AmbienteInfo[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.AmbienteInfo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.AmbienteInfo> GetAmbienteInfoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetAmbienteInfoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AmbienteInfo), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.AmbienteInfo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.AmbienteInfo> GetAmbienteInfo()
        {
            return this.Context.GetAmbienteInfoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsEmpresaGpecon> GetTcsEmpresaGpeconById([FromODataUri]Int32 key0, [FromODataUri]Int32 key1)
        {
            var entity = this.Context.GetTcsEmpresaGpeconByKey(key0, key1);
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
        public IQueryable<BusinessNS.TcsAmbienteInfo> GetTcsAmbienteInfoById([FromODataUri]int key0)
        {
            var entity = this.Context.GetTcsAmbienteInfoByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsAmbienteInfo[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsAmbienteInfo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsAmbienteInfo> GetTcsAmbienteInfoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsAmbienteInfoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteInfo), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsAmbienteInfo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsAmbienteInfo> GetTcsAmbienteInfo()
        {
            return this.Context.GetTcsAmbienteInfoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
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
        public IQueryable<BusinessNS.MultimarcaInfo> GetMultimarcaInfoById([FromODataUri]int key0)
        {
            var entity = this.Context.GetMultimarcaInfoByKey(key0);
            if (entity != null)
               return (new BusinessNS.MultimarcaInfo[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.MultimarcaInfo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.MultimarcaInfo> GetMultimarcaInfoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetMultimarcaInfoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.MultimarcaInfo), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.MultimarcaInfo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.MultimarcaInfo> GetMultimarcaInfo()
        {
            return this.Context.GetMultimarcaInfoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TbcFilial> GetTbcFilialById([FromODataUri]Int32 key0)
        {
            var entity = this.Context.GetTbcFilialByKey(key0);
            if (entity != null)
               return (new BusinessNS.TbcFilial[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TbcFilial>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TbcFilial> GetTbcFilialByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTbcFilialByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcFilial), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TbcFilial>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TbcFilial> GetTbcFilial()
        {
            return this.Context.GetTbcFilialByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TbcGrupoEconomico> GetTbcGrupoEconomicoById([FromODataUri]Int32 key0)
        {
            var entity = this.Context.GetTbcGrupoEconomicoByKey(key0);
            if (entity != null)
               return (new BusinessNS.TbcGrupoEconomico[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TbcGrupoEconomico>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TbcGrupoEconomico> GetTbcGrupoEconomicoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTbcGrupoEconomicoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcGrupoEconomico), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TbcGrupoEconomico>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TbcGrupoEconomico> GetTbcGrupoEconomico()
        {
            return this.Context.GetTbcGrupoEconomicoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TbcBandeiraRede> GetTbcBandeiraRedeById([FromODataUri]Int32 key0)
        {
            var entity = this.Context.GetTbcBandeiraRedeByKey(key0);
            if (entity != null)
               return (new BusinessNS.TbcBandeiraRede[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TbcBandeiraRede>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TbcBandeiraRede> GetTbcBandeiraRedeByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTbcBandeiraRedeByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcBandeiraRede), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TbcBandeiraRede>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TbcBandeiraRede> GetTbcBandeiraRede()
        {
            return this.Context.GetTbcBandeiraRedeByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.LjvCanalVenda> GetLjvCanalVendaById([FromODataUri]Int32 key0)
        {
            var entity = this.Context.GetLjvCanalVendaByKey(key0);
            if (entity != null)
               return (new BusinessNS.LjvCanalVenda[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.LjvCanalVenda>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.LjvCanalVenda> GetLjvCanalVendaByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetLjvCanalVendaByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LjvCanalVenda), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.LjvCanalVenda>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.LjvCanalVenda> GetLjvCanalVenda()
        {
            return this.Context.GetLjvCanalVendaByEntitySearchNoAssociations(null).AsQueryable();
        }
        #endregion
        
    }
    
    public partial class LinxFrameworkLinxAutoSetupControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return true;
        }
    }
}

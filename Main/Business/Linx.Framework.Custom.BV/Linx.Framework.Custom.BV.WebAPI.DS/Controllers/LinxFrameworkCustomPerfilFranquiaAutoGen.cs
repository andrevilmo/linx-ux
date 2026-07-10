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
using BusinessNS = Linx.Framework.Custom.BV.PerfilFranquia;

namespace Linx.Framework.Custom.BV.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/LinxFrameworkCustomPerfilFranquia/[ActionName]
    // Security Information Call: http://localhost:1710/LinxFrameworkCustomPerfilFranquia/GetSecurityInfo
    // Entities Catalog Call: http://localhost:1710/LinxFrameworkCustomPerfilFranquia/GetEntities
    // Entity MetaData Call: http://localhost:1710/LinxFrameworkCustomPerfilFranquia/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/LinxFrameworkCustomPerfilFranquia/GetClientDomains?erp=true
    // Client Service Call: http://localhost:1710/LinxFrameworkCustomPerfilFranquia/GetClientService?erp=true
    // Client Factory Call: http://localhost:1710/LinxFrameworkCustomPerfilFranquia/GetClientFactory?entityName=[EntityName]&erp=true
    // Client Factory Custom Events Call: http://localhost:1710/LinxFrameworkCustomPerfilFranquia/GetClientFactoryCustomEvents?entityName=[EntityName]&erp=true
    // Help Call: http://localhost:1710/HelpController/LinxFrameworkCustomPerfilFranquia
    // Feed OData Call: http://localhost:1710/LinxFrameworkCustomPerfilFranquiaOData
    [RoutePrefix("LinxFrameworkCustomPerfilFranquia")]
    [Breeze.WebApi2.BreezeController]
    [ODataBasicAuthenticationFilter]
    public partial class LinxFrameworkCustomPerfilFranquiaController : ApiController
    {
        private const int maxObjectExcelReturned = 2300000;
        private DataServiceRepository<BusinessNS.PerfilFranquiaDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.PerfilFranquiaDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.PerfilFranquiaDomainService>(typeof(BusinessNS.SyncInfo), typeof(BusinessNS.TbcFilial), typeof(BusinessNS.TcsPerfil), typeof(BusinessNS.TcsPerfilBandeiraRede), typeof(BusinessNS.TcsPerfilFilial), typeof(BusinessNS.TcsPerfilRegraModulo), typeof(BusinessNS.TcsPerfilRegraTransacao), typeof(BusinessNS.TcsUsuarioPerfil)); _repository.Context.IsSecure = true; } return _repository; } }
        public LinxFrameworkCustomPerfilFranquiaController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(LinxFrameworkCustomPerfilFranquiaController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.PerfilFranquiaDomainService).Assembly.FullName,
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
            var result = repository.Context.GetMetaDataObject("Linx.Framework.Custom.BV.PerfilFranquia." + entityName, false, true);
            return (allComposition ? result : result.Where(e => e.ClassName == entityName).ToList());
        }
        
        [Route("GetSecurityInfo"), System.Web.Http.HttpGet()]
        public string[] GetSecurityInfo()
        {
           return new string[] { "Linx.Framework.Custom.BV", "LinxFrameworkCustomPerfilFranquia", "LinxFrameworkCustomPerfilFranquia/ActionName" };
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
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Custom.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Custom.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetReportDataSource"), System.Web.Http.HttpGet()]
        public string GetReportDataSource()
        {
            var zip = new LinxZip();
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Custom.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Custom.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetDomainsInfo"), System.Web.Http.HttpGet()]
        public string[] GetDomainsInfo(string domainNames)
        {
            return Linx.Framework.Custom.BV.Domains.DomainHelper.GetDomainsInfo(domainNames);
        }
        
        [Route("GetDomainValues"), System.Web.Http.HttpGet()]
        public Dictionary<string, string> GetDomainValues(string domainName)
        {
            return Linx.Framework.Custom.BV.Domains.DomainHelper.GetDomainValues(domainName);
        }
        
        #region Get LookUps
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetAllLookUpTcsUsuario"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsUsuario> GetAllLookUpTcsUsuario()
        {
            return repository.Context.GetAllLookUpTcsUsuario();
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetLookUpTcsUsuarioByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsUsuario> GetLookUpTcsUsuarioByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsUsuarioByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetAllLookUpTcsPerfilRegraModulo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsPerfilRegraModulo> GetAllLookUpTcsPerfilRegraModulo()
        {
            return repository.Context.GetAllLookUpTcsPerfilRegraModulo();
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetLookUpTcsPerfilRegraModuloByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsPerfilRegraModulo> GetLookUpTcsPerfilRegraModuloByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsPerfilRegraModuloByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetAllLookUpTbcBandeiraRede"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTbcBandeiraRede> GetAllLookUpTbcBandeiraRede()
        {
            return repository.Context.GetAllLookUpTbcBandeiraRede();
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetLookUpTbcBandeiraRedeByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTbcBandeiraRede> GetLookUpTbcBandeiraRedeByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTbcBandeiraRedeByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetAllLookUpTbcFilial"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTbcFilial> GetAllLookUpTbcFilial()
        {
            return repository.Context.GetAllLookUpTbcFilial();
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetLookUpTbcFilialByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTbcFilial> GetLookUpTbcFilialByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTbcFilialByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetAllLookUpTcsPerfilRegraTransacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsPerfilRegraTransacao> GetAllLookUpTcsPerfilRegraTransacao()
        {
            return repository.Context.GetAllLookUpTcsPerfilRegraTransacao();
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetLookUpTcsPerfilRegraTransacaoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsPerfilRegraTransacao> GetLookUpTcsPerfilRegraTransacaoByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsPerfilRegraTransacaoByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetAllLookUpLxRegraAcessoModulo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpLxRegraAcessoModulo> GetAllLookUpLxRegraAcessoModulo()
        {
            return repository.Context.GetAllLookUpLxRegraAcessoModulo();
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetLookUpLxRegraAcessoModuloByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpLxRegraAcessoModulo> GetLookUpLxRegraAcessoModuloByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpLxRegraAcessoModuloByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetAllLookupLxRegraAcessoTransacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookupLxRegraAcessoTransacao> GetAllLookupLxRegraAcessoTransacao()
        {
            return repository.Context.GetAllLookupLxRegraAcessoTransacao();
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetLookupLxRegraAcessoTransacaoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookupLxRegraAcessoTransacao> GetLookupLxRegraAcessoTransacaoByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookupLxRegraAcessoTransacaoByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
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
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetTcsPerfil"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfil> GetTcsPerfil()
        {
            return repository.Context.GetTcsPerfil();
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetTcsPerfilNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfil> GetTcsPerfilNoAssociations()
        {
            return repository.Context.GetTcsPerfilNoAssociations();
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetTcsPerfilByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfil> GetTcsPerfilByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsPerfilByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfil), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetTcsPerfilByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfil> GetTcsPerfilByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsPerfilByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfil), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsPerfilToExcel"), System.Web.Http.HttpPost()]
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        public string GetTcsPerfilToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfil), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsPerfilByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdPerfil asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Custom.BV.PerfilFranquia.TcsPerfil");
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
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Custom.BV.PerfilFranquia.TcsPerfil", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.Custom.BV.Reports", DataSourceFullName = "Linx.Framework.Custom.BV.Reports.PerfilFranquiaDataSource", DataSourceObject = "GetTcsPerfil", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Custom.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Custom.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
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
        
        [Route("AddTcsPerfilEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsPerfilEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfil), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetTcsPerfilByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfil> GetTcsPerfilByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsPerfilByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetTcsUsuarioPerfil"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioPerfil> GetTcsUsuarioPerfil()
        {
            return repository.Context.GetTcsUsuarioPerfil();
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetTcsUsuarioPerfilNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioPerfil> GetTcsUsuarioPerfilNoAssociations()
        {
            return repository.Context.GetTcsUsuarioPerfilNoAssociations();
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetTcsUsuarioPerfilByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioPerfil> GetTcsUsuarioPerfilByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsUsuarioPerfilByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioPerfil), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetTcsUsuarioPerfilByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioPerfil> GetTcsUsuarioPerfilByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsUsuarioPerfilByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioPerfil), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsUsuarioPerfilToExcel"), System.Web.Http.HttpPost()]
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        public string GetTcsUsuarioPerfilToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioPerfil), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsUsuarioPerfilByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsUsuarioPerfil asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Custom.BV.PerfilFranquia.TcsUsuarioPerfil");
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
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Custom.BV.PerfilFranquia.TcsUsuarioPerfil", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.Custom.BV.Reports", DataSourceFullName = "Linx.Framework.Custom.BV.Reports.PerfilFranquiaDataSource", DataSourceObject = "GetTcsUsuarioPerfil", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Custom.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Custom.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetSampleTcsUsuarioPerfil"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioPerfil> GetSampleTcsUsuarioPerfil(string details)
        {
            var result = repository.Context.GetTcsUsuarioPerfilByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsUsuarioPerfilEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsUsuarioPerfilEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioPerfil), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetTcsUsuarioPerfilByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioPerfil> GetTcsUsuarioPerfilByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsUsuarioPerfilByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetTcsPerfilRegraModulo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilRegraModulo> GetTcsPerfilRegraModulo()
        {
            return repository.Context.GetTcsPerfilRegraModulo();
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetTcsPerfilRegraModuloNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilRegraModulo> GetTcsPerfilRegraModuloNoAssociations()
        {
            return repository.Context.GetTcsPerfilRegraModuloNoAssociations();
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetTcsPerfilRegraModuloByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilRegraModulo> GetTcsPerfilRegraModuloByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsPerfilRegraModuloByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilRegraModulo), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetTcsPerfilRegraModuloByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilRegraModulo> GetTcsPerfilRegraModuloByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsPerfilRegraModuloByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilRegraModulo), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsPerfilRegraModuloToExcel"), System.Web.Http.HttpPost()]
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        public string GetTcsPerfilRegraModuloToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilRegraModulo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsPerfilRegraModuloByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdModulo asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Custom.BV.PerfilFranquia.TcsPerfilRegraModulo");
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
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Custom.BV.PerfilFranquia.TcsPerfilRegraModulo", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.Custom.BV.Reports", DataSourceFullName = "Linx.Framework.Custom.BV.Reports.PerfilFranquiaDataSource", DataSourceObject = "GetTcsPerfilRegraModulo", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Custom.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Custom.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetSampleTcsPerfilRegraModulo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilRegraModulo> GetSampleTcsPerfilRegraModulo(string details)
        {
            var result = repository.Context.GetTcsPerfilRegraModuloByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsPerfilRegraModuloEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsPerfilRegraModuloEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilRegraModulo), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetTcsPerfilRegraModuloByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilRegraModulo> GetTcsPerfilRegraModuloByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsPerfilRegraModuloByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetTcsPerfilRegraTransacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilRegraTransacao> GetTcsPerfilRegraTransacao()
        {
            return repository.Context.GetTcsPerfilRegraTransacao();
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetTcsPerfilRegraTransacaoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilRegraTransacao> GetTcsPerfilRegraTransacaoNoAssociations()
        {
            return repository.Context.GetTcsPerfilRegraTransacaoNoAssociations();
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetTcsPerfilRegraTransacaoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilRegraTransacao> GetTcsPerfilRegraTransacaoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsPerfilRegraTransacaoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilRegraTransacao), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetTcsPerfilRegraTransacaoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilRegraTransacao> GetTcsPerfilRegraTransacaoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsPerfilRegraTransacaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilRegraTransacao), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsPerfilRegraTransacaoToExcel"), System.Web.Http.HttpPost()]
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        public string GetTcsPerfilRegraTransacaoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilRegraTransacao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsPerfilRegraTransacaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTransacao asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Custom.BV.PerfilFranquia.TcsPerfilRegraTransacao");
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
               return ExcelExportPagination<BusinessNS.TcsPerfilRegraTransacao>.CreateExcelDocumentFileMapPath("TcsPerfilRegraTransacao",new ExcelExportPagination<BusinessNS.TcsPerfilRegraTransacao>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsPerfilRegraTransacaoToReportXml"), System.Web.Http.HttpPost()]
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        public string GetTcsPerfilRegraTransacaoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilRegraTransacao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsPerfilRegraTransacaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Custom.BV.PerfilFranquia.TcsPerfilRegraTransacao", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.Custom.BV.Reports", DataSourceFullName = "Linx.Framework.Custom.BV.Reports.PerfilFranquiaDataSource", DataSourceObject = "GetTcsPerfilRegraTransacao", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Custom.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Custom.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetSampleTcsPerfilRegraTransacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilRegraTransacao> GetSampleTcsPerfilRegraTransacao(string details)
        {
            var result = repository.Context.GetTcsPerfilRegraTransacaoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsPerfilRegraTransacaoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsPerfilRegraTransacaoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilRegraTransacao), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetTcsPerfilRegraTransacaoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilRegraTransacao> GetTcsPerfilRegraTransacaoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsPerfilRegraTransacaoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetTcsPerfilBandeiraRede"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilBandeiraRede> GetTcsPerfilBandeiraRede()
        {
            return repository.Context.GetTcsPerfilBandeiraRede();
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetTcsPerfilBandeiraRedeNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilBandeiraRede> GetTcsPerfilBandeiraRedeNoAssociations()
        {
            return repository.Context.GetTcsPerfilBandeiraRedeNoAssociations();
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetTcsPerfilBandeiraRedeByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilBandeiraRede> GetTcsPerfilBandeiraRedeByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsPerfilBandeiraRedeByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilBandeiraRede), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetTcsPerfilBandeiraRedeByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilBandeiraRede> GetTcsPerfilBandeiraRedeByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsPerfilBandeiraRedeByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilBandeiraRede), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsPerfilBandeiraRedeToExcel"), System.Web.Http.HttpPost()]
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        public string GetTcsPerfilBandeiraRedeToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilBandeiraRede), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsPerfilBandeiraRedeByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdBandeiraR asc, IdPerfil asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Custom.BV.PerfilFranquia.TcsPerfilBandeiraRede");
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
               return ExcelExportPagination<BusinessNS.TcsPerfilBandeiraRede>.CreateExcelDocumentFileMapPath("TcsPerfilBandeiraRede",new ExcelExportPagination<BusinessNS.TcsPerfilBandeiraRede>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsPerfilBandeiraRedeToReportXml"), System.Web.Http.HttpPost()]
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        public string GetTcsPerfilBandeiraRedeToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilBandeiraRede), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsPerfilBandeiraRedeByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Custom.BV.PerfilFranquia.TcsPerfilBandeiraRede", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.Custom.BV.Reports", DataSourceFullName = "Linx.Framework.Custom.BV.Reports.PerfilFranquiaDataSource", DataSourceObject = "GetTcsPerfilBandeiraRede", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Custom.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Custom.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetSampleTcsPerfilBandeiraRede"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilBandeiraRede> GetSampleTcsPerfilBandeiraRede(string details)
        {
            var result = repository.Context.GetTcsPerfilBandeiraRedeByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsPerfilBandeiraRedeEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsPerfilBandeiraRedeEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilBandeiraRede), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetTcsPerfilBandeiraRedeByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilBandeiraRede> GetTcsPerfilBandeiraRedeByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsPerfilBandeiraRedeByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetTcsPerfilFilial"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilFilial> GetTcsPerfilFilial()
        {
            return repository.Context.GetTcsPerfilFilial();
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetTcsPerfilFilialNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilFilial> GetTcsPerfilFilialNoAssociations()
        {
            return repository.Context.GetTcsPerfilFilialNoAssociations();
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetTcsPerfilFilialByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilFilial> GetTcsPerfilFilialByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsPerfilFilialByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilFilial), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetTcsPerfilFilialByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilFilial> GetTcsPerfilFilialByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsPerfilFilialByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilFilial), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsPerfilFilialToExcel"), System.Web.Http.HttpPost()]
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        public string GetTcsPerfilFilialToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilFilial), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsPerfilFilialByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsPerfilFilial asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Custom.BV.PerfilFranquia.TcsPerfilFilial");
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
               return ExcelExportPagination<BusinessNS.TcsPerfilFilial>.CreateExcelDocumentFileMapPath("TcsPerfilFilial",new ExcelExportPagination<BusinessNS.TcsPerfilFilial>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsPerfilFilialToReportXml"), System.Web.Http.HttpPost()]
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        public string GetTcsPerfilFilialToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilFilial), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsPerfilFilialByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Custom.BV.PerfilFranquia.TcsPerfilFilial", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.Custom.BV.Reports", DataSourceFullName = "Linx.Framework.Custom.BV.Reports.PerfilFranquiaDataSource", DataSourceObject = "GetTcsPerfilFilial", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Custom.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Custom.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetSampleTcsPerfilFilial"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilFilial> GetSampleTcsPerfilFilial(string details)
        {
            var result = repository.Context.GetTcsPerfilFilialByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsPerfilFilialEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsPerfilFilialEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilFilial), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetTcsPerfilFilialByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilFilial> GetTcsPerfilFilialByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsPerfilFilialByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetTbcFilial"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcFilial> GetTbcFilial()
        {
            return repository.Context.GetTbcFilial();
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetTbcFilialNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcFilial> GetTbcFilialNoAssociations()
        {
            return repository.Context.GetTbcFilialNoAssociations();
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetTbcFilialByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcFilial> GetTbcFilialByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTbcFilialByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcFilial), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetTbcFilialByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcFilial> GetTbcFilialByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTbcFilialByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcFilial), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTbcFilialToExcel"), System.Web.Http.HttpPost()]
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        public string GetTbcFilialToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcFilial), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTbcFilialByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdFilialPfj asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Custom.BV.PerfilFranquia.TbcFilial");
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
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Custom.BV.PerfilFranquia.TbcFilial", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.Custom.BV.Reports", DataSourceFullName = "Linx.Framework.Custom.BV.Reports.PerfilFranquiaDataSource", DataSourceObject = "GetTbcFilial", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Custom.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Custom.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetSampleTbcFilial"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcFilial> GetSampleTbcFilial(string details)
        {
            var result = repository.Context.GetTbcFilialByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTbcFilialEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTbcFilialEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcFilial), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetTbcFilialByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcFilial> GetTbcFilialByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTbcFilialByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetSyncInfo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.SyncInfo> GetSyncInfo()
        {
            return repository.Context.GetSyncInfo().AsQueryable();
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetSyncInfoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.SyncInfo> GetSyncInfoNoAssociations()
        {
            return repository.Context.GetSyncInfoNoAssociations().AsQueryable();
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetSyncInfoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.SyncInfo> GetSyncInfoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetSyncInfoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.SyncInfo), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetSyncInfoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.SyncInfo> GetSyncInfoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetSyncInfoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.SyncInfo), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        [Route("GetSyncInfoToExcel"), System.Web.Http.HttpPost()]
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        public string GetSyncInfoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.SyncInfo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetSyncInfoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).AsQueryable().OrderBy("Operacao asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Custom.BV.PerfilFranquia.SyncInfo");
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
               return ExcelExportPagination<BusinessNS.SyncInfo>.CreateExcelDocumentFileMapPath("SyncInfo",new ExcelExportPagination<BusinessNS.SyncInfo>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetSyncInfoToReportXml"), System.Web.Http.HttpPost()]
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        public string GetSyncInfoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.SyncInfo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetSyncInfoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Custom.BV.PerfilFranquia.SyncInfo", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.Custom.BV.Reports", DataSourceFullName = "Linx.Framework.Custom.BV.Reports.PerfilFranquiaDataSource", DataSourceObject = "GetSyncInfo", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Custom.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Custom.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetSampleSyncInfo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.SyncInfo> GetSampleSyncInfo(string details)
        {
            var result = repository.Context.GetSyncInfoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddSyncInfoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddSyncInfoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.SyncInfo), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetSyncInfoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.SyncInfo> GetSyncInfoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetSyncInfoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        #endregion
        
        #region Get Business Entities By Parent Composition
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetTcsUsuarioPerfilParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioPerfilParentComposition> GetTcsUsuarioPerfilParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsUsuarioPerfilParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioPerfilParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsUsuarioPerfilParentCompositionToExcel"), System.Web.Http.HttpPost()]
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        public string GetTcsUsuarioPerfilParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("TcsUsuarioPerfil{", "TcsUsuarioPerfilParentComposition{");
            jEntitySearch = jEntitySearch.Replace("TcsPerfil{", "TcsUsuarioPerfilParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioPerfilParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsUsuarioPerfilParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsUsuarioPerfil asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Custom.BV.PerfilFranquia.TcsUsuarioPerfil");
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
               return ExcelExportPagination<BusinessNS.TcsUsuarioPerfilParentComposition>.CreateExcelDocumentFileMapPath("TcsUsuarioPerfil",new ExcelExportPagination<BusinessNS.TcsUsuarioPerfilParentComposition>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsUsuarioPerfilParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        public string GetTcsUsuarioPerfilParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioPerfilParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsUsuarioPerfilParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Custom.BV.PerfilFranquia.TcsUsuarioPerfil", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.Custom.BV.Reports", DataSourceFullName = "Linx.Framework.Custom.BV.Reports.PerfilFranquiaDataSource", DataSourceObject = "GetTcsUsuarioPerfilParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Custom.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Custom.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetSampleTcsUsuarioPerfilParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioPerfilParentComposition> GetSampleTcsUsuarioPerfilParentComposition(string details)
        {
            var result = repository.Context.GetTcsUsuarioPerfilParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetTcsPerfilRegraModuloParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilRegraModuloParentComposition> GetTcsPerfilRegraModuloParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsPerfilRegraModuloParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilRegraModuloParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsPerfilRegraModuloParentCompositionToExcel"), System.Web.Http.HttpPost()]
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        public string GetTcsPerfilRegraModuloParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("TcsPerfilRegraModulo{", "TcsPerfilRegraModuloParentComposition{");
            jEntitySearch = jEntitySearch.Replace("TcsPerfil{", "TcsPerfilRegraModuloParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilRegraModuloParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsPerfilRegraModuloParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdModulo asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Custom.BV.PerfilFranquia.TcsPerfilRegraModulo");
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
               return ExcelExportPagination<BusinessNS.TcsPerfilRegraModuloParentComposition>.CreateExcelDocumentFileMapPath("TcsPerfilRegraModulo",new ExcelExportPagination<BusinessNS.TcsPerfilRegraModuloParentComposition>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsPerfilRegraModuloParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        public string GetTcsPerfilRegraModuloParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilRegraModuloParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsPerfilRegraModuloParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Custom.BV.PerfilFranquia.TcsPerfilRegraModulo", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.Custom.BV.Reports", DataSourceFullName = "Linx.Framework.Custom.BV.Reports.PerfilFranquiaDataSource", DataSourceObject = "GetTcsPerfilRegraModuloParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Custom.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Custom.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetSampleTcsPerfilRegraModuloParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilRegraModuloParentComposition> GetSampleTcsPerfilRegraModuloParentComposition(string details)
        {
            var result = repository.Context.GetTcsPerfilRegraModuloParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetTcsPerfilRegraTransacaoParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilRegraTransacaoParentComposition> GetTcsPerfilRegraTransacaoParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsPerfilRegraTransacaoParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilRegraTransacaoParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsPerfilRegraTransacaoParentCompositionToExcel"), System.Web.Http.HttpPost()]
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        public string GetTcsPerfilRegraTransacaoParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("TcsPerfilRegraTransacao{", "TcsPerfilRegraTransacaoParentComposition{");
            jEntitySearch = jEntitySearch.Replace("TcsPerfil{", "TcsPerfilRegraTransacaoParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilRegraTransacaoParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsPerfilRegraTransacaoParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTransacao asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Custom.BV.PerfilFranquia.TcsPerfilRegraTransacao");
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
               return ExcelExportPagination<BusinessNS.TcsPerfilRegraTransacaoParentComposition>.CreateExcelDocumentFileMapPath("TcsPerfilRegraTransacao",new ExcelExportPagination<BusinessNS.TcsPerfilRegraTransacaoParentComposition>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsPerfilRegraTransacaoParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        public string GetTcsPerfilRegraTransacaoParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilRegraTransacaoParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsPerfilRegraTransacaoParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Custom.BV.PerfilFranquia.TcsPerfilRegraTransacao", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.Custom.BV.Reports", DataSourceFullName = "Linx.Framework.Custom.BV.Reports.PerfilFranquiaDataSource", DataSourceObject = "GetTcsPerfilRegraTransacaoParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Custom.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Custom.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetSampleTcsPerfilRegraTransacaoParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilRegraTransacaoParentComposition> GetSampleTcsPerfilRegraTransacaoParentComposition(string details)
        {
            var result = repository.Context.GetTcsPerfilRegraTransacaoParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetTcsPerfilBandeiraRedeParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilBandeiraRedeParentComposition> GetTcsPerfilBandeiraRedeParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsPerfilBandeiraRedeParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilBandeiraRedeParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsPerfilBandeiraRedeParentCompositionToExcel"), System.Web.Http.HttpPost()]
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        public string GetTcsPerfilBandeiraRedeParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("TcsPerfilBandeiraRede{", "TcsPerfilBandeiraRedeParentComposition{");
            jEntitySearch = jEntitySearch.Replace("TcsPerfil{", "TcsPerfilBandeiraRedeParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilBandeiraRedeParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsPerfilBandeiraRedeParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdBandeiraR asc, IdPerfil asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Custom.BV.PerfilFranquia.TcsPerfilBandeiraRede");
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
               return ExcelExportPagination<BusinessNS.TcsPerfilBandeiraRedeParentComposition>.CreateExcelDocumentFileMapPath("TcsPerfilBandeiraRede",new ExcelExportPagination<BusinessNS.TcsPerfilBandeiraRedeParentComposition>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsPerfilBandeiraRedeParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        public string GetTcsPerfilBandeiraRedeParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilBandeiraRedeParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsPerfilBandeiraRedeParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Custom.BV.PerfilFranquia.TcsPerfilBandeiraRede", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.Custom.BV.Reports", DataSourceFullName = "Linx.Framework.Custom.BV.Reports.PerfilFranquiaDataSource", DataSourceObject = "GetTcsPerfilBandeiraRedeParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Custom.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Custom.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetSampleTcsPerfilBandeiraRedeParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilBandeiraRedeParentComposition> GetSampleTcsPerfilBandeiraRedeParentComposition(string details)
        {
            var result = repository.Context.GetTcsPerfilBandeiraRedeParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetTcsPerfilFilialParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilFilialParentComposition> GetTcsPerfilFilialParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsPerfilFilialParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilFilialParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsPerfilFilialParentCompositionToExcel"), System.Web.Http.HttpPost()]
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        public string GetTcsPerfilFilialParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("TcsPerfilFilial{", "TcsPerfilFilialParentComposition{");
            jEntitySearch = jEntitySearch.Replace("TcsPerfil{", "TcsPerfilFilialParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilFilialParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsPerfilFilialParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsPerfilFilial asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Custom.BV.PerfilFranquia.TcsPerfilFilial");
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
               return ExcelExportPagination<BusinessNS.TcsPerfilFilialParentComposition>.CreateExcelDocumentFileMapPath("TcsPerfilFilial",new ExcelExportPagination<BusinessNS.TcsPerfilFilialParentComposition>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsPerfilFilialParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        public string GetTcsPerfilFilialParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilFilialParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsPerfilFilialParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.Custom.BV.PerfilFranquia.TcsPerfilFilial", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.Custom.BV.Reports", DataSourceFullName = "Linx.Framework.Custom.BV.Reports.PerfilFranquiaDataSource", DataSourceObject = "GetTcsPerfilFilialParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Custom.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.Custom.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("GetSampleTcsPerfilFilialParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilFilialParentComposition> GetSampleTcsPerfilFilialParentComposition(string details)
        {
            var result = repository.Context.GetTcsPerfilFilialParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        #endregion
        
        #region Save Changes
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [Route("SaveChanges"), System.Web.Http.HttpPost()]
        public SaveResult SaveChanges(JObject saveBundle)
        {
            var result = repository.SaveChanges(saveBundle);
            repository.Context.Dispose();
            return result;
        }
        #endregion
    }
    
    [ODataBasicAuthenticationFilter]
    public partial class LinxFrameworkCustomPerfilFranquiaFeedController : ODataController
    {
        private BusinessNS.PerfilFranquiaDomainService _context;
        public BusinessNS.PerfilFranquiaDomainService Context { get {  if (_context == null) { _context = new BusinessNS.PerfilFranquiaDomainService(); _context.IsSecure = true; } return _context; }  }
        
        #region Get Action to Business Entities
        
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfil> GetTcsPerfilById([FromODataUri]long key0)
        {
            var entity = this.Context.GetTcsPerfilByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsPerfil[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsPerfil>);
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
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
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfil> GetTcsPerfil()
        {
            return this.Context.GetTcsPerfilByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioPerfil> GetTcsPerfil__TcsUsuarioPerfil(long key0, string navigation)
        {
            var entity = this.Context.GetTcsPerfilByKey(key0);
            if (entity != null && navigation == "TcsUsuarioPerfilList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsUsuarioPerfil" });
               return entity.TcsUsuarioPerfilList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsUsuarioPerfil>);
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilRegraModulo> GetTcsPerfil__TcsPerfilRegraModulo(long key0, string navigation)
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
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilRegraTransacao> GetTcsPerfil__TcsPerfilRegraTransacao(long key0, string navigation)
        {
            var entity = this.Context.GetTcsPerfilByKey(key0);
            if (entity != null && navigation == "TcsPerfilRegraTransacaoList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsPerfilRegraTransacao" });
               return entity.TcsPerfilRegraTransacaoList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsPerfilRegraTransacao>);
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilBandeiraRede> GetTcsPerfil__TcsPerfilBandeiraRede(long key0, string navigation)
        {
            var entity = this.Context.GetTcsPerfilByKey(key0);
            if (entity != null && navigation == "TcsPerfilBandeiraRedeList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsPerfilBandeiraRede" });
               return entity.TcsPerfilBandeiraRedeList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsPerfilBandeiraRede>);
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilFilial> GetTcsPerfil__TcsPerfilFilial(long key0, string navigation)
        {
            var entity = this.Context.GetTcsPerfilByKey(key0);
            if (entity != null && navigation == "TcsPerfilFilialList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsPerfilFilial" });
               return entity.TcsPerfilFilialList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsPerfilFilial>);
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioPerfil> GetTcsUsuarioPerfilById([FromODataUri]long key0)
        {
            var entity = this.Context.GetTcsUsuarioPerfilByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsUsuarioPerfil[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsUsuarioPerfil>);
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
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
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioPerfil> GetTcsUsuarioPerfil()
        {
            return this.Context.GetTcsUsuarioPerfilByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioPerfilParentComposition> GetTcsUsuarioPerfilParentComposition()
        {
            return this.Context.GetTcsUsuarioPerfilParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioPerfilParentComposition> GetTcsUsuarioPerfilParentCompositionByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                jEntitySearch = jEntitySearch.Replace("TcsUsuarioPerfil{", "TcsUsuarioPerfilParentComposition{");
                jEntitySearch = jEntitySearch.Replace("TcsPerfil{", "TcsUsuarioPerfilParentComposition{");
                var entity = this.Context.GetTcsUsuarioPerfilParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioPerfilParentComposition), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsUsuarioPerfilParentComposition>);
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfil> GetTcsUsuarioPerfil__TcsPerfil(long key0, string navigation)
        {
            var entity = this.Context.GetTcsUsuarioPerfilByKey(key0);
            if (entity != null && navigation == "TcsPerfil")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsPerfil[] { entity.TcsPerfil }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsPerfil>);
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilRegraModulo> GetTcsPerfilRegraModuloById([FromODataUri]long key0)
        {
            var entity = this.Context.GetTcsPerfilRegraModuloByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsPerfilRegraModulo[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsPerfilRegraModulo>);
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
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
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilRegraModulo> GetTcsPerfilRegraModulo()
        {
            return this.Context.GetTcsPerfilRegraModuloByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilRegraModuloParentComposition> GetTcsPerfilRegraModuloParentComposition()
        {
            return this.Context.GetTcsPerfilRegraModuloParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilRegraModuloParentComposition> GetTcsPerfilRegraModuloParentCompositionByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                jEntitySearch = jEntitySearch.Replace("TcsPerfilRegraModulo{", "TcsPerfilRegraModuloParentComposition{");
                jEntitySearch = jEntitySearch.Replace("TcsPerfil{", "TcsPerfilRegraModuloParentComposition{");
                var entity = this.Context.GetTcsPerfilRegraModuloParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilRegraModuloParentComposition), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsPerfilRegraModuloParentComposition>);
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfil> GetTcsPerfilRegraModulo__TcsPerfil(long key0, string navigation)
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
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilRegraTransacao> GetTcsPerfilRegraTransacaoById([FromODataUri]long key0)
        {
            var entity = this.Context.GetTcsPerfilRegraTransacaoByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsPerfilRegraTransacao[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsPerfilRegraTransacao>);
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilRegraTransacao> GetTcsPerfilRegraTransacaoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsPerfilRegraTransacaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilRegraTransacao), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsPerfilRegraTransacao>);
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilRegraTransacao> GetTcsPerfilRegraTransacao()
        {
            return this.Context.GetTcsPerfilRegraTransacaoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilRegraTransacaoParentComposition> GetTcsPerfilRegraTransacaoParentComposition()
        {
            return this.Context.GetTcsPerfilRegraTransacaoParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilRegraTransacaoParentComposition> GetTcsPerfilRegraTransacaoParentCompositionByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                jEntitySearch = jEntitySearch.Replace("TcsPerfilRegraTransacao{", "TcsPerfilRegraTransacaoParentComposition{");
                jEntitySearch = jEntitySearch.Replace("TcsPerfil{", "TcsPerfilRegraTransacaoParentComposition{");
                var entity = this.Context.GetTcsPerfilRegraTransacaoParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilRegraTransacaoParentComposition), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsPerfilRegraTransacaoParentComposition>);
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfil> GetTcsPerfilRegraTransacao__TcsPerfil(long key0, string navigation)
        {
            var entity = this.Context.GetTcsPerfilRegraTransacaoByKey(key0);
            if (entity != null && navigation == "TcsPerfil")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsPerfil[] { entity.TcsPerfil }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsPerfil>);
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilBandeiraRede> GetTcsPerfilBandeiraRedeById([FromODataUri]int key0, [FromODataUri]long key1)
        {
            var entity = this.Context.GetTcsPerfilBandeiraRedeByKey(key0, key1);
            if (entity != null)
               return (new BusinessNS.TcsPerfilBandeiraRede[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsPerfilBandeiraRede>);
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilBandeiraRede> GetTcsPerfilBandeiraRedeByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsPerfilBandeiraRedeByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilBandeiraRede), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsPerfilBandeiraRede>);
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilBandeiraRede> GetTcsPerfilBandeiraRede()
        {
            return this.Context.GetTcsPerfilBandeiraRedeByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilBandeiraRedeParentComposition> GetTcsPerfilBandeiraRedeParentComposition()
        {
            return this.Context.GetTcsPerfilBandeiraRedeParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilBandeiraRedeParentComposition> GetTcsPerfilBandeiraRedeParentCompositionByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                jEntitySearch = jEntitySearch.Replace("TcsPerfilBandeiraRede{", "TcsPerfilBandeiraRedeParentComposition{");
                jEntitySearch = jEntitySearch.Replace("TcsPerfil{", "TcsPerfilBandeiraRedeParentComposition{");
                var entity = this.Context.GetTcsPerfilBandeiraRedeParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilBandeiraRedeParentComposition), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsPerfilBandeiraRedeParentComposition>);
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfil> GetTcsPerfilBandeiraRede__TcsPerfil(int key0, long key1, string navigation)
        {
            var entity = this.Context.GetTcsPerfilBandeiraRedeByKey(key0, key1);
            if (entity != null && navigation == "TcsPerfil")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsPerfil[] { entity.TcsPerfil }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsPerfil>);
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilFilial> GetTcsPerfilFilialById([FromODataUri]long key0)
        {
            var entity = this.Context.GetTcsPerfilFilialByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsPerfilFilial[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsPerfilFilial>);
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilFilial> GetTcsPerfilFilialByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsPerfilFilialByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilFilial), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsPerfilFilial>);
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilFilial> GetTcsPerfilFilial()
        {
            return this.Context.GetTcsPerfilFilialByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilFilialParentComposition> GetTcsPerfilFilialParentComposition()
        {
            return this.Context.GetTcsPerfilFilialParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilFilialParentComposition> GetTcsPerfilFilialParentCompositionByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                jEntitySearch = jEntitySearch.Replace("TcsPerfilFilial{", "TcsPerfilFilialParentComposition{");
                jEntitySearch = jEntitySearch.Replace("TcsPerfil{", "TcsPerfilFilialParentComposition{");
                var entity = this.Context.GetTcsPerfilFilialParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilFilialParentComposition), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsPerfilFilialParentComposition>);
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfil> GetTcsPerfilFilial__TcsPerfil(long key0, string navigation)
        {
            var entity = this.Context.GetTcsPerfilFilialByKey(key0);
            if (entity != null && navigation == "TcsPerfil")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsPerfil[] { entity.TcsPerfil }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsPerfil>);
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TbcFilial> GetTbcFilialById([FromODataUri]int key0)
        {
            var entity = this.Context.GetTbcFilialByKey(key0);
            if (entity != null)
               return (new BusinessNS.TbcFilial[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TbcFilial>);
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
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
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TbcFilial> GetTbcFilial()
        {
            return this.Context.GetTbcFilialByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.SyncInfo> GetSyncInfoById([FromODataUri]string key0)
        {
            var entity = this.Context.GetSyncInfoByKey(key0);
            if (entity != null)
               return (new BusinessNS.SyncInfo[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.SyncInfo>);
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.SyncInfo> GetSyncInfoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetSyncInfoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.SyncInfo), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.SyncInfo>);
        }
        
        [LinxFrameworkCustomPerfilFranquiaControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.SyncInfo> GetSyncInfo()
        {
            return this.Context.GetSyncInfoByEntitySearchNoAssociations(null).AsQueryable();
        }
        #endregion
        
    }
    
    public partial class LinxFrameworkCustomPerfilFranquiaControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return LinxAutorization.CheckAuthorization(actionContext, string.Format("{0}#{1}#{1}/{2}", "Linx.Framework.Custom.BV", "LinxFrameworkCustomPerfilFranquia", actionContext.ActionDescriptor.ActionName));
        }
    }
}

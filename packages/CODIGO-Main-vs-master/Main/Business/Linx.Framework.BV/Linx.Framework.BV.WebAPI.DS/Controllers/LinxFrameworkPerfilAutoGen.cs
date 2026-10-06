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
using BusinessNS = Linx.Framework.BV.Perfil;

namespace Linx.Framework.BV.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/LinxFrameworkPerfil/[ActionName]
    // Security Information Call: http://localhost:1710/LinxFrameworkPerfil/GetSecurityInfo
    // Entities Catalog Call: http://localhost:1710/LinxFrameworkPerfil/GetEntities
    // Entity MetaData Call: http://localhost:1710/LinxFrameworkPerfil/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/LinxFrameworkPerfil/GetClientDomains?erp=true
    // Client Service Call: http://localhost:1710/LinxFrameworkPerfil/GetClientService?erp=true
    // Client Factory Call: http://localhost:1710/LinxFrameworkPerfil/GetClientFactory?entityName=[EntityName]&erp=true
    // Client Factory Custom Events Call: http://localhost:1710/LinxFrameworkPerfil/GetClientFactoryCustomEvents?entityName=[EntityName]&erp=true
    // Help Call: http://localhost:1710/HelpController/LinxFrameworkPerfil
    // Feed OData Call: http://localhost:1710/LinxFrameworkPerfilOData
    [RoutePrefix("LinxFrameworkPerfil")]
    [Breeze.WebApi2.BreezeController]
    public partial class LinxFrameworkPerfilController : ApiController
    {
        private const int maxObjectExcelReturned = 2300000;
        private DataServiceRepository<BusinessNS.PerfilDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.PerfilDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.PerfilDomainService>(typeof(BusinessNS.TcsPerfil), typeof(BusinessNS.TcsPerfilBandeiraRede), typeof(BusinessNS.TcsPerfilFilial), typeof(BusinessNS.TcsPerfilLayout), typeof(BusinessNS.TcsPerfilRegraColuna), typeof(BusinessNS.TcsPerfilRegraModulo), typeof(BusinessNS.TcsPerfilRegraTransacao), typeof(BusinessNS.TcsUsuarioPerfil)); _repository.Context.IsSecure = true; } return _repository; } }
        public LinxFrameworkPerfilController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(LinxFrameworkPerfilController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.PerfilDomainService).Assembly.FullName,
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
            var result = repository.Context.GetMetaDataObject("Linx.Framework.BV.Perfil." + entityName, false, true);
            return (allComposition ? result : result.Where(e => e.ClassName == entityName).ToList());
        }
        
        [Route("GetSecurityInfo"), System.Web.Http.HttpGet()]
        public string[] GetSecurityInfo()
        {
           return new string[] { "Linx.Framework.BV", "LinxFrameworkPerfil", "LinxFrameworkPerfil/ActionName" };
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
        
        [Route("GetAllLookUpTcsUsuario"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsUsuario> GetAllLookUpTcsUsuario()
        {
            return repository.Context.GetAllLookUpTcsUsuario();
        }
        
        [Route("GetLookUpTcsUsuarioByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsUsuario> GetLookUpTcsUsuarioByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsUsuarioByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [Route("GetAllLookUpTcsPerfilRegraTransacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsPerfilRegraTransacao> GetAllLookUpTcsPerfilRegraTransacao()
        {
            return repository.Context.GetAllLookUpTcsPerfilRegraTransacao();
        }
        
        [Route("GetLookUpTcsPerfilRegraTransacaoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsPerfilRegraTransacao> GetLookUpTcsPerfilRegraTransacaoByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsPerfilRegraTransacaoByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [Route("GetAllLookUpTcsPerfilRegraColuna"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsPerfilRegraColuna> GetAllLookUpTcsPerfilRegraColuna()
        {
            return repository.Context.GetAllLookUpTcsPerfilRegraColuna();
        }
        
        [Route("GetLookUpTcsPerfilRegraColunaByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsPerfilRegraColuna> GetLookUpTcsPerfilRegraColunaByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsPerfilRegraColunaByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [Route("GetAllLookUpTcsPerfilRegraModulo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsPerfilRegraModulo> GetAllLookUpTcsPerfilRegraModulo()
        {
            return repository.Context.GetAllLookUpTcsPerfilRegraModulo();
        }
        
        [Route("GetLookUpTcsPerfilRegraModuloByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsPerfilRegraModulo> GetLookUpTcsPerfilRegraModuloByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsPerfilRegraModuloByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [Route("GetAllLookUpTbcBandeiraRede"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTbcBandeiraRede> GetAllLookUpTbcBandeiraRede()
        {
            return repository.Context.GetAllLookUpTbcBandeiraRede();
        }
        
        [Route("GetLookUpTbcBandeiraRedeByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTbcBandeiraRede> GetLookUpTbcBandeiraRedeByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTbcBandeiraRedeByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [Route("GetAllLookUpTcsLayout"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsLayout> GetAllLookUpTcsLayout()
        {
            return repository.Context.GetAllLookUpTcsLayout();
        }
        
        [Route("GetLookUpTcsLayoutByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsLayout> GetLookUpTcsLayoutByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsLayoutByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [Route("GetAllLookUpTbcFilial"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTbcFilial> GetAllLookUpTbcFilial()
        {
            return repository.Context.GetAllLookUpTbcFilial();
        }
        
        [Route("GetLookUpTbcFilialByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTbcFilial> GetLookUpTbcFilialByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTbcFilialByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [Route("GetAllLookUpTbcGrupoEconomico"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTbcGrupoEconomico> GetAllLookUpTbcGrupoEconomico()
        {
            return repository.Context.GetAllLookUpTbcGrupoEconomico();
        }
        
        [Route("GetLookUpTbcGrupoEconomicoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTbcGrupoEconomico> GetLookUpTbcGrupoEconomicoByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTbcGrupoEconomicoByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Perfil.TcsPerfil");
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Perfil.TcsPerfil", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.PerfilDataSource", DataSourceObject = "GetTcsPerfil", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
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
        
        [Route("AddTcsPerfilEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsPerfilEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfil), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsPerfilByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfil> GetTcsPerfilByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsPerfilByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Perfil.TcsUsuarioPerfil");
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Perfil.TcsUsuarioPerfil", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.PerfilDataSource", DataSourceObject = "GetTcsUsuarioPerfil", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
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
        
        [Route("GetTcsUsuarioPerfilByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioPerfil> GetTcsUsuarioPerfilByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsUsuarioPerfilByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Perfil.TcsPerfilRegraModulo");
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Perfil.TcsPerfilRegraModulo", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.PerfilDataSource", DataSourceObject = "GetTcsPerfilRegraModulo", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
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
        
        [Route("GetTcsPerfilRegraModuloByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilRegraModulo> GetTcsPerfilRegraModuloByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsPerfilRegraModuloByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsPerfilRegraColuna"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilRegraColuna> GetTcsPerfilRegraColuna()
        {
            return repository.Context.GetTcsPerfilRegraColuna();
        }
        
        [Route("GetTcsPerfilRegraColunaNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilRegraColuna> GetTcsPerfilRegraColunaNoAssociations()
        {
            return repository.Context.GetTcsPerfilRegraColunaNoAssociations();
        }
        
        [Route("GetTcsPerfilRegraColunaByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilRegraColuna> GetTcsPerfilRegraColunaByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsPerfilRegraColunaByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilRegraColuna), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsPerfilRegraColunaByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilRegraColuna> GetTcsPerfilRegraColunaByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsPerfilRegraColunaByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilRegraColuna), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsPerfilRegraColunaToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsPerfilRegraColunaToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilRegraColuna), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsPerfilRegraColunaByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdPerfilRegraColuna asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Perfil.TcsPerfilRegraColuna");
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
               return ExcelExportPagination<BusinessNS.TcsPerfilRegraColuna>.CreateExcelDocumentFileMapPath("TcsPerfilRegraColuna",new ExcelExportPagination<BusinessNS.TcsPerfilRegraColuna>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsPerfilRegraColunaToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsPerfilRegraColunaToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilRegraColuna), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsPerfilRegraColunaByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Perfil.TcsPerfilRegraColuna", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.PerfilDataSource", DataSourceObject = "GetTcsPerfilRegraColuna", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsPerfilRegraColuna"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilRegraColuna> GetSampleTcsPerfilRegraColuna(string details)
        {
            var result = repository.Context.GetTcsPerfilRegraColunaByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsPerfilRegraColunaEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsPerfilRegraColunaEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilRegraColuna), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsPerfilRegraColunaByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilRegraColuna> GetTcsPerfilRegraColunaByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsPerfilRegraColunaByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsPerfilRegraTransacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilRegraTransacao> GetTcsPerfilRegraTransacao()
        {
            return repository.Context.GetTcsPerfilRegraTransacao();
        }
        
        [Route("GetTcsPerfilRegraTransacaoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilRegraTransacao> GetTcsPerfilRegraTransacaoNoAssociations()
        {
            return repository.Context.GetTcsPerfilRegraTransacaoNoAssociations();
        }
        
        [Route("GetTcsPerfilRegraTransacaoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilRegraTransacao> GetTcsPerfilRegraTransacaoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsPerfilRegraTransacaoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilRegraTransacao), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsPerfilRegraTransacaoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilRegraTransacao> GetTcsPerfilRegraTransacaoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsPerfilRegraTransacaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilRegraTransacao), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsPerfilRegraTransacaoToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsPerfilRegraTransacaoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilRegraTransacao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsPerfilRegraTransacaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdPerfilRegraTransacao asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Perfil.TcsPerfilRegraTransacao");
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Perfil.TcsPerfilRegraTransacao", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.PerfilDataSource", DataSourceObject = "GetTcsPerfilRegraTransacao", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
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
        
        [Route("GetTcsPerfilRegraTransacaoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilRegraTransacao> GetTcsPerfilRegraTransacaoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsPerfilRegraTransacaoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsPerfilBandeiraRede"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilBandeiraRede> GetTcsPerfilBandeiraRede()
        {
            return repository.Context.GetTcsPerfilBandeiraRede();
        }
        
        [Route("GetTcsPerfilBandeiraRedeNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilBandeiraRede> GetTcsPerfilBandeiraRedeNoAssociations()
        {
            return repository.Context.GetTcsPerfilBandeiraRedeNoAssociations();
        }
        
        [Route("GetTcsPerfilBandeiraRedeByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilBandeiraRede> GetTcsPerfilBandeiraRedeByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsPerfilBandeiraRedeByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilBandeiraRede), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsPerfilBandeiraRedeByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilBandeiraRede> GetTcsPerfilBandeiraRedeByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsPerfilBandeiraRedeByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilBandeiraRede), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsPerfilBandeiraRedeToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsPerfilBandeiraRedeToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilBandeiraRede), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsPerfilBandeiraRedeByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdBandeiraR asc, IdPerfil asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Perfil.TcsPerfilBandeiraRede");
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Perfil.TcsPerfilBandeiraRede", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.PerfilDataSource", DataSourceObject = "GetTcsPerfilBandeiraRede", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
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
        
        [Route("GetTcsPerfilBandeiraRedeByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilBandeiraRede> GetTcsPerfilBandeiraRedeByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsPerfilBandeiraRedeByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsPerfilLayout"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilLayout> GetTcsPerfilLayout()
        {
            return repository.Context.GetTcsPerfilLayout();
        }
        
        [Route("GetTcsPerfilLayoutNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilLayout> GetTcsPerfilLayoutNoAssociations()
        {
            return repository.Context.GetTcsPerfilLayoutNoAssociations();
        }
        
        [Route("GetTcsPerfilLayoutByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilLayout> GetTcsPerfilLayoutByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsPerfilLayoutByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilLayout), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsPerfilLayoutByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilLayout> GetTcsPerfilLayoutByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsPerfilLayoutByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilLayout), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsPerfilLayoutToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsPerfilLayoutToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilLayout), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsPerfilLayoutByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdObjetoConteudo asc, IdPerfil asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Perfil.TcsPerfilLayout");
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
               return ExcelExportPagination<BusinessNS.TcsPerfilLayout>.CreateExcelDocumentFileMapPath("TcsPerfilLayout",new ExcelExportPagination<BusinessNS.TcsPerfilLayout>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsPerfilLayoutToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsPerfilLayoutToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilLayout), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsPerfilLayoutByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Perfil.TcsPerfilLayout", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.PerfilDataSource", DataSourceObject = "GetTcsPerfilLayout", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsPerfilLayout"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilLayout> GetSampleTcsPerfilLayout(string details)
        {
            var result = repository.Context.GetTcsPerfilLayoutByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsPerfilLayoutEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsPerfilLayoutEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilLayout), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsPerfilLayoutByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilLayout> GetTcsPerfilLayoutByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsPerfilLayoutByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsPerfilFilial"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilFilial> GetTcsPerfilFilial()
        {
            return repository.Context.GetTcsPerfilFilial();
        }
        
        [Route("GetTcsPerfilFilialNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilFilial> GetTcsPerfilFilialNoAssociations()
        {
            return repository.Context.GetTcsPerfilFilialNoAssociations();
        }
        
        [Route("GetTcsPerfilFilialByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilFilial> GetTcsPerfilFilialByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsPerfilFilialByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilFilial), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsPerfilFilialByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilFilial> GetTcsPerfilFilialByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsPerfilFilialByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilFilial), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsPerfilFilialToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsPerfilFilialToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilFilial), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsPerfilFilialByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsPerfilFilial asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Perfil.TcsPerfilFilial");
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Perfil.TcsPerfilFilial", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.PerfilDataSource", DataSourceObject = "GetTcsPerfilFilial", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
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
        
        [Route("GetTcsPerfilFilialByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilFilial> GetTcsPerfilFilialByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsPerfilFilialByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        #endregion
        
        #region Get Business Entities By Parent Composition
        
        [Route("GetTcsUsuarioPerfilParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioPerfilParentComposition> GetTcsUsuarioPerfilParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsUsuarioPerfilParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioPerfilParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsUsuarioPerfilParentCompositionToExcel"), System.Web.Http.HttpPost()]
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Perfil.TcsUsuarioPerfil");
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Perfil.TcsUsuarioPerfil", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.PerfilDataSource", DataSourceObject = "GetTcsUsuarioPerfilParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsUsuarioPerfilParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioPerfilParentComposition> GetSampleTcsUsuarioPerfilParentComposition(string details)
        {
            var result = repository.Context.GetTcsUsuarioPerfilParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("GetTcsPerfilRegraModuloParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilRegraModuloParentComposition> GetTcsPerfilRegraModuloParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsPerfilRegraModuloParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilRegraModuloParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsPerfilRegraModuloParentCompositionToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsPerfilRegraModuloParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("TcsPerfilRegraModulo{", "TcsPerfilRegraModuloParentComposition{");
            jEntitySearch = jEntitySearch.Replace("TcsPerfil{", "TcsPerfilRegraModuloParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilRegraModuloParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsPerfilRegraModuloParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdPerfilRegraModulo asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Perfil.TcsPerfilRegraModulo");
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Perfil.TcsPerfilRegraModulo", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.PerfilDataSource", DataSourceObject = "GetTcsPerfilRegraModuloParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsPerfilRegraModuloParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilRegraModuloParentComposition> GetSampleTcsPerfilRegraModuloParentComposition(string details)
        {
            var result = repository.Context.GetTcsPerfilRegraModuloParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("GetTcsPerfilRegraColunaParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilRegraColunaParentComposition> GetTcsPerfilRegraColunaParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsPerfilRegraColunaParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilRegraColunaParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsPerfilRegraColunaParentCompositionToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsPerfilRegraColunaParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("TcsPerfilRegraColuna{", "TcsPerfilRegraColunaParentComposition{");
            jEntitySearch = jEntitySearch.Replace("TcsPerfil{", "TcsPerfilRegraColunaParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilRegraColunaParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsPerfilRegraColunaParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdPerfilRegraColuna asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Perfil.TcsPerfilRegraColuna");
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
               return ExcelExportPagination<BusinessNS.TcsPerfilRegraColunaParentComposition>.CreateExcelDocumentFileMapPath("TcsPerfilRegraColuna",new ExcelExportPagination<BusinessNS.TcsPerfilRegraColunaParentComposition>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsPerfilRegraColunaParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsPerfilRegraColunaParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilRegraColunaParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsPerfilRegraColunaParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Perfil.TcsPerfilRegraColuna", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.PerfilDataSource", DataSourceObject = "GetTcsPerfilRegraColunaParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsPerfilRegraColunaParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilRegraColunaParentComposition> GetSampleTcsPerfilRegraColunaParentComposition(string details)
        {
            var result = repository.Context.GetTcsPerfilRegraColunaParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("GetTcsPerfilRegraTransacaoParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilRegraTransacaoParentComposition> GetTcsPerfilRegraTransacaoParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsPerfilRegraTransacaoParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilRegraTransacaoParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsPerfilRegraTransacaoParentCompositionToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsPerfilRegraTransacaoParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("TcsPerfilRegraTransacao{", "TcsPerfilRegraTransacaoParentComposition{");
            jEntitySearch = jEntitySearch.Replace("TcsPerfil{", "TcsPerfilRegraTransacaoParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilRegraTransacaoParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsPerfilRegraTransacaoParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdPerfilRegraTransacao asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Perfil.TcsPerfilRegraTransacao");
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Perfil.TcsPerfilRegraTransacao", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.PerfilDataSource", DataSourceObject = "GetTcsPerfilRegraTransacaoParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsPerfilRegraTransacaoParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilRegraTransacaoParentComposition> GetSampleTcsPerfilRegraTransacaoParentComposition(string details)
        {
            var result = repository.Context.GetTcsPerfilRegraTransacaoParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("GetTcsPerfilBandeiraRedeParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilBandeiraRedeParentComposition> GetTcsPerfilBandeiraRedeParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsPerfilBandeiraRedeParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilBandeiraRedeParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsPerfilBandeiraRedeParentCompositionToExcel"), System.Web.Http.HttpPost()]
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Perfil.TcsPerfilBandeiraRede");
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Perfil.TcsPerfilBandeiraRede", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.PerfilDataSource", DataSourceObject = "GetTcsPerfilBandeiraRedeParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsPerfilBandeiraRedeParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilBandeiraRedeParentComposition> GetSampleTcsPerfilBandeiraRedeParentComposition(string details)
        {
            var result = repository.Context.GetTcsPerfilBandeiraRedeParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("GetTcsPerfilLayoutParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilLayoutParentComposition> GetTcsPerfilLayoutParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsPerfilLayoutParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilLayoutParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsPerfilLayoutParentCompositionToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsPerfilLayoutParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("TcsPerfilLayout{", "TcsPerfilLayoutParentComposition{");
            jEntitySearch = jEntitySearch.Replace("TcsPerfil{", "TcsPerfilLayoutParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilLayoutParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsPerfilLayoutParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdObjetoConteudo asc, IdPerfil asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Perfil.TcsPerfilLayout");
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
               return ExcelExportPagination<BusinessNS.TcsPerfilLayoutParentComposition>.CreateExcelDocumentFileMapPath("TcsPerfilLayout",new ExcelExportPagination<BusinessNS.TcsPerfilLayoutParentComposition>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsPerfilLayoutParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsPerfilLayoutParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilLayoutParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsPerfilLayoutParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Perfil.TcsPerfilLayout", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.PerfilDataSource", DataSourceObject = "GetTcsPerfilLayoutParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsPerfilLayoutParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilLayoutParentComposition> GetSampleTcsPerfilLayoutParentComposition(string details)
        {
            var result = repository.Context.GetTcsPerfilLayoutParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("GetTcsPerfilFilialParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilFilialParentComposition> GetTcsPerfilFilialParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsPerfilFilialParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilFilialParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsPerfilFilialParentCompositionToExcel"), System.Web.Http.HttpPost()]
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Perfil.TcsPerfilFilial");
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Perfil.TcsPerfilFilial", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.PerfilDataSource", DataSourceObject = "GetTcsPerfilFilialParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsPerfilFilialParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfilFilialParentComposition> GetSampleTcsPerfilFilialParentComposition(string details)
        {
            var result = repository.Context.GetTcsPerfilFilialParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
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
    
    public partial class LinxFrameworkPerfilFeedController : ODataController
    {
        private BusinessNS.PerfilDomainService _context;
        public BusinessNS.PerfilDomainService Context { get {  if (_context == null) { _context = new BusinessNS.PerfilDomainService(); _context.IsSecure = true; } return _context; }  }
        
        #region Get Action to Business Entities
        
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfil> GetTcsPerfilById([FromODataUri]long key0)
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
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilRegraColuna> GetTcsPerfil__TcsPerfilRegraColuna(long key0, string navigation)
        {
            var entity = this.Context.GetTcsPerfilByKey(key0);
            if (entity != null && navigation == "TcsPerfilRegraColunaList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsPerfilRegraColuna" });
               return entity.TcsPerfilRegraColunaList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsPerfilRegraColuna>);
        }
        
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
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilLayout> GetTcsPerfil__TcsPerfilLayout(long key0, string navigation)
        {
            var entity = this.Context.GetTcsPerfilByKey(key0);
            if (entity != null && navigation == "TcsPerfilLayoutList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsPerfilLayout" });
               return entity.TcsPerfilLayoutList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsPerfilLayout>);
        }
        
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
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioPerfil> GetTcsUsuarioPerfilById([FromODataUri]long key0)
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
        public IQueryable<BusinessNS.TcsUsuarioPerfilParentComposition> GetTcsUsuarioPerfilParentComposition()
        {
            return this.Context.GetTcsUsuarioPerfilParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
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
        public IQueryable<BusinessNS.TcsPerfilRegraModuloParentComposition> GetTcsPerfilRegraModuloParentComposition()
        {
            return this.Context.GetTcsPerfilRegraModuloParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
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
        public IQueryable<BusinessNS.TcsPerfilRegraColuna> GetTcsPerfilRegraColunaById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetTcsPerfilRegraColunaByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsPerfilRegraColuna[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsPerfilRegraColuna>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilRegraColuna> GetTcsPerfilRegraColunaByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsPerfilRegraColunaByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilRegraColuna), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsPerfilRegraColuna>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilRegraColuna> GetTcsPerfilRegraColuna()
        {
            return this.Context.GetTcsPerfilRegraColunaByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilRegraColunaParentComposition> GetTcsPerfilRegraColunaParentComposition()
        {
            return this.Context.GetTcsPerfilRegraColunaParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilRegraColunaParentComposition> GetTcsPerfilRegraColunaParentCompositionByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                jEntitySearch = jEntitySearch.Replace("TcsPerfilRegraColuna{", "TcsPerfilRegraColunaParentComposition{");
                jEntitySearch = jEntitySearch.Replace("TcsPerfil{", "TcsPerfilRegraColunaParentComposition{");
                var entity = this.Context.GetTcsPerfilRegraColunaParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilRegraColunaParentComposition), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsPerfilRegraColunaParentComposition>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfil> GetTcsPerfilRegraColuna__TcsPerfil(Int64 key0, string navigation)
        {
            var entity = this.Context.GetTcsPerfilRegraColunaByKey(key0);
            if (entity != null && navigation == "TcsPerfil")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsPerfil[] { entity.TcsPerfil }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsPerfil>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilRegraTransacao> GetTcsPerfilRegraTransacaoById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetTcsPerfilRegraTransacaoByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsPerfilRegraTransacao[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsPerfilRegraTransacao>);
        }
        
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
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilRegraTransacao> GetTcsPerfilRegraTransacao()
        {
            return this.Context.GetTcsPerfilRegraTransacaoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilRegraTransacaoParentComposition> GetTcsPerfilRegraTransacaoParentComposition()
        {
            return this.Context.GetTcsPerfilRegraTransacaoParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
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
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfil> GetTcsPerfilRegraTransacao__TcsPerfil(Int64 key0, string navigation)
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
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilBandeiraRede> GetTcsPerfilBandeiraRedeById([FromODataUri]Int32 key0, [FromODataUri]Int64 key1)
        {
            var entity = this.Context.GetTcsPerfilBandeiraRedeByKey(key0, key1);
            if (entity != null)
               return (new BusinessNS.TcsPerfilBandeiraRede[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsPerfilBandeiraRede>);
        }
        
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
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilBandeiraRede> GetTcsPerfilBandeiraRede()
        {
            return this.Context.GetTcsPerfilBandeiraRedeByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilBandeiraRedeParentComposition> GetTcsPerfilBandeiraRedeParentComposition()
        {
            return this.Context.GetTcsPerfilBandeiraRedeParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
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
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfil> GetTcsPerfilBandeiraRede__TcsPerfil(Int32 key0, Int64 key1, string navigation)
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
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilLayout> GetTcsPerfilLayoutById([FromODataUri]Int64 key0, [FromODataUri]Int64 key1)
        {
            var entity = this.Context.GetTcsPerfilLayoutByKey(key0, key1);
            if (entity != null)
               return (new BusinessNS.TcsPerfilLayout[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsPerfilLayout>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilLayout> GetTcsPerfilLayoutByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsPerfilLayoutByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilLayout), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsPerfilLayout>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilLayout> GetTcsPerfilLayout()
        {
            return this.Context.GetTcsPerfilLayoutByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilLayoutParentComposition> GetTcsPerfilLayoutParentComposition()
        {
            return this.Context.GetTcsPerfilLayoutParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilLayoutParentComposition> GetTcsPerfilLayoutParentCompositionByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                jEntitySearch = jEntitySearch.Replace("TcsPerfilLayout{", "TcsPerfilLayoutParentComposition{");
                jEntitySearch = jEntitySearch.Replace("TcsPerfil{", "TcsPerfilLayoutParentComposition{");
                var entity = this.Context.GetTcsPerfilLayoutParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsPerfilLayoutParentComposition), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsPerfilLayoutParentComposition>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfil> GetTcsPerfilLayout__TcsPerfil(Int64 key0, Int64 key1, string navigation)
        {
            var entity = this.Context.GetTcsPerfilLayoutByKey(key0, key1);
            if (entity != null && navigation == "TcsPerfil")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsPerfil[] { entity.TcsPerfil }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsPerfil>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilFilial> GetTcsPerfilFilialById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetTcsPerfilFilialByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsPerfilFilial[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsPerfilFilial>);
        }
        
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
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilFilial> GetTcsPerfilFilial()
        {
            return this.Context.GetTcsPerfilFilialByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfilFilialParentComposition> GetTcsPerfilFilialParentComposition()
        {
            return this.Context.GetTcsPerfilFilialParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
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
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsPerfil> GetTcsPerfilFilial__TcsPerfil(Int64 key0, string navigation)
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
        #endregion
        
    }
    
    public partial class LinxFrameworkPerfilControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return true;
        }
    }
}

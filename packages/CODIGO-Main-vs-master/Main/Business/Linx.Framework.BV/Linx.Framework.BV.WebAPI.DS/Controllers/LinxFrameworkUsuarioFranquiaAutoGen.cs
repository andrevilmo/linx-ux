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
using BusinessNS = Linx.Framework.BV.UsuarioFranquia;

namespace Linx.Framework.BV.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/LinxFrameworkUsuarioFranquia/[ActionName]
    // Security Information Call: http://localhost:1710/LinxFrameworkUsuarioFranquia/GetSecurityInfo
    // Entities Catalog Call: http://localhost:1710/LinxFrameworkUsuarioFranquia/GetEntities
    // Entity MetaData Call: http://localhost:1710/LinxFrameworkUsuarioFranquia/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/LinxFrameworkUsuarioFranquia/GetClientDomains?erp=true
    // Client Service Call: http://localhost:1710/LinxFrameworkUsuarioFranquia/GetClientService?erp=true
    // Client Factory Call: http://localhost:1710/LinxFrameworkUsuarioFranquia/GetClientFactory?entityName=[EntityName]&erp=true
    // Client Factory Custom Events Call: http://localhost:1710/LinxFrameworkUsuarioFranquia/GetClientFactoryCustomEvents?entityName=[EntityName]&erp=true
    // Help Call: http://localhost:1710/HelpController/LinxFrameworkUsuarioFranquia
    // Feed OData Call: http://localhost:1710/LinxFrameworkUsuarioFranquiaOData
    [RoutePrefix("LinxFrameworkUsuarioFranquia")]
    [Breeze.WebApi2.BreezeController]
    public partial class LinxFrameworkUsuarioFranquiaController : ApiController
    {
        private const int maxObjectExcelReturned = 2300000;
        private DataServiceRepository<BusinessNS.UsuarioFranquiaDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.UsuarioFranquiaDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.UsuarioFranquiaDomainService>(typeof(BusinessNS.TcsUsuarioAutenticacao), typeof(BusinessNS.TcsUsuarioAutenticacaoAcesso), typeof(BusinessNS.TcsUsuarioAutenticacaoPerfil), typeof(BusinessNS.TcsUsuarioPerfil), typeof(BusinessNS.UsuarioPerfilInfo)); _repository.Context.IsSecure = true; } return _repository; } }
        public LinxFrameworkUsuarioFranquiaController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(LinxFrameworkUsuarioFranquiaController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.UsuarioFranquiaDomainService).Assembly.FullName,
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
            var result = repository.Context.GetMetaDataObject("Linx.Framework.BV.UsuarioFranquia." + entityName, false, true);
            return (allComposition ? result : result.Where(e => e.ClassName == entityName).ToList());
        }
        
        [Route("GetSecurityInfo"), System.Web.Http.HttpGet()]
        public string[] GetSecurityInfo()
        {
           return new string[] { "Linx.Framework.BV", "LinxFrameworkUsuarioFranquia", "LinxFrameworkUsuarioFranquia/ActionName" };
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
        
        [Route("GetAllLookUpTcsPerfil"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsPerfil> GetAllLookUpTcsPerfil()
        {
            return repository.Context.GetAllLookUpTcsPerfil();
        }
        
        [Route("GetLookUpTcsPerfilByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsPerfil> GetLookUpTcsPerfilByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsPerfilByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.UsuarioFranquia.TcsUsuarioAutenticacao");
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.UsuarioFranquia.TcsUsuarioAutenticacao", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.UsuarioFranquiaDataSource", DataSourceObject = "GetTcsUsuarioAutenticacao", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.UsuarioFranquia.TcsUsuarioAutenticacaoAcesso");
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.UsuarioFranquia.TcsUsuarioAutenticacaoAcesso", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.UsuarioFranquiaDataSource", DataSourceObject = "GetTcsUsuarioAutenticacaoAcesso", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsUsuarioAutenticacaoAcesso"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioAutenticacaoAcesso> GetSampleTcsUsuarioAutenticacaoAcesso(string details)
        {
            var result = repository.Context.GetTcsUsuarioAutenticacaoAcessoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsUsuarioAutenticacaoAcessoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsUsuarioAutenticacaoAcessoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioAutenticacaoAcesso), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsUsuarioAutenticacaoAcessoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioAutenticacaoAcesso> GetTcsUsuarioAutenticacaoAcessoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsUsuarioAutenticacaoAcessoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsUsuarioAutenticacaoPerfil"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioAutenticacaoPerfil> GetTcsUsuarioAutenticacaoPerfil()
        {
            return repository.Context.GetTcsUsuarioAutenticacaoPerfil().AsQueryable();
        }
        
        [Route("GetTcsUsuarioAutenticacaoPerfilNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioAutenticacaoPerfil> GetTcsUsuarioAutenticacaoPerfilNoAssociations()
        {
            return repository.Context.GetTcsUsuarioAutenticacaoPerfilNoAssociations().AsQueryable();
        }
        
        [Route("GetTcsUsuarioAutenticacaoPerfilByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioAutenticacaoPerfil> GetTcsUsuarioAutenticacaoPerfilByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsUsuarioAutenticacaoPerfilByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioAutenticacaoPerfil), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        
        [Route("GetTcsUsuarioAutenticacaoPerfilByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioAutenticacaoPerfil> GetTcsUsuarioAutenticacaoPerfilByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsUsuarioAutenticacaoPerfilByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioAutenticacaoPerfil), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        [Route("GetTcsUsuarioAutenticacaoPerfilToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsUsuarioAutenticacaoPerfilToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioAutenticacaoPerfil), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsUsuarioAutenticacaoPerfilByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).AsQueryable().OrderBy("IdLinx asc, IdTcsUsuarioPerfil asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.UsuarioFranquia.TcsUsuarioAutenticacaoPerfil");
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
               return ExcelExportPagination<BusinessNS.TcsUsuarioAutenticacaoPerfil>.CreateExcelDocumentFileMapPath("TcsUsuarioAutenticacaoPerfil",new ExcelExportPagination<BusinessNS.TcsUsuarioAutenticacaoPerfil>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsUsuarioAutenticacaoPerfilToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsUsuarioAutenticacaoPerfilToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioAutenticacaoPerfil), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsUsuarioAutenticacaoPerfilByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.UsuarioFranquia.TcsUsuarioAutenticacaoPerfil", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.UsuarioFranquiaDataSource", DataSourceObject = "GetTcsUsuarioAutenticacaoPerfil", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsUsuarioAutenticacaoPerfil"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioAutenticacaoPerfil> GetSampleTcsUsuarioAutenticacaoPerfil(string details)
        {
            var result = repository.Context.GetTcsUsuarioAutenticacaoPerfilByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsUsuarioAutenticacaoPerfilEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsUsuarioAutenticacaoPerfilEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioAutenticacaoPerfil), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsUsuarioAutenticacaoPerfilByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioAutenticacaoPerfil> GetTcsUsuarioAutenticacaoPerfilByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsUsuarioAutenticacaoPerfilByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.UsuarioFranquia.TcsUsuarioPerfil");
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.UsuarioFranquia.TcsUsuarioPerfil", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.UsuarioFranquiaDataSource", DataSourceObject = "GetTcsUsuarioPerfil", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
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
        
        [Route("GetUsuarioPerfilInfo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.UsuarioPerfilInfo> GetUsuarioPerfilInfo()
        {
            return repository.Context.GetUsuarioPerfilInfo().AsQueryable();
        }
        
        [Route("GetUsuarioPerfilInfoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.UsuarioPerfilInfo> GetUsuarioPerfilInfoNoAssociations()
        {
            return repository.Context.GetUsuarioPerfilInfoNoAssociations().AsQueryable();
        }
        
        [Route("GetUsuarioPerfilInfoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.UsuarioPerfilInfo> GetUsuarioPerfilInfoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetUsuarioPerfilInfoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.UsuarioPerfilInfo), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        
        [Route("GetUsuarioPerfilInfoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.UsuarioPerfilInfo> GetUsuarioPerfilInfoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetUsuarioPerfilInfoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.UsuarioPerfilInfo), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        [Route("GetUsuarioPerfilInfoToExcel"), System.Web.Http.HttpPost()]
        public string GetUsuarioPerfilInfoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.UsuarioPerfilInfo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetUsuarioPerfilInfoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).AsQueryable().OrderBy("IdUsuario asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.UsuarioFranquia.UsuarioPerfilInfo");
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
               return ExcelExportPagination<BusinessNS.UsuarioPerfilInfo>.CreateExcelDocumentFileMapPath("UsuarioPerfilInfo",new ExcelExportPagination<BusinessNS.UsuarioPerfilInfo>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetUsuarioPerfilInfoToReportXml"), System.Web.Http.HttpPost()]
        public string GetUsuarioPerfilInfoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.UsuarioPerfilInfo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetUsuarioPerfilInfoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.UsuarioFranquia.UsuarioPerfilInfo", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.UsuarioFranquiaDataSource", DataSourceObject = "GetUsuarioPerfilInfo", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleUsuarioPerfilInfo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.UsuarioPerfilInfo> GetSampleUsuarioPerfilInfo(string details)
        {
            var result = repository.Context.GetUsuarioPerfilInfoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddUsuarioPerfilInfoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddUsuarioPerfilInfoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.UsuarioPerfilInfo), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetUsuarioPerfilInfoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.UsuarioPerfilInfo> GetUsuarioPerfilInfoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetUsuarioPerfilInfoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
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
    
    public partial class LinxFrameworkUsuarioFranquiaFeedController : ODataController
    {
        private BusinessNS.UsuarioFranquiaDomainService _context;
        public BusinessNS.UsuarioFranquiaDomainService Context { get {  if (_context == null) { _context = new BusinessNS.UsuarioFranquiaDomainService(); _context.IsSecure = true; } return _context; }  }
        
        #region Get Action to Business Entities
        
        
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
        public IQueryable<BusinessNS.TcsUsuarioAutenticacaoPerfil> GetTcsUsuarioAutenticacaoPerfilById([FromODataUri]Int32 key0, [FromODataUri]Int64 key1)
        {
            var entity = this.Context.GetTcsUsuarioAutenticacaoPerfilByKey(key0, key1);
            if (entity != null)
               return (new BusinessNS.TcsUsuarioAutenticacaoPerfil[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsUsuarioAutenticacaoPerfil>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioAutenticacaoPerfil> GetTcsUsuarioAutenticacaoPerfilByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsUsuarioAutenticacaoPerfilByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioAutenticacaoPerfil), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsUsuarioAutenticacaoPerfil>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioAutenticacaoPerfil> GetTcsUsuarioAutenticacaoPerfil()
        {
            return this.Context.GetTcsUsuarioAutenticacaoPerfilByEntitySearchNoAssociations(null).AsQueryable();
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
        public IQueryable<BusinessNS.UsuarioPerfilInfo> GetUsuarioPerfilInfoById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetUsuarioPerfilInfoByKey(key0);
            if (entity != null)
               return (new BusinessNS.UsuarioPerfilInfo[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.UsuarioPerfilInfo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.UsuarioPerfilInfo> GetUsuarioPerfilInfoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetUsuarioPerfilInfoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.UsuarioPerfilInfo), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.UsuarioPerfilInfo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.UsuarioPerfilInfo> GetUsuarioPerfilInfo()
        {
            return this.Context.GetUsuarioPerfilInfoByEntitySearchNoAssociations(null).AsQueryable();
        }
        #endregion
        
    }
    
    public partial class LinxFrameworkUsuarioFranquiaControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return true;
        }
    }
}

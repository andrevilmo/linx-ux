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
using BusinessNS = Linx.Framework.BV.Autorizacao;

namespace Linx.Framework.BV.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/LinxFrameworkAutorizacao/[ActionName]
    // Security Information Call: http://localhost:1710/LinxFrameworkAutorizacao/GetSecurityInfo
    // Entities Catalog Call: http://localhost:1710/LinxFrameworkAutorizacao/GetEntities
    // Entity MetaData Call: http://localhost:1710/LinxFrameworkAutorizacao/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/LinxFrameworkAutorizacao/GetClientDomains?erp=true
    // Client Service Call: http://localhost:1710/LinxFrameworkAutorizacao/GetClientService?erp=true
    // Client Factory Call: http://localhost:1710/LinxFrameworkAutorizacao/GetClientFactory?entityName=[EntityName]&erp=true
    // Client Factory Custom Events Call: http://localhost:1710/LinxFrameworkAutorizacao/GetClientFactoryCustomEvents?entityName=[EntityName]&erp=true
    // Help Call: http://localhost:1710/HelpController/LinxFrameworkAutorizacao
    // Feed OData Call: http://localhost:1710/LinxFrameworkAutorizacaoOData
    [RoutePrefix("LinxFrameworkAutorizacao")]
    [Breeze.WebApi2.BreezeController]
    public partial class LinxFrameworkAutorizacaoController : ApiController
    {
        private const int maxObjectExcelReturned = 2300000;
        private DataServiceRepository<BusinessNS.AutorizacaoDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.AutorizacaoDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.AutorizacaoDomainService>(typeof(BusinessNS.Acesso), typeof(BusinessNS.AmbienteInfo), typeof(BusinessNS.AppInfo), typeof(BusinessNS.GpeconInfo), typeof(BusinessNS.LoginInfo), typeof(BusinessNS.TcsUsuarioAcesso), typeof(BusinessNS.UserInfo), typeof(BusinessNS.UsuarioAcesso)); _repository.Context.IsSecure = true; } return _repository; } }
        public LinxFrameworkAutorizacaoController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(LinxFrameworkAutorizacaoController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.AutorizacaoDomainService).Assembly.FullName,
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
            var result = repository.Context.GetMetaDataObject("Linx.Framework.BV.Autorizacao." + entityName, false, true);
            return (allComposition ? result : result.Where(e => e.ClassName == entityName).ToList());
        }
        
        [Route("GetSecurityInfo"), System.Web.Http.HttpGet()]
        public string[] GetSecurityInfo()
        {
           return new string[] { "Linx.Framework.BV", "LinxFrameworkAutorizacao", "LinxFrameworkAutorizacao/ActionName" };
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
        
        [Route("GetAcesso"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Acesso> GetAcesso()
        {
            return repository.Context.GetAcesso().AsQueryable();
        }
        
        [Route("GetAcessoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Acesso> GetAcessoNoAssociations()
        {
            return repository.Context.GetAcessoNoAssociations().AsQueryable();
        }
        
        [Route("GetAcessoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Acesso> GetAcessoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetAcessoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Acesso), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        
        [Route("GetAcessoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Acesso> GetAcessoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetAcessoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Acesso), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        [Route("GetAcessoToExcel"), System.Web.Http.HttpPost()]
        public string GetAcessoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Acesso), jEntitySearch, false, false, false);
            var entities = repository.Context.GetAcessoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).AsQueryable().OrderBy("IdTcsAmbiente asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Autorizacao.Acesso");
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
               return ExcelExportPagination<BusinessNS.Acesso>.CreateExcelDocumentFileMapPath("Acesso",new ExcelExportPagination<BusinessNS.Acesso>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetAcessoToReportXml"), System.Web.Http.HttpPost()]
        public string GetAcessoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Acesso), jEntitySearch, false, false, false);
            var entities = repository.Context.GetAcessoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Autorizacao.Acesso", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.AutorizacaoDataSource", DataSourceObject = "GetAcesso", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleAcesso"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Acesso> GetSampleAcesso(string details)
        {
            var result = repository.Context.GetAcessoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddAcessoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddAcessoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Acesso), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetAcessoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Acesso> GetAcessoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetAcessoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetUsuarioAcesso"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.UsuarioAcesso> GetUsuarioAcesso()
        {
            return repository.Context.GetUsuarioAcesso().AsQueryable();
        }
        
        [Route("GetUsuarioAcessoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.UsuarioAcesso> GetUsuarioAcessoNoAssociations()
        {
            return repository.Context.GetUsuarioAcessoNoAssociations().AsQueryable();
        }
        
        [Route("GetUsuarioAcessoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.UsuarioAcesso> GetUsuarioAcessoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetUsuarioAcessoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.UsuarioAcesso), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        
        [Route("GetUsuarioAcessoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.UsuarioAcesso> GetUsuarioAcessoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetUsuarioAcessoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.UsuarioAcesso), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        [Route("GetUsuarioAcessoToExcel"), System.Web.Http.HttpPost()]
        public string GetUsuarioAcessoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.UsuarioAcesso), jEntitySearch, false, false, false);
            var entities = repository.Context.GetUsuarioAcessoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).AsQueryable().OrderBy("UidUsuario asc, IdAmbiente asc, UidAplicacao asc, UidEmpresa asc, UidGrupoEconomico asc, UidGrupoAcesso asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Autorizacao.UsuarioAcesso");
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
               return ExcelExportPagination<BusinessNS.UsuarioAcesso>.CreateExcelDocumentFileMapPath("UsuarioAcesso",new ExcelExportPagination<BusinessNS.UsuarioAcesso>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetUsuarioAcessoToReportXml"), System.Web.Http.HttpPost()]
        public string GetUsuarioAcessoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.UsuarioAcesso), jEntitySearch, false, false, false);
            var entities = repository.Context.GetUsuarioAcessoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Autorizacao.UsuarioAcesso", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.AutorizacaoDataSource", DataSourceObject = "GetUsuarioAcesso", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleUsuarioAcesso"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.UsuarioAcesso> GetSampleUsuarioAcesso(string details)
        {
            var result = repository.Context.GetUsuarioAcessoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddUsuarioAcessoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddUsuarioAcessoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.UsuarioAcesso), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetUsuarioAcessoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.UsuarioAcesso> GetUsuarioAcessoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetUsuarioAcessoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetUserInfo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.UserInfo> GetUserInfo()
        {
            return repository.Context.GetUserInfo().AsQueryable();
        }
        
        [Route("GetUserInfoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.UserInfo> GetUserInfoNoAssociations()
        {
            return repository.Context.GetUserInfoNoAssociations().AsQueryable();
        }
        
        [Route("GetUserInfoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.UserInfo> GetUserInfoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetUserInfoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.UserInfo), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        
        [Route("GetUserInfoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.UserInfo> GetUserInfoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetUserInfoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.UserInfo), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        [Route("GetUserInfoToExcel"), System.Web.Http.HttpPost()]
        public string GetUserInfoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.UserInfo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetUserInfoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).AsQueryable().OrderBy("UidUsuario asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Autorizacao.UserInfo");
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
               return ExcelExportPagination<BusinessNS.UserInfo>.CreateExcelDocumentFileMapPath("UserInfo",new ExcelExportPagination<BusinessNS.UserInfo>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetUserInfoToReportXml"), System.Web.Http.HttpPost()]
        public string GetUserInfoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.UserInfo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetUserInfoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Autorizacao.UserInfo", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.AutorizacaoDataSource", DataSourceObject = "GetUserInfo", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleUserInfo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.UserInfo> GetSampleUserInfo(string details)
        {
            var result = repository.Context.GetUserInfoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddUserInfoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddUserInfoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.UserInfo), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetUserInfoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.UserInfo> GetUserInfoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetUserInfoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetLoginInfo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LoginInfo> GetLoginInfo()
        {
            return repository.Context.GetLoginInfo().AsQueryable();
        }
        
        [Route("GetLoginInfoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LoginInfo> GetLoginInfoNoAssociations()
        {
            return repository.Context.GetLoginInfoNoAssociations().AsQueryable();
        }
        
        [Route("GetLoginInfoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LoginInfo> GetLoginInfoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetLoginInfoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LoginInfo), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        
        [Route("GetLoginInfoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LoginInfo> GetLoginInfoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetLoginInfoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LoginInfo), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        [Route("GetLoginInfoToExcel"), System.Web.Http.HttpPost()]
        public string GetLoginInfoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LoginInfo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetLoginInfoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).AsQueryable().OrderBy("UidUsuario asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Autorizacao.LoginInfo");
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
               return ExcelExportPagination<BusinessNS.LoginInfo>.CreateExcelDocumentFileMapPath("LoginInfo",new ExcelExportPagination<BusinessNS.LoginInfo>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetLoginInfoToReportXml"), System.Web.Http.HttpPost()]
        public string GetLoginInfoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LoginInfo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetLoginInfoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Autorizacao.LoginInfo", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.AutorizacaoDataSource", DataSourceObject = "GetLoginInfo", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleLoginInfo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LoginInfo> GetSampleLoginInfo(string details)
        {
            var result = repository.Context.GetLoginInfoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddLoginInfoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddLoginInfoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LoginInfo), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetLoginInfoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LoginInfo> GetLoginInfoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetLoginInfoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
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
            var entities = repository.Context.GetAmbienteInfoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).AsQueryable().OrderBy("IdTcsAmbiente asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Autorizacao.AmbienteInfo");
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Autorizacao.AmbienteInfo", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.AutorizacaoDataSource", DataSourceObject = "GetAmbienteInfo", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleAmbienteInfo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AmbienteInfo> GetSampleAmbienteInfo(string details)
        {
            var result = repository.Context.GetAmbienteInfoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddAmbienteInfoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddAmbienteInfoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AmbienteInfo), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetAmbienteInfoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AmbienteInfo> GetAmbienteInfoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetAmbienteInfoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsUsuarioAcesso"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioAcesso> GetTcsUsuarioAcesso()
        {
            return repository.Context.GetTcsUsuarioAcesso();
        }
        
        [Route("GetTcsUsuarioAcessoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioAcesso> GetTcsUsuarioAcessoNoAssociations()
        {
            return repository.Context.GetTcsUsuarioAcessoNoAssociations();
        }
        
        [Route("GetTcsUsuarioAcessoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioAcesso> GetTcsUsuarioAcessoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsUsuarioAcessoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioAcesso), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsUsuarioAcessoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioAcesso> GetTcsUsuarioAcessoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsUsuarioAcessoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioAcesso), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsUsuarioAcessoToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsUsuarioAcessoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioAcesso), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsUsuarioAcessoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsUsuarioAcesso asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Autorizacao.TcsUsuarioAcesso");
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
               return ExcelExportPagination<BusinessNS.TcsUsuarioAcesso>.CreateExcelDocumentFileMapPath("TcsUsuarioAcesso",new ExcelExportPagination<BusinessNS.TcsUsuarioAcesso>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsUsuarioAcessoToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsUsuarioAcessoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioAcesso), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsUsuarioAcessoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Autorizacao.TcsUsuarioAcesso", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.AutorizacaoDataSource", DataSourceObject = "GetTcsUsuarioAcesso", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsUsuarioAcesso"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioAcesso> GetSampleTcsUsuarioAcesso(string details)
        {
            var result = repository.Context.GetTcsUsuarioAcessoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsUsuarioAcessoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsUsuarioAcessoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioAcesso), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsUsuarioAcessoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioAcesso> GetTcsUsuarioAcessoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsUsuarioAcessoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetAppInfo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AppInfo> GetAppInfo()
        {
            return repository.Context.GetAppInfo().AsQueryable();
        }
        
        [Route("GetAppInfoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AppInfo> GetAppInfoNoAssociations()
        {
            return repository.Context.GetAppInfoNoAssociations().AsQueryable();
        }
        
        [Route("GetAppInfoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AppInfo> GetAppInfoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetAppInfoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AppInfo), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        
        [Route("GetAppInfoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AppInfo> GetAppInfoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetAppInfoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AppInfo), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        [Route("GetAppInfoToExcel"), System.Web.Http.HttpPost()]
        public string GetAppInfoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AppInfo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetAppInfoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).AsQueryable().OrderBy("AppName asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Autorizacao.AppInfo");
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
               return ExcelExportPagination<BusinessNS.AppInfo>.CreateExcelDocumentFileMapPath("AppInfo",new ExcelExportPagination<BusinessNS.AppInfo>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetAppInfoToReportXml"), System.Web.Http.HttpPost()]
        public string GetAppInfoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AppInfo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetAppInfoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Autorizacao.AppInfo", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.AutorizacaoDataSource", DataSourceObject = "GetAppInfo", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleAppInfo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AppInfo> GetSampleAppInfo(string details)
        {
            var result = repository.Context.GetAppInfoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddAppInfoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddAppInfoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AppInfo), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetAppInfoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AppInfo> GetAppInfoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetAppInfoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetGpeconInfo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.GpeconInfo> GetGpeconInfo()
        {
            return repository.Context.GetGpeconInfo().AsQueryable();
        }
        
        [Route("GetGpeconInfoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.GpeconInfo> GetGpeconInfoNoAssociations()
        {
            return repository.Context.GetGpeconInfoNoAssociations().AsQueryable();
        }
        
        [Route("GetGpeconInfoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.GpeconInfo> GetGpeconInfoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetGpeconInfoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.GpeconInfo), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        
        [Route("GetGpeconInfoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.GpeconInfo> GetGpeconInfoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetGpeconInfoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.GpeconInfo), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        [Route("GetGpeconInfoToExcel"), System.Web.Http.HttpPost()]
        public string GetGpeconInfoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.GpeconInfo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetGpeconInfoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).AsQueryable().OrderBy("IdGpecon asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Autorizacao.GpeconInfo");
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
               return ExcelExportPagination<BusinessNS.GpeconInfo>.CreateExcelDocumentFileMapPath("GpeconInfo",new ExcelExportPagination<BusinessNS.GpeconInfo>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetGpeconInfoToReportXml"), System.Web.Http.HttpPost()]
        public string GetGpeconInfoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.GpeconInfo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetGpeconInfoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Autorizacao.GpeconInfo", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.AutorizacaoDataSource", DataSourceObject = "GetGpeconInfo", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleGpeconInfo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.GpeconInfo> GetSampleGpeconInfo(string details)
        {
            var result = repository.Context.GetGpeconInfoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddGpeconInfoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddGpeconInfoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.GpeconInfo), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetGpeconInfoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.GpeconInfo> GetGpeconInfoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetGpeconInfoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
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
    
    public partial class LinxFrameworkAutorizacaoFeedController : ODataController
    {
        private BusinessNS.AutorizacaoDomainService _context;
        public BusinessNS.AutorizacaoDomainService Context { get {  if (_context == null) { _context = new BusinessNS.AutorizacaoDomainService(); _context.IsSecure = true; } return _context; }  }
        
        #region Get Action to Business Entities
        
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Acesso> GetAcessoById([FromODataUri]int key0)
        {
            var entity = this.Context.GetAcessoByKey(key0);
            if (entity != null)
               return (new BusinessNS.Acesso[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.Acesso>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Acesso> GetAcessoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetAcessoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Acesso), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.Acesso>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Acesso> GetAcesso()
        {
            return this.Context.GetAcessoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.UsuarioAcesso> GetUsuarioAcessoById([FromODataUri]Guid key0, [FromODataUri]int key1, [FromODataUri]Guid key2, [FromODataUri]Guid key3, [FromODataUri]Guid key4, [FromODataUri]Guid key5)
        {
            var entity = this.Context.GetUsuarioAcessoByKey(key0, key1, key2, key3, key4, key5);
            if (entity != null)
               return (new BusinessNS.UsuarioAcesso[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.UsuarioAcesso>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.UsuarioAcesso> GetUsuarioAcessoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetUsuarioAcessoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.UsuarioAcesso), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.UsuarioAcesso>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.UsuarioAcesso> GetUsuarioAcesso()
        {
            return this.Context.GetUsuarioAcessoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.UserInfo> GetUserInfoById([FromODataUri]Guid key0)
        {
            var entity = this.Context.GetUserInfoByKey(key0);
            if (entity != null)
               return (new BusinessNS.UserInfo[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.UserInfo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.UserInfo> GetUserInfoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetUserInfoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.UserInfo), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.UserInfo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.UserInfo> GetUserInfo()
        {
            return this.Context.GetUserInfoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.LoginInfo> GetLoginInfoById([FromODataUri]Guid key0)
        {
            var entity = this.Context.GetLoginInfoByKey(key0);
            if (entity != null)
               return (new BusinessNS.LoginInfo[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.LoginInfo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.LoginInfo> GetLoginInfoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetLoginInfoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LoginInfo), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.LoginInfo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.LoginInfo> GetLoginInfo()
        {
            return this.Context.GetLoginInfoByEntitySearchNoAssociations(null).AsQueryable();
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
        public IQueryable<BusinessNS.TcsUsuarioAcesso> GetTcsUsuarioAcessoById([FromODataUri]int key0)
        {
            var entity = this.Context.GetTcsUsuarioAcessoByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsUsuarioAcesso[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsUsuarioAcesso>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioAcesso> GetTcsUsuarioAcessoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsUsuarioAcessoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioAcesso), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsUsuarioAcesso>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioAcesso> GetTcsUsuarioAcesso()
        {
            return this.Context.GetTcsUsuarioAcessoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.AppInfo> GetAppInfoById([FromODataUri]string key0)
        {
            var entity = this.Context.GetAppInfoByKey(key0);
            if (entity != null)
               return (new BusinessNS.AppInfo[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.AppInfo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.AppInfo> GetAppInfoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetAppInfoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AppInfo), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.AppInfo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.AppInfo> GetAppInfo()
        {
            return this.Context.GetAppInfoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.GpeconInfo> GetGpeconInfoById([FromODataUri]int key0)
        {
            var entity = this.Context.GetGpeconInfoByKey(key0);
            if (entity != null)
               return (new BusinessNS.GpeconInfo[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.GpeconInfo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.GpeconInfo> GetGpeconInfoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetGpeconInfoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.GpeconInfo), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.GpeconInfo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.GpeconInfo> GetGpeconInfo()
        {
            return this.Context.GetGpeconInfoByEntitySearchNoAssociations(null).AsQueryable();
        }
        #endregion
        
    }
    
    public partial class LinxFrameworkAutorizacaoControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
    }
}

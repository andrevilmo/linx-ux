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
using BusinessNS = Linx.Framework.BV.Mensagem;

namespace Linx.Framework.BV.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/LinxFrameworkMensagem/[ActionName]
    // Security Information Call: http://localhost:1710/LinxFrameworkMensagem/GetSecurityInfo
    // Entities Catalog Call: http://localhost:1710/LinxFrameworkMensagem/GetEntities
    // Entity MetaData Call: http://localhost:1710/LinxFrameworkMensagem/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/LinxFrameworkMensagem/GetClientDomains?erp=true
    // Client Service Call: http://localhost:1710/LinxFrameworkMensagem/GetClientService?erp=true
    // Client Factory Call: http://localhost:1710/LinxFrameworkMensagem/GetClientFactory?entityName=[EntityName]&erp=true
    // Client Factory Custom Events Call: http://localhost:1710/LinxFrameworkMensagem/GetClientFactoryCustomEvents?entityName=[EntityName]&erp=true
    // Help Call: http://localhost:1710/HelpController/LinxFrameworkMensagem
    // Feed OData Call: http://localhost:1710/LinxFrameworkMensagemOData
    [RoutePrefix("LinxFrameworkMensagem")]
    [Breeze.WebApi2.BreezeController]
    public partial class LinxFrameworkMensagemController : ApiController
    {
        private const int maxObjectExcelReturned = 2300000;
        private DataServiceRepository<BusinessNS.MensagemDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.MensagemDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.MensagemDomainService>(typeof(BusinessNS.MensagemInfo), typeof(BusinessNS.NewMessageInfo), typeof(BusinessNS.TcsMensagem), typeof(BusinessNS.TcsMensagemConsulta), typeof(BusinessNS.TcsMensagemConsultaLog), typeof(BusinessNS.TcsMensagemLog), typeof(BusinessNS.TcsMensagemLogDetail), typeof(BusinessNS.TcsMensagemUsuario), typeof(BusinessNS.TcsPerfil), typeof(BusinessNS.TcsUsuario)); _repository.Context.IsSecure = true; } return _repository; } }
        public LinxFrameworkMensagemController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(LinxFrameworkMensagemController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.MensagemDomainService).Assembly.FullName,
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
            var result = repository.Context.GetMetaDataObject("Linx.Framework.BV.Mensagem." + entityName, false, true);
            return (allComposition ? result : result.Where(e => e.ClassName == entityName).ToList());
        }
        
        [Route("GetSecurityInfo"), System.Web.Http.HttpGet()]
        public string[] GetSecurityInfo()
        {
           return new string[] { "Linx.Framework.BV", "LinxFrameworkMensagem", "LinxFrameworkMensagem/ActionName" };
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
        
        [Route("GetAllLookUpTcsUsuarioAutenticacaoCL"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsUsuarioAutenticacaoCL> GetAllLookUpTcsUsuarioAutenticacaoCL()
        {
            return repository.Context.GetAllLookUpTcsUsuarioAutenticacaoCL();
        }
        
        [Route("GetLookUpTcsUsuarioAutenticacaoCLByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsUsuarioAutenticacaoCL> GetLookUpTcsUsuarioAutenticacaoCLByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsUsuarioAutenticacaoCLByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [Route("GetAllLookUpTcsEmpresaAutenticacaoC"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsEmpresaAutenticacaoC> GetAllLookUpTcsEmpresaAutenticacaoC()
        {
            return repository.Context.GetAllLookUpTcsEmpresaAutenticacaoC();
        }
        
        [Route("GetLookUpTcsEmpresaAutenticacaoCByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsEmpresaAutenticacaoC> GetLookUpTcsEmpresaAutenticacaoCByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsEmpresaAutenticacaoCByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [Route("GetAllLookUpTcsUsuarioAutenticacaoC"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsUsuarioAutenticacaoC> GetAllLookUpTcsUsuarioAutenticacaoC()
        {
            return repository.Context.GetAllLookUpTcsUsuarioAutenticacaoC();
        }
        
        [Route("GetLookUpTcsUsuarioAutenticacaoCByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsUsuarioAutenticacaoC> GetLookUpTcsUsuarioAutenticacaoCByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsUsuarioAutenticacaoCByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
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
        
        [Route("GetTcsMensagem"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsMensagem> GetTcsMensagem()
        {
            return repository.Context.GetTcsMensagem();
        }
        
        [Route("GetTcsMensagemNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsMensagem> GetTcsMensagemNoAssociations()
        {
            return repository.Context.GetTcsMensagemNoAssociations();
        }
        
        [Route("GetTcsMensagemByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsMensagem> GetTcsMensagemByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsMensagemByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsMensagem), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsMensagemByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsMensagem> GetTcsMensagemByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsMensagemByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsMensagem), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsMensagemToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsMensagemToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsMensagem), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsMensagemByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsMensagem asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Mensagem.TcsMensagem");
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
               return ExcelExportPagination<BusinessNS.TcsMensagem>.CreateExcelDocumentFileMapPath("TcsMensagem",new ExcelExportPagination<BusinessNS.TcsMensagem>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsMensagemToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsMensagemToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsMensagem), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsMensagemByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Mensagem.TcsMensagem", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.MensagemDataSource", DataSourceObject = "GetTcsMensagem", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsMensagem"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsMensagem> GetSampleTcsMensagem(string details)
        {
            var result = repository.Context.GetTcsMensagemByEntitySearchNoAssociations(null).Take(100).ToList();
               if (!details.IsNullOrEmpty())
               {
                   foreach(var entity in result)
                   {
                       entity.FillDetails(repository.Context, null, null, details.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries), 5);
                   }
               }
            return result.AsQueryable();
        }
        
        [Route("AddTcsMensagemEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsMensagemEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsMensagem), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsMensagemByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsMensagem> GetTcsMensagemByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsMensagemByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetMensagemInfo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.MensagemInfo> GetMensagemInfo()
        {
            return repository.Context.GetMensagemInfo().AsQueryable();
        }
        
        [Route("GetMensagemInfoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.MensagemInfo> GetMensagemInfoNoAssociations()
        {
            return repository.Context.GetMensagemInfoNoAssociations().AsQueryable();
        }
        
        [Route("GetMensagemInfoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.MensagemInfo> GetMensagemInfoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetMensagemInfoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.MensagemInfo), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        
        [Route("GetMensagemInfoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.MensagemInfo> GetMensagemInfoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetMensagemInfoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.MensagemInfo), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        [Route("GetMensagemInfoToExcel"), System.Web.Http.HttpPost()]
        public string GetMensagemInfoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.MensagemInfo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetMensagemInfoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).AsQueryable().OrderBy("IdTcsMensagemLog asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Mensagem.MensagemInfo");
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
               return ExcelExportPagination<BusinessNS.MensagemInfo>.CreateExcelDocumentFileMapPath("MensagemInfo",new ExcelExportPagination<BusinessNS.MensagemInfo>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetMensagemInfoToReportXml"), System.Web.Http.HttpPost()]
        public string GetMensagemInfoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.MensagemInfo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetMensagemInfoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Mensagem.MensagemInfo", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.MensagemDataSource", DataSourceObject = "GetMensagemInfo", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleMensagemInfo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.MensagemInfo> GetSampleMensagemInfo(string details)
        {
            var result = repository.Context.GetMensagemInfoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddMensagemInfoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddMensagemInfoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.MensagemInfo), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetMensagemInfoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.MensagemInfo> GetMensagemInfoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetMensagemInfoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsMensagemUsuario"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsMensagemUsuario> GetTcsMensagemUsuario()
        {
            return repository.Context.GetTcsMensagemUsuario();
        }
        
        [Route("GetTcsMensagemUsuarioNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsMensagemUsuario> GetTcsMensagemUsuarioNoAssociations()
        {
            return repository.Context.GetTcsMensagemUsuarioNoAssociations();
        }
        
        [Route("GetTcsMensagemUsuarioByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsMensagemUsuario> GetTcsMensagemUsuarioByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsMensagemUsuarioByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsMensagemUsuario), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsMensagemUsuarioByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsMensagemUsuario> GetTcsMensagemUsuarioByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsMensagemUsuarioByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsMensagemUsuario), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsMensagemUsuarioToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsMensagemUsuarioToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsMensagemUsuario), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsMensagemUsuarioByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsMensagemLog asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Mensagem.TcsMensagemUsuario");
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
               return ExcelExportPagination<BusinessNS.TcsMensagemUsuario>.CreateExcelDocumentFileMapPath("TcsMensagemUsuario",new ExcelExportPagination<BusinessNS.TcsMensagemUsuario>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsMensagemUsuarioToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsMensagemUsuarioToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsMensagemUsuario), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsMensagemUsuarioByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Mensagem.TcsMensagemUsuario", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.MensagemDataSource", DataSourceObject = "GetTcsMensagemUsuario", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsMensagemUsuario"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsMensagemUsuario> GetSampleTcsMensagemUsuario(string details)
        {
            var result = repository.Context.GetTcsMensagemUsuarioByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsMensagemUsuarioEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsMensagemUsuarioEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsMensagemUsuario), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsMensagemUsuarioByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsMensagemUsuario> GetTcsMensagemUsuarioByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsMensagemUsuarioByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsMensagemLog"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsMensagemLog> GetTcsMensagemLog()
        {
            return repository.Context.GetTcsMensagemLog();
        }
        
        [Route("GetTcsMensagemLogNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsMensagemLog> GetTcsMensagemLogNoAssociations()
        {
            return repository.Context.GetTcsMensagemLogNoAssociations();
        }
        
        [Route("GetTcsMensagemLogByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsMensagemLog> GetTcsMensagemLogByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsMensagemLogByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsMensagemLog), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsMensagemLogByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsMensagemLog> GetTcsMensagemLogByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsMensagemLogByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsMensagemLog), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsMensagemLogToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsMensagemLogToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsMensagemLog), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsMensagemLogByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsMensagemLog asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Mensagem.TcsMensagemLog");
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
               return ExcelExportPagination<BusinessNS.TcsMensagemLog>.CreateExcelDocumentFileMapPath("TcsMensagemLog",new ExcelExportPagination<BusinessNS.TcsMensagemLog>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsMensagemLogToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsMensagemLogToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsMensagemLog), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsMensagemLogByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Mensagem.TcsMensagemLog", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.MensagemDataSource", DataSourceObject = "GetTcsMensagemLog", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsMensagemLog"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsMensagemLog> GetSampleTcsMensagemLog(string details)
        {
            var result = repository.Context.GetTcsMensagemLogByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsMensagemLogEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsMensagemLogEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsMensagemLog), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsMensagemLogByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsMensagemLog> GetTcsMensagemLogByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsMensagemLogByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
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
            var entities = repository.Context.GetTcsPerfilByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsUsuarioPerfil asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Mensagem.TcsPerfil");
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Mensagem.TcsPerfil", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.MensagemDataSource", DataSourceObject = "GetTcsPerfil", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsPerfil"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsPerfil> GetSampleTcsPerfil(string details)
        {
            var result = repository.Context.GetTcsPerfilByEntitySearchNoAssociations(null).Take(100).ToList();
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
        
        [Route("GetTcsUsuario"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuario> GetTcsUsuario()
        {
            return repository.Context.GetTcsUsuario();
        }
        
        [Route("GetTcsUsuarioNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuario> GetTcsUsuarioNoAssociations()
        {
            return repository.Context.GetTcsUsuarioNoAssociations();
        }
        
        [Route("GetTcsUsuarioByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuario> GetTcsUsuarioByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsUsuarioByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuario), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsUsuarioByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuario> GetTcsUsuarioByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsUsuarioByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuario), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsUsuarioToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsUsuarioToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuario), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsUsuarioByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdUsuario asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Mensagem.TcsUsuario");
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
               return ExcelExportPagination<BusinessNS.TcsUsuario>.CreateExcelDocumentFileMapPath("TcsUsuario",new ExcelExportPagination<BusinessNS.TcsUsuario>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsUsuarioToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsUsuarioToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuario), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsUsuarioByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Mensagem.TcsUsuario", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.MensagemDataSource", DataSourceObject = "GetTcsUsuario", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsUsuario"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuario> GetSampleTcsUsuario(string details)
        {
            var result = repository.Context.GetTcsUsuarioByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsUsuarioEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsUsuarioEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuario), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsUsuarioByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuario> GetTcsUsuarioByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsUsuarioByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetNewMessageInfo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.NewMessageInfo> GetNewMessageInfo()
        {
            return repository.Context.GetNewMessageInfo().AsQueryable();
        }
        
        [Route("GetNewMessageInfoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.NewMessageInfo> GetNewMessageInfoNoAssociations()
        {
            return repository.Context.GetNewMessageInfoNoAssociations().AsQueryable();
        }
        
        [Route("GetNewMessageInfoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.NewMessageInfo> GetNewMessageInfoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetNewMessageInfoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.NewMessageInfo), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        
        [Route("GetNewMessageInfoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.NewMessageInfo> GetNewMessageInfoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetNewMessageInfoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.NewMessageInfo), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        [Route("GetNewMessageInfoToExcel"), System.Web.Http.HttpPost()]
        public string GetNewMessageInfoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.NewMessageInfo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetNewMessageInfoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).AsQueryable().OrderBy("IdLinx asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Mensagem.NewMessageInfo");
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
               return ExcelExportPagination<BusinessNS.NewMessageInfo>.CreateExcelDocumentFileMapPath("NewMessageInfo",new ExcelExportPagination<BusinessNS.NewMessageInfo>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetNewMessageInfoToReportXml"), System.Web.Http.HttpPost()]
        public string GetNewMessageInfoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.NewMessageInfo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetNewMessageInfoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Mensagem.NewMessageInfo", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.MensagemDataSource", DataSourceObject = "GetNewMessageInfo", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleNewMessageInfo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.NewMessageInfo> GetSampleNewMessageInfo(string details)
        {
            var result = repository.Context.GetNewMessageInfoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddNewMessageInfoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddNewMessageInfoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.NewMessageInfo), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetNewMessageInfoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.NewMessageInfo> GetNewMessageInfoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetNewMessageInfoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsMensagemLogDetail"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsMensagemLogDetail> GetTcsMensagemLogDetail()
        {
            return repository.Context.GetTcsMensagemLogDetail();
        }
        
        [Route("GetTcsMensagemLogDetailNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsMensagemLogDetail> GetTcsMensagemLogDetailNoAssociations()
        {
            return repository.Context.GetTcsMensagemLogDetailNoAssociations();
        }
        
        [Route("GetTcsMensagemLogDetailByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsMensagemLogDetail> GetTcsMensagemLogDetailByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsMensagemLogDetailByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsMensagemLogDetail), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsMensagemLogDetailByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsMensagemLogDetail> GetTcsMensagemLogDetailByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsMensagemLogDetailByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsMensagemLogDetail), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsMensagemLogDetailToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsMensagemLogDetailToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsMensagemLogDetail), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsMensagemLogDetailByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsMensagemLog asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Mensagem.TcsMensagemLogDetail");
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
               return ExcelExportPagination<BusinessNS.TcsMensagemLogDetail>.CreateExcelDocumentFileMapPath("TcsMensagemLogDetail",new ExcelExportPagination<BusinessNS.TcsMensagemLogDetail>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsMensagemLogDetailToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsMensagemLogDetailToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsMensagemLogDetail), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsMensagemLogDetailByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Mensagem.TcsMensagemLogDetail", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.MensagemDataSource", DataSourceObject = "GetTcsMensagemLogDetail", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsMensagemLogDetail"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsMensagemLogDetail> GetSampleTcsMensagemLogDetail(string details)
        {
            var result = repository.Context.GetTcsMensagemLogDetailByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsMensagemLogDetailEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsMensagemLogDetailEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsMensagemLogDetail), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsMensagemLogDetailByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsMensagemLogDetail> GetTcsMensagemLogDetailByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsMensagemLogDetailByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsMensagemConsulta"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsMensagemConsulta> GetTcsMensagemConsulta()
        {
            return repository.Context.GetTcsMensagemConsulta();
        }
        
        [Route("GetTcsMensagemConsultaNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsMensagemConsulta> GetTcsMensagemConsultaNoAssociations()
        {
            return repository.Context.GetTcsMensagemConsultaNoAssociations();
        }
        
        [Route("GetTcsMensagemConsultaByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsMensagemConsulta> GetTcsMensagemConsultaByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsMensagemConsultaByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsMensagemConsulta), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsMensagemConsultaByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsMensagemConsulta> GetTcsMensagemConsultaByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsMensagemConsultaByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsMensagemConsulta), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsMensagemConsultaToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsMensagemConsultaToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsMensagemConsulta), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsMensagemConsultaByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsMensagem asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Mensagem.TcsMensagemConsulta");
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
               return ExcelExportPagination<BusinessNS.TcsMensagemConsulta>.CreateExcelDocumentFileMapPath("TcsMensagemConsulta",new ExcelExportPagination<BusinessNS.TcsMensagemConsulta>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsMensagemConsultaToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsMensagemConsultaToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsMensagemConsulta), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsMensagemConsultaByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Mensagem.TcsMensagemConsulta", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.MensagemDataSource", DataSourceObject = "GetTcsMensagemConsulta", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsMensagemConsulta"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsMensagemConsulta> GetSampleTcsMensagemConsulta(string details)
        {
            var result = repository.Context.GetTcsMensagemConsultaByEntitySearchNoAssociations(null).Take(100).ToList();
               if (!details.IsNullOrEmpty())
               {
                   foreach(var entity in result)
                   {
                       entity.FillDetails(repository.Context, null, null, details.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries), 5);
                   }
               }
            return result.AsQueryable();
        }
        
        [Route("AddTcsMensagemConsultaEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsMensagemConsultaEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsMensagemConsulta), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsMensagemConsultaByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsMensagemConsulta> GetTcsMensagemConsultaByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsMensagemConsultaByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsMensagemConsultaLog"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsMensagemConsultaLog> GetTcsMensagemConsultaLog()
        {
            return repository.Context.GetTcsMensagemConsultaLog();
        }
        
        [Route("GetTcsMensagemConsultaLogNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsMensagemConsultaLog> GetTcsMensagemConsultaLogNoAssociations()
        {
            return repository.Context.GetTcsMensagemConsultaLogNoAssociations();
        }
        
        [Route("GetTcsMensagemConsultaLogByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsMensagemConsultaLog> GetTcsMensagemConsultaLogByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsMensagemConsultaLogByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsMensagemConsultaLog), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsMensagemConsultaLogByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsMensagemConsultaLog> GetTcsMensagemConsultaLogByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsMensagemConsultaLogByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsMensagemConsultaLog), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsMensagemConsultaLogToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsMensagemConsultaLogToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsMensagemConsultaLog), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsMensagemConsultaLogByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsMensagemLog asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Mensagem.TcsMensagemConsultaLog");
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
               return ExcelExportPagination<BusinessNS.TcsMensagemConsultaLog>.CreateExcelDocumentFileMapPath("TcsMensagemConsultaLog",new ExcelExportPagination<BusinessNS.TcsMensagemConsultaLog>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsMensagemConsultaLogToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsMensagemConsultaLogToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsMensagemConsultaLog), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsMensagemConsultaLogByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Mensagem.TcsMensagemConsultaLog", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.MensagemDataSource", DataSourceObject = "GetTcsMensagemConsultaLog", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsMensagemConsultaLog"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsMensagemConsultaLog> GetSampleTcsMensagemConsultaLog(string details)
        {
            var result = repository.Context.GetTcsMensagemConsultaLogByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsMensagemConsultaLogEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsMensagemConsultaLogEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsMensagemConsultaLog), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsMensagemConsultaLogByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsMensagemConsultaLog> GetTcsMensagemConsultaLogByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsMensagemConsultaLogByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        #endregion
        
        #region Get Business Entities By Parent Composition
        
        [Route("GetTcsMensagemLogDetailParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsMensagemLogDetailParentComposition> GetTcsMensagemLogDetailParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsMensagemLogDetailParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsMensagemLogDetailParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsMensagemLogDetailParentCompositionToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsMensagemLogDetailParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("TcsMensagemLogDetail{", "TcsMensagemLogDetailParentComposition{");
            jEntitySearch = jEntitySearch.Replace("TcsMensagem{", "TcsMensagemLogDetailParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsMensagemLogDetailParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsMensagemLogDetailParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsMensagemLog asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Mensagem.TcsMensagemLogDetail");
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
               return ExcelExportPagination<BusinessNS.TcsMensagemLogDetailParentComposition>.CreateExcelDocumentFileMapPath("TcsMensagemLogDetail",new ExcelExportPagination<BusinessNS.TcsMensagemLogDetailParentComposition>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsMensagemLogDetailParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsMensagemLogDetailParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsMensagemLogDetailParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsMensagemLogDetailParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Mensagem.TcsMensagemLogDetail", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.MensagemDataSource", DataSourceObject = "GetTcsMensagemLogDetailParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsMensagemLogDetailParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsMensagemLogDetailParentComposition> GetSampleTcsMensagemLogDetailParentComposition(string details)
        {
            var result = repository.Context.GetTcsMensagemLogDetailParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("GetTcsMensagemConsultaLogParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsMensagemConsultaLogParentComposition> GetTcsMensagemConsultaLogParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsMensagemConsultaLogParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsMensagemConsultaLogParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsMensagemConsultaLogParentCompositionToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsMensagemConsultaLogParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("TcsMensagemConsultaLog{", "TcsMensagemConsultaLogParentComposition{");
            jEntitySearch = jEntitySearch.Replace("TcsMensagemConsulta{", "TcsMensagemConsultaLogParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsMensagemConsultaLogParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsMensagemConsultaLogParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsMensagemLog asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Mensagem.TcsMensagemConsultaLog");
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
               return ExcelExportPagination<BusinessNS.TcsMensagemConsultaLogParentComposition>.CreateExcelDocumentFileMapPath("TcsMensagemConsultaLog",new ExcelExportPagination<BusinessNS.TcsMensagemConsultaLogParentComposition>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsMensagemConsultaLogParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsMensagemConsultaLogParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsMensagemConsultaLogParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsMensagemConsultaLogParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Mensagem.TcsMensagemConsultaLog", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.MensagemDataSource", DataSourceObject = "GetTcsMensagemConsultaLogParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsMensagemConsultaLogParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsMensagemConsultaLogParentComposition> GetSampleTcsMensagemConsultaLogParentComposition(string details)
        {
            var result = repository.Context.GetTcsMensagemConsultaLogParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
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
    
    public partial class LinxFrameworkMensagemFeedController : ODataController
    {
        private BusinessNS.MensagemDomainService _context;
        public BusinessNS.MensagemDomainService Context { get {  if (_context == null) { _context = new BusinessNS.MensagemDomainService(); _context.IsSecure = true; } return _context; }  }
        
        #region Get Action to Business Entities
        
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsMensagem> GetTcsMensagemById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetTcsMensagemByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsMensagem[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsMensagem>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsMensagem> GetTcsMensagemByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsMensagemByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsMensagem), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsMensagem>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsMensagem> GetTcsMensagem()
        {
            return this.Context.GetTcsMensagemByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsMensagemLogDetail> GetTcsMensagem__TcsMensagemLogDetail(Int64 key0, string navigation)
        {
            var entity = this.Context.GetTcsMensagemByKey(key0);
            if (entity != null && navigation == "TcsMensagemLogDetailList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsMensagemLogDetail" });
               return entity.TcsMensagemLogDetailList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsMensagemLogDetail>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.MensagemInfo> GetMensagemInfoById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetMensagemInfoByKey(key0);
            if (entity != null)
               return (new BusinessNS.MensagemInfo[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.MensagemInfo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.MensagemInfo> GetMensagemInfoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetMensagemInfoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.MensagemInfo), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.MensagemInfo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.MensagemInfo> GetMensagemInfo()
        {
            return this.Context.GetMensagemInfoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsMensagemUsuario> GetTcsMensagemUsuarioById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetTcsMensagemUsuarioByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsMensagemUsuario[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsMensagemUsuario>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsMensagemUsuario> GetTcsMensagemUsuarioByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsMensagemUsuarioByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsMensagemUsuario), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsMensagemUsuario>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsMensagemUsuario> GetTcsMensagemUsuario()
        {
            return this.Context.GetTcsMensagemUsuarioByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsMensagemLog> GetTcsMensagemLogById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetTcsMensagemLogByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsMensagemLog[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsMensagemLog>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsMensagemLog> GetTcsMensagemLogByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsMensagemLogByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsMensagemLog), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsMensagemLog>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsMensagemLog> GetTcsMensagemLog()
        {
            return this.Context.GetTcsMensagemLogByEntitySearchNoAssociations(null).AsQueryable();
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
        public IQueryable<BusinessNS.TcsUsuario> GetTcsUsuarioById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetTcsUsuarioByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsUsuario[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsUsuario>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuario> GetTcsUsuarioByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsUsuarioByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuario), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsUsuario>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuario> GetTcsUsuario()
        {
            return this.Context.GetTcsUsuarioByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.NewMessageInfo> GetNewMessageInfoById([FromODataUri]Int32 key0)
        {
            var entity = this.Context.GetNewMessageInfoByKey(key0);
            if (entity != null)
               return (new BusinessNS.NewMessageInfo[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.NewMessageInfo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.NewMessageInfo> GetNewMessageInfoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetNewMessageInfoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.NewMessageInfo), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.NewMessageInfo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.NewMessageInfo> GetNewMessageInfo()
        {
            return this.Context.GetNewMessageInfoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsMensagemLogDetail> GetTcsMensagemLogDetailById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetTcsMensagemLogDetailByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsMensagemLogDetail[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsMensagemLogDetail>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsMensagemLogDetail> GetTcsMensagemLogDetailByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsMensagemLogDetailByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsMensagemLogDetail), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsMensagemLogDetail>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsMensagemLogDetail> GetTcsMensagemLogDetail()
        {
            return this.Context.GetTcsMensagemLogDetailByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsMensagemLogDetailParentComposition> GetTcsMensagemLogDetailParentComposition()
        {
            return this.Context.GetTcsMensagemLogDetailParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsMensagemLogDetailParentComposition> GetTcsMensagemLogDetailParentCompositionByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                jEntitySearch = jEntitySearch.Replace("TcsMensagemLogDetail{", "TcsMensagemLogDetailParentComposition{");
                jEntitySearch = jEntitySearch.Replace("TcsMensagem{", "TcsMensagemLogDetailParentComposition{");
                var entity = this.Context.GetTcsMensagemLogDetailParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsMensagemLogDetailParentComposition), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsMensagemLogDetailParentComposition>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsMensagem> GetTcsMensagemLogDetail__TcsMensagem(Int64 key0, string navigation)
        {
            var entity = this.Context.GetTcsMensagemLogDetailByKey(key0);
            if (entity != null && navigation == "TcsMensagem")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsMensagem[] { entity.TcsMensagem }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsMensagem>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsMensagemConsulta> GetTcsMensagemConsultaById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetTcsMensagemConsultaByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsMensagemConsulta[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsMensagemConsulta>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsMensagemConsulta> GetTcsMensagemConsultaByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsMensagemConsultaByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsMensagemConsulta), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsMensagemConsulta>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsMensagemConsulta> GetTcsMensagemConsulta()
        {
            return this.Context.GetTcsMensagemConsultaByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsMensagemConsultaLog> GetTcsMensagemConsulta__TcsMensagemConsultaLog(Int64 key0, string navigation)
        {
            var entity = this.Context.GetTcsMensagemConsultaByKey(key0);
            if (entity != null && navigation == "TcsMensagemConsultaLogList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsMensagemConsultaLog" });
               return entity.TcsMensagemConsultaLogList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsMensagemConsultaLog>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsMensagemConsultaLog> GetTcsMensagemConsultaLogById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetTcsMensagemConsultaLogByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsMensagemConsultaLog[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsMensagemConsultaLog>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsMensagemConsultaLog> GetTcsMensagemConsultaLogByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsMensagemConsultaLogByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsMensagemConsultaLog), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsMensagemConsultaLog>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsMensagemConsultaLog> GetTcsMensagemConsultaLog()
        {
            return this.Context.GetTcsMensagemConsultaLogByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsMensagemConsultaLogParentComposition> GetTcsMensagemConsultaLogParentComposition()
        {
            return this.Context.GetTcsMensagemConsultaLogParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsMensagemConsultaLogParentComposition> GetTcsMensagemConsultaLogParentCompositionByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                jEntitySearch = jEntitySearch.Replace("TcsMensagemConsultaLog{", "TcsMensagemConsultaLogParentComposition{");
                jEntitySearch = jEntitySearch.Replace("TcsMensagemConsulta{", "TcsMensagemConsultaLogParentComposition{");
                var entity = this.Context.GetTcsMensagemConsultaLogParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsMensagemConsultaLogParentComposition), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsMensagemConsultaLogParentComposition>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsMensagemConsulta> GetTcsMensagemConsultaLog__TcsMensagemConsulta(Int64 key0, string navigation)
        {
            var entity = this.Context.GetTcsMensagemConsultaLogByKey(key0);
            if (entity != null && navigation == "TcsMensagemConsulta")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsMensagemConsulta[] { entity.TcsMensagemConsulta }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsMensagemConsulta>);
        }
        #endregion
        
    }
    
    public partial class LinxFrameworkMensagemControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return true;
        }
    }
}

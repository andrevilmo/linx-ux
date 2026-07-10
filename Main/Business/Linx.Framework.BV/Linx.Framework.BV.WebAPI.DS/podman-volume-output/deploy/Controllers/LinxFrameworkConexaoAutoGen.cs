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
using BusinessNS = Linx.Framework.BV.Conexao;

namespace Linx.Framework.BV.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/LinxFrameworkConexao/[ActionName]
    // Security Information Call: http://localhost:1710/LinxFrameworkConexao/GetSecurityInfo
    // Entities Catalog Call: http://localhost:1710/LinxFrameworkConexao/GetEntities
    // Entity MetaData Call: http://localhost:1710/LinxFrameworkConexao/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/LinxFrameworkConexao/GetClientDomains?erp=true
    // Client Service Call: http://localhost:1710/LinxFrameworkConexao/GetClientService?erp=true
    // Client Factory Call: http://localhost:1710/LinxFrameworkConexao/GetClientFactory?entityName=[EntityName]&erp=true
    // Client Factory Custom Events Call: http://localhost:1710/LinxFrameworkConexao/GetClientFactoryCustomEvents?entityName=[EntityName]&erp=true
    // Help Call: http://localhost:1710/HelpController/LinxFrameworkConexao
    // Feed OData Call: http://localhost:1710/LinxFrameworkConexaoOData
    [RoutePrefix("LinxFrameworkConexao")]
    [Breeze.WebApi2.BreezeController]
    public partial class LinxFrameworkConexaoController : ApiController
    {
        private const int maxObjectExcelReturned = 2300000;
        private DataServiceRepository<BusinessNS.ConexaoDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.ConexaoDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.ConexaoDomainService>(typeof(BusinessNS.TcsAmbienteConexao), typeof(BusinessNS.TcsBancoServidor), typeof(BusinessNS.TcsConexaoDb)); _repository.Context.IsSecure = true; } return _repository; } }
        public LinxFrameworkConexaoController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(LinxFrameworkConexaoController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.ConexaoDomainService).Assembly.FullName,
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
            var result = repository.Context.GetMetaDataObject("Linx.Framework.BV.Conexao." + entityName, false, true);
            return (allComposition ? result : result.Where(e => e.ClassName == entityName).ToList());
        }
        
        [Route("GetSecurityInfo"), System.Web.Http.HttpGet()]
        public string[] GetSecurityInfo()
        {
           return new string[] { "Linx.Framework.BV", "LinxFrameworkConexao", "LinxFrameworkConexao/ActionName" };
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
        #endregion
        #region Get KPI Ranges
        #endregion
        
        #region Get Business Entities
        
        [Route("GetBmEntityProperties"), System.Web.Http.HttpGet()]
        public List<BmMetaDataProperty> GetBmEntityProperties(string entityName, string parentDataPath)
        {
            return repository.Context.GetBmEntityProperties(entityName, parentDataPath);
        }
        
        [Route("GetTcsConexaoDb"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsConexaoDb> GetTcsConexaoDb()
        {
            return repository.Context.GetTcsConexaoDb();
        }
        
        [Route("GetTcsConexaoDbNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsConexaoDb> GetTcsConexaoDbNoAssociations()
        {
            return repository.Context.GetTcsConexaoDbNoAssociations();
        }
        
        [Route("GetTcsConexaoDbByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsConexaoDb> GetTcsConexaoDbByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsConexaoDbByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsConexaoDb), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsConexaoDbByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsConexaoDb> GetTcsConexaoDbByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsConexaoDbByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsConexaoDb), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsConexaoDbToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsConexaoDbToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsConexaoDb), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsConexaoDbByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdConexaoDb asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Conexao.TcsConexaoDb");
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
               return ExcelExportPagination<BusinessNS.TcsConexaoDb>.CreateExcelDocumentFileMapPath("TcsConexaoDb",new ExcelExportPagination<BusinessNS.TcsConexaoDb>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsConexaoDbToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsConexaoDbToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsConexaoDb), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsConexaoDbByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Conexao.TcsConexaoDb", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ConexaoDataSource", DataSourceObject = "GetTcsConexaoDb", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsConexaoDb"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsConexaoDb> GetSampleTcsConexaoDb(string details)
        {
            var result = repository.Context.GetTcsConexaoDbByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsConexaoDbEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsConexaoDbEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsConexaoDb), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsConexaoDbByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsConexaoDb> GetTcsConexaoDbByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsConexaoDbByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsBancoServidor"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsBancoServidor> GetTcsBancoServidor()
        {
            return repository.Context.GetTcsBancoServidor();
        }
        
        [Route("GetTcsBancoServidorNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsBancoServidor> GetTcsBancoServidorNoAssociations()
        {
            return repository.Context.GetTcsBancoServidorNoAssociations();
        }
        
        [Route("GetTcsBancoServidorByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsBancoServidor> GetTcsBancoServidorByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsBancoServidorByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsBancoServidor), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsBancoServidorByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsBancoServidor> GetTcsBancoServidorByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsBancoServidorByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsBancoServidor), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsBancoServidorToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsBancoServidorToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsBancoServidor), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsBancoServidorByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsBancoServidor asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Conexao.TcsBancoServidor");
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
               return ExcelExportPagination<BusinessNS.TcsBancoServidor>.CreateExcelDocumentFileMapPath("TcsBancoServidor",new ExcelExportPagination<BusinessNS.TcsBancoServidor>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsBancoServidorToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsBancoServidorToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsBancoServidor), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsBancoServidorByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Conexao.TcsBancoServidor", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ConexaoDataSource", DataSourceObject = "GetTcsBancoServidor", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsBancoServidor"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsBancoServidor> GetSampleTcsBancoServidor(string details)
        {
            var result = repository.Context.GetTcsBancoServidorByEntitySearchNoAssociations(null).Take(100).ToList();
               if (!details.IsNullOrEmpty())
               {
                   foreach(var entity in result)
                   {
                       entity.FillDetails(repository.Context, null, null, details.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries), 5);
                   }
               }
            return result.AsQueryable();
        }
        
        [Route("AddTcsBancoServidorEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsBancoServidorEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsBancoServidor), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsBancoServidorByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsBancoServidor> GetTcsBancoServidorByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsBancoServidorByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Conexao.TcsAmbienteConexao");
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Conexao.TcsAmbienteConexao", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ConexaoDataSource", DataSourceObject = "GetTcsAmbienteConexao", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
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
        #endregion
        
        #region Get Business Entities By Parent Composition
        
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
            jEntitySearch = jEntitySearch.Replace("TcsBancoServidor{", "TcsAmbienteConexaoParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteConexaoParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsAmbienteConexaoParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsAmbienteConexao asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Conexao.TcsAmbienteConexao");
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Conexao.TcsAmbienteConexao", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ConexaoDataSource", DataSourceObject = "GetTcsAmbienteConexaoParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
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
    
    public partial class LinxFrameworkConexaoFeedController : ODataController
    {
        private BusinessNS.ConexaoDomainService _context;
        public BusinessNS.ConexaoDomainService Context { get {  if (_context == null) { _context = new BusinessNS.ConexaoDomainService(); _context.IsSecure = true; } return _context; }  }
        
        #region Get Action to Business Entities
        
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsConexaoDb> GetTcsConexaoDbById([FromODataUri]Int32 key0)
        {
            var entity = this.Context.GetTcsConexaoDbByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsConexaoDb[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsConexaoDb>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsConexaoDb> GetTcsConexaoDbByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsConexaoDbByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsConexaoDb), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsConexaoDb>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsConexaoDb> GetTcsConexaoDb()
        {
            return this.Context.GetTcsConexaoDbByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsBancoServidor> GetTcsBancoServidorById([FromODataUri]Int32 key0)
        {
            var entity = this.Context.GetTcsBancoServidorByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsBancoServidor[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsBancoServidor>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsBancoServidor> GetTcsBancoServidorByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsBancoServidorByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsBancoServidor), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsBancoServidor>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsBancoServidor> GetTcsBancoServidor()
        {
            return this.Context.GetTcsBancoServidorByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsAmbienteConexao> GetTcsBancoServidor__TcsAmbienteConexao(Int32 key0, string navigation)
        {
            var entity = this.Context.GetTcsBancoServidorByKey(key0);
            if (entity != null && navigation == "TcsAmbienteConexaoList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsAmbienteConexao" });
               return entity.TcsAmbienteConexaoList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsAmbienteConexao>);
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
                jEntitySearch = jEntitySearch.Replace("TcsBancoServidor{", "TcsAmbienteConexaoParentComposition{");
                var entity = this.Context.GetTcsAmbienteConexaoParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteConexaoParentComposition), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsAmbienteConexaoParentComposition>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsBancoServidor> GetTcsAmbienteConexao__TcsBancoServidor(Int32 key0, string navigation)
        {
            var entity = this.Context.GetTcsAmbienteConexaoByKey(key0);
            if (entity != null && navigation == "TcsBancoServidor")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsBancoServidor[] { entity.TcsBancoServidor }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsBancoServidor>);
        }
        #endregion
        
    }
    
    public partial class LinxFrameworkConexaoControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return true;
        }
    }
}

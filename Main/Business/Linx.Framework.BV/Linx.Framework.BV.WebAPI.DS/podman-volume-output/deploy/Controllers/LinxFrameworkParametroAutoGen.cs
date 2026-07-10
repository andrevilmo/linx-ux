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
using BusinessNS = Linx.Framework.BV.Parametro;

namespace Linx.Framework.BV.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/LinxFrameworkParametro/[ActionName]
    // Security Information Call: http://localhost:1710/LinxFrameworkParametro/GetSecurityInfo
    // Entities Catalog Call: http://localhost:1710/LinxFrameworkParametro/GetEntities
    // Entity MetaData Call: http://localhost:1710/LinxFrameworkParametro/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/LinxFrameworkParametro/GetClientDomains?erp=true
    // Client Service Call: http://localhost:1710/LinxFrameworkParametro/GetClientService?erp=true
    // Client Factory Call: http://localhost:1710/LinxFrameworkParametro/GetClientFactory?entityName=[EntityName]&erp=true
    // Client Factory Custom Events Call: http://localhost:1710/LinxFrameworkParametro/GetClientFactoryCustomEvents?entityName=[EntityName]&erp=true
    // Help Call: http://localhost:1710/HelpController/LinxFrameworkParametro
    // Feed OData Call: http://localhost:1710/LinxFrameworkParametroOData
    [RoutePrefix("LinxFrameworkParametro")]
    [Breeze.WebApi2.BreezeController]
    public partial class LinxFrameworkParametroController : ApiController
    {
        private const int maxObjectExcelReturned = 2300000;
        private DataServiceRepository<BusinessNS.ParametroDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.ParametroDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.ParametroDomainService>(typeof(BusinessNS.LjvLojaParametro), typeof(BusinessNS.ParametroInfo), typeof(BusinessNS.TbcBandeiraRedeParametro), typeof(BusinessNS.TbcFilialParametro), typeof(BusinessNS.TbcGrupoEconomicoParametro), typeof(BusinessNS.TcsParametro), typeof(BusinessNS.TcsParametroTabelaSelecao), typeof(BusinessNS.TcsParametroValor), typeof(BusinessNS.TcsParametroValorFilial), typeof(BusinessNS.TcsParametroValorFilialP), typeof(BusinessNS.TcsParametroValorGpecon), typeof(BusinessNS.TcsParametroValorGpeconP), typeof(BusinessNS.TcsParametroValorLjvLoja), typeof(BusinessNS.TcsParametroValorLojaP), typeof(BusinessNS.TcsParametroValorP), typeof(BusinessNS.TcsParametroValorP1), typeof(BusinessNS.TcsParametroValorP2), typeof(BusinessNS.TcsParametroValorRede), typeof(BusinessNS.TcsParametroValorRedeP), typeof(BusinessNS.TcsParametroValorUsuario), typeof(BusinessNS.TcsParametroValorUsuarioP), typeof(BusinessNS.TcsParametroValorVariacaoGenerica), typeof(BusinessNS.TcsParametroValorVariacaoGenericaP), typeof(BusinessNS.TcsParametroValorVariacaoGenericaP1), typeof(BusinessNS.TcsParametroValorVariacaoP), typeof(BusinessNS.TcsUsuarioParametro)); _repository.Context.IsSecure = true; } return _repository; } }
        public LinxFrameworkParametroController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(LinxFrameworkParametroController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.ParametroDomainService).Assembly.FullName,
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
            var result = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro." + entityName, false, true);
            return (allComposition ? result : result.Where(e => e.ClassName == entityName).ToList());
        }
        
        [Route("GetSecurityInfo"), System.Web.Http.HttpGet()]
        public string[] GetSecurityInfo()
        {
           return new string[] { "Linx.Framework.BV", "LinxFrameworkParametro", "LinxFrameworkParametro/ActionName" };
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
        
        [Route("GetAllLookTcsParametroUsuario"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookTcsParametroUsuario> GetAllLookTcsParametroUsuario()
        {
            return repository.Context.GetAllLookTcsParametroUsuario();
        }
        
        [Route("GetLookTcsParametroUsuarioByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookTcsParametroUsuario> GetLookTcsParametroUsuarioByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookTcsParametroUsuarioByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [Route("GetAllLookUpParametroRede"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpParametroRede> GetAllLookUpParametroRede()
        {
            return repository.Context.GetAllLookUpParametroRede();
        }
        
        [Route("GetLookUpParametroRedeByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpParametroRede> GetLookUpParametroRedeByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpParametroRedeByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [Route("GetAllLookUpParametroGpecon"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpParametroGpecon> GetAllLookUpParametroGpecon()
        {
            return repository.Context.GetAllLookUpParametroGpecon();
        }
        
        [Route("GetLookUpParametroGpeconByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpParametroGpecon> GetLookUpParametroGpeconByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpParametroGpeconByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [Route("GetAllLookUpParametroLoja"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpParametroLoja> GetAllLookUpParametroLoja()
        {
            return repository.Context.GetAllLookUpParametroLoja();
        }
        
        [Route("GetLookUpParametroLojaByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpParametroLoja> GetLookUpParametroLojaByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpParametroLojaByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [Route("GetAllLookUpParametroFilial"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpParametroFilial> GetAllLookUpParametroFilial()
        {
            return repository.Context.GetAllLookUpParametroFilial();
        }
        
        [Route("GetLookUpParametroFilialByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpParametroFilial> GetLookUpParametroFilialByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpParametroFilialByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [Route("GetAllLookUpTcsTabelaAutorizacaoC"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsTabelaAutorizacaoC> GetAllLookUpTcsTabelaAutorizacaoC()
        {
            return repository.Context.GetAllLookUpTcsTabelaAutorizacaoC();
        }
        
        [Route("GetLookUpTcsTabelaAutorizacaoCByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsTabelaAutorizacaoC> GetLookUpTcsTabelaAutorizacaoCByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsTabelaAutorizacaoCByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [Route("GetAllLookUpTcsAplicativo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsAplicativo> GetAllLookUpTcsAplicativo()
        {
            return repository.Context.GetAllLookUpTcsAplicativo();
        }
        
        [Route("GetLookUpTcsAplicativoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsAplicativo> GetLookUpTcsAplicativoByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsAplicativoByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
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
        
        [Route("GetTcsParametro"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametro> GetTcsParametro()
        {
            return repository.Context.GetTcsParametro();
        }
        
        [Route("GetTcsParametroNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametro> GetTcsParametroNoAssociations()
        {
            return repository.Context.GetTcsParametroNoAssociations();
        }
        
        [Route("GetTcsParametroByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametro> GetTcsParametroByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametro), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsParametroByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametro> GetTcsParametroByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametro), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsParametroToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsParametroToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametro), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdParametro asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TcsParametro");
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
               return ExcelExportPagination<BusinessNS.TcsParametro>.CreateExcelDocumentFileMapPath("TcsParametro",new ExcelExportPagination<BusinessNS.TcsParametro>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsParametroToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsParametroToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametro), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TcsParametro", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ParametroDataSource", DataSourceObject = "GetTcsParametro", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsParametro"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametro> GetSampleTcsParametro(string details)
        {
            var result = repository.Context.GetTcsParametroByEntitySearchNoAssociations(null).Take(100).ToList();
               if (!details.IsNullOrEmpty())
               {
                   foreach(var entity in result)
                   {
                       entity.FillDetails(repository.Context, null, null, details.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries), 5);
                   }
               }
            return result.AsQueryable();
        }
        
        [Route("AddTcsParametroEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsParametroEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametro), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsParametroByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametro> GetTcsParametroByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsParametroByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsParametroValorP"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorP> GetTcsParametroValorP()
        {
            return repository.Context.GetTcsParametroValorP();
        }
        
        [Route("GetTcsParametroValorPNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorP> GetTcsParametroValorPNoAssociations()
        {
            return repository.Context.GetTcsParametroValorPNoAssociations();
        }
        
        [Route("GetTcsParametroValorPByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorP> GetTcsParametroValorPByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroValorPByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorP), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsParametroValorPByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorP> GetTcsParametroValorPByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroValorPByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorP), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsParametroValorPToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsParametroValorPToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorP), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroValorPByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdParametroValor asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TcsParametroValorP");
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
               return ExcelExportPagination<BusinessNS.TcsParametroValorP>.CreateExcelDocumentFileMapPath("TcsParametroValorP",new ExcelExportPagination<BusinessNS.TcsParametroValorP>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsParametroValorPToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsParametroValorPToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorP), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroValorPByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TcsParametroValorP", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ParametroDataSource", DataSourceObject = "GetTcsParametroValorP", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsParametroValorP"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorP> GetSampleTcsParametroValorP(string details)
        {
            var result = repository.Context.GetTcsParametroValorPByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsParametroValorPEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsParametroValorPEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorP), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsParametroValorPByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorP> GetTcsParametroValorPByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsParametroValorPByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsParametroValorVariacaoP"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorVariacaoP> GetTcsParametroValorVariacaoP()
        {
            return repository.Context.GetTcsParametroValorVariacaoP();
        }
        
        [Route("GetTcsParametroValorVariacaoPNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorVariacaoP> GetTcsParametroValorVariacaoPNoAssociations()
        {
            return repository.Context.GetTcsParametroValorVariacaoPNoAssociations();
        }
        
        [Route("GetTcsParametroValorVariacaoPByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorVariacaoP> GetTcsParametroValorVariacaoPByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroValorVariacaoPByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorVariacaoP), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsParametroValorVariacaoPByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorVariacaoP> GetTcsParametroValorVariacaoPByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroValorVariacaoPByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorVariacaoP), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsParametroValorVariacaoPToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsParametroValorVariacaoPToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorVariacaoP), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroValorVariacaoPByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdParametroValor asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TcsParametroValorVariacaoP");
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
               return ExcelExportPagination<BusinessNS.TcsParametroValorVariacaoP>.CreateExcelDocumentFileMapPath("TcsParametroValorVariacaoP",new ExcelExportPagination<BusinessNS.TcsParametroValorVariacaoP>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsParametroValorVariacaoPToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsParametroValorVariacaoPToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorVariacaoP), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroValorVariacaoPByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TcsParametroValorVariacaoP", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ParametroDataSource", DataSourceObject = "GetTcsParametroValorVariacaoP", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsParametroValorVariacaoP"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorVariacaoP> GetSampleTcsParametroValorVariacaoP(string details)
        {
            var result = repository.Context.GetTcsParametroValorVariacaoPByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsParametroValorVariacaoPEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsParametroValorVariacaoPEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorVariacaoP), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsParametroValorVariacaoPByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorVariacaoP> GetTcsParametroValorVariacaoPByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsParametroValorVariacaoPByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsParametroTabelaSelecao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroTabelaSelecao> GetTcsParametroTabelaSelecao()
        {
            return repository.Context.GetTcsParametroTabelaSelecao();
        }
        
        [Route("GetTcsParametroTabelaSelecaoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroTabelaSelecao> GetTcsParametroTabelaSelecaoNoAssociations()
        {
            return repository.Context.GetTcsParametroTabelaSelecaoNoAssociations();
        }
        
        [Route("GetTcsParametroTabelaSelecaoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroTabelaSelecao> GetTcsParametroTabelaSelecaoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroTabelaSelecaoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroTabelaSelecao), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsParametroTabelaSelecaoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroTabelaSelecao> GetTcsParametroTabelaSelecaoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroTabelaSelecaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroTabelaSelecao), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsParametroTabelaSelecaoToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsParametroTabelaSelecaoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroTabelaSelecao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroTabelaSelecaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("DescTabela asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TcsParametroTabelaSelecao");
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
               return ExcelExportPagination<BusinessNS.TcsParametroTabelaSelecao>.CreateExcelDocumentFileMapPath("TcsParametroTabelaSelecao",new ExcelExportPagination<BusinessNS.TcsParametroTabelaSelecao>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsParametroTabelaSelecaoToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsParametroTabelaSelecaoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroTabelaSelecao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroTabelaSelecaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TcsParametroTabelaSelecao", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ParametroDataSource", DataSourceObject = "GetTcsParametroTabelaSelecao", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsParametroTabelaSelecao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroTabelaSelecao> GetSampleTcsParametroTabelaSelecao(string details)
        {
            var result = repository.Context.GetTcsParametroTabelaSelecaoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsParametroTabelaSelecaoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsParametroTabelaSelecaoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroTabelaSelecao), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsParametroTabelaSelecaoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroTabelaSelecao> GetTcsParametroTabelaSelecaoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsParametroTabelaSelecaoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
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
            var entities = repository.Context.GetTcsParametroValorByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdParametro asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TcsParametroValor");
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TcsParametroValor", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ParametroDataSource", DataSourceObject = "GetTcsParametroValor", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsParametroValor"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValor> GetSampleTcsParametroValor(string details)
        {
            var result = repository.Context.GetTcsParametroValorByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsParametroValorEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsParametroValorEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValor), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsParametroValorByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValor> GetTcsParametroValorByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsParametroValorByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsUsuarioParametro"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioParametro> GetTcsUsuarioParametro()
        {
            return repository.Context.GetTcsUsuarioParametro();
        }
        
        [Route("GetTcsUsuarioParametroNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioParametro> GetTcsUsuarioParametroNoAssociations()
        {
            return repository.Context.GetTcsUsuarioParametroNoAssociations();
        }
        
        [Route("GetTcsUsuarioParametroByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioParametro> GetTcsUsuarioParametroByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsUsuarioParametroByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioParametro), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsUsuarioParametroByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioParametro> GetTcsUsuarioParametroByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsUsuarioParametroByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioParametro), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsUsuarioParametroToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsUsuarioParametroToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioParametro), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsUsuarioParametroByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdUsuario asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TcsUsuarioParametro");
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
               return ExcelExportPagination<BusinessNS.TcsUsuarioParametro>.CreateExcelDocumentFileMapPath("TcsUsuarioParametro",new ExcelExportPagination<BusinessNS.TcsUsuarioParametro>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsUsuarioParametroToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsUsuarioParametroToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioParametro), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsUsuarioParametroByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TcsUsuarioParametro", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ParametroDataSource", DataSourceObject = "GetTcsUsuarioParametro", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsUsuarioParametro"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioParametro> GetSampleTcsUsuarioParametro(string details)
        {
            var result = repository.Context.GetTcsUsuarioParametroByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsUsuarioParametroEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsUsuarioParametroEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioParametro), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsUsuarioParametroByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioParametro> GetTcsUsuarioParametroByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsUsuarioParametroByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsParametroValorUsuario"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorUsuario> GetTcsParametroValorUsuario()
        {
            return repository.Context.GetTcsParametroValorUsuario();
        }
        
        [Route("GetTcsParametroValorUsuarioNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorUsuario> GetTcsParametroValorUsuarioNoAssociations()
        {
            return repository.Context.GetTcsParametroValorUsuarioNoAssociations();
        }
        
        [Route("GetTcsParametroValorUsuarioByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorUsuario> GetTcsParametroValorUsuarioByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroValorUsuarioByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorUsuario), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsParametroValorUsuarioByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorUsuario> GetTcsParametroValorUsuarioByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroValorUsuarioByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorUsuario), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsParametroValorUsuarioToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsParametroValorUsuarioToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorUsuario), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroValorUsuarioByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdParametroValor asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TcsParametroValorUsuario");
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
               return ExcelExportPagination<BusinessNS.TcsParametroValorUsuario>.CreateExcelDocumentFileMapPath("TcsParametroValorUsuario",new ExcelExportPagination<BusinessNS.TcsParametroValorUsuario>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsParametroValorUsuarioToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsParametroValorUsuarioToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorUsuario), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroValorUsuarioByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TcsParametroValorUsuario", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ParametroDataSource", DataSourceObject = "GetTcsParametroValorUsuario", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsParametroValorUsuario"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorUsuario> GetSampleTcsParametroValorUsuario(string details)
        {
            var result = repository.Context.GetTcsParametroValorUsuarioByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsParametroValorUsuarioEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsParametroValorUsuarioEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorUsuario), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsParametroValorUsuarioByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorUsuario> GetTcsParametroValorUsuarioByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsParametroValorUsuarioByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTbcBandeiraRedeParametro"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcBandeiraRedeParametro> GetTbcBandeiraRedeParametro()
        {
            return repository.Context.GetTbcBandeiraRedeParametro();
        }
        
        [Route("GetTbcBandeiraRedeParametroNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcBandeiraRedeParametro> GetTbcBandeiraRedeParametroNoAssociations()
        {
            return repository.Context.GetTbcBandeiraRedeParametroNoAssociations();
        }
        
        [Route("GetTbcBandeiraRedeParametroByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcBandeiraRedeParametro> GetTbcBandeiraRedeParametroByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTbcBandeiraRedeParametroByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcBandeiraRedeParametro), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTbcBandeiraRedeParametroByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcBandeiraRedeParametro> GetTbcBandeiraRedeParametroByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTbcBandeiraRedeParametroByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcBandeiraRedeParametro), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTbcBandeiraRedeParametroToExcel"), System.Web.Http.HttpPost()]
        public string GetTbcBandeiraRedeParametroToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcBandeiraRedeParametro), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTbcBandeiraRedeParametroByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdBandeiraRedeParam asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TbcBandeiraRedeParametro");
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
               return ExcelExportPagination<BusinessNS.TbcBandeiraRedeParametro>.CreateExcelDocumentFileMapPath("TbcBandeiraRedeParametro",new ExcelExportPagination<BusinessNS.TbcBandeiraRedeParametro>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTbcBandeiraRedeParametroToReportXml"), System.Web.Http.HttpPost()]
        public string GetTbcBandeiraRedeParametroToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcBandeiraRedeParametro), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTbcBandeiraRedeParametroByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TbcBandeiraRedeParametro", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ParametroDataSource", DataSourceObject = "GetTbcBandeiraRedeParametro", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTbcBandeiraRedeParametro"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcBandeiraRedeParametro> GetSampleTbcBandeiraRedeParametro(string details)
        {
            var result = repository.Context.GetTbcBandeiraRedeParametroByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTbcBandeiraRedeParametroEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTbcBandeiraRedeParametroEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcBandeiraRedeParametro), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTbcBandeiraRedeParametroByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcBandeiraRedeParametro> GetTbcBandeiraRedeParametroByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTbcBandeiraRedeParametroByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsParametroValorRede"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorRede> GetTcsParametroValorRede()
        {
            return repository.Context.GetTcsParametroValorRede();
        }
        
        [Route("GetTcsParametroValorRedeNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorRede> GetTcsParametroValorRedeNoAssociations()
        {
            return repository.Context.GetTcsParametroValorRedeNoAssociations();
        }
        
        [Route("GetTcsParametroValorRedeByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorRede> GetTcsParametroValorRedeByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroValorRedeByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorRede), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsParametroValorRedeByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorRede> GetTcsParametroValorRedeByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroValorRedeByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorRede), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsParametroValorRedeToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsParametroValorRedeToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorRede), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroValorRedeByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdParametroValor asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TcsParametroValorRede");
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
               return ExcelExportPagination<BusinessNS.TcsParametroValorRede>.CreateExcelDocumentFileMapPath("TcsParametroValorRede",new ExcelExportPagination<BusinessNS.TcsParametroValorRede>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsParametroValorRedeToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsParametroValorRedeToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorRede), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroValorRedeByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TcsParametroValorRede", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ParametroDataSource", DataSourceObject = "GetTcsParametroValorRede", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsParametroValorRede"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorRede> GetSampleTcsParametroValorRede(string details)
        {
            var result = repository.Context.GetTcsParametroValorRedeByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsParametroValorRedeEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsParametroValorRedeEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorRede), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsParametroValorRedeByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorRede> GetTcsParametroValorRedeByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsParametroValorRedeByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTbcGrupoEconomicoParametro"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcGrupoEconomicoParametro> GetTbcGrupoEconomicoParametro()
        {
            return repository.Context.GetTbcGrupoEconomicoParametro();
        }
        
        [Route("GetTbcGrupoEconomicoParametroNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcGrupoEconomicoParametro> GetTbcGrupoEconomicoParametroNoAssociations()
        {
            return repository.Context.GetTbcGrupoEconomicoParametroNoAssociations();
        }
        
        [Route("GetTbcGrupoEconomicoParametroByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcGrupoEconomicoParametro> GetTbcGrupoEconomicoParametroByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTbcGrupoEconomicoParametroByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcGrupoEconomicoParametro), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTbcGrupoEconomicoParametroByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcGrupoEconomicoParametro> GetTbcGrupoEconomicoParametroByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTbcGrupoEconomicoParametroByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcGrupoEconomicoParametro), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTbcGrupoEconomicoParametroToExcel"), System.Web.Http.HttpPost()]
        public string GetTbcGrupoEconomicoParametroToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcGrupoEconomicoParametro), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTbcGrupoEconomicoParametroByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdGpecon asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TbcGrupoEconomicoParametro");
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
               return ExcelExportPagination<BusinessNS.TbcGrupoEconomicoParametro>.CreateExcelDocumentFileMapPath("TbcGrupoEconomicoParametro",new ExcelExportPagination<BusinessNS.TbcGrupoEconomicoParametro>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTbcGrupoEconomicoParametroToReportXml"), System.Web.Http.HttpPost()]
        public string GetTbcGrupoEconomicoParametroToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcGrupoEconomicoParametro), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTbcGrupoEconomicoParametroByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TbcGrupoEconomicoParametro", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ParametroDataSource", DataSourceObject = "GetTbcGrupoEconomicoParametro", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTbcGrupoEconomicoParametro"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcGrupoEconomicoParametro> GetSampleTbcGrupoEconomicoParametro(string details)
        {
            var result = repository.Context.GetTbcGrupoEconomicoParametroByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTbcGrupoEconomicoParametroEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTbcGrupoEconomicoParametroEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcGrupoEconomicoParametro), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTbcGrupoEconomicoParametroByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcGrupoEconomicoParametro> GetTbcGrupoEconomicoParametroByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTbcGrupoEconomicoParametroByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsParametroValorGpecon"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorGpecon> GetTcsParametroValorGpecon()
        {
            return repository.Context.GetTcsParametroValorGpecon();
        }
        
        [Route("GetTcsParametroValorGpeconNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorGpecon> GetTcsParametroValorGpeconNoAssociations()
        {
            return repository.Context.GetTcsParametroValorGpeconNoAssociations();
        }
        
        [Route("GetTcsParametroValorGpeconByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorGpecon> GetTcsParametroValorGpeconByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroValorGpeconByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorGpecon), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsParametroValorGpeconByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorGpecon> GetTcsParametroValorGpeconByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroValorGpeconByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorGpecon), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsParametroValorGpeconToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsParametroValorGpeconToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorGpecon), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroValorGpeconByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdParametroValor asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TcsParametroValorGpecon");
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
               return ExcelExportPagination<BusinessNS.TcsParametroValorGpecon>.CreateExcelDocumentFileMapPath("TcsParametroValorGpecon",new ExcelExportPagination<BusinessNS.TcsParametroValorGpecon>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsParametroValorGpeconToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsParametroValorGpeconToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorGpecon), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroValorGpeconByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TcsParametroValorGpecon", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ParametroDataSource", DataSourceObject = "GetTcsParametroValorGpecon", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsParametroValorGpecon"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorGpecon> GetSampleTcsParametroValorGpecon(string details)
        {
            var result = repository.Context.GetTcsParametroValorGpeconByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsParametroValorGpeconEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsParametroValorGpeconEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorGpecon), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsParametroValorGpeconByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorGpecon> GetTcsParametroValorGpeconByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsParametroValorGpeconByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTbcFilialParametro"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcFilialParametro> GetTbcFilialParametro()
        {
            return repository.Context.GetTbcFilialParametro();
        }
        
        [Route("GetTbcFilialParametroNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcFilialParametro> GetTbcFilialParametroNoAssociations()
        {
            return repository.Context.GetTbcFilialParametroNoAssociations();
        }
        
        [Route("GetTbcFilialParametroByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcFilialParametro> GetTbcFilialParametroByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTbcFilialParametroByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcFilialParametro), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTbcFilialParametroByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcFilialParametro> GetTbcFilialParametroByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTbcFilialParametroByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcFilialParametro), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTbcFilialParametroToExcel"), System.Web.Http.HttpPost()]
        public string GetTbcFilialParametroToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcFilialParametro), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTbcFilialParametroByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdFilialPfj asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TbcFilialParametro");
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
               return ExcelExportPagination<BusinessNS.TbcFilialParametro>.CreateExcelDocumentFileMapPath("TbcFilialParametro",new ExcelExportPagination<BusinessNS.TbcFilialParametro>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTbcFilialParametroToReportXml"), System.Web.Http.HttpPost()]
        public string GetTbcFilialParametroToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcFilialParametro), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTbcFilialParametroByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TbcFilialParametro", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ParametroDataSource", DataSourceObject = "GetTbcFilialParametro", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTbcFilialParametro"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcFilialParametro> GetSampleTbcFilialParametro(string details)
        {
            var result = repository.Context.GetTbcFilialParametroByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTbcFilialParametroEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTbcFilialParametroEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcFilialParametro), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTbcFilialParametroByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcFilialParametro> GetTbcFilialParametroByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTbcFilialParametroByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsParametroValorFilial"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorFilial> GetTcsParametroValorFilial()
        {
            return repository.Context.GetTcsParametroValorFilial();
        }
        
        [Route("GetTcsParametroValorFilialNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorFilial> GetTcsParametroValorFilialNoAssociations()
        {
            return repository.Context.GetTcsParametroValorFilialNoAssociations();
        }
        
        [Route("GetTcsParametroValorFilialByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorFilial> GetTcsParametroValorFilialByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroValorFilialByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorFilial), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsParametroValorFilialByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorFilial> GetTcsParametroValorFilialByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroValorFilialByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorFilial), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsParametroValorFilialToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsParametroValorFilialToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorFilial), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroValorFilialByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdParametroValor asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TcsParametroValorFilial");
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
               return ExcelExportPagination<BusinessNS.TcsParametroValorFilial>.CreateExcelDocumentFileMapPath("TcsParametroValorFilial",new ExcelExportPagination<BusinessNS.TcsParametroValorFilial>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsParametroValorFilialToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsParametroValorFilialToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorFilial), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroValorFilialByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TcsParametroValorFilial", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ParametroDataSource", DataSourceObject = "GetTcsParametroValorFilial", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsParametroValorFilial"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorFilial> GetSampleTcsParametroValorFilial(string details)
        {
            var result = repository.Context.GetTcsParametroValorFilialByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsParametroValorFilialEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsParametroValorFilialEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorFilial), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsParametroValorFilialByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorFilial> GetTcsParametroValorFilialByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsParametroValorFilialByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsParametroValorLjvLoja"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorLjvLoja> GetTcsParametroValorLjvLoja()
        {
            return repository.Context.GetTcsParametroValorLjvLoja();
        }
        
        [Route("GetTcsParametroValorLjvLojaNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorLjvLoja> GetTcsParametroValorLjvLojaNoAssociations()
        {
            return repository.Context.GetTcsParametroValorLjvLojaNoAssociations();
        }
        
        [Route("GetTcsParametroValorLjvLojaByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorLjvLoja> GetTcsParametroValorLjvLojaByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroValorLjvLojaByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorLjvLoja), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsParametroValorLjvLojaByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorLjvLoja> GetTcsParametroValorLjvLojaByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroValorLjvLojaByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorLjvLoja), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsParametroValorLjvLojaToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsParametroValorLjvLojaToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorLjvLoja), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroValorLjvLojaByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdParametroValor asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TcsParametroValorLjvLoja");
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
               return ExcelExportPagination<BusinessNS.TcsParametroValorLjvLoja>.CreateExcelDocumentFileMapPath("TcsParametroValorLjvLoja",new ExcelExportPagination<BusinessNS.TcsParametroValorLjvLoja>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsParametroValorLjvLojaToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsParametroValorLjvLojaToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorLjvLoja), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroValorLjvLojaByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TcsParametroValorLjvLoja", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ParametroDataSource", DataSourceObject = "GetTcsParametroValorLjvLoja", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsParametroValorLjvLoja"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorLjvLoja> GetSampleTcsParametroValorLjvLoja(string details)
        {
            var result = repository.Context.GetTcsParametroValorLjvLojaByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsParametroValorLjvLojaEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsParametroValorLjvLojaEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorLjvLoja), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsParametroValorLjvLojaByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorLjvLoja> GetTcsParametroValorLjvLojaByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsParametroValorLjvLojaByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsParametroValorVariacaoGenericaP"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorVariacaoGenericaP> GetTcsParametroValorVariacaoGenericaP()
        {
            return repository.Context.GetTcsParametroValorVariacaoGenericaP();
        }
        
        [Route("GetTcsParametroValorVariacaoGenericaPNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorVariacaoGenericaP> GetTcsParametroValorVariacaoGenericaPNoAssociations()
        {
            return repository.Context.GetTcsParametroValorVariacaoGenericaPNoAssociations();
        }
        
        [Route("GetTcsParametroValorVariacaoGenericaPByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorVariacaoGenericaP> GetTcsParametroValorVariacaoGenericaPByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroValorVariacaoGenericaPByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorVariacaoGenericaP), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsParametroValorVariacaoGenericaPByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorVariacaoGenericaP> GetTcsParametroValorVariacaoGenericaPByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroValorVariacaoGenericaPByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorVariacaoGenericaP), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsParametroValorVariacaoGenericaPToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsParametroValorVariacaoGenericaPToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorVariacaoGenericaP), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroValorVariacaoGenericaPByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdParametroValor asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TcsParametroValorVariacaoGenericaP");
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
               return ExcelExportPagination<BusinessNS.TcsParametroValorVariacaoGenericaP>.CreateExcelDocumentFileMapPath("TcsParametroValorVariacaoGenericaP",new ExcelExportPagination<BusinessNS.TcsParametroValorVariacaoGenericaP>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsParametroValorVariacaoGenericaPToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsParametroValorVariacaoGenericaPToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorVariacaoGenericaP), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroValorVariacaoGenericaPByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TcsParametroValorVariacaoGenericaP", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ParametroDataSource", DataSourceObject = "GetTcsParametroValorVariacaoGenericaP", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsParametroValorVariacaoGenericaP"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorVariacaoGenericaP> GetSampleTcsParametroValorVariacaoGenericaP(string details)
        {
            var result = repository.Context.GetTcsParametroValorVariacaoGenericaPByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsParametroValorVariacaoGenericaPEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsParametroValorVariacaoGenericaPEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorVariacaoGenericaP), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsParametroValorVariacaoGenericaPByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorVariacaoGenericaP> GetTcsParametroValorVariacaoGenericaPByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsParametroValorVariacaoGenericaPByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsParametroValorVariacaoGenerica"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorVariacaoGenerica> GetTcsParametroValorVariacaoGenerica()
        {
            return repository.Context.GetTcsParametroValorVariacaoGenerica();
        }
        
        [Route("GetTcsParametroValorVariacaoGenericaNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorVariacaoGenerica> GetTcsParametroValorVariacaoGenericaNoAssociations()
        {
            return repository.Context.GetTcsParametroValorVariacaoGenericaNoAssociations();
        }
        
        [Route("GetTcsParametroValorVariacaoGenericaByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorVariacaoGenerica> GetTcsParametroValorVariacaoGenericaByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroValorVariacaoGenericaByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorVariacaoGenerica), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsParametroValorVariacaoGenericaByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorVariacaoGenerica> GetTcsParametroValorVariacaoGenericaByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroValorVariacaoGenericaByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorVariacaoGenerica), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsParametroValorVariacaoGenericaToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsParametroValorVariacaoGenericaToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorVariacaoGenerica), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroValorVariacaoGenericaByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdParametroValor asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TcsParametroValorVariacaoGenerica");
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
               return ExcelExportPagination<BusinessNS.TcsParametroValorVariacaoGenerica>.CreateExcelDocumentFileMapPath("TcsParametroValorVariacaoGenerica",new ExcelExportPagination<BusinessNS.TcsParametroValorVariacaoGenerica>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsParametroValorVariacaoGenericaToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsParametroValorVariacaoGenericaToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorVariacaoGenerica), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroValorVariacaoGenericaByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TcsParametroValorVariacaoGenerica", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ParametroDataSource", DataSourceObject = "GetTcsParametroValorVariacaoGenerica", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsParametroValorVariacaoGenerica"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorVariacaoGenerica> GetSampleTcsParametroValorVariacaoGenerica(string details)
        {
            var result = repository.Context.GetTcsParametroValorVariacaoGenericaByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsParametroValorVariacaoGenericaEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsParametroValorVariacaoGenericaEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorVariacaoGenerica), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsParametroValorVariacaoGenericaByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorVariacaoGenerica> GetTcsParametroValorVariacaoGenericaByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsParametroValorVariacaoGenericaByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsParametroValorP1"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorP1> GetTcsParametroValorP1()
        {
            return repository.Context.GetTcsParametroValorP1();
        }
        
        [Route("GetTcsParametroValorP1NoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorP1> GetTcsParametroValorP1NoAssociations()
        {
            return repository.Context.GetTcsParametroValorP1NoAssociations();
        }
        
        [Route("GetTcsParametroValorP1ByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorP1> GetTcsParametroValorP1ByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroValorP1ByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorP1), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsParametroValorP1ByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorP1> GetTcsParametroValorP1ByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroValorP1ByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorP1), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsParametroValorP1ToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsParametroValorP1ToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorP1), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroValorP1ByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdParametroValor asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TcsParametroValorP1");
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
               return ExcelExportPagination<BusinessNS.TcsParametroValorP1>.CreateExcelDocumentFileMapPath("TcsParametroValorP1",new ExcelExportPagination<BusinessNS.TcsParametroValorP1>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsParametroValorP1ToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsParametroValorP1ToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorP1), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroValorP1ByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TcsParametroValorP1", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ParametroDataSource", DataSourceObject = "GetTcsParametroValorP1", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsParametroValorP1"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorP1> GetSampleTcsParametroValorP1(string details)
        {
            var result = repository.Context.GetTcsParametroValorP1ByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsParametroValorP1EntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsParametroValorP1EntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorP1), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsParametroValorP1ByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorP1> GetTcsParametroValorP1ByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsParametroValorP1ByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetParametroInfo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.ParametroInfo> GetParametroInfo()
        {
            return repository.Context.GetParametroInfo().AsQueryable();
        }
        
        [Route("GetParametroInfoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.ParametroInfo> GetParametroInfoNoAssociations()
        {
            return repository.Context.GetParametroInfoNoAssociations().AsQueryable();
        }
        
        [Route("GetParametroInfoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.ParametroInfo> GetParametroInfoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetParametroInfoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.ParametroInfo), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        
        [Route("GetParametroInfoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.ParametroInfo> GetParametroInfoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetParametroInfoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.ParametroInfo), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        [Route("GetParametroInfoToExcel"), System.Web.Http.HttpPost()]
        public string GetParametroInfoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.ParametroInfo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetParametroInfoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).AsQueryable().OrderBy("IdTcsAmbiente asc, TituloParametro asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.ParametroInfo");
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
               return ExcelExportPagination<BusinessNS.ParametroInfo>.CreateExcelDocumentFileMapPath("ParametroInfo",new ExcelExportPagination<BusinessNS.ParametroInfo>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetParametroInfoToReportXml"), System.Web.Http.HttpPost()]
        public string GetParametroInfoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.ParametroInfo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetParametroInfoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.ParametroInfo", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ParametroDataSource", DataSourceObject = "GetParametroInfo", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleParametroInfo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.ParametroInfo> GetSampleParametroInfo(string details)
        {
            var result = repository.Context.GetParametroInfoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddParametroInfoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddParametroInfoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.ParametroInfo), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetParametroInfoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.ParametroInfo> GetParametroInfoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetParametroInfoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsParametroValorP2"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorP2> GetTcsParametroValorP2()
        {
            return repository.Context.GetTcsParametroValorP2();
        }
        
        [Route("GetTcsParametroValorP2NoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorP2> GetTcsParametroValorP2NoAssociations()
        {
            return repository.Context.GetTcsParametroValorP2NoAssociations();
        }
        
        [Route("GetTcsParametroValorP2ByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorP2> GetTcsParametroValorP2ByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroValorP2ByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorP2), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsParametroValorP2ByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorP2> GetTcsParametroValorP2ByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroValorP2ByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorP2), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsParametroValorP2ToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsParametroValorP2ToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorP2), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroValorP2ByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdParametroValor asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TcsParametroValorP2");
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
               return ExcelExportPagination<BusinessNS.TcsParametroValorP2>.CreateExcelDocumentFileMapPath("TcsParametroValorP2",new ExcelExportPagination<BusinessNS.TcsParametroValorP2>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsParametroValorP2ToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsParametroValorP2ToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorP2), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroValorP2ByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TcsParametroValorP2", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ParametroDataSource", DataSourceObject = "GetTcsParametroValorP2", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsParametroValorP2"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorP2> GetSampleTcsParametroValorP2(string details)
        {
            var result = repository.Context.GetTcsParametroValorP2ByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsParametroValorP2EntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsParametroValorP2EntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorP2), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsParametroValorP2ByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorP2> GetTcsParametroValorP2ByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsParametroValorP2ByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsParametroValorLojaP"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorLojaP> GetTcsParametroValorLojaP()
        {
            return repository.Context.GetTcsParametroValorLojaP();
        }
        
        [Route("GetTcsParametroValorLojaPNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorLojaP> GetTcsParametroValorLojaPNoAssociations()
        {
            return repository.Context.GetTcsParametroValorLojaPNoAssociations();
        }
        
        [Route("GetTcsParametroValorLojaPByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorLojaP> GetTcsParametroValorLojaPByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroValorLojaPByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorLojaP), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsParametroValorLojaPByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorLojaP> GetTcsParametroValorLojaPByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroValorLojaPByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorLojaP), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsParametroValorLojaPToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsParametroValorLojaPToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorLojaP), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroValorLojaPByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdParametroValor asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TcsParametroValorLojaP");
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
               return ExcelExportPagination<BusinessNS.TcsParametroValorLojaP>.CreateExcelDocumentFileMapPath("TcsParametroValorLojaP",new ExcelExportPagination<BusinessNS.TcsParametroValorLojaP>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsParametroValorLojaPToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsParametroValorLojaPToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorLojaP), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroValorLojaPByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TcsParametroValorLojaP", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ParametroDataSource", DataSourceObject = "GetTcsParametroValorLojaP", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsParametroValorLojaP"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorLojaP> GetSampleTcsParametroValorLojaP(string details)
        {
            var result = repository.Context.GetTcsParametroValorLojaPByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsParametroValorLojaPEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsParametroValorLojaPEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorLojaP), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsParametroValorLojaPByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorLojaP> GetTcsParametroValorLojaPByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsParametroValorLojaPByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetLjvLojaParametro"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LjvLojaParametro> GetLjvLojaParametro()
        {
            return repository.Context.GetLjvLojaParametro();
        }
        
        [Route("GetLjvLojaParametroNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LjvLojaParametro> GetLjvLojaParametroNoAssociations()
        {
            return repository.Context.GetLjvLojaParametroNoAssociations();
        }
        
        [Route("GetLjvLojaParametroByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LjvLojaParametro> GetLjvLojaParametroByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetLjvLojaParametroByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LjvLojaParametro), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetLjvLojaParametroByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LjvLojaParametro> GetLjvLojaParametroByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetLjvLojaParametroByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LjvLojaParametro), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetLjvLojaParametroToExcel"), System.Web.Http.HttpPost()]
        public string GetLjvLojaParametroToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LjvLojaParametro), jEntitySearch, false, false, false);
            var entities = repository.Context.GetLjvLojaParametroByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdLoja asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.LjvLojaParametro");
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
               return ExcelExportPagination<BusinessNS.LjvLojaParametro>.CreateExcelDocumentFileMapPath("LjvLojaParametro",new ExcelExportPagination<BusinessNS.LjvLojaParametro>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetLjvLojaParametroToReportXml"), System.Web.Http.HttpPost()]
        public string GetLjvLojaParametroToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LjvLojaParametro), jEntitySearch, false, false, false);
            var entities = repository.Context.GetLjvLojaParametroByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.LjvLojaParametro", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ParametroDataSource", DataSourceObject = "GetLjvLojaParametro", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleLjvLojaParametro"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LjvLojaParametro> GetSampleLjvLojaParametro(string details)
        {
            var result = repository.Context.GetLjvLojaParametroByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddLjvLojaParametroEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddLjvLojaParametroEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LjvLojaParametro), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetLjvLojaParametroByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LjvLojaParametro> GetLjvLojaParametroByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetLjvLojaParametroByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsParametroValorUsuarioP"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorUsuarioP> GetTcsParametroValorUsuarioP()
        {
            return repository.Context.GetTcsParametroValorUsuarioP();
        }
        
        [Route("GetTcsParametroValorUsuarioPNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorUsuarioP> GetTcsParametroValorUsuarioPNoAssociations()
        {
            return repository.Context.GetTcsParametroValorUsuarioPNoAssociations();
        }
        
        [Route("GetTcsParametroValorUsuarioPByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorUsuarioP> GetTcsParametroValorUsuarioPByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroValorUsuarioPByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorUsuarioP), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsParametroValorUsuarioPByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorUsuarioP> GetTcsParametroValorUsuarioPByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroValorUsuarioPByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorUsuarioP), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsParametroValorUsuarioPToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsParametroValorUsuarioPToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorUsuarioP), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroValorUsuarioPByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdParametroValor asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TcsParametroValorUsuarioP");
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
               return ExcelExportPagination<BusinessNS.TcsParametroValorUsuarioP>.CreateExcelDocumentFileMapPath("TcsParametroValorUsuarioP",new ExcelExportPagination<BusinessNS.TcsParametroValorUsuarioP>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsParametroValorUsuarioPToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsParametroValorUsuarioPToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorUsuarioP), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroValorUsuarioPByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TcsParametroValorUsuarioP", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ParametroDataSource", DataSourceObject = "GetTcsParametroValorUsuarioP", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsParametroValorUsuarioP"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorUsuarioP> GetSampleTcsParametroValorUsuarioP(string details)
        {
            var result = repository.Context.GetTcsParametroValorUsuarioPByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsParametroValorUsuarioPEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsParametroValorUsuarioPEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorUsuarioP), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsParametroValorUsuarioPByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorUsuarioP> GetTcsParametroValorUsuarioPByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsParametroValorUsuarioPByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsParametroValorRedeP"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorRedeP> GetTcsParametroValorRedeP()
        {
            return repository.Context.GetTcsParametroValorRedeP();
        }
        
        [Route("GetTcsParametroValorRedePNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorRedeP> GetTcsParametroValorRedePNoAssociations()
        {
            return repository.Context.GetTcsParametroValorRedePNoAssociations();
        }
        
        [Route("GetTcsParametroValorRedePByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorRedeP> GetTcsParametroValorRedePByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroValorRedePByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorRedeP), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsParametroValorRedePByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorRedeP> GetTcsParametroValorRedePByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroValorRedePByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorRedeP), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsParametroValorRedePToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsParametroValorRedePToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorRedeP), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroValorRedePByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdParametroValor asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TcsParametroValorRedeP");
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
               return ExcelExportPagination<BusinessNS.TcsParametroValorRedeP>.CreateExcelDocumentFileMapPath("TcsParametroValorRedeP",new ExcelExportPagination<BusinessNS.TcsParametroValorRedeP>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsParametroValorRedePToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsParametroValorRedePToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorRedeP), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroValorRedePByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TcsParametroValorRedeP", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ParametroDataSource", DataSourceObject = "GetTcsParametroValorRedeP", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsParametroValorRedeP"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorRedeP> GetSampleTcsParametroValorRedeP(string details)
        {
            var result = repository.Context.GetTcsParametroValorRedePByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsParametroValorRedePEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsParametroValorRedePEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorRedeP), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsParametroValorRedePByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorRedeP> GetTcsParametroValorRedePByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsParametroValorRedePByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsParametroValorGpeconP"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorGpeconP> GetTcsParametroValorGpeconP()
        {
            return repository.Context.GetTcsParametroValorGpeconP();
        }
        
        [Route("GetTcsParametroValorGpeconPNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorGpeconP> GetTcsParametroValorGpeconPNoAssociations()
        {
            return repository.Context.GetTcsParametroValorGpeconPNoAssociations();
        }
        
        [Route("GetTcsParametroValorGpeconPByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorGpeconP> GetTcsParametroValorGpeconPByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroValorGpeconPByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorGpeconP), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsParametroValorGpeconPByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorGpeconP> GetTcsParametroValorGpeconPByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroValorGpeconPByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorGpeconP), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsParametroValorGpeconPToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsParametroValorGpeconPToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorGpeconP), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroValorGpeconPByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdParametroValor asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TcsParametroValorGpeconP");
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
               return ExcelExportPagination<BusinessNS.TcsParametroValorGpeconP>.CreateExcelDocumentFileMapPath("TcsParametroValorGpeconP",new ExcelExportPagination<BusinessNS.TcsParametroValorGpeconP>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsParametroValorGpeconPToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsParametroValorGpeconPToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorGpeconP), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroValorGpeconPByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TcsParametroValorGpeconP", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ParametroDataSource", DataSourceObject = "GetTcsParametroValorGpeconP", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsParametroValorGpeconP"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorGpeconP> GetSampleTcsParametroValorGpeconP(string details)
        {
            var result = repository.Context.GetTcsParametroValorGpeconPByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsParametroValorGpeconPEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsParametroValorGpeconPEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorGpeconP), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsParametroValorGpeconPByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorGpeconP> GetTcsParametroValorGpeconPByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsParametroValorGpeconPByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsParametroValorFilialP"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorFilialP> GetTcsParametroValorFilialP()
        {
            return repository.Context.GetTcsParametroValorFilialP();
        }
        
        [Route("GetTcsParametroValorFilialPNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorFilialP> GetTcsParametroValorFilialPNoAssociations()
        {
            return repository.Context.GetTcsParametroValorFilialPNoAssociations();
        }
        
        [Route("GetTcsParametroValorFilialPByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorFilialP> GetTcsParametroValorFilialPByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroValorFilialPByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorFilialP), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsParametroValorFilialPByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorFilialP> GetTcsParametroValorFilialPByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroValorFilialPByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorFilialP), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsParametroValorFilialPToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsParametroValorFilialPToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorFilialP), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroValorFilialPByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdParametroValor asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TcsParametroValorFilialP");
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
               return ExcelExportPagination<BusinessNS.TcsParametroValorFilialP>.CreateExcelDocumentFileMapPath("TcsParametroValorFilialP",new ExcelExportPagination<BusinessNS.TcsParametroValorFilialP>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsParametroValorFilialPToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsParametroValorFilialPToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorFilialP), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroValorFilialPByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TcsParametroValorFilialP", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ParametroDataSource", DataSourceObject = "GetTcsParametroValorFilialP", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsParametroValorFilialP"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorFilialP> GetSampleTcsParametroValorFilialP(string details)
        {
            var result = repository.Context.GetTcsParametroValorFilialPByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsParametroValorFilialPEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsParametroValorFilialPEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorFilialP), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsParametroValorFilialPByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorFilialP> GetTcsParametroValorFilialPByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsParametroValorFilialPByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsParametroValorVariacaoGenericaP1"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorVariacaoGenericaP1> GetTcsParametroValorVariacaoGenericaP1()
        {
            return repository.Context.GetTcsParametroValorVariacaoGenericaP1();
        }
        
        [Route("GetTcsParametroValorVariacaoGenericaP1NoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorVariacaoGenericaP1> GetTcsParametroValorVariacaoGenericaP1NoAssociations()
        {
            return repository.Context.GetTcsParametroValorVariacaoGenericaP1NoAssociations();
        }
        
        [Route("GetTcsParametroValorVariacaoGenericaP1ByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorVariacaoGenericaP1> GetTcsParametroValorVariacaoGenericaP1ByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroValorVariacaoGenericaP1ByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorVariacaoGenericaP1), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsParametroValorVariacaoGenericaP1ByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorVariacaoGenericaP1> GetTcsParametroValorVariacaoGenericaP1ByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsParametroValorVariacaoGenericaP1ByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorVariacaoGenericaP1), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsParametroValorVariacaoGenericaP1ToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsParametroValorVariacaoGenericaP1ToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorVariacaoGenericaP1), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroValorVariacaoGenericaP1ByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdParametroValor asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TcsParametroValorVariacaoGenericaP1");
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
               return ExcelExportPagination<BusinessNS.TcsParametroValorVariacaoGenericaP1>.CreateExcelDocumentFileMapPath("TcsParametroValorVariacaoGenericaP1",new ExcelExportPagination<BusinessNS.TcsParametroValorVariacaoGenericaP1>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsParametroValorVariacaoGenericaP1ToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsParametroValorVariacaoGenericaP1ToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorVariacaoGenericaP1), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsParametroValorVariacaoGenericaP1ByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Parametro.TcsParametroValorVariacaoGenericaP1", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ParametroDataSource", DataSourceObject = "GetTcsParametroValorVariacaoGenericaP1", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsParametroValorVariacaoGenericaP1"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorVariacaoGenericaP1> GetSampleTcsParametroValorVariacaoGenericaP1(string details)
        {
            var result = repository.Context.GetTcsParametroValorVariacaoGenericaP1ByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsParametroValorVariacaoGenericaP1EntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsParametroValorVariacaoGenericaP1EntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorVariacaoGenericaP1), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsParametroValorVariacaoGenericaP1ByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsParametroValorVariacaoGenericaP1> GetTcsParametroValorVariacaoGenericaP1ByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsParametroValorVariacaoGenericaP1ByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
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
    
    public partial class LinxFrameworkParametroFeedController : ODataController
    {
        private BusinessNS.ParametroDomainService _context;
        public BusinessNS.ParametroDomainService Context { get {  if (_context == null) { _context = new BusinessNS.ParametroDomainService(); _context.IsSecure = true; } return _context; }  }
        
        #region Get Action to Business Entities
        
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametro> GetTcsParametroById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetTcsParametroByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsParametro[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsParametro>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametro> GetTcsParametroByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsParametroByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametro), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsParametro>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametro> GetTcsParametro()
        {
            return this.Context.GetTcsParametroByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroTabelaSelecao> GetTcsParametro__TcsParametroTabelaSelecao(Int64 key0, string navigation)
        {
            var entity = this.Context.GetTcsParametroByKey(key0);
            if (entity != null && navigation == "TcsParametroTabelaSelecaoList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsParametroTabelaSelecao" });
               return entity.TcsParametroTabelaSelecaoList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsParametroTabelaSelecao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValor> GetTcsParametro__TcsParametroValor(Int64 key0, string navigation)
        {
            var entity = this.Context.GetTcsParametroByKey(key0);
            if (entity != null && navigation == "TcsParametroValorList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsParametroValor" });
               return entity.TcsParametroValorList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsParametroValor>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorUsuario> GetTcsParametro__TcsParametroValorUsuario(Int64 key0, string navigation)
        {
            var entity = this.Context.GetTcsParametroByKey(key0);
            if (entity != null && navigation == "TcsParametroValorUsuarioList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsParametroValorUsuario" });
               return entity.TcsParametroValorUsuarioList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsParametroValorUsuario>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorRede> GetTcsParametro__TcsParametroValorRede(Int64 key0, string navigation)
        {
            var entity = this.Context.GetTcsParametroByKey(key0);
            if (entity != null && navigation == "TcsParametroValorRedeList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsParametroValorRede" });
               return entity.TcsParametroValorRedeList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsParametroValorRede>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorGpecon> GetTcsParametro__TcsParametroValorGpecon(Int64 key0, string navigation)
        {
            var entity = this.Context.GetTcsParametroByKey(key0);
            if (entity != null && navigation == "TcsParametroValorGpeconList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsParametroValorGpecon" });
               return entity.TcsParametroValorGpeconList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsParametroValorGpecon>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorFilial> GetTcsParametro__TcsParametroValorFilial(Int64 key0, string navigation)
        {
            var entity = this.Context.GetTcsParametroByKey(key0);
            if (entity != null && navigation == "TcsParametroValorFilialList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsParametroValorFilial" });
               return entity.TcsParametroValorFilialList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsParametroValorFilial>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorLjvLoja> GetTcsParametro__TcsParametroValorLjvLoja(Int64 key0, string navigation)
        {
            var entity = this.Context.GetTcsParametroByKey(key0);
            if (entity != null && navigation == "TcsParametroValorLjvLojaList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsParametroValorLjvLoja" });
               return entity.TcsParametroValorLjvLojaList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsParametroValorLjvLoja>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorVariacaoGenerica> GetTcsParametro__TcsParametroValorVariacaoGenerica(Int64 key0, string navigation)
        {
            var entity = this.Context.GetTcsParametroByKey(key0);
            if (entity != null && navigation == "TcsParametroValorVariacaoGenericaList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsParametroValorVariacaoGenerica" });
               return entity.TcsParametroValorVariacaoGenericaList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsParametroValorVariacaoGenerica>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorP> GetTcsParametroValorPById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetTcsParametroValorPByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsParametroValorP[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsParametroValorP>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorP> GetTcsParametroValorPByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsParametroValorPByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorP), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsParametroValorP>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorP> GetTcsParametroValorP()
        {
            return this.Context.GetTcsParametroValorPByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorVariacaoP> GetTcsParametroValorVariacaoPById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetTcsParametroValorVariacaoPByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsParametroValorVariacaoP[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsParametroValorVariacaoP>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorVariacaoP> GetTcsParametroValorVariacaoPByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsParametroValorVariacaoPByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorVariacaoP), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsParametroValorVariacaoP>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorVariacaoP> GetTcsParametroValorVariacaoP()
        {
            return this.Context.GetTcsParametroValorVariacaoPByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroTabelaSelecao> GetTcsParametroTabelaSelecaoById([FromODataUri]System.Guid key0)
        {
               return default(IQueryable<BusinessNS.TcsParametroTabelaSelecao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroTabelaSelecao> GetTcsParametroTabelaSelecaoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsParametroTabelaSelecaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroTabelaSelecao), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsParametroTabelaSelecao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroTabelaSelecao> GetTcsParametroTabelaSelecao()
        {
            return this.Context.GetTcsParametroTabelaSelecaoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametro> GetTcsParametroTabelaSelecao__TcsParametro(System.Guid key0, string navigation)
        {
               return default(IQueryable<BusinessNS.TcsParametro>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValor> GetTcsParametroValorById([FromODataUri]System.Guid key0)
        {
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
        public IQueryable<BusinessNS.TcsParametro> GetTcsParametroValor__TcsParametro(System.Guid key0, string navigation)
        {
               return default(IQueryable<BusinessNS.TcsParametro>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioParametro> GetTcsUsuarioParametroById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetTcsUsuarioParametroByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsUsuarioParametro[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsUsuarioParametro>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioParametro> GetTcsUsuarioParametroByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsUsuarioParametroByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioParametro), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsUsuarioParametro>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioParametro> GetTcsUsuarioParametro()
        {
            return this.Context.GetTcsUsuarioParametroByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorUsuario> GetTcsParametroValorUsuarioById([FromODataUri]long key0)
        {
            var entity = this.Context.GetTcsParametroValorUsuarioByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsParametroValorUsuario[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsParametroValorUsuario>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorUsuario> GetTcsParametroValorUsuarioByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsParametroValorUsuarioByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorUsuario), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsParametroValorUsuario>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorUsuario> GetTcsParametroValorUsuario()
        {
            return this.Context.GetTcsParametroValorUsuarioByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametro> GetTcsParametroValorUsuario__TcsParametro(long key0, string navigation)
        {
            var entity = this.Context.GetTcsParametroValorUsuarioByKey(key0);
            if (entity != null && navigation == "TcsParametro")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsParametro[] { entity.TcsParametro }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsParametro>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TbcBandeiraRedeParametro> GetTbcBandeiraRedeParametroById([FromODataUri]Int32 key0)
        {
            var entity = this.Context.GetTbcBandeiraRedeParametroByKey(key0);
            if (entity != null)
               return (new BusinessNS.TbcBandeiraRedeParametro[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TbcBandeiraRedeParametro>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TbcBandeiraRedeParametro> GetTbcBandeiraRedeParametroByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTbcBandeiraRedeParametroByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcBandeiraRedeParametro), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TbcBandeiraRedeParametro>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TbcBandeiraRedeParametro> GetTbcBandeiraRedeParametro()
        {
            return this.Context.GetTbcBandeiraRedeParametroByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorRede> GetTcsParametroValorRedeById([FromODataUri]long key0)
        {
            var entity = this.Context.GetTcsParametroValorRedeByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsParametroValorRede[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsParametroValorRede>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorRede> GetTcsParametroValorRedeByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsParametroValorRedeByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorRede), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsParametroValorRede>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorRede> GetTcsParametroValorRede()
        {
            return this.Context.GetTcsParametroValorRedeByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametro> GetTcsParametroValorRede__TcsParametro(long key0, string navigation)
        {
            var entity = this.Context.GetTcsParametroValorRedeByKey(key0);
            if (entity != null && navigation == "TcsParametro")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsParametro[] { entity.TcsParametro }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsParametro>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TbcGrupoEconomicoParametro> GetTbcGrupoEconomicoParametroById([FromODataUri]Int32 key0)
        {
            var entity = this.Context.GetTbcGrupoEconomicoParametroByKey(key0);
            if (entity != null)
               return (new BusinessNS.TbcGrupoEconomicoParametro[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TbcGrupoEconomicoParametro>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TbcGrupoEconomicoParametro> GetTbcGrupoEconomicoParametroByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTbcGrupoEconomicoParametroByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcGrupoEconomicoParametro), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TbcGrupoEconomicoParametro>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TbcGrupoEconomicoParametro> GetTbcGrupoEconomicoParametro()
        {
            return this.Context.GetTbcGrupoEconomicoParametroByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorGpecon> GetTcsParametroValorGpeconById([FromODataUri]long key0)
        {
            var entity = this.Context.GetTcsParametroValorGpeconByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsParametroValorGpecon[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsParametroValorGpecon>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorGpecon> GetTcsParametroValorGpeconByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsParametroValorGpeconByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorGpecon), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsParametroValorGpecon>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorGpecon> GetTcsParametroValorGpecon()
        {
            return this.Context.GetTcsParametroValorGpeconByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametro> GetTcsParametroValorGpecon__TcsParametro(long key0, string navigation)
        {
            var entity = this.Context.GetTcsParametroValorGpeconByKey(key0);
            if (entity != null && navigation == "TcsParametro")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsParametro[] { entity.TcsParametro }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsParametro>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TbcFilialParametro> GetTbcFilialParametroById([FromODataUri]Int32 key0)
        {
            var entity = this.Context.GetTbcFilialParametroByKey(key0);
            if (entity != null)
               return (new BusinessNS.TbcFilialParametro[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TbcFilialParametro>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TbcFilialParametro> GetTbcFilialParametroByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTbcFilialParametroByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcFilialParametro), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TbcFilialParametro>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TbcFilialParametro> GetTbcFilialParametro()
        {
            return this.Context.GetTbcFilialParametroByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorFilial> GetTcsParametroValorFilialById([FromODataUri]long key0)
        {
            var entity = this.Context.GetTcsParametroValorFilialByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsParametroValorFilial[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsParametroValorFilial>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorFilial> GetTcsParametroValorFilialByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsParametroValorFilialByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorFilial), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsParametroValorFilial>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorFilial> GetTcsParametroValorFilial()
        {
            return this.Context.GetTcsParametroValorFilialByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametro> GetTcsParametroValorFilial__TcsParametro(long key0, string navigation)
        {
            var entity = this.Context.GetTcsParametroValorFilialByKey(key0);
            if (entity != null && navigation == "TcsParametro")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsParametro[] { entity.TcsParametro }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsParametro>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorLjvLoja> GetTcsParametroValorLjvLojaById([FromODataUri]long key0)
        {
            var entity = this.Context.GetTcsParametroValorLjvLojaByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsParametroValorLjvLoja[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsParametroValorLjvLoja>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorLjvLoja> GetTcsParametroValorLjvLojaByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsParametroValorLjvLojaByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorLjvLoja), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsParametroValorLjvLoja>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorLjvLoja> GetTcsParametroValorLjvLoja()
        {
            return this.Context.GetTcsParametroValorLjvLojaByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametro> GetTcsParametroValorLjvLoja__TcsParametro(long key0, string navigation)
        {
            var entity = this.Context.GetTcsParametroValorLjvLojaByKey(key0);
            if (entity != null && navigation == "TcsParametro")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsParametro[] { entity.TcsParametro }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsParametro>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorVariacaoGenericaP> GetTcsParametroValorVariacaoGenericaPById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetTcsParametroValorVariacaoGenericaPByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsParametroValorVariacaoGenericaP[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsParametroValorVariacaoGenericaP>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorVariacaoGenericaP> GetTcsParametroValorVariacaoGenericaPByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsParametroValorVariacaoGenericaPByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorVariacaoGenericaP), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsParametroValorVariacaoGenericaP>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorVariacaoGenericaP> GetTcsParametroValorVariacaoGenericaP()
        {
            return this.Context.GetTcsParametroValorVariacaoGenericaPByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorVariacaoGenerica> GetTcsParametroValorVariacaoGenericaById([FromODataUri]long key0)
        {
            var entity = this.Context.GetTcsParametroValorVariacaoGenericaByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsParametroValorVariacaoGenerica[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsParametroValorVariacaoGenerica>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorVariacaoGenerica> GetTcsParametroValorVariacaoGenericaByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsParametroValorVariacaoGenericaByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorVariacaoGenerica), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsParametroValorVariacaoGenerica>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorVariacaoGenerica> GetTcsParametroValorVariacaoGenerica()
        {
            return this.Context.GetTcsParametroValorVariacaoGenericaByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametro> GetTcsParametroValorVariacaoGenerica__TcsParametro(long key0, string navigation)
        {
            var entity = this.Context.GetTcsParametroValorVariacaoGenericaByKey(key0);
            if (entity != null && navigation == "TcsParametro")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsParametro[] { entity.TcsParametro }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsParametro>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorP1> GetTcsParametroValorP1ById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetTcsParametroValorP1ByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsParametroValorP1[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsParametroValorP1>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorP1> GetTcsParametroValorP1ByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsParametroValorP1ByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorP1), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsParametroValorP1>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorP1> GetTcsParametroValorP1()
        {
            return this.Context.GetTcsParametroValorP1ByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.ParametroInfo> GetParametroInfoById([FromODataUri]int key0, [FromODataUri]string key1)
        {
            var entity = this.Context.GetParametroInfoByKey(key0, key1);
            if (entity != null)
               return (new BusinessNS.ParametroInfo[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.ParametroInfo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.ParametroInfo> GetParametroInfoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetParametroInfoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.ParametroInfo), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.ParametroInfo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.ParametroInfo> GetParametroInfo()
        {
            return this.Context.GetParametroInfoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorP2> GetTcsParametroValorP2ById([FromODataUri]long key0)
        {
            var entity = this.Context.GetTcsParametroValorP2ByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsParametroValorP2[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsParametroValorP2>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorP2> GetTcsParametroValorP2ByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsParametroValorP2ByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorP2), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsParametroValorP2>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorP2> GetTcsParametroValorP2()
        {
            return this.Context.GetTcsParametroValorP2ByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorLojaP> GetTcsParametroValorLojaPById([FromODataUri]long key0)
        {
            var entity = this.Context.GetTcsParametroValorLojaPByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsParametroValorLojaP[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsParametroValorLojaP>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorLojaP> GetTcsParametroValorLojaPByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsParametroValorLojaPByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorLojaP), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsParametroValorLojaP>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorLojaP> GetTcsParametroValorLojaP()
        {
            return this.Context.GetTcsParametroValorLojaPByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.LjvLojaParametro> GetLjvLojaParametroById([FromODataUri]int key0)
        {
            var entity = this.Context.GetLjvLojaParametroByKey(key0);
            if (entity != null)
               return (new BusinessNS.LjvLojaParametro[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.LjvLojaParametro>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.LjvLojaParametro> GetLjvLojaParametroByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetLjvLojaParametroByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LjvLojaParametro), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.LjvLojaParametro>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.LjvLojaParametro> GetLjvLojaParametro()
        {
            return this.Context.GetLjvLojaParametroByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorUsuarioP> GetTcsParametroValorUsuarioPById([FromODataUri]long key0)
        {
            var entity = this.Context.GetTcsParametroValorUsuarioPByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsParametroValorUsuarioP[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsParametroValorUsuarioP>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorUsuarioP> GetTcsParametroValorUsuarioPByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsParametroValorUsuarioPByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorUsuarioP), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsParametroValorUsuarioP>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorUsuarioP> GetTcsParametroValorUsuarioP()
        {
            return this.Context.GetTcsParametroValorUsuarioPByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorRedeP> GetTcsParametroValorRedePById([FromODataUri]long key0)
        {
            var entity = this.Context.GetTcsParametroValorRedePByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsParametroValorRedeP[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsParametroValorRedeP>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorRedeP> GetTcsParametroValorRedePByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsParametroValorRedePByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorRedeP), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsParametroValorRedeP>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorRedeP> GetTcsParametroValorRedeP()
        {
            return this.Context.GetTcsParametroValorRedePByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorGpeconP> GetTcsParametroValorGpeconPById([FromODataUri]long key0)
        {
            var entity = this.Context.GetTcsParametroValorGpeconPByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsParametroValorGpeconP[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsParametroValorGpeconP>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorGpeconP> GetTcsParametroValorGpeconPByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsParametroValorGpeconPByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorGpeconP), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsParametroValorGpeconP>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorGpeconP> GetTcsParametroValorGpeconP()
        {
            return this.Context.GetTcsParametroValorGpeconPByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorFilialP> GetTcsParametroValorFilialPById([FromODataUri]long key0)
        {
            var entity = this.Context.GetTcsParametroValorFilialPByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsParametroValorFilialP[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsParametroValorFilialP>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorFilialP> GetTcsParametroValorFilialPByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsParametroValorFilialPByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorFilialP), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsParametroValorFilialP>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorFilialP> GetTcsParametroValorFilialP()
        {
            return this.Context.GetTcsParametroValorFilialPByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorVariacaoGenericaP1> GetTcsParametroValorVariacaoGenericaP1ById([FromODataUri]long key0)
        {
            var entity = this.Context.GetTcsParametroValorVariacaoGenericaP1ByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsParametroValorVariacaoGenericaP1[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsParametroValorVariacaoGenericaP1>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorVariacaoGenericaP1> GetTcsParametroValorVariacaoGenericaP1ByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsParametroValorVariacaoGenericaP1ByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsParametroValorVariacaoGenericaP1), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsParametroValorVariacaoGenericaP1>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsParametroValorVariacaoGenericaP1> GetTcsParametroValorVariacaoGenericaP1()
        {
            return this.Context.GetTcsParametroValorVariacaoGenericaP1ByEntitySearchNoAssociations(null).AsQueryable();
        }
        #endregion
        
    }
    
    public partial class LinxFrameworkParametroControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return true;
        }
    }
}

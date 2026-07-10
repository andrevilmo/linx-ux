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
using BusinessNS = Linx.Framework.BV.Transacao;

namespace Linx.Framework.BV.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/LinxFrameworkTransacao/[ActionName]
    // Security Information Call: http://localhost:1710/LinxFrameworkTransacao/GetSecurityInfo
    // Entities Catalog Call: http://localhost:1710/LinxFrameworkTransacao/GetEntities
    // Entity MetaData Call: http://localhost:1710/LinxFrameworkTransacao/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/LinxFrameworkTransacao/GetClientDomains?erp=true
    // Client Service Call: http://localhost:1710/LinxFrameworkTransacao/GetClientService?erp=true
    // Client Factory Call: http://localhost:1710/LinxFrameworkTransacao/GetClientFactory?entityName=[EntityName]&erp=true
    // Client Factory Custom Events Call: http://localhost:1710/LinxFrameworkTransacao/GetClientFactoryCustomEvents?entityName=[EntityName]&erp=true
    // Help Call: http://localhost:1710/HelpController/LinxFrameworkTransacao
    // Feed OData Call: http://localhost:1710/LinxFrameworkTransacaoOData
    [RoutePrefix("LinxFrameworkTransacao")]
    [Breeze.WebApi2.BreezeController]
    public partial class LinxFrameworkTransacaoController : ApiController
    {
        private const int maxObjectExcelReturned = 2300000;
        private DataServiceRepository<BusinessNS.TransacaoDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.TransacaoDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.TransacaoDomainService>(typeof(BusinessNS.TcsModuloMenuP), typeof(BusinessNS.TcsTransacao), typeof(BusinessNS.TcsTransacaoDependente), typeof(BusinessNS.TcsTransacaoMenu), typeof(BusinessNS.TcsTransacaoMenuChild)); _repository.Context.IsSecure = true; } return _repository; } }
        public LinxFrameworkTransacaoController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(LinxFrameworkTransacaoController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.TransacaoDomainService).Assembly.FullName,
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
            var result = repository.Context.GetMetaDataObject("Linx.Framework.BV.Transacao." + entityName, false, true);
            return (allComposition ? result : result.Where(e => e.ClassName == entityName).ToList());
        }
        
        [Route("GetSecurityInfo"), System.Web.Http.HttpGet()]
        public string[] GetSecurityInfo()
        {
           return new string[] { "Linx.Framework.BV", "LinxFrameworkTransacao", "LinxFrameworkTransacao/ActionName" };
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
        
        [Route("GetAllLookUpTcsTransacaoMenuChildTcsModuloMenu"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsTransacaoMenuChildTcsModuloMenu> GetAllLookUpTcsTransacaoMenuChildTcsModuloMenu()
        {
            return repository.Context.GetAllLookUpTcsTransacaoMenuChildTcsModuloMenu();
        }
        
        [Route("GetLookUpTcsTransacaoMenuChildTcsModuloMenuByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsTransacaoMenuChildTcsModuloMenu> GetLookUpTcsTransacaoMenuChildTcsModuloMenuByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsTransacaoMenuChildTcsModuloMenuByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [Route("GetAllLookUpTcsTransacaoDependente"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsTransacaoDependente> GetAllLookUpTcsTransacaoDependente()
        {
            return repository.Context.GetAllLookUpTcsTransacaoDependente();
        }
        
        [Route("GetLookUpTcsTransacaoDependenteByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsTransacaoDependente> GetLookUpTcsTransacaoDependenteByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsTransacaoDependenteByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [Route("GetAllLookUpTcsObjetoTransacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsObjetoTransacao> GetAllLookUpTcsObjetoTransacao()
        {
            return repository.Context.GetAllLookUpTcsObjetoTransacao();
        }
        
        [Route("GetLookUpTcsObjetoTransacaoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsObjetoTransacao> GetLookUpTcsObjetoTransacaoByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsObjetoTransacaoByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
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
        
        [Route("GetTcsTransacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacao> GetTcsTransacao()
        {
            return repository.Context.GetTcsTransacao();
        }
        
        [Route("GetTcsTransacaoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacao> GetTcsTransacaoNoAssociations()
        {
            return repository.Context.GetTcsTransacaoNoAssociations();
        }
        
        [Route("GetTcsTransacaoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacao> GetTcsTransacaoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsTransacaoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacao), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsTransacaoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacao> GetTcsTransacaoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsTransacaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacao), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsTransacaoToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsTransacaoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsTransacaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTransacao asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Transacao.TcsTransacao");
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
               return ExcelExportPagination<BusinessNS.TcsTransacao>.CreateExcelDocumentFileMapPath("TcsTransacao",new ExcelExportPagination<BusinessNS.TcsTransacao>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsTransacaoToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsTransacaoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsTransacaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Transacao.TcsTransacao", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.TransacaoDataSource", DataSourceObject = "GetTcsTransacao", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsTransacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacao> GetSampleTcsTransacao(string details)
        {
            var result = repository.Context.GetTcsTransacaoByEntitySearchNoAssociations(null).Take(100).ToList();
               if (!details.IsNullOrEmpty())
               {
                   foreach(var entity in result)
                   {
                       entity.FillDetails(repository.Context, null, null, details.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries), 5);
                   }
               }
            return result.AsQueryable();
        }
        
        [Route("AddTcsTransacaoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsTransacaoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacao), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsTransacaoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacao> GetTcsTransacaoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsTransacaoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsTransacaoMenuChild"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoMenuChild> GetTcsTransacaoMenuChild()
        {
            return repository.Context.GetTcsTransacaoMenuChild();
        }
        
        [Route("GetTcsTransacaoMenuChildNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoMenuChild> GetTcsTransacaoMenuChildNoAssociations()
        {
            return repository.Context.GetTcsTransacaoMenuChildNoAssociations();
        }
        
        [Route("GetTcsTransacaoMenuChildByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoMenuChild> GetTcsTransacaoMenuChildByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsTransacaoMenuChildByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoMenuChild), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsTransacaoMenuChildByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoMenuChild> GetTcsTransacaoMenuChildByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsTransacaoMenuChildByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoMenuChild), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsTransacaoMenuChildToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsTransacaoMenuChildToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoMenuChild), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsTransacaoMenuChildByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsTransacaoMenu asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Transacao.TcsTransacaoMenuChild");
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
               return ExcelExportPagination<BusinessNS.TcsTransacaoMenuChild>.CreateExcelDocumentFileMapPath("TcsTransacaoMenuChild",new ExcelExportPagination<BusinessNS.TcsTransacaoMenuChild>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsTransacaoMenuChildToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsTransacaoMenuChildToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoMenuChild), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsTransacaoMenuChildByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Transacao.TcsTransacaoMenuChild", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.TransacaoDataSource", DataSourceObject = "GetTcsTransacaoMenuChild", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsTransacaoMenuChild"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoMenuChild> GetSampleTcsTransacaoMenuChild(string details)
        {
            var result = repository.Context.GetTcsTransacaoMenuChildByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsTransacaoMenuChildEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsTransacaoMenuChildEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoMenuChild), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsTransacaoMenuChildByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoMenuChild> GetTcsTransacaoMenuChildByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsTransacaoMenuChildByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsTransacaoMenu"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoMenu> GetTcsTransacaoMenu()
        {
            return repository.Context.GetTcsTransacaoMenu();
        }
        
        [Route("GetTcsTransacaoMenuNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoMenu> GetTcsTransacaoMenuNoAssociations()
        {
            return repository.Context.GetTcsTransacaoMenuNoAssociations();
        }
        
        [Route("GetTcsTransacaoMenuByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoMenu> GetTcsTransacaoMenuByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsTransacaoMenuByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoMenu), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsTransacaoMenuByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoMenu> GetTcsTransacaoMenuByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsTransacaoMenuByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoMenu), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsTransacaoMenuToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsTransacaoMenuToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoMenu), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsTransacaoMenuByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdModuloMenu asc, IdTransacao asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Transacao.TcsTransacaoMenu");
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
               return ExcelExportPagination<BusinessNS.TcsTransacaoMenu>.CreateExcelDocumentFileMapPath("TcsTransacaoMenu",new ExcelExportPagination<BusinessNS.TcsTransacaoMenu>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsTransacaoMenuToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsTransacaoMenuToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoMenu), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsTransacaoMenuByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Transacao.TcsTransacaoMenu", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.TransacaoDataSource", DataSourceObject = "GetTcsTransacaoMenu", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsTransacaoMenu"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoMenu> GetSampleTcsTransacaoMenu(string details)
        {
            var result = repository.Context.GetTcsTransacaoMenuByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsTransacaoMenuEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsTransacaoMenuEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoMenu), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsTransacaoMenuByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoMenu> GetTcsTransacaoMenuByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsTransacaoMenuByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsTransacaoDependente"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoDependente> GetTcsTransacaoDependente()
        {
            return repository.Context.GetTcsTransacaoDependente();
        }
        
        [Route("GetTcsTransacaoDependenteNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoDependente> GetTcsTransacaoDependenteNoAssociations()
        {
            return repository.Context.GetTcsTransacaoDependenteNoAssociations();
        }
        
        [Route("GetTcsTransacaoDependenteByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoDependente> GetTcsTransacaoDependenteByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsTransacaoDependenteByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoDependente), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsTransacaoDependenteByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoDependente> GetTcsTransacaoDependenteByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsTransacaoDependenteByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoDependente), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsTransacaoDependenteToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsTransacaoDependenteToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoDependente), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsTransacaoDependenteByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTransacaoDependente asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Transacao.TcsTransacaoDependente");
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
               return ExcelExportPagination<BusinessNS.TcsTransacaoDependente>.CreateExcelDocumentFileMapPath("TcsTransacaoDependente",new ExcelExportPagination<BusinessNS.TcsTransacaoDependente>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsTransacaoDependenteToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsTransacaoDependenteToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoDependente), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsTransacaoDependenteByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Transacao.TcsTransacaoDependente", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.TransacaoDataSource", DataSourceObject = "GetTcsTransacaoDependente", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsTransacaoDependente"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoDependente> GetSampleTcsTransacaoDependente(string details)
        {
            var result = repository.Context.GetTcsTransacaoDependenteByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsTransacaoDependenteEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsTransacaoDependenteEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoDependente), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsTransacaoDependenteByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoDependente> GetTcsTransacaoDependenteByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsTransacaoDependenteByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsModuloMenuP"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloMenuP> GetTcsModuloMenuP()
        {
            return repository.Context.GetTcsModuloMenuP();
        }
        
        [Route("GetTcsModuloMenuPNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloMenuP> GetTcsModuloMenuPNoAssociations()
        {
            return repository.Context.GetTcsModuloMenuPNoAssociations();
        }
        
        [Route("GetTcsModuloMenuPByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloMenuP> GetTcsModuloMenuPByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsModuloMenuPByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloMenuP), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsModuloMenuPByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloMenuP> GetTcsModuloMenuPByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsModuloMenuPByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloMenuP), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsModuloMenuPToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsModuloMenuPToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloMenuP), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsModuloMenuPByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdModuloMenu asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Transacao.TcsModuloMenuP");
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
               return ExcelExportPagination<BusinessNS.TcsModuloMenuP>.CreateExcelDocumentFileMapPath("TcsModuloMenuP",new ExcelExportPagination<BusinessNS.TcsModuloMenuP>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsModuloMenuPToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsModuloMenuPToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloMenuP), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsModuloMenuPByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Transacao.TcsModuloMenuP", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.TransacaoDataSource", DataSourceObject = "GetTcsModuloMenuP", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsModuloMenuP"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloMenuP> GetSampleTcsModuloMenuP(string details)
        {
            var result = repository.Context.GetTcsModuloMenuPByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsModuloMenuPEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsModuloMenuPEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloMenuP), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsModuloMenuPByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloMenuP> GetTcsModuloMenuPByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsModuloMenuPByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
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
    
    public partial class LinxFrameworkTransacaoFeedController : ODataController
    {
        private BusinessNS.TransacaoDomainService _context;
        public BusinessNS.TransacaoDomainService Context { get {  if (_context == null) { _context = new BusinessNS.TransacaoDomainService(); _context.IsSecure = true; } return _context; }  }
        
        #region Get Action to Business Entities
        
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacao> GetTcsTransacaoById([FromODataUri]long key0)
        {
            var entity = this.Context.GetTcsTransacaoByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsTransacao[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsTransacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacao> GetTcsTransacaoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsTransacaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacao), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsTransacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacao> GetTcsTransacao()
        {
            return this.Context.GetTcsTransacaoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacaoMenuChild> GetTcsTransacao__TcsTransacaoMenuChild(long key0, string navigation)
        {
            var entity = this.Context.GetTcsTransacaoByKey(key0);
            if (entity != null && navigation == "TcsTransacaoMenuChildList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsTransacaoMenuChild" });
               return entity.TcsTransacaoMenuChildList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsTransacaoMenuChild>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacaoDependente> GetTcsTransacao__TcsTransacaoDependente(long key0, string navigation)
        {
            var entity = this.Context.GetTcsTransacaoByKey(key0);
            if (entity != null && navigation == "TcsTransacaoDependenteList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsTransacaoDependente" });
               return entity.TcsTransacaoDependenteList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsTransacaoDependente>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacaoMenuChild> GetTcsTransacaoMenuChildById([FromODataUri]Int32 key0)
        {
            var entity = this.Context.GetTcsTransacaoMenuChildByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsTransacaoMenuChild[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsTransacaoMenuChild>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacaoMenuChild> GetTcsTransacaoMenuChildByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsTransacaoMenuChildByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoMenuChild), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsTransacaoMenuChild>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacaoMenuChild> GetTcsTransacaoMenuChild()
        {
            return this.Context.GetTcsTransacaoMenuChildByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacao> GetTcsTransacaoMenuChild__TcsTransacao(Int32 key0, string navigation)
        {
            var entity = this.Context.GetTcsTransacaoMenuChildByKey(key0);
            if (entity != null && navigation == "TcsTransacao")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsTransacao[] { entity.TcsTransacao }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsTransacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacaoMenu> GetTcsTransacaoMenuById([FromODataUri]Int64 key0, [FromODataUri]Int64 key1)
        {
            var entity = this.Context.GetTcsTransacaoMenuByKey(key0, key1);
            if (entity != null)
               return (new BusinessNS.TcsTransacaoMenu[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsTransacaoMenu>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacaoMenu> GetTcsTransacaoMenuByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsTransacaoMenuByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoMenu), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsTransacaoMenu>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacaoMenu> GetTcsTransacaoMenu()
        {
            return this.Context.GetTcsTransacaoMenuByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacaoDependente> GetTcsTransacaoDependenteById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetTcsTransacaoDependenteByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsTransacaoDependente[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsTransacaoDependente>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacaoDependente> GetTcsTransacaoDependenteByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsTransacaoDependenteByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoDependente), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsTransacaoDependente>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacaoDependente> GetTcsTransacaoDependente()
        {
            return this.Context.GetTcsTransacaoDependenteByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacao> GetTcsTransacaoDependente__TcsTransacao(Int64 key0, string navigation)
        {
            var entity = this.Context.GetTcsTransacaoDependenteByKey(key0);
            if (entity != null && navigation == "TcsTransacao")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsTransacao[] { entity.TcsTransacao }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsTransacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsModuloMenuP> GetTcsModuloMenuPById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetTcsModuloMenuPByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsModuloMenuP[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsModuloMenuP>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsModuloMenuP> GetTcsModuloMenuPByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsModuloMenuPByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloMenuP), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsModuloMenuP>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsModuloMenuP> GetTcsModuloMenuP()
        {
            return this.Context.GetTcsModuloMenuPByEntitySearchNoAssociations(null).AsQueryable();
        }
        #endregion
        
    }
    
    public partial class LinxFrameworkTransacaoControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return true;
        }
    }
}

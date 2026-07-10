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
using BusinessNS = Linx.Framework.BV.ObjetoAutorizacao;

namespace Linx.Framework.BV.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/LinxFrameworkObjetoAutorizacao/[ActionName]
    // Security Information Call: http://localhost:1710/LinxFrameworkObjetoAutorizacao/GetSecurityInfo
    // Entities Catalog Call: http://localhost:1710/LinxFrameworkObjetoAutorizacao/GetEntities
    // Entity MetaData Call: http://localhost:1710/LinxFrameworkObjetoAutorizacao/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/LinxFrameworkObjetoAutorizacao/GetClientDomains?erp=true
    // Client Service Call: http://localhost:1710/LinxFrameworkObjetoAutorizacao/GetClientService?erp=true
    // Client Factory Call: http://localhost:1710/LinxFrameworkObjetoAutorizacao/GetClientFactory?entityName=[EntityName]&erp=true
    // Client Factory Custom Events Call: http://localhost:1710/LinxFrameworkObjetoAutorizacao/GetClientFactoryCustomEvents?entityName=[EntityName]&erp=true
    // Help Call: http://localhost:1710/HelpController/LinxFrameworkObjetoAutorizacao
    // Feed OData Call: http://localhost:1710/LinxFrameworkObjetoAutorizacaoOData
    [RoutePrefix("LinxFrameworkObjetoAutorizacao")]
    [Breeze.WebApi2.BreezeController]
    public partial class LinxFrameworkObjetoAutorizacaoController : ApiController
    {
        private const int maxObjectExcelReturned = 2300000;
        private DataServiceRepository<BusinessNS.ObjetoAutorizacaoDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.ObjetoAutorizacaoDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.ObjetoAutorizacaoDomainService>(typeof(BusinessNS.TcsObjetoAutorizacao), typeof(BusinessNS.TcsObjetoConteudoAutorizacao), typeof(BusinessNS.TcsTransacaoAutorizacaoChild)); _repository.Context.IsSecure = true; } return _repository; } }
        public LinxFrameworkObjetoAutorizacaoController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(LinxFrameworkObjetoAutorizacaoController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.ObjetoAutorizacaoDomainService).Assembly.FullName,
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
            var result = repository.Context.GetMetaDataObject("Linx.Framework.BV.ObjetoAutorizacao." + entityName, false, true);
            return (allComposition ? result : result.Where(e => e.ClassName == entityName).ToList());
        }
        
        [Route("GetSecurityInfo"), System.Web.Http.HttpGet()]
        public string[] GetSecurityInfo()
        {
           return new string[] { "Linx.Framework.BV", "LinxFrameworkObjetoAutorizacao", "LinxFrameworkObjetoAutorizacao/ActionName" };
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
        
        [Route("GetAllLookUpTcsLayoutAutorizacaoLista"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsLayoutAutorizacaoLista> GetAllLookUpTcsLayoutAutorizacaoLista()
        {
            return repository.Context.GetAllLookUpTcsLayoutAutorizacaoLista();
        }
        
        [Route("GetLookUpTcsLayoutAutorizacaoListaByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsLayoutAutorizacaoLista> GetLookUpTcsLayoutAutorizacaoListaByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsLayoutAutorizacaoListaByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
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
        
        [Route("GetTcsObjetoAutorizacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsObjetoAutorizacao> GetTcsObjetoAutorizacao()
        {
            return repository.Context.GetTcsObjetoAutorizacao();
        }
        
        [Route("GetTcsObjetoAutorizacaoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsObjetoAutorizacao> GetTcsObjetoAutorizacaoNoAssociations()
        {
            return repository.Context.GetTcsObjetoAutorizacaoNoAssociations();
        }
        
        [Route("GetTcsObjetoAutorizacaoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsObjetoAutorizacao> GetTcsObjetoAutorizacaoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsObjetoAutorizacaoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsObjetoAutorizacao), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsObjetoAutorizacaoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsObjetoAutorizacao> GetTcsObjetoAutorizacaoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsObjetoAutorizacaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsObjetoAutorizacao), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsObjetoAutorizacaoToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsObjetoAutorizacaoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsObjetoAutorizacao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsObjetoAutorizacaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdObjeto asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.ObjetoAutorizacao.TcsObjetoAutorizacao");
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
               return ExcelExportPagination<BusinessNS.TcsObjetoAutorizacao>.CreateExcelDocumentFileMapPath("TcsObjetoAutorizacao",new ExcelExportPagination<BusinessNS.TcsObjetoAutorizacao>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsObjetoAutorizacaoToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsObjetoAutorizacaoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsObjetoAutorizacao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsObjetoAutorizacaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.ObjetoAutorizacao.TcsObjetoAutorizacao", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ObjetoAutorizacaoDataSource", DataSourceObject = "GetTcsObjetoAutorizacao", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsObjetoAutorizacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsObjetoAutorizacao> GetSampleTcsObjetoAutorizacao(string details)
        {
            var result = repository.Context.GetTcsObjetoAutorizacaoByEntitySearchNoAssociations(null).Take(100).ToList();
               if (!details.IsNullOrEmpty())
               {
                   foreach(var entity in result)
                   {
                       entity.FillDetails(repository.Context, null, null, details.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries), 5);
                   }
               }
            return result.AsQueryable();
        }
        
        [Route("AddTcsObjetoAutorizacaoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsObjetoAutorizacaoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsObjetoAutorizacao), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsObjetoAutorizacaoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsObjetoAutorizacao> GetTcsObjetoAutorizacaoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsObjetoAutorizacaoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsTransacaoAutorizacaoChild"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoAutorizacaoChild> GetTcsTransacaoAutorizacaoChild()
        {
            return repository.Context.GetTcsTransacaoAutorizacaoChild();
        }
        
        [Route("GetTcsTransacaoAutorizacaoChildNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoAutorizacaoChild> GetTcsTransacaoAutorizacaoChildNoAssociations()
        {
            return repository.Context.GetTcsTransacaoAutorizacaoChildNoAssociations();
        }
        
        [Route("GetTcsTransacaoAutorizacaoChildByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoAutorizacaoChild> GetTcsTransacaoAutorizacaoChildByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsTransacaoAutorizacaoChildByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoAutorizacaoChild), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsTransacaoAutorizacaoChildByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoAutorizacaoChild> GetTcsTransacaoAutorizacaoChildByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsTransacaoAutorizacaoChildByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoAutorizacaoChild), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsTransacaoAutorizacaoChildToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsTransacaoAutorizacaoChildToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoAutorizacaoChild), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsTransacaoAutorizacaoChildByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTransacao asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.ObjetoAutorizacao.TcsTransacaoAutorizacaoChild");
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
               return ExcelExportPagination<BusinessNS.TcsTransacaoAutorizacaoChild>.CreateExcelDocumentFileMapPath("TcsTransacaoAutorizacaoChild",new ExcelExportPagination<BusinessNS.TcsTransacaoAutorizacaoChild>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsTransacaoAutorizacaoChildToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsTransacaoAutorizacaoChildToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoAutorizacaoChild), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsTransacaoAutorizacaoChildByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.ObjetoAutorizacao.TcsTransacaoAutorizacaoChild", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ObjetoAutorizacaoDataSource", DataSourceObject = "GetTcsTransacaoAutorizacaoChild", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsTransacaoAutorizacaoChild"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoAutorizacaoChild> GetSampleTcsTransacaoAutorizacaoChild(string details)
        {
            var result = repository.Context.GetTcsTransacaoAutorizacaoChildByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsTransacaoAutorizacaoChildEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsTransacaoAutorizacaoChildEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoAutorizacaoChild), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsTransacaoAutorizacaoChildByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoAutorizacaoChild> GetTcsTransacaoAutorizacaoChildByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsTransacaoAutorizacaoChildByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsObjetoConteudoAutorizacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsObjetoConteudoAutorizacao> GetTcsObjetoConteudoAutorizacao()
        {
            return repository.Context.GetTcsObjetoConteudoAutorizacao();
        }
        
        [Route("GetTcsObjetoConteudoAutorizacaoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsObjetoConteudoAutorizacao> GetTcsObjetoConteudoAutorizacaoNoAssociations()
        {
            return repository.Context.GetTcsObjetoConteudoAutorizacaoNoAssociations();
        }
        
        [Route("GetTcsObjetoConteudoAutorizacaoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsObjetoConteudoAutorizacao> GetTcsObjetoConteudoAutorizacaoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsObjetoConteudoAutorizacaoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsObjetoConteudoAutorizacao), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsObjetoConteudoAutorizacaoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsObjetoConteudoAutorizacao> GetTcsObjetoConteudoAutorizacaoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsObjetoConteudoAutorizacaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsObjetoConteudoAutorizacao), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsObjetoConteudoAutorizacaoToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsObjetoConteudoAutorizacaoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsObjetoConteudoAutorizacao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsObjetoConteudoAutorizacaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdObjetoConteudo asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.ObjetoAutorizacao.TcsObjetoConteudoAutorizacao");
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
               return ExcelExportPagination<BusinessNS.TcsObjetoConteudoAutorizacao>.CreateExcelDocumentFileMapPath("TcsObjetoConteudoAutorizacao",new ExcelExportPagination<BusinessNS.TcsObjetoConteudoAutorizacao>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsObjetoConteudoAutorizacaoToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsObjetoConteudoAutorizacaoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsObjetoConteudoAutorizacao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsObjetoConteudoAutorizacaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.ObjetoAutorizacao.TcsObjetoConteudoAutorizacao", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ObjetoAutorizacaoDataSource", DataSourceObject = "GetTcsObjetoConteudoAutorizacao", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsObjetoConteudoAutorizacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsObjetoConteudoAutorizacao> GetSampleTcsObjetoConteudoAutorizacao(string details)
        {
            var result = repository.Context.GetTcsObjetoConteudoAutorizacaoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsObjetoConteudoAutorizacaoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsObjetoConteudoAutorizacaoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsObjetoConteudoAutorizacao), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsObjetoConteudoAutorizacaoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsObjetoConteudoAutorizacao> GetTcsObjetoConteudoAutorizacaoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsObjetoConteudoAutorizacaoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        #endregion
        
        #region Get Business Entities By Parent Composition
        
        [Route("GetTcsTransacaoAutorizacaoChildParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoAutorizacaoChildParentComposition> GetTcsTransacaoAutorizacaoChildParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsTransacaoAutorizacaoChildParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoAutorizacaoChildParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsTransacaoAutorizacaoChildParentCompositionToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsTransacaoAutorizacaoChildParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("TcsTransacaoAutorizacaoChild{", "TcsTransacaoAutorizacaoChildParentComposition{");
            jEntitySearch = jEntitySearch.Replace("TcsObjetoAutorizacao{", "TcsTransacaoAutorizacaoChildParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoAutorizacaoChildParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsTransacaoAutorizacaoChildParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTransacao asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.ObjetoAutorizacao.TcsTransacaoAutorizacaoChild");
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
               return ExcelExportPagination<BusinessNS.TcsTransacaoAutorizacaoChildParentComposition>.CreateExcelDocumentFileMapPath("TcsTransacaoAutorizacaoChild",new ExcelExportPagination<BusinessNS.TcsTransacaoAutorizacaoChildParentComposition>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsTransacaoAutorizacaoChildParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsTransacaoAutorizacaoChildParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoAutorizacaoChildParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsTransacaoAutorizacaoChildParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.ObjetoAutorizacao.TcsTransacaoAutorizacaoChild", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ObjetoAutorizacaoDataSource", DataSourceObject = "GetTcsTransacaoAutorizacaoChildParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsTransacaoAutorizacaoChildParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsTransacaoAutorizacaoChildParentComposition> GetSampleTcsTransacaoAutorizacaoChildParentComposition(string details)
        {
            var result = repository.Context.GetTcsTransacaoAutorizacaoChildParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("GetTcsObjetoConteudoAutorizacaoParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsObjetoConteudoAutorizacaoParentComposition> GetTcsObjetoConteudoAutorizacaoParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsObjetoConteudoAutorizacaoParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsObjetoConteudoAutorizacaoParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsObjetoConteudoAutorizacaoParentCompositionToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsObjetoConteudoAutorizacaoParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("TcsObjetoConteudoAutorizacao{", "TcsObjetoConteudoAutorizacaoParentComposition{");
            jEntitySearch = jEntitySearch.Replace("TcsObjetoAutorizacao{", "TcsObjetoConteudoAutorizacaoParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsObjetoConteudoAutorizacaoParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsObjetoConteudoAutorizacaoParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdObjetoConteudo asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.ObjetoAutorizacao.TcsObjetoConteudoAutorizacao");
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
               return ExcelExportPagination<BusinessNS.TcsObjetoConteudoAutorizacaoParentComposition>.CreateExcelDocumentFileMapPath("TcsObjetoConteudoAutorizacao",new ExcelExportPagination<BusinessNS.TcsObjetoConteudoAutorizacaoParentComposition>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsObjetoConteudoAutorizacaoParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsObjetoConteudoAutorizacaoParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsObjetoConteudoAutorizacaoParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsObjetoConteudoAutorizacaoParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.ObjetoAutorizacao.TcsObjetoConteudoAutorizacao", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ObjetoAutorizacaoDataSource", DataSourceObject = "GetTcsObjetoConteudoAutorizacaoParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsObjetoConteudoAutorizacaoParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsObjetoConteudoAutorizacaoParentComposition> GetSampleTcsObjetoConteudoAutorizacaoParentComposition(string details)
        {
            var result = repository.Context.GetTcsObjetoConteudoAutorizacaoParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
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
    
    public partial class LinxFrameworkObjetoAutorizacaoFeedController : ODataController
    {
        private BusinessNS.ObjetoAutorizacaoDomainService _context;
        public BusinessNS.ObjetoAutorizacaoDomainService Context { get {  if (_context == null) { _context = new BusinessNS.ObjetoAutorizacaoDomainService(); _context.IsSecure = true; } return _context; }  }
        
        #region Get Action to Business Entities
        
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsObjetoAutorizacao> GetTcsObjetoAutorizacaoById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetTcsObjetoAutorizacaoByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsObjetoAutorizacao[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsObjetoAutorizacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsObjetoAutorizacao> GetTcsObjetoAutorizacaoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsObjetoAutorizacaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsObjetoAutorizacao), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsObjetoAutorizacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsObjetoAutorizacao> GetTcsObjetoAutorizacao()
        {
            return this.Context.GetTcsObjetoAutorizacaoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacaoAutorizacaoChild> GetTcsObjetoAutorizacao__TcsTransacaoAutorizacaoChild(Int64 key0, string navigation)
        {
            var entity = this.Context.GetTcsObjetoAutorizacaoByKey(key0);
            if (entity != null && navigation == "TcsTransacaoAutorizacaoChildList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsTransacaoAutorizacaoChild" });
               return entity.TcsTransacaoAutorizacaoChildList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsTransacaoAutorizacaoChild>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsObjetoConteudoAutorizacao> GetTcsObjetoAutorizacao__TcsObjetoConteudoAutorizacao(Int64 key0, string navigation)
        {
            var entity = this.Context.GetTcsObjetoAutorizacaoByKey(key0);
            if (entity != null && navigation == "TcsObjetoConteudoAutorizacaoList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsObjetoConteudoAutorizacao" });
               return entity.TcsObjetoConteudoAutorizacaoList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsObjetoConteudoAutorizacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacaoAutorizacaoChild> GetTcsTransacaoAutorizacaoChildById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetTcsTransacaoAutorizacaoChildByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsTransacaoAutorizacaoChild[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsTransacaoAutorizacaoChild>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacaoAutorizacaoChild> GetTcsTransacaoAutorizacaoChildByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsTransacaoAutorizacaoChildByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoAutorizacaoChild), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsTransacaoAutorizacaoChild>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacaoAutorizacaoChild> GetTcsTransacaoAutorizacaoChild()
        {
            return this.Context.GetTcsTransacaoAutorizacaoChildByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacaoAutorizacaoChildParentComposition> GetTcsTransacaoAutorizacaoChildParentComposition()
        {
            return this.Context.GetTcsTransacaoAutorizacaoChildParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacaoAutorizacaoChildParentComposition> GetTcsTransacaoAutorizacaoChildParentCompositionByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                jEntitySearch = jEntitySearch.Replace("TcsTransacaoAutorizacaoChild{", "TcsTransacaoAutorizacaoChildParentComposition{");
                jEntitySearch = jEntitySearch.Replace("TcsObjetoAutorizacao{", "TcsTransacaoAutorizacaoChildParentComposition{");
                var entity = this.Context.GetTcsTransacaoAutorizacaoChildParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsTransacaoAutorizacaoChildParentComposition), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsTransacaoAutorizacaoChildParentComposition>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsObjetoAutorizacao> GetTcsTransacaoAutorizacaoChild__TcsObjetoAutorizacao(Int64 key0, string navigation)
        {
            var entity = this.Context.GetTcsTransacaoAutorizacaoChildByKey(key0);
            if (entity != null && navigation == "TcsObjetoAutorizacao")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsObjetoAutorizacao[] { entity.TcsObjetoAutorizacao }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsObjetoAutorizacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsObjetoConteudoAutorizacao> GetTcsObjetoConteudoAutorizacaoById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetTcsObjetoConteudoAutorizacaoByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsObjetoConteudoAutorizacao[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsObjetoConteudoAutorizacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsObjetoConteudoAutorizacao> GetTcsObjetoConteudoAutorizacaoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsObjetoConteudoAutorizacaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsObjetoConteudoAutorizacao), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsObjetoConteudoAutorizacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsObjetoConteudoAutorizacao> GetTcsObjetoConteudoAutorizacao()
        {
            return this.Context.GetTcsObjetoConteudoAutorizacaoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsObjetoConteudoAutorizacaoParentComposition> GetTcsObjetoConteudoAutorizacaoParentComposition()
        {
            return this.Context.GetTcsObjetoConteudoAutorizacaoParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsObjetoConteudoAutorizacaoParentComposition> GetTcsObjetoConteudoAutorizacaoParentCompositionByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                jEntitySearch = jEntitySearch.Replace("TcsObjetoConteudoAutorizacao{", "TcsObjetoConteudoAutorizacaoParentComposition{");
                jEntitySearch = jEntitySearch.Replace("TcsObjetoAutorizacao{", "TcsObjetoConteudoAutorizacaoParentComposition{");
                var entity = this.Context.GetTcsObjetoConteudoAutorizacaoParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsObjetoConteudoAutorizacaoParentComposition), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsObjetoConteudoAutorizacaoParentComposition>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsObjetoAutorizacao> GetTcsObjetoConteudoAutorizacao__TcsObjetoAutorizacao(Int64 key0, string navigation)
        {
            var entity = this.Context.GetTcsObjetoConteudoAutorizacaoByKey(key0);
            if (entity != null && navigation == "TcsObjetoAutorizacao")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsObjetoAutorizacao[] { entity.TcsObjetoAutorizacao }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsObjetoAutorizacao>);
        }
        #endregion
        
    }
    
    public partial class LinxFrameworkObjetoAutorizacaoControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return true;
        }
    }
}

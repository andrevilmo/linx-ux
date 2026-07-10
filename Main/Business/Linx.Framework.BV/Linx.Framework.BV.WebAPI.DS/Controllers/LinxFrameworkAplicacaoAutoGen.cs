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
using BusinessNS = Linx.Framework.BV.Aplicacao;

namespace Linx.Framework.BV.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/LinxFrameworkAplicacao/[ActionName]
    // Security Information Call: http://localhost:1710/LinxFrameworkAplicacao/GetSecurityInfo
    // Entities Catalog Call: http://localhost:1710/LinxFrameworkAplicacao/GetEntities
    // Entity MetaData Call: http://localhost:1710/LinxFrameworkAplicacao/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/LinxFrameworkAplicacao/GetClientDomains?erp=true
    // Client Service Call: http://localhost:1710/LinxFrameworkAplicacao/GetClientService?erp=true
    // Client Factory Call: http://localhost:1710/LinxFrameworkAplicacao/GetClientFactory?entityName=[EntityName]&erp=true
    // Client Factory Custom Events Call: http://localhost:1710/LinxFrameworkAplicacao/GetClientFactoryCustomEvents?entityName=[EntityName]&erp=true
    // Help Call: http://localhost:1710/HelpController/LinxFrameworkAplicacao
    // Feed OData Call: http://localhost:1710/LinxFrameworkAplicacaoOData
    [RoutePrefix("LinxFrameworkAplicacao")]
    [Breeze.WebApi2.BreezeController]
    public partial class LinxFrameworkAplicacaoController : ApiController
    {
        private const int maxObjectExcelReturned = 2300000;
        private DataServiceRepository<BusinessNS.AplicacaoDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.AplicacaoDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.AplicacaoDomainService>(typeof(BusinessNS.TcsAmbiente), typeof(BusinessNS.TcsAplicacao), typeof(BusinessNS.TcsAplicacaoVersaoHistorico)); _repository.Context.IsSecure = true; } return _repository; } }
        public LinxFrameworkAplicacaoController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(LinxFrameworkAplicacaoController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.AplicacaoDomainService).Assembly.FullName,
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
            var result = repository.Context.GetMetaDataObject("Linx.Framework.BV.Aplicacao." + entityName, false, true);
            return (allComposition ? result : result.Where(e => e.ClassName == entityName).ToList());
        }
        
        [Route("GetSecurityInfo"), System.Web.Http.HttpGet()]
        public string[] GetSecurityInfo()
        {
           return new string[] { "Linx.Framework.BV", "LinxFrameworkAplicacao", "LinxFrameworkAplicacao/ActionName" };
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
        
        [Route("GetAllLookUpTcsEmpresaAutenticacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsEmpresaAutenticacao> GetAllLookUpTcsEmpresaAutenticacao()
        {
            return repository.Context.GetAllLookUpTcsEmpresaAutenticacao();
        }
        
        [Route("GetLookUpTcsEmpresaAutenticacaoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsEmpresaAutenticacao> GetLookUpTcsEmpresaAutenticacaoByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsEmpresaAutenticacaoByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
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
        
        [Route("GetTcsAplicacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAplicacao> GetTcsAplicacao()
        {
            return repository.Context.GetTcsAplicacao();
        }
        
        [Route("GetTcsAplicacaoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAplicacao> GetTcsAplicacaoNoAssociations()
        {
            return repository.Context.GetTcsAplicacaoNoAssociations();
        }
        
        [Route("GetTcsAplicacaoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAplicacao> GetTcsAplicacaoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsAplicacaoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAplicacao), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsAplicacaoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAplicacao> GetTcsAplicacaoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsAplicacaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAplicacao), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsAplicacaoToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsAplicacaoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAplicacao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsAplicacaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdAplicacao asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Aplicacao.TcsAplicacao");
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
               return ExcelExportPagination<BusinessNS.TcsAplicacao>.CreateExcelDocumentFileMapPath("TcsAplicacao",new ExcelExportPagination<BusinessNS.TcsAplicacao>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsAplicacaoToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsAplicacaoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAplicacao), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsAplicacaoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Aplicacao.TcsAplicacao", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.AplicacaoDataSource", DataSourceObject = "GetTcsAplicacao", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsAplicacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAplicacao> GetSampleTcsAplicacao(string details)
        {
            var result = repository.Context.GetTcsAplicacaoByEntitySearchNoAssociations(null).Take(100).ToList();
               if (!details.IsNullOrEmpty())
               {
                   foreach(var entity in result)
                   {
                       entity.FillDetails(repository.Context, null, null, details.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries), 5);
                   }
               }
            return result.AsQueryable();
        }
        
        [Route("AddTcsAplicacaoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsAplicacaoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAplicacao), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsAplicacaoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAplicacao> GetTcsAplicacaoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsAplicacaoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsAplicacaoVersaoHistorico"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAplicacaoVersaoHistorico> GetTcsAplicacaoVersaoHistorico()
        {
            return repository.Context.GetTcsAplicacaoVersaoHistorico();
        }
        
        [Route("GetTcsAplicacaoVersaoHistoricoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAplicacaoVersaoHistorico> GetTcsAplicacaoVersaoHistoricoNoAssociations()
        {
            return repository.Context.GetTcsAplicacaoVersaoHistoricoNoAssociations();
        }
        
        [Route("GetTcsAplicacaoVersaoHistoricoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAplicacaoVersaoHistorico> GetTcsAplicacaoVersaoHistoricoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsAplicacaoVersaoHistoricoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAplicacaoVersaoHistorico), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsAplicacaoVersaoHistoricoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAplicacaoVersaoHistorico> GetTcsAplicacaoVersaoHistoricoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsAplicacaoVersaoHistoricoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAplicacaoVersaoHistorico), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsAplicacaoVersaoHistoricoToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsAplicacaoVersaoHistoricoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAplicacaoVersaoHistorico), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsAplicacaoVersaoHistoricoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsAplicacaoVersaoHistorico asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Aplicacao.TcsAplicacaoVersaoHistorico");
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
               return ExcelExportPagination<BusinessNS.TcsAplicacaoVersaoHistorico>.CreateExcelDocumentFileMapPath("TcsAplicacaoVersaoHistorico",new ExcelExportPagination<BusinessNS.TcsAplicacaoVersaoHistorico>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsAplicacaoVersaoHistoricoToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsAplicacaoVersaoHistoricoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAplicacaoVersaoHistorico), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsAplicacaoVersaoHistoricoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Aplicacao.TcsAplicacaoVersaoHistorico", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.AplicacaoDataSource", DataSourceObject = "GetTcsAplicacaoVersaoHistorico", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsAplicacaoVersaoHistorico"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAplicacaoVersaoHistorico> GetSampleTcsAplicacaoVersaoHistorico(string details)
        {
            var result = repository.Context.GetTcsAplicacaoVersaoHistoricoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsAplicacaoVersaoHistoricoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsAplicacaoVersaoHistoricoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAplicacaoVersaoHistorico), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsAplicacaoVersaoHistoricoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAplicacaoVersaoHistorico> GetTcsAplicacaoVersaoHistoricoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsAplicacaoVersaoHistoricoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Aplicacao.TcsAmbiente");
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Aplicacao.TcsAmbiente", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.AplicacaoDataSource", DataSourceObject = "GetTcsAmbiente", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsAmbiente"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbiente> GetSampleTcsAmbiente(string details)
        {
            var result = repository.Context.GetTcsAmbienteByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsAmbienteEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsAmbienteEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbiente), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsAmbienteByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbiente> GetTcsAmbienteByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsAmbienteByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        #endregion
        
        #region Get Business Entities By Parent Composition
        
        [Route("GetTcsAplicacaoVersaoHistoricoParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAplicacaoVersaoHistoricoParentComposition> GetTcsAplicacaoVersaoHistoricoParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsAplicacaoVersaoHistoricoParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAplicacaoVersaoHistoricoParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsAplicacaoVersaoHistoricoParentCompositionToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsAplicacaoVersaoHistoricoParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("TcsAplicacaoVersaoHistorico{", "TcsAplicacaoVersaoHistoricoParentComposition{");
            jEntitySearch = jEntitySearch.Replace("TcsAplicacao{", "TcsAplicacaoVersaoHistoricoParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAplicacaoVersaoHistoricoParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsAplicacaoVersaoHistoricoParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsAplicacaoVersaoHistorico asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Aplicacao.TcsAplicacaoVersaoHistorico");
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
               return ExcelExportPagination<BusinessNS.TcsAplicacaoVersaoHistoricoParentComposition>.CreateExcelDocumentFileMapPath("TcsAplicacaoVersaoHistorico",new ExcelExportPagination<BusinessNS.TcsAplicacaoVersaoHistoricoParentComposition>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsAplicacaoVersaoHistoricoParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsAplicacaoVersaoHistoricoParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAplicacaoVersaoHistoricoParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsAplicacaoVersaoHistoricoParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Aplicacao.TcsAplicacaoVersaoHistorico", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.AplicacaoDataSource", DataSourceObject = "GetTcsAplicacaoVersaoHistoricoParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsAplicacaoVersaoHistoricoParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAplicacaoVersaoHistoricoParentComposition> GetSampleTcsAplicacaoVersaoHistoricoParentComposition(string details)
        {
            var result = repository.Context.GetTcsAplicacaoVersaoHistoricoParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("GetTcsAmbienteParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbienteParentComposition> GetTcsAmbienteParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsAmbienteParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsAmbienteParentCompositionToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsAmbienteParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("TcsAmbiente{", "TcsAmbienteParentComposition{");
            jEntitySearch = jEntitySearch.Replace("TcsAplicacao{", "TcsAmbienteParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsAmbienteParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsAmbiente asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Aplicacao.TcsAmbiente");
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
               return ExcelExportPagination<BusinessNS.TcsAmbienteParentComposition>.CreateExcelDocumentFileMapPath("TcsAmbiente",new ExcelExportPagination<BusinessNS.TcsAmbienteParentComposition>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsAmbienteParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsAmbienteParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsAmbienteParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Aplicacao.TcsAmbiente", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.AplicacaoDataSource", DataSourceObject = "GetTcsAmbienteParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsAmbienteParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsAmbienteParentComposition> GetSampleTcsAmbienteParentComposition(string details)
        {
            var result = repository.Context.GetTcsAmbienteParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
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
    
    public partial class LinxFrameworkAplicacaoFeedController : ODataController
    {
        private BusinessNS.AplicacaoDomainService _context;
        public BusinessNS.AplicacaoDomainService Context { get {  if (_context == null) { _context = new BusinessNS.AplicacaoDomainService(); _context.IsSecure = true; } return _context; }  }
        
        #region Get Action to Business Entities
        
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsAplicacao> GetTcsAplicacaoById([FromODataUri]Int32 key0)
        {
            var entity = this.Context.GetTcsAplicacaoByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsAplicacao[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsAplicacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsAplicacao> GetTcsAplicacaoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsAplicacaoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAplicacao), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsAplicacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsAplicacao> GetTcsAplicacao()
        {
            return this.Context.GetTcsAplicacaoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsAplicacaoVersaoHistorico> GetTcsAplicacao__TcsAplicacaoVersaoHistorico(Int32 key0, string navigation)
        {
            var entity = this.Context.GetTcsAplicacaoByKey(key0);
            if (entity != null && navigation == "TcsAplicacaoVersaoHistoricoList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsAplicacaoVersaoHistorico" });
               return entity.TcsAplicacaoVersaoHistoricoList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsAplicacaoVersaoHistorico>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsAmbiente> GetTcsAplicacao__TcsAmbiente(Int32 key0, string navigation)
        {
            var entity = this.Context.GetTcsAplicacaoByKey(key0);
            if (entity != null && navigation == "TcsAmbienteList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsAmbiente" });
               return entity.TcsAmbienteList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsAmbiente>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsAplicacaoVersaoHistorico> GetTcsAplicacaoVersaoHistoricoById([FromODataUri]Int32 key0)
        {
            var entity = this.Context.GetTcsAplicacaoVersaoHistoricoByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsAplicacaoVersaoHistorico[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsAplicacaoVersaoHistorico>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsAplicacaoVersaoHistorico> GetTcsAplicacaoVersaoHistoricoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsAplicacaoVersaoHistoricoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAplicacaoVersaoHistorico), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsAplicacaoVersaoHistorico>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsAplicacaoVersaoHistorico> GetTcsAplicacaoVersaoHistorico()
        {
            return this.Context.GetTcsAplicacaoVersaoHistoricoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsAplicacaoVersaoHistoricoParentComposition> GetTcsAplicacaoVersaoHistoricoParentComposition()
        {
            return this.Context.GetTcsAplicacaoVersaoHistoricoParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsAplicacaoVersaoHistoricoParentComposition> GetTcsAplicacaoVersaoHistoricoParentCompositionByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                jEntitySearch = jEntitySearch.Replace("TcsAplicacaoVersaoHistorico{", "TcsAplicacaoVersaoHistoricoParentComposition{");
                jEntitySearch = jEntitySearch.Replace("TcsAplicacao{", "TcsAplicacaoVersaoHistoricoParentComposition{");
                var entity = this.Context.GetTcsAplicacaoVersaoHistoricoParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAplicacaoVersaoHistoricoParentComposition), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsAplicacaoVersaoHistoricoParentComposition>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsAplicacao> GetTcsAplicacaoVersaoHistorico__TcsAplicacao(Int32 key0, string navigation)
        {
            var entity = this.Context.GetTcsAplicacaoVersaoHistoricoByKey(key0);
            if (entity != null && navigation == "TcsAplicacao")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsAplicacao[] { entity.TcsAplicacao }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsAplicacao>);
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
        public IQueryable<BusinessNS.TcsAmbienteParentComposition> GetTcsAmbienteParentComposition()
        {
            return this.Context.GetTcsAmbienteParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsAmbienteParentComposition> GetTcsAmbienteParentCompositionByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                jEntitySearch = jEntitySearch.Replace("TcsAmbiente{", "TcsAmbienteParentComposition{");
                jEntitySearch = jEntitySearch.Replace("TcsAplicacao{", "TcsAmbienteParentComposition{");
                var entity = this.Context.GetTcsAmbienteParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsAmbienteParentComposition), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsAmbienteParentComposition>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsAplicacao> GetTcsAmbiente__TcsAplicacao(Int32 key0, string navigation)
        {
            var entity = this.Context.GetTcsAmbienteByKey(key0);
            if (entity != null && navigation == "TcsAplicacao")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsAplicacao[] { entity.TcsAplicacao }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsAplicacao>);
        }
        #endregion
        
    }
    
    public partial class LinxFrameworkAplicacaoControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return true;
        }
    }
}

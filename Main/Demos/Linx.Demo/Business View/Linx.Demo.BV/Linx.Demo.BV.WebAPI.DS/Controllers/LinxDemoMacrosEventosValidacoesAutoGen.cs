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
using BusinessNS = Linx.Demo.BV.MacrosEventosValidacoes;

namespace Linx.Demo.BV.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/LinxDemoMacrosEventosValidacoes/[ActionName]
    // Security Information Call: http://localhost:1710/LinxDemoMacrosEventosValidacoes/GetSecurityInfo
    // Entities Catalog Call: http://localhost:1710/LinxDemoMacrosEventosValidacoes/GetEntities
    // Entity MetaData Call: http://localhost:1710/LinxDemoMacrosEventosValidacoes/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/LinxDemoMacrosEventosValidacoes/GetClientDomains?erp=true
    // Client Service Call: http://localhost:1710/LinxDemoMacrosEventosValidacoes/GetClientService?erp=true
    // Client Factory Call: http://localhost:1710/LinxDemoMacrosEventosValidacoes/GetClientFactory?entityName=[EntityName]&erp=true
    // Client Factory Custom Events Call: http://localhost:1710/LinxDemoMacrosEventosValidacoes/GetClientFactoryCustomEvents?entityName=[EntityName]&erp=true
    // Help Call: http://localhost:1710/HelpController/LinxDemoMacrosEventosValidacoes
    // Feed OData Call: http://localhost:1710/LinxDemoMacrosEventosValidacoesOData
    [RoutePrefix("LinxDemoMacrosEventosValidacoes")]
    [Breeze.WebApi2.BreezeController]
    [ODataBasicAuthenticationFilter]
    public partial class LinxDemoMacrosEventosValidacoesController : ApiController
    {
        private const int maxObjectExcelReturned = 2300000;
        private DataServiceRepository<BusinessNS.MacrosEventosValidacoesDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.MacrosEventosValidacoesDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.MacrosEventosValidacoesDomainService>(typeof(BusinessNS.Arquivo), typeof(BusinessNS.Estado), typeof(BusinessNS.Pais), typeof(BusinessNS.ValorVendas)); _repository.Context.IsSecure = true; } return _repository; } }
        public LinxDemoMacrosEventosValidacoesController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(LinxDemoMacrosEventosValidacoesController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.MacrosEventosValidacoesDomainService).Assembly.FullName,
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
            var result = repository.Context.GetMetaDataObject("Linx.Demo.BV.MacrosEventosValidacoes." + entityName, false, true);
            return (allComposition ? result : result.Where(e => e.ClassName == entityName).ToList());
        }
        
        [Route("GetSecurityInfo"), System.Web.Http.HttpGet()]
        public string[] GetSecurityInfo()
        {
           return new string[] { "Linx.Demo.BV", "LinxDemoMacrosEventosValidacoes", "LinxDemoMacrosEventosValidacoes/ActionName" };
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
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetReportDataSource"), System.Web.Http.HttpGet()]
        public string GetReportDataSource()
        {
            var zip = new LinxZip();
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetDomainsInfo"), System.Web.Http.HttpGet()]
        public string[] GetDomainsInfo(string domainNames)
        {
            return Linx.Demo.BV.Domains.DomainHelper.GetDomainsInfo(domainNames);
        }
        
        [Route("GetDomainValues"), System.Web.Http.HttpGet()]
        public Dictionary<string, string> GetDomainValues(string domainName)
        {
            return Linx.Demo.BV.Domains.DomainHelper.GetDomainValues(domainName);
        }
        
        #region Get LookUps
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [Route("GetAllLookUpEntityAdapter1Cliente"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpEntityAdapter1Cliente> GetAllLookUpEntityAdapter1Cliente()
        {
            return repository.Context.GetAllLookUpEntityAdapter1Cliente();
        }
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [Route("GetLookUpEntityAdapter1ClienteByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpEntityAdapter1Cliente> GetLookUpEntityAdapter1ClienteByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpEntityAdapter1ClienteByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [Route("GetAllLookUpEntityAdapter1CodLoja"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpEntityAdapter1CodLoja> GetAllLookUpEntityAdapter1CodLoja()
        {
            return repository.Context.GetAllLookUpEntityAdapter1CodLoja();
        }
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [Route("GetLookUpEntityAdapter1CodLojaByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpEntityAdapter1CodLoja> GetLookUpEntityAdapter1CodLojaByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpEntityAdapter1CodLojaByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [Route("GetAllLookUpEntityAdapter1Data"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpEntityAdapter1Data> GetAllLookUpEntityAdapter1Data()
        {
            return repository.Context.GetAllLookUpEntityAdapter1Data();
        }
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [Route("GetLookUpEntityAdapter1DataByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpEntityAdapter1Data> GetLookUpEntityAdapter1DataByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpEntityAdapter1DataByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [Route("GetAllLookUpEntityAdapter1IdBandeiraRede"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpEntityAdapter1IdBandeiraRede> GetAllLookUpEntityAdapter1IdBandeiraRede()
        {
            return repository.Context.GetAllLookUpEntityAdapter1IdBandeiraRede();
        }
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [Route("GetLookUpEntityAdapter1IdBandeiraRedeByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpEntityAdapter1IdBandeiraRede> GetLookUpEntityAdapter1IdBandeiraRedeByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpEntityAdapter1IdBandeiraRedeByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [Route("GetAllLookUpEntityAdapter1Loja"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpEntityAdapter1Loja> GetAllLookUpEntityAdapter1Loja()
        {
            return repository.Context.GetAllLookUpEntityAdapter1Loja();
        }
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [Route("GetLookUpEntityAdapter1LojaByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpEntityAdapter1Loja> GetLookUpEntityAdapter1LojaByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpEntityAdapter1LojaByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
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
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [Route("GetArquivo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Arquivo> GetArquivo()
        {
            return repository.Context.GetArquivo().AsQueryable();
        }
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [Route("GetArquivoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Arquivo> GetArquivoNoAssociations()
        {
            return repository.Context.GetArquivoNoAssociations().AsQueryable();
        }
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [Route("GetArquivoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Arquivo> GetArquivoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetArquivoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Arquivo), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [Route("GetArquivoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Arquivo> GetArquivoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetArquivoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Arquivo), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        [Route("GetArquivoToExcel"), System.Web.Http.HttpPost()]
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        public string GetArquivoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Arquivo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetArquivoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).AsQueryable().OrderBy("NomeArquivo asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.MacrosEventosValidacoes.Arquivo");
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
               return ExcelExportPagination<BusinessNS.Arquivo>.CreateExcelDocumentFileMapPath("Arquivo",new ExcelExportPagination<BusinessNS.Arquivo>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetArquivoToReportXml"), System.Web.Http.HttpPost()]
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        public string GetArquivoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Arquivo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetArquivoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.MacrosEventosValidacoes.Arquivo", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Demo.BV.Reports", DataSourceFullName = "Linx.Demo.BV.Reports.MacrosEventosValidacoesDataSource", DataSourceObject = "GetArquivo", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [Route("GetSampleArquivo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Arquivo> GetSampleArquivo(string details)
        {
            var result = repository.Context.GetArquivoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [Route("GetPais"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Pais> GetPais()
        {
            return repository.Context.GetPais();
        }
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [Route("GetPaisNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Pais> GetPaisNoAssociations()
        {
            return repository.Context.GetPaisNoAssociations();
        }
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [Route("GetPaisByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Pais> GetPaisByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetPaisByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Pais), jEntitySearch, false, true, false), jEntitySearch);
        }
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [Route("GetPaisByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Pais> GetPaisByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetPaisByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Pais), jEntitySearch, false, true, false), jEntitySearch);
        }
        [Route("GetPaisToExcel"), System.Web.Http.HttpPost()]
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        public string GetPaisToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Pais), jEntitySearch, false, true, false);
            var entities = repository.Context.GetPaisByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdPais asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.MacrosEventosValidacoes.Pais");
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
               return ExcelExportPagination<BusinessNS.Pais>.CreateExcelDocumentFileMapPath("Pais",new ExcelExportPagination<BusinessNS.Pais>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetPaisToReportXml"), System.Web.Http.HttpPost()]
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        public string GetPaisToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Pais), jEntitySearch, false, true, false);
            var entities = repository.Context.GetPaisByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.MacrosEventosValidacoes.Pais", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Demo.BV.Reports", DataSourceFullName = "Linx.Demo.BV.Reports.MacrosEventosValidacoesDataSource", DataSourceObject = "GetPais", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [Route("GetSamplePais"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Pais> GetSamplePais(string details)
        {
            var result = repository.Context.GetPaisByEntitySearchNoAssociations(null).Take(100).ToList();
               if (!details.IsNullOrEmpty())
               {
                   foreach(var entity in result)
                   {
                       entity.FillDetails(repository.Context, null, null, details.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries), 5);
                   }
               }
            return result.AsQueryable();
        }
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [Route("GetEstado"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Estado> GetEstado()
        {
            return repository.Context.GetEstado();
        }
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [Route("GetEstadoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Estado> GetEstadoNoAssociations()
        {
            return repository.Context.GetEstadoNoAssociations();
        }
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [Route("GetEstadoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Estado> GetEstadoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetEstadoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Estado), jEntitySearch, false, true, false), jEntitySearch);
        }
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [Route("GetEstadoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Estado> GetEstadoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetEstadoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Estado), jEntitySearch, false, true, false), jEntitySearch);
        }
        [Route("GetEstadoToExcel"), System.Web.Http.HttpPost()]
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        public string GetEstadoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Estado), jEntitySearch, false, true, false);
            var entities = repository.Context.GetEstadoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdEstado asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.MacrosEventosValidacoes.Estado");
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
               return ExcelExportPagination<BusinessNS.Estado>.CreateExcelDocumentFileMapPath("Estado",new ExcelExportPagination<BusinessNS.Estado>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetEstadoToReportXml"), System.Web.Http.HttpPost()]
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        public string GetEstadoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Estado), jEntitySearch, false, true, false);
            var entities = repository.Context.GetEstadoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.MacrosEventosValidacoes.Estado", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Demo.BV.Reports", DataSourceFullName = "Linx.Demo.BV.Reports.MacrosEventosValidacoesDataSource", DataSourceObject = "GetEstado", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [Route("GetSampleEstado"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Estado> GetSampleEstado(string details)
        {
            var result = repository.Context.GetEstadoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [Route("GetValorVendas"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.ValorVendas> GetValorVendas()
        {
            return repository.Context.GetValorVendas().AsQueryable();
        }
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [Route("GetValorVendasNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.ValorVendas> GetValorVendasNoAssociations()
        {
            return repository.Context.GetValorVendasNoAssociations().AsQueryable();
        }
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [Route("GetValorVendasByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.ValorVendas> GetValorVendasByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetValorVendasByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.ValorVendas), jEntitySearch, false, false, true), jEntitySearch).AsQueryable();
        }
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [Route("GetValorVendasByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.ValorVendas> GetValorVendasByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetValorVendasByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.ValorVendas), jEntitySearch, false, false, true), jEntitySearch).AsQueryable();
        }
        [Route("GetValorVendasToExcel"), System.Web.Http.HttpPost()]
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        public string GetValorVendasToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch += "LinqValidProperties{LinqValidProperties#==#S" + string.Join(",", columns.Keys) + "}";
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.ValorVendas), jEntitySearch, false, false, true);
            var entities = repository.Context.GetValorVendasByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).AsQueryable().OrderBy("IdBandeiraRede asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.MacrosEventosValidacoes.ValorVendas");
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
               return ExcelExportPagination<BusinessNS.ValorVendas>.CreateExcelDocumentFileMapPath("ValorVendas",new ExcelExportPagination<BusinessNS.ValorVendas>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetValorVendasToReportXml"), System.Web.Http.HttpPost()]
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        public string GetValorVendasToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch += "LinqValidProperties{LinqValidProperties#==#S" + string.Join(",", columns.Keys) + "}";
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.ValorVendas), jEntitySearch, false, false, true);
            var entities = repository.Context.GetValorVendasByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.MacrosEventosValidacoes.ValorVendas", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Demo.BV.Reports", DataSourceFullName = "Linx.Demo.BV.Reports.MacrosEventosValidacoesDataSource", DataSourceObject = "GetValorVendas", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [Route("GetSampleValorVendas"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.ValorVendas> GetSampleValorVendas(string details)
        {
            var result = repository.Context.GetValorVendasByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        #endregion
        
        #region Get Business Entities By Parent Composition
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [Route("GetEstadoParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.EstadoParentComposition> GetEstadoParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetEstadoParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.EstadoParentComposition), jEntitySearch, false, true, false), jEntitySearch);
        }
        [Route("GetEstadoParentCompositionToExcel"), System.Web.Http.HttpPost()]
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        public string GetEstadoParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("Estado{", "EstadoParentComposition{");
            jEntitySearch = jEntitySearch.Replace("Pais{", "EstadoParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.EstadoParentComposition), jEntitySearch, false, true, false);
            var entities = repository.Context.GetEstadoParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdEstado asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.MacrosEventosValidacoes.Estado");
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
               return ExcelExportPagination<BusinessNS.EstadoParentComposition>.CreateExcelDocumentFileMapPath("Estado",new ExcelExportPagination<BusinessNS.EstadoParentComposition>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetEstadoParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        public string GetEstadoParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.EstadoParentComposition), jEntitySearch, false, true, false);
            var entities = repository.Context.GetEstadoParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Demo.BV.MacrosEventosValidacoes.Estado", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Demo.BV.Reports", DataSourceFullName = "Linx.Demo.BV.Reports.MacrosEventosValidacoesDataSource", DataSourceObject = "GetEstadoParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Demo.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [Route("GetSampleEstadoParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.EstadoParentComposition> GetSampleEstadoParentComposition(string details)
        {
            var result = repository.Context.GetEstadoParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        #endregion
        
        #region Save Changes
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
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
    public partial class LinxDemoMacrosEventosValidacoesFeedController : ODataController
    {
        private BusinessNS.MacrosEventosValidacoesDomainService _context;
        public BusinessNS.MacrosEventosValidacoesDomainService Context { get {  if (_context == null) { _context = new BusinessNS.MacrosEventosValidacoesDomainService(); _context.IsSecure = true; } return _context; }  }
        
        #region Get Action to Business Entities
        
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Arquivo> GetArquivoById([FromODataUri]string key0)
        {
            var entity = this.Context.GetArquivoByKey(key0);
            if (entity != null)
               return (new BusinessNS.Arquivo[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.Arquivo>);
        }
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Arquivo> GetArquivoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetArquivoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Arquivo), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.Arquivo>);
        }
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Arquivo> GetArquivo()
        {
            return this.Context.GetArquivoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Pais> GetPaisById([FromODataUri]int key0)
        {
            var entity = this.Context.GetPaisByKey(key0);
            if (entity != null)
               return (new BusinessNS.Pais[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.Pais>);
        }
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Pais> GetPaisByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetPaisByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Pais), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.Pais>);
        }
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Pais> GetPais()
        {
            return this.Context.GetPaisByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Estado> GetPais__Estado(int key0, string navigation)
        {
            var entity = this.Context.GetPaisByKey(key0);
            if (entity != null && navigation == "EstadoList")
            {
               entity.FillDetails(_context, null, null, new string[] { "Estado" });
               return entity.EstadoList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.Estado>);
        }
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Estado> GetEstadoById([FromODataUri]int key0)
        {
            var entity = this.Context.GetEstadoByKey(key0);
            if (entity != null)
               return (new BusinessNS.Estado[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.Estado>);
        }
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Estado> GetEstadoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetEstadoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Estado), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.Estado>);
        }
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Estado> GetEstado()
        {
            return this.Context.GetEstadoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.EstadoParentComposition> GetEstadoParentComposition()
        {
            return this.Context.GetEstadoParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.EstadoParentComposition> GetEstadoParentCompositionByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                jEntitySearch = jEntitySearch.Replace("Estado{", "EstadoParentComposition{");
                jEntitySearch = jEntitySearch.Replace("Pais{", "EstadoParentComposition{");
                var entity = this.Context.GetEstadoParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.EstadoParentComposition), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.EstadoParentComposition>);
        }
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Pais> GetEstado__Pais(int key0, string navigation)
        {
            var entity = this.Context.GetEstadoByKey(key0);
            if (entity != null && navigation == "Pais")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.Pais[] { entity.Pais }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.Pais>);
        }
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.ValorVendas> GetValorVendasById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetValorVendasByKey(key0);
            if (entity != null)
               return (new BusinessNS.ValorVendas[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.ValorVendas>);
        }
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.ValorVendas> GetValorVendasByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetValorVendasByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.ValorVendas), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.ValorVendas>);
        }
        
        [LinxDemoMacrosEventosValidacoesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.ValorVendas> GetValorVendas()
        {
            return this.Context.GetValorVendasByEntitySearchNoAssociations(null).AsQueryable();
        }
        #endregion
        
    }
    
    public partial class LinxDemoMacrosEventosValidacoesControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return LinxAutorization.CheckAuthorization(actionContext, string.Format("{0}#{1}#{1}/{2}", "Linx.Demo.BV", "LinxDemoMacrosEventosValidacoes", actionContext.ActionDescriptor.ActionName));
        }
    }
}

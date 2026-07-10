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
using System.ServiceModel.DomainServices.Server;
using Linx.DataService;
using Linx.Business.Tools;
using System.ComponentModel.Composition;
using System.Web.Http;
using Breeze.ContextProvider;
using Breeze.WebApi2;
using System.Web.Http.OData;
using BusinessNS = VAREJO.BV.MestreDetalheSubDetalhes;

namespace VAREJO.BV.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/VarejoMestreDetalheSubDetalhes/[ActionName]
    // Security Information Call: http://localhost:1710/VarejoMestreDetalheSubDetalhes/GetSecurityInfo
    // Entities Catalog Call: http://localhost:1710/VarejoMestreDetalheSubDetalhes/GetEntities
    // Entity MetaData Call: http://localhost:1710/VarejoMestreDetalheSubDetalhes/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/VarejoMestreDetalheSubDetalhes/GetClientDomains?erp=true
    // Client Service Call: http://localhost:1710/VarejoMestreDetalheSubDetalhes/GetClientService?erp=true
    // Client Factory Call: http://localhost:1710/VarejoMestreDetalheSubDetalhes/GetClientFactory?entityName=[EntityName]&erp=true
    // Client Factory Custom Events Call: http://localhost:1710/VarejoMestreDetalheSubDetalhes/GetClientFactoryCustomEvents?entityName=[EntityName]&erp=true
    // Help Call: http://localhost:1710/HelpController/VarejoMestreDetalheSubDetalhes
    // Feed OData Call: http://localhost:1710/VarejoMestreDetalheSubDetalhesOData
    [RoutePrefix("VarejoMestreDetalheSubDetalhes")]
    [Breeze.WebApi2.BreezeController]
    [ODataBasicAuthenticationFilter]
    public partial class VarejoMestreDetalheSubDetalhesController : ApiController
    {
        private const int maxObjectExcelReturned = 2300000;
        private DataServiceRepository<BusinessNS.MestreDetalheSubDetalhesDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.MestreDetalheSubDetalhesDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.MestreDetalheSubDetalhesDomainService>(typeof(BusinessNS.Cliente), typeof(BusinessNS.ClienteWizard), typeof(BusinessNS.Venda), typeof(BusinessNS.VendaAtacado), typeof(BusinessNS.VendaAtacadoWizard), typeof(BusinessNS.VendaItem), typeof(BusinessNS.VendaItemWizard), typeof(BusinessNS.VendaWizard)); _repository.Context.IsSecure = true; } return _repository; } }
        public VarejoMestreDetalheSubDetalhesController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(VarejoMestreDetalheSubDetalhesController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.MestreDetalheSubDetalhesDomainService).Assembly.FullName,
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
            var result = repository.Context.GetMetaDataObject("VAREJO.BV.MestreDetalheSubDetalhes." + entityName, false, true);
            return (allComposition ? result : result.Where(e => e.ClassName == entityName).ToList());
        }
        
        [Route("GetSecurityInfo"), System.Web.Http.HttpGet()]
        public string[] GetSecurityInfo()
        {
           return new string[] { "VAREJO.BV", "VarejoMestreDetalheSubDetalhes", "VarejoMestreDetalheSubDetalhes/ActionName" };
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
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/VAREJO.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/VAREJO.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetReportDataSource"), System.Web.Http.HttpGet()]
        public string GetReportDataSource()
        {
            var zip = new LinxZip();
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/VAREJO.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/VAREJO.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetDomainsInfo"), System.Web.Http.HttpGet()]
        public string[] GetDomainsInfo(string domainNames)
        {
            return VAREJO.BV.Domains.DomainHelper.GetDomainsInfo(domainNames);
        }
        
        [Route("GetDomainValues"), System.Web.Http.HttpGet()]
        public Dictionary<string, string> GetDomainValues(string domainName)
        {
            return VAREJO.BV.Domains.DomainHelper.GetDomainValues(domainName);
        }
        
        #region Get LookUps
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetAllLookUpLoja"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpLoja> GetAllLookUpLoja()
        {
            return repository.Context.GetAllLookUpLoja();
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetLookUpLojaByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpLoja> GetLookUpLojaByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpLojaByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetAllLookUpEstado"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpEstado> GetAllLookUpEstado()
        {
            return repository.Context.GetAllLookUpEstado();
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetLookUpEstadoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpEstado> GetLookUpEstadoByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpEstadoByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
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
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetCliente"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Cliente> GetCliente()
        {
            return repository.Context.GetCliente();
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetClienteNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Cliente> GetClienteNoAssociations()
        {
            return repository.Context.GetClienteNoAssociations();
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetClienteByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Cliente> GetClienteByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetClienteByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Cliente), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetClienteQuickSearch"), System.Web.Http.HttpGet()]
        public IQueryable<object> GetClienteQuickSearch(string q = "", int page = 1, string jExpr = "", string propertiesSelection = "")
        {
            string validProperties = "StringCliente";
            if (!propertiesSelection.IsNullOrEmpty())
                validProperties = propertiesSelection.Replace(" ", "");
            var validPropertiesList = validProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var whereProperties = new string[] { "StringCliente" };
            var whereCondition = String.Join("||#", whereProperties.Where(f => validPropertiesList.Contains(f)).Select(e => e + "#Like#S" + q + "%;"));
            var jExpression = "Cliente{(;" + whereCondition + ")" + (jExpr.IsNullOrEmpty() ? "" : ";&&;" + jExpr) + "}LinqValidProperties{LinqValidProperties#==#S" + validProperties + "}";
            return (
                               from r in this.GetClienteByEntitySearchNoAssociations(jExpression)
                               select new { StringCliente = r.StringCliente }
                              ).Distinct().OrderBy(e => new { e.StringCliente }).Take(10).Skip((page - 1) * 10);
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetClienteByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Cliente> GetClienteByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetClienteByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Cliente), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetClienteToExcel"), System.Web.Http.HttpPost()]
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        public string GetClienteToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Cliente), jEntitySearch, false, false, false);
            var entities = repository.Context.GetClienteByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdCliente asc");
            var metadata = repository.Context.GetMetaDataObject("VAREJO.BV.MestreDetalheSubDetalhes.Cliente");
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
               return ExcelExportPagination<BusinessNS.Cliente>.CreateExcelDocumentFileMapPath("Cliente",new ExcelExportPagination<BusinessNS.Cliente>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        [Route("GetClienteToReportXml"), System.Web.Http.HttpPost()]
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        public string GetClienteToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Cliente), jEntitySearch, false, false, false);
            var entities = repository.Context.GetClienteByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("VAREJO.BV.MestreDetalheSubDetalhes.Cliente", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "VAREJO.BV.Reports", DataSourceFullName = "VAREJO.BV.Reports.MestreDetalheSubDetalhesDataSource", DataSourceObject = "GetCliente", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/VAREJO.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/VAREJO.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetSampleCliente"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Cliente> GetSampleCliente(string details)
        {
            var result = repository.Context.GetClienteByEntitySearchNoAssociations(null).Take(100).ToList();
               if (!details.IsNullOrEmpty())
               {
                   foreach(var entity in result)
                   {
                       entity.FillDetails(repository.Context, null, null, details.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries), 5);
                   }
               }
            return result.AsQueryable();
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetVenda"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Venda> GetVenda()
        {
            return repository.Context.GetVenda();
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetVendaNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Venda> GetVendaNoAssociations()
        {
            return repository.Context.GetVendaNoAssociations();
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetVendaByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Venda> GetVendaByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetVendaByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Venda), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetVendaByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Venda> GetVendaByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetVendaByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Venda), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetVendaToExcel"), System.Web.Http.HttpPost()]
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        public string GetVendaToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Venda), jEntitySearch, false, false, false);
            var entities = repository.Context.GetVendaByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdVenda asc");
            var metadata = repository.Context.GetMetaDataObject("VAREJO.BV.MestreDetalheSubDetalhes.Venda");
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
               return ExcelExportPagination<BusinessNS.Venda>.CreateExcelDocumentFileMapPath("Venda",new ExcelExportPagination<BusinessNS.Venda>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        [Route("GetVendaToReportXml"), System.Web.Http.HttpPost()]
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        public string GetVendaToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Venda), jEntitySearch, false, false, false);
            var entities = repository.Context.GetVendaByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("VAREJO.BV.MestreDetalheSubDetalhes.Venda", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "VAREJO.BV.Reports", DataSourceFullName = "VAREJO.BV.Reports.MestreDetalheSubDetalhesDataSource", DataSourceObject = "GetVenda", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/VAREJO.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/VAREJO.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetSampleVenda"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Venda> GetSampleVenda(string details)
        {
            var result = repository.Context.GetVendaByEntitySearchNoAssociations(null).Take(100).ToList();
               if (!details.IsNullOrEmpty())
               {
                   foreach(var entity in result)
                   {
                       entity.FillDetails(repository.Context, null, null, details.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries), 5);
                   }
               }
            return result.AsQueryable();
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetVendaAtacado"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaAtacado> GetVendaAtacado()
        {
            return repository.Context.GetVendaAtacado();
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetVendaAtacadoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaAtacado> GetVendaAtacadoNoAssociations()
        {
            return repository.Context.GetVendaAtacadoNoAssociations();
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetVendaAtacadoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaAtacado> GetVendaAtacadoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetVendaAtacadoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaAtacado), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetVendaAtacadoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaAtacado> GetVendaAtacadoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetVendaAtacadoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaAtacado), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetVendaAtacadoToExcel"), System.Web.Http.HttpPost()]
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        public string GetVendaAtacadoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaAtacado), jEntitySearch, false, false, false);
            var entities = repository.Context.GetVendaAtacadoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdVendaAtacado asc");
            var metadata = repository.Context.GetMetaDataObject("VAREJO.BV.MestreDetalheSubDetalhes.VendaAtacado");
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
               return ExcelExportPagination<BusinessNS.VendaAtacado>.CreateExcelDocumentFileMapPath("VendaAtacado",new ExcelExportPagination<BusinessNS.VendaAtacado>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        [Route("GetVendaAtacadoToReportXml"), System.Web.Http.HttpPost()]
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        public string GetVendaAtacadoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaAtacado), jEntitySearch, false, false, false);
            var entities = repository.Context.GetVendaAtacadoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("VAREJO.BV.MestreDetalheSubDetalhes.VendaAtacado", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "VAREJO.BV.Reports", DataSourceFullName = "VAREJO.BV.Reports.MestreDetalheSubDetalhesDataSource", DataSourceObject = "GetVendaAtacado", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/VAREJO.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/VAREJO.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetSampleVendaAtacado"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaAtacado> GetSampleVendaAtacado(string details)
        {
            var result = repository.Context.GetVendaAtacadoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetVendaItem"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaItem> GetVendaItem()
        {
            return repository.Context.GetVendaItem();
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetVendaItemNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaItem> GetVendaItemNoAssociations()
        {
            return repository.Context.GetVendaItemNoAssociations();
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetVendaItemByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaItem> GetVendaItemByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetVendaItemByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaItem), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetVendaItemByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaItem> GetVendaItemByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetVendaItemByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaItem), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetVendaItemToExcel"), System.Web.Http.HttpPost()]
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        public string GetVendaItemToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaItem), jEntitySearch, false, false, false);
            var entities = repository.Context.GetVendaItemByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdVendaItem asc");
            var metadata = repository.Context.GetMetaDataObject("VAREJO.BV.MestreDetalheSubDetalhes.VendaItem");
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
               return ExcelExportPagination<BusinessNS.VendaItem>.CreateExcelDocumentFileMapPath("VendaItem",new ExcelExportPagination<BusinessNS.VendaItem>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        [Route("GetVendaItemToReportXml"), System.Web.Http.HttpPost()]
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        public string GetVendaItemToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaItem), jEntitySearch, false, false, false);
            var entities = repository.Context.GetVendaItemByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("VAREJO.BV.MestreDetalheSubDetalhes.VendaItem", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "VAREJO.BV.Reports", DataSourceFullName = "VAREJO.BV.Reports.MestreDetalheSubDetalhesDataSource", DataSourceObject = "GetVendaItem", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/VAREJO.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/VAREJO.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetSampleVendaItem"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaItem> GetSampleVendaItem(string details)
        {
            var result = repository.Context.GetVendaItemByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetClienteWizard"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.ClienteWizard> GetClienteWizard()
        {
            return repository.Context.GetClienteWizard();
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetClienteWizardNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.ClienteWizard> GetClienteWizardNoAssociations()
        {
            return repository.Context.GetClienteWizardNoAssociations();
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetClienteWizardByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.ClienteWizard> GetClienteWizardByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetClienteWizardByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.ClienteWizard), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetClienteWizardByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.ClienteWizard> GetClienteWizardByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetClienteWizardByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.ClienteWizard), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetClienteWizardToExcel"), System.Web.Http.HttpPost()]
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        public string GetClienteWizardToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.ClienteWizard), jEntitySearch, false, false, false);
            var entities = repository.Context.GetClienteWizardByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdCliente asc");
            var metadata = repository.Context.GetMetaDataObject("VAREJO.BV.MestreDetalheSubDetalhes.ClienteWizard");
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
               return ExcelExportPagination<BusinessNS.ClienteWizard>.CreateExcelDocumentFileMapPath("ClienteWizard",new ExcelExportPagination<BusinessNS.ClienteWizard>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        [Route("GetClienteWizardToReportXml"), System.Web.Http.HttpPost()]
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        public string GetClienteWizardToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.ClienteWizard), jEntitySearch, false, false, false);
            var entities = repository.Context.GetClienteWizardByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("VAREJO.BV.MestreDetalheSubDetalhes.ClienteWizard", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "VAREJO.BV.Reports", DataSourceFullName = "VAREJO.BV.Reports.MestreDetalheSubDetalhesDataSource", DataSourceObject = "GetClienteWizard", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/VAREJO.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/VAREJO.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetSampleClienteWizard"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.ClienteWizard> GetSampleClienteWizard(string details)
        {
            var result = repository.Context.GetClienteWizardByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetVendaWizard"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaWizard> GetVendaWizard()
        {
            return repository.Context.GetVendaWizard();
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetVendaWizardNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaWizard> GetVendaWizardNoAssociations()
        {
            return repository.Context.GetVendaWizardNoAssociations();
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetVendaWizardByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaWizard> GetVendaWizardByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetVendaWizardByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaWizard), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetVendaWizardByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaWizard> GetVendaWizardByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetVendaWizardByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaWizard), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetVendaWizardToExcel"), System.Web.Http.HttpPost()]
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        public string GetVendaWizardToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaWizard), jEntitySearch, false, false, false);
            var entities = repository.Context.GetVendaWizardByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdVenda asc");
            var metadata = repository.Context.GetMetaDataObject("VAREJO.BV.MestreDetalheSubDetalhes.VendaWizard");
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
               return ExcelExportPagination<BusinessNS.VendaWizard>.CreateExcelDocumentFileMapPath("VendaWizard",new ExcelExportPagination<BusinessNS.VendaWizard>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        [Route("GetVendaWizardToReportXml"), System.Web.Http.HttpPost()]
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        public string GetVendaWizardToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaWizard), jEntitySearch, false, false, false);
            var entities = repository.Context.GetVendaWizardByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("VAREJO.BV.MestreDetalheSubDetalhes.VendaWizard", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "VAREJO.BV.Reports", DataSourceFullName = "VAREJO.BV.Reports.MestreDetalheSubDetalhesDataSource", DataSourceObject = "GetVendaWizard", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/VAREJO.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/VAREJO.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetSampleVendaWizard"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaWizard> GetSampleVendaWizard(string details)
        {
            var result = repository.Context.GetVendaWizardByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetVendaItemWizard"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaItemWizard> GetVendaItemWizard()
        {
            return repository.Context.GetVendaItemWizard();
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetVendaItemWizardNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaItemWizard> GetVendaItemWizardNoAssociations()
        {
            return repository.Context.GetVendaItemWizardNoAssociations();
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetVendaItemWizardByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaItemWizard> GetVendaItemWizardByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetVendaItemWizardByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaItemWizard), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetVendaItemWizardByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaItemWizard> GetVendaItemWizardByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetVendaItemWizardByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaItemWizard), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetVendaItemWizardToExcel"), System.Web.Http.HttpPost()]
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        public string GetVendaItemWizardToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaItemWizard), jEntitySearch, false, false, false);
            var entities = repository.Context.GetVendaItemWizardByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdVendaItem asc");
            var metadata = repository.Context.GetMetaDataObject("VAREJO.BV.MestreDetalheSubDetalhes.VendaItemWizard");
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
               return ExcelExportPagination<BusinessNS.VendaItemWizard>.CreateExcelDocumentFileMapPath("VendaItemWizard",new ExcelExportPagination<BusinessNS.VendaItemWizard>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        [Route("GetVendaItemWizardToReportXml"), System.Web.Http.HttpPost()]
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        public string GetVendaItemWizardToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaItemWizard), jEntitySearch, false, false, false);
            var entities = repository.Context.GetVendaItemWizardByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("VAREJO.BV.MestreDetalheSubDetalhes.VendaItemWizard", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "VAREJO.BV.Reports", DataSourceFullName = "VAREJO.BV.Reports.MestreDetalheSubDetalhesDataSource", DataSourceObject = "GetVendaItemWizard", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/VAREJO.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/VAREJO.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetSampleVendaItemWizard"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaItemWizard> GetSampleVendaItemWizard(string details)
        {
            var result = repository.Context.GetVendaItemWizardByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetVendaAtacadoWizard"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaAtacadoWizard> GetVendaAtacadoWizard()
        {
            return repository.Context.GetVendaAtacadoWizard();
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetVendaAtacadoWizardNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaAtacadoWizard> GetVendaAtacadoWizardNoAssociations()
        {
            return repository.Context.GetVendaAtacadoWizardNoAssociations();
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetVendaAtacadoWizardByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaAtacadoWizard> GetVendaAtacadoWizardByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetVendaAtacadoWizardByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaAtacadoWizard), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetVendaAtacadoWizardByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaAtacadoWizard> GetVendaAtacadoWizardByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetVendaAtacadoWizardByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaAtacadoWizard), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetVendaAtacadoWizardToExcel"), System.Web.Http.HttpPost()]
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        public string GetVendaAtacadoWizardToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaAtacadoWizard), jEntitySearch, false, false, false);
            var entities = repository.Context.GetVendaAtacadoWizardByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdVendaAtacado asc");
            var metadata = repository.Context.GetMetaDataObject("VAREJO.BV.MestreDetalheSubDetalhes.VendaAtacadoWizard");
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
               return ExcelExportPagination<BusinessNS.VendaAtacadoWizard>.CreateExcelDocumentFileMapPath("VendaAtacadoWizard",new ExcelExportPagination<BusinessNS.VendaAtacadoWizard>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        [Route("GetVendaAtacadoWizardToReportXml"), System.Web.Http.HttpPost()]
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        public string GetVendaAtacadoWizardToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaAtacadoWizard), jEntitySearch, false, false, false);
            var entities = repository.Context.GetVendaAtacadoWizardByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("VAREJO.BV.MestreDetalheSubDetalhes.VendaAtacadoWizard", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "VAREJO.BV.Reports", DataSourceFullName = "VAREJO.BV.Reports.MestreDetalheSubDetalhesDataSource", DataSourceObject = "GetVendaAtacadoWizard", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/VAREJO.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/VAREJO.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetSampleVendaAtacadoWizard"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaAtacadoWizard> GetSampleVendaAtacadoWizard(string details)
        {
            var result = repository.Context.GetVendaAtacadoWizardByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        #endregion
        
        #region Get Business Entities By Parent Composition
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetVendaParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaParentComposition> GetVendaParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetVendaParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetVendaParentCompositionToExcel"), System.Web.Http.HttpPost()]
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        public string GetVendaParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("Venda{", "VendaParentComposition{");
            jEntitySearch = jEntitySearch.Replace("Cliente{", "VendaParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetVendaParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdVenda asc");
            var metadata = repository.Context.GetMetaDataObject("VAREJO.BV.MestreDetalheSubDetalhes.Venda");
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
               return ExcelExportPagination<BusinessNS.VendaParentComposition>.CreateExcelDocumentFileMapPath("Venda",new ExcelExportPagination<BusinessNS.VendaParentComposition>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        [Route("GetVendaParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        public string GetVendaParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetVendaParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("VAREJO.BV.MestreDetalheSubDetalhes.Venda", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "VAREJO.BV.Reports", DataSourceFullName = "VAREJO.BV.Reports.MestreDetalheSubDetalhesDataSource", DataSourceObject = "GetVendaParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/VAREJO.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/VAREJO.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetSampleVendaParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaParentComposition> GetSampleVendaParentComposition(string details)
        {
            var result = repository.Context.GetVendaParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetVendaAtacadoParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaAtacadoParentComposition> GetVendaAtacadoParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetVendaAtacadoParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaAtacadoParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetVendaAtacadoParentCompositionToExcel"), System.Web.Http.HttpPost()]
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        public string GetVendaAtacadoParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("VendaAtacado{", "VendaAtacadoParentComposition{");
            jEntitySearch = jEntitySearch.Replace("Cliente{", "VendaAtacadoParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaAtacadoParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetVendaAtacadoParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdVendaAtacado asc");
            var metadata = repository.Context.GetMetaDataObject("VAREJO.BV.MestreDetalheSubDetalhes.VendaAtacado");
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
               return ExcelExportPagination<BusinessNS.VendaAtacadoParentComposition>.CreateExcelDocumentFileMapPath("VendaAtacado",new ExcelExportPagination<BusinessNS.VendaAtacadoParentComposition>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        [Route("GetVendaAtacadoParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        public string GetVendaAtacadoParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaAtacadoParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetVendaAtacadoParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("VAREJO.BV.MestreDetalheSubDetalhes.VendaAtacado", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "VAREJO.BV.Reports", DataSourceFullName = "VAREJO.BV.Reports.MestreDetalheSubDetalhesDataSource", DataSourceObject = "GetVendaAtacadoParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/VAREJO.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/VAREJO.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetSampleVendaAtacadoParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaAtacadoParentComposition> GetSampleVendaAtacadoParentComposition(string details)
        {
            var result = repository.Context.GetVendaAtacadoParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetVendaItemParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaItemParentComposition> GetVendaItemParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetVendaItemParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaItemParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetVendaItemParentCompositionToExcel"), System.Web.Http.HttpPost()]
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        public string GetVendaItemParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("VendaItem{", "VendaItemParentComposition{");
            jEntitySearch = jEntitySearch.Replace("Venda{", "VendaItemParentComposition{");
            jEntitySearch = jEntitySearch.Replace("Cliente{", "VendaItemParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaItemParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetVendaItemParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdVendaItem asc");
            var metadata = repository.Context.GetMetaDataObject("VAREJO.BV.MestreDetalheSubDetalhes.VendaItem");
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
               return ExcelExportPagination<BusinessNS.VendaItemParentComposition>.CreateExcelDocumentFileMapPath("VendaItem",new ExcelExportPagination<BusinessNS.VendaItemParentComposition>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        [Route("GetVendaItemParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        public string GetVendaItemParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaItemParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetVendaItemParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("VAREJO.BV.MestreDetalheSubDetalhes.VendaItem", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "VAREJO.BV.Reports", DataSourceFullName = "VAREJO.BV.Reports.MestreDetalheSubDetalhesDataSource", DataSourceObject = "GetVendaItemParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/VAREJO.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/VAREJO.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("GetSampleVendaItemParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaItemParentComposition> GetSampleVendaItemParentComposition(string details)
        {
            var result = repository.Context.GetVendaItemParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        #endregion
        
        #region Save Changes
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("SaveChanges"), System.Web.Http.HttpPost()]
        public SaveResult SaveChanges(JObject saveBundle)
        {
            var result = repository.SaveChanges(saveBundle);
            repository.Context.Dispose();
            return result;
        }
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("SaveCliente"), System.Web.Http.HttpPost()]
        public List<BusinessNS.Cliente> SaveCliente(List<BusinessNS.Cliente> dataList)
        {
            if (dataList != null && dataList.Count > 0)
            {
                List<ChangeSetEntry> changeSetEntries = new List<ChangeSetEntry>();
                foreach (var data in dataList.Where(e => e.ChangeState.InList("I", "U", "D")).ToArray())
                {
                   if (data.ChangeState == "D") data.ResetDetails();
                   foreach (var entity in data.GetFlatEntities())
                   {
                       string state = entity.GetPropertyValue("ChangeState") as string;
                       if (state.InList("I", "U", "D"))
                       {
                           var changeOP = (state == "I" ? DomainOperation.Insert : (state == "D" ? DomainOperation.Delete :  DomainOperation.Update));
                           changeSetEntries.Add(new ChangeSetEntry(changeSetEntries.Count, entity, null, changeOP) { HasMemberChanges = (changeOP == DomainOperation.Update) });
                       }
                   }
                   if (data.ChangeState != "D") data.ResetDetails();
                }
                repository.Context.SaveEntities(changeSetEntries, false);
            }
            repository.Context.Dispose();
            //Set return with nochanges
            var result = dataList.Where(e => e.ChangeState.InList("I", "U", "N")).ToList();
            foreach (var data in result.ToArray())
            {
                   if (data.ChangeState == "N") data.ResetDetails();
                   else data.ResetChangeState();
            }
            return result;
        }
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("SaveClienteInCache"), System.Web.Http.HttpPost()]
        public void SaveClienteInCache(SaveInformation<BusinessNS.Cliente> saveInfo)
        {
            saveInfo.Validate();
            QueueTransaction.SaveTransaction(saveInfo, System.Reflection.Assembly.GetExecutingAssembly().FullName, this.ControllerContext, "SaveCliente");
        }
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        public List<BusinessNS.Cliente> SaveCliente__ForMEF(string jsonString, string viewMapInfo, List<ChangeTracker> changes)
        {
            List<BusinessNS.Cliente> dataList = SerializationManager<List<BusinessNS.Cliente>>.JsonToObject(jsonString);
            if (!viewMapInfo.IsNullOrEmpty() && changes.Count > 0)
            {
                var viewMap = ViewMapHelper.Parse(viewMapInfo);
                if(changes.Any(c => c.ComponentName == viewMap.ParentUIView))
                    dataList = viewMap.ReplaceEntities(dataList, changes.First(c => c.ComponentName == viewMap.ParentUIView).ListReturnedObjects);
            }
            return SaveCliente(dataList);
        }
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("SaveClienteWizard"), System.Web.Http.HttpPost()]
        public List<BusinessNS.ClienteWizard> SaveClienteWizard(List<BusinessNS.ClienteWizard> dataList)
        {
            if (dataList != null && dataList.Count > 0)
            {
                List<ChangeSetEntry> changeSetEntries = new List<ChangeSetEntry>();
                foreach (var data in dataList.Where(e => e.ChangeState.InList("I", "U", "D")).ToArray())
                {
                   if (data.ChangeState == "D") data.ResetDetails();
                   foreach (var entity in data.GetFlatEntities())
                   {
                       string state = entity.GetPropertyValue("ChangeState") as string;
                       if (state.InList("I", "U", "D"))
                       {
                           var changeOP = (state == "I" ? DomainOperation.Insert : (state == "D" ? DomainOperation.Delete :  DomainOperation.Update));
                           changeSetEntries.Add(new ChangeSetEntry(changeSetEntries.Count, entity, null, changeOP) { HasMemberChanges = (changeOP == DomainOperation.Update) });
                       }
                   }
                   if (data.ChangeState != "D") data.ResetDetails();
                }
                repository.Context.SaveEntities(changeSetEntries, false);
            }
            repository.Context.Dispose();
            //Set return with nochanges
            var result = dataList.Where(e => e.ChangeState.InList("I", "U", "N")).ToList();
            foreach (var data in result.ToArray())
            {
                   if (data.ChangeState == "N") data.ResetDetails();
                   else data.ResetChangeState();
            }
            return result;
        }
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("SaveClienteWizardInCache"), System.Web.Http.HttpPost()]
        public void SaveClienteWizardInCache(SaveInformation<BusinessNS.ClienteWizard> saveInfo)
        {
            saveInfo.Validate();
            QueueTransaction.SaveTransaction(saveInfo, System.Reflection.Assembly.GetExecutingAssembly().FullName, this.ControllerContext, "SaveClienteWizard");
        }
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        public List<BusinessNS.ClienteWizard> SaveClienteWizard__ForMEF(string jsonString, string viewMapInfo, List<ChangeTracker> changes)
        {
            List<BusinessNS.ClienteWizard> dataList = SerializationManager<List<BusinessNS.ClienteWizard>>.JsonToObject(jsonString);
            if (!viewMapInfo.IsNullOrEmpty() && changes.Count > 0)
            {
                var viewMap = ViewMapHelper.Parse(viewMapInfo);
                if(changes.Any(c => c.ComponentName == viewMap.ParentUIView))
                    dataList = viewMap.ReplaceEntities(dataList, changes.First(c => c.ComponentName == viewMap.ParentUIView).ListReturnedObjects);
            }
            return SaveClienteWizard(dataList);
        }
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("SaveVendaWizard"), System.Web.Http.HttpPost()]
        public List<BusinessNS.VendaWizard> SaveVendaWizard(List<BusinessNS.VendaWizard> dataList)
        {
            if (dataList != null && dataList.Count > 0)
            {
                List<ChangeSetEntry> changeSetEntries = new List<ChangeSetEntry>();
                foreach (var data in dataList.Where(e => e.ChangeState.InList("I", "U", "D")).ToArray())
                {
                   if (data.ChangeState == "D") data.ResetDetails();
                   foreach (var entity in data.GetFlatEntities())
                   {
                       string state = entity.GetPropertyValue("ChangeState") as string;
                       if (state.InList("I", "U", "D"))
                       {
                           var changeOP = (state == "I" ? DomainOperation.Insert : (state == "D" ? DomainOperation.Delete :  DomainOperation.Update));
                           changeSetEntries.Add(new ChangeSetEntry(changeSetEntries.Count, entity, null, changeOP) { HasMemberChanges = (changeOP == DomainOperation.Update) });
                       }
                   }
                   if (data.ChangeState != "D") data.ResetDetails();
                }
                repository.Context.SaveEntities(changeSetEntries, false);
            }
            repository.Context.Dispose();
            //Set return with nochanges
            var result = dataList.Where(e => e.ChangeState.InList("I", "U", "N")).ToList();
            foreach (var data in result.ToArray())
            {
                   if (data.ChangeState == "N") data.ResetDetails();
                   else data.ResetChangeState();
            }
            return result;
        }
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("SaveVendaWizardInCache"), System.Web.Http.HttpPost()]
        public void SaveVendaWizardInCache(SaveInformation<BusinessNS.VendaWizard> saveInfo)
        {
            saveInfo.Validate();
            QueueTransaction.SaveTransaction(saveInfo, System.Reflection.Assembly.GetExecutingAssembly().FullName, this.ControllerContext, "SaveVendaWizard");
        }
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        public List<BusinessNS.VendaWizard> SaveVendaWizard__ForMEF(string jsonString, string viewMapInfo, List<ChangeTracker> changes)
        {
            List<BusinessNS.VendaWizard> dataList = SerializationManager<List<BusinessNS.VendaWizard>>.JsonToObject(jsonString);
            if (!viewMapInfo.IsNullOrEmpty() && changes.Count > 0)
            {
                var viewMap = ViewMapHelper.Parse(viewMapInfo);
                if(changes.Any(c => c.ComponentName == viewMap.ParentUIView))
                    dataList = viewMap.ReplaceEntities(dataList, changes.First(c => c.ComponentName == viewMap.ParentUIView).ListReturnedObjects);
            }
            return SaveVendaWizard(dataList);
        }
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("SaveVendaItemWizard"), System.Web.Http.HttpPost()]
        public List<BusinessNS.VendaItemWizard> SaveVendaItemWizard(List<BusinessNS.VendaItemWizard> dataList)
        {
            if (dataList != null && dataList.Count > 0)
            {
                List<ChangeSetEntry> changeSetEntries = new List<ChangeSetEntry>();
                foreach (var data in dataList.Where(e => e.ChangeState.InList("I", "U", "D")).ToArray())
                {
                   if (data.ChangeState == "D") data.ResetDetails();
                   foreach (var entity in data.GetFlatEntities())
                   {
                       string state = entity.GetPropertyValue("ChangeState") as string;
                       if (state.InList("I", "U", "D"))
                       {
                           var changeOP = (state == "I" ? DomainOperation.Insert : (state == "D" ? DomainOperation.Delete :  DomainOperation.Update));
                           changeSetEntries.Add(new ChangeSetEntry(changeSetEntries.Count, entity, null, changeOP) { HasMemberChanges = (changeOP == DomainOperation.Update) });
                       }
                   }
                   if (data.ChangeState != "D") data.ResetDetails();
                }
                repository.Context.SaveEntities(changeSetEntries, false);
            }
            repository.Context.Dispose();
            //Set return with nochanges
            var result = dataList.Where(e => e.ChangeState.InList("I", "U", "N")).ToList();
            foreach (var data in result.ToArray())
            {
                   if (data.ChangeState == "N") data.ResetDetails();
                   else data.ResetChangeState();
            }
            return result;
        }
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("SaveVendaItemWizardInCache"), System.Web.Http.HttpPost()]
        public void SaveVendaItemWizardInCache(SaveInformation<BusinessNS.VendaItemWizard> saveInfo)
        {
            saveInfo.Validate();
            QueueTransaction.SaveTransaction(saveInfo, System.Reflection.Assembly.GetExecutingAssembly().FullName, this.ControllerContext, "SaveVendaItemWizard");
        }
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        public List<BusinessNS.VendaItemWizard> SaveVendaItemWizard__ForMEF(string jsonString, string viewMapInfo, List<ChangeTracker> changes)
        {
            List<BusinessNS.VendaItemWizard> dataList = SerializationManager<List<BusinessNS.VendaItemWizard>>.JsonToObject(jsonString);
            if (!viewMapInfo.IsNullOrEmpty() && changes.Count > 0)
            {
                var viewMap = ViewMapHelper.Parse(viewMapInfo);
                if(changes.Any(c => c.ComponentName == viewMap.ParentUIView))
                    dataList = viewMap.ReplaceEntities(dataList, changes.First(c => c.ComponentName == viewMap.ParentUIView).ListReturnedObjects);
            }
            return SaveVendaItemWizard(dataList);
        }
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("SaveVendaAtacadoWizard"), System.Web.Http.HttpPost()]
        public List<BusinessNS.VendaAtacadoWizard> SaveVendaAtacadoWizard(List<BusinessNS.VendaAtacadoWizard> dataList)
        {
            if (dataList != null && dataList.Count > 0)
            {
                List<ChangeSetEntry> changeSetEntries = new List<ChangeSetEntry>();
                foreach (var data in dataList.Where(e => e.ChangeState.InList("I", "U", "D")).ToArray())
                {
                   if (data.ChangeState == "D") data.ResetDetails();
                   foreach (var entity in data.GetFlatEntities())
                   {
                       string state = entity.GetPropertyValue("ChangeState") as string;
                       if (state.InList("I", "U", "D"))
                       {
                           var changeOP = (state == "I" ? DomainOperation.Insert : (state == "D" ? DomainOperation.Delete :  DomainOperation.Update));
                           changeSetEntries.Add(new ChangeSetEntry(changeSetEntries.Count, entity, null, changeOP) { HasMemberChanges = (changeOP == DomainOperation.Update) });
                       }
                   }
                   if (data.ChangeState != "D") data.ResetDetails();
                }
                repository.Context.SaveEntities(changeSetEntries, false);
            }
            repository.Context.Dispose();
            //Set return with nochanges
            var result = dataList.Where(e => e.ChangeState.InList("I", "U", "N")).ToList();
            foreach (var data in result.ToArray())
            {
                   if (data.ChangeState == "N") data.ResetDetails();
                   else data.ResetChangeState();
            }
            return result;
        }
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("SaveVendaAtacadoWizardInCache"), System.Web.Http.HttpPost()]
        public void SaveVendaAtacadoWizardInCache(SaveInformation<BusinessNS.VendaAtacadoWizard> saveInfo)
        {
            saveInfo.Validate();
            QueueTransaction.SaveTransaction(saveInfo, System.Reflection.Assembly.GetExecutingAssembly().FullName, this.ControllerContext, "SaveVendaAtacadoWizard");
        }
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        public List<BusinessNS.VendaAtacadoWizard> SaveVendaAtacadoWizard__ForMEF(string jsonString, string viewMapInfo, List<ChangeTracker> changes)
        {
            List<BusinessNS.VendaAtacadoWizard> dataList = SerializationManager<List<BusinessNS.VendaAtacadoWizard>>.JsonToObject(jsonString);
            if (!viewMapInfo.IsNullOrEmpty() && changes.Count > 0)
            {
                var viewMap = ViewMapHelper.Parse(viewMapInfo);
                if(changes.Any(c => c.ComponentName == viewMap.ParentUIView))
                    dataList = viewMap.ReplaceEntities(dataList, changes.First(c => c.ComponentName == viewMap.ParentUIView).ListReturnedObjects);
            }
            return SaveVendaAtacadoWizard(dataList);
        }
        
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("SubmitAllChanges"), System.Web.Http.HttpGet()]
        public Dictionary<string, List<object>> SubmitAllChanges(Guid transactionID)
        {
            var obj = QueueTransaction.GetTransaction(transactionID);
            if (obj.IsNull())
                throw new ArgumentOutOfRangeException(string.Format("Não foi possível localizar o objeto 'QueueTransaction', para ID={0}", transactionID));
            Dictionary<string, List<object>> changes = new Dictionary<string, List<object>>();
            var operations = obj.SubmitTansaction();
            foreach (var _op in operations) changes.Add(_op.ComponentName, _op.ListReturnedObjects);
            return changes;
        }
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [Route("CancelAllChanges"), System.Web.Http.HttpGet()]
        public void CancelAllChanges(Guid transactionID)
        {
            var obj = QueueTransaction.GetTransaction(transactionID);
            if (!obj.IsNull())
                obj.DeleteCache();
        }
        #endregion
    }
    
    [ODataBasicAuthenticationFilter]
    public partial class VarejoMestreDetalheSubDetalhesFeedController : ODataController
    {
        private BusinessNS.MestreDetalheSubDetalhesDomainService _context;
        public BusinessNS.MestreDetalheSubDetalhesDomainService Context { get {  if (_context == null) { _context = new BusinessNS.MestreDetalheSubDetalhesDomainService(); _context.IsSecure = true; } return _context; }  }
        
        #region Get Action to Business Entities
        
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Cliente> GetClienteById([FromODataUri]Int32 key0)
        {
            var entity = this.Context.GetClienteByKey(key0);
            if (entity != null)
               return (new BusinessNS.Cliente[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.Cliente>);
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Cliente> GetClienteByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetClienteByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Cliente), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.Cliente>);
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Cliente> GetCliente()
        {
            return this.Context.GetClienteByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Venda> GetCliente__Venda(Int32 key0, string navigation)
        {
            var entity = this.Context.GetClienteByKey(key0);
            if (entity != null && navigation == "VendaList")
            {
               entity.FillDetails(_context, null, null, new string[] { "Venda" });
               return entity.VendaList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.Venda>);
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaAtacado> GetCliente__VendaAtacado(Int32 key0, string navigation)
        {
            var entity = this.Context.GetClienteByKey(key0);
            if (entity != null && navigation == "VendaAtacadoList")
            {
               entity.FillDetails(_context, null, null, new string[] { "VendaAtacado" });
               return entity.VendaAtacadoList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.VendaAtacado>);
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Venda> GetVendaById([FromODataUri]Int32 key0)
        {
            var entity = this.Context.GetVendaByKey(key0);
            if (entity != null)
               return (new BusinessNS.Venda[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.Venda>);
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Venda> GetVendaByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetVendaByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Venda), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.Venda>);
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Venda> GetVenda()
        {
            return this.Context.GetVendaByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaParentComposition> GetVendaParentComposition()
        {
            return this.Context.GetVendaParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaParentComposition> GetVendaParentCompositionByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                jEntitySearch = jEntitySearch.Replace("Venda{", "VendaParentComposition{");
                jEntitySearch = jEntitySearch.Replace("Cliente{", "VendaParentComposition{");
                var entity = this.Context.GetVendaParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaParentComposition), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.VendaParentComposition>);
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Cliente> GetVenda__Cliente(Int32 key0, string navigation)
        {
            var entity = this.Context.GetVendaByKey(key0);
            if (entity != null && navigation == "Cliente")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.Cliente[] { entity.Cliente }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.Cliente>);
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaItem> GetVenda__VendaItem(Int32 key0, string navigation)
        {
            var entity = this.Context.GetVendaByKey(key0);
            if (entity != null && navigation == "VendaItemList")
            {
               entity.FillDetails(_context, null, null, new string[] { "VendaItem" });
               return entity.VendaItemList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.VendaItem>);
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaAtacado> GetVendaAtacadoById([FromODataUri]Int32 key0)
        {
            var entity = this.Context.GetVendaAtacadoByKey(key0);
            if (entity != null)
               return (new BusinessNS.VendaAtacado[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.VendaAtacado>);
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaAtacado> GetVendaAtacadoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetVendaAtacadoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaAtacado), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.VendaAtacado>);
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaAtacado> GetVendaAtacado()
        {
            return this.Context.GetVendaAtacadoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaAtacadoParentComposition> GetVendaAtacadoParentComposition()
        {
            return this.Context.GetVendaAtacadoParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaAtacadoParentComposition> GetVendaAtacadoParentCompositionByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                jEntitySearch = jEntitySearch.Replace("VendaAtacado{", "VendaAtacadoParentComposition{");
                jEntitySearch = jEntitySearch.Replace("Cliente{", "VendaAtacadoParentComposition{");
                var entity = this.Context.GetVendaAtacadoParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaAtacadoParentComposition), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.VendaAtacadoParentComposition>);
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Cliente> GetVendaAtacado__Cliente(Int32 key0, string navigation)
        {
            var entity = this.Context.GetVendaAtacadoByKey(key0);
            if (entity != null && navigation == "Cliente")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.Cliente[] { entity.Cliente }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.Cliente>);
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaItem> GetVendaItemById([FromODataUri]Int32 key0)
        {
            var entity = this.Context.GetVendaItemByKey(key0);
            if (entity != null)
               return (new BusinessNS.VendaItem[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.VendaItem>);
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaItem> GetVendaItemByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetVendaItemByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaItem), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.VendaItem>);
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaItem> GetVendaItem()
        {
            return this.Context.GetVendaItemByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaItemParentComposition> GetVendaItemParentComposition()
        {
            return this.Context.GetVendaItemParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaItemParentComposition> GetVendaItemParentCompositionByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                jEntitySearch = jEntitySearch.Replace("VendaItem{", "VendaItemParentComposition{");
                jEntitySearch = jEntitySearch.Replace("Venda{", "VendaItemParentComposition{");
                jEntitySearch = jEntitySearch.Replace("Cliente{", "VendaItemParentComposition{");
                var entity = this.Context.GetVendaItemParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaItemParentComposition), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.VendaItemParentComposition>);
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Venda> GetVendaItem__Venda(Int32 key0, string navigation)
        {
            var entity = this.Context.GetVendaItemByKey(key0);
            if (entity != null && navigation == "Venda")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.Venda[] { entity.Venda }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.Venda>);
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.ClienteWizard> GetClienteWizardById([FromODataUri]Int32 key0)
        {
            var entity = this.Context.GetClienteWizardByKey(key0);
            if (entity != null)
               return (new BusinessNS.ClienteWizard[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.ClienteWizard>);
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.ClienteWizard> GetClienteWizardByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetClienteWizardByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.ClienteWizard), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.ClienteWizard>);
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.ClienteWizard> GetClienteWizard()
        {
            return this.Context.GetClienteWizardByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaWizard> GetVendaWizardById([FromODataUri]Int32 key0)
        {
            var entity = this.Context.GetVendaWizardByKey(key0);
            if (entity != null)
               return (new BusinessNS.VendaWizard[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.VendaWizard>);
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaWizard> GetVendaWizardByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetVendaWizardByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaWizard), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.VendaWizard>);
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaWizard> GetVendaWizard()
        {
            return this.Context.GetVendaWizardByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaItemWizard> GetVendaItemWizardById([FromODataUri]Int32 key0)
        {
            var entity = this.Context.GetVendaItemWizardByKey(key0);
            if (entity != null)
               return (new BusinessNS.VendaItemWizard[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.VendaItemWizard>);
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaItemWizard> GetVendaItemWizardByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetVendaItemWizardByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaItemWizard), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.VendaItemWizard>);
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaItemWizard> GetVendaItemWizard()
        {
            return this.Context.GetVendaItemWizardByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaAtacadoWizard> GetVendaAtacadoWizardById([FromODataUri]Int32 key0)
        {
            var entity = this.Context.GetVendaAtacadoWizardByKey(key0);
            if (entity != null)
               return (new BusinessNS.VendaAtacadoWizard[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.VendaAtacadoWizard>);
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaAtacadoWizard> GetVendaAtacadoWizardByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetVendaAtacadoWizardByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaAtacadoWizard), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.VendaAtacadoWizard>);
        }
        
        [VarejoMestreDetalheSubDetalhesControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaAtacadoWizard> GetVendaAtacadoWizard()
        {
            return this.Context.GetVendaAtacadoWizardByEntitySearchNoAssociations(null).AsQueryable();
        }
        #endregion
        
    }
    
    public partial class VarejoMestreDetalheSubDetalhesControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return LinxAutorization.CheckAuthorization(actionContext, string.Format("{0}#{1}#{1}/{2}", "VAREJO.BV", "VarejoMestreDetalheSubDetalhes", actionContext.ActionDescriptor.ActionName));
        }
    }
}

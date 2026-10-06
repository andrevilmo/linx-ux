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
using BusinessNS = Linx.Framework.BV.UsuarioAutorizacao;

namespace Linx.Framework.BV.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/LinxFrameworkUsuarioAutorizacao/[ActionName]
    // Security Information Call: http://localhost:1710/LinxFrameworkUsuarioAutorizacao/GetSecurityInfo
    // Entities Catalog Call: http://localhost:1710/LinxFrameworkUsuarioAutorizacao/GetEntities
    // Entity MetaData Call: http://localhost:1710/LinxFrameworkUsuarioAutorizacao/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/LinxFrameworkUsuarioAutorizacao/GetClientDomains?erp=true
    // Client Service Call: http://localhost:1710/LinxFrameworkUsuarioAutorizacao/GetClientService?erp=true
    // Client Factory Call: http://localhost:1710/LinxFrameworkUsuarioAutorizacao/GetClientFactory?entityName=[EntityName]&erp=true
    // Client Factory Custom Events Call: http://localhost:1710/LinxFrameworkUsuarioAutorizacao/GetClientFactoryCustomEvents?entityName=[EntityName]&erp=true
    // Help Call: http://localhost:1710/HelpController/LinxFrameworkUsuarioAutorizacao
    // Feed OData Call: http://localhost:1710/LinxFrameworkUsuarioAutorizacaoOData
    [RoutePrefix("LinxFrameworkUsuarioAutorizacao")]
    [Breeze.WebApi2.BreezeController]
    public partial class LinxFrameworkUsuarioAutorizacaoController : ApiController
    {
        private const int maxObjectExcelReturned = 2300000;
        private DataServiceRepository<BusinessNS.UsuarioAutorizacaoDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.UsuarioAutorizacaoDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.UsuarioAutorizacaoDomainService>(typeof(BusinessNS.RequisicaoAcesso), typeof(BusinessNS.RequisicaoSuporte), typeof(BusinessNS.TcsIdentidadeExterna), typeof(BusinessNS.TcsSuporteAcessoLog), typeof(BusinessNS.TcsUsuarioAcesso), typeof(BusinessNS.TcsUsuarioAcessoAmbiente), typeof(BusinessNS.TcsUsuarioAutenticacao), typeof(BusinessNS.TcsUsuarioAutenticacaoAcessoP), typeof(BusinessNS.TcsUsuarioGpecon), typeof(BusinessNS.UsuarioAcesso)); _repository.Context.IsSecure = true; } return _repository; } }
        public LinxFrameworkUsuarioAutorizacaoController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(LinxFrameworkUsuarioAutorizacaoController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.UsuarioAutorizacaoDomainService).Assembly.FullName,
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
            var result = repository.Context.GetMetaDataObject("Linx.Framework.BV.UsuarioAutorizacao." + entityName, false, true);
            return (allComposition ? result : result.Where(e => e.ClassName == entityName).ToList());
        }
        
        [Route("GetSecurityInfo"), System.Web.Http.HttpGet()]
        public string[] GetSecurityInfo()
        {
           return new string[] { "Linx.Framework.BV", "LinxFrameworkUsuarioAutorizacao", "LinxFrameworkUsuarioAutorizacao/ActionName" };
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
        
        [Route("GetAllLookUpTcsUsuarioEmpresaAutenticacao"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsUsuarioEmpresaAutenticacao> GetAllLookUpTcsUsuarioEmpresaAutenticacao()
        {
            return repository.Context.GetAllLookUpTcsUsuarioEmpresaAutenticacao();
        }
        
        [Route("GetLookUpTcsUsuarioEmpresaAutenticacaoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsUsuarioEmpresaAutenticacao> GetLookUpTcsUsuarioEmpresaAutenticacaoByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsUsuarioEmpresaAutenticacaoByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
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
        
        [Route("GetAllLookUpTcsAmbiente1"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsAmbiente1> GetAllLookUpTcsAmbiente1()
        {
            return repository.Context.GetAllLookUpTcsAmbiente1();
        }
        
        [Route("GetLookUpTcsAmbiente1ByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsAmbiente1> GetLookUpTcsAmbiente1ByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsAmbiente1ByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [Route("GetAllLookUpTcsAmbiente2Relacionado"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsAmbiente2Relacionado> GetAllLookUpTcsAmbiente2Relacionado()
        {
            return repository.Context.GetAllLookUpTcsAmbiente2Relacionado();
        }
        
        [Route("GetLookUpTcsAmbiente2RelacionadoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsAmbiente2Relacionado> GetLookUpTcsAmbiente2RelacionadoByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsAmbiente2RelacionadoByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [Route("GetAllLookUpTcsAmbiente2"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsAmbiente2> GetAllLookUpTcsAmbiente2()
        {
            return repository.Context.GetAllLookUpTcsAmbiente2();
        }
        
        [Route("GetLookUpTcsAmbiente2ByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsAmbiente2> GetLookUpTcsAmbiente2ByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsAmbiente2ByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacao");
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacao", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.UsuarioAutorizacaoDataSource", DataSourceObject = "GetTcsUsuarioAutenticacao", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAcesso");
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAcesso", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.UsuarioAutorizacaoDataSource", DataSourceObject = "GetTcsUsuarioAcesso", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
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
        
        [Route("GetTcsIdentidadeExterna"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsIdentidadeExterna> GetTcsIdentidadeExterna()
        {
            return repository.Context.GetTcsIdentidadeExterna();
        }
        
        [Route("GetTcsIdentidadeExternaNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsIdentidadeExterna> GetTcsIdentidadeExternaNoAssociations()
        {
            return repository.Context.GetTcsIdentidadeExternaNoAssociations();
        }
        
        [Route("GetTcsIdentidadeExternaByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsIdentidadeExterna> GetTcsIdentidadeExternaByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsIdentidadeExternaByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsIdentidadeExterna), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsIdentidadeExternaByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsIdentidadeExterna> GetTcsIdentidadeExternaByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsIdentidadeExternaByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsIdentidadeExterna), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsIdentidadeExternaToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsIdentidadeExternaToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsIdentidadeExterna), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsIdentidadeExternaByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdIdentidadeExterna asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.UsuarioAutorizacao.TcsIdentidadeExterna");
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
               return ExcelExportPagination<BusinessNS.TcsIdentidadeExterna>.CreateExcelDocumentFileMapPath("TcsIdentidadeExterna",new ExcelExportPagination<BusinessNS.TcsIdentidadeExterna>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsIdentidadeExternaToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsIdentidadeExternaToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsIdentidadeExterna), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsIdentidadeExternaByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.UsuarioAutorizacao.TcsIdentidadeExterna", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.UsuarioAutorizacaoDataSource", DataSourceObject = "GetTcsIdentidadeExterna", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsIdentidadeExterna"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsIdentidadeExterna> GetSampleTcsIdentidadeExterna(string details)
        {
            var result = repository.Context.GetTcsIdentidadeExternaByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsIdentidadeExternaEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsIdentidadeExternaEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsIdentidadeExterna), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsIdentidadeExternaByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsIdentidadeExterna> GetTcsIdentidadeExternaByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsIdentidadeExternaByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetRequisicaoAcesso"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.RequisicaoAcesso> GetRequisicaoAcesso()
        {
            return repository.Context.GetRequisicaoAcesso().AsQueryable();
        }
        
        [Route("GetRequisicaoAcessoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.RequisicaoAcesso> GetRequisicaoAcessoNoAssociations()
        {
            return repository.Context.GetRequisicaoAcessoNoAssociations().AsQueryable();
        }
        
        [Route("GetRequisicaoAcessoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.RequisicaoAcesso> GetRequisicaoAcessoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetRequisicaoAcessoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.RequisicaoAcesso), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        
        [Route("GetRequisicaoAcessoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.RequisicaoAcesso> GetRequisicaoAcessoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetRequisicaoAcessoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.RequisicaoAcesso), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        [Route("GetRequisicaoAcessoToExcel"), System.Web.Http.HttpPost()]
        public string GetRequisicaoAcessoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.RequisicaoAcesso), jEntitySearch, false, false, false);
            var entities = repository.Context.GetRequisicaoAcessoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).AsQueryable().OrderBy("NomeAutenticacao asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.UsuarioAutorizacao.RequisicaoAcesso");
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
               return ExcelExportPagination<BusinessNS.RequisicaoAcesso>.CreateExcelDocumentFileMapPath("RequisicaoAcesso",new ExcelExportPagination<BusinessNS.RequisicaoAcesso>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetRequisicaoAcessoToReportXml"), System.Web.Http.HttpPost()]
        public string GetRequisicaoAcessoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.RequisicaoAcesso), jEntitySearch, false, false, false);
            var entities = repository.Context.GetRequisicaoAcessoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.UsuarioAutorizacao.RequisicaoAcesso", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.UsuarioAutorizacaoDataSource", DataSourceObject = "GetRequisicaoAcesso", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleRequisicaoAcesso"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.RequisicaoAcesso> GetSampleRequisicaoAcesso(string details)
        {
            var result = repository.Context.GetRequisicaoAcessoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddRequisicaoAcessoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddRequisicaoAcessoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.RequisicaoAcesso), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetRequisicaoAcessoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.RequisicaoAcesso> GetRequisicaoAcessoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetRequisicaoAcessoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
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
            var entities = repository.Context.GetUsuarioAcessoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).AsQueryable().OrderBy("IdTcsAmbiente asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.UsuarioAutorizacao.UsuarioAcesso");
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.UsuarioAutorizacao.UsuarioAcesso", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.UsuarioAutorizacaoDataSource", DataSourceObject = "GetUsuarioAcesso", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
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
        
        [Route("GetTcsSuporteAcessoLog"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsSuporteAcessoLog> GetTcsSuporteAcessoLog()
        {
            return repository.Context.GetTcsSuporteAcessoLog();
        }
        
        [Route("GetTcsSuporteAcessoLogNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsSuporteAcessoLog> GetTcsSuporteAcessoLogNoAssociations()
        {
            return repository.Context.GetTcsSuporteAcessoLogNoAssociations();
        }
        
        [Route("GetTcsSuporteAcessoLogByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsSuporteAcessoLog> GetTcsSuporteAcessoLogByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsSuporteAcessoLogByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsSuporteAcessoLog), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsSuporteAcessoLogByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsSuporteAcessoLog> GetTcsSuporteAcessoLogByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsSuporteAcessoLogByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsSuporteAcessoLog), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsSuporteAcessoLogToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsSuporteAcessoLogToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsSuporteAcessoLog), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsSuporteAcessoLogByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsSuporteAcessoLog asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.UsuarioAutorizacao.TcsSuporteAcessoLog");
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
               return ExcelExportPagination<BusinessNS.TcsSuporteAcessoLog>.CreateExcelDocumentFileMapPath("TcsSuporteAcessoLog",new ExcelExportPagination<BusinessNS.TcsSuporteAcessoLog>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsSuporteAcessoLogToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsSuporteAcessoLogToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsSuporteAcessoLog), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsSuporteAcessoLogByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.UsuarioAutorizacao.TcsSuporteAcessoLog", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.UsuarioAutorizacaoDataSource", DataSourceObject = "GetTcsSuporteAcessoLog", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsSuporteAcessoLog"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsSuporteAcessoLog> GetSampleTcsSuporteAcessoLog(string details)
        {
            var result = repository.Context.GetTcsSuporteAcessoLogByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsSuporteAcessoLogEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsSuporteAcessoLogEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsSuporteAcessoLog), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsSuporteAcessoLogByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsSuporteAcessoLog> GetTcsSuporteAcessoLogByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsSuporteAcessoLogByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetRequisicaoSuporte"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.RequisicaoSuporte> GetRequisicaoSuporte()
        {
            return repository.Context.GetRequisicaoSuporte().AsQueryable();
        }
        
        [Route("GetRequisicaoSuporteNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.RequisicaoSuporte> GetRequisicaoSuporteNoAssociations()
        {
            return repository.Context.GetRequisicaoSuporteNoAssociations().AsQueryable();
        }
        
        [Route("GetRequisicaoSuporteByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.RequisicaoSuporte> GetRequisicaoSuporteByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetRequisicaoSuporteByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.RequisicaoSuporte), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        
        [Route("GetRequisicaoSuporteByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.RequisicaoSuporte> GetRequisicaoSuporteByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetRequisicaoSuporteByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.RequisicaoSuporte), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        [Route("GetRequisicaoSuporteToExcel"), System.Web.Http.HttpPost()]
        public string GetRequisicaoSuporteToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.RequisicaoSuporte), jEntitySearch, false, false, false);
            var entities = repository.Context.GetRequisicaoSuporteByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).AsQueryable().OrderBy("UrlPortal asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.UsuarioAutorizacao.RequisicaoSuporte");
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
               return ExcelExportPagination<BusinessNS.RequisicaoSuporte>.CreateExcelDocumentFileMapPath("RequisicaoSuporte",new ExcelExportPagination<BusinessNS.RequisicaoSuporte>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetRequisicaoSuporteToReportXml"), System.Web.Http.HttpPost()]
        public string GetRequisicaoSuporteToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.RequisicaoSuporte), jEntitySearch, false, false, false);
            var entities = repository.Context.GetRequisicaoSuporteByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.UsuarioAutorizacao.RequisicaoSuporte", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.UsuarioAutorizacaoDataSource", DataSourceObject = "GetRequisicaoSuporte", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleRequisicaoSuporte"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.RequisicaoSuporte> GetSampleRequisicaoSuporte(string details)
        {
            var result = repository.Context.GetRequisicaoSuporteByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddRequisicaoSuporteEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddRequisicaoSuporteEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.RequisicaoSuporte), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetRequisicaoSuporteByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.RequisicaoSuporte> GetRequisicaoSuporteByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetRequisicaoSuporteByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsUsuarioAcessoAmbiente"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioAcessoAmbiente> GetTcsUsuarioAcessoAmbiente()
        {
            return repository.Context.GetTcsUsuarioAcessoAmbiente();
        }
        
        [Route("GetTcsUsuarioAcessoAmbienteNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioAcessoAmbiente> GetTcsUsuarioAcessoAmbienteNoAssociations()
        {
            return repository.Context.GetTcsUsuarioAcessoAmbienteNoAssociations();
        }
        
        [Route("GetTcsUsuarioAcessoAmbienteByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioAcessoAmbiente> GetTcsUsuarioAcessoAmbienteByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsUsuarioAcessoAmbienteByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioAcessoAmbiente), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsUsuarioAcessoAmbienteByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioAcessoAmbiente> GetTcsUsuarioAcessoAmbienteByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsUsuarioAcessoAmbienteByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioAcessoAmbiente), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsUsuarioAcessoAmbienteToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsUsuarioAcessoAmbienteToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioAcessoAmbiente), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsUsuarioAcessoAmbienteByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsUsuarioAcesso asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAcessoAmbiente");
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
               return ExcelExportPagination<BusinessNS.TcsUsuarioAcessoAmbiente>.CreateExcelDocumentFileMapPath("TcsUsuarioAcessoAmbiente",new ExcelExportPagination<BusinessNS.TcsUsuarioAcessoAmbiente>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsUsuarioAcessoAmbienteToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsUsuarioAcessoAmbienteToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioAcessoAmbiente), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsUsuarioAcessoAmbienteByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAcessoAmbiente", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.UsuarioAutorizacaoDataSource", DataSourceObject = "GetTcsUsuarioAcessoAmbiente", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsUsuarioAcessoAmbiente"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioAcessoAmbiente> GetSampleTcsUsuarioAcessoAmbiente(string details)
        {
            var result = repository.Context.GetTcsUsuarioAcessoAmbienteByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsUsuarioAcessoAmbienteEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsUsuarioAcessoAmbienteEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioAcessoAmbiente), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsUsuarioAcessoAmbienteByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioAcessoAmbiente> GetTcsUsuarioAcessoAmbienteByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsUsuarioAcessoAmbienteByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsUsuarioAutenticacaoAcessoP"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioAutenticacaoAcessoP> GetTcsUsuarioAutenticacaoAcessoP()
        {
            return repository.Context.GetTcsUsuarioAutenticacaoAcessoP();
        }
        
        [Route("GetTcsUsuarioAutenticacaoAcessoPNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioAutenticacaoAcessoP> GetTcsUsuarioAutenticacaoAcessoPNoAssociations()
        {
            return repository.Context.GetTcsUsuarioAutenticacaoAcessoPNoAssociations();
        }
        
        [Route("GetTcsUsuarioAutenticacaoAcessoPByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioAutenticacaoAcessoP> GetTcsUsuarioAutenticacaoAcessoPByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsUsuarioAutenticacaoAcessoPByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioAutenticacaoAcessoP), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsUsuarioAutenticacaoAcessoPByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioAutenticacaoAcessoP> GetTcsUsuarioAutenticacaoAcessoPByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsUsuarioAutenticacaoAcessoPByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioAutenticacaoAcessoP), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsUsuarioAutenticacaoAcessoPToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsUsuarioAutenticacaoAcessoPToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioAutenticacaoAcessoP), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsUsuarioAutenticacaoAcessoPByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsUsuarioAcesso asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacaoAcessoP");
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
               return ExcelExportPagination<BusinessNS.TcsUsuarioAutenticacaoAcessoP>.CreateExcelDocumentFileMapPath("TcsUsuarioAutenticacaoAcessoP",new ExcelExportPagination<BusinessNS.TcsUsuarioAutenticacaoAcessoP>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsUsuarioAutenticacaoAcessoPToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsUsuarioAutenticacaoAcessoPToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioAutenticacaoAcessoP), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsUsuarioAutenticacaoAcessoPByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacaoAcessoP", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.UsuarioAutorizacaoDataSource", DataSourceObject = "GetTcsUsuarioAutenticacaoAcessoP", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsUsuarioAutenticacaoAcessoP"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioAutenticacaoAcessoP> GetSampleTcsUsuarioAutenticacaoAcessoP(string details)
        {
            var result = repository.Context.GetTcsUsuarioAutenticacaoAcessoPByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsUsuarioAutenticacaoAcessoPEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsUsuarioAutenticacaoAcessoPEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioAutenticacaoAcessoP), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsUsuarioAutenticacaoAcessoPByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioAutenticacaoAcessoP> GetTcsUsuarioAutenticacaoAcessoPByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsUsuarioAutenticacaoAcessoPByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsUsuarioGpecon"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioGpecon> GetTcsUsuarioGpecon()
        {
            return repository.Context.GetTcsUsuarioGpecon();
        }
        
        [Route("GetTcsUsuarioGpeconNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioGpecon> GetTcsUsuarioGpeconNoAssociations()
        {
            return repository.Context.GetTcsUsuarioGpeconNoAssociations();
        }
        
        [Route("GetTcsUsuarioGpeconByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioGpecon> GetTcsUsuarioGpeconByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsUsuarioGpeconByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioGpecon), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsUsuarioGpeconByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioGpecon> GetTcsUsuarioGpeconByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsUsuarioGpeconByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioGpecon), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsUsuarioGpeconToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsUsuarioGpeconToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioGpecon), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsUsuarioGpeconByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsUsuarioAutGpecon asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioGpecon");
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
               return ExcelExportPagination<BusinessNS.TcsUsuarioGpecon>.CreateExcelDocumentFileMapPath("TcsUsuarioGpecon",new ExcelExportPagination<BusinessNS.TcsUsuarioGpecon>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsUsuarioGpeconToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsUsuarioGpeconToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioGpecon), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsUsuarioGpeconByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioGpecon", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.UsuarioAutorizacaoDataSource", DataSourceObject = "GetTcsUsuarioGpecon", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsUsuarioGpecon"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioGpecon> GetSampleTcsUsuarioGpecon(string details)
        {
            var result = repository.Context.GetTcsUsuarioGpeconByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsUsuarioGpeconEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsUsuarioGpeconEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioGpecon), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsUsuarioGpeconByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioGpecon> GetTcsUsuarioGpeconByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsUsuarioGpeconByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        #endregion
        
        #region Get Business Entities By Parent Composition
        
        [Route("GetTcsUsuarioAcessoParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioAcessoParentComposition> GetTcsUsuarioAcessoParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsUsuarioAcessoParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioAcessoParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsUsuarioAcessoParentCompositionToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsUsuarioAcessoParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("TcsUsuarioAcesso{", "TcsUsuarioAcessoParentComposition{");
            jEntitySearch = jEntitySearch.Replace("TcsUsuarioAutenticacao{", "TcsUsuarioAcessoParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioAcessoParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsUsuarioAcessoParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsUsuarioAcesso asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAcesso");
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
               return ExcelExportPagination<BusinessNS.TcsUsuarioAcessoParentComposition>.CreateExcelDocumentFileMapPath("TcsUsuarioAcesso",new ExcelExportPagination<BusinessNS.TcsUsuarioAcessoParentComposition>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsUsuarioAcessoParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsUsuarioAcessoParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioAcessoParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsUsuarioAcessoParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAcesso", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.UsuarioAutorizacaoDataSource", DataSourceObject = "GetTcsUsuarioAcessoParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsUsuarioAcessoParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioAcessoParentComposition> GetSampleTcsUsuarioAcessoParentComposition(string details)
        {
            var result = repository.Context.GetTcsUsuarioAcessoParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("GetTcsIdentidadeExternaParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsIdentidadeExternaParentComposition> GetTcsIdentidadeExternaParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsIdentidadeExternaParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsIdentidadeExternaParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsIdentidadeExternaParentCompositionToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsIdentidadeExternaParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("TcsIdentidadeExterna{", "TcsIdentidadeExternaParentComposition{");
            jEntitySearch = jEntitySearch.Replace("TcsUsuarioAutenticacao{", "TcsIdentidadeExternaParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsIdentidadeExternaParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsIdentidadeExternaParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdIdentidadeExterna asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.UsuarioAutorizacao.TcsIdentidadeExterna");
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
               return ExcelExportPagination<BusinessNS.TcsIdentidadeExternaParentComposition>.CreateExcelDocumentFileMapPath("TcsIdentidadeExterna",new ExcelExportPagination<BusinessNS.TcsIdentidadeExternaParentComposition>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsIdentidadeExternaParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsIdentidadeExternaParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsIdentidadeExternaParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsIdentidadeExternaParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.UsuarioAutorizacao.TcsIdentidadeExterna", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.UsuarioAutorizacaoDataSource", DataSourceObject = "GetTcsIdentidadeExternaParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsIdentidadeExternaParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsIdentidadeExternaParentComposition> GetSampleTcsIdentidadeExternaParentComposition(string details)
        {
            var result = repository.Context.GetTcsIdentidadeExternaParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("GetTcsUsuarioGpeconParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioGpeconParentComposition> GetTcsUsuarioGpeconParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsUsuarioGpeconParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioGpeconParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsUsuarioGpeconParentCompositionToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsUsuarioGpeconParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("TcsUsuarioGpecon{", "TcsUsuarioGpeconParentComposition{");
            jEntitySearch = jEntitySearch.Replace("TcsUsuarioAutenticacao{", "TcsUsuarioGpeconParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioGpeconParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsUsuarioGpeconParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsUsuarioAutGpecon asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioGpecon");
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
               return ExcelExportPagination<BusinessNS.TcsUsuarioGpeconParentComposition>.CreateExcelDocumentFileMapPath("TcsUsuarioGpecon",new ExcelExportPagination<BusinessNS.TcsUsuarioGpeconParentComposition>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsUsuarioGpeconParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsUsuarioGpeconParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioGpeconParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsUsuarioGpeconParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioGpecon", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.UsuarioAutorizacaoDataSource", DataSourceObject = "GetTcsUsuarioGpeconParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsUsuarioGpeconParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioGpeconParentComposition> GetSampleTcsUsuarioGpeconParentComposition(string details)
        {
            var result = repository.Context.GetTcsUsuarioGpeconParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
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
    
    public partial class LinxFrameworkUsuarioAutorizacaoFeedController : ODataController
    {
        private BusinessNS.UsuarioAutorizacaoDomainService _context;
        public BusinessNS.UsuarioAutorizacaoDomainService Context { get {  if (_context == null) { _context = new BusinessNS.UsuarioAutorizacaoDomainService(); _context.IsSecure = true; } return _context; }  }
        
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
        public IQueryable<BusinessNS.TcsUsuarioAcesso> GetTcsUsuarioAutenticacao__TcsUsuarioAcesso(Int64 key0, string navigation)
        {
            var entity = this.Context.GetTcsUsuarioAutenticacaoByKey(key0);
            if (entity != null && navigation == "TcsUsuarioAcessoList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsUsuarioAcesso" });
               return entity.TcsUsuarioAcessoList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsUsuarioAcesso>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsIdentidadeExterna> GetTcsUsuarioAutenticacao__TcsIdentidadeExterna(Int64 key0, string navigation)
        {
            var entity = this.Context.GetTcsUsuarioAutenticacaoByKey(key0);
            if (entity != null && navigation == "TcsIdentidadeExternaList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsIdentidadeExterna" });
               return entity.TcsIdentidadeExternaList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsIdentidadeExterna>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioGpecon> GetTcsUsuarioAutenticacao__TcsUsuarioGpecon(Int64 key0, string navigation)
        {
            var entity = this.Context.GetTcsUsuarioAutenticacaoByKey(key0);
            if (entity != null && navigation == "TcsUsuarioGpeconList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsUsuarioGpecon" });
               return entity.TcsUsuarioGpeconList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsUsuarioGpecon>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioAcesso> GetTcsUsuarioAcessoById([FromODataUri]Int32 key0)
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
        public IQueryable<BusinessNS.TcsUsuarioAcessoParentComposition> GetTcsUsuarioAcessoParentComposition()
        {
            return this.Context.GetTcsUsuarioAcessoParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioAcessoParentComposition> GetTcsUsuarioAcessoParentCompositionByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                jEntitySearch = jEntitySearch.Replace("TcsUsuarioAcesso{", "TcsUsuarioAcessoParentComposition{");
                jEntitySearch = jEntitySearch.Replace("TcsUsuarioAutenticacao{", "TcsUsuarioAcessoParentComposition{");
                var entity = this.Context.GetTcsUsuarioAcessoParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioAcessoParentComposition), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsUsuarioAcessoParentComposition>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioAutenticacao> GetTcsUsuarioAcesso__TcsUsuarioAutenticacao(Int32 key0, string navigation)
        {
            var entity = this.Context.GetTcsUsuarioAcessoByKey(key0);
            if (entity != null && navigation == "TcsUsuarioAutenticacao")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsUsuarioAutenticacao[] { entity.TcsUsuarioAutenticacao }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsUsuarioAutenticacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsIdentidadeExterna> GetTcsIdentidadeExternaById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetTcsIdentidadeExternaByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsIdentidadeExterna[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsIdentidadeExterna>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsIdentidadeExterna> GetTcsIdentidadeExternaByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsIdentidadeExternaByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsIdentidadeExterna), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsIdentidadeExterna>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsIdentidadeExterna> GetTcsIdentidadeExterna()
        {
            return this.Context.GetTcsIdentidadeExternaByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsIdentidadeExternaParentComposition> GetTcsIdentidadeExternaParentComposition()
        {
            return this.Context.GetTcsIdentidadeExternaParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsIdentidadeExternaParentComposition> GetTcsIdentidadeExternaParentCompositionByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                jEntitySearch = jEntitySearch.Replace("TcsIdentidadeExterna{", "TcsIdentidadeExternaParentComposition{");
                jEntitySearch = jEntitySearch.Replace("TcsUsuarioAutenticacao{", "TcsIdentidadeExternaParentComposition{");
                var entity = this.Context.GetTcsIdentidadeExternaParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsIdentidadeExternaParentComposition), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsIdentidadeExternaParentComposition>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioAutenticacao> GetTcsIdentidadeExterna__TcsUsuarioAutenticacao(Int64 key0, string navigation)
        {
            var entity = this.Context.GetTcsIdentidadeExternaByKey(key0);
            if (entity != null && navigation == "TcsUsuarioAutenticacao")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsUsuarioAutenticacao[] { entity.TcsUsuarioAutenticacao }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsUsuarioAutenticacao>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.RequisicaoAcesso> GetRequisicaoAcessoById([FromODataUri]string key0)
        {
            var entity = this.Context.GetRequisicaoAcessoByKey(key0);
            if (entity != null)
               return (new BusinessNS.RequisicaoAcesso[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.RequisicaoAcesso>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.RequisicaoAcesso> GetRequisicaoAcessoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetRequisicaoAcessoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.RequisicaoAcesso), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.RequisicaoAcesso>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.RequisicaoAcesso> GetRequisicaoAcesso()
        {
            return this.Context.GetRequisicaoAcessoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.UsuarioAcesso> GetUsuarioAcessoById([FromODataUri]int key0)
        {
            var entity = this.Context.GetUsuarioAcessoByKey(key0);
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
        public IQueryable<BusinessNS.TcsSuporteAcessoLog> GetTcsSuporteAcessoLogById([FromODataUri]Int32 key0)
        {
            var entity = this.Context.GetTcsSuporteAcessoLogByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsSuporteAcessoLog[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsSuporteAcessoLog>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsSuporteAcessoLog> GetTcsSuporteAcessoLogByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsSuporteAcessoLogByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsSuporteAcessoLog), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsSuporteAcessoLog>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsSuporteAcessoLog> GetTcsSuporteAcessoLog()
        {
            return this.Context.GetTcsSuporteAcessoLogByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.RequisicaoSuporte> GetRequisicaoSuporteById([FromODataUri]string key0)
        {
            var entity = this.Context.GetRequisicaoSuporteByKey(key0);
            if (entity != null)
               return (new BusinessNS.RequisicaoSuporte[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.RequisicaoSuporte>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.RequisicaoSuporte> GetRequisicaoSuporteByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetRequisicaoSuporteByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.RequisicaoSuporte), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.RequisicaoSuporte>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.RequisicaoSuporte> GetRequisicaoSuporte()
        {
            return this.Context.GetRequisicaoSuporteByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioAcessoAmbiente> GetTcsUsuarioAcessoAmbienteById([FromODataUri]Int32 key0)
        {
            var entity = this.Context.GetTcsUsuarioAcessoAmbienteByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsUsuarioAcessoAmbiente[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsUsuarioAcessoAmbiente>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioAcessoAmbiente> GetTcsUsuarioAcessoAmbienteByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsUsuarioAcessoAmbienteByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioAcessoAmbiente), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsUsuarioAcessoAmbiente>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioAcessoAmbiente> GetTcsUsuarioAcessoAmbiente()
        {
            return this.Context.GetTcsUsuarioAcessoAmbienteByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioAutenticacaoAcessoP> GetTcsUsuarioAutenticacaoAcessoPById([FromODataUri]Int32 key0)
        {
            var entity = this.Context.GetTcsUsuarioAutenticacaoAcessoPByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsUsuarioAutenticacaoAcessoP[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsUsuarioAutenticacaoAcessoP>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioAutenticacaoAcessoP> GetTcsUsuarioAutenticacaoAcessoPByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsUsuarioAutenticacaoAcessoPByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioAutenticacaoAcessoP), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsUsuarioAutenticacaoAcessoP>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioAutenticacaoAcessoP> GetTcsUsuarioAutenticacaoAcessoP()
        {
            return this.Context.GetTcsUsuarioAutenticacaoAcessoPByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioGpecon> GetTcsUsuarioGpeconById([FromODataUri]int key0)
        {
            var entity = this.Context.GetTcsUsuarioGpeconByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsUsuarioGpecon[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsUsuarioGpecon>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioGpecon> GetTcsUsuarioGpeconByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsUsuarioGpeconByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioGpecon), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsUsuarioGpecon>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioGpecon> GetTcsUsuarioGpecon()
        {
            return this.Context.GetTcsUsuarioGpeconByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioGpeconParentComposition> GetTcsUsuarioGpeconParentComposition()
        {
            return this.Context.GetTcsUsuarioGpeconParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioGpeconParentComposition> GetTcsUsuarioGpeconParentCompositionByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                jEntitySearch = jEntitySearch.Replace("TcsUsuarioGpecon{", "TcsUsuarioGpeconParentComposition{");
                jEntitySearch = jEntitySearch.Replace("TcsUsuarioAutenticacao{", "TcsUsuarioGpeconParentComposition{");
                var entity = this.Context.GetTcsUsuarioGpeconParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioGpeconParentComposition), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsUsuarioGpeconParentComposition>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioAutenticacao> GetTcsUsuarioGpecon__TcsUsuarioAutenticacao(int key0, string navigation)
        {
            var entity = this.Context.GetTcsUsuarioGpeconByKey(key0);
            if (entity != null && navigation == "TcsUsuarioAutenticacao")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsUsuarioAutenticacao[] { entity.TcsUsuarioAutenticacao }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsUsuarioAutenticacao>);
        }
        #endregion
        
    }
    
    public partial class LinxFrameworkUsuarioAutorizacaoControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return true;
        }
    }
}

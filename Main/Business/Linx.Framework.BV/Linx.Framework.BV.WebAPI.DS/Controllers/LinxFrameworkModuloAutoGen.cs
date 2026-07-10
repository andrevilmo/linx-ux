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
using BusinessNS = Linx.Framework.BV.Modulo;

namespace Linx.Framework.BV.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/LinxFrameworkModulo/[ActionName]
    // Security Information Call: http://localhost:1710/LinxFrameworkModulo/GetSecurityInfo
    // Entities Catalog Call: http://localhost:1710/LinxFrameworkModulo/GetEntities
    // Entity MetaData Call: http://localhost:1710/LinxFrameworkModulo/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/LinxFrameworkModulo/GetClientDomains?erp=true
    // Client Service Call: http://localhost:1710/LinxFrameworkModulo/GetClientService?erp=true
    // Client Factory Call: http://localhost:1710/LinxFrameworkModulo/GetClientFactory?entityName=[EntityName]&erp=true
    // Client Factory Custom Events Call: http://localhost:1710/LinxFrameworkModulo/GetClientFactoryCustomEvents?entityName=[EntityName]&erp=true
    // Help Call: http://localhost:1710/HelpController/LinxFrameworkModulo
    // Feed OData Call: http://localhost:1710/LinxFrameworkModuloOData
    [RoutePrefix("LinxFrameworkModulo")]
    [Breeze.WebApi2.BreezeController]
    public partial class LinxFrameworkModuloController : ApiController
    {
        private const int maxObjectExcelReturned = 2300000;
        private DataServiceRepository<BusinessNS.ModuloDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.ModuloDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.ModuloDomainService>(typeof(BusinessNS.AppMenu), typeof(BusinessNS.AppModule), typeof(BusinessNS.BreadCrumbItem), typeof(BusinessNS.EnvironmentInfo), typeof(BusinessNS.TcsModulo), typeof(BusinessNS.TcsModuloDoGrupo), typeof(BusinessNS.TcsModuloDoGrupoDetalhe), typeof(BusinessNS.TcsModuloGrupo), typeof(BusinessNS.TcsModuloMenu), typeof(BusinessNS.TcsTransacaoMenu), typeof(BusinessNS.TcsUsuarioFavorito), typeof(BusinessNS.UserModules)); _repository.Context.IsSecure = true; } return _repository; } }
        public LinxFrameworkModuloController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(LinxFrameworkModuloController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.ModuloDomainService).Assembly.FullName,
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
            var result = repository.Context.GetMetaDataObject("Linx.Framework.BV.Modulo." + entityName, false, true);
            return (allComposition ? result : result.Where(e => e.ClassName == entityName).ToList());
        }
        
        [Route("GetSecurityInfo"), System.Web.Http.HttpGet()]
        public string[] GetSecurityInfo()
        {
           return new string[] { "Linx.Framework.BV", "LinxFrameworkModulo", "LinxFrameworkModulo/ActionName" };
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
        
        [Route("GetAllLookUpTcsTransacaoMenu"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsTransacaoMenu> GetAllLookUpTcsTransacaoMenu()
        {
            return repository.Context.GetAllLookUpTcsTransacaoMenu();
        }
        
        [Route("GetLookUpTcsTransacaoMenuByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsTransacaoMenu> GetLookUpTcsTransacaoMenuByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsTransacaoMenuByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [Route("GetAllLookUpModuloMenuSuperior"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpModuloMenuSuperior> GetAllLookUpModuloMenuSuperior()
        {
            return repository.Context.GetAllLookUpModuloMenuSuperior();
        }
        
        [Route("GetLookUpModuloMenuSuperiorByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpModuloMenuSuperior> GetLookUpModuloMenuSuperiorByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpModuloMenuSuperiorByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [Route("GetAllLookUpTcsModuloGrupo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsModuloGrupo> GetAllLookUpTcsModuloGrupo()
        {
            return repository.Context.GetAllLookUpTcsModuloGrupo();
        }
        
        [Route("GetLookUpTcsModuloGrupoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsModuloGrupo> GetLookUpTcsModuloGrupoByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsModuloGrupoByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [Route("GetAllLookUpTcsModuloDoGrupoDetalhe"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsModuloDoGrupoDetalhe> GetAllLookUpTcsModuloDoGrupoDetalhe()
        {
            return repository.Context.GetAllLookUpTcsModuloDoGrupoDetalhe();
        }
        
        [Route("GetLookUpTcsModuloDoGrupoDetalheByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsModuloDoGrupoDetalhe> GetLookUpTcsModuloDoGrupoDetalheByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsModuloDoGrupoDetalheByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
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
        
        [Route("GetTcsModulo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModulo> GetTcsModulo()
        {
            return repository.Context.GetTcsModulo();
        }
        
        [Route("GetTcsModuloNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModulo> GetTcsModuloNoAssociations()
        {
            return repository.Context.GetTcsModuloNoAssociations();
        }
        
        [Route("GetTcsModuloByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModulo> GetTcsModuloByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsModuloByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModulo), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsModuloByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModulo> GetTcsModuloByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsModuloByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModulo), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsModuloToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsModuloToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModulo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsModuloByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("DescModulo asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Modulo.TcsModulo");
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
               return ExcelExportPagination<BusinessNS.TcsModulo>.CreateExcelDocumentFileMapPath("TcsModulo",new ExcelExportPagination<BusinessNS.TcsModulo>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsModuloToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsModuloToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModulo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsModuloByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Modulo.TcsModulo", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ModuloDataSource", DataSourceObject = "GetTcsModulo", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsModulo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModulo> GetSampleTcsModulo(string details)
        {
            var result = repository.Context.GetTcsModuloByEntitySearchNoAssociations(null).Take(100).ToList();
               if (!details.IsNullOrEmpty())
               {
                   foreach(var entity in result)
                   {
                       entity.FillDetails(repository.Context, null, null, details.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries), 5);
                   }
               }
            return result.AsQueryable();
        }
        
        [Route("AddTcsModuloEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsModuloEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModulo), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsModuloByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModulo> GetTcsModuloByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsModuloByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsModuloMenu"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloMenu> GetTcsModuloMenu()
        {
            return repository.Context.GetTcsModuloMenu();
        }
        
        [Route("GetTcsModuloMenuNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloMenu> GetTcsModuloMenuNoAssociations()
        {
            return repository.Context.GetTcsModuloMenuNoAssociations();
        }
        
        [Route("GetTcsModuloMenuByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloMenu> GetTcsModuloMenuByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsModuloMenuByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloMenu), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsModuloMenuByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloMenu> GetTcsModuloMenuByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsModuloMenuByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloMenu), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsModuloMenuToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsModuloMenuToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloMenu), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsModuloMenuByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdModuloMenu asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Modulo.TcsModuloMenu");
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
               return ExcelExportPagination<BusinessNS.TcsModuloMenu>.CreateExcelDocumentFileMapPath("TcsModuloMenu",new ExcelExportPagination<BusinessNS.TcsModuloMenu>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsModuloMenuToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsModuloMenuToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloMenu), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsModuloMenuByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Modulo.TcsModuloMenu", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ModuloDataSource", DataSourceObject = "GetTcsModuloMenu", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsModuloMenu"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloMenu> GetSampleTcsModuloMenu(string details)
        {
            var result = repository.Context.GetTcsModuloMenuByEntitySearchNoAssociations(null).Take(100).ToList();
               if (!details.IsNullOrEmpty())
               {
                   foreach(var entity in result)
                   {
                       entity.FillDetails(repository.Context, null, null, details.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries), 5);
                   }
               }
            return result.AsQueryable();
        }
        
        [Route("AddTcsModuloMenuEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsModuloMenuEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloMenu), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsModuloMenuByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloMenu> GetTcsModuloMenuByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsModuloMenuByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsModuloDoGrupo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloDoGrupo> GetTcsModuloDoGrupo()
        {
            return repository.Context.GetTcsModuloDoGrupo();
        }
        
        [Route("GetTcsModuloDoGrupoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloDoGrupo> GetTcsModuloDoGrupoNoAssociations()
        {
            return repository.Context.GetTcsModuloDoGrupoNoAssociations();
        }
        
        [Route("GetTcsModuloDoGrupoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloDoGrupo> GetTcsModuloDoGrupoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsModuloDoGrupoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloDoGrupo), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsModuloDoGrupoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloDoGrupo> GetTcsModuloDoGrupoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsModuloDoGrupoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloDoGrupo), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsModuloDoGrupoToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsModuloDoGrupoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloDoGrupo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsModuloDoGrupoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdGrupoModulo asc, IdModulo asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Modulo.TcsModuloDoGrupo");
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
               return ExcelExportPagination<BusinessNS.TcsModuloDoGrupo>.CreateExcelDocumentFileMapPath("TcsModuloDoGrupo",new ExcelExportPagination<BusinessNS.TcsModuloDoGrupo>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsModuloDoGrupoToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsModuloDoGrupoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloDoGrupo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsModuloDoGrupoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Modulo.TcsModuloDoGrupo", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ModuloDataSource", DataSourceObject = "GetTcsModuloDoGrupo", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsModuloDoGrupo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloDoGrupo> GetSampleTcsModuloDoGrupo(string details)
        {
            var result = repository.Context.GetTcsModuloDoGrupoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsModuloDoGrupoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsModuloDoGrupoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloDoGrupo), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsModuloDoGrupoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloDoGrupo> GetTcsModuloDoGrupoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsModuloDoGrupoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
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
            var entities = repository.Context.GetTcsTransacaoMenuByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsTransacaoMenu asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Modulo.TcsTransacaoMenu");
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Modulo.TcsTransacaoMenu", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ModuloDataSource", DataSourceObject = "GetTcsTransacaoMenu", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
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
        
        [Route("GetTcsModuloGrupo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloGrupo> GetTcsModuloGrupo()
        {
            return repository.Context.GetTcsModuloGrupo();
        }
        
        [Route("GetTcsModuloGrupoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloGrupo> GetTcsModuloGrupoNoAssociations()
        {
            return repository.Context.GetTcsModuloGrupoNoAssociations();
        }
        
        [Route("GetTcsModuloGrupoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloGrupo> GetTcsModuloGrupoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsModuloGrupoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloGrupo), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsModuloGrupoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloGrupo> GetTcsModuloGrupoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsModuloGrupoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloGrupo), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsModuloGrupoToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsModuloGrupoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloGrupo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsModuloGrupoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdGrupoModulo asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Modulo.TcsModuloGrupo");
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
               return ExcelExportPagination<BusinessNS.TcsModuloGrupo>.CreateExcelDocumentFileMapPath("TcsModuloGrupo",new ExcelExportPagination<BusinessNS.TcsModuloGrupo>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsModuloGrupoToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsModuloGrupoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloGrupo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsModuloGrupoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Modulo.TcsModuloGrupo", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ModuloDataSource", DataSourceObject = "GetTcsModuloGrupo", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsModuloGrupo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloGrupo> GetSampleTcsModuloGrupo(string details)
        {
            var result = repository.Context.GetTcsModuloGrupoByEntitySearchNoAssociations(null).Take(100).ToList();
               if (!details.IsNullOrEmpty())
               {
                   foreach(var entity in result)
                   {
                       entity.FillDetails(repository.Context, null, null, details.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries), 5);
                   }
               }
            return result.AsQueryable();
        }
        
        [Route("AddTcsModuloGrupoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsModuloGrupoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloGrupo), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsModuloGrupoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloGrupo> GetTcsModuloGrupoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsModuloGrupoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsModuloDoGrupoDetalhe"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloDoGrupoDetalhe> GetTcsModuloDoGrupoDetalhe()
        {
            return repository.Context.GetTcsModuloDoGrupoDetalhe();
        }
        
        [Route("GetTcsModuloDoGrupoDetalheNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloDoGrupoDetalhe> GetTcsModuloDoGrupoDetalheNoAssociations()
        {
            return repository.Context.GetTcsModuloDoGrupoDetalheNoAssociations();
        }
        
        [Route("GetTcsModuloDoGrupoDetalheByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloDoGrupoDetalhe> GetTcsModuloDoGrupoDetalheByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsModuloDoGrupoDetalheByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloDoGrupoDetalhe), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsModuloDoGrupoDetalheByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloDoGrupoDetalhe> GetTcsModuloDoGrupoDetalheByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsModuloDoGrupoDetalheByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloDoGrupoDetalhe), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsModuloDoGrupoDetalheToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsModuloDoGrupoDetalheToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloDoGrupoDetalhe), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsModuloDoGrupoDetalheByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdModuloDoGrupo asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Modulo.TcsModuloDoGrupoDetalhe");
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
               return ExcelExportPagination<BusinessNS.TcsModuloDoGrupoDetalhe>.CreateExcelDocumentFileMapPath("TcsModuloDoGrupoDetalhe",new ExcelExportPagination<BusinessNS.TcsModuloDoGrupoDetalhe>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsModuloDoGrupoDetalheToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsModuloDoGrupoDetalheToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloDoGrupoDetalhe), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsModuloDoGrupoDetalheByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Modulo.TcsModuloDoGrupoDetalhe", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ModuloDataSource", DataSourceObject = "GetTcsModuloDoGrupoDetalhe", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsModuloDoGrupoDetalhe"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloDoGrupoDetalhe> GetSampleTcsModuloDoGrupoDetalhe(string details)
        {
            var result = repository.Context.GetTcsModuloDoGrupoDetalheByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsModuloDoGrupoDetalheEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsModuloDoGrupoDetalheEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloDoGrupoDetalhe), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsModuloDoGrupoDetalheByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloDoGrupoDetalhe> GetTcsModuloDoGrupoDetalheByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsModuloDoGrupoDetalheByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetAppModule"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AppModule> GetAppModule()
        {
            return repository.Context.GetAppModule().AsQueryable();
        }
        
        [Route("GetAppModuleNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AppModule> GetAppModuleNoAssociations()
        {
            return repository.Context.GetAppModuleNoAssociations().AsQueryable();
        }
        
        [Route("GetAppModuleByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AppModule> GetAppModuleByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetAppModuleByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AppModule), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        
        [Route("GetAppModuleByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AppModule> GetAppModuleByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetAppModuleByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AppModule), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        [Route("GetAppModuleToExcel"), System.Web.Http.HttpPost()]
        public string GetAppModuleToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AppModule), jEntitySearch, false, false, false);
            var entities = repository.Context.GetAppModuleByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).AsQueryable().OrderBy("Id asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Modulo.AppModule");
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
               return ExcelExportPagination<BusinessNS.AppModule>.CreateExcelDocumentFileMapPath("AppModule",new ExcelExportPagination<BusinessNS.AppModule>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetAppModuleToReportXml"), System.Web.Http.HttpPost()]
        public string GetAppModuleToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AppModule), jEntitySearch, false, false, false);
            var entities = repository.Context.GetAppModuleByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Modulo.AppModule", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ModuloDataSource", DataSourceObject = "GetAppModule", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleAppModule"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AppModule> GetSampleAppModule(string details)
        {
            var result = repository.Context.GetAppModuleByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddAppModuleEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddAppModuleEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AppModule), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetAppModuleByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AppModule> GetAppModuleByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetAppModuleByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetBreadCrumbItem"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.BreadCrumbItem> GetBreadCrumbItem()
        {
            return repository.Context.GetBreadCrumbItem().AsQueryable();
        }
        
        [Route("GetBreadCrumbItemNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.BreadCrumbItem> GetBreadCrumbItemNoAssociations()
        {
            return repository.Context.GetBreadCrumbItemNoAssociations().AsQueryable();
        }
        
        [Route("GetBreadCrumbItemByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.BreadCrumbItem> GetBreadCrumbItemByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetBreadCrumbItemByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.BreadCrumbItem), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        
        [Route("GetBreadCrumbItemByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.BreadCrumbItem> GetBreadCrumbItemByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetBreadCrumbItemByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.BreadCrumbItem), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        [Route("GetBreadCrumbItemToExcel"), System.Web.Http.HttpPost()]
        public string GetBreadCrumbItemToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.BreadCrumbItem), jEntitySearch, false, false, false);
            var entities = repository.Context.GetBreadCrumbItemByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).AsQueryable().OrderBy("ModuleKey asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Modulo.BreadCrumbItem");
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
               return ExcelExportPagination<BusinessNS.BreadCrumbItem>.CreateExcelDocumentFileMapPath("BreadCrumbItem",new ExcelExportPagination<BusinessNS.BreadCrumbItem>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetBreadCrumbItemToReportXml"), System.Web.Http.HttpPost()]
        public string GetBreadCrumbItemToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.BreadCrumbItem), jEntitySearch, false, false, false);
            var entities = repository.Context.GetBreadCrumbItemByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Modulo.BreadCrumbItem", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ModuloDataSource", DataSourceObject = "GetBreadCrumbItem", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleBreadCrumbItem"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.BreadCrumbItem> GetSampleBreadCrumbItem(string details)
        {
            var result = repository.Context.GetBreadCrumbItemByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddBreadCrumbItemEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddBreadCrumbItemEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.BreadCrumbItem), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetBreadCrumbItemByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.BreadCrumbItem> GetBreadCrumbItemByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetBreadCrumbItemByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetAppMenu"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AppMenu> GetAppMenu()
        {
            return repository.Context.GetAppMenu().AsQueryable();
        }
        
        [Route("GetAppMenuNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AppMenu> GetAppMenuNoAssociations()
        {
            return repository.Context.GetAppMenuNoAssociations().AsQueryable();
        }
        
        [Route("GetAppMenuByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AppMenu> GetAppMenuByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetAppMenuByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AppMenu), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        
        [Route("GetAppMenuByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AppMenu> GetAppMenuByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetAppMenuByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AppMenu), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        [Route("GetAppMenuToExcel"), System.Web.Http.HttpPost()]
        public string GetAppMenuToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AppMenu), jEntitySearch, false, false, false);
            var entities = repository.Context.GetAppMenuByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).AsQueryable().OrderBy("Id asc, IdTcsAmbiente asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Modulo.AppMenu");
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
               return ExcelExportPagination<BusinessNS.AppMenu>.CreateExcelDocumentFileMapPath("AppMenu",new ExcelExportPagination<BusinessNS.AppMenu>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetAppMenuToReportXml"), System.Web.Http.HttpPost()]
        public string GetAppMenuToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AppMenu), jEntitySearch, false, false, false);
            var entities = repository.Context.GetAppMenuByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Modulo.AppMenu", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ModuloDataSource", DataSourceObject = "GetAppMenu", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleAppMenu"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AppMenu> GetSampleAppMenu(string details)
        {
            var result = repository.Context.GetAppMenuByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddAppMenuEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddAppMenuEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AppMenu), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetAppMenuByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.AppMenu> GetAppMenuByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetAppMenuByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetUserModules"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.UserModules> GetUserModules()
        {
            return repository.Context.GetUserModules().AsQueryable();
        }
        
        [Route("GetUserModulesNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.UserModules> GetUserModulesNoAssociations()
        {
            return repository.Context.GetUserModulesNoAssociations().AsQueryable();
        }
        
        [Route("GetUserModulesByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.UserModules> GetUserModulesByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetUserModulesByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.UserModules), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        
        [Route("GetUserModulesByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.UserModules> GetUserModulesByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetUserModulesByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.UserModules), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        [Route("GetUserModulesToExcel"), System.Web.Http.HttpPost()]
        public string GetUserModulesToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.UserModules), jEntitySearch, false, false, false);
            var entities = repository.Context.GetUserModulesByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).AsQueryable().OrderBy("Hash asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Modulo.UserModules");
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
               return ExcelExportPagination<BusinessNS.UserModules>.CreateExcelDocumentFileMapPath("UserModules",new ExcelExportPagination<BusinessNS.UserModules>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetUserModulesToReportXml"), System.Web.Http.HttpPost()]
        public string GetUserModulesToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.UserModules), jEntitySearch, false, false, false);
            var entities = repository.Context.GetUserModulesByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Modulo.UserModules", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ModuloDataSource", DataSourceObject = "GetUserModules", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleUserModules"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.UserModules> GetSampleUserModules(string details)
        {
            var result = repository.Context.GetUserModulesByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddUserModulesEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddUserModulesEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.UserModules), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetUserModulesByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.UserModules> GetUserModulesByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetUserModulesByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetTcsUsuarioFavorito"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioFavorito> GetTcsUsuarioFavorito()
        {
            return repository.Context.GetTcsUsuarioFavorito();
        }
        
        [Route("GetTcsUsuarioFavoritoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioFavorito> GetTcsUsuarioFavoritoNoAssociations()
        {
            return repository.Context.GetTcsUsuarioFavoritoNoAssociations();
        }
        
        [Route("GetTcsUsuarioFavoritoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioFavorito> GetTcsUsuarioFavoritoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTcsUsuarioFavoritoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioFavorito), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTcsUsuarioFavoritoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioFavorito> GetTcsUsuarioFavoritoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsUsuarioFavoritoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioFavorito), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsUsuarioFavoritoToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsUsuarioFavoritoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioFavorito), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsUsuarioFavoritoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdTcsUsuarioFavorito asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Modulo.TcsUsuarioFavorito");
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
               return ExcelExportPagination<BusinessNS.TcsUsuarioFavorito>.CreateExcelDocumentFileMapPath("TcsUsuarioFavorito",new ExcelExportPagination<BusinessNS.TcsUsuarioFavorito>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsUsuarioFavoritoToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsUsuarioFavoritoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioFavorito), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsUsuarioFavoritoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Modulo.TcsUsuarioFavorito", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ModuloDataSource", DataSourceObject = "GetTcsUsuarioFavorito", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsUsuarioFavorito"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioFavorito> GetSampleTcsUsuarioFavorito(string details)
        {
            var result = repository.Context.GetTcsUsuarioFavoritoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddTcsUsuarioFavoritoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTcsUsuarioFavoritoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioFavorito), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTcsUsuarioFavoritoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsUsuarioFavorito> GetTcsUsuarioFavoritoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTcsUsuarioFavoritoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        
        [Route("GetEnvironmentInfo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.EnvironmentInfo> GetEnvironmentInfo()
        {
            return repository.Context.GetEnvironmentInfo().AsQueryable();
        }
        
        [Route("GetEnvironmentInfoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.EnvironmentInfo> GetEnvironmentInfoNoAssociations()
        {
            return repository.Context.GetEnvironmentInfoNoAssociations().AsQueryable();
        }
        
        [Route("GetEnvironmentInfoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.EnvironmentInfo> GetEnvironmentInfoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetEnvironmentInfoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.EnvironmentInfo), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        
        [Route("GetEnvironmentInfoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.EnvironmentInfo> GetEnvironmentInfoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetEnvironmentInfoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.EnvironmentInfo), jEntitySearch, false, false, false), jEntitySearch).AsQueryable();
        }
        [Route("GetEnvironmentInfoToExcel"), System.Web.Http.HttpPost()]
        public string GetEnvironmentInfoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.EnvironmentInfo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetEnvironmentInfoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).AsQueryable().OrderBy("EnvironmentId asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Modulo.EnvironmentInfo");
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
               return ExcelExportPagination<BusinessNS.EnvironmentInfo>.CreateExcelDocumentFileMapPath("EnvironmentInfo",new ExcelExportPagination<BusinessNS.EnvironmentInfo>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetEnvironmentInfoToReportXml"), System.Web.Http.HttpPost()]
        public string GetEnvironmentInfoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.EnvironmentInfo), jEntitySearch, false, false, false);
            var entities = repository.Context.GetEnvironmentInfoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Modulo.EnvironmentInfo", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ModuloDataSource", DataSourceObject = "GetEnvironmentInfo", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleEnvironmentInfo"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.EnvironmentInfo> GetSampleEnvironmentInfo(string details)
        {
            var result = repository.Context.GetEnvironmentInfoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddEnvironmentInfoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddEnvironmentInfoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.EnvironmentInfo), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetEnvironmentInfoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.EnvironmentInfo> GetEnvironmentInfoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetEnvironmentInfoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        #endregion
        
        #region Get Business Entities By Parent Composition
        
        [Route("GetTcsModuloDoGrupoDetalheParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloDoGrupoDetalheParentComposition> GetTcsModuloDoGrupoDetalheParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTcsModuloDoGrupoDetalheParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloDoGrupoDetalheParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTcsModuloDoGrupoDetalheParentCompositionToExcel"), System.Web.Http.HttpPost()]
        public string GetTcsModuloDoGrupoDetalheParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("TcsModuloDoGrupoDetalhe{", "TcsModuloDoGrupoDetalheParentComposition{");
            jEntitySearch = jEntitySearch.Replace("TcsModuloGrupo{", "TcsModuloDoGrupoDetalheParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloDoGrupoDetalheParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsModuloDoGrupoDetalheParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdModuloDoGrupo asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Modulo.TcsModuloDoGrupoDetalhe");
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
               return ExcelExportPagination<BusinessNS.TcsModuloDoGrupoDetalheParentComposition>.CreateExcelDocumentFileMapPath("TcsModuloDoGrupoDetalhe",new ExcelExportPagination<BusinessNS.TcsModuloDoGrupoDetalheParentComposition>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTcsModuloDoGrupoDetalheParentCompositionToReportXml"), System.Web.Http.HttpPost()]
        public string GetTcsModuloDoGrupoDetalheParentCompositionToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloDoGrupoDetalheParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsModuloDoGrupoDetalheParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.Modulo.TcsModuloDoGrupoDetalhe", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.ModuloDataSource", DataSourceObject = "GetTcsModuloDoGrupoDetalheParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTcsModuloDoGrupoDetalheParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TcsModuloDoGrupoDetalheParentComposition> GetSampleTcsModuloDoGrupoDetalheParentComposition(string details)
        {
            var result = repository.Context.GetTcsModuloDoGrupoDetalheParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
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
    
    public partial class LinxFrameworkModuloFeedController : ODataController
    {
        private BusinessNS.ModuloDomainService _context;
        public BusinessNS.ModuloDomainService Context { get {  if (_context == null) { _context = new BusinessNS.ModuloDomainService(); _context.IsSecure = true; } return _context; }  }
        
        #region Get Action to Business Entities
        
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsModulo> GetTcsModuloById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetTcsModuloByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsModulo[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsModulo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsModulo> GetTcsModuloByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsModuloByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModulo), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsModulo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsModulo> GetTcsModulo()
        {
            return this.Context.GetTcsModuloByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsModuloMenu> GetTcsModulo__TcsModuloMenu(Int64 key0, string navigation)
        {
            var entity = this.Context.GetTcsModuloByKey(key0);
            if (entity != null && navigation == "TcsModuloMenuList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsModuloMenu" });
               return entity.TcsModuloMenuList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsModuloMenu>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsModuloDoGrupo> GetTcsModulo__TcsModuloDoGrupo(Int64 key0, string navigation)
        {
            var entity = this.Context.GetTcsModuloByKey(key0);
            if (entity != null && navigation == "TcsModuloDoGrupoList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsModuloDoGrupo" });
               return entity.TcsModuloDoGrupoList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsModuloDoGrupo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsModuloMenu> GetTcsModuloMenuById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetTcsModuloMenuByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsModuloMenu[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsModuloMenu>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsModuloMenu> GetTcsModuloMenuByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsModuloMenuByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloMenu), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsModuloMenu>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsModuloMenu> GetTcsModuloMenu()
        {
            return this.Context.GetTcsModuloMenuByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsModulo> GetTcsModuloMenu__TcsModulo(Int64 key0, string navigation)
        {
            var entity = this.Context.GetTcsModuloMenuByKey(key0);
            if (entity != null && navigation == "TcsModulo")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsModulo[] { entity.TcsModulo }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsModulo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacaoMenu> GetTcsModuloMenu__TcsTransacaoMenu(Int64 key0, string navigation)
        {
            var entity = this.Context.GetTcsModuloMenuByKey(key0);
            if (entity != null && navigation == "TcsTransacaoMenuList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsTransacaoMenu" });
               return entity.TcsTransacaoMenuList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsTransacaoMenu>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsModuloDoGrupo> GetTcsModuloDoGrupoById([FromODataUri]Int64 key0, [FromODataUri]Int64 key1)
        {
            var entity = this.Context.GetTcsModuloDoGrupoByKey(key0, key1);
            if (entity != null)
               return (new BusinessNS.TcsModuloDoGrupo[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsModuloDoGrupo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsModuloDoGrupo> GetTcsModuloDoGrupoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsModuloDoGrupoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloDoGrupo), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsModuloDoGrupo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsModuloDoGrupo> GetTcsModuloDoGrupo()
        {
            return this.Context.GetTcsModuloDoGrupoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsModulo> GetTcsModuloDoGrupo__TcsModulo(Int64 key0, Int64 key1, string navigation)
        {
            var entity = this.Context.GetTcsModuloDoGrupoByKey(key0, key1);
            if (entity != null && navigation == "TcsModulo")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsModulo[] { entity.TcsModulo }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsModulo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsTransacaoMenu> GetTcsTransacaoMenuById([FromODataUri]Int32 key0)
        {
            var entity = this.Context.GetTcsTransacaoMenuByKey(key0);
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
        public IQueryable<BusinessNS.TcsModuloMenu> GetTcsTransacaoMenu__TcsModuloMenu(Int32 key0, string navigation)
        {
            var entity = this.Context.GetTcsTransacaoMenuByKey(key0);
            if (entity != null && navigation == "TcsModuloMenu")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsModuloMenu[] { entity.TcsModuloMenu }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsModuloMenu>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsModuloGrupo> GetTcsModuloGrupoById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetTcsModuloGrupoByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsModuloGrupo[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsModuloGrupo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsModuloGrupo> GetTcsModuloGrupoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsModuloGrupoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloGrupo), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsModuloGrupo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsModuloGrupo> GetTcsModuloGrupo()
        {
            return this.Context.GetTcsModuloGrupoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsModuloDoGrupoDetalhe> GetTcsModuloGrupo__TcsModuloDoGrupoDetalhe(Int64 key0, string navigation)
        {
            var entity = this.Context.GetTcsModuloGrupoByKey(key0);
            if (entity != null && navigation == "TcsModuloDoGrupoDetalheList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsModuloDoGrupoDetalhe" });
               return entity.TcsModuloDoGrupoDetalheList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsModuloDoGrupoDetalhe>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsModuloDoGrupoDetalhe> GetTcsModuloDoGrupoDetalheById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetTcsModuloDoGrupoDetalheByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsModuloDoGrupoDetalhe[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsModuloDoGrupoDetalhe>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsModuloDoGrupoDetalhe> GetTcsModuloDoGrupoDetalheByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsModuloDoGrupoDetalheByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloDoGrupoDetalhe), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsModuloDoGrupoDetalhe>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsModuloDoGrupoDetalhe> GetTcsModuloDoGrupoDetalhe()
        {
            return this.Context.GetTcsModuloDoGrupoDetalheByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsModuloDoGrupoDetalheParentComposition> GetTcsModuloDoGrupoDetalheParentComposition()
        {
            return this.Context.GetTcsModuloDoGrupoDetalheParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsModuloDoGrupoDetalheParentComposition> GetTcsModuloDoGrupoDetalheParentCompositionByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                jEntitySearch = jEntitySearch.Replace("TcsModuloDoGrupoDetalhe{", "TcsModuloDoGrupoDetalheParentComposition{");
                jEntitySearch = jEntitySearch.Replace("TcsModuloGrupo{", "TcsModuloDoGrupoDetalheParentComposition{");
                var entity = this.Context.GetTcsModuloDoGrupoDetalheParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsModuloDoGrupoDetalheParentComposition), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsModuloDoGrupoDetalheParentComposition>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsModuloGrupo> GetTcsModuloDoGrupoDetalhe__TcsModuloGrupo(Int64 key0, string navigation)
        {
            var entity = this.Context.GetTcsModuloDoGrupoDetalheByKey(key0);
            if (entity != null && navigation == "TcsModuloGrupo")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TcsModuloGrupo[] { entity.TcsModuloGrupo }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsModuloGrupo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.AppModule> GetAppModuleById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetAppModuleByKey(key0);
            if (entity != null)
               return (new BusinessNS.AppModule[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.AppModule>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.AppModule> GetAppModuleByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetAppModuleByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AppModule), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.AppModule>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.AppModule> GetAppModule()
        {
            return this.Context.GetAppModuleByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.BreadCrumbItem> GetBreadCrumbItemById([FromODataUri]Guid key0)
        {
            var entity = this.Context.GetBreadCrumbItemByKey(key0);
            if (entity != null)
               return (new BusinessNS.BreadCrumbItem[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.BreadCrumbItem>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.BreadCrumbItem> GetBreadCrumbItemByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetBreadCrumbItemByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.BreadCrumbItem), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.BreadCrumbItem>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.BreadCrumbItem> GetBreadCrumbItem()
        {
            return this.Context.GetBreadCrumbItemByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.AppMenu> GetAppMenuById([FromODataUri]Int64 key0, [FromODataUri]int key1)
        {
            var entity = this.Context.GetAppMenuByKey(key0, key1);
            if (entity != null)
               return (new BusinessNS.AppMenu[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.AppMenu>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.AppMenu> GetAppMenuByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetAppMenuByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.AppMenu), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.AppMenu>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.AppMenu> GetAppMenu()
        {
            return this.Context.GetAppMenuByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.UserModules> GetUserModulesById([FromODataUri]string key0)
        {
            var entity = this.Context.GetUserModulesByKey(key0);
            if (entity != null)
               return (new BusinessNS.UserModules[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.UserModules>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.UserModules> GetUserModulesByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetUserModulesByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.UserModules), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.UserModules>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.UserModules> GetUserModules()
        {
            return this.Context.GetUserModulesByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioFavorito> GetTcsUsuarioFavoritoById([FromODataUri]Int64 key0)
        {
            var entity = this.Context.GetTcsUsuarioFavoritoByKey(key0);
            if (entity != null)
               return (new BusinessNS.TcsUsuarioFavorito[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TcsUsuarioFavorito>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioFavorito> GetTcsUsuarioFavoritoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTcsUsuarioFavoritoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioFavorito), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsUsuarioFavorito>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioFavorito> GetTcsUsuarioFavorito()
        {
            return this.Context.GetTcsUsuarioFavoritoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.EnvironmentInfo> GetEnvironmentInfoById([FromODataUri]int key0)
        {
            var entity = this.Context.GetEnvironmentInfoByKey(key0);
            if (entity != null)
               return (new BusinessNS.EnvironmentInfo[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.EnvironmentInfo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.EnvironmentInfo> GetEnvironmentInfoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetEnvironmentInfoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.EnvironmentInfo), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.EnvironmentInfo>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.EnvironmentInfo> GetEnvironmentInfo()
        {
            return this.Context.GetEnvironmentInfoByEntitySearchNoAssociations(null).AsQueryable();
        }
        #endregion
        
    }
    
    public partial class LinxFrameworkModuloControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return true;
        }
    }
}

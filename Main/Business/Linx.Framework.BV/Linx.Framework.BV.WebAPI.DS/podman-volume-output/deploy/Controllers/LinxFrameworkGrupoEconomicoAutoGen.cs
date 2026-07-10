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
using BusinessNS = Linx.Framework.BV.GrupoEconomico;

namespace Linx.Framework.BV.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/LinxFrameworkGrupoEconomico/[ActionName]
    // Security Information Call: http://localhost:1710/LinxFrameworkGrupoEconomico/GetSecurityInfo
    // Entities Catalog Call: http://localhost:1710/LinxFrameworkGrupoEconomico/GetEntities
    // Entity MetaData Call: http://localhost:1710/LinxFrameworkGrupoEconomico/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/LinxFrameworkGrupoEconomico/GetClientDomains?erp=true
    // Client Service Call: http://localhost:1710/LinxFrameworkGrupoEconomico/GetClientService?erp=true
    // Client Factory Call: http://localhost:1710/LinxFrameworkGrupoEconomico/GetClientFactory?entityName=[EntityName]&erp=true
    // Client Factory Custom Events Call: http://localhost:1710/LinxFrameworkGrupoEconomico/GetClientFactoryCustomEvents?entityName=[EntityName]&erp=true
    // Help Call: http://localhost:1710/HelpController/LinxFrameworkGrupoEconomico
    // Feed OData Call: http://localhost:1710/LinxFrameworkGrupoEconomicoOData
    [RoutePrefix("LinxFrameworkGrupoEconomico")]
    [Breeze.WebApi2.BreezeController]
    public partial class LinxFrameworkGrupoEconomicoController : ApiController
    {
        private const int maxObjectExcelReturned = 2300000;
        private DataServiceRepository<BusinessNS.GrupoEconomicoDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.GrupoEconomicoDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.GrupoEconomicoDomainService>(typeof(BusinessNS.EconomicGroupView), typeof(BusinessNS.TbcGrupoEconomico), typeof(BusinessNS.TcsUsuarioGpecon)); _repository.Context.IsSecure = true; } return _repository; } }
        public LinxFrameworkGrupoEconomicoController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(LinxFrameworkGrupoEconomicoController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.GrupoEconomicoDomainService).Assembly.FullName,
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
            var result = repository.Context.GetMetaDataObject("Linx.Framework.BV.GrupoEconomico." + entityName, false, true);
            return (allComposition ? result : result.Where(e => e.ClassName == entityName).ToList());
        }
        
        [Route("GetSecurityInfo"), System.Web.Http.HttpGet()]
        public string[] GetSecurityInfo()
        {
           return new string[] { "Linx.Framework.BV", "LinxFrameworkGrupoEconomico", "LinxFrameworkGrupoEconomico/ActionName" };
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
        
        [Route("GetAllLookUpTcsMoedaIndicador"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsMoedaIndicador> GetAllLookUpTcsMoedaIndicador()
        {
            return repository.Context.GetAllLookUpTcsMoedaIndicador();
        }
        
        [Route("GetLookUpTcsMoedaIndicadorByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsMoedaIndicador> GetLookUpTcsMoedaIndicadorByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsMoedaIndicadorByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [Route("GetAllLookUpTcsUsuario"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsUsuario> GetAllLookUpTcsUsuario()
        {
            return repository.Context.GetAllLookUpTcsUsuario();
        }
        
        [Route("GetLookUpTcsUsuarioByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTcsUsuario> GetLookUpTcsUsuarioByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTcsUsuarioByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
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
        
        [Route("GetTbcGrupoEconomico"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcGrupoEconomico> GetTbcGrupoEconomico()
        {
            return repository.Context.GetTbcGrupoEconomico();
        }
        
        [Route("GetTbcGrupoEconomicoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcGrupoEconomico> GetTbcGrupoEconomicoNoAssociations()
        {
            return repository.Context.GetTbcGrupoEconomicoNoAssociations();
        }
        
        [Route("GetTbcGrupoEconomicoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcGrupoEconomico> GetTbcGrupoEconomicoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetTbcGrupoEconomicoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcGrupoEconomico), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetTbcGrupoEconomicoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcGrupoEconomico> GetTbcGrupoEconomicoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetTbcGrupoEconomicoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcGrupoEconomico), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetTbcGrupoEconomicoToExcel"), System.Web.Http.HttpPost()]
        public string GetTbcGrupoEconomicoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcGrupoEconomico), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTbcGrupoEconomicoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdGpecon asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.GrupoEconomico.TbcGrupoEconomico");
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
               return ExcelExportPagination<BusinessNS.TbcGrupoEconomico>.CreateExcelDocumentFileMapPath("TbcGrupoEconomico",new ExcelExportPagination<BusinessNS.TbcGrupoEconomico>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetTbcGrupoEconomicoToReportXml"), System.Web.Http.HttpPost()]
        public string GetTbcGrupoEconomicoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcGrupoEconomico), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTbcGrupoEconomicoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.GrupoEconomico.TbcGrupoEconomico", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.GrupoEconomicoDataSource", DataSourceObject = "GetTbcGrupoEconomico", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleTbcGrupoEconomico"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcGrupoEconomico> GetSampleTbcGrupoEconomico(string details)
        {
            var result = repository.Context.GetTbcGrupoEconomicoByEntitySearchNoAssociations(null).Take(100).ToList();
               if (!details.IsNullOrEmpty())
               {
                   foreach(var entity in result)
                   {
                       entity.FillDetails(repository.Context, null, null, details.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries), 5);
                   }
               }
            return result.AsQueryable();
        }
        
        [Route("AddTbcGrupoEconomicoEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddTbcGrupoEconomicoEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcGrupoEconomico), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetTbcGrupoEconomicoByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.TbcGrupoEconomico> GetTbcGrupoEconomicoByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetTbcGrupoEconomicoByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
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
            var entities = repository.Context.GetTcsUsuarioGpeconByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdUsuarioGpecon asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.GrupoEconomico.TcsUsuarioGpecon");
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.GrupoEconomico.TcsUsuarioGpecon", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.GrupoEconomicoDataSource", DataSourceObject = "GetTcsUsuarioGpecon", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
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
        
        [Route("GetEconomicGroupView"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.EconomicGroupView> GetEconomicGroupView()
        {
            return repository.Context.GetEconomicGroupView();
        }
        
        [Route("GetEconomicGroupViewNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.EconomicGroupView> GetEconomicGroupViewNoAssociations()
        {
            return repository.Context.GetEconomicGroupViewNoAssociations();
        }
        
        [Route("GetEconomicGroupViewByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.EconomicGroupView> GetEconomicGroupViewByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetEconomicGroupViewByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.EconomicGroupView), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetEconomicGroupViewByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.EconomicGroupView> GetEconomicGroupViewByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetEconomicGroupViewByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.EconomicGroupView), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetEconomicGroupViewToExcel"), System.Web.Http.HttpPost()]
        public string GetEconomicGroupViewToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.EconomicGroupView), jEntitySearch, false, false, false);
            var entities = repository.Context.GetEconomicGroupViewByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdGpecon asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.GrupoEconomico.EconomicGroupView");
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
               return ExcelExportPagination<BusinessNS.EconomicGroupView>.CreateExcelDocumentFileMapPath("EconomicGroupView",new ExcelExportPagination<BusinessNS.EconomicGroupView>.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
        }
        
        [Route("GetEconomicGroupViewToReportXml"), System.Web.Http.HttpPost()]
        public string GetEconomicGroupViewToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.EconomicGroupView), jEntitySearch, false, false, false);
            var entities = repository.Context.GetEconomicGroupViewByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.GrupoEconomico.EconomicGroupView", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.GrupoEconomicoDataSource", DataSourceObject = "GetEconomicGroupView", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Framework.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleEconomicGroupView"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.EconomicGroupView> GetSampleEconomicGroupView(string details)
        {
            var result = repository.Context.GetEconomicGroupViewByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("AddEconomicGroupViewEntitySearchId"), System.Web.Http.HttpPost()]
        public Guid AddEconomicGroupViewEntitySearchId(string[] jEntitySearch)
        {
            return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.EconomicGroupView), jEntitySearch[0], false, false, false), jEntitySearch[0]);
        }
        
        [Route("GetEconomicGroupViewByEntitySearchIdNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.EconomicGroupView> GetEconomicGroupViewByEntitySearchIdNoAssociations(Guid entitySearchId)
        {
            return repository.Context.GetEconomicGroupViewByEntitySearchIdNoAssociations(entitySearchId).AsQueryable();
        }
        #endregion
        
        #region Get Business Entities By Parent Composition
        
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
            jEntitySearch = jEntitySearch.Replace("TbcGrupoEconomico{", "TcsUsuarioGpeconParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioGpeconParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetTcsUsuarioGpeconParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdUsuarioGpecon asc");
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.GrupoEconomico.TcsUsuarioGpecon");
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
            var metadata = repository.Context.GetMetaDataObject("Linx.Framework.BV.GrupoEconomico.TcsUsuarioGpecon", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Framework.BV.Reports", DataSourceFullName = "Linx.Framework.BV.Reports.GrupoEconomicoDataSource", DataSourceObject = "GetTcsUsuarioGpeconParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
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
    
    public partial class LinxFrameworkGrupoEconomicoFeedController : ODataController
    {
        private BusinessNS.GrupoEconomicoDomainService _context;
        public BusinessNS.GrupoEconomicoDomainService Context { get {  if (_context == null) { _context = new BusinessNS.GrupoEconomicoDomainService(); _context.IsSecure = true; } return _context; }  }
        
        #region Get Action to Business Entities
        
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TbcGrupoEconomico> GetTbcGrupoEconomicoById([FromODataUri]Int32 key0)
        {
            var entity = this.Context.GetTbcGrupoEconomicoByKey(key0);
            if (entity != null)
               return (new BusinessNS.TbcGrupoEconomico[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.TbcGrupoEconomico>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TbcGrupoEconomico> GetTbcGrupoEconomicoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetTbcGrupoEconomicoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TbcGrupoEconomico), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TbcGrupoEconomico>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TbcGrupoEconomico> GetTbcGrupoEconomico()
        {
            return this.Context.GetTbcGrupoEconomicoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioGpecon> GetTbcGrupoEconomico__TcsUsuarioGpecon(Int32 key0, string navigation)
        {
            var entity = this.Context.GetTbcGrupoEconomicoByKey(key0);
            if (entity != null && navigation == "TcsUsuarioGpeconList")
            {
               entity.FillDetails(_context, null, null, new string[] { "TcsUsuarioGpecon" });
               return entity.TcsUsuarioGpeconList.AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TcsUsuarioGpecon>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TcsUsuarioGpecon> GetTcsUsuarioGpeconById([FromODataUri]Int32 key0)
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
                jEntitySearch = jEntitySearch.Replace("TbcGrupoEconomico{", "TcsUsuarioGpeconParentComposition{");
                var entity = this.Context.GetTcsUsuarioGpeconParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.TcsUsuarioGpeconParentComposition), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.TcsUsuarioGpeconParentComposition>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.TbcGrupoEconomico> GetTcsUsuarioGpecon__TbcGrupoEconomico(Int32 key0, string navigation)
        {
            var entity = this.Context.GetTcsUsuarioGpeconByKey(key0);
            if (entity != null && navigation == "TbcGrupoEconomico")
            {
               entity.LoadParent(_context);
               return (new BusinessNS.TbcGrupoEconomico[] { entity.TbcGrupoEconomico }).AsQueryable();
            }
            else
               return default(IQueryable<BusinessNS.TbcGrupoEconomico>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.EconomicGroupView> GetEconomicGroupViewById([FromODataUri]Int32 key0)
        {
            var entity = this.Context.GetEconomicGroupViewByKey(key0);
            if (entity != null)
               return (new BusinessNS.EconomicGroupView[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.EconomicGroupView>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.EconomicGroupView> GetEconomicGroupViewByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetEconomicGroupViewByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.EconomicGroupView), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.EconomicGroupView>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.EconomicGroupView> GetEconomicGroupView()
        {
            return this.Context.GetEconomicGroupViewByEntitySearchNoAssociations(null).AsQueryable();
        }
        #endregion
        
    }
    
    public partial class LinxFrameworkGrupoEconomicoControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return true;
        }
    }
}

using System;
using System.Collections;
using System.Linq;
using Linx.Tools;
using Linx.Data;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.ServiceModel.DomainServices.Server;
using Linx.DataService;
using System.ComponentModel.Composition;
using System.Web.Http;
using Linx.Business.Tools;
using System.Web.Http.OData;
using Breeze.WebApi2;
using Breeze.ContextProvider;
using BusinessNS = Linx.Dashboard.DashboardFinanceiro;

namespace Linx.Dashboard.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/LinxDashboardFinanceiro/[ActionName]
    // Security Information Call: http://localhost:1710/LinxDashboardFinanceiro/GetSecurityInfo
    // Entities Catalog Call: http://localhost:1710/LinxDashboardFinanceiro/GetEntities
    // Entity MetaData Call: http://localhost:1710/LinxDashboardFinanceiro/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/LinxDashboardFinanceiro/GetClientDomains?erp=true
    // Client Service Call: http://localhost:1710/LinxDashboardFinanceiro/GetClientService?erp=true
    // Client Factory Call: http://localhost:1710/LinxDashboardFinanceiro/GetClientFactory?entityName=[EntityName]&erp=true
    // Client Factory Custom Events Call: http://localhost:1710/LinxDashboardFinanceiro/GetClientFactoryCustomEvents?entityName=[EntityName]&erp=true
    // Help Call: http://localhost:1710/HelpController/LinxDashboardFinanceiro
    // Feed OData Call: http://localhost:1710/LinxDashboardFinanceiroOData
    [RoutePrefix("LinxDashboardFinanceiro")]
    [Breeze.WebApi2.BreezeController]
    public partial class LinxDashboardFinanceiroController : ApiController
    {
        private DataServiceRepository<BusinessNS.DashboardFinanceiroDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.DashboardFinanceiroDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.DashboardFinanceiroDomainService>(typeof(BusinessNS.LjvAtendimento), typeof(BusinessNS.LjvAtendimentoVendedor)); _repository.Context.IsSecure = true; } return _repository; } }
        public LinxDashboardFinanceiroController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(LinxDashboardFinanceiroController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.DashboardFinanceiroDomainService).Assembly.FullName,
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
            var result = repository.Context.GetMetaDataObject("Linx.Dashboard.DashboardFinanceiro." + entityName, false, true);
            return (allComposition ? result : result.Where(e => e.ClassName == entityName).ToList());
        }
        
        [Route("GetSecurityInfo"), System.Web.Http.HttpGet()]
        public string[] GetSecurityInfo()
        {
           return new string[] { "Linx.Dashboard", "LinxDashboardFinanceiro", "LinxDashboardFinanceiro/ActionName" };
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
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Dashboard.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Dashboard.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetReportDataSource"), System.Web.Http.HttpGet()]
        public string GetReportDataSource()
        {
            var zip = new LinxZip();
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Dashboard.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Dashboard.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetDomainsInfo"), System.Web.Http.HttpGet()]
        public string[] GetDomainsInfo(string domainNames)
        {
            return Linx.Dashboard.Domains.DomainHelper.GetDomainsInfo(domainNames);
        }
        
        #region Get LookUps
        
        [Route("GetAllLookUpLjvLoja"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpLjvLoja> GetAllLookUpLjvLoja()
        {
            return repository.Context.GetAllLookUpLjvLoja();
        }
        
        [Route("GetLookUpLjvLojaByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpLjvLoja> GetLookUpLjvLojaByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpLjvLojaByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [Route("GetAllLookUpTbcFilial"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTbcFilial> GetAllLookUpTbcFilial()
        {
            return repository.Context.GetAllLookUpTbcFilial();
        }
        
        [Route("GetLookUpTbcFilialByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTbcFilial> GetLookUpTbcFilialByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTbcFilialByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [Route("GetAllLookUpTbcGrupoEconomico"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTbcGrupoEconomico> GetAllLookUpTbcGrupoEconomico()
        {
            return repository.Context.GetAllLookUpTbcGrupoEconomico();
        }
        
        [Route("GetLookUpTbcGrupoEconomicoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpTbcGrupoEconomico> GetLookUpTbcGrupoEconomicoByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpTbcGrupoEconomicoByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [Route("GetAllLookUpLjvAtendimento"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpLjvAtendimento> GetAllLookUpLjvAtendimento()
        {
            return repository.Context.GetAllLookUpLjvAtendimento();
        }
        
        [Route("GetLookUpLjvAtendimentoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpLjvAtendimento> GetLookUpLjvAtendimentoByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpLjvAtendimentoByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [Route("GetAllLookUpLjvVendedor"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpLjvVendedor> GetAllLookUpLjvVendedor()
        {
            return repository.Context.GetAllLookUpLjvVendedor();
        }
        
        [Route("GetLookUpLjvVendedorByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpLjvVendedor> GetLookUpLjvVendedorByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpLjvVendedorByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
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
        
        [Route("GetLjvAtendimento"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LjvAtendimento> GetLjvAtendimento()
        {
            return repository.Context.GetLjvAtendimento();
        }
        
        [Route("GetLjvAtendimentoNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LjvAtendimento> GetLjvAtendimentoNoAssociations()
        {
            return repository.Context.GetLjvAtendimentoNoAssociations();
        }
        
        [Route("GetLjvAtendimentoByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LjvAtendimento> GetLjvAtendimentoByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetLjvAtendimentoByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LjvAtendimento), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetLjvAtendimentoByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LjvAtendimento> GetLjvAtendimentoByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetLjvAtendimentoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LjvAtendimento), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetLjvAtendimentoToExcel"), System.Web.Http.HttpPost()]
        public string GetLjvAtendimentoToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LjvAtendimento), jEntitySearch, false, false, false);
            var entities = repository.Context.GetLjvAtendimentoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Dashboard.DashboardFinanceiro.LjvAtendimento");
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
            var excelBytes = ExcelExport.CreateExcelDocumentFile(new ExcelExport.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
            return Convert.ToBase64String(excelBytes);
        }
        [Route("GetLjvAtendimentoToReportXml"), System.Web.Http.HttpPost()]
        public string GetLjvAtendimentoToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LjvAtendimento), jEntitySearch, false, false, false);
            var entities = repository.Context.GetLjvAtendimentoByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Dashboard.DashboardFinanceiro.LjvAtendimento", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Dashboard.Reports", DataSourceFullName = "Linx.Dashboard.Reports.DashboardFinanceiroDataSource", DataSourceObject = "GetLjvAtendimento", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Dashboard.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Dashboard.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleLjvAtendimento"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LjvAtendimento> GetSampleLjvAtendimento(string details)
        {
            var result = repository.Context.GetLjvAtendimentoByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        
        [Route("GetLjvAtendimentoVendedor"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LjvAtendimentoVendedor> GetLjvAtendimentoVendedor()
        {
            return repository.Context.GetLjvAtendimentoVendedor();
        }
        
        [Route("GetLjvAtendimentoVendedorNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LjvAtendimentoVendedor> GetLjvAtendimentoVendedorNoAssociations()
        {
            return repository.Context.GetLjvAtendimentoVendedorNoAssociations();
        }
        
        [Route("GetLjvAtendimentoVendedorByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LjvAtendimentoVendedor> GetLjvAtendimentoVendedorByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetLjvAtendimentoVendedorByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LjvAtendimentoVendedor), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [Route("GetLjvAtendimentoVendedorByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LjvAtendimentoVendedor> GetLjvAtendimentoVendedorByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetLjvAtendimentoVendedorByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LjvAtendimentoVendedor), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetLjvAtendimentoVendedorToExcel"), System.Web.Http.HttpPost()]
        public string GetLjvAtendimentoVendedorToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LjvAtendimentoVendedor), jEntitySearch, false, false, false);
            var entities = repository.Context.GetLjvAtendimentoVendedorByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Dashboard.DashboardFinanceiro.LjvAtendimentoVendedor");
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
            var excelBytes = ExcelExport.CreateExcelDocumentFile(new ExcelExport.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });
            return Convert.ToBase64String(excelBytes);
        }
        [Route("GetLjvAtendimentoVendedorToReportXml"), System.Web.Http.HttpPost()]
        public string GetLjvAtendimentoVendedorToReportXml(string[] parameters)
        {
            string reportName = parameters[0];
            string jEntitySearch = parameters[1];
            string translatedJEntitySearch = parameters[2];
            string columnsDefinition = parameters[3];
            string serviceBusUrl = parameters[4];
            bool exportMedia = Convert.ToBoolean(parameters[5]);
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LjvAtendimentoVendedor), jEntitySearch, false, false, false);
            var entities = repository.Context.GetLjvAtendimentoVendedorByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);
            var metadata = repository.Context.GetMetaDataObject("Linx.Dashboard.DashboardFinanceiro.LjvAtendimentoVendedor", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "Linx.Dashboard.Reports", DataSourceFullName = "Linx.Dashboard.Reports.DashboardFinanceiroDataSource", DataSourceObject = "GetLjvAtendimentoVendedor", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Dashboard.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/Linx.Dashboard.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [Route("GetSampleLjvAtendimentoVendedor"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LjvAtendimentoVendedor> GetSampleLjvAtendimentoVendedor(string details)
        {
            var result = repository.Context.GetLjvAtendimentoVendedorByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
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
        [Route("SaveLjvAtendimento"), System.Web.Http.HttpPost()]
        public List<BusinessNS.LjvAtendimento> SaveLjvAtendimento(List<BusinessNS.LjvAtendimento> dataList)
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
        [Route("SaveLjvAtendimentoInCache"), System.Web.Http.HttpPost()]
        public void SaveLjvAtendimentoInCache(SaveInformation<BusinessNS.LjvAtendimento> saveInfo)
        {
            saveInfo.Validate();
            QueueTransaction.SaveTransaction(saveInfo, System.Reflection.Assembly.GetExecutingAssembly().FullName, this.ControllerContext, "SaveLjvAtendimento");
        }
        private List<BusinessNS.LjvAtendimento> SaveLjvAtendimento__ForMEF(string jsonString, string viewMapInfo, List<ChangeTracker> changes)
        {
            List<BusinessNS.LjvAtendimento> dataList = SerializationManager<List<BusinessNS.LjvAtendimento>>.JsonToObject(jsonString);
            if (!viewMapInfo.IsNullOrEmpty() && changes.Count > 0)
            {
                var viewMap = ViewMapHelper.Parse(viewMapInfo);
                if(changes.Any(c => c.ComponentName == viewMap.ParentUIView))
                    dataList = viewMap.ReplaceEntities(dataList, changes.First(c => c.ComponentName == viewMap.ParentUIView).ListReturnedObjects);
            }
            return SaveLjvAtendimento(dataList);
        }
        [Route("SaveLjvAtendimentoVendedor"), System.Web.Http.HttpPost()]
        public List<BusinessNS.LjvAtendimentoVendedor> SaveLjvAtendimentoVendedor(List<BusinessNS.LjvAtendimentoVendedor> dataList)
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
        [Route("SaveLjvAtendimentoVendedorInCache"), System.Web.Http.HttpPost()]
        public void SaveLjvAtendimentoVendedorInCache(SaveInformation<BusinessNS.LjvAtendimentoVendedor> saveInfo)
        {
            saveInfo.Validate();
            QueueTransaction.SaveTransaction(saveInfo, System.Reflection.Assembly.GetExecutingAssembly().FullName, this.ControllerContext, "SaveLjvAtendimentoVendedor");
        }
        private List<BusinessNS.LjvAtendimentoVendedor> SaveLjvAtendimentoVendedor__ForMEF(string jsonString, string viewMapInfo, List<ChangeTracker> changes)
        {
            List<BusinessNS.LjvAtendimentoVendedor> dataList = SerializationManager<List<BusinessNS.LjvAtendimentoVendedor>>.JsonToObject(jsonString);
            if (!viewMapInfo.IsNullOrEmpty() && changes.Count > 0)
            {
                var viewMap = ViewMapHelper.Parse(viewMapInfo);
                if(changes.Any(c => c.ComponentName == viewMap.ParentUIView))
                    dataList = viewMap.ReplaceEntities(dataList, changes.First(c => c.ComponentName == viewMap.ParentUIView).ListReturnedObjects);
            }
            return SaveLjvAtendimentoVendedor(dataList);
        }
        
        
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
        [Route("CancelAllChanges"), System.Web.Http.HttpGet()]
        public void CancelAllChanges(Guid transactionID)
        {
            var obj = QueueTransaction.GetTransaction(transactionID);
            if (!obj.IsNull())
                obj.DeleteCache();
        }
        #endregion
    }
    
    public partial class LinxDashboardFinanceiroFeedController : ODataController
    {
        private BusinessNS.DashboardFinanceiroDomainService _context;
        public BusinessNS.DashboardFinanceiroDomainService Context { get {  if (_context == null) { _context = new BusinessNS.DashboardFinanceiroDomainService(); _context.IsSecure = true; } return _context; }  }
        
        #region Get Action to Business Entities
        
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.LjvAtendimento> GetLjvAtendimentoById([FromODataUri]System.Guid key0)
        {
               return default(IQueryable<BusinessNS.LjvAtendimento>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.LjvAtendimento> GetLjvAtendimentoByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetLjvAtendimentoByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LjvAtendimento), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.LjvAtendimento>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.LjvAtendimento> GetLjvAtendimento()
        {
            return this.Context.GetLjvAtendimentoByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.LjvAtendimentoVendedor> GetLjvAtendimentoVendedorById([FromODataUri]System.Guid key0)
        {
               return default(IQueryable<BusinessNS.LjvAtendimentoVendedor>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.LjvAtendimentoVendedor> GetLjvAtendimentoVendedorByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                var entity = this.Context.GetLjvAtendimentoVendedorByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.LjvAtendimentoVendedor), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.LjvAtendimentoVendedor>);
        }
        
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.LjvAtendimentoVendedor> GetLjvAtendimentoVendedor()
        {
            return this.Context.GetLjvAtendimentoVendedorByEntitySearchNoAssociations(null).AsQueryable();
        }
        #endregion
        
    }
    
    public partial class LinxDashboardFinanceiroControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return true;
        }
    }
}

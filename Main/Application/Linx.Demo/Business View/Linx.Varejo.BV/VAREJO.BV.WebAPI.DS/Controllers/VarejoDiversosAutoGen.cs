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
using BusinessNS = VAREJO.BV.Diversos;

namespace VAREJO.BV.WebAPI.DS.Controllers
{
    
    //Examples:
    // Default Call: http://localhost:1710/VarejoDiversos/[ActionName]
    // Security Information Call: http://localhost:1710/VarejoDiversos/GetSecurityInfo
    // Entities Catalog Call: http://localhost:1710/VarejoDiversos/GetEntities
    // Entity MetaData Call: http://localhost:1710/VarejoDiversos/GetMetaData?entityName=[EntityName]&allComposition=false
    // Client Domains Call: http://localhost:1710/VarejoDiversos/GetClientDomains?erp=true
    // Client Service Call: http://localhost:1710/VarejoDiversos/GetClientService?erp=true
    // Client Factory Call: http://localhost:1710/VarejoDiversos/GetClientFactory?entityName=[EntityName]&erp=true
    // Client Factory Custom Events Call: http://localhost:1710/VarejoDiversos/GetClientFactoryCustomEvents?entityName=[EntityName]&erp=true
    // Help Call: http://localhost:1710/HelpController/VarejoDiversos
    // Feed OData Call: http://localhost:1710/VarejoDiversosOData
    [RoutePrefix("VarejoDiversos")]
    [Breeze.WebApi2.BreezeController]
    [ODataBasicAuthenticationFilter]
    public partial class VarejoDiversosController : ApiController
    {
        private const int maxObjectExcelReturned = 2300000;
        private DataServiceRepository<BusinessNS.DiversosDomainService> _repository = null;
        private DataServiceRepository<BusinessNS.DiversosDomainService> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS.DiversosDomainService>(typeof(BusinessNS.Venda), typeof(BusinessNS.VendaItem)); _repository.Context.IsSecure = true; } return _repository; } }
        public VarejoDiversosController()
        { }
        
        [Route("AssemblyInfo"), System.Web.Http.HttpGet()]
        public object AssemblyInfo()
        {
            return new
            {
                ApiAssemblyName = typeof(VarejoDiversosController).Assembly.FullName,
                BusinessAssemblyName = typeof(BusinessNS.DiversosDomainService).Assembly.FullName,
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
            var result = repository.Context.GetMetaDataObject("VAREJO.BV.Diversos." + entityName, false, true);
            return (allComposition ? result : result.Where(e => e.ClassName == entityName).ToList());
        }
        
        [Route("GetSecurityInfo"), System.Web.Http.HttpGet()]
        public string[] GetSecurityInfo()
        {
           return new string[] { "VAREJO.BV", "VarejoDiversos", "VarejoDiversos/ActionName" };
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
        
        [VarejoDiversosControllerAuthorize]
        [Route("GetAllLookUpCliente"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpCliente> GetAllLookUpCliente()
        {
            return repository.Context.GetAllLookUpCliente();
        }
        
        [VarejoDiversosControllerAuthorize]
        [Route("GetLookUpClienteByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpCliente> GetLookUpClienteByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpClienteByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
        }
        
        [VarejoDiversosControllerAuthorize]
        [Route("GetAllLookUpLoja"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpLoja> GetAllLookUpLoja()
        {
            return repository.Context.GetAllLookUpLoja();
        }
        
        [VarejoDiversosControllerAuthorize]
        [Route("GetLookUpLojaByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.LookUpLoja> GetLookUpLojaByEntitySearch(string propertyName, string jEntitySearch)
        {
            return repository.Context.GetLookUpLojaByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true));
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
        
        [VarejoDiversosControllerAuthorize]
        [Route("GetVenda"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Venda> GetVenda()
        {
            return repository.Context.GetVenda();
        }
        
        [VarejoDiversosControllerAuthorize]
        [Route("GetVendaNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Venda> GetVendaNoAssociations()
        {
            return repository.Context.GetVendaNoAssociations();
        }
        
        [VarejoDiversosControllerAuthorize]
        [Route("GetVendaByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Venda> GetVendaByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetVendaByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Venda), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [VarejoDiversosControllerAuthorize]
        [Route("GetVendaByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.Venda> GetVendaByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetVendaByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Venda), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetVendaToExcel"), System.Web.Http.HttpPost()]
        [VarejoDiversosControllerAuthorize]
        public string GetVendaToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.Venda), jEntitySearch, false, false, false);
            var entities = repository.Context.GetVendaByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdVenda asc");
            var metadata = repository.Context.GetMetaDataObject("VAREJO.BV.Diversos.Venda");
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
        [VarejoDiversosControllerAuthorize]
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
            var metadata = repository.Context.GetMetaDataObject("VAREJO.BV.Diversos.Venda", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "VAREJO.BV.Reports", DataSourceFullName = "VAREJO.BV.Reports.DiversosDataSource", DataSourceObject = "GetVenda", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/VAREJO.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/VAREJO.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [VarejoDiversosControllerAuthorize]
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
        
        [VarejoDiversosControllerAuthorize]
        [Route("GetVendaItem"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaItem> GetVendaItem()
        {
            return repository.Context.GetVendaItem();
        }
        
        [VarejoDiversosControllerAuthorize]
        [Route("GetVendaItemNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaItem> GetVendaItemNoAssociations()
        {
            return repository.Context.GetVendaItemNoAssociations();
        }
        
        [VarejoDiversosControllerAuthorize]
        [Route("GetVendaItemByEntitySearch"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaItem> GetVendaItemByEntitySearch(string jEntitySearch)
        {
            return repository.Context.GetVendaItemByEntitySearch(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaItem), jEntitySearch, false, false, false), jEntitySearch);
        }
        
        [VarejoDiversosControllerAuthorize]
        [Route("GetVendaItemByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaItem> GetVendaItemByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetVendaItemByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaItem), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetVendaItemToExcel"), System.Web.Http.HttpPost()]
        [VarejoDiversosControllerAuthorize]
        public string GetVendaItemToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaItem), jEntitySearch, false, false, false);
            var entities = repository.Context.GetVendaItemByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdVendaItem asc");
            var metadata = repository.Context.GetMetaDataObject("VAREJO.BV.Diversos.VendaItem");
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
        [VarejoDiversosControllerAuthorize]
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
            var metadata = repository.Context.GetMetaDataObject("VAREJO.BV.Diversos.VendaItem", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "VAREJO.BV.Reports", DataSourceFullName = "VAREJO.BV.Reports.DiversosDataSource", DataSourceObject = "GetVendaItem", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/VAREJO.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/VAREJO.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [VarejoDiversosControllerAuthorize]
        [Route("GetSampleVendaItem"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaItem> GetSampleVendaItem(string details)
        {
            var result = repository.Context.GetVendaItemByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        #endregion
        
        #region Get Business Entities By Parent Composition
        
        [VarejoDiversosControllerAuthorize]
        [Route("GetVendaItemParentCompositionByEntitySearchNoAssociations"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaItemParentComposition> GetVendaItemParentCompositionByEntitySearchNoAssociations(string jEntitySearch)
        {
            return repository.Context.GetVendaItemParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaItemParentComposition), jEntitySearch, false, false, false), jEntitySearch);
        }
        [Route("GetVendaItemParentCompositionToExcel"), System.Web.Http.HttpPost()]
        [VarejoDiversosControllerAuthorize]
        public string GetVendaItemParentCompositionToExcel(string[] parameters)
        {
            string jEntitySearch = parameters[0];
            string translatedJEntitySearch = parameters[1];
            string columnsDefinition = parameters[2];
            var columns = StringExtension.ConvertToDictionary(columnsDefinition);
            jEntitySearch = jEntitySearch.Replace("VendaItem{", "VendaItemParentComposition{");
            jEntitySearch = jEntitySearch.Replace("Venda{", "VendaItemParentComposition{");
            var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaItemParentComposition), jEntitySearch, false, false, false);
            var entities = repository.Context.GetVendaItemParentCompositionByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch).OrderBy("IdVendaItem asc");
            var metadata = repository.Context.GetMetaDataObject("VAREJO.BV.Diversos.VendaItem");
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
        [VarejoDiversosControllerAuthorize]
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
            var metadata = repository.Context.GetMetaDataObject("VAREJO.BV.Diversos.VendaItem", true);
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
            zip.AddStringContent(reportName + ".trdx", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = "VAREJO.BV.Reports", DataSourceFullName = "VAREJO.BV.Reports.DiversosDataSource", DataSourceObject = "GetVendaItemParentComposition", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/VAREJO.BV.Reports.dll"));
            zip.AddFile(System.Web.HttpContext.Current.Server.MapPath("~/bin/VAREJO.BV.dll"));
            return Convert.ToBase64String(zip.GetZipBytes());
        }
        
        [VarejoDiversosControllerAuthorize]
        [Route("GetSampleVendaItemParentComposition"), System.Web.Http.HttpGet()]
        public IQueryable<BusinessNS.VendaItemParentComposition> GetSampleVendaItemParentComposition(string details)
        {
            var result = repository.Context.GetVendaItemParentCompositionByEntitySearchNoAssociations(null).Take(100).ToList();
            return result.AsQueryable();
        }
        #endregion
        
        #region Save Changes
        [VarejoDiversosControllerAuthorize]
        [Route("SaveChanges"), System.Web.Http.HttpPost()]
        public SaveResult SaveChanges(JObject saveBundle)
        {
            var result = repository.SaveChanges(saveBundle);
            repository.Context.Dispose();
            return result;
        }
        [VarejoDiversosControllerAuthorize]
        [Route("SaveVenda"), System.Web.Http.HttpPost()]
        public List<BusinessNS.Venda> SaveVenda(List<BusinessNS.Venda> dataList)
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
        [VarejoDiversosControllerAuthorize]
        [Route("SaveVendaInCache"), System.Web.Http.HttpPost()]
        public void SaveVendaInCache(SaveInformation<BusinessNS.Venda> saveInfo)
        {
            saveInfo.Validate();
            QueueTransaction.SaveTransaction(saveInfo, System.Reflection.Assembly.GetExecutingAssembly().FullName, this.ControllerContext, "SaveVenda");
        }
        [VarejoDiversosControllerAuthorize]
        public List<BusinessNS.Venda> SaveVenda__ForMEF(string jsonString, string viewMapInfo, List<ChangeTracker> changes)
        {
            List<BusinessNS.Venda> dataList = SerializationManager<List<BusinessNS.Venda>>.JsonToObject(jsonString);
            if (!viewMapInfo.IsNullOrEmpty() && changes.Count > 0)
            {
                var viewMap = ViewMapHelper.Parse(viewMapInfo);
                if(changes.Any(c => c.ComponentName == viewMap.ParentUIView))
                    dataList = viewMap.ReplaceEntities(dataList, changes.First(c => c.ComponentName == viewMap.ParentUIView).ListReturnedObjects);
            }
            return SaveVenda(dataList);
        }
        
        
        [VarejoDiversosControllerAuthorize]
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
        [VarejoDiversosControllerAuthorize]
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
    public partial class VarejoDiversosFeedController : ODataController
    {
        private BusinessNS.DiversosDomainService _context;
        public BusinessNS.DiversosDomainService Context { get {  if (_context == null) { _context = new BusinessNS.DiversosDomainService(); _context.IsSecure = true; } return _context; }  }
        
        #region Get Action to Business Entities
        
        
        [VarejoDiversosControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Venda> GetVendaById([FromODataUri]Int32 key0)
        {
            var entity = this.Context.GetVendaByKey(key0);
            if (entity != null)
               return (new BusinessNS.Venda[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.Venda>);
        }
        
        [VarejoDiversosControllerAuthorize]
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
        
        [VarejoDiversosControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.Venda> GetVenda()
        {
            return this.Context.GetVendaByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [VarejoDiversosControllerAuthorize]
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
        
        [VarejoDiversosControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaItem> GetVendaItemById([FromODataUri]Int32 key0)
        {
            var entity = this.Context.GetVendaItemByKey(key0);
            if (entity != null)
               return (new BusinessNS.VendaItem[] { entity }).AsQueryable();
            else
               return default(IQueryable<BusinessNS.VendaItem>);
        }
        
        [VarejoDiversosControllerAuthorize]
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
        
        [VarejoDiversosControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaItem> GetVendaItem()
        {
            return this.Context.GetVendaItemByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [VarejoDiversosControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaItemParentComposition> GetVendaItemParentComposition()
        {
            return this.Context.GetVendaItemParentCompositionByEntitySearchNoAssociations(null).AsQueryable();
        }
        
        [VarejoDiversosControllerAuthorize]
        [EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]
        public IQueryable<BusinessNS.VendaItemParentComposition> GetVendaItemParentCompositionByEntitySearch([FromODataUri]String jEntitySearch)
        {
            if (!jEntitySearch.IsNullOrEmpty())
            {
                jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));
                jEntitySearch = jEntitySearch.Replace("VendaItem{", "VendaItemParentComposition{");
                jEntitySearch = jEntitySearch.Replace("Venda{", "VendaItemParentComposition{");
                var entity = this.Context.GetVendaItemParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS.VendaItemParentComposition), jEntitySearch, false, false, false), jEntitySearch);
                if (entity != null) return entity.AsQueryable();
            }
            return default(IQueryable<BusinessNS.VendaItemParentComposition>);
        }
        
        [VarejoDiversosControllerAuthorize]
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
        #endregion
        
    }
    
    public partial class VarejoDiversosControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return LinxAutorization.CheckAuthorization(actionContext, string.Format("{0}#{1}#{1}/{2}", "VAREJO.BV", "VarejoDiversos", actionContext.ActionDescriptor.ActionName));
        }
    }
}

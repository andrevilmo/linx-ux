using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Linx.Tools;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Composition;
using System.Net;
using System.Net.Http;
using System.Web.Http;


using Linx.Report.Access.BV.ReportAccess;
using Linx.Report.Access.BV.ReportingServicesSvc;
using System.Web;
using System.Text;
using System.IO;

namespace Linx.Report.Access.BV.WebAPI.DS.Controllers
{

    ////////////////////////////////////////////////////////////////////////////
    /////////////////////////// Business Api Controller ////////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class LinxReportAccessReportAccessController
    {
        private static ReportingServicesSvc.ReportingService2010 rsAccess;
        private string ReportingServicesBaseUrl { get; set; }

        private void CreateInstance()
        {
            if (ReportingServicesBaseUrl.IsNullOrEmpty())
            {
                ReportingServicesBaseUrl = Linx.Business.Tools.LinxParameters.GetParameter<string>("REPORTING_SERVICES_URL", null);
                ReportingServicesBaseUrl = ReportingServicesBaseUrl.Right(1) == "/" ? ReportingServicesBaseUrl : ReportingServicesBaseUrl + "/";
            }

            if (rsAccess.IsNull())
            {
                rsAccess = new ReportingServicesSvc.ReportingService2010();
                rsAccess.Credentials = System.Net.CredentialCache.DefaultCredentials;
                rsAccess.Url = string.Format("{0}ReportServer/ReportService2010.asmx", ReportingServicesBaseUrl);
            }
        }

        private bool CheckFolderExistence(string controller)
        {
            CreateInstance();

            SearchCondition condition = new SearchCondition();
            condition.Condition = ConditionEnum.Equals;
            condition.ConditionSpecified = true;
            condition.Name = "Name";
            condition.Values = new string[] { controller };
            SearchCondition[] conditions = new SearchCondition[1];
            conditions[0] = condition;

            ReportingServicesSvc.CatalogItem[] items = rsAccess.FindItems("/", BooleanOperatorEnum.And, new Property[] { new Property() { Name = "Recursive", Value = "True" } }, conditions);

            return items.Count() > 0;
        }

        [Route("GetReportingServicesList"), System.Web.Http.HttpGet(), LinxReportAccessReportAccessControllerAuthorize()]
        public List<RelatorioInfo> GetReportingServicesList(string controller)
        {
            CreateInstance();
            List<RelatorioInfo> relatorios = new List<RelatorioInfo>();

            if (!controller.IsNullOrEmpty() && CheckFolderExistence(controller))
            {
                ReportingServicesSvc.CatalogItem[] info = rsAccess.ListChildren(string.Format("/{0}", controller), true);

                foreach (ReportingServicesSvc.CatalogItem item in info)
                {
                    relatorios.Add(new RelatorioInfo() { IdRelatorio = item.ID, NomeRelatorio = item.Name, DescricaoRelatorio = item.Description, CaminhoRelatorio = item.Path });
                }
            }

            return relatorios;
        }

        [Route("GetReportingServicesFolder"), System.Web.Http.HttpGet(), LinxReportAccessReportAccessControllerAuthorize()]
        public string GetReportingServicesFolder(string controller)
        {
            string folderUrl = null;
            CreateInstance();

            if (!controller.IsNullOrEmpty() && CheckFolderExistence(controller))
            {
                folderUrl = string.Format("{0}Reports/Pages/Folder.aspx?ItemPath=/{1}&ViewMode=List", ReportingServicesBaseUrl, controller);
            }

            return folderUrl;
        }

        [Route("GetTelerikReportsList"), System.Web.Http.HttpGet()]
        public List<RelatorioInfo> GetTelerikReportsList(string controller, string masterEntity, string businessAssembly)
        {
            List<RelatorioInfo> reportList = GetTelerikReports().ReportList;

            string reportName = string.Format("{0}.Reports.{1}{2}", businessAssembly, controller, masterEntity);

            reportList = reportList.Where(i => i.IdRelatorio.Contains(reportName)).ToList();

            return reportList;
        }

        [Route("GetTelerikReportsFullList"), System.Web.Http.HttpGet()]
        public List<RelatorioInfo> GetTelerikReportsFullList(string cacheHash)
        {
            List<RelatorioInfo> reportList;

            TelerikReportsList fullReportList = GetTelerikReports();

            if (cacheHash.IsNullOrEmpty() || cacheHash != fullReportList.Hash)
                reportList = fullReportList.ReportList;
            else
                reportList = new List<RelatorioInfo>();

            if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.Response != null)
                System.Web.HttpContext.Current.Response.AddHeader("cacheHash", fullReportList.Hash);

            return reportList;
        }
        
        private string FormatName(string name)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < name.Length; i++)
            {
                char c = name[i];
                if (i == 0)
                {
                    c = Char.ToUpper(c);
                }
                else if (Char.IsUpper(c))
                {
                    sb.Append(" ");
                }
                sb.Append(c);
            }
            return sb.ToString();
        }
        
        private TelerikReportsList GetTelerikReports()
        {
            TelerikReportsList reportList = WebCacheHelper.GetWebCache<TelerikReportsList>("TelerikReportsList");

            if (reportList.IsNullOrEmpty())
            {
                //dlls
                reportList = new TelerikReportsList() { Hash = Guid.NewGuid().ToString(), ReportList = new List<RelatorioInfo>() };

                var directory = String.Empty;
                try
                {
                    directory = HttpRuntime.BinDirectory;
                }
                catch
                {
                    directory = AssemblyHelper.GetCurrentAssemblyDirectory<TelerikReportsList>();
                }

                if (!directory.IsNullOrEmpty() && System.IO.Directory.Exists(directory))
                {
                    var files = System.IO.Directory.GetFiles(directory, "*.reports.dll");

                    foreach (string file in files)
                    {
                        try
                        {
                            var reportAssembly = AssemblyHelper.Load(file);
                            List<Type> types = reportAssembly.GetTypes().Where(i => typeof(Telerik.Reporting.IReportDocument).IsAssignableFrom(i) && !i.IsAbstract).ToList();

                            for (int i = 0; i < types.Count; i++)
                            {
                                string description = string.Empty;
                                object[] attributes = null;

                                attributes = types[i].GetCustomAttributes(typeof(DescriptionAttribute), false);
                                if (attributes.Length > 0)
                                {
                                    description = ((DescriptionAttribute)attributes[0]).Description;
                                }
                                else if (types[i].FullName.Contains("Detail"))
                                {
                                    continue;
                                }
                                reportList.ReportList.Add(new RelatorioInfo() { IdRelatorio = types[i].AssemblyQualifiedName, NomeRelatorio = types[i].Name, DescricaoRelatorio = description, CaminhoRelatorio = types[i].AssemblyQualifiedName });
                            }
                        }
                        catch { }
                    }

                    files = System.IO.Directory.GetFiles(directory, "*.trdx");
                    foreach (string file in files)
                    {
                        try
                        {
                            var doc = new System.Xml.XmlDocument();
                            doc.Load(file);
                            var ns = new System.Xml.XmlNamespaceManager(doc.NameTable);
                            ns.AddNamespace("ns", doc.DocumentElement.NamespaceURI);


                            //Get Report Title
                            var node = doc.DocumentElement.SelectSingleNode("ns:Items/ns:PageHeaderSection/ns:Items/ns:TextBox[@Name='TextBoxHeader']/@Value", ns);
                            string reportTitle = node != null ? node.Value : new FileInfo(file).Name.Replace(".trdx", "");

                            //Getting Data Source
                            var dataMember = doc.DocumentElement.SelectSingleNode("ns:DataSources/ns:ObjectDataSource/@DataMember", ns).Value;
                            dataMember = dataMember.Right("Get");

                            var nameSpace = doc.DocumentElement.SelectSingleNode("ns:DataSources/ns:ObjectDataSource/ns:DataSource/ns:ClrType/@FullName", ns).Value;
                            nameSpace = nameSpace.Left("DataSource").Replace(".Reports.", ".");

                            string key = System.IO.Path.GetFileName(file);

                            reportList.ReportList.Add(new RelatorioInfo() { IdRelatorio = string.Format("{0}{1}", HttpContext.Current.IsNull() ? "" : @"bin/", key), NomeRelatorio = reportTitle + "." + nameSpace + "." + dataMember, DescricaoRelatorio = reportTitle, CaminhoRelatorio = string.Format("{0}{1}", HttpContext.Current.IsNull() ? "" : @"bin/", key) });

                        }
                        catch (Exception ex) { throw new Exception(string.Format("Não foi possível ler o relatório [{0}]", file), ex); }
                    }

                }

                ////.trdx
                //var reportsTrdx = Linx.Tools.ReportHelper.GetXmlReports();
                //reportsTrdx.Foreach(repo =>
                //{
                //    string descReport = repo.Value.Left("::");
                //    string nomeReport = repo.Value.Replace("::", ".");
                //    reportList.ReportList.Add(new RelatorioInfo() { IdRelatorio = string.Format(@"bin\{0}", repo.Key), NomeRelatorio = nomeReport, DescricaoRelatorio = descReport, CaminhoRelatorio = string.Format(@"bin\{0}", repo.Key) });
                //});

                if (LocalServiceBus.Enabled)
                {
                    var reports = LocalServiceBus.ReportList;
                    reportList.ReportList.AddRange(reports);
                }

                reportList.ReportList = reportList.ReportList.OrderBy(i => i.NomeRelatorio).ThenBy(i => i.DescricaoRelatorio).ToList();
                WebCacheHelper.AddWebCache("TelerikReportsList", reportList, 720); //30 dias
            }

            return reportList;
        }


        [Route("CleanTelerikReportsCache"), System.Web.Http.HttpGet()]
        public void CleanTelerikReportsCache()
        {
            this.repository.Context.CleanTelerikReportsCache();
        }

    }
}

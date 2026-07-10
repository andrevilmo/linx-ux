using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Telerik.Reporting.Services.WebApi;
using Linx.Tools;
using System.IO;
using System.Reflection;

namespace Linx.Report.Access.BV.TelerikReport.WebAPI.DS.Controllers
{
    public class LinxReportAccessBVTelerikReportController : ReportsControllerBase
    {
        //protected override Telerik.Reporting.Cache.Interfaces.ICache CreateCache()
        //{
        //    return Telerik.Reporting.Services.Engine.CacheFactory.CreateFileCache();
        //}

        //http://localhost:1710/api/LinxReportAccessBVTelerikReport/formats

        protected override Telerik.Reporting.Cache.Interfaces.IStorage CreateStorage()
        {
            if (Linx.Tools.LocalServiceBus.Enabled)
                return new Telerik.Reporting.Cache.CacheStorage(Telerik.Reporting.Services.Engine.CacheFactory.CreateFileCache());
            else
                return new Telerik.Reporting.Cache.MsSqlServerStorage(System.Configuration.ConfigurationManager.ConnectionStrings["TelerikCacheStorage"].ToString());
        }


        protected override Telerik.Reporting.Services.Engine.IReportResolver CreateReportResolver()
        {
            var reportsPath = "";
            if (!HttpContext.Current.IsNull())
                reportsPath = HttpContext.Current.Server.MapPath("~/");
            else if (LinxHttpContext.HttpContext.Current != null)
                reportsPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            return new ReportFileResolver(reportsPath).AddFallbackResolver(new ReportTypeResolver());
        }
    }
}

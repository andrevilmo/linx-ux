﻿using StackExchange.Profiling;
using StackExchange.Profiling.EntityFramework6;
using StackExchange.Profiling.Storage;
using System;
using System.Configuration;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Linx.Internet.Application
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private bool MiniProfilerEnabled = ConfigurationManager.AppSettings.GetValue<bool>("Shell.MiniProfiler.Enabled", false);

        protected void Application_Start()
        {
            if (MiniProfilerEnabled)
            {
                MiniProfiler.Settings.IgnoredPaths = new string[] { "/content/", "/scripts/", "/favicon.ico", "/lib/", "/App/", "/signalr/", "/linx-internet-application/" };
                MiniProfiler.Settings.StackMaxLength = 200;
                MiniProfiler.Settings.ShowControls = true;
                MiniProfiler.Settings.PopupShowTrivial = true;
                MiniProfiler.Settings.PopupMaxTracesToShow = 30;
                //MiniProfiler.Settings.SqlFormatter = new StackExchange.Profiling.SqlFormatters.VerboseSqlServerFormatter(true);
                //MiniProfiler.Settings.SqlFormatter = new StackExchange.Profiling.SqlFormatters.SqlServerFormatter();
                //MiniProfiler.Settings.SqlFormatter = new StackExchange.Profiling.SqlFormatters.InlineFormatter(true);

                WebRequestProfilerProvider.Settings.UserProvider = new Linx.Internet.Application.Class.MiniProfilerCustomUser();

                //var sql = SqlServerStorage.TableCreationScript;

                MiniProfiler.Settings.Results_List_Authorize = (request) =>
                {
                    return true; // all requests
                };

                if (ConfigurationManager.ConnectionStrings["MiniProfiler"] != null)
                {
                    MiniProfiler.Settings.Storage = new SqlServerStorage(ConfigurationManager.ConnectionStrings["MiniProfiler"]);
                }

                MiniProfilerEF6.Initialize();
            }
        }

        protected void Application_BeginRequest()
        {
            if (MiniProfilerEnabled)
            {
                if (!System.Web.HttpContext.Current.Request.QueryStringExistsValue("tracemode", "on", "1", "true"))
                    return;

                if ((new Linx.Internet.Application.Class.MiniProfilerCustomUser()).GetUser(System.Web.HttpContext.Current.Request) == "anonymous")
                    return;

                if (Request.HttpMethod.Equals("GET", StringComparison.InvariantCultureIgnoreCase) || Request.HttpMethod.Equals("POST", StringComparison.InvariantCultureIgnoreCase))
                {
                    MiniProfiler.Start(string.Concat("[UI] ", Request.Url.PathAndQuery));
                }
            }
        }

        protected void Application_EndRequest()
        {
            if (MiniProfilerEnabled)
            {
                MiniProfiler.Stop();
            }
        }

        public override string GetVaryByCustomString(HttpContext context, string custom)
        {
            if (custom == "ShellMode")
            {
                return Linx.Internet.Application.Helpers.BaseHelpers.GetShellMode();
            }

            return base.GetVaryByCustomString(context, custom);
        }
    }
}
using Linx.Internet.Application.Framework.Web;
using NLog;
using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

[assembly: WebActivator.PostApplicationStartMethod(typeof(Linx.Internet.Application.App_Start.LIAConfig), "PostStart", Order = 1)]

namespace Linx.Internet.Application.App_Start
{
    public static class LIAConfig
    {
        public static readonly Logger Logger = LogManager.GetCurrentClassLogger(); 
        
        private static PluginConfig plugins = null;
        public static void PostStart()
        {
            Logger.Info("Inicio da configuração dos modulos (SPAs)");

            MvcHandler.DisableMvcResponseHeader = true;
            plugins = new PluginConfig();
            plugins.RegisterPlugin();

            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
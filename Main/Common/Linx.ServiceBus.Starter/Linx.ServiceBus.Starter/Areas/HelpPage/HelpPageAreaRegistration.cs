using System.Web.Http;
using System.Web.Mvc;

namespace Linx.ServiceBus.Starter.Areas.HelpPage
{
    public class HelpPageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "HelpPage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "HelpPage_Default",
                "HelpPage/{action}/{apiId}",
                new { controller = "Help", action = "Index", apiId = UrlParameter.Optional });

            context.MapRoute(
                "HelpAssembly_Default",
                "HelpAssembly/{assemblyName}",
                new { controller = "Help", action = "IndexByAssembly", assemblyName = UrlParameter.Optional });

            context.MapRoute(
                "HelpController_Default",
                "HelpController/{controllerName}",
                new { controller = "Help", action = "IndexByController", controllerName = UrlParameter.Optional });

            HelpPageConfig.Register(GlobalConfiguration.Configuration);
        }
    }
}
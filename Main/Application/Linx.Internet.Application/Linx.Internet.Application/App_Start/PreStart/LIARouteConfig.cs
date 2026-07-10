using StackExchange.Profiling.Mvc;
using System.Web.Mvc;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Linx.Internet.Application.App_Start.LIARouteConfig), "PreStart", Order = 2)]

namespace Linx.Internet.Application.App_Start
{
    ///<summary>
    /// Inserts the LIA SPA sample view controller to the front of all MVC routes
    /// so that the LIA SPA sample becomes the default page.
    ///</summary>
    ///<remarks>
    /// This class is discovered and run during startup
    /// http://blogs.msdn.com/b/davidebb/archive/2010/10/11/light-up-your-nupacks-with-startup-code-and-webactivator.aspx
    ///</remarks>
    public static class LIARouteConfig
    {

        public static void PreStart()
        {
            System.Web.Routing.RouteTable.Routes.IgnoreRoute("{*staticfile}", new { staticfile = @".*\.(css|js|gif|jpg|png|html|hml|ico)(/.*)?" });
            System.Web.Routing.RouteTable.Routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });
            System.Web.Routing.RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            System.Web.Routing.RouteTable.Routes.IgnoreRoute("favicon.ico");

            //System.Web.Routing.RouteTable.Routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);

            // Preempt standard default MVC page routing to go to LIA Sample
            System.Web.Routing.RouteTable.Routes.MapRoute(
                name: "LIAMvc",
                url: "{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "LIA",
                    action = "Loading",
                    id = UrlParameter.Optional
                }
            );


            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            var copy = ViewEngines.Engines.ToList<IViewEngine>();
            ViewEngines.Engines.Clear();
            foreach (var item in copy)
            {
                ViewEngines.Engines.Add(new ProfilingViewEngine(item));
            }

            //ViewEngines.Engines.Add(new Linx.Internet.Application.Framework.MyRazorViewEngine());
        }
    }
}
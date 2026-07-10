using System.Web.Routing;
using AttributeRouting.Web.Mvc;
using System.Reflection;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Linx.Internet.Application.AttributeRoutingConfig), "PreStart", Order = 1)]

namespace Linx.Internet.Application 
{
    public static class AttributeRoutingConfig
	{
		public static void RegisterRoutes(RouteCollection routes) 
		{    
			// See http://github.com/mccalltd/AttributeRouting/wiki for more options.
			// To debug routes locally using the built in ASP.NET development server, go to /routes.axd

            routes.MapAttributeRoutes(config =>
            {
                config.AddRoutesFromAssembly(Assembly.GetExecutingAssembly());
                config.AutoGenerateRouteNames = true;
                config.UseLowercaseRoutes = true;
            });
        }

        public static void PreStart() 
		{
            RegisterRoutes(RouteTable.Routes);
        }
    }
}

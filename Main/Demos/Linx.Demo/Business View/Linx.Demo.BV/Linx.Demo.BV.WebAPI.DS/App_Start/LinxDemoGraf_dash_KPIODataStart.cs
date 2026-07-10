using Microsoft.Data.Edm;
using Newtonsoft.Json.Serialization;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using System.Web.Http.OData.Routing;
using System.Web.Http.OData.Routing.Conventions;
using System.Web.Routing;
using BusinessNS = Linx.Demo.BV.Graf_dash_KPI;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Linx.Demo.BV.WebAPI.DS.App_Start.LinxDemoGraf_dash_KPIODataStart), "Start")]

namespace Linx.Demo.BV.WebAPI.DS.App_Start
{

    public static class LinxDemoGraf_dash_KPIODataStart
    {
        public static void Start()
        {
           var conventions = ODataRoutingConventions.CreateDefault();
           conventions.Insert(0, new Linx.DataService.ODataContainmentRoutingConvention("LinxDemoGraf_dash_KPIFeed"));
           GlobalConfiguration.Configuration.Routes.MapODataServiceRoute(
               routeName: "LinxDemoGraf_dash_KPIODataRoute",
               routePrefix: "LinxDemoGraf_dash_KPIOData",
               model: GetEdmModel(), pathHandler: new DefaultODataPathHandler(), routingConventions: conventions
               );
        }
        
        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<BusinessNS.Produto>("Produto");
            return modelBuilder.GetEdmModel();
        }
    }
}

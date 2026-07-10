using System.Web.Http;
using System.Web;
using System.Web.Routing;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Linq;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using System.Web.Http.OData.Routing.Conventions;
using System.Web.Http.OData.Routing;
using Microsoft.Data.Edm;
using BusinessNS = Linx.Dashboard.DashboardFinanceiro;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Linx.Dashboard.WebAPI.DS.App_Start.LinxDashboardFinanceiroODataStart), "Start")]

namespace Linx.Dashboard.WebAPI.DS.App_Start
{

    public static class LinxDashboardFinanceiroODataStart
    {
        public static void Start()
        {
           var conventions = ODataRoutingConventions.CreateDefault();
           conventions.Insert(0, new Linx.DataService.ODataContainmentRoutingConvention("LinxDashboardFinanceiroFeed"));
           GlobalConfiguration.Configuration.Routes.MapODataServiceRoute(
               routeName: "LinxDashboardFinanceiroODataRoute",
               routePrefix: "LinxDashboardFinanceiroOData",
               model: GetEdmModel(), pathHandler: new DefaultODataPathHandler(), routingConventions: conventions
               );
        }
        
        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<BusinessNS.LjvAtendimento>("LjvAtendimento");
            modelBuilder.EntitySet<BusinessNS.LjvAtendimentoVendedor>("LjvAtendimentoVendedor");
            return modelBuilder.GetEdmModel();
        }
    }
}

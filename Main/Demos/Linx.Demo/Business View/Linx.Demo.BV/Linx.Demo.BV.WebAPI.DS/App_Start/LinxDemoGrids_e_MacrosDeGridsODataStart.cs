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
using BusinessNS = Linx.Demo.BV.Grids_e_MacrosDeGrids;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Linx.Demo.BV.WebAPI.DS.App_Start.LinxDemoGrids_e_MacrosDeGridsODataStart), "Start")]

namespace Linx.Demo.BV.WebAPI.DS.App_Start
{

    public static class LinxDemoGrids_e_MacrosDeGridsODataStart
    {
        public static void Start()
        {
           var conventions = ODataRoutingConventions.CreateDefault();
           conventions.Insert(0, new Linx.DataService.ODataContainmentRoutingConvention("LinxDemoGrids_e_MacrosDeGridsFeed"));
           GlobalConfiguration.Configuration.Routes.MapODataServiceRoute(
               routeName: "LinxDemoGrids_e_MacrosDeGridsODataRoute",
               routePrefix: "LinxDemoGrids_e_MacrosDeGridsOData",
               model: GetEdmModel(), pathHandler: new DefaultODataPathHandler(), routingConventions: conventions
               );
        }
        
        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<BusinessNS.Venda>("Venda");
            modelBuilder.EntitySet<BusinessNS.VendaItem>("VendaItem");
            modelBuilder.EntitySet<BusinessNS.VendaItemParentComposition>("VendaItemParentComposition");
            return modelBuilder.GetEdmModel();
        }
    }
}

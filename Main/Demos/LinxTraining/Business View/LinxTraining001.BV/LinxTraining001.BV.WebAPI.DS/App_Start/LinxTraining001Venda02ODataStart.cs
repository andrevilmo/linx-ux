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
using BusinessNS = LinxTraining001.BV.Venda02;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(LinxTraining001.BV.WebAPI.DS.App_Start.LinxTraining001Venda02ODataStart), "Start")]

namespace LinxTraining001.BV.WebAPI.DS.App_Start
{

    public static class LinxTraining001Venda02ODataStart
    {
        public static void Start()
        {
           var conventions = ODataRoutingConventions.CreateDefault();
           conventions.Insert(0, new Linx.DataService.ODataContainmentRoutingConvention("LinxTraining001Venda02Feed"));
           GlobalConfiguration.Configuration.Routes.MapODataServiceRoute(
               routeName: "LinxTraining001Venda02ODataRoute",
               routePrefix: "LinxTraining001Venda02OData",
               model: GetEdmModel(), pathHandler: new DefaultODataPathHandler(), routingConventions: conventions
               );
        }
        
        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<BusinessNS.ClientesView>("ClientesView");
            modelBuilder.EntitySet<BusinessNS.VendasView>("VendasView");
            modelBuilder.EntitySet<BusinessNS.VendasViewParentComposition>("VendasViewParentComposition");
            return modelBuilder.GetEdmModel();
        }
    }
}

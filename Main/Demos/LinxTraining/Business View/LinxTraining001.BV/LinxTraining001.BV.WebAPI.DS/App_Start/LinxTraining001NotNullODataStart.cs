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
using BusinessNS = LinxTraining001.BV.NotNull;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(LinxTraining001.BV.WebAPI.DS.App_Start.LinxTraining001NotNullODataStart), "Start")]

namespace LinxTraining001.BV.WebAPI.DS.App_Start
{

    public static class LinxTraining001NotNullODataStart
    {
        public static void Start()
        {
           var conventions = ODataRoutingConventions.CreateDefault();
           conventions.Insert(0, new Linx.DataService.ODataContainmentRoutingConvention("LinxTraining001NotNullFeed"));
           GlobalConfiguration.Configuration.Routes.MapODataServiceRoute(
               routeName: "LinxTraining001NotNullODataRoute",
               routePrefix: "LinxTraining001NotNullOData",
               model: GetEdmModel(), pathHandler: new DefaultODataPathHandler(), routingConventions: conventions
               );
        }
        
        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<BusinessNS.TiposCamposView>("TiposCamposView");
            modelBuilder.EntitySet<BusinessNS.TiposCamposFilhaView>("TiposCamposFilhaView");
            modelBuilder.EntitySet<BusinessNS.TiposCamposFilhaViewParentComposition>("TiposCamposFilhaViewParentComposition");
            modelBuilder.EntitySet<BusinessNS.PaiNotNullView>("PaiNotNullView");
            modelBuilder.EntitySet<BusinessNS.FilhaNotNullView>("FilhaNotNullView");
            modelBuilder.EntitySet<BusinessNS.FilhaNotNullViewParentComposition>("FilhaNotNullViewParentComposition");
            return modelBuilder.GetEdmModel();
        }
    }
}

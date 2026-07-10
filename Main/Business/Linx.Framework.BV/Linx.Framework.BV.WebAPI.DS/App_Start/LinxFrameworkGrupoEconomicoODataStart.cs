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
using BusinessNS = Linx.Framework.BV.GrupoEconomico;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Linx.Framework.BV.WebAPI.DS.App_Start.LinxFrameworkGrupoEconomicoODataStart), "Start")]

namespace Linx.Framework.BV.WebAPI.DS.App_Start
{

    public static class LinxFrameworkGrupoEconomicoODataStart
    {
        public static void Start()
        {
           var conventions = ODataRoutingConventions.CreateDefault();
           conventions.Insert(0, new Linx.DataService.ODataContainmentRoutingConvention("LinxFrameworkGrupoEconomicoFeed"));
           GlobalConfiguration.Configuration.Routes.MapODataServiceRoute(
               routeName: "LinxFrameworkGrupoEconomicoODataRoute",
               routePrefix: "LinxFrameworkGrupoEconomicoOData",
               model: GetEdmModel(), pathHandler: new DefaultODataPathHandler(), routingConventions: conventions
               );
        }
        
        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<BusinessNS.TbcGrupoEconomico>("TbcGrupoEconomico");
            modelBuilder.EntitySet<BusinessNS.TcsUsuarioGpecon>("TcsUsuarioGpecon");
            modelBuilder.EntitySet<BusinessNS.TcsUsuarioGpeconParentComposition>("TcsUsuarioGpeconParentComposition");
            modelBuilder.EntitySet<BusinessNS.EconomicGroupView>("EconomicGroupView");
            return modelBuilder.GetEdmModel();
        }
    }
}

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
using BusinessNS = Linx.Framework.BV.Aplicativo;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Linx.Framework.BV.WebAPI.DS.App_Start.LinxFrameworkAplicativoODataStart), "Start")]

namespace Linx.Framework.BV.WebAPI.DS.App_Start
{

    public static class LinxFrameworkAplicativoODataStart
    {
        public static void Start()
        {
           var conventions = ODataRoutingConventions.CreateDefault();
           conventions.Insert(0, new Linx.DataService.ODataContainmentRoutingConvention("LinxFrameworkAplicativoFeed"));
           GlobalConfiguration.Configuration.Routes.MapODataServiceRoute(
               routeName: "LinxFrameworkAplicativoODataRoute",
               routePrefix: "LinxFrameworkAplicativoOData",
               model: GetEdmModel(), pathHandler: new DefaultODataPathHandler(), routingConventions: conventions
               );
        }
        
        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<BusinessNS.TcsAplicativo>("TcsAplicativo");
            modelBuilder.EntitySet<BusinessNS.TcsAplicativoConexao>("TcsAplicativoConexao");
            modelBuilder.EntitySet<BusinessNS.TcsAplicativoConexaoParentComposition>("TcsAplicativoConexaoParentComposition");
            modelBuilder.EntitySet<BusinessNS.TcsAplicacao>("TcsAplicacao");
            modelBuilder.EntitySet<BusinessNS.TcsAplicacaoParentComposition>("TcsAplicacaoParentComposition");
            return modelBuilder.GetEdmModel();
        }
    }
}

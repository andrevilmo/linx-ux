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
using BusinessNS = Linx.Framework.BV.ModuloLoja;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Linx.Framework.BV.WebAPI.DS.App_Start.LinxFrameworkModuloLojaODataStart), "Start")]

namespace Linx.Framework.BV.WebAPI.DS.App_Start
{

    public static class LinxFrameworkModuloLojaODataStart
    {
        public static void Start()
        {
           var conventions = ODataRoutingConventions.CreateDefault();
           conventions.Insert(0, new Linx.DataService.ODataContainmentRoutingConvention("LinxFrameworkModuloLojaFeed"));
           GlobalConfiguration.Configuration.Routes.MapODataServiceRoute(
               routeName: "LinxFrameworkModuloLojaODataRoute",
               routePrefix: "LinxFrameworkModuloLojaOData",
               model: GetEdmModel(), pathHandler: new DefaultODataPathHandler(), routingConventions: conventions
               );
        }
        
        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<BusinessNS.LjvModulo>("LjvModulo");
            modelBuilder.EntitySet<BusinessNS.LjvModuloMenu>("LjvModuloMenu");
            modelBuilder.EntitySet<BusinessNS.LjvModuloMenuParentComposition>("LjvModuloMenuParentComposition");
            modelBuilder.EntitySet<BusinessNS.LjvTransacaoMenu>("LjvTransacaoMenu");
            return modelBuilder.GetEdmModel();
        }
    }
}

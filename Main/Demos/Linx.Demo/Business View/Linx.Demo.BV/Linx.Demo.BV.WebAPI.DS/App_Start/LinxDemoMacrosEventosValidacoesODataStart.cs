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
using BusinessNS = Linx.Demo.BV.MacrosEventosValidacoes;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Linx.Demo.BV.WebAPI.DS.App_Start.LinxDemoMacrosEventosValidacoesODataStart), "Start")]

namespace Linx.Demo.BV.WebAPI.DS.App_Start
{

    public static class LinxDemoMacrosEventosValidacoesODataStart
    {
        public static void Start()
        {
           var conventions = ODataRoutingConventions.CreateDefault();
           conventions.Insert(0, new Linx.DataService.ODataContainmentRoutingConvention("LinxDemoMacrosEventosValidacoesFeed"));
           GlobalConfiguration.Configuration.Routes.MapODataServiceRoute(
               routeName: "LinxDemoMacrosEventosValidacoesODataRoute",
               routePrefix: "LinxDemoMacrosEventosValidacoesOData",
               model: GetEdmModel(), pathHandler: new DefaultODataPathHandler(), routingConventions: conventions
               );
        }
        
        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<BusinessNS.Arquivo>("Arquivo");
            modelBuilder.EntitySet<BusinessNS.Pais>("Pais");
            modelBuilder.EntitySet<BusinessNS.Estado>("Estado");
            modelBuilder.EntitySet<BusinessNS.EstadoParentComposition>("EstadoParentComposition");
            modelBuilder.EntitySet<BusinessNS.ValorVendas>("ValorVendas");
            return modelBuilder.GetEdmModel();
        }
    }
}

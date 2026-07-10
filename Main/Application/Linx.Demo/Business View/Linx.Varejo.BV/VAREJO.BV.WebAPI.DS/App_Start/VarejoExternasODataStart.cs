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
using BusinessNS = VAREJO.BV.Externas;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(VAREJO.BV.WebAPI.DS.App_Start.VarejoExternasODataStart), "Start")]

namespace VAREJO.BV.WebAPI.DS.App_Start
{

    public static class VarejoExternasODataStart
    {
        public static void Start()
        {
           var conventions = ODataRoutingConventions.CreateDefault();
           conventions.Insert(0, new Linx.DataService.ODataContainmentRoutingConvention("VarejoExternasFeed"));
           GlobalConfiguration.Configuration.Routes.MapODataServiceRoute(
               routeName: "VarejoExternasODataRoute",
               routePrefix: "VarejoExternasOData",
               model: GetEdmModel(), pathHandler: new DefaultODataPathHandler(), routingConventions: conventions
               );
        }
        
        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<BusinessNS.Cliente>("Cliente");
            modelBuilder.EntitySet<BusinessNS.Venda>("Venda");
            modelBuilder.EntitySet<BusinessNS.Estado>("Estado");
            modelBuilder.EntitySet<BusinessNS.Loja>("Loja");
            modelBuilder.EntitySet<BusinessNS.VendaItem>("VendaItem");
            modelBuilder.EntitySet<BusinessNS.VendaPrincipal>("VendaPrincipal");
            modelBuilder.EntitySet<BusinessNS.VendaFiltro>("VendaFiltro");
            return modelBuilder.GetEdmModel();
        }
    }
}

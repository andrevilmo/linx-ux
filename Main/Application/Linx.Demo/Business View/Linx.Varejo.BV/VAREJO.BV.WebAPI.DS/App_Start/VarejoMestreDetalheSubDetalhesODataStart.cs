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
using BusinessNS = VAREJO.BV.MestreDetalheSubDetalhes;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(VAREJO.BV.WebAPI.DS.App_Start.VarejoMestreDetalheSubDetalhesODataStart), "Start")]

namespace VAREJO.BV.WebAPI.DS.App_Start
{

    public static class VarejoMestreDetalheSubDetalhesODataStart
    {
        public static void Start()
        {
           var conventions = ODataRoutingConventions.CreateDefault();
           conventions.Insert(0, new Linx.DataService.ODataContainmentRoutingConvention("VarejoMestreDetalheSubDetalhesFeed"));
           GlobalConfiguration.Configuration.Routes.MapODataServiceRoute(
               routeName: "VarejoMestreDetalheSubDetalhesODataRoute",
               routePrefix: "VarejoMestreDetalheSubDetalhesOData",
               model: GetEdmModel(), pathHandler: new DefaultODataPathHandler(), routingConventions: conventions
               );
        }
        
        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<BusinessNS.Cliente>("Cliente");
            modelBuilder.EntitySet<BusinessNS.Venda>("Venda");
            modelBuilder.EntitySet<BusinessNS.VendaParentComposition>("VendaParentComposition");
            modelBuilder.EntitySet<BusinessNS.VendaAtacado>("VendaAtacado");
            modelBuilder.EntitySet<BusinessNS.VendaAtacadoParentComposition>("VendaAtacadoParentComposition");
            modelBuilder.EntitySet<BusinessNS.VendaItem>("VendaItem");
            modelBuilder.EntitySet<BusinessNS.VendaItemParentComposition>("VendaItemParentComposition");
            modelBuilder.EntitySet<BusinessNS.ClienteWizard>("ClienteWizard");
            modelBuilder.EntitySet<BusinessNS.VendaWizard>("VendaWizard");
            modelBuilder.EntitySet<BusinessNS.VendaItemWizard>("VendaItemWizard");
            modelBuilder.EntitySet<BusinessNS.VendaAtacadoWizard>("VendaAtacadoWizard");
            return modelBuilder.GetEdmModel();
        }
    }
}

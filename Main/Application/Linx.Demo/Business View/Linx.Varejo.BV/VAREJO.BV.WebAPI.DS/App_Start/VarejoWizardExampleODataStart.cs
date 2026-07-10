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
using BusinessNS = VAREJO.BV.WizardExample;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(VAREJO.BV.WebAPI.DS.App_Start.VarejoWizardExampleODataStart), "Start")]

namespace VAREJO.BV.WebAPI.DS.App_Start
{

    public static class VarejoWizardExampleODataStart
    {
        public static void Start()
        {
           var conventions = ODataRoutingConventions.CreateDefault();
           conventions.Insert(0, new Linx.DataService.ODataContainmentRoutingConvention("VarejoWizardExampleFeed"));
           GlobalConfiguration.Configuration.Routes.MapODataServiceRoute(
               routeName: "VarejoWizardExampleODataRoute",
               routePrefix: "VarejoWizardExampleOData",
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
            return modelBuilder.GetEdmModel();
        }
    }
}

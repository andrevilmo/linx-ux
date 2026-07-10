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
using LinxTraining002.BM;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(LinxTraining002.BM.WebAPI.App_Start.ModelLFW002Controller1AttributeRoutingHttp), "Start")]

namespace LinxTraining002.BM.WebAPI.App_Start
{

    public static class ModelLFW002Controller1AttributeRoutingHttp
    {
        public static void Start()
        {
           var conventions = ODataRoutingConventions.CreateDefault();
           conventions.Insert(0, new Linx.DataService.ODataContainmentRoutingConvention("ModelLFW002Controller1"));
           GlobalConfiguration.Configuration.Routes.MapODataServiceRoute(
               routeName: "ModelLFW002Controller1Route",
               routePrefix: "ModelLFW002Controller1",
               model: GetEdmModel(), pathHandler: new DefaultODataPathHandler(), routingConventions: conventions
               );
        }
        
        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<Cidade>("Cidade");
            modelBuilder.EntitySet<Clientes>("Clientes");
            modelBuilder.EntitySet<Estado>("Estado");
            modelBuilder.EntitySet<FilhaNotNull>("FilhaNotNull");
            modelBuilder.EntitySet<Loja>("Loja");
            modelBuilder.EntitySet<PaiNotNull>("PaiNotNull");
            modelBuilder.EntitySet<TestePIVOT>("TestePIVOT");
            modelBuilder.EntitySet<TiposCampos>("TiposCampos");
            modelBuilder.EntitySet<TiposCamposFilha>("TiposCamposFilha");
            modelBuilder.EntitySet<VendaDetalhe>("VendaDetalhe");
            modelBuilder.EntitySet<Vendas>("Vendas");
            return modelBuilder.GetEdmModel();
        }
    }
}

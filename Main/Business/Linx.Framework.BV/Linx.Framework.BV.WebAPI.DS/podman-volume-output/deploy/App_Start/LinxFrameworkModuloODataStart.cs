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
using BusinessNS = Linx.Framework.BV.Modulo;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Linx.Framework.BV.WebAPI.DS.App_Start.LinxFrameworkModuloODataStart), "Start")]

namespace Linx.Framework.BV.WebAPI.DS.App_Start
{

    public static class LinxFrameworkModuloODataStart
    {
        public static void Start()
        {
           var conventions = ODataRoutingConventions.CreateDefault();
           conventions.Insert(0, new Linx.DataService.ODataContainmentRoutingConvention("LinxFrameworkModuloFeed"));
           GlobalConfiguration.Configuration.Routes.MapODataServiceRoute(
               routeName: "LinxFrameworkModuloODataRoute",
               routePrefix: "LinxFrameworkModuloOData",
               model: GetEdmModel(), pathHandler: new DefaultODataPathHandler(), routingConventions: conventions
               );
        }
        
        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<BusinessNS.TcsModulo>("TcsModulo");
            modelBuilder.EntitySet<BusinessNS.TcsModuloMenu>("TcsModuloMenu");
            modelBuilder.EntitySet<BusinessNS.TcsModuloDoGrupo>("TcsModuloDoGrupo");
            modelBuilder.EntitySet<BusinessNS.TcsTransacaoMenu>("TcsTransacaoMenu");
            modelBuilder.EntitySet<BusinessNS.TcsModuloGrupo>("TcsModuloGrupo");
            modelBuilder.EntitySet<BusinessNS.TcsModuloDoGrupoDetalhe>("TcsModuloDoGrupoDetalhe");
            modelBuilder.EntitySet<BusinessNS.TcsModuloDoGrupoDetalheParentComposition>("TcsModuloDoGrupoDetalheParentComposition");
            modelBuilder.EntitySet<BusinessNS.AppModule>("AppModule");
            modelBuilder.EntitySet<BusinessNS.BreadCrumbItem>("BreadCrumbItem");
            modelBuilder.EntitySet<BusinessNS.AppMenu>("AppMenu");
            modelBuilder.EntitySet<BusinessNS.UserModules>("UserModules");
            modelBuilder.EntitySet<BusinessNS.TcsUsuarioFavorito>("TcsUsuarioFavorito");
            modelBuilder.EntitySet<BusinessNS.EnvironmentInfo>("EnvironmentInfo");
            return modelBuilder.GetEdmModel();
        }
    }
}

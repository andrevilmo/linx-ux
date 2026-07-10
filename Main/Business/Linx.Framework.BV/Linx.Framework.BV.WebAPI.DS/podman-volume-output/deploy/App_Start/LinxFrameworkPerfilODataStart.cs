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
using BusinessNS = Linx.Framework.BV.Perfil;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Linx.Framework.BV.WebAPI.DS.App_Start.LinxFrameworkPerfilODataStart), "Start")]

namespace Linx.Framework.BV.WebAPI.DS.App_Start
{

    public static class LinxFrameworkPerfilODataStart
    {
        public static void Start()
        {
           var conventions = ODataRoutingConventions.CreateDefault();
           conventions.Insert(0, new Linx.DataService.ODataContainmentRoutingConvention("LinxFrameworkPerfilFeed"));
           GlobalConfiguration.Configuration.Routes.MapODataServiceRoute(
               routeName: "LinxFrameworkPerfilODataRoute",
               routePrefix: "LinxFrameworkPerfilOData",
               model: GetEdmModel(), pathHandler: new DefaultODataPathHandler(), routingConventions: conventions
               );
        }
        
        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<BusinessNS.TcsPerfil>("TcsPerfil");
            modelBuilder.EntitySet<BusinessNS.TcsUsuarioPerfil>("TcsUsuarioPerfil");
            modelBuilder.EntitySet<BusinessNS.TcsUsuarioPerfilParentComposition>("TcsUsuarioPerfilParentComposition");
            modelBuilder.EntitySet<BusinessNS.TcsPerfilRegraModulo>("TcsPerfilRegraModulo");
            modelBuilder.EntitySet<BusinessNS.TcsPerfilRegraModuloParentComposition>("TcsPerfilRegraModuloParentComposition");
            modelBuilder.EntitySet<BusinessNS.TcsPerfilRegraColuna>("TcsPerfilRegraColuna");
            modelBuilder.EntitySet<BusinessNS.TcsPerfilRegraColunaParentComposition>("TcsPerfilRegraColunaParentComposition");
            modelBuilder.EntitySet<BusinessNS.TcsPerfilRegraTransacao>("TcsPerfilRegraTransacao");
            modelBuilder.EntitySet<BusinessNS.TcsPerfilRegraTransacaoParentComposition>("TcsPerfilRegraTransacaoParentComposition");
            modelBuilder.EntitySet<BusinessNS.TcsPerfilBandeiraRede>("TcsPerfilBandeiraRede");
            modelBuilder.EntitySet<BusinessNS.TcsPerfilBandeiraRedeParentComposition>("TcsPerfilBandeiraRedeParentComposition");
            modelBuilder.EntitySet<BusinessNS.TcsPerfilLayout>("TcsPerfilLayout");
            modelBuilder.EntitySet<BusinessNS.TcsPerfilLayoutParentComposition>("TcsPerfilLayoutParentComposition");
            modelBuilder.EntitySet<BusinessNS.TcsPerfilFilial>("TcsPerfilFilial");
            modelBuilder.EntitySet<BusinessNS.TcsPerfilFilialParentComposition>("TcsPerfilFilialParentComposition");
            return modelBuilder.GetEdmModel();
        }
    }
}

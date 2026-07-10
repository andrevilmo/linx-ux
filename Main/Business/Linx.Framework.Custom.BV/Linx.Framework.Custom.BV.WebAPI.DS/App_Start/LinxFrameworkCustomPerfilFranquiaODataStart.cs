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
using BusinessNS = Linx.Framework.Custom.BV.PerfilFranquia;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Linx.Framework.Custom.BV.WebAPI.DS.App_Start.LinxFrameworkCustomPerfilFranquiaODataStart), "Start")]

namespace Linx.Framework.Custom.BV.WebAPI.DS.App_Start
{

    public static class LinxFrameworkCustomPerfilFranquiaODataStart
    {
        public static void Start()
        {
           var conventions = ODataRoutingConventions.CreateDefault();
           conventions.Insert(0, new Linx.DataService.ODataContainmentRoutingConvention("LinxFrameworkCustomPerfilFranquiaFeed"));
           GlobalConfiguration.Configuration.Routes.MapODataServiceRoute(
               routeName: "LinxFrameworkCustomPerfilFranquiaODataRoute",
               routePrefix: "LinxFrameworkCustomPerfilFranquiaOData",
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
            modelBuilder.EntitySet<BusinessNS.TcsPerfilRegraTransacao>("TcsPerfilRegraTransacao");
            modelBuilder.EntitySet<BusinessNS.TcsPerfilRegraTransacaoParentComposition>("TcsPerfilRegraTransacaoParentComposition");
            modelBuilder.EntitySet<BusinessNS.TcsPerfilBandeiraRede>("TcsPerfilBandeiraRede");
            modelBuilder.EntitySet<BusinessNS.TcsPerfilBandeiraRedeParentComposition>("TcsPerfilBandeiraRedeParentComposition");
            modelBuilder.EntitySet<BusinessNS.TcsPerfilFilial>("TcsPerfilFilial");
            modelBuilder.EntitySet<BusinessNS.TcsPerfilFilialParentComposition>("TcsPerfilFilialParentComposition");
            modelBuilder.EntitySet<BusinessNS.TbcFilial>("TbcFilial");
            modelBuilder.EntitySet<BusinessNS.SyncInfo>("SyncInfo");
            return modelBuilder.GetEdmModel();
        }
    }
}

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
using BusinessNS = Linx.Framework.Setup.LinxAutoSetup;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Linx.Framework.Setup.WebAPI.DS.App_Start.LinxFrameworkLinxAutoSetupODataStart), "Start")]

namespace Linx.Framework.Setup.WebAPI.DS.App_Start
{

    public static class LinxFrameworkLinxAutoSetupODataStart
    {
        public static void Start()
        {
           var conventions = ODataRoutingConventions.CreateDefault();
           conventions.Insert(0, new Linx.DataService.ODataContainmentRoutingConvention("LinxFrameworkLinxAutoSetupFeed"));
           GlobalConfiguration.Configuration.Routes.MapODataServiceRoute(
               routeName: "LinxFrameworkLinxAutoSetupODataRoute",
               routePrefix: "LinxFrameworkLinxAutoSetupOData",
               model: GetEdmModel(), pathHandler: new DefaultODataPathHandler(), routingConventions: conventions
               );
        }
        
        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<BusinessNS.TcsEmpresaAutenticacao>("TcsEmpresaAutenticacao");
            modelBuilder.EntitySet<BusinessNS.TcsEmpresaAutenticacaoModulo>("TcsEmpresaAutenticacaoModulo");
            modelBuilder.EntitySet<BusinessNS.TcsUsuarioAutenticacao>("TcsUsuarioAutenticacao");
            modelBuilder.EntitySet<BusinessNS.TcsUsuarioAutenticacaoAcesso>("TcsUsuarioAutenticacaoAcesso");
            modelBuilder.EntitySet<BusinessNS.TcsUsuarioPerfil>("TcsUsuarioPerfil");
            modelBuilder.EntitySet<BusinessNS.TcsAmbiente>("TcsAmbiente");
            modelBuilder.EntitySet<BusinessNS.TcsAmbienteConexao>("TcsAmbienteConexao");
            modelBuilder.EntitySet<BusinessNS.TcsAmbienteUsuarioAcesso>("TcsAmbienteUsuarioAcesso");
            modelBuilder.EntitySet<BusinessNS.TcsModuloGrupo>("TcsModuloGrupo");
            modelBuilder.EntitySet<BusinessNS.TcsModuloGrupoDetalhe>("TcsModuloGrupoDetalhe");
            modelBuilder.EntitySet<BusinessNS.TcsParametroValor>("TcsParametroValor");
            modelBuilder.EntitySet<BusinessNS.TcsPerfil>("TcsPerfil");
            modelBuilder.EntitySet<BusinessNS.TcsPerfilRegraModulo>("TcsPerfilRegraModulo");
            modelBuilder.EntitySet<BusinessNS.TcsPerfilUsuario>("TcsPerfilUsuario");
            modelBuilder.EntitySet<BusinessNS.AmbienteInfo>("AmbienteInfo");
            modelBuilder.EntitySet<BusinessNS.TcsEmpresaGpecon>("TcsEmpresaGpecon");
            modelBuilder.EntitySet<BusinessNS.TcsAmbienteInfo>("TcsAmbienteInfo");
            modelBuilder.EntitySet<BusinessNS.TcsParametroAutorizacao>("TcsParametroAutorizacao");
            modelBuilder.EntitySet<BusinessNS.MultimarcaInfo>("MultimarcaInfo");
            modelBuilder.EntitySet<BusinessNS.TbcFilial>("TbcFilial");
            modelBuilder.EntitySet<BusinessNS.TbcGrupoEconomico>("TbcGrupoEconomico");
            modelBuilder.EntitySet<BusinessNS.TbcBandeiraRede>("TbcBandeiraRede");
            modelBuilder.EntitySet<BusinessNS.LjvCanalVenda>("LjvCanalVenda");
            return modelBuilder.GetEdmModel();
        }
    }
}

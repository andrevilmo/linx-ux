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
using BusinessNS = Linx.Framework.BV.UsuarioFranquia;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Linx.Framework.BV.WebAPI.DS.App_Start.LinxFrameworkUsuarioFranquiaODataStart), "Start")]

namespace Linx.Framework.BV.WebAPI.DS.App_Start
{

    public static class LinxFrameworkUsuarioFranquiaODataStart
    {
        public static void Start()
        {
           var conventions = ODataRoutingConventions.CreateDefault();
           conventions.Insert(0, new Linx.DataService.ODataContainmentRoutingConvention("LinxFrameworkUsuarioFranquiaFeed"));
           GlobalConfiguration.Configuration.Routes.MapODataServiceRoute(
               routeName: "LinxFrameworkUsuarioFranquiaODataRoute",
               routePrefix: "LinxFrameworkUsuarioFranquiaOData",
               model: GetEdmModel(), pathHandler: new DefaultODataPathHandler(), routingConventions: conventions
               );
        }
        
        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<BusinessNS.TcsUsuarioAutenticacao>("TcsUsuarioAutenticacao");
            modelBuilder.EntitySet<BusinessNS.TcsUsuarioAutenticacaoAcesso>("TcsUsuarioAutenticacaoAcesso");
            modelBuilder.EntitySet<BusinessNS.TcsUsuarioAutenticacaoPerfil>("TcsUsuarioAutenticacaoPerfil");
            modelBuilder.EntitySet<BusinessNS.TcsUsuarioPerfil>("TcsUsuarioPerfil");
            modelBuilder.EntitySet<BusinessNS.UsuarioPerfilInfo>("UsuarioPerfilInfo");
            return modelBuilder.GetEdmModel();
        }
    }
}

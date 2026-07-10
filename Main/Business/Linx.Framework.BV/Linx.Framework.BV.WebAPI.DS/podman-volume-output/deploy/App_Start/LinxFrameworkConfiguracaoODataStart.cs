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
using BusinessNS = Linx.Framework.BV.Configuracao;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Linx.Framework.BV.WebAPI.DS.App_Start.LinxFrameworkConfiguracaoODataStart), "Start")]

namespace Linx.Framework.BV.WebAPI.DS.App_Start
{

    public static class LinxFrameworkConfiguracaoODataStart
    {
        public static void Start()
        {
           var conventions = ODataRoutingConventions.CreateDefault();
           conventions.Insert(0, new Linx.DataService.ODataContainmentRoutingConvention("LinxFrameworkConfiguracaoFeed"));
           GlobalConfiguration.Configuration.Routes.MapODataServiceRoute(
               routeName: "LinxFrameworkConfiguracaoODataRoute",
               routePrefix: "LinxFrameworkConfiguracaoOData",
               model: GetEdmModel(), pathHandler: new DefaultODataPathHandler(), routingConventions: conventions
               );
        }
        
        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<BusinessNS.TcsUsuarioConfiguracao>("TcsUsuarioConfiguracao");
            modelBuilder.EntitySet<BusinessNS.TcsUsuarioConfiguracaoAcesso>("TcsUsuarioConfiguracaoAcesso");
            modelBuilder.EntitySet<BusinessNS.TcsUsuarioConfiguracaoAcessoParentComposition>("TcsUsuarioConfiguracaoAcessoParentComposition");
            modelBuilder.EntitySet<BusinessNS.ConfiguracaoAcesso>("ConfiguracaoAcesso");
            return modelBuilder.GetEdmModel();
        }
    }
}

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
using BusinessNS = Linx.Framework.BV.Conexao;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Linx.Framework.BV.WebAPI.DS.App_Start.LinxFrameworkConexaoODataStart), "Start")]

namespace Linx.Framework.BV.WebAPI.DS.App_Start
{

    public static class LinxFrameworkConexaoODataStart
    {
        public static void Start()
        {
           var conventions = ODataRoutingConventions.CreateDefault();
           conventions.Insert(0, new Linx.DataService.ODataContainmentRoutingConvention("LinxFrameworkConexaoFeed"));
           GlobalConfiguration.Configuration.Routes.MapODataServiceRoute(
               routeName: "LinxFrameworkConexaoODataRoute",
               routePrefix: "LinxFrameworkConexaoOData",
               model: GetEdmModel(), pathHandler: new DefaultODataPathHandler(), routingConventions: conventions
               );
        }
        
        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<BusinessNS.TcsConexaoDb>("TcsConexaoDb");
            modelBuilder.EntitySet<BusinessNS.TcsBancoServidor>("TcsBancoServidor");
            modelBuilder.EntitySet<BusinessNS.TcsAmbienteConexao>("TcsAmbienteConexao");
            modelBuilder.EntitySet<BusinessNS.TcsAmbienteConexaoParentComposition>("TcsAmbienteConexaoParentComposition");
            return modelBuilder.GetEdmModel();
        }
    }
}

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
using BusinessNS = Linx.Framework.BV.Aplicacao;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Linx.Framework.BV.WebAPI.DS.App_Start.LinxFrameworkAplicacaoODataStart), "Start")]

namespace Linx.Framework.BV.WebAPI.DS.App_Start
{

    public static class LinxFrameworkAplicacaoODataStart
    {
        public static void Start()
        {
           var conventions = ODataRoutingConventions.CreateDefault();
           conventions.Insert(0, new Linx.DataService.ODataContainmentRoutingConvention("LinxFrameworkAplicacaoFeed"));
           GlobalConfiguration.Configuration.Routes.MapODataServiceRoute(
               routeName: "LinxFrameworkAplicacaoODataRoute",
               routePrefix: "LinxFrameworkAplicacaoOData",
               model: GetEdmModel(), pathHandler: new DefaultODataPathHandler(), routingConventions: conventions
               );
        }
        
        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<BusinessNS.TcsAplicacao>("TcsAplicacao");
            modelBuilder.EntitySet<BusinessNS.TcsAplicacaoVersaoHistorico>("TcsAplicacaoVersaoHistorico");
            modelBuilder.EntitySet<BusinessNS.TcsAplicacaoVersaoHistoricoParentComposition>("TcsAplicacaoVersaoHistoricoParentComposition");
            modelBuilder.EntitySet<BusinessNS.TcsAmbiente>("TcsAmbiente");
            modelBuilder.EntitySet<BusinessNS.TcsAmbienteParentComposition>("TcsAmbienteParentComposition");
            return modelBuilder.GetEdmModel();
        }
    }
}

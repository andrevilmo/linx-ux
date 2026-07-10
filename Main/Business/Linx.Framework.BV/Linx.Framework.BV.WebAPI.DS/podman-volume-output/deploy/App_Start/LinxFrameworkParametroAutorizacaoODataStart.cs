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
using BusinessNS = Linx.Framework.BV.ParametroAutorizacao;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Linx.Framework.BV.WebAPI.DS.App_Start.LinxFrameworkParametroAutorizacaoODataStart), "Start")]

namespace Linx.Framework.BV.WebAPI.DS.App_Start
{

    public static class LinxFrameworkParametroAutorizacaoODataStart
    {
        public static void Start()
        {
           var conventions = ODataRoutingConventions.CreateDefault();
           conventions.Insert(0, new Linx.DataService.ODataContainmentRoutingConvention("LinxFrameworkParametroAutorizacaoFeed"));
           GlobalConfiguration.Configuration.Routes.MapODataServiceRoute(
               routeName: "LinxFrameworkParametroAutorizacaoODataRoute",
               routePrefix: "LinxFrameworkParametroAutorizacaoOData",
               model: GetEdmModel(), pathHandler: new DefaultODataPathHandler(), routingConventions: conventions
               );
        }
        
        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<BusinessNS.TcsParametroAutorizacao>("TcsParametroAutorizacao");
            modelBuilder.EntitySet<BusinessNS.TcsParametroTabelaSelecaoAutorizacao>("TcsParametroTabelaSelecaoAutorizacao");
            modelBuilder.EntitySet<BusinessNS.TcsParametroTabelaSelecaoAutorizacaoParentComposition>("TcsParametroTabelaSelecaoAutorizacaoParentComposition");
            modelBuilder.EntitySet<BusinessNS.TcsParametroGrupoAutorizacao>("TcsParametroGrupoAutorizacao");
            modelBuilder.EntitySet<BusinessNS.TcsParametroAutorizacaoGrupo>("TcsParametroAutorizacaoGrupo");
            modelBuilder.EntitySet<BusinessNS.TcsParametroAutorizacaoGrupoParentComposition>("TcsParametroAutorizacaoGrupoParentComposition");
            return modelBuilder.GetEdmModel();
        }
    }
}

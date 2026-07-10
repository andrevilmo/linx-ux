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
using BusinessNS = Linx.Framework.BV.MultimidiaAutorizacao;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Linx.Framework.BV.WebAPI.DS.App_Start.LinxFrameworkMultimidiaAutorizacaoODataStart), "Start")]

namespace Linx.Framework.BV.WebAPI.DS.App_Start
{

    public static class LinxFrameworkMultimidiaAutorizacaoODataStart
    {
        public static void Start()
        {
           var conventions = ODataRoutingConventions.CreateDefault();
           conventions.Insert(0, new Linx.DataService.ODataContainmentRoutingConvention("LinxFrameworkMultimidiaAutorizacaoFeed"));
           GlobalConfiguration.Configuration.Routes.MapODataServiceRoute(
               routeName: "LinxFrameworkMultimidiaAutorizacaoODataRoute",
               routePrefix: "LinxFrameworkMultimidiaAutorizacaoOData",
               model: GetEdmModel(), pathHandler: new DefaultODataPathHandler(), routingConventions: conventions
               );
        }
        
        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<BusinessNS.DocMultimidiaAutorizacao>("DocMultimidiaAutorizacao");
            modelBuilder.EntitySet<BusinessNS.DocMultimidiaTabelaAutorizacaoChild>("DocMultimidiaTabelaAutorizacaoChild");
            modelBuilder.EntitySet<BusinessNS.DocMultimidiaTabelaAutorizacaoChildParentComposition>("DocMultimidiaTabelaAutorizacaoChildParentComposition");
            modelBuilder.EntitySet<BusinessNS.DocMultimidiaTabelaAutorizacao>("DocMultimidiaTabelaAutorizacao");
            return modelBuilder.GetEdmModel();
        }
    }
}

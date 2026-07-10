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
using BusinessNS = Linx.Framework.BV.ObjetoAutorizacao;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Linx.Framework.BV.WebAPI.DS.App_Start.LinxFrameworkObjetoAutorizacaoODataStart), "Start")]

namespace Linx.Framework.BV.WebAPI.DS.App_Start
{

    public static class LinxFrameworkObjetoAutorizacaoODataStart
    {
        public static void Start()
        {
           var conventions = ODataRoutingConventions.CreateDefault();
           conventions.Insert(0, new Linx.DataService.ODataContainmentRoutingConvention("LinxFrameworkObjetoAutorizacaoFeed"));
           GlobalConfiguration.Configuration.Routes.MapODataServiceRoute(
               routeName: "LinxFrameworkObjetoAutorizacaoODataRoute",
               routePrefix: "LinxFrameworkObjetoAutorizacaoOData",
               model: GetEdmModel(), pathHandler: new DefaultODataPathHandler(), routingConventions: conventions
               );
        }
        
        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<BusinessNS.TcsObjetoAutorizacao>("TcsObjetoAutorizacao");
            modelBuilder.EntitySet<BusinessNS.TcsTransacaoAutorizacaoChild>("TcsTransacaoAutorizacaoChild");
            modelBuilder.EntitySet<BusinessNS.TcsTransacaoAutorizacaoChildParentComposition>("TcsTransacaoAutorizacaoChildParentComposition");
            modelBuilder.EntitySet<BusinessNS.TcsObjetoConteudoAutorizacao>("TcsObjetoConteudoAutorizacao");
            modelBuilder.EntitySet<BusinessNS.TcsObjetoConteudoAutorizacaoParentComposition>("TcsObjetoConteudoAutorizacaoParentComposition");
            return modelBuilder.GetEdmModel();
        }
    }
}

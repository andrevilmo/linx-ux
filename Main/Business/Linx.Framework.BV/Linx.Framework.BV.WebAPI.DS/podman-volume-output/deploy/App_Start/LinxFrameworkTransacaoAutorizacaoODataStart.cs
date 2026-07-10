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
using BusinessNS = Linx.Framework.BV.TransacaoAutorizacao;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Linx.Framework.BV.WebAPI.DS.App_Start.LinxFrameworkTransacaoAutorizacaoODataStart), "Start")]

namespace Linx.Framework.BV.WebAPI.DS.App_Start
{

    public static class LinxFrameworkTransacaoAutorizacaoODataStart
    {
        public static void Start()
        {
           var conventions = ODataRoutingConventions.CreateDefault();
           conventions.Insert(0, new Linx.DataService.ODataContainmentRoutingConvention("LinxFrameworkTransacaoAutorizacaoFeed"));
           GlobalConfiguration.Configuration.Routes.MapODataServiceRoute(
               routeName: "LinxFrameworkTransacaoAutorizacaoODataRoute",
               routePrefix: "LinxFrameworkTransacaoAutorizacaoOData",
               model: GetEdmModel(), pathHandler: new DefaultODataPathHandler(), routingConventions: conventions
               );
        }
        
        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<BusinessNS.TcsTransacaoAutorizacao>("TcsTransacaoAutorizacao");
            modelBuilder.EntitySet<BusinessNS.TcsTransacaoMenuAutorizacao>("TcsTransacaoMenuAutorizacao");
            modelBuilder.EntitySet<BusinessNS.TcsTransacaoMenuAutorizacaoParentComposition>("TcsTransacaoMenuAutorizacaoParentComposition");
            modelBuilder.EntitySet<BusinessNS.TcsTransacaoDependenteAutorizacao>("TcsTransacaoDependenteAutorizacao");
            modelBuilder.EntitySet<BusinessNS.TcsTransacaoDependenteAutorizacaoParentComposition>("TcsTransacaoDependenteAutorizacaoParentComposition");
            return modelBuilder.GetEdmModel();
        }
    }
}

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
using BusinessNS = Linx.Framework.BV.Transacao;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Linx.Framework.BV.WebAPI.DS.App_Start.LinxFrameworkTransacaoODataStart), "Start")]

namespace Linx.Framework.BV.WebAPI.DS.App_Start
{

    public static class LinxFrameworkTransacaoODataStart
    {
        public static void Start()
        {
           var conventions = ODataRoutingConventions.CreateDefault();
           conventions.Insert(0, new Linx.DataService.ODataContainmentRoutingConvention("LinxFrameworkTransacaoFeed"));
           GlobalConfiguration.Configuration.Routes.MapODataServiceRoute(
               routeName: "LinxFrameworkTransacaoODataRoute",
               routePrefix: "LinxFrameworkTransacaoOData",
               model: GetEdmModel(), pathHandler: new DefaultODataPathHandler(), routingConventions: conventions
               );
        }
        
        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<BusinessNS.TcsTransacao>("TcsTransacao");
            modelBuilder.EntitySet<BusinessNS.TcsTransacaoMenuChild>("TcsTransacaoMenuChild");
            modelBuilder.EntitySet<BusinessNS.TcsTransacaoMenu>("TcsTransacaoMenu");
            modelBuilder.EntitySet<BusinessNS.TcsTransacaoDependente>("TcsTransacaoDependente");
            modelBuilder.EntitySet<BusinessNS.TcsModuloMenuP>("TcsModuloMenuP");
            return modelBuilder.GetEdmModel();
        }
    }
}

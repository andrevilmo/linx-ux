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
using BusinessNS = Linx.Framework.BV.Auditoria;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Linx.Framework.BV.WebAPI.DS.App_Start.LinxFrameworkAuditoriaODataStart), "Start")]

namespace Linx.Framework.BV.WebAPI.DS.App_Start
{

    public static class LinxFrameworkAuditoriaODataStart
    {
        public static void Start()
        {
           var conventions = ODataRoutingConventions.CreateDefault();
           conventions.Insert(0, new Linx.DataService.ODataContainmentRoutingConvention("LinxFrameworkAuditoriaFeed"));
           GlobalConfiguration.Configuration.Routes.MapODataServiceRoute(
               routeName: "LinxFrameworkAuditoriaODataRoute",
               routePrefix: "LinxFrameworkAuditoriaOData",
               model: GetEdmModel(), pathHandler: new DefaultODataPathHandler(), routingConventions: conventions
               );
        }
        
        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<BusinessNS.AdtAuditoria>("AdtAuditoria");
            modelBuilder.EntitySet<BusinessNS.AdtAuditoriaItem>("AdtAuditoriaItem");
            modelBuilder.EntitySet<BusinessNS.AdtAuditoriaItemParentComposition>("AdtAuditoriaItemParentComposition");
            modelBuilder.EntitySet<BusinessNS.AdtAuditoriaItemDetalhe>("AdtAuditoriaItemDetalhe");
            return modelBuilder.GetEdmModel();
        }
    }
}

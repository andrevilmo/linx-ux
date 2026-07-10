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
using BusinessNS = Linx.Framework.BV.Multimidia;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Linx.Framework.BV.WebAPI.DS.App_Start.LinxFrameworkMultimidiaODataStart), "Start")]

namespace Linx.Framework.BV.WebAPI.DS.App_Start
{

    public static class LinxFrameworkMultimidiaODataStart
    {
        public static void Start()
        {
           var conventions = ODataRoutingConventions.CreateDefault();
           conventions.Insert(0, new Linx.DataService.ODataContainmentRoutingConvention("LinxFrameworkMultimidiaFeed"));
           GlobalConfiguration.Configuration.Routes.MapODataServiceRoute(
               routeName: "LinxFrameworkMultimidiaODataRoute",
               routePrefix: "LinxFrameworkMultimidiaOData",
               model: GetEdmModel(), pathHandler: new DefaultODataPathHandler(), routingConventions: conventions
               );
        }
        
        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<BusinessNS.DocMultimidiaTabela>("DocMultimidiaTabela");
            modelBuilder.EntitySet<BusinessNS.DocMultimidiaCompact>("DocMultimidiaCompact");
            modelBuilder.EntitySet<BusinessNS.MultimidiaCompact2BO>("MultimidiaCompact2BO");
            modelBuilder.EntitySet<BusinessNS.DocMultimidiaUid>("DocMultimidiaUid");
            modelBuilder.EntitySet<BusinessNS.DocMultimidiaInfo>("DocMultimidiaInfo");
            modelBuilder.EntitySet<BusinessNS.DocMultimidia>("DocMultimidia");
            modelBuilder.EntitySet<BusinessNS.DocMultimidiaTabelaChild>("DocMultimidiaTabelaChild");
            modelBuilder.EntitySet<BusinessNS.DocMultimidiaTabelaChildParentComposition>("DocMultimidiaTabelaChildParentComposition");
            modelBuilder.EntitySet<BusinessNS.DocMultimidiaConfig>("DocMultimidiaConfig");
            modelBuilder.EntitySet<BusinessNS.MediaElement>("MediaElement");
            modelBuilder.EntitySet<BusinessNS.MediaConfigLength>("MediaConfigLength");
            modelBuilder.EntitySet<BusinessNS.DocMultimidiaUpload>("DocMultimidiaUpload");
            modelBuilder.EntitySet<BusinessNS.DocTabelaSync>("DocTabelaSync");
            return modelBuilder.GetEdmModel();
        }
    }
}

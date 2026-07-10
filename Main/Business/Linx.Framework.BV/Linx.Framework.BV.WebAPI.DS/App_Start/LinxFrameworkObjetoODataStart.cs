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
using BusinessNS = Linx.Framework.BV.Objeto;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Linx.Framework.BV.WebAPI.DS.App_Start.LinxFrameworkObjetoODataStart), "Start")]

namespace Linx.Framework.BV.WebAPI.DS.App_Start
{

    public static class LinxFrameworkObjetoODataStart
    {
        public static void Start()
        {
           var conventions = ODataRoutingConventions.CreateDefault();
           conventions.Insert(0, new Linx.DataService.ODataContainmentRoutingConvention("LinxFrameworkObjetoFeed"));
           GlobalConfiguration.Configuration.Routes.MapODataServiceRoute(
               routeName: "LinxFrameworkObjetoODataRoute",
               routePrefix: "LinxFrameworkObjetoOData",
               model: GetEdmModel(), pathHandler: new DefaultODataPathHandler(), routingConventions: conventions
               );
        }
        
        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<BusinessNS.TcsObjeto>("TcsObjeto");
            modelBuilder.EntitySet<BusinessNS.TcsObjetoConteudoMnt>("TcsObjetoConteudoMnt");
            modelBuilder.EntitySet<BusinessNS.TcsTransacao>("TcsTransacao");
            modelBuilder.EntitySet<BusinessNS.ConfiguracaoExportacao>("ConfiguracaoExportacao");
            modelBuilder.EntitySet<BusinessNS.TcsObjetoPermissao>("TcsObjetoPermissao");
            modelBuilder.EntitySet<BusinessNS.TcsUsuario>("TcsUsuario");
            modelBuilder.EntitySet<BusinessNS.LayoutInfo>("LayoutInfo");
            return modelBuilder.GetEdmModel();
        }
    }
}

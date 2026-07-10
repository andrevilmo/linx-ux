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
using BusinessNS = Linx.Framework.BV.Autorizacao;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Linx.Framework.BV.WebAPI.DS.App_Start.LinxFrameworkAutorizacaoODataStart), "Start")]

namespace Linx.Framework.BV.WebAPI.DS.App_Start
{

    public static class LinxFrameworkAutorizacaoODataStart
    {
        public static void Start()
        {
           var conventions = ODataRoutingConventions.CreateDefault();
           conventions.Insert(0, new Linx.DataService.ODataContainmentRoutingConvention("LinxFrameworkAutorizacaoFeed"));
           GlobalConfiguration.Configuration.Routes.MapODataServiceRoute(
               routeName: "LinxFrameworkAutorizacaoODataRoute",
               routePrefix: "LinxFrameworkAutorizacaoOData",
               model: GetEdmModel(), pathHandler: new DefaultODataPathHandler(), routingConventions: conventions
               );
        }
        
        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<BusinessNS.Acesso>("Acesso");
            modelBuilder.EntitySet<BusinessNS.UsuarioAcesso>("UsuarioAcesso");
            modelBuilder.EntitySet<BusinessNS.UserInfo>("UserInfo");
            modelBuilder.EntitySet<BusinessNS.LoginInfo>("LoginInfo");
            modelBuilder.EntitySet<BusinessNS.AmbienteInfo>("AmbienteInfo");
            modelBuilder.EntitySet<BusinessNS.TcsUsuarioAcesso>("TcsUsuarioAcesso");
            modelBuilder.EntitySet<BusinessNS.AppInfo>("AppInfo");
            modelBuilder.EntitySet<BusinessNS.GpeconInfo>("GpeconInfo");
            return modelBuilder.GetEdmModel();
        }
    }
}

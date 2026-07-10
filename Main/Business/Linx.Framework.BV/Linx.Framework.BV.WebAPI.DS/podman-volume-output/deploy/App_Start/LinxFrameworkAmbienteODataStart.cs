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
using BusinessNS = Linx.Framework.BV.Ambiente;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Linx.Framework.BV.WebAPI.DS.App_Start.LinxFrameworkAmbienteODataStart), "Start")]

namespace Linx.Framework.BV.WebAPI.DS.App_Start
{

    public static class LinxFrameworkAmbienteODataStart
    {
        public static void Start()
        {
           var conventions = ODataRoutingConventions.CreateDefault();
           conventions.Insert(0, new Linx.DataService.ODataContainmentRoutingConvention("LinxFrameworkAmbienteFeed"));
           GlobalConfiguration.Configuration.Routes.MapODataServiceRoute(
               routeName: "LinxFrameworkAmbienteODataRoute",
               routePrefix: "LinxFrameworkAmbienteOData",
               model: GetEdmModel(), pathHandler: new DefaultODataPathHandler(), routingConventions: conventions
               );
        }
        
        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<BusinessNS.TcsAmbienteUsuarioAcesso>("TcsAmbienteUsuarioAcesso");
            modelBuilder.EntitySet<BusinessNS.TcsAmbienteUsuarioAcessoParentComposition>("TcsAmbienteUsuarioAcessoParentComposition");
            modelBuilder.EntitySet<BusinessNS.TcsAmbiente>("TcsAmbiente");
            modelBuilder.EntitySet<BusinessNS.TcsAmbienteConexao>("TcsAmbienteConexao");
            modelBuilder.EntitySet<BusinessNS.TcsAmbienteConexaoParentComposition>("TcsAmbienteConexaoParentComposition");
            modelBuilder.EntitySet<BusinessNS.TcsAmbienteServicoExcecao>("TcsAmbienteServicoExcecao");
            modelBuilder.EntitySet<BusinessNS.TcsAmbienteServicoExcecaoParentComposition>("TcsAmbienteServicoExcecaoParentComposition");
            modelBuilder.EntitySet<BusinessNS.TcsServico>("TcsServico");
            modelBuilder.EntitySet<BusinessNS.TcsAmbienteRelacionado>("TcsAmbienteRelacionado");
            modelBuilder.EntitySet<BusinessNS.ServicoExcecaoInfo>("ServicoExcecaoInfo");
            modelBuilder.EntitySet<BusinessNS.AmbienteServicoInfo>("AmbienteServicoInfo");
            modelBuilder.EntitySet<BusinessNS.EnvironmentInfo>("EnvironmentInfo");
            return modelBuilder.GetEdmModel();
        }
    }
}

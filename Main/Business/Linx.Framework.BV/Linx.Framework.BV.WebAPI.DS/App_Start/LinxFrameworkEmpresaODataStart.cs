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
using BusinessNS = Linx.Framework.BV.Empresa;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Linx.Framework.BV.WebAPI.DS.App_Start.LinxFrameworkEmpresaODataStart), "Start")]

namespace Linx.Framework.BV.WebAPI.DS.App_Start
{

    public static class LinxFrameworkEmpresaODataStart
    {
        public static void Start()
        {
           var conventions = ODataRoutingConventions.CreateDefault();
           conventions.Insert(0, new Linx.DataService.ODataContainmentRoutingConvention("LinxFrameworkEmpresaFeed"));
           GlobalConfiguration.Configuration.Routes.MapODataServiceRoute(
               routeName: "LinxFrameworkEmpresaODataRoute",
               routePrefix: "LinxFrameworkEmpresaOData",
               model: GetEdmModel(), pathHandler: new DefaultODataPathHandler(), routingConventions: conventions
               );
        }
        
        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<BusinessNS.TcsEmpresaAutenticacao>("TcsEmpresaAutenticacao");
            modelBuilder.EntitySet<BusinessNS.TcsEmpresaGpecon>("TcsEmpresaGpecon");
            modelBuilder.EntitySet<BusinessNS.TcsEmpresaGpeconParentComposition>("TcsEmpresaGpeconParentComposition");
            modelBuilder.EntitySet<BusinessNS.TcsAmbiente>("TcsAmbiente");
            modelBuilder.EntitySet<BusinessNS.TcsAmbienteParentComposition>("TcsAmbienteParentComposition");
            modelBuilder.EntitySet<BusinessNS.TcsEmpresaModulo>("TcsEmpresaModulo");
            modelBuilder.EntitySet<BusinessNS.TcsEmpresaModuloParentComposition>("TcsEmpresaModuloParentComposition");
            modelBuilder.EntitySet<BusinessNS.TcsUsuarioAutenticacao>("TcsUsuarioAutenticacao");
            modelBuilder.EntitySet<BusinessNS.TcsUsuarioAutenticacaoParentComposition>("TcsUsuarioAutenticacaoParentComposition");
            modelBuilder.EntitySet<BusinessNS.TcsEmpresaGpeconP>("TcsEmpresaGpeconP");
            modelBuilder.EntitySet<BusinessNS.TcsEmpresaAutenticacaoP>("TcsEmpresaAutenticacaoP");
            return modelBuilder.GetEdmModel();
        }
    }
}

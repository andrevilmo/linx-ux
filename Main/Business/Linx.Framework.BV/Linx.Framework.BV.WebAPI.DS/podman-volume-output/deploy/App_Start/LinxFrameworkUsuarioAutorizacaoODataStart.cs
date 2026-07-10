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
using BusinessNS = Linx.Framework.BV.UsuarioAutorizacao;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Linx.Framework.BV.WebAPI.DS.App_Start.LinxFrameworkUsuarioAutorizacaoODataStart), "Start")]

namespace Linx.Framework.BV.WebAPI.DS.App_Start
{

    public static class LinxFrameworkUsuarioAutorizacaoODataStart
    {
        public static void Start()
        {
           var conventions = ODataRoutingConventions.CreateDefault();
           conventions.Insert(0, new Linx.DataService.ODataContainmentRoutingConvention("LinxFrameworkUsuarioAutorizacaoFeed"));
           GlobalConfiguration.Configuration.Routes.MapODataServiceRoute(
               routeName: "LinxFrameworkUsuarioAutorizacaoODataRoute",
               routePrefix: "LinxFrameworkUsuarioAutorizacaoOData",
               model: GetEdmModel(), pathHandler: new DefaultODataPathHandler(), routingConventions: conventions
               );
        }
        
        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<BusinessNS.TcsUsuarioAutenticacao>("TcsUsuarioAutenticacao");
            modelBuilder.EntitySet<BusinessNS.TcsUsuarioAcesso>("TcsUsuarioAcesso");
            modelBuilder.EntitySet<BusinessNS.TcsUsuarioAcessoParentComposition>("TcsUsuarioAcessoParentComposition");
            modelBuilder.EntitySet<BusinessNS.TcsIdentidadeExterna>("TcsIdentidadeExterna");
            modelBuilder.EntitySet<BusinessNS.TcsIdentidadeExternaParentComposition>("TcsIdentidadeExternaParentComposition");
            modelBuilder.EntitySet<BusinessNS.RequisicaoAcesso>("RequisicaoAcesso");
            modelBuilder.EntitySet<BusinessNS.UsuarioAcesso>("UsuarioAcesso");
            modelBuilder.EntitySet<BusinessNS.TcsSuporteAcessoLog>("TcsSuporteAcessoLog");
            modelBuilder.EntitySet<BusinessNS.RequisicaoSuporte>("RequisicaoSuporte");
            modelBuilder.EntitySet<BusinessNS.TcsUsuarioAcessoAmbiente>("TcsUsuarioAcessoAmbiente");
            modelBuilder.EntitySet<BusinessNS.TcsUsuarioAutenticacaoAcessoP>("TcsUsuarioAutenticacaoAcessoP");
            modelBuilder.EntitySet<BusinessNS.TcsUsuarioGpecon>("TcsUsuarioGpecon");
            modelBuilder.EntitySet<BusinessNS.TcsUsuarioGpeconParentComposition>("TcsUsuarioGpeconParentComposition");
            return modelBuilder.GetEdmModel();
        }
    }
}

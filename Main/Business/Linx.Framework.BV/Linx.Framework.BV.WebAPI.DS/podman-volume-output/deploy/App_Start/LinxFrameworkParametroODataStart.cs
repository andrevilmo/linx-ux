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
using BusinessNS = Linx.Framework.BV.Parametro;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Linx.Framework.BV.WebAPI.DS.App_Start.LinxFrameworkParametroODataStart), "Start")]

namespace Linx.Framework.BV.WebAPI.DS.App_Start
{

    public static class LinxFrameworkParametroODataStart
    {
        public static void Start()
        {
           var conventions = ODataRoutingConventions.CreateDefault();
           conventions.Insert(0, new Linx.DataService.ODataContainmentRoutingConvention("LinxFrameworkParametroFeed"));
           GlobalConfiguration.Configuration.Routes.MapODataServiceRoute(
               routeName: "LinxFrameworkParametroODataRoute",
               routePrefix: "LinxFrameworkParametroOData",
               model: GetEdmModel(), pathHandler: new DefaultODataPathHandler(), routingConventions: conventions
               );
        }
        
        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<BusinessNS.TcsParametro>("TcsParametro");
            modelBuilder.EntitySet<BusinessNS.TcsParametroValorP>("TcsParametroValorP");
            modelBuilder.EntitySet<BusinessNS.TcsParametroValorVariacaoP>("TcsParametroValorVariacaoP");
            modelBuilder.EntitySet<BusinessNS.TcsParametroTabelaSelecao>("TcsParametroTabelaSelecao");
            modelBuilder.EntitySet<BusinessNS.TcsParametroValor>("TcsParametroValor");
            modelBuilder.EntitySet<BusinessNS.TcsUsuarioParametro>("TcsUsuarioParametro");
            modelBuilder.EntitySet<BusinessNS.TcsParametroValorUsuario>("TcsParametroValorUsuario");
            modelBuilder.EntitySet<BusinessNS.TbcBandeiraRedeParametro>("TbcBandeiraRedeParametro");
            modelBuilder.EntitySet<BusinessNS.TcsParametroValorRede>("TcsParametroValorRede");
            modelBuilder.EntitySet<BusinessNS.TbcGrupoEconomicoParametro>("TbcGrupoEconomicoParametro");
            modelBuilder.EntitySet<BusinessNS.TcsParametroValorGpecon>("TcsParametroValorGpecon");
            modelBuilder.EntitySet<BusinessNS.TbcFilialParametro>("TbcFilialParametro");
            modelBuilder.EntitySet<BusinessNS.TcsParametroValorFilial>("TcsParametroValorFilial");
            modelBuilder.EntitySet<BusinessNS.TcsParametroValorLjvLoja>("TcsParametroValorLjvLoja");
            modelBuilder.EntitySet<BusinessNS.TcsParametroValorVariacaoGenericaP>("TcsParametroValorVariacaoGenericaP");
            modelBuilder.EntitySet<BusinessNS.TcsParametroValorVariacaoGenerica>("TcsParametroValorVariacaoGenerica");
            modelBuilder.EntitySet<BusinessNS.TcsParametroValorP1>("TcsParametroValorP1");
            modelBuilder.EntitySet<BusinessNS.ParametroInfo>("ParametroInfo");
            modelBuilder.EntitySet<BusinessNS.TcsParametroValorP2>("TcsParametroValorP2");
            modelBuilder.EntitySet<BusinessNS.TcsParametroValorLojaP>("TcsParametroValorLojaP");
            modelBuilder.EntitySet<BusinessNS.LjvLojaParametro>("LjvLojaParametro");
            modelBuilder.EntitySet<BusinessNS.TcsParametroValorUsuarioP>("TcsParametroValorUsuarioP");
            modelBuilder.EntitySet<BusinessNS.TcsParametroValorRedeP>("TcsParametroValorRedeP");
            modelBuilder.EntitySet<BusinessNS.TcsParametroValorGpeconP>("TcsParametroValorGpeconP");
            modelBuilder.EntitySet<BusinessNS.TcsParametroValorFilialP>("TcsParametroValorFilialP");
            modelBuilder.EntitySet<BusinessNS.TcsParametroValorVariacaoGenericaP1>("TcsParametroValorVariacaoGenericaP1");
            return modelBuilder.GetEdmModel();
        }
    }
}

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
using BusinessNS = Linx.Framework.BV.Mensagem;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Linx.Framework.BV.WebAPI.DS.App_Start.LinxFrameworkMensagemODataStart), "Start")]

namespace Linx.Framework.BV.WebAPI.DS.App_Start
{

    public static class LinxFrameworkMensagemODataStart
    {
        public static void Start()
        {
           var conventions = ODataRoutingConventions.CreateDefault();
           conventions.Insert(0, new Linx.DataService.ODataContainmentRoutingConvention("LinxFrameworkMensagemFeed"));
           GlobalConfiguration.Configuration.Routes.MapODataServiceRoute(
               routeName: "LinxFrameworkMensagemODataRoute",
               routePrefix: "LinxFrameworkMensagemOData",
               model: GetEdmModel(), pathHandler: new DefaultODataPathHandler(), routingConventions: conventions
               );
        }
        
        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<BusinessNS.TcsMensagem>("TcsMensagem");
            modelBuilder.EntitySet<BusinessNS.MensagemInfo>("MensagemInfo");
            modelBuilder.EntitySet<BusinessNS.TcsMensagemUsuario>("TcsMensagemUsuario");
            modelBuilder.EntitySet<BusinessNS.TcsMensagemLog>("TcsMensagemLog");
            modelBuilder.EntitySet<BusinessNS.TcsPerfil>("TcsPerfil");
            modelBuilder.EntitySet<BusinessNS.TcsUsuario>("TcsUsuario");
            modelBuilder.EntitySet<BusinessNS.NewMessageInfo>("NewMessageInfo");
            modelBuilder.EntitySet<BusinessNS.TcsMensagemLogDetail>("TcsMensagemLogDetail");
            modelBuilder.EntitySet<BusinessNS.TcsMensagemLogDetailParentComposition>("TcsMensagemLogDetailParentComposition");
            modelBuilder.EntitySet<BusinessNS.TcsMensagemConsulta>("TcsMensagemConsulta");
            modelBuilder.EntitySet<BusinessNS.TcsMensagemConsultaLog>("TcsMensagemConsultaLog");
            modelBuilder.EntitySet<BusinessNS.TcsMensagemConsultaLogParentComposition>("TcsMensagemConsultaLogParentComposition");
            return modelBuilder.GetEdmModel();
        }
    }
}

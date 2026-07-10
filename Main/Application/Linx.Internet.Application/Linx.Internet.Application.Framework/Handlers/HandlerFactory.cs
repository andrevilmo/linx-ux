using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;

namespace Linx.Internet.Application.Framework.Handlers
{
    public class FactoryHandler : IHttpHandlerFactory
    {
        public IHttpHandler GetHandler(HttpContext context,string requestType, String url, String pathTranslated)
        {
            IHttpHandler handlerToReturn = null;
            var id = context.Request.QueryString.GetValueOrDefault<string>("id");

            if (id == null)
            {
                context.Response.Write("Referencia invalida!");
            }
            else
            {

                if (!Linx.Internet.Application.Framework.Web.PluginConfig.CurrentHandles.ContainsKey(id))
                {
                    context.Response.Write("Handler [" + id + "] nao encontrado!");
                }
                else
                {
                    return Linx.Internet.Application.Framework.Web.PluginConfig.CurrentHandles[id];
                }
            }

            return handlerToReturn;
        }

        public void ReleaseHandler(IHttpHandler handler)
        {
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace Linx.SelfHost
{
    public static class LibConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            WebAPILoader loader = new WebAPILoader();
            config.Services.Replace(typeof(IAssembliesResolver), loader);
        }
    }
}

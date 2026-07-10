using System;
using Linx.Internet.Application.Common.Filters;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AttributeRouting.Web.Mvc;
using Linx.Internet.Application.Common.Providers;
using RestSharp;
using System.Net;
using System.Configuration;
using System.Text;
using Linx.Internet.Application.Helpers;
using System.Linq;

namespace Linx.Internet.Application.Controllers
{
    public class AppCacheController : Controller
    {
        [NoCache]
        [GET("lia.appcache")]
        public ActionResult AppCache()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("CACHE MANIFEST");
            sb.AppendLine("# " + BaseHelpers.NumeroVersaoAssembly); // + " " + BaseHelpers.QueryStringNoCache);

            if (BaseHelpers.CheckApplicationCache())
            {
                sb.AppendLine(BaseHelpers.BuildUrl("/core.css"));
                sb.AppendLine(BaseHelpers.BuildUrl("/theme-css-default.less"));
                sb.AppendLine(BaseHelpers.BuildUrl("/theme-css-orange.less"));
                sb.AppendLine(BaseHelpers.BuildUrl("/theme-css-black.less"));

                sb.AppendLine(BaseHelpers.BuildUrl("/start.js"));
                sb.AppendLine(BaseHelpers.BuildUrl("/core.js"));

                var itens = Linx.Internet.Application.Framework.Web.PluginConfig.EmbeddedResources
                    .Where(
                        w => w.Value.Extension != ".cshtml"
                        && w.Value.Extension != ".ico"
                        )
                    .OrderBy(o => o.Key);
                foreach (var item in itens)
                {
                    sb.AppendLine(item.Value.Url);

                    if (item.Value.Url.Contains("lib/linx/img/modules"))
                    {
                        sb.AppendLine(string.Concat(item.Value.Url, "?width=64"));
                    }
                }
                sb.AppendLine(BaseHelpers.BuildUrl("/lib/metronic/plugins/select2/select2.png"));
                sb.AppendLine(BaseHelpers.BuildUrl("/lib/linx/img/no-image.png?width=64"));
                sb.AppendLine(BaseHelpers.BuildUrl("/scripts/requirejs/__config.js"));
                sb.AppendLine(BaseHelpers.BuildUrl("/App/managers/__route.js"));
                sb.AppendLine(BaseHelpers.BuildUrl("/AppLogin/managers/__route.js"));
            }

            sb.AppendLine("");            
            sb.AppendLine("NETWORK:");
            sb.AppendLine("*");
            sb.AppendLine("/*.axd");
            sb.AppendLine("/tools/*");
            sb.AppendLine("/?appcache=off");
            sb.AppendLine("/authentication");
            sb.AppendLine("/unauthorized");
            sb.AppendLine("/logoff");
            sb.AppendLine("/logoffforpasswordchange");
            sb.AppendLine(BaseHelpers.BuildUrl("/App/managers/__auth.js"));
            sb.AppendLine(BaseHelpers.BuildUrl("/AppLogin/managers/__auth.js"));
            
            sb.AppendLine("");            
            sb.AppendLine("FALLBACK:");

            return Content(sb.ToString(), "text/cache-manifest", UTF8Encoding.UTF8);
            //return Content(sb.ToString(), "application/x-ms-manifest", UTF8Encoding.UTF8);
            
        }

    }
}

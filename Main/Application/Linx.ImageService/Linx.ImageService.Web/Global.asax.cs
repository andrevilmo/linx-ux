using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Linx.ImageService.Web
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_BeginRequest()
        {
            //NOTE: Stopping IE from being a caching whore
            //HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(false);
            //HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //HttpContext.Current.Response.Cache.SetNoStore();
            //Response.Cache.SetExpires(DateTime.Now);
            //Response.Cache.SetValidUntilExpires(true);
        }
    }
}
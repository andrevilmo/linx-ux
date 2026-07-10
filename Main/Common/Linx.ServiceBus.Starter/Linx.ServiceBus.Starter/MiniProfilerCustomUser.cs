using StackExchange.Profiling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Linx.ServiceBus.Starter
{
    public class MiniProfilerCustomUser : IUserProvider
    {
        public string GetUser(HttpRequest request)
        {
            if (!string.IsNullOrEmpty(request.Headers["SessionId"]))
            {
                return request.Headers["SessionId"];
            }
            else if (request.Cookies["ASP.NET_SessionId"] != null)
            {
                return request.Cookies["ASP.NET_SessionId"].Value;
            }
            else if (!string.IsNullOrEmpty(request.QueryString["SessionId"]))
            {
                return request.QueryString["SessionId"];
            }

            return "anonymous";
        }
    }
}
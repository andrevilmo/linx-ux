using StackExchange.Profiling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Linx.Internet.Application.Class
{
    public class MiniProfilerCustomUser : IUserProvider
    {
        public string GetUser(HttpRequest request)
        {
            if (request.Cookies["ASP.NET_SessionId"] != null)
            {
                return request.Cookies["ASP.NET_SessionId"].Value;
            }

            return "anonymous";
        }
    }
}
using System;
using System.Collections.Generic;
using System.Web;

namespace Linx.Tools
{
    public static class Helper
    {
        public static Dictionary<string, string> getHeaders(HttpRequest request)
        {
            var headers = new Dictionary<string, string>();
            foreach (string key in request.Headers.AllKeys)
            {
                headers.Add(key, request.Headers[key]);
            }
            return headers;
        }

        public static int? ParseInt(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;
            return int.Parse(value);
        }
        public static Guid? ParseGuid(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;
            return Guid.Parse(value);
        }
    }
}
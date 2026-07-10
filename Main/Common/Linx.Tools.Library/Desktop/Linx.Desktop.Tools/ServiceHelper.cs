using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.Collections.Specialized;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using System.Web;


namespace Linx.Tools
{
    public static class ServiceHelper
    {
        public static string RequestBody()
        {
            var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream);
            bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
            var bodyText = bodyStream.ReadToEnd();
            HttpContext.Current.Request.InputStream.Seek(0, SeekOrigin.Begin);
            return bodyText;
        }

        public static string GetMessageProperty(string propertyName)
        {
            return GetMessageProperty(propertyName, null);
        }

        public static Dictionary<string, string> GetHttpHeaders()
        {
            Dictionary<string, string> result = null;
            if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.Request != null && System.Web.HttpContext.Current.Request.Headers != null)
            {
                result = new Dictionary<string, string>();
                foreach (string key in System.Web.HttpContext.Current.Request.Headers.AllKeys)
                {
                    result.Add(key, System.Web.HttpContext.Current.Request.Headers[key]);
                }
            }
            else if (LinxHttpContext.HttpContext.Current != null && LinxHttpContext.HttpContext.Current.Request != null && LinxHttpContext.HttpContext.Current.Request.Inner != null)
            {
                var headers = LinxHttpContext.HttpContext.Current.Request.Inner.GetPropertyValue("Headers") as System.Net.Http.Headers.HttpRequestHeaders;
                if (headers != null)
                {
                    result = new Dictionary<string, string>();
                    var mHeaders = headers.ToDictionary();
                    foreach (var key in mHeaders.Keys)
                    {
                        result.Add(key, mHeaders[key]);
                    }
                }
            }            
            return result;
        }

        public static string GetMessageProperty(string propertyName, Dictionary<string, string> headers)
        {
            if (propertyName == "*DevMode*")
                return LocalServiceBus.DevMode.ToString().ToLower();
            
            string propertyValue = String.Empty;

            if (headers == null)
                headers = GetHttpHeaders();
           
            if (headers != null && headers.Count > 0)
            {
                if (headers.ContainsKey(propertyName))
                    propertyValue = headers[propertyName];
                else if (headers.ContainsKey(propertyName.ToLower()))
                    propertyValue = headers[propertyName.ToLower()];
                else if (headers.ContainsKey(propertyName.ToUpper()))
                    propertyValue = headers[propertyName.ToUpper()];
            }
            
            return propertyValue;
        }

        public static AuthorizationType GetAuth(this HttpMethod method)
        {
            var auth = AuthorizationType.Query;
            switch (method.ToString().ToUpper())
            {
                case "DELETE":
                    auth = AuthorizationType.Delete;
                    break;
                case "PUT":
                    auth = AuthorizationType.Update;
                    break;
                case "POST":
                    auth = AuthorizationType.Insert;
                    break;
                case "GET":
                    auth = AuthorizationType.Query;
                    break;
            }
            return auth;
        }

        public static Dictionary<string, string> ToDictionary(this HttpRequestHeaders headers)
        {
            return headers
                .ToDictionary(
                    h => h.Key,
                    h => h.Value == null ? string.Empty : h.Value.First());
        }
    }
}

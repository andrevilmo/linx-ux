using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Tools
{
    public class HttpHelper
    {
        private const string HttpRequestMessage = "MS_HttpRequestMessage";
        private const string HttpContext = "MS_HttpContext";
        private const string RemoteEndpointMessage = "System.ServiceModel.Channels.RemoteEndpointMessageProperty";

        public static string GetInfo()
        {
            string result = "";
            
            try
            {
                var request = System.Web.HttpContext.Current?.Request;

                if (request != null)
                {
                    result += Environment.NewLine + "  WebApi Call: " + request.Path;
                    result += Environment.NewLine + "  Http Method: " + request.HttpMethod;
                    var loginName = request.LogonUserIdentity?.Name;
                    if (!loginName.IsNullOrEmpty())
                    {
                        result += Environment.NewLine + "  User Identity: " + loginName;
                    }

                    var items = System.Web.HttpContext.Current?.Items;
                    if (items != null && items.Count > 0)
                    {
                        if (items.Contains(HttpRequestMessage))
                        {
                            var reqMsg = items[HttpRequestMessage] as HttpRequestMessage;
                            if (reqMsg != null)
                            {
                                var clientAddress = GetClientIpAddress(reqMsg);
                                if (!clientAddress.IsNullOrEmpty())
                                {
                                    result += Environment.NewLine + "  Client Address: " + clientAddress;
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                result = "";
            }

           return result;
        }
        
        private static string GetClientIpAddress(HttpRequestMessage request)
        {
            if (request.Properties != null && request.Properties.Count > 0)
            {
                if (request.Properties.ContainsKey(HttpContext))
                {
                    dynamic ctx = request.Properties[HttpContext];
                    if (ctx != null)
                    {
                        return ctx.Request.UserHostAddress;
                    }
                }

                if (request.Properties.ContainsKey(RemoteEndpointMessage))
                {
                    dynamic remoteEndpoint = request.Properties[RemoteEndpointMessage];
                    if (remoteEndpoint != null)
                    {
                        return remoteEndpoint.Address;
                    }
                }
            }

            return null;
        }

    }
}

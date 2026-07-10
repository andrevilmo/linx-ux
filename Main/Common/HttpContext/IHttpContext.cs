using System;
using System.Collections;
using System.Security.Principal;

namespace LinxHttpContext
{
    public interface IHttpContext
    {
        DateTime Timestamp { get; }
        IHttpRequest Request { get; }
        IHttpResponse Response { get; }
        IDictionary Items { get; }
        IPrincipal User { get; set; }
        object Inner { get; }
    }
}
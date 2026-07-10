using System.Net.Http;

namespace LinxHttpContext.SelfHost
{
    public class SelfHostHttpResponse : IHttpResponse
    {
        public SelfHostHttpResponse(HttpResponseMessage response)
        {
            Inner = response;
        }

        public object Inner { get; private set; }
    }
}
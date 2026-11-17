using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http.Filters;

namespace Linx.ServiceBus.Starter.ErrorHandling
{
    public class GenericExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var correlationId = Guid.NewGuid().ToString("N");

            try
            {
                ExceptionLogger.Instance.LogError(context);
            }
            catch
            {
                // Logging must not prevent a safe client response.
            }

            context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            // Never serialize exception/stack to the client — only a generic message + correlation id.
            var payload = new
            {
                Message = "Ocorreu um erro interno.",
                CorrelationId = correlationId
            };
            context.Response.Content = new JsonContent(JsonConvert.SerializeObject(payload));
        }

        public class JsonContent : StringContent
        {
            public JsonContent(string content) : this(content, Encoding.UTF8) { }

            public JsonContent(string content, Encoding encoding)
                : base(content, encoding, "application/json") { }
        }
    }
}

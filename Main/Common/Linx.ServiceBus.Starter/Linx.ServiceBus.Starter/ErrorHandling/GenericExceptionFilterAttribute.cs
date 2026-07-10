using Linx.Tools;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Filters;

namespace Linx.ServiceBus.Starter.ErrorHandling
{
    public class GenericExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var info = ExceptionLogger.Instance.LogError(context.Exception,
               context.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerName,
               context.ActionContext.ActionDescriptor.ActionName);

            context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            context.Response.Content = new JsonContent(new HttpError(context.Exception, true));
        }

        public class JsonContent : StringContent
        {
            public JsonContent(string content) : this(content, Encoding.UTF8) { }
            public JsonContent(HttpError content) : this(JsonConvert.SerializeObject(content), Encoding.UTF8) { }

            public JsonContent(string content, Encoding encoding)
                : base(content, encoding, "application/json") { }
        }
    }
}
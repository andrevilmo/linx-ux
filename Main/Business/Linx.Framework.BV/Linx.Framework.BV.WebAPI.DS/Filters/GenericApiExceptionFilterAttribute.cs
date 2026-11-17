using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Filters;

namespace Linx.Framework.BV.WebAPI.DS.Filters
{
    /// <summary>
    /// Catches unhandled Web API exceptions, logs full detail server-side,
    /// and returns a generic JSON body (no stack / type leak) to the client.
    /// </summary>
    public class GenericApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context == null || context.Exception == null)
                return;

            var correlationId = Guid.NewGuid().ToString("N");
            var controller = context.ActionContext != null
                && context.ActionContext.ActionDescriptor != null
                && context.ActionContext.ActionDescriptor.ControllerDescriptor != null
                    ? context.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerName
                    : "?";
            var action = context.ActionContext != null
                && context.ActionContext.ActionDescriptor != null
                    ? context.ActionContext.ActionDescriptor.ActionName
                    : "?";

            LogException(correlationId, controller, action, context.Exception);

            var payload = new
            {
                Message = "Ocorreu um erro interno.",
                CorrelationId = correlationId
            };

            context.Response = context.Request.CreateResponse(HttpStatusCode.InternalServerError, payload);
        }

        static void LogException(string correlationId, string controller, string action, Exception ex)
        {
            var line = string.Format(
                "[{0:o}] CorrelationId={1} Controller={2} Action={3} {4}: {5}{6}{7}",
                DateTime.UtcNow,
                correlationId,
                controller,
                action,
                ex.GetType().FullName,
                ex.Message,
                Environment.NewLine,
                ex.ToString());

            try { Trace.TraceError(line); }
            catch { /* ignore */ }

            try
            {
                var root = HttpContext.Current != null
                    ? HttpContext.Current.Server.MapPath("~/App_Data")
                    : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data");
                if (!Directory.Exists(root))
                    Directory.CreateDirectory(root);
                var file = Path.Combine(root, string.Format("errors{0:yyyyMMdd}.txt", DateTime.UtcNow));
                File.AppendAllText(file, line + Environment.NewLine, Encoding.UTF8);
            }
            catch { /* ignore file logging failures */ }
        }
    }
}

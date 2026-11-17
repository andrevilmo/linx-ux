using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Linx.Security.Core
{
    public class JsonExceptionMiddleware
    {
        public async Task Invoke(HttpContext context)
        {
            await this.GetError(context);
        }

        public async Task GetError(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            var ex = context.Features.Get<IExceptionHandlerFeature>()?.Error;

            if (ex is LinxAuthorizationException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            else if (context.Response.StatusCode < 400)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            var correlationId = Guid.NewGuid().ToString("N");

            // Full detail stays server-side only.
            if (ex != null)
            {
                try
                {
                    Console.Error.WriteLine(
                        "[{0}] CorrelationId={1} {2}: {3}{4}{5}",
                        DateTime.UtcNow.ToString("o"),
                        correlationId,
                        ex.GetType().FullName,
                        ex.Message,
                        Environment.NewLine,
                        ex);
                }
                catch { /* ignore */ }
            }

            var error = new LinxError
            {
                StatusCode = context.Response.StatusCode,
                StatusDescription = ((HttpStatusCode)context.Response.StatusCode).ToString(),
                Message = "Ocorreu um erro interno.",
                CorrelationId = correlationId
            };

            using (var writer = new StreamWriter(context.Response.Body))
            {
                new JsonSerializer().Serialize(writer, error);
                await writer.FlushAsync().ConfigureAwait(false);
            }
        }
    }

    public class LinxError
    {
        public int StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public string Message { get; set; }
        public string CorrelationId { get; set; }
    }
}

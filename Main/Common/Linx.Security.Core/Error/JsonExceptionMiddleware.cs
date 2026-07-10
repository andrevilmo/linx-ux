using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
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
            //context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            context.Response.ContentType = "application/json";

            var ex = context.Features.Get<IExceptionHandlerFeature>()?.Error;

            if (ex is LinxAuthorizationException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }

            LinxError error = new LinxError() { StatusCode = context.Response.StatusCode, StatusDescription = ((System.Net.HttpStatusCode)context.Response.StatusCode).ToString() };

            if (ex != null)
            {
                error.Message = ex.Message;
                error.InnerException = ex.InnerException;
                error.StackTrace = ex.StackTrace;
            }

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
        public System.Exception InnerException { get; set; }
        public string StackTrace { get; set; }
    }

}

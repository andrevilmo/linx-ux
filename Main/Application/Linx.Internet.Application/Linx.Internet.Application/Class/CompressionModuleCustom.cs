using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WebMarkupMin.Core;
using WebMarkupMin.Web.Helpers;
using WebMarkupMin.Web;


namespace Linx.Internet.Application.Class
{
    public sealed class CompressionModuleCustom : IHttpModule
    {
        /// <summary>
        /// Initializes a module and prepares it to handle requests
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpApplication"></see>
        /// that provides access to the methods, properties, and events common to
        /// all application objects within an ASP.NET application
        /// </param>
        public void Init(HttpApplication context)
        {
            context.PreRequestHandlerExecute += PreRequestHandlerExecute;
            context.Error += ProcessError;
        }

        /// <summary>
        /// Handles the BeginRequest event of the context control.
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data</param>
        private static void PreRequestHandlerExecute(object sender, EventArgs e)
        {
            if (!WebMarkupMinContext.Current.IsCompressionEnabled())
            {
                return;
            }

            HttpContext context = ((HttpApplication)sender).Context;
            HttpRequest request = context.Request;

            if (context.Request.Url.LocalPath.Contains("/mini-profiler-resources"))
            {
                return;
            }

            HttpResponse response = context.Response;
            string contentType = response.ContentType;

            if (request.HttpMethod == "GET" && response.StatusCode == 200
                && CompressionHelper.IsCompressionSupported(request)
                && ContentTypeHelper.IsTextBasedContentType(contentType))
            {
                context.Items["originalResponseFilter"] = response.Filter;
                CompressionHelper.TryCompressContent(context);
            }
        }

        /// <summary>
        /// Handles the Error event of the context control
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data</param>
        private static void ProcessError(object sender, EventArgs e)
        {
            HttpContext context = ((HttpApplication)sender).Context;
            if (context.Error != null && context.Items.Contains("originalResponseFilter"))
            {
                var originalResponseFilter = context.Items["originalResponseFilter"] as Stream;
                if (originalResponseFilter != null)
                {
                    context.Response.Filter = originalResponseFilter;
                }
            }
        }

        /// <summary>
        /// Destroys object
        /// </summary>
        public void Dispose()
        {
            // Nothing to destroy
        }
    }
}
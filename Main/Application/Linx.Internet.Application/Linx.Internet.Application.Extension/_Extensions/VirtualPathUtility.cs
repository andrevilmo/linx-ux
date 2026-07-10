// -----------------------------------------------------------------------
// <copyright file="ConfigurationExtension.cs" company="Linx Sistemas">
// Copyright (c) Linx Sistemas. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Linx.Internet.Application
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text;
    using System.Web;

    /// <summary>
    /// Classe ConfigurationExtension
    /// </summary>
    public static class HttpServerUtilityExtension
    {
        /// <summary>
        /// Returns a site relative HTTP path from a partial path starting out with a ~.
        /// Same syntax that ASP.Net internally supports but this method can be used
        /// outside of the Page framework.
        /// 
        /// Works like Control.ResolveUrl including support for ~ syntax
        /// but returns an absolute URL.
        /// </summary>
        /// <param name="originalUrl">Any Url including those starting with ~</param>
        /// <returns>relative url</returns>
        public static string ResolveUrl(this System.Web.HttpServerUtility reference, string originalUrl, string moduleId = null)
        {
            if (originalUrl == null)
                return null;

            // *** Absolute path - just return
            if (originalUrl.IndexOf("://") != -1)
                return originalUrl;

            // *** Fix up image path for ~ root app dir directory
            if (!originalUrl.StartsWith("~"))
            {
                originalUrl = string.Concat("~/", originalUrl);
            }

            /*
            
             * HttpContext.Current.Request.ApplicationPath
            "/Linx.Internet.Application"

            VirtualPathUtility.ToAbsolute(originalUrl)
            "/Linx.Internet.Application/lib/linx/img/logo-linx.png"
             * 
            */

            if (moduleId == null)
                moduleId = string.Empty;
            else
                moduleId = string.Concat("/", moduleId);

            if (HttpContext.Current != null)
            {
                var path = VirtualPathUtility.ToAbsolute(originalUrl);
                if (path == "/")
                    return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
                else
                {
                    if (HttpContext.Current.Request.ApplicationPath == "/")
                        return string.Concat(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority), HttpContext.Current.Request.ApplicationPath, moduleId, VirtualPathUtility.ToAbsolute(originalUrl));
                    else
                    {
                        var virtualPath = VirtualPathUtility.ToAbsolute(originalUrl).Replace(HttpContext.Current.Request.ApplicationPath, string.Empty);
                        return string.Concat(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority), HttpContext.Current.Request.ApplicationPath, moduleId, virtualPath);
                    }
                }
            }
            else
                // *** Not context: assume current directory is the base directory
                throw new ArgumentException("Invalid URL: Relative URL not allowed.");
        }
    }
}

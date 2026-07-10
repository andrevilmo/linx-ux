//// -----------------------------------------------------------------------
// <copyright file="HtmlHelper.cs" company="Linx Sistemas">
// Copyright (c) Linx Sistemas. All rights reserved.
// </copyright>
//// -----------------------------------------------------------------------

namespace Linx.Internet.Application.Helpers
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Hosting;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;
    using Linx.Internet.Application;
    using RestSharp;
    using System.Net;

    /// <summary>
    /// Classe responsável por auxiliar projeto com metodos e propriedades
    /// </summary>
    public static class HtmlHelper
    {
        public enum EnumTypeFile
        {
            Css,
            Js
        }

        //public static MvcHtmlString EmbedCss(this System.Web.Mvc.HtmlHelper htmlHelper, string path)
        //{
        //    var content = new System.Net.WebClient().DownloadString(HttpContext.Current.Server.ResolveUrl(path));
        //    var element = new TagBuilder("style");
            
        //    element.MergeAttribute("type", "text/css");
        //    element.SetInnerText(content);

        //    return MvcHtmlString.Create(element.ToString());
        //}

        public static MvcHtmlString EmbedBase64(this System.Web.Mvc.HtmlHelper htmlHelper, EnumTypeFile type, string path, string idValue = null)
        {
            // verifica se o item esta no AspNetCache
            string cacheKey = string.Concat("EmbedBase64|", path);
            object cacheItem = HttpContext.Current.Cache[cacheKey];
            MvcHtmlString retorno = null;

            if (cacheItem == null)
            {
                string tagAttribute = (type == EnumTypeFile.Css ? "link" : "script");
                string typeAttribute = (type == EnumTypeFile.Css ? "text/css" : "application/javascript");
                string srcAttribute = (type == EnumTypeFile.Css ? "href" : "src");

                // requisita a pagina
                var client = new RestClient();
                var request = new RestRequest(HttpContext.Current.Server.ResolveUrl(path), Method.GET);
                IRestResponse response = client.Execute(request);

                // adiciona o root da url "http://localhost"
                StringBuilder sb = new StringBuilder(response.Content);
                var root = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/"; //HttpContext.Current.Server.ResolveUrl("/");
                sb.Replace("url('", string.Concat("url('", root));
                sb.Replace("url(\"", string.Concat("url(\"", root));

                //sb.Replace("url('/", "url('");
                //sb.Replace("url(\"/", "url(\"");
                //sb.Replace("url(/", "url(");
                byte[] contentArray = System.Text.Encoding.UTF8.GetBytes(sb.ToString());

                var element = new TagBuilder(tagAttribute);
                element.MergeAttribute("type", typeAttribute);
                if (type == EnumTypeFile.Css)
                {
                    element.MergeAttribute("rel", "stylesheet");
                    element.MergeAttribute(srcAttribute, string.Concat("data:", typeAttribute, ";charset=UTF-8;base64,", System.Convert.ToBase64String(contentArray, Base64FormattingOptions.None)));
                    if (!string.IsNullOrEmpty(idValue))
                    {
                        element.MergeAttribute("id", idValue);
                    }
                }
                else
                {
                    element.MergeAttribute(srcAttribute, string.Concat("data:", typeAttribute, ";charset=UTF-8;base64,", System.Convert.ToBase64String(contentArray, Base64FormattingOptions.None)));
                }

                //<script type="text/javascript" src="data:text/javascript;base64,dmFyIHNjT2JqMSA9IG5ldyBzY3Jv..."/>
                //<link rel="stylesheet" type="text/css" href="data:text/css;base64,LyogKioqKiogVGVtcGxhdGUgKioq..." />
                retorno = MvcHtmlString.Create(element.ToString());
                HttpContext.Current.Cache[cacheKey] = retorno;
            }
            else
            {
                retorno = cacheItem as MvcHtmlString;
            }

            return retorno;
        }

        public static IHtmlString MetaVersion<t>(this HtmlHelper<t> html)
        {
            return new HtmlString(string.Format("<meta name=\"{0}\" content=\"{1}\">", "linx-internet-application-version", BaseHelpers.NumeroVersaoAssembly));
        }

        public static IHtmlString MetaVersionLabel<t>(this HtmlHelper<t> html)
        {
            return new HtmlString(string.Format("<meta name=\"{0}\" content=\"{1}\">", "linx-internet-application-version-label", BaseHelpers.NumeroVersaoAssemblyLabel));
        }

        public static IHtmlString MetaDateVersion<t>(this HtmlHelper<t> html)
        {
            return new HtmlString(string.Format("<meta name=\"{0}\" content=\"{1}\">", "linx-internet-application-date-version", BaseHelpers.DataVersao.ToString("dd/MM/yyyy HH:mm")));
        }

        public static IHtmlString StringVersion<t>(this HtmlHelper<t> html)
        {
            return new HtmlString(BaseHelpers.LabelVersao);
        }

        public static IHtmlString MetaLabelVersion<t>(this HtmlHelper<t> html)
        {
            return new HtmlString(string.Format("<meta name=\"{0}\" content=\"{1}\">", "linx-internet-application-label-version", BaseHelpers.LabelVersao));
        }

        public static IHtmlString MetaRoot<t>(this HtmlHelper<t> html)
        {
            return new HtmlString(
                string.Format("<meta name=\"{0}\" content=\"{1}\">", "linx-internet-application-root", GetRoot())
            );
        }

        public static string GetRoot()
        {
            var root = HttpContext.Current.Request.ApplicationPath;

            if (root.EndsWith("/") == false)
            {
                root = string.Concat(root, "/");
            }
            else
            {
                return string.Empty;
            }

            return root;
        }

        public static IHtmlString MetaModuleId<t>(this HtmlHelper<t> html)
        {
            return new HtmlString(
                string.Format("<meta name=\"{0}\" content=\"{1}\">", "linx-internet-application-module-id", ModuleId())
            );
        }

        public static IHtmlString MetaLoginMode<t>(this HtmlHelper<t> html)
        {
            return new HtmlString(
                string.Format("<meta name=\"{0}\" content=\"{1}\">", "linx-internet-application-login-mode", ConfigurationManager.AppSettings.GetValue("Shell.LoginMode", "PORTALUX"))
            );
        }

        public static IHtmlString MetaAppTitle<t>(this HtmlHelper<t> html)
        {
            return new HtmlString(string.Format("<meta name=\"{0}\" content=\"{1}\">", "linx-internet-application-app-title", "Linx UX"));
        }

        public static IHtmlString MetaAppMode<t>(this HtmlHelper<t> html)
        {
            return new HtmlString(string.Format("<meta name=\"{0}\" content=\"{1}\">", "linx-internet-application-app-mode", BaseHelpers.GetShellMode().ToLowerInvariant()));
        }

        public static IHtmlString MetaAppTrace<t>(this HtmlHelper<t> html)
        {
            return new HtmlString(string.Format("<meta name=\"{0}\" content=\"{1}\">", "linx-internet-application-trace-mode", System.Web.HttpContext.Current.Request.QueryStringExistsValue("tracemode", "on", "1", "true").ToString().ToLowerInvariant()));
        }

        public static IHtmlString MetaHash<t>(this HtmlHelper<t> html)
        {
            return new HtmlString(string.Format("<meta name=\"{0}\" content=\"{1}\">", "linx-internet-application-hash", BaseHelpers.QueryStringNoCache));
        }

        public static IHtmlString MetaOffline<t>(this HtmlHelper<t> html)
        {
            return new HtmlString(string.Format("<meta name=\"{0}\" content=\"{1}\">", "linx-internet-application-offline", BaseHelpers.CheckApplicationCache().ToString().ToLowerInvariant()));
        }

        public static IHtmlString MetaMin<t>(this HtmlHelper<t> html)
        {
            return new HtmlString(string.Format("<meta name=\"{0}\" content=\"{1}\">", "linx-internet-application-min", BaseHelpers.GetShellCombineAndMinifyCssJsMode().ToString().ToLowerInvariant()));
        }

        public static IHtmlString MetaTimeOut<t>(this HtmlHelper<t> html)
        {
            return new HtmlString(string.Format("<meta name=\"{0}\" content=\"{1}\">", "linx-internet-application-requirejs-timeout", ConfigurationManager.AppSettings.GetValue<string>("Shell.RequireJS.TimeOut", "7")));
        }

        public static string ContentWithModuleId(this UrlHelper url, string contentPath)
        {
            if (contentPath.StartsWith("~/"))
            {
                return HttpContext.Current.Server.ResolveUrl(contentPath, ModuleId());
            }

            var pathFile = System.IO.Path.GetFileName(url.Content(contentPath));
            return string.Concat("~/", ModuleId(), "/", pathFile);
        }

        public static string RelativePath(this UrlHelper url, string contentPath)
        {
            return string.Concat(GetRoot(), ModuleId(), "/", contentPath);
        }

        //public static string UrlWithModuleIdMin(string contentPath, string minSufix = null)
        //{
        //    if (ConfigurationManager.AppSettings.GetValue<bool>("Shell.CombineAndMinifyCssJs.Enabled", false))
        //    {

        //    }

        //    if (minSufix != null)
        //    {
        //        contentPath = string.Format(contentPath, minSufix);
        //    }

        //    return UrlWithModuleId(contentPath);
        //}

        public static string UrlWithModuleId(string contentPath)
        {
            var pathDir = System.IO.Path.GetDirectoryName(contentPath).Replace("\\", "/");
            var pathFile = System.IO.Path.GetFileName(contentPath);

            return string.Concat("~/", ModuleId(), pathDir, "/", pathFile);
        }
        
        public static string UrlWithModuleId(this UrlHelper url, string contentPath)
        {
            return HtmlHelper.UrlWithModuleId(contentPath);
        }

        public static string Root()
        {
            var root = "/";
            if (HttpContext.Current.Request.Url.Segments.Length > 1)
            {
                if (HttpContext.Current.Request.Url.LocalPath.EndsWith("/"))
                    root = HttpContext.Current.Request.Url.LocalPath;
                else
                    root = string.Concat(HttpContext.Current.Request.Url.LocalPath, "/");
            }

            return root;
        }

        public static string ModuleId()
        {
            return string.Concat("linx-internet-application", "/", BaseHelpers.NumeroVersaoURL);
        }

        public static string ThemeName(this UrlHelper url)
        {
            var cookieStyleMode = HttpContext.Current.Request.Cookies["style_color"];
            var styleClassName = (cookieStyleMode == null ? "default" : cookieStyleMode.Value);

            return styleClassName;
        }

    }
}

////// -----------------------------------------------------------------------
//// <copyright file="HtmlHelper.cs" company="Linx Sistemas">
//// Copyright (c) Linx Sistemas. All rights reserved.
//// </copyright>
////// -----------------------------------------------------------------------

//namespace Linx.Internet.Application.Common.Helpers
//{
//    using System;
//    using System.IO;
//    using System.Reflection;
//    using System.Text;
//    using System.Text.RegularExpressions;
//    using System.Web;
//    using System.Web.Hosting;
//    using System.Web.Mvc;
//    using System.Web.Mvc.Html;

//    /// <summary>
//    /// Classe responsável por auxiliar projeto com metodos e propriedades
//    /// </summary>
//    public static class HtmlHelper
//    {
//        /// <summary>
//        /// string estatica Numero da Versao
//        /// </summary>
//        private static IHtmlString _HtmlTemplates = null;

//        /// <summary>
//        /// Carrega metaTags
//        /// </summary>
//        /// <typeparam name="t">parametro tipo</typeparam>
//        /// <param name="html">tipo "HtmlHelper" como parametro</param>
//        /// <returns>retorna "HtmlString"</returns>
//        public static IHtmlString BuildTemplates<t>(this HtmlHelper<t> html)
//        {
//            if (_HtmlTemplates == null)
//            {
//                StringBuilder ContentTemplates = new StringBuilder();

//                var pathTemplates = HttpContext.Current.Server.MapPath("\\templates");
//                string[] filePaths = Directory.GetFiles(pathTemplates, "*.html");

//                foreach (var filePath in filePaths)
//                {
//                    ContentTemplates.AppendLine("<!-- " + System.IO.Path.GetFileName(filePath) + " -->");
//                    ContentTemplates.AppendLine(File.ReadAllText(filePath));
//                    ContentTemplates.AppendLine();
//                }

//                var HtmlTemplates = new HtmlString(ContentTemplates.ToString());
//#if !DEBUG
//                _HtmlTemplates = HtmlTemplates;
//#endif
//                return HtmlTemplates;
//            }

//            return _HtmlTemplates;
//        }

//    }
//}

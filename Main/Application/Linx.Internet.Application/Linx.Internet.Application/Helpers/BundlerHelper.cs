//// -----------------------------------------------------------------------
// <copyright file="BaseHelpers.cs" company="Linx Sistemas">
// Copyright (c) Linx Sistemas. All rights reserved.
// </copyright>
//// -----------------------------------------------------------------------

namespace Linx.Internet.Application.Helpers
{
    using System.Collections.Generic;
    using System.Web;
    using System.Text;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    /// <summary>
    /// Classe responsável por auxiliar projeto com metodos e propriedades
    /// </summary>
    public static class BundlerHelper
    {
        /// <summary>
        /// Renders the styles tag with optional html attributes.
        /// </summary>
        /// <param name="path">
        /// The virtual path of the styles.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        /// <returns>
        /// The <see cref="IHtmlString"/>.
        /// </returns>
        public static IHtmlString RenderStyles(string path, object htmlAttributes)
        {
            var attributes = BuildHtmlStringFrom(htmlAttributes);

            string completedTag = string.Empty;

#if DEBUG

            var originalHtml = Styles.Render(path).ToHtmlString();
            completedTag = originalHtml.Replace("/>", attributes + "/>");
#else
            completedTag = string.Format(
                "<link rel=\"stylesheet\" href=\"{0}\" type=\"text/css\"{1} />",
                Styles.Url(path), attributes);
 
#endif

            return MvcHtmlString.Create(completedTag);
        }

        /// <summary>
        /// Renders the scripts tag with optional html attributes.
        /// </summary>
        /// <param name="path">The virtual path of the scripts.</param>
        /// <param name="htmlAttributes">The html attributes.</param>
        /// <returns>The <see cref="IHtmlString"/>.</returns>
        public static IHtmlString RenderScripts(string path, object htmlAttributes)
        {
            var attributes = BuildHtmlStringFrom(htmlAttributes);

            string completedTag = string.Empty;

            completedTag = string.Format(
                "<script src=\"{0}\" {1} />",
                Scripts.Url(path), attributes);

            return MvcHtmlString.Create(completedTag);
        }

        /// <summary>
        /// Use the html attributes and loop through in order
        /// to add to the completed tag.
        /// </summary>
        /// <param name="htmlAttributes">The html attributes.</param>
        /// <returns>An HTML string containing the html attributes</returns>
        private static string BuildHtmlStringFrom(object htmlAttributes)
        {
            // Try and safely cast
            var routeHtmlAttributes = htmlAttributes as IDictionary<string, object> ?? new RouteValueDictionary(htmlAttributes);

            var attributeBuilder = new StringBuilder();

            foreach (var attribute in routeHtmlAttributes)
            {
                attributeBuilder.AppendFormat(" {0}=\"{1}\"", attribute.Key, attribute.Value);
            }

            return attributeBuilder.ToString();
        }
    }
}

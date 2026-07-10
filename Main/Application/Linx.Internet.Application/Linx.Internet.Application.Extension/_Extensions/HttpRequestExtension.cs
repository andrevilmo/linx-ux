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

    /// <summary>
    /// Classe ConfigurationExtension
    /// </summary>
    public static class HttpRequestExtension
    {
        /// <summary>
        /// Metodo GetValue Tipo generics
        /// </summary>
        /// <typeparam name="T">Parametro generico </typeparam>
        /// <param name="reference">Parametro Tipo Object</param>
        /// <param name="key">Parametro Tipo String</param>
        /// <param name="defaultValue">Parametro tipo T </param>
        /// <returns>Retorna um tipo Generico</returns>
        public static bool QueryStringExistsValue(this System.Web.HttpRequest reference, string key, string value)
        {
            var queryStringValue = System.Web.HttpContext.Current.Request.QueryString[key];
            return (queryStringValue != null && queryStringValue.Equals(value, StringComparison.InvariantCultureIgnoreCase));
        }

        public static bool QueryStringExistsValue(this System.Web.HttpRequest reference, string key, params string[] value)
        {
            var queryStringValue = System.Web.HttpContext.Current.Request.QueryString[key];
            return (queryStringValue != null && value.Any(w => w.Equals(queryStringValue, StringComparison.InvariantCultureIgnoreCase)));
        }
    }
}

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
    public static class ConfigurationExtension
    {
        /// <summary>
        /// Metodo GetValue Tipo generics
        /// </summary>
        /// <typeparam name="T">Parametro generico </typeparam>
        /// <param name="reference">Parametro Tipo Object</param>
        /// <param name="key">Parametro Tipo String</param>
        /// <param name="defaultValue">Parametro tipo T </param>
        /// <returns>Retorna um tipo Generico</returns>
        public static T GetValue<T>(this object reference, string key, T defaultValue = default(T))
        {
            string retorno = System.Configuration.ConfigurationManager.AppSettings[key];

            if (retorno == null)
            {
                if (defaultValue != null)
                {
                    return (T)Convert.ChangeType(defaultValue, typeof(T), System.Globalization.CultureInfo.InvariantCulture);
                }
                else
                {
                    return default(T);
                }
            }

            return (T)Convert.ChangeType(retorno, typeof(T), System.Globalization.CultureInfo.InvariantCulture);
        }

    }
}

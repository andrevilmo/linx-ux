// -----------------------------------------------------------------------
// <copyright file="SqlXmlExtension.cs" company="Linx Sistemas">
// Copyright (c) Linx Sistemas. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Linx.Internet.Application
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Provides extension methods for the <see cref="String" /> type.
    /// </summary>
    public static class SqlXmlExtension
    {
        #region Methods
        /// <summary>
        /// Função generica para Renderizar de objeto para XML
        /// </summary>
        /// <typeparam name="U">Tipo do objeto</typeparam>
        /// <param name="referencia">Tipo do objeto referencia</param>
        /// <returns>Instancia do objeto</returns>
        public static U ToObject<U>(this System.Data.SqlTypes.SqlXml referencia)
        {
            if (referencia != null)
            {
                var serializer = new System.Runtime.Serialization.DataContractSerializer(typeof(U));
                return (U)serializer.ReadObject(referencia.CreateReader());
            }

            return default(U);
        }
        #endregion
    }
}
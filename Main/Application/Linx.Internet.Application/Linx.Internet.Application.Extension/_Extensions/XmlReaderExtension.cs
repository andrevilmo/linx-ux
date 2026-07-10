// -----------------------------------------------------------------------
// <copyright file="XmlReaderExtension.cs" company="Linx Sistemas">
// Copyright (c) Linx Sistemas. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Linx.Internet.Application
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Xml;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class XmlReaderExtension
    {
        /// <summary>
        /// Função generica para serializar o XML em objeto
        /// </summary>
        /// <typeparam name="U">Tipo do objeto</typeparam>
        /// <param name="referencia">Tipo do objeto referencia</param>
        /// <returns>Instancia do objeto</returns>
        public static U ToObject<U>(this XmlReader referencia)
        {
            if (referencia != null)
            {
                if (referencia.Read())
                {
                    var serializer = new DataContractSerializer(typeof(U));
                    return (U)serializer.ReadObject(referencia);
                }
            }

            return default(U);
        }
    }
}

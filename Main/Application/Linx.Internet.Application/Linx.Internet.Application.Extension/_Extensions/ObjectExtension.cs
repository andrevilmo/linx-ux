// -----------------------------------------------------------------------
// <copyright file="ObjectExtension.cs" company="Linx Sistemas">
// Copyright (c) Linx Sistemas. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Linx.Internet.Application
{
    using System;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// Classe ObjectExtension
    /// </summary>
    public static class ObjectExtension
    {
        /// <summary>
        /// Função generica para Renderizar de objeto para XML
        /// </summary>
        /// <typeparam name="U">Tipo do objeto</typeparam>
        /// <param name="referencia">Tipo do objeto referencia</param>
        /// <returns>Instancia do objeto</returns>
        public static string ObjectToXml<U>(this object referencia)
        {
            var serializer = new DataContractSerializer(typeof(U));
            using (var backing = new System.IO.StringWriter())
            using (var writer = new System.Xml.XmlTextWriter(backing))
            {
                serializer.WriteObject(writer, referencia);
                return backing.ToString();
            }
        }

        /// <summary>
        /// Função generica para Renderizar de objeto para XML
        /// </summary>
        /// <typeparam name="U">Tipo do objeto</typeparam>
        /// <param name="referencia">Tipo do objeto referencia</param>
        /// <returns>Instancia do objeto</returns>
        public static U StringToXml<U>(this string referencia)
        {
            if (referencia != null)
            {
                var serializer = new DataContractSerializer(typeof(U));
                using (var backing = new System.IO.StringReader(referencia))
                using (var reader = new System.Xml.XmlTextReader(backing))
                {
                    return (U)serializer.ReadObject(reader);
                }
            }

            return default(U);
        }

        /// <summary>
        /// Função generica para Renderizar de objeto para XML
        /// </summary>
        /// <typeparam name="T">Tipo do objeto</typeparam>
        /// <typeparam name="U">Tipo do da renderização </typeparam>
        /// <param name="referencia">Tipo do objeto referencia</param>
        /// <returns>Retorna U</returns>
        public static U RenderizarObjetoParaXml<T, U>(this object referencia)
        {
            if (referencia != null)
            {
                T objeto = (T)referencia;
                Type tipo = typeof(U);

                using (MemoryStream ms = new MemoryStream())
                {
                    XmlTextWriter xmlWriter = new XmlTextWriter(ms, new UTF8Encoding());
                    XmlSerializer xmlSerializer = new XmlSerializer(objeto.GetType());

                    xmlWriter.Formatting = Formatting.Indented;
                    xmlWriter.IndentChar = ' ';
                    xmlWriter.Indentation = 3;

                    xmlSerializer.Serialize(xmlWriter, objeto);

                    byte[] resultado = new byte[ms.Length];
                    ms.Position = 0;
                    ms.Read(resultado, 0, (int)ms.Length);

                    if (tipo == typeof(byte[]))
                    {
                        return (U)Convert.ChangeType(resultado, tipo);
                    }
                    else if (tipo == typeof(string))
                    {
                        return (U)Convert.ChangeType(Encoding.UTF8.GetString(resultado, 0, (int)ms.Length), tipo);
                    }
                }
            }

            return default(U);
        }

        /// <summary>
        /// Metodo Renderizar Xml Para Objeto
        /// </summary>
        /// <typeparam name="T">Parametro Tipo Generico</typeparam>
        /// <param name="referencia"> Parametro tipo String </param>
        /// <returns>Retorna Um objeto generico</returns>
        public static T RenderizarXmlParaObjeto<T>(this string referencia)
        {
            if (referencia != null)
            {
                Type tipo = typeof(T);
                string xml = referencia;

                XmlSerializer xmlSerializer = new XmlSerializer(tipo);
                byte[] xmlByte = Encoding.UTF8.GetBytes(xml);

                //// Carregar Xml na memoria para fazer a Deserializacao, passando XML em byte[]
                using (MemoryStream ms = new MemoryStream(xmlByte))
                {
                    // Criar XmlReader para ler o XML do memory stream
                    XmlTextReader xmlReader = new XmlTextReader(ms);

                    // Verificar se é possivel deserializar xml
                    if (xmlSerializer.CanDeserialize(xmlReader))
                    {
                        return (T)xmlSerializer.Deserialize(xmlReader);
                    }
                }
            }

            return default(T);
        }

        /// <summary>
        /// Gera um valor único(hash) para o objeto
        /// </summary>
        /// <param name="objeto">a instância que o hash representa</param>
        /// <returns>Valor hash único</returns>
        public static string GetUniqueHash(this object objeto)
        {
            if (objeto == null)
            {
                return string.Empty;
            }

            using (MemoryStream streamMemoria = new MemoryStream())
            {
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter serializacaoBinaria
                        = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                serializacaoBinaria.Serialize(streamMemoria, objeto);

                System.Security.Cryptography.MD5CryptoServiceProvider criptografia = new System.Security.Cryptography.MD5CryptoServiceProvider();
                return BitConverter.ToString(criptografia.ComputeHash(streamMemoria.ToArray())).Replace("-", string.Empty);
            }
        }

        /// <summary>
        /// Retorna o valor de uma chave da coleção ou se ela não existir o valor default do tipo
        /// </summary>
        /// <typeparam name="T">Tipo do retorno</typeparam>
        /// <param name="colecao">Coleção NameValueCollection</param>
        /// <param name="key">Chave de busca na coleção</param>
        /// <returns>O conteúdo da chave ou o valor default do tipo</returns>
        public static T GetValueOrDefault<T>(this NameValueCollection colecao, string key)
        {
            if (colecao[key] != null)
            {
                return (T)Convert.ChangeType(colecao[key], typeof(T));
            }

            return default(T);
        }

        /// <summary>
        /// Retorna o valor de uma chave do dicionario ou se ela não existir o valor default do tipo
        /// </summary>
        /// <typeparam name="T">Tipo do retorno</typeparam>
        /// <param name="colecao">Coleção NameValueCollection</param>
        /// <param name="key">Chave de busca na coleção</param>
        /// <returns>O conteúdo da chave ou o valor default do tipo</returns>
        public static T GetValueOrDefault<T>(this System.Collections.Generic.IDictionary<string, T> colecao, string key)
        {
            if (colecao[key] != null)
            {
                return (T)Convert.ChangeType(colecao[key], typeof(T));
            }

            return default(T);
        }

        /// <summary>
        /// Retorna somente o valor da data, já formatada para a cultura corrente
        /// </summary>
        /// <param name="data">Data a ser formatada</param>
        /// <returns>Data formatada ou null</returns>
        public static string GetDateForDateTime(this DateTime? data)
        {
            if (data.HasValue)
            {
                return data.Value.ToString(System.Globalization.DateTimeFormatInfo.CurrentInfo.ShortDatePattern);
            }

            return null;
        }

        /// <summary>
        /// Retorna a descrição de um enum
        /// </summary>
        /// <param name="opcao">enum para buscar a descrição</param>
        /// <returns>string com a descrição do enum</returns>
        public static string GetDescription(this Enum opcao)
        {
            Type type = opcao.GetType();

            MemberInfo[] memInfo = type.GetMember(opcao.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return opcao.ToString();
        }

        /// <summary>
        /// Formata a querystring para o refinamento do catalogo
        /// </summary>
        /// <param name="colecao">Coleção NameValueCollection</param>
        /// <returns>Retorna a querystring formatada para o refinamento do catalogo</returns>
        public static string GetQueryStringRefinamento(this NameValueCollection colecao)
        {
            string[] chavesIgnoradas = { "n", "p", "catalogo", "trace", "cache", "sort", "_", "tipocatalogo", "nomecatalogo", "query", "custom", "tipoerro" };

            if (colecao.Count == 0)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();
            string aux = string.Empty;

            foreach (string chave in colecao.Keys)
            {
                aux = chave.ToLower();

                if (!chavesIgnoradas.Contains(aux))
                {
                    sb.AppendFormat("{0}={1}", aux, colecao[chave]);
                    sb.Append("@");
                }
            }

            aux = sb.ToString().Trim();

            if (aux.Length > 0)
            {
                // remove o ultimo '@'
                aux = aux.Substring(0, sb.ToString().Length - 1);
            }

            return aux;
        }

        /// <summary>
        /// Formata a querystring para a ordenação do catalogo
        /// </summary>
        /// <param name="colecao">Coleção NameValueCollection</param>
        /// <returns>Retorna a querystring formatada para a ordenação refinamento do catalogo</returns>
        public static string GetQueryStringOrdenacao(this NameValueCollection colecao)
        {
            string retorno = string.Empty;

            if (colecao.Count > 0 && colecao["sort"] != null)
            {
                retorno = colecao["sort"];
            }

            return retorno;
        }

        /// <summary>
        /// Retorna a lista de emails
        /// </summary>
        /// <param name="reference">Referência do objeto</param>
        /// <param name="count">Quantidade de itens</param>
        /// <returns>string com a lista de email</returns>
        public static string GetListEmails(this string reference, int count = -1)
        {
            if (reference.Contains(";") || reference.Contains(","))
            {
                char[] delimiters = new char[] { ',', ';' };
                string[] parts = reference.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                if (count == -1)
                {
                    return string.Join(",", parts);
                }
                else
                {
                    return string.Join(",", parts, 0, count);
                }
            }
            else
            {
                return reference;
            }
        }
    }
}

// -----------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Linx Sistemas">
// Copyright (c) Linx Sistemas. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Linx.Internet.Application
{
    using System;
    using System.Globalization;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Provides extension methods for the <see cref="String" /> type.
    /// </summary>
    public static class StringExtensions
    {
        #region Methods
        /// <summary>
        /// Retorna a string sempre preenchida
        /// </summary>
        /// <param name="reference">objeto string</param>
        /// <param name="defaultValue">valor padrão quando a string for nula</param>
        /// <returns>string com valor</returns>
        public static string GetStringValue(this string reference, string defaultValue = "")
        {
            if (!string.IsNullOrEmpty(reference))
            {
                return reference;
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Formats the given string using the specified culture and arguments.
        /// </summary>
        /// <param name="string">The string to format.</param>
        /// <param name="arguments">The arguments used to format the string.</param>
        /// <exception cref="ArgumentNullException">If the input string, or any arguments are null.</exception>
        /// <exception cref="FormatException">If the input string is invalid, or the index of a format item is less than zero, or greater than or equal to the length of the args array.</exception>
        /// <returns>return String</returns>
        public static string StringFormat(this string @string, params object[] arguments)
        {
            return StringFormat(@string, CultureInfo.CurrentUICulture, arguments);
        }

        /// <summary>
        /// Formats the given string using the specified culture and arguments.
        /// </summary>
        /// <param name="string">The string to format.</param>
        /// <param name="culture">The culture used to format the string.</param>
        /// <param name="arguments">The arguments used to format the string.</param>
        /// <exception cref="ArgumentNullException">If the input string, or any arguments are null.</exception>
        /// <exception cref="FormatException">If the input string is invalid, or the index of a format item is less than zero, or greater than or equal to the length of the args array.</exception>
        /// <returns>return String</returns>
        public static string StringFormat(this string @string, CultureInfo culture, params object[] arguments)
        {
            return string.Format(culture, @string, arguments);
        }

        /// <summary>
        /// Metodo Para tratamento de String 
        /// </summary>
        /// <param name="string">String Principal</param>
        /// <param name="searchString">String de pesquisa</param>
        /// <returns>Retorna uma String</returns>
        public static string Substring(this string @string, string searchString)
        {
            return @string.Substring(0, @string.IndexOf(searchString) + searchString.Length);
        }

        /// <summary>
        /// Criptografa chave
        /// </summary>
        /// <param name="string">extensão da string</param>
        /// <returns>retorna string criptografada</returns>
        public static string Encrypt(this string @string)
        {
            SimplerAES simple = new SimplerAES();
            return simple.Encrypt(@string);
        }

        /// <summary>
        /// Descriptografa chave
        /// </summary>
        /// <param name="string">extensão da string</param>
        /// <returns>retorna string descriptografada</returns>
        public static string Decrypt(this string @string)
        {
            SimplerAES simple = new SimplerAES();
            return simple.Decrypt(@string);
        }

        #endregion
    }
}
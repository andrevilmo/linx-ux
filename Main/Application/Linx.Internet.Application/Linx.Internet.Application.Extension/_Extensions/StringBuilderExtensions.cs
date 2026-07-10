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
    public static class StringBuilderExtensions
    {
        #region Methods
        /// <summary>
        /// Retorna a string sempre preenchida
        /// </summary>
        /// <param name="reference">objeto string</param>
        /// <param name="defaultValue">valor padrão quando a string for nula</param>
        /// <returns>string com valor</returns>
        public static byte[] ToArrayBytes(this StringBuilder reference)
        {
            String input = reference.ToString();
            byte[] bytes = new byte[input.Length * sizeof(char)];
            System.Buffer.BlockCopy(input.ToCharArray(), 0, bytes, 0, bytes.Length);

            return bytes;
        }

        #endregion
    }
}
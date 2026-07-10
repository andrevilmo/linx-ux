// <copyright file="ExceptionExtension.cs" company="Linx Sistemas">
// Copyright (c) Linx Sistemas. All rights reserved.
// </copyright>
namespace Linx.Internet.Application
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class ExceptionExtension
    {
        /// <summary>
        /// Pega a mensagem de erro completa
        /// </summary>
        /// <param name="reference">Objeto Exception</param>
        /// <returns>Mensagem de erro</returns>
        public static string LastMessage(this Exception reference)
        {
            var i = reference.ToString().IndexOf(" at ");

            if (i > -1)
            {
                return reference.ToString().Substring(0, i);
            }
            else
            {
                return reference.Message;
            }
        }
    }
}

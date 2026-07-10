// -----------------------------------------------------------------------
// <copyright file="ExceptionExtension.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Linx.Internet.Application.Service
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
        public static string LastMessage(this Exception reference)
        {
            var i = reference.ToString().IndexOf(" at ");

            if (i > -1)
                return reference.ToString().Substring(0, i);
            else
                return reference.Message;
            /*
            if (reference.InnerException == null)
                return null;
            else
            {
                string message = reference.InnerException.LastMessage();
                return message;
            }
            */
        }
    }
}

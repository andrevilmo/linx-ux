using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Linx.Internet.Application.Service.Helpers
{
    public static class StringHelper
    {
        public static string Format(string str, params object[] args)
        {
            return string.Format(System.Globalization.CultureInfo.CurrentUICulture, str, args);
        }
    }
}

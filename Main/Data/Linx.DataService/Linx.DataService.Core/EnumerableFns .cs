using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Linx.DataService
{
    public static class EnumerableFns
    {
        /// <summary>
        /// Concatenates the string version of each element in a collection using the delimiter provided.
        /// </summary>
        /// <param name="items">The enumerated items whose string formated elements will be concatenated</param>
        /// <param name="delimiter">Delimiter</param>
        /// <returns>A delimited string</returns>
        public static string ToAggregateString(this IEnumerable items, string delimiter)
        {
            StringBuilder sb = null;
            foreach (object aObject in items)
            {
                if (sb == null)
                {
                    sb = new StringBuilder();
                }
                else
                {
                    sb.Append(delimiter);
                }
                sb.Append(aObject.ToString());
            }
            if (sb == null) return String.Empty;
            return sb.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Linx.Dsl.Components
{
    public static class StringBuilderExtension
    {
        public static void AppendProperty<T>(this StringBuilder r, string key, string value, bool prefixSemi = false)
        {
            if (!string.IsNullOrEmpty(value))
            {
                
                r.Append("\t");
                if (prefixSemi)
                    r.Append(",");

                if (typeof(T) == typeof(string))
                    r.AppendFormat("{0}: \"{1}\"", key, value);
                else
                    r.AppendFormat("{0}: {1}", key, value);
                r.AppendLine();
            }
        }

        public static void AppendCollection(this StringBuilder r, string key, StringBuilder values)
        {
            if (values.Length > 0)
            {
                //r.AppendFormat("\t {0}: \"{1}\"\r\n", key, value);
                r.AppendFormat("\t ,{0}: [{1}] \r\n", key, values);
            }
        }
    }
}

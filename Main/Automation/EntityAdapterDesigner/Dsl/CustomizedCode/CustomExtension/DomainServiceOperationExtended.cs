using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DslModeling = global::Microsoft.VisualStudio.Modeling;
using DslDesign = global::Microsoft.VisualStudio.Modeling.Design;
using Linx.Tools;

namespace Linx.EntityAdapterDesigner
{

    public partial class DomainServiceOperation
    {
        public string GetJsonGetParams()
        {
            string result = String.Empty, paramName, paramType;

            if (!this.Parameters.IsNullOrEmpty() && this.IsJson)
            {
                foreach (string param in this.Parameters.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    paramName = param.Right(" ");
                    paramType = param.Left(" ");
                    result = (result.IsNullOrEmpty() ? String.Empty : "&") + paramName + "=" + (paramType.ToLower().Contains("string") ? "Abc" : "0");
                }
            }


            return result;
        }

        public string GetJsonPostParams(string indent)
        {
            string result = String.Empty, paramName, paramType, separate = String.Empty;

            if (!this.Parameters.IsNullOrEmpty() && !this.IsJson)
            {

                //Request Payload:
                result += "\r\n" + indent + "<div>";
                result += "\r\n" + indent + "" + indent + "<b>Request Payload:</b>";
                result += "\r\n" + indent + "</div>";

                result += "\r\n" + indent + "<div>";
                result += "\r\n" + indent + "" + indent + "{";
                result += "\r\n" + indent + "</div>";

                foreach (string param in this.Parameters.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    paramName = param.Right(" ");
                    paramType = param.Left(" ").Replace("<", "[").Replace(">", "]");

                    if (!separate.IsNullOrEmpty())
                    {
                        result += "\r\n" + indent + "<div>";
                        result += "\r\n" + indent + "" + separate;
                        result += "\r\n" + indent + "</div>";
                    }

                    result += "\r\n" + indent + "<div>";
                    result += "\r\n" + indent + "\"" + paramName + "\": Instance of " + paramType;
                    result += "\r\n" + indent + "</div>";

                    if (separate.IsNullOrEmpty())
                        separate = ",";
                }

                result += "\r\n" + indent + "<div>";
                result += "\r\n" + indent + "" + indent + "}";
                result += "\r\n" + indent + "</div>";
            }


            return result;
        }
    }
}
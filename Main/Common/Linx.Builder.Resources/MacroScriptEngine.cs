using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Linx.Tools;
using System.IO;
using System.Windows.Forms;

namespace Linx.Builder.Resources
{

    public class MacroScriptEngine
    {
        private string GetLocalPath()
        {
            return "C:\\VSTS - GrupoLinx\\Framework";
        }

        public string GetSamplesPath(string envPart)
        {
            if (envPart.Contains("\\"))
            {
                return Path.Combine(envPart, "MacroScriptCatalogSamples.xml");
            }
            else
            {
                string fullPath = Path.Combine(GetLocalPath(), "Linx Framework\\" + envPart + "\\Binary\\Library\\Common\\Linx\\Information\\MacroScriptCatalogSamples.xml");
                return fullPath;
            }
        }

        public string GetMacrosXML(string envPart)
        {
            if (envPart.Contains("\\"))
            {
                return Path.Combine(envPart, "MacroScriptCatalog.xml");
            }
            else
            {
                string fullPath = Path.Combine(GetLocalPath(), "Linx Framework\\" + envPart + "\\Binary\\Library\\Common\\Linx\\Information\\MacroScriptCatalog.xml");
                return fullPath;
            }
        }

        private Dictionary<string, MacroNode> _macros = null;

        public Dictionary<string, MacroNode> GetMacros(string envPart)
        {
            Dictionary<string, MacroNode> result = new Dictionary<string, MacroNode>();

            string dirInfoFile = GetMacrosXML(envPart);
            if (File.Exists(dirInfoFile))
            {
                try
                {
                    System.Xml.Linq.XElement xElement = System.Xml.Linq.XElement.Load(dirInfoFile);
                    if (!xElement.IsNull())
                    {
                        result = xElement.Elements().ToDictionary(e => e.Attributes("Name").First().Value, e => new MacroNode() { Name = e.Attributes("Name").First().Value, Description = e.Attributes("Description").First().Value, Document = e.Attributes("Document").First().Value, Domain = (MacroDomain)Enum.Parse(typeof(MacroDomain), e.Attributes("Domain").First().Value), ParameterCount = int.Parse(e.Attributes("ParameterCount").First().Value), Delimiter = e.Attributes("Delimiter").First().Value, Outputs = (!e.HasElements ? new List<MacroOutput>() : e.Elements().Select(o => new MacroOutput() { Type = (MacroOutputType)Enum.Parse(typeof(MacroOutputType), o.Attributes("Name").First().Value), Command = o.Value }).ToList()) });
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show(String.Format("Fail reading the file {0}. ", dirInfoFile) + exp.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }


            return result;
        }

        public bool HasMacro(string expression, string envPart)
        {
            if (_macros == null) _macros = this.GetMacros(envPart);

            return _macros.Any(m => expression.Contains("@" + m.Key + "("));
        }



        private FunctionParts GetFunctionParts(string expression, MacroNode mNode)
        {
            FunctionParts result = new FunctionParts() { LeftLength = 0, RightLength = 0, SubExpressions = new List<string>(), HasError = false, HasOccurrences = false };
            string cmd = "@" + mNode.Name + "(";
            int startPosition = expression.IndexOf(cmd);
            if (startPosition < 0)
                return result;
            int currentPosition = startPosition + cmd.Length;

            //Get function limits
            Stack<char> expControl = new Stack<char>();
            expControl.Push('(');
            while (currentPosition < expression.Length)
            {
                if (expression[currentPosition] == '(')
                    expControl.Push('(');
                else if (expression[currentPosition] == ')')
                    expControl.Pop();

                if (expControl.Count == 0)
                    break;

                currentPosition++;
            }

            //Mounting parts for returning
            if (expControl.Count > 0)
                result.HasError = true;
            else if (currentPosition > startPosition)
            {
                result.HasOccurrences = true;
                result.SubExpressions = GetParts(expression.Substring(startPosition + cmd.Length, (currentPosition - startPosition - cmd.Length)), mNode.Delimiter);
                result.LeftLength = startPosition;
                result.RightLength = expression.Length - currentPosition - 1;
            }

            return result;
        }

        private List<string> GetParts(string expression, string delimiter)
        {
            List<string> result = new List<string>();

            if (String.IsNullOrWhiteSpace(expression))
                return result;

            int startPosition = 0, currentPosition = 0;
            Stack<int> expControl = new Stack<int>();
            while (currentPosition < expression.Length - 1)
            {
                if (expression[currentPosition] == '(')
                {
                    expControl.Push('(');
                }
                else if (expression[currentPosition] == ')' && expControl.Count > 0)
                {
                    expControl.Pop();
                }
                else if (expControl.Count == 0 && ((currentPosition + delimiter.Length) <= expression.Length && expression.Substring(currentPosition, delimiter.Length) == delimiter))
                {
                    result.Add(expression.Substring(startPosition, currentPosition - startPosition));
                    startPosition = currentPosition + delimiter.Length;
                }
                currentPosition++;
            }

            result.Add(expression.Substring(startPosition));

            return result;
        }

        private List<MacroNode> GetInnerMacros(string expression, string envPart)
        {
            List<MacroNode> list = new List<MacroNode>();

            if (_macros == null) _macros = this.GetMacros(envPart);

            foreach (var macro in _macros)
            {
                for (int idx = 0; idx < expression.Occurs("@" + macro.Key + "("); idx++)
                {
                    list.Add(macro.Value);
                }
            }

            return list;
        }

        private string ReplaceEntitySqlMarks(string expression)
        {
            string result = expression.Replace("\"", "'");
            result = result.Replace("==", "=");
            result = result.Replace("!=", "<>");
            result = result.Replace("!", " Not ");
            result = result.Replace("= null", " is null");
            result = result.Replace("=null", " is null");
            return result;
        }

        private string ReplaceMacros(string expression, MacroOutputType output, string envPart)
        {
            string result = expression;

            var innerMacros = GetInnerMacros(result, envPart);
            foreach (var macro in innerMacros)
            {
                var part = GetFunctionParts(result, macro);
                if (!part.HasError && part.HasOccurrences)
                {
                    var outputElement = macro.Outputs.FirstOrDefault(e => e.Type == output);
                    if (outputElement != null && !outputElement.Command.IsNullOrEmpty() && part.SubExpressions.Count == macro.ParameterCount)
                    {
                        string command = outputElement.Command;
                        for (int pIndex = 0; pIndex < macro.ParameterCount; pIndex++)
                        {
                            command = command.Replace("{" + pIndex.ToString() + "}", ReplaceMacros(part.SubExpressions[pIndex], output, envPart));
                        }
                        result = result.Left(part.LeftLength) + command + result.Right(part.RightLength);
                    }
                }
            }

            if (output == MacroOutputType.EntitySQL)
                result = ReplaceEntitySqlMarks(result);

            return result;
        }

        public string ReplaceAllMacros(string expression, MacroOutputType output, string envPart)
        {
            return this.ReplaceMacros(expression, output, envPart).Replace("\r\n", "\n").Replace("\n", "\r\n");
        }
    }

    public class MacroNode
    {
        public string Name { get; set; }
        public MacroDomain Domain { get; set; }
        public int ParameterCount { get; set; }
        public string Description { get; set; }
        public string Document { get; set; }
        public string Delimiter { get; set; }
        public List<MacroOutput> Outputs { get; set; }
    }

    public class FunctionParts
    {
        public int LeftLength { get; set; }
        public int RightLength { get; set; }
        public List<string> SubExpressions { get; set; }
        public bool HasError { get; set; }
        public bool HasOccurrences { get; set; }
    }

    public class MacroOutput
    {
        public MacroOutputType Type { get; set; }
        public string Command { get; set; }
    }

    public enum MacroDomain
    {
        All, ClientAll, ClientContext, ClientEntity, ServerAll, ServerContext, ServerEntity
    }

    public enum MacroOutputType
    {
        CSharp, JavaScript, EntitySQL, JavaScriptMobile
    }
}

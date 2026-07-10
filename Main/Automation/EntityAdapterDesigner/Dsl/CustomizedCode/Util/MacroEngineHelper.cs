using Linx.Builder.Resources;
using Linx.Tools;
using System;
using System.IO;

namespace Linx.EntityAdapterDesigner.CustomizedCode.Util
{
    public static class MacroEngineHelper
    {
        private static string pathXML;
        private static DateTime LastModification;

        #region macroScriptEngine
        static MacroScriptEngine _macroScriptEngine;
        private static MacroScriptEngine macroScriptEngine(string envPath)
        {
            if (_macroScriptEngine == null)
            {
                _macroScriptEngine = new MacroScriptEngine();
                pathXML = _macroScriptEngine.GetMacrosXML(envPath);
            }
            //reload

            var _last = GetLastModification();
            if (LastModification == DateTime.MinValue)
                LastModification = _last;
            else if (_last > LastModification)
            {
                _macroScriptEngine = new MacroScriptEngine();
                LastModification = _last;
            }
            return _macroScriptEngine;

        }
        #endregion

        #region Private Methods
        private static DateTime GetLastModification()
        {
            if (File.Exists(pathXML))
                return File.GetLastWriteTime(pathXML);
            else
                return DateTime.MinValue;
        }

        #endregion


        #region Public Methods
        public static string ReplaceMacrosEntitySql(string macroScript, IAditionalInformation modelElement)
        {
            return ReplaceMacros(macroScript, MacroOutputType.EntitySQL, modelElement);
        }


        private static string ReplaceEntitySqlMarks(string expression)
        {
            string result = expression.Replace("\"", "'");
            result = result.Replace("==", "=");
            result = result.Replace("!=", "<>");
            result = result.Replace("!", " Not ");
            result = result.Replace("= null", " is null");
            result = result.Replace("=null", " is null");
            return result;
        }

        public static string ReplaceMacros(string macroScript, MacroOutputType typeScript, IAditionalInformation modelElement)
        {
            if (macroScript.IsNullOrEmpty())
                return macroScript;

            if (typeScript == MacroOutputType.EntitySQL)
                macroScript = ReplaceEntitySqlMarks(macroScript);

            var macroScriptEng = macroScriptEngine(modelElement.GetEnvPart());
            if (modelElement == null) throw new ArgumentNullException("modelElement is null");
            //old macros
            if (typeScript.In(MacroOutputType.EntitySQL, MacroOutputType.CSharp) && Linx.Tools.MacroEngine.HasMacro(macroScript))
                macroScript = Linx.Tools.MacroEngine.ReplaceMacros(macroScript, typeScript == MacroOutputType.EntitySQL);

            //new macros
            if (macroScriptEng.HasMacro(macroScript, modelElement.GetEnvPart()))
                macroScript = macroScriptEng.ReplaceAllMacros(macroScript, typeScript, modelElement.GetEnvPart());

            return macroScript;
        }

        public static bool HasMacro(string macroScript, IAditionalInformation modelElement)
        {
            return Linx.Tools.MacroEngine.HasMacro(macroScript) || macroScriptEngine(modelElement.GetEnvPart()).HasMacro(macroScript, modelElement.GetEnvPart());
        }
        #endregion
    }
}

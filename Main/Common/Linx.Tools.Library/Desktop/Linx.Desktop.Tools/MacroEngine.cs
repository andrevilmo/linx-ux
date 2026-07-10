using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Linx.Tools
{
    public class MacroEngine
    {
        private static string[] dateTimeFunctions = new string[] { "Year", "Month", "Day", "DayOfYear", "Hour", "Minute", "Second" };
        
        public static string ReplaceMacros(string expression, bool byEntitySQL)
        {
            if (!HasMacro(expression))
                return expression;

            string result = expression.Replace("||", "-Or-");
            result = ReplaceIif(result, byEntitySQL);
            result = ReplaceDivide(result, byEntitySQL);
            foreach (string func in dateTimeFunctions)
            {
                result = ReplaceDateTimeFunction(result, func, byEntitySQL);
            }
            
            return result.Replace("-Or-", "||");
        }

        public static bool HasMacro(string expression)
        {
          if(expression.Contains("@Iif["))
              return true;

          if (expression.Contains("@Divide["))
              return true;

          foreach (string func in dateTimeFunctions)
          {
              if (expression.Contains("@" + func + "["))
                  return true;
          }

          return false;
        }
        
        private static string ReplaceDateTimeFunction(string expression, string function, bool byEntitySQL)
        {
            string cmd = "@" + function + "[";
            int startPosition = expression.IndexOf(cmd);
            if (startPosition < 0)
                return expression;
            int currentPosition = startPosition + cmd.Length;
            Stack<char> expControl = new Stack<char>();
            expControl.Push('[');
            while (currentPosition < expression.Length)
            {
                if (expression[currentPosition] == '[')
                    expControl.Push('[');
                else if (expression[currentPosition] == ']')
                    expControl.Pop();

                if (expControl.Count == 0)
                    break;

                currentPosition++;
            }

            if (expControl.Count > 0)
                return expression.Replace(cmd, cmd.Left(cmd.Length - 1) + "Error[");

            if (currentPosition > startPosition)
            {
                string subExp = ReplaceDateTimeFunction(expression.Substring(startPosition + cmd.Length, (currentPosition - startPosition - cmd.Length)), function, byEntitySQL);
               
                if (byEntitySQL)
                    subExp = function + "(" + subExp + ")";
                else
                    subExp = "((System.DateTime)" + subExp + ")." + function;

                return expression.Left(startPosition) + subExp + expression.Right(expression.Length - currentPosition - 1);
            }

            return expression.Replace(cmd, "@iifError[");
        }

        private static string ReplaceIif(string expression, bool byEntitySQL)
        {
            string cmd = "@Iif[";
            int startPosition = expression.IndexOf(cmd);
            if (startPosition < 0)
                return expression;
            int currentPosition = startPosition + cmd.Length;
            Stack<char> expControl = new Stack<char>();
            expControl.Push('[');
            while (currentPosition < expression.Length)
            {
                if (expression[currentPosition] == '[')
                    expControl.Push('[');
                else if (expression[currentPosition] == ']')
                    expControl.Pop();

                if (expControl.Count == 0)
                    break;

                currentPosition++;
            }

            if (expControl.Count > 0)
                return expression.Replace(cmd, cmd.Left(cmd.Length - 1) + "Error[");

            if (currentPosition > startPosition)
            {
                string subExp = ReplaceIif(expression.Substring(startPosition + cmd.Length, (currentPosition - startPosition - cmd.Length)), byEntitySQL);
                string[] parts = subExp.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 3)
                    return expression.Replace(cmd, cmd.Left(cmd.Length - 1) + "Error[");

                if (byEntitySQL)
                    subExp = "CASE WHEN " + parts[0] + " THEN " + parts[1] + " ELSE " + parts[2] + " END";                   
                else
                    subExp = "(" + parts[0] + " ? " + parts[1] + " : " + parts[2] + ")";

                return expression.Left(startPosition) + subExp + expression.Right(expression.Length - currentPosition - 1);
            }

            return expression.Replace(cmd, cmd.Left(cmd.Length - 1) + "Error[");
        }


        private static string ReplaceDivide(string expression, bool byEntitySQL)
        {
            string cmd = "@Divide[";
            int startPosition = expression.IndexOf(cmd);
            if (startPosition < 0)
                return expression;
            int currentPosition = startPosition + cmd.Length;
            Stack<char> expControl = new Stack<char>();
            expControl.Push('[');
            while (currentPosition < expression.Length)
            {
                if (expression[currentPosition] == '[')
                    expControl.Push('[');
                else if (expression[currentPosition] == ']')
                    expControl.Pop();

                if (expControl.Count == 0)
                    break;

                currentPosition++;
            }

            if (expControl.Count > 0)
                return expression.Replace(cmd, cmd.Left(cmd.Length - 1) + "Error[");

            if (currentPosition > startPosition)
            {
                string subExp = ReplaceIif(expression.Substring(startPosition + cmd.Length, (currentPosition - startPosition - cmd.Length)), byEntitySQL);
                string[] parts = subExp.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 2)
                    return expression.Replace(cmd, cmd.Left(cmd.Length - 1) + "Error[");

                if (byEntitySQL)
                    subExp = "CASE WHEN (" + parts[1] + ") IS NULL OR (" + parts[1] + ") = 0 THEN 0 ELSE (" + parts[0] + ") / (" + parts[1] + ") END";                    
                else
                    subExp = "((" + parts[1] + ") == null || (" + parts[1] + ") == 0 ? 0 : (" + parts[0] + ") / (" + parts[1] + "))";

                return expression.Left(startPosition) + subExp + expression.Right(expression.Length - currentPosition - 1);
            }

            return expression.Replace(cmd, cmd.Left(cmd.Length - 1) + "Error[");
        }

    }
}

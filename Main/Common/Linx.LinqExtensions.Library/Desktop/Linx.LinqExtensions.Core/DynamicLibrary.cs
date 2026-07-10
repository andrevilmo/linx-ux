//Copyright (C) Microsoft Corporation.  All rights reserved.
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using System.Collections;
using Linx.Tools;
using System.Text.RegularExpressions;

namespace Linx.LinqExtensions.Dynamic
{
    /// <summary>
    /// Microsoft provided class. It allows dynamic string based querying. 
    /// Very handy when, at compile time, you don't know the type of queries that will be generated.
    /// </summary>
    public static class DynamicQueryable
    {
        #region String Command Converters
        public static string ToDynamicSqlExpression(this string predicate, Type entityType, System.Data.Entity.Core.Objects.ObjectParameter[] parameters, List<object> paramValues)
        {
            if (String.IsNullOrWhiteSpace(predicate) || predicate.ToLower().Trim() == "true")
                predicate = "1=1";
            else
            {
                predicate = predicate + " ";
                for (int p = 0; p < parameters.Length; p++)
                {
                    paramValues.Add(parameters[p].Value);
                    predicate = predicate.Replace("@" + parameters[p].Name + " ", "@p" + (paramValues.Count - 1).ToString() + " ").Replace("@" + parameters[p].Name + ")", "@p" + (paramValues.Count - 1).ToString() + ")");
                }

                //Adjust In Command
                var inCmdParts = predicate.Split(new string[] { " In {" }, StringSplitOptions.RemoveEmptyEntries);
                while (predicate.Contains(" In {"))
                {
                    string leftPart = predicate.Left(" In {");
                    string property = leftPart.Right("it.");
                    string rightPart = predicate.Right(predicate.Length - leftPart.Length - 5);
                    string contentList = rightPart.Left("}");
                    string replaceSource = "it." + property + " In {" + contentList + "}";
                    string replaceTarget = property + " In (" + contentList + ")";
                    predicate = predicate.Replace(replaceSource, "(" + replaceTarget + ")");
                }

                predicate = predicate.Replace("it.", "");
                predicate = predicate.TrimEnd();
            }

            return predicate;
        }

        public static string ToDynamicLinqExpression(this string predicate, Type entityType, System.Data.Entity.Core.Objects.ObjectParameter[] parameters, List<object> paramValues)
        {
            if (String.IsNullOrWhiteSpace(predicate))
                predicate = "true";
            else
            {
                predicate = predicate + " ";
                for (int p = 0; p < parameters.Length; p++)
                {
                    if (parameters[p].Value is string && parameters[p].Value.ToString().Contains("%"))
                    {
                        string value = parameters[p].Value.ToString();
                        if (value.Occurs("%") == 2 && value.StartsWith("%") && value.EndsWith("%")) //Contaisn
                        {
                            paramValues.Add(value.Replace("%", ""));
                            predicate = predicate.Replace(" Like @" + parameters[p].Name + " ", ".Contains(@" + (paramValues.Count - 1).ToString() + ") ").Replace(" Like @" + parameters[p].Name + ")", ".Contains(@" + (paramValues.Count - 1).ToString() + "))");
                        }
                        else if (value.Occurs("%") == 1 && value.EndsWith("%"))
                        {
                            paramValues.Add(value.Replace("%", ""));
                            predicate = predicate.Replace(" Like @" + parameters[p].Name + " ", ".StartsWith(@" + (paramValues.Count - 1).ToString() + ") ").Replace(" Like @" + parameters[p].Name + ")", ".StartsWith(@" + (paramValues.Count - 1).ToString() + "))");
                        }
                        else if (value.Occurs("%") == 1 && value.StartsWith("%"))
                        {
                            paramValues.Add(value.Replace("%", ""));
                            predicate = predicate.Replace(" Like @" + parameters[p].Name + " ", ".EndsWith(@" + (paramValues.Count - 1).ToString() + ") ").Replace(" Like @" + parameters[p].Name + ")", ".EndsWith(@" + (paramValues.Count - 1).ToString() + "))");
                        }
                        else
                        {
                            string propertyName = predicate.Left(" Like @" + parameters[p].Name).Right("it.");
                            var allParts = value.Split(new char[] { '%' }, StringSplitOptions.RemoveEmptyEntries);
                            string likeCommand = "";
                            for (int idx = 0; idx < allParts.Length; idx++)
                            {
                                var valuePart = allParts[idx];
                                likeCommand += (likeCommand == "" ? "" : " && ") + (idx == 0 ? "it." + propertyName + ".StartsWith(\"" + valuePart + "\")" : (idx == allParts.Length - 1 ? "it." + propertyName + ".EndsWith(\"" + valuePart + "\")" : "it." + propertyName + ".Contains(\"" + valuePart + "\")"));
                            }
                            predicate = predicate.Replace("it." + propertyName + " Like @" + parameters[p].Name, likeCommand);
                        }
                    }
                    else
                    {
                        paramValues.Add(parameters[p].Value);
                        predicate = predicate.Replace("@" + parameters[p].Name + " ", "@" + (paramValues.Count - 1).ToString() + " ").Replace("@" + parameters[p].Name + ")", "@" + (paramValues.Count - 1).ToString() + ")");
                    }
                }

                //Adjust In Command
                var inCmdParts = predicate.Split(new string[] { " In {" }, StringSplitOptions.RemoveEmptyEntries);
                while (predicate.Contains(" In {"))
                {
                    string leftPart = predicate.Left(" In {");
                    string property = leftPart.Right("it.");
                    string rightPart = predicate.Right(predicate.Length - leftPart.Length - 5);
                    string contentList = rightPart.Left("}");
                    string replaceSource = "it." + property + " In {" + contentList + "}";
                    string replaceTarget = "";
                    Type propType = entityType.GetProperty(property).PropertyType;
                    foreach (var inPart in contentList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        paramValues.Add(ParseValue(inPart.Replace("'", ""), propType));
                        replaceTarget += (replaceTarget == "" ? "" : " Or ") + property + " = @" + (paramValues.Count - 1).ToString();

                    }
                    if (replaceTarget != "")
                    {
                        predicate = predicate.Replace(replaceSource, "(" + replaceTarget + ")");
                    }
                }

                predicate = predicate.Replace("'", "\"");
                predicate = predicate.TrimEnd();
            }

            return predicate;
        }

        static object ParseValue(string value, Type propType)
        {
            if (propType.FullName.Contains("System.Guid"))
            {
                return System.Guid.Parse(value);
            }

            if (propType.FullName.Contains("System.String"))
            {
                return value;
            }

            if (propType.FullName.Contains("System.DateTime"))
            {
                return DateTime.Parse(value);
            }

            return Convert.ChangeType(value, (propType.Name == "Nullable`1" ? propType.GetElement() : propType));
        }

        #endregion

    }

}
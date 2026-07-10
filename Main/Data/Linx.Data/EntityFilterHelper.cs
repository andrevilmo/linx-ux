using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linx.Tools;
using System.Data.Entity.Core.Objects;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema;

namespace Linx.Data
{
    public static class EntityFilterHelper
    {
        #region Public Members

        private static ISecurityHelper _securityHelper = null;
        public static ISecurityHelper SecurityHelper
        {
            get
            {
                if (_securityHelper == null)
                    _securityHelper = ImplementationHelper<ISecurityHelper>.GetInstance("SecurityHelper", "Linx.Business.Tools");

                return _securityHelper;
            }
        }

        public static List<BmMetaDataProperty> GetBmEntityProperties(this System.Data.Entity.DbContext context, string entityName, string parentDataPath)
        {
            List<BmMetaDataProperty> properties = new List<BmMetaDataProperty>();

            if (parentDataPath.IsNullOrEmpty())
            {
                properties.Add(new BmMetaDataProperty() { id = entityName, parent = "#", text = entityName.Replace("_", " ").Proper(), dataType = 'S', entityName = entityName, enabled = false, children = true });
            }
            else
            {
                if (parentDataPath.Right(".") == "SID")
                {
                    if (SecurityHelper != null)
                    {
                        //Get custom available searchs 
                        foreach (var search in SecurityHelper.GetCustomSearchList(entityName))
                        {
                            properties.Add(new BmMetaDataProperty() { id = "*SID(" + parentDataPath.Left(parentDataPath.Length - 4) + "," + search.Key.ToString() + ")", parent = parentDataPath, text = search.Value.Translate(), dataType = 'S', entityName = entityName, enabled = true, children = false });
                        }
                    }
                }
                else
                {
                    var contextType = context.GetType();
                    var entity = contextType.Assembly.GetType(contextType.Namespace + "." + entityName);
                    var tableAttr = entity.GetCustomAttribute<System.ComponentModel.DataAnnotations.Schema.TableAttribute>();
                    string displayName = "";
                    foreach (var propInfo in entity.GetProperties())
                    {
                        if (!propInfo.GetMethod.IsVirtual && propInfo.GetCustomAttribute<ForeignKeyAttribute>() != null)
                            continue;

                        var display = propInfo.GetCustomAttribute<DisplayAttribute>();
                        if (display != null && !display.Name.IsNullOrEmpty())
                        {
                            displayName = display.Name;
                        }
                        else
                        {
                            if (propInfo.GetMethod.IsVirtual)
                            {
                                string propName = propInfo.Name;
                                string schema = tableAttr.Schema + "_";
                                if (propName.StartsWith(schema))
                                    propName = propName.Right(propName.Length - schema.Length);
                                displayName = propName.Replace("_", " ").Proper() + (propInfo.PropertyType.FullName.Contains("ICollection") ? " [1-*]" : " [1-1]");
                            }
                            else
                                displayName = propInfo.Name.Replace("_", " ").Proper();
                        }

                        var eName = (propInfo.GetMethod.IsVirtual ? (propInfo.PropertyType.IsGenericType ? propInfo.PropertyType.GetElement().Name : propInfo.PropertyType.Name) : "");
                        properties.Add(new BmMetaDataProperty() { id = parentDataPath + "." + propInfo.Name, parent = parentDataPath, text = displayName, dataType = Linx.Tools.EntitySearch.ParseJDataType((propInfo.PropertyType.IsGenericType ? propInfo.PropertyType.GetElement().Name : propInfo.PropertyType.Name)), entityName = eName, enabled = !propInfo.GetMethod.IsVirtual, children = propInfo.GetMethod.IsVirtual });
                    }
                    //Advanced Search Root
                    if (parentDataPath.Occurs(".") > 0)
                        properties.Add(new BmMetaDataProperty() { id = parentDataPath + "." + "SID", parent = parentDataPath, text = " --Pesquisas Avançadas-- ".Translate(), dataType = 'S', entityName = entityName, enabled = false, children = true });
                }
            }

            return properties.OrderBy(e => (e.enabled ? "1" : "0") + e.text).ToList();
        }

        public static string JExpressionToEntitySql(this System.Data.Entity.DbContext context, string jExpression, List<ObjectParameter> parameters)
        {
            var dbFilters = jExpression.JExpressionToDbFilters();
            string filterDefinition = "";
            int aliasCount = 0;
            foreach (var dbFilter in dbFilters)
            {
                if (dbFilter is DbFilterCondition)
                {
                    filterDefinition += (filterDefinition.IsNullOrEmpty() ? "" : " ") + ((DbFilterCondition)dbFilter).Content;
                }
                else
                {
                    filterDefinition += (filterDefinition.IsNullOrEmpty() ? "" : " ") + GetFilterPart(context, ((DbFilterExpression)dbFilter), parameters, ref aliasCount);
                }
            }

            return filterDefinition;
        }

        #endregion



        #region Private members

        private static string GetFilterPart(this System.Data.Entity.DbContext context, DbFilterExpression dbFilter, List<ObjectParameter> parameters, ref int aliasCount)
        {
            string entityDataPath = dbFilter.EntityDataPath, optr = dbFilter.Operator;
            object value = null;
            switch (optr)
            {
                case "In":
                case "!In":

                    string inList = (dbFilter.Value.IsNullOrEmpty() ? "" : dbFilter.Value.ToString());
                    if (!inList.IsNullOrEmpty() && dbFilter.DataType.In(new char[] { 'G', 'S', 'T', 'C' }))
                    {
                        switch (dbFilter.DataType)
                        {
                            case 'G':
                                inList = "GUID'" + String.Join("',GUID'", inList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(e => e.Trim())) + "'";
                                break;
                            case 'T':
                                inList = "DATETIME'" + String.Join("',DATETIME'", inList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(e => e.Trim())) + "'";
                                break;
                            default:
                                inList = "'" + String.Join("','", inList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(e => e.Trim())) + "'";
                                break;
                        }
                    }

                    value = "{" + inList + "}";

                    break;
                case "Contains":
                    value = "%" + (dbFilter.Value.IsNullOrEmpty() ? "" : dbFilter.Value.ToString()) + "%";
                    optr = "Like";
                    break;
                case "StartsWith":
                    value = (dbFilter.Value.IsNullOrEmpty() ? "" : dbFilter.Value.ToString()) + "%";
                    optr = "Like";
                    break;
                case "EndsWith":
                    value = "%" + (dbFilter.Value.IsNullOrEmpty() ? "" : dbFilter.Value.ToString());
                    optr = "Like";
                    break;
                default:
                    value = dbFilter.Value;
                    break;
            }

            string filterPart = "";
            if (context != null && !entityDataPath.IsNullOrEmpty() && entityDataPath.Occurs(".") > 0)
            {
                string propertyName = entityDataPath.Right(".");
                entityDataPath = entityDataPath.Left(entityDataPath.Length - propertyName.Length - 1);
                var contextType = context.GetType();
                var parts = entityDataPath.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                var topEntityName = parts[0];
                var currentLevelEntity = contextType.Assembly.GetType(contextType.Namespace + "." + topEntityName);
                Type previousLevelType = null;
                string directPath = "it";
                if (parts.Length > 1)
                {
                    for (int idx = 1; idx < parts.Length; idx++)
                    {
                        var propertyNavigationName = parts[idx];
                        var propertyNavigation = currentLevelEntity.GetProperty(propertyNavigationName);
                        previousLevelType = currentLevelEntity;
                        currentLevelEntity = (propertyNavigation.PropertyType.IsGenericType ? propertyNavigation.PropertyType.GetElement() : propertyNavigation.PropertyType);

                        if (propertyNavigation.PropertyType.FullName.Contains("ICollection"))
                        {
                            aliasCount++;
                            string alias = "alias" + aliasCount.ToString();
                            if (!filterPart.IsNullOrEmpty())
                                filterPart = filterPart.Replace("#where#", String.Format(" where Exists(select 1 from {0}.{1} as {2} #where#)", directPath, propertyNavigationName, alias));
                            else
                                filterPart = String.Format("Exists(select 1 from {0}.{1} as {2} #where#)", directPath, propertyNavigationName, alias);

                            if (idx == parts.Length - 1)
                            {
                                if (optr == "In" || optr == "!In")
                                    filterPart = filterPart.Replace("#where#", " where " + alias + "." + propertyName + " " + optr + " " + value.ToString());
                                else if (value == null || (value.ToString().ToLower() == "null"))
                                {
                                    if (optr == "==" || optr == "=")
                                        optr = "is";
                                    else if (optr == "!=" || optr == "<>")
                                        optr = "is not";
                                    filterPart = filterPart.Replace("#where#", " where " + alias + "." + propertyName + " " + optr + " null");
                                }
                                else
                                {
                                    string parameterName = "param" + (parameters.Count + 1).ToString();
                                    filterPart = filterPart.Replace("#where#", " where " + alias + "." + propertyName + " " + optr + " @" + parameterName);
                                    parameters.Add(new ObjectParameter(parameterName, value));
                                }
                            }

                            directPath = alias;
                        }
                        else
                        {
                            directPath += (directPath.IsNullOrEmpty() ? "" : ".") + propertyNavigationName;

                            if (idx == parts.Length - 1)
                            {
                                if (!filterPart.IsNullOrEmpty() && filterPart.Contains("#where#"))
                                    filterPart = filterPart.Replace("#where#", " where #filter#");
                                else
                                    filterPart = "#filter#";

                                if (optr == "In" || optr == "!In")
                                    filterPart = filterPart.Replace("#filter#", directPath + "." + propertyName + " " + optr + " " + value.ToString());
                                else if (value == null || (value.ToString().ToLower() == "null"))
                                {
                                    if (optr == "==" || optr == "=")
                                        optr = "is";
                                    else if (optr == "!=" || optr == "<>")
                                        optr = "is not";
                                    filterPart = filterPart.Replace("#filter#", directPath + "." + propertyName + " " + optr + " null");
                                }
                                else
                                {
                                    string parameterName = "param" + (parameters.Count + 1).ToString();
                                    filterPart = filterPart.Replace("#filter#", directPath + "." + propertyName + " " + optr + " @" + parameterName);
                                    parameters.Add(new ObjectParameter(parameterName, value));
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (optr == "In" || optr == "!In")
                        filterPart = directPath + "." + propertyName + " " + optr + " " + value.ToString();
                    else if (value == null || (value.ToString().ToLower() == "null"))
                    {
                        if (optr == "==" || optr == "=")
                            optr = "is";
                        else if (optr == "!=" || optr == "<>")
                            optr = "is not";
                        filterPart = directPath + "." + propertyName + " " + optr + " null";
                    }
                    else
                    {
                        string parameterName = "param" + (parameters.Count + 1).ToString();
                        filterPart = directPath + "." + propertyName + " " + optr + " @" + parameterName;
                        parameters.Add(new ObjectParameter(parameterName, value));
                    }
                }
            }

            return filterPart;
        }

        private static List<string> GetJExpressionParts(string jExpressionParts, string dataPrefix = "")
        {
            List<string> result = new List<string>();

            if (!jExpressionParts.IsNullOrEmpty())
            {
                var filtersList = jExpressionParts.Split(new string[] { "---" }, StringSplitOptions.RemoveEmptyEntries);
                string jExpression;
                foreach (string jExpressionPart in filtersList)
                {
                    string externalMetadaData = jExpressionPart.Left(":::");
                    if (externalMetadaData.IsNullOrEmpty())
                        jExpression = jExpressionPart;
                    else
                        jExpression = jExpressionPart.Right(":::");

                    if (jExpression.IsNullOrEmpty())
                        continue;

                    string[] entityExpressions = jExpression.Split(new char[] { '}' }, StringSplitOptions.RemoveEmptyEntries);
                    string eParentheses = String.Empty, eCondition = String.Empty;
                    foreach (string entityExpression in entityExpressions)
                    {
                        string entityName = entityExpression.Left("{");
                        if (!entityName.IsNullOrEmpty())
                        {
                            if (entityName == "SID")
                            {
                                if (SecurityHelper != null)
                                {
                                    //Get here the SID filter definition
                                    result.AddRange(GetJExpressionParts(SecurityHelper.GetCustomSearchById(Convert.ToInt64(entityExpression.Right("{")))));
                                }
                            }
                            else if (entityName.InList("&&", "||", "(", ")") || entityName == "*")
                            {
                                if (entityExpression.Contains("*SID("))
                                {
                                    string[] entityExpressionsBySID = entityExpression.Split(new string[] { "*SID(" }, StringSplitOptions.RemoveEmptyEntries);
                                    result.Add((entityName != "*" || dataPrefix.IsNullOrEmpty() ? "" : dataPrefix + ".") + entityExpressionsBySID[0]);
                                    for (int idx = 1; idx < entityExpressionsBySID.Length; idx++)
                                    {
                                        string sidParts = entityExpressionsBySID[idx].Left(")");
                                        string sidPrefix = sidParts.Left(",");
                                        //Adjust prefix by parent prefix
                                        if (!dataPrefix.IsNullOrEmpty())
                                        {
                                            if (sidPrefix.IsNullOrEmpty())
                                                sidPrefix = dataPrefix;
                                            else
                                                sidPrefix = dataPrefix + ("#" + sidPrefix).Right("#" + sidPrefix.Left(".") + ".");
                                        }

                                        string sidValue = sidParts.Right(",");

                                        //Replace inner SID expressions
                                        foreach (string sidPart in GetJExpressionParts(SecurityHelper.GetCustomSearchById(Convert.ToInt64(sidValue)), sidPrefix))
                                        {
                                            result.Add(sidPart);
                                        }

                                        result.Add((entityName != "*" || dataPrefix.IsNullOrEmpty() ? "" : dataPrefix + ".") + "*{" + entityExpressionsBySID[idx].Right(sidParts + ")"));
                                    }

                                }
                                else
                                {
                                    result.Add((entityName != "*" || dataPrefix.IsNullOrEmpty() ? "" : dataPrefix + ".") + entityExpression);
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Conver a jExpression to DbFilterElements
        /// </summary>
        /// <param name="jExpression">JExpression definition</param>
        /// <returns></returns>
        private static List<DbFilterElement> JExpressionToDbFilters(this string jExpressionParts)
        {
            List<DbFilterElement> filterElements = new List<DbFilterElement>();

            if (!jExpressionParts.IsNullOrEmpty())
            {
                string eParentheses = String.Empty, eCondition = String.Empty, prefix = String.Empty;
                foreach (string entityExpression in GetJExpressionParts(jExpressionParts))
                {
                    string entityName = entityExpression.Left("{");
                    if (!entityName.IsNullOrEmpty())
                    {
                        if (entityName.InList("&&", "||"))
                        {
                            eCondition = entityName;
                            if (filterElements.Count > 0 && (!(filterElements.Last() is DbFilterCondition) || !((DbFilterCondition)filterElements.Last()).Content.InList("&&", "||", "(")))
                            {
                                filterElements.Add(new DbFilterCondition(eCondition));
                            }
                            continue;
                        }

                        if (entityName.InList("(", ")"))
                        {
                            eParentheses = entityName;

                            if (eParentheses == ")" && filterElements.Count > 0 && (filterElements.Last() is DbFilterCondition) && ((DbFilterCondition)filterElements.Last()).Content.InList("&&", "||", "("))
                            {
                                filterElements.RemoveAt(filterElements.Count - 1);
                            }
                            else
                            {
                                filterElements.Add(new DbFilterCondition(eParentheses));
                            }
                            continue;
                        }

                        //Not BM Filter
                        if ((" " + entityName).Right(1) != "*")
                        {
                            //This is not an Entity BM
                            continue;
                        }

                        prefix = (entityName.Length > 2 && entityName.Right(2) == ".*" ? entityName.Left(entityName.Length - 1) : "");
                        eParentheses = String.Empty;
                        eCondition = String.Empty;
                        string[] expressions = entityExpression.Right("{").Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string expression in expressions)
                        {
                            if (expression.InList("&&", "||"))
                            {
                                filterElements.Add(new DbFilterCondition(expression));
                            }
                            else if (expression.InList("(", ")"))
                            {
                                filterElements.Add(new DbFilterCondition(expression));
                            }
                            else
                            {
                                string[] expParts = expression.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
                                if (expParts.Length == 3 || expParts.Length == 4)
                                {
                                    string propName = expParts[expParts.Length - 3];
                                    if (!prefix.IsNullOrEmpty())
                                    {
                                        propName = prefix + ("#" + propName).Right("#" + propName.Left(".") + ".");
                                    }
                                    string oprt = expParts[expParts.Length - 2];
                                    string strValue = expParts[expParts.Length - 1];
                                    char dataType = strValue[0];
                                    strValue = strValue.Right(strValue.Length - 1);
                                    object value = Linx.Tools.EntitySearch.ParseJValue((oprt.InList("In", "!In") ? "S" : dataType.ToString()) + strValue);
                                    if (value.ToString() != "DelExpr") // If not delete expression
                                    {
                                        if (filterElements.Count > 0)
                                        {
                                            if (!(filterElements.Last() is DbFilterCondition))
                                            {
                                                string condition = (expParts.Length == 3 ? "&&" : expParts[0]);
                                                if (!condition.InList("&&", "||"))
                                                    condition = "&&";

                                                filterElements.Add(new DbFilterCondition(condition));
                                            }
                                        }
                                        var predefinedExpr = GetPredefinedExpression(propName, value, dataType, oprt);
                                        if (predefinedExpr.Count == 0)
                                            filterElements.Add(new DbFilterExpression(propName, oprt, value, dataType));
                                        else
                                            filterElements.AddRange(predefinedExpr);
                                    }
                                }
                            }
                        }
                    }
                }

            }


            return filterElements;
        }

        private static List<DbFilterElement> GetPredefinedExpression(string propName, object propValue, char dataType, string oprt)
        {
            List<DbFilterElement> result = new List<DbFilterElement>();

            if (propValue is string && propValue.ToString().Occurs("$") == 2)
            {

                string predefinedValue = propValue.ToString().Extract("$", "$");
                string value = propValue.ToString().Right("$" + predefinedValue + "$");
                DateTime currentDate = DateTime.Now.Date, startDate = DateTime.MinValue, endDate = DateTime.MinValue;
                bool hasExpression = true;

                switch (predefinedValue)
                {
                    case "CurrentDate":
                        startDate = currentDate;
                        endDate = currentDate.AddDays(1).AddSeconds(-1);
                        break;

                    case "CurrentYear":
                        startDate = DateTime.ParseExact(String.Format("01/01/{0} 00:00:00", currentDate.Year), "dd/MM/yyyy HH:mm:ss", null);
                        endDate = DateTime.ParseExact(String.Format("31/12/{0} 23:59:59", currentDate.Year), "dd/MM/yyyy HH:mm:ss", null);
                        break;

                    case "LastYear":
                        startDate = DateTime.ParseExact(string.Format("01/01/{0} 00:00:00", currentDate.Year - 1), "dd/MM/yyyy HH:mm:ss", null);
                        endDate = DateTime.ParseExact(string.Format("31/12/{0} 23:59:59", currentDate.Year - 1), "dd/MM/yyyy HH:mm:ss", null);
                        break;

                    case "2YearsAgo":
                        startDate = DateTime.ParseExact(string.Format("01/01/{0} 00:00:00", currentDate.Year - 2), "dd/MM/yyyy HH:mm:ss", null);
                        endDate = DateTime.ParseExact(string.Format("31/12/{0} 23:59:59", currentDate.Year - 2), "dd/MM/yyyy HH:mm:ss", null);
                        break;

                    case "CurrentTrimester":
                        if (currentDate.Month >= 1 && currentDate.Month <= 3)
                            startDate = DateTime.ParseExact(string.Format("01/01/{0} 00:00:00", currentDate.Year), "dd/MM/yyyy HH:mm:ss", null);
                        else if (currentDate.Month >= 4 && currentDate.Month <= 6)
                            startDate = DateTime.ParseExact(string.Format("01/04/{0} 00:00:00", currentDate.Year), "dd/MM/yyyy HH:mm:ss", null);
                        else if (currentDate.Month >= 7 && currentDate.Month <= 9)
                            startDate = DateTime.ParseExact(string.Format("01/07/{0} 00:00:00", currentDate.Year), "dd/MM/yyyy HH:mm:ss", null);
                        else
                            startDate = DateTime.ParseExact(string.Format("01/10/{0} 00:00:00", currentDate.Year), "dd/MM/yyyy HH:mm:ss", null);

                        endDate = startDate.AddMonths(3).AddSeconds(-1);

                        break;

                    case "LastTrimester":
                        if (currentDate.Month >= 1 && currentDate.Month <= 3)
                            startDate = DateTime.ParseExact(string.Format("01/10/{0} 00:00:00", currentDate.Year - 1), "dd/MM/yyyy HH:mm:ss", null);
                        else if (currentDate.Month >= 4 && currentDate.Month <= 6)
                            startDate = DateTime.ParseExact(string.Format("01/01/{0} 00:00:00", currentDate.Year), "dd/MM/yyyy HH:mm:ss", null);
                        else if (currentDate.Month >= 7 && currentDate.Month <= 9)
                            startDate = DateTime.ParseExact(string.Format("01/04/{0} 00:00:00", currentDate.Year), "dd/MM/yyyy HH:mm:ss", null);
                        else
                            startDate = DateTime.ParseExact(string.Format("01/07/{0} 00:00:00", currentDate.Year), "dd/MM/yyyy HH:mm:ss", null);

                        endDate = startDate.AddMonths(3).AddSeconds(-1);

                        break;

                    case "CurrentMonth":
                        startDate = DateTime.ParseExact(string.Format("01/{0}/{1} 00:00:00", currentDate.Month.ToString().PadLeft(2, '0'), currentDate.Year), "dd/MM/yyyy HH:mm:ss", null);
                        endDate = startDate.AddMonths(1).AddSeconds(-1);
                        break;

                    case "LastMonth":
                        startDate = currentDate.AddMonths(-1);
                        startDate = DateTime.ParseExact(string.Format("01/{0}/{1} 00:00:00", startDate.Month.ToString().PadLeft(2, '0'), startDate.Year), "dd/MM/yyyy HH:mm:ss", null);
                        endDate = startDate.AddMonths(1).AddSeconds(-1);
                        break;

                    case "2MonthsAgo":
                        startDate = currentDate.AddMonths(-2);
                        startDate = DateTime.ParseExact(string.Format("01/{0}/{1} 00:00:00", startDate.Month.ToString().PadLeft(2, '0'), startDate.Year), "dd/MM/yyyy HH:mm:ss", null);
                        endDate = startDate.AddMonths(1).AddSeconds(-1);
                        break;

                    case "CurrentWeek":
                        startDate = currentDate.AddDays(-Convert.ToInt16(currentDate.DayOfWeek));
                        endDate = startDate.AddDays(7).AddSeconds(-1);
                        break;

                    case "LastWeek":
                        startDate = currentDate.AddDays(-(7 + Convert.ToInt16(currentDate.DayOfWeek)));
                        endDate = startDate.AddDays(7).AddSeconds(-1);
                        break;

                    case "2WeeksAgo":
                        startDate = currentDate.AddDays(-(14 + Convert.ToInt16(currentDate.DayOfWeek)));
                        endDate = startDate.AddDays(7).AddSeconds(-1);
                        break;

                    case "CurrrentDate":
                        startDate = currentDate;
                        endDate = currentDate.AddDays(1).AddSeconds(-1); ;
                        break;

                    case "MonthToDate":
                        startDate = DateTime.ParseExact(string.Format("01/{0}/{1} 00:00:00", currentDate.Month.ToString().PadLeft(2, '0'), currentDate.Year), "dd/MM/yyyy HH:mm:ss", null);
                        endDate = currentDate.AddDays(1).AddSeconds(-1); ;
                        break;

                    case "YearToDate":
                        startDate = DateTime.ParseExact(String.Format("01/01/{0} 00:00:00", currentDate.Year), "dd/MM/yyyy HH:mm:ss", null);
                        endDate = currentDate.AddDays(1).AddSeconds(-1); ;
                        break;

                    case "XDays":
                        startDate = currentDate.AddDays(Convert.ToDouble(value));
                        endDate = startDate.AddDays(1).AddSeconds(-1); ;
                        break;

                    case "XWeeks":
                        startDate = currentDate.AddDays(-Convert.ToInt16(currentDate.DayOfWeek));
                        startDate = startDate.AddDays(Convert.ToDouble(value) * 7);
                        endDate = startDate.AddDays(7).AddSeconds(-1);
                        break;

                    case "XMonths":
                        startDate = currentDate.AddMonths(Convert.ToInt16(value));
                        startDate = DateTime.ParseExact(string.Format("01/{0}/{1} 00:00:00", startDate.Month.ToString().PadLeft(2, '0'), startDate.Year), "dd/MM/yyyy HH:mm:ss", null);
                        endDate = startDate.AddMonths(1).AddSeconds(-1);
                        break;

                    case "XYears":
                        startDate = DateTime.ParseExact(string.Format("01/01/{0} 00:00:00", currentDate.AddYears(Convert.ToInt16(value)).Year), "dd/MM/yyyy HH:mm:ss", null);
                        endDate = DateTime.ParseExact(string.Format("31/12/{0} 23:59:59", startDate.Year), "dd/MM/yyyy HH:mm:ss", null);
                        break;

                    case "XDaysToDate":
                        startDate = currentDate.AddDays(Convert.ToDouble(value) * -1);
                        endDate = currentDate.AddDays(1).AddSeconds(-1); ;
                        break;

                    case "XWeeksToDate":
                        startDate = currentDate.AddDays(-Convert.ToInt16(currentDate.DayOfWeek));
                        startDate = startDate.AddDays((Convert.ToDouble(value) * -1) * 7);
                        endDate = currentDate.AddDays(1).AddSeconds(-1);
                        break;

                    case "XMonthsToDate":
                        startDate = currentDate.AddMonths(-Convert.ToInt16(value));
                        startDate = DateTime.ParseExact(string.Format("01/{0}/{1} 00:00:00", startDate.Month.ToString().PadLeft(2, '0'), startDate.Year), "dd/MM/yyyy HH:mm:ss", null);
                        endDate = currentDate.AddDays(1).AddSeconds(-1);
                        break;

                    case "XYearsToDate":
                        startDate = DateTime.ParseExact(string.Format("01/01/{0} 00:00:00", currentDate.AddYears(Convert.ToInt16(value) * -1).Year), "dd/MM/yyyy HH:mm:ss", null);
                        endDate = currentDate.AddDays(1).AddSeconds(-1);
                        break;


                    case "Param":
                        hasExpression = false;
                        Linx.Tools.IParametersHelper parametersHelper = null;
                        try { parametersHelper = Linx.Tools.ImplementationHelper<IParametersHelper>.GetInstance("ParametersHelper", "Linx.Business.Tools"); }
                        catch { }
                        if (parametersHelper != null)
                        {
                            var paramValue = parametersHelper.GetParameter<string>(value, new Dictionary<string, string>());
                            result.Add(new DbFilterExpression(propName, oprt, paramValue, dataType));
                        }
                        break;

                    default:
                        hasExpression = false;
                        break;
                }

                if (hasExpression)
                {
                    result.Add(new DbFilterExpression(propName, ">=", startDate, dataType));
                    result.Add(new DbFilterCondition("&&"));
                    result.Add(new DbFilterExpression(propName, "<=", endDate, dataType));
                }
            }

            return result;
        }


        #endregion
    }

    /// <summary>
    /// Class for supporting special search 
    /// </summary>
    public class BmMetaDataProperty
    {
        public string id { get; set; }
        public string parent { get; set; }
        public string text { get; set; }
        public bool children { get; set; }
        public bool enabled { get; set; }
        public char dataType { get; set; }
        public string entityName { get; set; }
    }
    public class OlapMetaDataProperty : BmMetaDataProperty { }


    internal class DbFilterElement
    {
    }

    internal class DbFilterCondition : DbFilterElement
    {
        public DbFilterCondition(string content)
        {
            this.Content = content;
        }

        public string Content { get; set; }
    }

    internal class DbFilterExpression : DbFilterElement
    {
        public DbFilterExpression(string entityDataPath, string optr, object value, char dataType)
        {
            this.EntityDataPath = entityDataPath;
            this.Operator = optr;
            this.Value = value;
            this.DataType = dataType;
        }

        public string EntityDataPath { get; set; }
        public string Operator { get; set; }
        public object Value { get; set; }
        public char DataType { get; set; }

    }
}

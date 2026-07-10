using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Reflection;

namespace Linx.Tools
{
    public delegate void EntitySearchHandler(List<EntitySearch> searchList);

    public enum EntitySearchExpressionName
    {
        Field, Operator, Value, Condition, PredefinedFilter
    }

    /// <summary>
    /// Class for enable entity search.
    /// </summary>
    public class EntitySearch
    {
        public EntitySearch() { }

        public EntitySearch(string entityName)
        {
            this.EntityName = entityName;
        }

        public string EntityName { get; set; }
        public string SubQueryInfo { get; set; }
        public string EdmEntityName { get; set; }
        public string EdmParentEntityName { get; set; }
        public string BaseEntityNames { get; set; }
        private string _connectionCondition = "&&";
        public string ConnectionCondition { get { return _connectionCondition; } set { _connectionCondition = value; } }
        public string Parentheses { get; set; }
        public int QueryGroup { get; set; }
        public string EconomicGroups { get; set; }
        public string ParamSuffix { get; set; }
        public int MyProperty { get; set; }
        public string EntityRelations { get; set; }

        private List<EntitySearchExpression> expressions;
        /// <summary>
        /// Expression class:
        /// Examples: 
        /// this.Expressions.Add( new EntitySearchExpression(EntitySearchExpressionName.Field, "Field Name") );
        /// this.Expressions.Add( new EntitySearchExpression(EntitySearchExpressionName.Operator, "==") );
        /// this.Expressions.Add( new EntitySearchExpression(EntitySearchExpressionName.Value, value) );
        /// this.Expressions.Add( new EntitySearchExpression(EntitySearchExpressionName.Condition, "&&") );
        /// this.Expressions.Add( new EntitySearchExpression(EntitySearchExpressionName.Field, "Field Name 2") );
        /// this.Expressions.Add( new EntitySearchExpression(EntitySearchExpressionName.Operator, ">") );
        /// this.Expressions.Add( new EntitySearchExpression(EntitySearchExpressionName.Value, otherValue) );
        /// </summary>
        public List<EntitySearchExpression> Expressions
        {
            get
            {
                if (expressions == null)
                    expressions = new List<EntitySearchExpression>();
                return expressions;
            }
        }

        public static string GetEdmKeyByProperty(Type dataClassType, string entityName, string propertyName)
        {
            if (dataClassType != null)
            {
                Type dataType = dataClassType;
                if (entityName.IsNullOrEmpty())
                    entityName = dataType.Name;
                return Linx.Tools.EntitySearch.GetFilterDataKeyByProperty(dataType, entityName, propertyName);
            }
            else return String.Empty;
        }

        public static void AdjustFiltersByInnerRelation(Type topDataType, List<EntitySearch> parentSearchList, List<EntitySearch> innerQueries, Type innerDataType, string parentFieldsRelation, string detailFieldsRelation, string parentSelectorTypeName)
        {
            EntitySearch searchInnerLink, searchToLink, parentEntity;

            //Get origin filter
            searchInnerLink = innerQueries.Where(e => !e.EdmEntityName.IsNullOrEmpty() && e.EdmParentEntityName.IsNullOrEmpty()).FirstOrDefault();
            if (searchInnerLink != null)
            {
                //Get destiny filter 
                string[] relationsForReplacing = null;
                searchToLink = parentSearchList.FirstOrDefault(e => e.EntityName == parentSelectorTypeName && e.EdmEntityName == searchInnerLink.EdmEntityName);
                if (searchToLink == null)
                {
                    searchToLink = parentSearchList.FirstOrDefault(e => e.EntityName == parentSelectorTypeName && e.EntityRelations != null && e.EntityRelations.Contains("(" + searchInnerLink.EdmEntityName + ")"));
                    if (searchToLink != null)
                        relationsForReplacing = searchToLink.EntityRelations.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries).Where(e => e.Contains("(" + searchInnerLink.EdmEntityName + ")")).Select(e => e.Left("(")).ToArray();
                }

                if (searchToLink == null)
                {
                    if (!parentFieldsRelation.IsNullOrEmpty() && !detailFieldsRelation.IsNullOrEmpty()) //Resolve Subquery
                    {
                        string[] parentFields = parentFieldsRelation.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        string[] detailFields = detailFieldsRelation.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                        if (parentFields.Length != detailFields.Length)
                            return;

                        //Adjust subquery
                        parentEntity = parentSearchList.FirstOrDefault(e => e.EntityName == parentSelectorTypeName);
                        if (parentEntity == null)
                            return;
                        Type parentDataType = GetInternalType(topDataType, parentEntity.EntityName);
                        if (parentDataType == null)
                            return;

                        string parentField, detailField;
                        string whereClause = String.Empty;
                        for (int idxF = 0; idxF < parentFields.Length; idxF++)
                        {
                            parentField = GetEdmKeyByProperty(parentDataType, parentEntity.EntityName, parentFields[idxF]);
                            detailField = GetEdmKeyByProperty(innerDataType, String.Empty, detailFields[idxF]);
                            whereClause += (whereClause.IsNullOrEmpty() ? String.Empty : " && ") + "#Alias#." + ("#" + detailField).Right("#" + searchInnerLink.EdmEntityName + ".") + " == #ParentAlias#." + ("#" + parentField).Right("#" + parentEntity.EdmEntityName + ".");
                        }

                        if (whereClause.IsNullOrEmpty())
                            return;

                        searchInnerLink.SubQueryInfo = "Select 1 From " + searchInnerLink.EdmEntityName + " as #Alias#  where " + whereClause;
                        searchInnerLink.EdmParentEntityName = parentEntity.EdmEntityName;
                    }
                }

                if (searchToLink != null && searchInnerLink.Expressions.Count > 0)
                {
                    //Remove parent queries
                    innerQueries.Remove(searchInnerLink);

                    if (searchInnerLink.Expressions.Where(e => !e.Excluded).Count() > 0 && searchToLink.Expressions.Where(e => !e.Excluded).Count() > 0)
                        searchToLink.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "&&"));

                    if (relationsForReplacing != null && relationsForReplacing.Length > 0)
                    {
                        string relationForReplacing = relationsForReplacing[0];
                        //Verify if get the relation by the related properties 
                        if (relationsForReplacing.Length > 0)
                        {
                            string[] parentFields = parentFieldsRelation.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                            if (parentFields.Length > 0)
                            {
                                //Adjust subquery
                                parentEntity = parentSearchList.FirstOrDefault(e => e.EntityName == parentSelectorTypeName);
                                if (parentEntity != null)
                                {
                                    Type parentDataType = GetInternalType(topDataType, parentEntity.EntityName);
                                    if (parentDataType != null)
                                    {
                                        string parentField = GetEdmKeyByProperty(parentDataType, parentEntity.EntityName, parentFields[0]);
                                        relationForReplacing = ("#" + parentField + "#").Right("#" + parentEntity.EdmEntityName + ".").Left("." + parentField.Right(".") + "#");
                                    }
                                }
                            }
                        }
                        if (!relationForReplacing.IsNullOrEmpty())
                        {
                            foreach (var expression in searchInnerLink.Expressions.ToList())
                            {
                                if (expression.Name == EntitySearchExpressionName.Field.ToString())
                                {
                                    expression.Value = searchToLink.EdmEntityName + "." + relationForReplacing + "." + ("#" + expression.Value.ToString()).Right("#" + searchInnerLink.EdmEntityName + ".");
                                }
                            }
                        }
                    }

                    searchToLink.Expressions.AddRange(searchInnerLink.Expressions);
                }

                if (innerQueries.Count() > 0)
                    parentSearchList.AddRange(innerQueries);

            }
        }

        public static void AdjustExcludedFilters(List<EntitySearch> entitySearchList, List<string> excludedFilters)
        {
            if (excludedFilters.Count == 0)
                return;

            foreach (var entity in entitySearchList)
            {
                foreach (var exp in entity.Expressions.Where(e => !e.Excluded && e.Name == "Field"))
                {
                    if (excludedFilters.Contains(entity.EntityName + "|" + exp.Value.ToString()))
                    {
                        exp.Excluded = true;
                        var index = entity.Expressions.IndexOf(exp);
                        if (index >= 0)
                        {
                            //Adjust before condition
                            int beforeCondition = index - 1;
                            bool beforeConditionIsExcluded = false;
                            if (beforeCondition >= 0 && entity.Expressions[beforeCondition].Name == "Condition" && entity.Expressions[beforeCondition].Value.ToString().InList("&&", "||") && !entity.Expressions[beforeCondition].Excluded)
                            {
                                entity.Expressions[beforeCondition].Excluded = true;
                                beforeConditionIsExcluded = true;
                            }

                            index++;
                            if (index < entity.Expressions.Count)
                            {
                                var oprator = entity.Expressions[index];
                                if (oprator.Name == "Operator")
                                {
                                    oprator.Excluded = true;

                                    index++;
                                    if (index < entity.Expressions.Count)
                                    {
                                        var value = entity.Expressions[index];
                                        if (value.Name == "Value")
                                        {
                                            value.Excluded = true;

                                            //Adjust after condition
                                            int afterCondition = index + 1;
                                            if (!beforeConditionIsExcluded && afterCondition >= 0 && afterCondition < entity.Expressions.Count && entity.Expressions[afterCondition].Name == "Condition" && entity.Expressions[afterCondition].Value.ToString().InList("&&", "||"))
                                            {
                                                entity.Expressions[afterCondition].Excluded = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public static string ParseFromJEntitySearch(string jEntitySearchParts, bool onlyFirstElement = false)
        {
            return ParseFromJEntitySearch(null, jEntitySearchParts, onlyFirstElement);
        }

        public static string ParseFromJEntitySearch(Type topDataType, string jEntitySearchParts, bool onlyFirstElement)
        {
            return ParseFromJEntitySearch(topDataType, jEntitySearchParts, onlyFirstElement, false);
        }


        /// <summary>
        /// Conver a jExpression to an EntitySearch
        /// </summary>
        /// <param name="topDataType">Top parent data type</param>
        /// <param name="jEntitySearchParts">Jexpression definition</param>
        /// <param name="onlyFirstElement">Returns just the first search</param>
        /// <param name="promoteSubQueries">Transforms sub-searchs in top parent search </param>
        /// <returns></returns>
        public static string ParseFromJEntitySearch(Type topDataType, string jEntitySearchParts, bool onlyFirstElement, bool promoteSubQueries, bool isOlap = false)
        {
            List<EntitySearch> searchs = new List<EntitySearch>();
            List<EntitySearch> topSearchList = new List<EntitySearch>();
            var topProperties = (topDataType == null ? new string[] { } : topDataType.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).Select(p => p.Name).ToArray());
            string serializedEntitySearch = String.Empty;
            string externalKeyType = String.Empty, externalDetailFieldsRelation = String.Empty, externalParentFieldsRelation = String.Empty, jEntitySearch = String.Empty, parentSelectorType = String.Empty;
            var filtersList = jEntitySearchParts.Split(new string[] { "---" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string jEntitySearchPart in filtersList)
            {
                if (!jEntitySearchPart.IsNullOrEmpty())
                {
                    string externalMetadaData = jEntitySearchPart.Left(":::");
                    if (externalMetadaData.IsNullOrEmpty())
                    {
                        externalKeyType = "";
                        parentSelectorType = "";
                        externalParentFieldsRelation = "";
                        externalDetailFieldsRelation = "";
                        jEntitySearch = jEntitySearchPart;
                    }
                    else
                    {
                        var metadaParts = externalMetadaData.Split(new char[] { '|' });
                        if (metadaParts.Length > 3)
                        {
                            externalKeyType = metadaParts[0];
                            parentSelectorType = metadaParts[1];
                            externalParentFieldsRelation = metadaParts[2];
                            externalDetailFieldsRelation = metadaParts[3];
                        }
                        jEntitySearch = jEntitySearchPart.Right(":::");
                    }

                    string[] entityExpressions = jEntitySearch.Split(new char[] { '}' }, StringSplitOptions.RemoveEmptyEntries);
                    searchs.Clear();
                    string eParentheses = String.Empty, eCondition = String.Empty;
                    foreach (string entityExpression in entityExpressions)
                    {
                        string entityName = entityExpression.Left("{");
                        if (!entityName.IsNullOrEmpty())
                        {
                            EntitySearch search = null;
                            if (entityName.InList("&&", "||"))
                            {
                                eCondition = entityName;
                                continue;
                            }

                            if (entityName.InList("(", ")"))
                            {
                                eParentheses = entityName;
                                var lastSearch = topSearchList.Union(searchs).LastOrDefault();
                                if (eParentheses == ")" && lastSearch != null && topSearchList.Union(searchs).Count(e => e.Parentheses == "(") > topSearchList.Union(searchs).Count(e => e.Parentheses == ")"))
                                {
                                    if (lastSearch.Parentheses.IsNullOrEmpty())
                                        lastSearch.Parentheses = eParentheses;
                                    else
                                        lastSearch.Parentheses = "";
                                    eParentheses = String.Empty;
                                }
                                continue;
                            }

                            if (entityName == "SID")
                            {
                                //Get here the SID filter definition
                                continue;
                            }

                            //BM Filter
                            if (entityName == "*" && !isOlap)
                            {
                                //This is not an Entity BM
                                continue;
                            }

                            if (!externalKeyType.IsNullOrEmpty() && topSearchList.Any(e => e.EntityName == entityName))
                            {
                                search = topSearchList.FirstOrDefault(e => e.EntityName == entityName);
                            }
                            else
                                search = new EntitySearch(entityName) { Parentheses = eParentheses, ConnectionCondition = eCondition };

                            eParentheses = String.Empty;
                            eCondition = String.Empty;
                            string[] expressions = entityExpression.Right("{").Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string expression in expressions)
                            {
                                if (expression.InList("&&", "||"))
                                {
                                    search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, expression));
                                }
                                else if (expression.InList("(", ")"))
                                {
                                    search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, expression));
                                }
                                else
                                {
                                    string[] expParts = expression.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
                                    if (expParts.Length == 3 || expParts.Length == 4)
                                    {
                                        object value = ParseJValue(expParts[expParts.Length - 1].DecodeUrlString());
                                        if (value.ToString() != "DelExpr") // If not delete expression
                                        {
                                            if (search.Expressions.Count > 0)
                                            {
                                                if (search.Expressions.Last().Name != "Condition")
                                                {
                                                    string condition = (expParts.Length == 3 ? "&&" : expParts[0]);
                                                    if (!condition.InList("&&", "||"))
                                                        condition = "&&";

                                                    if (topDataType == null || search.EntityName == "LinqValidProperties" || !promoteSubQueries || search.EntityName == topDataType.Name || topProperties.Contains(expParts[expParts.Length - 3]))
                                                        search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, condition));
                                                }
                                            }
                                            if (topDataType == null || search.EntityName == "LinqValidProperties" || !promoteSubQueries || search.EntityName == topDataType.Name || topProperties.Contains(expParts[expParts.Length - 3]))
                                            {
                                                //Adjust pre-defined value
                                                if (value is string && value.ToString().Occurs("$") == 2 && ContainsPredefinedFilter(value.ToString()))
                                                {
                                                    string predefinedValue = value.ToString().Extract("$", "$");
                                                    string xValue = value.ToString().Right("$" + predefinedValue + "$");
                                                    value = "Field[" + expParts[expParts.Length - 3] + "]PredefinedValue[" + predefinedValue + "]Value[" + xValue + "]";
                                                    search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, "PredefinedFilter"));
                                                    search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
                                                    search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, value));

                                                }
                                                else
                                                {
                                                    search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, expParts[expParts.Length - 3]));
                                                    search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, expParts[expParts.Length - 2]));
                                                    search.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, value));
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (!topSearchList.Contains(search) && (search.Expressions.Count > 0 || filtersList.Length > 1))
                                searchs.Add(search);
                        }
                    }

                    if (promoteSubQueries && topDataType != null && topSearchList.Count > 0)
                    {
                        var topSearch = topSearchList.FirstOrDefault(e => e.EntityName == topDataType.Name);
                        if (topSearch != null)
                        {
                            foreach (var subQuerySearch in searchs.Where(e => e.EntityName != "LinqValidProperties" && e.Expressions.Count > 0).ToArray())
                            {
                                if (topSearch.Expressions.Count > 0)
                                    topSearch.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "&&"));

                                topSearch.Expressions.AddRange(subQuerySearch.Expressions);
                            }
                            topSearchList.AddRange(searchs.Where(e => e.EntityName == "LinqValidProperties" && e.Expressions.Count > 0));
                        }
                    }
                    else
                    {
                        if (externalKeyType.IsNullOrEmpty())
                        {
                            if (searchs.Count > 0)
                            {
                                topSearchList.AddRange(ReplaceFieldToFilterDataKey(searchs, topDataType, true, false));
                            }
                        }
                        else if (topDataType != null && topSearchList.Count > 0)
                        {
                            string assemblyPattern = "";
                            var assemblyParts = externalKeyType.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                            for (int idx = 0; idx < assemblyParts.Length - 2; idx++)
                                assemblyPattern += (assemblyPattern.IsNullOrEmpty() ? "" : ".") + assemblyParts[idx];

                            var innerData = ImplementationHelper<object>.GetInstance(externalKeyType, assemblyPattern);
                            if (innerData != null)
                            {
                                var innerDataType = innerData.GetType();
                                EntitySearch.AdjustFiltersByInnerRelation(topDataType, topSearchList, ReplaceFieldToFilterDataKey(searchs, innerDataType), innerDataType, externalParentFieldsRelation, externalDetailFieldsRelation, parentSelectorType);
                            }
                        }
                    }
                }
            }

            if (topSearchList.Count > 0)
            {
                serializedEntitySearch = (onlyFirstElement ? SerializationManager<EntitySearch>.ObjectToString(topSearchList[0]) : SerializationManager<List<EntitySearch>>.ObjectToString(topSearchList));
            }

            return serializedEntitySearch;
        }

        private static bool ContainsPredefinedFilter(string valueSearch)
        {
            var filters = PredefinedFilter.LoadPredefinedFilters("All");

            return filters.Any(f => valueSearch.Contains(f.Condition));
        }

        public static object ParseJValue(string value)
        {
            object result = value;
            if (!value.IsNullOrEmpty())
            {
                if (value.Contains("DelExpr"))
                {
                    result = "DelExpr";
                }
                else if (value.Occurs("$") == 2 && ContainsPredefinedFilter(value)) //Predefined filter
                {
                    result = value.Right(value.Length - 1);
                }
                else
                {
                    char type = value[0];
                    switch (type)
                    {
                        case 'I':
                            result = int.Parse(value.Right(value.Length - 1));
                            break;
                        case 'Y':
                            result = byte.Parse(value.Right(value.Length - 1));
                            break;
                        case 'L':
                            result = long.Parse(value.Right(value.Length - 1));
                            break;
                        case 'H':
                            result = short.Parse(value.Right(value.Length - 1));
                            break;
                        case 'D':
                            result = decimal.Parse(value.Right(value.Length - 1), CultureInfo.InvariantCulture);
                            break;
                        case 'S':
                            result = value.Right(value.Length - 1);
                            break;
                        case 'C':
                            result = value.Right(value.Length - 1)[0];
                            break;
                        case 'T':
                            result = DateTime.Parse(value.Right(value.Length - 1));
                            break;
                        case 'B':
                            result = bool.Parse(value.Right(value.Length - 1));
                            break;
                        case 'G':
                            result = Guid.Parse(value.Right(value.Length - 1));
                            break;
                        case 'F':
                            result = float.Parse(value.Right(value.Length - 1), CultureInfo.InvariantCulture);
                            break;
                        default:
                            break;
                    }
                }
            }

            return result;
        }

        public static char ParseJDataType(string dataType)
        {
            char result = 'S';
            if (!dataType.IsNull())
            {
                dataType = dataType.ToLower();

                if (dataType.Contains("long") || dataType.Contains("int64"))
                    result = 'L';
                else if (dataType.Contains("short") || dataType.Contains("int16"))
                    result = 'H';
                else if (dataType.Contains("int"))
                    result = 'I';
                else if (dataType.Contains("byte"))
                    result = 'Y';
                else if (dataType.Contains("decimal") || dataType.Contains("double"))
                    result = 'D';
                else if (dataType.Contains("string"))
                    result = 'S';
                else if (dataType.Contains("char"))
                    result = 'C';
                else if (dataType.Contains("datetime"))
                    result = 'T';
                else if (dataType.Contains("bool"))
                    result = 'B';
                else if (dataType.Contains("guid"))
                    result = 'G';
                else if (dataType.Contains("float"))
                    result = 'F';
            }

            return result;
        }

        public static string[] GetLinqValidProperties(string[] linqValidProperties, Dictionary<string, string> fieldsMap = null)
        {
            List<string> validFields = new List<string>();

            if (!linqValidProperties.IsNullOrEmpty())
            {
                if (fieldsMap == null || fieldsMap.Count == 0)
                    return linqValidProperties;
                else
                {
                    foreach (string field in linqValidProperties)
                    {
                        if (fieldsMap.ContainsKey(field))
                            validFields.Add(fieldsMap[field]);
                    }
                }
            }

            return validFields.ToArray();
        }

        public static string[] GetValidProperties(List<EntitySearch> entitySearchList)
        {
            //Verify valid properties for LINQ
            string[] validProperties = new string[] { };
            foreach (EntitySearch linqValidation in entitySearchList.Where(e => e.EntityName == "LinqValidProperties").ToList())
            {
                entitySearchList.Remove(linqValidation);
                string linqValidProperties = linqValidation.GetExpressionValue("LinqValidProperties") as string;
                if (!linqValidProperties.IsNullOrEmpty())
                    validProperties = linqValidProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            }

            return validProperties;
        }


        /// <summary>
        /// Get full string expression and parameters from an EntitySearch object.
        /// </summary>
        /// <param name="query">String query expression.</param>
        /// <param name="parameters">Parameters for the string query expression.</param>
        public void GetFullExpression(ref string query, Dictionary<string, object> parameters, string alias, int startParamIndex, string paramSuffix = "")
        {
            string paramName, lastOperator = String.Empty, entName;
            object value = null;
            foreach (var expr in this.Expressions)
            {
                if (expr.Excluded)
                    continue;

                switch (expr.Name)
                {
                    case "Condition":
                        switch (expr.Value.ToString())
                        {
                            case "&&":
                                query += " And";
                                break;
                            case "||":
                                query += " Or";
                                break;
                            default:
                                query += " " + expr.Value.ToString();
                                break;
                        }
                        break;
                    case "Field":
                        entName = (this.EdmEntityName.IsNullOrEmpty() ? expr.Value.ToString().Left(".") : this.EdmEntityName);
                        query += " " + System.Text.RegularExpressions.Regex.Replace(expr.Value.ToString(), "(?<![a-zA-Z0-9_@])" + entName + "\\.", alias + ".");
                        break;
                    case "Operator":
                        lastOperator = expr.Value.ToString();
                        switch (lastOperator)
                        {
                            case "==":
                                query += " =";
                                break;
                            case "!=":
                                query += " <>";
                                break;
                            case "!":
                                query += " Not";
                                break;
                            case "Contains":
                                query += " Like";
                                break;
                            case "StartsWith":
                                query += " Like";
                                break;
                            case "EndsWith":
                                query += " Like";
                                break;
                            default:
                                query += " " + lastOperator.Replace("!In", "Not In").Replace("!Like", "Not Like");
                                break;
                        }
                        break;
                    case "Value":
                        switch (lastOperator)
                        {
                            case "In":
                            case "!In":
                                string exprValues = "";
                                if (!expr.Value.IsNullOrEmpty())
                                {
                                    exprValues = expr.Value.ToString();
                                    if (exprValues.StartsWith("S,") || exprValues.StartsWith("C,"))
                                    {
                                        exprValues = "'" + exprValues.Right(exprValues.Length - 2).Replace(",", "','") + "'";
                                    }
                                    else if (exprValues.StartsWith("G,"))
                                    {
                                        exprValues = "GUID'" + exprValues.Right(exprValues.Length - 2).Replace(",", "',GUID'") + "'";
                                    }
                                    else if (exprValues.StartsWith("T,"))
                                    {
                                        exprValues = "DATETIME'" + exprValues.Right(exprValues.Length - 2).Replace(",", "',DATETIME'") + "'";
                                    }
                                }
                                value = "{" + exprValues + "}";
                                break;
                            case "Contains":
                                value = "%" + (expr.Value.IsNullOrEmpty() ? "" : expr.Value.ToString()) + "%";
                                break;
                            case "StartsWith":
                                value = (expr.Value.IsNullOrEmpty() ? "" : expr.Value.ToString()) + "%";
                                break;
                            case "EndsWith":
                                value = "%" + (expr.Value.IsNullOrEmpty() ? "" : expr.Value.ToString());
                                break;
                            default:
                                value = expr.Value;
                                break;
                        }

                        if (lastOperator == "In" || lastOperator == "!In")
                            query += " " + value.ToString();
                        else if (value == null || (value.ToString().ToLower() == "null"))
                            query += " null";
                        else
                        {
                            paramName = (this.EntityName.IsNullOrEmpty() ? alias : this.EntityName) + "_P" + (startParamIndex + parameters.Count).ToString() + (paramSuffix.IsNullOrEmpty() ? string.Empty : paramSuffix);
                            paramName = paramName.Length > 30 ? "p" + paramName.Right(29) : paramName;
                            query += " @" + paramName;
                            parameters.Add(paramName, value);
                        }
                        break;
                    default:
                        break;
                }
            }

        }

        public static List<EntitySearch> ReplaceParentCompositionDataKey(List<EntitySearch> originalList, string edmOriginalParent, string edmNewParent, string edmNewParentNavigation, Type entityType, params Type[] siblings)
        {
            ReplaceFieldToFilterDataKeyByRef(originalList, entityType, true, true, true);
            foreach (var childType in siblings)
            {
                ReplaceFieldToFilterDataKeyByRef(originalList, childType, true, false, true);
            }

            foreach (var es in originalList.Where(e => e.EdmParentEntityName == edmOriginalParent && !e.SubQueryInfo.IsNullOrEmpty()).ToArray())
            {
                es.EdmParentEntityName = edmNewParent;
                es.SubQueryInfo = es.SubQueryInfo.Replace("#ParentAlias#.", "#ParentAlias#." + edmNewParentNavigation + ".");
            }

            return originalList;
        }

        public static List<EntitySearch> ReplaceFieldToFilterDataKey(List<EntitySearch> originalList, Type entityType, bool adjustConditions = true)
        {
            return ReplaceFieldToFilterDataKey(originalList, entityType, adjustConditions, true);
        }

        public static List<EntitySearch> ReplaceFieldToFilterDataKey(List<EntitySearch> originalList, Type entityType, bool adjustConditions, bool updateFilterDataKey)
        {
            if (entityType == null | originalList == null || originalList.Count == 0)
                return originalList;

            //Evaluate Predefined filters
            originalList = EntitySearch.EvaluatePredefinedFilters(originalList);

            //Copy original list for compatibility with the old version.
            List<EntitySearch> list = new List<EntitySearch>();
            EntitySearch newEntity;
            foreach (var entitySch in originalList)
            {
                newEntity = new EntitySearch()
                {
                    EntityName = (entitySch.EntityName.IsNullOrEmpty() ? entityType.Name : entitySch.EntityName),
                    SubQueryInfo = entitySch.SubQueryInfo,
                    EdmEntityName = entitySch.EdmEntityName,
                    EdmParentEntityName = entitySch.EdmParentEntityName,
                    ConnectionCondition = entitySch.ConnectionCondition,
                    Parentheses = entitySch.Parentheses,
                    QueryGroup = entitySch.QueryGroup,
                    EntityRelations = entitySch.EntityRelations
                };

                foreach (var exp in entitySch.Expressions)
                {
                    newEntity.Expressions.Add(new EntitySearchExpression(exp.Name, exp.Value) { Excluded = exp.Excluded });
                }
                list.Add(newEntity);
            }
            /////////////////////////

            ReplaceFieldToFilterDataKeyByRef(list, entityType, adjustConditions, true, updateFilterDataKey);

            return list;
        }

        public static string FilterExpressionFields(string serializedEntitySearch, string entityFrom, string entityTo, params string[] fields)
        {
            return FilterExpressionFields(serializedEntitySearch, entityFrom, entityTo, 0, fields);
        }

        public static string FilterExpressionFields(string serializedEntitySearch, string entityFrom, string entityTo, int paramSuffix, params string[] fields)
        {
            if (serializedEntitySearch.IsNullOrEmpty())
                return serializedEntitySearch;

            List<EntitySearch> entitySearchList = SerializationManager<List<EntitySearch>>.StringToObject(serializedEntitySearch);

            foreach (var entitySearch in entitySearchList.Where(e => e.EntityName.IsNullOrEmpty() || e.EntityName == entityFrom).ToArray())
            {
                entitySearch.EntityName = entityTo;
                entitySearch.EdmEntityName = String.Empty;

                //Remove not available expressions
                foreach (var expField in entitySearch.Expressions.Where(e => e.Name == "Field" && !e.Value.ToString().InList(fields.Select(f => f.Left("#")).ToArray())).ToArray())
                {
                    RemoveEntitySearchExpressionField(entitySearch.Expressions, expField);
                }

                //Remove empty expressions
                RemoveEmptyEntitySearchExpression(entitySearch.Expressions);

                //Adjust field name
                foreach (string field in fields.Where(e => e.Left("#") != e.Right("#")))
                {
                    foreach (var expField in entitySearch.Expressions.Where(e => e.Name == "Field" && e.Value.ToString() == field.Left("#")).ToArray())
                    {
                        expField.Name = field.Right("#");
                    }
                }
            }

            //Adjust params suffix
            if (!paramSuffix.IsNullOrEmpty())
            {
                foreach (var search in entitySearchList)
                {
                    search.ParamSuffix = "_" + paramSuffix.ToString();
                }
            }

            return SerializationManager<List<EntitySearch>>.ObjectToString(entitySearchList);
        }

        private static void RemoveEntitySearchExpressionField(List<EntitySearchExpression> expresions, EntitySearchExpression field)
        {
            if (field == null || expresions.Count == 0)
                return;

            EntitySearchExpression expCondition = null, expOperator = null, expValue = null;
            int idxField = expresions.IndexOf(field);
            if (idxField >= 0)
            {
                if ((idxField - 1) >= 0 && expresions[idxField - 1].Name == "Condition" && expresions[idxField - 1].Value is string && !expresions[idxField - 1].Value.ToString().InList("(", ")"))
                    expCondition = expresions[idxField - 1];
                if ((idxField + 1) >= 0 && (idxField + 1) < expresions.Count && expresions[idxField + 1].Name == "Operator")
                    expOperator = expresions[idxField + 1];
                if ((idxField + 2) >= 0 && (idxField + 2) < expresions.Count && expresions[idxField + 2].Name == "Value")
                    expValue = expresions[idxField + 2];

                expresions.Remove(field);
                if (expCondition != null)
                    expresions.Remove(expCondition);
                if (expOperator != null)
                    expresions.Remove(expOperator);
                if (expValue != null)
                    expresions.Remove(expValue);
            }
        }

        private static void RemoveEmptyEntitySearchExpression(List<EntitySearchExpression> expresions)
        {
            if (expresions.Count == 0)
                return;

            EntitySearchExpression openParent;
            EntitySearchExpression[] openParentheses = expresions.Where(e => e.Name == "Condition" && e.Value is string && e.Value.ToString() == "(").ToArray();
            EntitySearchExpression expCondition = null, closeParent = null;

            for (int idxExp = openParentheses.Length - 1; idxExp >= 0; idxExp--)
            {
                openParent = openParentheses[idxExp];
                int idxOpenParent = expresions.IndexOf(openParent);
                if (idxOpenParent >= 0)
                {
                    closeParent = expCondition = null;
                    if ((idxOpenParent - 1) >= 0 && expresions[idxOpenParent - 1].Name == "Condition" && expresions[idxOpenParent - 1].Value is string && !expresions[idxOpenParent - 1].Value.ToString().InList("(", ")"))
                        expCondition = expresions[idxOpenParent - 1];
                    if ((idxOpenParent + 1) >= 0 && (idxOpenParent + 1) < expresions.Count && expresions[idxOpenParent + 1].Name == "Condition" && expresions[idxOpenParent + 1].Value is string && expresions[idxOpenParent + 1].Value.ToString() == ")")
                        closeParent = expresions[idxOpenParent + 1];

                    if (closeParent != null)
                    {
                        if (expCondition != null)
                            expresions.Remove(expCondition);
                        expresions.Remove(openParent);
                        expresions.Remove(closeParent);
                    }

                }
            }

            //Remove inconsistent conditions
            int idxCondition;
            foreach (var condition in expresions.Where(e => e.Name == "Condition" && e.Value is string && e.Value.ToString().InList("&&", "||")).ToArray())
            {
                idxCondition = expresions.IndexOf(condition);
                if (idxCondition == 0)
                    expresions.RemoveAt(idxCondition);
                else if (idxCondition > 0 && expresions[idxCondition - 1].Value is string && expresions[idxCondition - 1].Value.ToString() == "(")
                {
                    expresions.RemoveAt(idxCondition);
                }
            }
        }

        private static Type GetInternalType(Type topType, string internalTypeName)
        {
            Type innerType = null;
            if (topType.Name == internalTypeName)
                innerType = topType;
            else
            {
                foreach (PropertyInfo detailMember in topType.GetProperties().Where(e => e.PropertyType.Name.InList("EntityCollection`1", "IEnumerable`1")))
                {
                    var customAttributes = detailMember.GetCustomAttributes(typeof(CompositionAttribute), false);
                    if (customAttributes != null && customAttributes.Count() > 0)
                    {
                        innerType = GetInternalType(detailMember.PropertyType.GetElement(), internalTypeName);
                        if (innerType != null)
                            break;
                    }
                }
            }
            return innerType;
        }

        private List<Type> GetInternalTypes(Type topType)
        {
            List<Type> innerTypes = new List<Type>() { topType };
            foreach (System.Reflection.PropertyInfo detailMember in topType.GetProperties().Where(e => e.PropertyType.Name.InList("EntityCollection`1", "IEnumerable`1")))
            {
                var customAttributes = detailMember.GetCustomAttributes(typeof(CompositionAttribute), false);
                if (customAttributes != null && customAttributes.Count() > 0)
                {
                    innerTypes.AddRange(GetInternalTypes(detailMember.PropertyType.GetElement()));
                }
            }
            return innerTypes;
        }

        /// <summary>
        /// Read object to make a query by example.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private static void ReplaceFieldToFilterDataKeyByRef(List<EntitySearch> list, Type entityType, bool adjustConditions = true, bool isParent = true, bool updateFilterDataKey = true)
        {
            //For compatibility with old version (Adjusting Conditions)
            if (adjustConditions)
            {
                int idxCnd;
                var lvConditionList = list.Where(e => e.EntityName.InList("OrEntityCondition", "AndEntityCondition")).ToList();
                if (lvConditionList.Count() > 0)
                {
                    foreach (var cntItem in lvConditionList)
                    {
                        idxCnd = list.IndexOf(cntItem);
                        if (idxCnd < list.Count - 1)
                            list[idxCnd + 1].ConnectionCondition = (cntItem.EntityName == "OrEntityCondition" ? "||" : "&&");
                    }
                    //Remove all conditions
                    foreach (var cndItem in lvConditionList)
                        list.Remove(cndItem);
                }
            }
            ///////////////////////////////////

            //Adjust Metadata
            var entityList = list.Where(e => e.EntityName == entityType.Name).ToList();
            if (entityList.Count > 0)
            {
                foreach (EntitySearch entitysearch in entityList)
                {
                    System.Reflection.PropertyInfo member;

                    if (entitysearch.EdmEntityName.IsNullOrEmpty())
                        entitysearch.EdmEntityName = ObjectExtension.GetFunctionalPointOfType(entityType, "EdmEntityName");

                    if (entitysearch.EntityRelations.IsNullOrEmpty())
                        entitysearch.EntityRelations = GetEntityRelations(entityType);


                    if (isParent)
                    {
                        entitysearch.EdmParentEntityName = String.Empty;
                        entitysearch.SubQueryInfo = String.Empty;
                    }
                    else
                    {
                        if (entitysearch.EdmParentEntityName.IsNullOrEmpty())
                            entitysearch.EdmParentEntityName = ObjectExtension.GetFunctionalPointOfType(entityType, "EdmParentEntityName");
                        if (entitysearch.SubQueryInfo.IsNullOrEmpty())
                            entitysearch.SubQueryInfo = ObjectExtension.GetFunctionalPointOfType(entityType, "SubQueryInfo");
                    }

                    if (updateFilterDataKey)
                    {
                        foreach (var expr in entitysearch.Expressions.Where(e => e.Name == "Field"))
                        {
                            member = entityType.GetProperties().Where(e => e.Name == expr.Value.ToString()).FirstOrDefault();
                            if (!member.IsNull())
                            {
                                var fPoint = (ObjectExtension.GetPropertyOfAttributeType(member, typeof(FunctionalPoint), "FunctionName") as string);
                                if (!fPoint.IsNullOrEmpty() && !fPoint.Extract("FilterDataKey[", "]").IsNullOrEmpty())
                                    expr.Value = fPoint.Extract("FilterDataKey[", "]");
                            }

                        }
                    }
                }
            }
            else ///For compatibility with new version
            {
                EntitySearch currentDTO = new EntitySearch(entityType.Name)
                {
                    SubQueryInfo = ObjectExtension.GetFunctionalPointOfType(entityType, "SubQueryInfo"),
                    EdmEntityName = ObjectExtension.GetFunctionalPointOfType(entityType, "EdmEntityName"),
                    EdmParentEntityName = ObjectExtension.GetFunctionalPointOfType(entityType, "EdmParentEntityName")
                };
                list.Add(currentDTO);
            }


            //Sub-Types Analisys
            foreach (System.Reflection.PropertyInfo detailMember in entityType.GetProperties().Where(e => e.PropertyType.Name.InList("EntityCollection`1", "IEnumerable`1")))
            {
                var customAttributes = detailMember.GetCustomAttributes(typeof(CompositionAttribute), false);
                if (customAttributes != null && customAttributes.Count() > 0)
                    ReplaceFieldToFilterDataKeyByRef(list, detailMember.PropertyType.GetElement(), false, false, updateFilterDataKey);
            }

        }

        public static string GetEdmEntityName(Type entityType)
        {
            return ObjectExtension.GetFunctionalPointOfType(entityType, "EdmEntityName");
        }

        public static string GetEntityRelations(Type entityType)
        {
            return ObjectExtension.GetFunctionalPointOfType(entityType, "EntityRelations") ?? "";
        }

        public static Dictionary<string, string> GetEdmEntityNames(Type entityType)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            string edmEntityName = ObjectExtension.GetFunctionalPointOfType(entityType, "EdmEntityName");
            if (!edmEntityName.IsNullOrEmpty())
                result.Add(entityType.Name, edmEntityName);

            //Sub-Types Analisys
            foreach (System.Reflection.PropertyInfo detailMember in entityType.GetProperties().Where(e => e.PropertyType.Name.InList("EntityCollection`1", "IEnumerable`1")))
            {
                var customAttributes = detailMember.GetCustomAttributes(typeof(CompositionAttribute), false);
                if (customAttributes != null && customAttributes.Count() > 0)
                {
                    foreach (var dict in GetEdmEntityNames(detailMember.PropertyType.GetElement()))
                        result.Add(dict.Key, dict.Value);
                }
            }

            return result;
        }

        public object GetExpressionValue(string propertyName, int ocurrences = 1)
        {
            object value = null;
            int counter = 0;
            if (this.Expressions.Count > 0)
            {
                List<EntitySearchExpression> field = this.Expressions.Where(e => e.Name == "Field" && e.Value.ToString() == propertyName).ToList();
                if (field.Count > 0)
                {
                    foreach (EntitySearchExpression expression in field)
                    {
                        counter++;
                        int valuePosition = this.Expressions.IndexOf(expression) + 2;
                        if (valuePosition < this.Expressions.Count && counter == ocurrences)
                        {
                            value = this.Expressions[valuePosition].Value;
                            break;
                        }
                    }
                }
            }
            return value;
        }

        /// <summary>
        /// Read object to make a query by example.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static List<EntitySearch> ReadQueryFromEntityObject(object entity)
        {
            return ReadQueryFromEntityObject(entity, false);
        }

        public static List<EntitySearch> ReadQueryFromEntityObject(object entity, bool byFilterDataKey)
        {
            return ReadQueryFromEntityObject(entity, byFilterDataKey, String.Empty);
        }

        public static EntitySearch GetEntitySearchHeader(Type entityType)
        {
            string entityTypeName = entityType.Name;
            EntitySearch entitysearch = new EntitySearch(entityTypeName)
            {
                SubQueryInfo = ObjectExtension.GetFunctionalPointOfType(entityType, "SubQueryInfo"),
                EdmEntityName = ObjectExtension.GetFunctionalPointOfType(entityType, "EdmEntityName"),
                EdmParentEntityName = ObjectExtension.GetFunctionalPointOfType(entityType, "EdmParentEntityName"),
                BaseEntityNames = string.Empty
            };

            Type baseType = entityType.GetTypeInfo().BaseType;
            while (baseType != null && baseType.Name != "Entity")
            {
                entitysearch.BaseEntityNames += (entitysearch.BaseEntityNames.IsNullOrEmpty() ? string.Empty : ",") + string.Format("[{0}]", baseType.Name);
                baseType = baseType.GetTypeInfo().BaseType;
            }

            return entitysearch;
        }


        public static string GetSpecificFunctionalPoint(Type type, string propertyName, string functionName)
        {
            System.Reflection.PropertyInfo member = type.GetProperty(propertyName);
            if (member != null)
            {
                string fPoint = (ObjectExtension.GetPropertyOfAttributeType(member, typeof(FunctionalPoint), "FunctionName") as string);
                if (fPoint != null)
                    return fPoint.Extract(functionName + "[", "]");
                else return String.Empty;
            }
            else
                return String.Empty;
        }

        public static string GetFilterDataKeyByProperty(Type entityType, string entityName, string propertyName)
        {
            string result = String.Empty;
            if (entityType.Name == entityName)
                return GetSpecificFunctionalPoint(entityType, propertyName, "FilterDataKey");

            //Sub-Types Analisys
            foreach (System.Reflection.PropertyInfo detailMember in entityType.GetProperties().Where(e => e.PropertyType.Name.InList("EntityCollection`1", "IEnumerable`1")))
            {
                var customAttributes = detailMember.GetCustomAttributes(typeof(CompositionAttribute), false);
                if (customAttributes != null && customAttributes.Count() > 0)
                {
                    result = GetFilterDataKeyByProperty(detailMember.PropertyType.GetElement(), entityName, propertyName);
                    if (!result.IsNullOrEmpty())
                        break;
                }
            }

            return result;
        }


        /// <summary>
        /// Read object to make a query by example.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static List<EntitySearch> ReadQueryFromEntityObject(object entity, bool byFilterDataKey, string propertyList)
        {
            List<EntitySearch> result = new List<EntitySearch>();
            System.Collections.IEnumerable details;
            string entityTypeName = entity.GetType().Name;
            EntitySearch currentDTO = GetEntitySearchHeader(entity.GetType());
            result.Add(currentDTO);
            object enteredValue;
            string fieldKey, fPoint;
            bool excludedField;
            string[] propList = (propertyList.IsNullOrEmpty() ? new string[] { } : propertyList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));

            foreach (System.Reflection.PropertyInfo member in entity.GetType().GetProperties())
            {
                if (member.PropertyType.Name.InList("EntityCollection`1", "IEnumerable`1"))
                {
                    var customAttributes = member.GetCustomAttributes(typeof(CompositionAttribute), false);
                    if (customAttributes != null && customAttributes.Count() > 0)
                    {
                        details = entity.GetPropertyValue(member.Name) as System.Collections.IEnumerable;
                        if (details != null)
                        {
                            foreach (var element in details)
                            {
                                result.AddRange(ReadQueryFromEntityObject(element, byFilterDataKey, propertyList));
                            }
                        }
                    }
                }
                else
                {
                    var customAttributes = member.GetCustomAttributes(typeof(DataMemberAttribute), false);
                    if (customAttributes != null && customAttributes.Count() > 0)
                    {
                        enteredValue = entity.GetPropertyValue(member.Name);
                        if (!enteredValue.IsNullOrEmpty())
                        {
                            fieldKey = member.Name;
                            excludedField = false;

                            //Check Functional Point
                            fPoint = (ObjectExtension.GetPropertyOfAttributeType(member, typeof(FunctionalPoint), "FunctionName") as string);
                            if (fPoint != null)
                            {
                                excludedField = fPoint.Extract("ExcludedAsFilter[", "]") == "true";
                                if (byFilterDataKey)
                                {
                                    fieldKey = fPoint.Extract("FilterDataKey[", "]");
                                    if (fieldKey.IsNullOrEmpty())
                                        fieldKey = member.Name;
                                }
                            }

                            if (fPoint != null || propList.Contains(entityTypeName + "." + member.Name))
                            {
                                if (currentDTO.Expressions.Where(e => !e.Excluded).Count() > 0)
                                    currentDTO.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "&&") { Excluded = excludedField });
                                currentDTO.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, fieldKey) { Excluded = excludedField });
                                currentDTO.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, ((enteredValue is string && ((string)enteredValue).Contains("%")) ? "Like" : "==")) { Excluded = excludedField });
                                currentDTO.Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, enteredValue) { Excluded = excludedField });
                            }

                        }
                    }
                }
            }

            return result;
        }

        public string GetInnerCloseParentheses(List<EntitySearch> list)
        {
            return GetInnerCloseParentheses(list, new List<EntitySearch>());
        }

        private string GetInnerCloseParentheses(List<EntitySearch> list, List<EntitySearch> analyzedList)
        {
            if (this.EdmEntityName.IsNullOrEmpty())
                return "";

            if (this.Parentheses == ")")
                return this.Parentheses;

            //Add this reference into the list
            analyzedList.Add(this);

            foreach (var subSearch in list.Where(e => !analyzedList.Contains(e) && e.EdmParentEntityName == this.EdmEntityName && e.QueryGroup == this.QueryGroup))
            {
                string closeParentheses = subSearch.GetInnerCloseParentheses(list, analyzedList);
                if (!closeParentheses.IsNullOrEmpty())
                    return closeParentheses;
            }

            return "";
        }

        public bool HasFilters(List<EntitySearch> list)
        {
            return HasFilters(list, new List<EntitySearch>());
        }

        public bool HasFilters(List<EntitySearch> list, List<EntitySearch> analyzedList)
        {
            if (this.EdmEntityName.IsNullOrEmpty())
                return false;

            if (this.Expressions.Where(e => !e.Excluded).Count() > 0)
                return true;

            //Add this reference into the list
            analyzedList.Add(this);

            foreach (var subSearch in list.Where(e => !analyzedList.Contains(e) && e.EdmParentEntityName == this.EdmEntityName && e.QueryGroup == this.QueryGroup))
            {
                if (subSearch.HasFilters(list, analyzedList))
                    return true;
            }

            return false;
        }

        public string GetDescription(List<PropertyDefinitions> properties = null)
        {
            StringBuilder sbQuery = new StringBuilder();
            string currentCondition = string.Empty;

            if (this.EntityName.Contains("Condition"))
                currentCondition = string.Format(" {0} ", this.EntityName == "AndEntityCondition" ? "E".Translate() : "OU".Translate());
            else
            {
                if (expressions.Count() > 0)
                {
                    if (currentCondition != string.Empty)
                    {
                        sbQuery.Append(currentCondition);
                        currentCondition = string.Empty;
                    }

                    sbQuery.Append(this.EntityName);

                    sbQuery.Append(" " + "Onde".Translate());

                    this.Expressions.ForEach((expression) =>
                    {
                        sbQuery.Append(" ");
                        string information = string.Empty;
                        switch (expression.Name.ToLower())
                        {
                            case "condition":
                                information = GetCondictionDescription(expression.Value);
                                break;
                            case "operator":
                                information = GetOperatorDescription(expression.Value.ToString());
                                break;
                            case "field":
                                PropertyDefinitions propDef = null;
                                if (properties != null && properties.Count > 0)
                                    propDef = properties.FirstOrDefault(p => p.Name.Equals(expression.Value));
                                information = (propDef != null ? propDef.Caption : ("." + expression.Value.ToString()).Right(".").Proper());
                                break;
                            default:
                                if (expression.Value.IsNull())
                                    information = "null".Translate();
                                else
                                    if (expression.Value.ToString() == string.Empty)
                                    information = "Vazio".Translate();
                                else
                                    information = expression.Value.ToString();
                                break;
                        }

                        sbQuery.Append(information);
                    });
                }
            }
            return sbQuery.ToString();
        }

        public static string GetDescription(List<EntitySearch> entitiesList, object dataAnalysis, Dictionary<string, Dictionary<string, string>> domains)
        {
            StringBuilder sbQuery = new StringBuilder();
            string currentCondition = string.Empty;
            string lastOperation = string.Empty;

            Type type = (dataAnalysis is Type ? (Type)dataAnalysis : dataAnalysis.GetType());

            foreach (EntitySearch entity in entitiesList.Where(e => e.EntityName != "LinqValidProperties"))
            {
                if (entity.Parentheses == "(")
                    sbQuery.Append(entity.Parentheses);

                if (entity.EntityName.Contains("Condition"))
                {
                    currentCondition = (string.Format(" {0} ", entity.EntityName == "AndEntityCondition" ? "E".Translate() : "OU".Translate()));
                    if (entitiesList.Count() == 1)
                        sbQuery.Append(currentCondition);
                }
                else
                {
                    if (!entity.expressions.IsNull() && entity.expressions.Count() > 0)
                    {
                        if (lastOperation == "filter")
                        {
                            sbQuery.Append(currentCondition == string.Empty ? string.Format(" {0} ", "E".Translate()) : currentCondition);
                            sbQuery.AppendLine();
                            lastOperation = "condition";
                        }

                        lastOperation = "filter";

                        //string entityName = ObjectExtension.GetFunctionalPointOfType(type, "DisplayName");
                        //sbQuery.Append(entityName.IsNullOrEmpty() ? entity.EntityName : entityName);
                        //sbQuery.Append(" " + "Onde".Translate());

                        string initialOperator = string.Empty;
                        string fieldName = null;

                        entity.Expressions.ForEach((expression) =>
                        {
                            sbQuery.Append(" ");
                            string information = string.Empty;
                            switch (expression.Name.ToLower())
                            {
                                case "predefinedfilter":
                                    string field, value, finalOperator = string.Empty, predefinedValue = string.Empty;

                                    finalOperator = expression.Value.ToString().Extract("InitialOperator[", "]") + expression.Value.ToString().Extract("Operator[", "]");
                                    field = "[" + expression.Value.ToString().Extract("Field[", "]") + "]";
                                    value = expression.Value.ToString().Extract("Value[", "]");
                                    predefinedValue = expression.Value.ToString().Extract("PredefinedValue[", "]");
                                    information = string.Format("{0} {1} {2}", entity.GetFieldCaption(field, type), entity.GetOperatorDescription(finalOperator), PredefinedFilter.GetPredefinedValueDescription(predefinedValue, (value.IsNullOrEmpty() ? Convert.ToInt32(null) : Convert.ToInt32(value))));

                                    break;

                                case "condition":
                                    information = entity.GetCondictionDescription(expression.Value);
                                    break;

                                case "operator":
                                    information = expression.Value.ToString().Replace("==", "="); //entity.GetOperatorDescription(expression.Value.ToString());
                                    if (expression.Value.ToString() == "!")
                                    {
                                        initialOperator = information;
                                        information = string.Empty;
                                    }
                                    else if (initialOperator != string.Empty)
                                    {
                                        information = string.Format("{0} {1}", initialOperator, information);
                                        initialOperator = string.Empty;
                                    }
                                    break;

                                case "field":
                                    fieldName = expression.Value.ToString();
                                    information = "[" + entity.GetFieldCaption(fieldName, type) + "]";
                                    break;

                                default:

                                    if (expression.Value.IsNull())
                                        information = "null".Translate();
                                    else
                                    {
                                        if (expression.Value.ToString() == string.Empty)
                                            information = "Vazio".Translate();
                                        else
                                        {
                                            PropertyDefinitions fPoint = (domains == null ? null : ObjectExtension.GetFunctionalPoints(type, fieldName).FirstOrDefault());
                                            string fieldValue = expression.Value.ToString();
                                            if (fPoint != null && !fPoint.Domain.IsNullOrEmpty() && domains.ContainsKey(fPoint.Domain) && domains[fPoint.Domain].ContainsKey(fieldValue))
                                                information = domains[fPoint.Domain][fieldValue];
                                            else
                                                information = fieldValue;
                                        }
                                    }
                                    information = "'" + information + "'";
                                    break;
                            }
                            sbQuery.Append(information);
                        });
                    }
                }

                if (entity.Parentheses == ")")
                    sbQuery.Append(entity.Parentheses);

            }
            return sbQuery.ToString();
        }

        private string GetFieldCaption(string fieldName, Type objectType)
        {
            var types = GetInternalTypes(objectType);
            List<PropertyDefinitions> definitions = new List<PropertyDefinitions>();
            foreach (var t in types)
            {
                definitions = ObjectExtension.GetFunctionalPoints(t, true).Where(i => i.FilterDataKey == fieldName || i.Name == fieldName).ToList();
                if (definitions.Count() > 0 && !definitions.First().Caption.IsNullOrEmpty())
                {
                    return definitions.First().Caption;
                }
            }

            return ("." + fieldName).Right(".").Replace("_", " ").Proper();
        }

        private string GetCondictionDescription(object value)
        {
            string ret = value.ToString();
            switch (ret)
            {
                case "&&":
                    ret = "E".Translate();
                    break;
                case "||":
                    ret = "Ou".Translate();
                    break;
                default:
                    break;
            }
            return ret;
        }

        private string GetOperatorDescription(string value)
        {
            string ret = value.ToLower();
            if (!ret.Equals("like"))
            {
                switch (ret)
                {
                    case "!":
                        ret = "não".Translate();
                        break;
                    case "==":
                        ret = "igual a".Translate();
                        break;
                    case "!=":
                        ret = "diferente de".Translate();
                        break;
                    case ">":
                        ret = "maior que".Translate();
                        break;
                    case ">=":
                        ret = "maior ou igual a".Translate();
                        break;
                    case "<":
                        ret = "menor que".Translate();
                        break;
                    case "<=":
                        ret = "menor ou igual".Translate();
                        break;
                    case "startswith":
                        ret = "começa com".Translate();
                        break;
                    case "endswith":
                        ret = "terminando com".Translate();
                        break;
                    case "in":
                        ret = "está contido em".Translate();
                        break;
                    case "contains":
                        ret = "contém".Translate();
                        break;
                    default:
                        break;
                }
            }
            return ret;
        }

        private static List<EntitySearchExpression> AddDateExpression(string field, DateTime initialDate, DateTime finalDate, string finalOperator)
        {
            List<EntitySearchExpression> expressions = new List<EntitySearchExpression>();

            expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "("));
            expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, field));

            switch (finalOperator)
            {
                case "==":
                    if (finalDate == initialDate)
                        expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "=="));
                    else
                        expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, ">="));

                    expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, initialDate));

                    if (finalDate != initialDate)
                    {
                        expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "&&"));
                        expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, field));
                        expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "<="));
                        expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, finalDate));
                    }
                    break;

                case "!=":
                    if (finalDate == initialDate)
                        expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "!="));
                    else
                        expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, "<"));

                    expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, initialDate));

                    if (finalDate != initialDate)
                    {
                        expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, "&&"));
                        expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, field));
                        expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, ">"));
                        expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, finalDate));
                    }
                    break;

                case ">":
                    expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, finalOperator));
                    expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, finalDate));
                    break;

                case ">=":
                    expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, finalOperator));
                    expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, initialDate));
                    break;

                case "<":
                    expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, finalOperator));
                    expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, initialDate));
                    break;

                case "<=":
                    expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, finalOperator));
                    expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, finalDate));
                    break;

                default:
                    break;
            }
            expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Condition, ")"));
            return expressions;
        }

        public static List<EntitySearch> EvaluatePredefinedFilters(List<EntitySearch> originalSearch)
        {
            DateTime currentDate = DateTime.Today;

            List<EntitySearch> predefinedSearches = originalSearch.Where(i => i.expressions.Any(it => it.Name == "Field" && it.Value.Equals("PredefinedFilter"))).ToList();

            if (predefinedSearches.Count() == 0)
                return originalSearch;

            foreach (EntitySearch search in predefinedSearches)
            {
                List<EntitySearchExpression> predefinedExpression = search.expressions.Where(it => it.Name == "Field" && it.Value.Equals("PredefinedFilter")).ToList();
                foreach (EntitySearchExpression expField in predefinedExpression)
                {
                    var indexOfField = search.Expressions.IndexOf(expField);
                    var expOperator = search.Expressions[indexOfField + 1];
                    var expValue = search.Expressions[indexOfField + 2];
                    DateTime startDate = currentDate;
                    DateTime endDate = currentDate;
                    string field, value, finalOperator = string.Empty, initialOperator = string.Empty, predefinedValue = string.Empty;
                    bool excludedField, isDateTime = true;
                    List<EntitySearchExpression> expressions = new List<EntitySearchExpression>();

                    finalOperator = expOperator.Value.ToString();
                    field = expValue.Value.ToString().Extract("Field[", "]");
                    value = expValue.Value.ToString().Extract("]Value[", "]");
                    initialOperator = expValue.Value.ToString().Extract("InitialOperator[", "]");
                    predefinedValue = expValue.Value.ToString().Extract("PredefinedValue[", "]");
                    excludedField = expValue.Value.ToString().Extract("ExcludedField[", "]").IsNullOrEmpty() ? false : Convert.ToBoolean(expValue.Value.ToString().Extract("ExcludedField[", "]"));

                    if (!initialOperator.IsNullOrEmpty())
                        finalOperator = initialOperator + finalOperator;

                    #region : predefined

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
                            startDate = currentDate.AddDays(Convert.ToDouble(value) * -1);
                            endDate = startDate.AddDays(1).AddSeconds(-1); ;
                            break;

                        case "XWeeks":
                            startDate = currentDate.AddDays(-Convert.ToInt16(currentDate.DayOfWeek));
                            startDate = startDate.AddDays(Convert.ToDouble(value) * -7);
                            endDate = startDate.AddDays(7).AddSeconds(-1);
                            break;

                        case "XMonths":
                            startDate = currentDate.AddMonths(Convert.ToInt16(value) * -1);
                            startDate = DateTime.ParseExact(string.Format("01/{0}/{1} 00:00:00", startDate.Month.ToString().PadLeft(2, '0'), startDate.Year), "dd/MM/yyyy HH:mm:ss", null);
                            endDate = startDate.AddMonths(1).AddSeconds(-1);
                            break;

                        case "XYears":
                            startDate = DateTime.ParseExact(string.Format("01/01/{0} 00:00:00", currentDate.AddYears(Convert.ToInt16(value) * -1).Year), "dd/MM/yyyy HH:mm:ss", null);
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




                        default:
                            break;
                    }

                    #endregion

                    EntitySearch entitySearch = originalSearch[originalSearch.IndexOf(search)];

                    if (isDateTime)
                        entitySearch.expressions.InsertRange(entitySearch.expressions.IndexOf(expField), AddDateExpression(field, startDate, endDate, finalOperator));
                    else
                        entitySearch.expressions.InsertRange(entitySearch.expressions.IndexOf(expField), expressions);

                    entitySearch.expressions.RemoveAt(entitySearch.expressions.IndexOf(expField));
                    entitySearch.expressions.RemoveAt(entitySearch.expressions.IndexOf(expOperator));
                    entitySearch.expressions.RemoveAt(entitySearch.expressions.IndexOf(expValue));
                }
            }
            return originalSearch;
        }
    }


    [DataContract]
    public class EntitySearchExpression
    {
        public EntitySearchExpression(EntitySearchExpressionName name, object value)
            : this(name.ToString(), value)
        {
        }

        public EntitySearchExpression(string name, object value)
        {
            this.Name = name;
            this.Value = value;
        }

        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public object Value { get; set; }
        [DataMember]
        public bool Excluded { get; set; }

        public override string ToString()
        {
            return string.Format("[{0}]=[{1}]", Name, Value);
        }
    }
}

using Linx.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace Linx.OlapProxy.Service.Helpers
{
    internal class JEntitySearchHelper
    {
        private MDXQueryFilterItem _mdxQueryFilterItem;

        public JEntitySearchHelper()
        {
            _mdxQueryFilterItem = new MDXQueryFilterItem(null);
        }

        internal List<string> ParseJEntitySearch(XmlNode mdxCommand, string cubeName, string jEntitySearch)
        {
            var filters = new List<string>();

            if (!string.IsNullOrEmpty(jEntitySearch))
            {
                var expressions = jEntitySearch.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var item in expressions)
                {
                    if (item.Length > 2)
                    {
                        var values = item.Split(new char[] { '#' });

                        var attribute = GetDimensionAttribute(values);
                        var attributeValue = GetDimensionAttributeValue(values);
                        var conditionalOperator = GetConditionalOperator(values);
                        var command = this.GetCommand(attribute, attributeValue, conditionalOperator);

                        if (!attribute.StartsWith("[Measures]"))
                        {
                            if (mdxCommand.InnerText.Contains(attribute))
                            {
                                var subSelect = " ( SELECT { " + attribute + ".allmembers } ON COLUMNS  FROM ( SELECT {  " + command + "} ON COLUMNS FROM " + cubeName + " )) ";

                                mdxCommand.InnerText = mdxCommand.InnerText.Replace(cubeName, subSelect);
                            }
                            else
                                filters.Add(command);
                        }
                    }
                }
            }

            return filters;
        }

        private string GetCommand(string attribute, string attributeValue, string conditionalOperator)
        {
            var command = string.Empty;

            var currentMDXField = new MDXField(attribute, attribute, attribute.Contains("[Measures]"));

            switch (conditionalOperator)
            {
                case ">":
                    command = _mdxQueryFilterItem.GreaterThan(currentMDXField, attributeValue).ToString(); break;
                case ">=":
                    command = _mdxQueryFilterItem.GreaterThanEq(currentMDXField, attributeValue).ToString(); break;
                case "In":
                    command = _mdxQueryFilterItem.In(currentMDXField, attributeValue).ToString(); break;
                case "!In":
                    command = _mdxQueryFilterItem.NotIn(currentMDXField, attributeValue).ToString(); break;
                case "==":
                    command = _mdxQueryFilterItem.Eq(currentMDXField, attributeValue).ToString(); break;
                case "<":
                    command = _mdxQueryFilterItem.LessThan(currentMDXField, attributeValue).ToString(); break;
                case "<=":
                    command = _mdxQueryFilterItem.LessThanEq(currentMDXField, attributeValue).ToString(); break;
                case "Like":
                    command = _mdxQueryFilterItem.Like(currentMDXField, attributeValue, false).ToString(); break;
                case "!Like":
                    command = _mdxQueryFilterItem.Like(currentMDXField, attributeValue, true).ToString(); break;
                case "!=":
                    command = _mdxQueryFilterItem.NotEq(currentMDXField, attributeValue).ToString(); break;
            }

            return command;
        }

        private string GetConditionalOperator(string[] expression)
        {
            return expression[1];
        }

        private string GetDimensionAttribute(string[] expression)
        {
            var startIndex = expression[0].IndexOf('[');
            var lenght = ((expression[0].LastIndexOf(']') - startIndex) + 1);

            var attributeName = expression[0].Substring(startIndex, lenght);

            if (attributeName.Count(x => x.Equals('.')) == 2)
                attributeName = attributeName.Remove(attributeName.LastIndexOf('.'));

            return attributeName;
        }

        private string GetDimensionAttributeValue(string[] expression)
        {
            var lenght = ((expression[2].Contains("}") ? expression[2].IndexOf("}") : expression[2].Length) - 1);

            return expression[2].Substring(1, lenght);
        }
    }

    internal class JEntitySearchFilterItem
    {
        public string Operator { get; set; }

        public List<string> Values { get; set; }

        public Linx.Tools.MDXField MDXField { get; set; }
    }
}
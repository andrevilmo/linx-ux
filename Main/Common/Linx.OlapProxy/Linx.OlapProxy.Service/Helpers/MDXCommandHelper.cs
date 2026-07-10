using Linx.OlapProxy.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace Linx.OlapProxy.Service.Helpers
{
    internal class MDXCommandHelper
    {
        internal string GetCubeName(string mdxCommand)
        {
            var mdx = mdxCommand.ToUpper();
            var startTarget = mdx.Substring(mdx.IndexOf("FROM ["));
            var cubeName = startTarget.Substring(0, startTarget.IndexOf("]") + 1).Replace("FROM", string.Empty);

            return cubeName;
        }

        internal List<string> GetFilters(OlapProxyRequest olapProxyRequest, XmlNode mdxCommand, string cubeName)
        {
            var filters = new List<string>();

            this.FillFilterByDimensions(olapProxyRequest, mdxCommand, filters, cubeName);
            this.FillFilterByMeasures(olapProxyRequest, mdxCommand, filters, cubeName);

            return filters;
        }

        internal void FillFilterByMeasures(OlapProxyRequest olapProxyRequest, XmlNode mdxCommand, List<string> filters, string cubeName)
        {
            var cubeMetadataInfo = Managers.CubeMetadatainfoManager.GetCubeMetadatainfo();

            if (cubeMetadataInfo.MeasuresInfo != null && cubeMetadataInfo.MeasuresInfo.Any())
            {
                foreach (var measure in cubeMetadataInfo.MeasuresInfo)
                {
                    var usedFilter = false;

                    foreach (var filter in filters)
                    {
                        usedFilter = (filter.IndexOf(measure.GroupName) >= 0);
                        if (usedFilter) break;
                    }

                    if (mdxCommand.InnerText.Contains(measure.Name) && !usedFilter &
                        cubeMetadataInfo.DimensionsInfo.Any(x => x.DimensionName == measure.GroupName))
                    {
                        var targetDimension = cubeMetadataInfo.DimensionsInfo.FirstOrDefault(x => x.DimensionName == measure.GroupName);

                        targetDimension.Fields.ForEach(field =>
                        {
                            if (field.KeyType == Enums.ParameterType.EconomicGroupId &&
                                LinxParametersHelper.LinxId == LinxParametersHelper.EconomicGroupId)
                                return;

                            if (field.KeyType != Enums.ParameterType.BrandsId)
                            {
                                var fieldValue = LinxParametersHelper.GetLinxParameter(field.KeyType);

                                if (!string.IsNullOrEmpty(fieldValue))
                                    filters.Add(string.Concat("{ ", field.HierarchyName, ".&[", fieldValue, "] }"));
                            }
                            else
                            {
                                var brandFilters = new List<string>();

                                foreach (var item in olapProxyRequest.CurrentBrandCollection)
                                    brandFilters.Add(string.Format("{0}.&[{1}]", field.HierarchyName, item));

                                if (mdxCommand.InnerText.Contains(field.HierarchyName))
                                {
                                    var indexTarget = mdxCommand.InnerText.IndexOf(field.HierarchyName);
                                    var indexWhere = mdxCommand.InnerText.ToLower().IndexOf("where");

                                    if (indexWhere < 0 || indexTarget < indexWhere)
                                    {
                                        var subSelect = " ( SELECT { " + field.HierarchyName + ".allmembers } ON COLUMNS  FROM ( SELECT {  " + string.Join(", ", brandFilters) + "} ON COLUMNS FROM " + cubeName + " )) ";

                                        mdxCommand.InnerText = mdxCommand.InnerText.Replace(cubeName, subSelect);
                                    }
                                    else if (mdxCommand.InnerText.Contains(field.HierarchyName + ".[All]"))
                                    {
                                        var replecementValue = field.HierarchyName + ".[All]";
                                        mdxCommand.InnerText = mdxCommand.InnerText.Replace(replecementValue, "{ " + string.Join(", ", brandFilters) + " }");
                                    }
                                }
                                else
                                {
                                    filters.Add(string.Concat("{ ", string.Join(", ", brandFilters), " }"));
                                }
                            }
                        });
                    }
                }
            }
        }

        internal void FillFilterByDimensions(OlapProxyRequest olapProxyRequest, XmlNode mdxCommand, List<string> filters, string cubeName)
        {
            var cubeMetadataInfo = Managers.CubeMetadatainfoManager.GetCubeMetadatainfo();

            if (cubeMetadataInfo != null && cubeMetadataInfo.DimensionsInfo.Any())
            {
                foreach (var dimension in cubeMetadataInfo.DimensionsInfo)
                {
                    if (mdxCommand.InnerText.Contains(dimension.DimensionName))
                    {
                        dimension.Fields.ForEach(field =>
                        {
                            if (field.KeyType == Enums.ParameterType.EconomicGroupId &&
                                LinxParametersHelper.LinxId == LinxParametersHelper.EconomicGroupId)
                                return;

                            if (field.KeyType != Enums.ParameterType.BrandsId)
                            {
                                var fieldValue = LinxParametersHelper.GetLinxParameter(field.KeyType);

                                if (!string.IsNullOrEmpty(fieldValue))
                                    filters.Add(string.Concat("{ ", field.HierarchyName, ".&[", fieldValue, "] }"));
                            }
                            else
                            {
                                var brandFilters = new List<string>();

                                foreach (var item in olapProxyRequest.CurrentBrandCollection)
                                    brandFilters.Add(string.Format("{0}.&[{1}]", field.HierarchyName, item));

                                if (mdxCommand.InnerText.Contains(field.HierarchyName))
                                {
                                    var indexTarget = mdxCommand.InnerText.IndexOf(field.HierarchyName);
                                    var indexWhere = mdxCommand.InnerText.ToLower().IndexOf("where");

                                    if (indexWhere < 0 || indexTarget < indexWhere)
                                    {
                                        var subSelect = " ( SELECT { " + field.HierarchyName + ".allmembers } ON COLUMNS  FROM ( SELECT {  " + string.Join(", ", brandFilters) + "} ON COLUMNS FROM " + cubeName + " )) ";

                                        mdxCommand.InnerText = mdxCommand.InnerText.Replace(cubeName, subSelect);
                                    }
                                    else if (mdxCommand.InnerText.Contains(field.HierarchyName + ".[All]"))
                                    {
                                        var replecementValue = field.HierarchyName + ".[All]";
                                        mdxCommand.InnerText = mdxCommand.InnerText.Replace(replecementValue, "{ " + string.Join(", ", brandFilters) + " }");
                                    }
                                }
                                else
                                {
                                    filters.Add(string.Concat("{ ", string.Join(", ", brandFilters), " }"));
                                }
                            }
                        });
                    }
                }
            }
        }

        internal string ParseMDXCommand(string command, List<string> filters, string jEntitySearch)
        {
            var mdxCommand = command;

            if (filters.Any())
            {
                if (command.Contains("where") || command.Contains("WHERE") || command.Contains("Where"))
                {
                    var conditions = string.Format(", {0} )", string.Join(", ", filters));

                    mdxCommand = string.Format("{0} {1}", command.Substring(0, command.LastIndexOf(')')), conditions);
                }
                else
                {
                    var conditions = string.Format(" WHERE ( {0} )", string.Join(", ", filters));

                    var lengthCommand = ((command.LastIndexOf("CELL PROPERTIES VALUE") == -1)
                        ? command.Length : command.LastIndexOf("CELL PROPERTIES VALUE"));

                    mdxCommand = string.Format("{0} {1}", command.Substring(0, lengthCommand), conditions);
                }
            }

            return mdxCommand;
        }

        internal void SetMDXConditions(XmlDocument document, OlapProxyRequest olapProxyRequest)
        {
            var statementNode = document.GetElementsByTagName("Statement");

            if (statementNode != null && statementNode.Count > 0)
            {
                var jEntitySearchHelper = new JEntitySearchHelper();

                foreach (XmlNode item in statementNode)
                {
                    var cubeName = this.GetCubeName(item.InnerText);

                    var filtersByJEntitySearch = jEntitySearchHelper.ParseJEntitySearch(item, cubeName, olapProxyRequest.JEntitySearch);

                    var filters = this.GetFilters(olapProxyRequest, item, cubeName);
                    var allFilters = filters.Concat(filtersByJEntitySearch).ToList();

                    item.InnerXml = document.CreateCDataSection(this.ParseMDXCommand(item.InnerText, allFilters, olapProxyRequest.JEntitySearch)).OuterXml;
                }
            }
        }
    }
}
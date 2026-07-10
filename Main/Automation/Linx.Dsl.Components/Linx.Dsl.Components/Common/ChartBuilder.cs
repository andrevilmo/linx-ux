using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Dsl.Components.Common
{
    public static class IgniteUIChartBuilder
    {
        public static string BuilderJS(ChartDTO config, Linx.Dsl.Components.Enums.LibEnum lib)
        {
            StringBuilder r = new StringBuilder();
            //r.AppendLine("var data = $root.dataView();");
            r.AppendLine();
            r.AppendLine("var innerList = @dataSource;");
            r.AppendLine();

            #region define o TIPO COMPONENTE            
            if (string.Equals(config.ChartType, "piechart", StringComparison.InvariantCultureIgnoreCase) == true)
            {
                r.AppendLine("@chartName.igPieChart({");
            }
            else if (string.Equals(config.ChartType, "doughnutchart", StringComparison.InvariantCultureIgnoreCase) == true)
            {
                r.AppendLine("@chartName.igDoughnutChart({");
            }
            else
            {
                r.AppendLine("@chartName.igDataChart({");
            }
            #endregion

            #region Configuracoes genericas
            r.AppendProperty<object>("dataSource", "innerList");
            r.AppendProperty<string>("width", config.Width, true);
            r.AppendProperty<string>("height", config.Height, true);
            r.AppendProperty<string>("title", config.Title, true);
            r.AppendProperty<string>("subtitle", config.SubTitle, true);

            if (config.Legend.Enabled == true)
            {
                r.AppendProperty<object>("legend", BuildLegend(config, lib).ToString(), true);
            }
            r.Append(config.GenerateDynamicProperties(lib));
            #endregion

            #region Tipo Grafico: PIECHART
            if (string.Equals(config.ChartType, "piechart", StringComparison.InvariantCultureIgnoreCase))
            {
                r.AppendProperty<string>("valueMemberPath", config.ValueMemberPath, true);
                r.AppendProperty<string>("labelMemberPath", config.LabelMemberPath, true);
                r.AppendProperty<string>("labelsPosition", config.LabelsPosition, true);
            }
            #endregion
            #region Tipo Grafico: Stacked
            else if (string.Equals(config.ChartGroup, "Stacked Series", StringComparison.InvariantCultureIgnoreCase))
            {
                r.AppendCollection("axes", IgniteUIChartBuilder.BuildAxes(config.Axes, lib));
                r.AppendCollection("series", IgniteUIChartBuilder.BuildSeriesStacked(config.Series, lib));
            }
            #endregion
            else
            {
                r.AppendCollection("axes", IgniteUIChartBuilder.BuildAxes(config.Axes, lib));
                r.AppendCollection("series", IgniteUIChartBuilder.BuildSeries(config.Series, lib));
            }

            r.AppendLine("});");

            return r.ToString();
        }

        private static StringBuilder BuildLegend(ChartDTO config, Linx.Dsl.Components.Enums.LibEnum lib)
        {
            StringBuilder r = new StringBuilder();
            r.Append("{");
            r.AppendProperty<string>("element", "@chartLegendElement");
            r.AppendProperty<string>("width", config.Legend.Width, true);
            r.AppendProperty<string>("height", config.Legend.Height, true);
            r.AppendProperty<string>("type", config.Legend.Type, true);
            r.Append("}");
            return r;
        }

        private static StringBuilder BuildAxes(List<AxeDTO> axes, Linx.Dsl.Components.Enums.LibEnum lib)
        {
            int current = 0;
            StringBuilder r = new StringBuilder();
            foreach (var i in axes)
            {
                if (current > 0)
                    r.Append(",");

                r.Append("{");   
                r.AppendProperty<string>("name", i.Name);
                r.AppendProperty<string>("type", i.Type, true);
                r.AppendProperty<string>("label", i.Label, true);
                r.AppendProperty<string>("title", i.Title, true);
                r.Append(i.GenerateDynamicProperties(lib));
                r.Append("}");
                current++;
            }

            return r;
        }

        private static StringBuilder BuildSeries(List<SerieDTO> series, Linx.Dsl.Components.Enums.LibEnum lib)
        {
            int current = 0;
            StringBuilder r = new StringBuilder();
            foreach (var i in series)
            {
                if (current > 0)
                    r.Append(",");

                /*
                name: "1995Population",
                type: "line",
                title: "Venda Hora",
                xAxis: "NameAxis",
                yAxis: "PopulationAxis",
                valueMemberPath: "TotalVendaPorDia",
                isTransitionInEnabled: true,
                isHighlightingEnabled: true,
                thickness: 5,
                showTooltip: true
                */

                r.Append("{");
                r.AppendProperty<string>("name", i.Name);
                r.AppendProperty<string>("type", i.Type, true);
                r.AppendProperty<string>("title", i.Title, true);
                r.AppendProperty<string>("xAxis", i.xAxis, true);
                r.AppendProperty<string>("yAxis", i.yAxis, true);
                r.AppendProperty<string>("valueMemberPath", i.ValueMemberPath, true);
                r.AppendProperty<string>("labelMemberPath", i.LabelPath, true);
                r.AppendProperty<bool>("showTooltip", i.Tooltip.ToString().ToLower(), true);
                r.AppendProperty<string>("lowMemberPath", i.LowMemberPath, true);
                r.AppendProperty<string>("highMemberPath", i.HighMemberPath, true);
                r.AppendProperty<string>("radiusMemberPath", i.RadiusMemberPath, true);
                r.AppendProperty<string>("fillMemberPath", i.FillMemberPath, true);
                r.AppendProperty<string>("labelMemberPath", i.LabelMemberPath, true);
                r.AppendProperty<string>("markerType", i.MarkerType, true);
                r.AppendProperty<string>("xMemberPath", i.xMemberPath, true);
                r.AppendProperty<string>("yMemberPath", i.yMemberPath, true);
                r.AppendProperty<string>("displayType", i.DisplayType, true);
                r.AppendProperty<string>("openMemberPath", i.OpenMemberPath, true);
                r.AppendProperty<string>("closeMemberPath", i.CloseMemberPath, true);
                r.AppendProperty<string>("angleAxis", i.AngleAxis, true);
                r.AppendProperty<string>("radiusAxis", i.RadiusAxis, true);
                r.AppendProperty<string>("angleMemberPath", i.AngleMemberPath, true);
                r.AppendProperty<string>("valueAxis", i.ValueAxis, true);

                #region Tipo Serie: BUBBLE
                if (string.Equals(i.Type, "bubble", StringComparison.InvariantCultureIgnoreCase))
                {
                    /*
                        radiusScale: {
                                minimumValue: 2,
                                maximumValue: 12,
                                isLogarithmic: true
                        },
                    */
                    r.AppendLine(",radiusScale: {");
                    r.AppendProperty<int>("minimumValue", i.RadiusScale_MinimumValue.ToString(), true);
                    r.AppendProperty<int>("maximumValue", i.RadiusScale_MaximumValue.ToString(), true);
                    r.AppendProperty<bool>("isLogarithmic", i.RadiusScale_IsLogarithmic.ToString().ToLower(), true);
                    r.AppendLine("}");

                    /*
                        fillScale: {
                            type: "value",
                            brushes: ["red", "orange", "yellow"],
                            minimumValue: 150,
                            maximumValue: 400
                        }                
                    */
                    r.AppendLine(",fillScale: {");
                    r.AppendProperty<string>("type", i.FillScale_Type, true);
                    var brushes = i.FillScale_Brushes.Split(new char[','], StringSplitOptions.RemoveEmptyEntries);
                    if (brushes.Length > 0)
                    {
                        r.Append(",brushes: [");
                        foreach (var b in brushes)
                        {
                            r.AppendFormat("\"{0}\"", b);
                            r.Append(",");
                        }
                        r.Append("]");
                    }

                    r.AppendLine("}");
                }
                #endregion

                r.Append(i.GenerateDynamicProperties(lib));
                r.Append("}");
                current++;
            }

            return r;
        }

        private static StringBuilder BuildSeriesStacked(List<SerieDTO> series, Linx.Dsl.Components.Enums.LibEnum lib)
        {
            int current = 0;
            StringBuilder r = new StringBuilder();
            foreach (var i in series)
            {
                
                if (current > 0)
                    r.Append(",");
                else
                {
                    r.Append("{");
                    r.AppendProperty<string>("name", "parent");
                    r.AppendProperty<string>("type", i.Type, true);
                    r.AppendProperty<string>("xAxis", i.xAxis, true);
                    r.AppendProperty<string>("yAxis", i.yAxis, true);
                    r.AppendProperty<string>("outline", "transparent", true);
                    if (i.Type.Contains("Column"))
                        r.AppendProperty<string>("radius", "0", true);

                    r.Append(",");
                    r.Append("series: [");
                }

                r.Append("{");
                r.AppendProperty<string>("name", i.Name);
                r.AppendProperty<string>("type", "stackedFragment", true);
                r.AppendProperty<string>("title", i.Title, true);
                r.AppendProperty<string>("valueMemberPath", i.ValueMemberPath, true);
                r.AppendProperty<string>("labelMemberPath", i.LabelPath, true);
                r.AppendProperty<bool>("showTooltip", i.Tooltip.ToString().ToLower(), true);
                r.AppendProperty<string>("lowMemberPath", i.LowMemberPath, true);
                r.AppendProperty<string>("highMemberPath", i.HighMemberPath, true);
                r.AppendProperty<string>("radiusMemberPath", i.RadiusMemberPath, true);
                r.AppendProperty<string>("fillMemberPath", i.FillMemberPath, true);
                r.AppendProperty<string>("labelMemberPath", i.LabelMemberPath, true);
                r.AppendProperty<string>("markerType", i.MarkerType, true);
                r.AppendProperty<string>("xMemberPath", i.xMemberPath, true);
                r.AppendProperty<string>("yMemberPath", i.yMemberPath, true);
                r.AppendProperty<string>("displayType", i.DisplayType, true);
                r.AppendProperty<string>("openMemberPath", i.OpenMemberPath, true);
                r.AppendProperty<string>("closeMemberPath", i.CloseMemberPath, true);
                r.AppendProperty<string>("angleAxis", i.AngleAxis, true);
                r.AppendProperty<string>("radiusAxis", i.RadiusAxis, true);
                r.AppendProperty<string>("angleMemberPath", i.AngleMemberPath, true);
                r.AppendProperty<string>("valueAxis", i.ValueAxis, true);

                r.Append(i.GenerateDynamicProperties(lib));
                r.Append("}");
                current++;
            }
            r.Append("]}");
            return r;
        }
    }
}

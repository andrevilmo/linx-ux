using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Dsl.Components.Common
{
    public static class ChartBuilderTelerik
    {
        const string cor = "#8B8386";
        const string coresGraficos = "['Orange', 'Green', 'Red', 'Blue', 'NavajoWhite', 'Yellow', 'Purple', 'MediumSeaGreen', 'DarkSalmon', 'Goldenrod', 'SkyBlue', 'Gray', 'SlateBlue', 'MediumPurple', 'RosyBrown']";

        public static string BuilderJS(ChartTelerikDTO config, Linx.Dsl.Components.Enums.LibEnum lib)
        {
            StringBuilder r = new StringBuilder();

            r.AppendLine("var innerList = [];");
            r.AppendLine("try{");
            r.AppendLine("  var innerList = @dataSource;");
            r.AppendLine("}catch(e){}");
            r.AppendLine();

            #region define o TIPO COMPONENTE
            if (string.Equals(config.ChartType, "pie", StringComparison.InvariantCultureIgnoreCase) == true)
            {
                r.AppendLine("\tif (vm.status() === 'Q') {");
                r.AppendLine("\t\t@chartName.kendoChart({");
            }
            else
                r.AppendLine("\t@chartName.kendoChart({");

            #endregion

            #region Configuracoes genericas
            GenericSettings(config, r);
            #endregion

            if (string.Equals(config.ChartType, "pie", StringComparison.InvariantCultureIgnoreCase) == true)
            {
                r.AppendProperty<object>("dataSource", "{data: innerList }", true);
                SeriesDefaultsPieChart(config, r);
                SeriesPieChart(config, r);
                var format = (config.FormatSeriePie == string.Empty ? "{0}" : config.FormatSeriePie);
                r.AppendProperty<object>("tooltip", "{visible: true, template: \"" + config.LabelMemberPath + ": #= kendo.format('" + format.ToUpper() + "', " + (format.ToUpper().Contains(":P") ? "percentage" : "value") + ") # \" }", true);
                r.AppendLine("\t,seriesClick: function (e) {");
                r.AppendLine("\t\t$.each(e.sender.dataSource.view(), function() {");
                r.AppendLine("\t\t\tthis.explode = false;");
                r.AppendLine("\t\t});");
                r.AppendLine("\t\te.sender.options.transitions = false;");
                r.AppendLine("\t\te.dataItem.explode = true;");
                r.AppendLine("\t\te.sender.refresh();");
                r.AppendLine("\t}");
                r.AppendLine("\t});");
                r.AppendLine("\t}");
                r.AppendLine("\telse{");
                r.AppendLine("\t\t@chartName.kendoChart({");
                GenericSettings(config, r);
                PieChartDefault(config, r);

            }
            else if (config.ChartGroup.Contains("Stacked"))
            {
                r.AppendCollection("valueAxis", ChartBuilderTelerik.BuildValueAxis(config.Series, lib, config));
                SeriesDefaultsStackedChart(config, r);
                r.AppendCollection("series", ChartBuilderTelerik.BuildSeriesStacked(config.Series, lib, config));
                r.AppendCollection("categoryAxis", ChartBuilderTelerik.BuildAxesStacked(config.Axes, lib, config));
                r.AppendProperty<object>("dataSource", "{data: innerList, group: {field: '" + config.Series.Select(x => x.Title).FirstOrDefault() + "', dir: 'asc'} }", true);
                r.AppendProperty<object>("tooltip", "{visible: true, template: \" #= category # : #= kendo.format('" + config.Series.Select(x => x.Format).FirstOrDefault() + "', value) # \" }", true);
            }
            else
            {
                string[] arrField = config.SortField.Split(';');
                string[] arrDir = config.SortDir.Split(';');
                string sort = string.Empty;

                IEnumerable<string> difArrField = arrField.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();
                IEnumerable<string> difArrDir = arrDir.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();

                if ((difArrField.Count() > 0 && difArrDir.Count() > 0) && (difArrField.Count() == difArrDir.Count()))
                {
                    for (int i = 0; i < difArrField.Count(); i++)
                    {
                        sort += "field: '" + difArrField.ElementAt(i) + "', dir: '" + difArrDir.ElementAt(i) + "' ,";
                    }
                }
               
                var fieldAxeGrouped = config.Axes.Where(x => x.GroupAxe).Select(x => x.Label).FirstOrDefault();
                if (fieldAxeGrouped != null)
                    r.AppendProperty<object>("dataSource", "{data: innerList, group: { field: '" + fieldAxeGrouped + "'} " + (sort != string.Empty ? ", sort:{ " + sort + " }" : "") + " }", true);
                else
                    r.AppendProperty<object>("dataSource", "{data: innerList  " + (sort != string.Empty ? ", sort:{ " + sort + " }" : "") + " }", true);
                r.AppendCollection("categoryAxis", ChartBuilderTelerik.BuildAxes(config.Axes, lib, config));
                r.AppendCollection("series", ChartBuilderTelerik.BuildSeries(config.Series, lib, config));

                if (config.MultiAxis)
                    r.AppendCollection("valueAxis", ChartBuilderTelerik.BuildValueAxes(config.Series, lib));
                else
                    r.AppendCollection("valueAxis", ChartBuilderTelerik.BuildValueAxis(config.Series, lib, config));
            }

            r.AppendLine("});");
            if (string.Equals(config.ChartType, "pie", StringComparison.InvariantCultureIgnoreCase) == true)
                r.AppendLine("\t}");

            r.AppendLine("$(window).on(\"resize\", function () {");
            r.AppendLine("   $(chart.selector + \" svg\").width(Number($('.k-content').width()));");
            r.AppendLine("   $(chart.selector + \" svg\").height(Number($('.k-content').height()));");
            r.AppendLine("   if($(chart).data(\"kendoChart\") != undefined)");
            r.AppendLine("      $(chart).data(\"kendoChart\").refresh();");
            r.AppendLine("});");

            r.AppendLine();

            return r.ToString();
        }

        private static void GenericSettings(ChartTelerikDTO config, StringBuilder r)
        {
            r.AppendProperty<object>("title", "{text: '" + config.Title + "'  , color: '" + cor + "'}", false);
            ChartArea(config, r);
            r.AppendProperty<object>("legend", "{position: '" + config.LegendPosition + "', labels: {color: '" + cor + "'}}", true);
            r.AppendProperty<object>("seriesColors", coresGraficos, true);
        }

        private static StringBuilder BuildAxesStacked(List<AxeDTO> axes, Enums.LibEnum lib, ChartTelerikDTO config)
        {
            int current = 0;
            StringBuilder r = new StringBuilder();
            foreach (var i in axes)
            {
                if (current > 0)
                    r.Append(",");

                r.Append("{\r\n");
                r.AppendProperty<object>("field", "'" + i.Label + "'", false);
                r.AppendProperty<object>("text", "{title: '" + i.Title + "'}", true);
                if (current <= 0 && i.CrossHair == true)
                {
                    r.AppendProperty<object>("crosshair", "{visible: true }", true);
                }
                r.AppendProperty<object>("color", "'" + cor + "'", true);

                r.Append(i.GenerateDynamicProperties(lib));
                r.Append("}");
                current++;
            }

            return r;
        }

        private static StringBuilder BuildSeriesStacked(List<SerieDTO> series, Enums.LibEnum lib, ChartTelerikDTO config)
        {
            int current = 0;
            StringBuilder r = new StringBuilder();
            foreach (var i in series)
            {
                if (current > 0)
                    r.Append(",");

                /*  Series
                field: "VlrItem",
                stack:true,
                tooltip: {visible: true, format: '{0:C2}'},
                */

                r.Append("{\r\n");
                r.AppendProperty<object>("field", "'" + i.ValueMemberPath + "'", false);
                r.AppendProperty<object>("stack", "true", true);
                //r.Append(",tooltip: {");
                ////r.AppendProperty<object>("visible", i.Tooltip.ToString().ToLower(), false);
                //r.AppendProperty<object>("format", "'" + i.Format + "'", true);
                //r.Append("}");
                r.Append(i.GenerateDynamicProperties(lib));
                r.Append("}");
                current++;
            }

            return r;
        }

        private static void SeriesDefaultsStackedChart(ChartTelerikDTO config, StringBuilder r)
        {

            r.Append(",seriesDefaults: {\r\n");
            r.AppendProperty<object>("stack", "true", false);
            r.AppendProperty<object>("type", "'" + config.ChartType.ToString() + "'", true);
            r.AppendProperty<object>("gap", "0.3", true);
            r.Append("}\r\n");

        }

        private static void ChartArea(ChartTelerikDTO config, StringBuilder r)
        {
            if (config.Width.ToString() == null || config.Width.ToString() == "0")
                config.Width = 600;

            if (config.Height.ToString() == null || config.Height.ToString() == "0")
                config.Height = 400;

            r.Append(",chartArea: {");
            r.AppendProperty<object>("background", "'transparent'}", false);

        }

        private static void SeriesDefaultsPieChart(ChartTelerikDTO config, StringBuilder r)
        {
            var format = (config.FormatSeriePie == string.Empty ? "{0}" : config.FormatSeriePie);
            r.Append(",seriesDefaults: {\r\n");
            r.Append("labels: {\r\n");
            r.AppendProperty<object>("template", "kendo.template('" + config.LabelMemberPath + ": #= kendo.format(\"" + format.ToUpper() + "\", " + (format.ToUpper().Contains(":P") ? "percentage" : "value") + ") # " + "')");
            r.AppendProperty<string>("position", "outsideEnd", true);
            r.AppendProperty<object>("visible", "true", true);
            r.AppendProperty<string>("background", "transparent", true);
            r.AppendProperty<object>("color", "'" + cor + "'", true);
            r.Append("}\r\n");
            r.Append("},\r\n");
        }

        private static void SeriesPieChart(ChartTelerikDTO config, StringBuilder r)
        {
            r.Append("series: [{\r\n");
            r.AppendProperty<string>("type", "pie");
            r.AppendProperty<string>("field", config.ValueMemberPath, true);
            r.AppendProperty<string>("categoryField", config.Category, true);
            r.Append("}]\r\n");
        }

        private static void PieChartDefault(ChartTelerikDTO config, StringBuilder r)
        {
            r.Append(",series: [{\r\n");
            r.AppendProperty<string>("type", "pie");
            r.Append("\t,data: [{");
            r.AppendProperty<string>("\t\tcategory", config.Category);
            r.AppendProperty<string>("\t\tvalue", "0.1", true);
            r.Append("\t}]");
            r.Append("}]\r\n");
        }

        private static StringBuilder BuildSeriesDefaults(ChartTelerikDTO config, Enums.LibEnum lib)
        {
            StringBuilder r = new StringBuilder();
            r.Append("{");
            r.Append("labels{");
            //r.AppendProperty<object>("labels", "{format: '" + format + "' }");
            r.AppendProperty<object>("template", "kendo.template('" + config.LabelMemberPath + ": #: value # " + "')");
            r.AppendProperty<string>("position", "outsideEnd", true);
            r.AppendProperty<object>("visible", "true", true);
            r.AppendProperty<string>("background", "transparent", true);
            r.Append("}");
            r.Append("}");
            return r;
        }

        private static StringBuilder BuildValueAxis(List<SerieDTO> valueAxis, Enums.LibEnum lib, ChartTelerikDTO config)
        {
            /* MultiAxis não checado  
                valueAxis: {labels: {format: "{0:C}"}}
           */

            StringBuilder r = new StringBuilder();
            string format = valueAxis.Select(x => x.Format).FirstOrDefault() == string.Empty ? "{0}" : valueAxis.Select(x => x.Format).FirstOrDefault();

            r.Append("{\r\n");
            r.AppendProperty<object>("color", "'" + cor + "'");
            r.AppendProperty<object>("labels", "{format: '" + format + "', rotation: " + config.RotacaoSerie + " }", true);
            //r.Append(valueAxis.FirstOrDefault().GenerateDynamicProperties(lib));
            r.Append("}\r\n");

            return r;
        }

        private static StringBuilder BuildValueAxes(List<SerieDTO> valueAxes, Enums.LibEnum lib)
        {
            int current = 0;
            StringBuilder r = new StringBuilder();
            foreach (var i in valueAxes)
            {
                if (current > 0)
                    r.Append(",");

                /* MultiAxis  checado  
                valueAxes
                    min: minArrayValue(dataValues, 'Qtd'),
                    max: maxArrayValue(dataValues, 'Qtd'),
                    labels: {format: "R${0}"},
                    name: "Qtd. Itens Troca",
                    title: { text: "Qtd. Itens Troca" },
                 */

                r.Append("{\r\n");
                r.AppendProperty<object>("min", "0");
                r.AppendProperty<object>("max", "(@maxValue(innerList,'" + i.ValueMemberPath + "') + @maxValue(innerList,'" + i.ValueMemberPath + "') / 5 )", true);
                r.AppendProperty<object>("labels", "{format: '" + i.Format + "'}", true);
                r.AppendProperty<object>("name", "'" + i.Title + "'", true);
                r.AppendProperty<object>("title", "{text: '" + i.Title + "'}", true);
                r.AppendProperty<object>("color", "'" + cor + "'", true);
                r.Append(i.GenerateDynamicProperties(lib));
                r.Append("}\r\n");
                current++;
            }

            return r;
        }

        private static StringBuilder BuildAxes(List<AxeDTO> axes, Linx.Dsl.Components.Enums.LibEnum lib, ChartTelerikDTO config)
        {
            int current = 0;
            StringBuilder r = new StringBuilder();
            foreach (var i in axes.Where(x => x.GroupAxe == false))
            {
                if (current > 0)
                    r.Append(",");

                r.Append("\r\n\t{\r\n");
                r.AppendProperty<object>("field", "'" + i.Label + "'", false);
                r.AppendProperty<object>("text", "{title: '" + i.Title + "'}", true);
                if (current <= 0 && i.CrossHair == true)
                {
                    r.AppendProperty<object>("crosshair", "{visible: true }", true);
                }
                r.AppendProperty<object>("color", "'" + cor + "'", true);
                r.AppendProperty<object>("labels", "{rotation: " + config.RotacaoAxe + "}", true);

                if (config.MultiAxis && !config.ChartGroup.Contains("Stacked"))
                    r.AppendProperty<object>("axisCrossingValue ", "[0 , 100]", true);

                r.Append(i.GenerateDynamicProperties(lib));
                r.Append("\t}\r\n");
                current++;
            }

            return r;
        }

        private static StringBuilder BuildSeries(List<SerieDTO> series, Linx.Dsl.Components.Enums.LibEnum lib, ChartTelerikDTO config)
        {
            int current = 0;
            StringBuilder r = new StringBuilder();
            var formatToolTip = "";

            foreach (var i in series)
            {
                if (current > 0)
                    r.Append(",");

                /*  Series
                type: "line",
                name: "Venda Hora",
                tooltip: {visible: true},
                field: "TotalVendaPorDia",
                */
                if (!string.IsNullOrEmpty(i.Format) || i.Format != string.Empty)
                    formatToolTip = i.Format;
                else
                    formatToolTip = "{0}";

                r.Append("{\r\n");
                r.AppendProperty<object>("name", "'" + i.Title + "'", false);
                if (i.EnableMultiType)
                    r.AppendProperty<object>("type", "'" + i.MultiType + "'", true);
                else
                    r.AppendProperty<object>("type", "'" + i.Type + "'", true);
                if (series.Count > 0 && !config.ChartGroup.Contains("Stacked") && config.MultiAxis)
                    r.AppendProperty<object>("axis", "'" + i.Title + "'", true);

                r.AppendProperty<object>("tooltip", "{visible: true, shared: true, template: \" " + i.Title + " : #= kendo.format('" + formatToolTip + "', value) # \" }", true);
                r.AppendProperty<object>("field", "'" + i.ValueMemberPath + "'", true);
                r.Append(i.GenerateDynamicProperties(lib));
                r.Append("}\r\n");
                current++;
            }

            return r;
        }
    }
}

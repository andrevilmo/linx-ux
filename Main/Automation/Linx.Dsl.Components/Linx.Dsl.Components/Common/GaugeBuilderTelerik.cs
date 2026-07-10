using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Dsl.Components.Common
{
    public static class GaugeBuilderTelerik
    {
        const string cor = "#8B8386";

        public static string BuilderGaugeJS(GaugeTelerikDTO config, Linx.Dsl.Components.Enums.LibEnum lib)
        {
            StringBuilder r = new StringBuilder();
            
            //r.AppendLine("$(\"div.pull-left\").removeClass(\"pull-left\");");
            r.AppendLine();

            r.AppendLine("$root.get@KPINameRanges(completeKPIData);");
            r.AppendLine("function completeKPIData(range, min, max){");

            r.AppendLine("\t@gaugeName.kendoRadialGauge({");
            r.AppendProperty<object>("gaugeArea", "{background: 'transparent'}", false);
            r.AppendProperty<object>("pointer", "{value: @valueField, color: '" + cor + "'}", true);
            GaugeScale(config, r);
            r.AppendLine("});");

            r.AppendLine("}");

            r.AppendLine("$(window).on(\"resize\", function() {kendo.resize($(@gaugeName));});");
            r.AppendLine();

            return r.ToString();
        }

        private static void GaugeScale(GaugeTelerikDTO config, StringBuilder r)
        {                                          
            r.Append(",scale: {\n");
            r.AppendProperty<object>("startAngle", config.StartAngle.ToString(), false);
            r.AppendProperty<object>("endAngle", config.EndAngle.ToString(), true);
            r.AppendProperty<object>("min", "min", true);
            r.AppendProperty<object>("max", "max", true);
            r.Append(",labels: {\n");
            r.AppendProperty<object>("position", "'" + config.Position + "'", false);
            r.AppendProperty<object>("format", "'" + config.FormatLabel + "'", true);
            r.AppendProperty<object>("color", "'" + cor + "'", true);           
            r.Append("\t}\n");
            r.AppendProperty<object>("ranges", "range", true);
            r.Append("}\n");
        }
    }
}

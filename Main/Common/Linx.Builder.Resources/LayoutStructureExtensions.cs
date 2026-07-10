using Linx.Tools;
using System;
using System.Runtime.CompilerServices;

namespace Linx.Builder.Resources
{
    public static class LayoutStructureExtensions
    {
        public static int GetDataGridOrder(this LayoutControlV2 control)
        {
            int num;
            int num1;
            num1 = (int.TryParse(control.DataGridOrder, out num) ? num : 0);
            return num1;
        }

        public static int GetPrecision(this LayoutControlV2 control)
        {
            return LayoutStructureExtensions.GetPrecision(control.Precision);
        }

        public static int GetPrecision(string precision)
        {
            int num = 0;
            if (!precision.IsNullOrEmpty())
            {
                num = (!precision.Contains(":") ? (int)((!precision.IsNullOrEmpty() ? decimal.Parse(precision) / new decimal(10) : decimal.Zero)) : int.Parse(precision.Left(":")));
            }
            return num;
        }

        public static int GetPrecisionDecimals(string precision)
        {
            int num = 0;
            if (!precision.IsNullOrEmpty())
            {
                if (!precision.Contains(":"))
                {
                    decimal num1 = (!precision.IsNullOrEmpty() ? decimal.Parse(precision) / new decimal(10) : decimal.Zero);
                    num = (int)(new decimal(10) * (num1 - (int)num1));
                }
                else
                {
                    num = int.Parse(precision.Right(":"));
                }
            }
            return num;
        }

        public static int GetPrecisionDecimalsInt(this LayoutControlV2 control)
        {
            return LayoutStructureExtensions.GetPrecisionDecimals(control.Precision);
        }

        public static string GetPrecisionDecimalsToString(this LayoutControlV2 control, string ui)
        {
            int precisionDecimals = LayoutStructureExtensions.GetPrecisionDecimals(control.Precision);
            return (control.BrandDecimalsControl ? string.Format("$root.{0}().getDecimalsByData($data, {1})", ui, precisionDecimals) : precisionDecimals.ToString());
        }
    }
}
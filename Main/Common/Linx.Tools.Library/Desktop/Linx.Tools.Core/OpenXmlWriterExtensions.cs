using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using sheet = DocumentFormat.OpenXml.Spreadsheet;

namespace Linx.Tools
{
    public static class OpenXmlWriterExtensions
    {
        // Number of 100ns ticks per time unit 
        static long TicksPerMillisecond = 10000;
        static long TicksPerSecond = TicksPerMillisecond * 1000;
        static long TicksPerMinute = TicksPerSecond * 60;
        static long TicksPerHour = TicksPerMinute * 60;
        static long TicksPerDay = TicksPerHour * 24;

        // Number of milliseconds per time unit 
        static int MillisPerSecond = 1000;
        static int MillisPerMinute = MillisPerSecond * 60;
        static int MillisPerHour = MillisPerMinute * 60;
        static int MillisPerDay = MillisPerHour * 24;

        // Number of days in a non-leap year 
        static int DaysPerYear = 365;
        // Number of days in 4 years 
        static int DaysPer4Years = DaysPerYear * 4 + 1;
        // Number of days in 100 years
        static int DaysPer100Years = DaysPer4Years * 25 - 1;
        // Number of days in 400 years
        static int DaysPer400Years = DaysPer100Years * 4 + 1;

        static long DaysTo1899 = DaysPer400Years * 4 + DaysPer100Years * 3 - 367;
        static long DoubleDateOffset = DaysTo1899 * TicksPerDay;
        static long OADateMinAsTicks = (DaysPer100Years - DaysPerYear) * TicksPerDay;

        static double OADateMinAsDouble = -657435.0;
        static double OADateMaxAsDouble = 2958466.0;
        static int DaysTo10000 = DaysPer400Years * 25 - 366;
        static long MaxMillis = (long)DaysTo10000 * MillisPerDay;


        public static Double ToOADate(this DateTime date)
        {
            var value = date.Ticks;

            if (value == 0)
                return 0.0;  // Returns OleAut's zero'ed date value.
            if (value < TicksPerDay) // This is a fix for VB. They want the default day to be 1/1/0001 rathar then 12/30/1899.
                value += DoubleDateOffset; // We could have moved this fix down but we would like to keep the bounds check.
            if (value < OADateMinAsTicks)
                throw new OverflowException("Invalid Date!");

            // Currently, our max date == OA's max date (12/31/9999), so we don't 
            // need an overflow check in that direction. 
            long millis = (value - DoubleDateOffset) / TicksPerMillisecond;
            if (millis < 0)
            {
                long frac = millis % MillisPerDay;
                if (frac != 0) millis -= (MillisPerDay + frac) * 2;
            }
            return (double)millis / MillisPerDay;
        }


        public static DateTime FromOADate(double oaDate)
        {
            return new DateTime(DoubleDateToTicks(oaDate), DateTimeKind.Unspecified);            
        }

        internal static long DoubleDateToTicks(double value)
        {
            if (value >= OADateMaxAsDouble || value <= OADateMinAsDouble)
                throw new ArgumentException("Invalid DateTime");
            long millis = (long)(value * MillisPerDay + (value >= 0 ? 0.5 : -0.5));
            // The interesting thing here is when you have a value like 12.5 it all positive 12 days and 12 hours from 01/01/1899
            // However if you a value of -12.25 it is minus 12 days but still positive 6 hours, almost as though you meant -11.75 all negative 
            // This line below fixes up the millis in the negative case
            if (millis < 0)
            {
                millis -= (millis % MillisPerDay) * 2;
            }

            millis += DoubleDateOffset / TicksPerMillisecond;

            if (millis < 0 || millis >= MaxMillis) throw new ArgumentException("Invalid DateTime Scale");
            return millis * TicksPerMillisecond;
        }


        public static void AddAutoFilter(this OpenXmlWriter writer, string range)
        {
            sheet.AutoFilter filter = new sheet.AutoFilter();
            filter.Reference = range;
            writer.WriteElement(filter);
        }

        public static void SettingColumnsFit(this OpenXmlWriter writer, PropertyDefinitions[] columns)
        {
            writer.WriteStartElement(new sheet.Columns());
            for (uint colInx = 0; colInx < columns.Length; colInx++)
                writer.WriteElement(CreateColumnData(colInx + 1, 14));

            writer.WriteEndElement();
        }

        private static sheet.Column CreateColumnData(UInt32 columnIndex, double ColumnWidth)
        {
            sheet.Column column = new sheet.Column();
            column.Min = columnIndex;
            column.Max = columnIndex;
            column.Width = DoubleValue.FromDouble(ColumnWidth);
            column.CustomWidth = BooleanValue.FromBoolean(true);
            column.BestFit = BooleanValue.FromBoolean(true);
            return column;
        }
    }

}

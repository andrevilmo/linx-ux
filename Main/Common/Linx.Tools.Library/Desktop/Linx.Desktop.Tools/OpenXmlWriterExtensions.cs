using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using sheet = DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Linx.Tools
{
    public static class OpenXmlWriterExtensions
    {
        public static void AddAutoFilter(this OpenXmlWriter writer, SpreadsheetDocument spreadsheet, string firstColum, uint firstRow, string lastColumn, uint lastRow, sheet.Sheet sheet)
        {
            sheet.AutoFilter filter = new sheet.AutoFilter { Reference = string.Format("{0}{1}:{2}{3}", firstColum, firstRow, lastColumn, lastRow) };
            writer.WriteElement(filter);
            if (spreadsheet.WorkbookPart.Workbook.DefinedNames == null)
                spreadsheet.WorkbookPart.Workbook.DefinedNames = new sheet.DefinedNames();

            spreadsheet.WorkbookPart.Workbook.DefinedNames.AppendChild(new sheet.DefinedName(string.Format("'{0}'!${1}${2}:${3}${4}",
                sheet.Name,
                firstColum,
                firstRow,
                lastColumn,
                lastRow))
            {
                Name = "_xlnm._FilterDatabase",
                LocalSheetId = sheet.SheetId - 1,
                Hidden = true
            });
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

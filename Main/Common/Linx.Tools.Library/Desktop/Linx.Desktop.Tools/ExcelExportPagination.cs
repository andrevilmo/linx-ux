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


namespace Linx.Tools
{
    public static class ExcelExportPagination<T>
    {
        private const int PageSize = 150000;
        private const int MaxRowsAllowed = 1048576;
        private const int MaxColumnsAllowed = 16384;


        public struct EntitiesToExport
        {
            public LinxEntityReferenceInfo Metadata { get; set; }
            public IQueryable<T> Entities { get; set; }
            public string JExpressionTranslated { get; set; }
        }



        public static byte[] CreateExcelDocumentFile(params EntitiesToExport[] exports)
        {
            string filePath = Path.GetTempFileName();
            try
            {
                using (SpreadsheetDocument document = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook))
                {
                    WriteExcelFile(exports, document);
                    document.Close();
                }
                return File.ReadAllBytes(filePath);
            }
            catch (OutOfMemoryException memoryException)
            {
                throw new Exception("Unable to export records, try exporting fewer records", memoryException);
            }
            finally
            {
                if (!String.IsNullOrEmpty(filePath) && File.Exists(filePath))
                    File.Delete(filePath);
            }
        }
        public static string CreateExcelDocumentFileMapPath(string fileName, params EntitiesToExport[] exports)
        {
            string path = System.Web.HttpContext.Current.Server.MapPath("~/FileDownload/");
            string logicalFilePath = "~/FileDownload/" + string.Format("{0}-{1}.xlsx", fileName, Guid.NewGuid());
            string physicalFilePath = System.Web.HttpContext.Current.Server.MapPath(logicalFilePath);

            //Remove arquivos que tem data de criação superior a 12 horas.
            Task.Factory.StartNew(() =>
            {
                try
                {
                    if (!Directory.Exists(Path.GetDirectoryName(path)))
                        return;

                    DirectoryInfo info = new DirectoryInfo(path);
                    info.GetFiles().Where(i => i.CreationTime <= DateTime.Now.AddHours(-12)).Foreach(file => { file.Delete(); });
                }
                catch { }
            });

            //Gera nova planilha excel.            
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(physicalFilePath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(physicalFilePath));

                using (SpreadsheetDocument document = SpreadsheetDocument.Create(physicalFilePath, SpreadsheetDocumentType.Workbook))
                {
                    WriteExcelFile(exports, document);
                    document.Close();
                }
            }
            catch (OutOfMemoryException memoryException)
            {
                throw new Exception("Unable to export records, try exporting fewer records", memoryException);
            }

            return logicalFilePath;
        }

        private static uint getStyleId(string dateTimeFormat = null, int decimalNumber = 0, bool header = false)
        {
            if (header)
                return 1U;
            if (dateTimeFormat != null)
            {

                switch (dateTimeFormat)
                {
                    case "dd/MM/yyyy":
                    case "d":
                        return 13U;
                    case "dd/MM/yyyy HH:mm":
                    case "dd/MM/yyyy HH:mm:ss":
                    case "D":
                    case "g":
                    case "G":
                        return 14U;
                    case "HH:mm":
                    case "HH:mm:ss":
                    case "t":
                    case "T":
                        return 15U;
                    default:
                        return 13U;
                }
            }

            if (decimalNumber > 0)
            {
                return Convert.ToUInt32(decimalNumber + 3);
            }
            return 0U;
        }

        private static void CreateStyleSheet(SpreadsheetDocument spreadsheet)
        {
            WorkbookStylesPart workbookStylesPart = spreadsheet.WorkbookPart.AddNewPart<WorkbookStylesPart>();
            sheet.Stylesheet stylesheet = new sheet.Stylesheet();

            stylesheet.Fonts = new sheet.Fonts();
            stylesheet.Fonts.Append(new sheet.Font());
            stylesheet.Fonts.Append(new sheet.Font() { Bold = new sheet.Bold() });

            stylesheet.Fills = new sheet.Fills();
            stylesheet.Fills.Append(new sheet.Fill());

            stylesheet.Borders = new sheet.Borders();
            stylesheet.Borders.Append(new sheet.Border());


            stylesheet.NumberingFormats = new sheet.NumberingFormats();
            stylesheet.NumberingFormats.Append(new sheet.NumberingFormat() { NumberFormatId = 101, FormatCode = "0.0" });
            stylesheet.NumberingFormats.Append(new sheet.NumberingFormat() { NumberFormatId = 102, FormatCode = "0.00" });
            stylesheet.NumberingFormats.Append(new sheet.NumberingFormat() { NumberFormatId = 103, FormatCode = "0.000" });
            stylesheet.NumberingFormats.Append(new sheet.NumberingFormat() { NumberFormatId = 104, FormatCode = "0.0000" });
            stylesheet.NumberingFormats.Append(new sheet.NumberingFormat() { NumberFormatId = 105, FormatCode = "0.00000" });
            stylesheet.NumberingFormats.Append(new sheet.NumberingFormat() { NumberFormatId = 106, FormatCode = "0.000000" });
            stylesheet.NumberingFormats.Append(new sheet.NumberingFormat() { NumberFormatId = 107, FormatCode = "0.0000000" });
            stylesheet.NumberingFormats.Append(new sheet.NumberingFormat() { NumberFormatId = 108, FormatCode = "0.00000000" });
            stylesheet.NumberingFormats.Append(new sheet.NumberingFormat() { NumberFormatId = 109, FormatCode = "0.000000000" });
            stylesheet.NumberingFormats.Append(new sheet.NumberingFormat() { NumberFormatId = 110, FormatCode = "dd/mm/yyyy" });
            stylesheet.NumberingFormats.Append(new sheet.NumberingFormat() { NumberFormatId = 111, FormatCode = "dd/mm/yyyy hh:mm;" });
            stylesheet.NumberingFormats.Append(new sheet.NumberingFormat() { NumberFormatId = 112, FormatCode = "hh:mm" });


            stylesheet.CellFormats = new sheet.CellFormats();
            stylesheet.CellFormats.Append(new sheet.CellFormat() { FontId = 0, FillId = 0, BorderId = 0 });//0
            stylesheet.CellFormats.Append(new sheet.CellFormat() { FontId = 1, FillId = 0, BorderId = 0 });//1
            stylesheet.CellFormats.Append(new sheet.CellFormat() { FontId = 0, FillId = 0, BorderId = 0 });//2
            stylesheet.CellFormats.Append(new sheet.CellFormat() { FontId = 1, FillId = 0, BorderId = 0 });//3
            stylesheet.CellFormats.Append(new sheet.CellFormat() { FontId = 0, FillId = 0, BorderId = 0, NumberFormatId = 101, ApplyNumberFormat = true });//4
            stylesheet.CellFormats.Append(new sheet.CellFormat() { FontId = 0, FillId = 0, BorderId = 0, NumberFormatId = 102, ApplyNumberFormat = true });//5
            stylesheet.CellFormats.Append(new sheet.CellFormat() { FontId = 0, FillId = 0, BorderId = 0, NumberFormatId = 103, ApplyNumberFormat = true });//6
            stylesheet.CellFormats.Append(new sheet.CellFormat() { FontId = 0, FillId = 0, BorderId = 0, NumberFormatId = 104, ApplyNumberFormat = true });//7
            stylesheet.CellFormats.Append(new sheet.CellFormat() { FontId = 0, FillId = 0, BorderId = 0, NumberFormatId = 105, ApplyNumberFormat = true });//8
            stylesheet.CellFormats.Append(new sheet.CellFormat() { FontId = 0, FillId = 0, BorderId = 0, NumberFormatId = 106, ApplyNumberFormat = true });//9
            stylesheet.CellFormats.Append(new sheet.CellFormat() { FontId = 0, FillId = 0, BorderId = 0, NumberFormatId = 107, ApplyNumberFormat = true });//10
            stylesheet.CellFormats.Append(new sheet.CellFormat() { FontId = 0, FillId = 0, BorderId = 0, NumberFormatId = 108, ApplyNumberFormat = true });//11
            stylesheet.CellFormats.Append(new sheet.CellFormat() { FontId = 0, FillId = 0, BorderId = 0, NumberFormatId = 109, ApplyNumberFormat = true });//12
            stylesheet.CellFormats.Append(new sheet.CellFormat() { FontId = 0, FillId = 0, BorderId = 0, NumberFormatId = 110, ApplyNumberFormat = true });//13
            stylesheet.CellFormats.Append(new sheet.CellFormat() { FontId = 0, FillId = 0, BorderId = 0, NumberFormatId = 111, ApplyNumberFormat = true });//14
            stylesheet.CellFormats.Append(new sheet.CellFormat() { FontId = 0, FillId = 0, BorderId = 0, NumberFormatId = 112, ApplyNumberFormat = true });//15

            workbookStylesPart.Stylesheet = stylesheet;
            workbookStylesPart.Stylesheet.Save();
        }

        private static void WriteExcelFile(EntitiesToExport[] tables, SpreadsheetDocument spreadsheet)
        {
            spreadsheet.AddWorkbookPart();
            spreadsheet.WorkbookPart.Workbook = new DocumentFormat.OpenXml.Spreadsheet.Workbook();

            CreateStyleSheet(spreadsheet);

            spreadsheet.WorkbookPart.Workbook.Append(new sheet.BookViews(new sheet.WorkbookView()));

            uint worksheetNumber = 1;
            sheet.Sheets sheets = spreadsheet.WorkbookPart.Workbook.AppendChild<sheet.Sheets>(new sheet.Sheets());
            foreach (var table in tables)
            {
                string worksheetName = table.Metadata.DisplayName;
                if (string.IsNullOrEmpty(worksheetName))
                    worksheetName = worksheetName.Replace("/", "|").Replace("*", "_");

                WorksheetPart newWorksheetPart = spreadsheet.WorkbookPart.AddNewPart<WorksheetPart>();
                sheet.Sheet sheet = new sheet.Sheet() { Id = spreadsheet.WorkbookPart.GetIdOfPart(newWorksheetPart), SheetId = worksheetNumber, Name = worksheetName };
                sheets.Append(sheet);

                WriteEntitiesToExportToExcelWorksheet(table, newWorksheetPart, spreadsheet, sheet);

                worksheetNumber++;
            }

            spreadsheet.WorkbookPart.Workbook.Save();
        }

        #region AppendTextCell

        private static void AppendTextCell(string cellReference, OpenXmlSimpleType cellStringValue, ref OpenXmlWriter writer, bool isHeader = false)
        {
            _AppendCell(cellReference, cellStringValue, ref writer, sheet.CellValues.String, getStyleId(header: isHeader));
        }

        private static void AppendNumericCell(string cellReference, OpenXmlSimpleType cellStringValue, ref OpenXmlWriter writer, int totalDecimais)
        {
            _AppendCell(cellReference, cellStringValue, ref writer, sheet.CellValues.Number, getStyleId(decimalNumber: totalDecimais));
        }
        private static void AppendBooleanCell(string cellReference, OpenXmlSimpleType cellStringValue, ref OpenXmlWriter writer)
        {
            _AppendCell(cellReference, cellStringValue, ref writer, sheet.CellValues.Boolean);
        }
        private static void AppendDateCell(string cellReference, OpenXmlSimpleType cellStringValue, ref OpenXmlWriter writer, string format)
        {
            _AppendCell(cellReference, cellStringValue, ref writer, getStyleId(dateTimeFormat: format ?? string.Empty));
        }
        private static void _AppendCell(string cellReference, OpenXmlSimpleType cellStringValue, ref OpenXmlWriter writer, uint styleIndex = 0U)
        {
            writer.WriteElement(new sheet.Cell
            {
                CellValue = new sheet.CellValue(cellStringValue),
                CellReference = cellReference,
                StyleIndex = new UInt32Value(styleIndex)
            });
        }
        private static void _AppendCell(string cellReference, OpenXmlSimpleType cellStringValue, ref OpenXmlWriter writer, sheet.CellValues cellValues, uint styleIndex = 0U)
        {
            writer.WriteElement(new sheet.Cell
            {
                CellValue = new sheet.CellValue(cellStringValue),
                CellReference = cellReference,
                DataType = cellValues,
                StyleIndex = new UInt32Value(styleIndex)
            });
        }

        #endregion

        private static void WriteEntitiesToExportToExcelWorksheet(EntitiesToExport table, WorksheetPart worksheetPart, SpreadsheetDocument spreadsheet, sheet.Sheet sheet)
        {
            var totalRows = table.Entities.Count();
            if (totalRows > MaxRowsAllowed)
            {
                throw new InvalidOperationException("A exportação ultrapassou o limite de linhas que o excel suporta.\nMáximo suportado: " + MaxRowsAllowed.ToString());
            }

            var columns = table.Metadata.Properties.Where(p => p.IsBrowsable).OrderBy(p => p.Order).ToArray();
            char[] typeColumns = new char[columns.Length];


            string[] excelColumnNames = new string[columns.Length];
            for (int n = 0; n < columns.Length; n++)
                excelColumnNames[n] = GetExcelColumnName(n);


            if (columns.Length > MaxColumnsAllowed)
            {
                throw new InvalidOperationException("A exportação ultrapassou o limite de colunas que o excel suporta.\nMáximo suportado: " + MaxColumnsAllowed.ToString());
            }

            OpenXmlWriter writer = OpenXmlWriter.Create(worksheetPart);
            writer.WriteStartElement(new sheet.Worksheet());


            writer.SettingColumnsFit(columns);

            writer.WriteStartElement(new sheet.SheetData());

            uint rowIndex = 1;
            PropertyDefinitions col = null;


            #region Print JExpressionFilter translated in B1
            if (!string.IsNullOrEmpty(table.JExpressionTranslated))
            {
                writer.WriteStartElement(new sheet.Row { RowIndex = rowIndex });
                AppendTextCell("A" + rowIndex.ToString(), StringValue.FromString("Filtro:"), ref writer, true);
                AppendTextCell("B" + rowIndex.ToString(), StringValue.FromString(table.JExpressionTranslated), ref writer, false);
                writer.WriteEndElement();
                rowIndex++;
            }
            #endregion

            uint firstRow = rowIndex;
            #region Print Headers
            writer.WriteStartElement(new sheet.Row { RowIndex = rowIndex });
            for (int colInx = 0; colInx < columns.Length; colInx++)
            {
                col = columns[colInx];
                AppendTextCell(excelColumnNames[colInx] + rowIndex.ToString(), StringValue.FromString(col.Caption), ref writer, true);
                typeColumns[colInx] = GetTypeColumn(col);
            }
            writer.WriteEndElement();   //  End of header "Row" 
            #endregion

            #region Print Body Rows

            int skip = 0;
            while (skip < totalRows)
            {
                var entities = table.Entities.Skip(skip).Take(PageSize);
                entities.Foreach(
                    (entity) =>
                    {
                        ++rowIndex;
                        writer.WriteStartElement(new sheet.Row { RowIndex = rowIndex });

                        for (int colInx = 0; colInx < columns.Length; colInx++)
                        {
                            col = columns[colInx];
                            PropertyInfo property = entity.GetType().GetProperty(col.Name);

                            var cellValue = GetStringValue(entity, property, col);
                            switch (typeColumns[colInx])
                            {
                                case 'N':
                                    AppendNumericCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, ref writer, col.GetPrecisionDecimals());
                                    break;
                                case 'D':
                                    AppendDateCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, ref writer, col.DataFormat);
                                    break;
                                case 'B':
                                    AppendBooleanCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, ref writer);
                                    break;
                                default:
                                    AppendTextCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, ref writer);
                                    break;
                            }
                        }
                        writer.WriteEndElement(); //  End of Row
                    });

                skip += PageSize;
            }


            #endregion

            writer.WriteEndElement(); //  End of SheetData
            writer.AddAutoFilter(spreadsheet, excelColumnNames[0], firstRow, excelColumnNames[columns.Length - 1], rowIndex, sheet);
            writer.WriteEndElement(); //  End of worksheet       

            writer.Close();
        }

        private static char GetTypeColumn(PropertyDefinitions col)
        {
            if (col.DataType == "DateTime")
                return 'D';

            if (col.DataType == "Boolean")
                return 'B';

            if (new string[] { "Int64", "Decimal", "Byte", "Int16", "Int32", "Double", "Single" }.Contains(col.DataType))
                return 'N';
            else
                return 'S';
        }

        private static sheet.Column CreateColumnData(UInt32 StartColumnIndex, UInt32 EndColumnIndex, double ColumnWidth)
        {
            sheet.Column column = new sheet.Column();
            column.Min = UInt32Value.FromUInt32(StartColumnIndex);
            column.Max = UInt32Value.FromUInt32(EndColumnIndex);
            column.Width = DoubleValue.FromDouble(ColumnWidth);
            column.CustomWidth = BooleanValue.FromBoolean(true);
            column.BestFit = BooleanValue.FromBoolean(true);
            return column;
        }

        private static OpenXmlSimpleType GetStringValue(object entity, PropertyInfo property, PropertyDefinitions colDefinition)
        {
            var value = property.GetValue(entity);
            if (value == null)
                return new StringValue();

            if (colDefinition.DataType == "Boolean")
            {
                return BooleanValue.FromBoolean((bool)value);
            }

            if (colDefinition.DataType == "DateTime")

                switch (colDefinition.DataFormat)
                {
                    case "d":
                    case "D":
                        return DoubleValue.FromDouble(((DateTime)value).Date.ToOADate());
                    case "t":
                    case "T":
                        return DoubleValue.FromDouble(DateTime.FromOADate(0).Add(((DateTime)value).TimeOfDay).ToOADate());

                    default:
                        return DoubleValue.FromDouble(((DateTime)value).ToOADate());
                }




            if (colDefinition.DataType == "Decimal")
                return DecimalValue.FromDecimal((decimal)value);

            if (colDefinition.DataType == "Double")
                return DoubleValue.FromDouble((double)value);


            return StringValue.FromString(value.ToString());
        }
        private static string GetExcelColumnName(int columnIndex)
        {
            //  Convert a zero-based column index into an Excel column reference  (A, B, C.. Y, Y, AA, AB, AC... AY, AZ, B1, B2..)
            //
            //  eg  GetExcelColumnName(0) should return "A"
            //      GetExcelColumnName(1) should return "B"
            //      GetExcelColumnName(25) should return "Z"
            //      GetExcelColumnName(26) should return "AA"
            //      GetExcelColumnName(27) should return "AB"
            //      ..etc..
            //
            if (columnIndex < 26)
                return ((char)('A' + columnIndex)).ToString();

            char firstChar = (char)('A' + (columnIndex / 26) - 1);
            char secondChar = (char)('A' + (columnIndex % 26));

            return string.Format("{0}{1}", firstChar, secondChar);
        }

    }
}

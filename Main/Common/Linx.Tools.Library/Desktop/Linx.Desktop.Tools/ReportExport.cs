using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Tools
{
    public static class ReportExport
    {
        public struct EntitiesToExport
        {
            public LinxEntityReferenceInfo Metadata { get; set; }
            public string JExpressionTranslated { get; set; }
            public string ReportName { get; set; }
            public string DataSourceObject { get; set; }
            public string DataSourceFullName { get; set; }
            public string DataSourceAssembly { get; set; }
            public string JQueryExpression { get; set; }
            public string ServiceBusUrl { get; set; }
            public bool HasMedia { get; set; }
            public bool OnLastColumn { get; set; }
        }

        public static string CreateXmlReport(EntitiesToExport export)
        {
            var reportName = export.ReportName;
            var dataSourceObject = export.DataSourceObject;
            var dataSourceAssembly = export.DataSourceAssembly;
            var dataSourceFullName = export.DataSourceFullName;

            var reportHeader = string.Format(
               "<?xml version='1.0' encoding='utf-8'?>" +
               "<Report Width='28.5000005551179cm' Name='{0}' SnapGridSize='0.1cm' xmlns='http://schemas.telerik.com/reporting/2012/3.8'>" +
                "<Style BackgroundColor='White' />", reportName);

            var reportDataSource = string.Format("<DataSources>" +
                                      " <ObjectDataSource DataMember='{0}' Name='LinxDataSource'>" +
                                      "     <DataSource>" +
                                      "         <ClrType FullName='{1}' Assembly='{2}' />" +
                                      "     </DataSource>" +
                                      "     <Parameters>" +
                                      "         <ObjectDataSourceParameter Name='reportItem'>" +
                                      "             <DataType>System.Object</DataType>" +
                                      "             <Value><String>=ReportItem</String></Value>" +
                                      "         </ObjectDataSourceParameter>" +
                                      "     </Parameters>" +
                                      " </ObjectDataSource>" +
                                      "</DataSources>", dataSourceObject, dataSourceFullName, dataSourceAssembly);

            var reportTableCells = string.Format("<Cells>{0}</Cells>", GetCellsCollection(export));
            var reportTableRows = "<Rows><Row Height='0.5cm' /></Rows>";


            var reportTableColumns = string.Format("<Columns>{0}</Columns>", GetColumnsCollection(export));
            var reportTableBody = string.Format("<Body>{0}{1}{2}</Body>",
                reportTableCells,
                reportTableRows,
                reportTableColumns);

            var reportRowGroups = "  <RowGroups>" +
                                     "  <TableGroup Name='Detail'>" +
                                     "      <Groupings><Grouping /></Groupings>" +
                                     "  </TableGroup>" +
                                     "</RowGroups>";


            var reportColumnGroups = string.Format("<ColumnGroups>{0}</ColumnGroups>", GetTableColumnGroupCollection(export));

            var reportTable = string.Format("<Table DataSourceName='LinxDataSource' Width='26cm' Height='1cm' Left='0.600000100930529cm' Top='0.4cm' NoDataMessage='A consulta não obteve resultados.' Name='table1' StyleName='Corporate.TableBody' >" +
                                               "    {0}" +
                                               "    <Corner />" +
                                               "    {1}" +
                                               "    {2}" +
                                               "</Table>",
                                               reportTableBody, reportRowGroups, reportColumnGroups);

            var detailSection = string.Format("<DetailSection Height='2cm' Name='detailSection1'>" +
                                                 "  <Items>{0}</Items>" +
                                                 "</DetailSection>",
                                                 reportTable);

            var pageHeader = string.Format("<PageHeaderSection Height='2.69999964674315cm' Name='pageHeader'>" +
                                "   <Style>" +
                                "       <BorderStyle Bottom='Solid' />" +
                                "       <Padding Bottom='0cm' />" +
                                "   </Style>" +
                                "   <Items>" +
                                "       <TextBox Width='12.8000001907349cm' Height='0.500000178813934cm' Left='2.70000004768372cm' Top='2cm' Value='{0}' Name='TextBoxHeader'>" +
                                "           <Style>" +
                                "               <Font Size='14pt' Bold='True' />" +
                                "           </Style>" +
                                "        </TextBox>" +
                                "       <TextBox Width='3.50000023841858cm' Height='0.400000005960464cm' Left='25cm' Top='0.00009992122372211cm' Value='= Now()' Name='TextBoxDateTime'>" +
                                "           <Style TextAlign='Right' />" +
                                "       </TextBox>" +
                                "       <PictureBox Url='= Parameters.CompanyLogo.Value' Width='2.5cm' Height='1.40000009536743cm' Left='0.0999999493360519cm' Top='1.2cm' MimeType='' Name='PictureBox1' />" +
                                "       <TextBox Width='9.10000038146973cm' Height='0.600000023841858cm' Left='2.59999990463257cm' Top='1.29999994953474cm' Value='= Parameters.CompanyName.Value' Name='TextBoxCompanyName'>" +
                                "           <Style>" +
                                "               <Font Size='14pt' Bold='True' />" +
                                "           </Style>" +
                                "       </TextBox>" +
                                "       <TextBox Width='15.5cm' Height='0.49cm' Left='0cm' Top='0cm' Value='=Parameters.TranslatedJqueryExpression.Value' Name='TextBoxFilter'>" +
                                "           <Style>" +
                                "               <Font Size='10pt' Bold='False' />" +
                                "           </Style>" +
                                "       </TextBox>" +
                                "   </Items>" +
                                "</PageHeaderSection>", reportName);

            var pageFooter = " <PageFooterSection Height='0.7cm' Name='pageFooter'>" +
                                "   <Style>" +
                                "       <BorderStyle Top='Solid' />" +
                                "   </Style>" +
                                "   <Items>" +
                                "       <TextBox Width='3cm' Height='0.41cm' Left='25.5cm' Top='0cm' Value='= &quot;Pg :&quot; + PageNumber.ToString() + &quot;/&quot; + PageCount.ToString()' Name='TextBoxPageCount'>" +
                                "           <Style TextAlign='Right' />" +
                                "       </TextBox>" +
                                "   </Items>" +
                                "</PageFooterSection>";


            var reportItems = string.Format("<Items>" +
                                               "    {0}" +
                                               "    {1}" +
                                               "    {2}" +
                                               "</Items>",
                                               pageHeader,
                                               pageFooter,
                                               detailSection);

            var reportStylesheet = "<StyleSheet>" +
                                      " <StyleRule>" +
                                      "     <Style Color='Black'>" +
                                      "         <BorderStyle Default='Solid' />" +
                                      "         <BorderColor Default='Black' />" +
                                      "         <BorderWidth Default='1px' />" +
                                      "         <Font Name='Tahoma' Size='9pt' />" +
                                      "     </Style>" +
                                      "     <Selectors>" +
                                      "         <StyleSelector Type='Table' StyleName='Corporate.TableNormal' />" +
                                      "     </Selectors>" +
                                      " </StyleRule>" +
                                      " <StyleRule>" +
                                      "     <Style BackgroundColor='28, 58, 112' Color='White' VerticalAlign='Middle'>" +
                                      "         <BorderStyle Default='Solid' />" +
                                      "         <BorderColor Default='Black' />" +
                                      "         <BorderWidth Default='1px' />" +
                                      "         <Font Name='Tahoma' Size='10pt' />" +
                                      "     </Style>" +
                                      "     <Selectors>" +
                                      "         <DescendantSelector>" +
                                      "             <Selectors>" +
                                      "                 <TypeSelector Type='Table' />" +
                                      "                 <StyleSelector Type='ReportItem' StyleName='Corporate.TableHeader' />" +
                                      "             </Selectors>" +
                                      "         </DescendantSelector>" +
                                      "     </Selectors>" +
                                      " </StyleRule>" +
                                      " <StyleRule>" +
                                      "     <Style>" +
                                      "         <BorderStyle Default='Solid' />" +
                                      "         <BorderColor Default='Black' />" +
                                      "         <BorderWidth Default='1px' />" +
                                      "         <Font Name='Tahoma' Size='9pt' />" +
                                      "     </Style>" +
                                      "     <Selectors>" +
                                      "         <DescendantSelector><Selectors>" +
                                      "             <TypeSelector Type='Table' />" +
                                      "             <StyleSelector Type='ReportItem' StyleName='Corporate.TableBody' />" +
                                      "             </Selectors>" +
                                      "         </DescendantSelector>" +
                                      "     </Selectors>" +
                                      " </StyleRule>" +
                                      "</StyleSheet>";

            var reportPageSettings = "<PageSettings>" +
                                        "   <PageSettings PaperKind='A4' Landscape='True'>" +
                                        "       <Margins>" +
                                        "           <MarginsU Left='0.5cm' Right='0.5cm' Top='0.5cm' Bottom='0.5cm' />" +
                                        "       </Margins>" +
                                        "   </PageSettings>" +
                                        "</PageSettings>";

            var reportParameters = GetReportParameters(export);

            var reportFooter = "</Report>";

            var report = string.Format("{0}{1}{2}{3}{4}{5}{6}",
                reportHeader,
                reportDataSource,
                reportItems,
                reportStylesheet,
                reportPageSettings,
                reportParameters,
                reportFooter);

            return report;
        }

        private static string GetReportParameters(EntitiesToExport table)
        {
            var reportParameters = String.Format("<ReportParameters>" +
                                      "     <ReportParameter Name='EntitySearchId'><Value><String></String></Value></ReportParameter>" +
                                      "     <ReportParameter Name='CurrentUser'><Value><String></String></Value></ReportParameter>" +
                                      "     <ReportParameter Name='CurrentCompany'><Value><String></String></Value></ReportParameter>" +
                                      "     <ReportParameter Name='AuthorizationToken'><Value><String></String></Value></ReportParameter>" +
                                      "     <ReportParameter Name='TransactionInfo'><Value><String></String></Value></ReportParameter>" +
                                      "     <ReportParameter Name='CompanyLogo'><Value><String></String></Value></ReportParameter>" +
                                      "     <ReportParameter Name='CompanyName'><Value><String></String></Value></ReportParameter>" +
                                      "     <ReportParameter Name='AccessGroup'><Value><String></String></Value></ReportParameter>" +
                                      "     <ReportParameter Name='Application'><Value><String></String></Value></ReportParameter>" +
                                      "     <ReportParameter Name='EconomicGroup'><Value><String></String></Value></ReportParameter>" +
                                      "     <ReportParameter Name='Environment'><Value><String></String></Value></ReportParameter>" +
                                      "     <ReportParameter Name='JqueryExpression'><Value><String></String></Value></ReportParameter>" +
                                      "     <ReportParameter Name='PreDefinedQueryExpression'><Value><String><![CDATA[{0}]]></String></Value></ReportParameter>" +
                                      "     <ReportParameter Name='CurrentUserName'><Value><String></String></Value></ReportParameter>" +
                                      "     <ReportParameter Name='PreDefinedTranslatedJqueryExpression'><Value><String><![CDATA[{1}]]></String></Value></ReportParameter>" +
                                      "     <ReportParameter Name='TranslatedJqueryExpression'><Value><String></String></Value></ReportParameter>" +
                                      "     <ReportParameter Name='GetSample' Type='Boolean' Text='Limitar aos 250 primeiros?' Value='True' />" +
                                      "     <ReportParameter Name='Username' Text='Usuário' Visible='True'/>" +
                                      "     <ReportParameter Name='Password' Text='Senha' Visible='True'/>" +
                                      "     <ReportParameter Name='ServiceBusUrl'><Value><String>{2}</String></Value></ReportParameter>" +
                                      "     <ReportParameter Name='Branch'><Value><String></String></Value></ReportParameter>" +
                                      "     <ReportParameter Name='LoginMode'><Value><String></String></Value></ReportParameter>" +
                                      "</ReportParameters>", table.JQueryExpression, table.JExpressionTranslated, table.ServiceBusUrl);
            return reportParameters;
        }

        private static string GetTableColumnGroupCollection(EntitiesToExport table)
        {
            var index = (table.HasMedia) ? 1 : 0;
            var tableColumnGroupCollection = string.Empty;

            if (table.HasMedia)
            {
                tableColumnGroupCollection +=
                                "<TableGroup>" +
                                "   <ReportItem>" +
                                "       <TextBox Width='2cm' Height='0.5cm' Left='0cm' Top='0cm' Value='Media' Name='textBox{1}' StyleName='Corporate.TableHeader' />" +
                                "   </ReportItem>" +
                                "</TableGroup>";
            }

            foreach (var column in table.Metadata.Properties.Where(p => p.IsBrowsable).OrderBy(p => p.Order))
            {
                tableColumnGroupCollection +=
                    string.Format("<TableGroup>" +
                                "   <ReportItem>" +
                                "       <TextBox Width='2cm' Height='0.5cm' Left='0cm' Top='0cm' Value='{0}' Name='textBox{1}' StyleName='Corporate.TableHeader' />" +
                                "   </ReportItem>" +
                                "</TableGroup>", column.Caption, index++);
            }

            return tableColumnGroupCollection;
        }

        private static string GetColumnsCollection(EntitiesToExport table)
        {
            var columns = table.Metadata.Properties.Where(p => p.IsBrowsable).OrderBy(p => p.Order).ToArray();
            var columnsCollection = string.Empty;

            if (table.HasMedia)
                columnsCollection += "<Column Width='2cm' />";

            for (int i = 0; i < columns.Count(); i++)
                columnsCollection += "<Column Width='2cm' />";

            return columnsCollection;
        }

        private static string GetCellsCollection(EntitiesToExport table)
        {
            var columns = table.Metadata.Properties.Where(p => p.IsBrowsable).OrderBy(p => p.Order).ToArray();
            var columnsCount = (table.HasMedia) ? columns.Count() + 1 : columns.Count();

            var index = (table.HasMedia) ? 1 : 0;

            var cellsCollection = string.Empty;

            if (table.HasMedia)
            {
                cellsCollection +=
                    string.Format("<TableCell RowIndex='0' ColumnIndex='0' RowSpan='1' ColumnSpan='1'>" +
                                        "<ReportItem>" +
                                            "<PictureBox " +
                                                "Url='= Medias.GetMediaPath(&quot;{0}&quot;, &quot;{1}&quot;, ReportItem, ReportItem.DataObject.RawData)' " +
                                                "Width='1.99999994732363in' " +
                                                "Height='0.5cm' " +
                                                "Left='2.95275561014811in' " +
                                                "Top='0.196850299835205in' " +
                                                "Sizing='ScaleProportional' " +
                                                "MimeType='' " +
                                                "Name='pbMedia' " +
                                                "StyleName='Corporate.TableBody' />" +
                                        "</ReportItem>" +
                                  "</TableCell>", table.Metadata.EdmEntityName, table.Metadata.Properties.FirstOrDefault(x => x.IsPK).Name);
            }

            foreach (var column in table.Metadata.Properties.Where(p => p.IsBrowsable).OrderBy(p => p.Order))
            {
                cellsCollection += string.Format(
                                  " <TableCell RowIndex='0' ColumnIndex='{0}' RowSpan='1' ColumnSpan='1'>" +
                                  "  <ReportItem>" +
                                  "      <TextBox Width='2cm' Height='0.5cm' Left='0cm' Top='0cm' " +
                                  "         Value='= Fields.{1}' Name='textBox{2}' StyleName='Corporate.TableBody' />" +
                                  "  </ReportItem>" +
                                  "</TableCell>", index, column.Name, columnsCount + index);
                index++;
            }

            return cellsCollection;
        }
    }
}

namespace LinxTraining001.BV.Reports {
    
    
    public partial class LinxTraining001DetalhamentoVendaVendasView13 {
        
        private Telerik.Reporting.DetailSection detailSection1;
        
        private Telerik.Reporting.Crosstab crosstabVendasView;
        
        private Telerik.Reporting.PageHeaderSection pageHeader;
        
        private Telerik.Reporting.PageFooterSection pageFooter;
        
        private Telerik.Reporting.PictureBox PictureBox1;
        
        private Telerik.Reporting.TextBox TextBoxCompanyName;
        
        private Telerik.Reporting.TextBox TextBoxHeader;
        
        private Telerik.Reporting.TextBox TextBoxDateTime;
        
        private Telerik.Reporting.TextBox TextBoxPageCount;
        
        private Telerik.Reporting.TextBox TextBoxFilter;
        
        private Telerik.Reporting.ObjectDataSource ReportDS;
        
        private Telerik.Reporting.TextBox TextBoxVendasView1;
        
        private Telerik.Reporting.TextBox TextBoxVendasView7;
        
        private Telerik.Reporting.TextBox TextBoxVendasView2;
        
        private Telerik.Reporting.TextBox TextBoxVendasView8;
        
        private Telerik.Reporting.TextBox TextBoxVendasView3;
        
        private Telerik.Reporting.TextBox TextBoxVendasView9;
        
        private Telerik.Reporting.TextBox TextBoxVendasView4;
        
        private Telerik.Reporting.TextBox TextBoxVendasView10;
        
        private Telerik.Reporting.TextBox TextBoxVendasView5;
        
        private Telerik.Reporting.TextBox TextBoxVendasView11;
        
        private void InitializeComponent() {
            // Sections
            this.detailSection1 = new Telerik.Reporting.DetailSection();
            this.detailSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(4.2D);
            this.crosstabVendasView = new Telerik.Reporting.Crosstab();
            var tableGroupControl = new Telerik.Reporting.TableGroup();
            tableGroupControl.Groupings.Add(new Telerik.Reporting.Grouping(null));
            tableGroupControl.Name = "Detail";
            this.crosstabVendasView.RowGroups.Add(tableGroupControl);
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            // PageHeader
            this.pageHeader.Height = new Telerik.Reporting.Drawing.Unit(2D, Telerik.Reporting.Drawing.UnitType.Cm);
            this.pageHeader.Name = "pageHeader";
            this.pageHeader.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.pageHeader.Style.Padding.Bottom = new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm);
            // PictureBox1
            this.PictureBox1 = new Telerik.Reporting.PictureBox();
            this.PictureBox1.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.099999949336051941D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.099999949336051941D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.PictureBox1.MimeType = "";
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.5D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.4000000953674316D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.PictureBox1.Value = "= Parameters.CompanyLogo.Value";
            // TextBoxCompanyName
            this.TextBoxCompanyName = new Telerik.Reporting.TextBox();
            this.TextBoxCompanyName.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(2.5999999046325684D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.20000000298023224D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.TextBoxCompanyName.Name = "TextBoxCompanyName";
            this.TextBoxCompanyName.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(9.1000003814697266D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.60000002384185791D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.TextBoxCompanyName.Style.Font.Bold = true;
            this.TextBoxCompanyName.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(14D, Telerik.Reporting.Drawing.UnitType.Point);
            this.TextBoxCompanyName.Value = "= Parameters.CompanyName.Value";
            // TextBoxHeader
            this.TextBoxHeader = new Telerik.Reporting.TextBox();
            this.TextBoxHeader.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(2.7D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.90D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.TextBoxHeader.Name = "TextBoxHeader";
            this.TextBoxHeader.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(12.800000190734863D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.50000017881393433D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.TextBoxHeader.Style.Font.Bold = true;
            this.TextBoxHeader.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(14D, Telerik.Reporting.Drawing.UnitType.Point);
            this.TextBoxHeader.Value = "tstDD123";
            // TextBoxDateTime
            this.TextBoxDateTime = new Telerik.Reporting.TextBox();
            this.TextBoxDateTime.Name = "TextBoxDateTime";
            this.TextBoxDateTime.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.5000002384185791D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.TextBoxDateTime.Value = "= Now()";
            this.TextBoxDateTime.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            // PageFooter
            this.pageFooter.Height = new Telerik.Reporting.Drawing.Unit(0.70000004768371582D, Telerik.Reporting.Drawing.UnitType.Cm);
            this.pageFooter.Name = "pageFooter";
            this.pageFooter.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            // TextBoxPageCount
            this.TextBoxPageCount = new Telerik.Reporting.TextBox();
            this.TextBoxPageCount.Name = "TextBoxPageCount";
            this.TextBoxPageCount.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.TextBoxPageCount.Value = "= \"Pg :\" + PageNumber.ToString() + \"/\" + PageCount.ToString()";
            this.TextBoxPageCount.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            // TextBoxFilter
            this.TextBoxFilter = new Telerik.Reporting.TextBox();
            this.TextBoxFilter.Name = "TextBoxFilter";
            this.TextBoxFilter.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(15.5D), Telerik.Reporting.Drawing.Unit.Cm(0.4999997615814209D));
            this.TextBoxFilter.Style.Font.Bold = false;
            this.TextBoxFilter.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
             this.TextBoxFilter.Value = "=Parameters.TranslatedJqueryExpression.Value";
            // Parameters
           Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
           reportParameter1.Name = "EntitySearchId";
           reportParameter1.Value = "";
            this.ReportParameters.Add(reportParameter1);
           Telerik.Reporting.ReportParameter reportParameter2 = new Telerik.Reporting.ReportParameter();
           reportParameter2.Name = "CurrentUser";
           reportParameter2.Value = "";
            this.ReportParameters.Add(reportParameter2);
           Telerik.Reporting.ReportParameter reportParameter3 = new Telerik.Reporting.ReportParameter();
           reportParameter3.Name = "CurrentCompany";
           reportParameter3.Value = "";
            this.ReportParameters.Add(reportParameter3);
           Telerik.Reporting.ReportParameter reportParameter4 = new Telerik.Reporting.ReportParameter();
           reportParameter4.Name = "AuthorizationToken";
           reportParameter4.Value = "";
            this.ReportParameters.Add(reportParameter4);
           Telerik.Reporting.ReportParameter reportParameter5 = new Telerik.Reporting.ReportParameter();
           reportParameter5.Name = "TransactionInfo";
           reportParameter5.Value = "";
            this.ReportParameters.Add(reportParameter5);
           Telerik.Reporting.ReportParameter reportParameter6 = new Telerik.Reporting.ReportParameter();
           reportParameter6.Name = "CompanyLogo";
           reportParameter6.Value = " ";
            this.ReportParameters.Add(reportParameter6);
           Telerik.Reporting.ReportParameter reportParameter7 = new Telerik.Reporting.ReportParameter();
           reportParameter7.Name = "CompanyName";
           reportParameter7.Value = " ";
            this.ReportParameters.Add(reportParameter7);
           Telerik.Reporting.ReportParameter reportParameter8 = new Telerik.Reporting.ReportParameter();
           reportParameter8.Name = "AccessGroup";
           reportParameter8.Value = " ";
            this.ReportParameters.Add(reportParameter8);
           Telerik.Reporting.ReportParameter reportParameter9 = new Telerik.Reporting.ReportParameter();
           reportParameter9.Name = "Application";
           reportParameter9.Value = " ";
            this.ReportParameters.Add(reportParameter9);
           Telerik.Reporting.ReportParameter reportParameter10 = new Telerik.Reporting.ReportParameter();
           reportParameter10.Name = "EconomicGroup";
           reportParameter10.Value = " ";
            this.ReportParameters.Add(reportParameter10);
           Telerik.Reporting.ReportParameter reportParameter11 = new Telerik.Reporting.ReportParameter();
           reportParameter11.Name = "Environment";
           reportParameter11.Value = " ";
            this.ReportParameters.Add(reportParameter11);
           Telerik.Reporting.ReportParameter reportParameter12 = new Telerik.Reporting.ReportParameter();
           reportParameter12.Name = "JqueryExpression";
           reportParameter12.Value = " ";
           this.ReportParameters.Add(reportParameter12);
           Telerik.Reporting.ReportParameter reportParameter13 = new Telerik.Reporting.ReportParameter();
           reportParameter13.Name = "CurrentUserName";
           reportParameter13.Value = "";
            this.ReportParameters.Add(reportParameter13);
           Telerik.Reporting.ReportParameter reportParameter14 = new Telerik.Reporting.ReportParameter();
           reportParameter14.Name = "TranslatedJqueryExpression";
           reportParameter14.Value = " ";
           this.ReportParameters.Add(reportParameter14);
            var tableGroupSubReport = new Telerik.Reporting.TableGroup();
            tableGroupSubReport.Name = "tableGroupSubReport";
            tableGroupControl.ChildGroups.Add(tableGroupSubReport);
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] { this.detailSection1, this.pageHeader, this.pageFooter });
            // BeginInit
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.detailSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(0.67D);
            this.detailSection1.Name = "detailSection1";
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] { this.TextBoxHeader, this.TextBoxDateTime, this.PictureBox1, this.TextBoxCompanyName });
            this.crosstabVendasView.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.detailSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] { this.crosstabVendasView });
            this.crosstabVendasView.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(Telerik.Reporting.Drawing.Unit.Cm(1.5D)));
            this.TextBoxVendasView1 = new Telerik.Reporting.TextBox();
            this.TextBoxVendasView1.Name = "TextBoxVendasView1";
            this.TextBoxVendasView1.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.2, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.TextBoxVendasView1.Style.Font.Bold = true;
            this.TextBoxVendasView1.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
            this.TextBoxVendasView1.Value = "Data";
            var tableGroupVendasView1 = new Telerik.Reporting.TableGroup();
            tableGroupVendasView1.ReportItem = this.TextBoxVendasView1;
            this.crosstabVendasView.ColumnGroups.Add(tableGroupVendasView1);
            this.TextBoxVendasView7 = new Telerik.Reporting.TextBox();
            this.TextBoxVendasView7.Name = "TextBoxVendasView7";
            this.TextBoxVendasView7.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.2, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.TextBoxVendasView7.Style.Font.Bold = false;
            this.TextBoxVendasView7.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
            this.TextBoxVendasView7.Value = "=Fields.Data";
            this.crosstabVendasView.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Cm(2D)));
            this.crosstabVendasView.Body.SetCellContent(0, 0, this.TextBoxVendasView7);
            this.TextBoxVendasView2 = new Telerik.Reporting.TextBox();
            this.TextBoxVendasView2.Name = "TextBoxVendasView2";
            this.TextBoxVendasView2.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(4.8, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.TextBoxVendasView2.Style.Font.Bold = true;
            this.TextBoxVendasView2.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
            this.TextBoxVendasView2.Value = "Nome";
            var tableGroupVendasView2 = new Telerik.Reporting.TableGroup();
            tableGroupVendasView2.ReportItem = this.TextBoxVendasView2;
            this.crosstabVendasView.ColumnGroups.Add(tableGroupVendasView2);
            this.TextBoxVendasView8 = new Telerik.Reporting.TextBox();
            this.TextBoxVendasView8.Name = "TextBoxVendasView8";
            this.TextBoxVendasView8.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(4.8, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.TextBoxVendasView8.Style.Font.Bold = false;
            this.TextBoxVendasView8.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
            this.TextBoxVendasView8.Value = "=Fields.Nome";
            this.crosstabVendasView.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Cm(2D)));
            this.crosstabVendasView.Body.SetCellContent(0, 1, this.TextBoxVendasView8);
            this.TextBoxVendasView3 = new Telerik.Reporting.TextBox();
            this.TextBoxVendasView3.Name = "TextBoxVendasView3";
            this.TextBoxVendasView3.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.44, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.TextBoxVendasView3.Style.Font.Bold = true;
            this.TextBoxVendasView3.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
            this.TextBoxVendasView3.Value = "Origem";
            var tableGroupVendasView3 = new Telerik.Reporting.TableGroup();
            tableGroupVendasView3.ReportItem = this.TextBoxVendasView3;
            this.crosstabVendasView.ColumnGroups.Add(tableGroupVendasView3);
            this.TextBoxVendasView9 = new Telerik.Reporting.TextBox();
            this.TextBoxVendasView9.Name = "TextBoxVendasView9";
            this.TextBoxVendasView9.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.44, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.TextBoxVendasView9.Style.Font.Bold = false;
            this.TextBoxVendasView9.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
            this.TextBoxVendasView9.Value = "=Fields.Origem";
            this.crosstabVendasView.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Cm(2D)));
            this.crosstabVendasView.Body.SetCellContent(0, 2, this.TextBoxVendasView9);
            this.TextBoxVendasView4 = new Telerik.Reporting.TextBox();
            this.TextBoxVendasView4.Name = "TextBoxVendasView4";
            this.TextBoxVendasView4.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.TextBoxVendasView4.Style.Font.Bold = true;
            this.TextBoxVendasView4.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
            this.TextBoxVendasView4.Value = "ValorTotal";
            var tableGroupVendasView4 = new Telerik.Reporting.TableGroup();
            tableGroupVendasView4.ReportItem = this.TextBoxVendasView4;
            this.crosstabVendasView.ColumnGroups.Add(tableGroupVendasView4);
            this.TextBoxVendasView10 = new Telerik.Reporting.TextBox();
            this.TextBoxVendasView10.Name = "TextBoxVendasView10";
            this.TextBoxVendasView10.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.TextBoxVendasView10.Style.Font.Bold = false;
            this.TextBoxVendasView10.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
            this.TextBoxVendasView10.Value = "=Fields.ValorTotal";
            this.crosstabVendasView.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Cm(2D)));
            this.crosstabVendasView.Body.SetCellContent(0, 3, this.TextBoxVendasView10);
            this.TextBoxVendasView5 = new Telerik.Reporting.TextBox();
            this.TextBoxVendasView5.Name = "TextBoxVendasView5";
            this.TextBoxVendasView5.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.6, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.TextBoxVendasView5.Style.Font.Bold = true;
            this.TextBoxVendasView5.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
            this.TextBoxVendasView5.Value = "VendaVip";
            var tableGroupVendasView5 = new Telerik.Reporting.TableGroup();
            tableGroupVendasView5.ReportItem = this.TextBoxVendasView5;
            this.crosstabVendasView.ColumnGroups.Add(tableGroupVendasView5);
            this.TextBoxVendasView11 = new Telerik.Reporting.TextBox();
            this.TextBoxVendasView11.Name = "TextBoxVendasView11";
            this.TextBoxVendasView11.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.6, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.TextBoxVendasView11.Style.Font.Bold = false;
            this.TextBoxVendasView11.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
            this.TextBoxVendasView11.Value = "=Fields.VendaVip";
            this.crosstabVendasView.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Cm(2D)));
            this.crosstabVendasView.Body.SetCellContent(0, 4, this.TextBoxVendasView11);
            this.crosstabVendasView.DataSource = this.ReportDS;
            this.crosstabVendasView.Name = "crosstabVendasView";
            this.crosstabVendasView.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(12D), Telerik.Reporting.Drawing.Unit.Cm(2D));
            this.crosstabVendasView.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {TextBoxVendasView1,TextBoxVendasView7,TextBoxVendasView2,TextBoxVendasView8,TextBoxVendasView3,TextBoxVendasView9,TextBoxVendasView4,TextBoxVendasView10,TextBoxVendasView5,TextBoxVendasView11 });
            this.ReportDS = new Telerik.Reporting.ObjectDataSource();
            this.ReportDS.DataMember = "GetVendasView";
            this.ReportDS.DataSource = typeof(LinxTraining001.BV.Reports.DetalhamentoVendaDataSource);
            this.ReportDS.Name = "ReportDS";
this.ReportDS.Parameters.AddRange(new Telerik.Reporting.ObjectDataSourceParameter[] { new Telerik.Reporting.ObjectDataSourceParameter("reportItem", typeof(System.Object), "= ReportItem")});
            this.DataSource = this.ReportDS;
            this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] { this.TextBoxPageCount, this.TextBoxFilter });
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] { this.detailSection1, this.pageHeader, this.pageFooter });
            // PageSettings
            this.Width = new Telerik.Reporting.Drawing.Unit(29D, Telerik.Reporting.Drawing.UnitType.Cm);
            this.TextBoxDateTime.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(25D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.099999949336051941D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.TextBoxPageCount.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(25.5D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.PageSettings.Margins.Bottom = new Telerik.Reporting.Drawing.Unit(0.5D, Telerik.Reporting.Drawing.UnitType.Cm);
            this.PageSettings.Margins.Left = new Telerik.Reporting.Drawing.Unit(0.5D, Telerik.Reporting.Drawing.UnitType.Cm);
            this.PageSettings.Margins.Right = new Telerik.Reporting.Drawing.Unit(0.5D, Telerik.Reporting.Drawing.UnitType.Cm);
            this.PageSettings.Margins.Top = new Telerik.Reporting.Drawing.Unit(0.5D, Telerik.Reporting.Drawing.UnitType.Cm);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.PageSettings.Landscape = true;
            // EndInit
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
        }
    }
}

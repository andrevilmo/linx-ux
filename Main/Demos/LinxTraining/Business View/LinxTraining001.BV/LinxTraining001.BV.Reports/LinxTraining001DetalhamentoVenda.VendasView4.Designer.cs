namespace LinxTraining001.BV.Reports {
    
    
    public partial class LinxTraining001DetalhamentoVendaVendasView4 {
        
        private Telerik.Reporting.DetailSection detailSection1;
        
        private Telerik.Reporting.PageHeaderSection pageHeader;
        
        private Telerik.Reporting.PageFooterSection pageFooter;
        
        private Telerik.Reporting.PictureBox PictureBox1;
        
        private Telerik.Reporting.TextBox TextBoxCompanyName;
        
        private Telerik.Reporting.TextBox TextBoxHeader;
        
        private Telerik.Reporting.TextBox TextBoxDateTime;
        
        private Telerik.Reporting.TextBox TextBoxPageCount;
        
        private Telerik.Reporting.TextBox TextBoxFilter;
        
        private Telerik.Reporting.SubReport subReportVendaDetalheView;
        
        private LinxTraining001DetalhamentoVendaVendasView4DetailVendaDetalheView VendaDetalheViewChild1;
        
        private Telerik.Reporting.ObjectDataSource ReportDS;
        
        private Telerik.Reporting.TextBox TextBox1;
        
        private Telerik.Reporting.TextBox TextBox2;
        
        private Telerik.Reporting.TextBox TextBox3;
        
        private Telerik.Reporting.TextBox TextBox4;
        
        private Telerik.Reporting.TextBox TextBox5;
        
        private Telerik.Reporting.TextBox TextBox6;
        
        private Telerik.Reporting.TextBox TextBox7;
        
        private Telerik.Reporting.TextBox TextBox8;
        
        private Telerik.Reporting.TextBox TextBox9;
        
        private Telerik.Reporting.TextBox TextBox10;
        
        private void InitializeComponent() {
            // Sections
this.detailSection1 = new Telerik.Reporting.DetailSection();
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
this.TextBoxHeader.Value = "Relat padrão pai e filha";
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
this.subReportVendaDetalheView = new Telerik.Reporting.SubReport();
            this.subReportVendaDetalheView.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.1D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.1D, Telerik.Reporting.Drawing.UnitType.Cm));
this.subReportVendaDetalheView.Name = "subReportVendaDetalheView";
this.subReportVendaDetalheView.ReportSource = this.VendaDetalheViewChild1;
this.VendaDetalheViewChild1 = new LinxTraining001.BV.Reports.LinxTraining001DetalhamentoVendaVendasView4DetailVendaDetalheView();
((System.ComponentModel.ISupportInitialize)(this.VendaDetalheViewChild1)).BeginInit();
this.ReportDS = new Telerik.Reporting.ObjectDataSource();
this.ReportDS.DataMember = "GetVendasView";
this.ReportDS.DataSource = typeof(LinxTraining001.BV.Reports.DetalhamentoVendaDataSource);
this.ReportDS.Name = "ReportDS";
this.ReportDS.Parameters.AddRange(new Telerik.Reporting.ObjectDataSourceParameter[] { new Telerik.Reporting.ObjectDataSourceParameter("reportItem", typeof(System.Object), "= ReportItem")});
this.DataSource = this.ReportDS;
this.TextBox1 = new Telerik.Reporting.TextBox();
this.TextBox2 = new Telerik.Reporting.TextBox();
this.TextBox3 = new Telerik.Reporting.TextBox();
this.TextBox4 = new Telerik.Reporting.TextBox();
this.TextBox5 = new Telerik.Reporting.TextBox();
this.TextBox6 = new Telerik.Reporting.TextBox();
this.TextBox7 = new Telerik.Reporting.TextBox();
this.TextBox8 = new Telerik.Reporting.TextBox();
this.TextBox9 = new Telerik.Reporting.TextBox();
this.TextBox10 = new Telerik.Reporting.TextBox();
this.Style.BackgroundColor = System.Drawing.Color.White;
this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {this.detailSection1, this.pageHeader, this.pageFooter});
            // BeginInit
((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
this.detailSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(2.00D);
this.detailSection1.KeepTogether = true;
this.detailSection1.Name = "detailSection1";
this.TextBox1.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.5D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox1.Name = "TextBox1";
this.TextBox1.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.2, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox1.Style.Font.Bold = true;
this.TextBox1.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
this.TextBox1.Value = "Data";
this.TextBox2.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox2.Name = "TextBox2";
this.TextBox2.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.2, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox2.Style.Font.Bold = false;
this.TextBox2.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
this.TextBox2.Value = "=Fields.Data";
this.TextBox3.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(1.3, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.5D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox3.Name = "TextBox3";
this.TextBox3.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(4.8, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox3.Style.Font.Bold = true;
this.TextBox3.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
this.TextBox3.Value = "Nome";
this.TextBox4.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(1.3, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox4.Name = "TextBox4";
this.TextBox4.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(4.8, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox4.Style.Font.Bold = false;
this.TextBox4.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
this.TextBox4.Value = "=Fields.Nome";
this.TextBox5.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(6.2, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.5D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox5.Name = "TextBox5";
this.TextBox5.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.44, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox5.Style.Font.Bold = true;
this.TextBox5.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
this.TextBox5.Value = "Origem";
this.TextBox6.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(6.2, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox6.Name = "TextBox6";
this.TextBox6.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.44, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox6.Style.Font.Bold = false;
this.TextBox6.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
this.TextBox6.Value = "=Fields.Origem";
this.TextBox7.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(7.74, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.5D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox7.Name = "TextBox7";
this.TextBox7.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox7.Style.Font.Bold = true;
this.TextBox7.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
this.TextBox7.Value = "ValorTotal";
this.TextBox8.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(7.74, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox8.Name = "TextBox8";
this.TextBox8.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox8.Style.Font.Bold = false;
this.TextBox8.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
this.TextBox8.Value = "=Fields.ValorTotal";
this.TextBox9.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(9.84, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.5D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox9.Name = "TextBox9";
this.TextBox9.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.6, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox9.Style.Font.Bold = true;
this.TextBox9.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
this.TextBox9.Value = "VendaVip";
this.TextBox10.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(9.84, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox10.Name = "TextBox10";
this.TextBox10.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.6, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox10.Style.Font.Bold = false;
this.TextBox10.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
this.TextBox10.Value = "=Fields.VendaVip";
this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] { this.TextBoxHeader, this.TextBoxDateTime, this.PictureBox1, this.TextBoxCompanyName,TextBox1,TextBox3,TextBox5,TextBox7,TextBox9 });
this.detailSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] { this.subReportVendaDetalheView,TextBox2,TextBox4,TextBox6,TextBox8,TextBox10 });
this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] { this.TextBoxPageCount, this.TextBoxFilter });
this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] { this.detailSection1, this.pageHeader, this.pageFooter});
            // PageSettings
this.PageSettings.Landscape = false;
this.Width = new Telerik.Reporting.Drawing.Unit(20D, Telerik.Reporting.Drawing.UnitType.Cm);
this.TextBoxDateTime.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(16.5D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.099999949336051941D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBoxPageCount.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(17D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
            this.subReportVendaDetalheView.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(19.50D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.95D, Telerik.Reporting.Drawing.UnitType.Cm));
this.PageSettings.Margins.Bottom = new Telerik.Reporting.Drawing.Unit(0.5D, Telerik.Reporting.Drawing.UnitType.Cm);
this.PageSettings.Margins.Left = new Telerik.Reporting.Drawing.Unit(0.5D, Telerik.Reporting.Drawing.UnitType.Cm);
this.PageSettings.Margins.Right = new Telerik.Reporting.Drawing.Unit(0.5D, Telerik.Reporting.Drawing.UnitType.Cm);
this.PageSettings.Margins.Top = new Telerik.Reporting.Drawing.Unit(0.5D, Telerik.Reporting.Drawing.UnitType.Cm);
this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
this.Style.BackgroundColor = System.Drawing.Color.White;
            // EndInit
((System.ComponentModel.ISupportInitialize)(this.VendaDetalheViewChild1)).EndInit();
((System.ComponentModel.ISupportInitialize)(this)).EndInit();
        }
    }
}

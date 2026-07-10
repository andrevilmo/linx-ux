namespace LinxTraining001.BV.Reports {
    
    
    public partial class LinxTraining001DetalhamentoVendaVendasView17 {
        
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
        
        private LinxTraining001DetalhamentoVendaVendasView17DetailVendaDetalheView VendaDetalheViewChild1;
        
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
            Telerik.Reporting.InstanceReportSource instanceReportSource1 = new Telerik.Reporting.InstanceReportSource();
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter2 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter3 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter4 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter5 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter6 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter7 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter8 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter9 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter10 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter11 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter12 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter13 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter14 = new Telerik.Reporting.ReportParameter();
            this.detailSection1 = new Telerik.Reporting.DetailSection();
            this.subReportVendaDetalheView = new Telerik.Reporting.SubReport();
            this.VendaDetalheViewChild1 = new LinxTraining001.BV.Reports.LinxTraining001DetalhamentoVendaVendasView17DetailVendaDetalheView();
            this.TextBox2 = new Telerik.Reporting.TextBox();
            this.TextBox4 = new Telerik.Reporting.TextBox();
            this.TextBox6 = new Telerik.Reporting.TextBox();
            this.TextBox8 = new Telerik.Reporting.TextBox();
            this.TextBox10 = new Telerik.Reporting.TextBox();
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.TextBoxHeader = new Telerik.Reporting.TextBox();
            this.TextBoxDateTime = new Telerik.Reporting.TextBox();
            this.PictureBox1 = new Telerik.Reporting.PictureBox();
            this.TextBoxCompanyName = new Telerik.Reporting.TextBox();
            this.TextBox1 = new Telerik.Reporting.TextBox();
            this.TextBox3 = new Telerik.Reporting.TextBox();
            this.TextBox5 = new Telerik.Reporting.TextBox();
            this.TextBox7 = new Telerik.Reporting.TextBox();
            this.TextBox9 = new Telerik.Reporting.TextBox();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.TextBoxPageCount = new Telerik.Reporting.TextBox();
            this.TextBoxFilter = new Telerik.Reporting.TextBox();
            this.ReportDS = new Telerik.Reporting.ObjectDataSource();
            ((System.ComponentModel.ISupportInitialize)(this.VendaDetalheViewChild1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // detailSection1
            // 
            this.detailSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(2.0499999523162842D);
            this.detailSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.subReportVendaDetalheView,
            this.TextBox2,
            this.TextBox4,
            this.TextBox6,
            this.TextBox8,
            this.TextBox10});
            this.detailSection1.KeepTogether = true;
            this.detailSection1.Name = "detailSection1";
            // 
            // subReportVendaDetalheView
            // 
            this.subReportVendaDetalheView.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612D), Telerik.Reporting.Drawing.Unit.Cm(1.1000000238418579D));
            this.subReportVendaDetalheView.Name = "subReportVendaDetalheView";
            instanceReportSource1.ReportDocument = this.VendaDetalheViewChild1;
            this.subReportVendaDetalheView.ReportSource = instanceReportSource1;
            this.subReportVendaDetalheView.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(19.5D), Telerik.Reporting.Drawing.Unit.Cm(0.949999988079071D));
            // 
            // VendaDetalheViewChild1
            // 
            this.VendaDetalheViewChild1.Name = "VendaDetalheViewChild1";
            // 
            // TextBox2
            // 
            this.TextBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TextBox2.Name = "TextBox2";
            this.TextBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.2000000476837158D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox2.Style.Font.Bold = false;
            this.TextBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox2.Value = "=Fields.Data";
            // 
            // TextBox4
            // 
            this.TextBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.2999999523162842D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TextBox4.Name = "TextBox4";
            this.TextBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.8000001907348633D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox4.Style.Font.Bold = false;
            this.TextBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox4.Value = "=Fields.Nome";
            // 
            // TextBox6
            // 
            this.TextBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(6.1999998092651367D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TextBox6.Name = "TextBox6";
            this.TextBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.440000057220459D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox6.Style.Font.Bold = false;
            this.TextBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox6.Value = "=Fields.Origem";
            // 
            // TextBox8
            // 
            this.TextBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(7.7399997711181641D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TextBox8.Name = "TextBox8";
            this.TextBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox8.Style.Font.Bold = false;
            this.TextBox8.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox8.Value = "=Fields.ValorTotal";
            // 
            // TextBox10
            // 
            this.TextBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(9.84000015258789D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TextBox10.Name = "TextBox10";
            this.TextBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.6000000238418579D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox10.Style.Font.Bold = false;
            this.TextBox10.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox10.Value = "=Fields.VendaVip";
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Cm(2D);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.TextBoxHeader,
            this.TextBoxDateTime,
            this.PictureBox1,
            this.TextBoxCompanyName,
            this.TextBox1,
            this.TextBox3,
            this.TextBox5,
            this.TextBox7,
            this.TextBox9});
            this.pageHeader.Name = "pageHeader";
            this.pageHeader.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.pageHeader.Style.Padding.Bottom = Telerik.Reporting.Drawing.Unit.Cm(0D);
            // 
            // TextBoxHeader
            // 
            this.TextBoxHeader.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.7000000476837158D), Telerik.Reporting.Drawing.Unit.Cm(0.89999997615814209D));
            this.TextBoxHeader.Name = "TextBoxHeader";
            this.TextBoxHeader.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(12.800000190734863D), Telerik.Reporting.Drawing.Unit.Cm(0.50000017881393433D));
            this.TextBoxHeader.Style.Font.Bold = true;
            this.TextBoxHeader.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(14D);
            this.TextBoxHeader.Value = "Relatorio teste Matheus";
            // 
            // TextBoxDateTime
            // 
            this.TextBoxDateTime.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(16.5D), Telerik.Reporting.Drawing.Unit.Cm(0.099999949336051941D));
            this.TextBoxDateTime.Name = "TextBoxDateTime";
            this.TextBoxDateTime.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.5000002384185791D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBoxDateTime.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.TextBoxDateTime.Value = "= Now()";
            // 
            // PictureBox1
            // 
            this.PictureBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.099999949336051941D), Telerik.Reporting.Drawing.Unit.Cm(0.099999949336051941D));
            this.PictureBox1.MimeType = "";
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.5D), Telerik.Reporting.Drawing.Unit.Cm(1.4000000953674316D));
            this.PictureBox1.Value = "= Parameters.CompanyLogo.Value";
            // 
            // TextBoxCompanyName
            // 
            this.TextBoxCompanyName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.5999999046325684D), Telerik.Reporting.Drawing.Unit.Cm(0.20000000298023224D));
            this.TextBoxCompanyName.Name = "TextBoxCompanyName";
            this.TextBoxCompanyName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(9.1000003814697266D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.TextBoxCompanyName.Style.Font.Bold = true;
            this.TextBoxCompanyName.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(14D);
            this.TextBoxCompanyName.Value = "= Parameters.CompanyName.Value";
            // 
            // TextBox1
            // 
            this.TextBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(1.5D));
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.2000000476837158D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox1.Style.Font.Bold = true;
            this.TextBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox1.Value = "Data";
            // 
            // TextBox3
            // 
            this.TextBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.2999999523162842D), Telerik.Reporting.Drawing.Unit.Cm(1.5D));
            this.TextBox3.Name = "TextBox3";
            this.TextBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.8000001907348633D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox3.Style.Font.Bold = true;
            this.TextBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox3.Value = "Nome";
            // 
            // TextBox5
            // 
            this.TextBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(6.1999998092651367D), Telerik.Reporting.Drawing.Unit.Cm(1.5D));
            this.TextBox5.Name = "TextBox5";
            this.TextBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.440000057220459D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox5.Style.Font.Bold = true;
            this.TextBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox5.Value = "Origem";
            // 
            // TextBox7
            // 
            this.TextBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(7.7399997711181641D), Telerik.Reporting.Drawing.Unit.Cm(1.5D));
            this.TextBox7.Name = "TextBox7";
            this.TextBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox7.Style.Font.Bold = true;
            this.TextBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox7.Value = "ValorTotal";
            // 
            // TextBox9
            // 
            this.TextBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(9.84000015258789D), Telerik.Reporting.Drawing.Unit.Cm(1.5D));
            this.TextBox9.Name = "TextBox9";
            this.TextBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.6000000238418579D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox9.Style.Font.Bold = true;
            this.TextBox9.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox9.Value = "VendaVip";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Cm(0.70000004768371582D);
            this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.TextBoxPageCount,
            this.TextBoxFilter});
            this.pageFooter.Name = "pageFooter";
            this.pageFooter.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            // 
            // TextBoxPageCount
            // 
            this.TextBoxPageCount.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(17D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TextBoxPageCount.Name = "TextBoxPageCount";
            this.TextBoxPageCount.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBoxPageCount.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.TextBoxPageCount.Value = "= \"Pg :\" + PageNumber.ToString() + \"/\" + PageCount.ToString()";
            // 
            // TextBoxFilter
            // 
            this.TextBoxFilter.Name = "TextBoxFilter";
            this.TextBoxFilter.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(15.5D), Telerik.Reporting.Drawing.Unit.Cm(0.4999997615814209D));
            this.TextBoxFilter.Style.Font.Bold = false;
            this.TextBoxFilter.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.TextBoxFilter.Value = "=Parameters.TranslatedJqueryExpression.Value";
            // 
            // ReportDS
            // 
            this.ReportDS.DataMember = "GetVendasView";
            this.ReportDS.DataSource = typeof(LinxTraining001.BV.Reports.DetalhamentoVendaDataSource);
            this.ReportDS.Name = "ReportDS";
            this.ReportDS.Parameters.AddRange(new Telerik.Reporting.ObjectDataSourceParameter[] {
            new Telerik.Reporting.ObjectDataSourceParameter("reportItem", typeof(object), "= ReportItem")});
            // 
            // LinxTraining001DetalhamentoVendaVendasView17
            // 
            this.DataSource = this.ReportDS;
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.detailSection1,
            this.pageHeader,
            this.pageFooter});
            this.Name = "LinxTraining001DetalhamentoVendaVendasView17";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.Name = "EntitySearchId";
            reportParameter1.Value = "";
            reportParameter2.Name = "CurrentUser";
            reportParameter2.Value = "";
            reportParameter3.Name = "CurrentCompany";
            reportParameter3.Value = "";
            reportParameter4.Name = "AuthorizationToken";
            reportParameter4.Value = "";
            reportParameter5.Name = "TransactionInfo";
            reportParameter5.Value = "";
            reportParameter6.Name = "CompanyLogo";
            reportParameter6.Value = " ";
            reportParameter7.Name = "CompanyName";
            reportParameter7.Value = " ";
            reportParameter8.Name = "AccessGroup";
            reportParameter8.Value = " ";
            reportParameter9.Name = "Application";
            reportParameter9.Value = " ";
            reportParameter10.Name = "EconomicGroup";
            reportParameter10.Value = " ";
            reportParameter11.Name = "Environment";
            reportParameter11.Value = " ";
            reportParameter12.Name = "JqueryExpression";
            reportParameter12.Value = " ";
            reportParameter13.Name = "CurrentUserName";
            reportParameter13.Value = "";
            reportParameter14.Name = "TranslatedJqueryExpression";
            reportParameter14.Value = " ";
            this.ReportParameters.Add(reportParameter1);
            this.ReportParameters.Add(reportParameter2);
            this.ReportParameters.Add(reportParameter3);
            this.ReportParameters.Add(reportParameter4);
            this.ReportParameters.Add(reportParameter5);
            this.ReportParameters.Add(reportParameter6);
            this.ReportParameters.Add(reportParameter7);
            this.ReportParameters.Add(reportParameter8);
            this.ReportParameters.Add(reportParameter9);
            this.ReportParameters.Add(reportParameter10);
            this.ReportParameters.Add(reportParameter11);
            this.ReportParameters.Add(reportParameter12);
            this.ReportParameters.Add(reportParameter13);
            this.ReportParameters.Add(reportParameter14);
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(20D);
            ((System.ComponentModel.ISupportInitialize)(this.VendaDetalheViewChild1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
    }
}

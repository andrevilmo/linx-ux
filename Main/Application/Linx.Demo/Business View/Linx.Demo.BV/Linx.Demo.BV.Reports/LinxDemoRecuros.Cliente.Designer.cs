namespace Linx.Demo.BV.Reports {
    
    
    public partial class LinxDemoRecurosCliente {
        
        private Telerik.Reporting.DetailSection detailSection1;
        
        private Telerik.Reporting.PageHeaderSection pageHeader;
        
        private Telerik.Reporting.PageFooterSection pageFooter;
        
        private Telerik.Reporting.PictureBox PictureBox1;
        
        private Telerik.Reporting.TextBox TextBoxCompanyName;
        
        private Telerik.Reporting.TextBox TextBoxHeader;
        
        private Telerik.Reporting.TextBox TextBoxDateTime;
        
        private Telerik.Reporting.TextBox TextBoxFilter;
        
        private Telerik.Reporting.TextBox TextBoxPageCount;
        
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
        
        private Telerik.Reporting.TextBox TextBox11;
        
        private Telerik.Reporting.TextBox TextBox12;
        
        private Telerik.Reporting.TextBox TextBox13;
        
        private Telerik.Reporting.TextBox TextBox14;
        
        private Telerik.Reporting.TextBox TextBox15;
        
        private Telerik.Reporting.TextBox TextBox16;
        
        private Telerik.Reporting.TextBox TextBox17;
        
        private Telerik.Reporting.TextBox TextBox18;
        
        private Telerik.Reporting.TextBox TextBox19;
        
        private Telerik.Reporting.TextBox TextBox20;
        
        private Telerik.Reporting.TextBox TextBox21;
        
        private Telerik.Reporting.TextBox TextBox22;
        
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
this.TextBoxHeader.Value = "Teste Relatorio Nome muito Grande frescura Frescura do C$#&*#@";
            // TextBoxDateTime
this.TextBoxDateTime = new Telerik.Reporting.TextBox();
this.TextBoxDateTime.Name = "TextBoxDateTime";
this.TextBoxDateTime.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.5000002384185791D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBoxDateTime.Value = "= Now()";
this.TextBoxDateTime.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            // TextBoxFilter
            this.TextBoxFilter = new Telerik.Reporting.TextBox();
            this.TextBoxFilter.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(15.799997329711914D), Telerik.Reporting.Drawing.Unit.Cm(0.90000033378601074D));
            this.TextBoxFilter.Name = "TextBoxFilter";
            this.TextBoxFilter.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(15.5D), Telerik.Reporting.Drawing.Unit.Cm(0.4999997615814209D));
            this.TextBoxFilter.Style.Font.Bold = false;
            this.TextBoxFilter.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
             this.TextBoxFilter.Value = "=Parameters.TranslatedJqueryExpression.Value";
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
           Telerik.Reporting.ReportParameter reportParameter15 = new Telerik.Reporting.ReportParameter();
           reportParameter15.Name = "Branch";
           reportParameter15.Value = " ";
           this.ReportParameters.Add(reportParameter15);
           Telerik.Reporting.ReportParameter reportParameter16 = new Telerik.Reporting.ReportParameter();
           reportParameter16.Name = "LoginMode";
           reportParameter16.Value = " ";
           this.ReportParameters.Add(reportParameter16);
this.ReportDS = new Telerik.Reporting.ObjectDataSource();
this.ReportDS.DataMember = "GetCliente";
this.ReportDS.DataSource = typeof(Linx.Demo.BV.Reports.RecurosDataSource);
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
this.TextBox11 = new Telerik.Reporting.TextBox();
this.TextBox12 = new Telerik.Reporting.TextBox();
this.TextBox13 = new Telerik.Reporting.TextBox();
this.TextBox14 = new Telerik.Reporting.TextBox();
this.TextBox15 = new Telerik.Reporting.TextBox();
this.TextBox16 = new Telerik.Reporting.TextBox();
this.TextBox17 = new Telerik.Reporting.TextBox();
this.TextBox18 = new Telerik.Reporting.TextBox();
this.TextBox19 = new Telerik.Reporting.TextBox();
this.TextBox20 = new Telerik.Reporting.TextBox();
this.TextBox21 = new Telerik.Reporting.TextBox();
this.TextBox22 = new Telerik.Reporting.TextBox();
this.Style.BackgroundColor = System.Drawing.Color.White;
this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {this.detailSection1, this.pageHeader, this.pageFooter});
            // BeginInit
((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
this.detailSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(0.67D);
this.detailSection1.KeepTogether = true;
this.detailSection1.Name = "detailSection1";
this.TextBox1.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.5D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox1.Name = "TextBox1";
this.TextBox1.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.88, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox1.Style.Font.Bold = true;
this.TextBox1.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
this.TextBox1.Value = "1";
this.TextBox2.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox2.Name = "TextBox2";
this.TextBox2.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.88, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox2.Style.Font.Bold = false;
this.TextBox2.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
this.TextBox2.Value = "=Fields.BigIntCliente";
this.TextBox3.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(2.98, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.5D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox3.Name = "TextBox3";
this.TextBox3.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.2, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox3.Style.Font.Bold = true;
this.TextBox3.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
this.TextBox3.Value = "2";
this.TextBox4.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(2.98, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox4.Name = "TextBox4";
this.TextBox4.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.2, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox4.Style.Font.Bold = false;
this.TextBox4.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
this.TextBox4.Value = "=Fields.BitCliente";
this.TextBox5.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(3.28, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.5D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox5.Name = "TextBox5";
this.TextBox5.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.36, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox5.Style.Font.Bold = true;
this.TextBox5.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
this.TextBox5.Value = "3";
this.TextBox6.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(3.28, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox6.Name = "TextBox6";
this.TextBox6.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.36, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox6.Style.Font.Bold = false;
this.TextBox6.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
this.TextBox6.Value = "=Fields.ComboboxCliente";
this.TextBox7.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(3.74, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.5D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox7.Name = "TextBox7";
this.TextBox7.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.2, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox7.Style.Font.Bold = true;
this.TextBox7.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
this.TextBox7.Value = "4";
this.TextBox8.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(3.74, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox8.Name = "TextBox8";
this.TextBox8.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.2, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox8.Style.Font.Bold = false;
this.TextBox8.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
this.TextBox8.Value = "=Fields.DatetimeCliente";
this.TextBox9.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(5.04, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.5D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox9.Name = "TextBox9";
this.TextBox9.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.584, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox9.Style.Font.Bold = true;
this.TextBox9.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
this.TextBox9.Value = "5";
this.TextBox10.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(5.04, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox10.Name = "TextBox10";
this.TextBox10.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.584, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox10.Style.Font.Bold = false;
this.TextBox10.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
this.TextBox10.Value = "=Fields.DecimalCliente";
this.TextBox11.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(6.724, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.5D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox11.Name = "TextBox11";
this.TextBox11.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.44, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox11.Style.Font.Bold = true;
this.TextBox11.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
this.TextBox11.Value = "6";
this.TextBox12.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(6.724, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox12.Name = "TextBox12";
this.TextBox12.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.44, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox12.Style.Font.Bold = false;
this.TextBox12.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
this.TextBox12.Value = "=Fields.IdCliente";
this.TextBox13.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(8.264, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.5D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox13.Name = "TextBox13";
this.TextBox13.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.44, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox13.Style.Font.Bold = true;
this.TextBox13.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
this.TextBox13.Value = "7";
this.TextBox14.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(8.264, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox14.Name = "TextBox14";
this.TextBox14.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.44, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox14.Style.Font.Bold = false;
this.TextBox14.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
this.TextBox14.Value = "=Fields.IdEstado";
this.TextBox15.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(9.804, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.5D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox15.Name = "TextBox15";
this.TextBox15.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.44, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox15.Style.Font.Bold = true;
this.TextBox15.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
this.TextBox15.Value = "8";
this.TextBox16.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(9.804, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox16.Name = "TextBox16";
this.TextBox16.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.44, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox16.Style.Font.Bold = false;
this.TextBox16.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
this.TextBox16.Value = "=Fields.IntCliente";
this.TextBox17.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(11.344, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.5D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox17.Name = "TextBox17";
this.TextBox17.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.72, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox17.Style.Font.Bold = true;
this.TextBox17.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
this.TextBox17.Value = "9";
this.TextBox18.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(11.344, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox18.Name = "TextBox18";
this.TextBox18.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.72, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox18.Style.Font.Bold = false;
this.TextBox18.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
this.TextBox18.Value = "=Fields.SmallIntCliente";
this.TextBox19.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(12.164, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.5D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox19.Name = "TextBox19";
this.TextBox19.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(6, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox19.Style.Font.Bold = true;
this.TextBox19.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
this.TextBox19.Value = "10";
this.TextBox20.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(12.164, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox20.Name = "TextBox20";
this.TextBox20.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(6, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox20.Style.Font.Bold = false;
this.TextBox20.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
this.TextBox20.Value = "=Fields.StringCliente";
this.TextBox21.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(18.264, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.5D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox21.Name = "TextBox21";
this.TextBox21.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(6, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox21.Style.Font.Bold = true;
this.TextBox21.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
this.TextBox21.Value = "11";
this.TextBox22.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(18.264, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox22.Name = "TextBox22";
this.TextBox22.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(6, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox22.Style.Font.Bold = false;
this.TextBox22.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
this.TextBox22.Value = "=Fields.StringEstado";
this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] { this.TextBoxHeader, this.TextBoxDateTime, this.PictureBox1, this.TextBoxCompanyName, this.TextBoxFilter,TextBox1,TextBox3,TextBox5,TextBox7,TextBox9,TextBox11,TextBox13,TextBox15,TextBox17,TextBox19,TextBox21 });
this.detailSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] { TextBox2,TextBox4,TextBox6,TextBox8,TextBox10,TextBox12,TextBox14,TextBox16,TextBox18,TextBox20,TextBox22 });
this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] { this.TextBoxPageCount });
this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] { this.detailSection1, this.pageHeader, this.pageFooter});
            // PageSettings
this.PageSettings.Landscape = true;
this.Width = new Telerik.Reporting.Drawing.Unit(31.299997329711914D, Telerik.Reporting.Drawing.UnitType.Cm);
this.TextBoxDateTime.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(25D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.099999949336051941D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBoxPageCount.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(25.5D, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
this.PageSettings.Margins.Bottom = new Telerik.Reporting.Drawing.Unit(0.5D, Telerik.Reporting.Drawing.UnitType.Cm);
this.PageSettings.Margins.Left = new Telerik.Reporting.Drawing.Unit(0.5D, Telerik.Reporting.Drawing.UnitType.Cm);
this.PageSettings.Margins.Right = new Telerik.Reporting.Drawing.Unit(0.5D, Telerik.Reporting.Drawing.UnitType.Cm);
this.PageSettings.Margins.Top = new Telerik.Reporting.Drawing.Unit(0.5D, Telerik.Reporting.Drawing.UnitType.Cm);
this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
this.Style.BackgroundColor = System.Drawing.Color.White;
            // EndInit
((System.ComponentModel.ISupportInitialize)(this)).EndInit();
        }
    }
}

namespace Linx.Demo.BV.Reports {
    
    
    public partial class LinxDemoPaiFilhaCliente1 {
        
        private Telerik.Reporting.DetailSection detailSection1;
        
        private Telerik.Reporting.PageHeaderSection pageHeader;
        
        private Telerik.Reporting.PageFooterSection pageFooter;
        
        private Telerik.Reporting.PictureBox PictureBox1;
        
        private Telerik.Reporting.TextBox TextBoxCompanyName;
        
        private Telerik.Reporting.TextBox TextBoxHeader;
        
        private Telerik.Reporting.TextBox TextBoxDateTime;
        
        private Telerik.Reporting.TextBox TextBoxFilter;
        
        private Telerik.Reporting.TextBox TextBoxPageCount;
        
        private Telerik.Reporting.SubReport subReportVenda;
        
        private LinxDemoPaiFilhaCliente1DetailVenda VendaChild1;
        
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
        
        private Telerik.Reporting.TextBox TextBox23;
        
        private Telerik.Reporting.TextBox TextBox24;
        
        private Telerik.Reporting.TextBox TextBox25;
        
        private Telerik.Reporting.TextBox TextBox26;
        
        private Telerik.Reporting.TextBox TextBox27;
        
        private Telerik.Reporting.TextBox TextBox28;
        
        private Telerik.Reporting.TextBox TextBox29;
        
        private Telerik.Reporting.TextBox TextBox30;
        
        private Telerik.Reporting.TextBox TextBox31;
        
        private Telerik.Reporting.TextBox TextBox32;
        
        private Telerik.Reporting.TextBox TextBox33;
        
        private Telerik.Reporting.TextBox TextBox34;
        
        private Telerik.Reporting.TextBox TextBox35;
        
        private Telerik.Reporting.TextBox TextBox36;
        
        private Telerik.Reporting.TextBox TextBox37;
        
        private Telerik.Reporting.TextBox TextBox38;
        
        private Telerik.Reporting.TextBox TextBox39;
        
        private Telerik.Reporting.TextBox TextBox40;
        
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
            Telerik.Reporting.ReportParameter reportParameter15 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter16 = new Telerik.Reporting.ReportParameter();
            this.detailSection1 = new Telerik.Reporting.DetailSection();
            this.subReportVenda = new Telerik.Reporting.SubReport();
            this.VendaChild1 = new Linx.Demo.BV.Reports.LinxDemoPaiFilhaCliente1DetailVenda();
            this.TextBox2 = new Telerik.Reporting.TextBox();
            this.TextBox4 = new Telerik.Reporting.TextBox();
            this.TextBox6 = new Telerik.Reporting.TextBox();
            this.TextBox8 = new Telerik.Reporting.TextBox();
            this.TextBox10 = new Telerik.Reporting.TextBox();
            this.TextBox12 = new Telerik.Reporting.TextBox();
            this.TextBox14 = new Telerik.Reporting.TextBox();
            this.TextBox16 = new Telerik.Reporting.TextBox();
            this.TextBox18 = new Telerik.Reporting.TextBox();
            this.TextBox20 = new Telerik.Reporting.TextBox();
            this.TextBox22 = new Telerik.Reporting.TextBox();
            this.TextBox24 = new Telerik.Reporting.TextBox();
            this.TextBox26 = new Telerik.Reporting.TextBox();
            this.TextBox28 = new Telerik.Reporting.TextBox();
            this.TextBox30 = new Telerik.Reporting.TextBox();
            this.TextBox32 = new Telerik.Reporting.TextBox();
            this.TextBox34 = new Telerik.Reporting.TextBox();
            this.TextBox36 = new Telerik.Reporting.TextBox();
            this.TextBox38 = new Telerik.Reporting.TextBox();
            this.TextBox40 = new Telerik.Reporting.TextBox();
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.TextBoxHeader = new Telerik.Reporting.TextBox();
            this.TextBoxDateTime = new Telerik.Reporting.TextBox();
            this.PictureBox1 = new Telerik.Reporting.PictureBox();
            this.TextBoxCompanyName = new Telerik.Reporting.TextBox();
            this.TextBoxFilter = new Telerik.Reporting.TextBox();
            this.TextBox1 = new Telerik.Reporting.TextBox();
            this.TextBox3 = new Telerik.Reporting.TextBox();
            this.TextBox5 = new Telerik.Reporting.TextBox();
            this.TextBox7 = new Telerik.Reporting.TextBox();
            this.TextBox9 = new Telerik.Reporting.TextBox();
            this.TextBox11 = new Telerik.Reporting.TextBox();
            this.TextBox13 = new Telerik.Reporting.TextBox();
            this.TextBox15 = new Telerik.Reporting.TextBox();
            this.TextBox17 = new Telerik.Reporting.TextBox();
            this.TextBox19 = new Telerik.Reporting.TextBox();
            this.TextBox21 = new Telerik.Reporting.TextBox();
            this.TextBox23 = new Telerik.Reporting.TextBox();
            this.TextBox25 = new Telerik.Reporting.TextBox();
            this.TextBox27 = new Telerik.Reporting.TextBox();
            this.TextBox29 = new Telerik.Reporting.TextBox();
            this.TextBox31 = new Telerik.Reporting.TextBox();
            this.TextBox33 = new Telerik.Reporting.TextBox();
            this.TextBox35 = new Telerik.Reporting.TextBox();
            this.TextBox37 = new Telerik.Reporting.TextBox();
            this.TextBox39 = new Telerik.Reporting.TextBox();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.TextBoxPageCount = new Telerik.Reporting.TextBox();
            this.ReportDS = new Telerik.Reporting.ObjectDataSource();
            ((System.ComponentModel.ISupportInitialize)(this.VendaChild1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // detailSection1
            // 
            this.detailSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(2D);
            this.detailSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.subReportVenda,
            this.TextBox2,
            this.TextBox4,
            this.TextBox6,
            this.TextBox8,
            this.TextBox10,
            this.TextBox12,
            this.TextBox14,
            this.TextBox16,
            this.TextBox18,
            this.TextBox20,
            this.TextBox22,
            this.TextBox24,
            this.TextBox26,
            this.TextBox28,
            this.TextBox30,
            this.TextBox32,
            this.TextBox34,
            this.TextBox36,
            this.TextBox38,
            this.TextBox40});
            this.detailSection1.KeepTogether = true;
            this.detailSection1.Name = "detailSection1";
            // 
            // subReportVenda
            // 
            this.subReportVenda.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612D), Telerik.Reporting.Drawing.Unit.Cm(1.1000000238418579D));
            this.subReportVenda.Name = "subReportVenda";
            instanceReportSource1.ReportDocument = this.VendaChild1;
            this.subReportVenda.ReportSource = instanceReportSource1;
            this.subReportVenda.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(28D), Telerik.Reporting.Drawing.Unit.Cm(0.949999988079071D));
            // 
            // VendaChild1
            // 
            this.VendaChild1.Name = "VendaChild1";
            // 
            // TextBox2
            // 
            this.TextBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TextBox2.Name = "TextBox2";
            this.TextBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox2.Style.Font.Bold = false;
            this.TextBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox2.Value = "=Fields.BooleanCliente";
            // 
            // TextBox4
            // 
            this.TextBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.0999999046325684D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TextBox4.Name = "TextBox4";
            this.TextBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.2000000476837158D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox4.Style.Font.Bold = false;
            this.TextBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox4.Value = "=Fields.ComboboxCliente";
            // 
            // TextBox6
            // 
            this.TextBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(6.4000000953674316D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TextBox6.Name = "TextBox6";
            this.TextBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox6.Style.Font.Bold = false;
            this.TextBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox6.Value = "=Fields.CpfCnpj";
            // 
            // TextBox8
            // 
            this.TextBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(12.5D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TextBox8.Name = "TextBox8";
            this.TextBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.7999999523162842D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox8.Style.Font.Bold = false;
            this.TextBox8.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox8.Value = "=Fields.IdCidade";
            // 
            // TextBox10
            // 
            this.TextBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(14.399999618530273D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TextBox10.Name = "TextBox10";
            this.TextBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.7999999523162842D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox10.Style.Font.Bold = false;
            this.TextBox10.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox10.Value = "=Fields.IdEstado";
            // 
            // TextBox12
            // 
            this.TextBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(16.299999237060547D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TextBox12.Name = "TextBox12";
            this.TextBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.440000057220459D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox12.Style.Font.Bold = false;
            this.TextBox12.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox12.Value = "=Fields.IdPais";
            // 
            // TextBox14
            // 
            this.TextBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(17.840000152587891D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TextBox14.Name = "TextBox14";
            this.TextBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox14.Style.Font.Bold = false;
            this.TextBox14.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox14.Value = "=Fields.IdCliente";
            // 
            // TextBox16
            // 
            this.TextBox16.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(19.940000534057617D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TextBox16.Name = "TextBox16";
            this.TextBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox16.Style.Font.Bold = false;
            this.TextBox16.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox16.Value = "=Fields.NomeCidade";
            // 
            // TextBox18
            // 
            this.TextBox18.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(26.040000915527344D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TextBox18.Name = "TextBox18";
            this.TextBox18.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox18.Style.Font.Bold = false;
            this.TextBox18.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox18.Value = "=Fields.NomeEstado";
            // 
            // TextBox20
            // 
            this.TextBox20.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(32.139999389648438D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TextBox20.Name = "TextBox20";
            this.TextBox20.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox20.Style.Font.Bold = false;
            this.TextBox20.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox20.Value = "=Fields.NomePais";
            // 
            // TextBox22
            // 
            this.TextBox22.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(38.2400016784668D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TextBox22.Name = "TextBox22";
            this.TextBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.2000000476837158D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox22.Style.Font.Bold = false;
            this.TextBox22.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox22.Value = "=Fields.DatetimeCliente";
            // 
            // TextBox24
            // 
            this.TextBox24.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(41.540000915527344D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TextBox24.Name = "TextBox24";
            this.TextBox24.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox24.Style.Font.Bold = false;
            this.TextBox24.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox24.Value = "=Fields.NomeCliente";
            // 
            // TextBox26
            // 
            this.TextBox26.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(47.639999389648438D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TextBox26.Name = "TextBox26";
            this.TextBox26.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox26.Style.Font.Bold = false;
            this.TextBox26.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox26.Value = "=Fields.DecimalCliente";
            // 
            // TextBox28
            // 
            this.TextBox28.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(50.7400016784668D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TextBox28.Name = "TextBox28";
            this.TextBox28.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.7999999523162842D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox28.Style.Font.Bold = false;
            this.TextBox28.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox28.Value = "=Fields.IdTipoPessoa";
            // 
            // TextBox30
            // 
            this.TextBox30.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(53.639999389648438D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TextBox30.Name = "TextBox30";
            this.TextBox30.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.2000000476837158D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox30.Style.Font.Bold = false;
            this.TextBox30.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox30.Value = "=Fields.IntCliente";
            // 
            // TextBox32
            // 
            this.TextBox32.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(55.939998626708984D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TextBox32.Name = "TextBox32";
            this.TextBox32.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.880000114440918D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox32.Style.Font.Bold = false;
            this.TextBox32.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox32.Value = "=Fields.LongCliente";
            // 
            // TextBox34
            // 
            this.TextBox34.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(58.919998168945312D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TextBox34.Name = "TextBox34";
            this.TextBox34.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.5999999046325684D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox34.Style.Font.Bold = false;
            this.TextBox34.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox34.Value = "=Fields.PessoaFisica";
            // 
            // TextBox36
            // 
            this.TextBox36.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(61.619998931884766D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TextBox36.Name = "TextBox36";
            this.TextBox36.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.5999999046325684D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox36.Style.Font.Bold = false;
            this.TextBox36.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox36.Value = "=Fields.ShortCliente";
            // 
            // TextBox38
            // 
            this.TextBox38.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(64.319999694824219D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TextBox38.Name = "TextBox38";
            this.TextBox38.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox38.Style.Font.Bold = false;
            this.TextBox38.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox38.Value = "=Fields.PessoaJuridica";
            // 
            // TextBox40
            // 
            this.TextBox40.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(67.419998168945312D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TextBox40.Name = "TextBox40";
            this.TextBox40.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox40.Style.Font.Bold = false;
            this.TextBox40.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox40.Value = "=Fields.StringCliente";
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Cm(2D);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.TextBoxHeader,
            this.TextBoxDateTime,
            this.PictureBox1,
            this.TextBoxCompanyName,
            this.TextBoxFilter,
            this.TextBox1,
            this.TextBox3,
            this.TextBox5,
            this.TextBox7,
            this.TextBox9,
            this.TextBox11,
            this.TextBox13,
            this.TextBox15,
            this.TextBox17,
            this.TextBox19,
            this.TextBox21,
            this.TextBox23,
            this.TextBox25,
            this.TextBox27,
            this.TextBox29,
            this.TextBox31,
            this.TextBox33,
            this.TextBox35,
            this.TextBox37,
            this.TextBox39});
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
            this.TextBoxHeader.Value = "Teste2PaiFilha";
            // 
            // TextBoxDateTime
            // 
            this.TextBoxDateTime.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(25D), Telerik.Reporting.Drawing.Unit.Cm(0.099999949336051941D));
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
            // TextBoxFilter
            // 
            this.TextBoxFilter.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(15.799997329711914D), Telerik.Reporting.Drawing.Unit.Cm(0.90000033378601074D));
            this.TextBoxFilter.Name = "TextBoxFilter";
            this.TextBoxFilter.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(15.5D), Telerik.Reporting.Drawing.Unit.Cm(0.4999997615814209D));
            this.TextBoxFilter.Style.Font.Bold = false;
            this.TextBoxFilter.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.TextBoxFilter.Value = "=Parameters.TranslatedJqueryExpression.Value";
            // 
            // TextBox1
            // 
            this.TextBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(1.5D));
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox1.Style.Font.Bold = true;
            this.TextBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox1.Value = "Boolean Cliente";
            // 
            // TextBox3
            // 
            this.TextBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.0999999046325684D), Telerik.Reporting.Drawing.Unit.Cm(1.5D));
            this.TextBox3.Name = "TextBox3";
            this.TextBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.2000000476837158D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox3.Style.Font.Bold = true;
            this.TextBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox3.Value = "Combobox Cliente";
            // 
            // TextBox5
            // 
            this.TextBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(6.4000000953674316D), Telerik.Reporting.Drawing.Unit.Cm(1.5D));
            this.TextBox5.Name = "TextBox5";
            this.TextBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox5.Style.Font.Bold = true;
            this.TextBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox5.Value = "Cpf Cnpj";
            // 
            // TextBox7
            // 
            this.TextBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(12.5D), Telerik.Reporting.Drawing.Unit.Cm(1.5D));
            this.TextBox7.Name = "TextBox7";
            this.TextBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.7999999523162842D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox7.Style.Font.Bold = true;
            this.TextBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox7.Value = "Id Cidade";
            // 
            // TextBox9
            // 
            this.TextBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(14.399999618530273D), Telerik.Reporting.Drawing.Unit.Cm(1.5D));
            this.TextBox9.Name = "TextBox9";
            this.TextBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.7999999523162842D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox9.Style.Font.Bold = true;
            this.TextBox9.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox9.Value = "Id Estado";
            // 
            // TextBox11
            // 
            this.TextBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(16.299999237060547D), Telerik.Reporting.Drawing.Unit.Cm(1.5D));
            this.TextBox11.Name = "TextBox11";
            this.TextBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.440000057220459D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox11.Style.Font.Bold = true;
            this.TextBox11.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox11.Value = "Id Pais";
            // 
            // TextBox13
            // 
            this.TextBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(17.840000152587891D), Telerik.Reporting.Drawing.Unit.Cm(1.5D));
            this.TextBox13.Name = "TextBox13";
            this.TextBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox13.Style.Font.Bold = true;
            this.TextBox13.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox13.Value = "Id Cliente";
            // 
            // TextBox15
            // 
            this.TextBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(19.940000534057617D), Telerik.Reporting.Drawing.Unit.Cm(1.5D));
            this.TextBox15.Name = "TextBox15";
            this.TextBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox15.Style.Font.Bold = true;
            this.TextBox15.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox15.Value = "Nome Cidade";
            // 
            // TextBox17
            // 
            this.TextBox17.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(26.040000915527344D), Telerik.Reporting.Drawing.Unit.Cm(1.5D));
            this.TextBox17.Name = "TextBox17";
            this.TextBox17.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox17.Style.Font.Bold = true;
            this.TextBox17.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox17.Value = "Nome Estado";
            // 
            // TextBox19
            // 
            this.TextBox19.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(32.139999389648438D), Telerik.Reporting.Drawing.Unit.Cm(1.5D));
            this.TextBox19.Name = "TextBox19";
            this.TextBox19.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox19.Style.Font.Bold = true;
            this.TextBox19.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox19.Value = "Nome Pais";
            // 
            // TextBox21
            // 
            this.TextBox21.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(38.2400016784668D), Telerik.Reporting.Drawing.Unit.Cm(1.5D));
            this.TextBox21.Name = "TextBox21";
            this.TextBox21.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.2000000476837158D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox21.Style.Font.Bold = true;
            this.TextBox21.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox21.Value = "Datetime Cliente";
            // 
            // TextBox23
            // 
            this.TextBox23.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(41.540000915527344D), Telerik.Reporting.Drawing.Unit.Cm(1.5D));
            this.TextBox23.Name = "TextBox23";
            this.TextBox23.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox23.Style.Font.Bold = true;
            this.TextBox23.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox23.Value = "Nome Cliente";
            // 
            // TextBox25
            // 
            this.TextBox25.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(47.639999389648438D), Telerik.Reporting.Drawing.Unit.Cm(1.5D));
            this.TextBox25.Name = "TextBox25";
            this.TextBox25.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox25.Style.Font.Bold = true;
            this.TextBox25.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox25.Value = "Decimal Cliente";
            // 
            // TextBox27
            // 
            this.TextBox27.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(50.7400016784668D), Telerik.Reporting.Drawing.Unit.Cm(1.5D));
            this.TextBox27.Name = "TextBox27";
            this.TextBox27.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.7999999523162842D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox27.Style.Font.Bold = true;
            this.TextBox27.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox27.Value = "Id Tipo Pessoa";
            // 
            // TextBox29
            // 
            this.TextBox29.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(53.639999389648438D), Telerik.Reporting.Drawing.Unit.Cm(1.5D));
            this.TextBox29.Name = "TextBox29";
            this.TextBox29.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.2000000476837158D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox29.Style.Font.Bold = true;
            this.TextBox29.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox29.Value = "Int Cliente";
            // 
            // TextBox31
            // 
            this.TextBox31.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(55.939998626708984D), Telerik.Reporting.Drawing.Unit.Cm(1.5D));
            this.TextBox31.Name = "TextBox31";
            this.TextBox31.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.880000114440918D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox31.Style.Font.Bold = true;
            this.TextBox31.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox31.Value = "Long Cliente";
            // 
            // TextBox33
            // 
            this.TextBox33.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(58.919998168945312D), Telerik.Reporting.Drawing.Unit.Cm(1.5D));
            this.TextBox33.Name = "TextBox33";
            this.TextBox33.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.5999999046325684D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox33.Style.Font.Bold = true;
            this.TextBox33.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox33.Value = "Pessoa Fisica";
            // 
            // TextBox35
            // 
            this.TextBox35.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(61.619998931884766D), Telerik.Reporting.Drawing.Unit.Cm(1.5D));
            this.TextBox35.Name = "TextBox35";
            this.TextBox35.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.5999999046325684D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox35.Style.Font.Bold = true;
            this.TextBox35.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox35.Value = "Short Cliente";
            // 
            // TextBox37
            // 
            this.TextBox37.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(64.319999694824219D), Telerik.Reporting.Drawing.Unit.Cm(1.5D));
            this.TextBox37.Name = "TextBox37";
            this.TextBox37.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox37.Style.Font.Bold = true;
            this.TextBox37.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox37.Value = "Pessoa Juridica";
            // 
            // TextBox39
            // 
            this.TextBox39.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(67.419998168945312D), Telerik.Reporting.Drawing.Unit.Cm(1.5D));
            this.TextBox39.Name = "TextBox39";
            this.TextBox39.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox39.Style.Font.Bold = true;
            this.TextBox39.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox39.Value = "String Cliente";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Cm(0.70000004768371582D);
            this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.TextBoxPageCount});
            this.pageFooter.Name = "pageFooter";
            this.pageFooter.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            // 
            // TextBoxPageCount
            // 
            this.TextBoxPageCount.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(25.5D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TextBoxPageCount.Name = "TextBoxPageCount";
            this.TextBoxPageCount.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBoxPageCount.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.TextBoxPageCount.Value = "= \"Pg :\" + PageNumber.ToString() + \"/\" + PageCount.ToString()";
            // 
            // ReportDS
            // 
            this.ReportDS.DataMember = "GetCliente";
            this.ReportDS.DataSource = typeof(Linx.Demo.BV.Reports.PaiFilhaDataSource);
            this.ReportDS.Name = "ReportDS";
            this.ReportDS.Parameters.AddRange(new Telerik.Reporting.ObjectDataSourceParameter[] {
            new Telerik.Reporting.ObjectDataSourceParameter("reportItem", typeof(object), "= ReportItem")});
            // 
            // LinxDemoPaiFilhaCliente1
            // 
            this.DataSource = this.ReportDS;
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.detailSection1,
            this.pageHeader,
            this.pageFooter});
            this.Name = "LinxDemoPaiFilhaCliente1";
            this.PageSettings.Landscape = true;
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
            reportParameter15.Name = "Branch";
            reportParameter15.Value = " ";
            reportParameter16.Name = "LoginMode";
            reportParameter16.Value = " ";
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
            this.ReportParameters.Add(reportParameter15);
            this.ReportParameters.Add(reportParameter16);
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(31.299997329711914D);
            ((System.ComponentModel.ISupportInitialize)(this.VendaChild1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
    }
}

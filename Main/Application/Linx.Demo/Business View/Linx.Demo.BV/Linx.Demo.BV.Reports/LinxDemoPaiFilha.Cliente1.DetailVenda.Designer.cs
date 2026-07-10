namespace Linx.Demo.BV.Reports {
    
    
    public partial class LinxDemoPaiFilhaCliente1DetailVenda {
        
        private Telerik.Reporting.DetailSection detailSection1;
        
        private Telerik.Reporting.Group group1;
        
        private Telerik.Reporting.GroupFooterSection groupFooterSection1;
        
        private Telerik.Reporting.GroupHeaderSection groupHeaderSection1;
        
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
        
        private void InitializeComponent() {
            Telerik.Reporting.Group group1 = new Telerik.Reporting.Group();
            this.detailSection1 = new Telerik.Reporting.DetailSection();
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
            this.groupFooterSection1 = new Telerik.Reporting.GroupFooterSection();
            this.groupHeaderSection1 = new Telerik.Reporting.GroupHeaderSection();
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
            this.ReportDS = new Telerik.Reporting.ObjectDataSource();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // detailSection1
            // 
            this.detailSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(0.67000001668930054D);
            this.detailSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
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
            this.TextBox24});
            this.detailSection1.KeepTogether = true;
            this.detailSection1.Name = "detailSection1";
            // 
            // TextBox2
            // 
            this.TextBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TextBox2.Name = "TextBox2";
            this.TextBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.5999999046325684D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox2.Style.Font.Bold = false;
            this.TextBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox2.Value = "=Fields.BooleanVenda";
            // 
            // TextBox4
            // 
            this.TextBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.7000000476837158D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TextBox4.Name = "TextBox4";
            this.TextBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.7999999523162842D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox4.Style.Font.Bold = false;
            this.TextBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox4.Value = "=Fields.ComboboxVenda";
            // 
            // TextBox6
            // 
            this.TextBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(5.5999999046325684D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TextBox6.Name = "TextBox6";
            this.TextBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.4000000953674316D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox6.Style.Font.Bold = false;
            this.TextBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox6.Value = "=Fields.IdTipoPagamento";
            // 
            // TextBox8
            // 
            this.TextBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(9.1000003814697266D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TextBox8.Name = "TextBox8";
            this.TextBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.5999999046325684D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox8.Style.Font.Bold = false;
            this.TextBox8.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox8.Value = "=Fields.IdTipoVenda";
            // 
            // TextBox10
            // 
            this.TextBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(11.800000190734863D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TextBox10.Name = "TextBox10";
            this.TextBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.6000000238418579D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox10.Style.Font.Bold = false;
            this.TextBox10.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox10.Value = "=Fields.IdVenda";
            // 
            // TextBox12
            // 
            this.TextBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.5D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TextBox12.Name = "TextBox12";
            this.TextBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.7999999523162842D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox12.Style.Font.Bold = false;
            this.TextBox12.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox12.Value = "=Fields.DatetimeVenda";
            // 
            // TextBox14
            // 
            this.TextBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(16.399999618530273D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TextBox14.Name = "TextBox14";
            this.TextBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox14.Style.Font.Bold = false;
            this.TextBox14.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox14.Value = "=Fields.IdCliente";
            // 
            // TextBox16
            // 
            this.TextBox16.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(18.5D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TextBox16.Name = "TextBox16";
            this.TextBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.5999999046325684D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox16.Style.Font.Bold = false;
            this.TextBox16.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox16.Value = "=Fields.DecimalVenda";
            // 
            // TextBox18
            // 
            this.TextBox18.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(21.200000762939453D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TextBox18.Name = "TextBox18";
            this.TextBox18.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.7999999523162842D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox18.Style.Font.Bold = false;
            this.TextBox18.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox18.Value = "=Fields.IntVenda";
            // 
            // TextBox20
            // 
            this.TextBox20.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(23.100000381469727D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TextBox20.Name = "TextBox20";
            this.TextBox20.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.880000114440918D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox20.Style.Font.Bold = false;
            this.TextBox20.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox20.Value = "=Fields.LongVenda";
            // 
            // TextBox22
            // 
            this.TextBox22.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(26.079999923706055D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TextBox22.Name = "TextBox22";
            this.TextBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.2000000476837158D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox22.Style.Font.Bold = false;
            this.TextBox22.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox22.Value = "=Fields.ShortVenda";
            // 
            // TextBox24
            // 
            this.TextBox24.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(28.3799991607666D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TextBox24.Name = "TextBox24";
            this.TextBox24.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox24.Style.Font.Bold = false;
            this.TextBox24.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox24.Value = "=Fields.StringVenda";
            // 
            // groupFooterSection1
            // 
            this.groupFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(1.1299999952316284D);
            this.groupFooterSection1.Name = "groupFooterSection1";
            // 
            // groupHeaderSection1
            // 
            this.groupHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(0.40999999642372131D);
            this.groupHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
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
            this.TextBox23});
            this.groupHeaderSection1.Name = "groupHeaderSection1";
            // 
            // TextBox1
            // 
            this.TextBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0.0099999997764825821D));
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.5999999046325684D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox1.Style.Font.Bold = true;
            this.TextBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox1.Value = "Boolean Venda";
            // 
            // TextBox3
            // 
            this.TextBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(6.29979944229126D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TextBox3.Name = "TextBox3";
            this.TextBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.7999999523162842D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox3.Style.Font.Bold = true;
            this.TextBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox3.Value = "Combobox Venda";
            // 
            // TextBox5
            // 
            this.TextBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.8995993137359619D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TextBox5.Name = "TextBox5";
            this.TextBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.4000000953674316D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox5.Style.Font.Bold = true;
            this.TextBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox5.Value = "Id Tipo Pagamento";
            // 
            // TextBox7
            // 
            this.TextBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(9.1000003814697266D), Telerik.Reporting.Drawing.Unit.Cm(0.0099999997764825821D));
            this.TextBox7.Name = "TextBox7";
            this.TextBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.5999999046325684D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox7.Style.Font.Bold = true;
            this.TextBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox7.Value = "Id Tipo Venda";
            // 
            // TextBox9
            // 
            this.TextBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(11.800000190734863D), Telerik.Reporting.Drawing.Unit.Cm(0.0099999997764825821D));
            this.TextBox9.Name = "TextBox9";
            this.TextBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.6000000238418579D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox9.Style.Font.Bold = true;
            this.TextBox9.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox9.Value = "Id Venda";
            // 
            // TextBox11
            // 
            this.TextBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.5D), Telerik.Reporting.Drawing.Unit.Cm(0.0099999997764825821D));
            this.TextBox11.Name = "TextBox11";
            this.TextBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.7999999523162842D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox11.Style.Font.Bold = true;
            this.TextBox11.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox11.Value = "Datetime Venda";
            // 
            // TextBox13
            // 
            this.TextBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(16.399999618530273D), Telerik.Reporting.Drawing.Unit.Cm(0.0099999997764825821D));
            this.TextBox13.Name = "TextBox13";
            this.TextBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox13.Style.Font.Bold = true;
            this.TextBox13.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox13.Value = "Id Cliente";
            // 
            // TextBox15
            // 
            this.TextBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(18.5D), Telerik.Reporting.Drawing.Unit.Cm(0.0099999997764825821D));
            this.TextBox15.Name = "TextBox15";
            this.TextBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.5999999046325684D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox15.Style.Font.Bold = true;
            this.TextBox15.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox15.Value = "Decimal Venda";
            // 
            // TextBox17
            // 
            this.TextBox17.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(21.200000762939453D), Telerik.Reporting.Drawing.Unit.Cm(0.0099999997764825821D));
            this.TextBox17.Name = "TextBox17";
            this.TextBox17.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.7999999523162842D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox17.Style.Font.Bold = true;
            this.TextBox17.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox17.Value = "Int Venda";
            // 
            // TextBox19
            // 
            this.TextBox19.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(23.100000381469727D), Telerik.Reporting.Drawing.Unit.Cm(0.0099999997764825821D));
            this.TextBox19.Name = "TextBox19";
            this.TextBox19.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.880000114440918D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox19.Style.Font.Bold = true;
            this.TextBox19.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox19.Value = "Long Venda";
            // 
            // TextBox21
            // 
            this.TextBox21.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(26.079999923706055D), Telerik.Reporting.Drawing.Unit.Cm(0.0099999997764825821D));
            this.TextBox21.Name = "TextBox21";
            this.TextBox21.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.2000000476837158D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox21.Style.Font.Bold = true;
            this.TextBox21.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox21.Value = "Short Venda";
            // 
            // TextBox23
            // 
            this.TextBox23.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(28.3799991607666D), Telerik.Reporting.Drawing.Unit.Cm(0.0099999997764825821D));
            this.TextBox23.Name = "TextBox23";
            this.TextBox23.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox23.Style.Font.Bold = true;
            this.TextBox23.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox23.Value = "String Venda";
            // 
            // ReportDS
            // 
            this.ReportDS.DataMember = "GetVenda";
            this.ReportDS.DataSource = typeof(Linx.Demo.BV.Reports.PaiFilhaDataSource);
            this.ReportDS.Name = "ReportDS";
            // 
            // LinxDemoPaiFilhaCliente1DetailVenda
            // 
            this.DataSource = this.ReportDS;
            group1.GroupFooter = this.groupFooterSection1;
            group1.GroupHeader = this.groupHeaderSection1;
            group1.Name = "group1";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.groupHeaderSection1,
            this.groupFooterSection1,
            this.detailSection1});
            this.Name = "LinxDemoPaiFilhaCliente1DetailVenda";
            this.PageSettings.Landscape = true;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(34.380001068115234D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
    }
}

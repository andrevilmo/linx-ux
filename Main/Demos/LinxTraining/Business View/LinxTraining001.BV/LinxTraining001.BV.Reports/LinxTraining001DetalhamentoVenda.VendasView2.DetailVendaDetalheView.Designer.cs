namespace LinxTraining001.BV.Reports {
    
    
    public partial class LinxTraining001DetalhamentoVendaVendasView2DetailVendaDetalheView {
        
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
        
        private void InitializeComponent() {
            Telerik.Reporting.Group group1 = new Telerik.Reporting.Group();
            this.groupFooterSection1 = new Telerik.Reporting.GroupFooterSection();
            this.groupHeaderSection1 = new Telerik.Reporting.GroupHeaderSection();
            this.TextBox1 = new Telerik.Reporting.TextBox();
            this.TextBox3 = new Telerik.Reporting.TextBox();
            this.TextBox5 = new Telerik.Reporting.TextBox();
            this.TextBox7 = new Telerik.Reporting.TextBox();
            this.TextBox9 = new Telerik.Reporting.TextBox();
            this.detailSection1 = new Telerik.Reporting.DetailSection();
            this.TextBox2 = new Telerik.Reporting.TextBox();
            this.TextBox4 = new Telerik.Reporting.TextBox();
            this.TextBox6 = new Telerik.Reporting.TextBox();
            this.TextBox8 = new Telerik.Reporting.TextBox();
            this.TextBox10 = new Telerik.Reporting.TextBox();
            this.ReportDS = new Telerik.Reporting.ObjectDataSource();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // groupFooterSection1
            // 
            this.groupFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(1.8000000715255737D);
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
            this.TextBox9});
            this.groupHeaderSection1.Name = "groupHeaderSection1";
            // 
            // TextBox1
            // 
            this.TextBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0.0099999997764825821D));
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox1.Style.Font.Bold = true;
            this.TextBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox1.Value = "Média";
            // 
            // TextBox3
            // 
            this.TextBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.1000000238418579D), Telerik.Reporting.Drawing.Unit.Cm(0.0099999997764825821D));
            this.TextBox3.Name = "TextBox3";
            this.TextBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.2000000476837158D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox3.Style.Font.Bold = true;
            this.TextBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox3.Value = "Hora";
            // 
            // TextBox5
            // 
            this.TextBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.4000000953674316D), Telerik.Reporting.Drawing.Unit.Cm(0.0099999997764825821D));
            this.TextBox5.Name = "TextBox5";
            this.TextBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.2239999771118164D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox5.Style.Font.Bold = true;
            this.TextBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox5.Value = "Preço";
            // 
            // TextBox7
            // 
            this.TextBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.7239999771118164D), Telerik.Reporting.Drawing.Unit.Cm(0.0099999997764825821D));
            this.TextBox7.Name = "TextBox7";
            this.TextBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.3999999761581421D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox7.Style.Font.Bold = true;
            this.TextBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox7.Value = "Produto";
            // 
            // TextBox9
            // 
            this.TextBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(5.2239999771118164D), Telerik.Reporting.Drawing.Unit.Cm(0.0099999997764825821D));
            this.TextBox9.Name = "TextBox9";
            this.TextBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox9.Style.Font.Bold = true;
            this.TextBox9.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox9.Value = "Quantidade";
            // 
            // detailSection1
            // 
            this.detailSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(1.4900000095367432D);
            this.detailSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.TextBox2,
            this.TextBox4,
            this.TextBox6,
            this.TextBox8,
            this.TextBox10});
            this.detailSection1.KeepTogether = true;
            this.detailSection1.Name = "detailSection1";
            // 
            // TextBox2
            // 
            this.TextBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TextBox2.Name = "TextBox2";
            this.TextBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox2.Style.Font.Bold = false;
            this.TextBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox2.Value = "=Fields.Media";
            // 
            // TextBox4
            // 
            this.TextBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.1000000238418579D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TextBox4.Name = "TextBox4";
            this.TextBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.2000000476837158D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox4.Style.Font.Bold = false;
            this.TextBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox4.Value = "=Fields.Hora";
            // 
            // TextBox6
            // 
            this.TextBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.4000000953674316D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TextBox6.Name = "TextBox6";
            this.TextBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.2239999771118164D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox6.Style.Font.Bold = false;
            this.TextBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox6.Value = "=Fields.Preco";
            // 
            // TextBox8
            // 
            this.TextBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.7239999771118164D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TextBox8.Name = "TextBox8";
            this.TextBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.3999999761581421D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox8.Style.Font.Bold = false;
            this.TextBox8.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox8.Value = "=Fields.Produto";
            // 
            // TextBox10
            // 
            this.TextBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(5.2239999771118164D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.TextBox10.Name = "TextBox10";
            this.TextBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBox10.Style.Font.Bold = false;
            this.TextBox10.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBox10.Value = "=Fields.Quantidade";
            // 
            // ReportDS
            // 
            this.ReportDS.DataMember = "GetVendaDetalheView";
            this.ReportDS.DataSource = typeof(LinxTraining001.BV.Reports.DetalhamentoVendaDataSource);
            this.ReportDS.Name = "ReportDS";
            this.ReportDS.Parameters.AddRange(new Telerik.Reporting.ObjectDataSourceParameter[] {
            new Telerik.Reporting.ObjectDataSourceParameter("reportItem", typeof(object), "= ReportItem")});
            // 
            // LinxTraining001DetalhamentoVendaVendasView2DetailVendaDetalheView
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
            this.Name = "LinxTraining001DetalhamentoVendaVendasView2DetailVendaDetalheView";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.5D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(20D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
    }
}

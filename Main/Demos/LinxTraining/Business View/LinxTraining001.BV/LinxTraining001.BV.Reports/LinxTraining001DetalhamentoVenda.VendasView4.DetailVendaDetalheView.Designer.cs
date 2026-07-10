namespace LinxTraining001.BV.Reports {
    
    
    public partial class LinxTraining001DetalhamentoVendaVendasView4DetailVendaDetalheView {
        
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
            // Sections
this.detailSection1 = new Telerik.Reporting.DetailSection();
this.group1 = new Telerik.Reporting.Group();
this.groupHeaderSection1 = new Telerik.Reporting.GroupHeaderSection();
this.groupFooterSection1 = new Telerik.Reporting.GroupFooterSection();
this.group1.GroupFooter = this.groupFooterSection1;
this.group1.GroupHeader = this.groupHeaderSection1;
this.group1.Name = "group1";
this.groupHeaderSection1.Height = new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm);
this.groupHeaderSection1.Name = "groupHeaderSection1";
this.groupFooterSection1.Height = new Telerik.Reporting.Drawing.Unit(0.13229165971279144D, Telerik.Reporting.Drawing.UnitType.Cm);
this.groupFooterSection1.Name = "groupFooterSection1";
this.Groups.AddRange(new Telerik.Reporting.Group[] { this.group1 });
this.ReportDS = new Telerik.Reporting.ObjectDataSource();
this.ReportDS.DataMember = "GetVendaDetalheView";
this.ReportDS.DataSource = typeof(LinxTraining001.BV.Reports.DetalhamentoVendaDataSource);
this.ReportDS.Name = "ReportDS";
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
this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {this.detailSection1, this.groupHeaderSection1, this.groupFooterSection1});
            // BeginInit
((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
this.detailSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(0.67D);
this.detailSection1.KeepTogether = true;
this.detailSection1.Name = "detailSection1";
this.TextBox1.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.01, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox1.Name = "TextBox1";
this.TextBox1.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox1.Style.Font.Bold = true;
this.TextBox1.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
this.TextBox1.Value = "Média";
this.TextBox2.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox2.Name = "TextBox2";
this.TextBox2.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox2.Style.Font.Bold = false;
this.TextBox2.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
this.TextBox2.Value = "=Fields.Media";
this.TextBox3.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(1.1, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.01, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox3.Name = "TextBox3";
this.TextBox3.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.2, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox3.Style.Font.Bold = true;
this.TextBox3.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
this.TextBox3.Value = "Hora";
this.TextBox4.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(1.1, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox4.Name = "TextBox4";
this.TextBox4.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.2, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox4.Style.Font.Bold = false;
this.TextBox4.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
this.TextBox4.Value = "=Fields.Hora";
this.TextBox5.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(2.4, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.01, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox5.Name = "TextBox5";
this.TextBox5.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.224, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox5.Style.Font.Bold = true;
this.TextBox5.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
this.TextBox5.Value = "Preço";
this.TextBox6.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(2.4, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox6.Name = "TextBox6";
this.TextBox6.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.224, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox6.Style.Font.Bold = false;
this.TextBox6.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
this.TextBox6.Value = "=Fields.Preco";
this.TextBox7.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(3.724, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.01, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox7.Name = "TextBox7";
this.TextBox7.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.4, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox7.Style.Font.Bold = true;
this.TextBox7.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
this.TextBox7.Value = "Produto";
this.TextBox8.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(3.724, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox8.Name = "TextBox8";
this.TextBox8.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.4, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox8.Style.Font.Bold = false;
this.TextBox8.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
this.TextBox8.Value = "=Fields.Produto";
this.TextBox9.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(5.224, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.01, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox9.Name = "TextBox9";
this.TextBox9.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox9.Style.Font.Bold = true;
this.TextBox9.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
this.TextBox9.Value = "Quantidade";
this.TextBox10.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(5.224, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox10.Name = "TextBox10";
this.TextBox10.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox10.Style.Font.Bold = false;
this.TextBox10.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
this.TextBox10.Value = "=Fields.Quantidade";
this.groupHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] { TextBox1,TextBox3,TextBox5,TextBox7,TextBox9 });
this.detailSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] { TextBox2,TextBox4,TextBox6,TextBox8,TextBox10 });
this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] { this.detailSection1});
            // PageSettings
this.PageSettings.Landscape = false;
this.Width = new Telerik.Reporting.Drawing.Unit(20D, Telerik.Reporting.Drawing.UnitType.Cm);
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

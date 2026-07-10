namespace LinxTraining001.BV.Reports {
    
    
    public partial class LinxTraining001Venda02ClientesViewDetailVendasView {
        
        private Telerik.Reporting.DetailSection detailSection1;
        
        private Telerik.Reporting.Group group1;
        
        private Telerik.Reporting.GroupFooterSection groupFooterSection1;
        
        private Telerik.Reporting.GroupHeaderSection groupHeaderSection1;
        
        private Telerik.Reporting.ObjectDataSource ReportDS;
        
        private Telerik.Reporting.TextBox TextBox1;
        
        private Telerik.Reporting.TextBox TextBox2;
        
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
this.ReportDS.DataMember = "GetVendasView";
this.ReportDS.DataSource = typeof(LinxTraining001.BV.Reports.Venda02DataSource);
this.ReportDS.Name = "ReportDS";
this.ReportDS.Parameters.AddRange(new Telerik.Reporting.ObjectDataSourceParameter[] {
            new Telerik.Reporting.ObjectDataSourceParameter("reportItem", typeof(object), "= ReportItem")});
this.DataSource = this.ReportDS;
this.TextBox1 = new Telerik.Reporting.TextBox();
this.TextBox2 = new Telerik.Reporting.TextBox();
this.Style.BackgroundColor = System.Drawing.Color.White;
this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {this.detailSection1, this.groupHeaderSection1, this.groupFooterSection1});
            // BeginInit
((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
this.detailSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(0.67D);
this.detailSection1.KeepTogether = true;
this.detailSection1.Name = "detailSection1";
this.TextBox1.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.01, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox1.Name = "TextBox1";
this.TextBox1.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(4.8, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox1.Style.Font.Bold = true;
this.TextBox1.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
this.TextBox1.Value = "Nome";
this.TextBox2.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox2.Name = "TextBox2";
this.TextBox2.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(4.8, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448D, Telerik.Reporting.Drawing.UnitType.Cm));
this.TextBox2.Style.Font.Bold = false;
this.TextBox2.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(11D, Telerik.Reporting.Drawing.UnitType.Point);
this.TextBox2.Value = "=Fields.Nome";
this.groupHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] { TextBox1 });
this.detailSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] { TextBox2 });
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

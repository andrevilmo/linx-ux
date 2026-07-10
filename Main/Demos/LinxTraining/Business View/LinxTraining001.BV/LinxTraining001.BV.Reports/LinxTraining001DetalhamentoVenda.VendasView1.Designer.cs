namespace LinxTraining001.BV.Reports {
    
    
    public partial class LinxTraining001DetalhamentoVendaVendasView1 {
        
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
        
        private Telerik.Reporting.Crosstab crosstabVendaDetalheView;
        
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
        
        private Telerik.Reporting.TextBox TextBoxVendaDetalheView1;
        
        private Telerik.Reporting.TextBox TextBoxVendaDetalheView7;
        
        private Telerik.Reporting.TextBox TextBoxVendaDetalheView2;
        
        private Telerik.Reporting.TextBox TextBoxVendaDetalheView8;
        
        private Telerik.Reporting.TextBox TextBoxVendaDetalheView3;
        
        private Telerik.Reporting.TextBox TextBoxVendaDetalheView9;
        
        private Telerik.Reporting.TextBox TextBoxVendaDetalheView4;
        
        private Telerik.Reporting.TextBox TextBoxVendaDetalheView10;
        
        private Telerik.Reporting.TextBox TextBoxVendaDetalheView5;
        
        private Telerik.Reporting.TextBox TextBoxVendaDetalheView11;
        
        private void InitializeComponent() {
            Telerik.Reporting.TableGroup tableGroup7 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup8 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup9 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup10 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup11 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup12 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup13 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup14 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup1 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup2 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup3 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup4 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup5 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup6 = new Telerik.Reporting.TableGroup();
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
            this.TextBoxVendasView1 = new Telerik.Reporting.TextBox();
            this.TextBoxVendasView2 = new Telerik.Reporting.TextBox();
            this.TextBoxVendasView3 = new Telerik.Reporting.TextBox();
            this.TextBoxVendasView4 = new Telerik.Reporting.TextBox();
            this.TextBoxVendasView5 = new Telerik.Reporting.TextBox();
            this.TextBoxVendaDetalheView1 = new Telerik.Reporting.TextBox();
            this.TextBoxVendaDetalheView2 = new Telerik.Reporting.TextBox();
            this.TextBoxVendaDetalheView3 = new Telerik.Reporting.TextBox();
            this.TextBoxVendaDetalheView4 = new Telerik.Reporting.TextBox();
            this.TextBoxVendaDetalheView5 = new Telerik.Reporting.TextBox();
            this.detailSection1 = new Telerik.Reporting.DetailSection();
            this.crosstabVendasView = new Telerik.Reporting.Crosstab();
            this.TextBoxVendasView7 = new Telerik.Reporting.TextBox();
            this.TextBoxVendasView8 = new Telerik.Reporting.TextBox();
            this.TextBoxVendasView9 = new Telerik.Reporting.TextBox();
            this.TextBoxVendasView10 = new Telerik.Reporting.TextBox();
            this.TextBoxVendasView11 = new Telerik.Reporting.TextBox();
            this.crosstabVendaDetalheView = new Telerik.Reporting.Crosstab();
            this.TextBoxVendaDetalheView7 = new Telerik.Reporting.TextBox();
            this.TextBoxVendaDetalheView8 = new Telerik.Reporting.TextBox();
            this.TextBoxVendaDetalheView9 = new Telerik.Reporting.TextBox();
            this.TextBoxVendaDetalheView10 = new Telerik.Reporting.TextBox();
            this.TextBoxVendaDetalheView11 = new Telerik.Reporting.TextBox();
            this.ReportDS = new Telerik.Reporting.ObjectDataSource();
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.TextBoxHeader = new Telerik.Reporting.TextBox();
            this.TextBoxDateTime = new Telerik.Reporting.TextBox();
            this.PictureBox1 = new Telerik.Reporting.PictureBox();
            this.TextBoxCompanyName = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.TextBoxPageCount = new Telerik.Reporting.TextBox();
            this.TextBoxFilter = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TextBoxVendasView1
            // 
            this.TextBoxVendasView1.Name = "TextBoxVendasView1";
            this.TextBoxVendasView1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2D), Telerik.Reporting.Drawing.Unit.Cm(0.39999997615814209D));
            this.TextBoxVendasView1.Style.Font.Bold = true;
            this.TextBoxVendasView1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBoxVendasView1.Value = "Data";
            // 
            // TextBoxVendasView2
            // 
            this.TextBoxVendasView2.Name = "TextBoxVendasView2";
            this.TextBoxVendasView2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2D), Telerik.Reporting.Drawing.Unit.Cm(0.39999997615814209D));
            this.TextBoxVendasView2.Style.Font.Bold = true;
            this.TextBoxVendasView2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBoxVendasView2.Value = "Nome";
            // 
            // TextBoxVendasView3
            // 
            this.TextBoxVendasView3.Name = "TextBoxVendasView3";
            this.TextBoxVendasView3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2D), Telerik.Reporting.Drawing.Unit.Cm(0.39999997615814209D));
            this.TextBoxVendasView3.Style.Font.Bold = true;
            this.TextBoxVendasView3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBoxVendasView3.Value = "Origem";
            // 
            // TextBoxVendasView4
            // 
            this.TextBoxVendasView4.Name = "TextBoxVendasView4";
            this.TextBoxVendasView4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBoxVendasView4.Style.Font.Bold = true;
            this.TextBoxVendasView4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBoxVendasView4.Value = "ValorTotal";
            // 
            // TextBoxVendasView5
            // 
            this.TextBoxVendasView5.Name = "TextBoxVendasView5";
            this.TextBoxVendasView5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2D), Telerik.Reporting.Drawing.Unit.Cm(0.39999997615814209D));
            this.TextBoxVendasView5.Style.Font.Bold = true;
            this.TextBoxVendasView5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBoxVendasView5.Value = "VendaVip";
            // 
            // TextBoxVendaDetalheView1
            // 
            this.TextBoxVendaDetalheView1.Name = "TextBoxVendaDetalheView1";
            this.TextBoxVendaDetalheView1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBoxVendaDetalheView1.Style.Font.Bold = true;
            this.TextBoxVendaDetalheView1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBoxVendaDetalheView1.Value = "Média";
            // 
            // TextBoxVendaDetalheView2
            // 
            this.TextBoxVendaDetalheView2.Name = "TextBoxVendaDetalheView2";
            this.TextBoxVendaDetalheView2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBoxVendaDetalheView2.Style.Font.Bold = true;
            this.TextBoxVendaDetalheView2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBoxVendaDetalheView2.Value = "Hora";
            // 
            // TextBoxVendaDetalheView3
            // 
            this.TextBoxVendaDetalheView3.Name = "TextBoxVendaDetalheView3";
            this.TextBoxVendaDetalheView3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBoxVendaDetalheView3.Style.Font.Bold = true;
            this.TextBoxVendaDetalheView3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBoxVendaDetalheView3.Value = "Preço";
            // 
            // TextBoxVendaDetalheView4
            // 
            this.TextBoxVendaDetalheView4.Name = "TextBoxVendaDetalheView4";
            this.TextBoxVendaDetalheView4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBoxVendaDetalheView4.Style.Font.Bold = true;
            this.TextBoxVendaDetalheView4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBoxVendaDetalheView4.Value = "Produto";
            // 
            // TextBoxVendaDetalheView5
            // 
            this.TextBoxVendaDetalheView5.Name = "TextBoxVendaDetalheView5";
            this.TextBoxVendaDetalheView5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.TextBoxVendaDetalheView5.Style.Font.Bold = true;
            this.TextBoxVendaDetalheView5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBoxVendaDetalheView5.Value = "Quantidade";
            // 
            // detailSection1
            // 
            this.detailSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(3.1000001430511475D);
            this.detailSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.crosstabVendasView});
            this.detailSection1.Name = "detailSection1";
            // 
            // crosstabVendasView
            // 
            this.crosstabVendasView.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Cm(2D)));
            this.crosstabVendasView.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Cm(2D)));
            this.crosstabVendasView.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Cm(2D)));
            this.crosstabVendasView.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Cm(2D)));
            this.crosstabVendasView.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Cm(2D)));
            this.crosstabVendasView.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(Telerik.Reporting.Drawing.Unit.Cm(1.5000002384185791D)));
            this.crosstabVendasView.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(Telerik.Reporting.Drawing.Unit.Cm(1.1999996900558472D)));
            this.crosstabVendasView.Body.SetCellContent(0, 0, this.TextBoxVendasView7);
            this.crosstabVendasView.Body.SetCellContent(0, 1, this.TextBoxVendasView8);
            this.crosstabVendasView.Body.SetCellContent(0, 2, this.TextBoxVendasView9);
            this.crosstabVendasView.Body.SetCellContent(0, 3, this.TextBoxVendasView10);
            this.crosstabVendasView.Body.SetCellContent(0, 4, this.TextBoxVendasView11);
            this.crosstabVendasView.Body.SetCellContent(1, 0, this.crosstabVendaDetalheView, 1, 5);
            tableGroup7.ReportItem = this.TextBoxVendasView1;
            tableGroup8.ReportItem = this.TextBoxVendasView2;
            tableGroup9.ReportItem = this.TextBoxVendasView3;
            tableGroup10.ReportItem = this.TextBoxVendasView4;
            tableGroup11.ReportItem = this.TextBoxVendasView5;
            this.crosstabVendasView.ColumnGroups.Add(tableGroup7);
            this.crosstabVendasView.ColumnGroups.Add(tableGroup8);
            this.crosstabVendasView.ColumnGroups.Add(tableGroup9);
            this.crosstabVendasView.ColumnGroups.Add(tableGroup10);
            this.crosstabVendasView.ColumnGroups.Add(tableGroup11);
            this.crosstabVendasView.DataSource = this.ReportDS;
            this.crosstabVendasView.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.TextBoxVendasView7,
            this.TextBoxVendasView8,
            this.TextBoxVendasView9,
            this.TextBoxVendasView10,
            this.TextBoxVendasView11,
            this.crosstabVendaDetalheView,
            this.TextBoxVendasView1,
            this.TextBoxVendasView2,
            this.TextBoxVendasView3,
            this.TextBoxVendasView4,
            this.TextBoxVendasView5});
            this.crosstabVendasView.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.crosstabVendasView.Name = "crosstabVendasView";
            tableGroup13.Name = "groupReport";
            tableGroup14.Name = "groupSubReport";
            tableGroup12.ChildGroups.Add(tableGroup13);
            tableGroup12.ChildGroups.Add(tableGroup14);
            tableGroup12.Groupings.Add(new Telerik.Reporting.Grouping(null));
            tableGroup12.Name = "Detail";
            this.crosstabVendasView.RowGroups.Add(tableGroup12);
            this.crosstabVendasView.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(10D), Telerik.Reporting.Drawing.Unit.Cm(3.0999999046325684D));
            // 
            // TextBoxVendasView7
            // 
            this.TextBoxVendasView7.Name = "TextBoxVendasView7";
            this.TextBoxVendasView7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2D), Telerik.Reporting.Drawing.Unit.Cm(1.5D));
            this.TextBoxVendasView7.Style.Font.Bold = false;
            this.TextBoxVendasView7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBoxVendasView7.Value = "=Fields.Data";
            // 
            // TextBoxVendasView8
            // 
            this.TextBoxVendasView8.Name = "TextBoxVendasView8";
            this.TextBoxVendasView8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2D), Telerik.Reporting.Drawing.Unit.Cm(1.5D));
            this.TextBoxVendasView8.Style.Font.Bold = false;
            this.TextBoxVendasView8.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBoxVendasView8.Value = "=Fields.Nome";
            // 
            // TextBoxVendasView9
            // 
            this.TextBoxVendasView9.Name = "TextBoxVendasView9";
            this.TextBoxVendasView9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2D), Telerik.Reporting.Drawing.Unit.Cm(1.5D));
            this.TextBoxVendasView9.Style.Font.Bold = false;
            this.TextBoxVendasView9.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBoxVendasView9.Value = "=Fields.Origem";
            // 
            // TextBoxVendasView10
            // 
            this.TextBoxVendasView10.Name = "TextBoxVendasView10";
            this.TextBoxVendasView10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2D), Telerik.Reporting.Drawing.Unit.Cm(1.5D));
            this.TextBoxVendasView10.Style.Font.Bold = false;
            this.TextBoxVendasView10.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBoxVendasView10.Value = "=Fields.ValorTotal";
            // 
            // TextBoxVendasView11
            // 
            this.TextBoxVendasView11.Name = "TextBoxVendasView11";
            this.TextBoxVendasView11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2D), Telerik.Reporting.Drawing.Unit.Cm(1.5D));
            this.TextBoxVendasView11.Style.Font.Bold = false;
            this.TextBoxVendasView11.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBoxVendasView11.Value = "=Fields.VendaVip";
            // 
            // crosstabVendaDetalheView
            // 
            this.crosstabVendaDetalheView.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Cm(2D)));
            this.crosstabVendaDetalheView.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Cm(2D)));
            this.crosstabVendaDetalheView.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Cm(2D)));
            this.crosstabVendaDetalheView.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Cm(2D)));
            this.crosstabVendaDetalheView.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Cm(2D)));
            this.crosstabVendaDetalheView.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(Telerik.Reporting.Drawing.Unit.Cm(1.5D)));
            this.crosstabVendaDetalheView.Body.SetCellContent(0, 0, this.TextBoxVendaDetalheView7);
            this.crosstabVendaDetalheView.Body.SetCellContent(0, 1, this.TextBoxVendaDetalheView8);
            this.crosstabVendaDetalheView.Body.SetCellContent(0, 2, this.TextBoxVendaDetalheView9);
            this.crosstabVendaDetalheView.Body.SetCellContent(0, 3, this.TextBoxVendaDetalheView10);
            this.crosstabVendaDetalheView.Body.SetCellContent(0, 4, this.TextBoxVendaDetalheView11);
            tableGroup1.ReportItem = this.TextBoxVendaDetalheView1;
            tableGroup2.ReportItem = this.TextBoxVendaDetalheView2;
            tableGroup3.ReportItem = this.TextBoxVendaDetalheView3;
            tableGroup4.ReportItem = this.TextBoxVendaDetalheView4;
            tableGroup5.ReportItem = this.TextBoxVendaDetalheView5;
            this.crosstabVendaDetalheView.ColumnGroups.Add(tableGroup1);
            this.crosstabVendaDetalheView.ColumnGroups.Add(tableGroup2);
            this.crosstabVendaDetalheView.ColumnGroups.Add(tableGroup3);
            this.crosstabVendaDetalheView.ColumnGroups.Add(tableGroup4);
            this.crosstabVendaDetalheView.ColumnGroups.Add(tableGroup5);
            this.crosstabVendaDetalheView.DataSource = this.ReportDS;
            this.crosstabVendaDetalheView.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.TextBoxVendaDetalheView7,
            this.TextBoxVendaDetalheView8,
            this.TextBoxVendaDetalheView9,
            this.TextBoxVendaDetalheView10,
            this.TextBoxVendaDetalheView11,
            this.TextBoxVendaDetalheView1,
            this.TextBoxVendaDetalheView2,
            this.TextBoxVendaDetalheView3,
            this.TextBoxVendaDetalheView4,
            this.TextBoxVendaDetalheView5});
            this.crosstabVendaDetalheView.Name = "crosstabVendaDetalheView";
            tableGroup6.Groupings.Add(new Telerik.Reporting.Grouping(null));
            tableGroup6.Name = "Detail";
            this.crosstabVendaDetalheView.RowGroups.Add(tableGroup6);
            this.crosstabVendaDetalheView.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(10D), Telerik.Reporting.Drawing.Unit.Cm(1.8999999761581421D));
            this.crosstabVendaDetalheView.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            // 
            // TextBoxVendaDetalheView7
            // 
            this.TextBoxVendaDetalheView7.Name = "TextBoxVendaDetalheView7";
            this.TextBoxVendaDetalheView7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2D), Telerik.Reporting.Drawing.Unit.Cm(1.5D));
            this.TextBoxVendaDetalheView7.Style.Font.Bold = false;
            this.TextBoxVendaDetalheView7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBoxVendaDetalheView7.Value = "=Fields.Media";
            // 
            // TextBoxVendaDetalheView8
            // 
            this.TextBoxVendaDetalheView8.Name = "TextBoxVendaDetalheView8";
            this.TextBoxVendaDetalheView8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2D), Telerik.Reporting.Drawing.Unit.Cm(1.5D));
            this.TextBoxVendaDetalheView8.Style.Font.Bold = false;
            this.TextBoxVendaDetalheView8.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBoxVendaDetalheView8.Value = "=Fields.Hora";
            // 
            // TextBoxVendaDetalheView9
            // 
            this.TextBoxVendaDetalheView9.Name = "TextBoxVendaDetalheView9";
            this.TextBoxVendaDetalheView9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2D), Telerik.Reporting.Drawing.Unit.Cm(1.5D));
            this.TextBoxVendaDetalheView9.Style.Font.Bold = false;
            this.TextBoxVendaDetalheView9.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBoxVendaDetalheView9.Value = "=Fields.Preco";
            // 
            // TextBoxVendaDetalheView10
            // 
            this.TextBoxVendaDetalheView10.Name = "TextBoxVendaDetalheView10";
            this.TextBoxVendaDetalheView10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2D), Telerik.Reporting.Drawing.Unit.Cm(1.5D));
            this.TextBoxVendaDetalheView10.Style.Font.Bold = false;
            this.TextBoxVendaDetalheView10.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBoxVendaDetalheView10.Value = "=Fields.Produto";
            // 
            // TextBoxVendaDetalheView11
            // 
            this.TextBoxVendaDetalheView11.Name = "TextBoxVendaDetalheView11";
            this.TextBoxVendaDetalheView11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2D), Telerik.Reporting.Drawing.Unit.Cm(1.5D));
            this.TextBoxVendaDetalheView11.Style.Font.Bold = false;
            this.TextBoxVendaDetalheView11.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.TextBoxVendaDetalheView11.Value = "=Fields.Quantidade";
            // 
            // ReportDS
            // 
            this.ReportDS.DataMember = "GetVendasView";
            this.ReportDS.DataSource = typeof(LinxTraining001.BV.Reports.DetalhamentoVendaDataSource);
            this.ReportDS.Name = "ReportDS";
            this.ReportDS.Parameters.AddRange(new Telerik.Reporting.ObjectDataSourceParameter[] {
            new Telerik.Reporting.ObjectDataSourceParameter("reportItem", typeof(object), "= ReportItem")});
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Cm(2D);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.TextBoxHeader,
            this.TextBoxDateTime,
            this.PictureBox1,
            this.TextBoxCompanyName,
            this.textBox1});
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
            this.TextBoxHeader.Value = "Crosstab tst";
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
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(16.399999618530273D), Telerik.Reporting.Drawing.Unit.Cm(0.49999994039535522D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.5999999046325684D), Telerik.Reporting.Drawing.Unit.Cm(1.0000001192092896D));
            this.textBox1.Value = "=Parameters.CurrentUser.Value";
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
            this.TextBoxPageCount.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(25.5D), Telerik.Reporting.Drawing.Unit.Cm(0D));
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
            // LinxTraining001DetalhamentoVendaVendasView1
            // 
            this.DataSource = this.ReportDS;
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.detailSection1,
            this.pageHeader,
            this.pageFooter});
            this.Name = "LinxTraining001DetalhamentoVendaVendasView1";
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
            reportParameter13.Name = "TranslatedJqueryExpression";
            reportParameter13.Value = " ";
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
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(29D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        private Telerik.Reporting.TextBox textBox1;
    }
}

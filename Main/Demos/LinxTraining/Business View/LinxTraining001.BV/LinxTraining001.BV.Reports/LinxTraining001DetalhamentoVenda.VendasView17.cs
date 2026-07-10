namespace LinxTraining001.BV.Reports {
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using System.Collections;
    using System.Linq;
    using Linx.Tools;
    
    
    // Inform here the report title.
    [Description("Relatorio teste Matheus")]
    public partial class LinxTraining001DetalhamentoVendaVendasView17 : Telerik.Reporting.Report {
        
        public LinxTraining001DetalhamentoVendaVendasView17() {
            InitializeComponent();
            this.ReportDS.DataMember = "GetVendasView";
            this.ReportDS.DataSource = new DetalhamentoVendaDataSource();
            this.DataSource = this.ReportDS;
            this.ItemDataBinding += new System.EventHandler(this.LinxTraining001DetalhamentoVendaVendasView17_ItemDataBinding);
            this.subReportVendaDetalheView.NeedDataSource += new System.EventHandler(this.subReportVendaDetalheView_NeedDataSource);
            this.subReportVendaDetalheView.ItemDataBound += new System.EventHandler(this.subReportVendaDetalheView_ItemDataBound);
        }
        
        private void LinxTraining001DetalhamentoVendaVendasView17_ItemDataBinding(object sender, EventArgs e) {
            var parameters = ((Telerik.Reporting.Processing.Report)sender).Parameters;
            //Adjust Image Reference
            Image logoImg = null;
            if (!String.IsNullOrWhiteSpace(parameters["CompanyLogo"].Value.ToString()))
            {
                try
                {
                    System.Net.WebClient wc = new System.Net.WebClient();
                    byte[] originalData = wc.DownloadData(parameters["CompanyLogo"].Value.ToString());
                    System.IO.MemoryStream stream = new System.IO.MemoryStream(originalData);
                    logoImg = Bitmap.FromStream(stream);
                }
                catch { }
            }
            if (logoImg == null)
            {
                var directory = String.Empty;
                try
                {
                    directory = System.Web.HttpRuntime.BinDirectory;
                    string logoFile = System.IO.Path.Combine(directory, "..\\image\\Linx.PNG");
                    if (System.IO.File.Exists(logoFile))
                        logoImg = Bitmap.FromFile(logoFile);
                }
                catch { }
            }
            PictureBox1.Value = logoImg;​
            ((DetalhamentoVendaDataSource)this.ReportDS.DataSource).DetailsForLoading = new string[] { "VendaDetalheView" };
        }
        
        private void subReportVendaDetalheView_NeedDataSource(object sender, EventArgs e) {
            Telerik.Reporting.Processing.ReportItemBase item = (Telerik.Reporting.Processing.ReportItemBase)sender;
            Telerik.Reporting.Processing.SubReport subReportItem = sender as Telerik.Reporting.Processing.SubReport;
            IList valores = item.DataObject["VendaDetalheViewList"] as IList;
            subReportItem.InnerReport.DataSource = valores;
            subReportVendaDetalheView.Height = Unit.Inch(0.01);
        }
        
        private void subReportVendaDetalheView_ItemDataBound(object sender, EventArgs e) {
            Telerik.Reporting.Processing.ReportItemBase item = (Telerik.Reporting.Processing.ReportItemBase)sender;
            IList valores = item.DataObject["VendaDetalheViewList"] as IList;
            item.Visible = (valores == null || valores.Count == 0 ? false : true);
        }
    }
}

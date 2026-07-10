namespace Linx.Demo.BV.Reports {
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using System.Collections;
    using System.Linq;
    using Linx.Tools;
    
    
    // Inform here the report title.
    [Description("Teste2PaiFilha")]
    public partial class LinxDemoPaiFilhaCliente1 : Telerik.Reporting.Report {
        
        public LinxDemoPaiFilhaCliente1() {
            InitializeComponent();
            this.ReportDS.DataMember = "GetCliente";
            this.ReportDS.DataSource = new PaiFilhaDataSource();
            this.DataSource = this.ReportDS;
            this.ItemDataBinding += new System.EventHandler(this.LinxDemoPaiFilhaCliente1_ItemDataBinding);
            this.subReportVenda.NeedDataSource += new System.EventHandler(this.subReportVenda_NeedDataSource);
            this.subReportVenda.ItemDataBound += new System.EventHandler(this.subReportVenda_ItemDataBound);
        }
        
        private void LinxDemoPaiFilhaCliente1_ItemDataBinding(object sender, EventArgs e) {
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
            PictureBox1.Value = logoImg;
            ((PaiFilhaDataSource)this.ReportDS.DataSource).DetailsForLoading = new string[] { "Venda" };
        }
        
        private void subReportVenda_NeedDataSource(object sender, EventArgs e) {
            Telerik.Reporting.Processing.ReportItemBase item = (Telerik.Reporting.Processing.ReportItemBase)sender;
            Telerik.Reporting.Processing.SubReport subReportItem = sender as Telerik.Reporting.Processing.SubReport;
            IList valores = item.DataObject["VendaList"] as IList;
            subReportItem.InnerReport.DataSource = valores;
            subReportVenda.Height = Unit.Inch(0.01);
        }
        
        private void subReportVenda_ItemDataBound(object sender, EventArgs e) {
            Telerik.Reporting.Processing.ReportItemBase item = (Telerik.Reporting.Processing.ReportItemBase)sender;
            IList valores = item.DataObject["VendaList"] as IList;
            item.Visible = (valores == null || valores.Count == 0 ? false : true);
        }
    }
}

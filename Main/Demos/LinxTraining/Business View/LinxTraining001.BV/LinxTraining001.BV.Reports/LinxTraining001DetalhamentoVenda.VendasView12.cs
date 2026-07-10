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
    [Description("tstCross21052015")]
    public partial class LinxTraining001DetalhamentoVendaVendasView12 : Telerik.Reporting.Report {
        
        public LinxTraining001DetalhamentoVendaVendasView12() {
            InitializeComponent();
            this.ReportDS.DataMember = "GetVendasView";
            this.ReportDS.DataSource = new DetalhamentoVendaDataSource();
            this.DataSource = null;
            this.crosstabVendasView.DataSource = this.ReportDS;
            this.ItemDataBinding += new System.EventHandler(this.LinxTraining001DetalhamentoVendaVendasView12_ItemDataBinding);
        }
        
        private void LinxTraining001DetalhamentoVendaVendasView12_ItemDataBinding(object sender, EventArgs e) {
            var parameters = ((Telerik.Reporting.Processing.Report)sender).Parameters;
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
        }
    }
}

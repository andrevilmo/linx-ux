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
    [Description("TesteDois")]
    public partial class LinxDemoPaiFilhaLoja : Telerik.Reporting.Report {
        
        public LinxDemoPaiFilhaLoja() {
            InitializeComponent();
            this.ReportDS.DataMember = "GetLoja";
            this.ReportDS.DataSource = new PaiFilhaDataSource();
            this.DataSource = this.ReportDS;
            this.ItemDataBinding += new System.EventHandler(this.LinxDemoPaiFilhaLoja_ItemDataBinding);
        }
        
        private void LinxDemoPaiFilhaLoja_ItemDataBinding(object sender, EventArgs e) {
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
 //           PictureBox1.Value = logoImg;​
        }
    }
}

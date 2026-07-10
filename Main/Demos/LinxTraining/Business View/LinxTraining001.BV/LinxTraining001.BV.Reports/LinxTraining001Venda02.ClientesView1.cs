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
    [Description("TSTUIExterna")]
    public partial class LinxTraining001Venda02ClientesView1 : Telerik.Reporting.Report {
        
        public LinxTraining001Venda02ClientesView1() {
            InitializeComponent();
            this.ReportDS.DataMember = "GetClientesView";
            this.ReportDS.DataSource = new Venda02DataSource();
            this.DataSource = this.ReportDS;
            this.ItemDataBinding += new System.EventHandler(this.LinxTraining001Venda02ClientesView1_ItemDataBinding);
        }
        
        private void LinxTraining001Venda02ClientesView1_ItemDataBinding(object sender, EventArgs e) {
            var parameters = ((Telerik.Reporting.Processing.Report)sender).Parameters;
            if (String.IsNullOrWhiteSpace(parameters["CompanyLogo"].Value.ToString()))
                parameters["CompanyLogo"].Value = "http://localhost:1710/image/Linx.PNG";
        }
    }
}

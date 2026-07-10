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
    [Description("relatorio UI eXTERNA")]
    public partial class LinxTraining001Venda02ClientesView : Telerik.Reporting.Report {
        
        public LinxTraining001Venda02ClientesView() {
            InitializeComponent();
            this.ReportDS.DataMember = "GetClientesView";
            this.ReportDS.DataSource = new Venda02DataSource();
            this.DataSource = this.ReportDS;
            this.ItemDataBinding += new System.EventHandler(this.LinxTraining001Venda02ClientesView_ItemDataBinding);
            this.subReportVendasView.NeedDataSource += new System.EventHandler(this.subReportVendasView_NeedDataSource);
            this.subReportVendasView.ItemDataBound += new System.EventHandler(this.subReportVendasView_ItemDataBound);
        }
        
        private void LinxTraining001Venda02ClientesView_ItemDataBinding(object sender, EventArgs e) {
            var parameters = ((Telerik.Reporting.Processing.Report)sender).Parameters;
            if (String.IsNullOrWhiteSpace(parameters["CompanyLogo"].Value.ToString()))
                parameters["CompanyLogo"].Value = "http://localhost:1710/image/Linx.PNG";
            ((Venda02DataSource)this.ReportDS.DataSource).DetailsForLoading = new string[] { "VendasView" };
        }
        
        private void subReportVendasView_NeedDataSource(object sender, EventArgs e) {
            Telerik.Reporting.Processing.ReportItemBase item = (Telerik.Reporting.Processing.ReportItemBase)sender;
            Telerik.Reporting.Processing.SubReport subReportItem = sender as Telerik.Reporting.Processing.SubReport;
            IList valores = item.DataObject["VendasViewList"] as IList;
            subReportItem.InnerReport.DataSource = valores;
            subReportVendasView.Height = Unit.Inch(0.01);
        }
        
        private void subReportVendasView_ItemDataBound(object sender, EventArgs e) {
            Telerik.Reporting.Processing.ReportItemBase item = (Telerik.Reporting.Processing.ReportItemBase)sender;
            IList valores = item.DataObject["VendasViewList"] as IList;
            item.Visible = (valores == null || valores.Count == 0 ? false : true);
        }
    }
}

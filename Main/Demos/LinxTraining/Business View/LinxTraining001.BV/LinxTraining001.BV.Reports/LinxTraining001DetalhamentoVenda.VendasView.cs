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
    [Description("Relatorio Detalhamento de Vendas")]
    public partial class LinxTraining001DetalhamentoVendaVendasView : Telerik.Reporting.Report {
        
        public LinxTraining001DetalhamentoVendaVendasView() {
            InitializeComponent();
            this.ReportDS.DataMember = "GetVendasView";
            this.ReportDS.DataSource = new DetalhamentoVendaDataSource();
            this.DataSource = this.ReportDS;
            this.ItemDataBinding += new System.EventHandler(this.LinxTraining001DetalhamentoVendaVendasView_ItemDataBinding);
            this.subReportVendaDetalheView.NeedDataSource += new System.EventHandler(this.subReportVendaDetalheView_NeedDataSource);
            this.subReportVendaDetalheView.ItemDataBound += new System.EventHandler(this.subReportVendaDetalheView_ItemDataBound);
        }
        
        private void LinxTraining001DetalhamentoVendaVendasView_ItemDataBinding(object sender, EventArgs e) {
            var parameters = ((Telerik.Reporting.Processing.Report)sender).Parameters;
            if (String.IsNullOrWhiteSpace(parameters["CompanyLogo"].Value.ToString()))
                parameters["CompanyLogo"].Value = "http://localhost:1710/image/Linx.PNG";
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

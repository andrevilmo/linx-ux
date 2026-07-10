namespace LinxTraining001.BV.Reports {
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using System.Collections;
    using System.Linq;
    using Linx.Tools;
    
    
    public partial class LinxTraining001DetalhamentoVendaVendasViewDetailVendaDetalheView : Telerik.Reporting.Report {
        
        public LinxTraining001DetalhamentoVendaVendasViewDetailVendaDetalheView() {
            InitializeComponent();
            this.DataSource = null;
        }
    }
}

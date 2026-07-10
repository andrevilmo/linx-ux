namespace Linx.Demo.BV.Reports {
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using System.Collections;
    using System.Linq;
    using Linx.Tools;
    
    
    public partial class LinxDemoPaiFilhaCliente1DetailVenda : Telerik.Reporting.Report {
        
        public LinxDemoPaiFilhaCliente1DetailVenda() {
            InitializeComponent();
            this.DataSource = null;
        }
    }
}

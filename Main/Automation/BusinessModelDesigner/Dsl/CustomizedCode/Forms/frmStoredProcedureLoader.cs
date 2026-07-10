using Linx.Tools.Migration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Linx.BusinessModelDesigner.CustomizedCode.Forms
{
    public partial class frmStoredProcedureLoader : Form
    {
        public bool OK { get; set; }
        public Procedure Procedure { get; set; }
        public List<ParameterValue> ParameterValues;
        public frmStoredProcedureLoader()
        {
            InitializeComponent();
        }

        private void frmStoredProcedureLoader_Load(object sender, EventArgs e)
        {
            ParameterValues =
                Procedure.Parameters
                    .Select(
                        p => new ParameterValue() { Name = p.Name, TypeName = p.DbDataType.ToString() }
                    ).ToList();

            this.gridParameters.DataSource = ParameterValues;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {




            OK = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            OK = false;
            this.Close();
        }
    }
    public class ParameterValue
    {
        public string Name { get; set; }
        public string TypeName { get; set; }
        public string Value { get; set; }
    }
}

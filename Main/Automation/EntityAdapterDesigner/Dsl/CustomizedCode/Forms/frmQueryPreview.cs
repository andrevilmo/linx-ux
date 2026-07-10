using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Linx.Tools;

namespace Linx.EntityAdapterDesigner.CustomCode
{
    public partial class frmQueryPreview : Form
    {
        public string AssemblyName { get; set; }
        public string ConfigName { get; set; }
        public string ContextName { get; set; }
        public EntityAdapter EntityClass { get; set; }

        public frmQueryPreview()
        {
            InitializeComponent();
        }

        public void ExecuteQuery()
        {
            this.Text = "Entity for analysing: " + this.EntityClass.Name;
            this.lbStatus.Text = "Executing Query...";
            this.btPlay.Enabled = false;
            this.numTotalRows.Enabled = false;
            this.txFilter.Enabled = false;

            Application.DoEvents();
            
            var result = Linx.LinqExtensions.BM.BmQueryBuilder.ExecuteQuery(this.AssemblyName, this.ConfigName , this.ContextName, "Get" + this.EntityClass.Name + "NoAssociations", (int)this.numTotalRows.Value, this.txFilter.Text);

            if (result == null || result.DataRows.IsNullOrEmpty())
            {
                return;
            }

            try
            {
                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = CreateTable(result.DataRows);
                this.dataGridResult.DataSource = bindingSource;
                this.dataGridResult.Refresh();
                this.txScript.Text = result.SqlOutput;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.GetCompleteMessage(), "Alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                this.lbStatus.Text = "";
                this.btPlay.Enabled = true;
                this.numTotalRows.Enabled = true;
                this.txFilter.Enabled = true;
                Application.DoEvents();
            }

        }

        public DataTable CreateTable(string jsonData)
        {
            dynamic model = System.Web.Helpers.Json.Decode(jsonData);
            var table = new System.Data.DataTable();
            
            foreach (var property in EntityClass.GetAllAttributes())
            {
                Type propType = property.GetDataType();
                table.Columns.Add(new DataColumn(property.Name, propType));
            }
            
            foreach (var data in model)
            {
                var row = table.NewRow();
                foreach (var property in EntityClass.GetAllAttributes())
                {
                    row[property.Name] = data[property.Name] ?? DBNull.Value;                    
                }
                table.Rows.Add(row);
            }
            return table;
        }

        private void btPlay_Click(object sender, EventArgs e)
        {
            ExecuteQuery();
        }

        private void frmQueryPreview_Shown(object sender, EventArgs e)
        {
            ExecuteQuery();
        }
    }
}

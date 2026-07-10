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

namespace Linx.BusinessDataModelDesigner.CustomizedCode.Forms
{
    public partial class frmQueryPreview : Form
    {
        public string ContextName { get; set; }
        public ModelClass EntityClass { get; set; }

        public frmQueryPreview()
        {
            InitializeComponent();
        }

        public void ExecuteQuery()
        {
            var defaultProvider = this.EntityClass.BusinessDataModelDesignerRoot.GetDefaultProvider();
            this.Text = "Entity for analysing: " + this.EntityClass.Name;
            this.lbStatus.Text = "Executing Query...";
            this.btPlay.Enabled = false;
            this.numTotalRows.Enabled = false;
            this.txFilter.Enabled = false;

            Application.DoEvents();

            string connectionString = this.EntityClass.BusinessDataModelDesignerRoot.GetConfigConnectionString(); 
            if (defaultProvider == Provider.PostgreSQL)
                connectionString = connectionString.Replace("Data Source", "Server").Replace("Initial Catalog", "Database");

            System.Data.DataTable result = null;
            string sqlOutput = this.EntityClass.GetSqlQuery((int)this.numTotalRows.Value, this.txFilter.Text);
            
            if (sqlOutput.IsNullOrEmpty())
            {
                MessageBox.Show("The selected entity has no command associated!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                switch (defaultProvider)
                {
                    case Provider.SQLServer:
                        result = Linx.Tools.ScriptQueryManager.ExecuteMSSQLCommand(connectionString, sqlOutput);
                        break;
                    case Provider.MySQL:
                        result = Linx.Tools.ScriptQueryManager.ExecuteMySQLCommand(connectionString, sqlOutput);
                        break;
                    case Provider.SQLite:
                        result = Linx.Tools.ScriptQueryManager.ExecuteSQLiteCommand(connectionString, sqlOutput);
                        break;
                    case Provider.PostgreSQL:
                        result = Linx.Tools.ScriptQueryManager.ExecutePostgreSQLCommand(connectionString, sqlOutput);
                        break;
                    default:
                        break;
                }

                if (result == null)
                {
                    return;
                }

                this.dataGridResult.DataSource = result;
                this.dataGridResult.Refresh();                
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.GetCompleteMessage(), "Alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                this.txScript.Text = sqlOutput;
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
                if (!property.IsNotMapped())
                    table.Columns.Add(new DataColumn(property.Name, ModelAttribute.GetAttribueDataType2(property.DataType, false)));
            }

            foreach (var data in model)
            {
                var row = table.NewRow();
                foreach (var property in EntityClass.GetAllAttributes())
                {
                    if (!property.IsNotMapped())
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

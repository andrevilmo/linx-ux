using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Linx.Tools;
using Linx.Tools.Migration;
using Linx.BusinessModelDesigner.CustomizedCode.ReverseEngineeringModel;
using Linx.BusinessModelDesigner.CustomizedCode.Forms;

namespace Linx.BusinessModelDesigner.CustomCode
{
    public partial class frmReverseEngineer : Form
    {
        public string ConnectionString { get; set; }
        public Provider ConnectionProvider { get; set; }
        public List<StructBase> SelectTables { get; private set; }
        public List<string> SuggestedSelection { get; set; }
        private IProviderDatabaseLoader databaseLoader;
        public bool OK { get; private set; }

        Database database;

        public frmReverseEngineer()
        {
            SelectTables = new List<StructBase>();
            InitializeComponent();
            trvDatabaseObjects.AfterCheck += trvDatabaseObjects_AfterCheck;
            trvDatabaseObjects.BeforeCheck += TrvDatabaseObjects_BeforeCheck;
            this.Shown += frmReverseEngineer_Shown;
            grdColumns.AutoGenerateColumns = false;
        }



        void frmReverseEngineer_Shown(object sender, EventArgs e)
        {
            this.UseWaitCursor = true;
            try
            {
                LoadObjects();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error na engenharia reversa", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.UseWaitCursor = false;
        }

        private void LoadObjects()
        {
            if (!ConnectionString.IsNullOrEmpty())
            {
                trvDatabaseObjects.Nodes.Clear();

                switch (ConnectionProvider)
                {
                    case Provider.SQLServer:
                        databaseLoader = new SqlServerDatabaseLoader(ConnectionString);
                        break;
                    case Provider.SQLite:
                        databaseLoader = new SQLiteDatabaseLoader(ConnectionString);
                        break;
                    case Provider.MySQL:
                        databaseLoader = new MySqlDatabaseLoader(ConnectionString);
                        break;
                    case Provider.PostgreSQL:
                        databaseLoader = new PostgresqlDatabaseLoader(ConnectionString.Replace("Data Source", "Server").Replace("Initial Catalog", "Database"));
                        break;
                    default:
                        throw new NotSupportedException("Provider not supported: " + ConnectionProvider.ToString());
                }

                database = databaseLoader.GetDatabaseObjects(UpdateMainStatus);

                MountTree();
            }
        }


        public void UpdateMainStatus(string text, int progress)
        {
            this.lblStatus.Text = text;
            this.LoadProgress.Value = progress;
            this.MainStatus.Refresh();
        }

        TreeNode GetFolder(string name)
        {
            var node = new TreeNode()
            {
                Text = name,
                ImageIndex = 6,
                SelectedImageIndex = 6,
            };
            return node;
        }

        private void MountTree()
        {
            Action<TreeNode, IEnumerable<DbInfo>> runner = (node, list) =>
            {
                if (list != null)
                    foreach (DbInfo item in list)
                    {
                        var child = item.GetTreeNode();
                        node.Nodes.Add(child);
                    }
            };

            //mount database
            var nodeDatabase = this.database.GetTreeNode();
            //schemas
            foreach (var schemas in this.database.Schemas)
            {
                TreeNode folder, n = schemas.GetTreeNode();

                //tables
                if (schemas.HasTables)
                {
                    folder = GetFolder("Tables");
                    runner(folder, schemas.GetTables());
                    n.Nodes.Add(folder);
                }
                //Views
                if (schemas.HasViews)
                {

                    folder = GetFolder("Views");
                    runner(folder, schemas.GetViews());
                    n.Nodes.Add(folder);
                }
                //Functions
                if (schemas.HasFunctions)
                {

                    folder = GetFolder("Functions");
                    runner(folder, schemas.GetFunctions());
                    n.Nodes.Add(folder);
                }
                //Stored Procedures
                if (schemas.HasProcedures)
                {
                    folder = GetFolder("Stored Procedures");
                    runner(folder, schemas.GetProcedures());
                    n.Nodes.Add(folder);
                }

                nodeDatabase.Nodes.Add(n);
                n.Expand();

            }

            nodeDatabase.Expand();
            this.trvDatabaseObjects.Nodes.Add(nodeDatabase);

            if (SuggestedSelection != null && SuggestedSelection.Count > 0)
                CheckTree(this.trvDatabaseObjects.Nodes);
        }

        private void CheckTree(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Tag is TableBase)
                {
                    TableBase table = (TableBase)node.Tag;

                    if (SuggestedSelection.Contains(table.Schema.Name + "." + table.Name))
                        node.Checked = true;
                }
                //else if (node.Tag is FunctionBase)
                //{
                //    FunctionBase fnBase = (FunctionBase)node.Tag;

                //    if (SuggestedSelection.Contains(fnBase.Schema.Name + "." + fnBase.Name))
                //        node.Checked = true;
                //}
                this.CheckTree(node.Nodes);
            }
        }

        private void trvDatabaseObjects_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is StructBase)
                FillTableBase(e.Node.Tag as StructBase);

        }

        private bool FillTableBase(StructBase structBase)
        {
            lblIndexes.Text = structBase is TableBase ? "Indexes" : "Parameters";

            if (structBase is TableBase)
            {
                var tableBase = structBase as TableBase;
                txtPrimaryKey.Text = tableBase.GetPrimaryKeyInfo();
                lstForeignKeys.DataSource = tableBase.GetForeignKeysList();
                lstIndexes.DataSource = tableBase.GetIndexList();
            }
            else
                lstIndexes.DataSource = (structBase as FunctionBase).GetParameterList();

            if (!structBase.HasColumns && structBase is Procedure)
            {
                var procedure = structBase as Procedure;
                Dictionary<string, string> paramValues = null;
                if (procedure.Parameters.Count > 0)
                {
                    using (var frm = new frmStoredProcedureLoader())
                    {
                        frm.Procedure = procedure;
                        frm.ShowDialog(this);
                        paramValues = frm.ParameterValues.ToDictionary(pv => pv.Name, pv => pv.Value);
                    }
                }
                if (ConnectionProvider == Provider.SQLServer)
                {
                    try
                    {
                        databaseLoader.GetProcedureColumns(procedure, paramValues);
                    }catch(Exception ex)
                    {
                        MessageBox.Show(ex.GetCompleteMessage(), "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }

            if (structBase.HasColumns)
                grdColumns.DataSource = structBase.Columns;

            return true;
        }

        private void TrvDatabaseObjects_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Tag is Procedure)
                e.Cancel = !FillTableBase(e.Node.Tag as Procedure);
            else e.Cancel = false;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text) ||
                txtSearch.Text.Length < 2 || trvDatabaseObjects.Nodes.Count == 0) return;


            var objs = trvDatabaseObjects.Nodes.OfType<TreeNode>()
                .Find(
                    t => t.Nodes.OfType<TreeNode>(),
                    t => t.Text.ToLower().Contains(txtSearch.Text.ToLower()),
                    true);

            foreach (var o in objs)
            {
                ParentExpanded(o);
                trvDatabaseObjects.SelectedNode = o;
            }
        }

        private void mnuCollapse_Click(object sender, EventArgs e)
        {
            if (trvDatabaseObjects.SelectedNode != null)
                trvDatabaseObjects.SelectedNode.Collapse();
        }

        private void mnuCollapseAll_Click(object sender, EventArgs e)
        {
            trvDatabaseObjects.CollapseAll();
        }

        private void mnuExpandAll_Click(object sender, EventArgs e)
        {
            trvDatabaseObjects.ExpandAll();
        }


        private void trvDatabaseObjects_AfterCheck(object sender, TreeViewEventArgs e)
        {
            this.trvDatabaseObjects.AfterCheck -= new TreeViewEventHandler(trvDatabaseObjects_AfterCheck);
            this.CheckNodeParent(e.Node);
            this.CheckNodeChildren(e.Node);
            this.trvDatabaseObjects.AfterCheck += new TreeViewEventHandler(trvDatabaseObjects_AfterCheck);
        }

        private void CheckNodeChildren(TreeNode node)
        {
            if (node != null)
            {
                if (node.Tag is StructBase)
                    ManagerSelectedList(node.Tag as StructBase, node.Checked);

                foreach (TreeNode child in node.Nodes)
                {
                    child.Checked = node.Checked;
                    CheckNodeChildren(child);
                }
            }
        }

        private void CheckNodeParent(TreeNode node)
        {
            if (node.Parent != null)
            {
                if (node.Checked)
                {
                    if (!node.Parent.Checked)
                        node.Parent.Checked = true;
                }
                else
                {
                    bool existsCheckedNode = false;
                    foreach (TreeNode child in node.Parent.Nodes)
                    {
                        if (child.Checked)
                        {
                            existsCheckedNode = true;
                            break;
                        }
                    }
                    if (existsCheckedNode != node.Parent.Checked)
                        node.Parent.Checked = existsCheckedNode;
                }
                CheckNodeParent(node.Parent);
            }
        }

        private void ManagerSelectedList(StructBase structBase, bool add)
        {
            if (add)
            {
                if (!SelectTables.Contains(structBase))
                    SelectTables.Add(structBase);
            }
            else
            {
                if (SelectTables.Contains(structBase))
                    SelectTables.Remove(structBase);
            }
        }

        private void CheckChildren(TreeNodeCollection nodes, bool value)
        {
            foreach (TreeNode n in nodes)
            {
                n.Checked = value;
                CheckChildren(n.Nodes, value);
            }
        }

        void ParentExpanded(TreeNode node)
        {
            if (node != null && node.Parent != null)
            {
                node.Parent.Expand();
                ParentExpanded(node.Parent);
            }
        }


        private void btOk_Click(object sender, EventArgs e)
        {
            if (SelectTables.Count() == 0)
            {
                MessageBox.Show("No table selected!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //Add Periphery
            if (this.checkPeriphery.Checked)
            {
                foreach (var table in SelectTables.Where(t => t is Table).OfType<Table>().ToArray())
                {
                    foreach (var fk in table.ForeignKey)
                    {
                        if (!SelectTables.Contains(fk.Referenced))
                        {
                            SelectTables.Add(fk.Referenced);
                        }
                    }
                }
            }

            OK = true;
            this.Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}

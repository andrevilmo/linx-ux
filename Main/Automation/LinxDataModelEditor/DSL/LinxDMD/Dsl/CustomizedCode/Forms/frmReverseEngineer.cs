using Linx.BusinessDataModelDesigner.AppUI.Model;
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

namespace Linx.BusinessDataModelDesigner.CustomCode
{
    public partial class frmReverseEngineer : Form
    {
        public string ConnectionString { get; set; }
        public Provider ConnectionProvider { get; set; }
        public List<TableBase> SelectTables { get; private set; }
        public List<string> SuggestedSelection { get; set; }

        public bool OK { get; private set; }

        Database database;

        public frmReverseEngineer()
        {
            SelectTables = new List<TableBase>();
            InitializeComponent();
            trvDatabaseObjects.AfterCheck += trvDatabaseObjects_AfterCheck;
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
                        database = new SqlServerDatabaseLoader().GetDatabaseObjects(UpdateMainStatus, ConnectionString);
                        break;
                    case Provider.SQLite:
                        database = new SQLiteDatabaseLoader().GetDatabaseObjects(UpdateMainStatus, ConnectionString);
                        break;
                    case Provider.MySQL:
                        database = new MySqlDatabaseLoader().GetDatabaseObjects(UpdateMainStatus, ConnectionString);
                        break;
                    case Provider.PostgreSQL:
                        database = new PostgresqlDatabaseLoader().GetDatabaseObjects(UpdateMainStatus, ConnectionString.Replace("Data Source", "Server").Replace("Initial Catalog", "Database"));
                        break;
                    default:
                        throw new NotSupportedException("Provider not supported: " + ConnectionProvider.ToString());
                }


                MountTree();

            }
        }


        public void UpdateMainStatus(string text, int progress)
        {
            this.lblStatus.Text = text;
            this.LoadProgress.Value = progress;
            this.MainStatus.Refresh();
        }

        private void MountTree()
        {
            Action<TreeNode, IEnumerable<DbInfo>> runner = null;
            runner = (node, list) =>
                {
                    if (list != null)
                        foreach (DbInfo item in list)
                        {
                            var child = item.GetTreeNode();
                            node.Nodes.Add(child);
                            runner(child, item.GetChildren());
                        }
                };

            var nodeDatabase = this.database.GetTreeNode();

            runner(nodeDatabase, this.database.GetChildren());

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
                else
                    this.CheckTree(node.Nodes);
            }
        }

        private void trvDatabaseObjects_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is TableBase)
                FillTableBase(e.Node.Tag as TableBase);
        }

        private void FillTableBase(TableBase tableBase)
        {
            txtPrimaryKey.Text = tableBase.GetPrimaryKeyInfo();
            grdColumns.DataSource = tableBase.Columns;
            lstForeignKeys.DataSource = tableBase.GetForeignKeysList();
            lstIndexes.DataSource = tableBase.GetIndexList();
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
                if (node.Tag is TableBase)
                    ManagerSelectedList(node.Tag as TableBase, node.Checked);

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

        private void ManagerSelectedList(TableBase tableBase, bool add)
        {
            if (add)
            {
                if (!SelectTables.Contains(tableBase))
                    SelectTables.Add(tableBase);
            }
            else
            {
                if (SelectTables.Contains(tableBase))
                    SelectTables.Remove(tableBase);
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
                foreach (var table in SelectTables.Where(t => t is Linx.Tools.Migration.Table).OfType<Linx.Tools.Migration.Table>().ToArray())
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

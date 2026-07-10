using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Linx.Tools;

namespace Linx.EntityAdapterDesigner.CustomizedCode
{
    public partial class FormReportSettings : Form
    {
        public bool GenerateCrossTabReport { get; set; }
        public string Title { get; set; }
        public Dictionary<string, List<string>> PropertySelection { get; set; }
        public EntityAdapter MainEntity { get; set; }
        public List<string> ChildEntities { get; set; }

        
        public FormReportSettings()
        {
            InitializeComponent();                        
        }
        
        private string GetDisplayName(string name, string displayName)
        {
            if (MainEntity.PropertyOrder == AttributeOrder.DisplayName)
                return displayName + "(" + name + ")";
            else
                return name + "(" + displayName + ")";
        }


        private void FillTree(TreeNode parentNode, EntityAdapter entity)
        {   
            if (parentNode == null)
                this.treeEntityRelatedTypes.Nodes.Clear();

            string key = entity.EntityAdapterDesignerRoot.TargetNamespace + "." + entity.Name;
            TreeNode entityNode, referecesNode, refNode;

            //Add entity
            entityNode = (parentNode == null ? this.treeEntityRelatedTypes.Nodes.Add(key, GetDisplayName(entity.Name, entity.Name), 0, 0) : parentNode.Nodes.Add(key, GetDisplayName(entity.Name, entity.Name), 0, 0));
            entityNode.Checked = true;
            entityNode.Tag = "IsEntity";

            var entityDetails = entity.SourceEntityAdapters.Where(e => this.PropertySelection.ContainsKey(e.Name)).ToArray();
            if (entityDetails.Length > 0)
            {
                referecesNode = entityNode.Nodes.Add("Details", "Details", 1, 1);
                referecesNode.Tag = "IsReference";
                referecesNode.Checked = true;

                //Add Reference
                foreach (var entityRelated in entityDetails)
                {
                    this.FillTree(referecesNode, entityRelated);
                }
            }
            else referecesNode = null;

            //Add members
            foreach (var member in entity.GetAllInheritanceAttributes().Where(e => e.IsBrowsable).OrderBy(e => GetDisplayName(e.Name, e.DisplayName)))
            {
                refNode = entityNode.Nodes.Add(key + "." + member.Name, GetDisplayName(member.Name, member.DisplayName), 3, 3);
                refNode.Checked = true;
                refNode.Tag = member;
            }

            //Expand Nodes
            if (parentNode == null)
            {
                entityNode.Expand();
                if (referecesNode != null)
                    referecesNode.Expand();
            }
        }
        
        private void treeEntityRelatedTypes_AfterCheck(object sender, TreeViewEventArgs e)
        {
            this.treeEntityRelatedTypes.AfterCheck -= new TreeViewEventHandler(treeEntityRelatedTypes_AfterCheck);
            this.CheckNodeParent(e.Node);
            this.CheckNodeChildren(e.Node);
            this.treeEntityRelatedTypes.AfterCheck += new TreeViewEventHandler(treeEntityRelatedTypes_AfterCheck);
        }

        private void CheckNodeChildren(TreeNode node)
        {
            foreach (TreeNode child in node.Nodes)
            {
                child.Checked = node.Checked;
                CheckNodeChildren(child);
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

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Title = this.textTitle.Text;
            if (String.IsNullOrWhiteSpace(this.Title))
            {
                MessageBox.Show("The report's title cannot be empty!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.ClearPropertySelection();
            this.PopulatePropertySelection(this.treeEntityRelatedTypes.Nodes);

            foreach (var entitySelector in this.PropertySelection)
            {
                if (entitySelector.Value.Count == 0)
                {
                    MessageBox.Show("The entity named [" + entitySelector.Key + "] must have at least one property selected!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            this.Close();            
        }

        private void ClearPropertySelection()
        {
            this.PropertySelection = new Dictionary<string, List<string>>();
            this.PropertySelection.Add(this.MainEntity.Name, new List<string>());
            if (this.ChildEntities != null && this.ChildEntities.Count > 0)
            {
                foreach (var chid in this.ChildEntities)
                {
                    this.PropertySelection.Add(chid, new List<string>());
                }
            }
        }

        private void PopulatePropertySelection(TreeNodeCollection nodes)
        {
            EntityAdapterAttribute attribute;
            if (nodes != null && nodes.Count > 0)
            {
                foreach (TreeNode node in nodes)
                {
                    if (node.Checked)
                    {
                        if (node.Tag != null && node.Tag is EntityAdapterAttribute)
                        {
                            attribute = ((EntityAdapterAttribute)node.Tag);
                            string entityName = attribute.GetEntityName();
                            if (this.PropertySelection.ContainsKey(entityName))
                            {
                                this.PropertySelection[entityName].Add(attribute.Name);
                            }
                        }
                        else
                            this.PopulatePropertySelection(node.Nodes);
                    }
                }
            }
        }
        
        private void FormReportSettings_Load(object sender, EventArgs e)
        {
            this.textTitle.Focus();
            if (this.MainEntity != null)
            {
                this.ClearPropertySelection();
                this.FillTree(null, this.MainEntity);
                this.treeEntityRelatedTypes.AfterCheck += new TreeViewEventHandler(treeEntityRelatedTypes_AfterCheck);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Title = "";
            this.Close();            
        }

        private void ckGenerateCrossTabReport_CheckedChanged(object sender, EventArgs e)
        {
            this.GenerateCrossTabReport = ckGenerateCrossTabReport.Checked;
        }
    }
}

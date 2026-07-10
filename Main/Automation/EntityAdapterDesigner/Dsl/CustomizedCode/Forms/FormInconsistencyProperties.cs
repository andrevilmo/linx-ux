using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Linx.Tools;
using Microsoft.VisualStudio.Modeling;

namespace Linx.EntityAdapterDesigner.CustomizedCode
{
    public partial class FormInconsistencyProperties : Form
    {
        List<EntityAdapterProperty> _inconsistentProperties;
        TreeNodeCollection _targetEntity;
        List<EntityJoinRelation> _relationPropertyList = new List<EntityJoinRelation>();

        public FormInconsistencyProperties()
        {
            InitializeComponent();
        }

        public FormInconsistencyProperties(List<EntityAdapterProperty> inconsistentProperties, TreeNodeCollection targetEntity)
            : this()
        {
            this._inconsistentProperties = inconsistentProperties;
            this._targetEntity = targetEntity;
            this.LodData();
        }

        private void LodData()
        {
            if (_targetEntity != null && _inconsistentProperties != null)
            {
                this.PrepareDataSource();

                this.dataGridViewTextSourceProperty.HeaderText = "Property Not Found";

                this.dataGridViewComboTargetProperty.HeaderText = "Related Property";

                Action<TreeNode> action = null;
                action = (node) =>
                    {
                        if (node.Tag is PublicationProperty)
                            this.dataGridViewComboTargetProperty.Items.Add(node.Name.Left("#") + "." + node.Name.Right("."));

                        foreach (TreeNode innerNode in node.Nodes)
                        {
                            action(innerNode);
                        }
                    };

                foreach (TreeNode node in _targetEntity)
                {
                    action(node);
                }

            }
        }


        private TreeNode GetNode(string key)
        {
            TreeNode result = null;
            Action<TreeNode> action = null;
            action = (node) =>
            {
                this.dataGridViewComboTargetProperty.Items.Add(node.Name.Left("#") + "." + node.Name.Right("."));

                if (node.Name.Left("#") + "." + node.Name.Right(".") == key)
                    result = node;
                else
                {
                    foreach (TreeNode innerNode in node.Nodes)
                    {
                        action(innerNode);

                        if (result != null)
                            break;
                    }
                }
            };

            foreach (TreeNode node in _targetEntity)
            {                
                action(node);

                if (result != null)
                    break;
            }

            return result;
        }

        private void PrepareDataSource()
        {

            _relationPropertyList.Clear();

            foreach (var property in _inconsistentProperties.OrderBy(e => e.Name))
            {
                _relationPropertyList.Add(new EntityJoinRelation() { SourceProperty = property.Name, TargetProperty = String.Empty });
            }

            entityJoinRelationBindingSource.DataSource = _relationPropertyList;

        }

        private void btnApplyOrder_Click(object sender, EventArgs e)
        {
            this.ApplyChanges();
        }

        private void ApplyChanges()
        {

            if (_relationPropertyList.Where(e => e.TargetProperty.IsNullOrEmpty() || e.SourceProperty.IsNullOrEmpty()).Count() > 0)
            {
                MessageBox.Show(null, "All relations must be filled!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string sourceProperties = String.Empty, targetProperties = String.Empty;
            TreeNode node;
            EntityAdapterProperty prop;
            foreach (var elementRelation in _relationPropertyList)
            {
                node = GetNode(elementRelation.TargetProperty);
                if (node != null)
                {
                    node.Checked = true;
                    prop = _inconsistentProperties.Where(e => e.Name == elementRelation.SourceProperty).FirstOrDefault();
                    if (prop != null)
                        prop.DataRelationKey = node.Name;
                }
            }


            this.Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listEntities_DoubleClick(object sender, EventArgs e)
        {
            this.ApplyChanges();
        }

    }


}

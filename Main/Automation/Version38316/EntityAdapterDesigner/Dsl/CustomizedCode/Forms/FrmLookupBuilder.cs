using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Linx.Tools;
using System.Reflection;
using System.IO;
using System.Collections;
using System.Data.Entity.Core.Objects.DataClasses;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Linx.EntityAdapterDesigner.CustomCode
{
    public partial class FrmLookUpBuilder : Form
    {
        private int edmMaxTreeLevel = 3;
        private bool isOk = false;
        private string entitySourceBase = "", entitySource = "";
        Dictionary<string, string> specializedClasses = new Dictionary<string, string>();
        private LookUpAdapter lookUp;
        public LookUpAdapter LookUp
        {
            get { return lookUp; }
            set
            {
                if (value != lookUp)
                {
                    lookUp = value;
                    if (lookUp.EntityAdapter.IsNull() && lookUp.EntityDataModel.IsNull())
                    {
                        MessageBox.Show("This LookUp is not linked to a View/DataContext.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.cmbEdmTypes.Enabled = false;
                        this.treeEdmRelatedTypes.Enabled = false;
                        this.btApply.Enabled = false;
                        return;
                    }

                    //Check level
                    if (!lookUp.EntityAdapter.IsNull())
                    {
                        lookUp.EntityAdapter.CheckEdmTreeMaximumLevel();
                        edmMaxTreeLevel = lookUp.EntityAdapter.EdmTreeMaximumLevel;
                    }

                    //Check base class
                    lookUp.UpdateBaseClassInfo();

                    this.cmbEdmTypes.Enabled = lookUp.RelationName.IsNullOrEmpty() && lookUp.BaseLookUpAdapter == null && lookUp.DerivedLookUpAdapters.Count() == 0;
                    Model = lookUp.GetCurrentDataModel();

                    this.Text += String.Format(" (DataContext Max Tree Level = {0})", edmMaxTreeLevel);
                }
            }
        }
        private EntityDataModel model;
        public EntityDataModel Model
        {
            get { return model; }
            set
            {
                if (value != model)
                {
                    model = value;
                    if (model != null)
                        this.FillTypes();
                }
            }
        }

        public FrmLookUpBuilder()
        {
            InitializeComponent();
            this.treeEdmRelatedTypes.AfterCheck += new TreeViewEventHandler(treeEdmRelatedTypes_AfterCheck);
        }

        private bool GetIsEntityObject(Type type)
        {
            if (type == null || type.BaseType == null)
                return false;
            else
            {
                if (type.BaseType.Name == "EntityObject")
                    return true;
                else
                    return this.GetIsEntityObject(type.BaseType);
            }
        }



        private void CheckTree(TreeNodeCollection nodes)
        {
            List<TreeNode> deletedNodes = new List<TreeNode>();
            LookUpProperty[] properties;
            foreach (TreeNode node in nodes)
            {
                if (node.Tag.ToString() == Linx.EntityAdapterDesigner.CustomizedCode.EdmReader.IsProperty)
                {
                    properties = this.lookUp.LookUpProperties.Where(e => e.EdmKey == node.Name).ToArray();
                    node.Checked = (properties.Length > 0);
                    if (properties.Length == 0)
                    {
                        //Verify properties on base type
                        properties = this.lookUp.GetInheritanceProperties().Where(e => e.EdmKey == node.Name).ToArray();
                        if (properties.Length > 0)
                            deletedNodes.Add(node);

                        //Verify properties on derived types
                        if (properties.Length == 0)
                        {
                            properties = this.lookUp.GetDerivedProperties().Where(e => e.EdmKey == node.Name).ToArray();
                            if (properties.Length > 0)
                                deletedNodes.Add(node);
                        }
                    }
                }
                else
                    this.CheckTree(node.Nodes);
            }

            //Remove parent nodes
            foreach (TreeNode deletedNode in deletedNodes)
                nodes.Remove(deletedNode);
        }

        private List<string> GetPKOfEntity(string EntityName)
        {
            List<String> pks = new List<string>();
            Type type = this.model.EdmInfo.GetTypes().Where(e => e.Name == EntityName).FirstOrDefault();

            if (!type.IsNull())
            {
                MemberInfo[] members = type.GetMembers();

                foreach (MemberInfo member in members)
                {
                    if (member.MemberType.ToString() == "Property" && !(member.Name.InList("EntityKey", "EntityState"))
                            && member.ToString().IndexOf("EntityCollection`1", StringComparison.CurrentCultureIgnoreCase) < 0
                            && member.ToString().IndexOf("EntityReference`1", StringComparison.CurrentCultureIgnoreCase) < 0)
                    {

                        //Get Attributes of field
                        object[] attributes = member.GetCustomAttributes(true);

                        foreach (object attrib in attributes)
                        {
                            if (attrib.GetType().Name == "EdmScalarPropertyAttribute")
                            {
                                if (((System.Data.Objects.DataClasses.EdmScalarPropertyAttribute)attrib).EntityKeyProperty)
                                    pks.Add(member.Name.PrepareName());

                                break;
                            }
                        }
                    }
                }
            }

            return pks;
        }

        private bool ExistsAllFields(string fields, LookUpAdapter entityRef)
        {
            bool exists = true;

            if (!fields.IsNullOrEmpty())
            {
                foreach (string field in fields.Split(new char[] { ',' }))
                {
                    if (entityRef.LookUpProperties.Where(e => e.Name == field.Trim()).Count() == 0)
                    {
                        exists = false;
                        break;
                    }
                }
            }
            else
                exists = false;

            return exists;
        }

        private bool ContainsMemberName(IList collection, string name)
        {
            string propertyName;
            foreach (object element in collection)
            {
                propertyName = element.GetPropertyValue("Name") as string;
                if (!propertyName.IsNullOrEmpty() && propertyName == name)
                    return true;
            }
            return false;
        }

        private void RemoveMemberByName(IList collection, string name)
        {
            string propertyName;
            object targetElement = null;
            foreach (object element in collection)
            {
                propertyName = element.GetPropertyValue("Name") as string;
                if (!propertyName.IsNullOrEmpty() && propertyName == name)
                {
                    targetElement = element;
                    break;
                }
            }
            if (targetElement != null)
                collection.Remove(targetElement);
        }

        private void ApplyChanges()
        {
            if (entitySource != this.cmbEdmTypes.SelectedItem.ToString())
                this.lookUp.Name = "LookUp" + this.cmbEdmTypes.SelectedItem.ToString().PrepareName();

            if (!this.model.EdmInfo.IsNull() && !this.model.EdmInfo.IsDbContext && this.model.EdmInfo.ReadResourceContent().IsNullOrEmpty())
                return;

            //Save custom configurations
            List<LookUpProperty> propertiesList = this.lookUp.LookUpProperties.Where(e => !e.IsCustomized).OrderBy(e => e.Name).ToList();
            List<LookUpProperty> propertiesOrderList = this.lookUp.LookUpProperties.ToList();
            LookUpProperty compare;

            //Update relations
            this.lookUp.EntityRelations = "";
            this.UpdateEntityRelations(treeEdmRelatedTypes.Nodes);

            //Remove no custom
            for (int idx = this.lookUp.LookUpProperties.Count - 1; idx >= 0; idx--)
            {
                if (!this.lookUp.LookUpProperties[idx].IsCustomized)
                    this.lookUp.LookUpProperties.RemoveAt(idx);
            }

            //Add properties
            this.AddProperties(this.treeEdmRelatedTypes.Nodes);

            //Restore custom configurations
            for (int propIndex = 0; propIndex < propertiesList.Count; propIndex++)
            {
                compare = this.lookUp.LookUpProperties.Where(e => !e.IsDeleted && e.EdmKey == propertiesList[propIndex].EdmKey).FirstOrDefault();
                if (compare != null)
                {
                    compare.Name = propertiesList[propIndex].Name;
                    compare.IsBrowsable = propertiesList[propIndex].IsBrowsable;
                    compare.IsCustomized = propertiesList[propIndex].IsCustomized;
                    compare.DisplayName = (propertiesList[propIndex].DisplayName.IsNullOrEmpty() ? propertiesList[propIndex].Name : propertiesList[propIndex].DisplayName);
                    compare.EntityPropertyRelated = propertiesList[propIndex].EntityPropertyRelated;
                    if (compare.DomainName.IsNullOrEmpty())
                        compare.DomainName = propertiesList[propIndex].DomainName;
                }
            }

            //Adjust last order
            this.lookUp.Reorder(propertiesOrderList);
            
            this.isOk = true;
            this.Close();
        }

        private void UpdateEntityRelations(TreeNodeCollection nodes)
        {
            bool executeReentrant;

            if (this.lookUp != null)
            {
                foreach (TreeNode node in nodes)
                {
                    executeReentrant = true;
                    if (node.Parent != null)
                    {
                        if (node.Tag.ToString() == "IsEntity")
                        {
                            if (("#" + this.lookUp.EntityRelations + "#").Contains("#" + node.Name + "#"))
                                executeReentrant = false;
                            else
                                this.lookUp.EntityRelations += (this.lookUp.EntityRelations.IsNullOrEmpty() ? "" : "#") + node.Name;
                        }
                    }
                    if (executeReentrant)
                        this.UpdateEntityRelations(node.Nodes);
                }
            }
        }

        private bool HasParentNullable(TreeNode node)
        {
            TreeNode parentNode = node.Parent;
            while (parentNode != null)
            {
                if (parentNode.Text.Length > 16 && parentNode.Text.Left(16) == "0..1 [ZeroOrOne]")
                    return true;
                parentNode = parentNode.Parent;
            }

            return false;
        }

        private void AddProperties(TreeNodeCollection nodes)
        {
            if (nodes != null)
            {
                foreach (TreeNode node in nodes)
                {
                    if (node.Checked)
                    {
                        if (node.Tag.ToString() == Linx.EntityAdapterDesigner.CustomizedCode.EdmReader.IsProperty)
                        {
                            if (this.lookUp.LookUpProperties.Where(e => !e.IsDeleted && e.IsCustomized && e.EdmKey == node.Name).Count() == 0)
                            {
                                LookUpProperty property = new LookUpProperty(this.lookUp.Partition);
                                property.Name = node.Name.Right(".").PrepareName();

                                //Check repetitions
                                int propsCnt = this.lookUp.LookUpProperties.Count(e => e.Name == property.Name);
                                if (propsCnt > 0)
                                    property.Name = property.Name + propsCnt.ToString();

                                property.Datatype = node.Text.Extract(" [", "] "); ;
                                if ((node.Parent != null && node.Parent.Parent != null) && ((property.Datatype != "System.String") && HasParentNullable(node) && !property.Datatype.Contains("System.Nullable<")))
                                    property.Datatype = "System.Nullable<" + property.Datatype + ">";
                                property.IsBrowsable = property.Datatype.IndexOf("System.Guid") < 0;
                                property.EdmKey = node.Name;
                                property.IsPrimaryKey = node.Text.IndexOf("(:PK:)") >= 0;
                                property.DisplayName = node.Name.Right(".").Replace("_", " ").Proper() + (propsCnt > 0 ? propsCnt.ToString() : "");
                                property.IsCustomized = true;
                                this.lookUp.LookUpProperties.Add(property);
                                property.Precision = ((int)(this.model.EdmInfo.GetFieldPrecision(property) * 10)).ToString();
                                property.DataFormatString = this.GetDataFormatString(property);
                                property.DomainName = this.model.EdmInfo.GetDomainName(property.GetEdmEntityName(), node.Name.Right("."));
                            }
                        }
                        else
                            this.AddProperties(node.Nodes);
                    }
                }
            }
        }

        private void FillTypes()
        {
            bool existsPrimaryUpdate = false;

            if (this.model != null && !this.model.Path.IsNullOrEmpty() && this.model.EdmInfo == null)
            {
                model.LoadEdmInformation();
            }

            if (this.model != null && this.lookUp != null)
            {
                if (this.model.Path.IsNullOrEmpty() || !System.IO.File.Exists(this.model.Path))
                    MessageBox.Show("The DataContext file is empty or does not exists. Check the property Path on DataContext element.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    cmbEdmTypes.Items.Clear();

                    var edmTypes = this.model.EdmInfo.GetEdmsSet();

                    foreach (var memberType in edmTypes)
                    {

                        if (!memberType.IsAbstract)
                        {
                            cmbEdmTypes.Items.Add(memberType.Name);

                            if (this.lookUp.EntitySource == memberType.Name)
                                existsPrimaryUpdate = true;
                        }

                        //Inherited types
                        foreach (Type specializedType in edmTypes.Where(e => !e.IsAbstract))
                        {
                            if (memberType.IsBaseTypeOf(specializedType) && !specializedClasses.ContainsKey(specializedType.Name))
                            {
                                cmbEdmTypes.Items.Add(specializedType.Name);
                                specializedClasses.Add(specializedType.Name, memberType.Name);
                                if (this.lookUp.EntitySource == specializedType.Name)
                                    existsPrimaryUpdate = true;
                            }
                        }

                    }
                }
            }

            //Save primary and secondary entities
            entitySource = this.lookUp.EntitySource;
            entitySourceBase = this.lookUp.EntitySourceBase;

            if (existsPrimaryUpdate)
                cmbEdmTypes.SelectedItem = entitySource;
            else //Reset updatable entities
            {
                this.lookUp.EntitySource = "";
                this.lookUp.EntitySourceBase = "";
            }

            this.CheckTree(this.treeEdmRelatedTypes.Nodes);

        }

        private string GetDataFormatString(LookUpProperty property)
        {
            if (!property.Datatype.Contains("[]") && property.Datatype.ToLower().Contains("datetime"))
                return "d";

            if (!property.Datatype.Contains("[]") && (property.Datatype.ToLower().Contains("decimal") || property.Datatype.ToLower().Contains("float") || property.Datatype.ToLower().Contains("double")))
                return "N" + int.Parse((10 * (decimal.Parse(property.Precision) - int.Parse(property.Precision))).ToString()).ToString().PadLeft(2, '0');

            return String.Empty;
        }

        private void cmbEdmTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            model.EdmInfo.FillTree(treeEdmRelatedTypes, (string)this.cmbEdmTypes.SelectedItem, () =>
            {
                this.lookUp.EntitySource = this.cmbEdmTypes.SelectedItem.ToString();
                if (specializedClasses.ContainsKey(this.lookUp.EntitySource))
                    this.lookUp.EntitySourceBase = specializedClasses[this.lookUp.EntitySource];
                else
                    this.lookUp.EntitySourceBase = "";
            }, edmMaxTreeLevel);
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btApply_Click(object sender, EventArgs e)
        {
            this.ApplyChanges();
        }

        private void treeEdmRelatedTypes_AfterCheck(object sender, TreeViewEventArgs e)
        {
            this.treeEdmRelatedTypes.AfterCheck -= new TreeViewEventHandler(treeEdmRelatedTypes_AfterCheck);
            this.CheckNodeParent(e.Node);
            this.CheckNodeChildren(e.Node);
            this.treeEdmRelatedTypes.AfterCheck += new TreeViewEventHandler(treeEdmRelatedTypes_AfterCheck);
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

        private void FrmLookUpBuilder_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Restore primary and secondary entities
            if (!isOk)
            {
                this.lookUp.EntitySource = entitySource;
                this.lookUp.EntitySourceBase = entitySourceBase;
            }
        }

    }
}

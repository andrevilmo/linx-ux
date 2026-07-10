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

namespace Linx.EntityAdapterDesigner.CustomCode
{
    public partial class FrmLocalEntityBuilder : Form
    {
        private EntityAdapter _entity;
        public EntityAdapter Entity
        {
            get { return _entity; }
            set
            {
                if (value != _entity)
                {
                    _entity = value;
                    if (_entity != null)
                    {
                        var localEntity = _entity.GetLocalEntityAdapter();
                        if (localEntity != null)
                        {
                            this.Text += " - " + localEntity.Name;
                            this.FillTree(null, localEntity);
                        }
                    }
                }
            }
        }

        public FrmLocalEntityBuilder()
        {
            InitializeComponent();
            this.treeEntityRelatedTypes.AfterCheck += new TreeViewEventHandler(treeEntityRelatedTypes_AfterCheck);
        }
        
        private void CheckTree(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Tag is EntityAdapterAttribute)
                    node.Checked = (this._entity.EntityAdapterProperties.Where(e => e.DataRelationKey == node.Name).Count() > 0);                    
                else
                    this.CheckTree(node.Nodes);
            }
        }


        private string GetDisplayName(string name, string displayName)
        {
            if (_entity.PropertyOrder == AttributeOrder.DisplayName)
                return displayName + "(" + name + ")";
            else
                return name + "(" + displayName + ")";            
        }
        

        private void FillTree(TreeNode parentNode, EntityAdapter currentEntity, string parentPath = "")
        {
            if (currentEntity == null)
                return;

            if (parentNode == null)
                this.treeEntityRelatedTypes.Nodes.Clear();


            string key = (parentPath.IsNullOrEmpty() ? "" : parentPath + ".") + currentEntity.Name;
            TreeNode entityNode, referecesNode, refNode;
            List<EntityAdapterAttribute> members = currentEntity.GetAllInheritanceAttributes();

            //Add entity
            entityNode = (parentNode == null ? this.treeEntityRelatedTypes.Nodes.Add(key, GetDisplayName(currentEntity.Name, currentEntity.DisplayName), 0, 0) : parentNode.Nodes.Add(key, GetDisplayName(currentEntity.Name, currentEntity.DisplayName), 0, 0));
            entityNode.Tag = "IsEntity";

            EntityAdapter parentRelation = currentEntity.GetTargetEntity();
            if (parentRelation != null)
            {
                referecesNode = entityNode.Nodes.Add("Parent Reference", "Parent Reference", 1, 1);
                referecesNode.Tag = "IsReference";

                //Add Reference
                this.FillTree(referecesNode, parentRelation, key);
            }
            else
                referecesNode = null;

            //Add members
            foreach (EntityAdapterAttribute member in members.OrderBy(e => e.DisplayName))
            {
                if (referecesNode == null || !referecesNode.Nodes.ContainsKey(key + "." + member.Name))
                {
                    refNode = entityNode.Nodes.Add(key + "." + member.Name, GetDisplayName(member.Name, member.DisplayName), 3, 3);
                    refNode.Tag = member;
                }
            }

            //Expand Nodes
            if (parentNode == null)
            {
                entityNode.Expand();
                if (referecesNode != null)
                    referecesNode.Expand();
                CheckTree(this.treeEntityRelatedTypes.Nodes);
            }
        }


        private void UpdateParentRelation(EntityAdapter entityRef)
        {
            EntityAdapter entityParent = EntityAdapterReferencesTargetEntityAdapter.GetTargetEntityAdapter(entityRef);
            if (entityParent != null)
            {
                EntityAdapterReferencesTargetEntityAdapter link = EntityAdapterReferencesTargetEntityAdapter.GetLinkToTargetEntityAdapter(entityRef);
                if (link != null)
                {
                    string parentKeyFields = "", detailKeyFields = "";
                    
                    List<String> properties = entityParent.EntityAdapterProperties.Where(e => entityParent.IsPrimaryKey(e)).Select(e => e.Name).ToList();
                    foreach (string property in properties)
                    {
                        parentKeyFields += (parentKeyFields.IsNullOrEmpty() ? "" : ",") + property;
                        detailKeyFields += (detailKeyFields.IsNullOrEmpty() ? "" : ",") + property;
                    }

                    if (!this.ExistsAllFields(link.ParentKeyFields, entityParent))
                        link.ParentKeyFields = parentKeyFields;
                    if (!this.ExistsAllFields(link.DetailKeyFields, entityRef))
                        link.DetailKeyFields = detailKeyFields;
                }
            }
        }

        private bool ExistsAllFields(string fields, EntityAdapter entityRef)
        {
            bool exists = true;

            if (!fields.IsNullOrEmpty())
            {
                foreach (string field in fields.Split(new char[] { ',' }))
                {
                    if (entityRef.EntityAdapterProperties.Where(e => e.Name == field.Trim()).Count() == 0)
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

        private void ApplyChanges()
        {
            this.Entity.PrimaryEntity = String.Empty;
            //Save configurations
            List<EntityAdapterProperty> propertiesList = this.Entity.EntityAdapterProperties.Where(e => !e.IsCustomized).OrderBy(e => e.Name).ToList();
            EntityAdapterProperty compare;
            
            //Remove no custom
            for (int idx = this.Entity.EntityAdapterProperties.Count - 1; idx >= 0; idx--)
            {
                if (!this.Entity.EntityAdapterProperties[idx].IsCustomized)
                    this.Entity.EntityAdapterProperties.RemoveAt(idx);
            }
            //Add properties
            this.AddProperties(this.treeEntityRelatedTypes.Nodes);

            //Restore custom configurations
            for (int propIndex = 0; propIndex < propertiesList.Count; propIndex++)
            {
                compare = this.Entity.EntityAdapterProperties.Where(e => !e.IsDeleted && !e.IsCustomized && e.DataRelationKey == propertiesList[propIndex].DataRelationKey).FirstOrDefault();
                if (compare != null)
                {
                    compare.Name = propertiesList[propIndex].Name;
                    compare.IsBrowsable = propertiesList[propIndex].IsBrowsable;
                    compare.ConnectedAttribute = propertiesList[propIndex].ConnectedAttribute;
                    compare.IsEditable = propertiesList[propIndex].IsEditable;
                    compare.DisplayName = (propertiesList[propIndex].DisplayName.IsNullOrEmpty() ? propertiesList[propIndex].Name : propertiesList[propIndex].DisplayName);
                    compare.DisplayControl = propertiesList[propIndex].DisplayControl;
                    compare.GroupName = propertiesList[propIndex].GroupName;
                    compare.DefaultValue = propertiesList[propIndex].DefaultValue;
                    compare.DisplayOrder = propertiesList[propIndex].DisplayOrder;
                    compare.TargetKeyName = propertiesList[propIndex].TargetKeyName;
                    compare.DomainName = propertiesList[propIndex].DomainName;
                    compare.IsCompulsory = propertiesList[propIndex].IsCompulsory;
                    compare.CustomValidationMethod = propertiesList[propIndex].CustomValidationMethod;
                    compare.CustomAttributes = propertiesList[propIndex].CustomAttributes;
                    compare.AggregationFunction = propertiesList[propIndex].AggregationFunction;
                    compare.IsPublicationSuggestion = propertiesList[propIndex].IsPublicationSuggestion;
                    compare.RemoveValidations = propertiesList[propIndex].RemoveValidations;
                    compare.KpiName = propertiesList[propIndex].KpiName;
                    compare.OrderBySequence = propertiesList[propIndex].OrderBySequence;
                    compare.OrderByOrientation = propertiesList[propIndex].OrderByOrientation;
                    compare.KpiRelatedAttribute = propertiesList[propIndex].KpiRelatedAttribute;
                    compare.Filter = propertiesList[propIndex].Filter;
                    compare.IsRequiredBeforeSearching = propertiesList[propIndex].IsRequiredBeforeSearching;
                    compare.IgnoreMetaData = propertiesList[propIndex].IgnoreMetaData;
                }
            }

            //Adjust Order by Name
            propertiesList = this.Entity.EntityAdapterProperties.OrderBy(e => e.Name).ToList();
            for (int propIndex = 0; propIndex < propertiesList.Count; propIndex++)
            {
                this.Entity.EntityAdapterProperties.Move(propertiesList[propIndex], propIndex);
            }

            this.UpdateParentRelation(this.Entity);

            this.Close();
        }

        private void AddProperties(TreeNodeCollection nodes)
        {
            EntityAdapterAttribute attribute;
            if (nodes != null)
            {
                foreach (TreeNode node in nodes)
                {
                    if (node.Checked)
                    {
                        if (node.Tag != null && node.Tag is EntityAdapterAttribute)
                        {
                            if (this.Entity.EntityAdapterProperties.Where(e => !e.IsDeleted && e.IsCustomized && e.DataRelationKey == node.Name).Count() == 0)
                            {
                                attribute = ((EntityAdapterAttribute)node.Tag);
                                EntityAdapterProperty property = new EntityAdapterProperty(this._entity.Partition);                                
                                property.EdmKey = String.Empty;
                                property.DataRelationKey = node.Name;                                
                                property.Name = attribute.Name;

                                //Check repetitions
                                int propsCnt = this.Entity.EntityAdapterProperties.Count(e => e.Name == property.Name);
                                if (propsCnt > 0)
                                    property.Name = property.Name + propsCnt.ToString();

                                property.Datatype = attribute.Datatype;
                                property.Precision = attribute.Precision;
                                property.DataFormatString = attribute.DataFormatString;
                                property.IsBrowsable = attribute.IsBrowsable;
                                property.Description = attribute.Description;
                                property.IsBrowsable = attribute.IsBrowsable;
                                property.ConnectedAttribute = attribute.ConnectedAttribute;
                                property.IsEditable = attribute.IsEditable;
                                property.DisplayName = (attribute.DisplayName.IsNullOrEmpty() ? attribute.Name : attribute.DisplayName) + (propsCnt > 0 ? propsCnt.ToString() : "");
                                property.DisplayControl = attribute.DisplayControl;
                                property.GroupName = attribute.GroupName;                                
                                property.DisplayOrder = attribute.DisplayOrder;                                
                                property.DomainName = attribute.DomainName;
                                property.IsCompulsory = attribute.IsCompulsory;
                                property.CustomValidationMethod = attribute.CustomValidationMethod;
                                property.CustomAttributes = attribute.CustomAttributes;
                                property.AggregationFunction = attribute.AggregationFunction;
                                property.IsPublicationSuggestion = attribute.IsPublicationSuggestion;
                                property.RemoveValidations = attribute.RemoveValidations;
                                property.KpiName = attribute.KpiName;                                
                                property.KpiRelatedAttribute = attribute.KpiRelatedAttribute;
                                property.Filter = (attribute is EntityAdapterProperty ?  ((EntityAdapterProperty)attribute).Filter : (attribute is EntityAdapterPublicationProperty ?  ((EntityAdapterPublicationProperty)attribute).Filter : ""));
                                property.DefaultValue = (attribute is EntityAdapterProperty ?  ((EntityAdapterProperty)attribute).DefaultValue : (attribute is EntityAdapterPublicationProperty ?  ((EntityAdapterPublicationProperty)attribute).DefaultValue : ""));
                                property.TargetKeyName = (attribute is EntityAdapterProperty ?  ((EntityAdapterProperty)attribute).TargetKeyName : (attribute is EntityAdapterPublicationProperty ?  ((EntityAdapterPublicationProperty)attribute).TargetKeyName : ""));
                                property.IsCustomized = false;

                                this._entity.EntityAdapterProperties.Add(property);
                            }
                        }
                        else
                            this.AddProperties(node.Nodes);
                    }
                }
            }
        }
        

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btApply_Click(object sender, EventArgs e)
        {
            this.ApplyChanges();
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

        

    }
}

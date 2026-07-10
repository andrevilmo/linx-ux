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
    public partial class FrmJoinEntityBuilder : Form
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
                    if (_entity != null && _entity.GetEntityAdapterRepresentation() != null)
                    {
                        this.Text += " - " + _entity.Name;
                        if (_entity.PrimaryEntity != _entity.GetEntityAdapterRepresentation().TargetEdmEntityName)
                            _entity.PrimaryEntity = _entity.GetEntityAdapterRepresentation().TargetEdmEntityName;
                        this.FillTree(null, _entity.GetEntityAdapterRepresentation());
                    }
                }
            }
        }

        public FrmJoinEntityBuilder()
        {
            InitializeComponent();
            this.treeEntityRelatedTypes.AfterCheck += new TreeViewEventHandler(treeEntityRelatedTypes_AfterCheck);
        }


        private void CheckTree(TreeNodeCollection nodes, bool checkStructureByName, List<string> checkedNames)
        {
            EntityAdapterProperty property;

            foreach (TreeNode node in nodes)
            {
                if (node.Tag is CustomizedCode.PublicationProperty)
                {
                    if (checkStructureByName)
                    {
                        if (!checkedNames.Contains(node.Name.Right(".")))
                        {
                            property = this._entity.EntityAdapterProperties.Where(e => e.Name == node.Name.Right(".")).FirstOrDefault();
                            if (property != null)
                            {
                                property.DataRelationKey = node.Name;
                                checkedNames.Add(property.Name);
                            }
                        }
                        else
                            property = null;
                    }
                    else
                        property = this._entity.EntityAdapterProperties.Where(e => e.DataRelationKey == node.Name).FirstOrDefault();

                    if (property != null)
                        node.Checked = true;                        
                }
                else
                    this.CheckTree(node.Nodes, checkStructureByName, checkedNames);
            }

        }


        private string GetDisplayName(string name, string displayName)
        {
            if (_entity.PropertyOrder == AttributeOrder.DisplayName)
                return displayName + "(" + name + ")";
            else
                return name + "(" + displayName + ")";            
        }
        

        private void FillTree(TreeNode parentNode, EntityAdapterRepresentation currentEntityPresentation)
        {
            if (currentEntityPresentation == null)
                return;

            if (parentNode == null)
                this.treeEntityRelatedTypes.Nodes.Clear();
            
            var parentLink = currentEntityPresentation.TargetEntityAdapterRepresentation == null ? null : EntityAdapterRepresentationReferencesTargetEntityAdapterRepresentation.GetLinkToTargetEntityAdapterRepresentation(currentEntityPresentation);
            var pubEntity = currentEntityPresentation.EntityAdapterDesignerRoot.GetPublishedEntityByRef(currentEntityPresentation.BusinessObject, currentEntityPresentation.TargetNameSpace, currentEntityPresentation.TargetEntityAdapterName);

            if (pubEntity == null)
                return;

            string key = currentEntityPresentation.Name + "#" + currentEntityPresentation.TargetNameSpace + "#" + currentEntityPresentation.TargetEntityAdapterName;
            TreeNode entityNode, referecesNode, refNode;

            //Add entity
            entityNode = (parentNode == null ? this.treeEntityRelatedTypes.Nodes.Add(key, GetDisplayName(currentEntityPresentation.Name, currentEntityPresentation.Name), 0, 0) : parentNode.Nodes.Add(key, (parentLink == null ? "" : parentLink.JoinType.ToString() + " ") + currentEntityPresentation.Name, 0, 0));
            if (parentLink != null)
                entityNode.Tag = parentLink.JoinType;

            if (currentEntityPresentation.SourceEntityAdapterRepresentations.Count > 0)
            {
                referecesNode = entityNode.Nodes.Add("Join References", "Join References" , 1, 1);
                referecesNode.Tag = "IsReference";

                //Add Reference
                foreach (var entityRelated in currentEntityPresentation.SourceEntityAdapterRepresentations)
                {
                    this.FillTree(referecesNode, entityRelated);
                }
            }
            else referecesNode = null;

            //Add members
            foreach (CustomizedCode.PublicationProperty member in pubEntity.Properties.OrderBy(e => GetDisplayName(e.Name, e.DisplayName)))
            {
               refNode = entityNode.Nodes.Add(key + "." + member.Name, GetDisplayName(member.Name, member.DisplayName), 3, 3);
               refNode.Tag = member;
            }

            //Expand Nodes
            if (parentNode == null)
            {
                entityNode.Expand();
                if (referecesNode != null)
                    referecesNode.Expand();
                List<string> checkedNames = new List<string>();
                bool checkStructureByName = this._entity.EntityAdapterProperties.Where(e => !e.IsCustomized && !e.DataRelationKey.IsNullOrEmpty()).Count() == 0;
                this.CheckTree(this.treeEntityRelatedTypes.Nodes, checkStructureByName, checkedNames);
                if (checkStructureByName)
                    this.SuggestProperties(checkedNames);                
            }
        }


        private void SuggestProperties(List<string> checkedNames)
        {
            var properties = this._entity.EntityAdapterProperties.Where(e => !e.IsCustomized && !checkedNames.Contains(e.Name)).ToList();
            if (properties.Count > 0)
            {
                CustomizedCode.FormInconsistencyProperties inconsistency = new CustomizedCode.FormInconsistencyProperties(properties, this.treeEntityRelatedTypes.Nodes);
                inconsistency.ShowDialog();
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
            //Check if has many EdmContexts
            if (this.Entity.HasAnyRepresentationAsEnumerableType())
                this.Entity.QueryReturnType = EntityQueryReturnType.IEnumerable;
            else
                this.Entity.QueryReturnType = EntityQueryReturnType.IQueryable;

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
                    compare.RestoreUserDefinition(propertiesList[propIndex], true);                    
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

        private bool CheckIsNullable(TreeNode node)
        {
            if (node != null)
            {
                if (node.Tag is EntityAdapterJoinType && ((EntityAdapterJoinType)node.Tag) == EntityAdapterJoinType.LeftJoin)
                    return true;
                else
                    return CheckIsNullable(node.Parent);
            }
            else
                return false;
        }

        private void AddProperties(TreeNodeCollection nodes)
        {
            bool hasNullableJoin;
            CustomizedCode.PublicationProperty attribute;
            if (nodes != null && nodes.Count > 0)
            {                
                foreach (TreeNode node in nodes)
                {
                    if (node.Checked)
                    {
                        if (node.Tag != null && node.Tag is CustomizedCode.PublicationProperty)
                        {
                            if (this.Entity.EntityAdapterProperties.Where(e => !e.IsDeleted && e.IsCustomized && e.DataRelationKey == node.Name).Count() == 0)
                            {
                                hasNullableJoin = CheckIsNullable(node);
                                attribute = ((CustomizedCode.PublicationProperty)node.Tag);
                                EntityAdapterProperty property = new EntityAdapterProperty(this._entity.Partition);
                                property.EdmKey = attribute.EdmKey;
                                property.DataRelationKey = node.Name;                                
                                property.Name = attribute.Name;

                                //Check repetitions
                                int propsCnt = this.Entity.EntityAdapterProperties.Count(e => e.Name == property.Name);
                                if (propsCnt > 0)
                                    property.Name = property.Name + propsCnt.ToString();

                                property.Datatype = (hasNullableJoin && !attribute.DataType.ToLower().Contains("string") && !attribute.IsNullable() ? "System.Nullable<" + attribute.DataType + ">" : attribute.DataType);
                                property.IsNull = hasNullableJoin || attribute.IsNullable();
                                property.Precision = attribute.Precision;
                                property.DataFormatString = attribute.DataFormatString;
                                property.IsBrowsable = attribute.IsBrowsable;
                                property.Description = String.Empty;
                                property.IsBrowsable = attribute.IsBrowsable;
                                property.ConnectedAttribute = String.Empty;
                                property.IsEditable = attribute.IsEditable;
                                property.DisplayName = (attribute.DisplayName.IsNullOrEmpty() ? attribute.Name : attribute.DisplayName) + (propsCnt > 0 ? propsCnt.ToString() : "");
                                property.DisplayControl = (DisplayControlType)Enum.Parse(typeof(DisplayControlType), attribute.DisplayControl);
                                property.GroupName = String.Empty;                                
                                property.DisplayOrder = -1;                                
                                property.DomainName = attribute.DomainName;
                                property.IsCompulsory = false;
                                property.CustomValidationMethod = String.Empty;
                                property.CustomAttributes = String.Empty;
                                property.AggregationFunction = UIAggregationFunctions.None;
                                property.IsPublicationSuggestion = false;
                                property.RemoveValidations = false;
                                property.KpiName = attribute.KpiName;
                                property.KpiRelatedAttribute = String.Empty;
                                property.Filter = String.Empty;
                                property.DefaultValue = attribute.DefaultValue;
                                property.TargetKeyName = String.Empty;
                                property.IsCustomized = false;
                                property.IsAutomaticSequency = attribute.IsAutomaticSequency;
                                property.LookUpSubscription = attribute.LookUpInfo;
                                property.Mask = attribute.Mask;
                                property.MaskType = attribute.MaskType;
                                property.IsPK = attribute.IsPrimaryKey;
                                property.IsFK = property.DataRelationKey.Left("#") != this.Entity.EntityAdapterRepresentation.Name;

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

        private void FrmJoinEntityBuilder_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

    }
}

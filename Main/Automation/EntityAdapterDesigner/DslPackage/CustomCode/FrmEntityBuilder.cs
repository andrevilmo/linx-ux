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

namespace Linx.EntityAdapterDesigner.CustomCode
{

    public partial class FrmEntityBuilder : Form
    {
        private bool isOk = false;
        private string primaryEntity = "", secondaryEntity = "";
        private EntityAdapter entity;
        public EntityAdapter Entity
        {
            get { return entity; }
            set
            {
                if (value != entity)
                {
                    entity = value;
                    Model = GetCurrentDataModel(entity);
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

        public FrmEntityBuilder()
        {
            InitializeComponent();
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

        private void FillTypes()
        {
            bool existsPrimaryUpdate = false;

            if (this.model != null && this.entity != null)
            {
                if (this.model.Path.IsNullOrEmpty() || !System.IO.File.Exists(this.model.Path))
                    MessageBox.Show("The Edm file is empty or does not exists. Check the property Path on Edm element.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    string propAsStr, propOfType;
                    Type memberType;
                    PropertyInfo[] properties;
                    Assembly assembly = Assembly.LoadFile(this.model.Path);
                    Type[] types = assembly.GetTypes();

                    cmbEdmTypes.Items.Clear();
                    foreach (Type type in types)
                    {
                        if (type.BaseType.Name == "ObjectContext")
                        {
                            this.txEdmContext.Text = type.FullName;

                            //Context Properties (Entities)
                            properties = type.GetProperties();

                            foreach (PropertyInfo property in properties)
                            {
                                propAsStr = property.ToString();

                                //Only Object Query
                                if (propAsStr.IndexOf("System.Data.Objects.ObjectQuery`1[") >= 0)
                                {
                                    //Get type of property
                                    propOfType = propAsStr.Replace("System.Data.Objects.ObjectQuery`1[", "").Replace("] " + property.Name, "");
                                    memberType = assembly.GetType(propOfType);
                                    if (this.GetIsEntityObject(memberType))
                                    {
                                        cmbEdmTypes.Items.Add(memberType.Name);
                                        if (this.entity.PrimaryEntity == memberType.Name)
                                            existsPrimaryUpdate = true;

                                        //Inherited types
                                        foreach (Type inheritedType in types)
                                        {
                                            if (inheritedType.BaseType.Equals(memberType))
                                            {
                                                cmbEdmTypes.Items.Add(inheritedType.Name);
                                                if (this.entity.PrimaryEntity == inheritedType.Name)
                                                    existsPrimaryUpdate = true;
                                            }
                                        }
                                    }
                                }
                            }


                            break;
                        }
                    }
                }
            }

            //Save primary and secondary entities
            primaryEntity = this.entity.PrimaryEntity;
            secondaryEntity = this.entity.SecondaryEntity;

            if (existsPrimaryUpdate)
            {
                cmbEdmTypes.SelectedItem = primaryEntity;
                cmbSecondaryTypes.SelectedItem = secondaryEntity;
            }
            else //Reset updatable entities
            {
                this.entity.PrimaryEntity = "";
                this.entity.SecondaryEntity = "";
            }

            this.CheckTree(this.treeEdmRelatedTypes.Nodes);
        }



        private void CheckTree(TreeNodeCollection nodes)
        {
            EntityAdapterProperty[] properties;
            foreach (TreeNode node in nodes)
            {
                if (node.Tag.ToString() == "IsProperty")
                {
                    properties = this.entity.EntityAdapterProperties.Where(e => e.EdmKey == node.Name).ToArray();
                    node.Checked = (properties.Length > 0);
                }
                else
                    this.CheckTree(node.Nodes);
            }
        }

        private void CheckUpdatableTreeByEntity(TreeNodeCollection nodes, string entityName, bool value)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Tag.ToString() == "IsProperty")
                {
                    if (this.GetIsEditableByEntity(node, entityName))
                        node.Checked = value;
                }
                else
                    this.CheckUpdatableTreeByEntity(node.Nodes, entityName, value);
            }
        }


        private void FillTree(TreeNode parentNode, string parentTypeName, string parentPath)
        {
            if (parentNode == null && this.cmbEdmTypes.SelectedItem.ToString() == this.Entity.PrimaryEntity && this.treeEdmRelatedTypes.Nodes.Count > 0)
                return;

            if (parentNode == null)
                this.treeEdmRelatedTypes.Nodes.Clear();

            if ((this.model != null && this.entity != null) && !cmbEdmTypes.SelectedItem.IsNullOrEmpty())
            {
                TreeNode entityNode, referecesNode, refNode;
                Type memberType, referenceType;
                string typeName = (parentNode == null ? cmbEdmTypes.SelectedItem.ToString() : parentTypeName);
                Assembly assembly = Assembly.LoadFile(this.model.Path);
                Type edmType = assembly.GetType(this.Model.TargetNamespace + "." + this.Model.Name);
                Type[] types = assembly.GetTypes();
                memberType = assembly.GetType(this.Model.TargetNamespace + "." + typeName);
                string relation, referenceOfType, referenceAlias, memberPropType, literalProps;
                MemberInfo[] members = memberType.GetMembers();

                //Add entity
                entityNode = (parentNode == null ? this.treeEdmRelatedTypes.Nodes.Add(typeName, typeName, 0, 0) : parentNode.Nodes.Add(parentNode.Name + "(" + typeName + ")", typeName, 0, 0));
                entityNode.Tag = "IsEntity";
                referecesNode = entityNode.Nodes.Add("Entity References", " Entity References", 1, 1);
                referecesNode.Tag = "IsReference";

                //Add References
                foreach (MemberInfo member in members)
                {
                    if ((member.MemberType.ToString() == "Property") &&
                        (member.ToString().IndexOf("EntityReference`1", StringComparison.CurrentCultureIgnoreCase) >= 0))
                    {
                        referenceOfType = member.ToString().Extract("EntityReference`1[", "] ");
                        referenceAlias = member.ToString().Extract("] ", "Reference");

                        var edmRelation = types.Where(item => item.Name == typeName).First().GetProperties().Where(item => item.Name == referenceAlias).First().GetCustomAttributes(true).Where(item => item.ToString() == "System.Data.Objects.DataClasses.EdmRelationshipNavigationPropertyAttribute").First() as System.Data.Objects.DataClasses.EdmRelationshipNavigationPropertyAttribute;
                        var edmRelation1 = assembly.GetCustomAttributes(true).Where(item => item.ToString() == "System.Data.Objects.DataClasses.EdmRelationshipAttribute" && (item as System.Data.Objects.DataClasses.EdmRelationshipAttribute).RelationshipName == edmRelation.RelationshipName).First() as System.Data.Objects.DataClasses.EdmRelationshipAttribute;

                        if (edmRelation1.Role1Name == edmRelation.TargetRoleName)
                            relation = edmRelation1.Role1Multiplicity == System.Data.Metadata.Edm.RelationshipMultiplicity.Many ? "* [Many] " : edmRelation1.Role1Multiplicity == System.Data.Metadata.Edm.RelationshipMultiplicity.One ? "1 [One] " : "0..1 [ZeroOrOne] ";
                        else
                            relation = edmRelation1.Role2Multiplicity == System.Data.Metadata.Edm.RelationshipMultiplicity.Many ? "* [Many] " : edmRelation1.Role2Multiplicity == System.Data.Metadata.Edm.RelationshipMultiplicity.One ? "1 [One] " : "0..1 [ZeroOrOne] ";

                        if (referenceOfType != "" && referenceAlias != "")
                        {
                            if (parentPath.IsNullOrEmpty() || ("." + parentPath + ".").IndexOf("." + referenceAlias + ".") < 0)
                            {
                                referenceType = assembly.GetType(referenceOfType);
                                refNode = referecesNode.Nodes.Add(referenceAlias, relation + referenceAlias, 2, 2);
                                refNode.Tag = "IsEntityReference";
                                this.FillTree(refNode, referenceType.Name, parentPath + (parentPath.IsNullOrEmpty() ? typeName : "") + "." + referenceAlias);
                            }
                        }
                    }
                }


                //Add members
                foreach (MemberInfo member in members)
                {
                    if (member.MemberType.ToString() == "Property" && !(member.Name.InList("EntityKey", "EntityState"))
                        && member.ToString().IndexOf("EntityCollection`1", StringComparison.CurrentCultureIgnoreCase) < 0
                        && member.ToString().IndexOf("EntityReference`1", StringComparison.CurrentCultureIgnoreCase) < 0)
                    {
                        if (member.ToString().IndexOf("System.Nullable`1[") >= 0)
                            memberPropType = member.ToString().Replace("System.Nullable`1[", "System.Nullable<").Replace("]", ">");
                        else
                            memberPropType = member.ToString();

                        //Get Attributes of field
                        object[] attributes = member.GetCustomAttributes(true);

                        literalProps = "";
                        foreach (object attrib in attributes)
                        {
                            if (attrib.GetType().Name == "EdmScalarPropertyAttribute")
                            {
                                if (((System.Data.Objects.DataClasses.EdmScalarPropertyAttribute)attrib).EntityKeyProperty)
                                    literalProps = literalProps + (literalProps == "" ? "" : ",") + "PK";

                                if (((System.Data.Objects.DataClasses.EdmScalarPropertyAttribute)attrib).IsNullable)
                                    literalProps = literalProps + (literalProps == "" ? "" : ",") + "Null";

                                if (literalProps != "")
                                    literalProps = " (:" + literalProps + ":)";

                                break;
                            }
                        }

                        if (!referecesNode.Nodes.ContainsKey(member.Name))
                        {
                            refNode = entityNode.Nodes.Add((parentPath.IsNullOrEmpty() ? typeName : parentPath) + "." + member.Name, memberPropType + literalProps, 3, 3);
                            refNode.Tag = "IsProperty";
                        }
                    }

                }

                //Expand Nodes
                if (parentNode == null)
                {
                    entityNode.Expand();
                    referecesNode.Expand();
                    this.Entity.PrimaryEntity = this.cmbEdmTypes.SelectedItem.ToString();
                    this.Entity.SecondaryEntity = "";
                    this.CheckUpdatableTreeByEntity(this.treeEdmRelatedTypes.Nodes, this.Entity.PrimaryEntity, true);
                    this.LoadSecondaryEntities();
                    this.cmbSecondaryTypes.SelectedItem = "";
                }

            }

        }


        private EntityDataModel GetCurrentDataModel(EntityAdapter entityRef)
        {
            EntityDataModel edm;

            if (entity != null)
            {
                edm = EntityAdapterReferencesEntityDataModel.GetEntityDataModel(entityRef);
                if (edm != null)
                    return edm;
                else
                {
                    EntityAdapter entityParent = EntityAdapterReferencesTargetEntityAdapter.GetTargetEntityAdapter(entityRef);
                    if (entityParent == null)
                        return null;
                    else
                        return GetCurrentDataModel(entityParent);
                }
            }
            else
                return null;

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
                    foreach (var property in entityParent.EntityAdapterProperties.Where(e => e.IsPK && e.EdmKey.Occurs(".") == 1))
                    {
                        parentKeyFields += (parentKeyFields.IsNullOrEmpty() ? "" : ",") + property.Name;
                        detailKeyFields += (detailKeyFields.IsNullOrEmpty() ? "" : ",") + property.Name;
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
            if (primaryEntity != this.cmbEdmTypes.SelectedItem.ToString())
            {
                this.Entity.Name = this.cmbEdmTypes.SelectedItem.ToString().Replace("_", " ").Proper().Replace(" ", "");
                if (this.Entity.Name == this.cmbEdmTypes.SelectedItem.ToString())
                    this.Entity.Name += "View";

                if (this.Entity.UILauoutColumns == 0)
                    this.Entity.UILauoutColumns = 2;
            }

            //Apply base types
            Assembly assembly = Assembly.LoadFile(this.model.Path);
            Type type = assembly.GetType(this.Model.TargetNamespace + "." + this.Entity.PrimaryEntity);
            if (type.BaseType != null && cmbEdmTypes.Items.Contains(type.BaseType.Name))
                this.Entity.PrimaryEntityBase = type.BaseType.Name;
            else
                this.Entity.PrimaryEntityBase = "";

            if (!this.Entity.SecondaryEntity.Extract("(", ")").IsNullOrEmpty())
            {
                type = assembly.GetType(this.Model.TargetNamespace + "." + this.Entity.SecondaryEntity.Extract("(", ")"));
                if (type.BaseType != null && cmbEdmTypes.Items.Contains(type.BaseType.Name))
                    this.Entity.SecondaryEntity = type.BaseType.Name;
                else
                    this.Entity.SecondaryEntity = "";
            }
            else
                this.Entity.SecondaryEntity = "";


            //Save custom configurations
            List<EntityAdapterProperty> propertiesList = this.Entity.EntityAdapterProperties.OrderBy(e => e.UIDisplayOrder).ToList();
            EntityAdapterProperty compare;

            //Add properties
            this.Entity.EntityAdapterProperties.Clear();
            this.AddProperties(this.treeEdmRelatedTypes.Nodes);

            //Restore custom configurations
            for (int propIndex = 0; propIndex < propertiesList.Count; propIndex++)
            {
                compare = this.Entity.EntityAdapterProperties.Where(e => e.EdmKey == propertiesList[propIndex].EdmKey).FirstOrDefault();
                if (compare != null)
                {
                    compare.Name = propertiesList[propIndex].Name;                    
                    compare.IsBrowsable = propertiesList[propIndex].IsBrowsable;
                    compare.UIConnectedAttribute = propertiesList[propIndex].UIConnectedAttribute;
                    compare.IsEditable = propertiesList[propIndex].IsEditable;
                    compare.UICaption = propertiesList[propIndex].UICaption;
                    compare.UIDisplayControl = propertiesList[propIndex].UIDisplayControl;
                    compare.UIGroup = propertiesList[propIndex].UIGroup;
                    compare.DefaultValue = propertiesList[propIndex].DefaultValue;
                    compare.UIValidationValues = propertiesList[propIndex].UIValidationValues;
                    compare.UIDisplayOrder = propertiesList[propIndex].UIDisplayOrder;
                }
            }

            //Adjust Order by UIDisplayOrder
            propertiesList = this.Entity.EntityAdapterProperties.OrderBy(e => e.UIDisplayOrder).ToList();
            for (int propIndex = 0; propIndex < propertiesList.Count; propIndex++)
            {
                this.Entity.EntityAdapterProperties.Move(propertiesList[propIndex], propIndex);
            }

            //Update Relation
            this.UpdateParentRelation(this.entity);

            this.isOk = true;
            this.Close();
        }

        private void AddProperties(TreeNodeCollection nodes)
        {
            if (nodes != null)
            {
                foreach (TreeNode node in nodes)
                {
                    if (node.Checked)
                    {
                        if (node.Tag.ToString() == "IsProperty")
                        {
                            EntityAdapterProperty property = new EntityAdapterProperty(this.Entity.Partition);
                            property.Name = node.Name.Right(".").Replace("_", " ").Proper().Replace(" ", "");
                            property.UIDisplayOrder = node.Index;
                            property.Datatype = node.Text.Left(" ");
                            property.IsBrowsable = property.Datatype.IndexOf("System.Guid") < 0;
                            property.UIConnectedAttribute = String.Empty;
                            property.IsPK = node.Text.IndexOf("(:PK:)") >= 0;
                            property.IsFK = node.Text.IndexOf("(:PK:)") >= 0 && node.Parent.Text != cmbEdmTypes.SelectedItem.ToString();
                            property.IsNull = ((node.Text.Extract("(:", ":)").IndexOf("Null", StringComparison.CurrentCultureIgnoreCase) >= 0));
                            property.IsEditable = this.GetIsEditable(node);
                            property.UICaption = node.Name.Right(".").Replace("_", " ").Proper();
                            property.UIDisplayControl = this.GetLinxClass(property);
                            property.Precision = ((int)(this.GetFieldPrecision(property) * 10)).ToString();
                            property.UIGroup = "";
                            property.EdmKey = node.Name;
                            property.DefaultValue = "";
                            property.UIValidationValues = "";
                            this.Entity.EntityAdapterProperties.Add(property);
                        }
                        else
                            this.AddProperties(node.Nodes);
                    }
                }
            }
        }


        private bool GetIsEditable(TreeNode node)
        {
            if (node.Parent != null)
            {
                if (node.Parent.Name.InList(this.Entity.PrimaryEntity, this.Entity.SecondaryEntity))
                    return (!this.ExistsSelectedFK(node));
                else
                {
                    if (node.Text.IndexOf("(:PK:)") >= 0 && node.Parent.Parent != null && node.Parent.Parent.Parent != null && node.Parent.Parent.Parent.Parent != null)
                        return (node.Parent.Parent.Parent.Parent.Name.InList(this.Entity.PrimaryEntity, this.Entity.SecondaryEntity));
                    else
                        return false;
                }
            }
            else
                return false;
        }


        private bool GetIsEditableByEntity(TreeNode node, string entityName)
        {
            if (node.Parent != null)
            {
                if (node.Parent.Name == entityName)
                    return (!this.ExistsSelectedFK(node));
                else
                {
                    if (node.Text.IndexOf("(:PK:)") >= 0 && node.Parent.Parent != null && node.Parent.Parent.Parent != null && node.Parent.Parent.Parent.Parent != null)
                        return (node.Parent.Parent.Parent.Parent.Name == entityName);
                    else
                        return false;
                }
            }
            else
                return false;
        }

        private decimal GetFieldPrecision(EntityAdapterProperty property)
        {
            if (!File.Exists(this.model.Path))
                return 0;

            string[] edmFiles = Directory.GetFiles((new FileInfo(this.model.Path).Directory.FullName + @"\..\..\"), "*.edmx", SearchOption.TopDirectoryOnly).ToArray();

            if (edmFiles.Length == 0)
                return 0;

            if (property.Datatype.ToLower().Contains("char"))
                return 1;

            if (property.Datatype.ToLower().Contains("datetime"))
                return 10;

            if (property.Datatype.ToLower().Contains("guid"))
                return 12;

            if (property.Datatype.ToLower().Contains("bool"))
                return 0;

            if (property.Datatype.ToLower().Contains("byte") || property.Datatype.ToLower().Contains("sbyte"))
                return 3;

            if (property.Datatype.ToLower().Contains("int16") || property.Datatype.ToLower().Contains("uint16"))
                return 6;

            if (property.Datatype.ToLower().Contains("int32") || property.Datatype.ToLower().Contains("uint32"))
                return 12;

            if (property.Datatype.ToLower().Contains("int64") || property.Datatype.ToLower().Contains("uint64"))
                return 24;

            string[] nameParts = property.Name.Split(new char[] { '.' });

            if (nameParts.Length < 2)
                return 0;

            string entityName = nameParts[nameParts.Length - 2], field = nameParts[nameParts.Length - 1];

            string edmFile = edmFiles[0];
            decimal precision = 0;

            using (System.Xml.XmlTextReader reader = new System.Xml.XmlTextReader(edmFile))
            {
                while (reader.Read())
                {
                    if (reader.NodeType.ToString() == "Element" && reader.Name == "EntityType")
                    {
                        while (reader.MoveToNextAttribute())
                        {
                            if (reader.Name == "Name" && reader.Value == entityName)
                            {
                                while (reader.Read())
                                {
                                    if (reader.NodeType.ToString() == "Element" && reader.Name == "Property")
                                    {
                                        while (reader.MoveToNextAttribute())
                                        {
                                            if (reader.Name == "Name" && reader.Value == field)
                                            {
                                                while (reader.MoveToNextAttribute()) // Read attributes
                                                {
                                                    if (reader.Name == "MaxLength")
                                                    {
                                                        try
                                                        {
                                                            precision = decimal.Parse(reader.Value);
                                                        }
                                                        catch
                                                        {
                                                            precision = 999999;
                                                        }
                                                        break;
                                                    }

                                                    if (reader.Name == "Precision")
                                                    {
                                                        try
                                                        {
                                                            precision = decimal.Parse(reader.Value);
                                                            if (reader.MoveToNextAttribute())
                                                                if (reader.Name == "Scale")
                                                                    precision = precision + (decimal.Parse(reader.Value) / (decimal)10);
                                                        }
                                                        catch
                                                        {
                                                            precision = 999999;
                                                        }
                                                        break;
                                                    }
                                                }
                                            }

                                            if (precision > 0)
                                                break;
                                        }
                                    }

                                    if (precision > 0)
                                        break;
                                }
                            }

                            if (precision > 0)
                                break;
                        }

                        if (precision > 0)
                            break;
                    }
                }
                reader.Close();
            }

            return (precision == 999999 ? 0 : precision);
        }

        private UIDisplayControls GetLinxClass(EntityAdapterProperty property)
        {
            if (property.IsFK)
                return UIDisplayControls.LinxLookUpTextBox;

            if (property.Datatype.Contains("Boolean"))
                return UIDisplayControls.LinxCheckBox;

            if (property.Datatype.Contains("DateTime"))
                return UIDisplayControls.LinxDateTimeTextBox;

            if (property.Datatype.ToLower().Contains("byte") ||
                property.Datatype.ToLower().Contains("int16") ||
                property.Datatype.ToLower().Contains("int32") ||
                property.Datatype.ToLower().Contains("int64") ||
                property.Datatype.ToLower().Contains("sbyte") ||
                property.Datatype.ToLower().Contains("uint16") ||
                property.Datatype.ToLower().Contains("uint32") ||
                property.Datatype.ToLower().Contains("uint64") ||
                property.Datatype.ToLower().Contains("single") ||
                property.Datatype.ToLower().Contains("double") ||
                property.Datatype.ToLower().Contains("decimal"))
                return UIDisplayControls.LinxNumericTextBox;

            return UIDisplayControls.LinxTextBox;
        }



        private void LoadSecondaryEntities()
        {
            this.cmbSecondaryTypes.Items.Clear();
            this.cmbSecondaryTypes.Items.Add(" ");

            if (this.treeEdmRelatedTypes.Nodes.Count > 0)
            {
                foreach (TreeNode node in this.treeEdmRelatedTypes.Nodes[0].Nodes)
                {
                    if (node.Tag != null && node.Tag.ToString() == "IsReference")
                    {
                        foreach (TreeNode refNode in node.Nodes)
                        {
                            if (refNode.Nodes.Count > 0)
                            {
                                if (!this.cmbSecondaryTypes.Items.Contains(refNode.Nodes[0].Name))
                                    this.cmbSecondaryTypes.Items.Add(refNode.Nodes[0].Name);
                            }
                        }
                    }
                }
            }

        }

        private bool ExistsSelectedFK(TreeNode targetNode)
        {
            if (targetNode.Parent.Nodes[0].Tag != null && targetNode.Parent.Nodes[0].Tag.ToString() == "IsReference")
            {
                foreach (TreeNode refNode in targetNode.Parent.Nodes[0].Nodes)
                {
                    foreach (TreeNode entityNode in refNode.Nodes)
                    {
                        foreach (TreeNode propNode in entityNode.Nodes)
                        {
                            if (propNode.Tag.ToString() == "IsProperty" && propNode.Checked && propNode.Text == targetNode.Text)
                                return true;
                        }
                    }
                }
            }

            return false;
        }


        private void cmbEdmTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.FillTree(null, "", "");
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
            if (e.Node.Parent != null)
            {
                if (e.Node.Checked)
                {
                    if (!e.Node.Parent.Checked)
                        e.Node.Parent.Checked = true;
                }
                else
                {
                    bool existsCheckedNode = false;
                    foreach (TreeNode node in e.Node.Parent.Nodes)
                    {
                        if (node.Checked)
                        {
                            existsCheckedNode = true;
                            break;
                        }
                    }
                    if (existsCheckedNode != e.Node.Parent.Checked)
                        e.Node.Parent.Checked = existsCheckedNode;
                }
            }
        }


        private void FrmEntityBuilder_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Restore primary and secondary entities
            if (!isOk)
            {
                this.entity.PrimaryEntity = primaryEntity;
                this.entity.SecondaryEntity = secondaryEntity;
            }
        }

        private void cmbSecondaryTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbSecondaryTypes.SelectedItem.ToString() != this.Entity.SecondaryEntity)
            {
                this.CheckUpdatableTreeByEntity(this.treeEdmRelatedTypes.Nodes, this.Entity.SecondaryEntity, false);
                this.Entity.SecondaryEntity = this.cmbSecondaryTypes.SelectedItem.ToString();
                this.CheckUpdatableTreeByEntity(this.treeEdmRelatedTypes.Nodes, this.Entity.SecondaryEntity, true);
                this.CheckUpdatableTreeByEntity(this.treeEdmRelatedTypes.Nodes, this.Entity.PrimaryEntity, true);
            }
        }

    }
}

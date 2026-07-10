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
using Linx.EntityAdapterDesigner.CustomizedCode;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Linx.EntityAdapterDesigner.CustomCode
{

    public partial class FrmEntityBuilder : Form
    {
        //Consts

        #region Variables

        private EntityAdapter baseEntity = null;
        private bool isOk = false;
        private string primaryEntityBase = string.Empty, primaryEntity = string.Empty, secondaryEntities = string.Empty;
        Dictionary<string, string> specializedClasses = new Dictionary<string, string>();
        List<string> lookUps = new List<string>();

        #endregion

        #region Properties

        private EntityAdapter entity;
        public EntityAdapter Entity
        {
            get { return entity; }
            set
            {
                if (value != entity)
                {
                    entity = value;
                    baseEntity = entity.BaseEntityAdapter;
                    //Check level
                    entity.CheckEdmTreeMaximumLevel();
                    //Verify base class
                    entity.UpdateBaseClassInfo();
                    Model = entity.GetCurrentDataModel();
                    this.Text += " (DataContext Max Tree Level = " + entity.EdmTreeMaximumLevel.ToString() + ")";
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

        public bool IsMainEntity
        {
            get
            {
                if (this.entity != null)
                    return (EntityAdapterReferencesEntityDataModel.GetEntityDataModel(this.entity) != null);
                else
                    return false;
            }
        }

        #endregion

        #region Constructor

        public FrmEntityBuilder()
        {
            InitializeComponent();
            this.treeEdmRelatedTypes.AfterCheck += new TreeViewEventHandler(treeEdmRelatedTypes_AfterCheck);
        }

        #endregion

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

        /// <summary>
        /// Fill combobox with dbContext Sets
        /// </summary>
        private void FillTypes()
        {
            WaitCursor(true);
            bool existsPrimaryUpdate = false;

            if (this.model != null && !this.model.Path.IsNullOrEmpty() && this.model.EdmInfo == null)
            {
                model.LoadEdmInformation();
            }

            if (this.model != null && this.entity != null)
            {
                if (this.model.Path.IsNullOrEmpty() || !System.IO.File.Exists(this.model.Path) || this.model.EdmInfo.IsNull())
                    MessageBox.Show("The DataContext file is empty or does not exists. Check the property Path on DataContext element.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    this.txEdmContext.Text = this.model.EdmInfo.Metadata.GetFullName();

                    cmbEdmTypes.Items.Clear();
                    var edmSets = this.model.EdmInfo.GetTypes();

                    foreach (var memberType in edmSets)
                    {
                        if (memberType.BaseTypeName.IsNullOrEmpty())
                        {
                            if (baseEntity == null && this.ckOnlyRelatedEntities.Checked && !this.entity.TargetEntityAdapter.IsNullOrEmpty() && !this.entity.TargetEntityAdapter.DetailRelations.IsNullOrEmpty())
                            {
                                if (("#" + this.entity.TargetEntityAdapter.DetailRelations).IndexOf("#" + memberType.Name + "(") >= 0)
                                    cmbEdmTypes.Items.Add(memberType.Name);
                            }
                            else
                                cmbEdmTypes.Items.Add(memberType.Name);

                            if (this.entity.PrimaryEntity == memberType.Name)
                                existsPrimaryUpdate = true;
                        }

                        //Inherited types
                        foreach (var specializedType in edmSets.Where(e => 
                            memberType.IsBaseTypeOf(e) && !specializedClasses.ContainsKey(e.Name)))
                        {
                            cmbEdmTypes.Items.Add(specializedType.Name);
                            specializedClasses.Add(specializedType.Name, memberType.Name);

                            if (this.entity.PrimaryEntity == specializedType.Name)
                                existsPrimaryUpdate = true;
                        }
                    }
                }
            }

            //Save primary and secondary entities
            primaryEntityBase = this.entity.PrimaryEntityBase;
            primaryEntity = this.entity.PrimaryEntity;
            secondaryEntities = this.entity.SecondaryEntities;

            if (existsPrimaryUpdate)
            {
                cmbEdmTypes.SelectedItem = primaryEntity;
                foreach (string secondaryEntity in secondaryEntities.Split(new char[] { ' ' }))
                {
                    if (this.treeSecondaryTypes.Nodes.ContainsKey(secondaryEntity))
                        this.treeSecondaryTypes.Nodes[secondaryEntity].Checked = true;
                }

                //Remove base class elements
                foreach (string secondaryEntity in this.entity.GetInheritanceSecondaryEntities().Split(new char[] { ' ' }))
                {
                    if (this.treeSecondaryTypes.Nodes.ContainsKey(secondaryEntity))
                        this.treeSecondaryTypes.Nodes.RemoveByKey(secondaryEntity);
                }
                //Remove derived class elements
                foreach (string secondaryEntity in this.entity.GetDerivedSecondaryEntities().Split(new char[] { ' ' }))
                {
                    if (this.treeSecondaryTypes.Nodes.ContainsKey(secondaryEntity))
                        this.treeSecondaryTypes.Nodes.RemoveByKey(secondaryEntity);
                }

                cmbEdmTypes.Enabled = baseEntity == null && entity.DerivedEntityAdapters.Count() == 0;
                ckOnlyRelatedEntities.Visible = cmbEdmTypes.Enabled;
            }
            else //Reset updatable entities
            {
                this.entity.PrimaryEntityBase = "";
                this.entity.PrimaryEntity = "";
                this.entity.SecondaryEntities = "";
            }

            this.CheckTree(this.treeEdmRelatedTypes.Nodes);

            WaitCursor(false);
        }

        private void WaitCursor(bool wait)
        {
            this.Cursor = wait ? Cursors.WaitCursor : Cursors.Default;
        }

        private void CheckTree(TreeNodeCollection nodes)
        {
            List<TreeNode> deletedNodes = new List<TreeNode>();
            EntityAdapterProperty[] properties;
            EntityAdapterPublicationProperty[] pubProperties;
            foreach (TreeNode node in nodes)
            {
                if (node.Tag.ToString() == EdmReader.IsProperty)
                {
                    properties = this.entity.EntityAdapterProperties.Where(e => e.EdmKey == node.Name).ToArray();
                    node.Checked = (properties.Length > 0);
                    if (properties.Length == 0)
                    {
                        //Verify properties on base type
                        properties = this.entity.GetInheritanceProperties().Where(e => e.EdmKey == node.Name).ToArray();
                        if (properties.Length > 0)
                            deletedNodes.Add(node);

                        //Verify properties on derived types
                        if (properties.Length == 0)
                        {
                            properties = this.entity.GetDerivedProperties().Where(e => e.EdmKey == node.Name).ToArray();
                            if (properties.Length > 0)
                                deletedNodes.Add(node);

                            //Verify publication properties on base type
                            if (properties.Length == 0)
                            {
                                pubProperties = this.entity.GetInheritancePublicationProperties().Where(e => e.EdmKey == node.Name).ToArray();
                                if (pubProperties.Length > 0)
                                    deletedNodes.Add(node);
                            }

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

        private void CheckUpdatableTreeByEntity(TreeNodeCollection nodes, string entityName, bool value)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Tag.ToString() == EdmReader.IsProperty)
                {
                    if (this.GetIsEditableByEntity(node, entityName))
                        node.Checked = value;
                }
                else
                    this.CheckUpdatableTreeByEntity(node.Nodes, entityName, value);
            }
        }

        private List<string> GetPKOfEntity(string EntityName)
        {
            List<String> pks = new List<string>();
            var entity = this.model.EdmInfo.Metadata.Entities.FirstOrDefault(e => e.Name == EntityName);

            if (!entity.IsNull())
            {
                foreach (var pKey in entity.Properties.Where(m => m.IsPrimaryKey()))
                    pks.Add(pKey.Name.PrepareName());
            }

            return pks;
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
                    List<String> properties = this.GetPKOfEntity(entityParent.PrimaryEntity);
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
            if (primaryEntity != this.cmbEdmTypes.SelectedItem.ToString())
            {
                this.Entity.Name = this.cmbEdmTypes.SelectedItem.ToString().PrepareName();
                if (this.Entity.Name == this.cmbEdmTypes.SelectedItem.ToString())
                    this.Entity.Name += "View";
            }

            //Save configurations
            List<EntityAdapterProperty> propertiesList = this.Entity.EntityAdapterProperties.Where(e => !e.IsCustomized).OrderBy(e => e.Name).ToList();
            EntityAdapterProperty compare;


            //Update Relation
            this.UpdateParentRelation(this.entity);

            //Update relations
            this.entity.EntityRelations = "";
            this.UpdateEntityRelations(treeEdmRelatedTypes.Nodes);

            //Update detail relations
            this.entity.DetailRelations = model.EdmInfo.DetailReferences;
            this.entity.ReferenceRelations = model.EdmInfo.RelationReferences;


            //Update Entity Sets
            this.entity.UpdateEntitySets();

            //Remove no custom
            for (int idx = this.Entity.EntityAdapterProperties.Count - 1; idx >= 0; idx--)
            {
                if (!this.Entity.EntityAdapterProperties[idx].IsCustomized)
                    this.Entity.EntityAdapterProperties.RemoveAt(idx);
            }
            //Add properties
            lookUps.Clear();
            this.AddProperties(this.treeEdmRelatedTypes.Nodes);

            //Restore custom configurations
            for (int propIndex = 0; propIndex < propertiesList.Count; propIndex++)
            {
                compare = this.Entity.EntityAdapterProperties.Where(e => !e.IsDeleted && !e.IsCustomized && e.EdmKey == propertiesList[propIndex].EdmKey).FirstOrDefault();
                if (compare != null)
                {
                    compare.RestoreUserDefinition(propertiesList[propIndex], model.EdmInfo.IsDbContext);
                }
            }

            //Adjust Order by Name
            propertiesList = this.Entity.EntityAdapterProperties.OrderBy(e => e.Name).ToList();
            for (int propIndex = 0; propIndex < propertiesList.Count; propIndex++)
            {
                this.Entity.EntityAdapterProperties.Move(propertiesList[propIndex], propIndex);
            }

            this.isOk = true;
            this.Entity.ApplyPublication();
            List<LookUpStruct> lookUpStructures = (this.entity.PrimaryEntity.IsNullOrEmpty() ? new List<LookUpStruct>() : LookUpStruct.GetLookUpStructures(this.model.EdmInfo.GetType(this.model.TargetNamespace + "." + this.entity.PrimaryEntity)));
            if (lookUpStructures.Count > 0)
                this.Entity.GenerateEntityLookUps(lookUpStructures);
            else
                this.Entity.GenerateEntityLookUps(specializedClasses);
            this.Entity.UpdateSourceDerivedClasses(specializedClasses);


            this.Close();
        }

        public TreeView GetTreeView()
        {
            return treeEdmRelatedTypes;
        }

        private bool CheckIsNullable(TreeNode node)
        {
            if (node != null)
            {
                if (!node.Text.IsNullOrEmpty() && node.Text.Length >= 16 && node.Text.Left(16) == "0..1 [ZeroOrOne]")
                    return true;
                else
                    return CheckIsNullable(node.Parent);
            }
            else
                return false;
        }

        private void AddProperties(TreeNodeCollection nodes)
        {
            string precision;
            if (nodes != null)
            {
                foreach (TreeNode node in nodes)
                {
                    if (node.Tag.ToString() == EdmReader.IsProperty)
                    {
                        if (node.Checked)
                        {
                            if (this.Entity.EntityAdapterProperties.Where(e => !e.IsDeleted && e.IsCustomized && e.EdmKey == node.Name).Count() == 0)
                            {
                                bool hasNullableJoin = CheckIsNullable(node.Parent);
                                EntityAdapterProperty property = new EntityAdapterProperty(this.Entity.Partition);
                                property.Name = node.Name.Right(".").PrepareName();

                                //Check repetitions
                                int propsCnt = this.Entity.EntityAdapterProperties.Count(e => e.Name == property.Name);
                                if (propsCnt > 0)
                                    property.Name = property.Name + propsCnt.ToString();

                                property.DisplayOrder = node.Index;
                                property.Datatype = node.Text.Extract(" [", "] ");
                                if (hasNullableJoin && !property.Datatype.ToLower().Contains("string") && !property.Datatype.Contains("Nullable<") && !property.Datatype.Contains("?"))
                                    property.Datatype = "System.Nullable<" + property.Datatype + ">";
                                property.IsBrowsable = property.Datatype.IndexOf("System.Guid") < 0;
                                property.ConnectedAttribute = String.Empty;
                                property.EdmKey = node.Name;
                                property.IsPK = node.Text.IndexOf("(:PK:)") >= 0;
                                property.IsFK = node.Text.IndexOf("(:PK:)") >= 0 && property.EdmKey.Occurs(".") >= 2;
                                property.IsNull = (
                                    ((node.Text.Extract("(:", ":)").IndexOf("Null", StringComparison.CurrentCultureIgnoreCase) >= 0)) ||
                                    hasNullableJoin
                                    );
                                property.IsEditable = this.GetIsEditable(node);
                                property.DisplayName = node.Name.Right(".").Replace("_", " ").Proper() + (propsCnt > 0 ? propsCnt.ToString() : "");
                                property.DefaultValue = "";
                                property.IsCustomized = false;

                                this.Entity.EntityAdapterProperties.Add(property);

                                //Adjust precision and format
                                precision = this.model.EdmInfo.GetFieldPrecision(property).ToString();
                                property.Precision = (precision + System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator).Left(System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator) + ":" + (precision.Right(System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator).IsNullOrEmpty() ? "0" : precision.Right(System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator));
                                property.DisplayControl = property.GetDisplayControlClass();
                                property.DataFormatString = this.GetDataFormatString(property);
                                property.BrandDecimalsControl = this.model.EdmInfo.IsBrandDecimalsControl(property);
                                property.DomainName = this.model.EdmInfo.GetDomainName(property.GetEdmEntityName(), node.Name.Right("."));
                            }
                        }
                    }
                    else
                    {
                        //Get look up
                        if (node.Checked && node.Level == 3 && !lookUps.Contains(node.Name.Left("(")))
                            lookUps.Add(node.Name.Left("("));

                        this.AddProperties(node.Nodes);
                    }
                }
            }
        }


        private bool GetIsEditable(TreeNode node)
        {
            if (node.Parent != null)
            {
                if (node.Parent.Name == this.Entity.PrimaryEntity || node.Parent.Name.InList(this.Entity.SecondaryEntities.Split(new char[] { ' ' })))
                    return (!this.ExistsSelectedFK(node));
                else
                {
                    if (node.Text.IndexOf("(:PK:)") >= 0 && node.Parent.Parent != null && node.Parent.Parent.Parent != null && node.Parent.Parent.Parent.Parent != null)
                        return (node.Parent.Parent.Parent.Parent.Name == this.Entity.PrimaryEntity || node.Parent.Parent.Parent.Parent.Name.InList(this.Entity.SecondaryEntities.Split(new char[] { ' ' })));
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


        private string GetDataFormatString(EntityAdapterProperty property)
        {
            if (!property.Datatype.Contains("[]") && property.Datatype.ToLower().Contains("datetime"))
                return "d";

            if (!property.Datatype.Contains("[]") && (property.Datatype.ToLower().Contains("decimal") || property.Datatype.ToLower().Contains("float") || property.Datatype.ToLower().Contains("double")))
                return "N" + (property.Precision.Right(":").IsNullOrEmpty() ? "0" : property.Precision.Right(":"));

            return String.Empty;
        }






        private void LoadSecondaryEntities()
        {
            this.treeSecondaryTypes.Nodes.Clear();

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
                                if (!this.treeSecondaryTypes.Nodes.ContainsKey(refNode.Nodes[0].Name))
                                    this.treeSecondaryTypes.Nodes.Add(refNode.Nodes[0].Name, refNode.Nodes[0].Name);
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
                            if (propNode.Tag.ToString() == EdmReader.IsProperty && propNode.Checked && propNode.Text.Left(" ") == targetNode.Text.Left(" "))
                                return true;
                        }
                    }
                }
            }

            string propertyName = targetNode.Text.Left(" ");
            return (propertyName.Length > 3 && propertyName.Right(3) == "_FK");
        }


        private void cmbEdmTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            WaitCursor(true);

            model.EdmInfo.FillTree(treeEdmRelatedTypes, (string)this.cmbEdmTypes.SelectedItem, () =>
                {
                    this.Entity.PrimaryEntity = this.cmbEdmTypes.SelectedItem.ToString();
                    if (specializedClasses.ContainsKey(this.Entity.PrimaryEntity))
                        this.Entity.PrimaryEntityBase = specializedClasses[this.Entity.PrimaryEntity];
                    else
                        this.Entity.PrimaryEntityBase = "";
                    this.Entity.SecondaryEntities = "";
                    this.CheckUpdatableTreeByEntity(this.treeEdmRelatedTypes.Nodes, this.Entity.PrimaryEntity, true);
                    this.LoadSecondaryEntities();
                }, entity.EdmTreeMaximumLevel);


            WaitCursor(false);
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

        private void FrmEntityBuilder_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Restore primary and secondary entities
            if (!isOk)
            {
                this.entity.PrimaryEntityBase = primaryEntityBase;
                this.entity.PrimaryEntity = primaryEntity;
                this.entity.SecondaryEntities = secondaryEntities;
            }
        }



        private void UpdateEntityRelations(TreeNodeCollection nodes)
        {
            bool executeReentrant;

            if (this.entity != null)
            {
                foreach (TreeNode node in nodes)
                {
                    executeReentrant = true;
                    if (node.Parent != null)
                    {
                        if (node.Tag.ToString() == "IsEntity")
                        {
                            if (("#" + this.entity.EntityRelations + "#").Contains("#" + node.Name + "#"))
                                executeReentrant = false;
                            else
                                this.entity.EntityRelations += (this.entity.EntityRelations.IsNullOrEmpty() ? "" : "#") + node.Name;
                        }
                    }
                    if (executeReentrant)
                        this.UpdateEntityRelations(node.Nodes);
                }
            }
        }


        private Dictionary<string, string> bkpExtendedFilters = new Dictionary<string, string>();
        private void RemoveExtendedFilters()
        {
            bkpExtendedFilters.Clear();

            //Save Display Names
            foreach (var item in this.Entity.EntityAdapterExtendedFilters)
            {
                bkpExtendedFilters.Add(item.Name, item.DisplayName);
                foreach (var prop in item.EntityAdapterPropertyExtendedFilters.Where(e => !e.IsEnabled))
                    bkpExtendedFilters.Add(item.Name + "." + prop.Name, prop.DisplayName + "#" + prop.IsEnabled.ToString().ToLower());
            }

            this.Entity.EntityAdapterExtendedFilters.Clear();
        }


        public void GetOutLinqExtendedFilters(TreeNodeCollection nodes, List<EntityAdapterExtendedFilter2> outLinqEntities, List<string> keysControl = null)
        {
            bool executeReentrant;
            string entityName, relationName, propName;
            List<EntityAdapterPropertyExtendedFilter2> propList = new List<EntityAdapterPropertyExtendedFilter2>();
            EntityAdapterExtendedFilter2 eFilter;
            bool isUsedInTheLinq;

            if (keysControl == null)
                keysControl = new List<string>();


            if (this.entity != null)
            {
                foreach (TreeNode node in nodes)
                {
                    executeReentrant = true;

                    if (node.Tag.ToString() == "IsEntity")
                    {
                        if (keysControl.Contains(node.Name))
                            executeReentrant = false;
                        else
                        {
                            keysControl.Add(node.Name);

                            entityName = node.Name.Extract("(", ")");
                            relationName = node.Name.Left("(");
                            if (entityName.IsNullOrEmpty())
                                entityName = node.Name;

                            isUsedInTheLinq = (this.Entity.EntityAdapterProperties.Where(e => ("." + e.EdmKey).Contains("." + (relationName.IsNullOrEmpty() ? entityName : relationName) + ".")).Count() > 0 || this.Entity.EntityAdapterPublicationProperties.Where(e => ("." + e.EdmKey).Contains("." + (relationName.IsNullOrEmpty() ? entityName : relationName) + ".")).Count() > 0);
                            if ((this.Entity.EntityAdapterExtendedFilters.Where(e => e.Name == ((relationName.IsNullOrEmpty() ? "" : relationName + "_") + entityName)).Count() == 0))
                            {
                                eFilter = new EntityAdapterExtendedFilter2() { Name = (relationName.IsNullOrEmpty() ? "" : relationName + "_") + entityName, EntityName = entityName, RelationName = relationName, DisplayName = (relationName.IsNullOrEmpty() ? entityName : relationName).PrepareName(), IsUsedInTheLinq = isUsedInTheLinq };
                                outLinqEntities.Add(eFilter);

                                //Prepare do create ordered list
                                propList.Clear();

                                foreach (TreeNode propNode in node.Nodes)
                                {
                                    if (propNode.Tag.ToString() == EdmReader.IsProperty)
                                    {
                                        propName = propNode.Name.Right(".");
                                        EntityAdapterPropertyExtendedFilter2 efProp = new EntityAdapterPropertyExtendedFilter2() { Name = propName, DataType = propNode.Text.Extract(" [", "] "), DisplayName = (relationName.IsNullOrEmpty() ? entityName : relationName).PrepareName() + "." + propName.PrepareName(), EdmKey = propNode.Name, IsEnabled = true };
                                        propList.Add(efProp);
                                    }
                                }

                                //Add ordered list
                                eFilter.EntityAdapterPropertyExtendedFilters.AddRange(propList.OrderBy(e => e.DisplayName));
                            }
                        }

                    }

                    if (executeReentrant)
                        this.GetOutLinqExtendedFilters(node.Nodes, outLinqEntities, keysControl);
                }
            }
        }


        private void ckOnlyRelatedEntities_CheckedChanged(object sender, EventArgs e)
        {
            this.FillTypes();
        }

        private void treeSecondaryTypes_AfterCheck(object sender, TreeViewEventArgs e)
        {
            string secondaryEntities = String.Empty;
            foreach (TreeNode node in this.treeSecondaryTypes.Nodes)
            {
                if (node.Checked)
                    secondaryEntities += (secondaryEntities.IsNullOrEmpty() ? "" : " ") + node.Text;
            }

            if (secondaryEntities != this.Entity.SecondaryEntities)
            {
                this.Entity.SecondaryEntities = secondaryEntities;
                this.CheckUpdatableTreeByEntity(this.treeEdmRelatedTypes.Nodes, e.Node.Text, false);
                if (e.Node.Checked)
                    this.CheckUpdatableTreeByEntity(this.treeEdmRelatedTypes.Nodes, e.Node.Text, true);
                this.CheckUpdatableTreeByEntity(this.treeEdmRelatedTypes.Nodes, this.Entity.PrimaryEntity, true);
            }
        }

    }

}

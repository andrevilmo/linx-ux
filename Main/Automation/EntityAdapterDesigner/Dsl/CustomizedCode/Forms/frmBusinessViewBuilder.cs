using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Linx.EntityAdapterDesigner;
using Linx.Tools;
using EnvDTE;
using Microsoft.VisualStudio.Modeling;
using System.Runtime.Serialization;
using Linx.EntityAdapterDesigner.CustomizedCode.Util;
using Linx.EntityAdapterDesigner.CustomizedCode;
using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Linx.EntityAdapterDesigner.CustomCode
{
    public partial class frmBusinessViewBuilder : Form
    {
        bool started = false;
        List<PublicationEntity> classes = new List<PublicationEntity>();
        public string ElementSelection { get; set; }
        public ProjectItem ItemSelection { get; set; }

        private EntityAdapter _entity;
        public EntityAdapter Entity
        {
            get
            {
                return _entity;
            }
            set
            {
                if (value != null)
                {
                    _publisherReferences = null;
                    _entity = value;
                    _model = _entity.EntityAdapterDesignerRoot;
                    _curDM = _entity.GetCurrentDataModel();

                    if (_curDM == null)
                    {
                        _publisherReferences = new List<PublicationStructure>();
                        _model.VerifyPublisherAutoReference();
                        if (_model.PublisherAutoReference != null)
                        {
                            _publisherReferences.Add(_model.PublisherAutoReference);
                        }
                        else
                        {
                            MessageBox.Show("This business project needs of a build execution.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        foreach (var pubCtx in _model.Subscriptions)
                        {
                            _publisherReferences.Add(pubCtx.Publisher);
                        }
                    }
                }
            }
        }

        private EntityAdapterDesignerRoot _model;
        private List<CustomizedCode.PublicationStructure> _publisherReferences;
        private EntityDataModel _curDM;


        public void PopulateTypes()
        {
            int currentIndex = 0;
            progressModels.Minimum = 0;
            progressModels.Maximum = 0;
            if (_curDM != null)
            {
                var edmSets = _curDM.EdmInfo.GetTypes();
                progressModels.Maximum = edmSets.Count();
                foreach (var memberType in edmSets)
                {
                    currentIndex++;
                    progressModels.Value = currentIndex;
                    Application.DoEvents();
                    try
                    {
                        PublicationEntity bmEntity = new PublicationEntity()
                        {
                            Name = memberType.Name,
                            EdmEntityName = memberType.Name,
                            Namespace = _curDM.EdmInfo.Metadata.Namespace,
                            CompositionHierarchy = "",
                            DisplayName = memberType.Name.Replace("_", " ").Proper(),
                            EdmName = _curDM.EdmInfo.Metadata.Name,
                            EntitiesDescription = ""
                        };

                        foreach (var propInfo in memberType.Properties.Where(p => p.Decorators.Any(d => d.Contains("Column"))))
                        {
                            var displayAttr = propInfo.GetCustomAttribute("Display");
                            var funcPoint = propInfo.GetCustomAttribute("FunctionalPoint");
                            var pubField = propInfo.GetCustomAttribute("LinxPublicationField");
                            var fKey = propInfo.GetCustomAttribute("ForeignKey");
                            var column = propInfo.GetCustomAttribute("Column");
                            var precision = propInfo.GetCustomAttribute("Precision");
                            var scale = propInfo.GetCustomAttribute("Scale");
                            var maxlength = propInfo.GetCustomAttribute("MaxLength");
                            var required = propInfo.GetCustomAttribute("Required");
                            var brandDecimalsControl = propInfo.GetCustomAttribute("BrandDecimals");

                            PublicationProperty pubProp = new PublicationProperty()
                            {
                                Name = propInfo.Name,
                                DisplayName = (!displayAttr.IsNullOrEmpty() ? displayAttr : propInfo.Name.Replace("_", " ").Proper()),
                                IsPrimaryKey = (propInfo.IsPrimaryKey()),
                                DataType = (propInfo.IsNullable ? "System.Nullable<" + propInfo.DataType + ">" : propInfo.DataType),
                                IsNull = (required == null),
                                DomainName = (funcPoint == null ? "" : funcPoint.Extract("DomainName[", "]")),
                                DisplayOrder = (column == null ? 0 : int.Parse(column.Right("Order=").Trim())),
                                Precision = (!precision.IsNullOrEmpty() && !scale.IsNullOrEmpty() ? precision + ":" + scale : (maxlength != null ? maxlength.Length.ToString() + ":0" : "0:0")),
                                BrandDecimalsControl = brandDecimalsControl!= null
                            };

                            bmEntity.Properties.Add(pubProp);
                        }

                        classes.Add(bmEntity);
                    }
                    catch { }
                }
            }
            else if (_publisherReferences != null)
            {
                progressModels.Maximum = 0;
                foreach (var pub in _publisherReferences)
                {
                    progressModels.Maximum += pub.Entities.Count;
                }
                foreach (var pub in _publisherReferences)
                {
                    foreach (var item in pub.Entities)
                    {
                        currentIndex++;
                        progressModels.Value = currentIndex;
                        Application.DoEvents();
                        try
                        {
                            //Adding classes
                            classes.Add(item);
                        }
                        catch { }
                    }
                }
            }

        }

        public void FillList()
        {
            this.listClasses.Nodes.Clear();

            //Filtering classes
            string filter = this.txtSearch.Text.ToLower();
            var query = classes.Select(e => e.Name).Distinct();

            if (!filter.IsNullOrEmpty())
            {
                query = query.Where(e => e.ToLower().Contains(filter));
            }

            //Adding classes
            foreach (var className in query.OrderBy(e => e).ToArray())
            {
                var node = this.listClasses.Nodes.Add(className, className, 0, 0);
                node.Nodes.Add("...", "...", 0, 0);
                node.Collapse();
            }

            if (this.listClasses.Nodes.Count > 0)
                this.listClasses.SelectedNode = this.listClasses.Nodes[0];

            this.listClasses.Invalidate();
            this.txtSearch.Focus();
        }

        #region Constructor

        public frmBusinessViewBuilder()
        {
            InitializeComponent();
        }

        #endregion

        #region Events


        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            this.FillList();
        }

        private void frmBusinessViewBuilder_Activated(object sender, EventArgs e)
        {
            if (!started && _model != null)
            {
                AdjustDataGridsVisibility();
                this.txtSearch.Enabled = false;
                started = true;
                PopulateTypes();
                FillList();
                this.txtSearch.Enabled = true;
                this.RestoreLayout();
            }
        }

        private void RestoreLayout()
        {
            var topEntityQueries = _entity.GetBusinessViewRootObjects();
            if (topEntityQueries != null && topEntityQueries.Count > 0)
            {
                List<TreeNode> deletedEntities = new List<TreeNode>();
                Action<EntityQueryNode, TreeNodeCollection, EntityQueryNode, bool> restoreLayout = null;
                restoreLayout = (eq, nodes, parent, syncPropertiesData) =>
                    {
                        eq.Parent = parent;
                        TreeNode node = null;
                        switch (eq.RelationType)
                        {
                            case QueryNodeType.Entity:

                                if (parent == null && this.treeViewQuery.Nodes.Count > 0)
                                    this.treeViewQuery.Nodes.Add("Union", "Union", 4, 4);

                                node = nodes.Add(eq.Name, eq.Name, 0, 0);
                                //Sync properties with source class
                                SyncSourceProperties(eq);
                                //Adjust selected properties
                                eq.SyncPropertiesWithView(_entity, topEntityQueries.Contains(eq) && syncPropertiesData, topEntityQueries.Contains(eq), syncPropertiesData);
                                //Generate deleted entities
                                if (!classes.Any(e => e.Name == eq.Name))
                                    deletedEntities.Add(node);
                                break;
                            case QueryNodeType.InnerJoin:
                                node = nodes.Add(eq.Name, eq.Name, 1, 1);
                                break;
                            case QueryNodeType.LeftJoin:
                                node = nodes.Add(eq.Name, eq.Name, 2, 2);
                                break;
                            default:
                                break;
                        }
                        if (node != null)
                        {
                            node.Tag = eq;
                            eq.Joins.ForEach(e => restoreLayout(e, node.Nodes, eq, syncPropertiesData));
                            node.Expand();
                        }
                    };

                for (var idx = 0; idx < topEntityQueries.Count(); idx++)
                {
                    restoreLayout(topEntityQueries[idx], this.treeViewQuery.Nodes, null, idx == 0);
                }


                //Adjust deleted entities
                if (deletedEntities.Count > 0)
                {
                    for (int idx = deletedEntities.Count - 1; idx >= 0; idx--)
                    {
                        RemoveNode(deletedEntities[idx]);
                    }
                }

                if (this.treeViewQuery.Nodes.Count > 0)
                {
                    this.treeViewQuery.SelectedNode = this.treeViewQuery.Nodes[0];
                    this.treeViewQuery.Select();
                }
            }
        }

        private void ckCheckedChanged(object sender, EventArgs e)
        {
            this.FillList();
        }

        #endregion

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btApply_Click(object sender, EventArgs e)
        {
            this.SaveRootObject();
            this.Close();
        }

        private void SaveRootObject()
        {
            List<EntityQueryNode> entityObjects = new List<EntityQueryNode>();
            foreach (TreeNode node in this.treeViewQuery.Nodes)
            {
                if (node.Tag is EntityQueryNode)
                    entityObjects.Add(node.Tag as EntityQueryNode);
            }

            using (Transaction t = _entity.Store.TransactionManager.BeginTransaction("Save ModelViewDefinition"))
            {
                _entity.ModelViewDefinition = (entityObjects.Count == 0 ? "" : SerializationManager<List<EntityQueryNode>>.ObjectToJson(entityObjects));
                _entity.GenerateBusinessViewAttributes(entityObjects[0]);

                //Adjust secondary 
                _entity.SecondaryEntities = "";
                if (_entity.GetCurrentDataModel() != null && !_entity.ModelViewDbSets.IsNullOrEmpty())
                {
                    var dbSets = _entity.ModelViewDbSets.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (dbSets.Length > 0)
                    {
                        for (int idx = 1; idx < dbSets.Length; idx++)
                        {
                            var entityName = dbSets[idx];
                            if (!entityName.IsNullOrEmpty())
                                _entity.SecondaryEntities += (_entity.SecondaryEntities.IsNullOrEmpty() ? "" : " ") + entityName + "(" + entityName + ")";
                        }
                    }
                }

                //Adjust Reference Relations
                _entity.ReferenceRelations = "";
                if (_entity.GetCurrentDataModel() != null && !_entity.ModelViewDbSets.IsNullOrEmpty())
                {
                    foreach (var entityName in _entity.EntityAdapterProperties.Select(e => e.GetModelViewSource().Left(".")).Distinct().Where(e => e != _entity.PrimaryEntity))
                    {
                        _entity.ReferenceRelations += (_entity.ReferenceRelations.IsNullOrEmpty() ? "" : "#") + entityName + "(" + entityName + ")";
                    }
                }
                _entity.EntityRelations = _entity.ReferenceRelations;

                //Update Entity Sets
                _entity.UpdateEntitySets();

                //Generate ModelView Lookups with a BM source
                if (_entity.EnableAutomaticLookUps)
                {
                    if (_entity.GetCurrentDataModel() != null)
                    {
                        List<LookUpStruct> lookUpStructures = LookUpStruct.GetModelViewLookUpStructures(_entity, classes);
                        if (lookUpStructures.Count > 0)
                            _entity.GenerateEntityLookUps(lookUpStructures);
                    }
                    else _entity.LookUpAdapters.Clear();
                }

                t.Commit();
            }
        }

        private string GetSelectedEntity()
        {
            if (this.listClasses.SelectedNode == null)
                return "";

            return this.listClasses.SelectedNode.Name;
        }

        private Dictionary<string, string> GetSelectedPropertyRelation(string entityParentName)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            if (this.listClasses.SelectedNode != null && this.listClasses.SelectedNode.Parent != null && this.listClasses.SelectedNode.Parent.Name == entityParentName && !this.listClasses.SelectedNode.Tag.IsNullOrEmpty())
            {
                foreach (var relation in this.listClasses.SelectedNode.Tag.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    result[relation.Right("=")] = relation.Left("=");
                }
            }

            return result;
        }

        private QueryNodeType GetSelectedTypeRelation(string entityParentName)
        {
            if (this.listClasses.SelectedNode != null && this.listClasses.SelectedNode.Parent != null && this.listClasses.SelectedNode.Parent.Name == entityParentName)
            {
                if (this.listClasses.SelectedNode.Text.Contains("0.."))
                    return QueryNodeType.LeftJoin;
            }

            return QueryNodeType.InnerJoin;
        }

        private void AddJoin(string entityName = "")
        {
            if (String.IsNullOrWhiteSpace(entityName))
                entityName = GetSelectedEntity();
            if (String.IsNullOrWhiteSpace(entityName))
            {
                return;
            }

            if (this.treeViewQuery.Nodes.Count == 0 || this.treeViewQuery.SelectedNode == null)
            {
                //Add Union Node
                if (this.treeViewQuery.Nodes.Count > 0)
                {
                    this.treeViewQuery.Nodes.Add("Union", "Union", 4, 4);
                }

                var topEntity = new EntityQueryNode() { Name = entityName, Alias = "", RelationType = QueryNodeType.Entity };
                SyncSourceProperties(topEntity);
                var node = this.treeViewQuery.Nodes.Add(entityName, entityName, 0, 0);
                node.Tag = topEntity;
                this.treeViewQuery.SelectedNode = node;
                this.treeViewQuery.Select();
                return;
            }

            if (this.treeViewQuery.SelectedNode != null)
            {
                if (this.treeViewQuery.SelectedNode.Tag is EntityQueryNode && ((EntityQueryNode)this.treeViewQuery.SelectedNode.Tag).RelationType == QueryNodeType.Entity)
                {
                    var entity = ((EntityQueryNode)this.treeViewQuery.SelectedNode.Tag);
                    var join = new EntityQueryNode() { Name = QueryNodeType.InnerJoin.ToString(), Alias = "", RelationType = QueryNodeType.InnerJoin };
                    join.Parent = entity;
                    entity.Joins.Add(join);
                    var joinNode = this.treeViewQuery.SelectedNode.Nodes.Add(join.Name, join.Name, 1, 1);
                    joinNode.Tag = join;

                    var joinEntity = new EntityQueryNode() { Name = entityName, Alias = "", RelationType = QueryNodeType.Entity };
                    SyncSourceProperties(joinEntity);
                    joinEntity.Parent = join;
                    join.Joins.Add(joinEntity);
                    var entityNode = joinNode.Nodes.Add(joinEntity.Name, joinEntity.Name, 0, 0);
                    entityNode.Tag = joinEntity;

                    SuggestJoinProperties(join);

                    if (join.RelationType == QueryNodeType.LeftJoin)
                    {
                        joinNode.ImageIndex = 2;
                        joinNode.SelectedImageIndex = 2;
                        joinNode.Name = QueryNodeType.LeftJoin.ToString();
                        joinNode.Text = joinNode.Name;
                        join.Name = joinNode.Name;
                    }

                    //Expand and select
                    joinNode.Expand();
                    this.treeViewQuery.SelectedNode = joinNode;
                    this.treeViewQuery.Select();

                    return;
                }
            }
        }

        private void SuggestJoinProperties(EntityQueryNode join)
        {
            if (join.RelationType != QueryNodeType.Entity && join.Parent != null && join.Joins.Count > 0)
            {
                string leftClassName = join.Parent.Name, rightClassName = join.Joins[0].Name;

                //Verify if exists pre-related properties
                var relations = GetSelectedPropertyRelation(leftClassName);
                if (relations.Count > 0)
                {
                    foreach (var relation in relations)
                    {
                        if (!join.Relations.Any(e => e.TargetExpression == "this." + relation.Key))
                            join.Relations.Add(new EntityQueryRelation() { SourceExpression = "this." + relation.Value, TargetExpression = "this." + relation.Key });
                    }
                    join.RelationType = GetSelectedTypeRelation(leftClassName);
                }
                else
                {
                    //Suggest basing in the same name and type
                    var leftClass = classes.FirstOrDefault(e => e.Name == leftClassName);
                    var rightClass = classes.FirstOrDefault(e => e.Name == rightClassName);

                    foreach (var lAttr in leftClass.Properties)
                    {
                        var rAttr = rightClass.Properties.FirstOrDefault(p => p.Name == lAttr.Name && p.DataType == lAttr.DataType);
                        if (rAttr != null)
                        {
                            if (!join.Relations.Any(e => e.TargetExpression == "this." + rAttr.Name))
                                join.Relations.Add(new EntityQueryRelation() { SourceExpression = "this." + lAttr.Name, TargetExpression = "this." + rAttr.Name });
                        }
                    }
                }
            }
        }

        private void PopulateJoinPropertiesSelector(EntityQueryNode entity)
        {
            if (entity.RelationType != QueryNodeType.Entity && entity.Parent != null && entity.Joins.Count > 0)
            {
                var sourceList = entity.Parent.Properties.Select(e => "this." + e.SourceName).OrderBy(e => e).ToList();
                sourceList.AddRange(entity.Relations.Where(e => !sourceList.Contains(e.SourceExpression)).Select(e => e.SourceExpression));
                this.dataGridSourceExpColumn.DataSource = sourceList;

                var targetList = entity.Joins[0].Properties.Select(e => "this." + e.SourceName).OrderBy(e => e).ToList();
                targetList.AddRange(entity.Relations.Where(e => !targetList.Contains(e.TargetExpression)).Select(e => e.TargetExpression));
                this.dataGridTargetExpColumn.DataSource = targetList;
            }
        }

        private void SyncSourceProperties(EntityQueryNode entity)
        {
            var entitySource = classes.Where(e => e.Name == entity.Name).FirstOrDefault();
            if (entitySource != null)
            {
                if (_entity.GetCurrentDataModel() == null && (entitySource.Namespace != _model.GetServiceNameSpace()))
                {
                    entity.ContextType = entitySource.Namespace + "." + entitySource.Namespace.Right(".") + "DomainService";
                    entity.ContextAlias = entitySource.Namespace.Right(".").ToCamelCase() + "Context";
                    entity.BusinessModelType = entitySource.EdmName;
                }
                else
                {
                    entity.ContextType = "";
                    entity.ContextAlias = "context";
                    entity.BusinessModelType = entitySource.EdmName;
                }
                //Adjust existent properties
                foreach (var prop in entity.Properties.ToArray())
                {
                    if (prop.Formula.IsNullOrEmpty())
                    {
                        var sourceProp = entitySource.Properties.FirstOrDefault(s => s.Name == prop.SourceName);
                        if (sourceProp == null)
                        {
                            entity.Properties.Remove(prop);
                        }
                        else
                        {
                            prop.Nullable = sourceProp.IsNull;
                            prop.PrimaryKey = sourceProp.IsPrimaryKey;
                            prop.Type = sourceProp.DataType;
                            prop.DomainName = sourceProp.DomainName;
                            if (sourceProp.Precision.Contains(":"))
                            {
                                prop.Precision = short.Parse(sourceProp.Precision.Left(":"));
                                prop.Scale = short.Parse(sourceProp.Precision.Right(":"));
                                prop.MaxLength = int.Parse(sourceProp.Precision.Left(":"));
                            }
                        }
                    }
                    else
                    {
                        if (!this._entity.EntityAdapterProperties.Any(s => s.Name == prop.Name))
                        {
                            entity.Properties.Remove(prop);
                        }
                    }
                }

                //Add non existent properties
                entity.Properties.AddRange(entitySource.Properties.Where(s => !entity.Properties.Any(p => p.SourceName == s.Name)).Select(e => new EntityQueryProperty()
                {
                    SourceName = e.Name,
                    Name = e.Name.Replace("_", " ").Proper().Replace(" ", ""),
                    Nullable = e.IsNull,
                    PrimaryKey = e.IsPrimaryKey,
                    Selected = false,
                    Type = e.DataType,
                    DisplayName = e.DisplayName,
                    DomainName = e.DomainName,
                    Formula = "",
                    Precision = e.Precision.Contains(":") ? short.Parse(e.Precision.Left(":")) : (short)0,
                    Scale = e.Precision.Contains(":") ? short.Parse(e.Precision.Right(":")) : (short)0,
                    MaxLength = e.Precision.Contains(":") ? int.Parse(e.Precision.Left(":")) : (short)0,
                    LookupInfo = e.LookUpInfo,
                    DisplayControl = e.DisplayControl
                }));
            }
        }

        private void RemoveNode(TreeNode node)
        {
            if (node.Parent != null && node.Parent.Tag is EntityQueryNode)
            {
                ((EntityQueryNode)node.Parent.Parent.Tag).Joins.Remove(((EntityQueryNode)node.Parent.Tag));
                node.Parent.Remove();
            }
            else
            {
                node.Remove();
                if (this.treeViewQuery.Nodes.Count > 0 && this.treeViewQuery.Nodes[this.treeViewQuery.Nodes.Count - 1].Name == "Union")
                {
                    this.treeViewQuery.Nodes[this.treeViewQuery.Nodes.Count - 1].Remove();
                }
            }
        }

        private void tlbbtnRemoveEntity_Click(object sender, EventArgs e)
        {
            if (this.treeViewQuery.SelectedNode != null)
            {
                if (this.treeViewQuery.SelectedNode.Tag is EntityQueryNode && ((EntityQueryNode)this.treeViewQuery.SelectedNode.Tag).RelationType == QueryNodeType.Entity)
                {
                    if (MessageBox.Show("Do you really want to remove the selected entity?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        RemoveNode(this.treeViewQuery.SelectedNode);
                    }

                    AdjustDataGridsVisibility();
                    return;
                }
            }

            MessageBox.Show("You should select an entity node before executing this operation.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void treeViewQuery_AfterSelect(object sender, TreeViewEventArgs e)
        {
            listClasses_AfterSelect(sender, e);
            AdjustDataGridsVisibility();
        }

        private void AdjustDataGridsVisibility()
        {
            this.propertiesDataGridView.Visible = (this.treeViewQuery.SelectedNode != null) && (this.treeViewQuery.SelectedNode.Tag is EntityQueryNode && ((EntityQueryNode)this.treeViewQuery.SelectedNode.Tag).RelationType == QueryNodeType.Entity);
            this.panelEntityDetails.Visible = this.propertiesDataGridView.Visible;
            this.relationsDataGridView.Visible = (this.treeViewQuery.SelectedNode != null) && (this.treeViewQuery.SelectedNode.Tag is EntityQueryNode && ((EntityQueryNode)this.treeViewQuery.SelectedNode.Tag).RelationType != QueryNodeType.Entity);
            this.panelTopRelatons.Visible = this.relationsDataGridView.Visible;

            if (this.propertiesDataGridView.Visible || this.relationsDataGridView.Visible)
            {
                this.entityQueryNodeBindingSource.DataSource = ((EntityQueryNode)this.treeViewQuery.SelectedNode.Tag);
            }

            if (this.propertiesDataGridView.Visible)
            {
                this.propertiesBindingSource.DataSource = ((EntityQueryNode)this.treeViewQuery.SelectedNode.Tag).Properties;
            }

            if (this.relationsDataGridView.Visible)
            {
                this.relationsBindingSource.DataSource = new List<EntityQueryRelation>();
                this.PopulateJoinPropertiesSelector(this.treeViewQuery.SelectedNode.Tag as EntityQueryNode);
                this.relationsBindingSource.DataSource = ((EntityQueryNode)this.treeViewQuery.SelectedNode.Tag).Relations;
            }
        }

        private void listClasses_MouseDown(object sender, MouseEventArgs e)
        {
            var hitTestNode = this.listClasses.HitTest(e.X, e.Y);
            if (hitTestNode != null)
            {
                listClasses.SelectedNode = hitTestNode.Node;
                this.listClasses.DoDragDrop(hitTestNode.Node, DragDropEffects.Move);
            }
        }

        private void listClasses_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void treeViewQuery_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void treeViewQuery_DragDrop(object sender, DragEventArgs e)
        {
            if (this.treeViewQuery.Nodes.Count > 0)
            {
                TreeNode nodeToDropIn = this.treeViewQuery.GetNodeAt(this.treeViewQuery.PointToClient(new Point(e.X, e.Y)));
                this.treeViewQuery.SelectedNode = nodeToDropIn;
            }

            TreeNode data = e.Data.GetData(typeof(TreeNode)) as TreeNode;
            if (data != null)
            {
                this.AddJoin(data.Name);
            }
        }

        private void relationTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.panelTopRelatons.Visible && (this.treeViewQuery.SelectedNode != null) && (this.treeViewQuery.SelectedNode.Tag is EntityQueryNode && ((EntityQueryNode)this.treeViewQuery.SelectedNode.Tag).RelationType != QueryNodeType.Entity))
            {
                string text = relationTypeComboBox.SelectedItem.ToString();
                if (this.treeViewQuery.SelectedNode.Text != text)
                {
                    this.treeViewQuery.SelectedNode.Text = text;
                    this.treeViewQuery.SelectedNode.Name = text;
                    ((EntityQueryNode)this.treeViewQuery.SelectedNode.Tag).Name = text;
                    this.treeViewQuery.SelectedNode.ImageIndex = (text == "InnerJoin" ? 1 : 2);
                    this.treeViewQuery.SelectedNode.SelectedImageIndex = this.treeViewQuery.SelectedNode.ImageIndex;
                    this.treeViewQuery.Invalidate();
                }
            }
        }

        private void relationsDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            ComboBox cb = e.Control as ComboBox;
            if (cb != null && cb.DropDownStyle != ComboBoxStyle.DropDown)
            {
                cb.DropDownStyle = ComboBoxStyle.DropDown;
            }
        }

        private void relationsDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == dataGridSourceExpColumn.DisplayIndex)
            {
                string value = e.FormattedValue.ToString();
                var sourceList = this.dataGridSourceExpColumn.DataSource as List<string>;
                if (!string.IsNullOrWhiteSpace(value) && !sourceList.Contains(value))
                {
                    sourceList.Add(value);
                    this.dataGridSourceExpColumn.DataSource = sourceList.ToList();
                }
            }
            else if (e.ColumnIndex == dataGridTargetExpColumn.DisplayIndex)
            {
                string value = e.FormattedValue.ToString();
                var targetList = this.dataGridTargetExpColumn.DataSource as List<string>;
                if (!string.IsNullOrWhiteSpace(value) && !targetList.Contains(value))
                {
                    targetList.Add(value);
                    this.dataGridTargetExpColumn.DataSource = targetList.ToList();
                }
            }
        }

        private void listClasses_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            //Load Relations
            if (e.Node.Nodes.Count > 0 && e.Node.Nodes[0].Text == "...")
            {
                e.Node.Nodes.Clear();
                this.LoadClassRelations(e.Node);
            }
        }

        private void LoadClassRelations(TreeNode node)
        {
            if (_curDM == null)
                return;

            string relatedName = node.Name;
            List<string> links = new List<string>();

            foreach (var relatedClass in classes.Where(e => e.Name == relatedName))
            {
                foreach (var link in relatedClass.GetAllLinkDefinitions(_curDM.EdmInfo.Metadata))
                {
                    if (!links.Contains(link))
                        links.Add(link);
                }
            }

            foreach (var link in links)
            {
                var innerNode = node.Nodes.Add(link.Extract("] ", " ("), link.Left("#"), 0, 0);
                innerNode.Tag = link.Right("#");
                innerNode.Nodes.Add("...", "...", 0, 0);
                innerNode.Collapse();
            }
        }

        private void listClasses_AfterSelect(object sender, TreeViewEventArgs e)
        {
            listClasses.Capture = false;
            treeViewQuery.Capture = false;
        }

        private void frmBusinessViewBuilder_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.btApply.Enabled = false;
            this.btCancel.Enabled = false;
            this.treeViewQuery.Enabled = false;
            this.listClasses.Enabled = false;
            this.txtSearch.Enabled = false;
        }

    }

}

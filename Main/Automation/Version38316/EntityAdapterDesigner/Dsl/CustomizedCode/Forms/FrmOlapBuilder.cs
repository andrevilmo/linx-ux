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
using Microsoft.AnalysisServices.AdomdClient;


namespace Linx.EntityAdapterDesigner.CustomCode
{

    public partial class FrmOlapBuilder : Form
    {
        public const int MeasureImageNumber = 1;
        public const int FolderImageNumber = 3;

        string SelectedCubeName;

        private EntityAdapter _entity, baseEntity = null;
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
                        baseEntity = _entity.BaseEntityAdapter;
                        this._catalog = _entity.GetOlapCatalog();
                        if (this._catalog != null)
                        {
                            this.txOlapContext.Text = _catalog.Connection.GetConnectionString();
                            this.FillCubes();
                        }
                    }
                }
            }
        }

        private OlapCatalog _catalog;

        private void FillCubes()
        {
            var cubes = new List<string>();

            using (AdomdConnection connection = new AdomdConnection(_catalog.Connection.GetConnectionString()))
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT CUBE_NAME FROM $system.MDSCHEMA_CUBES where CUBE_SOURCE = 1";
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    cubes = reader.Select(r => r["CUBE_NAME"] as string).ToList();
                }
            }

            cmbTypes.DataSource = cubes;
            string cubeName = this._entity.GetCubeName();
            if (cubeName.IsNullOrEmpty()) return;

            this.cmbTypes.SelectedItem = cubeName;
            this.cmbTypes.Enabled = baseEntity == null && _entity.DerivedEntityAdapters.Count() == 0;
        }

        private void CheckTree(TreeNodeCollection nodes)
        {
            List<TreeNode> deletedNodes = new List<TreeNode>();
            EntityAdapterProperty[] properties;
            EntityAdapterPublicationProperty[] pubProperties;
            foreach (TreeNode node in nodes)
            {
                if (node.Tag is OlapItemInfo && !node.Name.IsNullOrEmpty())
                {
                    properties = this._entity.EntityAdapterProperties.Where(e => e.DataRelationKey == node.Name).ToArray();
                    node.Checked = (properties.Length > 0);
                    if (properties.Length == 0)
                    {
                        //Verify properties on base type
                        properties = this._entity.GetInheritanceProperties().Where(e => e.DataRelationKey == node.Name).ToArray();
                        if (properties.Length > 0)
                            deletedNodes.Add(node);

                        //Verify properties on derived types
                        if (properties.Length == 0)
                        {
                            properties = this._entity.GetDerivedProperties().Where(e => e.DataRelationKey == node.Name).ToArray();
                            if (properties.Length > 0)
                                deletedNodes.Add(node);

                            //Verify publication properties on base type
                            if (properties.Length == 0)
                            {
                                pubProperties = this._entity.GetInheritancePublicationProperties().Where(e => e.DataRelationKey == node.Name).ToArray();
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

        #region events


        private void ApplyChanges()
        {
            //Check if has many EdmContexts
            this.Entity.QueryReturnType = EntityQueryReturnType.IEnumerable;
            this.Entity.GetTopBaseClass().CubeName = this.SelectedCubeName;
            this.Entity.PrimaryEntity = String.Empty;
            this.Entity.EnableMetaDataFilter = true;
            this.Entity.IsAggregationView = false;

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
            this.AddProperties(this.treeOlapItemTypes.Nodes);

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
                    if (!propertiesList[propIndex].DefaultValue.IsNullOrEmpty())
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
                    compare.Mask = propertiesList[propIndex].Mask;
                    compare.MaskType = propertiesList[propIndex].MaskType;
                    compare.CustomMediaTable = propertiesList[propIndex].CustomMediaTable;
                    compare.IsPK = propertiesList[propIndex].IsPK;
                    compare.IsNull = propertiesList[propIndex].IsNull;
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

            this.Entity.GenerateEntityOlapLookUps();


            this.Close();
        }


        private void AddProperties(TreeNodeCollection nodes)
        {
            OlapItemInfo attribute;
            if (nodes != null && nodes.Count > 0)
            {
                foreach (TreeNode node in nodes)
                {
                    if (node.Checked)
                    {
                        if (node.Tag != null && node.Tag is OlapItemInfo && !node.Name.IsNullOrEmpty())
                        {
                            if (this.Entity.EntityAdapterProperties.Where(e => !e.IsDeleted && e.IsCustomized && e.DataRelationKey == node.Name).Count() == 0)
                            {
                                attribute = ((OlapItemInfo)node.Tag);
                                EntityAdapterProperty property = new EntityAdapterProperty(this._entity.Partition);
                                property.EdmKey = String.Empty;
                                property.DataRelationKey = node.Name;
                                property.Name = property.DataRelationKey.Right(".").Replace("[", "").Replace("]", "").PrepareName();

                                //Check repetitions
                                int propsCnt = this.Entity.EntityAdapterProperties.Count(e => e.Name == property.Name);
                                if (propsCnt > 0)
                                    property.Name = property.Name + propsCnt.ToString();

                                property.Datatype = attribute.DataType.Name;
                                property.Precision = (attribute.OlapItemType == OlapItemEnum.Measure ? "20:" + (attribute.DataType.Name.ToLower().Contains("int") ? "0" : "2") : "0");
                                property.DataFormatString = (attribute.OlapItemType == OlapItemEnum.Measure ? "N" + (attribute.DataType.Name.ToLower().Contains("int") ? "0" : "2") : "");
                                property.IsBrowsable = true;
                                property.IsNull = (attribute.DataType.Name.ToLower().Contains("string") || attribute.DataType.Name.ToLower().Contains("nullable<") || attribute.DataType.Name.ToLower().Contains("?"));
                                property.Description = String.Empty;
                                property.ConnectedAttribute = String.Empty;
                                property.IsEditable = false;
                                property.DisplayName = node.Name.Right(".").Extract("[", "]").Replace("_", " ").Proper() + (propsCnt > 0 ? propsCnt.ToString() : "");
                                property.DisplayControl = (attribute.OlapItemType == OlapItemEnum.Measure ? DisplayControlType.NumericTextBox : DisplayControlType.TextBox);
                                property.GroupName = String.Empty;
                                property.DisplayOrder = -1;
                                property.DomainName = String.Empty;
                                property.IsCompulsory = false;
                                property.CustomValidationMethod = String.Empty;
                                property.CustomAttributes = String.Empty;
                                property.AggregationFunction = UIAggregationFunctions.None;
                                property.IsPublicationSuggestion = false;
                                property.RemoveValidations = false;
                                property.KpiName = String.Empty;
                                property.KpiRelatedAttribute = String.Empty;
                                property.Filter = String.Empty;
                                property.DefaultValue = String.Empty;
                                property.TargetKeyName = String.Empty;
                                property.IsCustomized = false;
                                property.IsAutomaticSequency = false;
                                property.LookUpSubscription = String.Empty;
                                property.Mask = String.Empty;
                                property.MaskType = String.Empty;
                                property.IsPK = false;
                                property.IsFK = false;
                                property.IsMeasure = attribute.OlapItemType == OlapItemEnum.Measure;

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


        private void cmbTypes_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            SelectedCubeName = (string)cmbTypes.SelectedItem;
            if (!SelectedCubeName.IsNullOrEmpty())
            {
                LoadCubeInfo();
                CheckTree(this.treeOlapItemTypes.Nodes);
            }
        }



        #endregion


        public FrmOlapBuilder()
        {
            InitializeComponent();
            this.treeOlapItemTypes.AfterCheck += new TreeViewEventHandler(treeOlapRelatedTypes_AfterCheck);
        }


        private void LoadCubeInfo()
        {
            treeOlapItemTypes.Nodes.Clear();

            if (string.IsNullOrEmpty(SelectedCubeName)) return;

            AddMeasures(LoadMeasures());

            foreach (var item in LoadDimensions())
            {
                PopulateTree(null, item);
            }

            List<string> checkedNames = new List<string>();
        }

        private void AddMeasures(OlapItemInfo[] measures)
        {
            var measureRoot = this.treeOlapItemTypes.Nodes.Add("Measures", "Measures", MeasureImageNumber, MeasureImageNumber);

            var measureGroups =
                from m in measures
                group m by m.GroupName into g
                select new
                {
                    Name = g.Key,
                    Items = g.ToArray()
                };

            foreach (var group in measureGroups)
            {
                string key = string.Format("Measures.Group.{0}", group.Name);
                var measureGroup = measureRoot.Nodes.Add(key, group.Name, FolderImageNumber, FolderImageNumber);

                foreach (var measure in group.Items)
                {
                    PopulateTree(measureGroup, measure);
                }
            }
        }

        private void PopulateTree(TreeNode parentNode, OlapItemInfo olapItem)
        {
            if (olapItem == null)
                return;

            int image = 0;
            switch (olapItem.OlapItemType)
            {
                case OlapItemEnum.Measure:
                    image = MeasureImageNumber;
                    break;
                case OlapItemEnum.Dimension:
                    image = 2;
                    break;
                case OlapItemEnum.Kpi:
                    image = 5;
                    break;
                case OlapItemEnum.DimensionProperty:
                    image = 7;
                    break;
                case OlapItemEnum.None:
                    image = FolderImageNumber;
                    break;
                default:
                    image = FolderImageNumber;
                    break;
            }
            TreeNode olapNode = (parentNode == null ?
                this.treeOlapItemTypes.Nodes.Add(olapItem.UniqueName, olapItem.DisplayName, image, image) :
                parentNode.Nodes.Add(olapItem.UniqueName, olapItem.DisplayName, image, image));

            olapNode.Tag = olapItem;

            if (olapItem.OlapItemType == OlapItemEnum.Dimension)
            {
                var dimItems = LoadDimensionProperties(olapItem.Name);
                foreach (var dimItem in dimItems)
                {
                    PopulateTree(olapNode, dimItem);
                }
            }

        }

        #region Load Olap Info

        private OlapItemInfo[] LoadMeasures()
        {
            var items = new List<OlapItemInfo>();

            using (AdomdConnection connection = new AdomdConnection(_catalog.Connection.GetConnectionString()))
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT MEASURE_NAME, MEASURE_UNIQUE_NAME, MEASURE_CAPTION, DATA_TYPE, MEASUREGROUP_NAME FROM $SYSTEM.MDSCHEMA_MEASURES " +
                " WHERE [CUBE_NAME]=@CUBE_NAME and MEASURE_IS_VISIBLE ORDER BY [MEASUREGROUP_NAME]";
                command.Parameters.Add(new AdomdParameter("CUBE_NAME", SelectedCubeName));

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    items = reader.Select(dr =>
                        new OlapItemInfo
                        {
                            Name = (string)dr["MEASURE_NAME"],
                            UniqueName = (string)dr["MEASURE_UNIQUE_NAME"],
                            DisplayName = (string)dr["MEASURE_CAPTION"],
                            DataTypeNumber = (ushort)dr["DATA_TYPE"],
                            OlapItemType = OlapItemEnum.Measure,
                            GroupName = (string)dr["MEASUREGROUP_NAME"]
                        }
                        ).ToList();
                }
            }

            return items.ToArray();
        }

        private OlapItemInfo[] LoadDimensions()
        {
            if (string.IsNullOrEmpty(SelectedCubeName)) return new OlapItemInfo[] { };

            var items = new List<OlapItemInfo>();

            using (AdomdConnection connection = new AdomdConnection(_catalog.Connection.GetConnectionString()))
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT DIMENSION_NAME, DIMENSION_CAPTION FROM $system.MDSchema_Dimensions " +
                " WHERE CUBE_NAME=@CUBE_NAME and DIMENSION_IS_VISIBLE AND DIMENSION_TYPE <> 2 ORDER BY DIMENSION_CAPTION";
                command.Parameters.Add(new AdomdParameter("CUBE_NAME", SelectedCubeName));

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    items = reader.Select(dr =>
                        new OlapItemInfo
                        {
                            Name = (string)dr["DIMENSION_NAME"],
                            DisplayName = (string)dr["DIMENSION_CAPTION"],
                            OlapItemType = OlapItemEnum.Dimension
                        }
                        ).ToList();
                }
            }

            return items.ToArray();
        }

        private OlapItemInfo[] LoadDimensionProperties(string dimensionName)
        {
            var items = new List<OlapItemInfo>();

            using (AdomdConnection connection = new AdomdConnection(_catalog.Connection.GetConnectionString()))
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT LEVEL_CAPTION, LEVEL_NAME, [LEVEL_UNIQUE_NAME], LEVEL_DBTYPE FROM $system.MDSchema_levels " +
                " WHERE CUBE_NAME=@CUBE_NAME AND [DIMENSION_UNIQUE_NAME]=@DIMENSION_NAME AND LEVEL_ORIGIN=2 AND LEVEL_NAME <> '(All)' AND LEVEL_IS_VISIBLE";
                command.Parameters.Add(new AdomdParameter("CUBE_NAME", SelectedCubeName));
                command.Parameters.Add(new AdomdParameter("DIMENSION_NAME", string.Format("[{0}]", dimensionName)));

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    items = reader.Select(dr =>
                        new OlapItemInfo
                        {
                            Name = (string)dr["LEVEL_NAME"],
                            UniqueName = (string)dr["LEVEL_UNIQUE_NAME"],
                            DisplayName = (string)dr["LEVEL_CAPTION"],
                            DataTypeNumber = (ushort)(int)dr["LEVEL_DBTYPE"],
                            OlapItemType = OlapItemEnum.DimensionProperty
                        }
                        ).ToList();
                }
            }


            return items.ToArray();
        }


        private OlapItemInfo[] LoadKpis()
        {
            var items = new List<OlapItemInfo>();

            using (AdomdConnection connection = new AdomdConnection(_catalog.Connection.GetConnectionString()))
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT KPI_NAME, KPI_CAPTION, MEASUREGROUP_NAME, KPI_VALUE, KPI_STATUS " +
                    " FROM $system.MDSCHEMA_KPIs WHERE [CUBE_NAME]='Model' ORDER BY KPI_CAPTION  ";
                command.Parameters.Add(new AdomdParameter("CUBE_NAME", SelectedCubeName));
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    items = reader.Select(dr =>
                        new OlapItemInfo
                        {
                            Name = (string)dr["KPI_NAME"],
                            UniqueName = (string)dr["KPI_VALUE"],
                            DisplayName = (string)dr["KPI_CAPTION"],
                            GroupName = (string)dr["MEASUREGROUP_NAME"],
                            OlapItemType = OlapItemEnum.Kpi
                        }
                        ).ToList();
                }

            }
            return items.ToArray();
        }

        #endregion

        private void treeOlapRelatedTypes_AfterCheck(object sender, TreeViewEventArgs e)
        {
            this.treeOlapItemTypes.AfterCheck -= new TreeViewEventHandler(treeOlapRelatedTypes_AfterCheck);
            this.CheckNodeParent(e.Node);
            this.CheckNodeChildren(e.Node);
            this.treeOlapItemTypes.AfterCheck += new TreeViewEventHandler(treeOlapRelatedTypes_AfterCheck);
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

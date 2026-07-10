namespace Linx.BusinessDataModelDesigner.CustomCode
{
    partial class frmBusinessViewBuilder
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label relationTypeLabel;
            System.Windows.Forms.Label aliasLabel;
            System.Windows.Forms.Label updatableLabel;
            System.Windows.Forms.Label keyLabel;
            System.Windows.Forms.Label whereClauseLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBusinessViewBuilder));
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.imgTree = new System.Windows.Forms.ImageList(this.components);
            this.progressModels = new System.Windows.Forms.ProgressBar();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listClasses = new System.Windows.Forms.TreeView();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tlbbtnRemoveEntity = new System.Windows.Forms.ToolStripButton();
            this.treeViewQuery = new System.Windows.Forms.TreeView();
            this.panelTopRelatons = new System.Windows.Forms.Panel();
            this.justFirstRightRelationCheckBox = new System.Windows.Forms.CheckBox();
            this.entityQueryNodeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.relationTypeComboBox = new System.Windows.Forms.ComboBox();
            this.relationsNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.relationsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.propertiesDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DisplayName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Formula = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SourceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Precision = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Scale = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaxLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DomainName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.propertiesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panelEntityDetails = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.whereClauseTextBox = new System.Windows.Forms.TextBox();
            this.keyTextBox = new System.Windows.Forms.TextBox();
            this.updatableCheckBox = new System.Windows.Forms.CheckBox();
            this.aliasTextBox = new System.Windows.Forms.TextBox();
            this.relationsDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridSourceExpColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridOperationColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridTargetExpColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.btCancel = new System.Windows.Forms.Button();
            this.btApply = new System.Windows.Forms.Button();
            relationTypeLabel = new System.Windows.Forms.Label();
            aliasLabel = new System.Windows.Forms.Label();
            updatableLabel = new System.Windows.Forms.Label();
            keyLabel = new System.Windows.Forms.Label();
            whereClauseLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panelTopRelatons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.entityQueryNodeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.relationsNavigator)).BeginInit();
            this.relationsNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.relationsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.propertiesDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.propertiesBindingSource)).BeginInit();
            this.panelEntityDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.relationsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // relationTypeLabel
            // 
            relationTypeLabel.AutoSize = true;
            relationTypeLabel.Location = new System.Drawing.Point(89, 10);
            relationTypeLabel.Name = "relationTypeLabel";
            relationTypeLabel.Size = new System.Drawing.Size(76, 13);
            relationTypeLabel.TabIndex = 27;
            relationTypeLabel.Text = "Relation Type:";
            // 
            // aliasLabel
            // 
            aliasLabel.AutoSize = true;
            aliasLabel.Location = new System.Drawing.Point(3, 9);
            aliasLabel.Name = "aliasLabel";
            aliasLabel.Size = new System.Drawing.Size(32, 13);
            aliasLabel.TabIndex = 28;
            aliasLabel.Text = "Alias:";
            // 
            // updatableLabel
            // 
            updatableLabel.AutoSize = true;
            updatableLabel.Location = new System.Drawing.Point(272, 9);
            updatableLabel.Name = "updatableLabel";
            updatableLabel.Size = new System.Drawing.Size(59, 13);
            updatableLabel.TabIndex = 29;
            updatableLabel.Text = "Updatable:";
            // 
            // keyLabel
            // 
            keyLabel.AutoSize = true;
            keyLabel.Location = new System.Drawing.Point(365, 9);
            keyLabel.Name = "keyLabel";
            keyLabel.Size = new System.Drawing.Size(28, 13);
            keyLabel.TabIndex = 30;
            keyLabel.Text = "Key:";
            // 
            // whereClauseLabel
            // 
            whereClauseLabel.AutoSize = true;
            whereClauseLabel.Location = new System.Drawing.Point(3, 33);
            whereClauseLabel.Name = "whereClauseLabel";
            whereClauseLabel.Size = new System.Drawing.Size(77, 13);
            whereClauseLabel.TabIndex = 31;
            whereClauseLabel.Text = "Where Clause:";
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(6, 6);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(41, 13);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Search";
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Location = new System.Drawing.Point(57, 3);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(970, 20);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // imgTree
            // 
            this.imgTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgTree.ImageStream")));
            this.imgTree.TransparentColor = System.Drawing.Color.Transparent;
            this.imgTree.Images.SetKeyName(0, "Entity.png");
            this.imgTree.Images.SetKeyName(1, "InnerJoin.png");
            this.imgTree.Images.SetKeyName(2, "LeftJoin.png");
            this.imgTree.Images.SetKeyName(3, "Property.png");
            this.imgTree.Images.SetKeyName(4, "Union.png");
            // 
            // progressModels
            // 
            this.progressModels.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.progressModels.Location = new System.Drawing.Point(1029, 3);
            this.progressModels.Name = "progressModels";
            this.progressModels.Size = new System.Drawing.Size(133, 20);
            this.progressModels.TabIndex = 33;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Location = new System.Drawing.Point(9, 28);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listClasses);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox2);
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox1);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(1153, 564);
            this.splitContainer1.SplitterDistance = 142;
            this.splitContainer1.TabIndex = 34;
            // 
            // listClasses
            // 
            this.listClasses.AllowDrop = true;
            this.listClasses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listClasses.BackColor = System.Drawing.SystemColors.Info;
            this.listClasses.HideSelection = false;
            this.listClasses.ImageIndex = 0;
            this.listClasses.ImageList = this.imgTree;
            this.listClasses.Location = new System.Drawing.Point(4, 3);
            this.listClasses.Name = "listClasses";
            this.listClasses.SelectedImageIndex = 0;
            this.listClasses.Size = new System.Drawing.Size(1143, 134);
            this.listClasses.TabIndex = 29;
            this.listClasses.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.listClasses_BeforeExpand);
            this.listClasses.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.listClasses_AfterSelect);
            this.listClasses.DragOver += new System.Windows.Forms.DragEventHandler(this.listClasses_DragOver);
            this.listClasses.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listClasses_MouseDown);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(543, 6);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(18, 18);
            this.pictureBox2.TabIndex = 35;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(11, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(18, 18);
            this.pictureBox1.TabIndex = 34;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label2.Location = new System.Drawing.Point(564, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(403, 13);
            this.label2.TabIndex = 33;
            this.label2.Text = "2. Drag an entity and drop over some entity below for creating JOINS.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label1.Location = new System.Drawing.Point(32, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(497, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "1. Drag entities and drop into the tree below for creating top level QUERIES/UNIO" +
    "NS.";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.splitContainer2);
            this.groupBox1.Location = new System.Drawing.Point(5, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1146, 386);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Query Definition";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Location = new System.Drawing.Point(6, 19);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.toolStrip1);
            this.splitContainer2.Panel1.Controls.Add(this.treeViewQuery);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.AutoScroll = true;
            this.splitContainer2.Panel2.Controls.Add(this.panelTopRelatons);
            this.splitContainer2.Panel2.Controls.Add(this.propertiesDataGridView);
            this.splitContainer2.Panel2.Controls.Add(this.panelEntityDetails);
            this.splitContainer2.Panel2.Controls.Add(this.relationsDataGridView);
            this.splitContainer2.Size = new System.Drawing.Size(1134, 361);
            this.splitContainer2.SplitterDistance = 416;
            this.splitContainer2.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlbbtnRemoveEntity});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(414, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tlbbtnRemoveEntity
            // 
            this.tlbbtnRemoveEntity.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlbbtnRemoveEntity.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtnRemoveEntity.Image")));
            this.tlbbtnRemoveEntity.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtnRemoveEntity.Name = "tlbbtnRemoveEntity";
            this.tlbbtnRemoveEntity.Size = new System.Drawing.Size(23, 22);
            this.tlbbtnRemoveEntity.Text = "Remove Entity";
            this.tlbbtnRemoveEntity.Click += new System.EventHandler(this.tlbbtnRemoveEntity_Click);
            // 
            // treeViewQuery
            // 
            this.treeViewQuery.AllowDrop = true;
            this.treeViewQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewQuery.BackColor = System.Drawing.SystemColors.Info;
            this.treeViewQuery.HideSelection = false;
            this.treeViewQuery.ImageIndex = 0;
            this.treeViewQuery.ImageList = this.imgTree;
            this.treeViewQuery.Location = new System.Drawing.Point(3, 28);
            this.treeViewQuery.Name = "treeViewQuery";
            this.treeViewQuery.SelectedImageIndex = 0;
            this.treeViewQuery.Size = new System.Drawing.Size(408, 328);
            this.treeViewQuery.TabIndex = 0;
            this.treeViewQuery.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewQuery_AfterSelect);
            this.treeViewQuery.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeViewQuery_DragDrop);
            this.treeViewQuery.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeViewQuery_DragEnter);
            // 
            // panelTopRelatons
            // 
            this.panelTopRelatons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTopRelatons.Controls.Add(this.justFirstRightRelationCheckBox);
            this.panelTopRelatons.Controls.Add(relationTypeLabel);
            this.panelTopRelatons.Controls.Add(this.relationTypeComboBox);
            this.panelTopRelatons.Controls.Add(this.relationsNavigator);
            this.panelTopRelatons.Location = new System.Drawing.Point(5, 2);
            this.panelTopRelatons.Name = "panelTopRelatons";
            this.panelTopRelatons.Size = new System.Drawing.Size(703, 32);
            this.panelTopRelatons.TabIndex = 29;
            // 
            // justFirstRightRelationCheckBox
            // 
            this.justFirstRightRelationCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.entityQueryNodeBindingSource, "JustFirstRightRelation", true));
            this.justFirstRightRelationCheckBox.Location = new System.Drawing.Point(298, 5);
            this.justFirstRightRelationCheckBox.Name = "justFirstRightRelationCheckBox";
            this.justFirstRightRelationCheckBox.Size = new System.Drawing.Size(157, 24);
            this.justFirstRightRelationCheckBox.TabIndex = 38;
            this.justFirstRightRelationCheckBox.Text = "Apply \"Top 1\" command";
            this.justFirstRightRelationCheckBox.UseVisualStyleBackColor = false;
            // 
            // entityQueryNodeBindingSource
            // 
            this.entityQueryNodeBindingSource.DataSource = typeof(Linx.BusinessDataModelDesigner.CustomizedCode.Util.EntityQueryNode);
            // 
            // relationTypeComboBox
            // 
            this.relationTypeComboBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.entityQueryNodeBindingSource, "RelationType", true));
            this.relationTypeComboBox.FormattingEnabled = true;
            this.relationTypeComboBox.Items.AddRange(new object[] {
            "InnerJoin",
            "LeftJoin"});
            this.relationTypeComboBox.Location = new System.Drawing.Point(171, 6);
            this.relationTypeComboBox.Name = "relationTypeComboBox";
            this.relationTypeComboBox.Size = new System.Drawing.Size(121, 21);
            this.relationTypeComboBox.TabIndex = 28;
            this.relationTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.relationTypeComboBox_SelectedIndexChanged);
            // 
            // relationsNavigator
            // 
            this.relationsNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.relationsNavigator.BindingSource = this.relationsBindingSource;
            this.relationsNavigator.CountItem = null;
            this.relationsNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.relationsNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem});
            this.relationsNavigator.Location = new System.Drawing.Point(0, 0);
            this.relationsNavigator.MoveFirstItem = null;
            this.relationsNavigator.MoveLastItem = null;
            this.relationsNavigator.MoveNextItem = null;
            this.relationsNavigator.MovePreviousItem = null;
            this.relationsNavigator.Name = "relationsNavigator";
            this.relationsNavigator.PositionItem = null;
            this.relationsNavigator.Size = new System.Drawing.Size(703, 25);
            this.relationsNavigator.TabIndex = 28;
            this.relationsNavigator.Text = "relationsNavigator";
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "Add new";
            // 
            // relationsBindingSource
            // 
            this.relationsBindingSource.DataMember = "Relations";
            this.relationsBindingSource.DataSource = this.entityQueryNodeBindingSource;
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "Delete";
            // 
            // propertiesDataGridView
            // 
            this.propertiesDataGridView.AllowUserToAddRows = false;
            this.propertiesDataGridView.AllowUserToDeleteRows = false;
            this.propertiesDataGridView.AllowUserToOrderColumns = true;
            this.propertiesDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propertiesDataGridView.AutoGenerateColumns = false;
            this.propertiesDataGridView.BackgroundColor = System.Drawing.SystemColors.Info;
            this.propertiesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.propertiesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn3,
            this.dataGridViewTextBoxColumn1,
            this.DisplayName,
            this.Formula,
            this.SourceName,
            this.dataGridViewCheckBoxColumn1,
            this.dataGridViewCheckBoxColumn2,
            this.dataGridViewTextBoxColumn2,
            this.Precision,
            this.Scale,
            this.MaxLength,
            this.DomainName});
            this.propertiesDataGridView.DataSource = this.propertiesBindingSource;
            this.propertiesDataGridView.Location = new System.Drawing.Point(3, 90);
            this.propertiesDataGridView.Name = "propertiesDataGridView";
            this.propertiesDataGridView.Size = new System.Drawing.Size(706, 266);
            this.propertiesDataGridView.TabIndex = 1;
            // 
            // dataGridViewCheckBoxColumn3
            // 
            this.dataGridViewCheckBoxColumn3.DataPropertyName = "Selected";
            this.dataGridViewCheckBoxColumn3.FillWeight = 50F;
            this.dataGridViewCheckBoxColumn3.HeaderText = "";
            this.dataGridViewCheckBoxColumn3.Name = "dataGridViewCheckBoxColumn3";
            this.dataGridViewCheckBoxColumn3.Width = 50;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Name";
            this.dataGridViewTextBoxColumn1.FillWeight = 150F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Name";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 150;
            // 
            // DisplayName
            // 
            this.DisplayName.DataPropertyName = "DisplayName";
            this.DisplayName.FillWeight = 150F;
            this.DisplayName.HeaderText = "Display";
            this.DisplayName.Name = "DisplayName";
            this.DisplayName.Width = 150;
            // 
            // Formula
            // 
            this.Formula.DataPropertyName = "Formula";
            this.Formula.FillWeight = 150F;
            this.Formula.HeaderText = "Formula";
            this.Formula.Name = "Formula";
            this.Formula.Width = 150;
            // 
            // SourceName
            // 
            this.SourceName.DataPropertyName = "SourceName";
            this.SourceName.FillWeight = 150F;
            this.SourceName.HeaderText = "Source Name";
            this.SourceName.Name = "SourceName";
            this.SourceName.ReadOnly = true;
            this.SourceName.Width = 150;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.DataPropertyName = "PrimaryKey";
            this.dataGridViewCheckBoxColumn1.FillWeight = 50F;
            this.dataGridViewCheckBoxColumn1.HeaderText = "PK";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.ReadOnly = true;
            this.dataGridViewCheckBoxColumn1.Width = 50;
            // 
            // dataGridViewCheckBoxColumn2
            // 
            this.dataGridViewCheckBoxColumn2.DataPropertyName = "Nullable";
            this.dataGridViewCheckBoxColumn2.FillWeight = 25F;
            this.dataGridViewCheckBoxColumn2.HeaderText = "Nullable";
            this.dataGridViewCheckBoxColumn2.Name = "dataGridViewCheckBoxColumn2";
            this.dataGridViewCheckBoxColumn2.ReadOnly = true;
            this.dataGridViewCheckBoxColumn2.Width = 50;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Type";
            this.dataGridViewTextBoxColumn2.FillWeight = 150F;
            this.dataGridViewTextBoxColumn2.HeaderText = "Type";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 150;
            // 
            // Precision
            // 
            this.Precision.DataPropertyName = "Precision";
            this.Precision.HeaderText = "Precision";
            this.Precision.Name = "Precision";
            this.Precision.ReadOnly = true;
            // 
            // Scale
            // 
            this.Scale.DataPropertyName = "Scale";
            this.Scale.HeaderText = "Scale";
            this.Scale.Name = "Scale";
            this.Scale.ReadOnly = true;
            // 
            // MaxLength
            // 
            this.MaxLength.DataPropertyName = "MaxLength";
            this.MaxLength.HeaderText = "Max Length";
            this.MaxLength.Name = "MaxLength";
            this.MaxLength.ReadOnly = true;
            // 
            // DomainName
            // 
            this.DomainName.DataPropertyName = "DomainName";
            this.DomainName.HeaderText = "Domain Name";
            this.DomainName.Name = "DomainName";
            this.DomainName.ReadOnly = true;
            // 
            // propertiesBindingSource
            // 
            this.propertiesBindingSource.DataMember = "Properties";
            this.propertiesBindingSource.DataSource = this.entityQueryNodeBindingSource;
            // 
            // panelEntityDetails
            // 
            this.panelEntityDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEntityDetails.Controls.Add(this.pictureBox3);
            this.panelEntityDetails.Controls.Add(whereClauseLabel);
            this.panelEntityDetails.Controls.Add(this.label3);
            this.panelEntityDetails.Controls.Add(this.whereClauseTextBox);
            this.panelEntityDetails.Controls.Add(keyLabel);
            this.panelEntityDetails.Controls.Add(this.keyTextBox);
            this.panelEntityDetails.Controls.Add(updatableLabel);
            this.panelEntityDetails.Controls.Add(this.updatableCheckBox);
            this.panelEntityDetails.Controls.Add(this.aliasTextBox);
            this.panelEntityDetails.Controls.Add(aliasLabel);
            this.panelEntityDetails.Location = new System.Drawing.Point(5, 2);
            this.panelEntityDetails.Name = "panelEntityDetails";
            this.panelEntityDetails.Size = new System.Drawing.Size(707, 106);
            this.panelEntityDetails.TabIndex = 30;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(84, 68);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(18, 18);
            this.pictureBox3.TabIndex = 37;
            this.pictureBox3.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label3.Location = new System.Drawing.Point(104, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(550, 13);
            this.label3.TabIndex = 36;
            this.label3.Text = "Example: (HasFilter(PROPERTY_NAME) && this.PROPERTY1 = \"AAXX\" || this.PROPERTY2 >" +
    " 20)";
            // 
            // whereClauseTextBox
            // 
            this.whereClauseTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.whereClauseTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.entityQueryNodeBindingSource, "WhereClause", true));
            this.whereClauseTextBox.Location = new System.Drawing.Point(82, 30);
            this.whereClauseTextBox.Multiline = true;
            this.whereClauseTextBox.Name = "whereClauseTextBox";
            this.whereClauseTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.whereClauseTextBox.Size = new System.Drawing.Size(619, 37);
            this.whereClauseTextBox.TabIndex = 32;
            // 
            // keyTextBox
            // 
            this.keyTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.keyTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.entityQueryNodeBindingSource, "Key", true));
            this.keyTextBox.Location = new System.Drawing.Point(397, 6);
            this.keyTextBox.Name = "keyTextBox";
            this.keyTextBox.ReadOnly = true;
            this.keyTextBox.Size = new System.Drawing.Size(304, 20);
            this.keyTextBox.TabIndex = 31;
            // 
            // updatableCheckBox
            // 
            this.updatableCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.entityQueryNodeBindingSource, "Updatable", true));
            this.updatableCheckBox.Location = new System.Drawing.Point(335, 10);
            this.updatableCheckBox.Name = "updatableCheckBox";
            this.updatableCheckBox.Size = new System.Drawing.Size(21, 14);
            this.updatableCheckBox.TabIndex = 30;
            this.updatableCheckBox.UseVisualStyleBackColor = true;
            // 
            // aliasTextBox
            // 
            this.aliasTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.entityQueryNodeBindingSource, "Alias", true));
            this.aliasTextBox.Location = new System.Drawing.Point(82, 6);
            this.aliasTextBox.Name = "aliasTextBox";
            this.aliasTextBox.Size = new System.Drawing.Size(176, 20);
            this.aliasTextBox.TabIndex = 29;
            // 
            // relationsDataGridView
            // 
            this.relationsDataGridView.AllowUserToAddRows = false;
            this.relationsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.relationsDataGridView.AutoGenerateColumns = false;
            this.relationsDataGridView.BackgroundColor = System.Drawing.SystemColors.Info;
            this.relationsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.relationsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridSourceExpColumn,
            this.dataGridOperationColumn,
            this.dataGridTargetExpColumn});
            this.relationsDataGridView.DataSource = this.relationsBindingSource;
            this.relationsDataGridView.Location = new System.Drawing.Point(3, 35);
            this.relationsDataGridView.MultiSelect = false;
            this.relationsDataGridView.Name = "relationsDataGridView";
            this.relationsDataGridView.Size = new System.Drawing.Size(706, 321);
            this.relationsDataGridView.TabIndex = 1;
            this.relationsDataGridView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.relationsDataGridView_CellValidating);
            this.relationsDataGridView.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.relationsDataGridView_EditingControlShowing);
            // 
            // dataGridSourceExpColumn
            // 
            this.dataGridSourceExpColumn.DataPropertyName = "SourceExpression";
            this.dataGridSourceExpColumn.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.dataGridSourceExpColumn.FillWeight = 250F;
            this.dataGridSourceExpColumn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dataGridSourceExpColumn.HeaderText = "Left Expression";
            this.dataGridSourceExpColumn.Name = "dataGridSourceExpColumn";
            this.dataGridSourceExpColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridSourceExpColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridSourceExpColumn.Width = 250;
            // 
            // dataGridOperationColumn
            // 
            this.dataGridOperationColumn.DataPropertyName = "Operator";
            this.dataGridOperationColumn.FillWeight = 40F;
            this.dataGridOperationColumn.HeaderText = "";
            this.dataGridOperationColumn.Name = "dataGridOperationColumn";
            this.dataGridOperationColumn.ReadOnly = true;
            this.dataGridOperationColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridOperationColumn.Width = 40;
            // 
            // dataGridTargetExpColumn
            // 
            this.dataGridTargetExpColumn.DataPropertyName = "TargetExpression";
            this.dataGridTargetExpColumn.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.dataGridTargetExpColumn.FillWeight = 250F;
            this.dataGridTargetExpColumn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dataGridTargetExpColumn.HeaderText = "Right Expression";
            this.dataGridTargetExpColumn.Name = "dataGridTargetExpColumn";
            this.dataGridTargetExpColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridTargetExpColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridTargetExpColumn.Width = 250;
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.Location = new System.Drawing.Point(1087, 598);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 36;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btApply
            // 
            this.btApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btApply.Location = new System.Drawing.Point(1007, 598);
            this.btApply.Name = "btApply";
            this.btApply.Size = new System.Drawing.Size(75, 23);
            this.btApply.TabIndex = 35;
            this.btApply.Text = "Apply";
            this.btApply.UseVisualStyleBackColor = true;
            this.btApply.Click += new System.EventHandler(this.btApply_Click);
            // 
            // frmBusinessViewBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1171, 626);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btApply);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.progressModels);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblSearch);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(270, 250);
            this.Name = "frmBusinessViewBuilder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Business View Builder";
            this.Activated += new System.EventHandler(this.frmBusinessViewBuilder_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmBusinessViewBuilder_FormClosed);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panelTopRelatons.ResumeLayout(false);
            this.panelTopRelatons.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.entityQueryNodeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.relationsNavigator)).EndInit();
            this.relationsNavigator.ResumeLayout(false);
            this.relationsNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.relationsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.propertiesDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.propertiesBindingSource)).EndInit();
            this.panelEntityDetails.ResumeLayout(false);
            this.panelEntityDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.relationsDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ProgressBar progressModels;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView relationsDataGridView;
        private System.Windows.Forms.BindingSource relationsBindingSource;
        private System.Windows.Forms.BindingSource entityQueryNodeBindingSource;
        private System.Windows.Forms.BindingSource propertiesBindingSource;
        private System.Windows.Forms.DataGridView propertiesDataGridView;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TreeView treeViewQuery;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btApply;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tlbbtnRemoveEntity;
        private System.Windows.Forms.ImageList imgTree;
        private System.Windows.Forms.ComboBox relationTypeComboBox;
        private System.Windows.Forms.Panel panelTopRelatons;
        private System.Windows.Forms.BindingNavigator relationsNavigator;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.Panel panelEntityDetails;
        private System.Windows.Forms.TextBox aliasTextBox;
        private System.Windows.Forms.CheckBox updatableCheckBox;
        private System.Windows.Forms.TextBox keyTextBox;
        private System.Windows.Forms.TextBox whereClauseTextBox;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridSourceExpColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridOperationColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridTargetExpColumn;
        private System.Windows.Forms.TreeView listClasses;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn DisplayName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Formula;
        private System.Windows.Forms.DataGridViewTextBoxColumn SourceName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Precision;
        private System.Windows.Forms.DataGridViewTextBoxColumn Scale;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaxLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn DomainName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox justFirstRightRelationCheckBox;

       
    }
}
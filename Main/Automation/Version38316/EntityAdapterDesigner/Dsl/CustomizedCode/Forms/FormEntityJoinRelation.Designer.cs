namespace Linx.EntityAdapterDesigner.CustomizedCode
{
    partial class FormEntityJoinRelation
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
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEntityJoinRelation));
            this.btnApplyOrder = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.entityJoinRelationBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.entityJoinRelationBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.entityJoinRelationDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewComboTargetProperty = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewComboSourceProperty = new System.Windows.Forms.DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.entityJoinRelationBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entityJoinRelationBindingNavigator)).BeginInit();
            this.entityJoinRelationBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.entityJoinRelationDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // btnApplyOrder
            // 
            this.btnApplyOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApplyOrder.Location = new System.Drawing.Point(355, 352);
            this.btnApplyOrder.Name = "btnApplyOrder";
            this.btnApplyOrder.Size = new System.Drawing.Size(92, 23);
            this.btnApplyOrder.TabIndex = 2;
            this.btnApplyOrder.Text = "Apply";
            this.btnApplyOrder.UseVisualStyleBackColor = true;
            this.btnApplyOrder.Click += new System.EventHandler(this.btnApplyOrder_Click);
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.Location = new System.Drawing.Point(451, 352);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(92, 23);
            this.btCancel.TabIndex = 24;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // entityJoinRelationBindingSource
            // 
            this.entityJoinRelationBindingSource.DataSource = typeof(Linx.EntityAdapterDesigner.CustomizedCode.EntityJoinRelation);
            // 
            // entityJoinRelationBindingNavigator
            // 
            this.entityJoinRelationBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.entityJoinRelationBindingNavigator.BindingSource = this.entityJoinRelationBindingSource;
            this.entityJoinRelationBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.entityJoinRelationBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.entityJoinRelationBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem});
            this.entityJoinRelationBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.entityJoinRelationBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.entityJoinRelationBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.entityJoinRelationBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.entityJoinRelationBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.entityJoinRelationBindingNavigator.Name = "entityJoinRelationBindingNavigator";
            this.entityJoinRelationBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.entityJoinRelationBindingNavigator.Size = new System.Drawing.Size(546, 25);
            this.entityJoinRelationBindingNavigator.TabIndex = 25;
            this.entityJoinRelationBindingNavigator.Text = "bindingNavigator1";
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
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
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
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // entityJoinRelationDataGridView
            // 
            this.entityJoinRelationDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.entityJoinRelationDataGridView.AutoGenerateColumns = false;
            this.entityJoinRelationDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.entityJoinRelationDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewComboTargetProperty,
            this.dataGridViewComboSourceProperty});
            this.entityJoinRelationDataGridView.DataSource = this.entityJoinRelationBindingSource;
            this.entityJoinRelationDataGridView.Location = new System.Drawing.Point(0, 28);
            this.entityJoinRelationDataGridView.Name = "entityJoinRelationDataGridView";
            this.entityJoinRelationDataGridView.Size = new System.Drawing.Size(543, 318);
            this.entityJoinRelationDataGridView.TabIndex = 25;
            // 
            // dataGridViewComboTargetProperty
            // 
            this.dataGridViewComboTargetProperty.DataPropertyName = "TargetProperty";
            this.dataGridViewComboTargetProperty.HeaderText = "TargetProperty";
            this.dataGridViewComboTargetProperty.Name = "dataGridViewComboTargetProperty";
            this.dataGridViewComboTargetProperty.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewComboTargetProperty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewComboTargetProperty.Width = 250;
            // 
            // dataGridViewComboSourceProperty
            // 
            this.dataGridViewComboSourceProperty.DataPropertyName = "SourceProperty";
            this.dataGridViewComboSourceProperty.HeaderText = "SourceProperty";
            this.dataGridViewComboSourceProperty.Name = "dataGridViewComboSourceProperty";
            this.dataGridViewComboSourceProperty.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewComboSourceProperty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewComboSourceProperty.Width = 250;
            // 
            // FormEntityJoinRelation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 379);
            this.Controls.Add(this.entityJoinRelationDataGridView);
            this.Controls.Add(this.entityJoinRelationBindingNavigator);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btnApplyOrder);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormEntityJoinRelation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Join Configuration";
            ((System.ComponentModel.ISupportInitialize)(this.entityJoinRelationBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entityJoinRelationBindingNavigator)).EndInit();
            this.entityJoinRelationBindingNavigator.ResumeLayout(false);
            this.entityJoinRelationBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.entityJoinRelationDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnApplyOrder;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.BindingSource entityJoinRelationBindingSource;
        private System.Windows.Forms.BindingNavigator entityJoinRelationBindingNavigator;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.DataGridView entityJoinRelationDataGridView;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboTargetProperty;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboSourceProperty;

    }
}
namespace Linx.EntityAdapterDesigner.CustomCode
{
    partial class FormAddExtendedFilter
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
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAddExtendedFilter));
            this.entityAdapterPropertyExtendedFiltersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.entityAdapterExtendedFilterBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.entityAdapterExtendedFilterDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.entityAdapterPropertyExtendedFiltersDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.entityAdapterPropertyExtendedFiltersBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entityAdapterExtendedFilterBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.entityAdapterExtendedFilterDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entityAdapterPropertyExtendedFiltersDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(5, 7);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(37, 13);
            label1.TabIndex = 5;
            label1.Text = "Fields:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(3, 7);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(221, 13);
            label2.TabIndex = 6;
            label2.Text = "Entities (You can select  more then one row): ";
            // 
            // entityAdapterPropertyExtendedFiltersBindingSource
            // 
            this.entityAdapterPropertyExtendedFiltersBindingSource.AllowNew = false;
            this.entityAdapterPropertyExtendedFiltersBindingSource.DataMember = "EntityAdapterPropertyExtendedFilters";
            this.entityAdapterPropertyExtendedFiltersBindingSource.DataSource = this.entityAdapterExtendedFilterBindingSource;
            // 
            // entityAdapterExtendedFilterBindingSource
            // 
            this.entityAdapterExtendedFilterBindingSource.DataSource = typeof(Linx.EntityAdapterDesigner.EntityAdapterExtendedFilter);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Location = new System.Drawing.Point(5, 4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.entityAdapterExtendedFilterDataGridView);
            this.splitContainer1.Panel1.Controls.Add(label2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(label1);
            this.splitContainer1.Panel2.Controls.Add(this.entityAdapterPropertyExtendedFiltersDataGridView);
            this.splitContainer1.Size = new System.Drawing.Size(774, 396);
            this.splitContainer1.SplitterDistance = 258;
            this.splitContainer1.TabIndex = 4;
            // 
            // entityAdapterExtendedFilterDataGridView
            // 
            this.entityAdapterExtendedFilterDataGridView.AllowUserToAddRows = false;
            this.entityAdapterExtendedFilterDataGridView.AllowUserToDeleteRows = false;
            this.entityAdapterExtendedFilterDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.entityAdapterExtendedFilterDataGridView.AutoGenerateColumns = false;
            this.entityAdapterExtendedFilterDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.entityAdapterExtendedFilterDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7});
            this.entityAdapterExtendedFilterDataGridView.DataSource = this.entityAdapterExtendedFilterBindingSource;
            this.entityAdapterExtendedFilterDataGridView.Location = new System.Drawing.Point(4, 26);
            this.entityAdapterExtendedFilterDataGridView.Name = "entityAdapterExtendedFilterDataGridView";
            this.entityAdapterExtendedFilterDataGridView.RowHeadersVisible = false;
            this.entityAdapterExtendedFilterDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.entityAdapterExtendedFilterDataGridView.Size = new System.Drawing.Size(245, 361);
            this.entityAdapterExtendedFilterDataGridView.TabIndex = 6;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "DisplayName";
            this.dataGridViewTextBoxColumn8.HeaderText = "Display Name";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Width = 250;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "EntityName";
            this.dataGridViewTextBoxColumn6.HeaderText = "Entity Name";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 200;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "RelationName";
            this.dataGridViewTextBoxColumn7.HeaderText = "Relation Name";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 200;
            // 
            // entityAdapterPropertyExtendedFiltersDataGridView
            // 
            this.entityAdapterPropertyExtendedFiltersDataGridView.AllowUserToAddRows = false;
            this.entityAdapterPropertyExtendedFiltersDataGridView.AllowUserToDeleteRows = false;
            this.entityAdapterPropertyExtendedFiltersDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.entityAdapterPropertyExtendedFiltersDataGridView.AutoGenerateColumns = false;
            this.entityAdapterPropertyExtendedFiltersDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.entityAdapterPropertyExtendedFiltersDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.entityAdapterPropertyExtendedFiltersDataGridView.DataSource = this.entityAdapterPropertyExtendedFiltersBindingSource;
            this.entityAdapterPropertyExtendedFiltersDataGridView.Location = new System.Drawing.Point(5, 26);
            this.entityAdapterPropertyExtendedFiltersDataGridView.MultiSelect = false;
            this.entityAdapterPropertyExtendedFiltersDataGridView.Name = "entityAdapterPropertyExtendedFiltersDataGridView";
            this.entityAdapterPropertyExtendedFiltersDataGridView.RowHeadersVisible = false;
            this.entityAdapterPropertyExtendedFiltersDataGridView.Size = new System.Drawing.Size(498, 361);
            this.entityAdapterPropertyExtendedFiltersDataGridView.TabIndex = 4;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.DataPropertyName = "IsEnabled";
            this.dataGridViewCheckBoxColumn1.HeaderText = "Enabled";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Width = 70;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "DisplayName";
            this.dataGridViewTextBoxColumn2.HeaderText = "Display Name";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 400;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Name";
            this.dataGridViewTextBoxColumn1.HeaderText = "Name";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 300;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "DataType";
            this.dataGridViewTextBoxColumn3.HeaderText = "Data Type";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "EdmKey";
            this.dataGridViewTextBoxColumn4.HeaderText = "Edm Key";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 300;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(619, 406);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(700, 406);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FormAddExtendedFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 434);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAddExtendedFilter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Extended Filters";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormEntityExtendedFilter_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.entityAdapterPropertyExtendedFiltersBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entityAdapterExtendedFilterBindingSource)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.entityAdapterExtendedFilterDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entityAdapterPropertyExtendedFiltersDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource entityAdapterExtendedFilterBindingSource;
        private System.Windows.Forms.BindingSource entityAdapterPropertyExtendedFiltersBindingSource;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView entityAdapterExtendedFilterDataGridView;
        private System.Windows.Forms.DataGridView entityAdapterPropertyExtendedFiltersDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}
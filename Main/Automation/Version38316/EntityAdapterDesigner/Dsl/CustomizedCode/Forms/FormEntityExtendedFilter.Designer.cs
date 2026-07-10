namespace Linx.EntityAdapterDesigner.CustomizedCode
{
    partial class FormEntityExtendedFilter
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
            System.Windows.Forms.Label displayNameLabel;
            System.Windows.Forms.Label label1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEntityExtendedFilter));
            this.displayNameTextBox = new System.Windows.Forms.TextBox();
            this.entityAdapterExtendedFilterBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.entityAdapterPropertyExtendedFiltersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.entityAdapterPropertyExtendedFiltersDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            displayNameLabel = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.entityAdapterExtendedFilterBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entityAdapterPropertyExtendedFiltersBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entityAdapterPropertyExtendedFiltersDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // displayNameLabel
            // 
            displayNameLabel.AutoSize = true;
            displayNameLabel.Location = new System.Drawing.Point(8, 16);
            displayNameLabel.Name = "displayNameLabel";
            displayNameLabel.Size = new System.Drawing.Size(75, 13);
            displayNameLabel.TabIndex = 1;
            displayNameLabel.Text = "Display Name:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(8, 47);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(37, 13);
            label1.TabIndex = 3;
            label1.Text = "Fields:";
            // 
            // displayNameTextBox
            // 
            this.displayNameTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.entityAdapterExtendedFilterBindingSource, "DisplayName", true));
            this.displayNameTextBox.Location = new System.Drawing.Point(89, 13);
            this.displayNameTextBox.Name = "displayNameTextBox";
            this.displayNameTextBox.Size = new System.Drawing.Size(310, 20);
            this.displayNameTextBox.TabIndex = 2;
            // 
            // entityAdapterExtendedFilterBindingSource
            // 
            this.entityAdapterExtendedFilterBindingSource.DataSource = typeof(Linx.EntityAdapterDesigner.EntityAdapterExtendedFilter);
            // 
            // entityAdapterPropertyExtendedFiltersBindingSource
            // 
            this.entityAdapterPropertyExtendedFiltersBindingSource.AllowNew = false;
            this.entityAdapterPropertyExtendedFiltersBindingSource.DataMember = "EntityAdapterPropertyExtendedFilters";
            this.entityAdapterPropertyExtendedFiltersBindingSource.DataSource = this.entityAdapterExtendedFilterBindingSource;
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
            this.entityAdapterPropertyExtendedFiltersDataGridView.Location = new System.Drawing.Point(11, 66);
            this.entityAdapterPropertyExtendedFiltersDataGridView.Name = "entityAdapterPropertyExtendedFiltersDataGridView";
            this.entityAdapterPropertyExtendedFiltersDataGridView.Size = new System.Drawing.Size(672, 299);
            this.entityAdapterPropertyExtendedFiltersDataGridView.TabIndex = 2;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.DataPropertyName = "IsEnabled";
            this.dataGridViewCheckBoxColumn1.HeaderText = "Is Enabled";
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
            // FormEntityExtendedFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 376);
            this.Controls.Add(label1);
            this.Controls.Add(this.entityAdapterPropertyExtendedFiltersDataGridView);
            this.Controls.Add(displayNameLabel);
            this.Controls.Add(this.displayNameTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormEntityExtendedFilter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Extended Filters";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormEntityExtendedFilter_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.entityAdapterExtendedFilterBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entityAdapterPropertyExtendedFiltersBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entityAdapterPropertyExtendedFiltersDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource entityAdapterExtendedFilterBindingSource;
        private System.Windows.Forms.TextBox displayNameTextBox;
        private System.Windows.Forms.BindingSource entityAdapterPropertyExtendedFiltersBindingSource;
        private System.Windows.Forms.DataGridView entityAdapterPropertyExtendedFiltersDataGridView;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    }
}
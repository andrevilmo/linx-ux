namespace Linx.EntityAdapterDesigner.CustomizedCode
{
    partial class FormPublishedEntityList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPublishedEntityList));
            this.btnApplyOrder = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.listEntities = new System.Windows.Forms.ListView();
            this.columnEntity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // btnApplyOrder
            // 
            this.btnApplyOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApplyOrder.Location = new System.Drawing.Point(220, 336);
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
            this.btCancel.Location = new System.Drawing.Point(316, 336);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(92, 23);
            this.btCancel.TabIndex = 24;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // listEntities
            // 
            this.listEntities.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listEntities.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnEntity});
            this.listEntities.FullRowSelect = true;
            this.listEntities.Location = new System.Drawing.Point(1, 2);
            this.listEntities.MultiSelect = false;
            this.listEntities.Name = "listEntities";
            this.listEntities.Size = new System.Drawing.Size(409, 330);
            this.listEntities.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listEntities.TabIndex = 25;
            this.listEntities.UseCompatibleStateImageBehavior = false;
            this.listEntities.View = System.Windows.Forms.View.Details;
            this.listEntities.DoubleClick += new System.EventHandler(this.listEntities_DoubleClick);
            // 
            // columnEntity
            // 
            this.columnEntity.Text = "Entity";
            this.columnEntity.Width = 400;
            // 
            // FormPublishedEntityList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 363);
            this.Controls.Add(this.listEntities);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btnApplyOrder);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPublishedEntityList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Published Entities";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnApplyOrder;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.ListView listEntities;
        private System.Windows.Forms.ColumnHeader columnEntity;

    }
}
namespace Linx.EntityAdapterDesigner.CustomCode
{
    partial class FrmEntityBuilder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEntityBuilder));
            this.treeEdmRelatedTypes = new System.Windows.Forms.TreeView();
            this.imgTree = new System.Windows.Forms.ImageList(this.components);
            this.labelEdmRelatedTypes = new System.Windows.Forms.Label();
            this.cmbEdmTypes = new System.Windows.Forms.ComboBox();
            this.labelEntities = new System.Windows.Forms.Label();
            this.labelContext = new System.Windows.Forms.Label();
            this.txEdmContext = new System.Windows.Forms.TextBox();
            this.btApply = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.labelSecondaryEntity = new System.Windows.Forms.Label();
            this.cmbSecondaryTypes = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // treeEdmRelatedTypes
            // 
            this.treeEdmRelatedTypes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeEdmRelatedTypes.CheckBoxes = true;
            this.treeEdmRelatedTypes.HideSelection = false;
            this.treeEdmRelatedTypes.ImageIndex = 0;
            this.treeEdmRelatedTypes.ImageList = this.imgTree;
            this.treeEdmRelatedTypes.Location = new System.Drawing.Point(2, 104);
            this.treeEdmRelatedTypes.Name = "treeEdmRelatedTypes";
            this.treeEdmRelatedTypes.SelectedImageIndex = 0;
            this.treeEdmRelatedTypes.Size = new System.Drawing.Size(558, 337);
            this.treeEdmRelatedTypes.TabIndex = 17;
            this.treeEdmRelatedTypes.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeEdmRelatedTypes_AfterCheck);
            // 
            // imgTree
            // 
            this.imgTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgTree.ImageStream")));
            this.imgTree.TransparentColor = System.Drawing.Color.Transparent;
            this.imgTree.Images.SetKeyName(0, "Entity.png");
            this.imgTree.Images.SetKeyName(1, "References.png");
            this.imgTree.Images.SetKeyName(2, "Relation.png");
            this.imgTree.Images.SetKeyName(3, "Property.png");
            // 
            // labelEdmRelatedTypes
            // 
            this.labelEdmRelatedTypes.AutoSize = true;
            this.labelEdmRelatedTypes.BackColor = System.Drawing.SystemColors.Control;
            this.labelEdmRelatedTypes.Location = new System.Drawing.Point(5, 86);
            this.labelEdmRelatedTypes.Name = "labelEdmRelatedTypes";
            this.labelEdmRelatedTypes.Size = new System.Drawing.Size(84, 13);
            this.labelEdmRelatedTypes.TabIndex = 16;
            this.labelEdmRelatedTypes.Text = "Related Entities:";
            // 
            // cmbEdmTypes
            // 
            this.cmbEdmTypes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbEdmTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEdmTypes.FormattingEnabled = true;
            this.cmbEdmTypes.Location = new System.Drawing.Point(102, 30);
            this.cmbEdmTypes.Name = "cmbEdmTypes";
            this.cmbEdmTypes.Size = new System.Drawing.Size(458, 21);
            this.cmbEdmTypes.Sorted = true;
            this.cmbEdmTypes.TabIndex = 18;
            this.cmbEdmTypes.SelectedIndexChanged += new System.EventHandler(this.cmbEdmTypes_SelectedIndexChanged);
            // 
            // labelEntities
            // 
            this.labelEntities.AutoSize = true;
            this.labelEntities.BackColor = System.Drawing.SystemColors.Control;
            this.labelEntities.Location = new System.Drawing.Point(5, 33);
            this.labelEntities.Name = "labelEntities";
            this.labelEntities.Size = new System.Drawing.Size(62, 13);
            this.labelEntities.TabIndex = 19;
            this.labelEntities.Text = "Main Entity:";
            // 
            // labelContext
            // 
            this.labelContext.AutoSize = true;
            this.labelContext.BackColor = System.Drawing.SystemColors.Control;
            this.labelContext.Location = new System.Drawing.Point(5, 9);
            this.labelContext.Name = "labelContext";
            this.labelContext.Size = new System.Drawing.Size(62, 13);
            this.labelContext.TabIndex = 20;
            this.labelContext.Text = "EDM Class:";
            // 
            // txEdmContext
            // 
            this.txEdmContext.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txEdmContext.Enabled = false;
            this.txEdmContext.Location = new System.Drawing.Point(102, 6);
            this.txEdmContext.Name = "txEdmContext";
            this.txEdmContext.Size = new System.Drawing.Size(458, 20);
            this.txEdmContext.TabIndex = 21;
            // 
            // btApply
            // 
            this.btApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btApply.Location = new System.Drawing.Point(485, 443);
            this.btApply.Name = "btApply";
            this.btApply.Size = new System.Drawing.Size(75, 23);
            this.btApply.TabIndex = 22;
            this.btApply.Text = "Apply";
            this.btApply.UseVisualStyleBackColor = true;
            this.btApply.Click += new System.EventHandler(this.btApply_Click);
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.Location = new System.Drawing.Point(408, 443);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 23;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // labelSecondaryEntity
            // 
            this.labelSecondaryEntity.AutoSize = true;
            this.labelSecondaryEntity.BackColor = System.Drawing.SystemColors.Control;
            this.labelSecondaryEntity.Location = new System.Drawing.Point(5, 59);
            this.labelSecondaryEntity.Name = "labelSecondaryEntity";
            this.labelSecondaryEntity.Size = new System.Drawing.Size(90, 13);
            this.labelSecondaryEntity.TabIndex = 25;
            this.labelSecondaryEntity.Text = "Secondary Entity:";
            // 
            // cmbSecondaryTypes
            // 
            this.cmbSecondaryTypes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSecondaryTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSecondaryTypes.FormattingEnabled = true;
            this.cmbSecondaryTypes.Location = new System.Drawing.Point(102, 56);
            this.cmbSecondaryTypes.Name = "cmbSecondaryTypes";
            this.cmbSecondaryTypes.Size = new System.Drawing.Size(458, 21);
            this.cmbSecondaryTypes.Sorted = true;
            this.cmbSecondaryTypes.TabIndex = 24;
            this.cmbSecondaryTypes.SelectedIndexChanged += new System.EventHandler(this.cmbSecondaryTypes_SelectedIndexChanged);
            // 
            // FrmEntityBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 468);
            this.Controls.Add(this.labelSecondaryEntity);
            this.Controls.Add(this.cmbSecondaryTypes);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btApply);
            this.Controls.Add(this.txEdmContext);
            this.Controls.Add(this.labelContext);
            this.Controls.Add(this.labelEntities);
            this.Controls.Add(this.cmbEdmTypes);
            this.Controls.Add(this.treeEdmRelatedTypes);
            this.Controls.Add(this.labelEdmRelatedTypes);
            this.MinimizeBox = false;
            this.Name = "FrmEntityBuilder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Entity Adapter Builder";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmEntityBuilder_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeEdmRelatedTypes;
        private System.Windows.Forms.Label labelEdmRelatedTypes;
        private System.Windows.Forms.ComboBox cmbEdmTypes;
        private System.Windows.Forms.Label labelEntities;
        private System.Windows.Forms.Label labelContext;
        private System.Windows.Forms.TextBox txEdmContext;
        private System.Windows.Forms.ImageList imgTree;
        private System.Windows.Forms.Button btApply;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Label labelSecondaryEntity;
        private System.Windows.Forms.ComboBox cmbSecondaryTypes;
    }
}
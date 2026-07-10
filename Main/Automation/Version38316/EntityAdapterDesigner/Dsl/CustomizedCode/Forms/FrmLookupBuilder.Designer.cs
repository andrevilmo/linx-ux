namespace Linx.EntityAdapterDesigner.CustomCode
{
    partial class FrmLookUpBuilder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLookUpBuilder));
            this.treeEdmRelatedTypes = new System.Windows.Forms.TreeView();
            this.imgTree = new System.Windows.Forms.ImageList(this.components);
            this.cmbEdmTypes = new System.Windows.Forms.ComboBox();
            this.labelEntities = new System.Windows.Forms.Label();
            this.btApply = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
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
            this.treeEdmRelatedTypes.Location = new System.Drawing.Point(2, 30);
            this.treeEdmRelatedTypes.Name = "treeEdmRelatedTypes";
            this.treeEdmRelatedTypes.SelectedImageIndex = 0;
            this.treeEdmRelatedTypes.Size = new System.Drawing.Size(558, 464);
            this.treeEdmRelatedTypes.TabIndex = 17;
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
            // cmbEdmTypes
            // 
            this.cmbEdmTypes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbEdmTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEdmTypes.FormattingEnabled = true;
            this.cmbEdmTypes.Location = new System.Drawing.Point(89, 3);
            this.cmbEdmTypes.Name = "cmbEdmTypes";
            this.cmbEdmTypes.Size = new System.Drawing.Size(471, 21);
            this.cmbEdmTypes.Sorted = true;
            this.cmbEdmTypes.TabIndex = 18;
            this.cmbEdmTypes.SelectedIndexChanged += new System.EventHandler(this.cmbEdmTypes_SelectedIndexChanged);
            // 
            // labelEntities
            // 
            this.labelEntities.AutoSize = true;
            this.labelEntities.BackColor = System.Drawing.SystemColors.Control;
            this.labelEntities.Location = new System.Drawing.Point(6, 7);
            this.labelEntities.Name = "labelEntities";
            this.labelEntities.Size = new System.Drawing.Size(77, 13);
            this.labelEntities.TabIndex = 19;
            this.labelEntities.Text = "LookUp Entity:";
            // 
            // btApply
            // 
            this.btApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btApply.Location = new System.Drawing.Point(406, 496);
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
            this.btCancel.Location = new System.Drawing.Point(485, 496);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 23;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // FrmLookUpBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 521);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btApply);
            this.Controls.Add(this.labelEntities);
            this.Controls.Add(this.cmbEdmTypes);
            this.Controls.Add(this.treeEdmRelatedTypes);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "FrmLookUpBuilder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LookUp Builder";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmLookUpBuilder_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeEdmRelatedTypes;
        private System.Windows.Forms.ComboBox cmbEdmTypes;
        private System.Windows.Forms.Label labelEntities;
        private System.Windows.Forms.ImageList imgTree;
        private System.Windows.Forms.Button btApply;
        private System.Windows.Forms.Button btCancel;
    }
}
namespace Linx.EntityAdapterDesigner.CustomCode
{
    partial class FrmOlapBuilder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOlapBuilder));
            this.treeOlapItemTypes = new System.Windows.Forms.TreeView();
            this.imgTree = new System.Windows.Forms.ImageList(this.components);
            this.labelEdmRelatedTypes = new System.Windows.Forms.Label();
            this.cmbTypes = new System.Windows.Forms.ComboBox();
            this.labelEntities = new System.Windows.Forms.Label();
            this.labelContext = new System.Windows.Forms.Label();
            this.txOlapContext = new System.Windows.Forms.TextBox();
            this.btApply = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // treeOlapItemTypes
            // 
            this.treeOlapItemTypes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeOlapItemTypes.CheckBoxes = true;
            this.treeOlapItemTypes.HideSelection = false;
            this.treeOlapItemTypes.ImageIndex = 0;
            this.treeOlapItemTypes.ImageList = this.imgTree;
            this.treeOlapItemTypes.Location = new System.Drawing.Point(2, 79);
            this.treeOlapItemTypes.Name = "treeOlapItemTypes";
            this.treeOlapItemTypes.SelectedImageIndex = 0;
            this.treeOlapItemTypes.Size = new System.Drawing.Size(558, 417);
            this.treeOlapItemTypes.TabIndex = 17;
            // 
            // imgTree
            // 
            this.imgTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgTree.ImageStream")));
            this.imgTree.TransparentColor = System.Drawing.Color.Transparent;
            this.imgTree.Images.SetKeyName(0, "Cube");
            this.imgTree.Images.SetKeyName(1, "Measure");
            this.imgTree.Images.SetKeyName(2, "Dimension");
            this.imgTree.Images.SetKeyName(3, "Folder");
            this.imgTree.Images.SetKeyName(4, "Formula");
            this.imgTree.Images.SetKeyName(5, "Kpi");
            this.imgTree.Images.SetKeyName(6, "Hierarchy");
            this.imgTree.Images.SetKeyName(7, "DimensionProperty");
            // 
            // labelEdmRelatedTypes
            // 
            this.labelEdmRelatedTypes.AutoSize = true;
            this.labelEdmRelatedTypes.BackColor = System.Drawing.SystemColors.Control;
            this.labelEdmRelatedTypes.Location = new System.Drawing.Point(5, 63);
            this.labelEdmRelatedTypes.Name = "labelEdmRelatedTypes";
            this.labelEdmRelatedTypes.Size = new System.Drawing.Size(64, 13);
            this.labelEdmRelatedTypes.TabIndex = 16;
            this.labelEdmRelatedTypes.Text = "Dimensions:";
            // 
            // cmbTypes
            // 
            this.cmbTypes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTypes.FormattingEnabled = true;
            this.cmbTypes.Location = new System.Drawing.Point(102, 30);
            this.cmbTypes.Name = "cmbTypes";
            this.cmbTypes.Size = new System.Drawing.Size(458, 21);
            this.cmbTypes.Sorted = true;
            this.cmbTypes.TabIndex = 18;
            this.cmbTypes.SelectedIndexChanged += new System.EventHandler(this.cmbTypes_SelectedIndexChanged);
            // 
            // labelEntities
            // 
            this.labelEntities.AutoSize = true;
            this.labelEntities.BackColor = System.Drawing.SystemColors.Control;
            this.labelEntities.Location = new System.Drawing.Point(5, 33);
            this.labelEntities.Name = "labelEntities";
            this.labelEntities.Size = new System.Drawing.Size(61, 13);
            this.labelEntities.TabIndex = 19;
            this.labelEntities.Text = "Main Cube:";
            // 
            // labelContext
            // 
            this.labelContext.AutoSize = true;
            this.labelContext.BackColor = System.Drawing.SystemColors.Control;
            this.labelContext.Location = new System.Drawing.Point(5, 9);
            this.labelContext.Name = "labelContext";
            this.labelContext.Size = new System.Drawing.Size(95, 13);
            this.labelContext.TabIndex = 20;
            this.labelContext.Text = "OLAP Connection:";
            // 
            // txOlapContext
            // 
            this.txOlapContext.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txOlapContext.Enabled = false;
            this.txOlapContext.Location = new System.Drawing.Point(102, 6);
            this.txOlapContext.Name = "txOlapContext";
            this.txOlapContext.Size = new System.Drawing.Size(458, 20);
            this.txOlapContext.TabIndex = 21;
            // 
            // btApply
            // 
            this.btApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btApply.Location = new System.Drawing.Point(407, 496);
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
            // FrmOlapBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 521);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btApply);
            this.Controls.Add(this.txOlapContext);
            this.Controls.Add(this.labelContext);
            this.Controls.Add(this.labelEntities);
            this.Controls.Add(this.cmbTypes);
            this.Controls.Add(this.treeOlapItemTypes);
            this.Controls.Add(this.labelEdmRelatedTypes);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "FrmOlapBuilder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Entity Adapter Builder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

     

        #endregion

        private System.Windows.Forms.TreeView treeOlapItemTypes;
        private System.Windows.Forms.Label labelEdmRelatedTypes;
        private System.Windows.Forms.ComboBox cmbTypes;
        private System.Windows.Forms.Label labelEntities;
        private System.Windows.Forms.Label labelContext;
        private System.Windows.Forms.TextBox txOlapContext;
        private System.Windows.Forms.ImageList imgTree;
        private System.Windows.Forms.Button btApply;
        private System.Windows.Forms.Button btCancel;
    }
}
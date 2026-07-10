using Linx.Builder.Resources;
namespace Linx.EntityAdapterDesigner.CustomCode
{
    partial class FrmAttributesOrderBuilder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAttributesOrderBuilder));
            this.btCancel = new System.Windows.Forms.Button();
            this.btApply = new System.Windows.Forms.Button();
            this.smallImageList = new System.Windows.Forms.ImageList(this.components);
            this.popUpMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.linkToNewGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lstOrder = new Linx.Builder.Resources.DragAndDropListView();
            this.AttributeName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.popUpMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.Location = new System.Drawing.Point(297, 358);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(84, 23);
            this.btCancel.TabIndex = 2;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btApply
            // 
            this.btApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btApply.Location = new System.Drawing.Point(210, 358);
            this.btApply.Name = "btApply";
            this.btApply.Size = new System.Drawing.Size(84, 23);
            this.btApply.TabIndex = 3;
            this.btApply.Text = "Apply";
            this.btApply.UseVisualStyleBackColor = true;
            this.btApply.Click += new System.EventHandler(this.btApply_Click);
            // 
            // smallImageList
            // 
            this.smallImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("smallImageList.ImageStream")));
            this.smallImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.smallImageList.Images.SetKeyName(0, "Property.png");
            this.smallImageList.Images.SetKeyName(1, "Formula.bmp");
            // 
            // popUpMenuStrip
            // 
            this.popUpMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.linkToNewGroupToolStripMenuItem,
            this.alterToolStripMenuItem});
            this.popUpMenuStrip.Name = "popUpMenuStrip";
            this.popUpMenuStrip.Size = new System.Drawing.Size(223, 48);
            // 
            // linkToNewGroupToolStripMenuItem
            // 
            this.linkToNewGroupToolStripMenuItem.Name = "linkToNewGroupToolStripMenuItem";
            this.linkToNewGroupToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.linkToNewGroupToolStripMenuItem.Text = "Link to New Group...";
            this.linkToNewGroupToolStripMenuItem.Click += new System.EventHandler(this.linkToNewGroupToolStripMenuItem_Click);
            // 
            // alterToolStripMenuItem
            // 
            this.alterToolStripMenuItem.Name = "alterToolStripMenuItem";
            this.alterToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.alterToolStripMenuItem.Text = "Alter Current Group Name...";
            this.alterToolStripMenuItem.Click += new System.EventHandler(this.alterToolStripMenuItem_Click);
            // 
            // lstOrder
            // 
            this.lstOrder.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lstOrder.AllowColumnReorder = true;
            this.lstOrder.AllowDrop = true;
            this.lstOrder.AllowRowReorder = true;
            this.lstOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstOrder.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstOrder.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.AttributeName});
            this.lstOrder.ContextMenuStrip = this.popUpMenuStrip;
            this.lstOrder.FullRowSelect = true;
            this.lstOrder.GridLines = true;
            this.lstOrder.Location = new System.Drawing.Point(0, 4);
            this.lstOrder.Name = "lstOrder";
            this.lstOrder.Size = new System.Drawing.Size(383, 350);
            this.lstOrder.SmallImageList = this.smallImageList;
            this.lstOrder.StateImageList = this.smallImageList;
            this.lstOrder.TabIndex = 0;
            this.lstOrder.UseCompatibleStateImageBehavior = false;
            this.lstOrder.View = System.Windows.Forms.View.Details;
            // 
            // AttributeName
            // 
            this.AttributeName.Text = "Attribute";
            this.AttributeName.Width = 343;
            // 
            // FrmAttributesOrderBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 382);
            this.Controls.Add(this.lstOrder);
            this.Controls.Add(this.btApply);
            this.Controls.Add(this.btCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "FrmAttributesOrderBuilder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Group and Reorder";
            this.Load += new System.EventHandler(this.FrmAttributesOrderBuilder_Load);
            this.popUpMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btApply;
        private DragAndDropListView lstOrder;
        private System.Windows.Forms.ImageList smallImageList;
        private System.Windows.Forms.ColumnHeader AttributeName;
        private System.Windows.Forms.ContextMenuStrip popUpMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem linkToNewGroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alterToolStripMenuItem;
    }
}
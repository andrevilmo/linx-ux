namespace Linx.EntityAdapterDesigner.CustomCode
{
    partial class FrmAttributeGroup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAttributeGroup));
            this.txGroupCode = new System.Windows.Forms.TextBox();
            this.frmAttributeGroupBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.txGroupName = new System.Windows.Forms.TextBox();
            this.labelContext = new System.Windows.Forms.Label();
            this.btCancel = new System.Windows.Forms.Button();
            this.btApply = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.frmAttributeGroupBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // txGroupCode
            // 
            this.txGroupCode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.frmAttributeGroupBindingSource, "GroupCode", true));
            this.txGroupCode.Location = new System.Drawing.Point(76, 16);
            this.txGroupCode.Name = "txGroupCode";
            this.txGroupCode.ReadOnly = true;
            this.txGroupCode.Size = new System.Drawing.Size(67, 20);
            this.txGroupCode.TabIndex = 0;
            this.txGroupCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // frmAttributeGroupBindingSource
            // 
            this.frmAttributeGroupBindingSource.DataSource = typeof(Linx.EntityAdapterDesigner.CustomCode.FrmAttributeGroup);
            // 
            // txGroupName
            // 
            this.txGroupName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txGroupName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.frmAttributeGroupBindingSource, "GroupName", true));
            this.txGroupName.Location = new System.Drawing.Point(149, 16);
            this.txGroupName.Name = "txGroupName";
            this.txGroupName.Size = new System.Drawing.Size(297, 20);
            this.txGroupName.TabIndex = 0;
            // 
            // labelContext
            // 
            this.labelContext.AutoSize = true;
            this.labelContext.BackColor = System.Drawing.SystemColors.Control;
            this.labelContext.Location = new System.Drawing.Point(8, 19);
            this.labelContext.Name = "labelContext";
            this.labelContext.Size = new System.Drawing.Size(39, 13);
            this.labelContext.TabIndex = 21;
            this.labelContext.Text = "Group:";
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.Location = new System.Drawing.Point(362, 42);
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
            this.btApply.Location = new System.Drawing.Point(275, 42);
            this.btApply.Name = "btApply";
            this.btApply.Size = new System.Drawing.Size(84, 23);
            this.btApply.TabIndex = 1;
            this.btApply.Text = "Save";
            this.btApply.UseVisualStyleBackColor = true;
            this.btApply.Click += new System.EventHandler(this.btApply_Click);
            // 
            // FrmAttributeGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 75);
            this.ControlBox = false;
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btApply);
            this.Controls.Add(this.labelContext);
            this.Controls.Add(this.txGroupName);
            this.Controls.Add(this.txGroupCode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmAttributeGroup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Group Definition";
            this.Load += new System.EventHandler(this.FrmAttributeGroup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.frmAttributeGroupBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txGroupCode;
        private System.Windows.Forms.TextBox txGroupName;
        private System.Windows.Forms.Label labelContext;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btApply;
		private System.Windows.Forms.BindingSource frmAttributeGroupBindingSource;
    }
}
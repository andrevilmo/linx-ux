namespace Linx.EntityAdapterDesigner.CustomCode
{
    partial class FrmReportDetailsSelector
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReportDetailsSelector));
            this.btCreate = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.lstDetails = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btCreate
            // 
            this.btCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCreate.Location = new System.Drawing.Point(133, 299);
            this.btCreate.Name = "btCreate";
            this.btCreate.Size = new System.Drawing.Size(89, 23);
            this.btCreate.TabIndex = 22;
            this.btCreate.Text = "Create Report";
            this.btCreate.UseVisualStyleBackColor = true;
            this.btCreate.Click += new System.EventHandler(this.btCreateEvents_Click);
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.Location = new System.Drawing.Point(225, 299);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(88, 23);
            this.btCancel.TabIndex = 23;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // lstDetails
            // 
            this.lstDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstDetails.FormattingEnabled = true;
            this.lstDetails.Location = new System.Drawing.Point(4, 3);
            this.lstDetails.Name = "lstDetails";
            this.lstDetails.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstDetails.Size = new System.Drawing.Size(310, 290);
            this.lstDetails.Sorted = true;
            this.lstDetails.TabIndex = 24;
            this.lstDetails.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstEvents_MouseDoubleClick);
            // 
            // FrmReportDetailsSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 327);
            this.Controls.Add(this.lstDetails);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btCreate);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmReportDetailsSelector";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Details";
            this.Load += new System.EventHandler(this.FrmReportDetailsSelector_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btCreate;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.ListBox lstDetails;
    }
}
namespace Linx.EntityAdapterDesigner.CustomCode
{
    partial class FrmAddClientService
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAddClientService));
            this.btSelectEvents = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.lstServices = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btSelectEvents
            // 
            this.btSelectEvents.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSelectEvents.Location = new System.Drawing.Point(133, 347);
            this.btSelectEvents.Name = "btSelectEvents";
            this.btSelectEvents.Size = new System.Drawing.Size(75, 23);
            this.btSelectEvents.TabIndex = 22;
            this.btSelectEvents.Text = "Select";
            this.btSelectEvents.UseVisualStyleBackColor = true;
            this.btSelectEvents.Click += new System.EventHandler(this.btSelectService_Click);
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.Location = new System.Drawing.Point(210, 347);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 23;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // lstServices
            // 
            this.lstServices.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstServices.FormattingEnabled = true;
            this.lstServices.Location = new System.Drawing.Point(4, 3);
            this.lstServices.Name = "lstServices";
            this.lstServices.Size = new System.Drawing.Size(281, 342);
            this.lstServices.Sorted = true;
            this.lstServices.TabIndex = 24;
            this.lstServices.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstEvents_MouseDoubleClick);
            // 
            // FrmAddClientService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 372);
            this.Controls.Add(this.lstServices);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btSelectEvents);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "FrmAddClientService";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Service";
            this.Load += new System.EventHandler(this.FrmAddClientService_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btSelectEvents;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.ListBox lstServices;
    }
}
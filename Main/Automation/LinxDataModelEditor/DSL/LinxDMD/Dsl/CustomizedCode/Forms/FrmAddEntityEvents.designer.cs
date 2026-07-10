namespace Linx.BusinessDataModelDesigner.CustomCode
{
    partial class FrmAddEntityEvents
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAddEntityEvents));
            this.btAddEvents = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.lstEvents = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btAddEvents
            // 
            this.btAddEvents.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btAddEvents.Location = new System.Drawing.Point(133, 347);
            this.btAddEvents.Name = "btAddEvents";
            this.btAddEvents.Size = new System.Drawing.Size(75, 23);
            this.btAddEvents.TabIndex = 22;
            this.btAddEvents.Text = "Add";
            this.btAddEvents.UseVisualStyleBackColor = true;
            this.btAddEvents.Click += new System.EventHandler(this.btAddEvents_Click);
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
            // lstEvents
            // 
            this.lstEvents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstEvents.FormattingEnabled = true;
            this.lstEvents.Location = new System.Drawing.Point(4, 3);
            this.lstEvents.Name = "lstEvents";
            this.lstEvents.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstEvents.Size = new System.Drawing.Size(281, 342);
            this.lstEvents.Sorted = true;
            this.lstEvents.TabIndex = 24;
            this.lstEvents.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstEvents_MouseDoubleClick);
            // 
            // FrmAddEntityEvents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 372);
            this.Controls.Add(this.lstEvents);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btAddEvents);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "FrmAddEntityEvents";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Events";
            this.Load += new System.EventHandler(this.FrmAddEntityEvents_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btAddEvents;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.ListBox lstEvents;
    }
}
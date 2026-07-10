namespace Linx.EntityAdapterDesigner.CustomizedCode.UserControls
{
    partial class FontControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpFont = new System.Windows.Forms.GroupBox();
            this.Bold = new System.Windows.Forms.CheckBox();
            this.Highlight = new System.Windows.Forms.CheckBox();
            this.Style = new System.Windows.Forms.ComboBox();
            this.lblStyle = new System.Windows.Forms.Label();
            this.grpFont.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpFont
            // 
            this.grpFont.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.grpFont.Controls.Add(this.Bold);
            this.grpFont.Controls.Add(this.Highlight);
            this.grpFont.Controls.Add(this.Style);
            this.grpFont.Controls.Add(this.lblStyle);
            this.grpFont.Location = new System.Drawing.Point(4, 0);
            this.grpFont.Name = "grpFont";
            this.grpFont.Size = new System.Drawing.Size(374, 47);
            this.grpFont.TabIndex = 36;
            this.grpFont.TabStop = false;
            this.grpFont.Text = "Font";
            // 
            // Bold
            // 
            this.Bold.AutoSize = true;
            this.Bold.Location = new System.Drawing.Point(321, 17);
            this.Bold.Name = "Bold";
            this.Bold.Size = new System.Drawing.Size(47, 17);
            this.Bold.TabIndex = 10;
            this.Bold.Text = "Bold";
            this.Bold.UseVisualStyleBackColor = true;
            this.Bold.CheckedChanged += new System.EventHandler(this.bold_CheckedChanged);
            // 
            // Highlight
            // 
            this.Highlight.AutoSize = true;
            this.Highlight.Location = new System.Drawing.Point(235, 17);
            this.Highlight.Name = "Highlight";
            this.Highlight.Size = new System.Drawing.Size(67, 17);
            this.Highlight.TabIndex = 9;
            this.Highlight.Text = "Highlight";
            this.Highlight.UseVisualStyleBackColor = true;
            this.Highlight.CheckedChanged += new System.EventHandler(this.Highlight_CheckedChanged);
            // 
            // Style
            // 
            this.Style.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Style.FormattingEnabled = true;
            this.Style.Location = new System.Drawing.Point(46, 15);
            this.Style.Name = "Style";
            this.Style.Size = new System.Drawing.Size(169, 21);
            this.Style.TabIndex = 5;
            this.Style.SelectedIndexChanged += new System.EventHandler(this.Style_SelectedIndexChanged);
            // 
            // lblStyle
            // 
            this.lblStyle.AutoSize = true;
            this.lblStyle.Location = new System.Drawing.Point(10, 18);
            this.lblStyle.Name = "lblStyle";
            this.lblStyle.Size = new System.Drawing.Size(30, 13);
            this.lblStyle.TabIndex = 4;
            this.lblStyle.Text = "Style";
            // 
            // FontControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.grpFont);
            this.Name = "FontControl";
            this.Size = new System.Drawing.Size(380, 52);
            this.grpFont.ResumeLayout(false);
            this.grpFont.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpFont;
        private System.Windows.Forms.Label lblStyle;
        public System.Windows.Forms.ComboBox Style;
        public System.Windows.Forms.CheckBox Highlight;
        public System.Windows.Forms.CheckBox Bold;
    }
}

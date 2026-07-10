namespace Linx.Dsl.Components
{
    partial class TelerikUIGauge
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
            this.tabChartInfo = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblExemploLabel = new System.Windows.Forms.Label();
            this.lblFormatLabel = new System.Windows.Forms.Label();
            this.txtLabelFormat = new System.Windows.Forms.TextBox();
            this.cmbPosition = new System.Windows.Forms.ComboBox();
            this.lblPositionLegend = new System.Windows.Forms.Label();
            this.cmbGaugeType = new System.Windows.Forms.ComboBox();
            this.lblTypeGauge = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabChartInfo.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabChartInfo
            // 
            this.tabChartInfo.Controls.Add(this.tabPage1);
            this.tabChartInfo.Location = new System.Drawing.Point(-4, -2);
            this.tabChartInfo.Name = "tabChartInfo";
            this.tabChartInfo.SelectedIndex = 0;
            this.tabChartInfo.Size = new System.Drawing.Size(484, 281);
            this.tabChartInfo.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lblExemploLabel);
            this.tabPage1.Controls.Add(this.lblFormatLabel);
            this.tabPage1.Controls.Add(this.txtLabelFormat);
            this.tabPage1.Controls.Add(this.cmbPosition);
            this.tabPage1.Controls.Add(this.lblPositionLegend);
            this.tabPage1.Controls.Add(this.cmbGaugeType);
            this.tabPage1.Controls.Add(this.lblTypeGauge);
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(476, 255);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Gauge Info";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblExemploLabel
            // 
            this.lblExemploLabel.AutoSize = true;
            this.lblExemploLabel.ForeColor = System.Drawing.Color.SeaGreen;
            this.lblExemploLabel.Location = new System.Drawing.Point(110, 101);
            this.lblExemploLabel.Name = "lblExemploLabel";
            this.lblExemploLabel.Size = new System.Drawing.Size(90, 13);
            this.lblExemploLabel.TabIndex = 81;
            this.lblExemploLabel.Text = "Ex.: \"C2; N1; P2\"";
            // 
            // lblFormatLabel
            // 
            this.lblFormatLabel.AutoSize = true;
            this.lblFormatLabel.Location = new System.Drawing.Point(34, 81);
            this.lblFormatLabel.Name = "lblFormatLabel";
            this.lblFormatLabel.Size = new System.Drawing.Size(65, 13);
            this.lblFormatLabel.TabIndex = 80;
            this.lblFormatLabel.Text = "Label format";
            // 
            // txtLabelFormat
            // 
            this.txtLabelFormat.Location = new System.Drawing.Point(110, 78);
            this.txtLabelFormat.Name = "txtLabelFormat";
            this.txtLabelFormat.Size = new System.Drawing.Size(121, 20);
            this.txtLabelFormat.TabIndex = 79;
            this.txtLabelFormat.TextChanged += new System.EventHandler(this.txtLabelFormat_TextChanged);
            // 
            // cmbPosition
            // 
            this.cmbPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPosition.FormattingEnabled = true;
            this.cmbPosition.Location = new System.Drawing.Point(110, 47);
            this.cmbPosition.Name = "cmbPosition";
            this.cmbPosition.Size = new System.Drawing.Size(121, 21);
            this.cmbPosition.TabIndex = 68;
            this.cmbPosition.SelectedIndexChanged += new System.EventHandler(this.cmbPosition_SelectedIndexChanged);
            // 
            // lblPositionLegend
            // 
            this.lblPositionLegend.AutoSize = true;
            this.lblPositionLegend.Location = new System.Drawing.Point(55, 50);
            this.lblPositionLegend.Name = "lblPositionLegend";
            this.lblPositionLegend.Size = new System.Drawing.Size(44, 13);
            this.lblPositionLegend.TabIndex = 67;
            this.lblPositionLegend.Text = "Position";
            // 
            // cmbGaugeType
            // 
            this.cmbGaugeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGaugeType.FormattingEnabled = true;
            this.cmbGaugeType.Location = new System.Drawing.Point(110, 15);
            this.cmbGaugeType.Name = "cmbGaugeType";
            this.cmbGaugeType.Size = new System.Drawing.Size(313, 21);
            this.cmbGaugeType.TabIndex = 0;
            this.cmbGaugeType.SelectedIndexChanged += new System.EventHandler(this.cmbGaugeType_SelectedIndexChanged);
            // 
            // lblTypeGauge
            // 
            this.lblTypeGauge.AutoSize = true;
            this.lblTypeGauge.Location = new System.Drawing.Point(68, 18);
            this.lblTypeGauge.Name = "lblTypeGauge";
            this.lblTypeGauge.Size = new System.Drawing.Size(31, 13);
            this.lblTypeGauge.TabIndex = 52;
            this.lblTypeGauge.Text = "Type";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(8, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(459, 243);
            this.pictureBox1.TabIndex = 66;
            this.pictureBox1.TabStop = false;
            // 
            // TelerikUIGauge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabChartInfo);
            this.Name = "TelerikUIGauge";
            this.Size = new System.Drawing.Size(485, 290);
            this.tabChartInfo.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabChartInfo;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ComboBox cmbGaugeType;
        private System.Windows.Forms.Label lblTypeGauge;
        private System.Windows.Forms.ComboBox cmbPosition;
        private System.Windows.Forms.Label lblPositionLegend;
        private System.Windows.Forms.Label lblExemploLabel;
        private System.Windows.Forms.Label lblFormatLabel;
        private System.Windows.Forms.TextBox txtLabelFormat;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

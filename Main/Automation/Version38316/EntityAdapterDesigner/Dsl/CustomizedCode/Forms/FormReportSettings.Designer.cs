namespace Linx.EntityAdapterDesigner.CustomizedCode
{
    partial class FormReportSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormReportSettings));
            this.labelTitle = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.textTitle = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.treeEntityRelatedTypes = new System.Windows.Forms.TreeView();
            this.labelPropSelector = new System.Windows.Forms.Label();
            this.ckGenerateCrossTabReport = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.Location = new System.Drawing.Point(13, 11);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(36, 13);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Title:";
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(424, 502);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(92, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // textTitle
            // 
            this.textTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textTitle.Location = new System.Drawing.Point(15, 26);
            this.textTitle.Name = "textTitle";
            this.textTitle.Size = new System.Drawing.Size(598, 20);
            this.textTitle.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(521, 502);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(92, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // treeEntityRelatedTypes
            // 
            this.treeEntityRelatedTypes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeEntityRelatedTypes.CheckBoxes = true;
            this.treeEntityRelatedTypes.HideSelection = false;
            this.treeEntityRelatedTypes.Location = new System.Drawing.Point(16, 77);
            this.treeEntityRelatedTypes.Name = "treeEntityRelatedTypes";
            this.treeEntityRelatedTypes.Size = new System.Drawing.Size(597, 421);
            this.treeEntityRelatedTypes.TabIndex = 18;
            // 
            // labelPropSelector
            // 
            this.labelPropSelector.AutoSize = true;
            this.labelPropSelector.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPropSelector.Location = new System.Drawing.Point(13, 59);
            this.labelPropSelector.Name = "labelPropSelector";
            this.labelPropSelector.Size = new System.Drawing.Size(119, 13);
            this.labelPropSelector.TabIndex = 19;
            this.labelPropSelector.Text = "Properties Selector:";
            // 
            // ckGenerateCrossTabReport
            // 
            this.ckGenerateCrossTabReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ckGenerateCrossTabReport.AutoSize = true;
            this.ckGenerateCrossTabReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckGenerateCrossTabReport.Location = new System.Drawing.Point(441, 59);
            this.ckGenerateCrossTabReport.Name = "ckGenerateCrossTabReport";
            this.ckGenerateCrossTabReport.Size = new System.Drawing.Size(173, 17);
            this.ckGenerateCrossTabReport.TabIndex = 20;
            this.ckGenerateCrossTabReport.Text = "Generate Crosstab Report";
            this.ckGenerateCrossTabReport.UseVisualStyleBackColor = true;
            this.ckGenerateCrossTabReport.CheckedChanged += new System.EventHandler(this.ckGenerateCrossTabReport_CheckedChanged);
            // 
            // FormReportSettings
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(625, 527);
            this.Controls.Add(this.ckGenerateCrossTabReport);
            this.Controls.Add(this.labelPropSelector);
            this.Controls.Add(this.treeEntityRelatedTypes);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.textTitle);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.labelTitle);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "FormReportSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report Settings";
            this.Load += new System.EventHandler(this.FormReportSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox textTitle;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TreeView treeEntityRelatedTypes;
        private System.Windows.Forms.Label labelPropSelector;
        private System.Windows.Forms.CheckBox ckGenerateCrossTabReport;

    }
}
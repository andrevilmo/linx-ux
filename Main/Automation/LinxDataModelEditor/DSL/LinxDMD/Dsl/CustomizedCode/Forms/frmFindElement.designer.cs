namespace Linx.BusinessDataModelDesigner.CustomCode
{
    partial class frmFindElement
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
        {            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.listClasses = new System.Windows.Forms.ListView();
            this.columnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnReference = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnModel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ckBrokenLink = new System.Windows.Forms.RadioButton();
            this.ckOriginClasses = new System.Windows.Forms.RadioButton();
            this.ckDomains = new System.Windows.Forms.RadioButton();
            this.progressModels = new System.Windows.Forms.ProgressBar();
            this.ckReferenceClasses = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(6, 25);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(41, 13);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Search";
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Location = new System.Drawing.Point(57, 22);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(742, 20);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyUp);
            // 
            // listClasses
            // 
            this.listClasses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listClasses.BackColor = System.Drawing.SystemColors.Info;
            this.listClasses.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnName,
            this.columnReference,
            this.columnModel,
            this.columnPath});
            this.listClasses.HideSelection = false;
            this.listClasses.Location = new System.Drawing.Point(9, 45);
            this.listClasses.MultiSelect = false;
            this.listClasses.Name = "listClasses";
            this.listClasses.Size = new System.Drawing.Size(790, 386);
            this.listClasses.TabIndex = 28;
            this.listClasses.UseCompatibleStateImageBehavior = false;
            this.listClasses.View = System.Windows.Forms.View.Details;
            this.listClasses.DoubleClick += new System.EventHandler(this.listClasses_DoubleClick);
            // 
            // columnName
            // 
            this.columnName.Text = "Name";
            this.columnName.Width = 242;
            // 
            // columnReference
            // 
            this.columnReference.Text = "Reference?";
            this.columnReference.Width = 80;
            // 
            // columnModel
            // 
            this.columnModel.Text = "Model";
            this.columnModel.Width = 222;
            // 
            // columnPath
            // 
            this.columnPath.Text = "Path";
            this.columnPath.Width = 300;
            // 
            // ckBrokenLink
            // 
            this.ckBrokenLink.AutoSize = true;
            this.ckBrokenLink.Location = new System.Drawing.Point(352, 2);
            this.ckBrokenLink.Name = "ckBrokenLink";
            this.ckBrokenLink.Size = new System.Drawing.Size(112, 17);
            this.ckBrokenLink.TabIndex = 30;
            this.ckBrokenLink.Text = "Classes with errors";
            this.ckBrokenLink.UseVisualStyleBackColor = true;
            this.ckBrokenLink.CheckedChanged += new System.EventHandler(this.ckCheckedChanged);
            // 
            // ckOriginClasses
            // 
            this.ckOriginClasses.AutoSize = true;
            this.ckOriginClasses.Checked = true;
            this.ckOriginClasses.Location = new System.Drawing.Point(57, 2);
            this.ckOriginClasses.Name = "ckOriginClasses";
            this.ckOriginClasses.Size = new System.Drawing.Size(91, 17);
            this.ckOriginClasses.TabIndex = 31;
            this.ckOriginClasses.TabStop = true;
            this.ckOriginClasses.Text = "Origin Classes";
            this.ckOriginClasses.UseVisualStyleBackColor = true;
            this.ckOriginClasses.CheckedChanged += new System.EventHandler(this.ckCheckedChanged);
            // 
            // ckDomains
            // 
            this.ckDomains.AutoSize = true;
            this.ckDomains.Location = new System.Drawing.Point(280, 2);
            this.ckDomains.Name = "ckDomains";
            this.ckDomains.Size = new System.Drawing.Size(66, 17);
            this.ckDomains.TabIndex = 32;
            this.ckDomains.Text = "Domains";
            this.ckDomains.UseVisualStyleBackColor = true;
            this.ckDomains.CheckedChanged += new System.EventHandler(this.ckCheckedChanged);
            // 
            // progressModels
            // 
            this.progressModels.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.progressModels.Location = new System.Drawing.Point(666, 2);
            this.progressModels.Name = "progressModels";
            this.progressModels.Size = new System.Drawing.Size(133, 18);
            this.progressModels.TabIndex = 33;
            // 
            // ckReferenceClasses
            // 
            this.ckReferenceClasses.AutoSize = true;
            this.ckReferenceClasses.Location = new System.Drawing.Point(154, 2);
            this.ckReferenceClasses.Name = "ckReferenceClasses";
            this.ckReferenceClasses.Size = new System.Drawing.Size(114, 17);
            this.ckReferenceClasses.TabIndex = 34;
            this.ckReferenceClasses.Text = "Reference Classes";
            this.ckReferenceClasses.UseVisualStyleBackColor = true;
            // 
            // frmFindElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 440);
            this.Controls.Add(this.ckReferenceClasses);
            this.Controls.Add(this.progressModels);
            this.Controls.Add(this.ckDomains);
            this.Controls.Add(this.ckOriginClasses);
            this.Controls.Add(this.ckBrokenLink);
            this.Controls.Add(this.listClasses);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblSearch);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(270, 250);
            this.Name = "frmFindElement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Find Element";
            this.Activated += new System.EventHandler(this.frmFindElement_Activated);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ListView listClasses;
        private System.Windows.Forms.ColumnHeader columnName;
        private System.Windows.Forms.ColumnHeader columnReference;
        private System.Windows.Forms.ColumnHeader columnModel;
        private System.Windows.Forms.ColumnHeader columnPath;
        private System.Windows.Forms.RadioButton ckBrokenLink;
        private System.Windows.Forms.RadioButton ckOriginClasses;
        private System.Windows.Forms.RadioButton ckDomains;
        private System.Windows.Forms.ProgressBar progressModels;
        private System.Windows.Forms.RadioButton ckReferenceClasses;

       
    }
}
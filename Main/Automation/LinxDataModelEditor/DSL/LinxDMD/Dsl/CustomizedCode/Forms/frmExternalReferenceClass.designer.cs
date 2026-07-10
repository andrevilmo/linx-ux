namespace Linx.BusinessDataModelDesigner.CustomCode
{
    partial class frmExternalReferenceClass
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
            this.comboProjectItems = new System.Windows.Forms.ComboBox();
            this.labelProjectItems = new System.Windows.Forms.Label();
            this.btApply = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.listClasses = new System.Windows.Forms.ListView();
            this.columnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnReference = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // comboProjectItems
            // 
            this.comboProjectItems.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboProjectItems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboProjectItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboProjectItems.FormattingEnabled = true;
            this.comboProjectItems.Location = new System.Drawing.Point(109, 9);
            this.comboProjectItems.Name = "comboProjectItems";
            this.comboProjectItems.Size = new System.Drawing.Size(599, 24);
            this.comboProjectItems.TabIndex = 0;
            this.comboProjectItems.SelectedIndexChanged += new System.EventHandler(this.comboProjectItems_SelectedIndexChanged);
            // 
            // labelProjectItems
            // 
            this.labelProjectItems.AutoSize = true;
            this.labelProjectItems.Location = new System.Drawing.Point(10, 15);
            this.labelProjectItems.Name = "labelProjectItems";
            this.labelProjectItems.Size = new System.Drawing.Size(94, 13);
            this.labelProjectItems.TabIndex = 1;
            this.labelProjectItems.Text = "Business Designer";
            // 
            // btApply
            // 
            this.btApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btApply.Location = new System.Drawing.Point(557, 494);
            this.btApply.Name = "btApply";
            this.btApply.Size = new System.Drawing.Size(75, 25);
            this.btApply.TabIndex = 2;
            this.btApply.Text = "Ok";
            this.btApply.UseVisualStyleBackColor = true;
            this.btApply.Click += new System.EventHandler(this.btApply_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "Model Classes:";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(633, 494);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 25);
            this.button1.TabIndex = 3;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listClasses
            // 
            this.listClasses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listClasses.BackColor = System.Drawing.SystemColors.Info;
            this.listClasses.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnName,
            this.columnReference});
            this.listClasses.HideSelection = false;
            this.listClasses.Location = new System.Drawing.Point(9, 62);
            this.listClasses.Name = "listClasses";
            this.listClasses.Size = new System.Drawing.Size(699, 427);
            this.listClasses.TabIndex = 27;
            this.listClasses.UseCompatibleStateImageBehavior = false;
            this.listClasses.View = System.Windows.Forms.View.Details;
            // 
            // columnName
            // 
            this.columnName.Text = "Name";
            this.columnName.Width = 370;
            // 
            // columnReference
            // 
            this.columnReference.Text = "Reference?";
            this.columnReference.Width = 80;
            // 
            // frmExternalReferenceClass
            // 
            this.ClientSize = new System.Drawing.Size(717, 522);
            this.Controls.Add(this.listClasses);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btApply);
            this.Controls.Add(this.labelProjectItems);
            this.Controls.Add(this.comboProjectItems);
            this.MinimizeBox = false;
            this.Name = "frmExternalReferenceClass";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "External Model Class Selector";
            this.Activated += new System.EventHandler(this.frmExternalReferenceClass_Activated);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboProjectItems;
        private System.Windows.Forms.Label labelProjectItems;
        private System.Windows.Forms.Button btApply;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListView listClasses;
        private System.Windows.Forms.ColumnHeader columnName;
        private System.Windows.Forms.ColumnHeader columnReference;

    }
}
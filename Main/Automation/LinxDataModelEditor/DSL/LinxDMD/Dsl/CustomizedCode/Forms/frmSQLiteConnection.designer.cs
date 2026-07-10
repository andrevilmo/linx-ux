namespace Linx.BusinessDataModelDesigner.CustomCode
{
    partial class frmSQLiteConnection
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btTest = new System.Windows.Forms.Button();
            this.btApply = new System.Windows.Forms.Button();
            this.PathComboBox = new System.Windows.Forms.ComboBox();
            this.VersionNumber = new System.Windows.Forms.NumericUpDown();
            this.FailIfMissingCheckBox = new System.Windows.Forms.CheckBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.OpenFileButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.VersionNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(41, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Path:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(27, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Version:";
            // 
            // btTest
            // 
            this.btTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btTest.Location = new System.Drawing.Point(204, 89);
            this.btTest.Name = "btTest";
            this.btTest.Size = new System.Drawing.Size(75, 23);
            this.btTest.TabIndex = 25;
            this.btTest.Text = "Test...";
            this.btTest.UseVisualStyleBackColor = true;
            this.btTest.Click += new System.EventHandler(this.TestButton_Click);
            // 
            // btApply
            // 
            this.btApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btApply.Location = new System.Drawing.Point(283, 89);
            this.btApply.Name = "btApply";
            this.btApply.Size = new System.Drawing.Size(75, 23);
            this.btApply.TabIndex = 24;
            this.btApply.Text = "Apply";
            this.btApply.UseVisualStyleBackColor = true;
            this.btApply.Click += new System.EventHandler(this.ApplyButton_Click);
            // 
            // PathComboBox
            // 
            this.PathComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PathComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.PathComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllSystemSources;
            this.PathComboBox.FormattingEnabled = true;
            this.PathComboBox.Location = new System.Drawing.Point(82, 10);
            this.PathComboBox.Name = "PathComboBox";
            this.PathComboBox.Size = new System.Drawing.Size(246, 21);
            this.PathComboBox.TabIndex = 26;
            // 
            // VersionNumber
            // 
            this.VersionNumber.Location = new System.Drawing.Point(82, 37);
            this.VersionNumber.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.VersionNumber.Name = "VersionNumber";
            this.VersionNumber.Size = new System.Drawing.Size(276, 20);
            this.VersionNumber.TabIndex = 27;
            this.VersionNumber.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // FailIfMissingCheckBox
            // 
            this.FailIfMissingCheckBox.AutoSize = true;
            this.FailIfMissingCheckBox.Location = new System.Drawing.Point(82, 65);
            this.FailIfMissingCheckBox.Name = "FailIfMissingCheckBox";
            this.FailIfMissingCheckBox.Size = new System.Drawing.Size(89, 17);
            this.FailIfMissingCheckBox.TabIndex = 28;
            this.FailIfMissingCheckBox.Text = "Fail If Missing";
            this.FailIfMissingCheckBox.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.AddExtension = false;
            this.openFileDialog1.CheckFileExists = false;
            this.openFileDialog1.FileName = "database.sqlite";
            this.openFileDialog1.Filter = "SQLite files|*.sqlite|All files|*.*";
            // 
            // OpenFileButton
            // 
            this.OpenFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OpenFileButton.Location = new System.Drawing.Point(334, 9);
            this.OpenFileButton.Name = "OpenFileButton";
            this.OpenFileButton.Size = new System.Drawing.Size(24, 23);
            this.OpenFileButton.TabIndex = 29;
            this.OpenFileButton.Text = "...";
            this.OpenFileButton.UseVisualStyleBackColor = true;
            this.OpenFileButton.Click += new System.EventHandler(this.OpenFileButton_Click);
            // 
            // frmSQLiteConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 113);
            this.Controls.Add(this.OpenFileButton);
            this.Controls.Add(this.FailIfMissingCheckBox);
            this.Controls.Add(this.VersionNumber);
            this.Controls.Add(this.PathComboBox);
            this.Controls.Add(this.btTest);
            this.Controls.Add(this.btApply);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(550, 250);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(270, 150);
            this.Name = "frmSQLiteConnection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Data Base Connection";
            ((System.ComponentModel.ISupportInitialize)(this.VersionNumber)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btTest;
        private System.Windows.Forms.Button btApply;
        private System.Windows.Forms.ComboBox PathComboBox;
        private System.Windows.Forms.NumericUpDown VersionNumber;
        private System.Windows.Forms.CheckBox FailIfMissingCheckBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button OpenFileButton;
    }
}
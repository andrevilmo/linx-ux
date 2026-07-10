namespace Linx.EntityAdapterDesigner.CustomCode
{
    partial class FrmMacroScriptEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMacroScriptEditor));
            this.btApplyScript = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.macroNodeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.macroNodeDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Document = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.DocumentTextBox = new System.Windows.Forms.TextBox();
            this.pictureSearch = new System.Windows.Forms.PictureBox();
            this.textSearch = new System.Windows.Forms.TextBox();
            this.labelMacros = new System.Windows.Forms.Label();
            this.scriptBox = new ScintillaNET.Scintilla();
            this.labelScript = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelInfo = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.macroNodeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.macroNodeDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scriptBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // btApplyScript
            // 
            this.btApplyScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btApplyScript.Location = new System.Drawing.Point(856, 534);
            this.btApplyScript.Name = "btApplyScript";
            this.btApplyScript.Size = new System.Drawing.Size(75, 23);
            this.btApplyScript.TabIndex = 22;
            this.btApplyScript.Text = "Apply";
            this.btApplyScript.UseVisualStyleBackColor = true;
            this.btApplyScript.Click += new System.EventHandler(this.btApplyScript_Click);
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.Location = new System.Drawing.Point(933, 534);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 23;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // macroNodeBindingSource
            // 
            this.macroNodeBindingSource.DataSource = typeof(Linx.Builder.Resources.MacroNode);
            // 
            // macroNodeDataGridView
            // 
            this.macroNodeDataGridView.AllowUserToAddRows = false;
            this.macroNodeDataGridView.AllowUserToDeleteRows = false;
            this.macroNodeDataGridView.AllowUserToOrderColumns = true;
            this.macroNodeDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.macroNodeDataGridView.AutoGenerateColumns = false;
            this.macroNodeDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.macroNodeDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn4,
            this.Document});
            this.macroNodeDataGridView.DataSource = this.macroNodeBindingSource;
            this.macroNodeDataGridView.Location = new System.Drawing.Point(3, 32);
            this.macroNodeDataGridView.MultiSelect = false;
            this.macroNodeDataGridView.Name = "macroNodeDataGridView";
            this.macroNodeDataGridView.ReadOnly = true;
            this.macroNodeDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.macroNodeDataGridView.Size = new System.Drawing.Size(995, 131);
            this.macroNodeDataGridView.TabIndex = 25;
            this.macroNodeDataGridView.SelectionChanged += new System.EventHandler(this.macroNodeDataGridView_SelectionChanged);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Name";
            this.dataGridViewTextBoxColumn1.HeaderText = "Name";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 200;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Description";
            this.dataGridViewTextBoxColumn4.HeaderText = "Description";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 350;
            // 
            // Document
            // 
            this.Document.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Document.DataPropertyName = "Document";
            this.Document.HeaderText = "Document";
            this.Document.Name = "Document";
            this.Document.ReadOnly = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Location = new System.Drawing.Point(4, 5);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.DocumentTextBox);
            this.splitContainer1.Panel1.Controls.Add(this.pictureSearch);
            this.splitContainer1.Panel1.Controls.Add(this.textSearch);
            this.splitContainer1.Panel1.Controls.Add(this.labelMacros);
            this.splitContainer1.Panel1.Controls.Add(this.macroNodeDataGridView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.scriptBox);
            this.splitContainer1.Panel2.Controls.Add(this.labelScript);
            this.splitContainer1.Size = new System.Drawing.Size(1006, 524);
            this.splitContainer1.SplitterDistance = 253;
            this.splitContainer1.TabIndex = 26;
            // 
            // DocumentTextBox
            // 
            this.DocumentTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DocumentTextBox.Location = new System.Drawing.Point(4, 170);
            this.DocumentTextBox.Multiline = true;
            this.DocumentTextBox.Name = "DocumentTextBox";
            this.DocumentTextBox.ReadOnly = true;
            this.DocumentTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.DocumentTextBox.Size = new System.Drawing.Size(994, 76);
            this.DocumentTextBox.TabIndex = 29;
            // 
            // pictureSearch
            // 
            this.pictureSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureSearch.Image = ((System.Drawing.Image)(resources.GetObject("pictureSearch.Image")));
            this.pictureSearch.Location = new System.Drawing.Point(664, 8);
            this.pictureSearch.Name = "pictureSearch";
            this.pictureSearch.Size = new System.Drawing.Size(18, 18);
            this.pictureSearch.TabIndex = 28;
            this.pictureSearch.TabStop = false;
            // 
            // textSearch
            // 
            this.textSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textSearch.Location = new System.Drawing.Point(684, 7);
            this.textSearch.Name = "textSearch";
            this.textSearch.Size = new System.Drawing.Size(314, 20);
            this.textSearch.TabIndex = 27;
            this.textSearch.TextChanged += new System.EventHandler(this.textSearch_TextChanged);
            // 
            // labelMacros
            // 
            this.labelMacros.AutoSize = true;
            this.labelMacros.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMacros.Location = new System.Drawing.Point(5, 11);
            this.labelMacros.Name = "labelMacros";
            this.labelMacros.Size = new System.Drawing.Size(108, 13);
            this.labelMacros.TabIndex = 26;
            this.labelMacros.Text = "Available Macros:";
            // 
            // scriptBox
            // 
            this.scriptBox.ConfigurationManager.Language = "js";
            this.scriptBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scriptBox.IsBraceMatching = true;
            this.scriptBox.Location = new System.Drawing.Point(0, 13);
            this.scriptBox.Margins.Margin0.Width = 20;
            this.scriptBox.Margins.Margin2.Width = 10;
            this.scriptBox.Name = "scriptBox";
            this.scriptBox.Size = new System.Drawing.Size(1002, 250);
            this.scriptBox.Styles.BraceBad.FontName = "Verdana\0\0\0\0\0\0\0\0\0\0\0\0\0";
            this.scriptBox.Styles.BraceLight.FontName = "Verdana\0\0\0\0\0\0\0\0\0\0\0\0\0";
            this.scriptBox.Styles.CallTip.FontName = "Segoe UI\0\0\0\0\0\0\0\0\0\0\0\0";
            this.scriptBox.Styles.ControlChar.FontName = "Verdana\0\0\0\0\0\0\0\0\0\0\0\0\0";
            this.scriptBox.Styles.Default.BackColor = System.Drawing.SystemColors.Window;
            this.scriptBox.Styles.Default.FontName = "Verdana\0\0\0\0\0\0\0\0\0\0\0\0\0";
            this.scriptBox.Styles.IndentGuide.FontName = "Verdana\0\0\0\0\0\0\0\0\0\0\0\0\0";
            this.scriptBox.Styles.LastPredefined.FontName = "Verdana\0\0\0\0\0\0\0\0\0\0\0\0\0";
            this.scriptBox.Styles.LineNumber.FontName = "Verdana\0\0\0\0\0\0\0\0\0\0\0\0\0";
            this.scriptBox.Styles.Max.FontName = "Verdana\0\0\0\0\0\0\0\0\0\0\0\0\0";
            this.scriptBox.TabIndex = 28;
            // 
            // labelScript
            // 
            this.labelScript.AutoSize = true;
            this.labelScript.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelScript.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelScript.Location = new System.Drawing.Point(0, 0);
            this.labelScript.Name = "labelScript";
            this.labelScript.Size = new System.Drawing.Size(44, 13);
            this.labelScript.TabIndex = 27;
            this.labelScript.Text = "Script:";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Description";
            this.dataGridViewTextBoxColumn2.HeaderText = "Description";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 400;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Document";
            this.dataGridViewTextBoxColumn3.HeaderText = "Syntax";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 300;
            // 
            // labelInfo
            // 
            this.labelInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelInfo.AutoSize = true;
            this.labelInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInfo.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.labelInfo.Location = new System.Drawing.Point(26, 539);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(417, 13);
            this.labelInfo.TabIndex = 29;
            this.labelInfo.Text = "Copy the command from column [Syntax] and paste into the [Script Box].";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(8, 537);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(18, 18);
            this.pictureBox1.TabIndex = 29;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(454, 537);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(18, 18);
            this.pictureBox2.TabIndex = 30;
            this.pictureBox2.TabStop = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(472, 539);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(253, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "Type [ctrl + j] to open the snippets window.";
            // 
            // FrmMacroScriptEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 561);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btApplyScript);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "FrmMacroScriptEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Macro Script Editor";
            ((System.ComponentModel.ISupportInitialize)(this.macroNodeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.macroNodeDataGridView)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scriptBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btApplyScript;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.BindingSource macroNodeBindingSource;
        private System.Windows.Forms.DataGridView macroNodeDataGridView;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label labelMacros;
        private System.Windows.Forms.Label labelScript;
        private System.Windows.Forms.TextBox textSearch;
        private System.Windows.Forms.PictureBox pictureSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox DocumentTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Document;
        private ScintillaNET.Scintilla scriptBox;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
    }
}
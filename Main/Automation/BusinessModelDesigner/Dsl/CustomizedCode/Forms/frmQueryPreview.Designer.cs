namespace Linx.BusinessModelDesigner.CustomizedCode.Forms
{
    partial class frmQueryPreview
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
            this.tabEntityPreview = new System.Windows.Forms.TabControl();
            this.tabResult = new System.Windows.Forms.TabPage();
            this.lbStatus = new System.Windows.Forms.Label();
            this.btPlay = new System.Windows.Forms.Button();
            this.lbRowsPerPage = new System.Windows.Forms.Label();
            this.numTotalRows = new System.Windows.Forms.NumericUpDown();
            this.dataGridResult = new System.Windows.Forms.DataGridView();
            this.tabScript = new System.Windows.Forms.TabPage();
            this.txScript = new System.Windows.Forms.RichTextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lbFilter = new System.Windows.Forms.Label();
            this.txFilter = new System.Windows.Forms.RichTextBox();
            this.tabEntityPreview.SuspendLayout();
            this.tabResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTotalRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridResult)).BeginInit();
            this.tabScript.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabEntityPreview
            // 
            this.tabEntityPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabEntityPreview.Controls.Add(this.tabResult);
            this.tabEntityPreview.Controls.Add(this.tabScript);
            this.tabEntityPreview.Location = new System.Drawing.Point(1, 3);
            this.tabEntityPreview.Name = "tabEntityPreview";
            this.tabEntityPreview.SelectedIndex = 0;
            this.tabEntityPreview.Size = new System.Drawing.Size(936, 595);
            this.tabEntityPreview.TabIndex = 0;
            // 
            // tabResult
            // 
            this.tabResult.Controls.Add(this.txFilter);
            this.tabResult.Controls.Add(this.lbFilter);
            this.tabResult.Controls.Add(this.lbStatus);
            this.tabResult.Controls.Add(this.btPlay);
            this.tabResult.Controls.Add(this.lbRowsPerPage);
            this.tabResult.Controls.Add(this.numTotalRows);
            this.tabResult.Controls.Add(this.dataGridResult);
            this.tabResult.Location = new System.Drawing.Point(4, 22);
            this.tabResult.Name = "tabResult";
            this.tabResult.Padding = new System.Windows.Forms.Padding(3);
            this.tabResult.Size = new System.Drawing.Size(928, 569);
            this.tabResult.TabIndex = 0;
            this.tabResult.Text = "Result";
            this.tabResult.UseVisualStyleBackColor = true;
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStatus.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lbStatus.Location = new System.Drawing.Point(199, 10);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(0, 13);
            this.lbStatus.TabIndex = 5;
            // 
            // btPlay
            // 
            this.btPlay.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btPlay.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btPlay.ForeColor = System.Drawing.Color.DarkRed;
            this.btPlay.Location = new System.Drawing.Point(167, 4);
            this.btPlay.Name = "btPlay";
            this.btPlay.Size = new System.Drawing.Size(26, 24);
            this.btPlay.TabIndex = 4;
            this.btPlay.Text = "!";
            this.toolTip1.SetToolTip(this.btPlay, "Execute query");
            this.btPlay.UseVisualStyleBackColor = true;
            this.btPlay.Click += new System.EventHandler(this.btPlay_Click);
            // 
            // lbRowsPerPage
            // 
            this.lbRowsPerPage.AutoSize = true;
            this.lbRowsPerPage.Location = new System.Drawing.Point(5, 7);
            this.lbRowsPerPage.Name = "lbRowsPerPage";
            this.lbRowsPerPage.Size = new System.Drawing.Size(37, 13);
            this.lbRowsPerPage.TabIndex = 3;
            this.lbRowsPerPage.Text = "Rows ";
            // 
            // numTotalRows
            // 
            this.numTotalRows.Location = new System.Drawing.Point(43, 6);
            this.numTotalRows.Name = "numTotalRows";
            this.numTotalRows.Size = new System.Drawing.Size(120, 20);
            this.numTotalRows.TabIndex = 1;
            this.numTotalRows.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // dataGridResult
            // 
            this.dataGridResult.AllowUserToAddRows = false;
            this.dataGridResult.AllowUserToDeleteRows = false;
            this.dataGridResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridResult.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridResult.Location = new System.Drawing.Point(6, 118);
            this.dataGridResult.Name = "dataGridResult";
            this.dataGridResult.ReadOnly = true;
            this.dataGridResult.Size = new System.Drawing.Size(916, 442);
            this.dataGridResult.TabIndex = 0;
            // 
            // tabScript
            // 
            this.tabScript.Controls.Add(this.txScript);
            this.tabScript.Location = new System.Drawing.Point(4, 22);
            this.tabScript.Name = "tabScript";
            this.tabScript.Padding = new System.Windows.Forms.Padding(3);
            this.tabScript.Size = new System.Drawing.Size(928, 569);
            this.tabScript.TabIndex = 1;
            this.tabScript.Text = "Script";
            this.tabScript.UseVisualStyleBackColor = true;
            // 
            // txScript
            // 
            this.txScript.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txScript.Location = new System.Drawing.Point(8, 7);
            this.txScript.Name = "txScript";
            this.txScript.ReadOnly = true;
            this.txScript.Size = new System.Drawing.Size(914, 556);
            this.txScript.TabIndex = 0;
            this.txScript.Text = "";
            // 
            // lbFilter
            // 
            this.lbFilter.AutoSize = true;
            this.lbFilter.Location = new System.Drawing.Point(5, 30);
            this.lbFilter.Name = "lbFilter";
            this.lbFilter.Size = new System.Drawing.Size(32, 13);
            this.lbFilter.TabIndex = 6;
            this.lbFilter.Text = "Filter:";
            // 
            // txFilter
            // 
            this.txFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txFilter.Location = new System.Drawing.Point(41, 30);
            this.txFilter.Name = "txFilter";
            this.txFilter.Size = new System.Drawing.Size(881, 82);
            this.txFilter.TabIndex = 7;
            this.txFilter.Text = "";
            // 
            // frmQueryPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 599);
            this.Controls.Add(this.tabEntityPreview);
            this.MinimizeBox = false;
            this.Name = "frmQueryPreview";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Entity Preview";
            this.Shown += new System.EventHandler(this.frmQueryPreview_Shown);
            this.tabEntityPreview.ResumeLayout(false);
            this.tabResult.ResumeLayout(false);
            this.tabResult.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTotalRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridResult)).EndInit();
            this.tabScript.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabEntityPreview;
        private System.Windows.Forms.TabPage tabResult;
        private System.Windows.Forms.DataGridView dataGridResult;
        private System.Windows.Forms.TabPage tabScript;
        private System.Windows.Forms.RichTextBox txScript;
        private System.Windows.Forms.NumericUpDown numTotalRows;
        private System.Windows.Forms.Button btPlay;
        private System.Windows.Forms.Label lbRowsPerPage;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.RichTextBox txFilter;
        private System.Windows.Forms.Label lbFilter;
    }
}
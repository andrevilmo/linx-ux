namespace Linx.BusinessDataModelDesigner.CustomCode
{
    partial class frmReverseEngineer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReverseEngineer));
            this.trvDatabaseObjects = new System.Windows.Forms.TreeView();
            this.ObjectImageList = new System.Windows.Forms.ImageList(this.components);
            this.lstForeignKeys = new System.Windows.Forms.ListBox();
            this.txtPrimaryKey = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.MainStatus = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.LoadProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lstIndexes = new System.Windows.Forms.ListBox();
            this.btCancel = new System.Windows.Forms.Button();
            this.btOk = new System.Windows.Forms.Button();
            this.checkPeriphery = new System.Windows.Forms.CheckBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grdColumns = new System.Windows.Forms.DataGridView();
            this.grdColumnsName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdColumnsDataType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdColumnsPK = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.MainStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdColumns)).BeginInit();
            this.SuspendLayout();
            // 
            // trvDatabaseObjects
            // 
            this.trvDatabaseObjects.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trvDatabaseObjects.BackColor = System.Drawing.SystemColors.Info;
            this.trvDatabaseObjects.CheckBoxes = true;
            this.trvDatabaseObjects.HideSelection = false;
            this.trvDatabaseObjects.ImageIndex = 0;
            this.trvDatabaseObjects.ImageList = this.ObjectImageList;
            this.trvDatabaseObjects.Location = new System.Drawing.Point(5, 62);
            this.trvDatabaseObjects.Name = "trvDatabaseObjects";
            this.trvDatabaseObjects.SelectedImageIndex = 0;
            this.trvDatabaseObjects.Size = new System.Drawing.Size(302, 438);
            this.trvDatabaseObjects.TabIndex = 2;
            this.trvDatabaseObjects.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvDatabaseObjects_AfterSelect);
            // 
            // ObjectImageList
            // 
            this.ObjectImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ObjectImageList.ImageStream")));
            this.ObjectImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.ObjectImageList.Images.SetKeyName(0, "database.png");
            this.ObjectImageList.Images.SetKeyName(1, "schema.png");
            this.ObjectImageList.Images.SetKeyName(2, "Table_32.png");
            this.ObjectImageList.Images.SetKeyName(3, "view.png");
            // 
            // lstForeignKeys
            // 
            this.lstForeignKeys.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstForeignKeys.BackColor = System.Drawing.SystemColors.MenuBar;
            this.lstForeignKeys.FormattingEnabled = true;
            this.lstForeignKeys.Location = new System.Drawing.Point(5, 62);
            this.lstForeignKeys.Name = "lstForeignKeys";
            this.lstForeignKeys.Size = new System.Drawing.Size(437, 56);
            this.lstForeignKeys.TabIndex = 3;
            // 
            // txtPrimaryKey
            // 
            this.txtPrimaryKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPrimaryKey.Location = new System.Drawing.Point(5, 22);
            this.txtPrimaryKey.Name = "txtPrimaryKey";
            this.txtPrimaryKey.ReadOnly = true;
            this.txtPrimaryKey.Size = new System.Drawing.Size(437, 20);
            this.txtPrimaryKey.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Primary Key:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 194);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Columns:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Foreign Keys:";
            // 
            // MainStatus
            // 
            this.MainStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.LoadProgress});
            this.MainStatus.Location = new System.Drawing.Point(0, 549);
            this.MainStatus.Name = "MainStatus";
            this.MainStatus.Size = new System.Drawing.Size(779, 22);
            this.MainStatus.TabIndex = 7;
            this.MainStatus.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // LoadProgress
            // 
            this.LoadProgress.Name = "LoadProgress";
            this.LoadProgress.Size = new System.Drawing.Size(100, 16);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Objects:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Search:";
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Location = new System.Drawing.Point(5, 22);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(302, 20);
            this.txtSearch.TabIndex = 10;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 120);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Indexes:";
            // 
            // lstIndexes
            // 
            this.lstIndexes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstIndexes.BackColor = System.Drawing.SystemColors.MenuBar;
            this.lstIndexes.FormattingEnabled = true;
            this.lstIndexes.Location = new System.Drawing.Point(5, 136);
            this.lstIndexes.Name = "lstIndexes";
            this.lstIndexes.Size = new System.Drawing.Size(437, 56);
            this.lstIndexes.TabIndex = 11;
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.Location = new System.Drawing.Point(697, 520);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 25);
            this.btCancel.TabIndex = 14;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btOk
            // 
            this.btOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btOk.Location = new System.Drawing.Point(621, 520);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(75, 25);
            this.btOk.TabIndex = 13;
            this.btOk.Text = "Ok";
            this.btOk.UseVisualStyleBackColor = true;
            this.btOk.Click += new System.EventHandler(this.btOk_Click);
            // 
            // checkPeriphery
            // 
            this.checkPeriphery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkPeriphery.AutoSize = true;
            this.checkPeriphery.Location = new System.Drawing.Point(133, 44);
            this.checkPeriphery.Name = "checkPeriphery";
            this.checkPeriphery.Size = new System.Drawing.Size(174, 17);
            this.checkPeriphery.TabIndex = 15;
            this.checkPeriphery.Text = "Add FKs of Business Neighbors";
            this.checkPeriphery.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Location = new System.Drawing.Point(4, 4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.checkPeriphery);
            this.splitContainer1.Panel1.Controls.Add(this.trvDatabaseObjects);
            this.splitContainer1.Panel1.Controls.Add(this.txtSearch);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grdColumns);
            this.splitContainer1.Panel2.Controls.Add(this.label7);
            this.splitContainer1.Panel2.Controls.Add(this.lstForeignKeys);
            this.splitContainer1.Panel2.Controls.Add(this.lstIndexes);
            this.splitContainer1.Panel2.Controls.Add(this.txtPrimaryKey);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Size = new System.Drawing.Size(770, 510);
            this.splitContainer1.SplitterDistance = 314;
            this.splitContainer1.TabIndex = 16;
            // 
            // grdColumns
            // 
            this.grdColumns.AllowUserToAddRows = false;
            this.grdColumns.AllowUserToDeleteRows = false;
            this.grdColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdColumns.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.grdColumnsName,
            this.grdColumnsDataType,
            this.grdColumnsPK});
            this.grdColumns.Location = new System.Drawing.Point(9, 211);
            this.grdColumns.Name = "grdColumns";
            this.grdColumns.ReadOnly = true;
            this.grdColumns.Size = new System.Drawing.Size(433, 289);
            this.grdColumns.TabIndex = 13;
            // 
            // grdColumnsName
            // 
            this.grdColumnsName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.grdColumnsName.DataPropertyName = "Name";
            this.grdColumnsName.HeaderText = "Nome";
            this.grdColumnsName.Name = "grdColumnsName";
            this.grdColumnsName.ReadOnly = true;
            // 
            // grdColumnsDataType
            // 
            this.grdColumnsDataType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.grdColumnsDataType.DataPropertyName = "DbDataType";
            this.grdColumnsDataType.HeaderText = "Db DataType";
            this.grdColumnsDataType.Name = "grdColumnsDataType";
            this.grdColumnsDataType.ReadOnly = true;
            this.grdColumnsDataType.Width = 96;
            // 
            // grdColumnsPK
            // 
            this.grdColumnsPK.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.grdColumnsPK.DataPropertyName = "IsPK";
            this.grdColumnsPK.HeaderText = "PK";
            this.grdColumnsPK.Name = "grdColumnsPK";
            this.grdColumnsPK.ReadOnly = true;
            this.grdColumnsPK.Width = 27;
            // 
            // frmReverseEngineer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 571);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.MainStatus);
            this.Controls.Add(this.splitContainer1);
            this.MinimizeBox = false;
            this.Name = "frmReverseEngineer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reverse Engineering";
            this.MainStatus.ResumeLayout(false);
            this.MainStatus.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdColumns)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView trvDatabaseObjects;
        private System.Windows.Forms.ListBox lstForeignKeys;
        private System.Windows.Forms.TextBox txtPrimaryKey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.StatusStrip MainStatus;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripProgressBar LoadProgress;
        private System.Windows.Forms.ImageList ObjectImageList;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListBox lstIndexes;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.CheckBox checkPeriphery;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView grdColumns;
        private System.Windows.Forms.DataGridViewTextBoxColumn grdColumnsName;
        private System.Windows.Forms.DataGridViewTextBoxColumn grdColumnsDataType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn grdColumnsPK;
    }
}


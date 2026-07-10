namespace Linx.EntityAdapterDesigner.CustomizedCode
{
    partial class FormPropertySort
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPropertySort));
            this.label1 = new System.Windows.Forms.Label();
            this.cmbPropertyOrder = new System.Windows.Forms.ComboBox();
            this.btnApplyOrder = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Property For Sorting:";
            // 
            // cmbPropertyOrder
            // 
            this.cmbPropertyOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPropertyOrder.FormattingEnabled = true;
            this.cmbPropertyOrder.Location = new System.Drawing.Point(128, 25);
            this.cmbPropertyOrder.Name = "cmbPropertyOrder";
            this.cmbPropertyOrder.Size = new System.Drawing.Size(222, 21);
            this.cmbPropertyOrder.TabIndex = 1;
            // 
            // btnApplyOrder
            // 
            this.btnApplyOrder.Location = new System.Drawing.Point(258, 58);
            this.btnApplyOrder.Name = "btnApplyOrder";
            this.btnApplyOrder.Size = new System.Drawing.Size(92, 23);
            this.btnApplyOrder.TabIndex = 2;
            this.btnApplyOrder.Text = "Apply";
            this.btnApplyOrder.UseVisualStyleBackColor = true;
            this.btnApplyOrder.Click += new System.EventHandler(this.btnApplyOrder_Click);
            // 
            // FormPropertySort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 89);
            this.Controls.Add(this.btnApplyOrder);
            this.Controls.Add(this.cmbPropertyOrder);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPropertySort";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sort Properties";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbPropertyOrder;
        private System.Windows.Forms.Button btnApplyOrder;

    }
}
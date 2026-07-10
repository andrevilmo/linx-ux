using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Linx.Tools; 

namespace Linx.EntityAdapterDesigner.CustomCode
{
    public partial class FrmAttributeGroup : Form
    {
        public string GroupCode { get; set; }
        public string GroupName { get; set; }
        public ListViewItem ListItem { get; set; }

        private ListViewGroupCollection groups;
        public ListViewGroupCollection Groups
        {
            get
            {
                return groups;
            }
            set
            {
                groups = value;
                if (groups != null)
                {
                    int maxCode = 0;
                    foreach (ListViewGroup gr in groups)
                    {
                        if (maxCode < int.Parse(((string)gr.Tag).Left("::")))
                            maxCode = int.Parse(((string)gr.Tag).Left("::"));
                    }
                    GroupCode = (maxCode+1).ToString().PadLeft(4, '0');
                    GroupName = "";
                }
            }
        }



        private ListViewGroup group;
        public ListViewGroup Group 
        {
            get
            {
                return group;
            }
            set
            {
                group = value;
                if (group != null)
                {
                    GroupCode = ((string)group.Tag).Left("::");
                    GroupName = (((string)group.Tag).Right("::") + "||").Left("||");
                }
            }
        }

        public FrmAttributeGroup()
        {
            InitializeComponent();
        }
               

        private void btApply_Click(object sender, EventArgs e)
        {
            if (this.group == null)
            {
                if (Groups != null)
                {
                    this.group = Groups.Add(this.GroupCode, this.GroupName);
                    this.group.Tag = this.GroupCode + "::" + this.GroupName;
                    if (this.ListItem != null)
                        this.ListItem.Group = this.group;
                }
            }
            else
            {
                this.group.Header = this.GroupName;
                this.group.Tag = this.GroupCode + "::" + this.GroupName;
            }
            this.Close();
        }

        private void FrmAttributeGroup_Load(object sender, EventArgs e)
        {
            this.frmAttributeGroupBindingSource.DataSource = this;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
    }
}

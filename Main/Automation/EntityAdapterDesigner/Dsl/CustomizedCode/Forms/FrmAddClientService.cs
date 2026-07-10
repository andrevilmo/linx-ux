using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Linx.Tools;
using System.Reflection;
using System.IO;
using System.Collections;

namespace Linx.EntityAdapterDesigner.CustomCode
{

    public partial class FrmAddClientService : Form
    {
        public string SelectService { get; set; }

        public FrmAddClientService()
        {
            InitializeComponent();
        }

        public void AddServices(string[] serviceNames)
        {
            foreach (var service in serviceNames)
            {
                this.lstServices.Items.Add(service);
            }
        }
        
        private void FrmAddClientService_Load(object sender, EventArgs e)
        {

        }
        
        private void btCancel_Click(object sender, EventArgs e)
        {
            this.SelectService = "";
            this.Close();
        }

        private void lstEvents_MouseDoubleClick(object sender, MouseEventArgs e)
        {            
            this.btSelectService_Click(sender, e);
        }

        private void btSelectService_Click(object sender, EventArgs e)
        {
            this.SelectService = this.lstServices.SelectedItem.ToString();
            this.Close();
        }
    }
}

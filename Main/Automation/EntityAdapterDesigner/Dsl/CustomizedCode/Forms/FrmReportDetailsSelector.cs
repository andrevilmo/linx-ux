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

    public partial class FrmReportDetailsSelector : Form
    {        
        private List<string> _selectedDetails = new List<string>();
        public List<string> SelectedDetails { get { return _selectedDetails; } }

        public bool IsMainEntity
        {
            get
            {
                if (this.entity != null)
                    return (EntityAdapterReferencesEntityDataModel.GetEntityDataModel(this.entity) != null);
                else
                    return false;
            }
        }

        private EntityAdapter entity;
        public EntityAdapter Entity
        {
            get { return this.entity; }
            set
            {
                this.entity = value;
                this.LoadChilds();
            }
        }


        public FrmReportDetailsSelector()
        {
            InitializeComponent();
        }
        
        private void LoadChilds()
        {
            if (this.entity != null)
            {
                foreach (var child in this.entity.SourceEntityAdapters)
                    this.lstDetails.Items.Add(child.Name);
            }
        }

        private void FrmReportDetailsSelector_Load(object sender, EventArgs e)
        {

        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lstEvents_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.btCreateEvents_Click(sender, e);
        }

        private void btCreateEvents_Click(object sender, EventArgs e)
        {
            if (this.entity != null)
            {
                this.SelectedDetails.Clear();
                foreach (var item in this.lstDetails.SelectedItems)
                {
                    this.SelectedDetails.Add((string)item);
                }
                if (this.SelectedDetails.Count == 0)
                {
                    MessageBox.Show("Select at least one entity detail for creating the report.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            this.Close();
        }
    }
}

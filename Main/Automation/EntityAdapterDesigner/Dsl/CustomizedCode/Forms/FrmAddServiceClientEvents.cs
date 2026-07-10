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

    public partial class FrmAddServiceClientEvents : Form
    {
        Dictionary<string, string> messages = new Dictionary<string, string>();
        private ClientLocalService _service;
        public ClientLocalService Service
        {
            get { return this._service; }
            set
            {
                this._service = value;
                this.LoadOperationalEvents();
            }
        }

        public FrmAddServiceClientEvents()
        {
            InitializeComponent();
        }

        private void AddEvent(string eventName)
        {
            if (this._service != null)
            {
                if (!this._service.ExistsClientEvent(eventName))
                    this.lstEvents.Items.Add(eventName);
            }
        }

        private void LoadOperationalEvents()
        {
            if (this._service == null)
                return;

            foreach (var evName in this._service.GetClientEventNames())
            {
                this.AddEvent(evName);
            }
        }

        private void FrmAddServiceClientEvents_Load(object sender, EventArgs e)
        {

        }

        private void btAddEvents_Click(object sender, EventArgs e)
        {
            if (this._service != null)
            {
                foreach (var item in this.lstEvents.SelectedItems)
                {
                    this._service.AddClientEvent((string)item, messages);
                }
            }
            this.Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lstEvents_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.btAddEvents_Click(sender, e);
        }
    }
}

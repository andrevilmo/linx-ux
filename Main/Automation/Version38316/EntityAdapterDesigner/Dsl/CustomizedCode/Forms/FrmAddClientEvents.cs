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

    public partial class FrmAddClientEvents : Form
    {
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
                this.LoadOperationalEvents();
            }
        }


        public FrmAddClientEvents()
        {
            InitializeComponent();
        }

        private void AddEvent(string eventName)
        {
            if (this.entity != null)
            {
                if (!this.entity.ExistsClientEvent(eventName))
                    this.lstEvents.Items.Add(eventName);
            }
        }

        private void LoadOperationalEvents()
        {
            this.AddEvent("OnDetailSearching");
            this.AddEvent("OnDetailSearched");
			this.AddEvent("OnSelected");
            this.AddEvent("OnPropertyChanged");
            this.AddEvent("OnSaving");
            this.AddEvent("OnSaved");
            this.AddEvent("OnDeleting");
            this.AddEvent("OnAdding");
            this.AddEvent("OnDeleted");
            this.AddEvent("OnAdded");
            this.AddEvent("OnDataRefreshing");
            this.AddEvent("OnDataRefreshed");
            
			foreach (LookUpAdapter lookUpAdapter in this.entity.LookUpAdapters)
			{
                this.AddEvent(lookUpAdapter.GetBeforeQueryName());                
                this.AddEvent(lookUpAdapter.GetOnLookedUpName());
                this.AddEvent(lookUpAdapter.GetOnLoadingQueryName());                 
			}

            foreach (var attr in this.entity.GetAllInheritanceAttributes())
            {
                if (!attr.LookUpSubscription.IsNullOrEmpty())
                {
                    string lookupName = attr.GetLookUpName();
                    if (!lookupName.IsNullOrEmpty())
                    {
                        this.AddEvent(LookUpAdapter.GetBeforeQueryName(lookupName));
                        this.AddEvent(LookUpAdapter.GetOnLookedUpName(lookupName));
                        this.AddEvent(LookUpAdapter.GetOnLoadingQueryName(lookupName)); 
                    }
                }
            }
        }

        private void FrmAddClientEvents_Load(object sender, EventArgs e)
        {

        }

        private void btAddEvents_Click(object sender, EventArgs e)
        {
            if (this.entity != null)
            {
                foreach (var item in this.lstEvents.SelectedItems)
                {
                    this.entity.AddClientEvent((string)item);   
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
            this.btAddEvents_Click(sender, e) ;
        }
    }
}

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

namespace Linx.BusinessDataModelDesigner.CustomCode
{

    public partial class FrmAddEntityEvents : Form
    {
        public bool IsMainEntity
        {
            get
            {
                if (this.entity != null)
                    return (entity.ModelView == null);
                else
                    return false;
            }
        }

        private ModelClass entity;
        public ModelClass Entity
        {
            get { return this.entity; }
            set
            {
                this.entity = value;
                this.LoadOperationalEvents();
            }
        }


        public FrmAddEntityEvents()
        {
            InitializeComponent();
        }

        private void AddEvent(string eventName)
        {
            if (this.entity != null)
            {
                if (this.entity.Operations.Where(e => e.Name == eventName).Count() == 0)
                    this.lstEvents.Items.Add(eventName);
            }
        }

        private void LoadOperationalEvents()
        {
            this.AddEvent("OnValidatingChanges");
            this.AddEvent("OnSavingChanges");
            this.AddEvent("OnSavingContextChanges");
            this.AddEvent("OnSavedChanges");
            this.AddEvent("OnSavedContextChanges");
            this.AddEvent("OnTransactingChanges");
            this.AddEvent("OnTransactingContextChanges");
            this.AddEvent("OnTransactedChanges");
            this.AddEvent("OnTransactedContextChanges");
            this.AddEvent("OnPrepareForSearching");
            this.AddEvent("OnSearching");
            this.AddEvent("OnSearchingReplacement");
            this.AddEvent("OnFiltering");
            
            //foreach (LookUpAdapter lookUpAdapter in this.entity.LookUpAdapters)
            //{
            //    this.AddEvent(lookUpAdapter.GetOnLookingUpName());
            //}
        }

        private void FrmAddEntityEvents_Load(object sender, EventArgs e)
        {

        }

        private void btAddEvents_Click(object sender, EventArgs e)
        {
            if (this.entity != null)
            {
                using (Microsoft.VisualStudio.Modeling.Transaction transaction =
                            entity.Store.TransactionManager.BeginTransaction("Change designer by events."))
                {
                    foreach (var item in this.lstEvents.SelectedItems)
                    {
                        this.entity.AddServerEvent((string)item);
                    }
                    transaction.Commit();
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

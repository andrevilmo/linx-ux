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

    public partial class FrmAddUiClientEvents : Form
    {
        private List<string> exposedEvents = new List<string>();
        private EntityAdapterUserInterface _ui;
        public EntityAdapterUserInterface UI
        {
            get { return this._ui; }
            set
            {
                this._ui = value;
                this.LoadOperationalEvents();
            }
        }

        public FrmAddUiClientEvents()
        {
            InitializeComponent();
        }

        private void AddEvent(string eventName)
        {
            if (this._ui != null)
            {
                if (!this._ui.ExistsClientEvent(eventName))
                    this.lstEvents.Items.Add(eventName);
            }
        }

        private void LoadOperationalEvents()
        {
            string exposedEvent;

            this.AddEvent("OnLoading");
            this.AddEvent("OnLoaded");
            this.AddEvent("OnClosing");

            //Web Events
            if (_ui.VisualType == InterfaceType.Web)
            {
                this.AddEvent("OnClearing");
                this.AddEvent("OnCleared");
                this.AddEvent("OnReporting");
                this.AddEvent("OnTabActive");
                this.AddEvent("OnDataGridCreated");
                this.AddEvent("OnDataGridRowChecked");
                this.AddEvent("OnPropertyChangeDataGrid");
                this.AddEvent("OnSearching");
                this.AddEvent("OnSearched");
                this.AddEvent("OnNavigating");
                this.AddEvent("OnNavigated");
                this.AddEvent("OnEditing");
                this.AddEvent("OnEdited");
                this.AddEvent("OnPrinting");
                this.AddEvent("OnPrinted");
                this.AddEvent("OnCancelling");
                this.AddEvent("OnCancelled");
                this.AddEvent("OnSaving");
                this.AddEvent("OnSaved");                
                this.AddEvent("OnControlGotFocus");
                this.AddEvent("OnControlLostFocus");
                this.AddEvent("OnToolbarAction");
                this.AddEvent("OnLoadedChildUI");
                this.AddEvent("OnClickPivotCell");
                this.AddEvent("OnOpeningExternalUIFromGrid");
                this.AddEvent("OnchangePivotLayoutOnLoad");
                this.AddEvent("OnPivotLoadLayoutCompleted");
                //Wizard Events
                exposedEvent = "OnWizardInitializing";
                this.AddEvent(exposedEvent);
                this.exposedEvents.Add(exposedEvent);
                exposedEvent = "OnWizardStepChanging";
                this.AddEvent(exposedEvent);
                this.exposedEvents.Add(exposedEvent);
                exposedEvent = "OnWizardStepChanged";
                this.AddEvent(exposedEvent);
                this.exposedEvents.Add(exposedEvent);
                exposedEvent = "OnWizardFinalizing";
                this.AddEvent(exposedEvent);
                this.exposedEvents.Add(exposedEvent);
                exposedEvent = "OnWizardFinalized";
                this.AddEvent(exposedEvent);
                this.exposedEvents.Add(exposedEvent);
                exposedEvent = "OnGridClientClick";
                this.AddEvent(exposedEvent);
                this.exposedEvents.Add(exposedEvent);
            }

            //Buttons events
            var layoutDef = this._ui.LayoutDefinition;
            foreach (var control in layoutDef.GetLayoutElementsByClass("Button"))
            {
                exposedEvent = control.GetControlName("") + "_Click";
                this.AddEvent(exposedEvent);
                this.exposedEvents.Add(exposedEvent);
            }

            foreach (var control in layoutDef.GetLayoutElementsWithCustomAggregation())
            {
                exposedEvent = "CustomAggregation" + control.GetControlName("").Replace("_", "");
                this.AddEvent(exposedEvent);
                this.exposedEvents.Add(exposedEvent);
            }
        }

        private void FrmAddUiClientEvents_Load(object sender, EventArgs e)
        {

        }

        private void btAddEvents_Click(object sender, EventArgs e)
        {
            if (this._ui != null)
            {
                foreach (var item in this.lstEvents.SelectedItems)
                {
                    this._ui.AddClientEvent((string)item, this.exposedEvents.Contains((string)item));   
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

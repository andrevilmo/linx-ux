using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Linx.BusinessDataModelDesigner;
using Linx.Tools;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Integration;
using Microsoft.VisualStudio.Modeling.Integration.Picker.Hosting;
using EnvDTE;


namespace Linx.BusinessDataModelDesigner.CustomCode
{
    public partial class frmExternalReferenceClass : Form
    {
        bool started = false;
        Dictionary<string, ProjectItem> desiners = new Dictionary<string, ProjectItem>();

        private BusinessDataModelDesignerRoot _designer;
        public BusinessDataModelDesignerRoot Designer
        {
            get
            {
                return _designer;
            }
            set
            {
                if (value != null)
                {
                    _designer = value;                    
                }
            }
        }

        private void PopulateProjectItems()
        {
            desiners = _designer.GetProjectModels(true);
            this.comboProjectItems.Items.Clear();
            this.comboProjectItems.Items.Add(String.Empty);
            this.comboProjectItems.Items.AddRange(desiners.Keys.OrderBy(e => e).ToArray());
            this.comboProjectItems.SelectedIndex = 0;
        }



        private IModelBus modelBus;
        private ModelBusReference modelReference;
        private ModelBusAdapterManager manager;
        public void PopulateClassesFromModel()
        {
            if (this.comboProjectItems.SelectedItem.IsNullOrEmpty())
                return;

            var item = desiners[this.comboProjectItems.SelectedItem.ToString()];
            this.listClasses.Items.Clear();

            // Get ModelBus
            modelBus = Designer.GetModelBus();
            try
            {
                // Get an adapterManager for the target DSL:
                manager = BusinessDataModelDesignerRoot.GetModelBusManager<ModelBusAdapterManager>(modelBus);

                // Create a reference to the target model:
                modelReference = manager.CreateReference(item);

                BusinessDataModelDesignerRoot modelRoot = Designer.GetModelRoot<BusinessDataModelDesignerRoot>(modelReference, manager);
                this.listClasses.Items.AddRange(modelRoot.Types.Where(e => e is ModelClass).Select(e => new ListViewItem(new string[] { e.Name, (e is ReferenceModelClass ? "Yes" : String.Empty) })).OrderBy(e => e.Text).ToArray());
                //Selecting items that exist in current model.
                foreach (ListViewItem classItem in this.listClasses.Items)
                {
                    if (_designer.Types.Any(e => e is ReferenceModelClass && e.Name == classItem.Text))
                    {
                        classItem.Selected = true;
                    }
                }
            }
            catch { }

            this.listClasses.Invalidate();
        }


        private void SelectClass()
        {
            if (this.listClasses.SelectedItems.Count == 0)
            {
                MessageBox.Show("You must select one or more ModelClass items!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Get ModelBus
            List<string> externalClasses = new List<string>();
            foreach (ListViewItem classItem in this.listClasses.SelectedItems)
            {
                if (!classItem.IsNullOrEmpty())
                    externalClasses.Add(classItem.Text);
            }

            Designer.AddExternalReferences(manager, modelReference, externalClasses);

            this.Close();
        }


        #region Constructor

        public frmExternalReferenceClass()
        {
            InitializeComponent();
        }

        #endregion

        private void btApply_Click(object sender, EventArgs e)
        {
            this.SelectClass();
        }

        private void comboProjectItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateClassesFromModel();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmExternalReferenceClass_Activated(object sender, EventArgs e)
        {
            if (!started && _designer != null)
            {
                started = true;
                PopulateProjectItems();
            }
        }

    }
}

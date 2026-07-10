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
using Linx.BusinessModelDesigner;
using Linx.Tools;
using EnvDTE;
using Microsoft.VisualStudio.Modeling.Integration;


namespace Linx.BusinessModelDesigner.CustomCode
{
    public partial class frmFindElement : Form
    {
        bool started = false;
        Dictionary<string, ProjectItem> models = new Dictionary<string, ProjectItem>();
        List<string> classes = new List<string>();
        private Dictionary<string, ProjectItem> _elementsSelection = null;
        public Dictionary<string, ProjectItem> ElementsSelection { get { if (_elementsSelection == null) { _elementsSelection = new Dictionary<string, ProjectItem>(); } return _elementsSelection; } }

        private BusinessModelDesignerRoot _model;
        public BusinessModelDesignerRoot Model
        {
            get
            {
                return _model;
            }
            set
            {
                if (value != null)
                {
                    _model = value;
                }
            }
        }

        private IModelBus modelBus;
        private ModelBusReference modelReference;
        public void PopulateTypes()
        {
            // Get ModelBus
            modelBus = _model.GetModelBus();
            if (modelBus == null)
                return;

            models = _model.GetProjectModels();
            int currentIndex = 0;
            progressModels.Minimum = 0;
            progressModels.Maximum = models.Count;
            foreach (var item in models)
            {
                currentIndex++;
                progressModels.Value = currentIndex;
                Application.DoEvents();
                try
                {
                    // Get an adapterManager for the target DSL:
                    ModelBusAdapterManager manager = modelBus.FindAdapterManagers(item.Value).First();

                    // Create a reference to the target model:
                    modelReference = manager.CreateReference(item.Value);
                    BusinessModelDesignerRoot modelRoot = Model.GetModelRoot<BusinessModelDesignerRoot>(modelReference);

                    //Adding classes
                    classes.AddRange(modelRoot.Types.Where(e => e is ModelClass || e is DomainView).Select(e => e.Name + "," + (e is ReferenceModelClass ? "Yes" : "No") + "," + modelRoot.DocumentName + "," + modelRoot.DocumentPath + "," + (e is ReferenceModelClass && ((ReferenceModelClass)e).HasReferenceError ? "Yes" : "No") + "," + (e is DomainView ? "D" : "C")).ToArray());
                }
                catch { }
            }
        }

        public void FillList()
        {
            this.listClasses.Items.Clear();

            //Filtering classes
            string filter = this.txtSearch.Text.ToLower();
            var query = classes.Select(e => e.Split(new char[] { ',' })).Where(e => (ckOriginClasses.Checked || ckReferenceClasses.Checked || ckBrokenLink.Checked ? e[5] == "C" : e[5] == "D"));

            if (ckOriginClasses.Checked)
            {
                query = query.Where(e => e[1] == "No");
            }
            if (ckReferenceClasses.Checked)
            {
                query = query.Where(e => e[1] == "Yes");
            }
            if (ckBrokenLink.Checked)
            {
                query = query.Where(e => e[1] == "Yes" && e[4] == "Yes");
            }
            if (!filter.IsNullOrEmpty())
            {
                query = query.Where(e => e[0].ToLower().Contains(filter));
            }

            //Adding classes
            this.listClasses.Items.AddRange(query.OrderBy(e => e[0]).Select(e => new ListViewItem(new string[] { e[0], e[1], e[2], e[3] })).ToArray());

            if (this.listClasses.Items.Count > 0)
                this.listClasses.Items[0].Selected = true;

            this.listClasses.Invalidate();
            this.txtSearch.Focus();
        }

        #region Constructor

        public frmFindElement()
        {
            InitializeComponent();
        }

        #endregion

        #region Events

        private void listClasses_DoubleClick(object sender, EventArgs e)
        {
            Apply();
        }

        private void Apply()
        {
            for (var idx = 0; idx < this.listClasses.SelectedItems.Count; idx++)
            {
                var elementSelection = this.listClasses.SelectedItems[idx];
                string key = (elementSelection.SubItems[2].Text + "  -  " + elementSelection.SubItems[3].Text).ToUpper();
                if (models.ContainsKey(key))
                    this.ElementsSelection.Add(idx.ToString() + "###" + elementSelection.Text, models[key]);

            }
            this.Close();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            this.FillList();
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Apply();
            }
        }

        private void frmFindElement_Activated(object sender, EventArgs e)
        {
            if (!started && _model != null)
            {
                this.txtSearch.Enabled = false;
                this.ckBrokenLink.Enabled = false;
                this.ckOriginClasses.Enabled = false;
                this.ckReferenceClasses.Enabled = false;
                this.ckDomains.Enabled = false;
                started = true;
                PopulateTypes();
                FillList();
                this.txtSearch.Enabled = true;
                this.ckBrokenLink.Enabled = true;
                this.ckOriginClasses.Enabled = true;
                this.ckReferenceClasses.Enabled = true;
                this.ckDomains.Enabled = true;
            }
        }

        private void ckCheckedChanged(object sender, EventArgs e)
        {
            this.FillList();
        }

        #endregion

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            this.Apply();
        }
    }
}

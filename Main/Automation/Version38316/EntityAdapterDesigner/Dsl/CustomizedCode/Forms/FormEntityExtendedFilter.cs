using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Linx.EntityAdapterDesigner.CustomizedCode
{
    public partial class FormEntityExtendedFilter : Form
    {
        private EntityAdapterExtendedFilter _extendedFilter;
        public EntityAdapterExtendedFilter ExtendedFilter { 
            get { return _extendedFilter; } 
            set
            {
                _extendedFilter = value;
                if (_extendedFilter != null)
                {
                    this.Text = "Extended Filter (" + _extendedFilter.EntityName + ")";
                    entityAdapterExtendedFilterBindingSource.DataSource = _extendedFilter;
                }
            }}

        public FormEntityExtendedFilter()
        {
            InitializeComponent();
        }
        
        private void FormEntityExtendedFilter_FormClosing(object sender, FormClosingEventArgs e)
        {
            displayNameTextBox.Select();
        }
    }
}

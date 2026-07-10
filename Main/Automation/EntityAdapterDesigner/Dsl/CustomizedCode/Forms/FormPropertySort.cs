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
    public partial class FormPropertySort : Form
    {

        public FormPropertySort()
        {
            InitializeComponent();
        }


        EntityAdapter _entity;
        public EntityAdapter Entity 
        {
            get { return _entity; }
            set 
            { 
                _entity = value;
                if (_entity != null)
                {
                    foreach (AttributeOrder element in Enum.GetValues(typeof(AttributeOrder)))
                        cmbPropertyOrder.Items.Add(element);
                    cmbPropertyOrder.SelectedItem = _entity.PropertyOrder;
                }
            }
        }

        private void btnApplyOrder_Click(object sender, EventArgs e)
        {
            this.Close();
            if (_entity != null)
            {
                _entity.PropertyOrder = (AttributeOrder)cmbPropertyOrder.SelectedItem;
                _entity.SetPropertyOrder();
            }
        }

       
        
    }
}

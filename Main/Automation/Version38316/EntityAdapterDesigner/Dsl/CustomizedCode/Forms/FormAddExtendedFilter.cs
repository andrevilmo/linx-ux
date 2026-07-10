using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Linx.EntityAdapterDesigner.CustomCode
{
    public partial class FormAddExtendedFilter : Form
    {
        public bool IsAddNew { get; set; }
        private EntityAdapter _entity;
        public EntityAdapter Entity
        {
            get { return _entity; }
            set
            {
                _entity = value;
                if (_entity != null)
                {
                    CustomCode.FrmEntityBuilder builder = new CustomCode.FrmEntityBuilder();
                    builder.Entity = _entity;
                    List<EntityAdapterExtendedFilter2> extList = new List<EntityAdapterExtendedFilter2>();
                    builder.GetOutLinqExtendedFilters(builder.GetTreeView().Nodes, extList);
                    entityAdapterExtendedFilterBindingSource.DataSource = extList;
                }
            }
        }
        
        public FormAddExtendedFilter()
        {
            InitializeComponent();
        }
       
       

        private void FormEntityExtendedFilter_FormClosing(object sender, FormClosingEventArgs e)
        {
            entityAdapterExtendedFilterDataGridView.Select();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
            foreach (DataGridViewRow row in entityAdapterExtendedFilterDataGridView.SelectedRows)
            {
                if (row.DataBoundItem is EntityAdapterExtendedFilter2)
                {
                    if (this.Entity.EntityAdapterExtendedFilters.Where(ex => ex.Name == ((EntityAdapterExtendedFilter2)row.DataBoundItem).Name).Count() == 0)
                    {
                        EntityAdapterExtendedFilter2 copy = ((EntityAdapterExtendedFilter2)row.DataBoundItem);
                        EntityAdapterExtendedFilter filter = (EntityAdapterExtendedFilter)this.Entity.EntityAdapterExtendedFilters.AddNew();

                        filter.Name = copy.Name;
                        filter.DisplayName = copy.DisplayName;
                        filter.EntityName = copy.EntityName;
                        filter.IsUsedInTheLinq = copy.IsUsedInTheLinq;
                        filter.RelationName = copy.RelationName;
                        foreach (var prop in copy.EntityAdapterPropertyExtendedFilters)
                        {
                            EntityAdapterPropertyExtendedFilter propFilter = (EntityAdapterPropertyExtendedFilter)filter.EntityAdapterPropertyExtendedFilters.AddNew();
                            propFilter.Name = prop.Name;
                            propFilter.DisplayName = prop.DisplayName;
                            propFilter.DataType = prop.DataType;
                            propFilter.IsEnabled = prop.IsEnabled;
                            propFilter.EdmKey = prop.EdmKey;
                        }
                    }
                }
            }

            //Adjust order
            int order = 0;
            foreach (var element in this.Entity.EntityAdapterExtendedFilters.OrderBy(ex => ex.DisplayName))
            {
                this.Entity.EntityAdapterExtendedFilters.Move(element, order);
                order++;
            }
        }

    }
}

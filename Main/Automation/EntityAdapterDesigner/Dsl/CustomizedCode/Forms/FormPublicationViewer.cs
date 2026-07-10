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
    public partial class FormPublicationViewer : Form
    {
        private PublicationStructure dataStructure;
        public PublicationStructure DataStructure
        {
            get
            {
                return dataStructure;
            }
            set
            {
                dataStructure = value;
                this.entitiesBindingSource.DataSource = dataStructure.Entities;
                this.domainsBindingSource.DataSource = dataStructure.Domains;
                this.kpisBindingSource.DataSource = dataStructure.Kpis;
            }
        }

        public FormPublicationViewer()
        {
            InitializeComponent();
        }
    }
}

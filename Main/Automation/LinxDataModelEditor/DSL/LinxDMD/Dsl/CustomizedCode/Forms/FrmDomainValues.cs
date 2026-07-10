using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Linx.BusinessDataModelDesigner.CustomCode
{
    public partial class FrmDomainValues : Form
    {
        List<SingleDomainValue> domainValues = new List<SingleDomainValue>();
        DomainView domainView;

        public FrmDomainValues(DomainView domainView)
            : this()
        {
            this.domainView = domainView;
            this.LoadValues();
            singleDomainValueBindingSource.DataSource = domainValues;
        }

        public void LoadValues()
        {
            domainValues.Clear();
            if (domainView != null)
            {
                foreach (var dValue in domainView.DomainValues)
                {
                    domainValues.Add(new SingleDomainValue() { Value = dValue.Value, Name = dValue.Name, DisplayName = dValue.DisplayName });
                }
            }
        }

        public FrmDomainValues()
        {
            InitializeComponent();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btApply_Click(object sender, EventArgs e)
        {
            if (domainView != null)
            {
                using (Microsoft.VisualStudio.Modeling.Transaction transaction =
                            domainView.Store.TransactionManager.BeginTransaction("Change designer by domain values."))
                {
                    domainView.DomainValues.Clear();
                    foreach (var item in domainValues)
                    {
                        domainView.DomainValues.Add(new DomainValue(domainView.Store) { Value = item.Value, Name = item.Name, DisplayName = item.DisplayName });
                    }
                    transaction.Commit();
                }
            }

            this.Close();
        }
    }


    public class SingleDomainValue
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string DisplayName { get; set; }
    }
}

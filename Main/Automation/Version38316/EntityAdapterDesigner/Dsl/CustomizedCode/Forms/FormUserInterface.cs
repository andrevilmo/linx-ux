using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Linx.Tools;
using Microsoft.VisualStudio.Modeling;

namespace Linx.EntityAdapterDesigner.CustomizedCode
{
    public partial class FormUserInterface : Form
    {
        public FormUserInterface()
        {
            InitializeComponent();
        }

        bool isOk = false;
        EntityAdapterUserInterface _userInterface;
        public EntityAdapterUserInterface UserInterface
        {
            get { return _userInterface; }
            set
            {
                _userInterface = value;
                if (_userInterface != null)
                {
                    _userInterface.HasPendingChanges = true;
                    this.Text = _userInterface.Name;
                    this.customizingLayout1.ShowLayout(_userInterface);
                }
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            isOk = true;
            this.Close();
        }

        private void btApply_Click(object sender, EventArgs e)
        {
            if (this.customizingLayout1.StoreCurrentlayout(false))
            {
                //Force changes
                using (Transaction transaction =
                            this.UserInterface.Store.TransactionManager.BeginTransaction("Changing UI."))
                {
                    transaction.Commit();
                }
                
                isOk = true;
                this.Close();
            }
        }

        private void FormUserInterface_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isOk)
            {
                if (MessageBox.Show("Do you really want to cancel all changes?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
        
    }
}

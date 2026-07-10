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
using Linx.Tools;


namespace Linx.Tools.Automation
{
    public partial class StringPropertyEditor : Form
    {

        public string Selection { get; set; }

        private string _textValue;
        public string TextValue
        {
            get
            {
                return _textValue;
            }
            set
            {
                if (value != null)
                {
                    _textValue = value;
                }
            }
        }


        #region Constructor

        public StringPropertyEditor()
        {
            InitializeComponent();
        }

        #endregion

        #region Events

        private void lstClasses_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Apply();
        }

        private void Apply()
        {
            TextValue = this.editor.Text;
            this.Close();
        }

        private void StringPropertyEditor_Activated(object sender, EventArgs e)
        {
            this.editor.Text = TextValue;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btApply_Click(object sender, EventArgs e)
        {
            Apply();
        }

        #endregion
    }
}

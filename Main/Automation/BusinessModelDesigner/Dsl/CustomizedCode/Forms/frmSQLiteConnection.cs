using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Linx.BusinessModelDesigner;
using Linx.Tools;
using System.Data.SQLite;
using System.IO;


namespace Linx.BusinessModelDesigner.CustomizedCode.Forms
{
    public partial class frmSQLiteConnection : Form, IFormOK
    {

        public bool Ok { get; set; }

        private DbProvider _provider;
        public DbProvider Provider
        {
            get
            {
                return _provider;
            }
            set
            {
                if (value != null)
                {
                    _provider = value;
                    try
                    {
                        var paths = Provider.Server.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                        foreach (var path in paths)
                        {
                            if (path.ToLower().Contains("version"))
                                this.VersionNumber.Value = int.Parse(path.Right("="));
                            else
                                if (path.ToLower().Contains("failifmissing"))
                                    this.FailIfMissingCheckBox.Checked = bool.Parse(path.Right("="));
                                else
                                    this.PathComboBox.Text = path.Contains("=") ? path.Right("=") : paths[0];

                        }
                    }
                    catch { }
                }
            }
        }



        #region Constructor

        public frmSQLiteConnection()
        {
            InitializeComponent();
        }

        #endregion

        #region Events





        private void TestButton_Click(object sender, EventArgs e)
        {
            if (TestConnection())
                MessageBox.Show("Connection Successful!");
        }

        private bool TestConnection()
        {
            bool testResult = false;
            SQLiteConnection conn = null;

            try
            {
                string connStr = GetConnString();
                if (File.Exists(this.PathComboBox.Text))
                {
                    conn = new SQLiteConnection(connStr);
                    conn.Open();
                }
                testResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }

            return testResult;
        }

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            this.Ok = TestConnection();
            if (this.Ok)
                this.Close();
        }

        #endregion

        #region Auxiliars methods

        private void SetDbProvider()
        {
            if (string.IsNullOrWhiteSpace(this.PathComboBox.Text))
                throw new Exception("File not informed.");
            if (this.FailIfMissingCheckBox.Checked && !File.Exists(this.PathComboBox.Text))
                throw new Exception("File not exists.");

            _provider.Server = string.Format("{0};Version={1};FailIfMissing={2}", this.PathComboBox.Text, this.VersionNumber.Value, this.FailIfMissingCheckBox.Checked.ToString());

        }

        public string GetConnString()
        {
            this.SetDbProvider();

            return "Data Source=" + _provider.Server;
        }




        private void ExecuteAction(Action<SQLiteConnection> action)
        {
            SQLiteConnection conn = null;
            try
            {
                conn = new SQLiteConnection(GetConnString());

                conn.Open();

                action(conn);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        #endregion

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.PathComboBox.Text = openFileDialog1.FileName;
            }
        }
    }
}

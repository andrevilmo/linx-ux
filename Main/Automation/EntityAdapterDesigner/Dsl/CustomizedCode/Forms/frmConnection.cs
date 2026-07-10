using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Linx.Tools;
using Microsoft.AnalysisServices.AdomdClient;

namespace Linx.EntityAdapterDesigner.CustomCode
{
    public partial class frmConnection : Form
    {

        public bool Ok { get; set; }

        private OlapCatalog _catalog;
        public OlapCatalog Catalog 
        { 
            get
            { 
                return _catalog; 
            }
            set
            {
                if (value != null)
                {
                    _catalog = value;
                    this.ServerTextBox.Text = _catalog.Server;
                    if (!_catalog.Catalog.IsNullOrEmpty())
                    {
                        this.CatalogComboBox.Items.Add( _catalog.Catalog );
                        this.CatalogComboBox.SelectedItem = _catalog.Catalog;
                    }
                    UserIdTextBox.Text = _catalog.UserId;
                    this.PasswordTextBox.Text = _catalog.Password;
                    this.WindowsAuthenticationCheckBox.Checked = _catalog.WindowsAuthentication;
                }
            }
        }



        #region Constructor

        public frmConnection()
        {
            InitializeComponent();
        }

        #endregion

        #region Events

        private void ConnectionForm_Load(object sender, EventArgs e)
        {
            this.WindowsAuthenticationCheckBox_CheckedChanged(this, null);
        }

        private void CatalogComboBox_Enter(object sender, EventArgs e)
        {
            GetCatalogs();
        }

        private void WindowsAuthenticationCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UserIdTextBox.Enabled = !WindowsAuthenticationCheckBox.Checked;
            PasswordTextBox.Enabled = !WindowsAuthenticationCheckBox.Checked;
        }

        private void TestButton_Click(object sender, EventArgs e)
        {            
            if (TestConnection())
                MessageBox.Show("Connection Successful!");
        }

        private bool TestConnection()
        {
            SetOlapCatalog();
            bool testResult = false;
            AdomdConnection conn = null;
            try
            {
                conn = new AdomdConnection(GetConnString());

                conn.Open();

                testResult = true;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
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

        private void SetOlapCatalog()
        {
            _catalog.Server = this.ServerTextBox.Text;
            _catalog.Catalog = this.CatalogComboBox.SelectedItem as string;
            _catalog.UserId = this.UserIdTextBox.Text;
            _catalog.Password = this.PasswordTextBox.Text;
            _catalog.WindowsAuthentication = this.WindowsAuthenticationCheckBox.Checked;
            if (!_catalog.Catalog.IsNullOrEmpty())
                _catalog.Name = _catalog.Catalog.Replace(" ", "").Replace("[", "").Replace("]", "");
        }

        public string GetConnString(bool includeCatalog = true)
        {
            if (string.IsNullOrWhiteSpace(ServerTextBox.Text))
                throw new Exception("Server not informed.");
            this.SetOlapCatalog();
            var connection =  _catalog.Connection;
            if (!includeCatalog)
                connection.Catalog = String.Empty;
            return connection.GetConnectionString();
        }
                

        private void GetCatalogs()
        {
            ExecuteAction(cn =>
            {
                var list = new List<string>();
                var cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT [CATALOG_NAME] FROM $system.DBSCHEMA_CATALOGS";
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add((string)reader["CATALOG_NAME"]);
                }

                CatalogComboBox.DataSource = list;
                CatalogComboBox.SelectedItem = this.Catalog.Catalog;
            });
        }

        private void ExecuteAction(Action<AdomdConnection> action)
        {
            AdomdConnection conn = null;
            try
            {
                conn = new AdomdConnection(GetConnString(false));

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
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Linx.BusinessDataModelDesigner;
using Linx.Tools;


namespace Linx.BusinessDataModelDesigner.CustomCode
{
    public partial class frmMySqlConnection : Form, IFormOK
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
                    this.ServerTextBox.Text = _provider.Server;
                    if (!_provider.Catalog.IsNullOrEmpty())
                    {
                        this.CatalogComboBox.Items.Add( _provider.Catalog );
                        this.CatalogComboBox.SelectedItem = _provider.Catalog;
                    }
                    UserIdTextBox.Text = _provider.UserId;
                    this.PasswordTextBox.Text = _provider.Password;
                }
            }
        }



        #region Constructor
        public frmMySqlConnection()
        {
            InitializeComponent();
        }
        #endregion

        #region Events

        private void ConnectionForm_Load(object sender, EventArgs e)
        {
        }
              

        private void CatalogComboBox_Enter(object sender, EventArgs e)
        {
            GetCatalogs();
        }

  

        private void TestButton_Click(object sender, EventArgs e)
        {            
            if (TestConnection())
                MessageBox.Show("Connection Successful!");
        }

        private bool TestConnection()
        {
            SetDbProvider();
            bool testResult = false;
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(GetConnString());

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

        private void SetDbProvider()
        {
            _provider.Server = this.ServerTextBox.Text;
            _provider.Catalog = this.CatalogComboBox.SelectedItem as string;
            _provider.UserId = this.UserIdTextBox.Text;
            _provider.Password = this.PasswordTextBox.Text;
            _provider.WindowsAuthentication = false;
        }

        public string GetConnString(bool includeCatalog = true)
        {
            if (string.IsNullOrWhiteSpace(ServerTextBox.Text))
                throw new Exception("Server not informed.");
            this.SetDbProvider();
            var connection =  _provider.ProviderConnection;
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
                cmd.CommandText = "select SCHEMA_NAME from information_schema.schemata where SCHEMA_NAME not in ('information_schema','mysql','performance_schema', 'sys');";
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add((string)reader["SCHEMA_NAME"]);
                }

                CatalogComboBox.DataSource = list;
                CatalogComboBox.SelectedItem = this.Provider.Catalog;
            });
        }

        private void ExecuteAction(Action<MySqlConnection> action)
        {
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(GetConnString(false));

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

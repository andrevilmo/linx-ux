using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Linx.BusinessDataModelDesigner.CustomCode;
using Linx.Tools;
using Microsoft.VisualStudio.Modeling;

namespace Linx.BusinessDataModelDesigner
{
    public partial class DbProvider
    {

        private ConnectionManager _providerConnection = null;
        public ConnectionManager ProviderConnection
        {
            get
            {
                if (_providerConnection == null)
                {
                    _providerConnection = new ConnectionManager() { Name = this.ConnectionName, Catalog = this.Catalog, Password = this.Password, Server = this.Server, UserId = this.UserId, WindowsAuthentication = this.WindowsAuthentication };
                }
                else if (_providerConnection.Name != this.ConnectionName || _providerConnection.Catalog != this.Catalog || _providerConnection.Password != this.Password || _providerConnection.Server != this.Server || _providerConnection.UserId != this.UserId || _providerConnection.WindowsAuthentication != this.WindowsAuthentication)
                {
                    _providerConnection.Name = this.ConnectionName;
                    _providerConnection.Catalog = this.Catalog;
                    _providerConnection.Password = this.Password;
                    _providerConnection.Server = this.Server;
                    _providerConnection.UserId = this.UserId;
                    _providerConnection.WindowsAuthentication = this.WindowsAuthentication;
                }
                return _providerConnection;
            }
        }

        public void SetDefaults()
        {
            switch (this.Type)
            {
                case Provider.SQLServer:
                    this.Server = @"localhost\SQLEXPRESS";
                    this.UserId = String.Empty;
                    this.Password = String.Empty;
                    this.Catalog = "model";
                    this.WindowsAuthentication = true;
                    break;                
                case Provider.MySQL:
                    this.Server = "localhost";
                    this.UserId = "root";
                    this.Password = String.Empty;
                    this.Catalog = "sakila";
                    this.WindowsAuthentication = false;
                    break;
                case Provider.SQLite:
                    this.Server = @"c:\temp\database.db;Version=3;FailIfMissing=False";
                    this.UserId = "";
                    this.Password = "";
                    this.Catalog = "";
                    this.WindowsAuthentication = false;
                    break;
                case Provider.PostgreSQL:
                    this.Server = "localhost";
                    this.UserId = "postgres";
                    this.Password = String.Empty;
                    this.Catalog = "postgres";
                    this.WindowsAuthentication = false;
                    break;
                default:
                    break;
            }
        }
        
        public bool IsValidConnection()
        {
            return !this.Server.IsNullOrEmpty() && !this.Catalog.IsNullOrEmpty();
        }

        public string GetConnectionString()
        {
            return this.ProviderConnection.GetConnectionString();
        }

        public void Config()
        {
            
            using (Transaction transaction =
                           this.Store.TransactionManager.BeginTransaction("Changing DbContext."))
            {
                Form preview = null;
                switch (this.Type)
                {
                    case Provider.SQLServer:
                        preview = new frmSqlServerConnection() { Provider = this };
                        break;
                    case Provider.SQLite:
                        preview = new frmSQLiteConnection() { Provider = this };
                        break;
                    case Provider.MySQL:
                        preview = new frmMySqlConnection() { Provider = this };
                        break;
                    case Provider.PostgreSQL:
                        preview = new frmPostgreSqlConnection() { Provider = this };
                        break;
                    default:
                        throw new NotImplementedException(string.Format("Not implemented {0} connection", this.Type));
                }
                preview.ShowDialog();
                if (((IFormOK)preview).Ok)
                    transaction.Commit();
                else
                    transaction.Rollback();
            }
        }

    }
}

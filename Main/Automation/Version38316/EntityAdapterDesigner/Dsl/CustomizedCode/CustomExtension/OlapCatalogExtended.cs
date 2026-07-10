using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnvDTE;
using System.IO;
using Linx.Tools;
using Linx.Builder.Resources;
using System.CodeDom;
using System.Windows.Forms;
using Microsoft.VisualStudio.Modeling;
using System.Xml;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using Linx.EntityAdapterDesigner.CustomCode;

namespace Linx.EntityAdapterDesigner
{
    public partial class OlapCatalog
    {
        private ConnectionManager _connection = null;
        public ConnectionManager Connection { 
            get 
            {
                if (_connection == null)
                {
                    _connection = new ConnectionManager() { Name = this.Name, Catalog = this.Catalog, Password = this.Password, Server = this.Server, UserId = this.UserId, WindowsAuthentication = this.WindowsAuthentication };
                }
                else if (_connection.Name != this.Name || _connection.Catalog != this.Catalog || _connection.Password != this.Password || _connection.Server != this.Server || _connection.UserId != this.UserId || _connection.WindowsAuthentication != this.WindowsAuthentication)
                {
                    _connection.Name = this.Name;
                    _connection.Catalog = this.Catalog; 
                    _connection.Password = this.Password;
                    _connection.Server = this.Server; 
                    _connection.UserId = this.UserId;
                    _connection.WindowsAuthentication = this.WindowsAuthentication;
                }
                return _connection;
            } 
        }

        public void Config()
        {
            using (Transaction transaction =
                           this.Store.TransactionManager.BeginTransaction("Changing OlapCatalog."))
            {
                frmConnection preview = new frmConnection() { Catalog = this };
                preview.ShowDialog();
                if (preview.Ok)
                    transaction.Commit();
                else
                    transaction.Rollback();
            }
        }
    }

}

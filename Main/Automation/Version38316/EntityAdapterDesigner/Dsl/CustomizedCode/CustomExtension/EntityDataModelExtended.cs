using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Linx.Tools;
using Linx.Builder.Resources;
using System.CodeDom;
using System.Windows.Forms;
using System.Configuration;
using System.Reflection;
using Microsoft.VisualStudio.Modeling;
using Linx.EntityAdapterDesigner.CustomizedCode;

namespace Linx.EntityAdapterDesigner
{
    public partial class EntityDataModel
    {
        #region EdmInfo

        private EdmReader _edmInfo;
        public EdmReader EdmInfo
        {
            get
            {
                if (_edmInfo == null)
                    LoadEdmInformation();
                return _edmInfo;
            }
        }

        #endregion
        
        public string GetContextTypeValue()
        {
            if (this.EdmInfo != null && File.Exists(this.Path))
                return (this.EdmInfo.EdmType == EdmReader.EdmTypeEnum.DbContext ? "BMD" : "EDMX");
            else
                return "ERROR";
        }

        public string GetConnectionString()
        {
            string connectionString;

            try
            {
                Configuration cfgFile = ConfigurationManager.OpenExeConfiguration(this.Path);
                if (cfgFile.ConnectionStrings.ConnectionStrings.Count > 0 && !this.ConnectionName.IsNullOrEmpty())
                {
                    connectionString = cfgFile.ConnectionStrings.ConnectionStrings[this.ConnectionName].ConnectionString;
                    connectionString = connectionString.Replace("res://*/", "res://" + System.IO.Path.GetFileNameWithoutExtension(this.Path) + "/");
                }
                else
                    connectionString = String.Empty;
            }
            catch
            {
                connectionString = String.Empty;
            }

            return connectionString;

        }

        public string GetProviderName()
        {
            string providerName;

            try
            {
                Configuration cfgFile = ConfigurationManager.OpenExeConfiguration(this.Path);
                if (cfgFile.ConnectionStrings.ConnectionStrings.Count > 0 && !this.ConnectionName.IsNullOrEmpty())
                {
                    providerName = cfgFile.ConnectionStrings.ConnectionStrings[this.ConnectionName].ProviderName;
                }
                else
                    providerName = "System.Data.SqlClient";
            }
            catch
            {
                providerName = "System.Data.SqlClient";
            }

            return providerName;
        }

        private void RemoveEdmReference()
        {
            if (!this.Path.IsNullOrEmpty())
            {
                this.EntityAdapterDesignerRoot.RemoveReference(this.Path);
                this.Path = String.Empty;
                this.TargetNamespace = String.Empty;
                this.ConnectionName = String.Empty;
            }
        }

        public void AddNewEdmReference(bool replace = false)
        {
            if (this.Path.IsNullOrEmpty() || replace)
            {
                OpenFileDialog fileDlg = new OpenFileDialog();
                fileDlg.CheckFileExists = true;
                fileDlg.FileName = "";
                fileDlg.Filter = "Assembly only|*.dll";
                fileDlg.Title = "Select DataContext assembly file";

                if (fileDlg.ShowDialog() == DialogResult.OK)
                {
                    if (!File.Exists(fileDlg.FileName))
                        throw new Exception("DataContext file does not exists!!!");
                    else if (System.IO.Path.GetExtension(fileDlg.FileName).ToLower() != ".dll")
                        throw new Exception("DataContext extension is invalid!!!");
                }
                else
                    throw new Exception("Operation cancelled!!!");

                using (Transaction transaction =
                           this.Store.TransactionManager.BeginTransaction("Changing Edm file path."))
                {
                    this.RemoveEdmReference();
                    this.Path = fileDlg.FileName;
                    this.SetEdmConfiguration();
                    this.EntityAdapterDesignerRoot.AddReference(this.Path, true);
                    transaction.Commit();
                }

            }
        }

        internal void LoadEdmInformation()
        {
            try
            {
                _edmInfo = EdmReader.GetEdmInfo(this.Path);
            }
            catch { }
        }

        private void SetEdmConfiguration()
        {
            if (EdmInfo == null)
                return;

            string connectionName = string.Empty;
            //Meta Data Analysis

            string oldEdmRef = EdmInfo.TargetNamespace + "." + EdmInfo.ContextName;

            LoadEdmInformation();

            Configuration cfgFile = ConfigurationManager.OpenExeConfiguration(this.Path);
            if (cfgFile.ConnectionStrings.ConnectionStrings.Count > 0)
                connectionName = cfgFile.ConnectionStrings.ConnectionStrings[cfgFile.ConnectionStrings.ConnectionStrings.Count - 1].Name;

            string newEdmRef = EdmInfo.TargetNamespace + "." + EdmInfo.ContextName;

            this.Name = EdmInfo.ContextName;
            this.TargetNamespace = EdmInfo.TargetNamespace;
            this.ConnectionName = connectionName;


            //Update new edm references
            foreach (var entity in this.EntityAdapters)
            {
                foreach (var evt in entity.EntityAdapterEvents.Where(e => e.Parameters.Contains(oldEdmRef)))
                {
                    evt.Parameters = evt.Parameters.Replace(oldEdmRef, newEdmRef);
                }

                foreach (var op in entity.EntityAdapterOperations.Where(e => e.Parameters.Contains(oldEdmRef)))
                {
                    op.Parameters = op.Parameters.Replace(oldEdmRef, newEdmRef);
                }
            }

        }
    }
}

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
        public string ErrorMessage { get; set; }

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
            {
                return "BM";
            }
            else
            {
                return this.ErrorMessage;
            }
        }

        public bool GetHasErrorValue()
        {
            return !this.ErrorMessage.IsNullOrEmpty();
        }

        public string GetJsonModelConnectionString(string indent, string connectionStrings = "")
        {
            string bmSettingsPath = this.EntityAdapterDesignerRoot.GetFullPath("Linx.CoreBusinessModels");
            string bmPath = System.IO.Path.Combine(bmSettingsPath, System.IO.Path.GetFileName(this.Path) + ".json");

            if (!bmPath.IsNullOrEmpty() && !File.Exists(bmPath))
                bmPath = System.IO.Path.Combine(bmSettingsPath, System.IO.Path.GetFileNameWithoutExtension(this.Path) + ".Core.dll.json");

            if (!bmPath.IsNullOrEmpty() && File.Exists(bmPath))
            {
                string connectionString = SerializationManager.GetJsonConnectionString(bmPath, this.ConnectionName).Replace("\\", "\\\\").Replace("\"", "&quot;");
                if (!connectionString.IsNullOrEmpty())
                    connectionStrings += indent + (connectionStrings.IsNullOrEmpty() ? "" : ", ") + "\"" + this.ConnectionName + "\": \"" + connectionString + "\"";
            }
            return connectionStrings;
        }

        public string GetConnectionString()
        {
            string connectionString;

            try
            {
                Configuration cfgFile = ConfigurationManager.OpenExeConfiguration(this.Path);
                if (cfgFile.ConnectionStrings.ConnectionStrings.Count > 0 && !this.ConnectionName.IsNullOrEmpty() && cfgFile.ConnectionStrings.ConnectionStrings[this.ConnectionName] != null)
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
                string dirName = (this.EntityAdapterDesignerRoot.IsAspNetCore ? "Linx.CoreBusinessModels" : "Linx.Business.Models");
                fileDlg.InitialDirectory = this.EntityAdapterDesignerRoot.GetFullPath(dirName);

                if (fileDlg.ShowDialog() == DialogResult.OK)
                {
                    if (!File.Exists(fileDlg.FileName))
                        throw new Exception("DataContext file does not exists!!!");
                    else if (System.IO.Path.GetExtension(fileDlg.FileName).ToLower() != ".dll")
                        throw new Exception("DataContext extension is invalid!!!");
                }
                else
                    return;

                using (Transaction transaction =
                           this.Store.TransactionManager.BeginTransaction("Changing Edm file path."))
                {
                    this.UpdateContextFile(fileDlg.FileName);
                    transaction.Commit();
                }

            }
        }

        public void UpdateContextFile(string filePath)
        {
            this.RemoveEdmReference();
            this.Path = filePath;
            this.SetEdmConfiguration();
            var eadProj = this.EntityAdapterDesignerRoot.GetEadProject();
            if (this.EntityAdapterDesignerRoot.IsAspNetCore)
                eadProj = this.EntityAdapterDesignerRoot.GetEadCoreProject(eadProj);
            this.EntityAdapterDesignerRoot.AddNewReference(eadProj, this.Path, true);
        }

        internal void LoadEdmInformation()
        {
            this.ErrorMessage = "";
            try
            {
                _edmInfo = EdmReader.GetEdmInfo(this.Path);
            }
            catch (Exception excep)
            {
                this.ErrorMessage = excep.Message;                
            }
        }

        private void SetEdmConfiguration()
        {
            if (EdmInfo == null)
                return;

            //Meta Data Analysis

            string oldEdmRef = EdmInfo.TargetNamespace + "." + EdmInfo.ContextName;

            LoadEdmInformation();
            
            string newEdmRef = EdmInfo.TargetNamespace + "." + EdmInfo.ContextName;

            this.Name = EdmInfo.ContextName;
            this.TargetNamespace = EdmInfo.TargetNamespace;
            this.ConnectionName = EdmInfo.Metadata?.ConnectionName;


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

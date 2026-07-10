using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.Modeling.Diagrams;
using Microsoft.VisualStudio.Modeling.Validation;
using Microsoft.VisualStudio.Modeling;
using System.Globalization;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using Linx.Tools;
using System.Linq;

namespace Linx.EntityAdapterDesigner
{
    [RuleOn(typeof(EntityDataModel), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class EdmStartConfiguration : AddRule
    {
        public override void ElementAdded(ElementAddedEventArgs e)
        {
            EntityDataModel edm = e.ModelElement as EntityDataModel;
            
            if (edm != null && edm.Path.IsNullOrEmpty())
            {
                OpenFileDialog fileDlg = new OpenFileDialog();
                fileDlg.CheckFileExists = true;
                fileDlg.FileName = "*.dll";
                fileDlg.Title = "Select the EDM assembly file";

                if (fileDlg.ShowDialog() == DialogResult.OK)
                {
                    if (!File.Exists(fileDlg.FileName))
                        throw new Exception("Edm file does not exists!!!");
                    else if (Path.GetExtension(fileDlg.FileName).ToLower() != ".dll")
                        throw new Exception("Edm extension is invalid!!!");
                }
                else
                    throw new Exception("Operation cancelled!!!");

                edm.Path = fileDlg.FileName;
                this.SetEdmConfiguration(edm);
                edm.EntityAdapterDesignerRoot.AddReference(edm.Path);
            }
            base.ElementAdded(e);
        }

        private void SetEdmConfiguration(EntityDataModel edmRef)
        {
            //Meta Data Analysis
            Assembly assembly = Assembly.LoadFile(edmRef.Path);
            Type[] types = assembly.GetTypes();

            foreach (Type type in types)
            {
                if (type.BaseType.Name == "ObjectContext")
                {
                    edmRef.Name = type.Name;
                    edmRef.TargetNamespace = (type.FullName.Trim() + ".").Left((type.FullName.Trim() + ".").Length - ("." + type.Name.Trim() + ".").Length);
                    break;
                }
            }
            
        }

    }


    /// <summary>  
    /// /// Domain model class allows extra reflective elements such as rules to be added  
    /// /// </summary>  
    public partial class EntityAdapterDesignerDomainModel
    {      
        protected override Type[] GetCustomDomainModelTypes()
        {
            return new System.Type[] { typeof(EdmStartConfiguration) };
        }
    }
}
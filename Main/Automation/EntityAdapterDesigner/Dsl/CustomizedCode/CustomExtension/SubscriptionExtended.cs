using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnvDTE;
using Linx.Tools;

namespace Linx.EntityAdapterDesigner
{
    partial class Subscription
    {
        public CustomizedCode.PublicationStructure Publisher { get; set; }
        
        public bool GetHasErrorValue()
        {
            return (Publisher == null);
        }

        public void UpdateReferences(Project project)
        {
            if (!project.IsNull())
            {
                if (System.IO.File.Exists(this.BusinessObjectPath))
                {
                    if (System.IO.Path.GetFileNameWithoutExtension(this.BusinessObjectPath) != this.EntityAdapterDesignerRoot.GetAssemblyName(project))
                        this.EntityAdapterDesignerRoot.AddNewReference(project, this.BusinessObjectPath);
                }
            }
        }
                   
        
    }
}

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
        
        public void UpdateReferences(Project project)
        {
            if (!project.IsNull())
            {
                if (System.IO.File.Exists(this.BusinessObjectPath))
                {                   
                    this.EntityAdapterDesignerRoot.AddReference(project, this.BusinessObjectPath);
                }
            }
        }
                   
        
    }
}

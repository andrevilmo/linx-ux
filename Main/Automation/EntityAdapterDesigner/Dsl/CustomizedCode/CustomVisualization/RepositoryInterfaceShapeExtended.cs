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
using System.Collections;

namespace Linx.EntityAdapterDesigner
{
    
    public partial class RepositoryInterfaceShape
    {        
        public override void OnDoubleClick(Microsoft.VisualStudio.Modeling.Diagrams.DiagramPointEventArgs e)
        {
            foreach (var element in e.DiagramHitTestInfo.HitDiagramItem.RepresentedElements)
            {
                if (element is RepositoryMethod)
                {
                    ((RepositoryMethod)element).RepositoryInterface.EntityAdapterDesignerRoot.OpenRepositoryMethod((RepositoryMethod)element);
                }
                else if (element is RepositoryInterfaceShape)
                {
                    ((RepositoryInterface)((RepositoryInterfaceShape)element).ModelElement).EntityAdapterDesignerRoot.OpenCodeElement(((RepositoryInterface)((RepositoryInterfaceShape)element).ModelElement));
                }
                break;
            }
            base.OnDoubleClick(e);
        }

    }

}

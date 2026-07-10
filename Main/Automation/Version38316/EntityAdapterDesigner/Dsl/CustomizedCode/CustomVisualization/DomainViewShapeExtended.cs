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
using System.Collections;

namespace Linx.EntityAdapterDesigner
{
    public partial class DomainViewShape
    {        
        public override void OnDoubleClick(Microsoft.VisualStudio.Modeling.Diagrams.DiagramPointEventArgs e)
        {
            foreach (var element in e.DiagramHitTestInfo.HitDiagramItem.RepresentedElements)
            {
                if (element is DomainViewShape)
                {
                    ((DomainView)((DomainViewShape)element).ModelElement).EntityAdapterDesignerRoot.OpenCodeElement(((DomainViewShape)element).ModelElement);
                }
                break;
            }
            base.OnDoubleClick(e);
        }
    }

}

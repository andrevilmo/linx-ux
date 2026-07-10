using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DslDiagrams = global::Microsoft.VisualStudio.Modeling.Diagrams;

namespace Linx.EntityAdapterDesigner
{

    public partial class WorkflowShape
    {
        public override void OnDoubleClick(DslDiagrams.DiagramPointEventArgs e)
        {
            foreach (var element in e.DiagramHitTestInfo.HitDiagramItem.RepresentedElements)
            {
                if (element is WorkflowShape)
                {
                    ((Workflow)((WorkflowShape)element).ModelElement).EntityAdapterDesignerRoot.OpenCodeElement(((Workflow)((WorkflowShape)element).ModelElement));
                }
                break;
            }
            base.OnDoubleClick(e);
        }
    }
}

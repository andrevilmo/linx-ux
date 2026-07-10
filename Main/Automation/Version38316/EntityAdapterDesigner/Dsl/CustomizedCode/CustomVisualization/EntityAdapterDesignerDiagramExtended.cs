using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DslDiagrams = global::Microsoft.VisualStudio.Modeling.Diagrams;

namespace Linx.EntityAdapterDesigner
{

    public partial class EntityAdapterDesignerDiagram
    {
        protected override void OnAssociated(DslDiagrams.DiagramAssociationEventArgs e)
        {            
            base.OnAssociated(e);
            this.StartStructure();
        }

        public void StartStructure()
        {
            if (this.ModelElement is EntityAdapterDesignerRoot && ((EntityAdapterDesignerRoot)this.ModelElement).DTEReference == null)
            { 
                ((EntityAdapterDesignerRoot)this.ModelElement).DTEReference = this.GetService(typeof(EnvDTE.DTE)) as EnvDTE.DTE;
                ((EntityAdapterDesignerRoot)this.ModelElement).AdjustStructuralInfo();
            }
        }
    
        public override void OnDoubleClick(Microsoft.VisualStudio.Modeling.Diagrams.DiagramPointEventArgs e)
        {
            foreach (var element in e.DiagramHitTestInfo.HitDiagramItem.RepresentedElements)
            {
                if (element is EntityAdapterDesignerDiagram)
                {
                    ((EntityAdapterDesignerRoot)((EntityAdapterDesignerDiagram)element).ModelElement).OpenCodeElement(((EntityAdapterDesignerRoot)((EntityAdapterDesignerDiagram)element).ModelElement));
                }
                break;
            }
            base.OnDoubleClick(e);
        }
    }
}

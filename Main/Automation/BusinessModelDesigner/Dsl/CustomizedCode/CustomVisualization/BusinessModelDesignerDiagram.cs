using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Diagrams;
using DslDiagrams = global::Microsoft.VisualStudio.Modeling.Diagrams;
using Linx.Tools;

namespace Linx.BusinessModelDesigner
{
    public partial class BusinessModelDesignerDiagram
    {

        public override void OnDoubleClick(Microsoft.VisualStudio.Modeling.Diagrams.DiagramPointEventArgs e)
        {
            foreach (var element in e.DiagramHitTestInfo.HitDiagramItem.RepresentedElements)
            {
                if (element is BusinessModelDesignerDiagram)
                {
                    ((BusinessModelDesignerRoot)((BusinessModelDesignerDiagram)element).ModelElement).OpenCodeElement(((BusinessModelDesignerRoot)((BusinessModelDesignerDiagram)element).ModelElement));
                }
                break;
            }
            base.OnDoubleClick(e);
        }
        

        public void ArrangeLayout()
        {
            using (Transaction t = this.Store.TransactionManager.BeginTransaction("Arrange Layout"))
            {
                this.AutoLayoutShapeElements(this.NestedChildShapes);
                t.Commit();
            }
            
        }

        protected override void OnAssociated(DslDiagrams.DiagramAssociationEventArgs e)
        {
            base.OnAssociated(e);
            this.StartStructure();
        }
                
        public void StartStructure()
        {
            if (this.ModelElement is BusinessModelDesignerRoot)
            {
                ((BusinessModelDesignerRoot)this.ModelElement).StartNuGetConsole();
                if (((BusinessModelDesignerRoot)this.ModelElement).DTEReference == null)
                {
                    
                    ((BusinessModelDesignerRoot)this.ModelElement).DTEReference = this.GetService(typeof(EnvDTE.DTE)) as EnvDTE.DTE;
                    ((BusinessModelDesignerRoot)this.ModelElement).AdjustStructuralInfo();
                    ((BusinessModelDesignerRoot)this.ModelElement).Refresh();                    
                }
            }
        }

    }
}

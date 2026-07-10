using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Diagrams;
using DslDiagrams = global::Microsoft.VisualStudio.Modeling.Diagrams;
using Linx.Tools;

namespace Linx.BusinessDataModelDesigner
{
    public partial class BusinessDataModelDesignerDiagram
    {

        public override void OnDoubleClick(Microsoft.VisualStudio.Modeling.Diagrams.DiagramPointEventArgs e)
        {
            foreach (var element in e.DiagramHitTestInfo.HitDiagramItem.RepresentedElements)
            {
                if (element is BusinessDataModelDesignerDiagram)
                {
                    ((BusinessDataModelDesignerRoot)((BusinessDataModelDesignerDiagram)element).ModelElement).OpenCodeElement(((BusinessDataModelDesignerRoot)((BusinessDataModelDesignerDiagram)element).ModelElement));
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
            if (this.ModelElement is BusinessDataModelDesignerRoot)
            {
                ((BusinessDataModelDesignerRoot)this.ModelElement).StartNuGetConsole();
                if (((BusinessDataModelDesignerRoot)this.ModelElement).DTEReference == null)
                {
                    ((BusinessDataModelDesignerRoot)this.ModelElement).DTEReference = this.GetService(typeof(EnvDTE.DTE)) as EnvDTE.DTE;
                    ((BusinessDataModelDesignerRoot)this.ModelElement).AdjustStructuralInfo();
                    ((BusinessDataModelDesignerRoot)this.ModelElement).Refresh();                    
                }
            }
        }

    }
}

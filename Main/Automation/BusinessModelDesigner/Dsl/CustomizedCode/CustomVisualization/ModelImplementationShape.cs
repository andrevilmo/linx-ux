using System.Drawing;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Diagrams;
using System.Linq;
using Linx.Tools;

namespace Linx.BusinessModelDesigner
{
    public partial class ModelImplementationShape
    {
        public override void OnClick(Microsoft.VisualStudio.Modeling.Diagrams.DiagramPointEventArgs e)
        {
            foreach (var element in e.DiagramHitTestInfo.HitDiagramItem.RepresentedElements)
            {
                if (element is ModelImplementationShape)
                {
                    ModelImplementation currentElement = ((ModelImplementation)((ModelImplementationShape)element).ModelElement);
                    if (currentElement.ModelInterface != null && !currentElement.HasFocus)
                    {
                        currentElement.HasFocus = true;
                        foreach (var rep in currentElement.ModelInterface.ModelImplementations.Where(r => r != currentElement))
                        {
                            rep.HasFocus = false;
                        }
                    }
                }
                break;
            }
            base.OnClick(e);
        }

        public override void OnDoubleClick(Microsoft.VisualStudio.Modeling.Diagrams.DiagramPointEventArgs e)
        {
            foreach (var element in e.DiagramHitTestInfo.HitDiagramItem.RepresentedElements)
            {
                if (element is ModelImplementationShape)
                {
                    ((ModelImplementation)((ModelImplementationShape)element).ModelElement).BusinessModelDesignerRoot.OpenCodeElement(((ModelImplementation)((ModelImplementationShape)element).ModelElement));
                }
                break;
            }
            base.OnDoubleClick(e);
        }

    }
}

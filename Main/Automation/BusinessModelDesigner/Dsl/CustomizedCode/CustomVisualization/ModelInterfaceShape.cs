using System.Drawing;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Diagrams;
using System.Linq;
using Linx.Tools;

namespace Linx.BusinessModelDesigner
{
    public partial class InterfaceShape
    {
        public override void OnDoubleClick(Microsoft.VisualStudio.Modeling.Diagrams.DiagramPointEventArgs e)
        {
            foreach (var element in e.DiagramHitTestInfo.HitDiagramItem.RepresentedElements)
            {
                if (element is InterfaceShape)
                {
                    ((ModelInterface)((InterfaceShape)element).ModelElement).BusinessModelDesignerRoot.OpenCodeElement(((InterfaceShape)element).ModelElement);
                }
                else if (element is InterfaceOperation)
                {
                    ((InterfaceOperation)element).Interface.BusinessModelDesignerRoot.OpenImplementationMethod((InterfaceOperation)element);
                }
                break;
            }
            base.OnDoubleClick(e);
        }

    }
}

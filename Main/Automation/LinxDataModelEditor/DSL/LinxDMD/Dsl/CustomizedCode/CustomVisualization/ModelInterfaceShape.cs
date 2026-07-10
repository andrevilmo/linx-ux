using System.Drawing;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Diagrams;
using System.Linq;
using Linx.Tools;

namespace Linx.BusinessDataModelDesigner
{
    public partial class InterfaceShape
    {
        public override void OnDoubleClick(Microsoft.VisualStudio.Modeling.Diagrams.DiagramPointEventArgs e)
        {
            foreach (var element in e.DiagramHitTestInfo.HitDiagramItem.RepresentedElements)
            {
                if (element is InterfaceShape)
                {
                    ((ModelInterface)((InterfaceShape)element).ModelElement).BusinessDataModelDesignerRoot.OpenCodeElement(((InterfaceShape)element).ModelElement);
                }
                else if (element is InterfaceOperation)
                {
                    ((InterfaceOperation)element).Interface.BusinessDataModelDesignerRoot.OpenImplementationMethod((InterfaceOperation)element);
                }
                break;
            }
            base.OnDoubleClick(e);
        }

    }
}

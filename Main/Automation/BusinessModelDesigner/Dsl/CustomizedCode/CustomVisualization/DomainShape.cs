using System.Drawing;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Diagrams;
using System.Linq;
using Linx.Tools;

namespace Linx.BusinessModelDesigner
{
    public partial class DomainViewShape
    {
        public override void OnDoubleClick(Microsoft.VisualStudio.Modeling.Diagrams.DiagramPointEventArgs e)
        {
            foreach (var element in e.DiagramHitTestInfo.HitDiagramItem.RepresentedElements)
            {
                if (element is DomainViewShape)
                {
                    ((DomainView)((DomainViewShape)element).ModelElement).BusinessModelDesignerRoot.OpenCodeElement(((DomainViewShape)element).ModelElement);
                }
                break;
            }
            base.OnDoubleClick(e);
        }

    }
}

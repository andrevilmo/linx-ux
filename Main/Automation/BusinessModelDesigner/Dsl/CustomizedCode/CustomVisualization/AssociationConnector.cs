using System.Drawing;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Diagrams;
using System.Linq;
using Linx.Tools;

namespace Linx.BusinessModelDesigner
{

    public partial class AssociationConnector
    {
        public override void OnMouseDown(DiagramMouseEventArgs e)
        {
            foreach (var element in e.DiagramHitTestInfo.HitDiagramItem.RepresentedElements)
            {
                if (element is AssociationConnector || element is DecoratorHostShape)
                {
                    Association link = ((Association)(element is AssociationConnector ? ((AssociationConnector)element).ModelElement : ((DecoratorHostShape)element).ParentShape.ModelElement));
                    link.SelectProperties();                                   
                    break;
                }
            }

            base.OnMouseDown(e);
        }

        public override void OnMouseUp(DiagramMouseEventArgs e)
        {

            foreach (var element in e.DiagramHitTestInfo.HitDiagramItem.RepresentedElements)
            {
                if (element is AssociationConnector || element is DecoratorHostShape)
                {
                    Association link = ((Association)(element is AssociationConnector ? ((AssociationConnector)element).ModelElement : ((DecoratorHostShape)element).ParentShape.ModelElement));
                    link.TargetModelClass.BusinessModelDesignerRoot.RefreshFocusedDiagramView();
                    break;
                }
            }                       

            base.OnMouseUp(e);
        }


    }
}

using System.Drawing;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Diagrams;
using System.Linq;
using Linx.Tools;

namespace Linx.BusinessDataModelDesigner
{
    public partial class MultipleAssociationRoleConnector
    {
        public override void OnMouseDown(DiagramMouseEventArgs e)
        {
            foreach (var element in e.DiagramHitTestInfo.HitDiagramItem.RepresentedElements)
            {
                if (element is MultipleAssociationRoleConnector || element is DecoratorHostShape)
                {
                    MultipleAssociationOrigin link = ((MultipleAssociationOrigin)(element is MultipleAssociationRoleConnector ? ((MultipleAssociationRoleConnector)element).ModelElement : ((DecoratorHostShape)element).ParentShape.ModelElement));
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
                if (element is MultipleAssociationRoleConnector || element is DecoratorHostShape)
                {
                    MultipleAssociationOrigin link = ((MultipleAssociationOrigin)(element is MultipleAssociationRoleConnector ? ((MultipleAssociationRoleConnector)element).ModelElement : ((DecoratorHostShape)element).ParentShape.ModelElement));
                    link.OriginType.BusinessDataModelDesignerRoot.RefreshFocusedDiagramView();
                    break;
                }
            }                       

            base.OnMouseUp(e);
        }


    }
}

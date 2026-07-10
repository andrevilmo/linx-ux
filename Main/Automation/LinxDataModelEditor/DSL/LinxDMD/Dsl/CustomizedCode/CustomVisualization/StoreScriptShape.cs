using System.Drawing;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Diagrams;
using System.Linq;
using Linx.Tools;

namespace Linx.BusinessDataModelDesigner
{
    public partial class StoreScriptShape
    {
        public override void OnDoubleClick(Microsoft.VisualStudio.Modeling.Diagrams.DiagramPointEventArgs e)
        {
            foreach (var element in e.DiagramHitTestInfo.HitDiagramItem.RepresentedElements)
            {
                if (element is StoreQuery)
                {
                    ((StoreQuery)element).StoreScript.BusinessDataModelDesignerRoot.OpenCodeElement(((StoreQuery)element));
                }
                break;
            }
            base.OnDoubleClick(e);
        }

    }
}

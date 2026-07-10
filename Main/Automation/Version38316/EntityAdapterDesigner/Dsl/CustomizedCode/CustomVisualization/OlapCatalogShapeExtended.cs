using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Linx.EntityAdapterDesigner.CustomCode;
using Microsoft.VisualStudio.Modeling;


namespace Linx.EntityAdapterDesigner
{
    public partial class OlapCatalogShape
    {
        public override void OnDoubleClick(Microsoft.VisualStudio.Modeling.Diagrams.DiagramPointEventArgs e)
        {
            foreach (var element in e.DiagramHitTestInfo.HitDiagramItem.RepresentedElements)
            {
                if (element is OlapCatalogShape)
                {
                    var catalog = ((OlapCatalog)((OlapCatalogShape)element).ModelElement);
                    if (catalog != null)
                    {
                        catalog.Config();
                    }
                }
                break;
            }
            base.OnDoubleClick(e);
        }
    }
}

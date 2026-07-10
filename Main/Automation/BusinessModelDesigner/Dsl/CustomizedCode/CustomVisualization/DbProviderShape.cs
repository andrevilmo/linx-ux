using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Modeling;


namespace Linx.BusinessModelDesigner
{
    public partial class DbProviderShape
    {
        public override void OnDoubleClick(Microsoft.VisualStudio.Modeling.Diagrams.DiagramPointEventArgs e)
        {
            foreach (var element in e.DiagramHitTestInfo.HitDiagramItem.RepresentedElements)
            {
                if (element is DbProviderShape)
                {
                    var catalog = ((DbProvider)((DbProviderShape)element).ModelElement);
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

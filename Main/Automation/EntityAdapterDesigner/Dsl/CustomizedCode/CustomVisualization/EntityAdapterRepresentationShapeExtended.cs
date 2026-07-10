using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Linx.EntityAdapterDesigner.CustomizedCode;

namespace Linx.EntityAdapterDesigner
{
    public partial class EntityAdapterRepresentationShape
    {
        public override void OnDoubleClick(Microsoft.VisualStudio.Modeling.Diagrams.DiagramPointEventArgs e)
        {
            foreach (var element in e.DiagramHitTestInfo.HitDiagramItem.RepresentedElements)
            {
                if (element is EntityAdapterRepresentationShape)
                {
                    var representation = ((EntityAdapterRepresentation)((EntityAdapterRepresentationShape)element).ModelElement);
                    if (representation != null)
                    {
                        FormPublishedEntityList preview = new FormPublishedEntityList() { Entity = representation };
                        preview.ShowDialog();
                    }
                }
                break;
            }
            base.OnDoubleClick(e);
        }
    }
}

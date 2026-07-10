using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnvDTE;
using System.IO;
using Linx.Tools;
using Linx.Builder.Resources;
using System.CodeDom;
using System.Windows.Forms;
using System.Collections;

namespace Linx.EntityAdapterDesigner
{
	public partial class DomainServiceExtensionShape
	{
        private static ArrayList customOutlineDashPattern;
        protected static ArrayList CustomOutlineDashPattern
        {
            get
            {
                if (customOutlineDashPattern == null)
                    customOutlineDashPattern = new ArrayList(new float[] { 4.0F, 2.0F, 1.0F, 3.0F });
                return customOutlineDashPattern;
            }
        }

		public override void OnDoubleClick(Microsoft.VisualStudio.Modeling.Diagrams.DiagramPointEventArgs e)
		{
			foreach (var element in e.DiagramHitTestInfo.HitDiagramItem.RepresentedElements)
			{
				if (element is DomainServiceOperation)
				{
					((DomainServiceOperation)element).DomainServiceExtension.EntityAdapterDesignerRoot.OpenDomainServiceOperation((DomainServiceOperation)element);
				}
				else if (element is DomainServiceExtensionShape)
				{
					((DomainServiceExtension)((DomainServiceExtensionShape)element).ModelElement).EntityAdapterDesignerRoot.OpenCodeElement(((DomainServiceExtension)((DomainServiceExtensionShape)element).ModelElement)); 
				}
				break;
			}
			base.OnDoubleClick(e);
		}

		public override void OnShapeRemoved()
		{
			base.OnShapeRemoved();
			
		}
	}

}

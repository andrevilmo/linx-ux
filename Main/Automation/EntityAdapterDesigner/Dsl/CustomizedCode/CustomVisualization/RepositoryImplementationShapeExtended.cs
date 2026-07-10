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
using Microsoft.VisualStudio.Modeling;
using DslDiagrams = global::Microsoft.VisualStudio.Modeling.Diagrams;

namespace Linx.EntityAdapterDesigner
{   
    
    public partial class RepositoryImplementationShape
	{
        
        public override void OnClick(Microsoft.VisualStudio.Modeling.Diagrams.DiagramPointEventArgs e)
        {
            foreach (var element in e.DiagramHitTestInfo.HitDiagramItem.RepresentedElements)
            {
                if (element is RepositoryImplementationShape)
                {
                    RepositoryImplementation currentElement = ((RepositoryImplementation)((RepositoryImplementationShape)element).ModelElement);
                    if (currentElement.RepositoryInterface != null && !currentElement.HasFocus)
                    {
                        currentElement.HasFocus = true;
                        foreach (var rep in currentElement.RepositoryInterface.RepositoryImplementations.Where(r => r != currentElement))
                        {
                            rep.HasFocus = false;
                        }
                    }
                }
                break;
            }
            base.OnClick(e);
        }
        
		public override void OnDoubleClick(Microsoft.VisualStudio.Modeling.Diagrams.DiagramPointEventArgs e)
		{
			foreach (var element in e.DiagramHitTestInfo.HitDiagramItem.RepresentedElements)
			{
                if (element is RepositoryImplementationShape)
				{
                    ((RepositoryImplementation)((RepositoryImplementationShape)element).ModelElement).EntityAdapterDesignerRoot.OpenCodeElement(((RepositoryImplementation)((RepositoryImplementationShape)element).ModelElement)); 
				}
				break;
			}
			base.OnDoubleClick(e);
		}

	}

}

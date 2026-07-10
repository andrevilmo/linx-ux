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


namespace Linx.BusinessModelDesigner
{
    public partial class WebApiControllerShape
    {

        public override void OnDoubleClick(Microsoft.VisualStudio.Modeling.Diagrams.DiagramPointEventArgs e)
        {
            foreach (var element in e.DiagramHitTestInfo.HitDiagramItem.RepresentedElements)
            {
                if (element is WebApiAction)
                {
                    ((WebApiAction)element).WebApiController.BusinessModelDesignerRoot.OpenWebApiAction((WebApiAction)element);
                }
                else if (element is WebApiControllerShape)
                {
                    ((WebApiController)((WebApiControllerShape)element).ModelElement).BusinessModelDesignerRoot.OpenCodeElement(((WebApiController)((WebApiControllerShape)element).ModelElement));
                }
                break;
            }
            base.OnDoubleClick(e);
        }

    }

}

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
using Microsoft.VisualStudio.Modeling;

namespace Linx.EntityAdapterDesigner
{
       
    public partial class AssociationEntityRepresentationConnector
    {
        public override void OnDoubleClick(Microsoft.VisualStudio.Modeling.Diagrams.DiagramPointEventArgs e)
        {
            foreach (var element in e.DiagramHitTestInfo.HitDiagramItem.RepresentedElements)
            {
                if (element is AssociationEntityRepresentationConnector)
                {
                    var link = ((EntityAdapterRepresentationReferencesTargetEntityAdapterRepresentation)((AssociationEntityRepresentationConnector)element).ModelElement);
                    if (CustomizedCode.FormEntityJoinRelation.IsValid(link))
                    {
                        CustomizedCode.FormEntityJoinRelation frmEditRelation = new CustomizedCode.FormEntityJoinRelation(link);
                        frmEditRelation.ShowDialog();
                    }
                    else
                        MessageBox.Show("Verify if the two representations are correctly defined!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                break;
            }
                        
            //Call the form here
            base.OnDoubleClick(e);
        }
    }
   
    public partial class EntityDataModelShape
    {
        public override void OnDoubleClick(Microsoft.VisualStudio.Modeling.Diagrams.DiagramPointEventArgs e)
        {
            foreach (var element in e.DiagramHitTestInfo.HitDiagramItem.RepresentedElements)
            {
                if (element is EntityDataModelShape)
                {
                    ((EntityDataModel)((EntityDataModelShape)element).ModelElement).AddNewEdmReference(true);
                }
                break;
            }
            base.OnDoubleClick(e);
        }
    }
    
}

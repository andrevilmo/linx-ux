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
using DslModeling = global::Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling;

namespace Linx.EntityAdapterDesigner
{
    

    public partial class StoreScriptShape
	{     
		public override void OnDoubleClick(Microsoft.VisualStudio.Modeling.Diagrams.DiagramPointEventArgs e)
		{
			foreach (var element in e.DiagramHitTestInfo.HitDiagramItem.RepresentedElements)
			{
                if (element is StoreQuery)
				{
                    StoreQuery sq = (StoreQuery)element;
                    using (Transaction transaction = this.Store.TransactionManager.BeginTransaction("OpenStoreQuery"))
                    {
                        DomainServiceOperation operation = new DomainServiceOperation(sq.Partition, new DslModeling.PropertyAssignment[] { }) { Access = OperationAccess.Public, Name = sq.Name, OverloadName = sq.Name, Parameters = sq.Parameters, ReturnType = sq.QueryReturnType + "<" + sq.GenericType + ">", DomainAttribute = DomainAttributeType.IgnoreOperation, IsUniqueOverload = true };
                        ((StoreQuery)element).StoreScript.EntityAdapterDesignerRoot.OpenStoreQuery(sq, operation);
                        operation = null;
                        transaction.Rollback();
                    }
                    
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

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
using System.Collections;
using System.Drawing;
using Microsoft.VisualStudio.Modeling.Diagrams;

namespace Linx.EntityAdapterDesigner
{
	public partial class LookUpAdapterShape
	{
        public override void OnShapeInserted()
        {
            base.OnShapeInserted();
            LookUpAdapter lookUp = this.ModelElement as LookUpAdapter;
            lookUp.AdjustColorShape();
        }

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

        public void SetOutlineColor(System.Drawing.Color color)
        {
            using (Transaction tran = this.Store.TransactionManager.BeginTransaction("Change color"))
            {
                this.OutlineColor = color;
                tran.Commit();
            }
        }

        public void SetTextColor(System.Drawing.Color color)
        {
            using (Transaction tran = this.Store.TransactionManager.BeginTransaction("Change color"))
            {
                this.TextColor = color;
                tran.Commit();
            }
        }

		public override void OnDoubleClick(Microsoft.VisualStudio.Modeling.Diagrams.DiagramPointEventArgs e)
		{	
			foreach (var element in e.DiagramHitTestInfo.HitDiagramItem.RepresentedElements)
			{
				if (element is LookUpProperty)
				{
					((LookUpProperty)element).LookUpAdapter.EntityAdapterDesignerRoot.OpenCodeElement(element); 
				}
				else if (element is LookUpAdapterShape)
				{
					((LookUpAdapter)((LookUpAdapterShape)element).ModelElement).EntityAdapterDesignerRoot.OpenCodeElement(((LookUpAdapterShape)element).ModelElement); 
				}
				break;
			}
			base.OnDoubleClick(e);
		}

        private static bool initializeFromMappings;
        protected override void InitializeFromMappings()
        {
            if (!LookUpAdapterShape.initializeFromMappings && LookUpAdapterShape.compartmentMappings != null && LookUpAdapterShape.compartmentMappings.Count > 0)
            {
                ElementListCompartmentMapping operationMapping = LookUpAdapterShape.compartmentMappings.First().Value.FirstOrDefault(e => e.CompartmentId == "LookUpPropertiesDomainServiceOperationsCompartiment") as ElementListCompartmentMapping;
                if (operationMapping != null)
                {
                    operationMapping.ImageGetter = LookUpAdapterShape.GetElementImage;
                    LookUpAdapterShape.initializeFromMappings = true;
                }
            }
            base.InitializeFromMappings();
        }

        /// <summary>
        /// Decides what the icon of the Attribute will be in the class shape
        /// </summary>
        private static Image GetElementImage(ModelElement mel)
        {
            LookUpProperty member = mel as LookUpProperty;
            if ((member != null))
            {                
                if (member.IsPrimaryKey)                    
                    return Resources.VSObject_Properties_PrimaryKey;
                else if (member.IgnoreMetaData)
                    return Resources.PropertyRemoved;
                else if (member.IsCustomized)
                    return Resources.VSObject_Custom;
                else
                    return Resources.VSObject_Properties;
            }

            return null;
        }

	}

}

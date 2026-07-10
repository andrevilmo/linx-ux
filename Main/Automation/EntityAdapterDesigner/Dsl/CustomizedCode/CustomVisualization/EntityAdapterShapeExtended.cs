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
using Linx.EntityAdapterDesigner.CustomizedCode;
using Microsoft.VisualStudio.Modeling.Diagrams;
using System.Drawing;

namespace Linx.EntityAdapterDesigner
{

    public partial class AssociationEntityConnector
    {
        public override void OnDoubleClick(DiagramPointEventArgs e)
        {
            foreach (var element in e.DiagramHitTestInfo.HitDiagramItem.RepresentedElements)
            {
                if (element is AssociationEntityConnector)
                {
                    var dsnElement = ((EntityAdapterReferencesTargetEntityAdapter)((AssociationEntityConnector)element).ModelElement);
                    dsnElement.SourceEntityAdapter.EntityAdapterDesignerRoot.OpenCodeElement(dsnElement);
                }
                break;
            }
            base.OnDoubleClick(e);
        }
    }

    public partial class InstanceConnector
    {
        public override void OnDoubleClick(DiagramPointEventArgs e)
        {
            foreach (var element in e.DiagramHitTestInfo.HitDiagramItem.RepresentedElements)
            {
                if (element is InstanceConnector)
                {
                    var dsnElement = ((EntityInstanceReferencesEntityOwners)((InstanceConnector)element).ModelElement);
                    dsnElement.SourceEntityAdapter.EntityAdapterDesignerRoot.OpenCodeElement(dsnElement);
                }                
                break;
            }
            base.OnDoubleClick(e);
        }
    }

    public partial class CollectionConnector
    {
        public override void OnDoubleClick(DiagramPointEventArgs e)
        {
            foreach (var element in e.DiagramHitTestInfo.HitDiagramItem.RepresentedElements)
            {
                if (element is CollectionConnector)
                {
                    var dsnElement = ((EntityCollectionReferencesEntityOwners)((CollectionConnector)element).ModelElement);
                    dsnElement.SourceEntityAdapter.EntityAdapterDesignerRoot.OpenCodeElement(dsnElement);
                }
                break;
            }
            base.OnDoubleClick(e);
        }
    }

	public partial class EntityAdapterShape
	{
        
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
				if (element is EntityAdapterOperation)
					((EntityAdapterOperation)element).EntityAdapter.EntityAdapterDesignerRoot.OpenEntityOperation(((EntityAdapterOperation)element));
				else if (element is EntityAdapterEvent)
					((EntityAdapterEvent)element).EntityAdapter.EntityAdapterDesignerRoot.OpenEntityEvent(((EntityAdapterEvent)element));
                else if (element is EntityAdapterClientEvent)
                    ((EntityAdapterClientEvent)element).EntityAdapter.EntityAdapterDesignerRoot.OpenClientEntityEvent(((EntityAdapterClientEvent)element));
				else if (element is EntityAdapterFormula)
				{
					((EntityAdapterFormula)element).EntityAdapter.EntityAdapterDesignerRoot.OpenCodeElement(element); 
				}
				else if (element is EntityAdapterProperty)
				{
					((EntityAdapterProperty)element).EntityAdapter.EntityAdapterDesignerRoot.OpenCodeElement(element); 
				}
                else if (element is EntityAdapterPublicationProperty)
                {
                    ((EntityAdapterPublicationProperty)element).EntityAdapter.EntityAdapterDesignerRoot.OpenCodeElement(element);
                }
				else if (element is EntityAdapterShape)
				{
					((EntityAdapter)((EntityAdapterShape)element).ModelElement).EntityAdapterDesignerRoot.OpenCodeElement(((EntityAdapterShape)element).ModelElement); 
				}
                else if (element is EntityAdapterExtendedFilter)
                {
                    using (Transaction transaction =
                          ((EntityAdapterExtendedFilter)element).Store.TransactionManager.BeginTransaction("Edit Extended Filter."))
                    {
                        FormEntityExtendedFilter form = new FormEntityExtendedFilter() { ExtendedFilter = ((EntityAdapterExtendedFilter)element)};
                        form.ShowDialog();
                        transaction.Commit();
                    }
                }
				break;
			}
			base.OnDoubleClick(e);
		}

        private static bool initializeFromMappings;
        protected override void InitializeFromMappings()
        {
            if (!EntityAdapterShape.initializeFromMappings && EntityAdapterShape.compartmentMappings != null && EntityAdapterShape.compartmentMappings.Count > 0)
            {
                ElementListCompartmentMapping operationMapping = EntityAdapterShape.compartmentMappings.First().Value.FirstOrDefault(e => e.CompartmentId == "PropertiesCompartiment") as ElementListCompartmentMapping;
                if (operationMapping != null)
                {
                    operationMapping.ImageGetter = EntityAdapterShape.GetElementImage;
                    EntityAdapterShape.initializeFromMappings = true;
                }
            }
            base.InitializeFromMappings();
        }

        /// <summary>
        /// Decides what the icon of the Attribute will be in the class shape
        /// </summary>
        private static Image GetElementImage(ModelElement mel)
        {
            EntityAdapterProperty member = mel as EntityAdapterProperty;
            if ((member != null))
            {
                bool isPrimaryKey = member.EntityAdapter.IsPrimaryKey(member);
                if (isPrimaryKey && member.IsFK)
                    return Resources.VSObject_Properties_Fk_PK;
                else if (isPrimaryKey)
                    return Resources.VSObject_Properties_PrimaryKey;                
                else if (member.IgnoreMetaData)
                    return Resources.PropertyRemoved;
                else if (member.IsFK)
                    return Resources.VSObject_Properties_ForeignKey;
                else if (member.IsCustomized)
                    return Resources.VSObject_Custom;
                else if (!member.Filter.IsNullOrEmpty())
                    return Resources.VSObject_Properties_Filter;
                else if (member.QuickSearchIndex >= 0)
                    return Resources.VSObject_lightning;
                else
                    return Resources.VSObject_Properties;
            }

            return null;
        }

	}

}

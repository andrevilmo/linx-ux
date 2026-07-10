using System.Drawing;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Diagrams;
using System.Linq;
using Linx.Tools;

namespace Linx.BusinessModelDesigner
{
    public partial class ClassShape
    {
        public override void OnClick(DiagramPointEventArgs e)
        {
            foreach (var element in e.DiagramHitTestInfo.HitDiagramItem.RepresentedElements)
            {
                if (element is ModelAttribute)
                {
                    if (!((ModelAttribute)element).ForeignKey.IsNullOrEmpty())
                    {
                        var links = ((ModelAttribute)element).ModelClass.GetLinks(((ModelAttribute)element).ForeignKey.Left("."));
                        foreach (var link in links)
                        {
                            ((ModelAttribute)element).ModelClass.BusinessModelDesignerRoot.SelectLink(link);
                            if (link is Association)
                            {
                                ((Association)link).SelectProperties();
                                ((ModelAttribute)element).ModelClass.BusinessModelDesignerRoot.RefreshFocusedDiagramView();
                            }
                            else if (link is MultipleAssociationOrigin)
                            {
                                ((MultipleAssociationOrigin)link).SelectProperties();
                                ((ModelAttribute)element).ModelClass.BusinessModelDesignerRoot.RefreshFocusedDiagramView();
                            }
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
                if (element is ClassOperation)
                {
                    if (!(((ClassOperation)element).ModelClass is ReferenceModelClass))
                        element.OpenCode();
                }
                else if (element is ModelAttribute)
                {
                    if (!(((ModelAttribute)element).ModelClass is ReferenceModelClass))
                        element.OpenCode();
                }
                else if (element is ClassShape)
                {
                    if (!(((ModelClass)((ClassShape)element).ModelElement) is ReferenceModelClass))
                        element.OpenCode();
                }
                break;
            }
            base.OnDoubleClick(e);
        }
        
        private static bool initializeFromMappings;
        protected override void InitializeFromMappings()
        {
            if (!ClassShape.initializeFromMappings && ClassShape.compartmentMappings != null && ClassShape.compartmentMappings.Count > 0)
            {
                ElementListCompartmentMapping operationMapping = ClassShape.compartmentMappings.First().Value.FirstOrDefault(e => e.CompartmentId == "AttributesCompartment") as ElementListCompartmentMapping;
                if (operationMapping != null)
                {
                    operationMapping.ImageGetter = ClassShape.GetElementImage;
                    ClassShape.initializeFromMappings = true;
                }
            }

            base.InitializeFromMappings();
        }


        static string GetDisplayPropertyFromModelClassForAttributesCompartment(ModelElement element)
        {
            string display = string.Empty;

            if (element is ModelAttribute)
            {
                display = ((ModelAttribute)element).GetDisplay();
            }

            return display;
        }

        public void CheckDimensionRoutes(ModelClass element)
        {
            if (element != null)
            {
                var compartiment = this.FindCompartment("DimensionRoutesCompartment");
                if (compartiment != null)
                {
                    if (element.IsFactTable && !(element is ReferenceModelClass))
                        compartiment.Show();
                    else
                        compartiment.Hide();
                }
            }
        }

        /// <summary>
        /// Decides what the icon of the Attribute will be in the class shape
        /// </summary>
        private static Image GetElementImage(ModelElement mel)
        {
            ModelAttribute member = mel as ModelAttribute;
            if ((member != null))
            {
                if (member.InStudy)
                    return Resources.VSObject_PropertyRemoved;
                else if (member.IsPrimaryKey)
                    return Resources.VSObject_Properties_PrimaryKey;
                else if (!member.Filter.IsNullOrEmpty())
                    return Resources.VSObject_Properties_Filter;
                else if (!member.Formula.IsNullOrEmpty())
                    return Resources.VSObject_Properties_Calculator;
                else if (!member.ModelViewFormula.IsNullOrEmpty())
                    return Resources.ModelViewFormula;
                else if (member.IsPrimaryKey && !member.ForeignKey.IsNullOrEmpty())
                    return Resources.VSObject_Properties_Fk_PK;
                else if (member.IsPrimaryKey)
                    return Resources.VSObject_Properties_PrimaryKey;
                else if (!member.ForeignKey.IsNullOrEmpty())
                    return Resources.VSObject_Properties_ForeignKey;
                else if (member.IsCustomized)
                    return Resources.VSObject_Custom;
            }

            return null;
        }


    }
}


using System.Collections;
using Microsoft.VisualStudio.Modeling.Integration;
using System.Linq;
using System;
using System.Windows.Forms;

namespace Linx.BusinessModelDesigner
{
    // This partial implementation of the ReferenceStateShape aims at providing a custom outline to the
    // reference shape (dashed). This is needed because, in the DslDefinition.dsl file, we chose a Non solid 
    // Outline dash style for ReferenceStateShape
    public partial class ReferenceModelClassShape
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

        /// <summary>
        /// ReferenceModelClass represented by this ReferenceModelClassShape
        /// </summary>
        ReferenceModelClass RepresentedReferenceModelClass
        {
            get
            {
                return this.ModelElement as ReferenceModelClass;
            }
        }
                

        public override void OnDoubleClick(Microsoft.VisualStudio.Modeling.Diagrams.DiagramPointEventArgs e)
        {
            if (RepresentedReferenceModelClass != null && RepresentedReferenceModelClass.ModelClassReference != null)
                this.NavigateTo(RepresentedReferenceModelClass.ModelClassReference);
            base.OnDoubleClick(e);

        }

    }

}

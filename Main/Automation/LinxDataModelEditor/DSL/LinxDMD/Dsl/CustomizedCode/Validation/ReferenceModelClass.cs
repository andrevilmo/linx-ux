using System;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Integration;
using Microsoft.VisualStudio.Modeling.Integration.Picker;
using Microsoft.VisualStudio.Modeling.Validation;

namespace Linx.BusinessDataModelDesigner
{
    [ValidationState(ValidationState.Enabled)]
    partial class ReferenceModelClass
    {
        internal const string MODEL_CLASS_CUSTOM_VALIDATION_GROUP = "ModelBusReference";
        private const string INVALID_REF_FORMAT = "The '{0}' domain property of ths ReferenceModelClass instance named '{1}' contains reference value '{2}' which is invalid";

        /// <summary>
        /// Validates the Domain property modelbus reference of the ModelClass DomainClass class
        /// </summary>
        [ValidationMethod(CustomCategory = ModelClass.MODEL_CLASS_CUSTOM_VALIDATION_GROUP)]
        public void ValidateModelBusReferences(ValidationContext context)
        {
            BrokenReferenceDetector.DetectBrokenReferences(context.ValidationSubjects, (IServiceProvider)this.Store,
                    delegate(ModelElement element, DomainPropertyInfo property, ModelBusReference reference)
                    {
                        ReferenceModelClass referenceModelClass = element as ReferenceModelClass;
                        if (referenceModelClass!=null)
                         context.LogError(string.Format(INVALID_REF_FORMAT, property.Name, referenceModelClass.Name, reference.GetDisplayName()), "MBRef", element);
                    }
            );
        }
    }
}

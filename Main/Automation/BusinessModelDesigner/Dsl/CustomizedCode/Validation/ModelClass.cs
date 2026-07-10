//***************************************************************************
//
//    Copyright (c) Microsoft Corporation. All rights reserved.
//    This code is licensed under the MICROSOFT VISUAL STUDIO 2010
//    VISUALIZATION AND MODELING SOFTWARE DEVELOPMENT KIT license terms.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//***************************************************************************
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Integration;
using Microsoft.VisualStudio.Modeling.Integration.Picker;
using Microsoft.VisualStudio.Modeling.Validation;
using Linx.Tools;

namespace Linx.BusinessModelDesigner
{
    [ValidationState(ValidationState.Enabled)]
    public partial class ModelClass
    {
        internal const string MODEL_CLASS_CUSTOM_VALIDATION_GROUP = "ModelBusReference";
        private const string INVALID_REF_FORMAT = "The '{0}' domain property of ModelClass '{1}' contains reference value '{2}' which is invalid";

        /// <summary>
        /// Validates the Domain property modelbus reference of the ModelClass DomainClass class
        /// </summary>
        /// <param name="context"></param>
        [ValidationMethod(CustomCategory = ModelClass.MODEL_CLASS_CUSTOM_VALIDATION_GROUP)]
        public void ValidateModelBusReferences(ValidationContext context)
        {
            BrokenReferenceDetector.DetectBrokenReferences(context.ValidationSubjects, (IServiceProvider)this.Store,
                new Action<ModelElement, DomainPropertyInfo, ModelBusReference>(
                    delegate(ModelElement element, DomainPropertyInfo property, ModelBusReference reference)
                    {
                        ModelClass modelClass = element as ModelClass;
                        if (modelClass!=null)
                         context.LogError(string.Format(INVALID_REF_FORMAT, property.Name, modelClass.Name, reference.GetDisplayName()), "MBRef", element);
                    }));
        }
        
    }
}

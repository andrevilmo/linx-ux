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
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.DslDefinition;
using Microsoft.VisualStudio.Modeling.Sdk.ModelBusAuthoringDslDesignerExtension;
using System;

namespace Microsoft.VisualStudio.Modeling.Sdk.ModelBusAuthoringDslDesignerExtension
{

    /// <summary>
    /// Rule executed when the Type of a Domain property changes to "ModelBusReference" so that it acquires
    /// the attributes needed for the modelbus consumption
    /// </summary>
    [RuleOn(typeof(PropertyHasType))]
    public class DomainPropertyTypeChangesToModelBusReferenceRule : RolePlayerChangeRule
    {
        /// <summary>
        /// Ensures that, when a Domain property has its Type be "ModelBusReference", the
        /// necessary attributes are added on the domain property
        /// </summary>
        /// <param name="e"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Called my framework")]
        public override void RolePlayerChanged(RolePlayerChangedEventArgs e)
        {
            if (e.DomainRole.Id == PropertyHasType.TypeDomainRoleId)
            {
                ExternalType externalType = e.NewRolePlayer as ExternalType;
                if (externalType != null)
                {
                    if (ModelBusAuthoring.ExternalTypeIsModelBusReference(externalType))
                    {
                        PropertyHasType relationship = e.ElementLink as PropertyHasType;
                        DomainProperty property = relationship.Property;
                        ModelBusAuthoring.EnsureDomainPropertyHoldsModelBusAttributes(property);
                    }
                }
            }
        }
    }
}

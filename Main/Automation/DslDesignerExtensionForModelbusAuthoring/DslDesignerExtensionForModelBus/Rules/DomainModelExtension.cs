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
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.DslDefinition;


namespace Microsoft.VisualStudio.Modeling.Sdk.ModelBusAuthoringDslDesignerExtension
{
    /// <summary>
    /// Minimal implementation for a Domain model, which is needed to host rules in an extension
    /// </summary>
    [DomainObjectId(SimpleDomainModelExtension.DomainModelId)]
    public class SimpleDomainModelExtension : DomainModel
    {
        /// <summary>
        /// GUID for our extension Domain model
        /// </summary>
        public const string DomainModelId = "7AB36AA2-35B7-4104-9B2D-E2C919B9327C";

        /// <summary>
        /// Constructor of our extension Domain model
        /// </summary>
        /// <param name="store">Modeling Store to which this Domain model will be stored</param>
        public SimpleDomainModelExtension(Store store)
            : base(store, new Guid(SimpleDomainModelExtension.DomainModelId))
        {

        }

        /// <summary>
        /// The Domain model extensions provides rules (fired when the Type of a DomainProperty changes to
        /// ModelBusReference)
        /// </summary>
        /// <returns></returns>
        protected override System.Type[] GetCustomDomainModelTypes()
        {
            return new Type[]
   {
    typeof(DomainPropertyTypeChangesToModelBusReferenceRule)
   };
        }
    }


    /// <summary>
    /// MEF Provider for the DomainModelExtension above
    /// </summary>
    /// <remarks>Thanks to MEF, this extension will be picked-up by the DslDesigner</remarks>
    [Export(typeof(DomainModelExtensionProvider))]
    [ProvidesExtensionToDomainModel(typeof(DslDefinitionModelDomainModel))]
    public class SimpleDomainModelExtensionProvider : DomainModelExtensionProvider
    {
        /// <summary>
        /// Type of the Extension domain model provided by this provider
        /// </summary>
        public override Type DomainModelType
        {
            get
            {
                return typeof(SimpleDomainModelExtension);
            }
        }

    }
}

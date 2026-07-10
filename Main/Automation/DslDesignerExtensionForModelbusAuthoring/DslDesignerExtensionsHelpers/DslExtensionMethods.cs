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
using Microsoft.VisualStudio.Modeling.DslDefinition;

namespace Microsoft.VisualStudio.Modeling.Sdk.DslDefinitionExtensionsHelpers
{
    using Properties;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// C# extension methods on the Dsl definition
    /// </summary>
    public static class DslExtensionMethods
    {
        /// <summary>
        /// Ensures that a Dsl has a specified external Type
        /// </summary>
        /// <param name="dsl">Dsl to update</param>
        /// <param name="typeToAdd">Type to add</param>
        public static void EnsureHasExternalType(this DslLibraryBase dsl, System.Type typeToAdd)
        {
            Contract.Requires(dsl != null);
            Contract.Requires(typeToAdd != null);

            // Verifies if the type is already in the model
            ExternalType newExternalType = dsl.Types.Find(type => type.GetFullName(false) == typeToAdd.FullName) as ExternalType;

            // Add it if it is not already
            if (newExternalType == null)
                using (Transaction t = dsl.Store.TransactionManager.BeginTransaction(Resources.AddingModelBusReferenceExternalType))
                {
                    newExternalType = new ExternalType(dsl.Partition, new PropertyAssignment(ExternalType.NameDomainPropertyId, typeToAdd.Name),
                                                       new PropertyAssignment(ExternalType.NamespaceDomainPropertyId, typeToAdd.Namespace));
                    dsl.Types.Add(newExternalType);
                    t.Commit();
                }
        }

    }
}

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
using System.Linq;
using Microsoft.VisualStudio.Modeling.DslDefinition;
using System.Diagnostics.Contracts;

namespace Microsoft.VisualStudio.Modeling.Sdk.DslDefinitionExtensionsHelpers
{
    /// <summary>
    /// C# extension methods enabling to manipulate the custom attributes (ClrAttribute) held by a DomainProperty in a Dsl definition
    /// </summary>
    public static class DomainPropertyExtensionMethods
    {
        /// <summary>
        /// Ensures that a custom attribute is held by a DomainProperty (Typed version)
        /// </summary>
        /// <param name="domainProperty">DomainProperty that should hold the custom attribute</param>
        /// <param name="attributeType">Type of the attribute to ensure</param>
        /// <param name="exactParameters">Do we want to have those exact parameters to the custom attribute constructor, or, in case
        /// it was already there with different attributes, is it fine?</param>
        /// <param name="parameterValues">Values of the parameters to the constructor of the attribute</param>
        /// <seealso cref="M:EnsureCustomAttribute(this DomainProperty, string, bool, params string[])"/>
        public static void EnsureCustomAttribute(this DomainProperty domainProperty, Type attributeType, bool exactParameters, params object[] parameterValues)
        {
            Contract.Requires(domainProperty != null);
            Contract.Requires(attributeType != null);
            EnsureCustomAttribute(domainProperty,
                                  attributeType.FullName.Replace("Attribute", string.Empty),
                                  exactParameters,
                                  parameterValues.Select(o => (o is Type)
                                                               ? "typeof(" + (o as Type).FullName + ")"
                                                               : o.ToString()).ToArray());
        }


        /// <summary>
        /// Ensures that a custom attribute is removed from AttributedDomainElement (Typed version)
        /// </summary>
        /// <param name="attributedDomainElement">DomainProperty that should hold the custom attribute</param>
        /// <param name="attributeType">Type of the custom attribute to remove from the attributed domain element</param>
        /// <seealso cref="M:RemoveCustomAttribute(this AttributedDomainElement, string)"/>
        public static void RemoveCustomAttribute(this AttributedDomainElement attributedDomainElement, Type attributeType)
        {
            Contract.Requires(attributedDomainElement != null);
            RemoveCustomAttribute(attributedDomainElement, attributeType.FullName.Replace("Attribute", string.Empty));
        }


        /// <summary>
        /// Ensures that a custom attribute is held by an AttributedDomainElement such as a DomainProperty (by name version)
        /// </summary>
        /// <param name="attributedElement">AttributedDomainElement that should help the custom attribute</param>
        /// <param name="attributeName">Name of the attribute</param>
        /// <param name="exactParameters">Do we want to have those exact parameters to the custom attribute constructor, or, in case
        /// it was already there with different attributes, is it fine?</param>
        /// <param name="parameterValues">Values of the parameters to the constructor of the attribute</param>
        /// <seealso cref="M:EnsureCustomAttribute(this AttributedDomainElement, Type, bool, params string[])"/>
        public static void EnsureCustomAttribute(this AttributedDomainElement attributedElement, string attributeName, bool exactParameters, params string[] parameterValues)
        {
            Contract.Requires(attributedElement != null);
            Contract.Requires(attributeName != null);
            Contract.Requires(parameterValues != null);

            // should we add the parameters
            bool addParameters = exactParameters;

            ClrAttribute attribute = attributedElement.Attributes.Find(a => a.Name == attributeName);
            if (attribute == null)
            {
                // Create the new attribute which name is the type converter
                attribute = new ClrAttribute(attributedElement.Partition, new PropertyAssignment(ClrAttribute.NameDomainPropertyId, attributeName));
                attributedElement.Attributes.Add(attribute);

                // In case of a creation, we add the parameters anyways
                addParameters = true;
            }

            // Adds its parameters
            if (addParameters)
            {
                int i;
                for (i = 0; i < parameterValues.Length; ++i)
                {
                    string parameter = parameterValues[i];
                    if (i < attribute.Parameters.Count)
                    {
                        attribute.Parameters[i].Value = parameter;
                    }
                    else
                    {
                        AttributeParameter attributeParameter = new AttributeParameter(attributedElement.Partition, new PropertyAssignment(AttributeParameter.ValueDomainPropertyId, parameter));
                        attribute.Parameters.Add(attributeParameter);
                    }
                }

                while (i < attribute.Parameters.Count)
                {
                    attribute.Parameters[i].Delete();
                }
            }
        }


        /// <summary>
        /// Ensures that a custom attribute is removed from AttributedDomainElement (by name version)
        /// </summary>
        /// <param name="attributedElement">AttributedDomainElement that should held the custom attribute</param>
        /// <param name="attributeName">Name of the attribute</param>
        /// <seealso cref="M:RemoveCustomAttribute(this AttributedDomainElement, Type)"/>
        public static void RemoveCustomAttribute(this AttributedDomainElement attributedElement, string attributeName)
        {
            Contract.Requires(attributedElement != null);
            Contract.Requires(attributeName != null);
            ClrAttribute attribute = attributedElement.Attributes.Find(a => a.Name == attributeName);
            if (attribute != null)
            {
                attribute.Delete();
            }
        }
    }

}

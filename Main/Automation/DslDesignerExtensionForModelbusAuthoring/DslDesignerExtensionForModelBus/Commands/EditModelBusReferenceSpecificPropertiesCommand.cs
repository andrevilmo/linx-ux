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
using System.Linq;
using Microsoft.VisualStudio.Modeling.Diagrams.ExtensionEnablement;
using Microsoft.VisualStudio.Modeling.DslDefinition;
using Microsoft.VisualStudio.Modeling.DslDefinition.ExtensionEnablement;
using Microsoft.VisualStudio.Modeling.ExtensionEnablement;
using Microsoft.VisualStudio.Modeling.Integration;

namespace Microsoft.VisualStudio.Modeling.Sdk.ModelBusAuthoringDslDesignerExtension
{

    /// <summary>
    /// Command extending the DslDesigner to edit the custom properties for a DoaminProperty of type ModelBusReference
    /// </summary>
    [DslDefinitionModelCommandExtension]
    public class EditModelBusReferenceSpecificPropertiesCommand : ICommandExtension
    {
        /// <summary>
        /// Selection Context for this command
        /// </summary>
        [Import]
        private IVsSelectionContext SelectionContext { get; set; }

        /// <summary>
        /// The command is only active if it applies on a DomainProperty of type ModelBusReference
        /// </summary>
        /// <param name="command"></param>
        public void QueryStatus(IMenuCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            command.Enabled = SelectionContext.GetCurrentDocumentSelection<DomainProperty>()
             .Where(d => d.Type != null && d.Type.GetFullName(false) == typeof(ModelBusReference).FullName)
             .FirstOrDefault() != null;
        }


        /// <summary>
        /// Executes the command
        /// </summary>
        /// <param name="command"></param>
        public void Execute(IMenuCommand command)
        {
            // Get the information from the Domain Property
            bool? isElementReference = null;
            string pickerUiCaption = string.Empty;
            string pickerUiFilter = string.Empty;
            string[] typeLimitations = new string[0];
            ModelBusAuthoring.GetReferencesProperties(SelectionContext.GetCurrentSelection<DomainProperty>(), out isElementReference, out pickerUiCaption, out pickerUiFilter, out typeLimitations);

            // Presents the modal dialog
            ModelBusReferencePropertiesDialog prop = new ModelBusReferencePropertiesDialog();
            prop.IsModelElementReference = (isElementReference.HasValue && isElementReference.Value) ? true : false;
            prop.IsModelReference = (isElementReference.HasValue && !isElementReference.Value) ? true : false;
            prop.PickerUiCaption = pickerUiCaption;
            prop.PickerUiFilterString = pickerUiFilter;
            prop.PickerUiAuthorizedTypes = typeLimitations;
            bool? dialogResult = prop.ShowDialog();

            // Updates the DomainProperty if necessary
            if (dialogResult.HasValue && dialogResult.Value)
            {
                if (!prop.IsModelElementReference && !prop.IsModelReference)
                {
                    isElementReference = null;
                }
                else
                {
                    isElementReference = prop.IsModelElementReference;
                }
                ModelBusAuthoring.SetReferencesProperties(SelectionContext.GetCurrentDocumentSelection<DomainProperty>(), isElementReference, prop.PickerUiCaption, prop.PickerUiFilterString, prop.PickerUiAuthorizedTypes);
            }
        }


        /// <summary>
        /// Label for the command
        /// </summary>
        public string Text
        {
            get { return "Edit ModelBusReference specific properties"; }
        }

    }
}

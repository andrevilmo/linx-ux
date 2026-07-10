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
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.VisualStudio.Modeling.Diagrams;
using Microsoft.VisualStudio.Modeling.Diagrams.ExtensionEnablement;
using Microsoft.VisualStudio.Modeling.DslDefinition;
using Microsoft.VisualStudio.Modeling.DslDefinition.ExtensionEnablement;
using Microsoft.VisualStudio.Modeling.DslDesigner;
using Microsoft.VisualStudio.Modeling.ExtensionEnablement;


namespace Microsoft.VisualStudio.Modeling.Sdk.ModelBusAuthoringDslDesignerExtension
{
    using UI;

    /// <summary>
    /// Command extending the DslDesigner to add support for the modelbus to a DSL
    /// </summary>
    [DslDefinitionModelCommandExtension]
    public class UseModelBusCommand : ICommandExtension
    {
        /// <summary>
        /// Selection Context for this command
        /// </summary>
        [Import]
        public IVsSelectionContext SelectionContext { get; set; }

        /// <summary>
        /// Is the command visible and active?
        /// </summary>
        /// <param name="command"></param>
        public void QueryStatus(IMenuCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            //command.Enabled = SelectionContext.AtLeastOneSelected<Dsl>()
            //    && ModelBusAuthoring.ModelBusIsNotReferenced(SelectionContext.GetCurrentSelection<Dsl>().First());
            command.Visible = true;

            // Is there any selected DomainClasses in the Dsl explorer?
            command.Enabled = SelectionContext.AtLeastOneSelected<Dsl>();

            // Is there any selected shape representing a DomainClass on the design surface?
            command.Enabled |= (GetRepresentedModelElements<Dsl>(SelectionContext).Count() > 0);
        }


        /// <summary>
        /// Executes the command
        /// </summary>
        /// <param name="command"></param>
        public void Execute(IMenuCommand command)
        {
            EnableModelBusDialog dialog = new EnableModelBusDialog();
            bool? result = dialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                // Get the "Dsl" either selected directly in the model explorer or on the design surface (and thus its Presentation element
                // is then selected)
                Dsl selectedDsl = SelectionContext.GetCurrentSelection<Dsl>().Union(GetRepresentedModelElements<Dsl>(SelectionContext)).FirstOrDefault();
                if (dialog.EnableForModelBusConsumption)
                {
                    ModelBusAuthoring.EnableModelBusUsage(selectedDsl);
                }

                if (dialog.ExposeToModelBus)
                {
                    ModelBusAuthoring.ExposeToModelBus(selectedDsl);
                }
            }
        }


        /// <summary>
        /// Label for the command
        /// </summary>
        public string Text
        {
            get { return "Enable ModelBus"; }
        }

        /// <summary>
        /// Get represented model elements of type T (for the DslDesigner)
        /// </summary>
        /// <param name="selectionContext">Selection context on which this extension method applies</param>
        /// <remarks>
        /// We need to use PresentationElementHelper.GetDslDefinitionModelElement(PresentationElement) because the Dsl Designer
        /// is a bit special in the sense that it has a view model for the tree layout,
        /// thus the ModelElement property of a PresentationElement mostly returns a TreeNode (which is private) and we need to do something to get a 
        /// usable ModelElement (as for instance a DomainClass)
        /// </remarks>
        public static IEnumerable<T> GetRepresentedModelElements<T>(IVsSelectionContext selectionContext)
        {
            // Returns the Domain model elements of type T, represented by the selected presentation elements
            return selectionContext.GetCurrentSelection<PresentationElement>().Select(pel => PresentationElementHelper.GetDslDefinitionModelElement(pel)).OfType<T>();
        }
    }
}

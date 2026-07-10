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
using Microsoft.VisualStudio.Modeling.Integration;
using Microsoft.VisualStudio.Modeling.Integration.Picker.Hosting;
using Linx.Tools;
using System.Linq;
using Linx.BusinessModelDesigner.CustomCode;
using Microsoft.VisualStudio.Modeling.Diagrams;
using System.Windows.Forms;
using System;
using System.Collections.Generic;

namespace Linx.BusinessModelDesigner
{
    /// <summary>
    /// Class providing Extension methods for any ModelElement to navigate to a ModelBusReference
    /// </summary>
    public static class ModelBusReferenceNavigationHelper
    {
        public static void OpenCode(this object element)
        {
            if (element is ClassOperation)
            {
                ((ClassOperation)element).ModelClass.BusinessModelDesignerRoot.OpenClassOperation(((ClassOperation)element));
            }
            else if (element is ModelAttribute)
            {
                ((ModelAttribute)element).ModelClass.BusinessModelDesignerRoot.OpenCodeElement(element);
            }
            else if (element is ClassShape)
            {
                ((ModelClass)((ClassShape)element).ModelElement).BusinessModelDesignerRoot.OpenCodeElement(((ClassShape)element).ModelElement);
            }
        }

        public static string GetReferenceFile(this ModelBusReference mbReference)
        {
            if (mbReference != null && mbReference.AdapterReference != null)
                return mbReference.AdapterReference.GetPropertyValue("AbsoluteTargetPath") as string;
            else
                return String.Empty;
        }

        /// <summary>
        /// Navigates to a given ModelBusReference
        /// </summary>
        /// <param name="caller">ModelElement calling the navigation</param>
        /// <param name="referenceToNavigateTo">ModelBusReference to navigate to</param>
        /// <remarks>the <paramref name="caller"/> is just a matter of getting the Store since this is
        /// a Service provider, and from it, of getting the ModelBus service</remarks>
        public static void NavigateTo(this ModelElement caller, ModelBusReference referenceToNavigateTo)
        {
            // We cannot navigate to a null modelbus reference.
            if (referenceToNavigateTo == null)
            {
                return;
            }

            // Get the ModelBus
            IModelBus modelbus = caller.GetModelBus();

            using (ModelBusAdapter modelAdapter = modelbus.CreateAdapter(referenceToNavigateTo))
            {
                if (modelAdapter != null)
                {
                    // Get the default view (if any)
                    ModelBusView view = modelAdapter.GetDefaultView();

                    if (view != null)
                    {
                        // Shows the view
                        view.Show();

                        // If the reference is a ModelElement reference, presents this model elemsnt
                        view.SetSelection(referenceToNavigateTo);
                    }

                    // Get the modelClass
                    ModelType referencedModelClass = modelAdapter.ResolveElementReference(referenceToNavigateTo) as ModelType;
                    if (referencedModelClass != null)
                    {
                        ModelBusReference modelClassReference = modelAdapter.GetElementReference(referencedModelClass);
                        System.Diagnostics.Debug.Assert(modelClassReference == referenceToNavigateTo, "modelAdapter.GetElementReference(ResolveElementReference<ModelClass>(referenceToNavigateTo)) should equal referenceToNavigateTo");
                        referencedModelClass.BusinessModelDesignerRoot.SelectShape(referencedModelClass.Name);
                    }

                    // Note that you could very well have requested a model element of a given reference
                    // ModelElement mel = modelAdapter.ResolveElementReference(referenceToNavigateTo) as ModelElement;
                    // And then do whatever you want with it using the regular DSL Tools API (in a modeling transaction if you change anything)
                }
            }
        }


        public static void ShowDesigner(this ModelElement caller, EnvDTE.ProjectItem item)
        {            
            // Get the ModelBus
            IModelBus modelBus = caller.GetModelBus();
            // Get an adapterManager for the target DSL:
            ModelBusAdapterManager manager = modelBus.FindAdapterManagers(item).First();
            // Create a reference to the target model:
            ModelBusReference modelReference = manager.CreateReference(item);
            
            using (ModelBusAdapter modelAdapter = modelBus.CreateAdapter(modelReference))
            {
                if (modelAdapter != null)
                {
                    // Get the default view (if any)
                    ModelBusView view = modelAdapter.GetDefaultView();
                    if (view != null)
                    {
                        // Shows the view
                        view.Show();
                    }
                }
            }
        }

        public static T GetPresentation<T>(this ModelElement element) where T : PresentationElement
        {
            return PresentationViewsSubject.GetPresentation(element).FirstOrDefault() as T;
        }

        public static T GetModelRoot<T>(this ModelBusAdapter modelAdapter) where T : ModelElement
        {
            return modelAdapter.GetPropertyValue("ModelRoot") as T;
        }
        
        public static T GetModelRoot<T>(this ModelElement caller, ModelBusReference reference) where T : ModelElement
        {
            // We cannot navigate to a null modelbus reference.
            if (reference == null)
            {
                return null;
            }

            // Get the ModelBus
            IModelBus modelbus = caller.GetModelBus();
            T referencedModelRoot = null;

            try
            {
                using (ModelBusAdapter modelAdapter = modelbus.CreateAdapter(reference))
                {
                    if (modelAdapter != null)
                    {
                        // Get the ModelRoot
                        referencedModelRoot = modelAdapter.GetModelRoot<T>();
                    }
                }
            }
            catch
            {
                string path = reference.AdapterReference.GetPropertyValue("AbsoluteTargetPath") as string;
                MessageBox.Show("Problem loading the root of [" + (path ?? reference.ModelDisplayName) + "].", "External Reference Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return referencedModelRoot;
        }

        /// <summary>
        /// Get the ModelElement instance from the other referenced model.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="caller">ModelElement calling the instance</param>
        /// <param name="reference">ModelBusReference to get the referenced instance from the other model</param>
        public static T GetInstanceReference<T>(this ModelElement caller, ModelBusReference reference, bool reportError = true) where T : ModelElement
        {
            // We cannot navigate to a null modelbus reference.
            if (reference == null)
            {
                return null;
            }

            // Get the ModelBus
            IModelBus modelbus = caller.GetModelBus();
            T referencedModelElement = null;

            try
            {
                using (ModelBusAdapter modelAdapter = modelbus.CreateAdapter(reference))
                {
                    if (modelAdapter != null)
                    {
                        // Get the ModelElement
                        referencedModelElement = modelAdapter.ResolveElementReference(reference) as T;
                    }
                }
            }
            catch
            {
                if (reportError)
                {
                    string path = reference.AdapterReference.GetPropertyValue("AbsoluteTargetPath") as string;
                    MessageBox.Show("Problem loading [" + (path ?? reference.ModelDisplayName) + "\\" + reference.ElementDisplayName + "].\r\nTry to add it again from the new location for correcting the problem.", "External Reference Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            return referencedModelElement;
        }


        public static IModelBus GetModelBus(this ModelElement caller)
        {
            return caller.Store.GetService(typeof(SModelBus)) as IModelBus;
        }

        public static IReferencePicker GetPicker(this ModelElement caller)
        {
            return caller.Store.GetService(typeof(SReferencePicker)) as IReferencePicker;
        }                
    }
}

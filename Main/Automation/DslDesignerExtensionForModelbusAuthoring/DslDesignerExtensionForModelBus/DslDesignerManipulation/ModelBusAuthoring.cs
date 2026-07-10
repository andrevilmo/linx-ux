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
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.Modeling.DslDefinition;
using Microsoft.VisualStudio.Modeling.Integration;
using Microsoft.VisualStudio.Modeling.Integration.Picker;
using Microsoft.VisualStudio.Modeling.Sdk.Dsl2Dte;
using Microsoft.VisualStudio.Modeling.Sdk.DslDefinitionExtensionsHelpers;

namespace Microsoft.VisualStudio.Modeling.Sdk.ModelBusAuthoringDslDesignerExtension
{
    /// <summary>
    /// Class containing the logic of what a DSL author needs to know to expose his model to the ModelBus
    /// or to use the ModelBus in a DSL
    /// </summary>
    static public class ModelBusAuthoring
    {
        #region Is the modelbus usage enabled in the DSL ?
        /// <summary>
        /// Tells if the external type passed as an argument is "ModelBusReference"
        /// </summary>
        /// <param name="externalType">External type of interest</param>
        /// <returns><c>true</c> if the parameter is "ModelBusReference", and <c>false</c> otherwise</returns>
        public static bool ExternalTypeIsModelBusReference(ExternalType externalType)
        {
            return externalType.GetFullName(false) == typeof(ModelBusReference).FullName;
        }

        /// <summary>
        /// Tells if the modelbus is already referenced by the project containing the DslDefinition
        /// or not
        /// </summary>
        /// <param name="dsl">Dsl definition the project of which we want to know if it references
        /// the Modelbus assembly or not</param>
        /// <returns><c>true</c> if the project containing the Dsl definition passed as an argument
        /// references the modelbus assembly, and <c>false</c> otherwise</returns>
        public static bool ModelBusIsNotReferenced(Dsl dsl)
        {
            return !StoreHostingProject.IsHostingProjectReferencingAssembly(dsl.Store, typeof(ModelBusReference).Assembly);
        }
        #endregion

        #region Enable the modelbus usage in the DSL
        /// <summary>
        /// Ensables the modelbus usage on a Dsl definition that is:
        /// <list type="bullet">
        /// <item>Add a reference to the modelbus assembly if it is not already done</item>
        /// <item>Add an external type "ModelBusReference" if it is not already there</item>
        /// </list>
        /// </summary>
        /// <param name="dsl">Dls definition of interest</param>
        public static void EnableModelBusUsage(Dsl dsl)
        {
            // Ensures that the project containing the DslDefinition.dsl file references the ModelBus assembly
            StoreHostingProject.EnsureProjectReferencesAssembly(dsl.Store, typeof(ModelBusReference).Assembly);

            // Ensures that the Dsl has ModelBusReference among its external types
            dsl.EnsureHasExternalType(typeof(ModelBusReference));

            StoreHostingProject.EnsureFileCopiedInDslProject(dsl.Store,
                                                             @"GeneratedCode\ModelBusReferencesSerialization.tt",
                                                             Path.Combine(Path.GetDirectoryName(typeof(ModelBusAuthoring).Assembly.Location), @"Templates\ModelBusReferencesSerialization.tt"));
        }
        #endregion

        #region Expose the DSL to the ModelBus
        /// <summary>
        /// Expose this Dsl to the modelbus
        /// </summary>
        /// <param name="dsl"></param>
        internal static void ExposeToModelBus(Dsl dsl)
        {
            // Add the ModelBusAdapter.csproj project to the solution (if necessary) and change the name of the generated assembly, and the default namespace
            StoreHostingProject.EnsureNamedProjectExistsInDslSolution(dsl.Store, @"ModelBusAdapter\ModelBusAdapter.csproj"
                                                           , Path.Combine(Path.GetDirectoryName(typeof(ModelBusAuthoring).Assembly.Location), @"Templates\ModelBusAdapters\MyTemplate.vstemplate")
                                                           , true
                                                           );

            // Update the link to the Key.sln if necessary
            StoreHostingProject.EnsureFileLinkInProject(dsl.Store, @"ModelBusAdapter\ModelBusAdapter.csproj", "Key.snk", @"..\Key.snk");

            // Update the VSIX manifest in the DslPackage to add the new project as a MEF extension
            string modelFileName = Store2DTE.GetFileNameForStore(dsl.Store);
            string vsixManifestFileName = Path.Combine(Path.GetDirectoryName(modelFileName) + @"\..\DslPackage\source.extension.tt");
            AddMefProject(vsixManifestFileName, "ModelBusAdapter");

            // Ensures that the DslPackage project references the ModelBusAdapter (for VSIX deployment) 
            StoreHostingProject.EnsureProjectReferencesProject(dsl.Store, @"DslPackage\DslPackage.csproj", @"ModelBusAdapter\ModelBusAdapter.csproj");
        }

        /// <summary>
        /// Add the Mef projects to the VSIX manifest
        /// </summary>
        /// <param name="vsixManifestFileName"></param>
        /// <param name="newMefProject"></param>
        private static void AddMefProject(string vsixManifestFileName, string newMefProject)
        {
            string content = File.ReadAllText(vsixManifestFileName);
            string lineToSearch = "<MefComponent>|"+newMefProject+"|</MefComponent>";
            if (!content.Contains(lineToSearch))
            {
                content = content.Replace("</Content>", "  " + lineToSearch + Environment.NewLine+"  </Content>");
                File.WriteAllText(vsixManifestFileName, content);
            }

            //string[] lines = File.ReadAllLines(vsixManifestFileName);
            //for (int i = 0; i < lines.Length; ++i)
            //{
            //    string line = lines[i];
            //    if (DefinesCustomContents(line))
            //    {
            //        lines[i] = UpdateCustomContent(line, newMefProject);
            //    }
            //}
            //File.WriteAllLines(vsixManifestFileName, lines);
        }


        ///// <summary>
        ///// Does this line define the custom content in the VSIX manifest?
        ///// </summary>
        ///// <param name="line"></param>
        ///// <returns></returns>
        //private static bool DefinesCustomContents(string line)
        //{
        //    line = line.Trim();
        //    return line.StartsWith("string customContent =", StringComparison.Ordinal);
        //}

        ///// <summary>
        ///// Update the custom content of the VSIX manifest
        ///// </summary>
        ///// <param name="line">Line to which to add the new MEF project</param>
        ///// <param name="newMefProject">New mef project to add to the content</param>
        ///// <returns></returns>
        //private static string UpdateCustomContent(string line, string newMefProject)
        //{
        //    string newContent = string.Format(CultureInfo.InvariantCulture, "\"<MefComponent>|{0}|</MefComponent>\"", newMefProject);

        //    // Only add the Mef component if it's not already there
        //    if (line.Contains(newContent))
        //    {
        //        return line;
        //    }

        //    // case of an un-customed vsix manifest text template
        //    else if (line.Contains("null"))
        //    {
        //        return line.Replace("null", newContent);
        //    }

        //    // case of a customized vsix manifest text template
        //    else
        //    {
        //        return line.Replace(";", "+" + newContent + ";");
        //    }
        //}

        #endregion

        #region Management of the custom attributes of the DomainProperty of type ModelBusReference
        /// <summary>
        /// Ensures that a DomainProperty (of type ModelBusReference) helds the necessary ModelBusAttributes
        /// </summary>
        /// <param name="domainProperty">domain property for which we want to ensure ClrAttributes to work with the ModelBus</param>
        public static void EnsureDomainPropertyHoldsModelBusAttributes(DomainProperty domainProperty)
        {
            using (Transaction t = domainProperty.Store.TransactionManager.BeginTransaction("Adding Type converter/editor attributes to Domain property"))
            {
                // Ensure [System.ComponentModel.TypeConverter(typeof(ModelBusReferenceTypeConverter))]
                domainProperty.EnsureCustomAttribute(typeof(TypeConverterAttribute), false,
                                                     typeof(ModelBusReferenceTypeConverter));


                // Ensure [System.ComponentModel.Editor(typeof(Microsoft.VisualStudio.Modeling.Integration.Picker.ModelElementReferenceEditor),typeof(System.Drawing.Design.UITypeEditor))]
                domainProperty.EnsureCustomAttribute(typeof(EditorAttribute), false,
                                                     typeof(ModelElementReferenceEditor), typeof(UITypeEditor));
                t.Commit();
            }
        }

        /// <summary>
        /// Set the specific modelbus reference customizations on the DomainProperties of the ModelBusReference
        /// </summary>
        /// <param name="properties">properties of interest</param>
        /// <param name="isElementReference">Is the ModelBusReference a model element reference? Or rather a model reference</param>
        /// <param name="pickerUiCaption">Caption for the picker UI window</param>
        /// <param name="pickerUiFilter">File filter for the picker UI window</param>
        /// <param name="typeLimitations">Types to present in the picker UI</param>
        internal static void SetReferencesProperties(IEnumerable<DomainProperty> properties, bool? isElementReference, string pickerUiCaption, string pickerUiFilter, string[] typeLimitations)
        {
            foreach (DomainProperty domainProperty in properties)
            {
                using (Transaction t = domainProperty.Store.TransactionManager.BeginTransaction("Change Model Reference custom attributes"))
                {
                    // EditorAttribute
                    if (isElementReference.HasValue)
                    {
                        if (isElementReference.Value)
                        {
                            domainProperty.EnsureCustomAttribute(typeof(EditorAttribute), true, typeof(ModelElementReferenceEditor), typeof(UITypeEditor));
                        }
                        else
                        {
                            domainProperty.EnsureCustomAttribute(typeof(EditorAttribute), true, typeof(ModelReferenceEditor), typeof(UITypeEditor));
                        }
                    }
                    else
                    {
                        // Fallback: will be a model element editor
                        domainProperty.EnsureCustomAttribute(typeof(EditorAttribute), true, typeof(ModelElementReferenceEditor), typeof(UITypeEditor));
                    }

                    // SupplyFileBasedBrowserConfigurationAttribute
                    if (!string.IsNullOrWhiteSpace(pickerUiCaption) || !string.IsNullOrWhiteSpace(pickerUiFilter))
                    {
                        domainProperty.EnsureCustomAttribute(typeof(SupplyFileBasedBrowserConfigurationAttribute), true, Quote(pickerUiCaption), Quote(pickerUiFilter));
                    }
                    else
                    {
                        domainProperty.RemoveCustomAttribute(typeof(SupplyFileBasedBrowserConfigurationAttribute));
                    }

                    // Type limitations
                    if (typeLimitations != null && typeLimitations.Length >0)
                    {
                        domainProperty.EnsureCustomAttribute(typeof(ApplyElementTypeLimitationsAttribute), true, typeLimitations.Select(typename => "typeof(" + typename + ")").ToArray());
                    }
                    else
                    {
                        domainProperty.RemoveCustomAttribute(typeof(ApplyElementTypeLimitationsAttribute));
                    }

                    // Only for the first property
                    if (t.HasPendingChanges)
                    {
                        t.Commit();
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// Get the properties for the first DomainProperty of type <c>ModelBusReference</c> in the the <paramref name="properties"/> parameter
        /// </summary>
        /// <param name="properties">Domain Element properties of interest</param>
        /// <param name="isElementReference">Tells whether this is a ModelBus model element reference, a Modelbus model reference, or none</param>
        /// <param name="pickerUiCaption">Caption for the picker UI</param>
        /// <param name="pickerUiFilter">File type Filter for the picker UI</param>
        /// <param name="typeLimitations">Type limitations for the Picker UI</param>
        public static void GetReferencesProperties(IEnumerable<DomainProperty> properties, out bool? isElementReference, out string pickerUiCaption, out string pickerUiFilter, out string[] typeLimitations)
        {
            isElementReference = null;
            pickerUiCaption = string.Empty;
            pickerUiFilter = string.Empty;
            typeLimitations = new string[0];

            foreach (DomainProperty domainProperty in properties)
            {
                isElementReference = IsElementReference(domainProperty);
                GetFileBasedBrowerConfiguration(domainProperty, ref pickerUiCaption, ref pickerUiFilter);
                typeLimitations = GetTypeLimitations(domainProperty);

                // Only for the first property
                break;
            }
        }

        /// <summary>
        /// Get the type limitations applied to the domain property (if they exist)
        /// </summary>
        /// <param name="domainProperty">Domain property for which we are looking for the type limitations</param>
        /// <returns>List of type limitations for the Picker UI</returns>
        private static string[] GetTypeLimitations(DomainProperty domainProperty)
        {
            string[] typeLimitations = null;
            ClrAttribute attribute = domainProperty.Attributes.FirstOrDefault(a => typeof(ApplyElementTypeLimitationsAttribute).FullName.StartsWith(a.Name, StringComparison.Ordinal));
            if (attribute != null)
            {
                typeLimitations = attribute.Parameters.Select(p => p.Value.Replace("typeof(", string.Empty).Replace(")", string.Empty)).ToArray();
            }
            return typeLimitations;
        }

        /// <summary>
        /// Get the File based browser configuration associated with a DomainProperty of type ModelBusReference
        /// </summary>
        /// <param name="domainProperty">Domain property, supposed to be of type "ModelBusReference"</param>
        /// <param name="pickerUiCaption">Caption</param>
        /// <param name="pickerUiFilter">UI Filter</param>
        private static void GetFileBasedBrowerConfiguration(DomainProperty domainProperty, ref string pickerUiCaption, ref string pickerUiFilter)
        {
            ClrAttribute attribute = domainProperty.Attributes.FirstOrDefault(a => typeof(SupplyFileBasedBrowserConfigurationAttribute).FullName.StartsWith(a.Name, StringComparison.Ordinal));
            if (attribute != null)
            {
                if (attribute.Parameters.Count() == 2)
                {
                    pickerUiCaption = UnQuote(attribute.Parameters[0].Value);
                    pickerUiFilter = UnQuote(attribute.Parameters[1].Value);
                }
            }
        }

        /// <summary>
        /// Tells if the domain property is a ModelBusReference on a Model element, on a model, or none of both
        /// </summary>
        /// <param name="domainProperty">>Domain property, supposed to be of type "ModelBusReference"</param>
        /// <returns>null if the domain property is neither for Model references nor Model element references, <c>true</c>
        /// if this represents a model element reference and <c>false</c> if this represents a model reference</returns>
        private static bool? IsElementReference(DomainProperty domainProperty)
        {
            bool? isElementReference = null;
            ClrAttribute attribute = domainProperty.Attributes.FirstOrDefault(a => typeof(EditorAttribute).FullName.StartsWith(a.Name, StringComparison.Ordinal));
            if (attribute != null)
            {
                if (attribute.Parameters.Count() > 0)
                {
                    AttributeParameter parameter = attribute.Parameters[0];
                    if (parameter.Value == "typeof(" + typeof(ModelReferenceEditor).FullName + ")")
                        isElementReference = false;
                    else if (parameter.Value == "typeof(" + typeof(ModelElementReferenceEditor).FullName + ")")
                        isElementReference = true;
                }
            }
            return isElementReference;
        }

        #endregion

        #region  Utilities
        /// <summary>
        /// Adds double quotes to a string
        /// </summary>
        /// <param name="s">string to quote</param>
        /// <returns>quoted string</returns>
        private static string Quote(string s)
        {
            return "\"" + s + "\"";
        }

        /// <summary>
        /// Remove the double quotes at the beginning and end of a string
        /// </summary>
        /// <param name="s">string to unquote</param>
        /// <returns>unquoted string</returns>
        private static string UnQuote(string s)
        {
            return s.Trim('"');
        }
        #endregion
    }
}

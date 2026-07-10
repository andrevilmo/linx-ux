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
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Reflection;
using EnvDTE;
using VSLangProj;

namespace Microsoft.VisualStudio.Modeling.Sdk.Dsl2Dte
{
    /// <summary>
    /// Helper class enabling to make the link between a Store and the VS Project that hosts the model stored in this Store. 
    /// It provides operations againt projects and files in a way that is relative to the model file, and without any reference
    /// to DTE concepts, hence simplifying the authoring
    /// </summary>
    public static class StoreHostingProject
    {
        #region References
        /// <summary>
        /// Is the project hosting the Store actually referencing an assembly?
        /// </summary>
        /// <param name="store">Modeling Store containing the model of interest</param>
        /// <param name="assembly">Assembly for which we want to know if it is referenced by the 
        /// project hosting the model contained in the store
        /// </param>
        /// <returns><c>true</c> if the assembly is referenced by the project hosting the store, and <c>false</c> otherwise</returns>
        public static bool IsHostingProjectReferencingAssembly(Store store, Assembly assembly)
        {
            Contract.Requires(store != null);
            Contract.Requires(assembly != null);
            return IsHostingProjectReferencingAssembly(store, assembly.GetName().Name);
        }

        /// <summary>
        /// Is the project hosting the Store actually referencing an assembly?
        /// </summary>
        /// <param name="store">Modeling Store containing the model of interest</param>
        /// <param name="assemblyName">Assembly name for which we want to know if it is referenced by the 
        /// project hosting the model contained in the store
        /// </param>
        /// <returns><c>true</c> if the assembly is referenced by the project hosting the store, and <c>false</c> otherwise</returns>
        public static bool IsHostingProjectReferencingAssembly(Store store, string assemblyName)
        {
            Project project = Store2DTE.GetProjectForStore(store);

            // Case where the Dsl was open outside a project.
            if (project == null)
                return false;

            // Add references.
            VSProject vsProject = project.Object as VSProject;
            return (vsProject.References.OfType<Reference>().FirstOrDefault(reference => reference.Name == assemblyName) != null);
        }


        /// <summary>
        /// Ensures that the VS project hosting a modeling store references a given assembly
        /// </summary>
        /// <param name="store">Modeling store of interest</param>
        /// <param name="assembly">Asssembly for which we want to ensure that it is referenced by the VS project hosting the <paramref name="store"/></param>
        public static void EnsureProjectReferencesAssembly(Store store, Assembly assembly)
        {
            Contract.Requires(store != null);
            EnsureProjectReferencesAssembly(store, assembly.GetName().Name);
        }

        /// <summary>
        /// Ensures that the VS project hosting a modeling store references a given assembly (by name)
        /// </summary>
        /// <param name="store">Modeling store of interest</param>
        /// <param name="assemblyName">Name of the asssembly for which we want to ensure that it is referenced by the VS project hosting the <paramref name="store"/></param>
        public static void EnsureProjectReferencesAssembly(Store store, string assemblyName)
        {
            Project project = Store2DTE.GetProjectForStore(store);

            // Case where the Dsl was open outside a project.
            if (project == null)
                return;

            // Add references.
            VSProject vsProject = project.Object as VSProject;
            if (!vsProject.References.OfType<Reference>().Any(reference => reference.Name == assemblyName))
            {
                vsProject.References.Add(assemblyName);
            }
        }


        /// <summary>
        /// Ensures that a VS project in the same solution as the project hosting a modeling store references a given project in the same solution (by name)
        /// </summary>
        /// <param name="store">Modeling store of interest</param>
        /// <param name="uniqueProjectNameReferencing">Unique project name of the project that needs to reference <paramref name="uniqueProjectNameToReference"/>. Can be null, in that
        /// case the project is the project holding the model in the store</param>
        /// <param name="uniqueProjectNameToReference">Unique project nameof the project for which we want to ensure that it is referenced by the VS project hosting the <paramref name="store"/></param>
        public static void EnsureProjectReferencesProject(Store store, string uniqueProjectNameReferencing, string uniqueProjectNameToReference)
        {
            Project project = Store2DTE.GetProjectForStore(store);

            // Case where the Dsl was open outside a project.
            if (project == null)
                return;

            // Get the referencing project
            Project referencingProject;
            if (!string.IsNullOrWhiteSpace(uniqueProjectNameReferencing))
            {
                referencingProject = project.DTE.Solution.Projects.OfType<Project>().FirstOrDefault(p => p.UniqueName == uniqueProjectNameReferencing);
            }
            else
            {
                referencingProject = project;
            }

            if (referencingProject == null)
            {
                return;
            }


            // Add reference to the other project if it is not already referenced
            VSProject vsProject = referencingProject.Object as VSProject;
            if (vsProject.References.OfType<Reference>().FirstOrDefault(reference => reference.SourceProject != null && reference.SourceProject.UniqueName == uniqueProjectNameToReference) == null)
            {

                Project otherProject = project.DTE.Solution.Projects.OfType<Project>().FirstOrDefault(p => p.UniqueName == uniqueProjectNameToReference);
                if (otherProject != null)
                {
                    vsProject.References.AddProject(otherProject);
                }
            }

        }
        #endregion

        #region Adding a project
        /// <summary>
        /// Ensuures that a project of a given name exists in the solution
        /// </summary>
        /// <param name="store">Model from which to look for</param>
        /// <param name="relativePath">Relative path where to create the new project if necessary (ending in the project name with .csproj)</param>
        /// <param name="sourcePath">Source path of the template of the project</param>
        /// <param name="updateAssemblyNameAndNamespace">Should we update the assembly name and namespace of the new project.</param>
        /// <remarks>
        /// Suppose you want to create add a new project named "MyProject.csproj" from a template (vs vsTemplate located in a sub folder of the location of the extension,
        /// and you want to have similar namespaces:
        /// <code>
        ///    StoreHostingProject.EnsureNamedProjectExistsInDslSolution(dsl.Store, "MyProject.csproj"
        ///                                          , Path.Combine(Path.GetDirectoryName(typeof(ATypeInMyExtension).Assembly.Location), @"Templates\MyProject\MyTemplate.vstemplate")
        ///                                          , true
        ///                                          );
        /// </code>
        /// </remarks>
        public static void EnsureNamedProjectExistsInDslSolution(Store store, string relativePath, string sourcePath, bool updateAssemblyNameAndNamespace)
        {
            // Verify that the relative path ends with csproj
            if (Path.GetExtension(relativePath) != ".csproj")
            {
                throw new ArgumentException("relativePath should be relative path of the .csproj file to create with respect to the solution, hence ending in .csproj", "relativePath");
            }

            Project project = Store2DTE.GetProjectForStore(store);
            Solution solution = project.DTE.Solution;
            Project newProject = solution.Projects.OfType<Project>().FirstOrDefault(p => p.UniqueName == relativePath);
            if (newProject != null)
            {
                return;
            }

            string projectDirectory = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(project.FullName)), Path.GetFileNameWithoutExtension(relativePath));
            string projectPath = Path.Combine(projectDirectory, Path.GetFileName(relativePath));
            string projectSimpleName = Path.GetFileNameWithoutExtension(relativePath);

            // The project exist but is not in the solution: let's just add it.
            if (File.Exists(projectPath))
            {
                solution.AddFromFile(projectPath, false);
            }

            // The project does not exist: create it from a template
            else
            {
                newProject = project.DTE.Solution.AddFromTemplate(sourcePath, projectDirectory, Path.GetFileName(relativePath), false);

                // Well known workaround for C# and VB projects, AddFromTemplate returns null
                newProject = solution.Projects.OfType<Project>().FirstOrDefault(p => p.Name == projectSimpleName);

                // Update the assembly name and namespace if necessary
                if (updateAssemblyNameAndNamespace)
                {
                    newProject.Properties.Item("AssemblyName").Value = project.Properties.Item("AssemblyName").Value.ToString().Replace("." + project.Name, "." + projectSimpleName);
                    newProject.Properties.Item("DefaultNamespace").Value = project.Properties.Item("DefaultNamespace").Value.ToString() + "." + projectSimpleName;
                }

            }
        }
        #endregion

        #region Adding a file or a link to a file
        /// <summary>
        /// Ensures that a file is present in the project
        /// </summary>
        /// <param name="store">Store containing a model</param>
        /// <param name="relativePath">relative path where the file should be located</param>
        /// <param name="sourcePath">Path of the file to copy if not already present in the solution</param>
        /// <example>
        /// if you have a file Adapter.tt, added to the VSIX, of type Content, and copied if newer, in a folder Temmplates of the extension project, you can add
        /// it to the GeneratedCode folder of the DSL by the following code:
        /// <code>
        ///    StoreHostingProject.EnsureFileInProject(dsl.Store, @"GeneratedCode\Adapter.tt",
        ///                                            Path.Combine(Path.GetDirectoryName(typeof(MyExtensionAuthoring).Assembly.Location), @"Templates\Adapter.tt"));
        /// </code>
        /// </example>
        public static void EnsureFileCopiedInDslProject(Store store, string relativePath, string sourcePath)
        {
            Contract.Requires(store != null);
            Contract.Requires(relativePath != null);
            Contract.Requires(sourcePath != null);

            Project project = Store2DTE.GetProjectForStore(store);

            string[] pathSegments = relativePath.Split('\\');

            ProjectItems parent = project.ProjectItems;

            // Find the folder (or create it if necessary)
            for (int i = 0; i < pathSegments.Length - 1; ++i)
            {
                ProjectItem folder = parent.OfType<ProjectItem>().FirstOrDefault(projectItem => projectItem.Name == pathSegments[i]);
                if (folder == null)
                {
                    folder = parent.AddFolder(pathSegments[i]);
                }

                parent = folder.ProjectItems;
            }

            // Find the file and create it if necessary
            ProjectItem file = parent.OfType<ProjectItem>().FirstOrDefault(projectItem => projectItem.Name == pathSegments[pathSegments.Length - 1]);
            if (file == null)
            {
                string fileDirectory = Path.Combine(Path.GetDirectoryName(project.FullName), Path.GetDirectoryName(relativePath));
                string filePath = Path.Combine(fileDirectory, Path.GetFileName(relativePath));

                // Case where the file is already there, but not added to the project
                if (File.Exists(filePath))
                {
                    parent.AddFromFile(filePath);
                }
                else
                {
                    parent.AddFromFileCopy(sourcePath);
                }
            }
        }


        /// <summary>
        /// Ensures a link on a file is created in a project
        /// </summary>
        /// <param name="store">Store containing a model</param>
        /// <param name="uniqueProjectName">Unique project name of the project to which to add a link a a file</param>
        /// <param name="relativePathOfFileToCreate">Relative path to the link to create in the project described by <paramref name="relativePathOfFileToCreate"/></param>
        /// <param name="originalFileToLink">Path to the original file to link</param>
        public static void EnsureFileLinkInProject(Store store, string uniqueProjectName, string relativePathOfFileToCreate, string originalFileToLink)
        {
            Contract.Requires(store != null);
            Contract.Requires(relativePathOfFileToCreate != null);
            Contract.Requires(originalFileToLink != null);

            Project project = Store2DTE.GetProjectForStore(store);
            if (!string.IsNullOrWhiteSpace(uniqueProjectName))
            {
                project = project.DTE.Solution.Projects.OfType<Project>().FirstOrDefault(p => p.UniqueName == uniqueProjectName);
            }
            if (project == null)
            {
                return;
            }

            string[] pathSegments = relativePathOfFileToCreate.Split('\\');

            ProjectItems parent = project.ProjectItems;

            // Find the folder (or create it if necessary)
            for (int i = 0; i < pathSegments.Length - 1; ++i)
            {
                ProjectItem folder = parent.OfType<ProjectItem>().FirstOrDefault(projectItem => projectItem.Name == pathSegments[i]);
                if (folder == null)
                {
                    folder = parent.AddFolder(pathSegments[i]);
                }

                parent = folder.ProjectItems;
            }

            // Find the file and create a link on the originalFileToLink it if necessary
            ProjectItem file = parent.OfType<ProjectItem>().FirstOrDefault(projectItem => projectItem.Name == pathSegments[pathSegments.Length - 1]);
            if (file == null)
            {
                parent.AddFromFile(Path.GetFullPath(Path.Combine(Path.GetDirectoryName(project.FullName), originalFileToLink)));
            }
        }
        #endregion
    }
}

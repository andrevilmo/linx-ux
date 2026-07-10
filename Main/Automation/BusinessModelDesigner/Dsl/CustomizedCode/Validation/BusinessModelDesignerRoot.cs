using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.Modeling.Diagrams;
using Microsoft.VisualStudio.Modeling.Validation;
using Microsoft.VisualStudio.Modeling;
using System.Globalization;
using Linx.Tools;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Linq;
using EnvDTE;
using EnvDTE80;

namespace Linx.BusinessModelDesigner
{

    [ValidationState(ValidationState.Enabled)]
    public partial class BusinessModelDesignerRoot
    {

        [ValidationMethod(ValidationCategories.Menu | ValidationCategories.Save)]
        private void ValidateStateNamesUnique(ValidationContext context)
        {
            if (this.Types.Count == 0 && this.DbProviders.Count == 0 && this.WebApiControllers.Count == 0) return;

            //Check if exist BMDs out of the project.
            this.ValidBmds(context);
            //Validating Unique Names
            this.ValidDataContextName(this, context);
            this.ValidUniqueNames(this.Types, context);
            this.ValidDomains(this.Types, context);
            this.ValidPrimaryKeys(this.Types, context);
            this.ValidUniqueTables(this.Types.Where(e => e is ModelClass).Select(e => (ModelClass)e).ToArray(), context);
            this.ValidSharedTables(this.Types.Where(e => e is ModelClass).Select(e => (ModelClass)e).ToArray(), context);
            this.ValidInheritanceProperties(this.Types.Where(e => e is ModelClass).Select(e => (ModelClass)e).ToArray(), context);
            this.ValidDiscriminators(this.Types.Where(e => e is ModelClass).Select(e => (ModelClass)e).ToArray(), context);
            this.ValidStructures(this.Types, context);
            this.ValidForeignKeys(this.Types, context);
            this.ValidReferemces(this.Types, context);
            this.ValidModelViews(this.Types, context);

            //Update NuGets
            if (GetProjectItemByName(this.GetBmdProject(), "Includes") != null)
                this.UpdatePackages();

            //After save the document, update the storage
            Action<EnvDTE.Document> afterSaver = null;
            var document = GetDiagramProjectItem();
            if (document == null)
            {
                MessageBox.Show("Problem by getting diagram project item of this project!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Save solution
            EnvDTE.DocumentEvents docEvents = null;
            try
            {
                docEvents = document.DTE.Events.get_DocumentEvents(document.Document) as EnvDTE.DocumentEvents;
            }
            catch (Exception exp)
            {
                MessageBox.Show("Problem by getting current document events of this project with the following message: " + exp.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (docEvents == null)
                return;

            afterSaver = (doc) =>
            {
                //Remove event
                docEvents.DocumentSaved -= new EnvDTE._dispDocumentEvents_DocumentSavedEventHandler(afterSaver);
                //verify projects
                this.AdjustProjects();
                //Update templates, references, etc.
                this.UpdateTemplates();
                //Check FK Erros
                this.ValidFKErrors();

            };
            docEvents.DocumentSaved += new EnvDTE._dispDocumentEvents_DocumentSavedEventHandler(afterSaver);

        }

        public bool MoveProjectToSolutionFolder(Project project, string folderName)
        {
            var itemFolder = GetProjectByName(folderName);
            if (itemFolder == null)
            {
                ((EnvDTE100.Solution4)project.DTE.Solution).AddSolutionFolder(folderName);
                itemFolder = GetProjectByName(folderName);

                string projectPath = project.FullName;
                //Remove from solution
                if (project.ParentProjectItem == null)
                    ((EnvDTE100.Solution4)project.DTE.Solution).Remove(project);
                else
                    project.ParentProjectItem.Remove();

                //Add projet to folder
                var designerFolder = itemFolder.Object as EnvDTE80.SolutionFolder;
                if (designerFolder != null)
                    designerFolder.AddFromFile(projectPath);

                return true;

            }

            return false;
        }

        private void AdjustProjects()
        {
            EnvDTE.Project diagramProject = this.GetBmdProject();

            //Adjust "Business Models" Folder
            if (diagramProject != null && (this.WebApiControllers.Count > 0 || this.GetContracts().Count > 0 || this.ModelImplementations.Count > 0))
            {
                if (MoveProjectToSolutionFolder(diagramProject, "Business Models"))
                {
                    var dte = this.GetDTE();
                    if (dte != null)
                    {
                        dte.ExecuteCommand("File.SaveAll");
                        diagramProject = this.GetBmdProject();
                    }
                }
            }

            //Update references
            this.UpdateExtendedReferences(diagramProject);

            //Post Build
            this.SetPostBuildEvent(diagramProject, true);

            //Verify inconsistent files
            this.DeleteInconsistentFiles();
            
        }

        public void SetStudyMode(bool inStudy)
        {
            using (Transaction t = this.Store.TransactionManager.BeginTransaction("Study Mode"))
            {
                foreach (var entity in this.Types.Where(e => e is ModelClass && !(e is ReferenceModelClass) & ((ModelClass)e).InStudy).Select(e => (ModelClass)e).ToArray())
                {
                    entity.InStudy = inStudy;
                }
                t.Commit();
            }
        }

        public void UpdateContextMetadata()
        {
            var bmProject = this.GetBmdProject();
            this.UpdateContextMetadata(bmProject);
        }


        private void AdjustBmdReferences(EnvDTE.Project diagramProject)
        {
            if (this.IsAspNetCoreEnabled())
                return;

            string gacPath = this.GetFullPath("Linx.GAC") ?? "";
            //this.RemoveReferencesWithoutFile(diagramProject);
            this.AdjustMissingReferences(diagramProject);

            this.UpdateVersion(diagramProject);

            this.RemoveReference(diagramProject, "System.Data.Entity");
            this.UpdateReference(diagramProject, Path.Combine(gacPath, "Linx.LinqExtensions.dll"));

            if (!this.ExistsReference(diagramProject, "System.Configuration.dll"))
                this.AddNewReference(diagramProject, "System.Configuration.dll");

            if (!this.ExistsReference(diagramProject, "System.ComponentModel.DataAnnotations.dll"))
                this.AddNewReference(diagramProject, "System.ComponentModel.DataAnnotations.dll");

            this.UpdateReference(diagramProject, Path.Combine(gacPath, "Linx.Tools.dll"));

            if (this.GetDefaultProvider() == Provider.SQLServer)
            {
                this.UpdateReference(diagramProject, Path.Combine(gacPath, "EntityFramework.Utilities.dll"));
            }
            else
            {
                if (this.ExistsReference(diagramProject, "EntityFramework.Utilities.dll"))
                    this.RemoveReference(diagramProject, "EntityFramework.Utilities.dll");
            }


            if (!this.ExistsReference(diagramProject, "System.ComponentModel.Composition.dll"))
                this.AddNewReference(diagramProject, "System.ComponentModel.Composition.dll");

            var defProvider = this.GetDefaultProvider();
            bool enableMigration = this.EnableMigration();
            switch (defProvider)
            {
                case Provider.SQLServer:
                    this.UpdateLibReferences(diagramProject, "Linx.CodeFirst.PreGenViews", true);
                    this.RemoveLibReferences(diagramProject, "Linx.CodeFirst.Oracle");
                    this.RemoveLibReferences(diagramProject, "Linx.CodeFirst.SQLite.Default");
                    this.RemoveInteropReferences(diagramProject, "Linx.CodeFirst.SQLite.Interop.Default");
                    break;
                case Provider.Oracle:
                    this.RemoveLibReferences(diagramProject, "Linx.CodeFirst.PreGenViews");
                    this.RemoveLibReferences(diagramProject, "Linx.CodeFirst.SQLite.Default");
                    this.RemoveInteropReferences(diagramProject, "Linx.CodeFirst.SQLite.Interop.Default");
                    this.UpdateLibReferences(diagramProject, "Linx.CodeFirst.Oracle", true);
                    break;
                case Provider.SQLite:
                    this.RemoveLibReferences(diagramProject, "Linx.CodeFirst.PreGenViews");
                    this.RemoveLibReferences(diagramProject, "Linx.CodeFirst.Oracle");
                    this.UpdateLibReferences(diagramProject, "Linx.CodeFirst.SQLite.Default", true);
                    this.AddInteropReferences(diagramProject, "Linx.CodeFirst.SQLite.Interop.Default");
                    break;
                default:
                    break;
            }

        }

        private void UpdateExtendedReferences(EnvDTE.Project diagramProject = null)
        {
            try
            {
                if (diagramProject == null)
                    diagramProject = this.GetBmdProject();

                //Update extended references
                if (!diagramProject.IsNull())
                {
                    //Adjust Diagram Project
                    this.AdjustBmdReferences(diagramProject);

                    //Implementations
                    foreach (var intf in this.GetContracts())
                    {
                        this.UpdateContractProject(diagramProject, intf);

                        foreach (var implement in intf.ModelImplementations)
                            this.UpdateImplementationProject(diagramProject, implement);
                    }

                    // Update .Net Core Model Project
                    this.UpdateBmCoreProject(diagramProject);

                    //WebApis
                    foreach (var api in this.WebApiControllers)
                    {
                        this.UpdateWebApiProject(diagramProject, api);
                        this.UpdateWebApiCoreProject(diagramProject, api);
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("Problem by updating the extended references of this project with the following message: " + exp.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public List<ModelInterface> GetContracts()
        {
            return this.Types.Where(e => e is ModelInterface).Select(e => (ModelInterface)e).ToList();
        }

        public VSLangProj.Reference AddProjectReference(Project project, Project reference, bool copyLocal)
        {
            VSLangProj.VSProject vsProject = (VSLangProj.VSProject)project.Object;

            VSLangProj.Reference prjReference;
            if (ExistsReference(project, reference.Name))
                prjReference = GetReference(project, reference.Name);
            else
                prjReference = vsProject.References.AddProject(reference);

            if (prjReference != null && prjReference.CopyLocal != copyLocal)
                prjReference.CopyLocal = copyLocal;

            return prjReference;
        }

        public static bool UpgradeVersion(Project project)
        {
            //Upgrade project to new Framework version if necessary        
            if ((((uint)project.Properties.Item("TargetFramework").Value) != 262406))
            {
                project.Properties.Item("TargetFrameworkMoniker").Value = (new System.Runtime.Versioning.FrameworkName(".NETFramework", new Version(4, 6, 1))).FullName;
                return true;
            }

            return false;
        }

        private void SetPostBuildEvent(EnvDTE.Project current = null, bool mergeConfig = false)
        {
            if (this.IsAspNetCoreEnabled())
                return;

            try
            {
                if (current == null)
                    current = this.GetBmdProject();
                if (current != null)
                {
                    string bmPath = this.GetFullPath("Linx.Business.Models");
                    if (bmPath.IsNullOrEmpty())
                        return;

                    if (!bmPath.IsNullOrEmpty() && Directory.Exists(bmPath))
                    {
                        string relativePath = Path.GetDirectoryName(current.FullName).GetRelativePath(bmPath);
                        if (!relativePath.IsNullOrEmpty())
                            bmPath = "$(ProjectDir)" + relativePath;

                        string assemblyName = GetAssemblyName(current) + ".dll";
                        string contextMetadataFile = this.GetContextMetadataFile(current);

                        string postBuildEventCommand = "";
                        if (!contextMetadataFile.IsNullOrEmpty())
                        {
                            string metadataFileName = assemblyName + ".meta.json";
                            postBuildEventCommand += @"xcopy """ + "$(ProjectDir)Model\\" + contextMetadataFile + @""" ""."" /Y /R" + "\r\n";
                            postBuildEventCommand += @"del """ + metadataFileName + @"""" + "\r\n";
                            postBuildEventCommand += @"rename """ + contextMetadataFile + @""" """ + metadataFileName + @"""" + "\r\n";
                        }

                        postBuildEventCommand += @"xcopy ""$(TargetName).dll*"" """ + bmPath + @""" /Y /R";

                        //Service Bus
                        string serviceBusPath = this.GetFullPath("Linx.Web.Service.Bus");
                        if (!serviceBusPath.IsNullOrEmpty() && Directory.Exists(serviceBusPath))
                        {
                            relativePath = Path.GetDirectoryName(current.FullName).GetRelativePath(serviceBusPath);
                            if (!relativePath.IsNullOrEmpty())
                                serviceBusPath = "$(ProjectDir)" + relativePath;
                            postBuildEventCommand += "\r\n" + @"xcopy ""$(TargetDir)*.dll"" """ + serviceBusPath + @"\bin"" /Y /R";

                            if (mergeConfig)
                            {
                                postBuildEventCommand += "\r\n" + @"cd """ + serviceBusPath + "\"" +
                                "\r\n" + @"XmlConfigMergeConsole ""Web.config"" -m ""$(ProjectDir)App.config""";
                            }
                        }

                        string postBuild = current.Properties.Item("PostBuildEvent").Value as string;
                        if (String.IsNullOrEmpty(postBuild) || !postBuild.Contains(postBuildEventCommand))
                            current.Properties.Item("PostBuildEvent").Value = postBuildEventCommand;
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("Problem by setting the PostBuildEvent of this project with the following message: " + exp.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void UpdateTemplates(Project bmProject)
        {
            if (this.IsAspNetCoreEnabled())
                return;

            this.UpdateDynamicModelsTemplate(bmProject);
            this.UpdateDynamicModelsTemplate(bmProject, true);
            this.UpdateContextTemplate();
            this.UpdateContextMetadata(bmProject);
            this.UpdateAppConfigTemplate();
            this.UpdateDomainViewsTemplate();
            this.GenerateBusinessOperations(false);
            this.GenerateBusinessOperations(true);
            if (this.GenerateCustomerCustomizationProject)
                this.GenerateCustomizationProject();
        }

        private void UpdateTemplates()
        {
            if (this.NoCode)
                return;

            try
            {
                var bmProject = this.GetBmdProject();
                //Sharing informations with other model of this project      
                //this.CreateEvents();
                this.SaveSharedInfo();

                //Generate template base structure
                this.UpdateTemplates(bmProject);

                //Remove inconsistences
                RemovePreGeneratedViewsInconsistencies(bmProject, true);

                //Generate Core Templates    
                this.UpdateCoreTemplates(bmProject);

                //Generate template base structure
                var contracts = this.GetContracts();
                if (contracts.Count > 0)
                {
                    this.GenerateContractsCode(contracts);
                    this.GenerateImplementationsCode(contracts);
                }

                //Generate webapi code
                if (this.WebApiControllers.Count > 0)
                {
                    this.GenerateWebApiAppStartCode(null);
                    this.GenerateWebApiControllersCode(null, null);

                    this.GenerateWebApiCoreInitialize(null);
                    this.GenerateWebApiCoreControllersCode(null, null);
                }
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message, "Alert executing templates", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


    }

}

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

namespace Linx.BusinessDataModelDesigner
{

    [ValidationState(ValidationState.Enabled)]
    public partial class BusinessDataModelDesignerRoot
    {

        [ValidationMethod(ValidationCategories.Menu | ValidationCategories.Save)]
        private void ValidateStateNamesUnique(ValidationContext context)
        {            
            //Check if exist BMDs out of the project.
            this.ValidLxdms(context);            
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
                //Update templates, references, etc.
                this.UpdateTemplates();
                //verify projects
                //this.AdjustProjects();
                //Check FK Erros
                this.ValidFKErrors();
            };
            docEvents.DocumentSaved += new EnvDTE._dispDocumentEvents_DocumentSavedEventHandler(afterSaver);
        }
        
        public void MoveProjectToSolutionFolder(Project project, string folderName)
        {
            var itemFolder = GetProjectByName(folderName);
            if (itemFolder == null)
            {
                ((EnvDTE100.Solution4)project.DTE.Solution).AddSolutionFolder(folderName);
                itemFolder = GetProjectByName(folderName);
            }

            if (!ExistsProjectItem(itemFolder.ProjectItems, project.Name))
            {
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

            if (prjReference != null)
                prjReference.CopyLocal = copyLocal;

            return prjReference;
        }

        public static void UpgradeVersion(Project project)
        {
            //Upgrade project to new Framework version if necessary        
            if ((((uint)project.Properties.Item("TargetFramework").Value) <= 0x00040000))
            {
                project.Properties.Item("TargetFrameworkMoniker").Value = (new System.Runtime.Versioning.FrameworkName(".NETFramework", new Version(4, 5))).FullName;
            }
        }

        private void SetPostBuildEvent(EnvDTE.Project current = null, bool mergeConfig = false)
        {
            try
            {
                if (current == null)
                    current = this.GetLxdmProject();
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

                        string postBuildEventCommand = @"xcopy ""$(TargetName).dll*"" """ + bmPath + @""" /Y /R";

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

                        string postBuild = current.Properties.Item("PostBuildEvent").Value.ToString();
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

        private void UpdateTemplates()
        {
            if (this.NoCode)
                return;

            try
            {
                //Sharing informations with other model of this project      
                //this.CreateEvents();
                this.SaveSharedInfo();
                //Generate template base structure
                CustomizedCode.CustomExtension.BusinessDataModelGen.GenerateCode(this);
                //SetGeneratedViews(this.GetLxdmProject(), true);
                
                ////Generate template base structure
                //var contracts = this.GetContracts();
                //if (contracts.Count > 0)
                //{
                //    this.GenerateContractsCode(contracts);
                //    this.GenerateImplementationsCode(contracts);
                //}

                ////Generate webapi code
                //if (this.WebApiControllers.Count > 0)
                //{
                //    this.GenerateWebApiControllersCode(null, null);
                //}
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.GetCompleteMessage(), "Alert while generating the code", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


    }

}

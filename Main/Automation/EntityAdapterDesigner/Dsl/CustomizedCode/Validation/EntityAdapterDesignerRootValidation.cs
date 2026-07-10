using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.Modeling.Diagrams;
using Microsoft.VisualStudio.Modeling.Validation;
using Microsoft.VisualStudio.Modeling;
using System.Globalization;
using Linx.Tools;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
using System.Linq;
using VSLangProj80;
using EnvDTE;
using System.IO;


namespace Linx.EntityAdapterDesigner
{
    [ValidationState(ValidationState.Enabled)]
    public partial class EntityAdapterDesignerRoot
    {
        [ValidationMethod(ValidationCategories.Menu | ValidationCategories.Save)]
        private void ValidateStateNamesUnique(ValidationContext context)
        {
            var spaProject = this.SpaCodeGen.GetSpaProject();
            if (spaProject != null)
            {
                string outputFile = Path.Combine(this.GetProjectPath(spaProject), "web.config");
                if (File.Exists(outputFile) && !this.VerifySourceControl(outputFile))
                    return;
            }

            this.ValidClientServices(context);
            this.ValidEads(context);
            this.ValidDomains(this.DomainViews, context);
            this.GenerateAlerts();

            //After save the document, update the storage
            Action<EnvDTE.Document> afterSaver = null;

            EnvDTE.ProjectItem document = null;

            try
            {
                Project diagramProject = this.GetEadProject();
                document = GetDiagramProjectItem(diagramProject);
            }
            catch (Exception ex)
            {
                CustomizedCode.Helpers.TreatException.LogError(ex);
                this.HasErrors = true;
                context.LogError(ex.Message, "Item Not Found Error", this);
                throw ex;
            }

            if (document == null)
                return;

            this.HasErrors = false;
            this.CheckVersion();

            var docEvents = (EnvDTE.DocumentEvents)document.DTE.Events.get_DocumentEvents(document.Document);
            afterSaver = (doc) =>
            {
                //Remove event
                docEvents.DocumentSaved -= new EnvDTE._dispDocumentEvents_DocumentSavedEventHandler(afterSaver);
                //Verify projects
                this.AdjustProjects();

                //Update templates, references, etc.
                if (this.HasStructuralChanges)
                {
                    this.UpdateTemplates();
                    this.UpdateCoreTemplates(this.GetEadProject());
                }

                //SPA Code Generation
                //Copy site resources from Shell
                this.SpaCodeGen.CopyShellFromSpaFolder();
                this.MobileCodeGen.CopyNodeToMobileFolder();
                //Generate bundle code
                this.SpaCodeGen.GenerateSpaModuleConfigCode();
                //Adjust config
                this.SpaCodeGen.AdjustSpaConflicts();

                //Save all pendencies
                var appDTE = GetDTE();
                if (appDTE != null)
                    appDTE.ExecuteCommand("File.SaveAll");

                this.SpaCodeGen.GenerateSpaUpdateInfo();


                if (this.HasErrors || context.CurrentViolations.Count > 0)
                {
                    using (Transaction transaction =
                          this.Store.TransactionManager.BeginTransaction("Changing Version."))
                    {
                        this.Version = "1.0.0.0";
                        this.HasStructuralChanges = true;
                        transaction.Commit();
                    }
                }
                else
                    this.HasStructuralChanges = false;

            };
            docEvents.DocumentSaved += new EnvDTE._dispDocumentEvents_DocumentSavedEventHandler(afterSaver);

            //Consist elements
            if (!(this.EntityAdapters.Count == 0 && this.DomainServiceExtensions.Count == 0))
            {
                Dictionary<string, ModelElement> elementNames = new Dictionary<string, ModelElement>();

                //Check primary keys
                foreach (EntityAdapter element in this.EntityAdapters.Where(e => e.BaseEntityAdapter == null))
                {
                    //Check inconsistent keys
                    foreach (var prop in element.EntityAdapterProperties.Where(e => element.IsInconsistentPrimaryKey(e)))
                    {
                        string description = String.Format(CultureInfo.CurrentCulture, "The Property '{0}' of view '{1}' is an inconsistent primary key because it is nullable.", prop.Name, element.Name);
                        context.LogError(description, "Primary Key Inconsistency", element, prop);
                    }
                    //Check primary key
                    if (!element.HasPrimaryKey() && !element.HasDynamicPrimaryKey())
                    {
                        string description = String.Format(CultureInfo.CurrentCulture, "The Entity '{0}' has no primary key.", element.Name);
                        context.LogError(description, "Primary Key Error", element);
                    }
                }

                //Check Base Entity and its children
                foreach (EntityAdapter element in this.EntityAdapters.Where(e => e.DerivedEntityAdapters.Count > 0 && e.SourceEntityAdapters.Count > 0))
                {
                    string description = String.Format(CultureInfo.CurrentCulture, "Children entities cannot be associated with base classes. Verify the base entity '{0}'.", element.Name);
                    context.LogError(description, "Base Entity Error", element);
                }

                //Check Entities
                foreach (EntityAdapter element in this.EntityAdapters)
                {
                    if (elementNames.ContainsKey(element.Name))
                    {
                        string description = String.Format(CultureInfo.CurrentCulture, "The Entity Name '{0}' is used more than once.", element.Name);
                        context.LogError(description, "Unique Name Error", element, elementNames[element.Name]);
                    }
                    elementNames[element.Name] = element;
                }

                //Check LookUpAdapters
                foreach (LookUpAdapter element in this.LookUpAdapters)
                {
                    if (elementNames.ContainsKey(element.Name))
                    {
                        string description = String.Format(CultureInfo.CurrentCulture, "The Lookup Name '{0}' is used more than once.", element.Name);
                        context.LogError(description, "Unique Name Error", element, elementNames[element.Name]);
                    }
                    elementNames[element.Name] = element;
                }

                //Check Edms
                foreach (EntityDataModel element in this.EntityDataModels)
                {
                    if (elementNames.ContainsKey(element.Name))
                    {
                        string description = String.Format(CultureInfo.CurrentCulture, "The DataContext Name '{0}' is used more than once.", element.Name);
                        context.LogError(description, "Unique Name Error", element, elementNames[element.Name]);
                    }
                    elementNames[element.Name] = element;
                }

                //Check DomainServiceExtension
                foreach (DomainServiceExtension element in this.DomainServiceExtensions)
                {
                    if (elementNames.ContainsKey(element.Name))
                    {
                        string description = String.Format(CultureInfo.CurrentCulture, "The DomainService Name '{0}' is used more than once.", element.Name);
                        context.LogError(description, "Unique Name Error", element, elementNames[element.Name]);
                    }
                    elementNames[element.Name] = element;
                }

                //Check LookUpAdapter properties.
                foreach (LookUpAdapter entity in this.LookUpAdapters)
                {
                    elementNames.Clear();

                    //Check Properties     
                    foreach (var element in entity.GetAllInheritanceAttributes())
                    {
                        if (elementNames.ContainsKey(element.Name))
                        {
                            string description = String.Format(CultureInfo.CurrentCulture, "The Property Name '{0}' is used more than once.", element.Name);
                            context.LogError(description, "Unique Name Error", element, elementNames[element.Name]);
                        }
                        elementNames[element.Name] = element;
                    }
                }

                //Check entity attributes and operations.        
                foreach (EntityAdapter entity in this.EntityAdapters)
                {
                    elementNames.Clear();

                    //Check Properties     
                    foreach (var element in entity.GetAllInheritanceAttributes())
                    {
                        if (elementNames.ContainsKey(element.Name))
                        {
                            string description = String.Format(CultureInfo.CurrentCulture, "The Property Name '{0}' is used more than once.", element.Name);
                            context.LogError(description, "Unique Name Error", element, elementNames[element.Name]);
                        }
                        elementNames[element.Name] = element;
                    }

                    elementNames.Clear();
                    //Check Operations
                    foreach (var element in entity.EntityAdapterOperations)
                    {
                        if (elementNames.ContainsKey(element.Name))
                        {
                            string description = String.Format(CultureInfo.CurrentCulture, "The Operation Name '{0}' is used more than once.", element.Name);
                            context.LogError(description, "Unique Name Error", element, elementNames[element.Name]);
                        }
                        elementNames[element.Name] = element;
                    }

                    elementNames.Clear();
                    //Check Events
                    foreach (var element in entity.EntityAdapterEvents)
                    {
                        if (elementNames.ContainsKey(element.Name))
                        {
                            string description = String.Format(CultureInfo.CurrentCulture, "The Event Name '{0}' is used more than once.", element.Name);
                            context.LogError(description, "Unique Name Error", element, elementNames[element.Name]);
                        }
                        elementNames[element.Name] = element;
                    }

                }

                //Check operations.        
                foreach (DomainServiceExtension serviceContractExt in this.DomainServiceExtensions)
                {
                    //Reset analisys
                    elementNames.Clear();

                    //Check Service Contract Operations
                    foreach (var element in serviceContractExt.DomainServiceOperations)
                    {
                        if (elementNames.ContainsKey(element.Name))
                        {
                            string description = String.Format(CultureInfo.CurrentCulture, "The DomainServiceOperation Name '{0}' is used more than once.", element.Name);
                            context.LogError(description, "Unique Name Error", element, elementNames[element.Name]);
                        }
                        elementNames[element.Name] = element;
                    }
                }

                //Check operations.        
                foreach (DomainServiceExtension serviceContractExt in this.DomainServiceExtensions)
                {
                    //Reset analisys
                    elementNames.Clear();

                    //Check Service Contract Operations
                    foreach (var element in serviceContractExt.DomainServiceOperations)
                    {
                        if (elementNames.ContainsKey(element.OverloadName))
                        {
                            string description = String.Format(CultureInfo.CurrentCulture, "The DomainServiceOperation OverloadName '{0}' is used more than once.", element.OverloadName);
                            context.LogError(description, "Unique Name Error", element, elementNames[element.OverloadName]);
                        }
                        elementNames[element.OverloadName] = element;
                    }
                }
            }

            if (this.HasStructuralChanges)
            {
                //Adjust structural informations
                using (Transaction transaction =
                                this.Store.TransactionManager.BeginTransaction("Changing Structure."))
                {
                    //Adjust Invalid Informations
                    this.AdjustInvalidEntities();

                    //Sync lookups from subscriptios
                    this.AdjustEntityLookupsInfoFromSubscription();

                    //Adjust MacroScripts
                    this.AdjustMacroScripts();

                    //Adjust Lookups
                    this.AdjustLookups();

                    //Data service
                    this.AdjustDataService();

                    //Adjust namespace
                    this.AdjustNamespace();

                    //Generate UIs
                    this.SaveLayoutDetails();

                    //Sync Edm File Path
                    this.SyncEdmFilePath();

                    transaction.Commit();
                }
            }
        }

        private void AdjustInvalidEntities()
        {
            var entitiesForNoTracking = this.EntityAdapters.Where(e => e.IsLargeDataMode && e.TargetEntityAdapter != null).ToArray();
            if (entitiesForNoTracking.Length > 0)
            {
                using (Transaction transaction =
                           this.Store.TransactionManager.BeginTransaction("Adjust Detail LargeData."))
                {
                    foreach (var entity in entitiesForNoTracking)
                    {
                        entity.IsLargeDataMode = false;
                    }
                    transaction.Commit();
                }
            }

            if (this.IsLargeDataMode())
            {
                var entitiesForSync = this.EntityAdapters.Where(e => !e.IsLargeDataMode && e.TargetEntityAdapter == null).ToArray();
                if (entitiesForSync.Length > 0)
                {
                    using (Transaction transaction =
                               this.Store.TransactionManager.BeginTransaction("Adjust Parent LargeData."))
                    {
                        foreach (var entity in entitiesForSync)
                        {
                            entity.IsLargeDataMode = true;
                        }
                        transaction.Commit();
                    }
                }
            }

        }

        private void AdjustEntityLookupsInfoFromSubscription()
        {
            foreach (var entity in this.EntityAdapters)
                entity.AdjustLookupsInfoFromSubscription();
        }

        private void AdjustMacroScripts()
        {
            foreach (var entity in this.EntityAdapters.ToArray())
            {
                foreach (var method in entity.EntityAdapterClientEvented.Where(e => !e.MacroScript.IsNullOrEmpty() && (e.MacroScript.Contains("$('#") || e.MacroScript.Contains("$(\"#"))).ToArray())
                {
                    method.MacroScript = method.MacroScript.Replace("$('#", "$lx(vm,'#").Replace("$(\"#", "$lx(vm,\"#");
                }
            }

            foreach (var ui in this.EntityAdapterUserInterfaces.ToArray())
            {
                foreach (var method in ui.UserInterfaceClientEvented.Where(e => !e.MacroScript.IsNullOrEmpty() && (e.MacroScript.Contains("$('#") || e.MacroScript.Contains("$(\"#"))).ToArray())
                {
                    method.MacroScript = method.MacroScript.Replace("$('#", "$lx(vm,'#").Replace("$(\"#", "$lx(vm,\"#");
                }
            }
        }

        private void AdjustLookups()
        {
            foreach (var lookup in this.LookUpAdapters.Where(e => !e.EntitySource.IsNullOrEmpty()).ToArray())
            {
                if (lookup.DisableSpecializedUI)
                {
                    if (!lookup.SpecializedUI.IsNullOrEmpty())
                        lookup.SpecializedUI = "";
                }
                else
                {
                    string specUI = this.GetSpecializedLookupInfo(lookup.EntitySource);
                    if (!specUI.IsNullOrEmpty() && lookup.SpecializedUI != specUI)
                    {
                        lookup.SpecializedUI = specUI;
                    }
                }
            }
        }

        private void ValidDomains(IEnumerable<DomainView> types, ValidationContext context)
        {
            //Check Domain informations
            foreach (DomainView element in types)
            {
                string inconsistenceInfo = element.GetInconsistenceInfo();
                if (!inconsistenceInfo.IsNullOrEmpty())
                {
                    string description = String.Format(CultureInfo.CurrentCulture, "The DomainView '{0}' has the following inconsistency: {1}.", element.Name, inconsistenceInfo);
                    context.LogError(description, "DomainView Error", element);
                }
            }
        }

        private void ValidClientServices(ValidationContext context)
        {
            //Check Mobile UIs
            foreach (var ui in this.EntityAdapterUserInterfaces.Where(e => e.VisualType == InterfaceType.Mobile && e.GetDirectEntityAdapter() != null))
            {
                if (ui.ClientLocalService == null)
                {
                    string description = String.Format(CultureInfo.CurrentCulture, "The user interface '{0}' needs a 'Local Service' associated.", ui.Name);
                    context.LogError(description, "User Interface Error", ui);
                }
            }
        }

        private void ValidEads(ValidationContext context)
        {
            if (!IsMainWindowVisible() || IsAutomaticSaving) return;

            var eadProj = this.GetEadProject();

            List<string> eadList = new List<string>();
            foreach (ProjectItem item in eadProj.ProjectItems)
            {
                if (System.IO.Path.GetExtension(item.Name).ToLower() == ".ead")
                    eadList.Add(item.Name.ToLower());
            }

            //Check inconsistent files
            string alertFileList = String.Empty;
            foreach (var file in System.IO.Directory.GetFiles(eadProj.Properties.Item("FullPath").Value.ToString(), "*.ead").Select(e => System.IO.Path.GetFileName(e).ToLower()).Where(e => !eadList.Contains(e)))
            {
                alertFileList += (alertFileList.IsNullOrEmpty() ? "" : ", ") + file;
            }

            if (!alertFileList.IsNullOrEmpty())
            {
                string description = String.Format(CultureInfo.CurrentCulture, "The files '{0}' exist in the project folder, but not in the project reference.", alertFileList);
                context.LogError(description, "EADs Error");
            }
        }

        private void CheckEdmReferencesByEntityRepresentation(EnvDTE.Project diagramProject)
        {
            string edmLib, edmFileName;
            foreach (var rep in this.EntityAdapterRepresentations.Where(e => !e.BusinessObject.IsNullOrEmpty() && !e.TargetEdmName.IsNullOrEmpty()))
            {
                edmFileName = (rep.TargetEdmName + "#").Left("." + rep.TargetEdmName.Right(".") + "#") + ".dll";
                edmLib = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(rep.BusinessObject), edmFileName);
                if (System.IO.File.Exists(edmLib))
                {
                    this.AddNewReference(diagramProject, edmLib, System.IO.Path.GetFileName(rep.BusinessObject) == System.IO.Path.GetFileName(this.GetOutputLibFile()));
                }
            }
        }

        public bool IsTCS()
        {
            return this.TargetNamespace.Contains("Linx.Framework.BV");
        }

        public bool HasDomainServiceExtension()
        {
            return RepositoryInterfaces.Where(i => i.IsExtension).Count() > 0;
        }

        public string DomainServiceExtensionName()
        {
            string domainServiceExtension = string.Empty;

            if (this.HasDomainServiceExtension())
                domainServiceExtension = RepositoryInterfaces.Where(i => i.IsExtension).FirstOrDefault().Name;

            return domainServiceExtension;
        }

        List<string> GetAssemblyNames()
        {
            List<string> assemplyNames = new List<string>();

            var vs = GetDTE();
            if (vs != null)
            {
                foreach (EnvDTE.Project project in vs.Solution.Projects)
                {
                    foreach (ProjectItem item in project.ProjectItems)
                    {
                        if (item.SubProject != null)
                        {
                            Project curPrj = item.SubProject;
                            string assName = this.GetAssemblyName(curPrj).ToLower();
                            if (!assemplyNames.Contains(assName)) assemplyNames.Add(assName);
                            VSLangProj.VSProject vsProject = (VSLangProj.VSProject)curPrj.Object;
                            if (vsProject != null)
                            {
                                foreach (var reference in vsProject.References)
                                {
                                    if (reference is Reference3)
                                    {
                                        assName = ((Reference3)reference).Name.ToLower();
                                        if (!assemplyNames.Contains(assName)) assemplyNames.Add(assName);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //Add BVs from SPAs
            foreach (var fileName in assemplyNames.Where(e => e.Length > 4 && e.Right(4) == ".spa").Select(e => e.Left(e.Length - 4)).ToArray())
            {
                if (!assemplyNames.Contains(fileName))
                    assemplyNames.Add(fileName);
            }

            //Add BVs from MObile App
            foreach (var fileName in assemplyNames.Where(e => e.Length > 7 && e.Right(7) == ".mobile").Select(e => e.Left(e.Length - 7)).ToArray())
            {
                if (!assemplyNames.Contains(fileName))
                    assemplyNames.Add(fileName);
            }

            return assemplyNames;
        }


        /// <summary>
        /// Adjust Service Bus Performance 
        /// </summary>
        public void CleanServiceBus()
        {
            if (IsMainWindowVisible() && !IsAutomaticSaving && this.GetDTE() != null)
            {
                try
                {
                    string serviceBusPath = this.GetFullPath("Linx.Web.Service.Bus");
                    if (System.IO.Directory.Exists(serviceBusPath))
                    {
                        string binPath = System.IO.Path.Combine(serviceBusPath, "bin");
                        List<string> assemplyNames = GetAssemblyNames();
                        foreach (var file in System.IO.Directory.GetFiles(binPath, "Linx.*.BV.*dll"))
                        {
                            if (!file.Contains(".Framework.") && !file.Contains(".Report.Access.") && !file.Contains(".Portal.Desenvolvimento."))
                            {
                                string fileNoExt = System.IO.Path.GetFileNameWithoutExtension(file).ToLower();
                                if (!assemplyNames.Any(e => fileNoExt.StartsWith(e)))
                                {
                                    LinxDirectoryInfo.RemoveReadOnlyAttribute(file);
                                    System.IO.File.Delete(file);
                                }
                            }
                        }
                        foreach (var file in System.IO.Directory.GetFiles(binPath, "*.dll.config"))
                        {
                            LinxDirectoryInfo.RemoveReadOnlyAttribute(file);
                            System.IO.File.Delete(file);
                        }
                    }
                }
                catch (Exception excp)
                {
                    CustomizedCode.Helpers.TreatException.LogError(excp);
                    this.HasErrors = true;
                    MessageBox.Show(excp.GetCompleteMessage("Fail cleaning the service bus binary"), "Alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

        }

        private void AdjustProjects()
        {
            EnvDTE.Project diagramProject = this.GetEadProject();
            
            //Update extended references
            if (!diagramProject.IsNull())
            {
                //Adjust Business Rules Folder
                if (diagramProject != null)
                {
                    if (MoveProjectToSolutionFolder(diagramProject, "Business Rules"))
                    {
                        var dte = this.GetDTE();
                        if (dte != null)
                        {
                            dte.ExecuteCommand("File.SaveAll");
                            diagramProject = this.GetEadProject();
                        }
                    }
                }

                //Adjust post build event
                this.SetPostBuildEventToServiceBus();

                if (this.IsAspNetCore)
                {
                    //Asp Net Core Business Project
                    this.UpdateEadCoreProject(diagramProject);

                    //WebApis
                    foreach (var api in this.WebApiControllers.Where(e => e.WebApiActions.Count > 0 || e.SynchronizedWithDomainService))
                    {
                        //Update Web API Core Project
                        this.UpdateWebApiCoreProject(diagramProject, api);
                    }
                }
                else
                {
                    //this.RemoveReferencesWithoutFile(diagramProject);
                    this.AdjustMissingReferences(diagramProject);
                    this.UpdateVersion(diagramProject);

                    if (this.EntityAdapters.Count > 0)
                        this.UpdateReportProject(true);

                    this.UpdateReportUtilsFile(null, diagramProject);

                    //Update Report Version
                    UpdateReportReferences(diagramProject);

                    this.CheckEdmReferencesByEntityRepresentation(diagramProject);

                    string gacPath = this.GetFullPath("Linx.GAC") ?? "";
                    this.UpdateReference(diagramProject, Path.Combine(gacPath, "Linx.Tools.dll"));
                    this.UpdateReference(diagramProject, Path.Combine(gacPath, "Linx.LinqExtensions.dll"));
                    this.RemoveLibReferences(diagramProject, "Linx.EFProviderWrapper.Library");
                    this.RemoveReference(diagramProject, "System.Data.Entity");

                    //Remove business tools of TCS
                    if (IsTCS())
                        this.RemoveReference(diagramProject, "Linx.Business.Tools.dll");
                    else  //Update business tools reference
                        this.UpdateLibReferences(diagramProject, "Linx.Business.Desktop.Tools", false);

                    //Remove dependencies between DomainService and the EntityFramework
                    this.RemoveReference(diagramProject, "Microsoft.ServiceModel.DomainServices.EntityFramework.dll");
                    this.RemoveReference(diagramProject, "System.ServiceModel.DomainServices.EntityFramework.dll");

                    //Update data reference
                    this.UpdateLibReferences(diagramProject, "Linx.DomainServices", false);
                    this.UpdateLibReferences(diagramProject, "Linx.Data.Library", false);
                    this.UpdateLibReferences(diagramProject, "Linx.CodeFirst.EF", false);

                    //Add IdentityModel reference
                    if (!this.ExistsReference(diagramProject, "System.IdentityModel.dll"))
                        this.AddNewReference(diagramProject, "System.IdentityModel.dll");

                    if (!this.ExistsReference(diagramProject, "System.Net.Http.dll"))
                        this.AddNewReference(diagramProject, "System.Net.Http.dll");

                    //Update WF suport references
                    if (!this.ExistsReference(diagramProject, "PresentationFramework.dll"))
                        this.AddNewReference(diagramProject, "PresentationFramework.dll");
                    if (!this.ExistsReference(diagramProject, "PresentationCore.dll"))
                        this.AddNewReference(diagramProject, "PresentationCore.dll");
                    if (!this.ExistsReference(diagramProject, "System.Xaml.dll"))
                        this.AddNewReference(diagramProject, "System.Xaml.dll");
                    if (!this.ExistsReference(diagramProject, "System.Activities.dll"))
                        this.AddNewReference(diagramProject, "System.Activities.dll");
                    if (!this.ExistsReference(diagramProject, "System.Activities.Presentation.dll"))
                        this.AddNewReference(diagramProject, "System.Activities.Presentation.dll");

                    //AdoMD
                    this.UpdateLibReferences(diagramProject, "Linx.AdomdClient", false, false, true, true);

                    //MEF Extension
                    if (!this.ExistsReference(diagramProject, "System.ComponentModel.Composition"))
                        this.AddNewReference(diagramProject, "System.ComponentModel.Composition");

                    if (!this.IsInPresentationDesigner())
                    {
                        //Update references by subscriptions
                        foreach (var sub in this.Subscriptions)
                        {
                            sub.UpdateReferences(diagramProject);
                        }
                    }

                    //Updating WebApi and Repository Projects
                    //Repositories
                    foreach (var intf in this.RepositoryInterfaces.Where(e => e.RepositoryMethods.Count > 0))
                    {
                        foreach (var repository in intf.RepositoryImplementations)
                            this.UpdateRepositoryProject(diagramProject, repository);
                    }

                    //WebApis
                    foreach (var api in this.WebApiControllers.Where(e => e.WebApiActions.Count > 0 || e.SynchronizedWithDomainService))
                    {
                        this.UpdateWebApiProject(diagramProject, api);
                    }

                    //User Extension
                    if (this.HasDomainServiceExtension())
                        this.UpdateUserExtensionProject(diagramProject);

                    //Upgrade to last framework version
                    UpgradeVersion(diagramProject);
                }


                //Verify inconsistent files
                this.DeleteInconsistentFiles(diagramProject);

                //Create/Update User Interface Projects
                UpdateUserInterfaceSolution();
                                
            }            
        }

        private bool HasDomains()
        {
            return this.DomainViews.Count > 0 || this.EntityDataModels.Any(e => e.EdmInfo.IsDbContext) || this.Subscriptions.Any(e => e.Publisher != null && e.Publisher.Domains.Count > 0);
        }

        private void UpdateTemplates()
        {
            if (this.IsAspNetCore)
                return;

            //Templates
            try
            {
                var eadProject = this.GetEadProject();
                this.UpdateDataEntityFunctionsTemplate(eadProject);
                this.UpdateEntityAdapterDynamicModelsTemplate(eadProject);
                this.UpdateEntityAdapterDynamicModelsTemplate(eadProject, true);

                //Generate template base structure
                if (this.RepositoryInterfaces.Count > 0)
                    this.GenerateRepositoriesCode();

                if (this.WebApiControllers.Count > 0)
                {
                    this.GenerateWebApiClientCode(eadProject);
                    this.GenerateWebApiAtomODataCode();
                    this.GenerateWebApiControllersCode();
                }

                if (this.EntityDataModels.Count > 0)
                    this.DeleteExtendedEdmTemplate();


                this.UpdateDomainViewsTemplate(eadProject, "DomainViewsTemplate", "DomainViews");
                this.UpdateDomainViewsTemplate(eadProject, "ClientDataDomains", "DataDomains");
                this.UpdateDomainViewsTemplate(eadProject, "MobileDataDomains", "MobileDataDomains");
                this.UpdateDomainViewsTemplate(eadProject, "ClientErpDataDomainsFactory", "ClientErpDataDomainsFactory");
                this.UpdateClientServicesResources(eadProject);

                if (this.KeyPerformanceIndicators.Count > 0)
                    this.UpdateKPIViewsTemplate(eadProject);


                if (!(this.EntityAdapters.Count == 0 && this.DomainServiceExtensions.Count == 0))
                {
                    //Generate Wfs
                    this.GenerateWorkflows();
                    //Generate Activities
                    this.GenerateActivities();

                    //Templates
                    this.UpdateAppConfigTemplate();
                    this.UpdateDomainServiceTemplate(eadProject);
                    this.UpdateFormulasTemplate(eadProject);
                    this.UpdateExtendedFiltersTemplate(eadProject);
                    this.UpdateLookUpsTemplate(eadProject);

                    //Operations
                    this.GenerateBusinessEvents(eadProject, false);
                    this.GenerateBusinessEvents(eadProject, true);
                    this.GenerateBusinessOperations(eadProject, false);
                    this.GenerateBusinessOperations(eadProject, true);
                    this.GenerateDomainServiceExtensions(eadProject);
                    this.CheckCustomValidationClass(eadProject);
                    this.GenerateCustomAuthorizationClass(eadProject);

                    //User Extension Project
                    if (this.HasDomainServiceExtension())
                        this.GenerateDomainServiceInterfaceImplementation(eadProject);
                }
            }
            catch (UnauthorizedAccessException uae)
            {
                CustomizedCode.Helpers.TreatException.LogError(uae);
                this.HasErrors = true;
                MessageBox.Show(uae.Message, "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                CustomizedCode.Helpers.TreatException.LogError(ex);
                this.HasErrors = true;
                MessageBox.Show("Save the designer again for executing all templates correctly.", "Executing templates", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void UpdateReference(EnvDTE.Project project, string reference, bool remove = false, bool copyLocal = false, bool specificVersion = false, bool compareFileLocation = true)
        {
            VSLangProj.Reference refItem = null;

            if (remove)
                RemoveReference(project, reference);
            else
                refItem = GetReference(project, reference);

            if (refItem != null && refItem.Path.IsNullOrEmpty())
            {
                refItem.Remove();
                refItem = null;
            }

            //Check path
            if (refItem != null && compareFileLocation)
            {
                string assemblyPath = System.IO.Path.GetDirectoryName(reference).ToLower();
                if (!assemblyPath.IsNullOrEmpty() && System.IO.Path.GetDirectoryName(refItem.Path).ToLower() != assemblyPath)
                {
                    refItem.Remove();
                    refItem = null;
                }
            }

            if (refItem == null)
                refItem = AddNewReference(project, reference, copyLocal, specificVersion);
            else
            {
                refItem.CopyLocal = copyLocal;
                if (refItem is Reference3 && ((Reference3)refItem).SpecificVersion != specificVersion)
                    ((Reference3)refItem).SpecificVersion = specificVersion;
            }
        }

    }
}

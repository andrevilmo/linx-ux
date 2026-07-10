using EnvDTE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linx.Tools;
using Linx.Builder.Resources;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms;
using Linx.EntityAdapterDesigner.CustomizedCode.Helpers;

namespace Linx.EntityAdapterDesigner.CustomizedCode.Apps.SPA
{
    public class SpaCodeGen
    {
        private const bool enableSaveLazingMode = true;
        private const string patternForDateTimeConstructor = @"^new(\s+)DateTime(\s*)\(\d{4},(\s*)\d{2},(\s*)\d{2}\)$";
        private const string patternForLinxParameter = @"^\[(\w+)\]$";
        private Guid viewModelCustomSourceCodeUid = new Guid("1CAC1A87-C063-4D71-BA30-9EC3CA3AC3DF");

        private EntityAdapterDesignerRoot _designerRoot;
        public SpaCodeGen(EntityAdapterDesignerRoot designerRoot)
        {
            _designerRoot = designerRoot;
        }

        #region SPA Project Functions



        /// <summary>
        /// App folder from SPA application.
        /// </summary>
        /// <param name="folderName"></param>
        /// <returns></returns>
        public ProjectItem GetSpaAppFolder(string folderName)
        {
            var appItem = GetSpaFolder("App");
            if (appItem != null && !folderName.IsNullOrEmpty())
            {
                var item = _designerRoot.GetProjectItemByName(appItem.ProjectItems, folderName);
                if (item == null)
                {
                    DTE appDTE = _designerRoot.GetDTE();
                    string solutionDir = Path.GetDirectoryName(((EnvDTE100.Solution4)appDTE.Solution).FullName);
                    string assemblyName = GetSpaProjectName();
                    string spaProjectName = assemblyName;
                    string projectDir = Path.Combine(solutionDir, spaProjectName) + @"\app\" + folderName;

                    if (System.IO.Directory.Exists(projectDir))
                    {
                        item = appItem.ProjectItems.AddFromDirectory(projectDir);
                    }
                    else
                    {
                        item = appItem.ProjectItems.AddFolder(folderName, Constants.vsProjectItemKindPhysicalFolder);
                    }
                }
                return item;
            }
            return null;
        }

        /// <summary>
        /// SPA folder from the current solution.
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        public ProjectItem GetSpaFolder(string folder)
        {
            var spaProject = GetSpaProject();

            if (spaProject != null)
            {
                var item = _designerRoot.GetProjectItemByName(spaProject.ProjectItems, folder);

                if (item == null)
                    item = spaProject.ProjectItems.AddFolder(folder, Constants.vsProjectItemKindPhysicalFolder);

                return item;
            }
            else
                return null;
        }

        /// <summary>
        /// SPA project.
        /// </summary>
        /// <returns></returns>
        public Project GetSpaProject()
        {
            var eadProject = _designerRoot.GetEadProject();
            if (eadProject != null)
            {
                return _designerRoot.GetProjectByName(this.GetSpaProjectName(eadProject));
            }
            else return null;
        }

        /// <summary>
        /// SPA project name.
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public string GetSpaProjectName(Project project = null)
        {
            if (project == null)
                project = _designerRoot.GetEadProject();
            if (project == null)
                return String.Empty;
            else
                return GetSpaName(project);
        }

        public static string GetSpaName(Project project)
        {
            return project.Name + ".SPA";
        }

        /// <summary>
        /// Update/Create SPA project.
        /// </summary>
        public void UpdateSPAProject()
        {
            if (_designerRoot.EntityAdapterUserInterfaces.Where(e => e.VisualType == InterfaceType.Web).Count() == 0)
                return;

            DTE appDTE = _designerRoot.GetDTE();
            if (appDTE == null)
                return;


            string folderName = "SPA User Interface Modules";
            string solutionName = Path.GetFileNameWithoutExtension(((EnvDTE100.Solution4)appDTE.Solution).FullName),
            solutionDir = Path.GetDirectoryName(((EnvDTE100.Solution4)appDTE.Solution).FullName);
            string assemblyName = GetSpaProjectName();
            string spaProjectName = assemblyName;
            Project spaProject = _designerRoot.GetProjectByName(assemblyName);
            string projectDir = Path.Combine(solutionDir, spaProjectName);
            bool saveSolution = false;


            //Add SPA User Interface Modules
            if (spaProject == null)
            {
                if (!System.IO.Directory.Exists(projectDir))
                    System.IO.Directory.CreateDirectory(projectDir);

                var tmpProj = _designerRoot.GetProjectByName(folderName);
                EnvDTE80.SolutionFolder businessDesignerFolder = (tmpProj == null ? null : tmpProj.Object) as EnvDTE80.SolutionFolder;
                if (businessDesignerFolder == null)
                    businessDesignerFolder = (EnvDTE80.SolutionFolder)((EnvDTE100.Solution4)appDTE.Solution).AddSolutionFolder(folderName).Object;

                if (System.IO.File.Exists(System.IO.Path.Combine(projectDir, spaProjectName + ".csproj")))
                {
                    businessDesignerFolder.AddFromFile(System.IO.Path.Combine(projectDir, spaProjectName + ".csproj"));
                }
                else
                {
                    // Get the location of the project templates
                    string templateName = ((EnvDTE100.Solution4)appDTE.Solution).GetProjectTemplate("Linx SPA Modules.zip", "CSharp");
                    businessDesignerFolder.AddFromTemplate(templateName, projectDir, spaProjectName);
                    saveSolution = true;
                }

                //Get SPA Project
                spaProject = _designerRoot.GetProjectByName(businessDesignerFolder.DTE, spaProjectName);

                //Generate MVVM Files
                this.GenerateSpaFiles(true);
            }

            //Update API References
            if (spaProject != null)
            {
                _designerRoot.UpdateVersion(spaProject);
                //_designerRoot.RemoveReferencesWithoutFile(spaProject);
                _designerRoot.RemoveLibReferences(spaProject, "Linx.WebApi.Library");
                _designerRoot.UpdateLibReferences(spaProject, "Linx.Internet.Framework", false);

                //Set PostBuildEvent
                _designerRoot.SetPostBuildEvent(spaProject, "Linx.SPA.Output");

                //Upgrade to last framework version
                EntityAdapterDesignerRoot.UpgradeVersion(spaProject);

                if (saveSolution) //Save solution
                {
                    appDTE.ExecuteCommand("File.SaveAll");
                }
            }
        }

        private string GenerateNavigationReplaces(EntityAdapter detailEntity, string detailAlias, string parentAlias)
        {
            string replaceResult = "";
            if (detailEntity.TargetEntityAdapter != null)
            {
                if (detailEntity.TargetEntityAdapter.HasDynamicPrimaryKey())
                {
                    replaceResult = "setAbsoluteValue(" + detailAlias + ", 'EntityParentUniqueKey', getAbsoluteValue(" + parentAlias + ".EntityUniqueKey));";
                }
                else
                {
                    foreach (var attribute in detailEntity.GetExtraParentRelationKey())
                    {
                        replaceResult += (replaceResult.IsNullOrEmpty() ? "" : " ") + "setAbsoluteValue(" + detailAlias + ", '" + attribute.Name + "', getAbsoluteValue(" + parentAlias + "." + attribute.Name + "));";
                    }
                }
            }
            return replaceResult;
        }

        /// <summary>
        /// Generating SPA files.
        /// </summary>
        /// <param name="force"></param>
        public void GenerateSpaFiles(bool force = false)
        {
            if (this.GetSpaProject() == null)
                return;

            try
            {
                _designerRoot.VerifyPublisherAutoReference();
                //SPA Code Generation
                MacroScriptEngine msEngine = new MacroScriptEngine();
                this.GenerateSpaModelCode(msEngine);
                this.AddDataDomainsReferenceToSpaService();
                this.GenerateSpaViewAndViewModelCode(force, msEngine);
                this.GenerateSpaViewModelCustomCode(force);
                this.RemoveDataComboFile();
            }
            catch (Exception ex)
            {
                CustomizedCode.Helpers.TreatException.LogError(ex);
                MessageBox.Show("An error occurred while generating the SPA.\n" + ex.Message, "Error gerating", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        /// <summary>
        /// Copy Shell struture to local project
        /// </summary>
        public void CopyShellFromSpaFolder()
        {
            //Getting SPA Project
            var spaProject = this.GetSpaProject();

            if (spaProject == null)
                return;

            string spaFolders = _designerRoot.GetFullPath("Linx.SPA.Folder");
            string projectPath = spaProject.Properties.Item("FullPath").Value.ToString();
            try
            {
                if (Directory.Exists(projectPath) && Directory.Exists(spaFolders))
                {
                    LinxDirectoryInfo.DirectoryCopy(spaFolders, projectPath, true);
                }
            }
            catch (Exception excep)
            {
                CustomizedCode.Helpers.TreatException.LogError(excep);
                MessageBox.Show(excep.Message, "Fail when copying shared SPA folders", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Generate module configuration.
        /// </summary>
        public void GenerateSpaModuleConfigCode()
        {
            string outputFile, className;
            ProjectItem item;
            Linx.Tools.CodeBuilder codeBuilder;

            item = this.GetSpaFolder("App_Start");
            if (!item.IsNull())
            {
                className = "ModuleConfig";
                outputFile = Path.Combine(item.Properties.Item("FullPath").Value.ToString(), className + ".cs");
                codeBuilder = new Linx.Tools.CodeBuilder();
                this.GenerateSpaModuleConfigCode(codeBuilder);

                _designerRoot.WriteFile(outputFile, codeBuilder, item.ProjectItems);
            }
        }

        /// <summary>
        /// Generate update notification.
        /// </summary>
        public void GenerateSpaUpdateInfo()
        {
            string outputFile;
            ProjectItem item = this.GetSpaFolder("App");
            if (!item.IsNull())
            {
                outputFile = Path.Combine(item.Properties.Item("FullPath").Value.ToString(), "info.txt");
                File.WriteAllText(outputFile, "\"" + _designerRoot.GetNamespace(this.GetSpaProject()).Replace(".", "-").ToLower() + "services\"");
            }
        }

        public bool VerifySourceControl(string file)
        {
            DTE appDTE = _designerRoot.GetDTE();
            if (appDTE == null)
                return false;

            return Linx.SourceControl.TfsAccess.VerifySourceControl(appDTE, file);
        }

        /// <summary>
        /// Adjust conflicts.
        /// </summary>
        public void AdjustSpaConflicts()
        {
            Project current = this.GetSpaProject();
            if (current != null)
            {
                string fileName = "web.config";

                if (!EntityAdapterDesignerRoot.ExistsProjectItem(current.ProjectItems, fileName))
                {
                    string outputFile = Path.Combine(_designerRoot.GetProjectPath(current), fileName);
                    if (File.Exists(outputFile))
                        current.ProjectItems.AddFromFile(outputFile);
                }

                var views = this.GetSpaAppFolder("views");
                if (views != null)
                {
                    foreach (var dir in Directory.GetDirectories(views.Properties.Item("FullPath").Value.ToString()))
                    {
                        Directory.Delete(dir, true);
                    }
                }
            }
        }

        #endregion

        #region SPA MVVM Core

        /// <summary>
        /// Generate View and ViewModel code.
        /// </summary>
        /// <param name="force"></param>
        /// <param name="msEngine"></param>
        private void GenerateSpaViewAndViewModelCode(bool force, MacroScriptEngine msEngine)
        {
            string outputFile, className;
            ProjectItem folderVM, folderViews, folderResources;
            Linx.Tools.CodeBuilder codeBuilderVM, codeBuilderView;

            folderVM = this.GetSpaAppFolder("viewmodels");
            folderViews = this.GetSpaAppFolder("views");
            folderResources = this.GetSpaAppFolder("resources");
            if (!folderVM.IsNull() && !folderViews.IsNull())
            {
                foreach (var ui in _designerRoot.EntityAdapterUserInterfaces.Where(e => e.VisualType == InterfaceType.Web && e.GeneratingType == DomainGeneratingType.AutomaticLayout && (force || e.HasPendingChanges)))
                {

                    if (ui.LayoutDefinition == null)
                    {
                        MessageBox.Show(String.Format("The user interface [{0}] has no definition. You should open the UI, configure and apply all changes.", ui.Name), "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }

                    className = GetSpaViewModelName(ui);

                    //View
                    outputFile = Path.Combine(folderViews.Properties.Item("FullPath").Value.ToString(), className + ".html");
                    codeBuilderView = new Linx.Tools.CodeBuilder();
                    this.GenerateSpaViewCode(ui, codeBuilderView);
                    string complementaryCalls = (codeBuilderView.ComplementaryCalls.GetBody().IsNullOrEmpty() ? "" : codeBuilderView.ComplementaryCalls.GetBody());
                    string eventChangedBrand = codeBuilderView.HasEvent(Tools.CodeBuilder.EventName.ChangedBrand) ? codeBuilderView.GetEvent(Tools.CodeBuilder.EventName.ChangedBrand).GetBody() : "";
                    _designerRoot.WriteFile(outputFile, codeBuilderView, folderViews.ProjectItems);

                    //View Model
                    outputFile = Path.Combine(folderVM.Properties.Item("FullPath").Value.ToString(), className + ".js");
                    codeBuilderVM = new Linx.Tools.CodeBuilder();
                    this.GenerateSpaViewModelCode(ui, codeBuilderVM, msEngine, complementaryCalls, eventChangedBrand);
                    _designerRoot.WriteFile(outputFile, codeBuilderVM, folderVM.ProjectItems);

                    //Complementary ViewModel                    
                    if (!complementaryCalls.IsNullOrEmpty())
                    {
                        outputFile = Path.Combine(folderVM.Properties.Item("FullPath").Value.ToString(), className + "Complement.js");
                        _designerRoot.WriteFile(outputFile, codeBuilderView.ComplementaryCode, folderVM.ProjectItems);
                    }
                    else
                    {
                        var complItem = _designerRoot.GetProjectItemByName(folderVM.ProjectItems, className + "Complement.js");
                        if (complItem != null) complItem.Delete();
                    }

                    //Resources
                    var fileResourceName = ui.SubscriptionNameSpace.Split('.')[0] + "-spa-" + ui.Name;
                    outputFile = Path.Combine(folderResources.Properties.Item("FullPath").Value.ToString(), fileResourceName.ToLower() + "_pt-br.js");
                    codeBuilderVM = new Linx.Tools.CodeBuilder();
                    this.GenerateSpaResourceCode(ui, codeBuilderVM, fileResourceName.ToLower() + "_custom_pt-br.js");
                    _designerRoot.WriteFile(outputFile, codeBuilderVM, folderResources.ProjectItems);

                    //Resources Custom
                    var fileResourceCustomName = ui.SubscriptionNameSpace.Split('.')[0] + "-spa-" + ui.Name;
                    string outputFileCustom = Path.Combine(folderResources.Properties.Item("FullPath").Value.ToString(), fileResourceName.ToLower() + "_custom_pt-br.js");
                    codeBuilderVM = new Linx.Tools.CodeBuilder();
                    if (GenerateSpaResourceCustomCode(outputFileCustom, codeBuilderVM))
                        _designerRoot.WriteFile(outputFileCustom, codeBuilderVM, folderResources.ProjectItems);
                }
            }
        }

        /// <summary>
        /// Generate Model Code.
        /// </summary>
        /// <param name="msEngine"></param>
        private void GenerateSpaModelCode(MacroScriptEngine msEngine)
        {
            var api = _designerRoot.WebApiControllers.FirstOrDefault(e => e.SynchronizedWithDomainService);
            if (api == null)
                return;

            if (_designerRoot.EntityAdapterUserInterfaces.Where(e => e.VisualType == InterfaceType.Web && (e.Subscription != null || e.EntityAdapter != null)).Count() == 0)
            {
                api.DeleteSpaServiceCode();
                return;
            }

            string outputFile, ctxClassName;
            ProjectItem item;
            Linx.Tools.CodeBuilder codeBuilder;

            item = this.GetSpaAppFolder("services");
            if (!item.IsNull())
            {
                ctxClassName = this.GetSpaContextName();
                outputFile = Path.Combine(item.Properties.Item("FullPath").Value.ToString(), ctxClassName + ".js");
                codeBuilder = new Linx.Tools.CodeBuilder();
                this.GenerateSpaContextCode(api, codeBuilder, ctxClassName, msEngine);

                _designerRoot.WriteFile(outputFile, codeBuilder, item.ProjectItems);
            }

        }

        /// <summary>
        /// Generate ViewModel custom code.
        /// </summary>
        /// <param name="force"></param>
        private void GenerateSpaViewModelCustomCode(bool force = false)
        {
            string outputFile, className;
            ProjectItem item;
            Linx.Tools.CodeBuilder codeBuilder;
            IEnumerable<string> methodList;

            item = this.GetSpaAppFolder("viewmodels");
            if (!item.IsNull())
            {
                foreach (var ui in _designerRoot.EntityAdapterUserInterfaces.Where(e => e.VisualType == InterfaceType.Web && (e.GeneratingType == DomainGeneratingType.AutomaticLayout && e.HasCustomization && (force || e.HasPendingChanges))))
                {
                    className = GetSpaViewModelName(ui);
                    methodList = GetButtonNamesByUI(ui);
                    outputFile = Path.Combine(item.Properties.Item("FullPath").Value.ToString(), className + "Custom.js");
                    if (File.Exists(outputFile))
                    {

                        var alltext = File.ReadAllText(outputFile);

                        if (!alltext.IsNullOrEmpty())
                            if (!alltext.Contains(viewModelCustomSourceCodeUid.ToString()))
                            {
                                throw new Exception(string.Format("A versão do arquivo customizado, foi alterado!\nRenomeie ou apague o mesmo e salve novamente.\nFileName:[{0}].", outputFile));
                            }
                            else
                            {
                                VerifyAndCreateCustomButtonMethodInVMCustom(outputFile, alltext, methodList, item.ProjectItems);
                                continue;
                            }
                    }

                    codeBuilder = new Linx.Tools.CodeBuilder();
                    codeBuilder.AddLine("/* Do not remove this line - File Generation id '{0}' - Do not remove this line */", viewModelCustomSourceCodeUid);

                    this.GenerateViewModelCustomCode(ui, codeBuilder, methodList);

                    _designerRoot.WriteFile(outputFile, codeBuilder, item.ProjectItems);
                }
            }
        }

        #endregion

        #region SPA Core

        /// <summary>
        /// Add domains reference to SPA
        /// </summary>
        private void AddDataDomainsReferenceToSpaService()
        {
            var project = _designerRoot.GetEadProject();
            if (this._designerRoot.IsAspNetCore)
                project = _designerRoot.GetEadCoreProject(project);

            var dataDomains = _designerRoot.GetProjectItemByName(project, "DataDomains.js", true);
            if (dataDomains != null)
            {
                string path = dataDomains.Properties.Item("FullPath").Value.ToString();
                var item = this.GetSpaAppFolder("services");
                if (!item.IsNull())
                {
                    var itemTo = _designerRoot.GetProjectItemByName(item.ProjectItems, Path.GetFileName(path));
                    if (itemTo == null)
                        item.ProjectItems.AddFromFileCopy(path);
                    else
                    {
                        string pathTo = itemTo.Properties.Item("FullPath").Value.ToString();

                        //Henry - 22/02/2016
                        //Checkout automático no DataDomains.js do SPA
                        if (File.Exists(pathTo) && !this.VerifySourceControl(pathTo))
                            return;

                        string body = File.ReadAllText(path);
                        if (File.ReadAllText(pathTo) != body)
                            File.WriteAllText(pathTo, body);
                    }
                }
            }
        }

        /// <summary>
        /// Remove data combo lookup.
        /// </summary>
        /// <param name="msEngine"></param>
        private void RemoveDataComboFile()
        {
            ProjectItem item = this.GetSpaAppFolder("services");
            if (!item.IsNull())
            {
                EntityAdapterDesignerRoot.RemoveProjectItems(item.ProjectItems, "DataCombos.js");
            }
        }

        /// <summary>
        /// Generate module configuration.
        /// </summary>
        /// <param name="codeBuilder"></param>
        private void GenerateSpaModuleConfigCode(Linx.Tools.CodeBuilder codeBuilder)
        {
            //Getting SPA Project
            var spaProject = this.GetSpaProject();
            if (spaProject == null)
                return;

            codeBuilder.AddLine("// <copyright file=\"RouteConfig.cs\" company=\"Linx Sistemas\">");
            codeBuilder.AddLine("// Copyright (c) Linx Sistemas. All rights reserved.");
            codeBuilder.AddLine("// </copyright>");
            codeBuilder.AddLine("using System.Collections.Generic;");
            codeBuilder.AddLine("using System.ComponentModel.Composition;");
            codeBuilder.AddLine("using Linx.Internet.Application.Framework.Web;");
            codeBuilder.AddLine("using Linx.Internet.Application.Framework.Classes;");
            codeBuilder.AddLine();
            codeBuilder.AddLine("namespace " + _designerRoot.GetNamespace(spaProject) + ".App_Start");
            codeBuilder.AddLine("{");
            codeBuilder.AddLine("    [Export(typeof(IRouteRegistrar)),");
            codeBuilder.AddLine("    ExportMetadata(\"Order\", 1),");
            codeBuilder.AddLine("    ExportMetadata(\"ModuleName\", \"" + _designerRoot.GetNamespace(spaProject).Replace(".", "-").ToLower() + "services\"),");
            codeBuilder.AddLine("    ExportMetadata(\"ModuleId\", \"" + _designerRoot.GetProjectGuid(spaProject).ToString() + "\")]");
            string liaPath = Path.Combine(_designerRoot.GetProjectPath(spaProject), "bin\\Linx.Internet.Application.dll");
            if (File.Exists(liaPath))
            {
                var fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(liaPath);
                if (fvi != null && !fvi.FileVersion.IsNullOrEmpty())
                    codeBuilder.AddLine("    [ExportMetadata(\"ShellVersion\", \"" + fvi.FileVersion + "\")]");
            }
            codeBuilder.AddLine("    public class ModuleConfig : IRouteRegistrar");
            codeBuilder.AddLine("    {");
            codeBuilder.AddLine("       Dictionary<string, EmbeddedFile> IRouteRegistrar.LoadEmbeddedResources(string moduleName)");
            codeBuilder.AddLine("       {");
            codeBuilder.AddLine("           return Linx.Internet.Application.Framework.Web.AssemblyResources.LoadEmbeddedResources(this.GetType(), moduleName);");
            codeBuilder.AddLine("       }");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("}");
        }

        /// <summary>
        /// Generating Model code.
        /// </summary>
        /// <param name="api"></param>
        /// <param name="codeBuilder"></param>
        /// <param name="contextName"></param>
        /// <param name="msEngine"></param>
        private void GenerateSpaContextCode(WebApiController api, Linx.Tools.CodeBuilder codeBuilder, string contextName, MacroScriptEngine msEngine)
        {
            string apiName = api.Name, packageName = "pkg_" + _designerRoot.GetNamespace(this.GetSpaProject()).Replace(".", "-").ToLower();
            bool hasLargeDataMode = _designerRoot.EntityAdapters.Any(e => e.IsBufferSaving());
            codeBuilder.AddLine("define(['durandal/system', '" + packageName + "/services/DataDomains', 'services/logger', 'breeze', 'durandal/app', 'managers/__auth', 'viewmodels/shared/modal', 'viewmodels/shared/modal2'],");
            codeBuilder.AddLine("function (system, dataDomains, logger, breeze, app, managerAuth, modal, modal2) {");

            codeBuilder.AddLine("var result = function () {");
            codeBuilder.IncreaseIndent();

            codeBuilder.AddLine("var getPivotLayouts = function (params, success, error) {");
            codeBuilder.AddLine("    return $.ajax({");
            codeBuilder.AddLine("        messageUser: 'Busca dos layouts exportados',");
            codeBuilder.AddLine("        contentType: 'application/json; charset=UTF-8',");
            codeBuilder.AddLine("        headers: managerAuth.getHeaders(),");
            codeBuilder.AddLine("        url: getServiceAddress('linxframeworkobjeto') + '/GetPivotLayouts?' +");
            codeBuilder.AddLine("                                                               'rootNameSpace=' + params.rootNamespace +");
            codeBuilder.AddLine("                                                               '&viewName=' + params.viewName +");
            codeBuilder.AddLine("                                                               '&pivotName=' + params.pivotName +");
            codeBuilder.AddLine("                                                               '&pivotDataSource=' + params.pivotDataSource,");
            codeBuilder.AddLine("        async: true,");
            codeBuilder.AddLine("        cache: false,");
            codeBuilder.AddLine("        error: error,");
            codeBuilder.AddLine("        success: success");
            codeBuilder.AddLine("    });");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("");

            codeBuilder.AddLine("var getServiceAddress = function(apiPart) {");
            codeBuilder.AddLine("   return managerAuth.getServiceAddress(apiPart, businessAssemblyName);");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var getBaseServiceAddress = function(apiPart) {");
            codeBuilder.AddLine("   return managerAuth.getBaseServiceAddress(apiPart, businessAssemblyName);");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var getAccessGroup = function() {");
            codeBuilder.AddLine("   return '00000000-0000-0000-0000-000000000000';");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var getNewGuid = function() {");
            codeBuilder.AddLine("   return breeze.core.getUuid();");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var getDataFeedUrl = function() {");
            codeBuilder.AddLine("   return getServiceAddress('" + api.GetRoutePrefix() + "OData');");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var getDataServiceUrl = function () {");
            codeBuilder.AddLine("   return getServiceAddress(controllerName);");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var setServiceBusUrl = function (url) {");
            codeBuilder.AddLine("   if (dataService) { dataService.serviceName = (isNullOrEmpty(url) ? getDataServiceUrl() : url + controllerName); }");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var initializePOCO = function(ownerReference, entityName) {");
            codeBuilder.AddLine("   if (ownerReference && !ownerReference.RowDataId) { eval(entityName + 'Initializer(ownerReference, true);'); }");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var businessAssemblyName = '" + _designerRoot.GetAssemblyName() + "';");
            codeBuilder.AddLine("var controllerName = '" + api.GetRoutePrefix() + "';");
            codeBuilder.AddLine("var dataService = new breeze.DataService({");
            codeBuilder.AddLine("    serviceName: getDataServiceUrl(),");
            codeBuilder.AddLine("    hasServerMetadata: false // don't ask the server for metadata");
            codeBuilder.AddLine("});");

            codeBuilder.AddLine("var manager = new breeze.EntityManager({ dataService: dataService });");

            codeBuilder.AddLine("manager.entityChanged.subscribe(function(changeArgs) {");
            codeBuilder.AddLine("    if (changeArgs.entityAction === breeze.EntityAction.PropertyChange) {");
            codeBuilder.AddLine("        if ((typeof changeArgs.args.newValue) === 'number' && changeArgs.args.oldValue < 0 && changeArgs.args.newValue > 0 && changeArgs.entity.isPrimaryKey(changeArgs.args.propertyName)) vm.replaceInnerUIsKeys(changeArgs.entity, changeArgs.args.propertyName, changeArgs.args.oldValue, changeArgs.args.newValue);");
            codeBuilder.AddLine("        if (typeof changeArgs.entity.OnPropertyChanged == 'function')");
            codeBuilder.AddLine("            changeArgs.entity.OnPropertyChanged(changeArgs.args.propertyName, changeArgs.args.oldValue, changeArgs.args.newValue);");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("});");

            codeBuilder.AddLine("var enableChangeTrack = true;");
            codeBuilder.AddLine("var entityPropChanged = function(entity, propName, oldVal, newVal) {");
            codeBuilder.AddLine("    if (!enableChangeTrack) return true;");
            codeBuilder.AddLine("    var result = true;");
            codeBuilder.AddLine("    if ((typeof entity.OnPropertyChanged == 'function') && oldVal !== newVal)");
            codeBuilder.AddLine("        result = (entity.OnPropertyChanged(propName, oldVal, newVal) !== false);");
            codeBuilder.AddLine("    if (result && ['U', 'I', 'D'].indexOf(entity.ChangeState) < 0) { entity.createOriginal(propName, oldVal); entity.ChangeState = 'U'; if (entity.setParentAsModified) entity.setParentAsModified(); }");
            codeBuilder.AddLine("    if (result && (typeof newVal) === 'number' && oldVal < 0 && newVal > 0 && entity.isPrimaryKey(propName)) vm.replaceInnerUIsKeys(entity, propName, oldVal, newVal);");
            codeBuilder.AddLine("    return result;");
            codeBuilder.AddLine("}");


            codeBuilder.AddLine("var metadataStore = manager.metadataStore;");
            codeBuilder.AddLine("var EntityQuery = breeze.EntityQuery;");
            //codeBuilder.AddLine("manager.enableSaveQueuing(true);");

            codeBuilder.AddLine("// Extract Breeze metadata definition types");
            codeBuilder.AddLine("var DataType = breeze.DataType;");
            codeBuilder.AddLine("var AutoGeneratedKeyType = breeze.AutoGeneratedKeyType;");
            codeBuilder.AddLine("var Validator = breeze.Validator;");
            codeBuilder.AddLine("Validator.hasValueValidator = new breeze.Validator('hasValueValidator', hasValueValidationFn, { messageTemplate: \"'%displayName%' é requerido\" });");

            codeBuilder.AddLine("//#region Metadata Info");
            var propNames = GetJsonMetadata(codeBuilder);
            codeBuilder.AddLine("var lookUpProperties = [];");
            codeBuilder.AddLine("//#endregion Metadata Info");

            string parameterNames = string.Join(",", _designerRoot.GetUsedParameterNames());
            #region the Class that manipulate parameters
            codeBuilder.AddLine("//#region dataParameters");
            codeBuilder.AddLine("var dataParameters = {");
            codeBuilder.AddLine("    isLoaded: false,");
            codeBuilder.AddLine("    parameters: [],");
            codeBuilder.AddLine("    registerParameters: function (parameterList, complete) {");
            codeBuilder.AddLine("        if (parameterList !== '') {");
            codeBuilder.AddLine("            var variation = '{TBC_GRUPO_ECONOMICO|' + managerAuth.loginInfo.IdLinxGrupoEconomico.toString() + '|TCS_USUARIO|' + managerAuth.loginInfo.UidUsuario + (vm != null && vm.getBandeiraRede() > 0 ? '|TBC_BANDEIRA_REDE|' + vm.getBandeiraRede().toString() : '') + '}';");
            codeBuilder.AddLine("            $.ajax({");
            codeBuilder.AddLine("                type: 'GET',");
            codeBuilder.AddLine("                url: getServiceAddress('LinxFrameworkParametro') + '/GetParameterValue?serializedParameterList=' + stringReplace(parameterList, '{}', variation),");
            codeBuilder.AddLine("                dataType: 'json',");
            codeBuilder.AddLine("                cache: false,");
            codeBuilder.AddLine("                headers: managerAuth.getHeaders(),");
            codeBuilder.AddLine("                error: function (jqXHR, textStatus, errorThrown) {");
            codeBuilder.AddLine("                    var msg = 'Os seguintes parâmetros não foram pesquisados: [' + parameterList + ']';");
            codeBuilder.AddLine("                    app.showMessage(msg, 'Alerta', ['Ok']);");
            codeBuilder.AddLine("                    dataParameters.isLoaded = true;");
            codeBuilder.AddLine("                },");
            codeBuilder.AddLine("                success: function (data) {");
            codeBuilder.AddLine("                    var parametersName = '';");
            codeBuilder.AddLine("                    var parameters = data.split('#');");
            codeBuilder.AddLine("                    for (var idx in parameters) {");
            codeBuilder.AddLine("                        var values = parameters[idx].split('|');");
            codeBuilder.AddLine("                        var pName = values[0];");
            codeBuilder.AddLine("                        var pValue = values[1];");
            codeBuilder.AddLine("                        dataParameters.parameters[pName] = pValue;");
            codeBuilder.AddLine("                    }");
            codeBuilder.AddLine("                    dataParameters.isLoaded = true;");
            codeBuilder.AddLine("                    if (complete) complete();");
            codeBuilder.AddLine("                }");
            codeBuilder.AddLine("            });");
            codeBuilder.AddLine("        }");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("//#endregion dataParameters");
            #endregion the Class that manipulate parameters

            codeBuilder.AddLine("//#region Classes Map");
            codeBuilder.AddLine("var sequences = [];");
            codeBuilder.AddLine("var getNextSequence = function(entityName) {");
            codeBuilder.AddLine("    if (!sequences[entityName]) resetSequence(entityName);");
            codeBuilder.AddLine("    sequence = sequences[entityName];");
            codeBuilder.AddLine("    sequences[entityName]++;");
            codeBuilder.AddLine("    return sequence;");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var resetSequence = function(entityName) {");
            codeBuilder.AddLine("    sequences[entityName] = 0;");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var getSequence = function(entityName) {");
            codeBuilder.AddLine("    if (!sequences[entityName]) resetSequence(entityName);");
            codeBuilder.AddLine("    return sequences[entityName];");
            codeBuilder.AddLine("};");
            //Verify Validators and DataType

            //Metadata
            Action<EntityAdapter> createMetadata = null;
            Dictionary<string, List<string>> entityKeysForDefault = new Dictionary<string, List<string>>();

            createMetadata = (entity) =>
            {
                var topParent = entity.GetTopParent();
                var details = entity.GetAllInheritanceSourceEntityAdapters();
                bool hasIdentity = entity.HasIdentityPrimaryKey();
                codeBuilder.AddLine();
                codeBuilder.AddLine("// Configure " + entity.Name + " data type");
                codeBuilder.AddLine("metadataStore.addEntityType({");
                codeBuilder.AddLine("shortName: \"" + entity.Name + "\",");
                codeBuilder.AddLine("namespace: \"" + _designerRoot.GetContextNamespace() + "\",");
                codeBuilder.AddLine("autoGeneratedKeyType: AutoGeneratedKeyType." + (hasIdentity ? "Identity" : "None") + ",");
                codeBuilder.AddLine("dataProperties: {");
                string comma = String.Empty, validators;
                bool isRequired;
                int precision;
                entityKeysForDefault.Add(entity.Name, new List<string>());

                bool hasDynamicPK = entity.HasDynamicPrimaryKey();
                if (hasDynamicPK)
                {
                    codeBuilder.AddLine(comma + "EntityUniqueKey" + ": { dataType: DataType." + _designerRoot.ToJsDataType("System.Guid") + ", isNullable: false, isPartOfKey: true, validators: [ ]  }");
                    comma = ",";
                    entityKeysForDefault[entity.Name].Add("EntityUniqueKey:Guid");
                }

                if (entity.TargetEntityAdapter != null && entity.TargetEntityAdapter.HasDynamicPrimaryKey())
                {
                    codeBuilder.AddLine(comma + "EntityParentUniqueKey" + ": { dataType: DataType." + _designerRoot.ToJsDataType("System.Guid") + ", isNullable: false, isPartOfKey: false, validators: [ ]  }");
                    comma = ",";
                }

                List<string> zeroValueDomains = this.GetZeroValueDomains();
                var classDomainExtenders = new List<EntityAdapterAttribute>();
                bool hasLookUps = false;
                string classProperties = String.Empty, queryRequiredProperties = String.Empty;
                string lookUpPropertiesDef = "";
                var lookUpsPropInfo = entity.GetAllLookUpPropertiesInfo(true);
                string keyFilterForRefreshing = String.Empty;
                Action<EntityAdapterAttribute, bool> genPropertyDefinitions = (prop, removePartOfKey) =>
                {
                    classProperties += comma + "'" + prop.Name + "'";
                    if (prop is EntityAdapterProperty && ((EntityAdapterProperty)prop).IsRequiredBeforeSearching)
                        queryRequiredProperties += (queryRequiredProperties.IsNullOrEmpty() ? "" : ",") + prop.Name + ": '" + prop.DisplayName + "'";

                    ///////////////////
                    bool isPK = !removePartOfKey && (!hasDynamicPK && (entity.IsIndependentKey(prop) || ((prop is EntityAdapterProperty) && entity.IsPrimaryKey(((EntityAdapterProperty)prop)))));

                    //Domain with zero value 
                    bool zeroDomainValue = !prop.DomainName.IsNullOrEmpty() && zeroValueDomains.Contains(prop.DomainName);

                    //Defining validators
                    isRequired = (!zeroDomainValue && !prop.RemoveValidations && (!prop.DomainName.IsNullOrEmpty() || !prop.IsNull || prop.IsCompulsory));

                    if (isRequired && ((!prop.IsZeroNotAllowed && prop.IsNumeric()) || prop.Datatype.ToLower().Contains("bool")) && prop.DomainName.IsNullOrEmpty() && !isPK)
                        isRequired = false;

                    validators = (isRequired ? "Validator.hasValueValidator" : String.Empty);

                    precision = (prop.Datatype.ToLower().Contains("string") && !prop.Precision.IsNullOrEmpty() && !prop.Precision.Left(":").IsNullOrEmpty() && prop.Precision.Left(":").IsNumeric() ? int.Parse(prop.Precision.Left(":")) : 0);
                    if (precision > 0)
                        validators += (validators.IsNullOrEmpty() ? String.Empty : ", ") + "Validator.maxLength( {maxLength: " + precision.ToString() + "})";

                    if (isPK)
                    {
                        keyFilterForRefreshing += (keyFilterForRefreshing.IsNullOrEmpty() ? "'" : " + ';") + prop.Name + "#==#" + Linx.Tools.EntitySearch.ParseJDataType(prop.Datatype) + "' + getAbsoluteValue(ownerReference." + prop.Name + ").toString()";
                    }

                    if (isPK && (entity.GetTopParent().IsBufferSaving() || (!hasIdentity && !entity.IsRelationWithParent(prop.Name)))) entityKeysForDefault[entity.Name].Add(prop.Name + ":" + prop.Datatype);

                    codeBuilder.AddLine(comma + prop.Name + ": { dataType: DataType." + _designerRoot.ToJsDataType(prop.Datatype) + (precision > 0 ? ", maxLength: " + precision.ToString() : String.Empty) + ", isNullable: " + (zeroDomainValue || prop.Datatype.ToLower().Contains("bool") || prop.IsNullable()).ToString().ToLower() + ", isPartOfKey: " + isPK.ToString().ToLower() + (prop.Datatype.ToLower().Contains("datetime") && !prop.IsNullable() ? ", defaultValue: ''" : "") + ", validators: [ " + (prop.Datatype.ToLower().Contains("bool") ? "" : validators) + "]  }");
                    if (comma.IsNullOrEmpty())
                        comma = ",";

                    if (!prop.DomainName.IsNullOrEmpty())
                    {
                        codeBuilder.AddLine(comma + prop.Name + "Name: { dataType: DataType.String, isNullable: false, isPartOfKey: false, validators: [] }");
                        classDomainExtenders.Add(prop);
                    }

                    if (lookUpsPropInfo.ContainsKey(prop.Name))
                    {
                        hasLookUps = true;
                        lookUpPropertiesDef += (lookUpPropertiesDef.IsNullOrEmpty() ? "" : ", ") + prop.Name + ": '" + LookUpAdapter.GetLookUpName(lookUpsPropInfo[prop.Name]) + "'";
                    }
                };
                foreach (var attribute in entity.GetAllInheritanceAttributes())
                {
                    genPropertyDefinitions(attribute, false);
                }
                foreach (var attribute in entity.GetExtraParentRelationKey())
                {
                    genPropertyDefinitions(attribute, true);
                }
                if (entity.GetTopParent().IsBufferSaving())
                {
                    codeBuilder.AddLine(comma + "ChangeState: { dataType: DataType.String, isNullable: true, isPartOfKey: false, validators: [] }");
                }
                if (!keyFilterForRefreshing.IsNullOrEmpty())
                {
                    keyFilterForRefreshing = "var filterByKey = '" + entity.Name + "{' + " + keyFilterForRefreshing + " + '}';";
                }

                if (entity.HasEnabledMedias())
                {
                    codeBuilder.AddLine(comma + "TableMedia: { dataType: DataType." + _designerRoot.ToJsDataType("string") + ", isNullable: true, isPartOfKey: false, validators: []  }");
                }

                lookUpPropertiesDef = "lookUpProperties['" + entity.Name + "'] = {" + lookUpPropertiesDef + "};";

                codeBuilder.AddLine("                },");
                codeBuilder.AddLine("navigationProperties: {");
                comma = String.Empty;
                if (entity.TargetEntityAdapter != null)
                {
                    codeBuilder.AddLine("// Returns a single parent and associates with Details");
                    codeBuilder.AddLine(entity.GetAssociation(true, true));
                    comma = ",";
                }
                codeBuilder.AddLine("// Returns collections of details and associates with Parent");
                foreach (var detail in details)
                {
                    codeBuilder.AddLine(comma + detail.GetAssociation(false, true));
                    if (comma.IsNullOrEmpty())
                        comma = ",";
                }
                codeBuilder.AddLine("                      }");
                codeBuilder.AddLine("});");

                //Lookup Properties
                codeBuilder.AddLine(lookUpPropertiesDef);

                //Add Extenders
                codeBuilder.AddLine("var " + entity.Name + "Initializer = function (ownerReference, isPOCO) {");

                codeBuilder.AddLine("   ownerReference.RowDataId = (isPOCO === true ? getNextSequence('" + entity.Name + "') : ko.observable(getNextSequence('" + entity.Name + "')));");

                if (entity.GetTopParent().IsBufferSaving() && !entity.IsReadOnly)
                {
                    if (propNames.ContainsKey(entity.Name))
                    {
                        codeBuilder.AddLine("    //Start Property Definitions");
                        foreach (var prop in propNames[entity.Name])
                        {
                            string privName = "_" + prop.ToCamelCase();
                            codeBuilder.AddLine("    var " + privName + " = ownerReference." + prop + ";");
                            codeBuilder.AddLine("    Object.defineProperty(ownerReference, '" + prop + "', {");
                            codeBuilder.AddLine("      get: function() { return " + privName + "; },");

                            string domainSetter = "";
                            var domProp = classDomainExtenders.FirstOrDefault(e => e.Name == prop);
                            if (domProp != null)
                            {
                                domainSetter = " else { " + privName + "Name = (dataDomains.getName('" + domProp.DomainName + "', newValue)); }";
                            }
                            else
                            {
                                domProp = classDomainExtenders.FirstOrDefault(e => (e.Name + "Name") == prop);
                                if (domProp != null)
                                {
                                    domainSetter = " else { " + privName.Left(privName.Length - 4) + " = (dataDomains.getId('" + domProp.DomainName + "', newValue)); }";
                                }
                            }

                            codeBuilder.AddLine("      set: function(newValue) { var oldValue = " + privName + "; " + privName + " = newValue; if (!entityPropChanged(ownerReference, '" + prop + "', oldValue, newValue)) { " + privName + " = oldValue; }" + domainSetter + " }");
                            codeBuilder.AddLine("    });");
                        }
                        codeBuilder.AddLine("    //End Property Definitions");
                    }
                }

                foreach (var detail in details)
                {
                    codeBuilder.AddLine("   ownerReference.current" + detail.Name + " = ko.observable(null);");
                }

                if (details.Count > 0)
                {
                    codeBuilder.AddLine("   //Adjust details for a POCO reference");
                    codeBuilder.AddLine("   if (isPOCO === true) {");
                    foreach (var detail in details)
                    {
                        codeBuilder.AddLine("       ownerReference." + detail.Name + "List = ko.observableArray(ownerReference." + detail.Name + "List);");
                    }
                    codeBuilder.AddLine("   }");
                }

                codeBuilder.AddLine("   ownerReference.setRemovedLookupFields = function(removedFields) {");
                codeBuilder.AddLine("       for (var idxLUp in entitylookUps[ownerReference.typeName]) {");
                codeBuilder.AddLine("           var hasKeyValue = false;");
                codeBuilder.AddLine("           var luName = entitylookUps[ownerReference.typeName][idxLUp];");
                codeBuilder.AddLine("           var luMeta = metadataInfo[luName];");
                codeBuilder.AddLine("           for (var idxProp in luMeta) {");
                codeBuilder.AddLine("               var prop = luMeta[idxProp];");
                codeBuilder.AddLine("               if (!isNullOrEmpty(prop.relatedKey) && prop.isPartOfKey) {");
                codeBuilder.AddLine("                   hasKeyValue = !isNullOrEmpty(getAbsoluteValue(ownerReference[prop.relatedKey]));");
                codeBuilder.AddLine("                   break;");
                codeBuilder.AddLine("               }");
                codeBuilder.AddLine("           }");
                codeBuilder.AddLine("           if (hasKeyValue) {");
                codeBuilder.AddLine("               for (var idxProp in luMeta) {");
                codeBuilder.AddLine("                   var prop = luMeta[idxProp];");
                codeBuilder.AddLine("                   if (!isNullOrEmpty(prop.relatedKey) && !prop.isPartOfKey) {");
                codeBuilder.AddLine("                       removedFields.push(prop.relatedKey);");
                codeBuilder.AddLine("                   }");
                codeBuilder.AddLine("               }");
                codeBuilder.AddLine("           }");
                codeBuilder.AddLine("       }");
                codeBuilder.AddLine("   }");

                codeBuilder.AddLine("   ownerReference.getJExpression = function(listFilterRange, removedFields, noDetails) {");
                codeBuilder.AddLine("       if (ownerReference.excludedFilters && ownerReference.excludedFilters.length > 0) { if (removedFields instanceof Array) removedFields = removedFields.concat(ownerReference.excludedFilters); else removedFields = ownerReference.excludedFilters; }");
                if (entity.EnableLookupOptimizationForQBE && !entity.IsDashboardFilter)
                    codeBuilder.AddLine("       ownerReference.setRemovedLookupFields(removedFields);");
                codeBuilder.AddLine("       var jExpression = getJEntityExpression(ownerReference, app, listFilterRange, removedFields, vm.useLikeCommandAsDefault, ownerReference.getQbeZeroFields());");
                codeBuilder.AddLine("       if (jExpression === 'Error') return jExpression;");
                //Getting jExpression from Details
                foreach (var detail in details.Where(e => e.EnableQBE))
                {
                    var parentLink = detail.GetParentLinkRelation();
                    codeBuilder.AddLine("       if (noDetails !== true && ownerReference." + (detail.Name + "List") + " && ownerReference." + (detail.Name + "List()") + ".length > 0) {");
                    codeBuilder.AddLine("         var detailExpr = ownerReference." + (detail.Name + "List") + "()[0].getJExpression(listFilterRange" + (parentLink == null || parentLink.DetailKeyFields.IsNullOrEmpty() ? "" : ", ['" + parentLink.DetailKeyFields.Replace(" ", "").Replace(",", "','") + "']") + ");");
                    codeBuilder.AddLine("         if (detailExpr === 'Error') return detailExpr;");
                    codeBuilder.AddLine("         jExpression += detailExpr;");
                    codeBuilder.AddLine("       }");
                }

                codeBuilder.AddLine("       return jExpression;");
                codeBuilder.AddLine("  };");

                codeBuilder.AddLine("   ownerReference.createOriginal = function(propertyName, oldValue) {");
                codeBuilder.AddLine("       ownerReference.original = ownerReference.getPrimitiveDTO();");
                codeBuilder.AddLine("       if (propertyName) ownerReference.original[propertyName] = oldValue;");
                codeBuilder.AddLine("   }");

                codeBuilder.AddLine("   ownerReference.restoreOriginal = function() {");
                codeBuilder.AddLine("       if (!isNullOrEmpty(ownerReference.original)) {");
                codeBuilder.AddLine("          enableChangeTrack = false;");
                codeBuilder.AddLine("          var properties = metadataInfo[ownerReference.typeName];");
                codeBuilder.AddLine("          for (var i = 0; i < properties.length; i++) {");
                codeBuilder.AddLine("              var propertyName = properties[i].key;");
                codeBuilder.AddLine("              if ((typeof ownerReference.original[propertyName]) !== 'undefined') ownerReference[propertyName] = ownerReference.original[propertyName];");
                codeBuilder.AddLine("          }");
                codeBuilder.AddLine("          delete ownerReference.original;");
                codeBuilder.AddLine("          enableChangeTrack = true;");
                codeBuilder.AddLine("       } else if(ownerReference.ChangeState === 'D') ownerReference.ChangeState = 'U';");
                codeBuilder.AddLine("   }");

                codeBuilder.AddLine("   if (isPOCO === true) {"); //Is POCO
                codeBuilder.IncreaseIndent();

                codeBuilder.AddLine("   ownerReference.getValidationErrors = function(propertyName) {");
                codeBuilder.AddLine("       var errors = [];");
                codeBuilder.AddLine("       if (!vm.canReportErrors) return errors;");
                codeBuilder.AddLine("       if (!ownerReference.ChangeState || ['I', 'U'].indexOf(ownerReference.ChangeState) < 0) return errors;");
                codeBuilder.AddLine("       var properties = metadataInfo[ownerReference.typeName];");
                codeBuilder.AddLine("       for (var i = 0; i < properties.length; i++) {");
                codeBuilder.AddLine("           var prop = properties[i];");
                codeBuilder.AddLine("           if (isNullOrEmpty(propertyName) || prop.key == propertyName) {");
                codeBuilder.AddLine("               if (prop.isRequired === true && !prop.isPartOfKey && isNullOrEmpty(ownerReference[prop.key]) && !(prop.isQbeZero === true && ownerReference[prop.key] == 0)) errors.push('O campo [' + prop.headerText + (managerAuth.shellMode=='DEV' ? ' (' + ownerReference.typeName + '.' + prop.key + ')' : '') + '] é requerido.');");
                codeBuilder.AddLine("               if (prop.validateMaxLength === true && prop.maxLength > 0 && !isNullOrEmpty(ownerReference[prop.key]) && ownerReference[prop.key].length > prop.maxLength) errors.push('O campo [' + prop.headerText + (managerAuth.shellMode=='DEV' ? ' (' + ownerReference.typeName + '.' + prop.key + ')' : '') + '] permite no máximo ' + prop.maxLength.toString() + ' caractere(s).');");
                codeBuilder.AddLine("           }");
                codeBuilder.AddLine("       }");

                if (details.Count > 0)
                {
                    foreach (var detail in details)
                    {
                        codeBuilder.AddLine("       if (isNullOrEmpty(propertyName)) {");
                        codeBuilder.AddLine("           for (var i = 0; i < ownerReference." + detail.Name + "List().length; i++) {");
                        codeBuilder.AddLine("               var detail = ownerReference." + detail.Name + "List()[i];");
                        codeBuilder.AddLine("               errors = errors.concat(detail.getValidationErrors());");
                        codeBuilder.AddLine("           }");
                        codeBuilder.AddLine("       }");
                    }
                }

                codeBuilder.AddLine("       return errors;");
                codeBuilder.AddLine("   }");

                codeBuilder.DecreaseIndent();
                codeBuilder.AddLine("   }"); //Is POCO


                codeBuilder.AddLine("   ownerReference.getQbeZeroFields = function() {");
                codeBuilder.AddLine("       var result = [];");
                codeBuilder.AddLine("       var properties = metadataInfo[ownerReference.typeName];");
                codeBuilder.AddLine("       for (var i = 0; i < properties.length; i++) {");
                codeBuilder.AddLine("           if (properties[i].isQbeZero) {");
                codeBuilder.AddLine("               result.push(properties[i].key);");
                codeBuilder.AddLine("           }");
                codeBuilder.AddLine("       }");
                codeBuilder.AddLine("       return result;");
                codeBuilder.AddLine("   }");

                codeBuilder.AddLine("   ownerReference.getPrimitiveDTO = function(loadDetails) {");
                codeBuilder.AddLine("       var command = '';");
                codeBuilder.AddLine("       var properties = metadataInfo[ownerReference.typeName];");
                codeBuilder.AddLine("       for (var i = 0; i < properties.length; i++) {");
                codeBuilder.AddLine("           command += (command === '' ? '' : ', ') + properties[i].key + ': getAbsoluteValue(ownerReference.' + properties[i].key + ')';");
                codeBuilder.AddLine("           if (properties[i].isDomain && properties[i].key.length > 4) command += (command === '' ? '' : ', ') + strLeft(properties[i].key, properties[i].key.length - 4) + ': getAbsoluteValue(ownerReference.' + strLeft(properties[i].key, properties[i].key.length - 4) + ')';");
                codeBuilder.AddLine("       }");
                codeBuilder.AddLine("       eval('var result = { ' + command + ' };');");

                if (details.Count > 0)
                {
                    codeBuilder.AddLine("       if (loadDetails) {");
                    foreach (var det in details)
                    {
                        codeBuilder.AddLine("           result." + det.Name + "List = [];");
                        codeBuilder.AddLine("           var sourceList = getAbsoluteValue(ownerReference." + det.Name + "List);");
                        codeBuilder.AddLine("           if (sourceList && sourceList.length > 0) {");
                        codeBuilder.AddLine("               for (var i = 0; i < sourceList.length; i++) {");
                        codeBuilder.AddLine("                   if (['U', 'I', 'D'].indexOf(sourceList[i].ChangeState) >= 0) result." + det.Name + "List.push(sourceList[i].getPrimitiveDTO(sourceList[i].ChangeState != 'D'));");
                        codeBuilder.AddLine("               }");
                        codeBuilder.AddLine("           }");
                    }
                    codeBuilder.AddLine("       }");
                }

                codeBuilder.AddLine("       return result;");
                codeBuilder.AddLine("   };");

                codeBuilder.AddLine("   ownerReference.getAllDetailChanges = function() {");
                codeBuilder.AddLine("       var result = [];");

                if (details.Count > 0)
                {
                    foreach (var det in details)
                    {
                        codeBuilder.AddLine("       var _" + det.Name + "List = getAbsoluteValue(ownerReference." + det.Name + "List);");
                        codeBuilder.AddLine("       if (_" + det.Name + "List && _" + det.Name + "List.length > 0) {");
                        codeBuilder.AddLine("           for (var i = 0; i < _" + det.Name + "List.length; i++) {");
                        codeBuilder.AddLine("               var detail = _" + det.Name + "List[i];");
                        codeBuilder.AddLine("               if (['U', 'I', 'D'].indexOf(detail.ChangeState) >= 0) {");
                        codeBuilder.AddLine("                   result.push(detail);");
                        codeBuilder.AddLine("                   result = result.concat(detail.getAllDetailChanges());");
                        codeBuilder.AddLine("               }");
                        codeBuilder.AddLine("           }");
                        codeBuilder.AddLine("       }");
                    }
                }

                codeBuilder.AddLine("       return result;");
                codeBuilder.AddLine("   };");

                codeBuilder.AddLine("   ownerReference.copyDataFrom = function(originData, copyDetails) {");
                codeBuilder.AddLine("       enableChangeTrack = false;");
                codeBuilder.AddLine("       var properties = metadataInfo[ownerReference.typeName];");
                codeBuilder.AddLine("       for (var i = 0; i < properties.length; i++) {");
                codeBuilder.AddLine("            setAbsoluteValue(ownerReference, properties[i].key, getAbsoluteValue(originData[properties[i].key]));");
                codeBuilder.AddLine("       }");

                if (details.Count > 0)
                {
                    codeBuilder.AddLine("       if (copyDetails) {");
                    foreach (var detail in details)
                    {
                        codeBuilder.AddLine("           if (ownerReference." + detail.Name + "List && originData." + detail.Name + "List) {");
                        codeBuilder.AddLine("               var toList = getAbsoluteValue(ownerReference." + detail.Name + "List);");
                        codeBuilder.AddLine("               var fromList = getAbsoluteValue(originData." + detail.Name + "List);");
                        codeBuilder.AddLine("               for (var idxElem = toList.length - 1; idxElem >= 0; idxElem--) {");
                        codeBuilder.AddLine("                  if (toList[idxElem].ChangeState === 'D') toList.splice(idxElem, 1);");
                        codeBuilder.AddLine("               }");

                        codeBuilder.AddLine("               for (var idxElem = toList.length - 1; idxElem >= 0; idxElem--) {");
                        codeBuilder.AddLine("                      if (toList[idxElem].ChangeState !== 'N') {");

                        var tmpKey = detail.GetTemporaryKey();
                        if (!tmpKey.IsNull())
                        {
                            codeBuilder.AddLine("                           var fromObj = [];");
                            codeBuilder.AddLine("                           if (toList[idxElem].ChangeState == 'I') {");
                            codeBuilder.AddLine("                               fromObj = _.where(fromList, { Temporary" + tmpKey.Name + ": toList[idxElem]['" + tmpKey.Name + "'] });");
                            codeBuilder.AddLine("                           } else {");
                            codeBuilder.AddLine("                               fromObj = _.where(fromList, { " + String.Join(",", detail.GetPrimaryKeys().Select(e => e + ": toList[idxElem]['" + e + "']")) + " });");
                            codeBuilder.AddLine("                           }");
                        }
                        else codeBuilder.AddLine("                           var fromObj = _.where(fromList, { " + String.Join(",", detail.GetPrimaryKeys().Select(e => e + ": toList[idxElem]['" + e + "']")) + " });");

                        codeBuilder.AddLine("                           if (fromObj.length > 0) toList[idxElem].copyDataFrom(fromObj[0], true);");

                        codeBuilder.AddLine("                      }");

                        codeBuilder.AddLine("               }");

                        codeBuilder.AddLine("           }");

                    }
                    codeBuilder.AddLine("       }");
                }
                codeBuilder.AddLine("   enableChangeTrack = true;");
                codeBuilder.AddLine("   };");

                codeBuilder.AddLine("      ownerReference.commitDetailsVisualPendings = function() {");

                foreach (var detail in details)
                {
                    codeBuilder.AddLine("          vm.dataBind('" + detail.Name + "List', true);");
                    codeBuilder.AddLine("          if (ownerReference.current" + detail.Name + "()) ownerReference.current" + detail.Name + "().commitDetailsVisualPendings();");
                }
                codeBuilder.AddLine("      }");

                codeBuilder.AddLine("      ownerReference.refreshData = function(noWait, succeeded) {");

                if (entity.ExistsClientEvent("OnDataRefreshing"))
                    codeBuilder.AddLine("       if (!ownerReference.OnDataRefreshing()) { vm.closeProcessing(); if (succeeded) { succeeded({results: [ownerReference]}); } return { then: function(thenMethod) { if (thenMethod) thenMethod(); }, fin: function(finMethod) { if (finMethod) finMethod(); } }; }");

                if (!keyFilterForRefreshing.IsNullOrEmpty())
                {
                    codeBuilder.AddLine("         " + keyFilterForRefreshing);
                    codeBuilder.AddLine("         if (!ownerReference.isPOCO && ownerReference.entityAspect && !ownerReference.isDetached() && !ownerReference.isUnchanged()) ownerReference.entityAspect.setUnchanged();");

                    codeBuilder.AddLine("         return dataContext.get" + entity.Name + "ByEntitySearchNoAssociations(filterByKey, 0, 0, false, true, ownerReference.isPOCO === true, '', querySucceeded);");
                    codeBuilder.AddLine("         function querySucceeded(data) {");

                    codeBuilder.AddLine("            if (ownerReference.isPOCO && data.results.length > 0) {  for (var idx = 0; idx < data.results.length; idx++) { ownerReference.copyDataFrom(data.results[idx]); } }");

                    codeBuilder.AddLine("            if (succeeded) { succeeded(data); }");

                    codeBuilder.AddLine("            if (data.results.length == 0) { return; }");

                    codeBuilder.AddLine("            if (!noWait || ownerReference.atLeastOneDetailLoaded()) { vm.clearInnerUIs(ownerReference); ownerReference.fillDetails(true, '', false, noWait); }");

                    if (entity.ExistsClientEvent("OnDataRefreshed"))
                        codeBuilder.AddLine("            ownerReference.OnDataRefreshed();");

                    codeBuilder.AddLine("       }");
                }
                else
                {
                    codeBuilder.AddLine("       if (succeeded) { succeeded({results: [ownerReference]}); }");
                    codeBuilder.AddLine("       return { then: function(thenMethod) { if (thenMethod) thenMethod(); }, fin: function(finMethod) { if (finMethod) finMethod(); } }; ");
                }

                codeBuilder.AddLine("      }");

                codeBuilder.AddLine("   if (isPOCO === true) {");
                codeBuilder.AddLine("       ownerReference.isPOCO = true;");
                codeBuilder.AddLine("       ownerReference.enableDetailsDataTack = function(breezeReference) {");
                if (!entity.GetTopParent().IsBufferSaving())
                {
                    foreach (var detail in details)
                    {
                        codeBuilder.AddLine("          breezeReference." + detail.Name + "IsLoaded = ownerReference." + detail.Name + "IsLoaded;");
                        codeBuilder.AddLine("          for (var idx = 0; idx < ownerReference." + detail.Name + "List().length; idx++) {");
                        codeBuilder.AddLine("              var entity = ownerReference." + detail.Name + "List()[idx];");
                        codeBuilder.AddLine("              if (entity.isPOCO)  {");
                        codeBuilder.AddLine("                  var newReference = createEntity(entity.typeName, entity.getPrimitiveDTO(), true);");
                        codeBuilder.AddLine("                  entity.enableDetailsDataTack(newReference);");
                        codeBuilder.AddLine("              }");
                        codeBuilder.AddLine("          }");
                    }
                }
                codeBuilder.AddLine("          if (breezeReference) breezeReference.setCurrentDetails();");
                codeBuilder.AddLine("       };");
                codeBuilder.AddLine("   }");

                codeBuilder.AddLine("   ownerReference.isAdded = (isPOCO === true ? function() { return " + (entity.GetTopParent().IsBufferSaving() ? "ownerReference.ChangeState === 'I'" : "false") + "; } : function() {");
                codeBuilder.AddLine("       return ownerReference.entityAspect.entityState === breeze.EntityState.Added;");
                codeBuilder.AddLine("   });");
                codeBuilder.AddLine("   ownerReference.isDeleted = (isPOCO === true ? function() { return " + (entity.GetTopParent().IsBufferSaving() ? "ownerReference.ChangeState === 'D'" : "false") + "; } : function() {");
                codeBuilder.AddLine("       return ownerReference.entityAspect.entityState === breeze.EntityState.Deleted;");
                codeBuilder.AddLine("   });");
                codeBuilder.AddLine("   ownerReference.isModified = (isPOCO === true ? function() { return " + (entity.GetTopParent().IsBufferSaving() ? "ownerReference.ChangeState === 'U'" : "false") + "; } : function() {");
                codeBuilder.AddLine("       return ownerReference.entityAspect.entityState === breeze.EntityState.Modified;");
                codeBuilder.AddLine("   });");
                codeBuilder.AddLine("   ownerReference.isDetached = (isPOCO === true ? function() { return false; } : function() {");
                codeBuilder.AddLine("       return ownerReference.entityAspect.entityState === breeze.EntityState.Detached;");
                codeBuilder.AddLine("   });");
                codeBuilder.AddLine("   ownerReference.isUnchanged = (isPOCO === true ? function() { return " + (entity.GetTopParent().IsBufferSaving() ? "ownerReference.ChangeState === 'N'" : "true") + "; } : function() {");
                codeBuilder.AddLine("       return ownerReference.entityAspect.entityState === breeze.EntityState.Unchanged;");
                codeBuilder.AddLine("   });");
                codeBuilder.AddLine("   ownerReference.setModified = (isPOCO === true ? function() { " + (entity.GetTopParent().IsBufferSaving() ? "ownerReference.ChangeState = 'U';" : "") + " } : function() {");
                codeBuilder.AddLine("       ownerReference.entityAspect.setModified();");
                codeBuilder.AddLine("   });");
                codeBuilder.AddLine("   ownerReference.setUnchanged = (isPOCO === true ? function() { " + (entity.GetTopParent().IsBufferSaving() ? "ownerReference.ChangeState = 'N';" : "") + " } : function() {");
                codeBuilder.AddLine("       ownerReference.entityAspect.setUnchanged();");
                codeBuilder.AddLine("   });");
                codeBuilder.AddLine("   ownerReference.serverDataType = [];");
                foreach (var prop in entity.GetAllInheritanceAttributes())
                {
                    codeBuilder.AddLine("   ownerReference.serverDataType['" + prop.Name + "'] = '" + Linx.Tools.EntitySearch.ParseJDataType(prop.Datatype) + "';");
                }

                codeBuilder.AddLine("   ownerReference.typeName = '" + entity.Name + "';");
                codeBuilder.AddLine("   ownerReference.isPrimaryKey = function(propertyName) {");
                codeBuilder.AddLine("       var keys = [ " + String.Join(",", entity.GetAllInheritanceAttributes().Where(e => e is EntityAdapterProperty && entity.IsPrimaryKey((EntityAdapterProperty)e)).Select(e => "'" + e.Name + "'").ToArray()) + " ];");
                codeBuilder.AddLine("       return keys.indexOf(propertyName) >= 0;");
                codeBuilder.AddLine("   }");
                codeBuilder.AddLine("   ownerReference.getDisplayName = function(propertyName) {");
                codeBuilder.AddLine("      var property = getEntityProperty(ownerReference.typeName, propertyName);");
                codeBuilder.AddLine("      return (property != null ? property.headerText : propertyName);");
                codeBuilder.AddLine("   }");
                codeBuilder.AddLine("   ownerReference.setDisplayName = function(propertyName, displayName) {");
                codeBuilder.AddLine("      var property = getEntityProperty(ownerReference.typeName, propertyName);");
                codeBuilder.AddLine("      if (property != null) property.headerText = displayName;");
                codeBuilder.AddLine("   }");

                #region Bandeira Rede
                codeBuilder.AddLine("   ownerReference.setBandeiraRede = function (idBandeiraRede) {");

                if (entity.HasBrand())
                {
                    codeBuilder.AddLine("       if (idBandeiraRede >= 0) setAbsoluteValue(ownerReference, 'IdBandeiraRede', idBandeiraRede);");
                }

                Action<EntityAdapter> setDetailBrandStructure = null;
                setDetailBrandStructure = (detElement) =>
                {
                    if (detElement.HasBrand() && detElement.ForceBrandFilter)
                    {
                        codeBuilder.AddLine("       if (idBandeiraRede >= 0 && ownerReference." + detElement.Name + "List().length > 0) ownerReference." + detElement.Name + "List()[0].setBandeiraRede(idBandeiraRede);");
                    }
                    detElement.SourceEntityAdapters.ToList().ForEach(e => setDetailBrandStructure(e));
                };
                entity.SourceEntityAdapters.ToList().ForEach(e => setDetailBrandStructure(e));

                codeBuilder.AddLine("   };");
                #endregion

                #region Gpecon
                codeBuilder.AddLine("   ownerReference.setGpecon = function (idGpecon) {");
                if (entity.HasGpecon())
                {
                    codeBuilder.AddLine("       if (idGpecon > 0) setAbsoluteValue(ownerReference, 'IdGpecon', idGpecon);");
                }
                codeBuilder.AddLine("   };");
                #endregion

                if (!entity.GetTopParent().IsBufferSaving())
                {
                    codeBuilder.AddLine("   ownerReference.UpdateIndependentRelation = function(detailName) {");
                    codeBuilder.AddLine("       var cacheElements = dataContext.getEntities(detailName);");
                    codeBuilder.AddLine("       for (var idxR = 0; idxR < cacheElements.length; idxR++) {");
                    codeBuilder.AddLine("           if (typeof cacheElements[idxR]." + entity.Name + " !== 'function') { return; }");
                    codeBuilder.AddLine("           else  if (cacheElements[idxR]." + entity.Name + "() != ownerReference) { cacheElements[idxR]." + entity.Name + "(ownerReference); }");
                    codeBuilder.AddLine("       }");
                    codeBuilder.AddLine("   }");
                }

                if (hasLookUps)
                {
                    codeBuilder.AddLine("   //#region Lookup Extended Methods");
                    if (!entity.EnableClientLookupOnQueryMode && !entity.GetTopParent().IsBufferSaving())
                    {
                        codeBuilder.AddLine("   if (isPOCO !== true) {");
                        codeBuilder.IncreaseIndent();
                    }
                    GenerateSPALookupExecuting(entity, codeBuilder);
                    if (!entity.EnableClientLookupOnQueryMode && !entity.GetTopParent().IsBufferSaving())
                    {
                        codeBuilder.DecreaseIndent();
                        codeBuilder.AddLine("   }");
                    }
                    codeBuilder.AddLine("   //#endregion Lookup Extended Methods");
                }

                #region setDefaults
                //Direct properties
                codeBuilder.AddLine("   ownerReference.setDefaults = function () {");


                codeBuilder.AddLine("        //Adjust default value for QBE Zero Properties");
                codeBuilder.AddLine("        var qbeZeroProperties = ownerReference.getQbeZeroFields();");
                codeBuilder.AddLine("        for (var i = 0; i < qbeZeroProperties.length; i++) {");
                codeBuilder.AddLine("               setAbsoluteValue(ownerReference, qbeZeroProperties[i], 0);");
                codeBuilder.AddLine("        }");


                Action<EntityAdapterAttribute> createDefault = (prop) =>
                {
                    codeBuilder.AddLine("       setAbsoluteValue(ownerReference, '" + prop.Name + "', " + GenerateSPACodeForDefaultValueAndParameters(prop) + ");");
                };

                entity.GetAllInheritanceProperties().Where(p => !p.DefaultValue.IsNullOrEmpty()).Foreach(createDefault);
                entity.GetAllInheritancePublicationProperties().Where(p => !p.DefaultValue.IsNullOrEmpty()).Foreach(createDefault);

                //Lookup properties
                Action<EntityAdapterAttribute> createDefaultLookup = (prop) =>
                {
                    codeBuilder.AddLine("       ownerReference.executeLookUp('" + prop.GetLookUpName() + "','" + prop.Name + "', null, vm, " + GenerateSPACodeForDefaultValueAndParameters(prop) + ", null);");
                };

                entity.GetAllInheritanceProperties().Where(p => p.IsFK && !p.DefaultValue.IsNullOrEmpty()).Foreach(createDefaultLookup);
                entity.GetAllInheritancePublicationProperties().Where(p => p.IsFK && !p.DefaultValue.IsNullOrEmpty()).Foreach(createDefaultLookup);

                //Set auxiliary parent key replacement.
                if (entity.TargetEntityAdapter != null)
                {
                    foreach (var keyReplacement in entity.GetNoParentKeyRelations())
                    {
                        codeBuilder.AddLine("       setAbsoluteValue(ownerReference, '" + keyReplacement.Key + "', getAbsoluteValue(getAbsoluteValue(ownerReference." + entity.TargetEntityAdapter.Name + ")." + keyReplacement.Value + "));");
                    }
                }

                codeBuilder.AddLine("   };");
                #endregion

                #region delete
                codeBuilder.AddLine("   ownerReference.delete = function() {");
                codeBuilder.AddLine("       if (ownerReference.isDetached()) {");
                codeBuilder.AddLine("           app.showMessage('A informação selecionada não pode ser excluída!', 'Alerta', ['Ok']);");
                codeBuilder.AddLine("           return;");
                codeBuilder.AddLine("       }");
                codeBuilder.AddLine("       if (ownerReference.setParentAsModified) ownerReference.setParentAsModified();");
                if (entity.TargetEntityAdapter != null)
                {
                    codeBuilder.AddLine("       var parent = getAbsoluteValue(ownerReference." + entity.TargetEntityAdapter.Name + ");");
                }

                foreach (var detail in details)
                {
                    codeBuilder.AddLine("       if (!isNullOrEmpty(ownerReference." + detail.Name + "List()) && ownerReference." + detail.Name + "List().length > 0) {");
                    codeBuilder.AddLine("          var details = [].concat(ownerReference." + detail.Name + "List());");
                    codeBuilder.AddLine("          for (var idx = 0; idx < details.length; idx++) {");
                    codeBuilder.AddLine("            details[idx].delete();");
                    codeBuilder.AddLine("          }");
                    codeBuilder.AddLine("       }");
                }

                if (entity.GetTopParent().IsBufferSaving())
                {
                    codeBuilder.AddLine("       if (ownerReference.ChangeState == 'I') {");
                    codeBuilder.AddLine("           if (parent && (typeof parent." + entity.Name + "List === 'function')) { ");
                    codeBuilder.AddLine("               parent." + entity.Name + "List.remove(ownerReference); ");
                    codeBuilder.AddLine("           }");
                    codeBuilder.AddLine("           else {");
                    codeBuilder.AddLine("               vm.dataView.remove(ownerReference);");
                    codeBuilder.AddLine("           }");
                    if (entity.TargetEntityAdapter != null)
                        codeBuilder.AddLine("           delete ownerReference." + entity.TargetEntityAdapter.Name + ";");
                    codeBuilder.AddLine("       }");
                    codeBuilder.AddLine("       else {");
                    codeBuilder.AddLine("           if (ownerReference.ChangeState == 'N') { ownerReference.createOriginal(); }");
                    codeBuilder.AddLine("           ownerReference.ChangeState = 'D'; // mark for deletion");
                    codeBuilder.AddLine("       }");

                }
                else
                    codeBuilder.AddLine("       if (ownerReference.entityAspect) ownerReference.entityAspect.setDeleted(); // mark for deletion");

                if (entity.TargetEntityAdapter != null)
                {
                    codeBuilder.AddLine("       if (parent && (typeof parent.setCurrentDetails === 'function') && (typeof parent." + entity.Name + "List === 'function') && parent." + entity.Name + "List().length == 0) parent.setCurrentDetails('" + entity.Name + "');");
                }

                codeBuilder.AddLine("   };");
                #endregion

                #region setParentAsModified
                codeBuilder.AddLine("   ownerReference.setParentAsModified = function() {");
                if (entity.TargetEntityAdapter != null)
                {
                    codeBuilder.AddLine("   var parent = getAbsoluteValue(ownerReference." + entity.TargetEntityAdapter.Name + ");");
                    codeBuilder.AddLine("   if (parent) {");
                    codeBuilder.AddLine("       if (parent.isUnchanged()) {");
                    codeBuilder.AddLine("           parent.setModified(); ");
                    codeBuilder.AddLine("       }");
                    codeBuilder.AddLine("       parent.setParentAsModified();");
                    codeBuilder.AddLine("   }");

                }
                codeBuilder.AddLine("   };");
                #endregion

                #region getParent/getSelfList
                codeBuilder.AddLine("   ownerReference.getParent = function() {");
                if (entity.TargetEntityAdapter != null)
                {
                    codeBuilder.AddLine("       return getAbsoluteValue(ownerReference." + entity.TargetEntityAdapter.Name + ");");
                }
                else
                    codeBuilder.AddLine("       return null;");
                codeBuilder.AddLine("   };");
                codeBuilder.AddLine("   ownerReference.getSelfList = function() {");
                if (entity.TargetEntityAdapter != null)
                {
                    codeBuilder.AddLine("       var parent = ownerReference.getParent();");
                    codeBuilder.AddLine("       if (!isNullOrEmpty(parent)) {");
                    codeBuilder.AddLine("           return getAbsoluteValue(parent." + entity.Name + "List);");
                    codeBuilder.AddLine("       } else { return null; }");
                }
                else
                    codeBuilder.AddLine("       return vm.dataView();");
                codeBuilder.AddLine("   };");
                #endregion

                codeBuilder.AddLine("   ownerReference.namespace = '" + _designerRoot.GetContextNamespace() + "';");
                codeBuilder.AddLine("   ownerReference.myProperties = [ " + classProperties + " ];");
                codeBuilder.AddLine("   ownerReference.queryRequiredProperties = { " + queryRequiredProperties + " };");

                string excludedFilters = "";
                foreach (var prop in entity.GetAllInheritanceAttributes().Where(e => e.RemoveFilterFromClientLayer))
                {
                    excludedFilters += (excludedFilters.IsNullOrEmpty() ? "" : ", ") + "'" + prop.Name + "'";
                }
                codeBuilder.AddLine("   ownerReference.excludedFilters = [" + excludedFilters + "];");

                codeBuilder.AddLine("   ownerReference.getCurrentElements = function() {");
                codeBuilder.AddLine("       var result = [ ownerReference ];");

                foreach (var detail in details)
                {
                    codeBuilder.AddLine("   if (!isNullOrEmpty(ownerReference.current" + detail.Name + "())) { result = result.concat(ownerReference.current" + detail.Name + "().getCurrentElements()); }");
                }

                codeBuilder.AddLine("       return result;");
                codeBuilder.AddLine("   };");

                #region SendAllRowsOnSubmitting
                codeBuilder.AddLine("   ownerReference.checkForSendingAllRowsToServer = function() {");

                var entitiesForSendingChanges = entity.GetSourceEntityAdapters().Where(e => e.SendAllRowsOnSubmitting).ToArray();
                if (entitiesForSendingChanges.Length > 0)
                {
                    codeBuilder.AddLine("      if (ownerReference.isUnchanged()) {");
                    codeBuilder.AddLine("          ownerReference.setModified();");
                    codeBuilder.AddLine("      }");
                    foreach (var detail in entitiesForSendingChanges)
                    {
                        codeBuilder.AddLine("      for (var idx = 0; idx < ownerReference." + detail.Name + "List().length; idx++) {");
                        codeBuilder.AddLine("          if (ownerReference." + detail.Name + "List()[idx].isUnchanged()) {");
                        codeBuilder.AddLine("              ownerReference." + detail.Name + "List()[idx].setModified();");
                        codeBuilder.AddLine("          }");
                        codeBuilder.AddLine("          ownerReference." + detail.Name + "List()[idx].checkForSendingAllRowsToServer();");
                        codeBuilder.AddLine("      }");
                    }
                }

                codeBuilder.AddLine("   };");
                #endregion

                //Generate where clause for details
                foreach (var detail in details)
                {
                    detail.GenerateJsWhereDetailRelationMethod(codeBuilder, "ownerReference");
                }
                foreach (var detail in details)
                {
                    codeBuilder.AddLine("   ownerReference." + detail.Name + "IsLoaded = false;");
                }
                codeBuilder.AddLine("   ownerReference.detailsLoaded = function() {");
                if (details.Count == 0)
                    codeBuilder.AddLine("       return true;");
                else
                {
                    string detailsLoadTest = "";
                    foreach (var detail in details)
                    {
                        detailsLoadTest += (detailsLoadTest.IsNullOrEmpty() ? "" : " && ") + "ownerReference." + detail.Name + "IsLoaded";
                    }
                    codeBuilder.AddLine("       return " + detailsLoadTest + ";");
                }
                codeBuilder.AddLine("   }");
                codeBuilder.AddLine("   ownerReference.atLeastOneDetailLoaded = function() {");
                if (details.Count == 0)
                    codeBuilder.AddLine("       return true;");
                else
                {
                    string detailsLoadTest = "";
                    foreach (var detail in details)
                    {
                        detailsLoadTest += (detailsLoadTest.IsNullOrEmpty() ? "" : " || ") + "ownerReference." + detail.Name + "IsLoaded";
                    }
                    codeBuilder.AddLine("       return " + detailsLoadTest + ";");
                }
                codeBuilder.AddLine("   }");
                codeBuilder.AddLine("   ownerReference.adjustDetailsLoaded = function(value) {");
                if (details.Count > 0)
                {
                    foreach (var detail in details)
                    {
                        codeBuilder.AddLine("       ownerReference." + detail.Name + "IsLoaded = value;");
                        codeBuilder.AddLine("       if (value === false && ownerReference.isPOCO)");
                        codeBuilder.AddLine("           ownerReference." + detail.Name + "List([]);");
                    }
                }
                codeBuilder.AddLine("   }");
                codeBuilder.AddLine("   ownerReference.fillDetails = function(force, detailName, noInnerUIs, noWait, callback, customParentRelation) {");

                codeBuilder.AddLine("      if (typeof force === 'undefined') force = false;");
                if (entity.ExistsClientEvent("OnSelected"))
                {
                    codeBuilder.AddLine("      ownerReference.OnSelected();");
                }

                codeBuilder.AddLine("      if (force) vm.clearInnerUIs(ownerReference);");
                codeBuilder.AddLine("      if (!noInnerUIs) vm.queryInnerUIs(ownerReference);");

                if (details.Count > 0)
                {
                    codeBuilder.AddLine("      if (ownerReference.isAdded()) {");
                    foreach (var detail in details)
                    {
                        codeBuilder.AddLine("        ownerReference." + detail.Name + "IsLoaded = true;");
                    }
                    codeBuilder.AddLine("      }");

                    string detailsRemoteTest = "";
                    foreach (var detail in details)
                    {
                        detailsRemoteTest += (detailsRemoteTest.IsNullOrEmpty() ? "" : " && ") + "_" + detail.Name + "RemoteComplete";
                        codeBuilder.AddLine("      var _" + detail.Name + "RemoteComplete = false;");
                    }
                    if (detailsRemoteTest.IsNullOrEmpty())
                        detailsRemoteTest = "true";

                    foreach (var detail in details)
                    {
                        codeBuilder.AddLine("      var detachList_" + detail.Name + " = [];");
                        codeBuilder.AddLine("      if (force) {");
                        codeBuilder.AddLine("           if (isNullOrEmpty(detailName) || detailName == '" + detail.Name + "') ownerReference." + detail.Name + "IsLoaded = false;");
                        codeBuilder.AddLine("           if ((isNullOrEmpty(detailName) || detailName == '" + detail.Name + "') && ownerReference." + (detail.Name + "List") + " && ownerReference." + (detail.Name + "List()") + ".length > 0) {");
                        codeBuilder.AddLine("               if (ownerReference.isPOCO) {");
                        codeBuilder.AddLine("                   ownerReference." + detail.Name + "List([]);");
                        codeBuilder.AddLine("               } else {");
                        codeBuilder.AddLine("                   var detailList = ownerReference." + detail.Name + "List();");
                        codeBuilder.AddLine("                   for (var idx = detailList.length - 1; idx >= 0; idx--) {");
                        codeBuilder.AddLine("                       detachList_" + detail.Name + ".push(detailList[idx]);");
                        codeBuilder.AddLine("                   }");
                        codeBuilder.AddLine("               }");
                        codeBuilder.AddLine("           }");
                        codeBuilder.AddLine("      }");
                        codeBuilder.AddLine();

                        var parentLink = detail.GetParentLinkRelation();
                        if (parentLink.IsDashboard && !entity.GetTopParent().IsBufferSaving())
                        {
                            codeBuilder.AddLine("      //Replace keys for independent entities");
                            codeBuilder.AddLine("      ownerReference.UpdateIndependentRelation('" + detail.Name + "');");
                        }

                        codeBuilder.AddLine("      if (!ownerReference." + detail.Name + "IsLoaded) {");

                        codeBuilder.AddLine("        //Load " + (detail.Name + "List"));
                        codeBuilder.AddLine("        if (" + (detail.LoadDataOnlyIfVisible ? "((force || !vm.isDataSourceHided('" + detail.Name + "List" + "')) && isNullOrEmpty(detailName))" : "isNullOrEmpty(detailName)") + " || detailName === '" + detail.Name + "') {");

                        codeBuilder.AddLine("          ownerReference." + detail.Name + "IsLoaded = true;");
                        codeBuilder.AddLine("          _" + detail.Name + "RemoteComplete = (ownerReference." + (detail.Name + "List") + " && ownerReference." + (detail.Name + "List()") + ".length > 0);");

                        codeBuilder.AddLine("          if ((force || !ownerReference." + (detail.Name + "List") + " || ownerReference." + (detail.Name + "List()") + ".length === 0)" + (parentLink.RemoveFieldIfEmpty ? "" : detail.GetJsTestDetailRelation("ownerReference")) + ") {");

                        //Se existe evento OnDetailSearching gera código para validar cancelamento da query do detalhe
                        if (detail.TargetEntityAdapter.ExistsClientEvent("OnDetailSearching"))
                        {
                            codeBuilder.AddLine("          if (ownerReference.GetJsWhereDetailRelationFor" + detail.Name + "(customParentRelation) === 'Error'){");
                            codeBuilder.AddLine("               ownerReference.setCurrentDetails('" + detail.Name + "');" + (entity.ExistsClientEvent("OnDetailSearched") ? " ownerReference.OnDetailSearched('" + detail.Name + "');" : "") + " notifyPresentation('" + (detail.Name + "List") + "');");
                            codeBuilder.AddLine("               _" + detail.Name + "RemoteComplete = true;");
                            codeBuilder.AddLine("           }");
                            codeBuilder.AddLine("           else {");
                        }
                        //

                        codeBuilder.AddLine("            var navQuery = EntityQuery.from('Get" + detail.Name + "ByEntitySearchNoAssociations').noTracking(ownerReference.isPOCO === true)");

                        var orderBy = detail.GetOrderByCommand();
                        if (!orderBy.IsNullOrEmpty())
                            codeBuilder.AddLine("            .orderBy('" + orderBy + "')");

                        detail.GetJsWhereDetailRelation(codeBuilder, "ownerReference");
                        codeBuilder.AddLine(";");
                        codeBuilder.AddLine("            if (!vm.dataToolbar._noBusyLoading) vm.showProcessing('Pesquisando detalhes...');");
                        codeBuilder.AddLine("            manager.executeQuery(navQuery).then(function (data) { if (ownerReference.isPOCO) { for (var idx = 0; idx < data.results.length; idx++) { initializePOCO(data.results[idx], '" + detail.Name + "'); data.results[idx]." + entity.Name + " = ko.observable(ownerReference); } ownerReference." + detail.Name + "List(data.results); } " + (detail.TargetEntityAdapter.HasDynamicPrimaryKey() || detail.GetExtraParentRelationKey().Count > 0 ? " for (var idx = 0; idx < data.results.length; idx++) { " + this.GenerateNavigationReplaces(detail, "data.results[idx]", "ownerReference") + " if (!ownerReference.isPOCO) { data.results[idx]." + entity.Name + "(ownerReference); data.results[idx].entityAspect.entityState === breeze.EntityState.Unchanged; } }" : ""));
                        codeBuilder.AddLine("               if (!ownerReference.isPOCO && detachList_" + detail.Name + ".length > 0)");
                        codeBuilder.AddLine("               {");
                        codeBuilder.AddLine("                   for (var idx = 0; idx < detachList_" + detail.Name + ".length; idx++)");
                        codeBuilder.AddLine("                   {");
                        codeBuilder.AddLine("                       if (!data.results.contains(detachList_" + detail.Name + "[idx]))");
                        codeBuilder.AddLine("                           detachEntity(detachList_" + detail.Name + "[idx]);");
                        codeBuilder.AddLine("                       else {");
                        codeBuilder.AddLine("                           if (force && detachList_" + detail.Name + "[idx].atLeastOneDetailLoaded())");
                        codeBuilder.AddLine("                               detachList_" + detail.Name + "[idx].fillDetails(force, '', false, noWait);");
                        codeBuilder.AddLine("                       }");
                        codeBuilder.AddLine("                   }");
                        codeBuilder.AddLine("               }");
                        codeBuilder.AddLine("               ownerReference.setCurrentDetails('" + detail.Name + "');" + (entity.ExistsClientEvent("OnDetailSearched") ? " ownerReference.OnDetailSearched('" + detail.Name + "');" : "") + " notifyPresentation('" + (detail.Name + "List") + "');");
                        codeBuilder.AddLine("               _" + detail.Name + "RemoteComplete = true;");
                        codeBuilder.AddLine("               if (callback && (!isNullOrEmpty(detailName) || (" + detailsRemoteTest + "))) { callback(); }");
                        if (entity.ExistsClientEvent("OnAllDetailsSearched"))
                            codeBuilder.AddLine("               if ((!isNullOrEmpty(detailName) || (" + detailsRemoteTest + "))) { ownerReference.OnAllDetailsSearched(); }");

                        codeBuilder.AddLine("            }).fail(queryFailed).fin(function() { if (!vm.dataToolbar._noBusyLoading) vm.closeProcessing(); });");

                        //Se existe evento OnDetailSearching gera código para validar cancelamento da query do detalhe
                        if (detail.TargetEntityAdapter.ExistsClientEvent("OnDetailSearching"))
                        {
                            codeBuilder.AddLine("            }");
                        }
                        //

                        codeBuilder.AddLine("          } else { ownerReference.setCurrentDetails('" + detail.Name + "'); notifyPresentation('" + (detail.Name + "List") + "'); }");
                        codeBuilder.AddLine("        } else { _" + detail.Name + "RemoteComplete = true; if (!ownerReference." + detail.Name + "IsLoaded && ownerReference." + (detail.Name + "List") + " && ownerReference." + (detail.Name + "List()") + ".length > 0) { ownerReference." + detail.Name + "IsLoaded = true; ownerReference.setCurrentDetails('" + detail.Name + "'); } }");

                        codeBuilder.AddLine("      } else { ");


                        codeBuilder.AddLine("        if (isNullOrEmpty(detailName) || detailName == '" + detail.Name + "') {");
                        codeBuilder.AddLine("           notifyPresentation('" + (detail.Name + "List") + "');");
                        codeBuilder.AddLine("           ownerReference.setCurrentDetails('" + detail.Name + "');");
                        codeBuilder.AddLine("        }");

                        codeBuilder.AddLine("        _" + detail.Name + "RemoteComplete = true;");

                        codeBuilder.AddLine("      }");
                    }

                    codeBuilder.AddLine("      if (callback && ((!isNullOrEmpty(detailName) && (eval('_' + detailName + 'RemoteComplete && ownerReference.' + detailName + 'IsLoaded') == true)) || (isNullOrEmpty(detailName) && (" + detailsRemoteTest + ")))) { callback(); }");
                    if (entity.ExistsClientEvent("OnAllDetailsSearched"))
                        codeBuilder.AddLine("      if (((!isNullOrEmpty(detailName) && (eval('_' + detailName + 'RemoteComplete && ownerReference.' + detailName + 'IsLoaded') == true)) || (isNullOrEmpty(detailName) && (" + detailsRemoteTest + ")))) { ownerReference.OnAllDetailsSearched(); }");
                }
                else
                {
                    codeBuilder.AddLine("      if (callback) { callback(); }");
                    if (entity.ExistsClientEvent("OnAllDetailsSearched"))
                        codeBuilder.AddLine("      ownerReference.OnAllDetailsSearched();");
                }


                codeBuilder.AddLine("   };");
                codeBuilder.AddLine("   //Select first element as a current item of each detail");
                codeBuilder.AddLine("   ownerReference.setCurrentDetails = function(detailName, clearing) {");
                foreach (var detail in details)
                {
                    codeBuilder.AddLine("      if ((isNullOrEmpty(detailName) || detailName === '" + detail.Name + "')) {");

                    codeBuilder.AddLine("           if (ownerReference." + (detail.Name + "List()") + ".length > 0) { ownerReference.current" + detail.Name + "(ownerReference." + (detail.Name + "List()") + "[0]); if (clearing == null || clearing === false) ownerReference.current" + detail.Name + "().fillDetails(); }");
                    codeBuilder.AddLine("           else { ownerReference.current" + detail.Name + "(null); ownerReference.notifyEmptyDetails('" + detail.Name + "'); }");

                    codeBuilder.AddLine("      }");

                }
                codeBuilder.AddLine("   };");

                codeBuilder.AddLine("   ownerReference.notifyEmptyDetails = function(detailName) {");

                foreach (var detail in details)
                {
                    codeBuilder.AddLine("      if (detailName === '" + detail.Name + "') {");
                    codeBuilder.AddLine("           notifyPresentation('" + (detail.Name + "List") + "');");
                    codeBuilder.AddLine("           vm.queryInnerUIs(null, '" + detail.Name + "');");
                    foreach (var subDetail in detail.GetAllSourceEntityAdapters())
                    {
                        codeBuilder.AddLine("           notifyPresentation('" + (subDetail.Name + "List") + "');");
                        codeBuilder.AddLine("           vm.queryInnerUIs(null, '" + subDetail.Name + "');");
                    }
                    codeBuilder.AddLine("      }");

                }
                codeBuilder.AddLine("   };");


                //Add Client Events
                if (entity.EntityAdapterClientEvented.Count > 0)
                {
                    codeBuilder.AddLine("//#region Client Events");
                    foreach (var cliEvent in entity.EntityAdapterClientEvented)
                    {
                        var parameters = cliEvent.Parameters;
                        if (LookUpAdapter.IsBeforeQuery(cliEvent.Name) && parameters.IsNullOrEmpty())
                            parameters = "string fieldToSearch#object lookupInfo";

                        codeBuilder.AddLine("   ownerReference." + cliEvent.Name + " = function (" + String.Join(", ", parameters.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries).Select(e => e.Right(" "))) + ") {");
                        codeBuilder.AddLine(cliEvent.MacroScript.IsNullOrEmpty() ? (cliEvent.ReturnType.ToLower().Contains("void") ? "" : "return " + GetSpaDefaultValueByType(cliEvent.ReturnType, true) + ";") : msEngine.ReplaceAllMacros(cliEvent.MacroScript, MacroOutputType.JavaScript, _designerRoot.GetDirectorySourcePart()) + (cliEvent.ReturnType.ToLower().Contains("bool") ? "\r\nreturn " + this.GetSpaDefaultValueByType(cliEvent.ReturnType, true) + ";" : ""));
                        codeBuilder.AddLine("   }");
                    }
                    codeBuilder.AddLine("//#endregion Client Events");
                }

                if (classDomainExtenders.Count > 0)
                {
                    codeBuilder.AddLine("//#region Extended Domain Names");
                    string defaultValue;
                    foreach (var prop in classDomainExtenders)
                    {
                        codeBuilder.AddLine("   if (isPOCO !== true) {");
                        codeBuilder.AddLine("       ownerReference." + prop.Name + "Name.subscribe(");
                        codeBuilder.AddLine("           function (newValue) {");
                        codeBuilder.AddLine("               if (newValue == null) { ownerReference." + prop.Name + "Name(''); return; }");
                        codeBuilder.AddLine("               var value = (dataDomains.getId('" + prop.DomainName + "', newValue));");
                        codeBuilder.AddLine("               if (value != ownerReference." + prop.Name + "()) {");
                        codeBuilder.AddLine("                   ownerReference." + prop.Name + "(value);");
                        codeBuilder.AddLine("               }");
                        codeBuilder.AddLine("        });");
                        codeBuilder.AddLine();
                        codeBuilder.AddLine("       ownerReference." + prop.Name + ".subscribe(");
                        codeBuilder.AddLine("       function (newValue) {");
                        defaultValue = this.GetSpaDefaultValueByType(prop.Datatype);
                        if (defaultValue != "null")
                            codeBuilder.AddLine("               if (newValue == null) { ownerReference." + prop.Name + "(" + defaultValue + "); return; }");
                        codeBuilder.AddLine("               var value = " + (defaultValue == "null" ? "(newValue == null) ? '' : " : "") + "dataDomains.getName('" + prop.DomainName + "', newValue);");
                        codeBuilder.AddLine("               if (value != ownerReference." + prop.Name + "Name()) {");
                        codeBuilder.AddLine("                   ownerReference." + prop.Name + "Name(value);");
                        codeBuilder.AddLine("           }");
                        codeBuilder.AddLine("       });");
                        codeBuilder.AddLine("   }");
                    }
                    codeBuilder.AddLine("//#endregion Extended Domain Names");
                }

                if (details.Count > 0)
                {
                    codeBuilder.AddLine("//#region Adjust details already loaded for a POCO reference");
                    codeBuilder.AddLine("   if (isPOCO === true) {");
                    foreach (var detail in details)
                    {
                        codeBuilder.AddLine("       if ((typeof ownerReference." + detail.Name + "List === 'function') && ownerReference." + detail.Name + "List().length > 0) {");
                        codeBuilder.AddLine("            for(var idx = 0; idx < ownerReference." + detail.Name + "List().length; idx++) { " + detail.Name + "Initializer(ownerReference." + detail.Name + "List()[idx], isPOCO); }");
                        codeBuilder.AddLine("       }");
                    }
                    codeBuilder.AddLine("   }");
                    codeBuilder.AddLine("//#endregion Adjust details already loaded for a POCO reference");
                }

                codeBuilder.AddLine("};");

                codeBuilder.AddLine("metadataStore.registerEntityTypeCtor(\"" + entity.Name + "\", null, " + entity.Name + "Initializer);");

                details.ForEach(e => createMetadata(e));
            };

            _designerRoot.EntityAdapters.Where(e => e.TargetEntityAdapter == null).ToList().ForEach(e => createMetadata(e));

            codeBuilder.AddLine("//#endregion Classes Map");

            codeBuilder.AddLine("//#region Context Definition");

            //Clear\Get Methods
            string queryMethods = GenerateDataServiceJsQueryActions(codeBuilder);

            //Event Definition
            codeBuilder.AddLine();
            codeBuilder.AddLine("// Create the data update event.");
            codeBuilder.AddLine("var dataUpdateEvent = document.createEvent('Event');");
            codeBuilder.AddLine("// Define that the event name is '" + contextName + "_DataUpdate'.");
            codeBuilder.AddLine("var contextUpdtEvt = '" + contextName + "_DataUpdate_' + getNewGuid();");
            codeBuilder.AddLine("dataUpdateEvent.initEvent(contextUpdtEvt, true, true);");

            //Finalizers LookUp
            string finalizerMethods = GenerateLookUpJsFinalizers(codeBuilder);

            codeBuilder.AddLine();
            codeBuilder.AddLine("var cancelChanges = function(dataForUndo) {");
            codeBuilder.AddLine("    if (dataForUndo && dataForUndo.length > 0) {");
            codeBuilder.AddLine("        dataForUndo.forEach(function(e) { e.restoreOriginal(); } ); ");
            codeBuilder.AddLine("    } else {");
            codeBuilder.AddLine("        manager.rejectChanges();");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine();
            codeBuilder.AddLine("var hasEmptyRequiredFilters = function() {");

            codeBuilder.AddLine("    return false;");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine();
            codeBuilder.AddLine("var getEntityProperty = function (entityName, propertyName) {");
            codeBuilder.AddLine("    for (var i = 0; i < metadataInfo[entityName].length; i++) {");
            codeBuilder.AddLine("        if (metadataInfo[entityName][i].key === propertyName)");
            codeBuilder.AddLine("            return metadataInfo[entityName][i];");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    return null;");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var getViewInfo = function (entityName) {");
            codeBuilder.AddLine("    var result = [];");
            codeBuilder.AddLine("    if (metadataInfo[entityName])");
            codeBuilder.AddLine("    {");
            codeBuilder.AddLine("        var viewInfoElements = metadataInfo[entityName];");
            codeBuilder.AddLine("        for (var i = 0; i < viewInfoElements.length; i++) {");
            codeBuilder.AddLine("            var selectedElement = viewInfoElements[i];");
            codeBuilder.AddLine("            if (!selectedElement.hidden && (selectedElement.dataType === 'string' || selectedElement.dataType === 'number'))");
            codeBuilder.AddLine("            {");
            codeBuilder.AddLine("                if (strLeft(selectedElement.key, 3) === 'Cod' || strLeft(selectedElement.key, 2) === 'Id' || strLeft(selectedElement.key, 6) === 'Numero' || strLeft(selectedElement.key, 6) === 'Number') {");
            codeBuilder.AddLine("                    result.push(selectedElement);");
            codeBuilder.AddLine("                }");
            codeBuilder.AddLine("            }");
            codeBuilder.AddLine("        }");
            codeBuilder.AddLine("        for (var i = 0; i < viewInfoElements.length; i++) {");
            codeBuilder.AddLine("            var selectedElement = viewInfoElements[i];");
            codeBuilder.AddLine("            if (!selectedElement.hidden && (selectedElement.dataType === 'string')) {");
            codeBuilder.AddLine("                if (strLeft(selectedElement.key, 4) === 'Nome' || strLeft(selectedElement.key, 4) === 'Name' || strLeft(selectedElement.key, 4) === 'Desc' || strLeft(selectedElement.key, 6) === 'Titulo' || strLeft(selectedElement.key, 5) === 'Title') {");
            codeBuilder.AddLine("                    result.push(selectedElement);");
            codeBuilder.AddLine("                }");
            codeBuilder.AddLine("            }");
            codeBuilder.AddLine("        }");
            codeBuilder.AddLine("        for (var i = 0; i < viewInfoElements.length; i++) {");
            codeBuilder.AddLine("            var selectedElement = viewInfoElements[i];");
            codeBuilder.AddLine("            if (!selectedElement.hidden && selectedElement.dataType === 'date')");
            codeBuilder.AddLine("            {                ");
            codeBuilder.AddLine("               result.push(selectedElement);");
            codeBuilder.AddLine("            }");
            codeBuilder.AddLine("        }");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    return result;");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine();
            codeBuilder.AddLine("var getChanges = function() {");
            codeBuilder.AddLine("   return manager.getEntities(null, [breeze.EntityState.Added, breeze.EntityState.Modified, breeze.EntityState.Deleted]);");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine();
            codeBuilder.AddLine("var hasValidationErrors = function(savingData) {");

            codeBuilder.AddLine("    if (savingData instanceof Array) {");
            codeBuilder.AddLine("       for (var idx = 0; idx < savingData.length; idx++) {");
            codeBuilder.AddLine("           var entity = savingData[idx];");
            codeBuilder.AddLine("           if (entity.ChangeState && entity.getValidationErrors && ['I', 'U'].indexOf(entity.ChangeState) >=0) {");
            codeBuilder.AddLine("              var errors = entity.getValidationErrors();");
            codeBuilder.AddLine("              if (errors.length > 0) {");
            codeBuilder.AddLine("                   showModalAlert('Campos obrigatórios não estão preenchidos.', errors);");
            codeBuilder.AddLine("                   return true;");
            codeBuilder.AddLine("               }");
            codeBuilder.AddLine("           }");
            codeBuilder.AddLine("       }");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    else {");
            codeBuilder.AddLine("       var changes = manager.getEntities(null, [breeze.EntityState.Added, breeze.EntityState.Modified]);");
            codeBuilder.AddLine("       for (var idxChange = 0; idxChange < changes.length; idxChange++) {");
            codeBuilder.AddLine("          changes[idxChange].setParentAsModified();");
            codeBuilder.AddLine("       }");
            codeBuilder.AddLine("       changes = manager.getEntities(null, [breeze.EntityState.Added, breeze.EntityState.Modified]);");
            codeBuilder.AddLine("       for (var idxChange = 0; idxChange < changes.length; idxChange++) {");
            codeBuilder.AddLine("          var entity = changes[idxChange];");
            codeBuilder.AddLine("          var isOk = entity.entityAspect.validateEntity();");
            codeBuilder.AddLine("          if (!isOk) {");
            codeBuilder.AddLine("              var errors = entity.entityAspect.getValidationErrors();");
            codeBuilder.AddLine("              var strErrors = [];");
            codeBuilder.AddLine("              for (var idx = 0; idx < errors.length; idx++) {");
            codeBuilder.AddLine("                  var errorMsg = errors[idx].errorMessage;");
            codeBuilder.AddLine("                  var propName = strExtract(errorMsg, \"'\", \"'\");");
            codeBuilder.AddLine("                  var propDisplay = entity.getDisplayName(propName);");
            codeBuilder.AddLine("                  errorMsg = errorMsg.replace(\"'\" + propName + \"'\", \"'\" + propDisplay + \"'\" + (managerAuth.shellMode=='DEV' ? \" (\" + entity.typeName + \".\" + propName + \")\": \"\"));");
            codeBuilder.AddLine("                  strErrors.push(translateError(errorMsg));");
            codeBuilder.AddLine("              }");
            codeBuilder.AddLine("              showModalAlert('Campos obrigatórios não estão preenchidos.', strErrors);");
            codeBuilder.AddLine("              return true;");
            codeBuilder.AddLine("          }");
            codeBuilder.AddLine("       }");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    return false;");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine();
            if (hasLargeDataMode && enableSaveLazingMode)
            {
                codeBuilder.AddLine("var saveChangesFake = function (transactionID, saveSucceeded) {");
                codeBuilder.AddLine("    var dataEntities = _.map(vm.getDataForSaving(), function (entity) { return entity.getPrimitiveDTO(entity.ChangeState != 'D'); });");
                codeBuilder.AddLine("    var dataForSaving = {");
                codeBuilder.AddLine("        TransactionID: transactionID,");
                codeBuilder.AddLine("        ComponentName: vm.__moduleId__,");
                codeBuilder.AddLine("        DataList: dataEntities,");
                codeBuilder.AddLine("        RelationInfo: vm.getViewMapInfo()");
                codeBuilder.AddLine("    };");
                codeBuilder.AddLine("    return $.ajax({");
                codeBuilder.AddLine("        type: 'POST',");
                codeBuilder.AddLine("        crossDomain: true,");
                codeBuilder.AddLine("        url: getServiceAddress('" + api.GetRoutePrefix() + "/Save' + vm.rootDataTypeName + 'InCache'),");
                codeBuilder.AddLine("        globalError: false,");
                codeBuilder.AddLine("        contentType: 'application/json',");
                codeBuilder.AddLine("        async: true,");
                codeBuilder.AddLine("        cache: false,");
                codeBuilder.AddLine("        data: JSON.stringify(dataForSaving),");
                codeBuilder.AddLine("        success: function (response) {");
                codeBuilder.AddLine("            if (saveSucceeded)");
                codeBuilder.AddLine("                saveSucceeded(response);");
                codeBuilder.AddLine("        },");
                codeBuilder.AddLine("        error: function (jqXHR, textStatus, errorThrown) {");
                codeBuilder.AddLine("            failed({ message: jqXHR.responseJSON.ExceptionMessage });");
                codeBuilder.AddLine("        }");
                codeBuilder.AddLine("    });");
                codeBuilder.AddLine("    function failed(error) {");
                codeBuilder.AddLine("        var msg = error.message.replace('Fail by saving data:', '');");
                codeBuilder.AddLine("        showModalAlert('Falha ao salvar informações.', [msg]);");
                codeBuilder.AddLine("        error.message = msg;");
                codeBuilder.AddLine("        throw error;");
                codeBuilder.AddLine("    }");
                codeBuilder.AddLine("};");
                codeBuilder.AddLine();
                codeBuilder.AddLine("var submitAllChanges = function (transactionId, saveSucceeded, failed, completed) {");
                codeBuilder.AddLine("    return $.ajax({");
                codeBuilder.AddLine("        type: 'GET',");
                codeBuilder.AddLine("        crossDomain: true,");
                codeBuilder.AddLine("        url: getServiceAddress('" + api.GetRoutePrefix() + "/submitAllChanges?transactionID=' + transactionId),");
                codeBuilder.AddLine("        globalError: false,");
                codeBuilder.AddLine("        contentType: 'application/json',");
                codeBuilder.AddLine("        async: true,");
                codeBuilder.AddLine("        cache: false,");
                codeBuilder.AddLine("        success: function (response) { if (typeof saveSucceeded === 'function') saveSucceeded(response); },");
                codeBuilder.AddLine("        error: function (jqXHR, textStatus, errorThrown) { failed(jqXHR.responseJSON);}");
                codeBuilder.AddLine("    });");
                codeBuilder.AddLine("}");

                codeBuilder.AddLine("var cancelAllChanges = function (transactionId, saveSucceeded, failed) {");
                codeBuilder.AddLine("    return $.ajax({");
                codeBuilder.AddLine("        type: 'GET',");
                codeBuilder.AddLine("        crossDomain: true,");
                codeBuilder.AddLine("        url: getServiceAddress('" + api.GetRoutePrefix() + "/CancelAllChanges?transactionID=' + transactionId),");
                codeBuilder.AddLine("        globalError: false,");
                codeBuilder.AddLine("        contentType: 'application/json',");
                codeBuilder.AddLine("        async: true,");
                codeBuilder.AddLine("        cache: false,");
                codeBuilder.AddLine("        success: function (response) { if (saveSucceeded) saveSucceeded(response); },");
                codeBuilder.AddLine("        error: function (jqXHR, textStatus, errorThrown) { failed(jqXHR.responseJSON); }");
                codeBuilder.AddLine("    });");
                codeBuilder.AddLine("}");
                codeBuilder.AddLine();

            }
            codeBuilder.AddLine("var saveChanges = function(saveSucceeded, saveFailed, fin, saveNoTRack) {");
            codeBuilder.AddLine("    if (saveNoTRack === true) {");
            codeBuilder.AddLine("        var dataForSaving = JSON.stringify(_.map(vm.getDataForSaving(), function(entity){ return entity.getPrimitiveDTO(entity.ChangeState != 'D'); }));");
            codeBuilder.AddLine("        return $.ajax({");
            codeBuilder.AddLine("           type: 'POST',");
            codeBuilder.AddLine("           crossDomain: true,");
            codeBuilder.AddLine("           url: getServiceAddress('" + api.GetRoutePrefix() + "/Save' + vm.rootDataTypeName),");
            codeBuilder.AddLine("           globalError: false,");
            codeBuilder.AddLine("           contentType: 'application/json',");
            codeBuilder.AddLine("           async: true,");
            codeBuilder.AddLine("           cache: false,");
            codeBuilder.AddLine("           data: dataForSaving,");
            codeBuilder.AddLine("           success: function (response) {");
            codeBuilder.AddLine("                  success(response);");
            codeBuilder.AddLine("           },");
            codeBuilder.AddLine("           error: function (jqXHR, textStatus, errorThrown) {");
            codeBuilder.AddLine("                  failed({ message: jqXHR.responseJSON.ExceptionMessage });");
            codeBuilder.AddLine("           }");
            codeBuilder.AddLine("        });");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    else {");
            codeBuilder.AddLine("        return manager.saveChanges()");
            codeBuilder.AddLine("               .fail(failed).then(success);");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine();
            codeBuilder.AddLine("    function success(result) {");
            codeBuilder.AddLine("        if (fin) fin();");
            codeBuilder.AddLine("        if (saveNoTRack === true && result.length > 0) {");
            codeBuilder.AddLine("           for (var idx = 0; idx < result.length; idx++) { dataContext.initializePOCO(result[idx], vm.rootDataTypeName); }");
            codeBuilder.AddLine("        }");
            codeBuilder.AddLine("        else if (result != null && result.keyMappings != null && result.keyMappings.length > 0) {");
            codeBuilder.AddLine("            for (var idx = 0; idx < result.keyMappings.length; idx++) {");
            codeBuilder.AddLine("                if (result.keyMappings[idx].realValue == null) {");
            codeBuilder.AddLine("                   var entity = manager.getEntityByKey(result.keyMappings[idx].entityTypeName, result.keyMappings[idx].tempValue);");
            codeBuilder.AddLine("                   if (entity) manager.detachEntity(entity);");
            codeBuilder.AddLine("                }");
            codeBuilder.AddLine("            }");
            codeBuilder.AddLine("            manager.acceptChanges();");
            codeBuilder.AddLine("        }");
            codeBuilder.AddLine("        if (saveSucceeded)");
            codeBuilder.AddLine("            saveSucceeded(result);");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine();
            codeBuilder.AddLine("    function failed(error) {");
            codeBuilder.AddLine("        if (fin) fin();");
            codeBuilder.AddLine("        if (error.message.indexOf('Internal Error in key fixup - unable to locate entity') == -1 && error.message.indexOf('An entity with this key is already in the cache:') == -1) {");
            codeBuilder.AddLine("           if (saveFailed)");
            codeBuilder.AddLine("               saveFailed(error);");
            codeBuilder.AddLine("           var msg = error.message.replace('Fail by saving data:', '');");
            codeBuilder.AddLine("           showModalAlert('Falha ao salvar informações.', [ msg ]);");
            codeBuilder.AddLine("           error.message = msg;");
            codeBuilder.AddLine("           throw error;");
            codeBuilder.AddLine("       } else {");
            codeBuilder.AddLine("           manager.acceptChanges();");
            codeBuilder.AddLine("       }");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine();
            codeBuilder.AddLine("var acceptChanges = function () {");
            codeBuilder.AddLine("    return manager.acceptChanges();");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine();
            codeBuilder.AddLine("var getEntities = function (entityName) {");
            codeBuilder.AddLine("    return manager.getEntities(entityName);");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine();
            codeBuilder.AddLine("var notifyPresentation = function(dataSourceName) {");
            codeBuilder.AddLine("      if (dataContext.dataForUpdate !== '') {");
            codeBuilder.AddLine("       setTimeout(function () { notifyPresentation(dataSourceName); }, 100);");
            codeBuilder.AddLine("       return;");
            codeBuilder.AddLine("      }");
            codeBuilder.AddLine("      dataContext.dataForUpdate = dataSourceName;");
            codeBuilder.AddLine("      document.dispatchEvent(dataUpdateEvent);");
            codeBuilder.AddLine("      dataContext.dataForUpdate = '';");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine();
            codeBuilder.AddLine("var getEntityInCache = function (entityName, propertiesReference) {");
            codeBuilder.AddLine("    var keys = [];");
            codeBuilder.AddLine("    if (!isNullOrEmpty(propertiesReference)) {");
            codeBuilder.AddLine("        for (var i = 0; i < metadataInfo[entityName].length; i++) {");
            codeBuilder.AddLine("            if (metadataInfo[entityName][i].isPartOfKey && !isNullOrEmpty(propertiesReference[metadataInfo[entityName][i].key]))");
            codeBuilder.AddLine("                keys.push(propertiesReference[metadataInfo[entityName][i].key]);");
            codeBuilder.AddLine("        }");
            codeBuilder.AddLine("        if (keys.length == 0)");
            codeBuilder.AddLine("            return null;");
            codeBuilder.AddLine("        else {");
            codeBuilder.AddLine("            return manager.getEntityByKey(entityName, (keys.length == 1 ? keys[0] : keys));");
            codeBuilder.AddLine("        }");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    else");
            codeBuilder.AddLine("        return null;");
            codeBuilder.AddLine("}");

            codeBuilder.AddLine();
            codeBuilder.AddLine("var createEntity = function(entityName, initialValues, unchanged) {");
            codeBuilder.AddLine("    var entity = getEntityInCache(entityName, initialValues);");
            codeBuilder.AddLine("    if (!isNullOrEmpty(entity))");
            codeBuilder.AddLine("        entity.entityAspect.entityState == (unchanged === true ? breeze.EntityState.Unchanged : breeze.EntityState.Added);");
            codeBuilder.AddLine("    else ");
            codeBuilder.AddLine("        entity = manager.createEntity(entityName, initialValues, (unchanged === true ? breeze.EntityState.Unchanged : breeze.EntityState.Added));");
            codeBuilder.AddLine("    return entity;");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine();
            codeBuilder.AddLine("var createFreeEntity = function(entityName) {");
            codeBuilder.AddLine("   return manager.createEntity(entityName, {}, breeze.EntityState.Detached);");
            codeBuilder.AddLine("}");

            foreach (var entity in _designerRoot.EntityAdapters)
            {
                codeBuilder.AddLine();
                codeBuilder.AddLine("var create" + entity.Name + " = function(" + (entity.TargetEntityAdapter == null ? "" : "parent, noCurrent") + ") {");

                //Generate Key default values
                string structDefaults = (entity.TargetEntityAdapter == null ? "" : entity.TargetEntityAdapter.Name + ": parent");
                if (entityKeysForDefault.ContainsKey(entity.Name))
                {
                    foreach (string keyRef in entityKeysForDefault[entity.Name])
                    {
                        string defaultValue = "(-1 * getSequence('" + entity.Name + "'))";
                        if (keyRef.Right(":").ToLower().Contains("guid"))
                            defaultValue = "getNewGuid()";
                        else if (keyRef.Right(":").ToLower().Contains("datetime"))
                            defaultValue = "getCurrentDate()";
                        else if (keyRef.Right(":").ToLower().Contains("string"))
                            defaultValue = defaultValue + ".toString()";

                        structDefaults += (structDefaults.IsNullOrEmpty() ? "" : ", ") + keyRef.Left(":") + ": " + defaultValue;
                    }
                }

                //Generate bollean default values
                foreach (var prop in entity.GetAllInheritanceAttributes().Where(e => e.Datatype.ToLower().Contains("bool")))
                {
                    structDefaults += (structDefaults.IsNullOrEmpty() ? "" : ", ") + prop.Name + ": false";
                }

                codeBuilder.AddLine("    //Create entity instance");
                codeBuilder.AddLine("    enableChangeTrack = false;");
                if (entity.GetTopParent().IsBufferSaving())
                {
                    codeBuilder.AddLine("    var defaultVals = { " + structDefaults + " };");
                    codeBuilder.AddLine("    var entityType = manager.metadataStore.getEntityType('" + entity.Name + "');");
                    codeBuilder.AddLine("    var entity = {};");
                    codeBuilder.AddLine("    for (var idx = 0; idx < entityType.dataProperties.length; idx++) { ");
                    codeBuilder.AddLine("        var prop = entityType.dataProperties[idx]; ");
                    codeBuilder.AddLine("        if ((typeof defaultVals[prop.name]) !== 'undefined') entity[prop.name] = defaultVals[prop.name];");
                    codeBuilder.AddLine("        else  entity[prop.name] = prop.defaultValue;");
                    codeBuilder.AddLine("    }");

                    codeBuilder.AddLine("    dataContext.initializePOCO(entity, '" + entity.Name + "');");
                    if (entity.TargetEntityAdapter != null)
                    {
                        codeBuilder.AddLine("    setAbsoluteValue(entity, '" + entity.TargetEntityAdapter.Name + "', parent);");
                        foreach (var rel in entity.GetAllParentKeystAssociation(true))
                        {
                            codeBuilder.AddLine("    setAbsoluteValue(entity, '" + rel.Key + "', getAbsoluteValue(parent." + rel.Value + "));");
                        }
                    }
                }
                else
                {
                    codeBuilder.AddLine("    var entity = createEntity('" + entity.Name + "'" + (structDefaults.IsNullOrEmpty() ? "" : ", { " + structDefaults + " }") + ");");
                }
                codeBuilder.AddLine("    entity.setDefaults();");

                if (entity.GetTopParent().IsBufferSaving())
                {
                    codeBuilder.AddLine("    setAbsoluteValue(entity, 'ChangeState', 'I');");
                }

                codeBuilder.AddLine("    if (typeof entity.OnAdding == 'function') {");
                codeBuilder.AddLine("        if (!entity.OnAdding()) { dataContext.deleteEntity(entity); return; }");
                codeBuilder.AddLine("    }");

                if (entity.TargetEntityAdapter != null)
                {
                    codeBuilder.AddLine("    if (noCurrent !== true) parent.current" + entity.Name + "(entity);");
                    if (entity.GetTopParent().IsBufferSaving())
                    {
                        codeBuilder.AddLine("    if (parent && (typeof parent." + entity.Name + "List === 'function')) parent." + entity.Name + "List().push(entity);");
                    }
                    codeBuilder.AddLine("    if (parent && (typeof parent.setCurrentDetails === 'function') && (typeof parent." + entity.Name + "List === 'function') && parent." + entity.Name + "List().length == 0) parent.setCurrentDetails('" + entity.Name + "');");

                    codeBuilder.AddLine("    if (entity.setParentAsModified) entity.setParentAsModified();");
                }
                codeBuilder.AddLine("    enableChangeTrack = true;");
                codeBuilder.AddLine("    return entity;");
                codeBuilder.AddLine("};");
            }

            codeBuilder.AddLine();
            codeBuilder.AddLine("var deleteEntity = function (entity) {");
            codeBuilder.AddLine("    entity.delete();");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine();
            codeBuilder.AddLine("var detachEntity = function (entity) {");
            codeBuilder.AddLine("    if (!entity.isDetached()) entity.entityAspect.setDetached();");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine();
            codeBuilder.AddLine("var attachEntity = function (entity) {");
            codeBuilder.AddLine("    manager.attachEntity(entity);");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine();
            codeBuilder.AddLine("var clearAll = function () {");
            codeBuilder.AddLine("    manager.rejectChanges();");
            codeBuilder.AddLine("    manager.clear();");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine();
            codeBuilder.AddLine("var executeQuery = function (getMethod, jEntitySearch, order, skip, take, noTracking, callBack) {");
            codeBuilder.AddLine("    var query = EntityQuery.from(getMethod).noTracking(noTracking)");
            codeBuilder.AddLine("    .orderBy(order);");
            codeBuilder.AddLine();
            codeBuilder.AddLine("    if ((typeof jEntitySearch !== 'undefined') && jEntitySearch !== null)");
            codeBuilder.AddLine("        query = query.withParameters({ jEntitySearch: jEntitySearch });");
            codeBuilder.AddLine();
            codeBuilder.AddLine("    if (take > 0)");
            codeBuilder.AddLine("       query = query.skip(skip).take(take);");
            codeBuilder.AddLine("    query = query.inlineCount(true);");
            codeBuilder.AddLine();
            codeBuilder.AddLine("    return manager.executeQuery(query)");
            codeBuilder.AddLine("    .fail(queryFailed).then(function (data) {");
            codeBuilder.AddLine("        if (callBack) {");
            codeBuilder.AddLine("            callBack(data.results);");
            codeBuilder.AddLine("        }");
            codeBuilder.AddLine("    });");
            codeBuilder.AddLine("};");

            #region exportToExcel
            codeBuilder.AddLine("var exportToExcel = function(entityName, jEntitySearch, translatedJEntitySearch, complete, columnsVisible) {");
            codeBuilder.AddLine("    var info = jQuery.grep(dataExportInfo[vm.rootDataTypeName], function (item, i) { return (item.name === entityName);});");
            codeBuilder.AddLine("    if (info == null || info.length === 0) {");
            codeBuilder.AddLine("        app.showMessage('Erro na exportação', 'Alerta', ['Ok']);");
            codeBuilder.AddLine("        return;");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    $.ajax({");
            codeBuilder.AddLine("       type: 'POST',");
            codeBuilder.AddLine("       crossDomain: true,");
            codeBuilder.AddLine("       url: getServiceAddress(info[0].actionExport),");
            codeBuilder.AddLine("       globalError: true,");
            codeBuilder.AddLine("       headers: managerAuth.getHeaders(),");
            codeBuilder.AddLine("       contentType: 'application/json',");
            codeBuilder.AddLine("       async: true,");
            codeBuilder.AddLine("       cache: false,");
            codeBuilder.AddLine("       data: JSON.stringify([jEntitySearch, translatedJEntitySearch, columnsVisible]),");
            codeBuilder.AddLine("       success: function (response) {");
            codeBuilder.AddLine("           if (response.startsWith('~/FileDownload/')) {");
            codeBuilder.AddLine("                saveURL(getBaseServiceAddress(info[0].actionExport) + response.substr(1), entityName + '.xlsx');");
            codeBuilder.AddLine("           } else {");
            codeBuilder.AddLine("                saveExcelBlob(entityName + '.xlsx', response);");
            codeBuilder.AddLine("           }");
            codeBuilder.AddLine("       },");
            codeBuilder.AddLine("       complete: function (jqXHR, textStatus) {");
            codeBuilder.AddLine("           if(complete) complete();");
            codeBuilder.AddLine("       },");
            codeBuilder.AddLine("       error: function (jqXHR, textStatus, errorThrown) {");
            codeBuilder.AddLine("       }");
            codeBuilder.AddLine("    });");
            codeBuilder.AddLine("};");
            #endregion

            #region exportReportDataSource
            codeBuilder.AddLine("var exportReportDataSource = function(complete) {");
            codeBuilder.AddLine("    $.ajax({");
            codeBuilder.AddLine("       type: 'GET',");
            codeBuilder.AddLine("       crossDomain: true,");
            codeBuilder.AddLine("       headers: managerAuth.getHeaders(),");
            codeBuilder.AddLine("       url: getServiceAddress(\"" + api.GetRoutePrefix() + "/GetReportDataSource\"),");
            codeBuilder.AddLine("       success: function (response) {");
            codeBuilder.AddLine("              saveExcelBlob('datasource.ldsx', response);");
            codeBuilder.AddLine("       },");
            codeBuilder.AddLine("       complete: function (jqXHR, textStatus) {");
            codeBuilder.AddLine("              if(complete) complete();");
            codeBuilder.AddLine("       },");
            codeBuilder.AddLine("       error: function (jqXHR, textStatus, errorThrown) {");
            codeBuilder.AddLine("              alert('" + "Erro na exportação do data source".Translate() + "');");
            codeBuilder.AddLine("       }");
            codeBuilder.AddLine("    });");
            codeBuilder.AddLine("};");
            #endregion

            #region exportTemplateReport
            codeBuilder.AddLine("var exportTemplateReport = function(reportPath, complete) {");
            codeBuilder.AddLine("    $.ajax({");
            codeBuilder.AddLine("       type: 'GET',");
            codeBuilder.AddLine("       crossDomain: true,");
            codeBuilder.AddLine("       headers: managerAuth.getHeaders(),");
            codeBuilder.AddLine("       url: getServiceAddress(\"" + api.GetRoutePrefix() + "/GetTemplateReport\"),");
            codeBuilder.AddLine("       data: { reportPath: reportPath },");
            codeBuilder.AddLine("       success: function (response) {");
            codeBuilder.AddLine("              saveExcelBlob(reportPath + '.lrtx', response);");
            codeBuilder.AddLine("       },");
            codeBuilder.AddLine("       complete: function (jqXHR, textStatus) {");
            codeBuilder.AddLine("              if(complete) complete();");
            codeBuilder.AddLine("       },");
            codeBuilder.AddLine("       error: function (jqXHR, textStatus, errorThrown) {");
            codeBuilder.AddLine("              alert('" + "Erro na exportação do data source".Translate() + "');");
            codeBuilder.AddLine("       }");
            codeBuilder.AddLine("    });");
            codeBuilder.AddLine("};");
            #endregion

            #region exportToReport
            codeBuilder.AddLine("var exportToReport = function(reportName, entityName, jEntitySearch, translatedJEntitySearch, complete, columnsVisible, exportMedia) {");
            codeBuilder.AddLine("    var info = jQuery.grep(dataExportInfo[vm.rootDataTypeName], function (item, i) { return (item.name === entityName);});");
            codeBuilder.AddLine("    if (info == null || info.length === 0) {");
            codeBuilder.AddLine("        app.showMessage('Erro na exportação', 'Alerta', ['Ok']);");
            codeBuilder.AddLine("        return;");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    $.ajax({");
            codeBuilder.AddLine("       type: 'POST',");
            codeBuilder.AddLine("       crossDomain: true,");
            codeBuilder.AddLine("       headers: managerAuth.getHeaders(),");
            codeBuilder.AddLine("       url: getServiceAddress(info[0].actionReport),");
            codeBuilder.AddLine("       globalError: true,");
            codeBuilder.AddLine("       contentType: 'application/json',");
            codeBuilder.AddLine("       async: true,");
            codeBuilder.AddLine("       cache: false,");
            codeBuilder.AddLine("       data: JSON.stringify([ reportName, jEntitySearch, translatedJEntitySearch, columnsVisible, getServiceAddress(''), exportMedia ]),");
            codeBuilder.AddLine("       success: function (response) {");
            codeBuilder.AddLine("              saveExcelBlob(entityName + '.lrtx', response);");
            codeBuilder.AddLine("       },");
            codeBuilder.AddLine("       complete: function (jqXHR, textStatus) {");
            codeBuilder.AddLine("              if(complete) complete();");
            codeBuilder.AddLine("       },");
            codeBuilder.AddLine("       error: function (jqXHR, textStatus, errorThrown) {");
            //codeBuilder.AddLine("              alert('" + "Erro na exportação do Relatório".Translate() + "');");
            codeBuilder.AddLine("       }");
            codeBuilder.AddLine("    });");
            codeBuilder.AddLine("};");
            #endregion

            codeBuilder.AddLine();
            codeBuilder.AddLine("var hasChanges = ko.observable(false);");
            codeBuilder.AddLine();
            codeBuilder.AddLine("manager.hasChangesChanged.subscribe(function(eventArgs) {");
            codeBuilder.AddLine("    hasChanges(eventArgs.hasChanges);");
            codeBuilder.AddLine("});");
            codeBuilder.AddLine();

            codeBuilder.AddLine("//#region Internal methods");

            codeBuilder.AddLine();
            codeBuilder.AddLine("function queryFailed(error) {");
            //codeBuilder.AddLine("    var msg = error.message;");
            //codeBuilder.AddLine("    if (isNullOrEmpty(msg)) return;");
            //codeBuilder.AddLine("    showModalAlert('Falha ao pesquisar informações.', [ msg ]);");
            //codeBuilder.AddLine("    error.message = msg;");
            //codeBuilder.AddLine("    throw error;");
            codeBuilder.AddLine("}");
            codeBuilder.AddLine();


            codeBuilder.AddLine("function log(msg, data) {");
            codeBuilder.AddLine("    logger.log(msg, data, system.getModuleId(dataContext), true);");
            codeBuilder.AddLine("}");
            codeBuilder.AddLine();
            codeBuilder.AddLine("function logError(msg, error) {");
            codeBuilder.AddLine("     logger.logError(msg, error, system.getModuleId(dataContext), true);");
            codeBuilder.AddLine("}");
            codeBuilder.AddLine();

            codeBuilder.AddLine("function loadParameters() {");
            if (!parameterNames.IsNullOrEmpty())
                codeBuilder.AddLine(" dataParameters.registerParameters('" + parameterNames + "');");
            else
                codeBuilder.AddLine(" dataParameters.isLoaded = true;");
            codeBuilder.AddLine("}");
            codeBuilder.AddLine();

            codeBuilder.AddLine("//#endregion Internal methods");
            #region getWithBinding
            codeBuilder.AddLine("//#region getWithBinding");
            codeBuilder.AddLine("var createDTOEmpty = function (entityName) {");
            codeBuilder.AddLine("    var entityType = manager.metadataStore.getEntityType(entityName);");
            codeBuilder.AddLine("    var entity = {};");
            codeBuilder.AddLine("    for (var idx = 0; idx < entityType.dataProperties.length; idx++) {");
            codeBuilder.AddLine("        var prop = entityType.dataProperties[idx];");
            codeBuilder.AddLine("        entity[prop.name] = prop.defaultValue;");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    for (var idx = 0; idx < entityType.navigationProperties.length; idx++) {");
            codeBuilder.AddLine("        var navigationName = entityType.navigationProperties[idx].name;");
            codeBuilder.AddLine("        if (navigationName.endsWith('List'))");
            codeBuilder.AddLine("            entity['current' + navigationName.replace('List', '')] = null;");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    entity.isEmptyEntity = true;");
            codeBuilder.AddLine("    return entity;");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var getWithBinding = function (binding, entityName) {");
            codeBuilder.AddLine("    if (typeof binding === 'function' && binding() != null)");
            codeBuilder.AddLine("        return binding;");
            codeBuilder.AddLine("    else");
            codeBuilder.AddLine("        return createDTOEmpty(entityName);");
            codeBuilder.AddLine("}");
            codeBuilder.AddLine("//#endregion getWithBinding");
            #endregion
            codeBuilder.AddLine();
            codeBuilder.AddLine("    var vm = null;");
            codeBuilder.AddLine("    var dataContext = {");
            codeBuilder.AddLine("        dataForUpdate: '',");
            codeBuilder.AddLine("        getPivotLayouts: getPivotLayouts,");
            codeBuilder.AddLine("        getServiceAddress: getServiceAddress,");
            codeBuilder.AddLine("        getBaseServiceAddress: getBaseServiceAddress,");
            codeBuilder.AddLine("        getDataFeedUrl: getDataFeedUrl,");
            codeBuilder.AddLine("        getDataServiceUrl: getDataServiceUrl,");
            codeBuilder.AddLine("        setServiceBusUrl: setServiceBusUrl,");
            codeBuilder.AddLine("        initializePOCO: initializePOCO,");
            codeBuilder.AddLine("        getWithBinding: getWithBinding,");
            codeBuilder.AddLine("        managerAuth: managerAuth,");
            codeBuilder.AddLine("        hasDataFeed: " + api.IsDataService.ToString().ToLower() + ",");
            codeBuilder.AddLine("        getAccessGroup: getAccessGroup,");
            codeBuilder.AddLine("        getNewGuid: getNewGuid,");
            codeBuilder.AddLine("        metadataInfo: metadataInfo,");
            codeBuilder.AddLine("        dataExportInfo: dataExportInfo,");
            codeBuilder.AddLine("        entityNames: entityNames,");
            codeBuilder.AddLine("        lookUpNames: lookUpNames,");
            codeBuilder.AddLine("        lookUpProperties: lookUpProperties,");
            codeBuilder.AddLine("        metadataStore: metadataStore,");
            codeBuilder.AddLine("        cancelChanges: cancelChanges,");
            codeBuilder.AddLine("        saveChanges: saveChanges,");
            if (hasLargeDataMode && enableSaveLazingMode)
            {
                codeBuilder.AddLine("        saveChangesFake: saveChangesFake,");
                codeBuilder.AddLine("        submitAllChanges: submitAllChanges,");
                codeBuilder.AddLine("        cancelAllChanges: cancelAllChanges,");
            }
            codeBuilder.AddLine("        getChanges: getChanges,");
            codeBuilder.AddLine("        hasValidationErrors: hasValidationErrors,");
            codeBuilder.AddLine("        getEntityProperty: getEntityProperty,");
            codeBuilder.AddLine("        getViewInfo: getViewInfo,");
            codeBuilder.AddLine("        createEntity: createEntity,");
            codeBuilder.AddLine("        notifyPresentation: notifyPresentation,");
            codeBuilder.AddLine("        createFreeEntity: createFreeEntity,");
            foreach (var entity in _designerRoot.EntityAdapters)
            {
                codeBuilder.AddLine("        create" + entity.Name + ": create" + entity.Name + ",");
            }
            codeBuilder.AddLine("        deleteEntity: deleteEntity,");
            codeBuilder.AddLine("        acceptChanges: acceptChanges,");
            codeBuilder.AddLine("        getEntities: getEntities,");
            codeBuilder.AddLine("        detachEntity: detachEntity,");
            codeBuilder.AddLine("        attachEntity: attachEntity,");
            codeBuilder.AddLine("        executeQuery: executeQuery,");
            codeBuilder.AddLine("        sharedData: [],");
            codeBuilder.AddLine("        clearAll: clearAll,");
            codeBuilder.AddLine("        hasChanges: hasChanges,");
            codeBuilder.AddLine("        dataDomains: dataDomains,");
            codeBuilder.AddLine("        dataParameters: dataParameters,");
            codeBuilder.AddLine("        loadParameters: loadParameters,");
            codeBuilder.AddLine("        exportToExcel: exportToExcel,");
            codeBuilder.AddLine("        exportToReport: exportToReport,");
            codeBuilder.AddLine("        exportReportDataSource: exportReportDataSource,");
            codeBuilder.AddLine("        exportTemplateReport: exportTemplateReport,");
            codeBuilder.AddLine("        businessAssemblyName: businessAssemblyName,");
            codeBuilder.AddLine("        controllerName: controllerName,");
            codeBuilder.AddLine("        getResultsCombo: getResultsCombo,");
            codeBuilder.AddLine("        clientFilterHasModified: clientFilterHasModified,");
            codeBuilder.AddLine("        lastClientFilterExpressions: {},");
            codeBuilder.AddLine("        breeze: breeze,");
            codeBuilder.AddLine("        contextUpdtEvt: contextUpdtEvt,");
            codeBuilder.AddLine("        setCurrentViewModel: function(vModel) { vm = vModel; },");
            codeBuilder.AddLine(queryMethods + (!finalizerMethods.IsNullOrEmpty() ? "," : String.Empty));

            if (!finalizerMethods.IsNullOrEmpty())
                codeBuilder.AddLine(finalizerMethods);

            codeBuilder.AddLine("    };");

            //parameterTitle{variationKey1|variationValue1|variationKey2|variationValue2|...|variationKeyN|variationValueN},parameterTitleN
            codeBuilder.AddLine("loadParameters();");

            codeBuilder.AddLine("return dataContext;");

            codeBuilder.AddLine("//#endregion Context Definition");

            codeBuilder.DecreaseIndent();
            codeBuilder.AddLine("}");

            codeBuilder.AddLine("return result;");

            codeBuilder.AddLine("});");

        }

        /// <summary>
        /// Generate View code.
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="codeBuilder"></param>
        private void GenerateSpaViewCode(EntityAdapterUserInterface ui, Linx.Tools.CodeBuilder codeBuilder)
        {
            var uiEntityAdapter = ui.GetDirectEntityAdapter();
            string masterEntityName = (uiEntityAdapter.IsNull() ? "" : uiEntityAdapter.Name), entitiesRef = masterEntityName + "s";
            bool hasDataContext = !uiEntityAdapter.IsNull() || !ui.Subscription.IsNull();
            if (ui.GeneratingType == DomainGeneratingType.CustomizableLayout || ui.LayoutDefinition == null)
                return;

            PublicationEntity entityAdapter = ui.GetEntityAdapter();

            //For getting the default layout, use like this:
            HtmlCodeGen viewGen = new HtmlCodeGen(ui.LayoutDefinition, uiEntityAdapter.IsNull() ? ui.Name : uiEntityAdapter.Name, ui.Name);
            var viewCode = viewGen.GetCode("           ");

            bool hasWizard = !ui.EnableWizardTableView && ui.LayoutDefinition.GetLayoutElementsByClass("WizardControl").Count > 0;

            //For getting the data grid layout, use like this:
            HtmlCodeGen viewDataGridGen = null;
            if (!hasWizard && !viewGen.HasMainTopDataGrid)
            {
                var dgLayout = ui.LayoutDefinition.GetDataGridLayout();
                dgLayout.IsSecundary = true;
                var dg = dgLayout.GetLayoutElementsByClass("DataGrid").FirstOrDefault() as LayoutContainer;
                if (dg != null)
                {
                    dg.IsTemplate = false;
                    if (ui.PageSize < 0)
                        dg.PageSize = 100;
                }
                viewDataGridGen = new HtmlCodeGen(dgLayout, uiEntityAdapter.IsNull() ? ui.Name : uiEntityAdapter.Name, ui.Name);
            }

            #region Toolbar and brand control
            //Toolbar

            codeBuilder.AddLine("<div data-bind=\"attr: { transactionNumberControl: transactionNumberControl }\"></div>");
            codeBuilder.AddLine("<div class=\"topo-container\" data-bind=\"if: !isDependentVM()\">");
            codeBuilder.AddLine("   <div id=\"mainHeader\">");
            codeBuilder.AddLine("        <!--ko compose: { model:'viewmodels/shell/_header', view:'views/shell/_header', activationData: { parentVM: $root }, preserveContext: false }-->");
            codeBuilder.AddLine("       <!--/ko-->");
            codeBuilder.AddLine("   </div>");
            codeBuilder.AddLine("</div>");

            codeBuilder.AddLine("<div data-bind=\"if: !isDependentVM()\">");
            codeBuilder.AddLine("   <div id=\"breadcrumb\" class=\"style-breadcrumb\" data-bind=\"widget: { kind: 'breadcrumb' }\"></div>");
            codeBuilder.AddLine("</div>");
            codeBuilder.AddLine("<div class=\"clearfix\" data-bind=\"if: !isDependentVM()\">");
            codeBuilder.AddLine("</div>");
            codeBuilder.AddLine("<!-- BEGIN CONTAINER -->");
            codeBuilder.AddLine("<div data-bind=\"css: { 'page-container fullbtn': !isDependentVM() }\">");
            codeBuilder.IncreaseIndent();

            codeBuilder.AddLine("   <!-- ko if: isDependentVM() && !hideToolbar() -->");
            codeBuilder.AddLine("   <!-- BEGIN SIDEBAR -->");
            codeBuilder.AddLine("   <div data-bind=\"css: { hide: (!hasBrand) }, widget: { kind: 'branditem', vm: $root }\" ></div>");
            codeBuilder.AddLine("   <div class=\"pull-right redebar\" data-bind=\"css: { hide: (!hasBrand || status() !== 'C') }, widget: { kind: 'brand', vm: $root }\"></div>");
            codeBuilder.AddLine("   <div data-bind=\"css: { hide: (!hideToolbar) }, widget: { kind: 'datatoolbar', vm: $data }\" ></div>");
            codeBuilder.AddLine("   <div class=\"clearfix\"></div>");
            codeBuilder.AddLine("   <!-- END SIDEBAR -->");
            codeBuilder.AddLine("   <!--/ko-->");
            #endregion

            //Form Content
            codeBuilder.AddLine("<!-- BEGIN PAGE -->");

            if (viewDataGridGen != null)
            {
                codeBuilder.AddLine("<section>");
                codeBuilder.IncreaseIndent();
                codeBuilder.AddLine("<div id=\"" + ui.Name + "_formViewer\" class=\"panelTransaition\">");
                codeBuilder.IncreaseIndent();
                codeBuilder.AddLine("<figure id=\"" + ui.Name + "_formViewer_front\" class=\"front\">");
            }


            //Generate Form Code
            codeBuilder.IncreaseIndent();
            codeBuilder.AddLine("<div class=\"col-md-12 remove-pl remove-pr\">");
            codeBuilder.IncreaseIndent();
            codeBuilder.AddLine("<section>");
            codeBuilder.IncreaseIndent();
            if (ui.LayoutDefinition.Containers.Count == 1)
            {
                codeBuilder.AddLine("<div class=\"screen-view\">");
                codeBuilder.IncreaseIndent();
            }
            if (viewDataGridGen != null)
            {
                codeBuilder.AddLine("<div data-bind=\"style: { display: $root." + ui.Name + "().viewType() === 'Main' ? '' : 'none' }\">");
                codeBuilder.IncreaseIndent();
            }
            codeBuilder.AddLine(viewCode);
            if (viewDataGridGen != null)
            {
                codeBuilder.DecreaseIndent();
                codeBuilder.AddLine("</div>");
            }
            if (ui.LayoutDefinition.Containers.Count == 1)
            {
                codeBuilder.DecreaseIndent();
                codeBuilder.AddLine("</div>");
            }
            codeBuilder.DecreaseIndent();
            codeBuilder.AddLine("</section>");
            codeBuilder.DecreaseIndent();
            codeBuilder.AddLine("</div>");
            codeBuilder.DecreaseIndent();


            if (viewDataGridGen != null)
            {
                codeBuilder.AddLine("</figure>");
                codeBuilder.AddLine("<figure id=\"" + ui.Name + "_formViewer_back\" class=\"back\">");

                //Generate DataGrid Code                
                codeBuilder.IncreaseIndent();
                codeBuilder.AddLine("<div class=\"col-md-12 remove-pl remove-pr\">");
                codeBuilder.IncreaseIndent();
                codeBuilder.AddLine("<section>");
                codeBuilder.IncreaseIndent();
                codeBuilder.AddLine("<div data-bind=\"style: { display: $root." + ui.Name + "().viewType() === 'Main' ? 'none' : '' }\">");
                codeBuilder.IncreaseIndent();
                codeBuilder.AddLine(viewDataGridGen.GetCode("           "));
                codeBuilder.DecreaseIndent();
                codeBuilder.AddLine("</div>");
                codeBuilder.DecreaseIndent();
                codeBuilder.AddLine("</section>");
                codeBuilder.DecreaseIndent();
                codeBuilder.AddLine("</div>");
                codeBuilder.DecreaseIndent();

                codeBuilder.AddLine("</figure>");
                codeBuilder.DecreaseIndent();
                codeBuilder.AddLine("</div>");
                codeBuilder.DecreaseIndent();
                codeBuilder.AddLine("</section>");
            }


            codeBuilder.AddLine("<!-- END PAGE -->");

            codeBuilder.DecreaseIndent();
            codeBuilder.AddLine("</div>");
            codeBuilder.AddLine("<!-- END CONTAINER -->");

            //Code Complements
            string complementaryCalls = viewGen.ComplementaryCalls.ToString();
            if (!complementaryCalls.IsNullOrEmpty())
                codeBuilder.ComplementaryCalls.Add(complementaryCalls);

            if (viewDataGridGen != null)
            {
                complementaryCalls = viewDataGridGen.ComplementaryCalls.ToString();
                if (!complementaryCalls.IsNullOrEmpty())
                    codeBuilder.ComplementaryCalls.Add(complementaryCalls);
            }


            string complementaryCode = viewGen.ComplementaryCode.ToString();
            if (viewDataGridGen != null)
            {
                var complementaryCodeDG = viewDataGridGen.ComplementaryCode.ToString();
                if (!complementaryCodeDG.IsNullOrEmpty())
                    complementaryCode += (complementaryCode.IsNullOrEmpty() ? "" : "\r\n") + complementaryCodeDG;
            }

            if (!complementaryCode.IsNullOrEmpty())
            {
                codeBuilder.ComplementaryCode.AddLine("define(['managers/__auth', 'managers/user'], function (managerAuth, managerUser) {");
                codeBuilder.ComplementaryCode.IncreaseIndent();
                codeBuilder.ComplementaryCode.AddLine("var complementCtor = function() {");
                codeBuilder.ComplementaryCode.IncreaseIndent();
                codeBuilder.ComplementaryCode.AddLine("var complement = {");
                codeBuilder.ComplementaryCode.IncreaseIndent();
                codeBuilder.ComplementaryCode.AddLine("isAutomatic: true");

                codeBuilder.ComplementaryCode.AddLines(complementaryCode);

                codeBuilder.ComplementaryCode.DecreaseIndent();
                codeBuilder.ComplementaryCode.AddLine("};");
                codeBuilder.ComplementaryCode.AddLine();
                codeBuilder.ComplementaryCode.AddLine("return complement;");
                codeBuilder.ComplementaryCode.DecreaseIndent();
                codeBuilder.ComplementaryCode.AddLine("}");
                codeBuilder.ComplementaryCode.AddLine();
                codeBuilder.ComplementaryCode.AddLine("return complementCtor;");
                codeBuilder.ComplementaryCode.DecreaseIndent();
                codeBuilder.ComplementaryCode.AddLine("});");
            }
            //todo: refatorar
            if (!viewGen.ComplementaryEvents.IsNull())
                viewGen.ComplementaryEvents.Foreach(ce => codeBuilder.AddEventCode(ce.Key, ce.Value.ToString()));
        }

        private bool HasOlapServiceConnection(EntityAdapterUserInterface ui)
        {
            var hasOlapServiceConnection = false;
            var olapLayoutElement = ui.LayoutDefinition.GetLayoutElementsByClass("FlatPivotGrid");

            if (olapLayoutElement != null && olapLayoutElement.Any())
            {
                var olapLayoutContainer = ((LayoutContainer)olapLayoutElement.First());

                hasOlapServiceConnection =
                    (!string.IsNullOrEmpty(olapLayoutContainer.PivotDataSource) && olapLayoutContainer.PivotDataSource.ToLower().Equals("olap"));
            }

            return hasOlapServiceConnection;
        }


        private string GenerateLayoutForVM(object element, EntityAdapterUserInterface ui, string typeElement = null)
        {
            var ret = String.Empty;
            var parentContainerGrid = String.Empty;
            var checkDisplayIsPivot = string.Empty;
            if (element is Linx.Tools.LayoutControlV2)
            {
                Linx.Tools.LayoutControlV2 control = (Linx.Tools.LayoutControlV2)element;
                ret += "Name: \"" + ui.Name + "_" + control.GetControlName(control.GetPrefix()) + "\", ";
                ret += "DisplayName: \"" + control.DisplayName + "\", ";
                ret += "ColumnSpan: " + control.ColumnSpan + ", ";
                ret += "Visible: " + control.IsVisible.ToString().ToLower() + ", ";
                if (control.ClassName == "LookUpTextBox" && !control.LookUpName.IsNullOrEmpty())
                    ret += "LookUpName: \"" + control.LookUpName + "\", ";
                ret += "Key: \"" + control.BindingPath.Split('.').Last() + "\"";
            }
            else if (element is Linx.Tools.LayoutContainer)
            {
                Linx.Tools.LayoutContainer container = (Linx.Tools.LayoutContainer)element;
                parentContainerGrid = container.GetControlName(container.GetPrefix()).Contains("dGrid") ? container.GetControlName(container.GetPrefix()) : null;
                bool isFlatPivotGrid = container.ClassName.Contains("FlatPivotGrid");

                ret += "Name: \"" + ui.Name + "_" + container.GetControlName(container.GetPrefix()) + "\", ";
                ret += "DisplayName: \"" + container.DisplayName + "\", ";
                ret += "ColumnSpan: " + container.ColumnSpan + ", ";
                ret += "Visible: " + container.IsVisible.ToString().ToLower() + ", ";
                if (container.Controls.Count > 0)
                {
                    ret += "Items: [";
                    container.Controls.ForEach(control =>
                    {
                        if (control is Linx.Tools.LayoutContainer)
                        {
                            if (parentContainerGrid.IsNullOrEmpty())
                                ret += GenerateLayoutForVM(control, ui);
                            else
                                ret += GenerateContainerIntoGrid(control, ui, parentContainerGrid);
                        }
                        else
                             if (parentContainerGrid.IsNullOrEmpty() && !isFlatPivotGrid)
                            ret += GenerateLayoutForVM(control, ui, null);
                        else
                                if (!parentContainerGrid.IsNullOrEmpty())
                            ret += GenerateLayoutIntoGrid(control, ui, parentContainerGrid);
                        else
                            ret += GenerateLayoutIntoPivot(control, ui, container.DisplayName, container.GetControlName(container.GetPrefix()));
                    });
                    ret += "]";
                }
            }
            return "\r\n\t {" + ret + "},";
        }

        private string GenerateLayoutIntoGrid(object element, EntityAdapterUserInterface ui, string typeElement = null)
        {
            var ret = String.Empty;
            var parentElement = typeElement;

            Linx.Tools.LayoutControlV2 control = (Linx.Tools.LayoutControlV2)element;
            ret += "Id: \"" + ui.Name + "_" + control.GetControlName(control.GetPrefix()) + "\", ";
            ret += "Name: \"" + ui.Name + "_" + parentElement + "_" + control.Name + "\", ";
            ret += "DisplayName: \"" + control.DisplayName + "\", ";
            ret += "ColumnSpan: " + control.ColumnSpan + ", ";
            ret += "Visible: " + control.IsVisible.ToString().ToLower() + ", ";
            if (control.ClassName == "LookUpTextBox" && !control.LookUpName.IsNullOrEmpty())
                ret += "LookUpName: \"" + control.LookUpName + "\", ";
            ret += "Key: \"" + control.BindingPath.Split('.').Last() + "\"";

            return "\r\n\t {" + ret + "},";

        }

        private string GenerateLayoutIntoPivot(object element, EntityAdapterUserInterface ui, string typeElement, string parentElement)
        {
            var ret = String.Empty;

            Linx.Tools.LayoutControlV2 control = (Linx.Tools.LayoutControlV2)element;
            ret += "Name: \"" + ui.Name + "_" + control.GetControlName(control.GetPrefix()) + "_pivot\", ";
            ret += "Id: \"" + ui.Name + "_" + parentElement + "_" + control.Name + "\", ";
            ret += "DisplayName: \"" + control.DisplayName + "\", ";
            ret += "ColumnSpan: " + control.ColumnSpan + ", ";
            ret += "Visible: " + control.IsVisible.ToString().ToLower() + ", ";
            ret += "Key: \"" + control.BindingPath.Split('.').Last() + "\", ";

            if (control.Group.IsNullOrEmpty())
                ret += "DimensionUniqueName: \"" + typeElement + "\" ";
            else
                ret += "DimensionUniqueName: \"" + control.Group + "\" ";

            return "\r\n\t {" + ret + "},";
        }
        private string GenerateContainerIntoGrid(object element, EntityAdapterUserInterface ui, string typeElement = null)
        {
            var ret = String.Empty;
            var parentElement = typeElement;

            Linx.Tools.LayoutContainer container = (Linx.Tools.LayoutContainer)element;
            ret += "Name: \"" + ui.Name + "_" + container.GetControlName(container.GetPrefix()) + "\", ";
            ret += "DisplayName: \"" + container.DisplayName + "\", ";
            ret += "ColumnSpan: " + container.ColumnSpan + ", ";
            ret += "Visible: " + container.IsVisible.ToString().ToLower() + ", ";
            if (container.Controls.Count > 0)
            {
                ret += "Items: [";
                var controlType = String.Empty;
                container.Controls.ForEach(control =>
                {
                    if (control is Linx.Tools.LayoutContainer)
                        ret += GenerateContainerIntoGrid(control, ui, parentElement);
                    else
                        ret += GenerateLayoutIntoGrid(control, ui, parentElement);
                });
                ret += "]";
            }

            return "\r\n\t {" + ret + "},";
        }

        /// <summary>
        /// Generate ViewModel code.
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="codeBuilder"></param>
        /// <param name="msEngine"></param>
        /// <param name="complementaryCalls"></param>
        private void GenerateSpaViewModelCode(EntityAdapterUserInterface ui, Linx.Tools.CodeBuilder codeBuilder, MacroScriptEngine msEngine, string complementaryCalls, string eventChangedBrand)
        {
            var uiEntityAdapter = ui.GetDirectEntityAdapter();
            string nameSpacePkg = _designerRoot.GetNamespace(this.GetSpaProject()).Replace(".", "-").ToLower();
            string contextName = GetSpaContextName(), viewModel = ui.Name, masterEntityName = (uiEntityAdapter.IsNull() ? "" : uiEntityAdapter.Name), entitiesRef = "dataView", packageName = "pkg_" + nameSpacePkg;
            bool hasDataContext = !uiEntityAdapter.IsNull() || !ui.Subscription.IsNull();
            codeBuilder.AddLine("define(['durandal/app', " + (hasDataContext ? "'" + packageName + "/services/" + contextName + "', " : "") + "'plugins/router', 'plugins/widget', 'managers/__auth', 'viewmodels/shared/modal', 'viewmodels/shared/modal2', 'managers/brand', 'managers/predefinedFilters', 'services/logger', 'viewmodels/shared/modalMultimidia', 'common'" + (ui.HasCustomization ? ", '" + packageName + "/viewmodels/" + ui.Name + "Custom'" : "") + (!complementaryCalls.IsNullOrEmpty() ? ", '" + packageName + "/viewmodels/" + ui.Name + "Complement'" : "") + ", 'viewmodels/shared/modalCustomSearch'],");
            codeBuilder.AddLine("function (app, " + (hasDataContext ? "dataContextFn, " : "") + "router, widget, managerAuth, modal, modal2, managerBrand, managerPredefined, logger, modalMultimidia, common" + (ui.HasCustomization ? ", customFn" : "") + (!complementaryCalls.IsNullOrEmpty() ? ", complementFn" : "") + ", modalCustomSearch) {");
            bool hasDetails = !uiEntityAdapter.IsNull() && uiEntityAdapter.ShowDetailsLoadProcess && uiEntityAdapter.GetAllInheritanceSourceEntityAdapters().Count > 0;
            bool hasBrand = ((!uiEntityAdapter.IsNull() && uiEntityAdapter.HasBrand(true)) || this.HasOlapServiceConnection(ui));
            bool hasWizard = !ui.EnableWizardTableView && ui.LayoutDefinition.GetLayoutElementsByClass("WizardControl").Count > 0;
            bool hasLargeDataMode = _designerRoot.EntityAdapters.Any(e => e.IsBufferSaving());

            codeBuilder.AddLine("var vms = [];");
            codeBuilder.AddLine("var pivots = [];");
            codeBuilder.AddLine("var vmInstance = function () {");
            codeBuilder.AddLine("    var activeRoute = document.URL;");
            codeBuilder.AddLine("    if (activeRoute.indexOf('?') >= 0)");
            codeBuilder.AddLine("        activeRoute = activeRoute.substring(0, activeRoute.indexOf('?'));");
            codeBuilder.AddLine("    if (vms[activeRoute])");
            codeBuilder.AddLine("        return vms[activeRoute];");
            codeBuilder.AddLine("    else {");
            codeBuilder.AddLine("        var vm = vmConstructor();");
            codeBuilder.AddLine("        vms[activeRoute] = vm;");
            codeBuilder.AddLine("        return vm;");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("}");

            codeBuilder.AddLine("var vmConstructor = function () {");
            codeBuilder.IncreaseIndent();

            codeBuilder.AddLine("var flattenObjectByProperty = function(obj, name) {");
            codeBuilder.AddLine("    var flat = {};");
            codeBuilder.AddLine("    function reduce(obj){");
            codeBuilder.AddLine("        flat[obj[name]] = $.extend({ }, obj);");
            codeBuilder.AddLine("        if (flat[obj[name]].Items) delete flat[obj[name]].Items;");
            codeBuilder.AddLine("        if (obj.Items) obj.Items.forEach(function(item) {");
            codeBuilder.AddLine("            return reduce(item);");
            codeBuilder.AddLine("        })");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    if (obj.Items) obj.Items.forEach(function(item) {");
            codeBuilder.AddLine("        reduce(item);");
            codeBuilder.AddLine("    });");
            codeBuilder.AddLine("    return flat;");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("");

            //codeBuilder.AddLine("var loadLanguage = function () {");
            //codeBuilder.AddLine("   var idioma = common.getIdioma();");
            //codeBuilder.AddLine("   if (idioma.indexOf('pt-br') >= 0)");
            //codeBuilder.AddLine("       vm.flattenLayout(ko.observable(flattenObjectByProperty(vm.layoutDesigner(), 'Name'))());");
            //codeBuilder.AddLine("   else {");
            //codeBuilder.AddLine("       try {");
            //codeBuilder.AddLine("           var scriptToRemove = vm.rootNamespace.toLowerCase().split(\".\")[0] + \"-spa-\" + vm.viewName.toLowerCase() + \"_\";");
            //codeBuilder.AddLine("           $('script[src*=\"' + scriptToRemove + '\"]').remove();");
            //codeBuilder.AddLine("");
            //codeBuilder.AddLine("           if (typeof languageFile == \"function\")");
            //codeBuilder.AddLine("               languageFile = new languageFile();");
            //codeBuilder.AddLine("");
            //codeBuilder.AddLine("           if (typeof objectLanguage_" + ui.Name + " == \"function\")");
            //codeBuilder.AddLine("               objectLanguage_" + ui.Name + " = new objectLanguage_" + ui.Name + "();");
            //codeBuilder.AddLine("");
            //codeBuilder.AddLine("           var nameProjectSPA = vm.rootNamespace.toLowerCase().split('.')[0] + \"-spa-\" + vm.viewName.toLowerCase() + \"_\" + idioma + \".js\";");
            //codeBuilder.AddLine("           var fName = managerAuth.pathLanguageResource + nameProjectSPA;");
            //codeBuilder.AddLine("");
            //codeBuilder.AddLine("           var fRef = document.createElement('script');");
            //codeBuilder.AddLine("           fRef.setAttribute(\"type\", \"text/javascript\");");
            //codeBuilder.AddLine("           fRef.setAttribute(\"src\", fName);");
            //codeBuilder.AddLine("           document.getElementsByTagName(\"head\")[0].appendChild(fRef);");
            //codeBuilder.AddLine();
            //codeBuilder.AddLine("           //changeLanguage();");
            //codeBuilder.AddLine("       } catch (e) {");
            //codeBuilder.AddLine("           console.log(\"Arquivo de tradução não encontrado[\" + idioma + \"].\");");
            //codeBuilder.AddLine("       }");
            //codeBuilder.AddLine("   }");
            //codeBuilder.AddLine("};");

            codeBuilder.AddLine("");
            codeBuilder.AddLine("var getLayoutColumnSpan = function(name) {");
            codeBuilder.AddLine("    return controlLayout.getColSpan(vm, name, typeof dialogIsOpen !== \"undefined\" ? dialogIsOpen : false);");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("");

            codeBuilder.AddLine("var getLayoutDisplayName = function(name) {");
            codeBuilder.AddLine("    return controlLayout.getDisplayName(vm, name, typeof dialogIsOpen !== \"undefined\" ? dialogIsOpen : false);");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("");

            codeBuilder.AddLine("var getLayoutVisible = function(name) {");
            codeBuilder.AddLine("    return controlLayout.getVisibility(vm, name, typeof dialogIsOpen !== \"undefined\" ? dialogIsOpen : false);");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("");

            codeBuilder.AddLine("var getDimensionUniqueName = function(name) {");
            codeBuilder.AddLine("    return controlLayout.getDimensionUniqueName(vm, name);");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("");

            codeBuilder.AddLine("var getLayoutHeaderGrid = function(name) {");
            codeBuilder.AddLine("    return controlLayout.getGridHeaderDisplayName(vm, name);");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("");

            codeBuilder.AddLine("var objectLayout = function () {");
            codeBuilder.AddLine("   return {Name: '" + ui.Name + "', Items: [");
            ui.LayoutDefinition.Containers.ForEach(container => codeBuilder.Add(GenerateLayoutForVM(container, ui)));
            codeBuilder.AddLine("   ]};");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("");

            codeBuilder.AddLine("var layoutDesignerOriginal = objectLayout;");
            codeBuilder.AddLine("");
            codeBuilder.AddLine("var layoutDesigner = ko.observable(objectLayout());");

            codeBuilder.AddLine("");
            codeBuilder.AddLine("var flattenLayout = ko.observable(flattenObjectByProperty(layoutDesigner(), 'Name'));");
            codeBuilder.AddLine("");

            codeBuilder.AddLine("var changeLanguage = function() {");
            codeBuilder.AddLine("    var idioma = common.getIdioma();");
            codeBuilder.AddLine("    if (idioma.indexOf('pt-br') >= 0)");
            codeBuilder.AddLine("        return vm.flattenLayout(ko.observable(flattenObjectByProperty(layoutDesigner(), 'Name'))());");
            codeBuilder.AddLine();
            codeBuilder.AddLine("    var nameProjectSPA = vm.rootNamespace.toLowerCase().split('.')[0] + \"-spa-\" + vm.viewName.toLowerCase() + \"_\" + idioma + \".js\";");
            codeBuilder.AddLine("    var fName = managerAuth.pathLanguageResource + nameProjectSPA;");
            codeBuilder.AddLine("    require([fName],");
            codeBuilder.AddLine("        function(result) {");
            codeBuilder.AddLine("        vm.flattenLayout(ko.observable(flattenObjectByProperty(result.objectLanguage_" + ui.Name + "(), 'Name'))());");
            codeBuilder.AddLine("    }, function (err) {");
            codeBuilder.AddLine("       console.log('Arquivo de tradução não encontrado!');");
            codeBuilder.AddLine("   });");
            codeBuilder.AddLine("};");

            //codeBuilder.AddLine("var timeout = 1000;");
            //codeBuilder.AddLine("var changeLanguage = function () {");
            //codeBuilder.AddLine("   var idioma = common.getIdioma();");
            //codeBuilder.AddLine("   if (idioma.indexOf('pt-br') >= 0)");
            //codeBuilder.AddLine("       return;");
            //codeBuilder.AddLine("");
            //codeBuilder.AddLine("   timeout--;");
            //codeBuilder.AddLine();
            //codeBuilder.AddLine("   if (typeof objectLanguage_" + ui.Name + " == \"function\" && Object.getOwnPropertyNames(objectLanguage_" + ui.Name + "()).length > 0) {");
            //codeBuilder.AddLine("       if (languageFile() == idioma)");
            //codeBuilder.AddLine("           vm.flattenLayout(ko.observable(flattenObjectByProperty(objectLanguage_" + ui.Name + "(), 'Name'))());");
            //codeBuilder.AddLine("       else if (timeout > 0)");
            //codeBuilder.AddLine("           changeLanguage();");
            //codeBuilder.AddLine("       else {");
            //codeBuilder.AddLine("           common.saveIdioma(\"pt-br\");");
            //codeBuilder.AddLine("           $(\"#cmbIdioma\").val(\"pt-br\");");
            //codeBuilder.AddLine("           vm.flattenLayout(ko.observable(flattenObjectByProperty(vm.layoutDesigner(), 'Name'))());");
            //codeBuilder.AddLine("           console.log(\"Erro ao carregar idioma[\" + idioma + \"]!\");");
            //codeBuilder.AddLine("       }");
            //codeBuilder.AddLine("   }");
            //codeBuilder.AddLine("   else if (timeout > 0)");
            //codeBuilder.AddLine("       changeLanguage();");
            //codeBuilder.AddLine("   else {");
            //codeBuilder.AddLine("       common.saveIdioma(\"pt-br\");");
            //codeBuilder.AddLine("       $(\"#cmbIdioma\").val(\"pt-br\");");
            //codeBuilder.AddLine("       vm.flattenLayout(ko.observable(flattenObjectByProperty(vm.layoutDesigner(), 'Name'))());");
            //codeBuilder.AddLine("       console.log(\"Erro ao carregar idioma[\" + idioma + \"]!\");");
            //codeBuilder.AddLine("   }");
            //codeBuilder.AddLine("   setTimeout(function () {");
            //codeBuilder.AddLine("       timeout--;");
            //codeBuilder.AddLine();
            //codeBuilder.AddLine("       if (typeof objectLanguage_" + ui.Name + " == \"function\" && Object.getOwnPropertyNames(objectLanguage_" + ui.Name + "()).length > 0) {");
            //codeBuilder.AddLine("           if (languageFile() == idioma)");
            //codeBuilder.AddLine("               vm.flattenLayout(ko.observable(flattenObjectByProperty(objectLanguage_" + ui.Name + "(), 'Name'))());");
            //codeBuilder.AddLine("           else if (timeout > 0)");
            //codeBuilder.AddLine("               changeLanguage();");
            //codeBuilder.AddLine("           else {");
            //codeBuilder.AddLine("               common.saveIdioma(\"pt-br\");");
            //codeBuilder.AddLine("               $(\"#cmbIdioma\").val(\"pt-br\");");
            //codeBuilder.AddLine("               vm.flattenLayout(ko.observable(flattenObjectByProperty(vm.layoutDesigner(), 'Name'))());");
            //codeBuilder.AddLine("               console.log(\"Erro ao carregar idioma[\" + idioma + \"]!\");");
            //codeBuilder.AddLine("           }");
            //codeBuilder.AddLine("       }");
            //codeBuilder.AddLine("       else if (timeout > 0)");
            //codeBuilder.AddLine("           changeLanguage();");
            //codeBuilder.AddLine("       else {");
            //codeBuilder.AddLine("           common.saveIdioma(\"pt-br\");");
            //codeBuilder.AddLine("           $(\"#cmbIdioma\").val(\"pt-br\");");
            //codeBuilder.AddLine("           vm.flattenLayout(ko.observable(flattenObjectByProperty(vm.layoutDesigner(), 'Name'))());");
            //codeBuilder.AddLine("           console.log(\"Erro ao carregar idioma[\" + idioma + \"]!\");");
            //codeBuilder.AddLine("       }");
            //codeBuilder.AddLine("   }, 100);");
            //codeBuilder.AddLine("};");

            if (!hasDataContext)
            {
                codeBuilder.AddLine();
                codeBuilder.IncreaseIndent();
                codeBuilder.AddLine("var isDependentVM = ko.observable(false);");
                codeBuilder.AddLine("var transactionNumberControl = ko.observable('00000000');");
                codeBuilder.AddLine("var hideToolbar = ko.observable(true);");
                if (!complementaryCalls.IsNullOrEmpty())
                    codeBuilder.AddLine("var complement = ((typeof complementFn === 'function') ? complementFn() : null);");
                if (ui.HasCustomization)
                    codeBuilder.AddLine("var custom = ((typeof customFn === 'function') ? customFn() : customFn);");

                generateNotifyInnerElements(ui, codeBuilder, false);
                generateIsBusyMethod(codeBuilder);

                #region Client Events
                this.AddClientEvents(ui, msEngine, codeBuilder);
                #endregion Client Events

                codeBuilder.AddLine();
                codeBuilder.AddLine("var bindingComplete = function () {");
                codeBuilder.AddLine("    return true;");
                codeBuilder.AddLine("};");
                codeBuilder.AddLine();
                codeBuilder.AddLine("var activate = function (settings, querystring) {");

                codeBuilder.AddLine("  if ((typeof settings === 'object') && (settings != null) && settings.objectQuery) {");
                codeBuilder.AddLine("      isDependentVM(false);");
                codeBuilder.AddLine("  }");
                codeBuilder.AddLine("  else {");
                codeBuilder.AddLine("      if ((typeof settings === 'object') && (settings != null) && settings.uiSettings) {");
                codeBuilder.AddLine("          isDependentVM(true);");
                codeBuilder.AddLine("      }");
                codeBuilder.AddLine("  }");

                codeBuilder.AddLine("  vm." + ui.Name + " = getVM;");
                codeBuilder.AddLine("};");
                codeBuilder.AddLine();
                codeBuilder.AddLine("var getVM = function () {");
                codeBuilder.AddLine("    return vm;");
                codeBuilder.AddLine("};");
                codeBuilder.AddLine();
                codeBuilder.AddLine("var compositionComplete = function () {");
                if (!complementaryCalls.IsNullOrEmpty())
                    codeBuilder.AddLine(complementaryCalls);

                codeBuilder.AddLine("    return true;");
                codeBuilder.AddLine("};");

                codeBuilder.AddLine();
                codeBuilder.AddLine("var vm = { ");
                codeBuilder.AddLine("    isDashboardFilter: " + (ui.EntityAdapter == null ? "false" : ui.EntityAdapter.IsDashboardFilter.ToString().ToLower()) + ",");
                codeBuilder.AddLine("    dataShared: [],");
                codeBuilder.AddLine("    viewName: '" + viewModel + "',");
                codeBuilder.AddLine("    hideToolbar: hideToolbar,");
                codeBuilder.AddLine("    isDependentVM: isDependentVM,");
                codeBuilder.AddLine("    bindingComplete: bindingComplete,");
                codeBuilder.AddLine("    activate: activate,");
                codeBuilder.AddLine("    compositionComplete: compositionComplete,");
                codeBuilder.AddLine("    notifyInnerElements: notifyInnerElements,");
                codeBuilder.AddLine("    transactionNumberControl: transactionNumberControl,");
                codeBuilder.AddLine("    dataToolbar: { title: function () { return ''; }, isBusy: isBusy, canCustomSearch: function () { return false; } },");
                codeBuilder.AddLine("    currentDataItem: function() { return null; },");
                codeBuilder.AddLine("    setBandeiraRede: function() { },");
                codeBuilder.AddLine("    status: ko.observable('N'),");
                codeBuilder.AddLine("    internalUIs: [],");
                codeBuilder.AddLine("    dataSource: [],");
                codeBuilder.AddLine("    dataBind: function(dataName, commitData) { },");
                codeBuilder.AddLine("    managerAuth: managerAuth,");
                codeBuilder.AddLine("    getLayoutColumnSpan: getLayoutColumnSpan,");
                codeBuilder.AddLine("    getLayoutVisible: getLayoutVisible,");
                foreach (var evt in ui.GetUserInterfaceClientEvented().Where(e => e.ExposedByViewModel))
                {
                    codeBuilder.AddLine("        " + evt.Name + ": " + evt.Name + ",");
                }
                codeBuilder.AddLine("    __moduleId__: '" + packageName + "/viewmodels/" + ui.Name + "'");
                codeBuilder.AddLine("};");
                codeBuilder.AddLine("return vm;");
                codeBuilder.DecreaseIndent();
                codeBuilder.AddLine("}");
                codeBuilder.AddLine();
                codeBuilder.AddLine("return vmInstance;");
                codeBuilder.DecreaseIndent();
                codeBuilder.AddLine("});");

                return;
            }

            #region Declarations
            codeBuilder.AddLine();
            codeBuilder.AddLine("var customSearch = function () { ");
            codeBuilder.AddLine("    modalCustomSearch.show(vm, dataContext);");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var layout = ko.observable();");
            codeBuilder.AddLine("var translatedJEntitySearch = '';");
            codeBuilder.AddLine("var customSearchResult = { searchDefinition: '', serializedSearch: '', translatedSearch: '' };");
            codeBuilder.AddLine("var hasCustomSearches = ko.observable(false);");
            codeBuilder.AddLine("var sortInfo = '';");
            codeBuilder.AddLine("var currentSettings = null;");
            codeBuilder.AddLine("var registeredUIs = [];");
            codeBuilder.AddLine("var dataContext = dataContextFn();");
            if (!complementaryCalls.IsNullOrEmpty())
                codeBuilder.AddLine("var complement = ((typeof complementFn === 'function') ? complementFn() : null);");
            if (ui.HasCustomization)
                codeBuilder.AddLine("var custom = ((typeof customFn === 'function') ? customFn() : customFn);");

            if (ui.QueryOnLoad)
                codeBuilder.AddLine("var queryLoaded = false;");
            codeBuilder.AddLine("var viewClosed = false;");
            codeBuilder.AddLine("var lastJEntitySearch = null;");
            codeBuilder.AddLine("var lastStatus = '';");
            codeBuilder.AddLine("var status = ko.observable('N');");
            codeBuilder.AddLine("var hideToolbar = ko.observable(" + ui.LayoutDefinition.RemoveDataToolbar.ToString().ToLower() + ");");
            codeBuilder.AddLine("var isDependentVM = ko.observable(false);");
            codeBuilder.AddLine("var transactionNumberControl = ko.observable('00000000');");
            codeBuilder.AddLine("var navigationByPage = ko.observable(false);");
            codeBuilder.AddLine("var viewType = ko.observable('Main');");
            codeBuilder.AddLine("var hasMainTopDataGrid = ko.observable(false);");
            codeBuilder.AddLine("var currentDataIndex = ko.observable(0);");
            codeBuilder.AddLine("var currentDataItem = ko.observable();");
            codeBuilder.AddLine("var currentActivityInformation = ko.observable('');");

            codeBuilder.AddLine("var currentPage = ko.observable(0);");
            codeBuilder.AddLine("var pageCount = ko.observable(0);");
            codeBuilder.AddLine("var pageSize = ko.observable(" + (ui.PageSize < 0 ? 0 : ui.PageSize).ToString() + ");");
            codeBuilder.AddLine("var totalItemCount = ko.observable(0);");

            codeBuilder.AddLine("var isSaving = ko.observable(false);");
            codeBuilder.AddLine("var " + entitiesRef + " = ko.observableArray([]);");
            codeBuilder.AddLine("var dataSource = [];");
            codeBuilder.AddLine("var brandDecimals = ko.observable(null)");
            codeBuilder.AddLine();
            codeBuilder.AddLine("var showDataFeedUrl = function() {");
            if (ui.ExistsClientEvent("OnToolbarAction"))
                codeBuilder.AddLine("    if (!OnToolbarAction('ShowFeed')) return;");
            codeBuilder.AddLine("    app.showMessage(dataContext.getDataFeedUrl(), 'Endereço do serviço', ['Ok']);");
            codeBuilder.AddLine("};");

            //LastSearchFilter

            codeBuilder.AddLine("var lastSearchFilter = function () {");
            if (ui.ExistsClientEvent("OnToolbarAction"))
                codeBuilder.AddLine("    if (!OnToolbarAction('ShowCurrentFilter')) return;");
            codeBuilder.AddLine("    var filterTranslation = getTranslatedFilter();");
            codeBuilder.AddLine("    app.showMessage((isNullOrEmpty(filterTranslation) ? 'Pesquisa sem filtros.' : filterTranslation), 'Filtros da pesquisa');");
            codeBuilder.AddLine("}");

            #endregion

            #region Register and Show
            codeBuilder.AddLine("var registerUI = function (name, viewPath, settings) {");
            codeBuilder.AddLine("    registeredUIs.push(name);");
            codeBuilder.AddLine("    registeredUIs[name] = {");
            codeBuilder.AddLine("        uiName: viewPath,");
            codeBuilder.AddLine("        uiSettings: settings");
            codeBuilder.AddLine("    };");
            codeBuilder.AddLine("}");
            codeBuilder.AddLine();
            codeBuilder.AddLine("var showRegisteredUI = function (name, elementName) {");
            codeBuilder.AddLine("    var ctrl = $('#' + elementName);");
            codeBuilder.AddLine("    var bindingContext = ko.contextFor(ctrl[0]);");
            codeBuilder.AddLine("    var uiSelected = registeredUIs[name];");
            codeBuilder.AddLine("    if (uiSelected.length == 0){");
            codeBuilder.AddLine("        console.warn('Não foi encontrado o elemento [' + elementName + ']');");
            codeBuilder.AddLine("        return;");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    var settings = {");
            codeBuilder.AddLine("        kind: uiSelected.uiName,");
            codeBuilder.AddLine("        parentVM: vm,");
            codeBuilder.AddLine("        uiSettings: uiSelected.uiSettings");
            codeBuilder.AddLine("    };");
            codeBuilder.AddLine("    var ext;");
            codeBuilder.AddLine("    ctrlName = elementName + \"_\" + name;");
            codeBuilder.AddLine("    if ($('#' + ctrlName).length == 0)");
            codeBuilder.AddLine("        ext = ctrl.append(\"<div id='\" + elementName + \"_\" + name + \"' />\");");
            codeBuilder.AddLine("    else");
            codeBuilder.AddLine("        ext = $('#' + ctrlName);");
            codeBuilder.AddLine("    widget.create(ext[0], settings, bindingContext, true);");
            codeBuilder.AddLine("};");
            #endregion

            #region currentRecord
            codeBuilder.AddLine("var currentRecord = ko.computed(function () {");
            codeBuilder.AddLine("    if (pageSize() === 0) return currentDataIndex();");
            codeBuilder.AddLine("    else return (currentPage() * pageSize()) + currentDataIndex();");
            codeBuilder.AddLine("});");
            #endregion
            #region isBusy function

            generateIsBusyMethod(codeBuilder);

            #endregion
            #region totalRecords
            codeBuilder.AddLine("var totalRecords = ko.computed(function () {");
            codeBuilder.AddLine("    if (pageSize() === 0) return " + entitiesRef + "().length;");
            codeBuilder.AddLine();
            codeBuilder.AddLine("    var recordCount = 0;");
            codeBuilder.AddLine("    if (currentPage() === 0) {");
            codeBuilder.AddLine("        if (pageCount() <= 1) {");
            codeBuilder.AddLine("             recordCount = " + entitiesRef + "().length;");
            codeBuilder.AddLine("        } else {");
            codeBuilder.AddLine("             recordCount = totalItemCount() - pageSize() +  " + entitiesRef + "().length;");
            codeBuilder.AddLine("        }");
            codeBuilder.AddLine("    } else if (currentPage() === (pageCount() - 1)) {");
            codeBuilder.AddLine("        recordCount = (pageSize() * (pageCount() - 1)) + " + entitiesRef + "().length;");
            codeBuilder.AddLine("    } else {");
            codeBuilder.AddLine("        recordCount = pageSize() * (currentPage() + 1);");
            codeBuilder.AddLine("        recordCount += totalItemCount() - (pageSize() * (currentPage() + 2));");
            codeBuilder.AddLine("        recordCount += " + entitiesRef + "().length;");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    return recordCount;");
            codeBuilder.AddLine("});");
            #endregion
            #region currentFormattedRecord
            codeBuilder.AddLine("var currentFormattedRecord = ko.computed(function () {");
            //codeBuilder.AddLine("    var totalR = totalRecords();");
            codeBuilder.AddLine("    if (totalRecords() === 0) return '0';");
            //codeBuilder.AddLine("    else if (navigationByPage()) return ((currentPage()*pageSize()) + 1).toString() + '-' + (currentPage() === (pageCount() - 1) ? totalR : pageSize()*(currentPage()+1)).toString();");
            codeBuilder.AddLine("    else return (currentRecord()+1).toString();");
            codeBuilder.AddLine("});");
            #endregion
            #region currentRecordInfo
            codeBuilder.AddLine("var currentRecordInfo = ko.computed(function () { var totalR = totalRecords(); if (totalR === 0) { return '0/0'; } else { return currentFormattedRecord() + '/' + totalR.toString(); } });");
            #endregion
            #region contextDataUpdateHandler
            codeBuilder.AddLine("var contextDataUpdateHandler = function (e) {");
            codeBuilder.AddLine("    dataBind(dataContext.dataForUpdate);");
            codeBuilder.AddLine("};");
            #endregion
            #region durandal events
            codeBuilder.AddLine("//#region Durandal Events");
            codeBuilder.AddLine();

            codeBuilder.AddLine("var started = false;");
            codeBuilder.AddLine("var parentVM = null;");
            codeBuilder.AddLine("var uiSettings = null;");
            codeBuilder.AddLine("var filteredEntities = [];");


            var quickSearchProperties = ui.EntityAdapter.GetAllInheritanceProperties().Where(e => e.QuickSearchIndex >= 0).OrderBy(e => e.QuickSearchIndex).ToArray();
            bool hasQuickSearch = quickSearchProperties.Where(e => e.Datatype.ToLower().Contains("string")).Count() > 0;
            codeBuilder.AddLine("//#region quick search");
            if (hasQuickSearch)
            {
                codeBuilder.AddLine("var quickSearchTerm = '';");
                codeBuilder.AddLine("function repoFormatResult(repo, element, ops, escapeMarkup) {");

                codeBuilder.AddLine("    //Adjust last quick qearch term");
                codeBuilder.AddLine("    quickSearchTerm = ops.term;");
                codeBuilder.AddLine();
                codeBuilder.AddLine("    var markup = '<div class=\"clearfix\">' +");
                codeBuilder.AddLine("     '<div clas=\"col-sm-10\">' +");
                codeBuilder.AddLine("     '<div class=\"clearfix\">';");
                codeBuilder.AddLine();
                codeBuilder.AddLine("    //Header");
                codeBuilder.AddLine("    if (repo.id === \"-1\") {");
                foreach (var prop in quickSearchProperties)
                {
                    codeBuilder.AddLine("         markup += '<div class=\"col-sm-" + (quickSearchProperties.Length > 12 ? "1" : ((int)(12 / quickSearchProperties.Length)).ToString()) + "\"><label class=\"control-label\" style=\"font-weight: bold\">" + prop.DisplayName + "</label></div>';");
                }
                codeBuilder.AddLine("    }");
                codeBuilder.AddLine("    else {");
                codeBuilder.AddLine("    //Data");
                foreach (var prop in quickSearchProperties)
                {
                    codeBuilder.AddLine("    markup += '<div class=\"col-sm-" + (quickSearchProperties.Length > 12 ? "1" : ((int)(12 / quickSearchProperties.Length)).ToString()) + "\"><label class=\"control-label\">' + " + (prop.Datatype.ToLower().Contains("datetime") ? "Globalize.format(getUTCDate(new Date(repo." + prop.Name + ")), '" + prop.DataFormatString + "')" : "repo." + prop.Name) + " + '</label></div>';");
                }
                codeBuilder.AddLine("    }");
                codeBuilder.AddLine();
                codeBuilder.AddLine("    markup += '</div></div></div>';");
                codeBuilder.AddLine();
                codeBuilder.AddLine("    return markup;");
                codeBuilder.AddLine("}");
                codeBuilder.AddLine();
                codeBuilder.AddLine("function repoFormatSelection(repo) {");
                codeBuilder.AddLine("    //Clear before query");
                codeBuilder.AddLine("    filteredEntities = [];");
                codeBuilder.AddLine("    clear();");
                codeBuilder.AddLine();

                codeBuilder.AddLine("    var quickSearchJExpression = '" + uiEntityAdapter.Name + "{';");
                codeBuilder.AddLine("    if (repo.id === \"-1\") //Select all");
                codeBuilder.AddLine("    {");
                string separator = "";
                codeBuilder.AddLine("       quickSearchJExpression += '(;'");
                foreach (var prop in quickSearchProperties)
                {
                    if (prop.Datatype.ToLower().Contains("string"))
                    {
                        codeBuilder.AddLine("       quickSearchJExpression += '" + separator + prop.Name + "#Like#" + Linx.Tools.EntitySearch.ParseJDataType(prop.Datatype) + "' + '%' + encode(quickSearchTerm) + '%';");
                        separator = ";||#";
                    }
                }
                codeBuilder.AddLine("       quickSearchJExpression += ';)'");
                codeBuilder.AddLine("    }");
                codeBuilder.AddLine("    else { //Select only one element");
                separator = "";
                foreach (var prop in quickSearchProperties)
                {
                    if (prop.Datatype.ToLower().Contains("datetime"))
                    {
                        codeBuilder.AddLine("       if (!isNullOrEmpty(repo." + prop.Name + ")) {");
                        codeBuilder.AddLine("           var value = new Date(repo." + prop.Name + ");");
                        codeBuilder.AddLine("           quickSearchJExpression += '" + separator + prop.Name + "#>=#" + Linx.Tools.EntitySearch.ParseJDataType(prop.Datatype) + "' + value.getUTCFullYear().toString() + '-' + (value.getUTCMonth() + 1).toString() + '-' + value.getUTCDate().toString() + ' 00:00:00.000;" + prop.Name + "#<=#" + Linx.Tools.EntitySearch.ParseJDataType(prop.Datatype) + "' + value.getUTCFullYear().toString() + '-' + (value.getUTCMonth() + 1).toString() + '-' + value.getUTCDate().toString() + ' 23:59:59.999';");
                        codeBuilder.AddLine("       }");
                        separator = ";";
                    }
                    else
                    {
                        codeBuilder.AddLine("       if (!isNullOrEmpty(repo." + prop.Name + ")) {");
                        codeBuilder.AddLine("           quickSearchJExpression += '" + separator + prop.Name + "#==#" + Linx.Tools.EntitySearch.ParseJDataType(prop.Datatype) + "' + encode(repo." + prop.Name + ".toString())" + (prop.Datatype.ToLower().Contains("bool") ? ".toLowerCase()" : "") + ";");
                        codeBuilder.AddLine("       }");
                        separator = ";";
                    }
                }
                codeBuilder.AddLine("    }");
                codeBuilder.AddLine("    quickSearchJExpression += '}';");

                codeBuilder.AddLine("    _canQuickSearch = false;");
                codeBuilder.AddLine("    refreshToolbar();");
                codeBuilder.AddLine("    _canQuickSearch = true;");
                codeBuilder.AddLine("    dataToolbar.query(false, null, quickSearchJExpression);");

                string qsFormatSelect = String.Join(" | ", quickSearchProperties.Select(e => (e.Datatype.ToLower().Contains("datetime") ? "Globalize.format(getUTCDate(new Date(repo." + e.Name + ")), '" + e.DataFormatString + "')" : "repo." + e.Name)));
                codeBuilder.AddLine("    return " + qsFormatSelect + ";");
                codeBuilder.AddLine("}");
                codeBuilder.AddLine();

            }

            codeBuilder.AddLine("var quickSearch = function () {");
            if (hasQuickSearch)
            {
                codeBuilder.AddLine("    createQuickSearch(dataContext.getDataServiceUrl() + \"/Get" + ui.EntityAdapter.Name + "QuickSearch?$inlinecount=allpages\",");
                codeBuilder.AddLine("    repoFormatResult, repoFormatSelection);");
            }
            codeBuilder.AddLine("}");

            codeBuilder.AddLine("//#endregion ");

            codeBuilder.AddLine("var activate = function (settings, querystring) {");
            codeBuilder.AddLine("  if (typeof common.getTransactionCode === 'function') transactionNumberControl(common.getTransactionCode());");
            if (ui.ExistsClientEvent("OnToolbarAction"))
                codeBuilder.AddLine("    OnToolbarAction('Open');");
            codeBuilder.AddLine("  vm." + ui.Name + " = getVM;");
            codeBuilder.AddLine("  //loadLanguage();");
            codeBuilder.AddLine("  changeLanguage();");
            codeBuilder.AddLine("  if ((typeof settings === 'object') && (settings != null)) {");
            codeBuilder.AddLine("      currentSettings = settings;");
            codeBuilder.AddLine("  }");
            codeBuilder.AddLine("  if ((typeof settings === 'object') && (settings != null) && settings.objectQuery) {");
            codeBuilder.AddLine("      isDependentVM(false);");
            codeBuilder.AddLine("      parentVM = null;");
            codeBuilder.AddLine("      filteredEntities = [];");
            codeBuilder.AddLine("      clear();");
            codeBuilder.AddLine("      var fieldProperty, value;");
            codeBuilder.AddLine("      if (!isNullOrEmpty(settings.objectQuery)) {");
            codeBuilder.AddLine("          $.each(settings.objectQuery.split(';'), function (idxElement, element) {");
            codeBuilder.AddLine("              var idx = element.indexOf(':');");
            codeBuilder.AddLine("              if (idx >= 0) {");
            codeBuilder.AddLine("                  field = element.slice(0, idx).trim();");
            codeBuilder.AddLine("                  value = element.slice(idx + 1, element.length);");
            codeBuilder.AddLine("                  setAbsoluteValue(currentDataItem(), field, value);");
            codeBuilder.AddLine("              }");
            codeBuilder.AddLine("          });");
            codeBuilder.AddLine("      }");
            codeBuilder.AddLine("      if (settings.executeQuery == 'true')");
            codeBuilder.AddLine("          query(true);");
            codeBuilder.AddLine("      if (window.location.hash)");
            codeBuilder.AddLine("          history.replaceState(undefined, undefined, window.location.hash.substring(0, window.location.hash.indexOf('?')))");
            codeBuilder.AddLine("  }");
            codeBuilder.AddLine("  else {");
            codeBuilder.AddLine("      if ((typeof settings === 'object') && (settings != null) && settings.uiSettings) {");
            codeBuilder.AddLine("          uiSettings = settings.uiSettings;");
            codeBuilder.AddLine("          isDependentVM(true);");
            codeBuilder.AddLine("          parentVM = null;");
            codeBuilder.AddLine("          if (uiSettings.executeQuery === true) {");
            codeBuilder.AddLine("              if (uiSettings.toolbarSettings) {");
            codeBuilder.AddLine("                  setSecurity(uiSettings.toolbarSettings.canAddNew, uiSettings.toolbarSettings.canClear, uiSettings.toolbarSettings.canCustomSearch, uiSettings.toolbarSettings.canDelete, uiSettings.toolbarSettings.canEdit, uiSettings.toolbarSettings.canLayout, uiSettings.toolbarSettings.canNavigate, uiSettings.toolbarSettings.canPrint, uiSettings.toolbarSettings.canSearch, uiSettings.toolbarSettings.canExport, uiSettings.toolbarSettings.noBusyLoading);");
            codeBuilder.AddLine("                  hideToolbar(uiSettings.toolbarSettings.removeDataToolbar);");
            codeBuilder.AddLine("              }");
            codeBuilder.AddLine("              filteredEntities = [];");
            codeBuilder.AddLine("              if (settings.parentVM) { settings.parentVM.internalUIs = [ '" + ui.Name + "' ]; settings.parentVM." + ui.Name + " = getVM; }");
            codeBuilder.AddLine("              clear();");
            codeBuilder.AddLine("              if ((typeof uiSettings.querySetters === 'object')) {");
            codeBuilder.AddLine("                  for (var field in uiSettings.querySetters) {");
            codeBuilder.AddLine("                       if (field.indexOf('entitySearchRange') >= 0){");
            codeBuilder.AddLine("                          setAbsoluteValue(vm.entitySearchRange, field.split('.')[1], uiSettings.querySetters[field]);");
            codeBuilder.AddLine("                       }");
            codeBuilder.AddLine("                       else {");
            codeBuilder.AddLine("                           setAbsoluteValue(currentDataItem(), field, uiSettings.querySetters[field]);");
            codeBuilder.AddLine("                       }");
            codeBuilder.AddLine("                  }");
            codeBuilder.AddLine("              }");
            codeBuilder.AddLine("              query(true);");
            codeBuilder.AddLine("          }");
            codeBuilder.AddLine("          else {");
            codeBuilder.AddLine("                   if (uiSettings.toolbarSettings) {");
            codeBuilder.AddLine("                       setSecurity(uiSettings.toolbarSettings.canAddNew, uiSettings.toolbarSettings.canClear, uiSettings.toolbarSettings.canCustomSearch, uiSettings.toolbarSettings.canDelete, uiSettings.toolbarSettings.canEdit, uiSettings.toolbarSettings.canLayout, uiSettings.toolbarSettings.canNavigate, uiSettings.toolbarSettings.canPrint, uiSettings.toolbarSettings.canSearch, uiSettings.toolbarSettings.canExport, uiSettings.toolbarSettings.noBusyLoading);");
            codeBuilder.AddLine("                       hideToolbar(uiSettings.toolbarSettings.removeDataToolbar);");
            codeBuilder.AddLine("                   }");
            codeBuilder.AddLine("                   else {");
            codeBuilder.AddLine("                       setSecurity(uiSettings.canAddNew, uiSettings.canClear, uiSettings.canCustomSearch, uiSettings.canDelete, uiSettings.canEdit, uiSettings.canLayout, uiSettings.canNavigate, uiSettings.canPrint, uiSettings.canSearch, uiSettings.canExport, uiSettings.noBusyLoading);");
            codeBuilder.AddLine("                       hideToolbar(uiSettings.removeDataToolbar);");
            codeBuilder.AddLine("                   }");
            codeBuilder.AddLine("              if ((typeof settings.parentVM === 'object') && settings.parentVM != null) {");
            codeBuilder.AddLine("                  parentVM = settings.parentVM;");
            codeBuilder.AddLine("                  parentVM." + ui.Name + " = getVM;");
            codeBuilder.AddLine("                  if (isLookup()) { ");
            codeBuilder.AddLine("                      parentVM.internalUIs = [];");
            codeBuilder.AddLine("                      filteredEntities = [];");
            codeBuilder.AddLine("                      clear();");
            codeBuilder.AddLine("                      if (!isNullOrEmpty(uiSettings.valueToSearch)) {");
            codeBuilder.AddLine("                          if (typeof currentDataItem()[uiSettings.fieldToSearch] === 'function') {");
            codeBuilder.AddLine("                              currentDataItem()[uiSettings.fieldToSearch](uiSettings.valueToSearch);");
            codeBuilder.AddLine("                              query(true);");
            codeBuilder.AddLine("                          }");
            codeBuilder.AddLine("                      }");
            codeBuilder.AddLine("                  }");
            codeBuilder.AddLine("                  if ($.inArray('" + ui.Name + "', parentVM.internalUIs) === -1){");
            codeBuilder.AddLine("                       if (parentVM.internalUIs) {");
            codeBuilder.AddLine("                           parentVM.internalUIs.push('" + ui.Name + "');");
            codeBuilder.AddLine("                       }");
            codeBuilder.AddLine("                       else {");
            codeBuilder.AddLine("                           parentVM.internalUIs = ['" + ui.Name + "'];");
            codeBuilder.AddLine("                       }");
            codeBuilder.AddLine("                  }");
            codeBuilder.AddLine("              }");
            codeBuilder.AddLine("          }");
            codeBuilder.AddLine("      }");
            codeBuilder.AddLine("      else {");
            codeBuilder.AddLine("          app.on('shell:close:all').then(function () {");
            codeBuilder.AddLine("              viewClosed = true;");
            codeBuilder.AddLine("              filteredEntities = [];");
            codeBuilder.AddLine("              clear();");
            codeBuilder.AddLine("          });");
            codeBuilder.AddLine("          if (viewClosed == true){");
            codeBuilder.AddLine("              viewClosed = false;");
            codeBuilder.AddLine("              loadDataView();");
            codeBuilder.AddLine("          }");
            codeBuilder.AddLine("          adjustModuleSecurity();");
            codeBuilder.AddLine("      }");
            codeBuilder.AddLine("  }");
            codeBuilder.AddLine("  if (isChildVM() && (!_canNavigate || hideToolbar() || _canAddNew || _canDelete || _canEdit))");
            codeBuilder.AddLine("       pageSize(0);");
            codeBuilder.AddLine("  document.addEventListener(dataContext.contextUpdtEvt, contextDataUpdateHandler, false);");
            codeBuilder.AddLine("  if (!started) { started = true; clear(); } else { viewType('Main'); refreshToolbar(); }");
            codeBuilder.AddLine("  //Call OnLoadedChildUI Event");
            codeBuilder.AddLine("  if (isChildVM() && !isLookup()) {");
            codeBuilder.AddLine("    if (typeof parentVM.OnLoadedChildUI === 'function')");
            codeBuilder.AddLine("        parentVM.OnLoadedChildUI(vm);");
            codeBuilder.AddLine("  }");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine();
            codeBuilder.AddLine("var adjustModuleSecurity = function () {");
            codeBuilder.AddLine("    parentVM = null;");
            codeBuilder.AddLine("    uiSettings = null;");
            codeBuilder.AddLine("    isDependentVM(false);");
            codeBuilder.AddLine("    setSecurity(" + ui.LayoutDefinition.CanAddNew.ToString().ToLower() + ", " + ui.LayoutDefinition.CanClear.ToString().ToLower() + ", " + ui.LayoutDefinition.CanCustomSearch.ToString().ToLower() + ", " + ui.LayoutDefinition.CanDelete.ToString().ToLower() + ", " + ui.LayoutDefinition.CanEdit.ToString().ToLower() + ", " + ui.LayoutDefinition.CanLayout.ToString().ToLower() + ", " + ui.LayoutDefinition.CanNavigate.ToString().ToLower() + ", " + ui.LayoutDefinition.CanPrint.ToString().ToLower() + ", " + ui.LayoutDefinition.CanSearch.ToString().ToLower() + ", " + ui.LayoutDefinition.CanExport.ToString().ToLower() + ");");
            codeBuilder.AddLine("    managerAuth.getFormAccess('" + nameSpacePkg + "-" + ui.Name + "', function (data) {");
            codeBuilder.AddLine("       if (data && !data.AcessoTotal) {");
            codeBuilder.AddLine("          setSecurity(" + (ui.LayoutDefinition.CanAddNew ? "data.Incluir" : "false") + ", " +
                                                            (ui.LayoutDefinition.CanClear ? "true" : "false") + ", " +
                                                            (ui.LayoutDefinition.CanCustomSearch ? "data.PesquisaEspecial" : "false") + ", " +
                                                            (ui.LayoutDefinition.CanDelete ? "data.Excluir" : "false") + ", " +
                                                            (ui.LayoutDefinition.CanEdit ? "data.Alterar" : "false") + ", " +
                                                            (ui.LayoutDefinition.CanLayout ? "data.Layout" : "false") + ", " +
                                                            ui.LayoutDefinition.CanNavigate.ToString().ToLower() + ", " +
                                                            (ui.LayoutDefinition.CanPrint ? "data.Imprimir" : "false") + ", " +
                                                            (ui.LayoutDefinition.CanSearch ? "data.Pesquisar" : "false") + ", " +
                                                            (ui.LayoutDefinition.CanExport ? "data.Exportar" : "false") + ");");

            codeBuilder.AddLine("       }");
            codeBuilder.AddLine("    }, logger);");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine();
            codeBuilder.AddLine("var getVM = function () {");
            codeBuilder.AddLine("    return vm;");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine();
            codeBuilder.AddLine("var binding = function () {");
            codeBuilder.AddLine("    if (!isChildVM()) vm.showProcessing('Inicializando...');");
            codeBuilder.AddLine("    return { cacheViews: false };");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine();
            codeBuilder.AddLine("var bindingComplete = function () {");
            codeBuilder.AddLine("    return true;");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var attached = function(view, parent) {");
            if (ui.ExistsClientEvent("OnLoading"))
                codeBuilder.AddLine("    OnLoading();");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var canDeactivate = function () {");
            codeBuilder.AddLine("    if (require('plugins/dialog').isOpen())");
            codeBuilder.AddLine("        return false;");

            codeBuilder.AddLine("    try {");
            codeBuilder.AddLine("        var dlg =  $('.toolbar-dialog-template:visible')[0].id;");
            codeBuilder.AddLine("        if ($('#' + dlg).dialog('isOpen'))");
            codeBuilder.AddLine("            return false;");
            codeBuilder.AddLine("    } catch (e) {}");

            if (ui.ExistsClientEvent("OnClosing"))
                codeBuilder.AddLine("    if (!OnClosing()) return false;");
            codeBuilder.AddLine("    if (status() === 'E') {");
            codeBuilder.AddLine("        return app.showMessage('Deseja realmente sair e cancelar o trabalho corrente?', 'Alerta', ['Yes', 'No'])");
            codeBuilder.AddLine("            .then(function (selectedOption) {");
            codeBuilder.AddLine("                if (selectedOption === 'Yes') {");
            codeBuilder.AddLine("                   undo();");
            codeBuilder.AddLine("               }");
            codeBuilder.AddLine("               return selectedOption;");
            codeBuilder.AddLine("          });");
            codeBuilder.AddLine("  }");
            codeBuilder.AddLine("  return true;");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var canActivate = function() {");
            codeBuilder.AddLine("    var data = router.activeInstruction().config;");
            codeBuilder.AddLine("    if (data.lxShellCompiledVersion != managerAuth.shellVersion) {");
            codeBuilder.AddLine("        app.showMessage('Versão de formulário incompatível com a versão de ambiente [' + managerAuth.shellVersion + '].', 'Formulário: " + ui.Name + "', ['Ok']);");
            codeBuilder.AddLine("        return false;");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    return true;");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var deactivate = function() {");
            codeBuilder.AddLine("   document.removeEventListener(dataContext.contextUpdtEvt, contextDataUpdateHandler, false);");
            codeBuilder.AddLine("};");

            #region CompositionComplete
            codeBuilder.AddLine("var compositionComplete = function() {");
            codeBuilder.AddLine("    //changeLanguage();");
            if (!complementaryCalls.IsNullOrEmpty())
                codeBuilder.AddLine(complementaryCalls);



            if (!hasWizard) codeBuilder.AddLine("    if (!hasMainTopDataGrid() && isChildVM()) removeFormViewControl();");

            codeBuilder.AddLine("    navigationByPage(hasMainTopDataGrid());");
            codeBuilder.AddLine("    dataBind();");
            codeBuilder.AddLine("    if (!isChildVM()) { vm.closeProcessing(); }");
            codeBuilder.AddLine("    try{ $(window).trigger('resize'); } catch(e){ console.log(e); }");
            codeBuilder.AddLine("    //Form startup routine");
            codeBuilder.AddLine("    if (currentSettings != null)");
            codeBuilder.AddLine("    {");
            codeBuilder.AddLine("        if (!isNullOrEmpty(currentSettings.action))");
            codeBuilder.AddLine("        {");
            codeBuilder.AddLine("            if (currentSettings.action.toLowerCase() == 'new')");
            codeBuilder.AddLine("            {");
            codeBuilder.AddLine("                if (dataToolbar.canAddNew())");
            codeBuilder.AddLine("                {");
            codeBuilder.AddLine("                    dataToolbar.addNew();");
            codeBuilder.AddLine("                }");
            codeBuilder.AddLine("            }");
            codeBuilder.AddLine("        }");
            codeBuilder.AddLine("    }");
            if (ui.HasCustomization)
                codeBuilder.AddLine("    custom.afterViewInitializing({ viewModel: vm });");
            if (ui.ExistsClientEvent("OnLoaded"))
                codeBuilder.AddLine("    OnLoaded();");
            codeBuilder.AddLine("    scrollMainTop();");

            #region Call Methods ComplementEvents
            codeBuilder.AddLine("    vm.currentBrands.subscribe(function(newValue) {");
            codeBuilder.AddLine("        newValue = isNull(newValue) ? vm.currentBrands() : newValue;");
            codeBuilder.AddLine("        var searchedBrands = managerBrand.searchBrandsVM(newValue, managerAuth.getIdTcsAmbiente());");
            codeBuilder.AddLine("        var reset = (!newValue || searchedBrands.cod === ''), decimals = searchedBrands.decimals;");
            codeBuilder.AddLine(eventChangedBrand);
            codeBuilder.AddLine("        vm.brandDecimals(reset || isNull(decimals) ? null : decimals);");
            codeBuilder.AddLine("        vm.currentDataItem.notifySubscribers();");
            codeBuilder.AddLine("    });");
            codeBuilder.AddLine("    vm.currentBrands.notifySubscribers();");
            #endregion

            codeBuilder.AddLine("    getLayoutFormPadrao(vm);");

            codeBuilder.AddLine("    " + (ui.QueryOnLoad && !hasBrand ? "if (!queryLoaded) { loadDataView(); } else " : "") + "return true;");
            codeBuilder.AddLine("};");
            #endregion

            codeBuilder.AddLine("var detached = function (view) {");
            if (ui.ExistsClientEvent("OnToolbarAction"))
                codeBuilder.AddLine("    OnToolbarAction('Close');");
            //Task 108824 - TraceGP
            //Retirado o código devido ao erro no filtro de datas. Não encontrei o porque dessa alteração.
            //Inclusão de chamada do método diretamente do LIA para não ser necessário salvar os EADs novamente no caso de alguma alteração futura.
            //codeBuilder.AddLine("   if (viewClosed == true)");
            //codeBuilder.AddLine("   {");
            //codeBuilder.AddLine("      $(view).empty();");
            //codeBuilder.AddLine("      $(view).remove();");
            //codeBuilder.AddLine("      view = null;");
            //codeBuilder.AddLine("   }");
            codeBuilder.AddLine("   viewDetached(view, viewClosed);");

            codeBuilder.AddLine("};");


            codeBuilder.AddLine("//#endregion");
            #endregion

            codeBuilder.AddLine("var getDecimalsByData = function getDecimalsByData(data, defaultValue) {");
            codeBuilder.AddLine("    var decimals = vm.brandDecimals();");
            codeBuilder.AddLine("    if (!isNull(data)) {");
            codeBuilder.AddLine("        if (data['IdBandeiraRede'] && getAbsoluteValue(data['IdBandeiraRede']) > 0) {");
            codeBuilder.AddLine("            var searchedBrands = managerBrand.searchBrandsVM(getAbsoluteValue(data['IdBandeiraRede']), managerAuth.getIdTcsAmbiente());");
            codeBuilder.AddLine("            decimals = searchedBrands.decimals;");
            codeBuilder.AddLine("        }");
            codeBuilder.AddLine("        if (data['NumeroDecimais'] && getAbsoluteValue(data['NumeroDecimais']) > 0)");
            codeBuilder.AddLine("            decimals = getAbsoluteValue(data['NumeroDecimais']);");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    return isNullOrEmpty(decimals) ? defaultValue : decimals;");
            codeBuilder.AddLine("};");

            #region getMaxLength
            codeBuilder.AddLine("var getMaxLength = function(entityName, propertyName){");
            codeBuilder.AddLine("    if (isNullOrEmpty(entityName)) entityName = '" + masterEntityName + "';");
            codeBuilder.AddLine("    var property = dataContext.getEntityProperty(entityName, propertyName);");
            codeBuilder.AddLine("    if(property != null)");
            codeBuilder.AddLine("        return property.maxLength;");
            codeBuilder.AddLine("    else");
            codeBuilder.AddLine("        return 0;");
            codeBuilder.AddLine("};");
            #endregion

            #region dataBind
            codeBuilder.AddLine("var isDataSourceHided = function (dataName) {");
            codeBuilder.AddLine("    var url = (document.URL.contains('?') ? document.URL.substring(0, document.URL.indexOf('?')) : document.URL);");
            codeBuilder.AddLine("    if (vm.dataSource.length > 0 && vms[url] === vm) {");
            codeBuilder.AddLine("       for (var db in vm.dataSource) { if (vm.dataSource[db].name === dataName && (typeof vm.dataSource[db].itemsSource.isElementHided === 'function')) { return vm.dataSource[db].itemsSource.isElementHided(); } }");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    return false;");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var dataBind = function (dataName, commitData) {");
            codeBuilder.AddLine("    var url = (document.URL.contains('?') ? document.URL.substring(0, document.URL.indexOf('?')) : document.URL);");
            codeBuilder.AddLine("    if (vm.dataSource.length > 0 && vms[url] === vm) {");
            codeBuilder.AddLine("       for (var db in vm.dataSource) { if (!dataName || dataName === '' || vm.dataSource[db].name === dataName) { vm.dataSource[db].itemsSource.dataBind(commitData); } }");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("};");
            #endregion
            #region addDataSource

            codeBuilder.AddLine("var getVisibleProperties = function (dataName) {");
            codeBuilder.AddLine("    if (vm.dataSource.length > 0) {");
            codeBuilder.AddLine("        for (var db in vm.dataSource) { if (vm.dataSource[db].name === dataName && (typeof vm.dataSource[db].itemsSource.getVisibleColumns === 'function')) { return 'LinqValidProperties{LinqValidProperties#==#S' + vm.dataSource[db].itemsSource.getVisibleColumns(true) + '}'; } }");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    return '';");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine();
            codeBuilder.AddLine("var visibleColumns = '" + string.Join(",", ui.LayoutDefinition.GetItemByPredicate<LayoutControlV2>(c => c.BindingPath.Occurs(".") == 2).Where(c => c.IsVisible).Select(c => c.BindingPath.Right("DataElement.DataView.")).Distinct().ToArray()) + "';");
            codeBuilder.AddLine();
            codeBuilder.AddLine("var getVisiblePropertiesForExcel = function (dataName) {");
            codeBuilder.AddLine("    if (vm.dataSource.length > 0) {");
            codeBuilder.AddLine("        for (var db in vm.dataSource) {");
            codeBuilder.AddLine("            if (vm.dataSource[db].name === dataName && (typeof vm.dataSource[db].itemsSource.getVisibleColumns === 'function')) {");
            codeBuilder.AddLine("               if (vm.dataSource[db].itemsSource.getVisibleColumns() === \"\") return visibleColumns;");
            codeBuilder.AddLine("               return vm.dataSource[db].itemsSource.getVisibleColumns();");
            codeBuilder.AddLine("            }");
            codeBuilder.AddLine("        }");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    return dataName === 'dataView' ? visibleColumns : '';");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine();
            codeBuilder.AddLine("var addDataSource = function (dsElement) {");
            codeBuilder.AddLine("    if (!dsElement.key) return;");
            codeBuilder.AddLine("    var foundElement = null;");
            codeBuilder.AddLine("    for (var ds in vm.dataSource) { if (vm.dataSource[ds].key === dsElement.key) { foundElement = vm.dataSource[ds]; break; } }");
            codeBuilder.AddLine("    if (foundElement === null) { vm.dataSource.push(dsElement); } else { foundElement.itemsSource = dsElement.itemsSource; }");
            codeBuilder.AddLine("};");
            #endregion

            #region KPIs
            bool hasKPI = false;
            foreach (var entity in _designerRoot.EntityAdapters.Where(e => e.DerivedEntityAdapters.Count == 0).ToList())
            {
                foreach (var kpiName in entity.GetAllInheritanceAttributes().Where(e => !e.KpiName.IsNullOrEmpty()).Select(d => d.KpiName).Distinct())
                {
                    hasKPI = true;
                    codeBuilder.AddLine("var get" + kpiName + "Ranges = function (succeeded) {");
                    codeBuilder.AddLine("    if (vm.kpi" + kpiName + " == null) dataContext.get" + kpiName + "Ranges().then(querySucceeded);");
                    codeBuilder.AddLine("    else if (succeeded) succeeded(vm.kpi" + kpiName + ".ranges, vm.kpi" + kpiName + ".min, vm.kpi" + kpiName + ".max);");
                    codeBuilder.AddLine("    return true;");
                    codeBuilder.AddLine();
                    codeBuilder.AddLine("    function querySucceeded(data) {");
                    codeBuilder.AddLine("        vm.kpi" + kpiName + " = { ranges: [], min: 0, max: 0 };");
                    codeBuilder.AddLine("        for (var r in data.results) {");
                    codeBuilder.AddLine("            vm.kpi" + kpiName + ".ranges.push({ from: data.results[r].StartValue, to: data.results[r].EndValue, color: data.results[r].Color });");
                    codeBuilder.AddLine("            if (vm.kpi" + kpiName + ".min > data.results[r].StartValue) vm.kpi" + kpiName + ".min = data.results[r].StartValue;");
                    codeBuilder.AddLine("            if (vm.kpi" + kpiName + ".max < data.results[r].EndValue) vm.kpi" + kpiName + ".max = data.results[r].EndValue;");
                    codeBuilder.AddLine("        }");
                    codeBuilder.AddLine("        if (succeeded) succeeded(vm.kpi" + kpiName + ".ranges, vm.kpi" + kpiName + ".min, vm.kpi" + kpiName + ".max);");
                    codeBuilder.AddLine("    }");
                    codeBuilder.AddLine("};");

                    codeBuilder.AddLine("var get" + kpiName + "GaugeGrid = function (succeeded) {");
                    codeBuilder.AddLine("   if (vm.kpi" + kpiName + " == null) {");
                    codeBuilder.AddLine("       get" + kpiName + "Ranges(succeeded);");
                    codeBuilder.AddLine("   }");
                    codeBuilder.AddLine("   if (vm.kpi" + kpiName + " != null) {");
                    codeBuilder.AddLine("       var ranges = '';");
                    codeBuilder.AddLine("       for (var i in vm.kpi" + kpiName + ".ranges) {");
                    codeBuilder.AddLine("           ranges += '{\"from\" : \"' + vm.kpi" + kpiName + ".ranges[i].from + '\", \"to\" : \"' + vm.kpi" + kpiName + ".ranges[i].to + '\" , \"color\" : \"' + vm.kpi" + kpiName + ".ranges[i].color + '\" }';");
                    codeBuilder.AddLine("           if (i < vm.kpi" + kpiName + ".ranges.length - 1)");
                    codeBuilder.AddLine("               ranges += \",\";");
                    codeBuilder.AddLine("       }");
                    codeBuilder.AddLine("       ranges = \"[\" + ranges + \"]\";");
                    codeBuilder.AddLine("       var obj = {min: vm.kpi" + kpiName + ".min, max: vm.kpi" + kpiName + ".max, ranges: ranges };");
                    codeBuilder.AddLine("       return obj;");
                    codeBuilder.AddLine("    }");
                    codeBuilder.AddLine("   else {");
                    codeBuilder.AddLine("       return {min: 0, max: 0, ranges: \"[]\" };");
                    codeBuilder.AddLine("    }");
                    codeBuilder.AddLine("};");
                }
            }

            if (hasKPI)
            {
                codeBuilder.AddLine("var getKpiColor = function(ranges, value) {");
                codeBuilder.AddLine("    var result = 'white';");
                codeBuilder.AddLine("    if (typeof value === 'string')");
                codeBuilder.AddLine("        value = eval(value);");
                codeBuilder.AddLine("    if (ranges.length > 0) {");
                codeBuilder.AddLine("        if (value < eval(ranges[0].from))");
                codeBuilder.AddLine("            result = ranges[0].color;");
                codeBuilder.AddLine("        else if (value > eval(ranges[ranges.length - 1].to))");
                codeBuilder.AddLine("            result = ranges[ranges.length - 1].color;");
                codeBuilder.AddLine("        else {");
                codeBuilder.AddLine("            for (var i in ranges)");
                codeBuilder.AddLine("            {");
                codeBuilder.AddLine("                if (value >= eval(ranges[i].from) && value <= eval(ranges[i].to)) {");
                codeBuilder.AddLine("                    result = ranges[i].color;");
                codeBuilder.AddLine("                    break;");
                codeBuilder.AddLine("                }");
                codeBuilder.AddLine("            }");
                codeBuilder.AddLine("        }");
                codeBuilder.AddLine("    }");
                codeBuilder.AddLine("    return result;");
                codeBuilder.AddLine("}");
            }

            #endregion


            #region loadDataView
            codeBuilder.AddLine("var loadDataView = function () {");
            codeBuilder.AddLine(ui.QueryOnLoad ? "    queryLoaded = true; return query();" : "");
            codeBuilder.AddLine("};");
            #endregion

            #region getInnerJExpression
            codeBuilder.AddLine("var getInnerJExpression = function () {");
            codeBuilder.AddLine("    if (!uiSettings.applyFilterToParent || isNullOrEmpty(currentDataItem())) return '';");
            codeBuilder.AddLine("    dataBind('', true);");
            codeBuilder.AddLine("    var parentFieldsRelation = '';");
            codeBuilder.AddLine("    var detailFieldsRelation = '';");
            codeBuilder.AddLine("    if (uiSettings != null && uiSettings.parentFieldsRelation.length == uiSettings.detailFieldsRelation.length) {");
            codeBuilder.AddLine("      for (var idx = 0; idx < uiSettings.parentFieldsRelation.length; idx++) {");
            codeBuilder.AddLine("         parentFieldsRelation += (parentFieldsRelation == '' ? '' : ',') + uiSettings.parentFieldsRelation[idx];");
            codeBuilder.AddLine("         detailFieldsRelation += (detailFieldsRelation == '' ? '' : ',') + uiSettings.detailFieldsRelation[idx];");
            codeBuilder.AddLine("      }");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    var jExp = getQueryFilter(currentDataItem());");
            codeBuilder.AddLine("    if (jExp === 'Error') return 'Error';");
            codeBuilder.AddLine("    return '---' + currentDataItem().namespace + '.' + currentDataItem().typeName + '|' + uiSettings.parentSelectorDataName + '|' + parentFieldsRelation + '|' + detailFieldsRelation + ':::' + jExp;");
            codeBuilder.AddLine("};");
            #endregion

            #region clearInnerUIs
            codeBuilder.AddLine("var clearInnerUIs = function (parentEntity) {");
            codeBuilder.AddLine("   for (var idx = 0; idx < vm.internalUIs.length; idx++) { var innerVM = vm[vm.internalUIs[idx]](); if (isNullOrEmpty(parentEntity) || innerVM.getParentSelectorDataName() === parentEntity.typeName) innerVM.dataToolbar.clear(); }");
            codeBuilder.AddLine("};");
            #endregion

            #region replaceInnerUIsKeys
            codeBuilder.AddLine("var replaceInnerUIsKeys = function (parentEntity, parentPropertyName, oldValue, newValue) {");
            codeBuilder.AddLine("   for (var idx = 0; idx < vm.internalUIs.length; idx++) { var innerVM = vm[vm.internalUIs[idx]](); if (innerVM.getParentSelectorDataName() === parentEntity.typeName) innerVM.replaceKeyFromParent(parentPropertyName, oldValue, newValue); }");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var replaceKeyFromParent = function (parentPropertyName, oldValue, newValue) {");
            codeBuilder.AddLine("    if (parentEntityRelated != null && isChildVM() && uiSettings.detailFieldsRelation.length == 1 && uiSettings.parentFieldsRelation.length == 1 && uiSettings.parentFieldsRelation[0] === parentPropertyName) {");
            codeBuilder.AddLine("        dataBind('dataView', true);");
            codeBuilder.AddLine("        var cacheElements = getAddedEntities();");
            codeBuilder.AddLine("        for (var idxR = 0; idxR < cacheElements.length; idxR++) {");
            codeBuilder.AddLine("            if (getAbsoluteValue(cacheElements[idxR][uiSettings.detailFieldsRelation[0]]) == oldValue) setAbsoluteValue(cacheElements[idxR], uiSettings.detailFieldsRelation[0], newValue);");
            codeBuilder.AddLine("        }");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("};");
            #endregion

            #region getInnerJExpressions
            codeBuilder.AddLine("var getInnerJExpressions = function () {");
            codeBuilder.AddLine("   var innerFilters = '';");
            codeBuilder.AddLine("   for (var idx = 0; idx < vm.internalUIs.length; idx++) { var eSearch = vm[vm.internalUIs[idx]]().getInnerJExpression(); if (eSearch === 'Error') return 'Error';  if (eSearch.indexOf('#') >= 0) innerFilters += eSearch; }");
            codeBuilder.AddLine("   return innerFilters;");
            codeBuilder.AddLine("};");
            #endregion

            #region getParentSelectorFunctions
            codeBuilder.AddLine("var getParentSelectorDataName = function () {");
            codeBuilder.AddLine("   return ((typeof uiSettings === 'object') ? uiSettings.parentSelectorDataName : '');");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var validParentSelectorDataCondition = function (data) {");
            codeBuilder.AddLine("   return ((typeof uiSettings === 'object') && !isNullOrEmpty(uiSettings.parentSelectorDataCondition) ? eval(uiSettings.parentSelectorDataCondition) : true);");
            codeBuilder.AddLine("};");
            #endregion

            #region getJExpression
            codeBuilder.AddLine("var getJExpression = function (currentDI) {");
            codeBuilder.AddLine("    if (typeof currentDI === 'undefined') currentDI = currentDataItem();");
            codeBuilder.AddLine("    if (parentEntityRelated != null && isChildVM()) {");
            codeBuilder.AddLine("       for (var idx = 0; idx < uiSettings.parentFieldsRelation.length; idx++) { setAbsoluteValue(currentDI, uiSettings.detailFieldsRelation[idx], getAbsoluteValue(parentEntityRelated[uiSettings.parentFieldsRelation[idx]])); }");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    var extraFilters = '';");
            codeBuilder.AddLine("    if (isLookup()) {");
            codeBuilder.AddLine("         extraFilters = uiSettings.ownerReference.getLookUpClientFilterExpressions(uiSettings.lookupName, uiSettings.lookupInfo);");
            codeBuilder.AddLine("         if (extraFilters === 'Error') return extraFilters;");
            codeBuilder.AddLine("         if (typeof uiSettings.ownerReference['BeforeGet' + uiSettings.lookupName + 'Query'] == 'function') {");
            codeBuilder.AddLine("               var customFilter = uiSettings.ownerReference['BeforeGet' + uiSettings.lookupName + 'Query']('', uiSettings.lookupInfo);");
            codeBuilder.AddLine("               if (customFilter === 'Error') return null;");
            codeBuilder.AddLine("               if (!isNullOrEmpty(customFilter)) { extraFilters = (isNullOrEmpty(extraFilters) ? '' : extraFilters + ';') + customFilter; }");
            codeBuilder.AddLine("         }");
            codeBuilder.AddLine("         if (!isNullOrEmpty(extraFilters)) extraFilters = currentDI.typeName + '{' + extraFilters + '}';");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    var innerExps = getInnerJExpressions();");
            codeBuilder.AddLine("    if (innerExps === 'Error') return 'Error';");
            codeBuilder.AddLine("    return currentDI.getJExpression(vm.entitySearchRange, [], (parentEntityRelated != null)) + extraFilters + innerExps;");
            codeBuilder.AddLine("};");
            #endregion

            #region getSpecializedLookupItems
            codeBuilder.AddLine("var getSpecializedLookupItems = function () {");
            codeBuilder.AddLine("   var result = [];");
            if (!complementaryCalls.IsNullOrEmpty())
            {
                codeBuilder.AddLine("   if (dataView().length > 1 && !isNullOrEmpty(complement) && (typeof complement.selectedCurrentItems === 'function'))");
                codeBuilder.AddLine("       result = complement.selectedCurrentItems(false, true);");
                codeBuilder.AddLine("   if ((dataView().length == 1 || !navigationByPage() || isNullOrEmpty(complement) || (typeof complement.selectedItems !== 'function') || (uiSettings && uiSettings.allowMultiSelectionInSearch === false)) && result.length == 0)");
                codeBuilder.AddLine("       result.push(currentDataItem());");
            }
            else codeBuilder.AddLine("   result.push(currentDataItem());");

            codeBuilder.AddLine("   return result;");
            codeBuilder.AddLine("};");
            #endregion

            #region exportData
            codeBuilder.AddLine("var exportData = function (forceAdd, isExcelDataSource) {");
            if (ui.ExistsClientEvent("OnToolbarAction"))
                codeBuilder.AddLine("    if (!OnToolbarAction('Export')) return;");
            if (uiEntityAdapter != null)
            {
                codeBuilder.AddLine("    if (forceAdd)");
                codeBuilder.AddLine("        require(['viewmodels/shared/addCustomExport'],");
                codeBuilder.AddLine("            function(addCustomExport){ addCustomExport.showModal(vm, null, '" + uiEntityAdapter.Name + "', getVisiblePropertiesForExcel('dataView'), null, true, isExcelDataSource); } );");
                codeBuilder.AddLine("    else");
                codeBuilder.AddLine("        require(['viewmodels/shared/customExport'],");
                codeBuilder.AddLine("            function(modalExport){ modalExport.showModal(vm, '" + uiEntityAdapter.Name + "', getVisiblePropertiesForExcel('dataView'), null, { canAdd: true, canEdit: true, canDel: true }, isExcelDataSource); } );");
            }
            codeBuilder.AddLine("};");
            #endregion

            #region exportDataDetails (Children)
            codeBuilder.AddLine("var exportDataDetails = function (entity, detailName, isExcelDataSource) {");
            codeBuilder.AddLine("    require(['viewmodels/shared/addCustomExport'], function(addCustomExport){");
            codeBuilder.AddLine("         addCustomExport.showModal(vm, null, detailName, getVisiblePropertiesForExcel(detailName + 'List'), entity['GetJsWhereDetailRelationFor' + detailName](), true, isExcelDataSource); } ");
            codeBuilder.AddLine("    );");
            codeBuilder.AddLine("};");
            #endregion

            #region customLayout
            codeBuilder.AddLine("var customLayout = function() {");
            codeBuilder.AddLine("    require(['viewmodels/shared/customLayoutForm'],");
            codeBuilder.AddLine("        function(customLayout) { customLayout.showModal(vm); });");
            codeBuilder.AddLine("}");
            #endregion
            #region finalizeCombo
            codeBuilder.AddLine("var finalizeCombo = function (current, itens, lookupName) {");
            codeBuilder.AddLine("   dataContext['finalizeAll' + lookupName](current, itens, '', '');");
            codeBuilder.AddLine("};");
            #endregion

            #region clearCombo
            codeBuilder.AddLine("var clearCombo = function (current, lookupName) {");
            codeBuilder.AddLine("   dataContext['clear' + lookupName](current);");
            codeBuilder.AddLine("};");
            #endregion

            #region getDataCombo
            codeBuilder.AddLine("var dataCombo = {");
            codeBuilder.AddLine("    combos: [],");
            codeBuilder.AddLine("    getItems: function (comboName, valuesFilter) {");
            codeBuilder.AddLine("        var items = dataCombo.combos[comboName];");
            codeBuilder.AddLine("        if (!isNullOrEmpty(valuesFilter) && items && items.length > 0) {");
            codeBuilder.AddLine("            for (var i = items.length - 1; i >= 0; i--) {");
            codeBuilder.AddLine("                if ((',' + valuesFilter + ',').indexOf(',' + items[i].id + ',') === -1) {");
            codeBuilder.AddLine("                    items.removeAt(i);");
            codeBuilder.AddLine("                }");
            codeBuilder.AddLine("            }");
            codeBuilder.AddLine("        }");
            codeBuilder.AddLine("        return (items && items.length > 0 ? items : []);");
            codeBuilder.AddLine("    },");
            codeBuilder.AddLine("    fillDataCombos: function (lookupName, fieldName, current, complete) {");
            codeBuilder.AddLine("        dataContext.getResultsCombo(lookupName, fieldName, current, function (result) {");
            codeBuilder.AddLine("            dataCombo.combos[lookupName] = result;");
            codeBuilder.AddLine("            if (complete) complete();");
            codeBuilder.AddLine("        });");
            codeBuilder.AddLine("    },");
            codeBuilder.AddLine("    isFilterChanged: function (lookupName, current) {");
            codeBuilder.AddLine("        return dataContext.clientFilterHasModified(lookupName, current);");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("};");
            #endregion

            #region refreshCurrentData
            codeBuilder.AddLine("var refreshCurrentData = function () {");
            if (ui.ExistsClientEvent("OnToolbarAction"))
                codeBuilder.AddLine("    if (!OnToolbarAction('Refresh')) return;");
            codeBuilder.AddLine("    if (navigationByPage()) {");
            codeBuilder.AddLine("       var refreshIndexedData = function (currentIndex) {");
            codeBuilder.AddLine("             if (currentIndex < dataView().length) {");
            codeBuilder.AddLine("                 if (currentIndex == 0) vm.showProcessing('Atualizando informações...');");
            codeBuilder.AddLine("                 dataView()[currentIndex].refreshData(true, function (data) { if (data.results.length == 0) { app.showMessage('A informação a ser atualizada não está mais presente na base de dados!', 'Alerta', ['Ok']); vm.closeProcessing(); return; } refreshIndexedData(currentIndex + 1); });");
            codeBuilder.AddLine("             }");
            codeBuilder.AddLine("             else {");
            codeBuilder.AddLine("                 vm.closeProcessing();");
            codeBuilder.AddLine("                 dataBind();");
            codeBuilder.AddLine("             }");
            codeBuilder.AddLine("       };");
            codeBuilder.AddLine("       if (dataView().length > 0) {");
            codeBuilder.AddLine("            refreshIndexedData(0);");
            codeBuilder.AddLine("       }");
            codeBuilder.AddLine("       return;");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    vm.showProcessing('Atualizando informações...');");
            codeBuilder.AddLine("    return currentDataItem().refreshData(false, complete);");
            codeBuilder.AddLine();
            codeBuilder.AddLine("    function complete(data) {");

            codeBuilder.AddLine("        if (data.results.length == 0) { app.showMessage('A informação a ser atualizada não está mais presente na base de dados!', 'Alerta', ['Ok']); vm.closeProcessing(); return; }");

            codeBuilder.AddLine("        currentDataItem.notifySubscribers();");
            codeBuilder.AddLine("        vm.closeProcessing();");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("}");
            #endregion

            #region lazyRefreshBinding
            codeBuilder.AddLine("var _pendingRefresh = false;");
            codeBuilder.AddLine("var lazyRefreshBinding = function () {");
            codeBuilder.AddLine("   if (!_pendingRefresh) {");
            codeBuilder.AddLine("       _pendingRefresh = true;");
            codeBuilder.AddLine("       setTimeout(function () { currentDataItem.notifySubscribers(); _pendingRefresh = false; }, 500);");
            codeBuilder.AddLine("   }");
            codeBuilder.AddLine("};");
            #endregion

            #region getQueryFilter

            codeBuilder.AddLine("var getTranslatedFilter = function () {");
            codeBuilder.AddLine("    return translatedJEntitySearch + (isNullOrEmpty(translatedJEntitySearch) || isNullOrEmpty(customSearchResult.translatedSearch) ? '' : ' e ') + customSearchResult.translatedSearch;");
            codeBuilder.AddLine("}");

            codeBuilder.AddLine("var getQueryFilter = function (currentDI) {");
            codeBuilder.AddLine("    if (typeof currentDI === 'undefined') currentDI = currentDataItem();");
            codeBuilder.AddLine("    dataBind('', true);");
            codeBuilder.AddLine("    currentDI.setBandeiraRede(getBandeiraRede());");
            codeBuilder.AddLine("    eSearch = getJExpression(currentDI);");
            codeBuilder.AddLine("    if (eSearch === 'Error')");
            codeBuilder.AddLine("       return 'Error';");

            if (ui.ExistsClientEvent("OnSearching"))
            {
                codeBuilder.AddLine("    var extraFilter = OnSearching();");
                codeBuilder.AddLine("    if (extraFilter === 'Error')");
                codeBuilder.AddLine("       return 'Error';");
                codeBuilder.AddLine("    if (!isNullOrEmpty(extraFilter)) eSearch += extraFilter;");
            }

            if (ui.HasCustomization)
            {
                codeBuilder.AddLine("    var e = { cancel: false, jEntitySearch: eSearch, viewModel: vm };");
                codeBuilder.AddLine("    custom.beforeQuerying(e);");
                codeBuilder.AddLine("    if (e.cancel) return 'Error';");
                codeBuilder.AddLine();
                codeBuilder.AddLine("    eSearch = e.jEntitySearch;");
            }

            if (uiEntityAdapter.HasBrand(true))
            {
                codeBuilder.AddLine("    if (vm.getBandeiraRede() === 0) {");
                if (uiEntityAdapter.HasBrand())
                    codeBuilder.AddLine("       eSearch += '" + uiEntityAdapter.Name + "{IdBandeiraRede#In#S' + vm.getCurrentBrands() + '}';");
                foreach (var detail in uiEntityAdapter.GetAllSourceEntityAdapters())
                {
                    if (detail.HasBrand() && detail.ForceBrandFilter)
                        codeBuilder.AddLine("       eSearch += '" + detail.Name + "{IdBandeiraRede#In#S' + vm.getCurrentBrands() + '}';");
                }
                codeBuilder.AddLine("    }");
            }

            codeBuilder.AddLine("   translatedJEntitySearch = common.translateSearch(dataContext, eSearch);");

            if (!uiEntityAdapter.IsDashboardFilter && uiEntityAdapter.EnableMetaDataFilter)
                codeBuilder.AddLine("    if (!isNullOrEmpty(eSearch) && eSearch.indexOf('{LinqValidProperties#') === -1) eSearch += getVisibleProperties('dataView');");

            codeBuilder.AddLine("    if (!isNullOrEmpty(customSearchResult.searchDefinition)) eSearch += customSearchResult.searchDefinition;");

            codeBuilder.AddLine("    return eSearch;");
            codeBuilder.AddLine("}");
            #endregion

            #region queryInnerUIs
            codeBuilder.AddLine("var queryInnerUIs = function (parentEntity, parentTypeName) {");
            codeBuilder.AddLine("   if (status() === 'C') return;");
            codeBuilder.AddLine("   commitInternalUIsData();");
            codeBuilder.AddLine("   for (var idx = 0; idx < vm.internalUIs.length; idx++) { var innerVM = vm[vm.internalUIs[idx]](); if ((!isNullOrEmpty(parentTypeName) && innerVM.getParentSelectorDataName() === parentTypeName) || (!isNullOrEmpty(parentEntity) && innerVM.getParentSelectorDataName() === parentEntity.typeName)) { if (isNullOrEmpty(parentEntity) || innerVM.validParentSelectorDataCondition(parentEntity)) innerVM.dataToolbar.query(false, parentEntity); else if (innerVM.status() === 'Q') innerVM.clear();  } }");
            codeBuilder.AddLine("};");
            #endregion

            #region addNewToInnerUI
            codeBuilder.AddLine("var addNewToInnerUI = function (parentEntity, uiName) {");
            codeBuilder.AddLine("   setTimeout(function () {");
            codeBuilder.AddLine("       for (var idx = 0; idx < vm.internalUIs.length; idx++) { var innerVM = vm[vm.internalUIs[idx]](); if (innerVM.getParentSelectorDataName() === parentEntity.typeName && (isNullOrEmpty(uiName) || innerVM.viewName === uiName)) innerVM.dataToolbar.addNew(parentEntity); }");
            codeBuilder.AddLine("   }, 1000);");
            codeBuilder.AddLine("};");
            #endregion

            #region remove related items from related items
            codeBuilder.AddLine("var removeInnerDataUIs = function (parentEntity) {");
            codeBuilder.AddLine("   for (var idx = 0; idx < vm.internalUIs.length; idx++) { var innerVM = vm[vm.internalUIs[idx]](); if (!isNullOrEmpty(parentEntity) && innerVM.getParentSelectorDataName() === parentEntity.typeName) innerVM.removeParentRelatedItems(parentEntity); }");
            codeBuilder.AddLine("};");
            #endregion

            #region getDataFromInnerUI
            codeBuilder.AddLine("var getDataFromInnerUI = function (uiName) {");
            codeBuilder.AddLine("   for (var idx = 0; idx < vm.internalUIs.length; idx++) { var innerVM = vm[vm.internalUIs[idx]](); if (innerVM.viewName === uiName) return innerVM.currentDataItem(); }");
            codeBuilder.AddLine("};");
            #endregion

            #region saveInnerUIs

            codeBuilder.AddLine("var saveInnerUIs = function () {");
            codeBuilder.AddLine("  var vmsForSaving = [];");
            codeBuilder.AddLine("  var saveInnerUI = function (currentIndex) {");
            codeBuilder.AddLine("        if (currentIndex < vmsForSaving.length)");
            codeBuilder.AddLine("            vmsForSaving[currentIndex].dataToolbar.save(false, function () { saveInnerUI(currentIndex + 1); });");
            codeBuilder.AddLine("  };");
            codeBuilder.AddLine("  for (var idx = 0; idx < vm.internalUIs.length; idx++) {");
            codeBuilder.AddLine("      var innerVM = vm[vm.internalUIs[idx]]();");
            codeBuilder.AddLine("      if (innerVM.status() === 'E') vmsForSaving.push(innerVM);");
            codeBuilder.AddLine("  }");
            codeBuilder.AddLine("  if (vmsForSaving.length > 0) {");
            codeBuilder.AddLine("       saveInnerUI(0);");
            codeBuilder.AddLine("  }");
            codeBuilder.AddLine("};");

            #endregion

            #region undoInnerUIs
            codeBuilder.AddLine("var undoInnerUIs = function () {");
            codeBuilder.AddLine("  for (var idx = 0; idx < vm.internalUIs.length; idx++) { var innerVM = vm[vm.internalUIs[idx]](); if (innerVM.status() === 'E') innerVM.dataToolbar.undo(); }");
            codeBuilder.AddLine("  if (status() === 'Q' && !isNullOrEmpty(currentDataItem())) {");
            codeBuilder.AddLine("       for (var idx = 0; idx < vm.internalUIs.length; idx++) { var innerVM = vm[vm.internalUIs[idx]](); innerVM.dataToolbar.clear(); }");
            codeBuilder.AddLine("       currentDataItem().fillDetails();");
            codeBuilder.AddLine("  }");
            codeBuilder.AddLine("};");
            #endregion

            #region syncStatus
            codeBuilder.AddLine("var editInnerUIs = function () {");
            codeBuilder.AddLine("  for (var idx = 0; idx < vm.internalUIs.length; idx++) { var innerVM = vm[vm.internalUIs[idx]](); if (innerVM.isEditable()) innerVM.dataToolbar.edit(); } ");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var setStatus = function (st) {");
            codeBuilder.AddLine("  status(st);");
            codeBuilder.AddLine("  goToIndex(currentDataIndex());");
            codeBuilder.AddLine("};");
            #endregion
            if (uiEntityAdapter.GetTopParent().IsBufferSaving())
            {
                codeBuilder.AddLine("var dataCache = []; //Initialize data cache");
                codeBuilder.AddLine("var syncDataCache = function () {");
                codeBuilder.AddLine("    " + entitiesRef + "().forEach(function (element) {");
                codeBuilder.AddLine("        if (element.ChangeState && dataCache.indexOf(element) < 0) { dataCache.push(element); }");
                codeBuilder.AddLine("    });");
                codeBuilder.AddLine("}");

                codeBuilder.AddLine("var getDataForSaving = function () {");
                codeBuilder.AddLine("    var result = [];");
                codeBuilder.AddLine("    dataCache = [];");
                codeBuilder.AddLine("    if (preserveDataCurrentState()) {");
                codeBuilder.AddLine("       syncDataCache();");
                codeBuilder.AddLine("       result = dataCache;");
                codeBuilder.AddLine("    }");
                codeBuilder.AddLine("    else {");
                codeBuilder.AddLine("       result = " + entitiesRef + "();");
                codeBuilder.AddLine("    }");
                codeBuilder.AddLine("    return _.filter(result, function (e) { return (['U', 'I', 'D'].indexOf(e.ChangeState) >= 0); }).concat(removedEntities);");
                codeBuilder.AddLine("}");
            }


            codeBuilder.AddLine("var getAllChanges = function () {");
            if (uiEntityAdapter.GetTopParent().IsBufferSaving())
            {
                codeBuilder.AddLine("    var details = [];");
                codeBuilder.AddLine("    var changes = getDataForSaving();");
                codeBuilder.AddLine("    for (var idx = 0; idx < changes.length; idx++) {");
                codeBuilder.AddLine("       details = details.concat(changes[idx].getAllDetailChanges());");
                codeBuilder.AddLine("    }");
                codeBuilder.AddLine("    if (details.length > 0)");
                codeBuilder.AddLine("         return changes.concat(details);");
                codeBuilder.AddLine("    else return changes;");
            }
            else
            {
                codeBuilder.AddLine("    return dataContext.getChanges();");
            }
            codeBuilder.AddLine("}");

            codeBuilder.AddLine("var getAddedEntities = function () {");

            if (uiEntityAdapter.GetTopParent().IsBufferSaving())
            {
                codeBuilder.AddLine("    var result = [];");
                codeBuilder.AddLine("    if (preserveDataCurrentState()) {");
                codeBuilder.AddLine("       syncDataCache();");
                codeBuilder.AddLine("       result = dataCache;");
                codeBuilder.AddLine("    }");
                codeBuilder.AddLine("    else {");
                codeBuilder.AddLine("       result = " + entitiesRef + "();");
                codeBuilder.AddLine("    }");
                codeBuilder.AddLine("    return _.filter(result, function (e) { return (e.ChangeState == 'I'); });");
            }
            else
            {
                codeBuilder.AddLine("    return dataContext.getEntities('" + masterEntityName + "', [dataContext.breeze.EntityState.Added]);");
            }

            codeBuilder.AddLine("}");

            codeBuilder.AddLine("var getRelatedElementsInCache = function () {");
            codeBuilder.AddLine("    if (parentEntityRelated != null && preserveDataCurrentState()) {");
            if (uiEntityAdapter.GetTopParent().IsBufferSaving())
            {
                codeBuilder.AddLine("       syncDataCache();");
                codeBuilder.AddLine("       var cacheElements = dataCache;");
            }
            else
                codeBuilder.AddLine("       var cacheElements = dataContext.getEntities('" + masterEntityName + "');");
            codeBuilder.AddLine("       var result = [];");
            codeBuilder.AddLine("       var relationExpr = '';");
            codeBuilder.AddLine("       for (var idx = 0; idx < uiSettings.parentFieldsRelation.length; idx++) { relationExpr += (relationExpr === '' ? '' : ' && ') + 'getAbsoluteValue(cacheElements[idxR][uiSettings.detailFieldsRelation[' + idx.toString() + ']]) === getAbsoluteValue(parentEntityRelated[uiSettings.parentFieldsRelation[' + idx.toString() + ']])'; }");
            codeBuilder.AddLine("       for (var idxR = 0; idxR < cacheElements.length; idxR++) {");
            codeBuilder.AddLine("           if (eval(relationExpr)) { result.push(cacheElements[idxR]); }");
            codeBuilder.AddLine("       }");
            codeBuilder.AddLine("       dataView(result);");
            codeBuilder.AddLine("       return (dataView().length > 0 ? 0 : (parentEntityRelated.isAdded() ? 0 : -1));");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    return -1;");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var isChildVM = function () {");
            codeBuilder.AddLine("   return (parentVM != null && uiSettings != null && !isNullOrEmpty(uiSettings.parentSelectorDataName) && (typeof uiSettings.parentFieldsRelation !== 'undefined') && (typeof uiSettings.detailFieldsRelation !== 'undefined') && uiSettings.parentFieldsRelation.length == uiSettings.detailFieldsRelation.length) && !isLookup();");
            codeBuilder.AddLine("}");

            codeBuilder.AddLine("var isLookup = function () {");
            codeBuilder.AddLine("   return (uiSettings != null && (typeof uiSettings.lookupInfo === 'object'));");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var allowMultiSelectionInSearch = function () {");
            codeBuilder.AddLine("   if (isLookup() && (typeof uiSettings.allowMultiSelectionInSearch !== 'undefined')) return uiSettings.allowMultiSelectionInSearch;");
            codeBuilder.AddLine("   else return true;");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var parentEntityRelated = null;");
            codeBuilder.AddLine("var freeEntityForQuerying = null;");
            codeBuilder.AddLine("var isProcessing = false;");
            codeBuilder.AddLine("var adjustExternalParentRelation = function (selectedElement) {");
            codeBuilder.AddLine("    if (isNullOrEmpty(selectedElement)) selectedElement = currentDataItem();");
            codeBuilder.AddLine("    if (parentEntityRelated != null && isChildVM() && (uiSettings.canAddNew || uiSettings.canEdit || uiSettings.canDelete)) {");
            codeBuilder.AddLine("        for (var idx = 0; idx < uiSettings.parentFieldsRelation.length; idx++) { setAbsoluteValue(selectedElement, uiSettings.detailFieldsRelation[idx], getAbsoluteValue(parentEntityRelated[uiSettings.parentFieldsRelation[idx]])); }");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("};");

            #region openingExternalUIFromGrid
            codeBuilder.AddLine("var openingExternalUIFromGrid = function (externalUIName, qbeSearch) {");
            if (ui.ExistsClientEvent("OnOpeningExternalUIFromGrid"))
            {
                codeBuilder.AddLine("   var customQBE = OnOpeningExternalUIFromGrid(externalUIName, qbeSearch);");
                codeBuilder.AddLine("   if(!isNullOrEmpty(customQBE))");
                codeBuilder.AddLine("       qbeSearch = customQBE");
            }

            codeBuilder.AddLine("   return qbeSearch;");
            codeBuilder.AddLine("}");
            #endregion

            #region query
            codeBuilder.AddLine("function restoreLastFilter(clearFilters) {");
            codeBuilder.AddLine("        if (isChildVM()) { filteredEntities = []; return false; }");
            codeBuilder.AddLine("        if (clearFilters || !common.getLastFilterMode()) filteredEntities = [];");
            codeBuilder.AddLine("        if (filteredEntities.length === 0) return false;");
            codeBuilder.AddLine("        dataContext.clearAll();");
            codeBuilder.AddLine("        //Attach Elements");
            codeBuilder.AddLine("        for(var idx = 0; idx < filteredEntities.length; idx++) { dataContext.attachEntity(filteredEntities[idx]); }");
            codeBuilder.AddLine("        //Set Current Details");
            codeBuilder.AddLine("        for(var idx = 0; idx < filteredEntities.length; idx++) { filteredEntities[idx].setCurrentDetails(null, true); }");
            codeBuilder.AddLine("        dataView([filteredEntities[0]]);");
            codeBuilder.AddLine("        if (clearFilters) filteredEntities = [];");
            codeBuilder.AddLine("        return true;");
            codeBuilder.AddLine("}");
            codeBuilder.AddLine();
            codeBuilder.AddLine("function adjustNavigationByPage(isNavByPage) {");
            codeBuilder.AddLine("    navigationByPage(isNavByPage);");
            codeBuilder.AddLine("    dataBind();");
            codeBuilder.AddLine("}");
            codeBuilder.AddLine();
            codeBuilder.AddLine("var preserveDataCurrentState = function () {");
            codeBuilder.AddLine("   return (status() !== 'C' && pageSize() === 0 && isChildVM());");
            codeBuilder.AddLine("}");
            codeBuilder.AddLine();

            codeBuilder.AddLine("var detachFilteredEntities = function (clear) {");
            codeBuilder.AddLine("    if (filteredEntities.length > 0) {");
            codeBuilder.AddLine("        for (var idx = 0; idx < filteredEntities.length; idx++) {");
            codeBuilder.AddLine("            dataContext.detachEntity(filteredEntities[idx]);");
            codeBuilder.AddLine("        }");
            codeBuilder.AddLine("        if (clear) filteredEntities = [];");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("}");


            codeBuilder.AddLine();
            codeBuilder.AddLine("var query = function (lookupInitializing, parentEntity, quickSearchJExpression, externalQueryCallBack, noMessages, noDetails) {");
            if (ui.ExistsClientEvent("OnToolbarAction"))
                codeBuilder.AddLine("    if (!OnToolbarAction('Query')) return;");

            codeBuilder.AddLine("    if (isProcessing) return;");
            codeBuilder.AddLine("    isProcessing = true;");
            codeBuilder.AddLine("    vm.canReportErrors = false;");

            codeBuilder.AddLine("    if (lookupInitializing === true && uiSettings && uiSettings.modalForm && (typeof uiSettings.modalForm.hide === 'function')) uiSettings.modalForm.hide(true);");
            codeBuilder.AddLine("    if (!isNullOrEmpty(parentEntity) && !isNullOrEmpty(parentEntity.typeName))");
            codeBuilder.AddLine("       parentEntityRelated = parentEntity;");
            codeBuilder.AddLine("    else");
            codeBuilder.AddLine("       parentEntityRelated = null;");
            codeBuilder.AddLine("    if ((isNullOrEmpty(parentEntityRelated) || (status() === 'C' && (parentEntityRelated != null && parentEntityRelated.isAdded()))) && isChildVM()) { dataContext.clearAll();" + (uiEntityAdapter.GetTopParent().IsBufferSaving() ? " dataCache = [];" : "") + " if (isNullOrEmpty(parentEntityRelated)) { currentDataItem(null); querySucceeded({ results: [] }); return complete(); } }");
            codeBuilder.AddLine("    if ((status() !== 'C' || (parentEntityRelated != null && parentEntityRelated.isAdded())) && getRelatedElementsInCache() >= 0) { querySucceeded({ results: " + entitiesRef + "() }); return complete(); }");
            codeBuilder.AddLine("    if (freeEntityForQuerying == null && isChildVM()) freeEntityForQuerying = dataContext.createFreeEntity('" + masterEntityName + "');");

            codeBuilder.AddLine("    if (status() === 'C' && !isNullOrEmpty(currentDataItem()) && currentDataItem().getCurrentElements) {");
            codeBuilder.AddLine("        filteredEntities = currentDataItem().getCurrentElements();");
            codeBuilder.AddLine("        if (isChildVM())");
            codeBuilder.AddLine("            detachFilteredEntities(true);");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    else");
            codeBuilder.AddLine("        filteredEntities = [];");

            codeBuilder.AddLine("    if (uiSettings != null && uiSettings.noSearch) { dataView([currentDataItem()]); status('Q'); refreshToolbar(); return complete(); }");
            codeBuilder.AddLine("    lastJEntitySearch = (isNullOrEmpty(quickSearchJExpression) ? '' : quickSearchJExpression) + getQueryFilter((isChildVM() ? freeEntityForQuerying : currentDataItem()));");
            codeBuilder.AddLine("    if (lastJEntitySearch === 'Error')");
            codeBuilder.AddLine("        return complete();");
            codeBuilder.AddLine("    var hasError = true;");
            codeBuilder.AddLine("    if (status() === 'C') { detachFilteredEntities(); }");

            if (uiEntityAdapter.GetTopParent().IsBufferSaving())
                codeBuilder.AddLine("    if (!preserveDataCurrentState()) dataCache = [];");

            codeBuilder.AddLine("    if (isChildVM() && (uiSettings.canAddNew || uiSettings.canEdit || uiSettings.canDelete))");
            codeBuilder.AddLine("       status(parentVM.status());");

            codeBuilder.AddLine("    if (!_noBusyLoading) vm.showProcessing('Pesquisando informações...');");

            codeBuilder.AddLine("    return dataContext.get" + masterEntityName + "ByEntitySearchNoAssociations(lastJEntitySearch, 0, pageSize(), (pageSize() > 0), preserveDataCurrentState(), " + (hasLargeDataMode ? "true" : "status() !== 'E'") + ", sortInfo, querySucceeded, complete);");
            codeBuilder.AddLine();
            codeBuilder.AddLine("    function complete() {");
            codeBuilder.AddLine("        isProcessing = false;");
            codeBuilder.AddLine("        if (!_noBusyLoading) vm.closeProcessing();");

            codeBuilder.AddLine("        if (hasError === true && lookupInitializing === true && isLookup() && (parentVM != null)) {");
            codeBuilder.AddLine("           parentVM.UI_Close_Click();");
            codeBuilder.AddLine("        }");
            codeBuilder.AddLine("        else if (hasError === true) {");
            codeBuilder.AddLine("           clear();");
            codeBuilder.AddLine("        }");

            codeBuilder.AddLine("    }");
            codeBuilder.AddLine();
            codeBuilder.AddLine("    function querySucceeded(data) {");
            codeBuilder.AddLine("        " + (hasLargeDataMode ? "" : "if (vm.status() !== 'E') { ") + "for (var idx = 0; idx < data.results.length; idx++) { dataContext.initializePOCO(data.results[idx], '" + masterEntityName + "'); }" + (hasLargeDataMode ? "" : " }"));
            codeBuilder.AddLine("        hasError = false;");
            codeBuilder.AddLine("        " + entitiesRef + "(data.results);");
            codeBuilder.AddLine("        if (" + entitiesRef + "().length === 0 && (parentVM == null || (parentVM != null && uiSettings != null && isNullOrEmpty(uiSettings.parentSelectorDataName)) || isLookup())) {");
            codeBuilder.AddLine("            if (isLookup() && (parentVM != null) && lookupInitializing === true) {");
            codeBuilder.AddLine("               uiSettings.ownerReference.clearLookUp(uiSettings.lookupName);");
            codeBuilder.AddLine("               app.showMessage('A informação de Lookup [' + uiSettings.ownerReference.getDisplayName(uiSettings.fieldToSearch) + '] não foi encontrada!', 'Informação', ['Ok']);");
            codeBuilder.AddLine("               parentVM.UI_Close_Click();");
            codeBuilder.AddLine("               return;");
            codeBuilder.AddLine("            }");
            codeBuilder.AddLine("            else  {");
            codeBuilder.AddLine("               if (!noMessages) { app.showMessage('Nenhum registro foi encontrado!', '" + "Informação', ['Ok']); }");
            codeBuilder.AddLine("               refreshToolbar();");
            codeBuilder.AddLine("            }");
            codeBuilder.AddLine("            if (restoreLastFilter()) {");
            codeBuilder.AddLine("               pageCount(1);");
            codeBuilder.AddLine("               totalItemCount(1);");
            codeBuilder.AddLine("               currentPage(0);");
            codeBuilder.AddLine("               status('C');");
            codeBuilder.AddLine("               goToIndex(0);");
            codeBuilder.AddLine("               dataBind();");
            codeBuilder.AddLine("               isBusy(false);");
            codeBuilder.AddLine("            }");
            codeBuilder.AddLine("            else {");
            codeBuilder.AddLine("               clear();");
            codeBuilder.AddLine("            }");
            codeBuilder.AddLine("            return true;");
            codeBuilder.AddLine("        }");
            codeBuilder.AddLine("        pageCount( (pageSize() > 0 ? Math.ceil((data.inlineCount ? data.inlineCount : " + entitiesRef + "().length) / pageSize()) : 1) );");
            codeBuilder.AddLine("        totalItemCount((data.inlineCount ? data.inlineCount : " + entitiesRef + "().length));");
            codeBuilder.AddLine("        currentPage(0);");

            codeBuilder.AddLine("        if (!(isChildVM() && (uiSettings.canAddNew || uiSettings.canEdit || uiSettings.canDelete)))");
            codeBuilder.AddLine("           status('Q');");
            codeBuilder.AddLine("        clearInnerUIs();");
            codeBuilder.AddLine("        goToIndex(0, noDetails);");
            if (ui.HasCustomization)
                codeBuilder.AddLine("        custom.afterQuerying({ dataItems: " + entitiesRef + "(), viewModel: vm });");

            codeBuilder.AddLine("        if (isLookup() && (parentVM != null) && (" + entitiesRef + "().length === 1) && lookupInitializing === true) {");
            codeBuilder.AddLine("           if (uiSettings.lookupInfo.isMultiSelection === true && (typeof currentDataItem().IsSelected === 'function')) currentDataItem().IsSelected(true);");
            codeBuilder.AddLine("           parentVM.UI_selectOption('Ok');");
            codeBuilder.AddLine("           return;");
            codeBuilder.AddLine("        }");
            codeBuilder.AddLine("        if (lookupInitializing === true && uiSettings.modalForm && (typeof uiSettings.modalForm.hide === 'function')) uiSettings.modalForm.hide(false);");
            codeBuilder.AddLine("        dataBind((isChildVM() ? '' : 'dataView'));");
            //Starts Viewinfo if isn't Navigation by Page and result > 1
            codeBuilder.AddLine("        if (common.getGridMode() == 'G' && !vm.navigationByPage() && (viewType() === 'Main') && !isChildVM() && " + entitiesRef + "().length > 1 && (parentVM == null))");
            codeBuilder.AddLine("            dataToolbar.viewInfo();");
            if (ui.ExistsClientEvent("OnSearched"))
                codeBuilder.AddLine("        OnSearched();");
            codeBuilder.AddLine("        if (typeof externalQueryCallBack === 'function') externalQueryCallBack();");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("};");
            #endregion query
            #region goToIndex
            codeBuilder.AddLine("function goToIndex(index, noDetails) {");
            codeBuilder.AddLine("    if (" + entitiesRef + "().length === 0) { currentDataIndex(0); currentDataItem(null); return true; }");
            codeBuilder.AddLine("    if (index < 0) { index = 0; }");
            codeBuilder.AddLine("    else if (index >= " + entitiesRef + "().length) { index = " + entitiesRef + "().length - 1; }");

            if (ui.ExistsClientEvent("OnNavigating"))
                codeBuilder.AddLine("    if (status() !== 'C' && currentDataItem() !== null && currentDataItem() !== " + entitiesRef + "()[index]) { if (!OnNavigating(currentDataIndex(), index)) return; }");

            codeBuilder.AddLine("    currentDataIndex(index);");
            codeBuilder.AddLine("    var oldValue = currentDataItem();");
            codeBuilder.AddLine("    currentDataItem(" + entitiesRef + "()[index]);");
            codeBuilder.AddLine("    if (status() !== 'C' && currentDataItem() !== null && oldValue !== currentDataItem()) {");

            codeBuilder.AddLine("       if (!noDetails) currentDataItem().fillDetails();");
            if (ui.ExistsClientEvent("OnNavigated"))
                codeBuilder.AddLine("       OnNavigated(index);");
            codeBuilder.AddLine("    }");
            if (ui.HasCustomization)
                codeBuilder.AddLine("    custom.afterSelecting({ selectedItem: currentDataItem(), viewModel: vm });");
            codeBuilder.AddLine("    resizeToolbar();");
            codeBuilder.AddLine("}");
            #endregion
            #region goToItem
            codeBuilder.AddLine("function goToItem(item) {");
            codeBuilder.AddLine("        goToIndex(" + entitiesRef + "().indexOf(item));");
            codeBuilder.AddLine("}");
            #endregion
            #region goToKey
            codeBuilder.AddLine("function goToKey(primaryKey, value, currentElement, viewSource) {");
            codeBuilder.AddLine("    if (!viewSource) viewSource = " + entitiesRef + ";");
            codeBuilder.AddLine("    var dataFiltered = viewSource().filter(function (item) { return getAbsoluteValue(item[primaryKey]) == value; });");
            codeBuilder.AddLine("    if (dataFiltered.length > 0) {");
            codeBuilder.AddLine("        if (currentElement && currentElement()) {");
            codeBuilder.AddLine("            currentElement().commitDetailsVisualPendings();");
            codeBuilder.AddLine("            currentElement(dataFiltered[0]);");
            codeBuilder.AddLine("            currentElement().fillDetails();");
            codeBuilder.AddLine("        } else {");
            codeBuilder.AddLine("            if (currentDataItem()) {");
            codeBuilder.AddLine("                currentDataItem().commitDetailsVisualPendings();");
            codeBuilder.AddLine("            }");
            codeBuilder.AddLine("            goToIndex(viewSource.indexOf(dataFiltered[0]));");
            codeBuilder.AddLine("        }");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("}");
            #endregion
            #region refresh
            codeBuilder.AddLine("var sortData = function (sortDef) {");
            codeBuilder.AddLine("    if (status() === 'Q' && pageCount() > 1 && sortInfo != sortDef) {");
            codeBuilder.AddLine("       sortInfo = sortDef;");
            codeBuilder.AddLine("       refresh(0, false);");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var refresh = function (curPage, goLast) {");
            //codeBuilder.AddLine("    isBusy(true);");
            codeBuilder.AddLine("    vm.showProcessing('Pesquisando informações...');");

            codeBuilder.AddLine("    return dataContext.get" + masterEntityName + "ByEntitySearchNoAssociations(lastJEntitySearch, curPage * pageSize(), pageSize(), false, false, status() !== 'E', sortInfo, querySucceeded, complete);");
            codeBuilder.AddLine();
            codeBuilder.AddLine("    function complete() {");
            codeBuilder.AddLine("        vm.closeProcessing();");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine();
            codeBuilder.AddLine("    function querySucceeded(data) {");
            codeBuilder.AddLine("        if (vm.status() !== 'E') { for (var idx = 0; idx < data.results.length; idx++) { dataContext.initializePOCO(data.results[idx], '" + masterEntityName + "'); } }");
            codeBuilder.AddLine("        " + entitiesRef + "(data.results);");
            codeBuilder.AddLine("        currentPage(curPage);");
            codeBuilder.AddLine("        goToIndex((goLast ? " + entitiesRef + "().length : 0));");
            codeBuilder.AddLine("        dataBind('dataView');");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("};");
            #endregion
            #region Client Events
            this.AddClientEvents(ui, msEngine, codeBuilder);
            #endregion Client Events            
            #region clear
            codeBuilder.AddLine("var clearByUser = function () {");
            codeBuilder.AddLine("    if (!isNullOrEmpty(customSearchResult.searchDefinition)) {");
            codeBuilder.AddLine("        app.showMessage('" + "Deseja limpar a pesquisa avançada?".Translate() + "', '" + "Alerta".Translate() + "', ['Yes', 'No'])");
            codeBuilder.AddLine("        .then(function (selectedOption) {");
            codeBuilder.AddLine("            if (selectedOption === 'Yes') {");
            codeBuilder.AddLine("                customSearchResult.searchDefinition = '';");
            codeBuilder.AddLine("                customSearchResult.serializedSearch = '';");
            codeBuilder.AddLine("                customSearchResult.translatedSearch = '';");
            codeBuilder.AddLine("                hasCustomSearches(false);");
            codeBuilder.AddLine("            }");
            codeBuilder.AddLine("            return clear();");
            codeBuilder.AddLine("         });");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    else return clear();");
            codeBuilder.AddLine("}");

            codeBuilder.AddLine("var clear = function (noBindingReport) {");
            codeBuilder.AddLine("    if (uiSettings && parentVM && uiSettings.noSearch === true && parentVM.status() !== 'C') return;");
            codeBuilder.AddLine("    vm.canReportErrors = false;");

            if (ui.ExistsClientEvent("OnToolbarAction"))
                codeBuilder.AddLine("    if (!OnToolbarAction('Clear')) return;");
            codeBuilder.AddLine("    parentEntityRelated = null;");
            if (ui.ExistsClientEvent("OnClearing"))
                codeBuilder.AddLine("    if (!OnClearing()) return;");
            if (ui.HasCustomization)
            {
                codeBuilder.AddLine("    var e = { cancel: false, viewModel: vm };");
                codeBuilder.AddLine("    custom.beforeClearing(e);");
                codeBuilder.AddLine("    if (e.cancel) return;");
            }
            codeBuilder.AddLine("    isBusy(true);");

            codeBuilder.AddLine("    lastStatus = status();");
            codeBuilder.AddLine("    status('C');");

            codeBuilder.AddLine("    if (restoreLastFilter(lastStatus === 'C')) return clearComplete({ results: dataView() }, true);");
            codeBuilder.AddLine("    else return dataContext.clear" + masterEntityName + "(getBandeiraRede(), clearComplete);");
            codeBuilder.AddLine();
            codeBuilder.AddLine("    function clearComplete(data, holdRanges) {");
            codeBuilder.AddLine("        dataForUndo = [];");
            if (uiEntityAdapter.GetTopParent().IsBufferSaving())
            {
                codeBuilder.AddLine("        dataCache = []; //Initialize data cache");
                codeBuilder.AddLine("        removedEntities = []; //Initialize removeds");
            }
            codeBuilder.AddLine("        dataView(data.results);");
            codeBuilder.AddLine("        if (holdRanges != true) vm.entitySearchRange.clear();");
            codeBuilder.AddLine("        if (typeof noBindingReport === 'boolean' && noBindingReport === true) { pageCount(1); currentPage(0); goToIndex(0); return; }");
            codeBuilder.AddLine("        pageCount(1);");
            codeBuilder.AddLine("        totalItemCount(data.results.length);");
            codeBuilder.AddLine("        lastStatus = 'C';");
            codeBuilder.AddLine("        currentPage(0);");
            codeBuilder.AddLine("        goToIndex(0);");
            codeBuilder.AddLine("        adjustFormView();");
            codeBuilder.AddLine("        dataBind();");
            codeBuilder.AddLine("        isBusy(false);");
            codeBuilder.AddLine("        hideButtonsEditorTemplate();");
            codeBuilder.AddLine("        clearInnerUIs();");
            if (ui.HasCustomization)
                codeBuilder.AddLine("        custom.afterClearing({ dataItem: data.results, viewModel: vm });");

            if (ui.ExistsClientEvent("OnCleared"))
                codeBuilder.AddLine("        OnCleared();");


            codeBuilder.AddLine("        scrollMainTop();");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("};");
            #endregion
            #region hasChanges
            codeBuilder.AddLine("var hasChanges = ko.computed(function () {");
            codeBuilder.AddLine("        return dataContext.hasChanges();");
            codeBuilder.AddLine("});");
            #endregion
            #region save
            codeBuilder.AddLine("var hasInternalUIsValidationErrors = function () {");
            codeBuilder.AddLine("    for (var idx = 0; idx < vm.internalUIs.length; idx++) { var innerVM = vm[vm.internalUIs[idx]](); if (innerVM.status() === 'E' && innerVM.hasValidationErrors()) return true; }");
            codeBuilder.AddLine("    return false;");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var hasInternalUIsSavingErrors = function () {");
            codeBuilder.AddLine("    for (var idx = 0; idx < vm.internalUIs.length; idx++) { var innerVM = vm[vm.internalUIs[idx]](); if (innerVM.status() === 'E' && !innerVM.onSavingValidation()) return true; }");
            codeBuilder.AddLine("    return false;");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var commitInternalUIsData = function () {");
            codeBuilder.AddLine("    for (var idx = 0; idx < vm.internalUIs.length; idx++) { var innerVM = vm[vm.internalUIs[idx]](); innerVM.dataBind('', true); }");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var onSavingValidation = function (changes) {");

            codeBuilder.AddLine("    if (!changes) changes = getAllChanges();");

            codeBuilder.AddLine("    if (changes.length === 0) { if (vm.internalUIs.length === 0) { undo(changes) }; return true; }");

            if (ui.ExistsClientEvent("OnSaving"))
                codeBuilder.AddLine("    if (!OnSaving(changes)) { return false; }");

            codeBuilder.AddLine("    for (var idxChange = 0; idxChange < changes.length; idxChange++) {");
            codeBuilder.AddLine("        var entity = changes[idxChange];");
            codeBuilder.AddLine("        if (typeof entity.OnSaving == 'function') {");
            codeBuilder.AddLine("           if (!entity.OnSaving()) { return false; }");
            codeBuilder.AddLine("        }");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    return true;");
            codeBuilder.AddLine("}");

            codeBuilder.AddLine("var hasValidationErrors = function () {");
            codeBuilder.AddLine("   vm.canReportErrors = true;");
            codeBuilder.AddLine("   return dataContext.hasValidationErrors(" + (uiEntityAdapter.GetTopParent().IsBufferSaving() ? entitiesRef + "()" : "") + ");");
            codeBuilder.AddLine("}");

            #region Save lazing methods
            if (uiEntityAdapter.GetTopParent().IsBufferSaving() && enableSaveLazingMode)
            {
                //fake for save lazing
                #region saveFakeInnerUIs
                codeBuilder.AddLine("var saveFakeInnerUIs = function (transactionID, saveCompleteCallback) {");
                codeBuilder.AddLine("    var vmsForSaving = [];");
                codeBuilder.AddLine("    var saveFakeInnerUI = function (currentIndex) {");
                codeBuilder.AddLine("        if (currentIndex < vmsForSaving.length)");
                codeBuilder.AddLine("            vmsForSaving[currentIndex].dataToolbar.save(false, function () {}, transactionID,  function () {}, function () { currentIndex ++; saveFakeInnerUI(currentIndex); });");
                codeBuilder.AddLine("        else if(saveCompleteCallback) saveCompleteCallback();");
                codeBuilder.AddLine("    };");
                codeBuilder.AddLine("    for (var idx = 0; idx < vm.internalUIs.length; idx++) {");
                codeBuilder.AddLine("        var innerVM = vm[vm.internalUIs[idx]]();");
                codeBuilder.AddLine("        if (innerVM.status() === 'E' && innerVM.getAllChanges().length > 0) vmsForSaving.push(innerVM);");
                codeBuilder.AddLine("    }");
                codeBuilder.AddLine("    saveFakeInnerUI(0);");
                codeBuilder.AddLine("};");
                codeBuilder.AddLine();
                #endregion
                #region getTransactionID
                codeBuilder.AddLine("var getTransactionID = function () {");
                codeBuilder.AddLine("    if (isNullOrEmpty(vm.transactionID))");
                codeBuilder.AddLine("        vm.transactionID = dataContext.getNewGuid();");
                codeBuilder.AddLine("    return vm.transactionID;");
                codeBuilder.AddLine("}");
                codeBuilder.AddLine();
                #endregion
                #region getViewMapInfo
                codeBuilder.AddLine("var getViewMapInfo = function () {");
                codeBuilder.AddLine("   if (!isChildVM() || isNullOrEmpty(parentVM)) return '';");
                codeBuilder.AddLine("   return 'ViewNameParent:'+parentVM.__moduleId__+");
                codeBuilder.AddLine("       ';EntityNameParent:' + parentEntityRelated.typeName + ';FieldsParent:' + uiSettings.parentFieldsRelation.join(',') +");
                codeBuilder.AddLine("       ';Fields:'+uiSettings.detailFieldsRelation.join(',')+';'");
                codeBuilder.AddLine("}");
                codeBuilder.AddLine();
                #endregion
                #region saveFake
                codeBuilder.AddLine("var saveFake = function (transactionID, externalSaveSucceeded, saveCompleteCallback, internalUiCallback) {");
                codeBuilder.AddLine("    return dataContext.saveChangesFake(transactionID, saveSucceeded)");
                codeBuilder.AddLine("    function saveSucceeded(saveResult) {");
                codeBuilder.AddLine("        vm.closeProcessing();");
                codeBuilder.AddLine("        saveFakeInnerUIs(transactionID, function () { if (typeof saveCompleteCallback === 'function') { saveCompleteCallback(); } });");
                codeBuilder.AddLine("        if (typeof externalSaveSucceeded === 'function' && parentVM == null && vm.internalUIs.length > 0) externalSaveSucceeded();");
                codeBuilder.AddLine("        if (typeof internalUiCallback === 'function') internalUiCallback();");
                codeBuilder.AddLine("    }");
                codeBuilder.AddLine("}");
                codeBuilder.AddLine();
                #endregion
                #region submitAllChanges
                codeBuilder.AddLine("var submitAllChanges = function (saveFailed) {");
                codeBuilder.AddLine("    var transactionId = getTransactionID();");
                codeBuilder.AddLine("    vm.showProcessing('Salvando informações...');");
                codeBuilder.AddLine("    isSaving(true);");
                codeBuilder.AddLine("    return dataContext.submitAllChanges(transactionId, saveSucceeded, failed, completed)");
                codeBuilder.AddLine("    function saveSucceeded(saveResult) {");
                codeBuilder.AddLine("        vm.saveSuccessInnerUIs(saveResult, completed);");
                codeBuilder.AddLine("    }");
                codeBuilder.AddLine("    function completed(){");
                codeBuilder.AddLine("        vm.canReportErrors = false;");
                codeBuilder.AddLine("        vm.closeProcessing();");
                codeBuilder.AddLine("        isSaving(false);");
                codeBuilder.AddLine("    }");
                codeBuilder.AddLine("    function failed(error) {");
                codeBuilder.AddLine("        if(typeof completed === 'function') completed();");
                codeBuilder.AddLine("        showModalAlert('Houve uma falha ao salvar a transação.' , [common.getExceptionDescription(error, ['Exception has been thrown by the target of an invocation.<br/>   ', 'Fail by saving data:'])]);");
                codeBuilder.AddLine("        dataContext.cancelAllChanges(transactionId, function(success){");
                codeBuilder.AddLine("           if (saveFailed) saveFailed();");
                codeBuilder.AddLine("        }, function(error){");
                codeBuilder.AddLine("            throw error;");
                codeBuilder.AddLine("        });");
                codeBuilder.AddLine("    }");
                codeBuilder.AddLine("}");
                codeBuilder.AddLine();
                #endregion
                #region saveSuccessInnerUIs
                codeBuilder.AddLine("var saveSuccessInnerUIs = function (saveResults, completed) {");
                codeBuilder.AddLine("    var saveResult = saveResults[vm.__moduleId__];");
                codeBuilder.AddLine("    if(saveResult) {");
                codeBuilder.AddLine("       saveSucceeded(saveResult);");
                codeBuilder.AddLine("    }");
                codeBuilder.AddLine("    for (var idx = 0; idx < vm.internalUIs.length; idx++) { var innerVM = vm[vm.internalUIs[idx]](); innerVM.saveSuccessInnerUIs(saveResults); }");
                codeBuilder.AddLine("    if(typeof completed === 'function') completed();");
                codeBuilder.AddLine("}");
                codeBuilder.AddLine();
                #endregion
            }
            #endregion

            #region Save
            codeBuilder.AddLine("var save = function (isExclusion, externalSaveSucceeded, transactionId, saveCompleteCallback, internalUiCallback) {");
            codeBuilder.AddLine("    if (typeof isExclusion !== 'boolean') isExclusion = false;");
            codeBuilder.AddLine("    if (isExclusion) { enableDataTrack(false, false); }");
            if (ui.ExistsClientEvent("OnToolbarAction"))
                codeBuilder.AddLine("    if (!isExclusion && !OnToolbarAction('Save')) return;");
            codeBuilder.AddLine("    var indexForUndoAction = currentDataIndex();");
            codeBuilder.AddLine("    if (isExclusion) { removeItem(); }");
            codeBuilder.AddLine("    commitInternalUIsData();");
            codeBuilder.AddLine("    dataBind('', true);");
            codeBuilder.AddLine("    vm.changes = getAllChanges();");

            codeBuilder.AddLine("    if (!onSavingValidation(vm.changes)) { if (isExclusion) return undo(indexForUndoAction); else return; }");

            codeBuilder.AddLine("    if (hasInternalUIsSavingErrors()) { if (isExclusion) return undo(indexForUndoAction); else return; }");


            if (ui.HasCustomization)
            {
                codeBuilder.AddLine("    var e = { cancel: false, viewModel: vm };");
                codeBuilder.AddLine("    custom.beforeSaving(e);");
                codeBuilder.AddLine("    if (e.cancel) { if (isExclusion) return undo(indexForUndoAction); else return; }");
            }
            codeBuilder.AddLine("    if (hasInternalUIsValidationErrors() || hasValidationErrors()) { if (isExclusion) return undo(indexForUndoAction); else { refreshToolbar(); return dataBind(); } }");
            //codeBuilder.AddLine("    isBusy(true);");
            codeBuilder.AddLine("    isSaving(true);");
            codeBuilder.AddLine("    if (!isExclusion && currentDataItem() && currentDataItem().checkForSendingAllRowsToServer) { currentDataItem().checkForSendingAllRowsToServer(); }");
            codeBuilder.AddLine("    vm.showProcessing('Salvando informações...');");

            if (uiEntityAdapter.GetTopParent().IsBufferSaving() && enableSaveLazingMode)
            {
                codeBuilder.AddLine("    if (isNullOrEmpty(transactionId) && parentVM == null && vm.internalUIs.length > 0){");
                codeBuilder.AddLine("        transactionId = getTransactionID();");
                codeBuilder.AddLine("        saveCompleteCallback = function(){ if(!isChildVM()) dataToolbar.submitAllChanges(saveFailed); }");
                codeBuilder.AddLine("    }");
                codeBuilder.AddLine("    if (!isNullOrEmpty(transactionId)){");
                codeBuilder.AddLine("        try{ dataToolbar.saveFake(transactionId, externalSaveSucceeded, saveCompleteCallback, internalUiCallback); }");
                codeBuilder.AddLine("        catch(e) { showModalAlert('Houve uma falha ao salvar as informações.', [e.message]); }");
                codeBuilder.AddLine("        return;");
                codeBuilder.AddLine("    }");
            }

            codeBuilder.AddLine("    return dataContext.saveChanges(saveSucceeded, saveFailed, complete, " + uiEntityAdapter.GetTopParent().IsBufferSaving().ToString().ToLower() + ");");
            codeBuilder.AddLine();
            codeBuilder.AddLine("    function complete() {");
            codeBuilder.AddLine("        vm.canReportErrors = false;");
            codeBuilder.AddLine("        vm.closeProcessing();");
            codeBuilder.AddLine("        isSaving(false);");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine();
            codeBuilder.AddLine("    function saveFailed(error) {");
            codeBuilder.AddLine("        if (isChildVM()) parentVM.dataToolbar.edit(true);");
            codeBuilder.AddLine("        if (isExclusion) return undo(indexForUndoAction); else return dataBind();");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine();
            #region SaveSucceeded
            codeBuilder.AddLine("    function saveSucceeded(saveResult) {");
            codeBuilder.AddLine("        dataForUndo = [];");
            if (uiEntityAdapter.GetTopParent().IsBufferSaving())
            {
                codeBuilder.AddLine("        dataCache = []; //Initialize data cache");
                codeBuilder.AddLine("        removedEntities = []; //Initialize removeds");
                codeBuilder.AddLine("        var toList = " + entitiesRef + "();");
                codeBuilder.AddLine("        var fromList = saveResult;");
                codeBuilder.AddLine("        for (var idxElem = toList.length - 1; idxElem >= 0; idxElem--) {");
                codeBuilder.AddLine("           if (toList[idxElem].ChangeState === 'D') toList.splice(idxElem, 1);");
                codeBuilder.AddLine("        }");
                codeBuilder.AddLine("        for (var idxElem = toList.length - 1; idxElem >= 0; idxElem--) {");

                codeBuilder.AddLine("                if (toList[idxElem].ChangeState !== 'N') {");

                var tmpKey = uiEntityAdapter.GetTemporaryKey();
                if (!tmpKey.IsNull())
                {
                    codeBuilder.AddLine("                   var fromObj = [];");
                    codeBuilder.AddLine("                   if (toList[idxElem].ChangeState == 'I') {");
                    codeBuilder.AddLine("                       fromObj = _.where(fromList, { Temporary" + tmpKey.Name + ": toList[idxElem]['" + tmpKey.Name + "'] });");
                    codeBuilder.AddLine("                   } else {");
                    codeBuilder.AddLine("                       fromObj = _.where(fromList, { " + String.Join(",", uiEntityAdapter.GetPrimaryKeys().Select(e => e + ": toList[idxElem]['" + e + "']")) + " });");
                    codeBuilder.AddLine("                   }");
                }
                else codeBuilder.AddLine("                           var fromObj = _.where(fromList, { " + String.Join(",", uiEntityAdapter.GetPrimaryKeys().Select(e => e + ": toList[idxElem]['" + e + "']")) + " });");

                codeBuilder.AddLine("                   if (fromObj.length > 0) { toList[idxElem].copyDataFrom(fromObj[0], true); }");

                codeBuilder.AddLine("                }");

                codeBuilder.AddLine("        }");

            }

            codeBuilder.AddLine("        if (" + entitiesRef + "().length === 0 && !isChildVM()) return clear();");

            codeBuilder.AddLine("        if (" + entitiesRef + "().length > 0) goToIndex(currentDataIndex());");


            codeBuilder.AddLine("        for (var idxChange = 0; idxChange < vm.changes.length; idxChange++) {");
            codeBuilder.AddLine("            var entity = vm.changes[idxChange];");
            codeBuilder.AddLine("            if (entity.isUnchanged() && !isNullOrEmpty(getAbsoluteValue(entity.TableMedia))) { setAbsoluteValue(entity, 'TableMedia', null); entity.setUnchanged(); }");

            codeBuilder.AddLine("            if (typeof entity.OnSaved == 'function') {");
            codeBuilder.AddLine("               entity.OnSaved();");
            codeBuilder.AddLine("            }");
            codeBuilder.AddLine("        }");

            codeBuilder.AddLine("        //if (isChildVM())");
            codeBuilder.AddLine("        //{");
            codeBuilder.AddLine("        //   dataContext.clearAll();");
            codeBuilder.AddLine("        //   query(false, parentEntityRelated);");
            codeBuilder.AddLine("        //}");


            codeBuilder.AddLine("        lastStatus = 'Q';");
            codeBuilder.AddLine("        status('Q');");
            codeBuilder.AddLine("        refreshToolbar();");

            if (ui.HasCustomization)
                codeBuilder.AddLine("        custom.afterSaving({ viewModel: vm });");

            if (ui.ExistsClientEvent("OnSaved"))
                codeBuilder.AddLine("        OnSaved(vm.changes);");

            codeBuilder.AddLine("        if (typeof externalSaveSucceeded == 'function') {");
            codeBuilder.AddLine("            externalSaveSucceeded();");
            codeBuilder.AddLine("        }");

            codeBuilder.AddLine("        dataBind();");
            codeBuilder.AddLine("        resizeToolbar();");

            if (uiEntityAdapter.RequeryAfterSave)
            {
                codeBuilder.AddLine("        if (!isChildVM()) currentDataItem().refreshData();");
            }
            else if (uiEntityAdapter.RequeryDetailsAfterSave)
            {
                codeBuilder.AddLine("        if (!isChildVM()) currentDataItem().fillDetails(true, '');");
            }
            else
            {
                foreach (var detail in uiEntityAdapter.SourceEntityAdapters.Where(e => e.RequeryAfterSave))
                {
                    codeBuilder.AddLine("        if (!isChildVM()) currentDataItem().fillDetails(true, '" + detail.Name + "');");
                }
            }

            codeBuilder.AddLine("    }");

            #endregion
            #endregion
            #endregion
            #region undo
            codeBuilder.AddLine("var dataForUndo = [];");
            codeBuilder.AddLine("var undo = function (indexForUndoAction) {");
            codeBuilder.AddLine("    vm.canReportErrors = false;");

            if (ui.ExistsClientEvent("OnToolbarAction"))
                codeBuilder.AddLine("    if (!OnToolbarAction('Undo')) return;");

            if (ui.ExistsClientEvent("OnCancelling"))
                codeBuilder.AddLine("    if (!OnCancelling()) return;");

            if (ui.HasCustomization)
            {
                codeBuilder.AddLine("    var e = { cancel: false, viewModel: vm };");
                codeBuilder.AddLine("    custom.beforeCancelEdition(e);");
                codeBuilder.AddLine("    if (e.cancel) return;");
            }
            if (uiEntityAdapter.GetTopParent().IsBufferSaving())
                codeBuilder.AddLine("    dataContext.cancelChanges(dataForUndo);");
            else
                codeBuilder.AddLine("    dataContext.cancelChanges();");
            codeBuilder.AddLine("    if ((typeof indexForUndoAction) === 'number' && !navigationByPage() && !isChildVM()) lastStatus = 'Q';");
            codeBuilder.AddLine("    if (lastStatus === 'C' || dataForUndo.length == 0) {");
            codeBuilder.AddLine("        clear();");
            codeBuilder.AddLine("    } else {");

            codeBuilder.AddLine("        " + entitiesRef + "(dataForUndo);");

            codeBuilder.AddLine("        dataForUndo = [];");
            codeBuilder.AddLine("        hideButtonsEditorTemplate();");
            if (uiEntityAdapter.GetTopParent().IsBufferSaving())
            {
                codeBuilder.AddLine("        dataCache = []; //Initialize data cache");
                codeBuilder.AddLine("        removedEntities = []; //Initialize removeds");
            }
            codeBuilder.AddLine("        status(lastStatus);");

            if (uiEntityAdapter.IsBufferSaving())
            {
                codeBuilder.AddLine("        var parentList = " + entitiesRef + "();");
                codeBuilder.AddLine("        for (var idx = 0; idx < parentList.length; idx++) {");
                codeBuilder.AddLine("            if (['U', 'I', 'D'].indexOf(parentList[idx].ChangeState) >= 0) { parentList[idx].restoreOriginal(); parentList[idx].adjustDetailsLoaded(false); }");
                codeBuilder.AddLine("        }");
            }

            codeBuilder.AddLine("        goToIndex(((typeof indexForUndoAction) === 'number' ? indexForUndoAction : currentDataIndex()));");

            if (ui.HasCustomization)
                codeBuilder.AddLine("        custom.afterCancelEdition({ viewModel: vm });");
            codeBuilder.AddLine("        dataBind();");
            codeBuilder.AddLine("        undoInnerUIs();");
            if (ui.ExistsClientEvent("OnCancelled"))
                codeBuilder.AddLine("        OnCancelled();");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("};");
            #endregion
            #region show/hide buttons editor template
            codeBuilder.AddLine("var hideButtonsEditorTemplate = function () {");
            codeBuilder.AddLine("   if ($('.addReg').is(':visible')) {");
            codeBuilder.AddLine("       $('.addReg :visible').each(function (index) {");
            codeBuilder.AddLine("           $('.addReg').hide();");
            codeBuilder.AddLine("           $('.delReg').hide();");
            codeBuilder.AddLine("       });");
            codeBuilder.AddLine("   }");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var showButtonsEditorTemplate = function () {");
            codeBuilder.AddLine("   if ($('.toolbar-dialog-template').is(':visible')) {");
            codeBuilder.AddLine("       $('.toolbar-dialog-template :visible').parent().find('button.addReg').show();");
            codeBuilder.AddLine("       $('.toolbar-dialog-template :visible').parent().find('button.delReg').show();");
            codeBuilder.AddLine("   }");
            codeBuilder.AddLine("};");
            #endregion
            #region print
            codeBuilder.AddLine("var print = function () {");
            if (ui.ExistsClientEvent("OnToolbarAction"))
                codeBuilder.AddLine("    if (!OnToolbarAction('Report')) return;");
            if (ui.ExistsClientEvent("OnPrinting"))
                codeBuilder.AddLine("    if (!OnPrinting()) return false;");
            if (ui.HasCustomization)
            {
                codeBuilder.AddLine("    var e = { cancel: false, viewModel: vm };");
                codeBuilder.AddLine("    custom.beforePrinting(e);");
                codeBuilder.AddLine("    if (e.cancel) return false;");
            }

            if (ui.HasCustomization)
                codeBuilder.AddLine("    custom.afterPrinting({ viewModel: vm });");
            if (ui.ExistsClientEvent("OnPrinted"))
                codeBuilder.AddLine("    OnPrinted();");
            codeBuilder.AddLine("    return true;");
            codeBuilder.AddLine("};");
            #endregion
            #region helper
            codeBuilder.AddLine("var helper = function () {");
            string helpTags = "\"" + ui.HelpTags.Replace(",", "\",\"") + "\"";
            codeBuilder.AddLine("    linxHelper(vm.status(), vm.viewName, vm.rootDataTypeName, '" + helpTags + "');");
            codeBuilder.AddLine("};");
            #endregion
            #region acceptChanges
            codeBuilder.AddLine("var acceptChanges = function () {");
            codeBuilder.AddLine("    if (!navigationByPage() && !isChildVM()) dataContext.acceptChanges();");
            codeBuilder.AddLine("};");
            #endregion
            #region edit
            codeBuilder.AddLine("var edit = function (noClearInnerUIs) {");
            codeBuilder.AddLine("    if (status() === 'E') { refreshToolbar(); return; }");

            if (ui.ExistsClientEvent("OnToolbarAction"))
                codeBuilder.AddLine("    if (!OnToolbarAction('Edit')) return;");
            codeBuilder.AddLine("    if (!canAddChangeEntity()) return;");
            codeBuilder.AddLine("    acceptChanges();");
            if (ui.ExistsClientEvent("OnEditing"))
                codeBuilder.AddLine("    if (!OnEditing()) return;");
            if (ui.HasCustomization)
            {
                codeBuilder.AddLine("    var e = { cancel: false, viewModel: vm };");
                codeBuilder.AddLine("    custom.beforeEditing(e);");
                codeBuilder.AddLine("    if (e.cancel) return;");
            }
            codeBuilder.AddLine("    lastStatus = status();");
            codeBuilder.AddLine("    status('E');");
            codeBuilder.AddLine("    if (!noClearInnerUIs) clearInnerUIs();");
            codeBuilder.AddLine("    goToIndex(currentDataIndex());");
            codeBuilder.AddLine("    if (lastStatus === 'Q') dataForUndo = [].concat(" + entitiesRef + "());");
            codeBuilder.AddLine("    //Enabling data track");
            codeBuilder.AddLine("    enableDataTrack(navigationByPage() || isChildVM(), true);");
            if (ui.HasCustomization)
                codeBuilder.AddLine("    custom.afterEditing({ viewModel: vm });");
            if (ui.ExistsClientEvent("OnEdited"))
                codeBuilder.AddLine("    OnEdited();");
            codeBuilder.AddLine("    editInnerUIs();");
            codeBuilder.AddLine("    showButtonsEditorTemplate();");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var enableDataTrack = function (all, convertDetails) {");

            codeBuilder.AddLine("    adjustFormView();");
            if (!uiEntityAdapter.IsBufferSaving())
            {
                codeBuilder.AddLine("    if (!all) {");
                codeBuilder.AddLine("       if (!isNullOrEmpty(currentDataItem()) && currentDataItem().isPOCO) {");
                codeBuilder.AddLine("           " + entitiesRef + "()[currentDataIndex()] = dataContext.createEntity(currentDataItem().typeName, currentDataItem().getPrimitiveDTO(), true);");
                codeBuilder.AddLine("           if (convertDetails) { currentDataItem().enableDetailsDataTack(" + entitiesRef + "()[currentDataIndex()]); }");
                codeBuilder.AddLine("       }");
                codeBuilder.AddLine("    } else {");
                codeBuilder.AddLine("       for (var idx = 0; idx < " + entitiesRef + "().length; idx++) {");
                codeBuilder.AddLine("           var entity = " + entitiesRef + "()[idx];");
                codeBuilder.AddLine("           if (entity.isPOCO)  {");
                codeBuilder.AddLine("               " + entitiesRef + "()[idx] = dataContext.createEntity(entity.typeName, entity.getPrimitiveDTO(), true);");
                codeBuilder.AddLine("               if (convertDetails) entity.enableDetailsDataTack(" + entitiesRef + "()[idx]);");
                codeBuilder.AddLine("           }");
                codeBuilder.AddLine("       }");
                codeBuilder.AddLine("    }");
                codeBuilder.AddLine("    if (" + entitiesRef + "().length > 0) currentDataItem(" + entitiesRef + "()[currentDataIndex()]);");
                codeBuilder.AddLine("    dataBind();");
            }
            codeBuilder.AddLine("};");

            #endregion
            #region setBandeiraRede
            codeBuilder.AddLine("var setBandeiraRede = function () {");
            if (hasBrand)
            {
                codeBuilder.AddLine("   " + (ui.QueryOnLoad ? "if (!queryLoaded) { loadDataView(); }" : ""));
                codeBuilder.AddLine("   if (getBandeiraRede() > 0) dataContext.loadParameters();");
            }
            codeBuilder.AddLine("};");
            #endregion

            foreach (var entity in uiEntityAdapter.GetCompleteHierarchy())
            {
                #region create
                codeBuilder.AddLine();
                codeBuilder.AddLine("var create" + entity.Name + " = function(" + (entity.TargetEntityAdapter == null ? "" : "parent, noCurrent") + ") {");

                codeBuilder.AddLine("    dataBind('" + (entity.TargetEntityAdapter == null ? "dataView" : entity.Name + "List") + "', true);");
                codeBuilder.AddLine("    var entity = dataContext.create" + entity.Name + "(" + (entity.TargetEntityAdapter == null ? "" : "parent, noCurrent") + ");");
                codeBuilder.AddLine("    if(!entity) return null;");


                if (entity.Name == ui.EntityAdapter.Name)
                    codeBuilder.AddLine("    adjustExternalParentRelation(entity);");

                codeBuilder.AddLine("    entity.setBandeiraRede(getBandeiraRede());");
                codeBuilder.AddLine("    entity.setGpecon(getGpecon());");

                if (entity.ExistsClientEvent("OnAdding"))
                {
                    codeBuilder.AddLine("    if (typeof entity.OnAdding == 'function') {");
                    codeBuilder.AddLine("        if (!entity.OnAdding()) { dataContext.deleteEntity(entity); return; }");
                    codeBuilder.AddLine("    }");
                }

                if (entity.TargetEntityAdapter == null)
                {
                    codeBuilder.AddLine("    " + entitiesRef + ".push(entity);");
                }

                if (entity.ExistsClientEvent("OnAdded"))
                {
                    codeBuilder.AddLine("    if (typeof entity.OnAdded == 'function') {");
                    codeBuilder.AddLine("        entity.OnAdded();");
                    codeBuilder.AddLine("    }");
                }

                if (entity.TargetEntityAdapter != null)
                    codeBuilder.AddLine("   if ((noCurrent !== true) && !isNullOrEmpty(parent)) { parent.current" + entity.Name + "(entity); entity.fillDetails(); } ");

                codeBuilder.AddLine("    return entity;");
                codeBuilder.AddLine("};");
                #endregion
                #region createAndNotify
                codeBuilder.AddLine();
                codeBuilder.AddLine("var createAndNotify" + entity.Name + " = function(" + (entity.TargetEntityAdapter == null ? "" : "parent") + ") {");
                if (ui.HasCustomization && entity.TargetEntityAdapter != null)
                {
                    codeBuilder.AddLine("    var e = { cancel: false, entityTypeName: '" + entity.Name + "', viewModel: vm };");
                    codeBuilder.AddLine("    custom.beforeAddingChild(e);");
                    codeBuilder.AddLine("    if (e.cancel) return;");
                }
                codeBuilder.AddLine("    var entity = create" + entity.Name + "(" + (entity.TargetEntityAdapter == null ? "" : "parent") + ");");
                if (ui.HasCustomization && entity.TargetEntityAdapter != null)
                    codeBuilder.AddLine("    custom.afterAddingChild({ entityTypeName: '" + entity.Name + "', viewModel: vm });");
                codeBuilder.AddLine("    notifyPresentation('" + (entity.TargetEntityAdapter == null ? "" : entity.Name + "List") + "');");
                codeBuilder.AddLine("    return entity;");
                codeBuilder.AddLine("};");
                #endregion
            }

            #region Notify Presentation
            codeBuilder.AddLine("var notifyPresentation = function(dataSourceName) {");
            codeBuilder.AddLine("      return dataContext.notifyPresentation(dataSourceName);");
            codeBuilder.AddLine("};");

            generateNotifyInnerElements(ui, codeBuilder, true);

            #endregion
            #region createEntity
            codeBuilder.AddLine("var createEntity = function(entityName, initialValues) {");
            codeBuilder.AddLine("    var entity = dataContext.createEntity(entityName, initialValues);");
            codeBuilder.AddLine("    entity.setBandeiraRede(getBandeiraRede());");
            codeBuilder.AddLine("    entity.setGpecon(getGpecon());");
            codeBuilder.AddLine("    return entity;");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var getBandeiraRede = function() {");
            codeBuilder.AddLine("    if (uiSettings != null && uiSettings.lookupInfo && uiSettings.lookupInfo.vm && (typeof uiSettings.lookupInfo.vm.getBandeiraRede === 'function')) return uiSettings.lookupInfo.vm.getBandeiraRede();");
            codeBuilder.AddLine("    else if (parentVM != null && (typeof parentVM.getBandeiraRede === 'function')) return parentVM.getBandeiraRede();");
            codeBuilder.AddLine("    else if (uiSettings != null && uiSettings.parentUI && uiSettings.parentUI.vm && (typeof uiSettings.parentUI.vm.getBandeiraRede === 'function')) return uiSettings.parentUI.vm.getBandeiraRede();");
            codeBuilder.AddLine("    else if (!isNullOrEmpty(vm.currentBrands()) && vm.currentBrands().indexOf(',') === -1) return parseInt(vm.currentBrands());");
            codeBuilder.AddLine("    else return 0;");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var getCurrentBrands = function() {");
            codeBuilder.AddLine("    if (uiSettings != null && uiSettings.lookupInfo && uiSettings.lookupInfo.vm  && uiSettings.lookupInfo.vm.hasBrand && (typeof uiSettings.lookupInfo.vm.getCurrentBrands === 'function')) return uiSettings.lookupInfo.vm.getCurrentBrands();");
            codeBuilder.AddLine("    else if (parentVM != null && parentVM.hasBrand && (typeof parentVM.getCurrentBrands === 'function')) return parentVM.getCurrentBrands();");
            codeBuilder.AddLine("    else if (uiSettings != null && uiSettings.parentUI && uiSettings.parentUI.vm  && uiSettings.parentUI.vm.hasBrand && (typeof uiSettings.parentUI.vm.getCurrentBrands === 'function')) return uiSettings.parentUI.vm.getCurrentBrands();");
            codeBuilder.AddLine("    else return (isNullOrEmpty(vm.currentBrands()) ? '0' : vm.currentBrands());");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var showProcessing = function(message) {");
            codeBuilder.AddLine("    currentActivityInformation(message);");
            codeBuilder.AddLine("    isBusy(true);");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var closeProcessing = function() {");
            codeBuilder.AddLine("    currentActivityInformation('');");
            codeBuilder.AddLine("    isBusy(false);");
            codeBuilder.AddLine("};");


            codeBuilder.AddLine("var getGpecon = function() {");
            codeBuilder.AddLine("    if (!isNullOrEmpty(managerAuth.loginInfo.IdLinxGrupoEconomico)) return parseInt(managerAuth.loginInfo.IdLinxGrupoEconomico);");
            codeBuilder.AddLine("    else return 0;");
            codeBuilder.AddLine("};");
            #endregion
            #region deleteEntity
            codeBuilder.AddLine("var deleteEntity = function (entity, isMultiSelection) {");

            if (ui.HasCustomization)
            {
                codeBuilder.AddLine("    var e = { cancel: false, entityTypeName: entity.typeName, viewModel: vm };");
                codeBuilder.AddLine("    custom.beforeRemovingChild(e);");
                codeBuilder.AddLine("    if (e.cancel) return false;");
            }

            codeBuilder.AddLine("    var selectedEntities = []");
            codeBuilder.AddLine("    if (isMultiSelection && !isNullOrEmpty(complement) && (typeof complement.selectedItems === 'function'))");
            codeBuilder.AddLine("        selectedEntities = complement.selectedCurrentItems(false, true);");

            codeBuilder.AddLine("    if (selectedEntities.length > 0) {");
            codeBuilder.AddLine("       for (var idx = 0; idx < selectedEntities.length; idx++) {");
            codeBuilder.AddLine("           var selectedEntity = selectedEntities[idx];");
            codeBuilder.AddLine("           if (typeof selectedEntity.OnDeleting == 'function') {");
            codeBuilder.AddLine("               if (!selectedEntity.OnDeleting()) return false;");
            codeBuilder.AddLine("           }");
            codeBuilder.AddLine("           removeInnerDataUIs(selectedEntity);");
            codeBuilder.AddLine("           dataContext.deleteEntity(selectedEntity);");
            codeBuilder.AddLine("           if (selectedEntity.typeName == vm.rootDataTypeName) {");
            if (uiEntityAdapter.GetTopParent().IsBufferSaving())
                codeBuilder.AddLine("               if (selectedEntity.ChangeState == 'D') removedEntities.push(selectedEntity);");
            codeBuilder.AddLine("               dataView.remove(selectedEntity);");
            if (uiEntityAdapter.GetTopParent().IsBufferSaving())
                codeBuilder.AddLine("               dataCache.removeItem(selectedEntity);");
            codeBuilder.AddLine("           }");
            codeBuilder.AddLine("           if (typeof selectedEntity.OnDeleted == 'function') {");
            codeBuilder.AddLine("               selectedEntity.OnDeleted();");
            codeBuilder.AddLine("           }");
            if (ui.HasCustomization)
                codeBuilder.AddLine("           custom.afterRemovingChild({ entityTypeName: selectedEntity.typeName, viewModel: vm });");
            codeBuilder.AddLine("       }");
            codeBuilder.AddLine("       if (typeof complement.clearSelectedItems === 'function') complement.clearSelectedItems();");
            codeBuilder.AddLine("           return true;");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    else {");
            codeBuilder.AddLine("       if (typeof entity.OnDeleting == 'function') {");
            codeBuilder.AddLine("           if (!entity.OnDeleting()) return false;");
            codeBuilder.AddLine("       }");
            codeBuilder.AddLine("       removeInnerDataUIs(entity);");
            codeBuilder.AddLine("       dataContext.deleteEntity(entity);");
            codeBuilder.AddLine("       if (typeof entity.OnDeleted == 'function') {");
            codeBuilder.AddLine("           entity.OnDeleted();");
            codeBuilder.AddLine("       }");
            if (ui.HasCustomization)
                codeBuilder.AddLine("       custom.afterRemovingChild({ entityTypeName: entity.typeName, viewModel: vm });");

            if (uiEntityAdapter.GetTopParent().IsBufferSaving())
            {
                codeBuilder.AddLine("       if (entity.typeName === vm.rootDataTypeName && typeof isMultiSelection !== 'undefined') {");
                codeBuilder.AddLine("           if (entity.ChangeState == 'D') removedEntities.push(entity);");
                codeBuilder.AddLine("       }");
            }

            codeBuilder.AddLine("    }");

            codeBuilder.AddLine("    return true;");

            codeBuilder.AddLine("};");
            #endregion
            #region addNew

            codeBuilder.AddLine("var canAddChangeEntity = function () {");
            if (hasBrand)
            {
                codeBuilder.AddLine("   if (getBandeiraRede() === 0) {");
                codeBuilder.AddLine("       app.showMessage('Bandeira/Rede precisa ser selecionada.', 'Alerta', ['Ok']);");
                codeBuilder.AddLine("       return false;");
                codeBuilder.AddLine("   } else {");
                codeBuilder.AddLine("       return true;");
                codeBuilder.AddLine("   }");
            }
            else
                codeBuilder.AddLine("   return true;");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var addNew = function (parentEntity) {");

            codeBuilder.AddLine("    if (!dataContext.dataParameters.isLoaded) {");
            codeBuilder.AddLine("       setTimeout(function () {");
            codeBuilder.AddLine("           addNew(parentEntity);");
            codeBuilder.AddLine("       }, 1000);");
            codeBuilder.AddLine("       return;");
            codeBuilder.AddLine("    }");

            codeBuilder.AddLine("    if (status() === 'Q' && !navigationByPage() && !isChildVM()) clear();");

            codeBuilder.AddLine("    if (parentEntity != null && (typeof parentEntity === 'object') && !isNullOrEmpty(parentEntity.typeName))");
            codeBuilder.AddLine("       parentEntityRelated = parentEntity;");
            if (ui.ExistsClientEvent("OnToolbarAction"))
                codeBuilder.AddLine("    if (!OnToolbarAction('Add')) return;");
            codeBuilder.AddLine("    if (!canAddChangeEntity()) return;");
            codeBuilder.AddLine("    acceptChanges();");
            if (ui.HasCustomization)
            {
                codeBuilder.AddLine("    var e = { cancel: false, viewModel: vm };");
                codeBuilder.AddLine("    custom.beforeAdding(e);");
                codeBuilder.AddLine("    if (e.cancel) return;");
            }
            codeBuilder.AddLine("    if (status() === 'C') {");
            codeBuilder.AddLine("        dataContext.clearAll();");
            codeBuilder.AddLine("        " + entitiesRef + "([]);");
            codeBuilder.AddLine("    }");

            codeBuilder.AddLine("    if (status() === 'Q') {");
            codeBuilder.AddLine("       adjustFormView();");
            codeBuilder.AddLine("       dataForUndo = [].concat(" + entitiesRef + "());");
            codeBuilder.AddLine("       if (navigationByPage()) enableDataTrack(true, true);");
            codeBuilder.AddLine("    }");

            codeBuilder.AddLine("    if (status() !== 'E') {");
            codeBuilder.AddLine("        lastStatus = status();");
            codeBuilder.AddLine("        status('E');");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    goToItem(create" + masterEntityName + "());");
            if (ui.HasCustomization)
                codeBuilder.AddLine("    custom.afterAdding({ viewModel: vm });");
            codeBuilder.AddLine("    editInnerUIs();");
            codeBuilder.AddLine("    showButtonsEditorTemplate();");
            codeBuilder.AddLine("    dataBind();");
            codeBuilder.AddLine("};");
            #endregion
            #region remove
            codeBuilder.AddLine("var remove = function () {");
            if (ui.ExistsClientEvent("OnToolbarAction"))
                codeBuilder.AddLine("    if (!OnToolbarAction('Delete')) return;");
            codeBuilder.AddLine("    acceptChanges();");
            if (ui.HasCustomization)
            {
                codeBuilder.AddLine("    var e = { cancel: false, viewModel: vm };");
                codeBuilder.AddLine("    custom.beforeRemoving(e);");
                codeBuilder.AddLine("    if (e.cancel) return;");
            }
            codeBuilder.AddLine("    app.showMessage('" + "Deseja realmente excluir o registro selecionado?".Translate() + "', '" + "Alerta".Translate() + "', ['Yes', 'No'])");
            codeBuilder.AddLine("        .then(function (selectedOption) {");
            codeBuilder.AddLine("            if (selectedOption === 'Yes') {");
            codeBuilder.AddLine("                if (!navigationByPage() && !isChildVM()) { dataForUndo = [].concat(" + entitiesRef + "()); save(true); } else { removeItem(); }");
            codeBuilder.AddLine("            }");
            codeBuilder.AddLine("            return selectedOption;");
            codeBuilder.AddLine("         });");
            codeBuilder.AddLine("};");
            #endregion
            #region removeItem
            codeBuilder.AddLine("var removeParentRelatedItems = function (parentEntity) {");
            codeBuilder.AddLine("    var removedIdx = []");
            codeBuilder.AddLine("    for (var idx = 0; idx < " + entitiesRef + "().length; idx++) {");
            codeBuilder.AddLine("       var isRelated = true;");
            codeBuilder.AddLine("       if (uiSettings != null && uiSettings.parentFieldsRelation.length == uiSettings.detailFieldsRelation.length) {");
            codeBuilder.AddLine("           for (var j = 0; j < uiSettings.parentFieldsRelation.length; j++) {");
            codeBuilder.AddLine("               if (getAbsoluteValue(" + entitiesRef + "()[idx][uiSettings.detailFieldsRelation[j]]) !== getAbsoluteValue(parentEntity[uiSettings.parentFieldsRelation[j]])) {");
            codeBuilder.AddLine("                   isRelated = false;");
            codeBuilder.AddLine("               }");
            codeBuilder.AddLine("           }");
            codeBuilder.AddLine("       }");
            codeBuilder.AddLine("       if (isRelated) {");
            codeBuilder.AddLine("           deleteEntity(" + entitiesRef + "()[idx]);");
            codeBuilder.AddLine("           removedIdx.push(idx);");
            codeBuilder.AddLine("       }");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    for (var i = removedIdx.length - 1; i >= 0; i--) {");
            codeBuilder.AddLine("       " + entitiesRef + "().splice(removedIdx[i], 1);");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    goToIndex(0);");
            codeBuilder.AddLine("    dataBind();");
            codeBuilder.AddLine("}");
            if (uiEntityAdapter.GetTopParent().IsBufferSaving())
                codeBuilder.AddLine("var removedEntities = [];");
            codeBuilder.AddLine("var removeItem = function () {");
            codeBuilder.AddLine("    if (deleteEntity(currentDataItem()) === false) return false;");
            codeBuilder.AddLine("    var index = " + entitiesRef + ".indexOf(currentDataItem());");

            if (uiEntityAdapter.GetTopParent().IsBufferSaving())
                codeBuilder.AddLine("    if (currentDataItem().ChangeState == 'D') removedEntities.push(currentDataItem());");
            codeBuilder.AddLine("    " + entitiesRef + ".remove(currentDataItem());");

            codeBuilder.AddLine("    if (" + entitiesRef + "().length > 0) {");
            codeBuilder.AddLine("        if (status() !== 'E') {");
            codeBuilder.AddLine("            lastStatus = status();");
            codeBuilder.AddLine("            status('E');");
            codeBuilder.AddLine("        }");
            codeBuilder.AddLine("        if (index > 0) { goToIndex(index-1); }");
            codeBuilder.AddLine("        else { goToIndex(0); }");
            codeBuilder.AddLine("        dataBind();");
            if (ui.HasCustomization)
                codeBuilder.AddLine("        custom.afterRemoving({ viewModel: vm });");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    else {");
            codeBuilder.AddLine("        goToIndex(0);");
            codeBuilder.AddLine("        dataBind();");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("};");
            #endregion
            #region goFirst
            codeBuilder.AddLine("var goFirst = function () {");
            if (ui.ExistsClientEvent("OnToolbarAction"))
                codeBuilder.AddLine("    if (!OnToolbarAction('First')) return;");
            if (ui.HasCustomization)
            {
                codeBuilder.AddLine("    var e = { cancel: false, viewModel: vm };");
                codeBuilder.AddLine("    custom.beforeGoingFirst(e);");
                codeBuilder.AddLine("    if (e.cancel) return;");
            }
            codeBuilder.AddLine("    var item;");
            codeBuilder.AddLine("    if (navigationByPage() || (viewType() === 'Secundary') || (!(pageCount() === 1 || pageSize() === 0 || currentPage() === 0))) {");
            codeBuilder.AddLine("        item = refresh(0, false);");
            codeBuilder.AddLine("    } else {");
            codeBuilder.AddLine("        item = goToIndex(0);");
            codeBuilder.AddLine("    }");
            if (ui.HasCustomization)
                codeBuilder.AddLine("    custom.afterGoingFirst({ viewModel: vm });");
            codeBuilder.AddLine("    return item;");
            codeBuilder.AddLine("};");
            #endregion
            #region goBack
            codeBuilder.AddLine("var goBack = function () {");
            if (ui.ExistsClientEvent("OnToolbarAction"))
                codeBuilder.AddLine("    if (!OnToolbarAction('Back')) return;");
            if (ui.HasCustomization)
            {
                codeBuilder.AddLine("    var e = { cancel: false, viewModel: vm };");
                codeBuilder.AddLine("    custom.beforeGoingPrevious(e);");
                codeBuilder.AddLine("    if (e.cancel) return;");
            }
            codeBuilder.AddLine("    var item;");
            codeBuilder.AddLine("    if (navigationByPage() || (viewType() === 'Secundary') || (!(pageCount() === 1 || pageSize() === 0 || currentPage() === 0) && currentDataIndex() === 0)) {");
            codeBuilder.AddLine("        item = refresh(currentPage()-1, !navigationByPage());");
            codeBuilder.AddLine("    } else {");
            codeBuilder.AddLine("        item = goToIndex(currentDataIndex()-1);");
            codeBuilder.AddLine("    }");
            if (ui.HasCustomization)
                codeBuilder.AddLine("    custom.afterGoingPrevious({ viewModel: vm });");
            codeBuilder.AddLine("    return item;");
            codeBuilder.AddLine("};");
            #endregion
            #region goForward
            codeBuilder.AddLine("var goForward = function () {");
            if (ui.ExistsClientEvent("OnToolbarAction"))
                codeBuilder.AddLine("    if (!OnToolbarAction('Next')) return;");
            if (ui.HasCustomization)
            {
                codeBuilder.AddLine("    var e = { cancel: false, viewModel: vm };");
                codeBuilder.AddLine("    custom.beforeGoingNext(e);");
                codeBuilder.AddLine("    if (e.cancel) return;");
            }
            codeBuilder.AddLine("    var item;");
            codeBuilder.AddLine("    if (navigationByPage() || (viewType() === 'Secundary') || (!(pageCount() === 1 || pageSize() === 0 || currentPage() === (pageCount()-1)) && currentDataIndex() === (" + entitiesRef + "().length-1))) {");
            codeBuilder.AddLine("        item = refresh(currentPage()+1, false);");
            codeBuilder.AddLine("    } else {");
            codeBuilder.AddLine("        item = goToIndex(currentDataIndex()+1);");
            codeBuilder.AddLine("    }");
            if (ui.HasCustomization)
                codeBuilder.AddLine("    custom.afterGoingNext({ viewModel: vm });");
            codeBuilder.AddLine("    return item;");
            codeBuilder.AddLine("};");
            #endregion
            #region goLast
            codeBuilder.AddLine("var goLast = function() {");
            if (ui.ExistsClientEvent("OnToolbarAction"))
                codeBuilder.AddLine("    if (!OnToolbarAction('Last')) return;");
            if (ui.HasCustomization)
            {
                codeBuilder.AddLine("    var e = { cancel: false, viewModel: vm };");
                codeBuilder.AddLine("    custom.beforeGoingLast(e);");
                codeBuilder.AddLine("    if (e.cancel) return;");
            }
            codeBuilder.AddLine("    var item;");
            codeBuilder.AddLine("    if (!navigationByPage() && (viewType() === 'Main') && (pageCount() === 1 || pageSize() === 0 || currentPage() === (pageCount()-1))) {");
            codeBuilder.AddLine("        item = goToIndex(" + entitiesRef + "().length-1);");
            codeBuilder.AddLine("    } else {");
            codeBuilder.AddLine("        item = refresh(pageCount()-1, !navigationByPage() && (viewType() === 'Main'));");
            codeBuilder.AddLine("    }");
            if (ui.HasCustomization)
                codeBuilder.AddLine("    custom.afterGoingLast({ viewModel: vm });");
            codeBuilder.AddLine("    return item;");
            codeBuilder.AddLine("};");
            #endregion

            #region toolbar functions
            codeBuilder.AddLine("//Databar enable control");
            codeBuilder.AddLine("var _canRefreshData = true, _canQuickSearch = true, _canAddNew = " + ui.LayoutDefinition.CanAddNew.ToString().ToLower() + ", _canClear = " + ui.LayoutDefinition.CanClear.ToString().ToLower() + ", _canCustomSearch = " + ui.LayoutDefinition.CanCustomSearch.ToString().ToLower() +
                ", _canDelete = " + ui.LayoutDefinition.CanDelete.ToString().ToLower() + ", _canEdit = " + ui.LayoutDefinition.CanEdit.ToString().ToLower() +
                ", _canLayout = " + ui.LayoutDefinition.CanLayout.ToString().ToLower() + ", _canNavigate = " + ui.LayoutDefinition.CanNavigate.ToString().ToLower() +
                ", _canPrint = " + ui.LayoutDefinition.CanPrint.ToString().ToLower() + ", _canSearch = " + ui.LayoutDefinition.CanSearch.ToString().ToLower() + ", _canExport = " + ui.LayoutDefinition.CanExport.ToString().ToLower() + ", _noBusyLoading = false;");
            codeBuilder.AddLine("var setSecurity = function(pCanAddNew, pCanClear, pCanCustomSearch, pCanDelete, pCanEdit, pCanLayout, pCanNavigate, pCanPrint, pCanSearch, pCanExport, pNoBusyLoading) {");
            codeBuilder.AddLine("   _canAddNew = pCanAddNew;");
            codeBuilder.AddLine("   _canClear = pCanClear;");
            codeBuilder.AddLine("   _canCustomSearch = pCanCustomSearch;");
            codeBuilder.AddLine("   _canDelete = pCanDelete;");
            codeBuilder.AddLine("   _canEdit = pCanEdit;");
            codeBuilder.AddLine("   _canLayout = pCanLayout;");
            codeBuilder.AddLine("   _canNavigate = pCanNavigate;");
            codeBuilder.AddLine("   _canPrint = pCanPrint;");
            codeBuilder.AddLine("   _canSearch = pCanSearch;");
            codeBuilder.AddLine("   _canExport = pCanExport;");
            codeBuilder.AddLine("   _noBusyLoading = pNoBusyLoading");
            codeBuilder.AddLine("   refreshToolbar();");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var refreshToolbar = function() {");
            codeBuilder.AddLine("   status.notifySubscribers();");
            codeBuilder.AddLine("   currentDataItem.notifySubscribers();");
            codeBuilder.AddLine("   canNavigate.notifySubscribers();");
            codeBuilder.AddLine("}");

            codeBuilder.AddLine("var refreshCurrentBind = function() {");
            codeBuilder.AddLine("   currentDataItem.notifySubscribers();");
            codeBuilder.AddLine("}");

            codeBuilder.AddLine("var isReportComposition = function (reportName) {");
            codeBuilder.AddLine("    if (!isNullOrEmpty(reportName))");
            codeBuilder.AddLine("    {");
            codeBuilder.AddLine("        for (var idx in dataContext.entityNames)");
            codeBuilder.AddLine("        {");
            codeBuilder.AddLine("            if (" + (ui.EntityAdapter.IsDashboardFilter ? "" : "dataContext.entityNames[idx].indexOf('ParentComposition') > -1 && ") + "reportName.indexOf(vm.rootNamespace + '.' + dataContext.entityNames[idx]) > -1)");
            codeBuilder.AddLine("                return true;");
            codeBuilder.AddLine("        }");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    return false;");
            codeBuilder.AddLine("}");

            codeBuilder.AddLine("var canGoFirst = ko.computed(function () { return (status() === 'Q' || (status() === 'E' && isChildVM())) && _canNavigate && ((!navigationByPage() && (viewType() === 'Main') && currentRecord() > 0) || ((navigationByPage() || (viewType() === 'Secundary')) && currentPage() > 0)); });");
            codeBuilder.AddLine("var canGoBack = ko.computed(function () { return (status() === 'Q' || (status() === 'E' && isChildVM())) && _canNavigate && ((!navigationByPage() && (viewType() === 'Main') && currentRecord() > 0) || ((navigationByPage() || (viewType() === 'Secundary')) && currentPage() > 0)); });");
            codeBuilder.AddLine("var canGoForward = ko.computed(function () { return (status() === 'Q' || (status() === 'E' && isChildVM())) && _canNavigate && ((!navigationByPage() && (viewType() === 'Main') && currentRecord() < (totalRecords()-1)) || ((navigationByPage() || (viewType() === 'Secundary')) && currentPage() < (pageCount()-1))); });");
            codeBuilder.AddLine("var canGoLast = ko.computed(function () { return (status() === 'Q' || (status() === 'E' && isChildVM())) && _canNavigate && ((!navigationByPage() && (viewType() === 'Main') && currentRecord() < (totalRecords()-1)) || ((navigationByPage() || (viewType() === 'Secundary')) && currentPage() < (pageCount()-1))); });");
            codeBuilder.AddLine("var canClear = ko.computed(function () { return ['C', 'Q'].indexOf(status()) >= 0 && _canClear && !isChildVM(); });");
            codeBuilder.AddLine("var canExport = ko.computed(function () { return (status() === 'Q' || status() === 'C') && _canExport; });");
            codeBuilder.AddLine("var canGridExport = ko.computed(function () { return status() === 'Q' && _canExport; });");
            codeBuilder.AddLine("var canQuery = ko.computed(function () { return status() === 'C' && _canSearch && !isChildVM(); });");
            codeBuilder.AddLine("var canCustomSearch = ko.computed(function () { return status() === 'C' && _canCustomSearch && !isChildVM(); });");
            codeBuilder.AddLine("var canQuickSearch = ko.computed(function () { return " + (hasQuickSearch ? "(status() === 'Q' || status() === 'C') && _canQuickSearch && _canClear && _canSearch && !isChildVM();" : "false;") + " });");
            codeBuilder.AddLine("var hasDataFeed = ko.computed(function () { return status() === 'C' && _canSearch && dataContext.hasDataFeed && parentVM == null && !isChildVM(); });");
            codeBuilder.AddLine("var canAddNew = ko.computed(function () { return " + (uiEntityAdapter.IsOlap() ? "false;" : "((['Q', 'C'].indexOf(status()) >= 0 && !isChildVM()) || (status() === 'E' && (navigationByPage() || isChildVM()))) && _canAddNew;") + " });");
            codeBuilder.AddLine("var canRemove = ko.computed(function () { return " + (uiEntityAdapter.IsOlap() ? "false;" : "(" + entitiesRef + "().length > 0) && ((!navigationByPage() && !isChildVM() && status() === 'Q') || (status() === 'E' && !navigationByPage() && isChildVM())) && _canDelete;") + " });");
            codeBuilder.AddLine("var canEdit = ko.computed(function () { return " + (uiEntityAdapter.IsOlap() ? "false;" : "status() === 'Q' && _canEdit && !isChildVM();") + " });");
            codeBuilder.AddLine("var canRefreshCurrentData = ko.computed(function () { return " + (uiEntityAdapter.HasDynamicPrimaryKey() || uiEntityAdapter.IsOlap() ? "false;" : "status() === 'Q' && _canSearch && _canRefreshData && !isChildVM();") + " });");
            codeBuilder.AddLine("var canUndo = ko.computed(function () { return status() === 'E' && (_canEdit || _canAddNew) && !isChildVM(); });");
            codeBuilder.AddLine("var canNavigate = ko.computed(function () { return  (!canUndo() && !canQuery() && (" + entitiesRef + "().length > 1 || pageCount() > 1) && _canNavigate); });");
            codeBuilder.AddLine("var canPrint = ko.computed(function () { return ['C', 'Q'].indexOf(status()) >= 0 && _canPrint && !isChildVM(); });");

            codeBuilder.AddLine("var canSave = ko.computed(function () {");
            codeBuilder.AddLine("       return !isSaving() && status() === 'E' && (_canEdit || _canAddNew) && !isChildVM();");
            codeBuilder.AddLine("});");

            codeBuilder.AddLine("var enabledForEditing = ko.computed(function () {");
            codeBuilder.AddLine("        return ['E', 'C'].indexOf(status()) >= 0;");
            codeBuilder.AddLine("});");

            codeBuilder.AddLine("var isEditable = function () {");
            codeBuilder.AddLine("    return _canEdit;");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var viewInfo = function () {");
            if (ui.ExistsClientEvent("OnToolbarAction"))
                codeBuilder.AddLine("    if (!OnToolbarAction('TableView')) return;");
            codeBuilder.AddLine("    changeFormView();");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var adjustFormView = function () {");
            codeBuilder.AddLine("    if (!hasMainTopDataGrid() && (status() === 'E' || status() === 'C') && viewType() === 'Secundary') changeFormView();");
            codeBuilder.AddLine("}");

            if (!hasWizard)
            {
                codeBuilder.AddLine("var removeFormViewControl = function () {");
                codeBuilder.AddLine("    var front = $('#" + ui.Name + "_formViewer_front')[0];");
                codeBuilder.AddLine("    if (front) front.removeClassName('front');");
                codeBuilder.AddLine("    var back = $('#" + ui.Name + "_formViewer_back')[0];");
                codeBuilder.AddLine("    if (back) { back.removeClassName('back'); back.addClassName('hide'); }");
                codeBuilder.AddLine("}");
            }

            codeBuilder.AddLine("var changeFormView = function () {");
            if (!hasWizard)
            {
                codeBuilder.AddLine("    if (hasMainTopDataGrid() || isChildVM()) return;");
                codeBuilder.AddLine("    var panel = $('#" + ui.Name + "_formViewer')[0];");
                //Verifica se o "panel" existe para evitar problemas ao utilizar como UI externa e fechar no modo Grid.
                codeBuilder.AddLine("    if (panel) {");
                codeBuilder.AddLine("       if (viewType() === 'Main') panel.addClassName('flip');");
                codeBuilder.AddLine("       else panel.removeClassName('flip');");
                codeBuilder.AddLine("    }");
                codeBuilder.AddLine("    if (viewType() === 'Main') viewType('Secundary');");
                codeBuilder.AddLine("    else viewType('Main');");
                codeBuilder.AddLine("    if (viewType() === 'Secundary') { dataBind('dataView'); } else { dataBind(); queryInnerUIs(currentDataItem()); };");
            }
            codeBuilder.AddLine("}");


            codeBuilder.AddLine("var canViewInfo = ko.computed(function () {");
            codeBuilder.AddLine("    return " + (hasWizard ? "false" : "!hasMainTopDataGrid() && status() !== 'E' && totalRecords() > 0 && !isChildVM()") + ";");
            codeBuilder.AddLine("});");

            codeBuilder.AddLine("var importPhoto = function () {");
            if (ui.ExistsClientEvent("OnToolbarAction"))
                codeBuilder.AddLine("    if (!OnToolbarAction('ImportPhoto')) return;");
            codeBuilder.AddLine("    require(['viewmodels/shared/modalMultimidiaBatch'], function (modalMultimidiaBatch) {");
            codeBuilder.AddLine("        modalMultimidiaBatch.showModal(dataContext).then(function (r, data) { });");
            codeBuilder.AddLine("    });");
            codeBuilder.AddLine("};");



            #endregion

            #region entitySearhRange

            var listPropertyDateTimeTextBoxForFilterRange = new List<string>();
            var listPropertyNumericTextBoxForFilterRange = new List<string>();
            var listPropertyForLookupFilterRange = new List<string>();
            foreach (var ea in uiEntityAdapter.GetCompleteHierarchy())
            {
                foreach (var p in ea.GetAllInheritanceAttributes())
                {
                    var control = ui.LayoutDefinition.GetItemByPredicate<LayoutControlV2>(c => c.BindingPath.EndsWith(((uiEntityAdapter == ea ? "DataView" : ea.Name + "PagedList") + "." + p.Name)) && c.ClassName.InList("DateTimeTextBox", "NumericTextBox", "LookUpTextBox"));
                    if (control.Count() > 0 && control.Any(c => c.ClassName == "NumericTextBox" && c.HasFilterRange))
                        listPropertyNumericTextBoxForFilterRange.Add(ea.Name + p.Name);
                    if (control.Count() > 0 && control.Any(c => c.ClassName == "DateTimeTextBox" && c.HasFilterRange))
                        listPropertyDateTimeTextBoxForFilterRange.Add(ea.Name + p.Name);
                    if (control.Count() > 0 && control.Any(c => c.ClassName == "LookUpTextBox"))
                        listPropertyForLookupFilterRange.Add(ea.Name + p.Name);
                }
            }

            codeBuilder.AddLine();

            codeBuilder.AddLine("var entitySearchRange = {");
            codeBuilder.AddLine("    predefinedFilters: ko.observableArray(managerPredefined.predefinedFilters),");
            codeBuilder.AddLine(string.Join(",\r\n",
                listPropertyDateTimeTextBoxForFilterRange.Select(p => string.Format("        {0}_typeRange: ko.observable('R'), {0}_begin: ko.observable(null), {0}_end: ko.observable(null), {0}_predefFilter: ko.observableArray([]), {0}_predefValue: ko.observable(null)", p))
                .Union(listPropertyNumericTextBoxForFilterRange.Select(p => string.Format("        {0}_begin: ko.observable(null), {0}_end: ko.observable(null)", p)))
                .Union(listPropertyForLookupFilterRange.Select(p => string.Format("        {0}: ko.observable(null)", p)))
                ));
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("entitySearchRange.clear = function(){");
            codeBuilder.AddLine(string.Join("\r\n",
                listPropertyDateTimeTextBoxForFilterRange.Select(p => string.Format("        entitySearchRange.{0}_typeRange('R'); entitySearchRange.{0}_begin(null); entitySearchRange.{0}_end(null); entitySearchRange.{0}_predefFilter([]); entitySearchRange.{0}_predefValue(null);", p))
                .Union(listPropertyNumericTextBoxForFilterRange.Select(p => string.Format("        entitySearchRange.{0}_begin(null); entitySearchRange.{0}_end(null);", p)))
                .Union(listPropertyForLookupFilterRange.Select(p => string.Format("        entitySearchRange.{0}(null);", p)))
                ));

            codeBuilder.AddLine("};");

            foreach (var p in listPropertyNumericTextBoxForFilterRange)
            {
                codeBuilder.AddLine("entitySearchRange.has_[prop] = ko.computed(function(){ return (entitySearchRange.[prop]_begin() != null || entitySearchRange.[prop]_end() != null); });".Replace("[prop]", p));
            }
            foreach (var p in listPropertyDateTimeTextBoxForFilterRange)
            {
                codeBuilder.AddLine("entitySearchRange.has_[prop] = ko.computed(function(){ return (entitySearchRange.[prop]_typeRange() == 'R' && (entitySearchRange.[prop]_begin() != null || entitySearchRange.[prop]_end() != null) || (entitySearchRange.[prop]_typeRange() == 'P' && entitySearchRange.[prop]_predefFilter().length > 0)); });".Replace("[prop]", p));

            }

            #endregion

            #region grid Template
            codeBuilder.AddLine();

            codeBuilder.AddLine("function deleteGrid(element, cName, cDataItem_listItem, isMultiSelect) {");

            codeBuilder.AddLine("   var element = element;");
            codeBuilder.AddLine("   var cName = cName;");
            codeBuilder.AddLine("   var dataItem_ListItem = cDataItem_listItem.split(';');");
            codeBuilder.AddLine("   var currentdataItem = dataItem_ListItem[0];");
            codeBuilder.AddLine("   var currentlistItem = dataItem_ListItem[1];");

            codeBuilder.AddLine("   $(element).igGridUpdating('endEdit');");
            codeBuilder.AddLine("   var selectedRows = [];");
            codeBuilder.AddLine("   var activeRow = $(element).igGrid('activeRow');");

            codeBuilder.AddLine("   if (isMultiSelect) { if ($(element).igGrid('selectedRows').length > 0) selectedRows = $(element).igGrid('selectedRows');");
            codeBuilder.AddLine("   } else { selectedRows.push($(element).igGrid('selectedRow')); }");
            codeBuilder.AddLine("   if (!activeRow) activeRow = selectedRows[0];");
            codeBuilder.AddLine("   if (isNullOrEmpty(selectedRows[0])) {");
            codeBuilder.AddLine("       app.showMessage('Nenhum registro selecionado!', 'Informação', ['Ok']);");
            codeBuilder.AddLine("       return;");
            codeBuilder.AddLine("   }");

            codeBuilder.AddLine("   var entity = findElementByKey(eval(currentlistItem), 'RowDataId', isNullOrEmpty(selectedRows) && selectedRows.length === 0 ? 0 : selectedRows[0].id);");
            codeBuilder.AddLine("   if (isNullOrEmpty(entity)) {");
            codeBuilder.AddLine("       app.showMessage('Nenhum registro selecionado!', 'Informação', ['Ok']);");
            codeBuilder.AddLine("       return;");
            codeBuilder.AddLine("   }");

            codeBuilder.AddLine("   if (deleteEntity(entity, isMultiSelect)) {");
            codeBuilder.AddLine("       if (entity.typeName === vm.rootDataTypeName) {");
            codeBuilder.AddLine("           eval(currentlistItem)['remove'](entity);");
            codeBuilder.AddLine("       }");
            codeBuilder.AddLine("   }");
            codeBuilder.AddLine("   else { return; }");

            codeBuilder.AddLine("    if ($(element).data('igGrid')._totalRowCount > 0) {");
            codeBuilder.AddLine("        for (i = 0; i < selectedRows.length; i++) {");
            codeBuilder.AddLine("           var selectedRow = selectedRows[i];");
            codeBuilder.AddLine("           $(element).igGridUpdating('deleteRow', selectedRow.id);");
            codeBuilder.AddLine("        }");
            codeBuilder.AddLine("    }");
            //Removido para atender ao chamado: Task 45882
            //codeBuilder.AddLine("    var rows = $(element).igGrid('rows');");
            //codeBuilder.AddLine("    if (rows.length > 0) {");
            //codeBuilder.AddLine("        var idx = (activeRow.index > rows.length) ? rows.length : (activeRow.index > 0 ? activeRow.index - 1 : activeRow.index);");
            //codeBuilder.AddLine("        $(element).igGridSelection('selectRow', idx);");
            //codeBuilder.AddLine("        var ui = $(element).data('igGridUpdating');");
            //codeBuilder.AddLine("        selectGridCurrentItem(goToKey, 'RowDataId', ui.grid.activeRow(), eval(currentdataItem), eval(currentlistItem));");
            //codeBuilder.AddLine("    }");

            codeBuilder.AddLine("};");

            codeBuilder.AddLine("function openEditor(element, cName, cDataItem_listItem, dataV_parentName, entityName, isEditorWithinGrid) {");
            codeBuilder.AddLine("   var element = element;");
            codeBuilder.AddLine("   var cName = cName;");
            codeBuilder.AddLine("   var dataItem_ListItem = cDataItem_listItem.split(';');");
            codeBuilder.AddLine("   var dataView_parentName = dataV_parentName.split(';');");
            codeBuilder.AddLine("   var currentdataItem = dataItem_ListItem[0];");
            codeBuilder.AddLine("   var currentlistItem = dataItem_ListItem[1];");
            codeBuilder.AddLine("   var entityName = entityName");
            codeBuilder.AddLine("   var dataView = dataView_parentName[0];");
            codeBuilder.AddLine("   var parentName = dataView_parentName[1];");
            codeBuilder.AddLine("   var ui = $(element).data('igGridUpdating');");
            codeBuilder.AddLine();

            codeBuilder.AddLine("   $('.ui-dialog:has(#' + $('#dialog' + cName + '').attr('id') + ')').empty().remove();");

            codeBuilder.AddLine("   if ($(element).data('igGridGroupBy') !== undefined && $(element).igGridGroupBy('groupByColumns').length !== 0){");
            codeBuilder.AddLine("      app.showMessage('Não é possível habilitar o editor template com campos agrupados!', 'Informação', ['Ok']);");
            codeBuilder.AddLine("      return false;");
            codeBuilder.AddLine("   }");

            codeBuilder.AddLine("   if (getSelectedIndex(element) == -1){");
            codeBuilder.AddLine("      app.showMessage('Registro não selecionado!', 'Informação', ['Ok']);");
            codeBuilder.AddLine("      return false;");
            codeBuilder.AddLine("   }");
            codeBuilder.AddLine();
            codeBuilder.AddLine("   configEditor(element, currentdataItem, currentlistItem);");
            codeBuilder.AddLine();

            codeBuilder.AddLine("   if (vm.status() !== 'E') {");
            codeBuilder.AddLine("       $('#addReg' + cName + '').hide();");
            codeBuilder.AddLine("       $('#delReg' + cName + '').hide();");
            codeBuilder.AddLine("   }");
            codeBuilder.AddLine("   else {");
            codeBuilder.AddLine("       $('#addReg' + cName + '').show();");
            codeBuilder.AddLine("       $('#delReg' + cName + '').show();");
            codeBuilder.AddLine("   }");
            codeBuilder.AddLine();

            codeBuilder.AddLine("   dialogIsOpen = true;");
            codeBuilder.AddLine("   var pk_id = getSelectedIndex(element) + 1;");
            codeBuilder.AddLine("   var ds = ui.grid.dataSource;");
            codeBuilder.AddLine("   var columns = ui.grid.options.columns;");

            codeBuilder.AddLine("   fillLabels(pk_id, element, dataView, cName);");
            codeBuilder.AddLine();

            codeBuilder.AddLine("   $.fn['backReg' + cName + ''] = function () {");
            codeBuilder.AddLine("       if (hasPaging(element).length > 0) {");
            codeBuilder.AddLine("           gridTrData = ui.grid.dataSource.dataView()[getSelectedIndex(element)];");
            codeBuilder.AddLine("           if (getSelectedIndex(element) > 0) {");
            codeBuilder.AddLine("               pk_id = getSelectedIndex(element) - 1;");
            codeBuilder.AddLine("               $(element).igGridSelection('clearSelection');");
            codeBuilder.AddLine("               updateGrid(gridTrData, pk_id, ui, currentdataItem, element);");
            codeBuilder.AddLine("               updateTemplate(pk_id, 1, element, ui, currentdataItem, currentlistItem);");
            codeBuilder.AddLine("               fillLabels(pk_id + 1, element, dataView, cName);");
            codeBuilder.AddLine("           }");
            codeBuilder.AddLine("       }");
            codeBuilder.AddLine("       else{");
            codeBuilder.AddLine("           pk_id = getSelectedIndex(element) + 1;");
            codeBuilder.AddLine("           gridTrData = ui.grid.dataSource.dataView()[pk_id - 1];");
            codeBuilder.AddLine("           updateGrid(gridTrData, pk_id, ui, currentdataItem, element);");
            codeBuilder.AddLine("           $(element).igGridSelection('clearSelection');");
            codeBuilder.AddLine("           if (pk_id > 1) {");
            codeBuilder.AddLine("               updateTemplate(pk_id, 1, element, ui, currentdataItem, currentlistItem);");
            codeBuilder.AddLine("               pk_id = pk_id - 1;");
            codeBuilder.AddLine("           }");
            codeBuilder.AddLine("           else");
            codeBuilder.AddLine("               $(element).igGridSelection('selectRow', pk_id - 1);");
            codeBuilder.AddLine("           fillLabels(pk_id, element, dataView, cName)");
            codeBuilder.AddLine("       }");
            codeBuilder.AddLine("   }");

            codeBuilder.AddLine("   $.fn['nextReg' + cName + ''] = function () {");
            codeBuilder.AddLine("       if (hasPaging(element).length > 0) {");
            codeBuilder.AddLine("           gridTrData = ui.grid.dataSource.dataView()[getSelectedIndex(element)];");
            codeBuilder.AddLine("           pk_id = getSelectedIndex(element) + 1;");
            codeBuilder.AddLine("           if (ui.grid.dataSource.dataView().length > pk_id) {");
            codeBuilder.AddLine("               $(element).igGridSelection('clearSelection');");
            codeBuilder.AddLine("               updateGrid(gridTrData, pk_id, ui, currentdataItem, element);");
            codeBuilder.AddLine("               updateTemplate(pk_id, 2, element, ui, currentdataItem, currentlistItem);");
            codeBuilder.AddLine("               pk_id = pk_id + 1;");
            codeBuilder.AddLine("           }");
            codeBuilder.AddLine("           else");
            codeBuilder.AddLine("               $(element).igGridSelection('selectRow', pk_id - 1);");
            codeBuilder.AddLine("       } else {");
            codeBuilder.AddLine("           pk_id = getSelectedIndex(element) + 1;");
            codeBuilder.AddLine("           gridTrData = ui.grid.dataSource.dataView()[pk_id - 1];");
            codeBuilder.AddLine("           updateGrid(gridTrData, pk_id, ui, currentdataItem, element);");
            codeBuilder.AddLine("           var totalGrid = (Array.isArray(ui.grid.options.dataSource) ? ui.grid.options.dataSource.count() : ui.grid.options.dataSource.data().length);");
            codeBuilder.AddLine("           $(element).igGridSelection('clearSelection');");
            codeBuilder.AddLine("           if (totalGrid > pk_id) {");
            codeBuilder.AddLine("               updateTemplate(pk_id, 2, element, ui, currentdataItem, currentlistItem);");
            codeBuilder.AddLine("               pk_id = pk_id + 1;");
            codeBuilder.AddLine("           }");
            codeBuilder.AddLine("           else");
            codeBuilder.AddLine("               $(element).igGridSelection('selectRow', pk_id - 1);");
            codeBuilder.AddLine("       }");
            codeBuilder.AddLine("      fillLabels(pk_id, element, dataView, cName);");
            codeBuilder.AddLine("   }");

            codeBuilder.AddLine("   $.fn['addReg' + cName + ''] = function () {");
            codeBuilder.AddLine("      var addedEntity = eval('vm.createAndNotify' + entityName);");
            codeBuilder.AddLine("      if (addedEntity) {");
            codeBuilder.AddLine("         var index = 0; var ds = (Array.isArray(ui.grid.options.dataSource) ? ui.grid.options.dataSource : ui.grid.options.dataSource.data());");
            codeBuilder.AddLine("         for (index = 0; index < ds.count(); index++) {");
            codeBuilder.AddLine("            if (addedEntity.RowDataId == ds[index].RowDataId) break;");
            codeBuilder.AddLine("         }");
            codeBuilder.AddLine("         updateFieldsTemplate(addedEntity.RowDataId, currentdataItem, currentlistItem);");
            codeBuilder.AddLine("         fillLabels(index + 1, element, dataView, cName);");
            codeBuilder.AddLine("      }");
            codeBuilder.AddLine("   }");

            codeBuilder.AddLine("   $.fn['delReg' + cName + ''] = function () {");
            codeBuilder.AddLine("       pk_id = getSelectedIndex(element);");
            codeBuilder.AddLine("       gridTrData = ui.grid.dataSource.dataView()[pk_id];");
            codeBuilder.AddLine("       var entity = findElementByKey(eval(currentlistItem), 'RowDataId', gridTrData['RowDataId']);");
            codeBuilder.AddLine("       if (entity) {");
            codeBuilder.AddLine("           removeInnerDataUIs(entity);");
            codeBuilder.AddLine("           if (deleteEntity(entity, false) === false) return false;");
            codeBuilder.AddLine("           $(element).igGridUpdating('deleteRow', gridTrData['RowDataId']);");
            codeBuilder.AddLine("       }");

            codeBuilder.AddLine("       var totalGrid = (Array.isArray(ui.grid.options.dataSource) ? ui.grid.options.dataSource : ui.grid.options.dataSource.data()).length;");
            codeBuilder.AddLine("       if (totalGrid === 0) return restartGrid(element, cName, isEditorWithinGrid);");

            codeBuilder.AddLine("       if (pk_id == totalGrid) {");
            codeBuilder.AddLine("           gridTrData = ui.grid.dataSource.dataView()[totalGrid - 1];");
            codeBuilder.AddLine("           $(element).igGridSelection('selectRow', totalGrid - 1);");
            codeBuilder.AddLine("       }");
            codeBuilder.AddLine("       else {");
            codeBuilder.AddLine("           gridTrData = ui.grid.dataSource.dataView()[pk_id];");
            codeBuilder.AddLine("           $(element).igGridSelection('selectRow', pk_id);");
            codeBuilder.AddLine("       }");

            codeBuilder.AddLine("       updateFieldsTemplate(gridTrData['RowDataId'], currentdataItem, currentlistItem);");
            codeBuilder.AddLine("       fillLabels(pk_id, element, dataView, cName);");
            codeBuilder.AddLine("   }");

            codeBuilder.AddLine("   $.fn['okReg' + cName + ''] = function () {");
            codeBuilder.AddLine("       pk_id = getSelectedIndex(element);");
            codeBuilder.AddLine("       gridTrData = ui.grid.dataSource.dataView()[pk_id];");
            codeBuilder.AddLine("       updateGrid(gridTrData, pk_id, ui, currentdataItem, element);");
            codeBuilder.AddLine("       $(element + '_EditorBtn').attr('title', 'Alterar edição para modo Template');");
            codeBuilder.AddLine("       return restartGrid(element, cName, isEditorWithinGrid);");
            codeBuilder.AddLine("   }");

            codeBuilder.AddLine("   $.fn['clickSelectorGrid'] = function (tb) {");
            codeBuilder.AddLine("       var table = tb[0].offsetParent.id;");
            codeBuilder.AddLine(@"      var removeSpace = $('#' + table).data('param').replace(/\s/g, """");");
            codeBuilder.AddLine("       var param = removeSpace.split(',');");
            codeBuilder.AddLine("       selectorEditorTemplate(param[0], parseInt(tb[0].id), param[1], param[2], param[3], param[4]);");
            codeBuilder.AddLine("   }");

            //begin Ricardo.muniz alteração para trazer os dados preenchidos
            codeBuilder.AddLine("   if(currentdataItem && eval(currentdataItem));");
            codeBuilder.AddLine("       eval(currentdataItem).notifySubscribers();");
            //end

            codeBuilder.AddLine("   if (!isEditorWithinGrid) {");
            codeBuilder.AddLine("       $('#dialog' + cName + '').dialog({");
            codeBuilder.AddLine("           modal: true,");
            codeBuilder.AddLine("           width: '90%',");
            codeBuilder.AddLine("           height: 700,");
            codeBuilder.AddLine("           show: { effect: 'drop', direction: 'up' },");
            codeBuilder.AddLine("           draggable: true,");
            codeBuilder.AddLine("           closeOnEscape: false,");
            codeBuilder.AddLine("           resizable: false,");
            codeBuilder.AddLine("           zIndex: getNew_zIndex()");
            codeBuilder.AddLine("       });");
            codeBuilder.AddLine("       $('.ui-widget-overlay.ui-front').css('z-index', getNew_zIndex() - 1);");
            codeBuilder.AddLine("       $('#dialog' + cName + '').dialog('widget').find('.ui-dialog-titlebar-close').hide();");
            codeBuilder.AddLine("   }");
            codeBuilder.AddLine("   else{");
            codeBuilder.AddLine("       $(element + '_ContentDLG').next().addClass('hide');");
            codeBuilder.AddLine("       $(element + '_container').parent().addClass('hide');");
            codeBuilder.AddLine("       $(element + '_ContentDLG').attr('style', 'position: static;height: 350px;');");
            codeBuilder.AddLine("       $('#dialog' + cName + '').appendTo($(element + '_ContentDLG'));");
            codeBuilder.AddLine("       $('#dialog' + cName + '').show();");
            codeBuilder.AddLine("   }");
            codeBuilder.AddLine();
            codeBuilder.AddLine("   return false;");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("   function updateGrid(grd, pk, ui, currentdataItem, element) {");
            codeBuilder.AddLine("       if (pk >= 0 && eval(currentdataItem + '()') !== null) {");
            codeBuilder.AddLine("           var propUpdate = 0;");
            codeBuilder.AddLine("           var hasChangeProp = false;");
            codeBuilder.AddLine("           var columns = ui.grid.options.columns;");
            codeBuilder.AddLine("           for (i = 1; i < columns.length; ++i) {");
            codeBuilder.AddLine("               if (columns[i].key.indexOf('Multi') < 0) {");
            codeBuilder.AddLine("                   propUpdate = getAbsoluteValue(eval(currentdataItem + '()')['' + columns[i].key + '']);");
            codeBuilder.AddLine("                   if (grd[columns[i].key] != propUpdate) {");
            codeBuilder.AddLine("                       grd[columns[i].key] = propUpdate;");
            codeBuilder.AddLine("                       hasChangeProp = true;");
            codeBuilder.AddLine("                   }");
            codeBuilder.AddLine("               }");
            codeBuilder.AddLine("           }");
            codeBuilder.AddLine("           if(hasChangeProp) $(element).igGridUpdating('updateRow', grd['RowDataId'], grd);");
            codeBuilder.AddLine("       }");
            codeBuilder.AddLine("   };");

            codeBuilder.AddLine("   function updateTemplate(pk, step, element, ui, currentdataItem, currentlistItem) {");
            codeBuilder.AddLine("       if (step == 1) {");
            codeBuilder.AddLine("           if (hasPaging(element).length == 0)");
            codeBuilder.AddLine("               pk = pk - 2;");
            codeBuilder.AddLine("           $(element).igGridSelection('selectRow', pk);");
            codeBuilder.AddLine("           gridTrData = ui.grid.dataSource.dataView()[pk];");
            codeBuilder.AddLine("       }");
            codeBuilder.AddLine("       else if (step == 2) {");
            codeBuilder.AddLine("           $(element).igGridSelection('selectRow', pk);");
            codeBuilder.AddLine("           gridTrData = ui.grid.dataSource.dataView()[pk];");
            codeBuilder.AddLine("       }");
            codeBuilder.AddLine("       updateFieldsTemplate(gridTrData['RowDataId'], currentdataItem, currentlistItem);");
            codeBuilder.AddLine("   };");

            codeBuilder.AddLine("   function updateFieldsTemplate(grd, currentdataItem, currentlistItem) {");
            codeBuilder.AddLine("       if (vm.goToKey && 'RowDataId' && grd) {");
            codeBuilder.AddLine("           vm.goToKey('RowDataId', grd, eval(currentdataItem), eval(currentlistItem));");
            codeBuilder.AddLine("       }");
            codeBuilder.AddLine("   };");

            codeBuilder.AddLine("   function configEditor(element, currentdataItem, currentlistItem){");
            codeBuilder.AddLine("       var mode = $(element).igGridUpdating('option', 'editMode');");
            codeBuilder.AddLine("       if (mode == 'cell') {");
            codeBuilder.AddLine("           var rows = $(element).igGrid('rows');");
            codeBuilder.AddLine("           if (rows.length === 0) {");
            codeBuilder.AddLine("               app.showMessage('Não é possível abrir a edição quando não existir ao menos uma linha na grade!', 'Informação', ['Ok']);");
            codeBuilder.AddLine("               return false;");
            codeBuilder.AddLine("           }");
            codeBuilder.AddLine("           var row =  $(element).igGrid('selectedRow');");
            codeBuilder.AddLine("           var isChk = $(element).igGridSelection('selectedRows');");
            codeBuilder.AddLine("           var rowEntity = 0;");
            codeBuilder.AddLine("           if (isChk && isChk.length != 0) rowEntity = isChk[0].id;");
            codeBuilder.AddLine("           var entity = findElementByKey(eval(currentlistItem), 'RowDataId', isNullOrEmpty(row) ? rowEntity : row.id);");
            codeBuilder.AddLine("           if (rowEntity !== 0)");
            codeBuilder.AddLine("               updateFieldsTemplate(entity['RowDataId'], currentdataItem, currentlistItem);");
            codeBuilder.AddLine("           //$(element).igGridUpdating('option', 'editMode', 'rowedittemplate');");
            codeBuilder.AddLine("           $(element).igGridUpdating('option', 'startEditTriggers', 'dblclick,F2');");
            codeBuilder.AddLine("           $('.fa.fa-th').addClass('fa fa-list-alt').removeClass('fa-th');");
            codeBuilder.AddLine("           $(element + '_EditorBtn').attr('title', 'Alterar edição para modo Célula');");
            codeBuilder.AddLine("       }");
            codeBuilder.AddLine("       else {");
            codeBuilder.AddLine("           //$(element).igGridUpdating('option', 'editMode', 'cell');");
            codeBuilder.AddLine("           $(element).igGridUpdating('option', 'startEditTriggers', 'click');");
            codeBuilder.AddLine("           $('.fa.fa-list-alt').addClass('fa fa-th').removeClass('fa-list-alt');");
            codeBuilder.AddLine("           $(element + '_EditorBtn').attr('title', 'Alterar edição para modo Template');");
            codeBuilder.AddLine("       }");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("   function restartGrid(element, cName, isEditorWithinGrid) {");
            codeBuilder.AddLine("       //$(element).igGridUpdating('option', 'editMode', 'cell');");
            codeBuilder.AddLine("       $(element).igGridUpdating('option', 'startEditTriggers', 'click');");
            codeBuilder.AddLine("       $('.fa.fa-list-alt').addClass('fa fa-th').removeClass('fa-list-alt');");
            codeBuilder.AddLine("       $(element).attr('title', 'Alterar edição para modo Template');");

            codeBuilder.AddLine("       if (isEditorWithinGrid) {");
            codeBuilder.AddLine("           if (cName.indexOf('dialog') > -1)");
            codeBuilder.AddLine("               $(cName).attr('style', 'display: none !important;');");
            codeBuilder.AddLine("           else");
            codeBuilder.AddLine("               $('#dialog' + cName + '').attr('style', 'display: none !important;');");
            codeBuilder.AddLine();
            codeBuilder.AddLine("            $(element + '_ContentDLG').attr('style', 'position: relative;height: 1px;');");
            codeBuilder.AddLine("            $(element + '_ContentDLG').next().removeClass('hide');");
            codeBuilder.AddLine("            $(element + '_container').parent().removeClass('hide');");
            codeBuilder.AddLine("       }");
            codeBuilder.AddLine("       else");
            codeBuilder.AddLine("           $('#dialog' + cName + '').dialog('close');");
            codeBuilder.AddLine();
            codeBuilder.AddLine("       dialogIsOpen = false;");
            codeBuilder.AddLine("   };");

            codeBuilder.AddLine("   function getSelectedIndex(element) {");
            codeBuilder.AddLine("       var sIndex = -1;");
            codeBuilder.AddLine("       if ($(element).data('igGridSelection') && $(element).igGridSelection('option', 'multipleSelection')) {");
            codeBuilder.AddLine("           var trs = $(element).igGrid('selectedRows');");
            codeBuilder.AddLine("           if (trs.length > 0) sIndex = trs[0].index;");
            codeBuilder.AddLine("       } else {");
            codeBuilder.AddLine("           var tr = $(element).igGrid('selectedRow');");
            codeBuilder.AddLine("           if (tr != null) sIndex = tr.index;");
            codeBuilder.AddLine("       }");
            codeBuilder.AddLine("       return sIndex;");
            codeBuilder.AddLine("   };");

            codeBuilder.AddLine("   function fillLabels(current, element, dataView, cName) {");
            codeBuilder.AddLine("       checkDisableControl(element);");
            codeBuilder.AddLine("       showAndHideColumnsEditor(element, dataView);");
            codeBuilder.AddLine("       var ui = $(element).data('igGridUpdating');");
            codeBuilder.AddLine("       var totalGrid = (Array.isArray(ui.grid.options.dataSource) ? ui.grid.options.dataSource : ui.grid.options.dataSource.data()).length;");

            codeBuilder.AddLine("       if ($(element).data('igGridSelection') && $(element).igGridSelection('option', 'multipleSelection')) {");
            codeBuilder.AddLine("           var trs = $(element).igGrid('selectedRows');");
            codeBuilder.AddLine("           if (trs.length > 0) var currentRow = trs[0].index + 1;");
            codeBuilder.AddLine("       }");
            codeBuilder.AddLine("       else");
            codeBuilder.AddLine("           var currentRow = $(element).igGrid('selectedRow').index + 1;");

            codeBuilder.AddLine("       if (hasPaging(element).length > 0) {");
            codeBuilder.AddLine("           var totalCurrentPage = totalGrid;");
            codeBuilder.AddLine("           var currentPage = $(element).igGridPaging('pageIndex') + 1;");
            codeBuilder.AddLine("           var pageIndex = $(element).igGridPaging('pageIndex');");
            codeBuilder.AddLine("           var pageSize = $(element).igGridPaging('pageSize');");
            codeBuilder.AddLine("           if (totalGrid / pageSize > currentPage)");
            codeBuilder.AddLine("               totalCurrentPage = (currentPage * ui.grid.dataSource.dataView().length);");
            codeBuilder.AddLine("           $('label#currentNumber' + cName + '').html(currentRow + ' - ' + totalCurrentPage);");
            codeBuilder.AddLine("       }");
            codeBuilder.AddLine("       else");
            codeBuilder.AddLine("           $('label#currentNumber' + cName + '').html((current == 0 ? totalGrid : current));");
            codeBuilder.AddLine("       $('label#totalNumber' + cName + '').html(totalGrid);");
            codeBuilder.AddLine("   };");

            codeBuilder.AddLine("   function checkDisableControl(element) {");
            codeBuilder.AddLine("       var columns = $(element).igGridUpdating('option', 'columnSettings');");
            codeBuilder.AddLine("       columns.forEach(function (entry, index) {");
            codeBuilder.AddLine("           if (entry.fieldTplDisabled) {");
            codeBuilder.AddLine("               var controlTemplate = $('[id^=\"' + $lx(vm, '#div').selector.replace('#', '') + '\"][id$=\"_' + entry.columnKey + 'Template\"]');");
            codeBuilder.AddLine("               $(controlTemplate).append('<div style=\"position: absolute;top:0;left:0;width: 100%;height:100%;z-index:2;opacity:0.4;filter: alpha(opacity = 50)\"></div>');");
            codeBuilder.AddLine("           };");
            codeBuilder.AddLine("       });");
            codeBuilder.AddLine("   };");

            codeBuilder.AddLine("   function showAndHideColumnsEditor(element, dataView) {");
            codeBuilder.AddLine("       if (vm.status() !== 'C') {");
            codeBuilder.AddLine("           var colunas = $(element).igGrid('option', 'columns');");
            codeBuilder.AddLine("           colunas.forEach(function (entry, index) {");
            codeBuilder.AddLine("               if (entry.hidden && entry.key !== 'RowDataId') {");
            codeBuilder.AddLine("                   var control = $('#" + viewModel + "_div' + (!dataView ? '' : '' + dataView + '_') + entry.key + 'Template');");
            codeBuilder.AddLine("                   if (!control.hasClass('hide') && !control.hasClass('onlyEditor'))");
            codeBuilder.AddLine("                       control.addClass('hide');");
            codeBuilder.AddLine("               } else if (entry.key !== 'RowDataId') {");
            codeBuilder.AddLine("                   var control = $('#" + viewModel + "_div' + (!dataView ? '' : '' + dataView + '_') + entry.key + 'Template');");
            codeBuilder.AddLine("                   if (control.hasClass('hide'))");
            codeBuilder.AddLine("                       control.removeClass('hide');");
            codeBuilder.AddLine("               }");
            codeBuilder.AddLine("           });");
            codeBuilder.AddLine("       }");
            codeBuilder.AddLine("   };");

            codeBuilder.AddLine("   function hasPaging(element) {");
            codeBuilder.AddLine("        return $.grep($(element).igGrid('option', 'features'), function (e) { return e.name == 'Paging'; }); ");
            codeBuilder.AddLine("   };");

            codeBuilder.AddLine("   function selectorEditorTemplate(element, pk, cName, cDataItem_listItem, dataV_parentName, entityName) {");
            codeBuilder.AddLine("       var element = element;");
            codeBuilder.AddLine("       var dataItem_ListItem = cDataItem_listItem.split(';');");
            codeBuilder.AddLine("       var dataView_parentName = dataV_parentName.split(';');");
            codeBuilder.AddLine("       var currentdataItem = dataItem_ListItem[0];");
            codeBuilder.AddLine("       var currentlistItem = dataItem_ListItem[1];");
            codeBuilder.AddLine("       var entityName = entityName;");
            codeBuilder.AddLine("       var dataView = dataView_parentName[0];");
            codeBuilder.AddLine("       var parentName = dataView_parentName[1];");
            codeBuilder.AddLine("       var ui = $(element).data('igGridUpdating');");

            codeBuilder.AddLine("       var verticalContainer = $(element).igGrid('scrollContainer');");
            codeBuilder.AddLine("       verticalContainer.scrollTop($(element).igGrid('option', 'avgRowHeight') * (pk - 1));");
            codeBuilder.AddLine("       gridTrData = ui.grid.dataSource.dataView()[pk];");
            codeBuilder.AddLine("       updateFieldsTemplate(gridTrData['RowDataId'], currentdataItem, currentlistItem);");
            codeBuilder.AddLine("       updateGrid(gridTrData, pk, ui, currentdataItem, element);");
            codeBuilder.AddLine("       $(element).igGridSelection('clearSelection');");
            codeBuilder.AddLine("       $(element).igGridSelection('selectRow', pk);");
            codeBuilder.AddLine("       if (status() === 'E') notifyPresentation('' + currentlistItem.split('.').pop() + '');");
            codeBuilder.AddLine("       fillLabels(pk + 1, element, dataView, cName);");
            codeBuilder.AddLine("       $(element + '_Toggle').slideToggle();");
            codeBuilder.AddLine("   };");

            codeBuilder.AddLine("   function loadSeletor(tbGrid, fields, grd, entity) {");
            codeBuilder.AddLine("       var tbody = $(tbGrid).children('tbody');");
            codeBuilder.AddLine("       var cols = fields.split(',');");
            codeBuilder.AddLine("       var list = $(grd).data('igGrid').dataSource.dataView();");
            codeBuilder.AddLine("       $(tbGrid + ' > tbody > tr').remove();");
            codeBuilder.AddLine("       var objCols = new Array();");
            codeBuilder.AddLine("       var metaDataEntity = vm.metadataInfo[entity];");
            codeBuilder.AddLine();
            codeBuilder.AddLine("       if ($(grd + '_Toggle').is(':hidden')) {");
            codeBuilder.AddLine("           if (status() !== 'C') {");
            codeBuilder.AddLine("               for (j = 0; j < cols.length; j++) {");
            codeBuilder.AddLine("                   for (var prop in metaDataEntity) {");
            codeBuilder.AddLine("                       if (metaDataEntity[prop]['key'] == cols[j]) {");
            codeBuilder.AddLine("                           objCols.push(metaDataEntity[prop]);");
            codeBuilder.AddLine("                           break;");
            codeBuilder.AddLine("                       }");
            codeBuilder.AddLine("                   }");
            codeBuilder.AddLine("               }");
            codeBuilder.AddLine("               for (i = 0; i < list.length; i++) {");
            codeBuilder.AddLine("                  var tr = document.createElement('TR');");
            codeBuilder.AddLine("                  tr.setAttribute('id', i);");
            codeBuilder.AddLine("                  tr.setAttribute('onclick', '$(this).clickSelectorGrid($(this));');");
            codeBuilder.AddLine("                  for (j = 0; j < objCols.length; j++) {");
            codeBuilder.AddLine("                      var td = document.createElement('TD');");
            codeBuilder.AddLine("                      if (objCols[j].isDomain)");
            codeBuilder.AddLine("                          var fieldFormat = vm.dataDomains.getName(objCols[j].domainName, list[i][objCols[j].key]);");
            codeBuilder.AddLine("                      else if (objCols[j].dataType == 'date')");
            codeBuilder.AddLine("                          var fieldFormat = Globalize.format(getUTCDate(list[i][objCols[j].key]), objCols[j].format);");
            codeBuilder.AddLine("                      else if (objCols[j].dataType == 'number' && objCols[j].format == 'int')");
            codeBuilder.AddLine("                          var fieldFormat = Globalize.format(list[i][objCols[j].key], \"n0\");");
            codeBuilder.AddLine("                      else");
            codeBuilder.AddLine("                          var fieldFormat = Globalize.format(list[i][objCols[j].key], (objCols[j].dataType == 'number' ? \"n\" : objCols[j].format));");
            codeBuilder.AddLine("                      td.appendChild(document.createTextNode(fieldFormat));");
            codeBuilder.AddLine("                      tr.appendChild(td);");
            codeBuilder.AddLine("                  }");
            codeBuilder.AddLine("                  tbody.append(tr);");
            codeBuilder.AddLine("               }");
            codeBuilder.AddLine("           } else {");
            codeBuilder.AddLine("               var tr = document.createElement('TR');");
            codeBuilder.AddLine("               var td = document.createElement('TD');");
            codeBuilder.AddLine("               td.setAttribute('colspan', '' + cols.length + '');");
            codeBuilder.AddLine("               td.style.textAlign = 'center';");
            codeBuilder.AddLine("               td.appendChild(document.createTextNode('Modo Pesquisa'));");
            codeBuilder.AddLine("               tr.appendChild(td);");
            codeBuilder.AddLine("               tbody.append(tr);");
            codeBuilder.AddLine("           }");
            codeBuilder.AddLine("       }");
            codeBuilder.AddLine("   };");

            #endregion

            #region get layout files

            var pivotControls = ui.LayoutDefinition.GetLayoutElementsByClass("FlatPivotGrid");

            if (pivotControls != null && pivotControls.Any())
            {
                codeBuilder.AddLine("var getSPAVersion = function(){");
                codeBuilder.AddLine("   var currentSPAVersion = '';");
                codeBuilder.AddLine("   var packageName = '" + packageName + "';");
                codeBuilder.AddLine("   var currentSpa = _.find(require.s.contexts._.config.packages, function (item) { return item.name == packageName });");
                codeBuilder.AddLine("   if (currentSpa && currentSpa.location){ ");
                codeBuilder.AddLine("      currentSPAVersion = currentSpa.location;");
                // melhorar maneira para pegar o caminho do SPA
                codeBuilder.AddLine("      while (currentSPAVersion.startsWith('.') || currentSPAVersion.startsWith('/'))");
                codeBuilder.AddLine("          currentSPAVersion = currentSPAVersion.substring(1);");
                codeBuilder.AddLine("   }");
                codeBuilder.AddLine("   return currentSPAVersion;");
                codeBuilder.AddLine("}");

                codeBuilder.AddLine("var projectTemplateFiles = [");

                var lastPivotControl = pivotControls.Last();
                foreach (var pivotControl in pivotControls)
                {
                    var pivotFileLayout = ((LayoutContainer)(pivotControl)).PivotFileLayout;

                    var layoutFiles = GetLayoutFiles(ui, pivotControl.GetControlName(""), pivotFileLayout);
                    var lastLayoutFile = layoutFiles.Last();
                    foreach (ComboboxItem layoutFile in layoutFiles)
                    {
                        codeBuilder.AddLine("    {");
                        codeBuilder.AddLine("        pivotName: '{0}',", pivotControl.GetControlName(""));
                        codeBuilder.AddLine("        layoutFullName: getSPAVersion() + '/pivotTableLayouts/{0}',", layoutFile.Value);
                        codeBuilder.AddLine("        name: '{0}',", layoutFile.Text);
                        codeBuilder.AddLine("        viewName: '{0}',", viewModel);
                        codeBuilder.AddLine("        projectName: '{0}',", GetSpaProjectName());
                        codeBuilder.AddLine("        content: '',");
                        codeBuilder.AddLine("        id: '',");
                        codeBuilder.AddLine("        selected: " + layoutFile.Selected.ToString().ToLower());
                        codeBuilder.AddLine("    }");
                        if (!layoutFile.Equals(lastLayoutFile) || !pivotControl.Equals(lastPivotControl))
                            codeBuilder.AddLine(",");
                    }
                }
                codeBuilder.AddLine("];");
                codeBuilder.AddLine("var layoutFiles = [];");
                codeBuilder.AddLine("var selectedLayoutFile = {};");
                codeBuilder.AddLine("var getLayoutsFiles = function () {");
                codeBuilder.AddLine();
                codeBuilder.AddLine("    showProcessing('Buscando Layouts...');");
                codeBuilder.AddLine("    ");
                codeBuilder.AddLine("    //Ação realizada para captura de versão disponível da SPA.");
                codeBuilder.AddLine("    layoutFiles.clear();");
                codeBuilder.AddLine("    ");
                codeBuilder.AddLine("    projectTemplateFiles.forEach(function(item){layoutFiles.push(item);});");

                foreach (var pivotControl in pivotControls)
                {
                    var pivotDataSource = !((LayoutContainer)(pivotControl)).EntityData.IsNullOrEmpty()
                                            ? ((LayoutContainer)(pivotControl)).EntityData
                                            : ((LayoutContainer)(pivotControl)).PivotDataSource.ToLower() + ((LayoutContainer)(pivotControl)).PivotCube;
                    codeBuilder.AddLine();
                    codeBuilder.AddLine("    dataContext.getPivotLayouts(");
                    codeBuilder.AddLine("    {");
                    codeBuilder.AddLine(String.Format("        rootNamespace: '{0}',", GetSpaProjectName()));
                    codeBuilder.AddLine(String.Format("        viewName: '{0}',", viewModel));
                    codeBuilder.AddLine(String.Format("        pivotName: '{0}',", pivotControl.GetControlName("")));
                    codeBuilder.AddLine(String.Format("        pivotDataSource: '{0}'", pivotDataSource));
                    codeBuilder.AddLine("    },");
                    codeBuilder.AddLine("        function (data) {");
                    codeBuilder.AddLine("            data.forEach(function (item) {");
                    codeBuilder.AddLine("                var content = '';");
                    codeBuilder.AddLine("                if (item.Selected)");
                    codeBuilder.AddLine("                    content = item.Content;");
                    codeBuilder.AddLine("    ");
                    codeBuilder.AddLine("                layoutFiles.push({");
                    codeBuilder.AddLine("                    projectName: item.ProjectName,");
                    codeBuilder.AddLine("                    viewName: item.ViewName,");
                    codeBuilder.AddLine("                    pivotName: item.PivotName,");
                    codeBuilder.AddLine("                    name: item.Name,");
                    codeBuilder.AddLine("                    layoutFullName: null,");
                    codeBuilder.AddLine("                    content: content,");
                    codeBuilder.AddLine("                    selected: item.Selected,");
                    codeBuilder.AddLine("                    id: item.Id");
                    codeBuilder.AddLine("                });");
                    codeBuilder.AddLine("            });");
                    codeBuilder.AddLine("    ");
                    codeBuilder.AddLine("            closeProcessing();");
                    codeBuilder.AddLine("        },");
                    codeBuilder.AddLine("        function (error) {");
                    codeBuilder.AddLine("            console.log('Error: ' + error);");
                    codeBuilder.AddLine("            closeProcessing();");
                    codeBuilder.AddLine("        });");
                }
                codeBuilder.AddLine("};");
                codeBuilder.AddLine();
                codeBuilder.AddLine("getLayoutsFiles();");

            }
            codeBuilder.AddLine();

            #endregion

            #region expose dataToolbar methods
            codeBuilder.AddLine();
            codeBuilder.AddLine("var dataToolbar = {");
            codeBuilder.AddLine("        isBusy: isBusy,");
            codeBuilder.AddLine("        currentRecordInfo: currentRecordInfo,");
            codeBuilder.AddLine("        canGoFirst: canGoFirst,");
            codeBuilder.AddLine("        canGoBack: canGoBack,");
            codeBuilder.AddLine("        canGoForward: canGoForward,");
            codeBuilder.AddLine("        canGoLast: canGoLast,");
            codeBuilder.AddLine("        canClear: canClear,");
            codeBuilder.AddLine("        canQuickSearch: canQuickSearch,");
            codeBuilder.AddLine("        canNavigate: canNavigate,");
            codeBuilder.AddLine("        noBusyLoading: _noBusyLoading,");
            codeBuilder.AddLine("        currentPage: currentPage,");
            codeBuilder.AddLine("        quickSearch: quickSearch,");
            codeBuilder.AddLine("        canExport: canExport,");
            codeBuilder.AddLine("        canGridExport: canGridExport,");
            codeBuilder.AddLine("        canQuery: canQuery,");
            codeBuilder.AddLine("        canCustomSearch: canCustomSearch,");
            codeBuilder.AddLine("        canRefreshCurrentData: canRefreshCurrentData,");
            codeBuilder.AddLine("        hasDataFeed: hasDataFeed,");
            codeBuilder.AddLine("        canAddNew: canAddNew,");
            codeBuilder.AddLine("        canRemove: canRemove,");
            codeBuilder.AddLine("        canEdit: canEdit,");
            codeBuilder.AddLine("        canSave: canSave,");
            codeBuilder.AddLine("        canUndo: canUndo,");
            codeBuilder.AddLine("        canPrint: canPrint,");
            codeBuilder.AddLine("        goFirst: goFirst,");
            codeBuilder.AddLine("        goBack: goBack,");
            codeBuilder.AddLine("        goForward: goForward,");
            codeBuilder.AddLine("        goLast: goLast,");
            codeBuilder.AddLine("        adjustNavigationByPage: adjustNavigationByPage,");
            codeBuilder.AddLine("        query: query,");
            codeBuilder.AddLine("        customSearch: customSearch,");
            codeBuilder.AddLine("        customSearchResult: customSearchResult,");
            codeBuilder.AddLine("        hasCustomSearches: hasCustomSearches,");
            codeBuilder.AddLine("        refreshCurrentData: refreshCurrentData,");
            codeBuilder.AddLine("        exportData: exportData,");
            codeBuilder.AddLine("        customLayout: customLayout,");
            codeBuilder.AddLine("        undo: undo,");
            codeBuilder.AddLine("        save: save,");
            if (uiEntityAdapter.GetTopParent().IsBufferSaving() && enableSaveLazingMode)
            {
                codeBuilder.AddLine("        saveFake: saveFake,");
                codeBuilder.AddLine("        submitAllChanges: submitAllChanges,");
                codeBuilder.AddLine("        saveSuccessInnerUIs: saveSuccessInnerUIs,");
            }

            codeBuilder.AddLine("        addNew: addNew,");
            codeBuilder.AddLine("        remove: remove,");
            codeBuilder.AddLine("        refresh: refresh,");
            codeBuilder.AddLine("        clear: clearByUser,");
            codeBuilder.AddLine("        helper: helper,");
            codeBuilder.AddLine("        print: print,");
            codeBuilder.AddLine("        showDataFeedUrl: showDataFeedUrl,");
            codeBuilder.AddLine("        edit: edit,");
            codeBuilder.AddLine("        canViewInfo: canViewInfo,");
            codeBuilder.AddLine("        viewInfo: viewInfo,");
            codeBuilder.AddLine("        lastSearchFilter: lastSearchFilter,");
            codeBuilder.AddLine("        importPhoto: importPhoto,");
            codeBuilder.AddLine("        title: function() { return (uiSettings && uiSettings.displayName ? uiSettings.displayName : '" + ui.DisplayName + "'); }");
            codeBuilder.AddLine("    };");
            #endregion


            codeBuilder.AddLine();

            codeBuilder.AddLine("if (dataContext.dataDomains) {");
            codeBuilder.AddLine("    dataContext.dataDomains.refreshData = function () {");
            codeBuilder.AddLine("        refreshToolbar();");
            codeBuilder.AddLine("    };");
            codeBuilder.AddLine("}");
            #region expose vm methods
            codeBuilder.AddLine("var vm = {");
            codeBuilder.AddLine("        isDashboardFilter: " + (ui.EntityAdapter == null ? "false" : ui.EntityAdapter.IsDashboardFilter.ToString().ToLower()) + ",");
            codeBuilder.AddLine("        layout: layout,");
            codeBuilder.AddLine("        layoutDesigner: layoutDesigner,");
            codeBuilder.AddLine("        layoutDesignerOriginal: layoutDesignerOriginal,");
            codeBuilder.AddLine("        flattenLayout: flattenLayout,");
            codeBuilder.AddLine("        getLayoutColumnSpan: getLayoutColumnSpan,");
            codeBuilder.AddLine("        getLayoutDisplayName: getLayoutDisplayName,");
            codeBuilder.AddLine("        getLayoutVisible: getLayoutVisible,");
            codeBuilder.AddLine("        getLayoutHeaderGrid: getLayoutHeaderGrid,");
            codeBuilder.AddLine("        getDimensionUniqueName: getDimensionUniqueName,");
            codeBuilder.AddLine("        flattenObjectByProperty: flattenObjectByProperty,");
            codeBuilder.AddLine("        currentLayout: ko.observable(),");
            codeBuilder.AddLine("        useLikeCommandAsDefault: " + ui.UseLikeCommandAsDefault.ToString().ToLower() + ",");
            codeBuilder.AddLine("        " + entitiesRef + ": " + entitiesRef + ",");
            if (ui.HasCustomization)
                codeBuilder.AddLine("        custom: custom,");
            codeBuilder.AddLine("        viewName: '" + viewModel + "',");
            if (pivotControls != null && pivotControls.Any())
            {
                codeBuilder.AddLine("        layoutFiles: layoutFiles,");
                codeBuilder.AddLine("        selectedLayoutFile: selectedLayoutFile,");
                codeBuilder.AddLine("        getLayoutsFiles: getLayoutsFiles,");
            }

            if (uiEntityAdapter.GetTopParent().IsBufferSaving())
                codeBuilder.AddLine("        getDataForSaving: getDataForSaving,");
            if (uiEntityAdapter.GetTopParent().IsBufferSaving() && enableSaveLazingMode)
            {
                codeBuilder.AddLine("        getViewMapInfo: getViewMapInfo,");
                codeBuilder.AddLine("        saveSuccessInnerUIs: saveSuccessInnerUIs,");
            }
            codeBuilder.AddLine("        getAddedEntities: getAddedEntities,");
            codeBuilder.AddLine("        getAllChanges: getAllChanges,");
            codeBuilder.AddLine("        gridSaveStates: [],");
            codeBuilder.AddLine("        hasValidationErrors: hasValidationErrors,");
            codeBuilder.AddLine("        hasInternalUIsValidationErrors: hasInternalUIsValidationErrors,");
            codeBuilder.AddLine("        canReportErrors: false,");
            codeBuilder.AddLine("        currentDataItem: currentDataItem,");
            codeBuilder.AddLine("        exportDataDetails: exportDataDetails,");
            codeBuilder.AddLine("        openEditor: openEditor,");
            codeBuilder.AddLine("        deleteGrid: deleteGrid,");
            codeBuilder.AddLine("        selectorEditorTemplate: selectorEditorTemplate,");
            codeBuilder.AddLine("        loadSeletor: loadSeletor,");
            codeBuilder.AddLine("        dialogIsOpen: false,");
            codeBuilder.AddLine("        currentDataIndex: currentDataIndex,");
            codeBuilder.AddLine("        navigationByPage: navigationByPage,");
            codeBuilder.AddLine("        hasMainTopDataGrid: hasMainTopDataGrid,");
            codeBuilder.AddLine("        dataShared: [],");
            codeBuilder.AddLine("        hasChanges: hasChanges,");
            codeBuilder.AddLine("        isSaving: isSaving,");
            codeBuilder.AddLine("        enabledForEditing: enabledForEditing,");
            codeBuilder.AddLine("        dataToolbar: dataToolbar,");
            codeBuilder.AddLine("        getDataContext: function() { return dataContext; },");
            codeBuilder.AddLine("        getParentSelectorDataName: getParentSelectorDataName,");
            codeBuilder.AddLine("        validParentSelectorDataCondition: validParentSelectorDataCondition,");
            codeBuilder.AddLine("        addNewToInnerUI: addNewToInnerUI,");
            codeBuilder.AddLine("        getDataFromInnerUI: getDataFromInnerUI,");
            codeBuilder.AddLine("        queryInnerUIs: queryInnerUIs,");
            codeBuilder.AddLine("        clear: clear,");
            codeBuilder.AddLine("        clearInnerUIs: clearInnerUIs,");
            codeBuilder.AddLine("        dataSource: dataSource,");
            codeBuilder.AddLine("        getMaxLength: getMaxLength,");
            codeBuilder.AddLine("        addDataSource: addDataSource,");
            codeBuilder.AddLine("        getVisibleProperties: getVisibleProperties,");
            codeBuilder.AddLine("        status: status,");
            codeBuilder.AddLine("        removeParentRelatedItems: removeParentRelatedItems,");
            codeBuilder.AddLine("        onSavingValidation: onSavingValidation,");
            codeBuilder.AddLine("        goToKey: goToKey,");
            codeBuilder.AddLine("        getSpecializedLookupItems: getSpecializedLookupItems,");
            codeBuilder.AddLine("        dataBind: dataBind,");
            codeBuilder.AddLine("        isDataSourceHided: isDataSourceHided,");
            codeBuilder.AddLine("        //Durandal Events");
            codeBuilder.AddLine("        activate: activate,");
            codeBuilder.AddLine("        binding: binding,");
            codeBuilder.AddLine("        finalizeCombo: finalizeCombo,");
            codeBuilder.AddLine("        dataCombo: dataCombo,");
            codeBuilder.AddLine("        clearCombo: clearCombo,");
            codeBuilder.AddLine("        dataDomains: dataContext.dataDomains,");
            codeBuilder.AddLine("        bindingComplete: bindingComplete,");
            codeBuilder.AddLine("        attached: attached,");
            codeBuilder.AddLine("        canDeactivate: canDeactivate,");
            codeBuilder.AddLine("        canActivate: canActivate,");
            codeBuilder.AddLine("        deactivate: deactivate,");
            codeBuilder.AddLine("        //End Durandal Events");
            codeBuilder.AddLine("        compositionComplete: compositionComplete,");
            codeBuilder.AddLine("        detached: detached,");
            codeBuilder.AddLine("        app: app,");
            codeBuilder.AddLine("        lookUpProperties: dataContext.lookUpProperties,");
            codeBuilder.AddLine("        metadataInfo: dataContext.metadataInfo,");
            codeBuilder.AddLine("        dataExportInfo: dataContext.dataExportInfo,");
            codeBuilder.AddLine("        entityNames: dataContext.entityNames,");
            codeBuilder.AddLine("        lookUpNames: dataContext.lookUpNames,");
            codeBuilder.AddLine("        getWithBinding: dataContext.getWithBinding,");
            codeBuilder.AddLine("        managerAuth: managerAuth,");
            codeBuilder.AddLine("        rootBmTypeName: '" + ui.EntityAdapter.PrimaryEntity + "',");
            codeBuilder.AddLine("        rootDataTypeName: '" + masterEntityName + "',");
            codeBuilder.AddLine("        rootNamespace: '" + _designerRoot.GetContextNamespace() + "',");
            codeBuilder.AddLine("        setSecurity: setSecurity,");
            codeBuilder.AddLine("        isReportComposition: isReportComposition,");
            codeBuilder.AddLine("        refreshToolbar: refreshToolbar,");
            codeBuilder.AddLine("        refreshCurrentBind: refreshCurrentBind,");
            codeBuilder.AddLine("        lazyRefreshBinding: lazyRefreshBinding,");
            codeBuilder.AddLine("        createEntity: createEntity,");
            codeBuilder.AddLine("        notifyPresentation: notifyPresentation,");
            codeBuilder.AddLine("        notifyInnerElements: notifyInnerElements,");
            codeBuilder.AddLine("        getServiceAddress: dataContext.getServiceAddress,");
            codeBuilder.AddLine("        getAccessGroup: dataContext.getAccessGroup,");
            codeBuilder.AddLine("        getBandeiraRede: getBandeiraRede,");
            codeBuilder.AddLine("        getCurrentBrands: getCurrentBrands,");
            codeBuilder.AddLine("        setBandeiraRede: setBandeiraRede,");
            codeBuilder.AddLine("        entitySearchRange: entitySearchRange,");
            codeBuilder.AddLine("        modalMultimidia: modalMultimidia,");
            codeBuilder.AddLine("        currentActivityInformation: currentActivityInformation,");
            codeBuilder.AddLine("        showProcessing: showProcessing,");
            codeBuilder.AddLine("        closeProcessing: closeProcessing,");
            codeBuilder.AddLine("        internalUIs: [],");
            codeBuilder.AddLine("        viewType: viewType,");
            codeBuilder.AddLine("        hideToolbar: hideToolbar,");
            codeBuilder.AddLine("        isDependentVM: isDependentVM,");
            codeBuilder.AddLine("        brandDecimals: brandDecimals,");
            codeBuilder.AddLine("        getInnerJExpression: getInnerJExpression,");
            codeBuilder.AddLine("        allowMultiSelectionInSearch: allowMultiSelectionInSearch,");
            codeBuilder.AddLine("        transactionNumberControl: transactionNumberControl,");

            foreach (var evt in ui.GetUserInterfaceClientEvented().Where(e => e.ExposedByViewModel))
            {
                codeBuilder.AddLine("        " + evt.Name + ": " + evt.Name + ",");
            }

            foreach (var entity in uiEntityAdapter.GetCompleteHierarchy())
            {
                codeBuilder.AddLine("        create" + entity.Name + ": create" + entity.Name + ",");
                codeBuilder.AddLine("        createAndNotify" + entity.Name + ": createAndNotify" + entity.Name + ",");
            }

            if (hasKPI)
            {
                foreach (var entity in _designerRoot.EntityAdapters.Where(e => e.DerivedEntityAdapters.Count == 0).ToList())
                {
                    foreach (var kpiName in entity.GetAllInheritanceAttributes().Where(e => !e.KpiName.IsNullOrEmpty()).Select(d => d.KpiName).Distinct())
                    {
                        codeBuilder.AddLine("        get" + kpiName + "Ranges: get" + kpiName + "Ranges,");
                        codeBuilder.AddLine("        kpi" + kpiName + ": null,");
                        codeBuilder.AddLine("        get" + kpiName + "GaugeGrid: get" + kpiName + "GaugeGrid,");
                    }
                }

                codeBuilder.AddLine("        getKpiColor: getKpiColor,");
            }

            codeBuilder.AddLine("        deleteEntity: deleteEntity,");
            codeBuilder.AddLine("        currentBrands: ko.observable(null),");
            codeBuilder.AddLine("        brands: managerBrand.getBrandVM(),");
            codeBuilder.AddLine("        hasBrand: " + hasBrand.ToString().ToLower() + ",");
            codeBuilder.AddLine("        controllerName: dataContext.controllerName,");
            codeBuilder.AddLine("        getJExpression: getJExpression,");
            codeBuilder.AddLine("        replaceInnerUIsKeys: replaceInnerUIsKeys,");
            codeBuilder.AddLine("        replaceKeyFromParent: replaceKeyFromParent,");
            codeBuilder.AddLine("        getQueryFilter: getQueryFilter,");
            codeBuilder.AddLine("        getTranslatedFilter: getTranslatedFilter,");
            codeBuilder.AddLine("        sortData: sortData,");
            codeBuilder.AddLine("        lastJEntitySearch: function () { return lastJEntitySearch; },");
            codeBuilder.AddLine("        isEditable: isEditable,");
            codeBuilder.AddLine("        setStatus: setStatus,");
            codeBuilder.AddLine("        common: common,");
            codeBuilder.AddLine("        getDecimalsByData: getDecimalsByData,");
            codeBuilder.AddLine("        showRegisteredUI: showRegisteredUI,");
            codeBuilder.AddLine("        openingExternalUIFromGrid: openingExternalUIFromGrid,");
            codeBuilder.AddLine("        __moduleId__: '" + packageName + "/viewmodels/" + ui.Name + "',");
            codeBuilder.AddLine("        pivots : pivots");


            codeBuilder.AddLine("    };");
            #endregion

            codeBuilder.AddLine();
            codeBuilder.AddLine("dataContext.setCurrentViewModel(vm);");
            codeBuilder.AddLine("return vm;");
            codeBuilder.DecreaseIndent();
            codeBuilder.AddLine("}");
            codeBuilder.AddLine();
            codeBuilder.AddLine("return vmInstance;");
            codeBuilder.AddLine("});");

        }

        /// <summary>
        /// Generate Resource code.
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="codeBuilder"></param>
        private void GenerateSpaResourceCode(EntityAdapterUserInterface ui, Linx.Tools.CodeBuilder codeBuilder, string filaName)
        {
            codeBuilder.AddLine("/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/");
            codeBuilder.AddLine("define(['./" + filaName + "'],");
            codeBuilder.AddLine("	function (custom){");
            codeBuilder.AddLine("		var result = {};");
            codeBuilder.AddLine();
            codeBuilder.AddLine("       result.languageFile = function () {");
            codeBuilder.AddLine("       	return 'pt-br';");
            codeBuilder.AddLine("       }");

            codeBuilder.AddLine();
            codeBuilder.AddLine("result.objectLanguage_" + ui.Name + " = function () {");
            codeBuilder.AddLine("           var langResult = {");
            codeBuilder.AddLine("               Name: '" + ui.Name + "', Items: [");
            ui.LayoutDefinition.Containers.ForEach(container => codeBuilder.Add(GenerateLayoutForVM(container, ui)));
            codeBuilder.AddLine("               ]");
            codeBuilder.AddLine("           }");
            codeBuilder.AddLine("           langResult.Items.concat(custom.getCustomTranslation().Items);");
            codeBuilder.AddLine("           return langResult;");
            codeBuilder.AddLine("        };");
            codeBuilder.AddLine("       return result;");
            codeBuilder.AddLine("  }");
            codeBuilder.AddLine(")");
            codeBuilder.AddLine("");
        }

        private bool GenerateSpaResourceCustomCode(string file, Linx.Tools.CodeBuilder codeBuilder)
        {
            var item = this.GetSpaAppFolder("resources");
            if (!item.IsNull())
            {
                if (!File.Exists(file))
                {
                    codeBuilder.AddLine("define([],");
                    codeBuilder.AddLine("    function() {");
                    codeBuilder.AddLine();
                    codeBuilder.AddLine("        var result = {};");
                    codeBuilder.AddLine();
                    codeBuilder.AddLine("        result.getCustomTranslation = function() {");
                    codeBuilder.AddLine("            return {");
                    codeBuilder.AddLine("                Items: [");
                    codeBuilder.AddLine("                    //Inserir aqui as propriedades customizadas conforme o modelo abaixo.");
                    codeBuilder.AddLine("                    {");
                    codeBuilder.AddLine("                        Name: 'IdExample', DisplayName: '.......'");
                    codeBuilder.AddLine("                    }");
                    codeBuilder.AddLine("                ]");
                    codeBuilder.AddLine("            };");
                    codeBuilder.AddLine("        };");
                    codeBuilder.AddLine();
                    codeBuilder.AddLine("        return result;");
                    codeBuilder.AddLine("    }");
                    codeBuilder.AddLine();
                    codeBuilder.AddLine(");");

                    return true;
                }
            }

            return false;
        }

        private void AddClientEvents(EntityAdapterUserInterface ui, MacroScriptEngine msEngine, Tools.CodeBuilder codeBuilder)
        {
            var uiClientEvents = ui.GetUserInterfaceClientEvented();
            if (uiClientEvents.Count > 0)
            {
                codeBuilder.AddLine("//#region Client Events");
                foreach (var cliEvent in uiClientEvents)
                {
                    codeBuilder.AddLine("var " + cliEvent.Name + " = function (" + String.Join(", ", cliEvent.Parameters.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries).Select(e => e.Right(" "))) + ") {");
                    codeBuilder.AddLine(cliEvent.MacroScript.IsNullOrEmpty() ? (cliEvent.ReturnType.ToLower().Contains("void") ? "" : "return " + this.GetSpaDefaultValueByType(cliEvent.ReturnType, true) + ";") : msEngine.ReplaceAllMacros(cliEvent.MacroScript, MacroOutputType.JavaScript, _designerRoot.GetDirectorySourcePart()) + (cliEvent.ReturnType.ToLower().Contains("bool") ? "\r\nreturn " + this.GetSpaDefaultValueByType(cliEvent.ReturnType, true) + ";" : ""));
                    codeBuilder.AddLine("}");
                }
                codeBuilder.AddLine("//#endregion Client Events");
            }
        }

        private void generateIsBusyMethod(Tools.CodeBuilder codeBuilder)
        {
            codeBuilder.AddLine();
            codeBuilder.AddLine("var _isBusy = false;");
            codeBuilder.AddLine("var isBusy = function isBusy(value) {");
            codeBuilder.AddLine("    if (typeof value === 'undefined') {");
            codeBuilder.AddLine("        return _isBusy;");
            codeBuilder.AddLine("    } else {");
            codeBuilder.AddLine("        _isBusy = value;");
            codeBuilder.AddLine("        if ($(\".page-container\").html() == undefined || $(\".page-container\").html().length == 0)");
            codeBuilder.AddLine("        return;");
            codeBuilder.AddLine("        if (value) { common.showProcess('#main'); }");
            codeBuilder.AddLine("        else { common.closeProcess('#main'); }");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("};");
        }

        private void generateNotifyInnerElements(EntityAdapterUserInterface ui, Tools.CodeBuilder codeBuilder, bool hasBinding)
        {
            codeBuilder.AddLine();
            codeBuilder.AddLine("var notifyInnerElements = function (element, isExpander) {");
            codeBuilder.AddLine("    if (element)");
            codeBuilder.AddLine("    {");
            if (hasBinding)
                codeBuilder.AddLine("        dataBind('', true);");
            codeBuilder.AddLine("        try{ $(window).trigger('resize'); } catch(e){ console.log(e); }");
            codeBuilder.AddLine("        var innerElements = element.find(\"table\");");
            codeBuilder.AddLine("        if (innerElements.length > 0 && (vm.dataSource.length > 0 || vm.internalUIs.length > 0)) {");
            codeBuilder.AddLine("            for (var idx = 0; idx < innerElements.length; idx++) {");
            codeBuilder.AddLine("                if($(innerElements[idx]).parents('.tab-pane').hasClass('active') || isExpander) {");
            codeBuilder.AddLine("                    for (var db in vm.dataSource) { if (vm.dataSource[db].key == innerElements[idx].id) vm.dataSource[db].itemsSource.dataBind(false, true); }");
            codeBuilder.AddLine("                    //Notifying inner UIs");
            codeBuilder.AddLine("                    for (var idxUI = 0; idxUI < vm.internalUIs.length; idxUI++) {");
            codeBuilder.AddLine("                       var innerVM = vm[vm.internalUIs[idxUI]]();");
            codeBuilder.AddLine("                       for (var db in innerVM.dataSource) {");
            codeBuilder.AddLine("                           if (innerVM.dataSource[db].key == innerElements[idx].id)");
            codeBuilder.AddLine("                               innerVM.dataSource[db].itemsSource.dataBind(false, true);");
            codeBuilder.AddLine("                       }");
            codeBuilder.AddLine("                    }");
            codeBuilder.AddLine("                }");
            codeBuilder.AddLine("            }");
            codeBuilder.AddLine("        }");
            if (ui.ExistsClientEvent("OnTabActive"))
                codeBuilder.AddLine("        OnTabActive(element.selector);");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("};");
        }

        #endregion

        #region Auxiliary Methods

        /// <summary>
        /// Spa Context Name.
        /// </summary>
        /// <returns></returns>
        public string GetSpaContextName()
        {
            return _designerRoot.GetDirectContextName() + "Context";
        }

        /// <summary>
        /// Spa ViewModel Name.
        /// </summary>
        /// <param name="ui"></param>
        /// <returns></returns>
        public string GetSpaViewModelName(EntityAdapterUserInterface ui)
        {
            return ui.Name;
        }

        public IEnumerable<ComboboxItem> GetLayoutFiles(EntityAdapterUserInterface ui, string pivotName, string selectedItem = null)
        {
            List<ComboboxItem> layoutFiles = new List<ComboboxItem>();
            var projectItem = GetSpaAppFolder("pivotTableLayouts");

            if (projectItem != null && projectItem.Properties != null)
            {
                var fullPath = projectItem.Properties.Item("FullPath");
                if (fullPath != null)
                {
                    var files = Directory.GetFiles(fullPath.Value.ToString(), pivotName + "*.*", SearchOption.AllDirectories)
                        .Where(f => f.EndsWith(".xml", StringComparison.CurrentCultureIgnoreCase) || f.EndsWith(".json", StringComparison.CurrentCultureIgnoreCase));

                    foreach (var file in files)
                    {
                        string fileName = new FileInfo(file).Name;
                        layoutFiles.Add(new ComboboxItem
                        {
                            Text = fileName.Replace(ui.Name + "_pivot", ""),
                            Value = fileName,
                            Selected = fileName.Equals(selectedItem, StringComparison.CurrentCultureIgnoreCase)
                        });
                    }
                }
            }

            layoutFiles.Insert(0, new ComboboxItem { Text = "(Nenhum)", Value = "", Selected = !layoutFiles.Any(i => i.Selected) });

            return layoutFiles;
        }

        /// <summary>
        /// Rename SPA service file.
        /// </summary>
        /// <param name="oldName"></param>
        public void RenameSpaServiceCode(string oldName)
        {
            var item = this.GetSpaAppFolder("services");
            if (!item.IsNull())
            {
                var fileItem = _designerRoot.GetProjectItemByName(item.ProjectItems, oldName + ".js");
                if (fileItem != null)
                    fileItem.Name = this.GetSpaContextName() + ".js";
            }
        }

        /// <summary>
        /// Generate lookup finalizers
        /// </summary>
        /// <param name="codeBuilder"></param>
        /// <returns></returns>
        private string GenerateLookUpJsFinalizers(Linx.Tools.CodeBuilder codeBuilder)
        {
            List<string> usedLookUps = new List<string>();
            string result = String.Empty;

            codeBuilder.AddLine();
            codeBuilder.AddLine("//#region LookUps Finalizers");

            foreach (var entity in _designerRoot.EntityAdapters)
            {
                foreach (var lookUp in entity.GetAllLookUpsInfo(true))
                {
                    if (usedLookUps.Contains(lookUp.Name))
                        continue;

                    //Add Used Lookup
                    usedLookUps.Add(lookUp.Name);

                    codeBuilder.AddLine(" var finalizeAll" + lookUp.Name + " = function (replaceTo, selectedElements, propertyName, lookupInfo) {");

                    codeBuilder.AddLine("    if (!replaceTo || !selectedElements)");
                    codeBuilder.AddLine("        return;");

                    codeBuilder.AddLine("    if (!Array.isArray(selectedElements)) {");
                    codeBuilder.AddLine("        selectedElements = [selectedElements];");
                    codeBuilder.AddLine("    }");

                    var parentLink = entity.GetParentLinkRelation();

                    if (lookUp.CheckExistence)
                    {
                        codeBuilder.AddLine("    var originalRow = replaceTo;");
                        codeBuilder.AddLine("    var hasDisconsideredElement = false;");
                    }

                    if ((lookUp.IsMultiSelection || lookUp.CheckExistence) && !parentLink.IsNull())
                    {
                        codeBuilder.AddLine("    var parent = getAbsoluteValue(replaceTo." + parentLink.TargetEntityAdapter.Name + ");");
                    }
                    codeBuilder.AddLine("    isFinalizingLookup(true);");
                    codeBuilder.AddLine("    if (!propertyName)");
                    codeBuilder.AddLine("        propertyName = '';");

                    codeBuilder.AddLine("    var isUsedOriginalRow = false;");

                    codeBuilder.AddLine("    for (var i = 0; i < selectedElements.length; i++)");
                    codeBuilder.AddLine("    {");
                    codeBuilder.AddLine("        var selectedElement = selectedElements[i];");


                    if (lookUp.CheckExistence)
                    {
                        string cmopareExpression = "";
                        foreach (var prop in lookUp.Properties.Where(e => e.IsPrimaryKey))
                        {
                            cmopareExpression += (cmopareExpression.IsNullOrEmpty() ? "" : " && ") + "getAbsoluteValue(curList[idx]." + prop.EntityPropertyRelated + ") === getAbsoluteValue(selectedElement['" + prop.Name + "'])";
                        }
                        if (!cmopareExpression.IsNullOrEmpty())
                        {
                            codeBuilder.AddLine("        var isInvalidElement = false;");
                            codeBuilder.AddLine("        var curList = " + (parentLink.IsNull() ? "lookupInfo.vm.dataView()" : "parent." + entity.Name + "List()") + ";");
                            codeBuilder.AddLine("        for (var idx = 0; idx < curList.length; idx++) {");
                            codeBuilder.AddLine("           if (curList[idx] != originalRow && " + cmopareExpression + ") {");
                            codeBuilder.AddLine("               isInvalidElement = true;");
                            codeBuilder.AddLine("               hasDisconsideredElement = true;");
                            codeBuilder.AddLine("               break;");
                            codeBuilder.AddLine("           }");
                            codeBuilder.AddLine("        }");
                            codeBuilder.AddLine("        if (isInvalidElement) continue;");
                        }
                    }

                    if (lookUp.IsMultiSelection)
                    {
                        codeBuilder.AddLine("        if (isUsedOriginalRow) {");
                        codeBuilder.AddLine("            replaceTo = lookupInfo.vm.create" + entity.Name + "(" + (parentLink.IsNull() ? "" : "parent") + ");");
                        codeBuilder.AddLine("        }");
                        codeBuilder.AddLine("        else {");
                        codeBuilder.AddLine("            isUsedOriginalRow = true;");
                        codeBuilder.AddLine("        }");
                    }

                    var properties = entity.GetAllInheritanceAttributes();
                    foreach (var prop in lookUp.Properties.Where(e => !e.EntityPropertyRelated.IsNullOrEmpty()))
                    {
                        var relatedProp = properties.FirstOrDefault(e => e.Name == prop.EntityPropertyRelated);
                        if (relatedProp != null)
                        {
                            codeBuilder.AddLine("        if (propertyName === '' || propertyName === '" + prop.EntityPropertyRelated + "') {");
                            codeBuilder.AddLine("           if (selectedElement.hasOwnProperty('" + prop.Name + "') && replaceTo.hasOwnProperty('" + prop.EntityPropertyRelated + "'))");
                            codeBuilder.AddLine("           {");
                            codeBuilder.AddLine("               setAbsoluteValue(replaceTo, '" + prop.EntityPropertyRelated + "', getAbsoluteValue(selectedElement['" + prop.Name + "']));");
                            codeBuilder.AddLine("           }");
                            codeBuilder.AddLine("           else if (replaceTo.hasOwnProperty('" + prop.EntityPropertyRelated + "')) {");
                            codeBuilder.AddLine("               setAbsoluteValue(replaceTo, '" + prop.EntityPropertyRelated + "', " + this.GetSpaDefaultValueByType(relatedProp.Datatype + (relatedProp.IsNullable() ? "?" : "")) + ");");
                            codeBuilder.AddLine("           }");
                            codeBuilder.AddLine("        }");
                        }
                    }

                    if (entity.ExistsClientEvent("OnLookedUp" + lookUp.Name))
                    {
                        codeBuilder.AddLine("        if (typeof replaceTo.OnLookedUp" + lookUp.Name + " == 'function') {");
                        codeBuilder.AddLine("            replaceTo.OnLookedUp" + lookUp.Name + "(selectedElement);");
                        codeBuilder.AddLine("        }");
                    }

                    codeBuilder.AddLine("        if (replaceTo.validatedlookupsArray && !replaceTo.validatedlookupsArray.contains('" + lookUp.Name + "'))");
                    codeBuilder.AddLine("            replaceTo.validatedlookupsArray.push('" + lookUp.Name + "');");
                    codeBuilder.AddLine("    }");

                    codeBuilder.AddLine("    //Trigger context data update event");
                    codeBuilder.AddLine("    if (replaceTo.isPOCO) vm.refreshCurrentBind();");
                    codeBuilder.AddLine("    document.dispatchEvent(dataUpdateEvent);");
                    codeBuilder.AddLine("    isFinalizingLookup(false);");

                    if (lookUp.CheckExistence)
                    {
                        codeBuilder.AddLine("    if (hasDisconsideredElement) {");
                        codeBuilder.AddLine("       clear" + lookUp.Name + "(replaceTo);");
                        codeBuilder.AddLine("       app.showMessage('Itens que já estão sendo utilizados foram desconsiderados nessa seleção!', 'Informação', ['Ok']);");
                        codeBuilder.AddLine("    }");
                    }

                    codeBuilder.AddLine("};");

                    codeBuilder.AddLine();
                    codeBuilder.AddLine("function clear" + lookUp.Name + "(replaceTo) {");

                    codeBuilder.AddLine("    if (!replaceTo)");
                    codeBuilder.AddLine("        return;");

                    codeBuilder.AddLine("    isClearingLookup(true);");

                    foreach (var prop in lookUp.Properties.Where(e => !e.EntityPropertyRelated.IsNullOrEmpty()))
                    {
                        var relatedProp = entity.GetAllInheritanceAttributes().FirstOrDefault(e => e.Name == prop.EntityPropertyRelated);
                        if (relatedProp != null)
                        {
                            codeBuilder.AddLine("    setAbsoluteValue(replaceTo, '" + relatedProp.Name + "', " + this.GetSpaDefaultValueByType(relatedProp.Datatype + (relatedProp.IsNullable() ? "?" : "")) + ");");
                            if (!relatedProp.DomainName.IsNullOrEmpty())
                                codeBuilder.AddLine("    setAbsoluteValue(replaceTo, '" + relatedProp.Name + "Name', " + this.GetSpaDefaultValueByType(relatedProp.Datatype + (relatedProp.IsNullable() ? "?" : "")) + ");");
                        }
                    }

                    codeBuilder.AddLine("    isClearingLookup(false);");

                    codeBuilder.AddLine("    //Trigger context data update event");
                    codeBuilder.AddLine("    if (replaceTo.isPOCO) vm.refreshCurrentBind();");
                    codeBuilder.AddLine("    setTimeout(function () {document.dispatchEvent(dataUpdateEvent);}, 100);");

                    codeBuilder.AddLine("}");

                    result += (result.IsNullOrEmpty() ? String.Empty : ",\r\n") + "            finalizeAll" + lookUp.Name + ": finalizeAll" + lookUp.Name;
                    result += (result.IsNullOrEmpty() ? String.Empty : ",\r\n") + "            clear" + lookUp.Name + ": clear" + lookUp.Name;
                }
            }


            codeBuilder.AddLine("//#endregion");

            return result;
        }

        /// <summary>
        /// Get Default Value.
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="invertBoolean"></param>
        /// <returns></returns>
        private string GetSpaDefaultValueByType(string dataType, bool invertBoolean = false)
        {
            var defaultValue = "null";
            if (!dataType.Contains("Nullable<") && !dataType.Contains("?"))
            {
                dataType = dataType.RemoveNullDefinition();
                if (dataType.InList(new string[] { "byte", "int16", "int32", "int", "long", "short", "int64", "sbyte", "uint16", "uint32", "uint64", "single", "double", "decimal" }))
                    defaultValue = "0";
                else if (dataType.Contains("datetime"))
                    defaultValue = "getCurrentDate()";
                else if (dataType.Contains("bool"))
                    defaultValue = invertBoolean ? "true" : "false";
                else if (dataType.ToLower().Contains("guid"))
                    defaultValue = "'00000000-0000-0000-0000-000000000000'";
                else
                    defaultValue = "''";
            }

            return defaultValue;
        }

        /// <summary>
        /// Generate JS query methods.
        /// </summary>
        /// <param name="codeBuilder"></param>
        /// <returns></returns>
        private string GenerateDataServiceJsQueryActions(Linx.Tools.CodeBuilder codeBuilder)
        {
            string result = String.Empty, orderBy;
            codeBuilder.AddLine();
            codeBuilder.AddLine("//#region Get LookUps");
            List<string> lookUps = new List<string>();

            foreach (var lookUp in _designerRoot.LookUpAdapters)
            {
                result += (result.IsNullOrEmpty() ? String.Empty : ",\r\n") + "            get" + lookUp.Name + "ByEntitySearch: get" + lookUp.Name + "ByEntitySearch";
                codeBuilder.AddLine();
                codeBuilder.AddLine("var get" + lookUp.Name + "ByEntitySearch = function (jEntitySearch, order, skip, take, direction, lookupField) {");
                codeBuilder.AddLine("    var query = EntityQuery.from('Get" + lookUp.Name + "ByEntitySearch').noTracking(true);");
                codeBuilder.AddLine("    query = (direction === 'descending' ? query.orderByDesc(order) : query.orderBy(order));");
                codeBuilder.AddLine();
                codeBuilder.AddLine("    if ((typeof jEntitySearch !== 'undefined') && jEntitySearch !== null)");
                codeBuilder.AddLine("        query = query.withParameters({ propertyName: (isNullOrEmpty(lookupField) ? order : lookupField), jEntitySearch: jEntitySearch });");
                codeBuilder.AddLine();
                codeBuilder.AddLine("    if (take > 0)");
                codeBuilder.AddLine("       query = query.skip(skip).take(take);");
                codeBuilder.AddLine("    query = query.inlineCount(true);");
                codeBuilder.AddLine();
                codeBuilder.AddLine("    return manager.executeQuery(query)");
                codeBuilder.AddLine("    .fail(queryFailed);");
                codeBuilder.AddLine("};");
                lookUps.Add(lookUp.Name);
            }


            List<PublicationStructure> publishers = new List<PublicationStructure>();
            if (_designerRoot.PublisherAutoReference != null)
                publishers.Add(_designerRoot.PublisherAutoReference);
            publishers.AddRange(_designerRoot.Subscriptions.Where(e => e.Publisher != null).Select(e => e.Publisher));
            string luContextName, luNSpace, serviceCtxName;

            if (publishers.Count > 0)
                codeBuilder.AddLine("var lookUpExternalManagers = [];");

            foreach (var pub in publishers)
            {
                foreach (var entity in pub.Entities.Where(e => _designerRoot.EntityAdapterRepresentations.Any(ent => ent.TargetEntityAdapterName == e.Name)))
                {
                    foreach (string lookUpName in entity.Properties.Where(e => !e.LookUpInfo.IsNullOrEmpty()).Select(e => LookUpAdapter.GetLookUpName(e.LookUpInfo)).Distinct().Where(e => !lookUps.Contains(e)))
                    {
                        result += (result.IsNullOrEmpty() ? String.Empty : ",\r\n") + "        get" + lookUpName + "ByEntitySearch: get" + lookUpName + "ByEntitySearch";
                        codeBuilder.AddLine();
                        codeBuilder.AddLine("var get" + lookUpName + "ByEntitySearch = function (jEntitySearch, order, skip, take, direction, lookupField) {");
                        codeBuilder.AddLine("    var query = EntityQuery.from('Get" + lookUpName + "ByEntitySearch').noTracking(true);");
                        codeBuilder.AddLine("    query = (direction === 'descending' ? query.orderByDesc(order) : query.orderBy(order));");
                        codeBuilder.AddLine();
                        codeBuilder.AddLine("    if ((typeof jEntitySearch !== 'undefined') && jEntitySearch !== null)");
                        codeBuilder.AddLine("        query = query.withParameters({ propertyName: (isNullOrEmpty(lookupField) ? order : lookupField), jEntitySearch: jEntitySearch });");
                        codeBuilder.AddLine();
                        codeBuilder.AddLine("    if (take > 0)");
                        codeBuilder.AddLine("       query = query.skip(skip).take(take);");
                        codeBuilder.AddLine("    query = query.inlineCount(true);");
                        codeBuilder.AddLine();

                        luContextName = entity.Namespace.Right(".");
                        luNSpace = entity.Namespace.Left("." + luContextName);
                        serviceCtxName = _designerRoot.GetBusinessControllerName(luNSpace, luContextName);

                        codeBuilder.AddLine("    if (!lookUpExternalManagers['" + serviceCtxName + "']) {");
                        codeBuilder.AddLine("       lookUpExternalManagers['" + serviceCtxName + "'] = new breeze.EntityManager({ dataService: new breeze.DataService({");
                        codeBuilder.AddLine("           serviceName: getServiceAddress('" + serviceCtxName + "'),");
                        codeBuilder.AddLine("           hasServerMetadata: false");
                        codeBuilder.AddLine("       }) });");
                        codeBuilder.AddLine("    }");

                        codeBuilder.AddLine("    return lookUpExternalManagers['" + serviceCtxName + "'].executeQuery(query)");
                        codeBuilder.AddLine("    .fail(queryFailed);");
                        codeBuilder.AddLine("};");
                        lookUps.Add(lookUpName);
                    }
                }
            }

            codeBuilder.AddLine("//#endregion");

            codeBuilder.AddLine("//#region Get KPI Ranges");
            foreach (var entity in _designerRoot.EntityAdapters.Where(e => e.DerivedEntityAdapters.Count == 0).ToList())
            {
                foreach (var kpiName in entity.GetAllInheritanceAttributes().Where(e => !e.KpiName.IsNullOrEmpty()).Select(d => d.KpiName).Distinct())
                {
                    codeBuilder.AddLine();

                    result += (result.IsNullOrEmpty() ? String.Empty : ",\r\n") + "        get" + kpiName + "Ranges: get" + kpiName + "Ranges";
                    codeBuilder.AddLine();
                    codeBuilder.AddLine("var get" + kpiName + "Ranges = function () {");
                    codeBuilder.AddLine("    var query = EntityQuery.from('Get" + kpiName + "Ranges').noTracking(true)");
                    codeBuilder.AddLine("    return manager.executeQuery(query)");
                    codeBuilder.AddLine("    .fail(queryFailed);");
                    codeBuilder.AddLine("};");

                    codeBuilder.AddLine();
                }
            }
            codeBuilder.AddLine("//#endregion");
            codeBuilder.AddLine();

            codeBuilder.AddLine("//#region Get Combo LookUp");
            codeBuilder.AddLine("var getResultsCombo = function (lookupName, fieldName, current, callback) {");
            codeBuilder.AddLine("    if (typeof current.executeLookUp === 'function') {");
            codeBuilder.AddLine("       current.executeLookUp(lookupName, fieldName, null, vm, null, null, function (result) {");
            codeBuilder.AddLine("           if (callback) callback(result);");
            codeBuilder.AddLine("       });");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var clientFilterHasModified = function clientFilterHasModified(lookupName, current) {");
            codeBuilder.AddLine("    var lastFilter = dataContext.lastClientFilterExpressions[lookupName];");
            codeBuilder.AddLine("    if (lastFilter === 'Error') return true;");
            codeBuilder.AddLine("    var currentFilter = current.getLookUpClientFilterExpressions(lookupName, null);");
            codeBuilder.AddLine("    return lastFilter != currentFilter;");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("//#endregion Get Combo LookUp");

            codeBuilder.AddLine();
            codeBuilder.AddLine("//#region Get Business Entities");

            result += (result.IsNullOrEmpty() ? String.Empty : ",\r\n") + "            getBmEntityProperties: getBmEntityProperties";
            codeBuilder.AddLine();
            codeBuilder.AddLine("var getBmEntityProperties = function (entityName, parentDataPath) {");
            codeBuilder.AddLine("    return manager.executeQuery(EntityQuery.from('GetBmEntityProperties').withParameters({ entityName: entityName, parentDataPath: parentDataPath }).noTracking(true))");
            codeBuilder.AddLine("    .fail(queryFailed);");
            codeBuilder.AddLine("};");

            foreach (var entity in _designerRoot.EntityAdapters)
            {
                orderBy = entity.GetOrderByCommand();
                result += (result.IsNullOrEmpty() ? String.Empty : ",\r\n") + "            clear" + entity.Name + ": clear" + entity.Name;
                codeBuilder.AddLine();
                codeBuilder.AddLine("var clear" + entity.Name + " = function (idBandeiraRede, complete) {");
                codeBuilder.AddLine("    clearAll();");

                if (entity.EnableQBE)
                {
                    Action<EntityAdapter, EntityAdapter> createEmptyStructure = null;
                    createEmptyStructure = (parentElement, detElement) =>
                    {
                        if (detElement.EnableQBE)
                        {
                            var parentLink = detElement.GetParentLinkRelation();
                            if (!parentLink.IsDashboard)
                            {
                                codeBuilder.AddLine("    resetSequence('" + detElement.Name + "');");
                                codeBuilder.AddLine("    var ref" + detElement.Name + " = manager.createEntity('" + detElement.Name + "', {}, breeze.EntityState.Unchanged);");
                                codeBuilder.AddLine("    ref" + parentElement.Name + ".current" + detElement.Name + "(ref" + detElement.Name + ");");
                                detElement.SourceEntityAdapters.ToList().ForEach(e => createEmptyStructure(detElement, e));
                            }
                        }
                    };

                    codeBuilder.AddLine("    resetSequence('" + entity.Name + "');");
                    codeBuilder.AddLine("    var ref" + entity.Name + " = manager.createEntity('" + entity.Name + "', {}, breeze.EntityState.Unchanged);");
                    entity.SourceEntityAdapters.ToList().ForEach(e => createEmptyStructure(entity, e));
                    codeBuilder.AddLine("    if (complete) complete({ results: [ ref" + entity.Name + " ] });");
                }
                else
                    codeBuilder.AddLine("    if (complete) complete({ results: [] });");

                codeBuilder.AddLine("    return true;");
                codeBuilder.AddLine("};");


                result += (result.IsNullOrEmpty() ? String.Empty : ",\r\n") + "            get" + entity.Name + ": get" + entity.Name;
                codeBuilder.AddLine();
                codeBuilder.AddLine("var get" + entity.Name + " = function (predicate, preserveCurrentState, noTracking) {");
                codeBuilder.AddLine("    if (!preserveCurrentState) clearAll();");
                codeBuilder.AddLine("    var query = EntityQuery.from('Get" + entity.Name + "').noTracking(noTracking)");
                if (!orderBy.IsNullOrEmpty())
                    codeBuilder.AddLine("    .orderBy('" + orderBy + "')");
                codeBuilder.AddLine("    ;");
                codeBuilder.AddLine();

                codeBuilder.AddLine("    if ((typeof predicate !== 'undefined') && predicate !== null)");
                codeBuilder.AddLine("        query = query.where(predicate);");

                codeBuilder.AddLine();
                codeBuilder.AddLine("    return manager.executeQuery(query)");
                codeBuilder.AddLine("    .fail(queryFailed);");
                codeBuilder.AddLine("};");

                result += (result.IsNullOrEmpty() ? String.Empty : ",\r\n") + "            get" + entity.Name + "ByEntitySearchNoAssociations: get" + entity.Name + "ByEntitySearchNoAssociations";
                codeBuilder.AddLine();
                codeBuilder.AddLine("var get" + entity.Name + "ByEntitySearchNoAssociations = function (jEntitySearch, skip, take, returnInlineCount, preserveCurrentState, noTracking, orderByDef, succeeded, complete) {");
                codeBuilder.AddLine("    if (!preserveCurrentState) clearAll();");

                codeBuilder.AddLine("       ");

                codeBuilder.AddLine("    var hasEntitySearch = (typeof jEntitySearch !== 'undefined') && jEntitySearch !== null;");
                codeBuilder.AddLine("    if (hasEntitySearch && jEntitySearch.length > 1000) {");

                var api = _designerRoot.WebApiControllers.FirstOrDefault(e => e.SynchronizedWithDomainService);

                codeBuilder.AddLine("       $.ajax({");
                codeBuilder.AddLine("           type: 'POST',");
                codeBuilder.AddLine("           crossDomain: true,");
                codeBuilder.AddLine("           url: getServiceAddress('" + api.GetRoutePrefix() + "/Add" + entity.Name + "EntitySearchId'),");
                codeBuilder.AddLine("           globalError: true,");
                codeBuilder.AddLine("           headers: managerAuth.getHeaders(),");
                codeBuilder.AddLine("           contentType: 'application/json',");
                codeBuilder.AddLine("           async: true,");
                codeBuilder.AddLine("           cache: false,");
                codeBuilder.AddLine("           data: JSON.stringify([jEntitySearch]),");
                codeBuilder.AddLine("           success: function (response) {");
                codeBuilder.AddLine("               var query = EntityQuery.from('Get" + entity.Name + "ByEntitySearchIdNoAssociations').noTracking(noTracking)");
                if (!orderBy.IsNullOrEmpty())
                    codeBuilder.AddLine("                       .orderBy((isNullOrEmpty(orderByDef) ? '" + orderBy + "' : orderByDef))");
                codeBuilder.AddLine("                           ;");
                codeBuilder.AddLine();
                codeBuilder.AddLine("           query = query.withParameters({ entitySearchId: response });");
                codeBuilder.AddLine();
                codeBuilder.AddLine("               if (take > 0)");
                codeBuilder.AddLine("                   query = query.skip(skip).take(take);");
                codeBuilder.AddLine("               if (returnInlineCount)");
                codeBuilder.AddLine("                   query = query.inlineCount(true);");
                codeBuilder.AddLine();
                codeBuilder.AddLine("               return manager.executeQuery(query).fail(queryFailed).then(succeeded).fin(complete);");
                codeBuilder.AddLine("           }");
                codeBuilder.AddLine("        });");
                codeBuilder.AddLine("}");
                codeBuilder.AddLine("else {");
                codeBuilder.AddLine("       var query = EntityQuery.from('Get" + entity.Name + "ByEntitySearchNoAssociations').noTracking(noTracking)");
                if (!orderBy.IsNullOrEmpty())
                    codeBuilder.AddLine("       .orderBy((isNullOrEmpty(orderByDef) ? '" + orderBy + "' : orderByDef))");
                codeBuilder.AddLine("       ;");
                codeBuilder.AddLine();

                codeBuilder.AddLine("       if (hasEntitySearch)");
                codeBuilder.AddLine("           query = query.withParameters({ jEntitySearch: jEntitySearch });");

                codeBuilder.AddLine("       if (take > 0)");
                codeBuilder.AddLine("           query = query.skip(skip).take(take);");
                codeBuilder.AddLine("       if (returnInlineCount)");
                codeBuilder.AddLine("            query = query.inlineCount(true);");

                codeBuilder.AddLine();
                codeBuilder.AddLine("       return manager.executeQuery(query).fail(queryFailed).then(succeeded).fin(complete);");
                codeBuilder.AddLine("   };");
                codeBuilder.AddLine("}");

            }

            codeBuilder.AddLine("//#endregion");

            return result;

        }

        /// <summary>
        /// Generate ViewModel custom code.
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="codeBuilder"></param>
        /// <param name="methodList"></param>
        private void GenerateViewModelCustomCode(EntityAdapterUserInterface ui, Linx.Tools.CodeBuilder codeBuilder, IEnumerable<string> methodList)
        {
            var uiEntityAdapter = ui.GetDirectEntityAdapter();
            string contextName = GetSpaContextName(), viewModel = ui.Name, masterEntityName = uiEntityAdapter.IsNull() ? "" : uiEntityAdapter.Name;
            bool hasDataContext = !uiEntityAdapter.IsNull() || !ui.Subscription.IsNull();
            codeBuilder.AddLine("define(['durandal/app', 'services/logger'],");
            codeBuilder.AddLine("function (app, logger) {");
            codeBuilder.IncreaseIndent();

            #region customize button events
            codeBuilder.AddLine();
            codeBuilder.AddLine("//#region Customize button events [***>]");
            foreach (var method in methodList)
            {
                codeBuilder.Add(GenerateButtonMethodForUI(method, true));
            }
            codeBuilder.AddLine("//***|Dont remove or change this line");
            codeBuilder.AddLine("//#endregion customize button events [***<]");
            #endregion customize button events

            #region ViewModel Methods
            codeBuilder.AddLine("//#region ViewModel Methods Customize");
            codeBuilder.AddLine("var afterViewInitializing = function (e) { /* e = { viewModel: viewModel } */");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var afterSelecting = function (e) { /* e = { selectedItem: entity, viewModel: viewModel } */");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var beforeGettingLookup = function (e) { /* e = { cancel: boolean, lookupName: string, jEntitySearch: string, entity: entity, viewModel: viewModel } */");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var afterGettingLookup = function (e) { /* e = { lookupName: string, entity: entity, viewModel: viewModel, userConfirm: bool } */");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("//#endregion ViewModel Methods Customize");
            #endregion

            #region Toolbar methods
            codeBuilder.AddLine("//#region Toolbar Methods Customize");
            codeBuilder.AddLine("var beforeClearing = function (e) { /* e = { cancel: boolean, viewModel: viewModel } */");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var afterClearing = function (e) { /* e = { dataItem: object, viewModel: viewModel } */");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var beforeQuerying = function (e) { /* e = { cancel: boolean, jEntitySearch: string, viewModel: viewModel } */");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var afterQuerying = function (e) { /* e = { dataItems: [], viewModel: viewModel } */");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var beforeSaving = function (e) { /* e = { cancel: boolean, viewModel: viewModel } */");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var afterSaving = function (e) { /* e = { viewModel: viewModel} */");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var beforeAdding = function (e) { /* e = { cancel: boolean, viewModel: viewModel } */");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var afterAdding = function (e) { /* e = { viewModel: viewModel } */");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var beforeGoingFirst = function (e) { /* e = { cancel: boolean, viewModel: viewModel } */");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var afterGoingFirst = function (e) { /* e = { viewModel: viewModel } */");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var beforeGoingPrevious = function (e) { /* e = { cancel: boolean, viewModel: viewModel } */");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var afterGoingPrevious = function (e) { /* e = { viewModel: viewModel } */");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var beforeGoingNext = function (e) { /* e = { cancel: boolean, viewModel: viewModel } */");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var afterGoingNext = function (e) { /* e = { viewModel: viewModel } */");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var beforeGoingLast = function (e) { /* e = { cancel: boolean, viewModel: viewModel } */");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var afterGoingLast = function (e) { /* e = { viewModel: viewModel } */");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var beforeRemoving = function (e) { /* e = { cancel: boolean, viewModel: viewModel } */");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var afterRemoving = function (e) { /* e = { viewModel: viewModel } */");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var beforeEditing = function (e) { /* e = { cancel: boolean, viewModel: viewModel } */");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var afterEditing = function (e) { /* e = { viewModel: viewModel } */");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var beforeCancelEdition = function (e) { /* e = { cancel: boolean, viewModel: viewModel } */");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var afterCancelEdition = function (e) { /* e = { viewModel: viewModel } */");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var beforePrinting = function (e) { /* e = { cancel: boolean, viewModel: viewModel } */");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var afterPrinting = function (e) { /* e = { viewModel: viewModel } */");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var beforeAddingChild = function (e) { /* e = { cancel: boolean, entityTypeName: string, viewModel: viewModel } */");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var afterAddingChild = function (e) { /* e = { entityTypeName: string, viewModel: viewModel } */");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var beforeRemovingChild = function (e) { /* e = { cancel: boolean, entityTypeName: string, viewModel: viewModel } */");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var afterRemovingChild = function (e) { /* e = { entityTypeName: string, viewModel: viewModel } */");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("//#endregion Toolbar Methods Customize");
            #endregion

            #region Wizard methods
            codeBuilder.AddLine("//#region Wizard Methods Customize");
            codeBuilder.AddLine("var afterWizardInitializing = function () {");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var beforeWizardStepChanging = function (e) { /* e = { oldIndex: number based zero, newIndex: number based zero, cancel: boolean, viewModel: viewModel, id: controlName } */");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var afterWizardStepChanging = function (e) { /* e = { oldIndex: number based zero, newIndex: number based zero, viewModel: viewModel, id: controlName } */");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var beforeWizardFinalizing = function (e) { /* e = { cancel: boolean, viewModel: viewModel, id: controlName } */");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var afterWizardFinalizing = function (e) { /* e = { viewModel: viewModel, id: controlName } */");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("//#endregion Wizard Methods Customize");
            #endregion



            codeBuilder.AddLine();
            //expose all methods
            #region Expose methods
            codeBuilder.AddLine("var customCtor = function() {");
            codeBuilder.IncreaseIndent();
            codeBuilder.AddLine("var custom = {");
            codeBuilder.IncreaseIndent();
            codeBuilder.AddLine("//begin custom buttons");
            foreach (var method in methodList)
                codeBuilder.AddLine("{0}: {0},", method);
            codeBuilder.AddLine("//end custom buttons - do not remove this line");
            codeBuilder.AddLine("//viewModel");
            codeBuilder.AddLine("afterViewInitializing: afterViewInitializing,");
            codeBuilder.AddLine("afterSelecting: afterSelecting,");
            codeBuilder.AddLine("beforeGettingLookup: beforeGettingLookup,");
            codeBuilder.AddLine("afterGettingLookup: afterGettingLookup,");
            codeBuilder.AddLine("//toolbar");
            codeBuilder.AddLine("beforeQuerying: beforeQuerying,");
            codeBuilder.AddLine("afterQuerying: afterQuerying,");
            codeBuilder.AddLine("beforeClearing: beforeClearing,");
            codeBuilder.AddLine("afterClearing: afterClearing,");
            codeBuilder.AddLine("beforeSaving: beforeSaving,");
            codeBuilder.AddLine("afterSaving: afterSaving,");
            codeBuilder.AddLine("beforeAdding: beforeAdding,");
            codeBuilder.AddLine("afterAdding: afterAdding,");
            codeBuilder.AddLine("beforeGoingFirst: beforeGoingFirst,");
            codeBuilder.AddLine("afterGoingFirst: afterGoingFirst,");
            codeBuilder.AddLine("beforeGoingPrevious: beforeGoingPrevious,");
            codeBuilder.AddLine("afterGoingPrevious: afterGoingPrevious,");
            codeBuilder.AddLine("beforeGoingNext: beforeGoingNext,");
            codeBuilder.AddLine("afterGoingNext: afterGoingNext,");
            codeBuilder.AddLine("beforeGoingLast: beforeGoingLast,");
            codeBuilder.AddLine("afterGoingLast: afterGoingLast,");
            codeBuilder.AddLine("beforeRemoving: beforeRemoving,");
            codeBuilder.AddLine("afterRemoving: afterRemoving,");
            codeBuilder.AddLine("beforeEditing: beforeEditing,");
            codeBuilder.AddLine("afterEditing: afterEditing,");
            codeBuilder.AddLine("beforeCancelEdition: beforeCancelEdition,");
            codeBuilder.AddLine("afterCancelEdition: afterCancelEdition,");
            codeBuilder.AddLine("beforePrinting: beforePrinting,");
            codeBuilder.AddLine("afterPrinting: afterPrinting,");
            codeBuilder.AddLine("beforeAddingChild: beforeAddingChild,");
            codeBuilder.AddLine("afterAddingChild: afterAddingChild,");
            codeBuilder.AddLine("beforeRemovingChild: beforeRemovingChild,");
            codeBuilder.AddLine("afterRemovingChild: afterRemovingChild,");
            codeBuilder.AddLine("//wizard");
            codeBuilder.AddLine("afterWizardInitializing: afterWizardInitializing,");
            codeBuilder.AddLine("beforeWizardStepChanging: beforeWizardStepChanging,");
            codeBuilder.AddLine("afterWizardStepChanging: afterWizardStepChanging,");
            codeBuilder.AddLine("beforeWizardFinalizing: beforeWizardFinalizing,");
            codeBuilder.AddLine("afterWizardFinalizing: afterWizardFinalizing");
            codeBuilder.DecreaseIndent();
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("return custom;");
            codeBuilder.DecreaseIndent();
            codeBuilder.AddLine("}");
            codeBuilder.AddLine();
            codeBuilder.AddLine("return customCtor;");
            codeBuilder.DecreaseIndent();
            #endregion

            codeBuilder.AddLine("});");
            codeBuilder.AddLine();
        }

        /// <summary>
        /// Get all custom method's buttons for the UI
        /// </summary>
        /// <param name="ui"></param>
        /// <returns></returns>
        private IEnumerable<string> GetButtonNamesByUI(EntityAdapterUserInterface ui)
        {
            List<string> methodList = new List<string>();

            Action<LayoutContainer> finder = null;
            finder = (container) =>
            {
                foreach (LayoutControlV2 control in container.Controls.Where(e => e is LayoutControlV2 && e.ClassName.ToLower() == "button").Select(e => (LayoutControlV2)e))
                {
                    methodList.Add(control.GetControlName("") + "_Click");
                }
                container.Controls.Where(e => e is LayoutContainer).Cast<LayoutContainer>().Foreach(finder);
            };

            ui.LayoutDefinition.Containers.ForEach(finder);

            return methodList;
        }

        /// <summary>
        /// Generate button method
        /// </summary>
        /// <param name="method"></param>
        /// <param name="putSpaceInStarts"></param>
        /// <returns></returns>
        private string GenerateButtonMethodForUI(string method, bool putSpaceInStarts)
        {
            return (putSpaceInStarts ? "    " : "") + "var " + method + " = function (e) { /* e = { viewModel: object } */ \r\n    };\r\n";
        }

        private List<string> GetZeroValueDomains()
        {
            List<string> zeroValueDomains = new List<string>();
            foreach (var dInstance in this._designerRoot.GetAllDomains())
            {
                if (dInstance.Values.Any(e => e.Value == "0"))
                {
                    zeroValueDomains.Add(dInstance.ClassName);
                }
            }

            return zeroValueDomains;
        }

        private List<string> CreateMetaDataInfo(EntityAdapter entity, Linx.Tools.CodeBuilder codeBuilder, Dictionary<string, string> lookupKeys, Dictionary<string, string> lookupVisbleColumns, bool byParentComposition = false)
        {
            List<string> propNames = new List<string>();
            codeBuilder.AddLine("entityNames.push('" + entity.Name + (byParentComposition ? "ParentComposition" : "") + "');");
            codeBuilder.AddLine("metadataInfo['" + entity.Name + (byParentComposition ? "ParentComposition" : "") + "'] = [");
            var properties = entity.GetAllInheritanceAttributes(byParentComposition);
            if (properties.Count > 0)
            {
                bool isBigDataMode = entity.GetTopParent().IsBufferSaving();
                bool hasDynamicPK = entity.HasDynamicPrimaryKey();
                if (hasDynamicPK)
                {
                    codeBuilder.AddLine("    { key: 'EntityUniqueKey', maxLength: 0, isPartOfKey: true, headerText: 'EntityUniqueKey', width: '50px', dataType: '" + Linx.Builder.Resources.HtmlCodeGen.GetPropDataType("Guid", "") + "', format: '', hidden: true, unbound: false, group: null },");
                    propNames.Add("EntityUniqueKey");
                }

                if (entity.TargetEntityAdapter != null && entity.TargetEntityAdapter.HasDynamicPrimaryKey())
                {
                    codeBuilder.AddLine("    { key: 'EntityParentUniqueKey', maxLength: 0, isPartOfKey: false, headerText: 'EntityUniqueKey', width: '50px', dataType: '" + Linx.Builder.Resources.HtmlCodeGen.GetPropDataType("Guid", "") + "', format: '', hidden: true, unbound: false, group: null },");
                    propNames.Add("EntityParentUniqueKey");
                }

                List<string> zeroValueDomains = this.GetZeroValueDomains();
                bool isRequired;
                string validators;
                int precision;
                for (int cIndex = 0; cIndex < properties.Count; cIndex++)
                {
                    var property = properties[cIndex];
                    var isDomain = !property.DomainName.IsNullOrEmpty();
                    int ctrlWidth = HtmlCodeGen.GetElementWidth(property.DisplayControl.ToString(), property.Datatype, property.DisplayName, property.DataFormatString, true, property.Precision);
                    string lookupPropertyName = "", lookupVisibleColumns = "";
                    if (lookupKeys.ContainsKey(property.Name))
                        lookupPropertyName = lookupKeys[property.Name];
                    if (lookupVisbleColumns.ContainsKey(property.Name))
                        lookupVisibleColumns = lookupVisbleColumns[property.Name];

                    bool isPK = (!hasDynamicPK && property is EntityAdapterProperty && entity.IsPrimaryKey((EntityAdapterProperty)property));

                    //Domain with zero value 
                    bool zeroDomainValue = !property.DomainName.IsNullOrEmpty() && zeroValueDomains.Contains(property.DomainName);

                    //Defining validators
                    if (property.RemoveValidations)
                        validators = "";
                    else
                    {
                        isRequired = (!property.DomainName.IsNullOrEmpty() || !property.IsNull || property.IsCompulsory);
                        if (isRequired && ((!property.IsZeroNotAllowed && property.IsNumeric()) || property.Datatype.ToLower().Contains("bool")) && !isDomain && !isPK)
                            isRequired = false;

                        validators = (isRequired ? "isRequired: true" : String.Empty);
                        precision = LayoutStructureExtensions.GetPrecision(property.Precision);
                        validators += (validators.IsNullOrEmpty() ? String.Empty : ", ") + "maxLength: " + precision.ToString() + ((!isDomain && precision > 0 && property.Datatype.ToLower().Contains("string")) ? ", validateMaxLength: true" : "");
                        if (!validators.IsNullOrEmpty())
                            validators = ", " + validators;
                    }
                    /////////////////

                    codeBuilder.AddLine("    { key: '" + property.Name + "', isQbeZero: " + zeroDomainValue.ToString().ToLower() + ", isDomain: " + (isDomain).ToString().ToLower() + ", domainName: '" + property.DomainName + "', lookupPropertyName: '" + lookupPropertyName + "', lookupVisibleColumns: '" + lookupVisibleColumns + "'" + validators + ", isPartOfKey: " + isPK.ToString().ToLower() + ", headerText: '" + (property.DisplayName.IsNullOrEmpty() ? property.Name : property.DisplayName) + "', width: '" + ctrlWidth.ToString() + "px', dataType: '" + HtmlCodeGen.GetPropDataType(property.Datatype, "") + "', format: '" + Linx.Builder.Resources.HtmlCodeGen.GetFormatDataType(property.Datatype, "", property.DataFormatString) + "', hidden: " + (!property.IsBrowsable).ToString().ToLower() + ", unbound: false, group: null }" + ((!isBigDataMode && cIndex == properties.Count - 1 && !isDomain) ? String.Empty : ","));
                    propNames.Add(property.Name);
                    if (isDomain)
                    {
                        codeBuilder.AddLine("    { key: '" + property.Name + "Name', isDomain: true, domainName: '" + property.DomainName + "', lookupPropertyName: '" + lookupPropertyName + "', lookupVisibleColumns: '" + lookupVisibleColumns + "', maxLength: 0, isPartOfKey: false, headerText: '" + (property.DisplayName.IsNullOrEmpty() ? property.Name : property.DisplayName) + " (Name)', width: '0px', dataType: '" + HtmlCodeGen.GetPropDataType("string", "") + "', format: '', hidden: true, unbound: false, group: null }" + (!isBigDataMode && cIndex == properties.Count - 1 ? String.Empty : ","));
                        propNames.Add(property.Name + "Name");
                    }
                }

                if (isBigDataMode)
                {
                    if (entity.HasEnabledMedias())
                    {
                        codeBuilder.AddLine("    { key: 'TableMedia', isDomain: false, isRequired: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: '', width: '0px', dataType: '" + HtmlCodeGen.GetPropDataType("string", "") + "', format: '', hidden: true, unbound: false, group: null },");
                        propNames.Add("TableMedia");
                    }

                    codeBuilder.AddLine("    { key: 'ChangeState', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: '', width: '0px', dataType: '" + HtmlCodeGen.GetPropDataType("string", "") + "', format: '', hidden: true, unbound: false, group: null }");
                }
            }
            codeBuilder.AddLine("];");

            return propNames;
        }

        private void CreateDataExportInfo(EntityAdapter entity, Linx.Tools.CodeBuilder codeBuilder)
        {
            var api = _designerRoot.WebApiControllers.FirstOrDefault(e => e.SynchronizedWithDomainService);
            if (api == null)
                return;

            codeBuilder.AddLine("dataExportInfo['" + entity.Name + "'] = [ ");

            Action<EntityAdapter> expData = null;
            expData = (e) =>
            {
                var suffix = ((entity != e && e.TargetEntityAdapter != null && e.IsParentCompositionAllowed()) ? "ParentComposition" : "");
                codeBuilder.AddLine("    " + (entity != e ? ", " : "") + "{ name: '" + e.Name + "', canExportMedia: " + (!e.HasDynamicPrimaryKey()).ToString().ToLower() + " , canExportReport: " + (!e.IsDashboardFilter).ToString().ToLower() + ", actionExport: '" + api.GetRoutePrefix() + "/Get" + e.Name + suffix + "ToExcel', actionReport: '" + api.GetRoutePrefix() + "/Get" + e.Name + suffix + "ToReportXml', actionFeed: '" + api.GetRoutePrefix() + "OData/" + e.Name + suffix + "', actionName: '" + api.GetRoutePrefix() + "/Get" + e.Name + suffix + "ByEntitySearchNoAssociations', display: '" + (String.IsNullOrWhiteSpace(e.DisplayName) ? e.Name : e.DisplayName) + "',  metaData: function() { return metadataInfo['" + e.Name + suffix + "']; } }");
                e.SourceEntityAdapters.ForEach(d => expData(d));
            };

            expData(entity);

            codeBuilder.AddLine("];");
        }

        /// <summary>
        /// Creating JSon Metadata
        /// </summary>
        /// <param name="codeBuilder"></param>
        private Dictionary<string, List<string>> GetJsonMetadata(Linx.Tools.CodeBuilder codeBuilder)
        {
            Dictionary<string, List<string>> propNames = new Dictionary<string, List<string>>();
            codeBuilder.AddLine("var metadataInfo = [];");
            codeBuilder.AddLine("var dataExportInfo = [];");
            codeBuilder.AddLine("var entityNames = [];");
            codeBuilder.AddLine("var lookUpNames = [];");
            codeBuilder.AddLine("var entitylookUps = [];");
            //Entities
            foreach (var entity in _designerRoot.EntityAdapters)
            {
                Dictionary<string, string> lookupKeys = new Dictionary<string, string>();
                Dictionary<string, string> lookupVisbleColumns = new Dictionary<string, string>();
                foreach (var lookup in entity.GetAllLookUpsInfo(true))
                {
                    foreach (var prop in lookup.Properties)
                    {
                        if (!prop.EntityPropertyRelated.IsNullOrEmpty() && !lookupKeys.ContainsKey(prop.EntityPropertyRelated))
                        {
                            lookupKeys.Add(prop.EntityPropertyRelated, prop.Name);
                            lookupVisbleColumns.Add(prop.EntityPropertyRelated, lookup.GetQueryGroupColumns(prop.Name));
                        }
                    }
                }
                propNames.Add(entity.Name, this.CreateMetaDataInfo(entity, codeBuilder, lookupKeys, lookupVisbleColumns));
                if (entity.TargetEntityAdapter != null && entity.IsParentCompositionAllowed())
                    this.CreateMetaDataInfo(entity, codeBuilder, lookupKeys, lookupVisbleColumns, true);
                this.CreateDataExportInfo(entity, codeBuilder);


                //Lookups            
                codeBuilder.AddLine("entitylookUps.push('" + entity.Name + "');");
                codeBuilder.AddLine("entitylookUps['" + entity.Name + "'] = [];");
                foreach (var lookUp in entity.GetAllLookUpsInfo(false))
                {
                    codeBuilder.AddLine("entitylookUps['" + entity.Name + "'].push('" + lookUp.Name + "');");
                    codeBuilder.AddLine("lookUpNames.push('" + lookUp.Name + "');");
                    codeBuilder.AddLine("metadataInfo['" + lookUp.Name + "'] = [");
                    var properties = lookUp.Properties.OrderBy(e => e.Order.ToString().PadLeft(4) + e.DisplayName).ToList();
                    for (int cIndex = 0; cIndex < properties.Count; cIndex++)
                    {
                        var property = properties[cIndex];

                        if (!lookupKeys.Values.Contains(property.Name))
                            property.EntityPropertyRelated = "";

                        int ctrlWidth = HtmlCodeGen.GetElementWidth(DisplayControlType.TextBox.ToString(), property.Datatype, property.DisplayName, property.GetDataFormatString(), true, property.Precision);
                        codeBuilder.AddLine("    { key: '" + property.Name + "', relatedKey: '" + property.EntityPropertyRelated + "', maxLength: " + LayoutStructureExtensions.GetPrecision(property.Precision).ToString() + ", isPartOfKey: " + property.IsPrimaryKey.ToString().ToLower() + ", headerText: '" + (property.DisplayName.IsNullOrEmpty() ? property.Name : property.DisplayName) + "', width: '" + ctrlWidth.ToString() + "px', dataType: '" + Linx.Builder.Resources.HtmlCodeGen.GetPropDataType(property.Datatype, "") + "', format: '" + Linx.Builder.Resources.HtmlCodeGen.GetFormatDataType(property.Datatype, "", property.GetDataFormatString()) + "', hidden: " + (!property.IsBrowsable || !property.DomainName.IsNullOrEmpty()).ToString().ToLower() + ", unbound: false, group: null }" + (cIndex == properties.Count - 1 && !(property.IsBrowsable && !property.DomainName.IsNullOrEmpty()) ? String.Empty : ","));

                        if (property.IsBrowsable && !property.DomainName.IsNullOrEmpty())
                        {
                            ctrlWidth = HtmlCodeGen.GetElementWidth(DisplayControlType.ComboBox.ToString(), property.Datatype, property.DisplayName, property.GetDataFormatString(), true, property.Precision);
                            codeBuilder.AddLine("    { key: '" + property.Name + "Name', relatedKey: '" + (property.EntityPropertyRelated.IsNullOrEmpty() ? "" : property.EntityPropertyRelated + "Name") + "', maxLength: " + LayoutStructureExtensions.GetPrecision("255:0").ToString() + ", isPartOfKey: false, headerText: '" + (property.DisplayName.IsNullOrEmpty() ? property.Name : property.DisplayName) + "', width: '" + ctrlWidth.ToString() + "px', dataType: '" + Linx.Builder.Resources.HtmlCodeGen.GetPropDataType("string", "") + "', format: '" + Linx.Builder.Resources.HtmlCodeGen.GetFormatDataType("string", "", "") + "', hidden: false, unbound: false, group: null }" + (cIndex == properties.Count - 1 ? String.Empty : ","));
                        }
                    }
                    codeBuilder.AddLine("];");
                }
            }

            return propNames;
        }

        /// <summary>
        /// Verify and create, if necessary, the button's method in VMCustom
        /// </summary>
        /// <param name="outputFile"></param>
        /// <param name="fileText"></param>
        /// <param name="methodList"></param>
        /// <param name="projectItems"></param>
        private void VerifyAndCreateCustomButtonMethodInVMCustom(string outputFile, string fileText, IEnumerable<string> methodList, ProjectItems projectItems)
        {
            bool saveFile = false;

            string definitionMethods = string.Empty;
            string exposeMethods = string.Empty;

            string regionMethods = fileText.Extract("[***>]", "[***<]");

            foreach (var method in methodList)
            {

                if (!Regex.IsMatch(regionMethods, @"var\s+" + method + @"\s*=\s*function"))
                {
                    definitionMethods += GenerateButtonMethodForUI(method, saveFile);
                    exposeMethods += string.Format("    {0}: {0},\r\n", method, (saveFile ? "    " : ""));
                    saveFile = true;
                }
            }

            if (saveFile)
            {
                if (!fileText.Contains("//***|") || !fileText.Contains("//end custom buttons"))
                    throw new Exception(string.Format("The file is not in the expected format. Rename or delete it.\nFileName:{0}", outputFile));

                fileText = fileText.Replace("//***|", definitionMethods + "\n//***|");
                fileText = fileText.Replace("//end custom buttons", exposeMethods + "//end custom buttons");

                Linx.Tools.CodeBuilder codeBuilder = new Tools.CodeBuilder();
                codeBuilder.Load(fileText);
                _designerRoot.WriteFile(outputFile, codeBuilder, projectItems);
            }
        }

        /// <summary>
        /// Generate Code for default value and parameters
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        private string GenerateSPACodeForDefaultValueAndParameters(EntityAdapterAttribute property)
        {
            string defaultValue = (property is EntityAdapterProperty ? ((EntityAdapterProperty)property).DefaultValue : (property is EntityAdapterPublicationProperty ? ((EntityAdapterPublicationProperty)property).DefaultValue : ""));

            if (defaultValue.IsNullOrEmpty())
                return String.Empty;

            Func<string> toValue = () =>
            {
                if (Regex.IsMatch(defaultValue, patternForLinxParameter))
                    return string.Format("dataParameters.parameters['{0}']", defaultValue.Extract("[", "]"));
                else if (property.Datatype.ToLower().Contains("guid") && defaultValue.Contains("Guid.NewGuid"))
                    return "getNewGuid()";
                else if (Regex.IsMatch(defaultValue, patternForDateTimeConstructor))
                    return defaultValue.Replace("DateTime", "Date").Replace(")", ",0,0,0)");
                else if (property.Datatype.ToLower().Contains("datetime") && defaultValue.Contains("DateTime.Now"))
                    return "";
                else
                    return defaultValue;
            };

            string valueToReturn = string.Empty;
            string dataType = property.Datatype.ToLower();
            if (dataType.Contains("datetime"))
            {
                string fixedValue = toValue();
                valueToReturn = (fixedValue.IsNullOrEmpty() ? "getCurrentDate()" : "new Date(" + fixedValue + ")");
            }
            else valueToReturn = toValue();

            return valueToReturn;
        }

        /// <summary>
        /// Generate methods for executing lookups
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="codeBuilder"></param>
        private void GenerateSPALookupExecuting(EntityAdapter entity, Linx.Tools.CodeBuilder codeBuilder)
        {

            codeBuilder.AddLine("   ownerReference.getLookupPropertyName = function(propertyName) {");
            codeBuilder.AddLine("      var property = getEntityProperty(ownerReference.typeName, propertyName);");
            codeBuilder.AddLine("      return (property != null && !isNullOrEmpty(property.lookupPropertyName) ? property.lookupPropertyName : propertyName);");
            codeBuilder.AddLine("   }");
            codeBuilder.AddLine("   ownerReference.getLookupVisibleColumns = function(propertyName) {");
            codeBuilder.AddLine("      var property = getEntityProperty(ownerReference.typeName, propertyName);");
            codeBuilder.AddLine("      return (property != null ? property.lookupVisibleColumns : '');");
            codeBuilder.AddLine("   }");


            codeBuilder.AddLine("   ownerReference.getLookUpClientFilterExpressions = function (lookupName, lookupInfo) {");

            foreach (var lookUp in entity.LookUpAdapters.Where(e => !e.ClientFilterExpression.IsNullOrEmpty() || e.HasBrand()))
            {
                codeBuilder.AddLine("       if (lookupName === '" + lookUp.Name + "') {");

                string clientFilter = lookUp.ClientFilterExpression;

                //Check Bandeira Rede                
                string expression = clientFilter.Left("[").Trim().Replace("\"", "'").Replace("this." + entity.Name + ".", "ownerReference.");
                bool hasBrand = lookUp.HasBrand();
                string[] values = clientFilter.Extract("[", "]").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (!expression.IsNullOrEmpty() || hasBrand)
                {
                    if (values.Length > 0)
                    {
                        for (int idx = 0; idx < values.Length; idx++)
                        {
                            EntityAdapterAttribute attrib = null;
                            bool isNullAttribute = false;
                            string propName = values[idx].Trim().Right(".").Left("("), exprElement = values[idx].Trim().Replace("\"", "'").Replace("this." + entity.Name + ".", "ownerReference.");
                            if (!propName.IsNullOrEmpty())
                            {
                                if (exprElement.Length > 2 && exprElement.Right(2) == "()")
                                    exprElement = "getAbsoluteValue(" + exprElement.Left(exprElement.Length - 2) + ")";
                                attrib = entity.GetAttributeFromComposition(propName);
                                if (attrib != null)
                                {
                                    isNullAttribute = attrib.IsNullable();
                                    if (!isNullAttribute)
                                    {
                                        codeBuilder.AddLine("           if (vm.status() === 'E' && isNullOrEmpty(" + exprElement + ")) {");
                                        codeBuilder.AddLine("               app.showMessage('O campo [" + (attrib.DisplayName.IsNullOrEmpty() ? propName : attrib.DisplayName) + "] precisa ser informado.', 'Alerta', ['Ok']);");
                                        codeBuilder.AddLine("               return 'Error';");
                                        codeBuilder.AddLine("           }");
                                    }
                                }
                            }
                            expression = expression.Replace("{" + idx.ToString() + "}", "' + " + (attrib == null ? "convertToString(" + exprElement + ")" : "(isNullOrEmpty(" + exprElement + ") || (" + exprElement + ").toString().indexOf('[') > -1 ? 'DelExpr' : convertToString(" + exprElement + "))") + " + '");
                        }
                    }
                    if (hasBrand)
                        codeBuilder.AddLine("           return (" + (expression.IsNullOrEmpty() ? "" : "'" + expression + ";' + ") + "'IdBandeiraRede#' + (lookupInfo.vm.getBandeiraRede() === 0 && !isNullOrEmpty(lookupInfo.vm.getCurrentBrands()) ? 'In#S' : '==#I') + lookupInfo.vm.getCurrentBrands());");
                    else
                        codeBuilder.AddLine("           return ('" + expression + "');");
                }
                codeBuilder.AddLine("       }");
            }

            codeBuilder.AddLine("       return '';");
            codeBuilder.AddLine("   };");
            codeBuilder.AddLine();


            codeBuilder.AddLine("   ownerReference.getLookupDisplay = function (lookupName) {");
            codeBuilder.AddLine("       var displayName = '';");
            foreach (var lookUp in entity.LookUpAdapters)
            {
                if (!lookUp.DisplayName.IsNullOrEmpty())
                {
                    codeBuilder.AddLine("       if (lookupName === '" + lookUp.Name + "') {");
                    codeBuilder.AddLine("           displayName = ' de " + (lookUp.DisplayName.Contains("Look Up ") ? lookUp.DisplayName.Right("Look Up ").Proper() : lookUp.DisplayName) + "';");
                    codeBuilder.AddLine("       }");
                }
            }
            codeBuilder.AddLine("       return 'Seleção' + displayName;");
            codeBuilder.AddLine("   };");
            codeBuilder.AddLine();

            codeBuilder.AddLine("   ownerReference.getSpecializedLookup = function (lookupName, lookupInfo, fieldToSearch, valueToSearch, ownerReference, allowMultiSelectionInSearch) {");
            codeBuilder.AddLine("       var specializedLookup = '';");
            foreach (var lookUp in entity.LookUpAdapters)
            {
                if (!lookUp.SpecializedUI.IsNullOrEmpty())
                {
                    codeBuilder.AddLine("       if (lookupName === '" + lookUp.Name + "') {");
                    codeBuilder.AddLine("           specializedLookup = { moduleName: 'pkg_" + lookUp.SpecializedUI.Left("/").ToLower().Replace(".", "-") + "/viewmodels/" + lookUp.SpecializedUI.Right("/") + "', uiSettings: { modalForm: modal, fieldToSearch: fieldToSearch, valueToSearch: valueToSearch, lookupInfo: lookupInfo, lookupName: lookupName, ownerReference: ownerReference, removeDataToolbar: false, shareParentBO: false, useFilterFromParent: false, parentSelectorDataName: '', canClear: true, canSearch: true, canAddNew: " + lookUp.CanAddNew.ToString().ToLower() + ", canEdit: " + lookUp.CanAddNew.ToString().ToLower() + ", canDelete: false, canCustomSearch: true, canPrint: false, canLayout: false, canNavigate: true, allowMultiSelectionInSearch: allowMultiSelectionInSearch, applyFilterToParent: false, noSearch: false, parentFieldsRelation: [], detailFieldsRelation: [] } ");
                    codeBuilder.AddLine("           };");
                    codeBuilder.AddLine("       }");
                }
            }
            codeBuilder.AddLine("       return specializedLookup;");
            codeBuilder.AddLine("   };");
            codeBuilder.AddLine();

            var entityProperties = entity.GetAllInheritanceAttributes();
            foreach (var lookup in entity.GetAllLookUpsInfo(true))
            {
                codeBuilder.AddLine("   ownerReference.getSubQueryFilterFrom" + lookup.Name + " = function (propertyName) {");

                codeBuilder.AddLine("       var filter = '';");
                foreach (var prop in lookup.Properties.Where(e => !e.EntityPropertyRelated.IsNullOrEmpty()))
                {
                    var cliFilters = lookup.GetSubQueryClientFilters(prop.EntityPropertyRelated).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (cliFilters.Length > 0 || !prop.DependencyProperty.IsNullOrEmpty())
                    {
                        codeBuilder.AddLine("       if (propertyName === '" + prop.EntityPropertyRelated + "') {");

                        if (!prop.DependencyProperty.IsNullOrEmpty())
                        {
                            codeBuilder.AddLine("           if (vm.status() === 'E' && isNullOrEmpty(getAbsoluteValue(ownerReference." + prop.DependencyProperty + "))) {");
                            codeBuilder.AddLine("               app.showMessage('O campo [' + ownerReference.getDisplayName('" + prop.DependencyProperty + "') + '] precisa ser informado.', 'Alerta', ['Ok']);");
                            codeBuilder.AddLine("               return 'Error';");
                            codeBuilder.AddLine("           }");
                        }

                        foreach (var relation in cliFilters)
                        {
                            string lProp = relation.Left("="), rProp = relation.Right("=");
                            var relationProp = lookup.Properties.FirstOrDefault(e => e.Name == lProp);
                            if (relationProp != null && entityProperties.Any(e => e.Name == rProp))
                            {
                                var dType = Linx.Tools.EntitySearch.ParseJDataType(relationProp.Datatype);
                                codeBuilder.AddLine("           var _" + lProp + " = getAbsoluteValue(this." + rProp + ");");
                                codeBuilder.AddLine("           if (!isNullOrEmpty(_" + lProp + ")) { filter += (filter === '' ? '' : ';') + '" + lProp + "' + (_" + lProp + ".toString().indexOf('[') > -1 ? '#In#S' : '#==#" + dType + "')" + ("SCGT".Contains(dType) ? " + (_" + lProp + ".toString().indexOf('[') > -1 ? '" + dType + ",' : '')" : "") + " + _" + lProp + ".toString().replaceAll('[', '').replaceAll(']', ''); }");
                            }
                        }

                        codeBuilder.AddLine("       }");
                    }
                }
                codeBuilder.AddLine("       return filter;");
                codeBuilder.AddLine("   }");
            }

            codeBuilder.AddLine("   ownerReference.canGetClientFilter = function (lookupName) {");
            string clearList = "";
            foreach (var lookup in entity.GetAllLookUpsInfo(true))
            {
                if (!lookup.ApplyClientFilterOnClear)
                    clearList += (clearList == "" ? "" : ", ") + "'" + lookup.Name + "'";
            }
            if (!clearList.IsNullOrEmpty())
            {
                codeBuilder.AddLine("       return !(vm.status() == 'C' && [" + clearList + "].indexOf(lookupName) >= 0);");
            }
            else
                codeBuilder.AddLine("       return true;");
            codeBuilder.AddLine("   }");

            codeBuilder.AddLine("   ownerReference.validatedlookupsArray = [];");

            codeBuilder.AddLine("   ownerReference.internalLookupSearch = function (lookupName, fieldToSearch, operation, querySucceeded, lookupInfo, valueToSearch, beforeGettingLookup, referencefield) {");
            codeBuilder.AddLine("       if (!lookupName || !fieldToSearch) { console.warn('lookupName or fieldToSearch is Empty!'); querySucceeded(null); return lookupInfo; }");
            codeBuilder.AddLine("       if (isNullOrEmpty(lookupInfo.lastJEntityExpression)) {");
            codeBuilder.AddLine("           if (isNullOrEmpty(referencefield)) referencefield = fieldToSearch;");
            codeBuilder.AddLine("           if ((typeof valueToSearch) === 'undefined')");
            codeBuilder.AddLine("               valueToSearch = getAbsoluteValue(ownerReference[referencefield]);");
            codeBuilder.AddLine("           var extraFilters = '';");
            codeBuilder.AddLine("           if (ownerReference.canGetClientFilter(lookupName)) {");
            codeBuilder.AddLine("               extraFilters = ownerReference.getLookUpClientFilterExpressions(lookupName, lookupInfo);");
            codeBuilder.AddLine("               dataContext.lastClientFilterExpressions[lookupName] = extraFilters;");
            codeBuilder.AddLine("               if (extraFilters === 'Error') { querySucceeded(null); return lookupInfo; }");
            codeBuilder.AddLine("               if (typeof ownerReference['BeforeGet' + lookupName + 'Query'] == 'function') {");
            codeBuilder.AddLine("                   var customFilter = ownerReference['BeforeGet' + lookupName + 'Query'](fieldToSearch, lookupInfo);");
            codeBuilder.AddLine("                   if (customFilter === 'Error') { querySucceeded(null); return lookupInfo; }");
            codeBuilder.AddLine("                   if (!isNullOrEmpty(customFilter)) { extraFilters = (isNullOrEmpty(extraFilters) ? '' : extraFilters + ';') + customFilter; }");
            codeBuilder.AddLine("               }");
            codeBuilder.AddLine("               if (typeof ownerReference['getSubQueryFilterFrom' + lookupName] == 'function') {");
            codeBuilder.AddLine("                   var customFilter = ownerReference['getSubQueryFilterFrom' + lookupName](referencefield);");
            codeBuilder.AddLine("                   if (customFilter === 'Error') { querySucceeded(null); return lookupInfo; }");
            codeBuilder.AddLine("                   if (!isNullOrEmpty(customFilter)) { extraFilters = (isNullOrEmpty(extraFilters) ? '' : extraFilters + ';') + customFilter; }");
            codeBuilder.AddLine("               }");
            codeBuilder.AddLine("           }");
            codeBuilder.AddLine("           var completeExpression = getLookUpJEntityExpression(lookupName, ownerReference, fieldToSearch, valueToSearch, extraFilters, referencefield, app, lookupInfo.vm.useLikeCommandAsDefault);");
            codeBuilder.AddLine("           if (completeExpression === 'Error') { querySucceeded(null); return lookupInfo; }");
            codeBuilder.AddLine("           lookupInfo.lastJEntityExpression = completeExpression;");
            codeBuilder.AddLine("       }");
            codeBuilder.AddLine("       switch (operation) {");
            codeBuilder.AddLine("           case 'F':");
            codeBuilder.AddLine("               lookupInfo.pageSkip = 0;");
            codeBuilder.AddLine("               break;");
            codeBuilder.AddLine("           case 'B':");
            codeBuilder.AddLine("               lookupInfo.pageSkip = lookupInfo.pageSkip - 1;");
            codeBuilder.AddLine("               break;");
            codeBuilder.AddLine("           case 'N':");
            codeBuilder.AddLine("               lookupInfo.pageSkip = lookupInfo.pageSkip + 1;");
            codeBuilder.AddLine("               break;");
            codeBuilder.AddLine("           case 'L':");
            codeBuilder.AddLine("               lookupInfo.pageSkip = lookupInfo.totalPages();");
            codeBuilder.AddLine("               break;");
            codeBuilder.AddLine("           default:");
            codeBuilder.AddLine("       }");
            codeBuilder.AddLine();
            codeBuilder.AddLine("       var e = { cancel: false, lookupName: lookupName, jEntitySearch: lookupInfo.lastJEntityExpression, entity: ownerReference, viewModel: lookupInfo.vm };");
            codeBuilder.AddLine("       if (beforeGettingLookup) beforeGettingLookup(e);");
            codeBuilder.AddLine("       if (e.cancel) { querySucceeded(null); return lookupInfo; }");
            codeBuilder.AddLine("       if(lookupInfo.lastJEntityExpression !== e.jEntitySearch)");
            codeBuilder.AddLine("           lookupInfo.lastJEntityExpression = e.jEntitySearch;");
            codeBuilder.AddLine("       if (lookupInfo.vm) lookupInfo.vm.dataToolbar.isBusy(true);");
            codeBuilder.AddLine("       var returnQueryResult = function (data) { lookupInfo.totalRecords = (isNullOrEmpty(data.inlineCount) ? data.results.length : data.inlineCount); querySucceeded(data); };");
            codeBuilder.AddLine("       eval('dataContext.get' + lookupName + 'ByEntitySearch(lookupInfo.lastJEntityExpression, (isNullOrEmpty(lookupInfo.fieldToSort) ? fieldToSearch : lookupInfo.fieldToSort), lookupInfo.pageSize*lookupInfo.pageSkip, lookupInfo.pageSize, lookupInfo.sortDirection, fieldToSearch).then(returnQueryResult).fail(queryFailed)').fin(function(){ if (lookupInfo.vm) lookupInfo.vm.dataToolbar.isBusy(false); });");
            codeBuilder.AddLine("       return lookupInfo;");
            codeBuilder.AddLine("   };");
            codeBuilder.AddLine();
            codeBuilder.AddLine("   ownerReference.hasValidClientFilter = function (lookupName, lookupInfo) {");
            codeBuilder.AddLine("       var checkClientFilter = ownerReference.getLookUpClientFilterExpressions(lookupName, lookupInfo);");
            codeBuilder.AddLine("       if (checkClientFilter === 'Error') { return false; }");
            codeBuilder.AddLine("       if (typeof ownerReference['BeforeGet' + lookupName + 'Query'] == 'function') {");
            codeBuilder.AddLine("           checkClientFilter = ownerReference['BeforeGet' + lookupName + 'Query']('', lookupInfo);");
            codeBuilder.AddLine("           if (checkClientFilter === 'Error') { return false; }");
            codeBuilder.AddLine("       }");
            codeBuilder.AddLine("       return true;");
            codeBuilder.AddLine("   }");

            codeBuilder.AddLine();
            codeBuilder.AddLine("   ownerReference.executeLookUp = function (lookupName, fieldToSearch, beforeGettingLookup, vm, valueToSearch, finished, comboCallBack, allowMultiSelectionInSearch) {");
            codeBuilder.AddLine("       if (!lookupName || !fieldToSearch) { console.warn('lookupName or fieldToSearch is Empty!'); if (finished) finished(false, null); return; }");
            codeBuilder.AddLine("       var lookupFieldName = ownerReference.getLookupPropertyName(fieldToSearch);");
            codeBuilder.AddLine("       vm.dataBind('', true);");
            codeBuilder.AddLine("       var lookupInfo = new lookupInformation();");
            codeBuilder.AddLine("       lookupInfo.visibleColumns = ownerReference.getLookupVisibleColumns(fieldToSearch);");
            codeBuilder.AddLine("       lookupInfo.vm = vm;");
            codeBuilder.AddLine("       lookupInfo.isMultiSelection = lookupName.in([" + string.Join(", ", _designerRoot.GetAllMultiSelectionLookups().Select(l => "'" + l + "'")) + "]);");
            codeBuilder.AddLine("       var specializedLookup = ownerReference.getSpecializedLookup(lookupName, lookupInfo, lookupFieldName, valueToSearch, ownerReference, allowMultiSelectionInSearch);");

            codeBuilder.AddLine("       if (isNullOrEmpty(specializedLookup)) {");
            codeBuilder.AddLine("               ownerReference.internalLookupSearch(lookupName, lookupFieldName, 'F',");
            codeBuilder.AddLine("                   function querySucceeded(data) {");
            codeBuilder.AddLine("                       if (typeof ownerReference['OnLoading' + lookupName + 'Query'] == 'function') {");
            codeBuilder.AddLine("                           ownerReference['OnLoading' + lookupName + 'Query'](data);");
            codeBuilder.AddLine("                       }");
            codeBuilder.AddLine("                       if ((typeof comboCallBack) === 'function') {");
            codeBuilder.AddLine("                           return comboCallBack(data ? data.results : null);");
            codeBuilder.AddLine("                       }");
            codeBuilder.AddLine("                       else if (data == null || data.results == null || data.results.length == 0) {");
            codeBuilder.AddLine("                           if (finished) finished(false, null);");
            codeBuilder.AddLine("                           ownerReference.clearLookUp(lookupName);");
            codeBuilder.AddLine("                           if (data != null) app.showMessage('A informação de Lookup [' + ownerReference.getDisplayName(fieldToSearch) + '] não foi encontrada!', 'Informação', ['Ok']);");
            codeBuilder.AddLine("                           return;");
            codeBuilder.AddLine("                       }");
            codeBuilder.AddLine("                       lookupInfo.totalRecords = (isNullOrEmpty(data.inlineCount) ? data.results.length : data.inlineCount);");
            codeBuilder.AddLine("                       showLookUp(dataContext, ownerReference, ownerReference.getLookupDisplay(lookupName), lookupName, lookupFieldName, ownerReference.internalLookupSearch, lookupInfo, ");
            codeBuilder.AddLine("                           function (confirm, values) {");
            codeBuilder.AddLine("                               var results = '';");
            codeBuilder.AddLine("                               if (values != null && values.length > 1) {");
            codeBuilder.AddLine("                                   $.each(values, function (index, item) { results += (index == 0 ? '' : ',') + item[lookupFieldName].toString().trim() });");
            codeBuilder.AddLine("                                   results = '[' + results + ']';");
            codeBuilder.AddLine("                                   ownerReference[fieldToSearch](results);");
            codeBuilder.AddLine("                                   if (vm.entitySearchRange[ownerReference.typeName + fieldToSearch] === undefined)");
            codeBuilder.AddLine("                                       vm.entitySearchRange[ownerReference.typeName + fieldToSearch] = ko.observable(results);");
            codeBuilder.AddLine("                                   else vm.entitySearchRange[ownerReference.typeName + fieldToSearch](results);");
            codeBuilder.AddLine("                                   document.dispatchEvent(dataUpdateEvent);");
            codeBuilder.AddLine("                               }");
            codeBuilder.AddLine("                               if (finished) finished(confirm, results);");
            codeBuilder.AddLine("                           }, data.results, allowMultiSelectionInSearch);");
            codeBuilder.AddLine("               }, lookupInfo, valueToSearch, beforeGettingLookup, fieldToSearch);");
            codeBuilder.AddLine("       }");
            codeBuilder.AddLine("       else {");
            codeBuilder.AddLine("               var currentModal = (modal.inUse ? modal2 : modal);");
            codeBuilder.AddLine("               if (currentModal.inUse === false) {");
            codeBuilder.AddLine("                   //Check Client Validations");
            codeBuilder.AddLine("                   if (!ownerReference.hasValidClientFilter(lookupName, lookupInfo)) { if (finished) finished(false); return; }");
            codeBuilder.AddLine("                   //Show External Lookup");
            codeBuilder.AddLine("                   currentModal.showModal(specializedLookup.moduleName, specializedLookup.uiSettings, ownerReference.getLookupDisplay(lookupName), ['Ok', 'Cancelar'], 'large').then(function (r, data) {");
            codeBuilder.AddLine("                   if (r == 'Ok') {");
            codeBuilder.AddLine("                       if (!currentModal.internalUIs || currentModal.internalUIs.length != 1) { if (finished) finished(false); return; }");
            codeBuilder.AddLine("                       var lookupVM = currentModal[currentModal.internalUIs[0]]; ");
            codeBuilder.AddLine("                       if (!lookupVM) return; ");
            codeBuilder.AddLine("                       if (typeof lookupVM == 'function') lookupVM = lookupVM(); ");
            codeBuilder.AddLine("                       currentModal[currentModal.internalUIs[0]] = null; ");
            codeBuilder.AddLine("                       var selectedItems = lookupVM.getSpecializedLookupItems(); ");
            codeBuilder.AddLine("                       if (vm.status() == 'C' && selectedItems != null && selectedItems.length > 1) { ");
            codeBuilder.AddLine("                           var results = '';");
            codeBuilder.AddLine("                           $.each(selectedItems, function (index, item) { results += (index == 0 ? '' : ',') + (typeof item[lookupFieldName] == 'function' ? item[lookupFieldName]() : item[lookupFieldName]).toString().trim() });");
            codeBuilder.AddLine("                           results = '[' + results + ']'");
            codeBuilder.AddLine("                           ownerReference[fieldToSearch](results);");
            codeBuilder.AddLine("                           if (vm.entitySearchRange[ownerReference.typeName + fieldToSearch] === undefined)");
            codeBuilder.AddLine("                               vm.entitySearchRange[ownerReference.typeName + fieldToSearch] = ko.observable(results);");
            codeBuilder.AddLine("                           else vm.entitySearchRange[ownerReference.typeName + fieldToSearch](results);");
            codeBuilder.AddLine("                           document.dispatchEvent(dataUpdateEvent);");
            codeBuilder.AddLine("                           if (finished) finished(true, results);");
            codeBuilder.AddLine("                       }");
            codeBuilder.AddLine("                       else if (selectedItems.length > 0) { dataContext['finalizeAll' + lookupName](ownerReference, selectedItems, '', lookupInfo); }");
            codeBuilder.AddLine("                   }");
            codeBuilder.AddLine("                   if (finished) finished(r === 'Ok');");
            codeBuilder.AddLine("               });");
            codeBuilder.AddLine("           }");
            codeBuilder.AddLine("           if (finished) finished(false);");
            codeBuilder.AddLine("       }");



            codeBuilder.AddLine("   };");
            codeBuilder.AddLine("   ownerReference.clearLookUp = function (lookupName) {");
            codeBuilder.AddLine("       return eval('dataContext.clear' + lookupName + '(ownerReference)');");
            codeBuilder.AddLine("   };");
        }



        #endregion

    }
}

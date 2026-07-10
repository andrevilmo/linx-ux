using EnvDTE;
using Linx.Builder.Resources;
using Linx.EntityAdapterDesigner.CustomizedCode;
using Linx.EntityAdapterDesigner.CustomizedCode.Apps.ClientErp;
using Linx.EntityAdapterDesigner.CustomizedCode.Apps.Mobile;
using Linx.EntityAdapterDesigner.CustomizedCode.Apps.SPA;
using Linx.Tools;
using Microsoft.CSharp;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Diagrams;
using Microsoft.Win32;
using NuGet.VisualStudio;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Collections;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using VSLangProj80;
using DslModeling = global::Microsoft.VisualStudio.Modeling;



namespace Linx.EntityAdapterDesigner
{

    public partial class EntityAdapterDesignerRoot
    {
        #region Utilities        
        public Project GetEadCoreProject(Project eadProject)
        {
            string eadCoreProjectName = GeEadCoreProjectName(eadProject);
            return GetProjectByName(eadCoreProjectName);
        }
        private void AddBMDs(Project eadCoreProject)
        {
            string bmPathLib = this.GetFullPath("Linx.CoreBusinessModels");
            foreach (var bm in this.EntityDataModels)
            {
                string bmPath = System.IO.Path.Combine(bmPathLib, System.IO.Path.GetFileName(bm.Path));
                if (!File.Exists(bmPath))
                    bmPath = System.IO.Path.Combine(bmPathLib, System.IO.Path.GetFileNameWithoutExtension(bm.Path) + ".Core.dll");
                if (File.Exists(bmPath))
                {
                    AddNewReference(eadCoreProject, bmPath);
                }
            }
        }

        public void AdjustProjectCorePublishing(Project eadCoreProject)
        {
            string eadPath = this.GetFullPath("Linx.CoreBusinessViews");
            if (!eadPath.IsNullOrEmpty() && Directory.Exists(eadPath))
            {
                string relativePath = this.GetOutputPath(eadCoreProject).GetRelativePath(eadPath);
                if (!relativePath.IsNullOrEmpty())
                    eadPath = relativePath;

                var parts = this.GetOutputPathPart(eadCoreProject).Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
                string prjDir = String.Join("", parts.Select(e => "..\\"));

                string assemblyName = GetAssemblyName(eadCoreProject) + ".dll";
                string settingsFileName = assemblyName + ".json";
                string contextMetadataFile = this.GetContextMetadataFile(eadCoreProject);

                string postBuildEventCommand = @"cd ""$(TargetDir)""" + "\r\n";
                postBuildEventCommand += @"xcopy """ + prjDir + @"appsettings.json"" ""."" /Y /R" + "\r\n";
                postBuildEventCommand += @"rename ""appsettings.json"" """ + settingsFileName + @"""" + "\r\n";

                if (!contextMetadataFile.IsNullOrEmpty())
                {
                    string metadataFileName = assemblyName + ".meta.json";
                    postBuildEventCommand += @"xcopy """ + Path.Combine(prjDir, "Model\\" + contextMetadataFile) + @""" ""."" /Y /R" + "\r\n";
                    postBuildEventCommand += @"del """ + metadataFileName + @"""" + "\r\n";
                    postBuildEventCommand += @"rename """ + contextMetadataFile + @""" """ + metadataFileName + @"""" + "\r\n";
                }

                postBuildEventCommand += @"xcopy """ + assemblyName + @"*"" """ + eadPath + @""" /Y /R" + "\r\n";
                postBuildEventCommand += GetCoreServiceBusCopyCommand(eadCoreProject, "BV");
                string postEventValue = eadCoreProject.Properties.Item("PostBuildEvent").Value as string;
                if (postEventValue.IsNullOrEmpty() || !postEventValue.StartsWith(postBuildEventCommand))
                    eadCoreProject.Properties.Item("PostBuildEvent").Value = postBuildEventCommand;

                string coreHost = this.GetFullPath("Linx.CoreServiceBus");
                if (!coreHost.IsNullOrEmpty() && Directory.Exists(coreHost))
                {
                    var item = GetProjectItemByName(eadCoreProject, "appsettings.json");
                    if (item != null)
                    {
                        var sourcePath = item.Properties.Item("FullPath").Value.ToString();
                        var targetPath = Path.Combine(coreHost, "appsettings.json");
                        SerializationManager.MergeJsonConnectionStrings(sourcePath, targetPath);
                    }
                }
            }
        }

        public string GetWebApiCoreProjectName(string projectSuffix, Project eadProject)
        {
            return eadProject.Name + ".WebAPICore" + (projectSuffix.IsNullOrEmpty() ? String.Empty : "." + projectSuffix);
        }

        public Project GetWebApiCoreProject(string projectSuffix, Project eadProject = null)
        {
            if (eadProject == null)
                eadProject = GetEadProject();
            if (eadProject != null)
                return this.GetProjectByName(GetWebApiCoreProjectName(projectSuffix, eadProject));
            else
                return null;
        }

        public string GeEadCoreProjectName(Project eadProject)
        {
            return eadProject.Name + ".Core";
        }

        private void InstallEntityFramework(Project project)
        {
            InstallNuGetPackage("System.Linq.Dynamic.Core", "1.0.8", project);
            InstallNuGetPackage("Microsoft.EntityFrameworkCore.Tools", "2.0.1", project);
        }

        private void InstallAssets(Project project)
        {
            InstallNuGetPackage("System.Composition", "1.1.0", project);
            InstallNuGetPackage("System.Reflection", "4.3.0", project);
            InstallNuGetPackage("System.Reflection.Extensions", "4.3.0", project);
            InstallNuGetPackage("System.Reflection.TypeExtensions", "4.4.0", project);
            InstallNuGetPackage("System.Runtime.Serialization.Primitives", "4.3.0", project);
            InstallNuGetPackage("System.Xml.XmlSerializer", "4.3.0", project);
        }

        public string GetOutputPathPart(Project vsProject)
        {
            return vsProject.ConfigurationManager.ActiveConfiguration.Properties.Item("OutputPath").Value.ToString();
        }

        public string GetOutputPath(Project vsProject)
        {
            string fullPath = vsProject.Properties.Item("FullPath").Value.ToString();
            string outputPath = GetOutputPathPart(vsProject);
            return Path.Combine(fullPath, outputPath);
        }

        private string GetCoreServiceBusCopyCommand(Project project, string apiPart, string coreHost = "")
        {
            string postBuildEventCommand = "";

            if (coreHost.IsNullOrEmpty())
                coreHost = this.GetFullPath("Linx.CoreServiceBus");

            if (!coreHost.IsNullOrEmpty() && Directory.Exists(coreHost))
            {
                string corPrjName = project.Name;
                string dirPart = "BusinessModules\\" + apiPart + "\\" + corPrjName + "\\bin";
                var serviceBusPath = Path.Combine(coreHost, dirPart);
                if (!Directory.Exists(serviceBusPath))
                {
                    Directory.CreateDirectory(serviceBusPath);
                }

                if (!serviceBusPath.IsNullOrEmpty() && Directory.Exists(serviceBusPath))
                {
                    string relativePath = this.GetOutputPath(project).GetRelativePath(serviceBusPath);
                    if (!relativePath.IsNullOrEmpty())
                        serviceBusPath = "\\" + relativePath;

                    postBuildEventCommand = GetServiceBusCopyCommands(project, serviceBusPath, false, "");
                }
            }

            return postBuildEventCommand;
        }

        public void AdjustWebApiCorePublishing(WebApiController coreApi, Project webApiProject)
        {
            //Core Service Bus Publishing
            string coreHost = this.GetFullPath("Linx.CoreServiceBus");
            if (!coreHost.IsNullOrEmpty() && Directory.Exists(coreHost))
            {
                string postBuildEventCommand = @"cd ""$(TargetDir)""" + "\r\n";
                postBuildEventCommand += GetCoreServiceBusCopyCommand(webApiProject, "API", coreHost);
                string postEventValue = webApiProject.Properties.Item("PostBuildEvent").Value as string;
                if (postEventValue.IsNullOrEmpty() || !postEventValue.StartsWith(postBuildEventCommand))
                    webApiProject.Properties.Item("PostBuildEvent").Value = postBuildEventCommand;
            }
        }

        #endregion

        #region Projects
        public void UpdateEadCoreProject(Project eadProject)
        {
            if (!this.IsAspNetCore)
                return;

            EnvDTE.DTE appDTE = eadProject.DTE;
            string eadCoreProjectName = GeEadCoreProjectName(eadProject);
            Project eadCoreProject = GetProjectByName(eadCoreProjectName);
            string folderName = "Business Rules Core";
            EnvDTE80.SolutionFolder bmCoreDesignerFolder = null;

            if (eadCoreProject == null)
            {
                string bmCoreDir = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(eadProject.FullName), "..\\" + eadCoreProjectName));

                if (!System.IO.Directory.Exists(bmCoreDir))
                    System.IO.Directory.CreateDirectory(bmCoreDir);

                var tmpProj = this.GetProjectByName(folderName);
                bmCoreDesignerFolder = (tmpProj == null ? null : tmpProj.Object) as EnvDTE80.SolutionFolder;
                if (bmCoreDesignerFolder == null)
                    bmCoreDesignerFolder = (EnvDTE80.SolutionFolder)((EnvDTE100.Solution4)appDTE.Solution).AddSolutionFolder(folderName).Object;

                if (System.IO.File.Exists(System.IO.Path.Combine(bmCoreDir, eadCoreProjectName + ".csproj")))
                {
                    bmCoreDesignerFolder.AddFromFile(System.IO.Path.Combine(bmCoreDir, eadCoreProjectName + ".csproj"));
                }
                else
                {
                    // Get the location of the project templates
                    string templateName = ((EnvDTE100.Solution4)appDTE.Solution).GetProjectTemplate("Linx Class Lib (.NET Core).zip", "CSharp");
                    bmCoreDesignerFolder.AddFromTemplate(templateName, bmCoreDir, eadCoreProjectName);
                    
                    eadCoreProject = GetProjectByName(eadCoreProjectName);

                    //Set Assembly Name
                    eadCoreProject.Properties.Item("AssemblyName").Value = eadCoreProjectName;
                }
            }
            
            //Adjust Core Libs
            string coreLibs = this.GetFullPath("Linx.CoreLibs");
            if (!coreLibs.IsNullOrEmpty() && Directory.Exists(coreLibs))
            {
                UpdateReference(eadCoreProject, Path.Combine(coreLibs, "Linx.Tools.Core.dll"));
                UpdateReference(eadCoreProject, Path.Combine(coreLibs, "Linx.LinqExtensions.Core.dll"));
                UpdateReference(eadCoreProject, Path.Combine(coreLibs, "Linx.Data.Core.dll"));
                UpdateReference(eadCoreProject, Path.Combine(coreLibs, "Linx.DataService.Core.dll"));
                UpdateReference(eadCoreProject, Path.Combine(coreLibs, "Linx.DomainService.Core.dll"));
            }

            //Add Business Models
            this.AddBMDs(eadCoreProject);

            //Install Entity Framework Core
            InstallEntityFramework(eadCoreProject);

            //Install Assets
            InstallAssets(eadCoreProject);

            //Adjust AppSettings.json
            UpdateAppSettingsTemplate(eadCoreProject);

            //Adjust BM Core Publishing
            AdjustProjectCorePublishing(eadCoreProject);
        }

        public void UpdateWebApiCoreProject(Project eadProject, WebApiController api)
        {
            if (!this.IsAspNetCore)
                return;

            EnvDTE.DTE appDTE = eadProject.DTE;
            string webApiProjectName = GetWebApiCoreProjectName(api.ProjectSuffix, eadProject);
            Project webApiProject = GetProjectByName(webApiProjectName);
            string folderName = "Web API Controllers Core";
            EnvDTE80.SolutionFolder webApiDesignerFolder = null;

            if (webApiProject == null)
            {
                string webApiDir = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(eadProject.FullName), "..\\" + webApiProjectName));

                if (!System.IO.Directory.Exists(webApiDir))
                    System.IO.Directory.CreateDirectory(webApiDir);

                var tmpProj = this.GetProjectByName(folderName);
                webApiDesignerFolder = (tmpProj == null ? null : tmpProj.Object) as EnvDTE80.SolutionFolder;
                if (webApiDesignerFolder == null)
                    webApiDesignerFolder = (EnvDTE80.SolutionFolder)((EnvDTE100.Solution4)appDTE.Solution).AddSolutionFolder(folderName).Object;

                if (System.IO.File.Exists(System.IO.Path.Combine(webApiDir, webApiProjectName + ".csproj")))
                {
                    webApiDesignerFolder.AddFromFile(System.IO.Path.Combine(webApiDir, webApiProjectName + ".csproj"));
                }
                else
                {
                    // Create by project template
                    string templateName = ((EnvDTE100.Solution4)appDTE.Solution).GetProjectTemplate("Linx WebApi (.NET Core).zip", "CSharp");
                    webApiDesignerFolder.AddFromTemplate(templateName, webApiDir, webApiProjectName);
                    
                    webApiProject = GetProjectByName(webApiProjectName);

                    webApiProject.ProjectItems.AddFolder("App_Start");
                    webApiProject.ProjectItems.AddFolder("Controllers");

                    //Set Assembly Name
                    webApiProject.Properties.Item("AssemblyName").Value = webApiProjectName;
                }
            }
            
            //Adjust Core Libs
            string coreLibs = this.GetFullPath("Linx.CoreLibs");
            if (!coreLibs.IsNullOrEmpty() && Directory.Exists(coreLibs))
            {
                UpdateReference(webApiProject, Path.Combine(coreLibs, "Linx.Tools.Core.dll"));
                UpdateReference(webApiProject, Path.Combine(coreLibs, "Linx.LinqExtensions.Core.dll"));
                UpdateReference(webApiProject, Path.Combine(coreLibs, "Modular.Core.dll"));
                UpdateReference(webApiProject, Path.Combine(coreLibs, "Microsoft.AspNetCore.OData.dll"));
                UpdateReference(webApiProject, Path.Combine(coreLibs, "Linx.DataService.Core.dll"));
                UpdateReference(webApiProject, Path.Combine(coreLibs, "Linx.Data.Core.dll"));
                UpdateReference(webApiProject, Path.Combine(coreLibs, "Linx.DomainService.Core.dll"));
            }

            InstallNuGetPackage("Newtonsoft.Json", "10.0.3", webApiProject);
            InstallNuGetPackage("System.Linq.Dynamic.Core", "1.0.8", webApiProject);

            //Add project reference
            string eadCoreProjectName = GeEadCoreProjectName(eadProject);
            Project eadCoreProject = GetProjectByName(eadCoreProjectName);
            AddProjectReference(webApiProject, eadCoreProject, true);
            
            //Adjust WebApi Core Publishing
            AdjustWebApiCorePublishing(api, webApiProject);
        }

        #endregion

        #region Templates
        public void UpdateAppSettingsTemplate(Project eadCoreProject)
        {
            string outputFile = "", templateName;
            ProjectItem newItem = null;
            string body = ReadResourceContent(@"Linx.EntityAdapterDesigner.CoreTemplates.AppSettingsTemplate.txt");

            if (!eadCoreProject.IsNull())
            {
                if (ExistsProjectItem(eadCoreProject.ProjectItems, "appsettings.json"))
                    eadCoreProject.ProjectItems.Item("appsettings.json").Delete();

                templateName = "appsettings.tt";
                outputFile = Path.Combine(GetProjectPath(eadCoreProject), templateName);
                if (!this.VerifySourceControl(outputFile))
                    return;

                bool newFile = false;
                if (!ExistsProjectItem(eadCoreProject.ProjectItems, templateName))
                {
                    File.WriteAllText(outputFile, body);
                    newItem = eadCoreProject.ProjectItems.AddFromFile(outputFile);
                    //newItem.Properties.Item("CustomTool").Value = "TextTemplatingFileGenerator";
                    newFile = true;
                }
                else
                {
                    if (File.ReadAllText(outputFile) != body)
                        File.WriteAllText(outputFile, body);
                    newItem = eadCoreProject.ProjectItems.Item(templateName);
                }
                //Run Template
                if (newFile || !IsAutomaticSaving)
                    ((VSLangProj.VSProjectItem)newItem.Object).RunCustomTool();
            }
        }

        public string GetContextMetadataFile(Project bvProject)
        {
            string fileName = "";
            string folderName = "Model";
            string ctxFile = "ContextMetadata.tt";
            var folder = GetProjectItemByName(bvProject, folderName);
            if (folder != null)
            {
                var newItem = GetProjectItemByName(folder.ProjectItems, ctxFile);
                if (newItem != null)
                {
                    foreach (ProjectItem gItem in newItem.ProjectItems)
                    {
                        fileName = gItem.Name;
                        break;
                    }
                }
            }

            return fileName;
        }

        public void UpdateContextMetadata()
        {
            Project current = this.GetEadProject();
            if (this.IsAspNetCore)
                current = this.GetEadCoreProject(current);
            
            string outputFile = "";
            ProjectItem folder = null, newItem;

            if (current != null)
            {
                string folderName = "Model";
                string body = AdjustGacPath(ReadResourceContent(@"Linx.EntityAdapterDesigner.Templates.ContextMetadata.txt"));

                string ctxFile = "ContextMetadata.tt";
                folder = GetProjectItemByName(current, folderName);
                if (folder == null)
                {
                    try
                    {
                        folder = current.ProjectItems.AddFolder(folderName);
                    }
                    catch
                    {
                        folder = null;
                    }
                }

                if (folder != null)
                {
                    outputFile = Path.Combine(Path.Combine(GetProjectPath(current), folderName), ctxFile);
                    if (!this.VerifySourceControl(outputFile))
                        return;

                    if (!ExistsProjectItem(folder.ProjectItems, ctxFile))
                    {
                        File.WriteAllText(outputFile, body);
                        newItem = folder.ProjectItems.AddFromFile(outputFile);
                    }
                    else
                    {
                        if (File.ReadAllText(outputFile) != body)
                            File.WriteAllText(outputFile, body);
                        newItem = folder.ProjectItems.Item(ctxFile);
                    }

                    //Run Template and Generate All Classes Files
                    ((VSLangProj.VSProjectItem)newItem.Object).RunCustomTool();

                    if (this.IsAspNetCore)
                        this.AdjustProjectCorePublishing(current);
                    else
                        this.SetPostBuildEventToServiceBus(current);

                }
            }
        }

        private void UpdateCoreTemplates(Project eadProject)
        {
            if (!this.IsAspNetCore || eadProject == null)
                return;

            var coreProject = this.GetEadCoreProject(eadProject);

            //Check Diagram Folder In The Core Project
            var item = GetDiagramProjectItem(coreProject);
            if (item == null)
            {
                string forlderName = Path.GetFileNameWithoutExtension(this.DocumentName);
                coreProject.ProjectItems.AddFolder(forlderName);
            }
            ////////////////////////////////////////

            try
            {
                this.UpdateDataEntityFunctionsTemplate(coreProject, true);
                this.UpdateEntityAdapterDynamicModelsTemplate(coreProject, false, eadProject);
                this.UpdateEntityAdapterDynamicModelsTemplate(coreProject, true, eadProject);

                if (this.WebApiControllers.Count > 0)
                {
                    //.Net Core
                    this.GenerateWebApiCoreInitialize(eadProject);
                    this.GenerateWebApiCoreControllersCode(eadProject);
                }


                this.UpdateDomainViewsTemplate(coreProject, "DomainViewsTemplate", "DomainViews");
                this.UpdateDomainViewsTemplate(coreProject, "ClientDataDomains", "DataDomains");
                this.UpdateDomainViewsTemplate(coreProject, "MobileDataDomains", "MobileDataDomains");
                this.UpdateDomainViewsTemplate(coreProject, "ClientErpDataDomainsFactory", "ClientErpDataDomainsFactory");
                this.UpdateClientServicesResources(coreProject);


                if (this.KeyPerformanceIndicators.Count > 0)
                    this.UpdateKPIViewsTemplate(coreProject);


                if (!(this.EntityAdapters.Count == 0 && this.DomainServiceExtensions.Count == 0))
                {
                    //Templates                                                    
                    this.UpdateDomainServiceTemplate(coreProject, eadProject, true);
                    this.UpdateFormulasTemplate(coreProject, eadProject);
                    this.UpdateExtendedFiltersTemplate(coreProject, eadProject);
                    this.UpdateLookUpsTemplate(coreProject, eadProject);

                    //Operations
                    this.GenerateBusinessEvents(coreProject, false);
                    this.GenerateBusinessEvents(coreProject, true);
                    this.GenerateBusinessOperations(coreProject, false);
                    this.GenerateBusinessOperations(coreProject, true);
                    this.GenerateDomainServiceExtensions(coreProject);
                    this.CheckCustomValidationClass(coreProject);
                    this.DeleteInconsistentFiles(coreProject);
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
        #endregion

        #region  WebAPI Core Controllers
        private void GenerateWebApiCoreInitialize(Project eadProject)
        {
            if (!this.IsAspNetCore)
                return;

            string outputFile;

            if (this.IsAspNetCore && eadProject != null)
            {
                Project webApiProject;
                ProjectItem item;
                Linx.Tools.CodeBuilder codeBuilder;

                foreach (var api in this.WebApiControllers.Where(c => c.IsDataService && c.SynchronizedWithDomainService))
                {
                    webApiProject = this.GetWebApiCoreProject(api.ProjectSuffix, eadProject);

                    if (webApiProject != null)
                    {
                        item = GetWebApiAppStartItem(api, webApiProject);
                        if (!item.IsNull())
                        {
                            outputFile = Path.Combine(this.GetProjectPath(webApiProject), Path.GetFileNameWithoutExtension(item.Name) + "\\ModuleInitializer_" + api.GetRoutePrefix() + ".cs");
                            codeBuilder = new Linx.Tools.CodeBuilder();
                            this.GenerateWebApiCoreInitializer(codeBuilder, api, webApiProject, item);

                            //Checkout
                            if (File.Exists(outputFile) && !this.VerifySourceControl(outputFile))
                                return;

                            if (!File.Exists(outputFile) || !ExistsProjectItem(item.ProjectItems, System.IO.Path.GetFileName(outputFile)))
                            {
                                RemoveProjectItems(item.ProjectItems, System.IO.Path.GetFileName(outputFile));
                                //Write code to file
                                System.IO.File.WriteAllText(outputFile, codeBuilder.GetBody());
                                //Add project item.
                                item.ProjectItems.AddFromFile(outputFile);
                            }
                            else
                            {
                                //Write code to file
                                System.IO.File.WriteAllText(outputFile, codeBuilder.GetBody());
                            }
                        }
                    }
                }
            }
        }

        private void GenerateWebApiCoreInitializer(Linx.Tools.CodeBuilder codeBuilder, WebApiController api, Project webApiProject, ProjectItem itemAppStart)
        {
            if (!this.IsAspNetCore)
                return;

            var item = GetWebApiAppStartItem(api, webApiProject);

            codeBuilder.AddLine("using Microsoft.Extensions.DependencyInjection;");
            codeBuilder.AddLine("using Modular.Core;");
            codeBuilder.AddLine("using System;");
            codeBuilder.AddLine("using Microsoft.AspNetCore.OData.Extensions;");
            codeBuilder.AddLine("using Microsoft.AspNetCore.Builder;");
            codeBuilder.AddLine("using Microsoft.OData.Edm;");
            codeBuilder.AddLine("using Microsoft.AspNetCore.OData.Builder;");
            codeBuilder.AddLine("using Microsoft.AspNetCore.OData;");
            codeBuilder.AddLine("using Microsoft.AspNetCore.Hosting;");
            if (!(this.EntityAdapters.Count == 0 && this.DomainServiceExtensions.Count == 0))
                codeBuilder.AddLine("using BusinessNS = " + api.EntityAdapterDesignerRoot.GetDirectContextNamespace() + ";");
            codeBuilder.AddLine();
            codeBuilder.AddLine("namespace " + webApiProject.Name + "." + item.Name);
            codeBuilder.AddLine("{");

            //Class Definition
            codeBuilder.AddLine();

            codeBuilder.IncreaseIndent();

            codeBuilder.AddLine("public class ModuleInitializer_" + api.Name + " : IModuleInitializer");
            codeBuilder.AddLine("{");
            codeBuilder.IncreaseIndent();

            codeBuilder.AddLine();
            codeBuilder.AddLine("private static IHostingEnvironment _hostingEnvironment;");
            codeBuilder.AddLine("public static string MapPath(string complement)");
            codeBuilder.AddLine("{");
            codeBuilder.AddLine("    var webRoot = _hostingEnvironment.WebRootPath;");
            codeBuilder.AddLine("    return System.IO.Path.Combine(webRoot, complement);");
            codeBuilder.AddLine("}");

            codeBuilder.AddLine();
            codeBuilder.AddLine("public void Init(IServiceCollection services, IHostingEnvironment hostingEnvironment)");
            codeBuilder.AddLine("{");
            codeBuilder.AddLine("    _hostingEnvironment = hostingEnvironment;");
            codeBuilder.AddLine("    services.AddOData();");
            codeBuilder.AddLine("}");

            codeBuilder.AddLine();
            codeBuilder.AddLine("public void Init(IApplicationBuilder app)");
            codeBuilder.AddLine("{");
            codeBuilder.AddLine("    _serviceProvider = app.ApplicationServices;");
            codeBuilder.AddLine("    _model = GetEdmModel(_serviceProvider.GetRequiredService<IAssemblyProvider>());");
            codeBuilder.AddLine("    app.UseMvc(builder => {");
            codeBuilder.AddLine("        builder.MapODataRoute(\"" + api.GetRoutePrefix() + "\", _model);");
            codeBuilder.AddLine("    });");
            codeBuilder.AddLine("}");

            codeBuilder.AddLine();
            codeBuilder.AddLine("private static IServiceProvider _serviceProvider;");
            codeBuilder.AddLine("public static IServiceProvider ServiceProvider { get { return _serviceProvider; } }");
            codeBuilder.AddLine("private static IEdmModel _model;");
            codeBuilder.AddLine("public static IEdmModel Model");
            codeBuilder.AddLine("{");
            codeBuilder.AddLine("    get");
            codeBuilder.AddLine("    {");
            codeBuilder.AddLine("        return _model;");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("}");

            codeBuilder.AddLine();
            codeBuilder.AddLine("private static IEdmModel GetEdmModel(IAssemblyProvider assemblyProvider)");
            codeBuilder.AddLine("{");
            codeBuilder.AddLine("    var modelBuilder = new ODataConventionModelBuilder(assemblyProvider);");

            foreach (var entityAdapter in this.EntityAdapters.Where(e => e.ExposeAsService))
            {
                codeBuilder.AddLine("    modelBuilder.EntitySet<BusinessNS.{0}>(\"{0}\");", entityAdapter.Name);
                if (entityAdapter.TargetEntityAdapter != null && entityAdapter.IsParentCompositionAllowed())
                    codeBuilder.AddLine("    modelBuilder.EntitySet<BusinessNS.{0}>(\"{0}\");", entityAdapter.Name + "ParentComposition");
            }

            foreach (var lookUp in this.LookUpAdapters.Where(e => e.EntityAdapter != null && e.EntityAdapter.ExposeAsService))
            {
                codeBuilder.AddLine("    modelBuilder.EntitySet<BusinessNS.{0}>(\"{0}\");", lookUp.Name);
            }

            codeBuilder.AddLine("    var model = modelBuilder.GetEdmModel();");
            codeBuilder.AddLine("    return model;");
            codeBuilder.AddLine("}");


            codeBuilder.DecreaseIndent();
            codeBuilder.AddLine("}");

            codeBuilder.DecreaseIndent();
            //End Class Definition

            //End Namespace
            codeBuilder.AddLine("}");
        }

        public void GenerateWebApiCoreControllersCode(Project eadProject)
        {
            string outputFile;
            if (this.IsAspNetCore && eadProject != null)
            {
                Project webApiProject;
                ProjectItem item;
                Linx.Tools.CodeBuilder codeBuilder;

                foreach (var api in this.WebApiControllers)
                {
                    webApiProject = this.GetWebApiCoreProject(api.ProjectSuffix, eadProject);
                    if (webApiProject != null)
                    {
                        item = this.GetWebApiControllersItem(api, webApiProject);
                        if (!item.IsNull())
                        {
                            //Automatic Controller
                            //Get automatic code
                            codeBuilder = new Linx.Tools.CodeBuilder();
                            this.GenerateWebApiAutomaticControllerCode(codeBuilder, api, webApiProject, eadProject, true);
                            outputFile = this.GetWebApiClassFile(api, webApiProject, true);
                            WriteFile(outputFile, codeBuilder, item.ProjectItems);

                            //Custom Controller
                            outputFile = this.GetWebApiClassFile(api, webApiProject);
                            if (!File.Exists(outputFile) || !ExistsProjectItem(item.ProjectItems, System.IO.Path.GetFileName(outputFile)))
                            {
                                if (!this.VerifySourceControl(outputFile))
                                    return;

                                RemoveProjectItems(item.ProjectItems, System.IO.Path.GetFileName(outputFile));
                                codeBuilder = new Linx.Tools.CodeBuilder();
                                this.GenerateWebApiControllerCode(codeBuilder, api, webApiProject, eadProject, true);
                                string body = codeBuilder.ToString();

                                //Add Events
                                var webApiOriginalProject = this.GetWebApiProject(api.ProjectSuffix, eadProject);
                                if (webApiOriginalProject != null)
                                {
                                    var outputOriginalFile = this.GetWebApiClassFile(api, webApiOriginalProject);
                                    if (File.Exists(outputOriginalFile))
                                    {
                                        var originalCode = File.ReadAllText(outputOriginalFile);
                                        string mark = "public partial class " + api.Name + "Controller";

                                        body = body.Left(mark) + mark + originalCode.Right(mark);
                                    }
                                }

                                System.IO.File.WriteAllText(outputFile, body);
                                //Add project item.
                                item.ProjectItems.AddFromFile(outputFile);
                            }

                        }
                    }
                }
            }
        }

        #endregion

    }
}

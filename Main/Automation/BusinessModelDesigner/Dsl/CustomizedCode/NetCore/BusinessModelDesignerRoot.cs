using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnvDTE;
using System.IO;
using Linx.Tools;
using Linx.Builder.Resources;
using System.CodeDom;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Collections;
using System.Xml;
using System.Reflection;
using DslModeling = global::Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Integration;
using Linx.BusinessModelDesigner.CustomCode;
using Microsoft.VisualStudio.Modeling.Diagrams;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Modeling.Immutability;
using NuGet.VisualStudio;
using Microsoft.VisualStudio.ComponentModelHost;
using VSLangProj80;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Modeling.Validation;
using System.Globalization;
using Linx.Tools.Migration;
using System.Diagnostics;

namespace Linx.BusinessModelDesigner
{
    public partial class BusinessModelDesignerRoot
    {
        #region Utilities

        private void InstallEntityFramework(Project project)
        {
            InstallNuGetPackage("System.Linq.Dynamic.Core", "1.0.8", project);
            InstallNuGetPackage("Microsoft.EntityFrameworkCore", "2.0.1", project);
            InstallNuGetPackage("Microsoft.EntityFrameworkCore.Tools", "2.0.1", project);
            InstallNuGetPackage("Microsoft.Extensions.Configuration.Abstractions", "2.0.0", project);
            switch (this.GetDefaultProvider())
            {
                case Provider.SQLServer:
                    InstallNuGetPackage("Microsoft.EntityFrameworkCore.SqlServer", "2.0.1", project);
                    break;
                case Provider.Oracle:
                    break;
                case Provider.SQLite:
                    InstallNuGetPackage("Microsoft.EntityFrameworkCore.SqLite", "2.0.1", project);
                    break;
                case Provider.MySQL:
                    InstallNuGetPackage("MySql.Data.EntityFrameworkCore", "6.10.4", project);//8.0.8-dmr
                    break;
                case Provider.PostgreSQL:
                    InstallNuGetPackage("Npgsql.EntityFrameworkCore.PostgreSQL", "2.0.0", project);
                    break;
                default:
                    break;
            }
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

        public string GeBmCoreProjectName(Project bmProject)
        {
            return bmProject.Name + ".Core";
        }

        public string GetAssemblyName()
        {
            return GetAssemblyName(this.GetBmdProject());
        }

        public string GetAssemblyName(Project current)
        {
            return (current == null ? String.Empty : (string)current.Properties.Item("AssemblyName").Value);
        }

        private void SaveDocument(ProjectItem item, bool close)
        {
            Window window = item.Open("{7651A702-06E5-11D1-8EBD-00A0C90F26EA}");
            window.SetFocus();
            window.Document.Save();
            if (close)
                window.Close();
        }

        public Project GetBmdCoreProject(Project bmProject)
        {
            string bmCoreProjectName = GeBmCoreProjectName(bmProject);
            return GetProjectByName(bmCoreProjectName);
        }

        public string GetWebApiCoreProjectName(string projectSuffix, Project eadProject)
        {
            return eadProject.Name + ".WebAPICore" + (projectSuffix.IsNullOrEmpty() ? String.Empty : "." + projectSuffix);
        }

        public Project GetWebApiCoreProject(string projectSuffix, Project bmdProject = null)
        {
            if (bmdProject == null)
                bmdProject = GetBmdProject();
            if (bmdProject != null)
                return this.GetProjectByName(GetWebApiCoreProjectName(projectSuffix, bmdProject));
            else
                return null;
        }

        public void AdjustProjectCorePublishing(Project bmCoreProject)
        {
            string bmPath = this.GetFullPath("Linx.CoreBusinessModels");
            if (!bmPath.IsNullOrEmpty() && Directory.Exists(bmPath))
            {
                string relativePath = this.GetOutputPath(bmCoreProject).GetRelativePath(bmPath);
                if (!relativePath.IsNullOrEmpty())
                    bmPath = relativePath;

                var parts = this.GetOutputPathPart(bmCoreProject).Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
                string prjDir = String.Join("", parts.Select(e => "..\\"));

                string assemblyName = GetAssemblyName(bmCoreProject) + ".dll";
                string settingsFileName = assemblyName + ".json";                
                string contextMetadataFile = this.GetContextMetadataFile(bmCoreProject);
                
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
                postBuildEventCommand += @"xcopy """ + assemblyName + @"*"" """ + bmPath + @""" /Y /R" + "\r\n";
                postBuildEventCommand += GetCoreServiceBusCopyCommand(bmCoreProject, "BM");
                string postEventValue = bmCoreProject.Properties.Item("PostBuildEvent").Value as string;
                if (postEventValue.IsNullOrEmpty() || !postEventValue.StartsWith(postBuildEventCommand))
                    bmCoreProject.Properties.Item("PostBuildEvent").Value = postBuildEventCommand;

                string coreHost = this.GetFullPath("Linx.CoreServiceBus");
                if (!coreHost.IsNullOrEmpty() && Directory.Exists(coreHost))
                {
                    var item = GetProjectItemByName(bmCoreProject, "appsettings.json");
                    if (item != null)
                    {
                        var sourcePath = item.Properties.Item("FullPath").Value.ToString();
                        var targetPath = Path.Combine(coreHost, "appsettings.json");
                        SerializationManager.MergeJsonConnectionStrings(sourcePath, targetPath);
                    }
                }
            }
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
                        serviceBusPath = relativePath;

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
        public void UpdateBmCoreProject(Project bmProject)
        {
            if (!this.IsAspNetCoreEnabled())
                return;

            EnvDTE.DTE appDTE = bmProject.DTE;
            string bmCoreProjectName = GeBmCoreProjectName(bmProject);
            Project bmCoreProject = GetProjectByName(bmCoreProjectName);
            string folderName = "Business Models Core";
            EnvDTE80.SolutionFolder bmCoreDesignerFolder = null;

            if (bmCoreProject == null)
            {
                string bmCoreDir = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(bmProject.FullName), "..\\" + bmCoreProjectName));

                if (!System.IO.Directory.Exists(bmCoreDir))
                    System.IO.Directory.CreateDirectory(bmCoreDir);

                var tmpProj = this.GetProjectByName(folderName);
                bmCoreDesignerFolder = (tmpProj == null ? null : tmpProj.Object) as EnvDTE80.SolutionFolder;
                if (bmCoreDesignerFolder == null)
                    bmCoreDesignerFolder = (EnvDTE80.SolutionFolder)((EnvDTE100.Solution4)appDTE.Solution).AddSolutionFolder(folderName).Object;

                if (System.IO.File.Exists(System.IO.Path.Combine(bmCoreDir, bmCoreProjectName + ".csproj")))
                {
                    bmCoreDesignerFolder.AddFromFile(System.IO.Path.Combine(bmCoreDir, bmCoreProjectName + ".csproj"));                    
                }
                else
                {
                    // Get the location of the project templates
                    string templateName = ((EnvDTE100.Solution4)appDTE.Solution).GetProjectTemplate("Linx Class Lib (.NET Core).zip", "CSharp");
                    bmCoreDesignerFolder.AddFromTemplate(templateName, bmCoreDir, bmCoreProjectName);
                    bmCoreProject = GetProjectByName(bmCoreProjectName);

                    //Set Assembly Name
                    bmCoreProject.Properties.Item("AssemblyName").Value = bmCoreProjectName;
                }
            }
            

            //Adjust Core Libs
            string coreLibs = this.GetFullPath("Linx.CoreLibs");
            if (!coreLibs.IsNullOrEmpty() && Directory.Exists(coreLibs))
            {
                UpdateReference(bmCoreProject, Path.Combine(coreLibs, "Linx.Tools.Core.dll"));
                UpdateReference(bmCoreProject, Path.Combine(coreLibs, "Linx.LinqExtensions.Core.dll"));
            }

            InstallEntityFramework(bmCoreProject);

            //Adjust AppSettings.json
            UpdateAppSettingsTemplate(bmCoreProject);

            //Adjust BM Core Publishing
            AdjustProjectCorePublishing(bmCoreProject);
        }

        public void UpdateWebApiCoreProject(Project bmdProject, WebApiController api)
        {
            if (!this.IsAspNetCoreEnabled())
                return;

            EnvDTE.DTE appDTE = bmdProject.DTE;
            string webApiProjectName = GetWebApiCoreProjectName(api.ProjectSuffix, bmdProject);
            Project webApiProject = GetProjectByName(webApiProjectName);
            string folderName = "Web API Controllers Core";
            EnvDTE80.SolutionFolder webApiDesignerFolder = null;
            bool saveSolution = false;

            if (webApiProject == null)
            {
                string webApiDir = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(bmdProject.FullName), "..\\" + webApiProjectName));

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
                    // Get the location of the project templates
                    string templateName = ((EnvDTE100.Solution4)appDTE.Solution).GetProjectTemplate("Linx WebApi (.NET Core).zip", "CSharp");
                    webApiDesignerFolder.AddFromTemplate(templateName, webApiDir, webApiProjectName);

                    webApiProject = GetProjectByName(webApiProjectName);

                    webApiProject.ProjectItems.AddFolder("App_Start");
                    webApiProject.ProjectItems.AddFolder("Controllers");

                    //Set Assembly Name
                    webApiProject.Properties.Item("AssemblyName").Value = webApiProjectName;
                }

                saveSolution = true;
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
            }

            InstallEntityFramework(webApiProject);

            //Add project reference
            string bmCoreProjectName = GeBmCoreProjectName(bmdProject);
            Project bmCoreProject = GetProjectByName(bmCoreProjectName);
            AddProjectReference(webApiProject, bmCoreProject, true);
            
            //Adjust WebApi Core Publishing
            AdjustWebApiCorePublishing(api, webApiProject);

            if (saveSolution)
            {
                appDTE.ExecuteCommand("File.SaveAll");
            }
        }
        #endregion

        #region WebAPI Core Controllers
        public void GenerateWebApiCoreInitialize(List<ModelClass> modelClasses)
        {
            if (!this.IsAspNetCoreEnabled())
                return;

            string outputFile;
            Project bmdProject = GetBmdProject();

            if (bmdProject != null)
            {
                Project webApiProject;
                ProjectItem item;
                Linx.Tools.CodeBuilder codeBuilder;

                foreach (var api in this.WebApiControllers.Where(e => (modelClasses == null && !e.ExposeAllContext) || (modelClasses != null && e.ExposeAllContext)))
                {
                    webApiProject = this.GetWebApiCoreProject(api.ProjectSuffix, bmdProject);

                    if (webApiProject != null)
                    {
                        item = GetWebApiAppStartItem(api, webApiProject);
                        if (!item.IsNull())
                        {
                            outputFile = Path.Combine(GetProjectPath(webApiProject), Path.GetFileNameWithoutExtension(item.Name) + "\\ModuleInitializer_" + api.Name + ".cs");
                            codeBuilder = new Linx.Tools.CodeBuilder();
                            this.GenerateWebApiCoreInitializer(modelClasses, codeBuilder, api, webApiProject, item);

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

        private void GenerateWebApiCoreInitializer(List<ModelClass> modelClasses, Linx.Tools.CodeBuilder codeBuilder, WebApiController api, Project webApiProject, ProjectItem itemAppStart)
        {
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
            if (modelClasses != null && modelClasses.Count > 0)
                codeBuilder.AddLine("using BusinessNS = " + api.BusinessModelDesignerRoot.GetNamespace() + ";");
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
            if (modelClasses != null && modelClasses.Count > 0)
            {
                foreach (var entityAdapter in modelClasses)
                {
                    codeBuilder.AddLine("    modelBuilder.EntitySet<BusinessNS.{0}>(\"{0}\");", entityAdapter.Name);
                }
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

        public void GenerateWebApiCoreControllersCode(List<ModelClass> modelClasses, List<BusinessModelDesignerRoot> models)
        {
            if (!this.IsAspNetCoreEnabled())
                return;

            string outputFile;
            Project bmdProject = GetBmdProject();

            if (bmdProject != null)
            {
                Project webApiProject;
                ProjectItem item;
                Linx.Tools.CodeBuilder codeBuilder;

                foreach (var api in this.WebApiControllers.Where(e => (modelClasses == null && !e.ExposeAllContext) || (modelClasses != null && e.ExposeAllContext)))
                {
                    webApiProject = this.GetWebApiCoreProject(api.ProjectSuffix, bmdProject);
                    if (webApiProject != null)
                    {
                        item = this.GetWebApiControllersItem(api, webApiProject);
                        if (!item.IsNull())
                        {
                            //Automatic Controller
                            //Get automatic code
                            codeBuilder = new Linx.Tools.CodeBuilder();
                            this.GenerateWebApiAutomaticControllerCode(modelClasses, models, codeBuilder, api, webApiProject, bmdProject, (this.EnableAutomaticAuthorization ? api.Name + "ControllerAuthorize" : ""), true);
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
                                this.GenerateWebApiControllerCode(codeBuilder, api, webApiProject, true);
                                string body = codeBuilder.ToString();

                                //Add Events
                                var webApiOriginalProject = this.GetWebApiProject(api.ProjectSuffix, bmdProject);
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

        #region Templates
        public void UpdateAppSettingsTemplate(Project bmCoreProject)
        {
            string outputFile = "", templateName;
            ProjectItem newItem = null;
            string body = ReadResourceContent(@"Linx.BusinessModelDesigner.CoreTemplates.AppSettingsTemplate.txt");

            if (!bmCoreProject.IsNull())
            {
                if (ExistsProjectItem(bmCoreProject.ProjectItems, "appsettings.json"))
                    bmCoreProject.ProjectItems.Item("appsettings.json").Delete();

                templateName = "appsettings.tt";
                outputFile = Path.Combine(GetProjectPath(bmCoreProject), templateName);
                if (!this.VerifySourceControl(outputFile))
                    return;

                if (!ExistsProjectItem(bmCoreProject.ProjectItems, templateName))
                {
                    File.WriteAllText(outputFile, body);
                    newItem = bmCoreProject.ProjectItems.AddFromFile(outputFile);
                    //newItem.Properties.Item("CustomTool").Value = "TextTemplatingFileGenerator";
                }
                else
                {
                    if (File.ReadAllText(outputFile) != body)
                        File.WriteAllText(outputFile, body);
                    newItem = bmCoreProject.ProjectItems.Item(templateName);
                }
                //Run Template
                ((VSLangProj.VSProjectItem)newItem.Object).RunCustomTool();
            }
        }

        public string GetBusinessClassesFolder()
        {
            return "Business Classes";
        }

        public void UpdateCoreContextTemplate(Project coreProject)
        {
            if (!this.IsAspNetCoreEnabled())
                return;

            string outputFile = "";
            ProjectItem folder = null, newItem;

            if (coreProject != null)
            {
                string folderName = "Model", bcFolderName = GetBusinessClassesFolder();
                string body = AdjustGacPath(ReadResourceContent(@"Linx.BusinessModelDesigner.CoreTemplates.BusinessModelDesignerTemplate.txt"));

                string ctxFile = "BusinessDataModel.tt";
                folder = GetProjectItemByName(coreProject, folderName);
                if (folder == null)
                    folder = coreProject.ProjectItems.AddFolder(folderName);

                if (folder != null)
                {
                    outputFile = Path.Combine(Path.Combine(GetProjectPath(coreProject), folderName), ctxFile);
                    if (!this.VerifySourceControl(outputFile))
                        return;

                    if (!ExistsProjectItem(folder.ProjectItems, ctxFile))
                    {
                        File.WriteAllText(outputFile, body);
                        newItem = folder.ProjectItems.AddFromFile(outputFile);
                        //newItem.Properties.Item("CustomTool").Value = "TextTemplatingFileGenerator";
                    }
                    else
                    {
                        if (File.ReadAllText(outputFile) != body)
                            File.WriteAllText(outputFile, body);
                        newItem = folder.ProjectItems.Item(ctxFile);
                    }

                    //Run Template and Generate All Classes Files
                    ((VSLangProj.VSProjectItem)newItem.Object).RunCustomTool();

                }

                GenerateOperationalEvents(coreProject, true);

            }
        }

        public string GetContextMetadataFile(Project bmProject)
        {
            string fileName = "";
            string folderName = "Model";
            string ctxFile = "ContextMetadata.tt";
            var folder = GetProjectItemByName(bmProject, folderName);            
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

        public void UpdateContextMetadata(Project current)
        {
            string outputFile = "";
            ProjectItem folder = null, newItem;

            if (current != null)
            {
                string folderName = "Model";
                string body = AdjustGacPath(ReadResourceContent(@"Linx.BusinessModelDesigner.Templates.ContextMetadata.txt"));

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
                    
                }
            }
        }

        private void UpdateCoreTemplates(Project bmProject)
        {
            if (!this.IsAspNetCoreEnabled())
                return;

            var coreProject = this.GetBmdCoreProject(bmProject);
            this.UpdateDynamicModelsTemplate(coreProject, false, bmProject);
            this.UpdateDynamicModelsTemplate(coreProject, true, bmProject);
            this.UpdateCoreContextTemplate(coreProject);
            this.UpdateContextMetadata(coreProject);
            this.UpdateDomainViewsTemplate(coreProject);
            this.GenerateBusinessOperations(false, coreProject);
            this.GenerateBusinessOperations(true, coreProject);
        }
        #endregion


    }
}

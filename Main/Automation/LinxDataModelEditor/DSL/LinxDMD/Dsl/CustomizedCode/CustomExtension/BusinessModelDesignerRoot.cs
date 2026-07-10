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
using Linx.BusinessDataModelDesigner.CustomCode;
using Microsoft.VisualStudio.Modeling.Diagrams;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Modeling.Immutability;
using VSLangProj80;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Modeling.Validation;
using System.Globalization;
using System.Diagnostics;
using Microsoft.VisualStudio.ComponentModelHost;
using Linx.Tools.Migration;
using Linx.BusinessDataModelDesigner.CustomizedCode.DatabaseScriptGenerator;

namespace Linx.BusinessDataModelDesigner
{

    #region External Elements
    static class GuidList
    {
        public const string guidNuGetPkgString = "F7D0E7A3-C60B-422A-BFAE-CEED36ADE7D2";
        public const string guidNuGetVSEventsPackagePkgString = "38ebd926-b8b6-44e9-952d-1cfd38c84209";

        public const string guidNuGetConsoleCmdSetString = "1E8A55F6-C18D-407F-91C8-94B02AE1CED6";
        public const string guidNuGetDialogCmdSetString = "25fd982b-8cae-4cbd-a440-e03ffccde106";
        public const string guidNuGetToolsGroupString = "C0D88179-5D25-4982-BFE6-EC5FD59AC103";
        public const string guidNuGetPackagesRestoreGroupString = "B4B288EF-D5B7-4669-9D6A-ACD644F90AC8";

        // any project system that wants to load NuGet when its project opens needs to activate a UI context with this GUID
        public const string guidAutoLoadNuGetString = "65B1D035-27A5-4BBA-BAB9-5F61C1E2BC4A";

        public static readonly Guid guidNuGetConsoleCmdSet = new Guid(guidNuGetConsoleCmdSetString);
        public static readonly Guid guidNuGetDialogCmdSet = new Guid(guidNuGetDialogCmdSetString);
        public static readonly Guid guidNuGetToolsGroupCmdSet = new Guid(guidNuGetToolsGroupString);
        public static readonly Guid guidNuGetPackagesRestoreCmdSet = new Guid(guidNuGetPackagesRestoreGroupString);
    }

    static class PkgCmdIDList
    {
        public const int cmdidPowerConsole = 0x0100;
        public const int cmdidAddPackageDialog = 0x100;
        public const int cmdidAddPackageDialogForSolution = 0x200;
        public const int cmdidRestorePackages = 0x300;
        public const int cmdidSourceSettings = 0x0200;
        public const int cmdIdGeneralSettings = 0x0300;
        public const int cmdIdVisualizer = 0x0310;
    }
    #endregion

    public partial class BusinessDataModelDesignerRoot
    {
        private List<string> _selectedProperties;
        public List<string> SelectedProperties
        {
            get
            {
                if (_selectedProperties == null)
                    _selectedProperties = new List<string>();
                return _selectedProperties;
            }
        }
        public const string vsInvalidProvider = "INVALID_PROVIDER_ERROR";
        public const string vsViewKindCode = "{7651A701-06E5-11D1-8EBD-00A0C90F26EA}";
        public bool IsLocked { get; set; }


        public WebApiController CheckWebApiDataServices(string apiName)
        {
            if (this.DbProviders.Count > 0)
            {
                WebApiController dataService = this.WebApiControllers.Where(e => e.ExposeAllContext).FirstOrDefault();
                if (dataService == null)
                {
                    using (Transaction transaction =
                                this.Store.TransactionManager.BeginTransaction("Changing StructuralInfo."))
                    {
                        dataService = new WebApiController(this.Store) { Name = (apiName.IsNullOrEmpty() ? "DataService" : apiName), ProjectSuffix = "web", RoutePrefix = "{Name}", ExposeAllContext = true };
                        this.WebApiControllers.Add(dataService);
                        transaction.Commit();
                    }
                }

                return dataService;
            }

            return null;
        }
        
        public List<ModelBusAdapter> GetModelAdapterss()
        {
            List<ModelBusAdapter> adapters = new List<ModelBusAdapter>();
            var modelBus = this.GetModelBus();
            // Get an adapterManager for the target DSL:
            ModelBusAdapterManager manager = BusinessDataModelDesignerRoot.GetModelBusManager<ModelBusAdapterManager>(modelBus);
            var designers = this.GetProjectModels(true);
            foreach (var designer in designers)
            {
                var item = designer.Value;
                // Create a reference to the target model:
                var modelReference = manager.CreateReference(item);
                //Get adapter
                var adapter = manager.CreateAdapter(modelReference);
                BusinessDataModelDesignerRoot modelRoot = adapter.GetModelRoot<BusinessDataModelDesignerRoot>();
                if (modelRoot != null)
                {
                    adapters.Add(adapter);
                }
            }

            return adapters;
        }

        public void ReleasePropertySelection()
        {
            if (this.SelectedProperties.Count > 0)
            {
                this.SelectedProperties.Clear();
                this.RefreshFocusedDiagramView();
            }
        }

        public void RefreshFocusedDiagramView()
        {
            var shape = this.GetPresentation<BusinessDataModelDesignerDiagram>();
            if (shape != null)
            {
                if (shape.FocusedDiagramView != null)
                    shape.FocusedDiagramView.Refresh();
                else
                    shape.Invalidate();
            }
        }

        #region Open any code element.

        public void SelectShape(string name)
        {
            var element = this.Types.FirstOrDefault(e => e.Name == name);
            if (element != null)
            {
                var diagram = this.GetPresentation<BusinessDataModelDesignerDiagram>();
                if (diagram != null)
                {
                    DiagramClientView clientView = (diagram.ClientViews.Count > 0 ? diagram.ClientViews[0] as DiagramClientView : null);
                    DiagramView activeView = diagram.ActiveDiagramView;
                    ShapeElement modelElementShape = element.GetPresentation<ShapeElement>();
                    if ((modelElementShape != null && activeView != null && activeView.Selection != null))
                    {
                        DiagramItem diagramItem = new DiagramItem(modelElementShape);
                        activeView.Selection.Set(diagramItem);
                        activeView.Selection.EnsureVisible();
                    }
                    else if ((modelElementShape != null && clientView != null && clientView.Selection != null))
                    {
                        DiagramItem diagramItem = new DiagramItem(modelElementShape);
                        clientView.Selection.Set(diagramItem);
                        clientView.Selection.EnsureVisible();
                    }
                }
            }
        }

        public void SelectLink(ModelElement element)
        {
            if (element != null)
            {
                BinaryLinkShape link = element.GetPresentation<BinaryLinkShape>();
                if (link != null)
                {
                    DiagramItem diagramItem = new DiagramItem(link);
                    var diagram = this.GetPresentation<BusinessDataModelDesignerDiagram>();
                    if (diagram != null)
                    {
                        DiagramClientView clientView = (diagram.ClientViews.Count > 0 ? diagram.ClientViews[0] as DiagramClientView : null);
                        DiagramView activeView = diagram.ActiveDiagramView;
                        activeView.Selection.Set(diagramItem);
                    }
                }
            }
        }

        public void OpenCodeElement(object designElement)
        {
            Project project = this.GetLxdmProject();
            if (project.IsNull())
                return;

            ProjectItem item = null;
            string className, elementName, fileName, attributes = String.Empty;
            
            if (designElement is BusinessDataModelDesignerRoot)
            {
                item = GetDataContextFolder();
                className = this.GetDefaultContextName();
                elementName = String.Empty;
                fileName = this.GetDataContextName() + ".CodeGen.js";
                if (!BusinessDataModelDesignerRoot.ExistsProjectItem(item.ProjectItems, fileName))
                {
                    MessageBox.Show(String.Format("File [{0}] does not exist!", fileName), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else if (designElement is StoreQuery)
            {
                className = this.GetDefaultContextName(); ;
                elementName = ((StoreQuery)designElement).Name;
                fileName = "BusinessDataModel";
                if (BusinessDataModelDesignerRoot.ExistsProjectItem(item.ProjectItems, fileName + ".tt"))
                {
                    item = item.ProjectItems.Item(fileName + ".tt");
                    fileName = fileName + ".cs";
                }
                else
                {
                    MessageBox.Show(String.Format("File [{0}] does not exist!", fileName), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else if (designElement is DomainView)
            {
                if (BusinessDataModelDesignerRoot.ExistsProjectItem(project.ProjectItems, "Domains"))
                {
                    item = project.ProjectItems.Item("Domains");
                    if (BusinessDataModelDesignerRoot.ExistsProjectItem(item.ProjectItems, "DomainViews.tt"))
                    {
                        item = item.ProjectItems.Item("DomainViews.tt");
                        fileName = "DomainViews.shared.cs";
                    }
                    else
                    {
                        MessageBox.Show(String.Format("File [{0}] does not exist!", "DomainViews.tt"), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show(String.Format("Folder [{0}] does not exist!", "Domains"), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                className = ((DomainView)designElement).Name;
                elementName = String.Empty;
            }
            else if (designElement is WebApiController)
            {               
                var api = (WebApiController)designElement;
                attributes = "";
                item = api.GetControllerFolder();
                className = api.GetRoutePrefix();
                elementName = String.Empty;
                fileName = "service.CodeGen.js";
                if (!BusinessDataModelDesignerRoot.ExistsProjectItem(item.ProjectItems, fileName))
                {
                    MessageBox.Show(String.Format("File [{0}] does not exist!", fileName), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else if (designElement is ClassOperation)
            {
                var op = (ClassOperation)designElement;
                attributes = "";
                item = this.GetDataEntitytCustomFolder(op.ModelClass.Name);
                className = op.Name;
                elementName = String.Empty;
                fileName = className + ".js";
                if (!BusinessDataModelDesignerRoot.ExistsProjectItem(item.ProjectItems, fileName))
                {
                    MessageBox.Show(String.Format("File [{0}] does not exist!", fileName), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else if (designElement is ModelImplementation)
            {
                var implProject = this.GetImplementationProject((ModelImplementation)designElement);

                if (implProject == null)
                {
                    MessageBox.Show("This implementation project does not exist! Save the designer before this operation.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var implement = (ModelImplementation)designElement;
                attributes = "Export(typeof(" + implement.ModelInterface.Name + "))";
                attributes += "#ExportMetadata(\"ImplementationName\", \"" + implement.Name + "\")";
                item = this.GetModelImplementationsItem(implement, implProject);
                className = Path.GetFileNameWithoutExtension(this.GetImplementationClassFile(implement, implProject));
                fileName = className + ".cs";
                elementName = String.Empty;
                if (!BusinessDataModelDesignerRoot.ExistsProjectItem(item.ProjectItems, fileName))
                {
                    MessageBox.Show(String.Format("File [{0}] does not exist! Save the designer before this operation.", fileName), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else if (designElement is ModelInterface)
            {
                var contractProject = this.GetContractProject();

                if (contractProject == null)
                {
                    MessageBox.Show("This contract project does not exist! Save the designer before this operation.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var intrf = (ModelInterface)designElement;
                item = this.GetModelContractsItem(contractProject);
                className = Path.GetFileNameWithoutExtension(this.GetContractClassFile(intrf.Name, contractProject));
                fileName = className + ".cs";
                elementName = String.Empty;
                if (!BusinessDataModelDesignerRoot.ExistsProjectItem(item.ProjectItems, fileName))
                {
                    MessageBox.Show(String.Format("File [{0}] does not exist! Save the designer before this operation.", fileName), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                item = GetDataEntitytFolder();
                if (designElement is ModelClass)
                {
                    className = ((ModelClass)designElement).Name;
                    elementName = String.Empty;
                    fileName = className + ".CodeGen.js";
                }
                else if (designElement is ModelAttribute)
                {
                    className = ((ModelAttribute)designElement).ModelClass.Name;
                    elementName = ((ModelAttribute)designElement).Name;
                    fileName = className + ".CodeGen.js";
                }
                else return;

                if (!BusinessDataModelDesignerRoot.ExistsProjectItem(item.ProjectItems, fileName))
                {
                    MessageBox.Show(String.Format("File [{0}] does not exist!", fileName), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            if (!OpenCodeMember(item, fileName, className, elementName, attributes))
                MessageBox.Show(String.Format("File [{0}] does not exist!", fileName), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        public bool OpenCodeMember(ProjectItem item, string fileName, string className, string elementName, string attributes)
        {
            if (BusinessDataModelDesignerRoot.ExistsProjectItem(item.ProjectItems, fileName))
            {
                Window window = item.ProjectItems.Item(fileName).Open();
                window.SetFocus();
                TextSelection selection = ((TextSelection)item.ProjectItems.Item(fileName).Document.Selection);
                if (selection != null)
                    selection.MoveToCodeElement(className, elementName, attributes);
                return true;
            }
            else return false;
        }


        public void OpenCodeElement(string fileName, string className, string elementName)
        {
            ProjectItem item = GetDiagramProjectItem();

            if (!item.IsNull())
            {
                if (!OpenCodeMember(item, fileName, className, elementName, String.Empty))
                    MessageBox.Show(String.Format("File [{0}] does not exist!", fileName), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        public TextSelection OpenClassOperation(ClassOperation targetOperation)
        {
            Project project = this.GetLxdmProject();
            if (project.IsNull())
                return null;

            ProjectItem item = project.ProjectItems.Item("Model");

            if (!item.IsNull())
                return this.OpenClassOperation(targetOperation.ModelClass.Name + ".Operations" + (targetOperation.IsShared ? ".shared" : "") + ".cs", targetOperation, targetOperation.ModelClass.Name, item);
            else return null;
        }

        public TextSelection OpenClassOperation(string fileName, ClassOperation targetOperation, string className)
        {
            return OpenClassOperation(fileName, targetOperation, className, null);
        }

        #region Implementation


        public void GenerateContractsCode(List<ModelInterface> interfaces)
        {
            if (interfaces == null || interfaces.Count == 0)
                return;

            string outputFile;
            Project lxdmProject = GetLxdmProject();
            ProjectItem item;

            if (lxdmProject != null)
            {
                Project contractProject;
                StringBuilder codeBuilder;

                foreach (var intf in interfaces)
                {
                    contractProject = this.GetContractProject(lxdmProject);
                    if (!contractProject.IsNull())
                    {
                        item = this.GetModelContractsItem(contractProject);
                        if (item.IsNull())
                            item = contractProject.ProjectItems.AddFolder(this.GetContractFolderName());
                        if (!item.IsNull())
                        {
                            codeBuilder = new StringBuilder();
                            this.GenerateContractCode(codeBuilder, intf, contractProject);
                            outputFile = this.GetContractClassFile(intf.Name, contractProject);
                            if (!File.Exists(outputFile) || !ExistsProjectItem(item.ProjectItems, intf.Name + ".cs"))
                            {
                                if (!this.VerifySourceControl(outputFile))
                                    return;

                                RemoveProjectItems(item.ProjectItems, intf.Name + ".cs");

                                //Add Events                                
                                System.IO.File.WriteAllText(outputFile, codeBuilder.ToString());
                                //Add project item.
                                item.ProjectItems.AddFromFile(outputFile);
                            }
                            else if (System.IO.File.ReadAllText(outputFile) != codeBuilder.ToString())
                                System.IO.File.WriteAllText(outputFile, codeBuilder.ToString(), Encoding.UTF8);

                        }
                    }

                }

                //Generate model                
                GenerateContractsModelCode();
            }
        }

        public void GenerateContractsModelCode()
        {
            string outputFile;
            Project lxdmProject = GetLxdmProject();
            ProjectItem item;

            if (lxdmProject != null)
            {
                Project contractProject;
                StringBuilder codeBuilder;

                foreach (var type in this.Types.Where(e => e is ModelClass).Select(e => (ModelClass)e))
                {
                    contractProject = this.GetContractProject(lxdmProject);
                    if (!contractProject.IsNull())
                    {
                        item = this.GetModelContractsItem(contractProject, true);
                        if (item.IsNull())
                            item = contractProject.ProjectItems.AddFolder(this.GetContractModelFolderName());
                        if (!item.IsNull())
                        {
                            codeBuilder = new StringBuilder();
                            this.GenerateContractModelCode(codeBuilder, type, contractProject);
                            outputFile = this.GetContractClassFile(type.Name, contractProject, true);
                            if (!File.Exists(outputFile) || !ExistsProjectItem(item.ProjectItems, type.Name + ".cs"))
                            {
                                if (!this.VerifySourceControl(outputFile))
                                    return;

                                RemoveProjectItems(item.ProjectItems, type.Name + ".cs");

                                //Add Events                                
                                System.IO.File.WriteAllText(outputFile, codeBuilder.ToString());
                                //Add project item.
                                item.ProjectItems.AddFromFile(outputFile);
                            }
                            else if (System.IO.File.ReadAllText(outputFile) != codeBuilder.ToString())
                                System.IO.File.WriteAllText(outputFile, codeBuilder.ToString(), Encoding.UTF8);

                        }
                    }
                }
            }
        }

        public void GenerateImplementationsCode(List<ModelInterface> interfaces)
        {
            if (interfaces == null || interfaces.Count == 0)
                return;

            string outputFile;
            Project lxdmProject = GetLxdmProject();
            ProjectItem item;

            if (lxdmProject != null)
            {
                Project implementProject;
                StringBuilder codeBuilder;

                foreach (var intf in interfaces)
                {
                    foreach (var repository in intf.ModelImplementations)
                    {
                        implementProject = this.GetImplementationProject(repository, lxdmProject);
                        if (!implementProject.IsNull())
                        {
                            item = this.GetModelImplementationsItem(repository, implementProject);
                            if (!item.IsNull())
                            {
                                outputFile = this.GetImplementationClassFile(repository, implementProject);
                                if (!File.Exists(outputFile) || !ExistsProjectItem(item.ProjectItems, repository.Name + ".cs"))
                                {
                                    if (!this.VerifySourceControl(outputFile))
                                        return;

                                    RemoveProjectItems(item.ProjectItems, repository.Name + ".cs");
                                    codeBuilder = new StringBuilder();

                                    //Add Events
                                    this.GenerateImplementationCode(codeBuilder, repository, implementProject);
                                    System.IO.File.WriteAllText(outputFile, codeBuilder.ToString());
                                    //Add project item.
                                    item.ProjectItems.AddFromFile(outputFile);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void GenerateImplementationCode(StringBuilder codeBuilder, ModelImplementation implement, Project implementProject)
        {
            string baseIndent = "	";

            codeBuilder.AppendLine("using System;");
            codeBuilder.AppendLine("using System.Collections;");
            codeBuilder.AppendLine("using System.Collections.Generic;");
            codeBuilder.AppendLine("using System.Linq.Expressions;");
            codeBuilder.AppendLine("using Linx.Tools;");
            codeBuilder.AppendLine("using System.Linq;");
            codeBuilder.AppendLine("using System.ComponentModel.Composition;");
            codeBuilder.AppendLine("using " + implement.BusinessDataModelDesignerRoot.GetNamespace() + ";");
            codeBuilder.AppendLine("using " + this.GetContractProjectName(this.GetLxdmProject()) + ";");

            codeBuilder.AppendLine();
            codeBuilder.AppendLine("namespace " + implementProject.Name);
            codeBuilder.AppendLine("{");

            //Class Definition
            codeBuilder.AppendLine(baseIndent + "");
            codeBuilder.AppendLine(baseIndent + "////////////////////////////////////////////////////////////////////////////");
            codeBuilder.AppendLine(baseIndent + "//////////////////////////// Business Implementation ///////////////////////");
            codeBuilder.AppendLine(baseIndent + "////////////////////////////////////////////////////////////////////////////");
            codeBuilder.AppendLine(baseIndent + "[Export(typeof(" + implement.ModelInterface.Name + "))]");
            codeBuilder.AppendLine(baseIndent + "[ExportMetadata(\"ImplementationName\", \"" + implement.Name + "\")]");
            codeBuilder.AppendLine(baseIndent + "public partial class " + implement.Name + " : " + implement.ModelInterface.Name);
            codeBuilder.AppendLine(baseIndent + "{");
            codeBuilder.AppendLine(baseIndent + "}");
            //End Class Definition

            //End Namespace
            codeBuilder.AppendLine("}");
        }

        private void GenerateContractCode(StringBuilder codeBuilder, ModelInterface intrf, Project contractProject)
        {
            string baseIndent = "	";

            codeBuilder.AppendLine("using System;");
            codeBuilder.AppendLine("using System.Collections;");
            codeBuilder.AppendLine("using System.Collections.Generic;");
            codeBuilder.AppendLine("using System.Linq.Expressions;");
            codeBuilder.AppendLine("using Linx.Tools;");
            codeBuilder.AppendLine("using System.Linq;");
            codeBuilder.AppendLine("using System.ComponentModel.Composition;");

            codeBuilder.AppendLine();
            codeBuilder.AppendLine("namespace " + contractProject.Name);
            codeBuilder.AppendLine("{");

            //Interface Definition
            codeBuilder.AppendLine(baseIndent + "");
            codeBuilder.AppendLine(baseIndent + "////////////////////////////////////////////////////////////////////////////");
            codeBuilder.AppendLine(baseIndent + "/////////////////////////////// Business Contract //////////////////////////");
            codeBuilder.AppendLine(baseIndent + "////////////////////////////////////////////////////////////////////////////");
            codeBuilder.AppendLine(baseIndent + "public interface " + intrf.Name);
            codeBuilder.AppendLine(baseIndent + "{");

            //Generate operations
            foreach (InterfaceOperation operation in intrf.Operations)
            {
                codeBuilder.AppendLine(baseIndent + "   //" + (String.IsNullOrEmpty(operation.DocComment) ? operation.Name : operation.DocComment));
                codeBuilder.AppendLine(baseIndent + "   " + operation.ReturnType + " " + operation.Name + "(" + operation.Parameters.Replace("#", ", ") + ");");
            }

            codeBuilder.AppendLine(baseIndent + "}");
            codeBuilder.AppendLine();

            //End Interface Definition

            //End Namespace
            codeBuilder.AppendLine("}");
        }


        private void GenerateContractModelCode(StringBuilder codeBuilder, ModelClass type, Project contractProject)
        {
            string baseIndent = "	";

            codeBuilder.AppendLine("using System;");
            codeBuilder.AppendLine("using System.Collections;");
            codeBuilder.AppendLine("using System.Collections.Generic;");
            codeBuilder.AppendLine("using System.Linq.Expressions;");
            codeBuilder.AppendLine("using Linx.Tools;");
            codeBuilder.AppendLine("using System.Linq;");
            codeBuilder.AppendLine("using System.ComponentModel.Composition;");

            codeBuilder.AppendLine();
            codeBuilder.AppendLine("namespace " + contractProject.Name);
            codeBuilder.AppendLine("{");

            //Interface Definition
            codeBuilder.AppendLine(baseIndent + "");
            codeBuilder.AppendLine(baseIndent + "////////////////////////////////////////////////////////////////////////////");
            codeBuilder.AppendLine(baseIndent + "//////////////////////////////// Business Class ////////////////////////////");
            codeBuilder.AppendLine(baseIndent + "////////////////////////////////////////////////////////////////////////////");
            codeBuilder.AppendLine(baseIndent + "public partial class " + type.Name);
            codeBuilder.AppendLine(baseIndent + "{");

            //Generate operations
            foreach (var attr in type.Attributes)
            {
                codeBuilder.AppendLine(baseIndent + "   public " + attr.GetDataType() + " " + attr.Name + " { get; set; }");
            }

            string name;
            List<string> names = new List<string>();
            codeBuilder.AppendLine(baseIndent + "   //Direct Associations Multiplicity.One");
            foreach (var link in type.GetLinksToTargetModelClasses().Where(e => e.TargetMultiplicity == Multiplicity.One || e.TargetMultiplicity == Multiplicity.ZeroOne))
            {
                name = (link.SourcePropertyNameToTarget.IsNullOrEmpty() ? link.TargetModelClass.Name + "_LISTA" : link.SourcePropertyNameToTarget);
                if (names.Contains(name))
                    name += (names.Count(e => e.Length >= name.Length && e.Left(name.Length) == name)).ToString();
                names.Add(name);
                codeBuilder.AppendLine(baseIndent + "   public " + link.TargetModelClass.Name + " " + name + " { get; set; }");
            }
            codeBuilder.AppendLine(baseIndent + "   //Direct Associations Multiplicity.Many");
            foreach (var link in type.GetLinksToTargetModelClasses().Where(e => e.TargetMultiplicity == Multiplicity.Many || e.TargetMultiplicity == Multiplicity.ZeroMany))
            {
                name = (link.SourcePropertyNameToTarget.IsNullOrEmpty() ? link.TargetModelClass.Name + "_LISTA" : link.SourcePropertyNameToTarget);
                if (names.Contains(name))
                    name += (names.Count(e => e.Length >= name.Length && e.Left(name.Length) == name)).ToString();
                names.Add(name);
                codeBuilder.AppendLine(baseIndent + "   public IEnumerable<" + link.TargetModelClass.Name + "> " + name + " { get; set; }");
            }
            foreach (var link in type.GetLinksToMultipleAssociations())
            {
                name = (link.CollectionName.IsNullOrEmpty() ? link.MultipleAssociation.TargetType.Name + "_LISTA" : link.CollectionName);
                if (names.Contains(name))
                    name += (names.Count(e => e.Length >= name.Length && e.Left(name.Length) == name)).ToString();
                names.Add(name);
                codeBuilder.AppendLine(baseIndent + "   public IEnumerable<" + link.MultipleAssociation.TargetType.Name + "> " + name + " { get; set; }");
            }

            codeBuilder.AppendLine(baseIndent + "}");
            codeBuilder.AppendLine();

            //End Interface Definition

            //End Namespace
            codeBuilder.AppendLine("}");
        }

        public TextSelection OpenImplementationMethod(InterfaceOperation targetOperation)
        {
            ModelImplementation implement;
            if (((InterfaceOperation)targetOperation).Interface.ModelImplementations.Count == 1)
                implement = ((InterfaceOperation)targetOperation).Interface.ModelImplementations.First();
            else
                implement = ((InterfaceOperation)targetOperation).Interface.ModelImplementations.Where(e => e.HasFocus).FirstOrDefault();

            if (implement == null)
            {
                MessageBox.Show("Select an implementation before this action!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }

            var repProject = this.GetImplementationProject(implement);
            if (!repProject.IsNull())
            {
                string classFile = Path.GetFileName(this.GetImplementationClassFile(implement, repProject));
                ProjectItem item = this.GetModelImplementationsItem(implement, repProject);
                return this.OpenClassOperation(classFile, targetOperation, Path.GetFileNameWithoutExtension(classFile), item);
            }
            else return null;
        }

        public Project GetImplementationProject(ModelImplementation implement, Project lxdmProject = null)
        {
            if (lxdmProject == null)
                lxdmProject = GetLxdmProject();

            if (lxdmProject != null)
                return this.GetProjectByName(GetImplementationProjectName(implement, lxdmProject));
            else
                return null;
        }

        public Project GetContractProject(Project lxdmProject = null)
        {
            if (lxdmProject == null)
                lxdmProject = GetLxdmProject();

            if (lxdmProject != null)
                return this.GetProjectByName(GetContractProjectName(lxdmProject));
            else
                return null;
        }

        public ProjectItem GetModelImplementationsItem(ModelImplementation implement, Project implementProject = null)
        {
            string outputFile = String.Empty;
            ProjectItem item = null;

            if (implementProject == null)
                implementProject = this.GetImplementationProject(implement);

            if (implementProject != null)
                item = GetProjectItemByName(implementProject, this.GetImplementationFolderName());

            return item;
        }

        public ProjectItem GetModelContractsItem(Project contractProject = null, bool model = false)
        {
            string outputFile = String.Empty;
            ProjectItem item = null;

            if (contractProject == null)
                contractProject = this.GetContractProject();

            if (contractProject != null)
                item = GetProjectItemByName(contractProject, (model ? this.GetContractModelFolderName() : this.GetContractFolderName()));

            return item;
        }

        public string GetImplementationClassFile(ModelImplementation implement, Project implementProject = null)
        {
            string outputFile = String.Empty;
            ProjectItem item = null;

            if (implementProject == null)
                implementProject = this.GetImplementationProject(implement);

            if (implementProject != null)
                item = GetModelImplementationsItem(implement, implementProject); ;

            if (!item.IsNull())
                outputFile = Path.Combine(GetProjectPath(implementProject), Path.GetFileNameWithoutExtension(item.Name) + "\\" + implement.Name + ".cs");

            return outputFile;
        }

        public string GetContractClassFile(string name, Project contractProject = null, bool model = false)
        {
            string outputFile = String.Empty;
            ProjectItem item = null;

            if (contractProject == null)
                contractProject = this.GetContractProject();

            if (contractProject != null)
                item = GetModelContractsItem(contractProject, model); ;

            if (!item.IsNull())
                outputFile = Path.Combine(GetProjectPath(contractProject), Path.GetFileNameWithoutExtension(item.Name) + "\\" + name + ".cs");

            return outputFile;
        }

        public string GetImplementationProjectName(ModelImplementation implement, Project lxdmProject)
        {
            return lxdmProject.Name + "." + GetImplementationFolderName() + (implement.ProjectSuffix.IsNullOrEmpty() ? String.Empty : "." + implement.ProjectSuffix);
        }

        public string GetContractProjectName(Project lxdmProject)
        {
            return lxdmProject.Name + "." + GetContractFolderName();
        }

        public string GetContractFolderName()
        {
            return "Contracts";
        }

        public string GetContractModelFolderName()
        {
            return "Model";
        }

        public string GetImplementationFolderName()
        {
            return "Implementations";
        }

        public void UpdateContractProject(Project lxdmProject, ModelInterface intrf)
        {
            EnvDTE.DTE appDTE = lxdmProject.DTE;
            string contractProjectName = GetContractProjectName(lxdmProject);
            Project contractProject = GetProjectByName(contractProjectName);
            string folderName = GetContractFolderName();

            if (contractProject == null)
            {
                string repositoryDir = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(lxdmProject.FullName), "..\\" + contractProjectName));

                if (!System.IO.Directory.Exists(repositoryDir))
                    System.IO.Directory.CreateDirectory(repositoryDir);

                EnvDTE80.SolutionFolder businessDesignerFolder = null;
                var tmpProj = this.GetProjectByName(folderName);
                businessDesignerFolder = (tmpProj == null ? null : tmpProj.Object) as EnvDTE80.SolutionFolder;
                if (businessDesignerFolder == null)
                    businessDesignerFolder = (EnvDTE80.SolutionFolder)((EnvDTE100.Solution4)appDTE.Solution).AddSolutionFolder(folderName).Object;

                if (System.IO.File.Exists(System.IO.Path.Combine(repositoryDir, contractProjectName + ".csproj")))
                {
                    if (businessDesignerFolder != null)
                        businessDesignerFolder.AddFromFile(System.IO.Path.Combine(repositoryDir, contractProjectName + ".csproj"));
                    else
                        ((EnvDTE100.Solution4)appDTE.Solution).AddFromFile(System.IO.Path.Combine(repositoryDir, contractProjectName + ".csproj"), false);

                    contractProject = GetProjectByName(contractProjectName);
                }
                else
                {
                    // Get the location of the project templates
                    string templateName = ((EnvDTE100.Solution4)appDTE.Solution).GetProjectTemplate("Class Library", "CSharp");
                    if (businessDesignerFolder != null)
                        businessDesignerFolder.AddFromTemplate(templateName, repositoryDir, contractProjectName);
                    else
                        ((EnvDTE100.Solution4)appDTE.Solution).AddFromTemplate(templateName, repositoryDir, contractProjectName, false);

                    contractProject = GetProjectByName(contractProjectName);

                    //Delete Class1.cs from template project
                    if (ExistsProjectItem(contractProject.ProjectItems, "Class1.cs"))
                        contractProject.ProjectItems.Item("Class1.cs").Delete();
                    contractProject.ProjectItems.AddFolder(folderName);

                    //Set Assembly Name
                    contractProject.Properties.Item("AssemblyName").Value = contractProjectName;
                    //Set Default Namespace
                    contractProject.Properties.Item("DefaultNamespace").Value = contractProjectName;
                }
            }

            this.RemoveReferencesWithoutFile(contractProject);
            this.UpdateVersion(contractProject);

            //Upgrade to last framework version
            UpgradeVersion(contractProject);

            //Update library references
            this.AddReference(contractProject, "System.ComponentModel.Composition.dll");
            this.AddReference(contractProject, "Linx.Tools.dll");

            //Set PostBuildEvent
            SetPostBuildEvent(contractProject);
        }

        public void UpdateImplementationProject(Project lxdmProject, ModelImplementation implement)
        {
            EnvDTE.DTE appDTE = lxdmProject.DTE;
            string implementProjectName = GetImplementationProjectName(implement, lxdmProject);
            Project implementProject = GetProjectByName(implementProjectName);
            Project contractProject = this.GetContractProject(lxdmProject);
            string folderName = this.GetImplementationFolderName();

            if (implementProject == null)
            {
                string repositoryDir = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(lxdmProject.FullName), "..\\" + implementProjectName));

                if (!System.IO.Directory.Exists(repositoryDir))
                    System.IO.Directory.CreateDirectory(repositoryDir);

                EnvDTE80.SolutionFolder businessDesignerFolder = null;
                var tmpProj = this.GetProjectByName(folderName);
                businessDesignerFolder = (tmpProj == null ? null : tmpProj.Object) as EnvDTE80.SolutionFolder;
                if (businessDesignerFolder == null)
                    businessDesignerFolder = (EnvDTE80.SolutionFolder)((EnvDTE100.Solution4)appDTE.Solution).AddSolutionFolder(folderName).Object;

                if (System.IO.File.Exists(System.IO.Path.Combine(repositoryDir, implementProjectName + ".csproj")))
                {
                    if (businessDesignerFolder != null)
                        businessDesignerFolder.AddFromFile(System.IO.Path.Combine(repositoryDir, implementProjectName + ".csproj"));
                    else
                        ((EnvDTE100.Solution4)appDTE.Solution).AddFromFile(System.IO.Path.Combine(repositoryDir, implementProjectName + ".csproj"), false);

                    implementProject = GetProjectByName(implementProjectName);
                }
                else
                {
                    // Get the location of the project templates
                    string templateName = ((EnvDTE100.Solution4)appDTE.Solution).GetProjectTemplate("Class Library", "CSharp");
                    if (businessDesignerFolder != null)
                        businessDesignerFolder.AddFromTemplate(templateName, repositoryDir, implementProjectName);
                    else
                        ((EnvDTE100.Solution4)appDTE.Solution).AddFromTemplate(templateName, repositoryDir, implementProjectName, false);

                    implementProject = GetProjectByName(implementProjectName);

                    //Delete Class1.cs from template project
                    if (ExistsProjectItem(implementProject.ProjectItems, "Class1.cs"))
                        implementProject.ProjectItems.Item("Class1.cs").Delete();
                    implementProject.ProjectItems.AddFolder(folderName);

                    //Set Assembly Name
                    implementProject.Properties.Item("AssemblyName").Value = implementProjectName;
                    //Set Default Namespace
                    implementProject.Properties.Item("DefaultNamespace").Value = implementProjectName;
                }
            }

            this.RemoveReferencesWithoutFile(implementProject);
            this.UpdateVersion(implementProject);

            //Upgrade to last framework version
            UpgradeVersion(implementProject);

            //Add project references
            AddProjectReference(implementProject, lxdmProject, false);
            AddProjectReference(implementProject, contractProject, false);

            //Update library references
            this.AddReference(implementProject, "System.ComponentModel.Composition.dll");
            this.AddReference(implementProject, "Linx.Tools.dll");
            this.AddReference(implementProject, "Linx.LinqExtensions.dll");

            //Set PostBuildEvent
            SetPostBuildEvent(implementProject);
        }

        #endregion

        #region WebAPI

        public TextSelection OpenWebApiAction(WebApiAction targetOperation)
        {
            var webApiProject = this.GetWebApiProject(((WebApiAction)targetOperation).WebApiController.ProjectSuffix);
            if (!webApiProject.IsNull())
            {
                string classFile = Path.GetFileName(this.GetWebApiClassFile(((WebApiAction)targetOperation).WebApiController, webApiProject));
                ProjectItem item = this.GetWebApiControllersItem(((WebApiAction)targetOperation).WebApiController, webApiProject);
                return this.OpenClassOperation(classFile, targetOperation, Path.GetFileNameWithoutExtension(classFile), item);
            }
            else return null;
        }


        private void GenerateWebApiAttributeRoutingHttpCode(List<ModelClass> modelClasses, Linx.Tools.CodeBuilder codeBuilder, WebApiController api, Project webApiProject, Project eadProject)
        {
            var item = GetWebApiAppStartItem(api, webApiProject);

            codeBuilder.AddLine("using System.Web.Http;");
            codeBuilder.AddLine("using System.Web;");
            codeBuilder.AddLine("using System.Web.Routing;");
            codeBuilder.AddLine("using Newtonsoft.Json.Serialization;");
            codeBuilder.AddLine("using System.Reflection;");
            codeBuilder.AddLine("using System.Net.Http;");
            codeBuilder.AddLine("using System.Web.Http.Controllers;");
            codeBuilder.AddLine("using System.Linq;");
            codeBuilder.AddLine("using System.Web.Http.OData.Builder;");
            codeBuilder.AddLine("using System.Web.Http.OData.Extensions;");
            codeBuilder.AddLine("using System.Web.Http.OData.Routing.Conventions;");
            codeBuilder.AddLine("using System.Web.Http.OData.Routing;");
            codeBuilder.AddLine("using Microsoft.Data.Edm;");
            codeBuilder.AddLine("using " + api.BusinessDataModelDesignerRoot.GetNamespace() + ";");

            codeBuilder.AddLine();
            codeBuilder.AddLine("[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(" + webApiProject.Name + "." + item.Name + "." + api.Name + "AttributeRoutingHttp), \"Start\")]");
            codeBuilder.AddLine("");

            codeBuilder.AddLine("namespace " + webApiProject.Name + "." + item.Name);
            codeBuilder.AddLine("{");

            //Class Definition
            codeBuilder.AddLine();

            codeBuilder.IncreaseIndent();

            codeBuilder.AddLine("public static class " + api.Name + "AttributeRoutingHttp");
            codeBuilder.AddLine("{");
            codeBuilder.IncreaseIndent();
            codeBuilder.AddLine("public static void Start()");
            codeBuilder.AddLine("{");

            codeBuilder.AddLine("   var conventions = ODataRoutingConventions.CreateDefault();");
            codeBuilder.AddLine("   conventions.Insert(0, new Linx.DataService.ODataContainmentRoutingConvention(\"" + api.Name + "\"));");
            codeBuilder.AddLine("   GlobalConfiguration.Configuration.Routes.MapODataServiceRoute(");
            codeBuilder.AddLine("       routeName: \"" + api.Name + "Route\",");
            codeBuilder.AddLine("       routePrefix: \"" + api.GetRoutePrefix() + "\",");
            codeBuilder.AddLine("       model: GetEdmModel(), pathHandler: new DefaultODataPathHandler(), routingConventions: conventions");
            codeBuilder.AddLine("       );");
            codeBuilder.AddLine("}");
            codeBuilder.AddLine();
            codeBuilder.AddLine("private static IEdmModel GetEdmModel()");
            codeBuilder.AddLine("{");
            codeBuilder.AddLine("    ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();");

            if (modelClasses == null)
                modelClasses = this.Types.Where(e => e is ModelClass).Select(e => (ModelClass)e).ToList();

            foreach (var entity in modelClasses.Where(e => !((ModelClass)e).InStudy && !((ModelClass)e).NotMapped).Select(e => (ModelClass)e).OrderBy(e => e.Name))
            {
                codeBuilder.AddLine("    modelBuilder.EntitySet<" + entity.Name + ">(\"" + entity.Name + "\");");
            }

            codeBuilder.AddLine("    return modelBuilder.GetEdmModel();");
            codeBuilder.AddLine("}");


            codeBuilder.DecreaseIndent();
            codeBuilder.AddLine("}");

            codeBuilder.DecreaseIndent();
            //End Class Definition

            //End Namespace
            codeBuilder.AddLine("}");
        }

        private bool WriteFile(string outputFilePath, Linx.Tools.CodeBuilder codeBuilder, ProjectItems projectItems)
        {
            string fileName = Path.GetFileName(outputFilePath);
            bool existsInProject = ExistsProjectItem(projectItems, fileName);

            if (existsInProject)
                this.VerifySourceControl(outputFilePath);

            if (!File.Exists(outputFilePath) || !existsInProject)
            {
                if (existsInProject)
                    RemoveProjectItems(projectItems, fileName);

                System.IO.File.WriteAllText(outputFilePath, codeBuilder.ToString(), Encoding.UTF8);

                projectItems.AddFromFile(outputFilePath);
            }
            else if (System.IO.File.ReadAllText(outputFilePath) != codeBuilder.ToString())
                System.IO.File.WriteAllText(outputFilePath, codeBuilder.ToString(), Encoding.UTF8);

            return true;
        }

        public void GenerateWebApiControllersCode(List<ModelClass> modelClasses, List<BusinessDataModelDesignerRoot> models)
        {
            string outputFile;
            Project lxdmProject = GetLxdmProject();

            if (lxdmProject != null)
            {
                Project webApiProject;
                ProjectItem item;
                Linx.Tools.CodeBuilder codeBuilder;

                foreach (var api in this.WebApiControllers.Where(e => (modelClasses == null && !e.ExposeAllContext) || (modelClasses != null && e.ExposeAllContext)))
                {
                    webApiProject = this.GetWebApiProject(api.ProjectSuffix, lxdmProject);
                    if (webApiProject != null)
                    {
                        item = item = this.GetWebApiControllersItem(api, webApiProject);
                        if (!item.IsNull())
                        {
                            //Automatic Controller
                            //Get automatic code
                            codeBuilder = new Linx.Tools.CodeBuilder();
                            this.GenerateWebApiAutomaticControllerCode(modelClasses, models, codeBuilder, api, webApiProject, lxdmProject, (this.EnableAutomaticAuthorization ? api.Name + "ControllerAuthorize" : ""));
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

                                //Add Events
                                this.GenerateWebApiControllerCode(codeBuilder, api, webApiProject);
                                System.IO.File.WriteAllText(outputFile, codeBuilder.ToString());
                                //Add project item.
                                item.ProjectItems.AddFromFile(outputFile);
                            }

                        }
                    }
                }
            }
        }

        private void GenerateWebApiControllerCode(Linx.Tools.CodeBuilder codeBuilder, WebApiController api, Project webApiProject)
        {

            codeBuilder.AddLine("using System;");
            codeBuilder.AddLine("using System.Collections;");
            codeBuilder.AddLine("using System.Collections.Generic;");
            codeBuilder.AddLine("using System.Linq.Expressions;");
            codeBuilder.AddLine("using Linx.Tools;");
            codeBuilder.AddLine("using System.Linq;");
            codeBuilder.AddLine("using System.ComponentModel;");
            codeBuilder.AddLine("using System.ComponentModel.DataAnnotations;");
            codeBuilder.AddLine("using System.ComponentModel.Composition;");
            codeBuilder.AddLine("using System.Net;");
            codeBuilder.AddLine("using System.Net.Http;");
            codeBuilder.AddLine("using System.Web.Http;");
            codeBuilder.AddLine("using " + api.BusinessDataModelDesignerRoot.GetNamespace() + ";");

            var item = GetWebApiControllersItem(api, webApiProject);

            codeBuilder.AddLine("");
            codeBuilder.AddLine("namespace " + webApiProject.Name + "." + item.Name);
            codeBuilder.AddLine("{");

            codeBuilder.IncreaseIndent();

            //Class Definition
            codeBuilder.AddLine("");
            codeBuilder.AddLine("////////////////////////////////////////////////////////////////////////////");
            codeBuilder.AddLine("/////////////////////////// Business Api Controller ////////////////////////");
            codeBuilder.AddLine("////////////////////////////////////////////////////////////////////////////");
            codeBuilder.AddLine("public partial class " + api.Name + "Controller");
            codeBuilder.AddLine("{");

            codeBuilder.AddLine("}");
            //End Class Definition

            codeBuilder.DecreaseIndent();

            //End Namespace
            codeBuilder.AddLine("}");
        }

        public void OpenOperationalEvents()
        {
            var lxdmProject = this.GetLxdmProject();
            string folderName = "Model";
            ProjectItem folder = GetProjectItemByName(lxdmProject, folderName);
            if (!folder.IsNull())
            {
                string contextEventsName = this.GetOperationalEventsClassName();
                string fileName = contextEventsName + ".cs";
                if (!OpenCodeMember(folder, fileName, contextEventsName, "", ""))
                    MessageBox.Show(String.Format("File [{0}] does not exist!", fileName), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void OpenStartEvents()
        {
            var lxdmProject = this.GetLxdmProject();
            string folderName = "Model";
            ProjectItem folder = GetProjectItemByName(lxdmProject, folderName);
            if (!folder.IsNull())
            {
                string contextEventsName = this.GetStartEventsClassName();
                string fileName = contextEventsName + ".cs";
                if (!OpenCodeMember(folder, fileName, contextEventsName, "", ""))
                    MessageBox.Show(String.Format("File [{0}] does not exist!", fileName), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public string GetOperationalEventsClassName()
        {
            return "ContextEvents";
        }

        public string GetStartEventsClassName()
        {
            return "ContextStartEvents";
        }

        public void GenerateOperationalEvents(Project lxdmProject)
        {
            if (lxdmProject != null)
            {
                string outputFile, folderName = "Model";
                ProjectItem folder = GetProjectItemByName(lxdmProject, folderName);
                if (folder == null)
                    folder = lxdmProject.ProjectItems.AddFolder(folderName);

                Linx.Tools.CodeBuilder codeBuilder;

                if (!folder.IsNull())
                {
                    string contextEventsName = this.GetOperationalEventsClassName();
                    outputFile = Path.Combine(Path.Combine(this.GetProjectPath(), folderName), contextEventsName + ".cs");
                    if (!File.Exists(outputFile) || !ExistsProjectItem(folder.ProjectItems, System.IO.Path.GetFileName(outputFile)))
                    {
                        if (!this.VerifySourceControl(outputFile))
                            return;

                        RemoveProjectItems(folder.ProjectItems, System.IO.Path.GetFileName(outputFile));
                        codeBuilder = new Linx.Tools.CodeBuilder();

                        //Add Events
                        this.GenerateOperationalEventsCode(codeBuilder, lxdmProject, contextEventsName);
                        System.IO.File.WriteAllText(outputFile, codeBuilder.ToString());
                        //Add project item.
                        folder.ProjectItems.AddFromFile(outputFile);
                    }

                    contextEventsName = this.GetStartEventsClassName();
                    outputFile = Path.Combine(Path.Combine(this.GetProjectPath(), folderName), contextEventsName + ".cs");
                    if (!File.Exists(outputFile) || !ExistsProjectItem(folder.ProjectItems, System.IO.Path.GetFileName(outputFile)))
                    {
                        if (!this.VerifySourceControl(outputFile))
                            return;

                        RemoveProjectItems(folder.ProjectItems, System.IO.Path.GetFileName(outputFile));
                        codeBuilder = new Linx.Tools.CodeBuilder();

                        //Add Events
                        this.GenerateStartEventsCode(codeBuilder, lxdmProject, contextEventsName);
                        System.IO.File.WriteAllText(outputFile, codeBuilder.ToString());
                        //Add project item.
                        folder.ProjectItems.AddFromFile(outputFile);
                    }

                }
            }
        }


        private void GenerateOperationalEventsCode(Linx.Tools.CodeBuilder codeBuilder, Project lxdmProject, string contextEventsName)
        {
            codeBuilder.AddLine("using System;");
            codeBuilder.AddLine("using System.Data.Entity;");
            codeBuilder.AddLine("using System.Data.Entity.Infrastructure;");
            codeBuilder.AddLine("using System.Collections.Generic;");
            codeBuilder.AddLine("using System.ComponentModel.DataAnnotations;");
            codeBuilder.AddLine("using System.ComponentModel.DataAnnotations.Schema;");
            codeBuilder.AddLine("using System.Data.Entity.ModelConfiguration.Conventions;");
            codeBuilder.AddLine("using Linx.Tools;");
            codeBuilder.AddLine("using System.Linq;");
            codeBuilder.AddLine("using System.Data;");

            codeBuilder.AddLine("");
            codeBuilder.AddLine("namespace " + this.GetNamespace(lxdmProject));
            codeBuilder.AddLine("{");

            codeBuilder.IncreaseIndent();

            //Class Definition
            codeBuilder.AddLine();
            codeBuilder.AddLine("/// <summary>");
            codeBuilder.AddLine("/// Events for executing rules before and after saving the context.");
            codeBuilder.AddLine("/// e.g.: var addedEntities = context.ChangeTracker.Entries().Where(c => c.State == EntityState.Added);");
            codeBuilder.AddLine("/// </summary>");
            codeBuilder.AddLine("public partial class " + contextEventsName);
            codeBuilder.AddLine("{");
            codeBuilder.IncreaseIndent();
            codeBuilder.AddLine("public static bool BeforeSaveChanges(DbContext context)");
            codeBuilder.AddLine("{");
            codeBuilder.AddLine("   return true;");
            codeBuilder.AddLine("}");
            codeBuilder.AddLine();
            codeBuilder.AddLine("public static void AfterSaveChanges(DbContext context)");
            codeBuilder.AddLine("{");
            codeBuilder.AddLine("   ");
            codeBuilder.AddLine("}");
            codeBuilder.DecreaseIndent();
            codeBuilder.AddLine("}");
            //End Class Definition

            codeBuilder.DecreaseIndent();

            //End Namespace
            codeBuilder.AddLine("}");
        }

        private void GenerateStartEventsCode(Linx.Tools.CodeBuilder codeBuilder, Project lxdmProject, string contextEventsName)
        {
            codeBuilder.AddLine("using System;");
            codeBuilder.AddLine("using System.Data.Entity;");
            codeBuilder.AddLine("using System.Data.Entity.Infrastructure;");
            codeBuilder.AddLine("using System.Collections.Generic;");
            codeBuilder.AddLine("using System.ComponentModel.DataAnnotations;");
            codeBuilder.AddLine("using System.ComponentModel.DataAnnotations.Schema;");
            codeBuilder.AddLine("using System.Data.Entity.ModelConfiguration.Conventions;");
            codeBuilder.AddLine("using Linx.Tools;");
            codeBuilder.AddLine("using System.Linq;");
            codeBuilder.AddLine("using System.Data;");

            codeBuilder.AddLine("");
            codeBuilder.AddLine("namespace " + this.GetNamespace(lxdmProject));
            codeBuilder.AddLine("{");

            codeBuilder.IncreaseIndent();

            //Class Definition
            codeBuilder.AddLine();
            codeBuilder.AddLine("public partial class " + contextEventsName);
            codeBuilder.AddLine("{");
            codeBuilder.IncreaseIndent();
            codeBuilder.AddLine("public static void Load(DbContext context)");
            codeBuilder.AddLine("{");
            codeBuilder.AddLine("   ");
            codeBuilder.AddLine("}");
            codeBuilder.DecreaseIndent();
            codeBuilder.AddLine("}");
            //End Class Definition

            codeBuilder.DecreaseIndent();

            //End Namespace
            codeBuilder.AddLine("}");
        }

        private void GenerateWebApiAutomaticControllerCode(List<ModelClass> modelClasses, List<BusinessDataModelDesignerRoot> models, Linx.Tools.CodeBuilder codeBuilder, WebApiController api, Project webApiProject, Project eadProject, string authorizeAttribute)
        {
            codeBuilder.AddLine("using System;");
            codeBuilder.AddLine("using System.Collections;");
            codeBuilder.AddLine("using System.Collections.Generic;");
            codeBuilder.AddLine("using System.Linq.Expressions;");
            codeBuilder.AddLine("using Linx.Tools;");
            codeBuilder.AddLine("using Linx.Business.Tools;");
            codeBuilder.AddLine("using System.Linq;");
            codeBuilder.AddLine("using System.ComponentModel;");
            codeBuilder.AddLine("using System.ComponentModel.DataAnnotations;");
            codeBuilder.AddLine("using System.ComponentModel.Composition;");
            codeBuilder.AddLine("using System.Net;");
            codeBuilder.AddLine("using System.Net.Http;");
            codeBuilder.AddLine("using System.Web.Http;");
            codeBuilder.AddLine("using Newtonsoft.Json.Linq;");
            codeBuilder.AddLine("using Linx.Data;");
            codeBuilder.AddLine("using System.Web.Http.OData;");
            codeBuilder.AddLine("using Linx.DataService;");
            codeBuilder.AddLine("using " + api.BusinessDataModelDesignerRoot.GetNamespace() + ";");

            var item = GetWebApiControllersItem(api, webApiProject);

            codeBuilder.AddLine("");
            codeBuilder.AddLine("namespace " + webApiProject.Name + "." + item.Name);
            codeBuilder.AddLine("{");

            codeBuilder.IncreaseIndent();

            //Class Definition            
            codeBuilder.AddLine();
            codeBuilder.AddLine("//Examples:");
            if (api.ExposeAllContext)
                codeBuilder.AddLine("// Default Call: http://localhost:1710/" + api.GetRoutePrefix());
            else
            {
                codeBuilder.AddLine("// Default Call: http://localhost:1710/" + api.GetRoutePrefix() + "/[ActionName]");
                codeBuilder.AddLine("[RoutePrefix(\"" + api.GetRoutePrefix() + "\")]");
                codeBuilder.AddLine("[Breeze.WebApi2.BreezeController]");

            }
            if (api.ExposeAllContext && !authorizeAttribute.IsNullOrEmpty())
                codeBuilder.AddLine("[ODataBasicAuthenticationFilter]");
            codeBuilder.AddLine("public partial class " + api.Name + "Controller : " + (api.ExposeAllContext ? "ODataController" : "ApiController"));
            codeBuilder.AddLine("{");
            codeBuilder.IncreaseIndent();
            string contextName = this.GetDefaultContextName();
            codeBuilder.AddLine("private " + contextName + " _context;");
            codeBuilder.AddLine("public " + contextName + " Context { get {  if (_context == null) { _context = new " + contextName + "(); } return _context; }  }");

            if (api.ExposeAllContext)
            {
                codeBuilder.AddLine();
                //Domain Service reference
                if (modelClasses == null)
                    modelClasses = this.Types.Where(e => e is ModelClass).Select(e => (ModelClass)e).ToList();

                foreach (var entity in modelClasses.Where(e => !((ModelClass)e).InStudy && !((ModelClass)e).NotMapped).Select(e => (ModelClass)e).OrderBy(e => e.Name))
                {
                    var pKeys = entity.GetPrimaryKeys();
                    if (entity.Kind == ClassKind.ModelView || pKeys.Count() > 0)
                    {
                        if (pKeys.Count() > 0)
                        {
                            codeBuilder.AddLine();
                            if (!authorizeAttribute.IsNullOrEmpty())
                                codeBuilder.AddLine("[" + authorizeAttribute + "]");
                            codeBuilder.AddLine("[EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]");
                            codeBuilder.AddLine("public IQueryable<" + entity.Name + "> Get" + entity.Name + "(" + String.Join(", ", pKeys.Select(p => p.GetDataType() + " key" + (pKeys.IndexOf(p) > 0 ? pKeys.IndexOf(p).ToString() : ""))) + ")");
                            codeBuilder.AddLine("{");
                            codeBuilder.AddLine("    return this.Context." + entity.Name + ".Where(e => " + String.Join(" && ", pKeys.Select(p => "e." + p.Name + " == key" + (pKeys.IndexOf(p) > 0 ? pKeys.IndexOf(p).ToString() : ""))) + ").AsQueryable();");
                            codeBuilder.AddLine("}");
                        }
                        codeBuilder.AddLine();
                        if (!authorizeAttribute.IsNullOrEmpty())
                            codeBuilder.AddLine("[" + authorizeAttribute + "]");
                        codeBuilder.AddLine("[EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]");
                        codeBuilder.AddLine("public IQueryable<" + entity.Name + "> Get" + entity.Name + "()");
                        codeBuilder.AddLine("{");
                        codeBuilder.AddLine("    return this.Context." + entity.Name + ".AsQueryable();");
                        codeBuilder.AddLine("}");

                        if (entity.Kind == ClassKind.Table && pKeys.Count() > 0)
                        {
                            //Get all relation types
                            List<string> relationships = entity.GetForeignKeyProperties(models, null).Split(new string[] { "public virtual " }, StringSplitOptions.RemoveEmptyEntries).Select(e => e.Left(" ")).Where(e => !e.IsNullOrEmpty()).Distinct().ToList();
                            var collections = entity.GetForeignKeyCollecions(models, null).Split(new string[] { "public virtual " }, StringSplitOptions.RemoveEmptyEntries).Select(e => e.Left(" ")).Distinct().ToArray();
                            for (int idx = 1; idx < collections.Length; idx++)
                            {
                                string entityName = collections[idx];
                                if (entityName.Contains("ICollection<"))
                                    entityName = entityName.Extract("ICollection<", ">");
                                if (!entityName.IsNullOrEmpty() && !relationships.Contains(entityName))
                                    relationships.Add(entityName);
                            }

                            foreach (var associationName in relationships)
                            {
                                codeBuilder.AddLine();
                                if (!authorizeAttribute.IsNullOrEmpty())
                                    codeBuilder.AddLine("[" + authorizeAttribute + "]");
                                codeBuilder.AddLine("[EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]");
                                codeBuilder.AddLine("public IQueryable<" + associationName + "> Get" + entity.Name + "__" + associationName + "(" + String.Join(", ", pKeys.Select(p => p.GetDataType() + " key" + (pKeys.IndexOf(p) > 0 ? pKeys.IndexOf(p).ToString() : ""))) + ", string navigation)");
                                codeBuilder.AddLine("{");
                                codeBuilder.AddLine("    var entity = this.Context." + entity.Name + ".Include(navigation).FirstOrDefault(e => " + String.Join(" && ", pKeys.Select(p => "e." + p.Name + " == key" + (pKeys.IndexOf(p) > 0 ? pKeys.IndexOf(p).ToString() : ""))) + ");");
                                codeBuilder.AddLine("    if (entity != null)");
                                codeBuilder.AddLine("    {");
                                codeBuilder.AddLine("       var navProperty = entity.GetPropertyValue(navigation);");
                                codeBuilder.AddLine("       if (navProperty is " + associationName + ")");
                                codeBuilder.AddLine("           return (new " + associationName + "[] { (" + associationName + ")navProperty }).AsQueryable();");
                                codeBuilder.AddLine("       else");
                                codeBuilder.AddLine("           return ((IEnumerable<" + associationName + ">)navProperty).AsQueryable();");
                                codeBuilder.AddLine("    }");
                                codeBuilder.AddLine("    else");
                                codeBuilder.AddLine("       return default(IQueryable<" + associationName + ">);");
                                codeBuilder.AddLine("}");
                            }
                        }
                    }
                }
            }

            codeBuilder.DecreaseIndent();
            codeBuilder.AddLine("}");
            codeBuilder.AddLine();
            //End Class Definition

            //Authorization Definition
            if (!authorizeAttribute.IsNullOrEmpty())
            {
                codeBuilder.AddLine("public partial class " + api.Name + "ControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute");
                codeBuilder.AddLine("{");
                codeBuilder.AddLine("    protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)");
                codeBuilder.AddLine("    {");
                codeBuilder.AddLine("        return LinxAutorization.CheckAuthorization(actionContext, string.Format(\"{0}#{1}#{1}/{2}\", \"" + api.BusinessDataModelDesignerRoot.TargetNamespace + "\", \"" + api.GetRoutePrefix() + "\", actionContext.ActionDescriptor.ActionName));");
                codeBuilder.AddLine("    }");
                codeBuilder.AddLine("}");
                codeBuilder.AddLine();
            }

            //End Namespace
            codeBuilder.DecreaseIndent();
            codeBuilder.AddLine("}");
        }

        public string GetWebApiClassFile(WebApiController api, Project webApiProject = null, bool isAutomatic = false)
        {
            string outputFile = String.Empty;
            ProjectItem item = null;

            if (webApiProject == null)
                webApiProject = this.GetWebApiProject(api.ProjectSuffix);

            if (webApiProject != null)
                item = GetWebApiControllersItem(api, webApiProject); ;

            if (!item.IsNull())
                outputFile = Path.Combine(GetProjectPath(webApiProject), Path.GetFileNameWithoutExtension(item.Name) + "\\" + api.Name + (isAutomatic ? "AutoGen" : "") + ".cs");

            return outputFile;
        }

        public string GetWebApiClassODataStartFile(WebApiController api, Project webApiProject = null)
        {
            string outputFile = String.Empty;
            ProjectItem item = null;

            if (webApiProject == null)
                webApiProject = this.GetWebApiProject(api.ProjectSuffix);

            if (webApiProject != null)
                item = GetWebApiAppStartItem(api, webApiProject); ;

            if (!item.IsNull())
                outputFile = Path.Combine(GetProjectPath(webApiProject), Path.GetFileNameWithoutExtension(item.Name) + "\\" + api.Name + "ODataStart" + ".cs");

            return outputFile;
        }

        public string GetWebApiAppStartClassFile(WebApiController api, Project webApiProject = null, string className = "")
        {
            string outputFile = String.Empty;
            ProjectItem item = null;

            if (webApiProject == null)
                webApiProject = this.GetWebApiProject(api.ProjectSuffix);

            if (webApiProject != null)
                item = GetWebApiAppStartItem(api, webApiProject); ;

            if (!item.IsNull())
                outputFile = Path.Combine(GetProjectPath(webApiProject), Path.GetFileNameWithoutExtension(item.Name) + "\\" + className + ".cs");

            return outputFile;
        }

        public ProjectItem GetWebApiControllersItem(WebApiController api, Project webApiProject = null)
        {
            string outputFile = String.Empty;
            ProjectItem item = null;

            if (webApiProject == null)
                webApiProject = this.GetWebApiProject(api.ProjectSuffix);

            if (webApiProject != null)
                item = GetProjectItemByName(webApiProject, "Controllers");

            return item;
        }

        public ProjectItem GetWebApiAppStartItem(WebApiController api, Project webApiProject = null)
        {
            string outputFile = String.Empty;
            ProjectItem item = null;

            if (webApiProject == null)
                webApiProject = this.GetWebApiProject(api.ProjectSuffix);

            if (webApiProject != null)
                item = GetProjectItemByName(webApiProject, "App_Start");

            return item;
        }

        public string GetWebApiProjectName(string projectSuffix, Project eadProject)
        {
            return eadProject.Name + ".WebAPI" + (projectSuffix.IsNullOrEmpty() ? String.Empty : "." + projectSuffix);
        }

        public Project GetWebApiProject(string projectSuffix, Project eadProject = null)
        {
            if (eadProject == null)
                eadProject = GetLxdmProject();
            if (eadProject != null)
                return this.GetProjectByName(GetWebApiProjectName(projectSuffix, eadProject));
            else
                return null;
        }

        public void UpdateWebApiProject(Project lxdmProject, WebApiController api)
        {
            EnvDTE.DTE appDTE = lxdmProject.DTE;
            string webApiProjectName = GetWebApiProjectName(api.ProjectSuffix, lxdmProject);
            Project webApiProject = GetProjectByName(webApiProjectName);
            string folderName = "Web API Controllers";
            EnvDTE80.SolutionFolder webApiDesignerFolder = null;

            if (webApiProject == null)
            {
                string webApiDir = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(lxdmProject.FullName), "..\\" + webApiProjectName));

                if (!System.IO.Directory.Exists(webApiDir))
                    System.IO.Directory.CreateDirectory(webApiDir);

                var tmpProj = this.GetProjectByName(folderName);
                webApiDesignerFolder = (tmpProj == null ? null : tmpProj.Object) as EnvDTE80.SolutionFolder;
                if (webApiDesignerFolder == null)
                    webApiDesignerFolder = (EnvDTE80.SolutionFolder)((EnvDTE100.Solution4)appDTE.Solution).AddSolutionFolder(folderName).Object;

                if (System.IO.File.Exists(System.IO.Path.Combine(webApiDir, webApiProjectName + ".csproj")))
                {
                    if (webApiDesignerFolder != null)
                        webApiDesignerFolder.AddFromFile(System.IO.Path.Combine(webApiDir, webApiProjectName + ".csproj"));
                    else
                        ((EnvDTE100.Solution4)appDTE.Solution).AddFromFile(System.IO.Path.Combine(webApiDir, webApiProjectName + ".csproj"), false);
                }
                else
                {
                    // Get the location of the project templates
                    string templateName = ((EnvDTE100.Solution4)appDTE.Solution).GetProjectTemplate("Class Library", "CSharp");
                    if (webApiDesignerFolder != null)
                        webApiDesignerFolder.AddFromTemplate(templateName, webApiDir, webApiProjectName);
                    else
                        ((EnvDTE100.Solution4)appDTE.Solution).AddFromTemplate(templateName, webApiDir, webApiProjectName, false);

                    webApiProject = GetProjectByName(webApiProjectName);

                    //Delete Class1.cs from template project
                    if (ExistsProjectItem(webApiProject.ProjectItems, "Class1.cs"))
                        webApiProject.ProjectItems.Item("Class1.cs").Delete();

                    webApiProject.ProjectItems.AddFolder("App_Start");
                    webApiProject.ProjectItems.AddFolder("Controllers");

                    //Set Assembly Name
                    webApiProject.Properties.Item("AssemblyName").Value = webApiProjectName;
                    //Set Default Namespace
                    webApiProject.Properties.Item("DefaultNamespace").Value = webApiProjectName;
                }
            }

            this.RemoveReferencesWithoutFile(webApiProject);
            this.UpdateVersion(webApiProject);

            //Upgrade to last framework version
            UpgradeVersion(webApiProject);

            //Add project reference
            AddProjectReference(webApiProject, lxdmProject, false);

            //Update library references            
            this.AddReference(webApiProject, "System.ServiceModel.DomainServices.Server.dll");
            this.RemoveReference(webApiProject, "System.Data.Entity");
            this.AddReference(webApiProject, "System.Web.dll");
            this.AddReference(webApiProject, "System.Net.Http.dll");
            this.AddReference(webApiProject, "System.ComponentModel.DataAnnotations.dll");
            this.AddReference(webApiProject, "Linx.Tools.dll");
            this.AddReference(webApiProject, "Linx.LinqExtensions.dll");
            this.AddReference(webApiProject, "System.ComponentModel.Composition.dll");
            this.UpdateLibReferences(webApiProject, "Linx.Business.Desktop.Tools", false);
            this.UpdateLibReferences(webApiProject, "Linx.WebApi.Library", false, true, true);
            this.UpdateLibReferences(webApiProject, "Linx.Data.Library", false);
            this.UpdateLibReferences(webApiProject, "Linx.CodeFirst.EF", false);
            this.UpdateLibReferences(webApiProject, "Linx.DataService.Library", false, false, true);
            this.UpdateLibReferences(webApiProject, "Linx.LinxDataService.Library", false, true, false);

            //Set PostBuildEvent
            SetPostBuildEventToServiceBus(webApiProject);
        }

        public void SetPostBuildEventToServiceBus(Project current = null)
        {
            if (current == null)
                current = this.GetLxdmProject();
            if (current != null)
            {
                string serviceBusPath = this.GetFullPath("Linx.Web.Service.Bus");
                if (serviceBusPath.IsNullOrEmpty())
                    return;

                if (!serviceBusPath.IsNullOrEmpty() && Directory.Exists(serviceBusPath))
                {
                    string relativePath = Path.GetDirectoryName(current.FullName).GetRelativePath(serviceBusPath);
                    if (!relativePath.IsNullOrEmpty())
                        serviceBusPath = "$(ProjectDir)" + relativePath;

                    string postBuildEventCommand = GetServiceBusCopyCommands(current, serviceBusPath + @"\bin");
                    string postEbentValue = current.Properties.Item("PostBuildEvent").Value.ToString();
                    if (postEbentValue.IsNullOrEmpty() || !postEbentValue.Contains(postBuildEventCommand))
                        current.Properties.Item("PostBuildEvent").Value = postBuildEventCommand;

                }
            }
        }

        public string GetServiceBusCopyCommands(Project project, string outputDir)
        {
            string xCopyCommands = @"xcopy ""$(TargetName).dll"" """ + outputDir + @""" /Y /R" + "\r\n";
            VSLangProj.VSProject vsProject = (VSLangProj.VSProject)project.Object;
            foreach (VSLangProj.Reference reference in vsProject.References)
            {
                if (reference.CopyLocal)
                    xCopyCommands += @"xcopy ""$(TargetDir)" + reference.Name + @".dll"" """ + outputDir + @""" /Y /R" + "\r\n";
            }
            return xCopyCommands;
        }
        #endregion

        public string GetProjectPath()
        {
            return GetProjectPath(this.GetLxdmProject());
        }

        public static string GetProjectPath(Project current)
        {
            if (current == null)
                return "";
            else
                return Path.GetDirectoryName(current.FullName);
        }

        public static T GetModelBusManager<T>(IModelBus modelBus)
        {
            string assemblyName = "Linx.BusinessDataModelDesigner.ModelBusAdapter.dll";
            var catalog = new System.ComponentModel.Composition.Hosting.AggregateCatalog(new System.ComponentModel.Composition.Hosting.DirectoryCatalog(AssemblyHelper.GetCurrentAssemblyDirectory<BusinessDataModelDesignerRoot>(), assemblyName));
            System.ComponentModel.Composition.Hosting.CompositionContainer container = new System.ComponentModel.Composition.Hosting.CompositionContainer(catalog);

            var pluginExport = container.GetExports<T, IDictionary<string, object>>().FirstOrDefault();
            if (pluginExport != null)
            {
                var manager = pluginExport.Value;
                if (manager is ILinxModelBus)
                    ((ILinxModelBus)manager).UpdateModelBus(modelBus);
                return manager;
            }

            return default(T);
        }

        public TextSelection OpenClassOperation(string fileName, Operation targetOperation, string className, ProjectItem item)
        {
            return OpenClassOperation(fileName, targetOperation, className, item, String.Empty);
        }

        public TextSelection OpenClassOperation(string fileName, Operation targetOperation, string className, ProjectItem item, String insertCommandText)
        {
            TextSelection selection = null;

            if (targetOperation.OverloadName.IsNullOrEmpty())
            {
                MessageBox.Show("Cannot open the operation because the OverloadName property is empty!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return selection;
            }

            if (item.IsNull())
                item = GetDiagramProjectItem();

            if (!item.IsNull())
            {
                if (BusinessDataModelDesignerRoot.ExistsProjectItem(item.ProjectItems, fileName))
                {
                    Window window = item.ProjectItems.Item(fileName).Open(vsViewKindCode);
                    window.SetFocus();
                    selection = ((TextSelection)item.ProjectItems.Item(fileName).Document.Selection);
                    selection.OpenOperation(targetOperation, className, insertCommandText);
                }
                else
                    MessageBox.Show(String.Format("File [{0}] does not exist!", fileName), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return selection;
        }

        #endregion Open any code element.


        #region Commom tasks

        public void GenerateScript(BusinessDataModelDesignerDiagram diagram)
        {
            if (!this.EnableMigration())
            {
                MessageBox.Show("The migration is disabled for the active provider!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var project = this.GetLxdmProject();
            if (project != null)
            {
                string nodePath = Path.Combine(this.GetProjectPath(), "NodePack");
                string scriptFile = Path.Combine(nodePath, "script.sql");

                ScriptGeneratorBase scriptGen = null;
                switch (this.GetDefaultProvider())
                {
                    case Provider.SQLServer:
                        scriptGen = new ScriptGeneratorSqlServer();
                        break;
                    case Provider.SQLite:
                        scriptGen = new ScriptGeneratorSqlite();
                        break;
                    case Provider.MySQL:
                        scriptGen = new ScriptGeneratorMySql();
                        break;
                    case Provider.PostgreSQL:
                        scriptGen = new ScriptGeneratorPostgreSQL();
                        break;
                    default:
                        break;
                }
                //Generate script content
                scriptGen.GenerateScript(ConceptualDataBase.GetDataBase(this), scriptFile);

                if (File.Exists(scriptFile))
                {
                    var code = this.DTEReference.ItemOperations.OpenFile(scriptFile);
                }
            }
        }


        public void ExecuteFile(string exeFilePath, string arguments)
        {
            // Use ProcessStartInfo class
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.FileName = exeFilePath;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = arguments;

            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                using (System.Diagnostics.Process exeProcess = System.Diagnostics.Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                }
            }
            catch
            {
                MessageBox.Show("Fail executing the file [" + startInfo.FileName + "].", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // Log error.
            }
        }


        public void ValidSqlMigratorFKs(List<BusinessDataModelDesignerRoot> models, string projectPath)
        {
            List<string> errors = new List<string>();
            string objectName;
            Dictionary<String, String> fkControl = new Dictionary<string, string>();
            //Default Associations
            foreach (var model in models)
            {
                foreach (ModelClass modelClass in model.Types.Where(e => e is ModelClass && ((ModelClass)e).Kind == ClassKind.Table))
                {
                    foreach (var link in Association.GetLinksToSourceModelClasses(modelClass))
                    {
                        objectName = link.SourceModelClass.GetTableName() + "." +
                        String.Join(".", link.SourceModelClass.GetTopSuperClass().Attributes.Where(e => e.IsPrimaryKey && !e.IsNullable).Select(e => e.GetColumnName()).OrderBy(e => e).ToArray()) + "." +
                        modelClass.GetTableName() + "." +
                        String.Join(".", link.GetTargetColumns());
                        string command = link.GetFkName(true, this.IsDynamicForeignKeyNames()) + "," + link.WillCascadeOnDelete.ToString().ToLower();
                        if (!fkControl.ContainsKey(objectName))
                        {
                            fkControl.Add(objectName, link.SourceModelClass.BusinessDataModelDesignerRoot.DocumentName + "::" + command);
                        }
                        else
                        {
                            if (fkControl[objectName].Right("::") != command)
                            {
                                string description = String.Format("-Warning: Foreignkey differences detected between the tables {0} and {1} on files {2} and {3}.", link.SourceModelClass.Name, link.TargetModelClass.Name, fkControl[objectName].Left("::"), link.SourceModelClass.BusinessDataModelDesignerRoot.DocumentName);
                                errors.Add(description);
                            }
                        }
                    }
                }

                //Multiple Associations
                foreach (ModelClass modelClass in model.Types.Where(e => e is ModelClass && ((ModelClass)e).Kind == ClassKind.Table))
                {
                    var lma = MultipleAssociationTarget.GetLinkToMultipleAssociation(modelClass);
                    if (lma != null && lma.MultipleAssociation != null)
                    {
                        foreach (var link in MultipleAssociationOrigin.GetLinksToOriginTypes(lma.MultipleAssociation))
                        {
                            objectName = link.OriginType.GetTableName() + "." +
                            String.Join(".", link.OriginType.GetTopSuperClass().Attributes.Where(e => e.IsPrimaryKey && !e.IsNullable).Select(e => e.GetColumnName()).OrderBy(e => e).ToArray()) + "." +
                            modelClass.GetTableName() + "." +
                            String.Join(".", link.GetTargetColumns());
                            string command = link.GetFkName(true, this.IsDynamicForeignKeyNames()) + "," + link.WillCascadeOnDelete.ToString().ToLower();
                            if (!fkControl.ContainsKey(objectName))
                            {
                                fkControl.Add(objectName, link.OriginType.BusinessDataModelDesignerRoot.DocumentName + "::" + command);
                            }
                            else
                            {
                                if (fkControl[objectName].Right("::") != command)
                                {
                                    string description = String.Format("-Warning: Foreignkey differences detected between the tables {0} and {1} on files {2} and {3}.", link.OriginType.Name, link.MultipleAssociation.TargetType.Name, fkControl[objectName].Left("::"), link.OriginType.BusinessDataModelDesignerRoot.DocumentName);
                                    errors.Add(description);
                                }
                            }
                        }
                    }
                }
            }

            string errorFile = System.IO.Path.Combine(projectPath, "_fkWarnings.err");
            if (errors.Count > 0)
                File.WriteAllLines(errorFile, errors);
            else
                File.Delete(errorFile);

        }

        private void ValidFKErrors()
        {
            var lxdmProj = this.GetLxdmProject();
            string errorFile = System.IO.Path.Combine(lxdmProj.Properties.Item("FullPath").Value.ToString(), "_fkWarnings.err");
            if (File.Exists(errorFile))
            {
                var errors = File.ReadAllText(errorFile);
                MessageBox.Show(errors, "Foreignkey Warnings", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ValidLxdms(ValidationContext context)
        {
            List<string> lxdmList = new List<string>();
            foreach (var item in GetProjectModels())
            {
                lxdmList.Add(item.Value.Properties.Item("FullPath").Value.ToString().ToLower());
            }

            //Check inconsistent files
            var lxdmProj = this.GetLxdmProject();
            string alertFileList = String.Empty;
            string[] lxdmFiles = System.IO.Directory.GetFiles(lxdmProj.Properties.Item("FullPath").Value.ToString(), "*.lxdm", SearchOption.AllDirectories);
            foreach (var file in lxdmFiles.Where(e => !lxdmList.Contains(e.ToLower())))
            {
                alertFileList += (alertFileList.IsNullOrEmpty() ? "" : ", ") + file.ToLower().Right(((string)lxdmProj.Properties.Item("FullPath").Value).ToLower()).ToUpper();
            }

            if (!alertFileList.IsNullOrEmpty())
            {
                string description = String.Format(CultureInfo.CurrentCulture, "The files '{0}' exist in the project folder, but not in the project reference.", alertFileList);
                context.LogError(description, "BMDs Error");
            }
        }

        public void UpdatePackages()
        {
            //Instalar pacotes Nodejs
        }

        public string GetConfigEFProviders(string indent)
        {
            var defaultProvType = this.GetDefaultProvider();
            if (defaultProvType == Provider.SQLite)
            {
                return "<providers><provider invariantName=\"System.Data.SQLite\" type=\"System.Data.SQLite.EF6.SQLiteProviderServices, System.Data.SQLite.EF6\" /></providers>";
            }
            else
                return "";
        }

        public void ExecuteNuGetCommand(string command)
        {
            if (!command.IsNullOrEmpty())
            {
                BusinessDataModelDesignerDiagram diagram = this.GetPresentation<BusinessDataModelDesignerDiagram>();
                if (diagram != null)
                {
                    DTE dte = (DTE)diagram.GetService(typeof(DTE));
                    if (dte != null)
                    {
                        try
                        {
                            Linx.Tools.CodeBuilder builder = new Tools.CodeBuilder();
                            builder.AddLine("Clear");
                            builder.AddLine(command);
                            dte.ExecuteCommand("View.PackageManagerConsole", builder.GetBody()); //The name of dialog is Project.ManageNuGetPackages                            
                        }
                        catch (Exception excep)
                        {
                            MessageBox.Show(excep.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
        }

        public void StartNuGetConsole()
        {
            BusinessDataModelDesignerDiagram diagram = this.GetPresentation<BusinessDataModelDesignerDiagram>();
            if (diagram != null)
            {
                OleMenuCommandService menuCommandService = diagram.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
                var menuCommandID = new CommandID(GuidList.guidNuGetConsoleCmdSet, PkgCmdIDList.cmdidPowerConsole);
                menuCommandService.GlobalInvoke(menuCommandID);
            }
        }


        public bool IsRemoveAutomaticIndexes()
        {
            //DataContextName, ProviderType, ConnectionString
            var contextInfo = this.GetSharedInfo();
            return contextInfo.ContainsKey("RemoveAutomaticIndexes") && contextInfo["RemoveAutomaticIndexes"] == "true";
        }

        public bool HasAutomaticAuthorization()
        {
            //DataContextName, ProviderType, ConnectionString
            var contextInfo = this.GetSharedInfo();
            return contextInfo.ContainsKey("EnableAutomaticAuthorization") && contextInfo["EnableAutomaticAuthorization"] == "true";
        }

        public bool HasIdLinxAsIdGpecon()
        {
            //DataContextName, ProviderType, ConnectionString
            var contextInfo = this.GetSharedInfo();
            return contextInfo.ContainsKey("SetIdLinxWithIdGpecon") && contextInfo["SetIdLinxWithIdGpecon"] == "true";
        }

        public bool HasAccessConnectionControl()
        {
            var contextInfo = this.GetSharedInfo();
            return contextInfo.ContainsKey("EnableAccessConnectionControl") && contextInfo["EnableAccessConnectionControl"] == "true";
        }

        public bool IsDynamicForeignKeyNames()
        {
            var contextInfo = this.GetSharedInfo();
            return contextInfo.ContainsKey("ForceDynamicForeignKeyNames") && contextInfo["ForceDynamicForeignKeyNames"] == "true";
        }

        public Provider GetDefaultProvider()
        {
            var contextInfo = this.GetSharedInfo();
            return (Provider)Enum.Parse(typeof(Provider), contextInfo["ProviderType"]);
        }

        public bool EnableMigration()
        {
            var contextInfo = this.GetSharedInfo();
            if (contextInfo != null && contextInfo.ContainsKey("EnableMigration"))
                return contextInfo["EnableMigration"] == "true";
            else
                return false;
        }

        public static Provider GetDefaultProvider(Project current)
        {
            var contextInfo = GetSharedInfo(current);
            if (contextInfo != null && contextInfo.ContainsKey("ProviderType"))
                return (Provider)Enum.Parse(typeof(Provider), contextInfo["ProviderType"]);
            else
                return Provider.SQLServer;
        }

        public string GetDefaultContextName()
        {
            //DataContextName, ProviderType, ConnectionString
            var contextInfo = this.GetSharedInfo();
            return contextInfo["DataContextName"];
        }

        public static string GetDefaultContextName(Project current)
        {
            //DataContextName, ProviderType, ConnectionString
            var contextInfo = GetSharedInfo(current);
            if (contextInfo != null && contextInfo.ContainsKey("DataContextName"))
                return contextInfo["DataContextName"];
            else
                return "";
        }

        #endregion

        #region Commands For Menus


        public void FindElement()
        {
            var finder = new frmFindElement() { Model = this };
            finder.ShowDialog();
            if (!finder.ElementSelection.IsNullOrEmpty())
            {
                ShowClassElement(finder.ItemSelection, finder.ElementSelection);
            }
        }

        public void ShowClassElement(ProjectItem item, string className)
        {
            if (item == null)
            {
                this.SelectShape(className);
                return;
            }

            IModelBus modelBus = this.GetModelBus();
            if (modelBus == null)
                return;

            // Get an adapterManager for the target DSL:
            ModelBusAdapterManager manager = BusinessDataModelDesignerRoot.GetModelBusManager<ModelBusAdapterManager>(modelBus);
            // Create a reference to the target model:
            ModelBusReference modelReference = manager.CreateReference(item);

            using (ModelBusAdapter modelAdapter = manager.CreateAdapter(modelReference))
            {
                if (modelAdapter != null)
                {
                    BusinessDataModelDesignerRoot modelRoot = modelAdapter.GetPropertyValue("ModelRoot") as BusinessDataModelDesignerRoot;
                    if (modelRoot != null)
                    {
                        var element = modelRoot.Types.FirstOrDefault(e => (e is ModelClass || e is DomainView) && e.Name == className);
                        if (element != null)
                        {
                            var classRef = modelAdapter.GetElementReference(element);
                            if (classRef != null)
                                modelRoot.NavigateTo(classRef);
                        }
                    }
                }
            }
        }

        public bool ExistsAnyReference(ProjectItem item, string[] typeNames)
        {
            bool exists = false;
            // Get ModelBus
            IModelBus modelBus = this.GetModelBus();
            if (modelBus == null)
                return exists;

            try
            {
                // Get an adapterManager for the target DSL:
                ModelBusAdapterManager manager = BusinessDataModelDesignerRoot.GetModelBusManager<ModelBusAdapterManager>(modelBus);

                // Create a reference to the target model:
                ModelBusReference modelReference = manager.CreateReference(item);
                BusinessDataModelDesignerRoot modelRoot = this.GetModelRoot<BusinessDataModelDesignerRoot>(modelReference, manager);

                exists = modelRoot.Types.Any(e => e is ReferenceModelClass && typeNames.Contains(e.Name));
            }
            catch { }

            return exists;
        }

        public void OpenAllDesigners()
        {
            var typeNames = this.Types.Where(e => e is ModelClass).Select(e => e.Name).ToArray();
            foreach (var item in GetProjectModels(true))
            {
                if (ExistsAnyReference(item.Value, typeNames))
                    this.ShowDesigner(item.Value);
            }
        }

        public Dictionary<string, ProjectItem> GetProjectModels(bool removeCurrent = false)
        {
            Dictionary<string, ProjectItem> innerModels = new Dictionary<string, ProjectItem>();
            var currentProject = this.GetLxdmProject();

            if (currentProject != null)
            {
                foreach (EnvDTE.Project project in currentProject.DTE.Solution.Projects)
                {
                    foreach (var element in GetModels(project.ProjectItems, removeCurrent))
                    {
                        if (!innerModels.ContainsKey(element.Key))
                            innerModels.Add(element.Key, element.Value);
                    }
                }
            }
            return innerModels;
        }

        public Dictionary<string, ProjectItem> GetModelsFromCurrentProject(bool removeCurrent = false)
        {
            Dictionary<string, ProjectItem> innerModels = new Dictionary<string, ProjectItem>();
            EnvDTE.Project project = this.GetLxdmProject();
            if (project != null)
            {
                foreach (var element in GetModels(project.ProjectItems, removeCurrent))
                {
                    if (!innerModels.ContainsKey(element.Key))
                        innerModels.Add(element.Key, element.Value);
                }
            }
            return innerModels;
        }


        private Dictionary<string, ProjectItem> GetModels(ProjectItems items, bool removeCurrent)
        {
            Dictionary<string, ProjectItem> desiners = new Dictionary<string, ProjectItem>();
            string fName, fPath, completePath, key;

            if (items == null)
                return desiners;

            foreach (ProjectItem item in items)
            {
                if (item.Name.Right(5).ToLower() == ".lxdm")
                {
                    completePath = item.Properties.Item("FullPath").Value.ToString().ToUpper();
                    fName = Path.GetFileName(completePath);
                    fPath = Path.GetDirectoryName(completePath);
                    key = fName + "  -  " + fPath;
                    if (!(removeCurrent && completePath == System.IO.Path.Combine(this.DocumentPath, this.DocumentName).ToUpper()))
                    {
                        if (!desiners.ContainsKey(key))
                            desiners.Add(key, item);
                    }
                }
                else
                {
                    ProjectItems innerItems = item.ProjectItems;
                    if (innerItems == null && item.SubProject != null && item.SubProject.ProjectItems != null)
                        innerItems = item.SubProject.ProjectItems;
                    if (innerItems != null)
                    {
                        foreach (var element in GetModels(innerItems, removeCurrent))
                        {
                            if (!desiners.ContainsKey(element.Key))
                                desiners.Add(element.Key, element.Value);
                        }
                    }
                }
            }

            return desiners;
        }

        public void ExecuteReverseEngineer(BusinessDataModelDesignerDiagram designer)
        {
            Provider defaultProv = this.GetDefaultProvider();

            string errorMessage = String.Empty;
            string connectioString = GetConfigConnectionString();

            if (connectioString.IsNullOrEmpty())
            {
                errorMessage = "No default provider found or the connection is not configured yet. You can add one using the toolbox or configuring its properties.";
            }
            else if (connectioString == vsInvalidProvider)
            {
                errorMessage = "This operation is supported only for [SQL Server, SQLite, MySQL] providers.";
            }

            if (!errorMessage.IsNullOrEmpty())
            {
                MessageBox.Show(errorMessage, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var reverseEng = new frmReverseEngineer() { ConnectionString = connectioString, ConnectionProvider = getConfigProvider(), SuggestedSelection = this.Types.Where(e => e is ModelClass && !(e is ReferenceModelClass)).Select(e => ((ModelClass)e).Schema + "." + ((ModelClass)e).Name).ToList() };
            reverseEng.ShowDialog();

            if (reverseEng.OK)
            {
                string pkName;
                ClassKind kind;
                bool isClustered;

                using (Transaction t = this.Store.TransactionManager.BeginTransaction("Reverse Engineering"))
                {
                    try
                    {
                        this.IsLocked = true;

                        foreach (var table in reverseEng.SelectTables)
                        {
                            kind = (table is Linx.Tools.Migration.View ? ClassKind.DatabaseView : ClassKind.Table);
                            pkName = (kind == ClassKind.Table && ((Linx.Tools.Migration.Table)table).PrimaryKey != null ? ((Linx.Tools.Migration.Table)table).PrimaryKey.Name : String.Empty);
                            isClustered = (kind == ClassKind.Table && ((Linx.Tools.Migration.Table)table).PrimaryKey != null ? ((Linx.Tools.Migration.Table)table).PrimaryKey.IsClustered : false);


                            //Add Clsses
                            this.AddTableClass(table.Name, kind, table.Schema.Name, pkName, isClustered);
                            //Add Attributes
                            int indeOrder = 0;
                            this.CheckColumns(table.Name, table.Columns.Select(e => e.Name).ToArray());
                            foreach (var column in table.Columns)
                            {
                                this.AddColumnAttribute(table.Name, column.Name, DbToModelDataType(column.DbDataType, column.MaxLength), column.IsPK, column.IsIdentity, column.IsNullable, column.Precision, column.Scale, column.MaxLength, indeOrder, column.SqlDefault);
                                indeOrder++;
                            }
                            //Add Indexes
                            if (kind == ClassKind.Table)
                            {
                                indeOrder = 0;
                                this.CheckIndexes(table.Name, ((Linx.Tools.Migration.Table)table).Indexes.Select(e => e.Name).ToArray());
                                foreach (var index in ((Linx.Tools.Migration.Table)table).Indexes)
                                {
                                    this.AddTableIndex(table.Name, index.Name, index.CommandColumns, index.IsUnique, indeOrder, index.IsClustered);
                                    indeOrder++;
                                }
                            }
                        }

                        //Add Associations
                        foreach (Linx.Tools.Migration.Table table in reverseEng.SelectTables.Where(e => e is Linx.Tools.Migration.Table))
                        {
                            this.CheckAssociations(table.Name, table.ForeignKey.Select(e => e.Name).ToArray());
                            foreach (var link in table.ForeignKey)
                            {
                                this.AddTableAssociation(link.Name, link.Referenced.Name, link.Parent.Name, GetDbMultiplicity(link), link.DeleteAction == Linx.Tools.Migration.ForeignKey.ReferentialAction.Cascade, link.ForeignKeyColumns.Select(e => e.ParentColumn.Name + ":" + e.ReferencedColumn.Name).ToArray());
                            }
                        }

                        t.Commit();
                    }
                    catch (Exception e)
                    {
                        t.Rollback();
                        throw e;
                    }
                    finally
                    {
                        this.IsLocked = false;
                    }
                }

                designer.ArrangeLayout();
                this.Repaint();
            }
        }

        private Multiplicity GetDbMultiplicity(ForeignKey fk)
        {
            if (("," + String.Join(",", fk.Parent.PrimaryKey.Columns.Select(e => e.Name).OrderBy(e => e)) + ",") == ("," + String.Join(",", fk.ForeignKeyColumns.Select(e => e.ParentColumn.Name).OrderBy(e => e)) + ","))
                return Multiplicity.One;
            else
            {
                if (fk.ForeignKeyColumns.Any(e => e.ParentColumn.IsNullable))
                    return Multiplicity.ZeroMany;
                else
                    return Multiplicity.Many;
            }

        }


        public void AddExternalReferences()
        {
            var frm = new frmExternalReferenceClass();
            frm.Designer = this;
            frm.ShowDialog();
        }

        public void Refresh()
        {
            this.UpdateAllReferenceClassRelations();
        }

        public void SendToBack(ShapeElement shape)
        {
            if (shape.IsNestedChild)
            {
                using (Transaction t = this.Store.TransactionManager.BeginTransaction("Send To Back"))
                {
                    // Make the current shape the first in the list.
                    shape.ParentShape.NestedChildShapes.Move(shape, 0);
                    // Update the ZOrder of the shapes to reflect the change.
                    shape.Diagram.NeedsRenumber = true;
                    // Make sure the shape is redrawn:
                    shape.Invalidate();
                    t.Commit();
                }
            }

        }

        public void BringToFront(ShapeElement shape)
        {
            if (shape.IsNestedChild)
            {
                using (Transaction t = this.Store.TransactionManager.BeginTransaction("Bring To Front"))
                {
                    // Make the current shape the last in the list.
                    shape.ParentShape.NestedChildShapes.Move(shape, shape.ParentShape.NestedChildShapes.Count - 1);
                    // Update the ZOrder of the shapes to reflect the change.
                    shape.Diagram.NeedsRenumber = true;
                    // Make sure the shape is redrawn:
                    shape.Invalidate();
                    t.Commit();
                }
            }
        }

        #endregion


        #region External designers


        public void ConvertToModelCLass(ReferenceModelClass oldReference)
        {
            if (oldReference != null)
            {
                ModelClass newReference = null;
                using (Transaction transaction =
                                this.Store.TransactionManager.BeginTransaction("Converting Reference."))
                {
                    //Create new reference
                    newReference = new ModelClass(this.Partition);
                    newReference.CopyInstanceFrom(oldReference);
                    this.Types.Add(newReference);

                    foreach (var link in Association.GetLinksToSourceModelClasses(oldReference).ToList())
                    {
                        link.TargetModelClass = newReference;
                    }
                    foreach (var link in Association.GetLinksToTargetModelClasses(oldReference).ToList())
                    {
                        link.SourceModelClass = newReference;
                    }
                    foreach (var link in MultipleAssociationOrigin.GetLinksToMultipleAssociations(oldReference).ToList())
                    {
                        link.OriginType = newReference;
                    }
                    var linkMA = MultipleAssociationTarget.GetLinkToMultipleAssociation(oldReference);
                    if (linkMA != null)
                        linkMA.TargetType = newReference;
                    var inherit = Generalization.GetLinkToSuperclass(oldReference);
                    if (inherit != null)
                        inherit.Subclass = newReference;
                    foreach (var link in Generalization.GetLinksToSubclasses(oldReference).ToList())
                    {
                        link.Superclass = newReference;
                    }
                    foreach (var attr in oldReference.Attributes.ToList())
                    {
                        attr.ModelClass = newReference;
                    }
                    foreach (var idx in oldReference.ModelIndexes.ToList())
                    {
                        idx.ModelClass = newReference;
                    }
                    foreach (var op in oldReference.Operations.ToList())
                    {
                        op.ModelClass = newReference;
                    }

                    transaction.Commit();
                }

                if (newReference != null)
                {
                    using (Transaction transaction =
                                               this.Store.TransactionManager.BeginTransaction("Deleting Element."))
                    {
                        var oldShape = oldReference.GetPresentation<ReferenceModelClassShape>();
                        var newShape = newReference.GetPresentation<ClassShape>();
                        if (newShape != null && oldShape != null)
                        {
                            newShape.Location = oldShape.Location;
                        }

                        oldReference.Delete();

                        transaction.Commit();
                    }
                }
            }
        }

        public void AddExternalReferences(ModelBusAdapterManager manager, ModelBusReference modelReference, List<string> externalClasses)
        {
            List<ModelClass> deletedElements = new List<ModelClass>();
            List<ReferenceModelClass> pendantElements = new List<ReferenceModelClass>();
            ModelClass existentElement;
            bool hasChanges = false;
            try
            {
                this.IsLocked = true;
                using (ModelBusAdapter modelAdapter = manager.CreateAdapter(modelReference))
                {
                    if (modelAdapter != null)
                    {
                        BusinessDataModelDesignerRoot modelRoot = modelAdapter.GetPropertyValue("ModelRoot") as BusinessDataModelDesignerRoot;
                        if (modelRoot != null)
                        {
                            using (Transaction transaction =
                                this.Store.TransactionManager.BeginTransaction("Adding External References."))
                            {
                                foreach (string className in externalClasses)
                                {
                                    var element = modelRoot.Types.Where(e => e is ModelClass && e.Name == className).FirstOrDefault() as ModelClass;
                                    if (element != null)
                                    {
                                        var classRef = modelAdapter.GetElementReference(element);
                                        existentElement = this.Types.FirstOrDefault(e => e.Name == className) as ModelClass;
                                        if (existentElement != null && existentElement is ReferenceModelClass)
                                        {
                                            //Update Broken Reference
                                            if (((ReferenceModelClass)existentElement).HasReferenceError || classRef.GetReferenceFile() != ((ReferenceModelClass)existentElement).ModelClassReference.GetReferenceFile())
                                            {
                                                hasChanges = true;
                                                ((ReferenceModelClass)existentElement).SetLocks(Locks.None);
                                                ((ReferenceModelClass)existentElement).ModelClassReference = classRef;
                                                ((ReferenceModelClass)existentElement).ReleaseError();
                                                ((ReferenceModelClass)existentElement).SetLocks(Locks.Properties);
                                            }
                                        }
                                        else
                                        {
                                            hasChanges = true;
                                            //Create new reference
                                            ReferenceModelClass reference = new ReferenceModelClass(this.Partition);
                                            reference.Name = className;
                                            reference.ModelClassReference = classRef;
                                            this.Types.Add(reference);

                                            //Class already exists as normal, therefore change all links and delete the old class.
                                            if (existentElement != null && !(existentElement is ReferenceModelClass))
                                            {
                                                foreach (var link in Association.GetLinksToSourceModelClasses(existentElement).ToList())
                                                {
                                                    link.TargetModelClass = reference;
                                                }
                                                foreach (var link in Association.GetLinksToTargetModelClasses(existentElement).ToList())
                                                {
                                                    link.SourceModelClass = reference;
                                                }
                                                foreach (var link in MultipleAssociationOrigin.GetLinksToMultipleAssociations(existentElement).ToList())
                                                {
                                                    link.OriginType = reference;
                                                }
                                                var linkMA = MultipleAssociationTarget.GetLinkToMultipleAssociation(existentElement);
                                                if (linkMA != null)
                                                    linkMA.TargetType = reference;
                                                var inherit = Generalization.GetLinkToSuperclass(existentElement);
                                                if (inherit != null)
                                                    inherit.Subclass = reference;
                                                foreach (var link in Generalization.GetLinksToSubclasses(existentElement).ToList())
                                                {
                                                    link.Superclass = reference;
                                                }
                                                foreach (var attr in existentElement.Attributes.ToList())
                                                {
                                                    attr.ModelClass = reference;
                                                }
                                                foreach (var idx in existentElement.ModelIndexes.ToList())
                                                {
                                                    idx.ModelClass = reference;
                                                }
                                                foreach (var op in existentElement.Operations.ToList())
                                                {
                                                    op.ModelClass = reference;
                                                }
                                                //Mark old class for deleting
                                                deletedElements.Add(existentElement);
                                                //Add pendant element
                                                pendantElements.Add(reference);
                                            }
                                        }
                                    }

                                }
                                transaction.Commit();
                            }


                            //Deleting old elements and setting location of the new element.
                            if (deletedElements.Count > 0)
                            {
                                using (Transaction transaction =
                                           this.Store.TransactionManager.BeginTransaction("Deleting Elements."))
                                {
                                    foreach (var element in deletedElements)
                                    {
                                        ReferenceModelClass relatedReference = this.Types.FirstOrDefault(e => e.Name == element.Name && e is ReferenceModelClass && e != element) as ReferenceModelClass;
                                        if (relatedReference != null)
                                        {
                                            var rShape = relatedReference.GetPresentation<ReferenceModelClassShape>();
                                            var dShape = element.GetPresentation<ClassShape>();
                                            if (dShape != null && rShape != null)
                                            {
                                                rShape.Location = dShape.Location;
                                            }
                                        }

                                        element.Delete();
                                    }
                                    transaction.Commit();
                                }
                            }

                            this.IsLocked = false;

                            //Update links for pendant elements.
                            if (pendantElements.Count > 0)
                            {
                                using (Transaction transaction = this.Store.TransactionManager.BeginTransaction("Update links for pendant elements."))
                                {
                                    foreach (var element in pendantElements)
                                    {
                                        element.UpdateLinksReference(modelRoot.Types.Where(e => e is ModelClass && e.Name == element.Name).FirstOrDefault() as ModelClass);
                                        //Adjust Source References
                                        foreach (var link in Association.GetLinksToSourceModelClasses(element).Where(e => e.SourceModelClass is ReferenceModelClass).ToList())
                                        {
                                            ((ReferenceModelClass)link.SourceModelClass).UpdateLinksReference(modelRoot.Types.Where(e => e is ModelClass && e.Name == ((ReferenceModelClass)link.SourceModelClass).Name).FirstOrDefault() as ModelClass);
                                        }
                                    }
                                    transaction.Commit();
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                this.IsLocked = false;
            }

            if (hasChanges)
            {
                this.UpdateAllReferenceClassRelations(modelReference.ModelDisplayName);
                this.Repaint();
            }
        }

        public void Repaint()
        {
            var diagram = this.GetPresentation<BusinessDataModelDesignerDiagram>();
            if (diagram != null)
                diagram.Invalidate(true);
        }

        public string GetConfigConnectionString()
        {
            //DataContextName, ProviderType, ConnectionString
            var contextInfo = this.GetSharedInfo();
            return contextInfo["ConnectionString"];
        }

        public string GetDataContextName()
        {
            //DataContextName, ProviderType, ConnectionString
            var contextInfo = this.GetSharedInfo();
            return contextInfo["DataContextName"];
        }

        public Provider getConfigProvider()
        {
            var contextInfo = this.GetSharedInfo();
            return (Provider)Enum.Parse(typeof(Provider), contextInfo["ProviderType"]);

        }
        #endregion


        private ModelBusReference GetModelBusReference(string modelName)
        {
            return this.Types.Where(e => e is ReferenceModelClass && ((ReferenceModelClass)e).ModelClassReference != null && ((ReferenceModelClass)e).ModelClassReference.ModelDisplayName == modelName).Select(e => ((ReferenceModelClass)e).ModelClassReference).FirstOrDefault();
        }

        private bool HasReferencesWithError()
        {
            bool hasError = false, hasChanges = false;

            using (Transaction transaction =
                                this.Store.TransactionManager.BeginTransaction("Checking External References."))
            {
                foreach (var type in this.Types.Where(e => e is ReferenceModelClass && ((ReferenceModelClass)e).ModelClassReference != null && !((ReferenceModelClass)e).ModelClassReference.ModelDisplayName.IsNullOrEmpty()).Select(e => (ReferenceModelClass)e))
                {
                    var reference = type.GetInstanceReference<ModelClass>(type.ModelClassReference, false);

                    //Check and adjust the class name
                    if (reference != null && type.Name != reference.Name)
                    {
                        hasChanges = true;
                        type.SetLocks(Locks.None);
                        type.Name = reference.Name;
                        type.SetLocks(Locks.Properties);
                    }

                    //Check error
                    if (reference == null)
                    {
                        hasError = true;
                        if (!type.HasReferenceError)
                        {
                            hasChanges = true;
                            type.AlertError();
                        }
                    }
                    else if (type.HasReferenceError)
                    {
                        hasChanges = true;
                        type.ReleaseError();
                    }
                }

                if (hasChanges)
                    transaction.Commit();
                else
                    transaction.Rollback();
            }

            if (hasError)
                MessageBox.Show("Fail when loading external references. Try to add them again from the new location for correcting the problem.", "External Reference Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            return hasError;
        }

        public void UpdateAllReferenceClassRelations(string modelName = "")
        {
            if (!this.Types.Any(e => e is ReferenceModelClass && ((ReferenceModelClass)e).ModelClassReference != null && !((ReferenceModelClass)e).ModelClassReference.ModelDisplayName.IsNullOrEmpty()))
                return;

            bool hasReferenceError = this.HasReferencesWithError();
            IModelBus modelBus = this.GetModelBus();
            List<string> modelNames = new List<string>();
            if (modelName.IsNullOrEmpty())
            {
                modelNames = this.Types.Where(e => e is ReferenceModelClass && ((ReferenceModelClass)e).ModelClassReference != null && !((ReferenceModelClass)e).ModelClassReference.ModelDisplayName.IsNullOrEmpty()).Select(e => ((ReferenceModelClass)e).ModelClassReference.ModelDisplayName).Distinct().ToList();
            }
            else modelNames.Add(modelName);

            if (modelNames.Count > 0 && modelBus != null)
            {
                ModelBusAdapterManager manager = BusinessDataModelDesignerRoot.GetModelBusManager<ModelBusAdapterManager>(modelBus);
                //Update All Links
                using (Transaction transaction =
                                               this.Store.TransactionManager.BeginTransaction("Update External References."))
                {
                    bool hasChanges = false;
                    var allReferences = this.Types.Where(e => e is ReferenceModelClass).Select(e => (ReferenceModelClass)e).ToList();

                    //Clear locks
                    foreach (var reference in allReferences)
                    {
                        reference.SetStructuralLocks(DslModeling.Immutability.Locks.None);
                    }

                    foreach (string model in modelNames)
                    {
                        ModelBusReference modelReference = GetModelBusReference(model);

                        if (modelReference == null)
                            continue;

                        try
                        {
                            using (ModelBusAdapter modelAdapter = manager.CreateAdapter(modelReference))
                            {
                                if (modelAdapter != null)
                                {
                                    BusinessDataModelDesignerRoot modelRoot = modelAdapter.GetPropertyValue("ModelRoot") as BusinessDataModelDesignerRoot;
                                    if (modelRoot != null)
                                    {
                                        var references = this.Types.Where(e => e is ReferenceModelClass && !((ReferenceModelClass)e).HasReferenceError && ((ReferenceModelClass)e).ModelClassReference != null && ((ReferenceModelClass)e).ModelClassReference.ModelDisplayName == model).Select(e => (ReferenceModelClass)e).ToList();

                                        foreach (var reference in references)
                                        {
                                            if (reference.UpdateClassReference(modelRoot.Types.FirstOrDefault(e => e is ModelClass && e.Name == reference.Name) as ModelClass))
                                                hasChanges = true;
                                        }

                                        foreach (var reference in references)
                                        {
                                            if (reference.UpdateLinksReference(modelRoot.Types.FirstOrDefault(e => e is ModelClass && e.Name == reference.Name) as ModelClass))
                                                hasChanges = true;
                                        }

                                        //Delete UnUsed Multiassociations
                                        var multAssociations = this.Types.Where(e => e is MultipleAssociation && !((MultipleAssociation)e).IdReference.IsNullOrEmpty() && (((MultipleAssociation)e).OriginTypes.Count == 0 || ((MultipleAssociation)e).TargetType == null)).Select(e => (MultipleAssociation)e).ToList();
                                        foreach (var multAssociation in multAssociations)
                                        {
                                            multAssociation.Delete();
                                            hasChanges = true;
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception exep)
                        {
                            if (!hasReferenceError) //Error already treated
                                MessageBox.Show(exep.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }

                    //Restore locks
                    foreach (var reference in allReferences)
                    {
                        reference.SetStructuralLocks(DslModeling.Immutability.Locks.Properties);
                    }

                    if (hasChanges)
                        transaction.Commit();
                    else
                        transaction.Rollback();
                }
            }
        }

        /// <summary>
        /// Remove a project item by one standard expression.
        /// </summary>
        /// <param name="items"></param>
        /// <param name="endsWith"></param>
        public static void RemoveProjectItems(ProjectItems items, string endsWith)
        {
            List<ProjectItem> selection = new List<ProjectItem>();

            foreach (ProjectItem item in items)
            {
                if (item.Name.ToLower().EndsWith(endsWith.ToLower()))
                    item.Remove();
            }
        }

        public ProjectItem GetDiagramProjectItem()
        {
            return GetProjectItemByName(this.GetLxdmProject(), this.DocumentName);
        }

        public Project GetLxdmProject()
        {
            return GetLxdmProject((EnvDTE.DTE)null);
        }

        public Project GetLxdmProject(EnvDTE.DTE vs)
        {
            Project current = null;
            if (vs == null)
                vs = GetDTE();
            if (vs != null)
            {
                foreach (EnvDTE.Project project in vs.Solution.Projects)
                {
                    current = GetLxdmProject(project);
                    if (!current.IsNull())
                        break;
                }
            }

            return current;
        }

        private static bool IsLxdmProject(Project project)
        {
            return IsLxdmProject(project.ProjectItems);
        }

        private static bool IsLxdmProject(ProjectItems projectItems)
        {
            bool result = false;

            if (!projectItems.IsNullOrEmpty() && projectItems.Count > 0)
            {
                foreach (ProjectItem item in projectItems)
                {
                    if (Path.GetExtension(item.Name).ToLower() == ".lxdm")
                    {
                        result = true;
                        break;
                    }

                    if (IsLxdmProject(item.ProjectItems))
                    {
                        result = true;
                        break;
                    }
                }
            }

            return result;
        }


        private Project GetLxdmProject(Project project)
        {
            Project current = null;

            if (BusinessDataModelDesignerRoot.IsLxdmProject(project, System.IO.Path.Combine(this.DocumentPath, this.DocumentName)))
            {
                current = project;
            }
            else
            {
                if (project.ProjectItems != null && project.ProjectItems.Count > 0)
                {
                    foreach (ProjectItem projItem in project.ProjectItems)
                    {
                        if (projItem.SubProject != null)
                        {
                            current = GetLxdmProject(projItem.SubProject);
                            if (current != null)
                                break;
                        }
                    }
                }
            }

            return current;
        }

        private static bool IsLxdmProject(Project project, string itemPath)
        {
            return IsLxdmProject(project.ProjectItems, itemPath);
        }

        private static bool IsLxdmProject(ProjectItems projectItems, string itemPath)
        {
            bool result = false;
            string fullName;

            //Convert Relaite Path to Absolute Path
            if (File.Exists(itemPath))
                itemPath = Path.GetFullPath(itemPath);

            if (!projectItems.IsNullOrEmpty() && projectItems.Count > 0)
            {
                foreach (ProjectItem item in projectItems)
                {
                    if (item.Properties == null)
                        continue;

                    fullName = item.Properties.Item("FullPath").Value.ToString();
                    if (!fullName.IsNullOrEmpty())
                    {
                        if ((fullName.ToLower() == itemPath.ToLower()) || (item.Name.ToLower() == itemPath.ToLower()))
                        {
                            result = true;
                            break;
                        }
                    }

                    if (IsLxdmProject(item.ProjectItems, itemPath))
                    {
                        result = true;
                        break;
                    }
                }
            }

            return result;
        }

        public string GetSqlMigratorChanges(List<BusinessDataModelDesignerRoot> models, string indent, Provider defaultProvider, string templateFile)
        {
            ValidSqlMigratorFKs(models, Path.Combine(Path.GetDirectoryName(templateFile), @"..\"));

            Linx.Tools.CodeBuilder builder = new Tools.CodeBuilder(indent);
            bool addHeader = true;
            List<string> objects = new List<string>();
            string objectName;

            builder.AddLine("_tableMigrator = new SqlTableMigrator();");
            foreach (var model in models)
            {
                foreach (ModelClass modelClass in model.Types.Where(e => e is ModelClass && ((ModelClass)e).Kind == ClassKind.Table && !((ModelClass)e).InStudy))
                {
                    foreach (var index in modelClass.ModelIndexes)
                    {
                        if (addHeader)
                        {
                            builder.AddLine("CreateIndexOperation createIndexOperation;");
                            builder.AddLine("//Add Indexes");
                            addHeader = false;
                        }

                        objectName = index.Name;
                        if (!objects.Contains(objectName))
                        {
                            objects.Add(objectName);
                            builder.AddLine("createIndexOperation = new CreateIndexOperation(" + (index.IsClustered ? "new { IsClustered = true }" : String.Empty) + ") { Table = \"" + modelClass.GetTableName() + "\", IsUnique = " + index.IsUnique.ToString().ToLower() + ", Name = \"" + objectName + "\" };");
                            foreach (string column in index.Properties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(e => e.Trim()))
                            {
                                builder.AddLine("createIndexOperation.Columns.Add(\"" + (column.ToUpper().Right(4) == " ASC" ? column.Left(column.Length - 4) : (column.ToUpper().Right(5) == " DESC" ? column.Left(column.Length - 5) + (defaultProvider == Provider.SQLServer ? " DESC" : String.Empty) : column)) + "\");");
                            }
                            builder.AddLine("_tableMigrator.Indexes.Add(createIndexOperation);");
                        }
                    }
                }
            }

            objects.Clear();
            addHeader = true;
            foreach (var model in models)
            {
                foreach (ModelClass modelClass in model.Types.Where(e => e is ModelClass && ((ModelClass)e).Kind == ClassKind.Table && !((ModelClass)e).InStudy && (!((ModelClass)e).PrimaryKeyConstraintName.IsNullOrEmpty() || (defaultProvider == Provider.SQLServer && !((ModelClass)e).IsClustered))))
                {
                    if (addHeader)
                    {
                        builder.AddLine("//Add Primary Keys");
                        addHeader = false;
                    }

                    objectName = modelClass.GetTableName();
                    if (!objects.Contains(objectName))
                    {
                        objects.Add(objectName);
                        builder.AddLine("_tableMigrator.PrimaryKeys[\"" + objectName + "\"] = \"" + (modelClass.PrimaryKeyConstraintName.IsNullOrEmpty() ? "XPK_" + objectName : modelClass.PrimaryKeyConstraintName) + (defaultProvider == Provider.SQLServer && !modelClass.IsClustered ? "__NC__" : String.Empty) + "\";");
                    }
                }
            }

            objects.Clear();
            addHeader = true;
            foreach (var model in models)
            {
                foreach (ModelClass modelClass in model.Types.Where(e => e is ModelClass && ((ModelClass)e).Kind == ClassKind.DatabaseView))
                {
                    if (addHeader)
                    {
                        builder.AddLine("//Add Views");
                        addHeader = false;
                    }

                    objectName = modelClass.GetTableName();
                    if (!objects.Contains(objectName))
                    {
                        objects.Add(objectName);
                        builder.AddLine("_tableMigrator.Views.Add(\"" + objectName + "\");");
                    }
                }
            }

            objects.Clear();
            addHeader = true;
            foreach (var model in models)
            {
                foreach (ModelClass modelClass in model.Types.Where(e => e is ModelClass && ((ModelClass)e).Kind == ClassKind.Table && !((ModelClass)e).InStudy))
                {
                    foreach (var attr in modelClass.Attributes.Where(e => !e.SqlDefault.IsNullOrEmpty()))
                    {
                        if (addHeader)
                        {
                            builder.AddLine("//Add Defaults");
                            addHeader = false;
                        }

                        objectName = modelClass.GetTableName() + "." + attr.GetColumnName();
                        if (!objects.Contains(objectName))
                        {
                            objects.Add(objectName);
                            builder.AddLine("_tableMigrator.Defauls.Add(\"" + objectName + "\", \"" + attr.SqlDefault + "\");");
                        }
                    }
                }
            }

            objects.Clear();
            addHeader = true;
            foreach (var model in models)
            {
                foreach (ModelClass modelClass in model.Types.Where(e => e is ModelClass && ((ModelClass)e).Kind == ClassKind.Table && !((ModelClass)e).InStudy))
                {
                    foreach (var attr in modelClass.Attributes.Where(e => e.IsNullable && !e.ForeignKey.IsNullOrEmpty()))
                    {
                        if (addHeader)
                        {
                            builder.AddLine("//Add Nullables");
                            addHeader = false;
                        }

                        objectName = modelClass.GetTableName() + "." + attr.GetColumnName();
                        if (!objects.Contains(objectName))
                        {
                            objects.Add(objectName);
                            builder.AddLine("_tableMigrator.Nullables.Add(\"" + objectName + "\");");
                        }
                    }
                }
            }

            objects.Clear();
            addHeader = true;
            //Default Associations
            foreach (var model in models)
            {
                foreach (ModelClass modelClass in model.Types.Where(e => e is ModelClass && ((ModelClass)e).Kind == ClassKind.Table && !((ModelClass)e).InStudy))
                {
                    foreach (var link in Association.GetLinksToSourceModelClasses(modelClass))
                    {
                        if (link.SourceModelClass.InStudy || link.TargetModelClass.InStudy)
                            continue;

                        if (addHeader)
                        {
                            builder.AddLine("//Add Foreign Keys");
                            addHeader = false;
                        }

                        objectName = link.SourceModelClass.GetTableName() + "." +
                        String.Join(".", link.SourceModelClass.GetTopSuperClass().Attributes.Where(e => e.IsPrimaryKey && !e.IsNullable).Select(e => e.GetColumnName()).OrderBy(e => e).ToArray()) + "." +
                        modelClass.GetTableName() + "." +
                        String.Join(".", link.GetTargetColumns());
                        if (!objects.Contains(objectName))
                        {
                            objects.Add(objectName);
                            builder.AddLine("_tableMigrator.Fks.Add(\"" + objectName + "\", \"" + link.GetFkName(false, this.IsDynamicForeignKeyNames()) + "," + link.WillCascadeOnDelete.ToString().ToLower() + "\");");
                        }
                    }
                }

                //Multiple Associations
                foreach (ModelClass modelClass in model.Types.Where(e => e is ModelClass && ((ModelClass)e).Kind == ClassKind.Table && !((ModelClass)e).InStudy))
                {
                    var lma = MultipleAssociationTarget.GetLinkToMultipleAssociation(modelClass);
                    if (lma != null && lma.MultipleAssociation != null)
                    {
                        foreach (var link in MultipleAssociationOrigin.GetLinksToOriginTypes(lma.MultipleAssociation))
                        {
                            if (link.OriginType.InStudy || link.MultipleAssociation.TargetType.InStudy)
                                continue;

                            if (addHeader)
                            {
                                builder.AddLine("//Add Foreign Keys");
                                addHeader = false;
                            }

                            objectName = link.OriginType.GetTableName() + "." +
                            String.Join(".", link.OriginType.GetTopSuperClass().Attributes.Where(e => e.IsPrimaryKey && !e.IsNullable).Select(e => e.GetColumnName()).OrderBy(e => e).ToArray()) + "." +
                            modelClass.GetTableName() + "." +
                            String.Join(".", link.GetTargetColumns());
                            if (!objects.Contains(objectName))
                            {
                                objects.Add(objectName);
                                builder.AddLine("_tableMigrator.Fks.Add(\"" + objectName + "\", \"" + link.GetFkName(false, this.IsDynamicForeignKeyNames()) + "," + link.WillCascadeOnDelete.ToString().ToLower() + "\");");
                            }
                        }
                    }
                }

                //SuperClass Associations
                foreach (ModelClass modelClass in model.Types.Where(e => e is ModelClass && ((ModelClass)e).Kind == ClassKind.Table && !((ModelClass)e).InStudy && ((ModelClass)e).Superclass != null))
                {
                    //Get top class
                    var superClass = modelClass.Superclass;
                    while (superClass.Superclass != null)
                    {
                        superClass = superClass.Superclass;
                    }

                    if (superClass.InStudy)
                        continue;

                    if (addHeader)
                    {
                        builder.AddLine("//Add Foreign Keys");
                        addHeader = false;
                    }

                    string columns = String.Join(".", superClass.Attributes.Where(e => e.IsPrimaryKey && !e.IsNullable).Select(e => e.GetColumnName()).OrderBy(e => e).ToArray());
                    objectName = modelClass.Superclass.GetTableName() + "." +
                    columns + "." +
                    modelClass.GetTableName() + "." +
                    columns;
                    if (!objects.Contains(objectName))
                    {
                        objects.Add(objectName);
                        builder.AddLine("_tableMigrator.Fks.Add(\"" + objectName + "\", \"" + modelClass.GetFkBaseName() + ",true\");");
                    }
                }
            }

            return builder.GetBody();
        }

        public string GetModelInfo()
        {
            return (this.DocumentPath).Right("\\") + "\\" + this.DocumentName;
        }

        public void AdjustStructuralInfo()
        {
            this.AdjustNamespace();
        }

        public void AdjustDocumentInfo(string modelFileName)
        {
            if (String.IsNullOrEmpty(this.DocumentPath) || this.DocumentPath != System.IO.Path.GetDirectoryName(modelFileName) || String.IsNullOrEmpty(this.DocumentName) || this.DocumentName != System.IO.Path.GetFileName(modelFileName))
            {
                using (Transaction transaction =
                            this.Store.TransactionManager.BeginTransaction("Changing DocumentInfo."))
                {
                    this.DocumentPath = System.IO.Path.GetDirectoryName(modelFileName);
                    this.DocumentName = System.IO.Path.GetFileName(modelFileName);
                    transaction.Commit();
                }
            }
        }

        public string GetNamespace(Project prj = null)
        {
            if (prj == null)
                prj = this.GetLxdmProject();

            if (prj == null)
                return "Linx";
            else
            {
                var item = prj.Properties.Item("DefaultNamespace");
                return (item == null ? "" : (string)item.Value);
            }
        }

        public string GetAssemblyPath(Project prj = null)
        {
            if (prj == null)
                prj = this.GetLxdmProject();

            string fullPath = prj.Properties.Item("FullPath").Value.ToString();
            string outputPath = prj.ConfigurationManager.ActiveConfiguration.Properties.Item("OutputPath").Value.ToString();
            string outputDir = Path.Combine(fullPath, outputPath);
            string outputFileName = prj.Properties.Item("OutputFileName").Value.ToString();
            string assemblyPath = Path.Combine(outputDir, outputFileName);
            return assemblyPath;
        }

        public void AdjustNamespace()
        {
            //Adjust name space.
            string nameSpace = this.GetNamespace();
            if (this.TargetNamespace != nameSpace)
            {
                using (Transaction transaction =
                           this.Store.TransactionManager.BeginTransaction("Changing StructuralInfo."))
                {
                    this.TargetNamespace = nameSpace;
                    transaction.Commit();
                }
            }
        }

        #region DTE Reference
        public EnvDTE.DTE DTEReference { get; set; }
        public EnvDTE.DTE GetDTE()
        {
            return DTEReference;
        }
        #endregion

        public string GenerateStoreScriptsCode(string indent, List<BusinessDataModelDesignerRoot> models)
        {
            Tools.CodeBuilder builder = new Tools.CodeBuilder(indent);
            string parameters;
            List<StoreScript> scripts = new List<StoreScript>();
            foreach (var model in models)
            {
                scripts.AddRange(model.StoreScripts.Where(e => e.StoreQueries.Count() > 0).ToList());
            }
            if (scripts.Count > 0)
            {

                foreach (var script in scripts.OrderBy(e => e.Name))
                {
                    builder.AddLine("");
                    builder.AddLine("#region Store Scripts: " + script.Name);

                    foreach (var storeQuery in script.StoreQueries)
                    {
                        parameters = String.Empty;
                        foreach (string p in storeQuery.Parameters.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            parameters += ", " + (p.Contains("=") ? p.Left("=") : p).Trim().Right(" ");
                        }
                        builder.AddLine("");
                        builder.AddLine("public IEnumerable<" + storeQuery.GenericType + "> " + storeQuery.Name + "(" + storeQuery.Parameters.Replace("#", ", ") + ")");
                        builder.AddLine("{");
                        builder.AddLine("   return this.Database.SqlQuery<" + storeQuery.GenericType + ">(\"" + storeQuery.Command + "\"" + parameters + ");");
                        builder.AddLine("}");
                    }

                    builder.AddLine("");
                    builder.AddLine("#endregion");
                }

            }

            return builder.GetBody();
        }

        public Project GetProjectByName(string projectName)
        {
            return GetProjectByName(this.GetDTE(), projectName);
        }

        public static Project GetProjectByName(EnvDTE.DTE vs, string projectName)
        {
            Project current = null;
            if (vs != null)
            {
                foreach (EnvDTE.Project project in vs.Solution.Projects)
                {
                    current = GetProjectByName(project, projectName);
                    if (current != null)
                        break;
                }
            }

            return current;
        }

        private static Project GetProjectByName(Project project, string projectName)
        {
            Project current = null;

            if (project.Name == projectName || project.UniqueName == projectName)
                current = project;
            else
            {
                if (project.ProjectItems != null && project.ProjectItems.Count > 0)
                {
                    foreach (ProjectItem projItem in project.ProjectItems)
                    {
                        if (projItem.SubProject != null)
                        {
                            current = GetProjectByName(projItem.SubProject, projectName);
                            if (current != null)
                                break;
                        }

                    }
                }
            }

            return current;
        }


        public static IList<Project> GetAllProjects(DTE dte)
        {
            Projects projects = dte.Solution.Projects;
            List<Project> list = new List<Project>();
            var item = projects.GetEnumerator();
            while (item.MoveNext())
            {
                var project = item.Current as Project;
                if (project == null)
                {
                    continue;
                }

                if (project.Kind == "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}")
                {
                    list.AddRange(GetSolutionFolderProjects(project));
                }
                else
                {
                    list.Add(project);
                }
            }

            return list;
        }

        private static IEnumerable<Project> GetSolutionFolderProjects(Project solutionFolder)
        {
            List<Project> list = new List<Project>();
            for (var i = 1; i <= solutionFolder.ProjectItems.Count; i++)
            {
                var subProject = solutionFolder.ProjectItems.Item(i).SubProject;
                if (subProject == null)
                {
                    continue;
                }

                // If this is another solution folder, do a recursive call, otherwise add
                if (subProject.Kind == "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}")
                {
                    list.AddRange(GetSolutionFolderProjects(subProject));
                }
                else
                {
                    list.Add(subProject);
                }
            }

            return list;
        }



        public static ProjectItem GetProjectItemByName(Project project, string itemName)
        {
            return GetProjectItemByName(project.ProjectItems, itemName);
        }

        public static ProjectItem GetProjectItemByName(ProjectItems items, string itemName)
        {
            ProjectItem result = null;
            if (items != null)
            {
                foreach (ProjectItem item in items)
                {
                    if (item.Name.ToLower() == itemName.ToLower())
                    {
                        result = item;
                        break;
                    }

                    result = GetProjectItemByName(item.ProjectItems, itemName);
                    if (result != null)
                        break;
                }
            }
            return result;
        }

        public string GetDirectorySourcePart()
        {
            string dirPart = null;
            if (this.DTEReference != null)
            {
                var envs = GetEnvironments();
                dirPart = envs.FirstOrDefault(e => this.DTEReference.Solution.FullName.ToLower().Contains("\\" + e.Trim().ToLower() + "\\"));
            }
            return (dirPart.IsNullOrEmpty() ? "Dev" : dirPart);
        }

        public string[] GetEnvironments()
        {
            if (this.DTEReference == null)
                return null;

            string[] result = new string[] { };
            string worksapaceMapedpath = "";// ("Linx Framework").GetWorkspaceMappedPath();

            if (worksapaceMapedpath.IsNullOrEmpty())
                worksapaceMapedpath = GetLocalFrameworkPath();

            if (!worksapaceMapedpath.IsNullOrEmpty())
            {
                string endInfoFile = Path.Combine(worksapaceMapedpath, "Linx Framework\\Environments.xml");
                if (File.Exists(endInfoFile))
                {
                    try
                    {
                        System.Xml.Linq.XElement xElementFound = System.Xml.Linq.XElement.Load(endInfoFile);
                        result = (xElementFound.IsNull() ? String.Empty : xElementFound.Value.Replace("\n", String.Empty).Replace("\t", String.Empty)).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(String.Format("Fail reading the file {0}.", endInfoFile) + exp.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }

            return result;
        }

        private string GetLocalFrameworkPath()
        {
            if (!this.DocumentPath.IsNullOrEmpty())
            {
                string localMapPath = Path.Combine(Path.GetPathRoot(this.DocumentPath), "Linx Workspace");
                if (Directory.Exists(localMapPath))
                    return localMapPath;
            }

            return "";
        }

        public string GetDirectoryInfo(string directoryName)
        {
            if (this.DTEReference == null)
                return null;

            string dirPart = GetDirectorySourcePart();
            string result = String.Empty;
            string worksapaceMapedpath = "";// ("Linx Framework\\" + dirPart + "\\Binary").GetWorkspaceMappedPath();

            if (worksapaceMapedpath.IsNullOrEmpty())
                worksapaceMapedpath = GetLocalFrameworkPath();

            if (!worksapaceMapedpath.IsNullOrEmpty())
            {
                string dirInfoFile = Path.Combine(worksapaceMapedpath, "Linx Framework\\" + dirPart + "\\Binary\\Library\\Common\\Linx\\Information\\EntityAdapterDirectoryInfo.xml");
                if (File.Exists(dirInfoFile))
                {
                    try
                    {
                        System.Xml.Linq.XElement xElement = System.Xml.Linq.XElement.Load(dirInfoFile);
                        if (!xElement.IsNull())
                        {
                            System.Xml.Linq.XElement xElementFound = xElement.Elements().Where(e => e.Name == directoryName).FirstOrDefault();
                            result = (xElementFound.IsNull() ? String.Empty : xElementFound.Value.Replace("\n", String.Empty).Replace("\t", String.Empty));
                        }
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(String.Format("Fail reading the file {0}.", dirInfoFile) + exp.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }

            if (result.IsNullOrEmpty())
                MessageBox.Show(String.Format("The DirectoryInfo [{0}] is not found in the environment {1}!", directoryName, dirPart), "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            return result;
        }

        public string GetFullPath(string directoryName)
        {
            string dirtLib = this.GetDirectoryInfo(directoryName);
            if (!dirtLib.IsNullOrEmpty())
                return dirtLib.Trim();
            else
                return "";
        }

        #region Adjust Version

        public void UpdateVersion(Project project)
        {
            var properties = GetProjectItemByName(project, "Properties");
            if (properties != null)
            {
                var itemAssemblyInfo = GetProjectItemByName(properties.ProjectItems, "AssemblyInfo.cs");
                if (itemAssemblyInfo != null)
                {
                    string body = this.GetAssemblyInfoContent(project);
                    string filePath = itemAssemblyInfo.Properties.Item("FullPath").Value.ToString();
                    if (File.ReadAllText(filePath) != body)
                    {
                        File.WriteAllText(filePath, body);
                    }
                }

                string assemblyShared = this.GetAsssemblyShared();
                if (!assemblyShared.IsNullOrEmpty())
                {
                    var itemAssemblyShared = GetProjectItemByName(properties.ProjectItems, "AssemblyInfoShared.cs");
                    if (itemAssemblyShared != null && Path.GetDirectoryName(assemblyShared).ToLower() != Path.GetDirectoryName(itemAssemblyShared.Properties.Item("FullPath").Value.ToString()).ToLower())
                    {
                        itemAssemblyShared.Remove();
                        itemAssemblyShared = null;
                    }

                    if (itemAssemblyShared == null)
                    {
                        properties.ProjectItems.AddFromFile(assemblyShared);
                    }
                }
            }
        }

        private string GetAssemblyInfoContent(Project proj)
        {
            string assemblyName = proj.Properties.Item("AssemblyName").Value;
            Linx.Tools.CodeBuilder builder = new Linx.Tools.CodeBuilder();

            builder.AddLine("using System.Reflection;");
            builder.AddLine("using System.Runtime.CompilerServices;");
            builder.AddLine("using System.Runtime.InteropServices;");
            builder.AddLine();
            builder.AddLine("// General Information about an assembly is controlled through the following");
            builder.AddLine("// set of attributes. Change these attribute values to modify the information");
            builder.AddLine("// associated with an assembly.");
            builder.AddLine("[assembly: AssemblyTitle(\"" + assemblyName + "\")]");
            builder.AddLine("[assembly: AssemblyDescription(\"\")]");
            builder.AddLine("[assembly: AssemblyProduct(\"" + assemblyName + "\")]");
            builder.AddLine("[assembly: AssemblyTrademark(\"\")]");
            builder.AddLine("[assembly: AssemblyCulture(\"\")]");
            builder.AddLine();
            builder.AddLine("// Setting ComVisible to false makes the types in this assembly not visible");
            builder.AddLine("// to COM components.  If you need to access a type in this assembly from");
            builder.AddLine("// COM, set the ComVisible attribute to true on that type.");
            builder.AddLine("[assembly: ComVisible(false)]");
            builder.AddLine();
            builder.AddLine("// The following GUID is for the ID of the typelib if this project is exposed to COM");
            builder.AddLine("[assembly: Guid(\"" + GetProjectGuid(proj).ToString() + "\")]");

            return builder.GetBody();
        }

        public Guid GetProjectGuid(EnvDTE.Project project)
        {
            Guid projectGuid = Guid.Empty;

            Microsoft.VisualStudio.Shell.Interop.IVsHierarchy hierarchy;

            IServiceProvider serviceProvider = new Microsoft.VisualStudio.Shell.ServiceProvider(project.DTE as Microsoft.VisualStudio.OLE.Interop.IServiceProvider);

            Microsoft.VisualStudio.Shell.Interop.IVsSolution solution = serviceProvider.GetService(typeof(Microsoft.VisualStudio.Shell.Interop.SVsSolution)) as Microsoft.VisualStudio.Shell.Interop.IVsSolution;

            solution.GetProjectOfUniqueName(project.FullName, out hierarchy);

            if (hierarchy != null)
            {
                solution.GetGuidOfProject(hierarchy, out projectGuid);
            }

            return projectGuid;
        }

        public string GetAsssemblyShared()
        {
            if (this.DTEReference == null)
                return null;
            string dirPart = GetDirectorySourcePart();
            string worksapaceMapedpath = "";// ("Linx Framework\\" + dirPart + "\\Binary").GetWorkspaceMappedPath();

            if (worksapaceMapedpath.IsNullOrEmpty())
                worksapaceMapedpath = GetLocalFrameworkPath();

            if (worksapaceMapedpath.IsNullOrEmpty())
                return null;
            else
            {

                string asssemblySharedFile = Path.Combine(worksapaceMapedpath, "Linx Framework\\" + dirPart + "\\Binary\\Library\\Common\\Linx\\AssemblyInfoShared\\AssemblyInfoShared.cs");
                if (File.Exists(asssemblySharedFile))
                {
                    return asssemblySharedFile;
                }
                else
                    return String.Empty;
            }
        }

        #endregion

        public void RemoveReferencesWithoutFile(Project project)
        {
            if (!project.IsNull())
            {
                List<VSLangProj.Reference> references = new List<VSLangProj.Reference>();
                VSLangProj.VSProject vsProject = (VSLangProj.VSProject)project.Object;
                foreach (VSLangProj.Reference reference in vsProject.References)
                {
                    if (reference.Path.IsNullOrEmpty() || !File.Exists(reference.Path))
                        references.Add(reference);
                }

                //Delete inconsistents references
                foreach (VSLangProj.Reference reference in references)
                    reference.Remove();
            }
        }

        public VSLangProj.Reference AddReference(string strAssemblyName, bool copyLocal = false, bool specificVersion = false)
        {
            return this.AddReference(this.GetLxdmProject(), strAssemblyName, copyLocal, specificVersion);
        }


        public VSLangProj.Reference AddReference(Project project, string strAssemblyName, bool copyLocal = false, bool specificVersion = false)
        {
            VSLangProj.Reference reference = null;
            try
            {
                if (!project.IsNull())
                {
                    reference = GetReference(project, strAssemblyName);
                    if (reference == null)
                    {
                        VSLangProj.VSProject vsProject = (VSLangProj.VSProject)project.Object;
                        reference = vsProject.References.Add(strAssemblyName);
                    }

                    if (reference != null)
                    {
                        reference.CopyLocal = copyLocal;
                        if (reference is Reference3)
                            ((Reference3)reference).SpecificVersion = specificVersion;
                    }
                }
            }
            catch (Exception exeption)
            {
                MessageBox.Show("Cannot add the assembly \"" + strAssemblyName + "\" to the project!\r\nDetails:\r\n" + exeption.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return reference;
        }

        public bool ExistsReference(Project project, string strAssemblyName)
        {
            if (!strAssemblyName.IsNullOrEmpty())
            {
                if (strAssemblyName.Right(4).ToLower() != ".dll")
                    strAssemblyName += ".dll";

                VSLangProj.VSProject vsProject = (VSLangProj.VSProject)project.Object;
                foreach (VSLangProj.Reference reference in vsProject.References)
                {
                    if (reference.Name == Path.GetFileNameWithoutExtension(strAssemblyName))
                        return true;
                }
            }

            return false;
        }

        public VSLangProj.Reference GetReference(Project project, string strAssemblyName)
        {
            if (!strAssemblyName.IsNullOrEmpty())
            {
                if (strAssemblyName.Right(4).ToLower() != ".dll")
                    strAssemblyName += ".dll";

                VSLangProj.VSProject vsProject = (VSLangProj.VSProject)project.Object;
                foreach (VSLangProj.Reference reference in vsProject.References)
                {
                    if (reference.Name == Path.GetFileNameWithoutExtension(strAssemblyName))
                        return reference;
                }
            }

            return null;
        }

        public void UpdateLibReferences(Project project, string libFolder, bool copyLocal, bool remove = false, bool specificVersion = false)
        {
            string[] slFiles = GetLibraryFiles(libFolder);
            foreach (string file in slFiles)
            {
                UpdateReference(project, file, remove, copyLocal, specificVersion);
            }
        }

        public void RemoveLibReferences(Project project, string libFolder)
        {
            string[] slFiles = GetLibraryFiles(libFolder);
            foreach (string file in slFiles)
            {
                RemoveReference(project, Path.GetFileNameWithoutExtension(file));
            }
        }

        public void RemoveInteropReferences(Project project, string libFolder)
        {
            string[] slFiles = GetLibraryFiles(libFolder);
            foreach (string file in slFiles)
            {
                if (ExistsProjectItem(project.ProjectItems, Path.GetFileName(file)))
                    project.ProjectItems.Item(Path.GetFileName(file)).Delete();
            }
        }

        public void AddInteropReferences(Project project, string libFolder)
        {
            string[] slFiles = GetLibraryFiles(libFolder);
            foreach (string file in slFiles)
            {
                if (!ExistsProjectItem(project.ProjectItems, Path.GetFileName(file)))
                {
                    var item = project.ProjectItems.AddFromFile(file);
                    {
                        item.Properties.Item("CopyToOutputDirectory").Value = 2; //Copy Always
                        item.Properties.Item("BuildAction").Value = 0; //None
                    }
                }
            }
        }

        public void CheckDefaultProvider(DbProvider provider = null, bool remove = false)
        {
            List<DbProvider> providers = (remove && provider != null ? this.DbProviders.Where(e => e != provider).ToList() : this.DbProviders.ToList());
            if (providers.Count > 0)
            {
                if (!providers.Any(p => p.IsDefault))
                    providers[0].IsDefault = true;
                else
                {
                    if (provider != null && provider.IsDefault)
                    {
                        foreach (var prv in providers.Where(e => e != provider).ToList())
                        {
                            prv.IsDefault = false;
                        }
                    }
                }
            }
        }

        private void UpdateReference(EnvDTE.Project project, string reference, bool remove, bool copyLocal = false, bool specificVersion = false)
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
            if (refItem != null)
            {
                string assemblyPath = System.IO.Path.GetDirectoryName(reference).ToLower();
                if (!assemblyPath.IsNullOrEmpty() && System.IO.Path.GetDirectoryName(refItem.Path).ToLower() != assemblyPath)
                {
                    refItem.Remove();
                    refItem = null;
                }
            }

            if (refItem == null)
                refItem = AddReference(project, reference, copyLocal, specificVersion);
            else
            {
                refItem.CopyLocal = copyLocal;
                if (refItem is Reference3)
                    ((Reference3)refItem).SpecificVersion = specificVersion;
            }
        }

        public string[] GetLibraryFiles(string directoryName)
        {
            string[] libFiles = new string[] { };

            string dirtLib = this.GetDirectoryInfo(directoryName);
            if (!dirtLib.IsNullOrEmpty())
            {
                dirtLib = dirtLib.Trim().Replace("\r", "").Replace("\n", "");
                if (File.Exists(dirtLib))
                    libFiles = new string[] { dirtLib };
                else
                {
                    libFiles = Directory.GetFiles(dirtLib, "*.dll", SearchOption.AllDirectories);
                    if (libFiles.Length == 0)
                        MessageBox.Show(String.Format("Does not exists assemblies in [{0}] directory!", directoryName), "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
                MessageBox.Show(String.Format("The directory [{0}] is not found!", directoryName), "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);


            return libFiles;
        }

        public void RemoveReference(string strAssemblyName)
        {
            this.RemoveReference(this.GetLxdmProject(), strAssemblyName);
        }

        public void RemoveReference(Project project, string strAssemblyName)
        {
            try
            {
                VSLangProj.VSProject vsProject = (VSLangProj.VSProject)project.Object;
                VSLangProj.Reference reference = vsProject.References.Find(strAssemblyName);
                if (!reference.IsNullOrEmpty())
                    reference.Remove();
            }
            catch (Exception excep)
            {
                MessageBox.Show(@"Cannot remove the assembly """ + strAssemblyName + @""" to the project!\r\nDetails:\r\n" + excep.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void DeleteInconsistentFiles()
        {
            try
            {
                ProjectItem item = GetDiagramProjectItem();
                List<string> deletedList = new List<string>();

                if (!item.IsNull())
                {
                    foreach (ProjectItem projectItem in item.ProjectItems)
                    {
                        if (Path.GetExtension(projectItem.Name).ToLower() == ".tt" && projectItem.Name.Left((Path.GetFileNameWithoutExtension(item.Name) + ".").Length) != (Path.GetFileNameWithoutExtension(item.Name) + "."))
                            deletedList.Add(projectItem.Name);

                        if (Path.GetExtension(projectItem.Name).ToLower() == ".cs" && projectItem.Name.Left((Path.GetFileNameWithoutExtension(item.Name) + ".").Length) != (Path.GetFileNameWithoutExtension(item.Name) + "."))
                        {
                            if (MessageBox.Show("The file [" + projectItem.Name + "] is inconsistent. Do you want to delete it?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                deletedList.Add(projectItem.Name);
                        }
                    }
                }

                //Apply deleting
                foreach (string itemName in deletedList)
                {
                    item.ProjectItems.Item(itemName).Delete();
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("Problem by deleting inconsistent files of this project with the following message: " + exp.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public static string ReadResourceContent(string resourcePath)
        {
            string body = String.Empty;
            //Read template file
            using (Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    body = reader.ReadToEnd();
                }
            }
            return body;
        }

        public static bool ExistsProjectItem(ProjectItems items, string itemName)
        {
            foreach (ProjectItem item in items)
            {
                if (item.Name == itemName)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Verify file in Source Control.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool VerifySourceControl(string fileName)
        {
            return VerifySourceControl(fileName, this.GetLxdmProject());
        }

        public static bool VerifySourceControl(string fileName, Project project)
        {
            //if (File.Exists(fileName) && project != null)
            //    return project.DTE.VerifySourceControl(fileName);

            return true;
        }


        public List<ModelClass> GetModelClasses(List<BusinessDataModelDesignerRoot> models)
        {
            List<ModelClass> classes = new List<ModelClass>();

            //Add normal classes
            foreach (var model in models)
            {
                if (model.Types != null && model.Types.Count > 0)
                {
                    classes.AddRange(
                        (from cls in model.Types
                         where cls is ModelClass && !((ModelClass)cls).InStudy && !(cls is ReferenceModelClass)
                         && !classes.Any(c => c.Name == cls.Name)
                         select (ModelClass)cls));
                }
            }
            //Add normal reference classes
            foreach (var model in models)
            {
                if (model.Types != null && model.Types.Count > 0)
                {
                    classes.AddRange(
                        (from cls in model.Types
                         where (cls is ReferenceModelClass) && !((ReferenceModelClass)cls).InStudy
                         && !classes.Any(c => c.Name == cls.Name)
                         select (ModelClass)cls));
                }
            }

            return SortElementsByInheritance(classes);
        }

        public List<ModelClass> SortElementsByInheritance(List<ModelClass> types)
        {
            List<ModelClass> orderedList = new List<ModelClass>();

            //Add inheritance types
            var topClasses = types.Where(e => e.IsInheritanceClass()).ToArray();
            Action<ModelClass> fillDerivedTypes = null;
            fillDerivedTypes = (type) =>
                {
                    orderedList.Add(type);
                    foreach (var derivedType in type.Subclasses)
                    {
                        fillDerivedTypes(derivedType);
                    }

                    foreach (var derivedType in type.SubclassesSh)
                    {
                        fillDerivedTypes(derivedType);
                    }
                };

            topClasses.Foreach(e => fillDerivedTypes(e));

            //Add all other types
            var otherTypes = types.Where(e => !orderedList.Contains(e)).ToList();
            orderedList.AddRange(otherTypes);

            return orderedList;
        }

        public List<DomainView> GetAllDomains()
        {
            return
                (from model in this.Types
                 where model is DomainView
                 select (DomainView)model).ToList();
        }

        public BusinessDataModelDesignerRoot GetMainModel(List<BusinessDataModelDesignerRoot> models)
        {
            var principal = models.FirstOrDefault(e => e.DbProviders.Any(p => p.IsDefault));

            if (principal == null)
                principal = this;

            return principal;
        }

        public string GetConnectionName()
        {
            var defaultProvider = this.DbProviders.FirstOrDefault(e => !e.ConnectionName.IsNullOrEmpty() && e.IsDefault);
            return (defaultProvider == null ? this.DocumentName.Left(".") : defaultProvider.ConnectionName);
        }

        public string GetUniqueValuesDefinition(string indent, List<BusinessDataModelDesignerRoot> models)
        {
            Linx.Tools.CodeBuilder builder = new Tools.CodeBuilder(indent);
            List<string> objects = new List<string>();
            string objectName;

            foreach (var model in models)
            {
                foreach (ModelClass modelClass in model.Types.Where(e => e is ModelClass && ((ModelClass)e).Kind == ClassKind.Table && !((ModelClass)e).InStudy))
                {
                    objectName = modelClass.Name;
                    if (!objects.Contains(objectName))
                    {
                        objects.Add(objectName);

                        if (modelClass.HasUniqueValues())
                        {
                            builder.AddLine();
                            builder.AddLine("var added" + modelClass.Name + "List = this.ChangeTracker.Entries().Where(e => e.State == EntityState.Added && e.Entity is " + modelClass.Name + ").Select(e => (" + modelClass.Name + ")e.Entity).ToArray();");
                            builder.AddLine("for (int idx = 0; idx < added" + modelClass.Name + "List.Length; idx++)");
                            builder.AddLine("{");
                            foreach (var attr in modelClass.GetUniqueValues())
                            {
                                builder.AddLine("    var entity = added" + modelClass.Name + "List[idx];");
                                builder.AddLine("    var oldKey = entity." + attr.Name + ";");
                                builder.AddLine("    var newKey = " + attr.GetUniqueValue() + ";");
                                builder.AddLine("    entity." + attr.Name + " = newKey;");
                                builder.AddLine("    entity.UpdateRelations(this.ChangeTracker.Entries(), oldKey, newKey);");
                            }
                            builder.AddLine("}");
                        }
                    }

                }
            }

            return builder.GetBody();
        }

        public string GetConstraintDescriptors(string indent, List<BusinessDataModelDesignerRoot> models, Provider defaultProvider)
        {
            Linx.Tools.CodeBuilder builder = new Tools.CodeBuilder(indent);
            List<string> objects = new List<string>();
            string objectName;

            builder.AddLine("var result = new Dictionary<string, string>();");


            foreach (var model in models)
            {
                foreach (ModelClass modelClass in model.Types.Where(e => e is ModelClass && ((ModelClass)e).Kind == ClassKind.Table && !((ModelClass)e).InStudy))
                {
                    foreach (var index in modelClass.ModelIndexes)
                    {

                        objectName = index.Name;
                        if (!objects.Contains(objectName))
                        {
                            objects.Add(objectName);

                            var columns = index.Properties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(e => (e + " ").Left(" "));
                            string idxDescr = String.Join(",", modelClass.GetAllAttributes().Where(e => columns.Contains(e.Name)).Select(e => "'" + (e.DisplayName.IsNullOrEmpty() ? e.Name.Replace("_", " ").Proper() : e.DisplayName) + "'").ToArray());
                            builder.AddLine("result.Add(\"" + objectName + "\", \"" + idxDescr + "\");");


                        }
                    }
                }
            }

            objects.Clear();

            foreach (var model in models)
            {
                foreach (ModelClass modelClass in model.Types.Where(e => e is ModelClass && ((ModelClass)e).Kind == ClassKind.Table && !((ModelClass)e).InStudy && (!((ModelClass)e).PrimaryKeyConstraintName.IsNullOrEmpty() || (defaultProvider == Provider.SQLServer && !((ModelClass)e).IsClustered))))
                {
                    objectName = modelClass.GetTableName();
                    if (!objects.Contains(objectName))
                    {
                        objects.Add(objectName);
                        string pkDescr = String.Join(",", modelClass.GetPrimaryKeys().Select(e => "'" + (e.DisplayName.IsNullOrEmpty() ? e.Name.Replace("_", " ").Proper() : e.DisplayName) + "'").ToArray());
                        builder.AddLine("result.Add(\"" + (modelClass.PrimaryKeyConstraintName.IsNullOrEmpty() ? "XPK_" + objectName : modelClass.PrimaryKeyConstraintName) + (defaultProvider == Provider.SQLServer && !modelClass.IsClustered ? "__NC__" : String.Empty) + "\", \"" + pkDescr + "\");");
                    }
                }
            }


            objects.Clear();
            //Default Associations
            foreach (var model in models)
            {
                foreach (ModelClass modelClass in model.Types.Where(e => e is ModelClass && ((ModelClass)e).Kind == ClassKind.Table && !((ModelClass)e).InStudy))
                {
                    foreach (var link in Association.GetLinksToSourceModelClasses(modelClass))
                    {
                        if (link.TargetModelClass.InStudy || link.SourceModelClass.InStudy)
                            continue;

                        objectName = link.SourceModelClass.GetTableName() + "." +
                        String.Join(".", link.SourceModelClass.GetTopSuperClass().Attributes.Where(e => e.IsPrimaryKey && !e.IsNullable).Select(e => e.GetColumnName()).OrderBy(e => e).ToArray()) + "." +
                        modelClass.GetTableName() + "." +
                        String.Join(".", link.GetTargetColumns());
                        if (!objects.Contains(objectName))
                        {
                            objects.Add(objectName);

                            string fkDescr = String.Join(",", link.GetTargetAttributeElements().Select(e => "'" + (e.DisplayName.IsNullOrEmpty() ? e.Name.Replace("_", " ").Proper() : e.DisplayName) + "'").ToArray());
                            string fkName = link.GetFkName(false, this.IsDynamicForeignKeyNames());
                            builder.AddLine("result.Add(\"" + fkName + "\", \"" + fkDescr + "\");");
                        }
                    }
                }

                //Multiple Associations
                foreach (ModelClass modelClass in model.Types.Where(e => e is ModelClass && ((ModelClass)e).Kind == ClassKind.Table && !((ModelClass)e).InStudy))
                {
                    var lma = MultipleAssociationTarget.GetLinkToMultipleAssociation(modelClass);
                    if (lma != null && lma.MultipleAssociation != null)
                    {
                        foreach (var link in MultipleAssociationOrigin.GetLinksToOriginTypes(lma.MultipleAssociation))
                        {
                            if (link.OriginType.InStudy || link.MultipleAssociation.TargetType.InStudy)
                                continue;

                            objectName = link.OriginType.GetTableName() + "." +
                            String.Join(".", link.OriginType.GetTopSuperClass().Attributes.Where(e => e.IsPrimaryKey && !e.IsNullable).Select(e => e.GetColumnName()).OrderBy(e => e).ToArray()) + "." +
                            modelClass.GetTableName() + "." +
                            String.Join(".", link.GetTargetColumns());
                            if (!objects.Contains(objectName))
                            {
                                objects.Add(objectName);

                                string fkDescr = String.Join(",", link.GetTargetAttributeElements().Select(e => "'" + (e.DisplayName.IsNullOrEmpty() ? e.Name.Replace("_", " ").Proper() : e.DisplayName) + "'").ToArray());
                                string fkName = link.GetFkName(false, this.IsDynamicForeignKeyNames());
                                builder.AddLine("result.Add(\"" + fkName + "\", \"" + fkDescr + "\");");
                            }
                        }
                    }
                }

                //SuperClass Associations
                foreach (ModelClass modelClass in model.Types.Where(e => e is ModelClass && ((ModelClass)e).Kind == ClassKind.Table && !((ModelClass)e).InStudy && ((ModelClass)e).Superclass != null))
                {
                    //Get top class
                    var superClass = modelClass.Superclass;
                    while (superClass.Superclass != null)
                    {
                        superClass = superClass.Superclass;
                    }

                    if (superClass.InStudy)
                        continue;

                    string columns = String.Join(".", superClass.Attributes.Where(e => e.IsPrimaryKey && !e.IsNullable).Select(e => e.GetColumnName()).OrderBy(e => e).ToArray());
                    objectName = modelClass.Superclass.GetTableName() + "." +
                    columns + "." +
                    modelClass.GetTableName() + "." +
                    columns;
                    if (!objects.Contains(objectName))
                    {
                        objects.Add(objectName);

                        string fkDescr = String.Join(",", superClass.Attributes.Where(e => e.IsPrimaryKey && !e.IsNullable).Select(e => "'" + (e.DisplayName.IsNullOrEmpty() ? e.Name.Replace("_", " ").Proper() : e.DisplayName) + "'").ToArray());
                        string fkName = modelClass.GetFkBaseName();
                        builder.AddLine("result.Add(\"" + fkName + "\", \"" + fkDescr + "\");");
                    }
                }
            }

            builder.AddLine("return result;");
            return builder.GetBody();
        }

        #region Templates

        const string sharedFolderName = "share";
        const string sharedFile = "ContextInfo.shr";
        public Dictionary<string, string> GetSharedInfo()
        {
            Dictionary<string, string> contextInfo;
            var defProvider = this.DbProviders.FirstOrDefault(e => e.IsDefault);
            if (defProvider != null)
            {
                contextInfo = new Dictionary<string, string>();
                contextInfo["DataContextName"] = this.DataContextName;
                contextInfo["ProviderType"] = defProvider.Type.ToString();
                contextInfo["EnableMigration"] = defProvider.EnableMigration.ToString().ToLower();
                contextInfo["ConnectionString"] = defProvider.GetConnectionString();
                contextInfo["RemoveAutomaticIndexes"] = this.RemoveAutomaticIndexes.ToString().ToLower();
                contextInfo["EnableAutomaticAuthorization"] = this.EnableAutomaticAuthorization.ToString().ToLower();
                contextInfo["EnableAccessConnectionControl"] = this.EnableAccessConnectionControl.ToString().ToLower();
                contextInfo["SetIdLinxWithIdGpecon"] = this.SetIdLinxWithIdGpecon.ToString().ToLower();
                contextInfo["ForceDynamicForeignKeyNames"] = this.ForceDynamicForeignKeyNames.ToString().ToLower();
                return contextInfo;
            }
            else
            {
                Project current = this.GetLxdmProject();
                if (current != null)
                {
                    contextInfo = GetSharedInfo(current);
                    if (contextInfo != null)
                        return contextInfo;
                }
            }

            contextInfo = new Dictionary<string, string>();
            contextInfo["DataContextName"] = this.DataContextName;
            contextInfo["ProviderType"] = Provider.SQLServer.ToString();
            contextInfo["EnableMigration"] = "true";
            contextInfo["ConnectionString"] = String.Empty;
            contextInfo["RemoveAutomaticIndexes"] = "false";
            contextInfo["EnableAutomaticAuthorization"] = "true";
            contextInfo["EnableAccessConnectionControl"] = "true";
            contextInfo["SetIdLinxWithIdGpecon"] = "false";
            contextInfo["ForceDynamicForeignKeyNames"] = "false";
            return contextInfo;
        }

        public static Dictionary<string, string> GetSharedInfo(Project current)
        {
            Dictionary<string, string> contextInfo = null;
            if (current != null)
            {
                string outputFile = Path.Combine(Path.Combine(GetProjectPath(current), sharedFolderName), sharedFile);
                if (File.Exists(outputFile))
                {
                    try
                    {
                        contextInfo = SerializationManager<Dictionary<string, string>>.StringToObject(File.ReadAllText(outputFile));
                        return contextInfo;
                    }
                    catch { }
                }
            }

            return contextInfo;
        }

        //public static bool CanGeneratedViews(Project current)
        //{
        //    if (current != null)
        //    {
        //        string folderName = "Model";
        //        var folder = GetProjectItemByName(current, folderName);
        //        if (folder != null)
        //        {
        //            string file = Path.Combine(folder.Properties.Item("FullPath").Value.ToString(), "GenerateViews.txt");
        //            return (File.Exists(file) && File.ReadAllText(file) == "true");
        //        }
        //    }
        //    return false;
        //}

        //public static void SetGeneratedViews(Project current, bool generate)
        //{
        //    if (current != null && !GetDefaultContextName(current).IsNullOrEmpty())
        //    {
        //        string folderName = "Model";
        //        var folder = GetProjectItemByName(current, folderName);
        //        if (folder != null)
        //        {
        //            string file = Path.Combine(folder.Properties.Item("FullPath").Value.ToString(), "GenerateViews.txt");
        //            File.WriteAllText(file, generate.ToString().ToLower());
        //        }
        //    }
        //}

        public void SaveSharedInfo()
        {
            var defProvider = this.DbProviders.FirstOrDefault(e => e.IsDefault);
            if (defProvider != null)
            {
                string outputFile = "";
                Project current = this.GetLxdmProject();
                ProjectItem folder = null, newItem;

                if (current != null)
                {
                    Dictionary<string, string> contextInfo = new Dictionary<string, string>();
                    contextInfo["DataContextName"] = this.DataContextName;
                    contextInfo["ProviderType"] = defProvider.Type.ToString();
                    contextInfo["EnableMigration"] = defProvider.EnableMigration.ToString().ToLower();
                    contextInfo["ConnectionString"] = defProvider.GetConnectionString();
                    contextInfo["RemoveAutomaticIndexes"] = this.RemoveAutomaticIndexes.ToString().ToLower();
                    contextInfo["EnableAutomaticAuthorization"] = this.EnableAutomaticAuthorization.ToString().ToLower();
                    contextInfo["EnableAccessConnectionControl"] = this.EnableAccessConnectionControl.ToString().ToLower();
                    contextInfo["SetIdLinxWithIdGpecon"] = this.SetIdLinxWithIdGpecon.ToString().ToLower();
                    contextInfo["ForceDynamicForeignKeyNames"] = this.ForceDynamicForeignKeyNames.ToString().ToLower();
                    string body = SerializationManager<Dictionary<string, string>>.ObjectToString(contextInfo);

                    folder = GetProjectItemByName(current, sharedFolderName);
                    if (folder == null)
                        folder = current.ProjectItems.AddFolder(sharedFolderName);

                    if (folder != null)
                    {
                        outputFile = Path.Combine(Path.Combine(this.GetProjectPath(), sharedFolderName), sharedFile);
                        if (!this.VerifySourceControl(outputFile))
                            return;

                        if (!ExistsProjectItem(folder.ProjectItems, sharedFile))
                        {
                            File.WriteAllText(outputFile, body);
                            newItem = folder.ProjectItems.AddFromFile(outputFile);
                        }
                        else
                        {
                            if (File.ReadAllText(outputFile) != body)
                                File.WriteAllText(outputFile, body);
                            newItem = folder.ProjectItems.Item(sharedFile);
                        }
                    }
                }
            }
        }


        public void ValidContextFiles(List<string> contexts, List<string> entityFiles, string projPrefix)
        {
            //Context folder
            var folder = GetDataContextFolder();
            if (folder != null)
            {
                foreach (ProjectItem item in folder.ProjectItems)
                {
                    string name = item.Name.ToLower();
                    if (name.Length > 11 && name.Right(11) == ".codegen.js" && !contexts.Contains(item.Name.ToLower()))
                    {
                        item.Delete();
                    }
                }
            }

            //Entities folder
            folder = GetDataEntitytFolder();
            if (folder != null)
            {
                foreach (ProjectItem item in folder.ProjectItems)
                {
                    string name = item.Name.ToLower();
                    if (name.Length > 11 && name.Right(11) == ".codegen.js" && !entityFiles.Contains(item.Name.ToLower()))
                    {
                        item.Delete();
                    }
                }
            }

            //EntityRoutes folder
            if (!projPrefix.IsNullOrEmpty())
            {
                folder = GetRoutesEntitytFolder(projPrefix);
                if (folder != null)
                {
                    foreach (ProjectItem item in folder.ProjectItems)
                    {
                        string name = item.Name.ToLower();
                        if (name.Length > 11 && name.Right(11) == ".codegen.js" && !entityFiles.Contains(item.Name.ToLower()))
                        {
                            item.Delete();
                        }
                    }
                }
            }
        }

        public const string SourceCodeFolder = "NodePack";
        public const string ModelFolderName = "model";
        public const string ContextFolderName = "context";
        public const string EntityFolderName = "entities";
        public const string CustomFolderName = "custom";
        public const string EntityFolderRoutes = "routes";

        public ProjectItem GetDataContextFolder()
        {
            ProjectItem folder = null, modelFolder = null, srcFolder = GetSrcFolder();

            if (srcFolder != null)
            {
                modelFolder = GetProjectItemByName(srcFolder.ProjectItems, ModelFolderName);
                if (modelFolder == null)
                    modelFolder = srcFolder.ProjectItems.AddFolder(ModelFolderName);

                if (modelFolder != null)
                {
                    folder = GetProjectItemByName(modelFolder.ProjectItems, ContextFolderName);
                    if (folder == null)
                        folder = modelFolder.ProjectItems.AddFolder(ContextFolderName);
                }
            }

            return folder;
        }




        public ProjectItem GetSrcFolder()
        {
            Project current = this.GetLxdmProject();
            ProjectItem srcFolder = GetProjectItemByName(current, SourceCodeFolder);
            if (srcFolder == null)
                srcFolder = current.ProjectItems.AddFolder(SourceCodeFolder);

            return srcFolder;
        }

        public ProjectItem GetDataEntitytFolder()
        {
            ProjectItem folder = null, modelFolder = null, srcFolder = GetSrcFolder();

            if (srcFolder != null)
            {
                modelFolder = GetProjectItemByName(srcFolder.ProjectItems, ModelFolderName);
                if (modelFolder == null)
                    modelFolder = srcFolder.ProjectItems.AddFolder(ModelFolderName);

                if (modelFolder != null)
                {
                    folder = GetProjectItemByName(modelFolder.ProjectItems, EntityFolderName);
                    if (folder == null)
                        folder = modelFolder.ProjectItems.AddFolder(EntityFolderName);
                }
            }

            return folder;
        }

        public string GetDataEntitytCustomFolderName(string entityName)
        {
            return entityName + "_" + CustomFolderName;
        }

        public ProjectItem GetDataEntitytCustomFolder(string entityName)
        {
            ProjectItem srcFolder = GetDataEntitytFolder(), folder = null;
            if (srcFolder != null)
            {
                string folderName = GetDataEntitytCustomFolderName(entityName);
                folder = GetProjectItemByName(srcFolder.ProjectItems, folderName);
                if (folder == null)
                    folder = srcFolder.ProjectItems.AddFolder(folderName);
            }

            return folder;
        }

        public ProjectItem GetRoutesEntitytFolder(string projPrefix)
        {
            ProjectItem folder = null, serviceFolder = null, srcFolder = GetSrcFolder();

            if (srcFolder != null)
            {
                serviceFolder = GetProjectItemByName(srcFolder.ProjectItems, projPrefix);
                if (serviceFolder == null)
                    serviceFolder = srcFolder.ProjectItems.AddFolder(projPrefix);

                if (serviceFolder != null)
                {
                    folder = GetProjectItemByName(serviceFolder.ProjectItems, EntityFolderRoutes);
                    if (folder == null)
                        folder = serviceFolder.ProjectItems.AddFolder(EntityFolderRoutes);
                }
            }

            return folder;
        }

        public string UpdateContextTemplate(string body, string contextName)
        {
            string outputFile = "";
            Project current = this.GetLxdmProject();
            ProjectItem folder = null, newItem;
            string ctxFile = contextName + ".CodeGen.js";

            if (current != null)
            {
                folder = GetDataContextFolder();

                if (folder != null)
                {
                    outputFile = Path.Combine(folder.Properties.Item("FullPath").Value.ToString(), ctxFile);
                    if (!this.VerifySourceControl(outputFile))
                        return "";

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
                }

                //GenerateOperationalEvents(current);
            }

            return ctxFile;
        }


        public string UpdateFileTemplate(string body, string fileName, string folderName = "", string subFolderName = "")
        {
            string outputFile = "";
            ProjectItem folder = null, newItem, srcFolder = GetSrcFolder();

            if (srcFolder != null)
            {
                if (folderName.IsNullOrEmpty())
                {
                    folder = srcFolder;
                }
                else
                {
                    folder = GetProjectItemByName(srcFolder.ProjectItems, folderName);
                    if (folder == null)
                        folder = srcFolder.ProjectItems.AddFolder(folderName);

                    if (folder != null && !subFolderName.IsNullOrEmpty())
                    {
                        srcFolder = folder;
                        folder = GetProjectItemByName(srcFolder.ProjectItems, subFolderName);
                        if (folder == null)
                            folder = srcFolder.ProjectItems.AddFolder(subFolderName);
                    }

                }

                if (folder != null)
                {
                    outputFile = Path.Combine(folder.Properties.Item("FullPath").Value.ToString(), fileName);
                    if (!this.VerifySourceControl(outputFile))
                        return "";

                    if (!ExistsProjectItem(folder.ProjectItems, fileName))
                    {
                        File.WriteAllText(outputFile, body);
                        newItem = folder.ProjectItems.AddFromFile(outputFile);
                    }
                    else
                    {
                        if (File.ReadAllText(outputFile) != body)
                            File.WriteAllText(outputFile, body);
                        newItem = folder.ProjectItems.Item(fileName);
                    }
                }
            }

            return fileName;
        }

        public string UpdateEntityTemplate(string body, string entityName, string customCodeName = "")
        {
            bool customCode = !customCodeName.IsNullOrEmpty();
            string outputFile = "";
            Project current = this.GetLxdmProject();
            ProjectItem folder = null, newItem;
            string entityFile = (customCode ? customCodeName : entityName + "." + "CodeGen") + ".js";

            if (current != null)
            {
                folder = (customCode ? GetDataEntitytCustomFolder(entityName) : GetDataEntitytFolder());

                if (folder != null)
                {
                    outputFile = Path.Combine(folder.Properties.Item("FullPath").Value.ToString(), entityFile);
                    if (!this.VerifySourceControl(outputFile))
                        return "";

                    if (!ExistsProjectItem(folder.ProjectItems, entityFile))
                    {
                        File.WriteAllText(outputFile, body);
                        newItem = folder.ProjectItems.AddFromFile(outputFile);
                    }
                    else if (!customCode)
                    {
                        if (File.ReadAllText(outputFile) != body)
                            File.WriteAllText(outputFile, body);
                        newItem = folder.ProjectItems.Item(entityFile);
                    }
                }
            }

            return (customCode ? "" : entityFile);
        }

        public static void RemovePreGeneratedViewsInconsistencies(Project current, bool removeTemplate)
        {
            if (current != null)
            {
                string folderName = "Model";
                var folder = GetProjectItemByName(current, folderName);
                if (folder != null)
                {
                    string templateName = "pregeneratedviews";
                    foreach (ProjectItem item in folder.ProjectItems)
                    {
                        if (item.Name.ToLower().StartsWith(templateName))
                        {
                            if (removeTemplate || item.Name.ToLower() != templateName + ".tt")
                            {
                                VerifySourceControl(item.Properties.Item("FullPath").Value.ToString(), current);
                                item.Delete();
                            }
                        }
                    }
                }
            }
        }

        public void GenerateBusinessOperations(bool isShared)
        {
            Project project = this.GetLxdmProject();
            if (project.IsNull())
                return;

            ProjectItem item = project.ProjectItems.Item("Model");
            StringBuilder codeBuilder;
            string outputFile;

            if (!item.IsNull())
            {

                foreach (ModelClass modelClass in this.Types.Where(e => e is ModelClass))
                {
                    if (modelClass.Operations.Count > 0)
                    {
                        outputFile = Path.Combine(this.GetProjectPath(), item.Name + "\\" + modelClass.Name + ".Operations" + (isShared ? ".shared" : "") + ".cs");
                        if (!File.Exists(outputFile) || !ExistsProjectItem(item.ProjectItems, modelClass.Name + ".Operations" + (isShared ? ".shared" : "") + ".cs"))
                        {
                            if (!this.VerifySourceControl(outputFile))
                                return;

                            RemoveProjectItems(item.ProjectItems, modelClass.Name + ".Operations" + (isShared ? ".shared" : "") + ".cs");
                            codeBuilder = new StringBuilder();

                            //Add Events
                            this.GenerateEntityOperationsCode(codeBuilder, modelClass, isShared);
                            System.IO.File.WriteAllText(outputFile, codeBuilder.ToString());
                            //Add project item.
                            item.ProjectItems.AddFromFile(outputFile);
                        }
                    }
                }
            }
        }

        private void GenerateEntityOperationsCode(StringBuilder codeBuilder, ModelClass modelClass, bool isShared)
        {
            string baseIndent = "	";

            codeBuilder.AppendLine("using System;");
            codeBuilder.AppendLine("using System.Collections;");
            codeBuilder.AppendLine("using System.Collections.Generic;");
            codeBuilder.AppendLine("using System.Linq.Expressions;");
            codeBuilder.AppendLine("using Linx.Tools;");
            codeBuilder.AppendLine("using System.Linq;");
            codeBuilder.AppendLine("using System.ComponentModel;");
            codeBuilder.AppendLine("using System.ComponentModel.DataAnnotations;");

            codeBuilder.AppendLine("");
            codeBuilder.AppendLine("namespace " + modelClass.BusinessDataModelDesignerRoot.TargetNamespace);
            codeBuilder.AppendLine("{");

            //Class Definition
            codeBuilder.AppendLine(baseIndent + "");
            codeBuilder.AppendLine(baseIndent + "////////////////////////////////////////////////////////////////////////////");
            codeBuilder.AppendLine(baseIndent + "//////////////////////// Business Operations Definition ////////////////////");
            codeBuilder.AppendLine(baseIndent + "////////////////////////////////////////////////////////////////////////////");
            codeBuilder.AppendLine(baseIndent + "public partial class " + modelClass.Name);
            codeBuilder.AppendLine(baseIndent + "{");
            codeBuilder.AppendLine(baseIndent + "}");
            //End Class Definition

            //End Namespace
            codeBuilder.AppendLine("}");
        }

        #endregion
    }
}

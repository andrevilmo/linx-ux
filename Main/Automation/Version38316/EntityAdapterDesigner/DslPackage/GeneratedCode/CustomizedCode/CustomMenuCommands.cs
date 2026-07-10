using System.Collections.Generic;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.Modeling.Shell;
using System;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Diagrams;
using System.Linq;
using Linx.Tools;
using EnvDTE;
using System.Windows.Forms;
using Linx.EntityAdapterDesigner.CustomizedCode;


namespace Linx.EntityAdapterDesigner
{

    internal partial class EntityAdapterDesignerCommandSet
    {
        private Guid cmdidEntityAdapterToolWindowGUID = new Guid("{e024017a-a1a6-42ed-84d6-96b12b173a70}");
        private const uint cmdidEntityAdapterToolWindow = 0x0501;

        private Guid cmdidBuildEntityGUID = new Guid("{e024017a-a1a6-42ed-84d6-96b12b173a71}");
        private const int cmdidBuildEntityID = 0x8800;

        private Guid cmdMoveUpGUID = new Guid("{e024017a-a1a6-42ed-84d6-96b12b173a72}");
        private const int cmdMoveUpID = 0x8801;

        private Guid cmdMoveDownGUID = new Guid("{e024017a-a1a6-42ed-84d6-96b12b173a73}");
        private const int cmdMoveDownID = 0x8802;

        private Guid cmdDomainValuesGUID = new Guid("{e024017a-a1a6-42ed-84d6-96b12b173a74}");
        private const int cmdDomainValuesID = 0x8803;

        private Guid cmdOpenCustomValidationGUID = new Guid("{e024017a-a1a6-42ed-84d6-96b12b173a75}");
        private const int cmdOpenCustomValidationID = 0x8804;

        private Guid cmdServerEventsGUID = new Guid("{e024017a-a1a6-42ed-84d6-96b12b173a78}");
        private const int cmdServerEventsID = 0x8807;

        private Guid cmdPropertyChangingEventGUID = new Guid("{e024017a-a1a6-42ed-84d6-96b12b173a82}");
        private const int cmdPropertyChangingEventID = 0x8811;

        private Guid cmdPropertyChangedEventGUID = new Guid("{e024017a-a1a6-42ed-84d6-96b12b173a83}");
        private const int cmdPropertyChangedEventID = 0x8812;

        private Guid cmdUpdateReportGUID = new Guid("{e024017a-a1a6-42ed-84d6-96b12b173a86}");
        private const int cmdUpdateReportID = 0x8815;

        private Guid cmdUpdateMasterDetailReportGUID = new Guid("{e024017a-a1a6-42ed-84d6-96b12b173a87}");
        private const int cmdUpdateMasterDetailReportID = 0x8816;


        private Guid cmdCreateWfByOperationGUID = new Guid("{e024017a-a1a6-42ed-84d6-96b12b173a88}");
        private const int cmdCreateWfByOperationID = 0x8817;

        private Guid cmdidExtendedFilterGUID = new Guid("{e024017a-a1a6-42ed-84d6-96b12b173a89}");
        private const int cmdidExtendedFilterID = 0x8818;

        private Guid cmdidSortPropertyGUID = new Guid("{e024017a-a1a6-42ed-84d6-96b12b173a90}");
        private const int cmdidSortPropertyID = 0x8819;

        private Guid cmdidUserInterfaceGUID = new Guid("{e024017a-a1a6-42ed-84d6-96b12b173a91}");
        private const int cmdidUserInterfaceID = 0x8820;

        private Guid cmdidUserInterfaceLoadGUID = new Guid("{e024017a-a1a6-42ed-84d6-96b12b173a92}");
        private const int cmdidUserInterfaceLoadID = 0x8821;

        private Guid cmdidBuildLookUpGUID = new Guid("{e024017a-a1a6-42ed-84d6-96b12b173a93}");
        private const int cmdidBuildLookUpID = 0x8822;

        private Guid cmdKpiRangeValuesGUID = new Guid("{e024017a-a1a6-42ed-84d6-96b12b173a94}");
        private const int cmdKpiRangeValuesID = 0x8823;

        private Guid cmdidSelectRepresentedEntityGUID = new Guid("{e024017a-a1a6-42ed-84d6-96b12b173a95}");
        private const int cmdidSelectRepresentedEntityID = 0x8824;

        private Guid cmdClientEventsGUID = new Guid("{e024017a-a1a6-42ed-84d6-96b12b173a96}");
        private const int cmdClientEventsID = 0x8825;

        private Guid cmdUiClientEventsGUID = new Guid("{e024017a-a1a6-42ed-84d6-96b12b173a97}");
        private const int cmdUiClientEventsID = 0x8826;

        private Guid cmdScriptEditorGUID = new Guid("{e024017a-a1a6-42ed-84d6-96b12b173a98}");
        private const int cmdScriptEditorID = 0x8827;

        private Guid cmdSaveAllDocsGUID = new Guid("{e024017a-a1a6-42ed-84d6-96b12b173a99}");
        private const int cmdSaveAllDocsID = 0x8828;

        private Guid cmdUpdateSpaGUID = new Guid("{e024017a-a1a6-42ed-84d6-96b12b174a99}");
        private const int cmdUpdateSpaID = 0x8829;

        public void SelectShape(ModelElement modelElement)
        {
            ShapeElement modelElementShape = PresentationViewsSubject.GetPresentation(modelElement).FirstOrDefault() as ShapeElement;

            Diagram diagram = modelElement.Store.ElementDirectory.AllElements.OfType<Diagram>().FirstOrDefault();

            DiagramItem diagramItem = new DiagramItem(modelElementShape);

            diagram.ActiveDiagramView.Selection.Set(diagramItem);
        }

        protected override System.Collections.Generic.IList<System.ComponentModel.Design.MenuCommand> GetMenuCommands()
        {

            ModelElementLocator locator = new ModelElementLocator(this.ServiceProvider);
            //ModelingDocView view = locator.FindDocView(Guid.Empty, null);

            IList<MenuCommand> commands = base.GetMenuCommands();

            //Add new commands
            commands.Add(new DynamicStatusMenuCommand(
            new EventHandler(OnPopUpMenuDisplayAction),
            new EventHandler(OnPopUpMenuClick),
            new CommandID(cmdidBuildEntityGUID, cmdidBuildEntityID)));

            commands.Add(new DynamicStatusMenuCommand(
            new EventHandler(OnPopUpMenuDisplayAction),
            new EventHandler(OnPopUpMenuClick),
            new CommandID(cmdidBuildLookUpGUID, cmdidBuildLookUpID)));

            commands.Add(new DynamicStatusMenuCommand(
            new EventHandler(OnPopUpMenuDisplayAction),
            new EventHandler(OnPopUpMenuClick),
            new CommandID(cmdMoveUpGUID, cmdMoveUpID)));

            commands.Add(new DynamicStatusMenuCommand(
            new EventHandler(OnPopUpMenuDisplayAction),
            new EventHandler(OnPopUpMenuClick),
            new CommandID(cmdMoveDownGUID, cmdMoveDownID)));

            commands.Add(new DynamicStatusMenuCommand(
            new EventHandler(OnPopUpMenuDisplayAction),
            new EventHandler(OnPopUpMenuClick),
            new CommandID(cmdDomainValuesGUID, cmdDomainValuesID)));

            commands.Add(new DynamicStatusMenuCommand(
            new EventHandler(OnPopUpMenuDisplayAction),
            new EventHandler(OnPopUpMenuClick),
            new CommandID(cmdKpiRangeValuesGUID, cmdKpiRangeValuesID)));

            commands.Add(new DynamicStatusMenuCommand(
            new EventHandler(OnPopUpMenuDisplayAction),
            new EventHandler(OnPopUpMenuClick),
            new CommandID(cmdOpenCustomValidationGUID, cmdOpenCustomValidationID)));

            commands.Add(new DynamicStatusMenuCommand(
            new EventHandler(OnPopUpMenuDisplayAction),
            new EventHandler(OnPopUpMenuClick),
            new CommandID(cmdServerEventsGUID, cmdServerEventsID)));


            commands.Add(new DynamicStatusMenuCommand(
            new EventHandler(OnPopUpMenuDisplayAction),
            new EventHandler(OnPopUpMenuClick),
            new CommandID(cmdPropertyChangingEventGUID, cmdPropertyChangingEventID)));

            commands.Add(new DynamicStatusMenuCommand(
            new EventHandler(OnPopUpMenuDisplayAction),
            new EventHandler(OnPopUpMenuClick),
            new CommandID(cmdPropertyChangedEventGUID, cmdPropertyChangedEventID)));

            commands.Add(new DynamicStatusMenuCommand(
            new EventHandler(OnPopUpMenuDisplayAction),
            new EventHandler(OnPopUpMenuClick),
            new CommandID(cmdUpdateReportGUID, cmdUpdateReportID)));

            commands.Add(new DynamicStatusMenuCommand(
            new EventHandler(OnPopUpMenuDisplayAction),
            new EventHandler(OnPopUpMenuClick),
            new CommandID(cmdUpdateMasterDetailReportGUID, cmdUpdateMasterDetailReportID)));

            commands.Add(new DynamicStatusMenuCommand(
            new EventHandler(OnPopUpMenuDisplayAction),
            new EventHandler(OnPopUpMenuClick),
            new CommandID(cmdCreateWfByOperationGUID, cmdCreateWfByOperationID)));

            commands.Add(new DynamicStatusMenuCommand(
            new EventHandler(OnPopUpMenuDisplayAction),
            new EventHandler(OnPopUpMenuClick),
            new CommandID(cmdidExtendedFilterGUID, cmdidExtendedFilterID)));

            commands.Add(new DynamicStatusMenuCommand(
            new EventHandler(OnPopUpMenuDisplayAction),
            new EventHandler(OnPopUpMenuClick),
            new CommandID(cmdidSortPropertyGUID, cmdidSortPropertyID)));

            commands.Add(new DynamicStatusMenuCommand(
            new EventHandler(OnPopUpMenuDisplayAction),
            new EventHandler(OnPopUpMenuClick),
            new CommandID(cmdidUserInterfaceGUID, cmdidUserInterfaceID)));

            commands.Add(new DynamicStatusMenuCommand(
            new EventHandler(OnPopUpMenuDisplayAction),
            new EventHandler(OnPopUpMenuClick),
            new CommandID(cmdidUserInterfaceLoadGUID, cmdidUserInterfaceLoadID)));

            commands.Add(new DynamicStatusMenuCommand(
            new EventHandler(OnPopUpMenuDisplayAction),
            new EventHandler(OnPopUpMenuClick),
            new CommandID(cmdidSelectRepresentedEntityGUID, cmdidSelectRepresentedEntityID)));

            commands.Add(new DynamicStatusMenuCommand(
            new EventHandler(OnPopUpMenuDisplayAction),
            new EventHandler(OnPopUpMenuClick),
            new CommandID(cmdClientEventsGUID, cmdClientEventsID)));

            commands.Add(new DynamicStatusMenuCommand(
            new EventHandler(OnPopUpMenuDisplayAction),
            new EventHandler(OnPopUpMenuClick),
            new CommandID(cmdUiClientEventsGUID, cmdUiClientEventsID)));

            commands.Add(new DynamicStatusMenuCommand(
            new EventHandler(OnPopUpMenuDisplayAction),
            new EventHandler(OnPopUpMenuClick),
            new CommandID(cmdScriptEditorGUID, cmdScriptEditorID)));

            commands.Add(new DynamicStatusMenuCommand(
            new EventHandler(OnPopUpMenuDisplayAction),
            new EventHandler(OnPopUpMenuClick),
            new CommandID(cmdSaveAllDocsGUID, cmdSaveAllDocsID)));

            commands.Add(new DynamicStatusMenuCommand(
            new EventHandler(OnPopUpMenuDisplayAction),
            new EventHandler(OnPopUpMenuClick),
            new CommandID(cmdUpdateSpaGUID, cmdUpdateSpaID)));

            return commands;
        }


        internal void OnPopUpMenuDisplayAction(object sender, EventArgs e)
        {
            MenuCommand command = sender as MenuCommand;
            command.Visible = command.Enabled = false;

            foreach (object selectedObject in this.CurrentSelection)
            {
                if (selectedObject is EntityAdapterUserInterfaceShape && (command.CommandID.ID == cmdidUserInterfaceID || command.CommandID.ID == cmdUiClientEventsID || command.CommandID.ID == cmdidUserInterfaceLoadID))
                {
                    command.Visible = true;
                    command.Enabled = true;
                    break;
                }

                if (selectedObject is ClientEvent && command.CommandID.ID == cmdScriptEditorID)
                {
                    command.Visible = true;
                    command.Enabled = true;
                    break;
                }

                if (selectedObject is EntityAdapterDesignerDiagram && (command.CommandID.ID == cmdSaveAllDocsID || command.CommandID.ID == cmdUpdateSpaID))
                {
                    command.Visible = true;
                    command.Enabled = true;
                    break;
                }
                
                if (selectedObject is EntityAdapterUserInterfaceShape && command.CommandID.ID == cmdidSelectRepresentedEntityID && ((EntityAdapterUserInterface)((EntityAdapterUserInterfaceShape)selectedObject).ModelElement).Subscription != null)
                {
                    command.Visible = true;
                    command.Enabled = true;
                    break;
                }

                if (selectedObject is DomainViewShape && command.CommandID.ID == cmdDomainValuesID)
                {
                    command.Visible = command.Enabled = true;
                    break;
                }

                if (selectedObject is Linx.EntityAdapterDesigner.KeyPerformanceIndicatorShape && command.CommandID.ID == cmdKpiRangeValuesID)
                {
                    command.Visible = command.Enabled = true;
                    break;
                }

                if (selectedObject is EntityAdapterShape && (command.CommandID.ID == cmdServerEventsID || command.CommandID.ID == cmdidBuildEntityID || command.CommandID.ID == cmdidExtendedFilterID || command.CommandID.ID == cmdidSortPropertyID || command.CommandID.ID == cmdClientEventsID))
                {
                    command.Visible = true;
                    command.Enabled = true;

                    break;
                }

                if (selectedObject is LookUpAdapterShape && command.CommandID.ID == cmdidBuildLookUpID)
                {
                    command.Visible = true;
                    command.Enabled = true;

                    break;
                }

                if ((selectedObject is EntityAdapterProperty || selectedObject is EntityAdapterFormula) && (command.CommandID.ID == cmdPropertyChangingEventID || command.CommandID.ID == cmdPropertyChangedEventID))
                {
                    command.Visible = command.Enabled = true;
                    break;
                }


                if (selectedObject is GenericOperation && !(selectedObject is ClientEvent) && command.CommandID.ID == cmdCreateWfByOperationID)
                {
                    command.Visible = command.Enabled = (((GenericOperation)selectedObject).Workflow == null && !((GenericOperation)selectedObject).IsShared && ((GenericOperation)selectedObject).Access == OperationAccess.Public);
                    break;
                }


                if ((selectedObject is EntityAdapterEvent || selectedObject is EntityAdapterOperation || selectedObject is LookUpProperty) && (command.CommandID.ID == cmdMoveUpID || command.CommandID.ID == cmdMoveDownID))
                {
                    command.Visible = command.Enabled = true;
                    break;
                }

                if ((selectedObject is EntityAdapterShape || selectedObject is EntityAdapterAttribute) && command.CommandID.ID == cmdOpenCustomValidationID)
                {
                    command.Visible = command.Enabled = true;
                    break;
                }

                if ((selectedObject is EntityAdapterShape) && (command.CommandID.ID == cmdUpdateReportID || (command.CommandID.ID == cmdUpdateMasterDetailReportID && ((EntityAdapter)((EntityAdapterShape)selectedObject).ModelElement).HasDetails())))
                {
                    command.Visible = command.Enabled = true;
                    break;
                }

            }
        }

        internal void OnPopUpMenuClick(object sender, EventArgs e)
        {
            MenuCommand command = sender as MenuCommand;

            foreach (object selectedObject in this.CurrentSelection)
            {
                if (selectedObject is EntityAdapterDesignerDiagram && command.CommandID.ID == cmdSaveAllDocsID)
                {
                    ((EntityAdapterDesignerRoot)((EntityAdapterDesignerDiagram)selectedObject).ModelElement).SaveAllDocuments();
                    break;
                }

                if (selectedObject is EntityAdapterDesignerDiagram && command.CommandID.ID == cmdUpdateSpaID)
                {
                    ((EntityAdapterDesignerRoot)((EntityAdapterDesignerDiagram)selectedObject).ModelElement).UpdateSpaStructure();
                    break;
                }

                if (selectedObject is ClientEvent && command.CommandID.ID == cmdScriptEditorID)
                {
                    ((ClientEvent)selectedObject).EditScript();
                    break;
                }

                if (selectedObject is EntityAdapterUserInterfaceShape)
                {
                    if (command.CommandID.ID == cmdidUserInterfaceID)
                    {
                        SaveFileDialog dialog = new SaveFileDialog();
                        dialog.Filter = "XML file (*.uil)|*.uil";
                        dialog.Title = "Where do you want to save the [UI Layout] file?";
                        if (dialog.ShowDialog() == DialogResult.OK)
                            System.IO.File.WriteAllText(dialog.FileName, ((EntityAdapterUserInterface)((EntityAdapterUserInterfaceShape)selectedObject).ModelElement).LayoutContent);
                        dialog.Dispose();
                        dialog = null;
                    }

                    if (command.CommandID.ID == cmdUiClientEventsID)
                    {
                        using (Transaction transaction =
                            this.CurrentEntityAdapterDesignerDocData.Store.TransactionManager.BeginTransaction("Change designer by UI events."))
                        {
                            CustomCode.FrmAddUiClientEvents frmEvents = new CustomCode.FrmAddUiClientEvents();
                            frmEvents.UI = ((EntityAdapterUserInterfaceShape)selectedObject).ModelElement as EntityAdapterUserInterface;
                            frmEvents.ShowDialog();
                            transaction.Commit();
                        }
                    }

                    if (command.CommandID.ID == cmdidUserInterfaceLoadID)
                    {
                        OpenFileDialog dialog = new OpenFileDialog();
                        dialog.Filter = "XML file (*.uil)|*.uil";
                        dialog.Title = "Where do you want to get the [UI Layout] file?";
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            using (Transaction transaction =
                                    ((EntityAdapterUserInterface)((EntityAdapterUserInterfaceShape)selectedObject).ModelElement).Store.TransactionManager.BeginTransaction("Change User Interface Content."))
                            {
                                ((EntityAdapterUserInterface)((EntityAdapterUserInterfaceShape)selectedObject).ModelElement).LayoutContent = System.IO.File.ReadAllText(dialog.FileName);
                                transaction.Commit();
                            }

                        }
                        dialog.Dispose();
                        dialog = null;
                    }

                    if (command.CommandID.ID == cmdidSelectRepresentedEntityID && ((EntityAdapterUserInterface)((EntityAdapterUserInterfaceShape)selectedObject).ModelElement).Subscription != null)
                    {
                        ((EntityAdapterUserInterface)((EntityAdapterUserInterfaceShape)selectedObject).ModelElement).SelectRepresentedEntity();
                    }

                    break;
                }

                if ((selectedObject is EntityAdapterProperty || selectedObject is EntityAdapterFormula) && (command.CommandID.ID == cmdPropertyChangingEventID || command.CommandID.ID == cmdPropertyChangedEventID))
                {
                    using (Transaction transaction =
                            this.CurrentEntityAdapterDesignerDocData.Store.TransactionManager.BeginTransaction("Change designer by change event."))
                    {
                        if (selectedObject is EntityAdapterProperty)
                        {
                            ((EntityAdapterProperty)selectedObject).EntityAdapter.AddPropertyChangeEvent((EntityAdapterProperty)selectedObject, command.CommandID.ID == cmdPropertyChangingEventID);
                        }
                        else
                        {
                            ((EntityAdapterFormula)selectedObject).EntityAdapter.AddPropertyChangeEvent((EntityAdapterFormula)selectedObject, command.CommandID.ID == cmdPropertyChangingEventID);
                        }
                        transaction.Commit();
                    }
                    break;
                }


                if (selectedObject is GenericOperation && command.CommandID.ID == cmdCreateWfByOperationID)
                {
                    using (Transaction transaction =
                            this.CurrentEntityAdapterDesignerDocData.Store.TransactionManager.BeginTransaction("Create Workflow related with this operation."))
                    {
                        Workflow wf = null;
                        string fullMethodName = String.Empty;
                        if (selectedObject is EntityAdapterOperation)
                        {
                            fullMethodName = ((EntityAdapterOperation)selectedObject).EntityAdapter.Name + "." + ((EntityAdapterOperation)selectedObject).Name;
                            wf = new Workflow(((EntityAdapterOperation)selectedObject).Partition) { Display = fullMethodName, Name = "WF_" + fullMethodName.Replace(".", "_"), IsOperationRelated = true };
                            ((EntityAdapterOperation)selectedObject).EntityAdapter.EntityAdapterDesignerRoot.Workflows.Add(wf);
                        }
                        else if (selectedObject is EntityAdapterEvent)
                        {
                            fullMethodName = ((EntityAdapterEvent)selectedObject).EntityAdapter.Name + "." + ((EntityAdapterEvent)selectedObject).Name;
                            wf = new Workflow(((EntityAdapterEvent)selectedObject).Partition) { Display = fullMethodName, Name = "WF_" + fullMethodName.Replace(".", "_"), IsOperationRelated = true };
                            ((EntityAdapterEvent)selectedObject).EntityAdapter.EntityAdapterDesignerRoot.Workflows.Add(wf);
                        }
                        else if (selectedObject is DomainServiceOperation)
                        {
                            fullMethodName = ((DomainServiceOperation)selectedObject).DomainServiceExtension.Name + "." + ((DomainServiceOperation)selectedObject).Name;
                            wf = new Workflow(((DomainServiceOperation)selectedObject).Partition) { Display = fullMethodName, Name = "WF_" + fullMethodName.Replace(".", "_"), IsOperationRelated = true };
                            ((DomainServiceOperation)selectedObject).DomainServiceExtension.EntityAdapterDesignerRoot.Workflows.Add(wf);
                        }

                        if (wf != null)
                            ((GenericOperation)selectedObject).Workflow = wf;

                        transaction.Commit();
                    }
                    break;
                }
                                

                if (selectedObject is DomainViewShape && command.CommandID.ID == cmdDomainValuesID)
                {
                    GeneratedCode.CustomizedCode.FrmDomainValues builder = new GeneratedCode.CustomizedCode.FrmDomainValues(((DomainViewShape)selectedObject).ModelElement as DomainView);
                    builder.ShowDialog();
                    break;
                }

                if (selectedObject is KeyPerformanceIndicatorShape && command.CommandID.ID == cmdKpiRangeValuesID)
                {
                    using (Transaction transaction =
                            this.CurrentEntityAdapterDesignerDocData.Store.TransactionManager.BeginTransaction("Change designer by KPI values."))
                    {
                        FrmKpiValues builder = new FrmKpiValues(((Linx.EntityAdapterDesigner.KeyPerformanceIndicatorShape)selectedObject).ModelElement as Linx.EntityAdapterDesigner.KeyPerformanceIndicator);
                        builder.ShowDialog();
                        transaction.Commit();
                    }
                    break;
                }

                if (selectedObject is EntityAdapterShape && (command.CommandID.ID == cmdServerEventsID || command.CommandID.ID == cmdidBuildEntityID || command.CommandID.ID == cmdDomainValuesID || command.CommandID.ID == cmdidExtendedFilterID || command.CommandID.ID == cmdidSortPropertyID || command.CommandID.ID == cmdClientEventsID))
                {

                    using (Transaction transaction =
                            this.CurrentEntityAdapterDesignerDocData.Store.TransactionManager.BeginTransaction("Change designer by entity builder."))
                    {
                        if (command.CommandID.ID == cmdServerEventsID)
                        {
                            CustomCode.FrmAddEntityEvents frmEvents = new CustomCode.FrmAddEntityEvents();
                            frmEvents.Entity = ((EntityAdapterShape)selectedObject).ModelElement as EntityAdapter;
                            frmEvents.ShowDialog();
                            transaction.Commit();
                        }

                        if (command.CommandID.ID == cmdClientEventsID)
                        {
                            CustomCode.FrmAddClientEvents frmEvents = new CustomCode.FrmAddClientEvents();
                            frmEvents.Entity = ((EntityAdapterShape)selectedObject).ModelElement as EntityAdapter;
                            frmEvents.ShowDialog();
                            transaction.Commit();
                        }

                        if (command.CommandID.ID == cmdidBuildEntityID)
                        {
                            EntityAdapter entity = ((EntityAdapterShape)selectedObject).ModelElement as EntityAdapter;

                            if (entity.EntityAdapterRepresentation != null)
                            {
                                CustomCode.FrmJoinEntityBuilder builder = new CustomCode.FrmJoinEntityBuilder();
                                builder.Entity = entity;
                                if (!builder.IsNull())
                                    builder.ShowDialog();
                            }
                            else if (entity.LocalEntityAdapter != null)
                            {
                                CustomCode.FrmLocalEntityBuilder builder = new CustomCode.FrmLocalEntityBuilder();
                                builder.Entity = entity;
                                if (!builder.IsNull())
                                    builder.ShowDialog();
                            }
                            else if (entity.GetOlapCatalog() != null)
                            {
                                CustomCode.FrmOlapBuilder builder = new CustomCode.FrmOlapBuilder();
                                builder.Entity = entity;
                                if (!builder.IsNull())
                                    builder.ShowDialog();
                            }
                            else
                            {
                                if (entity.IsModelView)
                                {
                                    entity.ConfigureBusinessView();
                                }
                                else
                                {
                                    CustomCode.FrmEntityBuilder builder = new CustomCode.FrmEntityBuilder();
                                    builder.Entity = entity;
                                    if (!builder.IsNull())
                                        builder.ShowDialog();
                                }
                            }

                            transaction.Commit();
                        }

                        if (command.CommandID.ID == cmdidExtendedFilterID)
                        {
                            if (((EntityAdapter)((EntityAdapterShape)selectedObject).ModelElement).PrimaryEntity.IsNullOrEmpty())
                            {
                                transaction.Rollback();
                                MessageBox.Show("This EntityAdapter is not configured yet. Configure this Entity before.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            CustomCode.FormAddExtendedFilter builder = new CustomCode.FormAddExtendedFilter();
                            builder.Entity = ((EntityAdapterShape)selectedObject).ModelElement as EntityAdapter;
                            if (!builder.IsNull())
                                builder.ShowDialog();
                            transaction.Commit();
                        }
                        if (command.CommandID.ID == cmdidSortPropertyID)
                        {
                            ((EntityAdapter)((EntityAdapterShape)selectedObject).ModelElement).ConfigPropertyOrder();
                            transaction.Commit();
                        }
                    }
                    break;
                }

                if (selectedObject is LookUpAdapterShape && command.CommandID.ID == cmdidBuildLookUpID)
                {

                    using (Transaction transaction =
                            this.CurrentEntityAdapterDesignerDocData.Store.TransactionManager.BeginTransaction("Change designer by LookUp builder."))
                    {
                        CustomCode.FrmLookUpBuilder builder = new CustomCode.FrmLookUpBuilder();
                        builder.LookUp = ((LookUpAdapterShape)selectedObject).ModelElement as LookUpAdapter;
                        if (!builder.IsNull())
                            builder.ShowDialog();
                        transaction.Commit();
                    }
                    break;
                }

                if ((command.CommandID.ID == cmdMoveUpID || command.CommandID.ID == cmdMoveDownID))
                {
                    if (selectedObject is LookUpProperty)
                    {
                        int index = ((LookUpProperty)selectedObject).LookUpAdapter.LookUpProperties.IndexOf(((LookUpProperty)selectedObject));
                        index += (command.CommandID.ID == cmdMoveUpID ? -1 : 1);
                        if (index >= 0 && index < ((LookUpProperty)selectedObject).LookUpAdapter.LookUpProperties.Count)
                        {
                            DiagramItem selectedItem = this.CurrentDocView.CurrentDiagram.ActiveDiagramView.Selection.FocusedItem;
                            this.CurrentDocView.CurrentDiagram.ActiveDiagramView.Selection.Clear();
                            using (Transaction transaction =
                                    this.CurrentEntityAdapterDesignerDocData.Store.TransactionManager.BeginTransaction("Move LookUpProperty Position."))
                            {
                                ((LookUpProperty)selectedObject).LookUpAdapter.LookUpProperties.Move(((LookUpProperty)selectedObject), index);
                                transaction.Commit();
                            }
                            this.CurrentDocView.CurrentDiagram.ActiveDiagramView.Selection.Set(selectedItem);
                        }
                        break;
                    }

                    if (selectedObject is EntityAdapterOperation)
                    {
                        int index = ((EntityAdapterOperation)selectedObject).EntityAdapter.EntityAdapterOperations.IndexOf(((EntityAdapterOperation)selectedObject));
                        index += (command.CommandID.ID == cmdMoveUpID ? -1 : 1);
                        if (index >= 0 && index < ((EntityAdapterOperation)selectedObject).EntityAdapter.EntityAdapterOperations.Count)
                        {
                            DiagramItem selectedItem = this.CurrentDocView.CurrentDiagram.ActiveDiagramView.Selection.FocusedItem;
                            this.CurrentDocView.CurrentDiagram.ActiveDiagramView.Selection.Clear();
                            using (Transaction transaction =
                                    this.CurrentEntityAdapterDesignerDocData.Store.TransactionManager.BeginTransaction("Move Operation Position."))
                            {
                                ((EntityAdapterOperation)selectedObject).EntityAdapter.EntityAdapterOperations.Move(((EntityAdapterOperation)selectedObject), index);
                                transaction.Commit();
                            }
                            this.CurrentDocView.CurrentDiagram.ActiveDiagramView.Selection.Set(selectedItem);
                        }
                        break;
                    }

                    if (selectedObject is EntityAdapterEvent)
                    {
                        int index = ((EntityAdapterEvent)selectedObject).EntityAdapter.EntityAdapterEvents.IndexOf(((EntityAdapterEvent)selectedObject));
                        index += (command.CommandID.ID == cmdMoveUpID ? -1 : 1);
                        if (index >= 0 && index < ((EntityAdapterEvent)selectedObject).EntityAdapter.EntityAdapterEvents.Count)
                        {
                            DiagramItem selectedItem = this.CurrentDocView.CurrentDiagram.ActiveDiagramView.Selection.FocusedItem;
                            this.CurrentDocView.CurrentDiagram.ActiveDiagramView.Selection.Clear();
                            using (Transaction transaction =
                                    this.CurrentEntityAdapterDesignerDocData.Store.TransactionManager.BeginTransaction("Move Event Position."))
                            {
                                ((EntityAdapterEvent)selectedObject).EntityAdapter.EntityAdapterEvents.Move(((EntityAdapterEvent)selectedObject), index);
                                transaction.Commit();
                            }
                            this.CurrentDocView.CurrentDiagram.ActiveDiagramView.Selection.Set(selectedItem);
                        }
                        break;
                    }
                }

                if (command.CommandID.ID == cmdUpdateReportID && selectedObject is EntityAdapterShape)
                {
                    EntityAdapter entity = ((EntityAdapter)((EntityAdapterShape)selectedObject).ModelElement);

                    if (!entity.IsNull())
                        entity.UpdateReport();

                    break;
                }

                if (command.CommandID.ID == cmdUpdateMasterDetailReportID && selectedObject is EntityAdapterShape)
                {
                    EntityAdapter entity = ((EntityAdapter)((EntityAdapterShape)selectedObject).ModelElement);

                    if (!entity.IsNull())
                        entity.UpdateMasterDetailReport();

                    break;
                }

                if (command.CommandID.ID == cmdOpenCustomValidationID)
                {

                    if (selectedObject is EntityAdapterShape)
                    {
                        EntityAdapter entity = ((EntityAdapter)((EntityAdapterShape)selectedObject).ModelElement);

                        if (entity.CustomValidationMethod.IsNullOrEmpty())
                        {
                            MessageBox.Show("The property [Custom Validation Method] is not defined for this element.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        using (Transaction transaction =
                            entity.Store.TransactionManager.BeginTransaction("Changing TargetNamespace."))
                        {
                            GenericOperation operation = new EntityAdapterOperation(entity.Store);
                            operation.OverloadName = entity.CustomValidationMethod;
                            operation.Access = OperationAccess.Public;
                            operation.IsStatic = true;
                            operation.Parameters = entity.Name + " elementForValidation#ValidationContext context";
                            operation.ReturnType = "ValidationResult";
                            operation.IsUniqueOverload = true;
                            operation.DocComment = "Operation for a custom validation.\r\n" +
                            "Parameters:\r\n" +
                            "		elementForValidation: Describes the element in which a validation is being performed.\r\n" +
                            "		context: Describes the context in which a validation is being performed.\r\n" +
                            "Example:\r\n" +
                            "		if ([Custom Condition])\r\n" +
                            "			return ValidationResult.Success;\r\n" +
                            "		else\r\n" +
                            "			return new ValidationResult(\"Message for presentation.\", new string[] { \"PropertyName\" });\r\n";
                            entity.EntityAdapterDesignerRoot.OpenCustomValidationOperation(operation);
                            transaction.Rollback();
                        }

                    }
                    else if (selectedObject is EntityAdapterProperty)
                    {
                        if (((EntityAdapterProperty)selectedObject).CustomValidationMethod.IsNullOrEmpty())
                        {
                            MessageBox.Show("The property [CustomValidationMethod] is not defined for this element.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        using (Transaction transaction =
                            ((EntityAdapterProperty)selectedObject).Store.TransactionManager.BeginTransaction("Changing TargetNamespace."))
                        {
                            GenericOperation operation = new EntityAdapterOperation(((EntityAdapterProperty)selectedObject).Store);
                            operation.OverloadName = ((EntityAdapterProperty)selectedObject).CustomValidationMethod;
                            operation.Access = OperationAccess.Public;
                            operation.IsStatic = true;
                            operation.Parameters = ((EntityAdapterProperty)selectedObject).Datatype + " elementForValidation#ValidationContext context";
                            operation.ReturnType = "ValidationResult";
                            operation.IsUniqueOverload = true;
                            operation.DocComment = "Operation for a custom validation.\r\n" +
                            "Parameters:\r\n" +
                            "		elementForValidation: Describes the element in which a validation is being performed.\r\n" +
                            "		context: Describes the context in which a validation is being performed.\r\n" +
                            "Example:\r\n" +
                            "		if ([Custom Condition])\r\n" +
                            "			return ValidationResult.Success;\r\n" +
                            "		else\r\n" +
                            "			return new ValidationResult(\"Message for presentation.\", new string[] { \"" + ((EntityAdapterProperty)selectedObject).Name + "\" });\r\n";
                            ((EntityAdapterProperty)selectedObject).EntityAdapter.EntityAdapterDesignerRoot.OpenCustomValidationOperation(operation);
                            transaction.Rollback();
                        }
                    }
                    else if (selectedObject is EntityAdapterFormula)
                    {
                        if (((EntityAdapterFormula)selectedObject).CustomValidationMethod.IsNullOrEmpty())
                        {
                            MessageBox.Show("The property [CustomValidationMethod] is not defined for this element.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        using (Transaction transaction =
                            ((EntityAdapterFormula)selectedObject).Store.TransactionManager.BeginTransaction("Changing TargetNamespace."))
                        {
                            GenericOperation operation = new EntityAdapterOperation(((EntityAdapterFormula)selectedObject).Store);
                            operation.OverloadName = ((EntityAdapterFormula)selectedObject).CustomValidationMethod;
                            operation.Access = OperationAccess.Public;
                            operation.IsStatic = true;
                            operation.Parameters = ((EntityAdapterFormula)selectedObject).Datatype + " elementForValidation#ValidationContext context";
                            operation.ReturnType = "ValidationResult";
                            operation.IsUniqueOverload = true;
                            operation.DocComment = "Operation for a custom validation.\r\n" +
                            "Parameters:\r\n" +
                            "		elementForValidation: Describes the element in which a validation is being performed.\r\n" +
                            "		context: Describes the context in which a validation is being performed.\r\n" +
                            "Example:\r\n" +
                            "		if ([Custom Condition])\r\n" +
                            "			return ValidationResult.Success;\r\n" +
                            "		else\r\n" +
                            "			return new ValidationResult(\"Message for presentation.\", new string[] { \"" + ((EntityAdapterFormula)selectedObject).Name + "\" });\r\n";
                            ((EntityAdapterFormula)selectedObject).EntityAdapter.EntityAdapterDesignerRoot.OpenCustomValidationOperation(operation);
                            transaction.Rollback();
                        }
                    }
                    break;
                }

            }
        }


    }
}
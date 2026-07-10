using System.Collections.Generic;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.Modeling.Shell;
using System;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Diagrams;
using System.Linq;
using EnvDTE;
using System.Windows.Forms;


namespace Linx.BusinessDataModelDesigner
{

    internal partial class BusinessDataModelDesignerCommandSet
    {
        private Guid cmdidBusinessDataModelDesignerToolWindowGUID = new Guid("{e024017a-a1a6-42ed-84d6-96b12b183a70}");
        private const uint cmdidBusinessDataModelDesignerToolWindow = 0x8700;

        private Guid cmdidRefreshDesignerGUID = new Guid("{e024017a-a1a6-42ed-84d6-96b12b183a71}");
        private const int cmdidRefreshDesignerID = 0x8800;

        private Guid cmdidReverseEngineerGUID = new Guid("{e024017a-a1a6-42ed-84d6-96b12b183a72}");
        private const int cmdidReverseEngineerID = 0x8801;

        private Guid cmdidGenerateScriptGUID = new Guid("{e024017a-a1a6-42ed-84d6-96b12b183a73}");
        private const int cmdidGenerateScriptID = 0x8802;

        private Guid cmdidAddExternalReferencesGUID = new Guid("{e024017a-a1a6-42ed-84d6-96b12b183a74}");
        private const int cmdidAddExternalReferencesID = 0x8803;

        private Guid cmdidSendToBackGUID = new Guid("{e024017a-a1a6-42ed-84d6-96b12b183a75}");
        private const int cmdidSendToBackID = 0x8804;

        private Guid cmdidBringToFrontGUID = new Guid("{e024017a-a1a6-42ed-84d6-96b12b183a76}");
        private const int cmdidBringToFrontID = 0x8805;

        private Guid cmdidArrangeLayoutGUID = new Guid("{e024017a-a1a6-42ed-84d6-96b12b183a77}");
        private const int cmdidArrangeLayoutID = 0x8806;

        private Guid cmdidFindElementGUID = new Guid("{e024017a-a1a6-42ed-84d6-96b12b183a78}");
        private const int cmdidFindElementID = 0x8807;

        private Guid cmdidOpenCodeGUID = new Guid("{e024017a-a1a6-42ed-84d6-96b12b183a79}");
        private const int cmdidOpenCodeID = 0x8808;

        private Guid cmdidConvertToModelCLassGUID = new Guid("{e024017a-a1a6-42ed-84d6-96b12b183a80}");
        private const int cmdidConvertToModelCLassID = 0x8809;

        private Guid cmdidOperationalEventsGUID = new Guid("{e024017a-a1a6-42ed-84d6-96b12b183a81}");
        private const int cmdidOperationalEventsID = 0x8810;

        private Guid cmdidRefreshAllDesignersGUID = new Guid("{e024017a-a1a6-42ed-84d6-96b12b183a82}");
        private const int cmdidRefreshAllDesignersID = 0x8811;

        private Guid cmdDomainValuesGUID = new Guid("{e024017a-a1a6-42ed-84d6-96b12b183a83}");
        private const int cmdDomainValuesID = 0x8812;

        private Guid cmdidConfigureBusinessViewGUID = new Guid("{e024017a-a1a6-42ed-84d6-96b12b183a84}");
        private const int cmdidConfigureBusinessViewID = 0x8813;

        private Guid cmdidStartEventsGUID = new Guid("{e024017a-a1a6-42ed-84d6-96b12b183a85}");
        private const int cmdidStartEventsID = 0x8814;

        private Guid cmdidPreviewEntityGUID = new Guid("{e024017a-a1a6-42ed-84d6-96b12b183a86}");
        private const int cmdidPreviewEntityID = 0x8815;

        private Guid cmdServerEventsGUID = new Guid("{e024017a-a1a6-42ed-84d6-96b12b173a87}");
        private const int cmdServerEventsID = 0x8816;
        
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
            new CommandID(cmdidRefreshDesignerGUID, cmdidRefreshDesignerID)));

            commands.Add(new DynamicStatusMenuCommand(
            new EventHandler(OnPopUpMenuDisplayAction),
            new EventHandler(OnPopUpMenuClick),
            new CommandID(cmdidReverseEngineerGUID, cmdidReverseEngineerID)));

            commands.Add(new DynamicStatusMenuCommand(
            new EventHandler(OnPopUpMenuDisplayAction),
            new EventHandler(OnPopUpMenuClick),
            new CommandID(cmdidGenerateScriptGUID, cmdidGenerateScriptID)));

            commands.Add(new DynamicStatusMenuCommand(
            new EventHandler(OnPopUpMenuDisplayAction),
            new EventHandler(OnPopUpMenuClick),
            new CommandID(cmdidAddExternalReferencesGUID, cmdidAddExternalReferencesID)));

            commands.Add(new DynamicStatusMenuCommand(
            new EventHandler(OnPopUpMenuDisplayAction),
            new EventHandler(OnPopUpMenuClick),
            new CommandID(cmdidSendToBackGUID, cmdidSendToBackID)));

            commands.Add(new DynamicStatusMenuCommand(
            new EventHandler(OnPopUpMenuDisplayAction),
            new EventHandler(OnPopUpMenuClick),
            new CommandID(cmdidBringToFrontGUID, cmdidBringToFrontID)));

            commands.Add(new DynamicStatusMenuCommand(
            new EventHandler(OnPopUpMenuDisplayAction),
            new EventHandler(OnPopUpMenuClick),
            new CommandID(cmdidArrangeLayoutGUID, cmdidArrangeLayoutID)));

            commands.Add(new DynamicStatusMenuCommand(
            new EventHandler(OnPopUpMenuDisplayAction),
            new EventHandler(OnPopUpMenuClick),
            new CommandID(cmdidFindElementGUID, cmdidFindElementID)));

            commands.Add(new DynamicStatusMenuCommand(
            new EventHandler(OnPopUpMenuDisplayAction),
            new EventHandler(OnPopUpMenuClick),
            new CommandID(cmdidOpenCodeGUID, cmdidOpenCodeID)));

            commands.Add(new DynamicStatusMenuCommand(
            new EventHandler(OnPopUpMenuDisplayAction),
            new EventHandler(OnPopUpMenuClick),
            new CommandID(cmdidConvertToModelCLassGUID, cmdidConvertToModelCLassID)));

            commands.Add(new DynamicStatusMenuCommand(
            new EventHandler(OnPopUpMenuDisplayAction),
            new EventHandler(OnPopUpMenuClick),
            new CommandID(cmdidOperationalEventsGUID, cmdidOperationalEventsID)));

            commands.Add(new DynamicStatusMenuCommand(
            new EventHandler(OnPopUpMenuDisplayAction),
            new EventHandler(OnPopUpMenuClick),
            new CommandID(cmdidRefreshAllDesignersGUID, cmdidRefreshAllDesignersID)));

            commands.Add(new DynamicStatusMenuCommand(
            new EventHandler(OnPopUpMenuDisplayAction),
            new EventHandler(OnPopUpMenuClick),
            new CommandID(cmdDomainValuesGUID, cmdDomainValuesID)));
            
            commands.Add(new DynamicStatusMenuCommand(
            new EventHandler(OnPopUpMenuDisplayAction),
            new EventHandler(OnPopUpMenuClick),
            new CommandID(cmdidConfigureBusinessViewGUID, cmdidConfigureBusinessViewID)));

            commands.Add(new DynamicStatusMenuCommand(
            new EventHandler(OnPopUpMenuDisplayAction),
            new EventHandler(OnPopUpMenuClick),
            new CommandID(cmdidStartEventsGUID, cmdidStartEventsID)));
            
            commands.Add(new DynamicStatusMenuCommand(
            new EventHandler(OnPopUpMenuDisplayAction),
            new EventHandler(OnPopUpMenuClick),
            new CommandID(cmdidPreviewEntityGUID, cmdidPreviewEntityID)));

            commands.Add(new DynamicStatusMenuCommand(
            new EventHandler(OnPopUpMenuDisplayAction),
            new EventHandler(OnPopUpMenuClick),
            new CommandID(cmdServerEventsGUID, cmdServerEventsID)));
            
            return commands;
        }


        internal void OnPopUpMenuDisplayAction(object sender, EventArgs e)
        {
            MenuCommand command = sender as MenuCommand;
            command.Visible = command.Enabled = false;

            foreach (object selectedObject in this.CurrentSelection)
            {
                if (selectedObject is BusinessDataModelDesignerDiagram && (command.CommandID.ID == cmdidOperationalEventsID || command.CommandID.ID == cmdidStartEventsID || command.CommandID.ID == cmdidRefreshAllDesignersID || command.CommandID.ID == cmdidGenerateScriptID || command.CommandID.ID == cmdidReverseEngineerID || command.CommandID.ID == cmdidAddExternalReferencesID || command.CommandID.ID == cmdidRefreshDesignerID || command.CommandID.ID == cmdidArrangeLayoutID || command.CommandID.ID == cmdidFindElementID))
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

                if (selectedObject is ShapeElement && ((ShapeElement)selectedObject).ParentShape is BusinessDataModelDesignerDiagram && (command.CommandID.ID == cmdidSendToBackID || command.CommandID.ID == cmdidBringToFrontID))
                {
                    command.Visible = true;
                    command.Enabled = true;
                    break;
                }

                if ((selectedObject is ClassShape || selectedObject is ClassOperation || selectedObject is ModelAttribute) && command.CommandID.ID == cmdidOpenCodeID)
                {
                    command.Visible = true;
                    command.Enabled = true;
                    break;
                }

                if (selectedObject is ClassShape && !(selectedObject is ReferenceModelClassShape) && (command.CommandID.ID == cmdidConfigureBusinessViewID) && ((ModelClass)((ClassShape)selectedObject).ModelElement).Kind == ClassKind.ModelView)
                {
                    command.Visible = true;
                    command.Enabled = true;
                    break;
                }

                if (selectedObject is ClassShape && (command.CommandID.ID == cmdidPreviewEntityID || (command.CommandID.ID == cmdServerEventsID && !(selectedObject is ReferenceModelClassShape))))
                {
                    command.Visible = true;
                    command.Enabled = true;
                    break;
                }

                if ((selectedObject is ReferenceModelClassShape) && command.CommandID.ID == cmdidConvertToModelCLassID)
                {
                    command.Visible = true;
                    command.Enabled = true;
                    break;
                }
            }
        }

        internal void OnPopUpMenuClick(object sender, EventArgs e)
        {
            MenuCommand command = sender as MenuCommand;

            foreach (object selectedObject in this.CurrentSelection)
            {

                if (selectedObject is BusinessDataModelDesignerDiagram)
                {
                    BusinessDataModelDesignerRoot designer = (BusinessDataModelDesignerRoot)((BusinessDataModelDesignerDiagram)selectedObject).ModelElement;
                    switch (command.CommandID.ID)
                    {
                        case cmdidOperationalEventsID:
                            designer.OpenOperationalEvents();
                            break;

                        case cmdidStartEventsID:
                            designer.OpenStartEvents();
                            break;

                        case cmdidRefreshAllDesignersID:
                            designer.OpenAllDesigners();
                            break;

                        case cmdidGenerateScriptID:
                            designer.GenerateScript(((BusinessDataModelDesignerDiagram)selectedObject));
                            break;

                        case cmdidReverseEngineerID:
                            designer.ExecuteReverseEngineer(((BusinessDataModelDesignerDiagram)selectedObject));
                            break;

                        case cmdidAddExternalReferencesID:
                            designer.AddExternalReferences();
                            break;

                        case cmdidRefreshDesignerID:
                            designer.Refresh();
                            break;

                        case cmdidArrangeLayoutID:
                            ((BusinessDataModelDesignerDiagram)selectedObject).ArrangeLayout();
                            break;

                        case cmdidFindElementID:
                            designer.FindElement();
                            break;
                        
                        default:
                            break;
                    }

                    break;
                }

                if (selectedObject is ClassShape && command.CommandID.ID == cmdidConfigureBusinessViewID)
                {
                    ((ModelClass)((ClassShape)selectedObject).ModelElement).ConfigureBusinessView();
                    break;
                }

                if (selectedObject is ClassShape && command.CommandID.ID == cmdidPreviewEntityID)
                {
                    ((ModelClass)((ClassShape)selectedObject).ModelElement).PreViewEntity();
                    break;
                }

                if (selectedObject is ClassShape && command.CommandID.ID == cmdServerEventsID)
                {
                    CustomCode.FrmAddEntityEvents frmEvents = new CustomCode.FrmAddEntityEvents();
                    frmEvents.Entity = ((ClassShape)selectedObject).ModelElement as ModelClass;
                    frmEvents.ShowDialog();
                    break;
                }

                if (selectedObject is DomainViewShape && command.CommandID.ID == cmdDomainValuesID)
                {
                    Linx.BusinessDataModelDesigner.CustomCode.FrmDomainValues builder = new Linx.BusinessDataModelDesigner.CustomCode.FrmDomainValues(((DomainViewShape)selectedObject).ModelElement as DomainView);
                    builder.ShowDialog();
                    break;
                }

                if ((selectedObject is ClassShape || selectedObject is ClassOperation || selectedObject is ModelAttribute) && command.CommandID.ID == cmdidOpenCodeID)
                {
                    selectedObject.OpenCode();
                    break;
                }

                if ((selectedObject is ReferenceModelClassShape) && command.CommandID.ID == cmdidConvertToModelCLassID)
                {
                    ((ReferenceModelClass)((ReferenceModelClassShape)selectedObject).ModelElement).ConvertToModelCLass();
                    break;
                }

                if (selectedObject is ShapeElement && ((ShapeElement)selectedObject).ParentShape is BusinessDataModelDesignerDiagram && (command.CommandID.ID == cmdidSendToBackID || command.CommandID.ID == cmdidBringToFrontID))
                {
                    BusinessDataModelDesignerRoot designer = (BusinessDataModelDesignerRoot)(((ShapeElement)selectedObject).ParentShape).ModelElement;

                    switch (command.CommandID.ID)
                    {
                        case cmdidSendToBackID:
                            designer.SendToBack((ShapeElement)selectedObject);
                            break;

                        case cmdidBringToFrontID:
                            designer.BringToFront((ShapeElement)selectedObject);
                            break;

                        default:
                            break;
                    }
                    break;
                }

            }
        }
    }
}
using System.Collections.Generic;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.Modeling.Shell;
using System;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Diagrams;
using System.Linq;
using Linx.Tools;

namespace Linx.EntityAdapterDesigner
{

    internal partial class EntityAdapterDesignerCommandSet
    {
        private Guid cmdidBuildEntityGUID = new Guid("{e024017a-a1a6-42ed-84d6-96b12b173a71}");
        private const int cmdidBuildEntityID = 0x8800;

        private Guid cmdMoveUpGUID = new Guid("{e024017a-a1a6-42ed-84d6-96b12b173a72}");
        private const int cmdMoveUpID = 0x8801;

        private Guid cmdMoveDownGUID = new Guid("{e024017a-a1a6-42ed-84d6-96b12b173a73}");
        private const int cmdMoveDownID = 0x8802;

        private Guid cmdAttributeOrderGUID = new Guid("{e024017a-a1a6-42ed-84d6-96b12b173a74}");
        private const int cmdAttributeOrderID = 0x8803;

        public void SelectShape(ModelElement modelElement)
        {
            ShapeElement modelElementShape = PresentationViewsSubject.GetPresentation(modelElement).FirstOrDefault() as ShapeElement;

            Diagram diagram = modelElement.Store.ElementDirectory.AllElements.OfType<Diagram>().FirstOrDefault();

            DiagramItem diagramItem = new DiagramItem(modelElementShape);

            diagram.ActiveDiagramView.Selection.Set(diagramItem);
        }



        protected override System.Collections.Generic.IList<System.ComponentModel.Design.MenuCommand> GetMenuCommands()
        {
            IList<MenuCommand> commands = base.GetMenuCommands();

            //Add new commands
            commands.Add(new DynamicStatusMenuCommand(
            new EventHandler(OnPopUpMenuDisplayAction),
            new EventHandler(OnPopUpMenuClick),
            new CommandID(cmdidBuildEntityGUID, cmdidBuildEntityID)));

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
            new CommandID(cmdAttributeOrderGUID, cmdAttributeOrderID)));

            return commands;
        }

        internal void OnPopUpMenuDisplayAction(object sender, EventArgs e)
        {
            MenuCommand command = sender as MenuCommand;
            command.Visible = command.Enabled = false;

            foreach (object selectedObject in this.CurrentSelection)
            {
                if (selectedObject is EntityAdapterShape && (command.CommandID.ID == cmdidBuildEntityID || command.CommandID.ID == cmdAttributeOrderID))
                {
                    command.Visible = true;
                    command.Enabled = true;
                    break;
                }

                if ((selectedObject is EntityAdapterEvent || selectedObject is EntityAdapterOperation) && (command.CommandID.ID == cmdMoveUpID || command.CommandID.ID == cmdMoveDownID))
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
                if (selectedObject is EntityAdapterShape && (command.CommandID.ID == cmdidBuildEntityID || command.CommandID.ID == cmdAttributeOrderID))
                {
                    using (Transaction transaction =
                            this.CurrentEntityAdapterDesignerDocData.Store.TransactionManager.BeginTransaction("Change designer by entity builder."))
                    {
                        if (command.CommandID.ID == cmdidBuildEntityID)
                        {
                            CustomCode.FrmEntityBuilder builder = new CustomCode.FrmEntityBuilder();
                            builder.Entity = ((EntityAdapterShape)selectedObject).ModelElement as EntityAdapter;
                            builder.ShowDialog();
                            transaction.Commit();
                        }
                        else if (command.CommandID.ID == cmdAttributeOrderID)
                        {
                            CustomCode.FrmAttributesOrderBuilder builder = new CustomCode.FrmAttributesOrderBuilder();
                            builder.Entity = ((EntityAdapterShape)selectedObject).ModelElement as EntityAdapter;
                            builder.ShowDialog();
                            transaction.Commit();
                        }
                    }
                    break;
                }

                if ((command.CommandID.ID == cmdMoveUpID || command.CommandID.ID == cmdMoveDownID))
                {

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
            }
        }


    }
}
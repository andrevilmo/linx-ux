using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Design;
using Microsoft.VisualStudio.Modeling.Diagrams;
using System.Collections.Generic;
using System.Linq;


// This sample allows users to re-order items in a compartment shape by dragging.
// This example is built on the "Class Diagrams" solution template of VMSDK (DSL Tools).
// You will need to change the following domain class names to your own:
// ClassShape = a compartment shape
// ClassModelElement = the domain class displayed using a ClassShape
// This code assumes that the embedding relationships displayed in the compartments
// don't use inheritance (don't have base or derived domain relationships).

namespace Linx.BusinessModelDesigner
{
    /// <summary>
    /// Manage the mouse while dragging a compartment item.
    /// </summary>
    public class CompartmentDragMouseAction : MouseAction
    {
        private ModelElement sourceChild;
        private ClassShape sourceShape;
        private RectangleD sourceCompartmentBounds;

        public CompartmentDragMouseAction(ModelElement sourceChildElement, ClassShape sourceParentShape, RectangleD bounds)
            : base(sourceParentShape.Diagram)
        {
            sourceChild = sourceChildElement;
            sourceShape = sourceParentShape;
            sourceCompartmentBounds = bounds; // For cursor.
        }

        /// <summary>
        /// Call back to the source shape to drop the dragged item.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(DiagramMouseEventArgs e)
        {
            base.OnMouseUp(e);
            sourceShape.DoMouseUp(sourceChild, e);
            this.Cancel(e.DiagramClientView);
            e.Handled = true;
        }

        /// <summary>
        /// Ideally, this shouldn't happen. This action should only be active
        /// while the mouse is still pressed. However, it can happen if you
        /// move the mouse rapidly out of the source shape, let go, and then 
        /// click somewhere else in the source shape. Yuk.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(DiagramMouseEventArgs e)
        {
            base.OnMouseDown(e);
            this.Cancel(e.DiagramClientView);
            e.Handled = false;
        }

        /// <summary>
        /// Display an appropriate cursor while the drag is in progress:
        /// Up-down arrow if we are inside the original compartment.
        /// No entry if we are elsewhere.
        /// </summary>
        /// <param name="currentCursor"></param>
        /// <param name="diagramClientView"></param>
        /// <param name="mousePosition"></param>
        /// <returns></returns>
        public override System.Windows.Forms.Cursor GetCursor(System.Windows.Forms.Cursor currentCursor, DiagramClientView diagramClientView, PointD mousePosition)
        {
            // If the cursor is inside the original compartment, show up-down cursor.
            return sourceCompartmentBounds.Contains(mousePosition)
             ? System.Windows.Forms.Cursors.SizeNS // Up-down arrow.
             : System.Windows.Forms.Cursors.No;
        }
    }

    /// <summary>
    /// Override some methods of the compartment shape.
    /// *** GenerateDoubleDerived must be set for this shape in DslDefinition.dsl. ****
    /// </summary>
    public partial class ClassShape
    {
        /// <summary>
        /// Model element that is being dragged.
        /// </summary>
        private static ClassModelElement dragStartElement = null;
        /// <summary>
        /// Absolute bounds of the compartment, used to set the cursor.
        /// </summary>
        private static RectangleD compartmentBounds;

        /// <summary>
        /// Attach mouse listeners to the compartments for the shape.
        /// This is called once per compartment shape.
        /// The base method creates the compartments for this shape.
        /// </summary>
        public override void EnsureCompartments()
        {
            base.EnsureCompartments();
            foreach (Compartment compartment in this.NestedChildShapes.OfType<Compartment>())
            {
                compartment.MouseDown += new DiagramMouseEventHandler(compartment_MouseDown);
                compartment.MouseUp += new DiagramMouseEventHandler(compartment_MouseUp);
                compartment.MouseMove += new DiagramMouseEventHandler(compartment_MouseMove);
            }
        }


        /// <summary>
        /// Remember which item the mouse was dragged from.
        /// We don't create an Action immediately, as this would inhibit the
        /// inline text editing feature. Instead, we just remember the details
        /// and will create an Action when/if the mouse moves off this list item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void compartment_MouseDown(object sender, DiagramMouseEventArgs e)
        {
            dragStartElement = e.HitDiagramItem.RepresentedElements.OfType<ClassModelElement>().FirstOrDefault();
            compartmentBounds = e.HitDiagramItem.Shape.AbsoluteBoundingBox;
        }

        /// <summary>
        /// When the mouse moves away from the initial list item, but still inside the compartment,
        /// create an Action to supervise the cursor and handle subsequent mouse events.
        /// Transfer the details of the initial mouse position to the Action.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void compartment_MouseMove(object sender, DiagramMouseEventArgs e)
        {
            if (dragStartElement != null)
            {
                if (dragStartElement != e.HitDiagramItem.RepresentedElements.OfType<ClassModelElement>().FirstOrDefault())
                {
                    e.DiagramClientView.ActiveMouseAction = new CompartmentDragMouseAction(dragStartElement, this, compartmentBounds);
                    dragStartElement = null;
                }
            }
        }

        /// <summary>
        /// User has released the mouse button. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void compartment_MouseUp(object sender, DiagramMouseEventArgs e)
        {
            dragStartElement = null;
        }

        /// <summary>
        /// Forget the source item if mouse up occurs outside the
        /// compartment.
        /// </summary>
        /// <param name="e"></param>
        public override void OnMouseUp(DiagramMouseEventArgs e)
        {
            base.OnMouseUp(e);
            dragStartElement = null;
        }


        /// <summary>
        /// Called by the Action when the user releases the mouse.
        /// If we are still on the same compartment but in a different list item,
        /// move the starting item to the position of the current one.
        /// </summary>
        /// <param name="dragFrom"></param>
        /// <param name="e"></param>
        public void DoMouseUp(ModelElement dragFrom, DiagramMouseEventArgs e)
        {
            // Original or "from" item:
            ClassModelElement dragFromElement = dragFrom as ClassModelElement;
            // Current or "to" item:
            ClassModelElement dragToElement = e.HitDiagramItem.RepresentedElements.OfType<ClassModelElement>().FirstOrDefault();
            if (dragFromElement != null && dragToElement != null)
            {
                // Find the common parent model element, and the relationship links:
                ElementLink parentToLink = GetEmbeddingLink(dragToElement);
                ElementLink parentFromLink = GetEmbeddingLink(dragFromElement);
                if (parentToLink != parentFromLink && parentFromLink != null && parentToLink != null)
                {
                    // Get the static relationship and role (= end of relationship):
                    DomainRelationshipInfo relationshipFrom = parentFromLink.GetDomainRelationship();
                    DomainRoleInfo parentFromRole = relationshipFrom.DomainRoles[0];
                    // Get the node in which the element is embedded, usually the element displayed in the shape:
                    ModelElement parentFrom = parentFromLink.LinkedElements[0];

                    // Same again for the target:
                    DomainRelationshipInfo relationshipTo = parentToLink.GetDomainRelationship();
                    DomainRoleInfo parentToRole = relationshipTo.DomainRoles[0];
                    ModelElement parentTo = parentToLink.LinkedElements[0];

                    // Mouse went down and up in same parent and same compartment:
                    if (parentTo == parentFrom && relationshipTo == relationshipFrom)
                    {
                        // Find index of target position:
                        int newIndex = 0;
                        var elementLinks = parentToRole.GetElementLinks(parentTo);
                        foreach (ElementLink link in elementLinks)
                        {
                            if (link == parentToLink) { break; }
                            newIndex++;
                        }

                        if (newIndex < elementLinks.Count)
                        {
                            using (Transaction t = parentFrom.Store.TransactionManager.BeginTransaction("Move list item"))
                            {
                                parentFromLink.MoveToIndex(parentFromRole, newIndex);
                                t.Commit();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Get the embedding link to this element.
        /// Assumes there is no inheritance between embedding relationships.
        /// (If there is, you need to make sure you've got the relationship
        /// that is represented in the shape compartment.)
        /// </summary>
        /// <param name="child"></param>
        /// <returns></returns>
        ElementLink GetEmbeddingLink(ClassModelElement child)
        {
            foreach (DomainRoleInfo role in child.GetDomainClass().AllEmbeddedByDomainRoles)
            {
                foreach (ElementLink link in role.OppositeDomainRole.GetElementLinks(child))
                {
                    // Just the assume the first embedding link is the only one.
                    // Not a valid assumption if one relationship is derived from another.
                    return link;
                }
            }
            return null;
        }
    }
}
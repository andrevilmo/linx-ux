using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Diagrams;
using Microsoft.VisualStudio.Modeling.Integration;
using Microsoft.VisualStudio.Modeling.Integration.Shell;
using Microsoft.VisualStudio.Modeling.Shell;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Linx.BusinessDataModelDesigner.ModelBusAdapters
{
	[CLSCompliant(false)]
	public class StandardVsModelingDiagramView : VsModelingView
	{
		private Microsoft.VisualStudio.Modeling.Diagrams.Diagram diagram;

		private Microsoft.VisualStudio.Modeling.Shell.VSDiagramView vsDiagramView;

		public Microsoft.VisualStudio.Modeling.Diagrams.Diagram Diagram
		{
			get
			{
				return this.diagram;
			}
		}

		public Microsoft.VisualStudio.Modeling.Shell.VSDiagramView VSDiagramView
		{
			get
			{
				return this.vsDiagramView;
			}
		}

		public StandardVsModelingDiagramView(ModelBusAdapter ownerAdapter, ModelBusReference viewReference) : base(ownerAdapter, viewReference)
		{
			DiagramDocView docView = base.GetDocView() as DiagramDocView;
			if (docView == null || docView.CurrentDiagram == null || docView.CurrentDesigner == null)
			{
				throw new ViewOperationException("VSModelBusExceptionMessages.CannotCreateView(base.Adapter.DisplayName)");
			}
			this.diagram = docView.CurrentDiagram;
			this.vsDiagramView = docView.CurrentDesigner;
		}

		private ShapeElement ObtainShapeFromReference(ModelBusReference reference)
		{
			ModelElement modelElement = null;
			ShapeElement shapeElement = null;
			modelElement = base.Adapter.ResolveElementReference(reference) as ModelElement;
			shapeElement = modelElement as ShapeElement;
			if (modelElement != null && shapeElement == null)
			{
				shapeElement = PresentationViewsSubject.GetPresentation(modelElement).FirstOrDefault<PresentationElement>() as ShapeElement;
			}
			return shapeElement;
		}

		public override void SetSelection(ModelBusReference reference)
		{
			ShapeElement shapeElement = this.ObtainShapeFromReference(reference);
			if (shapeElement != null)
			{
				base.GetDocView().Show();
				this.VSDiagramView.Selection.Set(new DiagramItem(shapeElement));
			}
		}

		public override void SetSelection(IEnumerable<ModelBusReference> references)
		{
			DiagramItemCollection diagramItemCollections = new DiagramItemCollection();
			ShapeElement shapeElement = null;
			foreach (ModelBusReference reference in references)
			{
				shapeElement = this.ObtainShapeFromReference(reference);
				if (shapeElement == null)
				{
					continue;
				}
				diagramItemCollections.Add(new DiagramItem(shapeElement));
			}
			if (diagramItemCollections.Count > 0)
			{
				base.GetDocView().Show();
				this.VSDiagramView.Selection.Set(diagramItemCollections);
			}
		}
	}
}
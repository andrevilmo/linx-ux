using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Modeling.Integration;
using Microsoft.VisualStudio.Modeling.Shell;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;

namespace Linx.BusinessDataModelDesigner.ModelBusAdapters
{
    [CLSCompliant(false)]
    public abstract class VsModelingView : ModelBusView
    {
        private Guid logicalViewId;

        private VsModelingDocumentHandler DocHandler
        {
            get
            {
                return ((ModelingAdapter)base.Adapter).DocumentHandler as VsModelingDocumentHandler;
            }
        }

        public override bool IsOperational
        {
            get
            {
                return !base.Adapter.Disposed;
            }
        }

        protected VsModelingView(ModelBusAdapter ownerAdapter, ModelBusReference viewReference)
            : base(ownerAdapter, viewReference)
        {
            this.InitializeView();
        }

        public override void Close()
        {
            ModelingDocView docView = this.GetDocView();
            if (docView != null)
            {
                if (docView.Frame == null)
                {
                    docView.Dispose();
                }
                else
                {
                    Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(docView.Frame.CloseFrame(65792));
                }
                docView = null;
            }
        }

        protected ModelingDocView GetDocView()
        {
            if (base.Adapter.Disposed)
            {
                throw new ViewOperationException("VSModelBusExceptionMessages.AdapterAlreadyDisposed");
            }
            ModelingDocView modelingDocView = this.DocHandler.GetModelingDocView(this.logicalViewId);
            if (modelingDocView == null)
            {
                throw new ViewOperationException("VSModelBusExceptionMessages.CannotCreateView(base.Adapter.DisplayName)");
            }
            return modelingDocView;
        }

        public override void Hide()
        {
            this.GetDocView().Hide();
        }

        private void InitializeView()
        {
            this.Validate();
            this.InitializeViewId();
            this.GetDocView();
        }

        private void InitializeViewId()
        {
            ModelingAdapterReference adapterReference = base.ViewReference.AdapterReference as ModelingAdapterReference;
            if (string.IsNullOrEmpty(adapterReference.ViewId))
            {
                this.logicalViewId = VSConstants.LOGVIEWID_Primary;
                return;
            }
            try
            {
                this.logicalViewId = new Guid(adapterReference.ViewId);
            }
            catch
            {
                throw new ModelBusReferenceFormatException("ModelBusExceptionMessages.ModelBusReferenceNotValid");
            }
        }

        public override void Open()
        {
            this.GetDocView();
        }

        public override void Show()
        {
            this.GetDocView().Show();
        }

        private void Validate()
        {
            if (base.ViewReference == null || string.IsNullOrEmpty(base.ViewReference.LogicalAdapterId) || !base.ViewReference.IsAdapterReferenceResolved)
            {
                throw new ModelBusReferenceFormatException("ModelBusExceptionMessages.ModelBusReferenceNotValid");
            }
            ModelingAdapterReference adapterReference = base.ViewReference.AdapterReference as ModelingAdapterReference;
            ModelingAdapter adapter = base.Adapter as ModelingAdapter;
            ModelingAdapterReference modelingAdapterReference = base.Adapter.Reference.AdapterReference as ModelingAdapterReference;
            if (adapterReference != null && adapter != null && modelingAdapterReference != null)
            {
                if (string.IsNullOrEmpty(adapterReference.AbsoluteTargetPath))
                {
                    throw new ModelBusReferenceFormatException("ModelBusExceptionMessages.ModelBusReferenceNotValid");
                }
                if (object.Equals(adapterReference.AbsoluteTargetPath, modelingAdapterReference.AbsoluteTargetPath))
                {
                    VsModelingDocumentHandler documentHandler = adapter.DocumentHandler as VsModelingDocumentHandler;
                    if (documentHandler != null && documentHandler.ModelingDocData != null)
                    {
                        return;
                    }
                }
            }
            throw new AdapterNotSupportedException("VSModelBusExceptionMessages.CannotCreateView(base.Adapter.DisplayName)");
        }
    }
}
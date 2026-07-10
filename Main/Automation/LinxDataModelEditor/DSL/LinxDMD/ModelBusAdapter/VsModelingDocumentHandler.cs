using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Integration;
using Microsoft.VisualStudio.Modeling.Shell;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Linx.BusinessDataModelDesigner.ModelBusAdapters
{
	internal class VsModelingDocumentHandler : ModelingDocumentHandler, IVsDocumentLockHolder
	{
		private IRelatedDocList relatedDocList;

		internal Microsoft.VisualStudio.Modeling.Shell.ModelingDocData ModelingDocData
		{
			get;
			private set;
		}

		public override ModelElement Root
		{
			get
			{
				if (this.ModelingDocData == null)
				{
					return null;
				}
				return this.ModelingDocData.RootElement;
			}
		}

		internal uint VsDocDataCookie
		{
			get;
			private set;
		}

		internal IVsHierarchy VsDocDataHierarchy
		{
			get;
			private set;
		}

		internal uint VsDocDataItemId
		{
			get;
			private set;
		}

		internal uint VsDocHandlerCookie
		{
			get;
			private set;
		}

		private VsModelingDocumentHandler(Microsoft.VisualStudio.Modeling.Shell.ModelingDocData docData, IVsHierarchy vsDocDataHierarchy, uint vsDocDataItemId, uint vsDocDataCookie)
		{
			if (docData == null)
			{
				throw new ArgumentNullException("docData");
			}
			if (vsDocDataHierarchy == null)
			{
				throw new ArgumentNullException("vsDocDataHierarchy");
			}			
			if (vsDocDataCookie == 0)
			{
				throw new ArgumentNullException("vsDocDataCookie");
			}
			this.ModelingDocData = docData;
			this.VsDocDataHierarchy = vsDocDataHierarchy;
			this.VsDocDataItemId = vsDocDataItemId;
			this.VsDocDataCookie = vsDocDataCookie;
			if (docData != null && !string.IsNullOrEmpty(docData.FileName))
			{
				base.ModelFile = Path.GetFullPath(docData.FileName);
			}
		}

		internal void AddRelatedSaveItem(System.IServiceProvider serviceProvider)
		{
			if (serviceProvider == null)
			{
				throw new ArgumentNullException("serviceProvider");
			}
			if (this.relatedDocList != null)
			{
				throw new AdapterCreationException("VSModelBusExceptionMessages.CannotAddRelatedSaveItem");
			}
			IRelatedDocList service = serviceProvider.GetService(typeof(SRelatedDocList)) as IRelatedDocList;
			if (service == null || this.ModelingDocData == null)
			{
				throw new AdapterCreationException("VSModelBusExceptionMessages.CannotAddRelatedSaveItem");
			}
			service.RegisterDependentDocument(this.ModelingDocData);
			this.relatedDocList = service;
		}

		public int CloseDocumentHolder(uint dwSaveOptions)
		{
			return -2147467263;
		}

		private static VsModelingDocumentHandler CreateDocDataAndModelingDocHandler(string targetDocumentPath, System.IServiceProvider vsServiceProvider)
		{
			IVsProject vsProject;
			string str;
			IVsEditorFactory vsEditorFactory;
			uint num;
			VsModelingDocumentHandler vsModelingDocumentHandler = null;
			Microsoft.VisualStudio.Modeling.Shell.ModelingDocData modelingDocDatum = null;
			IVsHierarchy vsHierarchy = null;
			uint num1 = 0;
			IVsUIShellOpenDocument service = vsServiceProvider.GetService(typeof(SVsUIShellOpenDocument)) as IVsUIShellOpenDocument;
			if (service == null)
			{
				throw new InvalidOperationException("VSModelBusExceptionMessages.ServiceTypeNotFound(typeof(SVsUIShellOpenDocument).Name, vsServiceProvider.GetType().Name)");
			}
			IVsUIHierarchy vsUIHierarchy = null;
			uint num2 = 0;
			Microsoft.VisualStudio.OLE.Interop.IServiceProvider serviceProvider = null;
			int num3 = 0;
			Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(service.IsDocumentInAProject(targetDocumentPath, out vsUIHierarchy, out num2, out serviceProvider, out num3));
			if (num3 == 0)
			{
				IVsExternalFilesManager vsExternalFilesManager = vsServiceProvider.GetService(typeof(SVsExternalFilesManager)) as IVsExternalFilesManager;
				if (vsExternalFilesManager == null)
				{
					throw new InvalidOperationException("VSModelBusExceptionMessages.ServiceTypeNotFound(typeof(SVsExternalFilesManager).Name, vsServiceProvider.GetType().Name)");
				}
				Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(vsExternalFilesManager.GetExternalFilesProject(out vsProject));
				if (vsProject == null)
				{
					throw new InvalidOperationException("VSModelBusExceptionMessages.CannotFindContainingProjectItems(null)");
				}
				Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(vsExternalFilesManager.TransferDocument(null, targetDocumentPath, null));
				Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(service.IsDocumentInAProject(targetDocumentPath, out vsUIHierarchy, out num2, out serviceProvider, out num3));
				if (num3 == 0)
				{
					throw new InvalidOperationException("VSModelBusExceptionMessages.CannotFindContainingProjectItems(null)");
				}
				vsHierarchy = vsUIHierarchy;
				num1 = num2;
			}
			else
			{
				vsHierarchy = vsUIHierarchy;
				num1 = num2;
			}
			IVsRunningDocumentTable runningDocumentTableService = VsModelingDocumentHandler.GetRunningDocumentTableService(vsServiceProvider);
			ModelingEditorFactory modelingEditorFactory = null;
			Guid empty = Guid.Empty;
			Guid lOGVIEWIDPrimary = VSConstants.LOGVIEWID_Primary;
			Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(service.GetStandardEditorFactory(0, ref empty, targetDocumentPath, ref lOGVIEWIDPrimary, out str, out vsEditorFactory));
			if (vsEditorFactory == null)
			{
				throw new InvalidOperationException("VSModelBusExceptionMessages.EditorNotFound(targetDocumentPath)");
			}
			modelingEditorFactory = vsEditorFactory as ModelingEditorFactory;
			if (modelingEditorFactory == null)
			{
				throw new InvalidOperationException("VSModelBusExceptionMessages.EditorNotFound(targetDocumentPath)");
			}
			modelingDocDatum = modelingEditorFactory.CreateDocData(targetDocumentPath, vsHierarchy, num1);
			if (modelingDocDatum == null)
			{
				throw new InvalidOperationException("VSModelBusExceptionMessages.DocDataNotFound(targetDocumentPath, modelingEditorFactory.GetType().FullName)");
			}
			uint num4 = 0;
			IntPtr unknownForObject = Marshal.GetIUnknownForObject(modelingDocDatum);
			Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(runningDocumentTableService.RegisterAndLockDocument(2, targetDocumentPath, vsHierarchy, num1, unknownForObject, out num));
			if (num == 0)
			{
				uint num5 = 258;
				if (num != 0)
				{
					Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(runningDocumentTableService.UnlockDocument(num5, num));
				}
				throw new InvalidOperationException("VSModelBusExceptionMessages.UnknownErrorOccured");
			}
			try
			{
				modelingDocDatum.LoadDocData(targetDocumentPath);
				vsModelingDocumentHandler = new VsModelingDocumentHandler(modelingDocDatum, vsHierarchy, num1, num);
				Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(runningDocumentTableService.RegisterDocumentLockHolder(0, num, vsModelingDocumentHandler, out num4));
				if (num4 == 0)
				{
					throw new InvalidOperationException("VSModelBusExceptionMessages.UnknownErrorOccured");
				}
				vsModelingDocumentHandler.VsDocHandlerCookie = num4;
			}
			catch
			{
				if (num != 0)
				{
					Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(runningDocumentTableService.UnlockDocument(2, num));
				}
				throw;
			}
			return vsModelingDocumentHandler;
		}

		internal static VsModelingDocumentHandler CreateInstance(string filePath, System.IServiceProvider vsServiceProvider)
		{
			if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
			{
				throw new ArgumentException("ModelBusExceptionMessages.InvalidModelFilePath");
			}
			if (vsServiceProvider == null)
			{
				throw new ArgumentNullException("vsServiceProvider");
			}
			VsModelingDocumentHandler vsModelingDocumentHandler = VsModelingDocumentHandler.CreateModelingDocHandlerIfDocDataOpen(filePath, vsServiceProvider, true);
			if (vsModelingDocumentHandler != null)
			{
				return vsModelingDocumentHandler;
			}
			vsModelingDocumentHandler = VsModelingDocumentHandler.CreateDocDataAndModelingDocHandler(filePath, vsServiceProvider);
			return vsModelingDocumentHandler;
		}

		private static VsModelingDocumentHandler CreateModelingDocHandlerIfDocDataOpen(string filePath, System.IServiceProvider vsServiceProvider, bool closeIncompatibleDocData)
		{
			IntPtr zero = IntPtr.Zero;
			uint num = 0;
			Microsoft.VisualStudio.Modeling.Shell.ModelingDocData objectForIUnknown = null;
			VsModelingDocumentHandler vsModelingDocumentHandler = null;
			IVsHierarchy vsHierarchy = null;
			uint num1 = 0;
			IVsRunningDocumentTable runningDocumentTableService = VsModelingDocumentHandler.GetRunningDocumentTableService(vsServiceProvider);
			if (runningDocumentTableService.FindAndLockDocument(0, filePath, out vsHierarchy, out num1, out zero, out num) != 0 || !(zero != IntPtr.Zero))
			{
				return null;
			}
			objectForIUnknown = Marshal.GetObjectForIUnknown(zero) as Microsoft.VisualStudio.Modeling.Shell.ModelingDocData;
			Marshal.Release(zero);
			if (objectForIUnknown != null)
			{
				uint num2 = 0;
				vsModelingDocumentHandler = new VsModelingDocumentHandler(objectForIUnknown, vsHierarchy, num1, num);
				Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(runningDocumentTableService.RegisterDocumentLockHolder(0, num, vsModelingDocumentHandler, out num2));
				Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(runningDocumentTableService.LockDocument(2, num));
				vsModelingDocumentHandler.VsDocHandlerCookie = num2;
				return vsModelingDocumentHandler;
			}
			if (!closeIncompatibleDocData)
			{
				return null;
			}
			IVsRunningDocumentTable2 vsRunningDocumentTable2 = (IVsRunningDocumentTable2)runningDocumentTableService;
			int num3 = 0;
			if (vsRunningDocumentTable2 == null)
			{
				throw new InvalidOperationException("VSModelBusExceptionMessages.ServiceTypeNotFound(typeof(SVsRunningDocumentTable).Name, vsServiceProvider.GetType().Name)");
			}
			if (vsRunningDocumentTable2.QueryCloseRunningDocument(filePath, out num3) != 0 || num3 == 0)
			{
				throw new InvalidOperationException("VSModelBusExceptionMessages.CannotCloseIncompatibleDocData");
			}
			return null;
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
                
                    if (this.relatedDocList == null)
                    {
                        this.ReleaseEditLock(1024);
                    }
                    else
                    {
                        this.relatedDocList.UnregisterDependentDocument(this.ModelingDocData);
                        this.ReleaseEditLock(256);
                    }
                    IVsRunningDocumentTable globalService = Package.GetGlobalService(typeof(SVsRunningDocumentTable)) as IVsRunningDocumentTable;
                    if (globalService != null)
                    {
                        globalService.UnregisterDocumentLockHolder(this.VsDocHandlerCookie);
                    }
                    this.ModelingDocData = null;
                    base.ModelFile = null;
                    this.VsDocDataCookie = 0;
                    this.VsDocHandlerCookie = 0;
                    this.VsDocDataItemId = 0;
                    this.VsDocDataHierarchy = null;
                
			}
		}

		internal ModelingDocView GetModelingDocView(Guid viewId)
		{
			Microsoft.VisualStudio.OLE.Interop.IServiceProvider serviceProvider;
			object obj;
			ModelingDocView modelingDocView = null;
			IVsWindowFrame vsWindowFrame = null;
			IVsUIHierarchy vsUIHierarchy = null;
			uint num = 0;
			IVsUIShellOpenDocument service = this.ModelingDocData.GetService(typeof(SVsUIShellOpenDocument)) as IVsUIShellOpenDocument;
			if (service == null)
			{
				throw new InvalidOperationException("VSModelBusExceptionMessages.ServiceTypeNotFound(typeof(SVsUIShellOpenDocument).Name, this.ModelingDocData.GetType().Name)");
			}
			Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(service.OpenDocumentViaProject(base.ModelFile, ref viewId, out serviceProvider, out vsUIHierarchy, out num, out vsWindowFrame));
			if (vsWindowFrame != null)
			{
				Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(vsWindowFrame.GetProperty(-3011, out obj));
				modelingDocView = obj as ModelingDocView;
			}
			return modelingDocView;
		}

		private static IVsRunningDocumentTable GetRunningDocumentTableService(System.IServiceProvider vsServiceProvider)
		{
			IVsRunningDocumentTable service = vsServiceProvider.GetService(typeof(SVsRunningDocumentTable)) as IVsRunningDocumentTable;
			if (service == null)
			{
				throw new InvalidOperationException("VSModelBusExceptionMessages.ServiceTypeNotFound(typeof(SVsRunningDocumentTable).Name, vsServiceProvider.GetType().Name)");
			}
			return service;
		}

		private void ReleaseEditLock(uint saveFlag)
		{
			if (this.ModelingDocData != null && this.VsDocDataCookie != 0)
			{
				IVsRunningDocumentTable runningDocumentTableService = VsModelingDocumentHandler.GetRunningDocumentTableService(this.ModelingDocData);
				uint num = 2 | saveFlag;
				Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(runningDocumentTableService.UnlockDocument(num, this.VsDocDataCookie));
			}
		}

		public int ShowDocumentHolder()
		{
			return -2147467263;
		}
	}
}
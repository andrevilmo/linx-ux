using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Linx.Tools;
using EnvDTE;
using System.IO;
using System.Windows.Forms;
using Tools.XmlConfigMerge;
using Microsoft.VisualStudio.Modeling.Diagrams;
using Microsoft.VisualStudio.Modeling;
using Linx.EntityAdapterDesigner.CustomizedCode;
using Linx.EntityAdapterDesigner.CustomizedCode.Apps.SPA;

namespace Linx.EntityAdapterDesigner
{
    public partial class EntityAdapterUserInterface
    {
        public bool HasPendingChanges { get; set; }

        public PublicationEntity GetEntityAdapter()
        {
            Subscription subscription = null;
            try { subscription = this.Subscription; }
            catch { return null; }

            if (subscription != null)
            {
                return this.GetSubscriptedEntity();
            }
            else
            {
                if (this.EntityAdapter != null)
                    return this.EntityAdapter.ToPublicationEntity();
                else
                {
                    var baseClass = this.BaseUserInterface;
                    while (baseClass != null)
                    {
                        if (baseClass.EntityAdapter != null)
                            return baseClass.EntityAdapter.ToPublicationEntity();
                        baseClass = baseClass.BaseUserInterface;
                    }
                }
            }

            return null;
        }

        public EntityAdapter GetDirectEntityAdapter()
        {
            if (this.EntityAdapter != null)
                return this.EntityAdapter;
            else
            {
                var baseClass = this.BaseUserInterface;
                while (baseClass != null)
                {
                    if (baseClass.EntityAdapter != null)
                        return baseClass.EntityAdapter;
                    baseClass = baseClass.BaseUserInterface;
                }
            }

            return null;
        }

        public bool ExistsClientEvent(string eventName)
        {
            if (this.UserInterfaceClientEvented.Any(e => e.Name == eventName))
                return true;
            else
            {
                var baseClass = this.BaseUserInterface;
                while (baseClass != null)
                {
                    if (baseClass.UserInterfaceClientEvented.Any(e => e.Name == eventName))
                        return true;
                    baseClass = baseClass.BaseUserInterface;
                }
            }

            return false;
        }

        public List<UserInterfaceClientEvent> GetUserInterfaceClientEvented()
        {
            List<UserInterfaceClientEvent> result = new List<UserInterfaceClientEvent>();
            result.AddRange(this.UserInterfaceClientEvented);

            var baseClass = this.BaseUserInterface;
            while (baseClass != null)
            {
                foreach (var cEvent in baseClass.UserInterfaceClientEvented)
                {
                    if (!result.Any(e => e.Name == cEvent.Name))
                        result.Add(cEvent);
                }
                baseClass = baseClass.BaseUserInterface;
            }

            return result;
        }

        public bool HasOlapSource()
        {
            return (this.EntityAdapter != null && this.EntityAdapter.IsOlap());
        }

        public PublicationEntity GetSubscriptedEntity()
        {
            return this.EntityAdapterDesignerRoot.GetPublishedEntityByRef(this.Subscription.BusinessObjectPath, this.SubscriptionNameSpace, this.SubscriptionEntityAdapterName);
        }

        public void SelectRepresentedEntity()
        {
            if (this.Subscription != null)
            {
                FormPublishedEntityList preview = new FormPublishedEntityList() { UserInterface = this };
                preview.ShowDialog();
            }
        }

        public void CheckSize()
        {
            var shape = PresentationViewsSubject.GetPresentation(this).FirstOrDefault() as EntityAdapterUserInterfaceShape;
            if (shape != null)
            {               
                if (shape.Size.Width < 1.5)
                    shape.Size = new SizeD(2, 0.8);
            }
        }

        public void AddClientEvent(string eventName, bool exposeVM)
        {
            UserInterfaceClientEvent customEvent = new UserInterfaceClientEvent(this.Partition);
            customEvent.Name = eventName;
            customEvent.OverloadName = eventName;
            customEvent.IsUniqueOverload = true;
            customEvent.Access = OperationAccess.Public;
            customEvent.ReturnType = "void";
            customEvent.Parameters = "";
            customEvent.ExposedByViewModel = exposeVM;
            
            switch (eventName)
            {
                case "OnWizardStepChanging":
                    customEvent.ReturnType = "bool";
                    customEvent.Parameters = "int oldIndex#int newIndex#string id";
                    break;
                case "OnWizardStepChanged":
                    customEvent.Parameters = "int oldIndex#int newIndex#string id";
                    break;
                case "OnWizardFinalizing":
                    customEvent.ReturnType = "bool";
                    customEvent.Parameters = "string id";
                    break;
                case "OnWizardFinalized":
                    customEvent.Parameters = "string id";
                    break;    
                case "OnSearching":
                    customEvent.ReturnType = "string";                    
                    break;
                case "OnReporting":
                    customEvent.ReturnType = "string";
                    customEvent.Parameters = "string reportName";
                    customEvent.ExposedByViewModel = true;
                    break;
                case "OnLoadedChildUI":
                    customEvent.Parameters = "object childVM";
                    customEvent.ExposedByViewModel = true;
                    break;
                case "OnTabActive":
                    customEvent.Parameters = "string tabName";
                    break;
                case "OnDataGridCreated":
                    customEvent.Parameters = "string dataGridName";
                    customEvent.ExposedByViewModel = true;
                    break;
                case "OnDataGridRowChecked":
                    customEvent.Parameters = "string dataGridName#object[] selectedRows";
                    customEvent.ExposedByViewModel = true;
                    break;
                case "OnToolbarAction":
                    customEvent.ReturnType = "bool";
                    customEvent.Parameters = "string action";
                    customEvent.DocComment = "Actions: Open, Close, Export, Next, Back, First, Last, Edit, Save, Query, SpecialQuery, Refresh, Delete, Add, Undo, Report, ShowFeed, Clear, TableView, ShowCurrentFilter, ImportPhoto";
                    break;
                case "OnClearing":
                case "OnEditing":                
                case "OnPrinting":
                case "OnCancelling":
                case "OnControlGotFocus":
                case "OnClosing":
                    customEvent.ReturnType = "bool";
                    break;
                case "OnSaving":
                    customEvent.ReturnType = "bool";
                    customEvent.Parameters = "object[] changes";
                    break;
                case "OnSaved":
                    customEvent.Parameters = "object[] changes";
                    break;                    
                case "OnNavigating":
                    customEvent.ReturnType = "bool";
                    customEvent.Parameters = "int currentIndex#int nextIndex";
                    break;
                case "OnNavigaed":
                    customEvent.Parameters = "int currentIndex";
                    break;
                default:                  
                    break;
            }
            
            //Add event
            this.UserInterfaceClientEvented.Add(customEvent);
        }

        public void AdjustColorShape()
        {
            var shape = PresentationViewsSubject.GetPresentation(this).FirstOrDefault() as EntityAdapterUserInterfaceShape;
            if (shape != null)
            {
                if (this.BaseUserInterface != null)
                {
                    shape.SetOutlineColor(System.Drawing.Color.Black);
                    shape.SetTextColor(System.Drawing.Color.Black);
                    shape.OutlineDashStyle = System.Drawing.Drawing2D.DashStyle.Dash;                    
                }
                else
                {
                    shape.SetOutlineColor(System.Drawing.Color.Transparent);
                    shape.SetTextColor(System.Drawing.Color.White);
                    shape.OutlineDashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                }
                if (shape.Size.Width < 2)
                    shape.Size = new SizeD(2, 0.8);
            }
        }

        public string GetEntityClassInfoValue()
        {
            return (this.BaseUserInterface != null ? "Base Type: " + this.BaseUserInterface.Name : String.Empty);
        }

        public CustomizedLayoutV2 GetNewLayoutDefinition()
        {
            if (!this.LayoutContent.IsNullOrEmpty())
            {
                CustomizedLayoutV2 layout = EntityAdapterUserInterface.GetLayoutDefinition(this.LayoutContent);
                layout.CheckVersion();
                return layout;
            }
            else
                return null;
        }

        public void StoreCurrentlayout(CustomizedLayoutV2 layoutDefinition, bool reset = false)
        {
            try
            {
                var currentLayout = this;
                if (currentLayout.IsDeleted)
                    currentLayout = null;

                if (!currentLayout.IsNull() && !layoutDefinition.IsNull())
                {
                    currentLayout.AdjustExternaUIs(layoutDefinition);
                    string content = (reset ? String.Empty : SerializationManager<CustomizedLayoutV2>.ObjectToJson(layoutDefinition));
                    if (reset || content != currentLayout.LayoutContent)
                    {
                        using (Microsoft.VisualStudio.Modeling.Transaction transaction =
                                currentLayout.Store.TransactionManager.BeginTransaction("Change dynamic layout."))
                        {
                            currentLayout.LayoutContent = content;
                            transaction.Commit();
                        }
                    }
                }
            }
            catch { }
        }


        public static CustomizedLayoutV2 GetLayoutDefinition(string layoutContent)
        {
            CustomizedLayoutV2 layoutInstance;

            try
            {
                if (layoutContent.IsNullOrEmpty())
                    layoutInstance = null;
                else
                {
                    if (layoutContent[0] == '<')
                        layoutInstance = Linx.Tools.SerializationManager<CustomizedLayoutV2>.StringToObject(layoutContent);
                    else
                        layoutInstance = Linx.Tools.SerializationManager<CustomizedLayoutV2>.JsonToObject(layoutContent);
                }
            }
            catch
            {
                layoutInstance = null;
            }

            return layoutInstance;
        }

        public CustomizedLayoutV2 LayoutDefinition
        {
            get
            {
                if (!this.LayoutContent.IsNullOrEmpty())
                {
                    CustomizedLayoutV2 layout = EntityAdapterUserInterface.GetLayoutDefinition(this.LayoutContent);
                    layout.CheckVersion();
                    return layout;
                }
                else
                    return null;
            }
        }

        public bool GetIsBlockadeValue()
        {
            bool result;

            try
            {
                result = !(this.EntityAdapterDesignerRoot.GetSolutionName().ToLower() == this.SolutionName.ToLower());
            }
            catch { result = true; }

            return result;
        }
        
        public void AdjustExternaUIs(CustomizedLayoutV2 layoutDefinition)
        {
            foreach (LayoutContainer container in layoutDefinition.GetLayoutElementsByClass("ExternalUI"))
            {
                container.SpecializedFilterEntityName = String.Empty;
                container.SpecializedFilterRelationName = String.Empty;
                PublicationEntity entity = null;
                if (!container.ShareParentBO && !container.ParentFieldsRelation.IsNullOrEmpty() && !container.DetailFieldsRelation.IsNullOrEmpty())
                {
                    string parentField = container.ParentFieldsRelation.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
                    if (!parentField.IsNullOrEmpty())
                    {
                        if (container.ParentSelectorDataName.IsNullOrEmpty())
                        {
                            entity = this.GetEntityAdapter();
                            if (entity != null)
                                container.SpecializedFilterEntityName = entity.Name;
                        }
                        else
                        {
                            LayoutContainer selector = layoutDefinition.GetContainerByDefinedUserName(container.ParentSelectorDataName);
                            if (selector != null && selector.Controls.Count > 0)
                            {
                                string bindingPath = selector.Controls.Where(e => !e.BindingPath.IsNullOrEmpty()).Select(e => e.BindingPath).FirstOrDefault();
                                if (!bindingPath.IsNullOrEmpty())
                                {
                                    string[] bindingparts = bindingPath.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                                    if (bindingparts.Length > 1)
                                    {
                                        string dataView = bindingparts[bindingparts.Length - 2];
                                        if (dataView == "DataView")
                                        {
                                            entity = this.GetEntityAdapter();
                                        }
                                        else if (!dataView.IsNullOrEmpty())
                                        {
                                            dataView = dataView.Left("PagedList");
                                            if (!dataView.IsNullOrEmpty())
                                            {
                                                entity = this.GetEntityAdapter().GetDetailByName(dataView);
                                            }
                                        }

                                        if (entity != null)
                                            container.SpecializedFilterEntityName = entity.Name;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        #region Files Rules

        public void RenameSpaSourceFiles(string oldName)
        {
            if (!oldName.IsNullOrEmpty() && oldName != this.Name)
            {
                var vmName = this.EntityAdapterDesignerRoot.SpaCodeGen.GetSpaViewModelName(this);
                ProjectItem item = this.EntityAdapterDesignerRoot.SpaCodeGen.GetSpaAppFolder("viewmodels");
                if (!item.IsNull())
                {
                    var fileItem = EntityAdapterDesignerRoot.GetProjectItemByName(item.ProjectItems, oldName + ".js");
                    if (fileItem != null)
                        fileItem.Name = vmName + ".js";
                    fileItem = EntityAdapterDesignerRoot.GetProjectItemByName(item.ProjectItems, oldName + "Complement.js");
                    if (fileItem != null)
                        fileItem.Name = vmName + "Complement.js";
                }

                item = this.EntityAdapterDesignerRoot.SpaCodeGen.GetSpaAppFolder("views");
                if (!item.IsNull())
                {
                    var fileItem = EntityAdapterDesignerRoot.GetProjectItemByName(item.ProjectItems, oldName + ".html");
                    if (fileItem != null)
                        fileItem.Name = vmName + ".html";
                }
            }
        }

        public void DeleteSpaSourceFiles()
        {
            var vmName = this.EntityAdapterDesignerRoot.SpaCodeGen.GetSpaViewModelName(this);
            ProjectItem item = this.EntityAdapterDesignerRoot.SpaCodeGen.GetSpaAppFolder("viewmodels");
            if (!item.IsNull())
            {
                var fileItem = EntityAdapterDesignerRoot.GetProjectItemByName(item.ProjectItems, vmName + ".js");
                if (fileItem != null)
                    fileItem.Delete();
                fileItem = EntityAdapterDesignerRoot.GetProjectItemByName(item.ProjectItems, vmName + "Complement.js");
                if (fileItem != null)
                    fileItem.Delete();
            }

            item = this.EntityAdapterDesignerRoot.SpaCodeGen.GetSpaAppFolder("views");
            if (!item.IsNull())
            {
                var fileItem = EntityAdapterDesignerRoot.GetProjectItemByName(item.ProjectItems, vmName + ".html");
                if (fileItem != null)
                    fileItem.Delete();
            }
        }


        //public void RenameMobileSourceFiles(string oldName)
        //{
        //    if (!oldName.IsNullOrEmpty() && oldName != this.Name)
        //    {                
        //        ProjectItem item = this.EntityAdapterDesignerRoot.MobileCodeGen.GetMobileAppFolder("controllers");
        //        if (!item.IsNull())
        //        {
        //            var controllerName = this.EntityAdapterDesignerRoot.MobileCodeGen.GetMobileControllerName(this);
        //            var fileItem = EntityAdapterDesignerRoot.GetProjectItemByName(item.ProjectItems, oldName.ToCamelCase() + "Controller.js");
        //            if (fileItem != null)
        //                fileItem.Name = controllerName + ".js";
        //            fileItem = EntityAdapterDesignerRoot.GetProjectItemByName(item.ProjectItems, oldName.ToCamelCase() + "ControllerComplement.js");
        //            if (fileItem != null)
        //                fileItem.Name = controllerName + "Complement.js";

        //        }
                                
        //        item = this.EntityAdapterDesignerRoot.MobileCodeGen.GetMobileAppFolder("views");
        //        if (!item.IsNull())
        //        {
        //            var viewName = this.EntityAdapterDesignerRoot.MobileCodeGen.GetMobileViewName(this);
        //            var fileItem = EntityAdapterDesignerRoot.GetProjectItemByName(item.ProjectItems, oldName.ToCamelCase() + "View.html");
        //            if (fileItem != null)
        //                fileItem.Name = viewName + ".html";
        //        }
        //    }
        //}

        //public void DeleteMobileSourceFiles()
        //{            
        //    ProjectItem item = this.EntityAdapterDesignerRoot.MobileCodeGen.GetMobileAppFolder("controllers");            
        //    if (!item.IsNull())
        //    {
        //        var controllerName = this.EntityAdapterDesignerRoot.MobileCodeGen.GetMobileControllerName(this);
        //        var fileItem = EntityAdapterDesignerRoot.GetProjectItemByName(item.ProjectItems, controllerName + ".js");
        //        if (fileItem != null)
        //            fileItem.Delete();
        //        fileItem = EntityAdapterDesignerRoot.GetProjectItemByName(item.ProjectItems, controllerName + "Complement.js");
        //        if (fileItem != null)
        //            fileItem.Delete();
        //    }

        //    item = this.EntityAdapterDesignerRoot.MobileCodeGen.GetMobileAppFolder("views");
        //    if (!item.IsNull())
        //    {
        //        var viewName = this.EntityAdapterDesignerRoot.MobileCodeGen.GetMobileViewName(this);
        //        var fileItem = EntityAdapterDesignerRoot.GetProjectItemByName(item.ProjectItems, viewName + ".html");
        //        if (fileItem != null)
        //            fileItem.Delete();
        //    }
        //}

        #endregion
        
    }
}

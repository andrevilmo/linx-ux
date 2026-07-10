using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Linx.Tools
{
    #region Structure V2

    #region Enum´s

    public enum CharTypeEnum
    {
        Area,
        Bar,
        Bubble,
        CandleStick,
        Doughnut,
        HorizontalBar,
        HorizontalStackedBar,
        HorizontalStackedBar100,
        Line,
        Pie,
        Range,
        ScatterPoint,
        Spline,
        SplineArea,
        SplineRange,
        StackedArea,
        StackedArea100,
        StackedBar,
        StackedBar100,
        StackedLine,
        StackedSpline,
        StackedSplineArea,
        StackedSplineArea100,
        StepArea,
        StepLine,
        Stick,
        Scatter,
        BarStack,
        BarLine
    }

    public enum PivotScope
    {
        OnlyTable,
        OnlyChart,
        TableAndChart
    }

    public enum ContainerStyle
    {
        Normal,
        NoBorder
    }
    public enum FontForegroundStyle
    {
        Normal,
        Muted,
        Warning,
        Error,
        Info,
        Success
    }

    public enum FontBackground
    {
        Normal,
        Highlight
    }

    public enum LabelPosition
    {
        Top, Left
    }

    public enum ControlWidth
    {
        Automatic,
        ExtraSmall,
        Small,
        Medium,
        Large,
        ExtraLarge,
        [EnumMember(Value = "ExtraLarge")]
        Fluid,
        Mini,
        MinMedium,
        ExtraMedium
    }
    public enum MediaWidth
    {
        Small,
        Medium,
        Large
    }
    public enum GridSizeWidth
    {
        Small,
        Medium,
        Large
    }
    public enum GridSizeHeight
    {
        Small,
        Medium,
        Large,
        Auto
    }

    public enum VisibleFieldGrid
    {
        Both,
        Grid,
        Editor
    }

    #endregion Enum´s

    #region CustomizedLayoutV2

    public partial class CustomizedLayoutV2
    {
        public string Name { get; set; }
        public bool EnableTopScroll { get; set; }
        public bool EnableMedias { get; set; }
        public bool RemoveDataToolbar { get; set; }
        public bool RemoveViewSwitch { get; set; }
        #region ToolbarControl
        private bool _canClear = true;
        public bool CanClear { get { return _canClear; } set { _canClear = value; } }
        private bool _canSearch = true;
        public bool CanSearch { get { return _canSearch; } set { _canSearch = value; } }
        private bool _canAddNew = true;
        public bool CanAddNew { get { return _canAddNew; } set { _canAddNew = value; } }
        private bool _canEdit = true;
        public bool CanEdit { get { return _canEdit; } set { _canEdit = value; } }
        private bool _canDelete = true;
        public bool CanDelete { get { return _canDelete; } set { _canDelete = value; } }
        private bool _canCustomSearch = true;
        public bool CanCustomSearch { get { return _canCustomSearch; } set { _canCustomSearch = value; } }
        private bool _canPrint = true;
        public bool CanPrint { get { return _canPrint; } set { _canPrint = value; } }
        private bool _canLayout = true;
        public bool CanLayout { get { return _canLayout; } set { _canLayout = value; } }
        private bool _canNavigate = true;
        public bool CanNavigate { get { return _canNavigate; } set { _canNavigate = value; } }
        private bool _canExport = true;
        public bool CanExport { get { return _canExport; } set { _canExport = value; } }
        #endregion
        private static string _datatypeStatic = string.Empty;
        private string flddatatype = string.Empty;
        public string DataType
        {
            get
            {
                return flddatatype;
            }
            set
            {
                flddatatype = value;
                _datatypeStatic = value;
            }
        }
        public LayoutContainer DataGridViewLayout { get; set; }
        private int _version = 5;
        public int Version { get { return _version; } set { _version = value; } }

        private bool _ShowAllLabelsForConnectedFields = true;
        public bool ShowAllLabelsForConnectedFields { get { return _ShowAllLabelsForConnectedFields; } set { _ShowAllLabelsForConnectedFields = value; } }

        public string[] MetaDataKeys { get; set; }

        private List<LayoutContainer> _LayContainer;
        public List<LayoutContainer> Containers
        {
            get
            {
                if (_LayContainer == null)
                    _LayContainer = new List<LayoutContainer>();

                return _LayContainer;
            }
        }
        public bool IsSecundary { get; set; }

        public CustomizedLayoutV2 GetDataGridLayout()
        {
            //Get Grid Layout
            var gridLayout = new CustomizedLayoutV2();
            gridLayout.CopyInstanceFrom(this);
            gridLayout.Containers.Add(this.GetDataGridLayoutContainer());

            return gridLayout;
        }

        private LayoutContainer GetDataGridLayoutContainer()
        {
            var drContainer = new LayoutContainer() { ClassName = "DataGrid", IsTemplate = false, IsEditable = false, CanAddNew = false, CanDelete = false, CanEdit = false, Name = "", BindingPath = "DataElement.DataView" };

            LayoutControlV2 lControl;
            int orderIndex;

            var layoutParentControls = this.GetTabularControls(drContainer.BindingPath);
            foreach (var frmControl in layoutParentControls)
            {
                if (String.IsNullOrWhiteSpace(frmControl.DataGridOrder) || !int.TryParse(frmControl.DataGridOrder, out orderIndex))
                    orderIndex = layoutParentControls.IndexOf(frmControl);

                lControl = new LayoutControlV2();
                lControl.CopyInstanceFrom(frmControl);
                lControl.ParentName = drContainer.Name;
                lControl.DataGridOrder = orderIndex.ToString();
                lControl.OrderIndex = orderIndex;
                lControl.Sync = true;
                drContainer.Controls.Add(lControl);
            }

            return drContainer;
        }

        public string GetPrimaryKeyNameByEntity(string entityName)
        {
            if (MetaDataKeys != null && MetaDataKeys.Length > 0)
            {
                var key = entityName.IsNullOrEmpty() ? MetaDataKeys[0] : MetaDataKeys.FirstOrDefault(e => e.Left(":") == entityName);
                if (!key.IsNullOrEmpty())
                {
                    var parts = key.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 3)
                        return parts[1];
                }
            }

            return String.Empty;
        }

        public string GetPrimaryKeyTypeByEntity(string entityName)
        {
            if (MetaDataKeys != null && MetaDataKeys.Length > 0)
            {
                var key = entityName.IsNullOrEmpty() ? MetaDataKeys[0] : MetaDataKeys.FirstOrDefault(e => e.Left(":") == entityName);
                if (!key.IsNullOrEmpty())
                {
                    var parts = key.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 3)
                        return parts[2];
                }
            }

            return String.Empty;
        }

        private List<LayoutElement> _RemovedLayoutElements;
        public List<LayoutElement> RemovedLayoutElements
        {
            get
            {
                if (_RemovedLayoutElements == null)
                    _RemovedLayoutElements = new List<LayoutElement>();

                return _RemovedLayoutElements;
            }
        }


        public void CheckVersion()
        {
            if (Version < 4)
            {
                ShowAllLabelsForConnectedFields = false;

                //Adjust new version
                Version = 5;
            }


        }

        public void ResetSync()
        {
            foreach (LayoutContainer container in this.Containers)
            {
                container.ResetSync();
            }
            RemoveExpandersTMP();

        }

        public void SetDerived()
        {
            Action<LayoutElement> action = null;

            action = element =>
                {
                    element.IsDerived = true;
                    if (element is LayoutContainer)
                        ((LayoutContainer)element).Controls.ForEach(control => action(control));
                };

            this.Containers.ForEach(e => action(e));
            this.RemovedLayoutElements.ForEach(e => action(e));
        }

        public void MergeLayout(CustomizedLayoutV2 layout)
        {
            //Get layout top settings
            this.EnableTopScroll = layout.EnableTopScroll;
            this.RemoveDataToolbar = layout.RemoveDataToolbar;
            this.RemoveViewSwitch = layout.RemoveViewSwitch;
            this.CanClear = layout.CanClear;
            this.CanSearch = layout.CanSearch;
            this.CanAddNew = layout.CanAddNew;
            this.CanEdit = layout.CanEdit;
            this.CanDelete = layout.CanDelete;
            this.CanCustomSearch = layout.CanCustomSearch;
            this.CanPrint = layout.CanPrint;
            this.CanLayout = layout.CanLayout;
            this.CanNavigate = layout.CanNavigate;
            this.CanExport = layout.CanExport;

            //Merge internal elements
            MergeLayout(layout.Containers, this.Containers);

            LayoutContainer fromContainer = new LayoutContainer() { IsDerived = true, Name = "DeletedRoot" };
            fromContainer.Controls = layout.RemovedLayoutElements;
            LayoutContainer toContainer = new LayoutContainer() { IsDerived = true, Name = "DeletedRoot" };
            toContainer.Controls = this.RemovedLayoutElements;

            MergeLayout((new LayoutContainer[] { fromContainer }).ToList(), (new LayoutContainer[] { toContainer }).ToList());
        }


        private int GetValidIndex(int index, int count)
        {
            if (index > count)
                return count;
            else if (index < 0)
                return 0;
            else
                return index;
        }

        public void MergeLayout(List<LayoutContainer> fromContainers, List<LayoutContainer> toContainers)
        {
            Action<LayoutElement, LayoutContainer> action = null;

            action = (element, parent) =>
            {
                if (!element.IsDerived)
                {
                    if (parent != null)
                    {
                        LayoutContainer cntParent = this.GetContainerByName(parent.Name, toContainers);
                        if (cntParent != null)
                            cntParent.Controls.Insert(GetValidIndex(parent.Controls.IndexOf(element), cntParent.Controls.Count), element);
                    }
                    else if (element is LayoutContainer)
                    {
                        toContainers.Insert(GetValidIndex(fromContainers.IndexOf((LayoutContainer)element), fromContainers.Count), (LayoutContainer)element);
                    }
                }
                else
                {
                    LayoutElement adfElement = null;
                    if (element is LayoutContainer)
                        adfElement = this.GetContainerByName(element.Name, toContainers);
                    else if (element is LayoutControlV2)
                        adfElement = this.GetControlByName(element.Name, toContainers);

                    if (adfElement != null)
                        adfElement.IsVisible = element.IsVisible;
                }

                if (element.IsDerived && element is LayoutContainer)
                    ((LayoutContainer)element).Controls.ForEach(control => action(control, element as LayoutContainer));

            };

            fromContainers.ForEach(e => action(e, null));
        }

        private Boolean CanRemoveContainer(LayoutContainer layoutContainer)
        {
            foreach (var item in layoutContainer.Controls)
            {
                if (item is LayoutContainer)
                    return CanRemoveContainer((LayoutContainer)item);
                else
                    return false;
            }
            return true;
        }

        private void RemoveExpandersTMP()
        {
            List<LayoutElement> lTMP = new List<LayoutElement>();

            foreach (var item in this.RemovedLayoutElements)
            {
                if (item is LayoutContainer)
                    if (item.Name.Contains("Expander"))
                        lTMP.Add((LayoutContainer)item);
            }

            foreach (var item in lTMP)
                this.RemovedLayoutElements.Remove(item);

        }

        public void AdjustSyncCopies(LayoutControlV2 originalControl)
        {
            foreach (var container in this.Containers)
                container.AdjustSyncCopies(originalControl);
        }

        public void RemoveNoSyncElements()
        {
            foreach (LayoutContainer container in this.Containers)
                container.RemoveNoSyncElements();
        }

        public void RemoveControl(LayoutControlV2 control)
        {
            foreach (var item in this.Containers)
                item.RemoveControl(control);

        }

        public void MoveToRemovedControls(LayoutControlV2 control)
        {
            RemoveControl(control);
            this.RemovedLayoutElements.Add(control);
        }

        public void MoveToRemovedContainer(LayoutContainer container)
        {
            RemoveContainerByName(container.Name);
            this.RemovedLayoutElements.Add(container);
        }

        public bool RestoreLayoutElement(LayoutElement control)
        {

            LayoutContainer container = GetContainerByName(control.ParentName);

            if (!container.IsNull())
                container.Controls.Add(control);

            this.RemovedLayoutElements.Remove(control);

            return true;
        }

        public LayoutContainer GetContainerByName(string containerName)
        {
            return (LayoutContainer)GetContainerByElementName(containerName, null, this.Containers);
        }

        public LayoutContainer GetContainerByName(string containerName, List<LayoutContainer> containers)
        {
            return (LayoutContainer)GetContainerByElementName(containerName, null, containers);
        }

        public LayoutControlV2 GetControlByName(string controlName)
        {
            return GetControlByName(controlName, this.Containers);
        }

        public LayoutControlV2 GetContainerControlByName(LayoutContainer container, string controlName)
        {
            return container.Controls.Where(e => e is LayoutControlV2 && e.Name == controlName).Select(e => (LayoutControlV2)e).FirstOrDefault();
        }

        public LayoutControlV2 GetControlByBindingPath(string bindingPath, bool isMedia = false)
        {
            return GetControlByBindingPath(this.Containers, bindingPath, isMedia);
        }


        public List<LayoutElement> GetLayoutElementsByClass(string className)
        {
            List<LayoutElement> result = new List<LayoutElement>();

            Action<LayoutElement> finder = null;
            finder = (n) =>
                {
                    if (n.ClassName == className)
                        result.Add(n);

                    if (n is LayoutContainer)
                    {
                        foreach (LayoutElement element in ((LayoutContainer)n).Controls)
                            finder(element);
                    }

                };

            foreach (LayoutElement element in this.Containers)
                finder(element);

            return result;
        }

        public List<LayoutElement> GetLayoutElementsWithCustomAggregation()
        {
            List<LayoutElement> result = new List<LayoutElement>();

            Action<LayoutElement> finder = null;
            finder = (n) =>
            {
                if (n is LayoutControlV2 && ((LayoutControlV2)n).AggregationFunction.ToUpper() == "CUSTOM")
                    result.Add(n);

                if (n is LayoutContainer)
                {
                    foreach (LayoutElement element in ((LayoutContainer)n).Controls)
                        finder(element);
                }

            };

            foreach (LayoutElement element in this.Containers)
                finder(element);

            return result;
        }


        public IEnumerable<T> GetItemByPredicate<T>(Func<T, bool> predicate, bool returnOnFirst = false) where T : LayoutElement
        {
            List<T> result = new List<T>();

            Action<LayoutElement> finder = null;
            finder = (element) =>
            {
                if (!returnOnFirst || (returnOnFirst && result.Count == 0))
                {
                    if (element is T && predicate((T)element))
                        result.Add((T)element);

                    if (element is LayoutContainer && (!returnOnFirst || (returnOnFirst && result.Count == 0)))
                        ((LayoutContainer)element).Controls.Foreach(finder);
                }
            };

            this.Containers.ForEach(finder);


            return result;
        }

        public LayoutControlV2 GetControlByBindingPath(List<LayoutContainer> containers, string bindingPath, bool isMedia = false)
        {
            LayoutControlV2 result = null;

            Action<LayoutContainer> finder = null;
            finder = (n) =>
            {
                if (result == null)
                {
                    if (result.IsNull())
                        result = n.Controls.Where(e => e is LayoutControlV2 && e.BindingPath == bindingPath && ((isMedia && e.ClassName == "MultimediaControl") || (!isMedia && e.ClassName != "MultimediaControl"))).OrderBy(e => e.Name.Extract("Copy_", "_")).FirstOrDefault() as LayoutControlV2;

                    if (result.IsNull())
                        foreach (LayoutContainer container in n.Controls.Where(e => e is LayoutContainer))
                            if (result == null)
                                finder(container);
                            else
                                break;
                }
            };

            foreach (LayoutContainer container in containers)
                if (result == null)
                    finder(container);
                else
                    break;


            return result;
        }

        public LayoutContainer GetContainerByControlBindingPath(string bindingPath, bool isMedia = false)
        {
            return GetContainerByControlBindingPath(this.Containers, bindingPath, isMedia);
        }

        public LayoutContainer GetContainerByControlBindingPath(List<LayoutContainer> containers, string bindingPath, bool isMedia = false)
        {
            LayoutContainer result = null;

            Action<LayoutContainer> finder = null;
            finder = (n) =>
            {
                if (result == null)
                {
                    if (result.IsNull())
                    {
                        LayoutControlV2 control = n.Controls.Where(e => e is LayoutControlV2 && e.BindingPath == bindingPath && ((isMedia && e.ClassName == "MultimediaControl") || (!isMedia && e.ClassName != "MultimediaControl")) && e.Name.Extract("Copy_", "_").IsNullOrEmpty()).FirstOrDefault() as LayoutControlV2;
                        if (!control.IsNull())
                            result = n;
                    }

                    if (result.IsNull())
                        foreach (LayoutContainer container in n.Controls.Where(e => e is LayoutContainer))
                            if (result == null)
                                finder(container);
                            else
                                break;
                }
            };

            foreach (LayoutContainer container in containers)
                if (result == null)
                    finder(container);
                else
                    break;

            return result;
        }


        public LayoutElement GetRemovedControlByBindingPath(string bindingPath, bool isMedia = false)
        {
            LayoutElement result = null;

            foreach (LayoutElement element in this.RemovedLayoutElements)
            {
                if (element is LayoutControlV2 && element.BindingPath == bindingPath && ((isMedia && element.ClassName == "MultimediaControl") || (!isMedia && element.ClassName != "MultimediaControl")) && element.Name.Extract("Copy_", "_").IsNullOrEmpty())
                {
                    result = element;
                    break;
                }
                else if (element is LayoutContainer)
                {
                    result = GetControlByBindingPath((new LayoutContainer[] { ((LayoutContainer)element) }).ToList(), bindingPath, isMedia);
                    if (result != null)
                        break;
                }
            }

            return result;
        }

        public LayoutControlV2 GetContainerControlByName(string containerName, string controlName)
        {
            LayoutContainer container = this.GetContainerByName(containerName);
            if (!container.IsNull())
                return container.Controls.Where(e => e is LayoutControlV2 && e.Name == controlName).Select(e => (LayoutControlV2)e).FirstOrDefault();
            else
                return null;
        }


        public void AdjustFullPathClass()
        {
            Action<LayoutElement, string> setPath = null;
            setPath = (element, parentPath) =>
                {
                    element.FullPathClass = parentPath + (parentPath.IsNullOrEmpty() ? "" : "\\") + element.ClassName;

                    if (element is LayoutContainer)
                    {
                        ((LayoutContainer)element).Controls.ForEach(e => setPath(e, element.FullPathClass));
                    }
                };
            this.Containers.ForEach(e => setPath(e, ""));
        }


        public LayoutContainer GetContainerByControlName(string controlName)
        {
            return GetContainerByElementName(null, controlName, this.Containers);
        }

        private LayoutContainer GetContainerByElementName(string containerName, string controlName, List<LayoutContainer> containerCollection)
        {
            LayoutContainer containerResult = null;
            Action<LayoutContainer> finder = null;
            finder = (n) =>
            {
                if (containerResult == null)
                {
                    if ((!containerName.IsNull() && n.Name == containerName) || (!controlName.IsNull() && n.Controls.Where(e => e.Name == controlName).Count() > 0))
                        containerResult = n;
                    else
                        foreach (LayoutContainer container in n.Controls.Where(e => e is LayoutContainer))
                            if (containerResult == null)
                                finder(container);
                            else
                                break;
                }
            };

            foreach (LayoutContainer container in containerCollection)
                if (containerResult == null)
                    finder(container);
                else
                    break;

            return containerResult;
        }


        public LayoutContainer GetContainerByDefinedUserName(string containerName)
        {
            if (containerName.IsNullOrEmpty())
                return null;

            LayoutContainer containerResult = null;
            Action<LayoutContainer> finder = null;
            finder = (n) =>
            {
                if (containerResult == null)
                {
                    if (!n.DefinedUserName.IsNullOrEmpty() && n.DefinedUserName == containerName)
                        containerResult = n;
                    else
                        foreach (LayoutContainer container in n.Controls.Where(e => e is LayoutContainer))
                            if (containerResult == null)
                                finder(container);
                            else
                                break;
                }
            };

            foreach (LayoutContainer container in this.Containers)
                if (containerResult == null)
                    finder(container);
                else
                    break;

            return containerResult;
        }



        private LayoutControlV2 GetControlByName(string controlName, List<LayoutContainer> containerCollection)
        {
            LayoutControlV2 controlResult = null;
            Action<LayoutContainer> finder = null;
            finder = (n) =>
            {
                if (controlResult == null)
                {
                    controlResult = n.Controls.Where(e => e is LayoutControlV2 && e.Name == controlName).Select(e => (LayoutControlV2)e).FirstOrDefault();
                    if (controlResult == null)
                        foreach (LayoutContainer container in n.Controls.Where(e => e is LayoutContainer))
                            if (controlResult == null)
                                finder(container);
                            else
                                break;
                }
            };

            foreach (LayoutContainer container in containerCollection)
                if (controlResult == null)
                    finder(container);
                else
                    break;

            return controlResult;
        }


        public List<LayoutControlV2> GetTabularControls(string bindingPath)
        {
            List<LayoutControlV2> controlsResult = new List<LayoutControlV2>();
            int bindingPartsCount = bindingPath.Occurs(".") + 1;
            Action<LayoutContainer> finder = null;
            finder = (cnt) =>
            {
                //controlsResult.AddRange(cnt.Controls.OfType<LayoutControlV2>().Where(e => !e.IsPassword && e.IsVisible && e.FieldVisibleGrid != VisibleFieldGrid.Editor && !e.Name.Contains("Copy_") && !e.BindingPath.EndsWith("PagedList") && e.BindingPath.StartsWith(bindingPath + ".") && e.BindingPath.Occurs(".") == bindingPartsCount));
                controlsResult.AddRange(cnt.Controls.OfType<LayoutControlV2>().Where(e => !e.IsPassword && e.FieldVisibleGrid != VisibleFieldGrid.Editor && !e.Name.Contains("Copy_") && !e.BindingPath.EndsWith("PagedList") && e.BindingPath.StartsWith(bindingPath + ".") && e.BindingPath.Occurs(".") == bindingPartsCount));
                cnt.Controls.OfType<LayoutContainer>().ToList().ForEach(container => finder(container));
            };

            this.Containers.ForEach(container => finder(container));

            return controlsResult;
        }

        public void RemoveContainerByName(string containerName)
        {
            LayoutContainer containerFound = GetContainerByName(containerName);
            if (this.Containers.Contains(containerFound))
                this.Containers.Remove(containerFound);
            else
            {
                if (containerFound != null)
                {
                    foreach (LayoutContainer cnt in this.Containers)
                        DeleteContainerOrControlByName(cnt, containerFound);
                }
            }
        }


        public void DeleteContainerOrControlByName(LayoutContainer root, LayoutElement elementForDeleting)
        {
            LayoutElement element = root.Controls.Where(e => (e is LayoutContainer && e.Name == elementForDeleting.Name) || (e is LayoutControlV2 && !elementForDeleting.BindingPath.IsNullOrEmpty() && e.BindingPath == elementForDeleting.BindingPath)).FirstOrDefault();
            if (element != null)
                root.Controls.Remove(element);
            else
            {
                Action<LayoutContainer> tryRemove = null;
                tryRemove = (n) =>
                {
                    if (element == null)
                    {
                        element = n.Controls.Where(e => (e is LayoutContainer && e.Name == elementForDeleting.Name) || (e is LayoutControlV2 && !elementForDeleting.BindingPath.IsNullOrEmpty() && e.BindingPath == elementForDeleting.BindingPath)).FirstOrDefault() as LayoutElement;
                        if (element == null)
                        {
                            foreach (LayoutContainer container in n.Controls.Where(e => e is LayoutContainer))
                                if (element == null)
                                    tryRemove(container);
                                else
                                    break;
                        }
                        else
                            n.Controls.Remove(element);
                    }
                };

                foreach (LayoutContainer container in root.Controls.Where(e => e is LayoutContainer))
                    tryRemove(container);
            }
        }

        public LayoutElement GetRemovedContainerOrControlByName(string containerName, String controlName)
        {
            if (!containerName.IsNullOrEmpty() && !controlName.IsNullOrEmpty())
                return this.RemovedLayoutElements.Where(e => e.ParentName == containerName && e.Name == controlName).FirstOrDefault();
            else
            {
                List<LayoutContainer> llcTMP = new List<LayoutContainer>();
                LayoutContainer container = new LayoutContainer();
                container.Controls.AddRange(this.RemovedLayoutElements);
                llcTMP.Add(container);

                if (!containerName.IsNullOrEmpty())
                    return GetContainerByElementName(containerName, null, llcTMP);
                else
                    return GetControlByName(controlName, llcTMP);
            }
        }

        public void DeleteRemovedContainerOrControlByName(LayoutElement elementForDeleting)
        {
            LayoutElement element = this.RemovedLayoutElements.Where(e => (e is LayoutContainer && e.Name == elementForDeleting.Name) || (e is LayoutControlV2 && !elementForDeleting.BindingPath.IsNullOrEmpty() && e.BindingPath == elementForDeleting.BindingPath)).FirstOrDefault();
            if (element != null)
                this.RemovedLayoutElements.Remove(element);
            else
            {
                foreach (LayoutContainer cnt in this.RemovedLayoutElements.Where(e => e is LayoutContainer))
                    DeleteContainerOrControlByName(cnt, elementForDeleting);
            }
        }

        public LayoutContainer GetRemovedContainerByName(string containerName)
        {
            return (LayoutContainer)GetRemovedContainerOrControlByName(containerName, null);
        }

        public LayoutControlV2 GetRemovedControlByName(string controlName)
        {
            return (LayoutControlV2)GetRemovedContainerOrControlByName(null, controlName);
        }

    }

    #endregion


    #region LayoutElement

    [KnownType(typeof(LayoutControlV2))]
    [KnownType(typeof(LayoutContainer))]
    public partial class LayoutElement
    {
        private string _name = String.Empty;
        public string Name
        {
            get
            {
                if (_name == null)
                    _name = String.Empty;
                return _name;
            }
            set
            {
                if (value != _name)
                    _name = value;
            }
        }
        public string FullPathClass { get; set; }
        public string DefinedUserName { get; set; } // Control Name defined by developer
        public string DisplayName { get; set; }
        public string ClassName { get; set; }
        public bool IsEditable { get; set; }
        private bool _isVisible = true;
        public bool IsVisible { get { return _isVisible; } set { _isVisible = value; } }
        public int ImageIndex { get; set; }
        public bool Sync { get; set; }
        public string ParentName { get; set; } //PageName
        private string _bindingPath = string.Empty;
        public string BindingPath
        {
            get
            {
                return _bindingPath;
            }
            set
            {
                _bindingPath = (value == null ? String.Empty : value);
            }
        }
        public string ColumnMultiHeader { get; set; }

        private int _ColumnSpan = 0;
        public int ColumnSpan { get { return _ColumnSpan; } set { _ColumnSpan = value; } }
        public int Height { get; set; }

        public int OrderIndex { get; set; }
        public bool IsDerived { get; set; }
        public string FilterMark { get; set; }

        private FontForegroundStyle _FontForegroundStyle = FontForegroundStyle.Normal;
        public FontForegroundStyle FontForegroundStyle { get { return _FontForegroundStyle; } set { _FontForegroundStyle = value; } }

        private FontBackground _FontBackground = FontBackground.Normal;
        public FontBackground FontBackground { get { return _FontBackground; } set { _FontBackground = value; } }

        public bool FontBold { get; set; }

        public String SpecializedFilterEntityName { get; set; }
        public String SpecializedFilterRelationName { get; set; }
        public string InternalDefinition { get; set; }
        public string ScriptDefinition { get; set; }

        public string GetPrefix()
        {
            string prefix = "";
            switch (this.ClassName)
            {
                case "Button":
                    prefix = "btn";
                    break;
                case "CheckBox":
                    prefix = "ck";
                    break;
                case "ComboBox":
                    prefix = "cmb";
                    break;
                case "RadioButtonGroup":
                    prefix = "cmb";
                    break;
                case "EditBox":
                    prefix = "ed";
                    break;
                case "ExternalUI":
                    prefix = "eui";
                    break;
                case "TextBlock":
                    prefix = "lbl";
                    break;
                case "Label":
                    prefix = "lbl";
                    break;
                case "MaskedTextBox":
                    prefix = "msk";
                    break;
                case "NumericTextBox":
                    prefix = "ntx";
                    break;
                case "LookUpTextBox":
                    prefix = "lUp";
                    break;
                case "TextBox":
                    prefix = "tb";
                    break;
                case "Gauge":
                    prefix = "gauge";
                    break;
                case "DateTimeTextBox":
                    prefix = "dt";
                    break;
                case "Chart":
                    prefix = "chart";
                    break;
                case "TabItem":
                case "WizardItem":
                case "DockItem":
                    prefix = "ti";
                    break;
                case "TabControl":
                case "DockManager":
                    prefix = "tc";
                    break;
                case "PivotChart":
                case "PivotDrillDownChart":
                    prefix = "pChart";
                    break;
                case "CustomContainer":
                    prefix = "cnt";
                    break;
                case "TreeListView":
                case "DataGrid":
                    prefix = "dGrid";
                    break;
                case "OlapPivotGrid":
                    prefix = "olapPivot";
                    break;
                case "FlatPivotGrid":
                    prefix = "pivot";
                    break;
                case "WizardControl":
                    prefix = "wiz";
                    break;
                case "Expander":
                case "GroupBox":
                    prefix = "gb";
                    break;
                default:
                    break;
            }

            return prefix;
        }

        public string GetControlName(string prefix = "ctrl")
        {
            if (this.DefinedUserName.IsNullOrEmpty())
            {
                string recordTypeName = "", name = this.Name;
                if (this is LayoutControlV2 && !this.BindingPath.IsNullOrEmpty())
                {
                    string bindingPath = this.BindingPath.Right("DataElement.DataView.");
                    if (!bindingPath.IsNullOrEmpty())
                    {
                        string[] bidingParts = bindingPath.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                        if (bidingParts.Length > 1)
                        {
                            recordTypeName = (bidingParts[bidingParts.Length - 2] + "#").Left("PagedList#");
                        }
                    }
                }
                else if (this is LayoutContainer && name.IsNullOrEmpty())
                {
                    string bindingPath = ((LayoutContainer)this).Controls.Where(e => e is LayoutControlV2 && !e.BindingPath.IsNullOrEmpty()).Select(e => e.BindingPath.Right("DataElement.DataView.")).FirstOrDefault();
                    if (!bindingPath.IsNullOrEmpty())
                    {
                        string[] bidingParts = bindingPath.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                        if (bidingParts.Length > 1)
                        {
                            name = (bidingParts[bidingParts.Length - 2] + "#").Left("PagedList#");
                        }
                    }
                }

                return prefix + (recordTypeName.IsNullOrEmpty() ? "" : recordTypeName.Replace(".", "") + "_") + name;
            }
            else
            {
                return prefix + this.DefinedUserName;
            }
        }
        public string GetDefaultControlName()
        {
            return GetControlName(GetPrefix());
        }

        public override string ToString()
        {
#if DEBUG
            return this.Name;
#else
                return base.ToString();
#endif
        }
    }

    #endregion

    #region LayoutContainer

    [KnownType(typeof(LayoutControlV2))]
    public partial class LayoutContainer : LayoutElement
    {
        public int ColumnCount { get; set; }

        public Boolean RemoveDataToolbar { get; set; }
        public Boolean RemoveViewSwitch { get; set; }
        public Boolean Virtualization { get; set; }

        private ContainerStyle _Style = ContainerStyle.Normal;
        public ContainerStyle Style { get { return _Style; } set { _Style = value; } }

        public int PageSize { get; set; }

        #region ToolbarControl
        private bool _canClear = true;
        public bool CanClear { get { return _canClear; } set { _canClear = value; } }
        private bool _canSearch = true;
        public bool CanSearch { get { return _canSearch; } set { _canSearch = value; } }
        private bool _canAddNew = true;
        public bool CanAddNew { get { return _canAddNew; } set { _canAddNew = value; } }
        private bool _canEdit = true;
        public bool CanEdit { get { return _canEdit; } set { _canEdit = value; } }
        private bool _canDelete = true;
        public bool CanDelete { get { return _canDelete; } set { _canDelete = value; } }
        private bool _canCustomSearch = true;
        public bool CanCustomSearch { get { return _canCustomSearch; } set { _canCustomSearch = value; } }
        private bool _canPrint = true;
        public bool CanPrint { get { return _canPrint; } set { _canPrint = value; } }
        private bool _canLayout = true;
        public bool CanLayout { get { return _canLayout; } set { _canLayout = value; } }
        private bool _canNavigate = true;
        public bool CanNavigate { get { return _canNavigate; } set { _canNavigate = value; } }
        private bool _canExport = true;
        public bool CanExport { get { return _canExport; } set { _canExport = value; } }
        public bool NoBusyLoading { get; set; }
        #endregion

        private LabelPosition _labelPosition = LabelPosition.Top;
        public LabelPosition LabelPosition { get { return _labelPosition; } set { _labelPosition = value; } }

        private UILayouts _userInterfaceLayoutType = UILayouts.ColumnsLayout;
        public UILayouts UserInterfaceLayoutType { get { return _userInterfaceLayoutType; } set { _userInterfaceLayoutType = value; } }
        private string _pivotChartType = "Column";
        public string PivotChartType { get { return _pivotChartType; } set { _pivotChartType = value; } }
        public string PivotGridName { get; set; }

        public string UserInterfaceName { get; set; }
        public string ParentFieldsRelation { get; set; }
        public string DetailFieldsRelation { get; set; }
        public string ParentSelectorDataName { get; set; }
        public string GroupByColumns { get; set; }
        public bool ShareParentBO { get; set; }
        public bool NoSearch { get; set; }

        private bool _isExpanded = true;
        public bool IsExpanded { get { return _isExpanded; } set { _isExpanded = value; } }

        public bool HasGroupBy { get; set; }
        private bool _isTemplate = true;
        public bool IsTemplate { get { return _isTemplate; } set { _isTemplate = value; } }
        public bool IsInnerTemplate { get; set; }
        private bool _editorWithinGrid = false;
        public bool EditorWithinGrid { get { return _editorWithinGrid; } set { _editorWithinGrid = value; } }
        public bool HasColumnFixing { get; set; }

        private bool _canExportGrid = true;
        public bool CanExportGrid { get { return _canExportGrid; } set { _canExportGrid = value; } }

        private bool _enableGridSelector = false;
        public bool EnableGridSelector { get { return _enableGridSelector; } set { _enableGridSelector = value; } }

        private bool _startOpenSelector = false;
        public bool StartOpenSelector { get { return _startOpenSelector; } set { _startOpenSelector = value; } }

        public string SelectorGridColumns { get; set; }
        public string DisplaySelectorGridColumns { get; set; }

        private bool _editionOnlyTemplate = false;
        public bool EditionOnlyTemplate { get { return _editionOnlyTemplate; } set { _editionOnlyTemplate = value; } }



        #region Grid
        private GridSizeWidth _gridWidth = GridSizeWidth.Large;
        public GridSizeWidth GridWidth
        {
            get { return _gridWidth; }
            set { _gridWidth = value; }
        }

        private GridSizeHeight _gridHeight = GridSizeHeight.Medium;
        public GridSizeHeight GridHeight
        {
            get { return _gridHeight; }
            set { _gridHeight = value; }
        }

        public bool EnableMultiSelection { get; set; }

        #endregion

        #region LightDataGrid

        public bool AutoFitLastColumn { get; set; }
        public byte GridVisibleRowsNumber { get; set; }

        private bool _enableFilterTextInGrid = true;
        public bool EnableFilterTextInGrid
        {
            get { return _enableFilterTextInGrid; }
            set { _enableFilterTextInGrid = value; }
        }

        #endregion

        #region Parent Filter Control
        public bool UseFilterFromParent { get; set; }
        private bool _applyFilterToParent = true;
        public bool ApplyFilterToParent { get { return _applyFilterToParent; } set { _applyFilterToParent = value; } }
        #endregion


        #region Olap control

        /// <summary>
        /// Pivot Table Layout
        /// </summary>
        private string _pivotFileLayout;
        public string PivotFileLayout
        {
            get { return _pivotFileLayout; }
            set { _pivotFileLayout = value; }
        }

        /// <summary>
        /// Pivot View Mode
        /// </summary>
        private string _pivotViewType;
        public string PivotViewType
        {
            get { return _pivotViewType; }
            set { _pivotViewType = value; }
        }

        /// <summary>
        /// Type chart with grid pivot
        /// </summary>
        private string _pivotChartTypeGridChart;
        public string PivotChartTypeGridChart
        {
            get { return _pivotChartTypeGridChart; }
            set { _pivotChartTypeGridChart = value; }
        }
        /// <summary>
        /// Position chart with grid Pivot
        /// </summary>
        private string _pivotChartPosition;
        public string PivotChartPosition
        {
            get { return _pivotChartPosition; }
            set { _pivotChartPosition = value; }
        }

        private string _pivotDataSource;
        public string PivotDataSource
        {
            get { return _pivotDataSource; }
            set { _pivotDataSource = value; }
        }

        private string _entityData;
        public string EntityData
        {
            get { return _entityData; }
            set { _entityData = value; }
        }

        private string _pivotEixoMeasure;
        public string PivotEixoMeasure
        {
            get { return _pivotEixoMeasure; }
            set { _pivotEixoMeasure = value; }
        }

        private string _pivotDimensionsColsCustom;
        public string PivotDimensionsColsCustom
        {
            get { return _pivotDimensionsColsCustom; }
            set { _pivotDimensionsColsCustom = value; }
        }

        private string __pivotDimensionsRowsCustom;
        public string PivotDimensionsRowsCustom
        {
            get { return __pivotDimensionsRowsCustom; }
            set { __pivotDimensionsRowsCustom = value; }
        }

        private string _pivotMeasuresCustom;
        public string PivotMeasuresCustom
        {
            get { return _pivotMeasuresCustom; }
            set { _pivotMeasuresCustom = value; }
        }

        private string _pivotTopData;
        public string PivotTopData
        {
            get { return _pivotTopData; }
            set { _pivotTopData = value; }
        }

        private bool _isLayoutToolbar;
        public bool IsLayoutToolbar
        {
            get { return _isLayoutToolbar; }
            set { _isLayoutToolbar = value; }
        }

        private bool _pivotToggleView;
        public bool PivotToggleView
        {
            get { return _pivotToggleView; }
            set { _pivotToggleView = value; }
        }

        public bool PivotFullScreen { get; set; }

        public bool PivotOpenReport { get; set; }

        /// <summary>
        /// Cube Name.
        /// </summary>
        public String PivotCube { get; set; }
        /// <summary>
        /// Example: [Geography].[City]
        /// </summary>
        public String PivotRows { get; set; }
        /// <summary>
        /// Example: [Date].[Calendar]
        /// </summary>
        public String PivotColumns { get; set; }
        /// <summary>
        /// Example: [Sales Territory].[Sales Territory Country]{[Sales Territory].[Sales Territory Country].&amp;[United Kingdom]}
        /// </summary>
        public String PivotFilters { get; set; }
        /// <summary>
        /// Example: Reseller Sales Amount
        /// </summary>
        public String PivotMeasures { get; set; }
        /// <summary>
        /// Columns or Rows
        /// </summary>
        private string _pivotMeasuresLocation = "Columns";
        public string PivotMeasuresLocation { get { return _pivotMeasuresLocation; } set { _pivotMeasuresLocation = value; } }
        public bool IsPivotExpanded { get; set; }
        public bool IsPivotReadOnly { get; set; }
        public bool ParentInFrontForRows { get; set; }
        public bool ParentInFrontForColumns { get; set; }
        public bool IsLinqSelectionControl { get; set; }
        private bool _isTotalVisible = true;
        public bool IsTotalVisible { get { return _isTotalVisible; } set { _isTotalVisible = value; } }

        private PivotScope _pivotScope = PivotScope.OnlyTable;
        public PivotScope PivotScope
        {
            get { return _pivotScope; }
            set { _pivotScope = value; }
        }

        private string _pivotRelationship;
        public string PivotRelationship
        {
            get { return _pivotRelationship; }
            set { _pivotRelationship = value; }
        }

        private bool _pivotRelationshipQuery;
        public bool PivotRelationshipQuery
        {
            get { return _pivotRelationshipQuery; }
            set { _pivotRelationshipQuery = value; }
        }

        private CharTypeEnum _pivotTableChartType = CharTypeEnum.Area;
        public CharTypeEnum PivotTableChartType
        {
            get { return _pivotTableChartType; }
            set { _pivotTableChartType = value; }
        }

        private bool _chartMultipleValues = false;
        public bool ChartMultipleValues
        {
            get { return _chartMultipleValues; }
            set { _chartMultipleValues = value; }
        }

        private bool _callServiceOkEvent;
        public bool CallServiceOkEvent
        {
            get { return _callServiceOkEvent; }
            set { _callServiceOkEvent = value; }
        }

        #endregion

        #region Olap Chart
        /// <summary>
        /// Example: TotalQtde, TotalValue, etc...
        /// </summary>
        public string ChartMeasures { get; set; }
        public string ChartAvailableMeasures { get; set; }
        /// <summary>
        /// Name: Description, etc...
        /// </summary>
        public string ChartColumns { get; set; }
        /// <summary>
        /// Example: TotalQtde, TotalValue, etc...
        /// </summary>
        public string ChartRows { get; set; }
        /// <summary>
        /// Example: TotalQtde, TotalValue, etc...
        /// </summary>
        public string ChartDimensions { get; set; }
        /// <summary>
        /// Columns or Rows
        /// </summary>
        private string _olapAxisSource = "Columns";
        public string OlapAxisSource { get { return _olapAxisSource; } set { _olapAxisSource = value; } }
        #endregion

        #region Wizard
        public bool RemoveInitialPage { get; set; }
        public bool RemoveFinalPage { get; set; }
        public string InitialPageDisplayName { get; set; }
        public string FinalPageDisplayName { get; set; }
        public string InitialPageDescription { get; set; }
        public string FinalPageDescription { get; set; }
        public string SideBarDescription { get; set; }
        public LayoutElement InitialPageDescriptionLayout { get; set; }
        public LayoutElement FinalPageDescriptionLayout { get; set; }
        #endregion

        private List<LayoutElement> _Controls;
        public List<LayoutElement> Controls
        {
            get
            {
                if (_Controls == null)
                    _Controls = new List<LayoutElement>();

                return _Controls;
            }

            set
            {
                _Controls = value;
            }
        }



        public IEnumerable<LayoutElement> GetAllControls()
        {
            return ObjectExtension.GetFlattenHierarchical(this, (e) => e.Controls);
        }

        public void ResetSync()
        {
            foreach (LayoutElement control in this.Controls)
                if (control is LayoutContainer)
                    ((LayoutContainer)control).ResetSync();
                else if (control is LayoutControlV2)
                    ((LayoutControlV2)control).Sync = ((LayoutControlV2)control).IsCustomized;
        }

        public void AdjustSyncCopies(LayoutControlV2 originalControl)
        {
            if (originalControl == null)
                return;

            Action<LayoutContainer> reset = null;
            reset = (container) =>
            {
                if (container != null)
                {
                    foreach (var control in container.Controls)
                        if (control is LayoutControlV2)
                        {
                            if (!control.Sync && control.BindingPath == originalControl.BindingPath)
                                control.Sync = originalControl.Sync;
                        }
                        else if (control is LayoutContainer)
                            reset(((LayoutContainer)control));
                }

            };

            reset(this);
        }

        public void RemoveNoSyncElements()
        {
            Action<LayoutContainer> removing = null;
            removing = (container) =>
            {
                for (int cnt = container.Controls.Count - 1; cnt >= 0; cnt--)
                    if (container.Controls[cnt] is LayoutControlV2)
                    {
                        if (!((LayoutControlV2)container.Controls[cnt]).Sync)
                            container.Controls.RemoveAt(cnt);

                    }
                    else if (cnt < _Controls.Count)
                        if (_Controls[cnt] is LayoutContainer)
                            removing(((LayoutContainer)container.Controls[cnt]));

            };
            removing(this);
        }


        public void RemoveControl(LayoutControlV2 control)
        {
            if (this.Controls.Contains(control))
                this.Controls.Remove(control);
            else
                foreach (LayoutContainer item in this.Controls.Where(e => e is LayoutContainer))
                    item.RemoveControl(control);
        }


        public List<TreeLayoutContainer> GetInnerContainersAsTree(string clasName)
        {
            List<TreeLayoutContainer> result = new List<TreeLayoutContainer>();

            Action<LayoutContainer, List<TreeLayoutContainer>> genDataGrids = null;

            genDataGrids = (cnt, list) =>
            {
                if (cnt.ClassName == clasName)
                {
                    TreeLayoutContainer treeDG = new TreeLayoutContainer() { Container = cnt, Containers = new List<TreeLayoutContainer>() };
                    list.Add(treeDG);
                    cnt.Controls.Where(e => e is LayoutContainer).Select(e => (LayoutContainer)e).ToList().ForEach(e => genDataGrids(e, treeDG.Containers));
                }
                else
                    cnt.Controls.Where(e => e is LayoutContainer).Select(e => (LayoutContainer)e).ToList().ForEach(e => genDataGrids(e, list));
            };

            this.Controls.Where(e => e is LayoutContainer).Select(e => (LayoutContainer)e).ToList().ForEach(e => genDataGrids(e, result));

            return result;
        }
    }

    #endregion

    #region LayoutControlV2

    [KnownType(typeof(LayoutContainer))]
    public partial class LayoutControlV2 : LayoutElement
    {
        public bool BrandDecimalsControl { get; set; }
        public bool HasTemporaryKey { get; set; }
        public bool IsParentBind { get; set; }
        public bool IsCustomized { get; set; }
        public string ConnectedAttribute { get; set; }
        public string GroupName { get; set; }
        public string SourceViewName { get; set; }
        public string Precision { get; set; }
        public string DataFormatString { get; set; }
        public string AggregationFunction { get; set; }
        public string AggregationDescription { get; set; }
        public string ActionEvent { get; set; }
        public bool IsDataField { get; set; }
        public string DataType { get; set; }
        public string DataGridOrder { get; set; }
        public int LabelWidth { get; set; }
        public string Url { get; set; }
        public string HtmlCode { get; set; }
        public int TotalLines { get; set; }

        public int DataGridWidth { get; set; }
        public bool GridColAutoFit { get; set; }

        private bool _dataGridWordWrap = false;
        public bool DataGridWordWrap
        {
            get { return _dataGridWordWrap; }
            set { _dataGridWordWrap = value; }
        }

        private ControlWidth _ControlWidth = ControlWidth.Automatic;
        public ControlWidth ControlWidth { get { return _ControlWidth; } set { _ControlWidth = value; } }

        private VisibleFieldGrid _FieldVisibleGrid = VisibleFieldGrid.Both;
        public VisibleFieldGrid FieldVisibleGrid
        {
            get { return _FieldVisibleGrid; }
            set { _FieldVisibleGrid = value; }
        }

        private MediaWidth _MediaWidth = MediaWidth.Small;
        public MediaWidth MediaWidth { get { return _MediaWidth; } set { _MediaWidth = value; } }
        /// <summary>
        /// Can multi seletion in Editing
        /// </summary>
        public bool MultiSelection { get; set; }

        private bool _AllowMultiSelectionInSearch = true;
        /// <summary>
        /// Allow multi selecion in clearState, to search
        /// </summary>
        public bool AllowMultiSelectionInSearch
        {
            get { return _AllowMultiSelectionInSearch; }
            set { _AllowMultiSelectionInSearch = value; }
        }

        public bool ValidateOnClearState { get; set; }

        private bool _allowEmptyOption = true;
        public bool AllowEmptyOption
        {
            get { return _allowEmptyOption; }
            set { _allowEmptyOption = value; }
        }

        private bool _allowNegativeValue = false;
        public bool AllowNegativeValue
        {
            get { return _allowNegativeValue; }
            set { _allowNegativeValue = value; }
        }

        private string _radioPosition = "Vertical";
        public string RadioPosition
        {
            get { return _radioPosition; }
            set { _radioPosition = value; }
        }



        // Chart Attributes
        public String ChartTitle { get; set; }
        public String AxisXTitle { get; set; }
        public String AxisYTitle { get; set; }
        public String LegendTitle { get; set; }
        public Double AxisXLabelRotation { get; set; }
        public Double AxisYLabelRotation { get; set; }
        public int ChartLegendPosition { get; set; }
        public String XCategoryFieldName { get; set; }
        public String LegendLabelFieldName { get; set; }
        public String LabelFieldName { get; set; }
        public String YValueFieldName { get; set; }
        public String LowValueFieldName { get; set; }
        public String HighValueFieldName { get; set; }
        public String OpenValueFieldName { get; set; }
        public String CloseValueFieldName { get; set; }
        public String BubbleSizeFieldName { get; set; }
        public byte ChartType { get; set; }
        public String SpecializedFilterName { get; set; }
        public String MaskType { get; set; }
        public String Mask { get; set; }
        public String MaskCulture { get; set; }
        public String CommandInputButton { get; set; }
        public String FieldAlignment { get; set; }
        public String ToolTip { get; set; }
        public String ChartColor { get; set; }
        public String GaugeType { get; set; }
        public String GaugeRadialScaleName { get; set; }
        public String GaugeRadialBarName { get; set; }
        public String GaugeStateIndicatorName { get; set; }
        public String GaugeNeedleName { get; set; }
        public String GaugeLinearScaleName { get; set; }
        public String GaugeMarkerName { get; set; }
        public String GaugeLinearBarName { get; set; }
        public Boolean RadialCheck { get; set; }
        public Boolean StateIndicatorCheck { get; set; }
        public Boolean NeedleCheck { get; set; }
        public Boolean LinearScaleCheck { get; set; }
        public Boolean RadialScaleCheck { get; set; }
        public Boolean MarkerCheck { get; set; }
        public Boolean LinearBarCheck { get; set; }
        public Boolean AlwaysEditable { get; set; }
        public Boolean EditableOnInsert { get; set; }
        public Boolean IsPassword { get; set; }

        public Boolean ChartUseSolidColors { get; set; }
        public Boolean ChartScrollAndZoom { get; set; }

        public int ChartFontSize { get; set; }
        public int ChartTitleFontSize { get; set; }

        private String _chartAxisXLabelFormat = String.Empty;
        public String ChartAxisXLabelFormat { get { return _chartAxisXLabelFormat; } set { _chartAxisXLabelFormat = value; } }
        private String _chartAxisYLabelFormat = String.Empty;
        public String ChartAxisYLabelFormat { get { return _chartAxisYLabelFormat; } set { _chartAxisYLabelFormat = value; } }

        private int _majorDivisions = 3;
        private int _middleDivisions = 2;
        private int _minorDivisions = 2;

        public int MajorDivisions { get { return _majorDivisions; } set { _majorDivisions = value; } }
        public int MiddleDivisions { get { return _middleDivisions; } set { _middleDivisions = value; } }
        public int MinorDivisions { get { return _minorDivisions; } set { _minorDivisions = value; } }
        public bool HideValueLabel { get; set; }

        private bool _hasFilterRange = true;
        public bool HasFilterRange { get { return _hasFilterRange; } set { _hasFilterRange = value; } }

        private bool _displayRangeDate = false;
        public bool DisplayRangeDate { get { return _displayRangeDate; } set { _displayRangeDate = value; } }

        #region Olap control
        public bool IsMeasure { get; set; }
        public String Group { get; set; }
        public string MeasureFormula { get; set; }
        #endregion

        public string SubstituteProperties { get; set; }
        public string KpiName { get; set; }
        public string DomainName { get; set; }
        public string DomainFilterValues { get; set; }
        public string LookUpName { get; set; }
        public bool HasLookupFilter { get; set; }
        public bool EnableLookupAutoComplete { get; set; }
        public byte LookupAutoCompleteMaxResults { get; set; }

        public bool IsPartOfKey { get; set; }
        public bool IsNullable { get; set; }

        public string Range { get; set; }

       

        #region Dashboard

        /// <summary>
        /// Background-color cssName, used in dashboard and tile control
        /// </summary>
        public string DashboardBackgroundColorClassName { get; set; }

        /// <summary>
        /// Font-Awesome icon. Samples: fa-search, fa-refresh, etc
        /// </summary>
        public string DashboardIconFAName { get; set; }

        /// <summary>
        /// Dashboard Width sizes, large:12, medium:6 and small:3
        /// </summary>
        public string DashboardWidth { get; set; }

        #endregion
    }

    #endregion

    #endregion

    #region TreeDataGrid

    public class TreeLayoutContainer
    {
        public LayoutContainer Container { get; set; }
        public List<TreeLayoutContainer> Containers { get; set; }
    }

    #endregion

    #region Visual Element Type

    public enum LayoutControlClass
    {
        CheckBox,
        Label,
        Chart,
        ComboBox,
        DateTimeTextBox,
        EditBox,
        EconomicGroup,
        LookUpTextBox,
        MultimediaControl,
        NumericTextBox,
        TextBox,
        Button,
        TextBlock,
        MaskedTextBox,
        KpiBox,
        Gauge,
        RadioButtonGroup,
        HtmlViewer,
        ChildToolBar,
        ColorPicker,
        Dashboard,
        CustomControl
    }

    public enum LayoutContainerClass
    {
        Expander,
        TabItem,
        DataGrid,
        TabControl,
        CustomContainer,
        TreeListView,
        WizardItem,
        WizardControl,
        FlatPivotGrid,
        OlapPivotGrid,
        PivotChart,
        PivotDrillDownChart,
        DockItem,
        DockManager,
        ExternalUI,
        GroupBox
    }

    #endregion

}

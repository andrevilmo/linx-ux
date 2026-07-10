using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Linx.Tools;

namespace Linx.Builder.Resources
{
    public class HtmlCodeGen : LayoutCodeGen<CustomizedLayoutV2>
    {
        public const int columnSpanMax = 12;
        public const int columnSpanLabel = 3;
        public const string spaceHtml = "&nbsp;";
        private const long cMaxValueLongType = 9007199254740992;
        public const int minGridHeight = 140;



        public HtmlCodeGen(CustomizedLayoutV2 layout, string entityName, string viewModelName)
            : base(layout, entityName, viewModelName)
        {
            this._layOut.AdjustFullPathClass();
            this.SupportsDataGridTemplate = false;
            List<LayoutElement> dataGrids = new List<LayoutElement>();
            dataGrids.AddRange(layout.GetLayoutElementsByClass("DataGrid"));
            dataGrids.AddRange(layout.GetLayoutElementsByClass("TreeListView"));

            foreach (LayoutContainer grd in dataGrids)
            {
                Linx.Tools.LayoutContainer gridTemplate = (Linx.Tools.LayoutContainer)((Linx.Tools.LayoutContainer)(grd));
                if (gridTemplate != null && gridTemplate.IsTemplate)
                {
                    var parent = dataGrids[grd.OrderIndex];
                    List<LayoutElement> templateControl = (List<LayoutElement>)gridTemplate.Controls.CloneSerializing();


                    Action<LayoutElement> recursive = null;
                    recursive = (element) =>
                    {
                        if (element is LayoutContainer)
                        {
                            ((LayoutContainer)element).Controls.RemoveAction(it => it.ClassName == "ExternalUI");
                            ((LayoutContainer)element).Controls.ForEach(recursive);
                        }
                    };
                    templateControl.RemoveAction(it => it.ClassName == "ExternalUI");
                    templateControl.ForEach(recursive);

                    var template = new Linx.Tools.LayoutContainer
                    {
                        BindingPath = gridTemplate.BindingPath,
                        Name = gridTemplate.Name,
                        DisplayName = gridTemplate.DisplayName,
                        ClassName = "CustomContainer",
                        Controls = templateControl,
                        IsTemplate = gridTemplate.IsTemplate,
                        ColumnCount = (gridTemplate.ColumnCount == 0 ? 2 : gridTemplate.ColumnCount),
                        RemoveDataToolbar = gridTemplate.RemoveDataToolbar,
                        IsInnerTemplate = true,
                        CanDelete = gridTemplate.CanDelete,
                        CanAddNew = gridTemplate.CanAddNew,
                        CanEdit = gridTemplate.CanEdit,
                        EditionOnlyTemplate = gridTemplate.EditionOnlyTemplate,
                        EnableGridSelector = gridTemplate.EnableGridSelector,
                        StartOpenSelector = gridTemplate.StartOpenSelector,
                        SelectorGridColumns = gridTemplate.SelectorGridColumns,
                        DisplaySelectorGridColumns = gridTemplate.DisplaySelectorGridColumns
                    };
                    var parentContainer = base._layOut.GetContainerByControlName(parent.ToString());
                    if (parentContainer != null)
                        parentContainer.Controls.Add(template);
                    else
                        base._layOut.Containers.Add(template);
                }
            }


        }


        public override void ComposeContainerStart(LayoutContainer parentContainer, LayoutContainer container, LayoutContainerClass elementClass, Tools.CodeBuilder codeBuilder, int rows, int columns, List<TreeLayoutContainer> innerDataGrids, int index)
        {

            switch (elementClass)
            {
                case LayoutContainerClass.CustomContainer:
                    this.ComposeDefaultContainerStart(parentContainer, container, elementClass, codeBuilder, rows, columns);
                    break;
                case LayoutContainerClass.ExternalUI:
                    codeBuilder.AddLine("<div style=\"height:" + (container.Height > 0 ? container.Height.ToString() + "px" : "auto") + ";\">");
                    codeBuilder.IncreaseIndent();
                    codeBuilder.AddLine("<div id=\"" + container.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "eui") + "\" class=\"extenalUI" + (container.IsVisible ? "" : " hide") + "\" ");
                    if (container.UserInterfaceName.IsNullOrEmpty())
                        codeBuilder.Add("/>");
                    else
                    {
                        codeBuilder.AddLine("data-bind=\"widget: { kind: 'pkg_" +
                            container.UserInterfaceName.Left("/").ToLower().Replace(".", "-") + "/viewmodels/" + container.UserInterfaceName.Right("/") +
                            "', parentVM: $root, uiSettings: { removeDataToolbar: " + container.RemoveDataToolbar.ToString().ToLower() + ", shareParentBO: " +
                            container.ShareParentBO.ToString().ToLower() + ", useFilterFromParent: " + container.UseFilterFromParent.ToString().ToLower() +
                            ", parentSelectorDataName: '" + (container.ParentSelectorDataName ?? "") + "'" +
                            ", displayName: '" + (container.DisplayName.IsNullOrEmpty() || container.DisplayName == "New Group" ? "" : container.DisplayName) + "'" +
                            ", canClear: " + container.CanClear.ToString().ToLower() +
                            ", canSearch: " + container.CanSearch.ToString().ToLower() +
                            ", canExport: " + container.CanExport.ToString().ToLower() +
                            ", canAddNew: " + container.CanAddNew.ToString().ToLower() +
                            ", canEdit: " + container.CanEdit.ToString().ToLower() +
                            ", canDelete: " + container.CanDelete.ToString().ToLower() +
                            ", canCustomSearch: " + container.CanCustomSearch.ToString().ToLower() +
                            ", canPrint: " + container.CanPrint.ToString().ToLower() +
                            ", canLayout: " + container.CanLayout.ToString().ToLower() +
                            ", canNavigate: " + container.CanNavigate.ToString().ToLower() +
                            ", canNavigate: " + container.CanNavigate.ToString().ToLower() +
                            ", applyFilterToParent: " + container.ApplyFilterToParent.ToString().ToLower() + ", noSearch: " + container.NoSearch.ToString().ToLower() +
                            ", noBusyLoading: " + container.NoBusyLoading.ToString().ToLower() +
                            ", parentFieldsRelation: ['" + (container.ParentFieldsRelation ?? "").Replace(", ", ",").Replace(",", "', '") + "'], detailFieldsRelation: ['" +
                            (container.DetailFieldsRelation ?? "").Replace(", ", ",").Replace(",", "', '") + "'] } }\" />");
                    }
                    codeBuilder.DecreaseIndent();
                    break;
                case LayoutContainerClass.TreeListView:
                    this.ComposeLightDataGrid(parentContainer, container, elementClass, codeBuilder, rows, columns, innerDataGrids);
                    break;
                case LayoutContainerClass.DataGrid:
                    this.ComposeDataGrid(parentContainer, container, elementClass, codeBuilder, rows, columns, innerDataGrids);
                    break;
                case LayoutContainerClass.Expander:
                    this.ComposeGroupBoxContainerStart(parentContainer, container, elementClass, codeBuilder, rows, columns, true);
                    break;
                case LayoutContainerClass.GroupBox:
                    this.ComposeGroupBoxContainerStart(parentContainer, container, elementClass, codeBuilder, rows, columns, false);
                    break;
                case LayoutContainerClass.OlapPivotGrid:
                    this.ComposeOlapPivotGrid(parentContainer, container, elementClass, codeBuilder, rows, columns);
                    break;
                case LayoutContainerClass.FlatPivotGrid:
                    this.ComposeFlatPivotGrid(parentContainer, container, elementClass, codeBuilder, rows, columns);
                    break;
                case LayoutContainerClass.PivotChart:
                case LayoutContainerClass.PivotDrillDownChart:
                    this.ComposePivotChart(parentContainer, container, elementClass, codeBuilder);
                    break;
                case LayoutContainerClass.DockManager:
                case LayoutContainerClass.TabControl:
                    this.ComposeTabControlContainerStart(parentContainer, container, elementClass, codeBuilder, rows, columns);
                    break;
                case LayoutContainerClass.DockItem:
                case LayoutContainerClass.WizardItem:
                case LayoutContainerClass.TabItem:
                    this.ComposeTabItemControlContainerStart(container, elementClass, codeBuilder, rows, columns, index);
                    break;
                case LayoutContainerClass.WizardControl:
                    this.ComposeWizardControlContainerStart(parentContainer, container, elementClass, codeBuilder, rows, columns);
                    break;
                default:
                    break;
            }
        }

        public override void ComposeContainerStartRow(LayoutContainer container, LayoutContainerClass elementClass, Tools.CodeBuilder codeBuilder, int row)
        {
        }

        public override void ComposeContainerStartColumn(LayoutContainer container, LayoutContainerClass elementClass, Tools.CodeBuilder codeBuilder, int totalColumns, int columnSpan)
        {
            if (!elementClass.In(LayoutContainerClass.WizardControl, LayoutContainerClass.TabControl))
            {
                if (!IsButtonContainer(container))
                    StartColumn(container, codeBuilder, columnSpan == 0 ? (int)(columnSpanMax / totalColumns) : columnSpan);
            }
        }

        public override void ComposeControl(LayoutControlV2 control, LayoutControlClass elementClass, Tools.CodeBuilder codeBuilder, bool isConnected, Dictionary<LayoutControlV2, LayoutControlClass> connectedControls, bool labelOnTop, bool isTemplate)
        {
            string binding = GetBindingPath(control.BindingPath, true);
            bool hasBind = !control.BindingPath.IsNullOrEmpty();
            string dataView = binding.IsNullOrEmpty() ? this.EntityName : (binding.Right(".") + "#").Left("List#");
            var hasConnectedControls = connectedControls != null && connectedControls.Count > 0;
            string fontclass = GetFont(control, false);
            string controlName = (_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + control.GetDefaultControlName();
            control.IsVisible = (control.IsVisible && control.FieldVisibleGrid != VisibleFieldGrid.Grid ? true : false);
            string currentBinding = GetFullBindingPath(control.BindingPath, false);
            string controlBinding = null;

            ControlWidth _width = control.ControlWidth;
            if (_width == ControlWidth.Fluid)
                _width = ControlWidth.Automatic;
            if (elementClass != LayoutControlClass.MultimediaControl && _width == ControlWidth.Automatic)
                _width = HtmlCodeGen.GetControlWidth(control.ClassName, control.DataType, control.DisplayName, control.DataFormatString, control.Precision);


            if (control.ColumnSpan == 0 && _width != ControlWidth.Automatic && _width != ControlWidth.Fluid)
            {
                //1  Mini
                //2  ExtraSmall
                //3  Small
                //5  MinMedium
                //6  Medium
                //8  ExtraMedium
                //10 Large
                //12 ExtraLarge

                switch (_width)
                {
                    case ControlWidth.Mini:
                        control.ColumnSpan = 1;
                        break;
                    case ControlWidth.ExtraSmall:
                        control.ColumnSpan = 2;
                        break;
                    case ControlWidth.Small:
                        control.ColumnSpan = 3;
                        break;
                    case ControlWidth.MinMedium:
                        control.ColumnSpan = 5;
                        break;
                    case ControlWidth.Medium:
                        control.ColumnSpan = 6;
                        break;
                    case ControlWidth.ExtraMedium:
                        control.ColumnSpan = 8;
                        break;
                    case ControlWidth.Large:
                        control.ColumnSpan = 10;
                        break;
                    case ControlWidth.ExtraLarge:
                        control.ColumnSpan = 12;
                        break;
                    default:
                        control.ColumnSpan = 12;
                        break;
                }
            }

            //if (elementClass != LayoutControlClass.Button)
            //{
            codeBuilder.AddLine("<div data-bind=\"css: $root." + this.ViewModelName + "().getLayoutColumnSpan('" + controlName + "'), visible: $root." + this.ViewModelName + "().getLayoutVisible('" + controlName + "')\" class=\"" + (isConnected ? "connected-field" : "") + "\">");
            codeBuilder.IncreaseIndent();
            //}

            codeBuilder.AddLine("<div class=\"form-group\">");

            //if (elementClass != LayoutControlClass.Button)
            codeBuilder.IncreaseIndent();

            codeBuilder.AddLine("<div id=\"" + (isTemplate == true ? control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "div") + "Template" : control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "div")) + "\" class=\"" + (control.FieldVisibleGrid == VisibleFieldGrid.Editor ? "onlyEditor " : "") + "\" >");
            codeBuilder.IncreaseIndent();

            if (labelOnTop && !elementClass.In(LayoutControlClass.Chart, LayoutControlClass.CheckBox, LayoutControlClass.Dashboard, LayoutControlClass.Button, LayoutControlClass.Label, LayoutControlClass.TextBlock, LayoutControlClass.MultimediaControl) && control.SubstituteProperties.IsNullOrEmpty())
            {
                var showLabelTitle = (isConnected && !this._layOut.ShowAllLabelsForConnectedFields ? false : (control.DisplayName.IsNullOrEmpty() ? false : true));

                //content-required
                var dataBind = !ControlIsRequired(control, elementClass) ? "" : "css: { 'content-required': $root." + this.ViewModelName + "().status() === 'E' && !isEmptyEntityFn($data) }";
                //title
                dataBind = dataBind + (!showLabelTitle ? "" : (dataBind.IsNullOrEmpty() ? "" : ", ") + "attr: { title: $root." + this.ViewModelName + "().getLayoutDisplayName('" + controlName + "')}");
                //text
                dataBind = dataBind + (!showLabelTitle || !control.ToolTip.IsNullOrEmpty() ? "" : (dataBind.IsNullOrEmpty() ? "" : ", ") + "text: $root." + this.ViewModelName + "().getLayoutDisplayName('" + controlName + "')");
                dataBind = (!dataBind.IsNullOrEmpty() ? "data-bind=\"" : "") + dataBind + (!dataBind.IsNullOrEmpty() ? "\"" : "");

                codeBuilder.AddLine("<label " + dataBind + " id=\"" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "lbl") + "\" for=\"" + controlName + "\" " + "class=\"" + (labelOnTop ? "" : string.Format("col-md-{0} remove-plr text-right ", columnSpanLabel)) + "control-label ellipsis" + fontclass + "\">");

                if (!showLabelTitle)
                {
                    codeBuilder.AddLine("&nbsp;");
                }

                //Tooltip
                if (!control.ToolTip.IsNullOrEmpty())
                {
                    if (showLabelTitle)
                        codeBuilder.Add("<span data-bind =\"text: $root." + this.ViewModelName + "().getLayoutDisplayName('" + controlName + "')\" />");

                    codeBuilder.Add("<i class=\"fa fa-info linx-tooltip\" title=\"" + System.Net.WebUtility.HtmlEncode(control.ToolTip) + "\" />");
                }

                codeBuilder.AddLine("</label>");
            }

            if (!labelOnTop && elementClass.In(LayoutControlClass.CheckBox, LayoutControlClass.Dashboard, LayoutControlClass.Button, LayoutControlClass.Label, LayoutControlClass.TextBlock, LayoutControlClass.MultimediaControl))
                codeBuilder.AddLine("<label id=\"" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "lbl") + "\" for=\"" + controlName + "\" " + (!ControlIsRequired(control, elementClass) ? "" : "data-bind=\"css: { 'content-required': $root." + this.ViewModelName + "().status() === 'E' && !isEmptyEntityFn($data) }\" ") + "class=\"" + string.Format("col-md-{0} right ", columnSpanLabel) + "control-label\"></label>");

            #region Label case Lookup is SubstituteProperties
            if (elementClass == LayoutControlClass.LookUpTextBox && !control.SubstituteProperties.IsNullOrEmpty())
            {
                var showLabelTitle = !control.DisplayName.IsNullOrEmpty();

                //content-required
                var dataBind = !ControlIsRequired(control, elementClass) ? "" : "css: { 'content-required': $root." + this.ViewModelName + "().status() === 'E' && !isEmptyEntityFn($data) }";
                //title
                dataBind = dataBind + (!showLabelTitle ? "" : (dataBind.IsNullOrEmpty() ? "" : ", ") + "attr: { title: $root." + this.ViewModelName + "().getLayoutDisplayName('" + controlName + "')}");

                dataBind = (!dataBind.IsNullOrEmpty() ? "data-bind=\"" : "") + dataBind + (!dataBind.IsNullOrEmpty() ? "\"" : "");

                codeBuilder.AddLine("<label " + dataBind + " id=\"" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "lbl") + "\" for=\"" + controlName + "\" " + "class=\"" + (labelOnTop ? "" : string.Format("col-md-{0} right ", columnSpanLabel)) + "control-label ellipsis dropdown-toggle" + fontclass + "\" style=\"cursor: pointer\" data-toggle=\"dropdown\">" +
                    "<span id=\"" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "lblMV") + "\"" + (!showLabelTitle ? "" : " data-bind=\"text: $root." + this.ViewModelName + "().getLayoutDisplayName('" + controlName + "') \"") +
                     "></span><span class=\"fa fa-sort-asc\" style=\"padding-left:3px;\" /> " + (control.ToolTip.IsNullOrEmpty() ? "" : "<i class=\"fa fa-info linx-tooltip\" title=\"" + control.ToolTip + "\" />") + "</label>");
                codeBuilder.AddLine("<ul class=\"dropdown-menu\">");
                var itemNames = control.SubstituteProperties.Split(",".ToCharArray()).Select(i => i.Left(":")).ToList();
                itemNames.Add(control.BindingPath.Right("."));
                Action<string, string> actionAddItem = (string itemName, string itemDisplay) =>
                {
                    codeBuilder.AddLine("    <li><a href=\"#\" data-bind=\"click: function (s, e) { " +
                        "$data.clearLookUp('" + control.LookUpName + "'); " +
                        string.Join("", itemNames.Where(i => i != itemName).Select(i => string.Format("$('#{0}').addClass('hide');", control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "lUp") + i))) +
                        string.Format("$('#{0}').removeClass('hide');", control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "lUp") + itemName) +
                        "$('#" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "lblMV") + "').text('" + itemDisplay + "');  " +
                        "}\">" + itemDisplay + "</a></li>");
                };
                actionAddItem(control.Name, control.DisplayName);
                foreach (var item in control.SubstituteProperties.Split(",".ToCharArray()))
                {
                    actionAddItem(item.Left(":"), item.Right(":"));
                }
                codeBuilder.AddLine("</ul>");
            }
            #endregion

            if (!labelOnTop)
            {
                codeBuilder.AddLine("<div class=\"" + string.Format("col-md-{0} ", columnSpanMax - columnSpanLabel) + "\">");
                codeBuilder.IncreaseIndent();
            }

            #region Create Specific Controls
            switch (elementClass)
            {
                case LayoutControlClass.Button:
                    codeBuilder.AddLine("<button id=\"" + controlName + "\" class=\"form-control btn-block btn-press ellipsis " + "\" data-bind=\"text: $root." + this.ViewModelName + "().getLayoutDisplayName('" + controlName + "'), attr: {title: $root." + this.ViewModelName + "().getLayoutDisplayName('" + controlName + "')}, css: { IsEditableStyle: " + GetEditableBind(control, controlName) + " }, enable: " + GetEditableBind(control, controlName) + ", " + "click:function(){ if (typeof $root." + this.ViewModelName + "()." + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + "") + "_Click == 'function') $root." + this.ViewModelName + "()." + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + "") + "_Click(); if ($root." + this.ViewModelName + "().custom) $root." + this.ViewModelName + "().custom." + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + "") + "_Click({viewModel: $root." + this.ViewModelName + "()}); }\"></button>");
                    break;
                case LayoutControlClass.Chart:
                    ComposeChart(control, elementClass, codeBuilder);
                    break;
                case LayoutControlClass.CheckBox:
                    #region CheckBox
                    codeBuilder.AddLine("<label class=\"" + "\">");
                    codeBuilder.AddLine("  <input id=\"" + controlName + "\" type=\"checkbox\" " + (hasBind ? "data-bind=\"css: { IsEditableStyle: " + GetEditableBind(control, controlName) + "}, disable: " + GetReadOnlyBind(control, controlName) + ", nullableChecked: " + control.BindingPath.Right(".") + "\"" : "") + " title=\"" + "Clique para marcar/desmarcar".Translate() + "\"/> ");
                    codeBuilder.AddLine("  <span id=\"lbl" + controlName + "\" " + "data-bind=\"text: $root." + this.ViewModelName + "().getLayoutDisplayName('" + controlName + "'), attr: {title: $root." + this.ViewModelName + "().getLayoutDisplayName('" + controlName + "')}" + ((!ControlIsRequired(control, elementClass) ? "" : ", css: { 'content-required-checkbox': $root." + this.ViewModelName + "().status() === 'E'}") + "\" ") + "class=\"align-label-checkbox\"></span>");
                    codeBuilder.AddLine("</label>");
                    break;
                #endregion
                case LayoutControlClass.ChildToolBar:
                    break;
                case LayoutControlClass.ColorPicker:
                    break;
                case LayoutControlClass.CustomControl:
                    codeBuilder.AddLine("<div id=\"" + controlName + "\" \">");
                    codeBuilder.AddLine(control.HtmlCode);
                    codeBuilder.AddLine("</div>");
                    break;
                case LayoutControlClass.RadioButtonGroup:
                    if (hasBind)
                    {
                        codeBuilder.AddLine("<span id=\"" + controlName + "\" class=\"form-control \" data-bind=\"validatedField: '" + control.BindingPath.Right(".") + "', css: { IsEditableStyle: " + GetEditableBind(control, controlName) + ", activeReadonly: " + GetReadOnlyBind(control, controlName) + " }, readOnly: " + GetReadOnlyBind(control, controlName) + "\">");

                        codeBuilder.AddLine("<!-- ko foreach: $root." + this.ViewModelName + "().dataDomains.getItems('" + control.DomainName + "', '" + (control.DomainFilterValues ?? "") + "') -->");
                        codeBuilder.AddLine("     <label style=\"margin: 0;\" class=\"" + control.RadioPosition.ToLower() + "\">");
                        codeBuilder.AddLine("        <input style=\"vertical-align: middle; margin-top: -2px;\"  type=\"radio\" name=\"" + controlName + "_OG\" data-bind=\"attr: {value: $data.name}, checked: $parent." + control.BindingPath.Right(".") + "Name,  enable: " + GetEditableBind(control, controlName) + "\" />");
                        codeBuilder.AddLine("        <span style=\"vertical-align: middle; margin-right: 5px;\" data-bind=\"text: $data.name\" ></span>");
                        codeBuilder.AddLine("     </label>");
                        codeBuilder.AddLine("<!-- /ko -->");

                        codeBuilder.AddLine("</span>");
                    }
                    else
                    {
                        codeBuilder.AddLine("<span id=\"" + controlName + "\" class=\"form-control \"></span>");
                    }
                    break;
                case LayoutControlClass.ComboBox:
                    #region ComboBox
                    if (hasBind)
                    {
                        codeBuilder.AddLine("<span id=\"" + controlName + "\" class=\"form-control \" data-bind=\"validatedField: '" + control.BindingPath.Right(".") + "', css: { IsEditableStyle: " + GetEditableBind(control, controlName) + ", activeReadonly: " + GetReadOnlyBind(control, controlName) + " }, readOnly: " + GetReadOnlyBind(control, controlName) + ", igCombo: {");
                        if (control.DataFormatString == "none") codeBuilder.AddLine("   format: 'none',");

                        codeBuilder.AddLine("   selectedItems: " + control.BindingPath.Right(".") + ",");
                        if (control.DomainName.IsNullOrEmpty() && !control.LookUpName.IsNullOrEmpty())
                        {

                            codeBuilder.AddLine("   selectionChanged: function (evt, ui) {");
                            codeBuilder.AddLine("       if(ui.items == null || ui.items.length == 0){ ");
                            codeBuilder.AddLine("           $root." + this.ViewModelName + "().clearCombo($root." + this.ViewModelName + "()." + GetFullBindingPath(control.BindingPath, false, false) + "(), '" + control.LookUpName + "');");
                            codeBuilder.AddLine("           return; ");
                            codeBuilder.AddLine("       }");
                            codeBuilder.AddLine("       $root." + this.ViewModelName + "().finalizeCombo($root." + this.ViewModelName + "()." + GetFullBindingPath(control.BindingPath, false, false) + "(), ui.items[0].data, '" + control.LookUpName + "');");
                            codeBuilder.AddLine("   },");
                            codeBuilder.AddLine("   dropDownOpening: function (evt, ui) {");
                            if (control.HasLookupFilter)
                            {
                                codeBuilder.AddLine("       if(ui.owner.items() && ui.owner.items().length > 0 && !$root." + this.ViewModelName + "().dataCombo.isFilterChanged('" + control.LookUpName + "', $root." + this.ViewModelName + "()" + currentBinding.Replace("vm", "") + "())) return;");
                                codeBuilder.AddLine("       $root." + this.ViewModelName + "().dataCombo.fillDataCombos('" + control.LookUpName + "', '" + control.BindingPath.Right(".") + "', $root." + this.ViewModelName + "()" + currentBinding.Replace("vm", "") + "(), function (result){");
                                codeBuilder.AddLine("           if(!isNullOrEmpty(evt))");
                                codeBuilder.AddLine("              $('#" + controlName + "').one('igcombodatabound', function () { setTimeout(function () { $('#" + controlName + "').igCombo('openDropDown'); }, 0); });");
                                codeBuilder.AddLine("           $('#" + controlName + "').igCombo({ dataSource: $root." + this.ViewModelName + "().dataCombo.getItems('" + control.LookUpName + "', '') });");
                                codeBuilder.AddLine("       });");

                            }
                            else
                            {
                                codeBuilder.AddLine("       if($root." + this.ViewModelName + "().dataCombo.getItems('" + control.LookUpName + "', '').length == 0 ){");
                                codeBuilder.AddLine("          $root." + this.ViewModelName + "().dataCombo.fillDataCombos('" + control.LookUpName + "', '" + control.BindingPath.Right(".") + "', $root." + this.ViewModelName + "()" + currentBinding.Replace("vm", "") + "(), function (result){");
                                codeBuilder.AddLine("           if(!isNullOrEmpty(evt))");
                                codeBuilder.AddLine("           $('#" + controlName + "').one('igcombodatabound', function () { setTimeout(function () { $('#" + controlName + "').igCombo('openDropDown'); }, 10); });");
                                codeBuilder.AddLine("              $('#" + controlName + "').igCombo({ dataSource: $root." + this.ViewModelName + "().dataCombo.getItems('" + control.LookUpName + "', '') });");
                                codeBuilder.AddLine("          });");
                                codeBuilder.AddLine("       }");
                            }
                            //codeBuilder.AddLine("       },");
                            codeBuilder.AddLine("   },");
                            codeBuilder.AddLine("   dataSource: $root." + this.ViewModelName + "().dataCombo.getItems('" + control.LookUpName + "', ''),");
                            codeBuilder.AddLine("   textKey: '" + control.BindingPath.Right(".") + "',");
                            codeBuilder.AddLine("   valueKey: '" + control.BindingPath.Right(".") + "',");
                        }
                        else
                        {
                            if (!control.DomainName.IsNullOrEmpty())
                            {
                                codeBuilder.AddLine("   dataSource:  $root." + this.ViewModelName + "().dataDomains.getItems('" + control.DomainName + "', '" + (control.DomainFilterValues ?? "") + "'),");
                                codeBuilder.AddLine("   textKey: 'name',");
                                codeBuilder.AddLine("   valueKey: 'id',");
                            }
                        }

                        codeBuilder.AddLine("   allowCustomValue : true,");
                        codeBuilder.AddLine("   enableSelectionChangedUpdate: true,");
                        codeBuilder.AddLine("   enableClearButton: " + control.IsNullable.ToString().ToLower() + ",");
                        codeBuilder.AddLine("   dropDownOnFocus: true,");
                        codeBuilder.AddLine("   disabled: (" + GetReadOnlyBind(control, controlName) + "),");
                        codeBuilder.AddLine("   mode: 'editable',");
                        codeBuilder.AddLine("   width:'100%'");
                        codeBuilder.AddLine("}\"></span>");
                    }
                    else
                    {
                        codeBuilder.AddLine("<span id=\"" + controlName + "\" class=\"form-control \"></span>");
                    }
                    break;
                #endregion
                case LayoutControlClass.Dashboard:
                    #region Dashboard
                    #region GetPath
                    Func<LayoutControlV2, string> getBindingPath = (ctrl) =>
                    {
                        var bind = ctrl.BindingPath.Right(".");
                        if (ctrl.DataFormatString.IsNullOrEmpty())
                            return bind;
                        if (ctrl.DataType.Contains("DateTime"))
                            return "Globalize.format(getUTCDate(getAbsoluteValue(" + bind + ")), '" + ctrl.DataFormatString + "')";
                        else
                            return "Globalize.format(getAbsoluteValue(" + bind + "), '" + ctrl.DataFormatString + "')";
                    };
                    #endregion
                    string dashWidth = control.DashboardWidth == "Large" ? "col-md-12" : control.DashboardWidth == "Medium" ? "col-md-6" : "col-md-3";
                    codeBuilder.AddLine("<div id=\"" + controlName + "\" class=\"dashboard-stat " + (dashWidth ?? "") + "\" style=\"background-color:" + (control.DashboardBackgroundColorClassName.IsNullOrEmpty() ? "blue" : control.DashboardBackgroundColorClassName) + "\">");
                    codeBuilder.AddLine("    <div class=\"visual\">");
                    codeBuilder.AddLine("        <i class=\"fa " + control.DashboardIconFAName + "\"></i>");
                    codeBuilder.AddLine("    </div>");
                    codeBuilder.AddLine("    <div class=\"details\">");
                    codeBuilder.AddLine("        <div class=\"number\"><span id=\"" + controlName + "_value\"  " + (hasBind ? "data-bind=\"text: " + getBindingPath(control) + "\"" : "") + "/></div>");
                    codeBuilder.AddLine("        <div data-bind=\"text: $root." + this.ViewModelName + "().getLayoutDisplayName('" + controlName + "')\" class=\"desc\"></div>");
                    codeBuilder.AddLine("    </div>");
                    codeBuilder.AddLine("</div>");
                    break;
                #endregion
                case LayoutControlClass.DateTimeTextBox:
                    #region DateTimeTextBox

                    codeBuilder.AddLine("<div class=\"input-group\" >");
                    codeBuilder.IncreaseIndent();
                    bool isTypeTime = (GetFormatDataType(control) == "time" || GetFormatDataType(control) == "timeLong" ? true : false);
                    controlBinding = dataView + control.BindingPath.Right(".");

                    if (control.DisplayRangeDate)
                    {
                        var begin = "<span id=\"" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "dtb") + "\" class=\"date form-control campo-date-picker\" data-bind=\"css:{ igDatePickerReadOnlyStyle: " + GetReadOnlyBind(control, controlName) + " }, igDatePicker: { value: $root." + this.ViewModelName + "().entitySearchRange." + controlBinding + "_begin,  dateDisplayFormat: 'date', readOnly: $root." + this.ViewModelName + "().status() !== 'C', width: '100%', dateInputFormat: 'date', enableUTCDates: true, datepickerOptions: { changeMonth: true, changeYear: true }, valueChanged: function (evt, ui) {$('#" + controlName + "').igDatePicker('option', 'readOnly', (ui.value != null || $('#" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "dte") + "').igDatePicker('option', 'value') != null ? true : false)); var valorMin = $('#" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "dtb") + "').igDatePicker('value'); var valorMax = $('#" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "dte") + "').igDatePicker('value');if (valorMax != null && valorMin > valorMax)$('#" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "dtb") + "').igDatePicker('option', 'value', valorMax);} }\" />";
                        var end = "<span id=\"" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "dte") + "\" class=\"date form-control campo-date-picker\" data-bind=\"css:{ igDatePickerReadOnlyStyle: " + GetReadOnlyBind(control, controlName) + " }, igDatePicker: { value: $root." + this.ViewModelName + "().entitySearchRange." + controlBinding + "_end,  dateDisplayFormat: 'date', readOnly: $root." + this.ViewModelName + "().status() !== 'C', width: '100%', dateInputFormat: 'date', enableUTCDates: true, datepickerOptions: { changeMonth: true, changeYear: true },valueChanged: function (evt, ui) {$('#" + controlName + "').igDatePicker('option', 'readOnly', ($('#" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "dtb") + "').igDatePicker('option', 'value') != null || ui.value != null? true : false)); var valorMin = $('#" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "dtb") + "').igDatePicker('value'); var valorMax = $('#" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "dte") + "').igDatePicker('value');if (valorMin != null && valorMax < valorMin) $('#" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "dte") + "').igDatePicker('option', 'value', valorMin);} }\" />";

                        codeBuilder.AddLine("    <div class=\"row connected-field\"><div class=\"wrapper-campus\"><span class=\"txt-descricao\" title=\"Data inicial\">" + "De:".Translate() + "</span>{0}</div></div>", begin);
                        codeBuilder.AddLine("    <div class=\"row connected-field\"><div class=\"wrapper-campus\"><span class=\"txt-descricao\" title=\"Data final\">" + "Até:".Translate() + "</span>{0}</div></div>", end);
                    }
                    else
                    {

                        var bindingReadOnly = (control.HasFilterRange ? "$root." + this.ViewModelName + "().entitySearchRange.has_" + controlBinding + "()?true:" : "") + GetReadOnlyBind(control, controlName);
                        if (hasBind)
                            codeBuilder.AddLine("<span id=\"" + controlName + "\" class=\"date form-control " + (control.HasFilterRange ? "dateFilterRange " : "") + " \"  data-bind=\"" + (hasBind ? "validatedField: '" + control.BindingPath.Right(".") + "', " : "") + "css: { vmEditing: $root." + this.ViewModelName + "().status() === 'E', IsEditableStyle: " + GetEditableBind(control, controlName) + ", igDatePickerReadOnlyStyle: " + GetReadOnlyBind(control, controlName) + " },  igDatePicker: { value: " + (hasBind ? control.BindingPath.Right(".") : "null") + ", minValue: new Date(1900, 0, 1) , width: '100%'," + (isTypeTime ? "button: 'clear', " : "") + "dateDisplayFormat: '" + GetFormatDataType(control) + "', dateInputFormat: '" + GetFormatDataType(control) + "', dropDownOnReadOnly: false, readOnly: " + bindingReadOnly + ", display:'block', enableUTCDates: true, datepickerOptions: { changeMonth: true, changeYear: true }  }\" />");
                        else
                        {
                            codeBuilder.AddLine("<span id=\"" + controlName + "\" type=\"date\" class=\"date form-control \" ></span>");
                            codeBuilder.ComplementaryCode.AddLine(", renderControl" + controlName + ": function(vm){");
                            codeBuilder.ComplementaryCode.IncreaseIndent();
                            codeBuilder.ComplementaryCode.AddLine("   $('#" + controlName + "').igDatePicker({ minValue: new Date(1900, 0, 1) , width: '100%'," + (isTypeTime ? "button: 'clear', " : "") + "dateDisplayFormat: '" + GetFormatDataType(control) + "', dateInputFormat: '" + GetFormatDataType(control) + "', dropDownOnReadOnly: false , display:'block', enableUTCDates: true });");
                            codeBuilder.ComplementaryCode.DecreaseIndent();
                            codeBuilder.ComplementaryCode.Add("}");
                            codeBuilder.ComplementaryCalls.AddLine("complement.renderControl" + controlName + "();");
                        }
                        if (hasBind && control.HasFilterRange && !isTypeTime)
                        {
                            #region FilterRange

                            var begin = "<span id=\"" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "dtb") + "\" class=\"IsEditableStyle date\" data-bind=\"igDatePicker: { value: " + controlBinding + "_begin,  dateDisplayFormat: 'date', width: '100%', dateInputFormat: 'date', enableUTCDates: true, datepickerOptions: { changeMonth: true, changeYear: true }, valueChanged: function (evt, ui) {$('#" + controlName + "').igDatePicker('option', 'readOnly', (ui.value != null || $('#" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "dte") + "').igDatePicker('option', 'value') != null ? true : false)); var valorMin = $('#" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "dtb") + "').igDatePicker('value'); var valorMax = $('#" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "dte") + "').igDatePicker('value');if (valorMax != null && valorMin > valorMax){" + controlBinding + "_begin(valorMax); $('#" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "dtb") + "').igDatePicker('option', 'value', valorMax);}} }\" />";
                            var end = "<span id=\"" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "dte") + "\" class=\"IsEditableStyle date\" data-bind=\"igDatePicker: { value: " + controlBinding + "_end,  dateDisplayFormat: 'date', width: '100%', dateInputFormat: 'date', enableUTCDates: true, datepickerOptions: { changeMonth: true, changeYear: true }, valueChanged: function (evt, ui) {$('#" + controlName + "').igDatePicker('option', 'readOnly', ($('#" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "dtb") + "').igDatePicker('option', 'value') != null || ui.value != null? true : false)); var valorMin = $('#" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "dtb") + "').igDatePicker('value'); var valorMax = $('#" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "dte") + "').igDatePicker('value');if (valorMin != null && valorMax < valorMin){" + controlBinding + "_end(valorMin); $('#" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "dte") + "').igDatePicker('option', 'value', valorMin);}} }\" />";

                            codeBuilder.AddLine("<span class=\"input-group-btn" + "\" title=\"Filtro de datas\">");
                            codeBuilder.AddLine("   <button id=\"" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "rgbtn") + "\"type=\"button\" class=\"filterRange btn-linx btn-ok\" data-bind=\"css: { 'icon-filter': !$root." + this.ViewModelName + "().entitySearchRange.has_" + controlBinding + "(), 'icon-ok': $root." + this.ViewModelName + "().entitySearchRange.has_" + controlBinding + "(), hide: $root." + this.ViewModelName + "().status() !== 'C' }, popoverWithBind: { template: '#" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "rg") + "', vm: $root." + this.ViewModelName + "().entitySearchRange, ctrlName: '" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "rgc") + "', headerText:'Filtro de datas' }\"  /></span>");
                            codeBuilder.AddLine("   <script id=\"" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "rg") + "\" type=\"text/html\" tabindex=\"-1\">");

                            codeBuilder.AddLine("   <div class=\"range-radio-group\">");
                            codeBuilder.AddLine("       <input type=\"radio\" data-bind=\"enable: !has_" + controlBinding + "(), checked:" + controlBinding + "_typeRange\" value=\"R\" ><span class=\"align-label-checkbox\">Faixa de datas</span>");
                            codeBuilder.AddLine("       <input type=\"radio\" data-bind=\"enable: !has_" + controlBinding + "(), checked:" + controlBinding + "_typeRange\" value=\"P\" ><span class=\"align-label-checkbox\">Predefinidos</span>");
                            codeBuilder.AddLine("   </div>");

                            codeBuilder.AddLine("   <div class=\"range-input-group\" id=\"" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "divrg") + "\" >");
                            codeBuilder.AddLine("       <div class=\"filterByRange\" data-bind=\"visible: " + controlBinding + "_typeRange() == 'R'\">");
                            codeBuilder.AddLine("           <div class=\"row\"><span class=\"col-md-3 text-right\" title=\"Data inicial\">" + "De:".Translate() + "</span><div class=\"col-md-9\">{0}</div></div>", begin);
                            codeBuilder.AddLine("           <div class=\"row\"><span class=\"col-md-3 text-right\" title=\"Data final\">" + "Até:".Translate() + "</span><div class=\"col-md-9\">{0}</div></div>", end);
                            codeBuilder.AddLine("       </div>");
                            codeBuilder.AddLine("       <div class=\"filterByPredefined \" data-bind=\"visible: " + controlBinding + "_typeRange() == 'P'\">");
                            codeBuilder.AddLine("           <span class=\"cboPredefinedFilters IsEditableStyle\"  data-bind=\"igCombo: { selectedItems: " + controlBinding + "_predefFilter, textKey: 'text', valueKey: 'id', enableSelectionChangedUpdate: true, enableClearButton: " + control.IsNullable.ToString().ToLower() + ", dropDownOnFocus: true, mode: 'dropdown', dataSource: $root.predefinedFilters,   width:'100%' }\"></span>");
                            codeBuilder.AddLine("           <div class=\"row predefValue\" data-bind=\"visible: " + controlBinding + "_predefFilter() != null && " + controlBinding + "_predefFilter()[0] && " + controlBinding + "_predefFilter()[0][0] == 'X'\"><span style=\"margin-bottom: 4px;margin-left: 15px;margin-top: 5px\" class=\"col-md-12\" title=\"Valor do Parâmetro\">Valor:</span><div class=\"col-md-12\"><input class=\"predefValueControl IsEditableStyle ellipsis input-medium\" data-bind=\"igNumericEditor: { value: " + controlBinding + "_predefValue, minValue: 0, maxValue:1000, dataMode: 'short'}\" /> </div></div>");
                            codeBuilder.AddLine("       </div>");
                            codeBuilder.AddLine("   </div>");
                            codeBuilder.AddLine("</script>");
                            #endregion
                        }
                    }
                    codeBuilder.DecreaseIndent();
                    codeBuilder.AddLine("</div>");

                    break;
                #endregion
                case LayoutControlClass.EconomicGroup:
                    break;
                case LayoutControlClass.EditBox:
                    codeBuilder.AddLine("<textarea id=\"" + controlName + "\" class=\"form-control \" " + (control.GetPrecision() == 0 ? "" : " maxLength=\"" + control.GetPrecision().ToString() + "\"") + " data-bind=\"" + (hasBind ? "validatedField: '" + control.BindingPath.Right(".") + "', attr:{ readOnly: " + GetReadOnlyBind(control, controlName) + "}, css: { IsEditableStyle: " + GetEditableBind(control, controlName) + "}, " : "") + (hasBind ? " value: " + control.BindingPath.Right(".") : "") + "\" rows=\"" + (control.TotalLines == 0 ? 2 : control.TotalLines).ToString() + "\"/>");
                    break;
                case LayoutControlClass.Gauge:
                    ComposeGauge(control, elementClass, codeBuilder);
                    break;
                case LayoutControlClass.HtmlViewer:
                    break;
                case LayoutControlClass.KpiBox:
                case LayoutControlClass.Label:
                case LayoutControlClass.TextBlock:
                    codeBuilder.AddLine("<span id=\"" + controlName + "\" class=\"text " + (!string.IsNullOrWhiteSpace(fontclass) ? fontclass + " " : " bold ") + "\" " + (control.BindingPath.IsNullOrEmpty() ? " data-bind=\"text: $root." + this.ViewModelName + "().getLayoutDisplayName('" + controlName + "') \"" : " data-bind=\"text: " + control.BindingPath.Right(".") + "\"") + (control.BindingPath.IsNullOrEmpty() ? "></span>" : "/>"));
                    break;
                case LayoutControlClass.LookUpTextBox:
                    #region LookUpTextBox
                    var isMultiValue = !control.SubstituteProperties.IsNullOrEmpty();
                    var maxLength = GetMaxLengthNumeric(control);
                    var maxValue = getMaxValueByType(control);
                    var getAutoCompleteInfo = " enableAutoComplete:" + (control.EnableLookupAutoComplete && control.DataType.ToLower().Contains("string")).ToString().ToLower() + ", autoCompleteMaxResults: " + (control.LookupAutoCompleteMaxResults == 0 ? 7 : control.LookupAutoCompleteMaxResults).ToString() + ",";
                    if (isMultiValue)
                    {
                        var items = control.SubstituteProperties.Split(",".ToCharArray());
                        codeBuilder.AddLine("<div id=\"" + controlName + control.BindingPath.Right(".") + "\" class=\"form-control input-group \" data-bind=\"validatedField: '" + control.BindingPath.Right(".") + "', css: { vmEditing: $root." + this.ViewModelName + "().status() === 'E', IsEditableStyle: " + GetEditableBind(control, (controlName + control.BindingPath.Right("."))) + " }, lookupControl: { vm: $root." + this.ViewModelName + "(), value: " + control.BindingPath.Right(".") + (control.GetPrecision() == 0 ? "" : ", maxLength: " + control.GetPrecision().ToString()) + "," +
                            "lookupName: '" + control.LookUpName + "'," + getAutoCompleteInfo + " isMultiSelection: " + control.MultiSelection.ToString().ToLower() + " , multiSelectionValue: " + control.BindingPath.Right(".") + ", fieldName: '" + control.BindingPath.Right(".") + "', disabled:" + GetReadOnlyBind(control, controlName + control.BindingPath.Right(".")) + ", isNullable:" + control.IsNullable.ToString().ToLower() + ", allowMultiSelectionInSearch:" + control.AllowMultiSelectionInSearch.ToString().ToLower() + ", validateOnClearState:" + control.ValidateOnClearState.ToString().ToLower() + ", maxValue:" + maxValue + ", maxLength: " + maxLength + ", defaultValue: " + GetJSDefaultValueByType(control.DataType) + "}\" />");
                        foreach (var item in items)
                        {
                            if (string.IsNullOrWhiteSpace(item)) continue;
                            var itemName = item.Left(":");
                            var itemDisplay = item.Right(":");
                            codeBuilder.AddLine("<div id=\"" + controlName + itemName + "\" class=\"form-control input-group hide\" data-bind=\"validatedField: '" + control.BindingPath.Right(".") + "', css: { vmEditing: $root." + this.ViewModelName + "().status() === 'E', IsEditableStyle: " + GetEditableBind(control, controlName + itemName) + " }, lookupControl: { vm: $root." + this.ViewModelName + "(), value: " + itemName + (control.GetPrecision() == 0 ? "" : ", maxLength: " + control.GetPrecision().ToString()) + "," +
                                    "lookupName: '" + control.LookUpName + "'," + getAutoCompleteInfo + " isMultiSelection: " + control.MultiSelection.ToString().ToLower() + ", multiSelectionValue: " + (control.MultiSelection ? control.BindingPath.Right(".") : "''") + ", fieldName: '" + itemName + "', disabled:" + GetReadOnlyBind(control, controlName + itemName) + ", isNullable:" + control.IsNullable.ToString().ToLower() + ", allowMultiSelectionInSearch:" + control.AllowMultiSelectionInSearch.ToString().ToLower() + ", validateOnClearState:" + control.ValidateOnClearState.ToString().ToLower() + ", maxValue:" + maxValue + ", maxLength: " + maxLength + ", defaultValue: " + GetJSDefaultValueByType(control.DataType) + " }\" />");
                        }
                    }
                    else
                    {
                        codeBuilder.AddLine("<div id=\"" + controlName + "\" class=\"form-control input-group " + "\" data-bind=\"validatedField: '" + control.BindingPath.Right(".") + "', css: { vmEditing: $root." + this.ViewModelName + "().status() === 'E', IsEditableStyle: " + GetEditableBind(control, controlName) + " }, lookupControl: { vm: $root." + this.ViewModelName + "(), value: " + control.BindingPath.Right(".") + (control.GetPrecision() == 0 ? "" : ", maxLength: " + control.GetPrecision().ToString()) + "," +
                        "lookupName: '" + control.LookUpName + "'," + getAutoCompleteInfo + " isMultiSelection: " + control.MultiSelection.ToString().ToLower() + ", multiSelectionValue: $root." + this.ViewModelName + "().entitySearchRange." + control.BindingPath.Right(".") + ", fieldName: '" + control.BindingPath.Right(".") + "', disabled:" + GetReadOnlyBind(control, controlName) + ", isNullable:" + control.IsNullable.ToString().ToLower() + ", allowMultiSelectionInSearch:" + control.AllowMultiSelectionInSearch.ToString().ToLower() + ", validateOnClearState:" + control.ValidateOnClearState.ToString().ToLower() + ", maxValue:" + maxValue + ", maxLength: " + maxLength + ", defaultValue: " + GetJSDefaultValueByType(control.DataType) + "}\" />");
                    }
                    break;
                #endregion
                case LayoutControlClass.MaskedTextBox:
                    codeBuilder.AddLine("<input id=\"" + controlName + "\" class=\"form-control \" data-bind=\"" + (hasBind ? "validatedField: '" + control.BindingPath.Right(".") + "', css: { IsEditableStyle: " + GetEditableBind(control, controlName) + " }, enable: " + GetEditableBind(control, controlName) + ", " : "") + "igMaskEditor: { " + (hasBind ? "value: " + control.BindingPath.Right(".") + ", " : "") + "dataMode: 'rawText', inputMask: '" + control.Mask + "', excludeKeys: ''" + (hasBind ? ", readOnly: " + GetReadOnlyBind(control, controlName) : "") + " }\" />");
                    break;
                case LayoutControlClass.MultimediaControl:
                    if (hasBind)
                        codeBuilder.AddLine("<div id=\"" + controlName + "\" class=\"form-control  " + GetMediaWidth(control.MediaWidth) + "\" data-bind=\"template: { name: getTemplateImageName($root." + this.ViewModelName + "().status(), 'form'), afterRender: function (element, data) { KO_afterRenderImageTemplate(data, element, '" + control.Name.Left(".") + "', getAbsoluteValue(" + control.BindingPath.Right(".") + "), $root." + this.ViewModelName + "(), $root) } }\" title=\"" + control.DisplayName + "\"></div>");
                    else
                        codeBuilder.AddLine("<img data-bind=\"attr: { alt: $root." + this.ViewModelName + "().getLayoutDisplayName('" + controlName + "'), title: $root." + this.ViewModelName + "().getLayoutDisplayName('" + controlName + "') }\" id=\"" + controlName + "\" class=\"form-control  " + GetMediaWidth(control.MediaWidth) + "\"/>");
                    break;
                case LayoutControlClass.NumericTextBox:
                    #region NumericTextBox
                    #region get type by propertyType
                    string type = getNumericDataMode(control);
                    #endregion



                    string minMaxValue = GetControlRange(control);
                    controlBinding = dataView + control.BindingPath.Right(".");
                    codeBuilder.AddLine("<div class=\"input-group\">");
                    codeBuilder.IncreaseIndent();
                    if (control.DisplayRangeDate)
                    {

                        string begin = string.Empty;
                        string end = string.Empty;
                        if (!control.DataFormatString.IsNullOrEmpty() && control.DataFormatString.Left(1).ToLower() == "p")
                        {
                            begin += "<input id=\"" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "ntx") + "\" class=\"campo-date-picker " + (control.HasFilterRange ? " numericFilterRange " : "") + "\" data-bind=\"igPercentEditor: {" + (control.DataFormatString == "none" ? "groupSeparator: ''," : "") + " value: $root." + this.ViewModelName + "().entitySearchRange." + binding + "_begin" + (control.AllowNegativeValue ? "" : ", readOnly: $root." + this.ViewModelName + "().status() !== 'C', minValue: 0, width: '100%'") + (control.GetPrecision() == 0 ? "" : ", maxLength: " + (control.GetPrecision() - control.GetPrecisionDecimalsInt()).ToString()) + ",  displayFactor : 1, dataMode: 'number', minDecimals: " + control.GetPrecisionDecimalsToString(this.ViewModelName) + ", maxDecimals: " + control.GetPrecisionDecimalsToString(this.ViewModelName) + ", valueChanged: function (evt, ui) {$('#" + controlName + "').igPercentEditor('option', 'readOnly', (ui.value != null || $('#" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "ntxe") + "').igPercentEditor('option', 'value') != null ? true : false)); $('#" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "ntxe") + "').igPercentEditor('option', 'minValue', ui.value); } }\" />";
                            end += "<input id=\"" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "ntxe") + "\" class=\"campo-date-picker " + (control.HasFilterRange ? " numericFilterRange " : "") + "\" data-bind=\"igPercentEditor: {" + (control.DataFormatString == "none" ? "groupSeparator: ''," : "") + " value: $root." + this.ViewModelName + "().entitySearchRange." + binding + "_end" + (control.AllowNegativeValue ? "" : ", readOnly: $root." + this.ViewModelName + "().status() !== 'C', minValue: 0, width: '100%'") + (control.GetPrecision() == 0 ? "" : ", maxLength: " + (control.GetPrecision() - control.GetPrecisionDecimalsInt()).ToString()) + ",  displayFactor : 1, dataMode: 'number', minDecimals: " + control.GetPrecisionDecimalsToString(this.ViewModelName) + ", maxDecimals: " + control.GetPrecisionDecimalsToString(this.ViewModelName) + ", valueChanged: function (evt, ui) {$('#" + controlName + "').igPercentEditor('option', 'readOnly', ($('#" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "ntx") + "').igPercentEditor('option', 'value') != null || ui.value != null? true : false)); $('#" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "ntx") + "').igPercentEditor('option', 'maxValue', ui.value); } }\" />";
                        }
                        else if (!control.DataFormatString.IsNullOrEmpty() && control.DataFormatString.Left(1).ToLower() == "c")
                        {
                            begin += "<input id=\"" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "ntx") + "\" class=\"campo-date-picker " + (control.HasFilterRange ? " numericFilterRange " : "") + "\" data-bind=\"igCurrencyEditor: {" + (control.DataFormatString == "none" ? "groupSeparator: ''," : "") + " value: $root." + this.ViewModelName + "().entitySearchRange." + binding + "_begin" + (control.AllowNegativeValue ? "" : ", readOnly: $root." + this.ViewModelName + "().status() !== 'C', minValue: 0, width: '100%'") + (control.GetPrecision() == 0 ? "" : ", maxLength: " + (control.GetPrecision() - control.GetPrecisionDecimalsInt()).ToString()) + ", dataMode: 'number', minDecimals: " + control.GetPrecisionDecimalsToString(this.ViewModelName) + ", maxDecimals: " + control.GetPrecisionDecimalsToString(this.ViewModelName) + ", valueChanged: function (evt, ui) {$('#" + controlName + "').igCurrencyEditor('option', 'readOnly', (ui.value != null || $('#" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "ntxe") + "').igCurrencyEditor('option', 'value') != null ? true : false)); $('#" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "ntxe") + "').igCurrencyEditor('option', 'minValue', ui.value); } }\" />";
                            end += "<input id=\"" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "ntxe") + "\" class=\"campo-date-picker " + (control.HasFilterRange ? " numericFilterRange " : "") + "\" data-bind=\"igCurrencyEditor: {" + (control.DataFormatString == "none" ? "groupSeparator: ''," : "") + " value: $root." + this.ViewModelName + "().entitySearchRange." + binding + "_end" + (control.AllowNegativeValue ? "" : ", readOnly: $root." + this.ViewModelName + "().status() !== 'C', minValue: 0, width: '100%'") + (control.GetPrecision() == 0 ? "" : ", maxLength: " + (control.GetPrecision() - control.GetPrecisionDecimalsInt()).ToString()) + ", dataMode: 'number', minDecimals: " + control.GetPrecisionDecimalsToString(this.ViewModelName) + ", maxDecimals: " + control.GetPrecisionDecimalsToString(this.ViewModelName) + ", valueChanged: function (evt, ui) {$('#" + controlName + "').igCurrencyEditor('option', 'readOnly', ($('#" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "ntx") + "').igCurrencyEditor('option', 'value') != null || ui.value != null? true : false)); $('#" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "ntx") + "').igCurrencyEditor('option', 'maxValue', ui.value); } }\" />";
                        }
                        else
                        {
                            begin += "<input id=\"" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "ntx") + "\" class=\"campo-date-picker " + (control.HasFilterRange ? " numericFilterRange " : "") + "\" data-bind=\"igNumericEditor: {" + (control.DataFormatString == "none" ? "groupSeparator: ''," : "") + " value: $root." + this.ViewModelName + "().entitySearchRange." + binding + "_begin" + (control.AllowNegativeValue ? "" : ", readOnly: $root." + this.ViewModelName + "().status() !== 'C', minValue: 0, width: '100%'") + (control.GetPrecision() == 0 ? "" : ", maxLength: " + (control.GetPrecision() - control.GetPrecisionDecimalsInt()).ToString()) + (control.DataType.RemoveNullDefinition().InList(new string[] { "byte", "int16", "int32", "int", "long", "short", "int64", "sbyte", "uint16", "uint32", "uint64" }) ? ", dataMode: '" + type + "'" : ", dataMode: 'number', selectionOnFocus: 'atEnd', minDecimals: " + control.GetPrecisionDecimalsToString(this.ViewModelName) + ", maxDecimals: " + control.GetPrecisionDecimalsToString(this.ViewModelName)) + ", valueChanged: function (evt, ui) {$('#" + controlName + "').igNumericEditor('option', 'readOnly', (ui.value != null || $('#" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "ntxe") + "').igNumericEditor('option', 'value') != null ? true : false)); $('#" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "ntxe") + "').igNumericEditor('option', 'minValue', ui.value); } }\" />";
                            end += "<input id=\"" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "ntxe") + "\" class=\"campo-date-picker " + (control.HasFilterRange ? " numericFilterRange " : "") + "\" data-bind=\"igNumericEditor: {" + (control.DataFormatString == "none" ? "groupSeparator: ''," : "") + " value: $root." + this.ViewModelName + "().entitySearchRange." + binding + "_end" + (control.AllowNegativeValue ? "" : ", readOnly: $root." + this.ViewModelName + "().status() !== 'C', minValue: 0, width: '100%'") + (control.GetPrecision() == 0 ? "" : ", maxLength: " + (control.GetPrecision() - control.GetPrecisionDecimalsInt()).ToString()) + (control.DataType.RemoveNullDefinition().InList(new string[] { "byte", "int16", "int32", "int", "long", "short", "int64", "sbyte", "uint16", "uint32", "uint64" }) ? ", dataMode: '" + type + "'" : ", dataMode: 'number', selectionOnFocus: 'atEnd', minDecimals: " + control.GetPrecisionDecimalsToString(this.ViewModelName) + ", maxDecimals: " + control.GetPrecisionDecimalsToString(this.ViewModelName)) + ", valueChanged: function (evt, ui) {$('#" + controlName + "').igNumericEditor('option', 'readOnly', ($('#" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "ntx") + "').igNumericEditor('option', 'value') != null || ui.value != null? true : false)); $('#" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "ntx") + "').igNumericEditor('option', 'maxValue', ui.value); } }\" />";
                        }

                        codeBuilder.AddLine("    <div class=\"row connected-field\"><div class=\"wrapper-campus\"><span class=\"txt-descricao\" title=\"Valor inicial\">" + "De:".Translate() + "</span>{0}</div></div>", begin);
                        codeBuilder.AddLine("    <div class=\"row connected-field\"><div class=\"wrapper-campus\"><span class=\"txt-descricao\" title=\"Valor final\">" + "Até:".Translate() + "</span>{0}</div></div>", end);

                    }
                    else
                    {
                        if (!control.DataFormatString.IsNullOrEmpty() && control.DataFormatString.Left(1).ToLower() == "p")
                            codeBuilder.AddLine("<input id=\"" + controlName + "\" class=\"form-control " + (control.HasFilterRange ? "numericFilterRange " : "") + "\" data-bind=\"" + (hasBind ? "validatedField: '" + control.BindingPath.Right(".") + "', css: { vmEditing: $root." + this.ViewModelName + "().status() === 'E', IsEditableStyle: " + GetEditableBind(control, controlName) + " }, " : "") + "igPercentEditor: { " + (control.DataFormatString == "none" ? "groupSeparator: ''," : "") + (hasBind ? "value: " + control.BindingPath.Right(".") + "," : "") + (minMaxValue.IsNullOrEmpty() || !minMaxValue.Contains("maxValue") ? (control.AllowNegativeValue ? "" : " minValue: 0,") + " " + GetMaxValueNumeric(control, true, false, true) : minMaxValue) + " maxLength: " + GetMaxLengthNumeric(control) + ", displayFactor : 1, dataMode: 'number', minDecimals: " + control.GetPrecisionDecimalsToString(this.ViewModelName) + ", maxDecimals: " + control.GetPrecisionDecimalsToString(this.ViewModelName) + (hasBind ? ", readOnly: " + GetReadOnlyBind(control, controlName) : "") + " }\" />");
                        else if (!control.DataFormatString.IsNullOrEmpty() && control.DataFormatString.Left(1).ToLower() == "c")
                            codeBuilder.AddLine("<input id=\"" + controlName + "\" class=\"form-control " + (control.HasFilterRange ? "numericFilterRange " : "") + "\" data-bind=\"" + (hasBind ? "validatedField: '" + control.BindingPath.Right(".") + "', css: { vmEditing: $root." + this.ViewModelName + "().status() === 'E', IsEditableStyle: " + GetEditableBind(control, controlName) + " }, " : "") + "igCurrencyEditor: { " + (control.DataFormatString == "none" ? "groupSeparator: ''," : "") + (hasBind ? "value: " + control.BindingPath.Right(".") + "," : "") + (minMaxValue.IsNullOrEmpty() || !minMaxValue.Contains("maxValue") ? (control.AllowNegativeValue ? "" : " minValue: 0,") + " " + GetMaxValueNumeric(control, true, false, true) : minMaxValue) + " maxLength: " + GetMaxLengthNumeric(control) + ", dataMode: 'number', minDecimals: " + control.GetPrecisionDecimalsToString(this.ViewModelName) + ", maxDecimals: " + control.GetPrecisionDecimalsToString(this.ViewModelName) + (hasBind ? ", readOnly: " + GetReadOnlyBind(control, controlName) : "") + " }\" />");
                        else
                            codeBuilder.AddLine("<input id=\"" + controlName + "\" class=\"form-control " + (control.HasFilterRange ? "numericFilterRange " : "") + "\" data-bind=\"" + (hasBind ? "validatedField: '" + control.BindingPath.Right(".") + "', css: { vmEditing: $root." + this.ViewModelName + "().status() === 'E', IsEditableStyle: " + GetEditableBind(control, controlName) + " }, " : "") + "igNumericEditor: { " + (control.DataFormatString == "none" ? "groupSeparator: ''," : "") + (hasBind ? "value: " + control.BindingPath.Right(".") + "," : "") + (minMaxValue.IsNullOrEmpty() || !minMaxValue.Contains("maxValue") ? (control.AllowNegativeValue ? "" : " minValue: 0,") + " " + GetMaxValueNumeric(control, true, false, true) : minMaxValue) + " maxLength: " + GetMaxLengthNumeric(control) + ", " + (control.DataType.RemoveNullDefinition().InList(new string[] { "byte", "int16", "int32", "int", "long", "short", "int64", "sbyte", "uint16", "uint32", "uint64" }) ? " dataMode: '" + type + "'" : " dataMode: 'number', minDecimals: " + control.GetPrecisionDecimalsToString(this.ViewModelName) + ", maxDecimals: " + control.GetPrecisionDecimalsToString(this.ViewModelName)) + (hasBind ? ", readOnly: " + GetReadOnlyBind(control, controlName) + ", selectionOnFocus: 'atEnd'" : "") + " }\" />");


                        if (hasBind && control.HasFilterRange)
                        {
                            #region Filter Range
                            string begin = string.Empty;
                            string end = string.Empty;
                            if (!control.DataFormatString.IsNullOrEmpty() && control.DataFormatString.Left(1).ToLower() == "p")
                            {
                                begin += "<input id=\"" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "ntxb") + "\" class=\"IsEditableStyle\" data-bind=\"igPercentEditor: { value: " + controlBinding + "_begin" + (control.AllowNegativeValue ? "" : ", minValue: 0") + (control.GetPrecision() == 0 ? "" : ", maxLength: " + (control.GetPrecision() - control.GetPrecisionDecimalsInt()).ToString()) + ",  displayFactor : 1, dataMode: 'number', minDecimals: " + control.GetPrecisionDecimalsToString(this.ViewModelName) + ", maxDecimals: " + control.GetPrecisionDecimalsToString(this.ViewModelName) + ", valueChanged: function (evt, ui) {$('#" + controlName + "').igPercentEditor('option', 'readOnly', (ui.value != null || $('#" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "ntxe") + "').igPercentEditor('option', 'value') != null ? true : false)); $('#" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "ntxe") + "').igPercentEditor('option', 'minValue', ui.value); } }\" />";
                                end += "<input id=\"" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "ntxe") + "\" class=\"IsEditableStyle\" data-bind=\"igPercentEditor: { value: " + controlBinding + "_end" + (control.AllowNegativeValue ? "" : ", minValue: 0") + (control.GetPrecision() == 0 ? "" : ", maxLength: " + (control.GetPrecision() - control.GetPrecisionDecimalsInt()).ToString()) + ",  displayFactor : 1, dataMode: 'number', minDecimals: " + control.GetPrecisionDecimalsToString(this.ViewModelName) + ", maxDecimals: " + control.GetPrecisionDecimalsToString(this.ViewModelName) + ", valueChanged: function (evt, ui) {$('#" + controlName + "').igPercentEditor('option', 'readOnly', ($('#" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "ntxb") + "').igPercentEditor('option', 'value') != null || ui.value != null? true : false)); $('#" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "ntxb") + "').igPercentEditor('option', 'maxValue', ui.value); } }\" />";
                            }
                            else if (!control.DataFormatString.IsNullOrEmpty() && control.DataFormatString.Left(1).ToLower() == "c")
                            {
                                begin += "<input id=\"" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "ntxb") + "\" class=\"IsEditableStyle\" data-bind=\"igCurrencyEditor: { value: " + controlBinding + "_begin" + (control.AllowNegativeValue ? "" : ", minValue: 0") + (control.GetPrecision() == 0 ? "" : ", maxLength: " + (control.GetPrecision() - control.GetPrecisionDecimalsInt()).ToString()) + ", dataMode: 'number', minDecimals: " + control.GetPrecisionDecimalsToString(this.ViewModelName) + ", maxDecimals: " + control.GetPrecisionDecimalsToString(this.ViewModelName) + ", valueChanged: function (evt, ui) {$('#" + controlName + "').igCurrencyEditor('option', 'readOnly', (ui.value != null || $('#" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "ntxe") + "').igCurrencyEditor('option', 'value') != null ? true : false)); $('#" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "ntxe") + "').igCurrencyEditor('option', 'minValue', ui.value); } }\" />";
                                end += "<input id=\"" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "ntxe") + "\" class=\"IsEditableStyle\" data-bind=\"igCurrencyEditor: { value: " + controlBinding + "_end" + (control.AllowNegativeValue ? "" : ", minValue: 0") + (control.GetPrecision() == 0 ? "" : ", maxLength: " + (control.GetPrecision() - control.GetPrecisionDecimalsInt()).ToString()) + ", dataMode: 'number', minDecimals: " + control.GetPrecisionDecimalsToString(this.ViewModelName) + ", maxDecimals: " + control.GetPrecisionDecimalsToString(this.ViewModelName) + ", valueChanged: function (evt, ui) {$('#" + controlName + "').igCurrencyEditor('option', 'readOnly', ($('#" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "ntxb") + "').igCurrencyEditor('option', 'value') != null || ui.value != null? true : false)); $('#" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "ntxb") + "').igCurrencyEditor('option', 'maxValue', ui.value); } }\" />";
                            }
                            else
                            {
                                begin += "<input id=\"" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "ntxb") + "\" class=\"IsEditableStyle\" data-bind=\"igNumericEditor: {" + (control.DataFormatString == "none" ? "groupSeparator: ''," : "") + " value: " + controlBinding + "_begin" + (control.AllowNegativeValue ? "" : ", minValue: 0") + (control.GetPrecision() == 0 ? "" : ", maxLength: " + (control.GetPrecision() - control.GetPrecisionDecimalsInt()).ToString()) + (control.DataType.RemoveNullDefinition().InList(new string[] { "byte", "int16", "int32", "int", "long", "short", "int64", "sbyte", "uint16", "uint32", "uint64" }) ? ", dataMode: '" + type + "'" : ", dataMode: 'number', minDecimals: " + control.GetPrecisionDecimalsToString(this.ViewModelName) + ", maxDecimals: " + control.GetPrecisionDecimalsToString(this.ViewModelName)) + ", valueChanged: function (evt, ui) {$('#" + controlName + "').igNumericEditor('option', 'readOnly', (ui.value != null || $('#" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "ntxe") + "').igNumericEditor('option', 'value') != null ? true : false)); $('#" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "ntxe") + "').igNumericEditor('option', 'minValue', ui.value); } }\" />";
                                end += "<input id=\"" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "ntxe") + "\" class=\"IsEditableStyle\" data-bind=\"igNumericEditor: {" + (control.DataFormatString == "none" ? "groupSeparator: ''," : "") + " value: " + controlBinding + "_end" + (control.AllowNegativeValue ? "" : ", minValue: 0") + (control.GetPrecision() == 0 ? "" : ", maxLength: " + (control.GetPrecision() - control.GetPrecisionDecimalsInt()).ToString()) + (control.DataType.RemoveNullDefinition().InList(new string[] { "byte", "int16", "int32", "int", "long", "short", "int64", "sbyte", "uint16", "uint32", "uint64" }) ? ", dataMode: '" + type + "'" : ", dataMode: 'number', minDecimals: " + control.GetPrecisionDecimalsToString(this.ViewModelName) + ", maxDecimals: " + control.GetPrecisionDecimalsToString(this.ViewModelName)) + ", valueChanged: function (evt, ui) {$('#" + controlName + "').igNumericEditor('option', 'readOnly', ($('#" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "ntxb") + "').igNumericEditor('option', 'value') != null || ui.value != null? true : false)); $('#" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "ntxb") + "').igNumericEditor('option', 'maxValue', ui.value); } }\" />";
                            }

                            codeBuilder.AddLine("<span class=\"input-group-btn" + "\" title=\"Filtro numérico\">");
                            codeBuilder.AddLine("   <button type=\"button\" class=\"filterRange btn-linx btn-ok\" data-bind=\"css: { 'icon-filter': !$root." + this.ViewModelName + "().entitySearchRange.has_" + controlBinding + "(), 'icon-ok': $root." + this.ViewModelName + "().entitySearchRange.has_" + controlBinding + "(), hide: $root." + this.ViewModelName + "().status() !== 'C' }, popoverWithBind: { template: '#" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "rg") + "', vm: $root." + this.ViewModelName + "().entitySearchRange, ctrlName: '" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "rgc") + "', headerText:'Filtro numérico' }\"  /></span>");
                            codeBuilder.AddLine("<script id=\"" + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "rg") + "\" type=\"text/html\" tabindex=\"-1\">");
                            codeBuilder.AddLine("    <div class=\"row\"><span class=\"col-md-3 text-right\" title=\"Valor inicial\">" + "De:".Translate() + "</span><div class=\"col-md-9\">{0}</div></div>", begin);
                            codeBuilder.AddLine("    <p></p>");
                            codeBuilder.AddLine("    <div class=\"row\"><span class=\"col-md-3 text-right\" title=\"Valor final\">" + "Até:".Translate() + "</span><div class=\"col-md-9\">{0}</div></div>", end);
                            codeBuilder.AddLine("</script>");
                            #endregion
                        }
                    }


                    codeBuilder.DecreaseIndent();
                    codeBuilder.AddLine("</div>");

                    break;
                #endregion
                case LayoutControlClass.TextBox:
                    #region TextBox
                    if (control.IsPassword)
                        codeBuilder.AddLine("<input id=\"" + controlName + "\" type=\"password\"" + (control.GetPrecision() == 0 ? "" : " maxLength=\"" + control.GetPrecision().ToString() + "\"") + " class=\"form-control \" data-bind=\"" + (hasBind ? "validatedField: '" + control.BindingPath.Right(".") + "', css: { IsEditableStyle: " + GetEditableBind(control, controlName) + " }, readOnly: " + GetReadOnlyBind(control, controlName) + ", disabled: " + GetEditableBind(control, controlName) + ", value: " + control.BindingPath.Right(".") : "") + "\"/>");
                    else
                        codeBuilder.AddLine("<input id=\"" + controlName + "\" class=\"form-control ellipsis" + "\" data-bind=\"" + (hasBind ? "attr: { title: " + control.BindingPath.Right(".") + " }, validatedField: '" + control.BindingPath.Right(".") + "', css: { IsEditableStyle: " + GetEditableBind(control, controlName) + " }, " : "") + "igTextEditor: { " + (control.GetPrecision() == 0 ? "" : "maxLength: " + control.GetPrecision().ToString() + ", ") + (hasBind ? "value: " + control.BindingPath.Right(".") + ", readOnly: " + GetReadOnlyBind(control, controlName) + ", selectionOnFocus: 'atEnd'" : "") + " }\"/>");
                    break;
                #endregion
                default:
                    break;
            }

            #endregion Create Specific Controls                      

            codeBuilder.DecreaseIndent();
            codeBuilder.AddLine("</div>");

            //if (elementClass != LayoutControlClass.Button)
            //{
            codeBuilder.DecreaseIndent();
            codeBuilder.AddLine("</div>");
            //}

            if (labelOnTop)
            {
                codeBuilder.DecreaseIndent();
                codeBuilder.AddLine("</div>");
            }

            if (connectedControls != null && connectedControls.Count > 0)
            {
                foreach (var connectedControl in connectedControls)
                {
                    ComposeControl(connectedControl.Key, connectedControl.Value, codeBuilder, true, null, labelOnTop, isTemplate);
                }
            }

            if (!labelOnTop)
            {
                codeBuilder.DecreaseIndent();
                codeBuilder.AddLine("</div>");
            }

        }

        private bool ControlIsRequired(LayoutControlV2 control, LayoutControlClass elementClass)
        {
            return !control.IsNullable && !elementClass.In(LayoutControlClass.Button, LayoutControlClass.CustomControl, LayoutControlClass.HtmlViewer);
        }

        private string getMaxValueByType(LayoutControlV2 control)
        {
            string maxValue = "";
            string type = control.DataType.RemoveNullDefinition();
            switch (type)
            {
                case "sbyte": maxValue = sbyte.MaxValue.ToString(); break;
                case "byte": maxValue = byte.MaxValue.ToString(); break;
                case "double": maxValue = double.MaxValue.ToString(); break;
                case "float": maxValue = float.MaxValue.ToString(); break;
                case "short":
                case "int16": maxValue = short.MaxValue.ToString(); break;
                case "ushort":
                case "uint16": maxValue = ushort.MaxValue.ToString(); break;
                case "int":
                case "int32": maxValue = int.MaxValue.ToString(); break;
                case "uint":
                case "uint32": maxValue = uint.MaxValue.ToString(); break;
                case "long":
                case "int64": maxValue = cMaxValueLongType.ToString(); break;
                case "ulong":
                case "uint64": maxValue = cMaxValueLongType.ToString(); break;
                default: maxValue = GetMaxValueNumeric(control, false, false, false); break;
            }

            return maxValue;
        }

        private static string getNumericDataMode(LayoutControlV2 control)
        {
            string type = control.DataType.RemoveNullDefinition();
            switch (type)
            {
                case "decimal": type = "decimal"; break;
                case "sbyte": type = "sbyte"; break;
                case "byte": type = "byte"; break;
                case "double": type = "double"; break;
                case "float": type = "float"; break;
                case "short":
                case "int16": type = "short"; break;
                case "ushort":
                case "uint16": type = "ushort"; break;
                case "int":
                case "int32": type = "int"; break;
                case "uint":
                case "uint32": type = "uint"; break;
                case "long":
                case "int64": type = "long"; break;
                case "ulong":
                case "uint64": type = "ulong"; break;
                default: type = "int"; break;
            }
            return type;
        }

        public override void ComposeContainerEndColumn(LayoutContainer container, LayoutContainerClass elementClass, Tools.CodeBuilder codeBuilder, int column)
        {
            if (!IsButtonContainer(container))
            {
                switch (elementClass)
                {
                    case LayoutContainerClass.CustomContainer:
                    case LayoutContainerClass.TreeListView:
                    case LayoutContainerClass.DataGrid:
                    case LayoutContainerClass.GroupBox:
                    case LayoutContainerClass.Expander:
                    case LayoutContainerClass.DockItem:
                    case LayoutContainerClass.WizardItem:
                    case LayoutContainerClass.OlapPivotGrid:
                    case LayoutContainerClass.FlatPivotGrid:
                    case LayoutContainerClass.PivotChart:
                    case LayoutContainerClass.PivotDrillDownChart:
                    case LayoutContainerClass.TabItem:
                        this.ComposeGenericContainerEnd(container, elementClass, codeBuilder, column);
                        break;
                    case LayoutContainerClass.WizardControl:
                    case LayoutContainerClass.DockManager:
                    case LayoutContainerClass.TabControl:
                        break;

                    default:
                        break;
                }
            }
        }

        private void ComposeGenericContainerEnd(LayoutContainer container, LayoutContainerClass elementClass, Tools.CodeBuilder codeBuilder, int column)
        {
            codeBuilder.DecreaseIndent();
            codeBuilder.AddLine("</div>");
        }

        public override void ComposeContainerEndRow(LayoutContainer container, LayoutContainerClass elementClass, Tools.CodeBuilder codeBuilder, int row)
        {
        }


        #region ComposeContainerEnd

        public override void ComposeContainerEnd(LayoutContainer container, LayoutContainerClass elementClass, Tools.CodeBuilder codeBuilder)
        {
            switch (elementClass)
            {
                case LayoutContainerClass.WizardControl:
                    this.ComposeWizardNavigation(container, codeBuilder);
                    this.ComposeDefaultContainerEnd(codeBuilder, container, elementClass);
                    break;
                case LayoutContainerClass.CustomContainer:
                case LayoutContainerClass.DataGrid:
                case LayoutContainerClass.DockItem:
                case LayoutContainerClass.DockManager:
                case LayoutContainerClass.FlatPivotGrid:
                case LayoutContainerClass.OlapPivotGrid:
                case LayoutContainerClass.PivotChart:
                case LayoutContainerClass.PivotDrillDownChart:
                case LayoutContainerClass.TabControl:
                case LayoutContainerClass.TreeListView:
                case LayoutContainerClass.WizardItem:
                    this.ComposeDefaultContainerEnd(codeBuilder, container, elementClass);
                    break;
                case LayoutContainerClass.TabItem:
                    this.ComposeTabItemContainerEnd(container, elementClass, codeBuilder);
                    break;
                case LayoutContainerClass.ExternalUI:
                    codeBuilder.AddLine("</div>");
                    break;
                case LayoutContainerClass.Expander:
                case LayoutContainerClass.GroupBox:
                    this.ComposeGroupBoxContainerEnd(container, elementClass, codeBuilder);
                    break;
                default:
                    break;
            }

        }

        private void ComposeGroupBoxContainerEnd(LayoutContainer container, LayoutContainerClass elementClass, Tools.CodeBuilder codeBuilder)
        {
            codeBuilder.DecreaseIndent();
            codeBuilder.AddLine("</div>");
            codeBuilder.DecreaseIndent();
            codeBuilder.AddLine("</div>");

            var control = GetControlByContainer(container);
            if (control != null)
            {
                codeBuilder.AddLine(GetKoBindingDivs(control.BindingPath, true, true));
                codeBuilder.DecreaseIndent();
            }

            codeBuilder.AddLine("</div>");
            codeBuilder.DecreaseIndent();
            codeBuilder.AddLine("</div>");

        }

        private void ComposeTabItemContainerEnd(LayoutContainer container, LayoutContainerClass elementClass, Tools.CodeBuilder codeBuilder)
        {
            codeBuilder.DecreaseIndent();
            codeBuilder.AddLine("</div>");

            var control = GetControlByContainer(container);
            if (control != null)
            {
                codeBuilder.AddLine(GetKoBindingDivs(control.BindingPath, true, true));
                codeBuilder.DecreaseIndent();
            }

            codeBuilder.AddLine("</div>");
            codeBuilder.DecreaseIndent();
            codeBuilder.AddLine("</div>");
        }

        private void ComposeDefaultContainerEnd(Tools.CodeBuilder codeBuilder, LayoutContainer container, LayoutContainerClass elementClass)
        {
            if (container.EnableGridSelector)
            {
                codeBuilder.DecreaseIndent();
                codeBuilder.AddLine("</div>");
            }
            if (elementClass.In(LayoutContainerClass.CustomContainer, LayoutContainerClass.WizardItem, LayoutContainerClass.DockItem, LayoutContainerClass.WizardControl))
            {
                codeBuilder.DecreaseIndent();
                codeBuilder.AddLine("</div>");
            }

            if (elementClass.In(LayoutContainerClass.CustomContainer, LayoutContainerClass.DockItem, LayoutContainerClass.Expander,
                LayoutContainerClass.GroupBox, LayoutContainerClass.WizardItem))
            {
                var control = GetControlByContainer(container);
                if (control != null)
                {
                    codeBuilder.AddLine(GetKoBindingDivs(control.BindingPath, true, true));
                    codeBuilder.DecreaseIndent();
                }
            }

            codeBuilder.AddLine("</div>");


            if (elementClass.In(LayoutContainerClass.TabControl, LayoutContainerClass.WizardItem, LayoutContainerClass.DockItem))
                codeBuilder.AddLine("</div>");

        }

        #endregion


        #region Auxiliar members

        private string addHtmlComment(string comment)
        {
            return "<!-- " + comment + " -->";
        }

        private string GetEditableBind(LayoutControlV2 control, string controlName)
        {
            return "ctrl.hasCustomEnable($root." + this.ViewModelName + "(), '" + controlName + "') ? ctrl.getCustomEnable($root." + this.ViewModelName + "(), '" + controlName + "') : (!isEmptyEntityFn($data) && " + (control.AlwaysEditable ? "true" : (control.EditableOnInsert || (control.IsPartOfKey && control.IsEditable) ? "$root." + this.ViewModelName + "().status() === 'C' || ($data.isAdded && $data.isAdded())" : (control.IsEditable ? "$root." + this.ViewModelName + "().enabledForEditing()" : "$root." + this.ViewModelName + "().status() === 'C'"))) + ")";
        }

        private string GetReadOnlyBind(LayoutControlV2 control, string controlName)
        {
            return "ctrl.hasCustomEnable($root." + this.ViewModelName + "(), '" + controlName + "') ? !ctrl.getCustomEnable($root." + this.ViewModelName + "(), '" + controlName + "') : (isEmptyEntityFn($data) || " + (control.AlwaysEditable ? "false" : (control.EditableOnInsert || (control.IsPartOfKey && control.IsEditable) ? "!($root." + this.ViewModelName + "().status() === 'C' || ($data.isAdded && $data.isAdded()))" : (control.IsEditable ? "!$root." + this.ViewModelName + "().enabledForEditing()" : "$root." + this.ViewModelName + "().status() !== 'C'"))) + ")";
        }


        public static string GetPropDataType(string dataType, string domainName)
        {
            string type = "string";

            if (domainName.IsNullOrEmpty())
            {
                dataType = dataType.RemoveNullDefinition();

                if (dataType.InList(new string[] { "byte", "int16", "int32", "int", "long", "short", "int64", "sbyte", "uint16", "uint32", "uint64", "single", "double", "decimal" }))
                    type = "number";
                else
                {
                    if (dataType.Contains("datetime"))
                        type = "date";
                    else if (dataType.Contains("bool"))
                        type = "bool";
                }
            }

            return type;
        }

        private string GetFormatDataType(LayoutControlV2 control)
        {
            if (control.ClassName == "MaskedTextBox" && !String.IsNullOrWhiteSpace(control.Mask))
                return control.Mask;
            else
                return GetFormatDataType(control.DataType, control.DomainName, control.DataFormatString);
        }

        public static string GetFormatDataType(string dataType, string domainName, string dataFormatString)
        {
            string format = String.Empty;
            dataType = dataType.RemoveNullDefinition();

            if (domainName.IsNullOrEmpty())
            {
                if (dataType.InList(new string[] { "byte", "int16", "int32", "int", "long", "short", "int64", "sbyte", "uint16", "uint32", "uint64" }))
                    format = "int";
                else if (dataType.InList(new string[] { "single", "double", "decimal" }))
                {
                    format = "number";
                    if (!dataFormatString.IsNullOrEmpty())
                    {
                        if (dataFormatString[0] == 'c' || dataFormatString[0] == 'C')
                            format = "currency";
                        else if (dataFormatString[0] == 'p' || dataFormatString[0] == 'P')
                            format = "percent";
                        else if ((dataFormatString[0] == 'n' || dataFormatString[0] == 'N') && dataFormatString.Length > 1)
                        {
                            string len = dataFormatString.Right(dataFormatString.Length - 1);
                            if (len.IsNumeric() && int.Parse(len) > 0)
                                format = "0." + "0".PadRight(int.Parse(len), '0');
                            else
                                format = "int";
                        }
                    }
                }
                else
                {
                    if (dataType.Contains("datetime"))
                    {//fix to display in format: dd/MM/yyyy
                        format = "dd/MM/yyyy";
                        if (!dataFormatString.IsNullOrEmpty())
                        {
                            if (dataFormatString.Length == 1)
                            {
                                switch (dataFormatString[0])
                                {
                                    case 'D':
                                    case 'f':
                                    case 'F':
                                        format = "dateLong";
                                        break;
                                    case 'g':
                                    case 'G':
                                        format = "dd/MM/yyyy HH:mm";
                                        break;
                                    case 't':
                                        format = "HH:mm";
                                        break;
                                    case 'T':
                                        format = "timeLong";
                                        break;
                                    default:
                                        break;
                                }
                            }
                            else
                                format = dataFormatString;
                        }
                    }
                    else if (dataType.Contains("bool"))
                        format = "checkbox";
                }
            }

            return format;
        }

        private void ComposePivotChart(LayoutContainer parentContainer, LayoutContainer container, LayoutContainerClass elementClass, Tools.CodeBuilder codeBuilder)
        {
            string containerName = container.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + container.GetPrefix());
            codeBuilder.AddLine("<div data-bind=\"css: $root." + this.ViewModelName + "().getLayoutColumnSpan('" + containerName + "'), visible: $root." + this.ViewModelName + "().getLayoutVisible('" + containerName + "')\"" + GetCssContainerHeight(container) + ">");

            codeBuilder.IncreaseIndent();

            codeBuilder.AddLine("<style type=\"text/css\">   ");
            codeBuilder.AddLine("   .ig-chart-root {         ");
            codeBuilder.AddLine("       width: 79%;          ");
            codeBuilder.AddLine("       float: left;         ");
            codeBuilder.AddLine("       margin-right: 1%;    ");
            codeBuilder.AddLine("       margin-bottom: 5px; ");
            codeBuilder.AddLine("   }                        ");
            codeBuilder.AddLine("                            ");
            codeBuilder.AddLine("   .ig-chart-legend {       ");
            codeBuilder.AddLine("       width: 19%;          ");
            codeBuilder.AddLine("       float: left;         ");
            codeBuilder.AddLine("   }                        ");
            codeBuilder.AddLine("</style>                     ");


            string idElement = container.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "pChart");
            codeBuilder.AddLine("<div id=\"olapSampleRoot" + idElement + "\" style=\"overflow: auto; min-height: 100px\" >");
            codeBuilder.AddLine("    <div id=\"olapChart" + idElement + "\" class=\"ig-chart-root\">");
            codeBuilder.AddLine("    </div>");
            codeBuilder.AddLine("    <div id=\"olapChartLegend" + idElement + "\" class=\"ig-chart-legend\">");
            codeBuilder.AddLine("    </div>");
            codeBuilder.AddLine("   <label class=\"legend\" style=\"float: left; margin-top: 10px\">");
            codeBuilder.AddLine("       Transpor Legenda".Translate());
            codeBuilder.AddLine("       <span class=\"checker\"><input type=\"checkbox\" id=\"transpose" + idElement + "\" /></span> ");
            codeBuilder.AddLine("   </label>");
            codeBuilder.AddLine("</div>");

            #region Defining auxiliary code
            codeBuilder.ComplementaryCalls.AddLine("complement.render" + idElement.Replace("-", "").Replace(" ", "") + "(vm);");
            codeBuilder.ComplementaryCode.AddLine(", render" + idElement.Replace("-", "").Replace(" ", "") + ": function(vm) {");
            codeBuilder.ComplementaryCode.IncreaseIndent();

            codeBuilder.ComplementaryCode.AddLine("var transposeCheckBox = $(\"#transpose" + idElement + "\"),");
            codeBuilder.ComplementaryCode.AddLine("chart = $(\"#olapChart" + idElement + "\");");

            codeBuilder.ComplementaryCode.AddLine("var hasValue = function (value) {");
            codeBuilder.ComplementaryCode.AddLine("        return value !== undefined && value != null && value.count() > 0;");
            codeBuilder.ComplementaryCode.AddLine("    },");
            codeBuilder.ComplementaryCode.AddLine("getCellData = function (rowIndex, columnIndex, columnCount, cells) {");
            codeBuilder.ComplementaryCode.AddLine("    var cellOrdinal = (rowIndex * columnCount) + columnIndex;");
            codeBuilder.ComplementaryCode.AddLine("    if (!hasValue(cells)) {");
            codeBuilder.ComplementaryCode.AddLine("        return 0;");
            codeBuilder.ComplementaryCode.AddLine("    }");
            codeBuilder.ComplementaryCode.AddLine("    for (var index = 0; index < cells.count(); index++) {");
            codeBuilder.ComplementaryCode.AddLine("        var cell = cells.item(index);");
            codeBuilder.ComplementaryCode.AddLine("        if (cell.cellOrdinal() == cellOrdinal) {");
            codeBuilder.ComplementaryCode.AddLine("           if (!isNaN(Number(cell.value()))) {");
            codeBuilder.ComplementaryCode.AddLine("               if (cell.value().indexOf('.') > -1) {");
            codeBuilder.ComplementaryCode.AddLine("                   var cellFormat = cell.value().replace('.', '');");
            codeBuilder.ComplementaryCode.AddLine("                   return new Number(parseInt(cellFormat));");
            codeBuilder.ComplementaryCode.AddLine("               }");
            codeBuilder.ComplementaryCode.AddLine("               else");
            codeBuilder.ComplementaryCode.AddLine("                   return new Number(parseInt(cell.value()));");
            codeBuilder.ComplementaryCode.AddLine("           }");
            codeBuilder.ComplementaryCode.AddLine("           else if (cell.value().indexOf('.') > -1) {");
            codeBuilder.ComplementaryCode.AddLine("               var cellFormat = cell.value().replace('.', '');");
            codeBuilder.ComplementaryCode.AddLine("               return new Number(parseInt(cellFormat));");
            codeBuilder.ComplementaryCode.AddLine("           } else");
            codeBuilder.ComplementaryCode.AddLine("            return new Number(parseInt(cell.value()));");
            codeBuilder.ComplementaryCode.AddLine("        }");
            codeBuilder.ComplementaryCode.AddLine("    }");
            codeBuilder.ComplementaryCode.AddLine("    return 0;");
            codeBuilder.ComplementaryCode.AddLine("},");

            codeBuilder.ComplementaryCode.AddLine("updateChart = function (tableView, transpose) {");
            codeBuilder.ComplementaryCode.AddLine("var columnHeaders,");
            codeBuilder.ComplementaryCode.AddLine("    rowHeaders,");
            codeBuilder.ComplementaryCode.AddLine("    cells = tableView.resultCells(),");
            codeBuilder.ComplementaryCode.AddLine("    dataArray = [],");
            codeBuilder.ComplementaryCode.AddLine("    series = [],");
            codeBuilder.ComplementaryCode.AddLine("    rowHeaderIndex,");
            codeBuilder.ComplementaryCode.AddLine("    columnHeaderIndex,");
            codeBuilder.ComplementaryCode.AddLine("    ds,");
            codeBuilder.ComplementaryCode.AddLine("    headerCell,");
            codeBuilder.ComplementaryCode.AddLine("    columnCount,");
            codeBuilder.ComplementaryCode.AddLine("    rowCount,");
            codeBuilder.ComplementaryCode.AddLine("    data;");
            codeBuilder.ComplementaryCode.AddLine("");
            codeBuilder.ComplementaryCode.AddLine("if (transpose) {");
            codeBuilder.ComplementaryCode.AddLine("    columnHeaders = tableView.rowHeaders(),");
            codeBuilder.ComplementaryCode.AddLine("    rowHeaders = tableView.columnHeaders()");
            codeBuilder.ComplementaryCode.AddLine("}");
            codeBuilder.ComplementaryCode.AddLine("else {");
            codeBuilder.ComplementaryCode.AddLine("    columnHeaders = tableView.columnHeaders(),");
            codeBuilder.ComplementaryCode.AddLine("    rowHeaders = tableView.rowHeaders()");
            codeBuilder.ComplementaryCode.AddLine("}");
            codeBuilder.ComplementaryCode.AddLine("");
            codeBuilder.ComplementaryCode.AddLine("if (!hasValue(cells) && !hasValue(rowHeaders) && !hasValue(columnHeaders)) {");
            codeBuilder.ComplementaryCode.AddLine("    var dataDefault = [{ 'caption': '', 'col0': 0 }];");
            codeBuilder.ComplementaryCode.AddLine("    chart.igDataChart({");
            codeBuilder.ComplementaryCode.AddLine("        height: '500px', width: '100%', dataSource: dataDefault, series: series,");
            codeBuilder.ComplementaryCode.AddLine("        axes: [{ name: 'xAxis', type: 'categoryX', label: 'caption' },");
            codeBuilder.ComplementaryCode.AddLine("        { name: 'yAxis', type: 'numericY' }],");
            codeBuilder.ComplementaryCode.AddLine("        series: [{");
            codeBuilder.ComplementaryCode.AddLine("             name: 'series0', dataSource: dataDefault, title: 'caption', type: 'column', xAxis: 'xAxis', yAxis: 'yAxis', valueMemberPath: 'col0'");
            codeBuilder.ComplementaryCode.AddLine("        }]");
            codeBuilder.ComplementaryCode.AddLine("    });");
            codeBuilder.ComplementaryCode.AddLine("    return;");
            codeBuilder.ComplementaryCode.AddLine("}");
            codeBuilder.ComplementaryCode.AddLine("else");
            codeBuilder.ComplementaryCode.AddLine("   chart.igDataChart('destroy');");
            codeBuilder.ComplementaryCode.AddLine("");
            codeBuilder.ComplementaryCode.AddLine("if (!hasValue(rowHeaders)) {");
            codeBuilder.ComplementaryCode.AddLine("    rowHeaders = [{ caption: function () { return ''; } }];");
            codeBuilder.ComplementaryCode.AddLine("}");
            codeBuilder.ComplementaryCode.AddLine("");
            codeBuilder.ComplementaryCode.AddLine("if (!hasValue(columnHeaders)) {");
            codeBuilder.ComplementaryCode.AddLine("    columnHeaders = [{ caption: function () { return ''; } }];");
            codeBuilder.ComplementaryCode.AddLine("}");
            codeBuilder.ComplementaryCode.AddLine("");
            codeBuilder.ComplementaryCode.AddLine("for (rowHeaderIndex = 0; rowHeaderIndex < rowHeaders.count(); rowHeaderIndex++) {");
            codeBuilder.ComplementaryCode.AddLine("    headerCell = rowHeaders.item(rowHeaderIndex);");
            codeBuilder.ComplementaryCode.AddLine("    columnCount = columnHeaders.count();");
            codeBuilder.ComplementaryCode.AddLine("    rowCount = rowHeaders.count();");
            codeBuilder.ComplementaryCode.AddLine("    data = { caption: headerCell.caption() };");
            codeBuilder.ComplementaryCode.AddLine("    var value;");
            codeBuilder.ComplementaryCode.AddLine("    for (columnHeaderIndex = 0; columnHeaderIndex < columnCount; columnHeaderIndex++) {");
            codeBuilder.ComplementaryCode.AddLine("        if (transpose) {");
            codeBuilder.ComplementaryCode.AddLine("            value = getCellData(columnHeaderIndex, rowHeaderIndex, rowCount, cells, transpose)");
            codeBuilder.ComplementaryCode.AddLine("        }");
            codeBuilder.ComplementaryCode.AddLine("        else {");
            codeBuilder.ComplementaryCode.AddLine("            value = getCellData(rowHeaderIndex, columnHeaderIndex, columnCount, cells, transpose)");
            codeBuilder.ComplementaryCode.AddLine("        }");
            codeBuilder.ComplementaryCode.AddLine("        data['col' + columnHeaderIndex] = value;");
            codeBuilder.ComplementaryCode.AddLine("    }");
            codeBuilder.ComplementaryCode.AddLine("");
            codeBuilder.ComplementaryCode.AddLine("    dataArray[rowHeaderIndex] = data;");
            codeBuilder.ComplementaryCode.AddLine("};");
            codeBuilder.ComplementaryCode.AddLine("");
            codeBuilder.ComplementaryCode.AddLine("for (columnHeaderIndex = 0; columnHeaderIndex < columnHeaders.count(); columnHeaderIndex++) {");
            codeBuilder.ComplementaryCode.AddLine("    series[columnHeaderIndex] = {");
            codeBuilder.ComplementaryCode.AddLine("        name: 'series' + columnHeaderIndex,");
            codeBuilder.ComplementaryCode.AddLine("        title: columnHeaders.item(columnHeaderIndex).caption(),");
            codeBuilder.ComplementaryCode.AddLine("        type: '" + container.PivotChartType.ToLower() + "',");
            codeBuilder.ComplementaryCode.AddLine("        xAxis: 'xAxis',");
            codeBuilder.ComplementaryCode.AddLine("        yAxis: 'yAxis',");
            codeBuilder.ComplementaryCode.AddLine("        showTooltip: true,");
            codeBuilder.ComplementaryCode.AddLine("        valueMemberPath: 'col' + columnHeaderIndex");
            codeBuilder.ComplementaryCode.AddLine("    };");
            codeBuilder.ComplementaryCode.AddLine("};");
            codeBuilder.ComplementaryCode.AddLine("");

            //codeBuilder.ComplementaryCode.AddLine("if (dataArray.length > 1 && dataArray[0].caption.indexOf('T:') > -1) {");
            //codeBuilder.ComplementaryCode.AddLine("    dataArray.splice(0, 1);");
            //codeBuilder.ComplementaryCode.AddLine("}");

            codeBuilder.ComplementaryCode.AddLine("ds = new $.ig.DataSource({ dataSource: dataArray });");
            codeBuilder.ComplementaryCode.AddLine("");
            codeBuilder.ComplementaryCode.AddLine("chart.igDataChart({");
            codeBuilder.ComplementaryCode.AddLine("    height: " + (container.Height <= 0 ? "'500px'" : container.Height.ToString()) + ",");
            codeBuilder.ComplementaryCode.AddLine("    width: '100%',");
            codeBuilder.ComplementaryCode.AddLine("    dataSource: ds,");
            codeBuilder.ComplementaryCode.AddLine("    series: series,");
            codeBuilder.ComplementaryCode.AddLine("    legend: { element: 'olapChartLegend" + idElement + "' },");
            codeBuilder.ComplementaryCode.AddLine("    axes: [{");
            codeBuilder.ComplementaryCode.AddLine("        name: 'xAxis',");
            codeBuilder.ComplementaryCode.AddLine("        type: 'categoryX',");
            codeBuilder.ComplementaryCode.AddLine("        label: 'caption'");
            codeBuilder.ComplementaryCode.AddLine("    },");
            codeBuilder.ComplementaryCode.AddLine("    {");
            codeBuilder.ComplementaryCode.AddLine("        name: 'yAxis',");
            codeBuilder.ComplementaryCode.AddLine("        type: 'numericY'");
            codeBuilder.ComplementaryCode.AddLine("    }],");
            codeBuilder.ComplementaryCode.AddLine("    horizontalZoomable: false,");
            codeBuilder.ComplementaryCode.AddLine("    verticalZoomable: false,");
            codeBuilder.ComplementaryCode.AddLine("    windowResponse: 'immediate'");
            codeBuilder.ComplementaryCode.AddLine("});");
            codeBuilder.ComplementaryCode.AddLine("};");


            //Creating Binding Update
            codeBuilder.ComplementaryCode.AddLine("var bindingUpdate = function () {");
            codeBuilder.ComplementaryCode.AddLine("    var pivotView = vm.dataShared['" + container.PivotGridName + "'];");
            codeBuilder.ComplementaryCode.AddLine("    if (!pivotView || pivotView.data().igPivotView == null) return;");
            codeBuilder.ComplementaryCode.AddLine("    var pivotGrid = pivotView.igPivotView('pivotGrid');");
            codeBuilder.ComplementaryCode.AddLine("    if (!pivotGrid) return;");

            codeBuilder.ComplementaryCode.AddLine("    pivotGrid.element.igPivotGrid({");
            codeBuilder.ComplementaryCode.AddLine("        pivotGridRendered: function () {");
            codeBuilder.ComplementaryCode.AddLine("            updateChart(pivotGrid._tableView, transposeCheckBox.is(':checked')); }");
            codeBuilder.ComplementaryCode.AddLine("    });");

            codeBuilder.ComplementaryCode.AddLine("    transposeCheckBox.click(function () {");
            codeBuilder.ComplementaryCode.AddLine("        updateChart(pivotGrid._tableView, transposeCheckBox.is(':checked'));");
            codeBuilder.ComplementaryCode.AddLine("    });");
            codeBuilder.ComplementaryCode.AddLine("    vm.dataSource.removeItem(itemsSource);");
            codeBuilder.ComplementaryCode.AddLine("    itemsSource = null;");
            codeBuilder.ComplementaryCode.AddLine("};");

            //Creating Items Source
            codeBuilder.ComplementaryCode.AddLine("var itemsSource = { key: '" + idElement + "', name: 'dataView', itemsSource: { dataBind: bindingUpdate } };");
            codeBuilder.ComplementaryCode.AddLine("vm.addDataSource(itemsSource);");


            codeBuilder.ComplementaryCode.DecreaseIndent();
            codeBuilder.ComplementaryCode.AddLine("}");

            #endregion


            codeBuilder.DecreaseIndent();
        }

        private void ComposeGauge(LayoutControlV2 control, LayoutControlClass elementClass, Tools.CodeBuilder codeBuilder)
        {
            if (!control.ScriptDefinition.IsNullOrEmpty())
            {
                string controlBindingPath = control.BindingPath;
                string binding = GetBindingPath(controlBindingPath, true);
                codeBuilder.IncreaseIndent();
                string idElement = control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "gauge");
                codeBuilder.AddLine("<div id=\"" + idElement + "\" />");
                codeBuilder.AddLine("<div id=\"gauge" + idElement + "\" class=\"ig-chart-root\">");
                codeBuilder.AddLine("</div>");
                codeBuilder.AddLine("<p class=\"gaugeLegend\" ><span id=\"gauge" + idElement + "_Legend\"></span></p>");
                codeBuilder.AddLine("<!-- ko template: {name: 'EmptyTemplate', data: $data, ");
                codeBuilder.AddLine("afterRender: function(element) {");
                codeBuilder.IncreaseIndent();

                //Create Gauge Definition
                codeBuilder.AddLine("var gauge = $(\"#gauge" + idElement + "\");");
                codeBuilder.AddLine(control.ScriptDefinition.Replace("@valueField", "getAbsoluteValue(" + control.BindingPath.Right(".") + ")").Replace("@KPIName", control.KpiName).Replace("@gaugeName", "gauge"));

                codeBuilder.DecreaseIndent();
                codeBuilder.AddLine("if(gauge.data(\"kendoRadialGauge\") !== undefined){");
                codeBuilder.AddLine("   var valor = gauge.data(\"kendoRadialGauge\").value().toString();");
                codeBuilder.AddLine("   if(valor.indexOf('0.') >= 0) valor = (parseFloat(valor) * 100).toFixed(2);");
                codeBuilder.AddLine("   else if(parseInt(valor) == 1 || parseInt(valor) == -1) valor = parseInt(valor) * 100;");
                codeBuilder.AddLine("   setTimeout(function () {");
                codeBuilder.AddLine("       $(\"#gauge" + idElement + "_Legend\").html('Val: ' + valor + '%');");
                codeBuilder.AddLine("   }, 50);");
                codeBuilder.AddLine("}");
                codeBuilder.AddLine("}} -->");
                codeBuilder.AddLine("<!-- /ko -->");
                codeBuilder.DecreaseIndent();
            }
        }

        private void ComposeChart(LayoutControlV2 control, LayoutControlClass elementClass, Tools.CodeBuilder codeBuilder)
        {
            if (!control.ScriptDefinition.IsNullOrEmpty())
            {
                string controlBindingPath = control.BindingPath + "." + "Id";
                string binding = GetBindingPath(controlBindingPath, true);
                string currentBinding = GetFullBindingPath(controlBindingPath, false), listBinding = GetFullBindingPath(controlBindingPath, true);

                codeBuilder.IncreaseIndent();
                codeBuilder.AddLine(GetKoBindingDivs(controlBindingPath, false, false));
                codeBuilder.IncreaseIndent();
                string idElement = control.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "chart");
                codeBuilder.AddLine("<div id=\"" + idElement + "\" />");
                codeBuilder.AddLine("<div id=\"chart" + idElement + "\" class=\"ig-chart-root\">");
                codeBuilder.AddLine("</div>");
                codeBuilder.AddLine("<div id=\"chart" + idElement + "_Legend\" class=\"ig-chart-root\">");
                codeBuilder.AddLine("</div>");
                #region Defining auxiliary code
                codeBuilder.ComplementaryCalls.AddLine("complement.render" + idElement.Replace("-", "").Replace(" ", "") + "(vm);");
                codeBuilder.ComplementaryCode.AddLine(", render" + idElement.Replace("-", "").Replace(" ", "") + ": function(vm) {");
                codeBuilder.ComplementaryCode.IncreaseIndent();

                //Creating Binding Update
                codeBuilder.ComplementaryCode.AddLine("var itemsSource = { dataBind: function (commitData) {");
                codeBuilder.ComplementaryCode.AddLine("if (commitData) {");
                codeBuilder.ComplementaryCode.AddLine("    return;");
                codeBuilder.ComplementaryCode.AddLine("}");
                //Create Chart Definition
                codeBuilder.ComplementaryCode.AddLine("var chart = $(\"#chart" + idElement + "\");");
                codeBuilder.ComplementaryCode.AddLine(control.ScriptDefinition.Replace("$root", "vm").Replace("@dataSource", "formatTimeZone(unwrapObservableArray(" + listBinding + ", vm, true))").Replace("@maxValue", "maxArrayValue").Replace("@minValue", "minArrayValue").Replace("@chartLegendElement", "chart" + idElement + "_Legend").Replace("@chartName", "chart"));
                codeBuilder.ComplementaryCode.AddLine("}};");

                codeBuilder.ComplementaryCode.AddLine("vm.addDataSource({ key: '" + idElement + "', name: '" + (binding.IsNullOrEmpty() ? "dataView" : binding.Right(".")) + "', itemsSource: itemsSource });");

                codeBuilder.ComplementaryCode.DecreaseIndent();
                codeBuilder.ComplementaryCode.AddLine("}");
                #endregion
                codeBuilder.DecreaseIndent();
                codeBuilder.AddLine(GetKoBindingDivs(controlBindingPath, false, true));
                codeBuilder.DecreaseIndent();
            }
        }

        private string GetBindingPath(string bindingPath, bool returnBindingList = false, bool ko = false)
        {
            string result = String.Empty;

            foreach (string part in GetBindingParts(bindingPath, returnBindingList, ko))
            {
                result += (result.IsNullOrEmpty() ? "" : ".") + part;
            }
            return result;
        }

        private string GetKoBindingDivs(string bindingPath, bool returnLastPath, bool closeTags)
        {
            string[] bindingParts = GetBindingParts(bindingPath, false, true);
            string result = String.Empty;

            for (int idx = 0; idx < bindingParts.Length - (returnLastPath ? 0 : 1); idx++)
            {
                result += (closeTags ? "</div>" :
                    "<div data-bind=\"with: $root." + this.ViewModelName + "().getWithBinding(" + bindingParts[idx] + ", " +
                    (idx == 0 ? "$root." + this.ViewModelName + "().rootDataTypeName" :
                    "'" + (bindingParts[idx].StartsWith("current") ? bindingParts[idx].Substring(7) : bindingParts[idx]) + "'")
                    + ")\">");
            }

            return result;
        }

        private string GetFullBindingPath(string bindingPath, bool returnBindingList, bool putPreffixVM = true)
        {
            string result = String.Empty;

            string[] bindingParts = GetBindingParts(bindingPath, returnBindingList);
            for (int idx = 0; idx < bindingParts.Length; idx++)
            {
                result += (result == String.Empty ? String.Empty : ".") + bindingParts[idx];
            }

            if (result.IsNullOrEmpty())
                result = (returnBindingList ? "dataView" : "currentDataItem");

            return (putPreffixVM ? "vm." : "") + result;
        }

        private string[] GetBindingParts(string bindingPath, bool returnBindingList = false, bool ko = false)
        {
            List<string> result = new List<string>();

            string[] bindingParts = GetDataBind((bindingPath + ".").Left("." + bindingPath.Right(".") + ".")).Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

            if (bindingParts.Length > 0 || !returnBindingList)
                result.Add("currentDataItem" + (bindingParts.Length == 0 || ko ? "" : "()"));

            if (bindingParts.Length > 0)
            {
                for (int idxBind = 0; idxBind < bindingParts.Length; idxBind++)
                {
                    string bindingPart = bindingParts[idxBind];
                    if (idxBind == (bindingParts.Length - 1))
                    {
                        result.Add((returnBindingList ? "" : "current") + bindingPart.Replace("PagedList", (returnBindingList ? "List" : "")));
                    }
                    else
                    {
                        result.Add("current" + bindingPart.Replace("PagedList", "") + (ko ? "" : "()"));
                    }
                }
            }

            return result.ToArray();
        }

        private string GetMaskForDisplay(LayoutControlV2 control)
        {
            //http://help.infragistics.com/jQuery/2014.1/ui.igmaskeditor/#options:inputMask
            return control.Mask.IsNullOrEmpty() ? "MaskNull" :
                control.Mask
                    .Replace("C", "#")
                    .Replace("a", "#")
                    .Replace("A", "#")
                    .Replace("?", "#")
                    .Replace("L", "#")
                    .Replace("9", "#")
                    .Replace("0", "#")
                    .Replace("+", "#")
                    .Replace("_", "#");
        }

        private void ComposeDataGrid(LayoutContainer parentContainer, LayoutContainer container, LayoutContainerClass elementClass, Tools.CodeBuilder codeBuilder, int rows, int columns, List<TreeLayoutContainer> innerDataGrids)
        {
            //Remove this code when resolve the auto height problem.
            if (container.GridHeight == GridSizeHeight.Auto)
                container.GridHeight = GridSizeHeight.Large;

            List<LayoutControlV2> controls = new List<LayoutControlV2>();

            Action<LayoutElement> finder = null;
            finder = (element) =>
            {
                if (element is LayoutControlV2) controls.Add((LayoutControlV2)element);
                else
                    if (element is LayoutContainer) ((LayoutContainer)element).Controls.ForEach(finder);
            };
            container.Controls.ForEach(finder);

            controls = controls.OrderBy(c => c.GetDataGridOrder()).ToList();

            var NavigableUIs = container.Controls.Where(e => e.ClassName == "ExternalUI").Select(e => (LayoutContainer)e);

            string idElement = container.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "dGrid");

            codeBuilder.AddLine("<div class=\"" + GetColumnSpan(parentContainer, container, false) + (this._layOut.GetLayoutElementsByClass("WizardControl").Count > 0 ? "" : " tab-height-size ") + "\"" + (_layOut.IsSecundary ? "" : " data-bind=\"visible: $root." + this.ViewModelName + "().getLayoutVisible('" + idElement + "') \"") + " >");


            if (controls.Count == 0)
                return;

            string controlBindingPath = controls.First().BindingPath;

            string binding = GetBindingPath(controlBindingPath, true), propertyKey;
            string dataView = binding.IsNullOrEmpty() ? "" : (binding.Right(".") + "#").Left("List#");
            string currentBinding = GetFullBindingPath(controlBindingPath, false), listBinding = GetFullBindingPath(controlBindingPath, true);
            string rootListBinding = listBinding.Replace("vm", "$root." + this.ViewModelName + "()");
            string rootCurrent = currentBinding.Replace("vm", "$root." + this.ViewModelName + "()");
            #region addNavigableUIsAction
            Action addNavigableUIsAction = () =>
                {
                    codeBuilder.AddLine("<div class=\"dropdown\" data-bind=\"visible: $root." + this.ViewModelName + "().status() === 'Q'\">");
                    codeBuilder.AddLine("   <a href=\"#\" title=\"Telas Externas\" class=\"btn default background-color-theme\" data-toggle=\"dropdown\" data-hover=\"dropdown\" data-close-others=\"true\" data-placement=\"right\"><i class=\"fa fa-sign-out\"></i></a>");
                    codeBuilder.AddLine("   <ul class=\"dropdown-menu extended notification\" >");
                    foreach (var extUI in NavigableUIs)
                    {
                        if (extUI.UserInterfaceName.IsNullOrEmpty()) throw new ArgumentNullException("The value externalUI [" + extUI.DisplayName + "] - 'User Interface' is Null");
                        string cmd = "      <li><a title=\"{DisplayName}\" data-bind=\"click: function () { if(!hasObjectWithPropertyValues({currentBinding}, '{pSource}', '{pDest}')) { require('durandal/app').showMessage('Nenhum registro foi encontrado!', 'Informação', ['Ok']); return; } var entityFilter = getObjectWithPropertyValues({currentBinding}, '{pSource}', '{pDest}');  entityFilter = vm.openingExternalUIFromGrid('{UIName}', entityFilter); if(entityFilter === 'Error') { return; } vm.common.go('#{UIName}', 'objectQuery=' + entityFilter + '&executeQuery=true') }\"><i class=\"fa fa-chevron-right\"></i>{DisplayName}</a></li>";
                        cmd = cmd.Replace("{DisplayName}", extUI.DisplayName);
                        cmd = cmd.Replace("{currentBinding}", currentBinding + "()");
                        cmd = cmd.Replace("{UIName}", extUI.UserInterfaceName.Left("/").ToLower().Replace(".", "-") + "-" + extUI.UserInterfaceName.ToLower().Right("/"));
                        cmd = cmd.Replace("{pSource}", extUI.ParentFieldsRelation);
                        cmd = cmd.Replace("{pDest}", extUI.DetailFieldsRelation);
                        cmd = cmd.Replace("vm.", "$root." + this.ViewModelName + "().");


                        codeBuilder.AddLine(cmd);
                    }
                    codeBuilder.AddLine("   </ul>");
                    codeBuilder.AddLine("</div>");
                };
            #endregion
            string parentBinding = currentBinding;
            int idx = currentBinding.LastIndexOf('.');
            if (idx >= 0)
                parentBinding = currentBinding.Left(currentBinding.LastIndexOf('.'));

            string dataPrimaryKey = controls.Where(e => e.IsPartOfKey).Select(e => e.BindingPath.Right(".")).FirstOrDefault(), comma = String.Empty, primaryKey = "RowDataId", primaryKeyType = this._layOut.GetPrimaryKeyTypeByEntity(dataView);

            bool hasSummaries = controls.Any(e => !e.AggregationFunction.IsNullOrEmpty() && e.AggregationFunction != "None");
            codeBuilder.IncreaseIndent();
            if (!binding.IsNullOrEmpty() && !container.RemoveDataToolbar)
            {
                codeBuilder.AddLine("<div id=\"" + idElement + "_ContentDLG\" style=\"position:relative;\"></div>");
                codeBuilder.AddLine("<div class=\"linx-table-button position-icon\">");
                codeBuilder.IncreaseIndent();

                codeBuilder.AddLine("<div class=\"botoes-tabela\">");
                codeBuilder.AddLine(GetKoBindingDivs(controlBindingPath, false, false));

                if ((this._layOut.CanEdit || this._layOut.CanAddNew) && container.CanAddNew)
                    codeBuilder.AddLine("<button id=\"" + idElement + "_AddBtn\" class=\"btn default background-color-theme\" data-placement=\"top\" title=\"Novo Registro\" data-bind=\"visible: $root." + this.ViewModelName + "().status() === 'E' && !isEmptyEntityFn($data), click: function () { $root." + this.ViewModelName + "().createAndNotify" + dataView + "($data); }\"><i class=\"fa fa-plus\"></i></button>");

                if (this._layOut.CanEdit && container.CanDelete)
                {
                    codeBuilder.AddLine("<button id=\"" + idElement + "_DelBtn\" class=\"btn default background-color-theme\" data-placement=\"top\" title=\"Excluir Registro\" ");
                    codeBuilder.AddLine("data-bind=\"visible: $root." + this.ViewModelName + "().status() === 'E', ");
                    codeBuilder.AddLine("click: function () {");
                    codeBuilder.AddLine("   $root." + this.ViewModelName + "().deleteGrid('#" + idElement + "', '" + container.Name + "', '" + currentBinding + ";" + listBinding + "', " + container.EnableMultiSelection.ToString().ToLower() + ");");
                    codeBuilder.AddLine("}\"> ");
                    codeBuilder.AddLine("<i class=\"fa fa-trash-o\"></i></button>");
                }

                AddButtonsConfigAndEditor(container, codeBuilder, idElement, dataView, currentBinding, listBinding, parentBinding, (dataView.IsNullOrEmpty() ? EntityName + "()" : dataView + '(' + parentBinding + ')'), rootListBinding);

                if (container.CanExportGrid)
                {
                    codeBuilder.AddLine("<button id=\"" + idElement + "_ExpExcelBtn\" class=\"btn default background-color-theme\" data-placement=\"top\" title=\"Exportar para Excel\" data-bind=\"visible: $root." + this.ViewModelName + "().dataToolbar.canGridExport(), click: function () {$root." + this.ViewModelName + "().exportDataDetails($data, '" + dataView + "', true);  }\"><i class=\"fa fa-file-excel-o\"></i></button>");
                    //codeBuilder.AddLine("<button id=\"" + idElement + "_ExpReportBtn\" class=\"btn default background-color-theme\" data-placement=\"top\" title=\"Gerar Relatório\" data-bind=\"visible: $root." + this.ViewModelName + "().dataToolbar.canGridExport(), click: function () {$root." + this.ViewModelName + "().exportDataDetails($data, '" + dataView + "', false);  }\"><i class=\"fa fa-print\"></i></button>");
                }

                if (NavigableUIs.Any())
                    addNavigableUIsAction();

                codeBuilder.AddLine(GetKoBindingDivs(controlBindingPath, false, true));
                codeBuilder.AddLine("</div>");

                codeBuilder.DecreaseIndent();
                codeBuilder.AddLine("</div>");
            }
            else if (binding.IsNullOrEmpty() && !container.RemoveDataToolbar)
            {
                codeBuilder.AddLine("<div id=\"" + idElement + "_ContentDLG\" style=\"position:relative;\"></div>");
                codeBuilder.AddLine("<div class=\"linx-table-button position-icon\">");
                codeBuilder.IncreaseIndent();

                codeBuilder.AddLine("<div class=\"botoes-tabela\" >");

                if (container.CanAddNew)
                    codeBuilder.AddLine("<button id=\"" + idElement + "_AddBtn\" class=\"btn default background-color-theme\" data-placement=\"top\" title=\"Novo Registro\" data-bind=\"visible: $root." + this.ViewModelName + "().hideToolbar() && $root." + this.ViewModelName + "().status() === 'E', click: function () { $root." + this.ViewModelName + "().dataToolbar.addNew(); }\"><i class=\"fa fa-plus\"></i></button>");

                if (container.CanDelete)
                {
                    codeBuilder.AddLine("<button id=\"" + idElement + "_DelBtn\" class=\"btn default background-color-theme\" data-placement=\"top\" title=\"Excluir Registro\" ");
                    codeBuilder.AddLine("data-bind=\"visible: $root." + this.ViewModelName + "().status() === 'E', ");
                    codeBuilder.AddLine("click: function () { ");
                    codeBuilder.AddLine("   $root." + this.ViewModelName + "().deleteGrid('#" + idElement + "', '" + container.Name + "', '" + currentBinding + ";" + listBinding + "', " + container.EnableMultiSelection.ToString().ToLower() + ");");
                    codeBuilder.AddLine("}\"> ");
                    codeBuilder.AddLine("<i class=\"fa fa-trash-o\"></i></button>");
                }

                AddButtonsConfigAndEditor(container, codeBuilder, idElement, dataView, currentBinding, listBinding, parentBinding, (dataView.IsNullOrEmpty() ? EntityName + "()" : dataView + '(' + parentBinding + ')'), rootListBinding);

                if (container.CanExportGrid)
                {
                    codeBuilder.AddLine("<button id=\"" + idElement + "_ExpExcelBtn\" class=\"btn default background-color-theme\" data-placement=\"top\" title=\"Exportar para Excel\" data-bind=\"visible: $root." + this.ViewModelName + "().dataToolbar.canExport(), click: function () {$root." + this.ViewModelName + "().dataToolbar.exportData(false, true);  }\"><i class=\"fa fa-file-excel-o\"></i></button>");
                    //codeBuilder.AddLine("<button id=\"" + idElement + "_ExpReportBtn\" class=\"btn default background-color-theme\" data-placement=\"top\" title=\"Gerar Relatório\" data-bind=\"visible: $root." + this.ViewModelName + "().dataToolbar.canExport(), click: function () {$root." + this.ViewModelName + "().dataToolbar.exportData(false, false);  }\"><i class=\"fa fa-print\"></i></button>");
                }
                if (NavigableUIs.Any())
                    addNavigableUIsAction();


                codeBuilder.AddLine("</div>");

                codeBuilder.DecreaseIndent();
                codeBuilder.AddLine("</div>");
            }

            else if (!container.RemoveDataToolbar)
            {
                codeBuilder.AddLine("<div id=\"" + idElement + "_ContentDLG\" style=\"position:relative;\"></div>");
                codeBuilder.AddLine("<div class=\"linx-table-button position-icon\">");
                codeBuilder.IncreaseIndent();

                codeBuilder.AddLine("<div class=\"botoes-tabela\"  >");
                AddButtonsConfigAndEditor(container, codeBuilder, idElement, dataView, currentBinding, listBinding, parentBinding, (dataView.IsNullOrEmpty() ? EntityName + "()" : dataView + '(' + parentBinding + ')'), rootListBinding);
                if (this._layOut.CanEdit && container.CanDelete)
                {
                    codeBuilder.AddLine("<button id=\"" + idElement + "_DelBtn\" class=\"btn default background-color-theme\" data-placement=\"top\" title=\"Excluir Registro\" ");
                    codeBuilder.AddLine("data-bind=\"visible: $root." + this.ViewModelName + "().dataToolbar.canUndo, ");
                    codeBuilder.AddLine("click: function () {");
                    codeBuilder.AddLine("   $root." + this.ViewModelName + "().deleteGrid('#" + idElement + "', '" + container.Name + "', '" + currentBinding + ";" + listBinding + "', " + container.EnableMultiSelection.ToString().ToLower() + ");");
                    codeBuilder.AddLine("}\"> ");
                    codeBuilder.AddLine("<i class=\"fa fa-trash-o\"></i></button>");
                }

                codeBuilder.AddLine("</div>");

                codeBuilder.DecreaseIndent();
                codeBuilder.AddLine("</div>");
            }
            codeBuilder.AddLine("<div class=\"linx-table-grid position-table-grid\"" + (((_layOut.Containers.Count == 1 && _layOut.Containers[0] == container) || (_layOut.Containers.Count == 2 && _layOut.Containers[0] == container && _layOut.Containers[1].IsTemplate)) ? " style=\"height: inherit;\"" : "") + ">");
            codeBuilder.IncreaseIndent();

            if ((_layOut.Containers.Count == 1 && _layOut.Containers[0] == container) || (_layOut.Containers.Count == 2 && _layOut.Containers[0] == container && _layOut.Containers[1].IsTemplate))
                codeBuilder.AddLine("<div class=\"screen-view\">");
            codeBuilder.AddLine("   <table id=\"" + idElement + "\" data-bind=\"css: { IsEditableStyle: $root." + this.ViewModelName + "().enabledForEditing }\" />");
            if (_layOut.IsSecundary)
                codeBuilder.AddLine("</div>");
            codeBuilder.DecreaseIndent();
            codeBuilder.AddLine("</div>");
            #region Event ChangedBrand
            codeBuilder.AddEventCode(Tools.CodeBuilder.EventName.ChangedBrand, "complement.ChangedBrand" + idElement.Replace("-", "").Replace(" ", "") + "(vm, decimals, reset);");
            codeBuilder.ComplementaryCode.AddLine(", ChangedBrand" + idElement.Replace("-", "").Replace(" ", "") + ": function(vm, decimals, reset) {");
            codeBuilder.ComplementaryCode.IncreaseIndent();
            codeBuilder.ComplementaryCode.AddLine("var i, format = '0.'.concat('0'.repeat(decimals)), grd =$('#" + idElement + "').data('igGrid'),");
            codeBuilder.ComplementaryCode.AddLine("    grdUpd = $('#" + idElement + "').data('igGridUpdating');");
            codeBuilder.ComplementaryCode.AddLine("if(isNull(grd) || isNull(grdUpd)) return;");
            codeBuilder.ComplementaryCode.AddLine("for (i = 0; i < grd.options.columns.length; i++) {");
            controls.Where(c => c.BrandDecimalsControl).Foreach(c => codeBuilder.ComplementaryCode.AddLine("    if (grd.options.columns[i].key === '" + c.Name + "') grd.options.columns[i].format = (reset ? '" + GetFormatDataType(c) + "': format);"));
            codeBuilder.ComplementaryCode.AddLine("}");
            codeBuilder.ComplementaryCode.AddLine("for (i = 0; i < grdUpd.options.columnSettings.length; i++) {");
            controls.Where(c => c.BrandDecimalsControl).Foreach(c =>
            {
                codeBuilder.ComplementaryCode.AddLine("    if (grdUpd.options.columnSettings[i].columnKey === '" + c.Name + "') {");
                codeBuilder.ComplementaryCode.AddLine("        grdUpd.options.columnSettings[i].editorOptions.minDecimals = grdUpd.options.columnSettings[i].editorOptions.maxDecimals = (reset ? " + c.GetPrecisionDecimalsInt().ToString() + ":decimals);");
                codeBuilder.ComplementaryCode.AddLine("    }");
            });
            codeBuilder.ComplementaryCode.AddLine("}");
            codeBuilder.ComplementaryCode.AddLine("grd.dataBind();");
            codeBuilder.ComplementaryCode.DecreaseIndent();
            codeBuilder.ComplementaryCode.AddLine("}");
            #endregion
            #region Defining auxiliary code
            codeBuilder.ComplementaryCalls.AddLine("complement.render" + idElement.Replace("-", "").Replace(" ", "") + "(vm);");

            //Não mais utilizado como no novo modelo de selector da grid no editor template
            //if (container.EnableGridSelector)
            //{
            //    bool collapsed = ((bool)container.StartOpenSelector == true ? false : true);
            //    codeBuilder.ComplementaryCalls.AddLine("$('#" + idElement + "splt').igSplitter({ height: '100%', panels: [{ size: 200, max: 200, collapsible: true, collapsed: " + collapsed.ToString().ToLower() + " }, ] });");
            //}

            #region Enable multi-selection
            if (container.EnableMultiSelection)
            {
                codeBuilder.ComplementaryCode.AddLine(", vm: null");

                codeBuilder.ComplementaryCode.AddLine(", selectedCollection: { }");
                codeBuilder.ComplementaryCode.AddLine(", currentPage: 0");
                codeBuilder.ComplementaryCode.AddLine(", selectedItems: function(firstIfNoItem) {");
                codeBuilder.ComplementaryCode.AddLine("    var result = [];");
                codeBuilder.ComplementaryCode.AddLine("    complement.saveSelection();");
                codeBuilder.ComplementaryCode.AddLine("    for (var propName in complement.selectedCollection)");
                codeBuilder.ComplementaryCode.AddLine("    {");
                codeBuilder.ComplementaryCode.AddLine("        result = result.concat(complement.selectedCollection[propName]);");
                codeBuilder.ComplementaryCode.AddLine("    }");
                codeBuilder.ComplementaryCode.AddLine("    if (result.length == 0 && firstIfNoItem)");
                codeBuilder.ComplementaryCode.AddLine("        result = complement.selectedCurrentItems(true);");
                codeBuilder.ComplementaryCode.AddLine("    return result;");
                codeBuilder.ComplementaryCode.AddLine("}");
                codeBuilder.ComplementaryCode.AddLine(", saveSelection: function() {");
                codeBuilder.ComplementaryCode.AddLine("    if (complement.vm.status() === 'C') { complement.currentPage = 0; complement.selectedCollection = {}; return; }");
                codeBuilder.ComplementaryCode.AddLine("    var pageProp = " + (binding.IsNullOrEmpty() ? "'Page' + complement.currentPage.toString()" : "'Page0'") + ";");
                codeBuilder.ComplementaryCode.AddLine("    complement.selectedCollection[pageProp] = complement.selectedCurrentItems();");
                codeBuilder.ComplementaryCode.AddLine("    complement.currentPage = complement.vm.dataToolbar.currentPage();");
                codeBuilder.ComplementaryCode.AddLine("}");

                codeBuilder.ComplementaryCode.AddLine(", selectedCurrentItems: function (firstIfNoItem, isSavingData) {");
                codeBuilder.ComplementaryCode.AddLine("      var grid = $('#" + idElement + "');");
                codeBuilder.ComplementaryCode.AddLine("      var selectedItems = [];");
                codeBuilder.ComplementaryCode.AddLine("      var ds = grid.data().igGrid.dataSource.dataView();");
                codeBuilder.ComplementaryCode.AddLine("      var rows = grid.igGridSelection(\"selectedRows\");");

                codeBuilder.ComplementaryCode.AddLine("      if (rows && rows.length == 0 && firstIfNoItem) {");
                codeBuilder.ComplementaryCode.AddLine("          var dataList = this." + listBinding + ";");
                codeBuilder.ComplementaryCode.AddLine("          var entity = (isSavingData ? findElementByKey(dataList, 'RowDataId', ds[0].RowDataId) : ds[0]);");
                codeBuilder.ComplementaryCode.AddLine("          if (entity) selectedItems.push(entity);");
                codeBuilder.ComplementaryCode.AddLine("      }");
                codeBuilder.ComplementaryCode.AddLine("      else if (rows && rows.length > 0) {");
                codeBuilder.ComplementaryCode.AddLine("          var dataList = this." + listBinding + ";");
                codeBuilder.ComplementaryCode.AddLine("          $.each(rows, function (index, value) {");
                codeBuilder.ComplementaryCode.AddLine("              var entity = (isSavingData ? findElementByKey(dataList, 'RowDataId', ds[value.index].RowDataId) : ds[value.index]);");
                codeBuilder.ComplementaryCode.AddLine("              if (entity) selectedItems.push(entity);");
                codeBuilder.ComplementaryCode.AddLine("          });");
                codeBuilder.ComplementaryCode.AddLine("      }");
                codeBuilder.ComplementaryCode.AddLine("      return selectedItems;");
                codeBuilder.ComplementaryCode.AddLine("}");
                codeBuilder.ComplementaryCode.AddLine(", clearSelectedItems: function () {");
                codeBuilder.ComplementaryCode.AddLine("      var grid = $('#" + idElement + "');");
                codeBuilder.ComplementaryCode.AddLine("      grid.igGridSelection('clearSelection');");
                codeBuilder.ComplementaryCode.AddLine("}");
            }
            #endregion
            codeBuilder.ComplementaryCode.AddLine(", render" + idElement.Replace("-", "").Replace(" ", "") + ": function(vm) {");
            codeBuilder.ComplementaryCode.IncreaseIndent();
            if (container.EnableMultiSelection)
            {
                codeBuilder.ComplementaryCode.AddLine("var self = this;");
                codeBuilder.ComplementaryCode.AddLine("self.vm = vm;");
            }
            var controlsKPI = controls.Where(i => i.KpiName != "").ToList();
            List<string> gaugeData = new List<string>();
            for (int i = 0; i < controlsKPI.Count; i++)
            {
                if (controls.Any(c => (c.ClassName.Contains("Gauge") || c.ClassName.Contains("KpiBox")) && c.BindingPath.Right(".").InList(controlsKPI[i].BindingPath.Right("."), controlsKPI[i].BindingPath.Right(".") + "KpiInfo")))
                {
                    string dataDef = "var dadosGauge" + controlsKPI[i].BindingPath.Right(".") + " = vm.get" + controlsKPI[i].KpiName + "GaugeGrid(function(ranges, min, max) { complement.render" + idElement.Replace("-", "").Replace(" ", "") + "(vm);  });";
                    if (!gaugeData.Contains(dataDef))
                    {
                        gaugeData.Add(dataDef);
                        codeBuilder.ComplementaryCode.AddLine(dataDef);
                    }
                }
            }

            //Control Data            
            if (binding.IsNullOrEmpty() && !_layOut.IsSecundary)
            {
                codeBuilder.ComplementaryCode.AddLine("if (!vm.hasMainTopDataGrid()) vm.hasMainTopDataGrid(true);");
                this.HasMainTopDataGrid = true;
            }

            codeBuilder.ComplementaryCode.AddLine("var getDataSource = function() {");
            codeBuilder.ComplementaryCode.AddLine("    var source = null;");
            codeBuilder.ComplementaryCode.AddLine("    try {");
            codeBuilder.ComplementaryCode.AddLine("        source = " + listBinding + ";");
            codeBuilder.ComplementaryCode.AddLine("    }");
            codeBuilder.ComplementaryCode.AddLine("    catch (e) { }");
            codeBuilder.ComplementaryCode.AddLine("    return isNullOrEmpty(source) ? ko.observableArray([]) : source;");
            codeBuilder.ComplementaryCode.AddLine("};");

            string parentRecord = listBinding.Left("." + listBinding.Right("."));

            if (parentRecord != "vm")
            {
                codeBuilder.ComplementaryCode.AddLine("var dataSourceIsLoaded = function() {");
                codeBuilder.ComplementaryCode.AddLine("    var isLoaded = false;");
                codeBuilder.ComplementaryCode.AddLine("    try {");
                codeBuilder.ComplementaryCode.AddLine("        isLoaded = (" + parentRecord + "." + dataView + "IsLoaded === true || " + parentRecord + "." + dataView + "List().length > 0);");
                codeBuilder.ComplementaryCode.AddLine("    }");
                codeBuilder.ComplementaryCode.AddLine("    catch (e) {");
                codeBuilder.ComplementaryCode.AddLine("        isLoaded = true;");
                codeBuilder.ComplementaryCode.AddLine("    }");
                codeBuilder.ComplementaryCode.AddLine("    return isLoaded;");
                codeBuilder.ComplementaryCode.AddLine("}");
            }

            codeBuilder.ComplementaryCode.AddLine("$('#" + idElement + "_headers').live('focus  keydown', function (evt) {");
            codeBuilder.ComplementaryCode.AddLine("    var keyCode = window.event ? evt.which : evt.keyCode;");
            codeBuilder.ComplementaryCode.AddLine("    if (keyCode === 9) {");
            codeBuilder.ComplementaryCode.AddLine("        var cols = $('#" + idElement + "').igGrid('option', 'columns');");
            codeBuilder.ComplementaryCode.AddLine("        var dataView = $('#" + idElement + "').data('igGrid').dataSource._dataView");
            codeBuilder.ComplementaryCode.AddLine("        if (dataView.length === 0) return;");
            codeBuilder.ComplementaryCode.AddLine("        var firstRow = dataView[0].RowDataId;");
            codeBuilder.ComplementaryCode.AddLine("        clear = vm.status() === 'C';");
            codeBuilder.ComplementaryCode.AddLine("        if (vm.status() === 'C')");
            codeBuilder.ComplementaryCode.AddLine("            $('#" + idElement + "').igGridUpdating('startEdit', firstRow, 0, true);");
            codeBuilder.ComplementaryCode.AddLine("        else {");
            codeBuilder.ComplementaryCode.AddLine("            var entity = findElementByKey(getDataSource(), 'RowDataId', firstRow);");
            codeBuilder.ComplementaryCode.AddLine("            var indexColumn = 0;");
            codeBuilder.ComplementaryCode.AddLine("            cols.some(function (entry) {");
            codeBuilder.ComplementaryCode.AddLine("                if (entry.key !== 'RowDataId' && !entry.hidden) {");
            codeBuilder.ComplementaryCode.AddLine("                    if (verifyCanEditCol(entry.key, clear, entity)) {");
            codeBuilder.ComplementaryCode.AddLine("                        $('#" + idElement + "').igGridUpdating('startEdit', firstRow, indexColumn, true);");
            codeBuilder.ComplementaryCode.AddLine("                        return true;");
            codeBuilder.ComplementaryCode.AddLine("                    }");
            codeBuilder.ComplementaryCode.AddLine("                    indexColumn++;");
            codeBuilder.ComplementaryCode.AddLine("                }");
            codeBuilder.ComplementaryCode.AddLine("            });");
            codeBuilder.ComplementaryCode.AddLine("        }");
            codeBuilder.ComplementaryCode.AddLine("    }");
            codeBuilder.ComplementaryCode.AddLine("});");

            codeBuilder.ComplementaryCode.AddLine("var getVisibleColumns = function(metaDataControl) {");
            if (!container.IsLinqSelectionControl)
                codeBuilder.ComplementaryCode.AddLine("   if (metaDataControl) return '';");
            codeBuilder.ComplementaryCode.AddLine("   var visibleColumns = '';");
            codeBuilder.ComplementaryCode.AddLine("   if($('#" + idElement + "').data('igGrid') === undefined) return '';");
            codeBuilder.ComplementaryCode.AddLine("   var cols = $('#" + idElement + "').igGrid('option', 'columns');");
            codeBuilder.ComplementaryCode.AddLine("   if (cols) {");
            codeBuilder.ComplementaryCode.AddLine("     for (var idx = 0; idx < cols.length; idx++) {");
            codeBuilder.ComplementaryCode.AddLine("         if (cols[idx].hidden !== true) visibleColumns += (visibleColumns === '' ? '' : ',') + cols[idx].key;");
            codeBuilder.ComplementaryCode.AddLine("     }");
            codeBuilder.ComplementaryCode.AddLine("   }");
            codeBuilder.ComplementaryCode.AddLine("   return visibleColumns;");
            codeBuilder.ComplementaryCode.AddLine("};");

            //Creating Binding Update
            codeBuilder.ComplementaryCode.AddLine("var started = false;");
            codeBuilder.ComplementaryCode.AddLine("var currentRow = null;");
            codeBuilder.ComplementaryCode.AddLine("var updateEntity = function (columnKey, value, execDataBind) {");
            codeBuilder.ComplementaryCode.AddLine("    if(value && Array.isArray(value) && value.length === 0) value = null;");
            codeBuilder.ComplementaryCode.AddLine("    var entity = findElementByKey(getDataSource(), '" + primaryKey + "', currentRow);");
            codeBuilder.ComplementaryCode.AddLine("    if (entity != null && typeof value !== 'undefined' && getAbsoluteValue(entity[columnKey]) !== value) {");
            codeBuilder.ComplementaryCode.AddLine("        setAbsoluteValue(entity, columnKey, value);");
            codeBuilder.ComplementaryCode.AddLine("        if (execDataBind) itemsSource.dataBind(false);");
            codeBuilder.ComplementaryCode.AddLine("    }");
            codeBuilder.ComplementaryCode.AddLine("};");

            codeBuilder.ComplementaryCode.AddLine("var isElementHided = function (grid, forceCreating) {");
            codeBuilder.ComplementaryCode.AddLine("  if (!grid) grid = $('#" + idElement + "');");
            codeBuilder.ComplementaryCode.AddLine("  return ((!grid[0] || (!forceCreating && grid.parent().width() <= 0)) && !$('#dialog" + container.Name + "').is(':visible'));");
            codeBuilder.ComplementaryCode.AddLine("}");

            codeBuilder.ComplementaryCode.AddLine("var refreshData = true;");
            codeBuilder.ComplementaryCode.AddLine("var itemsSource = { isElementHided: isElementHided, getVisibleColumns: getVisibleColumns, containerId: '" + idElement + "_container', dataBind: function (commitData, forceCreating) {");
            codeBuilder.ComplementaryCode.AddLine("   var grid = $('#" + idElement + "');");
            codeBuilder.ComplementaryCode.AddLine("   if (started && typeof grid.data('igGridUpdating') === 'undefined') { started = false; }");
            codeBuilder.ComplementaryCode.AddLine("   if (commitData && started) {");
            codeBuilder.ComplementaryCode.AddLine("       if (grid.igGridUpdating('isEditing')) {");
            codeBuilder.ComplementaryCode.AddLine("           grid.igGrid('commit');");
            codeBuilder.ComplementaryCode.AddLine("       }");
            codeBuilder.ComplementaryCode.AddLine("       return;");
            codeBuilder.ComplementaryCode.AddLine("   }");

            if (parentRecord != "vm")
            {
                codeBuilder.ComplementaryCode.AddLine("   var execFillDetais = ((vm.status() !== 'C' && vm.status() !== 'I') && !dataSourceIsLoaded());");
                codeBuilder.ComplementaryCode.AddLine("   if (forceCreating && started && !refreshData && !execFillDetais) return;");
            }
            else
                codeBuilder.ComplementaryCode.AddLine("   if (forceCreating && started && !refreshData) return;");

            codeBuilder.ComplementaryCode.AddLine("   var isHided = isElementHided(grid, forceCreating);");
            codeBuilder.ComplementaryCode.AddLine("   refreshData = !forceCreating;");
            codeBuilder.ComplementaryCode.AddLine("   if (refreshData && !isHided) refreshData = false;");

            codeBuilder.ComplementaryCode.AddLine("   if (isHided) return;");

            codeBuilder.ComplementaryCode.AddLine("   if (!started) {");
            codeBuilder.ComplementaryCode.AddLine("       createDataGrid(grid);");
            codeBuilder.ComplementaryCode.AddLine("       started = true;");
            codeBuilder.ComplementaryCode.AddLine("       commitData = false;");
            if (container.GroupByColumns.IsNullOrEmpty())
                codeBuilder.ComplementaryCode.AddLine("       $('#" + idElement + "_groupbyarea').addClass('hide');");
            codeBuilder.ComplementaryCode.AddLine("   }");

            codeBuilder.ComplementaryCode.AddLine("   if (grid.igGridUpdating('isEditing')) {");
            codeBuilder.ComplementaryCode.AddLine("        grid.igGridUpdating('endEdit', true);");
            codeBuilder.ComplementaryCode.AddLine("   }");

            if (parentRecord != "vm")
            {
                codeBuilder.ComplementaryCode.AddLine("   if (execFillDetais) {");
                codeBuilder.ComplementaryCode.AddLine("     grid.igGrid(\"option\", \"dataSource\", []);");
                codeBuilder.ComplementaryCode.AddLine("     " + parentRecord + ".fillDetails(false, '" + dataView + "');");
                codeBuilder.ComplementaryCode.AddLine("     return;");
                codeBuilder.ComplementaryCode.AddLine("   }");
            }

            if (container.EnableMultiSelection)
            {
                codeBuilder.ComplementaryCode.AddLine("   var selectedRows = complement.selectedItems();");
                codeBuilder.ComplementaryCode.AddLine("   grid.igGridSelection('clearSelection');");
            }

            if (binding.IsNullOrEmpty() && container.PageSize <= 0)
                codeBuilder.ComplementaryCode.AddLine("   grid.data('igGridSorting')._shouldFireColumnSorted = false;");

            codeBuilder.ComplementaryCode.AddLine("   grid.igGrid(\"option\", \"dataSource\", unwrapObservableArray(getDataSource(), vm));");

            if (!dataPrimaryKey.IsNullOrEmpty() && container.PageSize > 0 && !_layOut.IsSecundary)
            {
                codeBuilder.ComplementaryCode.AddLine("   if (vm.status() === 'E') {");
                codeBuilder.ComplementaryCode.AddLine("      grid.igGridSorting(\"sortColumn\", \"" + dataPrimaryKey + "\", \"ascending\");");
                codeBuilder.ComplementaryCode.AddLine("   }");
            }
            if (container.PageSize > 0)
            {
                codeBuilder.ComplementaryCode.AddLine("   grid.igGridPaging(\"option\", \"currentPageIndex\", 0);");
            }
            else if (binding.IsNullOrEmpty())
                codeBuilder.ComplementaryCode.AddLine("   grid.data('igGridSorting')._shouldFireColumnSorted = true;");

            codeBuilder.ComplementaryCode.AddLine("   var rows = grid.igGrid('allRows');");
            codeBuilder.ComplementaryCode.AddLine("   if (rows.length > 0) {");
            if (container.EnableMultiSelection)
            {
                codeBuilder.ComplementaryCode.AddLine("     if (selectedRows.length > 0) {");
                codeBuilder.ComplementaryCode.AddLine("         var dataView = grid.data().igGrid.dataSource.dataView();");
                codeBuilder.ComplementaryCode.AddLine("         if (dataView.length > 0) {");
                codeBuilder.ComplementaryCode.AddLine("             $.each(selectedRows, function (index, item) {");
                codeBuilder.ComplementaryCode.AddLine("                var idxFound = findIndexByKey(dataView, '" + primaryKey + "', getAbsoluteValue(item['" + primaryKey + "']))");
                codeBuilder.ComplementaryCode.AddLine("                if (idxFound < 0) idxFound = findIndexByKey(dataView, '" + dataPrimaryKey + "', getAbsoluteValue(item['" + dataPrimaryKey + "']))");
                codeBuilder.ComplementaryCode.AddLine("                if (idxFound >= 0) grid.igGridSelection(\"selectRow\", idxFound);");
                codeBuilder.ComplementaryCode.AddLine("             });");
                codeBuilder.ComplementaryCode.AddLine("         }");
                codeBuilder.ComplementaryCode.AddLine("     }");
            }
            else
            {
                codeBuilder.ComplementaryCode.AddLine("     var verticalContainer = grid.igGrid('scrollContainer');");
                codeBuilder.ComplementaryCode.AddLine("     var isSelected = false;");
                codeBuilder.ComplementaryCode.AddLine("     if (" + currentBinding + "() != null)");
                codeBuilder.ComplementaryCode.AddLine("     {");
                codeBuilder.ComplementaryCode.AddLine("       for(var idx = 0; idx < rows.length; idx++)");
                codeBuilder.ComplementaryCode.AddLine("       {");
                codeBuilder.ComplementaryCode.AddLine("         if (rows[idx].dataset.id == getAbsoluteValue(" + currentBinding + "().RowDataId))");
                codeBuilder.ComplementaryCode.AddLine("         {");
                codeBuilder.ComplementaryCode.AddLine("            grid.igGridSelection('selectRow', idx);");
                codeBuilder.ComplementaryCode.AddLine("            verticalContainer.scrollTop(grid.igGrid('option', 'avgRowHeight') * idx);");
                codeBuilder.ComplementaryCode.AddLine("            isSelected = true;");
                codeBuilder.ComplementaryCode.AddLine("            break;");
                codeBuilder.ComplementaryCode.AddLine("         }");
                codeBuilder.ComplementaryCode.AddLine("       }");
                codeBuilder.ComplementaryCode.AddLine("     }");
                codeBuilder.ComplementaryCode.AddLine("     if (!isSelected) {");
                codeBuilder.ComplementaryCode.AddLine("         grid.igGridSelection('selectRow', 0);");
                codeBuilder.ComplementaryCode.AddLine("         verticalContainer.scrollTop(0);");
                codeBuilder.ComplementaryCode.AddLine("     }");
                //todo: 

                if (_layOut.IsSecundary)
                {
                    codeBuilder.ComplementaryCode.AddLine("     $(grid.selector + '_container').focus();");
                }
            }
            codeBuilder.ComplementaryCode.AddLine("     if ($('#dialog" + container.Name + "').is(':visible')) {");
            codeBuilder.ComplementaryCode.AddLine("        var hasPaging = $.grep(grid.igGrid('option', 'features'), function (e) {");
            codeBuilder.ComplementaryCode.AddLine("           return e.name === 'Paging';");
            codeBuilder.ComplementaryCode.AddLine("        });");
            codeBuilder.ComplementaryCode.AddLine("        var totalGrid = grid.data('igGrid').options.dataSource.length;");
            codeBuilder.ComplementaryCode.AddLine("        var current = 1;");
            codeBuilder.ComplementaryCode.AddLine("        if (hasPaging.length > 0) {");
            codeBuilder.ComplementaryCode.AddLine("           var totalCurrentPage = totalGrid;");
            codeBuilder.ComplementaryCode.AddLine("           var currentPage = grid.igGridPaging('pageIndex') + 1;");
            codeBuilder.ComplementaryCode.AddLine("           var pageIndex = grid.igGridPaging('pageIndex');");
            codeBuilder.ComplementaryCode.AddLine("           var pageSize = grid.igGridPaging('pageSize');");
            codeBuilder.ComplementaryCode.AddLine("           if (totalGrid / pageSize > currentPage) totalCurrentPage = (1 * grid.igGrid('rows').length);");
            codeBuilder.ComplementaryCode.AddLine("           if (currentPage > 1) current = (pageIndex * pageSize) + current;");
            codeBuilder.ComplementaryCode.AddLine("           $('label#currentNumber" + container.Name + "').html(current + ' - ' + totalCurrentPage);");
            codeBuilder.ComplementaryCode.AddLine("        }");
            codeBuilder.ComplementaryCode.AddLine("        else");
            codeBuilder.ComplementaryCode.AddLine("           $('label#currentNumber" + container.Name + "').html(1);");
            codeBuilder.ComplementaryCode.AddLine("        $('label#totalNumber" + container.Name + "').html(totalGrid);");
            codeBuilder.ComplementaryCode.AddLine("    }");

            codeBuilder.ComplementaryCode.AddLine("   } else {");
            codeBuilder.ComplementaryCode.AddLine("       $('label#currentNumber" + container.Name + "').html(0);");
            codeBuilder.ComplementaryCode.AddLine("       $('label#totalNumber" + container.Name + "').html(0);");
            codeBuilder.ComplementaryCode.AddLine("   }");
            codeBuilder.ComplementaryCode.AddLine("}};");

            codeBuilder.ComplementaryCode.AddLine("var valueGrouBy = -1;");
            codeBuilder.ComplementaryCode.AddLine("var deletedIndex = -1;");

            //codeBuilder.ComplementaryCode.AddLine("$('#" + idElement + "').live(\"iggridheadercellrendered\", function(event, ui) {");
            //codeBuilder.ComplementaryCode.AddLine("    if (vm.common.getIdioma().indexOf('pt-br') >= 0)");
            //codeBuilder.ComplementaryCode.AddLine("        return;");
            //codeBuilder.ComplementaryCode.AddLine("    if (ui.columnKey == \"RowDataId\"){");
            //codeBuilder.ComplementaryCode.AddLine("        if (typeof objectLanguage_" + this.ViewModelName + " == \"function\" && Object.getOwnPropertyNames(objectLanguage_" + this.ViewModelName + "()).length > 0)");
            //codeBuilder.ComplementaryCode.AddLine("         vm.flattenLayout(ko.observable(vm.flattenObjectByProperty(objectLanguage_" + this.ViewModelName + "(), 'Name'))());");
            //codeBuilder.ComplementaryCode.AddLine("    }");
            //codeBuilder.ComplementaryCode.AddLine("    else");
            //codeBuilder.ComplementaryCode.AddLine("        $('#" + idElement + "').igGrid('headersTable').find('th[id$=\"' + ui.th[0].id + '\"] span.ui-iggrid-headertext').text(vm.flattenLayout()[ui.th[0].id].DisplayName);");
            //codeBuilder.ComplementaryCode.AddLine("});");

            codeBuilder.ComplementaryCode.AddLine("function verifyCanEditCol(column, clear, entity){");
            codeBuilder.ComplementaryCode.AddLine("    switch(column){");
            List<string> keyControl = new List<string>();
            foreach (var control in controls)
            {
                string key = control.BindingPath.Right(".");
                if (!keyControl.Contains(key))
                {
                    keyControl.Add(key);
                    if (control.ClassName == "MultimediaControl")
                        codeBuilder.ComplementaryCode.AddLine("        case '" + key + "Multi': { canEditing = false; break;}");
                    else
                        codeBuilder.ComplementaryCode.AddLine("        case '" + key + "': { canEditing = " + (control.AlwaysEditable ? "true" : (!container.CanEdit || container.EditionOnlyTemplate ? "clear" : (control.EditableOnInsert || (control.IsPartOfKey && control.IsEditable) ? "clear || (entity && entity.isAdded())" : (control.IsEditable ? "clear || vm.enabledForEditing()" : "clear")))) + "; break;}");
                }
            }

            codeBuilder.ComplementaryCode.AddLine("    }");
            codeBuilder.ComplementaryCode.AddLine("    return canEditing;");
            codeBuilder.ComplementaryCode.AddLine("};");


            if (controlsKPI.Count > 0)
            {
                codeBuilder.ComplementaryCode.AddLine("function makeGauge(val, record, field, solid, sufix) {");
                codeBuilder.ComplementaryCode.AddLine("    var row = 0, value = 0;");
                codeBuilder.ComplementaryCode.AddLine("    if (record.RowDataId > 0) {");
                codeBuilder.ComplementaryCode.AddLine("        row = record.RowDataId;");
                codeBuilder.ComplementaryCode.AddLine("        value = record[field];");
                codeBuilder.ComplementaryCode.AddLine("        if (solid) {");
                codeBuilder.ComplementaryCode.AddLine("             var descValue = record[field + (isNullOrEmpty(sufix) ? \"\" : sufix)];");
                codeBuilder.ComplementaryCode.AddLine("             return \"<div id='c\" + row + field + sufix + \"' style='color:black;text-align:\" + (isNullOrEmpty(sufix) ? \"right\" : \"center\") + \";background-color:\" + vm.getKpiColor(eval(eval(\"dadosGauge\" + field).ranges), value) + \";'><strong\" + (isNullOrEmpty(sufix) ? \" style='margin-right: 5px;'\" : \"\") + \">\" + descValue + \"</strong></div>\";");
                codeBuilder.ComplementaryCode.AddLine("        }");
                codeBuilder.ComplementaryCode.AddLine("        else");
                codeBuilder.ComplementaryCode.AddLine("             return \"<div id='g\" + row + field + \"' class='gauge' style='width:400px;height:20px;'></div> <script id='scriptg\" + row + field + \"'>$('#g\" + row + field + \"').kendoLinearGauge( {gaugeArea: {background: 'transparent', width:230}, pointer: { value: \" + value + \", color: '#8B8386', shape: 'arrow' }, scale: { vertical: false ,line:{visible: false}, labels: {visible: false}, min: \" + eval(\"dadosGauge\" + field).min + \", max: \" + eval(\"dadosGauge\" + field).max + \", ranges: \" + eval(\"dadosGauge\" + field).ranges + \" } });</script>\"");
                codeBuilder.ComplementaryCode.AddLine("    }");
                codeBuilder.ComplementaryCode.AddLine("    return '';");
                codeBuilder.ComplementaryCode.AddLine("}");
            }
            codeBuilder.ComplementaryCode.AddLine("function createDataGrid(grid) {");
            codeBuilder.ComplementaryCode.IncreaseIndent();
            #region  config layout edition
            codeBuilder.ComplementaryCode.AddLine("var gridId = grid[0].id;");

            codeBuilder.ComplementaryCode.AddLine("vm.gridSaveStates[gridId] = {");
            codeBuilder.ComplementaryCode.IncreaseIndent();
            codeBuilder.ComplementaryCode.AddLine("savedLayouts: typeof vm.gridSaveStates[gridId] === 'object' ? vm.gridSaveStates[gridId].savedLayouts: ko.observableArray([]),");
            codeBuilder.ComplementaryCode.AddLine("currentLayout: typeof vm.gridSaveStates[gridId] === 'object' ? vm.gridSaveStates[gridId].currentLayout : ko.observable({ Id: 0 }),");
            codeBuilder.ComplementaryCode.AddLine("currentLayoutId: typeof vm.gridSaveStates[gridId] === 'object' ? vm.gridSaveStates[gridId].currentLayoutId : ko.observable(0),");
            codeBuilder.ComplementaryCode.AddLine("__applyLayout: function (jsonContent) {");
            codeBuilder.ComplementaryCode.AddLine("    this.gridSaveStates.returnToSavedState(jsonContent);");
            codeBuilder.ComplementaryCode.AddLine("    vm.dataToolbar.isBusy(false);");
            codeBuilder.ComplementaryCode.AddLine("    this.closePopover();");
            codeBuilder.ComplementaryCode.AddLine("},");
            codeBuilder.ComplementaryCode.AddLine("closePopover: function () {");
            codeBuilder.ComplementaryCode.AddLine("    $('#" + idElement + "_LayoutBtn').igPopover('hide');");
            codeBuilder.ComplementaryCode.AddLine("},");
            codeBuilder.ComplementaryCode.AddLine("applyLayout: function (layoutInfo) {");
            codeBuilder.ComplementaryCode.AddLine("    var _this = this;");
            codeBuilder.ComplementaryCode.AddLine("    if (isNull(layoutInfo) && (!_this.currentLayout() || _this.currentLayout().Id === 0)) {");
            codeBuilder.ComplementaryCode.AddLine("        vm.app.showMessage('" + "Não existe layout selecionado".Translate() + "');");
            codeBuilder.ComplementaryCode.AddLine("        return;");
            codeBuilder.ComplementaryCode.AddLine("    }");
            codeBuilder.ComplementaryCode.AddLine("    vm.dataToolbar.isBusy(true);");
            codeBuilder.ComplementaryCode.AddLine("    if (layoutInfo && layoutInfo.ConteudoJson) {");
            codeBuilder.ComplementaryCode.AddLine("        _this.__applyLayout(layoutInfo.ConteudoJson)");
            codeBuilder.ComplementaryCode.AddLine("    }");
            codeBuilder.ComplementaryCode.AddLine("    else if (_this.currentLayout() && _this.currentLayout().ConteudoJson) {");
            codeBuilder.ComplementaryCode.AddLine("        _this.__applyLayout(_this.currentLayout().ConteudoJson)");
            codeBuilder.ComplementaryCode.AddLine("    } else {");
            codeBuilder.ComplementaryCode.AddLine("        managerUser.getGridLayout(_this.currentLayout().Id).then(function (result) {");
            codeBuilder.ComplementaryCode.AddLine("            _this.currentLayout(result);");
            codeBuilder.ComplementaryCode.AddLine("            var _arr = _this.savedLayouts(); ");
            codeBuilder.ComplementaryCode.AddLine("            for (var i = 0 ; i < _arr.length; i++) {");
            codeBuilder.ComplementaryCode.AddLine("                if (_arr[i].Id === result.Id)");
            codeBuilder.ComplementaryCode.AddLine("                    _arr[i] = result;");
            codeBuilder.ComplementaryCode.AddLine("            }");
            codeBuilder.ComplementaryCode.AddLine("            _this.savedLayouts(_arr);");
            codeBuilder.ComplementaryCode.AddLine("            _this.__applyLayout(result.ConteudoJson);");
            codeBuilder.ComplementaryCode.AddLine("        });");
            codeBuilder.ComplementaryCode.AddLine("    }");
            codeBuilder.ComplementaryCode.AddLine("},");
            codeBuilder.ComplementaryCode.AddLine("openLayoutCustomize: function(saveAs) {");
            codeBuilder.ComplementaryCode.AddLine("    var _this = this;");
            codeBuilder.ComplementaryCode.AddLine("    var _open = function () {");
            codeBuilder.ComplementaryCode.AddLine("        require(['viewmodels/shared/gridConfiguration'], function (mdl) {");
            codeBuilder.ComplementaryCode.AddLine("            _this.closePopover();");
            codeBuilder.ComplementaryCode.AddLine("            mdl.showModal(vm, vm.gridSaveStates[gridId], gridId, saveAs).then(function (refreshSource, selectedLayout) {");
            codeBuilder.ComplementaryCode.AddLine("                _this.loadLayouts(true).then(function () {");
            codeBuilder.ComplementaryCode.AddLine("                    if (typeof selectedLayout === 'object' && selectedLayout != null) {");
            codeBuilder.ComplementaryCode.AddLine("                        _this.currentLayoutId(selectedLayout.Id);");
            codeBuilder.ComplementaryCode.AddLine("                        _this.currentLayout(selectedLayout);");
            codeBuilder.ComplementaryCode.AddLine("                        _this.applyLayout(selectedLayout);");
            codeBuilder.ComplementaryCode.AddLine("                    }");
            codeBuilder.ComplementaryCode.AddLine("                    if (typeof selectedLayout === 'number' && selectedLayout > 0) {");
            codeBuilder.ComplementaryCode.AddLine("                        _this.savedLayouts().forEach(function(item) {");
            codeBuilder.ComplementaryCode.AddLine("                            if (item.Id === selectedLayout) {");
            codeBuilder.ComplementaryCode.AddLine("                                _this.currentLayoutId(selectedLayout);");
            codeBuilder.ComplementaryCode.AddLine("                                _this.currentLayout(item);");
            codeBuilder.ComplementaryCode.AddLine("                                _this.applyLayout();");
            codeBuilder.ComplementaryCode.AddLine("                            }");
            codeBuilder.ComplementaryCode.AddLine("                        });");
            codeBuilder.ComplementaryCode.AddLine("                    }");
            codeBuilder.ComplementaryCode.AddLine("                });");
            codeBuilder.ComplementaryCode.AddLine("            });");
            codeBuilder.ComplementaryCode.AddLine("        });");
            codeBuilder.ComplementaryCode.AddLine("    };");
            codeBuilder.ComplementaryCode.AddLine("    if (this.currentLayout() && this.currentLayout().Id > 0 && isNullOrEmpty(this.currentLayout().ConteudoJson)) {");
            codeBuilder.ComplementaryCode.AddLine("        managerUser.getGridLayout(_this.currentLayout().Id).then(function (result) {");
            codeBuilder.ComplementaryCode.AddLine("            _this.currentLayout(result);");
            codeBuilder.ComplementaryCode.AddLine("            var _arr = _this.savedLayouts();");
            codeBuilder.ComplementaryCode.AddLine("            for (var i = 0 ; i < _arr.length; i++) {");
            codeBuilder.ComplementaryCode.AddLine("                if (_arr[i].Id === result.Id)");
            codeBuilder.ComplementaryCode.AddLine("                    _arr[i] = result;");
            codeBuilder.ComplementaryCode.AddLine("            }");
            codeBuilder.ComplementaryCode.AddLine("            _this.savedLayouts(_arr);");
            codeBuilder.ComplementaryCode.AddLine("            _open();");
            codeBuilder.ComplementaryCode.AddLine("        });");
            codeBuilder.ComplementaryCode.AddLine("    } else {");
            codeBuilder.ComplementaryCode.AddLine("        _open();");
            codeBuilder.ComplementaryCode.AddLine("    }");
            codeBuilder.ComplementaryCode.AddLine("},");
            codeBuilder.ComplementaryCode.AddLine("loadLayouts: function (force) {");
            codeBuilder.ComplementaryCode.AddLine("    var dfd = $.Deferred(), _this = this;");
            codeBuilder.ComplementaryCode.AddLine("    if (force || _this.savedLayouts().length === 0) {");
            codeBuilder.ComplementaryCode.AddLine("         managerUser.getAllGridLayouts(vm.__moduleId__, gridId).then(function (results) {");
            codeBuilder.ComplementaryCode.AddLine("             _this.savedLayouts(results);");
            codeBuilder.ComplementaryCode.AddLine("             _this.savedLayouts.splice(0, 0, _this.defaultLayout);");
            codeBuilder.ComplementaryCode.AddLine("             dfd.resolve();");
            codeBuilder.ComplementaryCode.AddLine("         });");
            codeBuilder.ComplementaryCode.AddLine("    } else {");
            codeBuilder.ComplementaryCode.AddLine("         dfd.resolve();");
            codeBuilder.ComplementaryCode.AddLine("    }");
            codeBuilder.ComplementaryCode.AddLine("    return dfd;");
            codeBuilder.ComplementaryCode.AddLine("},");
            codeBuilder.ComplementaryCode.AddLine("deleteLayout: function () {");
            codeBuilder.ComplementaryCode.AddLine("    var _this = this;");
            codeBuilder.ComplementaryCode.AddLine("    return vm.app.showMessage('" + "Deseja realmente excluir o Layout".Translate() + " [' + _this.currentLayout().NomeLayout + ']?', 'Alerta', ['Yes', 'No'])");
            codeBuilder.ComplementaryCode.AddLine("    .then(function (selectedOption) {");
            codeBuilder.ComplementaryCode.AddLine("        if (selectedOption === 'Yes') {");
            codeBuilder.ComplementaryCode.AddLine("            managerUser.deleteGridLayout(_this.currentLayout().Id, _this.currentLayout().Modulo, _this.currentLayout().NomeObjeto).then(function () {");
            codeBuilder.ComplementaryCode.AddLine("                vm.app.showMessage('" + "Excluido com sucesso!".Translate() + "', 'Alerta');");
            codeBuilder.ComplementaryCode.AddLine("                _this.loadLayouts(true).then(function () {");
            codeBuilder.ComplementaryCode.AddLine("                    _this.currentLayoutId(_this.savedLayouts()[0].Id);");
            codeBuilder.ComplementaryCode.AddLine("                    _this.applyLayout();");
            codeBuilder.ComplementaryCode.AddLine("                });;");
            codeBuilder.ComplementaryCode.AddLine("            });");
            codeBuilder.ComplementaryCode.AddLine("        }");
            codeBuilder.ComplementaryCode.AddLine("    });");
            codeBuilder.ComplementaryCode.AddLine("},");
            codeBuilder.ComplementaryCode.AddLine("initialize: function () {");
            codeBuilder.ComplementaryCode.AddLine("    var _this = this;");
            codeBuilder.ComplementaryCode.AddLine("    _this.currentLayoutId.subscribe(function (newItem) {");
            codeBuilder.ComplementaryCode.AddLine("        _this.currentLayout(null);");
            codeBuilder.ComplementaryCode.AddLine("        var _arr = _this.savedLayouts();");
            codeBuilder.ComplementaryCode.AddLine("        for (var i = 0 ; i < _arr.length; i++) {");
            codeBuilder.ComplementaryCode.AddLine("            if (_arr[i].Id === newItem)");
            codeBuilder.ComplementaryCode.AddLine("                _this.currentLayout(_arr[i]);");
            codeBuilder.ComplementaryCode.AddLine("        }");
            codeBuilder.ComplementaryCode.AddLine("    });");
            codeBuilder.ComplementaryCode.AddLine("    _this.loadLayouts();");
            codeBuilder.ComplementaryCode.AddLine("}");
            codeBuilder.ComplementaryCode.DecreaseIndent();
            codeBuilder.ComplementaryCode.AddLine("};");
            codeBuilder.ComplementaryCode.AddLine("vm.gridSaveStates[gridId].initialize();");


            #endregion

            //Create Grid View               
            codeBuilder.ComplementaryCode.AddLine("grid.igGrid({ height: " + (((_layOut.Containers.Count == 1 && _layOut.Containers[0] == container) || (_layOut.Containers.Count == 2 && _layOut.Containers[0] == container && _layOut.Containers[1].IsTemplate)) ? "(vm.isDependentVM() ? getGridHeightSuggested() * 0.7 : $(window).height() * 0.85)" : GetGridHeight(container)) + ", width: " + GetGridWidth(container) + ",");
            codeBuilder.ComplementaryCode.IncreaseIndent();
            codeBuilder.ComplementaryCode.AddLine("dataSource: [],");
            codeBuilder.ComplementaryCode.AddLine("primaryKey: '" + primaryKey + "',");
            codeBuilder.ComplementaryCode.AddLine("autoGenerateColumns: false,");
            codeBuilder.ComplementaryCode.AddLine("autofitLastColumn: false,");

            codeBuilder.ComplementaryCode.AddLine("dataSourceType: 'json',");
            if (innerDataGrids != null && innerDataGrids.Count > 0)
                codeBuilder.ComplementaryCode.AddLine("autoGenerateLayouts: false,");
            codeBuilder.ComplementaryCode.AddLine("renderCheckboxes: true,");
            codeBuilder.ComplementaryCode.AddLine("autoCommit: true,");
            if (container.Virtualization)
            {
                codeBuilder.ComplementaryCode.AddLine("rowVirtualization: true,");
                codeBuilder.ComplementaryCode.AddLine("virtualizationMode: \"continuous\",");
            }

            codeBuilder.ComplementaryCode.AddLine("cellClick: function(evt, ui) {");
            codeBuilder.ComplementaryCode.AddLine("     if (ui.cellElement && ui.cellElement.childNodes[0] && ui.cellElement.childNodes[0].childNodes[1]) {");
            codeBuilder.ComplementaryCode.AddLine("         var entity = null, e = ui.cellElement.childNodes[0].childNodes[1];");
            codeBuilder.ComplementaryCode.AddLine("         if (e && e.tagName === 'IMG' && vm.status() !== 'C')");
            codeBuilder.ComplementaryCode.AddLine("         {");
            codeBuilder.ComplementaryCode.AddLine("              entity = findElementByKey(getDataSource(), '" + primaryKey + "', ui.rowKey);");
            codeBuilder.ComplementaryCode.AddLine("              var key = e.attributes['key'].value;");
            codeBuilder.ComplementaryCode.AddLine("              var table = e.attributes['tableName'].value;");
            codeBuilder.ComplementaryCode.AddLine("              showMultimidia(entity, e, table, key, vm." + this.ViewModelName + "());");
            codeBuilder.ComplementaryCode.AddLine("         }");
            codeBuilder.ComplementaryCode.AddLine("     }");
            codeBuilder.ComplementaryCode.AddLine("     if (typeof vm.OnGridClientClick === 'function') {");
            codeBuilder.ComplementaryCode.AddLine("         entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowKey);");
            codeBuilder.ComplementaryCode.AddLine("         vm.OnGridClientClick('" + idElement + "', ui.colKey, entity);");
            codeBuilder.ComplementaryCode.AddLine("     }");
            codeBuilder.ComplementaryCode.AddLine("     if (vm.status() != 'Q') {");
            codeBuilder.ComplementaryCode.AddLine("         var grid = $('#" + idElement + "');");
            codeBuilder.ComplementaryCode.AddLine("         var isEditing = grid.igGridUpdating('isEditing');");
            codeBuilder.ComplementaryCode.AddLine("         if (!isEditing && ui.colKey != undefined)");
            codeBuilder.ComplementaryCode.AddLine("             grid.igGridUpdating('startEdit', ui.rowKey, ui.colKey, true);");
            codeBuilder.ComplementaryCode.AddLine("     }");
            codeBuilder.ComplementaryCode.AddLine("},");
            codeBuilder.ComplementaryCode.AddLine("enableUTCDates: true,");
            codeBuilder.ComplementaryCode.AddLine("featureChooserIconDisplay: 'always',");
            codeBuilder.ComplementaryCode.AddLine("rendered: function(evt, ui) {");
            codeBuilder.ComplementaryCode.AddLine("    if (isNull(vm.gridSaveStates[ui.owner.id()].gridSaveStates)) {");
            codeBuilder.ComplementaryCode.AddLine("        vm.gridSaveStates[ui.owner.id()].gridSaveStates = gridSaveStates(ui.owner.element, vm);");
            codeBuilder.ComplementaryCode.AddLine("        vm.gridSaveStates[ui.owner.id()].defaultLayout = { Id: -1, NomeLayout: \"" + "Layout Padrão".Translate() + "\", ConteudoJson: vm.gridSaveStates[ui.owner.id()].gridSaveStates.save() };");
            codeBuilder.ComplementaryCode.AddLine("    }");
            codeBuilder.ComplementaryCode.AddLine("    setTimeout(function() { $('#' + ui.owner.id() + '_headers>thead>tr>th').each(function(i, item) { if (item.attributes['aria-label']) { item.attributes['title'].value = item.attributes['aria-label'].value; } }); ");
            //Se a Grid possui layout selecionado efetua o applyLayout
            codeBuilder.ComplementaryCode.AddLine("    if (vm.gridSaveStates[ui.owner.id()].currentLayout().Id !== 0) {");
            codeBuilder.ComplementaryCode.AddLine("         vm.gridSaveStates[ui.owner.id()].applyLayout(vm.gridSaveStates[ui.owner.id()].currentLayout());");
            codeBuilder.ComplementaryCode.AddLine("    }");
            ///
            codeBuilder.ComplementaryCode.AddLine("    }, 500);");
            codeBuilder.ComplementaryCode.AddLine("    $('.ui-icon-gear').remove();");
            codeBuilder.ComplementaryCode.AddLine("},");
            codeBuilder.ComplementaryCode.AddLine("dataRendered: function(evt, ui) { ");

            if (container.EnableMultiSelection)
            {
                codeBuilder.ComplementaryCode.AddLine("$('th.ui-iggrid-rowselector-class').unbind('click') ");
            }

            if (controls.Any(c => c.ClassName == "MultimediaControl"))
            {
                codeBuilder.ComplementaryCode.AddLine("   showMultimidiaLazy('#" + idElement + "');");
            }
            if (controlsKPI.Count > 0)
            {
                codeBuilder.ComplementaryCode.AddLine("    if ($('.gauge').length) {");
                codeBuilder.ComplementaryCode.AddLine("        var x = document.getElementsByClassName('gauge');");
                codeBuilder.ComplementaryCode.AddLine("        for (var i = 0; i < x.length; i++)");
                codeBuilder.ComplementaryCode.AddLine("            eval(document.getElementById('script' + x[i].id).innerHTML);");
                codeBuilder.ComplementaryCode.AddLine("    }");
            }
            //codeBuilder.ComplementaryCode.AddLine("   if(typeof $('#" + idElement + "').data().igGridGroupBy == 'object' && !$('#" + idElement + "').data().igGridGroupBy._isgroup)");
            //codeBuilder.ComplementaryCode.AddLine("      $('#" + idElement + "_groupbyarea').addClass('hide');");

            codeBuilder.ComplementaryCode.AddLine("},");
            codeBuilder.ComplementaryCode.AddLine("columns: [");

            //Add primary key column
            codeBuilder.ComplementaryCode.AddLine("    { key: '" + primaryKey + "', headerText: '" + primaryKey + "', width: '50px', dataType: '" + primaryKeyType + "', hidden: true },");

            string gridColumnSettings = string.Empty;
            string gridColumnSettingsTooltips = string.Empty;

            Func<string, int> GetIntValue = (str) =>
            {
                int value = 0;
                int.TryParse(str, out value);
                return value;
            };

            var hasGroupColumn = controls.FindAll(x => x.ColumnMultiHeader != null && x.ColumnMultiHeader != "").OrderBy(x => GetIntValue(x.DataGridOrder));
            var isNotGroupColumn = controls.FindAll(x => x.ColumnMultiHeader == null || x.ColumnMultiHeader == "");

            var controlVerified = hasGroupColumn.Concat(isNotGroupColumn).ToList();
            var columnCurrent = string.Empty;

            for (int cIndex = 0; cIndex < controls.Count; cIndex++)
            {
                //var control = controls[cIndex];
                var control = controlVerified[cIndex];
                var controlId = (_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + control.GetDefaultControlName();
                var controlHeaderName = this.ViewModelName + "_" + control.GetDefaultControlName();

                if (hasGroupColumn.Count() > 0 && (control.ColumnMultiHeader != null && control.ColumnMultiHeader != "") && (columnCurrent != control.ColumnMultiHeader))
                {
                    codeBuilder.ComplementaryCode.AddLine("{");
                    codeBuilder.ComplementaryCode.AddLine("    headerText: '" + control.ColumnMultiHeader + "',");
                    codeBuilder.ComplementaryCode.AddLine("    group: [");
                }

                //int ctrlWidth = (control.ControlWidth != ControlWidth.Automatic ? HtmlCodeGen.GetMaxWidth(control.ControlWidth) :
                // HtmlCodeGen.GetElementWidth(control.ClassName, control.DataType, control.DisplayName, control.ClassName == "MultimediaControl" ? control.MediaWidth.ToString() : control.DataFormatString, true, control.Precision));
                int ctrlWidth = control.DataGridWidth;

                propertyKey = control.BindingPath.Right(".");
                var visibleControl = (control.IsVisible && control.FieldVisibleGrid != VisibleFieldGrid.Editor ? "false" : "true");
                gridColumnSettingsTooltips += (gridColumnSettingsTooltips.IsNullOrEmpty() ? "" : ",") +
                    "{ columnKey: \"" + propertyKey + "\", allowTooltips: " + (control.ClassName != "LookUpTextBox").ToString().ToLower() + " }";

                string columnCssClass = " columnCssClass: " + (!control.DataGridWordWrap ? "'ellipsis'" : "''");

                switch (control.ClassName)
                {
                    case "MultimediaControl":
                        codeBuilder.ComplementaryCode.AddLine("    { key: '" + propertyKey + "Multi', headerText: '', width: '" + ctrlWidth.ToString() + "px', dataType: 'string'," + columnCssClass + ", format: '', hidden: " + visibleControl + ", unbound: true, group: null, " +
                                "formula: function(data, grid) { var templateName = getTemplateImageName(vm.status(), 'grid'); var entity = findElementByKey(getDataSource(), 'RowDataId', data.RowDataId); " +
                                "var url = loadMultimidiaUrl('" + control.Name.Left(".") + "', entity." + control.BindingPath.Right(".") + ", vm." + this.ViewModelName + "()); return \"<div class='" + GetMediaWidth(control.MediaWidth) + "'>\" + ko.renderTemplateX(templateName, vm, { tableName: '" + control.Name.Left(".") + "', key: getAbsoluteValue(entity." + control.BindingPath.Right(".") + "), vm: vm." + this.ViewModelName + "(), url: url })+ \"</div>\";}}" + (cIndex == controls.Count - 1 ? String.Empty : ","));
                        break;
                    case "KpiBox":
                    case "Gauge":
                        var nameGauge = control.BindingPath.Right(".");
                        string kpiSufix = "";
                        if (nameGauge.Length > 7 && nameGauge.Right(7) == "KpiInfo")
                        {
                            kpiSufix = "KpiInfo";
                            nameGauge = nameGauge.Remove(nameGauge.Length - 7);
                        }
                        var template = "\"<div id='${RowDataId}' style='width:400px;height:20px;'></div> <script>$('#${RowDataId}').kendoLinearGauge( {gaugeArea: {background: 'transparent', width:230}, pointer: { value: ${" + nameGauge + "}, color: '#8B8386', shape: 'arrow' }, scale: { vertical: false ,line:{visible: false}, labels: {visible: false}, min: \" + dadosGauge" + nameGauge + ".min + \", max: \" + dadosGauge" + nameGauge + ".max + \", ranges: \" + dadosGauge" + nameGauge + ".ranges + \" } });</script>\"";
                        var formatter = "function (val, record) { return makeGauge(val, record, '" + nameGauge + "'" + (control.ClassName == "KpiBox" ? ", true, '" + kpiSufix + "'" : "") + "); }";
                        codeBuilder.ComplementaryCode.AddLine("    { key: '" + propertyKey + "', headerText: vm." + (_layOut.IsSecundary ? "getLayoutDisplayName" : "getLayoutHeaderGrid") + "('" + controlHeaderName + "'), width: '" + ctrlWidth.ToString() + "px', dataType: '" + GetPropDataType(control.DataType, control.DomainName) + "'," + columnCssClass + ", format: '" + GetFormatDataType(control) + "', hidden: " + visibleControl + ", unbound: false, group: null, formatter: " + formatter + " }" + (cIndex == controls.Count - 1 ? String.Empty : ","));
                        break;
                    case "MaskedTextBox":
                        codeBuilder.ComplementaryCode.AddLine("    { key: '" + propertyKey + "', headerText: vm." + (_layOut.IsSecundary ? "getLayoutDisplayName" : "getLayoutHeaderGrid") + "('" + controlHeaderName + "'), width: '" + ctrlWidth.ToString() + "px', dataType: '" + GetPropDataType(control.DataType, control.DomainName) + "'," + columnCssClass + ", hidden: " + visibleControl + ", formatter: function(val) { return (val == null ? '' : val.toString()).mask('" + GetMaskForDisplay(control) + "') } }" + (cIndex == controls.Count - 1 ? String.Empty : ","));
                        gridColumnSettings += (gridColumnSettings.IsNullOrEmpty() ? "" : ", ") + "{ columnKey: \"" + propertyKey + "\", editorOptions: { type: \"mask\", inputMask: \"" + control.Mask + "\", dataMode: 0 } }";
                        break;
                    case "CustomControl":
                        var customHtml = "\"" + control.HtmlCode.Replace('\r', ' ').Replace('\n', ' ').Replace("\"", "\\\"") + "\"";
                        codeBuilder.ComplementaryCode.AddLine("    { key: '" + control.Name + "', headerText: vm." + (_layOut.IsSecundary ? "getLayoutDisplayName" : "getLayoutHeaderGrid") + "('" + controlHeaderName + "'), width: '" + ctrlWidth.ToString() + "px', dataType: 'string', hidden: " + visibleControl + ", unbound: true, template: " + customHtml + " }" + (cIndex == controls.Count - 1 ? String.Empty : ","));
                        break;
                    default:
                        codeBuilder.ComplementaryCode.AddLine("    { key: '" + propertyKey + "', headerText: vm." + (_layOut.IsSecundary ? "getLayoutDisplayName" : "getLayoutHeaderGrid") + "('" + controlHeaderName + "'), width: '" + ctrlWidth.ToString() + "px', dataType: '" + (control.ClassName == "LookUpTextBox" ? "string" : GetPropDataType(control.DataType, control.DomainName)) + "'," + columnCssClass + ", format: '" + (control.ClassName == "LookUpTextBox" ? "" : GetFormatDataType(control)) + "', hidden: " + visibleControl + ", unbound: false, group: null " + (control.ClassName == "NumericTextBox" ? ", formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); } " : " ") + (control.ClassName == "ComboBox" && !control.DomainName.IsNullOrEmpty() ? ", formatter: function (val, record) { return  vm.dataDomains.getName('" + control.DomainName + "', val);}" : "") + " }" + (cIndex == controls.Count - 1 ? String.Empty : ","));
                        if (!control.ClassName.InList("ComboBox", "LookUpTextBox", "DateTimeTextBox", "CheckBox"))
                        {
                            gridColumnSettings += (gridColumnSettings.IsNullOrEmpty() ? "" : ", ") +
                                "{ columnKey: \"" + propertyKey + "\" " +
                                (control.ClassName == "NumericTextBox" ?
                                (", editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('" + idElement + "', '" + propertyKey + "', ui.oldValue, ui.value);}},  maxLength: " + GetMaxLengthNumeric(control) + ", maxValue: " + GetMaxValueNumeric(control, false, false, false) + ", " + GetControlRange(control) + "dataMode: '" + getNumericDataMode(control) + "'" + (control.GetPrecisionDecimalsInt() > 0 ? ", minDecimals: " + control.GetPrecisionDecimalsInt().ToString() + ", maxDecimals: " + control.GetPrecisionDecimalsInt().ToString() : "") + " }")
                                :
                                (", editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('" + idElement + "', '" + propertyKey + "', ui.oldValue, ui.value);}}, maxLength: " + GetMaxLengthNumeric(control) + " }")) + " }";
                        }
                        break;
                }
                columnCurrent = control.ColumnMultiHeader;
                var nextControl = (controlVerified.Count > (cIndex + 1) ? controlVerified[cIndex + 1] : null);
                //if(nextControl.GetPropertyValue("ColumnMultiHeader") == null)
                //&& columnCurrent != nextControl.ColumnMultiHeader
                bool closeGroupHeader = false;
                if (nextControl == null) closeGroupHeader = true;
                else if (columnCurrent != nextControl.ColumnMultiHeader) closeGroupHeader = true;
                if (hasGroupColumn.Count() > 0 && (control.ColumnMultiHeader != null && control.ColumnMultiHeader != "") && closeGroupHeader)
                {
                    codeBuilder.ComplementaryCode.AddLine("    ]");
                    codeBuilder.ComplementaryCode.AddLine("},");
                }

            }
            codeBuilder.ComplementaryCode.AddLine("],");
            codeBuilder.ComplementaryCode.AddLine("features: [");
            if (container.PageSize > 0) codeBuilder.ComplementaryCode.AddLine("            { name: 'Paging', type: 'local', pageSizeDropDownLocation: 'inpager', pageSize: " + container.PageSize.ToString() + ", pageIndexChanged: function (evt, ui) { if (!$('#" + idElement + "').igGridSelection('option', 'multipleSelection')) $('#" + idElement + "').igGridSelection('selectRow', 0); selectGridCurrentItem(vm.goToKey, '" + primaryKey + "', ui" + (binding.IsNullOrEmpty() ? "" : ", " + currentBinding + ", getDataSource()") + "); } },");
            codeBuilder.ComplementaryCode.AddLine("            { name: 'Sorting', type: 'local', caseSensitive: false, unsortedColumnTooltip: '', sortedColumnTooltip: '',");
            codeBuilder.ComplementaryCode.AddLine("              columnSorting: function (evt, ui) { }");
            codeBuilder.ComplementaryCode.AddLine("              , customSortFunction: function (data, fields, direction) { return gridFunctions.sort(data, fields, direction); }");
            codeBuilder.ComplementaryCode.AddLine((binding.IsNullOrEmpty() && container.PageSize <= 0 ? "              , columnSorted: function (event, args) { if (!isNullOrEmpty(args.columnKey) && !isNullOrEmpty(args.direction)) { vm.sortData(args.columnKey + ' ' + args.direction); } } " : "") + "},");
            codeBuilder.ComplementaryCode.AddLine("            { name: 'Filtering', mode: 'advanced', filterDropDownItemIcons: false, filterDropDownWidth: 200, allowFiltering: true, type: 'local', renderFC: false, renderFilterButton: true, ");
            codeBuilder.ComplementaryCode.AddLine("                  dataFiltered: function (evt, ui) {");
            codeBuilder.ComplementaryCode.AddLine("                  var columnsFilters = [];");
            codeBuilder.ComplementaryCode.AddLine("                  $.each(ui.owner._currentAdvancedExpressions, function(i, item){");
            codeBuilder.ComplementaryCode.AddLine("                      if (item.expr != null)");
            codeBuilder.ComplementaryCode.AddLine("                         columnsFilters.push(item.fieldName);");
            codeBuilder.ComplementaryCode.AddLine("                  });");
            codeBuilder.ComplementaryCode.AddLine("                  var cols = $('#' + ui.owner.grid.element[0].id + '_container .ui-iggrid-headertable th');");
            codeBuilder.ComplementaryCode.AddLine("                  cols.each(function (i, item) {");
            codeBuilder.ComplementaryCode.AddLine("                      var name = item.id.substr(ui.owner.grid.element[0].id.length + 1);");
            codeBuilder.ComplementaryCode.AddLine("                      var filter = $(item).find('span.ui-icon-search');");
            codeBuilder.ComplementaryCode.AddLine("                      if (columnsFilters.contains(name)) {");
            codeBuilder.ComplementaryCode.AddLine("                          if (!filter.hasClass('grid-column-researched'))");
            codeBuilder.ComplementaryCode.AddLine("                              filter.addClass('grid-column-researched');");
            codeBuilder.ComplementaryCode.AddLine("                      } else {");
            codeBuilder.ComplementaryCode.AddLine("                          if (filter.hasClass('grid-column-researched'))");
            codeBuilder.ComplementaryCode.AddLine("                              filter.removeClass('grid-column-researched');");
            codeBuilder.ComplementaryCode.AddLine("                      }");
            codeBuilder.ComplementaryCode.AddLine("                  });");
            codeBuilder.ComplementaryCode.AddLine("                },");
            codeBuilder.ComplementaryCode.AddLine("            dataFiltering: function (evt, ui) {");
            codeBuilder.ComplementaryCode.AddLine("                 if (ui.newExpressions.length == 1) {");
            codeBuilder.ComplementaryCode.AddLine("                     if (ui.newExpressions[0].expr == null) return false;");
            codeBuilder.ComplementaryCode.AddLine("                 } else {");
            codeBuilder.ComplementaryCode.AddLine("                     $.grep(ui.newExpressions, function (e) {");
            codeBuilder.ComplementaryCode.AddLine("                         return e.logic = 'OR';");
            codeBuilder.ComplementaryCode.AddLine("                     });");
            codeBuilder.ComplementaryCode.AddLine("                 }");
            codeBuilder.ComplementaryCode.AddLine("            },");
            codeBuilder.ComplementaryCode.AddLine("            filterDialogOpening: function (evt, ui) {");
            codeBuilder.ComplementaryCode.AddLine("                 var dgl = ui.dialog;");
            codeBuilder.ComplementaryCode.AddLine("                 var divDinamica = dgl[0].id + '_din';");
            codeBuilder.ComplementaryCode.AddLine("                 if ($('#' + divDinamica).length)");
            codeBuilder.ComplementaryCode.AddLine("                     $('#' + divDinamica).remove();");
            codeBuilder.ComplementaryCode.AddLine("");
            codeBuilder.ComplementaryCode.AddLine("                var dataView = $('#" + idElement + "').data('igGrid').dataSource;");
            codeBuilder.ComplementaryCode.AddLine("                if (dataView.settings.filtering.expressions.length <= 0)");
            codeBuilder.ComplementaryCode.AddLine("                    dataView._filteredData = [];");
            codeBuilder.ComplementaryCode.AddLine("");
            //codeBuilder.ComplementaryCode.AddLine("                var listGrid = '';");
            codeBuilder.ComplementaryCode.AddLine("                var col = ui.owner._dialogCurrentColumn;");
            codeBuilder.ComplementaryCode.AddLine("");
            //codeBuilder.ComplementaryCode.AddLine("                var reloadList = function (col) {");
            //codeBuilder.ComplementaryCode.AddLine("                     var grid = $('#" + idElement + "');");
            //codeBuilder.ComplementaryCode.AddLine("                     var dataView = grid.data('igGrid').dataSource;");
            //codeBuilder.ComplementaryCode.AddLine("                     listGrid = '<span>Propriedade: <b>' + col + '</b></span>';");
            //codeBuilder.ComplementaryCode.AddLine("                     for (var i = 0; i < dataView._data.length; i++) {");
            //codeBuilder.ComplementaryCode.AddLine("                         var isChecked = '';");
            //codeBuilder.ComplementaryCode.AddLine("                         var rowId = dataView._data[i]['RowDataId'];");
            //codeBuilder.ComplementaryCode.AddLine("");
            //codeBuilder.ComplementaryCode.AddLine("                         if (dataView._filteredData != undefined && dataView._filteredData.length >= 1) {");
            //codeBuilder.ComplementaryCode.AddLine("                             isChecked = $.grep(dataView._filteredData, function (e) {");
            //codeBuilder.ComplementaryCode.AddLine("                                 return e.RowDataId == dataView._data[i]['RowDataId'];");
            //codeBuilder.ComplementaryCode.AddLine("                             });");
            //codeBuilder.ComplementaryCode.AddLine("                         }");
            //codeBuilder.ComplementaryCode.AddLine("");
            //codeBuilder.ComplementaryCode.AddLine("                         isChecked = isChecked.length ? 'checked' : '';");
            //codeBuilder.ComplementaryCode.AddLine("                         listGrid += '<div style=\"white-space: nowrap;\"><input type=\"checkbox\" ' + isChecked + ' style=\"position:static;opacity:1;height:17px !important;\"';");
            //codeBuilder.ComplementaryCode.AddLine("                         listGrid += 'onclick = \"selRow(this)\" value= ' + rowId + ' id= ' + rowId + ' name= ' + rowId + ' />';");
            //codeBuilder.ComplementaryCode.AddLine("");
            //codeBuilder.ComplementaryCode.AddLine("                         if (grid.igGrid('columnByKey', col).dataType == 'date') {");
            //codeBuilder.ComplementaryCode.AddLine("                             if (dataView._data[i][col] != '') {");
            //codeBuilder.ComplementaryCode.AddLine("                                 if (dataView._data[i][col] == null)");
            //codeBuilder.ComplementaryCode.AddLine("                                     listGrid += '<span>01/01/1990</span> ';");
            //codeBuilder.ComplementaryCode.AddLine("                                 else");
            //codeBuilder.ComplementaryCode.AddLine("                                     listGrid += '<span>' + Globalize.format(getUTCDate(dataView._data[i][col]), 'd') + '</span> ';");
            //codeBuilder.ComplementaryCode.AddLine("                             }");
            //codeBuilder.ComplementaryCode.AddLine("                         }");
            //codeBuilder.ComplementaryCode.AddLine("                         else");
            //codeBuilder.ComplementaryCode.AddLine("                             listGrid += '<span>' + dataView._data[i][col] + '</span> ';");
            //codeBuilder.ComplementaryCode.AddLine("");
            //codeBuilder.ComplementaryCode.AddLine("                         listGrid += '</div>';");
            //codeBuilder.ComplementaryCode.AddLine("                     }");
            //codeBuilder.ComplementaryCode.AddLine("");
            //codeBuilder.ComplementaryCode.AddLine("                     return listGrid;");
            //codeBuilder.ComplementaryCode.AddLine("                };");
            //codeBuilder.ComplementaryCode.AddLine("");
            //codeBuilder.ComplementaryCode.AddLine("                reloadList(col);");
            codeBuilder.ComplementaryCode.AddLine("                var divDialog = $('#' + dgl[0].id).find('.ui-iggrid-filterdialogaddcondition').find('span')[0];");
            codeBuilder.ComplementaryCode.AddLine("");
            codeBuilder.ComplementaryCode.AddLine("                var scriptHtml = '<div id=\"' + divDinamica + '\">';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '  <script>';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '    var newCol = \"' + col + '\";';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '    var newGrid = $(\"#" + idElement + "\");';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '    var listFilter = [];';");
            //codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '    var reloadList = ' + reloadList + ';';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '    function hideColumn(){';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '     if ($(\"#showHideColumn_" + idElement + "\")[\"0\"].innerHTML.indexOf(\"Ocultar\") >= 0) {';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '        $(\"#showHideColumn_" + idElement + "\")[\"0\"].innerHTML = \"Mostrar Coluna\";';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '        newGrid.igGridHiding(\"hideColumn\", newCol);';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '     }';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '     else{';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '        $(\"#showHideColumn_" + idElement + "\")[\"0\"].innerHTML = \"Ocultar Coluna\";';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '        newGrid.igGridHiding(\"showColumn\", newCol);';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '     }';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '    }';");

            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '    function updateHideButton(){';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '         var column = $.grep(newGrid.igGrid(\"option\", \"columns\"), function (element, index) { return element.key == newCol });';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '         if (column.length > 0){';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '             $(\"#showHideColumn_" + idElement + "\")[\"0\"].innerHTML = column[0].hidden ? \"Mostrar Coluna\" : \"Ocultar Coluna\"';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '         }';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '    }';");

            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '    function orderColumn(dir){';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '      if(dir == 1){dir = \"asc\"} else{dir = \"desc\"}';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '      if(newGrid.data(\"igGrid\").dataSource._filteredData.length <= 0)';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '         newGrid.data(\"igGrid\").dataSource._filter = false;';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '      newGrid.igGridSorting(\"sortColumn\", newCol, dir);';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '    }';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '    function selRow(row){';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '      var list = newGrid.data(\"igGrid\").dataSource;';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '      var filterFormated = [];';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '      if(row.checked){';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '         for (var i = 0; i < list._data.length; i++) {';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '                 if (list._data[i][\"RowDataId\"] == row.value){';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '                     if(list._filteredData != undefined && list.settings.filtering.expressions.length){';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '                         list._filteredData.push(list._data[i]);';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '                         listFilter = list._filteredData;';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '                         for (var p = 0; p < listFilter.length; p++) {';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '                             var value = listFilter[p][\"RowDataId\"];';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '                                  filterFormated.push({fieldName: \"RowDataId\", expr: parseInt(value) , cond: \"equals\", logic: \"OR\"});';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '                         }';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '                         newGrid.igGridFiltering(\"filter\", filterFormated);';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '                     }';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '                     else{';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '                         newGrid.igGridFiltering(\"filter\", ([{fieldName: \"RowDataId\", expr: parseInt(row.value), cond: \"equals\", logic: \"OR\"}]));';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '                     }';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '                     break;';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '                 }';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '             }';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '      }';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '      else {';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '         listFilter = newGrid.data(\"igGrid\").dataSource._filteredData;';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '         for (var i = 0; i < listFilter.length; i++) {';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '                 if (listFilter[i][\"RowDataId\"] == row.value){';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '                     listFilter.splice(i, 1);';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '                     for (var p = 0; p < listFilter.length; p++) {';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '                         var value = listFilter[p][\"RowDataId\"];';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '                         filterFormated.push({fieldName: \"RowDataId\", expr: parseInt(value) , cond: \"equals\", logic: \"OR\"});';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '                     }';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '                     newGrid.igGridFiltering(\"filter\", filterFormated);';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '                     break;';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '                 }';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '             }';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '      }';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '    }';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '  </script>';");

            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '  <div class=\"col-lg-12 col-md-12 col-sm-12 col-xs-12\">';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '     <div class=\"col-lg-6 col-md-6 col-sm-6 col-xs-6\">';");

            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '          <div  style=\"margin-left: 5px\" >';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '              <div>Propriedade:</div>';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '              <div id=\"comboFields_" + idElement + "\"></div>';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '              <script>';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '                  var columns = newGrid.igGrid(\"option\", \"columns\");';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '                  $(\"#comboFields_" + idElement + "\").igCombo({ dataSource: columns, mode : \"dropdown\", valueKey: \"key\", textKey: \"headerText\", selectionChanging: function (evt, ui) {';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '                      newCol = ui.items[\"0\"].data.key;';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '                      updateHideButton()';");
            //codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '                      var newList = reloadList(newCol);';");
            //codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '                      $(\"#" + idElement + "_container_dialog_list\").html(newList)';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '                  }});';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '                  $(\"#comboFields_" + idElement + "\").igCombo(\"value\", newCol);';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '              </script>';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '          </div>';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '      </div>';");

            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '     <div class=\"col-lg-6 col-md-6 col-sm-6 col-xs-6\">';");

            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '          <div style=\"margin-left: 5px; margin-top: 5px\">';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '              <i class=\"fa fa-sort-alpha-asc\" aria-hidden=\"true\" style=\"margin-right: 5px;\"></i><a onclick=\"orderColumn(1)\" style=\"cursor: pointer\">Ordem Crescente</a>';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '          </div>';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '          <div style=\"margin-left: 5px; margin-top: 5px\">';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '              <i class=\"fa fa-sort-alpha-desc\" aria-hidden=\"true\" style=\"margin-right: 5px;\"></i><a onclick=\"orderColumn(2)\" style=\"cursor: pointer\">Ordem Decrescente</a>';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '          </div>';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '          <div style=\"margin-left: 5px; margin-top: 5px\">';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '              <i class=\"fa fa-eye-slash\" aria-hidden=\"true\" style=\"margin-right: 5px;\"></i><a onclick=\"hideColumn()\" style=\"cursor: pointer\" id=\"showHideColumn_" + idElement + "\">Ocultar Coluna</a>';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '          </div>';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '          <br>';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '      </div>';");
            codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '  </div>';");

            //codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '  <div style=\"overflow: auto; max-height:100px\" id=\"' + dgl[0].id + \"_list\" + '\">';");
            //codeBuilder.ComplementaryCode.AddLine("                scriptHtml += listGrid;");
            //codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '  </div>';");
            //codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '  <hr/>';");
            //codeBuilder.ComplementaryCode.AddLine("                scriptHtml += '</div>';");

            codeBuilder.ComplementaryCode.AddLine("");
            codeBuilder.ComplementaryCode.AddLine("                $(scriptHtml).insertBefore(divDialog);");
            codeBuilder.ComplementaryCode.AddLine("           },");
            codeBuilder.ComplementaryCode.AddLine("     },");
            codeBuilder.ComplementaryCode.AddLine("            { name: 'Selection', mode: 'row'" + (container.EnableMultiSelection ? ", multipleSelection: vm.allowMultiSelectionInSearch()" : ""));
            codeBuilder.ComplementaryCode.AddLine("            }, ");

            if (container.EnableMultiSelection)
            {

                string hasWithVirtualization = string.Empty;
                if (container.Virtualization)
                    hasWithVirtualization = "rowSelectorColumnWidth: 30,";

                codeBuilder.ComplementaryCode.AddLine("            { name: 'RowSelectors', enableCheckBoxes: vm.allowMultiSelectionInSearch(), enableRowNumbering: false, " + hasWithVirtualization + " checkBoxStateChanged: function(evt, ui){ ");
                codeBuilder.ComplementaryCode.AddLine("               if ((typeof vm.OnDataGridRowChecked === 'function')){");
                codeBuilder.ComplementaryCode.AddLine("                   vm.OnDataGridRowChecked('" + idElement + "', self.selectedItems());");
                codeBuilder.ComplementaryCode.AddLine("               }");
                codeBuilder.ComplementaryCode.AddLine("               var selectedRows = grid.igGridSelection('selectedRows');");
                codeBuilder.ComplementaryCode.AddLine("               var selectedRow = ui.owner.grid.selectedRow();");
                codeBuilder.ComplementaryCode.AddLine("               var dataViewLength = ui.grid.dataSource.dataView().length;");
                codeBuilder.ComplementaryCode.AddLine("               if ((selectedRows.length == dataViewLength) || (selectedRow == null && selectedRows.length > 0)){");
                codeBuilder.ComplementaryCode.AddLine("                   rowId = [];");
                codeBuilder.ComplementaryCode.AddLine("                   rowId['id'] = 1;");
                codeBuilder.ComplementaryCode.AddLine("                   selectGridCurrentItem(vm.goToKey, 'RowDataId', rowId" + (binding.IsNullOrEmpty() ? "" : ", " + currentBinding + ", getDataSource()") + ");");
                codeBuilder.ComplementaryCode.AddLine("               } else if(ui.owner.grid.selectedRow() != null)");
                codeBuilder.ComplementaryCode.AddLine("                   selectGridCurrentItem(vm.goToKey, 'RowDataId', ui" + (binding.IsNullOrEmpty() ? "" : ", " + currentBinding + ", getDataSource()") + ");");
                codeBuilder.ComplementaryCode.AddLine("                }, ");
                codeBuilder.ComplementaryCode.AddLine("            },");
            }
            codeBuilder.ComplementaryCode.AddLine("            { name: 'Tooltips', columnSettings:[" + gridColumnSettingsTooltips + "] },");

            codeBuilder.ComplementaryCode.AddLine("            { name: 'Resizing' }, ");
            codeBuilder.ComplementaryCode.AddLine("            { name: 'Hiding', ");
            //codeBuilder.ComplementaryCode.AddLine("                columnHidden: function (evt, ui) {");
            //codeBuilder.ComplementaryCode.AddLine("                   showMultimidiaLazy('#" + idElement + "');");
            //codeBuilder.ComplementaryCode.AddLine("                },");
            //codeBuilder.ComplementaryCode.AddLine("                columnShown: function (evt, ui) {");
            //codeBuilder.ComplementaryCode.AddLine("                   showMultimidiaLazy('#" + idElement + "');");
            //codeBuilder.ComplementaryCode.AddLine("                }");
            codeBuilder.ComplementaryCode.AddLine("            },");
            codeBuilder.ComplementaryCode.AddLine("            { name: 'MultiColumnHeaders' }");
            if (container.HasColumnFixing)
                codeBuilder.ComplementaryCode.AddLine("            ,{ name: 'ColumnFixing' }");
            else
                codeBuilder.ComplementaryCode.AddLine("            ,{ name: 'ColumnMoving' }");
            codeBuilder.ComplementaryCode.AddLine((container.HasGroupBy ? "           ,{ name: 'GroupBy', emptyGroupByAreaContent: 'Arraste para esta área a(s) coluna(s) que deseja agrupar.', initialExpand: false" + GetGroupByColumnSettings(container.GroupByColumns, controls) + ", groupedColumnsChanged: function (evt, ui) { $('#" + idElement + "_groupbyarea').toggleClass('is-grouped', (ui.groupedColumns.length > 0)); } }" : ""));
            codeBuilder.ComplementaryCode.AddLine((hasSummaries ? "           ,{ name: 'Summaries', " + GetSumariesColumnSettings(controls, idElement) + " }" : String.Empty));
            codeBuilder.ComplementaryCode.AddLine("           ,{ name: 'Updating', horizontalMoveOnEnter: true,");

            codeBuilder.ComplementaryCode.AddLine("              enableDataDirtyException: false, ");
            codeBuilder.ComplementaryCode.AddLine("              generatePrimaryKeyValue: function(evt, ui){  },");
            codeBuilder.ComplementaryCode.AddLine("              enableDeleteRow: false,");
            codeBuilder.ComplementaryCode.AddLine("              enableAddRow: false,");
            codeBuilder.ComplementaryCode.AddLine("              startEditTriggers: 'click',");
            codeBuilder.ComplementaryCode.AddLine("              editMode:" + (container.EditionOnlyTemplate == true ? "'none'" : "'cell'") + ", /*cell(atual) ou rowedittemplate(template)*/");
            codeBuilder.ComplementaryCode.AddLine("              rowEditDialogContainment: 'window',");
            codeBuilder.ComplementaryCode.AddLine("              showReadonlyEditors: false,");
            codeBuilder.ComplementaryCode.AddLine("              showDoneCancelButtons: false,");

            if (!container.EditionOnlyTemplate)
            {
                //ColumnSettings
                string comboColumnSetting = GetComboColumnSettings(controls, currentBinding, idElement);
                string columnSettings = comboColumnSetting + (comboColumnSetting.IsNullOrEmpty() ? "" : (gridColumnSettings.IsNullOrEmpty() ? "" : ", ")) + gridColumnSettings;
                string datePickerColumnSetting = GetDatePickerColumnSettings(controls, idElement);
                columnSettings = datePickerColumnSetting + (datePickerColumnSetting.IsNullOrEmpty() ? "" : (columnSettings.IsNullOrEmpty() ? "" : ", ")) + columnSettings;
                string lookUpColumnSetting = GetLookupColumnSettings(controls, idElement);
                columnSettings = lookUpColumnSetting + (lookUpColumnSetting.IsNullOrEmpty() ? "" : (columnSettings.IsNullOrEmpty() ? "" : ", ")) + columnSettings;

                if (!columnSettings.IsNullOrEmpty())
                {
                    codeBuilder.ComplementaryCode.AddLine("              columnSettings: [" + columnSettings + "],");
                }
                codeBuilder.ComplementaryCode.AddLine("              rowDeleting: function (evt, ui) {");
                codeBuilder.ComplementaryCode.AddLine("                  deletedIndex = ui.element.context.rowIndex;");
                codeBuilder.ComplementaryCode.AddLine("                  var entity = findElementByKey(getDataSource(), '" + primaryKey + "', ui.rowID);");
                codeBuilder.ComplementaryCode.AddLine("                  if (entity) {");
                codeBuilder.ComplementaryCode.AddLine("                      vm.deleteEntity(entity);");
                codeBuilder.ComplementaryCode.AddLine("                  }");
                codeBuilder.ComplementaryCode.AddLine("              },");
                codeBuilder.ComplementaryCode.AddLine("              rowDeleted: function (evt, ui) {");
                codeBuilder.ComplementaryCode.AddLine("                  var grid = $('#" + idElement + "');");
                codeBuilder.ComplementaryCode.AddLine("                  var rows = grid.igGrid('allRows');");
                codeBuilder.ComplementaryCode.AddLine("                  if (rows.length > 0)");
                codeBuilder.ComplementaryCode.AddLine("                  {");
                codeBuilder.ComplementaryCode.AddLine("                      if (deletedIndex < 0) deletedIndex = 0;");
                codeBuilder.ComplementaryCode.AddLine("                      else if (rows.length <= deletedIndex) deletedIndex = rows.length - 1;");
                codeBuilder.ComplementaryCode.AddLine("                      grid.igGridSelection('selectRow', deletedIndex);");
                codeBuilder.ComplementaryCode.AddLine("                      grid.igGrid('scrollContainer').scrollTop(grid.igGrid('option', 'avgRowHeight') * deletedIndex);");
                codeBuilder.ComplementaryCode.AddLine("                  }");
                codeBuilder.ComplementaryCode.AddLine("              },");

                codeBuilder.ComplementaryCode.AddLine("              editCellStarting: function(evt, ui) { ");
                codeBuilder.ComplementaryCode.AddLine("                  var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowID);");
                codeBuilder.ComplementaryCode.AddLine("                  var canEditing = false, clear = vm.status() === 'C';");
                codeBuilder.ComplementaryCode.AddLine("                  canEditing = verifyCanEditCol(ui.columnKey, clear, entity);");
                codeBuilder.ComplementaryCode.AddLine("                  grid.igGridSelection('clearSelection');");
                codeBuilder.ComplementaryCode.AddLine("                  grid.igGridSelection('selectRow', ui.owner._rowIndex);");

                codeBuilder.ComplementaryCode.AddLine("                 if (vm.status() === 'Q'){");
                codeBuilder.ComplementaryCode.AddLine("                     var gridCell = ui.owner.grid;");
                codeBuilder.ComplementaryCode.AddLine("                     grid.find('div.borderCell').remove();");
                codeBuilder.ComplementaryCode.AddLine("                     $(gridCell.cellAt(ui.columnIndex - 1, ui.owner._rowIndex)).append(\"<div class='borderCell' style='z-index:100; border: 1px solid #849fd9 !important;'></div>\");");
                codeBuilder.ComplementaryCode.AddLine("                  }");

                codeBuilder.ComplementaryCode.AddLine("                  if (!canEditing && vm.status() !== 'C') {");
                codeBuilder.ComplementaryCode.AddLine("                      var isDesc = grid.igGridSorting('option', 'columnSettings').filter(function (el) {");
                codeBuilder.ComplementaryCode.AddLine("                          var desc = el.currentSortDirection;");
                codeBuilder.ComplementaryCode.AddLine("                          if (desc !== undefined) return desc.indexOf('desc') > -1;");
                codeBuilder.ComplementaryCode.AddLine("                      });");
                codeBuilder.ComplementaryCode.AddLine("                      var canEditingOneField = false;");
                codeBuilder.ComplementaryCode.AddLine("                      var columnsVisible = ui.owner.grid._visibleColumnsArray;");
                codeBuilder.ComplementaryCode.AddLine("                      var rowId = ui.rowID, colId = ui.columnIndex;");
                codeBuilder.ComplementaryCode.AddLine("                      var colIndexVisible = 0;");
                codeBuilder.ComplementaryCode.AddLine("                      for (var i = 0; i < ui.owner.grid._visibleColumnsArray.length; i++) {");
                codeBuilder.ComplementaryCode.AddLine("                          var nameColumn = ui.owner.grid._visibleColumnsArray[i].key;");
                codeBuilder.ComplementaryCode.AddLine("                          canEditingOneField = canEditingOneField === true ? canEditingOneField : verifyCanEditCol(nameColumn, clear, entity);");
                codeBuilder.ComplementaryCode.AddLine("                          if (nameColumn === ui.columnKey) colIndexVisible = i;");
                codeBuilder.ComplementaryCode.AddLine("                      }");
                codeBuilder.ComplementaryCode.AddLine("                      if (canEditingOneField) {");
                codeBuilder.ComplementaryCode.AddLine("                          var indexColumn = colIndexVisible;");
                codeBuilder.ComplementaryCode.AddLine("                          var rowIndex = ui.owner._rowIndex;");
                codeBuilder.ComplementaryCode.AddLine("                          for (; indexColumn < ui.owner.grid._visibleColumnsArray.length;) {");
                codeBuilder.ComplementaryCode.AddLine("                              var colNameVisible = ui.owner.grid._visibleColumnsArray[indexColumn].key;");
                codeBuilder.ComplementaryCode.AddLine("                              canNewEditing = verifyCanEditCol(colNameVisible, clear, entity);");
                codeBuilder.ComplementaryCode.AddLine("                              if (canNewEditing) {");
                codeBuilder.ComplementaryCode.AddLine("                                  if (ui.owner._rowIndex + 1 >= grid.igGrid('rows').length && ui.owner.grid._visibleColumnsArray.length <= indexColumn) rowId = (isDesc.length ? ui.rowID + ui.owner._rowIndex : ui.rowID - ui.owner._rowIndex);");
                codeBuilder.ComplementaryCode.AddLine("                                  grid.igGridSelection('selectRow', rowIndex);");
                codeBuilder.ComplementaryCode.AddLine("                                  grid.igGridUpdating('startEdit', rowId, indexColumn, true);");
                codeBuilder.ComplementaryCode.AddLine("                                  break;");
                codeBuilder.ComplementaryCode.AddLine("                              }");
                codeBuilder.ComplementaryCode.AddLine("                              else {");
                codeBuilder.ComplementaryCode.AddLine("                                  indexColumn++;");
                codeBuilder.ComplementaryCode.AddLine("                                  if (event.toString() === '[object KeyboardEvent]') {");
                codeBuilder.ComplementaryCode.AddLine("                                     if (indexColumn >= ui.owner.grid._visibleColumnsArray.length) {");
                codeBuilder.ComplementaryCode.AddLine("                                         isDesc.length ? rowId-- : rowId++;");
                codeBuilder.ComplementaryCode.AddLine("                                         rowIndex++;");
                codeBuilder.ComplementaryCode.AddLine("                                         grid.igGridSelection('clearSelection');");
                codeBuilder.ComplementaryCode.AddLine("                                         indexColumn = 0;");
                codeBuilder.ComplementaryCode.AddLine("                                     }");
                codeBuilder.ComplementaryCode.AddLine("                                  }");
                codeBuilder.ComplementaryCode.AddLine("                              }");
                codeBuilder.ComplementaryCode.AddLine("                          }");
                codeBuilder.ComplementaryCode.AddLine("                      }");
                codeBuilder.ComplementaryCode.AddLine("                  }");
                controls.Where(c => c.BrandDecimalsControl).Foreach(c =>
                {
                    codeBuilder.ComplementaryCode.AddLine("                  if (canEditing && ui.columnKey === '" + c.Name + "') {");
                    codeBuilder.ComplementaryCode.AddLine("                      var decimals = vm.getDecimalsByData(entity, " + c.GetPrecisionDecimalsInt().ToString() + ");");
                    codeBuilder.ComplementaryCode.AddLine("                      ui.editor.igEditor('option', 'minDecimals', decimals);");
                    codeBuilder.ComplementaryCode.AddLine("                      ui.editor.igEditor('option', 'maxDecimals', decimals);");
                    codeBuilder.ComplementaryCode.AddLine("                  }");
                });

                codeBuilder.ComplementaryCode.AddLine("                  return canEditing;");
                codeBuilder.ComplementaryCode.AddLine("              },");
                codeBuilder.ComplementaryCode.AddLine("              editCellStarted: function(evt, ui){");
                codeBuilder.ComplementaryCode.AddLine("                  var lstRefreshDados = null;");
                codeBuilder.ComplementaryCode.AddLine("                  var columns = $('#" + idElement + "').igGridUpdating('option', 'columnSettings');");
                codeBuilder.ComplementaryCode.AddLine("                  var currentCol = null;");
                codeBuilder.ComplementaryCode.AddLine("                  currentRow = ui.rowID;");
                codeBuilder.ComplementaryCode.AddLine("                  columns.forEach(function (entry, index) {");
                codeBuilder.ComplementaryCode.AddLine("                     if (entry.columnKey === ui.columnKey) currentCol = entry;");
                codeBuilder.ComplementaryCode.AddLine("                     if (currentCol != null) return false;");
                codeBuilder.ComplementaryCode.AddLine("                  });");
                codeBuilder.ComplementaryCode.AddLine("                  if (currentCol != null && currentCol.hasOwnProperty('editorType') && currentCol.editorType === 'combo') {");
                codeBuilder.ComplementaryCode.AddLine("                     var lookUpName = $(ui.editor).igCombo('option', 'inputName');");
                codeBuilder.ComplementaryCode.AddLine("                     if (lookUpName != null) {");
                codeBuilder.ComplementaryCode.AddLine("                         lstRefreshDados = vm.dataCombo.getItems(lookUpName, '');");
                codeBuilder.ComplementaryCode.AddLine("                         if (lstRefreshDados.length === 0)");
                codeBuilder.ComplementaryCode.AddLine("                             vm.dataCombo.fillDataCombos(lookUpName, ui.columnKey, " + currentBinding + "(), function (result) {");
                codeBuilder.ComplementaryCode.AddLine("                                 ui.owner.endEdit(false, false);");
                codeBuilder.ComplementaryCode.AddLine("                                 setTimeout(function () { ui.owner.startEdit(ui.rowID, ui.columnKey, true); }, 100);");
                codeBuilder.ComplementaryCode.AddLine("                             });");
                codeBuilder.ComplementaryCode.AddLine("                         else {");
                codeBuilder.ComplementaryCode.AddLine("                             $(ui.editor).igCombo('option', 'dataSource', lstRefreshDados);");
                codeBuilder.ComplementaryCode.AddLine("                             $(ui.editor).one('igcombodatabound', function () { setTimeout(function () { $(ui.editor).igCombo('openDropDown'); }, 10); });");
                codeBuilder.ComplementaryCode.AddLine("                         }");
                codeBuilder.ComplementaryCode.AddLine("                     }");
                codeBuilder.ComplementaryCode.AddLine("                     $(ui.editor).igCombo('openDropDown');");
                codeBuilder.ComplementaryCode.AddLine("                  }");
                codeBuilder.ComplementaryCode.AddLine("              },");
                codeBuilder.ComplementaryCode.AddLine("              editCellEnded: function(evt, ui) {");
                codeBuilder.ComplementaryCode.AddLine("                  currentRow = ui.rowID;");
                codeBuilder.ComplementaryCode.AddLine("                  updateEntity(ui.columnKey, ui.value, !ui.update);");
                codeBuilder.ComplementaryCode.AddLine("                  currentRow = null;");
                codeBuilder.ComplementaryCode.AddLine("              }");
            }
            codeBuilder.ComplementaryCode.AddLine("            }");
            codeBuilder.ComplementaryCode.AddLine("        ]");

            codeBuilder.ComplementaryCode.AddLine("});");

            codeBuilder.ComplementaryCode.AddLine("if ((typeof vm.OnDataGridCreated === 'function')){");
            codeBuilder.ComplementaryCode.AddLine("    vm.OnDataGridCreated('" + idElement + "');");
            codeBuilder.ComplementaryCode.AddLine("}");

            //treatment for select a row through tab key
            codeBuilder.ComplementaryCode.AddLine("var selectionrowselectionchanged = null, selectedRowId = -1;");
            codeBuilder.ComplementaryCode.AddLine("selectionrowselectionchanged = function (evt, ui) {");
            codeBuilder.ComplementaryCode.AddLine("    if ((ui.owner.grid.selectedRow() && typeof ui.owner.grid.selectedRow().id !== 'undefined') || (ui.owner.grid.selectedRows() && ui.owner.grid.selectedRows().length > 0)) { ");
            if (!container.EnableMultiSelection)
            {
                codeBuilder.ComplementaryCode.AddLine("        if (isNullOrEmpty(ui.owner.selectedRows())|| ui.selectedRows.length <= 1) {");
                codeBuilder.ComplementaryCode.AddLine("            $(document).undelegate('#" + idElement + "', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);");
                codeBuilder.ComplementaryCode.AddLine("            ui.owner.clearSelection();");
                codeBuilder.ComplementaryCode.AddLine("            ui.owner.selectRow(ui.row.index);");
                codeBuilder.ComplementaryCode.AddLine("            if (vm.status() === 'Q'){");
                codeBuilder.ComplementaryCode.AddLine("                var gridCell = ui.owner.grid;");
                codeBuilder.ComplementaryCode.AddLine("                grid.find('div.borderCell').remove();");
                codeBuilder.ComplementaryCode.AddLine("                //$(gridCell.cellAt(-1, ui.owner._rowIndex)).append(\" < div class='borderCell' style='z-index:100; border: 1px solid #849fd9 !important;'></div>\");");
                codeBuilder.ComplementaryCode.AddLine("            }");
                codeBuilder.ComplementaryCode.AddLine("            selectedRowId = ui.row.id;");
                codeBuilder.ComplementaryCode.AddLine("            $(document).delegate ('#" + idElement + "', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);");
                codeBuilder.ComplementaryCode.AddLine("        }");
            }
            codeBuilder.ComplementaryCode.AddLine("        selectGridCurrentItem(vm.goToKey, '" + primaryKey + "', ui" + (binding.IsNullOrEmpty() ? "" : ", " + currentBinding + ", getDataSource()") + "); ");
            codeBuilder.ComplementaryCode.AddLine("     } ");
            if (container.EnableMultiSelection)
                codeBuilder.ComplementaryCode.AddLine("     if ((typeof vm.OnDataGridRowChecked === 'function')){ vm.OnDataGridRowChecked('" + idElement + "', self.selectedItems()); }");
            codeBuilder.ComplementaryCode.AddLine("};");

            codeBuilder.ComplementaryCode.AddLine("$(document).delegate('#" + idElement + "', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);");
            //event focus
            if (!container.EnableMultiSelection)
            {
                codeBuilder.ComplementaryCode.AddLine("$('#" + idElement + " > tbody tr').live('focus', function(evt) {");
                codeBuilder.ComplementaryCode.AddLine("    var grid = $('#" + idElement + "'), row = $(this).closest('tr'), id = parseInt(row.attr('data-id'), 10);");
                codeBuilder.ComplementaryCode.AddLine("    var selectedRows = grid.igGridSelection('option', 'multipleSelection') ? grid.igGridSelection('selectedRows') : [grid.igGridSelection('selectedRow')];");
                codeBuilder.ComplementaryCode.AddLine("    if (selectedRowId === id) return;");
                codeBuilder.ComplementaryCode.AddLine("    selectedRowId = id;");
                codeBuilder.ComplementaryCode.AddLine("    grid.igGridSelection('selectRowById', id);");
                codeBuilder.ComplementaryCode.AddLine("    grid.trigger('iggridselectionrowselectionchanged', {");
                codeBuilder.ComplementaryCode.AddLine("    owner: grid.data('igGridSelection'),");
                codeBuilder.ComplementaryCode.AddLine("        row: {");
                codeBuilder.ComplementaryCode.AddLine("           element: row,");
                codeBuilder.ComplementaryCode.AddLine("           index: row.index(),");
                codeBuilder.ComplementaryCode.AddLine("           id: id");
                codeBuilder.ComplementaryCode.AddLine("        },");
                codeBuilder.ComplementaryCode.AddLine("        selectedRows: selectedRows");
                codeBuilder.ComplementaryCode.AddLine("     });");
                codeBuilder.ComplementaryCode.AddLine("});");
            }

            if (_layOut.IsSecundary)
            {
                codeBuilder.ComplementaryCode.AddLine("grid.delegate('.ui-iggrid-activerow', 'dblclick', function (e) {");
                codeBuilder.ComplementaryCode.AddLine("    if (vm." + this.ViewModelName + "().status() === 'Q') vm." + this.ViewModelName + "().dataToolbar.viewInfo();");
                codeBuilder.ComplementaryCode.AddLine("});");
            }

            codeBuilder.ComplementaryCode.DecreaseIndent();
            codeBuilder.ComplementaryCode.AddLine("}");

            codeBuilder.ComplementaryCode.AddLine("vm.addDataSource({ key: '" + idElement + "', name: '" + (binding.IsNullOrEmpty() ? "dataView" : binding.Right(".")) + "', itemsSource: itemsSource });");

            codeBuilder.ComplementaryCode.DecreaseIndent();
            codeBuilder.ComplementaryCode.AddLine("}");
            #endregion

            codeBuilder.DecreaseIndent();


            //Generate internal data grid
            if (innerDataGrids != null && innerDataGrids.Count > 0)
            {
                foreach (var dg in innerDataGrids)
                {
                    ComposeDataGrid(container, dg.Container, elementClass, codeBuilder, rows, columns, dg.Containers);
                }
            }
        }

        private void ComposeLightDataGrid(LayoutContainer parentContainer, LayoutContainer container, LayoutContainerClass elementClass, Tools.CodeBuilder codeBuilder, int rows, int columns, List<TreeLayoutContainer> innerDataGrids)
        {
            //Remove this code when resolve the auto height problem.
            if (container.GridHeight == GridSizeHeight.Auto)
                container.GridHeight = GridSizeHeight.Large;

            List<LayoutControlV2> controls = new List<LayoutControlV2>();

            Action<LayoutElement> finder = null;
            finder = (element) =>
            {
                if (element is LayoutControlV2) controls.Add((LayoutControlV2)element);
                else
                    if (element is LayoutContainer) ((LayoutContainer)element).Controls.ForEach(finder);
            };
            container.Controls.ForEach(finder);

            controls = controls.OrderBy(c => c.GetDataGridOrder()).ToList();

            var NavigableUIs = container.Controls.Where(e => e.ClassName == "ExternalUI").Select(e => (LayoutContainer)e);

            string idElement = container.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "dGrid");

            codeBuilder.AddLine("<div class=\"light-data-grid-container " + GetColumnSpan(parentContainer, container, false) + (this._layOut.GetLayoutElementsByClass("WizardControl").Count > 0 ? "" : " tab-height-size ") + "\"" + (_layOut.IsSecundary ? "" : " data-bind=\"visible: $root." + this.ViewModelName + "().getLayoutVisible('" + idElement + "') \"") + " >");

            if (controls.Count == 0)
                return;

            string controlBindingPath = controls.First().BindingPath;

            string binding = GetBindingPath(controlBindingPath, true), propertyKey;
            string dataView = binding.IsNullOrEmpty() ? "" : (binding.Right(".") + "#").Left("List#");
            string currentBinding = GetFullBindingPath(controlBindingPath, false), listBinding = GetFullBindingPath(controlBindingPath, true);
            string rootListBinding = listBinding.Replace("vm", "$root." + this.ViewModelName + "()");
            string rootCurrent = currentBinding.Replace("vm", "$root." + this.ViewModelName + "()");
            #region addNavigableUIsAction
            Action addNavigableUIsAction = () =>
            {
                codeBuilder.AddLine("<div class=\"dropdown\" data-bind=\"visible: $root." + this.ViewModelName + "().status() === 'Q'\">");
                codeBuilder.AddLine("   <a href=\"#\" title=\"Telas Externas\" class=\"btn default background-color-theme\" data-toggle=\"dropdown\" data-hover=\"dropdown\" data-close-others=\"true\" data-placement=\"right\"><i class=\"fa fa-sign-out\"></i></a>");
                codeBuilder.AddLine("   <ul class=\"dropdown-menu extended notification\" >");
                foreach (var extUI in NavigableUIs)
                {
                    if (extUI.UserInterfaceName.IsNullOrEmpty()) throw new ArgumentNullException("The value externalUI [" + extUI.DisplayName + "] - 'User Interface' is Null");
                    string cmd = "      <li><a title=\"{DisplayName}\" data-bind=\"click: function () { if(!hasObjectWithPropertyValues({currentBinding}, '{pSource}', '{pDest}')) { require('durandal/app').showMessage('Nenhum registro foi encontrado!', 'Informação', ['Ok']); return; } var entityFilter = getObjectWithPropertyValues({currentBinding}, '{pSource}', '{pDest}');  entityFilter = vm.openingExternalUIFromGrid('{UIName}', entityFilter); if(entityFilter === 'Error') { return; } vm.common.go('#{UIName}', 'objectQuery=' + entityFilter + '&executeQuery=true') }\"><i class=\"fa fa-chevron-right\"></i>{DisplayName}</a></li>";
                    cmd = cmd.Replace("{DisplayName}", extUI.DisplayName);
                    cmd = cmd.Replace("{currentBinding}", currentBinding + "()");
                    cmd = cmd.Replace("{UIName}", extUI.UserInterfaceName.Left("/").ToLower().Replace(".", "-") + "-" + extUI.UserInterfaceName.ToLower().Right("/"));
                    cmd = cmd.Replace("{pSource}", extUI.ParentFieldsRelation);
                    cmd = cmd.Replace("{pDest}", extUI.DetailFieldsRelation);
                    cmd = cmd.Replace("vm.", "$root." + this.ViewModelName + "().");


                    codeBuilder.AddLine(cmd);
                }
                codeBuilder.AddLine("   </ul>");
                codeBuilder.AddLine("</div>");
            };
            #endregion
            string parentBinding = currentBinding;
            int idx = currentBinding.LastIndexOf('.');
            if (idx >= 0)
                parentBinding = currentBinding.Left(currentBinding.LastIndexOf('.'));

            string dataPrimaryKey = controls.Where(e => e.IsPartOfKey).Select(e => e.BindingPath.Right(".")).FirstOrDefault(), comma = String.Empty, primaryKey = "RowDataId", primaryKeyType = this._layOut.GetPrimaryKeyTypeByEntity(dataView);

            bool hasSummaries = controls.Any(e => !e.AggregationFunction.IsNullOrEmpty() && e.AggregationFunction != "None");
            codeBuilder.IncreaseIndent();
            #region Grid Buttons
            if (!binding.IsNullOrEmpty() && !container.RemoveDataToolbar)
            {
                codeBuilder.AddLine("<div id=\"" + idElement + "_ContentDLG\" style=\"position:relative;\"></div>");
                codeBuilder.AddLine("<div class=\"linx-table-button position-icon\">");
                codeBuilder.IncreaseIndent();

                codeBuilder.AddLine("<div class=\"botoes-tabela\">");
                codeBuilder.AddLine(GetKoBindingDivs(controlBindingPath, false, false));

                if ((this._layOut.CanEdit || this._layOut.CanAddNew) && container.CanAddNew)
                {
                    codeBuilder.AddLine("<button id=\"" + idElement + "_AddBtn\" class=\"btn default background-color-theme\" data-placement=\"top\" title=\"Novo Registro\" data-bind=\"visible: $root." + this.ViewModelName + "().status() === 'E' && !isEmptyEntityFn($data), click: function () { ");
                    codeBuilder.AddLine("   $root." + this.ViewModelName + "().createAndNotify" + dataView + "($data); ");
                    if (!container.EnableMultiSelection)
                        codeBuilder.AddLine("   setTimeout(function(){ $root." + this.ViewModelName + "().openEditor('#" + idElement + "', '" + container.Name + "', '" + currentBinding + ";" + listBinding + "', '" + dataView + ";" + parentBinding + "', '" + (dataView.IsNullOrEmpty() ? EntityName + "()" : dataView + '(' + parentBinding + ')') + "', false); }, 1000);");
                    codeBuilder.AddLine("}\"><i class=\"fa fa-plus\"></i></button>");
                }


                if (this._layOut.CanEdit && container.CanDelete)
                {
                    codeBuilder.AddLine("<button id=\"" + idElement + "_DelBtn\" class=\"btn default background-color-theme\" data-placement=\"top\" title=\"Excluir Registro\" ");
                    codeBuilder.AddLine("data-bind=\"visible: $root." + this.ViewModelName + "().status() === 'E', ");
                    codeBuilder.AddLine("click: function () {");
                    codeBuilder.AddLine("   $root." + this.ViewModelName + "().deleteGrid('#" + idElement + "', '" + container.Name + "', '" + currentBinding + ";" + listBinding + "', " + container.EnableMultiSelection.ToString().ToLower() + ");");
                    codeBuilder.AddLine("}\"> ");
                    codeBuilder.AddLine("<i class=\"fa fa-trash-o\"></i></button>");
                }

                AddButtonsConfigAndEditor(container, codeBuilder, idElement, dataView, currentBinding, listBinding, parentBinding, (dataView.IsNullOrEmpty() ? EntityName + "()" : dataView + '(' + parentBinding + ')'), rootListBinding, true);

                if (container.CanExportGrid)
                {
                    codeBuilder.AddLine("<button id=\"" + idElement + "_ExpExcelBtn\" class=\"btn default background-color-theme\" data-placement=\"top\" title=\"Exportar para Excel\" data-bind=\"visible: $root." + this.ViewModelName + "().dataToolbar.canGridExport(), click: function () {$root." + this.ViewModelName + "().exportDataDetails($data, '" + dataView + "', true);  }\"><i class=\"fa fa-file-excel-o\"></i></button>");
                }

                if (NavigableUIs.Any())
                    addNavigableUIsAction();

                codeBuilder.AddLine(GetKoBindingDivs(controlBindingPath, false, true));
                codeBuilder.AddLine("</div>");

                codeBuilder.DecreaseIndent();
                codeBuilder.AddLine("</div>");
            }
            else if (binding.IsNullOrEmpty() && !container.RemoveDataToolbar)
            {
                codeBuilder.AddLine("<div id=\"" + idElement + "_ContentDLG\" style=\"position:relative;\"></div>");
                codeBuilder.AddLine("<div class=\"linx-table-button position-icon\">");
                codeBuilder.IncreaseIndent();

                codeBuilder.AddLine("<div class=\"botoes-tabela\" >");

                if (container.CanAddNew)
                    codeBuilder.AddLine("<button id=\"" + idElement + "_AddBtn\" class=\"btn default background-color-theme\" data-placement=\"top\" title=\"Novo Registro\" data-bind=\"visible: $root." + this.ViewModelName + "().hideToolbar() && $root." + this.ViewModelName + "().status() === 'E', click: function () { $root." + this.ViewModelName + "().dataToolbar.addNew(); }\"><i class=\"fa fa-plus\"></i></button>");

                if (container.CanDelete)
                {
                    codeBuilder.AddLine("<button id=\"" + idElement + "_DelBtn\" class=\"btn default background-color-theme\" data-placement=\"top\" title=\"Excluir Registro\" ");
                    codeBuilder.AddLine("data-bind=\"visible: $root." + this.ViewModelName + "().status() === 'E', ");
                    codeBuilder.AddLine("click: function () { ");
                    codeBuilder.AddLine("   $root." + this.ViewModelName + "().deleteGrid('#" + idElement + "', '" + container.Name + "', '" + currentBinding + ";" + listBinding + "', " + container.EnableMultiSelection.ToString().ToLower() + ");");
                    codeBuilder.AddLine("}\"> ");
                    codeBuilder.AddLine("<i class=\"fa fa-trash-o\"></i></button>");
                }

                AddButtonsConfigAndEditor(container, codeBuilder, idElement, dataView, currentBinding, listBinding, parentBinding, (dataView.IsNullOrEmpty() ? EntityName + "()" : dataView + '(' + parentBinding + ')'), rootListBinding, true);

                if (container.CanExportGrid)
                {
                    codeBuilder.AddLine("<button id=\"" + idElement + "_ExpExcelBtn\" class=\"btn default background-color-theme\" data-placement=\"top\" title=\"Exportar para Excel\" data-bind=\"visible: $root." + this.ViewModelName + "().dataToolbar.canExport(), click: function () {$root." + this.ViewModelName + "().dataToolbar.exportData(false, true);  }\"><i class=\"fa fa-file-excel-o\"></i></button>");
                }
                if (NavigableUIs.Any())
                    addNavigableUIsAction();


                codeBuilder.AddLine("</div>");

                codeBuilder.DecreaseIndent();
                codeBuilder.AddLine("</div>");
            }
            else if (!container.RemoveDataToolbar)
            {
                codeBuilder.AddLine("<div id=\"" + idElement + "_ContentDLG\" style=\"position:relative;\"></div>");
                codeBuilder.AddLine("<div class=\"linx-table-button position-icon\">");
                codeBuilder.IncreaseIndent();

                codeBuilder.AddLine("<div class=\"botoes-tabela\"  >");
                AddButtonsConfigAndEditor(container, codeBuilder, idElement, dataView, currentBinding, listBinding, parentBinding, (dataView.IsNullOrEmpty() ? EntityName + "()" : dataView + '(' + parentBinding + ')'), rootListBinding, true);
                if (this._layOut.CanEdit && container.CanDelete)
                {
                    codeBuilder.AddLine("<button id=\"" + idElement + "_DelBtn\" class=\"btn default background-color-theme\" data-placement=\"top\" title=\"Excluir Registro\" ");
                    codeBuilder.AddLine("data-bind=\"visible: $root." + this.ViewModelName + "().dataToolbar.canUndo, ");
                    codeBuilder.AddLine("click: function () {");
                    codeBuilder.AddLine("   $root." + this.ViewModelName + "().deleteGrid('#" + idElement + "', '" + container.Name + "', '" + currentBinding + ";" + listBinding + "', " + container.EnableMultiSelection.ToString().ToLower() + ");");
                    codeBuilder.AddLine("}\"> ");
                    codeBuilder.AddLine("<i class=\"fa fa-trash-o\"></i></button>");
                }

                codeBuilder.AddLine("</div>");

                codeBuilder.DecreaseIndent();
                codeBuilder.AddLine("</div>");
            }
            #endregion
            codeBuilder.AddLine("<div class=\"linx-table-grid position-table-grid\"" + (((_layOut.Containers.Count == 1 && _layOut.Containers[0] == container) || (_layOut.Containers.Count == 2 && _layOut.Containers[0] == container && _layOut.Containers[1].IsTemplate)) ? " style=\"height: inherit;\"" : "") + ">");
            codeBuilder.IncreaseIndent();

            if ((_layOut.Containers.Count == 1 && _layOut.Containers[0] == container) || (_layOut.Containers.Count == 2 && _layOut.Containers[0] == container && _layOut.Containers[1].IsTemplate))
                codeBuilder.AddLine("<div class=\"screen-view\">");
            if (container.EnableFilterTextInGrid)
            {
                codeBuilder.AddLine("   <div class=\"row\">");
                codeBuilder.AddLine("       <div class=\"col-md-12\">");
                codeBuilder.AddLine("           <div class=\"lightGrid\">");
                codeBuilder.AddLine("               <i class=\"fa fa-search lightGrid__faSearch\"></i>");
                codeBuilder.AddLine("               <input id=\"" + idElement + "_finder\" class=\"lightGrid__search\" placeholder=\"" + "Filtre aqui".Translate() + "\" />");
                codeBuilder.AddLine("           </div>");
                codeBuilder.AddLine("       </div>");
                codeBuilder.AddLine("   </div>");
            }
            codeBuilder.AddLine("   <table id=\"" + idElement + "\" class=\"light-data-grid-table\" data-bind=\"css: { IsEditableStyle: $root." + this.ViewModelName + "().enabledForEditing }\" " + (container.IsVisible ? "" : "class=\"hide\" ") + " />");
            if (_layOut.IsSecundary)
                codeBuilder.AddLine("</div>");
            codeBuilder.DecreaseIndent();
            codeBuilder.AddLine("</div>");
            #region Defining auxiliary code
            codeBuilder.ComplementaryCalls.AddLine("complement.render" + idElement.Replace("-", "").Replace(" ", "") + "(vm);");


            #region Enable multi-selection
            if (container.EnableMultiSelection)
            {
                codeBuilder.ComplementaryCode.AddLine(", vm: null");

                codeBuilder.ComplementaryCode.AddLine(", selectedCollection: { }");
                codeBuilder.ComplementaryCode.AddLine(", currentPage: 0");
                codeBuilder.ComplementaryCode.AddLine(", selectedItems: function(firstIfNoItem) {");
                codeBuilder.ComplementaryCode.AddLine("    var result = [];");
                codeBuilder.ComplementaryCode.AddLine("    complement.saveSelection();");
                codeBuilder.ComplementaryCode.AddLine("    for (var propName in complement.selectedCollection)");
                codeBuilder.ComplementaryCode.AddLine("    {");
                codeBuilder.ComplementaryCode.AddLine("        result = result.concat(complement.selectedCollection[propName]);");
                codeBuilder.ComplementaryCode.AddLine("    }");
                codeBuilder.ComplementaryCode.AddLine("    if (result.length == 0 && firstIfNoItem)");
                codeBuilder.ComplementaryCode.AddLine("        result = complement.selectedCurrentItems(true);");
                codeBuilder.ComplementaryCode.AddLine("    return result;");
                codeBuilder.ComplementaryCode.AddLine("}");
                codeBuilder.ComplementaryCode.AddLine(", saveSelection: function() {");
                codeBuilder.ComplementaryCode.AddLine("    if (complement.vm.status() === 'C') { complement.currentPage = 0; complement.selectedCollection = {}; return; }");
                codeBuilder.ComplementaryCode.AddLine("    var pageProp = " + (binding.IsNullOrEmpty() ? "'Page' + complement.currentPage.toString()" : "'Page0'") + ";");
                codeBuilder.ComplementaryCode.AddLine("    complement.selectedCollection[pageProp] = complement.selectedCurrentItems();");
                codeBuilder.ComplementaryCode.AddLine("    complement.currentPage = complement.vm.dataToolbar.currentPage();");
                codeBuilder.ComplementaryCode.AddLine("}");

                codeBuilder.ComplementaryCode.AddLine(", selectedCurrentItems: function (firstIfNoItem, isSavingData) {");
                codeBuilder.ComplementaryCode.AddLine("      var grid = $('#" + idElement + "');");
                codeBuilder.ComplementaryCode.AddLine("      var selectedItems = [];");
                codeBuilder.ComplementaryCode.AddLine("      var ds = grid.data().igGrid.dataSource.dataView();");
                codeBuilder.ComplementaryCode.AddLine("      var rows = grid.igGridSelection(\"selectedRows\");");
                codeBuilder.ComplementaryCode.AddLine("      if (rows && rows.length == 0 && firstIfNoItem) {");
                codeBuilder.ComplementaryCode.AddLine("          var dataList = this." + listBinding + ";");
                codeBuilder.ComplementaryCode.AddLine("          var entity = (isSavingData ? findElementByKey(dataList, 'RowDataId', ds[0].RowDataId) : ds[0]);");
                codeBuilder.ComplementaryCode.AddLine("          if (entity) selectedItems.push(entity);");
                codeBuilder.ComplementaryCode.AddLine("      }");
                codeBuilder.ComplementaryCode.AddLine("      else if (rows && rows.length > 0) {");
                codeBuilder.ComplementaryCode.AddLine("          var dataList = this." + listBinding + ";");
                codeBuilder.ComplementaryCode.AddLine("          $.each(rows, function (index, value) {");
                codeBuilder.ComplementaryCode.AddLine("              var entity = (isSavingData ? findElementByKey(dataList, 'RowDataId', ds[value.index].RowDataId) : ds[value.index]);");
                codeBuilder.ComplementaryCode.AddLine("              if (entity) selectedItems.push(entity);");
                codeBuilder.ComplementaryCode.AddLine("          });");
                codeBuilder.ComplementaryCode.AddLine("      }");
                codeBuilder.ComplementaryCode.AddLine("      return selectedItems;");
                codeBuilder.ComplementaryCode.AddLine("}");
                codeBuilder.ComplementaryCode.AddLine(", clearSelectedItems: function () {");
                codeBuilder.ComplementaryCode.AddLine("      var grid = $('#" + idElement + "');");
                codeBuilder.ComplementaryCode.AddLine("      grid.igGridSelection('clearSelection');");
                codeBuilder.ComplementaryCode.AddLine("}");
            }
            #endregion
            codeBuilder.ComplementaryCode.AddLine(", render" + idElement.Replace("-", "").Replace(" ", "") + ": function(vm) {");
            codeBuilder.ComplementaryCode.IncreaseIndent();
            if (container.EnableMultiSelection)
            {
                codeBuilder.ComplementaryCode.AddLine("var self = this;");
                codeBuilder.ComplementaryCode.AddLine("self.vm = vm;");
            }
            var controlsKPI = controls.Where(i => i.KpiName != "").ToList();
            List<string> gaugeData = new List<string>();
            for (int i = 0; i < controlsKPI.Count; i++)
            {
                if (controls.Any(c => (c.ClassName.Contains("Gauge") || c.ClassName.Contains("KpiBox")) && c.BindingPath.Right(".").InList(controlsKPI[i].BindingPath.Right("."), controlsKPI[i].BindingPath.Right(".") + "KpiInfo")))
                {
                    string dataDef = "var dadosGauge" + controlsKPI[i].BindingPath.Right(".") + " = vm.get" + controlsKPI[i].KpiName + "GaugeGrid(function(ranges, min, max) { complement.render" + idElement.Replace("-", "").Replace(" ", "") + "(vm);  });";
                    if (!gaugeData.Contains(dataDef))
                    {
                        gaugeData.Add(dataDef);
                        codeBuilder.ComplementaryCode.AddLine(dataDef);
                    }
                }
            }


            if (binding.IsNullOrEmpty() && !_layOut.IsSecundary)
            {
                codeBuilder.ComplementaryCode.AddLine("if (!vm.hasMainTopDataGrid()) vm.hasMainTopDataGrid(true);");
                this.HasMainTopDataGrid = true;
            }
            codeBuilder.ComplementaryCode.AddLine("var source = null;");
            Func<LayoutControlV2, string> getColumnType = (c) =>
            {
                if (c.ClassName.InList("MultimediaControl", "CustomControl"))
                    return "string";
                else
                    return GetPropDataType(c.DataType, c.DomainName);

            };

            var schemas = controls.Select(i => "{ name: '" + i.BindingPath.Right(".") + (i.DomainName.IsNullOrEmpty() ? "" : "Name") + "', type: '" + (i.DomainName.IsNullOrEmpty() ? getColumnType(i) : "string") + "' }").ToList();

            codeBuilder.ComplementaryCode.AddLine("var schema = [{ name: 'RowDataId', type: 'number' }, " + string.Join(", ", schemas) + "];");
            codeBuilder.ComplementaryCode.AddLine("var getDataSource = function() {");
            codeBuilder.ComplementaryCode.AddLine("    try {");
            codeBuilder.ComplementaryCode.AddLine("        source = new $.ig.JSONDataSource({");
            codeBuilder.ComplementaryCode.AddLine("            dataSource: unwrapObservableArray(" + listBinding + ", vm),");
            codeBuilder.ComplementaryCode.AddLine("            schema: { fields: schema },");
            codeBuilder.ComplementaryCode.AddLine("            filtering: { type: 'local'}");
            codeBuilder.ComplementaryCode.AddLine("        }).dataBind();");
            codeBuilder.ComplementaryCode.AddLine("    }");
            codeBuilder.ComplementaryCode.AddLine("    catch (e) { }");
            codeBuilder.ComplementaryCode.AddLine("    return isNullOrEmpty(source) ? ko.observableArray([]) : source;");
            codeBuilder.ComplementaryCode.AddLine("};");

            string parentRecord = listBinding.Left("." + listBinding.Right("."));

            if (parentRecord != "vm")
            {
                codeBuilder.ComplementaryCode.AddLine("var dataSourceIsLoaded = function() {");
                codeBuilder.ComplementaryCode.AddLine("    var isLoaded = false;");
                codeBuilder.ComplementaryCode.AddLine("    try {");
                codeBuilder.ComplementaryCode.AddLine("        isLoaded = (" + parentRecord + "." + dataView + "IsLoaded === true || " + parentRecord + "." + dataView + "List().length > 0);");
                codeBuilder.ComplementaryCode.AddLine("    }");
                codeBuilder.ComplementaryCode.AddLine("    catch (e) {");
                codeBuilder.ComplementaryCode.AddLine("        isLoaded = true;");
                codeBuilder.ComplementaryCode.AddLine("    }");
                codeBuilder.ComplementaryCode.AddLine("    return isLoaded;");
                codeBuilder.ComplementaryCode.AddLine("}");
            }


            codeBuilder.ComplementaryCode.AddLine("var getVisibleColumns = function(metaDataControl) {");
            if (!container.IsLinqSelectionControl)
                codeBuilder.ComplementaryCode.AddLine("   if (metaDataControl) return '';");
            codeBuilder.ComplementaryCode.AddLine("   var visibleColumns = '';");
            codeBuilder.ComplementaryCode.AddLine("   if($('#" + idElement + "').data('igGrid') === undefined) return '';");
            codeBuilder.ComplementaryCode.AddLine("   var cols = $('#" + idElement + "').igGrid('option', 'columns');");
            codeBuilder.ComplementaryCode.AddLine("   if (cols) {");
            codeBuilder.ComplementaryCode.AddLine("     for (var idx = 0; idx < cols.length; idx++) {");
            codeBuilder.ComplementaryCode.AddLine("         if (cols[idx].hidden !== true) visibleColumns += (visibleColumns === '' ? '' : ',') + cols[idx].key;");
            codeBuilder.ComplementaryCode.AddLine("     }");
            codeBuilder.ComplementaryCode.AddLine("   }");
            codeBuilder.ComplementaryCode.AddLine("   return visibleColumns;");
            codeBuilder.ComplementaryCode.AddLine("};");

            //Creating Binding Update
            codeBuilder.ComplementaryCode.AddLine("var started = false;");

            codeBuilder.ComplementaryCode.AddLine("var isElementHided = function (grid, forceCreating) {");
            codeBuilder.ComplementaryCode.AddLine("  if (!grid) grid = $('#" + idElement + "');");
            codeBuilder.ComplementaryCode.AddLine("  return ((!grid[0] || (!forceCreating && grid.parent().width() <= 0)) && !$('#dialog" + container.Name + "').is(':visible'));");
            codeBuilder.ComplementaryCode.AddLine("}");

            codeBuilder.ComplementaryCode.AddLine("var refreshData = true;");
            codeBuilder.ComplementaryCode.AddLine("var itemsSource = { isElementHided: isElementHided, getVisibleColumns: getVisibleColumns, containerId: '" + idElement + "_container', dataBind: function (commitData, forceCreating) {");
            codeBuilder.ComplementaryCode.AddLine("   var grid = $('#" + idElement + "');");
            codeBuilder.ComplementaryCode.AddLine("   if (started && grid.children().length === 0) { started = false; }");
            codeBuilder.ComplementaryCode.AddLine("   if (commitData && started) {");
            codeBuilder.ComplementaryCode.AddLine("       return;");
            codeBuilder.ComplementaryCode.AddLine("   }");

            if (parentRecord != "vm")
            {
                codeBuilder.ComplementaryCode.AddLine("   var execFillDetais = ((vm.status() !== 'C' && vm.status() !== 'I') && !dataSourceIsLoaded());");
                codeBuilder.ComplementaryCode.AddLine("   if (forceCreating && started && !refreshData && !execFillDetais) return;");
            }
            else
                codeBuilder.ComplementaryCode.AddLine("   if (forceCreating && started && !refreshData) return;");

            codeBuilder.ComplementaryCode.AddLine("   var isHided = isElementHided(grid, forceCreating);");
            codeBuilder.ComplementaryCode.AddLine("   refreshData = !forceCreating;");
            codeBuilder.ComplementaryCode.AddLine("   if (refreshData && !isHided) refreshData = false;");

            codeBuilder.ComplementaryCode.AddLine("   if (isHided) return;");

            codeBuilder.ComplementaryCode.AddLine("   if (!started) {");
            codeBuilder.ComplementaryCode.AddLine("       createDataGrid(grid);");
            codeBuilder.ComplementaryCode.AddLine("       started = true;");
            codeBuilder.ComplementaryCode.AddLine("       commitData = false;");
            if (container.GroupByColumns.IsNullOrEmpty())
                codeBuilder.ComplementaryCode.AddLine("       $('#" + idElement + "_groupbyarea').addClass('hide');");
            codeBuilder.ComplementaryCode.AddLine("   }");


            if (parentRecord != "vm")
            {
                codeBuilder.ComplementaryCode.AddLine("   if (execFillDetais) {");
                codeBuilder.ComplementaryCode.AddLine("     grid.igGrid(\"option\", \"dataSource\", []);");
                codeBuilder.ComplementaryCode.AddLine("     " + parentRecord + ".fillDetails(false, '" + dataView + "');");
                codeBuilder.ComplementaryCode.AddLine("     return;");
                codeBuilder.ComplementaryCode.AddLine("   }");
            }

            if (container.EnableMultiSelection)
            {
                codeBuilder.ComplementaryCode.AddLine("   var selectedRows = complement.selectedItems();");
                codeBuilder.ComplementaryCode.AddLine("   grid.igGridSelection('clearSelection');");
            }

            if (binding.IsNullOrEmpty() && container.PageSize <= 0)
                codeBuilder.ComplementaryCode.AddLine("   grid.data('igGridSorting')._shouldFireColumnSorted = false;");

            #region refresh data 
            codeBuilder.ComplementaryCode.AddLine("   grid.data('igGrid')._loadingIndicator.show();");
            codeBuilder.ComplementaryCode.AddLine("   setTimeout(function () {");
            codeBuilder.ComplementaryCode.IncreaseIndent();
            codeBuilder.ComplementaryCode.AddLine("   grid.igGrid(\"option\", \"dataSource\", getDataSource());");

            if (!dataPrimaryKey.IsNullOrEmpty() && container.PageSize > 0 && !_layOut.IsSecundary)
            {
                codeBuilder.ComplementaryCode.AddLine("   if (vm.status() === 'E') {");
                codeBuilder.ComplementaryCode.AddLine("      grid.igGridSorting(\"sortColumn\", \"" + dataPrimaryKey + "\", \"ascending\");");
                codeBuilder.ComplementaryCode.AddLine("   }");
            }
            if (container.PageSize > 0)
            {
                codeBuilder.ComplementaryCode.AddLine("   grid.igGridPaging(\"option\", \"currentPageIndex\", 0);");
            }
            else if (binding.IsNullOrEmpty())
                codeBuilder.ComplementaryCode.AddLine("   grid.data('igGridSorting')._shouldFireColumnSorted = true;");

            codeBuilder.ComplementaryCode.AddLine("   var totalGrid = source.dataView().length;");
            codeBuilder.ComplementaryCode.AddLine("   if (totalGrid > 0) {");
            if (container.EnableMultiSelection)
            {
                codeBuilder.ComplementaryCode.AddLine("     if (selectedRows.length > 0) {");
                codeBuilder.ComplementaryCode.AddLine("         var dataView = grid.data().igGrid.dataSource.dataView();");
                codeBuilder.ComplementaryCode.AddLine("         if (dataView.length > 0) {");
                codeBuilder.ComplementaryCode.AddLine("             $.each(selectedRows, function (index, item) {");
                codeBuilder.ComplementaryCode.AddLine("                var idxFound = findIndexByKey(dataView, '" + primaryKey + "', getAbsoluteValue(item['" + primaryKey + "']))");
                codeBuilder.ComplementaryCode.AddLine("                if (idxFound < 0) idxFound = findIndexByKey(dataView, '" + dataPrimaryKey + "', getAbsoluteValue(item['" + dataPrimaryKey + "']))");
                codeBuilder.ComplementaryCode.AddLine("                if (idxFound >= 0) grid.igGridSelection(\"selectRow\", idxFound);");
                codeBuilder.ComplementaryCode.AddLine("             });");
                codeBuilder.ComplementaryCode.AddLine("         }");
                codeBuilder.ComplementaryCode.AddLine("     }");
            }
            else
            {
                codeBuilder.ComplementaryCode.AddLine("     if (" + currentBinding + "() != null) {");
                codeBuilder.ComplementaryCode.AddLine("         var searchedItem = $.grep(source.dataView(), function (item, i) { return item.RowDataId === getAbsoluteValue(" + currentBinding + "().RowDataId) });");
                codeBuilder.ComplementaryCode.AddLine("         var idx = searchedItem.length === 0 ? 0 : source.dataView().indexOf(searchedItem[0]);");
                codeBuilder.ComplementaryCode.AddLine("         grid.igGridSelection('selectRow', idx);");
                codeBuilder.ComplementaryCode.AddLine("         grid.igGrid('scrollContainer').scrollTop(grid.igGrid('option', 'avgRowHeight') * idx)");
                codeBuilder.ComplementaryCode.AddLine("     } else {");
                codeBuilder.ComplementaryCode.AddLine("         grid.igGridSelection('selectRow', 0);");
                codeBuilder.ComplementaryCode.AddLine("         grid.igGrid('scrollContainer').scrollTop(0);");
                codeBuilder.ComplementaryCode.AddLine("     }");

                if (_layOut.IsSecundary)
                {
                    codeBuilder.ComplementaryCode.AddLine("     $(grid.selector + '_container').focus();");
                }
            }
            codeBuilder.ComplementaryCode.AddLine("     if ($('#dialog" + container.Name + "').is(':visible')) {");
            codeBuilder.ComplementaryCode.AddLine("        var hasPaging = $.grep(grid.igGrid('option', 'features'), function (e) {");
            codeBuilder.ComplementaryCode.AddLine("           return e.name === 'Paging';");
            codeBuilder.ComplementaryCode.AddLine("        });");
            codeBuilder.ComplementaryCode.AddLine("        var totalGrid = grid.data('igGrid').options.dataSource.length;");
            codeBuilder.ComplementaryCode.AddLine("        var current = 1;");
            codeBuilder.ComplementaryCode.AddLine("        if (hasPaging.length > 0) {");
            codeBuilder.ComplementaryCode.AddLine("           var totalCurrentPage = totalGrid;");
            codeBuilder.ComplementaryCode.AddLine("           var currentPage = grid.igGridPaging('pageIndex') + 1;");
            codeBuilder.ComplementaryCode.AddLine("           var pageIndex = grid.igGridPaging('pageIndex');");
            codeBuilder.ComplementaryCode.AddLine("           var pageSize = grid.igGridPaging('pageSize');");
            codeBuilder.ComplementaryCode.AddLine("           if (totalGrid / pageSize > currentPage) totalCurrentPage = (1 * grid.igGrid('rows').length);");
            codeBuilder.ComplementaryCode.AddLine("           if (currentPage > 1) current = (pageIndex * pageSize) + current;");
            codeBuilder.ComplementaryCode.AddLine("           $('label#currentNumber" + container.Name + "').html(current + ' - ' + totalCurrentPage);");
            codeBuilder.ComplementaryCode.AddLine("        }");
            codeBuilder.ComplementaryCode.AddLine("        else");
            codeBuilder.ComplementaryCode.AddLine("           $('label#currentNumber" + container.Name + "').html(1);");
            codeBuilder.ComplementaryCode.AddLine("        $('label#totalNumber" + container.Name + "').html(totalGrid);");
            codeBuilder.ComplementaryCode.AddLine("    }");

            codeBuilder.ComplementaryCode.AddLine("   } else {");
            codeBuilder.ComplementaryCode.AddLine("       $('label#currentNumber" + container.Name + "').html(0);");
            codeBuilder.ComplementaryCode.AddLine("       $('label#totalNumber" + container.Name + "').html(0);");
            codeBuilder.ComplementaryCode.AddLine("   }");
            codeBuilder.ComplementaryCode.AddLine("   grid.data('igGrid')._loadingIndicator.hide();");
            codeBuilder.ComplementaryCode.DecreaseIndent();
            codeBuilder.ComplementaryCode.AddLine("   }, 10);");
            #endregion
            codeBuilder.ComplementaryCode.AddLine("}};");

            codeBuilder.ComplementaryCode.AddLine("var valueGrouBy = -1;");
            codeBuilder.ComplementaryCode.AddLine("var deletedIndex = -1;");

            if (controlsKPI.Count > 0)
            {
                codeBuilder.ComplementaryCode.AddLine("function makeGauge(val, record, field, solid, sufix) {");
                codeBuilder.ComplementaryCode.AddLine("    var row = 0, value = 0;");
                codeBuilder.ComplementaryCode.AddLine("    if (record.RowDataId > 0) {");
                codeBuilder.ComplementaryCode.AddLine("        row = record.RowDataId;");
                codeBuilder.ComplementaryCode.AddLine("        value = record[field];");
                codeBuilder.ComplementaryCode.AddLine("        if (solid) {");
                codeBuilder.ComplementaryCode.AddLine("             var descValue = record[field + (isNullOrEmpty(sufix) ? \"\" : sufix)];");
                codeBuilder.ComplementaryCode.AddLine("             return \"<div id='c\" + row + field + sufix + \"' style='color:black;text-align:\" + (isNullOrEmpty(sufix) ? \"right\" : \"center\") + \";background-color:\" + vm.getKpiColor(eval(eval(\"dadosGauge\" + field).ranges), value) + \";'><strong\" + (isNullOrEmpty(sufix) ? \" style='margin-right: 5px;'\" : \"\") + \">\" + descValue + \"</strong></div>\";");
                codeBuilder.ComplementaryCode.AddLine("        }");
                codeBuilder.ComplementaryCode.AddLine("        else");
                codeBuilder.ComplementaryCode.AddLine("             return \"<div id='g\" + row + field + \"' class='gauge' style='width:400px;height:20px;'></div> <script id='scriptg\" + row + field + \"'>$('#g\" + row + field + \"').kendoLinearGauge( {gaugeArea: {background: 'transparent', width:230}, pointer: { value: \" + value + \", color: '#8B8386', shape: 'arrow' }, scale: { vertical: false ,line:{visible: false}, labels: {visible: false}, min: \" + eval(\"dadosGauge\" + field).min + \", max: \" + eval(\"dadosGauge\" + field).max + \", ranges: \" + eval(\"dadosGauge\" + field).ranges + \" } });</script>\"");
                codeBuilder.ComplementaryCode.AddLine("    }");
                codeBuilder.ComplementaryCode.AddLine("    return '';");
                codeBuilder.ComplementaryCode.AddLine("}");
            }
            codeBuilder.ComplementaryCode.AddLine("function createDataGrid(grid) {");
            codeBuilder.ComplementaryCode.IncreaseIndent();

            codeBuilder.ComplementaryCode.AddLine("var gridId = grid[0].id;");


            //Create Grid View               
            var gridHeight = "";
            int avgRowHeight = 27;
            if (container.GridVisibleRowsNumber == 0)
                gridHeight = (((_layOut.Containers.Count == 1 && _layOut.Containers[0] == container) || (_layOut.Containers.Count == 2 && _layOut.Containers[0] == container && _layOut.Containers[1].IsTemplate)) ? "(vm.isDependentVM() ? getGridHeightSuggested() * 0.7 : $(window).height() * 0.85)" : GetGridHeight(container));
            else
            {
                var _height = container.GridVisibleRowsNumber * (avgRowHeight + 1);
                gridHeight = (_height < minGridHeight ? minGridHeight : _height).ToString();
            }

            codeBuilder.ComplementaryCode.AddLine("grid.igGrid({ height: " + gridHeight + "+'px', width: " + GetGridWidth(container) + ",");
            codeBuilder.ComplementaryCode.IncreaseIndent();
            codeBuilder.ComplementaryCode.AddLine("dataSource: [],");
            codeBuilder.ComplementaryCode.AddLine("primaryKey: '" + primaryKey + "',");
            codeBuilder.ComplementaryCode.AddLine("autoGenerateColumns: false,");
            codeBuilder.ComplementaryCode.AddLine("autofitLastColumn: {0},", container.AutoFitLastColumn.ToString().ToLower());

            codeBuilder.ComplementaryCode.AddLine("dataSourceType: 'json',");
            if (innerDataGrids != null && innerDataGrids.Count > 0)
                codeBuilder.ComplementaryCode.AddLine("autoGenerateLayouts: false,");
            codeBuilder.ComplementaryCode.AddLine("renderCheckboxes: true,");
            codeBuilder.ComplementaryCode.AddLine("autoCommit: true,");
            if (container.Virtualization)
            {
                codeBuilder.ComplementaryCode.AddLine("rowVirtualization: true,");
                codeBuilder.ComplementaryCode.AddLine("virtualizationMode: \"continuous\",");
                codeBuilder.ComplementaryCode.AddLine("avgRowHeight: {0},", avgRowHeight);
                codeBuilder.ComplementaryCode.AddLine("autoAdjustHeight: false,");
            }

            codeBuilder.ComplementaryCode.AddLine("cellClick: function(evt, ui) {");
            codeBuilder.ComplementaryCode.AddLine("     if (typeof vm.OnGridClientClick === 'function') {");
            codeBuilder.ComplementaryCode.AddLine("         entity = findElementByKey(" + listBinding + ", 'RowDataId', ui.rowKey);");
            codeBuilder.ComplementaryCode.AddLine("         vm.OnGridClientClick('" + idElement + "', ui.colKey, entity);");
            codeBuilder.ComplementaryCode.AddLine("     }");
            codeBuilder.ComplementaryCode.AddLine("},");
            codeBuilder.ComplementaryCode.AddLine("enableUTCDates: true,");
            codeBuilder.ComplementaryCode.AddLine("featureChooserIconDisplay: 'none',");
            codeBuilder.ComplementaryCode.AddLine("dataRendered: function(evt, ui) { ");
            if (container.EnableMultiSelection)
                codeBuilder.ComplementaryCode.AddLine("   $('th.ui-iggrid-rowselector-class').unbind('click');");

            if (controls.Any(c => c.ClassName == "MultimediaControl"))
            {
                codeBuilder.ComplementaryCode.AddLine("   showMultimidiaLazy('#" + idElement + "');");
            }
            if (controlsKPI.Count > 0)
            {
                codeBuilder.ComplementaryCode.AddLine("    if ($('.gauge').length) {");
                codeBuilder.ComplementaryCode.AddLine("        var x = document.getElementsByClassName('gauge');");
                codeBuilder.ComplementaryCode.AddLine("        for (var i = 0; i < x.length; i++)");
                codeBuilder.ComplementaryCode.AddLine("            eval(document.getElementById('script' + x[i].id).innerHTML);");
                codeBuilder.ComplementaryCode.AddLine("    }");
            }

            codeBuilder.ComplementaryCode.AddLine("},");
            codeBuilder.ComplementaryCode.AddLine("columns: [");

            //Add primary key column
            codeBuilder.ComplementaryCode.AddLine("    { key: '" + primaryKey + "', headerText: '" + primaryKey + "', width: '50px', dataType: '" + primaryKeyType + "', hidden: true },");

            string gridColumnSettingsTooltips = string.Empty;

            var hasGroupColumn = controls.FindAll(x => x.ColumnMultiHeader != null && x.ColumnMultiHeader != "").OrderBy(x => x.GetDataGridOrder());
            var isNotGroupColumn = controls.FindAll(x => x.ColumnMultiHeader == null || x.ColumnMultiHeader == "");

            var controlVerified = hasGroupColumn.Concat(isNotGroupColumn).ToList();
            var columnCurrent = string.Empty;

            for (int cIndex = 0; cIndex < controls.Count; cIndex++)
            {
                var control = controlVerified[cIndex];

                if (hasGroupColumn.Count() > 0 && (control.ColumnMultiHeader != null && control.ColumnMultiHeader != "") && (columnCurrent != control.ColumnMultiHeader))
                {
                    codeBuilder.ComplementaryCode.AddLine("{");
                    codeBuilder.ComplementaryCode.AddLine("    headerText: '" + control.ColumnMultiHeader + "',");
                    codeBuilder.ComplementaryCode.AddLine("    group: [");
                }

                string ctrlWidth = control.GridColAutoFit ? "*" : control.DataGridWidth.ToString() + "px";

                propertyKey = control.BindingPath.Right(".");
                var visibleControl = (control.IsVisible && control.FieldVisibleGrid != VisibleFieldGrid.Editor ? "false" : "true");
                gridColumnSettingsTooltips += (gridColumnSettingsTooltips.IsNullOrEmpty() ? "" : ",") +
                    "{ columnKey: \"" + propertyKey + "\", allowTooltips: " + (control.ClassName != "LookUpTextBox").ToString().ToLower() + " }";

                string columnCssClass = " columnCssClass: " + (!control.DataGridWordWrap ? "'ellipsis'" : "''");

                switch (control.ClassName)
                {
                    case "MultimediaControl":
                        codeBuilder.ComplementaryCode.AddLine("    { key: '" + propertyKey + "Multi', headerText: '', width: '" + ctrlWidth + "', dataType: 'string'," + columnCssClass + ", format: '', hidden: " + visibleControl + ", unbound: true, group: null, " +
                                "formula: function(data, grid) { var templateName = getTemplateImageName(vm.status(), 'grid'); var entity = findElementByKey(" + listBinding + ", 'RowDataId', data.RowDataId); " +
                                "var url = loadMultimidiaUrl('" + control.Name.Left(".") + "', entity." + control.BindingPath.Right(".") + ", vm." + this.ViewModelName + "()); return \"<div class='" + GetMediaWidth(control.MediaWidth) + "'>\" + ko.renderTemplateX(templateName, vm, { tableName: '" + control.Name.Left(".") + "', key: getAbsoluteValue(entity." + control.BindingPath.Right(".") + "), vm: vm." + this.ViewModelName + "(), url: url })+ \"</div>\";}}" + (cIndex == controls.Count - 1 ? String.Empty : ","));
                        break;
                    case "KpiBox":
                    case "Gauge":
                        var nameGauge = control.BindingPath.Right(".");
                        string kpiSufix = "";
                        if (nameGauge.Length > 7 && nameGauge.Right(7) == "KpiInfo")
                        {
                            kpiSufix = "KpiInfo";
                            nameGauge = nameGauge.Remove(nameGauge.Length - 7);
                        }
                        var template = "\"<div id='${RowDataId}' style='width:400px;height:20px;'></div> <script>$('#${RowDataId}').kendoLinearGauge( {gaugeArea: {background: 'transparent', width:230}, pointer: { value: ${" + nameGauge + "}, color: '#8B8386', shape: 'arrow' }, scale: { vertical: false ,line:{visible: false}, labels: {visible: false}, min: \" + dadosGauge" + nameGauge + ".min + \", max: \" + dadosGauge" + nameGauge + ".max + \", ranges: \" + dadosGauge" + nameGauge + ".ranges + \" } });</script>\"";
                        var formatter = "function (val, record) { return makeGauge(val, record, '" + nameGauge + "'" + (control.ClassName == "KpiBox" ? ", true, '" + kpiSufix + "'" : "") + "); }";
                        codeBuilder.ComplementaryCode.AddLine("    { key: '" + propertyKey + "', headerText: '" + control.DisplayName + "', width: '" + ctrlWidth + "', dataType: '" + GetPropDataType(control.DataType, control.DomainName) + "'," + columnCssClass + ", format: '" + GetFormatDataType(control) + "', hidden: " + visibleControl + ", unbound: false, group: null, formatter: " + formatter + " }" + (cIndex == controls.Count - 1 ? String.Empty : ","));
                        break;
                    case "MaskedTextBox":
                        codeBuilder.ComplementaryCode.AddLine("    { key: '" + propertyKey + "', headerText: '" + control.DisplayName + "', headerCssClass: '" + "header-line-break" + "', width: '" + ctrlWidth + "', dataType: '" + GetPropDataType(control.DataType, control.DomainName) + "'," + columnCssClass + ", hidden: " + visibleControl + ", formatter: function(val) { return (val == null ? '' : val.toString()).mask('" + GetMaskForDisplay(control) + "') } }" + (cIndex == controls.Count - 1 ? String.Empty : ","));
                        break;
                    case "CustomControl":
                        var customHtml = "\"" + control.HtmlCode.Replace('\r', ' ').Replace('\n', ' ').Replace("\"", "\\\"") + "\"";
                        codeBuilder.ComplementaryCode.AddLine("    { key: '" + control.Name + "', headerText: '" + control.DisplayName + "', headerCssClass: '" + "header-line-break" + "', width: '" + ctrlWidth + "', dataType: 'string', hidden: " + visibleControl + ", unbound: true, template: " + customHtml + " }" + (cIndex == controls.Count - 1 ? String.Empty : ","));
                        break;
                    case "CheckBox":
                        codeBuilder.ComplementaryCode.AddLine("    { key: '" + propertyKey + "', headerText: '" + control.DisplayName + "', headerCssClass: '" + "header-line-break" + "', width: '" + ctrlWidth + "', dataType: 'bool', format: 'checkbox', hidden: " + visibleControl + ", unbound: false, group: null " + " }" + (cIndex == controls.Count - 1 ? String.Empty : ","));
                        break;
                    case "DateTimeTextBox":
                        codeBuilder.ComplementaryCode.AddLine("    { key: '" + propertyKey + "', headerText: '" + control.DisplayName + "', headerCssClass: '" + "header-line-break" + "', width: '" + ctrlWidth + "', dataType: 'date'," + columnCssClass + ", format: '" + GetFormatDataType(control) + "', hidden: " + visibleControl + ", unbound: false, group: null }" + (cIndex == controls.Count - 1 ? String.Empty : ","));
                        break;
                    case "ComboBox":
                        codeBuilder.ComplementaryCode.AddLine("    { key: '" + propertyKey + "Name', headerText: '" + control.DisplayName + "', headerCssClass: '" + "header-line-break" + "', width: '" + ctrlWidth + "', dataType: 'string'," + columnCssClass + ", hidden: " + visibleControl + ", unbound: false, group: null }" + (cIndex == controls.Count - 1 ? String.Empty : ","));
                        break;
                    case "NumericTextBox":
                        codeBuilder.ComplementaryCode.AddLine("    { key: '" + propertyKey + "', headerText: '" + control.DisplayName + "', headerCssClass: '" + "header-line-break" + "', width: '" + ctrlWidth.ToString() + "px', dataType: 'number'," + columnCssClass + ", format: '" + GetFormatDataType(control) + "', hidden: " + visibleControl + ", unbound: false, group: null, formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }}" + (cIndex == controls.Count - 1 ? String.Empty : ","));
                        break;
                    default:
                        codeBuilder.ComplementaryCode.AddLine("    { key: '" + propertyKey + "', headerText: '" + control.DisplayName + "', headerCssClass: '" + "header-line-break" + "', width: '" + ctrlWidth.ToString() + "px', dataType: '" + (control.ClassName == "LookUpTextBox" ? "string" : GetPropDataType(control.DataType, control.DomainName)) + "'," + columnCssClass + ", format: '" + (control.ClassName == "LookUpTextBox" ? "" : GetFormatDataType(control)) + "', hidden: " + visibleControl + ", unbound: false, group: null " + (control.ClassName == "NumericTextBox" ? ", formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); } " : " ") + " }" + (cIndex == controls.Count - 1 ? String.Empty : ","));
                        break;
                }
                columnCurrent = control.ColumnMultiHeader;
                var nextControl = (controlVerified.Count > (cIndex + 1) ? controlVerified[cIndex + 1] : null);

                bool closeGroupHeader = false;
                if (nextControl == null) closeGroupHeader = true;
                else if (columnCurrent != nextControl.ColumnMultiHeader) closeGroupHeader = true;
                if (hasGroupColumn.Count() > 0 && (control.ColumnMultiHeader != null && control.ColumnMultiHeader != "") && closeGroupHeader)
                {
                    codeBuilder.ComplementaryCode.AddLine("    ]");
                    codeBuilder.ComplementaryCode.AddLine("},");
                }

            }
            codeBuilder.ComplementaryCode.AddLine("],");
            codeBuilder.ComplementaryCode.AddLine("features: [");
            if (container.PageSize > 0) codeBuilder.ComplementaryCode.AddLine("            { name: 'Paging', type: 'local', pageSizeDropDownLocation: 'inpager', pageSize: " + container.PageSize.ToString() + ", pageIndexChanged: function (evt, ui) { if (!$('#" + idElement + "').igGridSelection('option', 'multipleSelection')) $('#" + idElement + "').igGridSelection('selectRow', 0); selectLightGridCurrentItem(vm.goToKey, '" + primaryKey + "', ui" + (binding.IsNullOrEmpty() ? "" : ", " + currentBinding + ", " + listBinding + "") + "); } },");
            codeBuilder.ComplementaryCode.AddLine("            { name: 'Sorting', type: 'local', caseSensitive: false,");
            codeBuilder.ComplementaryCode.AddLine("              columnSorting: function (evt, ui) { ");
            codeBuilder.ComplementaryCode.AddLine("                  $.grep(ui.owner.grid._visibleColumnsArray, function (e) { ");
            codeBuilder.ComplementaryCode.AddLine("                      if (e.key === ui.columnKey && e.dataType === 'string') ");
            codeBuilder.ComplementaryCode.AddLine("                          return $('#" + idElement + "').igGridSorting('option', 'caseSensitive', false); ");
            codeBuilder.ComplementaryCode.AddLine("                      else if (e.key === ui.columnKey) ");
            codeBuilder.ComplementaryCode.AddLine("                          return $('#" + idElement + "').igGridSorting('option', 'caseSensitive', true); ");
            codeBuilder.ComplementaryCode.AddLine("                  }); ");
            codeBuilder.ComplementaryCode.AddLine("              } ");
            codeBuilder.ComplementaryCode.AddLine("              , customSortFunction: function (data, fields, direction) { return gridFunctions.sort(data, fields, direction); } ");
            codeBuilder.ComplementaryCode.AddLine((binding.IsNullOrEmpty() && container.PageSize <= 0 ? "              , columnSorted: function (event, args) { if (!isNullOrEmpty(args.columnKey) && !isNullOrEmpty(args.direction)) { vm.sortData(args.columnKey + ' ' + args.direction); } } " : "") + "},");
            if (container.EnableFilterTextInGrid)
            {
                codeBuilder.ComplementaryCode.AddLine("            { name: 'Filtering', mode: 'advanced', filterDropDownItemIcons: false, filterDropDownWidth: 200, allowFiltering: true, type: 'local', renderFC: false, renderFilterButton: false, ");
                codeBuilder.ComplementaryCode.AddLine("                  dataFiltered: function (evt, ui) {");
                codeBuilder.ComplementaryCode.AddLine("                  var columnsFilters = [];");
                codeBuilder.ComplementaryCode.AddLine("                  $.each(ui.owner._currentAdvancedExpressions, function(i, item){ columnsFilters.push(item.fieldName); });");
                codeBuilder.ComplementaryCode.AddLine("                  var cols = $('#' + ui.owner.grid.element[0].id + '_container .ui-iggrid-headertable th');");
                codeBuilder.ComplementaryCode.AddLine("                  cols.each(function (i, item) {");
                codeBuilder.ComplementaryCode.AddLine("                      var name = item.id.substr(ui.owner.grid.element[0].id.length + 1);");
                codeBuilder.ComplementaryCode.AddLine("                      var filter = $(item).find('span.ui-icon-search');");
                codeBuilder.ComplementaryCode.AddLine("                      if (columnsFilters.contains(name)) {");
                codeBuilder.ComplementaryCode.AddLine("                          if (!filter.hasClass('grid-column-researched'))");
                codeBuilder.ComplementaryCode.AddLine("                              filter.addClass('grid-column-researched');");
                codeBuilder.ComplementaryCode.AddLine("                      } else {");
                codeBuilder.ComplementaryCode.AddLine("                          if (filter.hasClass('grid-column-researched'))");
                codeBuilder.ComplementaryCode.AddLine("                              filter.removeClass('grid-column-researched');");
                codeBuilder.ComplementaryCode.AddLine("                      }");
                codeBuilder.ComplementaryCode.AddLine("                  });");
                codeBuilder.ComplementaryCode.AddLine("                }");
                codeBuilder.ComplementaryCode.AddLine("            },");
            }

            codeBuilder.ComplementaryCode.AddLine("            { name: 'Selection', mode: 'row'" + (container.EnableMultiSelection ? ", multipleSelection: vm.allowMultiSelectionInSearch()" : "") + "},");
            if (container.EnableMultiSelection)
            {
                string hasWithVirtualization = string.Empty;
                if (container.Virtualization)
                    hasWithVirtualization = "rowSelectorColumnWidth: 40,";

                codeBuilder.ComplementaryCode.AddLine("            { name: 'RowSelectors', enableCheckBoxes: vm.allowMultiSelectionInSearch(), enableRowNumbering: false, " + hasWithVirtualization + " checkBoxStateChanged: function(evt, ui){ ");
                codeBuilder.ComplementaryCode.AddLine("               if ((typeof vm.OnDataGridRowChecked === 'function')){");
                codeBuilder.ComplementaryCode.AddLine("                   vm.OnDataGridRowChecked('" + idElement + "', self.selectedItems());");
                codeBuilder.ComplementaryCode.AddLine("               }");
                codeBuilder.ComplementaryCode.AddLine("               var selectedRows = grid.igGridSelection('selectedRows');");
                codeBuilder.ComplementaryCode.AddLine("               var selectedRow = ui.owner.grid.selectedRow();");
                codeBuilder.ComplementaryCode.AddLine("               var dataViewLength = ui.grid.dataSource.dataView().length;");
                codeBuilder.ComplementaryCode.AddLine("               if ((selectedRows.length == dataViewLength) || (selectedRow == null && selectedRows.length > 0)){");
                codeBuilder.ComplementaryCode.AddLine("                   rowId = [];");
                codeBuilder.ComplementaryCode.AddLine("                   rowId['id'] = 1;");
                codeBuilder.ComplementaryCode.AddLine("                   selectLightGridCurrentItem(vm.goToKey, 'RowDataId', rowId" + (binding.IsNullOrEmpty() ? "" : ", " + currentBinding + ", " + listBinding) + ");");
                codeBuilder.ComplementaryCode.AddLine("               } else if(ui.owner.grid.selectedRow() != null)");
                codeBuilder.ComplementaryCode.AddLine("                   selectLightGridCurrentItem(vm.goToKey, 'RowDataId', ui" + (binding.IsNullOrEmpty() ? "" : ", " + currentBinding + ", " + listBinding) + ");");
                codeBuilder.ComplementaryCode.AddLine("                }, ");
                codeBuilder.ComplementaryCode.AddLine("                checkBoxStateChanging: function (evt, ui) { isFiredFromCheckbox = true; }");
                codeBuilder.ComplementaryCode.AddLine("            },");
            }
            codeBuilder.ComplementaryCode.AddLine("            { name: 'Tooltips', columnSettings:[" + gridColumnSettingsTooltips + "] },");

            codeBuilder.ComplementaryCode.AddLine("            { name: 'Resizing' }, ");
            codeBuilder.ComplementaryCode.AddLine("            { name: 'MultiColumnHeaders' }");
            if (container.HasColumnFixing)
                codeBuilder.ComplementaryCode.AddLine("            ,{ name: 'ColumnFixing' }");
            else
                codeBuilder.ComplementaryCode.AddLine("            ,{ name: 'ColumnMoving', addMovingDropdown: false }");
            codeBuilder.ComplementaryCode.AddLine((container.HasGroupBy ? "           ,{ name: 'GroupBy', emptyGroupByAreaContent: 'Arraste para esta área a(s) coluna(s) que deseja agrupar.', initialExpand: false" + GetGroupByColumnSettings(container.GroupByColumns, controls) + ", groupedColumnsChanged: function (evt, ui) { $('#" + idElement + "_groupbyarea').toggleClass('is-grouped', (ui.groupedColumns.length > 0)); } }" : ""));
            codeBuilder.ComplementaryCode.AddLine((hasSummaries ? "           ,{ name: 'Summaries', showSummariesButton:false, " + GetSumariesColumnSettings(controls, idElement) + " }" : String.Empty));
            #region updating
            codeBuilder.ComplementaryCode.AddLine("           ,{ name: 'Updating', horizontalMoveOnEnter: true,");
            codeBuilder.ComplementaryCode.AddLine("               enableDataDirtyException: false, ");
            codeBuilder.ComplementaryCode.AddLine("               generatePrimaryKeyValue: function(evt, ui){  },");
            codeBuilder.ComplementaryCode.AddLine("               enableDeleteRow: false,");
            codeBuilder.ComplementaryCode.AddLine("               enableAddRow: false,");
            codeBuilder.ComplementaryCode.AddLine("               startEditTriggers: 'click',");
            codeBuilder.ComplementaryCode.AddLine("               editMode: 'none',");
            codeBuilder.ComplementaryCode.AddLine("               rowEditDialogContainment: 'window',");
            codeBuilder.ComplementaryCode.AddLine("               showReadonlyEditors: false,");
            codeBuilder.ComplementaryCode.AddLine("               showDoneCancelButtons: false,");
            codeBuilder.ComplementaryCode.AddLine("            }");
            #endregion
            codeBuilder.ComplementaryCode.AddLine("        ]");


            codeBuilder.ComplementaryCode.AddLine("});");

            #region Filter By Text
            if (container.EnableFilterTextInGrid)
            {
                var listControls = controls.Select(c => new
                {
                    NameBind = c.BindingPath.Right(".") + (c.DomainName.IsNullOrEmpty() ? "" : "Name"),
                    DisplayName = c.DisplayName,
                    jsType = GetPropDataType(c.DataType, c.DomainName),
                    ClassName = c.ClassName,
                    IsNumber = GetPropDataType(c.DataType, c.DomainName).InList("number") || c.ClassName.InList("NumericTextBox")
                });

                codeBuilder.ComplementaryCode.AddLine("$('#" + idElement + "_finder').igTextEditor({");
                codeBuilder.ComplementaryCode.AddLine("    textChanged: function(evt, args) {");
                var campos = listControls.Where(c => !c.jsType.InList("bool", "date"))
                    .Select(c =>
                        "{ fieldName: '" + c.NameBind + "', expr: " +
                        (c.IsNumber ? "(isNull(args.text)?'':args.text).replaceAll(',','.')" : "args.text") +
                        ", cond: '" + (c.IsNumber ? "equals" : "contains") + "' }");

                codeBuilder.ComplementaryCode.AddLine("        var filterSettings = [" + string.Join(",", campos) + "];");

                if (listControls.Where(c => c.jsType == "bool").Any())
                {
                    codeBuilder.ComplementaryCode.AddLine("        if(args.text.toLowerCase() === 'true' || args.text.toLowerCase() === 'verdadeiro') {");
                    foreach (var c in listControls.Where(c => c.jsType == "bool"))
                        codeBuilder.ComplementaryCode.AddLine("        filterSettings.push({ fieldName: '" + c.NameBind + "', expr: '',cond: 'true' })");
                    codeBuilder.ComplementaryCode.AddLine("        }");
                    codeBuilder.ComplementaryCode.AddLine("        if(args.text.toLowerCase() === 'false' || args.text.toLowerCase() === 'falso') {");
                    foreach (var c in listControls.Where(c => c.jsType == "bool"))
                        codeBuilder.ComplementaryCode.AddLine("        filterSettings.push({ fieldName: '" + c.NameBind + "', expr: '',cond: 'false' })");
                    codeBuilder.ComplementaryCode.AddLine("        }");

                    foreach (var c in listControls.Where(c => c.jsType == "bool"))
                    {
                        codeBuilder.ComplementaryCode.AddLine("        if(args.text.toLowerCase() == '" + c.DisplayName.ToLower() + "')");
                        codeBuilder.ComplementaryCode.AddLine("            filterSettings.push({ fieldName: '" + c.NameBind + "', expr: '', cond: 'true' })");
                    }
                }
                if (listControls.Where(c => c.jsType == "date").Any())
                {

                    codeBuilder.ComplementaryCode.AddLine("        var dateExpr = moment(args.text, 'DD/MM/YYYY').toDate();");
                    codeBuilder.ComplementaryCode.AddLine("        if (!isNullOrEmpty(dateExpr)) {");
                    foreach (var c in listControls.Where(c => c.jsType == "date"))
                        codeBuilder.ComplementaryCode.AddLine("            filterSettings.push({ fieldName: '" + c.NameBind + "', expr: dateExpr, cond: 'on' })");
                    codeBuilder.ComplementaryCode.AddLine("        }");
                }
                codeBuilder.ComplementaryCode.AddLine("        source.filter(filterSettings, 'OR', false);");
                codeBuilder.ComplementaryCode.AddLine("        $(grid).igGrid('option', 'dataSource', source.dataView());");
                codeBuilder.ComplementaryCode.AddLine("    }");
                codeBuilder.ComplementaryCode.AddLine("});");
            }
            #endregion
            codeBuilder.ComplementaryCode.AddLine("if ((typeof vm.OnDataGridCreated === 'function')){");
            codeBuilder.ComplementaryCode.AddLine("    vm.OnDataGridCreated('" + idElement + "');");
            codeBuilder.ComplementaryCode.AddLine("}");

            //treatment for select a row through tab key
            codeBuilder.ComplementaryCode.AddLine("var selectionrowselectionchanged = null, selectedRowId = -1;");
            codeBuilder.ComplementaryCode.AddLine("selectionrowselectionchanged = function (evt, ui) {");
            codeBuilder.ComplementaryCode.AddLine("    if ((ui.owner.grid.selectedRow() && typeof ui.owner.grid.selectedRow().id !== 'undefined') || (ui.owner.grid.selectedRows() && ui.owner.grid.selectedRows().length > 0)) { ");
            if (!container.EnableMultiSelection)
            {
                codeBuilder.ComplementaryCode.AddLine("        if (isNullOrEmpty(ui.owner.selectedRows())|| ui.selectedRows.length <= 1) {");
                codeBuilder.ComplementaryCode.AddLine("            $(document).undelegate('#" + idElement + "', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);");
                codeBuilder.ComplementaryCode.AddLine("            ui.owner.clearSelection();");
                codeBuilder.ComplementaryCode.AddLine("            ui.owner.selectRow(ui.row.index);");
                codeBuilder.ComplementaryCode.AddLine("            selectedRowId = ui.row.id;");
                codeBuilder.ComplementaryCode.AddLine("            $(document).delegate ('#" + idElement + "', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);");
                codeBuilder.ComplementaryCode.AddLine("        }");
            }
            codeBuilder.ComplementaryCode.AddLine("        selectLightGridCurrentItem(vm.goToKey, '" + primaryKey + "', ui" + (binding.IsNullOrEmpty() ? "" : ", " + currentBinding + ", " + listBinding + "") + "); ");
            codeBuilder.ComplementaryCode.AddLine("     } ");
            if (container.EnableMultiSelection)
                codeBuilder.ComplementaryCode.AddLine("     if ((typeof vm.OnDataGridRowChecked === 'function')){ vm.OnDataGridRowChecked('" + idElement + "', self.selectedItems()); }");
            codeBuilder.ComplementaryCode.AddLine("};");

            codeBuilder.ComplementaryCode.AddLine("$(document).delegate('#" + idElement + "', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);");
            //event focus
            if (!container.EnableMultiSelection)
            {
                codeBuilder.ComplementaryCode.AddLine("$('#" + idElement + " > tbody tr').live('focus', function(evt) {");
                codeBuilder.ComplementaryCode.AddLine("    var grid = $('#" + idElement + "'), row = $(this).closest('tr'), id = parseInt(row.attr('data-id'), 10);");
                codeBuilder.ComplementaryCode.AddLine("    var selectedRows = grid.igGridSelection('option', 'multipleSelection') ? grid.igGridSelection('selectedRows') : [grid.igGridSelection('selectedRow')];");
                codeBuilder.ComplementaryCode.AddLine("    if (selectedRowId === id) return;");
                codeBuilder.ComplementaryCode.AddLine("    selectedRowId = id;");
                codeBuilder.ComplementaryCode.AddLine("    grid.igGridSelection('selectRowById', id);");
                codeBuilder.ComplementaryCode.AddLine("    grid.trigger('iggridselectionrowselectionchanged', {");
                codeBuilder.ComplementaryCode.AddLine("    owner: grid.data('igGridSelection'),");
                codeBuilder.ComplementaryCode.AddLine("        row: {");
                codeBuilder.ComplementaryCode.AddLine("           element: row,");
                codeBuilder.ComplementaryCode.AddLine("           index: row.index(),");
                codeBuilder.ComplementaryCode.AddLine("           id: id");
                codeBuilder.ComplementaryCode.AddLine("        },");
                codeBuilder.ComplementaryCode.AddLine("        selectedRows: selectedRows");
                codeBuilder.ComplementaryCode.AddLine("     });");
                codeBuilder.ComplementaryCode.AddLine("});");
            }

            if (_layOut.IsSecundary)
            {
                codeBuilder.ComplementaryCode.AddLine("grid.delegate('.ui-iggrid-activerow', 'dblclick', function (e) {");
                codeBuilder.ComplementaryCode.AddLine("    if (vm." + this.ViewModelName + "().status() === 'Q') vm." + this.ViewModelName + "().dataToolbar.viewInfo();");
                codeBuilder.ComplementaryCode.AddLine("});");
            }

            codeBuilder.ComplementaryCode.DecreaseIndent();
            codeBuilder.ComplementaryCode.AddLine("}");

            codeBuilder.ComplementaryCode.AddLine("vm.addDataSource({ key: '" + idElement + "', name: '" + (binding.IsNullOrEmpty() ? "dataView" : binding.Right(".")) + "', itemsSource: itemsSource });");

            codeBuilder.ComplementaryCode.DecreaseIndent();
            codeBuilder.ComplementaryCode.AddLine("}");
            #endregion

            codeBuilder.DecreaseIndent();


            //Generate internal data grid
            if (innerDataGrids != null && innerDataGrids.Count > 0)
            {
                foreach (var dg in innerDataGrids)
                {
                    ComposeLightDataGrid(container, dg.Container, elementClass, codeBuilder, rows, columns, dg.Containers);
                }
            }
        }

        private string GetMaxValueNumeric(LayoutControlV2 control, bool putMaxValueLabel, bool putBeforeComma, bool putAfterComma)
        {
            bool hasDecimal = control.GetPrecisionDecimalsInt() > 0;
            string value = string.Empty, valueRet = string.Empty;
            if (control.DataType.RemoveNullDefinition() == "long")
            {
                valueRet = cMaxValueLongType.ToString();
            }
            else
            {
                value = ((control.GetPrecision() <= control.GetPrecisionDecimalsInt()) ? "" : new string('9', control.GetPrecision() - control.GetPrecisionDecimalsInt()) + (hasDecimal ? "." + new string('9', control.GetPrecisionDecimalsInt()) : ""));
                valueRet = (value.IsNullOrEmpty() || control.GetPrecisionDecimalsInt() <= 0 ? "null" : value);
            }

            if (putMaxValueLabel)
                valueRet = "maxValue: " + valueRet;
            if (putBeforeComma)
                valueRet = ", " + valueRet;
            if (putAfterComma)
                valueRet = valueRet + ", ";

            return valueRet;

        }

        private string GetMaxLengthNumeric(LayoutControlV2 control)
        {
            bool hasDecimal = control.GetPrecisionDecimalsInt() > 0;
            return (control.GetPrecision() + (hasDecimal ? 1 : 0)).ToString();
        }

        private string GetGridWidth(LayoutContainer container)
        {
            string result = "100";

            switch (container.GridWidth)
            {
                case GridSizeWidth.Medium:
                    result = "75";
                    break;
                case GridSizeWidth.Small:
                    result = "50";
                    break;
                default:
                    break;
            }

            return "'" + result + "%'";
        }

        private void AddButtonsConfigAndEditor(LayoutContainer container, Tools.CodeBuilder codeBuilder, string idElement, string dataView, string currentBinding, string listBinding, string parentBinding, string entityName, string rootListBinding, bool newGrid = false)
        {
            //codeBuilder.AddLine("<button id=\"" + idElement + "_ConfigBtn\" class=\"btn default background-color-theme\" data-placement=\"top\" title=\"Configurar Grade\" data-bind=\"click: function () {$('#" + idElement + "').igGridHiding('showColumnChooser');  }\"><i class=\"fa fa-gear\"></i></button>");
            if (container.IsTemplate)
            {
                codeBuilder.AddLine("<button id=\"" + idElement + "_EditorBtn\" class=\"btn default background-color-theme\" data-placement=\"top\" title=\"Alterar edição para modo Template\" ");
                codeBuilder.AddLine(" data-bind=\"click:function () { ");
                codeBuilder.AddLine("   $root." + this.ViewModelName + "().openEditor('#" + idElement + "', '" + container.Name + "', '" + currentBinding + ";" + listBinding + "', '" + dataView + ";" + parentBinding + "', '" + entityName + "', false)");
                codeBuilder.AddLine("}\"><i class=\"fa fa-th\"></i></button>");
                if (!newGrid)
                {
                    codeBuilder.AddLine("<button id=\"" + idElement + "_FormBtn\" class=\"btn default background-color-theme\" data-placement=\"top\" title=\"Alterar edição para modo Formulário\" ");
                    codeBuilder.AddLine(" data-bind=\"click:function () { ");
                    codeBuilder.AddLine("   $root." + this.ViewModelName + "().openEditor('#" + idElement + "', '" + container.Name + "', '" + currentBinding + ";" + listBinding + "', '" + dataView + ";" + parentBinding + "', '" + entityName + "', true)");
                    codeBuilder.AddLine("}\"><i class=\"fa fa-th-large\"></i></button>");
                }
            }
            if (container.HasGroupBy)
            {
                codeBuilder.AddLine("<button id=\"" + idElement + "_GroupBtn\" class=\"btn default background-color-theme\" data-placement=\"top\" title=\"Habilita agrupamento\" ");
                codeBuilder.AddLine(" data-bind=\"click:function () { ");
                codeBuilder.AddLine("   if($('#" + idElement + "_groupbyarea').length) {");
                codeBuilder.AddLine("       if(typeof $('#" + idElement + "').data().igGridGroupBy == 'object' && !$('#" + idElement + "').data().igGridGroupBy._isgroup){");
                codeBuilder.AddLine("           if($('#" + idElement + "_groupbyarea').hasClass('hide')){");
                codeBuilder.AddLine("               $('#" + idElement + "_groupbyarea').removeClass('hide');");
                codeBuilder.AddLine("               $('.fa.fa-level-up').addClass('fa fa-level-down').removeClass('fa-level-up');");
                codeBuilder.AddLine("               $('#" + idElement + "_GroupBtn').attr('title', 'Desabilita agrupamento');");
                codeBuilder.AddLine("           }");
                codeBuilder.AddLine("           else{");
                codeBuilder.AddLine("               $('#" + idElement + "_groupbyarea').addClass('hide');");
                codeBuilder.AddLine("               $('.fa.fa-level-down').addClass('fa fa-level-up').removeClass('fa-level-down');");
                codeBuilder.AddLine("               $('#" + idElement + "_GroupBtn').attr('title', 'Habilita agrupamento');");
                codeBuilder.AddLine("           }");
                codeBuilder.AddLine("       }");
                codeBuilder.AddLine("       else{");
                codeBuilder.AddLine("           $root." + this.ViewModelName + "().app.showMessage('Não é possível desabilitar o agrupamento quando ja está sendo utilizado!', 'Informação', ['Ok']);");
                codeBuilder.AddLine("       }");
                codeBuilder.AddLine("}");
                if (!container.GroupByColumns.IsNullOrEmpty())
                    codeBuilder.AddLine("}\"><i class=\"fa fa-level-down\"></i></button>");
                else
                    codeBuilder.AddLine("}\"><i class=\"fa fa-level-up\"></i></button>");
            }

            #region button for edit Layout
            //btn
            if (!newGrid)
            {
                codeBuilder.AddLine("<button id=\"" + idElement + "_LayoutBtn\" class=\"btn default background-color-theme\" data-placement=\"top\" title=\"" + "Editar o Layout da Grid".Translate() + "\" data-bind=\"popoverWithBind: { template: '#" + idElement + "_templateLayout', vm: function(){return $root." + this.ViewModelName + "().gridSaveStates['" + idElement + "'];}, ctrlName: '" + idElement + "_gb', headerText:'Layout da grid' }\"><i class=\"fa fa-gears\" aria-hidden=\"true\" data-placement=\"top\" title=\"" + "Editar o Layout da Grid".Translate() + "\"></i></button>");
            }
            #region template
            codeBuilder.AddLine("<script id=\"" + idElement + "_templateLayout\" type=\"text/html\" tabindex=\"-1\">");
            codeBuilder.AddLine("    <div id=\"" + idElement + "_gb\" class=\"layout-popover\">");
            codeBuilder.AddLine("        <div class=\"row\">");
            codeBuilder.AddLine("            <div class=\"col-md-12\">");
            codeBuilder.AddLine("                <span title=\"" + "Layout salvos".Translate() + "\" style=\"padding-left: 10px;\">Layout salvos:</span>");
            codeBuilder.AddLine("                <div class=\"col-md-10\">");
            codeBuilder.AddLine("                    <span class=\"form-control box-layout-grid\" data-bind=\"igCombo: {selectedItems: currentLayoutId, dataSource: savedLayouts, textKey: 'NomeLayout', valueKey: 'Id', itemTemplate: &quot;<span class='ellipsis' style='display:block' title='${NomeLayout}'>${NomeLayout}</span>&quot;, allowCustomValue : false, enableSelectionChangedUpdate: true, enableClearButton: true, mode: 'editable', width:'100%'}\"></span>");
            codeBuilder.AddLine("                </div>");
            codeBuilder.AddLine("                <div  class=\"col-md-2\">");
            codeBuilder.AddLine("                    <button class=\"btn-press ellipsis\" data-bind=\"enable: (currentLayoutId() < 0 || currentLayoutId() > 0), click: function(){ applyLayout(); }\" style=\"border:0; margin-bottom:0;margin-top:5px;padding:5px 10px;height:26px;width:33px;\"><i class=\"fa fa-check\" aria-hidden=\"true\" /></button>");
            codeBuilder.AddLine("                </div>");
            codeBuilder.AddLine("            </div>");
            codeBuilder.AddLine("        </div>");
            codeBuilder.AddLine("        <div class=\"row\">");
            codeBuilder.AddLine("            <div class=\"col-md-12\">");
            codeBuilder.AddLine("                <div class=\"col-md-10\">");
            codeBuilder.AddLine("                    <button class=\"btn-press ellipsis\" data-bind=\"click: function(){openLayoutCustomize(false);}\"  style=\"border:0;\">" + "Configurar...".Translate() + "</button>");
            codeBuilder.AddLine("                    <button class=\"btn-press ellipsis\" data-bind=\"enable: (currentLayoutId() > 0), click: function(){openLayoutCustomize(true);}\"  style=\"border:0;\">" + "Salvar como".Translate() + "</button>");
            codeBuilder.AddLine("                </div>");
            codeBuilder.AddLine("                <div class=\"col-md-2\">");
            codeBuilder.AddLine("                    <button class=\"btn-press ellipsis\" data-bind=\"enable: (currentLayoutId() > 0), click: deleteLayout\"  style=\"border:0;\"><i class=\"fa fa-trash-o\" aria-hidden=\"true\" /></button>");
            codeBuilder.AddLine("                </div>");
            codeBuilder.AddLine("            </div>");
            codeBuilder.AddLine("        </div>");
            codeBuilder.AddLine("    </div>");
            codeBuilder.AddLine("</script>");

            #endregion template
            #endregion
        }

        private void ComposeOlapPivotGrid(LayoutContainer parentContainer, LayoutContainer container, LayoutContainerClass elementClass, Tools.CodeBuilder codeBuilder, int rows, int columns)
        {
            string idElement = container.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "olapPivot");
            codeBuilder.AddLine("<div data-bind=\"css: $root." + this.ViewModelName + "().getLayoutColumnSpan('" + idElement + "'), visible: $root." + this.ViewModelName + "().getLayoutVisible('" + idElement + "')\" " + GetCssContainerHeight(container) + ">");
            codeBuilder.IncreaseIndent();
            codeBuilder.AddLine("<div id=\"" + idElement + "\" />");

            #region Defining auxiliary code
            codeBuilder.ComplementaryCalls.AddLine("complement.render" + idElement.Replace("-", "").Replace(" ", "") + "(vm);");
            codeBuilder.ComplementaryCode.AddLine(", render" + idElement.Replace("-", "").Replace(" ", "") + ": function(vm) {");
            codeBuilder.ComplementaryCode.IncreaseIndent();

            codeBuilder.ComplementaryCode.AddLine("if(isNullOrEmpty(globalDataParameters.parameters['OlapServerUri'])){");
            codeBuilder.ComplementaryCode.AddLine("    console.error(\"The parameter 'OlapServerUri' is not defined.\");");
            codeBuilder.ComplementaryCode.AddLine("    throw new Error(200, \"The parameter 'OlapServerUri' is not defined.\");");
            codeBuilder.ComplementaryCode.AddLine("}");
            codeBuilder.ComplementaryCode.AddLine("if(isNullOrEmpty(globalDataParameters.parameters['OlapDataBaseName'])){");
            codeBuilder.ComplementaryCode.AddLine("    console.error(\"The parameter 'OlapDataBaseName' is not defined.\");");
            codeBuilder.ComplementaryCode.AddLine("    throw new Error(200, \"The parameter 'OlapDataBaseName' is not defined.\");");
            codeBuilder.ComplementaryCode.AddLine("}");

            codeBuilder.ComplementaryCode.AddLine();
            codeBuilder.ComplementaryCode.AddLine("$.support.cors = true;");
            codeBuilder.ComplementaryCode.AddLine();

            #region create control
            //Create Pivot View
            codeBuilder.ComplementaryCode.AddLine("var olapPivot = $(\"#" + idElement + "\");");
            codeBuilder.ComplementaryCode.AddLine("olapPivot.igPivotView({ ");
            codeBuilder.ComplementaryCode.AddLine("    height: " + (container.Height <= 0 ? "'500px'" : container.Height.ToString()) + ", ");
            codeBuilder.ComplementaryCode.AddLine("    width: '100%',");
            codeBuilder.ComplementaryCode.AddLine("    dataSourceOptions: {");
            codeBuilder.ComplementaryCode.AddLine("        xmlaOptions: {");
            codeBuilder.ComplementaryCode.AddLine("            serverUrl: globalDataParameters.parameters['OlapServerUri'],");
            codeBuilder.ComplementaryCode.AddLine("            catalog: globalDataParameters.parameters['OlapDataBaseName'],");
            codeBuilder.ComplementaryCode.AddLine("            cube: '" + container.PivotCube + "',");
            //codeBuilder.ComplementaryCode.AddLine("            measureGroup: '" + measureGroup + "',");
            codeBuilder.ComplementaryCode.AddLine("        },");
            codeBuilder.ComplementaryCode.AddLine("        rows: '" + container.PivotRows + "',");
            codeBuilder.ComplementaryCode.AddLine("        columns: '" + container.PivotColumns + "',");
            codeBuilder.ComplementaryCode.AddLine("        measures: '" + (container.PivotMeasures.IsNullOrEmpty() ? String.Empty : String.Join(",", container.PivotMeasures.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(e => "[Measures].[" + e + "]"))) + "',");
            codeBuilder.ComplementaryCode.AddLine("        filters: '" + container.PivotFilters + "'");
            codeBuilder.ComplementaryCode.AddLine("    }");
            codeBuilder.ComplementaryCode.AddLine("});");
            #endregion create control

            //Creating Binding Update
            if (!container.DefinedUserName.IsNullOrEmpty())
            {
                codeBuilder.ComplementaryCode.AddLine("if (!vm.dataShared['" + container.DefinedUserName + "'])");
                codeBuilder.ComplementaryCode.AddLine("   vm.dataShared['" + container.DefinedUserName + "'] = olapPivot;");
            }

            codeBuilder.ComplementaryCode.DecreaseIndent();
            codeBuilder.ComplementaryCode.AddLine("}");
            #endregion

            codeBuilder.DecreaseIndent();
        }

        private string GetConditionsToBinding(string bidingPath)
        {
            var condition = string.Empty;

            if (!string.IsNullOrEmpty(bidingPath))
            {
                var bidingPathParts = bidingPath.Split(new char[] { '.' }).ToList();

                for (int i = 0; i < bidingPathParts.Count; i++)
                {
                    var length = (i + 1);
                    var current = bidingPathParts.GetRange(0, length);

                    if (length < bidingPathParts.Count)
                        condition += string.Format("{0} != null && ", string.Join(".", current));
                    else
                        condition += string.Format("{0} != null ", string.Join(".", current));
                }
            }

            return condition;
        }

        private string GetFlexmonsterDatePattern(List<LayoutControlV2> controls)
        {
            var datePattern = "dd/MM/yyyy";

            if (controls != null && controls.Any(x => x.DataType.Contains("DateTime")))
            {
                var format = controls.First(x => x.DataType.Contains("DateTime")).DataFormatString;

                if (string.IsNullOrEmpty(format) || format == "d")
                    datePattern = "dd/MM/yyyy";
                else if (format == "G")
                    datePattern = "dd/MM/yyyy hh:mm:ss";
                else if (format == "g")
                    datePattern = "dd/MM/yyyy hh:mm";
                else if (format == "T")
                    datePattern = "HH:mm:ss";
                else if (format == "t")
                    datePattern = "HH:mm";
                else
                    datePattern = format;
            }
            return datePattern;
        }

        private void ComposeFlatPivotGrid(LayoutContainer parentContainer, LayoutContainer container, LayoutContainerClass elementClass, Tools.CodeBuilder codeBuilder, int rows, int columns)
        {
            if (container.PivotDataSource.IsNull())
                throw new Exception("A fonte de dados do Pivot Table está nula. Favor selecione um tipo de fonte de dados.");

            var controls = container.Controls.Where(e => e is LayoutControlV2 && !e.BindingPath.IsNullOrEmpty()).Select(e => (LayoutControlV2)e).ToList();

            var componentName = container.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "pivot");
            //var idElement = "pivot" + Guid.NewGuid().ToString() + componentName;
            var idElement = "pivotContainer" + componentName;
            var pivotName = container.GetControlName();
            codeBuilder.AddLine("<div id=\"" + componentName + "\" data-bind=\"css: $root." + this.ViewModelName + "().getLayoutColumnSpan('" + componentName + "'), visible: $root." + this.ViewModelName + "().getLayoutVisible('" + componentName + "')\">");

            if (controls.Count == 0 && !container.PivotDataSource.ToLower().Equals("olap")) return;

            var controlBindingPath = container.PivotDataSource.ToLower().Equals("olap") ? container.BindingPath : controls.First().BindingPath;
            var binding = GetBindingPath(controlBindingPath, true);
            var currentBinding = GetFullBindingPath(controlBindingPath, false);
            var listBinding = GetFullBindingPath(controlBindingPath, true);
            var parentRecord = listBinding.Left("." + listBinding.Right("."));
            var dataView = binding.IsNullOrEmpty() ? "" : (binding.Right(".") + "#").Left("List#");

            codeBuilder.IncreaseIndent();
            codeBuilder.AddLine("<div id=\"" + idElement + "\" class=\"row pivot-style\" />");

            #region Defining auxiliary code

            codeBuilder.ComplementaryCalls.AddLine("complement.render" + idElement.Replace("-", "").Replace(" ", "") + "(vm);");
            codeBuilder.ComplementaryCode.AddLine(", render" + idElement.Replace("-", "").Replace(" ", "") + ": function(vm) {");
            codeBuilder.ComplementaryCode.IncreaseIndent();

            //Create Pivot View
            if (binding.IsNullOrEmpty() && !_layOut.IsSecundary)
            {
                codeBuilder.ComplementaryCode.AddLine("if (vm.hasMainTopDataGrid && !vm.hasMainTopDataGrid()) vm.hasMainTopDataGrid(true);");
                this.HasMainTopDataGrid = true;
            }

            //Creating Binding Update
            codeBuilder.ComplementaryCode.AddLine("var flexmonsterPath = managerAuth.META_ROOT + managerAuth.META_MODULE_ID + '/lib/flexmonster/';");
            codeBuilder.ComplementaryCode.AddLine("var pivot = null;");
            codeBuilder.ComplementaryCode.AddLine("var arrayData = [];");
            codeBuilder.ComplementaryCode.AddLine("var currentStatus = '';");
            codeBuilder.ComplementaryCode.AddLine("var currentPage = undefined;");
            codeBuilder.ComplementaryCode.AddLine("var app = require('durandal/app');");
            codeBuilder.ComplementaryCode.AddLine("var jEntitySearchPivotRelationship = '';");

            if (parentRecord != "vm")
            {
                codeBuilder.ComplementaryCode.AddLine("var dataSourceIsLoaded = function() {");
                codeBuilder.ComplementaryCode.AddLine("    var isLoaded = false;");
                codeBuilder.ComplementaryCode.AddLine("    try {");
                codeBuilder.ComplementaryCode.AddLine("        isLoaded = (" + parentRecord + "." + dataView + "IsLoaded === true || " + parentRecord + "." + dataView + "List().length > 0);");
                codeBuilder.ComplementaryCode.AddLine("    }");
                codeBuilder.ComplementaryCode.AddLine("    catch (e) {");
                codeBuilder.ComplementaryCode.AddLine("        isLoaded = true;");
                codeBuilder.ComplementaryCode.AddLine("    }");
                codeBuilder.ComplementaryCode.AddLine("    return isLoaded;");
                codeBuilder.ComplementaryCode.AddLine("};");
            }

            codeBuilder.ComplementaryCode.AddLine("var getVisibleColumns = function() {");
            if (container.IsLinqSelectionControl)
            {
                codeBuilder.ComplementaryCode.AddLine("return Flexmonster.getVisibleRowsColumns(pivot)");
            }
            else
                codeBuilder.ComplementaryCode.AddLine("   return '';");

            codeBuilder.ComplementaryCode.AddLine("};");

            codeBuilder.ComplementaryCode.AddLine("var getStructure = function () {");
            codeBuilder.ComplementaryCode.AddLine("    var structure = {};");
            foreach (var item in controls)
            {
                var controlName = item.BindingPath.Right(".");
                var dateType = (item.DataType == "DateTime" || item.DataType == "System.DateTime" || item.DataType == "System.Nullable<System.DateTime>") ? "date " : string.Empty;
                var layoutDisplayName = this.ViewModelName + "_" + item.GetControlName(item.GetPrefix()) + "_pivot";

                codeBuilder.ComplementaryCode.AddLine("    if (vm.getLayoutVisible('" + layoutDisplayName + "'))");
                codeBuilder.ComplementaryCode.AddLine("    {");

                if (!item.IsMeasure)
                {
                    if (item.Group.IsNullOrEmpty())
                        codeBuilder.ComplementaryCode.AddLine("         structure." + controlName + " = { type:'" + dateType + "string', caption: vm.getLayoutDisplayName('" + layoutDisplayName + "'), dimensionUniqueName: vm.getDimensionUniqueName('" + this.ViewModelName + "_" + item.GetControlName(item.GetPrefix()) + "_pivot')};");
                    else
                        codeBuilder.ComplementaryCode.AddLine("         structure." + controlName + " = { type:'" + dateType + "string', caption: vm.getLayoutDisplayName('" + layoutDisplayName + "'), dimensionUniqueName: vm.getDimensionUniqueName('" + this.ViewModelName + "_" + item.GetControlName(item.GetPrefix()) + "_pivot')};");
                }
                else if (item.MeasureFormula.IsNullOrEmpty())
                {
                    if (item.Group.IsNullOrEmpty())
                        codeBuilder.ComplementaryCode.AddLine("         structure." + controlName + " = { type:'number', caption: vm.getLayoutDisplayName('" + layoutDisplayName + "') }; ");
                    else
                        codeBuilder.ComplementaryCode.AddLine("         structure." + controlName + " = { type:'number', caption: vm.getLayoutDisplayName('" + layoutDisplayName + "'), dimensionUniqueName: vm.getDimensionUniqueName('" + this.ViewModelName + "_" + item.GetControlName(item.GetPrefix()) + "_pivot')}; ");
                }
                codeBuilder.ComplementaryCode.AddLine("    }");
            }
            codeBuilder.ComplementaryCode.AddLine("    return structure;");
            codeBuilder.ComplementaryCode.AddLine("};");

            codeBuilder.ComplementaryCode.AddLine("var getPivotFormats = function () {");
            codeBuilder.ComplementaryCode.AddLine("    var formats = {};");
            codeBuilder.ComplementaryCode.AddLine();

            if (controls.Any(x => x.IsMeasure))
                controls.Where(x => x.IsMeasure).Foreach(x =>
                {

                    codeBuilder.ComplementaryCode.AddLine("     formats." + x.BindingPath.Right(".") + " = {");

                    if (!x.DataFormatString.IsNullOrEmpty())
                    {
                        var dataFormat = x.DataFormatString.Trim().ToUpper();

                        if (dataFormat.StartsWith("C"))
                        {
                            codeBuilder.ComplementaryCode.AddLine("            currencySymbol: 'R$ ',");
                        }
                        else if (dataFormat.StartsWith("P"))
                        {
                            codeBuilder.ComplementaryCode.AddLine("            currencySymbol: '% ', ");
                            codeBuilder.ComplementaryCode.AddLine("            currencySymbolAlign : 'left',");
                        }

                        codeBuilder.ComplementaryCode.AddLine("            decimalPlaces : '" + dataFormat.Substring(1) + "',");
                    }

                    codeBuilder.ComplementaryCode.AddLine("            decimalSeparator: ',',");
                    codeBuilder.ComplementaryCode.AddLine("            thousandsSeparator: '.',");
                    codeBuilder.ComplementaryCode.AddLine("            name: '" + x.BindingPath.Right(".") + "'};");
                });

            codeBuilder.ComplementaryCode.AddLine("    return formats;");

            codeBuilder.ComplementaryCode.AddLine("};");

            codeBuilder.ComplementaryCode.AddLine("var itemsSource = { ");
            codeBuilder.ComplementaryCode.IncreaseIndent();
            codeBuilder.ComplementaryCode.AddLine("getVisibleColumns: getVisibleColumns, getStructure: getStructure, getPivotFormats: getPivotFormats, dataBind: function (commitData) {");
            codeBuilder.ComplementaryCode.IncreaseIndent();

            if (string.IsNullOrEmpty(container.PivotDataSource) || !container.PivotDataSource.ToLower().Equals("olap"))
            {
                /*Alterado dia 27/09/2017 - Paulo - Bug 43925:Tela não recarrega dados (refaz a consulta) ao mudar o layout do PIVOT; Corrigido tbm o limpar da pivot com dashboard*/
                if (parentRecord != "vm")
                {
                    codeBuilder.ComplementaryCode.AddLine("var currentRelation = " + parentRecord + ".GetJsWhereDetailRelationFor" + dataView + "();");
                    codeBuilder.ComplementaryCode.AddLine("if ($('#" + idElement + "').is(':visible')) {");
                    codeBuilder.ComplementaryCode.AddLine("     if (this.lastRelation == currentRelation && vm.isDashboardFilter && vm.status() == 'C') {");
                    codeBuilder.ComplementaryCode.AddLine("         arrayData = unwrapObservableArray(" + listBinding + ", vm);");
                    codeBuilder.ComplementaryCode.AddLine("         this.lastRelation = '';");
                    codeBuilder.ComplementaryCode.AddLine("     } ");
                    codeBuilder.ComplementaryCode.AddLine("     else if (this.lastRelation != currentRelation){");
                    codeBuilder.ComplementaryCode.AddLine("         this.lastRelation = currentRelation;");
                    codeBuilder.ComplementaryCode.AddLine("         currentStatus = vm.status();");
                    codeBuilder.ComplementaryCode.AddLine("         currentPage = vm.dataToolbar.currentPage();");
                    codeBuilder.ComplementaryCode.AddLine("         if (currentStatus && currentStatus.toLowerCase() == 'c') {");
                    codeBuilder.ComplementaryCode.AddLine("              jEntitySearchPivotRelationship = '';");
                    codeBuilder.ComplementaryCode.AddLine("         }");
                    codeBuilder.ComplementaryCode.AddLine("         if ((vm.status() != 'C' && vm.status() != 'I') && !dataSourceIsLoaded()) {");
                    codeBuilder.ComplementaryCode.AddLine("             " + parentRecord + ".fillDetails(false, '" + dataView + "');");
                    codeBuilder.ComplementaryCode.AddLine("             return;");
                    codeBuilder.ComplementaryCode.AddLine("         }");
                    codeBuilder.ComplementaryCode.AddLine("         if(" + this.GetConditionsToBinding(listBinding) + ") {");
                    codeBuilder.ComplementaryCode.AddLine("              arrayData = unwrapObservableArray(" + listBinding + ", vm);");
                    codeBuilder.ComplementaryCode.AddLine("              if (vm.status() == 'C') this.lastRelation = '';");
                    codeBuilder.ComplementaryCode.AddLine("         }");
                    codeBuilder.ComplementaryCode.AddLine("     }");
                    codeBuilder.ComplementaryCode.AddLine("     else {");
                    codeBuilder.ComplementaryCode.AddLine("         return;");
                    codeBuilder.ComplementaryCode.AddLine("     }");
                }
                else
                {
                    codeBuilder.ComplementaryCode.AddLine("if ($('#" + idElement + "').is(':visible')) {");
                    codeBuilder.ComplementaryCode.AddLine("     currentStatus = vm.status();");
                    codeBuilder.ComplementaryCode.AddLine("     currentPage = vm.dataToolbar.currentPage();");

                    codeBuilder.ComplementaryCode.AddLine("     if (currentStatus && currentStatus.toLowerCase() == 'c') {");
                    codeBuilder.ComplementaryCode.AddLine("          jEntitySearchPivotRelationship = '';");
                    codeBuilder.ComplementaryCode.AddLine("     }");
                    codeBuilder.ComplementaryCode.AddLine("if(" + this.GetConditionsToBinding(listBinding) + ") {");
                    codeBuilder.ComplementaryCode.AddLine("     arrayData = unwrapObservableArray(" + listBinding + ", vm);");
                    codeBuilder.ComplementaryCode.AddLine("}");
                }

                codeBuilder.ComplementaryCode.AddLine("if(pivot == null) {");
                codeBuilder.ComplementaryCode.AddLine("$('#" + idElement + " #fm-fields-view .fm-ui-btn:contains(\\'OK\\')')");
                codeBuilder.ComplementaryCode.AddLine("     .live('mouseup', function () {");

                if (container.CallServiceOkEvent)
                {
                    codeBuilder.ComplementaryCode.AddLine("             if (vm.status() == 'Q') {");
                    codeBuilder.ComplementaryCode.AddLine("                setTimeout(function () {");
                    codeBuilder.ComplementaryCode.AddLine("                    var recall = false;");

                    codeBuilder.ComplementaryCode.AddLine("                    var laterContext = getVisibleRowsColumns().split(',');");

                    codeBuilder.ComplementaryCode.AddLine("                    laterContext.forEach(function (previousItem) {");
                    codeBuilder.ComplementaryCode.AddLine("                        if (!recall) recall = ((!arrayData.length) || (!arrayData[0][previousItem]));");
                    codeBuilder.ComplementaryCode.AddLine("                    });");
                    codeBuilder.ComplementaryCode.AddLine("                    if (recall) {");

                    if (parentRecord != "vm")
                    {
                        codeBuilder.ComplementaryCode.AddLine("                        vm.currentDataItem().fillDetails(true, '" + this.GetModelName(controls.First().BindingPath) + "');");
                    }
                    else
                    {
                        codeBuilder.ComplementaryCode.AddLine("                        vm.dataToolbar.clear();");
                        codeBuilder.ComplementaryCode.AddLine("                        vm.dataToolbar.query();");
                    }

                    codeBuilder.ComplementaryCode.AddLine("                    }");
                    codeBuilder.ComplementaryCode.AddLine("                  }, 1);");
                    codeBuilder.ComplementaryCode.AddLine("               }");
                }
                codeBuilder.ComplementaryCode.AddLine(" });");
                codeBuilder.ComplementaryCode.AddLine();
                codeBuilder.ComplementaryCode.AddLine("    $('#" + idElement + " #fm-toolbar-row .fm-ui-btn:contains(\\'OK\\')')");
                codeBuilder.ComplementaryCode.AddLine("       .live('mouseup', function () {");
                codeBuilder.ComplementaryCode.AddLine("           setTimeout(function () { filterPivotRelationship() }, 1);");
                codeBuilder.ComplementaryCode.AddLine("    });");
                codeBuilder.ComplementaryCode.AddLine("}");
                codeBuilder.ComplementaryCode.AddLine();

                string viewType = !container.PivotViewType.IsNullOrEmpty() ? container.PivotViewType.ToLower() : "grid";

                codeBuilder.ComplementaryCode.AddLine("var pivotContext = { rows: [], columns: [], pages: [], measures: [], options: { viewType: \"" + viewType + "\"" + (viewType.Contains("charts") ? ", chart: { type: \"" + container.PivotChartTypeGridChart + "\"" + (viewType.Contains("grid_charts") ? ", position:\"" + container.PivotChartPosition +"\"" : "")  + "}" : "") +" }, formats: [], conditions: [], report: null };");
                codeBuilder.ComplementaryCode.AddLine();

                codeBuilder.ComplementaryCode.AddLine("var addMeasuresFormulas = function () {");

                controls.Where(x => x.IsMeasure && !x.MeasureFormula.IsNullOrEmpty()).Foreach(x =>
                {
                    codeBuilder.ComplementaryCode.AddLine("           pivot.addMeasure({");
                    codeBuilder.ComplementaryCode.AddLine("               active: true,");
                    codeBuilder.ComplementaryCode.AddLine("               calculated: true,");
                    codeBuilder.ComplementaryCode.AddLine("               name: '" + x.DisplayName + "',");
                    codeBuilder.ComplementaryCode.AddLine("               caption: '" + x.DisplayName + "',");
                    codeBuilder.ComplementaryCode.AddLine("               uniqueName: '" + x.BindingPath.Right(".") + "',");
                    codeBuilder.ComplementaryCode.AddLine("               originalCaption: '" + x.BindingPath.Right(".") + "',");
                    codeBuilder.ComplementaryCode.AddLine("               aggregation: '" + x.AggregationFunction.ToLower() + "',");
                    codeBuilder.ComplementaryCode.AddLine("               formula: '" + this.ParseMeasureFormula(x.MeasureFormula, x.AggregationFunction, controls) + "'");
                    codeBuilder.ComplementaryCode.AddLine("           });");
                });
                codeBuilder.ComplementaryCode.AddLine("};");
                codeBuilder.ComplementaryCode.AddLine();

                #region Method updateData
                codeBuilder.ComplementaryCode.AddLine("var updateData = function() { ");

                var objectCollection = controls.Where(x => x.MeasureFormula.IsNullOrEmpty()).ToDictionary
                (
                    key => key.BindingPath.Right("."),
                    value =>
                    {
                        if (value.IsMeasure)
                            return string.Format("isNullOrEmpty(item.{0}) ? 0 : item.{0}", value.BindingPath.Right("."));

                        else if (value.DataType == "DateTime" || value.DataType == "System.DateTime" || value.DataType == "System.Nullable<System.DateTime>")
                            return string.Format("(isNullOrEmpty(item.{0}) ? '' : Globalize.format(getUTCDate(item.{0}), 'MM/dd/yyyy'))", value.BindingPath.Right("."));

                        else if (!string.IsNullOrEmpty(value.DomainName))
                            return string.Format("(isNullOrEmpty(item.{0}Name) ? '' : item.{0}Name.toString())", value.BindingPath.Right("."));

                        else
                            return string.Format("(isNullOrEmpty(item.{0}) ? '' : item.{0}.toString())", value.BindingPath.Right("."));
                    }
                );


                codeBuilder.ComplementaryCode.AddLine("    var data = arrayData.map(function(item){");
                codeBuilder.ComplementaryCode.AddLine("        return {" + string.Join(", ", objectCollection.Select(x => string.Format("'{0}': {1}", x.Key, x.Value))) + "};");
                codeBuilder.ComplementaryCode.AddLine("    });");

                codeBuilder.ComplementaryCode.AddLine();
                codeBuilder.ComplementaryCode.AddLine("    var structure = getStructure();");
                codeBuilder.ComplementaryCode.AddLine();
                codeBuilder.ComplementaryCode.AddLine("    pivot.updateData({ data: [structure].concat(data) });");
                codeBuilder.ComplementaryCode.AddLine("};");
                codeBuilder.ComplementaryCode.AddLine();
                #endregion


                codeBuilder.ComplementaryCode.AddLine("var getFormats = function () {");
                codeBuilder.ComplementaryCode.AddLine("    var measuresFormat = getMeasureFormats();");
                codeBuilder.ComplementaryCode.AddLine("    var measuresCalculatedFormat = Flexmonster.getMeasureCalculated(pivot);");
                codeBuilder.ComplementaryCode.AddLine();
                codeBuilder.ComplementaryCode.AddLine("    return measuresFormat.concat(measuresCalculatedFormat);");
                codeBuilder.ComplementaryCode.AddLine("};");
                codeBuilder.ComplementaryCode.AddLine();

                codeBuilder.ComplementaryCode.AddLine("var getMeasureFormats = function() {");
                codeBuilder.ComplementaryCode.AddLine("    var formatMeasures = [];");

                if (controls.Any(x => x.IsMeasure))
                    controls.Where(x => x.IsMeasure).Foreach(x =>
                    {
                        codeBuilder.ComplementaryCode.AddLine("    formatMeasures.push({ name: '" + x.BindingPath.Right(".") + "',  current: pivot.getFormat('" + x.BindingPath.Right(".") + "') });");
                    });
                codeBuilder.ComplementaryCode.AddLine();
                codeBuilder.ComplementaryCode.AddLine("    return formatMeasures;");
                codeBuilder.ComplementaryCode.AddLine("};");
                codeBuilder.ComplementaryCode.AddLine();

                codeBuilder.ComplementaryCode.AddLine("var getAllConditions = function () {");
                codeBuilder.ComplementaryCode.AddLine("    return pivot.getAllConditions();");
                codeBuilder.ComplementaryCode.AddLine("};");
                codeBuilder.ComplementaryCode.AddLine();

                codeBuilder.ComplementaryCode.AddLine("var setConditions = function () {");
                codeBuilder.ComplementaryCode.AddLine("    if (pivotContext.conditions.length) {");
                codeBuilder.ComplementaryCode.AddLine("        pivotContext.conditions.forEach(function (item) {");
                codeBuilder.ComplementaryCode.AddLine("            pivot.addCondition(item);");
                codeBuilder.ComplementaryCode.AddLine("        });");
                codeBuilder.ComplementaryCode.AddLine("    }");
                codeBuilder.ComplementaryCode.AddLine("};");

                codeBuilder.ComplementaryCode.AddLine();
                codeBuilder.ComplementaryCode.AddLine("var setFormat = function () {");
                codeBuilder.ComplementaryCode.AddLine("    if (pivotContext.formats.length) {");
                codeBuilder.ComplementaryCode.AddLine("        pivotContext.formats.forEach(function (item) {");
                codeBuilder.ComplementaryCode.AddLine("            pivot.setFormat(item.current, item.name);");
                codeBuilder.ComplementaryCode.AddLine("        });");
                codeBuilder.ComplementaryCode.AddLine("    } else {");

                codeBuilder.ComplementaryCode.AddLine("         var formats = getPivotFormats();");
                codeBuilder.ComplementaryCode.AddLine();
                codeBuilder.ComplementaryCode.AddLine("         Object.keys(formats).forEach(function (key) {");
                codeBuilder.ComplementaryCode.AddLine("             pivot.setFormat(formats[key], key);");
                codeBuilder.ComplementaryCode.AddLine("         });");
                codeBuilder.ComplementaryCode.AddLine("     }");
                codeBuilder.ComplementaryCode.AddLine("};");

                codeBuilder.ComplementaryCode.AddLine();

                codeBuilder.ComplementaryCode.AddLine("var setSlice = function (isCreating) {");
                codeBuilder.ComplementaryCode.AddLine("     if (!isCreating){");
                codeBuilder.ComplementaryCode.AddLine("         var slice = {};");
                #region setting Slice
                //begin
                string eixoDimension = string.Empty;
                bool hasPutMeasure = false;

                if (!container.PivotEixoMeasure.IsNullOrEmpty() && !container.PivotEixoMeasure.ToLower().Contains("default"))
                {
                    string eixoMesure = container.PivotEixoMeasure.ToLower();
                    eixoDimension = container.PivotEixoMeasure.ToLower();
                    Func<string, string> _getUniqueName = (textToSplit) =>
                    {
                        if (textToSplit.IsNullOrEmpty()) return "";
                        var list = textToSplit.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(i => "{ uniqueName: '" + i + "' }");
                        return string.Join(",", list);
                    };
                    if (!container.PivotDimensionsRowsCustom.IsNullOrEmpty())
                    {
                        codeBuilder.ComplementaryCode.AddLine("         slice.rows = [{0}];", _getUniqueName(container.PivotDimensionsRowsCustom));
                    }
                    else if (eixoDimension == "rows")
                    {
                        codeBuilder.ComplementaryCode.AddLine("         slice." + eixoMesure + " = [{ uniqueName: '[Measures]' }];");
                        hasPutMeasure = true;
                    }

                    if (!container.PivotDimensionsColsCustom.IsNullOrEmpty())
                    {
                        codeBuilder.ComplementaryCode.AddLine("         slice.columns = [{0}];", _getUniqueName(container.PivotDimensionsColsCustom));
                    }
                    else if (eixoDimension == "columns")
                    {
                        codeBuilder.ComplementaryCode.AddLine("         slice." + eixoMesure + " = [{ uniqueName: '[Measures]' }];");
                        hasPutMeasure = true;
                    }

                    if (!container.PivotMeasuresCustom.IsNullOrEmpty())
                    {
                        codeBuilder.ComplementaryCode.AddLine("         slice.measures = [{0}];", _getUniqueName(container.PivotMeasuresCustom));
                    }
                }

                if (!container.PivotEixoMeasure.IsNullOrEmpty() && !container.PivotEixoMeasure.ToLower().Contains("default"))
                {
                    if (!hasPutMeasure)
                    {
                        if (eixoDimension == "rows")
                            codeBuilder.ComplementaryCode.AddLine("         slice.rows.push({uniqueName: '[Measures]'});");
                        else
                            codeBuilder.ComplementaryCode.AddLine("         slice.columns.push({uniqueName: '[Measures]'});");
                    }
                }

                if (!container.PivotTopData.IsNullOrEmpty())
                    codeBuilder.ComplementaryCode.AddLine("         pivot.setTopX(" + container.PivotTopData + ");");

                codeBuilder.ComplementaryCode.AddLine("         if (!isNull(slice)) pivot.runQuery(slice);");
                #endregion
                codeBuilder.ComplementaryCode.AddLine("     } else if (pivotContext.rows.length || pivotContext.columns.length || pivotContext.measures.length || pivotContext.pages.length) {");
                codeBuilder.ComplementaryCode.AddLine("         pivot.runQuery(pivotContext);");
                codeBuilder.ComplementaryCode.AddLine("     }");
                codeBuilder.ComplementaryCode.AddLine("};");
                codeBuilder.ComplementaryCode.AddLine();

                codeBuilder.ComplementaryCode.AddLine("var setOptions = function () {");
                codeBuilder.ComplementaryCode.AddLine("    if (pivotContext.options != null) {");
                codeBuilder.ComplementaryCode.AddLine("         pivot.setOptions(pivotContext.options);");
                codeBuilder.ComplementaryCode.AddLine("    }");
                codeBuilder.ComplementaryCode.AddLine("};");
                codeBuilder.ComplementaryCode.AddLine();

                codeBuilder.ComplementaryCode.AddLine("var setFilterByCell = function (filters, format) {");
                codeBuilder.ComplementaryCode.AddLine("    if (format.length)");
                codeBuilder.ComplementaryCode.AddLine("        format.forEach(function (item) {");
                codeBuilder.ComplementaryCode.AddLine("            if (!filters.some(function (filter) { return item.hierarchyUniqueName == filter.key }))");
                codeBuilder.ComplementaryCode.AddLine("                filters.push({");
                codeBuilder.ComplementaryCode.AddLine("                    negation: false,");
                codeBuilder.ComplementaryCode.AddLine("                    key: item.hierarchyUniqueName,");
                if (container.PivotRelationshipQuery)
                    codeBuilder.ComplementaryCode.AddLine("                    values: [item.caption],");
                else
                    codeBuilder.ComplementaryCode.AddLine("                    values: [item.hierarchyUniqueName +'.['+ item.caption + ']'],");
                codeBuilder.ComplementaryCode.AddLine("                });");
                codeBuilder.ComplementaryCode.AddLine("        });");
                codeBuilder.ComplementaryCode.AddLine("};");
                codeBuilder.ComplementaryCode.AddLine();

                codeBuilder.ComplementaryCode.AddLine("var setFilterByReport = function (filters, reportItem) {");
                codeBuilder.ComplementaryCode.AddLine("    if (reportItem.length)");
                codeBuilder.ComplementaryCode.AddLine("        reportItem.forEach(function (item) {");
                codeBuilder.ComplementaryCode.AddLine("            if (item.filter && item.filter.members.length && !filters.some(function (filter) { return item.uniqueName == filter.key })) {");
                if (container.PivotRelationshipQuery)
                {
                    codeBuilder.ComplementaryCode.AddLine("                var currentItem = {");
                    codeBuilder.ComplementaryCode.AddLine("                    values: [],");
                    codeBuilder.ComplementaryCode.AddLine("                    key: item.uniqueName,");
                    codeBuilder.ComplementaryCode.AddLine("                    negation: item.filter.negation,");
                    codeBuilder.ComplementaryCode.AddLine("                };");
                    codeBuilder.ComplementaryCode.AddLine("                item.filter.members.forEach(function (filter) {");
                    codeBuilder.ComplementaryCode.AddLine("                    var value = filter.split('.')[1].replace('[', '').replace(']', '');");
                    codeBuilder.ComplementaryCode.AddLine("                    currentItem.values.push(value);");
                    codeBuilder.ComplementaryCode.AddLine("                });");
                }
                else
                {
                    codeBuilder.ComplementaryCode.AddLine("                var currentItem = {");
                    codeBuilder.ComplementaryCode.AddLine("                    values: item.filter.members,");
                    codeBuilder.ComplementaryCode.AddLine("                    key: item.uniqueName,");
                    codeBuilder.ComplementaryCode.AddLine("                    negation: item.filter.negation,");
                    codeBuilder.ComplementaryCode.AddLine("                };");
                }
                codeBuilder.ComplementaryCode.AddLine("                filters.push(currentItem);");
                codeBuilder.ComplementaryCode.AddLine("            }");
                codeBuilder.ComplementaryCode.AddLine("        });");
                codeBuilder.ComplementaryCode.AddLine("};");
                codeBuilder.ComplementaryCode.AddLine();

                codeBuilder.ComplementaryCode.AddLine("var parseFilters = function (cell) {");
                codeBuilder.ComplementaryCode.AddLine("    var filterItems = [];");
                codeBuilder.ComplementaryCode.AddLine("    var report = pivot.getReport();");
                codeBuilder.ComplementaryCode.AddLine("    if (cell) {");
                codeBuilder.ComplementaryCode.AddLine("        setFilterByCell(filterItems, cell.rows);");
                codeBuilder.ComplementaryCode.AddLine("        setFilterByCell(filterItems, cell.columns);");
                codeBuilder.ComplementaryCode.AddLine("    }");
                codeBuilder.ComplementaryCode.AddLine("    setFilterByReport(filterItems, report.slice.rows);");
                codeBuilder.ComplementaryCode.AddLine("    setFilterByReport(filterItems, report.slice.pages ? report.slice.pages : []);");
                codeBuilder.ComplementaryCode.AddLine("    setFilterByReport(filterItems, report.slice.columns);");
                codeBuilder.ComplementaryCode.AddLine("    return filterItems;");
                codeBuilder.ComplementaryCode.AddLine("};");
                codeBuilder.ComplementaryCode.AddLine();

                codeBuilder.ComplementaryCode.AddLine("var filterPivotRelationship = function (cell) {");
                codeBuilder.ComplementaryCode.AddLine("   if ((cell && cell.type != 'value') || isNaN(cell.value)) return false;");
                codeBuilder.ComplementaryCode.AddLine("   var dataContext = vm.getDataContext();");
                codeBuilder.ComplementaryCode.AddLine("   var dataFilter = parseFilters(cell);");
                codeBuilder.ComplementaryCode.AddLine("   var jEntitySearch = Flexmonster.parsejEntitySearch(dataFilter);");
                codeBuilder.ComplementaryCode.AddLine("   if (!jEntitySearchPivotRelationship || jEntitySearchPivotRelationship != jEntitySearch) {");
                codeBuilder.ComplementaryCode.AddLine("       jEntitySearchPivotRelationship = jEntitySearch;");
                codeBuilder.ComplementaryCode.AddLine("       vm.showProcessing('Pesquisando informações...');");
                codeBuilder.ComplementaryCode.AddLine("       if (arrayData && arrayData.length > 0) {");
                codeBuilder.ComplementaryCode.AddLine("           arrayData[0].fillDetails(true, '', true, true, function () {");
                codeBuilder.ComplementaryCode.AddLine("               vm.closeProcessing();");
                codeBuilder.ComplementaryCode.AddLine("           }, jEntitySearch);");
                codeBuilder.ComplementaryCode.AddLine("       }");
                codeBuilder.ComplementaryCode.AddLine("   }");
                codeBuilder.ComplementaryCode.AddLine("};");
                codeBuilder.ComplementaryCode.AddLine();

                if (!container.PivotFileLayout.IsNullOrEmpty() || container.PivotFileLayout != "(Nenhum)")
                {
                    codeBuilder.ComplementaryCode.AddLine("var setLayouts = function () {");
                    codeBuilder.ComplementaryCode.AddLine("     vm.layoutFiles.forEach(function(file) {");
                    codeBuilder.ComplementaryCode.AddLine(string.Format("         if (file.selected && (file.layoutFullName.indexOf('.xml') > 0 || file.layoutFullName.indexOf('.json') > 0) && file.layoutFullName.indexOf('{0}') > 0)", container.GetControlName("")));
                    codeBuilder.ComplementaryCode.AddLine("             pivot.load(file.layoutFullName);");
                    codeBuilder.ComplementaryCode.AddLine("     });");
                    codeBuilder.ComplementaryCode.AddLine("};");
                }
                codeBuilder.ComplementaryCode.AddLine();

                #region event onPivotReady
                codeBuilder.ComplementaryCode.AddLine("var onPivotReady = function () {");
                codeBuilder.ComplementaryCode.AddLine("     pivot.clear();");
                codeBuilder.ComplementaryCode.AddLine("     updateData();");
                codeBuilder.ComplementaryCode.AddLine("     addMeasuresFormulas();");

                codeBuilder.ComplementaryCode.AddLine("     setTimeout(function() {");

                if (!container.PivotFileLayout.IsNullOrEmpty() || container.PivotFileLayout != "(Nenhum)")
                {
                    codeBuilder.ComplementaryCode.AddLine("         setLayouts();");
                }

                codeBuilder.ComplementaryCode.AddLine("         setOptions();");

                if (container.IsPivotExpanded)
                    codeBuilder.ComplementaryCode.AddLine("         pivot.expandAllData(true);");

                codeBuilder.ComplementaryCode.AddLine("         setSlice(true);");
                codeBuilder.ComplementaryCode.AddLine("         setFormat();");
                codeBuilder.ComplementaryCode.AddLine("         pivot.refresh();");
                codeBuilder.ComplementaryCode.AddLine("         if (typeof vm.OnchangePivotLayoutOnLoad === 'function') {");
                codeBuilder.ComplementaryCode.AddLine("             vm.OnchangePivotLayoutOnLoad(pivot);");
                codeBuilder.ComplementaryCode.AddLine("         }");

                codeBuilder.ComplementaryCode.AddLine("     }, 500);");

                codeBuilder.ComplementaryCode.AddLine("};");
                codeBuilder.ComplementaryCode.AddLine();
                #endregion
                #region event onBeforeToolbarCreated
                codeBuilder.ComplementaryCode.AddLine("var onBeforeToolbarCreated = function (toolbarInstance) {");
                codeBuilder.ComplementaryCode.AddLine("    Flexmonster.initLinxToolbar({");
                codeBuilder.ComplementaryCode.AddLine("        toolbarInstance: toolbarInstance,");
                codeBuilder.ComplementaryCode.AddLine("        vm: vm,");
                codeBuilder.ComplementaryCode.AddLine("        pivotName: '{0}',", container.GetControlName(""));
                codeBuilder.ComplementaryCode.AddLine("        pivotAdapterLayout: '{0}',", !container.EntityData.IsNullOrEmpty() ? container.EntityData : container.PivotDataSource.ToLower() + container.PivotCube);
                codeBuilder.ComplementaryCode.AddLine("        tb_layoutToolbar: {0},", container.IsLayoutToolbar.ToString().ToLower());
                codeBuilder.ComplementaryCode.AddLine("        tb_FullScreen: {0},", container.PivotFullScreen.ToString().ToLower());
                codeBuilder.ComplementaryCode.AddLine("        tb_ToggleView: {0},", container.PivotToggleView.ToString().ToLower());
                codeBuilder.ComplementaryCode.AddLine("        tb_OpenReport: {0}", container.PivotOpenReport.ToString().ToLower());
                codeBuilder.ComplementaryCode.AddLine("    });");
                codeBuilder.ComplementaryCode.AddLine("}; ");
                codeBuilder.ComplementaryCode.AddLine();
                #endregion
                #region event onClickPivotCell
                codeBuilder.ComplementaryCode.AddLine("var onCellClick = function (cell) {");
                codeBuilder.ComplementaryCode.AddLine("    filterPivotRelationship(cell);");
                codeBuilder.ComplementaryCode.AddLine("};");
                codeBuilder.ComplementaryCode.AddLine();
                #endregion

                #region updatePivot
                codeBuilder.ComplementaryCode.AddLine("var updatePivot = function () {");
                codeBuilder.ComplementaryCode.AddLine("    pivotContext.rows = pivot.getRows();");
                codeBuilder.ComplementaryCode.AddLine("    pivotContext.pages = pivot.getPages();");
                codeBuilder.ComplementaryCode.AddLine("    pivotContext.columns = pivot.getColumns();");
                codeBuilder.ComplementaryCode.AddLine("    pivotContext.options = pivot.getOptions();");
                codeBuilder.ComplementaryCode.AddLine("    pivotContext.measures = pivot.getMeasures();");
                codeBuilder.ComplementaryCode.AddLine("    pivotContext.formats = getFormats();");
                codeBuilder.ComplementaryCode.AddLine("    pivotContext.report = pivot.getReport();");
                codeBuilder.ComplementaryCode.AddLine("    pivotContext.conditions = getAllConditions();");
                codeBuilder.ComplementaryCode.AddLine();
                codeBuilder.ComplementaryCode.AddLine("    updateData();");
                codeBuilder.ComplementaryCode.AddLine("    addMeasuresFormulas();");
                codeBuilder.ComplementaryCode.AddLine("    setOptions();");

                if (container.IsPivotExpanded)
                    codeBuilder.ComplementaryCode.AddLine("    pivot.expandAllData(true);");

                // codeBuilder.ComplementaryCode.AddLine("    setSlice(false);");
                codeBuilder.ComplementaryCode.AddLine("    setFormat();");
                codeBuilder.ComplementaryCode.AddLine("    setConditions();");
                codeBuilder.ComplementaryCode.AddLine("    pivot.refresh();");
                codeBuilder.ComplementaryCode.AddLine("    pivot.closeFieldsList();");
                codeBuilder.ComplementaryCode.AddLine("};");
                codeBuilder.ComplementaryCode.AddLine();
                #endregion

                codeBuilder.ComplementaryCode.AddLine("var setLanguage = function(lang) {");
                codeBuilder.ComplementaryCode.AddLine("    var idioma = lang;");
                codeBuilder.ComplementaryCode.AddLine("    if (idioma.indexOf('pt-br') >= 0)");
                codeBuilder.ComplementaryCode.AddLine("        return;");
                codeBuilder.ComplementaryCode.AddLine("    else {");
                codeBuilder.ComplementaryCode.AddLine("        try {");
                codeBuilder.ComplementaryCode.AddLine("            var nameFileLang = managerAuth.META_ROOT + managerAuth.META_MODULE_ID + \"/lib/flexmonster/toolbar/language_toolbar/\" + idioma + \".js\";");
                codeBuilder.ComplementaryCode.AddLine();
                codeBuilder.ComplementaryCode.AddLine("            var fRef = document.createElement('script');");
                codeBuilder.ComplementaryCode.AddLine("            fRef.setAttribute(\"type\", \"text/javascript\");");
                codeBuilder.ComplementaryCode.AddLine("            fRef.setAttribute(\"src\", nameFileLang);");
                codeBuilder.ComplementaryCode.AddLine("            document.getElementsByTagName(\"head\")[0].appendChild(fRef);");
                codeBuilder.ComplementaryCode.AddLine("        }");
                codeBuilder.ComplementaryCode.AddLine("        catch (e)");
                codeBuilder.ComplementaryCode.AddLine("        {");
                codeBuilder.ComplementaryCode.AddLine("            console.log(\"Arquivo de tradução não encontrado[\" + idioma + \"].\");");
                codeBuilder.ComplementaryCode.AddLine("        }");
                codeBuilder.ComplementaryCode.AddLine("    }");
                codeBuilder.ComplementaryCode.AddLine("};");

                codeBuilder.ComplementaryCode.AddLine();

                codeBuilder.ComplementaryCode.AddLine("var timeout = 50;");
                codeBuilder.ComplementaryCode.AddLine("var createInstance = function () {");
                codeBuilder.ComplementaryCode.AddLine("     var idioma = vm.common.getIdioma();");
                codeBuilder.ComplementaryCode.AddLine("     var createPivotInstance = function() {");
                codeBuilder.ComplementaryCode.AddLine("         pivot = new Flexmonster({");
                codeBuilder.ComplementaryCode.AddLine("             container: '{0}',", idElement);
                codeBuilder.ComplementaryCode.AddLine("             componentFolder: flexmonsterPath,");
                codeBuilder.ComplementaryCode.AddLine("             report: flexmonsterPath + 'report_lang/report_' + idioma + '.json',");
                codeBuilder.ComplementaryCode.AddLine("             global: {");
                codeBuilder.ComplementaryCode.AddLine("                 localization: 'report_lang/loc_' + idioma + '.json'");
                codeBuilder.ComplementaryCode.AddLine("             },");
                codeBuilder.ComplementaryCode.AddLine("             toolbar: {0},", (!container.IsPivotReadOnly).ToString().ToLower());
                codeBuilder.ComplementaryCode.AddLine("             width: '100%',");
                codeBuilder.ComplementaryCode.AddLine("             height: {0},", GetPivotHeight(container));
                codeBuilder.ComplementaryCode.AddLine("             licenseKey: managerAuth.flexMonsterLicenseKey");
                codeBuilder.ComplementaryCode.AddLine("         });");
                codeBuilder.ComplementaryCode.AddLine("         pivot.on('cellclick', onCellClick);");
                codeBuilder.ComplementaryCode.AddLine("         pivot.on('ready', onPivotReady);");
                codeBuilder.ComplementaryCode.AddLine("         pivot.on('beforetoolbarcreated', onBeforeToolbarCreated);");
                codeBuilder.ComplementaryCode.AddLine("         vm.pivots.push({ container: '" + idElement + "', pivotName: '" + container.GetControlName("") + "', instance: pivot });");
                codeBuilder.ComplementaryCode.AddLine("     };");
                codeBuilder.ComplementaryCode.AddLine();
                codeBuilder.ComplementaryCode.AddLine("     if (idioma.indexOf('pt-br') >= 0) {");
                codeBuilder.ComplementaryCode.AddLine("         createPivotInstance();");
                codeBuilder.ComplementaryCode.AddLine("     } else {");
                codeBuilder.ComplementaryCode.AddLine("         setTimeout(function() {");
                codeBuilder.ComplementaryCode.AddLine("             timeout--;");
                codeBuilder.ComplementaryCode.AddLine("             if (typeof langPropsToolbar == \"function\" && Object.getOwnPropertyNames(langPropsToolbar()).length > 0) {");
                codeBuilder.ComplementaryCode.AddLine("                 if (langToolbar() == idioma) {");
                codeBuilder.ComplementaryCode.AddLine("                     return createPivotInstance(idioma);");
                codeBuilder.ComplementaryCode.AddLine("                 }");
                codeBuilder.ComplementaryCode.AddLine("                 else if (timeout > 0)");
                codeBuilder.ComplementaryCode.AddLine("                     createInstance();");
                codeBuilder.ComplementaryCode.AddLine("                 else {");
                codeBuilder.ComplementaryCode.AddLine("                     vm.common.saveIdioma(\"pt-br\");");
                codeBuilder.ComplementaryCode.AddLine("                     $(\"#cmbIdioma\").val(\"pt-br\");");
                codeBuilder.ComplementaryCode.AddLine("                     console.log(\"Erro ao carregar idioma[\" + idioma + \"]!\");");
                codeBuilder.ComplementaryCode.AddLine("                     return createPivotInstance();");
                codeBuilder.ComplementaryCode.AddLine("                 }");
                codeBuilder.ComplementaryCode.AddLine("             }");
                codeBuilder.ComplementaryCode.AddLine("             else if (timeout > 0)");
                codeBuilder.ComplementaryCode.AddLine("                 createInstance();");
                codeBuilder.ComplementaryCode.AddLine("             else {");
                codeBuilder.ComplementaryCode.AddLine("                 vm.common.saveIdioma(\"pt-br\");");
                codeBuilder.ComplementaryCode.AddLine("                 $(\"#cmbIdioma\").val(\"pt-br\");");
                codeBuilder.ComplementaryCode.AddLine("                 console.log(\"Erro ao carregar idioma[\" + idioma + \"]!\");");
                codeBuilder.ComplementaryCode.AddLine("                 return createPivotInstance();");
                codeBuilder.ComplementaryCode.AddLine("             }");
                codeBuilder.ComplementaryCode.AddLine("         }, 100);");
                codeBuilder.ComplementaryCode.AddLine("     }");
                codeBuilder.ComplementaryCode.AddLine("};");
                codeBuilder.ComplementaryCode.AddLine();

                codeBuilder.ComplementaryCode.AddLine("     try {");
                codeBuilder.ComplementaryCode.AddLine("         var idioma = vm.common.getIdioma();");
                codeBuilder.ComplementaryCode.AddLine("         if (idioma.indexOf('pt-br') < 0)");
                codeBuilder.ComplementaryCode.AddLine("             setLanguage(idioma);");
                codeBuilder.ComplementaryCode.AddLine("         if (pivot == null)");
                codeBuilder.ComplementaryCode.AddLine("             createInstance();");
                codeBuilder.ComplementaryCode.AddLine("         else");
                codeBuilder.ComplementaryCode.AddLine("             updatePivot();");
                codeBuilder.ComplementaryCode.AddLine("     }");
                codeBuilder.ComplementaryCode.AddLine("     catch (e) { }");
                codeBuilder.ComplementaryCode.AddLine("}");
            }
            else
            {
                codeBuilder.ComplementaryCode.AddLine("app.on('shell:brand:change').then(function () {");
                codeBuilder.ComplementaryCode.AddLine("    updatePivot();");
                codeBuilder.ComplementaryCode.AddLine("});");
                codeBuilder.ComplementaryCode.AddLine();

                codeBuilder.ComplementaryCode.AddLine("app.on('shell:customSearch:change').then(function () {");
                codeBuilder.ComplementaryCode.AddLine("    updatePivot();");
                codeBuilder.ComplementaryCode.AddLine("});");
                codeBuilder.ComplementaryCode.AddLine();

                if (!container.PivotFileLayout.IsNullOrEmpty() || container.PivotFileLayout != "(Nenhum)")
                {
                    codeBuilder.ComplementaryCode.AddLine("var setLayouts = function () {");
                    codeBuilder.ComplementaryCode.AddLine("    vm.layoutFiles && vm.layoutFiles.forEach(function(file) {");
                    codeBuilder.ComplementaryCode.AddLine("        if (file.selected && file.layoutFullName.indexOf('.json') > 0 && file.layoutFullName.indexOf('{0}') > 0)", container.GetControlName(""));
                    codeBuilder.ComplementaryCode.AddLine("            pivot.load(file.layoutFullName);");
                    codeBuilder.ComplementaryCode.AddLine("    });");
                    codeBuilder.ComplementaryCode.AddLine("};");
                    codeBuilder.ComplementaryCode.AddLine();
                }

                codeBuilder.ComplementaryCode.AddLine("var updatePivot = function () {");
                codeBuilder.ComplementaryCode.AddLine("    var report = pivot.getReport();");
                codeBuilder.ComplementaryCode.AddLine("    if(!report) report = {};");
                codeBuilder.ComplementaryCode.AddLine("    if(!report.dataSource) report.dataSource = {};");
                codeBuilder.ComplementaryCode.AddLine("    report.dataSource.proxyUrl = getProxyUrl();");
                codeBuilder.ComplementaryCode.AddLine("    pivot.setReport(report);");
                codeBuilder.ComplementaryCode.AddLine("};");
                codeBuilder.ComplementaryCode.AddLine();
                codeBuilder.ComplementaryCode.AddLine("var getAllowedBrands = function () {");
                codeBuilder.ComplementaryCode.AddLine("    var allowedBrands = '';");
                codeBuilder.ComplementaryCode.AddLine("    if (vm.brands) {");
                codeBuilder.ComplementaryCode.AddLine("        if (vm.brands.some(function (item) { return item.text.toLocaleLowerCase() == 'todas as redes' }))");
                codeBuilder.ComplementaryCode.AddLine("            allowedBrands = vm.brands.filter(function (item) { return item.text.toLocaleLowerCase() == 'todas as redes' })[0].id;");
                codeBuilder.ComplementaryCode.AddLine("        else");
                codeBuilder.ComplementaryCode.AddLine("            allowedBrands = vm.brands[0].id;");
                codeBuilder.ComplementaryCode.AddLine("    }");
                codeBuilder.ComplementaryCode.AddLine("    return allowedBrands;");
                codeBuilder.ComplementaryCode.AddLine("};");
                codeBuilder.ComplementaryCode.AddLine();

                codeBuilder.ComplementaryCode.AddLine("var getProxyUrl = function () {");
                codeBuilder.ComplementaryCode.AddLine("    var allowedBrands = getAllowedBrands();");
                codeBuilder.ComplementaryCode.AddLine("    var currentBrands = vm.getCurrentBrands && vm.getCurrentBrands();");
                codeBuilder.ComplementaryCode.AddLine("    var searchDefinition = encodeURIComponent(vm.dataToolbar.customSearchResult && vm.dataToolbar.customSearchResult.searchDefinition ? vm.dataToolbar.customSearchResult.searchDefinition : '');");
                codeBuilder.ComplementaryCode.AddLine();
                codeBuilder.ComplementaryCode.AddLine("    return managerAuth.serviceBus + '/api/olapproxy?currentBrands=' + (isNull(currentBrands) ? '0' : currentBrands) +");
                codeBuilder.ComplementaryCode.AddLine("        '&allowedBrands=' + allowedBrands + '&jEntitySearch=' + searchDefinition;");
                codeBuilder.ComplementaryCode.AddLine("};");
                codeBuilder.ComplementaryCode.AddLine();

                codeBuilder.ComplementaryCode.AddLine("var onPivotReady = function () {");
                if (container.IsPivotExpanded)
                    codeBuilder.ComplementaryCode.AddLine("    pivot.expandAllData(true);");
                codeBuilder.ComplementaryCode.AddLine("    pivot.connectTo({");
                codeBuilder.ComplementaryCode.AddLine("        dataSourceType: 'Microsoft Analysis Services',");
                codeBuilder.ComplementaryCode.AddLine("        proxyUrl: getProxyUrl(),");
                codeBuilder.ComplementaryCode.AddLine("        dataSourceInfo: 'Provider=MSOLAP; Data Source=extranet;',");
                codeBuilder.ComplementaryCode.AddLine("        catalog: '.',");
                codeBuilder.ComplementaryCode.AddLine("        cube: '" + (string.IsNullOrEmpty(container.PivotCube) ? "MODEL" : container.PivotCube.ToUpper()) + "'");
                codeBuilder.ComplementaryCode.AddLine("    });");
                codeBuilder.ComplementaryCode.AddLine();
                if (!container.PivotFileLayout.IsNullOrEmpty() || container.PivotFileLayout != "(Nenhum)")
                    codeBuilder.ComplementaryCode.AddLine("    setLayouts();");
                codeBuilder.ComplementaryCode.AddLine("};");

                var pivotHeight = ((container.PivotScope == PivotScope.TableAndChart) ? 700 : ((container.Height > 0) ? container.Height : 350));

                codeBuilder.ComplementaryCode.AddLine("var createInstance = function () {");
                codeBuilder.ComplementaryCode.AddLine("    var idioma = require('common').getIdioma();");
                codeBuilder.ComplementaryCode.AddLine("    pivot = new Flexmonster({");
                codeBuilder.ComplementaryCode.AddLine("        container: '{0}',", idElement);
                codeBuilder.ComplementaryCode.AddLine("        componentFolder: flexmonsterPath,");
                codeBuilder.ComplementaryCode.AddLine("        report: flexmonsterPath + 'report_lang/report_olap_' + idioma + '.json',");
                codeBuilder.ComplementaryCode.AddLine("             global: {");
                codeBuilder.ComplementaryCode.AddLine("                 localization: 'report_lang/loc_' + idioma + '.json'");
                codeBuilder.ComplementaryCode.AddLine("             },");
                codeBuilder.ComplementaryCode.AddLine("        toolbar: {0},", (!container.IsPivotReadOnly).ToString().ToLower());
                codeBuilder.ComplementaryCode.AddLine("        width: '100%',");
                codeBuilder.ComplementaryCode.AddLine("        height: {0},", pivotHeight);
                codeBuilder.ComplementaryCode.AddLine("        licenseKey: managerAuth.flexMonsterLicenseKey");
                codeBuilder.ComplementaryCode.AddLine("    });");
                codeBuilder.ComplementaryCode.AddLine("    pivot.on('ready', onPivotReady);");
                codeBuilder.ComplementaryCode.AddLine("    pivot.on('beforetoolbarcreated', function(toolbar){ });");

                codeBuilder.ComplementaryCode.AddLine("};");
                codeBuilder.ComplementaryCode.AddLine();

                codeBuilder.ComplementaryCode.AddLine("if(pivot == null) {");
                codeBuilder.ComplementaryCode.AddLine("    createInstance();");
                codeBuilder.ComplementaryCode.AddLine("}");
            }

            codeBuilder.ComplementaryCode.AddLine();
            codeBuilder.ComplementaryCode.DecreaseIndent();
            codeBuilder.ComplementaryCode.AddLine("}");
            codeBuilder.ComplementaryCode.DecreaseIndent();
            codeBuilder.ComplementaryCode.AddLine("};");
            codeBuilder.ComplementaryCode.AddLine("if (vm.addDataSource){ vm.addDataSource({ key: '" + idElement + "', name: '" + (binding.IsNullOrEmpty() ? "dataView" : binding.Right(".")) + "', itemsSource: itemsSource }); }");
            codeBuilder.ComplementaryCode.AddLine("else { itemsSource.dataBind(); }");
            codeBuilder.ComplementaryCode.DecreaseIndent();
            codeBuilder.ComplementaryCode.AddLine("}");
            #endregion
            codeBuilder.DecreaseIndent();
        }

        private int GetPivotHeight(LayoutContainer container)
        {
            var height = 0;
            if (container.Height > 0)
                height = container.Height;

            if (height == 0)
            {
                switch (container.GridHeight)
                {
                    case GridSizeHeight.Auto:
                    case GridSizeHeight.Small:
                        height = 350;
                        break;
                    case GridSizeHeight.Medium:
                        height = 500;
                        break;
                    case GridSizeHeight.Large:
                        height = 750;
                        break;
                    default:
                        height = 350;
                        break;
                }
            }
            return height;
        }

        private string ParseMeasureFormula(string measureFormula, string aggregationFunction, List<LayoutControlV2> controls)
        {
            var formula = measureFormula.Replace("_", "");
            var parameters = formula.Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);

            if (parameters != null && parameters.Length > 0)
            {
                foreach (var item in parameters)
                {
                    var currentProperty = controls.FirstOrDefault(x => (string.Compare(x.BindingPath.Right("."), item, true) == 0));

                    if (currentProperty != null)
                    {
                        var operation = string.Empty;

                        switch (aggregationFunction.ToLower())
                        {
                            case "avg": operation = "average"; break;
                            case "count": operation = "count"; break;
                            case "max": operation = "max"; break;
                            case "min": operation = "min"; break;
                            case "distinct count": operation = "distinctcount"; break;
                            case "product": operation = "product"; break;
                            case "porcent": operation = "porcent"; break;
                            case "porcent column": operation = "percentofcolumn"; break;
                            case "porcent row": operation = "percentofrow"; break;
                            case "index": operation = "index"; break;
                            case "sum":
                            default: operation = "sum"; break;
                        }
                        formula = formula.Replace("[" + item + "]", "" + operation + "(\\'" + currentProperty.BindingPath.Right(".") + "\\') ");
                    }
                }
            }

            return formula;
        }

        private string GetModelName(string bindingPath)
        {
            var modelName = string.Empty;

            var bindingParts = bindingPath.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

            if (bindingParts != null && bindingParts.Length > 0)
                modelName = bindingParts[bindingParts.Length - 2].Replace("PagedList", "");

            return modelName;
        }

        //private string GenerateMeasureFomula(string measureFormula)
        private string GenerateMeasureFomula(LayoutControlV2 control)
        {
            string script = string.Empty;

            if (!control.MeasureFormula.IsNullOrEmpty())
            {
                List<string> properties = new List<string>();
                control.MeasureFormula = MacroEngine.ReplaceMacros(control.MeasureFormula, false);
                var measureFormulaParts = control.MeasureFormula.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                var split = control.MeasureFormula.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (split.Length >= 2)
                {
                    script = "function(items, cellMetadata){ ";
                    //try
                    script += "try{ ";
                    for (int i = 1; i < split.Length; i++)
                    {
                        var propertyName = split[i].Trim();
                        properties.Add("sum" + propertyName);
                        script += string.Format("var sum{0} = sumPropertyAggregator(items, '{0}');", propertyName);
                    }

                    script += "return sumPropertyAggregatorFormat(eval(\"" + string.Format(split[0], properties.ToArray()) + "\")," + (control.DataType == "System.Decimal" ? "'0.00'" : "\'" + control.DataType + "\'") + " );";
                    //catch
                    script += "}catch(e){ messageBoxException(e);}";
                    script += "}";
                }
            }
            return script;
        }

        private string ToDimensions(string dimensions, string domains)
        {
            string result = String.Empty;
            if (!dimensions.IsNullOrEmpty())
            {
                foreach (var dim in dimensions.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    bool isDomain = domains.Contains(dim.Left("."));
                    result += (result.IsNullOrEmpty() ? String.Empty : ",") + "[" + dim.Extract("[", "].") + (isDomain ? "Name" : String.Empty) + "].[" + dim.Extract(".[", "]") + (isDomain ? "Name" : String.Empty) + "]";
                }
            }

            return result;
        }

        private string GetComboColumnSettings(List<LayoutControlV2> controls, string currentBinding, string idElement)
        {
            string columnSettings = String.Empty;

            foreach (var control in controls.Where(e => e.ClassName == "ComboBox"))
            {
                if (control.DomainName.IsNullOrEmpty() && !control.LookUpName.IsNullOrEmpty())
                {
                    columnSettings += (columnSettings.IsNullOrEmpty() ? String.Empty : ", ") + "{ columnKey: '" + control.BindingPath.Right(".") + "'";
                    columnSettings += ", editorType: 'combo', editorOptions: { mode: 'dropdown', dropDownOnFocus: true, ";
                    if (control.DataFormatString == "none") columnSettings += " format: 'none',";
                    columnSettings += " selectionChanged: function (evt, ui) {";
                    columnSettings += " if (evt.keyCode == undefined) {";
                    columnSettings += " vm.finalizeCombo(" + currentBinding + "(), (ui.items != null && ui.items.length > 0 ? ui.items[0].data : ''), '" + control.LookUpName + "');";
                    columnSettings += " if (typeof vm.OnPropertyChangeDataGrid === 'function') {";
                    columnSettings += " vm.OnPropertyChangeDataGrid('" + idElement + "', '" + control.BindingPath.Right(".") + "', (ui.oldItems != null ? ui.oldItems[0].value : ''), (ui.items != null && ui.items.length > 0 ? ui.items[0].value : ''));";
                    columnSettings += " }";
                    columnSettings += " }},";
                    columnSettings += " dropDownClosing: function (evt, ui) {";
                    columnSettings += " if (evt.keyCode == 13 || evt.keyCode == 32) {";
                    columnSettings += " vm.finalizeCombo(" + currentBinding + "(), (ui.owner._options.selectedData != null && ui.owner._options.selectedData.length > 0 ? ui.owner._options.selectedData[0] : ''), '" + control.LookUpName + "');";
                    columnSettings += " }";
                    columnSettings += " },";
                    columnSettings += " dataSource: vm.dataCombo.getItems('" + control.LookUpName + "', ''), textKey: '" + control.BindingPath.Right(".") + "', valueKey: '" + control.BindingPath.Right(".") + "', enableClearButton: " + control.IsNullable.ToString().ToLower() + ", inputName: '" + control.LookUpName + "', allowCustomValue: true, enableSelectionChangedUpdate: true, }";
                    columnSettings += "}";
                }
                else
                {
                    columnSettings += (columnSettings.IsNullOrEmpty() ? String.Empty : ", ") + "{ columnKey: '" + control.BindingPath.Right(".") + "'";
                    columnSettings += ", editorType: 'combo', editorOptions: { ";
                    columnSettings += "  selectionChanged: function (evt, ui) { ";
                    columnSettings += "  var val = null; ";
                    columnSettings += "  if (ui.items != null && ui.items.length > 0) { val = ui.items[0].data['id']; } ";
                    columnSettings += " updateEntity('" + control.BindingPath.Right(".") + "', val, false); }, ";
                    columnSettings += " mode: 'dropdown', dropDownOnFocus: true, ";
                    columnSettings += " dataSource: vm.dataDomains.getItems('" + control.DomainName + "', '" + (control.DomainFilterValues ?? "") + "'), ";
                    columnSettings += " textKey: 'name', valueKey: 'id', enableClearButton: " + control.IsNullable.ToString().ToLower() + " }";
                    columnSettings += "}";
                }

            }

            return columnSettings;
        }

        private string GetDatePickerColumnSettings(List<LayoutControlV2> controls, string idElement)
        {
            string columnSettings = String.Empty;
            string dateType = string.Empty;

            foreach (var control in controls.Where(e => e.ClassName == "DateTimeTextBox"))
            {
                if (control.DataFormatString.Contains("time"))
                    dateType = ", button: 'clear', dateDisplayFormat: '" + control.DataFormatString + "', dateInputFormat: '" + control.DataFormatString + "'";

                columnSettings += (columnSettings.IsNullOrEmpty() ? String.Empty : ", ") +
                    "{ columnKey: '" + control.BindingPath.Right(".") + "', editorType: 'datepicker', editorOptions: {valueChanged: function(evt, ui){if (typeof vm.OnPropertyChangeDataGrid === 'function') {vm.OnPropertyChangeDataGrid('" + idElement + "', '" + control.BindingPath.Right(".") + "', ui.oldValue, ui.value);}}, minValue: new Date(1900, 0, 1), datepickerOptions: { changeMonth: true, changeYear: true } " + dateType + " } }";
            }

            return columnSettings;
        }

        private string GetLookupColumnSettings(List<LayoutControlV2> controls, string idElement)
        {
            string columnSettings = String.Empty;

            foreach (var control in controls.Where(e => e.ClassName == "LookUpTextBox"))
            {
                var maxLength = GetMaxLengthNumeric(control);
                var maxValue = getMaxValueByType(control);
                columnSettings += (columnSettings.IsNullOrEmpty() ? "" : ", ") +
                    "{ columnKey: \"" + control.BindingPath.Right(".") + "\", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: \""
                    + control.LookUpName + "\", isNullable: "
                    + control.IsNullable.ToString().ToLower()
                    + ", custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:" + control.AllowMultiSelectionInSearch.ToString().ToLower() +
                    ", activateAutoComplete: " + control.EnableLookupAutoComplete.ToString().ToLower() +
                    ", autoCompleteMaxResults: " + (control.LookupAutoCompleteMaxResults == 0 ? 7 : control.LookupAutoCompleteMaxResults).ToString() +
                    ", validateOnClearState:" + control.ValidateOnClearState.ToString().ToLower() +
                    ", maxValue:" + maxValue +
                    ", maxLength: " + maxLength +
                    ", defaultValue: " + GetJSDefaultValueByType(control.DataType) + " } }";
            }

            return columnSettings;
        }

        private string GetJSDefaultValueByType(string dataType)
        {
            var defaultValue = "null";
            if (!dataType.Contains("Nullable<") && !dataType.Contains("?"))
            {
                dataType = dataType.RemoveNullDefinition();
                if (dataType.InList(new string[] { "byte", "int16", "int32", "int", "long", "short", "int64", "sbyte", "uint16", "uint32", "uint64", "single", "double", "decimal" }))
                    defaultValue = "0";
                else if (dataType.Contains("datetime"))
                    defaultValue = "getCurrentDate()";
                else if (dataType.Contains("bool"))
                    defaultValue = "false";
                else
                    defaultValue = "''";
            }

            return defaultValue;
        }

        private string GetLookupColumns(List<LayoutControlV2> controls)
        {
            string columns = String.Empty;

            foreach (var control in controls.Where(e => e.ClassName == "LookUpTextBox"))
            {
                columns += (columns.IsNullOrEmpty() ? "" : ",") + control.BindingPath.Right(".");
            }

            return (columns.IsNullOrEmpty() ? "" : "," + columns + ",");
        }

        private string GetSumariesColumnSettings(List<LayoutControlV2> controls, string idElement)
        {
            string columnSettings = String.Empty;

            foreach (var control in controls)
            {
                columnSettings += (columnSettings.IsNullOrEmpty() ? String.Empty : ", ") + "{ columnKey: '" + control.BindingPath.Right(".") + "'";
                if ((!control.AggregationFunction.IsNullOrEmpty() && control.AggregationFunction != "None"))
                {
                    string type = control.AggregationFunction.ToUpper();
                    string summaryCalculator = string.Empty;

                    if (control.AggregationFunction.ToUpper() == "CUSTOM")
                    {
                        type = control.AggregationFunction + "Aggregation" + control.ParentName + control.BindingPath.Right(".");
                        summaryCalculator = ", summaryCalculator: function () { return typeof vm." + type + " === 'function' ?  vm."+ type + "() : ''; }";
                        type = type.ToLower();
                    }

                    columnSettings += ", allowSummaries: true, summaryOperands: [ { rowDisplayLabel: '" + control.AggregationDescription + "', type: '" + type + "', active: true" + summaryCalculator + " } ]";
                }
                else
                    columnSettings += ", allowSummaries: false";
                columnSettings += "}";
            }

            if (!columnSettings.IsNullOrEmpty())
            {
                columnSettings = "columnSettings: [" + columnSettings + "]";
            }

            return columnSettings;
        }

        private string GetGroupByColumnSettings(string groupByColumns, List<LayoutControlV2> controls)
        {
            var groupedColumns = (groupByColumns ?? "").Replace(" ", "").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            var columns = controls
                .Select(c =>
                    new
                    {
                        columnKey = c.Name,
                        isGroupBy = groupedColumns.Contains(c.Name),
                        hasAggregation = !c.AggregationFunction.IsNullOrEmpty() && c.AggregationFunction != "None",
                        aggregation = c.AggregationFunction,
                        Text = c.AggregationDescription,
                    });
            List<string> columnSettings = new List<string>();
            foreach (var c in columns)
            {
                if (!c.isGroupBy && !c.hasAggregation) continue;
                columnSettings.Add("{ columnKey: \"" + c.columnKey + "\", isGroupBy: " + c.isGroupBy.ToString().ToLower() + (c.hasAggregation ? ", summaries: [{ summaryFunction: \"" + c.aggregation + "\", text: \"" + c.Text + "\" }]" : "") + "}");
            }
            if (columnSettings.Count == 0)
                return string.Empty;
            else
                return ", columnSettings: [" + string.Join(",", columnSettings) + "]";
        }

        public override string GetDataBind(string bindingPath, string dataBase = "DataElement.DataView.")
        {
            return base.GetDataBind(bindingPath, dataBase);
        }

        private LayoutControlV2 GetControlByContainer(LayoutContainer container)
        {
            if (container == null)
                return null;

            return container.Controls.Where(e => e is LayoutControlV2 && !e.BindingPath.IsNullOrEmpty() && e.ClassName.ToLower() != "chart").Select(e => (LayoutControlV2)e).FirstOrDefault();
        }

        private bool IsButtonContainer(LayoutContainer container)
        {
            return (container.Controls.Any(e => e.ClassName == "Button") && !container.Controls.Any(e => e.ClassName != "Button"));
        }

        private void ComposeDefaultContainerStart(LayoutContainer parentContainer, LayoutContainer container, LayoutContainerClass elementClass, Tools.CodeBuilder codeBuilder, int rows, int columns)
        {

            var control = GetControlByContainer(container);
            if (container.IsTemplate && container.IsInnerTemplate)
            {
                codeBuilder.AddLine("<div id=\"dialog" + container.Name + "\"  class=\"toolbar-dialog-template\" title='" + container.DisplayName + "' style=\"display: none !important; overflow-y:auto;\">");
                if (container.EnableGridSelector)
                {
                    string controlBindingPath = container.Controls.First().BindingPath;
                    string idElement = container.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "dGrid");
                    string binding = GetBindingPath(controlBindingPath, true);
                    string dataView = binding.IsNullOrEmpty() ? "" : (binding.Right(".") + "#").Left("List#");
                    string currentBinding = GetFullBindingPath(controlBindingPath, false), listBinding = GetFullBindingPath(controlBindingPath, true);
                    string rootListBinding = listBinding.Replace("vm", "$root." + this.ViewModelName + "()");
                    string rootCurrent = currentBinding.Replace("vm", "$root." + this.ViewModelName + "()");
                    string parentBinding = currentBinding;
                    int idx = currentBinding.LastIndexOf('.');
                    if (idx >= 0)
                        parentBinding = currentBinding.Left(currentBinding.LastIndexOf('.'));


                    codeBuilder.AddLine("<button  class=\"open-grid btn\" data-bind=\"click: function () { ");
                    codeBuilder.AddLine("$root." + this.ViewModelName + "().loadSeletor('#tb" + container.Name + "', '" + container.SelectorGridColumns.Replace(" ", string.Empty) + "', '#" + idElement + "','" + container.Name + "');");
                    codeBuilder.AddLine("$('#" + idElement + "_Toggle').slideToggle(); }\">Open/Close Selector</button>");
                    codeBuilder.AddLine("   <div id=\"" + idElement + "splt\" class=\"wr-splitter splitter\">");
                    codeBuilder.AddLine("       <div id=\"" + idElement + "_Toggle\" class=\"div-tab-toogle\">");
                    codeBuilder.AddLine("           <table id=\"tb" + container.Name + "\" class=\"splitter-table\"");
                    codeBuilder.AddLine("               data-param=\"");
                    codeBuilder.AddLine("                   #" + idElement + ",");
                    codeBuilder.AddLine("                   " + container.Name + ",");
                    codeBuilder.AddLine("                   " + currentBinding + ";" + listBinding + ",");
                    codeBuilder.AddLine("                   " + dataView + ";" + parentBinding + ",");
                    codeBuilder.AddLine("                   " + (dataView.IsNullOrEmpty() ? EntityName + "()" : dataView + '(' + parentBinding + ')') + ")");
                    codeBuilder.AddLine("               \">");
                    codeBuilder.AddLine("               <thead class=\"\"><tr>");

                    if (!container.DisplaySelectorGridColumns.IsNullOrEmpty())
                    {
                        var propsDisplay = container.DisplaySelectorGridColumns.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
                        foreach (var item in propsDisplay)
                        {
                            codeBuilder.AddLine("               <th>" + item.Replace(" ", string.Empty) + "</th>");
                        }
                    }

                    codeBuilder.AddLine("           </tr></thead>");
                    codeBuilder.AddLine("               <tbody class=\"\">");
                    codeBuilder.AddLine("               </tbody>");
                    codeBuilder.AddLine("           </table>");
                    codeBuilder.AddLine("   </div>");
                    codeBuilder.AddLine("   <div>");
                }

                codeBuilder.AddLine("   <button class=\"btn btn-sm btn-default backReg \" id=\"backReg" + container.Name + "\" onClick=\"$(this).backReg" + container.Name + "();\" data-placement=\"bottom\" title=\"Registro Anterior\"><i class=\"fa fa-backward\"></i></button>");
                codeBuilder.AddLine("   <span class=\"caption\"><label id=\"currentNumber" + container.Name + "\">0</label><label>/</label><label id=\"totalNumber" + container.Name + "\">0</label></span>");
                codeBuilder.AddLine("   <button class=\"btn btn-sm btn-default nextReg \" id=\"nextReg" + container.Name + "\" onClick=\"$(this).nextReg" + container.Name + "();\" data-placement=\"bottom\" title=\"Próximo Registro\"><i class=\"fa fa-forward\"></i></button>");
                if (!container.RemoveDataToolbar)
                {
                    if ((this._layOut.CanEdit || this._layOut.CanAddNew) && container.CanAddNew)
                        codeBuilder.AddLine("   <button class=\"btn btn-sm btn-default addReg \" id=\"addReg" + container.Name + "\" onClick=\"$(this).addReg" + container.Name + "();\" data-placement=\"bottom\" title=\"Adicionar Registro\"><i class=\"fa fa-plus\"></i></button>");

                    if (this._layOut.CanEdit && container.CanDelete)
                        codeBuilder.AddLine("   <button class=\"btn btn-sm btn-default delReg \" id=\"delReg" + container.Name + "\" onClick=\"$(this).delReg" + container.Name + "();\" data-placement=\"bottom\" title=\"Deletar Registro\"><i class=\"fa fa-trash-o\"></i></button>");
                }
                codeBuilder.AddLine("   <button style=\"float: right;\" class=\"btn btn-sm btn-default okReg\" id=\"okReg" + container.Name + "\" onClick=\"$(this).okReg" + container.Name + "();\" data-placement=\"bottom\" title=\"Concluir\"><i class=\"fa fa fa-times\"></i></button>");

                codeBuilder.AddLine("<div id=\"" + container.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "template") + "\" class=\"" + (elementClass == LayoutContainerClass.CustomContainer ? "" : "portlet-body form") + "\" " + GetCssElementSize(container) + " >");
            }
            else
            {
                var containerName = container.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "cnt");
                codeBuilder.AddLine("<div id=\"" + containerName + "\" data-bind=\"css: $root." + this.ViewModelName + "().getLayoutColumnSpan('" + containerName + "'), visible: $root." + this.ViewModelName + "().getLayoutVisible('" + containerName + "')\" class=\"cnt " + (elementClass == LayoutContainerClass.CustomContainer ? "" : "portlet-body form") + " remove-pl remove-pr\" " + GetCssContainerHeight(container) + ">");
                codeBuilder.AddLine("<div" + (IsButtonContainer(container) ? " class=\"container-buttons\"" : "") + ">");
            }

            if (control != null)
            {
                codeBuilder.IncreaseIndent();
                codeBuilder.AddLine(GetKoBindingDivs(control.BindingPath, true, false));
            }

        }

        private void ComposeTabControlContainerStart(LayoutContainer parentContainer, LayoutContainer container, LayoutContainerClass elementClass, Tools.CodeBuilder codeBuilder, int rows, int columns)
        {
            string controlName = container.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "tc");
            codeBuilder.ComplementaryCalls.AddLine("$('#" + controlName + "').on('shown.bs.tab', function (e) { vm.notifyInnerElements($(e.target.hash)); });");
            codeBuilder.ComplementaryCalls.AddLine("initializeTabControl('#" + controlName + "');");

            codeBuilder.AddLine("<div id=\"" + controlName + "\"  data-bind=\"css: $root." + this.ViewModelName + "().getLayoutColumnSpan('" + controlName + "'), visible: $root." + this.ViewModelName + "().getLayoutVisible('" + controlName + "')\"  class=\"tabbable tabbable-custom" + (container.Style == ContainerStyle.NoBorder ? "" : " box") + "\" " + GetCssContainerHeight(container) + ">");
            codeBuilder.AddLine("   <div class=\"container-tab\">");
            codeBuilder.AddLine("      <div id=\"" + controlName + "_scroller_left\" class=\"scroller-c scroller-left\"><i class=\"glyphicon glyphicon-chevron-left\"></i></div>");
            codeBuilder.AddLine("      <div id=\"" + controlName + "_scroller_right\" class=\"scroller-c scroller-right\"><i class=\"glyphicon glyphicon-chevron-right\"></i></div>");
            codeBuilder.AddLine("      <div id=\"" + controlName + "_wrapper\" class=\"wrapper\">");
            codeBuilder.AddLine("         <ul id=\"" + controlName + "_list\" class=\"nav nav-tabs list\">");

            bool isActive = true;
            foreach (LayoutContainer tab in container.Controls.Where(e => e is LayoutContainer))
            {
                var nameTabItem = tab.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "ti");
                codeBuilder.AddLine("            <li id=\"" + tab.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "ti") + "\" class=\"" + (isActive ? "active" : "") + "\"" + (tab.IsVisible ? "" : " style=\"display: none;\"") + "><a data-bind=\"text: $root." + this.ViewModelName + "().getLayoutDisplayName('" + nameTabItem + "'), attr: {title: $root." + this.ViewModelName + "().getLayoutDisplayName('" + nameTabItem + "')}\" href=\"#" + tab.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "tab") + "\" data-toggle=\"tab\"></a></li>");
                isActive = false;
            }

            codeBuilder.AddLine("         </ul>");
            codeBuilder.AddLine("         </div>");
            codeBuilder.AddLine("      </div>");
            codeBuilder.AddLine("   <div class=\"tab-content\">");
        }

        private void ComposeWizardControlContainerStart(LayoutContainer parentContainer, LayoutContainer container, LayoutContainerClass elementClass, Tools.CodeBuilder codeBuilder, int rows, int columns)
        {
            codeBuilder.ComplementaryCalls.AddLine("$('#" + container.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "wiz") + "').on('shown.bs.tab', function (e) { vm.notifyInnerElements($(e.target.hash)); });");

            codeBuilder.AddLine("<div id=\"{0}\" class=\"form-wizard" + (container.IsVisible ? "" : " hide") + " " + GetColumnSpan(parentContainer, container, false) + "\" {1}>", container.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "wiz"), GetCssContainerHeight(container));
            codeBuilder.IncreaseIndent();
            codeBuilder.AddLine("<div class=\"form-body\">");
            codeBuilder.IncreaseIndent();
            codeBuilder.AddLine("<div class=\"row\">");
            codeBuilder.IncreaseIndent();
            codeBuilder.AddLine("<div class=\"col-md-10\">");
            codeBuilder.IncreaseIndent();
            codeBuilder.AddLine("<ul class=\"nav nav-pills nav-justified steps\">");

            codeBuilder.IncreaseIndent();
            bool isActive = true;
            var tabs = container.Controls.Where(e => e is LayoutContainer).ToList();
            foreach (LayoutContainer tab in tabs)
            {
                codeBuilder.AddLine("<li " + (container.IsVisible ? "" : "class=\"hide\" ") + ">");
                codeBuilder.AddLine("    <a href=\"#" + tab.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "tab") + "\" data-toggle=\"tab\" class=\"step" + (isActive ? " active" : "") + "\" style=\"cursor: default\" >");
                codeBuilder.AddLine("        <span class=\"number\">" + (tabs.IndexOf(tab) + 1).ToString() + "</span>");
                codeBuilder.AddLine("        <span class=\"desc\"><i class=\"fa fa-check\"></i><span data-bind=\"text: $root." + this.ViewModelName + "().getLayoutDisplayName('" + tab.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "ti") + "')\">\"</span></span>");
                codeBuilder.AddLine("    </a>");
                codeBuilder.AddLine("</li>");

                isActive = false;
            }
            codeBuilder.DecreaseIndent();
            codeBuilder.AddLine("</ul>");
            codeBuilder.DecreaseIndent();

            codeBuilder.AddLine("   <div id=\"" + container.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "wiz") + "_bar\" class=\"progress progress-striped active\">");
            codeBuilder.AddLine("       <div class=\"progress-bar progress-bar-success\"></div>");
            codeBuilder.AddLine("   </div>");
            codeBuilder.AddLine("</div>");
            codeBuilder.AddLine("<div class=\"col-md-2 position-absolute-btnWizard\">");
            codeBuilder.AddLine("    <div class=\"form-actions right btnWizard-bottom remove-pb\">");
            codeBuilder.AddLine("        <a href=\"javascript:;\" class=\"btn default button-previous button-previous-wizard\"><i class=\"m-icon-swapleft\"></i>" + "Voltar".Translate() + "</a>");
            codeBuilder.AddLine("        <a href=\"javascript:;\" class=\"btn blue button-next button-next-wizard\">" + "Continuar".Translate() + "<i class=\"m-icon-swapright m-icon-white\"></i></a>");
            codeBuilder.AddLine("        <a href=\"javascript:;\" class=\"btn green button-submit button-submit-wizard\">" + "Submeter".Translate() + "<i class=\"m-icon-swapright m-icon-white\"></i></a>");
            codeBuilder.AddLine("    </div>");
            codeBuilder.AddLine("</div>");

            codeBuilder.DecreaseIndent();
            codeBuilder.AddLine("</div>");
            codeBuilder.AddLine("<div class=\"tab-content\">");

            codeBuilder.IncreaseIndent();
        }

        private void ComposeWizardNavigation(LayoutContainer container, Tools.CodeBuilder codeBuilder)
        {
            codeBuilder.DecreaseIndent();
            codeBuilder.AddLine("</div>");
            string containerName = container.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "wiz");

            //generate JS
            #region Defining auxiliary code
            codeBuilder.ComplementaryCalls.AddLine("complement.render" + containerName.Replace("-", "").Replace(" ", "") + "(vm);");
            codeBuilder.ComplementaryCode.AddLine(", render" + containerName.Replace("-", "").Replace(" ", "") + ": function(vm) {");
            codeBuilder.ComplementaryCode.IncreaseIndent();

            codeBuilder.ComplementaryCode.AddLine("$('#" + containerName + "').bootstrapWizard({");
            codeBuilder.ComplementaryCode.AddLine("    'nextSelector': '.button-next',");
            codeBuilder.ComplementaryCode.AddLine("    'previousSelector': '.button-previous',");
            codeBuilder.ComplementaryCode.AddLine("    onInit: function (tab, navigation, index) {");
            codeBuilder.ComplementaryCode.AddLine("        $('#" + containerName + "').find('.button-previous').hide();");
            codeBuilder.ComplementaryCode.AddLine("		 $('#" + containerName + " .button-submit').click(function () {");
            codeBuilder.ComplementaryCode.AddLine("			 if((typeof vm.OnWizardFinalizing === 'function')) {");
            codeBuilder.ComplementaryCode.AddLine("			    if (!vm.OnWizardFinalizing('" + containerName + "')) return false;");
            codeBuilder.ComplementaryCode.AddLine("			 }");
            codeBuilder.ComplementaryCode.AddLine("			 if((typeof vm.OnWizardFinalized === 'function')) {");
            codeBuilder.ComplementaryCode.AddLine("			    vm.OnWizardFinalized('" + containerName + "');");
            codeBuilder.ComplementaryCode.AddLine("			 }");
            codeBuilder.ComplementaryCode.AddLine("			 if(vm.custom) {");
            codeBuilder.ComplementaryCode.AddLine("		        var e = { cancel: false, viewModel: vm };");
            codeBuilder.ComplementaryCode.AddLine("			    vm.custom.beforeWizardFinalizing(e); //custom Finalizing");
            codeBuilder.ComplementaryCode.AddLine("			    if(e.cancel) return false;");
            codeBuilder.ComplementaryCode.AddLine("			    vm.custom.afterWizardFinalizing({viewModel: vm, id: '" + containerName + "'}); //custom Finalized");
            codeBuilder.ComplementaryCode.AddLine("			 }");
            codeBuilder.ComplementaryCode.AddLine("		 }).hide();");
            codeBuilder.ComplementaryCode.AddLine("		 if((typeof vm.OnWizardInitializing === 'function')) {");
            codeBuilder.ComplementaryCode.AddLine("		     vm.OnWizardInitializing();");
            codeBuilder.ComplementaryCode.AddLine("		 }");
            codeBuilder.ComplementaryCode.AddLine("		 if(vm.custom) vm.custom.afterWizardInitializing({viewModel: vm});");
            codeBuilder.ComplementaryCode.AddLine("    },");
            codeBuilder.ComplementaryCode.AddLine("    onTabClick: function (tab, navigation, index) {");
            codeBuilder.ComplementaryCode.AddLine("        return false;");
            codeBuilder.ComplementaryCode.AddLine("    },");
            codeBuilder.ComplementaryCode.AddLine("    onPrevious: function (tab, navigation, index) {");

            codeBuilder.ComplementaryCode.AddLine("		 if((typeof vm.OnWizardStepChanging === 'function')) {");
            codeBuilder.ComplementaryCode.AddLine("		    if (!vm.OnWizardStepChanging(tab.index(), index, '" + containerName + "')) return false;");
            codeBuilder.ComplementaryCode.AddLine("		 }");
            codeBuilder.ComplementaryCode.AddLine("		 var e = { oldIndex: tab.index(), newIndex: index, cancel: false, viewModel: vm, id: '" + containerName + "'};");
            codeBuilder.ComplementaryCode.AddLine("		 if(vm.custom) vm.custom.beforeWizardStepChanging(e); //custom Step changing");
            codeBuilder.ComplementaryCode.AddLine("		 if(e.cancel) return false;");

            codeBuilder.ComplementaryCode.AddLine("		 wizardStepChange('" + containerName + "',  navigation, index);");

            codeBuilder.ComplementaryCode.AddLine("		 if((typeof vm.OnWizardStepChanged === 'function')) {");
            codeBuilder.ComplementaryCode.AddLine("		    vm.OnWizardStepChanged(tab.index(), index, '" + containerName + "');");
            codeBuilder.ComplementaryCode.AddLine("		 }");
            codeBuilder.ComplementaryCode.AddLine("		 if(vm.custom) vm.custom.afterWizardStepChanging({ oldIndex: tab.index(), newIndex: index, viewModel: vm, id: '" + containerName + "'}); //custom Step changed");
            codeBuilder.ComplementaryCode.AddLine("    },");
            codeBuilder.ComplementaryCode.AddLine("    onNext: function (tab, navigation, index) {");
            codeBuilder.ComplementaryCode.AddLine("		 if((typeof vm.OnWizardStepChanging === 'function')) {");
            codeBuilder.ComplementaryCode.AddLine("		    if (!vm.OnWizardStepChanging(tab.index(), index, '" + containerName + "')) return false;");
            codeBuilder.ComplementaryCode.AddLine("		 }");
            codeBuilder.ComplementaryCode.AddLine("		 var e = { oldIndex: tab.index(), newIndex: index, cancel: false, viewModel: vm, id: '" + containerName + "'};");
            codeBuilder.ComplementaryCode.AddLine("		 if(vm.custom) vm.custom.beforeWizardStepChanging(e); //custom Step changing");
            codeBuilder.ComplementaryCode.AddLine("		 if(e.cancel) return false;");
            codeBuilder.ComplementaryCode.AddLine("		 wizardStepChange('" + containerName + "',  navigation, index);");
            codeBuilder.ComplementaryCode.AddLine("		 if((typeof vm.OnWizardStepChanged === 'function')) {");
            codeBuilder.ComplementaryCode.AddLine("		    vm.OnWizardStepChanged(tab.index(), index, '" + containerName + "');");
            codeBuilder.ComplementaryCode.AddLine("		 }");
            codeBuilder.ComplementaryCode.AddLine("		 if(vm.custom) vm.custom.afterWizardStepChanging({ oldIndex: tab.index(), newIndex: index, viewModel: vm, id: '" + containerName + "'}); //custom Step changed");
            codeBuilder.ComplementaryCode.AddLine("    },");
            codeBuilder.ComplementaryCode.AddLine("    onTabShow: function (tab, navigation, index) {");
            codeBuilder.ComplementaryCode.AddLine("        var total = navigation.find('li').length;");
            codeBuilder.ComplementaryCode.AddLine("        var current = index + 1;");
            codeBuilder.ComplementaryCode.AddLine("        var $percent = (current / total) * 100;");
            codeBuilder.ComplementaryCode.AddLine("        $('#" + containerName + "').find('.progress-bar').css({");
            codeBuilder.ComplementaryCode.AddLine("            width: $percent + '%'");
            codeBuilder.ComplementaryCode.AddLine("        });");
            codeBuilder.ComplementaryCode.AddLine("    }");
            codeBuilder.ComplementaryCode.AddLine("});");

            codeBuilder.ComplementaryCode.DecreaseIndent();
            codeBuilder.ComplementaryCode.AddLine("}");
            #endregion
        }

        private void ComposeTabItemControlContainerStart(LayoutContainer container, LayoutContainerClass elementClass, Tools.CodeBuilder codeBuilder, int rows, int columns, int index)
        {
            codeBuilder.AddLine("<div class=\"tab-pane fade" + (index == 0 ? " active in" : "") + "\" id=\"" + container.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "tab") + "\">");
            codeBuilder.IncreaseIndent();
            codeBuilder.AddLine("<div class=\"ui-helper-clearfix" + (elementClass == LayoutContainerClass.WizardItem ? " input-box-wizard" : "") + "\">");
            var control = GetControlByContainer(container);
            if (control != null)
            {
                codeBuilder.IncreaseIndent();
                codeBuilder.AddLine(GetKoBindingDivs(control.BindingPath, true, false));
            }

            codeBuilder.AddLine("<div class=\"\">");
        }

        private void ComposeGroupBoxContainerStart(LayoutContainer parentContainer, LayoutContainer container, LayoutContainerClass elementClass, Tools.CodeBuilder codeBuilder, int rows, int columns, bool isExpander)
        {
            string containerName = container.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "gb");
            if (isExpander)
                codeBuilder.ComplementaryCalls.AddLine("$('#" + containerName + "').one('shown.bs.collapse', function (e) { vm.notifyInnerElements($(e.currentTarget), true); });");
            if (isExpander && container.DisplayName.IsNullOrEmpty())
                container.DisplayName = "Título não definido";

            codeBuilder.AddLine("<div id=\"" + containerName + "\" data-bind=\"css: $root." + this.ViewModelName + "().getLayoutColumnSpan('" + containerName + "'), visible: $root." + this.ViewModelName + "().getLayoutVisible('" + containerName + "')\"  class=\"" + (isExpander ? "gb-expander" : " gbox") + " position-element" + "\" " + GetCssContainerHeight(container) + " >");
            codeBuilder.IncreaseIndent();
            codeBuilder.AddLine("<div class=\"portlet" + (container.Style == ContainerStyle.NoBorder ? "" : " box") + (container.FullPathClass.Contains("TabItem") ? " dark-background" : (parentContainer.IsNull() ? " row" : " ")) + "\" >");
            codeBuilder.IncreaseIndent();

            if (!container.DisplayName.IsNullOrEmpty())
            {
                codeBuilder.AddLine("<div class=\"portlet-title\">");
                codeBuilder.IncreaseIndent();
                codeBuilder.AddLine("<div class=\"caption\" data-bind=\"text: $root." + this.ViewModelName + "().getLayoutDisplayName('" + containerName + "')\"><i class=\"icon-reorder\"></i></div>");
                codeBuilder.DecreaseIndent();
                if (isExpander)
                {
                    codeBuilder.IncreaseIndent();
                    codeBuilder.AddLine("<div class=\"tools\">");
                    codeBuilder.IncreaseIndent();
                    codeBuilder.AddLine("<a id=\"" + containerName + "_anchor\" href=\"#" + containerName + "_body\" role=\"button\" data-toggle=\"collapse\" data-parent=\"#" + containerName + "\" aria-controls=\"#" + containerName + "_body\" class=\"" + (container.IsExpanded ? "" : "collapsed") + "\" title=\"Clique para expandir ou colapsar\"></a>");
                    codeBuilder.DecreaseIndent();
                    codeBuilder.AddLine("</div>");
                    codeBuilder.DecreaseIndent();
                }
                codeBuilder.AddLine("</div>");
            }

            codeBuilder.AddLine("<div id=\"" + containerName + "_body\" class=\"portlet-body" + (isExpander ? " collapse" + (container.IsExpanded ? " in" : "") : "") + "\" >");

            var control = GetControlByContainer(container);
            if (control != null)
            {
                codeBuilder.IncreaseIndent();
                codeBuilder.AddLine(GetKoBindingDivs(control.BindingPath, true, false));
            }

            codeBuilder.IncreaseIndent();
            codeBuilder.AddLine("<div class=\"row\">");
        }

        private void StartColumn(LayoutContainer container, Tools.CodeBuilder codeBuilder, int columnSpan)
        {

            codeBuilder.AddLine("<div class=\"" + GetColumnSpan(null, columnSpan, false) + " remove-pl remove-pr\">");
            codeBuilder.IncreaseIndent();
        }

        private string GetCssElementSize(LayoutElement element, bool putStyleTag = true)
        {
            string size = "width: 100%; height: 100%;";

            if (putStyleTag)
                return "style=\"" + size + "\"";
            else
                return size;
        }

        private string GetCssContainerHeight(LayoutContainer container, int defaultValue = 0, bool putStyleTag = true)
        {
            int height = container.Height == 0 ? defaultValue : container.Height;
            if (height == 0)

                return "";

            string size = string.Format("height: {0}px", height);

            if (putStyleTag)
                return "style=\"" + size + "\"";
            else
                return size;
        }

        #region  Comentado pelo Henry
        private string GetColumnSpan(LayoutContainer parentContainer, LayoutElement element, bool putClassTag = true, bool joinInformationOfFont = false)
        {
            return GetColumnSpan(element, element.ColumnSpan, putClassTag, joinInformationOfFont);
        }

        private string GetColumnSpan(LayoutElement element, int columnSpan, bool putClassTag = true, bool joinInformationOfFont = false, bool isConnected = false)
        {
            columnSpan = columnSpan == 0 ? columnSpanMax : columnSpan;

            string size = string.Format("col-lg-{0} col-md-{0} col-sm-12 col-xs-12", columnSpan);

            if (joinInformationOfFont && !element.IsNull())
            {
                var font = GetFont(element, false);
                if (!font.IsNullOrEmpty())
                    size += " " + font;
            }

            if (isConnected)
                size += " connected-field";

            if (putClassTag)
                return "class=\"" + size + "\"";
            else
                return size;
        }
        #endregion
        private static string GetMediaWidth(MediaWidth width)
        {
            string mediaClassName = "";
            switch (width)
            {
                case MediaWidth.Medium:
                    mediaClassName = "media-medium";
                    break;
                case MediaWidth.Small:
                    mediaClassName = "media-small";
                    break;
                default:
                    mediaClassName = "media-large";
                    break;
            }
            return mediaClassName;
        }
        private string getControlWidth(ControlWidth width)
        {
            string classWidth = "";
            switch (width)
            {
                case ControlWidth.Mini:
                    classWidth = "input-mini";
                    break;
                case ControlWidth.ExtraSmall:
                    classWidth = "input-xsmall";
                    break;
                case ControlWidth.Small:
                    classWidth = "input-small";
                    break;
                case ControlWidth.MinMedium:
                    classWidth = "input-min-medium";
                    break;
                case ControlWidth.Medium:
                    classWidth = "input-medium";
                    break;
                case ControlWidth.ExtraMedium:
                    classWidth = "input-xmedium";
                    break;
                case ControlWidth.Large:
                    classWidth = "input-large";
                    break;
                case ControlWidth.ExtraLarge:
                    classWidth = "input-xlarge";
                    break;
                default:
                    classWidth = width.ToString();
                    break;
            }
            return classWidth;
        }

        #region Font configuration
        public string GetFont(LayoutElement element, bool putclassTag = true)
        {
            string classFont = string.Empty;
            classFont += GetClassFontBackground(element);
            classFont += GetClassFontForegroundColor(element);
            classFont += GetFontBold(element);

            if (classFont.IsNullOrEmpty())
                return string.Empty;

            if (putclassTag)
                return "class=\"" + classFont + "\"";
            else
                return classFont.StartsWith(" ") ? classFont : " " + classFont;
        }

        public string GetClassFontBackground(LayoutElement element)
        {
            if (element.FontBackground == FontBackground.Highlight)
                return " fs-highlight";
            else
                return string.Empty;
        }

        public string GetClassFontForegroundColor(LayoutElement element)
        {
            string classFore = string.Empty;
            switch (element.FontForegroundStyle)
            {
                case FontForegroundStyle.Error:
                    classFore = "text-danger";
                    break;
                case FontForegroundStyle.Info:
                    classFore = "text-info";
                    break;
                case FontForegroundStyle.Muted:
                    classFore = "text-muted";
                    break;
                case FontForegroundStyle.Success:
                    classFore = "text-success";
                    break;
                case FontForegroundStyle.Warning:
                    classFore = "text-warning";
                    break;
                default:
                    break;
            }
            return classFore.IsNullOrEmpty() ? string.Empty : " " + classFore;
        }

        public string GetFontBold(LayoutElement element)
        {
            return element.FontBold ? " fs-bold" : string.Empty;
        }
        #endregion

        private string GetGridHeight(LayoutContainer grid)
        {
            string result = "";
            if (grid.Height > 0)
                result = string.Format("'{0}px'", grid.Height);
            else if (grid.GridHeight == GridSizeHeight.Auto)
                result = "grid.parents('.linx-table-grid').height()";
            else
                result = "(getGridHeightSuggested() " + (grid.GridHeight == GridSizeHeight.Small ? "* 0.5" : (grid.GridHeight == GridSizeHeight.Medium ? "* 0.75" : "")) + ")";

            return result;
        }

        private string GetControlRange(LayoutControlV2 control)
        {
            long? minValue = null, maxValue = null;
            long min, max;
            if (!control.Range.IsNullOrEmpty())
            {
                var temp = control.Range.Split(",".ToCharArray());
                if (temp.Length == 2)
                {
                    if (long.TryParse(temp[0], out min))
                        minValue = min;
                    if (long.TryParse(temp[1], out max))
                        maxValue = max;
                }
            }
            if (!control.AllowNegativeValue)
                minValue = !minValue.HasValue || minValue.Value < 0 ? 0 : minValue;

            //Generating result
            string result = String.Empty;
            if (minValue.HasValue)
                result += "minValue: " + minValue.Value.ToString() + ", ";

            if (maxValue.HasValue)
                result += "maxValue: " + maxValue.Value.ToString() + ", ";

            return result;
        }
        #endregion
    }
}

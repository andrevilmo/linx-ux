using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Linx.Tools;

namespace Linx.Builder.Resources
{
    public class MobileViewGen : LayoutCodeGen<CustomizedLayoutV2>
    {
        public const int columnSpanMax = 12;
        public const int columnSpanLabel = 3;
        public const string spaceHtml = "&nbsp;";

        public MobileViewGen(CustomizedLayoutV2 layout, string entityName, string viewModelName)
            : base(layout, entityName, viewModelName)
        {
            this._layOut.AdjustFullPathClass();
            this.SupportsDataGridTemplate = false;
        }


        private void ComposeDefaultContainer(Tools.CodeBuilder codeBuilder)
        {
            codeBuilder.AddLine("<div class=\"list\">");
            codeBuilder.IncreaseIndent();
        }

        public override void ComposeContainerEndRow(LayoutContainer container, LayoutContainerClass elementClass, Tools.CodeBuilder codeBuilder, int row)
        {
            ComposeGenericContainerEnd(container, elementClass, codeBuilder, 0);
        }

        public override void ComposeContainerStart(LayoutContainer parentContainer, LayoutContainer container, LayoutContainerClass elementClass, Tools.CodeBuilder codeBuilder, int rows, int columns, List<TreeLayoutContainer> innerDataGrids, int index)
        {
            switch (elementClass)
            {
                case LayoutContainerClass.CustomContainer:
                    ComposeDefaultContainer(codeBuilder);
                    break;
                case LayoutContainerClass.ExternalUI:
                    break;
                case LayoutContainerClass.TreeListView:
                case LayoutContainerClass.DataGrid:
                    this.ComposeDataList(parentContainer, container, elementClass, codeBuilder, rows, columns);
                    break;
                case LayoutContainerClass.Expander:
                    codeBuilder.AddLine("<span class=\"input-label\">" + container.DisplayName + ":</span>");
                    ComposeDefaultContainer(codeBuilder);
                    break;
                case LayoutContainerClass.GroupBox:
                    codeBuilder.AddLine("<span class=\"input-label\">" + container.DisplayName + ":</span>");
                    ComposeDefaultContainer(codeBuilder);
                    break;
                case LayoutContainerClass.OlapPivotGrid:
                    
                    break;
                case LayoutContainerClass.FlatPivotGrid:
                    
                    break;
                case LayoutContainerClass.PivotChart:
                case LayoutContainerClass.PivotDrillDownChart:
                    
                    break;
                case LayoutContainerClass.DockManager:
                case LayoutContainerClass.TabControl:
                    //this.ComposeTabControlContainerStart(parentContainer, container, elementClass, codeBuilder, rows, columns);
                    ComposeDefaultContainer(codeBuilder);
                    break;
                case LayoutContainerClass.DockItem:
                case LayoutContainerClass.WizardItem:
                case LayoutContainerClass.TabItem:
                    codeBuilder.AddLine("<span class=\"input-label\">" + container.DisplayName + ":</span>");
                    ComposeDefaultContainer(codeBuilder);
                    break;
                case LayoutContainerClass.WizardControl:
                    
                    break;
                default:
                    break;
            }
        }

        public override void ComposeContainerStartRow(LayoutContainer container, LayoutContainerClass elementClass, Tools.CodeBuilder codeBuilder, int row)
        {
            codeBuilder.AddLine("<div class=\"col\">");            
            codeBuilder.IncreaseIndent();
        }

        public override void ComposeContainerStartColumn(LayoutContainer container, LayoutContainerClass elementClass, Tools.CodeBuilder codeBuilder, int totalColumns, int columnSpan)
        {
            codeBuilder.AddLine("<div class=\"row responsive-sm\">");
            codeBuilder.IncreaseIndent();
        }

        public override void ComposeControl(LayoutControlV2 control, LayoutControlClass elementClass, Tools.CodeBuilder codeBuilder, bool isConnected, Dictionary<LayoutControlV2, LayoutControlClass> connectedControls, bool labelOnTop, bool isTemplate)
        {
            string controlName = (_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + control.GetDefaultControlName();
            string bindPath = this.GetFullBindingPath(control.BindingPath, false);

            switch (elementClass)
            {
                case LayoutControlClass.Button:
                    codeBuilder.AddLine("<button id=\"" + controlName + "\" class=\"button\" ng-click=\"vm." + control.GetControlName((_layOut.IsSecundary ? "scy" : "") + "") + "_Click()\">" + control.DisplayName + "</button>");
                    break;
                case LayoutControlClass.Chart:
                    
                    break;
                case LayoutControlClass.CheckBox:
                    codeBuilder.AddLine("<div class=\"item item-checkbox\">");
                    codeBuilder.AddLine("   <label class=\"checkbox\">");
                    codeBuilder.AddLine("     <input type=\"checkbox\" id=\"" + controlName + "\" ng-disabled=\"" + this.GetReadOnlyBind(control).Replace("$data", bindPath) + "\" ng-model=\"" + bindPath + "." + control.BindingPath.Right(".") + "\">");
                    codeBuilder.AddLine("   </label>");
                    codeBuilder.AddLine("   <span class=\"input-label\">" + control.DisplayName + "</span>");
                    codeBuilder.AddLine("</div>");
                    break;
                case LayoutControlClass.ChildToolBar:
                    break;
                case LayoutControlClass.ColorPicker:
                    break;
                case LayoutControlClass.RadioButtonGroup:
                case LayoutControlClass.ComboBox:               
                     codeBuilder.AddLine("<lx-combobox lx-visible=\"true\"");
                     codeBuilder.AddLine("displayname=\"'" + control.DisplayName + "'\"");
                     codeBuilder.AddLine("lx-disable=\"" + this.GetReadOnlyBind(control).Replace("$data", bindPath) + "\"");
                     codeBuilder.AddLine("lx-collection=\"vm.dataBusiness.getDataContext().dataDomains.getItems('" + control.DomainName + "', '" + (control.DomainFilterValues ?? "") + "')\"");
                     codeBuilder.AddLine("lx-model=\"" + bindPath + "." + control.BindingPath.Right(".") + "\"");
                     codeBuilder.AddLine("lx-value=\"'id'\"");
                     codeBuilder.AddLine("lx-label=\"'name'\"></lx-combobox>");
                     break;
                                        
                case LayoutControlClass.Dashboard:
                                        
                case LayoutControlClass.DateTimeTextBox:
                                        
                case LayoutControlClass.EconomicGroup:
                    
                case LayoutControlClass.EditBox:
                                        
                case LayoutControlClass.Gauge:
                                        
                case LayoutControlClass.HtmlViewer:
                    
                case LayoutControlClass.KpiBox:
                    
                case LayoutControlClass.Label:
                case LayoutControlClass.TextBlock:
                                        
                case LayoutControlClass.MaskedTextBox:
                                        
                case LayoutControlClass.MultimediaControl:
                                        
                case LayoutControlClass.NumericTextBox:

                case LayoutControlClass.LookUpTextBox:
                    
                case LayoutControlClass.TextBox:
                    if (control.IsPassword)
                        codeBuilder.AddLine("");
                    else
                    {                        
                        codeBuilder.AddLine("<label class=\"item item-input item-stacked-label" + (control.IsVisible ? "" : " hide") + "\">");
                        codeBuilder.AddLine("  <span class=\"input-label\">" + control.DisplayName + "</span>");
                        codeBuilder.AddLine("  <input type=\"text\" id=\"" + controlName + "\" ng-disabled=\"" + this.GetReadOnlyBind(control).Replace("$data", bindPath) + "\" ng-model=\"" + bindPath + "." + control.BindingPath.Right(".") + "\" >");
                        codeBuilder.AddLine("</label>");
                    }
                    break;
                default:
                    break;
            }
        }

        public override void ComposeContainerEndColumn(LayoutContainer container, LayoutContainerClass elementClass, Tools.CodeBuilder codeBuilder, int column)
        {
            if (!IsButtonContainer(container))
            {
                this.ComposeGenericContainerEnd(container, elementClass, codeBuilder, column);
            }

        }

        private void ComposeTabControlContainerStart(LayoutContainer parentContainer, LayoutContainer container, LayoutContainerClass elementClass, Tools.CodeBuilder codeBuilder, int rows, int columns)
        {
            codeBuilder.AddLine("<div id=\"" + container.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "tc") + "\" class=\"tabs\">");

            foreach (LayoutContainer tab in container.Controls.Where(e => e is LayoutContainer))
            {
                codeBuilder.AddLine("   <a id=\"" + tab.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "ti") + "\" class=\"tab-item\">" + tab.DisplayName + "</a>");
            }

            codeBuilder.AddLine("</div>");            
        }

        private LayoutControlV2 GetControlByContainer(LayoutContainer container)
        {
            if (container == null)
                return null;

            return container.Controls.Where(e => e is LayoutControlV2 && !e.BindingPath.IsNullOrEmpty() && e.ClassName.ToLower() != "chart").Select(e => (LayoutControlV2)e).FirstOrDefault();
        }

        private void ComposeGenericContainerEnd(LayoutContainer container, LayoutContainerClass elementClass, Tools.CodeBuilder codeBuilder, int column)
        {
            codeBuilder.DecreaseIndent();
            codeBuilder.AddLine("</div>");
        }


        #region ComposeContainerEnd

        public override void ComposeContainerEnd(LayoutContainer container, LayoutContainerClass elementClass, Tools.CodeBuilder codeBuilder)
        {
            ComposeGenericContainerEnd(container, elementClass, codeBuilder, 0);
        }

        #endregion


        #region Auxiliary members

        private string GetEditableBind(LayoutControlV2 control)
        {
            return (control.AlwaysEditable ? "true" : (control.EditableOnInsert || (control.IsPartOfKey && control.IsEditable) ? "vm.dataBusiness.status() === 'C' || $data.isAdded()" : (control.IsEditable ? "vm.dataBusiness.enabledForEditing()" : "vm.dataBusiness.status() === 'C'")));
        }

        private string GetReadOnlyBind(LayoutControlV2 control)
        {
            return (control.AlwaysEditable ? "false" : (control.EditableOnInsert || (control.IsPartOfKey && control.IsEditable) ? "!(vm.dataBusiness.status() === 'C' || $data.isAdded())" : (control.IsEditable ? "!vm.dataBusiness.enabledForEditing()" : "vm.dataBusiness.status() !== 'C'")));
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

        private string GetBindingPath(string bindingPath, bool returnBindingList = false)
        {
            string result = String.Empty;

            foreach (string part in GetBindingParts(bindingPath, returnBindingList))
            {
                result += (result.IsNullOrEmpty() ? "" : ".") + part;
            }
            return result;
        }

        private string GetFullBindingPath(string bindingPath, bool returnBindingList)
        {
            string result = String.Empty;

            string[] bindingParts = GetBindingParts(bindingPath, returnBindingList);
            for (int idx = 0; idx < bindingParts.Length; idx++)
            {
                result += (result == String.Empty ? String.Empty : ".") + bindingParts[idx];
            }

            if (result.IsNullOrEmpty())
                result = (returnBindingList ? "dataView" : "currentDataItem");

            return "vm.dataBusiness." + result;
        }

        private string[] GetBindingParts(string bindingPath, bool returnBindingList = false)
        {
            List<string> result = new List<string>();

            string[] bindingParts = GetDataBind((bindingPath + ".").Left("." + bindingPath.Right(".") + ".")).Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

            if (bindingParts.Length > 0 || !returnBindingList)
                result.Add("currentDataItem()");

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
                        result.Add("current" + bindingPart.Replace("PagedList", ""));
                    }
                }
            }

            return result.ToArray();
        }

        private void ComposeDataList(LayoutContainer parentContainer, LayoutContainer container, LayoutContainerClass elementClass, Tools.CodeBuilder codeBuilder, int rows, int columns)
        {
            var controls = container.Controls.Where(e => e is LayoutControlV2 && !e.BindingPath.IsNullOrEmpty()).Select(e => (LayoutControlV2)e).OrderBy(e => e.GetDataGridOrder()).ToList();

            if (controls.Count == 0)
            {
                this.ComposeDefaultContainer(codeBuilder);
                return;
            }
            
            string idElement = container.GetControlName((_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + "dList");
            string controlBindingPath = controls.First().BindingPath;
            string binding = GetBindingPath(controlBindingPath, true);
            string dataView = binding.IsNullOrEmpty() ? "" : (binding.Right(".") + "#").Left("List#");
            string currentBinding = GetFullBindingPath(controlBindingPath, false);
            string listBinding = GetFullBindingPath(controlBindingPath, true);

            codeBuilder.AddLine("<button id=\"" + idElement + "_AddBtn\" class=\"button button-icon ion-plus-round\" ng-if=\"vm.dataBusiness.status() === 'E'\" title=\"Novo Registro\" ng-click=\"vm.dataBusiness.createAndNotify" + dataView + "(" + currentBinding.Left(".current" + dataView) + ")\"></button>");
            codeBuilder.AddLine("<div id=\"" + idElement + "\" class=\"list\">");
            codeBuilder.IncreaseIndent();            
            codeBuilder.AddLine("    <a class=\"item item-thumbnail-left\" href=\"#\" ng-repeat=\"item in " + listBinding + "\">");
            codeBuilder.IncreaseIndent();
            foreach (var control in controls)
            {

                string controlName = (_layOut.IsSecundary ? "scy" : "") + this.ViewModelName + "_" + control.GetDefaultControlName();

                codeBuilder.AddLine("<label class=\"item item-input item-stacked-label" + (control.IsVisible ? "" : " hide") + "\">");
                codeBuilder.AddLine("  <span class=\"input-label\">" + control.DisplayName + "</span>");
                codeBuilder.AddLine("  <input type=\"text\" id=\"" + controlName + "\" ng-disabled=\"" + this.GetReadOnlyBind(control).Replace("$data", "item") + "\" ng-model=\"item." + control.BindingPath.Right(".") + "\" >");
                codeBuilder.AddLine("</label>");

            }
            codeBuilder.DecreaseIndent();
            codeBuilder.AddLine("    </a>");
            codeBuilder.DecreaseIndent();

        }

        private string getMaxLengthNumeric(LayoutControlV2 control)
        {
            bool hasDecimal = control.GetPrecisionDecimalsInt() > 0;
            return (control.GetPrecision() + (hasDecimal ? 1 : 0)).ToString();
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

        public override string GetDataBind(string bindingPath, string dataBase = "DataElement.DataView.")
        {
            return base.GetDataBind(bindingPath, dataBase);
        }

        private bool IsButtonContainer(LayoutContainer container)
        {
            return (container.Controls.Any(e => e.ClassName == "Button") && !container.Controls.Any(e => e.ClassName != "Button"));
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

        private string GetColumnSpan(LayoutContainer parentContainer, LayoutElement element, bool putClassTag = true, bool joinInformationOfFont = false)
        {
            return GetColumnSpan(element, element.ColumnSpan, putClassTag, joinInformationOfFont);
        }

        private string GetColumnSpan(LayoutElement element, int columnSpan, bool putClassTag = true, bool joinInformationOfFont = false)
        {
            columnSpan = columnSpan == 0 ? columnSpanMax : columnSpan;
            string size = string.Format("col-lg-{0} col-md-{0} col-sm-12 col-xs-12", columnSpan);

            if (joinInformationOfFont && !element.IsNull())
            {
                var font = GetFont(element, false);
                if (!font.IsNullOrEmpty())
                    size += " " + font;
            }

            if (putClassTag)
                return "class=\"" + size + "\"";
            else
                return size;
        }

        #endregion

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
                    classFore = "text-error";
                    break;
                case FontForegroundStyle.Info:
                    classFore = "text-info";
                    break;
                case FontForegroundStyle.Muted:
                    classFore = "muted";
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

    }
}

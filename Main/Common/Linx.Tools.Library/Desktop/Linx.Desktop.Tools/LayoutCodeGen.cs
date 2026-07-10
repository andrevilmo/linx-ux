using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Tools
{
    public abstract class LayoutCodeGen<T> where T : CustomizedLayoutV2
    {

        private const int DataColumnGridMinWidth = 40;
        private const int MaxLenghtAllowed = 400;
        private const double CharWidth = 8.3;

        protected T _layOut;
        StringBuilder _codeBuilder;
        public bool SupportsDataGridTemplate { get; set; }
        public string EntityName { get; set; }
        public string ViewModelName { get; set; }
        private StringBuilder _complementaryCode;
        public StringBuilder ComplementaryCode { get { return _complementaryCode; } }
        private StringBuilder _complementaryCalls;
        public StringBuilder ComplementaryCalls { get { return _complementaryCalls; } }
        private Dictionary<CodeBuilder.EventName, StringBuilder> _complementaryEvents;
        public Dictionary<CodeBuilder.EventName, StringBuilder> ComplementaryEvents { get { return _complementaryEvents; } }
        public bool HasMainTopDataGrid { get; set; }


        public LayoutCodeGen(T layout)
        {
            _layOut = layout;
        }

        public LayoutCodeGen(T layout, string entityName, string viewModelName)
            : this(layout)
        {
            this.EntityName = entityName;
            this.ViewModelName = viewModelName;
        }

        public LayoutControlClass GetClassType(LayoutControlV2 control)
        {
            LayoutControlClass elClass;
            if (!Enum.TryParse<LayoutControlClass>(control.ClassName, true, out elClass))
                elClass = LayoutControlClass.TextBlock;

            return elClass;
        }

        public LayoutContainerClass GetClassType(LayoutContainer container)
        {
            LayoutContainerClass cntClass;
            if (!Enum.TryParse<LayoutContainerClass>(container.ClassName, true, out cntClass))
                cntClass = LayoutContainerClass.CustomContainer;

            return cntClass;
        }

        private void AddComplementaryCode(CodeBuilder code)
        {
            string cmpCalls = code.ComplementaryCalls.GetBody(), cmpCode = code.ComplementaryCode.GetBody();
            if (!cmpCalls.IsNullOrEmpty())
                this.ComplementaryCalls.AppendLine(cmpCalls);
            if (!cmpCode.IsNullOrEmpty())
                this.ComplementaryCode.AppendLine(cmpCode);
            if (code._complementaryEvents != null)
            {
                foreach (var e in code._complementaryEvents)
                {
                    if (!string.IsNullOrWhiteSpace(e.Value.ToString()))
                    {
                        if (_complementaryEvents.ContainsKey(e.Key))
                            _complementaryEvents[e.Key].AppendLine(e.Value.GetBody());
                        else
                            _complementaryEvents.Add(e.Key, new StringBuilder(e.Value.GetBody()));
                    }
                }
            }
        }

        private void ResetComplementaryCode()
        {
            _complementaryCode = new StringBuilder();
            _complementaryCalls = new StringBuilder();
            _complementaryEvents = new Dictionary<CodeBuilder.EventName, StringBuilder>();
        }

        public string GetCode(string rootIndent)
        {
            _codeBuilder = new StringBuilder();
            this.ResetComplementaryCode();

            Action<LayoutContainer, LayoutContainer, string, int> genCode = null;
            genCode = (parentElement, element, indent, cntIndex) =>
            {
                CodeBuilder code = new CodeBuilder(indent);
                LayoutContainerClass cntClass;
                if (!Enum.TryParse<LayoutContainerClass>(element.ClassName, true, out cntClass)) return;

                bool hasContainerStart = false;
                if (!cntClass.In(LayoutContainerClass.DataGrid, LayoutContainerClass.FlatPivotGrid, LayoutContainerClass.OlapPivotGrid,
                    LayoutContainerClass.TreeListView, LayoutContainerClass.PivotChart, LayoutContainerClass.PivotDrillDownChart, LayoutContainerClass.ExternalUI))
                {
                    #region control Not (DataGrid, FlatPivotGrid, OlapPivotGrid, TreeListView, PivotChart, PivotDrillDownChart)
                    int elementIndex = 0;
                    var containerMatrix = this.GetContainerMatrix(element);
                    if (containerMatrix.Length > 0)
                    {
                        int totalColumnContainer = containerMatrix.GetLength(1);
                        code.Clear();
                        hasContainerStart = true;
                        ComposeContainerStart(parentElement, element, cntClass, code, containerMatrix.GetLength(0), totalColumnContainer, null, cntIndex);
                        _codeBuilder.AppendLine(code.GetBody());
                        this.AddComplementaryCode(code);

                        for (int col = 0; col < totalColumnContainer; col++)
                        {
                            #region Column Iteration

                            code.Clear();
                            ComposeContainerStartColumn(element, cntClass, code, totalColumnContainer, 0);
                            _codeBuilder.AppendLine(code.GetBody());
                            this.AddComplementaryCode(code);

                            for (int row = 0; row < containerMatrix.GetLength(0); row++)
                            {
                                #region Row Iteration
                                if (containerMatrix[row, col] == null)
                                    continue;

                                code.Clear();
                                ComposeContainerStartRow(element, cntClass, code, row);
                                _codeBuilder.AppendLine(code.GetBody());
                                this.AddComplementaryCode(code);

                                #region Control generate
                                genCode(element, containerMatrix[row, col], indent + "    ", elementIndex);
                                #endregion Control generate

                                code.Clear();
                                ComposeContainerEndRow(element, cntClass, code, row);
                                _codeBuilder.AppendLine(code.GetBody());
                                this.AddComplementaryCode(code);
                                #endregion Row Iteration
                            }

                            elementIndex++;

                            code.Clear();
                            ComposeContainerEndColumn(element, cntClass, code, col);
                            _codeBuilder.AppendLine(code.GetBody());
                            this.AddComplementaryCode(code);
                            #endregion Column Iteration
                        }

                    }
                    else
                    {
                        var controlMatrix = this.GetControlMatrix(element);
                        int totalColumns = controlMatrix.GetLength(1);
                        if (controlMatrix.Length > 0)
                        {
                            code.Clear();
                            hasContainerStart = true;
                            ComposeContainerStart(parentElement, element, cntClass, code, controlMatrix.GetLength(0), totalColumns, null, cntIndex);
                            _codeBuilder.AppendLine(code.GetBody());
                            this.AddComplementaryCode(code);


                            for (int col = 0; col < totalColumns; col++)
                            {
                                if (controlMatrix[0, col] == null)
                                    continue;

                                code.Clear();
                                ComposeContainerStartColumn(element, cntClass, code, totalColumns, 0);
                                _codeBuilder.AppendLine(code.GetBody());
                                this.AddComplementaryCode(code);

                                for (int row = 0; row < controlMatrix.GetLength(0); row++)
                                {
                                    if (controlMatrix[row, col] == null)
                                        continue;
                                    code.Clear();
                                    ComposeContainerStartRow(element, cntClass, code, row);
                                    _codeBuilder.AppendLine(code.GetBody());
                                    this.AddComplementaryCode(code);

                                    LayoutControlClass elClass;
                                    if (Enum.TryParse<LayoutControlClass>(controlMatrix[row, col].ClassName, true, out elClass))
                                    {
                                        bool _isTemplate = false;
                                        if (element.ClassName == "CustomContainer")
                                            _isTemplate = element.IsInnerTemplate;
                                        else if (!parentElement.IsNullOrEmpty())
                                            _isTemplate = parentElement.IsInnerTemplate;

                                        code.Clear();
                                        ComposeControl(controlMatrix[row, col], elClass, code, false, this.GetConnectedControls(controlMatrix[row, col], element), element.LabelPosition == LabelPosition.Top, _isTemplate);

                                        _codeBuilder.AppendLine(code.GetBody());
                                        this.AddComplementaryCode(code);
                                    }
                                    code.Clear();
                                    ComposeContainerEndRow(element, cntClass, code, row);
                                    _codeBuilder.AppendLine(code.GetBody());
                                    this.AddComplementaryCode(code);

                                }
                                code.Clear();
                                ComposeContainerEndColumn(element, cntClass, code, col);
                                _codeBuilder.AppendLine(code.GetBody());
                                this.AddComplementaryCode(code);
                            }


                        }
                    }

                    #endregion control Not (DataGrid, FlatPivotGrid, OlapPivotGrid, TreeListView, PivotChart, PivotDrillDownChart)
                }
                else
                {
                    #region control equals (DataGrid, FlatPivotGrid, OlapPivotGrid, TreeListView, PivotChart, PivotDrillDownChart)
                    List<TreeLayoutContainer> innerDataGrids = null;
                    if (!SupportsDataGridTemplate && cntClass == LayoutContainerClass.DataGrid)
                        innerDataGrids = element.GetInnerContainersAsTree("DataGrid");

                    code.Clear();
                    hasContainerStart = true;
                    ComposeContainerStart(parentElement, element, cntClass, code, 0, 0, innerDataGrids, cntIndex);
                    _codeBuilder.AppendLine(code.GetBody());
                    this.AddComplementaryCode(code);

                    if (SupportsDataGridTemplate)
                    {
                        var containers = element.Controls.Where(e => e is LayoutContainer).Select(e => (LayoutContainer)e).ToList();
                        containers.ForEach(e => genCode(element, e, indent + "    ", containers.IndexOf(e)));
                    }
                    #endregion control equals (DataGrid, FlatPivotGrid, OlapPivotGrid, TreeListView, PivotChart, PivotDrillDownChart)
                }

                if (hasContainerStart)
                {
                    code.Clear();
                    ComposeContainerEnd(element, cntClass, code);
                    _codeBuilder.AppendLine(code.GetBody());
                    this.AddComplementaryCode(code);
                }
            };
            _layOut.Containers.ForEach(e => genCode(null, e, rootIndent, _layOut.Containers.IndexOf(e)));

            return _codeBuilder.ToString();
        }

        private Dictionary<LayoutControlV2, LayoutControlClass> GetConnectedControls(LayoutControlV2 propDef, LayoutContainer container)
        {
            Dictionary<LayoutControlV2, LayoutControlClass> result = new Dictionary<LayoutControlV2, LayoutControlClass>();
            List<LayoutControlV2> lc = container.Controls.Where(e => e is LayoutControlV2).Select(e => (LayoutControlV2)e).ToList();
            var connectedList = lc.Where(e => e.ConnectedAttribute == (propDef.DefinedUserName.IsNullOrEmpty() ? propDef.Name : propDef.DefinedUserName)).ToList();

            LayoutControlClass elClass;
            foreach (var connectedElement in connectedList)
            {
                if (Enum.TryParse<LayoutControlClass>(connectedElement.ClassName, true, out elClass))
                {
                    result.Add(connectedElement, elClass);
                }
            }

            return result;
        }

        private LayoutControlV2[,] GetControlMatrix(LayoutContainer lcontainer)
        {
            List<LayoutControlV2> lc = lcontainer.Controls.Where(e => e is LayoutControlV2).Select(e => (LayoutControlV2)e).ToList();
            List<LayoutControlV2> listProps = new List<LayoutControlV2>();

            for (int idx = 0; idx < lc.Count; idx++)
            {
                if (!lc[idx].ConnectedAttribute.IsNullOrEmpty())
                    continue;
                listProps.Add(lc[idx]);
            }

            //Get Columns Number
            int layOutColumns = (lcontainer.ColumnCount == 0 ? 1 : (lcontainer.ColumnCount > listProps.Count ? listProps.Count : lcontainer.ColumnCount));
            //Get Rows Number
            int rowsNumber = (layOutColumns > 0 ? ((int)((listProps.Count / layOutColumns) + ((listProps.Count % layOutColumns) == 0 ? 0 : 1))) : 0);

            //Fill Properties Matrix
            LayoutControlV2[,] matrix = new LayoutControlV2[rowsNumber, layOutColumns];
            int column = 0, row = 0;
            foreach (LayoutControlV2 prop in listProps)
            {
                matrix[row, column] = prop;
                row++;
                if (row == rowsNumber)
                {
                    row = 0;
                    column++;
                }
            }

            return matrix;
        }

        private LayoutContainer[,] GetContainerMatrix(LayoutContainer lc)
        {
            LayoutContainer[,] matrix = new LayoutContainer[0, 0];
            List<LayoutContainer> containers = lc.Controls.Where(e => e is LayoutContainer).Select(e => (LayoutContainer)e).ToList();
            List<LayoutControlV2> controls = lc.Controls.Where(e => e is LayoutControlV2).Select(e => (LayoutControlV2)e).ToList();

            if (containers.Count > 0)
            {
                int layOutColumns = containers.Count, rowsNumber = 1;
                matrix = new LayoutContainer[rowsNumber, layOutColumns];

                if (!lc.ClassName.InList("TabControl", "WizardControl", "DockManager"))
                {
                    layOutColumns = (lc.ColumnCount > 0 ? (lc.ColumnCount < containers.Count ? lc.ColumnCount : containers.Count) : 1);

                    //Get Rows Number
                    rowsNumber = ((int)((containers.Count / layOutColumns) + ((containers.Count % layOutColumns) == 0 ? 0 : 1)));

                    //Fill Containers Matrix
                    matrix = new LayoutContainer[rowsNumber, layOutColumns];
                    int column = 0, row = 0;
                    foreach (LayoutContainer container in containers)
                    {
                        matrix[row, column] = container;
                        row++;
                        if (row == rowsNumber)
                        {
                            row = 0;
                            column++;
                        }
                    }

                }
                else
                {
                    int column = 0, row = 0;
                    foreach (LayoutContainer container in containers)
                    {
                        matrix[row, column] = container;
                        row++;
                        if (row == rowsNumber)
                        {
                            row = 0;
                            column++;
                        }
                    }
                }

            }

            return matrix;
        }

        public virtual string GetDataBind(string bindingPath, string dataBase = "")
        {
            if (dataBase.IsNullOrEmpty())
                return bindingPath;
            else
                return bindingPath.Right(dataBase);
        }

        public virtual void ComposeContainerStart(LayoutContainer parentContainer, LayoutContainer container, LayoutContainerClass elementClass, CodeBuilder codeBuilder, int rows, int columns, List<TreeLayoutContainer> innerDataGrids, int index)
        {

        }

        public virtual void ComposeContainerStartRow(LayoutContainer container, LayoutContainerClass elementClass, CodeBuilder codeBuilder, int row)
        {

        }

        public virtual void ComposeContainerStartColumn(LayoutContainer container, LayoutContainerClass elementClass, CodeBuilder codeBuilder, int totalColumns, int columnSpan)
        {

        }

        public virtual void ComposeControl(LayoutControlV2 control, LayoutControlClass elementClass, CodeBuilder codeBuilder, bool isConnected, Dictionary<LayoutControlV2, LayoutControlClass> connectedControls, bool labelOnTop, bool isTemplate)
        {

        }

        public virtual void ComposeContainerEndColumn(LayoutContainer container, LayoutContainerClass elementClass, CodeBuilder codeBuilder, int column)
        {

        }

        public virtual void ComposeContainerEndRow(LayoutContainer container, LayoutContainerClass elementClass, CodeBuilder codeBuilder, int row)
        {

        }

        public virtual void ComposeContainerEnd(LayoutContainer container, LayoutContainerClass elementClass, CodeBuilder codeBuilder)
        {

        }

        private static int GetPrecisionDecimals(string precisionDescriptor)
        {
            int result = 0;

            if (!precisionDescriptor.IsNullOrEmpty())
            {
                if (precisionDescriptor.Contains(":"))
                    result = int.Parse(precisionDescriptor.Right(":"));
                else
                {
                    decimal precision = (!precisionDescriptor.IsNullOrEmpty() ? decimal.Parse(precisionDescriptor) / 10 : 0);
                    result = (int)(10 * (precision - ((int)precision)));
                }
            }

            return result;
        }

        private static int GetPrecision(string precisionDescriptor)
        {
            int result = 0;

            if (!precisionDescriptor.IsNullOrEmpty())
            {
                if (precisionDescriptor.Contains(":"))
                    result = int.Parse(precisionDescriptor.Left(":"));
                else
                {
                    decimal precision = (!precisionDescriptor.IsNullOrEmpty() ? decimal.Parse(precisionDescriptor) / 10 : 0);
                    result = ((int)precision);
                }
            }

            return result;
        }


        public static ControlWidth GetControlWidth(string className, string dataType, string displayName, string formatString, string precisionDescriptor)
        {
            int pxWidth = GetElementWidth(className, dataType, displayName, formatString, false, precisionDescriptor);

            if (pxWidth <= 45)
                return ControlWidth.Mini;
            else if (pxWidth <= 80)
                return ControlWidth.ExtraSmall;
            else if (pxWidth <= 120)
                return ControlWidth.Small;
            else if (pxWidth <= 180)
                return ControlWidth.MinMedium;
            else if (pxWidth <= 240)
                return ControlWidth.Medium;
            else if (pxWidth <= 280)
                return ControlWidth.ExtraMedium;
            else if (pxWidth <= 320)
                return ControlWidth.Large;
            else
                return ControlWidth.ExtraLarge;
        }

        public static int GetMaxWidth(ControlWidth controlWidth)
        {
            int width = 500;
            switch (controlWidth)
            {
                case ControlWidth.Mini:
                    width = 45;
                    break;
                case ControlWidth.ExtraSmall:
                    width = 80;
                    break;
                case ControlWidth.Small:
                    width = 120;
                    break;
                case ControlWidth.MinMedium:
                    width = 180;
                    break;
                case ControlWidth.Medium:
                    width = 240;
                    break;
                case ControlWidth.ExtraMedium:
                    width = 280;
                    break;
                case ControlWidth.Large:
                    width = 320;
                    break;
            }
            return width;
        }

        public static int GetElementWidth(string className, string dataType, string displayName, string formatString, bool isDataGrid, string precisionDescriptor, bool useNewGenerator = false)
        {
            if (useNewGenerator && isDataGrid)
                return GetColumnWithNewGrid(className, dataType, displayName, formatString, isDataGrid, precisionDescriptor);

            string objectClass = "Linx" + className;
            int width = 0, lengthPrecision;

            lengthPrecision = GetPrecision(precisionDescriptor);

            if (objectClass == "LinxKpiBox")
                width = 150;
            else if (objectClass == "LinxMultimediaControl")
            {
                if (isDataGrid)
                {
                    switch (formatString)
                    {
                        case "Small": width = 85; break;
                        case "Medium": width = 115; break;
                        case "Large": width = 145; break;
                    }
                }
                else
                    width = 50;
            }
            else if (objectClass == "LinxComboBox")
            {
                width = 200;
            }
            else if (!objectClass.InList("LinxCheckBox", "LinxButton", "LinxLabel"))
            {
                width = 250;
                dataType = dataType.RemoveNullDefinition();

                if (dataType.InList(new string[] { "byte", "int16", "int32", "int64", "sbyte", "uint16", "uint32", "uint64", "double", "float", "decimal" }))
                {
                    width = (lengthPrecision > 0 ? Math.Min(400, (10 * (lengthPrecision + 1))) : 250);
                    if (width < 40)
                        width = 40;
                }
                else if (dataType.Contains("datetime"))
                    switch (formatString)
                    {
                        case "d": width = 120; break;
                        case "D": width = 250; break;
                        case "t": width = 80; break;
                        case "T": width = 88; break;
                        case "g": width = 135; break;
                        case "G": width = 135; break;
                        default:
                            if (formatString.Length <= 1)
                                width = 120;
                            else
                                width = formatString.Length * 10;
                            break;
                    }

                else if (dataType.Contains("string"))
                {
                    width = (lengthPrecision > 0 ? (Math.Min(400, 10 * lengthPrecision)) : 250);
                    if (width < 40) width = 40;
                }
                else if (dataType.Contains("char"))
                    width = 40;

                if (objectClass == "LinxLookUpTextBox")
                    width = (width + 21);

            }
            else if (!displayName.IsNullOrEmpty())
            {
                width = Math.Min(400, 10 * displayName.Length);
                if (width < 40)
                    width = 40;

                if (objectClass == "LinxButton")
                    width = (width + 20);

                if (objectClass == "LinxCheckBox")
                    width = (width + 40);
            }

            if (isDataGrid && width < ((displayName.Length * 13) + 36) && objectClass != "LinxMultimediaControl")
                width = (displayName.Length * 13) + 36;

            if (isDataGrid && width < DataColumnGridMinWidth)
                width = DataColumnGridMinWidth;


            return width;
        }

        public static int GetColumnWithNewGrid(string className, string dataType, string displayName, string formatString, bool isDataGrid, string precisionDescriptor)
        {
            string objectClass = "Linx" + className;
            int width = 0, lengthPrecision;

            lengthPrecision = GetPrecision(precisionDescriptor);

            if (objectClass == "LinxKpiBox")
                width = 150;
            else if (objectClass == "LinxMultimediaControl")
            {
                if (isDataGrid)
                {
                    switch (formatString)
                    {
                        case "Small": width = 85; break;
                        case "Medium": width = 115; break;
                        case "Large": width = 145; break;
                    }
                }
                else
                    width = 50;
            }
            else if (objectClass == "LinxComboBox")
            {
                width = 200;
            }
            else if (!objectClass.InList("LinxCheckBox", "LinxButton", "LinxLabel"))
            {
                width = 250;
                dataType = dataType.RemoveNullDefinition();

                if (dataType.InList(new string[] { "byte", "int16", "int32", "int64", "sbyte", "uint16", "uint32", "uint64", "double", "float", "decimal" }))
                {
                    width = (lengthPrecision > 0 ? Math.Min(MaxLenghtAllowed, GetWidthChar(lengthPrecision)) : 250);
                    if (width < 40)
                        width = 40;
                }
                else if (dataType.Contains("datetime"))
                    switch (formatString)
                    {
                        case "d": width = 120; break;
                        case "D": width = 250; break;
                        case "t": width = 80; break;
                        case "T": width = 88; break;
                        case "g": width = 135; break;
                        case "G": width = 135; break;
                        default:
                            if (formatString.Length <= 1)
                                width = 120;
                            else
                                width = GetWidthChar(formatString.Length);
                            break;
                    }

                else if (dataType.Contains("string"))
                {
                    width = (lengthPrecision > 0 ? (Math.Min(MaxLenghtAllowed, GetWidthChar(lengthPrecision))) : 250);
                    if (width < 40) width = 40;
                }
                else if (dataType.Contains("char"))
                    width = 40;

                if (objectClass == "LinxLookUpTextBox")
                    width = (width + 21);

            }
            else if (!displayName.IsNullOrEmpty())
            {
                width = Math.Min(MaxLenghtAllowed, GetWidthChar(displayName.Length, true));
                if (width < 40)
                    width = 40;

                if (objectClass == "LinxButton")
                    width = (width + 20);
            }

            if (isDataGrid && width < GetWidthChar(displayName.Length, true) && objectClass != "LinxMultimediaControl")
                width = GetWidthChar(displayName.Length, true);

            if (isDataGrid && width < DataColumnGridMinWidth)
                width = DataColumnGridMinWidth;


            return width;
        }

        private static int GetWidthChar(int lenght, bool isHeader = false)
        {
            if (isHeader)
                return (int)Math.Ceiling(lenght * CharWidth * .2);
            else
                return (int)Math.Ceiling(lenght * CharWidth * .1);
        }
    }
    /// <summary>
    /// Create Code
    /// </summary>
    public class BaseCodeBuilder
    {
        const int _INDENT_SIZE = 4;

        protected string _indent;
        StringBuilder _builder;


        public BaseCodeBuilder() : this(String.Empty) { }

        public BaseCodeBuilder(string indent)
        {
            _builder = new StringBuilder();
            _indent = indent;

        }

        public void Load(string allText)
        {
            _builder = new StringBuilder(allText);
        }

        public void Add(string value, params object[] args)
        {
            _builder.Append((args.Length > 0 ? String.Format(value, args) : value));
        }

        public void AddLine(string line = "", params object[] args)
        {
            _builder.AppendLine(_indent + (args.Length > 0 ? String.Format(line, args) : line));
        }

        public void AddLines(string lines)
        {
            using (StringReader sr = new StringReader(lines))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    AddLine(line);
                }
            }

        }
        public void AddLines(string[] lines)
        {
            foreach (var line in lines)
                AddLine(line);
        }
        public string GetBody()
        {
            return _builder.ToString();
        }

        public virtual void Clear()
        {
            _builder.Clear();
        }

        public bool HasContent()
        {
            return _builder.Length > 0;
        }

        public void IncreaseIndent()
        {
            _indent += new string(' ', _INDENT_SIZE);
        }

        public void DecreaseIndent()
        {
            if (_indent.Length > _INDENT_SIZE)
                _indent = new string(' ', _indent.Length - _INDENT_SIZE);
            else
                _indent = string.Empty;
        }

        public override string ToString()
        {
            return GetBody();
        }
    }

    /// <summary>
    /// Create Code
    /// </summary>
    public class CodeBuilder : BaseCodeBuilder
    {
        public enum EventName { ChangedBrand }


        private BaseCodeBuilder _complementaryCode;
        public BaseCodeBuilder ComplementaryCode { get { if (_complementaryCode == null) { _complementaryCode = new BaseCodeBuilder(); } return _complementaryCode; } }

        private BaseCodeBuilder _complementaryCalls;
        public BaseCodeBuilder ComplementaryCalls { get { if (_complementaryCalls == null) { _complementaryCalls = new BaseCodeBuilder(); _complementaryCalls.IncreaseIndent(); } return _complementaryCalls; } }

        internal Dictionary<EventName, BaseCodeBuilder> _complementaryEvents;

        public CodeBuilder() : this(String.Empty) { }

        public CodeBuilder(string indent) : base(indent) { }


        #region Event Codes
        public void AddEventCode(EventName eventName, string code)
        {
            if (_complementaryEvents.IsNull())
                _complementaryEvents = new Dictionary<EventName, BaseCodeBuilder>();

            if (_complementaryEvents.ContainsKey(eventName))
            {
                _complementaryEvents[eventName].AddLine(code);
            }
            else
            {
                _complementaryEvents.Add(eventName, new BaseCodeBuilder(this._indent));
                _complementaryEvents[eventName].AddLine(code);
            }
        }
        public BaseCodeBuilder GetEvent(EventName eventName)
        {
            return HasEvent(eventName) ? _complementaryEvents[eventName] : new BaseCodeBuilder();
        }
        public bool HasEvent(EventName eventName)
        {
            return !_complementaryEvents.IsNull() && _complementaryEvents.ContainsKey(eventName);
        }
        #endregion

        public override void Clear()
        {
            base.Clear();
            if (_complementaryCalls != null)
                _complementaryCalls.Clear();
            if (_complementaryCode != null)
                _complementaryCode.Clear();
            if (_complementaryEvents != null)
                _complementaryEvents.Clear();
        }
    }
}

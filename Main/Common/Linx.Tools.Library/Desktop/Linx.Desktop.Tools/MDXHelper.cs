using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Tools
{
    public class MDXHelper
    {
        #region Fields

        private string _cubeName;
        private OlapItemInfo[] _metadata;
        private MDXQueryFilterBuilder _builder;

        private List<string> _selectedColumns;
        private List<string> _selectedRows;
        private Dictionary<string, string> _selectedMeasures;
        private List<string> _metaDataFields;
        private string _subquery;

        private int _idLinx;
        private int _idGpEcon;
        private int[] _idFiliais;

        private string[] _idLinxDimensions;
        private string[] _idGpeconDimensions;
        private string[] _idBandeiraRedeDimensions;
        private string[] _idFilialDimensions;

        private Dictionary<string, string> _measuresDimensions = new Dictionary<string, string>();

        #endregion

        #region Ctor

        public MDXHelper(string cubeName)
            : this(cubeName, null) { }

        public MDXHelper(string cubeName, OlapItemInfo[] metadata)
        {
            this._metadata = metadata;
            this._cubeName = cubeName;
        }

        #endregion

        #region Fluent Interface Methods

        public MDXHelper SetIdLinxDimensions(string idLinxDimensions)
        {
            this._idLinxDimensions = idLinxDimensions.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            return this;
        }
        public MDXHelper SetIdGpeconDimensions(string idGpeconDimensions)
        {
            this._idGpeconDimensions = idGpeconDimensions.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            return this;
        }
        public MDXHelper SetIdBandeiraRedeDimensions(string idBandeiraRedeDimensions)
        {
            this._idBandeiraRedeDimensions = idBandeiraRedeDimensions.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            return this;
        }
        public MDXHelper SetIdFilialDimensions(string idFilialDimensions)
        {
            this._idFilialDimensions = idFilialDimensions.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            return this;
        }
        public MDXHelper SetMeasuresDimensions(string measuresDimensions)
        {
            var splittedItems = measuresDimensions.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in splittedItems)
            {
                var indDelimter = item.IndexOf('#');
                this._measuresDimensions.Add(item.Substring(0, indDelimter), item.Substring(indDelimter + 1));
            }
            return this;
        }


        public MDXHelper SetIdGpEcon(int idGpEcon)
        {
            this._idGpEcon = idGpEcon;
            return this;
        }
        public MDXHelper SetIdLinx(int idLinx)
        {
            this._idLinx = idLinx;
            return this;
        }

        public MDXHelper SetIdFiliais(int[] idFiliais)
        {
            this._idFiliais = idFiliais;
            return this;
        }

        public MDXHelper Columns(string columns)
        {
            return Columns(columns.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries));
        }
        public MDXHelper Columns(params string[] columns)
        {
            var _list = new List<string>();
            if (_selectedColumns != null)
                _list.AddRange(_selectedColumns);
            _list.AddRange(columns);
            _selectedColumns = _list;

            return this;
        }

        public MDXHelper Rows(string rows)
        {
            return Rows(rows.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries));
        }
        public MDXHelper Rows(params string[] rows)
        {
            var _list = new List<string>();
            if (_selectedRows != null)
                _list.AddRange(_selectedRows);
            _list.AddRange(rows);
            _selectedRows = _list;
            return this;
        }


        public MDXHelper FilterMetaData(params string[] metaDataFields)
        {
            if (metaDataFields != null)
                this._metaDataFields = metaDataFields.ToList();
            return this;
        }

        /// <summary>
        /// Add measures
        /// </summary>
        /// <param name="measures"></param>
        /// <returns></returns>
        /// <remarks>Pattern is "data key|Alias"</remarks>
        public MDXHelper Measures(params string[] measures)
        {
            Dictionary<string, string> dicMeasures = new Dictionary<string, string>();

            string formule, alias;
            string[] splitTemp;
            foreach (var m in measures)
            {
                if (m.IsNullOrEmpty()) continue;
                splitTemp = m.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                formule = splitTemp[0];
                alias = splitTemp.Length > 1 ? splitTemp[1] : formule;

                dicMeasures.Add(alias, formule);
            }

            return Measures(dicMeasures);
        }

        public MDXHelper Measures(Dictionary<string, string> measures)
        {
            if (_selectedMeasures == null)
                _selectedMeasures = new Dictionary<string, string>();

            if (measures != null)
                measures.Foreach(i =>
                    _selectedMeasures.Add(i.Key, i.Value));

            return this;
        }


        public MDXHelper GenericItems(params OlapItemInfo[] genericItems)
        {
            foreach (var item in genericItems)
            {
                AddGenericItem(item);
            }
            return this;
        }

        public MDXHelper GenericItems(string genericItems)
        {
            return GenericItems(genericItems.Split(new char[] { ',', ';' }));
        }

        public MDXHelper GenericItems(params string[] genericItems)
        {
            foreach (var item in genericItems)
            {
                AddGenericItem(item);
            }

            return this;
        }

        public MDXHelper SubqueryFilter(string subquery)
        {
            this._subquery = subquery;
            return this;
        }


        public MDXHelper Where(MDXQueryFilterBuilder builder)
        {
            this._builder = builder;

            return this;
        }

        #endregion

        #region Public Methods

        public void ResetData()
        {
            _selectedColumns = null;
            _selectedRows = null;
            _selectedMeasures = null;
        }

        public string GetCommand(MDXQuerySettings settings = null)
        {
            CodeBuilder codeBuilder = new CodeBuilder();
            string[] allDimensionsInQuery = null;

            //filtra somente os metadados permitidos
            if (this._metaDataFields != null && this._metaDataFields.Count > 0)
            {
                #region Remove fields denied
                Action<List<string>> removelist = (lista) =>
                {
                    if (lista != null)
                    {
                        foreach (string field in lista.ToArray())
                        {
                            if (!_metaDataFields.Contains(field))
                                lista.Remove(field);
                        }
                    }
                };
                Action<Dictionary<string, string>> removeDict = (lista) =>
                {
                    if (lista != null)
                    {
                        foreach (var field in lista.ToArray())
                        {
                            if (!_metaDataFields.Contains(field.Key) && !_metaDataFields.Contains(field.Value))
                                lista.Remove(field.Key);
                        }
                    }
                };
                removelist(_selectedColumns);
                removelist(_selectedRows);
                removeDict(_selectedMeasures);

                #endregion
            }

            if (_builder != null && _builder.HasConditions)
            {
                _builder.RemoveBetweens();

                #region Verify if property will filter in select
                if (_selectedRows != null)
                    for (int i = 0; i < _selectedRows.Count; i++)
                    {
                        _selectedRows[i] = _builder.VerifyAndRemoveProperty(_selectedRows[i]);
                    }
                if (_selectedColumns != null)
                    for (int i = 0; i < _selectedColumns.Count; i++)
                    {
                        _selectedColumns[i] = _builder.VerifyAndRemoveProperty(_selectedColumns[i]);
                    }
                #endregion
            }


            //Get Measures Members
            if (this._selectedMeasures != null && this._selectedMeasures.Count > 0)
            {
                codeBuilder.AddLine("WITH ");
                codeBuilder.IncreaseIndent();
                foreach (var measureItem in _selectedMeasures)
                {
                    codeBuilder.AddLine("MEMBER Measures.{0} \t AS ( {1} )", measureItem.Key, measureItem.Value);
                }

                codeBuilder.DecreaseIndent();

                codeBuilder.AddLine(string.Empty);
            }

            codeBuilder.AddLine("SELECT ");

            if (_selectedMeasures != null && _selectedMeasures.Count > 0)
            {
                codeBuilder.IncreaseIndent();
                if (!settings.IsNull() && settings.NonEmptyColumns)
                    codeBuilder.AddLine(" NON EMPTY ");
                codeBuilder.AddLine(" { " + string.Join(", [Measures].", _selectedMeasures.Keys) + " } ON COLUMNS");
                codeBuilder.DecreaseIndent();
            }
            else
                codeBuilder.AddLine(" { } ON COLUMNS");

            var columns = ColumnsJoined();
            if (columns != null && columns.Length > 0)
            {
                codeBuilder.IncreaseIndent();
                codeBuilder.AddLine(",");
                if (_selectedMeasures != null && _selectedMeasures.Count > 0 && !settings.IsNull() && settings.NonEmptyRows)
                    codeBuilder.AddLine(" NON EMPTY ");
                string items = string.Join(" ,  ", columns);
                string filterMeasures = _builder.GetMeasuresFilter();
                if (filterMeasures.IsNullOrEmpty())
                    codeBuilder.AddLine(" { ( " + items + " ) } ON ROWS");
                else
                    codeBuilder.AddLine(" { FILTER( ( " + items + " ), " + filterMeasures + ") } ON ROWS");
                codeBuilder.DecreaseIndent();
            }

            var cubeNameWithBrackets = "[" + _cubeName + "]";
            var from = string.Format("FROM {0}", !_subquery.IsNullOrEmpty() ? "( " + _subquery + " )" : cubeNameWithBrackets);

            codeBuilder.AddLine(from);

            allDimensionsInQuery = GetAllDimensionsInQuery(columns);

            Trace.WriteLine("allDimensionsInQuery:" + string.Join(",", allDimensionsInQuery));
            Trace.WriteLine("_idLinxDimensions:" + string.Join(",", _idLinxDimensions));
            Trace.WriteLine("_idGpeconDimensions:" + string.Join(",", _idGpeconDimensions));
            Trace.WriteLine("_idBandeiraRedeDimensions:" + string.Join(",", _idBandeiraRedeDimensions));
            Trace.WriteLine("_idFilialDimensions:" + string.Join(",", _idFilialDimensions.IsNullOrEmpty() ? new string[] { } : _idFilialDimensions));

            if (this._idLinx > 0 && _idLinxDimensions != null && _idLinxDimensions.Length > 0)
            {
                foreach (var dim in allDimensionsInQuery)
                {
                    if (_idLinxDimensions.Any(d => dim.Equals(d.Replace("[", "").Replace("]", ""))))
                    {
                        if (!_builder.HasFilterFor(dim, "ID_LINX"))
                            _builder.Condition(f => f.Eq(new MDXField(string.Format("[{0}].[ID_LINX]", dim)), _idLinx.ToString()));
                    }
                }
            }



            if (this._idGpEcon != this._idLinx && this._idGpEcon > 0 && _idGpeconDimensions != null && _idGpeconDimensions.Length > 0)
            {
                foreach (var dim in allDimensionsInQuery)
                {
                    if (_idGpeconDimensions.Any(d => dim.Equals(d.Replace("[", "").Replace("]", ""))))
                    {
                        if (!_builder.HasFilterFor(dim, "ID_GPECON"))
                            _builder.Condition(f => f.Eq(new MDXField(string.Format("[{0}].[ID_GPECON]", dim)), _idGpEcon.ToString()));
                    }
                }
            }

            var idBandeiraRede = _builder.GetBrandId();
            if (idBandeiraRede > 0 && _idBandeiraRedeDimensions != null && _idBandeiraRedeDimensions.Length > 0)
            {
                foreach (var dim in allDimensionsInQuery)
                {
                    if (_idBandeiraRedeDimensions.Any(d => dim.Equals(d.Replace("[", "").Replace("]", ""))))
                    {
                        if (!_builder.HasFilterFor(dim, "ID_BANDEIRA_REDE"))
                            _builder.Condition(f => f.Eq(new MDXField(string.Format("[{0}].[ID_BANDEIRA_REDE]", dim)), idBandeiraRede.ToString()));
                    }
                }
            }

            if (_idFilialDimensions != null && _idFiliais.Count() > 0)
            {
                foreach (var dim in allDimensionsInQuery)
                {
                    if (_idFilialDimensions.Any(d => dim.Equals(d.Replace("[", "").Replace("]", ""))))
                    {
                        if (!_builder.HasFilterFor(dim, "ID_FILIAL_PFJ"))
                            _builder.Condition(f => f.EqListInt(new MDXField(string.Format("[{0}].[ID_FILIAL_PFJ]", dim)), _idFiliais));
                    }
                }
            }

            if (_builder != null && _builder.HasConditions && !_builder.ToScript().IsNullOrEmpty())
            {
                codeBuilder.AddLine("Where ({0}) ", _builder.ToScript());
            }


            string commandResult = codeBuilder.GetBody();
            Trace.WriteLine(commandResult);

            return commandResult;
        }

        #endregion

        #region Private Methods

        private string[] GetAllMeasuresInQuery()
        {
            var allMeasuresInQuery = new List<string>();
            var filters = _builder.GetAllFields(true);
            if (!_selectedMeasures.IsNull() && _selectedMeasures.Count > 0)
                allMeasuresInQuery.AddRange(_selectedMeasures.Values);
            if (!filters.IsNull() && filters.Count > 0)
                allMeasuresInQuery.AddRange(filters.Where(m => m.MDX.Contains("[Measures].")).Select(m => m.MDX));

            return allMeasuresInQuery.Where(m => m.Contains("[Measures].")).Select(m => "[Measures].[" + m.Extract("[Measures].[", "]") + "]").Distinct().ToArray();
        }

        private string[] DimensionMeasureInQuery()
        {
            List<string> measuresInQuery = new List<string>();
            var selectedMeasures = GetAllMeasuresInQuery();

            foreach (var item in _measuresDimensions)
            {
                if (item.Value.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Any(m => selectedMeasures.Contains(m)))
                    measuresInQuery.Add(item.Key);
            }

            return measuresInQuery.ToArray();
        }

        private string[] GetAllDimensionsInQuery(string[] columns)
        {
            var allDimensionsInQuery = new List<string>();
            var filters = _builder.GetAllFields(false);
            var measureDim = DimensionMeasureInQuery();

            if (!columns.IsNull() && columns.Count() > 0)
                allDimensionsInQuery.AddRange(columns);
            if (!filters.IsNull() && filters.Count > 0)
                allDimensionsInQuery.AddRange(filters.Select(m => m.MDX));
            if (!measureDim.IsNull() && measureDim.Length > 0)
                allDimensionsInQuery.AddRange(measureDim);

            return allDimensionsInQuery.Select(i => i.Extract("[", "]")).Distinct().ToArray();
        }

        private string[] ColumnsJoined()
        {
            if (_selectedRows == null)
                _selectedRows = new List<string>();
            if (_selectedColumns == null)
                return _selectedRows.ToArray();
            return _selectedRows.Union(_selectedColumns).ToArray();
        }

        private string[] SelectedDimensions()
        {
            var columns = ColumnsJoined();
            List<string> dimensions = new List<string>();
            string dimension = null;

            foreach (var item in columns)
            {
                dimension = item.Extract("[", "]");
                if (!dimensions.Contains(dimension))
                    dimensions.Add(dimension);
            }

            return dimensions.ToArray();
        }

        private void AddGenericItem(string item)
        {
            OlapItemInfo cubeItem = _metadata.FirstOrDefault(i => i.Name == item);

            AddGenericItem(cubeItem);
        }

        private void AddGenericItem(OlapItemInfo item)
        {
            if (item.OlapItemType == OlapItemEnum.Measure)
                Measures(item.UniqueName);
            else
                Columns(item.UniqueName);
        }

        #endregion
    }

    public class MDXField
    {
        public MDXField(string uniqueName) : this(uniqueName, uniqueName, false) { }

        public MDXField(string name, string uniqueName, bool isMeasure)
        {
            this.Name = name;
            this.MDX = uniqueName;
            this.IsMeasure = isMeasure;
        }

        public string Name { get; set; }
        public string MDX { get; set; }
        public bool IsMeasure { get; set; }
    }

    public class MDXQueryFilterBuilder
    {
        #region Fields

        List<MDXQueryFilterItem> _filters = new List<MDXQueryFilterItem>();
        IEnumerable<MDXField> _fieldsMap;
        List<EntitySearchExpression> _expressions;
        #endregion

        #region Ctor

        public MDXQueryFilterBuilder()
        {
        }

        public MDXQueryFilterBuilder(IEnumerable<MDXField> fieldsMap)
        {
            _fieldsMap = fieldsMap;
        }

        #endregion

        #region Public Methods

        public List<MDXField> GetAllFields(bool isMeasure)
        {
            return _fieldsMap.IsNull() ? null : _fieldsMap.Where(f => f.IsMeasure = isMeasure).ToList();
        }

        public int GetBrandId()
        {
            var filter = _filters.FirstOrDefault(i => i.Field.Name == "IdBandeiraRede");
            int brandValue = 0;
            if (!filter.IsNull())
            {
                int.TryParse(filter.Value, out brandValue);
                _filters.Remove(filter);
            }
            return brandValue;
        }

        public MDXField GetMdxField(string name)
        {
            if (_fieldsMap != null && _fieldsMap.Any(f => f.Name == name))
                return _fieldsMap.First(f => f.Name == name);
            else
                return null;
        }

        private string TreatData(object value)
        {
            if (value is DateTime)
                return ((DateTime)value).ToString("yyyy-MM-dd") + "T00:00:00";
            if (value is decimal)
                return ((decimal)value).ToString(CultureInfo.InvariantCulture);
            if (value is float)
                return ((float)value).ToString(CultureInfo.InvariantCulture);

            return value.ToString();
        }

        public string GetMeasuresFilter()
        {
            if (_expressions == null || _expressions.Count == 0)
                return null;

            List<string> filterMeasures = new List<string>();
            int indexExp = 0;
            MDXField fieldOlapName = null;
            string olapValue = string.Empty;
            MDXQueryFilterItem.FilterOperation olapOperator = MDXQueryFilterItem.FilterOperation.Equal;
            EntitySearchExpressionName expType;

            while (indexExp < _expressions.Count)
            {
                expType = (EntitySearchExpressionName)Enum.Parse(typeof(EntitySearchExpressionName), _expressions[indexExp].Name);

                switch (expType)
                {
                    case EntitySearchExpressionName.Field:
                        fieldOlapName = GetMdxField(_expressions[indexExp].Value.ToString());
                        break;
                    case EntitySearchExpressionName.Operator:
                        olapOperator = GetOperation(_expressions[indexExp].Value.ToString());
                        break;
                    case EntitySearchExpressionName.Value:
                        olapValue = TreatData(_expressions[indexExp].Value);
                        if (fieldOlapName != null && fieldOlapName.IsMeasure)
                        {
                            var filterItem = new MDXQueryFilterItem(this).GenericOperation(fieldOlapName, olapOperator, olapValue);
                            if (filterItem != null)
                                filterMeasures.Add(filterItem.ToString());
                        }
                        break;

                    case EntitySearchExpressionName.Condition:
                        break;
                    case EntitySearchExpressionName.PredefinedFilter:
                        break;
                    default:
                        break;
                }

                indexExp++;
            }

            return string.Join(" and ", filterMeasures);
        }

        public MDXQueryFilterBuilder Conditions(List<EntitySearch> entitySearch)
        {
            if (entitySearch != null && entitySearch.Count > 0)
            {
                List<EntitySearchExpression> expressionSearch = new List<EntitySearchExpression>();
                string entityName = entitySearch[0].EntityName;
                foreach (var search in entitySearch.Where(e => e.EntityName == entityName))
                {
                    expressionSearch.AddRange(search.Expressions);
                }
                return Conditions(expressionSearch);
            }
            else
                return null;
        }

        public MDXQueryFilterBuilder Conditions(List<EntitySearchExpression> expressionSearch)
        {
            if (expressionSearch == null || expressionSearch.Count == 0)
                return this;

            _expressions = expressionSearch;
            int indexExp = 0;
            MDXField fieldOlapName = null;
            string olapValue = string.Empty;
            MDXQueryFilterItem.FilterOperation olapOperator = MDXQueryFilterItem.FilterOperation.Equal;
            EntitySearchExpressionName expType;

            while (indexExp < _expressions.Count)
            {
                expType = (EntitySearchExpressionName)Enum.Parse(typeof(EntitySearchExpressionName), _expressions[indexExp].Name);
                if (!_expressions[indexExp].Excluded)
                {
                    switch (expType)
                    {
                        case EntitySearchExpressionName.Field:
                            fieldOlapName = GetMdxField(_expressions[indexExp].Value.ToString());
                            break;
                        case EntitySearchExpressionName.Operator:
                            olapOperator = GetOperation(_expressions[indexExp].Value.ToString());
                            break;
                        case EntitySearchExpressionName.Value:
                            olapValue = TreatData(_expressions[indexExp].Value);
                            if (fieldOlapName != null && !fieldOlapName.IsMeasure)
                            {
                                if (!_expressions[indexExp].Excluded || fieldOlapName.Name == "IdBandeiraRede")
                                {
                                    var filterItem = new MDXQueryFilterItem(this).GenericOperation(fieldOlapName, olapOperator, olapValue);
                                    if (filterItem != null)
                                        _filters.Add(filterItem);
                                }
                            }
                            break;

                        case EntitySearchExpressionName.Condition:
                            break;
                        case EntitySearchExpressionName.PredefinedFilter:
                            break;
                        default:
                            break;
                    }
                }
                indexExp++;
            }

            return this;
        }

        public MDXQueryFilterBuilder Condition(Func<MDXQueryFilterItem, MDXQueryFilterItem> condition)
        {
            var item = condition(new MDXQueryFilterItem(this));

            _filters.Add(item);
            return this;
        }

        public MDXQueryFilterBuilder And(Func<MDXQueryFilterBuilder, MDXQueryFilterItem> conditionA, Func<MDXQueryFilterBuilder, MDXQueryFilterItem> conditionB)
        {
            var itemA = conditionA(this);
            var itemB = conditionB(this);

            _filters.Add(itemA);
            _filters.Add(itemB);
            return this;
        }

        //public OlapQueryBuilder Parenteses(Func<OlapQueryBuilder, OlapQueryBuilder> buider)
        //{
        //    var item = buider(this);
        //    filters.Enqueue(string.Format(" ( {0} ) ", item));
        //    return this;
        //}

        public string ToScript()
        {
            if (!this.HasConditions) return null;

            List<string> conditions = new List<string>();
            //verificar os dulicados
            foreach (var filter in _filters)
                conditions.Add(filter.ToString());

            return "{" + string.Join("}, {", conditions.ToArray()) + "}";
        }

        public bool HasConditions { get { return _filters.Count > 0; } }

        public bool HasFilterFor(string dimension, string property)
        {
            var mdx = string.Format("[{0}].[{1}]", dimension, property);
            return this._filters.Any(f => f.Field.MDX.Contains(mdx));
        }

        #endregion

        #region Private Methods

        internal string VerifyAndRemoveProperty(string uniqueName)
        {
            var items = _filters.Where(f => f.Exists(uniqueName)).ToArray();
            if (items == null || items.Count() == 0) return uniqueName;

            var item = items.First();
            for (int i = 0; i < items.Length; i++)
                _filters.Remove(items[i]);

            return item.ToString();
        }


        public void RemoveBetweens()
        {
            var items = _filters.GroupBy(f => f.Field);

            foreach (var item in items)
            {
                if (item.Count() == 2)
                {
                    var range = item.OrderBy(i => i.Operation).ToArray();
                    if (range[0].Operation.In(MDXQueryFilterItem.FilterOperation.GreaterThan, MDXQueryFilterItem.FilterOperation.GreaterThanEq)
                     && range[1].Operation.In(MDXQueryFilterItem.FilterOperation.LessThan, MDXQueryFilterItem.FilterOperation.LessThanEq)
                        )
                    {
                        var between = new MDXQueryFilterItem(this).Between(range[0].Field, range[0].Value, range[1].Value);
                        _filters.Remove(range[0]);
                        _filters.Remove(range[1]);
                        _filters.Add(between);
                    }
                }
            }

        }

        private MDXQueryFilterItem.FilterOperation GetOperation(string operation)
        {
            MDXQueryFilterItem.FilterOperation op = MDXQueryFilterItem.FilterOperation.Equal;
            switch (operation)
            {
                case "==":
                    op = MDXQueryFilterItem.FilterOperation.Equal;
                    break;
                case "!=":
                    op = MDXQueryFilterItem.FilterOperation.NotEqual;
                    break;
                case ">":
                    op = MDXQueryFilterItem.FilterOperation.GreaterThan;
                    break;
                case ">=":
                    op = MDXQueryFilterItem.FilterOperation.GreaterThanEq;
                    break;
                case "<":
                    op = MDXQueryFilterItem.FilterOperation.LessThan;
                    break;
                case "<=":
                    op = MDXQueryFilterItem.FilterOperation.LessThanEq;
                    break;
                case "Like":
                    op = MDXQueryFilterItem.FilterOperation.Like;
                    break;
                case "!Like":
                    op = MDXQueryFilterItem.FilterOperation.NotLike;
                    break;
                case "In":
                    op = MDXQueryFilterItem.FilterOperation.In;
                    break;
                case "!In":
                    op = MDXQueryFilterItem.FilterOperation.NotIn;
                    break;
                default:
                    break;
            }
            return op;
        }


        #endregion


    }

    public class MDXQueryFilterItem
    {
        #region Properties

        public MDXField Field { get; internal set; }
        public FilterOperation Operation { get; internal set; }
        public string Value { get; internal set; }
        public string SecondaryValue { get; internal set; }

        #endregion
        #region Fields

        readonly MDXQueryFilterBuilder Parent;
        private string queryToken;

        #endregion

        #region Ctor

        public MDXQueryFilterItem(MDXQueryFilterBuilder parent)
        {
            this.Parent = parent;
        }

        #endregion

        #region Public Methods

        #region IsNotEmpty
        public MDXQueryFilterItem IsNotEmpty(MDXField mdxField)
        {
            Field = mdxField; Operation = FilterOperation.IsNotEmpty; Value = null;
            queryToken = string.Format(" {0} ", mdxField.MDX);
            return this;
        }
        #endregion
        #region Not Eq
        public MDXQueryFilterItem NotEq(MDXField mdxField, string value)
        {
            Field = mdxField; Operation = FilterOperation.Equal; Value = value;
            if (mdxField.IsMeasure)
                queryToken = string.Format(" {0} != {1} ", mdxField.MDX, value);
            else
                queryToken = "-{ " + string.Format(" {0}.&[{1}] ", mdxField.MDX, value) + " }";
            return this;
        }
        #endregion
        #region Eq
        public MDXQueryFilterItem Eq(MDXField mdxField, string value)
        {
            Field = mdxField; Operation = FilterOperation.Equal; Value = value;
            if (mdxField.IsMeasure)
                queryToken = string.Format(" {0} = {1} ", mdxField.MDX, value);
            else
                queryToken = string.Format(" {0}.&[{1}] ", mdxField.MDX, value);
            return this;
        }

        public MDXQueryFilterItem EqListInt(MDXField mdxField, int[] values)
        {
            Field = mdxField; Operation = FilterOperation.Equal; 

            string innerToken = string.Empty;
            foreach (int value in values)
            {
                innerToken =  innerToken + (innerToken.IsNullOrEmpty() ? "" : ",") + string.Format(" {0}.&[{1}] ", mdxField.MDX, value);
            }
            queryToken = innerToken;
            return this;
        }



        #endregion
        #region In
        public MDXQueryFilterItem In(MDXField mdxField, string value)
        {
            Field = mdxField; Operation = FilterOperation.In; Value = value;
            if (mdxField.IsMeasure)
            {
                throw new NotImplementedException("Ainda não é possível fazer o filtro IN com measures!");
                //queryToken = string.Format(" {0} = {1} ", mdxField.MDX, value);
            }
            else
            {
                var inValue = value.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries)
                    .Select(v => string.Format(" {0}.&[{1}] ", mdxField.MDX, v.Replace("'", "")));

                queryToken = "Exists(" + mdxField.MDX + ".MEMBERS, {" + string.Join(",", inValue) + "})";
            }

            return this;
        }
        #endregion
        #region In
        public MDXQueryFilterItem NotIn(MDXField mdxField, string value)
        {
            Field = mdxField; Operation = FilterOperation.In; Value = value;
            if (mdxField.IsMeasure)
            {
                throw new NotImplementedException("Ainda não é possível fazer o filtro IN com measures!");
                //queryToken = string.Format(" {0} = {1} ", mdxField.MDX, value);
            }
            else
            {
                var inValue = value.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries)
                    .Select(v => string.Format(" {0}.&[{1}] ", mdxField.MDX, v.Replace("'", "")));

                queryToken = "Except(" + mdxField.MDX + ".MEMBERS, {" + string.Join(",", inValue) + "})";
            }

            return this;
        }
        #endregion
        #region GreaterThan
        public MDXQueryFilterItem GreaterThan(MDXField mdxField, string value)
        {
            Field = mdxField; Operation = FilterOperation.GreaterThan; Value = value;
            if (mdxField.IsMeasure)
                queryToken = string.Format(" {0} > {1} ", mdxField.MDX, value);
            else
                queryToken = string.Format(" {0}.&[{1}]:null ", mdxField.MDX, value);
            return this;
        }
        public MDXQueryFilterItem GreaterThanEq(MDXField mdxField, string value)
        {
            Field = mdxField; Operation = FilterOperation.GreaterThanEq; Value = value;
            if (mdxField.IsMeasure)
                queryToken = string.Format(" {0} >= {1} ", mdxField.MDX, value);
            else
                queryToken = string.Format(" {0}.&[{1}]:null ", mdxField.MDX, value);
            return this;
        }
        #endregion
        #region LessThan
        public MDXQueryFilterItem LessThan(MDXField mdxField, string value)
        {
            Field = mdxField; Operation = FilterOperation.LessThan; Value = value;
            if (mdxField.IsMeasure)
                queryToken = string.Format(" {0} < {1} ", mdxField.MDX, value);
            else
                queryToken = string.Format(" null:{0}.&[{1}] ", mdxField.MDX, value);
            return this;
        }
        public MDXQueryFilterItem LessThanEq(MDXField mdxField, string value)
        {
            Field = mdxField; Operation = FilterOperation.LessThanEq; Value = value;
            if (mdxField.IsMeasure)
                queryToken = string.Format(" {0} <= {1} ", mdxField.MDX, value);
            else
                queryToken = string.Format(" null:{0}.&[{1}] ", mdxField.MDX, value);
            return this;
        }
        #endregion
        #region Between
        public MDXQueryFilterItem Between(MDXField mdxField, string minorValue, string majorValue)
        {
            Field = mdxField; Operation = FilterOperation.Between; Value = minorValue; SecondaryValue = majorValue;
            queryToken = string.Format(" {0}.&[{1}]:{0}.&[{2}] ", mdxField.MDX, minorValue, majorValue);
            return this;
        }
        #endregion
        #region Like
        public MDXQueryFilterItem Like(MDXField mdxField, string stringSearch, bool isNot)
        {
            Field = mdxField; Operation = FilterOperation.Like; Value = stringSearch;
            if (!mdxField.IsMeasure)
            {
                //this method considering only the first condition of the stringSearch
                string[] splitValues = stringSearch.Split(new char[] { '%' }, StringSplitOptions.RemoveEmptyEntries);
                List<string> conditions = new List<string>();
                if (splitValues.Length > 0)
                {
                    string mdx = mdxField.MDX.Left(mdxField.MDX.LastIndexOf('.'));

                    foreach (string search in splitValues)
                    {
                        if (search == splitValues.First() && !stringSearch.StartsWith("%")) //Left
                        {
                            conditions.Add(string.Format("(Left({0}.currentmember.name, {1}) {3} '{2}')", mdx, search.Length, search, (isNot ? "<>" : "=")));
                        }
                        else if (search == splitValues.Last() && !stringSearch.EndsWith("%"))//Right
                        {
                            conditions.Add(string.Format("(Right({0}.currentmember.name, {1}) {3} '{2}')", mdx, search.Length, search, (isNot ? "<>" : "=")));
                        }
                        else //InStr
                        {
                            conditions.Add(string.Format("(InStr({0}.currentmember.name, '{1}') {2} 0)", mdx, search, (isNot ? "=" : ">")));
                        }
                    }
                    queryToken = string.Format(" FILTER({0}, {1}) ", mdxField.MDX, string.Join(" AND ", conditions));
                }
            }
            return this;
        }
        #endregion

        #region GenericOperation
        public MDXQueryFilterItem GenericOperation(MDXField mdxField, FilterOperation op, string value1 = null, string value2 = null)
        {
            //Field = mdxField; Operation = FilterOperation.op; Value = value1; SecondaryValue = value2;
            switch (op)
            {
                case FilterOperation.Equal:
                    return this.Eq(mdxField, value1);
                case FilterOperation.NotEqual:
                    return this.NotEq(mdxField, value1);
                case FilterOperation.GreaterThanEq:
                    return this.GreaterThanEq(mdxField, value1);
                case FilterOperation.GreaterThan:
                    return this.GreaterThan(mdxField, value1);
                case FilterOperation.LessThan:
                    return this.LessThan(mdxField, value1);
                case FilterOperation.LessThanEq:
                    return this.LessThan(mdxField, value1);
                case FilterOperation.Between:
                    return this.Between(mdxField, value1, value2);
                case FilterOperation.IsNotEmpty:
                    return this.IsNotEmpty(mdxField);
                case FilterOperation.Like:
                    return this.Like(mdxField, value1, false);
                case FilterOperation.NotLike:
                    return this.Like(mdxField, value1, true);
                case FilterOperation.In:
                    return this.In(mdxField, value1);
                case FilterOperation.NotIn:
                    return this.NotIn(mdxField, value1);
                default:
                    return this;
            }
        }
        #endregion

        public override string ToString()
        {
            return queryToken;
        }

        public bool Exists(MDXField mdxField)
        {
            return (queryToken ?? "").Contains(mdxField.MDX);
        }

        public bool Exists(string mdx)
        {
            return (queryToken ?? "").Contains(mdx);
        }
        #endregion

        #region enum FilterOperation

        public enum FilterOperation { Equal, GreaterThan, GreaterThanEq, LessThan, LessThanEq, Between, IsNotEmpty, Like, NotLike, In, NotIn, NotEqual }

        #endregion
    }

    public class MDXQuerySettings
    {
        #region Properties

        public bool NonEmptyColumns { get; set; }
        public bool NonEmptyRows { get; set; }

        #endregion

        #region Public methods

        #endregion
    }
}

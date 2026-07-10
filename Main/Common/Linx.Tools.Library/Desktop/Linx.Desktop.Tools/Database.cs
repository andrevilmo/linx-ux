using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Linx.Tools.Migration
{
    public abstract class DbInfo
    {
        public object Id { get; set; }
        public string Name { get; set; }
    }

    public class Database : DbInfo
    {
        private ObservableCollection<Schema> _Schemas;
        public ObservableCollection<Schema> Schemas
        {
            get
            {
                if (_Schemas == null)
                {
                    _Schemas = new ObservableCollection<Schema>();
                    _Schemas.CollectionChanged += (s, e) =>
                        {
                            if (e.NewItems != null && e.NewItems.Count > 0)
                                foreach (var item in e.NewItems)
                                    ((Schema)item).Database = this;
                        };
                }
                return _Schemas;
            }
        }


        public TableBase FindTable(string tableName, string schemaName)
        {
            if (tableName.IsNullOrEmpty()) throw new ArgumentNullException("tableName");
            TableBase found = null;
            foreach (var schema in _Schemas)
            {
                if (schemaName.IsNullOrEmpty() || string.Equals(schemaName, schema.Name, StringComparison.InvariantCultureIgnoreCase))
                {
                    found = schema.TablesBase.FirstOrDefault(t => string.Equals(t.Name, tableName, StringComparison.InvariantCultureIgnoreCase));
                    if(found != null)
                        break;
                }
            }
            return found;
        }

        public TableBase FindTable(string tableName)
        {
            string schema = null;
            if (tableName.Occurs(".") == 1)
            {
                schema = tableName.Left(".");
                tableName = tableName.Right(".");
            }
            return FindTable(tableName, schema);
        }
    }

    public class Schema : DbInfo
    {
        public Database Database { get; set; }

        private ObservableCollection<TableBase> _TablesBase;
        public ObservableCollection<TableBase> TablesBase
        {
            get
            {
                if (_TablesBase == null)
                {
                    _TablesBase = new ObservableCollection<TableBase>();
                    _TablesBase.CollectionChanged += (s, e) =>
                    {
                        if (e.NewItems != null && e.NewItems.Count > 0)
                            foreach (var item in e.NewItems)
                                ((TableBase)item).Schema = this;
                    };
                }
                return _TablesBase;
            }
        }

        private ObservableCollection<FunctionBase> _FunctionsBase;
        public ObservableCollection<FunctionBase> FunctionsBase
        {
            get
            {
                if (_FunctionsBase == null)
                {
                    _FunctionsBase = new ObservableCollection<FunctionBase>();
                    _FunctionsBase.CollectionChanged += (s, e) =>
                    {
                        if (e.NewItems != null && e.NewItems.Count > 0)
                            foreach (var item in e.NewItems)
                                ((FunctionBase)item).Schema = this;
                    };
                }
                return _FunctionsBase;
            }
        }

        public bool HasTableBase { get { return _TablesBase.Count > 0; } }
        public bool HasFunctionBase { get { return FunctionsBase.Count > 0; } }
        public bool HasTables { get { return GetTables().Count() > 0; } }
        public bool HasViews { get { return GetViews().Count() > 0; } }
        public bool HasFunctions { get { return GetFunctions().Count() > 0; } }
        public bool HasProcedures { get { return GetProcedures().Count() > 0; } }


        public IEnumerable<Table> GetTables()
        {
            return TablesBase.OfType<Table>();
        }

        public IEnumerable<View> GetViews()
        {
            return TablesBase.OfType<View>();
        }
        public IEnumerable<Function> GetFunctions()
        {
            return FunctionsBase.OfType<Function>();
        }
        public IEnumerable<Procedure> GetProcedures()
        {
            return FunctionsBase.OfType<Procedure>();
        }
    }

    public abstract class StructBase : DbInfo
    {
        public Schema Schema { get; set; }

        private ObservableCollection<Column> _Columns;
        public ObservableCollection<Column> Columns
        {
            get
            {
                if (_Columns == null)
                {
                    _Columns = new ObservableCollection<Column>();
                    _Columns.CollectionChanged += (s, e) =>
                    {
                        if (e.NewItems != null && e.NewItems.Count > 0)
                            foreach (var item in e.NewItems)
                                ((Column)item).StructBase = this;
                    };
                }
                return _Columns;
            }
        }

        public bool HasColumns { get { return Columns.Count > 0; } }
    }

    public abstract class TableBase : StructBase
    {
        public bool IsTable { get { return this is Table; } }

        public virtual string GetPrimaryKeyInfo()
        {
            return String.Empty;
        }

        public virtual string[] GetForeignKeysList()
        {
            return new string[] { };
        }

        public virtual string[] GetIndexList()
        {
            return new string[] { };
        }

        public string[] GetColumnsList()
        {
            return Columns.Select(c => c.ToString()).ToArray();
        }

        public override string ToString()
        {
            return string.Format("{0}{1}", Name, IsTable ? "" : " (V)");
        }
    }

    public class Table : TableBase
    {
        public PrimaryKey PrimaryKey { get; set; }

        private ObservableCollection<ForeignKey> _ForeignKey;
        public ObservableCollection<ForeignKey> ForeignKey
        {
            get
            {
                if (_ForeignKey == null)
                    _ForeignKey = new ObservableCollection<ForeignKey>();

                return _ForeignKey;
            }
        }

        private ObservableCollection<Index> _Indexes;
        public ObservableCollection<Index> Indexes
        {
            get
            {
                if (_Indexes == null)
                {
                    _Indexes = new ObservableCollection<Index>();
                    _Indexes.CollectionChanged += (s, e) =>
                    {
                        if (e.NewItems != null && e.NewItems.Count > 0)
                            foreach (var item in e.NewItems)
                                ((Index)item).Table = this;
                    };
                }
                return _Indexes;
            }
        }

        public override string GetPrimaryKeyInfo()
        {
            return PrimaryKey == null ? "-" : PrimaryKey.ToString();
        }

        public override string[] GetForeignKeysList()
        {
            return this.ForeignKey.Select(f => f.ToString()).ToArray();
        }

        public override string[] GetIndexList()
        {
            return this.Indexes.Select(f => f.ToString()).ToArray();
        }
    }

    public class View : TableBase
    {
    }

    public class PrimaryKey : DbInfo
    {
        public Table Table { get; set; }
        public bool IsClustered { get; set; }

        private ObservableCollection<Column> _Columns;
        public ObservableCollection<Column> Columns
        {
            get
            {
                if (_Columns == null)
                    _Columns = new ObservableCollection<Column>();

                return _Columns;
            }
        }

        public override string ToString()
        {
            return
                string.Format("{0} ({1})",
                this.Name,
                string.Join(", ", this.Columns.Select(c => c.Name)));

        }
    }

    public class ForeignKey : DbInfo
    {
        public Table Parent { get; set; }
        public Table Referenced { get; set; }

        private ObservableCollection<ForeignKeyColumns> _ForeignKeyColumns;
        public ObservableCollection<ForeignKeyColumns> ForeignKeyColumns
        {
            get
            {
                if (_ForeignKeyColumns == null)
                {
                    _ForeignKeyColumns = new ObservableCollection<ForeignKeyColumns>();
                    _ForeignKeyColumns.CollectionChanged += (s, e) =>
                    {
                        if (e.NewItems != null && e.NewItems.Count > 0)
                            foreach (var item in e.NewItems)
                                ((ForeignKeyColumns)item).ForeignKey = this;
                    };
                }
                return _ForeignKeyColumns;
            }
        }

        public ReferentialAction UpdateAction { get; set; }
        public ReferentialAction DeleteAction { get; set; }

        public bool RemoveAutomaticIndex { get; set; }

        public override string ToString()
        {
            return
                string.Format("{0} ({1})",
                    Name,
                    Referenced.Name
                );
        }

        public enum ReferentialAction
        {
            NoAction = 0,
            Cascade = 1
        }


    }

    public class ForeignKeyColumns
    {
        public ForeignKey ForeignKey { get; set; }

        public ForeignKeyColumns(Table parent, Column parentColumn, Table referenced, Column referencedColumn)
        {
            this.Parent = parent;
            this.ParentColumn = parentColumn;
            this.Referenced = referenced;
            this.ReferencedColumn = referencedColumn;
        }

        public Table Parent { get; private set; }
        public Column ParentColumn { get; private set; }
        public Table Referenced { get; private set; }
        public Column ReferencedColumn { get; private set; }
    }

    public class Column : DbInfo
    {
        public StructBase StructBase { get; set; }
        public PrimaryKey PrimaryKey { get; set; }

        public DataTypeEnum DbDataType { get; set; }
        public bool IsNullable { get; set; }
        public Type DataType { get { return GetTypeByDbDataType(DbDataType, MaxLength); } }
        public bool IsIdentity { get; set; }
        public bool IsPK { get; set; }
        public string SqlDefault { get; set; }
        public Table TableBase { get; set; }

        public static Type GetTypeByDbDataType(DataTypeEnum dbDataType, short maxLength)
        {
            switch (dbDataType)
            {
                case DataTypeEnum.BIGINT:
                    return typeof(long);
                case DataTypeEnum.BINARY:
                    return typeof(byte);
                case DataTypeEnum.BIT:
                    return typeof(bool);
                case DataTypeEnum.CHAR:
                case DataTypeEnum.NCHAR:
                    return maxLength == 1 ? typeof(char) : typeof(string);
                case DataTypeEnum.DATE:
                case DataTypeEnum.DATETIME:
                case DataTypeEnum.DATETIME2:
                case DataTypeEnum.SMALLDATETIME:
                case DataTypeEnum.TIME:
                    return typeof(DateTime);
                case DataTypeEnum.DATETIMEOFFSET:
                    return typeof(DateTimeOffset);
                case DataTypeEnum.NUMERIC:
                case DataTypeEnum.DECIMAL:
                case DataTypeEnum.REAL:
                    return typeof(Decimal);
                case DataTypeEnum.FLOAT:
                    return typeof(float);
                case DataTypeEnum.GEOGRAPHY:
                case DataTypeEnum.GEOMETRY:
                case DataTypeEnum.HIERARCHYID:
                case DataTypeEnum.TIMESTAMP:
                    throw new NotImplementedException();
                case DataTypeEnum.IMAGE:
                case DataTypeEnum.VARBINARY:
                    return typeof(byte[]);
                case DataTypeEnum.INT:
                    return typeof(int);
                case DataTypeEnum.MONEY:
                case DataTypeEnum.SMALLMONEY:
                    return typeof(decimal);
                case DataTypeEnum.NTEXT:
                case DataTypeEnum.NVARCHAR:
                case DataTypeEnum.TEXT:
                case DataTypeEnum.VARCHAR:
                case DataTypeEnum.SYSNAME:
                    return typeof(string);
                case DataTypeEnum.SMALLINT:
                    return typeof(short);
                case DataTypeEnum.SQL_VARIANT:
                    return typeof(object);
                case DataTypeEnum.TINYINT:
                    return typeof(byte);
                case DataTypeEnum.UNIQUEIDENTIFIER:
                    return typeof(Guid);
                case DataTypeEnum.XML:
                    return typeof(System.Xml.XmlDocument);
                default:
                    throw new NotImplementedException();
            }
        }

        public short MaxLength { get; set; }
        public byte Precision { get; set; }
        public byte Scale { get; set; }

        public override string ToString()
        {
            return
                string.Format("{0}\t({1})\t{2}", Name, DbDataType, IsPK ? "(PK)" : "");
        }


    }

    public class Index : DbInfo
    {
        public Table Table { get; set; }
        public string CommandColumns { get; set; }

        private ObservableCollection<Column> _Columns;
        public ObservableCollection<Column> Columns
        {
            get
            {
                if (_Columns == null)
                    _Columns = new ObservableCollection<Column>();

                return _Columns;
            }
        }

        public bool IsUnique { get; set; }

        private ObservableCollection<Column> _Include;
        public ObservableCollection<Column> Include
        {
            get
            {
                if (_Include == null)
                    _Include = new ObservableCollection<Column>();

                return _Include;
            }
        }

        public bool IsUniqueConstraint { get; set; }
        public bool IsClustered { get; set; }

        public override string ToString()
        {
            return
                string.Format("{0}{1}{2} ({3}) include({4})",
                Name,
                IsUnique ? " UNIQUE" : "",
                IsUniqueConstraint ? "(C)" : "",
                string.Join(",", this.Columns.Select(c => c.Name)),
                string.Join(",", this.Include.Select(c => c.Name))
                );
        }
    }

    public abstract class FunctionBase : StructBase
    {
        private ObservableCollection<Parameter> _Parameters;
        public ObservableCollection<Parameter> Parameters
        {
            get
            {
                if (_Parameters == null)
                {
                    _Parameters = new ObservableCollection<Parameter>();
                    _Parameters.CollectionChanged += (s, e) =>
                    {
                        if (e.NewItems != null && e.NewItems.Count > 0)
                            foreach (var item in e.NewItems)
                                ((Parameter)item).FunctionBase = this;
                    };
                }
                return _Parameters;
            }
        }


        public string[] GetParameterList()
        {
            return this.Parameters.Select(p => p.ToString()).ToArray();
        }

        public override string ToString()
        {
            if (this is Function)
                return string.Format("fn: {0}.{1}({2})", this.Schema.Name, this.Name, string.Join(",", this.Parameters.Select(p => p.Name)));
            else
                return string.Format("proc: {0}.{1} {2}", this.Schema.Name, this.Name, string.Join(",", this.Parameters.Select(p => p.Name)));
        }
    }

    public class Function : FunctionBase
    {
        public FunctionTypeEnum FunctionType { get; set; }
        public static FunctionTypeEnum getFunctionTypeEnum(string typeFn)
        {
            FunctionTypeEnum enumFn = FunctionTypeEnum.MultiValued;
            switch (typeFn)
            {
                case "IF":
                    enumFn = FunctionTypeEnum.Inline;
                    break;
                case "FN":
                    enumFn = FunctionTypeEnum.Scalar;
                    break;
                case "TF":
                    enumFn = FunctionTypeEnum.MultiValued;
                    break;

                default:
                    break;
            }
            return enumFn;
        }
    }

    public class Procedure : FunctionBase { }

    public class Parameter : DbInfo
    {
        public FunctionBase FunctionBase { get; set; }

        public DataTypeEnum DbDataType { get; set; }
        public bool IsNullable { get; set; }
        public Type DataType { get { return Column.GetTypeByDbDataType(DbDataType, MaxLength); } }
        public short MaxLength { get; set; }
        public byte Precision { get; set; }
        public byte Scale { get; set; }

        public override string ToString()
        {
            return string.Format("{0} ({1})", Name, DbDataType);
        }
    }

    public enum DataTypeEnum
    {
        BIGINT = 127,
        BINARY = 173,
        BIT = 104,
        CHAR = 175,
        DATE = 40,
        DATETIME = 61,
        DATETIME2 = 42,
        DATETIMEOFFSET = 43,
        DECIMAL = 106,
        FLOAT = 62,
        GEOGRAPHY = 130,
        GEOMETRY = 129,
        HIERARCHYID = 128,
        IMAGE = 34,
        INT = 56,
        MONEY = 60,
        NCHAR = 239,
        NTEXT = 99,
        NUMERIC = 108,
        NVARCHAR = 231,
        REAL = 59,
        SMALLDATETIME = 58,
        SMALLINT = 52,
        SMALLMONEY = 122,
        SQL_VARIANT = 98,
        SYSNAME = 256,
        TEXT = 35,
        TIME = 41,
        TIMESTAMP = 189,
        TINYINT = 48,
        UNIQUEIDENTIFIER = 36,
        VARBINARY = 165,
        VARCHAR = 167,
        XML = 241
    }
    public enum FunctionTypeEnum { Scalar, Inline, MultiValued }
}

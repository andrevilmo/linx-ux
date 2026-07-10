using Linx.Tools;
using Linx.Tools.Migration;
using System;
using System.Linq;

namespace Linx.BusinessDataModelDesigner.CustomizedCode.DatabaseScriptGenerator
{
    public class ScriptGeneratorPostgreSQL : ScriptGeneratorBase
    {
        const string ifNotExists = " IF NOT EXISTS";
        const string defaultSchema = "public";
        #region Constructors
        public ScriptGeneratorPostgreSQL() : this(null) { }
        public ScriptGeneratorPostgreSQL(ScriptGeneratorOptions options) : base(options, ScriptGeneratorType.PostgreSql) { }
        #endregion

        protected override bool HasGenerateDatabase()
        {
            return false;
        }
        protected override void GenerateScriptDatabase()
        {
            if (_options.CreateDatabase)
            {
                AddLine("CREATE DATABASE \"{0}\";", _database.Name);
                AddLine();
            }

            AddComment("Desconectar e conectar na base criada.");
        }

        protected override void GenerateScriptSchemas()
        {
            foreach (var schema in _database.Schemas)
            {
                if (schema.Name != defaultSchema)
                    AddLine("CREATE SCHEMA{0} {1};", (_options.CheckSchemaExists ? ifNotExists : ""), "\"" + schema.Name + "\"");
            }
        }

        protected override void GenerateScriptTables()
        {
            foreach (var schema in _database.Schemas)
            {
                foreach (var table in schema.TablesBase.OfType<Table>())
                {
                    AddComment("create table {0}", getFullTableName(table));
                    if (table.Columns.Any(c => c.IsIdentity && !c.DbDataType.In(DataTypeEnum.BIGINT, DataTypeEnum.INT)))
                    {
                        AddLine("CREATE SEQUENCE{0} {1}_SEQ;", ifNotExists, getFullTableName(table));
                        AddLine();
                    }

                    AddLine("CREATE TABLE{0} {1} (", (_options.CheckTableExists ? ifNotExists : ""), getFullTableName(table));
                    GenerateScriptDetails(table, "");
                    AddLine(");");
                }
            }
            AddLine();
        }

        protected override void GenerateScriptDetails(Table table, string initialIdent)
        {
            for (var i = 0; i < table.Columns.Count; i++)
            {
                var column = table.Columns[i];
                string createCol = string.Format("{0} {1}", "\"" + column.Name + "\"", convertDataTypeToString(column));
                //identity
                if (column.IsIdentity && !column.DbDataType.In(DataTypeEnum.BIGINT, DataTypeEnum.INT))
                {
                    createCol += string.Format(" default nextval ('{0}_SEQ')", getFullTableName(table));
                }

                //precision e Max Value
                if (!column.DbDataType.In(DataTypeEnum.SMALLINT, DataTypeEnum.INT, DataTypeEnum.BIGINT, DataTypeEnum.BIT, DataTypeEnum.TINYINT, DataTypeEnum.DATE, DataTypeEnum.DATETIME, DataTypeEnum.DATETIME2, DataTypeEnum.DATETIMEOFFSET, DataTypeEnum.SMALLDATETIME, DataTypeEnum.TIMESTAMP, DataTypeEnum.TIME))
                {
                    if (column.Precision > 0 && column.Scale > 0)
                    {
                        createCol += String.Format("({0}, {1})", column.Precision, column.Scale);
                    }
                    else
                    {
                        if (column.MaxLength > 0)
                            createCol += String.Format("({0})", column.MaxLength);
                    }
                }
                //null
                createCol += column.IsNullable ? " NULL" : " NOT NULL";
                //default 
                if (!column.IsIdentity && !column.SqlDefault.IsNullOrEmpty()) createCol += string.Format(" default {0}", column.SqlDefault);

                AddLine(initialIdent + _indent + "{0}{1}", createCol, ((i + 1) < table.Columns.Count || (table.PrimaryKey != null)) ? "," : "");
            }
            if (!table.PrimaryKey.IsNull())
            {
                AddLine(initialIdent + _indent + "CONSTRAINT {0} PRIMARY KEY ({1})", "\"" + table.PrimaryKey.Name + "\"", string.Join(",", table.PrimaryKey.Columns.Select(c => "\"" + c.Name + "\"")));
            }

        }

        protected override void GenerateScriptForeignKeys()
        {
            var foreignKeys = _database.Schemas.SelectMany(s => s.TablesBase.Where(t => t.IsTable).OfType<Table>().SelectMany(t => t.ForeignKey.ToArray()));
            foreach (var fk in foreignKeys)
            {
                if (_options.DropForeignKeys)
                {
                    AddLine("ALTER TABLE {0} DROP CONSTRAINT IF EXISTS {1};", getFullTableName(fk.Parent), "\"" + fk.Name + "\"");
                }

                AddLine("ALTER TABLE {0} ADD CONSTRAINT {1} FOREIGN KEY ({2}) REFERENCES {3} ({4}) ON DELETE {5} ON UPDATE {6};",
                    getFullTableName(fk.Parent), //table
                    "\"" + fk.Name + "\"",
                    string.Join(",", fk.ForeignKeyColumns.Select(c => "\"" + c.ParentColumn.Name + "\"")), //cols
                    getFullTableName(fk.Referenced), //ref table
                    string.Join(",", fk.ForeignKeyColumns.Select(c => "\"" + c.ReferencedColumn.Name + "\"")), //ref cols
                    fkActionName(fk.DeleteAction),
                    fkActionName(fk.UpdateAction)
                );
            }
        }

        protected override void GenerateScriptIndexes()
        {
            var indexes = _database.Schemas.SelectMany(s => s.TablesBase.Where(t => t.IsTable).OfType<Table>().SelectMany(t => t.Indexes.ToArray()));
            foreach (var index in indexes)
            {
               
                AddLine("CREATE{0} INDEX{1} {2} ON {3}({4});",
                    (index.IsUnique || index.IsUniqueConstraint ? " UNIQUE" : ""), //unique
                    ifNotExists,
                    "\"" + index.Name + "\"", //index Name
                    getFullTableName(index.Table), //schema
                    String.Join(",", index.Columns.Select(c => "\"" + c.Name + "\""))
                );
            }
        }



        private string getFullTableName(Table table)
        {
            return (table.Schema.Name == defaultSchema ? string.Empty : "\"" + table.Schema.Name + "\".") + "\"" + table.Name + "\"";
        }

        private string convertDataTypeToString(Column col)
        {
            var type = "";
            if (col.IsIdentity)
            {
                if (col.DbDataType == DataTypeEnum.INT)
                    return "SERIAL";
                if (col.DbDataType == DataTypeEnum.BIGINT)
                    return "BIGSERIAL";
            }

            switch (col.DbDataType)
            {
                case DataTypeEnum.BIT:
                    type = "boolean";
                    break;
                case DataTypeEnum.CHAR:
                    type = col.MaxLength == -1 ? "text" : "character";
                    break;
                case DataTypeEnum.DATE:
                    type = "date";
                    break;
                case DataTypeEnum.DATETIME:
                case DataTypeEnum.DATETIME2:
                case DataTypeEnum.SMALLDATETIME:
                    type = "timestamp without time zone";
                    break;
                case DataTypeEnum.DATETIMEOFFSET:
                    type = "timestamp with time zone";
                    break;
                case DataTypeEnum.TIME:
                    type = "time";
                    break;
                case DataTypeEnum.UNIQUEIDENTIFIER:
                    type = "uuid";
                    break;
                case DataTypeEnum.IMAGE:
                case DataTypeEnum.VARBINARY:
                    type = "bytea";
                    break;
                case DataTypeEnum.VARCHAR:
                    type = col.MaxLength == -1 ? "text" : "character varying";
                    break;
                case DataTypeEnum.BIGINT:
                case DataTypeEnum.BINARY:                         
                case DataTypeEnum.DECIMAL:
                case DataTypeEnum.FLOAT:
                case DataTypeEnum.GEOGRAPHY:
                case DataTypeEnum.GEOMETRY:
                case DataTypeEnum.HIERARCHYID:                
                case DataTypeEnum.INT:
                case DataTypeEnum.MONEY:
                case DataTypeEnum.NCHAR:
                case DataTypeEnum.NTEXT:
                case DataTypeEnum.NUMERIC:
                case DataTypeEnum.NVARCHAR:
                case DataTypeEnum.REAL:
                case DataTypeEnum.SMALLINT:
                case DataTypeEnum.SMALLMONEY:
                case DataTypeEnum.SQL_VARIANT:
                case DataTypeEnum.SYSNAME:
                case DataTypeEnum.TEXT:               
                case DataTypeEnum.TIMESTAMP:
                case DataTypeEnum.TINYINT:              
                case DataTypeEnum.XML:
                    type = col.DbDataType.ToString();
                    break;
               
                default:
                    type = col.DbDataType.ToString();
                    break;
            }
            return type.ToUpper();
        }
    }
}

using Linx.Tools;
using Linx.Tools.Migration;
using System;
using System.Linq;

namespace Linx.BusinessDataModelDesigner.CustomizedCode.DatabaseScriptGenerator
{
    public class ScriptGeneratorMySql : ScriptGeneratorBase
    {
        const string ifNotExists = " IF NOT EXISTS";
        #region Constructors
        public ScriptGeneratorMySql() : this(null) { }
        public ScriptGeneratorMySql(ScriptGeneratorOptions options) : base(options, ScriptGeneratorType.MySql) { }
        #endregion

        protected override void GenerateScriptDatabase()
        {
            if (_options.CreateDatabase)
            {
                AddLine("CREATE DATABASE{1} `{0}`;", _database.Name, (_options.CheckDatabaseExists ? ifNotExists : ""));
                AddLine();
            }

            AddLine("USE `{0}`;", _database.Name);
        }

        protected override bool HasGenerateSchemas() { return false; }
        
        protected override void GenerateScriptSchemas()
        {
            AddLine("NOT SUPPORTED");
        }

        protected override void GenerateScriptTables()
        {
            foreach (var schema in _database.Schemas)
            {
                foreach (var table in schema.TablesBase.OfType<Table>())
                {
                    AddComment("create table [{0}]", table.Name);
                    AddLine("CREATE TABLE{0} `{1}` (", (_options.CheckTableExists ? ifNotExists : ""), table.Name);
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
                string createCol = string.Format("`{0}` {1}", column.Name, convertDataTypeToString(column));
                //identity
                if (column.IsIdentity) createCol += " AUTO_INCREMENT";

                //precision e Max Value
                if (!column.DbDataType.In(DataTypeEnum.SMALLINT, DataTypeEnum.INT, DataTypeEnum.BIGINT, DataTypeEnum.BIT, DataTypeEnum.TINYINT, DataTypeEnum.DATE, DataTypeEnum.DATETIME, DataTypeEnum.DATETIME2, DataTypeEnum.DATETIMEOFFSET, DataTypeEnum.SMALLDATETIME, DataTypeEnum.TIMESTAMP, DataTypeEnum.TIME))
                {
                    if (column.Precision > 0 && column.Scale > 0)
                    {
                        createCol += String.Format("({0}, {1})", column.Precision, column.Scale);
                    }
                    else
                    {
                        if (column.DbDataType != DataTypeEnum.VARCHAR && column.MaxLength == -1)
                            createCol += String.Format("(65535)", column.MaxLength);
                        else if (column.MaxLength > 0)
                            createCol += String.Format("({0})", column.MaxLength);
                    }
                }
                //null
                createCol += column.IsNullable ? " NULL" : " NOT NULL";
                //default 
                if (!column.SqlDefault.IsNullOrEmpty()) createCol += string.Format(" default {0}", column.SqlDefault);

                AddLine(initialIdent + _indent + "{0}{1}", createCol, ((i + 1) < table.Columns.Count || (table.PrimaryKey != null || table.ForeignKey.Count > 0)) ? "," : "");
            }
            if (!table.PrimaryKey.IsNull())
            {
                AddLine(initialIdent + _indent + "CONSTRAINT `{0}` PRIMARY KEY (`{1}`) {2}", table.PrimaryKey.Name, string.Join("`,`", table.PrimaryKey.Columns.Select(c => c.Name)), (table.ForeignKey.Count > 0 ? "," : ""));
            }
            if (table.ForeignKey.Count > 0)
            {
                var fks = table.ForeignKey.Select(f =>
                string.Format(initialIdent + _indent + "CONSTRAINT `{0}` FOREIGN KEY (`{1}`) REFERENCES `{2}`(`{3}`) ON DELETE {4} ON UPDATE {5}",
                    f.Name,
                    string.Join("`,`", f.ForeignKeyColumns.Select(c => c.ParentColumn.Name)),
                    f.Referenced.Name,
                    string.Join("`,`", f.ForeignKeyColumns.Select(c => c.ReferencedColumn.Name)),
                    base.fkActionName(f.DeleteAction),
                    base.fkActionName(f.UpdateAction)
                    )
                );
                AddLine(string.Join(",\r\n", fks));
            }

        }

        protected override bool HasGenerateForeignKeys() { return false; }
        protected override void GenerateScriptForeignKeys()
        {
            AddLine("NOT SUPPORTED");
        }
        protected override void GenerateScriptIndexes()
        {
            var indexes = _database.Schemas.SelectMany(s => s.TablesBase.Where(t => t.IsTable).OfType<Table>().SelectMany(t => t.Indexes.ToArray()));
            foreach (var index in indexes)
            {
                if (_options.DropIndexes)
                {
                    AddLine("set @var=IF(EXISTS(SELECT * FROM INFORMATION_SCHEMA.STATISTICS WHERE TABLE_NAME='{0}' AND INDEX_NAME='{1}'), 'DROP INDEX `{1}` ON `{0}`', 'SELECT 1');", index.Table.Name, index.Name);
                    AddLine("prepare stmt from @var; execute stmt; deallocate prepare stmt;");
                }
                AddLine("CREATE{0} INDEX `{1}` ON `{2}`(`{3}`);",
                    (index.IsUnique || index.IsUniqueConstraint ? " UNIQUE" : ""), //unique
                    index.Name, //index Name
                    index.Table.Name, //table
                    String.Join("`,`", index.Columns.Select(c => c.Name))
                    );
            }
        }


        protected override void GenerateScriptStarting()
        {
            AddLine("SET FOREIGN_KEY_CHECKS = 0;");
        }
        protected override void GenerateScriptFinalizing()
        {
            AddLine("SET FOREIGN_KEY_CHECKS = 1;");
        }

        private string convertDataTypeToString(Column col)
        {
            var type = "";
            switch (col.DbDataType)
            {
                case DataTypeEnum.DATETIME:
                case DataTypeEnum.DATETIME2:
                case DataTypeEnum.DATETIMEOFFSET:
                    type = "datetime";
                    break;
                case DataTypeEnum.UNIQUEIDENTIFIER:
                    type = "char(36)";
                    break;

                case DataTypeEnum.BIGINT:
                case DataTypeEnum.BINARY:
                case DataTypeEnum.BIT:
                case DataTypeEnum.DATE:
                case DataTypeEnum.DECIMAL:
                case DataTypeEnum.FLOAT:
                case DataTypeEnum.GEOGRAPHY:
                case DataTypeEnum.GEOMETRY:
                case DataTypeEnum.HIERARCHYID:
                case DataTypeEnum.IMAGE:
                case DataTypeEnum.INT:
                case DataTypeEnum.MONEY:
                case DataTypeEnum.NCHAR:
                case DataTypeEnum.NTEXT:
                case DataTypeEnum.NUMERIC:
                case DataTypeEnum.NVARCHAR:
                case DataTypeEnum.REAL:
                case DataTypeEnum.SMALLDATETIME:
                case DataTypeEnum.SMALLINT:
                case DataTypeEnum.SMALLMONEY:
                case DataTypeEnum.SQL_VARIANT:
                case DataTypeEnum.SYSNAME:
                case DataTypeEnum.TEXT:
                case DataTypeEnum.TIME:
                case DataTypeEnum.TIMESTAMP:
                case DataTypeEnum.TINYINT:
                case DataTypeEnum.VARBINARY:
                case DataTypeEnum.XML:
                    type = col.DbDataType.ToString();
                    break;
                case DataTypeEnum.CHAR:
                case DataTypeEnum.VARCHAR:
                    type = col.MaxLength == -1 ? "text" : col.DbDataType.ToString();
                    break;
                default:
                    type = col.DbDataType.ToString();
                    break;
            }
            return type.ToUpper();
        }
    }
}

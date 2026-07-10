using Linx.Tools.Migration;
using System;
using System.Linq;
using Linx.Tools;

namespace Linx.BusinessModelDesigner.CustomizedCode.DatabaseScriptGenerator
{
    public class ScriptGeneratorSqlite : ScriptGeneratorBase
    {
        #region Constructors
        public ScriptGeneratorSqlite() : this(null) { }
        public ScriptGeneratorSqlite(ScriptGeneratorOptions options) : base(options, ScriptGeneratorType.Sqlite) { }
        #endregion

        protected override void GenerateScriptDatabase()
        {
            AddComment("Database not suportted");
        }

        protected override void GenerateScriptSchemas()
        {
            AddComment("Schema not suportted");
        }

        protected override void GenerateScriptTables()
        {
            foreach (var schema in _database.Schemas)
            {
                foreach (var table in schema.TablesBase.OfType<Table>())
                {
                    AddComment("create table [{0}]", table.Name);

                    var initialIdent = "";
                    AddLine(initialIdent + "CREATE TABLE [{0}] (", table.Name);
                    GenerateScriptDetails(table, initialIdent);
                    AddLine(initialIdent + ");");

                }
            }
            AddLine();
        }
        
        protected override void GenerateScriptDetails(Table table, string initialIdent)
        {
            for (var i = 0; i < table.Columns.Count; i++)
            {
                var column = table.Columns[i];
                string createCol = string.Format("[{0}]", column.Name);
                //check for PK and a single column PK
                if (column.IsPK && table.PrimaryKey.Columns.Count == 1 && column.IsIdentity)
                {
                    createCol += " INTEGER PRIMARY KEY AUTOINCREMENT";
                }
                else {

                    createCol += string.Format(" [{0}]", column.DbDataType.ToString().ToLower());

                    if (!column.DbDataType.In(DataTypeEnum.SMALLINT, DataTypeEnum.INT, DataTypeEnum.BIGINT, DataTypeEnum.BIT, DataTypeEnum.TINYINT, DataTypeEnum.DATE, DataTypeEnum.DATETIME, DataTypeEnum.DATETIME2, DataTypeEnum.DATETIMEOFFSET, DataTypeEnum.SMALLDATETIME, DataTypeEnum.TIMESTAMP, DataTypeEnum.TIME))
                    {
                        if (column.Precision > 0 && column.Scale > 0)
                        {
                            createCol += String.Format("({0}, {1})", column.Precision, column.Scale);
                        }
                        else
                        {
                            if (column.MaxLength == -1)
                                createCol += String.Format("(MAX)", column.MaxLength);
                            if (column.MaxLength > 0)
                            {
                                createCol += String.Format("({0})", column.MaxLength);
                            }
                        }
                    }

                    if (column.IsPK && table.PrimaryKey.Columns.Count == 1)
                    {
                        createCol += " PRIMARY KEY";
                    }

                    //null
                    createCol += column.IsNullable ? " NULL" : " NOT NULL";
                }


                AddLine(initialIdent + _indent + "{0}{1}", createCol, ((i + 1) < table.Columns.Count || (table.PrimaryKey.Columns.Count > 1 || table.ForeignKey.Count > 0)) ? "," : "");
            }
            if (table.PrimaryKey.Columns.Count > 1)
            {
                AddLine(initialIdent + _indent + "PRIMARY KEY ({0}) {1}", string.Join(",", table.PrimaryKey.Columns.Select(c => c.Name)), (table.ForeignKey.Count > 0 ? "," : ""));
            }
            if (table.ForeignKey.Count > 0)
            {
                var fks = table.ForeignKey.Select(f =>
                string.Format(initialIdent + _indent + "FOREIGN KEY ({0}) REFERENCES {1}({2})",
                    string.Join(",", f.ForeignKeyColumns.Select(c => c.ParentColumn.Name)),
                    f.Referenced.Name,
                    string.Join(",", f.ForeignKeyColumns.Select(c => c.ReferencedColumn.Name))
                    )
                );
                AddLine(string.Join(",\r\n", fks));
            }
        }
        
        protected override void GenerateScriptForeignKeys()
        {
            AddComment("not suportted");
        }

        protected override void GenerateScriptIndexes()
        {
            var indexes = _database.Schemas.SelectMany(s => s.TablesBase.Where(t => t.IsTable).OfType<Table>().SelectMany(t => t.Indexes.ToArray()));
            foreach (var index in indexes)
            {
                AddLine("CREATE{0} INDEX [{1}] ON [{2}]({3});",
                    (index.IsUnique || index.IsUniqueConstraint ? " UNIQUE" : ""), //unique
                    index.Name, //index Name
                    index.Table.Name, //table
                    String.Join(",", index.Columns.Select(c => c.Name)) //columns
                    );
            }

        }

        protected override void GenerateScriptFinalizing()
        {
            AddComment("Turned ON FKs");
            AddLine("PRAGMA foreign_keys = ON");
        }

        protected override bool HasGenerateForeignKeys()
        {
            return false;
        }
        protected override bool HasGenerateSchemas()
        {
            return false;
        }        
    }
}

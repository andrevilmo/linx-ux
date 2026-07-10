using Linx.Tools.Migration;
using System;
using System.IO;
using System.Linq;
using System.Text;
using Linx.Tools;
using System.Collections.ObjectModel;

namespace Linx.BusinessModelDesigner.CustomizedCode.DatabaseScriptGenerator
{
    public abstract class ScriptGeneratorBase
    {
        #region Enum ScriptGeneratorType
        public enum ScriptGeneratorType
        {
            MySql,
            Sqlite,
            SqlServer,
            PostgreSql
        }
        #endregion

        #region Fields
        protected ScriptGeneratorOptions _options;
        protected readonly string _indent;
        protected StringBuilder _sb;
        protected Database _database;
        protected readonly ScriptGeneratorType _dbType;
        #endregion

        #region Constructors
        public ScriptGeneratorBase(ScriptGeneratorOptions options, ScriptGeneratorType dbType)
        {
            if (options == null)
                options = new ScriptGeneratorOptions();

            this._options = options;

            ///set defaults
            if (options.InsertTabs)
                _indent = "\t";
            else
                _indent = new string(' ', options.TabSpace);

            this._dbType = dbType;
        }
        #endregion

        #region Public Methods
        public string GenerateScript(Database database)
        {
            _database = database;
            _sb = new StringBuilder();

            GenerateStartScript();

            GenerateScriptStarting();

            if (HasGenerateDatabase())
            {
                GenerateSubSession("Database");
                GenerateScriptDatabase();
            }

            if (HasGenerateSchemas())
            {
                GenerateSubSession("Schemas");
                GenerateScriptSchemas();
            }


            GenerateSubSession("Tables");
            GenerateScriptTables();

            GenerateSubSession("Indexes");
            GenerateScriptIndexes();
            if (HasGenerateForeignKeys())
            {
                GenerateSubSession("Foreign Keys");
                GenerateScriptForeignKeys();
            }

            GenerateScriptFinalizing();

            GenerateEndScript();

            return _sb.ToString();
        }

        public void GenerateScript(Database database, string path)
        {
            string script = GenerateScript(database);
            if (File.Exists(path))
                File.Delete(path);

            File.AppendAllText(path, script);
        }
        #endregion

        #region Virtual Methods

        protected virtual void GenerateScriptDatabase()
        {
            if (_options.CreateDatabase)
            {
                if (_options.CheckDatabaseExists)
                {
                    AddLine("USE [master]");
                    AddLine("IF db_id('{0}') IS NULL", _database.Name);
                }
                AddLine((_options.CheckDatabaseExists ? _indent : "") + "CREATE DATABASE [{0}]", _database.Name);
                AddLine("GO");
                AddLine();
            }


            AddLine("USE [{0}]", _database.Name);
        }

        protected virtual void GenerateScriptSchemas()
        {
            if (_options.CreateSchema)
            {
                foreach (var schema in _database.Schemas)
                {
                    if (schema.Name.ToLower() != "dbo")
                    {
                        if (_options.CheckSchemaExists)
                        {
                            AddLine("IF schema_id('{0}') IS NULL", schema.Name);
                        }
                        AddLine((_options.CheckDatabaseExists ? _indent : "") + "EXEC('CREATE SCHEMA [{0}]')", schema.Name);
                    }
                }
                AddLine();
            }
        }

        protected virtual void GenerateScriptTables()
        {
            foreach (var schema in _database.Schemas)
            {
                foreach (var table in schema.TablesBase.OfType<Table>())
                {
                    AddComment("create table [{0}].[{1}]", schema.Name, table.Name);
                    if (_options.CheckTableExists)
                    {
                        AddLine("IF NOT EXISTS(SELECT * FROM sys.tables where name = '{1}' AND schema_id = schema_id('{0}') )", schema.Name, table.Name);
                        AddLine("BEGIN");
                    }
                    var initialIdent = _options.CheckDatabaseExists ? _indent : "";
                    AddLine(initialIdent + "CREATE TABLE [{0}].[{1}] (", schema.Name, table.Name);
                    GenerateScriptDetails(table, initialIdent);
                    AddLine(initialIdent + ")");

                    if (_options.CheckTableExists)
                    {
                        AddLine("END");
                    }
                }
            }
            AddLine();
        }

        protected virtual void GenerateScriptDetails(Table table, string initialIdent)
        {
            for (var i = 0; i < table.Columns.Count; i++)
            {
                var column = table.Columns[i];

                string createCol = string.Format("[{0}] [{1}]", column.Name, column.DbDataType.ToString().ToLower());

                //precision e Max Value
                if (!column.DbDataType.In(DataTypeEnum.SMALLINT, DataTypeEnum.INT, DataTypeEnum.BIGINT, DataTypeEnum.BIT, DataTypeEnum.TINYINT, DataTypeEnum.DATE, DataTypeEnum.DATETIME, DataTypeEnum.DATETIME2, DataTypeEnum.DATETIMEOFFSET, DataTypeEnum.SMALLDATETIME, DataTypeEnum.TIMESTAMP, DataTypeEnum.TIME, DataTypeEnum.UNIQUEIDENTIFIER, DataTypeEnum.TEXT))
                {
                    if (column.Precision > 0 && column.Scale > 0)
                    {
                        createCol += String.Format("({0}, {1})", column.Precision, column.Scale);
                    }
                    else
                    {
                        if (column.MaxLength == -1)
                            createCol += String.Format("(MAX)", column.MaxLength);
                        else if (column.MaxLength > 0)
                            createCol += String.Format("({0})", column.MaxLength);

                    }
                }
                //null
                createCol += column.IsNullable ? " NULL" : " NOT NULL";
                //default 
                if (!column.SqlDefault.IsNullOrEmpty())
                    createCol += string.Format(" DEFAULT {0}", column.SqlDefault);


                //identity
                if (column.IsIdentity && column.PrimaryKey != null)
                {
                    //Identity with numeric column
                    if (column.DbDataType.In(DataTypeEnum.TINYINT, DataTypeEnum.SMALLINT, DataTypeEnum.INT, DataTypeEnum.BIGINT))
                        createCol += " IDENTITY";

                    //Identity with UNIQUEIDENTIFIER column
                    if (column.DbDataType == DataTypeEnum.UNIQUEIDENTIFIER)
                        createCol += " DEFAULT NEWSEQUENTIALID()";
                }

                AddLine(initialIdent + _indent + "{0}{1}", createCol, (table.PrimaryKey == null && (i + 1) == table.Columns.Count) ? "" : ",");
            }
            if (!table.PrimaryKey.IsNull())
            {
                AddLine(initialIdent + _indent + "CONSTRAINT [{0}] PRIMARY KEY {1} ([{2}])", table.PrimaryKey.Name, table.PrimaryKey.IsClustered ? "CLUSTERED" : "NONCLUSTERED", string.Join("],[", table.PrimaryKey.Columns.Select(c => c.Name)));
            }
        }

        protected virtual void GenerateScriptForeignKeys()
        {
            var foreignKeys = _database.Schemas.SelectMany(s => s.TablesBase.Where(t => t.IsTable).OfType<Table>().SelectMany(t => t.ForeignKey.ToArray()));
            foreach (var fk in foreignKeys)
            {
                if (_options.CreateAutomaticIndexes && !fk.RemoveAutomaticIndex)
                {
                    CreateIndex(fk.Parent.Schema.Name, //schema
                        fk.Parent.Name, //table
                        string.Format("IX_FK__{0}", fk.Name), //indexName
                        false,
                        false,
                        false,
                        new ObservableCollection<Column>(fk.ForeignKeyColumns.Select(f => f.ParentColumn)));
                }

                if (_options.DropForeignKeys)
                {
                    AddLine("IF EXISTS(SELECT * FROM sys.foreign_keys WHERE name = '{0}' AND parent_object_id = object_id('[{1}].[{2}]') AND schema_id = schema_id('{1}') )", fk.Name, fk.Parent.Schema.Name, fk.Parent.Name);
                    AddLine(_indent + "ALTER TABLE [{0}].[{1}] DROP CONSTRAINT [{2}]", fk.Parent.Schema.Name, fk.Parent.Name, fk.Name);
                }

                AddLine("ALTER TABLE [{0}].[{1}] ADD CONSTRAINT [{2}] FOREIGN KEY ([{3}]) REFERENCES [{4}].[{5}] ([{6}]) ON DELETE {7} ON UPDATE {8}",
                    fk.Parent.Schema.Name, //schema
                    fk.Parent.Name, //table
                    fk.Name,
                    string.Join("],[", fk.ForeignKeyColumns.Select(c => c.ParentColumn.Name)), //cols
                    fk.Referenced.Schema.Name, //ref schema
                    fk.Referenced.Name, //ref table
                    string.Join("],[", fk.ForeignKeyColumns.Select(c => c.ReferencedColumn.Name)), //ref cols
                    FkActionName(fk.DeleteAction),
                    FkActionName(fk.UpdateAction)
                );
            }

        }
        protected virtual void GenerateScriptIndexes()
        {

            var indexes = _database.Schemas.SelectMany(s => s.TablesBase.Where(t => t.IsTable).OfType<Table>().SelectMany(t => t.Indexes.ToArray()));
            foreach (var index in indexes)
            {
                CreateIndex(index.Table.Schema.Name, index.Table.Name, index.Name, index.IsUnique, index.IsUniqueConstraint, index.IsClustered, index.Columns, index.Include);
            }
        }
        private void CreateIndex(string indexTableSchemaName, string indexTableName, string indexName, bool indexIsUnique, bool indexIsUniqueConstraint, bool indexIsClustered, ObservableCollection<Column> indexColumns)
        {
            CreateIndex(indexTableSchemaName, indexTableName, indexName, indexIsUnique, indexIsUniqueConstraint, indexIsClustered, indexColumns, new ObservableCollection<Column>());
        }
        private void CreateIndex(string indexTableSchemaName, string indexTableName, string indexName, bool indexIsUnique, bool indexIsUniqueConstraint, bool indexIsClustered, ObservableCollection<Column> indexColumns,
            ObservableCollection<Column> indexInclude)
        {
            if (_options.DropIndexes)
            {
                AddLine("IF EXISTS(SELECT * FROM sys.indexes WHERE name = '{0}' AND object_id = object_id('[{1}].[{2}]'))", indexName, indexTableSchemaName, indexTableName);
                AddLine(_indent + "DROP INDEX [{0}] ON [{1}].[{2}]", indexName, indexTableSchemaName, indexTableName);
            }
            AddLine("CREATE{0}{1} INDEX [{2}] ON [{3}].[{4}]([{5}]){6}",
                (indexIsUnique || indexIsUniqueConstraint ? " UNIQUE" : ""), //unique
                indexIsClustered ? " CLUSTERED" : " NONCLUSTERED", //Clustered
                indexName, //index Name
                indexTableSchemaName, //schema
                indexTableName, //table
                String.Join("],[", indexColumns.Select(c => c.Name)), //columns
                (indexInclude.Count == 0 ? "" : " INCLUDE ([" + string.Join("],[", indexInclude.Select(c => c.Name)) + "])") //include
                );
        }
        protected virtual void GenerateScriptStarting() { }
        protected virtual void GenerateScriptFinalizing() { }

        protected virtual bool HasGenerateForeignKeys()
        {
            return true;
        }
        protected virtual bool HasGenerateSchemas()
        {
            return true;
        }
        protected virtual bool HasGenerateDatabase()
        {
            return false;
        }
        #endregion

        #region Private and Protected Methods
        private void GenerateStartScript()
        {
            AddLine("-- Script {0} was generated by Linx Systems", _dbType.ToString());
            AddLine("-- Company home page: http://www.linx.com.br");
            AddLine("-- Script started {0:dd/MM/yyyy HH:mm:ss}", DateTime.Now);
            AddLine();
        }

        private void GenerateEndScript()
        {
            AddLine();
            AddLine("-- Script finished {0:dd/MM/yyyy HH:mm:ss}", DateTime.Now);
        }


        protected void GenerateSubSession(string title)
        {
            AddLine();
            AddLine(new string('-', 80));
            AddComment("-- {0}", title);
            AddLine(new string('-', 80));
        }

        protected void AddComment(string format, params string[] values)
        {
            AddLine(format.TrimStart().StartsWith("--") ? format : "-- " + format, values);
        }

        protected void AddLine(string format = null, params object[] values)
        {
            if (format == null)
            {
                _sb.AppendLine();
            }
            else
            {
                _sb.AppendLine(string.Format(format, values));
            }
        }


        protected string FkActionName(ForeignKey.ReferentialAction action)
        {
            var value = "";
            switch (action)
            {
                case ForeignKey.ReferentialAction.NoAction:
                    value = "NO ACTION";
                    break;
                case ForeignKey.ReferentialAction.Cascade:
                    value = "CASCADE";
                    break;
                default:
                    break;
            }
            return value;
        }
        #endregion
    }
}

using Linx.Tools;
using System;
using System.Collections.Generic;
using Npgsql;
using System.Linq;
using System.Text;
using System.Xml;
using Linx.Tools.Migration;
using System.Data.Common;

namespace Linx.BusinessDataModelDesigner.AppUI.Model
{


    public class PostgresqlDatabaseLoader
    {
        string connString;
        Database database;

        public Database GetDatabaseObjects(Action<string, int> status, string connString)
        {
            Action<string, bool, float> statusUpd = (text, isTerminated, progress) =>
            {
                status(string.Format("Load{0} {1}", isTerminated ? "ed" : "ing", text), (int)progress);
            };
            this.connString = connString;

            database = new Database();

            float totalOperation = 15;
            float operation = 0;

            statusUpd("Database", false, (operation++ / totalOperation) * 100);
            GetDatabaseInfo();
            statusUpd("Database", true, (operation++ / totalOperation) * 100);

            statusUpd("Schemas", false, (operation++ / totalOperation) * 100);
            GetSchemas();
            statusUpd("Schemas", true, (operation++ / totalOperation) * 100);

            statusUpd("Tables", false, (operation++ / totalOperation) * 100);
            GetTables();
            statusUpd("Tables", true, (operation++ / totalOperation) * 100);

            statusUpd("Views", false, (operation++ / totalOperation) * 100);
            GetViews();
            statusUpd("Views", true, (operation++ / totalOperation) * 100);

            statusUpd("Columns", false, (operation++ / totalOperation) * 100);
            GetColumns();
            statusUpd("Columns", true, (operation++ / totalOperation) * 100);

            statusUpd("Primary Keys", false, (operation++ / totalOperation) * 100);
            GetPrimaryKeys();
            statusUpd("Primary Keys", true, (operation++ / totalOperation) * 100);

            statusUpd("Foreing Keys", false, (operation++ / totalOperation) * 100);
            GetForeignKeys();
            statusUpd("Foreing Keys", true, (operation++ / totalOperation) * 100);

            statusUpd("Indexes", false, (operation++ / totalOperation) * 100);
            GetIndexes();
            statusUpd("Indexes", true, (operation++ / totalOperation) * 100);

            status("Finished", 100);

            return database;
        }

        private void GetDatabaseInfo()
        {
            ScriptQueryManager.ExecutePostgreSQLCommandAction(
                connString,
                "select current_database() as dbname",
                dr => database.Name = (string)dr["dbname"]
            );
        }

        private void GetSchemas()
        {
            ScriptQueryManager.ExecutePostgreSQLCommandAction(
                connString,
                "SELECT TABLE_SCHEMA FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA NOT IN ('information_schema','pg_catalog') GROUP BY TABLE_SCHEMA HAVING COUNT(*) > 1",
                dr =>
                database.Schemas.Add(new Schema() { Id = (string)dr["TABLE_SCHEMA"], Name = (string)dr["TABLE_SCHEMA"] })
            );
        }

        private void GetTables()
        {
            foreach (var schema in database.Schemas)
            {
                ScriptQueryManager.ExecutePostgreSQLCommandAction(
                    connString,
                    "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_SCHEMA=@schema ORDER BY TABLE_NAME;",
                    dr => schema.TablesBase.Add(new Table
                    {
                        Id = (string)dr["TABLE_NAME"],
                        Name = (string)dr["TABLE_NAME"]
                    })
                , new NpgsqlParameter("schema", schema.Name));
            }
        }

        private void GetViews()
        {
            foreach (Schema schema in database.Schemas)
            {
                ScriptQueryManager.ExecutePostgreSQLCommandAction(
                    connString,
                    "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='VIEW' AND TABLE_SCHEMA=@schema ORDER BY TABLE_NAME",
                    dr => schema.TablesBase.Add(new View
                    {
                        Id = (string)dr["TABLE_NAME"],
                        Name = (string)dr["TABLE_NAME"]
                    })
                , new NpgsqlParameter("schema", schema.Name));
            }
        }

        T getCellValue<T>(DbDataReader dr, string name, Func<object, object> converter = null)
        {
            var value = dr[name];
            if (value == DBNull.Value)
                return default(T);
            else
            {
                if (converter != null)
                    return (T)converter(value);
                else
                    return (T)value;
            }
        }
        private void GetColumns()
        {
            foreach (Schema schema in database.Schemas)
            {
                foreach (TableBase table in schema.TablesBase)
                {
                    ScriptQueryManager.ExecutePostgreSQLCommandAction(
                        connString,
                       "SELECT COLUMN_NAME, CASE IS_NULLABLE WHEN 'YES' THEN true ELSE false END AS IS_NULLABLE, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, " +
                       "NUMERIC_PRECISION, NUMERIC_SCALE, CASE WHEN column_default LIKE 'nextval(%::regclass)' THEN true ELSE false END AS IS_IDENTITY, COLUMN_DEFAULT " +
                       "FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA=@schema and table_name=@table and data_type != 'USER-DEFINED' ORDER BY ORDINAL_POSITION",
                   dr =>
                   {
                       string colName = (string)dr["COLUMN_NAME"];
                       try
                       {
                           if (isSupported(dr))
                           {
                               table.Columns.Add(new Column
                               {
                                   Id = colName,
                                   Name = colName,
                                   IsNullable = (bool)dr["IS_NULLABLE"],
                                   DbDataType = convertStringToDataTypeEnum((string)dr["DATA_TYPE"]),
                                   MaxLength = (short)getCellValue<int>(dr, "CHARACTER_MAXIMUM_LENGTH"),
                                   Precision = (byte)getCellValue<int>(dr, "NUMERIC_PRECISION"),
                                   Scale = (byte)getCellValue<int>(dr, "NUMERIC_PRECISION"),
                                   IsIdentity = (bool)dr["IS_IDENTITY"],
                                   SqlDefault = ((bool)dr["IS_IDENTITY"] ? "" : getCellValue<string>(dr, "COLUMN_DEFAULT"))
                               });
                           }
                       }
                       catch (Exception ex)
                       {
                           throw new Exception("Erro na coluna [" + table.Name + "].[" + colName + "]", ex);
                       }
                   },
                new NpgsqlParameter("schema", schema.Name), new NpgsqlParameter("table", table.Name));
                }
            }
        }

        private void GetPrimaryKeys()
        {
            foreach (Schema schema in database.Schemas)
            {
                foreach (Table table in schema.TablesBase.OfType<Table>())
                {
                    //get constraint name
                    ScriptQueryManager.ExecutePostgreSQLCommandAction(
                        connString,
                        "SELECT CONSTRAINT_NAME FROM INFORMATION_SCHEMA.table_constraints " +
                        "WHERE TABLE_SCHEMA=@schema and TABLE_NAME=@table and CONSTRAINT_TYPE='PRIMARY KEY';",
                        dr =>
                        {
                            table.PrimaryKey = new PrimaryKey();
                            table.PrimaryKey.Id = (string)dr["CONSTRAINT_NAME"];
                            table.PrimaryKey.Name = (string)dr["CONSTRAINT_NAME"];
                        },
                        new NpgsqlParameter("schema", schema.Name), new NpgsqlParameter("table", table.Name));

                    //get columns
                    if (table.PrimaryKey == null) continue;

                    ScriptQueryManager.ExecutePostgreSQLCommandAction(
                        connString,
                        "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE " +
                        " WHERE TABLE_SCHEMA = @schema AND TABLE_NAME=@table AND CONSTRAINT_NAME=@pkName ORDER BY ORDINAL_POSITION;",
                        dr =>
                        {
                            var columnId = (string)dr["COLUMN_NAME"];
                            var column = table.Columns.Single(c => c.Name == columnId);
                            column.IsPK = true;
                            table.PrimaryKey.Columns.Add(column);
                        },
                        new NpgsqlParameter("schema", schema.Name), new NpgsqlParameter("table", table.Name), new NpgsqlParameter("pkName", table.PrimaryKey.Id));
                }
            }
        }


        private void GetForeignKeys()
        {
            foreach (Schema schema in database.Schemas)
            {
                foreach (Table table in schema.TablesBase.OfType<Table>())
                {
                    ScriptQueryManager.ExecutePostgreSQLCommandAction(
                        connString,
                       "SELECT RC.CONSTRAINT_NAME, ccu.table_name as REFERENCED_TABLE_NAME, RC.DELETE_RULE, RC.UPDATE_RULE " +
                        "FROM INFORMATION_SCHEMA.table_CONSTRAINTS TC join INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS RC on TC.CONSTRAINT_NAME = RC.CONSTRAINT_NAME " +
                        "JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE AS CCU   ON CCU.CONSTRAINT_NAME = RC.CONSTRAINT_NAME " +
                        "WHERE TC.CONSTRAINT_SCHEMA=@schema and TC.TABLE_NAME=@table;",
                        dr =>
                        {
                            var referencedId = (string)dr["REFERENCED_TABLE_NAME"];
                            var referenced = (Table)database.Schemas.SelectMany(t => t.TablesBase).First(t => (string)t.Id == referencedId);
                            if (referenced != null)
                            {
                                var fk = new ForeignKey
                                {
                                    Id = (string)dr["CONSTRAINT_NAME"],
                                    Name = (string)dr["CONSTRAINT_NAME"],
                                    Referenced = referenced,
                                    Parent = table,
                                    DeleteAction = convertStringToReferentialAction((string)dr["DELETE_RULE"]),
                                    UpdateAction = convertStringToReferentialAction((string)dr["UPDATE_RULE"]),

                                };
                                table.ForeignKey.Add(fk);

                                #region get columns
                                ScriptQueryManager.ExecutePostgreSQLCommandAction(
                                   connString,
                                   "SELECT KCU.COLUMN_NAME, CCU.COLUMN_NAME AS REFERENCED_COLUMN_NAME FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE KCU JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE AS CCU ON CCU.CONSTRAINT_NAME = KCU.CONSTRAINT_NAME " +
                                   "WHERE KCU.CONSTRAINT_SCHEMA=@schema AND KCU.TABLE_NAME=@table AND KCU.CONSTRAINT_NAME=@fkName ORDER BY KCU.ORDINAL_POSITION ",
                                   dr_col =>
                                   {
                                       var pColId = (string)dr_col["COLUMN_NAME"];
                                       var rColId = (string)dr_col["REFERENCED_COLUMN_NAME"];
                                       var pCol = fk.Parent.Columns.Single(p => (string)p.Id == pColId);
                                       var rCol = fk.Referenced.Columns.Single(r => (string)r.Id == rColId);
                                       fk.ForeignKeyColumns.Add(new ForeignKeyColumns(
                                           parent: fk.Parent,
                                           referenced: fk.Referenced,
                                           parentColumn: pCol,
                                           referencedColumn: rCol
                                       ));
                                   },
                                   new NpgsqlParameter("schema", schema.Name), new NpgsqlParameter("table", table.Name), new NpgsqlParameter("fkName", fk.Name));
                                #endregion
                            }
                        }, new NpgsqlParameter("schema", schema.Name), new NpgsqlParameter("table", table.Name));
                }
            }
        }

        private void GetIndexes()
        {
            foreach (Schema schema in database.Schemas)
            {
                foreach (Table table in schema.TablesBase.OfType<Table>())
                {
                    Index index = null;
                    //get constraint name //and (is_unique=1 or is_unique_constraint=1) 
                    ScriptQueryManager.ExecutePostgreSQLCommandAction(
                        connString,
                        "SELECT IX.INDEXRELID AS INDEX_ID, I.RELNAME AS INDEX_NAME, IX.INDISUNIQUE AS IS_UNIQUE, IX.INDISCLUSTERED AS IS_CLUSTERED " +
                        "FROM PG_CLASS T JOIN PG_NAMESPACE TS ON T.RELNAMESPACE = TS.OID JOIN PG_INDEX IX ON T.OID = IX.INDRELID JOIN PG_CLASS I ON I.OID = IX.INDEXRELID " +
                        "WHERE IX.INDISPRIMARY = FALSE AND TS.nspname = @schema and T.RELNAME = @table;",
                        dr =>
                        {
                            index = new Index
                            {
                                Id = dr["INDEX_ID"],
                                Name = (string)dr["INDEX_NAME"],
                                IsUnique = (bool)dr["IS_UNIQUE"],
                                IsClustered = (bool)dr["IS_CLUSTERED"],
                                CommandColumns = String.Empty
                            };

                            //get columns
                            ScriptQueryManager.ExecutePostgreSQLCommandAction(
                                connString,
                                "SELECT A.ATTNAME AS COLUMN_NAME FROM PG_INDEX IX JOIN PG_CLASS T ON T.OID = IX.INDRELID  JOIN PG_ATTRIBUTE A ON A.ATTRELID = T.OID AND A.ATTNUM = ANY(IX.INDKEY) " +
                                "WHERE IX.INDEXRELID = @ixID ORDER BY ARRAY_POSITION(INDKEY::INT2[], A.ATTNUM);",
                                drC =>
                                {
                                    var columnId = (string)drC["COLUMN_NAME"];
                                    var isDescending = false;

                                    var column = table.Columns.SingleOrDefault(c => (string)c.Id == columnId);
                                    if (column != null)
                                    {
                                        index.Columns.Add(column);
                                        index.CommandColumns += (index.CommandColumns == String.Empty ? String.Empty : ",") + column.Name + (isDescending ? " DESC" : String.Empty);
                                    }
                                }
                                , new NpgsqlParameter("ixID", (int)(uint)index.Id));

                            table.Indexes.Add(index);
                        },
                        new NpgsqlParameter("schema", schema.Name), new NpgsqlParameter("table", table.Name));
                }
            }
        }



        private bool isSupported(DbDataReader dr)
        {
            if ((new string[] { "ARRAY", "tsvector" }).Contains((string)dr["DATA_TYPE"]))
                return false;

            return true;
        }


        #region Internals
        private ForeignKey.ReferentialAction convertStringToReferentialAction(string text)
        {
            if (text == "CASCADE")
                return ForeignKey.ReferentialAction.Cascade;
            else
                return ForeignKey.ReferentialAction.NoAction;
        }

        private DataTypeEnum convertStringToDataTypeEnum(string type)
        {
            DataTypeEnum value = DataTypeEnum.BIGINT;
            if (type == "integer") type = "int";
            if (type == "character varying") type = "varchar";
            if (type == "character") type = "char";
            if (type == "timestamp without time zone") type = "datetimeoffset";
            if (type == "boolean") type = "bit";
            if (type == "bytea") type = "varbinary";
            if (type == "uuid") type = "uniqueidentifier";

            if (!Enum.TryParse<DataTypeEnum>(type.ToUpper(), out value))
            {
                throw new Exception("type not found: " + type);
            }
            return value;
        }

        #endregion
    }

}

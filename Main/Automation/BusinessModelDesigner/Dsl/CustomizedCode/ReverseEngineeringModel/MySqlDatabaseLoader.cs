using Linx.Tools;
using Linx.Tools.Migration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Linx.BusinessModelDesigner.CustomizedCode.ReverseEngineeringModel
{
    public class MySqlDatabaseLoader : IProviderDatabaseLoader
    {
        string connString;
        Database database;

        public MySqlDatabaseLoader(string connString)
        {
            this.connString = connString;
        }

        public Database GetDatabaseObjects(Action<string, int> status)
        {
            Action<string, bool, float> statusUpd = (text, isTerminated, progress) =>
            {
                status(string.Format("Load{0} {1}", isTerminated ? "ed" : "ing", text), (int)progress);
            };

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
            ExecuteDataTable(
                "select database() as dbname",
                dr => database.Name = (string)dr["dbname"]
            );
        }

        private void GetSchemas()
        {
            database.Schemas.Add(new Schema { Name = "" });
        }

        private void GetTables()
        {
            var schema = database.Schemas[0];
            ExecuteDataTable(
               "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_SCHEMA=@db ORDER BY TABLE_NAME;",
               dr => schema.TablesBase.Add(new Table
               {
                   Id = (string)dr["TABLE_NAME"],
                   Name = (string)dr["TABLE_NAME"]
               })
           , new MySqlParameter("db", database.Name));
        }

        private void GetViews()
        {
            foreach (Schema schema in database.Schemas)
            {
                ExecuteDataTable(
                    "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='VIEW' AND TABLE_SCHEMA=@db ORDER BY TABLE_NAME",
                    dr => schema.TablesBase.Add(new View
                    {
                        Id = (string)dr["TABLE_NAME"],
                        Name = (string)dr["TABLE_NAME"]
                    })
                , new MySqlParameter("db", database.Name));
            }
        }

        T getCellValue<T>(MySqlDataReader dr, string name, Func<object, object> converter = null)
        {
            var value = dr[name];
            if (value == DBNull.Value)
                return default(T);
            else {
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
                    ExecuteDataTable(
                       "SELECT COLUMN_NAME, CASE IS_NULLABLE WHEN 'YES' THEN true ELSE false END AS IS_NULLABLE, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, " +
                       "NUMERIC_PRECISION, NUMERIC_SCALE, CASE WHEN EXTRA LIKE 'auto_increment' THEN true ELSE false END AS IS_IDENTITY, COLUMN_DEFAULT " +
                       "FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA=@db and table_name=@table ORDER BY ORDINAL_POSITION",
                   dr =>
                       table.Columns.Add(new Column
                       {
                           Id = (string)dr["COLUMN_NAME"],
                           Name = (string)dr["COLUMN_NAME"],
                           IsNullable = (long)dr["IS_NULLABLE"] == 1,
                           DbDataType = convertStringToDataTypeEnum((string)dr["DATA_TYPE"]),
                           MaxLength = getCellValue<short>(dr, "CHARACTER_MAXIMUM_LENGTH", o => ulong.Parse(o.ToString()) > (ulong)short.MaxValue ? (short)-1 : short.Parse(o.ToString())),
                           Precision = getCellValue<byte>(dr, "NUMERIC_PRECISION", o => (byte)(ulong)o),
                           Scale = getCellValue<byte>(dr, "NUMERIC_SCALE", o => (byte)(ulong)o),
                           IsIdentity = (long)dr["IS_IDENTITY"] == 1,
                           SqlDefault = getCellValue<string>(dr, "COLUMN_DEFAULT")
                       }),
                new MySqlParameter("db", database.Name), new MySqlParameter("table", table.Name));
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
                    ExecuteDataTable(
                        "SELECT CONSTRAINT_NAME FROM INFORMATION_SCHEMA.table_constraints " +
                        "WHERE TABLE_SCHEMA=@db and TABLE_NAME=@table and CONSTRAINT_TYPE='PRIMARY KEY';",
                        dr =>
                        {
                            table.PrimaryKey = new PrimaryKey();
                            table.PrimaryKey.Id = (string)dr["CONSTRAINT_NAME"];
                            table.PrimaryKey.Name = (string)dr["CONSTRAINT_NAME"];
                        },
                        new MySqlParameter("db", database.Name), new MySqlParameter("table", table.Name));

                    //get columns
                    if (table.PrimaryKey == null) continue;

                    ExecuteDataTable(
                        "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE " +
                        " WHERE TABLE_SCHEMA = @db AND TABLE_NAME=@table AND CONSTRAINT_NAME=@pkName ORDER BY ORDINAL_POSITION;",
                        dr =>
                        {
                            var columnId = (string)dr["COLUMN_NAME"];
                            var column = table.Columns.Single(c => c.Name == columnId);
                            column.IsPK = true;
                            table.PrimaryKey.Columns.Add(column);
                        },
                        new MySqlParameter("db", database.Name), new MySqlParameter("table", table.Name), new MySqlParameter("pkName", table.PrimaryKey.Id));
                }
            }
        }


        private void GetForeignKeys()
        {
            foreach (Schema schema in database.Schemas)
            {
                foreach (Table table in schema.TablesBase.OfType<Table>())
                {
                    ExecuteDataTable(
                       "SELECT CONSTRAINT_NAME, REFERENCED_TABLE_NAME, DELETE_RULE, UPDATE_RULE  FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS " +
                        "WHERE CONSTRAINT_SCHEMA=@db and TABLE_NAME=@table;",
                        dr =>
                        {
                            var referencedId = (string)dr["REFERENCED_TABLE_NAME"];
                            var referenced = database.FindTable(referencedId) as Table;
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
                                ExecuteDataTable(
                                       "SELECT COLUMN_NAME, REFERENCED_COLUMN_NAME FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE " +
                                       "WHERE CONSTRAINT_SCHEMA=@db AND TABLE_NAME=@table AND CONSTRAINT_NAME=@fkName ORDER BY ORDINAL_POSITION ",
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
                                       new MySqlParameter("db", database.Name), new MySqlParameter("table", table.Name), new MySqlParameter("fkName", fk.Name));
                                #endregion
                            }
                        }, new MySqlParameter("db", database.Name), new MySqlParameter("table", table.Name));
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
                    ExecuteDataTable(
                        "SELECT DISTINCT INDEX_NAME, NON_UNIQUE FROM INFORMATION_SCHEMA.STATISTICS WHERE TABLE_SCHEMA=@db AND TABLE_NAME=@table AND INDEX_NAME!='PRIMARY';",
                        dr =>
                        {
                            index = new Index
                            {
                                Id = (string)dr["INDEX_NAME"],
                                Name = (string)dr["INDEX_NAME"],
                                IsUnique = dr["NON_UNIQUE"].Equals(0),
                                CommandColumns = String.Empty
                            };

                            //get columns
                            ExecuteDataTable(
                                    "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.STATISTICS  " +
                                    "WHERE TABLE_SCHEMA=@db AND TABLE_NAME=@table AND INDEX_NAME=@ixName ORDER BY SEQ_IN_INDEX;",
                                    drC =>
                                    {
                                        var columnId = (string)drC["COLUMN_NAME"];
                                        var isDescending = false;

                                        var column = table.Columns.Single(c => (string)c.Id == columnId);
                                        {
                                            index.Columns.Add(column);
                                            index.CommandColumns += (index.CommandColumns == String.Empty ? String.Empty : ",") + column.Name + (isDescending ? " DESC" : String.Empty);
                                        }
                                    }
                                    , new MySqlParameter("db", database.Name), new MySqlParameter("table", table.Name), new MySqlParameter("ixName", index.Name));

                            table.Indexes.Add(index);
                        },
                        new MySqlParameter("db", database.Name), new MySqlParameter("table", table.Name));
                }
            }
        }






        #region Internals
        public void GetProcedureColumns(Procedure procedure, Dictionary<string, string> values)
        {
            
        }

        public List<Column> GetScriptColumns(string sqlScript)
        {
            return new List<Column>();
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(connString);
        }
        private void ExecuteDataTable(string sql, Action<MySqlDataReader> action, params MySqlParameter[] parameters)
        {
            var cn = GetConnection();

            try
            {
                var cmd = cn.CreateCommand();
                cmd.CommandText = sql;
                if (parameters != null)
                    foreach (MySqlParameter _param in parameters)
                    {
                        cmd.Parameters.Add(_param);
                    }
                cn.Open();
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            action(dr);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cn.Close();
                cn.Dispose();
            }
        }
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
            if (type == "year") type = "smallint";
            if (type == "mediumint") type = "int";
            if (type == "blob") type = "varbinary";
            if (type.InList("enum", "set")) type = "varchar";
            if (!Enum.TryParse<DataTypeEnum>(type.ToUpper(), out value))
            {
                throw new Exception(type);
            }
            return value;
        }

        #endregion

        
    }

}

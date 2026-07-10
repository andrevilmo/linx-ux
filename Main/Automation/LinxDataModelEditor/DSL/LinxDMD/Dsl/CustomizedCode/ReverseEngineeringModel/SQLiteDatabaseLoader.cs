using Linx.Tools;
using Linx.Tools.Migration;
using System;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace Linx.BusinessDataModelDesigner.AppUI.Model
{

    public class SQLiteDatabaseLoader
    {
        const float TotalOperation = 15f;
        float Operation = 0;
        string connString;
        Database database;
        DataTable dtTables, dtViews;

        public Database GetDatabaseObjects(Action<string, int> status, string connString)
        {
            Action<string, bool> statusUpd = (text, isTerminated) =>
            {
                status(string.Format("Load{0} {1}", isTerminated ? "ed" : "ing", text), (int)((Operation++ / TotalOperation) * 100));
            };
            this.connString = connString;

            CheckConnection();

            database = new Database();


            statusUpd("Database", false);
            GetDatabaseInfo();
            statusUpd("Database", true);

            statusUpd("Schemas", false);
            GetSchemas();
            statusUpd("Schemas", true);

            statusUpd("Tables", false);
            GetTables();
            statusUpd("Tables", true);

            statusUpd("Views", false);
            GetViews();
            statusUpd("Views", true);

            statusUpd("Columns", false);
            GetColumns();
            statusUpd("Columns", true);

            statusUpd("Primary Keys", false);
            //   GetPrimaryKeys();
            statusUpd("Primary Keys", true);

            statusUpd("Foreing Keys", false);
            GetForeignKeys();
            statusUpd("Foreing Keys", true);

            statusUpd("Indexes", false);
            GetIndexes();
            statusUpd("Indexes", true);

            status("Finished", 100);

            return database;
        }




        private void GetIndexes()
        {
            DataTable dtIndexes = null;
            DataTable dtIndexColumns = null;
            using (var cn = GetConnection())
            {
                cn.Open();
                dtIndexes = cn.GetSchema("Indexes");
                dtIndexColumns = cn.GetSchema("IndexColumns");
            }

            foreach (Schema schema in database.Schemas)
            {
                foreach (Table table in schema.TablesBase.OfType<Table>())
                {

                    Index index = null;
                    //get constraint name //and (is_unique=1 or is_unique_constraint=1) 
                    foreach (var row in dtIndexes.Select(createWhere("TABLE_SCHEMA {0} and TABLE_NAME {1} and primary_key = false", schema.Id, table.Id)))
                    {
                        index = new Index
                        {
                            Id = (string)row["INDEX_NAME"],
                            Name = (string)row["INDEX_NAME"],
                            IsUnique = (bool)row["UNIQUE"],
                            IsUniqueConstraint = false,
                            IsClustered = tryParseToBoolean(row["CLUSTERED"]),
                            CommandColumns = String.Empty
                        };

                        foreach (var rowCol in dtIndexColumns.Select(createWhere("INDEX_NAME {0}", index.Id)))
                        {
                            var columnId = (string)rowCol["COLUMN_NAME"];
                            var isDescending = (string)rowCol["SORT_MODE"] == "DESC";

                            var column = table.Columns.Single(c => c.Name == columnId);

                            index.Columns.Add(column);
                            index.CommandColumns += (index.CommandColumns == String.Empty ? String.Empty : ",") + column.Name + (isDescending ? " DESC" : String.Empty);
                        }

                        table.Indexes.Add(index);
                    }
                }
            }
        }


        private void GetForeignKeys()
        {
            DataTable dtFK = null;
            using (var cn = GetConnection())
            {
                cn.Open();
                dtFK = cn.GetSchema("ForeignKeys");
            }

            foreach (Schema schema in database.Schemas)
            {
                foreach (Table table in schema.TablesBase.OfType<Table>())
                {
                    var dataView = new DataView(dtFK, createWhere("TABLE_SCHEMA {0} and TABLE_NAME {1}", schema.Id, table.Id), "FKEY_ID", DataViewRowState.Unchanged);

                    foreach (DataRow row in dataView.ToTable(true, "FKEY_ID", "FKEY_TO_TABLE", "FKEY_ON_UPDATE", "FKEY_ON_DELETE").Rows)
                    {

                        var referencedId = (string)row["FKEY_TO_TABLE"];
                        var referenced = database.FindTable(referencedId);
                        if (referenced != null)
                        {
                            var fk = new ForeignKey
                            {
                                Id = table.Name + row["FKEY_ID"].ToString(),
                                Name = table.Name + row["FKEY_ID"].ToString(),
                                Referenced = referenced,
                                Parent = table,
                                DeleteAction = getForeignKeyAction((string)row["FKEY_ON_UPDATE"]),
                                UpdateAction = getForeignKeyAction((string)row["FKEY_ON_DELETE"]),
                            };

                            table.ForeignKey.Add(fk);

                            #region get columns
                            foreach (DataRowView rowCol in dataView.FindRows(row["FKEY_ID"]))
                            {
                                var pColId = (string)rowCol["FKEY_FROM_COLUMN"];
                                var rColId = (string)rowCol["FKEY_TO_COLUMN"];
                                var pCol = fk.Parent.Columns.Single(p => p.Name == pColId);
                                Column rCol = null;
                                rCol = fk.Referenced.Columns.FirstOrDefault(r => r.Name == rColId);
                                if (rCol == null)
                                    rCol = fk.Referenced.Columns.FirstOrDefault(r => r.IsPK);
                                fk.ForeignKeyColumns.Add(new ForeignKeyColumns(
                                    parent: fk.Parent,
                                    referenced: fk.Referenced,
                                    parentColumn: pCol,
                                    referencedColumn: rCol
                                ));
                            }
                            
                            #endregion
                        }
                    }
                }
            }
        }

        private ForeignKey.ReferentialAction getForeignKeyAction(string action)
        {
            if (action == "NO ACTION")
                return ForeignKey.ReferentialAction.NoAction;
            else
                return ForeignKey.ReferentialAction.Cascade;
        }


        private void GetColumns()
        {
            DataTable dtTColumns = null;
            DataTable dtVColumns = null;
            using (var cn = GetConnection())
            {
                cn.Open();
                dtTColumns = cn.GetSchema("columns");
                dtVColumns = cn.GetSchema("viewcolumns");
            }

            foreach (Schema schema in database.Schemas)
            {
                foreach (TableBase tableBase in schema.TablesBase)
                {
                    if (tableBase is Table)
                    {
                        var table = (Table)tableBase;
                        foreach (DataRowView row in new DataView(dtTColumns, createWhere("TABLE_SCHEMA {0} and TABLE_NAME {1}", schema.Id.IsNullOrEmpty() ? "sqlite_default_schema" : schema.Id, table.Id), "ORDINAL_POSITION", DataViewRowState.Unchanged))
                        {
                            var column = new Column
                            {
                                Id = (string)row["COLUMN_NAME"],
                                Name = (string)row["COLUMN_NAME"],
                                IsNullable = (bool)row["IS_NULLABLE"],
                                DbDataType = getEnumType(row["DATA_TYPE"]),
                                MaxLength = tryParseToShort(row["NUMERIC_PRECISION"]),
                                Precision = tryParseToByte(row["NUMERIC_PRECISION"].ToString()),
                                Scale = tryParseToByte(row["NUMERIC_SCALE"].ToString()),
                                IsIdentity = (bool)row["AUTOINCREMENT"],
                                IsPK = (bool)row["PRIMARY_KEY"],
                                SqlDefault = (bool)row["COLUMN_HASDEFAULT"] ? (string)row["COLUMN_DEFAULT"] : string.Empty,
                            };
                            table.Columns.Add(column);
                            if ((bool)row["PRIMARY_KEY"])
                            {
                                if (table.PrimaryKey == null) table.PrimaryKey = new PrimaryKey();
                                table.PrimaryKey.Columns.Add(column);
                            }
                        }
                    }
                    else
                    {
                        var view = (View)tableBase;
                        foreach (DataRowView row in new DataView(dtVColumns, createWhere("VIEW_SCHEMA {0} and VIEW_NAME {1}", schema.Id, view.Id), "ORDINAL_POSITION", DataViewRowState.Unchanged))
                        {
                            var column = new Column
                            {
                                Id = (string)row["VIEW_COLUMN_NAME"],
                                Name = (string)row["VIEW_COLUMN_NAME"],
                                IsNullable = (bool)row["IS_NULLABLE"],
                                DbDataType = getEnumType(row["DATA_TYPE"]),
                                MaxLength = tryParseToShort(row["NUMERIC_PRECISION"].ToString()),
                                Precision = tryParseToByte(row["NUMERIC_PRECISION"].ToString()),
                                Scale = tryParseToByte(row["NUMERIC_SCALE"].ToString()),
                                IsIdentity = (bool)row["AUTOINCREMENT"],
                                SqlDefault = (bool)row["COLUMN_HASDEFAULT"] ? (string)row["COLUMN_DEFAULT"] : string.Empty,
                            };
                            view.Columns.Add(column);
                            //if ((bool)row["PRIMARY_KEY"])
                            //    view.PrimaryKey.Columns.Add(column);
                        }
                    }



                }
            }
        }



        private void GetTables()
        {
            foreach (Schema schema in database.Schemas)
            {
                foreach (DataRow row in dtTables.Select(createWhere("TABLE_SCHEMA {0} and TABLE_TYPE = 'table'", schema.Id)))
                {
                    schema.TablesBase.Add(new Table() { Id = row["TABLE_NAME"].ToString(), Name = row["TABLE_NAME"].ToString() });
                }
            }
        }

        private string createWhere(string format, params object[] parameters)
        {
            var parametersFormatted = new string[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
                parametersFormatted[i] = parameters[i].IsNullOrEmpty() ? " is null" : " = '" + parameters[i] + "'";
            return string.Format(format, parametersFormatted);
        }

        private void GetViews()
        {
            foreach (Schema schema in database.Schemas)
            {
                foreach (DataRow row in dtViews.Select(createWhere("TABLE_SCHEMA {0}", schema.Id)))
                {
                    schema.TablesBase.Add(new View
                    {
                        Id = (string)row["TABLE_NAME"],
                        Name = (string)row["TABLE_NAME"]
                    });
                }
            }
        }

        private void GetSchemas()
        {
            using (var cn = GetConnection())
            {
                cn.Open();
                dtTables = cn.GetSchema("tables");
                dtViews = cn.GetSchema("views");
            }
            using (var schemas = new DataView(dtTables).ToTable(true, "TABLE_SCHEMA"))
            {
                foreach (DataRow row in schemas.Rows)
                {
                    database.Schemas.Add(new Schema() { Id = row[0].ToString(), Name = row[0].ToString() });
                }
            }

            using (var schemas = new DataView(dtViews).ToTable(true, "TABLE_SCHEMA"))
            {
                foreach (DataRow row in schemas.Rows)
                {
                    if (!database.Schemas.Any(s => (string)s.Id == row[0].ToString()))
                        database.Schemas.Add(new Schema() { Id = row[0].ToString(), Name = row[0].ToString() });
                }
            }
        }

        private void GetDatabaseInfo()
        {
            using (var cn = GetConnection())
            {
                database.Name = cn.Database;
            }

        }

        #region Internals

        private DataTypeEnum getEnumType(object dataTypeObject)
        {
            DataTypeEnum type = DataTypeEnum.TEXT;

            if (dataTypeObject is DBNull)
                return type;

            var dataType = dataTypeObject.ToString();

            if (dataType.Contains("("))
                dataType = dataType.Substring(0, dataType.IndexOf("("));


            switch (dataType.ToLower())
            {
                case "bit":
                    type = DataTypeEnum.BIT;
                    break;
                case "tinyint":
                    type = DataTypeEnum.TINYINT;
                    break;
                case "smallint":
                    type = DataTypeEnum.SMALLINT;
                    break;
                case "int":
                case "integer":
                    type = DataTypeEnum.INT;
                    break;
                case "bigint":
                    type = DataTypeEnum.BIGINT;
                    break;
                case "char":
                case "nchar":
                    type = DataTypeEnum.CHAR;
                    break;
                case "varchar":
                case "nvarchar":
                    type = DataTypeEnum.VARCHAR;
                    break;
                case "decimal":
                    type = DataTypeEnum.DECIMAL;
                    break;
                case "numeric":
                    type = DataTypeEnum.NUMERIC;
                    break;
                case "date":
                    type = DataTypeEnum.DATE;
                    break;
                case "datetime":
                    type = DataTypeEnum.DATETIME;
                    break;
                case "text":
                    type = DataTypeEnum.TEXT;
                    break;
                case "blob":
                    type = DataTypeEnum.VARBINARY;
                    break;
                case "real":
                    type = DataTypeEnum.REAL;
                    break;
                case "double":
                    type = DataTypeEnum.FLOAT;
                    break;
                case "float":
                    type = DataTypeEnum.FLOAT;
                    break;
                default:
                    break;
            }
            return type;
        }

        private bool tryParseToBoolean(object value)
        {
            bool valueBool;
            if (value != null && bool.TryParse(value.ToString(), out valueBool))
                return valueBool;

            else return false;
        }

        private byte tryParseToByte(object value)
        {
            byte valueByte;
            if (value != null && byte.TryParse(value.ToString(), out valueByte))
                return valueByte;
            else return 0;
        }

        private short tryParseToShort(object value)
        {
            short valueShort;
            if (value != null && short.TryParse(value.ToString(), out valueShort))
                return valueShort;
            else return 0;
        }

        private void ExecuteDataTable(string sql, Action<SQLiteDataReader> action, params SQLiteParameter[] parameters)
        {
            var cn = GetConnection();

            try
            {
                var cmd = cn.CreateCommand();
                cmd.CommandText = sql;
                if (parameters != null)
                    foreach (SQLiteParameter _param in parameters)
                    {
                        cmd.Parameters.Add(_param);
                    }
                cn.Open();
                using (SQLiteDataReader dr = cmd.ExecuteReader())
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

        private SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(connString);
        }

        private void CheckConnection()
        {
            string file = null;

            if (connString.IsNullOrEmpty())
                throw new ArgumentException("Não foi encontrada a conexão com o provider, configure novamente o provider.");

            file = connString.ToLower().Extract("data source=", ";");
            if (!System.IO.File.Exists(file))
                throw new System.IO.FileNotFoundException("Não encontrado arquivo: " + file);


            using (var cn = GetConnection())
                cn.Open();

        }
        #endregion
    }
}


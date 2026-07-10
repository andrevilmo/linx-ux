using Linx.Tools.Migration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Xml;
using Linx.Tools;

namespace Linx.BusinessModelDesigner.CustomizedCode.ReverseEngineeringModel
{

    public class SqlServerDatabaseLoader : IProviderDatabaseLoader
    {
        string connString;
        Database database;

        public SqlServerDatabaseLoader(string connString)
        {
            this.connString = connString;
        }

        public Database GetDatabaseObjects(Action<string, int> status)
        {
            Action<string, bool, float> statusUpd = (text, isTerminated, progress) =>
            {
                status?.Invoke(string.Format("Load{0} {1}", isTerminated ? "ed" : "ing", text), (int)progress);
            };

            database = new Database();

            float totalOperation = 19;
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

            statusUpd("Functions", false, (operation++ / totalOperation) * 100);
            GetFunctions();
            statusUpd("Functions", true, (operation++ / totalOperation) * 100);

            statusUpd("Stored Procedures", false, (operation++ / totalOperation) * 100);
            GetStoredProcedures();
            statusUpd("Stored Procedures", true, (operation++ / totalOperation) * 100);

            statusUpd("Finished", true, 100);

            return database;
        }

        private void GetFunctions()
        {
            foreach (Schema schema in database.Schemas)
            {
                ExecuteDataTable(
                      " select object_id, name, type from sys.objects " +
                      " where schema_id = @schemaId and type in ('IF', 'TF') order by name", //types: FN=scalar, IF=inline, TF=Table function
                      dr =>
                      {
                          var fn = new Function()
                          {
                              Id = (int)dr["object_id"],
                              Name = dr["name"].ToString(),
                              FunctionType = Function.getFunctionTypeEnum(dr["type"].ToString())
                          };
                          //get parameters
                          GetParameterDB(fn);

                          //get definitions
                          ExecuteDataTable(
                              " select c.column_id, c.name, system_type_id = Cast(c.system_type_id as int), " +
                              " cast(isnull(sc.prec, 0) as smallint) as max_length, c.precision, c.scale, c.is_nullable " +
                              " from sys.Columns c join SysColumns as sc on (c.object_id =  sc.id and c.column_id = sc.colid and c.name = sc.name) " +
                              " where c.object_id= @objectId order by c.column_id",
                          drFC =>
                          {
                              fn.Columns.Add(new Column
                              {
                                  Id = (int)drFC["column_id"],
                                  Name = (string)drFC["name"],
                                  DbDataType = (DataTypeEnum)drFC["system_type_id"],
                                  MaxLength = (short)drFC["max_length"],
                                  Precision = (byte)drFC["precision"],
                                  Scale = (byte)drFC["scale"],
                                  IsNullable = (bool)drFC["is_nullable"]

                              });
                          }, new SqlParameter("objectId", fn.Id));


                          schema.FunctionsBase.Add(fn);
                      }, new SqlParameter("schemaId", schema.Id));
            }
        }

        private void GetStoredProcedures()
        {
            foreach (Schema schema in database.Schemas)
            {
                ExecuteDataTable(
                      " select object_id, name, type from sys.procedures " +
                      " where schema_id = @schemaId order by name",
                      dr =>
                      {
                          var proc = new Procedure()
                          {
                              Id = (int)dr["object_id"],
                              Name = dr["name"].ToString()
                          };
                          //parameters
                          GetParameterDB(proc);

                          schema.FunctionsBase.Add(proc);
                      }, new SqlParameter("schemaId", schema.Id));
            }
        }

        private void GetParameterDB(FunctionBase fnBase)
        {
            ExecuteDataTable(
                "select parameter_id, name, system_type_id = Cast(system_type_id as int), max_length, precision, scale from sys.parameters " +
                "where object_id = @procedureId and is_output = 0 order by parameter_id",
                drP =>
                {
                    fnBase.Parameters.Add(
                        new Parameter()
                        {
                            Id = (int)drP["parameter_id"],
                            Name = drP["name"].ToString(),
                            DbDataType = (DataTypeEnum)drP["system_type_id"],
                            MaxLength = (short)drP["max_length"],
                            Precision = (byte)drP["precision"],
                            Scale = (byte)drP["scale"],
                        });
                }, new SqlParameter("procedureId", fnBase.Id));
        }

        public void GetProcedureColumns(Procedure procedure, Dictionary<string, string> values)
        {
            string parameters = string.Join(", ", procedure.Parameters.Select(p => string.Format("{0}='{1}'", p.Name, values[p.Name])));
            GetColumnDynamic(string.Format("EXEC [{0}].[{1}] {2}", procedure.Schema.Name, procedure.Name, parameters), (col) => procedure.Columns.Add(col));

        }
        private void GetColumnDynamic(string script, Action<Column> addFunc)
        {
            string command = string.Format(@" declare @sql as nvarchar(MAX) = N'{0}';" +
                " SELECT column_ordinal, name, is_nullable, system_type_id = Cast(system_type_id as int), max_length, precision, scale, error_message " +
                " FROM sys.dm_exec_describe_first_result_set(@sql, null, 0);", script.Replace("'", "''"));

            bool hasError = false;
            try
            {
                ExecuteDataTable(command,
                    dr =>
                    {
                        if (!hasError)
                        {
                            if (dr.IsDBNull(dr.GetOrdinal("error_message")))
                            {
                                addFunc(new Column
                                {
                                    Id = (int)dr["column_ordinal"],
                                    Name = (string)dr["name"],
                                    DbDataType = (DataTypeEnum)dr["system_type_id"],
                                    MaxLength = (short)dr["max_length"],
                                    Precision = (byte)dr["precision"],
                                    Scale = (byte)dr["scale"],
                                    IsNullable = (bool)dr["is_nullable"]

                                });
                            }
                            else
                            {
                                throw new Exception((string)dr["error_message"]);
                            }
                        }
                    }, null);

            }
            catch
            {
                hasError = true;
            }

            try
            {
                if (hasError)
                {
                    GetNoMetaScriptColumns(script, addFunc);
                }
            }
            catch (Exception exp)
            {
                System.Windows.Forms.MessageBox.Show(exp.GetCompleteMessage(), "Alerta", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
        }

        private void GetNoMetaScriptColumns(string sqlScript, Action<Column> addFunc)
        {
            ExecuteDataTable("SET FMTONLY ON; \r\n" + sqlScript,
                dr =>
                {
                    for (var i = 0; i < dr.FieldCount; i++)
                    {
                        addFunc(new Column
                        {
                            Id = i,
                            Name = dr.GetName(i),
                            DbDataType = (DataTypeEnum)Enum.Parse(typeof(DataTypeEnum), dr.GetDataTypeName(i), true),
                            IsNullable = true,
                            MaxLength = 25,
                            Precision = 13,
                            Scale = 2
                        });
                    }
                }, null, false);

        }

        public List<Column> GetScriptColumns(string sqlScript)
        {
            List<Column> result = new List<Column>();

            GetColumnDynamic(string.Format(@"SELECT * FROM (" + sqlScript + ") AS A WHERE 1=0"), (col) => result.Add(col));

            return result;
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
                        " select index_id, name, is_unique, is_unique_constraint, type_desc from sys.indexes " +
                        " where is_primary_key=0 and type_desc in ('CLUSTERED', 'NONCLUSTERED') and object_id=@table_id order by index_id",
                        dr =>
                        {
                            index = new Index
                            {
                                Id = (int)dr["index_id"],
                                Name = (string)dr["name"],
                                IsUnique = (bool)dr["is_unique"],
                                IsUniqueConstraint = (bool)dr["is_unique_constraint"],
                                IsClustered = ((string)dr["type_desc"]).Trim() == "CLUSTERED",
                                CommandColumns = String.Empty
                            };

                            //get columns
                            ExecuteDataTable(
                    "select column_id, is_included_column, is_descending_key from sys.index_columns " +
                    " where object_id=@tableId and index_id=@indexId order by key_ordinal",
                    drC =>
                    {
                        var columnId = (int)drC["column_id"];
                        var isInclude = (bool)drC["is_included_column"];
                        var isDescending = (bool)drC["is_descending_key"];

                        var column = table.Columns.Single(c => (int)c.Id == columnId);
                        if (isInclude)
                        {
                            index.Include.Add(column);
                        }
                        else
                        {
                            index.Columns.Add(column);
                            index.CommandColumns += (index.CommandColumns == String.Empty ? String.Empty : ",") + column.Name + (isDescending ? " DESC" : String.Empty);
                        }
                    }
                    , new SqlParameter("tableId", table.Id), new SqlParameter("indexId", index.Id));

                            table.Indexes.Add(index);
                        }
                        , new SqlParameter("table_id", table.Id));
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
                        " select object_id, name, referenced_object_id, delete_referential_action, update_referential_action " +
                        " from sys.foreign_keys where parent_object_id = @table_id  order by name",
                        dr =>
                        {
                            var referencedId = (int)dr["referenced_object_id"];
                            var referenced = database.FindTable(referencedId);
                            if (referenced != null)
                            {
                                var fk = new ForeignKey
                                {
                                    Id = (int)dr["object_id"],
                                    Name = (string)dr["name"],
                                    Referenced = referenced,
                                    Parent = table,
                                    DeleteAction = (ForeignKey.ReferentialAction)(byte)dr["delete_referential_action"],
                                    UpdateAction = (ForeignKey.ReferentialAction)(byte)dr["update_referential_action"],
                                };
                                table.ForeignKey.Add(fk);

                                #region get columns
                                ExecuteDataTable(
                       " select parent_column_id, referenced_column_id from sys.foreign_key_columns " +
                       " where constraint_object_id = @fkId order by constraint_column_id ",
                       dr_col =>
                       {
                           var pColId = (int)dr_col["parent_column_id"];
                           var rColId = (int)dr_col["referenced_column_id"];
                           var pCol = fk.Parent.Columns.Single(p => (int)p.Id == pColId);
                           var rCol = fk.Referenced.Columns.Single(r => (int)r.Id == rColId);
                           fk.ForeignKeyColumns.Add(new ForeignKeyColumns(
                   parent: fk.Parent,
                   referenced: fk.Referenced,
                   parentColumn: pCol,
                   referencedColumn: rCol
               ));
                       }, new SqlParameter("fkId", fk.Id));
                                #endregion
                            }
                        }, new SqlParameter("table_id", table.Id));
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
                        " select index_id, name, type_desc from sys.indexes " +
                        " where is_primary_key = 1 and object_id = @table_id order by index_id",
                        dr =>
                        {
                            table.PrimaryKey = new PrimaryKey();
                            table.PrimaryKey.Id = (int)dr["index_id"];
                            table.PrimaryKey.Name = (string)dr["name"];
                            table.PrimaryKey.IsClustered = ((string)dr["type_desc"]).Trim() == "CLUSTERED";
                        }
                        , new SqlParameter("table_id", table.Id));

                    //get columns
                    if (table.PrimaryKey == null) continue;

                    ExecuteDataTable(
                        "select column_id from sys.index_columns " +
                        " where object_id = @table_id and index_id = @index_id order by index_column_id",
                        dr =>
                        {
                            var columnId = (int)dr["column_id"];
                            var column = table.Columns.Single(c => (int)c.Id == columnId);
                            column.IsPK = true;
                            table.PrimaryKey.Columns.Add(column);
                        }
                        , new SqlParameter("table_id", table.Id), new SqlParameter("index_id", table.PrimaryKey.Id));
                }
            }
        }

        private void GetColumns()
        {
            foreach (Schema schema in database.Schemas)
            {
                foreach (TableBase table in schema.TablesBase)
                {
                    ExecuteDataTable(
                       "select c.column_id, c.name, c.is_nullable, system_type_id = Cast(c.system_type_id as int), " +
                       " cast(isnull(sc.prec, 0) as smallint) as max_length, c.precision, c.scale, isnull(sd.definition, '') as SqlDefault, " +
                       " is_identity = cast( case when Exists(select 1 from sys.identity_columns where object_id = c.object_Id and column_id = c.column_id) then 1 else 0 end as bit) " +
                       " from sys.Columns as c join SysColumns as sc on (c.object_id =  sc.id and c.column_id = sc.colid) " +
                       " left join sys.default_constraints sd on c.[object_id] = sd.[parent_object_id] and c.column_id = sd.parent_column_id " +
                       " where c.object_id= @objectId order by c.column_id",
                   dr =>
                       table.Columns.Add(new Column
                       {
                           Id = (int)dr["column_id"],
                           Name = (string)dr["name"],
                           IsNullable = (bool)dr["is_nullable"],
                           DbDataType = (DataTypeEnum)dr["system_type_id"],
                           MaxLength = (short)dr["max_length"],
                           Precision = (byte)dr["precision"],
                           Scale = (byte)dr["scale"],
                           IsIdentity = (bool)dr["is_identity"],
                           SqlDefault = (string)dr["SqlDefault"]
                       })
               , new SqlParameter("objectId", table.Id));
                }
            }
        }

        private void GetTables()
        {
            foreach (Schema schema in database.Schemas)
            {
                ExecuteDataTable(
                    "select object_id, name from sys.tables where type='U' and schema_id = @schemaId order by name",
                    dr =>
                        schema.TablesBase.Add(new Table
                        {
                            Id = (int)dr["object_id"],
                            Name = (string)dr["name"]
                        })
                , new SqlParameter("schemaId", schema.Id));
            }
        }

        private void GetViews()
        {
            foreach (Schema schema in database.Schemas)
            {
                ExecuteDataTable(
                    "select object_id, name from sys.views where schema_id = @schemaId order by name",
                    dr =>
                        schema.TablesBase.Add(new View
                        {
                            Id = (int)dr["object_id"],
                            Name = (string)dr["name"]
                        })
                , new SqlParameter("schemaId", schema.Id));
            }
        }

        private void GetSchemas()
        {
            ExecuteDataTable(
                 "select schema_id, name from sys.schemas where (principal_id != schema_id and name != 'db_owner') or name = 'dbo' order by name",
                 dr =>
                     database.Schemas.Add(new Schema
                     {
                         Id = (int)dr["schema_id"],
                         Name = (string)dr["name"]
                     })
             );
        }

        private void GetDatabaseInfo()
        {
            ExecuteDataTable(
                "select db_name() as dbname",
                dr => database.Name = (string)dr["dbname"]
            );
        }

        #region Internals

        private void ExecuteDataTable(string sql, Action<SqlDataReader> action, params SqlParameter[] parameters)
        {
            ExecuteDataTable(sql, action, parameters, true);
        }

        private void ExecuteDataTable(string sql, Action<SqlDataReader> action, SqlParameter[] parameters, bool iterationInList)
        {
            var cn = GetConnection();

            try
            {
                var cmd = cn.CreateCommand();
                cmd.CommandText = sql;
                if (parameters != null)
                    foreach (SqlParameter _param in parameters)
                    {
                        cmd.Parameters.Add(_param);
                    }
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (iterationInList)
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                action(dr);
                            }
                        }
                    }
                    else
                    {
                        action(dr);
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

        private SqlConnection GetConnection()
        {
            return new SqlConnection(connString);
        }

        #endregion
    }
}


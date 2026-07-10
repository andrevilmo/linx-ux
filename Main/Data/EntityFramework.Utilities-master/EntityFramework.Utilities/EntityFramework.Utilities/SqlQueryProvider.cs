using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Linx.LinqExtensions.Dynamic;
using System.Reflection;
using Linx.Tools;

namespace EntityFramework.Utilities
{
    public class SqlQueryProvider : IQueryProvider
    {
        public bool CanDelete { get { return true; } }
        public bool CanUpdate { get { return true; } }
        public bool CanInsert { get { return true; } }
        public bool CanBulkUpdate { get { return true; } }

        public string GetDeleteQuery(QueryInformation queryInfo)
        {
            return string.Format("DELETE FROM [{0}].[{1}] {2}", queryInfo.Schema, queryInfo.Table, queryInfo.WhereSql);
        }

        public string GetUpdateQuery(QueryInformation predicateQueryInfo, QueryInformation modificationQueryInfo)
        {
            var msql = modificationQueryInfo.WhereSql.Replace("WHERE ", "");
            var indexOfAnd = msql.IndexOf("AND");
            var update = indexOfAnd == -1 ? msql : msql.Substring(0, indexOfAnd).Trim();

            var updateRegex = new Regex(@"(\[[^\]]+\])[^=]+=(.+)", RegexOptions.IgnoreCase);
            var match = updateRegex.Match(update);
            string updateSql;
            if (match.Success)
            {
                var col = match.Groups[1];
                var rest = match.Groups[2].Value;

                rest = SqlStringHelper.FixParantheses(rest);

                updateSql = col.Value + " = " + rest;
            }
            else
            {
                updateSql = string.Join(" = ", update.Split(new string[] { " = " }, StringSplitOptions.RemoveEmptyEntries).Reverse());
            }


            return string.Format("UPDATE [{0}].[{1}] SET {2} {3}", predicateQueryInfo.Schema, predicateQueryInfo.Table, updateSql, predicateQueryInfo.WhereSql);
        }

        public void InsertItems<T>(IEnumerable<T> items, string schema, string tableName, IList<ColumnMapping> properties, DbConnection storeConnection, int? batchSize)
        {
            using (var reader = new EFDataReader<T>(items, properties))
            {
                var con = storeConnection as SqlConnection;
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
                using (SqlBulkCopy copy = new SqlBulkCopy(con))
                {
                    copy.BatchSize = Math.Min(reader.RecordsAffected, batchSize ?? 15000); //default batch size
                    if (!string.IsNullOrWhiteSpace(schema))
                    {
                        copy.DestinationTableName = string.Format("[{0}].[{1}]", schema, tableName);
                    }
                    else
                    {
                        copy.DestinationTableName = "[" + tableName + "]";
                    }

                    copy.NotifyAfter = 0;

                    foreach (var i in Enumerable.Range(0, reader.FieldCount))
                    {
                        copy.ColumnMappings.Add(i, properties[i].NameInDatabase);
                    }
                    try
                    {
                        copy.WriteToServer(reader);
                    }
                    catch (Exception excep)
                    {
                        if (excep.Message.Contains("Received an invalid column length from the bcp client for colid"))
                        {
                            string pattern = @"\d+";
                            Match match = Regex.Match(excep.Message.ToString(), pattern);
                            var index = Convert.ToInt32(match.Value) - 1;

                            FieldInfo fi = typeof(SqlBulkCopy).GetField("_sortedColumnMappings", BindingFlags.NonPublic | BindingFlags.Instance);
                            var sortedColumns = fi.GetValue(copy);
                            var _items = (Object[])sortedColumns.GetType().GetField("_items", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(sortedColumns);

                            FieldInfo itemdata = _items[index].GetType().GetField("_metadata", BindingFlags.NonPublic | BindingFlags.Instance);
                            var metadata = itemdata.GetValue(_items[index]);

                            var column = metadata.GetType().GetField("column", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).GetValue(metadata);
                            var length = metadata.GetType().GetField("length", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).GetValue(metadata);
                            throw new Exception(String.Format("Column: {0} contains data with a length greater than: {1}", column, length));
                        }

                        throw;
                    }
                    copy.Close();
                }
            }
        }

        private string GetNewSequence(int length)
        {
            var rndDigits = new System.Text.StringBuilder().Insert(0, "0123456789", length).ToString().ToCharArray();
            return string.Join("", rndDigits.OrderBy(o => Guid.NewGuid()).Take(length));
        }

        private string GetDeletedCondition(ColumnMapping deletedProperty, string alias, bool value, string startCondition)
        {
            string result = "";
            if (deletedProperty != null)
            {
                result = (String.IsNullOrWhiteSpace(startCondition) ? "" : " " + startCondition + " ") + string.Format("{0}.[{1}] = " + (value ? "1" : "0"), alias, deletedProperty.NameInDatabase);
            }
            return result;
        }

        public List<WarningResult<T>> UpdateItems<T>(IEnumerable<T> items, string schema, string tableName, ScriptEvent scriptEvent, string deletedPropertyName, IList<ColumnMapping> properties, DbConnection storeConnection, int? batchSize, UpdateSpecification<T> updateSpecification, List<ForeignKeyCfg> fkCfgs)
        {
            string newLine = "\r\n";
            List<WarningResult<T>> result = new List<WarningResult<T>>();

            ColumnMapping deletedProperty = (String.IsNullOrWhiteSpace(deletedPropertyName) ? null : properties.FirstOrDefault(p => p.NameOnObject == deletedPropertyName));

            var primaryKeys = properties.Where(p => p.IsBusinessKey).ToArray();
            if (primaryKeys.Length == 0)
                primaryKeys = properties.Where(p => p.IsPrimaryKey).ToArray();
            string tmpName = tableName + "_" + GetNewSequence(10);
            var tempTableName = "#temp_" + tmpName;
            var tableWarningsName = "#wtemp_" + tmpName;
            var columnsToUpdate = (updateSpecification.Properties.Count() == 0 ? properties.Select(p => p.NameOnObject).ToDictionary(x => x) : updateSpecification.Properties.Select(p => p.GetPropertyName()).ToDictionary(x => x));
            var filtered = properties.Where(p => columnsToUpdate.ContainsKey(p.NameOnObject) || p.IsPrimaryKey || p.IsBusinessKey).ToList();
            var columns = filtered.Select(c => "[" + c.NameInDatabase + "] " + c.DataType + (c.DataType.ToLower().Contains("char") || c.DataType.ToLower().Contains("text") ? " COLLATE database_default" : ""));
            var pkColumns = primaryKeys.Select(c => "[" + c.NameInDatabase + "] " + c.DataType + (c.DataType.ToLower().Contains("char") || c.DataType.ToLower().Contains("text") ? " COLLATE database_default" : ""));
            var pkConstraint = string.Join(", ", primaryKeys.Select(c => "[" + c.NameInDatabase + "]"));

            var controlColumns = new Dictionary<string, string>();
            foreach (var fkCfg in fkCfgs)
            {
                controlColumns.Add(fkCfg.Table, "Exists_" + fkCfg.Table);
            }

            var str = string.Format("CREATE TABLE {0}.[{1}]({2}, PRIMARY KEY ({3}))", schema, tempTableName, string.Join(", ", columns.Union(controlColumns.Values.Select(e => "[" + e + "] bit"))), pkConstraint) +
            newLine + string.Format("CREATE TABLE {0}.[{1}]({2})", schema, tableWarningsName, string.Join(", ", pkColumns) + ", [MESSAGE] VARCHAR(255) COLLATE database_default");

            var con = storeConnection as SqlConnection;
            if (con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }

            //Update Temp Table by FKs
            var mergeTmpCommand = "";
            List<string> removedColumns = new List<string>();

            foreach (var fkCfg in fkCfgs)
            {
                if (fkCfg.ReplaceColumnsMap.Count > 0)
                    removedColumns.AddRange(fkCfg.RelationColumnsMap.Keys);
                var fks = fkCfg.RelationColumnsMap.Select(x => "(TEMP.[" + x.Key + "] IS NOT NULL and FK.[" + x.Value + "] IS NULL)").ToArray();
                var FKwhereClause = string.Join(" and ", fks);
                var fkSetters = string.Join(",", fkCfg.ReplaceColumnsMap.Select(c => "[" + c.Key + "] = CASE WHEN " + FKwhereClause + " THEN TEMP.[" + c.Key + "] ELSE FK.[" + c.Value + "] END").Union(new string[] { "[" + controlColumns[fkCfg.Table] + "] = CASE WHEN " + FKwhereClause + " THEN 0 ELSE 1 END" }));
                var fkRelations = fkCfg.RelationColumnsMap.Select(x => "TEMP.[" + x.Key + "] = FK.[" + x.Value + "]").ToArray();
                var fkFilter = string.Join(" and ", fkRelations);

                mergeTmpCommand += (mergeTmpCommand == "" ? "/* Update Temporary Table by FKs */" : newLine + newLine) + string.Format(@"
 UPDATE TEMP
 SET
     {3}
 FROM
     {4}.[{0}] TEMP
 LEFT JOIN
      {5}.[{1}] FK
 ON 
      {2}" + this.GetDeletedCondition(deletedProperty, "TEMP", false, "WHERE"), tempTableName, fkCfg.Table, fkFilter, fkSetters, schema, fkCfg.Schema);
            }

            //Select FK Warnings
            var warningsCommand = "";
            foreach (var fkCfg in fkCfgs)
            {
                var fkSelect = string.Join(",", primaryKeys.Select(c => "TEMP.[" + c.NameInDatabase + "]"));
                var fkInsert = string.Join(",", primaryKeys.Select(c => "[" + c.NameInDatabase + "]")) + ", [MESSAGE]"; ;
                var fkWarningRelationValues = string.Join(", ", fkCfg.RelationColumnsMap.Select(x => "[" + x.Key + "] = [" + x.Value + "]"));
                var FKwhereClause = "TEMP.[" + controlColumns[fkCfg.Table] + "] = 0" + this.GetDeletedCondition(deletedProperty, "TEMP", false, "AND");
                warningsCommand += (warningsCommand == "" ? "/* Generate FK warnings and delete them from the result */" : newLine + newLine) + string.Format(@"
 INSERT INTO {5}.[{3}]({4})
 SELECT {1}, MESSAGE = '" + fkCfg.Table + @"(" + fkWarningRelationValues + @") does not exist! '
 FROM
     {5}.[{0}] TEMP               
 WHERE {2}", tempTableName, fkSelect, FKwhereClause, tableWarningsName, fkInsert, schema)
 + newLine + newLine +
 string.Format(@"
 DELETE TEMP
 FROM
     {2}.[{0}] TEMP                
 WHERE {1}", tempTableName, FKwhereClause, schema);
            }

            //Concatenate temporary sequence
            var temporarySequenceCommand = mergeTmpCommand + newLine + newLine + warningsCommand;

            //Remove deleted property from columns collection
            if (deletedProperty != null)
            {
                removedColumns.Add(deletedProperty.NameInDatabase);
            }

            //Preparing DELETE and UPDATE command
            var setters = string.Join(",", filtered.Where(c => !c.IsPrimaryKey && !c.IsBusinessKey && !removedColumns.Contains(c.NameInDatabase)).Select(c => "[" + c.NameInDatabase + "] = TEMP.[" + c.NameInDatabase + "]"));
            var pks = primaryKeys.Select(x => "ORIG.[" + x.NameInDatabase + "] = TEMP.[" + x.NameInDatabase + "]").ToArray();
            var filter = string.Join(" and ", pks);


            string deleteCommand = "";
            //Delete command
            if (deletedProperty != null)
            {
                string deleteWhere = this.GetDeletedCondition(deletedProperty, "TEMP", true, "WHERE");
                deleteCommand = newLine + newLine + "/* Delete target table */";
                if (scriptEvent != null && !String.IsNullOrEmpty(scriptEvent.BeforeDelete))
                    deleteCommand += newLine + scriptEvent.BeforeDelete;

                deleteCommand += string.Format(@"
DELETE ORIG
FROM
    {3}.[{0}] ORIG
INNER JOIN
    {3}.[{1}] TEMP
ON 
    {2}
" + deleteWhere, tableName, tempTableName, filter, schema);

                if (scriptEvent != null && !String.IsNullOrEmpty(scriptEvent.AfterDelete))
                    deleteCommand += newLine + scriptEvent.AfterDelete;

                deleteCommand += string.Format(@"
DELETE TEMP
FROM {1}.[{0}] TEMP
" + deleteWhere, tempTableName, schema);

            }

            var mergeCommand = newLine + newLine + "/* Update target table */";
            if (scriptEvent != null && !String.IsNullOrEmpty(scriptEvent.BeforeUpdate))
                mergeCommand += newLine + scriptEvent.BeforeUpdate;

            mergeCommand += string.Format(@"
UPDATE ORIG
SET
    {3}
FROM
    {4}.[{0}] ORIG
INNER JOIN
    {4}.[{1}] TEMP
ON 
    {2}", tableName, tempTableName, filter, setters, schema);

            if (scriptEvent != null && !String.IsNullOrEmpty(scriptEvent.AfterUpdate))
                mergeCommand += newLine + scriptEvent.AfterUpdate;


            //Insert command
            var insertCommand = newLine + newLine + "/* Insert into target table */";
            if (scriptEvent != null && !String.IsNullOrEmpty(scriptEvent.BeforeInsert))
                insertCommand += newLine + scriptEvent.BeforeInsert;
            pks = primaryKeys.Select(x => "ORIG.[" + x.NameInDatabase + "] IS NULL").ToArray();
            var whereClause = string.Join(" and ", pks);
            var insertFields = string.Join(",", filtered.Where(c => !removedColumns.Contains(c.NameInDatabase) && !c.IsStoreGeneratedIdentity).Select(c => "[" + c.NameInDatabase + "]"));
            var selectFields = string.Join(",", filtered.Where(c => !removedColumns.Contains(c.NameInDatabase) && !c.IsStoreGeneratedIdentity).Select(c => "TEMP.[" + c.NameInDatabase + "]"));
            insertCommand += string.Format(@"
 INSERT INTO {6}.[{0}]
    ({4})
 SELECT {3}
 FROM
     {6}.[{1}] TEMP
 LEFT JOIN
      {6}.[{0}] ORIG
 ON 
      {2}
 WHERE {5}", tableName, tempTableName, filter, selectFields, insertFields, whereClause, schema);


            if (scriptEvent != null && !String.IsNullOrEmpty(scriptEvent.AfterInsert))
                insertCommand += newLine + scriptEvent.AfterInsert;

            //Exceptions
            Exception createError = null;
            Exception processError = null;
            Exception dropError = null;

            try
            {
                //Create temporary tables            
                using (var createCommand = new SqlCommand(str, con))
                {
                    createCommand.ExecuteNonQuery();
                }
            }
            catch (Exception excep)
            {
                createError = excep;
            }

            if (createError == null)
            {

                try
                {
                    using (var sCommand = new SqlCommand(temporarySequenceCommand, con))
                    using (var mCommand = new SqlCommand(mergeCommand, con))
                    using (var iCommand = new SqlCommand(insertCommand, con))
                    using (var wSelectCommand = new SqlCommand(string.Format("/* Return Warnings */\r\nSELECT * FROM {0}.[{1}]", schema, tableWarningsName), con))
                    {

                        InsertItems(items, schema, tempTableName, filtered, storeConnection, batchSize);

                        //Executing process
                        sCommand.ExecuteNonQuery();

                        if (!String.IsNullOrWhiteSpace(deleteCommand))
                        {
                            using (var dCommand = new SqlCommand(deleteCommand, con))
                            {
                                dCommand.ExecuteNonQuery();
                            }
                        }

                        mCommand.ExecuteNonQuery();
                        iCommand.ExecuteNonQuery();

                        using (SqlDataReader reader = wSelectCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string predicate = "";
                                List<object> parameters = new List<object>();
                                int idxParam = 0;
                                foreach (var property in primaryKeys)
                                {
                                    predicate += (predicate == "" ? "" : " and ") + "it." + property.NameOnObject + (property.DataType.ToLower().Contains("char") ? ".Trim()" : "") + " == @" + idxParam;
                                    var value = reader[property.NameInDatabase];
                                    if (property.DataType.ToLower().Contains("char") && value != null)
                                        value = value.ToString().Trim();
                                    parameters.Add(value);
                                    idxParam++;
                                }

                                //Add warnings to result
                                T item = items.Where(predicate, parameters.ToArray()).FirstOrDefault();
                                if (item != null)
                                {
                                    var resItem = result.FirstOrDefault(r => r.Element.Equals(item));
                                    if (resItem == null)
                                    {
                                        resItem = new WarningResult<T>() { Element = item, Message = "" };
                                        result.Add(resItem);
                                    }
                                    resItem.Message += (resItem.Message == "" ? "" : ", ") + reader["MESSAGE"].ToString();
                                }

                            }
                        }
                    }
                }
                catch (Exception excep)
                {
                    processError = excep;
                }
                
                try
                {
                    //Drop temporary tables
                    var strDrop = string.Format("DROP table {0}.[{1}]", schema, tempTableName) + newLine +
                                  string.Format("DROP table {0}.[{1}]", schema, tableWarningsName);
                    using (var dCommand = new SqlCommand(strDrop, con))
                    {
                        dCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception excep)
                {
                    dropError = excep;
                }
            }

            if (createError != null)
            {
                result.Clear();
                result.Add(new WarningResult<T>() { Message = createError.GetCompleteMessage() });
            }
            else if (processError != null)
            {
                result.Clear();
                result.Add(new WarningResult<T>() { Message = processError.GetCompleteMessage() });
            }
            else if (dropError != null)
            {
                result.Clear();
                result.Add(new WarningResult<T>() { Message = dropError.GetCompleteMessage() });
            }

            return result;
        }


        public bool CanHandle(System.Data.Common.DbConnection storeConnection)
        {
            return storeConnection is SqlConnection;
        }


        public QueryInformation GetQueryInformation<T>(System.Data.Entity.Core.Objects.ObjectQuery<T> query)
        {
            var fromRegex = new Regex(@"FROM \[([^\]]+)\]\.\[([^\]]+)\] AS (\[[^\]]+\])", RegexOptions.IgnoreCase);

            var queryInfo = new QueryInformation();

            var str = query.ToTraceString();
            var match = fromRegex.Match(str);
            queryInfo.Schema = match.Groups[1].Value;
            queryInfo.Table = match.Groups[2].Value;
            queryInfo.Alias = match.Groups[3].Value;

            var i = str.IndexOf("WHERE");
            if (i > 0)
            {
                var whereClause = str.Substring(i);
                queryInfo.WhereSql = whereClause.Replace(queryInfo.Alias + ".", "");
            }
            return queryInfo;
        }

    }
}

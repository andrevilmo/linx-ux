using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace EntityFramework.Utilities
{
    public interface IQueryProvider
    {
        bool CanDelete { get; }
        bool CanUpdate { get; }
        bool CanInsert { get; }
        bool CanBulkUpdate { get; }

        string GetDeleteQuery(QueryInformation queryInformation);
        string GetUpdateQuery(QueryInformation predicateQueryInfo, QueryInformation modificationQueryInfo);
        void InsertItems<T>(IEnumerable<T> items, string schema, string tableName, IList<ColumnMapping> properties, DbConnection storeConnection, int? batchSize);
        List<WarningResult<T>> UpdateItems<T>(IEnumerable<T> items, string schema, string tableName, ScriptEvent scriptEvent, string deletedPropertyName, IList<ColumnMapping> properties, DbConnection storeConnection, int? batchSize, UpdateSpecification<T> updateSpecification, List<ForeignKeyCfg> fkCfg);

        bool CanHandle(DbConnection storeConnection);


        QueryInformation GetQueryInformation<T>(System.Data.Entity.Core.Objects.ObjectQuery<T> query);


    }

    public class ForeignKeyCfg
    {
        public ForeignKeyCfg() : this("DBO", "")
        {

        }

        public ForeignKeyCfg(string table) : this("DBO", table)
        {
        }

        public ForeignKeyCfg(string schema, string table)
        {
            this.Schema = schema;
            this.Table = table;
        }

        public string Schema { get; set; }
        public string Table { get; set; }
        private Dictionary<string, string> _relationColumnsMap;
        public Dictionary<string, string> RelationColumnsMap
        {
            get
            {
                if (_relationColumnsMap == null)
                    _relationColumnsMap = new Dictionary<string, string>();

                return _relationColumnsMap;
            }
        }
        private Dictionary<string, string> _replaceColumnsMap;
        public Dictionary<string, string> ReplaceColumnsMap
        {
            get
            {
                if (_replaceColumnsMap == null)
                    _replaceColumnsMap = new Dictionary<string, string>();

                return _replaceColumnsMap;
            }
        }
    }

    public class WarningResult<T>
    {
        public T Element { get; set; }
        public string Message { get; set; }
    }

    public class ScriptEvent
    {
        public string BeforeUpdate { get; set; }
        public string BeforeInsert { get; set; }
        public string BeforeDelete { get; set; }
        public string AfterUpdate { get; set; }
        public string AfterInsert { get; set; }
        public string AfterDelete { get; set; }
    }

}

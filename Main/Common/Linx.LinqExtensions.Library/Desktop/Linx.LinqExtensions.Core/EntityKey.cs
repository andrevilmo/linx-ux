using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Linx.LinqExtensions
{
    public class EntityKey
    {
        public EntityKey(string entityName, List<EntityKeyMember> entityKeyValues)
        {
            this._entityName = entityName;
            this._entityKeyValues = entityKeyValues;
        }

        private string _entityName;
        public string EntityName { get { return _entityName; } }

        private List<EntityKeyMember> _entityKeyValues;
        public List<EntityKeyMember> EntityKeyValues { get { return _entityKeyValues; } }
    }

}

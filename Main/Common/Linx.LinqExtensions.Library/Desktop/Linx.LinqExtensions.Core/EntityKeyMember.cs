using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Linx.LinqExtensions
{
    [DataContract]
    public class EntityKeyMember
    {
        public EntityKeyMember() { }
        public EntityKeyMember(string keyName, object keyValue)
        {
            this.Key = keyName;
            this.Value = keyValue;
        }

        [DataMember]
        public string Key { get; set; }
        [DataMember]
        public object Value { get; set; }

        public override string ToString()
        {
            return Key + ": " + Value;
        }
    }
}

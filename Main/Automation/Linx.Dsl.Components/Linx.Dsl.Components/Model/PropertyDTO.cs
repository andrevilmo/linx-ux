using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Dsl.Components
{
    [Serializable]
    [DataContract]
    public class PropertyDTO
    {
        [DataMember]
        public Guid UIDRef { get; set; }

        [DataMember]
        public Linx.Dsl.Components.Enums.LibEnum Lib { get; set; }

        [DataMember]
        public string Key { get; set; }

        [DataMember]
        public string Value { get; set; }
    }
}

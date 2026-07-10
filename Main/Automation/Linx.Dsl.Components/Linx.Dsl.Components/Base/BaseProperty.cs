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
    public class PropertyBase
    {
        [DataMember(Order = 0)]
        public Guid UID { get; set; }

        [DataMember(Order = 999)]
        public List<PropertyDTO> Properties { get; set; }

        public StringBuilder GenerateDynamicProperties(Linx.Dsl.Components.Enums.LibEnum lib)
        {
            StringBuilder r = new StringBuilder();
            foreach (var p in this.Properties.Where(w => w.Lib == lib || w.UIDRef == this.UID))
            {
                r.AppendFormat("\t{0}: \"{1}\",", p.Key, p.Value);
                r.AppendLine();
            }

            if (r.Length > 1)
            {
                r.Insert(0, ",");
                return r.Remove(r.Length - 3, 3);
            }

            return r;
        }
    }
}

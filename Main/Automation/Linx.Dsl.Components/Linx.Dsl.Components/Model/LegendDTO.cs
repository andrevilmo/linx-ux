using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Dsl.Components
{
    [DataContract]
    [Serializable]
    public class LegendDTO : PropertyBase
    {
        public LegendDTO()
        {
            this.UID = Guid.NewGuid();
            this.Enabled = false;
            this.Element = string.Empty;
            this.Height = string.Empty;
            this.Width = string.Empty;
            this.Type = string.Empty;
        }

        [DataMember(Order = 1)]
        public bool Enabled { get; set; }

        [DataMember(Order = 2)]
        public string Element { get; set; }

        [DataMember(Order = 3)]
        public string Height { get; set; }

        [DataMember(Order = 4)]
        public string Width { get; set; }

        [DataMember(Order = 5)]
        public string Type { get; set; }

    }
}

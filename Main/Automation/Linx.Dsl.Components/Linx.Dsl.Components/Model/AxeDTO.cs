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
    public class AxeDTO : PropertyBase
    {
        public AxeDTO()
        {
            this.UID = Guid.NewGuid();
            this.Name = string.Empty;
            this.Type = string.Empty;
            this.Path = string.Empty;
            this.Title = string.Empty;
            this.Properties = new List<PropertyDTO>();
            this.Label = string.Empty;
            this.CrossHair = false;
            this.GroupAxe = false;

        }

        [DataMember(Order = 1)]
        public string Name { get; set; }

        [DataMember(Order = 2)]
        public string Type { get; set; }

        [DataMember(Order = 3)]
        public string Path { get; set; }

        [DataMember(Order = 4)]
        public string Title { get; set; }

        [DataMember(Order = 5)]
        public String Label { get; set; }

        [DataMember(Order = 6)]
        public bool CrossHair { get; set; }

        [DataMember(Order = 7)]
        public bool GroupAxe { get; set; }

    }
}

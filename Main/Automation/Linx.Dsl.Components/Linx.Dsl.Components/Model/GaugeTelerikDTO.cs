using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Linx.Dsl.Components
{   
    [DataContract]
    [Serializable]
    public class GaugeTelerikDTO : PropertyBase
    {

        public GaugeTelerikDTO()
        {
            this.UID = Guid.NewGuid();
            this.GaugeType = string.Empty;
            this.ValueField = string.Empty;
            this.Position = string.Empty;
            this.FormatLabel = string.Empty;
            this.Min = 0;
            this.Max = 0;
            this.Ranges = string.Empty;
            this.StartAngle = string.Empty;
            this.EndAngle = string.Empty;
        }

        [DataMember(Order = 1)]
        public string GaugeType { get; set; }

        [DataMember(Order = 2)]
        public string ValueField { get; set; }

        [DataMember(Order = 3)]
        public string Position { get; set; }

        [DataMember(Order = 4)]
        public string FormatLabel { get; set; }

        [DataMember(Order = 5)]
        public int Min { get; set; }

        [DataMember(Order = 6)]
        public int Max { get; set; }

        [DataMember(Order = 7)]
        public string Ranges { get; set; }

        [DataMember(Order= 8)]
        public string StartAngle { get; set; }

        [DataMember(Order = 9)]
        public string EndAngle { get; set; }


    }
}

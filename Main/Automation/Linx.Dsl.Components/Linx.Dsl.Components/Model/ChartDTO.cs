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
    public class ChartDTO : PropertyBase
    {
        public ChartDTO()
        {
            this.UID = Guid.NewGuid();
            this.ChartType = string.Empty;
            this.Title = string.Empty;
            this.SubTitle = string.Empty;
            this.Width = string.Empty;
            this.Height = string.Empty;
            this.SubTitle = string.Empty;
            this.Axes = new List<AxeDTO>();
            this.Series = new List<SerieDTO>();
            this.Properties = new List<PropertyDTO>();
            this.ChartGroup = string.Empty;
            this.ValueMemberPath = string.Empty;
            this.LabelMemberPath = string.Empty;
            this.LabelsPosition = string.Empty;
            this.Legend = new LegendDTO();
            
        }

        [DataMember(Order = 1)]
        public string ChartType { get; set; }

        [DataMember(Order = 2)]
        public string Title { get; set; }

        [DataMember(Order = 3)]
        public string SubTitle { get; set; }

        [DataMember(Order = 4)]
        public string Width { get; set; }

        [DataMember(Order = 5)]
        public string Height { get; set; }

        [DataMember(Order = 6)]
        public List<AxeDTO> Axes { get; set; }

        [DataMember(Order = 7)]
        public List<SerieDTO> Series { get; set; }

        [DataMember(Order = 8)]
        public String ChartGroup { get; set; }

        [DataMember(Order = 9)]
        public string ValueMemberPath { get; set; }

        [DataMember(Order = 10)]
        public string LabelMemberPath { get; set; }

        [DataMember(Order = 11)]
        public string LabelsPosition { get; set; }

        [DataMember(Order = 12)]
        public LegendDTO Legend { get; set; }

        
    }
}

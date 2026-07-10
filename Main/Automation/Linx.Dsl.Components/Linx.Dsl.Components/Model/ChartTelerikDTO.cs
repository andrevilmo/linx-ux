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
    public class ChartTelerikDTO : PropertyBase
    {

        public ChartTelerikDTO()
        {
            this.UID = Guid.NewGuid();
            this.ChartType = string.Empty;
            this.Title = string.Empty;
            this.LegendPosition = string.Empty;
            this.MultiAxis = false;
            this.Axes = new List<AxeDTO>();
            this.Series = new List<SerieDTO>();
            this.Properties = new List<PropertyDTO>();
            this.ChartGroup = string.Empty;
            this.ValueMemberPath = string.Empty;
            this.LabelMemberPath = string.Empty;
            this.Category = string.Empty;
            this.Width = 0;
            this.Height = 0;
            this.FormatSeriePie = string.Empty;
            this.RotacaoAxe = string.Empty;
            this.RotacaoSerie = string.Empty;
            this.SortField = string.Empty;
            this.SortDir = string.Empty;

        }

        [DataMember(Order = 1)]
        public string ChartType { get; set; }

        [DataMember(Order = 2)]
        public string Title { get; set; }

        [DataMember(Order = 3)]
        public string LegendPosition { get; set; }

        [DataMember(Order = 4)]
        public bool MultiAxis { get; set; }

        [DataMember(Order = 5)]
        public List<AxeDTO> Axes { get; set; }

        [DataMember(Order = 6)]
        public List<SerieDTO> Series { get; set; }

        [DataMember(Order = 7)]
        public String ChartGroup { get; set; }

        [DataMember(Order = 8)]
        public string ValueMemberPath { get; set; }

        [DataMember(Order = 9)]
        public string LabelMemberPath { get; set; }

        [DataMember(Order = 10)]
        public string Category { get; set; }

        [DataMember(Order = 11)]
        public int Width { get; set; }

        [DataMember(Order = 12)]
        public int Height { get; set; }

        [DataMember(Order = 13)]
        public string FormatSeriePie { get; set; }

        [DataMember(Order=14)]
        public string RotacaoAxe { get; set; }

        [DataMember(Order = 15)]
        public string RotacaoSerie { get; set; }

        [DataMember(Order= 16)]
        public string SortField { get; set; }
        
        [DataMember(Order = 17)]
        public string SortDir { get; set; }

    }
}

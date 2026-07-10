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
    public class SerieDTO : PropertyBase
    {
        public SerieDTO()
        {
            this.UID = Guid.NewGuid();
            this.Name = string.Empty;
            this.Type = string.Empty;
            this.Title = string.Empty;
            this.xAxis = string.Empty;
            this.yAxis = string.Empty;
            this.ValueMemberPath = string.Empty;
            this.Tooltip = false;
            this.Properties = new List<PropertyDTO>();
            this.LowMemberPath = string.Empty;
            this.HighMemberPath = string.Empty;
            this.LabelPath = string.Empty;
            this.xMemberPath = string.Empty;
            this.yMemberPath = string.Empty;

            this.Format = string.Empty;
            this.EnableMultiType = false;
            this.MultiType = string.Empty;

            this.RadiusMemberPath = string.Empty;
            this.FillMemberPath = string.Empty;
            this.LabelMemberPath = string.Empty;
            this.MarkerType = string.Empty;
            this.RadiusScale_MinimumValue = 0;
            this.RadiusScale_MaximumValue = 0;
            this.RadiusScale_IsLogarithmic = false;
            this.FillScale_Type = string.Empty;
            this.FillScale_Brushes = string.Empty;
            this.FillScale_MinimumValue = 0;
            this.FillScale_MaximumValue = 0;

            this.OpenMemberPath = string.Empty;
            this.CloseMemberPath = string.Empty;
            this.DisplayType = string.Empty;

            this.AngleAxis = string.Empty;
            this.RadiusAxis = string.Empty;
            this.AngleMemberPath = string.Empty;
            this.ValueAxis = string.Empty;

        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string xAxis { get; set; }

        [DataMember]
        public string yAxis { get; set; }

        [DataMember]
        public string ValueMemberPath { get; set; }

        [DataMember]
        public string LabelPath { get; set; }

        [DataMember]
        public bool Tooltip { get; set; }

        [DataMember]
        public string LowMemberPath { get; set; }

        [DataMember]
        public string HighMemberPath { get; set; }
        
        [DataMember]
        public string xMemberPath { get; set; }
        
        [DataMember]
        public string yMemberPath { get; set; }

        [DataMember]
        public string Format { get; set; }

        [DataMember]
        public string RadiusMemberPath { get; set; }

        [DataMember]
        public string FillMemberPath { get; set; }

        [DataMember]
        public string LabelMemberPath { get; set; }

        [DataMember]
        public string MarkerType { get; set; }

        [DataMember]
        public int RadiusScale_MinimumValue { get; set; }

        [DataMember]
        public int RadiusScale_MaximumValue { get; set; }

        [DataMember]
        public bool RadiusScale_IsLogarithmic { get; set; }

        [DataMember]
        public string FillScale_Type { get; set; }

        [DataMember]
        public string FillScale_Brushes { get; set; }

        [DataMember]
        public int FillScale_MinimumValue { get; set; }

        [DataMember]
        public int FillScale_MaximumValue { get; set; }

        [DataMember]
        public string OpenMemberPath { get; set; }

        [DataMember]
        public string CloseMemberPath { get; set; }

        [DataMember]
        public string DisplayType { get; set; }

        [DataMember]
        public string AngleAxis { get; set; }

        [DataMember]
        public string RadiusAxis { get; set; }

        [DataMember]
        public string AngleMemberPath { get; set; }

        [DataMember]
        public string ValueAxis { get; set; }

        [DataMember]
        public bool EnableMultiType { get; set; }

        [DataMember]
        public string MultiType { get; set; }
 
    }
}

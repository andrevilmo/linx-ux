using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Linx.OlapProxy.Service.Models
{
    public class CubeMetadatainfo
    {
        public string Cube { get; set; }

        public List<MeasureInfo> MeasuresInfo { get; set; }

        public List<DimensionInfo> DimensionsInfo { get; set; }

        public CubeMetadatainfo()
            : this("MODEL") { }

        public CubeMetadatainfo(string cube)
        {
            this.Cube = cube;

            this.MeasuresInfo = new List<MeasureInfo>();
            this.DimensionsInfo = new List<DimensionInfo>();
        }
    }
}
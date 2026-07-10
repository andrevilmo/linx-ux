using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Linx.OlapProxy.Service.Models
{
    public class DimensionInfo
    {
        public string DimensionName { get; set; }
        public List<FieldDimension> Fields { get; set; }

        public DimensionInfo(string dimensionName)
        {
            this.DimensionName = dimensionName;
            this.Fields = new List<FieldDimension>();
        }
    }
}
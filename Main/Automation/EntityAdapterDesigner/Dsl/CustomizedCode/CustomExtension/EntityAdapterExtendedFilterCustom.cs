using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DslModeling = global::Microsoft.VisualStudio.Modeling;

namespace Linx.EntityAdapterDesigner
{    
    public partial class EntityAdapterExtendedFilter : DslModeling::ModelElement
    {
        public string GetDisplayInfoValue()
        {
            return (this.IsUsedInTheLinq ? ((char)8730).ToString() : " ") + " " + this.DisplayName;
        }
    }

    public class CommonExtendedFilterProperty
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string DataType { get; set; }
        public string EdmKey { get; set; }
    }
}

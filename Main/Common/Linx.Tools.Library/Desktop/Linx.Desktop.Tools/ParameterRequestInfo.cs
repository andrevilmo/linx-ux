using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Linx.Tools
{    
    public class ParameterRequestInfo
    {
        public string Title { get; set; }
        public Dictionary<string, string> VariationValues { get; set; }
    }

    public class ParameterRequestValue
    {
        public string Title { get; set; }
        public string Value { get; set; }
        public Type DataType { get; set; }
    }
}

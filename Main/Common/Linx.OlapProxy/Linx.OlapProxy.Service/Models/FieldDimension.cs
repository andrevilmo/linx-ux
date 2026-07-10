using Linx.OlapProxy.Service.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Linx.OlapProxy.Service.Models
{
    public class FieldDimension
    {
        public string Name { get; set; }
        public ParameterType KeyType { get; set; }
        public string HierarchyName { get; set; }
    }
}
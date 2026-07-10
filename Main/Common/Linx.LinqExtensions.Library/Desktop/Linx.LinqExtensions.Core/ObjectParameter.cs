using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace System.Data.Entity.Core.Objects
{
    public class ObjectParameter
    {

        public ObjectParameter(string name, object value)
        {
            this.Name = name;
            this.Value = value;
        }


        public string Name { get; set; }
        public object Value { get; set; }
    }
}

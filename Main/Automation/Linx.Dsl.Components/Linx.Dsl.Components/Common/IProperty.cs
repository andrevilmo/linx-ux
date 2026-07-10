using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Dsl.Components.Common
{
    public interface IProperty
    {
        List<PropertyDTO> Properties { get; set; }
    }
}

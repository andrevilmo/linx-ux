using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Linx.Tools
{
    //
    // Summary:
    //     Attribute applied to an association member to indicate that the association is
    //     a compositional relationship.
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public sealed class CompositionAttribute : Attribute
    {
        
    }

}

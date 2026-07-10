using System;

namespace Linx.DS.Core.Data
{
    /// <summary>
    /// Attribute applied to a <see cref="DomainService"/> method to indicate that it is an insert method.
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Field | AttributeTargets.Method | AttributeTargets.Property,
        AllowMultiple = false, Inherited = true)]
    public sealed class InsertAttribute : Attribute
    {
    }
}

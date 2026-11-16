using System;

namespace Linx.Framework.BV.WebAPI.DS.Filters
{
    /// <summary>
    /// Opt-out marker: skips <see cref="BlockDangerousInputAttribute"/> for the action/controller.
    /// Use only for endpoints that must accept opaque/encrypted payloads.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class AllowDangerousInputAttribute : Attribute
    {
    }
}

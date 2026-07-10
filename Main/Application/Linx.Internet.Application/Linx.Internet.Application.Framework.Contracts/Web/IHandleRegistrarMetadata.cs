// -----------------------------------------------------------------------
// <copyright file="IRouteRegistrarMetadata.cs" company="Linx Sistemas">
// Copyright (c) Linx Sistemas. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace Linx.Internet.Application.Framework.Web
{
    /// <summary>
    /// Defines the contract for providing metadata for a route registrar.
    /// </summary>
    public interface IHandleRegistrarMetadata
    {
        #region Properties

        /// <summary>
        /// Gets the module name in which the registrar must be processed.
        /// </summary>
        string ExternalName { get; }

        #endregion
    }
}

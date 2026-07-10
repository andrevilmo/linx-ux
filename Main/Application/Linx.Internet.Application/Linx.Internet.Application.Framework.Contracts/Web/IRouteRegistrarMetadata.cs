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
    public interface IRouteRegistrarMetadata
    {
        #region Properties
        /// <summary>
        /// Gets the order in which the registrar must be processed.
        /// </summary>
        int Order { get; }

        /// <summary>
        /// Gets the module name in which the registrar must be processed.
        /// </summary>
        string ModuleName { get; }

        /// <summary>
        /// Gets the module id in which the registrar must be processed.
        /// </summary>
        string ModuleId { get; }

        /// <summary>
        /// Gets the module id in which the registrar must be processed.
        /// </summary>
        string ShellVersion { get; }
        #endregion
    }
}

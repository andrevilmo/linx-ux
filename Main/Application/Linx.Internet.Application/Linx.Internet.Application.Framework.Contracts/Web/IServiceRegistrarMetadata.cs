// -----------------------------------------------------------------------
// <copyright file="IServiceRegistrarMetadata.cs" company="Linx Sistemas">
// Copyright (c) Linx Sistemas. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace Linx.Internet.Application.Framework.Web
{
    /// <summary>
    /// Defines the contract for providing metadata for a route registrar.
    /// </summary>
    public interface IServiceRegistrarMetadata
    {
        #region Properties
        /// <summary>
        /// Gets the order in which the registrar must be processed.
        /// </summary>
        int Order { get; }
        #endregion
    }
}

// -----------------------------------------------------------------------
// <copyright file="IRouteRegistrar.cs" company="Linx Sistemas">
// Copyright (c) Linx Sistemas. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace Linx.Internet.Application.Framework.Web
{
    using System.Collections.Generic;
    using System.Web.Routing;
    using Linx.Internet.Application.Framework.Classes;

    /// <summary>
    /// Defines the contract for implementing a registrar to publish routes.
    /// </summary>
    public interface IRouteRegistrar
    {
        #region Methods
        ///// <summary>
        ///// Registers any routes to be ignored by the routing system.
        ///// </summary>
        ///// <param name="routes">The collection of routes to add to.</param>
        ////void RegisterIgnoreRoutes(RouteCollection routes);

        ///// <summary>
        ///// Registers any routes to be used by the routing system.
        ///// </summary>
        ///// <param name="routes">The collection of routes to add to.</param>
        ////void RegisterRoutes(RouteCollection routes);

        /// <summary>
        /// Atributo Tipo Dicionario contendo parametros: String e EmbeddedFile
        /// </summary>
        /// <param name="moduleName">The collection of routes to add to.</param>
        /// <returns>Retorna um Dicionario contendo parametros: String e EmbeddedFile</returns>
        Dictionary<string, EmbeddedFile> LoadEmbeddedResources(string moduleName);
        #endregion
    }
}

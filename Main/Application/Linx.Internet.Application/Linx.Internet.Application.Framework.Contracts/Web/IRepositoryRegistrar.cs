// -----------------------------------------------------------------------
// <copyright file="IRepositoryRegistrar.cs" company="Linx Sistemas">
// Copyright (c) Linx Sistemas. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace Linx.Internet.Application.Framework.Web
{
    using System.Collections.Generic;
    using System.Text;
    using System.Web.Routing;

    /// <summary>
    /// Defines the contract for implementing a registrar to publish routes.
    /// </summary>
    public interface IRepositoryRegistrar
    {
        #region Methods
        /// <summary>
        /// Atributo de Classe Dicionario de strings
        /// </summary>
        /// <returns>Retona um Dicionario de Strings</returns>
        Dictionary<string, string> LoadRepository();
        #endregion
    }
}

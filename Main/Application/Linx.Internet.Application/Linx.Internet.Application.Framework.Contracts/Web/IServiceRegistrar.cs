// -----------------------------------------------------------------------
// <copyright file="IServiceRegistrar.cs" company="Linx Sistemas">
// Copyright (c) Linx Sistemas. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace Linx.Internet.Application.Framework.Web
{
    using System.Collections.Generic;
    using System.Web.Routing;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IServiceRegistrar
    {
        #region Methods
        /// <summary>
        /// Atributo de classe
        /// </summary>
        void RegisterType();
        #endregion
    }
}

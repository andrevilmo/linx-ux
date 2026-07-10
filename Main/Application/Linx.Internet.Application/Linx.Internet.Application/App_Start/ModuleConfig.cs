// -----------------------------------------------------------------------
// <copyright file="RouteConfig.cs" company="Linx Sistemas">
// Copyright (c) Linx Sistemas. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace Linx.Internet.Application.App_Start
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using Linx.Internet.Application.Framework.Web;
    using Linx.Internet.Application.Framework.Classes;

    /// <summary>
    /// Class RouteConfig
    /// </summary>
    [Export(typeof(IRouteRegistrar)),
    ExportMetadata("Order", -1),
    ExportMetadata("ModuleName", "shell"),
    ExportMetadata("ModuleId", "9D90FBBC-F519-473A-999B-565082D7D276"),
    ExportMetadata("ShellVersion", "0.0.0.0")]
    public class ModuleConfig : IRouteRegistrar
    {
        /// <summary>
        /// Metodo LoadEmbeddedResources
        /// </summary>
        /// <returns>Retorna um objeto tipo Dictionary</returns>
        #region IRouteRegistrar Members
        Dictionary<string, EmbeddedFile> IRouteRegistrar.LoadEmbeddedResources(string moduleName)
        {
            return Linx.Internet.Application.Framework.Web.AssemblyResources.LoadEmbeddedResources(this.GetType(), moduleName);
        }
        #endregion
    }
}
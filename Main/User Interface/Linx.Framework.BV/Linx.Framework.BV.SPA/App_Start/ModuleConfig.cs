// <copyright file="RouteConfig.cs" company="Linx Sistemas">
// Copyright (c) Linx Sistemas. All rights reserved.
// </copyright>
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Linx.Internet.Application.Framework.Web;
using Linx.Internet.Application.Framework.Classes;

namespace Linx.Framework.BV.SPA.App_Start
{
    [Export(typeof(IRouteRegistrar)),
    ExportMetadata("Order", 1),
    ExportMetadata("ModuleName", "linx-framework-bv-spaservices"),
    ExportMetadata("ModuleId", "6c432bb9-1594-4f42-8cea-f4e3a4fd4bbe")]
    [ExportMetadata("ShellVersion", "6.0.0.32648")]
    public class ModuleConfig : IRouteRegistrar
    {
       Dictionary<string, EmbeddedFile> IRouteRegistrar.LoadEmbeddedResources(string moduleName)
       {
           return Linx.Internet.Application.Framework.Web.AssemblyResources.LoadEmbeddedResources(this.GetType(), moduleName);
       }
    }
}

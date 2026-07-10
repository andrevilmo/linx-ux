// <copyright file="RouteConfig.cs" company="Linx Sistemas">
// Copyright (c) Linx Sistemas. All rights reserved.
// </copyright>
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Linx.Internet.Application.Framework.Web;
using Linx.Internet.Application.Framework.Classes;

namespace Linx.Dashboard.SPA.App_Start
{
    [Export(typeof(IRouteRegistrar)),
    ExportMetadata("Order", 1),
    ExportMetadata("ModuleName", "linx-dashboard-spaservices"),
    ExportMetadata("ModuleId", "71649875-6286-43ae-b53e-908c3edb1770")]
    [ExportMetadata("ShellVersion", "5.2.0.32255")]
    public class ModuleConfig : IRouteRegistrar
    {
       Dictionary<string, EmbeddedFile> IRouteRegistrar.LoadEmbeddedResources(string moduleName)
       {
           return Linx.Internet.Application.Framework.Web.AssemblyResources.LoadEmbeddedResources(this.GetType(), moduleName);
       }
    }
}

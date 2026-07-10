// <copyright file="RouteConfig.cs" company="Linx Sistemas">
// Copyright (c) Linx Sistemas. All rights reserved.
// </copyright>
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Linx.Internet.Application.Framework.Web;
using Linx.Internet.Application.Framework.Classes;

namespace Linx.Demo.BV.SPA.App_Start
{
    [Export(typeof(IRouteRegistrar)),
    ExportMetadata("Order", 1),
    ExportMetadata("ModuleName", "linx-demo-bv-spaservices"),
    ExportMetadata("ModuleId", "86a16112-630a-44aa-88df-a91e6f370a93")]
    [ExportMetadata("ShellVersion", "6.0.0.24358")]
    public class ModuleConfig : IRouteRegistrar
    {
       Dictionary<string, EmbeddedFile> IRouteRegistrar.LoadEmbeddedResources(string moduleName)
       {
           return Linx.Internet.Application.Framework.Web.AssemblyResources.LoadEmbeddedResources(this.GetType(), moduleName);
       }
    }
}

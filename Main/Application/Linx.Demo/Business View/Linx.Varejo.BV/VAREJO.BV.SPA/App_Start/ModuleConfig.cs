// <copyright file="RouteConfig.cs" company="Linx Sistemas">
// Copyright (c) Linx Sistemas. All rights reserved.
// </copyright>
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Linx.Internet.Application.Framework.Web;
using Linx.Internet.Application.Framework.Classes;

namespace VAREJO.BV.SPA.App_Start
{
    [Export(typeof(IRouteRegistrar)),
    ExportMetadata("Order", 1),
    ExportMetadata("ModuleName", "varejo-bv-spaservices"),
    ExportMetadata("ModuleId", "d31e6c5e-d50d-454a-90dd-3122ea333046")]
    [ExportMetadata("ShellVersion", "5.2.0.20163")]
    public class ModuleConfig : IRouteRegistrar
    {
       Dictionary<string, EmbeddedFile> IRouteRegistrar.LoadEmbeddedResources(string moduleName)
       {
           return Linx.Internet.Application.Framework.Web.AssemblyResources.LoadEmbeddedResources(this.GetType(), moduleName);
       }
    }
}

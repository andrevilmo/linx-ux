// <copyright file="RouteConfig.cs" company="Linx Sistemas">
// Copyright (c) Linx Sistemas. All rights reserved.
// </copyright>
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Linx.Internet.Application.Framework.Web;
using Linx.Internet.Application.Framework.Classes;

namespace LinxTraining001.BV.SPA.App_Start
{
    [Export(typeof(IRouteRegistrar)),
    ExportMetadata("Order", 1),
    ExportMetadata("ModuleName", "linxtraining001-bv-spaservices"),
    ExportMetadata("ModuleId", "6f2ea1c1-7114-4f49-be64-437d9d77eac5")]
    [ExportMetadata("ShellVersion", "4.1.3.32602")]
    public class ModuleConfig : IRouteRegistrar
    {
       Dictionary<string, EmbeddedFile> IRouteRegistrar.LoadEmbeddedResources(string moduleName)
       {
           return Linx.Internet.Application.Framework.Web.AssemblyResources.LoadEmbeddedResources(this.GetType(), moduleName);
       }
    }
}

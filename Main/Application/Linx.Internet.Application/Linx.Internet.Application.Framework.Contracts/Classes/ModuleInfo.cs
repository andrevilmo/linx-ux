// -----------------------------------------------------------------------
// <copyright file="EmbeddedFile.cs" company="Linx Sistemas">
// Copyright (c) Linx Sistemas. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace Linx.Internet.Application.Framework.Classes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// classe usada para conter daados dos arquivos embedados
    /// </summary>
    public class ModuleInfo
    {
        public string ModuleUId { get; set; }

        public string ModuleName { get; set; }

        public int ModuleOrder { get; set; }

        public bool IsModuleShell { get; set; }

        public string AssemblyName { get; set; }

        public string AssemblyVersion { get; set; }

        public string AssemblyVersionURL { get; set; }

        public string AssemblyType { get; set; }

        public string ShellAssemblyVersion { get; set; }

        public string ModuleNamePath { get; set; }

        public string AssemblyVersionPath { get; set; }

        public DateTime BuildDate { get; set; }

        public string CRC32 { get; set; }
    }
}
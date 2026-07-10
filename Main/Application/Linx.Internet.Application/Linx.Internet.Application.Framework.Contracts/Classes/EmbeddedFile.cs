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
    public class EmbeddedFile
    {
        /// <summary>
        /// Gets or sets nome projeto a qual pertence
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// Gets or sets nome projeto a qual pertence
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// Gets or sets nome projeto a qual pertence
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets nome projeto a qual pertence
        /// </summary>
        public string FileNameFlat { get; set; }

        /// <summary>
        /// Gets or sets nome projeto a qual pertence
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// Gets or sets bytes 
        /// </summary>
        public byte[] Bytes { get; set; }

        /// <summary>
        /// Gets or sets nome projeto a qual pertence
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets nome projeto a qual pertence
        /// </summary>
        public string FullPathIO { get; set; }

        public string FullPathZip { get; set; }

        public string CRC32 { get; set; }

        public string RequireId { get; set; }

        public DateTime LastModified { get; set; }

    }
}
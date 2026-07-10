////// -----------------------------------------------------------------------
//// <copyright file="BaseHelpers.cs" company="Linx Sistemas">
//// Copyright (c) Linx Sistemas. All rights reserved.
//// </copyright>
////// -----------------------------------------------------------------------

//namespace Linx.Internet.Application.Common.Helpers
//{
//    using System;
//    using System.IO;
//    using System.Linq;
//    using System.Reflection;
//    using System.Text;
//    using System.Text.RegularExpressions;
//    using System.Web;

//    /// <summary>
//    /// Classe responsável por auxiliar projeto com metodos e propriedades
//    /// </summary>
//    public static class BaseHelpers
//    {
//        /// <summary>
//        /// string estatica Numero da Versao
//        /// </summary>
//        private static string numeroVersao;

//        /// <summary>
//        /// Gets or sets propriedade Numero da versão
//        /// </summary>
//        public static string NumeroVersao
//        {
//            get
//            {
//                if (numeroVersao == null)
//                {
//                    var assembly = System.Reflection.Assembly.GetCallingAssembly();

//                    AssemblyName assemblyName = assembly.GetName();
//#if DEBUG
//                    numeroVersao = string.Format("v{0}.{1}.{2}.{3}", assemblyName.Version.Major, assemblyName.Version.Minor, assemblyName.Version.Build, assemblyName.Version.Revision);
//#else
//                    numeroVersao = string.Format("v{0}.{1}.{2}.{3}", assemblyName.Version.Major, assemblyName.Version.Minor, assemblyName.Version.Build, assemblyName.Version.Revision);
//#endif
//                }

//                return numeroVersao;
//            }

//            set
//            {
//            }
//        }

//        /// <summary>
//        /// Gets or sets Retorna request QueryStringNoCache
//        /// </summary>
//        public static string QueryStringNoCache
//        {
//            get
//            {
//#if DEBUG
//                return string.Concat("version=", Guid.Empty.ToString().GetHashCode().ToString("x"));
//#else
//                if (queryStringNoCache == null)
//                {
//                    queryStringNoCache = NumeroVersao;
//                }

//                return string.Concat("version=", queryStringNoCache);

//#endif
//            }

//            set
//            {
//            }
//        }

//    }
//}

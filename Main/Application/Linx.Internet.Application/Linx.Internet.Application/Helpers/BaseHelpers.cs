//// -----------------------------------------------------------------------
// <copyright file="BaseHelpers.cs" company="Linx Sistemas">
// Copyright (c) Linx Sistemas. All rights reserved.
// </copyright>
//// -----------------------------------------------------------------------

namespace Linx.Internet.Application.Helpers
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;

    /// <summary>
    /// Classe responsável por auxiliar projeto com metodos e propriedades
    /// </summary>
    public static class BaseHelpers
    {
        /// <summary>
        /// string estatica Numero da Versao
        /// </summary>
        private static string numeroVersao;
        private static string numeroVersaoAssembly;
        private static string numeroVersaoAssemblyLabel;
        private static DateTime? dataVersao;

        /// <summary>
        /// string estatica Numero da Versao
        /// </summary>
        private static string numeroVersaoReduzida;

        /// <summary>
        /// string estatica Numero da Versao
        /// </summary>
        private static string numeroVersaoURL;

        /// <summary>
        /// string estatica Numero da Versao
        /// </summary>
        private static string queryStringNoCache = Guid.NewGuid().ToString().GetHashCode().ToString("x");

        /// <summary>
        /// Gets or sets propriedade Numero da versão
        /// </summary>
        public static string NumeroVersaoAssembly
        {
            get
            {
                if (numeroVersaoAssembly == null)
                {
                    AssemblyName assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName();
#if DEBUG
                    numeroVersaoAssembly = string.Format("v{0}.{1}.{2}.{3}-debug", assemblyName.Version.Major, assemblyName.Version.Minor, assemblyName.Version.Build, assemblyName.Version.Revision);
#else
                    numeroVersaoAssembly = string.Format("v{0}.{1}.{2}.{3}-release", assemblyName.Version.Major, assemblyName.Version.Minor, assemblyName.Version.Build, assemblyName.Version.Revision);
#endif
                }

                return numeroVersaoAssembly;
            }

            set
            {
            }
        }

        public static string NumeroVersaoAssemblyLabel
        {
            get
            {
                if (numeroVersaoAssemblyLabel == null)
                {
                    AssemblyName assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName();
                    numeroVersaoAssemblyLabel = string.Format("v{0}.{1}.{2}.{3}", assemblyName.Version.Major, assemblyName.Version.Minor, assemblyName.Version.Build, assemblyName.Version.Revision);
                }

                return numeroVersaoAssemblyLabel;
            }

            set
            {
            }
        }

        /// <summary>
        /// Gets or sets propriedade Numero da versão
        /// </summary>
        public static string NumeroVersao
        {
            get
            {
                if (numeroVersao == null)
                {
                    AssemblyName assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName();
                    numeroVersao = string.Format("v{0}.{1}.{2}.{3}", assemblyName.Version.Major, assemblyName.Version.Minor, assemblyName.Version.Build, assemblyName.Version.Revision);
                }

                return numeroVersao;
            }

            set
            {
            }
        }

        public static string LabelVersao
        {
            get
            {
                return string.Format("© {0} Linx - Todos direitos reservados.", DateTime.Now.Year.ToString());
            }

            set
            {
            }
        }

        /// <summary>
        /// Gets or sets propriedade Numero da versão
        /// </summary>
        public static DateTime DataVersao
        {
            get
            {
                if (!dataVersao.HasValue)
                {
                    dataVersao = Linx.Internet.Application.Framework.Common.GetBuildDateTime(System.Reflection.Assembly.GetExecutingAssembly());
                }

                return dataVersao.Value;
            }

            set
            {
            }
        }

        /// <summary>
        /// Gets or sets propriedade Numero da versão
        /// </summary>
        public static string NumeroVersaoReduzida
        {
            get
            {
                if (numeroVersaoReduzida == null)
                {
                    AssemblyName assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName();
#if DEBUG
                    numeroVersaoReduzida = string.Format("{0}.{1}.{2}", assemblyName.Version.Major, assemblyName.Version.Minor, assemblyName.Version.Build);
#else
                    numeroVersaoReduzida = string.Format("{0}.{1}.{2}", assemblyName.Version.Major, assemblyName.Version.Minor, assemblyName.Version.Build);
#endif
                }

                return numeroVersaoReduzida;
            }

            set
            {
            }
        }

        /// <summary>
        /// Gets or sets propriedade Numero da versão
        /// </summary>
        public static string NumeroVersaoURL
        {
            get
            {
                if (numeroVersaoURL == null)
                {
                    numeroVersaoURL = NumeroVersaoAssembly.Replace(".", "-");
                }

                return numeroVersaoURL;
            }

            set
            {
            }
        }

        /// <summary>
        /// Gets or sets Retorna request QueryStringNoCache
        /// </summary>
        public static string HashNoCache
        {
            get
            {
                return queryStringNoCache;
            }

            set
            {
            }
        }

        /// <summary>
        /// Gets or sets Retorna request QueryStringNoCache
        /// </summary>
        public static string QueryStringNoCache
        {
            get
            {
                //return string.Concat("hash=", queryStringNoCache);
                return string.Empty;
            }

            set
            {
            }
        }


        public static bool CheckApplicationCache()
        {
            // liga / desliga o cache via querystring
            if (ConfigurationManager.AppSettings.GetValue<bool>("Shell.BrowseApplicationCache.Enabled", false))
            {
                return !System.Web.HttpContext.Current.Request.QueryStringExistsValue("appcache", "off", "0", "false");
            }
            else
            {
                return System.Web.HttpContext.Current.Request.QueryStringExistsValue("appcache", "on", "1", "true");
            }
        }

        public static string GetShellMode()
        {
            // liga / desliga via querystring
            if (System.Web.HttpContext.Current.Request.QueryStringExistsValue("appmode", "dev"))
            {
                return "DEV";
            }
            if (System.Web.HttpContext.Current.Request.QueryStringExistsValue("appmode", "setup"))
            {
                return "SETUP";
            }
            if (System.Web.HttpContext.Current.Request.QueryStringExistsValue("appmode", "prod"))
            {
                return "PROD";
            }
            else
            {
                return ConfigurationManager.AppSettings.GetValue("ShellMode", "PROD").ToUpper();
            }
        }

        public static string GetLoginMode()
        {
            // liga / desliga via querystring
            if (System.Web.HttpContext.Current.Request.QueryStringExistsValue("loginmode", "portalux"))
            {
                return "DEV";
            }
            if (System.Web.HttpContext.Current.Request.QueryStringExistsValue("loginmode", "posux"))
            {
                return "SETUP";
            }
            if (System.Web.HttpContext.Current.Request.QueryStringExistsValue("loginmode", "trusted"))
            {
                return "PROD";
            }
            else
            {
                return ConfigurationManager.AppSettings.GetValue("Shell.LoginMode", "PORTALUX").ToUpper();
            }
        }

        public static bool GetShellCombineAndMinifyCssJsMode()
        {
            if (GetShellMode() == "SETUP")
                return true;
            else
                return ConfigurationManager.AppSettings.GetValue<bool>("Shell.CombineAndMinifyCssJs.Enabled", false);
        }

        public static string BuildUrl(string contentPath)
        {
            return string.Concat("/linx-internet-application/", BaseHelpers.NumeroVersaoURL, contentPath, "?", System.Web.HttpContext.Current.Request.QueryString);
        }

    }
}

using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Reflection;
using System.Windows.Resources;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.Composition.Hosting;

namespace Linx.Tools
{
    public static partial class AssemblyHelper
    {

        public static string ReadResourceContent(string resourcePath, Assembly assembly = null)
        {
            if (assembly == null)
                return "";

            string body = String.Empty;
            //Read template file            
            using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    body = reader.ReadToEnd();
                }
            }

            return body;
        }

        public static Assembly LoadWithDependencies(string assemblyPath)
        {
            return Assembly.LoadFrom(assemblyPath);
        }
        
        public static string GetCurrentAssemblyDirectory<T>()
        {
            string codeBase = typeof(T).Assembly.GetName().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return System.IO.Path.GetDirectoryName(path);
        }

        public static Assembly Load(string assemblyFileName)
        {
            if (!File.Exists(assemblyFileName))
                return null;

            Assembly assembly = null;

            using (FileStream fileStream = File.OpenRead(assemblyFileName))
            {
                assembly = Load(fileStream);
            }

            return assembly;
        }


        public static Assembly LoadFromZip(string relativeUriString, Stream zipPackageStream)
        {
            Uri uri = new Uri(relativeUriString, UriKind.Relative);
            StreamResourceInfo assemblySri = IsolatedStorageFileExtension.GetStreamResourceInfo(zipPackageStream, relativeUriString);
            return Load(assemblySri.Stream);
        }


        /// <summary>
        /// Loader extension by MEF, using dll name, in folder [DllHostLocation]\bin\Extension\[IdLinx]
        /// </summary>
        /// <param name="dllExtensionName">Dll Name</param>
        /// <param name="idLinx">IdLinx</param>
        /// <returns></returns>
        public static AggregateCatalog LoadUserExtension(string dllExtensionName, int idLinx)
        {
            return LoadUserExtension(dllExtensionName, idLinx, null);
        }

        public static AggregateCatalog LoadUserExtension(string dllExtensionName, int idLinx, string filePath)
        {
            AggregateCatalog catalog = new AggregateCatalog();

            try
            {
                if (filePath.IsNullOrEmpty())
                {
                    filePath = String.Format(@"{0}bin\Extension\{1}", AppDomain.CurrentDomain.BaseDirectory, idLinx);
                }

                if (Directory.Exists(filePath))
                    catalog.Catalogs.Add(new DirectoryCatalog(filePath, dllExtensionName));
            }
            catch
            {
                catalog = new AggregateCatalog();
            }

            return catalog;

        }

    }
}

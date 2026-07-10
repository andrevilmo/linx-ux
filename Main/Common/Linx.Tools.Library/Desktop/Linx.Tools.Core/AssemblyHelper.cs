using System;
using System.Net;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Linq;

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
            return Assembly.Load(new AssemblyName(assemblyPath));
        }

        public static string GetCurrentAssemblyDirectory<T>()
        {
            return System.IO.Directory.GetCurrentDirectory();
        }
                
    }
}

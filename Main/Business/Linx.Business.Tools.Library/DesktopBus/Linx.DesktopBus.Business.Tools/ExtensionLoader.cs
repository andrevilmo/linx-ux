using Linx.Framework.BV;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Business.Tools
{
    public class ExtensionLoader
    {
        public static AggregateCatalog LoadUserExtension(Assembly executingAssembly, string dllExtensionName, string filePath = null)
        {
            return LinxBusinessExtensionLoader.LoadUserExtension(executingAssembly, dllExtensionName, filePath);
        }
    }
}

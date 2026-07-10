using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Linx.Tools;

namespace Linx.Framework.BV
{
    public class LinxBusinessExtensionLoader
    {
        public static AggregateCatalog LoadUserExtension(Assembly executingAssembly, string dllExtensionName, string filePath = null)
        {
            var idLinx = BusinessUserServiceHelper.GetCurrentIdLinx("ControleSistema");

            return idLinx.HasValue ? AssemblyHelper.LoadUserExtension(dllExtensionName, idLinx.Value) : null;
        }
    }
}

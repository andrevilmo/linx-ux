
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Http.Dispatcher;

namespace Linx.SelfHost
{
    public class WebAPILoader : DefaultAssembliesResolver
    {
        public override ICollection<Assembly> GetAssemblies()
        {

            ICollection<Assembly> externalAssemblies = new List<Assembly>();
            string path = string.Empty;

            path = ConfigurationManager.AppSettings["PathAssembly"];
            if (string.IsNullOrEmpty(path))
                path = string.Format(@"{0}\External", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            Console.WriteLine("Path: " + path);

            if (!Directory.Exists(path))
                return externalAssemblies;

            var list = Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories)
                                      .Where(n => !n.Contains("SQLite") &&
                                      !n.Contains("WITDataStore") &&
                                      !n.Contains("EntityFramework")).ToList();

            //var list = Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories)
            //                          .Where(n => !n.Contains("SQLite") &&
            //                          !n.Contains("WITDataStore")).ToList();


            try
            {
                //for (int i = 0; i < list.Count(); i++)
                //    externalAssemblies.Add(Assembly.LoadFrom(list[i]));

                foreach (var dll in list)
                    externalAssemblies.Add(Assembly.LoadFrom(dll));

                return externalAssemblies;
            }
            catch (Exception ex)
            {

                Console.WriteLine("Erro: " + ex.Message);
                return externalAssemblies;
            }



        }
    }
}
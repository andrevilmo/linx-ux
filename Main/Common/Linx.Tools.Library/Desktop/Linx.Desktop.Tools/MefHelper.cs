// -----------------------------------------------------------------------
// <copyright file="RepositoryHelper.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Linx.Tools
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition.Hosting;
    using System.Linq;
    using System.Text;
    using System.Web;

    public static class ImplementationHelper<T>
    {
        public static T GetInstance(string implementationName, string assemblyPattern = "*.Implementations*")
        {
            var directory = String.Empty;
            try
            {
                directory = HttpRuntime.BinDirectory;
            }
            catch
            {
                directory = AssemblyHelper.GetCurrentAssemblyDirectory<T>();
            }
            var catalog = new AggregateCatalog(new DirectoryCatalog(directory, assemblyPattern + ".dll"));
            CompositionContainer compositionContainer = new CompositionContainer(catalog);

            //Get Implementations by MEF
            return GetPluginFromContainer(compositionContainer, implementationName);
        }

        private static T GetPluginFromContainer(CompositionContainer container, string implementationName)
        {
            foreach (Lazy<T, IDictionary<string, object>> pluginExport in container.GetExports<T, IDictionary<string, object>>())
            {
                if (pluginExport.Metadata["ImplementationName"].ToString().Equals(implementationName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return pluginExport.Value;
                }
            }
            return default(T);
        }
    }

    public static class RepositoryHelper<T>
    {
        public static T GetInstance(string fileName, string repositoryName)
        {
            var catalog = new AggregateCatalog(new DirectoryCatalog(HttpRuntime.BinDirectory, fileName + "*.dll"));
            CompositionContainer compositionContainer = new CompositionContainer(catalog);

            //Get Repositories by MEF
            return GetPluginFromContainer(compositionContainer, repositoryName);
        }

        private static T GetPluginFromContainer(CompositionContainer container, string repositoryName)
        {
            foreach (Lazy<T, IDictionary<string, object>> pluginExport in container.GetExports<T, IDictionary<string, object>>())
            {
                if (pluginExport.Metadata["RepositoryName"].ToString().Equals(repositoryName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return pluginExport.Value;
                }
            }
            return default(T);
        }

        public static Dictionary<string, T> GetInstances(string fileName)
        {
            var catalog = new AggregateCatalog(new DirectoryCatalog(HttpRuntime.BinDirectory, fileName + "*.dll"));
            CompositionContainer compositionContainer = new CompositionContainer(catalog);
            // busca os repositorios via MEF
            return GetPluginsFromContainer(compositionContainer);
        }

        private static Dictionary<string, T> GetPluginsFromContainer(CompositionContainer container)
        {
            string dictionaryKey;
            Dictionary<string, T> instances = new Dictionary<string, T>();
            foreach (Lazy<T, IDictionary<string, object>> pluginExport in container.GetExports<T, IDictionary<string, object>>())
            {
                dictionaryKey = pluginExport.Metadata["RepositoryName"].ToString();
                if (!instances.ContainsKey(dictionaryKey))
                    instances.Add(dictionaryKey, pluginExport.Value);
            }
            return instances;
        }

    }


    #region BusinessModelConnectionHelper

    public interface IBusinessModelInstructions
    {
        string GetName();
        string GetInstructions();
    }

    public static class BusinessModelInstructionHelper
    {
        public static void LoadInstructions()
        {
            string key, value;
            _instructions = new Dictionary<string, string>();
            foreach (var instance in GetInstances("*.BM"))
            {
                key = instance.GetName();
                value = instance.GetInstructions();
                if (!value.IsNullOrEmpty() && !_instructions.ContainsKey(key))
                    _instructions.Add(key, value);
            }
        }

        private static Dictionary<string, string> _instructions = null;
        public static Dictionary<string, string> Instructions
        {
            get 
            {
                if (_instructions == null)
                   LoadInstructions();
                return _instructions;
            }
        }

        private static IEnumerable<IBusinessModelInstructions> GetInstances(string assemblyPattern)
        {
            var directory = String.Empty;
            try
            {
                directory = HttpRuntime.BinDirectory;
            }
            catch
            {
                directory = AssemblyHelper.GetCurrentAssemblyDirectory<IBusinessModelInstructions>();
            }
            var catalog = new AggregateCatalog(new DirectoryCatalog(directory, assemblyPattern + ".dll"));
            CompositionContainer compositionContainer = new CompositionContainer(catalog);

            foreach (Lazy<IBusinessModelInstructions, IDictionary<string, object>> pluginExport in compositionContainer.GetExports<IBusinessModelInstructions, IDictionary<string, object>>())
            {
                yield return pluginExport.Value;
            }
        }

    }

    #endregion

}

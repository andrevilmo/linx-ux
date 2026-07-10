// -----------------------------------------------------------------------
// <copyright file="RepositoryHelper.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Linx.Tools
{
    using System;
    using System.Collections.Generic;
    using System.Composition.Hosting;
    using System.Composition.Convention;
    using System.Linq;
    using System.Text;

    public static class ImplementationHelper<T>
    {
        public static T GetInstance(string implementationName, string assemblyPattern = "*.Implementations*")
        {
            var directory = AssemblyHelper.GetCurrentAssemblyDirectory<T>();

            var configuration = new ContainerConfiguration()
            .WithAssembliesInPath(directory, assemblyPattern);

            //Get Implementations by MEF
            return GetPluginFromContainer(configuration.WithPart<T>(), implementationName);
        }

        private static T GetPluginFromContainer(ContainerConfiguration configuration, string implementationName)
        {
            using (var container = configuration.CreateContainer())
            {
                foreach (var pluginExport in container.GetExports<T>(implementationName))
                {
                    return pluginExport;
                }
            }
            
            return default(T);
        }
    }

    public static class RepositoryHelper<T>
    {
        public static T GetInstance(string fileName, string repositoryName)
        {            

            var directory = AssemblyHelper.GetCurrentAssemblyDirectory<T>();

            var configuration = new ContainerConfiguration()
            .WithAssembliesInPath(directory, fileName + "*.dll");

            //Get Implementations by MEF
            return GetPluginFromContainer(configuration.WithPart<T>(), repositoryName);
            
        }

        private static T GetPluginFromContainer(ContainerConfiguration configuration, string repositoryName)
        {
            using (var container = configuration.CreateContainer())
            {
                foreach (var pluginExport in container.GetExports<T>(repositoryName))
                {
                    return pluginExport;
                }
            }
            
            return default(T);
        }

        public static Dictionary<string, T> GetInstances(string fileName)
        {
            var directory = AssemblyHelper.GetCurrentAssemblyDirectory<T>();

            var configuration = new ContainerConfiguration()
            .WithAssembliesInPath(directory, fileName + "*.dll");
            // busca os repositorios via MEF
            return GetPluginsFromContainer(configuration);
        }

        private static Dictionary<string, T> GetPluginsFromContainer(ContainerConfiguration configuration)
        {
            string dictionaryKey;
            Dictionary<string, T> instances = new Dictionary<string, T>();
            
            using (var container = configuration.CreateContainer())
            {
                foreach (var pluginExport in container.GetExports<T>())
                {
                    dictionaryKey = pluginExport.GetType().Name;
                    if (!instances.ContainsKey(dictionaryKey))
                        instances.Add(pluginExport.GetType().Name, pluginExport);
                }
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
            directory = AssemblyHelper.GetCurrentAssemblyDirectory<IBusinessModelInstructions>();
           
            var configuration = new ContainerConfiguration()
           .WithAssembliesInPath(directory, assemblyPattern + "*.dll");
            
            foreach (Lazy<IBusinessModelInstructions, IDictionary<string, object>> pluginExport in configuration.CreateContainer().GetExports<IBusinessModelInstructions>())
            {
                yield return pluginExport.Value;
            }
        }

    }

    #endregion

}

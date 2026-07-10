using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Composition.Hosting;
using System.Composition.Convention;
using System.IO;
using System.Runtime.Loader;

namespace Linx.Tools
{
    public static class ContainerConfigurationExtensions
    {
        public static ContainerConfiguration WithAssembliesInPath(this ContainerConfiguration configuration, string path, string assemblyPattern, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            return WithAssembliesInPath(configuration, path, null, assemblyPattern, searchOption);
        }

        public static ContainerConfiguration WithAssembliesInPath(this ContainerConfiguration configuration, string path, AttributedModelProvider conventions, string assemblyPattern, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            var assemblies = System.IO.Directory
                .GetFiles(path, assemblyPattern, searchOption)
                .Select(AssemblyLoadContext.GetAssemblyName)
                .Select(AssemblyLoadContext.Default.LoadFromAssemblyName)
                .ToList();

            configuration = configuration.WithAssemblies(assemblies, conventions);

            return configuration;
        }
    }

}

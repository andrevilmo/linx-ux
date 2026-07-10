using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Linx.LinqExtensions
{
    public class AppSettings
    {
        static AppSettings()
        {
            Instance = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
        }
        public static IConfigurationRoot Instance { get; set; }
    }
}

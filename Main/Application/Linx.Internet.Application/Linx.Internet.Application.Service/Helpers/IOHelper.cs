using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Linx.Internet.Application.Service.Helpers
{
    public static partial class IOHelper
    {
        public static void CreateDirectoryNotExists(string path, bool removeDirectory)
        {
            if (removeDirectory)
                DeleteDirectory(path, true);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

        }

        public static void CreateDirectoryNotExists(string path)
        {
            if (path.Contains("%TEMP%"))
            {
                path = path.Replace("%TEMP%\\", Path.GetTempPath());
            }

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

        }

        public static void CleanDirectory(string path)
        {
            foreach (var file in Directory.GetFiles(path))
            {
                File.Delete(file);
            }
        }

        public static void CleanDirectory(string path, string searchPattern)
        {
            foreach (var file in Directory.GetFiles(path, searchPattern))
            {
                File.Delete(file);
            }
        }

        public static void DeleteDirectory(string path, bool recursive)
        {
            try
            {
                Directory.Delete(path, recursive);
            }
            catch { }
        }

    }
}

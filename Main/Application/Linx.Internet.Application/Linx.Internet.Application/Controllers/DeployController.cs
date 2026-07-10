using AttributeRouting;
using AttributeRouting.Web.Mvc;
using System.Web;
using System.Web.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Linx.Internet.Application.Common.Filters;
using System.Web.Security;
using Linx.Internet.Application.Framework.Classes;
using Ionic.Zip;
using System.Net;
using System.Net.Http;
using Linx.Internet.Application;
using Linx.Internet.Application.Helpers;
using Linx.Internet.Application.Framework.Web;



namespace Linx.Internet.Application.Controllers
{
    [RoutePrefix("Deploy")]
    public class DeployController : Controller
    {
        string root = Linx.Internet.Application.Helpers.HtmlHelper.GetRoot();
        string moduleId = Linx.Internet.Application.Helpers.HtmlHelper.ModuleId();


        [OutputCache(CacheProfile = "ProfileCacheRoot")]
        [GET("info.json")]
        public ActionResult Info(string moduleName)
        {
            List<dynamic> routesVersion = new List<dynamic>();

            foreach (var module in PluginConfig.CurrentModules.OrderBy(o => o.Key))
            {
                if (!string.IsNullOrEmpty(moduleName))
                {
                    if (moduleName.IndexOf(module.Value.ModuleName, StringComparison.InvariantCultureIgnoreCase) == -1)
                        continue;
                }

                #region processamento: routesversion
                var moduleItem = new
                {
                    moduleUId = module.Value.ModuleUId.ToString(),
                    moduleId = string.Concat("pkg_", module.Value.ModuleName),
                    moduleName = module.Value.ModuleName,

                    requireId = string.Concat("v", module.Value.AssemblyVersion, "-", module.Value.AssemblyType).Replace(".", "-").ToLower(),
                    versionNumber = string.Concat("v", module.Value.AssemblyVersion, "-", module.Value.AssemblyType).ToLower(),
                    buildDate = module.Value.BuildDate.ToString("dd/MM/yyyy HH:mm"),
                    CRC32 = module.Value.CRC32,
                    IsModuleShell = module.Value.IsModuleShell,
                    Download = string.Concat("deploy/download/", module.Value.ModuleName, ".zip")
                };
                routesVersion.Add(moduleItem);
                #endregion
            }

            //string jsonResponse = Newtonsoft.Json.JsonConvert.SerializeObject(routes);
            return View(routesVersion);
        }

        [OutputCache(CacheProfile = "ProfileCacheRoot")]
        [GET("download/{moduleName}.zip")]
        public ActionResult DownloadModule(string moduleName)
        {
            moduleName = string.IsNullOrEmpty(moduleName) ? "modules" : moduleName;
            bool isFull = (moduleName.IndexOf("modules", StringComparison.InvariantCultureIgnoreCase) > -1);
            string zipFileNameDownload = string.Concat(moduleName, "-", Linx.Internet.Application.Helpers.BaseHelpers.GetShellMode().ToLowerInvariant(), ".zip");

            string tempRoot = Path.GetTempPath(); // ou HttpRuntime.CodegenDir ou Server.MapPath("~")
            string tempDirName = Path.GetRandomFileName();
            string tempDirNameZip = Path.GetRandomFileName();

            string tempPathFiles = Path.Combine(tempRoot, moduleName, tempDirName);
            string tempPathZip = Path.Combine(tempRoot, moduleName, tempDirNameZip);

            string tempFullPathZip = Path.Combine(tempPathZip, zipFileNameDownload);

            IOHelper.CreateDirectoryNotExists(tempPathFiles, true);
            IOHelper.CreateDirectoryNotExists(tempPathZip, true);

            var modules = Linx.Internet.Application.Framework.Web.PluginConfig.CurrentModules;
            foreach (var module in modules)
            {
                if (isFull == false)
                {
                    if (moduleName.IndexOf(module.Value.ModuleName, StringComparison.InvariantCultureIgnoreCase) == -1)
                        continue;
                }

                var files = Linx.Internet.Application.Framework.Web.PluginConfig.EmbeddedResources.Where(w => w.Value.ModuleName.Equals(module.Value.ModuleName, StringComparison.InvariantCultureIgnoreCase));

                foreach (var f in files.OrderBy(o => o.Key))
                {
                    if (BaseHelpers.GetShellMode() == "SETUP")
                    {
                        if (f.Value.FullPathIO.Contains("\\lib\\"))
                        {
                            if ((f.Value.FullPathIO.Contains("lib\\requirejs\\text")
                                || f.Value.FullPathIO.Contains("lib\\breeze")
                                || f.Value.FullPathIO.Contains("lib\\durandal")
                                || f.Value.FullPathIO.Contains("lib\\requirejs\\json")
                                || f.Value.FullPathIO.Contains("lib\\hi_base32")
                                || f.Value.FullPathIO.Contains("lib\\jsSHA")
                                || f.Value.FullPathIO.Contains(".png")
                                || f.Value.FullPathIO.Contains(".jpg")
                                || f.Value.FullPathIO.Contains(".gif")
                                || f.Value.FullPathIO.Contains(".ttf")
                                || f.Value.FullPathIO.Contains(".svg")
                                || f.Value.FullPathIO.Contains(".woff")
                                || f.Value.FullPathIO.Contains(".map")
                                || f.Value.FullPathIO.Contains(".eot")
                                || f.Value.FullPathIO.Contains(".otf")) == false)

                            {
                                continue;
                            }
                        }
                    }

                    string fullPathDir = string.Concat(tempPathFiles, Path.GetDirectoryName(f.Value.FullPathIO));
                    string fullPathFile = string.Concat(tempPathFiles, f.Value.FullPathIO);

                    IOHelper.SaveFileBytes(fullPathFile, f.Value.Bytes);
                }

                if (module.Value.IsModuleShell == true)
                {
                    var moduleId = Linx.Internet.Application.Helpers.HtmlHelper.ModuleId();
                    var root = (string.IsNullOrEmpty(Linx.Internet.Application.Helpers.HtmlHelper.GetRoot()) ? "/" : Linx.Internet.Application.Helpers.HtmlHelper.GetRoot());
                    var oldValue = string.Concat(root, Linx.Internet.Application.Helpers.HtmlHelper.ModuleId(), "/lib/");

                    IOHelper.SaveFileText(string.Concat(tempPathFiles, "/", moduleId, "/lib/theme-css-default.css"), RequestFileText(moduleId, "/lib/theme-css-default.css", oldValue, string.Empty), Encoding.GetEncoding("ISO-8859-1"));
                    IOHelper.SaveFileText(string.Concat(tempPathFiles, "/", moduleId, "/lib/theme-css-orange.css"), RequestFileText(moduleId, "/lib/theme-css-orange.css", oldValue, string.Empty), Encoding.GetEncoding("ISO-8859-1"));
                    IOHelper.SaveFileText(string.Concat(tempPathFiles, "/", moduleId, "/lib/theme-css-black.css"), RequestFileText(moduleId, "/lib/theme-css-black.css", oldValue, string.Empty), Encoding.GetEncoding("ISO-8859-1"));
                    IOHelper.SaveFileText(string.Concat(tempPathFiles, "/", moduleId, "/lib/core.css"), RequestFileText(moduleId, "/lib/core.css", oldValue, string.Empty), Encoding.GetEncoding("ISO-8859-1"));


                    IOHelper.SaveFileBytes(string.Concat(tempPathFiles, "/", moduleId, "/App/managers/__auth.js"), RequestFileBytes(moduleId, "/App/managers/__auth.js"));
                    IOHelper.SaveFileBytes(string.Concat(tempPathFiles, "/", moduleId, "/AppLogin/managers/__auth.js"), RequestFileBytes(moduleId, "/AppLogin/managers/__auth.js"));
                    IOHelper.SaveFileBytes(string.Concat(tempPathFiles, "/", moduleId, "/App/managers/__route.js"), RequestFileBytes(moduleId, "/App/managers/__route.js"));
                    IOHelper.SaveFileBytes(string.Concat(tempPathFiles, "/", moduleId, "/AppLogin/managers/__route.js"), RequestFileBytes(moduleId, "/AppLogin/managers/__route.js"));
                    IOHelper.SaveFileBytes(string.Concat(tempPathFiles, "/", moduleId, "/scripts/core.js"), RequestFileBytes(moduleId, "/scripts/core.js"));

                    if (BaseHelpers.GetShellMode() == "SETUP")
                    {
                        IOHelper.SaveFileBytes(string.Concat(tempPathFiles, "/", moduleId, "/lib/requirejs/require.js"), RequestFileBytes(moduleId, "/lib/requirejs/require.js"));
                        IOHelper.SaveFileBytes(string.Concat(tempPathFiles, "/", moduleId, "/lib/linx/js/config-require.js"), RequestFileBytes(moduleId, "/lib/linx/js/config-require.js"));
                    }

                    IOHelper.SaveFileText(string.Concat(tempPathFiles, "/index.html"), RequestFileText(string.Empty, "/", Linx.Internet.Application.Helpers.HtmlHelper.GetRoot(), string.Empty), Encoding.GetEncoding("ISO-8859-1"));
                    IOHelper.SaveFileBytes(string.Concat(tempPathFiles, "/config.json"), RequestFileBytes(string.Empty, "config.json"));
                    IOHelper.SaveFileBytes(string.Concat(tempPathFiles, "/routes.json"), RequestFileBytes(string.Empty, "routes.json"));

                    IOHelper.SaveFileBytes(string.Concat(tempPathFiles, "/favicon.ico"), RequestFileBytes(string.Empty, "favicon.ico"));
                    IOHelper.SaveFileBytes(string.Concat(tempPathFiles, "/icon.png"), RequestFileBytes(string.Empty, "icon.png"));
                }
            }

            System.IO.Compression.ZipFile.CreateFromDirectory(tempPathFiles, tempFullPathZip, System.IO.Compression.CompressionLevel.Optimal, false);

            IOHelper.DeleteDirectory(tempPathFiles, true);
            return File(System.IO.File.OpenRead(tempFullPathZip), "application/zip", zipFileNameDownload);
        }

        [OutputCache(CacheProfile = "ProfileCacheRoot")]
        [GET("selfhost.zip")]
        public ActionResult DownloadSelfHost()
        {
            string moduleName = "SelfHost";
            string zipFileNameDownload = string.Concat(moduleName, "-", Linx.Internet.Application.Helpers.BaseHelpers.GetShellMode().ToLowerInvariant(), ".zip");

            string tempRoot = Path.GetTempPath(); // ou HttpRuntime.CodegenDir ou Server.MapPath("~")
            string tempDirName = Path.GetRandomFileName();
            string tempDirNameZip = Path.GetRandomFileName();

            string tempPathFiles = Path.Combine(tempRoot, moduleName, tempDirName);
            string tempPathZip = Path.Combine(tempRoot, moduleName, tempDirNameZip);

            string tempFullPathZip = Path.Combine(tempPathZip, zipFileNameDownload);

            IOHelper.CreateDirectoryNotExists(tempPathFiles, true);
            IOHelper.CreateDirectoryNotExists(tempPathZip, true);

            var modules = Linx.Internet.Application.Framework.Web.PluginConfig.CurrentModules;
            foreach (var module in modules)
            {
                var files = Linx.Internet.Application.Framework.Web.PluginConfig.EmbeddedResources.Where(w => w.Value.ModuleName.Equals(module.Value.ModuleName, StringComparison.InvariantCultureIgnoreCase));

                foreach (var f in files.OrderBy(o => o.Key))
                {
                    if (f.Value.FileName.IndexOf("SelfHostBase.zip", StringComparison.InvariantCultureIgnoreCase) > -1)
                    {
                        string selfHostPath = System.IO.Path.Combine(tempPathFiles, f.Value.FileName);

                        System.IO.File.WriteAllBytes(selfHostPath, f.Value.Bytes);
                        System.IO.Compression.ZipFile.ExtractToDirectory(selfHostPath, tempPathFiles);
                        System.IO.File.Delete(selfHostPath);
                    }

                    if (BaseHelpers.GetShellMode() == "SETUP")
                    {
                        if (f.Value.FullPathIO.Contains("\\lib\\"))
                        {
                            if ((f.Value.FullPathIO.Contains("lib\\requirejs\\text")
                                || f.Value.FullPathIO.Contains("lib\\breeze")
                                || f.Value.FullPathIO.Contains("lib\\durandal")
                                || f.Value.FullPathIO.Contains("lib\\requirejs\\json")
                                || f.Value.FullPathIO.Contains("lib\\hi_base32")
                                || f.Value.FullPathIO.Contains("lib\\jsSHA")
                                || f.Value.FullPathIO.Contains(".png")
                                || f.Value.FullPathIO.Contains(".jpg")
                                || f.Value.FullPathIO.Contains(".gif")
                                || f.Value.FullPathIO.Contains(".ttf")
                                || f.Value.FullPathIO.Contains(".svg")
                                || f.Value.FullPathIO.Contains(".woff")
                                || f.Value.FullPathIO.Contains(".map")
                                || f.Value.FullPathIO.Contains(".eot")
                                || f.Value.FullPathIO.Contains(".otf")) == false)

                            {
                                continue;
                            }
                        }
                    }

                    string fullPathDir = string.Concat(tempPathFiles, "/static/", Path.GetDirectoryName(f.Value.FullPathIO));
                    string fullPathFile = string.Concat(tempPathFiles, "/static/", f.Value.FullPathIO);

                    IOHelper.SaveFileBytes(fullPathFile, f.Value.Bytes);
                }

                if (module.Value.IsModuleShell == true)
                {
                    var moduleId = Linx.Internet.Application.Helpers.HtmlHelper.ModuleId();
                    var root = (string.IsNullOrEmpty(Linx.Internet.Application.Helpers.HtmlHelper.GetRoot()) ? "/" : Linx.Internet.Application.Helpers.HtmlHelper.GetRoot());
                    var oldValue = string.Concat(root, Linx.Internet.Application.Helpers.HtmlHelper.ModuleId(), "/lib/");

                    IOHelper.SaveFileText(string.Concat(tempPathFiles, "/static/", moduleId, "/lib/theme-css-default.css"), RequestFileText(moduleId, "/lib/theme-css-default.css", oldValue, string.Empty), Encoding.GetEncoding("ISO-8859-1"));
                    IOHelper.SaveFileText(string.Concat(tempPathFiles, "/static/", moduleId, "/lib/theme-css-orange.css"), RequestFileText(moduleId, "/lib/theme-css-orange.css", oldValue, string.Empty), Encoding.GetEncoding("ISO-8859-1"));
                    IOHelper.SaveFileText(string.Concat(tempPathFiles, "/static/", moduleId, "/lib/theme-css-black.css"), RequestFileText(moduleId, "/lib/theme-css-black.css", oldValue, string.Empty), Encoding.GetEncoding("ISO-8859-1"));
                    IOHelper.SaveFileText(string.Concat(tempPathFiles, "/static/", moduleId, "/lib/core.css"), RequestFileText(moduleId, "/lib/core.css", oldValue, string.Empty), Encoding.GetEncoding("ISO-8859-1"));


                    IOHelper.SaveFileBytes(string.Concat(tempPathFiles, "/static/", moduleId, "/App/managers/__auth.js"), RequestFileBytes(moduleId, "/App/managers/__auth.js"));
                    IOHelper.SaveFileBytes(string.Concat(tempPathFiles, "/static/", moduleId, "/AppLogin/managers/__auth.js"), RequestFileBytes(moduleId, "/AppLogin/managers/__auth.js"));
                    IOHelper.SaveFileBytes(string.Concat(tempPathFiles, "/static/", moduleId, "/App/managers/__route.js"), RequestFileBytes(moduleId, "/App/managers/__route.js"));
                    IOHelper.SaveFileBytes(string.Concat(tempPathFiles, "/static/", moduleId, "/AppLogin/managers/__route.js"), RequestFileBytes(moduleId, "/AppLogin/managers/__route.js"));
                    IOHelper.SaveFileBytes(string.Concat(tempPathFiles, "/static/", moduleId, "/scripts/core.js"), RequestFileBytes(moduleId, "/scripts/core.js"));

                    if (BaseHelpers.GetShellMode() == "SETUP")
                    {
                        IOHelper.SaveFileBytes(string.Concat(tempPathFiles, "/static/", moduleId, "/lib/requirejs/require.js"), RequestFileBytes(moduleId, "/lib/requirejs/require.js"));
                        IOHelper.SaveFileBytes(string.Concat(tempPathFiles, "/static/", moduleId, "/lib/linx/js/config-require.js"), RequestFileBytes(moduleId, "/lib/linx/js/config-require.js"));
                    }

                    IOHelper.SaveFileText(string.Concat(tempPathFiles, "/static/index.html"), RequestFileText(string.Empty, "/", Linx.Internet.Application.Helpers.HtmlHelper.GetRoot(), string.Empty), Encoding.GetEncoding("ISO-8859-1"));
                    IOHelper.SaveFileBytes(string.Concat(tempPathFiles, "/static/config.json"), RequestFileBytes(string.Empty, "config.json"));
                    IOHelper.SaveFileBytes(string.Concat(tempPathFiles, "/static/routes.json"), RequestFileBytes(string.Empty, "routes.json"));

                    IOHelper.SaveFileBytes(string.Concat(tempPathFiles, "/static/favicon.ico"), RequestFileBytes(string.Empty, "favicon.ico"));
                    IOHelper.SaveFileBytes(string.Concat(tempPathFiles, "/static/icon.png"), RequestFileBytes(string.Empty, "icon.png"));
                }
            }

            System.IO.Compression.ZipFile.CreateFromDirectory(tempPathFiles, tempFullPathZip, System.IO.Compression.CompressionLevel.Optimal, false);

            IOHelper.DeleteDirectory(tempPathFiles, true);
            return File(System.IO.File.OpenRead(tempFullPathZip), "application/zip", zipFileNameDownload);
        }

        [OutputCache(CacheProfile = "ProfileCacheRoot")]
        [GET("selfhostbase.zip")]
        public ActionResult DownloadSelfHostBase()
        {
            var zipFile = Linx.Internet.Application.Framework.Web.PluginConfig.EmbeddedResources.FirstOrDefault(w => w.Value.FileName.IndexOf("SelfHostBase.zip", StringComparison.InvariantCultureIgnoreCase) > -1);

            return File(zipFile.Value.Bytes, "application/zip", "SelfHostBase.zip");
        }

        private byte[] RequestFileBytes(string path, string url)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                UriBuilder urlSite = new UriBuilder();
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(System.Web.HttpContext.Current.Server.ResolveUrl(path + url + "?appmode=" + Linx.Internet.Application.Helpers.BaseHelpers.GetShellMode()));

                request.Timeout = 30000; // padrao: 30s

                try
                {
                    using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        response.GetResponseStream().CopyTo(memory);
                    }
                }
                catch
                {
                }

                memory.Position = 0;
                return memory.ToArray();
            }
        }

        private string RequestFileText(string path, string url, string oldValue = null, string newValue = null)
        {
            StringBuilder contentFile = new StringBuilder();
            UriBuilder urlSite = new UriBuilder();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(System.Web.HttpContext.Current.Server.ResolveUrl(path + url + "?appmode=" + Linx.Internet.Application.Helpers.BaseHelpers.GetShellMode()));

            request.Timeout = 30000; // padrao: 30s

            try
            {
                using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("ISO-8859-1")))
                    {
                        contentFile = new StringBuilder(reader.ReadToEnd());
                    }
                }
                if (oldValue != null)
                {
                    contentFile = contentFile.Replace(oldValue, newValue);
                }
            }
            catch
            {
            }

            return contentFile.ToString();
        }

        private byte[] RequestFile2(string path, string url, string oldValue = null, string newValue = null)
        {
            StringBuilder contentFile = new StringBuilder();
            UriBuilder urlSite = new UriBuilder();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(System.Web.HttpContext.Current.Server.ResolveUrl(path + url + "?appmode=" + Linx.Internet.Application.Helpers.BaseHelpers.GetShellMode()));

            request.Timeout = 30000; // padrao: 30s

            try
            {
                using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("ISO-8859-1")))
                    {
                        contentFile = new StringBuilder(reader.ReadToEnd());
                    }
                }
                if (oldValue != null)
                {
                    contentFile = contentFile.Replace(oldValue, newValue);
                }
            }
            catch
            {
            }

            return Encoding.GetEncoding("ISO-8859-1").GetBytes(contentFile.ToString());
        }

        public struct ZipItem
        {
            string _FileNameSource;
            string _PathinArchive;
            byte[] _Bytes;
            public ZipItem(string __FileNameSource, string __PathinArchive)
            {
                _Bytes = null;
                _FileNameSource = __FileNameSource;
                _PathinArchive = __PathinArchive;
            }
            public ZipItem(byte[] __Bytes, string __PathinArchive)
            {
                _Bytes = __Bytes;
                _FileNameSource = "";
                _PathinArchive = __PathinArchive;

            }
            public string FileNameSource
            {
                set
                {
                    FileNameSource = value;
                }
                get
                {
                    return _FileNameSource;
                }
            }
            public string PathinArchive
            {
                set
                {
                    _PathinArchive = value;
                }
                get
                {
                    return _PathinArchive;
                }
            }
            public byte[] Bytes
            {
                set
                {
                    _Bytes = value;
                }
                get
                {
                    return _Bytes;
                }
            }
        }

        public MemoryStream AddFileToArchive_InputByte(ZipItem[] ZipItems)
        {
            var memoryStream = new MemoryStream();

            using (var archive = new System.IO.Compression.ZipArchive(memoryStream, System.IO.Compression.ZipArchiveMode.Create, true))
            {
                foreach (ZipItem item in ZipItems)
                {
                    System.IO.Compression.ZipArchiveEntry FileInArchive = archive.CreateEntry(item.PathinArchive);

                    //Open File in Archive For Write
                    using (var OpenFileInArchive = FileInArchive.Open())
                    {
                        byte[] ReadAllbytes = new byte[4096];//Capcity buffer
                        int ReadByte = 4096;
                        int TotalWrite = 0;
                        while (TotalWrite != item.Bytes.Length)
                        {
                            if (TotalWrite + 4096 > item.Bytes.Length)
                                ReadByte = item.Bytes.Length - TotalWrite;

                            Array.Copy(item.Bytes, TotalWrite, ReadAllbytes, 0, ReadByte);

                            //Write Bytes
                            OpenFileInArchive.Write(ReadAllbytes, 0, ReadByte);
                            TotalWrite += ReadByte;
                        }
                    }
                }
            }

            return memoryStream;
        }
    }
}

using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using NLog;
using Owin;
using System.Configuration;
using Linx.Internet.Application.Service;
using System.IO;
using Ionic.Zip;
using System.Collections;
using System.Collections.Generic;
using System;
using Microsoft.Win32;
using System.Diagnostics;
//using System.IO.Compression;


namespace Linx.Internet.Application.Service
{
    public class OwinStartup
    {
        public static readonly Logger Logger = LogManager.GetLogger(SyncSvcInstaller.SVC_NAME);
        private string rootPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

        public void Configuration(IAppBuilder app)
        {
            Logger.Info("Configurando servidor web");

            var staticPath = this.BuildStaticDirectory();
            Logger.Info("Diretorio raiz '{0}'", staticPath);

            this.ExtractZip(staticPath);

            var options = new FileServerOptions
            {
                EnableDirectoryBrowsing = true,
                EnableDefaultFiles = true,
                FileSystem = new PhysicalFileSystem(staticPath)
            };
            options.StaticFileOptions.ServeUnknownFileTypes = true;

            app.UseErrorPage();
            app.UseFileServer(options);
        }

        private string BuildStaticDirectory()
        {
            string staticPath = ConfigurationManager.AppSettings.GetValue<string>("StaticPath", "");

            if (string.IsNullOrEmpty(staticPath))
            {
                staticPath = Path.Combine(rootPath, "static");
            }

            if (!Directory.Exists(staticPath))
            {
                Logger.Trace("Criando diretorio '{0}'", staticPath);
                Directory.CreateDirectory(staticPath);
            }

            return staticPath;
        }

        private void ExtractZip(string staticPath)
        {
            IEnumerable<string> zipsPath = Directory.EnumerateFiles(rootPath, "*.zip");

            if (zipsPath.GetEnumerator().Current != null)
            {
                DeleteFilesAndFoldersRecursively(staticPath);

                foreach (var zipPath in zipsPath)
                {
                    Logger.Info("Extraindo zip '{0}'...", zipPath);
                    using (ZipFile zip = ZipFile.Read(zipPath))
                    {
                        zip.ExtractAll(staticPath, ExtractExistingFileAction.OverwriteSilently);
                    }

                    File.Move(zipPath, Path.Combine(staticPath, Path.GetFileName(zipPath)));
                }
            }

        }

        private void DeleteFilesAndFoldersRecursively(string targetDir)
        {
            foreach (string file in Directory.GetFiles(targetDir))
            {
                System.IO.File.Delete(file);
            }

            foreach (string subDir in Directory.GetDirectories(targetDir))
            {
                DeleteFilesAndFoldersRecursively(subDir);
            }

            System.Threading.Thread.Sleep(1); // This makes the difference between whether it works or not. Sleep(0) is not enough.
            Directory.Delete(targetDir);
        }

    }
}

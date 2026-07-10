using Linx.Internet.Application.Framework.Web;
using Linx.Internet.Application.Helpers;
using NLog;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading;
using System.Linq;
using Linx.Internet.Application.Common;
using Microsoft.AspNet.SignalR;
using Linx.Internet.Application.Hubs;
using System.Configuration;


[assembly: WebActivator.PostApplicationStartMethod(typeof(Linx.Internet.Application.App_Start.FileWatcher), "PostStart", Order = 4)]

namespace Linx.Internet.Application.App_Start
{
    public static class FileWatcher
    {
        public static readonly FileSystemWatcher watcher = new FileSystemWatcher();
        public static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        public static void PostStart()
        {
            if (ConfigurationManager.AppSettings.GetValue("ShellMode", "PROD").ToUpperInvariant() == "PROD" || ConfigurationManager.AppSettings.GetValue<bool>("Shell.FileWatcher.Enabled", false) == false)
            {
                Logger.Info("Monitoramento de arquivos desabilitado");
                return;
            }
            Logger.Info("Inicio da configuração 'FileWatcher'");

            string fullPath = Path.Combine(HttpRuntime.AppDomainAppPath, "app");

            if (System.IO.Directory.Exists(fullPath))
            {
                MyFileSystemWatcher fsw = new MyFileSystemWatcher(Path.Combine(HttpRuntime.AppDomainAppPath, "app"), "*.*");
                fsw.IncludeSubdirectories = true;
                fsw.Changed += new System.IO.FileSystemEventHandler(fsw_Changed);

                fsw.EnableRaisingEvents = true;
            }
        }

        private static void fsw_Changed(object sender, System.IO.FileSystemEventArgs e)
        {
            var extension = System.IO.Path.GetExtension(e.FullPath);

            // verificar a extensao (.js / .html)
            if (extension.Equals(".js", StringComparison.InvariantCultureIgnoreCase) == false 
                && extension.Equals(".html", StringComparison.InvariantCultureIgnoreCase) == false 
                && extension.Equals(".txt", StringComparison.InvariantCultureIgnoreCase) == false)
                return;

            var context = GlobalHost.ConnectionManager.GetHubContext<VersionHub>();
            var fileFinishProcess = System.IO.Path.GetFileNameWithoutExtension(e.FullPath);
            if (!fileFinishProcess.Equals("info", StringComparison.InvariantCultureIgnoreCase))
                return;

            // moduleName, files
            string[] arrFullPath = e.FullPath.Split('\\');
            var indexApp = Array.FindIndex(arrFullPath, item => item.Equals("app", StringComparison.InvariantCultureIgnoreCase));
            var indexRoot = (indexApp + 1);

            if (indexApp == -1)
                return;

            var moduleName = arrFullPath[indexApp - 1].Replace(".", "-").ToLower();
            var pkgName = string.Concat("pkg_", moduleName);
            var displayFileName = string.Join("/", arrFullPath, indexRoot, (arrFullPath.Length - indexRoot));
            var fileName = string.Concat(pkgName, "/", displayFileName);

            // despresa as alterações do shell
            if (moduleName == "linx-internet-application")
                return;

            if (extension.Equals(".html", StringComparison.InvariantCultureIgnoreCase))
            {
                fileName = string.Concat("text!", fileName);
            }
            else
            {
                fileName = fileName.Replace(extension, string.Empty);
            }

            Logger.Info("Changed: FileName - {0}, ChangeType - {1}", e.Name, e.ChangeType);
            context.Clients.All.clientFileChanged(moduleName, pkgName, fileName, displayFileName);
        }
    }
}
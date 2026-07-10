using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Linx.Tools;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Composition;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Linx.Framework.BV.Utilitarios;
using System.IO;
using System.IO.Compression;
using System.Net.Http.Headers;
using System.Web.Configuration;
using System.Configuration;
using System.Collections.Specialized;

namespace Linx.Framework.BV.WebAPI.DS.Controllers
{

    ////////////////////////////////////////////////////////////////////////////
    /////////////////////////// Business Api Controller ////////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class LinxFrameworkUtilitariosController
    {
        [Route("CleanCache"), System.Web.Http.HttpPost()]
        public bool CleanCache(TcsUsuarioAutenticacao info)
        {
            bool isPosux = (LocalServiceBus.Enabled && BusinessUserServiceHelper.GetCurrentLoginMode() == "POSUX");

            if (!isPosux)
            {
                if (info.BandeiraRede)
                    repository.Context.CleanUserBandeiraRedeCache(info.UidUsuario);

                if (info.Conexao)
                    repository.Context.CleanConnectionsCache();

                if (info.Geral)
                    repository.Context.CleanCache();

                if (info.Relatorio)
                    repository.Context.CleanTelerikReportsCache();
            }

            if (info.Modulo)
                repository.Context.CleanUserModulesCache(isPosux ? Guid.Empty : info.UidUsuario);

            if (!info.UidUsuario.IsNullOrEmpty())
                repository.Context.CleanUserInfoCache(info.UidUsuario);

            return true;
        }

        [Route("download/service.zip")]
        [HttpGet()]
        public HttpResponseMessage DownloadZip()
        {
            var PathFileZip = System.Web.Hosting.HostingEnvironment.MapPath("~/Linx.SelfHost.zip");
            var PathFolderBin = System.Web.Hosting.HostingEnvironment.MapPath("~/bin/");
            var PathFolderService = System.Web.Hosting.HostingEnvironment.MapPath("~");

            string tempRoot = Path.GetTempPath();
            string tempDirName = Path.GetRandomFileName();
            //var dest = tempRoot + tempDirName + @"\Linx.SelfHost\External\Linx\";
            var dest = tempRoot + tempDirName + @"\External\Linx\";

            if (File.Exists(tempRoot + @"Service.Selfhost.zip"))
                File.Delete(tempRoot + @"Service.Selfhost.zip");

            ZipFile.ExtractToDirectory(PathFileZip, tempRoot + "/" + tempDirName);

            DirectoryCopy(PathFolderBin, dest, true);

            var configuration = WebConfigurationManager.OpenWebConfiguration("~");
            var section = (ConnectionStringsSection)configuration.GetSection("connectionStrings");

            ExeConfigurationFileMap oConfigFile = new ExeConfigurationFileMap();
            oConfigFile.ExeConfigFilename = tempRoot + tempDirName + @"\Linx.SelfHost.exe.config";
            Configuration oConfiguration = ConfigurationManager.OpenMappedExeConfiguration(oConfigFile, ConfigurationUserLevel.None);

            ConnectionStringSettings oConnectionSettings = new ConnectionStringSettings();

            foreach (ConnectionStringSettings item in section.ConnectionStrings)
                oConfiguration.ConnectionStrings.ConnectionStrings.Add(item);

            oConfiguration.Save(ConfigurationSaveMode.Modified);


            ZipFile.CreateFromDirectory(tempRoot + tempDirName, tempRoot + @"Service.Selfhost.zip", CompressionLevel.Optimal, false);

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(new FileStream(tempRoot + @"Service.Selfhost.zip", FileMode.Open, FileAccess.Read));
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = "Service.Selfhost.zip";
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/zip");

            return response;
            
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Diretório nao existe:  "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                if (file.Name.Contains("Newtonsoft.Json"))
                    continue;
                    
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, true);
                
                
            }

            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
    }
}

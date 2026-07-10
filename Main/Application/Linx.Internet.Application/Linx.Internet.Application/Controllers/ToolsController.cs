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

namespace Linx.Internet.Application.Controllers
{
    [RoutePrefix("Tools")]
    public class ToolsController : Controller
    {
        string root = Linx.Internet.Application.Helpers.HtmlHelper.GetRoot();
        string moduleId = Linx.Internet.Application.Helpers.HtmlHelper.ModuleId();


        //[NoCache]
        //[GET("SetAuthCookie")]
        //public ActionResult SetAuthCookie()
        //{
        //    FormsAuthentication.SetAuthCookie("fernando.chaves", true);
        //    Response.Write(User.Identity.Name);
        //    return new EmptyResult();
        //}


        //[NoCache]
        //[GET("GetAuthCookie")]
        //public ActionResult GetAuthCookie()
        //{
        //    Response.Write("Name:" + User.Identity.Name);
        //    return new EmptyResult();
        //}

        public void DeleteFilesAndFoldersRecursively(string target_dir)
        {
            foreach (string file in Directory.GetFiles(target_dir))
            {
                System.IO.File.Delete(file);
            }

            foreach (string subDir in Directory.GetDirectories(target_dir))
            {
                DeleteFilesAndFoldersRecursively(subDir);
            }

            System.Threading.Thread.Sleep(1); // This makes the difference between whether it works or not. Sleep(0) is not enough.
            Directory.Delete(target_dir);
        }

        [NoCache]
        [GET("Info")]
        public ActionResult Info()
        {
            Response.Write(BundleConfig.ApplicationExecutionId);

            return new EmptyResult();
        }

        [NoCache]
        [GET("ExtractFiles/Clean")]
        public ActionResult ExtractFilesClean()
        {
            this.Cabecalho();

            // verifica se o diretorio "_custom" existe
            var customPath = System.IO.Path.Combine(Server.MapPath("~"), "_custom");

            Response.Write("---------------------------<BR>");
            Response.Write("--- Diretorio excluido [" + customPath + "] <br>");
            Response.Write("---------------------------<BR>");

            if (Directory.Exists(customPath))
                DeleteFilesAndFoldersRecursively(customPath);

            if (!Directory.Exists(customPath))
                Directory.CreateDirectory(customPath);

            return new EmptyResult();
        }

        [NoCache]
        [GET("ExtractFiles")]
        public ActionResult ExtractFiles(string action, string moduleName, string viewName, string fileName)
        {
            this.Cabecalho();

            Response.Write(Request.PhysicalApplicationPath + "<BR>");

            // verifica se o diretorio "_custom" existe
            var customPath = System.IO.Path.Combine(Server.MapPath("~"), "_custom");
            if (!Directory.Exists(customPath))
                Directory.CreateDirectory(customPath);


            var modules = Linx.Internet.Application.Framework.Web.PluginConfig.CurrentModules;
            foreach (var m in modules)
            {
                if (!string.IsNullOrEmpty(moduleName) && (m.Value.ModuleName != moduleName))
                {
                    continue;
                }

                var modulePath = System.IO.Path.Combine(customPath, m.Value.ModuleNamePath);
                if (!Directory.Exists(modulePath))
                    Directory.CreateDirectory(modulePath);

                var moduleVersionPath = System.IO.Path.Combine(modulePath, m.Value.AssemblyVersionPath);
                if (!Directory.Exists(moduleVersionPath))
                    Directory.CreateDirectory(moduleVersionPath);

                //Response.Write("---------------------------<BR>");
                //Response.Write("--- Diretorio criado [" + moduleVersionPath + "] <br>");
                //Response.Write("---------------------------<BR>");


                var files = Linx.Internet.Application.Framework.Web.PluginConfig.EmbeddedResources.Where(w => w.Value.ModuleName.Equals(m.Value.ModuleName, StringComparison.InvariantCultureIgnoreCase));

                if (!string.IsNullOrEmpty(fileName))
                {
                    files = files.Where(w => w.Value.FullPath.Equals(fileName, StringComparison.InvariantCultureIgnoreCase));
                }

                foreach (var f in files.OrderBy(o => o.Key))
                {
                    if (!string.IsNullOrEmpty(viewName) && (f.Value.Url.IndexOf(viewName, StringComparison.InvariantCultureIgnoreCase) == -1))
                    {
                        continue;
                    }

                    var filePath = string.Concat(customPath, f.Value.FullPathIO);
                    var fileDir = System.IO.Path.GetDirectoryName(filePath);
                    if (!Directory.Exists(fileDir))
                        Directory.CreateDirectory(fileDir);

                    System.IO.File.WriteAllBytes(filePath, f.Value.Bytes);
                    Response.Write("Arquivo criado [" + filePath + "]<br>");
                }
            }

            return new EmptyResult();
        }

        [NoCache]
        [GET("Static")]
        public ActionResult GenStaticZip()
        {
            this.Cabecalho();

            var zipFileName = string.Concat("lia", ".zip");
            var comentZip = new StringBuilder();

            comentZip.AppendLine("-----------------------------------------------------------------");
            comentZip.AppendLine(string.Concat("lia_", Linx.Internet.Application.Helpers.BaseHelpers.NumeroVersao, "_", Linx.Internet.Application.Helpers.BaseHelpers.DataVersao.ToString("yyyyMMdd-HHmm"), "_", Linx.Internet.Application.Helpers.BaseHelpers.GetShellMode().ToLowerInvariant(), ".zip"));
            comentZip.AppendLine("-----------------------------------------------------------------");
            comentZip.AppendLine("");

            using (ZipFile zip = new ZipFile())
            {
                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;

                var modules = Linx.Internet.Application.Framework.Web.PluginConfig.CurrentModules;
                foreach (var m in modules)
                {
                    comentZip.AppendLine("-----------------------------------------------------------------");
                    comentZip.AppendFormat("AssemblyName [{0}.dll]\r\n", m.Value.AssemblyName);
                    comentZip.AppendLine("-----------------------------------------------------------------");
                    comentZip.AppendFormat("ModuleUId: {0}\r\nModuleName: {1}\r\nAssemblyVersion: {2}\r\nBuildDate: {3}", m.Value.ModuleUId, m.Value.ModuleName, m.Value.AssemblyVersion, m.Value.BuildDate.ToString("dd/MM/yyyy HH:mm"));
                    comentZip.AppendLine();
                    comentZip.AppendLine();

                    var files = Linx.Internet.Application.Framework.Web.PluginConfig.EmbeddedResources.Where(w => w.Value.ModuleName.Equals(m.Value.ModuleName, StringComparison.InvariantCultureIgnoreCase));

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

                        var filePath = f.Value.FullPathIO;
                        zip.AddEntry(filePath, f.Value.Bytes);
                    }

                }

                zip.Comment = comentZip.ToString();

                // /index.html
                var moduleId = Linx.Internet.Application.Helpers.HtmlHelper.ModuleId();

                zip.AddEntry("index.html", RequestFile(string.Empty, "/"));
                zip.AddEntry("favicon.ico", RequestFile(string.Empty, "favicon.ico"));
                zip.AddEntry("config.json", RequestFile(string.Empty, "config.json"));

                zip.AddEntry(string.Concat(moduleId, "/lib/theme-css-default.css"), RequestFile(moduleId, "/lib/theme-css-default.css"));
                zip.AddEntry(string.Concat(moduleId, "/lib/theme-css-orange.css"), RequestFile(moduleId, "/lib/theme-css-orange.css"));
                zip.AddEntry(string.Concat(moduleId, "/lib/theme-css-black.css"), RequestFile(moduleId, "/lib/theme-css-black.css"));
                zip.AddEntry(string.Concat(moduleId, "/lib/core.css"), RequestFile(moduleId, "/lib/core.css"));

                zip.AddEntry(string.Concat(moduleId, "/scripts/requirejs/__config.js"), RequestFile(moduleId, "/scripts/requirejs/__config.js"));
                zip.AddEntry(string.Concat(moduleId, "/App/managers/__auth.js"), RequestFile(moduleId, "/App/managers/__auth.js"));
                zip.AddEntry(string.Concat(moduleId, "/AppLogin/managers/__auth.js"), RequestFile(moduleId, "/AppLogin/managers/__auth.js"));
                zip.AddEntry(string.Concat(moduleId, "/App/managers/__route.js"), RequestFile(moduleId, "/App/managers/__route.js"));
                zip.AddEntry(string.Concat(moduleId, "/AppLogin/managers/__route.js"), RequestFile(moduleId, "/AppLogin/managers/__route.js"));
                zip.AddEntry(string.Concat(moduleId, "/scripts/core.js"), RequestFile(moduleId, "/scripts/core.js"));
                zip.AddEntry(string.Concat(moduleId, "/lib/requirejs/require.js"), RequestFile(moduleId, "/lib/requirejs/require.js"));
                zip.AddEntry(string.Concat(moduleId, "/lib/linx/js/config-require.js"), RequestFile(moduleId, "/lib/linx/js/config-require.js"));


                var tempPath = System.IO.Path.Combine(Server.MapPath("~"), "temp");
                var zipPath = System.IO.Path.Combine(tempPath, zipFileName);

                using (MemoryStream zipStreamOut = new MemoryStream())
                {
                    if (!Directory.Exists(tempPath))
                        Directory.CreateDirectory(tempPath);

                    zip.Save(zipPath);
                }

                Response.Write("Arquivo '" + zipPath + "' gerado com sucesso!");

                Response.Redirect("~/temp/" + zipFileName, true);
                return new EmptyResult();
            }
        }
        private MemoryStream RequestFile(string path, string url)
        {
            MemoryStream retorno = new MemoryStream();
            UriBuilder urlSite = new UriBuilder();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(System.Web.HttpContext.Current.Server.ResolveUrl(path + url + "?appmode=" + Linx.Internet.Application.Helpers.BaseHelpers.GetShellMode()));

            request.Timeout = 30000; // padrao: 30s

            try
            {
                using (var response = (System.Net.HttpWebResponse)request.GetResponse())
                {
                    response.GetResponseStream().CopyTo(retorno);
                }
            }
            catch (HttpRequestException ex)
            {
            }
            catch (Exception ex)
            {
            }

            retorno.Position = 0;
            return retorno;
        }


        [NoCache]
        [GET("DownloadModules")]
        public ActionResult DownloadModules(string action, string moduleName)
        {
            // verifica se o diretorio "_custom" existe
            var customPath = System.IO.Path.Combine(Server.MapPath("~"), "_custom");
            var binPath = System.IO.Path.Combine(Server.MapPath("~"), "bin");
            var zipFileName = (string.IsNullOrEmpty(moduleName) ? "Modules.zip" : moduleName);
            var comentZip = new StringBuilder();

            using (ZipFile zip = new ZipFile())
            {
                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;

                var modules = Linx.Internet.Application.Framework.Web.PluginConfig.CurrentModules;
                foreach (var m in modules)
                {
                    if ((!string.IsNullOrEmpty(moduleName) && (m.Value.ModuleName != moduleName)) || m.Value.ModuleOrder == -1)
                    {
                        continue;
                    }

                    var moduleBinPath = System.IO.Path.Combine(binPath, string.Concat(m.Value.AssemblyName, ".dll"));
                    zip.AddEntry("bin/" + m.Value.AssemblyName + ".dll", System.IO.File.ReadAllBytes(moduleBinPath));
                    comentZip.AppendLine("-----------------------------------------------------------------");
                    comentZip.AppendFormat("AssemblyName [{0}.dll]\r\n", m.Value.AssemblyName);
                    comentZip.AppendLine("-----------------------------------------------------------------");
                    comentZip.AppendFormat("ModuleUId: {0}\r\nModuleName: {1}\r\nAssemblyVersion: {2}\r\nBuildDate: {3}", m.Value.ModuleUId, m.Value.ModuleName, m.Value.AssemblyVersion, m.Value.BuildDate.ToString("dd/MM/yyyy HH:mm"));
                    comentZip.AppendLine();
                    comentZip.AppendLine();

                    if (!string.IsNullOrEmpty(moduleName))
                    {
                        zipFileName = string.Concat(m.Value.AssemblyName, "_v", m.Value.AssemblyVersion, ".zip");
                    }
                }

                zip.Comment = comentZip.ToString();


                using (MemoryStream zipStreamOut = new MemoryStream())
                {
                    //zip.Save( System.IO.Path.Combine(Server.MapPath("~"), zipFileName));
                    zip.Save(zipStreamOut);

                    zipStreamOut.Position = 0;
                    return File(new MemoryStream(zipStreamOut.ToArray()), "application/zip", zipFileName);
                }
            }
        }

        [NoCache]
        [GET("Files")]
        public ActionResult Files()
        {
            this.Cabecalho();

            var itens = Linx.Internet.Application.Framework.Web.PluginConfig.EmbeddedResources.OrderBy(o => o.Key);
            Response.Write("Total: " + itens.Count());

            Response.Write("<table>");
            Response.Write("<tr>");
            Response.Write("<td><b>File</b></td>");
            Response.Write("<td><b>Size</b></td>");
            Response.Write("<td><b>CRC32</b></td>");
            Response.Write("<td><b>Extract Files</b></td>");
            Response.Write("</tr>");
            foreach (var item in itens)
            {
                Response.Write("<tr>");
                Response.Write("<td><code>");

                Response.Write("<a href='" + item.Value.Url + "'>");
                Response.Write(item.Key);
                Response.Write("<a>");

                Response.Write("</code></td>");
                //item.Value.Url
                Response.Write("<td><code>");
                Response.Write(item.Value.Bytes.Length + " bytes");

                Response.Write("</code></td>");

                Response.Write("<td><code>");
                Response.Write(item.Value.CRC32);
                Response.Write("</code></td>");

                Response.Write("<td><a href=" + root + "tools/extractfiles?modulename=" + item.Value.ModuleName + "&filename=" + item.Value.FullPath + " target=\"_blank\">");
                Response.Write("[extract]</a></td>");
                Response.Write("</tr>");

                Response.Write("</tr>");
            }

            Response.Write("</table>");

            return new EmptyResult();
        }

        [NoCache]
        [GET("Modules")]
        public ActionResult Modules()
        {
            this.Cabecalho();

            var itens = Linx.Internet.Application.Framework.Web.PluginConfig.CurrentModules;
            Response.Write("<table>");
            Response.Write("<tr>");
            Response.Write("<td><b>ModuleUId</b></td>");
            Response.Write("<td><b>ModuleName</b></td>");
            Response.Write("<td><b>ModuleOrder</b></td>");
            Response.Write("<td><b>AssemblyName</b></td>");
            Response.Write("<td><b>AssemblyVersion</b></td>");
            Response.Write("<td><b>BuildDate</b></td>");
            Response.Write("<td><b>AssemblyType</b></td>");
            Response.Write("<td><b>ShellAssemblyVersion</b></td>");
            Response.Write("<td><b>CRC32</b></td>");
            Response.Write("<td><b>Extract Files</b></td>");
            Response.Write("</tr>");

            foreach (var item in itens)
            {
                Response.Write("<tr>");

                Response.Write("<td><code>");
                Response.Write(item.Value.ModuleUId + ":");
                Response.Write("</code></td>");

                Response.Write("<td><code>");
                Response.Write(item.Value.ModuleName);
                Response.Write("</code></td>");

                Response.Write("<td><code>");
                Response.Write(item.Value.ModuleOrder);
                Response.Write("</code></td>");

                Response.Write("<td><code>");
                Response.Write(item.Value.AssemblyName);
                Response.Write("</code></td>");

                Response.Write("<td><code>");
                Response.Write(item.Value.AssemblyVersion);
                Response.Write("</code></td>");

                Response.Write("<td><code>");
                Response.Write(item.Value.BuildDate.ToString("dd/MM/yyyy HH:mm"));
                Response.Write("</code></td>");

                Response.Write("<td><code>");
                Response.Write(item.Value.AssemblyType);
                Response.Write("</code></td>");

                Response.Write("<td><code>");
                Response.Write(item.Value.ShellAssemblyVersion);
                Response.Write("</code></td>");

                Response.Write("<td><code>");
                Response.Write(item.Value.CRC32);
                Response.Write("</code></td>");

                Response.Write("<td><a href=" + root + "tools/extractfiles?modulename=" + item.Value.ModuleName + " target=\"_blank\">");
                Response.Write("[extract]</a></td>");
                Response.Write("</tr>");
            }

            Response.Write("</table>");

            return new EmptyResult();
        }

        /// <summary>
        /// Action resposavel por remover um item do cache
        /// </summary>
        /// <param name="removerItem">Chave do item armazenado no cache</param>
        /// <returns>Action vazia</returns>
        [NoCache]
        [GET("AspNetCache/{removerItem?}")]
        public EmptyResult AspNetCache(string removerItem)
        {
            this.Cabecalho();
            IEnumerator en = HttpContext.Cache.GetEnumerator();

            #region acao REMOVER
            if (removerItem != null)
            {
                if (removerItem.Equals("clean", StringComparison.CurrentCultureIgnoreCase))
                {
                    while (en.MoveNext())
                    {
                        DictionaryEntry item = (DictionaryEntry)en.Current;
                        HttpContext.Cache.Remove(item.Key.ToString());
                    }
                }
                else
                {
                    HttpContext.Cache.Remove(removerItem);
                }

                // refresh 
                en = HttpContext.Cache.GetEnumerator();
            }
            #endregion

            Response.Write("PhysicalApplicationPath = " + Request.PhysicalApplicationPath);
            Response.Write("<BR>");
            Response.Write("EffectivePercentagePhysicalMemoryLimit = " + HttpContext.Cache.EffectivePercentagePhysicalMemoryLimit);
            Response.Write("<BR>");
            Response.Write("<a href='AspNetCache/Clean'> X </a>");
            Response.Write("Total = " + HttpContext.Cache.Count);
            Response.Write("<BR>");

            Response.Write("<BR>");
            Response.Write("---------------------------<BR>");
            Response.Write("--- HttpContext.Cache ---<BR>");
            Response.Write("---------------------------<BR>");
            Response.Write("<BR>");
            en.Reset();
            while (en.MoveNext())
            {
                DictionaryEntry item = (DictionaryEntry)en.Current;

                Response.Write("<a href='AspNetCache?removerItem=" + item.Key.ToString() + "'> X </a>");
                Response.Write("[<b>" + item.Key.ToString() + "</b>] = ");

                var r = item.Value.ToString();
                if (r.Length > 15)
                {
                    Response.Write("...");
                }
                else
                {
                    Response.Write("<CODE>" + r + "</CODE>");
                }

                Response.Write("<BR>");
            }

            return new EmptyResult();
        }

        private void Cabecalho()
        {
            #region Sem cache de pagina
            Response.CacheControl = "no-cache";
            Response.AddHeader("pragma", "no-cache");
            Response.Expires = -1;
            #endregion

            Response.Write("<BR>");
            Response.Write("--------------------------------------------------------------<BR>");
            Response.Write("<b>Servidor [" + Server.MachineName + "]</b><br>");
            Response.Write("--------------------------------------------------------------<BR>");
            Response.Write("<BR>");
        }

        [NoCache]
        [GET("InfoUser")]
        public ActionResult InfoUser()
        {
            this.Cabecalho();

            Response.Write("User.Identity.Name: " + User.Identity.Name + "<BR>");
            Response.Write("uidEmpresa: " + this.Session["uidEmpresa"] + "<BR>");
            //Response.Write("uidGrupoAcesso: " + this.Session["uidGrupoAcesso"] + "<BR>");
            Response.Write("uidUsuario: " + this.Session["uidUsuario"] + "<BR>");
            Response.Write("uidAplicacao: " + this.Session["uidAplicacao"] + "<BR>");
            Response.Write("tokenId: " + this.Session["tokenId"] + "<BR>");

            return new EmptyResult();
        }

        [NoCache]
        [POST("Upload")]
        public ActionResult Upload()
        {
            return new EmptyResult();
        }

        [NoCache]
        [GET("LocalCache")]
        public EmptyResult LocalCache()
        {
            this.Cabecalho();

            Response.Write("<BR>");
            Response.Write("[visualiza os arquivos em cache] chrome://appcache-internals <BR>");
            Response.Write("<BR>");
            Response.Write("[local cache ligado] <a href='/?appcache=on' target='_blank'>?appcache=on</a><BR>");
            Response.Write("<BR>");
            Response.Write("[local cache desligado] <a href='/?appcache=off' target='_blank'>?appcache=off</a><BR>");
            Response.Write("<BR>");
            return new EmptyResult();
        }

        //chrome://appcache-internals/
    }
}

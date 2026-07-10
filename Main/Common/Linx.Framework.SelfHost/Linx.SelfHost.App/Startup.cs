using Microsoft.Owin;
using Owin;
using System.Configuration;
using Microsoft.Owin.StaticFiles;
using Microsoft.Owin.FileSystems;

[assembly: OwinStartup(typeof(Linx.SelfHost.App.Startup))]

namespace Linx.SelfHost.App
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.UseWelcomePage();
            var path = ConfigurationManager.AppSettings["PathApp"];
            var options = new FileServerOptions
            {
                EnableDirectoryBrowsing = true,
                EnableDefaultFiles = true,
                FileSystem = new PhysicalFileSystem(path)
            };
            options.StaticFileOptions.ServeUnknownFileTypes = true;
            app.UseErrorPage();
            app.UseFileServer(options);
        }
        
    }
}

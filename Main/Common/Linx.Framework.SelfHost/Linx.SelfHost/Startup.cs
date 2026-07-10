using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using LinxHttpContext;
using Microsoft.Owin.StaticFiles;

[assembly: OwinStartup(typeof(Linx.SelfHost.Startup))]

namespace Linx.SelfHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var webApiConfiguration = ConfigureWebApi();
            app.UseFileServer(enableDirectoryBrowsing: true);
            var optionsFile = new FileServerOptions();
            optionsFile.StaticFileOptions.ServeUnknownFileTypes = true;
            app.UseFileServer(optionsFile);
            app.UseWebApi(webApiConfiguration);
        }
        private HttpConfiguration ConfigureWebApi()
        {
            Linx.Tools.LocalServiceBus.Start();
            var config = new HttpConfiguration();
            config.MessageHandlers.Add(new Linx.Tools.WebApi.CorsHandler());
            config.MessageHandlers.Add(new HttpContextHandler());
            LibConfig.Register(config);
            return config;
        }
    }
}

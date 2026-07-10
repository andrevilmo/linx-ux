using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using System.Configuration;
[assembly: OwinStartup(typeof(Linx.Internet.Application.App_Start.Startup))]

namespace Linx.Internet.Application.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            if (ConfigurationManager.AppSettings.GetValue<bool>("Shell.CheckVersion.Enabled", false))
            {
                var hubConfiguration = new HubConfiguration();
                hubConfiguration.EnableDetailedErrors = false;
                hubConfiguration.EnableJavaScriptProxies = false; // signalr/hubs

                app.MapSignalR("/signalr", hubConfiguration);
            }
        }
    }
} 
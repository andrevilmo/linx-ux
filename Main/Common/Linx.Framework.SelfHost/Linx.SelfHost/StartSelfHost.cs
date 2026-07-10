using Microsoft.Owin.Hosting;
using System;

namespace Linx.SelfHost
{
    public class StartSelfHost
    {
        private IDisposable _owinHost;
        public void StartHost(string port)
        {
            //StartOptions options = new StartOptions();
            Microsoft.Owin.Hosting.StartOptions options = new Microsoft.Owin.Hosting.StartOptions();
            options.Urls.Add(string.Format("http://*:{0}/", port));
            //options.Port = 1715;
            //options.ServerFactory = "Microsoft.Owin.Host.HttpListener";
            _owinHost = WebApp.Start<Startup>(options);

        }
        public void StopHost()
        {
            _owinHost.Dispose();
        }
    }
}

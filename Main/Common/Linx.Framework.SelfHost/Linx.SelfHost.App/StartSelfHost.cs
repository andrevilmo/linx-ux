using Microsoft.Owin.Hosting;
using System;

namespace Linx.SelfHost.App
{
    public class StartSelfHost
    {
        private IDisposable _owinHost;
        public void StartHost(string port)
        {
            StartOptions options = new StartOptions();
            options.Urls.Add(string.Format("http://*:{0}/", port));
            _owinHost = WebApp.Start<Startup>(options);

        }
        public void StopHost()
        {
            _owinHost.Dispose();
        }
    }
}

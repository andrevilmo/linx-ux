using System;
using System.Windows;

namespace Linx.Internet.Application.WinHost
{
    public partial class App : System.Windows.Application
    {
        public App()
        {
            var cachePath = System.IO.Path.Combine(Environment.CurrentDirectory, "cache");

            if (!System.IO.Directory.Exists(cachePath))
            {
                System.IO.Directory.CreateDirectory(cachePath);
            }

            var settings = new CefSharp.CefSettings
            {
                IgnoreCertificateErrors = true,
                LogSeverity = CefSharp.LogSeverity.Default,
                BrowserSubprocessPath = string.Format("Linx.Internet.Application.WinHost.Browser_{0}.exe", (Environment.Is64BitProcess ? "x64" : "x86")),
                CachePath = cachePath
            };

            CefSharp.Cef.Initialize(settings);
        }
    }
}

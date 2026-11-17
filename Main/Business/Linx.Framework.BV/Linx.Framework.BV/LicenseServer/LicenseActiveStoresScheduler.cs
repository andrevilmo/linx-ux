using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Linx.LicenseServer.Client.Licensing;

namespace Linx.Framework.BV.LicenseServer
{
    /// <summary>
    /// Daily background send of Lojas Ativas via ILicenseManager (does not run on the user request path).
    /// </summary>
    public static class LicenseActiveStoresScheduler
    {
        private static readonly object Sync = new object();
        private static Timer _timer;
        private static int _started;

        public static void EnsureStarted()
        {
            if (Interlocked.Exchange(ref _started, 1) == 1)
                return;

            // First tick after a short delay; then once per day.
            _timer = new Timer(OnTick, null, TimeSpan.FromMinutes(2), TimeSpan.FromHours(24));
        }

        public static void Stop()
        {
            lock (Sync)
            {
                if (_timer != null)
                {
                    _timer.Dispose();
                    _timer = null;
                }
                Interlocked.Exchange(ref _started, 0);
            }
        }

        private static void OnTick(object state)
        {
            try
            {
                if (!LicenseServerBootstrap.IsEnabled)
                    return;

                var settings = LicenseServerSettings.Load();
                if (!settings.ActiveStoresEnabled)
                    return;

                LicenseServerBootstrap.SendActiveStoresBestEffort(settings);
            }
            catch (Exception ex)
            {
                try { Trace.WriteLine("[LicenseActiveStores] " + ex.Message); }
                catch { }
            }
        }
    }
}

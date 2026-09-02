using System;
using System.IO;
using System.Web.Hosting;
using Microsoft.Identity.Client;

namespace Linx.Portal.Authentication
{
    /// <summary>
    /// Persistent MSAL token cache. IIS ApplicationPoolIdentity often has no usable
    /// LocalApplicationData, so prefer ~/App_Data then TEMP.
    /// </summary>
    public class FileTokenCacheStore : ITokenCacheStore
    {
        private readonly string _cacheFilePath;
        private readonly object _fileLock = new object();

        public FileTokenCacheStore(string cacheName, string appFolderName = "LinxPortal")
        {
            string dir = null;
            try
            {
                if (HostingEnvironment.IsHosted)
                    dir = HostingEnvironment.MapPath("~/App_Data/AuthCache");
            }
            catch
            {
                dir = null;
            }

            if (string.IsNullOrWhiteSpace(dir))
            {
                string local = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                if (!string.IsNullOrWhiteSpace(local))
                    dir = Path.Combine(local, appFolderName, "AuthCache");
                else
                    dir = Path.Combine(Path.GetTempPath(), appFolderName, "AuthCache");
            }

            Directory.CreateDirectory(dir);
            _cacheFilePath = Path.Combine(dir, cacheName);
        }

        public void RegisterCache(ITokenCache tokenCache)
        {
            tokenCache.SetBeforeAccess(BeforeAccessNotification);
            tokenCache.SetAfterAccess(AfterAccessNotification);
        }

        public void Clear()
        {
            lock (_fileLock)
            {
                if (File.Exists(_cacheFilePath))
                    File.Delete(_cacheFilePath);
            }
        }

        private void BeforeAccessNotification(TokenCacheNotificationArgs args)
        {
            lock (_fileLock)
            {
                args.TokenCache.DeserializeMsalV3(
                    File.Exists(_cacheFilePath) ? File.ReadAllBytes(_cacheFilePath) : null);
            }
        }

        private void AfterAccessNotification(TokenCacheNotificationArgs args)
        {
            if (!args.HasStateChanged)
                return;

            lock (_fileLock)
            {
                File.WriteAllBytes(_cacheFilePath, args.TokenCache.SerializeMsalV3());
            }
        }
    }
}

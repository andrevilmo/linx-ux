using System;
using System.IO;
using Microsoft.Identity.Client;

namespace Linx.Portal.Authentication
{
    /// <summary>
    /// Persistent MSAL token cache under LocalApplicationData (OmniPOS DpapiTokenCacheStore pattern,
    /// without Extensions.Msal — works on IIS/.NET Framework 4.6.1).
    /// </summary>
    public class FileTokenCacheStore : ITokenCacheStore
    {
        private readonly string _cacheFilePath;
        private readonly object _fileLock = new object();

        public FileTokenCacheStore(string cacheName, string appFolderName = "LinxPortal")
        {
            var dir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                appFolderName,
                "AuthCache");

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Linx.Tools;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Linx.Tools
{
    public static class WebCacheHelper
    {
        private enum CacheType { Redis, AppFabric, Memory };

        public const string CacheNameSettings = "CacheName";
      
        // Get data from asp.net Cache
        public static object GetWebCache(string key, string region = null)
        {
            object value = null;

            
            return value;

        }

        public static T GetWebCache<T>(string key)
        {
            return default(T);
        }

        // Add data into asp.net Cache
        public static void AddWebCache(string key, object value, int expirationInHours = 8, string region = null)
        {
            
        }

        public static void RemoveWebCache(string key, string region = null)
        {
           
        }

        public static void UpdateWebCache(string key, object value, int expirationInHours = 8, string region = null)
        {
            
        }

        public static void InvalidateCache(string partialKey, string region = null)
        {
            
        }

        public static void CleanCache(string[] regions = null)
        {
           
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Linx.Tools;
using System.Configuration;
using System.Web;
using System.Collections;
using Microsoft.ApplicationServer.Caching;
using System.ServiceModel.DomainServices.Server;
using System.Threading;
using System.Threading.Tasks;
using StackExchange.Redis;
using Newtonsoft.Json;
using System.Runtime.Caching;

namespace Linx.Tools
{
    public static class WebCacheHelper
    {
        private enum CacheType { Redis, AppFabric, Memory };

        public const string CacheNameSettings = "CacheName";
        private static DataCache SecurityCache;
        private static CacheType _cacheType = CacheType.Memory;

        private static ConnectionMultiplexer connectionMultiplexer;
        private static IDatabase database;
        private static string _cacheName;

        static WebCacheHelper()
        {

            try
            {
                Hashtable config = System.Configuration.ConfigurationManager.GetSection("WebCacheServerSettings") as Hashtable;

                if (config.IsNullOrEmpty())
                    return;

                if (config["CacheType"].ToString().IsNullOrEmpty() || config["CacheType"].ToString() == CacheType.Memory.ToString())
                    return;

                _cacheType = config["CacheType"].ToString() == CacheType.Redis.ToString() ? CacheType.Redis : CacheType.AppFabric;

                string webCacheServer = config["Servers"].ToString();
                _cacheName = (string)config[CacheNameSettings];

                if (_cacheName.IsNullOrEmpty())
                    _cacheName = "LinxUXSecurity";

                if (_cacheType == CacheType.Redis)
                {
                    connectionMultiplexer = ConnectionMultiplexer.Connect(webCacheServer.Replace(";", ","));
                    database = connectionMultiplexer.GetDatabase();
                }
                else
                {
                    if (webCacheServer.IsNullOrEmpty())
                        return;

                    string[] cacheServers = webCacheServer.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                    if (cacheServers.Count() == 0)
                        return;

                    DataCacheServerEndpoint[] servers = new DataCacheServerEndpoint[cacheServers.Count()];

                    for (int i = 0; i < cacheServers.Count(); i++)
                    {
                        servers[i] = new DataCacheServerEndpoint(cacheServers[i].ToString().Extract("", ":"), Convert.ToInt32(cacheServers[i].ToString().Right(":")));
                    }

                    DataCacheFactoryConfiguration factoryConfig = new DataCacheFactoryConfiguration();
                    factoryConfig.Servers = servers;
                    DataCacheFactory cacheFactory = new DataCacheFactory(factoryConfig);
                    SecurityCache = cacheFactory.GetCache(_cacheName);
                }
            }
            catch (DataCacheException cacheException)
            {
                throw new Exception("Erro ao acessar servidor de Cache.\n\n" + cacheException.Message);
            }
            catch (Exception oException)
            {
                throw new Exception("Configuração do servidor de cache incorreta.\n\n" + oException.Message);
            }
        }


        // Get data from asp.net Cache
        public static object GetWebCache(string key, string region = null)
        {
            object value = null;

            switch (_cacheType)
            {
                case CacheType.Redis:
                    key = _cacheName + ':' + key;
                    value = database.KeyExists(key) ? JsonConvert.DeserializeObject(database.StringGet(key)) : null;
                    break;

                case CacheType.AppFabric:
                    if (region.IsNull())
                        value = SecurityCache.Get(key);
                    else
                        value = SecurityCache.Get(key, region);
                    break;

                case CacheType.Memory:
                    if (HttpContext.Current != null && HttpContext.Current.Cache != null)
                        value = HttpContext.Current.Cache.Get(key);
                    else
                        value = MemoryCache.Default.Get(key);
                    break;
            }

            return value;

        }

        public static T GetWebCache<T>(string key)
        {
            if (_cacheType == CacheType.Redis)
            {
                key = _cacheName + ':' + key;
                return (database.KeyExists(key) ? JsonConvert.DeserializeObject<T>(database.StringGet(key)) : default(T));
            }
            else
            {
                return (T)Convert.ChangeType(GetWebCache(key, null), typeof(T));
            }
        }

        // Add data into asp.net Cache
        public static void AddWebCache(string key, object value, int expirationInHours = 8, string region = null)
        {
            switch (_cacheType)
            {
                case CacheType.Redis:
                    key = _cacheName + ':' + key;
                    var serializedObject = JsonConvert.SerializeObject(value);
                    database.StringSet(key, serializedObject, new TimeSpan(expirationInHours, 0, 0));
                    break;

                case CacheType.AppFabric:
                    if (region.IsNull())
                        SecurityCache.Add(key, value, new TimeSpan(expirationInHours, 0, 0));
                    else
                    {
                        SecurityCache.CreateRegion(region);
                        SecurityCache.Add(key, value, new TimeSpan(expirationInHours, 0, 0), region);
                    }
                    break;

                case CacheType.Memory:
                    if (HttpContext.Current != null && HttpContext.Current.Cache != null)
                    {
                        HttpContext.Current.Cache.Add(
                        key,
                        value,
                        null,
                        DateTime.Now.AddHours(expirationInHours),
                        System.Web.Caching.Cache.NoSlidingExpiration,
                        System.Web.Caching.CacheItemPriority.NotRemovable,
                        null);
                    }
                    else
                    {
                        MemoryCache.Default.Set(key, value, DateTimeOffset.Now.AddHours(expirationInHours));
                    }
                    break;
            }
        }

        public static void RemoveWebCache(string key, string region = null)
        {
            switch (_cacheType)
            {
                case CacheType.Redis:
                    key = _cacheName + ':' + key;
                    database.KeyDelete(key);
                    break;

                case CacheType.AppFabric:
                    if (region.IsNull())
                        SecurityCache.Remove(key);
                    else
                        SecurityCache.Remove(key, region);

                    break;

                case CacheType.Memory:
                    if (HttpContext.Current != null && HttpContext.Current.Cache != null)
                        HttpContext.Current.Cache.Remove(key);
                    else
                        MemoryCache.Default.Remove(key);
                    break;
            }
        }

        public static void UpdateWebCache(string key, object value, int expirationInHours = 8, string region = null)
        {
            switch (_cacheType)
            {
                case CacheType.Redis:
                    key = _cacheName + ':' + key;
                    var serializedObject = JsonConvert.SerializeObject(value);
                    database.StringSet(key, serializedObject, new TimeSpan(expirationInHours, 0, 0));
                    break;

                case CacheType.AppFabric:
                    if (region.IsNull())
                        SecurityCache.Put(key, value, new TimeSpan(expirationInHours, 0, 0));
                    else
                        SecurityCache.Put(key, value, new TimeSpan(expirationInHours, 0, 0), region);
                    break;

                case CacheType.Memory:
                    if (HttpContext.Current != null && HttpContext.Current.Cache != null)
                        HttpContext.Current.Cache[key] = value;
                    else
                        MemoryCache.Default.Set(key, value, DateTimeOffset.Now.AddHours(expirationInHours));
                    break;
            }
        }

        public static void InvalidateCache(string partialKey, string region = null)
        {
            switch (_cacheType)
            {
                case CacheType.Redis:
                    foreach (var ep in connectionMultiplexer.GetEndPoints())
                    {
                        partialKey = _cacheName + ':' + partialKey;
                        var server = connectionMultiplexer.GetServer(ep);
                        var keys = server.Keys(database: database.Database, pattern: partialKey + "*").ToArray();
                        database.KeyDeleteAsync(keys);
                    }

                    break;

                case CacheType.AppFabric:
                    if (region.IsNullOrEmpty())
                    {
                        foreach (string systemRegion in SecurityCache.GetSystemRegions())
                        {
                            var keys = SecurityCache.GetObjectsInRegion(systemRegion).Where(i => i.Key.StartsWith(partialKey)).ToList();

                            Parallel.ForEach(keys, i =>
                            {
                                SecurityCache.Remove(i.Key);
                            });
                        }
                    }
                    else
                        SecurityCache.RemoveRegion(region);

                    break;

                case CacheType.Memory:
                    if (HttpContext.Current != null && HttpContext.Current.Cache != null)
                    {
                        var keys = (from System.Collections.DictionaryEntry dict in HttpContext.Current.Cache
                                    let key = dict.Key.ToString()
                                    where key.StartsWith(partialKey)
                                    select key).ToList();

                        System.Web.Caching.Cache cache = HttpContext.Current.Cache;

                        System.Threading.Tasks.Parallel.ForEach(keys, i =>
                        {
                            cache.Remove(i);
                        });
                    }
                    else
                    {
                        var keys = MemoryCache.Default.Where(i => i.Key.StartsWith(partialKey)).Select(i => i.Key).ToList();
                        System.Threading.Tasks.Parallel.ForEach(keys, i =>
                        {
                            MemoryCache.Default.Remove(i);
                        });
                    }
                    break;
            }
        }

        public static void CleanCache(string[] regions = null)
        {
            switch (_cacheType)
            {
                case CacheType.Redis:
                    foreach (var ep in connectionMultiplexer.GetEndPoints())
                    {
                        var pattern = _cacheName + ":*";
                        var server = connectionMultiplexer.GetServer(ep);
                        var keys = server.Keys(database: database.Database, pattern: pattern).ToArray();
                        database.KeyDeleteAsync(keys);
                    }

                    break;

                case CacheType.AppFabric:
                    //Default Regions
                    Parallel.ForEach(SecurityCache.GetSystemRegions(), region =>
                    {
                        SecurityCache.ClearRegion(region);
                    });

                    //Named Regions
                    if (!regions.IsNull())
                    {
                        foreach (string regionName in regions)
                        {
                            SecurityCache.RemoveRegion(regionName);
                        }
                    }
                    break;

                case CacheType.Memory:
                    if (HttpContext.Current != null && HttpContext.Current.Cache != null)
                    {
                        var keys = (from System.Collections.DictionaryEntry dict in HttpContext.Current.Cache
                                    let key = dict.Key.ToString()
                                    select key).ToList();

                        System.Web.Caching.Cache cache = HttpContext.Current.Cache;

                        System.Threading.Tasks.Parallel.ForEach(keys, i =>
                        {
                            cache.Remove(i);
                        });
                    }
                    else
                    {
                        var keys = MemoryCache.Default.Select(i => i.Key).ToList();
                        System.Threading.Tasks.Parallel.ForEach(keys, i =>
                        {
                            MemoryCache.Default.Remove(i);
                        });
                    }
                    break;
            }
        }
    }
}

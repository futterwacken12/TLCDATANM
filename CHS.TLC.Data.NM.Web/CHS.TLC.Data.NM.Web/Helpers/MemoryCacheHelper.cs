using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;

namespace CHS.TLC.Data.NM.Web.Helpers
{
    public static class MemoryCacheHelper
    {
        public static bool Add<T>(T o, string key, Int32 expirationMinutes = 60) where T : class
        {
            var cacheTime = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["CacheExpirationMinutes"]);
            var expiratioTime = new DateTimeOffset(DateTime.Now.AddMinutes(cacheTime == 0 ? expirationMinutes : cacheTime));

            var memoryCache = MemoryCache.Default;
            return memoryCache.Add(key, o, expiratioTime);
        }
        public static bool Add(Object o, string key, Int32 expirationMinutes = 60)
        {
            var cacheTime = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["CacheExpirationMinutes"]);
            var expiratioTime = new DateTimeOffset(DateTime.Now.AddMinutes(cacheTime == 0 ? expirationMinutes : cacheTime));

            var memoryCache = MemoryCache.Default;
            return memoryCache.Add(key, o, expiratioTime);
        }
        public static bool Exists(string key)
        {
            var memoryCache = MemoryCache.Default;
            return memoryCache.Get(key) != null;
        }

        public static void Clear(string key)
        {
            var memoryCache = MemoryCache.Default;
            if (memoryCache.Contains(key))
            {
                memoryCache.Remove(key);
            }
        }

        public static void Replace<T>(T o, string key) where T : class
        {
            Clear(key);
            Add<T>(o, key);
        }

        public static void Replace(Object o, string key)
        {
            Clear(key);
            Add(o, key);
        }
        public static object Get<T>(string key)
        {
            try
            {
                var memoryCache = MemoryCache.Default;
                return (T)memoryCache.Get(key);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
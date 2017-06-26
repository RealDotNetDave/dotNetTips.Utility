using Microsoft.Extensions.Caching.Memory;

namespace dotNetTips.Utility.Standard.Cache
{
    public class InMemoryCache : ISingleton<InMemoryCache>
    {
        private MemoryCache _cache;
        private InMemoryCache _instance;

        public InMemoryCache Instance()
        {
            if (this._instance == null)
            {
                this._instance = new InMemoryCache();
            }

            return this._instance;
        }

        protected InMemoryCache()
        {
            var options = new MemoryCacheOptions() { CompactOnMemoryPressure = true, ExpirationScanFrequency = new System.TimeSpan(0, 5, 0) };
            
            this._cache = new MemoryCache(options);
        }

        public void GetOrCreate(string key)
        {
           // _cache.GetOrCreate(key, new System.Func<ICacheEntry, TItem>() {  })
        }
    }
}

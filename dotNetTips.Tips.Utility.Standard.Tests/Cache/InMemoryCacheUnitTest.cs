using dotNetTips.Utility.Standard.Cache;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace dotNetTips.Tips.Utility.Standard.Tests.Cache
{
    [TestClass]
    public class InMemoryCacheUnitTest
    {
        [TestMethod]
        public void AddItemsToCache()
        {
            var cache = InMemoryCache.Instance;

            for (int i = 0; i < 100; i++)
            {
                cache.AddCacheItem<int>(i.ToString(), i);
            }

            Assert.IsTrue(cache.Count == 100);

        }
    }
}

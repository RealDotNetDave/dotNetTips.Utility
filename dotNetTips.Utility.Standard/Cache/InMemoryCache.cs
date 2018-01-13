// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard
// Author           : David McCarter
// Created          : 06-26-2017
//
// Last Modified By : David McCarter
// Last Modified On : 11-01-2017
// ***********************************************************************
// <copyright file="InMemoryCache.cs" company="dotNetTips.com - David McCarter">
//     dotNetTips.com - David McCarter
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using dotNetTips.Utility.Standard.Extensions;
using Microsoft.Extensions.Caching.Memory;

namespace dotNetTips.Utility.Standard.Cache
{
    /// <summary>
    /// Class InMemoryCache.
    /// </summary>
    /// <seealso cref="dotNetTips.Utility.Standard.Cache.InMemoryCache" />
    /// <seealso cref="System.IDisposable" />
    public class InMemoryCache : ISingleton<InMemoryCache>, IDisposable
    {
        /// <summary>
        /// The disposed
        /// </summary>
        protected bool disposed;

        /// <summary>
        /// The cache
        /// </summary>
        private MemoryCache _cache;

        /// <summary>
        /// The instance
        /// </summary>
        private InMemoryCache _instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryCache" /> class. Sets ExpirationScanFrequency to 5 minutes.
        /// </summary>
        protected InMemoryCache() : this(new TimeSpan(0, 5, 0))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryCache" /> class.
        /// </summary>
        /// <param name="expirationScanFrequency">The expiration scan frequency.</param>
        protected InMemoryCache(TimeSpan expirationScanFrequency)
        {
            var options = new MemoryCacheOptions() { ExpirationScanFrequency = expirationScanFrequency };

            this._cache = new MemoryCache(options);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Returns the instance.
        /// </summary>
        /// <returns>T.</returns>
        private InMemoryCache GetInstance()
        {
            if (this._instance == null)
            {
                this._instance = new InMemoryCache();
            }

            return this._instance;
        }

        /// <summary>
        /// Returns instance for the object.
        /// </summary>
        /// <returns>T.</returns>
        public InMemoryCache Instance()
        {
            return this.GetInstance();
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            lock (this)
            {
                if (this.disposed)
                {
                    return;
                }

                if (disposing)
                {
                    this.DisposeFields();
                }

                this.disposed = true;
            }
        }
    }
}

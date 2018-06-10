// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard
// Author           : David McCarter
// Created          : 04-02-2018
//
// Last Modified By : David McCarter
// Last Modified On : 04-02-2018
// ***********************************************************************
// <copyright file="ServiceProxy.cs" company="dotNetTips.com - David McCarter">
//     dotNetTips.com - David McCarter
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace dotNetTips.Utility.Standard.Web
{
    /// <summary>
    /// Class ServiceProxy.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="System.IDisposable" />
    public abstract class ServiceProxy<T>
        where T : ICommunicationObject, IDisposable
    {
        /// <summary>
        /// The channel
        /// </summary>
        private T _channel;

        /// <summary>
        /// The channel factory
        /// </summary>
        private IChannelFactory<T> _channelFactory;

        /// <summary>
        /// The lock
        /// </summary>
        private readonly object _lock = new object();

        /// <summary>
        /// The service endpoint
        /// </summary>
        private readonly string _serviceEndpoint;
        /// <summary>
        /// The disposed
        /// </summary>
        protected bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceProxy{T}" /> class.
        /// </summary>
        /// <param name="serviceEndpoint">The service endpoint.</param>
        protected ServiceProxy(string serviceEndpoint) => _serviceEndpoint = serviceEndpoint;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            lock(_lock)
            {
                if(Channel != null)
                {
                    return;
                }

                _channelFactory = new ChannelFactory<T>(_serviceEndpoint);
                Channel = _channelFactory.CreateChannel(new EndpointAddress(_serviceEndpoint));
            }
        }

        /// <summary>
        /// Closes the channel.
        /// </summary>
        protected void CloseChannel()
        {
            if(Channel != null)
            {
                ((ICommunicationObject)Channel).Close();
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            // Do nothing if the object has already been disposed of.
            if(disposed)
            {
                return;
            }

            if (disposing)
            {
                lock(_lock)
                {
                    // Release disposable objects used by this instance here.
                    if(Channel != null)
                    {
                        Channel.Dispose();
                    }
                }
            }

            // Release unmanaged resources here. Don't access reference type fields.

            // Remember that the object has been disposed of.
            disposed = true;
        }

        /// <summary>
        /// Gets the channel.
        /// </summary>
        /// <value>The channel.</value>
        protected T Channel
        {
            get
            {
                Initialize();
                return _channel;
            }
            private set => _channel = value;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public virtual void Dispose()
        {
            Dispose(true);
            // Unregister object for finalization.
            GC.SuppressFinalize(this);
        }
    }
}

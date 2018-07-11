// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard
// Author           : David McCarter
// Created          : 07-09-2018
//
// Last Modified By : David McCarter
// Last Modified On : 07-10-2018
// ***********************************************************************
// <copyright file="SystemEvents.cs" company="dotNetTips.com - David McCarter">
//     McCarter Consulting (David McCarter)
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;

namespace dotNetTips.Utility.Standard
{
    /// <summary>
    /// Enum SystemEventType
    /// </summary>
    public enum SystemEventType
    {
        /// <summary>
        /// The none
        /// </summary>
        None,

        /// <summary>
        /// The network address changed
        /// </summary>
        NetworkAddressChanged,

        /// <summary>
        /// The network available changed
        /// </summary>
        NetworkAvailableChanged,

        /// <summary>
        /// The unhandled exception
        /// </summary>
        UnhandledException
    }

    /// <summary>
    /// Interface IEventInformation
    /// </summary>
    public interface IEventInformation
    {
    }

    /// <summary>
    /// Class NetworkAddressChangedEventInformation.
    /// </summary>
    /// <seealso cref="dotNetTips.Utility.Standard.IEventInformation" />
    public sealed class NetworkAddressChangedEventInformation : IEventInformation
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; internal set; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string Id { get; internal set; }

        /// <summary>
        /// Gets the operational status.
        /// </summary>
        /// <value>The operational status.</value>
        public OperationalStatus OperationalStatus { get; internal set; }
    }

    /// <summary>
    /// Class NetworkAvailabilityChangedEventInformation.
    /// </summary>
    /// <seealso cref="dotNetTips.Utility.Standard.IEventInformation" />
    public sealed class NetworkAvailabilityChangedEventInformation : IEventInformation
    {
        /// <summary>
        /// Gets a value indicating whether [network available].
        /// </summary>
        /// <value><c>true</c> if [network available]; otherwise, <c>false</c>.</value>
        public bool NetworkAvailable { get; internal set; }
    }

    /// <summary>
    /// Class SystemEvents.
    /// </summary>
    public static class SystemEvents
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SystemEvents" /> class.
        /// </summary>
        static SystemEvents()
        {
            NetworkChange.NetworkAddressChanged += new NetworkAddressChangedEventHandler(NetworkAddressChangedCallback);
            NetworkChange.NetworkAvailabilityChanged += NetworkChange_NetworkAvailabilityChanged;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException;
        }

        private static void CurrentDomain_FirstChanceException(object sender, System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Class UnhandledExceptionEventInformation.
        /// </summary>
        /// <seealso cref="dotNetTips.Utility.Standard.IEventInformation" />
        public sealed class UnhandledExceptionEventInformation : IEventInformation
        {
            /// <summary>
            /// Gets a value indicating whether [network available].
            /// </summary>
            /// <value><c>true</c> if [network available]; otherwise, <c>false</c>.</value>
            public bool IsTerminating { get; internal set; }

            /// <summary>
            /// Gets the exception.
            /// </summary>
            /// <value>The exception.</value>
            public LoggableException Exception { get; internal set; }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var eventArgs = new SystemChangedEventArgs { SystemEventType = SystemEventType.UnhandledException };

            var eventInfo = new UnhandledExceptionEventInformation { IsTerminating = e.IsTerminating, Exception = e.ExceptionObject as LoggableException };

            eventArgs.EventInformation = new UnhandledExceptionEventInformation[] { eventInfo };

            OnSystemChanged(eventArgs);
        }

        /// <summary>
        /// Handles the NetworkAvailabilityChanged event of the NetworkChange control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="NetworkAvailabilityEventArgs"/> instance containing the event data.</param>
        private static void NetworkChange_NetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            var eventInfo = new List<NetworkAvailabilityChangedEventInformation>(1)
            {
                new NetworkAvailabilityChangedEventInformation { NetworkAvailable = e.IsAvailable }
            };

            Trace.Write($"Network availble: {e.IsAvailable}");

            var eventArgs = new SystemChangedEventArgs { SystemEventType = SystemEventType.NetworkAvailableChanged, EventInformation = eventInfo };

            OnSystemChanged(eventArgs);
        }

        /// <summary>
        /// Networks the address changed callback.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private static void NetworkAddressChangedCallback(object sender, EventArgs e)
        {
            var adapters = NetworkInterface.GetAllNetworkInterfaces();

            var eventInfo = new List<NetworkAddressChangedEventInformation>(adapters.Count());

            foreach (NetworkInterface adapter in adapters)
            {
                var networkEvent = new NetworkAddressChangedEventInformation();
                networkEvent.Id = adapter.Id;
                networkEvent.Name = adapter.Name;
                networkEvent.OperationalStatus = adapter.OperationalStatus;

                eventInfo.Add(networkEvent);
            }

            eventInfo.TrimExcess();

            var eventArgs = new SystemChangedEventArgs()
            {
                SystemEventType = SystemEventType.NetworkAddressChanged,
                EventInformation = eventInfo
            };

            OnSystemChanged(eventArgs);
        }

        /// <summary>
        /// Occurs when [system changed].
        /// </summary>
        public static event EventHandler<SystemChangedEventArgs> SystemChanged;

        /// <summary>
        /// Handles the <see cref="E:SystemChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="SystemChangedEventArgs" /> instance containing the event data.</param>
        private static void OnSystemChanged(SystemChangedEventArgs e) => SystemChanged?.Invoke(AppDomain.CurrentDomain, e);

        /// <summary>
        /// Class SystemChangedEventArgs. This class cannot be inherited.
        /// </summary>
        /// <seealso cref="System.EventArgs" />
        public sealed class SystemChangedEventArgs : EventArgs
        {
            /// <summary>
            /// Gets the type of the system event.
            /// </summary>
            /// <value>The type of the system event.</value>
            public SystemEventType SystemEventType { get; internal set; }

            /// <summary>
            /// Gets the event information.
            /// </summary>
            /// <value>The event information.</value>
            public IEnumerable<IEventInformation> EventInformation { get; internal set; }
        }
    }
}
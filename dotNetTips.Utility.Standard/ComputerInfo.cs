// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard
// Author           : David McCarter
// Created          : 10-15-2018
//
// Last Modified By : David McCarter
// Last Modified On : 11-24-2018
// ***********************************************************************
// <copyright file="ComputerInfo.cs" company="dotNetTips.com - McCarter Consulting">
//     dotNetTips.com - McCarter Consulting. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using dotNetTips.Utility.Standard.Extensions;

namespace dotNetTips.Utility.Standard
{
    /// <summary>
    /// Class ComputerInfo.
    /// </summary>
    public class ComputerInfo
    {
        /// <summary>
        /// Gets the name of the machine.
        /// </summary>
        /// <value>The name of the machine.</value>
        public string MachineName { get; } = Environment.MachineName;

        /// <summary>
        /// Gets the ip address.
        /// </summary>
        /// <value>The ip address.</value>
        public string IPAddress { get; } = Dns.GetHostAddresses(Dns.GetHostName()).Where(p => p.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToList().ToDelimitedString(char.Parse(","));

        /// <summary>
        /// Gets the os version.
        /// </summary>
        /// <value>The os version.</value>
        public string OSDescription { get; } =  App.OSDescription;

        /// <summary>
        /// Gets the processor count.
        /// </summary>
        /// <value>The processor count.</value>
        public int ProcessorCount { get; } = Environment.ProcessorCount;


    }
}

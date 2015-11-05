// ***********************************************************************
// Assembly         : dotNetTips.Utility.Portable
// Author           : davidmccarter
// Created          : 12-31-2014
//
// Last Modified By : davidmccarter
// Last Modified On : 12-31-2014
// ***********************************************************************
// <copyright file="LoggableException.cs" company="McCarter Consulting">
//     . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace dotNetTips.Utility.Portable.Logger
{
    /// <summary>
    /// Exception meant to be logged.
    /// </summary>
    public class LoggableException : Exception, System.Xml.Serialization.IXmlSerializable
    {
        /// <summary>
        /// Gets or sets the user message.	
        /// </summary>
        /// <returns></returns>
        /// <value>The user message.</value>
        /// <remarks></remarks>
        public virtual string UserMessage { get; set; }

        public virtual IDictionary<LogItem, string> Messages
        {
            get
            {
                var exceptions = this.AllExceptions(this);
                var errorMessages = new Dictionary<LogItem, string>();

                var enumerator = exceptions.GetEnumerator();
                foreach (var current in exceptions)
                {
                    var exceptionLevel = (errorMessages.Count == 0) ? LogItem.Exception : LogItem.InnerException;

                    errorMessages.Add(LogItem.ExceptionType, current.GetType().FullName);
                    errorMessages.Add(LogItem.Properties, ReflectException(current));

                    if (current.StackTrace != null)
                    {
                        errorMessages.Add(LogItem.StackTrace, current.StackTrace);
                    }
                }

                return errorMessages;
            }

        }

        private IEnumerable<Exception> AllExceptions(Exception ex)
        {
            List<Exception> collection = null;

            if (ex != null)
            {
                collection = new List<Exception> { ex };
                if (ex.InnerException != null)
                {
                    collection.AddRange(this.AllExceptions(ex.InnerException));
                }
            }

            return collection.AsEnumerable<Exception>();
        }

        private static string ReflectException(Exception ex)
        {
            var sb = new StringBuilder();

            foreach (var current in ex.GetType().GetRuntimeProperties())
            {
                object objectValue = null;
                try
                {
                    objectValue = RuntimeHelpers.GetObjectValue(current.GetValue(ex, null));
                }
                catch (SecurityException securityEx)
                {
                    ProjectData.SetProjectError(securityEx);
                    SecurityException exception = securityEx;
                    objectValue = null;
                    ProjectData.ClearProjectError();
                }
                if ((objectValue != null) && (objectValue.ToString() != objectValue.GetType().FullName))
                {
                    sb.AppendLine(string.Format(CultureInfo.CurrentCulture, "{0}: {1}", new object[] { current.Name, RuntimeHelpers.GetObjectValue(current) }));
                }
            }

            return sb.ToString();
        }

        public LoggableException(string message, Exception ex, string userMessage) : base(message, ex)
        {
            UserMessage = userMessage;
        }

        XmlSchema IXmlSerializable.GetSchema()
        {
            throw new NotImplementedException();
        }

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            throw new NotImplementedException();
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            throw new NotImplementedException();
        }
    }

    public enum LogItem
    {
        ExceptionType,
        Exception,
        InnerException,
        StackTrace,
        Properties
    }
}

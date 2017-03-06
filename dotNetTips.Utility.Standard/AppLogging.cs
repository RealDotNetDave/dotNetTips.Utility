using Microsoft.Extensions.Logging;
using System;

namespace dotNetTips.Utility.Standard
{
    public static class AppLogging
    {
        public static ILogger Logger { get; private set; }
        public static ILoggerFactory LoggerFactory { get; } = new LoggerFactory();
        public static ILogger CreateLogger<T>()
        {
            Logger = LoggerFactory.CreateLogger<T>();

            return Logger;
        }
    }
}

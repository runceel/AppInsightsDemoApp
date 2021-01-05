using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;

namespace AppInsightsDemoApp.Logging
{
    public static class LoggerExtensions
    {
        private static readonly object Empty = new object();
        public static IDisposable LogMethod(this ILogger self, IDictionary<string, string> properties = null, [CallerMemberName]string methodName = null)
        {
            var propertiesJson = JsonSerializer.Serialize(properties ?? Empty);
            self.LogInformation("{0} started. properties: {1}", methodName, propertiesJson);
            return new LogScope(self, methodName);
        }

        class LogScope : IDisposable
        {
            private readonly ILogger _logger;
            private readonly string _methodName;
            private readonly Stopwatch _stopwatch;

            public LogScope(ILogger logger, string methodName)
            {
                _logger = logger;
                _methodName = methodName;
                _stopwatch = Stopwatch.StartNew();
            }

            public void Dispose()
            {
                _stopwatch.Stop();
                _logger.LogInformation("{methodName} end. ({elapsed}ms)", _methodName, _stopwatch.ElapsedMilliseconds);
            }
        }
    }
}

using AppInsightsDemoApp.Utils;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AppInsightsDemoApp.RedisClients
{
    public interface IRedisClient
    {
        Task<bool> ContainsKeyAsync(string key);
        Task<string> StringGetAsync(string key);
        Task StringSetAsync(string key, string value);
    }

    public class RedisClient : IRedisClient, IDisposable
    {
        private AsyncLazy<ConnectionMultiplexer> _connection;
        private readonly TelemetryClient _telemetryClient;

        public RedisClient(string connectionString, TelemetryClient telemetryClient)
        {
            _connection = new AsyncLazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.ConnectAsync(connectionString));
            _telemetryClient = telemetryClient;
        }

        public Task StringSetAsync(string key, string value) => Track<object>(key, async () =>
        {
            var cache = await GetDatabaseAsync();
            cache.StringSet(key, value);
            return null;
        });

        public Task<string> StringGetAsync(string key) => Track<string>(key, async () =>
        {
            var cache = await GetDatabaseAsync();
            return await cache.StringGetAsync(key);
        });

        public Task<bool> ContainsKeyAsync(string key) => Track(key, async () =>
        {
            var cache = await GetDatabaseAsync();
            return cache.KeyExists(key);
        });

        private async Task<T> Track<T>(string key, Func<Task<T>> f, [CallerMemberName]string methodName = null)
        {
            var startTime = DateTime.UtcNow;
            var stopwatch = Stopwatch.StartNew();
            try
            {
                var res = await f();
                _telemetryClient?.TrackDependency("Azure Cache for Redis",
                    "Redis",
                    $"{methodName}({key}) => {res}",
                    startTime,
                    stopwatch.Elapsed,
                    true);
                return res;
            }
            catch (Exception ex)
            {
                _telemetryClient?.TrackDependency("Azure Cache for Redis",
                    "Redis",
                    $"{methodName}({key}) => {ex}",
                    startTime,
                    stopwatch.Elapsed,
                    false);
                throw;
            }
        }

        private async Task<IDatabase> GetDatabaseAsync() => (await _connection).GetDatabase();

        public void Dispose() => _connection.Value.Result.Dispose();
    }
}

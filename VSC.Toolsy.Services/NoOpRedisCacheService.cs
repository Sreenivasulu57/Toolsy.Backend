using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VSC.Toolsy.Common.Interfaces;

namespace VSC.Toolsy.Services
{
    public class NoOpRedisCacheService : IRedisCacheService
    {
        public Task SetCacheValueAsync<T>(string key, T value, TimeSpan expiration)
        {
            // No-op: does nothing
            return Task.CompletedTask;
        }

        public Task<T?> GetCacheValueAsync<T>(string key)
        {
            // No-op: always returns default value
            return Task.FromResult<T?>(default);
        }
    }
}

using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VSC.Toolsy.Common.Interfaces;

namespace VSC.Toolsy.Services
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IConnectionMultiplexer _redis;

        public RedisCacheService(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public async Task SetCacheValueAsync<T>(string key, T value, TimeSpan expiration)
        {
            var db = _redis.GetDatabase();
            var json = JsonConvert.SerializeObject(value);
            await db.StringSetAsync(key, json, expiration);
        }

        public async Task<T?> GetCacheValueAsync<T>(string key)
        {
            IDatabase db = _redis.GetDatabase();
            RedisValue json = await db.StringGetAsync(key);
            return json.HasValue ? JsonConvert.DeserializeObject<T>(json) : default;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSC.Toolsy.Common.Interfaces
{
    public interface IRedisCacheService
    {
        Task SetCacheValueAsync<T>(string key, T value, TimeSpan expiration);
        Task<T?> GetCacheValueAsync<T>(string key);
    }
}

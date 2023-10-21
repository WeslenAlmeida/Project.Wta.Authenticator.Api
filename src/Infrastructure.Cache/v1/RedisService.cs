using CrossCutting.Configuration;
using Domain.Interfaces.v1;
using Microsoft.Extensions.Caching.Distributed;

namespace Infrastructure.Cache.v1
{
    public class RedisService : IRedisService
    {
        private readonly IDistributedCache _cache;
        private readonly DistributedCacheEntryOptions _options;
        public RedisService(IDistributedCache cache)
        {
            _cache = cache;
            _options = new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromSeconds(Double.Parse(AppSettings.RedisSettings.TimeExpiration!)),
            };
        }
        public async Task<string> GetAsync(string key)
        {
            return await _cache.GetStringAsync(key)!;
        }

        public async Task SetAsync(string key, string value)
        {
            await _cache.SetStringAsync(key.ToString(), value,_options);
        }
    }
}
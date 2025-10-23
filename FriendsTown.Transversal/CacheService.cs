using Microsoft.Extensions.Caching.Memory;

namespace FriendsTown.Transversal
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        private readonly int _defaultExpirationInMinutes=20;
        
        public CacheService (IMemoryCache cache)
        {
            _cache = cache;
        }

        public T? Get<T>(string key)
        {
            _cache.TryGetValue(key, out T? value);
            return value;
        }

        public void Set<T>(string key, T value, 
                TimeSpan? expirationInMinutes = null)
        {
            _cache.Set(key, value, expirationInMinutes ?? 
                TimeSpan.FromMinutes(_defaultExpirationInMinutes));
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }
    }
}

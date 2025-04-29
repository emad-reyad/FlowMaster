using Microsoft.Extensions.Caching.Memory;

namespace WorkFlowEngine.Shared.Cashing
{
    public class CashService : ICashService
    {
        private readonly IMemoryCache _memoryCache;
        public CashService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public T GetCash<T>(string key)
        {
            T data;
            if (_memoryCache.TryGetValue(key, out data))
                return data;
            else
                return default;
        }

        public bool RemoveCash(string key)
        {
            if (string.IsNullOrEmpty(key)) return false;
            _memoryCache.Remove(key);
            return true;
        }

        public bool SetCash<T>(string key, T value, DateTimeOffset expirationDate)
        {
            if (string.IsNullOrEmpty(key))
                return false;
            _memoryCache.Set(key, value, expirationDate);
            return true;
        }
    }
}

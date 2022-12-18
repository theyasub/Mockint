using Microsoft.Extensions.Caching.Memory;

namespace AbuInt.Service.Helpers;

public class CacheService : ICacheService
{
    private readonly IMemoryCache memoryCache;

    public CacheService(IMemoryCache memoryCache)
    {
        this.memoryCache = memoryCache;
    }

    public T GetData<T>(string key)
    {
        try
        {
            T item = (T)memoryCache.Get(key);
            return item;
        }
        catch (Exception e)
        {
            throw;
        }
    }
    public bool SetData<T>(string key, T value, TimeSpan timeSpan)
    {
        bool res = true;
        try
        {
            if (!string.IsNullOrEmpty(key))
            {
                memoryCache.Set(key, value, timeSpan);
            }
        }
        catch (Exception e)
        {
            throw;
        }
        return res;
    }
}

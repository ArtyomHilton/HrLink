using System.Text.Json;
using HrLink.Application.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace HrLink.Caching.Services;

public class RedisCacheService : ICacheService
{
    private readonly IDistributedCache _distributedCache;

    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        WriteIndented = true
    };

    private readonly DistributedCacheEntryOptions _distributedCacheEntryOptions = new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
    };

    public RedisCacheService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken)
    {
        var cache = await _distributedCache.GetStringAsync(key, cancellationToken);

        return string.IsNullOrEmpty(cache)
            ? default(T)
            : JsonSerializer.Deserialize<T>(cache, _jsonSerializerOptions);
    }

    public async Task SetAsync<T>(string key, T value, CancellationToken cancellationToken)
    {
        var stringValue = JsonSerializer.Serialize(value, _jsonSerializerOptions);

        await _distributedCache.SetStringAsync(key, stringValue, _distributedCacheEntryOptions, cancellationToken);
    }

    public async Task RemoveAsync(string key, CancellationToken cancelationToken) =>
        await _distributedCache.RemoveAsync(key, cancelationToken);
}
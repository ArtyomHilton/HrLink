using System.Text.Json;
using HrLink.Application.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace HrLink.Caching.Services;

/// <summary>
/// <inheritdoc cref="ICacheService"/> через Redis.
/// </summary>
public class RedisCacheService : ICacheService
{
    private readonly IDistributedCache _distributedCache;

    /// <summary>
    /// <see cref="JsonSerializerOptions"/>
    /// </summary>
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        WriteIndented = true
    };
    
    /// <summary>
    /// <see cref="DistributedCacheEntryOptions"/>
    /// </summary>
    private readonly DistributedCacheEntryOptions _distributedCacheEntryOptions = new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
    };

    public RedisCacheService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    /// <inheritdoc />
    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken)
    {
        var cache = await _distributedCache.GetStringAsync(key, cancellationToken);

        return string.IsNullOrEmpty(cache)
            ? default(T)
            : JsonSerializer.Deserialize<T>(cache, _jsonSerializerOptions);
    }

    /// <inheritdoc />
    public async Task SetAsync<T>(string key, T value, CancellationToken cancellationToken)
    {
        var stringValue = JsonSerializer.Serialize(value, _jsonSerializerOptions);
        
        await _distributedCache.SetStringAsync(key, stringValue, _distributedCacheEntryOptions, cancellationToken);
    }

    /// <inheritdoc />
    public async Task RemoveAsync(string key, CancellationToken cancellationToken) =>
        await _distributedCache.RemoveAsync(key, cancellationToken);
}
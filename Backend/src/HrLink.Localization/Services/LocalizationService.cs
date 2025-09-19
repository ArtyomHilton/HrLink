using System.Collections.Concurrent;
using System.Reflection;
using System.Text.Json;
using HrLink.Application.Interfaces;

namespace HrLink.Localization.Services;

public class LocalizationService : ILocalizationService
{
    public LocalizationService()
    {
    }

    private readonly ConcurrentDictionary<string, string> _cacheLocalization = new ConcurrentDictionary<string, string>();

    private readonly string _localizationKey = "{0}_{1}";

    public async Task<string?> GetLocalizationMessage(string messageCode, string culture, CancellationToken cancellationToken)
    {
        var key = string.Format(_localizationKey, messageCode, culture);
        _cacheLocalization.TryGetValue(key, out var message);

        if (string.IsNullOrEmpty(message))
        {
            var json = await ReadLocalizationFile(culture, cancellationToken);
            message = JsonSerializer.Deserialize<Dictionary<string, string>>(json!)?[messageCode] ?? null;
        }

        if (!string.IsNullOrEmpty(message))
        {
            _cacheLocalization.TryAdd(key, message);
        }
        
        return message;
    }

    private async Task<string?> ReadLocalizationFile(string culture, CancellationToken cancellationToken)
    {
        var name = Assembly.GetExecutingAssembly().GetName().Name;

        await using var stream = Assembly.GetExecutingAssembly()
            .GetManifestResourceStream($"{name}.Resources.localization-{culture}.json")!;

        using var streamReader = new StreamReader(stream);
        return await streamReader.ReadToEndAsync(cancellationToken);
    }
}
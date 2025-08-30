namespace HrLink.Application.Interfaces;

/// <summary>
/// Сервис кэширования
/// </summary>
public interface ICacheService
{
    /// <summary>
    /// Получает по <paramref name="key"/> данные из хранилища.
    /// </summary>
    /// <param name="key">Ключ.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <typeparam name="T"><see cref="Object"/></typeparam>
    /// <returns>Полученные данные или default</returns>
    Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken);
    
    /// <summary>
    /// Добавляет <paramref name="value"/> в хранилище с ключём <paramref name="key"/>.
    /// </summary>
    /// <param name="key">Ключ.</param>
    /// <param name="value">Значение.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <typeparam name="T"><see cref="Object"/></typeparam>
    Task SetAsync<T>(string key, T value, CancellationToken cancellationToken);
    
    /// <summary>
    /// Удаление значения из хранилища по <paramref name="key"/>.
    /// </summary>
    /// <param name="key">Ключ.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    Task RemoveAsync(string key, CancellationToken cancellationToken);
}
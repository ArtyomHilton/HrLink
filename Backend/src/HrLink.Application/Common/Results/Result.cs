using HrLink.Application.Common.Results.Errors;

namespace HrLink.Application.Common.Results;

/// <summary>
/// Результат.
/// </summary>
public class Result
{
    /// <summary>
    /// Ошибка.
    /// </summary>
    public IError? Error { get; init; }
    
    /// <summary>
    /// Успешно.
    /// </summary>
    public bool IsSuccess { get; init; }

    /// <summary>
    /// Неуспешно.
    /// </summary>
    public bool IsFailure
        => !IsSuccess;

    protected Result(IError? error, bool result)
    {
        Error = error;
        IsSuccess = result;
    }

    /// <summary>
    /// Создает объект типа <see cref="Result"/> c успешным результатом.
    /// </summary>
    /// <returns>Объект <see cref="Result"/>.</returns>
    public static Result Success() =>
        new Result(null, true);

    /// <summary>
    /// Создает объект типа <see cref="Result"/> c не успешным результатом.
    /// </summary>
    /// <param name="error">Ошибка <see cref="IError"/>.</param>
    /// <returns>Объект <see cref="Result"/>.</returns>
    public static Result Failure(IError error) =>
        new Result(error, false);

    /// <summary>
    /// Создает объект типа <see cref="Result{T}"/> c успешным результатом и значением <paramref name="value"/>.
    /// </summary>
    /// <param name="value">Значение.</param>
    /// <typeparam name="T"><see cref="Object"/></typeparam>
    /// <returns>Объект <see cref="Result{T}"/></returns>
    public static Result<T> Success<T>(T value) =>
        new Result<T>(value, null, true);

    /// <summary>
    /// Создает объект типа <see cref="Result{T}"/>
    /// c неуспешным результатом, значением <paramref name="value"/>
    /// и ошибкой <paramref name="error"/>.
    /// </summary>
    /// <param name="value">Значение.</param>
    /// <param name="error">Ошибка <see cref="IError"/>.</param>
    /// <typeparam name="T"><see cref="Object"/></typeparam>
    /// <returns>Объект <see cref="Result{T}"/></returns>
    public static Result<T> Failure<T>(T value, IError error) =>
        new Result<T>(value, error, false);
}

/// <summary>
/// Результат со значением.
/// </summary>
/// <typeparam name="T"><see cref="Object"/></typeparam>
public class Result<T> : Result
{
    /// <summary>
    /// Значение.
    /// </summary>
    public T? Value { get; init; }

    protected internal Result(T? value, IError? error, bool result) : base(error, result)
    {
        Value = value;
    }
}
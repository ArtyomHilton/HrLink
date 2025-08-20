using HrLink.Application.Common.Results.Errors;

namespace HrLink.Application.Common.Results;

public class Result
{
    public IError? Error { get; init; }
    public bool IsSuccess { get; init; }

    public bool IsFailure
        => !IsSuccess;

    protected Result(IError? error, bool result)
    {
        Error = error;
        IsSuccess = result;
    }

    public static Result Success() =>
        new Result(null, true);

    public static Result Failure(IError error) =>
        new Result(error, false);

    public static Result<T> Success<T>(T value) =>
        new Result<T>(value, null, true);

    public static Result<T> Failure<T>(T value, IError error) =>
        new Result<T>(value, error, false);
}

public class Result<T> : Result
{
    public T Value { get; init; }

    protected internal Result(T value, IError? error, bool result) : base(error, result)
    {
        Value = value;
    }
}
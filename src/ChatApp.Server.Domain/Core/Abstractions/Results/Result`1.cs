using ChatApp.Server.Domain.Core.Abstractions.Errors;

namespace ChatApp.Server.Domain.Core.Abstractions.Results;

public sealed class Result<TValue> : Result
{
    private Result(bool isSuccess, Error error, TValue? value = default)
        : base(isSuccess, error)
    {
        if (isSuccess && value == null)
            throw new ArgumentException(InvalidException, nameof(error));

        Value = value;
    }

    public TValue? Value { get; }

    public bool HasValue()
    {
        return Value != null;
    }

    public static Result<TValue> Success(TValue value)
    {
        return new Result<TValue>(true, Error.None, value);
    }

    public new static Result<TValue> Failure(Error error)
    {
        return new Result<TValue>(false, error);
    }

    public static implicit operator Result<TValue>(Error error)
    {
        return Failure(error);
    }
}
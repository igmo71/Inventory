using System.Diagnostics.CodeAnalysis;

namespace Inventory.Common.Results
{
    public class Result
    {
        private Result()
        {
            IsSuccess = true;
            Error = null;
        }

        private Result(Exception error)
        {
            IsSuccess = false;
            Error = error;
        }

        [MemberNotNullWhen(true)]
        [MemberNotNullWhen(false)]
        public bool IsSuccess { get; }
        public Exception? Error { get; }

        public static Result Success() => new();
        public static Result Fail(Exception error) => new(error);
    }

    public class Result<T>
    {
        private Result(T value)
        {
            IsSuccess = true;
            Value = value;
            Error = null;
        }

        private Result(Exception error)
        {
            IsSuccess = false;
            Value = default;
            Error = error;
        }

        [MemberNotNullWhen(true, nameof(Value))]
        [MemberNotNullWhen(false, nameof(Error))]
        public bool IsSuccess { get; }
        public T? Value { get; }
        public Exception? Error { get; }

        public static Result<T> Success(T value) => new(value);
        public static Result<T> Fail(Exception error) => new(error);
    }
}

using System.Diagnostics.CodeAnalysis;

namespace Inventory.Common.Results
{
    public class ListResult<T>
    {
        private ListResult(List<T> value, int total)
        {
            IsSuccess = true;
            Value = value;
            Total = total;
            Error = null;
        }

        private ListResult(Exception error)
        {
            IsSuccess = false;
            Value = default;
            Error = error;
        }

        [MemberNotNullWhen(true, nameof(Value))]
        [MemberNotNullWhen(false, nameof(Error))]
        public bool IsSuccess { get; }
        public List<T>? Value { get; }
        public int Total { get; } = 0;
        public Exception? Error { get; }

        public static ListResult<T> Success(List<T> value, int total) => new(value, total);
        public static ListResult<T> Fail(Exception error) => new(error);
    }
}

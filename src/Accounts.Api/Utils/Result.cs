namespace Accounts.Api.Utils
{
    public class Result<R, T>
    {
        public R Status { get; }
        public T Value { get; }
        public bool IsSuccess { get; }
        public string Message { get; }

        private Result(R status, T value) 
        {
            Status = status;
            Value = value;
            IsSuccess = true;
        }

        private Result(R status, string message) 
        {
            Status = status;
            IsSuccess = false;
            Message = message;
        }

        public static Result<R, T> Success(R status, T value) => new Result<R, T>(status, value);

        public static Result<R, T> Fail(R status, string message) => new Result<R, T>(status, message);
    }
}
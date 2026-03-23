namespace AbySalto.Junior.Services.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public List<string> ErrorItems { get; }
        public T Value { get; }

        private Result(bool isSuccess, List<string> errorList, T value)
        {
            IsSuccess = isSuccess;
            ErrorItems = errorList;
            Value = value;
        }

        public static Result<T> Success(T value) => new Result<T>(true, new List<string>(), value);
        public static Result<T> Success() => new Result<T>(true, new List<string>(), default);
        public static Result<T> Failure(List<string> errorList) => new Result<T>(false, errorList, default);
    }
}
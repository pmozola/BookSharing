using System;
using System.Threading.Tasks;

namespace BookSharing.Application.Results
{
    public class Result
    {
        public bool IsSuccessful { get; }
        public Exception Exception { get; }

        private protected Result()
        {
            IsSuccessful = true;
        }

        private protected Result(Exception error)
        {
            IsSuccessful = false;
            Exception = error;
        }

        public TResult Match<TResult>(Func<TResult> success, Func<Exception, TResult> error)
            => IsSuccessful ? success() : error(Exception);

        public void Match(Action success, Action error)
        {
            if (IsSuccessful) success();
            else error();
        }

        public static Result Success() => new();
        public static Result Error(Exception error) => new(error);
    }

    public class Result<T> : Result
    {
        public T Data { get; }

        private Result(T data) : base()
        {
            Data = data;
        }

        private Result(Exception e) : base(e)
        { }

        public TResult Match<TResult>(Func<T, TResult> success, Func<Exception, TResult> error)
            => IsSuccessful ? success(Data) : error(Exception);

        public Task<TResult> MatchAsync<TResult>(Func<T, Task<TResult>> success, Func<Exception, TResult> error)
            => IsSuccessful ? success(Data) : Task.FromResult(error(Exception));

        public void Match(Action<T> success, Action error)
        {
            if (IsSuccessful) success(Data);
            else error();
        }

        public void Match(Action<T> success, Action<Exception> error)
        {
            if (IsSuccessful) success(Data);
            else error(Exception);
        }

        public static Result<T> Success(T data) => new(data);
        public new static Result<T> Error(Exception error) => new(error);
    }


}

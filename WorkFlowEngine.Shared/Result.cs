namespace WorkFlowEngine.Shared
{
    public class Result
    {
        public List<KeyValuePair<string, string>> Errors { get; set; }
        public bool IsSuccess { get; set; }
        public Result(bool isSuccess)
        {
            IsSuccess = isSuccess;
            Errors = new List<KeyValuePair<string, string>>();
        }
        public Result(bool isSuccess, List<KeyValuePair<string, string>> errors) : this(isSuccess)
        {
            Errors = errors;
        }
    }
    public class Result<TData> : Result
    {
        //private readonly TValue? _value;
        public TData Data { get; private set; }
        public Result(bool isSuccess, List<KeyValuePair<string, string>> errors) : base(isSuccess, errors)
        {
        }
        public Result(TData? data, bool isSuccess) : base(isSuccess) =>
            Data = data;

        public Result(TData? data, bool isSuccess, List<KeyValuePair<string, string>> errors) : base(isSuccess, errors) => Data = data;

        //public TValue Value { get => _value!;}
        //=>IsSuccess ? _value! : throw new InvalidOperationException("The value of a failure result can not be accessed.");
        //public static implicit operator Result<TValue>(TValue? value) => value;
    }
}

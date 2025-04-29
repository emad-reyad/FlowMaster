namespace WorkFlowEngine.Application.Common
{
    public class BaseResponse<T>
    {
        public KeyValuePair<string, string> Errors { get; set; }
        public string Message { get; set; }
        public int Code { get; set; }
        public T Data { get; set; }
        public bool IsSuccess
        {
            get
            {
                return Code >= 200 && Code < 300;
            }
            private set
            {
            }
        }

        public BaseResponse() { }
        public BaseResponse(int code, string message)
        {
            Message = message;
            Code = code;
        }
        public BaseResponse(int code, string message, KeyValuePair<string, string> errors) : this(code, message)
        {
            Errors = errors;
        }


    }
}

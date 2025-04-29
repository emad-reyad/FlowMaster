namespace WorkFlowEngine.Application.Common
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        {
        }

        public BadRequestException(List<KeyValuePair<string, string>> errors) : base("Multiple errors occurred. See error details.")
        {
            Errors = errors;
        }

        public List<KeyValuePair<string, string>> Errors { get; set; }
    }
}
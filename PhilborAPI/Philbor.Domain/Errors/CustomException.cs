using Philbor.Domain.Shared;

namespace Philbor.Domain.Errors
{
    public class CustomException : Exception
    {
        public int StatusCode { get; }

        public string? Code { get; }

        public CustomException()
            : base("Unexpected Error")
        {
            StatusCode = 500;
        }

        public CustomException(string message, int statusCode = 500)
            : base(message)
        {
            StatusCode = statusCode;
        }

        public CustomException(string message, int statusCode, Exception inner)
            : base(message, inner)
        {
            StatusCode = statusCode;
        }

        public CustomException(Error error)
            : base(error.Description)
        {
            StatusCode = error.Status.Value;
            Code = error.Code;
        }
    }
}

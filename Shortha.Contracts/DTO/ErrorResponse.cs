using System.Net;

namespace Shortha.DTO
{
    public class ErrorResponse
    {
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; }
        public object? Errors { get; set; }

        public ErrorResponse(HttpStatusCode status, string message, object? errors = null)
        {
            Status = status;
            Message = message;
            Errors = errors;
        }
    }

}

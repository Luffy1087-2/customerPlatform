namespace CustomerPlatform.Core.Models
{
    public class ErrorDto
    {
        public ErrorDto(int statusCode, string errorMessage)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
        }

        public int StatusCode { get;  } 
        public string ErrorMessage { get; }
    }
}

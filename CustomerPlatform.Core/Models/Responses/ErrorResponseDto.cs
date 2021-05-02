namespace CustomerPlatform.Core.Models.Responses
{
    public class ErrorResponseDto
    {
        public ErrorResponseDto(int statusCode, string errorMessage)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
        }

        public int StatusCode { get;  } 
        public string ErrorMessage { get; }
    }
}

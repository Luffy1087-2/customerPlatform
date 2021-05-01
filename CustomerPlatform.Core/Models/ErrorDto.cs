namespace CustomerPlatform.Core.Models
{
    public class ErrorDto
    {
        public ErrorDto(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; }
    }
}

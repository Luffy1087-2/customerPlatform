using Microsoft.AspNetCore.Http;

namespace CustomerPlatform.Core.Models.Responses
{
    public class OkResponseDto
    {
        public OkResponseDto(string message)
        {
            StatusCode = StatusCodes.Status200OK;
            Message = message;
        }

        public int StatusCode { get; }
        public string Message { get; }
    }
}

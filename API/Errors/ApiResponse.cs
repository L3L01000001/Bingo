using System;

namespace API.Errors
{
    public class ApiResponse
    {

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultStatusCodeMessage();
        }

        private string GetDefaultStatusCodeMessage()
        {
            return StatusCode switch  
            {
                400 => "Bad request, you have made",
                401 => "Authorized, you are not",
                404 => "Resource found, it was not",
                500 => "Internal server error",
                _ => null

            };

        }
    }
}
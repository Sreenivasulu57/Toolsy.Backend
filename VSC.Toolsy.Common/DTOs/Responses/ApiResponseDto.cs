

namespace VSC.Toolsy.Common.DTOs.Responses
{
    public class ApiResponseDto<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public static ApiResponseDto<T> SuccessResponse(T data, string message = "Success")
            => new() { Success = true, Data = data, Message = message };

        public static ApiResponseDto<T> FailureResponse(string message)
            => new() { Success = false, Message = message };
    }
}

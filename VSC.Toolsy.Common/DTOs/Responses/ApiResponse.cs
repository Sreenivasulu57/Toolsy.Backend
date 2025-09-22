using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSC.Toolsy.Common.DTOs.Responses
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public static ApiResponse<T> SuccessResponse(T data, string message = "Success")
            => new() { Success = true, Data = data, Message = message };

        public static ApiResponse<T> FailureResponse(string message)
            => new() { Success = false, Message = message };
    }
}

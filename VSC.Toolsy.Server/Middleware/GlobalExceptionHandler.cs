using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Newtonsoft.Json;
using VSC.Toolsy.Common.DTOs.Responses;
using System.Net;
using VSC.Toolsy.Common.Exceptions;

namespace VSC.Toolsy.Server.Middleware
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {

                HttpResponse response = httpContext.Response;
                response.ContentType = "application/json";

                switch (e)
                {
                    case UserNotFoundException userNotFoundException:

                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case DbUpdateException dbEx

                        when dbEx.InnerException is MySqlException mysqlEx && mysqlEx.Number == 1062:
                        response.StatusCode = (int)HttpStatusCode.Conflict;
                        break;
                    default:

                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                string result = JsonConvert.SerializeObject(ApiResponseDto<string>.FailureResponse(e.Message));
                await response.WriteAsync(result);

            }
        }

    }
}

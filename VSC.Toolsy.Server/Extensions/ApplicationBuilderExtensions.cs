using VSC.Toolsy.Server.Middleware;

namespace VSC.Toolsy.Server.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseApiDefaults(this IApplicationBuilder app)
        {       

            app.UseMiddleware<GlobalExceptionHandler>();
 
            return app;
        }
    }
}

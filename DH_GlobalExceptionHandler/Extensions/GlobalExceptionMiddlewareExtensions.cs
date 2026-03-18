using DH_GlobalExceptionHandler.Middleware;
using Microsoft.AspNetCore.Builder;

namespace DH_GlobalExceptionHandler.Extensions
{
    public static class GlobalExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<GlobalExceptionMiddleware>();
        }
    }
}
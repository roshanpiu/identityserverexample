using IdentityServerWithAspNetIdentity.Middleware;

namespace Microsoft.AspNetCore.Builder
{
    public static class RequestTimerMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestTimer(this IApplicationBuilder app, RequestTimerOptions options = null)
        {
            return app.UseMiddleware<RequestTimerMiddleware>(options ?? new RequestTimerOptions());
        }
    }
}
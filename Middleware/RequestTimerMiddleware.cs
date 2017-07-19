using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;


namespace IdentityServerWithAspNetIdentity.Middleware
{
    public class RequestTimerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestTimerMiddleware> _logger;
        private readonly RequestTimerOptions _options;

        public RequestTimerMiddleware(RequestDelegate next, RequestTimerOptions options, ILogger<RequestTimerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
            _options = options;
        }

        public async Task Invoke(HttpContext context)
        {
            var timer = new Stopwatch();
            timer.Start();

            await _next(context);

            timer.Stop();
            _logger.LogInformation(_options.Format(context, timer.ElapsedMilliseconds));
        }
    }
}
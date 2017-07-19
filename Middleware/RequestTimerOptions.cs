using System;
using Microsoft.AspNetCore.Http;

namespace IdentityServerWithAspNetIdentity.Middleware
{
    public class RequestTimerOptions
    {
        public RequestTimerOptions()
        {
            Format = (context, elapsed) => $"Request to {context.Request.Method} {context.Request.Path} took {elapsed} ms";
        }
        public Func<HttpContext, long, string> Format { get; set; }
    }
}
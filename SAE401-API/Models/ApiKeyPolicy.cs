using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models
{
    public class ApiKeyPolicy
    {
        private readonly RequestDelegate next;

        public ApiKeyPolicy(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!bool.Parse(Environment.GetEnvironmentVariable("REQUIRE_API_KEY") ?? "true"))
            {
                await next(context);
                return;
            }

            if (context.Request.Headers.TryGetValue("API_KEY", out var apiKey))
            {
                var key = apiKey.FirstOrDefault();
                if (key == null)
                {
                    context.Response.StatusCode = 400;
                }
                else if (key != Environment.GetEnvironmentVariable("API_KEY"))
                {
                    context.Response.StatusCode = 403;
                }
                else
                {
                    await next(context);
                    return;
                }
            }
            else
            {
                context.Response.StatusCode = 400;
            }
        }
    }
}

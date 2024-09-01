using LagEnvironmentApplication.Interfaces;

namespace LagQueueAnalysisAPI.Middleware
{
    public class TokenAutheticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ITokenStore _tokenStore;

        public TokenAutheticationMiddleware(RequestDelegate next, ITokenStore tokenStore)
        {
            _next = next;
            _tokenStore = tokenStore;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var allow = new[] { "/auth" };
            if (allow.Any(route => context.Request.Path.StartsWithSegments(route)))
            {
                await _next(context);
                return;
            }

            if (!context.Request.Headers.TryGetValue("Authorization", out var token))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Token não informado.");
                return;
            }

            if (!_tokenStore.HasToken(Guid.Parse(token)))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Token inválido.");
                return;
            }

            await _next(context);
        }
    }
}

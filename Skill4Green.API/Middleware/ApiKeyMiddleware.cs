namespace Skill4Green.API.Middleware;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private const string ApiKeyHeader = "X-API-KEY";
    private const string ValidKey = "skill4green-secret";

    public ApiKeyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue(ApiKeyHeader, out var extractedKey) || extractedKey != ValidKey)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("API Key inválida ou ausente");
            return;
        }

        await _next(context);
    }
}
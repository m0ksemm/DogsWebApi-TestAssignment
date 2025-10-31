namespace DogsWebApi.UI.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next; _logger = logger;
        }
        public async Task InvokeAsync(HttpContext ctx)
        {
            try { await _next(ctx); }
            catch (BadHttpRequestException ex)
            {
                ctx.Response.StatusCode = StatusCodes.Status400BadRequest;
                await ctx.Response.WriteAsJsonAsync(new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                ctx.Response.StatusCode = StatusCodes.Status409Conflict;
                await ctx.Response.WriteAsJsonAsync(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");
                ctx.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await ctx.Response.WriteAsJsonAsync(new { error = "An unexpected error occurred." });
            }
        }
    }
}

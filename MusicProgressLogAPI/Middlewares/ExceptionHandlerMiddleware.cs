using System.Net;

namespace MusicProgressLogAPI.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger,
            RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Any exceptions throughout application will be handled here.
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                var errorId = Guid.NewGuid().ToString();

                _logger.LogError($"{errorId} : {e.Message}", e);

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var body = new
                {
                    Id = errorId,
                    ErrorMessage = "Server failed to process request."
                };

                await context.Response.WriteAsJsonAsync(body);
            }
        }
    }
}

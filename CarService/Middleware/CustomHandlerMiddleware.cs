namespace CarService.Middleware
{
    public class CustomHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public CustomHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method == "GET")
            {
                _logger.LogError("This is get method");
            }

            await _next(context);
        }
    }
}

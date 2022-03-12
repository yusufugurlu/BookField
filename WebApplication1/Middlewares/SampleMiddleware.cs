namespace WebApplication1.Middlewares
{
    public class SampleMiddleware
    {
        private readonly RequestDelegate _next;
        public SampleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext content)
        {
            System.Diagnostics.Debug.WriteLine("Run Sample Middleware");
            await _next.Invoke(content);
            System.Diagnostics.Debug.WriteLine("Finish Sample Middleware");
        }
    }

    public static class SampleMiddlewareExtension
    {
        public static IApplicationBuilder UseSample(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SampleMiddleware>();
        }
    }
}

using System.Buffers;
using System.Diagnostics;
using System.IO.Pipelines;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;

namespace WebApplication1.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            var watch = Stopwatch.StartNew();
            string message = "";
            if (context.Request.Method == "Get")
            {
                message = "[Request] HTTP " + context.Request.Method + " - " + context.Request.Path+"\n";
            }
            else
            {
                message = "[Request] HTTP " + context.Request.Method + " - " + context.Request.Path;
                //message += "\nBody=" + context.Request.BodyReader;
            }
            if (context.Request.QueryString.HasValue)
            {
                message += "\nQuery= " + context.Request.QueryString.Value;
            }

            try
            {
                await _next.Invoke(context);
                watch.Stop();
                message += "\n[Response] " + context.Request.Method + " - " + context.Request.Path + " - Responsed " + context.Response.StatusCode + " in " + watch.Elapsed.TotalMilliseconds;
                Debug.WriteLine(message);
            }
            catch (Exception ex)
            {
                watch.Stop();
                await HandleExceptionControl(context, ex, watch);
                throw;
            }
        }

        //public async Task<string> ReadLineUsingPipelineAsync(PipeReader pipeReader)
        //{
        //    var reader = pipeReader;
        //    string str;
        //    while (true)
        //    {
        //        ReadResult result = await reader.ReadAsync();
        //        ReadOnlySequence<byte> buffer = result.Buffer;

        //        while ((str = ReadLine(ref buffer)) is not null)
        //        {
        //            // simulate string processing
        //            str = str.AsSpan().Slice(0, 5).ToString();
        //        }

        //        reader.AdvanceTo(buffer.Start, buffer.End);

        //        if (result.IsCompleted) break;
        //    }

        //    await reader.CompleteAsync();
        //    return str;
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //private static string ReadLine(ref ReadOnlySequence<byte> buffer)
        //{
        //    var reader = new SequenceReader<byte>(buffer);

        //    if (reader.TryReadTo(out var line, NewLine))
        //    {
        //        buffer = buffer.Slice(reader.Position);
        //        return Encoding.UTF8.GetString(line);
        //    }

        //    return default;
        //}
        private Task HandleExceptionControl(HttpContext context, Exception ex, Stopwatch watch)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var message = "\n[Error]" + context.Request.Method + " - " + context.Request.Path + " - Responsed " + context.Response.StatusCode + " in " + watch.Elapsed.TotalMilliseconds;
            Debug.WriteLine("---");
            Debug.WriteLine(message);
            Debug.WriteLine("---");
            var result = Newtonsoft.Json.JsonConvert.SerializeObject(new { error = ex.Message }, Newtonsoft.Json.Formatting.None);
            return context.Response.WriteAsync(result);
        }
    }

    public static class CustomExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomException(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}

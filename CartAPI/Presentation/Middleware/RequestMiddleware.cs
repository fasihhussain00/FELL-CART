using CartAPI.Application.IService;
using CartAPI.Domain.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CartAPI.Presentation.Middleware
{
    public class RequestMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogService _logService;

        public RequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, ILogService logService)
        {
            _logService = logService;
            GenerateRequestIdAndAddToHeaders(context);
            EnableToReadRequestMultipleTimes(context.Request);
            var originalResponseStream = EnableToReadResponseMultipleTimesByBackingUpOriginalStream(context);
            await _next?.Invoke(context);
            await LogRequest(context);
            await RestoreOriginalResponseStream(context, originalResponseStream);
        }
        private static void EnableToReadRequestMultipleTimes(HttpRequest request)
        {
            request.EnableBuffering();
        }
        private static Stream EnableToReadResponseMultipleTimesByBackingUpOriginalStream(HttpContext context)
        {
            var originalResponseStream = BackupOriginalResponseStream(context.Response.Body);
            AssignNewReadableStreamToResponseBody(context);
            return originalResponseStream;
        }
        private static void AssignNewReadableStreamToResponseBody(HttpContext context)
        {
            context.Response.Body = new MemoryStream();
        }
        private static Stream BackupOriginalResponseStream(Stream stream)
        {
            return stream;
        }
        private async Task<RequestLog> LogRequest(HttpContext context)
        {
            var requestBody = await ReadRequestBody(context.Request.Body);
            var responseBody = await ReadResponseBody(context.Response.Body);
            return await RecordLog(context, requestBody, responseBody);
        }
        private static async Task RestoreOriginalResponseStream(HttpContext context, Stream originalResponseStream)
        {
            ((MemoryStream)context.Response.Body).Seek(0, SeekOrigin.Begin);
            await context.Response.Body.CopyToAsync(originalResponseStream);
            await context.Response.Body.FlushAsync();
            context.Response.Body = originalResponseStream;
        }
        private async Task<RequestLog> RecordLog(HttpContext context, string requestBody, string responseBody)
        {
            var requestLogModel = new RequestLog
            {
                ID = Guid.Parse(context.Request.Headers["RequestId"]),
                Type = "Incoming",
                RequestDomain = context.Request.Host.Host,
                RequestIp = context.Connection.RemoteIpAddress?.ToString(),
                RequestBody = requestBody,
                RequestHeader = GetRequestHeader(context),
                RequestUrl = context.Request.Path.Value,
                ResponseBody = responseBody,
                ResponseHeader = GetResponseHeaders(context),
                Scheme = context.Request.Scheme,
                FormData = GetSerializedFormData(context),
                RouteData = GetSerializedRouteData(context),
                RequestQueryString = context.Request.QueryString.Value,
                Method = context.Request.Method,
                StatusCode = context.Response.StatusCode.ToString(),
                UserAgent = context.Request.Headers["User-Agent"].ToString(),
                TimeStamp = DateTimeOffset.Now,
            };
            return await _logService.LogAsync(requestLogModel);
        }
        private static string GetSerializedRouteData(HttpContext context)
        {
            return JsonSerializer.Serialize(context.Request.RouteValues);
        }
        private static string GetSerializedFormData(HttpContext context)
        {
            return context.Request.HasFormContentType ? JsonSerializer.Serialize(context.Request.Form.ToDictionary(kv => kv.Key, kv => kv.Value)) : string.Empty;
        }
        private static string GetRequestHeader(HttpContext context) => JsonSerializer.Serialize(context.Request.Headers);
        private static string GetResponseHeaders(HttpContext context) => JsonSerializer.Serialize(context.Response.Headers);
        private async Task<string> ReadRequestBody(Stream stream)
        {
            stream.Position = 0;
            string body = await new StreamReader(stream, Encoding.UTF8, true, 1024, true).ReadToEndAsync();
            stream.Position = 0;
            return body;
        }
        private async Task<string> ReadResponseBody(Stream stream)
        {
            ((MemoryStream)stream).Seek(0, SeekOrigin.Begin);
            string body = await new StreamReader(stream, Encoding.UTF8, true, 1024, true).ReadToEndAsync();
            ((MemoryStream)stream).Seek(0, SeekOrigin.Begin);
            stream.Position = 0;
            return body;
        }
        private static void GenerateRequestIdAndAddToHeaders(HttpContext context)
        {
            context.Request.Headers.Add("RequestId", Guid.NewGuid().ToString());
        }
    }
}

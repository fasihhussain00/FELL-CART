using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CartAPI.Utils
{
    public class ResponseBuilder
    {
        private readonly Guid requestID;
        public ResponseBuilder(Guid RequestID)
        {
            requestID = RequestID;
        }
        public Response<T> BuildResponse<T>(T data)
        {
            return new Response<T>
            {
                RequestID = requestID,
                Data = data,
            };
        }
        public Response<T> BuildResponse<T>(T data, string message)
        {
            return new Response<T>
            {
                RequestID = requestID,
                Data = data,
                Message = message,
            };
        }
        public Response BuildResponse(string message, int statuscode)
        {
            return new Response
            {
                RequestID = requestID,
                StatusCode = statuscode,
                Message = message,
            };
        }
        public Response<T> BuildResponse<T>(T data, string message, int statusCode)
        {
            return new Response<T>
            {
                RequestID = requestID,
                Data = data,
                Message = message,
                StatusCode = statusCode
            };
        }
        public Response<T> BuildResponse<T>(T data, string message, bool success, int statusCode)
        {
            return new Response<T>
            {
                RequestID = requestID,
                Data = data,
                Message = message,
                StatusCode = statusCode
            };
        }
    }
    public class ResponseWriter
    {
        private readonly HttpContext context;
        public ResponseWriter(HttpContext context)
        {
            this.context = context;
        }
        public async Task WriteResponse(object responseBody, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
                context.Response.StatusCode = (int)statusCode;
                await context.Response.WriteAsJsonAsync(responseBody);
                await context.Response.CompleteAsync();
                context.Abort();            
        }
        public async Task WriteResponse<T>(T responseBody, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsJsonAsync(responseBody);
            await context.Response.CompleteAsync();
            context.Abort();
        }
        public async Task WriteBody<T>(T responseBody, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsJsonAsync(responseBody);
        }
        public async Task WriteResponse(string absolutePathToFile, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            context.Response.StatusCode = (int)statusCode;
            await context.Response.SendFileAsync(absolutePathToFile);
            await context.Response.CompleteAsync();
            context.Abort();
        }
    }
    public class Response
    {
        public Guid RequestID { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }

    public class Response<T> : Response
    {
        public T Data { get; set; }
    }
}

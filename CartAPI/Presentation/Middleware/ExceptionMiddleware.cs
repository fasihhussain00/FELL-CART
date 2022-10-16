using CartAPI.Application.IService;
using CartAPI.Domain.Model;
using CartAPI.Infrastructure.Exceptions;
using CartAPI.Utils;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace CartAPI.Presentation.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogService _exceptionLogger;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ILogService exceptionLogger)
        {
            _exceptionLogger = exceptionLogger;
            try
            {
                await _next?.Invoke(context);
            }
            catch (CustomException e)
            {
                var errorResponseBody = new ResponseBuilder(GetGuid(context)).BuildResponse(e.Message, e.StatusCode);
                await new ResponseWriter(context).WriteBody(errorResponseBody);
                return;
            }
            catch (Exception)
            {
                var errorResponseBody = new ResponseBuilder(GetGuid(context)).BuildResponse("Some Error Occured", 400);
                await new ResponseWriter(context).WriteBody(errorResponseBody);
                return;
            }
        }



        private static Guid GetGuid(HttpContext httpContext)
        {
            return Guid.Parse(httpContext.Request.Headers["RequestId"]);
        }

        private async Task<ExceptionLog> LogException(Exception e, HttpContext context)
        {
            var exceptionLogModel = new ExceptionLog
            {
                ID = Guid.NewGuid(),
                RequestID = GetGuid(context),
                Message = e.Message,
                StackTrace = e.StackTrace,
                TimeStamp = DateTimeOffset.Now,
            };
            return await _exceptionLogger.LogAsync(exceptionLogModel);
        }
    }
}

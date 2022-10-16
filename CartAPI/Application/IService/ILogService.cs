using CartAPI.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CartAPI.Application.IService
{
    public interface ILogService
    {
        ValueTask<IEnumerable<RequestLog>> GetFilteredLogAsync(DateTimeOffset? startFrom = null, DateTimeOffset? endAt = null, string statusCode = null, string type = null, string method = null, string url = null);
        ValueTask<RequestLog> GetLogAsync(Guid requestId);
        ValueTask<RequestLog> LogAsync(RequestLog requestLog);
        ValueTask<ExceptionLog> LogAsync(ExceptionLog exceptionLog);
    }
}

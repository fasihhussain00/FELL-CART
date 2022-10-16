using CartAPI.Application.IRepo;
using CartAPI.Database;
using CartAPI.Domain.Model;
using CartAPI.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CartAPI.Infrastructure.Repo
{
    public class RequestLogRepo : IRequestLogRepo
    {
        private readonly LogDBManager _connection;

        public RequestLogRepo(LogDBManager connection)
        {
            _connection = connection;
        }

        public async ValueTask<RequestLog> Insert(RequestLog requestLog)
        {
            var parameters = RequestLogModelToParameters(requestLog);
            var createdRequestLog = await _connection.QueryAsync<RequestLog>(StoredProcedures.SaveLogRequest, parameters);
            return createdRequestLog;

        }
        private static Dictionary<string, object> RequestLogModelToParameters(RequestLog requestLog)
        {
            return new Dictionary<string, object>
            {
                { "id", requestLog.ID },
                { "type", requestLog.Type },
                { "requestdomain", requestLog.RequestDomain },
                { "requestip", requestLog.RequestIp },
                { "requesturl", requestLog.RequestUrl },
                { "requestheader", requestLog.RequestHeader },
                { "requestbody", requestLog.RequestBody },
                { "requestquerystring", requestLog.RequestQueryString},
                { "scheme", requestLog.Scheme },
                { "formdata", requestLog.FormData },
                { "routedata", requestLog.RouteData },
                { "httpmethod", requestLog.Method },
                { "responseheader", requestLog.ResponseHeader },
                { "responsebody", requestLog.ResponseBody },
                { "statuscode", requestLog.StatusCode },
                { "useragent", requestLog.UserAgent },
                { "timestamp", requestLog.TimeStamp },
            };
        }
        public async ValueTask<RequestLog> Get(Guid requestId)
        {
            var parameters = new Dictionary<string, object>
            {
                { "id", requestId },
            };
            return await _connection.QueryAsync<RequestLog>(StoredProcedures.GetLogRequest, parameters);
        }
        public async ValueTask<IEnumerable<RequestLog>> GetFiltered(
            DateTimeOffset? startFrom = null,
            DateTimeOffset? endAt = null,
            string statusCode = null,
            string type = null,
            string method = null,
            string url = null)
        {
            var parameters = new Dictionary<string, object>
            {
                { "startfrom", startFrom },
                { "endat", endAt },
                { "statuscode", statusCode },
                { "type", type },
                { "method", method },
                { "url", url },
            };
            return await _connection.QueryListAsync<RequestLog>(StoredProcedures.GetLogRequest, parameters);
        }
    }
}

using CartAPI.Application.IRepo;
using CartAPI.Database;
using CartAPI.Domain.Model;
using CartAPI.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CartAPI.Infrastructure.Repo
{
    public class ExceptionLogRepo : IExceptionLogRepo
    {
        private readonly LogDBManager _connection;
        public ExceptionLogRepo(LogDBManager connection)
        {
            _connection = connection;
        }
        public async ValueTask<ExceptionLog> Insert(ExceptionLog exceptionLog)
        {

            var parameters = ExceptionModelToParameters(exceptionLog);
            var requestId = await _connection.QueryAsync<ExceptionLog>(StoredProcedures.SaveLogException, parameters);
            return requestId;

        }
        public ValueTask<ExceptionLog> Get(Guid Id)
        {
            throw new NotImplementedException();
        }
        public ValueTask<IEnumerable<RequestLog>> GetFiltered(DateTimeOffset? startFrom = null, DateTimeOffset? endAt = null)
        {
            throw new NotImplementedException();
        }
        private Dictionary<string, object> ExceptionModelToParameters(ExceptionLog exceptionLog)
        {
            return new Dictionary<string, object>
            {
                { "id", exceptionLog.ID },
                { "reqeustid", exceptionLog.RequestID },
                { "message", exceptionLog.Message },
                { "stacktrace", exceptionLog.StackTrace},
                { "timestamp", exceptionLog.TimeStamp },
            };
        }
    }
}

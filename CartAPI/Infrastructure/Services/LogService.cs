using CartAPI.Application.IRepo;
using CartAPI.Application.IService;
using CartAPI.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CartAPI.Infrastructure.Services
{
    public class LogService : ILogService
    {
        private readonly IRequestLogRepo _requestLogRepo;
        private readonly IExceptionLogRepo _exceptionLogRepo;

        public LogService(IRequestLogRepo requestLogRepo, IExceptionLogRepo exceptionLogRepo)
        {
            _requestLogRepo = requestLogRepo;
            _exceptionLogRepo = exceptionLogRepo;
        }

        public async ValueTask<RequestLog> LogAsync(RequestLog requestLog)
        {
            return await _requestLogRepo.Insert(requestLog);
        }
        public async ValueTask<ExceptionLog> LogAsync(ExceptionLog exceptionLog)
        {
            return await _exceptionLogRepo.Insert(exceptionLog);
        }

        public async ValueTask<RequestLog> GetLogAsync(Guid requestId)
        {
            return await _requestLogRepo.Get(requestId);
        }
        public async ValueTask<IEnumerable<RequestLog>> GetFilteredLogAsync(
            DateTimeOffset? startFrom = null,
            DateTimeOffset? endAt = null,
            string statusCode = null,
            string type = null,
            string method = null,
            string url = null)
        {
            return await _requestLogRepo.GetFiltered(startFrom, endAt, statusCode, type, method, url);
        }
    }
}

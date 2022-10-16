using CartAPI.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CartAPI.Application.IRepo
{
    public interface ILogRepo<T>
    {
        ValueTask<T> Insert(T logModel);
        ValueTask<T> Get(Guid Id);
    }

    public interface IRequestLogRepo : ILogRepo<RequestLog>
    {
        ValueTask<IEnumerable<RequestLog>> GetFiltered(DateTimeOffset? startFrom = null, DateTimeOffset? endAt = null, string statusCode = null, string type = null, string method = null, string url = null);
    }
    public interface IExceptionLogRepo : ILogRepo<ExceptionLog>
    {
        ValueTask<IEnumerable<RequestLog>> GetFiltered(DateTimeOffset? startFrom = null, DateTimeOffset? endAt = null);
    }

}

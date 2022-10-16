using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CartAPI.Utils
{
    public class RequestManager
    {
        public RequestManager()
        {
            
        }
        public Task LogRequest(string requestId, object requestBody, string method, string url, string domain, DateTimeOffset timestamp, string ip, StringValues useragent, string requestHeaders, string responseHeaders, int statusCode, object responseBody)
        {
            throw new NotImplementedException();
        }

        public Task LogError(string requestId, string v, HttpContext context)
        {
            throw new NotImplementedException();
        }
    }
}

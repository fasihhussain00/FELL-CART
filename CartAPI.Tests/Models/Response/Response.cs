using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartAPI.Tests.Models.Response
{
    public class Response
    {
        public string RequestID { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }

    public class Response<T> : Response
    {
        public T Data { get; set; }
    }
}

using System;

namespace CartAPI.Domain.Model
{
    public class RequestLog
    {
        public Guid ID { get; set; }
        public string Type { get; set; }
        public string RequestDomain { get; set; }
        public string RequestIp { get; set; }
        public string RequestBody { get; set; }
        public string RequestHeader { get; set; }
        public string RequestUrl { get; set; }
        public string ResponseBody { get; set; }
        public string ResponseHeader { get; set; }
        public string Scheme { get; set; }
        public string FormData { get; set; }
        public string RouteData { get; set; }
        public string RequestQueryString { get; set; }
        public string Method { get; set; }
        public string StatusCode { get; set; }
        public string UserAgent { get; set; }
        public DateTimeOffset TimeStamp { get; set; }

        internal void Deconstruct(
            out string type,
            out string requestdomain,
            out string requestip,
            out string requestbody,
            out string requestheader,
            out string requesturl,
            out string responsebody,
            out string responseheader,
            out string scheme,
            out string formdata,
            out string routedata,
            out string requestquerystring,
            out string method,
            out string statuscode,
            out string useragent,
            out DateTimeOffset timestamp
            )
        {
            type = Type;
            requestdomain = RequestDomain;
            requestip = RequestIp;
            requestbody = RequestBody;
            requestheader = RequestHeader;
            requesturl = RequestUrl;
            responsebody = ResponseBody;
            responseheader = ResponseHeader;
            scheme = Scheme;
            formdata = FormData;
            routedata = RouteData;
            requestquerystring = RequestQueryString;
            method = Method;
            statuscode = StatusCode;
            useragent = UserAgent;
            timestamp = TimeStamp;
        }
    }
}
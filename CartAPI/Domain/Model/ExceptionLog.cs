using System;

namespace CartAPI.Domain.Model
{
    public class ExceptionLog
    {
        public Guid ID { get; set; }
        public Guid RequestID { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public DateTimeOffset? TimeStamp { get; set; }
    }
}













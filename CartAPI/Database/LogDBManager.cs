using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.Common;

namespace CartAPI.Database
{

    public sealed class LogDBManager : DBManager
    {
        public LogDBManager(IConfiguration configuration) : base(configuration.GetConnectionString("LogDBConnection"))
        {
        }
    }

}

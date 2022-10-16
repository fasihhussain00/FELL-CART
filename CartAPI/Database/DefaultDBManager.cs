using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.Common;

namespace CartAPI.Database
{

    public sealed class DefaultDBManager : DBManager
    {
        public DefaultDBManager(IConfiguration configuration) : base(configuration.GetConnectionString("DefaultDBConnection"))
        {
        }
    }

}

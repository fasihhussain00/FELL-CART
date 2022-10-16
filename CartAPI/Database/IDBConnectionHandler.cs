using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace CartAPI.Database
{
    public interface IDBConnectionHandler : IDisposable
    {
        Task<int> ExecuteAsync(string sp, IDictionary parameters = null, CommandType commandType = CommandType.StoredProcedure);
        DbConnection GetConnection();
        void OpenConnection();
        Task OpenConnectionAsync();
        T Query<T>(string sql, IDictionary parameters = null, CommandType commandType = CommandType.StoredProcedure);
        Task<T> QueryAsync<T>(string sql, IDictionary parameters = null, CommandType commandType = CommandType.StoredProcedure);
        IEnumerable<T> QueryList<T>(string sql, IDictionary parameters = null, CommandType commandType = CommandType.StoredProcedure);
        IEnumerable<T> QueryList<TFirst, TSecond, T>(string sql, Func<TFirst, TSecond, T> func, IDictionary parameters = null, string SplitOn = "Id", CommandType commandType = CommandType.StoredProcedure);
        IEnumerable<T> QueryList<TFirst, TSecond, TThird, T>(string sql, Func<TFirst, TSecond, TThird, T> func, IDictionary parameters = null, string SplitOn = "Id", CommandType commandType = CommandType.StoredProcedure);
        Task<IEnumerable<T>> QueryListAsync<T>(string sql, IDictionary parameters = null, CommandType commandType = CommandType.StoredProcedure);
        Task<IEnumerable<T>> QueryListAsync<TFirst, TSecond, T>(string sql, Func<TFirst, TSecond, T> func, IDictionary parameters = null, string SplitOn = "Id", CommandType commandType = CommandType.StoredProcedure);
        Task<IEnumerable<T>> QueryListAsync<TFirst, TSecond, TThird, T>(string sql, Func<TFirst, TSecond, TThird, T> func, IDictionary parameters = null, string SplitOn = "Id", CommandType commandType = CommandType.StoredProcedure);
    }
}

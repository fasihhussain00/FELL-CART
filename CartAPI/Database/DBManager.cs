using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace CartAPI.Database
{

    public class DBManager : IDBManager
    {
        private readonly DbConnection _connection;
        private DbTransaction _transaction;
        public string ConnectionString { get; set; }

        public DBManager(string connectionString)
        {
            ConnectionString = connectionString;
            _connection = new SqlConnection(ConnectionString);
        }
        public DbConnection GetConnection()
        {
            return _connection;
        }
        public DbTransaction GetTransaction()
        {
            return _transaction;
        }
        public void OpenConnection()
        {
            if (_connection.State == ConnectionState.Closed)
                _connection.Open();
        }
        public async Task OpenConnectionAsync()
        {
            if (_connection.State == ConnectionState.Closed)
                await _connection.OpenAsync();
        }
        public void BeginTransaction()
        {
            if (_connection.State == ConnectionState.Closed && _connection.State != ConnectionState.Open)
                _connection.Open();
            _transaction = _connection.BeginTransaction();
        }
        public async Task BeginTransactionAsync()
        {
            if (_connection.State == ConnectionState.Closed && _connection.State != ConnectionState.Open)
                await _connection.OpenAsync();
            _transaction = await _connection.BeginTransactionAsync();
        }
        public void CommitTransaction()
        {
            _transaction.Commit();
        }
        public async Task CommitTransactionAsync()
        {
            await _transaction.CommitAsync();
        }
        public void RollBackTransaction()
        {
            _transaction.Rollback();
        }
        public async Task RollBackTransactionAsync()
        {
            await _transaction.RollbackAsync();
        }
        public Task<IEnumerable<T>> QueryListAsync<T>(string sql, IDictionary parameters = null, CommandType commandType = CommandType.StoredProcedure)
        {
            return _connection.QueryAsync<T>(sql, parameters, _transaction, commandType: commandType);
        }
        public Task<T> QueryAsync<T>(string sql, IDictionary parameters = null, CommandType commandType = CommandType.StoredProcedure)
        {
            return _connection.QueryFirstOrDefaultAsync<T>(sql, parameters, _transaction, commandType: commandType);
        }
        public Task<IEnumerable<T>> QueryListAsync<TFirst, TSecond, T>(string sql, Func<TFirst, TSecond, T> func, IDictionary parameters = null, string SplitOn = "Id", CommandType commandType = CommandType.StoredProcedure)
        {
            return _connection.QueryAsync(sql: sql, param: parameters, transaction: _transaction, commandType: commandType, map: func, splitOn: SplitOn);
        }
        public Task<IEnumerable<T>> QueryListAsync<TFirst, TSecond, TThird, T>(string sql, Func<TFirst, TSecond, TThird, T> func, IDictionary parameters = null, string SplitOn = "Id", CommandType commandType = CommandType.StoredProcedure)
        {
            return _connection.QueryAsync(sql: sql, param: parameters, transaction: _transaction, commandType: commandType, map: func, splitOn: SplitOn);
        }
        public IEnumerable<T> QueryList<T>(string sql, IDictionary parameters = null, CommandType commandType = CommandType.StoredProcedure)
        {
            return _connection.Query<T>(sql, parameters, _transaction, commandType: commandType);
        }
        public IEnumerable<T> QueryList<TFirst, TSecond, T>(string sql, Func<TFirst, TSecond, T> func, IDictionary parameters = null, string SplitOn = "Id", CommandType commandType = CommandType.StoredProcedure)
        {
            return _connection.Query(sql: sql, param: parameters, transaction: _transaction, commandType: commandType, map: func, splitOn: SplitOn);
        }
        public IEnumerable<T> QueryList<TFirst, TSecond, TThird, T>(string sql, Func<TFirst, TSecond, TThird, T> func, IDictionary parameters = null, string SplitOn = "Id", CommandType commandType = CommandType.StoredProcedure)
        {
            return _connection.Query(sql: sql, param: parameters, transaction: _transaction, commandType: commandType, map: func, splitOn: SplitOn);
        }
        public T Query<T>(string sql, IDictionary parameters = null, CommandType commandType = CommandType.StoredProcedure)
        {
            return _connection.QueryFirstOrDefault<T>(sql, parameters, _transaction, commandType: commandType);
        }
        public Task<int> ExecuteAsync(string sp, IDictionary parameters = null, CommandType commandType = CommandType.StoredProcedure)
        {
            return _connection.ExecuteAsync(sp, parameters, _transaction, commandType: commandType);
        }
        public void Dispose()
        {
            if (_connection?.State == ConnectionState.Open) _connection.Close();
            _connection?.Dispose();
            _transaction?.Dispose();
        }
    }

}

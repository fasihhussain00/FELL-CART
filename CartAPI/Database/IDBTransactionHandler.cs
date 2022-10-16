using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace CartAPI.Database
{
    public interface IDBTransactionHandler : IDisposable
    {
        void BeginTransaction();
        Task BeginTransactionAsync();
        void CommitTransaction();
        Task CommitTransactionAsync();
        DbTransaction GetTransaction();
        void RollBackTransaction();
        Task RollBackTransactionAsync();
    }
}

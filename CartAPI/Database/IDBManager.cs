namespace CartAPI.Database
{
    public interface IDBManager : IDBConnectionHandler, IDBTransactionHandler
    {
        string ConnectionString { get; set; }
    }
}

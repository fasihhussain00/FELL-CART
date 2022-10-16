namespace CartAPI.Domain.Queries
{
    public class StoredProcedures
    {
        public const string GetCart = "sp_fetchCart";
        public const string GetCartDetail = "sp_fetchCartDetail";
        public const string SaveCart = "sp_saveCartWithDetail";
        public const string DeleteCart = "sp_removeCart";

        public const string AuthCustomer = "sp_authenticateCustomer";
        public const string DeleteCustomer = "sp_removeCustomer";
        public const string SaveCustomer = "sp_saveCustomer";
        public const string GetCustomer = "sp_fetchCustomer";

        public const string GetProduct = "sp_fetchProduct";
        public const string SaveProduct = "sp_saveProduct";
        public const string DeleteProduct = "sp_removeProduct";

        public const string SaveRefreshToken = "sp_saveRefreshToken";

        public const string SaveLogRequest = "sp_saveRequestLogs";
        public const string SaveLogException = "sp_saveExceptionLogs";

        public const string GetLogRequest = "sp_fetchRequestLogs";
        public const string GetLogException = "sp_fetchExceptionLogs";

        public const string GetFilteredLogRequest = "sp_fetchFilteredRequestLogs";
        public const string GetFilteredLogException = "sp_fetchFilteredExceptionLogs";
    }
}

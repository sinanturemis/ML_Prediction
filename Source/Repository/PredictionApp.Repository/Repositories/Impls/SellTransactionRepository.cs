namespace PredictionApp.Repository
{
    /// <summary>
    /// This class executes DB operations on SELL_TRANSACTION table.
    /// </summary>
    public class SellTransactionRepository : DbRepositoryBase<SellTransactionEntity>
    {
        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="connectionString">Connection string to connect database</param>
        public SellTransactionRepository(string connectionString) : base(connectionString) { }
    }
}
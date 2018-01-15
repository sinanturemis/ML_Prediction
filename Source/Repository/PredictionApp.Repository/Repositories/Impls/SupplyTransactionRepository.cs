using Dapper;
using System;
using System.Linq;

namespace PredictionApp.Repository
{
    /// <summary>
    /// This class executes DB operations on SUPPLY_TRANSACTION table.
    /// </summary>
    public class SupplyTransactionRepository : DbRepositoryBase<SupplyTransactionEntity>
    {
        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="connectionString">Connection string to connect database</param>
        public SupplyTransactionRepository(string connectionString) : base(connectionString) { }

        /// <summary>
        /// Gets supplier from SUPPLIER table by id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>supplier entity by id</returns>
        public SupplyTransactionEntity Get(Guid id)
        {
            using (var connection = CreateConnection())
            {
                string selectQuery = @"SELECT * FROM [TRANSACTION].[SUPPLY_TRANSACTION] WHERE [ID] = @id";
                return connection.Query<SupplyTransactionEntity>(selectQuery, new { id = id }).FirstOrDefault();
            }
        }

        /// <summary>
        /// Adds new supply transaction record
        /// </summary>
        /// <param name="entities">supply transaction entity to add</param>
        public void Add(SupplyTransactionEntity entity)
        {
            using (var connection = CreateConnection())
            {
                string query = @"INSERT INTO [TRANSACTION].[SUPPLY_TRANSACTION]([ID],[StaffID],[ProductID],[RestaurantID],[Amount],[ExpirationDate],[OrderDateTime],[ReceivedDateTime]) VALUES (@ID,@StaffID,@ProductID,@RestaurantID,@Amount,@ExpirationDate,@OrderDateTime,@ReceivedDateTime)";
                connection.Execute(query, entity);
            }
        }

        /// <summary>
        /// Updates supply transactions by id
        /// </summary>
        /// <param name="entity">new state of supply transcation entity</param>
        public void Update(SupplyTransactionEntity entity)
        {
            using (var connection = CreateConnection())
            {
                string query = @"UPDATE [TRANSACTION].[SUPPLY_TRANSACTION] SET [ID]= @ID, [StaffID]= @StaffID, [ProductID] = @ProductID, [RestaurantID] =@RestaurantID, [Amount]= @Amount,[ExpirationDate]= @ExpirationDate,[OrderDateTime]= @OrderDateTime,[ReceivedDateTime]= @ReceivedDateTime WHERE [ID]=@ID";
                connection.Execute(query, entity);
            }
        }
    }
}

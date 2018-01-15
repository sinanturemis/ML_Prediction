using Dapper;
using System;
using System.Collections.Generic;

namespace PredictionApp.Repository
{
    /// <summary>
    /// This class executes DB operations on PRODUCT_STOCK_TRANSACTION table.
    /// </summary>
    public class ProductStockTransactionRepository : DbRepositoryBase<ProductStockTransactionEntity>
    {
        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="connectionString">Connection string to connect database</param>
        public ProductStockTransactionRepository(string connectionString) : base(connectionString) { }

        public List<ProductStockTransactionEntity> GetLastNegativeStockTransactions(Guid restaurantId)
        {
            using (var connection = CreateConnection())
            {
                string selectQuery = @"SELECT * FROM [SinanTest].[TRANSACTION].[PRODUCT_STOCK_TRANSACTION]	WHERE CreatedDatetime > (SELECT TOP 1 [CreatedDatetime] FROM [SinanTest].[TRANSACTION].[PRODUCT_STOCK_TRANSACTION]	WHERE RestaurantID = @RestaurantId AND TransactionAmount > 0 ORDER BY CreatedDatetime DESC)";
                return connection.Query<ProductStockTransactionEntity>(selectQuery).AsList();
            }
        }
        /// <summary>
        /// Returns product stock transactions by restaurant and start date
        /// </summary>
        /// <param name="restaurantId">filtered restaurantId</param>
        /// <param name="startDate">start date for query</param>
        /// <returns>product stock transaction entites</returns>
        public List<ProductStockTransactionEntity> Get(Guid restaurantId, DateTime startDate)
        {
            using (var connection = CreateConnection())
            {
                string selectQuery = @"SELECT * FROM [TRANSACTION].[PRODUCT_STOCK_TRANSACTION] WHERE [RestaurantID] = @restaurantId AND CreatedDatetime > @startDate";
                return connection.Query<ProductStockTransactionEntity>(selectQuery, new { restaurantId = restaurantId, startDate = startDate }).AsList();
            }
        }

        /// <summary>
        /// Adds new product stock transaction
        /// </summary>
        /// <param name="entities">product stock transaction entity to add</param>
        public void Add(ProductStockTransactionEntity entity)
        {
            using (var connection = CreateConnection())
            {
                string query = @"INSERT INTO [TRANSACTION].[PRODUCT_STOCK_TRANSACTION] ([RestaurantID],[ProductID],[TransactionAmount],[RemainingAmount],[CreatedDatetime]) VALUES(@RestaurantID,@ProductID,@TransactionAmount,@RemainingAmount,@CreatedDatetime)";
                connection.Execute(query, entity);
            }
        }
    }
}

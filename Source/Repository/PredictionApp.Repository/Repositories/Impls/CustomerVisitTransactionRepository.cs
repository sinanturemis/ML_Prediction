using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PredictionApp.Repository
{
    /// <summary>
    /// This class executes DB operations on CUSTOMER_VISIT_TRANSACTION table.
    /// </summary>
    public class CustomerVisitTransactionRepository : DbRepositoryBase<CustomerVisitTransactionEntity>
    {
        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="connectionString">Connection string to connect database</param>
        public CustomerVisitTransactionRepository(string connectionString) : base(connectionString)
        {

        }

        /// <summary>
        /// Adds new customer visit
        /// </summary>
        /// <param name="entities">customer visit entity to add</param>
        public void Add(IEnumerable<CustomerVisitTransactionEntity> entities)
        {
            using (var connection = CreateConnection())
            {
                string query = @"INSERT INTO [TRANSACTION].[CUSTOMER_VISIT_TRANSACTION]([ID],[ReservationID],[RestaurantID],[CustomerID],[DateIn]) VALUES(@ID,@ReservationID,@RestaurantID,@CustomerID,@DateIn)";
                var result = connection.Execute(query, entities);
            }
        }

        /// <summary>
        /// Gets customer visits from CUSTOMER_VISIT_TRANSACTION table
        /// </summary>
        /// <param name="idList">visit Id List</param>
        /// <returns>Visit transactions on database</returns>
        public CustomerVisitTransactionEntity GetById(Guid id)
        {
            using (var connection = CreateConnection())
            {
                string query = @"SELECT * FROM [TRANSACTION].[CUSTOMER_VISIT_TRANSACTION] WHERE [ID] = @id";
                return connection.Query<CustomerVisitTransactionEntity>(query, new { id = id }).FirstOrDefault();
            }
        }

        /// <summary>
        /// Updates visits by id
        /// </summary>
        /// <param name="entity">new state of updateing entity</param>
        public void Update(CustomerVisitTransactionEntity entities)
        {
            using (var connection = CreateConnection())
            {
                string query = @"UPDATE [TRANSACTION].[CUSTOMER_VISIT_TRANSACTION] SET [DateOut]=@DateOut, [SatisfactionFeedback]=@SatisfactionFeedback WHERE ID=@ID";
                var result = connection.Execute(query, entities);
            }
        }
    }
}

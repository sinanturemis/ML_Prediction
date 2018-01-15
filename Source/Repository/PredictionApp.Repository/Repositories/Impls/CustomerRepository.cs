using Dapper;
using System;
using System.Collections.Generic;

namespace PredictionApp.Repository
{
    /// <summary>
    /// This class executes DB operations on CUSTOMER table.
    /// </summary>
    public class CustomerRepository : DbRepositoryBase<CustomerEntity>
    {
        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="connectionString">Connection string to connect database</param>
        public CustomerRepository(string connectionString) : base(connectionString) { }

        /// <summary>
        ///Gets customers from CUSTOMER table
        /// </summary>
        /// <returns>customer list on database</returns>
        public IEnumerable<CustomerEntity> Get()
        {
            using (var connection = CreateConnection())
            {
                string query = @"SELECT * FROM [COLLECTION].[CUSTOMER]";
                return connection.Query<CustomerEntity>(query);
            }
        }

        /// <summary>
        /// Increase visit count for customer
        /// </summary>
        /// <param name="customerIds">Customer Identities to increase visit count</param>
        public void IncreaseVisitCount(List<Guid> customerIds)
        {
            using (var connection = CreateConnection())
            {
                string query = @"UPDATE [COLLECTION].[CUSTOMER] SET [VisitCountInYear]=[VisitCountInYear] + 1 WHERE ID in @customerIds";
                var result = connection.Execute(query, new { customerIds = customerIds });
            }
        }

        /// <summary>
        /// Adds new customer
        /// </summary>
        /// <param name="entities">customer entity to add</param>
        public void Add(IEnumerable<CustomerEntity> entities)
        {
            using (var connection = CreateConnection())
            {
                string query = @"INSERT INTO [COLLECTION].[CUSTOMER]([ID],[Age],[GenderType],[Latitude],[Longitude],[VisitCountInYear]) VALUES(@ID,@Age,@GenderType,@Latitude,@Longitude,@VisitCountInYear)";
                var result = connection.Execute(query, entities);
            }
        }

        /// <summary>
        /// Updates customer by id
        /// </summary>
        /// <param name="entity">new state of updated entity</param>
        public void Update(CustomerEntity entity)
        {
            using (var connection = CreateConnection())
            {
                string query = @"UPDATE [COLLECTION].[CUSTOMER] SET [Age]=@Age,[GenderType]=@GenderType,[Latitude]=@Latitude,[Longitude]=@Longitude,[VisitCountInYear]=@VisitCountInYear,[LocationGroupId] = @LocationGroupId WHERE ID = @ID";
                var result = connection.Execute(query, entity);
            }
        }
    }
}

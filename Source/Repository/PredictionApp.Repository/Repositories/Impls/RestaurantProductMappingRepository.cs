using Dapper;
using System.Collections.Generic;

namespace PredictionApp.Repository
{
    /// <summary>
    /// This class executes DB operations on RESTAURANT_PRODUCT_MAPPING table.
    /// </summary>
    public class RestaurantProductMappingRepository : DbRepositoryBase<RestaurantProductMappingEntity>
    {
        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="connectionString">Connection string to connect database</param>
        public RestaurantProductMappingRepository(string connectionString) : base(connectionString)
        {
        }

        /// <summary>
        ///Get restaurant-product mappings from RESTAURANT_PRODUCT_MAPPING table
        /// </summary>
        /// <returns>restaurant-product mapping entities on database</returns>
        public IEnumerable<RestaurantProductMappingEntity> Get()
        {
            using (var connection = CreateConnection())
            {
                string query = @"SELECT * FROM [MAP].[RESTAURANT_PRODUCT_MAPPING]";
                return connection.Query<RestaurantProductMappingEntity>(query);
            }
        }

        /// <summary>
        /// Adds new restaurant-product mapping
        /// </summary>
        /// <param name="entities">restaurant-product mapping entity to add</param>
        public void Add(RestaurantProductMappingEntity entity)
        {
            using (var connection = CreateConnection())
            {
                string query = @"INSERT INTO [MAP].[RESTAURANT_PRODUCT_MAPPING]([RestaurantID],[ProductID],[ExpectedStockAmount]) VALUES(@RestaurantID,@ProductID,@ExpectedStockAmount)";
                connection.Execute(query, entity);
            }
        }


    }
}

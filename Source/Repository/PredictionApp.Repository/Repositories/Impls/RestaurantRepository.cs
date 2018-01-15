using Dapper;
using System.Collections.Generic;

namespace PredictionApp.Repository
{
    /// <summary>
    /// This class executes DB operations on RESTAURANT table.
    /// </summary>
    public class RestaurantRepository : DbRepositoryBase<RestaurantEntity>
    {
        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="connectionString">Connection string to connect database</param>
        public RestaurantRepository(string connectionString) : base(connectionString) { }

        /// <summary>
        ///Gets restaurants from RESTAURANT table
        /// </summary>
        /// <returns>restaurant list on database</returns>
        public List<RestaurantEntity> Get()
        {
            using (var connection = CreateConnection())
            {
                string query = @"SELECT * FROM [COLLECTION].[RESTAURANT]";
                return connection.Query<RestaurantEntity>(query).AsList();
            }
        }

        /// <summary>
        ///Gets restaurants from RESTAURANT table by supply day of week
        /// </summary>
        /// <returns>restaurant list on database</returns>
        public List<RestaurantEntity> GetBySupplyDayOfWeek(byte supplyDayOfWeek)
        {
            using (var connection = CreateConnection())
            {
                string query = @"SELECT * FROM [COLLECTION].[RESTAURANT] WHERE [SupplyDayOfWeek] = @SupplyDayOfWeek";
                return connection.Query<RestaurantEntity>(query, new { SupplyDayOfWeek = supplyDayOfWeek }).AsList();
            }
        }

        /// <summary>
        /// Adds new restaurant
        /// </summary>
        /// <param name="entities">restaurant entity to add</param>
        public void Add(IEnumerable<RestaurantEntity> entities)
        {
            using (var connection = CreateConnection())
            {
                string query = @"INSERT INTO [COLLECTION].[RESTAURANT]([ID],[Latitude],[Longitude],[SupplyDayOfWeek]) VALUES(@ID,@Latitude,@Longitude,@SupplyDayOfWeek)";
                connection.Execute(query, entities);
            }
        }
    }
}

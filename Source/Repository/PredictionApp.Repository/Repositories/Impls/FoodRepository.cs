using Dapper;
using System.Collections.Generic;

namespace PredictionApp.Repository
{
    /// <summary>
    /// This class executes DB operations on FOOD table.
    /// </summary>
    public class FoodRepository : DbRepositoryBase<FoodEntity>
    {
        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="connectionString">Connection string to connect database</param>
        public FoodRepository(string connectionString) : base(connectionString) { }

        /// <summary>
        /// Adds new food
        /// </summary>
        /// <param name="entities">food entity to add</param>
        public void Add(IEnumerable<FoodEntity> entities)
        {
            using (var connection = CreateConnection())
            {
                string query = @"INSERT INTO [COLLECTION].[FOOD]([ID],[Name],[Price]) VALUES(@ID,@Name,@Price)";
                connection.Execute(query, entities);
            }
        }
    }
}

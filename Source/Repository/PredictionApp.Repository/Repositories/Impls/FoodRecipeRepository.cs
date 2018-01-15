using Dapper;
using System.Collections.Generic;

namespace PredictionApp.Repository
{
    /// <summary>
    /// This class executes DB operations on FOOD_RECIPE table.
    /// </summary>
    public class FoodRecipeRepository : DbRepositoryBase<FoodRecipeEntity>
    {
        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="connectionString">Connection string to connect database</param>
        public FoodRecipeRepository(string connectionString) : base(connectionString) { }

        /// <summary>
        /// Adds new food recipe
        /// </summary>
        /// <param name="entities">food recipe entity to add</param>
        public void Add(IEnumerable<FoodRecipeEntity> entities)
        {
            using (var connection = CreateConnection())
            {
                string query = @"INSERT INTO [COLLECTION].[FOOD_RECIPE]([FoodID],[ProductID],[ProductAmount]) VALUES(@FoodID,@ProductID,@ProductAmount)";
                connection.Execute(query, entities);
            }
        }
    }
}

using Dapper;
using System.Collections.Generic;

namespace PredictionApp.Repository
{
    /// <summary>
    /// This class executes DB operations on TABLE table.
    /// </summary>
    public class TableRepository : DbRepositoryBase<TableEntity>
    {
        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="connectionString">Connection string to connect database</param>
        public TableRepository(string connectionString) : base(connectionString) { }

        /// <summary>
        /// Adds new table
        /// </summary>
        /// <param name="entities">table entity to add</param>
        public void Add(IEnumerable<TableEntity> entities)
        {
            using (var connection = CreateConnection())
            {
                string query = @"INSERT INTO [COLLECTION].[TABLE]([ID],[RestaurantID],[MaxCapacity],[Status]) VALUES(@ID,@RestaurantID,@MaxCapacity,@Status)";
                connection.Execute(query, entities);
            }
        }
    }
}

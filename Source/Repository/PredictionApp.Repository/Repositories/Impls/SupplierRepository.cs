using Dapper;
using System.Collections.Generic;

namespace PredictionApp.Repository
{
    /// <summary>
    /// This class executes DB operations on SUPPLIER table.
    /// </summary>
    public class SupplierRepository : DbRepositoryBase<SupplierEntity>
    {
        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="connectionString">Connection string to connect database</param>
        public SupplierRepository(string connectionString) : base(connectionString) { }

        /// <summary>
        ///Gets suppliers from SUPPLIER table
        /// </summary>
        /// <returns>supplier list on database</returns>
        public List<SupplierEntity> Get()
        {
            using (var connection = CreateConnection())
            {
                string selectQuery = @"SELECT * FROM [COLLECTION].[SUPPLIER]";
                return connection.Query<SupplierEntity>(selectQuery).AsList();
            }
        }

        /// <summary>
        /// Adds new supplier
        /// </summary>
        /// <param name="entities">supplier entity to add</param>
        public void Add(IEnumerable<SupplierEntity> entities)
        {
            using (var connection = CreateConnection())
            {
                string query = @"INSERT INTO [COLLECTION].[SUPPLIER]([ID],[Latitude],[Longitude],[SatisfactionIndex]) VALUES(@ID,@Latitude,@Longitude,@SatisfactionIndex)";
                connection.Execute(query, entities);
            }
        }
    }
}

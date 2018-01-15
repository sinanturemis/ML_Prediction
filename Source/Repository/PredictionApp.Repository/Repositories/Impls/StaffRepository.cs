using Dapper;
using System.Collections.Generic;
using System;

namespace PredictionApp.Repository
{
    /// <summary>
    /// This class executes DB operations on STAFF table.
    /// </summary>
    public class StaffRepository : DbRepositoryBase<StaffEntity>
    {
        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="connectionString">Connection string to connect database</param>
        public StaffRepository(string connectionString) : base(connectionString) { }

        /// <summary>
        ///Gets staffs from STAFF table
        /// </summary>
        /// <returns>staff list on database</returns>
        public List<StaffEntity> Get()
        {
            using (var connection = CreateConnection())
            {
                string selectQuery = @"SELECT * FROM [COLLECTION].[STAFF]";
                return connection.Query<StaffEntity>(selectQuery).AsList();
            }
        }

        /// <summary>
        /// Gets staffs from STAFF table by restaurantId
        /// </summary>
        /// <param name="workplaceID">restaurantId to filter</param>
        /// <returns>staff list on database</returns>
        public List<StaffEntity> Get(Guid workplaceID)
        {
            using (var connection = CreateConnection())
            {
                string selectQuery = @"SELECT * FROM [COLLECTION].[STAFF] WHERE WorkplaceID = @workplaceID";
                return connection.Query<StaffEntity>(selectQuery, new { workplaceID = workplaceID }).AsList();
            }
        }

        /// <summary>
        /// Adds new staff
        /// </summary>
        /// <param name="entities">staff entity to add</param>
        public void Add(IEnumerable<StaffEntity> entities)
        {
            using (var connection = CreateConnection())
            {
                string query = @"INSERT INTO [COLLECTION].[STAFF]([ID],[WorkplaceID]) VALUES(@ID,@WorkplaceID)";
                connection.Execute(query, entities);
            }
        }
    }
}

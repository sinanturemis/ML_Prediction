using Dapper;
using System.Collections.Generic;

namespace PredictionApp.Repository
{
    /// <summary>
    /// This class executes DB operations on PRODUCT table.
    /// </summary>
    public class ProductRepository : DbRepositoryBase<ProductEntity>
    {
        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="connectionString">Connection string to connect database</param>
        public ProductRepository(string connectionString) : base(connectionString) { }

        /// <summary>
        ///Gets products from PRODUCT table
        /// </summary>
        /// <returns>product list on database</returns>
        public List<ProductEntity> Get()
        {
            using (var connection = CreateConnection())
            {
                string selectQuery = @"SELECT * FROM [COLLECTION].[PRODUCT]";
                return connection.Query<ProductEntity>(selectQuery).AsList();
            }
        }

        /// <summary>
        /// Adds new product
        /// </summary>
        /// <param name="entities">product entity to add</param>
        public void Add(IEnumerable<ProductEntity> entities)
        {
            using (var connection = CreateConnection())
            {
                string query = @"INSERT INTO [COLLECTION].[PRODUCT]([ID],[Name],[ExpectedUnitsInStock],[UnitPrice],[SupplierID]) VALUES(@ID,@Name,@ExpectedUnitsInStock,@UnitPrice,@SupplierID)";
                connection.Execute(query, entities);
            }
        }
    }
}

using System.Data;
using System.Data.SqlClient;

namespace PredictionApp.Repository
{
    /// <summary>
    /// This class is a generic base class to reduce repetitive tasks in repositories.
    /// </summary>
    /// <typeparam name="T">an entity class derived from IEntity type</typeparam>
    public abstract class DbRepositoryBase<T> : IRepository<T> where T : IEntity
    {
        /// <summary>
        /// Connection string to connect database
        /// </summary>
        protected string _connectionString;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString">connection string for repositories to connect database</param>
        protected DbRepositoryBase(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Creates an database stream to mssql database
        /// </summary>
        /// <returns></returns>
        protected IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}

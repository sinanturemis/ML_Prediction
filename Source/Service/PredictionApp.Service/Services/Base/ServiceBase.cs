using System;
using System.Transactions;

namespace PredictionApp.Service
{
    /// <summary>
    /// This class is a base class to reduce repetitive tasks in services.
    /// </summary>
    public abstract class ServiceBase
    {
        /// <summary>
        /// Required database configurations for services
        /// </summary>
        protected DbSettings _dbSettings;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbSettings">required for db transaction scope operations</param>
        protected ServiceBase(DbSettings dbSettings)
        {
            _dbSettings = dbSettings;
        }

        /// <summary>
        /// Creates db transactions
        /// </summary>
        /// <returns>created transaction scope</returns>
        protected TransactionScope CreateTransactionScope()
        {
            return new TransactionScope(TransactionScopeOption.RequiresNew, TimeSpan.FromMinutes(_dbSettings.TransactionScopeTimeoutInMunites));
        }

    }
}

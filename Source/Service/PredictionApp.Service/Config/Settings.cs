namespace PredictionApp.Service
{
    /// <summary>
    /// Configuration DTO (Data transfer object)
    /// </summary>
    public class Settings
    {
        public DbSettings DbSettings { get; set; }
        public GenerationCounts GenerationCounts { get; set; }
        public CustomerSettings CustomerSettings { get; set; }
    }

    /// <summary>
    /// Configuration DTO for DB settings
    /// </summary>
    public class DbSettings
    {
        public string ConnectionString { get; set; }
        public int TransactionScopeTimeoutInMunites { get; set; }

    }

    /// <summary>
    /// Configuration DTO for item generation settings
    /// </summary>
    public class GenerationCounts
    {
        public int RestaurantsCount { get; set; }
        public int StaffsCount { get; set; }
        public int SuppliersCount { get; set; }
        public int ProductTypesCount { get; set; }
        public int FoodsCount { get; set; }
        public int CustomersCount { get; set; }

    }

    /// <summary>
    /// Configuration DTO for customer settings
    /// </summary>
    public class CustomerSettings
    {
        public int MaxVisitCountInYear { get; set; }

    }
}
using System;

namespace PredictionApp.Repository
{
    public class ProductEntity : IEntity
    {
        public Guid ID { get; set; }
        public Guid SupplierID { get; set; }
        public string Name { get; set; }
        public int ExpectedUnitsInStock { get; set; }
        public double UnitPrice { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}

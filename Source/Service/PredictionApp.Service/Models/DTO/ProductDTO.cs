using System;

namespace PredictionApp.Service
{
    public class ProductDTO
    {
        public Guid ID { get; set; }
        public Guid SupplierID { get; set; }
        public string Name { get; set; }
        public double UnitPrice { get; set; }
    }
}

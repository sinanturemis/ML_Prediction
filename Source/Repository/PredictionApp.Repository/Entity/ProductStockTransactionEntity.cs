using System;

namespace PredictionApp.Repository
{
    public class ProductStockTransactionEntity : IEntity
    {
        public Guid RestaurantID { get; set; }
        public Guid ProductID { get; set; }
        public double TransactionAmount { get; set; }
        public double RemainingAmount { get; set; }
        public DateTime CreatedDatetime { get; set; }
    }
}

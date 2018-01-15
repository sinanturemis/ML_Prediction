using System;

namespace PredictionApp.Service
{
    public class ProductStockTransactionDTO
    {
        public Guid RestaurantID { get; set; }
        public Guid ProductID { get; set; }
        public double TransactionAmount { get; set; }
        public double RemainingAmount { get; set; }
        public DateTime CreatedDatetime { get; set; }
    }
}

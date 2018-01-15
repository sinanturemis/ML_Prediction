using System;

namespace PredictionApp.Repository
{
    public class SupplyTransactionEntity : IEntity
    {
        public Guid ID { get; set; }
        public Guid StaffID { get; set; }
        public Guid ProductID { get; set; }
        public Guid RestaurantID { get; set; }
        public double Amount { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime OrderDateTime { get; set; }
        public DateTime? ReceivedDateTime { get; set; }

    }
}

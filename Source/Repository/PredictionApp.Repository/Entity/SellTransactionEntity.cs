using System;

namespace PredictionApp.Repository
{
    public class SellTransactionEntity : IEntity
    {
        public Guid ID { get; set; }
        public Guid ReservationID { get; set; }
        public Guid StaffID { get; set; }
        public Guid FoodID { get; set; }
        public double OrderedAmount { get; set; }
        public double DeliveredAmount { get; set; }
        public DateTime OrderDateTime { get; set; }
        public bool IsAnomaly { get; set; }
    }
}

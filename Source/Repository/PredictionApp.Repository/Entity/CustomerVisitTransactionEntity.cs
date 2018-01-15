using System;

namespace PredictionApp.Repository
{
    public class CustomerVisitTransactionEntity : IEntity
    {
        public Guid ID { get; set; }
        public Guid ReservationID { get; set; }
        public Guid RestaurantID { get; set; }
        public Guid CustomerID { get; set; }
        public DateTime DateIn { get; set; }
        public DateTime? DateOut { get; set; }
        public string SatisfactionFeedback { get; set; }
    }
}

using System;

namespace PredictionApp.Repository
{
    public class TableEntity : IEntity
    {
        public Guid ID { get; set; }
        public Guid RestaurantID { get; set; }
        public byte MaxCapacity { get; set; }
    }
}

using System;

namespace PredictionApp.Repository
{
    public class RestaurantEntity : IEntity
    {
        public Guid ID { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public byte SupplyDayOfWeek { get; set; }

    }
}

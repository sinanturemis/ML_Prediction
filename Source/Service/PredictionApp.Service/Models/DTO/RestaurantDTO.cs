using System;

namespace PredictionApp.Service
{
    public class RestaurantDTO
    {
        public Guid ID { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public byte SupplyDayOfWeek { get; set; }
    }
}

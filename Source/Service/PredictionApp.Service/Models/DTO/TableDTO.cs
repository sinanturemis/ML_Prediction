using System;

namespace PredictionApp.Service
{
    public class TableDTO
    {
        public Guid ID { get; set; }
        public Guid RestaurantID { get; set; }
        public byte MaxCapacity { get; set; }
        public byte Status { get; set; }
    }
}

using System;

namespace PredictionApp.Service
{
    public class RestaurantProductMappingDTO
    {
        public Guid RestaurantID { get; set; }
        public Guid ProductID { get; set; }
        public double ExpectedStockAmount { get; set; }
    }
}

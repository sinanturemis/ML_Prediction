using System;

namespace PredictionApp.Repository
{
    public class RestaurantProductMappingEntity : IEntity
    {
        public Guid RestaurantID { get; set; }
        public Guid ProductID { get; set; }
        public double ExpectedStockAmount { get; set; }
    }
}

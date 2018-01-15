using System.Collections.Generic;

namespace PredictionApp.Service
{
    public class GetRestaurantsBySupplyDayOfWeekResponse : ResponseBase
    {
        public List<RestaurantDTO> Restaurants { get; set; }
    }
}

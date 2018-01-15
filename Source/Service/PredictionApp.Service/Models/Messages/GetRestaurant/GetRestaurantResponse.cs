using System.Collections.Generic;

namespace PredictionApp.Service
{
    public class GetRestaurantResponse : ResponseBase
    {
        public List<RestaurantDTO> Restaurants { get; set; }
    }
}

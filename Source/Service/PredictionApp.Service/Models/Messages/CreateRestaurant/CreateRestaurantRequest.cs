using System.Collections.Generic;

namespace PredictionApp.Service
{
    public class CreateRestaurantRequest : RequestBase
    {
        public List<RestaurantDTO> Restaurants { get; set; }

        public List<TableDTO> Tables { get; set; }
    }
}

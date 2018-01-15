using System.Collections.Generic;

namespace PredictionApp.Service
{
    public class GetRestaurantProductMappingResponse : ResponseBase
    {
        public List<RestaurantProductMappingDTO> RestaurantProductMappings { get; set; }
    }
}

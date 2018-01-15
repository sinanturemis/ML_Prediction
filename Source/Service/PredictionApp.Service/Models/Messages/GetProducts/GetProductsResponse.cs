using System.Collections.Generic;

namespace PredictionApp.Service
{
    public class GetProductsResponse : ResponseBase
    {
        public List<ProductDTO> Products { get; set; }
    }
}

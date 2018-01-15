using System.Collections.Generic;

namespace PredictionApp.Service
{
    public class CreateProductRequest : RequestBase
    {
        public List<ProductDTO> Products { get; set; }
        public CreateProductRequest()
        {
            Products = new List<ProductDTO>();
        }
    }
}

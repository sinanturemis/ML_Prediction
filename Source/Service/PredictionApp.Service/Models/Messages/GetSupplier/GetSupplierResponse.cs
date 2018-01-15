using System.Collections.Generic;

namespace PredictionApp.Service
{
    public class GetSupplierResponse : ResponseBase
    {
        public List<SupplierDTO> Suppliers { get; set; }
    }
}

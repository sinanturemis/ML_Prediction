using System.Collections.Generic;

namespace PredictionApp.Service
{
    public class CreateSupplierRequest : RequestBase
    {
        public List<SupplierDTO> Suppliers { get; set; }
    }
}

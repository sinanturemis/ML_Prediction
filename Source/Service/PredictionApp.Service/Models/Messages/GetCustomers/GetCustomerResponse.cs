using System.Collections.Generic;

namespace PredictionApp.Service
{
    public class GetCustomerResponse : ResponseBase
    {
        public List<CustomerDTO> Customers { get; set; }
    }
}

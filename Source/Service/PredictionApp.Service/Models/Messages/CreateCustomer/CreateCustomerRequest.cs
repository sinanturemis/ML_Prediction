using System.Collections.Generic;

namespace PredictionApp.Service
{
    public class CreateCustomerRequest : RequestBase
    {
        public List<CustomerDTO> CustomersToCreate { get; set; }
    }
}

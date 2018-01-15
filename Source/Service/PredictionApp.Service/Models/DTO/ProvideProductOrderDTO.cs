using System;

namespace PredictionApp.Service
{
    public class ProvideProductOrderDTO
    {
        public Guid OrderID { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}

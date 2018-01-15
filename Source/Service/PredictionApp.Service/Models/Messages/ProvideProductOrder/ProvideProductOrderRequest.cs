using System;

namespace PredictionApp.Service
{
    public class ProvideProductOrderRequest : RequestBase
    {
        public Guid OrderID { get; set; }
        public double UnitsInStock { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime ReceivedDateTime { get; set; }
    }
}

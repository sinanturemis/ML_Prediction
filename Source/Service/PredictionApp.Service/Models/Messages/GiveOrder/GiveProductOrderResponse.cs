using System;

namespace PredictionApp.Service
{
    public class GiveProductOrderResponse : ResponseBase
    {
        public Guid OrderID { get; set; }
    }
}

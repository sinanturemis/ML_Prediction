using System;

namespace PredictionApp.Service
{
    public class SupplierDTO
    {
        public Guid ID { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double SatisfactionIndex { get; set; }
    }
}

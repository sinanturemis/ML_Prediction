using System;

namespace PredictionApp.Repository
{
    public class SupplierEntity : IEntity
    {
        public Guid ID { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double SatisfactionIndex { get; set; }
    }
}

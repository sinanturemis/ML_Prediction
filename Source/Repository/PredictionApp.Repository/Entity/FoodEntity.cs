using System;

namespace PredictionApp.Repository
{
    public class FoodEntity: IEntity
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

    }
}

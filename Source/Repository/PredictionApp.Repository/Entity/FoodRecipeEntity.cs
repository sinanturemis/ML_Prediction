using System;

namespace PredictionApp.Repository
{
    public class FoodRecipeEntity: IEntity
    {
        public Guid FoodID { get; set; }
        public Guid ProductID { get; set; }
        public double ProductAmount { get; set; }
    }
}

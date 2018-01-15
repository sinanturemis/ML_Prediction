using System;

namespace PredictionApp.Service
{
    public class FoodRecipeDTO
    {
        public Guid FoodID { get; set; }
        public Guid ProductID { get; set; }
        public double ProductAmount { get; set; }
    }
}

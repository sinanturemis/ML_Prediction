using System.Collections.Generic;

namespace PredictionApp.Service
{
    public class CreateFoodRequest : RequestBase
    {
        public List<FoodDTO> Foods { get; set; }

        public List<FoodRecipeDTO> Ingredents { get; set; }

        public CreateFoodRequest()
        {
            Ingredents = new List<FoodRecipeDTO>();
            Foods = new List<FoodDTO>();
        }
    }
}

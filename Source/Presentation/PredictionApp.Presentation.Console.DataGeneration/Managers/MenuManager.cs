using PredictionApp.Common;
using PredictionApp.Common.Extension;
using PredictionApp.Common.Helpers;
using PredictionApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PredictionApp.Presentation.Console.DataGeneration
{
    public class MenuManager
    {
        #region Fields

        /// <summary>
        /// a service to manage food operations on datasource 
        /// </summary>
        private FoodService _foodService;

        /// <summary>
        /// a service to manage product operations on datasource 
        /// </summary>
        private ProductService _productService;

        #endregion Fields

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="foodService">foodService</param>
        /// <param name="productService">productService</param>
        public MenuManager(FoodService foodService, ProductService productService)
        {
            _foodService = foodService;
            _productService = productService;
        }

        #endregion Constructor

        #region Managers

        /// <summary>
        /// Produces new foods and add them to data source
        /// </summary>
        /// <param name="count"></param>
        public void GenerateFoods(int count)
        {
            var products = _productService.Get(new GetProductsRequest()).Products;
            var createFoodRequest = PrepareFoods(count, products);
            _foodService.Create(createFoodRequest);
        }

        #endregion Managers

        #region Helpers

        /// <summary>
        /// Prepares random generated foods and food recipe lists
        /// </summary>
        /// <param name="count">>how many foods will be created</param>
        /// <param name="allProducts">product list for ingredents</param>
        /// <returns></returns>
        private CreateFoodRequest PrepareFoods(int count, List<ProductDTO> allProducts)
        {
            List<FoodDTO> foods = new List<FoodDTO>();
            List<FoodRecipeDTO> ingredents = new List<FoodRecipeDTO>();

            FoodDTO food;
            for (int i = 0; i < count; i++)
            {
                food = GenerateFood();
                foods.Add(food);
                ingredents.AddRange(GenerateIngredents(food.ID, allProducts));
            }

            return new CreateFoodRequest { Foods = foods, Ingredents = ingredents };
        }

        /// <summary>
        /// Returns random generated foodDto
        /// </summary>
        /// <returns>random generated foodDto</returns>
        private FoodDTO GenerateFood()
        {
            return new FoodDTO
            {
                ID = Guid.NewGuid(),
                Name = RandomHelper.RandomString(8),
                Price = (new Random()).NextDouble().ToPriceFormat()
            };
        }

        /// <summary>
        /// Generates food recipes
        /// </summary>
        /// <param name="foodIdForIngredents">for which food</param>
        /// <param name="products">product list for ingredents</param>
        /// <returns></returns>
        private List<FoodRecipeDTO> GenerateIngredents(Guid foodIdForIngredents, List<ProductDTO> products)
        {
            List<FoodRecipeDTO> ingredents = new List<FoodRecipeDTO>();

            //select how many product will be used in this food.
            var ingredentsCount = RandomHelper.RandomInteger(Constants.IngredentsAmount.Min, Constants.IngredentsAmount.Max);

            var ingredentsCounter = 0;
            while (ingredentsCount > ingredentsCounter)
            {
                var selectedProduct = GetRandomProduct(products);

                //If selected product already exist in ingredent list, then continue.
                if (ingredents.Any(x => x.ProductID == selectedProduct.ID))
                    continue;

                //Otherwise, add product in ingredents
                ingredents.Add(new FoodRecipeDTO
                {
                    FoodID = foodIdForIngredents,
                    ProductAmount = RandomHelper.RandomDouble(Constants.SameProductAmountInFood.Min, Constants.SameProductAmountInFood.Max),
                    ProductID = selectedProduct.ID
                });

                ingredentsCounter++;
            }

            return ingredents;
        }

        /// <summary>
        /// Returns a random product within product list
        /// </summary>
        /// <param name="products">all products</param>
        /// <returns>selected product</returns>
        private ProductDTO GetRandomProduct(List<ProductDTO> products)
        {
            return products[RandomHelper.RandomInteger(products.Count)];
        }

        #endregion Helpers
    }
}
